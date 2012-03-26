// <copyright file="TobiiTracker.cs" company="FU Berlin">
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

#if TOBII

namespace Ogama.Modules.Recording.Tobii
{
  using System;
  using System.Drawing;
  using System.IO;
  using System.Runtime.InteropServices;
  using System.Threading;
  using System.Windows.Forms;
  using System.Xml.Serialization;
  using Microsoft.Win32;
  using Ogama.ExceptionHandling;
  using Ogama.Modules.Common;
  using Ogama.Modules.Recording.TrackerBase;

  using TetComp;

  /// <summary>
  /// This class implements the <see cref="TrackerWithStatusControls"/> class
  /// to represent an OGAMA known eyetracker.
  /// It encapsulates a TOBII http://www.tobii.com eyetracker 
  /// and is written with the SDK 2.0.1 from Tobii Systems.
  /// It is tested with the Tobii T60/T120 series.
  /// </summary>
  public class TobiiTracker : TrackerWithStatusControls
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
    /// The TetTrackStatus is a help tool to provide a real time display 
    /// of the tracking ability of the subject being eye tracked and 
    /// to make it easy to verify that the subject is advantageous 
    /// positioned. Preferably use it before the gaze tracking starts to 
    /// verify that the eyes of the subject are found. Note that 
    /// the subjects gaze point is not represented in any way, 
    /// only the position of the eyes in the eye tracker sensor are shown. 
    /// The TetTrackStatus is a COM object implemented as a Microsoft ActiveX
    /// control and it is internally using the functionality of the TetClient.
    /// </summary>
    /// <remarks>That is from the TobiiSDK documentation.</remarks>
    private ITetTrackStatus tetTrackStatus;

    /// <summary>
    /// It displays the result of a calibration, and can be used to provide
    /// information to decide if the calibration should be accepted, 
    /// rejected or improved. It is implemented as an ActiveX control.
    /// </summary>
    /// <remarks>That is from the TobiiSDK documentation.</remarks>
    private ITetCalibPlot tetCalibPlot;

    /// <summary>
    /// Handles the calls to the lower software abstraction layer. 
    /// Thus, it exposes the full functionality of the TETServer 
    /// and it is possible to build a complete eye tracking 
    /// application by using this object only.
    /// </summary>
    private TetClient tetClient;

    /// <summary>
    /// This object is used to calibrate the subject. 
    /// To do that it opens its own window to display
    /// an appropriate calibration stimulus.
    /// </summary>
    private TetCalibProc tetCalibProc;

    /// <summary>
    /// Saves the tobii settings.
    /// </summary>
    private TobiiSetting tobiiSettings;

    /// <summary>
    /// Saves the time stamp object.
    /// </summary>
    private TetTimeStamp timeStamp;

    /// <summary>
    /// Saves the track status dialog that can be shown
    /// to the subject before calibration or during
    /// tracking.
    /// </summary>
    private TobiiTrackStatus dlgTrackStatus;

    /// <summary>
    /// The tobii track status ActiveX object
    /// </summary>
    private AxTetComp.AxTetTrackStatus tobiiTrackStatus;

    /// <summary>
    /// The tobii calibration plot ActiveX object.
    /// </summary>
    private AxTetComp.AxTetCalibPlot tobiiCalibPlot;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the TobiiTracker class.
    /// Initializes COM objects and other.
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
    /// named "Accept" at the tab page of the Tobii device.</param>
    /// <param name="trackerRecalibrateButton">The <see cref="Button"/>
    /// named "Recalibrate" at the tab page of the Tobii device.</param>
    /// <param name="trackerConnectButton">The <see cref="Button"/>
    /// named "Connect" at the tab page of the Tobii device.</param>
    /// <param name="trackerSubjectButton">The <see cref="Button"/>
    /// named "Subject" at the tab page of the Tobii device.</param>
    /// <param name="trackerCalibrateButton">The <see cref="Button"/>
    /// named "Calibrate" at the tab page of the Tobii device.</param>
    /// <param name="trackerRecordButton">The <see cref="Button"/>
    /// named "Record" at the tab page of the Tobii device.</param>
    /// <param name="trackerSubjectNameTextBox">The <see cref="TextBox"/>
    /// which should contain the subject name at the tab page of the Tobii device.</param>
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
    /// Gets the current tobii settings.
    /// </summary>
    /// <value>A <see cref="TobiiSetting"/> with the current tracker settings.</value>
    public TobiiSetting Settings
    {
      get { return this.tobiiSettings; }
    }

