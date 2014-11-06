// <copyright file="SmartEyeTracker.cs" company="Smart Eye AB">
// ******************************************************
// OGAMA - open gaze and mouse analyzer 
// Copyright (C) 2014 Dr. Adrian Voßkühler  
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
  using System.Drawing.Imaging;
  using System.Globalization;
  using System.IO;
  using System.Linq;
  using System.Net.Sockets;
  using System.Runtime.InteropServices;
  using System.Text;
  using System.Text.RegularExpressions;
  using System.Threading;
  using System.Windows.Forms;
  using System.Windows.Media;
  using System.Windows.Media.Imaging;
  using System.Xml.Serialization;
  using Microsoft.Win32;
  using Ogama.ExceptionHandling;
  using Ogama.Modules.Common.CustomEventArgs;
  using Ogama.Modules.Common.Tools;
  using Ogama.Modules.Recording.TrackerBase;
  using SmartEye.Tracker;

  /// <summary>
  ///   This class implements the <see cref="TrackerWithStatusControls" /> class
  ///   to represent an OGAMA known eye tracker.
  ///   It encapsulates a Smart Eye http://www.smarteye.se Aurora eye tracker.
  /// </summary>
  public class SmartEyeTracker : TrackerWithStatusControls
  {
    ///////////////////////////////////////////////////////////////////////////////
    // Defining Variables, Enumerations, Events                                  //
    ///////////////////////////////////////////////////////////////////////////////
    #region FIELDS

    /// <summary>
    /// Saves the Smart Eye settings.
    /// </summary>
    private SmartEyeSetting smartEyeSettings;

    /// <summary>
    /// The Smart Eye client which handles all communication.
    /// </summary>
    private SmartEyeClient smartEyeClient;

    /// <summary>
    /// The Smart Eye calibration class.
    /// </summary>
    private SmartEyeCalibrationRunner smartEyeCalibration;

    /// <summary>
    /// The thread for getting live image updates.
    /// </summary>
    private Thread liveImageThread;

    /// <summary>
    /// The flag to stop the thread for getting live image updates.
    /// </summary>
    private bool stopliveImageThread;

    /// <summary>
    /// The track status dialog.
    /// </summary>
    private SmartEyeTrackStatus dlgTrackStatus;

    /// <summary>
    /// The track status control.
    /// </summary>
    private SmartEyeTrackStatusControl smartEyeTrackStatus;

    /// <summary>
    /// The calibration result panel.
    /// </summary>
    private SmartEyeCalibrationResultPanel smartEyeCalibPlot;

    #endregion FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the <see cref="SmartEyeTracker"/> class.
    /// </summary>
    /// <param name="owningRecordModule">The parent</param>
    /// <param name="trackerTrackerControlsContainer">The container</param>
    /// <param name="trackerTrackStatusPanel">The track status panel</param>
    /// <param name="trackerCalibrationResultPanel">The calibration result panel</param>
    /// <param name="trackerShowOnSecondaryScreenButton">The show on secondary screen button</param>
    /// <param name="trackerAcceptButton">The calibration acept button</param>
    /// <param name="trackerRecalibrateButton">The recalibrate button</param>
    /// <param name="trackerConnectButton">The connect button</param>
    /// <param name="trackerSubjectButton">The subject button</param>
    /// <param name="trackerCalibrateButton">The calibrate button</param>
    /// <param name="trackerRecordButton">The record button</param>
    /// <param name="trackerSubjectNameTextBox">The subject name textbox</param>
    public SmartEyeTracker(
      RecordModule owningRecordModule,
      SplitContainer trackerTrackerControlsContainer,
      Panel trackerTrackStatusPanel,
      Panel trackerCalibrationResultPanel,
      Button trackerShowOnSecondaryScreenButton,
      Button trackerAcceptButton,
      Button trackerRecalibrateButton,
      Button trackerConnectButton,
      Button trackerSubjectButton,
      Button trackerCalibrateButton,
      Button trackerRecordButton,
      TextBox trackerSubjectNameTextBox)
      : base(
        owningRecordModule,
        trackerTrackerControlsContainer,
        trackerTrackStatusPanel,
        trackerCalibrationResultPanel,
        trackerShowOnSecondaryScreenButton,
        trackerAcceptButton,
        trackerRecalibrateButton,
        trackerConnectButton,
        trackerSubjectButton,
        trackerCalibrateButton,
        trackerRecordButton,
        trackerSubjectNameTextBox,
        Properties.Settings.Default.EyeTrackerSettingsPath + "SmartEyeSetting.xml")
    {
      // Call the initialize methods of derived classes
      this.Initialize();

      this.smartEyeTrackStatus.PropertyChanged += this.SmartEyeTrackStatusPropertyChanged;
    }

    #endregion CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets a value indicating whether the tracker is connected
    /// </summary>
    public override bool IsConnected
    {
      get
      {
        if (this.smartEyeClient == null || this.smartEyeClient.RpcClient == null)
        {
          return false;
        }

        return this.smartEyeClient.RpcIsConnected;
      }
    }

    #endregion PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Public methods                                                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region PUBLICMETHODS

    /// <summary>
    /// Checks if a Smart Eye tracker is available in the system.
    /// </summary>
    /// <param name="errorMessage">
    ///   Out. A <see cref="string"/> with an error message.
    /// </param>
    /// <returns>
    /// <strong>True</strong>, if Smart Eye tracker 
    ///   is available in the system, otherwise <strong>false</strong>
    /// </returns>
    public static TrackerStatus IsAvailable(out string errorMessage)
    {
      var p = Process.GetProcessesByName("eye_tracker_core");
      if (p.Length > 0)
      {
        errorMessage = "The Smart Eye Aurora eye tracker is running on the system.";
        return TrackerStatus.Available;
      }
      else
      {
        var smartEyeVersion = Registry.GetValue(@"HKEY_CURRENT_USER\Software\Smart Eye AB\Shared", "EyeTrackerCoreVersion", null);
        if (smartEyeVersion != null)
        {
          var smartEyeTrackingPath = Registry.GetValue(
            string.Format(@"HKEY_CURRENT_USER\Software\Smart Eye AB\Eye tracker core {0}\DefaultPaths", smartEyeVersion.ToString()),
            "ProgramDirectory",
            null);
          if (smartEyeTrackingPath != null)
          {
            errorMessage = "The Smart Eye Aurora eye tracker was found on the system, but it is not running. Please start up Aurora before recording with Ogama!";
            return TrackerStatus.Available;
          }
        }
      }

      errorMessage = "No Smart Eye Aurora eye tracker has been found on the system. Make sure you have installed Aurora correctly.";
      return TrackerStatus.Undetermined;
    }

    /// <summary>
    /// Serializes the <see cref="SmartEyeSetting"/> into the given file in a xml structure.
    /// </summary>
    /// <param name="settings">The <see cref="SmartEyeSetting"/> object to serialize.</param>
    /// <param name="filePath">Full file path to the xml settings file.</param>
    public void SerializeSettings(SmartEyeSetting settings, string filePath)
    {
      // Create an instance of the XmlSerializer class;
      // specify the type of object to serialize 
      var serializer = new XmlSerializer(typeof(SmartEyeSetting));

      // Serialize the SmartEyeSetting, and close the TextWriter.
      try
      {
        TextWriter writer = new StreamWriter(filePath, false);
        serializer.Serialize(writer, settings);
        writer.Close();
      }
      catch (Exception ex)
      {
        string message = "Serialization of SmartEyeSettings failed with the following message: " +
          Environment.NewLine + ex.Message;
        if (this.smartEyeSettings.SilentMode)
        {
          ExceptionMethods.HandleExceptionSilent(ex);
        }
        else
        {
          ExceptionMethods.HandleException(ex);
        }
      }
    }

    ///////////////////////////////////////////////////////////////////////////////
    // Inherited methods                                                         //
    ///////////////////////////////////////////////////////////////////////////////
    #region OVERRIDES

    /// <summary>
    /// An implementation of this method should show a hardware 
    /// system specific dialog to change its settings like
    /// sampling rate or connection properties. It should also
    /// provide a xml serialization possibility of the settings,
    /// so that the user can store and backup system settings in
    /// a separate file. These settings should be implemented in
    /// a separate class and are stored in a special place of
    /// Ogama's directory structure.
    /// </summary>
    /// <remarks>Please have a look at the existing implementation
    /// of the Smart Eye system in the namespace SmartEye.</remarks>
    public override void ChangeSettings()
    {
      var dlg = new SmartEyeSettingsDialog { SmartEyeSettings = this.smartEyeSettings };
      switch (dlg.ShowDialog())
      {
        case DialogResult.OK:
          this.smartEyeSettings = dlg.SmartEyeSettings;
          dlg.UpdateSmartEyeSettings();
          this.SerializeSettings(this.smartEyeSettings, this.SettingsFile);
          break;
      }
    }

    /// <summary>
    /// An implementation of this method should do all 
    /// connection routines for the specific hardware, so that the
    /// system is ready for calibration.
    /// </summary>
    /// <returns><strong>True</strong> if successful connected to tracker,
    /// otherwise <strong>false</strong>.</returns>
    public override bool Connect()
    {
      if (!this.ValidateAddresses(this.smartEyeSettings))
      {
        return false;
      }

      if (this.smartEyeClient == null)
      {
        this.smartEyeClient = new SmartEyeClient(this.smartEyeSettings);
      }
      else
      {
        this.smartEyeClient.CreateRPC();
        this.smartEyeClient.CreateUDP(this.smartEyeSettings.SmartEyeServerAddress, this.smartEyeSettings.OgamaPort);
      }

      this.smartEyeClient.PropertyChanged += this.SmartEyeClientPropertyChanged;
      this.smartEyeClient.GazeDataAvailable += this.SmartEyeGazeDataAvailable;

      if (this.smartEyeClient.MajorVersion >= 1 && this.smartEyeClient.MinorVersion >= 1)
      {
        this.StartLiveImageThread();
      }

      return this.smartEyeClient.RpcIsConnected;
    }

    /// <summary>
    /// An implementation of this method should do the calibration
    /// for the specific hardware, so that the
    /// system is ready for recording.
    /// </summary>
    /// <param name="isRecalibrating"><strong>True</strong> if calibration
    /// is in recalibration mode, indicating to renew only a few points,
    /// otherwise <strong>false</strong>.</param>
    /// <returns><strong>True</strong> if successful calibrated,
    /// otherwise <strong>false</strong>.</returns>
    /// <remarks>Implementations do not have to use the recalibrating 
    /// parameter.</remarks>
    public override bool Calibrate(bool isRecalibrating)
    {
      try
      {
        int? auroraConfigured = (int?)Registry.GetValue(@"HKEY_CURRENT_USER\Software\Smart Eye AB\Aurora Configuration Tool", "isConfigured", null);
        if (auroraConfigured == null || auroraConfigured == 0)
        {
          var message = "Calibration cannot be started as the Aurora Hardware has not yet been linked to a screen." +
            Environment.NewLine + "Please set up the system correctly in the Aurora Configuration Tool software before using it in OGAMA.";
          ExceptionMethods.ProcessMessage(
            "Aurora not configured via Aurora Configuration Tool",
            message);
          return false;
        }
      }
      catch (Exception e)
      {
        if (this.smartEyeSettings.SilentMode)
        {
          ExceptionMethods.HandleExceptionSilent(e);
        }
        else
        {
          ExceptionMethods.HandleException(e);
        }
      }

      if (!this.IsTrackerTracking("Calibration"))
      {
        return false;
      }

      // Should hide TrackStatusDlg
      if (this.dlgTrackStatus != null)
      {
        this.ShowOnSecondaryScreenButton.BackColor = System.Drawing.Color.Transparent;
        this.ShowOnSecondaryScreenButton.Text = "Show on presentation screen";
        this.dlgTrackStatus.Close();
      }

      this.ShowTrackStatus();

      if (this.liveImageThread.IsAlive)
      {
        this.stopliveImageThread = true;
      }

      try
      {
        this.smartEyeCalibration = new SmartEyeCalibrationRunner(this.smartEyeClient, this.smartEyeSettings);
      }
      catch (Exception ex)
      {
        if (this.smartEyeSettings.SilentMode)
        {
          ExceptionMethods.HandleExceptionSilent(ex);
        }
        else
        {
          ExceptionMethods.HandleException(ex);
        }

        this.StartLiveImageThread();

        return false;
      }

      try
      {
        // Start a new calibration procedure
        List<CalibrationResult> calibResult = this.smartEyeCalibration.RunCalibration();

        // Show a calibration plot if everything went OK
        if (calibResult != null)
        {
          double degreeToPixel = 0;
          string wm;

          if (this.smartEyeClient != null && this.smartEyeClient.RpcClient != null)
          {
            try
            {
              this.smartEyeClient.RpcClient.GetWorldModel(out wm);
              var wmClean = Regex.Replace(wm, @"[ \t\r\f]", string.Empty);
              var h = wmClean.IndexOf("Screen:{");

              if (h >= 0)
              {
                var wmScreenSub = wmClean.Substring(h);
                var i = wmScreenSub.IndexOf("size=");
                if (i >= 0)
                {
                  var wmSub = wmScreenSub.Substring(i + 5);
                  var j = wmSub.IndexOf(",");
                  var physWidthString = wmSub.Substring(0, j);
                  var physWidth = Convert.ToDouble(physWidthString, CultureInfo.InvariantCulture);

                  var resWidth = PresentationScreen.GetPresentationResolution().Width;

                  degreeToPixel = Math.Tan(Math.PI / 180) * 0.65 * resWidth / physWidth;
                }
              }
            }
            catch (Exception ex)
            {
              if (this.smartEyeSettings.SilentMode)
              {
                ExceptionMethods.HandleExceptionSilent(ex);
              }
              else
              {
                ExceptionMethods.HandleException(ex);
              }
            }
          }

          if (degreeToPixel != 0)
          {
            this.smartEyeCalibPlot.Initialize(calibResult, degreeToPixel);
          }
          else
          {
            this.smartEyeCalibPlot.Initialize(calibResult);
          }

          this.ShowCalibPlot();
        }
        else
        {
          if (!this.smartEyeCalibration.HasShownMessage)
          {
            ExceptionMethods.ProcessMessage("Calibration failed", "Not enough data to create a calibration (or calibration aborted).");
          }

          this.StartLiveImageThread();

          return false;
        }
      }
      catch (Exception ee)
      {
        if (this.smartEyeSettings.SilentMode)
        {
          ExceptionMethods.HandleExceptionSilent(ee);
        }
        else
        {
          ExceptionMethods.HandleException(ee);
        }

        this.StartLiveImageThread();

        return false;
      }

      return true;
    }

    /// <summary>
    /// An implementation of this method should start the recording
    /// for the specific hardware, so that the
    /// system sends GazeDataChanged events.
    /// </summary>
    /// <remarks>
    /// Is empty as other methods make sure recording is possible beforehand
    /// </remarks>
    public override void Record()
    {
    }

    /// <summary>
    /// An implementation of this method should stop the recording
    /// for the specific hardware.
    /// </summary>
    public override void Stop()
    {
      if (this.smartEyeClient == null || this.smartEyeClient.RpcClient == null)
      {
        return;
      }

      if (this.smartEyeClient.RpcIsConnected)
      {
        try
        {
          this.smartEyeClient.RpcClient.StopTracking();
        }
        catch (Exception ex)
        {
          if (this.smartEyeSettings.SilentMode)
          {
            ExceptionMethods.HandleExceptionSilent(ex);
          }
          else
          {
            ExceptionMethods.HandleException(ex);
          }

          this.IsRpcConnected();
          return;
        }
      }
    }

    /// <summary>
    /// Overridden. Dispose the <see cref="SmartEyeTracker"/> if applicable
    /// by a call to <see cref="ITracker.CleanUp()"/> and 
    /// removing the connected event handlers.
    /// </summary>
    public override void Dispose()
    {
      this.Stop();

      if (this.smartEyeTrackStatus != null)
      {
        this.smartEyeTrackStatus.Dispose();
      }

      if (this.smartEyeCalibPlot != null)
      {
        this.smartEyeCalibPlot.Dispose();
      }

      if (this.dlgTrackStatus != null)
      {
        this.dlgTrackStatus.Dispose();
      }

      base.Dispose();
    }

    /// <summary>
    /// Do a clean up for Smart Eye Tracker, so that the
    /// system is ready for shut down.
    /// </summary>
    public override void CleanUp()
    {
      Cursor.Current = Cursors.WaitCursor;

      try
      {
        if (this.liveImageThread != null && this.liveImageThread.IsAlive)
        {
          this.stopliveImageThread = true;
        }

        this.smartEyeTrackStatus.OnGazeData(new SmartEyeGazeData());

        if (this.smartEyeClient != null)
        {
          this.smartEyeClient.PropertyChanged -= this.SmartEyeClientPropertyChanged;
          this.smartEyeClient.GazeDataAvailable -= this.SmartEyeGazeDataAvailable;
          this.smartEyeClient.Dispose();
          this.smartEyeClient = null;
        }
      }
      catch (Exception ex)
      {
        if (this.smartEyeSettings.SilentMode)
        {
          ExceptionMethods.HandleExceptionSilent(ex);
        }
        else
        {
          ExceptionMethods.HandleException(ex);
        }
      }

      base.CleanUp();

      Cursor.Current = Cursors.Default;
    }

    /// <summary>
    /// Sets up calibration procedure and the tracking client
    /// and wires the events. Reads settings from file.
    /// </summary>
    protected override sealed void Initialize()
    {
      // Load Smart Eye tracker settings.
      if (File.Exists(this.SettingsFile))
      {
        this.smartEyeSettings = this.DeserializeSettings(this.SettingsFile);
      }
      else
      {
        this.smartEyeSettings = new SmartEyeSetting();
        this.SerializeSettings(this.smartEyeSettings, this.SettingsFile);
      }

      base.Initialize();
    }

    /// <summary>
    /// This method initializes the designer components for the
    ///   calibration plot
    /// </summary>
    protected override void InitializeStatusControls()
    {
      if (this.smartEyeTrackStatus == null && this.smartEyeCalibPlot == null)
      {
        this.smartEyeTrackStatus = new SmartEyeTrackStatusControl();
        this.smartEyeCalibPlot = new SmartEyeCalibrationResultPanel();

        // SmartEyeTrackStatus
        this.smartEyeTrackStatus.Dock = DockStyle.Fill;
        this.smartEyeTrackStatus.Enabled = true;
        this.smartEyeTrackStatus.Location = new Point(0, 0);
        this.smartEyeTrackStatus.Name = "smartEyeTrackStatus";
        this.smartEyeTrackStatus.Size = new Size(190, 54);
        this.smartEyeTrackStatus.TabIndex = 0;

        // SmartEyeCalibPlot
        this.smartEyeCalibPlot.Dock = DockStyle.Fill;
        this.smartEyeCalibPlot.Enabled = true;
        this.smartEyeCalibPlot.Location = new Point(0, 0);
        this.smartEyeCalibPlot.Name = "smartEyeCalibPlot";
        this.smartEyeCalibPlot.Size = new Size(190, 54);
        this.smartEyeCalibPlot.TabIndex = 0;

        try
        {
          this.TrackStatusPanel.Controls.Add(this.smartEyeTrackStatus);
          this.CalibrationResultPanel.Controls.Add(this.smartEyeCalibPlot);

          this.ShowCalibPlot();
          this.ShowTrackStatus();
        }
        catch (COMException)
        {
          this.TrackStatusPanel.Controls.Clear();
          this.CalibrationResultPanel.Controls.Clear();
          throw;
        }
      }
    }

    /// <summary>
    /// Extend connect button click to always show tracking status and hide calibration panel if it's shown
    /// </summary>
    /// <param name="sender">The sender</param>
    /// <param name="e">The event</param>
    protected override void BtnConnectClick(object sender, EventArgs e)
    {
      base.BtnConnectClick(sender, e);

      this.ShowTrackStatus();
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> "showOnPresentationScreenButton.
    /// Implementations should show the track status object
    /// on the presentation screen.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/>.</param>
    protected override void BtnShowOnPresentationScreenClick(object sender, EventArgs e)
    {
      if (this.ShowOnSecondaryScreenButton.Text.Contains("Show"))
      {
        // Should show TrackStatusDlg
        if (this.dlgTrackStatus != null)
        {
          this.dlgTrackStatus.Dispose();
        }

        this.dlgTrackStatus = new SmartEyeTrackStatus();

        Rectangle presentationBounds = PresentationScreen.GetPresentationWorkingArea();

        this.dlgTrackStatus.Location = new Point(
                            presentationBounds.Left + presentationBounds.Width / 2 - this.dlgTrackStatus.Width / 2,
                            presentationBounds.Top + presentationBounds.Height / 2 - this.dlgTrackStatus.Height / 2);

        // Dialog will be disposed when connection failed.
        if (!this.dlgTrackStatus.IsDisposed)
        {
          this.ShowOnSecondaryScreenButton.Text = "Hide from presentation screen";
          this.ShowOnSecondaryScreenButton.BackColor = System.Drawing.Color.Red;
          this.dlgTrackStatus.Show();
        }

        this.dlgTrackStatus.FormClosed += (o, e2) =>
        {
          this.ShowOnSecondaryScreenButton.BackColor = System.Drawing.Color.Transparent;
          this.ShowOnSecondaryScreenButton.Text = "Show on presentation screen";
        };
      }
      else
      {
        // Should hide TrackStatusDlg
        if (this.dlgTrackStatus != null)
        {
          this.ShowOnSecondaryScreenButton.BackColor = System.Drawing.Color.Transparent;
          this.ShowOnSecondaryScreenButton.Text = "Show on presentation screen";
          this.dlgTrackStatus.Close();
        }
      }
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> subjectButton.
    /// Calls the OpenSubjectDialog(ref string) method
    /// to specify the new subject and parameters and 
    /// enables calibration button.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/>.</param>
    protected override void BtnSubjectNameClick(object sender, EventArgs e)
    {
      base.BtnSubjectNameClick(sender, e);

      if (base.CalibrateButton.Enabled)
      {
        if (this.smartEyeClient == null || this.smartEyeClient.RpcClient == null)
        {
          return;
        }

        // restart tracking before calibration
        try
        {
          this.smartEyeClient.RpcClient.StopTracking();
          this.smartEyeClient.RpcClient.ClearProfile();
          this.smartEyeClient.RpcClient.StartTracking();
        }
        catch (Exception ex)
        {
          if (this.smartEyeSettings.SilentMode)
          {
            ExceptionMethods.HandleExceptionSilent(ex);
          }
          else
          {
            ExceptionMethods.HandleException(ex);
          }

          base.CalibrateButton.Enabled = false;

          this.IsRpcConnected();
        }

        // recalibrate before restarting recording!
        base.RecordButton.Enabled = false;
      }

      this.ShowTrackStatus();
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> acceptButton.
    /// Accepts the calibration by enabling the record button
    /// and hiding the calibration plot.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/>.</param>
    protected override void BtnAcceptCalibrationClick(object sender, EventArgs e)
    {
      if (this.smartEyeClient == null || this.smartEyeClient.RpcClient == null)
      {
        return;
      }

      try
      {
        this.smartEyeClient.RpcClient.ApplyGazeCalibration();
      }
      catch (Exception ex)
      {
        if (this.smartEyeSettings.SilentMode)
        {
          ExceptionMethods.HandleExceptionSilent(ex);
        }
        else
        {
          ExceptionMethods.HandleException(ex);
        }

        this.IsRpcConnected();
        return;
      }

      base.BtnAcceptCalibrationClick(sender, e);

      this.StartLiveImageThread();
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    ///   <see cref="Button"/> recordButton.
    /// </summary>
    /// <param name="sender">
    /// Source of the event.
    /// </param>
    /// <param name="e">
    /// An empty <see cref="EventArgs"/>.
    /// </param>
    protected override void BtnRecordClick(object sender, EventArgs e)
    {
      if (!this.IsTrackerTracking("Recording"))
      {
        return;
      }

      base.BtnRecordClick(sender, e);
    }

    /// <summary>
    /// Overridden.
    /// Check visibility of the track status window before starting to record.
    /// </summary>
    protected override void PrepareRecording()
    {
      // Hide Trackstatus on presentation screen
      if (this.dlgTrackStatus != null && this.dlgTrackStatus.Visible)
      {
        // Hide TrackStatusDlg
        this.ShowOnSecondaryScreenButton.BackColor = System.Drawing.Color.Transparent;
        this.ShowOnSecondaryScreenButton.Text = "Show on presentation screen";
        this.dlgTrackStatus.Close();
      }
    }

    /// <summary>
    /// Raised when the recorder has finished a recording.
    /// Resets the button states.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/>.</param>
    protected override void RecordModuleNewRecordingFinished(object sender, EventArgs e)
    {
      base.RecordModuleNewRecordingFinished(sender, e);

      string subjectName = this.Subject.SubjectName;
      if (!Queries.ValidateSubjectName(ref subjectName, true))
      {
        // new subject has to be created before going on with calibration
        base.CalibrateButton.Enabled = false;
      }
    }

    #endregion OVERRIDES

    #endregion PUBLICMETHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Methods for doing main class job                                          //
    ///////////////////////////////////////////////////////////////////////////////
    #region PRIVATEMETHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler                                                              //
    ///////////////////////////////////////////////////////////////////////////////
    #region EVENTHANDLER

    /// <summary>
    /// Method is called when a property change is fired on the Smart Eye client
    /// </summary>
    /// <param name="sender">The Sender.</param>
    /// <param name="e">Property changed argument.</param>
    private void SmartEyeClientPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      if (e != null && e.PropertyName == "UdpIsConnected")
      {
        if (!this.smartEyeClient.UdpIsConnected)
        {
          this.smartEyeTrackStatus.OnGazeData(new SmartEyeGazeData());
        }
      }
    }

    /// <summary>
    /// Is called when new gaze data is available, updates track status and raises OnGazeDataChanged
    /// </summary>
    /// <param name="sender">The Sender.</param>
    /// <param name="e">Gaze data.</param>
    private void SmartEyeGazeDataAvailable(object sender, GazeDataReceivedEventArgs e)
    {
      // Send the gaze data to the track status control.
      SmartEyeGazeData gd = e.Gazedata;
      this.smartEyeTrackStatus.OnGazeData(gd);
      if (this.dlgTrackStatus != null && this.dlgTrackStatus.Visible)
      {
        this.dlgTrackStatus.Update(gd);
      }

      GazeData newGD = new GazeData();
      newGD.Time = gd.Time;

      if (gd.HeadQuality >= 1 && gd.GazeQuality > this.smartEyeSettings.QualityThreshold)   // cut off bad quality data
      {
        newGD.GazePosX = gd.GazePosX;
        newGD.GazePosY = gd.GazePosY;
        newGD.PupilDiaX = gd.PupilDiaX;
        newGD.PupilDiaY = gd.PupilDiaY;
      }
      else
      {
        newGD.GazePosX = null;
        newGD.GazePosY = null;
        newGD.PupilDiaX = null;
        newGD.PupilDiaY = null;
      }

      this.OnGazeDataChanged(new GazeDataChangedEventArgs(newGD));
    }

    /// <summary>
    /// Property changed on track status panel
    /// </summary>
    /// <param name="sender">The sender</param>
    /// <param name="e">The event</param>
    private void SmartEyeTrackStatusPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      if (e.PropertyName == "ToggleLiveImageDisplay")
      {
        if (this.liveImageThread != null)
        {
          if (this.liveImageThread.IsAlive)
          {
            this.stopliveImageThread = true;
          }
          else
          {
            if (this.IsRpcConnected())
            {
              this.StartLiveImageThread();
            }
          }
        }
      }
    }

    /// <summary>
    /// Start the live image thread
    /// </summary>
    private void StartLiveImageThread()
    {
      if (this.liveImageThread == null || !this.liveImageThread.IsAlive)
      {
        this.liveImageThread = new Thread(this.GetLiveImage);
        this.liveImageThread.Name = "LiveImageThread";
        if (this.smartEyeClient.RpcIsConnected)
        {
          this.liveImageThread.Start();
          this.stopliveImageThread = false;
        }
      }
    }

    /// <summary>
    /// Gets a live image from the Smart Eye client on each liveImageUpdateTimer-tick
    /// </summary>
    private void GetLiveImage()
    {
      while (!this.stopliveImageThread)
      {
        string base64String;
        int width, height, stride;
        int index = 0;

        if (this.smartEyeClient == null || this.smartEyeClient.RpcClient == null)
        {
          return;
        }

        try
        {
          this.smartEyeClient.RpcClient.GetCameraImage(index, out base64String, out width, out height, out stride);

          byte[] binaryData = Convert.FromBase64String(base64String);

          Bitmap bitmap = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format8bppIndexed);

          ColorPalette ncp = bitmap.Palette;
          for (int i = 0; i < 256; i++)
          {
            ncp.Entries[i] = System.Drawing.Color.FromArgb(255, i, i, i);
          }

          bitmap.Palette = ncp;

          BitmapData bitmapData = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, bitmap.PixelFormat);
          IntPtr intPointer = bitmapData.Scan0;
          Marshal.Copy(binaryData, 0, intPointer, binaryData.Length);
          bitmap.UnlockBits(bitmapData);

          this.smartEyeTrackStatus.OnLiveImage(bitmap);

          if (this.dlgTrackStatus != null && this.dlgTrackStatus.Visible)
          {
            this.dlgTrackStatus.UpdateLiveImage(bitmap);
          }
        }
        catch (Exception ex)
        {
          if (this.smartEyeSettings.SilentMode)
          {
            ExceptionMethods.HandleExceptionSilent(ex);
          }
          else
          {
            ExceptionMethods.HandleException(ex);
          }

          this.IsRpcConnected();
          Thread.Sleep(200);
        }

        Thread.Sleep(100);
      }

      if (this.stopliveImageThread)
      {
        this.smartEyeTrackStatus.OnLiveImage(null);
      }
    }

    #endregion EVENTHANDLER

    /// <summary>
    /// Deserializes the <see cref="SmartEyeSetting"/> from the given xml file.
    /// </summary>
    /// <param name="filePath">Full file path to the xml settings file.</param>
    /// <returns>A <see cref="SmartEyeSetting"/> object.</returns>
    private SmartEyeSetting DeserializeSettings(string filePath)
    {
      var settings = new SmartEyeSetting();

      // Create an instance of the XmlSerializer class;
      // specify the type of object to be deserialized 
      var serializer = new XmlSerializer(typeof(SmartEyeSetting));

      // * If the XML document has been altered with unknown 
      // nodes or attributes, handle them with the 
      // UnknownNode and UnknownAttribute events.*/
      serializer.UnknownNode += this.SerializerUnknownNode;
      serializer.UnknownAttribute += this.SerializerUnknownAttribute;

      try
      {
        // A FileStream is needed to read the XML document.
        var fs = new FileStream(filePath, FileMode.Open);

        // Use the Deserialize method to restore the object's state with
        // data from the XML document. 
        settings = (SmartEyeSetting)serializer.Deserialize(fs);
        fs.Close();
      }
      catch (Exception ex)
      {
        if (this.smartEyeSettings.SilentMode)
        {
          ExceptionMethods.HandleExceptionSilent(ex);
        }
        else
        {
          ExceptionMethods.HandleException(ex);
        }
      }

      this.ValidateAddresses(settings);

      return settings;
    }

    /// <summary>
    /// Validate IP and port addresses of the deserialized settings.
    /// </summary>
    /// <param name="settings">Deserialized settings.</param>
    /// <returns>True if all addresses are valid</returns>
    private bool ValidateAddresses(SmartEyeSetting settings)
    {
      return this.ValidateIpAddress(settings.SmartEyeServerAddress) &&
             this.ValidatePortAddress(settings.SmartEyeRPCPort) &&
             this.ValidatePortAddress(settings.OgamaPort);
    }

    /// <summary>
    /// Check if the port address is in a valid number range.
    /// </summary>
    /// <param name="port">The port value.</param>
    /// <returns>True if port address valid</returns>
    private bool ValidatePortAddress(int port)
    {
      if (port < 0 || port > 65535)
      {
        ExceptionMethods.ProcessErrorMessage("The saved settings contained an invalid port address, which must be a numerical value between 0 and 65535."
           + Environment.NewLine + "Please check your tracker settings and correct the error to make sure a connection can be established.");
        return false;
      }

      return true;
    }

    /// <summary>
    /// Validate the Smart Eye IP address.
    /// </summary>
    /// <param name="ip">The IP value.</param>
    /// <returns>True if IP address valid</returns>
    private bool ValidateIpAddress(string ip)
    {
      if (!string.IsNullOrEmpty(ip))
      {
        var parts = ip.Split('.');
        if (parts.Length == 4)
        {
          foreach (var p in parts)
          {
            int intPart;
            if (int.TryParse(p, NumberStyles.Integer, CultureInfo.CurrentCulture.NumberFormat, out intPart))
            {
              if (intPart >= 0 && intPart <= 255)
              {
                return true;
              }
            }
          }
        }
      }

      ExceptionMethods.ProcessErrorMessage("The saved settings did not contain a valid Smart Eye IP address."
         + Environment.NewLine + "Please check your tracker settings and correct the error to make sure a connection can be established.");
      return false;
    }

    /// <summary>
    /// Checks if the UDP socket of the Smart Eye client is connected.
    /// </summary>
    /// <returns>A <see cref="bool"/> with the connection state.</returns>
    private bool IsUdpConnected()
    {
      if (!this.smartEyeClient.UdpIsConnected)
      {
        this.smartEyeClient.ConnectUDP();
        Thread.Sleep(50);
      }

      return this.smartEyeClient.UdpIsConnected;
    }

    /// <summary>
    /// Checks if the RPC client of the Smart Eye client is connected.
    /// </summary>
    /// <returns>A <see cref="bool"/> with the connection state.</returns>
    private bool IsRpcConnected()
    {
      if (this.smartEyeClient == null || this.smartEyeClient.RpcClient == null)
      {
        return false;
      }

      if (!this.smartEyeClient.PingRPC())
      {
        this.smartEyeClient.ConnectRPC();
        Thread.Sleep(50);
      }

      return this.smartEyeClient.RpcIsConnected;
    }

    /// <summary>
    /// Check if the Smart Eye tracker is in a tracking state, or tracking can be started.
    /// </summary>
    /// <param name="featureName">Feature who called method to put in possible error message.</param>
    /// <returns>A <see cref="bool"/> with the tracking state.</returns>
    private bool IsTrackerTracking(string featureName)
    {
      if (!this.IsRpcConnected())
      {
        string message = featureName + " could not be started as no connection to the RPC server could be established.";
        ExceptionMethods.ProcessErrorMessage(message);
        return false;
      }

      if (!this.IsUdpConnected())
      {
        string message = featureName + " could not be started as no UDP data is been received from Smart Eye Aurora.";
        ExceptionMethods.ProcessErrorMessage(message);
        return false;
      }

      var state = TrackerState.Idling;
      try
      {
        this.smartEyeClient.RpcClient.GetState(out state);
      }
      catch (Exception ex)
      {
        string message = featureName + " could not be started as getting the tracker state failed with the following message:" +
                Environment.NewLine + ex.Message + Environment.NewLine + Environment.NewLine +
            "If this error is recurring, please make sure the hardware is connected and set up correctly, and try to reconnect.";
        ExceptionMethods.ProcessErrorMessage(message);
        return false;
      }

      if (state != TrackerState.Tracking)
      {
        try
        {
          this.smartEyeClient.RpcClient.StartTracking();
          return true;
        }
        catch (Exception ex)
        {
          string message = featureName + " could not be started as starting to track failed with the following message:" +
              Environment.NewLine + ex.Message + Environment.NewLine + Environment.NewLine +
          "If this error is recurring, please make sure the hardware is connected and set up correctly, and try to reconnect.";
          ExceptionMethods.ProcessErrorMessage(message);
          return false;
        }
      }

      return true;
    }

    #endregion PRIVATEMETHODS
  }
}
