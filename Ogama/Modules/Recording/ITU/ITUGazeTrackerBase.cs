// <copyright file="ITUGazeTrackerBase.cs" company="FU Berlin">
// ******************************************************
// OGAMA - open gaze and mouse analyzer 
// Copyright (C) 2010 Adrian Voßkühler  
// ------------------------------------------------------------------------
// This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
// This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
// You should have received a copy of the GNU General Public License along with this program; if not, write to the Free Software Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
// **************************************************************
// </copyright>
// <author>Adrian Voßkühler</author>
// <email>adrian.vosskuehler@fu-berlin.de</email>

namespace Ogama.Modules.Recording.ITUGazeTracker
{
  using System;
  using System.Drawing;
  using System.Windows.Forms;
  using DirectShowLib;
  using Ogama.ExceptionHandling;
  using Ogama.Modules.Common;
  using OgamaClient;
  using GazeTrackingLibrary.Settings;

  /// <summary>
  /// This class implements the <see cref="TrackerWithStatusControls"/> class
  /// to represent an OGAMA known eyetracker.
  /// It encapsulates the ITU GazeTracker http://www.gazegroup.org 
  /// </summary>
  public class ITUGazeTrackerBase : TrackerWithStatusControls
  {
    ///////////////////////////////////////////////////////////////////////////////
    // Defining Constants                                                        //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTANTS
    #endregion //CONSTANTS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Variables, Enumerations, Events                                  //
    ///////////////////////////////////////////////////////////////////////////////
    #region FIELDS

    /// <summary>
    /// This client contains the api connection to the ITU GazeTracker
    /// </summary>
    private ITUGazeTrackerAPI ituClient;

    /// <summary>
    /// This <see cref="EyeVideoControl"/> is a winform control displaying
    /// an OpenCV image of the eye.
    /// </summary>
    private EyeVideoControl eyeVideo;

    /// <summary>
    /// This <see cref="CalibrationResultControl"/> shows up the calibration result
    /// send from the ITU Gazetracker indicating quality with a star rating control.
    /// </summary>
    private CalibrationResultControl calibrationResultControl;

    /// <summary>
    /// X-Screenresolution in Pixel. 
    /// </summary>
    private int resolutionX;

    /// <summary>
    /// Y-Screenresolution in Pixel. 
    /// </summary>
    private int resolutionY;

