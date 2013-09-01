// <copyright file="MouseOnlyTracker.cs" company="FU Berlin">
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

namespace Ogama.Modules.Recording.MouseOnlyInterface
{
  using System;
  using System.Diagnostics;
  using System.IO;
  using System.Windows.Forms;
  using System.Xml.Serialization;

  using Ogama.ExceptionHandling;
  using Ogama.Modules.Common;
  using Ogama.Modules.Common.CustomEventArgs;
  using Ogama.Modules.Common.Tools;
  using Ogama.Modules.Recording.TrackerBase;

  using VectorGraphics.Controls;
  using VectorGraphics.Controls.Timer;

  /// <summary>
  /// This class implements the ITracker interface to represent 
  /// an mouse tracker.
  /// </summary>
  public class MouseOnlyTracker : Tracker
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
    /// This saves the tick counter to return the correct time.
    /// </summary>
    private long counter;

    /// <summary>
    /// Saves the <see cref="MouseOnlySetting"/> settings.
    /// </summary>
    private MouseOnlySetting mouseOnlySettings;

    /// <summary>
    /// The timer that gives the sampling rate.
    /// </summary>
    private Timer trackingTimer;

    /// <summary>
    /// A precise timer for getting local time stamps.
    /// </summary>
    private Stopwatch stopWatch;

    /// <summary>
    /// A <see cref="MultimediaTimer"/> which gives very much more precise
    /// timing ticks than the built in windows forms timer.
    /// </summary>
    private MultimediaTimer multimediaTimer;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the MouseOnlyTracker class.
    /// </summary>
    /// <param name="owningRecordModule">The <see cref="RecordModule"/>
    /// form wich host the recorder.</param>
    /// <param name="trackerConnectButton">The <see cref="Button"/>
    /// named "Connect" at the tab page of the MouseOnly device.</param>
    /// <param name="trackerSubjectButton">The <see cref="Button"/>
    /// named "Subject" at the tab page of the MouseOnly device.</param>
    /// <param name="trackerCalibrateButton">The <see cref="Button"/>
    /// named "Calibrate" at the tab page of the MouseOnly device.</param>
    /// <param name="trackerRecordButton">The <see cref="Button"/>
    /// named "Record" at the tab page of the MouseOnly device.</param>
    /// <param name="trackerSubjectNameTextBox">The <see cref="TextBox"/>
    /// which should contain the subject name at the tab page of the MouseOnly device.</param>
    public MouseOnlyTracker(
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
      Properties.Settings.Default.EyeTrackerSettingsPath + "MouseOnlySetting.xml")
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
    /// Gets the connection status of the Mouse Only tracker
    /// which is always true for this tracker.
    /// </summary>
    public override bool IsConnected
    {
      get { return true; }
    }

    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Public methods                                                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region PUBLICMETHODS

    /// <summary>
    /// Method to return the current timing of the tracking system.
    /// </summary>
    /// <returns>A <see cref="Int64"/> with the current time in milliseconds
    /// if the stopwatch is running, otherwise 0.</returns>
    public long GetCurrentTime()
    {
      return this.multimediaTimer.Period * this.counter;
    }

    #endregion //PUBLICMETHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Inherited methods                                                         //
    ///////////////////////////////////////////////////////////////////////////////
    #region OVERRIDES

    /// <summary>
    /// Connects to the mouse only system.
    /// </summary>
    /// <returns>Always <strong>True</strong>.</returns>
    public override bool Connect()
    {
      // Nothing needed
      return true;
    }

    /// <summary>
    /// Starts calibration.
    /// </summary>
    /// <param name="isRecalibrating">whether to use recalibration or not.</param>
    /// <returns>Always <strong>True</strong>.</returns>
    public override bool Calibrate(bool isRecalibrating)
    {
      // Nothing needed
      return true;
    }

    /// <summary>
    /// Clean up objects.
    /// </summary>
    public override void CleanUp()
    {
      this.Stop();

      this.trackingTimer.Dispose();
      this.multimediaTimer.Dispose();

      base.CleanUp();
    }

    /// <summary>
    /// Start tracking.
    /// </summary>
    public override void Record()
    {
      try
      {
        this.counter = 0;
        this.stopWatch.Start();
        this.multimediaTimer.Start();
        this.trackingTimer.Start();
      }
      catch (Exception ex)
      {
        ExceptionMethods.HandleException(ex);

        this.stopWatch.Stop();
        this.trackingTimer.Stop();
        this.multimediaTimer.Stop();
      }
    }

    /// <summary>
    /// Stops tracking.
    /// </summary>
    public override void Stop()
    {
      if (this.trackingTimer != null)
      {
        this.counter = 0;
        this.trackingTimer.Stop();
        this.stopWatch.Stop();
        this.multimediaTimer.Stop();
      }
    }

    /// <summary>
    /// Show a <see cref="MouseOnlySettingsDlg"/> to change the settings
    /// for this interface.
    /// </summary>
    public override void ChangeSettings()
    {
      var dlg = new MouseOnlySettingsDlg { MouseOnlySettings = this.mouseOnlySettings };
      if (dlg.ShowDialog() == DialogResult.OK)
      {
        this.mouseOnlySettings = dlg.MouseOnlySettings;
        this.UpdateSettings();
        this.SerializeSettings(this.mouseOnlySettings, this.SettingsFile);
      }
    }

