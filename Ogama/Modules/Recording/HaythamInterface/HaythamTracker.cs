// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HaythamTracker.cs" company="Freie Universität Berlin">
//   OGAMA - open gaze and mouse analyzer 
//   Copyright (C) 2015 Dr. Adrian Voßkühler  
//   Licensed under GPL V3
// </copyright>
// <author>Adrian Voßkühler</author>
// <email>adrian@ogama.net</email>
// <summary>
//   This class implements the <see cref="Tracker" /> class
//   to represent an OGAMA known eyetracker.
//   It encapsulates the Haytham eye tracker http://www.itu.dk/research/eye
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Ogama.Modules.Recording.HaythamInterface
{
  using System;
  using System.Diagnostics;
  using System.IO;
  using System.Linq;
  using System.Net;
  using System.Net.Sockets;
  using System.Threading;
  using System.Threading.Tasks;
  using System.Windows.Forms;
  using System.Xml.Serialization;

  using GTCommons.Events;

  using Haytham.ExtData;

  using Ogama.ExceptionHandling;
  using Ogama.Modules.Common.CustomEventArgs;
  using Ogama.Modules.Common.Tools;
  using Ogama.Modules.Common.Types;
  using Ogama.Modules.Recording.Dialogs;
  using Ogama.Modules.Recording.TrackerBase;

  using OgamaControls;

  using Timer = System.Windows.Forms.Timer;

  /// <summary>
  ///   This class implements the <see cref="Tracker" /> class
  ///   to represent an OGAMA known eyetracker.
  ///   It encapsulates the Haytham eye tracker http://www.itu.dk/research/eye
  /// </summary>
  public class HaythamTracker : Tracker
  {
    #region Fields

    /// <summary>
    ///   The <see cref="Button" /> in the UI, where the
    ///   haytham tracker application should be launched from.
    /// </summary>
    private readonly Button launchButton;

    /// <summary>
    ///   The <see cref="TextBox" /> in the UI, where the connection
    ///   and error infos should be displayed.
    /// </summary>
    private readonly TextBox statusTextBox;

    /// <summary>
    ///   The split container owning the info textbox and the track status
    /// </summary>
    private readonly SplitContainer trackControlsSplitContainer;

    /// <summary>
    ///   The <see cref="TrackStatusControl" /> that displays information about the current track status.
    /// </summary>
    private readonly TrackStatusControl trackStatusControl;

    /// <summary>
    ///   Indicating the client status of the haytham tracker.
    /// </summary>
    private HaythamStatus clientStatus;

    /// <summary>
    ///   The client which hosts
    ///   the tcp connection to the haytham server.
    /// </summary>
    private HaythamClient haythamClient;

    /// <summary>
    ///   Stores a string to display the last gaze data sample
    ///   on timer update of the status text box.
    /// </summary>
    private string lastGazedataString;

    /// <summary>
    ///   Saves the sample time of the last received gaze sample-
    /// </summary>
    private long lastTime;

    /// <summary>
    ///   The current HaythamSetting settings.
    /// </summary>
    private HaythamSetting settings;

    /// <summary>
    ///   Flag indicating whether this tracker is in record mode,
    ///   which is if it is sending gaze samples.
    ///   Because we receive and display gaze sample just after connection
    ///   to visualize valid connection, this is used to start recording.
    /// </summary>
    private bool shouldSendGazeSamples;

    /// <summary>
    ///   This timer is used to update the status text box.
    /// </summary>
    private Timer statusUpdateTimer;

    #endregion

    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the HaythamTracker class.
    ///   <remarks>
    /// Note that the xml settings file is set, but not used,
    ///     Haytham internally saves it state in another location.
    ///   </remarks>
    /// </summary>
    /// <param name="owningRecordModule">
    /// The <see cref="RecordModule"/> form wich host the recorder.
    /// </param>
    /// <param name="trackerTrackerControlsContainer">
    /// The split container owning the info textbox and the track status
    /// </param>
    /// <param name="trackerTrackStatusControl">
    /// The <see cref="TrackStatusControl"/> that displays information about the
    ///   current track status.
    /// </param>
    /// <param name="trackerStatusTextBox">
    /// The <see cref="TextBox"/> to retreive status messages of the tracker. 
    /// </param>
    /// <param name="trackerLaunchButton">
    /// The <see cref="Button"/> named "Launch" at the tab page of the device.
    /// </param>
    /// <param name="trackerConnectButton">
    /// The <see cref="Button"/> named "Connect" at the tab page of the device.
    /// </param>
    /// <param name="trackerSubjectButton">
    /// The <see cref="Button"/> named "Subject" at the tab page of the device. 
    /// </param>
    /// <param name="trackerRecordButton">
    /// The <see cref="Button"/> named "Record" at the tab page of the device. 
    /// </param>
    /// <param name="trackerSubjectNameTextBox">
    /// The <see cref="TextBox"/> which should contain the subject name at the tab
    ///   page of the device.
    /// </param>
    public HaythamTracker(
      RecordModule owningRecordModule,
      SplitContainer trackerTrackerControlsContainer,
      TrackStatusControl trackerTrackStatusControl,
      TextBox trackerStatusTextBox,
      Button trackerLaunchButton,
      Button trackerConnectButton,
      //Button trackerCalibrateButton,
      Button trackerSubjectButton,
      Button trackerRecordButton,
      TextBox trackerSubjectNameTextBox)
      : base(
        owningRecordModule,
        trackerConnectButton,
        trackerSubjectButton,
        //trackerCalibrateButton,
      null,
        trackerRecordButton,
        trackerSubjectNameTextBox,
        Properties.Settings.Default.EyeTrackerSettingsPath + "HaythamSetting.xml")
    {
      this.statusTextBox = trackerStatusTextBox;
      this.trackControlsSplitContainer = trackerTrackerControlsContainer;
      this.trackStatusControl = trackerTrackStatusControl;
      this.launchButton = trackerLaunchButton;

      // Call the initialize methods of derived classes
      this.Initialize();
    }

    /// <summary>
    ///   Finalizes an instance of the HaythamTracker class
    /// </summary>
    ~HaythamTracker()
    {
      if (this.haythamClient == null)
      {
        return;
      }

      this.haythamClient.Disconnect();
    }

    #endregion

    #region Public Properties

    /// <summary>
    ///   Gets the connection status of the Haytham Tracker
    /// </summary>
    public override bool IsConnected
    {
      get
      {
        return this.clientStatus.HasFlag(HaythamStatus.IsConnected);
      }
    }

    /// <summary>
    ///   Gets the current haytham tracker settings.
    /// </summary>
    /// <value>A <see cref="HaythamSetting" /> with the current tracker settings.</value>
    public HaythamSetting Settings
    {
      get
      {
        return this.settings;
      }
    }

    #endregion

    ///////////////////////////////////////////////////////////////////////////////
    // Public methods                                                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region Public Methods and Operators

    /// <summary>
    /// This method always returns true, because we do not check the ip
    ///   connection until we connected.
    /// </summary>
    /// <param name="errorMessage">
    /// Out. A <see cref="string"/> with an error message.
    /// </param>
    /// <returns>
    /// Always true.
    /// </returns>
    public static TrackerStatus IsAvailable(out string errorMessage)
    {
      errorMessage = "The hardware status of the haytham tracker can not be automatically determined.";
      return TrackerStatus.Undetermined;
    }

    ///// <summary>
    ///// Starts calibration.
    ///// </summary>
    ///// <param name="isRecalibrating">
    ///// whether to use recalibration or not.
    ///// </param>
    ///// <returns>
    ///// <strong>True</strong> if calibration succeded, otherwise
    /////   <strong>false</strong>.
    ///// </returns>
    //public override bool Calibrate(bool isRecalibrating)
    //{
    //  try
    //  {
    //    this.haythamClient.Calibrate();
    //  }
    //  catch (Exception ex)
    //  {
    //    this.DisplayMessage(
    //      "Haytham calibration failed with the following message: " + Environment.NewLine + ex.Message);
    //    this.CleanUp();
    //    return false;
    //  }

    //  return true;
    //}

    /// <summary>
    /// An implementation of this method should do the calibration
    ///   for the specific hardware, so that the
    ///   system is ready for recording.
    /// </summary>
    /// <param name="isRecalibrating"><strong>True</strong> if calibration
    ///   is in recalibration mode, indicating to renew only a few points,
    ///   otherwise 
    /// <strong>false</strong>.</param>
    /// <returns>
    /// <strong>True</strong> if successful calibrated,
    ///   otherwise 
    /// <strong>false</strong>.
    /// </returns>
    /// <remarks>
    /// Implementors do not have to use the recalibrating
    ///   parameter.
    /// </remarks>
    public override bool Calibrate(bool isRecalibrating)
    {
      // Calibration should be done in the haytham tracker application
      return true;
    }

    /// <summary>
    ///   Raises SettingsWindow to change the network settings for the haytham tracker
    /// </summary>
    public override void ChangeSettings()
    {
      var dlg = new HaythamSettingsDialog { HaythamSetting = this.Settings };

      if (dlg.ShowDialog() == DialogResult.OK)
      {
        this.settings = dlg.HaythamSetting;
        this.SerializeSettings(this.Settings, this.SettingsFile);

        if (this.haythamClient != null)
        {
          this.haythamClient.ServerIPAddress = IPAddress.Parse(this.settings.HaythamServerIPAddress);
        }
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
        this.clientStatus = 0;
        if (this.haythamClient != null)
        {
          this.haythamClient.Disconnect();
          this.haythamClient.CalibrationFinished -= this.CalibrationFinished;
          this.haythamClient.GazeDataReceived -= this.GazeDataReceived;
          this.haythamClient.TrackStatusDataChanged -= this.HaythamClientTrackStatusDataChanged;

          // Hide track status control
          this.trackControlsSplitContainer.Panel1Collapsed = false;
          this.trackControlsSplitContainer.Panel2Collapsed = true;
        }
      }
      catch (Exception ex)
      {
        this.DisplayMessage("Haytham Cleanup failed with the following message: " + Environment.NewLine + ex.Message);
      }

      base.CleanUp();
    }

    /// <summary>
    ///   Connects to the Haytham camera system via UDP connection.
    /// </summary>
    /// <returns>
    ///   <strong>True</strong> if connection succeded, otherwise
    ///   <strong>false</strong>.
    /// </returns>
    public override bool Connect()
    {
      try
      {
        this.haythamClient = new HaythamClient
                     {
                       ServerIPAddress = IPAddress.Parse(this.Settings.HaythamServerIPAddress)
                     };

        this.haythamClient.CalibrationFinished += this.CalibrationFinished;
        this.haythamClient.GazeDataReceived += this.GazeDataReceived;
        this.haythamClient.TrackStatusDataChanged += this.HaythamClientTrackStatusDataChanged;

        if (!this.clientStatus.HasFlag(HaythamStatus.IsConnected))
        {
          if (!this.haythamClient.Connect())
          {
            throw new Exception("Connection to haytham server failed.");
          }
          //else
          //{
          //  // Show track status control
          //  this.trackControlsSplitContainer.Panel1Collapsed = true;
          //  this.trackControlsSplitContainer.Panel2Collapsed = false;

          //}
        }
      }
      catch (Exception ex)
      {
        var dlg = new ConnectionFailedDialog { ErrorMessage = ex.Message };
        dlg.ShowDialog();
        this.CleanUp();
        return false;
      }

      this.clientStatus = this.clientStatus | HaythamStatus.IsConnected;
      return true;
    }

    /// <summary>
    ///   Method to return the current timing of the tracking system.
    /// </summary>
    /// <returns>
    ///   A <see cref="long" /> with the current time in milliseconds
    ///   if the stopwatch is running, otherwise 0.
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
      this.shouldSendGazeSamples = true;
    }

    /// <summary>
    ///   Stops tracking.
    /// </summary>
    public override void Stop()
    {
      this.shouldSendGazeSamples = false;
      this.clientStatus = HaythamStatus.IsCalibrated | HaythamStatus.IsConnected;
    }

    #endregion

    #region Methods

    /// <summary>
    /// Overridden <see cref="Control.Click"/> event handler for the
    ///   <see cref="Button"/> RecordButton.
    ///   Checks for valid calibration and tracking data then starts base call.
    /// </summary>
    /// <param name="sender">
    /// Source of the event.
    /// </param>
    /// <param name="e">
    /// An empty <see cref="EventArgs"/>.
    /// </param>
    protected override void BtnRecordClick(object sender, EventArgs e)
    {
      if (!this.clientStatus.HasFlag(HaythamStatus.IsCalibrated))
      {
        var inform = new InformationDialog(
          "Calibration required",
          "Please calibrate first using the user interface of the haytham tracker application.",
          false,
          MessageBoxIcon.Warning);
        inform.ShowDialog();
        return;
      }

      //if (!this.clientStatus.HasFlag(HaythamStatus.IsStreaming))
      //{
      //  var inform = new InformationDialog(
      //    "Streaming required",
      //    "Please start the data stream of the haytham tracker by turning on the data server in the network tab of the haytham tracker application.",
      //    false,
      //    MessageBoxIcon.Warning);
      //  inform.ShowDialog();
      //  return;
      //}

      base.BtnRecordClick(sender, e);
    }

    /// <summary>
    /// Overridden <see cref="Control.Click"/> event handler for the
    ///   <see cref="Button"/> subject button.
    ///   Calls the base class and enables the record button,
    ///   because this tracker has done its calibration externally
    /// </summary>
    /// <param name="sender">
    /// Source of the event.
    /// </param>
    /// <param name="e">
    /// An empty <see cref="EventArgs"/>.
    /// </param>
    protected override void BtnSubjectNameClick(object sender, EventArgs e)
    {
      base.BtnSubjectNameClick(sender, e);

      // Activate the calibrate button
      // if the subject name is OK 
      if (!Queries.CheckDatabaseForExistingSubject(this.SubjectButton.Text))
      {
        //this.CalibrateButton.Enabled = true;
        this.DisplayMessage(
          "Please ensure the haytham tracker has a camera stream running and is tracking the eye successfully!");
      }

      ThreadSafe.EnableDisableButton(this.RecordButton, true);
    }

    /// <summary>
    ///   Reads settings from file. Then checks for existing haytham servers on
    ///   the network to set the IP in the settings.
    /// </summary>
    protected override sealed void Initialize()
    {
      // Load haytham tracker settings.
      if (File.Exists(this.SettingsFile))
      {
        this.settings = this.DeserializeSettings(this.SettingsFile);
      }
      else
      {
        this.settings = new HaythamSetting();
        this.SerializeSettings(this.settings, this.SettingsFile);
      }

      Task.Factory.StartNew(
        () =>
        {
          // find haytham hosts on network
          Uri hostUri = Client.getActiveHosts().FirstOrDefault();
          while (hostUri == null)
          {
            // wait 5 seconds before next try
            Thread.Sleep(5000);

            // it has 2seconds timeout
            hostUri = Client.getActiveHosts().FirstOrDefault();
          }

          // show IPv4 address if exists
          IPAddress server =
          Dns.GetHostAddresses(hostUri.DnsSafeHost)
            .FirstOrDefault(adr => adr.AddressFamily == AddressFamily.InterNetwork);
          if (server != null)
          {
            this.settings.HaythamServerIPAddress = server.ToString();
          }
        });

      this.lastTime = 0;
      this.launchButton.Click += this.LaunchButtonClick;
      this.statusUpdateTimer = new Timer { Interval = 100 };
      this.statusUpdateTimer.Tick += this.StatusUpdateTimerTick;
      this.statusUpdateTimer.Start();
      this.clientStatus = 0;
      this.trackControlsSplitContainer.Panel1Collapsed = false;
      this.trackControlsSplitContainer.Panel2Collapsed = true;

      ThreadSafe.EnableDisableButton(this.ConnectButton, false);
    }

    /// <summary>
    /// Event handler for the calibration OnEnd event of the haytham tracker client
    ///   which updates the status with the calibration quality rating.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The <see cref="EventArgs"/> instance containing the event data.
    /// </param>
    private void CalibrationFinished(object sender, EventArgs e)
    {
      ThreadSafe.EnableDisableButton(this.RecordButton, true);
      this.DisplayMessage("Calibration Finished");
      this.clientStatus = this.clientStatus | HaythamStatus.IsCalibrated;

      // Show track status control
      ThreadSafe.ShowHideSplitContainerPanel(this.trackControlsSplitContainer, true, false);
      ThreadSafe.ShowHideSplitContainerPanel(this.trackControlsSplitContainer, false, true);
    }

    ///// <summary>
    ///// The event handler for the error occured event of the haytham tracker client.
    /////   Displays the error message.
    ///// </summary>
    ///// <param name="sender">
    ///// The source of the event
    ///// </param>
    ///// <param name="e">
    ///// A StringEventArgs with the error message.
    ///// </param>
    //private void ClientErrorOccured(object sender, StringEventArgs e)
    //{
    //  this.DisplayMessage(e.Param);
    //}

    /// <summary>
    /// Deserializes the <see cref="HaythamSetting"/> from the given xml file.
    /// </summary>
    /// <param name="filePath">
    /// Full file path to the xml settings file.
    /// </param>
    /// <returns>
    /// A <see cref="HaythamSetting"/> object.
    /// </returns>
    private HaythamSetting DeserializeSettings(string filePath)
    {
      var clientSettings = new HaythamSetting();

      // Create an instance of the XmlSerializer class;
      // specify the type of object to be deserialized 
      var serializer = new XmlSerializer(typeof(HaythamSetting));

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
        clientSettings = (HaythamSetting)serializer.Deserialize(fs);
        fs.Close();
      }
      catch (Exception ex)
      {
        InformationDialog.Show(
          "Error occured",
          "Deserialization of HaythamIPSetting failed with the following message: " + Environment.NewLine + ex.Message,
          false,
          MessageBoxIcon.Error);
      }

      return clientSettings;
    }

    /// <summary>
    /// Displays the given message in the text box control
    ///   for information at the top panel of the haytham tracker tab.
    /// </summary>
    /// <param name="message">
    /// The message to be displayed.
    /// </param>
    private void DisplayMessage(string message)
    {
      ThreadSafe.ThreadSafeSetText(this.statusTextBox, message);
    }

    /// <summary>
    /// The
    ///   <see cref="GTNetworkClient.GazeData.OnGazeData"/> event handler
    ///   which is called whenever there is a new frame arrived.
    ///   Sends the GazeDataChanged event to the recording module.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The <see cref="GazeDataChangedEventArgs"/> instance containing the event data.
    /// </param>
    private void GazeDataReceived(object sender, GazeDataChangedEventArgs e)
    {
      this.clientStatus = this.clientStatus | HaythamStatus.IsStreaming;

      if (e.Gazedata.GazePosX != null && e.Gazedata.GazePosY != null)
      {
        this.lastGazedataString = string.Format(
          "Receiving gaze data: {0}Time: {1} {2} X: {3}, Y: {4} {5} Pupil: {6}",
          Environment.NewLine,
          e.Gazedata.Time.ToString("N0"),
          Environment.NewLine,
          e.Gazedata.GazePosX.Value.ToString("N0"),
          e.Gazedata.GazePosY.Value.ToString("N0"),
          Environment.NewLine,
          e.Gazedata.PupilDiaX);
      }

      if (this.shouldSendGazeSamples)
      {
        this.lastTime = e.Gazedata.Time;
        this.OnGazeDataChanged(e);
      }
    }

    /// <summary>
    /// Handles the TrackStatusDataChanged event of the haythamClient control.
    /// </summary>
    /// <param name="sender">
    /// The source of the event.
    /// </param>
    /// <param name="e">
    /// The <see cref="TrackStatusDataChangedEventArgs"/> instance containing the event data.
    /// </param>
    private void HaythamClientTrackStatusDataChanged(object sender, TrackStatusDataChangedEventArgs e)
    {
      this.trackStatusControl.OnTrackStatusData(e.TrackStatusData);
    }

    /// <summary>
    ///   Checks the process list on the data send computer, whether a haytham tracker
    ///   process is already running.
    /// </summary>
    /// <returns>True if a haytham tracker instance is running, otherwise false.</returns>
    private bool IsHaythamApplicationRunning()
    {
      // Check for Haytham server application
      Process[] haythamProcesses = Process.GetProcessesByName("Haytham");
      return haythamProcesses.Length != 0;
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    ///   <see cref="Button"/> <see cref="launchButton"/>.
    ///   Launches the haytham tracker application and
    ///   enables connect button.
    /// </summary>
    /// <param name="sender">
    /// Source of the event.
    /// </param>
    /// <param name="e">
    /// An empty <see cref="EventArgs"/>.
    /// </param>
    private void LaunchButtonClick(object sender, EventArgs e)
    {
      if (!this.IsHaythamApplicationRunning())
      {
        var programFilesFolder = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);
        string haythamPath = Path.Combine(programFilesFolder, Path.Combine("Haytham", "Haytham.exe"));
        Process.Start(haythamPath);
      }
      else
      {
        var dialog = new InformationDialog(
          "Haytham is already running.",
          string.Format("An instance of haytham tracker is already running on this computer. Ogama will try to connect to this instance."),
          false,
          MessageBoxIcon.Information);
        dialog.ShowDialog();
      }

      ThreadSafe.EnableDisableButton(this.ConnectButton, true);
    }

    /// <summary>
    /// Serializes the <see cref="HaythamSetting"/> into the given file in a xml structure.
    /// </summary>
    /// <param name="clientSettings">
    /// The <see cref="HaythamSetting"/> object to serialize.
    /// </param>
    /// <param name="filePath">
    /// Full file path to the xml settings file.
    /// </param>
    private void SerializeSettings(HaythamSetting clientSettings, string filePath)
    {
      // Create an instance of the XmlSerializer class;
      // specify the type of object to serialize 
      var serializer = new XmlSerializer(typeof(HaythamSetting));

      // Serialize the HaythamIPSetting, and close the TextWriter.
      try
      {
        TextWriter writer = new StreamWriter(filePath, false);
        serializer.Serialize(writer, clientSettings);
        writer.Close();
      }
      catch (Exception ex)
      {
        InformationDialog.Show(
          "Error occured",
          "Serialization of HaythamSetting failed with the following message: " + Environment.NewLine + ex.Message,
          false,
          MessageBoxIcon.Error);
      }
    }

    /// <summary>
    /// The timer update tick event handler which updates the status text box with the new
    ///   status of the client.
    /// </summary>
    /// <param name="sender">
    /// Source of the event.
    /// </param>
    /// <param name="e">
    /// An empty <see cref="EventArgs"/>.
    /// </param>
    private void StatusUpdateTimerTick(object sender, EventArgs e)
    {
      string informationString = this.lastGazedataString;

      if (!this.clientStatus.IsFlagSet(HaythamStatus.IsConnected))
      {
        informationString = "Connection to haytham tracker closed. " + "Please launch it and connect.";
      }
      else if (!this.clientStatus.IsFlagSet(HaythamStatus.IsCalibrated))
      {
        informationString = "Connection to haytham tracker successfully established."
                  + "Now configure the haytham tracker to track the eyes using the "
                  + "user interface of the haytham tracker application asn calibrate the subject."
                  + "Return to ogama when gaze tracking is successfully established. ";
      }

      this.DisplayMessage(informationString);
    }

    #endregion
  }
}