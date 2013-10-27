// <copyright file="AleaTracker.cs" company="alea technologies">
// ******************************************************
// OGAMA - open gaze and mouse analyzer 
// Copyright (C) 2013 Dr. Adrian Voßkühler  
// ------------------------------------------------------------------------
// This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
// This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
// You should have received a copy of the GNU General Public License along with this program; if not, write to the Free Software Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
// **************************************************************
// </copyright>
// <author>Martin Werner</author>
// <email>martin.werner@alea-technologies.de</email>

namespace Ogama.Modules.Recording.AleaInterface
{
  using System;
  using System.ComponentModel;
  using System.Diagnostics;
  using System.Drawing;
  using System.IO;
  using System.Runtime.InteropServices;
  using System.Windows.Forms;
  using System.Xml.Serialization;
  using Alea.Api;
  using Microsoft.Win32;
  using Ogama.ExceptionHandling;
  using Ogama.Modules.Common.CustomEventArgs;
  using Ogama.Modules.Common.Tools;
  using Ogama.Modules.Recording.Dialogs;
  using Ogama.Modules.Recording.TrackerBase;

  /// <summary>
  /// This class implements the <see cref="ITracker"/> interface to represent 
  /// an OGAMA known eyetracker.
  /// It encapsulates a Alea http://www.alea-technologies.com eyetracker 
  /// which requirs API 1.1 and Intelligaze 1.2 or higher from alea technologies.
  /// It is tested with the IG30 series.
  /// </summary>
  public class AleaTracker : TrackerWithStatusControls
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
    /// This is the label on the calibration result panel
    /// to show the quality status of the calibration.
    /// </summary>
    private Label resultLabel;

    /// <summary>
    /// This is the customized tab page for the Alea tracker.
    /// </summary>
    private TabPage tabpage;

    /// <summary>
    /// Alea API object
    /// </summary>
    private EtApi api;

    /// <summary>
    /// local lock for GetCurrentTime-Method
    /// </summary>
    private object timeLock;

    /// <summary>
    /// Save last time stamp
    /// </summary>
    private long lastTimeStamp;

    /// <summary>
    /// True if Interface throws GazeDataChanged-Events, otherwise false
    /// </summary>
    private bool isRecording;

    /// <summary>
    /// Stopwatch for calculating the timestamp between two gazesamples
    /// </summary>
    private Stopwatch stopwatch;

    /// <summary>
    /// X-Screenresolution in Pixel. Value is valid after calibration
    /// </summary>
    private int resolutionX;

    /// <summary>
    /// Y-Screenresolution in Pixel. Value is valid after calibration
    /// </summary>
    private int resolutionY;

    /// <summary>
    /// The current Alea settings.
    /// </summary>
    private AleaSetting settings;

    /// <summary>
    /// The alea track status ActiveX object
    /// </summary>
    private AxAleaStatusControl aleaTrackStatus;

