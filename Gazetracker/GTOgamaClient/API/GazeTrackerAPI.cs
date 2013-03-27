// <copyright file="GazeTrackerAPI.cs" company="FU Berlin">
// ******************************************************
// OgamaClient for ITU GazeTracker
// Copyright (C) 2013 Dr. Adrian Voßkühler  
// ------------------------------------------------------------------------
// This program is free software; you can redistribute it and/or modify it 
// under the terms of the GNU General Public License as published by the 
// Free Software Foundation; either version 3 of the License, 
// or (at your option) any later version.
// This program is distributed in the hope that it will be useful, 
// but WITHOUT ANY WARRANTY; without even the implied warranty of 
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU 
// General Public License for more details.
// You should have received a copy of the GNU General Public License 
// along with this program; if not, see http://www.gnu.org/licenses/.
// **************************************************************
// </copyright>
// <author>Adrian Voßkühler</author>
// <email>adrian@ogama.net</email>

namespace GTOgamaClient.API
{
  using System;
  using System.Diagnostics;
  using System.Windows;

  using GTOgamaClient.Controls;

  using GTApplication.CalibrationUI;
  using GTApplication.SettingsUI;
  using GTApplication.Tools;

  using GTLibrary;
  using GTLibrary.Logging;
  using GTLibrary.Utils;

  using GTCommons;
  using GTCommons.Enum;
  using GTCommons.Events;

  using Settings = GTSettings.Settings;

  /// <summary>
  /// This is the main API of the OGAMA client for the ITU GazeTracker.
  /// Its main purpose is to implement the functionality required for
  /// the ITracker interface of OGAMA.
  /// </summary>
  public class GazeTrackerAPI
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
    /// A windows forms version of the GTApplication VideoImageControl
    /// that displays the processed eye video.
    /// </summary>
    private EyeVideoControl eyeVideoControl;

    /// <summary>
    /// A windows forms version of the GTApplication VideoViewer
    /// that displays the eye video in native size.
    /// </summary>
    private TrackStatusControl eyeTrackStatus;

    /// <summary>
    /// Flag, indicating whether this API client is currently
    /// tracking gaze data.
    /// </summary>
    private bool isRunning;

