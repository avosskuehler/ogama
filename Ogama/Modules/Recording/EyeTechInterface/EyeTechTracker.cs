// <copyright file="EyeTechTracker.cs" company="Validators">
// ******************************************************
// OGAMA - open gaze and mouse analyzer 
// Copyright (C) 2010 Adrian Voßkühler  
// ------------------------------------------------------------------------
// This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
// This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
// You should have received a copy of the GNU General Public License along with this program; if not, write to the Free Software Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
// **************************************************************
// </copyright>
// <author>Jochem</author>
// <author>M.Schraal</author>
// <email>marinus@validators.nl</email>

namespace Ogama.Modules.Recording.EyeTechInterface
{
  using System;
  using System.Diagnostics;
  using System.IO;
  using System.Threading;
  using System.Windows.Forms;
  using System.Xml.Serialization;

  using Microsoft.Win32;

  using Ogama.ExceptionHandling;
  using Ogama.Modules.Common.CustomEventArgs;
  using Ogama.Modules.Recording.Dialogs;
  using Ogama.Modules.Recording.TrackerBase;

  /// <summary>
  /// This class implements the <see cref="Tracker"/> class
  /// to represent an OGAMA known eyetracker.
  /// It encapsulates an EyeTech http://www.eyetechds.com/ eyetracker 
  /// and is written with the SDK v5.2 from EyeTech Digital Systems.
  /// It is tested with the EyeTech TM3, but supposed to work with an TM2
  /// as well.
  /// </summary>
  public class EyeTechTracker : Tracker
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
    /// Saves the eyetech settings.
    /// </summary>
    private EyeTechSetting eyetechSettings;

    /// <summary>
    /// True if Interface throws GazeDataChanged-Events, otherwise false
    /// </summary>
    private bool isRecording;

    /// <summary>
    /// Thread used for collecting raw data from the eyetracker.
    /// </summary>
    private Thread dataCollector;

    /// <summary>
    /// Container for raw gaze- and image data
    /// </summary>
    private ImageData imageData;

    /// <summary>
    /// X-Screenresolution in Pixel. Valid after calibration.
    /// </summary>
    private int resolutionX;

    /// <summary>
    /// Y-Screenresolution in Pixel. Valid after calibration.
    /// </summary>
    private int resolutionY;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the EyeTechTracker class.
    /// Initializes COM objects and other.
    /// </summary>
    /// <param name="owningRecordModule">The <see cref="RecordModule"/>
    /// form which host the recorder.</param>
    /// <param name="trackerConnectButton">The <see cref="Button"/>
    /// named "Connect" at the tab page of the EyeTech device.</param>
    /// <param name="trackerSubjectButton">The <see cref="Button"/>
    /// named "Subject" at the tab page of the EyeTech device.</param>
    /// <param name="trackerCalibrateButton">The <see cref="Button"/>
    /// named "Calibrate" at the tab page of the EyeTech device.</param>
    /// <param name="trackerRecordButton">The <see cref="Button"/>
    /// named "Record" at the tab page of the EyeTech device.</param>
    /// <param name="trackerSubjectNameTextBox">The <see cref="TextBox"/>
    /// which should contain the subject name at the tab page of the EyeTech device.</param>
    public EyeTechTracker(
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
      Properties.Settings.Default.EyeTrackerSettingsPath + "EyeTechSetting.xml")
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
    /// Gets the current EyeTech settings.
    /// </summary>
    /// <value>A <see cref="EyeTechSetting"/> with the current tracker settings.</value>
    public EyeTechSetting Settings
    {
      get { return this.eyetechSettings; }
    }

    /// <summary>
    /// Checks if QuickGlance is running. No need for further connecting, 
    /// as QuickGlance will not start if the device is not physically connected.
    /// </summary>
    public override bool IsConnected
    {
      get { return NativeMethods.GetQGOnFlag(); }
    }

    /// <summary>
    /// Gets or sets a value indicating whether eyetech tracker is recording
    /// that means processing images.
    /// </summary>
    private bool IsRecording
    {
      get { return this.isRecording; }
      set { this.isRecording = value; }
    }

    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Public methods                                                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region PUBLICMETHODS

