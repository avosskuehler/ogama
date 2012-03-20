// <copyright file="GazetrackerDirectClientTracker.cs" company="FU Berlin">
// ******************************************************
// OGAMA - open gaze and mouse analyzer 
// Copyright (C) 2012 Adrian Voßkühler  
// ------------------------------------------------------------------------
// This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
// This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
// You should have received a copy of the GNU General Public License along with this program; if not, write to the Free Software Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
// **************************************************************
// </copyright>
// <author>Adrian Voßkühler</author>
// <email>adrian@ogama.net</email>

namespace Ogama.Modules.Recording.GazegroupInterface
{
  using System;
  using System.Drawing;
  using System.Windows.Forms;
  using GTHardware;
  using GTLibrary.Utils;
  using GTOgamaClient.API;
  using GTOgamaClient.Controls;
  using Ogama.ExceptionHandling;
  using Ogama.Modules.Common;
  using Ogama.Modules.Common.CustomEventArgs;
  using Ogama.Modules.Recording.Dialogs;
  using Ogama.Modules.Recording.TrackerBase;
  using OgamaControls;

  /// <summary>
  /// This class implements the <see cref="TrackerWithStatusControls"/> class
  /// to represent an OGAMA known eyetracker.
  /// It encapsulates the Gazegroup GazeTracker http://www.gazegroup.org in an implementation
  /// that uses a direct connection to the gazetracker via the
  /// ogama client.
  /// </summary>
  public class GazetrackerDirectClientTracker : Tracker
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
    /// This button is used to call the settings dialog of the ITU tracker.
    /// </summary>
    private readonly Button adjustButton;

    /// <summary>
    /// This <see cref="Button"/> is placed at the bottom of
    /// the track status <see cref="SplitContainer"/>.
    /// </summary>
    private readonly Button showOnSecondaryScreenButton;

    /// <summary>
    /// This <see cref="EyeVideoControl"/> is a winform control displaying
    /// an OpenCV image of the eye.
    /// </summary>
    private readonly EyeVideoControl eyeVideo;

    /// <summary>
    /// This client contains the api connection to the ITU GazeTracker
    /// </summary>
    private GazeTrackerAPI gazeTrackerApiClient;

    /// <summary>
    /// The resolution of the presentation screen
    /// </summary>
    private Size presentationScreenSize;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the GazetrackerDirectClientTracker class.
    /// <remarks>
    /// Note that the xml settings file is set, but not used,
    /// GazeTracker internally saves it state in another location.
    /// </remarks>
    /// </summary>
    /// <param name="owningRecordModule">The <see cref="RecordModule"/>
    /// form wich host the recorder.</param>
    /// <param name="trackerEyeVideoControl">The <see cref="EyeVideoControl"/>
    /// which displays the eye video of the gazetracker.</param>
    /// <param name="trackerShowOnSecondaryScreenButton">The <see cref="Button"/>
    /// named "ShowOnPresentationScreenButton" at the tab page of the Tobii device.</param>
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
    public GazetrackerDirectClientTracker(
      RecordModule owningRecordModule,
      EyeVideoControl trackerEyeVideoControl,
      Button trackerShowOnSecondaryScreenButton,
      Button trackerConnectButton,
      Button trackerAdjustButton,
      Button trackerSubjectButton,
      Button trackerCalibrateButton,
      Button trackerRecordButton,
      TextBox trackerSubjectNameTextBox)
      : base(
      owningRecordModule,
      trackerConnectButton,
      trackerSubjectButton,
      trackerCalibrateButton,
      trackerRecordButton,
      trackerSubjectNameTextBox,
      Properties.Settings.Default.EyeTrackerSettingsPath + "GazetrackerDirectClientSetting.xml")
    {
      this.adjustButton = trackerAdjustButton;
      this.adjustButton.Click += this.AdjustButtonClick;
      this.showOnSecondaryScreenButton = trackerShowOnSecondaryScreenButton;
      this.showOnSecondaryScreenButton.Click += this.BtnShowOnPresentationScreenClick;
      this.eyeVideo = trackerEyeVideoControl;
      this.Initialize();
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
      get { return this.gazeTrackerApiClient.IsConnected; }
    }

