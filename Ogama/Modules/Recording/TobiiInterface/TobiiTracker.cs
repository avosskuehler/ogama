// <copyright file="TobiiTracker.cs" company="FU Berlin">
// ******************************************************
// OGAMA - open gaze and mouse analyzer 
// Copyright (C) 2013 Dr. Adrian Voßkühler  
// ------------------------------------------------------------------------
// This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
// This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
// You should have received a copy of the GNU General Public License along with this program; if not, write to the Free Software Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
// **************************************************************
// </copyright>
// <author>Adrian Voßkühler</author>
// <email>adrian@ogama.net</email>

namespace Ogama.Modules.Recording.TobiiInterface
{
  using System;
  using System.Collections.Generic;
  using System.Drawing;
  using System.IO;
  using System.Runtime.InteropServices;
  using System.Windows.Forms;
  using System.Xml.Serialization;

  using Ogama.ExceptionHandling;
  using Ogama.Modules.Common;
  using Ogama.Modules.Common.CustomEventArgs;
  using Ogama.Modules.Common.Tools;
  using Ogama.Modules.Recording.Dialogs;
  using Ogama.Modules.Recording.TrackerBase;
  using Ogama.Properties;

  using Tobii.Eyetracking.Sdk;
  using Tobii.Eyetracking.Sdk.Exceptions;
  using Tobii.Eyetracking.Sdk.Time;

  /// <summary>
  /// This class implements the <see cref="TrackerWithStatusControls"/> class
  ///   to represent an OGAMA known eyetracker.
  ///   It encapsulates a TOBII http://www.tobii.com eyetracker 
  ///   and is written with the SDK 3 from Tobii Systems.
  ///   It is tested with the Tobii T60/T120 series.
  /// </summary>
  public class TobiiTracker : TrackerWithStatusControls
  {
    ///////////////////////////////////////////////////////////////////////////////
    // Defining Constants                                                        //
    ///////////////////////////////////////////////////////////////////////////////

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Variables, Enumerations, Events                                  //
    ///////////////////////////////////////////////////////////////////////////////
    #region Constants and Fields

    /// <summary>
    /// The available eyetracker.
    /// </summary>
    private static List<EyetrackerInfo> availableEyetracker;

    /// <summary>
    /// The connected tracker.
    /// </summary>
    private static IEyetracker connectedTracker;

    /// <summary>
    ///   Saves the track status dialog that can be shown
    ///   to the subject before calibration or during
    ///   tracking.
    /// </summary>
    private TobiiTrackStatus dlgTrackStatus;

    /// <summary>
    /// The is tracking.
    /// </summary>
    private bool isTracking;

    /// <summary>
    /// The sync manager.
    /// </summary>
    private SyncManager syncManager;

    /// <summary>
    ///   It displays the result of a calibration, and can be used to provide
    ///   information to decide if the calibration should be accepted, 
    ///   rejected or improved.
    /// </summary>
    /// <remarks>
    ///   That is from the TobiiSDK documentation.
    /// </remarks>
    private TobiiCalibrationResultPanel tobiiCalibPlot;

    /// <summary>
    ///   Saves the time stamp object.
    /// </summary>
    private Clock tobiiClock;

    /// <summary>
    ///   Saves the tobii settings.
    /// </summary>
    private TobiiSetting tobiiSettings;

    /// <summary>
    ///   The TetTrackStatus is a help tool to provide a real time display 
    ///   of the tracking ability of the subject being eye tracked and 
    ///   to make it easy to verify that the subject is advantageous 
    ///   positioned. Preferably use it before the gaze tracking starts to 
    ///   verify that the eyes of the subject are found. Note that 
    ///   the subjects gaze point is not represented in any way, 
    ///   only the position of the eyes in the eye tracker sensor are shown.
    /// </summary>
    /// <remarks>
    ///   That is from the TobiiSDK documentation.
    /// </remarks>
    private TobiiTrackStatusControl tobiiTrackStatus;

