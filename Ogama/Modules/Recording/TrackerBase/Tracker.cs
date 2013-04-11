// <copyright file="Tracker.cs" company="FU Berlin">
// ******************************************************
// OGAMA - open gaze and mouse analyzer 
// Copyright (C) 2012 Adrian Voßkühler  
// ------------------------------------------------------------------------
// This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
// This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
// You should have received a copy of the GNU General Public License along with this program; if not, write to the Free Software Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
// **************************************************************
// </copyright>
// <author>Adrian Voßkühler</author>
// <email>adrian@ogama.net</email>

namespace Ogama.Modules.Recording.TrackerBase
{
  using System;
  using System.Drawing;
  using System.Windows.Forms;
  using System.Xml.Serialization;

  using Ogama.ExceptionHandling;
  using Ogama.Modules.Common;
  using Ogama.Modules.Common.CustomEventArgs;
  using Ogama.Modules.Common.Tools;
  using Ogama.Modules.ImportExport;
  using Ogama.Modules.ImportExport.Common;
  using Ogama.Modules.Recording.Dialogs;

  using VectorGraphics.Tools;

  /// <summary>
  /// This abstract class implements <see cref="ITracker"/> and <see cref="IDisposable"/>
  /// and is the base class for all interfaces to hardware trackers.
  /// It is initialized with buttons of the GUI frontend and performs
  /// default tasks for default buttons and calls to the <see cref="ITracker"/>
  /// interface.
  /// </summary>
  /// <remarks>Every new hardware device that should be used in the recorder
  /// module of OGAMA has to be a child of this class.</remarks>
  public abstract class Tracker : ITracker, IDisposable
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
    /// This <see cref="Button"/> is the first available button on the
    /// tab page for the tracker. It should be named "Connect".
    /// </summary>
    private readonly Button connectButton;

    /// <summary>
    /// This <see cref="Button"/> is the second button on the
    /// tab page for the tracker. It should be named "Subject".
    /// </summary>
    private readonly Button subjectButton;

    /// <summary>
    /// This <see cref="Button"/> is the third button on the
    /// tab page for the tracker. It should be named "Calibrate".
    /// </summary>
    private readonly Button calibrateButton;

    /// <summary>
    /// This <see cref="Button"/> is the fourth button on the
    /// tab page for the tracker. It should be named "Record".
    /// </summary>
    private readonly Button recordButton;

    /// <summary>
    /// This <see cref="TextBox"/> is beneath the subject button
    /// and shows the name of the currently recorded subject.
    /// </summary>
    private readonly TextBox subjectNameTextBox;

    /// <summary>
    /// This is the <see cref="RecordModule"/> control which can
    /// be used to get access to the public methods of the
    /// recording module.
    /// </summary>
    private readonly RecordModule recordModule;

    /// <summary>
    /// Saves the settings file with full path for the custom settings of
    /// the hardware device
    /// </summary>
    private readonly string settingsFile;