    /// <summary>
    /// These class method check if the eyetech tracker is available in the system.
    /// </summary>
    /// <param name="errorMessage">Out. A <see cref="String"/> with an error message.</param>
    /// <returns><strong>True</strong>, if eyetech tracker is available in the system, 
    /// otherwise <strong>false</strong></returns>.
    public static bool IsAvailable(out string errorMessage)
    {
      // No options available in the API for checking this
      // FIXME Add method for Quick Glance detection
      var key = Registry.LocalMachine.OpenSubKey(@"Software\EyeTech Digital Systems\Quick Glance", false);
      if (key == null)
      {
        errorMessage =
          "Quick Glance application not found in registry. EyeTech tracker will not work. Please install the "
          + "Quick glance application.";
        return false;
      }

      errorMessage = string.Empty;
      return true;
    }

    #endregion //PUBLICMETHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Inherited methods                                                         //
    ///////////////////////////////////////////////////////////////////////////////
    #region OVERRIDES

    /// <summary>
    /// Connects the track status object to the eyetech system.
    /// </summary>
    /// <returns><strong>True</strong> if connection succeeded, otherwise
    /// <strong>false</strong>.</returns>
    public override bool Connect()
    {
      // Check if Quick Glance is running.
      if (this.IsConnected)
      {
        return true;
      }

      // Try to start quickglance
      if (this.StartQuickGlance())
      {
        return true;
      }

      // Show a message and exit if QuickGlance could not be ran automagically.
      var dlg = new ConnectionFailedDialog
        {
          ErrorMessage = "Quick Glance could not be found. Please start/install and try again"
        };
      dlg.ShowDialog();
      this.CleanUp();

      return false;
    }

    /// <summary>
    /// Starts calibration.
    /// </summary>
    /// <param name="isRecalibrating">whether to use recalibration or not.</param>
    /// <returns><strong>True</strong> if calibration succeeded, otherwise
    /// <strong>false</strong>.</returns>
    public override bool Calibrate(bool isRecalibrating)
    {
      ////// Get the default sample time per calibration point.
      ////int calibrationTime = this.eyetechSettings.CalibrationOptions.Calibration_TargetTime;
      ////int targetHandle = 0;

      ////// Value's to indicate calibration precision. <=10 Good. >10 requires a retry.
      ////double scoreLeft = -1.0;
      ////double scoreRight = -1.0;

      // Update the screenresolution
      this.resolutionX = Document.ActiveDocument.PresentationSize.Width;
      this.resolutionY = Document.ActiveDocument.PresentationSize.Height;

      ////// Error container for callibration.
      ////CalibrationErrorEx calibrationResult = CalibrationErrorEx.CALIBRATIONEX_NOT_INITIALIZED;
      ////CalibrationStyle calibrationStyle = this.eyetechSettings.CalibrationOptions.Calibration_Style;

      // Check if Quick Glance is running.
      if (!this.Connect())
      {
        return false;
      }

      // Start the QuickGlance Calibration
      // IMPORTANT : We use the external QuickGlance calibration
      // instead of our own, because it's better and our own isn't
      // worth fixing up for the v1 API
      NativeMethods.InternalCalibration1();

      // EYETECH TODO return result;
      // IMPORTANT : external calibration has no way of returning success
      // so it's up to the subject to check calibration statuss
      this.RecordButton.Enabled = true;
      return true;
    }

    /// <summary>
    /// Clean up objects.
    /// </summary>
    public override void CleanUp()
    {
      this.Stop();
      base.CleanUp();
    }

    /// <summary>
    /// Start tracking.
    /// </summary>
    public override void Record()
    {
      // Check if Quick Glance is running.   
      if (!this.Connect())
      {
        // If not, try to start it. If this fails warn the user and assume manual starting.
        if (!this.StartQuickGlance())
        {
          var dlg = new ConnectionFailedDialog
            {
              ErrorMessage =
                "Quick Glance is not running. Please connect the tracker, start Quick Glance and try again."
            };

          dlg.ShowDialog();
          this.Stop();

          return;
        }
      }

      if (this.IsRecording)
      {
        return;
      }

      this.IsRecording = true;
      this.dataCollector = new Thread(this.StartDataCollecting);
      this.dataCollector.Start();
    }

