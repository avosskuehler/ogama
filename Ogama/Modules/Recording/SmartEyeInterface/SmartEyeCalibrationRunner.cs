// <copyright file="SmartEyeCalibrationRunner.cs" company="Smart Eye AB">
// ******************************************************
// OGAMA - open gaze and mouse analyzer 
// Copyright (C) 2015 Dr. Adrian Voßkühler  
// ------------------------------------------------------------------------
// This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
// This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
// You should have received a copy of the GNU General Public License along with this program; if not, write to the Free Software Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
// **************************************************************
// </copyright>
// <author>Kathrin Scheil</author>
// <email>kathrin.scheil@smarteye.se</email>

namespace Ogama.Modules.Recording.SmartEyeInterface
{
  using System;
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.Diagnostics;
  using System.Drawing;
  using System.Linq;
  using System.Text;
  using System.Text.RegularExpressions;
  using System.Windows.Forms;
  using Ogama.ExceptionHandling;
  using Ogama.Modules.Common.Tools;
  using SmartEye.Geometry;
  using SmartEye.Network;
  using SmartEye.Rpc.Json;
  using SmartEye.Tracker;
  using SmartEye.WorldModel;

  /// <summary>
  /// The Smart Eye calibration state.
  /// </summary>
  public enum CalibrationState
  {
    /// <summary>
    /// Not calibrated, initial state
    /// </summary>
    Uncalibrated,

    /// <summary>
    /// Waiting state
    /// </summary>
    Idle,

    /// <summary>
    /// Collecting samples
    /// </summary>
    CollectingSamples,

    /// <summary>
    /// Calculating the calibration
    /// </summary>
    Calculating,

    /// <summary>
    /// Done with calibration
    /// </summary>
    Done,

    /// <summary>
    /// Applied the calibration
    /// </summary>
    Applied,

    /// <summary>
    /// Aborted calibration
    /// </summary>
    Abort
  }

  /// <summary>
  /// The Smart Eye calibration runner
  /// </summary>
  public class SmartEyeCalibrationRunner
  {
    ///////////////////////////////////////////////////////////////////////////////
    // Defining Variables, Enumerations, Events                                  //
    ///////////////////////////////////////////////////////////////////////////////
    #region FIELDS

    /// <summary>
    /// The Smart Eye calibration form to draw on.
    /// </summary>
    private readonly SmartEyeCalibrationForm smartEyeCalibrationForm;

    /// <summary>
    /// The Smart Eye settings.
    /// </summary>
    private SmartEyeSetting settings;

    /// <summary>
    /// The Smart Eye client with RPC and UDP connection.
    /// </summary>
    private SmartEyeClient client;

    /// <summary>
    /// The queue of calibration points.
    /// </summary>
    private Queue<Point2D> calibrationPoints;

    /// <summary>
    /// The list wit Calibration results.
    /// </summary>
    private List<CalibrationResult> calibrationResult;

    /// <summary>
    /// No gaze calibration starts without enough good-quality gaze samples.
    /// </summary>
    private int goodTrackingCount;

    /// <summary>
    /// Current calibration point id.
    /// </summary>
    private int id;

    /// <summary>
    /// The presentation screen name in the Smart Eye world model.
    /// </summary>
    private string screenName;

    /// <summary>
    /// The calibration state.
    /// </summary>
    private CalibrationState state;

    /// <summary>
    /// The timer activated when the calibration form is loaded.
    /// </summary>
    private Timer initTimer;

    /// <summary>
    /// Stopwatch for the collection of gaze samples.
    /// </summary>
    private Stopwatch collectSamplesStopwatch;

    /// <summary>
    /// Timer while collecting samples (to make abort possible).
    /// </summary>
    private Timer collectSamplesTimer;

    /// <summary>
    /// The collection time for each point.
    /// </summary>
    private int calibTime;

    /// <summary>
    /// The timer for a short pause in-between collection times.
    /// </summary>
    private Timer goToNextPointTimer;

    /// <summary>
    /// The background worker for the calibration process.
    /// </summary>
    private BackgroundWorker calibrationWorker;

