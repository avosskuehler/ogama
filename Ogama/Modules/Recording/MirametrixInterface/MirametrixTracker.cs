// <copyright file="MirametrixTracker.cs" company="Mirametrix">
// ******************************************************
// OGAMA - open gaze and mouse analyzer 
// Copyright (C) 2013 Dr. Adrian Voßkühler  
// ------------------------------------------------------------------------
// This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
// This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
// You should have received a copy of the GNU General Public License along with this program; if not, write to the Free Software Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
// **************************************************************
// </copyright>
// <author>Herval Yatchou</author>
// <email>herval.yatchou@tandemlaunchtech.com</email>

namespace Ogama.Modules.Recording.MirametrixInterface
{
  using System;
  using System.Diagnostics;
  using System.Drawing;
  using System.Globalization;
  using System.IO;
  using System.Runtime.InteropServices;
  using System.Windows.Forms;
  using System.Xml;
  using System.Xml.Serialization;

  using GTCommons.Events;

  using Microsoft.Win32;

  using Ogama.ExceptionHandling;
  using Ogama.Modules.Common.CustomEventArgs;
  using Ogama.Modules.Common.Tools;
  using Ogama.Modules.Recording.Dialogs;
  using Ogama.Modules.Recording.TrackerBase;

  /// <summary>
  /// This class implements the <see cref="ITracker"/> interface to represent 
  /// an OGAMA known eyetracker.
  /// </summary>        
  public class MirametrixTracker : TrackerWithStatusControls
  {
    ///////////////////////////////////////////////////////////////////////////////
    // Defining Variables, Enumerations, Events                                  //
    ///////////////////////////////////////////////////////////////////////////////
    #region FIELDS

    /// <summary>
    /// This is the label on the calibration result panel
    /// to show the quality status of the calibration.
    /// </summary>
    private Label memCalibrationResult;

    /// <summary>
    /// This is the customized tab page for the Mirametrix tracker.
    /// </summary>
    private TabPage memTabPage;

    /// <summary>
    /// Saves Mirametrix settings
    /// </summary>
    private MirametrixSetting memSettings;

    /// <summary>
    /// Saves the track status dialog that can be shown
    /// to the subject before calibration or during
    /// tracking.
    /// </summary>
    private MirametrixTrackStatus memDlgTrackStatus;

    /// <summary>
    /// Network manager to manage interactions with the tracker
    /// </summary>
    private ClientNetworkManager memNetworkManager;

    /// <summary>
    /// Specify if we are calibrating (true )or not (false)
    /// </summary>
    private bool memIsCalibrating;

    /// <summary>
    /// Specify if we are recording (true )or not (false)
    /// </summary>
    private bool memIsRecording;

    /// <summary>
    /// Specify the time when recording started
    /// </summary>
    private Stopwatch memTimeOfRecordingStart;

    /// <summary>
    /// Specify the time when recording started
    /// </summary>
    private int[] memConnectionsIds;

    /// <summary>
    /// Xml document which contains all parameterizations
    /// </summary>
    private XmlDocument memXmlDocument;

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
    private MirametrixTrackStatusControl memMirametrixTrackStatus;

    #endregion

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTORS

    /// <summary>
    /// Initializes a new instance of the MirametrixTracker class.
    /// </summary>
    /// <param name="mirametrixResultLabel">The label on the calibration result panel
    /// to show the quality status of the calibration.</param>
    /// <param name="mirametrixTabpage">The customized tab page for the Mirametrix tracker.</param>
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
    /// named "ShowOnPresentationScreenButton" at the tab page of the Mirametrix device.</param>
    /// <param name="trackerAcceptButton">The <see cref="Button"/>
    /// named "Accept" at the tab page of the Mirametrix device.</param>
    /// <param name="trackerRecalibrateButton">The <see cref="Button"/>
    /// named "Recalibrate" at the tab page of the Mirametrix device.</param>
    /// <param name="trackerConnectButton">The <see cref="Button"/>
    /// named "Connect" at the tab page of the Mirametrix device.</param>
    /// <param name="trackerSubjectButton">The <see cref="Button"/>
    /// named "Subject" at the tab page of the Mirametrix device.</param>
    /// <param name="trackerCalibrateButton">The <see cref="Button"/>
    /// named "Calibrate" at the tab page of the Mirametrix device.</param>
    /// <param name="trackerRecordButton">The <see cref="Button"/>
    /// named "Record" at the tab page of the Mirametrix device.</param>
    /// <param name="trackerSubjectNameTextBox">The <see cref="TextBox"/>
    /// which should contain the subject name at the tab page of the Mirametrix device.</param>
    public MirametrixTracker(
      ref Label mirametrixResultLabel,
      TabPage mirametrixTabpage,
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
      Properties.Settings.Default.EyeTrackerSettingsPath + "Mirametrix.xml")
    {
      this.memCalibrationResult = mirametrixResultLabel;
      this.memTabPage = mirametrixTabpage;

      // Call the initialize methods of derived classes
      this.Initialize();
    }