    /// <summary>
    /// Gets the connection status of the tobii tracker
    /// </summary>
    public override bool IsConnected
    {
      get { return this.tetTrackStatus.IsConnected; }
    }

    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Public methods                                                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region PUBLICMETHODS

    /// <summary>
    /// Checks if the tobii tracker is available in the system.
    /// </summary>
    /// <param name="errorMessage">Out. A <see cref="String"/> with an error message.</param>
    /// <returns><strong>True</strong>, if Tobii tracker 
    /// is available in the system, otherwise <strong>false</strong></returns>
    public static bool IsAvailable(out string errorMessage)
    {
      errorMessage = string.Empty;

      RegistryKey clsid = Registry.ClassesRoot.OpenSubKey("CLSID");
      string[] clsIDs = clsid.GetSubKeyNames();
      string subkey = string.Empty;
      for (int i = 0; i < clsIDs.Length; i++)
      {
        subkey = clsIDs[i];
        if (subkey.Substring(0, 1) != "{")
        {
          continue;
        }

        RegistryKey cls = Registry.ClassesRoot.OpenSubKey("CLSID\\" + subkey + "\\InprocServer32");
        if (cls == null)
        {
          continue;
        }

        string value = cls.GetValue(string.Empty, string.Empty).ToString();
        if (value.Contains("tetcomp.dll") || value.Contains("TetComp.dll"))
        {
          return true;
        }
      }

      errorMessage = "The 'tetcomp.dll' is not installed. " +
        "Please install the Tobii Studio or Tobii SDK V 2.0.1.";
      return false;
    }

    /// <summary>
    /// Method to return the current timing of the tracking system.
    /// </summary>
    /// <returns>A <see cref="Int64"/> with the time in milliseconds.</returns>
    public long GetCurrentTime()
    {
      this.timeStamp = this.tetClient.GetTimeStamp();
      return this.timeStamp.second * 1000 + (long)(this.timeStamp.microsecond / 1000f);
    }

    #endregion //PUBLICMETHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Inherited methods                                                         //
    ///////////////////////////////////////////////////////////////////////////////
    #region OVERRIDES