    /// <summary>
    /// Stops tracking.
    /// </summary>
    public override void Stop()
    {
      if (this.isRecording)
      {
        this.IsRecording = false;

        // This should allow the collector thread to end gracefully.
        // EYETECHTODO: test if this is actually true.
      }
    }

    /// <summary>
    /// Change the settings
    /// for this interface.
    /// </summary>
    public override void ChangeSettings()
    {
      // Do nothing for now
    }

    /// <summary>
    /// Sets up calibration procedure and the tracking client
    /// and wires the events. Reads settings from file.
    /// </summary>
    protected override sealed void Initialize()
    {
      this.imageData = new ImageData();

      // Load eyetech tracker settings. First tries to get realtime QuickGlance
      // settings. If this fails, fallback to a settingsfile. If that fails, create
      // a default settingsfile. 
      // NOTE: These settings are not always up to date as a user can just change
      // the settings through QuickGlance.
      this.eyetechSettings = new EyeTechSetting();
      if (!this.eyetechSettings.GetQuickGlanceSettings())
      {
        if (File.Exists(this.SettingsFile))
        {
          this.eyetechSettings = this.DeserializeSettings(this.SettingsFile);
        }
        else
        {
          this.SerializeSettings(this.eyetechSettings, this.SettingsFile);
        }
      }

      this.UpdateSettings();
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
    /// This method starts the data collection process running an infinite loop
    /// unless isRecording is false
    /// </summary>
    private void StartDataCollecting()
    {
      var tempImgData = new ImageData();
      while (this.isRecording)
      {
        // Keep looping for valid image data.
        while (!NativeMethods.GetImageData(100, ref tempImgData))
        {
        }

        // Check if we have this sample, if so ignore it
        if (tempImgData.Time != this.imageData.Time)
        {
          this.imageData = tempImgData;
          this.ProcessImageData(this.imageData);
        }
      }
    }

    /// <summary>
    /// Processes an <see cref="ImageData"/> object and reports it to Ogama.
    /// </summary>
    /// <param name="data">The <see cref="ImageData"/> to process.</param>
    private void ProcessImageData(ImageData data)
    {
      var newGazeData = new GazeData();

      if (data.LeftEye.Found && data.RightEye.Found)
      {
        newGazeData.GazePosX = (float)((data.LeftEye.GazePoint.x + data.RightEye.GazePoint.x) / 2.0);
        newGazeData.GazePosY = (float)((data.LeftEye.GazePoint.y + data.RightEye.GazePoint.y) / 2.0);
        newGazeData.PupilDiaX = (float)data.LeftEye.PupilDiameter;
        newGazeData.PupilDiaY = (float)data.RightEye.PupilDiameter;

        newGazeData.GazePosX = newGazeData.GazePosX / (float)this.resolutionX;
        newGazeData.GazePosY = newGazeData.GazePosY / (float)this.resolutionY;
      }
      else if (data.LeftEye.Found)
      {
        newGazeData.GazePosX = (float)data.LeftEye.GazePoint.x;
        newGazeData.GazePosY = (float)data.LeftEye.GazePoint.y;
        newGazeData.PupilDiaX = (float)data.LeftEye.PupilDiameter;
        newGazeData.PupilDiaY = null;

        newGazeData.GazePosX = newGazeData.GazePosX / (float)this.resolutionX;
        newGazeData.GazePosY = newGazeData.GazePosY / (float)this.resolutionY;
      }
      else if (data.RightEye.Found)
      {
        newGazeData.GazePosX = (float)data.RightEye.GazePoint.x;
        newGazeData.GazePosY = (float)data.RightEye.GazePoint.y;
        newGazeData.PupilDiaX = null;
        newGazeData.PupilDiaY = (float)data.RightEye.PupilDiameter;

        newGazeData.GazePosX = newGazeData.GazePosX / (float)this.resolutionX;
        newGazeData.GazePosY = newGazeData.GazePosY / (float)this.resolutionY;
      }
      else
      {
        newGazeData.GazePosX = null;
        newGazeData.GazePosY = null;
        newGazeData.PupilDiaX = null;
        newGazeData.PupilDiaY = null;
      }

      newGazeData.Time = (long)data.Time;

      this.OnGazeDataChanged(new GazeDataChangedEventArgs(newGazeData));
    }

    /// <summary>
    /// Updates the current <see cref="eyetechSettings"/> object to 
    /// QuickGlance and the Tracker.
    /// </summary>
    private void UpdateSettings()
    {
      this.eyetechSettings.SetQuickGlanceSettings();
    }

    /// <summary>
    /// Deserializes the <see cref="EyeTechSetting"/> from the given xml file.
    /// </summary>
    /// <param name="filePath">Full file path to the xml settings file.</param>
    /// <returns>A <see cref="EyeTechSetting"/> object.</returns>
    private EyeTechSetting DeserializeSettings(string filePath)
    {
      var settings = new EyeTechSetting();

      // Create an instance of the XmlSerializer class;
      // specify the type of object to be deserialized 
      var serializer = new XmlSerializer(typeof(EyeTechSetting));

      // If the XML document has been altered with unknown 
      // nodes or attributes, handle them with the 
      // UnknownNode and UnknownAttribute events.
      serializer.UnknownNode += new XmlNodeEventHandler(this.SerializerUnknownNode);
      serializer.UnknownAttribute += new XmlAttributeEventHandler(this.SerializerUnknownAttribute);

      try
      {
        // A FileStream is needed to read the XML document.
        var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);

        // Use the Deserialize method to restore the object's state with
        // data from the XML document.
        settings = (EyeTechSetting)serializer.Deserialize(fs);
        fs.Close();
      }
      catch (Exception ex)
      {
        InformationDialog.Show(
          "Error occured",
          "Deserialization of EyeTechSetting failed with the following message: " + Environment.NewLine + ex.Message,
          false,
          MessageBoxIcon.Error);
      }

      return settings;
    }

