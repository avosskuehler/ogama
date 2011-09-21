// <copyright file="ITUGazeTracker.cs" company="FU Berlin">
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

namespace Ogama.Modules.Recording.ITU
{
  using System;
  using System.Windows.Forms;
  using DirectShowLib;
  using Ogama.ExceptionHandling;
  using Ogama.Modules.Common;

  using OgamaControls;

  /// <summary>
  /// This class implements the <see cref="TrackerWithStatusControls"/> class
  /// to represent an OGAMA known eyetracker.
  /// It encapsulates the ITU GazeTracker http://www.gazegroup.org 
  /// </summary>
  public class ITUGazeTracker : Tracker
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

    private TextBox statusTextBox;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the ITUGazeTracker class.
    /// <remarks>
    /// Note that the xml settings file is set, but not used,
    /// GazeTracker internally saves it state in another location.
    /// </remarks>
    /// </summary>
    /// <param name="owningRecordModule">The <see cref="RecordModule"/>
    /// form wich host the recorder.</param>
    /// <param name="trackerStatusTextBox">The <see cref="TextBox"/>
    /// to retreive status messages of the tracker.</param>
    /// <param name="trackerConnectButton">The <see cref="Button"/>
    /// named "Connect" at the tab page of the device.</param>
    /// <param name="trackerSubjectButton">The <see cref="Button"/>
    /// named "Subject" at the tab page of the device.</param>
    /// <param name="trackerCalibrateButton">The <see cref="Button"/>
    /// named "Calibrate" at the tab page of the device.</param>
    /// <param name="trackerRecordButton">The <see cref="Button"/>
    /// named "Record" at the tab page of the device.</param>
    /// <param name="trackerSubjectNameTextBox">The <see cref="TextBox"/>
    /// which should contain the subject name at the tab page of the device.</param>
    public ITUGazeTracker(
      RecordModule owningRecordModule,
      TextBox trackerStatusTextBox,
      Button trackerConnectButton,
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
      Properties.Settings.Default.EyeTrackerSettingsPath + "ITUGazeTrackerSetting.xml")
    {
      // Call the initialize methods of derived classes
      this.Initialize();
      this.statusTextBox = trackerStatusTextBox;
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
      get { return this.ItuClient.IsConnected; }
    }

    /// <summary>
    /// Gets or sets the client that contains the api connection to the ITU GazeTracker
    /// </summary>
    protected ITUClient ItuClient { private get; set; }

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
      return this.ItuClient.GetCurrentTime();
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
        if (!this.ItuClient.Connect())
        {
          return false;
        }

        this.statusTextBox.Text = "Connected";

        this.ItuClient.CalibrationFinished += this.ituClient_CalibrationFinishedEvent;
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
        this.ItuClient.Calibrate();
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

      this.statusTextBox.Text = "Calibrated";

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
        if (this.ItuClient != null)
        {
          this.ItuClient.Disconnect();
        }
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
        this.ItuClient.StartStreaming();
        this.statusTextBox.Text = "StartStreaming";
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
        if (this.ItuClient != null)
        {
          this.ItuClient.StopStreaming();
          this.statusTextBox.Text = "StopStreaming";
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
    /// Raises GazeTrackerUI SettingsWindow to change the settings
    /// for this interface.
    /// </summary>
    public override void ChangeSettings()
    {
      this.ItuClient.ChangeSettings();
    }

    /// <summary>
    /// Sets up calibration procedure and the tracking client
    /// and wires the events. Reads settings from file.
    /// </summary>
    protected override sealed void Initialize()
    {
      this.ItuClient = new ITUClient();
      this.ItuClient.GazeDataAvailable += this.ItuClientGazeDataAvailable;
      this.ItuClient.ErrorOccured += this.ItuClient_ErrorOccured;
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
    #endregion //WINDOWSEVENTHANDLER

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler for Custom Defined Events                                    //
    ///////////////////////////////////////////////////////////////////////////////
    #region CUSTOMEVENTHANDLER

    private void ItuClient_ErrorOccured(object sender, StringEventArgs e)
    {
      ThreadSafe.ThreadSafeSetText(this.statusTextBox, e.Param);
      if (e.Param == "Not connected")
      {
        this.ConnectButton.BackColor = System.Drawing.Color.Transparent;
        this.ConnectButton.Enabled = true;
      }
    }

    private void ituClient_CalibrationFinishedEvent(object sender, EventArgs e)
    {
      ThreadSafe.EnableDisableButton(this.RecordButton, true);
      ThreadSafe.ThreadSafeSetText(this.statusTextBox, "CalibrationFinished");
    }

    /// <summary>
    /// The <see cref="ItuClientGazeDataAvailable"/> event handler
    /// which is called whenever there is a new frame arrived.
    /// Sends the GazeDataChanged event to the recording module.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">The <see cref="GazeDataChangedEventArgs"/> with the event data
    /// which are the mapped gaze coordinates in pixel units.</param>
    private void ItuClientGazeDataAvailable(object sender, GazeDataChangedEventArgs e)
    {
      this.OnGazeDataChanged(e);
      this.statusTextBox.Text = string.Format("GazeDataChanged {0} {1} {2}", Environment.NewLine, e.Gazedata.GazePosX, e.Gazedata.PupilDiaX);
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
    #endregion //METHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER
    #endregion //HELPER
  }
}
