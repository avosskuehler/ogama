// <copyright file="GazetrackerIPClientTracker.cs" company="FU Berlin">
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

namespace Ogama.Modules.Recording.GazegroupInterface
{
  using System;
  using System.Diagnostics;
  using System.Drawing;
  using System.IO;
  using System.Net;
  using System.Windows.Forms;
  using System.Xml.Serialization;

  using GTCommons.Events;

  using GTNetworkClient;

  using Ogama.ExceptionHandling;
  using Ogama.Modules.Common.CustomEventArgs;
  using Ogama.Modules.Common.Tools;
  using Ogama.Modules.Recording.Dialogs;
  using Ogama.Modules.Recording.TrackerBase;

  using OgamaControls;

  using GazeData = Ogama.Modules.Recording.GazeData;

  /// <summary>
  /// This class implements the <see cref="TrackerWithStatusControls"/> class
  /// to represent an OGAMA known eyetracker.
  /// It encapsulates the GazeTracker http://www.gazegroup.org 
  /// </summary>
  public class GazetrackerIPClientTracker : Tracker
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
    /// The <see cref="TextBox"/> in the UI, where the connection
    /// and error infos should be displayed.
    /// </summary>
    private readonly TextBox statusTextBox;

    /// <summary>
    /// The <see cref="Button"/> in the UI, where the 
    /// gazetracker application should be launched from.
    /// </summary>
    private readonly Button launchButton;

    /// <summary>
    /// The client which hosts 
    /// the udp connection to the iViewX host computer.
    /// </summary>
    private Client client;

    /////// <summary>
    /////// The thread which listens to packets received from
    /////// the udp connection to the iViewX host computer.
    /////// </summary>
    ////private Thread clientReceiveThread;

    /// <summary>
    /// Saves the sample time of the last received gaze sample-
    /// </summary>
    private long lastTime;

    /// <summary>
    /// The current GazetrackerIPClientSetting settings.
    /// </summary>
    private GazetrackerIPClientSetting settings;

    /// <summary>
    /// Saves the size of the presentation screen to calibrate
    /// the incoming gaze samples to 0..1 values.
    /// </summary>
    private Size presentationScreenSize;

    /// <summary>
    /// Flag indicating whether this tracker is in record mode,
    /// which is if it is sending gaze samples.
    /// Because we receive and display gaze sample just after connection
    /// to visualize valid connection, this is used to start recording.
    /// </summary>
    private bool shouldSendGazeSamples;

    /// <summary>
    /// Indicating the client status of the gazetracker.
    /// </summary>
    private GazetrackerIPClientStatus clientStatus;

    /// <summary>
    /// This timer is used to update the status text box.
    /// </summary>
    private Timer statusUpdateTimer;

    /// <summary>
    /// Stores a string to display the last gaze data sample
    /// on timer update of the status text box.
    /// </summary>
    private string lastGazedataString;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the GazetrackerIPClientTracker class.
    /// <remarks>Note that the xml settings file is set, but not used,
    /// GazeTracker internally saves it state in another location.
    /// </remarks>
    /// </summary>
    /// <param name="owningRecordModule">The <see cref="RecordModule"/>
    /// form wich host the recorder.</param>
    /// <param name="trackerStatusTextBox">The <see cref="TextBox"/>
    /// to retreive status messages of the tracker.</param>
    /// <param name="trackerLaunchButton">The <see cref="Button"/>
    /// named "Launch" at the tab page of the device.</param>
    /// <param name="trackerConnectButton">The <see cref="Button"/>
    /// named "Connect" at the tab page of the device.</param>
    /// <param name="trackerSubjectButton">The <see cref="Button"/>
    /// named "Subject" at the tab page of the device.</param>
    /// <param name="trackerRecordButton">The <see cref="Button"/>
    /// named "Record" at the tab page of the device.</param>
    /// <param name="trackerSubjectNameTextBox">The <see cref="TextBox"/>
    /// which should contain the subject name at the tab page of the device.</param>
    public GazetrackerIPClientTracker(
      RecordModule owningRecordModule,
      TextBox trackerStatusTextBox,
      Button trackerLaunchButton,
      Button trackerConnectButton,
      Button trackerSubjectButton,
      Button trackerRecordButton,
      TextBox trackerSubjectNameTextBox)
      : base(
      owningRecordModule,
      trackerConnectButton,
      trackerSubjectButton,
      null,
      trackerRecordButton,
      trackerSubjectNameTextBox,
      Properties.Settings.Default.EyeTrackerSettingsPath + "GazetrackerIPClientSetting.xml")
    {
      this.statusTextBox = trackerStatusTextBox;
      this.launchButton = trackerLaunchButton;

      // Call the initialize methods of derived classes
      this.Initialize();
    }