    /// <summary>
    /// Sets the client that contains the api connection to the ITU GazeTracker
    /// </summary>
    protected GazeTrackerAPI ItuClient
    {
      set { this.gazeTrackerApiClient = value; }
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
        switch (Camera.GetConnectedDevice())
        {
          case Camera.DeviceTypeEnum.None:
            errorMessage = "Gazetracker was unable to connect a camera. " + Environment.NewLine +
              "Make sure that the device is connected and that the device drivers are installed. " +
              "Verified configurations can be found in the forum located at http://forum.gazegroup.org";
            return false;
          case Camera.DeviceTypeEnum.DirectShow:
            errorMessage = "Gazetracker found a camera. ";
            return true;
          case Camera.DeviceTypeEnum.Thorlabs:
            errorMessage = "Gazetracker found a thorlabs camera. ";
            return true;
          case Camera.DeviceTypeEnum.Kinect:
            errorMessage = "Gazetracker found a kinect camera. ";
            return true;
        }
      }
      catch (Exception)
      {
        errorMessage = "Initialization of the GazeTracker failed.";
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
      return this.gazeTrackerApiClient.GetCurrentTime();
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
        if (!this.gazeTrackerApiClient.Connect())
        {
          return false;
        }

        this.eyeVideo.Start();
        this.SetPresentationScreen();

        // Video preview window (tracker visualizes image processing) 
        this.gazeTrackerApiClient.CalibrationFinishedEvent += this.GazeTrackerApiClientCalibrationFinishedEvent;
        this.adjustButton.Enabled = true;
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
        this.gazeTrackerApiClient.Calibrate();
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
        if (this.gazeTrackerApiClient != null)
        {
          this.gazeTrackerApiClient.CleanUp();
        }

        this.adjustButton.Enabled = false;
        this.SubjectButton.Enabled = false;
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
        this.gazeTrackerApiClient.Record();
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
          // this.eyeVideo.Stop();
        }

        if (this.gazeTrackerApiClient != null)
        {
          this.gazeTrackerApiClient.Stop();
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
    /// Raises GTApplication SettingsWindow to change the settings
    /// for this interface.
    /// </summary>
    public override void ChangeSettings()
    {
      this.SetPresentationScreen();
      this.gazeTrackerApiClient.ChangeSettings();
    }

    /// <summary>
    /// Sets up calibration procedure and the tracking client
    /// and wires the events. Reads settings from file.
    /// </summary>
    protected override sealed void Initialize()
    {
      this.presentationScreenSize = Document.ActiveDocument.PresentationSize;
      this.gazeTrackerApiClient = new GazeTrackerAPI();
      this.gazeTrackerApiClient.Initialize(this.eyeVideo);
      this.gazeTrackerApiClient.GTExtendedDataChanged += this.OnGTExtendedDataChanged;
    }

    /// <summary>
    /// Overridden.
    /// Check visibility of the track status window before starting to record.
    /// </summary>
    protected override void PrepareRecording()
    {
      this.gazeTrackerApiClient.ShowOrHideTrackStatusOnPresentationScreen(false);
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
      this.adjustButton.Enabled = true;
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
    protected virtual void AdjustButtonClick(object sender, EventArgs e)
    {
      this.ChangeSettings();
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="TrackerWithStatusControls.ShowOnSecondaryScreenButton"/>.
    /// Shows a new track status object in a new thread.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/>.</param>
    protected void BtnShowOnPresentationScreenClick(object sender, EventArgs e)
    {
      this.SetPresentationScreen();
      if (this.showOnSecondaryScreenButton.Text.Contains("Show"))
      {
        this.showOnSecondaryScreenButton.Text = "Hide from presentation screen";
        this.showOnSecondaryScreenButton.BackColor = Color.Red;

        this.gazeTrackerApiClient.ShowOrHideTrackStatusOnPresentationScreen(true);
      }
      else
      {
        // Should hide TrackStatusDlg
        this.showOnSecondaryScreenButton.BackColor = Color.Transparent;
        this.showOnSecondaryScreenButton.Text = "Show on presentation screen";

        this.gazeTrackerApiClient.ShowOrHideTrackStatusOnPresentationScreen(false);
      }
    }

    #endregion //WINDOWSEVENTHANDLER

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler for Custom Defined Events                                    //
    ///////////////////////////////////////////////////////////////////////////////
    #region CUSTOMEVENTHANDLER

    /// <summary>
    /// The <see cref="GazeTrackerAPI.CalibrationFinishedEvent"/> event handler.
    /// Is called when the calibration has finished and the result is set to
    /// the control. So here we show the control.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/>.</param>
    private void GazeTrackerApiClientCalibrationFinishedEvent(object sender, EventArgs e)
    {
      // Enable Record button
      ThreadSafe.EnableDisableButton(this.RecordButton, true);
    }

    /// <summary>
    /// The <see cref="GTExtendedData"/> event handler
    /// which is called whenever there is a new frame arrived.
    /// Sends the GazeDataChanged event to the recording module.
    /// </summary>
    /// <param name="data">The tracking data with the mapped gaze 
    /// coordinates in pixel units.</param>
    private void OnGTExtendedDataChanged(GTExtendedData data)
    {
      var gazeData = new GazeData
        {
          Time = data.TimeStamp,
          PupilDiaX = (float)data.PupilDiameterLeft,
          PupilDiaY = (float)data.PupilDiameterRight,

          // Calculate values between 0..1
          GazePosX = (float)(data.GazePositionX / this.presentationScreenSize.Width),
          GazePosY = (float)(data.GazePositionY / this.presentationScreenSize.Height)
        };

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
      this.presentationScreenSize = Document.ActiveDocument.PresentationSize;
      this.gazeTrackerApiClient.IsTrackingOnSecondaryScreen = Properties.Settings.Default.PresentationScreenMonitor == "Secondary";
    }

    #endregion //METHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER
    #endregion //HELPER
  }
}