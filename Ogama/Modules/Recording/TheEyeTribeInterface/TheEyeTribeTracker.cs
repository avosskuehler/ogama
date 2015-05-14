// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TheEyeTribeTracker.cs" company="Freie Universität Berlin">
//   OGAMA - open gaze and mouse analyzer 
//   Copyright (C) 2014 Dr. Adrian Voßkühler  
//   Licensed under GPL V3
// </copyright>
// <author>Adrian Voßkühler</author>
// <email>adrian@ogama.net</email>
// <summary>
//   This class implements the <see cref="TrackerWithStatusControls" /> class
//   to represent an OGAMA known eyetracker.
//   It encapsulates a http://www.theEyeTribe.com eyetracker.
//   It is tested with the TheEyeTribe device.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Ogama.Modules.Recording.TheEyeTribeInterface
{
  using System;
  using System.Diagnostics;
  using System.Drawing;
  using System.IO;
  using System.Threading;
  using System.Windows.Forms;
  using System.Windows.Forms.Integration;
  using System.Xml.Serialization;

  using Microsoft.Win32;

  using Ogama.ExceptionHandling;
  using Ogama.Modules.Common.CustomEventArgs;
  using Ogama.Modules.Common.Tools;
  using Ogama.Modules.Recording.Dialogs;
  using Ogama.Modules.Recording.TrackerBase;

  using TETControls.Calibration;
  using TETControls.TrackBox;

  using TETCSharpClient;

  using GazeData = Ogama.Modules.Recording.GazeData;

  /// <summary>
  ///   This class implements the <see cref="TrackerWithStatusControls" /> class
  ///   to represent an OGAMA known eyetracker.
  ///   It encapsulates a http://www.theEyeTribe.com eyetracker.
  ///   It is tested with the TheEyeTribe device.
  /// </summary>
  public class TheEyeTribeTracker : TrackerWithStatusControls, IGazeListener, IConnectionStateListener
  {
    #region Fields

    /// <summary>
    ///   Saves the track status dialog that can be shown
    ///   to the subject before calibration or during
    ///   tracking.
    /// </summary>
    private TheEyeTribeTrackStatusDialog dlgTrackStatus;

    /// <summary>
    ///   The is tracking.
    /// </summary>
    private bool isTracking;

    /// <summary>
    ///   It displays the result of a calibration, and can be used to provide
    ///   information to decide if the calibration should be accepted,
    ///   or redone.
    /// </summary>
    private TheEyeTribeCalibrationResultPanel theEyeTribeCalibrationResultPanel;

    /// <summary>
    /// A WPF control that displays the track status for the
    /// eye tribe tracking device.
    /// </summary>
    private TrackBoxStatus eyeTribeTrackStatus;

    /// <summary>
    /// The container for the track status control
    /// </summary>
    private ElementHost wpfContainer;

    /// <summary>
    ///   Saves the theEyeTribe settings.
    /// </summary>
    private TheEyeTribeSetting theEyeTribeSettings;

    /// <summary>
    ///   Saves the sample time of the last received gaze sample.
    /// </summary>
    private long lastTime;

    /// <summary>
    /// Stores the server process that was started from within ogama.
    /// </summary>
    private Process eyeTribeServerProcess;

    #endregion

    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the TheEyeTribeTracker class.
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
    ///   named "ShowOnPresentationScreenButton" at the tab page of the TheEyeTribe device.
    /// </param>
    /// <param name="trackerAcceptButton">
    /// The <see cref="Button"/>
    ///   named "Accept" at the tab page of the TheEyeTribe device.
    /// </param>
    /// <param name="trackerRecalibrateButton">
    /// The <see cref="Button"/>
    ///   named "Recalibrate" at the tab page of the TheEyeTribe device.
    /// </param>
    /// <param name="trackerConnectButton">
    /// The <see cref="Button"/>
    ///   named "Connect" at the tab page of the TheEyeTribe device.
    /// </param>
    /// <param name="trackerSubjectButton">
    /// The <see cref="Button"/>
    ///   named "Subject" at the tab page of the TheEyeTribe device.
    /// </param>
    /// <param name="trackerCalibrateButton">
    /// The <see cref="Button"/>
    ///   named "Calibrate" at the tab page of the TheEyeTribe device.
    /// </param>
    /// <param name="trackerRecordButton">
    /// The <see cref="Button"/>
    ///   named "Record" at the tab page of the TheEyeTribe device.
    /// </param>
    /// <param name="trackerSubjectNameTextBox">
    /// The <see cref="TextBox"/>
    ///   which should contain the subject name at the tab page of the TheEyeTribe device.
    /// </param>
    public TheEyeTribeTracker(
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
        Properties.Settings.Default.EyeTrackerSettingsPath + "TheEyeTribeSetting.xml")
    {
      // Call the initialize methods of derived classes
      this.Initialize();
    }

    #endregion

    #region Public Properties

    /// <summary>
    ///   Gets the connection status of the theEyeTribe tracker
    /// </summary>
    public override bool IsConnected
    {
      get
      {
        return GazeManager.Instance.IsActivated;
      }
    }

    /// <summary>
    ///   Gets the current theEyeTribe  settings.
    /// </summary>
    /// <value>A <see cref="TheEyeTribeSetting" /> with the current tracker settings.</value>
    public TheEyeTribeSetting Settings
    {
      get
      {
        return this.theEyeTribeSettings;
      }
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// Checks if the theEyeTribe tracker is available in the system.
    /// </summary>
    /// <param name="errorMessage">
    /// Out. A <see cref="string"/> with an error message.
    /// </param>
    /// <returns>
    /// <strong>True</strong>, if TheEyeTribe tracker
    ///   is available in the system, otherwise <strong>false</strong>
    /// </returns>
    public static TrackerStatus IsAvailable(out string errorMessage)
    {
      RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\EyeTribe\EyeTribe Service", false);
      if (key != null)
      {
        errorMessage = "The EyeTribe server has been found on the system. ";
        return TrackerStatus.Available;
      }

      errorMessage = "No EyeTribe server has been found on the system. ";
      return TrackerStatus.NotAvailable;
    }

    /// <summary>
    /// Converts a System Drawing Color into a System Windows Media Color.
    /// </summary>
    /// <param name="color">The color to be converted.</param>
    /// <returns>A System.Windows.Media.Color that represents the winforms color.</returns>
    public static System.Windows.Media.Color ToMediaColor(Color color)
    {
      return System.Windows.Media.Color.FromArgb(color.A, color.R, color.G, color.B);
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
        // Should hide TrackStatusDlg
        if (this.dlgTrackStatus != null)
        {
          this.ShowOnSecondaryScreenButton.BackColor = Color.Transparent;
          this.ShowOnSecondaryScreenButton.Text = "Show on presentation screen";
          this.dlgTrackStatus.Close();
        }

        var pointCount = 9;

        switch (this.theEyeTribeSettings.CalibrationPointCount)
        {
          case TheEyeTribeCalibrationPoints.Nine:
            pointCount = 9;
            break;
          case TheEyeTribeCalibrationPoints.Twelve:
            pointCount = 12;
            break;
          case TheEyeTribeCalibrationPoints.Sixteen:
            pointCount = 16;
            break;
        }

        // Start a new calibration procedure
        var calRunner = new CalibrationRunner(
          PresentationScreen.GetPresentationScreen(),
          PresentationScreen.GetPresentationBounds().Size,
          pointCount);
        calRunner.BackgroundColor = new System.Windows.Media.SolidColorBrush(ToMediaColor(this.theEyeTribeSettings.CalibrationBackgroundColor));
        calRunner.HelpVisibility = this.theEyeTribeSettings.DisplayHelp ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;
        calRunner.PointColor = new System.Windows.Media.SolidColorBrush(ToMediaColor(this.theEyeTribeSettings.CalibrationPointColor));
        calRunner.PointRecordingTime = this.theEyeTribeSettings.PointDisplayTime;

        calRunner.Start(); // blocks until complete
        calRunner.OnResult += calRunner_OnResult;
      }
      catch (Exception ex)
      {
        InformationDialog.Show(
          "Calibration failed",
          "TheEyeTribe calibration failed with the following message: " + Environment.NewLine + ex.Message,
          false,
          MessageBoxIcon.Error);

        this.CleanUp();
        return false;
      }

      return true;
    }

    /// <summary>
    ///   Raises <see cref="TheEyeTribeSettingsDialog" /> to change the settings
    ///   for this interface.
    /// </summary>
    public override void ChangeSettings()
    {
      var dlg = new TheEyeTribeSettingsDialog { TheEyeTribeSettings = this.theEyeTribeSettings };
      switch (dlg.ShowDialog())
      {
        case DialogResult.OK:
          var oldServerStartParams = this.theEyeTribeSettings.ServerStartParams;
          this.theEyeTribeSettings = dlg.TheEyeTribeSettings;
          this.SerializeSettings(this.theEyeTribeSettings, this.SettingsFile);

          if (this.theEyeTribeSettings.ServerStartParams != oldServerStartParams)
          {
            // Need to restart the eye tribe server if it is running
            if (IOHelpers.IsProcessOpen("EyeTribe"))
            {
              this.KillEyeTribeProcess();

              // Now restart 
              this.StartEyeTribeProcess();
            }
          }

          break;
      }
    }

    /// <summary>
    ///   Clean up objects.
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
          "TheEyeTribe CleanUp failed with the following message: " + Environment.NewLine + ex.Message,
          false,
          MessageBoxIcon.Error);
      }

      base.CleanUp();
    }

    /// <summary>
    ///   Connects the tracker.
    /// </summary>
    /// <returns>
    ///   <strong>True</strong> if connection succeded, otherwise
    ///   <strong>false</strong>.
    /// </returns>
    public override bool Connect()
    {
      try
      {
        // Start the eye tribe server
        if (!IOHelpers.IsProcessOpen("EyeTribe"))
        {
          this.StartEyeTribeProcess();
        }
        else
        {
          InformationDialog.Show(
            "EyeTribe server is running",
            "The eye tribe server is already running, so we connect to this instance. Please note: it may be using a different tracking rate than specified in the settings tab.",
            false,
            MessageBoxIcon.Information);
        }

        // Connect client
        if (!GazeManager.Instance.Activate(GazeManager.ApiVersion.VERSION_1_0, GazeManager.ClientMode.Push))
        {
          throw new Exception("Could not connect to the eye tribe tracker, please start the server manually and try again.");
        }

        //this.eyeTribeTrackStatus.Connect();

        // Register this class for events
        GazeManager.Instance.AddGazeListener(this);
        //GazeManager.Instance.AddTrackerStateListener(this);
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
    ///   Method to return the current timing of the tracking system.
    /// </summary>
    /// <returns>
    ///   A <see cref="long" /> with the time in milliseconds.
    /// </returns>
    public long GetCurrentTime()
    {
      return this.lastTime;
    }

    /// <summary>
    ///   Start tracking.
    /// </summary>
    public override void Record()
    {
      try
      {
        if (!this.isTracking)
        {
          // Start subscribing to gaze data stream
          // connectedTracker.StartTracking();
          this.isTracking = true;
        }
      }
      catch (Exception ex)
      {
        InformationDialog.Show(
          "Record failed",
          "TheEyeTribe Record failed with the following message: " + Environment.NewLine + ex.Message,
          false,
          MessageBoxIcon.Error);
        this.Stop();
      }
    }

    /// <summary>
    ///   Stops tracking.
    /// </summary>
    public override void Stop()
    {
      try
      {
        this.isTracking = false;
      }
      catch (Exception ex)
      {
        InformationDialog.Show(
          "Stop failed",
          "TheEyeTribe stop tracking failed with the following message: " + Environment.NewLine + ex.Message,
          false,
          MessageBoxIcon.Error);
      }
    }

    /// <summary>
    /// OnGazeData event handler for connected tracker.
    ///   This event fires whenever there are new gaze data
    ///   to receive.
    ///   It converts the interface internal gaze structure into
    ///   a OGAMA readable format and fires the <see cref="Tracker.OnGazeDataChanged"/>
    ///   event to the recorder.
    /// </summary>
    /// <param name="gazeData">
    /// The <see cref="Recording.GazeData"/> with the new gaze data
    ///   from the device.
    /// </param>
    public void OnGazeUpdate(TETCSharpClient.Data.GazeData gazeData)
    {
      var presentationSize = Document.ActiveDocument.PresentationSize;

      // Convert TheEyeTribe gaze data to ogama gaze data
      var newGazeData = new GazeData
      {
        Time = gazeData.TimeStamp,

        // Scale to 0..1
        GazePosX = (float)(gazeData.RawCoordinates.X / presentationSize.Width),
        GazePosY = (float)(gazeData.RawCoordinates.Y / presentationSize.Height),

        PupilDiaX = (float)gazeData.LeftEye.PupilSize,
        PupilDiaY = (float)gazeData.RightEye.PupilSize
      };

      this.OnGazeDataChanged(new GazeDataChangedEventArgs(newGazeData));
      this.lastTime = newGazeData.Time;
    }

    /// <summary>
    /// Called when the connection state changed.
    /// </summary>
    /// <param name="isConnected">If set to <c>true</c> the tracker is connected.</param>
    public void OnConnectionStateChanged(bool isConnected)
    {
      if (!isConnected)
      {
        InformationDialog.Show(
          "Tracker connection lost.",
          "The connection to the eye tribe tracker was lost. Please reconnect.",
          false,
          MessageBoxIcon.Exclamation);
        this.CleanUp();
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

        this.dlgTrackStatus = new TheEyeTribeTrackStatusDialog();

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
          //this.dlgTrackStatus.trackBoxStatus.Connect();
        }
      }
      else
      {
        // Should hide TrackStatusDlg
        if (this.dlgTrackStatus != null)
        {
          this.ShowOnSecondaryScreenButton.BackColor = Color.Transparent;
          this.ShowOnSecondaryScreenButton.Text = "Show on presentation screen";
          //this.dlgTrackStatus.trackBoxStatus.Disconnect();
          this.dlgTrackStatus.Close();
        }
      }
    }

    /// <summary>
    ///   Sets up calibration procedure and the tracking client
    ///   and wires the events. Reads settings from file.
    /// </summary>
    protected override sealed void Initialize()
    {
      // Load theEyeTribe tracker settings and write the config file for the eye tribe server
      if (File.Exists(this.SettingsFile))
      {
        this.theEyeTribeSettings = this.DeserializeSettings(this.SettingsFile);
      }
      else
      {
        this.theEyeTribeSettings = new TheEyeTribeSetting();
        this.SerializeSettings(this.theEyeTribeSettings, this.SettingsFile);
      }

      base.Initialize();
    }

    /// <summary>
    ///   Overridden.
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

    /// <summary>
    /// Implementations of this method should initialize 
    /// the designer components for the custom hardware device tab page.
    /// </summary>
    protected override void InitializeStatusControls()
    {
      this.theEyeTribeCalibrationResultPanel = new TheEyeTribeCalibrationResultPanel();
      this.wpfContainer = new ElementHost();
      this.eyeTribeTrackStatus = new TrackBoxStatus();
      this.CalibrationResultPanel.Controls.Add(this.theEyeTribeCalibrationResultPanel);
      this.TrackStatusPanel.Controls.Add(this.wpfContainer);

      // elementHost1
      this.wpfContainer.Dock = DockStyle.Fill;
      this.wpfContainer.Location = new Point(0, 0);
      this.wpfContainer.Name = "wpfContainer";
      this.wpfContainer.Size = new Size(190, 40);
      this.wpfContainer.TabIndex = 0;
      this.wpfContainer.Text = "wpfContainer";
      this.wpfContainer.Child = this.eyeTribeTrackStatus;

      // theEyeTribeCalibrationResultPanel
      this.theEyeTribeCalibrationResultPanel.BackColor = Color.WhiteSmoke;
      this.theEyeTribeCalibrationResultPanel.Dock = DockStyle.Fill;
      this.theEyeTribeCalibrationResultPanel.Location = new Point(0, 0);
      this.theEyeTribeCalibrationResultPanel.Name = "theEyeTribeCalibrationResultPanel";
      this.theEyeTribeCalibrationResultPanel.Size = new Size(190, 42);
      this.theEyeTribeCalibrationResultPanel.TabIndex = 0;
      this.theEyeTribeCalibrationResultPanel.Text = "theEyeTribeCalibrationResultPanel1";
    }

    /// <summary>
    /// Kills the eye tribe process.
    /// </summary>
    private void KillEyeTribeProcess()
    {
      // Kill EyeTribe Server
      if (this.eyeTribeServerProcess != null)
      {
        // Wait for shutdown
        Thread.Sleep(1000);
        this.eyeTribeServerProcess.Kill();
        this.eyeTribeServerProcess = null;
      }
    }

    /// <summary>
    /// Starts the eye tribe process.
    /// </summary>
    private void StartEyeTribeProcess()
    {
      var key = Registry.CurrentUser.OpenSubKey(@"Software\EyeTribe\EyeTribe Service", false);
      if (key != null)
      {
        var pathToServer = key.GetValue("InstallDir").ToString();
        var serverFile = Path.Combine(pathToServer, "EyeTribe.exe");
        var psi = new ProcessStartInfo(serverFile, this.theEyeTribeSettings.ServerStartParams)
        {
          CreateNoWindow = true,
          WindowStyle =
            ProcessWindowStyle.Hidden,
          UseShellExecute = false,
          RedirectStandardOutput =
            true
        };

        this.eyeTribeServerProcess = Process.Start(psi);
        System.Threading.Thread.Sleep(2000);
      }
    }

    /// <summary>
    /// Handles the OnResult event of the calRunner control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="CalibrationRunnerEventArgs"/> instance containing the event data.</param>
    private void calRunner_OnResult(object sender, CalibrationRunnerEventArgs e)
    {
      var result = e.CalibrationResult;

      // Show a calibration plot if everything went OK
      if (result != null)
      {
        this.theEyeTribeCalibrationResultPanel.Initialize(result);
        this.ShowCalibPlot();
      }
      else
      {
        MessageBox.Show("Not enough data to create a calibration (or calibration aborted).");
      }
    }


    /// <summary>
    /// Deserializes the <see cref="TheEyeTribeSetting"/> from the given xml file.
    /// </summary>
    /// <param name="filePath">
    /// Full file path to the xml settings file. 
    /// </param>
    /// <returns>
    /// A <see cref="TheEyeTribeSetting"/> object. 
    /// </returns>
    private TheEyeTribeSetting DeserializeSettings(string filePath)
    {
      var settings = new TheEyeTribeSetting();

      // Create an instance of the XmlSerializer class;
      // specify the type of object to be deserialized 
      var serializer = new XmlSerializer(typeof(TheEyeTribeSetting));

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
        settings = (TheEyeTribeSetting)serializer.Deserialize(fs);
        fs.Close();
      }
      catch (Exception ex)
      {
        InformationDialog.Show(
          "Error occured",
          "Deserialization of TheEyeTribeSettings failed with the following message: " + Environment.NewLine + ex.Message,
          false,
          MessageBoxIcon.Error);
      }

      return settings;
    }

    /// <summary>
    ///   The disconnect tracker method.
    /// </summary>
    private void DisconnectTracker()
    {
      //this.eyeTribeTrackStatus.Disconnect();

      // Unregister this class for gaze events
      GazeManager.Instance.RemoveGazeListener(this);
      //GazeManager.Instance.RemoveTrackerStateListener(this);

      // Disconnect 
      GazeManager.Instance.Deactivate();
      
      // Close server if it was opened from within ogama.
      this.KillEyeTribeProcess();

      this.isTracking = false;
    }

    /// <summary>
    /// Serializes the <see cref="TheEyeTribeSetting"/> into the given file in a xml structure.
    /// </summary>
    /// <param name="settings">
    /// The <see cref="TheEyeTribeSetting"/> object to serialize.
    /// </param>
    /// <param name="filePath">
    /// Full file path to the xml settings file.
    /// </param>
    private void SerializeSettings(TheEyeTribeSetting settings, string filePath)
    {
      // Create an instance of the XmlSerializer class;
      // specify the type of object to serialize 
      var serializer = new XmlSerializer(typeof(TheEyeTribeSetting));

      // Serialize the TheEyeTribeSetting, and close the TextWriter.
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
          "Serialization of TheEyeTribeSettings failed with the following message: " + Environment.NewLine + ex.Message,
          false,
          MessageBoxIcon.Error);
      }
    }

    #endregion
  }
}