    /// <summary>
    /// Finalizes an instance of the GazetrackerIPClientTracker class
    /// </summary>
    ~GazetrackerIPClientTracker()
    {
      if (this.client == null)
      {
        return;
      }

      this.client.Disconnect();
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
    /// Gets the connection status of the GazegroupIPClientTracker
    /// </summary>
    public override bool IsConnected
    {
      get { return this.client.IsRunning; }
    }

    /// <summary>
    /// Gets the current gazetracker settings.
    /// </summary>
    /// <value>A <see cref="GazetrackerIPClientSetting"/> with the current tracker settings.</value>
    public GazetrackerIPClientSetting Settings
    {
      get { return this.settings; }
    }

    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Public methods                                                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region PUBLICMETHODS

    /// <summary>
    /// This method always returns true, because we do not check the ip
    /// connection until we connected.
    /// </summary>
    /// <param name="errorMessage">Out. A <see cref="String"/> with an error message.</param>
    /// <returns>Always true.</returns>
    public static TrackerStatus IsAvailable(out string errorMessage)
    {
      errorMessage = string.Empty;
      return TrackerStatus.Undetermined;
    }

    /// <summary>
    /// Method to return the current timing of the tracking system.
    /// </summary>
    /// <returns>A <see cref="Int64"/> with the current time in milliseconds
    /// if the stopwatch is running, otherwise 0.</returns>
    public long GetCurrentTime()
    {
      return this.lastTime;
    }

    #endregion //PUBLICMETHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Inherited methods                                                         //
    ///////////////////////////////////////////////////////////////////////////////
    #region OVERRIDES

    /// <summary>
    /// Connects to the Gazetracker camera system via UDP connection.
    /// </summary>
    /// <returns><strong>True</strong> if connection succeded, otherwise
    /// <strong>false</strong>.</returns>
    public override bool Connect()
    {
      try
      {
        this.client = new Client
        {
          IPAddress = IPAddress.Parse(this.Settings.GazeDataServerIPAddress),
          PortReceive = this.Settings.GazeDataServerPort,
          PortSend = this.Settings.CommandServerPort
        };

        this.client.ClientConnectionChanged += this.ClientClientConnectionChanged;
        this.client.ErrorOccured += this.ClientErrorOccured;
        this.client.Calibration.OnEnd += this.Calibration_OnEnd;
        this.client.GazeData.OnGazeData += this.GazeData_OnGazeData;

        if (!this.client.IsRunning)
        {
          this.client.Connect();
        }
      }
      catch (Exception ex)
      {
        var dlg = new ConnectionFailedDialog { ErrorMessage = ex.Message };
        dlg.ShowDialog();
        this.CleanUp();
        return false;
      }

      this.clientStatus.IsConnected = true;
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
        this.client.Calibration.Start();
      }
      catch (Exception ex)
      {
        this.DisplayMessage("ITU GazeTracker calibration failed with the following message: " + Environment.NewLine + ex.Message);
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
        this.clientStatus.Reset();
        if (this.client != null)
        {
          this.client.Disconnect();
          this.client.ClientConnectionChanged -= this.ClientClientConnectionChanged;
          this.client.ErrorOccured -= this.ClientErrorOccured;
          this.client.Calibration.OnEnd -= this.Calibration_OnEnd;
          this.client.GazeData.OnGazeData -= this.GazeData_OnGazeData;
        }
      }
      catch (Exception ex)
      {
        this.DisplayMessage("GazeTracker CleanUp failed with the following message: " + Environment.NewLine + ex.Message);
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
        this.shouldSendGazeSamples = true;
      }
      catch (Exception ex)
      {
        this.DisplayMessage("GazeTracker record failed with the following message: " + Environment.NewLine + ex.Message);
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
        if (this.client != null)
        {
          this.shouldSendGazeSamples = false;
        }
      }
      catch (Exception ex)
      {
        this.DisplayMessage("GazeTracker stop tracking failed with the following message: " + Environment.NewLine + ex.Message);
      }
    }

