// <copyright file="MirametrixTracker.cs" company="Mirametrix">
// ******************************************************
// OGAMA - open gaze and mouse analyzer 
// Copyright (C) 2010 Adrian Voßkühler  
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
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;
    using System.Xml.Serialization;
    using System.Xml;
    using Ogama.Modules.Recording.TrackerBase;
    using Microsoft.Win32;
    using Ogama.ExceptionHandling;
    using Ogama.Modules.Common;

   
   
    

    /// <summary>
    /// This class implements the <see cref="ITracker"/> interface to represent 
    /// an OGAMA known eyetracker.
    /// </summary>    
    
    public class MirametrixTracker : TrackerWithStatusControls
    {


        ///////////////////////////////////////////////////////////////////////////////
        // Defining Variables, Enumerations, Events                                  //
        ///////////////////////////////////////////////////////////////////////////////
        #region Properties

        /// <summary>
        /// This is the label on the calibration result panel
        /// to show the quality status of the calibration.
        /// </summary>
        private Label _calibrationResult;

        /// <summary>
        /// This is the customized tab page for the Mirametrix tracker.
        /// </summary>
        private TabPage _tabPage;     
        private MirametrixSetting _settings;    // TODO : A Mirametrix setting windows


        /// <summary>
        /// Saves the track status dialog that can be shown
        /// to the subject before calibration or during
        /// tracking.
        /// </summary>
        private MirametrixTrackStatus _dlgTrackStatus;  // TODO : A mirametrix track status windows

        private ClientNetworkManager _networkManager;
        private bool _isCalibrating;
        private bool _isRecording;
        private Stopwatch _time_of_recording_start;
        private XmlDocument _XmlDocument;

        /// <summary>
        /// Delegate to process incoming messages
        /// </summary>
        /// <param name="messageEventArgs"></param>
        public delegate void ProcessMessageReceivedDelegate(MessageReceivedEventArgs messageEventArgs);
        #endregion

        ///////////////////////////////////////////////////////////////////////////////
        // Construction and Initializing methods                                     //
        ///////////////////////////////////////////////////////////////////////////////
        #region Constructors and Destructors

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
            _calibrationResult = mirametrixResultLabel;
            _tabPage = mirametrixTabpage;

            

            // Call the initialize methods of derived classes
            this.Initialize();
        }

        #endregion 

        ///////////////////////////////////////////////////////////////////////////////
        // Defining Enumerations                                                     //
        ///////////////////////////////////////////////////////////////////////////////
        #region ENUMS
        #endregion ENUMS

        ///////////////////////////////////////////////////////////////////////////////
        // Defining Properties                                                       //
        ///////////////////////////////////////////////////////////////////////////////
        #region Properties

       /// <summary>
       /// Get Mirametrix settings
       /// </summary>
        public MirametrixSetting Settings
        {
            get { return _settings; }
        }

        /// <summary>
        /// Gets the connection status of the Mirametrix tracker
        /// </summary>
        public override bool IsConnected
        {
            get
            {
                return _networkManager.AllConnected;
            }
        }

        #endregion

        ///////////////////////////////////////////////////////////////////////////////
        // Methods                                                            //
        ///////////////////////////////////////////////////////////////////////////////
        #region Methods

       
        /// <summary>
        /// Checks if the mirametrix tracker is available in the system.
        /// </summary>
        /// <param name="errorMessage">Out. A <see cref="String"/> with an error message.</param>
        /// <returns><strong>True</strong>, if Mirametrix tracker with intelligaze
        /// is available in the system, otherwise <strong>false</strong></returns>
        public static bool IsAvailable(out string errorMessage)
        {
            //TODO : Conditions for mirametrix tracker availability
            errorMessage = string.Empty;
            return true;
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

        /// <summary>
        /// Process received message from the tracker
        /// </summary>
        /// <param name="messageEventArgs"></param>
        /// 

        protected void ProcessReceivedMessage(MessageReceivedEventArgs messageEventArgs)
        {
            // CANDO : Separate this method in submethods for each process depending on received message
            try
            {
                if (this.RecordModule.InvokeRequired)
                {
                    this.RecordModule.BeginInvoke(new ProcessMessageReceivedDelegate(ProcessReceivedMessage), new object[] { messageEventArgs });
                }
                else
                {
                    string grossMessage = messageEventArgs.MessageReceived;
                    int endIdex = grossMessage.IndexOf("\r\n");
                    if (endIdex != -1)
                    {
                        string availableMessage = grossMessage.Substring(0, (endIdex));
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
                                    this._calibrationResult.Text = root.GetAttribute("AVE_ERROR");

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
                                    _isCalibrating = false;
                                    _networkManager.SendMessage("<GET ID=\"CALIBRATE_RESULT_SUMMARY\" />\r\n");
                                    this.ShowCalibPlot();

                                }

                            }
                            else if (root.Name == "REC")
                            {
                                // TODO : Optimize data send to Ogama by verifying if they are valid

                                var newGazeData = new GazeData();
                                // Get gazeTimestamp in milliseconds.
                                newGazeData.Time = _time_of_recording_start.ElapsedMilliseconds;

                                // Calculate values between 0..1
                                attribute = root.GetAttribute("BPOGX");
                                newGazeData.GazePosX = float.Parse(attribute);
                                attribute = root.GetAttribute("BPOGY");
                                newGazeData.GazePosY = float.Parse(attribute);

                                // Set pupil diameter
                                attribute = root.GetAttribute("LPD");
                                newGazeData.PupilDiaX = float.Parse(attribute);
                                attribute = root.GetAttribute("RPD");
                                newGazeData.PupilDiaY = float.Parse(attribute);
                                this.OnGazeDataChanged(new GazeDataChangedEventArgs(newGazeData));

                            }

                        }

                    }

                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception catched in ProcessReceivedMessage(...) Method : ", e);
            }

        }

        #region ITrackerInterfaceImplementation

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
                connectionSucceeded = _networkManager.Connect();
                _networkManager.SendMessage("<GET ID=\"SCREEN_SELECTED\" />\r\n");  // Get the displayed screen for configure Ogama having the same
            }
            catch (Exception ex)
            {
                var dlg = new ConnectionFailedDialog { ErrorMessage = ex.Message };
                dlg.ShowDialog();
                this.CleanUp();
                return  false;
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
                if (_networkManager.AllConnected)
                {
                    // TODO : Make possible to stop calibration 
                    if (!_isCalibrating)
                    {
                        _isCalibrating = true;
                        _networkManager.SendMessage("<SET ID=\"CALIBRATE_SHOW\" STATE=\"1\" />\r\n");
                        _networkManager.SendMessage("<SET ID=\"CALIBRATE_START\" STATE=\"1\" />\r\n");
                    }                    
                }
            }
            catch (Exception e) {
                Console.WriteLine("Exception catched in Calibrate(...) Method : ", e);
                MessageBox.Show("Failed to calibrate. Got exception " + e, "Calibration Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return true;
        }

        /// <summary>
        /// Clean up objects.
        /// </summary>
        public override void CleanUp()
        {
            Stop();
            _networkManager.Disconnect();
            _time_of_recording_start.Reset();
            base.CleanUp();
        }

        /// <summary>
        /// Start tracking.
        /// </summary>
        public override void Record()
        {
            try
            {
                if (_networkManager.AllConnected)
                {

                    if (!_isRecording)
                    {
                        _isRecording = true;
                        _time_of_recording_start.Reset();
                        _time_of_recording_start.Start();
                        _networkManager.SendMessage("<SET ID=\"ENABLE_SEND_POG_BEST\" STATE=\"1\"/> \r\n");
                        _networkManager.SendMessage("<SET ID=\"ENABLE_SEND_PUPIL_LEFT\" STATE=\"1\"/> \r\n");
                        _networkManager.SendMessage("<SET ID=\"ENABLE_SEND_PUPIL_RIGHT\" STATE=\"1\"/> \r\n");                       
                        _networkManager.SendMessage("<SET ID=\"ENABLE_SEND_DATA\" STATE=\"1\" /> \r\n");
                    }
                    
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception catched in Record(...) Method : ", e);
                MessageBox.Show( "Failed to Record. Got exception " + e, "Record Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        /// <summary>
        /// Stops tracking.
        /// </summary>
        public override void Stop()
        {
            _isRecording = false;
            _isCalibrating = false;
            _time_of_recording_start.Stop();
        }


        /// <summary>
        /// Raises MirametrixSettingDialog to change the settings
        /// for this interface.
        /// </summary>
        public override void ChangeSettings()
        {
            // TODO : Create a mirametrix windows to change settings
        }

        #endregion //ITrackerInterfaceImplementation

        #endregion // Methods

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
            _isCalibrating = false;
            _isRecording = false;
            _networkManager.MessageReceived -= new NewMessageEventHandler(ProcessReceivedMessage);
        }

        /// <summary>
        /// This method initializes the designer components for the
        /// Mirametrix interface tab page.    
        /// </summary>
        protected override void InitializeStatusControls()
        {
            
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
                if (this._dlgTrackStatus != null)
                {
                    this._dlgTrackStatus.Dispose();
                }

                this._dlgTrackStatus = new MirametrixTrackStatus();

                Rectangle presentationBounds = PresentationScreen.GetPresentationWorkingArea();

                this._dlgTrackStatus.Location = new Point(
                                    presentationBounds.Left + presentationBounds.Width / 2 - this._dlgTrackStatus.Width / 2,
                                    presentationBounds.Top + presentationBounds.Height / 2 - this._dlgTrackStatus.Height / 2);

                // Dialog will be disposed when connection failed.
                if (!this._dlgTrackStatus.IsDisposed)
                {
                    this.ShowOnSecondaryScreenButton.Text = "Hide from presentation screen";
                    this.ShowOnSecondaryScreenButton.BackColor = Color.Red;
                    this._dlgTrackStatus.Show();
                }
            }
            else
            {
                // Should hide TrackStatusDlg
                if (this._networkManager != null)
                {
                    this.ShowOnSecondaryScreenButton.BackColor = Color.Transparent;
                    this.ShowOnSecondaryScreenButton.Text = "Show on presentation screen";
                    this._dlgTrackStatus.Close();
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
            _calibrationResult.Text = "Not Set !";
            this.CalibrationResultPanel.BackColor = Color.Transparent;
        }

        /// <summary>
        /// Sets up calibration procedure and the tracking client
        /// and wires the events. Reads settings from file.
        /// </summary>
        protected override void Initialize()
        {
            // WARNING : NUMBER OF SERVER HARDODED
            // TODO : Make it more dynamic
            int numberOfConnexions = 1;
            string[] adresses = new string[1];
            adresses[0] = "127.0.0.1";
            int[] ports = new int[1];
            ports[0] = 4242;
            _networkManager = new ClientNetworkManager(numberOfConnexions, adresses, ports);
            _networkManager.MessageReceived += new NewMessageEventHandler(ProcessReceivedMessage);
            _isCalibrating = false;
            _isRecording = false;
            _XmlDocument = new XmlDocument();
            _time_of_recording_start = new Stopwatch();
            base.Initialize();
        }

        /// <summary>
        /// Overridden.
        /// Check visibility of the track status window before starting to record.
        /// </summary>
        protected override void PrepareRecording()
        {
            // Hide Trackstatus on presentation screen
            if (this._dlgTrackStatus != null && this._dlgTrackStatus.Visible)
            {
                // Hide TrackStatusDlg
                this.ShowOnSecondaryScreenButton.BackColor = Color.Transparent;
                this.ShowOnSecondaryScreenButton.Text = "Show on presentation screen";
                this._dlgTrackStatus.Close();
            }
        }
        #endregion 
    }
}