    #endregion FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the <see cref="SmartEyeCalibrationRunner"/> class.
    /// </summary>
    /// <param name="client">The <see cref="SmartEyeClient"/></param>
    /// <param name="settings">The <see cref="SmartEyeSetting"/></param>
    public SmartEyeCalibrationRunner(SmartEyeClient client, SmartEyeSetting settings)
    {
      this.client = client;
      this.client.UdpSocket.PacketReceived += this.BaseClientOnPacketReceived;
      this.client.PropertyChanged += this.SmartEyeClientPropertyChanged;

      this.settings = settings;

      this.smartEyeCalibrationForm = new SmartEyeCalibrationForm();
      this.smartEyeCalibrationForm.Load += this.SmartEyeCalibrationFormLoaded;
      this.smartEyeCalibrationForm.KeyDown += this.SmartEyeCalibrationFormKeyDown;

      this.initTimer = new Timer() { Interval = 50 };
      this.initTimer.Tick += this.CheckStartCalibration;

      this.collectSamplesTimer = new Timer() { Interval = 50 };
      this.collectSamplesTimer.Tick += this.CheckCollectSamples;

      this.HasShownMessage = false;
    }

    #endregion CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets or sets a value indicating whether an error message has been displayed so that it is not displayed twice
    /// </summary>
    public bool HasShownMessage { get; set; }

    #endregion PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Methods for doing main class job                                          //
    ///////////////////////////////////////////////////////////////////////////////
    #region METHODS

    /// <summary>
    /// The method runs the calibration of the Smart Eye device.
    /// </summary>
    /// <returns>A list of <see cref="CalibrationResult"/> objects</returns>
    public List<CalibrationResult> RunCalibration()
    {
      if (this.PrepareForCalibration())
      {
        PresentationScreen.PutFormOnPresentationScreen(this.smartEyeCalibrationForm, true);
        this.smartEyeCalibrationForm.ShowDialog();

        return this.calibrationResult;
      }

      return null;
    }

    /// <summary>
    /// Aborts calibration.
    /// </summary>
    public void AbortCalibration()
    {
      if (this.state == CalibrationState.Done)
      {
        return;
      }

      if (this.initTimer != null)
      {
        this.initTimer.Dispose();
      }

      if (this.collectSamplesTimer != null)
      {
        this.collectSamplesTimer.Dispose();
      }

      if (this.goToNextPointTimer != null)
      {
        this.goToNextPointTimer.Dispose();
      }

      if (this.state == CalibrationState.CollectingSamples)
      {
        try
        {
          this.client.RpcClient.StopCollectSamples();
        }
        catch (Exception ex)
        {
          if (this.settings.SilentMode)
          {
            ExceptionMethods.HandleExceptionSilent(ex);
          }
          else
          {
            ExceptionMethods.HandleException(ex);
          }
        }
      }

      if (this.state == CalibrationState.Calculating)
      {
        this.calibrationWorker.RunWorkerCompleted -= this.CalibrationCompleted;
        this.calibrationWorker.CancelAsync();
      }

      this.state = CalibrationState.Abort;

      if (this.client != null)
      {
        this.client.IsCalibrating = false;
      }

      if (this.client != null && this.client.UdpSocket != null)
      {
        this.client.UdpSocket.PacketReceived -= this.BaseClientOnPacketReceived;
      }

      if (this.client != null && this.client.RpcClient != null)
      {
        if (!this.client.PingRPC())
        {
          // Reconnect to RPC
          this.client.ConnectRPC();
        }
      }

      this.calibrationResult = null;
      this.smartEyeCalibrationForm.BeginInvoke(new Action(() => this.smartEyeCalibrationForm.Dispose()));
    }