    /// <summary>
    /// Saves the track status dialog that can be shown
    /// to the subject before calibration or during
    /// tracking.
    /// </summary>
    private AleaTrackStatus dlgTrackStatus;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the AleaTracker class.
    /// </summary>
    /// <param name="aleaResultLabel">The label on the calibration result panel
    /// to show the quality status of the calibration.</param>
    /// <param name="aleaTabpage">The customized tab page for the Alea tracker.</param>
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
    /// named "ShowOnPresentationScreenButton" at the tab page of the Alea device.</param>
    /// <param name="trackerAcceptButton">The <see cref="Button"/>
    /// named "Accept" at the tab page of the Alea device.</param>
    /// <param name="trackerRecalibrateButton">The <see cref="Button"/>
    /// named "Recalibrate" at the tab page of the Alea device.</param>
    /// <param name="trackerConnectButton">The <see cref="Button"/>
    /// named "Connect" at the tab page of the Alea device.</param>
    /// <param name="trackerSubjectButton">The <see cref="Button"/>
    /// named "Subject" at the tab page of the Alea device.</param>
    /// <param name="trackerCalibrateButton">The <see cref="Button"/>
    /// named "Calibrate" at the tab page of the Alea device.</param>
    /// <param name="trackerRecordButton">The <see cref="Button"/>
    /// named "Record" at the tab page of the Alea device.</param>
    /// <param name="trackerSubjectNameTextBox">The <see cref="TextBox"/>
    /// which should contain the subject name at the tab page of the Alea device.</param>
    public AleaTracker(
      ref Label aleaResultLabel,
      TabPage aleaTabpage,
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
      Properties.Settings.Default.EyeTrackerSettingsPath + "AleaSetting.xml")
    {
      this.resultLabel = aleaResultLabel;
      this.tabpage = aleaTabpage;

      // Call the initialize methods of derived classes
      this.Initialize();
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Enumerations                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region ENUMS

    /// <summary>
    /// CalibrationDone Event
    /// </summary>
    public event EventHandler<CalibrationDoneEventArgs> CalibrationDone;

    /// <summary>
    /// API Calibration Done Event
    /// </summary>
    private static event CalibrationDoneDelegate APICalibrationDone;

    /// <summary>
    /// API RawDataReceived Event
    /// </summary>
    private static event RawDataDelegate APIRawDataReceived;

    #endregion ENUMS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets the current Alea settings.
    /// </summary>
    /// <value>A <see cref="AleaSetting"/> with the current tracker settings.</value>
    public AleaSetting Settings
    {
      get { return this.settings; }
    }

    /// <summary>
    /// Gets the connection status of the alea tracker
    /// </summary>
    public override bool IsConnected
    {
      get
      {
        bool isOpen;
        ApiError result = this.api.IsOpen(out isOpen);
        return isOpen;
      }
    }

    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Public methods                                                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region PUBLICMETHODS

    /// <summary>
    /// Checks if the alea tracker is available in the system.
    /// </summary>
    /// <param name="errorMessage">Out. A <see cref="String"/> with an error message.</param>
    /// <returns><strong>True</strong>, if Alea tracker with intelligaze
    /// is available in the system, otherwise <strong>false</strong></returns>
    public static TrackerStatus IsAvailable(out string errorMessage)
    {
      errorMessage = string.Empty;

      // Check intelligaze process
      if (!IsProcessOpen("IntelliGaze"))
      {
        const string FILENAME = "Intelligaze.exe";

        // Intelligaze not open. Try to open
        string fullpath = GetIntelligazePath();

        if (fullpath.Length == 0 || !new FileInfo(fullpath + "\\" + FILENAME).Exists)
        {
          errorMessage = "Can't find Intelligaze folder. Maybe Intelligaze is not installed or installation is corrupted." + Environment.NewLine + "Please reinstall Intelligaze.";
          return TrackerStatus.NotAvailable;
        }
      }

      // search ActiveX Control
      Type statusControlType = Type.GetTypeFromProgID("ALEA.HeadDisplayCtrl", false);

      if (statusControlType == null)
      {
        errorMessage = "The Alea ActiveX Control is not registered. Please reinstall Intelligaze.";
        return TrackerStatus.NotAvailable;
      }

      // do some further registry checks
      string aleaCLSID = '{' + statusControlType.GUID.ToString().ToLower() + '}';

      RegistryKey clsid = Registry.ClassesRoot.OpenSubKey("CLSID");
      string[] clsIDs = clsid.GetSubKeyNames();

      for (int i = 0; i < clsIDs.Length; i++)
      {
        if (clsIDs[i].ToLower() == aleaCLSID)
        {
          errorMessage = "Intelligaze found.";
          return TrackerStatus.Available;
        }
      }

      errorMessage = "The Alea ActiveX Control is not registered. Please reinstall Intelligaze.";
      return TrackerStatus.NotAvailable;
    }

    #region ITrackerInterfaceImplementation

    /// <summary>
    /// Connect to the alea system.
    /// </summary>
    /// <returns><strong>True</strong> if connection succeded, otherwise
    /// <strong>false</strong>.</returns>
    public override bool Connect()
    {
      // Check intelligaze process
      if (!IsProcessOpen("IntelliGaze"))
      {
        const string FILENAME = "Intelligaze.exe";

        // Intelligaze not open. Try to open
        string fullpath = GetIntelligazePath();

        if (fullpath.Length == 0 || !new FileInfo(fullpath + "\\" + FILENAME).Exists)
        {
          ConnectionFailedDialog dlg = new ConnectionFailedDialog();
          dlg.ErrorMessage = "Can't find Intelligaze folder. Maybe Intelligaze is not installed or installation is corrupted." + Environment.NewLine + "Please reinstall Intelligaze.";
          dlg.ShowDialog();
          return false;
        }

        // Start Intelligaze with background param
        Process process = new Process();
        process.StartInfo.WorkingDirectory = fullpath;
        process.StartInfo.FileName = FILENAME;
        process.StartInfo.Arguments = "background";
        process.StartInfo.UseShellExecute = true;
        if (!process.Start())
        {
          ConnectionFailedDialog dlg = new ConnectionFailedDialog();
          dlg.ErrorMessage = "Can't start Intelligaze Process.";
          dlg.ShowDialog();
          return false;
        }
      }

      bool isOpen;
      ApiError result = this.api.IsOpen(out isOpen);

      // API open?
      if (result != ApiError.NoError)
      {
        ConnectionFailedDialog dlg = new ConnectionFailedDialog();
        string errorMessage;
        this.api.GetLastError(out errorMessage);
        if (errorMessage.Length == 0)
        {
          errorMessage = "Intelligaze Error: Method IsOpen" + Environment.NewLine + result.ToString();
        }

        dlg.ErrorMessage = errorMessage;
        dlg.ShowDialog();
        this.CleanUp();

        return false;
      }

      if (!isOpen)
      {
        // API is not open ... try to open API
        Stopwatch timeoutStopwatch = new Stopwatch();

        // Timeout is 20 seconds
        timeoutStopwatch.Start();

        do
        {
          // Try to open API
          result = this.api.Open("(ogama)=2", this.Settings.ServerAddress, this.Settings.ServerPort, this.Settings.ClientAddress, this.Settings.ClientPort);
        }
        while (result != ApiError.NoError && timeoutStopwatch.ElapsedMilliseconds < 20000);
        timeoutStopwatch.Stop();

        if (result != ApiError.NoError)
        {
          ConnectionFailedDialog dlg = new ConnectionFailedDialog();
          string errorMessage;
          this.api.GetLastError(out errorMessage);
          if (errorMessage.Length == 0)
          {
            errorMessage = "Intelligaze Error: Can't open API." + Environment.NewLine + "Reason: " + result.ToString();
          }

          dlg.ErrorMessage = errorMessage;
          dlg.ShowDialog();
          this.CleanUp();

          return false;
        }
      }

      // Register Callbacks
      this.api.SetCalibrationDoneCB(Marshal.GetFunctionPointerForDelegate(APICalibrationDone).ToInt32(), IntPtr.Zero);
      this.api.SetRawDataCB(Marshal.GetFunctionPointerForDelegate(APIRawDataReceived).ToInt32(), IntPtr.Zero);

      // enable datastreaming ... important to do it here, otherwise user get mousecursor control
      this.api.DataStreaming(1);

      // hide status window
      this.api.HideStatusWindow();

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
        bool isOpen;

        ApiError result = this.api.IsOpen(out isOpen);

        if (result != ApiError.NoError)
        {
          ConnectionFailedDialog dlg = new ConnectionFailedDialog();
          string errorMessage;
          this.api.GetLastError(out errorMessage);
          if (errorMessage.Length == 0)
          {
            errorMessage = "Intelligaze Error: " + result.ToString() + ". Make sure Intelligaze is running.";
          }

          dlg.ErrorMessage = errorMessage;
          dlg.ShowDialog();
          this.CleanUp();

          return false;
        }

        // Connect the calibration procedure if necessary
        if (!isOpen)
        {
          // Try to Open API
          if (!this.Connect())
          {
            this.CleanUp();
            return false;
          }
        }

        result = this.api.PerformCalibration(this.Settings.NumCalibPoint, (PointLocationEnum)this.Settings.CalibArea, this.Settings.RandomizeCalibPointOrder, this.Settings.SlowMode, this.Settings.PlayAudioFeedback, (EyeTypeEnum)this.Settings.Eye, false, this.Settings.SkipBadPoints, true, this.Settings.CalibBackgroundColor.ToArgb(), this.Settings.CalibPointColor.ToArgb(), string.Empty);

        if (result != ApiError.NoError)
        {
          string errorMessage;
          this.api.GetLastError(out errorMessage);
          if (errorMessage.Length == 0)
          {
            errorMessage = "Calibration Error: " + result.ToString();
          }

          InformationDialog.Show(
          "Calibration error",
          errorMessage,
          false,
          MessageBoxIcon.Error);

          this.CleanUp();
          return false;
        }
      }
      catch (Exception ex)
      {
        InformationDialog.Show(
          "Calibration failed",
          "Alea calibration failed with the following message: " + Environment.NewLine + ex.Message,
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
      this.Stop();
      this.api.ExitServer();
      this.api.Close();

      base.CleanUp();
    }

    /// <summary>
    /// Start tracking.
    /// </summary>
    public override void Record()
    {
      if (!this.isRecording)
      {
        this.lastTimeStamp = -1;
        this.stopwatch.Stop();
        this.stopwatch.Reset();

        bool isOpen;
        ApiError result = this.api.IsOpen(out isOpen);

        if (result != ApiError.NoError)
        {
          string errorMessage;
          this.api.GetLastError(out errorMessage);
          if (errorMessage.Length == 0)
          {
            errorMessage = "Intelligaze Error: " + result.ToString() + "." + Environment.NewLine + "Make sure Intelligaze is running.";
          }

          InformationDialog.Show(
          "Record failed",
          "Alea Record failed with the following message: " + Environment.NewLine + errorMessage,
          false,
          MessageBoxIcon.Error);

          this.CleanUp();
          return;
        }

        if (!isOpen)
        {
          InformationDialog.Show(
          "Record failed",
          "Alea Record failed with the following message: " + Environment.NewLine + "Not Connected.",
          false,
          MessageBoxIcon.Error);

          this.CleanUp();
          return;
        }

        this.isRecording = true;
      }
    }

    /// <summary>
    /// Stops tracking.
    /// </summary>
    public override void Stop()
    {
      this.isRecording = false;
      this.stopwatch.Stop();

      // don't call this.api.DataStreaming(0) here to turn off datastreaming .. otherwise IntelliGaze take over control      
    }

    /// <summary>
    /// Method to return the current timing of the tracking system.
    /// </summary>
    /// <returns>A <see cref="Int64"/> with the time in milliseconds.</returns>
    public long GetCurrentTime()
    {
      long currentTime;

      // set lock for lastTimeStamp value
      lock (this.timeLock)
      {
        if (this.lastTimeStamp != -1)
        {
          currentTime = this.lastTimeStamp + this.stopwatch.ElapsedMilliseconds;
        }
        else
        {
          currentTime = -1;
        }
      }

      return currentTime;
    }

    /// <summary>
    /// Raises <see cref="AleaSettingsDialog"/> to change the settings
    /// for this interface.
    /// </summary>
    public override void ChangeSettings()
    {
      AleaSettingsDialog dlg = new AleaSettingsDialog();
      dlg.AleaSettings = this.Settings;

      if (dlg.ShowDialog() == DialogResult.OK)
      {
        this.settings = dlg.AleaSettings;
        this.SerializeSettings(this.Settings, this.SettingsFile);
      }
    }

    #endregion //ITrackerInterfaceImplementation

    #endregion //PUBLICMETHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Inherited methods                                                         //
    ///////////////////////////////////////////////////////////////////////////////
    #region OVERRIDES

    /// <summary>
    /// Overridden. Dispose the <see cref="Tracker"/> if applicable
    /// by a call to <see cref="ITracker.CleanUp()"/> and 
    /// removing the connected event handlers.
    /// </summary>
    public override void Dispose()
    {
      base.Dispose();
      this.CalibrationDone -= this.aleaInterface_CalibrationDone;
    }

    /// <summary>
    /// This method initializes the designer components for the
    /// alea interface tab page.    
    /// </summary>
    protected override void InitializeStatusControls()
    {
      var resources = new ComponentResourceManager(typeof(RecordModule));

      try
      {
        this.aleaTrackStatus = new AxAleaStatusControl();
      }
      catch (NullReferenceException)
      {
        // occurs if ActiveX-Control was not registered by IntelliGaze
        this.aleaTrackStatus = null;
      }

      if (this.aleaTrackStatus != null)
      {
        this.TrackStatusPanel.SuspendLayout();

        ((ISupportInitialize)this.aleaTrackStatus).BeginInit();

        // AleaTrackStatus
        this.aleaTrackStatus.Dock = DockStyle.Fill;
        this.aleaTrackStatus.Enabled = true;
        this.aleaTrackStatus.Location = new Point(0, 0);
        this.aleaTrackStatus.Name = "aleaTrackStatus";
        this.aleaTrackStatus.OcxState = (AxHost.State)resources.GetObject("aleaTrackStatus.OcxState");
        this.aleaTrackStatus.TabIndex = 0;

        try
        {
          this.TrackStatusPanel.Controls.Add(this.aleaTrackStatus);
        }
        catch (COMException)
        {
          this.TrackStatusPanel.Controls.Clear();
          throw;
        }

        ((ISupportInitialize)this.aleaTrackStatus).EndInit();

        this.TrackStatusPanel.ResumeLayout(false);
      }
    }

    /// <summary>
    /// Overridden <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="TrackerWithStatusControls.ShowOnSecondaryScreenButton"/>.
    /// Shows the track status object on presentation screen outside the Tabpage.
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

        this.dlgTrackStatus = new AleaTrackStatus();

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
    }

    /// <summary>
    /// Overridden <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="Tracker.CalibrateButton"/>.
    /// Calls the <see cref="ITracker.Calibrate(Boolean)"/> method
    /// for interface specific calibration.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/>.</param>
    protected override void BtnCalibrateClick(object sender, EventArgs e)
    {
      base.BtnCalibrateClick(sender, e);
      this.resultLabel.Text = "Not Set";
      this.CalibrationResultPanel.BackColor = Color.Transparent;
    }

    /// <summary>
    /// Sets up calibration procedure and the tracking client
    /// and wires the events. Reads settings from file.
    /// </summary>
    protected override void Initialize()
    {
      this.CalibrationDone += new EventHandler<CalibrationDoneEventArgs>(this.aleaInterface_CalibrationDone);

      this.isRecording = false;
      this.stopwatch = new Stopwatch();
      this.timeLock = new object();

      this.api = new EtApi();

      APICalibrationDone += new CalibrationDoneDelegate(this.AleaInterface_APICalibrationDone);
      APIRawDataReceived += new RawDataDelegate(this.AleaInterface_APIRawDataReceived);

      // Load alea tracker settings.
      if (File.Exists(this.SettingsFile))
      {
        this.settings = this.DeserializeSettings(this.SettingsFile);
      }
      else
      {
        this.settings = new AleaSetting();
        this.SerializeSettings(this.settings, this.SettingsFile);
      }

      base.Initialize();
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
        this.ShowOnSecondaryScreenButton.BackColor = Color.Transparent;
        this.ShowOnSecondaryScreenButton.Text = "Show on presentation screen";
        this.dlgTrackStatus.Close();
      }
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
    /// Calibration Done Event handler. Updates the CalibrationPlot-Panel
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/>.</param>
    private void aleaInterface_CalibrationDone(object sender, CalibrationDoneEventArgs e)
    {
      switch (e.Result)
      {
        case 0:
          this.resultLabel.Text = "Excellent";
          this.CalibrationResultPanel.BackColor = Color.Green;
          break;
        case 1:
          this.resultLabel.Text = "Good";
          this.CalibrationResultPanel.BackColor = Color.Green;
          break;
        case 2:
          this.resultLabel.Text = "Average";
          this.CalibrationResultPanel.BackColor = Color.Yellow;
          break;
        case 3:
          this.resultLabel.Text = "Poor";
          this.CalibrationResultPanel.BackColor = Color.Yellow;
          break;
        case 4:
          this.resultLabel.Text = "Very Poor";
          this.CalibrationResultPanel.BackColor = Color.Red;
          break;
        default:
          this.resultLabel.Text = "Invalid";
          this.CalibrationResultPanel.BackColor = Color.Red;
          break;
      }

      // Hide the track status and show the calibration plot.
      // this.HideTrackstatusWindow();
      this.ShowCalibPlot();
    }

    #endregion //WINDOWSEVENTHANDLER

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler for Custom Defined Events                                    //
    ///////////////////////////////////////////////////////////////////////////////
    #region CUSTOMEVENTHANDLER

    /// <summary>
    /// CalibrationDone Event
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A <see cref="CalibrationDoneEventArgs"/> with the event data.</param>
    private void OnCalibrationDone(object sender, CalibrationDoneEventArgs e)
    {
      if (this.CalibrationDone != null)
      {
        this.CalibrationDone(sender, e);
      }
    }

    /// <summary>
    /// CalibrationDone Event Handler of API. Throws a public CalibrationDone-Event
    /// </summary>
    /// <param name="data">Tracker RawData</param>
    /// <param name="improvmentSuggestion">can improve calibration</param>
    /// <param name="userData">Pointer to userdata</param>
    private void AleaInterface_APICalibrationDone(ref int data, bool improvmentSuggestion, IntPtr userData)
    {
      if (this.RecordModule.InvokeRequired)
      {
        this.RecordModule.BeginInvoke(new CalibrationDoneDelegate(this.AleaInterface_APICalibrationDone), new object[] { data, improvmentSuggestion, userData });
      }
      else
      {
        // Get Calibration Size
        int width, height;
        ApiError result = this.api.CalibrationSize(out width, out height);

        if (result != ApiError.NoError)
        {
          // raise CalibrationDone Event with -1 ... calibration fails with unknown error
          this.OnCalibrationDone(this, new CalibrationDoneEventArgs(-1));
        }
        else
        {
          // Set screenresolution
          this.resolutionX = width;
          this.resolutionY = height;

          // raise public CalibrationDone Event
          this.OnCalibrationDone(this, new CalibrationDoneEventArgs(data));
        }
      }
    }

    /// <summary>
    /// Raw Data Event Handler. Throws a GazeDataChanged-Event if isRecording is true
    /// </summary>
    /// <param name="data">Tracker RawData</param>
    /// <param name="userData">Pointer to user Data</param>
    private void AleaInterface_APIRawDataReceived(ref RawData data, IntPtr userData)
    {
        if (this.RecordModule.InvokeRequired)
        {
            this.RecordModule.BeginInvoke(new RawDataDelegate(this.AleaInterface_APIRawDataReceived), new object[] { data, userData });
        }
        else
        {
            if (this.isRecording && this.resolutionX != 0 && this.resolutionY != 0)
            {
                // Save current timestamp
                lock (this.timeLock)
                {
                    this.lastTimeStamp = data.timeStamp;

                    // Reset Stopwatch
                    this.stopwatch.Reset();

                    // Start stopwatch, if at least one timestamp is saved.
                    this.stopwatch.Start();
                }

                GazeData newGazeData = new GazeData();

                // Get gazeTimestamp in milliseconds.
                newGazeData.Time = this.lastTimeStamp;

                // Calculate values between 0..1
                newGazeData.GazePosX = (float)(data.intelliGazeX / (float)this.resolutionX);
                newGazeData.GazePosY = (float)(data.intelliGazeY / (float)this.resolutionY);

                // Set pupil diameter
                newGazeData.PupilDiaX = (float)data.leftEye.pupilDiameter;
                newGazeData.PupilDiaY = (float)data.rightEye.pupilDiameter;

                // raise event
                this.OnGazeDataChanged(new GazeDataChangedEventArgs(newGazeData));
            }
        }
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
    /// Searchs a specified process
    /// </summary>
    /// <param name="name">Name of process, without .exe or .dll</param>
    /// <returns>True if process is running, otherwise false</returns>
    private static bool IsProcessOpen(string name)
    {
      foreach (Process clsProcess in Process.GetProcesses())
      {
        if (clsProcess.ProcessName == name)
        {
          return true;
        }
      }

      return false;
    }

    /// <summary>
    /// Get the Path of Intelligaze installation
    /// </summary>
    /// <returns>Full Path of Intelligaze</returns>
    private static string GetIntelligazePath()
    {
      string personal = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
      string intelligazePathFile = personal + "\\alea_technologies_gmbh\\IntelliGaze\\IntelliGazePath.txt";

      // IntelliGazePath.txt exists?
      if (!File.Exists(intelligazePathFile))
      {
        return string.Empty;
      }
      else
      {
        // Read file
        StreamReader reader = new StreamReader(intelligazePathFile);
        string binaryPath = reader.ReadLine();
        reader.Close();
        return binaryPath;
      }
    }

    /// <summary>
    /// Deserializes the <see cref="AleaSetting"/> from the given xml file.
    /// </summary>
    /// <param name="filePath">Full file path to the xml settings file.</param>
    /// <returns>A <see cref="AleaSetting"/> object.</returns>
    private AleaSetting DeserializeSettings(string filePath)
    {
      AleaSetting settings = new AleaSetting();

      // Create an instance of the XmlSerializer class;
      // specify the type of object to be deserialized 
      XmlSerializer serializer = new XmlSerializer(typeof(AleaSetting));

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
        settings = (AleaSetting)serializer.Deserialize(fs);
        fs.Close();
      }
      catch (Exception ex)
      {
        InformationDialog.Show(
          "Error occured",
          "Deserialization of AleaSettings failed with the following message: " + Environment.NewLine + ex.Message,
          false,
          MessageBoxIcon.Error);
      }

      return settings;
    }

    /// <summary>
    /// Serializes the <see cref="AleaSetting"/> into the given file in a xml structure.
    /// </summary>
    /// <param name="settings">The <see cref="AleaSetting"/> object to serialize.</param>
    /// <param name="filePath">Full file path to the xml settings file.</param>
    private void SerializeSettings(AleaSetting settings, string filePath)
    {
      // Create an instance of the XmlSerializer class;
      // specify the type of object to serialize 
      XmlSerializer serializer = new XmlSerializer(typeof(AleaSetting));

      // Serialize the AleaSetting, and close the TextWriter.
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
          "Serialization of AleaSettings failed with the following message: " + Environment.NewLine + ex.Message,
          false,
          MessageBoxIcon.Error);
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