    /// <summary>
    /// Saves the last valid time stamp;
    /// </summary>
    private long lastTime;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the GazeTrackerAPI class.
    /// </summary>
    public GazeTrackerAPI()
    {
      this.lastTime = -1;
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Enumerations                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region ENUMS

    /// <summary>
    /// The event that is raised whenever this class has got new data
    /// </summary>
    public event GTExtendedData.GTExtendedDataChangedEventHandler GTExtendedDataChanged;

    /// <summary>
    /// Event that is raised whenever a calibration process
    /// has been succesfully finished.
    /// </summary>
    public event EventHandler CalibrationFinishedEvent;

    /// <summary>
    /// Event. Raised whenever the client has catched an error.
    /// </summary>
    public event StringEventHandler ErrorOccured;

    #endregion ENUMS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets or sets a value indicating whether the the tracker is connected 
    /// to a device.
    /// </summary>
    public bool IsConnected { get; set; }

    /// <summary>
    /// Sets a value indicating whether the gazetracker
    /// should use secondary screen for calibration and tracking.
    /// </summary>
    public bool IsTrackingOnSecondaryScreen
    {
      set
      {
        Settings.Instance.Calibration.IsTrackingOnSecondaryScreen = value;
      }
    }

    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Public methods                                                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region PUBLICMETHODS

    /// <summary>
    /// Method to return the current timing of the tracking system.
    /// </summary>
    /// <returns>A <see cref="Int64"/> with the current time in milliseconds
    /// if the tracker has send a time, otherwise -1.</returns>
    public long GetCurrentTime()
    {
      return this.lastTime;
    }

    /// <summary>
    /// The implementation of this method connects the client, so that the
    /// system is ready for calibration.
    /// </summary>
    /// <returns><strong>True</strong> if succesful connected to tracker,
    /// otherwise <strong>false</strong>.</returns>
    public virtual bool Connect()
    {
      try
      {
        // Little fix for colorschema (must run before initializing)
        ComboBoxBackgroundColorFix.Initialize();

        // Register for special error messages
        ErrorLogger.TrackerError += this.OnTrackerError;

        // Load GTSettings
        Settings.Instance.LoadLatestConfiguration();

        // Camera initialization and start frame grabbing
        if (GTHardware.Camera.Instance.DeviceType != GTHardware.Camera.DeviceTypeEnum.None)
        {
          // If DirectShow camera, init using saved settings
          if (GTHardware.Camera.Instance.DeviceType == GTHardware.Camera.DeviceTypeEnum.DirectShow)
          {
            GTHardware.Camera.Instance.SetDirectShowCamera(
                Settings.Instance.Camera.DeviceNumber,
                Settings.Instance.Camera.DeviceMode);
          }
          else
          {
            GTHardware.Camera.Instance.Device.Initialize();
          }

          GTHardware.Camera.Instance.Device.Start();
        }
        else
        {
          // No camera detected, display message
          const string Error = "The GazeTracker was unable to connect a camera. \n" +
                               "Make sure that the device is connected and that the device drivers are installed. " +
                               "Verified configurations can be found in our forum located at http://forum.gazegroup.org";
          this.OnTrackerError(Error);
        }

        // Set this process to real-time priority
        Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.High;

        SettingsWindow.Instance.Title = "SettingsWindow"; // Just touch it..

        // Load settings and apply 
        this.InitSettings();

        // Video preview window (tracker visualizes image processing) 
        this.eyeVideoControl.Start();

        GTCommands.Instance.Calibration.OnAccepted += this.OnCalibrationAccepted;
        GTCommands.Instance.Calibration.OnStart += this.OnCalibrationStart;
        GTCommands.Instance.Calibration.OnRunning += this.OnCalibrationRunning;
        GTCommands.Instance.Calibration.OnPointStart += this.OnPointStart;
        GTCommands.Instance.Calibration.OnAbort += this.OnCalibrationAbort;
        GTCommands.Instance.Calibration.OnEnd += this.OnCalibrationEnd;

        this.IsConnected = true;
      }
      catch (Exception ex)
      {
        this.OnTrackerError(ex.Message);
        return false;
      }

      return true;
    }

    /// <summary>
    /// This method initializes custom components of the 
    /// implemented tracking device.
    /// </summary>
    /// <param name="clientEyeVideoConrol">The <see cref="EyeVideoControl"/>
    /// of the client where to display the eye video.</param>
    public void Initialize(EyeVideoControl clientEyeVideoConrol)
    {
      try
      {
        this.eyeVideoControl = clientEyeVideoConrol;
        this.eyeVideoControl.ShowInitialImage();
      }
      catch (Exception ex)
      {
        this.OnTrackerError(ex.Message);
      }
    }

    /// <summary>
    /// This method shows or hides a copy of the eye video on
    /// the current presentation screen in native resolution.
    /// </summary>
    /// <param name="show"><strong>True</strong> if control should be shown,
    /// otherwise<strong>false</strong></param>
    public void ShowOrHideTrackStatusOnPresentationScreen(bool show)
    {
      if (show)
      {
        // Create a new window to host a larger VideoViewer
        this.eyeTrackStatus = new TrackStatusControl();
        this.eyeTrackStatus.EyeVideoControl.IsNativeResolution = true;
        this.eyeTrackStatus.SetSizeAndLabels(
          Tracker.Instance.VideoWidth,
          Tracker.Instance.VideoHeight,
          Tracker.Instance.FPSVideo);

        this.eyeTrackStatus.Left = (int)(
          TrackingScreen.TrackingScreenLeft +
          TrackingScreen.TrackingScreenCenter.X - (this.eyeTrackStatus.Width / 2));
        this.eyeTrackStatus.Top = (int)(
          TrackingScreen.TrackingScreenTop +
          TrackingScreen.TrackingScreenCenter.Y - (this.eyeTrackStatus.Height / 2));

        this.eyeTrackStatus.Show();
      }
      else
      {
        if (this.eyeTrackStatus != null)
        {
          this.eyeTrackStatus.Close();
        }
      }
    }

    /// <summary>
    /// This method performs the calibration
    /// for the client, so that the
    /// system is ready for recording.
    /// </summary>
    public void Calibrate()
    {
      SettingsWindow.Instance.SaveSettings();
      GTCommands.Instance.Calibration.Start();
    }

    /// <summary>
    /// This method starts the recording
    /// for the specific hardware, so the
    /// system sends <see cref="GTExtendedDataChanged"/> events.
    /// </summary>
    public void Record()
    {
      if (this.isRunning)
      {
        return;
      }

      // Register listner for gazedata events
      Tracker.Instance.GTExtendedData.GTExtendedDataChanged += this.OnGTExtendedDataChanged;

      this.isRunning = true;
    }

    /// <summary>
    /// This method stops the recording
    /// for the specific hardware.
    /// </summary>
    public void Stop()
    {
      if (!this.isRunning)
      {
        return;
      }

      // Unregister events
      Tracker.Instance.GTExtendedData.GTExtendedDataChanged -= this.OnGTExtendedDataChanged;

      this.isRunning = false;
    }

    /// <summary>
    /// This method performs a clean up
    /// for the specific hardware, so that the
    /// system is ready for shut down.
    /// </summary>
    public void CleanUp()
    {
      // If video is detached (seperate window), stop updating images)
      if (this.eyeTrackStatus != null)
      {
        this.eyeTrackStatus.EyeVideoControl.Stop();
        this.eyeTrackStatus.Close();
      }

      // Save settings 
      SettingsWindow.Instance.SaveSettings();

      // Cleanup tracker & release camera
      Tracker.Instance.Cleanup();

      if (this.eyeVideoControl != null)
      {
        this.eyeVideoControl.ShowInitialImage();
        this.eyeVideoControl.Update();
      }

      this.IsConnected = false;
    }

    /// <summary>
    /// This method shows a hardware 
    /// system specific dialog to change its settings like
    /// sampling rate or connection properties. It should also
    /// provide a xml serialization possibility of the settings,
    /// so that the user can store and backup system settings in
    /// a separate file.
    /// </summary>
    public void ChangeSettings()
    {
      if (this.IsConnected)
      {
        this.ShowSetupWindow();
      }
    }

    /// <summary>
    /// Disposes the <see cref="Tracker"/> if applicable
    /// by a call to <see cref="CleanUp()"/> and 
    /// removing the connected event handlers.
    /// </summary>
    public void Dispose()
    {
      Tracker.Instance.GTExtendedData.GTExtendedDataChanged -= this.OnGTExtendedDataChanged;

      this.CleanUp();

      // Close all windows (including Visibility.Collapsed & Hidden)
      if (this.eyeTrackStatus != null)
      {
        this.eyeTrackStatus.Close();
      }

      SettingsWindow.Instance.Close();
    }

    #endregion //PUBLICMETHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Inherited methods                                                         //
    ///////////////////////////////////////////////////////////////////////////////
    #region OVERRIDES

    /// <summary>
    /// This virtual method sets the default gazetracker settings
    /// for a succesful ogama connection.
    /// </summary>
    protected virtual void InitSettings()
    {
      GTCommands.Instance.Camera.OnCameraChange += this.OnCameraChange;
      Settings.Instance.Processing.EyeMouseEnabled = false;
      Settings.Instance.Processing.EyeCrosshairEnabled = false;
      Settings.Instance.Visualization.VideoMode = VideoModeEnum.Normal;
      Settings.Instance.Network.TCPIPServerEnabled = false;
      Settings.Instance.Network.UDPServerEnabled = false;
      Settings.Instance.Network.UDPServerSendSmoothedData = false;
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

    /// <summary>
    /// Shows the calibration window and starts the calibration process
    /// in this window.
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">An empty <see cref="System.Windows.RoutedEventArgs"/></param>
    private void OnCalibrationStart(object sender, RoutedEventArgs e)
    {
      CalibrationWindow.Instance.Reset();
      CalibrationWindow.Instance.Show();
      CalibrationWindow.Instance.Start();
    }

    /// <summary>
    /// Starts the calibration procedure of the tracker.
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">An empty <see cref="System.Windows.RoutedEventArgs"/></param>
    private void OnCalibrationRunning(object sender, RoutedEventArgs e)
    {
      Tracker.Instance.CalibrationStart();
    }

    /// <summary>
    /// Closes the calibration window, accepts the calibration and
    /// ynchronously raises the <see cref="CalibrationFinishedEvent"/> event.
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">An empty <see cref="System.Windows.RoutedEventArgs"/></param>
    private void OnCalibrationAccepted(object sender, RoutedEventArgs e)
    {
      CalibrationWindow.Instance.Close();
      Tracker.Instance.CalibrationAccepted();
      if (this.CalibrationFinishedEvent != null)
      {
        this.CalibrationFinishedEvent(this, EventArgs.Empty);
      }
    }

    /// <summary>
    /// Updates the tracker calibration process with the new calibration point.
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">An empty <see cref="System.Windows.RoutedEventArgs"/></param>
    private void OnPointStart(object sender, RoutedEventArgs e)
    {
      var control = sender as CalibrationControl;
      if (control != null)
      {
        Tracker.Instance.CalibrationPointStart(control.CurrentPoint.Number, control.CurrentPoint.Point);
      }

      e.Handled = true;
    }

    /// <summary>
    /// Stops the calibration and closes the window.
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">An empty <see cref="System.Windows.RoutedEventArgs"/></param>
    private void OnCalibrationAbort(object sender, RoutedEventArgs e)
    {
      CalibrationWindow.Instance.Stop();
      Tracker.Instance.CalibrationAbort();
    }

    /// <summary>
    /// Stops the calibration process of the tracker.
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">An empty <see cref="System.Windows.RoutedEventArgs"/></param>
    private void OnCalibrationEnd(object sender, RoutedEventArgs e)
    {
      Tracker.Instance.CalibrationEnd();
    }

    /// <summary>
    /// Is called whenever new raw data is available,
    /// sends the <see cref="GTExtendedDataChanged"/> event.
    /// </summary>
    /// <param name="data">The gaze data.</param>
    private void OnGTExtendedDataChanged(GTExtendedData data)
    {
      this.lastTime = data.TimeStamp;

      if (this.GTExtendedDataChanged != null)
      {
        this.GTExtendedDataChanged(data);
      }
    }

    /// <summary>
    /// The event handler for the tracker error event.
    /// Just posts the error down the line to any client listeners.
    /// </summary>
    /// <param name="message">A string with the error message to send.</param>
    private void OnTrackerError(string message)
    {
      if (this.ErrorOccured != null)
      {
        this.ErrorOccured(this, new StringEventArgs(message));
      }
    }

    /// <summary>
    /// The event handler for the OnCameraChanged which creates
    /// a new camera for the tracker.
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">An empty <see cref="System.Windows.RoutedEventArgs"/></param>
    private void OnCameraChange(object sender, RoutedEventArgs e)
    {
      Tracker.Instance.SetCamera(
        Settings.Instance.Camera.DeviceNumber,
        Settings.Instance.Camera.DeviceMode);
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
    /// This method shows/hides an already existing settings window.
    /// </summary>
    private void ShowSetupWindow()
    {
      if (SettingsWindow.Instance.Visibility.Equals(Visibility.Collapsed))
      {
        SettingsWindow.Instance.Visibility = Visibility.Visible;
      }
      else
      {
        SettingsWindow.Instance.SaveSettings();
        SettingsWindow.Instance.Visibility = Visibility.Collapsed;
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