    /// <summary>
    /// Saves the <see cref="SubjectsData"/> of the current subject.
    /// </summary>
    private SubjectsData subject;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the Tracker class.
    /// </summary>
    /// <param name="owningRecordModule">The <see cref="RecordModule"/>
    /// form wich host the recorder.</param>
    /// <param name="trackerConnectButton">The <see cref="Button"/>
    /// named "Connect" at the tab page of the tracking device.</param>
    /// <param name="trackerSubjectButton">The <see cref="Button"/>
    /// named "Subject" at the tab page of the tracking device.</param>
    /// <param name="trackerCalibrateButton">The <see cref="Button"/>
    /// named "Calibrate" at the tab page of the tracking device.</param>
    /// <param name="trackerRecordButton">The <see cref="Button"/>
    /// named "Record" at the tab page of the tracking device.</param>
    /// <param name="trackerSubjectNameTextBox">The <see cref="TextBox"/>
    /// which should contain the subject name at the tab page of the tracking device.</param>
    /// <param name="trackerSettingsFile">The file with full path to the settings
    /// xml file of the tracking device.</param>
    protected Tracker(
      RecordModule owningRecordModule,
      Button trackerConnectButton,
      Button trackerSubjectButton,
      Button trackerCalibrateButton,
      Button trackerRecordButton,
      TextBox trackerSubjectNameTextBox,
      string trackerSettingsFile)
    {
      if (trackerSubjectButton == null)
      {
        throw new ArgumentNullException("trackerSubjectButton", "All custom devices should have a subject button on their tab pages.");
      }

      if (trackerRecordButton == null)
      {
        throw new ArgumentNullException("trackerRecordButton", "All custom devices should have a record button on their tab pages.");
      }

      this.recordModule = owningRecordModule;
      this.connectButton = trackerConnectButton;
      this.subjectButton = trackerSubjectButton;
      this.calibrateButton = trackerCalibrateButton;
      this.recordButton = trackerRecordButton;
      this.subjectNameTextBox = trackerSubjectNameTextBox;
      this.settingsFile = trackerSettingsFile;

      // Wires the recording finished event from the record module
      // to wait for resetting the button states after recording
      // stopped
      this.recordModule.RecordingFinished += this.RecordModuleNewRecordingFinished;

      // Wires the GazeDataChanged event of this tracking device
      // to the record modules
      // event handler.
      this.GazeDataChanged += this.recordModule.TrackerGazeDataChanged;

      // Create new empty subject
      this.subject = new SubjectsData();

      // Wire button events.
      this.recordButton.Click += this.BtnRecordClick;

      if (this.calibrateButton != null)
      {
        this.calibrateButton.Click += this.BtnCalibrateClick;
      }

      this.subjectButton.Click += this.BtnSubjectNameClick;

      if (this.connectButton != null)
      {
        this.connectButton.Click += this.BtnConnectClick;
      }
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Enumerations                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region ENUMS

    /// <summary>
    /// Implementation of the ITracker interface.
    /// Event. Raised, when new gaze data is available.
    /// </summary>
    public event GazeDataChangedEventHandler GazeDataChanged;

    #endregion ENUMS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets the <see cref="SubjectsData"/> of the current subject.
    /// </summary>
    public SubjectsData Subject
    {
      get { return this.subject; }
    }

    /// <summary>
    /// Gets the settings file with full path for the custom settings of
    /// the hardware device
    /// </summary>
    public string SettingsFile
    {
      get { return this.settingsFile; }
    }

    /// <summary>
    /// Gets a value indicating whether the connection status of the tracker.
    /// </summary>
    public abstract bool IsConnected { get; }

    /// <summary>
    /// Gets the the <see cref="RecordModule"/> control which can
    /// be used to get access to the public methods of the
    /// recording module.
    /// </summary>
    public RecordModule RecordModule
    {
      get { return this.recordModule; }
    }

    /// <summary>
    /// Gets the <see cref="Button"/> named "Connect"
    /// on the tab page for this tracking device.
    /// </summary>
    protected Button ConnectButton
    {
      get { return this.connectButton; }
    }

    /// <summary>
    /// Gets the <see cref="Button"/> named "Subject"
    /// on the tab page for this tracking device.
    /// </summary>
    protected Button SubjectButton
    {
      get { return this.subjectButton; }
    }

    /// <summary>
    /// Gets the <see cref="Button"/> named "Calibrate"
    /// on the tab page for this tracking device.
    /// </summary>
    protected Button CalibrateButton
    {
      get { return this.calibrateButton; }
    }

    /// <summary>
    /// Gets the <see cref="Button"/> named "Record"
    /// on the tab page for this tracking device.
    /// </summary>
    protected Button RecordButton
    {
      get { return this.recordButton; }
    }

    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Public methods                                                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region PUBLICMETHODS

    /// <summary>
    /// An implementation of this method should do all 
    /// connection routines for the specific hardware, so that the
    /// system is ready for calibration.
    /// </summary>
    /// <returns><strong>True</strong> if succesful connected to tracker,
    /// otherwise <strong>false</strong>.</returns>
    public abstract bool Connect();

    /// <summary>
    /// An implementation of this method should do the calibration
    /// for the specific hardware, so that the
    /// system is ready for recording.
    /// </summary>
    /// <param name="isRecalibrating"><strong>True</strong> if calibration
    /// is in recalibration mode, indicating to renew only a few points,
    /// otherwise <strong>false</strong>.</param>
    /// <returns><strong>True</strong> if succesful calibrated,
    /// otherwise <strong>false</strong>.</returns>
    /// <remarks>Implementors do not have to use the recalibrating 
    /// parameter.</remarks>
    public abstract bool Calibrate(bool isRecalibrating);

    /// <summary>
    /// An implementation of this method should start the recording
    /// for the specific hardware, so that the
    /// system sends <see cref="GazeDataChanged"/> events.
    /// </summary>
    public abstract void Record();

    /// <summary>
    /// An implementation of this method should stop the recording
    /// for the specific hardware.
    /// </summary>
    public abstract void Stop();

    /// <summary>
    /// An implementation of this method should do a clean up
    /// for the specific hardware, so that the
    /// system is ready for shut down.
    /// </summary>
    public virtual void CleanUp()
    {
      if (this.connectButton != null)
      {
        this.connectButton.Enabled = true;
        this.connectButton.BackColor = Color.Transparent;
      }

      this.subjectButton.Enabled = false;

      if (this.calibrateButton != null)
      {
        this.calibrateButton.Enabled = false;
      }

      this.recordButton.Enabled = false;
      this.recordButton.BackColor = Color.Transparent;
    }

    /// <summary>
    /// An implementation of this method should show a hardware 
    /// system specific dialog to change its settings like
    /// sampling rate or connection properties. It should also
    /// provide a xml serialization possibility of the settings,
    /// so that the user can store and backup system settings in
    /// a separate file. These settings should be implemented in
    /// a separate class and are stored in a special place of
    /// Ogamas directory structure.
    /// </summary>
    /// <remarks>Please have a look at the existing implemention
    /// of the tobii system in the namespace Tobii.</remarks>
    public abstract void ChangeSettings();

    /// <summary>
    /// Dispose the <see cref="Tracker"/> if applicable
    /// by a call to <see cref="ITracker.CleanUp()"/> and 
    /// removing the connected event handlers.
    /// </summary>
    public virtual void Dispose()
    {
      this.CleanUp();

      this.subjectButton.Enabled = false;

      if (this.calibrateButton != null)
      {
        this.calibrateButton.Enabled = false;
      }

      if (this.connectButton != null)
      {
        this.connectButton.BackColor = Color.Transparent;
        this.connectButton.Enabled = true;
      }

      this.recordButton.BackColor = Color.Transparent;

      this.GazeDataChanged -= this.recordModule.TrackerGazeDataChanged;
    }

    /// <summary>
    /// This method starts the presentation and recording for
    /// the current tracker.
    /// </summary>
    public void StartRecording()
    {
      // Cancel if button is not available
      if (!this.recordButton.Enabled)
      {
        return;
      }

      // Cancel presentation and recording if record button is
      // clicked again.
      if (this.recordButton.BackColor == Color.Red)
      {
        if (this.recordModule.Presenter != null)
        {
          AsyncHelper.FireAsync(new MethodInvoker(this.ClosePresentation));
        }

        this.recordButton.BackColor = Color.Transparent;
        return;
      }

      // Check for unique subject name
      if (!Queries.CheckDatabaseForExistingSubject(this.subjectNameTextBox.Text))
      {
        this.PrepareRecording();

        if (this.recordModule.StartRecording())
        {
          // This avoids having trouble when the mouse focus switches to the record 
          // module from the presentation module and the user presses 
          // space or enter which would raise a record button click again, which 
          // is not allowed during recording.
          this.subjectNameTextBox.Focus();

          // Disable buttons
          this.recordButton.BackColor = Color.Red;
          this.subjectButton.Enabled = false;
          if (this.calibrateButton != null)
          {
            this.calibrateButton.Enabled = false;
          }
        }
      }
    }

    #endregion //PUBLICMETHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Inherited methods                                                         //
    ///////////////////////////////////////////////////////////////////////////////
    #region OVERRIDES

    /// <summary>
    /// This method initializes custom components of the 
    /// implemented tracking device.
    /// </summary>
    protected abstract void Initialize();

    /// <summary>
    /// Implementors should use this method to customize the
    /// preparation of the recording.
    /// </summary>
    protected virtual void PrepareRecording()
    {
    }

    /// <summary>
    /// This method shows a <see cref="SubjectDetailsDialog"/> dialog
    /// for the user to enter subjects information.
    /// This information is stored for later writing to the database.
    /// </summary>
    /// <param name="subjectname">Ref. A <see cref="string"/> with the subject name to update.</param>
    /// <returns><strong>True</strong>if succesfull, otherswise <strong>false</strong>.</returns>
    protected bool OpenSubjectDialog(ref string subjectname)
    {
      var dlg = new SubjectDetailsDialog { SubjectName = subjectname };
      if (dlg.ShowDialog() == DialogResult.OK)
      {
        this.subject.SubjectName = dlg.SubjectName;
        this.subject.Category = dlg.Category;
        this.subject.Age = dlg.Age;
        this.subject.Sex = dlg.Sex;
        this.subject.Handedness = dlg.Handedness;
        this.subject.Comments = dlg.Comments;
        subjectname = dlg.SubjectName;
        Document.ActiveDocument.SelectionState.Update(subjectname, null, null, null);
        return true;
      }

      return false;
    }

    /// <summary>
    /// <see cref="XmlSerializer.UnknownNode"/> event handler for
    /// the <see cref="XmlSerializer"/>, that raises
    /// a error message box when getting an unknown node in the
    /// xml settings file.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A <see cref="XmlNodeEventArgs"/> that contains the event data.</param>
    protected void SerializerUnknownNode(object sender, XmlNodeEventArgs e)
    {
      string message = "Unknown Node:" + e.Name + "\t" + e.Text
        + Environment.NewLine + " in tracker settings file."
        + Environment.NewLine + "I try to ignore it.";

      InformationDialog.Show("XML error", message, false, MessageBoxIcon.Information);
    }

    /// <summary>
    /// <see cref="XmlSerializer.UnknownAttribute"/> event handler for
    /// the <see cref="XmlSerializer"/>, that raises
    /// a error message box when getting an unknown attribute in the
    /// xml settings file.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A <see cref="XmlAttributeEventArgs"/> that contains the event data.</param>
    protected void SerializerUnknownAttribute(object sender, XmlAttributeEventArgs e)
    {
      var attr = e.Attr;
      var message = "Unknown attribute " + attr.Name + "='"
        + attr.Value + "'" + Environment.NewLine
        + " in tracker settings file." + Environment.NewLine
        + "I try to ignore it.";

      InformationDialog.Show("Unknown attribute", message, false, MessageBoxIcon.Information);
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
    /// <see cref="Button"/> <see cref="connectButton"/>.
    /// Calls the <see cref="ITracker.Connect()"/> method and 
    /// enables subject definition button.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/>.</param>
    protected virtual void BtnConnectClick(object sender, EventArgs e)
    {
      // Cancel presentation and recording and
      // disconnect if connect button is
      // clicked again.
      if (this.connectButton.BackColor == Color.Green)
      {
        if (this.recordModule.Presenter != null)
        {
          this.recordModule.Presenter.EndPresentation(true);
        }

        this.recordButton.BackColor = Color.Transparent;
        this.connectButton.BackColor = Color.Transparent;
        this.connectButton.Enabled = true;
        this.subjectButton.Enabled = false;

        if (this.calibrateButton != null)
        {
          this.calibrateButton.Enabled = false;
        }

        this.recordButton.Enabled = false;
        this.Dispose();
        return;
      }

      Cursor.Current = Cursors.WaitCursor;
      if (this.Connect())
      {
        this.connectButton.BackColor = Color.Green;
        this.subjectButton.Enabled = true;
      }

      Cursor.Current = Cursors.Default;
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="subjectButton"/>.
    /// Calls the <see cref="OpenSubjectDialog(ref string)"/> method
    /// to specify the new subject and parameters and 
    /// enables calibration button.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/>.</param>
    protected virtual void BtnSubjectNameClick(object sender, EventArgs e)
    {
      string subjectName = this.subjectNameTextBox.Text;
      if (this.OpenSubjectDialog(ref subjectName))
      {
        this.subjectNameTextBox.Text = subjectName;
        this.subject.SubjectName = subjectName;

        if (this.calibrateButton != null)
        {
          this.calibrateButton.Enabled = true;
        }
      }
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="calibrateButton"/>.
    /// Calls the <see cref="ITracker.Calibrate(Boolean)"/> method
    /// for interface specific calibration.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/>.</param>
    protected virtual void BtnCalibrateClick(object sender, EventArgs e)
    {
      this.Calibrate(false);
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="recordButton"/>.
    /// Calls the <see cref="PrepareRecording()"/> method
    /// which should be implemented to customize the 
    /// initialization of the recording.
    /// Then starts the recording of the <see cref="RecordModule"/>,
    /// showing the Presenter.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/>.</param>
    /// <remarks>The button can be used for starting and
    /// stopping and is changed to red backgroundcolor as
    /// long as the recording is active.</remarks>
    protected virtual void BtnRecordClick(object sender, EventArgs e)
    {
      this.StartRecording();
    }

    #endregion //WINDOWSEVENTHANDLER

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler for Custom Defined Events                                    //
    ///////////////////////////////////////////////////////////////////////////////
    #region CUSTOMEVENTHANDLER

    /// <summary>
    /// Calls listeners methods (which is always the <see cref="RecordModule"/>)
    /// when new gaze data is available.
    /// </summary>
    /// <param name="e"><see cref="GazeDataChangedEventArgs"/> event arguments</param>.
    protected void OnGazeDataChanged(GazeDataChangedEventArgs e)
    {
      if (this.GazeDataChanged != null)
      {
        this.GazeDataChanged(this, e);
      }
    }

    /// <summary>
    /// Raised when the recorder has finished a recording.
    /// Resets the button states.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/>.</param>
    protected virtual void RecordModuleNewRecordingFinished(object sender, EventArgs e)
    {
      this.recordButton.BackColor = Color.Transparent;
      if (this.connectButton != null)
      {
        this.connectButton.Enabled = true;
      }

      this.subjectButton.Enabled = true;
      if (this.calibrateButton != null)
      {
        this.calibrateButton.Enabled = true;
      }

      // If subject data has been written to database
      // disable the record button, because
      // the subject should be newly set via
      // a click to the subject button.
      string subjectName = this.subjectNameTextBox.Text;
      if (!Queries.ValidateSubjectName(ref subjectName, true))
      {
        this.recordButton.Enabled = false;
      }
    }

    /// <summary>
    /// This method stops the presentation if it is active.
    /// </summary>
    private void ClosePresentation()
    {
      if (this.recordModule.Presenter != null)
      {
        this.recordModule.Presenter.EndPresentation(true);
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
    #endregion //METHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER
    #endregion //HELPER
  }
}