// <copyright file="ASLTracker.cs" company="FU Berlin">
// ******************************************************
// OGAMA - open gaze and mouse analyzer 
// Copyright (C) 2013 Dr. Adrian Voßkühler  
// ------------------------------------------------------------------------
// This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
// This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
// You should have received a copy of the GNU General Public License along with this program; if not, write to the Free Software Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
// **************************************************************
// </copyright>
// <author>
//  University Toulouse 2 - CLLE-LTC UMR5263
//  Yves LECOURT
//  Modifications : Smaïl KHAMED
//  </author>
// <email>virginie.feraud@univ-tlse2.fr</email>

namespace Ogama.Modules.Recording.ASLInterface
{
  using System;
  using System.Diagnostics;
  using System.Drawing;
  using System.IO;
  using System.Windows.Forms;

  using ASLSERIALOUTLIB2Lib;

  using Ogama.ExceptionHandling;
  using Ogama.Modules.Common.CustomEventArgs;
  using Ogama.Modules.Common.Tools;
  using Ogama.Modules.Recording.TrackerBase;

  /// <summary>
  /// This class implements the <see cref="ITracker"/> interface to represent 
  /// an OGAMA known eyetracker.
  /// It encapsulates a ASL http://asleyetracking.com/site/ eyetracker 
  /// which requirs ASLSerialOutLib2.dll
  /// It is tested with the ASL Model 504 with Pan/Tilt Optics.
  /// </summary>
  public class AslTracker : Tracker
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
    /// "Port COM" object
    /// </summary>
    private ASLSerialOutPort3Class aslPort;

    /// <summary>
    /// The Eye Tracker gives horizontal coordinates included between 0 and 260
    /// (at the right edge).
    /// </summary>
    private short maxHorizontal;

    /// <summary>
    /// The Eye Tracker gives vertical coordinates included between 0 and 240
    /// (at the bottom of the monitore).
    /// </summary>
    private short maxVertical;

    /// <summary>
    /// The opposite border of <see cref="maxHorizontal"/> coordinate.
    /// </summary>
    private short minHorizontal;

    /// <summary>
    /// The opposite border of <see cref="maxVertical"/> coordinate.
    /// </summary>
    private short minVertical;

    /// <summary>
    /// Number of data in the items array returned by the ASLSerialOutPort2Class.GetDataRecord method.
    /// </summary>
    private byte dataCount;

    /// <summary>
    /// To convert recorded pupil diameter values to millimeters.
    /// </summary>
    private float pupilScaleFactor;

    /// <summary>
    /// Out parameter of the GetDataRecord dll method.
    /// </summary>
    private bool available;

    /// <summary>
    /// The current <see cref="UserSettings"/>
    /// </summary>
    private UserSettings settings;

    /// <summary>
    /// Saves the track status dialog that can be shown to the subject during calibration.
    /// </summary>
    private AslTrackStatus dlgTrackStatus;

    /// <summary>
    /// Attribute which indicate whether the tracker is connected.
    /// </summary>
    private bool isConnected;

    /// <summary>
    /// An array of data feild returned by the Eye Tracker : status, Pupil diameter,
    /// Point of gaze horizontal and vertical coordinates.
    /// </summary>
    private Array items;

    /// <summary>
    /// Number of data in the items array. Out parameter of the GetDataRecord dll method.
    /// </summary>
    private int itemCount;

    /// <summary>
    /// Save the time between the record beginning and the last sample transmitted.
    /// </summary>
    private long lastTimeStamp;

    /// <summary>
    /// Local data struct of <see cref="GazeData"/> type. This attribut is reset in the
    /// aslRawDataReceived() method which is called with a 60 Hertz frequency.
    /// So i prefer to create just one object which is initialized in Record() method.
    /// </summary>
    private GazeData newGazeData;

    /// <summary>
    /// A precise timer <see cref="System.Diagnostics.Stopwatch"/>
    /// for calculating the timestamp between two gaze samples.
    /// </summary>
    private Stopwatch stopwatch;

    /// <summary>
    /// A precise timer <see cref="System.Diagnostics.Stopwatch"/>
    /// for calculating the timeout for no pupil recognition.
    /// </summary>
    private Stopwatch stopwatch2;

