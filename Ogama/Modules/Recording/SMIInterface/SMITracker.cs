// <copyright file="SMITracker.cs" company="FU Berlin">
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

namespace Ogama.Modules.Recording.SMIInterface
{
  using System;
  using System.IO;
  using System.Windows.Forms;
  using System.Xml.Serialization;

  using Ogama.ExceptionHandling;
  using Ogama.Modules.Common;
  using Ogama.Modules.Common.CustomEventArgs;
  using Ogama.Modules.Recording.Dialogs;
  using Ogama.Modules.Recording.TrackerBase;

  /// <summary>
  /// This class implements the <see cref="ITracker"/> interface to represent 
  /// an OGAMA known eyetracker.
  /// It encapsulates a SMI http://www.smivision.com/ eyetracker (iViewX series).
  /// It is tested with the SMI HED series.
  /// </summary>
  public class SMITracker : Tracker
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
    /// Saves the SMI settings.
    /// </summary>
    private SMISetting smiSettings;

    /// <summary>
    /// Handles the calls to the SMI iViewX application layer. 
    /// </summary>
    private SMIClient smiClient;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the SMITracker class.
    /// </summary>
    /// <param name="owningRecordModule">The <see cref="RecordModule"/>
    /// form wich host the recorder.</param>
    /// <param name="trackerConnectButton">The <see cref="Button"/>
    /// named "Connect" at the tab page of the SMI device.</param>
    /// <param name="trackerSubjectButton">The <see cref="Button"/>
    /// named "Subject" at the tab page of the SMI device.</param>
    /// <param name="trackerCalibrateButton">The <see cref="Button"/>
    /// named "Calibrate" at the tab page of the SMI device.</param>
    /// <param name="trackerRecordButton">The <see cref="Button"/>
    /// named "Record" at the tab page of the SMI device.</param>
    /// <param name="trackerSubjectNameTextBox">The <see cref="TextBox"/>
    /// which should contain the subject name at the tab page of the SMI device.</param>
    public SMITracker(
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
      Properties.Settings.Default.EyeTrackerSettingsPath + "SMISetting.xml")
    {
      // Call the initialize methods of derived classes
      this.Initialize();
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Enumerations                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region ENUMS

    /// <summary>
    /// Event. Raised, when new calibration is finished.
    /// </summary>
    public event EventHandler CalibrationFinished;

    #endregion ENUMS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets the current SMI settings.
    /// </summary>
    /// <value>A <see cref="SMISetting"/> with the current tracker settings.</value>
    public SMISetting Settings
    {
      get { return this.smiSettings; }
    }

    /// <summary>
    /// Gets the connection status of the SMI tracker
    /// </summary>
    public override bool IsConnected
    {
      get { return this.smiClient.IsConnected; }
    }

    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Public methods                                                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region PUBLICMETHODS

    /// <summary>
    /// Method to return the current timing of the tracking system.
    /// </summary>
    /// <returns>A <see cref="Int64"/> with the time in milliseconds.</returns>
    public long GetCurrentTime()
    {
      return this.smiClient.GetTimeStamp();
    }

    #endregion //PUBLICMETHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Inherited methods                                                         //
    ///////////////////////////////////////////////////////////////////////////////
    #region OVERRIDES

    /// <summary>
    /// Connects to the SMI iView system.
    /// </summary>
    /// <returns><strong>True</strong> if connection succeded, otherwise
    /// <strong>false</strong>.</returns>
    public override bool Connect()
    {
      try
      {
        // Connect to the SMI server if necessary
        if (!this.smiClient.IsConnected)
        {
          this.smiClient.Connect();
        }

        // Start the SMI client
        if (!this.smiClient.IsTracking)
        {
          this.smiClient.StartTracking();
        }
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
    /// Starts calibration.
    /// </summary>
    /// <param name="isRecalibrating">whether to use recalibration or not.</param>
    /// <returns><strong>True</strong> if calibration succeded, otherwise
    /// <strong>false</strong>.</returns>
    public override bool Calibrate(bool isRecalibrating)
    {
      try
      {
        // Connect if necessary
        if (!this.smiClient.IsConnected)
        {
          this.smiClient.Connect();
        }

        this.smiClient.Calibrate();
      }
      catch (Exception ex)
      {
        string message = "SMI iViewX calibration failed with the following message: " +
          Environment.NewLine + ex;
        ExceptionMethods.ProcessErrorMessage(message);
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

      try
      {
        if (this.smiClient.IsConnected)
        {
          this.smiClient.Disconnect();
        }
      }
      catch (Exception ex)
      {
        string message = "SMI CleanUp failed with the following message: " +
         Environment.NewLine + ex;
        ExceptionMethods.ProcessErrorMessage(message);
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
        // Connect to the SMI server if necessary
        if (!this.smiClient.IsConnected)
        {
          this.smiClient.Connect();
        }

        if (!this.smiClient.IsTracking)
        {
          // Start tracking gaze data
          this.smiClient.StartTracking();
        }

        this.smiClient.StartStreaming();
      }
      catch (Exception ex)
      {
        string message = "SMI iViewX record failed with the following message: " +
         Environment.NewLine + ex;
        ExceptionMethods.ProcessErrorMessage(message);
        this.CleanUp();
      }
    }

    /// <summary>
    /// Stops tracking.
    /// </summary>
    public override void Stop()
    {
      try
      {
        if (this.smiClient.IsConnected)
        {
          if (this.smiClient.IsTracking)
          {
            this.smiClient.StopTracking();
          }
        }
      }
      catch (Exception ex)
      {
        string message = "SMI Stop failed with the following message: " +
         Environment.NewLine + ex;
        ExceptionMethods.ProcessErrorMessage(message);
      }
    }

    /// <summary>
    /// Raises <see cref="SMISettingsDlg"/> to change the settings
    /// for this interface.
    /// </summary>
    public override void ChangeSettings()
    {
      var dlg = new SMISettingsDlg { SMISettings = this.smiSettings };
      if (dlg.ShowDialog() == DialogResult.OK)
      {
        this.smiSettings = dlg.SMISettings;
        this.SerializeSettings(this.smiSettings, this.SettingsFile);
      }
    }

    /// <summary>
    /// Overridden. Dispose the <see cref="SMITracker"/> if applicable
    /// by a call to <see cref="ITracker.CleanUp()"/> and 
    /// removing the connected event handlers.
    /// </summary>
    public override void Dispose()
    {
      base.Dispose();
      this.CalibrationFinished -= this.SMIInterfaceCalibrationFinished;
    }

    /// <summary>
    /// Sets up calibration procedure and the tracking client
    /// and wires the events. Reads settings from file.
    /// </summary>
    protected override sealed void Initialize()
    {
      this.CalibrationFinished += this.SMIInterfaceCalibrationFinished;

      // Set up the SMI client object and it's events
      this.smiClient = new SMIClient();
      this.smiClient.GazeDataAvailable += this.SMIClientGazeDataAvailable;
      this.smiClient.CalibrationFinished += this.SMIClientCalibrationFinished;

      // Load SMI tracker settings.
      if (File.Exists(this.SettingsFile))
      {
        this.smiSettings = this.DeserializeSettings(this.SettingsFile);
      }
      else
      {
        this.smiSettings = new SMISetting();
        this.SerializeSettings(this.smiSettings, this.SettingsFile);
      }

      // Set tracker settings.
      this.smiClient.Settings = this.smiSettings;
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
    /// This method is called whenever a SMI interface has finished its calibration.
    /// It to give feedback on the calibration,
    /// but currently not fully implemented.
    /// </summary>
    /// <param name="sender">Sender of the event</param>
    /// <param name="e">Empty <see cref="EventArgs"/></param>
    private void SMIInterfaceCalibrationFinished(object sender, EventArgs e)
    {
      this.RecordButton.Enabled = true;
    }

    /// <summary>
    /// Fires the <see cref="Tracker.OnGazeDataChanged"/>
    /// event to the recorder, if client has new gaze data.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A <see cref="GazeDataChangedEventArgs"/> with the event data.</param>
    private void SMIClientGazeDataAvailable(object sender, GazeDataChangedEventArgs e)
    {
      this.OnGazeDataChanged(new GazeDataChangedEventArgs(e.Gazedata));
    }

    /// <summary>
    /// Fires the <see cref="OnCalibrationFinished"/>
    /// event to the recorder, if client has finished calibration.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void SMIClientCalibrationFinished(object sender, EventArgs e)
    {
      this.OnCalibrationFinished(e);
    }

    /// <summary>
    /// Raises the CalibrationFinished event.
    /// </summary>
    /// <param name="e">An <see cref="EventArgs"/> event arguments</param>
    private void OnCalibrationFinished(EventArgs e)
    {
      if (this.CalibrationFinished != null)
      {
        this.CalibrationFinished(this, e);
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
    #region PRIVATEMETHODS

    /// <summary>
    /// Deserializes the <see cref="SMISetting"/> from the given xml file.
    /// </summary>
    /// <param name="filePath">Full file path to the xml settings file.</param>
    /// <returns>A <see cref="SMISetting"/> object.</returns>
    private SMISetting DeserializeSettings(string filePath)
    {
      var settings = new SMISetting();

      // Create an instance of the XmlSerializer class;
      // specify the type of object to be deserialized 
      var serializer = new XmlSerializer(typeof(SMISetting));

      // * If the XML Document has been altered with unknown 
      // nodes or attributes, handle them with the 
      // UnknownNode and UnknownAttribute events.*/
      serializer.UnknownNode += this.SerializerUnknownNode;
      serializer.UnknownAttribute += this.SerializerUnknownAttribute;

      try
      {
        // A FileStream is needed to read the XML Document.
        var fs = new FileStream(filePath, FileMode.Open);

        // Use the Deserialize method to restore the object's state with
        // data from the XML Document. 
        settings = (SMISetting)serializer.Deserialize(fs);
        fs.Close();
      }
      catch (Exception ex)
      {
        string message = "Deserialization of SMISettings failed with the following message: " +
          Environment.NewLine + ex.Message;
        ExceptionMethods.ProcessErrorMessage(message);
      }

      return settings;
    }

    /// <summary>
    /// Serializes the <see cref="SMISetting"/> into the given file in a xml structure.
    /// </summary>
    /// <param name="settings">The <see cref="SMISetting"/> object to serialize.</param>
    /// <param name="filePath">Full file path to the xml settings file.</param>
    private void SerializeSettings(SMISetting settings, string filePath)
    {
      // Create an instance of the XmlSerializer class;
      // specify the type of object to serialize 
      var serializer = new XmlSerializer(typeof(SMISetting));

      // Serialize the SMISetting, and close the TextWriter.
      try
      {
        TextWriter writer = new StreamWriter(filePath, false);
        serializer.Serialize(writer, settings);
        writer.Close();
      }
      catch (Exception ex)
      {
        string message = "Serialization of SMISettings failed with the following message: " +
          Environment.NewLine + ex.Message;
        ExceptionMethods.ProcessErrorMessage(message);
      }
    }

    #endregion //PRIVATEMETHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER
    #endregion //HELPER
  }
}