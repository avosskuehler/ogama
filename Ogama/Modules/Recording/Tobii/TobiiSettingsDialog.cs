// <copyright file="TobiiSettingsDialog.cs" company="FU Berlin">
// ******************************************************
// OGAMA - open gaze and mouse analyzer 
// Copyright (C) 2010 Adrian Voßkühler  
// ------------------------------------------------------------------------
// This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
// This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
// You should have received a copy of the GNU General Public License along with this program; if not, write to the Free Software Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
// **************************************************************
// </copyright>
// <author>Adrian Voßkühler</author>
// <email>adrian.vosskuehler@fu-berlin.de</email>

namespace Ogama.Modules.Recording.Tobii
{
  using System;
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.Data;
  using System.Drawing;
  using System.Text;
  using System.Windows.Forms;

  using Ogama.Properties;

#if TOBII
  using TetComp;
#endif

  /// <summary>
  /// Popup form to specify settings for the tobii system.
  /// </summary>
  public partial class TobiiSettingsDialog : Form
  {
#if TOBII
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
    /// Saves the current <see cref="TobiiSetting"/>.
    /// </summary>
    private TobiiSetting tobiiSettings;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the TobiiSettingsDialog class.
    /// </summary>
    public TobiiSettingsDialog()
    {
      this.InitializeComponent();
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets or sets the <see cref="TobiiSetting"/> class.
    /// </summary>
    /// <value>A <see cref="TobiiSetting"/> with the current settings.</value>
    public TobiiSetting TobiiSettings
    {
      get
      {
        return this.tobiiSettings;
      }

      set
      {
        this.tobiiSettings = value;
        this.RealizeNewSettings();
      }
    }

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
    /// The <see cref="Control.TextChanged"/> event handler for
    /// the <see cref="TextBox"/> <see cref="txbServerAddress"/>.
    /// Updates current tobii settings with the new server address.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void txbServerAddress_TextChanged(object sender, EventArgs e)
    {
      this.tobiiSettings.TetServerAddress = this.txbServerAddress.Text;
    }

    /// <summary>
    /// The <see cref="Control.TextChanged"/> event handler for
    /// the <see cref="TextBox"/> <see cref="txbServerPort"/>.
    /// Updates current tobii settings with the new server port.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void txbServerPort_TextChanged(object sender, EventArgs e)
    {
      this.tobiiSettings.TetServerPort = Convert.ToInt32(this.txbServerPort.Text);
    }

    /// <summary>
    /// The <see cref="RadioButton.CheckedChanged"/> event handler for
    /// the num points <see cref="RadioButton"/>s.
    /// Updates current tobii number of calibration point setting by calling
    /// <see cref="SetNumberOfCalibrationPoints()"/>
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void rdbTobiiNumPtsCalib_CheckedChanged(object sender, EventArgs e)
    {
      this.SetNumberOfCalibrationPoints();
    }

    /// <summary>
    /// The <see cref="RadioButton.CheckedChanged"/> event handler for
    /// the size <see cref="RadioButton"/>s.
    /// Updates current tobii calibration point size settings by calling
    /// <see cref="SetSizeOfCalibrationPoints()"/>
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void rdbTobiiSize_CheckedChanged(object sender, EventArgs e)
    {
      this.SetSizeOfCalibrationPoints();
    }

    /// <summary>
    /// The <see cref="RadioButton.CheckedChanged"/> event handler for
    /// the speed <see cref="RadioButton"/>s.
    /// Updates current tobii calibration point speed setting by calling
    /// <see cref="SetSpeedOfCalibrationPoints()"/>
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void rdbTobiiSpeed_CheckedChanged(object sender, EventArgs e)
    {
      this.SetSpeedOfCalibrationPoints();
    }

    /// <summary>
    /// The <see cref="OgamaControls.ColorButton.ColorChanged"/> event handler for
    /// the <see cref="OgamaControls.ColorButton"/> <see cref="clbTobiiPointColor"/>.
    /// Updates current tobii calibration point color setting.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void clbTobiiPointColor_ColorChanged(object sender, EventArgs e)
    {
      this.tobiiSettings.TetCalibPointColor = this.clbTobiiPointColor.CurrentColor;
    }

    /// <summary>
    /// The <see cref="OgamaControls.ColorButton.ColorChanged"/> event handler for
    /// the <see cref="OgamaControls.ColorButton"/> <see cref="clbTobiiBackColor"/>.
    /// Updates current tobii calibration background color setting.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void clbTobiiBackColor_ColorChanged(object sender, EventArgs e)
    {
      this.tobiiSettings.TetCalibBackgroundColor = this.clbTobiiBackColor.CurrentColor;
    }

    /// <summary>
    /// The <see cref="CheckBox.CheckedChanged"/> event handler for
    /// the <see cref="CheckBox"/> <see cref="chbRandomizePointOrder"/>.
    /// Updates current tobii calibration point randomization setting.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void chbRandomizePointOrder_CheckedChanged(object sender, EventArgs e)
    {
      this.tobiiSettings.TetRandomizeCalibPointOrder = this.chbRandomizePointOrder.Checked;
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
    // Inherited methods                                                         //
    ///////////////////////////////////////////////////////////////////////////////
    #region OVERRIDES
    #endregion //OVERRIDES

    ///////////////////////////////////////////////////////////////////////////////
    // Methods for doing main class job                                          //
    ///////////////////////////////////////////////////////////////////////////////
    #region METHODS

    /// <summary>
    /// Updates the forms UI with the new settings from the 
    /// <see cref="TobiiSetting"/> member.
    /// </summary>
    private void RealizeNewSettings()
    {
      switch (this.tobiiSettings.TetCalibPointSizes)
      {
        case TetComp.TetCalibPointSize.TetCalibPointSize_Large:
          this.rdbTobiiSizeLarge.Checked = true;
          break;
        case TetComp.TetCalibPointSize.TetCalibPointSize_Medium:
          this.rdbTobiiSizeMedium.Checked = true;
          break;
        case TetComp.TetCalibPointSize.TetCalibPointSize_Small:
          this.rdbTobiiSizeSmall.Checked = true;
          break;
        default:
          this.rdbTobiiSizeMedium.Checked = true;
          break;
      }

      switch (this.tobiiSettings.TetCalibPointSpeeds)
      {
        case TetComp.TetCalibPointSpeed.TetCalibPointSpeed_Fast:
          this.rdbTobiiSpeedFast.Checked = true;
          break;
        case TetComp.TetCalibPointSpeed.TetCalibPointSpeed_Medium:
          this.rdbTobiiSpeedMedium.Checked = true;
          break;
        case TetComp.TetCalibPointSpeed.TetCalibPointSpeed_MediumFast:
          this.rdbTobiiSpeedMediumFast.Checked = true;
          break;
        case TetComp.TetCalibPointSpeed.TetCalibPointSpeed_MediumSlow:
          this.rdbTobiiSpeedMediumSlow.Checked = true;
          break;
        case TetComp.TetCalibPointSpeed.TetCalibPointSpeed_Slow:
          this.rdbTobiiSpeedSlow.Checked = true;
          break;
        default:
          this.rdbTobiiSpeedMediumSlow.Checked = true;
          break;
      }

      switch (this.tobiiSettings.TetNumCalibPoint)
      {
        case TetComp.TetNumCalibPoints.TetNumCalibPoints_2:
          this.rdbTobii2PtsCalib.Checked = true;
          break;
        case TetComp.TetNumCalibPoints.TetNumCalibPoints_5:
          this.rdbTobii5PtsCalib.Checked = true;
          break;
        case TetComp.TetNumCalibPoints.TetNumCalibPoints_9:
          this.rdbTobii9PtsCalib.Checked = true;
          break;
        default:
          this.rdbTobii5PtsCalib.Checked = true;
          break;
      }

      this.txbServerAddress.Text = this.tobiiSettings.TetServerAddress;
      this.txbServerPort.Text = this.tobiiSettings.TetServerPort.ToString();
      this.clbTobiiBackColor.CurrentColor = this.tobiiSettings.TetCalibBackgroundColor;
      this.clbTobiiPointColor.CurrentColor = this.tobiiSettings.TetCalibPointColor;
      this.chbRandomizePointOrder.Checked = this.tobiiSettings.TetRandomizeCalibPointOrder;
    }

    /// <summary>
    /// Updates the current tobii settings with
    /// the number of points to use when calibrating.
    /// </summary>
    private void SetNumberOfCalibrationPoints()
    {
      if (this.rdbTobii9PtsCalib.Checked)
      {
        this.tobiiSettings.TetNumCalibPoint = TetNumCalibPoints.TetNumCalibPoints_9;
      }
      else if (this.rdbTobii5PtsCalib.Checked)
      {
        this.tobiiSettings.TetNumCalibPoint = TetNumCalibPoints.TetNumCalibPoints_5;
      }
      else if (this.rdbTobii2PtsCalib.Checked)
      {
        this.tobiiSettings.TetNumCalibPoint = TetNumCalibPoints.TetNumCalibPoints_2;
      }
    }

    /// <summary>
    /// Updates the current tobii settings with
    /// the number of points to use during calibration.
    /// </summary>
    private void SetSizeOfCalibrationPoints()
    {
      if (this.rdbTobiiSizeLarge.Checked)
      {
        this.tobiiSettings.TetCalibPointSizes = TetCalibPointSize.TetCalibPointSize_Large;
      }
      else if (this.rdbTobiiSizeMedium.Checked)
      {
        this.tobiiSettings.TetCalibPointSizes = TetCalibPointSize.TetCalibPointSize_Medium;
      }
      else if (this.rdbTobiiSizeSmall.Checked)
      {
        this.tobiiSettings.TetCalibPointSizes = TetCalibPointSize.TetCalibPointSize_Small;
      }
    }

    /// <summary>
    /// Updates the current tobii settings with
    /// the speed of the calibration during calibration.
    /// </summary>
    private void SetSpeedOfCalibrationPoints()
    {
      if (this.rdbTobiiSpeedFast.Checked)
      {
        this.tobiiSettings.TetCalibPointSpeeds = TetCalibPointSpeed.TetCalibPointSpeed_Fast;
      }
      else if (this.rdbTobiiSpeedMediumFast.Checked)
      {
        this.tobiiSettings.TetCalibPointSpeeds = TetCalibPointSpeed.TetCalibPointSpeed_MediumFast;
      }
      else if (this.rdbTobiiSpeedMedium.Checked)
      {
        this.tobiiSettings.TetCalibPointSpeeds = TetCalibPointSpeed.TetCalibPointSpeed_Medium;
      }
      else if (this.rdbTobiiSpeedMediumSlow.Checked)
      {
        this.tobiiSettings.TetCalibPointSpeeds = TetCalibPointSpeed.TetCalibPointSpeed_MediumSlow;
      }
      else if (this.rdbTobiiSpeedSlow.Checked)
      {
        this.tobiiSettings.TetCalibPointSpeeds = TetCalibPointSpeed.TetCalibPointSpeed_Slow;
      }
    }

    #endregion //METHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER
    #endregion //HELPER
#endif
  }
}