    /// <summary>
    /// Sets up calibration procedure and the tracking client
    /// and wires the events. Then reads settings from file.
    /// </summary>
    protected override sealed void Initialize()
    {
      this.counter = 0;
      this.trackingTimer = new Timer();
      this.stopWatch = new Stopwatch();
      this.multimediaTimer = new MultimediaTimer();
      this.multimediaTimer.Tick += this.TrackingTimerTick;

      if (File.Exists(this.SettingsFile))
      {
        this.mouseOnlySettings = this.DeserializeSettings(this.SettingsFile);
      }
      else
      {
        this.mouseOnlySettings = new MouseOnlySetting();
        this.SerializeSettings(this.mouseOnlySettings, this.SettingsFile);
      }

      this.UpdateSettings();
    }

    /// <summary>
    /// Overridden <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="Tracker.SubjectButton"/>.
    /// Calls the base class and enables the record button,
    /// because this tracker does not need a calibration
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/>.</param>
    protected override void BtnSubjectNameClick(object sender, EventArgs e)
    {
      base.BtnSubjectNameClick(sender, e);

      // This tracker does not need a calibration
      // so activate immediately the record button
      // if the subject name is OK.
      if (!Queries.CheckDatabaseForExistingSubject(this.SubjectButton.Text))
      {
        this.RecordButton.Enabled = true;
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
    #endregion //WINDOWSEVENTHANDLER

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler for Custom Defined Events                                    //
    ///////////////////////////////////////////////////////////////////////////////
    #region CUSTOMEVENTHANDLER

    /// <summary>
    /// <see cref="Timer.Tick"/> event handler for
    /// the <see cref="trackingTimer"/> <see cref="Timer"/>
    /// This event fires whenever the timer intervall is elapsed.
    /// It sends an empty gaze structure with the current timing in
    /// a OGAMA readable format and fires the <see cref="Tracker.OnGazeDataChanged"/>
    /// event to the recorder.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/>.</param>
    private void TrackingTimerTick(object sender, EventArgs e)
    {
      var newGazeData = new GazeData
        {
          Time = this.GetCurrentTime(),
          GazePosX = null,
          GazePosY = null,
          PupilDiaX = null,
          PupilDiaY = null
        };

      this.OnGazeDataChanged(new GazeDataChangedEventArgs(newGazeData));
      this.counter++;
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
    /// Update the settings.
    /// (Sets sampling rate)
    /// </summary>
    private void UpdateSettings()
    {
      if (this.trackingTimer != null)
      {
        this.trackingTimer.Stop();
        this.trackingTimer.Interval = (int)(1000f / this.mouseOnlySettings.SampleRate);
        this.multimediaTimer.Stop();
        this.multimediaTimer.Period = (int)(1000f / this.mouseOnlySettings.SampleRate);
        this.stopWatch.Reset();
      }
    }

    /// <summary>
    /// Deserializes the <see cref="MouseOnlySetting"/> from the given xml file.
    /// </summary>
    /// <param name="filePath">Full file path to the xml settings file.</param>
    /// <returns>A <see cref="MouseOnlySetting"/> object.</returns>
    private MouseOnlySetting DeserializeSettings(string filePath)
    {
      var settings = new MouseOnlySetting();

      // Create an instance of the XmlSerializer class;
      // specify the type of object to be deserialized 
      var serializer = new XmlSerializer(typeof(MouseOnlySetting));

      // If the XML Document has been altered with unknown 
      // nodes or attributes, handle them with the 
      // UnknownNode and UnknownAttribute events.
      serializer.UnknownNode += this.SerializerUnknownNode;
      serializer.UnknownAttribute += this.SerializerUnknownAttribute;

      try
      {
        // A FileStream is needed to read the XML Document.
        var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);

        /* Use the Deserialize method to restore the object's state with
        data from the XML Document. */
        settings = (MouseOnlySetting)serializer.Deserialize(fs);
        fs.Close();
      }
      catch (Exception ex)
      {
        InformationDialog.Show(
          "Error occured",
          "Deserialization of MouseOnlySettings failed with the following message: " + Environment.NewLine + ex.Message,
          false,
          MessageBoxIcon.Error);
      }

      return settings;
    }

    /// <summary>
    /// Serializes the <see cref="MouseOnlySetting"/> into the given file in a xml structure.
    /// </summary>
    /// <param name="settings">The <see cref="MouseOnlySetting"/> object to serialize.</param>
    /// <param name="filePath">Full file path to the xml settings file.</param>
    private void SerializeSettings(MouseOnlySetting settings, string filePath)
    {
      // Create an instance of the XmlSerializer class;
      // specify the type of object to serialize 
      var serializer = new XmlSerializer(typeof(MouseOnlySetting));

      // Serialize the TobiiSetting, and close the TextWriter.
      try
      {
        // Create an instance of StreamWriter to write text to a file.
        // The using statement also closes the StreamWriter.
        using (var writer = new StreamWriter(filePath))
        {
          serializer.Serialize(writer, settings);
        }
      }
      catch (Exception ex)
      {
        InformationDialog.Show(
          "Error occured",
          "Serialization of MouseOnlySettings failed with the following message: " + Environment.NewLine + ex.Message,
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