    /// <summary>
    /// Connects the track status object to the tobii system.
    /// </summary>
    /// <returns><strong>True</strong> if connection succeded, otherwise
    /// <strong>false</strong>.</returns>
    public override bool Connect()
    {
      try
      {
        // Connect to the TET server if necessary
        if (!this.tetTrackStatus.IsConnected)
        {
          this.tetTrackStatus.Connect(
            this.tobiiSettings.TetServerAddress,
            this.tobiiSettings.TetServerPort);
        }

        // Start the track status meter
        if (!this.tetTrackStatus.IsTracking)
        {
          this.tetTrackStatus.Start();
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
        // Connect the calibration procedure if necessary
        if (!this.tetCalibProc.IsConnected)
        {
          this.tetCalibProc.Connect(this.tobiiSettings.TetServerAddress, this.tobiiSettings.TetServerPort);
        }

        // Reset calibration monitor, if changed after opening record module
        this.tetCalibProc.DisplayMonitor = PresentationScreen.GetPresentationScreen().DeviceName;

        // Always delete old calibration samples when new calibration point
        // positions are set
        this.tetCalibProc.CalibManager.UseIdealCalibGrid = true;

        if (isRecalibrating)
        {
          // Mark points from which the samples of the calibration in
          // use should be deleted (Delete bad samples from calibration
          // that are marked in the calibration plot)
          this.tetCalibProc.CalibManager.SetRemovePoints(this.tetCalibPlot.SelectedPoints);

          try
          {
            // Delete the samples from the above marked points.
            this.tetCalibProc.CalibManager.RemoveCalibrationPoints();
          }
          catch (Exception)
          {
            InformationDialog.Show(
              "Calibration error",
              "Calibration point selection failed. A new calibration is started.",
              false,
              MessageBoxIcon.Information);
            isRecalibrating = false;
          }

          // Now create the points for recalibration
          TetPointDArray recalibpoints = new TetPointDArray();

          // Add the default recalibration points
          recalibpoints = this.tetCalibProc.CalibManager.GetRecalibPoints();

          // Add the manually selected points.
          recalibpoints.AddArray(this.tetCalibPlot.SelectedPoints);

          // Set the points to recalibrate
          this.tetCalibProc.CalibManager.SetRecalibPoints(recalibpoints);
        }

        this.tetCalibProc.WindowTopmost = true;
        this.tetCalibProc.WindowVisible = true;
        this.tetCalibProc.StartCalibration(
          isRecalibrating ?
          TetCalibType.TetCalibType_Recalib : TetCalibType.TetCalibType_Calib,
          this.tobiiSettings.TetRandomizeCalibPointOrder);
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
    /// Clean up objects.
    /// </summary>
    public override void CleanUp()
    {
      try
      {
        this.Stop();

        if (this.tetTrackStatus.IsConnected)
        {
          if (this.tetTrackStatus.IsTracking)
          {
            this.tetTrackStatus.Stop();
          }

          this.tetTrackStatus.Disconnect();
        }

        if (this.tetCalibPlot.IsConnected)
        {
          this.tetCalibPlot.Disconnect();
        }

        if (this.tetCalibProc.IsConnected)
        {
          this.tetCalibProc.Disconnect();
        }

        if (this.tetClient.IsConnected)
        {
          this.tetClient.Disconnect();
        }
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
    /// Start tracking.
    /// </summary>
    public override void Record()
    {
      try
      {
        // Connect to the TET server if necessary
        if (!this.tetClient.IsConnected)
        {
          this.tetClient.Connect(
            this.tobiiSettings.TetServerAddress,
            this.tobiiSettings.TetServerPort,
            TetSynchronizationMode.TetSynchronizationMode_Local);
        }

        if (!this.tetClient.IsTracking)
        {
          // Start tracking gaze data
          this.tetClient.StartTracking();
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
        if (this.tetCalibProc.IsConnected)
        {
          if (this.tetCalibProc.IsCalibrating)
          {
            this.tetCalibProc.InterruptCalibration();
          }
        }

        if (this.tetClient.IsConnected)
        {
          if (this.tetClient.IsTracking)
          {
            this.tetClient.StopTracking();
          }
        }
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

    /// <summary>
    /// Raises <see cref="TobiiSettingsDialog"/> to change the settings
    /// for this interface.
    /// </summary>
    public override void ChangeSettings()
    {
      TobiiSettingsDialog dlg = new TobiiSettingsDialog();
      dlg.TobiiSettings = this.tobiiSettings;
      if (dlg.ShowDialog() == DialogResult.OK)
      {
        this.tobiiSettings = dlg.TobiiSettings;
        this.UpdateSettings();
        this.SerializeSettings(this.tobiiSettings, this.SettingsFile);
      }
    }

    /// <summary>
    /// Sets up calibration procedure and the tracking client
    /// and wires the events. Reads settings from file.
    /// </summary>
    protected override void Initialize()
    {
      // Set up the TET client object and it's events
      this.tetClient = new TetClientClass();
      this.tetClient.GazeDataDelivery = TetGazeDataDelivery.TetGazeDataDelivery_RealTime;
      _ITetClientEvents_Event tetClientEvents = (_ITetClientEvents_Event)this.tetClient;
      tetClientEvents.OnTrackingStarted += new _ITetClientEvents_OnTrackingStartedEventHandler(this.tetClientEvents_OnTrackingStarted);
      tetClientEvents.OnTrackingStopped += new _ITetClientEvents_OnTrackingStoppedEventHandler(this.tetClientEvents_OnTrackingStopped);
      tetClientEvents.OnGazeData += new _ITetClientEvents_OnGazeDataEventHandler(this.tetClientEvents_OnGazeData);

      // Set up the calibration procedure object and it's events
      this.tetCalibProc = new TetCalibProcClass();
      _ITetCalibProcEvents_Event tetCalibProcEvents = (_ITetCalibProcEvents_Event)this.tetCalibProc;
      tetCalibProcEvents.OnCalibrationEnd += new _ITetCalibProcEvents_OnCalibrationEndEventHandler(this.tetCalibProcEvents_OnCalibrationEnd);
      tetCalibProcEvents.OnKeyDown += new _ITetCalibProcEvents_OnKeyDownEventHandler(this.tetCalibProcEvents_OnKeyDown);

      // Initiate window properties 
      this.tetCalibProc.DisplayMonitor = PresentationScreen.GetPresentationScreen().DeviceName;

      this.timeStamp = new TetTimeStamp();

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
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="TrackerWithStatusControls.ShowOnSecondaryScreenButton"/>.
    /// Shows a new track status object in a new thread.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/>.</param>
    protected override void BtnShowOnPresentationScreenClick(object sender, EventArgs e)
    {
#if TOBII
      if (this.ShowOnSecondaryScreenButton.Text.Contains("Show"))
      {
        // Should show TrackStatusDlg
        if (this.dlgTrackStatus != null)
        {
          this.dlgTrackStatus.Dispose();
        }

        this.dlgTrackStatus = new TobiiTrackStatus(this.Settings);

        Rectangle presentationBounds = PresentationScreen.GetPresentationWorkingArea();

        this.dlgTrackStatus.Location = new Point(
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
#endif
    }

    /// <summary>
    /// Overridden.
    /// Check visibility of the track status window before starting to record.
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

    /// <summary>
    /// This method initializes the designer components for the
    /// tobii interface tab page.
    /// This is from the visual studio designer removed, because it crashes,
    /// when tobii sdk dlls are not installed on the target computer.
    /// </summary>
    protected override void InitializeStatusControls()
    {
#if TOBII
      if (this.tobiiTrackStatus == null && this.tobiiCalibPlot == null)
      {
        System.ComponentModel.ComponentResourceManager resources =
          new System.ComponentModel.ComponentResourceManager(typeof(RecordModule));

        this.tobiiTrackStatus = new AxTetComp.AxTetTrackStatus();
        this.tobiiCalibPlot = new AxTetComp.AxTetCalibPlot();

        ((System.ComponentModel.ISupportInitialize)this.tobiiTrackStatus).BeginInit();
        ((System.ComponentModel.ISupportInitialize)this.tobiiCalibPlot).BeginInit();

        // TobiiTrackStatus
        this.tobiiTrackStatus.Dock = System.Windows.Forms.DockStyle.Fill;
        this.tobiiTrackStatus.Enabled = true;
        this.tobiiTrackStatus.Location = new System.Drawing.Point(0, 0);
        this.tobiiTrackStatus.Name = "tobiiTrackStatus";
        this.tobiiTrackStatus.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("tobiiTrackStatus.OcxState");
        this.tobiiTrackStatus.Size = new System.Drawing.Size(190, 54);
        this.tobiiTrackStatus.TabIndex = 0;

        // TobiiCalibPlot
        this.tobiiCalibPlot.Dock = System.Windows.Forms.DockStyle.Fill;
        this.tobiiCalibPlot.Enabled = true;
        this.tobiiCalibPlot.Location = new System.Drawing.Point(0, 0);
        this.tobiiCalibPlot.Name = "tobiiCalibPlot";
        this.tobiiCalibPlot.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("tobiiCalibPlot.OcxState");
        this.tobiiCalibPlot.Size = new System.Drawing.Size(190, 54);
        this.tobiiCalibPlot.TabIndex = 0;

        ((System.ComponentModel.ISupportInitialize)this.tobiiTrackStatus).EndInit();
        ((System.ComponentModel.ISupportInitialize)this.tobiiCalibPlot).EndInit();

        try
        {
          this.TrackStatusPanel.Controls.Add(this.tobiiTrackStatus);
          this.CalibrationResultPanel.Controls.Add(this.tobiiCalibPlot);

          this.ShowCalibPlot();

          // Retreive underlying references to ActiveX controls
          this.tetTrackStatus = (ITetTrackStatus)this.tobiiTrackStatus.GetOcx();
          this.tetCalibPlot = (ITetCalibPlot)this.tobiiCalibPlot.GetOcx();
          this.tetCalibPlot.AllowMouseInteraction = true;

          this.ShowTrackStatus();
        }
        catch (COMException)
        {
          this.TrackStatusPanel.Controls.Clear();
          this.CalibrationResultPanel.Controls.Clear();
          throw;
        }
      }
#endif
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

    #region TrackingEvents

    /// <summary>
    /// <see cref="TetCalibProcClass.OnCalibrationEnd"/> event handler for
    /// the <see cref="tetCalibProc"/> <see cref="TetCalibProcClass"/>
    /// Gets fired when the calibration has ended,
    /// so hide the calibration window 
    /// and update the calibration plot
    /// </summary>
    /// <param name="result">Result. Zero (ITF_S_OK) for success or specific error code.</param>
    private void tetCalibProcEvents_OnCalibrationEnd(int result)
    {
      this.tetCalibProc.WindowVisible = false;
      this.tetCalibPlot.SelectedPoints = new TetPointDArray();

      // Update the calibration plot.
      this.UpdateCalibrationPlot();

      // Hide the track status and show the calibration plot.
      this.ShowCalibPlot();
    }

    /// <summary>
    /// <see cref="TetCalibProcClass.OnKeyDown"/> event handler for
    /// the <see cref="tetCalibProc"/> <see cref="TetCalibProcClass"/>
    /// Interrupt the calibration on key events.
    /// The event is fired when the calibration window gets the Windows message
    /// KeyDown. The parameter passed along with the event is the virtual-key 
    /// code of the key pressed.
    /// </summary>
    /// <param name="virtualKeyCode">The virtual-key code of the key pressed.
    /// The code is actual keyboard key identifiers, but includes also other 
    /// "virtual" elements such as the three mouse buttons. 
    /// It does not change when modifier keys (Ctrl, Alt, Shift, etc.) 
    /// are held, e.g. the “1” key has the same virtual-key code when ‘1’
    /// or ‘!’ is pressed.</param>
    private void tetCalibProcEvents_OnKeyDown(int virtualKeyCode)
    {
      if (this.tetCalibProc.IsCalibrating)
      {
        // Will trigger OnCalibrationEnd
        this.tetCalibProc.InterruptCalibration();
      }
    }

    /// <summary>
    /// OnTrackingStarted event handler for
    /// the <see cref="tetClient"/> <see cref="TetClient"/>
    /// Fired when the tracking was successfully started by the StartTracking method.
    /// </summary>
    private void tetClientEvents_OnTrackingStarted()
    {
    }

    /// <summary>
    /// OnTrackingStopped event handler for
    /// the <see cref="tetClient"/> <see cref="TetClient"/>
    /// The event is fired when tracking stops, 
    /// either as a result of calling the method StopTracking 
    /// or due to an error. The argument tells something 
    /// about the reason of why tracking was stopped.
    /// </summary>
    /// <param name="hr">Result. Zero (ITF_S_OK) for success 
    /// or specific error code.</param>
    private void tetClientEvents_OnTrackingStopped(int hr)
    {
      if (hr != (int)TetHResults.ITF_S_OK)
      {
        InformationDialog.Show(
          "Error",
          string.Format("Error {0} occured while tracking.", hr),
          false,
          MessageBoxIcon.Error);
      }

      while (this.tetClient.GetNumPendingPostGazeData() > 0)
      {
        Application.DoEvents();
      }
    }

    /// <summary>
    /// OnGazeData event handler for
    /// the <see cref="tetClient"/> <see cref="TetClient"/>
    /// This event fires whenever there are new gaze data 
    /// to receive and the GazeDataDelivery property 
    /// is set to TetGazeDataDelivery_RealTime.
    /// It converts the interface internal gaze structure into
    /// a OGAMA readable format and fires the <see cref="Tracker.OnGazeDataChanged"/>
    /// event to the recorder.
    /// </summary>
    /// <param name="gazeData">The gaze data.</param>
    private void tetClientEvents_OnGazeData(ref TetGazeData gazeData)
    {
      GazeData newGazeData = new GazeData();

      // Convert Tobii gazestamp in milliseconds.
      newGazeData.Time = gazeData.timestamp_sec * 1000
        + (int)(gazeData.timestamp_microsec / 1000f);

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
      if (gazeData.validity_lefteye == 0 && gazeData.validity_righteye == 0)
      {
        // Let the x, y and distance be the right and left eye average
        newGazeData.GazePosX = (gazeData.x_gazepos_lefteye + gazeData.x_gazepos_righteye) / 2;
        newGazeData.GazePosY = (gazeData.y_gazepos_lefteye + gazeData.y_gazepos_righteye) / 2;
        newGazeData.PupilDiaX = gazeData.diameter_pupil_lefteye;
        newGazeData.PupilDiaY = gazeData.diameter_pupil_righteye;
      }
      else if (gazeData.validity_lefteye == 4 && gazeData.validity_righteye == 4)
      {
        newGazeData.GazePosX = 0;
        newGazeData.GazePosY = 0;
        newGazeData.PupilDiaX = 0;
        newGazeData.PupilDiaY = 0;
      }
      else if (gazeData.validity_lefteye == 2 && gazeData.validity_righteye == 2)
      {
        newGazeData.GazePosX = 0;
        newGazeData.GazePosY = 0;
        newGazeData.PupilDiaX = 0;
        newGazeData.PupilDiaY = 0;
      }
      else if (gazeData.validity_lefteye == 1 && gazeData.validity_righteye == 3)
      {
        newGazeData.GazePosX = gazeData.x_gazepos_lefteye;
        newGazeData.GazePosY = gazeData.y_gazepos_lefteye;
        newGazeData.PupilDiaX = gazeData.diameter_pupil_lefteye;
        newGazeData.PupilDiaY = null;
      }
      else if (gazeData.validity_lefteye == 3 && gazeData.validity_righteye == 1)
      {
        newGazeData.GazePosX = gazeData.x_gazepos_righteye;
        newGazeData.GazePosY = gazeData.y_gazepos_righteye;
        newGazeData.PupilDiaX = null;
        newGazeData.PupilDiaY = gazeData.diameter_pupil_righteye;
      }
      else if (gazeData.validity_lefteye == 0 && gazeData.validity_righteye == 4)
      {
        newGazeData.GazePosX = gazeData.x_gazepos_lefteye;
        newGazeData.GazePosY = gazeData.y_gazepos_lefteye;
        newGazeData.PupilDiaX = gazeData.diameter_pupil_lefteye;
        newGazeData.PupilDiaY = null;
      }
      else if (gazeData.validity_lefteye == 4 && gazeData.validity_righteye == 0)
      {
        newGazeData.GazePosX = gazeData.x_gazepos_righteye;
        newGazeData.GazePosY = gazeData.y_gazepos_righteye;
        newGazeData.PupilDiaX = null;
        newGazeData.PupilDiaY = gazeData.diameter_pupil_righteye;
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

    #endregion //TrackingEvents

    #endregion //CUSTOMEVENTHANDLER

    #endregion //EVENTS

    ///////////////////////////////////////////////////////////////////////////////
    // Methods and Eventhandling for Background tasks                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region BACKGROUNDWORKER

    /// <summary>
    /// The user track status <see cref="Thread"/> DoWork event handler.
    /// Shows a TobiiTrackStatus form 
    /// at the center of the presentation screen.
    /// This allows correction of subject seating with real time feedback.
    /// </summary>
    /// <param name="data">A <see cref="object"/> with the  tobii settings.</param>
    private void TrackStatusThread_DoWork(object data)
    {
#if TOBII
      // Cast thread data.
      TobiiSetting setting = (TobiiSetting)data;

      TobiiTrackStatus dlgTrackStatus = new TobiiTrackStatus(setting);

      Rectangle presentationBounds = PresentationScreen.GetPresentationWorkingArea();

      this.dlgTrackStatus.Location = new Point(
        presentationBounds.Left + presentationBounds.Width / 2 - this.dlgTrackStatus.Width / 2,
        presentationBounds.Top + presentationBounds.Height / 2 - this.dlgTrackStatus.Height / 2);

      dlgTrackStatus.ShowDialog();
#endif
    }

    #endregion //BACKGROUNDWORKER

    ///////////////////////////////////////////////////////////////////////////////
    // Methods for doing main class job                                          //
    ///////////////////////////////////////////////////////////////////////////////
    #region METHODS

    /// <summary>
    /// Update the <see cref="tetCalibProc"/> object with the new
    /// settings.
    /// </summary>
    private void UpdateSettings()
    {
      this.tetCalibProc.PointColor = (uint)ColorTranslator.ToOle(this.tobiiSettings.TetCalibPointColor);
      this.tetCalibProc.PointSize = this.tobiiSettings.TetCalibPointSizes;
      this.tetCalibProc.PointSpeed = this.tobiiSettings.TetCalibPointSpeeds;
      this.tetCalibProc.NumPoints = this.tobiiSettings.TetNumCalibPoint;
      this.tetCalibProc.BackgroundColor = (uint)ColorTranslator.ToOle(this.tobiiSettings.TetCalibBackgroundColor);
    }

    /// <summary>
    /// Deserializes the <see cref="TobiiSetting"/> from the given xml file.
    /// </summary>
    /// <param name="filePath">Full file path to the xml settings file.</param>
    /// <returns>A <see cref="TobiiSetting"/> object.</returns>
    private TobiiSetting DeserializeSettings(string filePath)
    {
      TobiiSetting settings = new TobiiSetting();

      // Create an instance of the XmlSerializer class;
      // specify the type of object to be deserialized 
      XmlSerializer serializer = new XmlSerializer(typeof(TobiiSetting));

      // If the XML document has been altered with unknown 
      // nodes or attributes, handle them with the 
      // UnknownNode and UnknownAttribute events.
      serializer.UnknownNode += new XmlNodeEventHandler(this.SerializerUnknownNode);
      serializer.UnknownAttribute += new XmlAttributeEventHandler(this.SerializerUnknownAttribute);

      try
      {
        // A FileStream is needed to read the XML document.
        FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);

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
    /// Serializes the <see cref="TobiiSetting"/> into the given file in a xml structure.
    /// </summary>
    /// <param name="settings">The <see cref="TobiiSetting"/> object to serialize.</param>
    /// <param name="filePath">Full file path to the xml settings file.</param>
    private void SerializeSettings(TobiiSetting settings, string filePath)
    {
      // Create an instance of the XmlSerializer class;
      // specify the type of object to serialize 
      XmlSerializer serializer = new XmlSerializer(typeof(TobiiSetting));

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
    /// Updates the calibration plot object with data from last calibration.
    /// </summary>
    private void UpdateCalibrationPlot()
    {
      try
      {
        if (!this.tetCalibPlot.IsConnected)
        {
          this.tetCalibPlot.Connect(this.tobiiSettings.TetServerAddress, this.tobiiSettings.TetServerPort);

          // Will use the currently stored calibration data
          this.tetCalibPlot.SetData(null);
        }

        this.tetCalibPlot.UpdateData();
      }
      catch (Exception ex)
      {
        InformationDialog.Show(
          "Update Calibration failed.",
          "Tobii update of calibration plot failed with the following message: " + Environment.NewLine + ex.Message,
          false,
          MessageBoxIcon.Error);
        this.CleanUp();
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

#endif