    /// <summary>
    /// Serializes the <see cref="EyeTechSetting"/> into the given file in a xml structure.
    /// </summary>
    /// <param name="settings">The <see cref="EyeTechSetting"/> object to serialize.</param>
    /// <param name="filePath">Full file path to the xml settings file.</param>
    private void SerializeSettings(EyeTechSetting settings, string filePath)
    {
      // Create an instance of the XmlSerializer class;
      // specify the type of object to serialize 
      var serializer = new XmlSerializer(typeof(EyeTechSetting));

      // Serialize the EyeTechSetting, and close the TextWriter.
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
          "Serialization of EyeTechSetting failed with the following message: " + Environment.NewLine + ex.Message,
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
    /// Function used to locate and start QuickGlance, as it is required to run for the API to work.
    /// </summary>
    /// <returns>True if QuickGlance was successfully started.</returns>
    private bool StartQuickGlance()
    {
      var key = Registry.LocalMachine.OpenSubKey(@"Software\EyeTech Digital Systems\Quick Glance", false);
      if (key != null)
      {
        var value = (string)key.GetValue("Path");

        // Start QuickGlance with background param
        var quickGlance = new Process { StartInfo = { FileName = value, Arguments = "background" } };
        ////quickGlance.StartInfo.UseShellExecute = false;
        ////quickGlance.StartInfo.UserName = Environment.UserName;

        quickGlance.Start();

        // Calculate an endtime for the timeout.
        var deadLine = DateTime.Now.AddSeconds(60);

        // Keep looping till the deadline expires or break
        // when goals are met.
        while (DateTime.Compare(DateTime.Now, deadLine) < 0)
        {
          if (NativeMethods.GetQGOnFlag())
          {
            return true;
          }
        }
      }

      return false;
    }

    #endregion //HELPER
  }
}