    #endregion

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the TobiiTracker class.
    ///   Initializes COM objects and other.
    /// </summary>
    /// <param name="owningRecordModule">
    /// The <see cref="RecordModule"/>
    ///   form wich host the recorder.
    /// </param>
    /// <param name="trackerTrackerControlsContainer">
    /// The <see cref="SplitContainer"/>
    ///   control which contains two <see cref="SplitContainer"/>s with
    ///   track status and calibration plot controls and buttons.
    /// </param>
    /// <param name="trackerTrackStatusPanel">
    /// The <see cref="Panel"/>
    ///   which should contain the track status object.
    /// </param>
    /// <param name="trackerCalibrationResultPanel">
    /// The <see cref="Panel"/>
    ///   which should contain the calibration result object.
    /// </param>
    /// <param name="trackerShowOnSecondaryScreenButton">
    /// The <see cref="Button"/>
    ///   named "ShowOnPresentationScreenButton" at the tab page of the Tobii device.
    /// </param>
    /// <param name="trackerAcceptButton">
    /// The <see cref="Button"/>
    ///   named "Accept" at the tab page of the Tobii device.
    /// </param>
    /// <param name="trackerRecalibrateButton">
    /// The <see cref="Button"/>
    ///   named "Recalibrate" at the tab page of the Tobii device.
    /// </param>
    /// <param name="trackerConnectButton">
    /// The <see cref="Button"/>
    ///   named "Connect" at the tab page of the Tobii device.
    /// </param>
    /// <param name="trackerSubjectButton">
    /// The <see cref="Button"/>
    ///   named "Subject" at the tab page of the Tobii device.
    /// </param>
    /// <param name="trackerCalibrateButton">
    /// The <see cref="Button"/>
    ///   named "Calibrate" at the tab page of the Tobii device.
    /// </param>
    /// <param name="trackerRecordButton">
    /// The <see cref="Button"/>
    ///   named "Record" at the tab page of the Tobii device.
    /// </param>
    /// <param name="trackerSubjectNameTextBox">
    /// The <see cref="TextBox"/>
    ///   which should contain the subject name at the tab page of the Tobii device.
    /// </param>
    public TobiiTracker(
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
        Properties.Settings.Default.EyeTrackerSettingsPath + "TobiiSetting.xml")
    {
      // Call the initialize methods of derived classes
      this.Initialize();
    }

    #endregion

    #region Public Properties

    /// <summary>
    /// Gets the tracker browser.
    /// </summary>
    public static EyetrackerBrowser TrackerBrowser { get; private set; }

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Enumerations                                                     //
    ///////////////////////////////////////////////////////////////////////////////

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////

    /// <summary>
    ///   Gets the connection status of the tobii tracker
    /// </summary>
    public override bool IsConnected
    {
      get
      {
        return connectedTracker != null;
      }
    }

    /// <summary>
    ///   Gets the current tobii settings.
    /// </summary>
    /// <value>A <see cref = "TobiiSetting" /> with the current tracker settings.</value>
    public TobiiSetting Settings
    {
      get
      {
        return this.tobiiSettings;
      }
    }

    #endregion

    ///////////////////////////////////////////////////////////////////////////////
    // Public methods                                                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region Public Methods

    /// <summary>
    /// Checks if the tobii tracker is available in the system.
    /// </summary>
    /// <param name="errorMessage">
    /// Out. A <see cref="string"/> with an error message.
    /// </param>
    /// <returns>
    /// <strong>True</strong>, if Tobii tracker 
    ///   is available in the system, otherwise <strong>false</strong>
    /// </returns>
    public static bool IsAvailable(out string errorMessage)
    {
      if (availableEyetracker.Count > 0)
      {
        var name = availableEyetracker[0].GivenName;
        errorMessage = "Tobii: " + availableEyetracker[0].Model;
        if (name != string.Empty)
        {
          errorMessage += ", Name: " + name;
        }

        errorMessage += " is found";

        return true;
      }

      errorMessage = "No tobii eyetracker has been found on the system. ";
      return false;
    }

    /// <summary>
    /// The static dispose.
    /// </summary>
    public static void StaticDispose()
    {
      TrackerBrowser.Stop();
    }

    /// <summary>
    /// The static initialize.
    /// </summary>
    public static void StaticInitialize()
    {
      Library.Init();
      availableEyetracker = new List<EyetrackerInfo>();
      TrackerBrowser = new EyetrackerBrowser();
      TrackerBrowser.EyetrackerFound += EyetrackerFound;
      TrackerBrowser.EyetrackerUpdated += EyetrackerUpdated;
      TrackerBrowser.EyetrackerRemoved += EyetrackerRemoved;
      TrackerBrowser.Start();
    }