    /// <summary>
    /// Raises GTApplication SettingsWindow to change the settings
    /// for this interface.
    /// </summary>
    public override void ChangeSettings()
    {
      var dlg = new GazetrackerIPClientSettingsDialog { GazetrackerIPClientSetting = this.Settings };

      if (dlg.ShowDialog() == DialogResult.OK)
      {
        this.settings = dlg.GazetrackerIPClientSetting;
        this.SerializeSettings(this.Settings, this.SettingsFile);

        if (this.client != null)
        {
          this.client.IPAddress = IPAddress.Parse(this.settings.GazeDataServerIPAddress);
          this.client.PortReceive = this.settings.GazeDataServerPort;
          this.client.PortSend = this.settings.CommandServerPort;
        }
      }
    }

    /// <summary>
    /// Sets up calibration procedure and the tracking client
    /// and wires the events. Reads settings from file.
    /// </summary>
    protected override sealed void Initialize()
    {
      // Load alea tracker settings.
      if (File.Exists(this.SettingsFile))
      {
        this.settings = this.DeserializeSettings(this.SettingsFile);
      }
      else
      {
        this.settings = new GazetrackerIPClientSetting();
        this.SerializeSettings(this.settings, this.SettingsFile);
      }

      this.lastTime = 0;
      this.presentationScreenSize = Document.ActiveDocument.PresentationSize;
      this.launchButton.Click += this.LaunchButtonClick;
      this.statusUpdateTimer = new Timer { Interval = 100 };
      this.statusUpdateTimer.Tick += this.StatusUpdateTimerTick;
      this.statusUpdateTimer.Start();
      this.clientStatus.Reset();

      ThreadSafe.EnableDisableButton(this.ConnectButton, false);
    }

    /// <summary>
    /// Overridden <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> subject button.
    /// Calls the base class and enables the record button,
    /// because this tracker has done its calibration externally
    ///  </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/>.</param>
    protected override void BtnSubjectNameClick(object sender, EventArgs e)
    {
      base.BtnSubjectNameClick(sender, e);

      // Activate the record button
      // if the subject name is OK and we receive a correct calibration.
      if (!Queries.CheckDatabaseForExistingSubject(this.SubjectButton.Text))
      {
        this.RecordButton.Enabled = true;
      }
    }