    #endregion

    /// <summary>
    /// Delegate to process incoming messages
    /// </summary>
    /// <param name="messageEventArgs">Message to process</param>
    public delegate void ProcessMessageReceivedDelegate(StringEventArgs messageEventArgs);

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
    /// Gets Mirametrix settings
    /// </summary>
    public MirametrixSetting Settings
    {
      get
      {
        return this.memSettings;
      }
    }

    /// <summary>
    /// Gets the connection status of the Mirametrix tracker
    /// </summary>
    public override bool IsConnected
    {
      get
      {
        return this.memNetworkManager.AllConnected;
      }
    }

    #endregion

    ///////////////////////////////////////////////////////////////////////////////
    // Methods                                                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region METHODS

    /// <summary>
    /// Checks if the mirametrix tracker is available in the system.
    /// </summary>
    /// <param name="errorMessage">Out. A <see cref="String"/> with an error message.</param>
    /// <returns><strong>True</strong>, if Mirametrix tracker
    /// is available in the system, otherwise <strong>false</strong></returns>
    public static bool IsAvailable(out string errorMessage)
    {
      errorMessage = string.Empty;

      // Check Mirametrix process
      if (!IsProcessOpen("tracker"))
      {
        if (!IsApplicationInstalled("Mirametrix"))
        {
          errorMessage = "Can't find Mirametrix S2 Eye Tracker on this computer. Maybe Mirametrix S2 Eye Tracker is not installed or installation is corrupted." + Environment.NewLine + "Please reinstall Mirametrix S2 Eye Tracker.";
          return false;
        }
        else
        {
          errorMessage = "Warning : Mirametrix S2 Eye tracker does not seem to be connected on this computer. Please connect Mirametrix S2 Eye tracker.";
          return true;
        }
      }

      return true;
    }

    /// <summary>
    /// Connect to the mirametrix system.
    /// </summary>
    /// <returns><strong>True</strong> if connection succeded, otherwise
    /// <strong>false</strong>.</returns>
    public override bool Connect()
    {
      bool connectionSucceeded;
      try
      {
        connectionSucceeded = this.memNetworkManager.Connect();

        // Get the displayed screen for configure Ogama having the same
        this.memNetworkManager.SendMessage("<GET ID=\"SCREEN_SELECTED\" />\r\n");
        if (this.memSettings.CalibrationType.Equals(1))
        {
          this.Calibrate(false);
        }
      }
      catch (Exception ex)
      {
        var dlg = new ConnectionFailedDialog { ErrorMessage = ex.Message };
        dlg.ShowDialog();
        this.CleanUp();
        return false;
      }

      return connectionSucceeded;
    }