    /// <summary>
    /// Path to the user settings file
    /// </summary>
    private string userSettingsFile;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION
    /// <summary>
    /// Initializes a new instance of the AslTracker class.
    /// </summary>
    /// <param name="owningRecordModule">The <see cref="RecordModule"/>
    /// form wich host the recorder.</param>
    /// <param name="trackerConnectButton">The <see cref="Button"/>
    /// named "Connect" at the tab page of the Asl device.</param>
    /// <param name="trackerSubjectButton">The <see cref="Button"/>
    /// named "Subject" at the tab page of the Asl device.</param>
    /// <param name="trackerCalibrateButton">The <see cref="Button"/>
    /// named "Calibrate" at the tab page of the Asl device.</param>
    /// <param name="trackerRecordButton">The <see cref="Button"/>
    /// named "Record" at the tab page of the Asl device.</param>
    /// <param name="trackerSubjectNameTextBox">The <see cref="TextBox"/>
    /// which should contain the subject name at the tab page of the Asl device.</param>
    public AslTracker(
        RecordModule owningRecordModule,
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
      Properties.Settings.Default.EyeTrackerSettingsPath + "ASLUserSettings.cfg")
    {
      this.UserSettingsFile = Properties.Settings.Default.EyeTrackerSettingsPath + "ASLUserSettings.cfg";
#if ASL
      this.Settings = UserSettings.Load(this.UserSettingsFile);
      this.CreateDefaultConfigFile();
      this.Settings.DefaultConfigFile = Properties.Settings.Default.EyeTrackerSettingsPath + "ASLStandardStreaming.cfg";

      if (this.Settings.ConfigFile == null)
      {
        this.Settings.ConfigFile = this.Settings.DefaultConfigFile;
      }

      this.Settings.Store(this.UserSettingsFile);
#endif

      // Call the "local" initialize method of derived class
      this.Initialize();

      // Set default values
      this.MinHorizontal = 0;
      this.MinVertical = 0;
      this.MaxHorizontal = 260;
      this.MaxVertical = 240;
      this.DataCount = 5;
      this.PupilScaleFactor = 0.15f;
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining events, enums, delegates                                         //
    ///////////////////////////////////////////////////////////////////////////////
    #region EVENTS
    #endregion EVENTS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets the path to the user settings file
    /// </summary>
    public string UserSettingsFile
    {
      get { return this.userSettingsFile; }
      private set { this.userSettingsFile = value; }
    }

    /// <summary>
    /// Gets the connection status of the eye tracker.
    /// </summary>
    public override bool IsConnected
    {
      get { return this.isConnected; }
    }

    /// <summary>
    /// Gets or sets the last time stamp of the gaze tracker.
    /// </summary>
    public long LastTimeStamp
    {
      get { return this.lastTimeStamp; }
      set { this.lastTimeStamp = value; }
    }

    /// <summary>
    /// Gets or sets the current ASL settings.
    /// </summary>
    /// <value>A <see cref="UserSettings"/> with the current tracker settings.</value>
    public UserSettings Settings
    {
      get { return this.settings; }
      set { this.settings = value; }
    }

    /// <summary>
    /// Gets or sets the minimal horizontal coordonate of the eye tracker
    /// </summary>
    private short MinHorizontal
    {
      get { return this.minHorizontal; }
      set { this.minHorizontal = value; }
    }

    /// <summary>
    /// Gets or sets the maximal horizontal coordonate of the eye tracker
    /// </summary>
    private short MaxHorizontal
    {
      get { return this.maxHorizontal; }
      set { this.maxHorizontal = value; }
    }

    /// <summary>
    /// Gets or sets the minimal vertical coordonate of the eye tracker
    /// </summary>
    private short MinVertical
    {
      get { return this.minVertical; }
      set { this.minVertical = value; }
    }

    /// <summary>
    /// Gets or sets the maximal vertical coordonate of the eye tracker
    /// </summary>
    private short MaxVertical
    {
      get { return this.maxVertical; }
      set { this.maxVertical = value; }
    }

    /// <summary>
    /// Gets or sets the number of data in the items array returned by the ASLSerialOutPort2Class.GetDataRecord method.
    /// </summary>
    private byte DataCount
    {
      get { return this.dataCount; }
      set { this.dataCount = value; }
    }

    /// <summary>
    /// Gets or sets the factor used to convert pupil diameter values to millimeters.
    /// </summary>
    private float PupilScaleFactor
    {
      get { return this.pupilScaleFactor; }
      set { this.pupilScaleFactor = value; }
    }

    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Public methods                                                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region PUBLICMETHODS

    /// <summary>
    /// These class method check if the asl tracker is available in the system.
    /// </summary>
    /// <param name="errorMessage">Out. A <see cref="String"/> with an error message.</param>
    /// <returns><strong>True</strong>, if Asl tracker is available in the system, 
    /// otherwise <strong>false</strong></returns>.
    public static TrackerStatus IsAvailable(out string errorMessage)
    {
      // Search if the needed dll exists in the Application
      errorMessage = string.Empty;
      string neededLibrary = Application.StartupPath +
          @"\Interop.ASLSERIALOUTLIB2Lib.dll";
      if (!File.Exists(neededLibrary))
      {
        errorMessage = "The needed library does not exist in the good" +
            " application directory.";
        return TrackerStatus.NotAvailable;
      }

      errorMessage = "The connection library is found, the tracker cannot automatically detected.";
      return TrackerStatus.Undetermined;
    }

    /// <summary>
    /// Close the serial port.
    /// </summary>
    public void CloseComPort()
    {
      this.aslPort.Disconnect();
    }

    #endregion //PUBLICMETHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Inherited methods                                                         //
    ///////////////////////////////////////////////////////////////////////////////
    #region OVERRIDES
    /// <summary>
    /// Connect to the asl eyetracker system.
    /// </summary>
    /// <returns><strong>True</strong> if connection succeded,
    /// otherwise <strong>false</strong>.</returns>
    public override bool Connect()
    {
      this.isConnected = true;

      // type of each item that the eye tracker should return
      System.Array itemTypes = Array.CreateInstance(typeof(int), this.DataCount);

      // Standard Serial Out data feild
      // byte description
      // 1    Status (0 = normal, >0 = error condition)
      // 2    Pupil diameter, most significant byte (0=loss)
      // 3    Pupil diameter, least significant byte
      // 4    <Used only by model 501 system with EYEHEAD Integration>
      // 5    Point of gaze horizontal coordinate most significant byte
      //      (scene monitor coordinates)
      // 6    Point of gaze horizontal coordinate least significant byte
      // 7    Point of gaze vertical coordinate most significant byte
      // 8    Point of gaze vertical coordinate least significant byte
      itemTypes.SetValue(EAslSerialOutPortType.ASL_TYPE_BYTE, 0);
      itemTypes.SetValue(EAslSerialOutPortType.ASL_TYPE_SHORT, 1);
      itemTypes.SetValue(EAslSerialOutPortType.ASL_TYPE_BYTE, 2);
      itemTypes.SetValue(EAslSerialOutPortType.ASL_TYPE_SHORT, 3);
      itemTypes.SetValue(EAslSerialOutPortType.ASL_TYPE_SHORT, 4);

      try
      {
        int baudRate, updateRate;
        bool streamingMode;

        // Initialize COM port, define message format, baud rate ...
        this.aslPort.Connect(
          this.Settings.ConfigFile,
          this.Settings.ComPortNo,
          this.Settings.EyeHead,
          out baudRate,
          out updateRate,
          out streamingMode,
          out this.itemCount,
          out itemTypes);

        if (!streamingMode)
        {
          MessageBox.Show(
            "You must use streaming-mode, check your configuration file into <Tracker Settings> menu.",
            "Error",
            MessageBoxButtons.OK,
            MessageBoxIcon.Error);
          this.aslPort.Disconnect();
          this.isConnected = false;
        }
        else
        {
          this.Settings.BaudRate = baudRate;
          this.Settings.UpdateRate = updateRate;
          this.Settings.Streaming = streamingMode;
#if ASL
          this.Settings.Store(this.SettingsFile);
#endif
        }

        return this.IsConnected;
      }
      catch (Exception ex)
      {
        if (!this.GetLastError("Connection port COM failed " + Environment.NewLine + ex.Message))
        {
          ExceptionMethods.ProcessErrorMessage("Connection port COM failed ");
        }

        this.CleanUp();
        this.isConnected = false;
        return this.IsConnected;
      }
    } // end of bool Connect()

    /// <summary>
    /// Starts to display the screens for the calibration.
    /// </summary>
    /// <param name="isRecalibrating">whether to use recalibration or not.</param>
    /// <returns><strong>True</strong> if calibration succeded, otherwise
    /// <strong>false</strong>.</returns>
    public override bool Calibrate(bool isRecalibrating)
    {
      if (this.dlgTrackStatus != null)
      {
        this.dlgTrackStatus.Dispose();
      }

      this.dlgTrackStatus = new AslTrackStatus();
      Rectangle presentationBounds = PresentationScreen.GetPresentationWorkingArea();
      this.dlgTrackStatus.Location = new Point(
          (presentationBounds.Width / 2) - (this.dlgTrackStatus.Width / 2),
          (presentationBounds.Height / 2) - (this.dlgTrackStatus.Height / 2));
      this.dlgTrackStatus.ShowDialog();
      return true;
    }

    /// <summary>
    /// Start eye tracker recording. 
    /// </summary>
    public override void Record()
    {
      //// add the aslRawDataReceived method from the Notify event
      this.aslPort.Notify +=
          new _IASLSerialOutPort2Events_NotifyEventHandler(this.aslRawDataReceived);

      this.LastTimeStamp = 0;
      try
      {
        // Create and/or start the timer
        if (this.stopwatch != null)
        {
          this.stopwatch.Reset();
          this.stopwatch.Start();
        }
        else
        {
          this.stopwatch = new Stopwatch();
          this.stopwatch.Start();
        }

        // Initiate callbacks from COM Server (used in streaming mode)
        this.aslPort.StartContinuousMode();

        // If not exist create the Gaze data structure with fields
        // that match the database columns
        if (this.newGazeData.Equals(null))
        {
          this.newGazeData = new GazeData();
        }
      }
      catch (Exception ex)
      {
        if (!this.GetLastError("start the recording failed "))
        {
          ExceptionMethods.ProcessErrorMessage("start the recording failed "
              + ex.Message);
        }

        // remove the aslRawDataReceived method from the Notify event
        this.aslPort.Notify -=
          new _IASLSerialOutPort2Events_NotifyEventHandler(this.aslRawDataReceived);
        if (this.stopwatch != null)
        {
          this.stopwatch.Reset();
        }

        this.CleanUp();
      }
    } // end of Record()

    /// <summary>
    /// Stops tracking.
    /// </summary>
    public override void Stop()
    {
      if (this.stopwatch != null)
      {
        this.stopwatch.Stop();
      }

      try
      {
        // Stop callbacks
        this.aslPort.StopContinuousMode();

        // remove the aslRawDataReceived method from the Notify event
        this.aslPort.Notify -= new
            _IASLSerialOutPort2Events_NotifyEventHandler(this.aslRawDataReceived);
      }
      catch (Exception ex)
      {
        if (!this.GetLastError("stop the recording failed "))
        {
          ExceptionMethods.ProcessErrorMessage("stop the recording failed " + ex.Message);
        }

        this.CleanUp();
      }
    }

    /// <summary>
    /// Raises <see cref="aslSettingsDialog"/> to change the settings
    /// for this interface.
    /// </summary>
    public override void ChangeSettings()
    {
      aslSettingsDialog dlg = new aslSettingsDialog(this, this.aslPort);
      dlg.AslSettings = this.Settings;
      dlg.ShowDialog();
    }

    /// <summary>
    /// Overridden. Dispose the <see cref="Tracker"/> if applicable
    /// by a call to <see cref="ITracker.CleanUp()"/> and 
    /// removing the connected event handlers.
    /// </summary>
    public override void Dispose()
    {
      try
      {
        // Close COM port
        this.aslPort.Disconnect();
      }
      catch (Exception ex)
      {
        if (!this.GetLastError("Disconnection port COM failed "))
        {
          ExceptionMethods.ProcessErrorMessage("Disconnection port COM failed " + ex.Message);
        }

        this.CleanUp();
      }

      base.Dispose();
    }

    /// <summary>
    /// Clean up objects. 
    /// </summary>
    //// Called after this.Dispose
    public override void CleanUp()
    {
      base.CleanUp();
    }

    /// <summary>
    ///  Event handler call when pressing the connect button 
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    protected override void BtnConnectClick(object sender, EventArgs e)
    {
      // Cancel presentation and recording and
      // disconnect if connect button is
      // clicked again.
      if (this.ConnectButton.BackColor == Color.Green)
      {
        if (this.RecordModule.Presenter != null)
        {
          this.RecordModule.Presenter.EndPresentation(true);
        }

        this.CloseComPort();
        this.RecordButton.BackColor = Color.Transparent;
        this.ConnectButton.BackColor = Color.Transparent;
        this.ConnectButton.Enabled = true;
        this.SubjectButton.Enabled = false;

        if (this.CalibrateButton != null)
        {
          this.CalibrateButton.Enabled = false;
        }

        this.RecordButton.Enabled = false;
        return;
      }

      Cursor.Current = Cursors.WaitCursor;
      if (this.Connect())
      {
        this.ConnectButton.BackColor = Color.Green;
        this.SubjectButton.Enabled = true;
      }

      Cursor.Current = Cursors.Default;
    }

    /// <summary>
    /// Sets up calibration procedure and wires the events. Reads settings from file.
    /// </summary>
    protected override void Initialize()
    {
#if ASL
      // Create Port COM Object
      this.aslPort = new ASLSerialOutPort3Class();

      // ???
      this.stopwatch2 = new Stopwatch();

      // Load Asl tracker settings.
      if (File.Exists(this.UserSettingsFile))
      {
        this.settings = UserSettings.Load(this.UserSettingsFile);
      }
      else
      {
        this.settings = new UserSettings();
        this.settings.Store(this.UserSettingsFile);
      }
#endif
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
      this.Calibrate(false);
      this.RecordButton.Enabled = true;
    }

    /// <summary>
    /// Overridden.
    /// Check visibility of the track status window before starting to record.
    /// </summary>
    protected override void PrepareRecording()
    {
      // Hide Trackstatus
      if (this.dlgTrackStatus != null && this.dlgTrackStatus.Visible)
      {
        this.dlgTrackStatus.Close();
      }
    }

    /// <summary>
    /// Event handler call when pressing record button
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    protected override void BtnRecordClick(object sender, EventArgs e)
    {
      this.aslPort.GetScaledData(
              out this.items,
              out this.itemCount,
              out this.available);

      if (this.available)
      {
        base.BtnRecordClick(sender, e);
      }
      else
      {
        MessageBox.Show(
          "Cannot read data on serial port.",
          "Error",
          MessageBoxButtons.OK,
          MessageBoxIcon.Error);
      }
    }
    #endregion //OVERRIDES

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler                                                              //
    ///////////////////////////////////////////////////////////////////////////////
    #region EVENTHANDLER
    #endregion //EVENTHANDLER

    ///////////////////////////////////////////////////////////////////////////////
    // Methods and Eventhandling for Background tasks                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region THREAD
    #endregion //THREAD

    ///////////////////////////////////////////////////////////////////////////////
    // Methods for doing main class job                                          //
    ///////////////////////////////////////////////////////////////////////////////
    #region PRIVATEMETHODS

    /// <summary>
    /// Raw Data Event Handler. Throws a GazeDataChanged-Event.
    /// </summary>
    /// <remarks>La structure des données GazeData est déclarée dans le fichier Recording\GazeData.cs
    /// Le type GazeDataChangedEventArgs, dérivant de EventArgs, encapsulant ces
    ///      informations est la classe Common\CustomEventArgs\GazeDataChangedEventArgs.cs
    /// La déclaration du delegate d'évènement GazeDataChangedEventHandler est faite dans le
    /// fichier Common\CustomEventArgs\GazeDataChangedEventArgs.cs
    /// La déclaration de l'évènement GazeDataChanged,
    /// la création de la méthode protégée, OnGazeDataChanged,
    /// destinée à "publier" l'évènement
    /// et l'abonnement à l'évènement
    /// sont faits dans la classe abstraite Recording\TrackerBase\Tracker.cs
    /// Le gestionnaire de l'évènement, la méthode associée à l'évènement,
    /// ITracker_GazeDataChanged est créée dans la classe RecordModule.cs
    /// L'évènement est signalé à la fin de cette méthode</remarks>
    private void aslRawDataReceived()
    {
      // object [] items; Erreur impossible de convertir de 'out object[]' en 'out System.Array'
      // [MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_VARIANT)] System.Array items;
      float diam = 0;
      try
      {
        this.aslPort.GetScaledData(
            out this.items,
            out this.itemCount,
            out this.available);

        if (Convert.ToByte(this.items.GetValue(0)) == 0) // if Status = normal
        {
          // All items value are never null
          // newGazeData.PupilDiaX = Convert.ToSingle(items.GetValue(1));
          diam = Convert.ToSingle(this.items.GetValue(1));

          // Save current timestamp
          this.setCurrentTime();

          // Get gazeTimestamp in milliseconds.
          this.newGazeData.Time = this.LastTimeStamp;

          // Set pupil diameter in mm
          this.newGazeData.PupilDiaX = diam * this.PupilScaleFactor;
          this.newGazeData.PupilDiaY = this.newGazeData.PupilDiaX;

          // Calculate values between 0..1
          this.newGazeData.GazePosX = Convert.ToSingle(this.items.GetValue(3));
          this.newGazeData.GazePosY = Convert.ToSingle(this.items.GetValue(4));
          this.newGazeData.GazePosX = this.newGazeData.GazePosX / this.MaxHorizontal;
          this.newGazeData.GazePosY = this.newGazeData.GazePosY / this.MaxVertical;

          // raise the event
          this.OnGazeDataChanged(new GazeDataChangedEventArgs(this.newGazeData));
        }
        else
        {
          InformationDialog.Show(
              "Error occured",
              "ASL GetScaledData failed with the following error number : " + this.items.GetValue(0),
              false,
              MessageBoxIcon.Error);
        }
      }
      catch (Exception ex)
      {
        if (!this.GetLastError("ASL GetDataRecord failed "))
        {
          ExceptionMethods.ProcessErrorMessage("ASL GetDataRecord failed " + ex.Message);
        }

        this.CleanUp();
      }
    } // end of aslRawDataReceived()

    /// <summary>
    /// Method to update the lastTimeStamp value with the time in milliseconds.
    /// </summary>
    private void setCurrentTime()
    {
      long lastTime = this.LastTimeStamp;

      // set lock for lastTimeStamp value
      lock (this)
      {
        this.stopwatch.Stop();
        this.LastTimeStamp = lastTime + this.stopwatch.ElapsedMilliseconds;
        this.stopwatch.Reset(); // Reset Stopwatch
        this.stopwatch.Start(); // Start stopwatch
      }
    }

    /// <summary>
    /// Return the description of the ASL last error.
    /// </summary>
    /// <param name="msg">error description</param>
    /// <returns><strong>True</strong> if there is a error message to show, otherwise
    /// <strong>false</strong>.</returns>
    private bool GetLastError(string msg)
    {
      bool somethingShown = true;
      string errorDesc = string.Empty;
      try
      {
        // Would be called after any other function returns an error.
        // Note : this function clears the error description
        this.aslPort.GetLastError(out errorDesc);
        if (errorDesc.Length != 0)
        {
          ExceptionMethods.ProcessErrorMessage(msg + Environment.NewLine + errorDesc);
        }
        else
        {
          somethingShown = false;
        }

        return somethingShown;
      }
      catch (Exception ex)
      {
        InformationDialog.Show(
            "GetLastError failed",
            "ASL GetLastError failed with the following message : " + Environment.NewLine + ex.Message,
            false,
            MessageBoxIcon.Error);

        return somethingShown;
      }
    }

    #endregion //PRIVATEMETHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER

    /// <summary>
    /// This method creates the default standardStreaming configuration file.
    /// </summary>
    private void CreateDefaultConfigFile()
    {
      var defaultFile = Properties.Settings.Default.EyeTrackerSettingsPath + "ASLStandardStreaming.cfg";
      if (File.Exists(defaultFile))
      {
        return;
      }

      using (var sw = new StreamWriter(defaultFile))
      {
        // Add some text to the file.
        sw.WriteLine("[System Settings]");
        sw.WriteLine("using_6000_serial_out_format=1");
        sw.WriteLine("serial_out_streaming=1");
        sw.WriteLine("eye_camera_update_rate=60");
        sw.WriteLine("serial_out_baud_rate=57600");
        sw.WriteLine("serial_out_std_sel_1=7201");
        sw.WriteLine("serial_out_std_sel_2=0");
        sw.WriteLine("serial_out_ehi_sel_1=14680097");
        sw.WriteLine("serial_out_ehi_sel_2=0");
      }
    }

    #endregion //HELPER
  } // end of public class AslTracker
} // end of namespace Ogama.Modules.Recording.ASL