    /// <summary>
    /// Prepare for calibration by resetting profile and fields
    /// </summary>
    /// <returns>A <see cref="bool"/> with the tracking state.</returns>
    private bool PrepareForCalibration()
    {
      this.HasShownMessage = false;

      try
      {
        string wm;
        this.client.RpcClient.GetWorldModel(out wm);
        var wmClean = Regex.Replace(wm, @"[ \t\r\f]", string.Empty);

        var h = wmClean.IndexOf("Screen:{");

        if (h >= 0)
        {
          var wmScreenSub = wmClean.Substring(h);

          var i = wmScreenSub.IndexOf("name=\"");
          if (i >= 0)
          {
            var wmSub = wmScreenSub.Substring(i + 6);
            var j = wmSub.IndexOf("\"");
            this.screenName = wmSub.Substring(0, j);
          }

          i = wmScreenSub.IndexOf("resolution=");
          if (i >= 0)
          {
            var wmSub2 = wmScreenSub.Substring(i + 11);
            var j2 = wmSub2.IndexOf(",");
            var screenWidthString = wmSub2.Substring(0, j2);
            var screenWidth = Convert.ToInt32(screenWidthString);

            var j3 = wmSub2.IndexOf("\n");
            var screenHeightString = wmSub2.Substring(j2 + 1, j3 - j2 - 1);
            var screenHeight = Convert.ToInt32(screenHeightString);

            if (screenWidth != PresentationScreen.GetPresentationResolution().Width ||
                screenHeight != PresentationScreen.GetPresentationResolution().Height)
            {
              string message = "The resolution of the presentation screen: " + PresentationScreen.GetPresentationResolution().Width + "x"
               + PresentationScreen.GetPresentationResolution().Height + " is different from the size "
               + "specified in the Smart Eye World Model: " + screenWidthString + "x" + screenHeightString + "."
               + Environment.NewLine
               + "Please change the display settings or the Smart Eye World Model to fit the resolution specified in the experiment settings.";

              ExceptionMethods.ProcessMessage("Resolution mismatch", message);
              this.HasShownMessage = true;
              return false;
            }
          }
        }
        else
        {
          string message = "There is no screen specified in the Smart Eye World Model, please edit it!";
          ExceptionMethods.ProcessMessage("Screen not defined in World Model", message);
          this.HasShownMessage = true;
          return false;
        }
      }
      catch (Exception ex)
      {
        if (this.settings.SilentMode)
        {
          ExceptionMethods.HandleExceptionSilent(ex);
        }
        else
        {
          ExceptionMethods.HandleException(ex);
          this.HasShownMessage = true;
        }

        this.AbortCalibration();
        return false;
      }

      this.ResetValues();
      this.smartEyeCalibrationForm.BackColor = this.settings.CalibBackgroundColor;
      return true;
    }

    /// <summary>
    /// Reset values before starting new calibration.
    /// </summary>
    private void ResetValues()
    {
      this.state = CalibrationState.Uncalibrated;

      this.goodTrackingCount = 0;
      this.id = 0;

      this.calibTime = 750 * (4 - this.settings.CalibPointSpeed);
      
      this.CreatePointList();
      this.calibrationResult = new List<CalibrationResult>();
      int i = 0;
      foreach (var point in this.calibrationPoints)
      {
        this.calibrationResult.Add(new CalibrationResult(point, i));
        i++;
      }

      try
      {
        this.client.RpcClient.ClearGazeCalibration();
        this.client.RpcClient.ClearAllTargetSamples();
      }
      catch (Exception ex)
      {
        if (this.settings.SilentMode)
        {
          ExceptionMethods.HandleExceptionSilent(ex);
        }
        else
        {
          ExceptionMethods.HandleException(ex);
          this.HasShownMessage = true;
        }

        this.AbortCalibration();
        return;
      }
    }

    /// <summary>
    /// Create new calibration point list with current setting.
    /// </summary>
    private void CreatePointList()
    {
      this.calibrationPoints = new Queue<Point2D>();

      switch (this.settings.NumCalibPoints)
      {
        case 3:
          this.calibrationPoints.Enqueue(new Point2D(0.1, 0.1));
          this.calibrationPoints.Enqueue(new Point2D(0.9, 0.1));
          this.calibrationPoints.Enqueue(new Point2D(0.1, 0.9));
          break;
        case 5:
          this.calibrationPoints.Enqueue(new Point2D(0.1, 0.1));
          this.calibrationPoints.Enqueue(new Point2D(0.5, 0.5));
          this.calibrationPoints.Enqueue(new Point2D(0.9, 0.1));
          this.calibrationPoints.Enqueue(new Point2D(0.9, 0.9));
          this.calibrationPoints.Enqueue(new Point2D(0.1, 0.9));
          break;
        case 9:
          this.calibrationPoints.Enqueue(new Point2D(0.1, 0.1));
          this.calibrationPoints.Enqueue(new Point2D(0.5, 0.1));
          this.calibrationPoints.Enqueue(new Point2D(0.9, 0.1));
          this.calibrationPoints.Enqueue(new Point2D(0.1, 0.5));
          this.calibrationPoints.Enqueue(new Point2D(0.5, 0.5));
          this.calibrationPoints.Enqueue(new Point2D(0.9, 0.5));
          this.calibrationPoints.Enqueue(new Point2D(0.1, 0.9));
          this.calibrationPoints.Enqueue(new Point2D(0.5, 0.9));
          this.calibrationPoints.Enqueue(new Point2D(0.9, 0.9));
          break;
      }

      if (this.settings.RandomizeCalibPointOrder)
      {
        var list = this.calibrationPoints.ToList<Point2D>();
        this.ShuffleList(list);
        this.calibrationPoints = new Queue<Point2D>(list);
      }
    }

