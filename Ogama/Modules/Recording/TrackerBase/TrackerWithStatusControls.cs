// <copyright file="TrackerWithStatusControls.cs" company="FU Berlin">
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

  /// <summary>
  /// This abstract class derives from <see cref="Tracker"/>
  /// and extends it with support for device status controls
  /// calibration plot and track status.
  /// </summary>
  /// <remarks>The layout and buttons for such devices are
  /// standardized, please look at the existing implementations.</remarks>
  public abstract class TrackerWithStatusControls : Tracker
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
    /// This <see cref="SplitContainer"/> 
    /// contains two <see cref="SplitContainer"/>s with
    /// track status and calibration plot controls and buttons.
    /// </summary>
    private readonly SplitContainer trackerControlsContainer;

    /// <summary>
    /// This <see cref="Panel"/> should contain the
    /// track status object of the implementation.
    /// </summary>
    private readonly Panel trackStatusPanel;

    /// <summary>
    /// This <see cref="Panel"/> should contain the
    /// calibration result object of the implementation.
    /// </summary>
    private readonly Panel calibrationResultPanel;

    /// <summary>
    /// This <see cref="Button"/> is placed at the bottom of
    /// the track status <see cref="SplitContainer"/>.
    /// </summary>
    private readonly Button showOnPresentationScreenButton;

    /// <summary>
    /// This <see cref="Button"/> is placed at the bottom of
    /// the calibration result <see cref="SplitContainer"/>
    /// and named "Accept".
    /// </summary>
    private readonly Button acceptButton;

    /// <summary>
    /// This <see cref="Button"/> is placed at the bottom of
    /// the calibration result <see cref="SplitContainer"/>
    /// and named "Recalibrate".
    /// </summary>
    private readonly Button recalibrateButton;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the TrackerWithStatusControls class.
    /// </summary>
    /// <param name="owningRecordModule">The <see cref="RecordModule"/>
    /// form wich host the recorder.</param>
    /// <param name="trackerTrackerControlsContainer">The <see cref="SplitContainer"/>
    /// control which contains two <see cref="SplitContainer"/>s with
    /// track status and calibration plot controls and buttons.</param>
    /// <param name="trackerTrackStatusPanel">The <see cref="Panel"/>
    /// which should contain the track status object.</param>
    /// <param name="trackerCalibrationResultPanel">The <see cref="Panel"/>
    /// which should contain the calibration result object.</param>
    /// <param name="trackerShowOnPresentationScreenButton">The <see cref="Button"/>
    /// named "ShowOnPresentationScreenButton" at the tab page of the tracking device.</param>
    /// <param name="trackerAcceptButton">The <see cref="Button"/>
    /// named "Accept" at the tab page of the tracking device.</param>
    /// <param name="trackerRecalibrateButton">The <see cref="Button"/>
    /// named "Recalibrate" at the tab page of the tracking device.</param>
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
    protected TrackerWithStatusControls(
      RecordModule owningRecordModule,
      SplitContainer trackerTrackerControlsContainer,
      Panel trackerTrackStatusPanel,
      Panel trackerCalibrationResultPanel,
      Button trackerShowOnPresentationScreenButton,
      Button trackerAcceptButton,
      Button trackerRecalibrateButton,
      Button trackerConnectButton,
      Button trackerSubjectButton,
      Button trackerCalibrateButton,
      Button trackerRecordButton,
      TextBox trackerSubjectNameTextBox,
      string trackerSettingsFile)
      : base(
      owningRecordModule,
      trackerConnectButton,
      trackerSubjectButton,
      trackerCalibrateButton,
      trackerRecordButton,
      trackerSubjectNameTextBox,
      trackerSettingsFile)
    {
      this.trackerControlsContainer = trackerTrackerControlsContainer;
      this.trackStatusPanel = trackerTrackStatusPanel;
      this.calibrationResultPanel = trackerCalibrationResultPanel;
      this.showOnPresentationScreenButton = trackerShowOnPresentationScreenButton;
      this.acceptButton = trackerAcceptButton;
      this.recalibrateButton = trackerRecalibrateButton;

      this.showOnPresentationScreenButton.Click += this.BtnShowOnPresentationScreenClick;
      this.recalibrateButton.Click += this.BtnRecalibrateClick;
      this.acceptButton.Click += this.BtnAcceptCalibrationClick;
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
    /// Gets the <see cref="SplitContainer"/> that
    /// contains two <see cref="SplitContainer"/>s with
    /// track status and calibration plot controls and buttons.
    /// </summary>
    protected SplitContainer TrackerControlsContainer
    {
      get { return this.trackerControlsContainer; }
    }

    /// <summary>
    /// Gets the <see cref="Button"/> that is placed at the bottom of
    /// the track status <see cref="SplitContainer"/>.
    /// </summary>
    protected Button ShowOnSecondaryScreenButton
    {
      get { return this.showOnPresentationScreenButton; }
    }

    /// <summary>
    /// Gets the  <see cref="Button"/> that is placed at the bottom of
    /// the calibration result <see cref="SplitContainer"/>
    /// and named "Accept".
    /// </summary>
    protected Button AcceptButton
    {
      get { return this.acceptButton; }
    }

    /// <summary>
    /// Gets the  <see cref="Button"/> that is placed at the bottom of
    /// the calibration result <see cref="SplitContainer"/>
    /// and named "Recalibrate".
    /// </summary>
    protected Button RecalibrateButton
    {
      get { return this.recalibrateButton; }
    }

    /// <summary>
    /// Gets the  <see cref="Panel"/> that should contain the
    /// track status object of the implementation.
    /// </summary>
    protected Panel TrackStatusPanel
    {
      get { return this.trackStatusPanel; }
    }

    /// <summary>
    /// Gets the  <see cref="Panel"/> that should contain the
    /// calibration result object of the implementation.
    /// </summary>
    protected Panel CalibrationResultPanel
    {
      get { return this.calibrationResultPanel; }
    }

    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Public methods                                                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region PUBLICMETHODS
    #endregion //PUBLICMETHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Inherited methods                                                         //
    ///////////////////////////////////////////////////////////////////////////////
    #region OVERRIDES

    /// <summary>
    /// This base implementation of this method resets
    /// the UI to start conditions
    /// and cleans up the tracking connection.
    /// </summary>
    public override void CleanUp()
    {
      this.ResetUI();
      base.CleanUp();
    }

    /// <summary>
    /// This method initializes custom components of the 
    /// implemented tracking device by calling
    /// <see cref="InitializeStatusControls()"/>
    /// and afterwards <see cref="ShowTrackStatus()"/>
    /// </summary>
    protected override void Initialize()
    {
      this.ShowTrackStatus();
      this.InitializeStatusControls();
    }

    /// <summary>
    /// Implementations of this method should initialize 
    /// the designer components for the custom hardware device tab page.
    /// </summary>
    protected abstract void InitializeStatusControls();

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="Tracker.ConnectButton"/>.
    /// Calls the <see cref="ITracker.Connect()"/> method and 
    /// enables subject definition button.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/>.</param>
    protected override void BtnConnectClick(object sender, EventArgs e)
    {
      // Cancel presentation and recording and
      // disconnect if connect button is
      // clicked again.
      if (this.ConnectButton.BackColor == Color.Green)
      {
        this.CleanUp();
        return;
      }

      Cursor.Current = Cursors.WaitCursor;
      if (this.Connect())
      {
        this.ConnectButton.BackColor = Color.Green;
        this.showOnPresentationScreenButton.Enabled = true;
        this.SubjectButton.Enabled = true;
      }

      Cursor.Current = Cursors.Default;
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
    /// <see cref="Button"/> <see cref="showOnPresentationScreenButton"/>.
    /// Implementors should show the track status object
    /// on the presentation screen.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/>.</param>
    protected virtual void BtnShowOnPresentationScreenClick(object sender, EventArgs e)
    {
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="acceptButton"/>.
    /// Accepts the calibration by enabling the record button
    /// and hiding the calibration plot.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/>.</param>
    protected virtual void BtnAcceptCalibrationClick(object sender, EventArgs e)
    {
      this.CalibrateButton.Enabled = true;
      this.RecordButton.Enabled = true;
      this.ShowTrackStatus();
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="recalibrateButton"/>.
    /// Restarts the calibration routine by calling it
    /// with the recalibrate flag set to true.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/>.</param>
    protected virtual void BtnRecalibrateClick(object sender, EventArgs e)
    {
      this.ShowTrackStatus();
      this.Calibrate(true);
    }

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
    /// This method collapses the split container with the track status
    /// to show the calibration plot.
    /// </summary>
    /// <remarks>The track status is made visible again, when the calibration
    /// is accepted.</remarks>
    protected void ShowCalibPlot()
    {
      this.trackerControlsContainer.Panel1Collapsed = true;
    }

    /// <summary>
    /// This method collapses the split container with the calibration plot
    /// to show the track status.
    /// </summary>
    protected void ShowTrackStatus()
    {
      this.trackerControlsContainer.Panel2Collapsed = true;
    }

    /// <summary>
    /// This method closes an running presentation and 
    /// resets the button state of the showOnPresentationScreenButton
    /// </summary>
    private void ResetUI()
    {
      if (this.RecordModule.Presenter != null)
      {
        this.RecordModule.Presenter.EndPresentation(true);
      }

      if (this.showOnPresentationScreenButton.BackColor == Color.Red)
      {
        this.BtnShowOnPresentationScreenClick(this, EventArgs.Empty);
      }

      this.showOnPresentationScreenButton.Enabled = false;
    }

    #endregion //METHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER
    #endregion //HELPER
  }
}