    /// <summary>
    /// Starts calibration.
    /// </summary>
    /// <param name="isRecalibrating">
    /// whether to use recalibration or not.
    /// </param>
    /// <returns>
    /// <strong>True</strong> if calibration succeded, otherwise
    ///   <strong>false</strong>.
    /// </returns>
    public override bool Calibrate(bool isRecalibrating)
    {
      try
      {
        var runner = new TobiiCalibrationRunner();

        try
        {
          // Should hide TrackStatusDlg
          if (this.dlgTrackStatus != null)
          {
            this.ShowOnSecondaryScreenButton.BackColor = Color.Transparent;
            this.ShowOnSecondaryScreenButton.Text = "Show on presentation screen";
            this.dlgTrackStatus.Close();
          }

          // Start a new calibration procedure
          var result = runner.RunCalibration(connectedTracker);

          // Show a calibration plot if everything went OK
          if (result != null)
          {
            this.tobiiCalibPlot.Initialize(result.Plot);
            this.ShowCalibPlot();
          }
          else
          {
            MessageBox.Show("Not enough data to create a calibration (or calibration aborted).");
          }
        }
        catch (EyetrackerException ee)
        {
          MessageBox.Show(
            "Failed to calibrate. Got exception " + ee, "Calibration Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
      }
      catch (Exception ex)
      {
        InformationDialog.Show(
          "Calibration failed",
          "Tobii calibration failed with the following message: " + Environment.NewLine + ex.Message,
          false,
          MessageBoxIcon.Error);

        this.CleanUp();
        return false;
      }

      return true;
    }

    /// <summary>
    /// Raises <see cref="TobiiSettingsDialog"/> to change the settings
    ///   for this interface.
    /// </summary>
    public override void ChangeSettings()
    {
      var dlg = new TobiiSettingsDialog { TobiiSettings = this.tobiiSettings };
      switch (dlg.ShowDialog())
      {
        case DialogResult.OK:
          this.tobiiSettings = dlg.TobiiSettings;
          this.UpdateSettings();
          this.SerializeSettings(this.tobiiSettings, this.SettingsFile);
          break;
      }
    }

    /// <summary>
    /// Clean up objects.
    /// </summary>
    public override void CleanUp()
    {
      try
      {
        this.Stop();
        this.DisconnectTracker();
      }
      catch (Exception ex)
      {
        InformationDialog.Show(
          "CleanUp failed",
          "Tobii CleanUp failed with the following message: " + Environment.NewLine + ex.Message,
          false,
          MessageBoxIcon.Error);
      }

      base.CleanUp();
    }

    /// <summary>
    /// Connects the track status object to the tobii system.
    /// </summary>
    /// <returns>
    /// <strong>True</strong> if connection succeded, otherwise
    ///   <strong>false</strong>.
    /// </returns>
    public override bool Connect()
    {
      try
      {
        if (availableEyetracker.Count == 0)
        {
          throw new EyetrackerException(1, "No tobii eyetracker system found");
        }

        this.ConnectToTracker(availableEyetracker[0]);
      }
      catch (Exception ex)
      {
        var dlg = new ConnectionFailedDialog { ErrorMessage = ex.Message };
        dlg.ShowDialog();
        this.CleanUp();
        return false;
      }

      return true;
    }

    /// <summary>
    /// Method to return the current timing of the tracking system.
    /// </summary>
    /// <returns>
    /// A <see cref="long"/> with the time in milliseconds.
    /// </returns>
    public long GetCurrentTime()
    {
      return this.tobiiClock.GetTime();
    }

    /// <summary>
    /// Start tracking.
    /// </summary>
    public override void Record()
    {
      try
      {
        if (!this.isTracking)
        {
          // Start subscribing to gaze data stream
          //connectedTracker.StartTracking();
          this.isTracking = true;
        }
      }
      catch (Exception ex)
      {
        InformationDialog.Show(
          "Record failed",
          "Tobii Record failed with the following message: " + Environment.NewLine + ex.Message,
          false,
          MessageBoxIcon.Error);
        this.Stop();
      }
    }

    /// <summary>
    /// Stops tracking.
    /// </summary>
    public override void Stop()
    {
      try
      {
        if (connectedTracker == null)
        {
          return;
        }

        this.isTracking = false;
        //connectedTracker.StopTracking();
      }
      catch (Exception ex)
      {
        InformationDialog.Show(
          "Stop failed",
          "Tobii stop tracking failed with the following message: " + Environment.NewLine + ex.Message,
          false,
          MessageBoxIcon.Error);
      }
    }

    #endregion

    #region Methods

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    ///   <see cref="Button"/> <see cref="TrackerWithStatusControls.ShowOnSecondaryScreenButton"/>.
    ///   Shows a new track status object in a new thread.
    /// </summary>
    /// <param name="sender">
    /// Source of the event.
    /// </param>
    /// <param name="e">
    /// An empty <see cref="EventArgs"/>.
    /// </param>
    protected override void BtnShowOnPresentationScreenClick(object sender, EventArgs e)
    {
      if (this.ShowOnSecondaryScreenButton.Text.Contains("Show"))
      {
        // Should show TrackStatusDlg
        if (this.dlgTrackStatus != null)
        {
          this.dlgTrackStatus.Dispose();
        }

        this.dlgTrackStatus = new TobiiTrackStatus(this.Settings);

        Rectangle presentationBounds = PresentationScreen.GetPresentationWorkingArea();

        this.dlgTrackStatus.Location =
          new Point(
            presentationBounds.Left + presentationBounds.Width / 2 - this.dlgTrackStatus.Width / 2,
            presentationBounds.Top + presentationBounds.Height / 2 - this.dlgTrackStatus.Height / 2);

        // Dialog will be disposed when connection failed.
        if (!this.dlgTrackStatus.IsDisposed)
        {
          this.ShowOnSecondaryScreenButton.Text = "Hide from presentation screen";
          this.ShowOnSecondaryScreenButton.BackColor = Color.Red;
          this.dlgTrackStatus.Show();
        }
      }
      else
      {
        // Should hide TrackStatusDlg
        if (this.dlgTrackStatus != null)
        {
          this.ShowOnSecondaryScreenButton.BackColor = Color.Transparent;
          this.ShowOnSecondaryScreenButton.Text = "Show on presentation screen";
          this.dlgTrackStatus.Close();
        }
      }
    }

    /// <summary>
    /// Sets up calibration procedure and the tracking client
    ///   and wires the events. Reads settings from file.
    /// </summary>
    protected override sealed void Initialize()
    {
      // Load tobii tracker settings.
      if (File.Exists(this.SettingsFile))
      {
        this.tobiiSettings = this.DeserializeSettings(this.SettingsFile);
      }
      else
      {
        this.tobiiSettings = new TobiiSetting();
        this.SerializeSettings(this.tobiiSettings, this.SettingsFile);
      }

      this.UpdateSettings();

      base.Initialize();
    }

    /// <summary>
    /// This method initializes the designer components for the
    ///   tobii interface tab page.
    ///   This is from the visual studio designer removed, because it crashes,
    ///   when tobii sdk dlls are not installed on the target computer.
    /// </summary>
    protected override void InitializeStatusControls()
    {
      if (this.tobiiTrackStatus == null && this.tobiiCalibPlot == null)
      {
        this.tobiiTrackStatus = new TobiiTrackStatusControl();
        this.tobiiCalibPlot = new TobiiCalibrationResultPanel();

        // TobiiTrackStatus
        this.tobiiTrackStatus.Dock = DockStyle.Fill;
        this.tobiiTrackStatus.Enabled = true;
        this.tobiiTrackStatus.Location = new Point(0, 0);
        this.tobiiTrackStatus.Name = "tobiiTrackStatus";
        this.tobiiTrackStatus.Size = new Size(190, 54);
        this.tobiiTrackStatus.TabIndex = 0;

        // TobiiCalibPlot
        this.tobiiCalibPlot.Dock = DockStyle.Fill;
        this.tobiiCalibPlot.Enabled = true;
        this.tobiiCalibPlot.Location = new Point(0, 0);
        this.tobiiCalibPlot.Name = "tobiiCalibPlot";
        this.tobiiCalibPlot.Size = new Size(190, 54);
        this.tobiiCalibPlot.TabIndex = 0;

        try
        {
          this.TrackStatusPanel.Controls.Add(this.tobiiTrackStatus);
          this.CalibrationResultPanel.Controls.Add(this.tobiiCalibPlot);

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
    /// Overridden.
    ///   Check visibility of the track status window before starting to record.
    /// </summary>
    protected override void PrepareRecording()
    {
      if (this.dlgTrackStatus != null && this.dlgTrackStatus.Visible)
      {
        // Hide TrackStatusDlg
        this.ShowOnSecondaryScreenButton.BackColor = Color.Transparent;
        this.ShowOnSecondaryScreenButton.Text = "Show on presentation screen";
        this.dlgTrackStatus.Close();
      }
    }

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler                                                              //
    ///////////////////////////////////////////////////////////////////////////////

    /// <summary>
    /// The eyetracker found.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    private static void EyetrackerFound(object sender, EyetrackerInfoEventArgs e)
    {
      // When an eyetracker is found on the network we add it to the listview
      availableEyetracker.Add(e.EyetrackerInfo);
    }

    /// <summary>
    /// The static event handler method that is called whenever an eyetracker 
    /// has been removed.
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">A <see cref="EyetrackerInfoEventArgs"/> with the event data.</param>
    private static void EyetrackerRemoved(object sender, EyetrackerInfoEventArgs e)
    {
      // When an eyetracker disappears from the network we remove it from the listview
      availableEyetracker.Remove(e.EyetrackerInfo);
    }

    /// <summary>
    /// The static event handler method that is called whenever an eyetracker 
    /// has been updated.
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">A <see cref="EyetrackerInfoEventArgs"/> with the event data.</param>
    private static void EyetrackerUpdated(object sender, EyetrackerInfoEventArgs e)
    {
    }

    /// <summary>
    /// This methods connects to the tracker.
    /// </summary>
    /// <param name="info">A <see cref="EyetrackerInfo"/>with information about the
    /// eyetracker to connect to.</param>
    private void ConnectToTracker(EyetrackerInfo info)
    {
      try
      {
        connectedTracker = EyetrackerFactory.CreateEyetracker(info);
        connectedTracker.ConnectionError += this.HandleConnectionError;
        this.tobiiSettings.ConnectedTrackerName = connectedTracker.GetUnitName();

        this.tobiiClock = new Clock();
        this.syncManager = new SyncManager(this.tobiiClock, info);

        connectedTracker.GazeDataReceived += this.ConnectedTrackerGazeDataReceived;
        connectedTracker.StartTracking();
      }
      catch (EyetrackerException ee)
      {
        if (ee.ErrorCode == 0x20000402)
        {
          MessageBox.Show(
            Resources.TobiiTracker_ConnectToTrackerFailed, "Upgrade Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        else
        {
          MessageBox.Show("Eyetracker responded with error " + ee, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        this.DisconnectTracker();
      }
      catch (Exception)
      {
        MessageBox.Show(
          "Could not connect to eyetracker.", "Connection Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
        this.DisconnectTracker();
      }
    }

    /// <summary>
    /// Deserializes the <see cref="TobiiSetting"/> from the given xml file.
    /// </summary>
    /// <param name="filePath">Full file path to the xml settings file. </param>
    /// <returns> A <see cref="TobiiSetting"/> object. </returns>
    private TobiiSetting DeserializeSettings(string filePath)
    {
      var settings = new TobiiSetting();

      // Create an instance of the XmlSerializer class;
      // specify the type of object to be deserialized 
      var serializer = new XmlSerializer(typeof(TobiiSetting));

      // If the XML document has been altered with unknown 
      // nodes or attributes, handle them with the 
      // UnknownNode and UnknownAttribute events.
      serializer.UnknownNode += this.SerializerUnknownNode;
      serializer.UnknownAttribute += this.SerializerUnknownAttribute;

      try
      {
        // A FileStream is needed to read the XML document.
        var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);

        // Use the Deserialize method to restore the object's state with
        // data from the XML document.
        settings = (TobiiSetting)serializer.Deserialize(fs);
        fs.Close();
      }
      catch (Exception ex)
      {
        InformationDialog.Show(
          "Error occured",
          "Deserialization of TobiiSettings failed with the following message: " + Environment.NewLine + ex.Message,
          false,
          MessageBoxIcon.Error);
      }

      return settings;
    }

    /// <summary>
    /// The disconnect tracker method.
    /// </summary>
    private void DisconnectTracker()
    {
      if (connectedTracker == null)
      {
        return;
      }

      connectedTracker.GazeDataReceived -= this.ConnectedTrackerGazeDataReceived;
      connectedTracker.Dispose();
      connectedTracker = null;
      this.isTracking = false;

      this.syncManager.Dispose();
    }

    /// <summary>
    /// This method handles a connection error by disconnecting.
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">The <see cref="ConnectionErrorEventArgs"/> with the new gaze data
    /// from the device.</param>
    private void HandleConnectionError(object sender, ConnectionErrorEventArgs e)
    {
      // If the connection goes down we dispose 
      // the IAsyncEyetracker instance. This will release 
      // all resources held by the connection
      this.DisconnectTracker();
    }

    /// <summary>
    /// Serializes the <see cref="TobiiSetting"/> into the given file in a xml structure.
    /// </summary>
    /// <param name="settings">
    /// The <see cref="TobiiSetting"/> object to serialize.
    /// </param>
    /// <param name="filePath">
    /// Full file path to the xml settings file.
    /// </param>
    private void SerializeSettings(TobiiSetting settings, string filePath)
    {
      // Create an instance of the XmlSerializer class;
      // specify the type of object to serialize 
      var serializer = new XmlSerializer(typeof(TobiiSetting));

      // Serialize the TobiiSetting, and close the TextWriter.
      try
      {
        TextWriter writer = new StreamWriter(filePath, false);
        serializer.Serialize(writer, settings);
        writer.Close();
      }
      catch (Exception ex)
      {
        InformationDialog.Show(
          "Error occured",
          "Serialization of TobiiSettings failed with the following message: " + Environment.NewLine + ex.Message,
          false,
          MessageBoxIcon.Error);
      }
    }

    /// <summary>
    /// This method sets up the user interface to use 
    /// the new settings for the calibration.
    /// </summary>
    private void UpdateSettings()
    {
      // TODO: Adapt to SDK 3.
      // this.tobiiCalibPlot.PointColor = (uint)ColorTranslator.ToOle(this.tobiiSettings.TetCalibPointColor);
      // this.tetCalibProc.PointSize = this.tobiiSettings.TetCalibPointSizes;
      // this.tetCalibProc.PointSpeed = this.tobiiSettings.TetCalibPointSpeeds;
      // this.tetCalibProc.NumPoints = this.tobiiSettings.TetNumCalibPoint;
      // this.tetCalibProc.BackgroundColor = (uint)ColorTranslator.ToOle(this.tobiiSettings.TetCalibBackgroundColor);
    }

    private long lasttime = -1;

    /// <summary>
    /// OnGazeData event handler for connected tracker.
    ///   This event fires whenever there are new gaze data 
    ///   to receive.
    ///   It converts the interface internal gaze structure into
    ///   a OGAMA readable format and fires the <see cref="Tracker.OnGazeDataChanged"/>
    ///   event to the recorder.
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">The <see cref="GazeDataEventArgs"/> with the new gaze data
    /// from the device.</param>
    private void ConnectedTrackerGazeDataReceived(object sender, GazeDataEventArgs e)
    {
      // Send the gaze data to the track status control.
      GazeDataItem gd = e.GazeDataItem;
      this.tobiiTrackStatus.OnGazeData(gd);
      if (this.dlgTrackStatus != null && this.dlgTrackStatus.Visible)
      {
        this.dlgTrackStatus.Update(gd);
      }

      if (this.syncManager.SyncState.StateFlag == SyncStateFlag.Synchronized)
      {
        var convertedTime = this.syncManager.RemoteToLocal(gd.TimeStamp);
        var localTime = this.tobiiClock.GetTime();
      }
      else
      {
        Console.WriteLine("Warning. Sync state is " + this.syncManager.SyncState.StateFlag);
      }

      //if (this.lasttime == gd.TimeStamp)
      //{
      //  var message = string.Format(
      //    "TobiiTracker, ConnectedTrackerGazeDataReceived: Data sample with time {0} "
      //    + "has same timestamp as foregoing sample ",
      //    gd.TimeStamp);
      //  throw new ArgumentException(message);
      //}

      this.lasttime = gd.TimeStamp;

      var newGazeData = new GazeData { Time = gd.TimeStamp/1000 };

      // Convert Tobii gazestamp in milliseconds.

      // The validity code takes one of five values for each eye ranging from 0 to 4, with the
      // following interpretation:
      // 0 - The eye tracker is certain that the data for this eye is right. There is no risk of
      // confusing data from the other eye.
      // 1 - The eye tracker has only recorded one eye, and has made some assumptions and
      // estimations regarding which is the left and which is the right eye. However, it is still
      // very likely that the assumption made is correct. The validity code for the other eye is
      // in this case always set to 3.
      // 2 - The eye tracker has only recorded one eye, and has no way of determining which
      // one is left eye and which one is right eye. The validity code for both eyes is set to 2.
      // 3 - The eye tracker is fairly confident that the actual gaze data belongs to the other
      // eye. The other eye will always have validity code 1.
      // 4 - The actual gaze data is missing or definitely belonging to the other eye.
      // Hence, there are a limited number of possible combinations of validity codes for the
      // two eyes:
      // Code Description
      // 0 - 0 Both eyes found. Data is valid for both eyes.
      // 0 - 4 or 4 - 0 One eye found. Gaze data is the same for both eyes.
      // 1 – 3 or 3 - 1 One eye found. Gaze data is the same for both eyes.
      // 2 – 2 One eye found. Gaze data is the same for both eyes.
      // 4 – 4 No eye found. Gaze data for both eyes are invalid.
      // Use data only if both left and right eye was found by the eye tracker
      // It is recommended that the validity codes are always used for data filtering, 
      // to remove data points which are obviously incorrect. 
      // Normally, we recommend removing all data points with a validity code of 2 or higher.
      if (gd.LeftValidity == 0 && gd.RightValidity == 0)
      {
        // Let the x, y and distance be the right and left eye average
        newGazeData.GazePosX = (float)((gd.LeftGazePoint2D.X + gd.RightGazePoint2D.X) / 2);
        newGazeData.GazePosY = (float)((gd.LeftGazePoint2D.Y + gd.RightGazePoint2D.Y) / 2);
        newGazeData.PupilDiaX = gd.LeftPupilDiameter;
        newGazeData.PupilDiaY = gd.RightPupilDiameter;
      }
      else if (gd.LeftValidity == 4 && gd.RightValidity == 4)
      {
        newGazeData.GazePosX = 0;
        newGazeData.GazePosY = 0;
        newGazeData.PupilDiaX = 0;
        newGazeData.PupilDiaY = 0;
      }
      else if (gd.LeftValidity == 2 && gd.RightValidity == 2)
      {
        newGazeData.GazePosX = 0;
        newGazeData.GazePosY = 0;
        newGazeData.PupilDiaX = 0;
        newGazeData.PupilDiaY = 0;
      }
      else if (gd.LeftValidity == 1 && gd.RightValidity == 3)
      {
        newGazeData.GazePosX = (float)gd.LeftGazePoint2D.X;
        newGazeData.GazePosY = (float)gd.LeftGazePoint2D.Y;
        newGazeData.PupilDiaX = gd.LeftPupilDiameter;
        newGazeData.PupilDiaY = null;
      }
      else if (gd.LeftValidity == 3 && gd.RightValidity == 1)
      {
        newGazeData.GazePosX = (float)gd.RightGazePoint2D.X;
        newGazeData.GazePosY = (float)gd.RightGazePoint2D.Y;
        newGazeData.PupilDiaX = null;
        newGazeData.PupilDiaY = gd.RightPupilDiameter;
      }
      else if (gd.LeftValidity == 0 && gd.RightValidity == 4)
      {
        newGazeData.GazePosX = (float)gd.LeftGazePoint2D.X;
        newGazeData.GazePosY = (float)gd.LeftGazePoint2D.Y;
        newGazeData.PupilDiaX = gd.LeftPupilDiameter;
        newGazeData.PupilDiaY = null;
      }
      else if (gd.LeftValidity == 4 && gd.RightValidity == 0)
      {
        newGazeData.GazePosX = (float)gd.RightGazePoint2D.X;
        newGazeData.GazePosY = (float)gd.RightGazePoint2D.Y;
        newGazeData.PupilDiaX = null;
        newGazeData.PupilDiaY = gd.RightPupilDiameter;
      }
      else
      {
        newGazeData.GazePosX = null;
        newGazeData.GazePosY = null;
        newGazeData.PupilDiaX = null;
        newGazeData.PupilDiaY = null;
      }

      this.OnGazeDataChanged(new GazeDataChangedEventArgs(newGazeData));
    }

    #endregion
  }
}