    /// <summary>
    /// This button is used to call the settings dialog of the ITU tracker.
    /// </summary>
    private Button adjustButton;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the ITUGazeTrackerBase class.
    /// <remarks>
    /// Note that the xml settings file is set, but not used,
    /// GazeTracker internally saves it state in another location.
    /// </remarks>
    /// </summary>
    /// <param name="owningRecordModule">The <see cref="RecordModule"/>
    /// form wich host the recorder.</param>
    /// <param name="trackerTrackerControlsContainer">The <see cref="SplitContainer"/>
    /// control which contains two <see cref="SplitContainer"/>s with
    /// track status and calibration plot controls and buttons.</param>
    /// <param name="trackerTrackStatusPanel">The <see cref="Panel"/>
    /// which should contain the track status object.</param>
    /// <param name="trackerCalibrationResultPanel">The <see cref="Panel"/>
    /// which should contain the calibration result object.</param>
    /// <param name="trackerShowOnSecondaryScreenButton">The <see cref="Button"/>
    /// named "ShowOnPresentationScreenButton" at the tab page of the Tobii device.</param>
    /// <param name="trackerAcceptButton">The <see cref="Button"/>
    /// named "Accept" at the tab page of the device.</param>
    /// <param name="trackerRecalibrateButton">The <see cref="Button"/>
    /// named "Recalibrate" at the tab page of the device.</param>
    /// <param name="trackerConnectButton">The <see cref="Button"/>
    /// named "Connect" at the tab page of the device.</param>
    /// <param name="trackerAdjustButton">The <see cref="Button"/>
    /// named "Adjust" at the tab page of the device.</param>
    /// <param name="trackerSubjectButton">The <see cref="Button"/>
    /// named "Subject" at the tab page of the device.</param>
    /// <param name="trackerCalibrateButton">The <see cref="Button"/>
    /// named "Calibrate" at the tab page of the device.</param>
    /// <param name="trackerRecordButton">The <see cref="Button"/>
    /// named "Record" at the tab page of the device.</param>
    /// <param name="trackerSubjectNameTextBox">The <see cref="TextBox"/>
    /// which should contain the subject name at the tab page of the device.</param>
    public ITUGazeTrackerBase(
      RecordModule owningRecordModule,
      SplitContainer trackerTrackerControlsContainer,
      Panel trackerTrackStatusPanel,
      Panel trackerCalibrationResultPanel,
      Button trackerShowOnSecondaryScreenButton,
      Button trackerAcceptButton,
      Button trackerRecalibrateButton,
      Button trackerConnectButton,
      Button trackerAdjustButton,
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
      Properties.Settings.Default.EyeTrackerSettingsPath + "ITUGazeTrackerSetting.xml")
    {
      this.adjustButton = trackerAdjustButton;
      this.adjustButton.Click += new EventHandler(this.adjustButton_Click);
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Enumerations                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region ENUMS
    #endregion ENUMS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets the connection status of the ITUGazeTracker
    /// </summary>
    public override bool IsConnected
    {
      get { return this.ituClient.IsConnected; }
    }

    /// <summary>
    /// Sets the client that contains the api connection to the ITU GazeTracker
    /// </summary>
    protected ITUGazeTrackerAPI ItuClient
    {
      set { this.ituClient = value; }
    }

    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Public methods                                                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region PUBLICMETHODS

    /// <summary>
    /// Checks if the ITU GazeTracker has a camera available.
    /// </summary>
    /// <param name="errorMessage">Out. A <see cref="String"/> with an error message.</param>
    /// <returns><strong>True</strong>, if ITU Gazetracker with an camera
    /// is available in the system, otherwise <strong>false</strong></returns>
    public static bool IsAvailable(out string errorMessage)
    {
      errorMessage = string.Empty;

      try
      {
        DsDevice[] capDevices = DsDevice.GetDevicesOfCat(FilterCategory.VideoInputDevice);

        if (capDevices.Length == 0)
        {
          errorMessage = "The GazeTracker was unable to connect a camera. " + Environment.NewLine +
            "Make sure that the device is connected and that the device drivers are installed. " +
            "Verified configurations can be found in our forum located at http://forum.gazegroup.org";
          return false;
        }
      }
      catch (Exception)
      {
        errorMessage = "Initialization of the ITU GazeTracker failed.";
        return false;
      }

      return true;
    }

    /// <summary>
    /// Method to return the current timing of the tracking system.
    /// </summary>
    /// <returns>A <see cref="Int64"/> with the current time in milliseconds
    /// if the stopwatch is running, otherwise 0.</returns>
    public long GetCurrentTime()
    {
      return this.ituClient.GetCurrentTime();
    }

    #endregion //PUBLICMETHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Inherited methods                                                         //
    ///////////////////////////////////////////////////////////////////////////////
    #region OVERRIDES

    /// <summary>
    /// Connects to the ITU GazeTracker camera system.
    /// </summary>
    /// <returns><strong>True</strong> if connection succeded, otherwise
    /// <strong>false</strong>.</returns>
    public override bool Connect()
    {
      try
      {
        if (!this.ituClient.Connect())
        {
          return false;
        }
        else
        {
          this.SetPresentationScreen();

          // Video preview window (tracker visualizes image processing) 
          this.eyeVideo.Tracker = this.ituClient.Tracker;
          this.ituClient.CalibrationFinishedEvent += new EventHandler(this.ituClient_CalibrationFinishedEvent);

          this.adjustButton.Enabled = true;
        }
      }
      catch (Exception ex)
      {
        ConnectionFailedDialog dlg = new ConnectionFailedDialog();
        dlg.ErrorMessage = ex.Message;
        dlg.ShowDialog();
        this.CleanUp();
        return false;
      }

      return true;
    }

    /// <summary>
    /// Starts calibration.
    /// </summary>
    /// <param name="isRecalibrating">whether to use recalibration or not.</param>
    /// <returns><strong>True</strong> if calibration succeded, otherwise
    /// <strong>false</strong>.</returns>
    public override bool Calibrate(bool isRecalibrating)
    {
      try
      {
        this.SetPresentationScreen();
        this.ituClient.Calibrate(isRecalibrating);
      }
      catch (Exception ex)
      {
        InformationDialog.Show(
          "Calibration failed",
          "ITU GazeTracker calibration failed with the following message: " + Environment.NewLine + ex.Message,
          false,
          MessageBoxIcon.Error);

        this.CleanUp();
        return false;
      }

      return true;
    }

    /// <summary>
    /// Clean up objects.
    /// </summary>
    public override void CleanUp()
    {
      try
      {
        this.Stop();
        if (this.ituClient != null)
        {
          this.ituClient.CleanUp();
        }

        this.adjustButton.Enabled = false;
      }
      catch (Exception ex)
      {
        InformationDialog.Show(
          "CleanUp failed",
          "ITU GazeTracker CleanUp failed with the following message: " + Environment.NewLine + ex.Message,
          false,
          MessageBoxIcon.Error);
      }

      base.CleanUp();
    }

    /// <summary>
    /// Start tracking.
    /// </summary>
    public override void Record()
    {
      try
      {
        this.ituClient.Record();
      }
      catch (Exception ex)
      {
        InformationDialog.Show(
          "Record failed",
          "ITU GazeTracking Record failed with the following message: " + Environment.NewLine + ex.Message,
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
        if (this.eyeVideo != null)
        {
          // Stop updating images in small preview box
          this.eyeVideo.Stop();
        }

        if (this.ituClient != null)
        {
          this.ituClient.Stop();
        }
      }
      catch (Exception ex)
      {
        InformationDialog.Show(
          "Stop failed",
          "ITU GazeTracker stop tracking failed with the following message: " + Environment.NewLine + ex.Message,
          false,
          MessageBoxIcon.Error);
      }
    }

    /// <summary>
    /// Raises <see cref="GazeTrackerUI.Settings.SettingsWindow"/> to change the settings
    /// for this interface.
    /// </summary>
    public override void ChangeSettings()
    {
      this.SetPresentationScreen();
      this.ituClient.ChangeSettings();
    }

    /// <summary>
    /// Sets up calibration procedure and the tracking client
    /// and wires the events. Reads settings from file.
    /// </summary>
    protected override void Initialize()
    {
      this.ituClient.GazeDataChanged += new OgamaClient.OgamaGazeDataChangedEventHandler(this.ituClient_GazeDataChanged);

      base.Initialize();
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="TrackerWithStatusControls.ShowOnSecondaryScreenButton"/>.
    /// Shows a new track status object in a new thread.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/>.</param>
    protected override void btnShowOnPresentationScreen_Click(object sender, EventArgs e)
    {
      this.SetPresentationScreen();
      if (this.ShowOnSecondaryScreenButton.Text.Contains("Show"))
      {
        this.ShowOnSecondaryScreenButton.Text = "Hide from presentation screen";
        this.ShowOnSecondaryScreenButton.BackColor = Color.Red;

        this.ituClient.ShowOrHideTrackStatusOnPresentationScreen(true);
      }
      else
      {
        // Should hide TrackStatusDlg
        this.ShowOnSecondaryScreenButton.BackColor = Color.Transparent;
        this.ShowOnSecondaryScreenButton.Text = "Show on presentation screen";

        this.ituClient.ShowOrHideTrackStatusOnPresentationScreen(false);
      }
    }

    /// <summary>
    /// Overridden.
    /// Check visibility of the track status window before starting to record.
    /// </summary>
    protected override void PrepareRecording()
    {
      this.ituClient.ShowOrHideTrackStatusOnPresentationScreen(false);
    }

    /// <summary>
    /// This method initializes the designer components for the
    /// tobii interface tab page.
    /// This is from the visual studio designer removed, because it crashes,
    /// when tobii sdk dlls are not installed on the target computer.
    /// </summary>
    protected override void InitializeStatusControls()
    {
      if (this.ituClient != null)
      {
        this.eyeVideo = new EyeVideoControl();
        this.calibrationResultControl = new CalibrationResultControl();

        this.eyeVideo.Dock = System.Windows.Forms.DockStyle.Fill;
        this.eyeVideo.Location = new System.Drawing.Point(0, 0);
        this.eyeVideo.Name = "emguImageITUEyeVideo";
        this.eyeVideo.Size = new System.Drawing.Size(190, 54);
        this.eyeVideo.TabIndex = 0;

        this.calibrationResultControl.Dock = System.Windows.Forms.DockStyle.Fill;
        this.calibrationResultControl.Location = new System.Drawing.Point(0, 0);
        this.calibrationResultControl.Name = "calibrationResultControl";
        this.calibrationResultControl.Size = new System.Drawing.Size(190, 54);
        this.calibrationResultControl.TabIndex = 1;

        try
        {
          this.TrackStatusPanel.Controls.Add(this.eyeVideo);
          this.CalibrationResultPanel.Controls.Add(this.calibrationResultControl);
          this.ituClient.Initialize(this.eyeVideo, this.calibrationResultControl);
        }
        catch (Exception)
        {
          this.TrackStatusPanel.Controls.Clear();
          this.CalibrationResultPanel.Controls.Clear();
          throw;
        }
      }
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// accept <see cref="Button"/>.
    /// Accepts the calibration by enabling the record button
    /// and hiding the calibration plot and closing the calibration window
    /// of the itu client.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/>.</param>
    protected override void btnAcceptCalibration_Click(object sender, EventArgs e)
    {
      this.ituClient.AcceptCalibration();
      base.btnAcceptCalibration_Click(sender, e);
    }

    #endregion //OVERRIDES

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler                                                              //
    ///////////////////////////////////////////////////////////////////////////////
    #region EVENTS

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler for UI, Menu, Buttons, Toolbars etc.                         //
    ///////////////////////////////////////////////////////////////////////////////
    #region WINDOWSEVENTHANDLER

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="adjustButton"/>.
    /// Shows up the settings dialog with the tracking settings.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/>.</param>
    protected virtual void adjustButton_Click(object sender, EventArgs e)
    {
      this.ChangeSettings();
    }

    #endregion //WINDOWSEVENTHANDLER

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler for Custom Defined Events                                    //
    ///////////////////////////////////////////////////////////////////////////////
    #region CUSTOMEVENTHANDLER

    /// <summary>
    /// The <see cref="ITUGazeTrackerAPI.CalibrationFinishedEvent"/> event handler.
    /// Is called when the calibration has finished and the result is set to
    /// the control. So here we show the control.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/>.</param>
    private void ituClient_CalibrationFinishedEvent(object sender, EventArgs e)
    {
      // Hide the track status and show the calibration plot.
      this.ShowCalibPlot();
    }

    /// <summary>
    /// The <see cref="ITUGazeTrackerAPI.GazeDataChanged"/> event handler
    /// which is called whenever there is a new frame arrived.
    /// Sends the GazeDataChanged event to the recording module.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">The <see cref="OgamaGazeDataChangedEventArgs"/> with the event data
    /// which are the mapped gaze coordinates in pixel units.</param>
    private void ituClient_GazeDataChanged(object sender, OgamaGazeDataChangedEventArgs e)
    {
      GazeData gazeData = new GazeData();
      gazeData.Time = e.Gazedata.Time;
      gazeData.PupilDiaX = e.Gazedata.PupilDiaX;
      gazeData.PupilDiaY = e.Gazedata.PupilDiaY;

      // Calculate values between 0..1
      gazeData.GazePosX = (float)(e.Gazedata.GazePosX / (float)this.resolutionX);
      gazeData.GazePosY = (float)(e.Gazedata.GazePosY / (float)this.resolutionY);

      this.OnGazeDataChanged(new GazeDataChangedEventArgs(gazeData));
    }

    #endregion //CUSTOMEVENTHANDLER

    #endregion //EVENTS

    ///////////////////////////////////////////////////////////////////////////////
    // Methods and Eventhandling for Background tasks                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region BACKGROUNDWORKER
    #endregion //BACKGROUNDWORKER

    ///////////////////////////////////////////////////////////////////////////////
    // Methods for doing main class job                                          //
    ///////////////////////////////////////////////////////////////////////////////
    #region METHODS

    /// <summary>
    /// This method updates the itu tracker settings with
    /// the current set presentation screen,
    /// </summary>
    private void SetPresentationScreen()
    {
      this.resolutionX = Document.ActiveDocument.PresentationSize.Width;
      this.resolutionY = Document.ActiveDocument.PresentationSize.Height;

      if (Ogama.Properties.Settings.Default.PresentationScreenMonitor == "Secondary")
      {
        GTSettings.Current.ProcessingSettings.IsTrackingOnSecondaryScreen = true;
      }
      else
      {
        GTSettings.Current.ProcessingSettings.IsTrackingOnPrimaryScreen = true;
      }
    }

    #endregion //METHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER
    #endregion //HELPER
  }
}