    /// <summary>
    /// If user chose to randomize the calibration point order, shuffle the list.
    /// </summary>
    /// <param name="list">List with calibration point in order.</param>
    private void ShuffleList(List<Point2D> list)
    {
      Random rng = new Random();
      int n = list.Count;
      while (n > 1)
      {
        n--;
        int k = rng.Next(n + 1);
        Point2D value = list[k];
        list[k] = list[n];
        list[n] = value;
      }
    }

    /// <summary>
    /// Start procedure when calibration form is loaded.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The event arguments.</param>
    private void SmartEyeCalibrationFormLoaded(object sender, EventArgs e)
    {
      this.initTimer.Start();
      this.smartEyeCalibrationForm.ShowMessage("Initializing Calibration...\n\nPlease look at the screen,\nthen focus on the appearing point!", this.settings.CalibPointColor);
      Cursor.Current = Cursors.WaitCursor;
    }

    /// <summary>
    /// Check if gaze data is available, if so start the calibration.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The event arguments.</param>
    private void CheckStartCalibration(object sender, EventArgs e)
    {
      if (!this.InitializeTracking())
      {
        this.ResetConnection();
      }
      else
      {
        this.initTimer.Stop();
        Cursor.Current = Cursors.Default;
        this.smartEyeCalibrationForm.ClearMessage();
        this.StartCalibration();
      }
    }

    /// <summary>
    /// Get the good tracking count.
    /// </summary>
    /// <returns>Ready to start calibration</returns>
    private bool InitializeTracking()
    {
      return this.goodTrackingCount >= Document.ActiveDocument.ExperimentSettings.GazeSamplingRate;
    }

    /// <summary>
    /// Reset the event handler.
    /// </summary>
    private void ResetConnection()
    {
      if (this.client.UdpSocket != null)
      {
        this.client.UdpSocket.PacketReceived -= this.BaseClientOnPacketReceived;
        this.client.UdpSocket.PacketReceived += this.BaseClientOnPacketReceived;
      }
    }

    /// <summary>
    /// Start the calibration.
    /// </summary>
    private void StartCalibration()
    {
      this.GoToNextPoint();
    }

    /// <summary>
    /// Get the next calibration point.
    /// </summary>
    private void GoToNextPoint()
    {
      if (this.calibrationPoints.Count == 0)
      {
        this.StartProcessing();
        return;
      }

      var point = this.calibrationPoints.Dequeue();
      this.smartEyeCalibrationForm.DrawCalibrationPoint(point, this.settings.CalibPointColor, this.settings.CalibPointSize);
      this.goToNextPointTimer = new Timer() { Interval = 200 * (4 - this.settings.CalibPointSpeed) };
      this.goToNextPointTimer.Tick += (s, e) =>
      {
        this.CollectSamples(id, point);
      };
      this.goToNextPointTimer.Start();
    }

