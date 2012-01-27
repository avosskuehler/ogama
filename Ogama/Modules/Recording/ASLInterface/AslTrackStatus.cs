// <copyright file="AslTrackStatus.cs" company="FU Berlin">
// ******************************************************
// OGAMA - open gaze and mouse analyzer 
// Copyright (C) 2012 Adrian Voßkühler  
// ------------------------------------------------------------------------
// This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
// This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
// You should have received a copy of the GNU General Public License along with this program; if not, write to the Free Software Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
// **************************************************************
// </copyright>
// <author>
//  University Toulouse 2 - CLLE-LTC UMR5263
//  Yves LECOURT
//  </author>
// <email>virginie.feraud@univ-tlse2.fr</email>

namespace Ogama.Modules.Recording.ASLInterface
{
  using System;
  using System.Drawing;
  using System.Windows.Forms;

  /// <summary>
  /// A dialog <see cref="Form"/> which exposes the Setting Up Calibration to the subject.
  /// </summary>
  public partial class AslTrackStatus : Form
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
    /// Saves the track status dialog that can be shown
    /// to the subject during calibration
    /// </summary>
    private AslCalibration scrCalibration;

    #endregion //FIELDS
    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the AslTrackStatus class.
    /// </summary>
    public AslTrackStatus()
    {
      this.InitializeComponent();

      // call the additional local initialize method 
      this.initialize();
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Methods for doing main class job                                          //
    ///////////////////////////////////////////////////////////////////////////////
    #region METHODS

    /// <summary>
    /// Initialize the screen texts in french or english.
    /// </summary>
    protected void initialize()
    {
      this.Text = "Setting Up Calibration";
      this.lblDescription.Text = "    The eye tracker requires a calibration to learn the characteristics of your eye."
          + Environment.NewLine
          + "   The purpose of the eye calibration is to provide the relation between your pupil centre and your corneal reflection (which differs for each subject)."
          + Environment.NewLine
          + "   Please seat yourself in a comfortable position and listen the operator's asks."
          + Environment.NewLine
          + "   During the calibration procedure you have to fix in succession each of the test map points."
          + Environment.NewLine
          + "   The calibration time for each subject will vary.";
      this.lblDescription2.Text = "    The next view is set to remain on the screen until the operator tell you to press Escape key.";
      this.lblDescription2.ForeColor = Color.Red;
      this.continueButton.Text = "&Continue";
    }

    #endregion //METHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES
    #endregion //PROPERTIES
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
    /// <see cref="Button"/> continue.
    /// Shows up the test map to calibrate the ASL eye tracker.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/>.</param>
    private void continueButton_Click(object sender, EventArgs e)
    {
      if (this.scrCalibration != null)
      {
        this.scrCalibration.Dispose();
      }

      this.scrCalibration = new AslCalibration();

      // Wires the AslCalibrationDone event of this AslCalibration
      // to the aslInterface_CalibrationDone method
      this.scrCalibration.AslCalibrationDone += new EventHandler(this.aslInterface_CalibrationDone);

      // Display the picture in full screen
      this.scrCalibration.ClientSize = new System.Drawing.Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
      this.scrCalibration.Location = new Point(0, 0);
      this.scrCalibration.Show();
    }

    #endregion //WINDOWSEVENTHANDLER
    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler for Custom Defined Events                                    //
    ///////////////////////////////////////////////////////////////////////////////
    #region CUSTOMEVENTHANDLER

    /// <summary>
    /// CalibrationDone Event handler.
    /// Calibration is done, so unplug this event handlers and close the specific forms
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/>.</param>
    private void aslInterface_CalibrationDone(object sender, EventArgs e)
    {
      if (this.scrCalibration != null)
      {
        this.continueButton.Click -= new System.EventHandler(this.continueButton_Click);
        this.scrCalibration.AslCalibrationDone -= new EventHandler(this.aslInterface_CalibrationDone);
        this.scrCalibration.Close();
        this.Close();
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
    // Inherited methods                                                         //
    ///////////////////////////////////////////////////////////////////////////////
    #region OVERRIDES
    #endregion //OVERRIDES

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER
    #endregion //HELPER
  }
}