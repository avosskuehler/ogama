// <copyright file="ASLCalibration.cs" company="FU Berlin">
// ******************************************************
// OGAMA - open gaze and mouse analyzer 
// Copyright (C) 2015 Dr. Adrian Voßkühler  
// ------------------------------------------------------------------------
// This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
// This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
// You should have received a copy of the GNU General Public License along with this program; if not, write to the Free Software Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
// **************************************************************
// </copyright>
// <author>
//  University Toulouse 2 - CLLE-LTC UMR5263
//  Yves Lecourt
// </author>
// <email>virginie.feraud@univ-tlse2.fr</email>

namespace Ogama.Modules.Recording.ASLInterface
{
  using System;
  using System.Windows.Forms;

  using Ogama.ExceptionHandling;

  // for InformationDialog

  /// <summary>
  /// A popup form to display a nine-point calibration screen
  /// </summary>
  public partial class AslCalibration : Form
  {
    ///////////////////////////////////////////////////////////////////////////////
    // Defining Constants                                                        //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTANTS
    #endregion //CONSTANTS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Enumerations                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region ENUMS
    #endregion ENUMS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Variables, Enumerations, Events                                  //
    ///////////////////////////////////////////////////////////////////////////////
    #region FIELDS
    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the AslCalibration class.
    /// </summary>
    public AslCalibration()
    {
      int experimentWidth, experimentHeight;
      this.InitializeComponent();

      experimentWidth = Document.ActiveDocument.ExperimentSettings.WidthStimulusScreen;
      experimentHeight = Document.ActiveDocument.ExperimentSettings.HeightStimulusScreen;

      if ((experimentWidth == 1280) && (experimentHeight == 1024))
      {
        this.calibrationPictureBox.Image = global::Ogama.Properties.Resources.ASLmire_1280x1024;
      }
      else if ((experimentWidth == 1024) && (experimentHeight == 768))
      {
        this.calibrationPictureBox.Image = global::Ogama.Properties.Resources.ASLmire_1024x768;
      }
      else
      {
        this.calibrationPictureBox.Image = global::Ogama.Properties.Resources.ASLmire_defaultSize;
      }

      // We must set KeyPreview object to true to allow the form to process 
      // the key before the control with focus processes it.
      this.KeyPreview = true;

      // Associate the event-handling method with the KeyDown event.
      this.KeyDown += new KeyEventHandler(this.AslCalibration_KeyDown);
    }

    #endregion //CONSTRUCTION

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
    /// Declaration of AslCalibrationDone Event.
    /// <remarks>
    /// It's raised at the end of eye tracker calibration.
    /// The attached method is AslTrackStatus.aslInterface_CalibrationDone().
    /// </remarks>
    /// </summary>
    public event EventHandler AslCalibrationDone;

    /// <summary>
    /// The <see cref="Control.KeyDown"/> event handler.
    /// When the key 'escape' is pressed, pull this AslCalibrationDone event
    /// to indicate to AslTracker, his subscriber, the end of calibration 
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">The <see cref="KeyEventArgs"/> with the event data.</param>
    private void AslCalibration_KeyDown(object sender, KeyEventArgs e)
    {
      if (this.isEscape(e.KeyCode))
      {
        DialogResult result;
        result = InformationDialog.Show(
            "Escape ?",
            "Do you want to leave this calibration screen ?",
            true, // yes no and cancel buttons are shown
            MessageBoxIcon.Question);

        if (result == DialogResult.Yes)
        {
          this.KeyDown -= new KeyEventHandler(this.AslCalibration_KeyDown);
          this.OnAslCalibrationDone(this, new EventArgs());
        }
      }
    }

    #endregion //WINDOWSEVENTHANDLER

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler for Custom Defined Events                                    //
    ///////////////////////////////////////////////////////////////////////////////
    #region CUSTOMEVENTHANDLER

    /// <summary>
    /// AslCalibrationDone Event
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void OnAslCalibrationDone(object sender, EventArgs e)
    {
      if (this.AslCalibrationDone != null)
      {
        this.AslCalibrationDone(sender, e);
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
    // Methods for doing main class job                                          //
    ///////////////////////////////////////////////////////////////////////////////
    #region METHODS

    #endregion //METHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER

    /// <summary>
    /// Check if key entered is "Escape".
    /// </summary>
    /// <param name="key">A <see cref="Keys"/> to check.</param>
    /// <returns><strong>True</strong> if given key is Escape,
    /// otherwise <strong>false</strong></returns>
    private bool isEscape(Keys key)
    {
      if (key == Keys.Escape)
      {
        return true;
      }
      else
      {
        return false;
      }
    }

    #endregion //HELPER
  }
}