    /// <summary>
    /// Start collecting gaze samples for current calibration point.
    /// </summary>
    /// <param name="collectionId">Current ID.</param>
    /// <param name="screenPoint">The actual screen target.</param>
    private void CollectSamples(int collectionId, Point2D screenPoint)
    {
      this.goToNextPointTimer.Stop();
      this.state = CalibrationState.CollectingSamples;

      var x = screenPoint.X * PresentationScreen.GetPresentationResolution().Width;
      var y = screenPoint.Y * PresentationScreen.GetPresentationResolution().Height;

      try
      {
        this.client.RpcClient.StartCollectSamplesObject(collectionId, this.screenName, x, y, 0, this.calibTime * 2);
      }
      catch (Exception ex)
      {
        if (this.settings.SilentMode)
        {
          ExceptionMethods.HandleExceptionSilent(ex);
        }
        else
        {
          ExceptionMethods.HandleException(ex);
          this.HasShownMessage = true;
        }

        this.AbortCalibration();
        return;
      }

      this.collectSamplesStopwatch = new Stopwatch();
      this.collectSamplesStopwatch.Start();

      this.collectSamplesTimer.Start();
    }

    /// <summary>
    /// Stop collecting samples after calibration time over and move on to next point.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The event arguments.</param>
    private void CheckCollectSamples(object sender, EventArgs e)
    {
      if (this.collectSamplesStopwatch.Elapsed.TotalMilliseconds < this.calibTime)
      {
        return;
      }

      this.collectSamplesTimer.Stop();

      try
      {
        this.client.RpcClient.StopCollectSamples();
      }
      catch (Exception ex)
      {
        if (this.settings.SilentMode)
        {
          ExceptionMethods.HandleExceptionSilent(ex);
        }
        else
        {
          ExceptionMethods.HandleException(ex);
          this.HasShownMessage = true;
        }

        this.AbortCalibration();
        return;
      }

      this.smartEyeCalibrationForm.ClearCalibrationPoint();

      this.state = CalibrationState.Idle;
      this.id++;

      this.GoToNextPoint();
    }

    /// <summary>
    /// All gaze samples are collected, prepare asynchronous processing.
    /// </summary>
    private void StartProcessing()
    {
      this.state = CalibrationState.Calculating;

      this.smartEyeCalibrationForm.ShowMessage("Calculating Calibration...", this.settings.CalibPointColor);
      Cursor.Current = Cursors.WaitCursor;

      this.calibrationWorker = new BackgroundWorker();
      this.calibrationWorker.DoWork += this.PerformCalibration;
      this.calibrationWorker.RunWorkerCompleted += this.CalibrationCompleted;
      this.calibrationWorker.WorkerSupportsCancellation = true;
      ////this.calibrationWorker.WorkerReportsProgress = true;
      ////this.calibrationworker.ProgressChanged += CalibrationChanged;
      this.calibrationWorker.RunWorkerAsync();
    }

    /// <summary>
    /// The calibration processing method.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="doWorkEventArgs">The event arguments.</param>
    private void PerformCalibration(object sender, DoWorkEventArgs doWorkEventArgs)
    {
      Cursor.Current = Cursors.WaitCursor;

      if (this.client != null)
      {
        this.client.IsCalibrating = true;
      }

      JsonRpcAsyncResult asyncResult;
      try
      {
        asyncResult = this.client.RpcClient.CalibrateGazeAsync();
      }
      catch (Exception ex)
      {
        if (this.settings.SilentMode)
        {
          ExceptionMethods.HandleExceptionSilent(ex);
        }
        else
        {
          ExceptionMethods.HandleException(ex);
          this.HasShownMessage = true;
        }

        this.AbortCalibration();
        return;
      }

      if (!asyncResult.WaitHandle.WaitOne(25000))
      {
        // handle timeout error of 25 seconds
        string message = "Smart Eye Gaze Calibration calculation failed due to timeout error.";
        ExceptionMethods.ProcessErrorMessage(message);
        this.HasShownMessage = true;
        this.AbortCalibration();
        return;
      }

      if (asyncResult.Response == null || (asyncResult.Response != null && asyncResult.Response.ErrorCode != TrackerErrorId.Success))
      {
        var mes = asyncResult.Response.ErrorMessage;
        string message = "Smart Eye Gaze Calibration calculation failed with the following message: " +
         Environment.NewLine + mes;
        ExceptionMethods.ProcessErrorMessage(message);
        this.HasShownMessage = true;
        this.AbortCalibration();
        return;
      }

      doWorkEventArgs.Result = asyncResult;
    }