    /// <summary>
    /// Starts calibration.
    /// </summary>
    /// <param name="isRecalibrating">whether to use recalibration or not.</param>
    /// <returns><strong>True</strong> if calibration succeded, otherwise
    /// <strong>false</strong>.</returns>
    public override bool Calibrate(bool isRecalibrating)
    {
      // TODO : Forbit or reset calibration if the calibrate button is clicked when calibrating
      try
      {
        if (this.memNetworkManager.AllConnected)
        {
          // TODO : Make possible to stop calibration 
          if (!this.memIsCalibrating)
          {
            this.memIsCalibrating = true;

            if (this.memSettings.IsCalibFast)
            {
              this.memNetworkManager.SendMessage("<SET ID=\"CALIBRATE_FAST\" STATE=\"1\" />\r\n");
            }
            else
            {
              this.memNetworkManager.SendMessage("<SET ID=\"CALIBRATE_FAST\" STATE=\"0\" />\r\n");
              string tmpMess = "<SET ID=\"CALIBRATE_TIMEOUT\" VALUE=\"" + this.memSettings.CalibPointSpeed.ToString() + "\" />\r\n";
              this.memNetworkManager.SendMessage(tmpMess/*"<SET ID=\"CALIBRATE_TIMEOUT\" VALUE=\"5.00\" />\r\n"*/);
            }

            this.memNetworkManager.SendMessage("<SET ID=\"CALIBRATE_SHOW\" STATE=\"1\" />\r\n");
            this.memNetworkManager.SendMessage("<SET ID=\"CALIBRATE_START\" STATE=\"1\" />\r\n");
          }
        }
      }
      catch (Exception e)
      {
        var message = "Exception catched in Calibrate(...) Method : " +
              Environment.NewLine + e.Message;
        ExceptionMethods.ProcessErrorMessage(message);
        MessageBox.Show("Failed to calibrate. Got exception " + e, "Calibration Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }

      return true;
    }

    /// <summary>
    /// Clean up objects.
    /// </summary>
    public override void CleanUp()
    {
      this.Stop();
      this.memNetworkManager.Disconnect();
      this.memTimeOfRecordingStart.Reset();
      base.CleanUp();
    }

    /// <summary>
    /// Start tracking.
    /// </summary>
    public override void Record()
    {
      try
      {
        if (this.memNetworkManager.AllConnected)
        {
          if (!this.memIsRecording)
          {
            this.memIsRecording = true;
            this.memTimeOfRecordingStart.Reset();
            this.memTimeOfRecordingStart.Start();
            this.memNetworkManager.SendMessage("<SET ID=\"ENABLE_SEND_POG_BEST\" STATE=\"1\"/> \r\n");
            this.memNetworkManager.SendMessage("<SET ID=\"ENABLE_SEND_PUPIL_LEFT\" STATE=\"1\"/> \r\n");
            this.memNetworkManager.SendMessage("<SET ID=\"ENABLE_SEND_PUPIL_RIGHT\" STATE=\"1\"/> \r\n");
            this.memNetworkManager.SendMessage("<SET ID=\"ENABLE_SEND_DATA\" STATE=\"1\" /> \r\n");
          }
        }
      }
      catch (Exception e)
      {
        string message = "Exception catched in Record(...) Method : " +
            Environment.NewLine + e.Message;
        ExceptionMethods.ProcessErrorMessage(message);
        MessageBox.Show("Failed to Record. Got exception " + e, "Record Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    /// <summary>
    /// Stops tracking.
    /// </summary>
    public override void Stop()
    {
      this.memIsRecording = false;
      this.memIsCalibrating = false;
      this.memTimeOfRecordingStart.Stop();
      this.memNetworkManager.SendMessage("<SET ID=\"ENABLE_SEND_DATA\" STATE=\"0\" /> \r\n");
    }

    /// <summary>
    /// Raises MirametrixSettingDialog to change the settings
    /// for this interface.
    /// </summary>
    public override void ChangeSettings()
    {
      var dlg = new MirametrixSettingsDialog { MirametrixSetting = this.memSettings };
      string prevAdd = this.memSettings.ServerAddress;
      int prevPort = this.memSettings.ServerPort;
      if (dlg.ShowDialog() == DialogResult.OK)
      {
        // If settings has changed, and we where connected, we need to disconnect
        if (!prevAdd.Equals(this.memSettings.ServerAddress) ||
            !prevPort.Equals(this.memSettings.ServerPort))
        {
          this.CleanUp();
          this.memNetworkManager.ChangePort(this.memConnectionsIds[0], dlg.MirametrixSetting.ServerPort);
          this.memNetworkManager.ChangeAddress(this.memConnectionsIds[0], dlg.MirametrixSetting.ServerAddress);
        }

        this.memSettings = dlg.MirametrixSetting;
        this.SerializeSettings(this.Settings, this.SettingsFile);
      }
    }

    ///////////////////////////////////////////////////////////////////////////////
    // Inherited methods                                                         //
    ///////////////////////////////////////////////////////////////////////////////

    /// <summary>
    /// Overridden. Dispose the <see cref="Tracker"/> if applicable
    /// by a call to <see cref="ITracker.CleanUp()"/> and 
    /// removing the connected event handlers.
    /// </summary>
    public override void Dispose()
    {
      base.Dispose();
      this.memIsCalibrating = false;
      this.memIsRecording = false;
      this.memNetworkManager.MessageReceived -= new NewMessageEventHandler(this.ProcessReceivedMessage);
    }

    /// <summary>
    /// Process received message from the tracker
    /// </summary>
    /// <param name="messageEventArgs">Message to process</param>
    protected void ProcessReceivedMessage(StringEventArgs messageEventArgs)
    {
      // CANDO : Separate this method in submethods for each process depending on received message
      try
      {
        if (this.RecordModule.InvokeRequired)
        {
          this.RecordModule.BeginInvoke(new ProcessMessageReceivedDelegate(this.ProcessReceivedMessage), new object[] { messageEventArgs });
        }
        else
        {
          string grossMessage = messageEventArgs.Param;
          int endIdex = grossMessage.IndexOf("\r\n");
          if (endIdex != -1)
          {
            string availableMessage = grossMessage.Substring(0, endIdex);
            XmlDocument doc = new XmlDocument();
            doc.InnerXml = availableMessage;
            XmlElement root = doc.DocumentElement;
            string attribute;

            if (root != null)
            {
              if (root.Name == "ACK")
              {
                attribute = root.GetAttribute("ID");
                if (attribute == "CALIBRATE_RESULT_SUMMARY")
                {
                  this.memCalibrationResult.Text = root.GetAttribute("AVE_ERROR");
                  if (this.memSettings.HideCalibWindow)
                  {
                    this.memNetworkManager.SendMessage("<SET ID=\"CALIBRATE_SHOW\" STATE=\"0\" />\r\n");
                  }
                }
                else if (attribute == "SCREEN_SELECTED")
                {
                  if (root.GetAttribute("VALUE") == "0")
                  {
                    Properties.Settings.Default.PresentationScreenMonitor = "Primary";
                  }
                  else if (root.GetAttribute("VALUE") == "1")
                  {
                    Properties.Settings.Default.PresentationScreenMonitor = "Secondary";
                  }
                }
              }
              else if (root.Name == "CAL")
              {
                attribute = root.GetAttribute("ID");
                if (attribute == "CALIB_RESULT")
                {
                  // TODO : Hide calibration result screen after calibration
                  this.memIsCalibrating = false;
                  this.memNetworkManager.SendMessage("<GET ID=\"CALIBRATE_RESULT_SUMMARY\" />\r\n");
                  this.ShowCalibPlot();
                }
              }
              else if (root.Name == "REC")
              {
                // TODO : Optimize data send to Ogama by verifying if they are valid
                var newGazeData = new GazeData();

                // Get gazeTimestamp in milliseconds.
                newGazeData.Time = this.memTimeOfRecordingStart.ElapsedMilliseconds;

                // Calculate values between 0..1
                attribute = root.GetAttribute("BPOGX");
                newGazeData.GazePosX = float.Parse(attribute, CultureInfo.InvariantCulture);
                attribute = root.GetAttribute("BPOGY");
                newGazeData.GazePosY = float.Parse(attribute, CultureInfo.InvariantCulture);

                // Set pupil diameter
                attribute = root.GetAttribute("LPD");
                newGazeData.PupilDiaX = float.Parse(attribute, CultureInfo.InvariantCulture);
                attribute = root.GetAttribute("RPD");
                newGazeData.PupilDiaY = float.Parse(attribute, CultureInfo.InvariantCulture);
                this.OnGazeDataChanged(new GazeDataChangedEventArgs(newGazeData));

                // Values needed by the trackstatus windows
                attribute = root.GetAttribute("LPS");
                float lED = float.Parse(attribute, CultureInfo.InvariantCulture);
                attribute = root.GetAttribute("RPS");
                float rED = float.Parse(attribute, CultureInfo.InvariantCulture);
                float averageRD = (lED + rED) / 2;
                if (this.memDlgTrackStatus != null)
                {
                  this.memDlgTrackStatus.UpdateStatus((float)newGazeData.GazePosX, (float)newGazeData.GazePosY, averageRD);
                }
              }
            }
          }
        }
      }
      catch (Exception e)
      {
        string message = "Exception catched in ProcessReceivedMessage(...) Method : " +
            Environment.NewLine + e.Message;
        ExceptionMethods.ProcessErrorMessage(message);
      }
    }

    /// <summary>
    /// This method initializes the designer components for the
    /// Mirametrix interface tab page.    
    /// </summary>
    protected override void InitializeStatusControls()
    {
      if (this.memMirametrixTrackStatus == null)
      {
        this.memMirametrixTrackStatus = new MirametrixTrackStatusControl();

        // TobiiTrackStatus
        this.memMirametrixTrackStatus.Dock = DockStyle.Fill;
        this.memMirametrixTrackStatus.Enabled = true;
        this.memMirametrixTrackStatus.Location = new Point(0, 0);
        this.memMirametrixTrackStatus.Name = "mirametrixTrackStatus";
        this.memMirametrixTrackStatus.Size = new Size(190, 54);
        this.memMirametrixTrackStatus.TabIndex = 0;

        try
        {
          this.TrackStatusPanel.Controls.Add(this.memMirametrixTrackStatus);
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
        if (this.memDlgTrackStatus != null)
        {
          this.memDlgTrackStatus.Dispose();
        }

        this.memDlgTrackStatus = new MirametrixTrackStatus();

        Rectangle presentationBounds = PresentationScreen.GetPresentationWorkingArea();

        this.memDlgTrackStatus.Location = new Point(
                            presentationBounds.Left + presentationBounds.Width / 2 - this.memDlgTrackStatus.Width / 2,
                            presentationBounds.Top + presentationBounds.Height / 2 - this.memDlgTrackStatus.Height / 2);

        // Dialog will be disposed when connection failed.
        if (!this.memDlgTrackStatus.IsDisposed)
        {
          this.ShowOnSecondaryScreenButton.Text = "Hide from presentation screen";
          this.ShowOnSecondaryScreenButton.BackColor = Color.Red;
          this.memDlgTrackStatus.Show();
        }
      }
      else
      {
        // Should hide TrackStatusDlg
        if (this.memNetworkManager != null)
        {
          this.ShowOnSecondaryScreenButton.BackColor = Color.Transparent;
          this.ShowOnSecondaryScreenButton.Text = "Show on presentation screen";
          this.memDlgTrackStatus.Close();
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
      this.memCalibrationResult.Text = "Not Set !";
      this.CalibrationResultPanel.BackColor = Color.Transparent;
    }

    /// <summary>
    /// Sets up calibration procedure and the tracking client
    /// and wires the events. Reads settings from file.
    /// </summary>
    protected override void Initialize()
    {
      // Load Mirametrix tracker settings.
      if (File.Exists(this.SettingsFile))
      {
        this.memSettings = this.DeserializeSettings(this.SettingsFile);
      }
      else
      {
        this.memSettings = new MirametrixSetting();
        this.SerializeSettings(this.memSettings, this.SettingsFile);
      }

      // We just need one connection for now. Maybe for subsequent development, we will be able to connect to many trackers
      this.memNetworkManager = new ClientNetworkManager(this.memSettings.ServerAddress, this.memSettings.ServerPort, out this.memConnectionsIds);
      this.memNetworkManager.MessageReceived += new NewMessageEventHandler(this.ProcessReceivedMessage);
      this.memIsCalibrating = false;
      this.memIsRecording = false;
      this.memXmlDocument = new XmlDocument();
      this.memTimeOfRecordingStart = new Stopwatch();
      base.Initialize();
    }

    /// <summary>
    /// Overridden.
    /// Check visibility of the track status window before starting to record.
    /// </summary>
    protected override void PrepareRecording()
    {
      // Hide Trackstatus on presentation screen
      if (this.memDlgTrackStatus != null && this.memDlgTrackStatus.Visible)
      {
        // Hide TrackStatusDlg
        this.ShowOnSecondaryScreenButton.BackColor = Color.Transparent;
        this.ShowOnSecondaryScreenButton.Text = "Show on presentation screen";
        this.memDlgTrackStatus.Close();
      }
    }

    /// <summary>
    /// Searchs a specified process
    /// </summary>
    /// <param name="name">Name of process, without .exe or .dll</param>
    /// <returns>True if process is running, false otherwise</returns>
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
    /// Searchs a specified application in windows registries
    /// </summary>
    /// <param name="appName">Name of application</param>
    /// <returns>True if application installed, false otherwise</returns>
    private static bool IsApplicationInstalled(string appName)
    {
      string keyName;

      // search in: CurrentUser
      keyName = @"SOFTWARE";
      if (ExistsInSubKey(Registry.CurrentUser, keyName, "STARTMENU_REGISTRYNAME", appName) == true)
      {
        return true;
      }

      // search in: LocalMachine_32            
      if (ExistsInSubKey(Registry.LocalMachine, keyName, "STARTMENU_REGISTRYNAME", appName) == true)
      {
        return true;
      }

      // search in: LocalMachine_64
      keyName = @"SOFTWARE\Wow6432Node";
      if (ExistsInSubKey(Registry.LocalMachine, keyName, "STARTMENU_REGISTRYNAME", appName) == true)
      {
        return true;
      }

      return false;
    }

    /// <summary>
    /// Find matching application's name with specified subkey's name in subkeys of a root registry directory
    /// </summary>
    /// <param name="root">Registry root</param>
    /// <param name="subKeyName">Searching root</param>
    /// <param name="attributeName">Subkey name to find</param>
    /// <param name="appName">Application name</param>
    /// <returns>True if we found matching subkey name, false otherwise</returns>
    private static bool ExistsInSubKey(RegistryKey root, string subKeyName, string attributeName, string appName)
    {
      RegistryKey subkey;
      string displayName;

      using (RegistryKey key = root.OpenSubKey(subKeyName))
      {
        if (key != null)
        {
          foreach (string kn in key.GetSubKeyNames())
          {
            using (subkey = key.OpenSubKey(kn))
            {
              displayName = subkey.GetValue(attributeName) as string;
              if (appName.Equals(displayName, StringComparison.OrdinalIgnoreCase) == true)
              {
                return true;
              }
            }
          }
        }
      }

      return false;
    }

    /// <summary>
    /// Deserializes the <see cref="MirametrixSetting"/> from the given xml file.
    /// </summary>
    /// <param name="filePath">Full file path to the xml settings file.</param>
    /// <returns>A <see cref="MirametrixSetting"/> object.</returns>
    private MirametrixSetting DeserializeSettings(string filePath)
    {
      MirametrixSetting settings = new MirametrixSetting();

      // Create an instance of the XmlSerializer class;
      // specify the type of object to be deserialized 
      XmlSerializer serializer = new XmlSerializer(typeof(MirametrixSetting));

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
        settings = (MirametrixSetting)serializer.Deserialize(fs);
        fs.Close();
      }
      catch (Exception ex)
      {
        InformationDialog.Show(
          "Error occured",
          "Deserialization ofMirametrixSettings failed with the following message: " + Environment.NewLine + ex.Message,
          false,
          MessageBoxIcon.Error);
      }

      return settings;
    }

    /// <summary>
    /// Serializes the <see cref="MirametrixSetting"/> into the given file in a xml structure.
    /// </summary>
    /// <param name="settings">The <see cref="MirametrixSetting"/> object to serialize.</param>
    /// <param name="filePath">Full file path to the xml settings file.</param>
    private void SerializeSettings(MirametrixSetting settings, string filePath)
    {
      // Create an instance of the XmlSerializer class;
      // specify the type of object to serialize 
      XmlSerializer serializer = new XmlSerializer(typeof(MirametrixSetting));

      // Serialize the MirametrixSetting, and close the TextWriter.
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
          "Serialization of MirametrixSettings failed with the following message: " + Environment.NewLine + ex.Message,
          false,
          MessageBoxIcon.Error);
      }
    }
    #endregion // METHODS
  }
}