    /// <summary>
    /// Overridden <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> RecordButton.
    /// Checks for valid calibration and tracking data then starts base call.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/>.</param>
    protected override void BtnRecordClick(object sender, EventArgs e)
    {
      if (!this.clientStatus.IsCalibrated)
      {
        var inform = new InformationDialog(
          "Calibration required", "Please calibrate first using the user interface of the gazetracker application.", false, MessageBoxIcon.Warning);
        inform.ShowDialog();
        return;
      }

      if (!this.clientStatus.IsStreaming)
      {
        var inform = new InformationDialog(
          "Streaming required", "Please start the data stream of the gazetracker by turning on the data server in the network tab of the gazetracker application.", false, MessageBoxIcon.Warning);
        inform.ShowDialog();
        return;
      }

      base.BtnRecordClick(sender, e);
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
    /// <see cref="Button"/> <see cref="launchButton"/>.
    /// Launches the gazetracker application and
    /// enables connect button.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/>.</param>
    private void LaunchButtonClick(object sender, EventArgs e)
    {
      if (!this.IsGazeTrackerApplicationRunning())
      {
        var gazetrackerPath = Path.Combine(
          Application.StartupPath, "GTApplication.exe");
        Process.Start(gazetrackerPath);
      }
      else
      {
        var dialog = new InformationDialog(
          "Gazetracker is already running.",
          string.Format("An instance of gazetracker is already running on this computer. Ogama will try to connect to this instance."),
            false,
            MessageBoxIcon.Information);
        dialog.ShowDialog();
      }

      ThreadSafe.EnableDisableButton(this.ConnectButton, true);
    }

    /// <summary>
    /// The timer update tick event handler which updates the status text box with the new
    /// status of the client.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/>.</param>
    private void StatusUpdateTimerTick(object sender, EventArgs e)
    {
      var informationString = this.lastGazedataString;

      if (!this.clientStatus.IsConnected)
      {
        informationString = "Connection to gazetracker closed. " + "Please launch it and connect.";
      }
      else if (!this.clientStatus.IsCalibrated)
      {
        informationString = "Connection to gazetracker succesfully established."
                            + "Now configure the gazetracker to track the eyes using the "
                            + "user interface of the gazetracker application."
                            + "Return to ogama when tracking is succesfully calibrated. "
                            + "Then specify subject and start recording.";
      }
      else if (!this.clientStatus.IsStreaming)
      {
        informationString = "Please enable the gaze data stream by turning on the data send server in the network tab of the gazetracker application.";
      }

      this.DisplayMessage(informationString);
    }

    #endregion //WINDOWSEVENTHANDLER

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler for Custom Defined Events                                    //
    ///////////////////////////////////////////////////////////////////////////////
    #region CUSTOMEVENTHANDLER
    /// <summary>
    /// The <see cref="GTNetworkClient.GazeData.OnGazeData"/> event handler
    /// which is called whenever there is a new frame arrived.
    /// Sends the GazeDataChanged event to the recording module.
    /// </summary>
    /// <param name="gazeData">The <see cref="GTNetworkClient.GazeData"/> with the gaze data
    /// which are the mapped gaze coordinates in pixel units.</param>
    private void GazeData_OnGazeData(GTNetworkClient.GazeData gazeData)
    {
      this.clientStatus.IsStreaming = true;

      var ogamaGazeData = new GazeData
      {
        // Calculate values between 0..1
        GazePosX = (float)gazeData.GazePositionX / this.presentationScreenSize.Width,
        GazePosY = (float)gazeData.GazePositionY / this.presentationScreenSize.Height,
        ////GazePosX = (float)gazeData.GazePositionX,
        ////GazePosY = (float)gazeData.GazePositionY,
        PupilDiaX = gazeData.PupilDiameterLeft,
        PupilDiaY = gazeData.PupilDiameterRight,
        Time = gazeData.TimeStamp
      };

      this.lastGazedataString = string.Format(
           "Receiving gaze data: {0}Time: {1} {2} X: {3}, Y: {4} {5} Pupil: {6}",
           Environment.NewLine,
           gazeData.TimeStamp.ToString("N0"),
           Environment.NewLine,
           gazeData.GazePositionX.ToString("N0"),
           gazeData.GazePositionY.ToString("N0"),
           Environment.NewLine,
           gazeData.PupilDiameterLeft);

      if (this.shouldSendGazeSamples)
      {
        this.lastTime = gazeData.TimeStamp;
        this.OnGazeDataChanged(new GazeDataChangedEventArgs(ogamaGazeData));
      }
    }

    /// <summary>
    /// Event handler for the calibration OnEnd event of the gazetracker client
    /// which updates the status with the calibration quality rating.
    /// </summary>
    /// <param name="quality">The quality index of the calibration.</param>
    private void Calibration_OnEnd(int quality)
    {
      ThreadSafe.EnableDisableButton(this.RecordButton, true);
      this.DisplayMessage("Calibration Finished + Rating: " + quality);
      this.clientStatus.IsCalibrated = true;
      this.clientStatus.IsStreaming = false;
    }

    /// <summary>
    /// The event handler for the error occured event of the gazetracker client.
    /// Displays the error message.
    /// </summary>
    /// <param name="sender">The source of the event</param>
    /// <param name="e">A StringEventArgs with the error message.</param>
    private void ClientErrorOccured(object sender, StringEventArgs e)
    {
      this.DisplayMessage(e.Param);
    }

    /// <summary>
    /// The ClientConnectionChanged event handler for the gazetracker
    /// client. Updates UI.
    /// </summary>
    /// <param name="sender">The source of the event</param>
    /// <param name="success">True if connection could be established, otherwise false.</param>
    private void ClientClientConnectionChanged(object sender, bool success)
    {
      if (success)
      {
        ThreadSafe.EnableDisableButton(this.SubjectButton, true);
        this.clientStatus.IsConnected = true;
      }
      else
      {
        this.clientStatus.Reset();
        this.ConnectButton.BackColor = Color.Transparent;
        ThreadSafe.EnableDisableButton(this.SubjectButton, false);
        ThreadSafe.EnableDisableButton(this.RecordButton, false);
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
    /// Displays the given message in the text box control
    /// for information at the top panel of the gazetracker tab.
    /// </summary>
    /// <param name="message">The message to be displayed.</param>
    private void DisplayMessage(string message)
    {
      ThreadSafe.ThreadSafeSetText(this.statusTextBox, message);
    }

    /// <summary>
    /// Deserializes the <see cref="GazetrackerIPClientSetting"/> from the given xml file.
    /// </summary>
    /// <param name="filePath">Full file path to the xml settings file.</param>
    /// <returns>A <see cref="GazetrackerIPClientSetting"/> object.</returns>
    private GazetrackerIPClientSetting DeserializeSettings(string filePath)
    {
      var clientSettings = new GazetrackerIPClientSetting();

      // Create an instance of the XmlSerializer class;
      // specify the type of object to be deserialized 
      var serializer = new XmlSerializer(typeof(GazetrackerIPClientSetting));

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
        clientSettings = (GazetrackerIPClientSetting)serializer.Deserialize(fs);
        fs.Close();
      }
      catch (Exception ex)
      {
        InformationDialog.Show(
          "Error occured",
          "Deserialization of GazetrackerIPSetting failed with the following message: " + Environment.NewLine + ex.Message,
          false,
          MessageBoxIcon.Error);
      }

      return clientSettings;
    }

    /// <summary>
    /// Serializes the <see cref="GazetrackerIPClientSetting"/> into the given file in a xml structure.
    /// </summary>
    /// <param name="clientSettings">The <see cref="GazetrackerIPClientSetting"/> object to serialize.</param>
    /// <param name="filePath">Full file path to the xml settings file.</param>
    private void SerializeSettings(GazetrackerIPClientSetting clientSettings, string filePath)
    {
      // Create an instance of the XmlSerializer class;
      // specify the type of object to serialize 
      var serializer = new XmlSerializer(typeof(GazetrackerIPClientSetting));

      // Serialize the GazetrackerIPSetting, and close the TextWriter.
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
          "Serialization of GazetrackerIPClientSetting failed with the following message: " + Environment.NewLine + ex.Message,
          false,
          MessageBoxIcon.Error);
      }
    }

    #endregion //METHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER

    /// <summary>
    /// Checks the process list on the data send computer, whether a gazetracker
    /// process is already running.
    /// </summary>
    /// <returns>True if a gazetracker instance is running, otherwise false.</returns>
    private bool IsGazeTrackerApplicationRunning()
    {
      // Check for Ogamas GT adaption
      Process[] gazetrackerProcesses =
        Process.GetProcessesByName("GTApplication");
      if (gazetrackerProcesses.Length == 0)
      {
        // Check for original 2.0b gazetracker
        gazetrackerProcesses = Process.GetProcessesByName("GazeTrackerUI");
        return gazetrackerProcesses.Length != 0;
      }

      return true;
    }

    #endregion //HELPER
  }
}