    /// <summary>
    /// The post-processing method to complete calibration.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The event arguments.</param>
    private void CalibrationCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
      if (e != null && e.Result != null && this.state != CalibrationState.Abort)
      {
        var res = e.Result as JsonRpcAsyncResult;

        if (res != null && res.Response.ErrorCode == TrackerErrorId.Success) // && res.Response.Result.HasValues)
        {
          this.state = CalibrationState.Done;
          this.GetCalibrationStatistics();
        }
      }

      if (this.client != null)
      {
        this.client.IsCalibrating = false;
      }

      this.smartEyeCalibrationForm.ShowMessage("The Calibration has been calculated successfully.", this.settings.CalibPointColor);
      Cursor.Current = Cursors.Default;
      this.client.UdpSocket.PacketReceived -= this.BaseClientOnPacketReceived;
      this.smartEyeCalibrationForm.BeginInvoke(new Action(() => this.smartEyeCalibrationForm.Dispose()));
    }

    /// <summary>
    /// Gets the calibration statistics.
    /// </summary>
    private void GetCalibrationStatistics()
    {
      if (this.client != null)
      {
        int i = 0;
        foreach (var result in this.calibrationResult)
        {
          Trace.WriteLine("SessionManager - Retrieving Collection (" + i + ")");

          double[] accuracy;
          double[] stdDev;
          double[] errorsXL;
          double[] errorsXR;
          double[] errorsYL;
          double[] errorsYR;

          try
          {
            this.client.RpcClient.RetrieveTargetStatistics(
              i,
              out stdDev,
              out accuracy,
              out errorsXL,
              out errorsYL,
              out errorsXR,
              out errorsYR);
          }
          catch (Exception ex)
          {
            if (this.settings.SilentMode)
            {
              ExceptionMethods.HandleExceptionSilent(ex);
            }
            else
            {
              ExceptionMethods.HandleException(ex);
              this.HasShownMessage = true;
            }

            return;
          }

          if (accuracy == null ||
              stdDev == null ||
              errorsXL == null ||
              errorsXR == null ||
              errorsYL == null ||
              errorsYR == null)
          {
            return;
          }

          result.Accuracy = accuracy;
          result.Precision = stdDev;

          result.SamplesLeft = new List<Point2D>();
          for (int p = 0; p < errorsXL.Length; p++)
          {
            result.SamplesLeft.Add(new Point2D(errorsXL[p], errorsYL[p]));
          }

          result.SamplesRight = new List<Point2D>();
          for (int p = 0; p < errorsXR.Length; p++)
          {
            result.SamplesRight.Add(new Point2D(errorsXR[p], errorsYR[p]));
          }

          i++;
        }
      }
    }

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler                                                              //
    ///////////////////////////////////////////////////////////////////////////////
    #region EVENTS

    /// <summary>
    /// Packet received handler to check for good tracking count.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="packetReceivedEventArgs">The event arguments.</param>
    private void BaseClientOnPacketReceived(object sender, PacketReceivedEventArgs packetReceivedEventArgs)
    {
      var packet = packetReceivedEventArgs.Packet;

      NetworkData data;
      if (!packet.TryGetValue(TrackerDataId.GazeDirectionQ, out data))
      {
        return;
      }

      var q = data.GetValue<double>();
      if (q > 0.2)
      {
        this.goodTrackingCount++;
      }
      else if (this.goodTrackingCount > 0)
      {
        this.goodTrackingCount--;
      }
    }

    /// <summary>
    /// Method is called when a property change is fired on the Smart Eye client
    /// </summary>
    /// <param name="sender">The Sender.</param>
    /// <param name="e">Property changed argument.</param>
    private void SmartEyeClientPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      if (e != null && e.PropertyName == "UdpIsConnected")
      {
        if (!this.client.UdpIsConnected)
        {
          // client will try to reconnect, here only calibration specific code
        }
      }
    }

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler for UI, Menu, Buttons, Toolbars etc.                         //
    ///////////////////////////////////////////////////////////////////////////////
    #region WINDOWSEVENTHANDLER

    /// <summary>
    /// Process key down events.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The event arguments.</param>
    private void SmartEyeCalibrationFormKeyDown(object sender, KeyEventArgs e)
    {
      if (e != null && e.KeyCode == Keys.Escape)
      {
        this.AbortCalibration();
      }
    }

    #endregion WINDOWSEVENTHANDLER

    #endregion EVENTS

    #endregion METHODS
  }
}
