// <copyright file="MirametrixSettingsDialog.cs" company="Mirametrix">
// ******************************************************
// OGAMA - open gaze and mouse analyzer 
// Copyright (C) 2013 Dr. Adrian Voßkühler  
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
  using System.Windows.Forms;

  /// <summary>
  /// Popup form to specify settings for the Mirametrix system.
  /// </summary>
  public partial class MirametrixSettingsDialog : Form
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
    /// Saves the current <see cref="MirametrixSetting"/>.
    /// </summary>
    private MirametrixSetting memMirametrixSettings;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the MirametrixSettingsDialog class.
    /// </summary>
    public MirametrixSettingsDialog()
    {
      this.InitializeComponent();
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets or sets the <see cref="MirametrixSetting"/> class.
    /// </summary>
    /// <value>A <see cref="MirametrixSetting"/> with the current settings.</value>
    public MirametrixSetting MirametrixSetting
    {
      get
      {
        return this.memMirametrixSettings;
      }

      set
      {
        this.memMirametrixSettings = value;
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
    /// <see cref="MirametrixSetting"/> member.
    /// </summary>
    private void RealizeNewSettings()
    {
      switch (this.memMirametrixSettings.CalibrationType)
      {
        case 0:
          this.rdbMirametrixManualCalib.Checked = true;
          break;
        case 1:
          this.rdbMirametrixAutoCalib.Checked = true;
          break;
      }

      switch (this.memMirametrixSettings.IsCalibFast)
      {
        case false:
          this.rdbMirametrixTimeOutCalib.Checked = true;
          this.labelSpeedInSec.Enabled = true;
          this.txbTimeOut.Enabled = true;
          break;
        case true:
          this.rdbMirametrixFastCalib.Checked = true;
          this.labelSpeedInSec.Enabled = false;
          this.txbTimeOut.Enabled = false;
          break;
      }

      switch (this.memMirametrixSettings.HideCalibWindow)
      {
        case true:
          this.chbHideCalibWindow.Checked = true;
          break;
        case false:
          this.chbHideCalibWindow.Checked = false;
          break;
      }

      this.txbServerAddress.Text = this.MirametrixSetting.ServerAddress;
      this.txbServerPort.Text = this.MirametrixSetting.ServerPort.ToString();
      this.txbTimeOut.Text = Convert.ToString(this.MirametrixSetting.CalibPointSpeed);
    }

    /// <summary>
    /// The <see cref="Control.TextChanged"/> event handler for
    /// the <see cref="TextBox"/> <see cref="txbTimeOut"/>.
    /// Updates current Mirametrix S2 settings with the new time out for calibration.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void txbTimeOut_TextChanged(object sender, EventArgs e)
    {
      if (this.txbTimeOut.Text.Length == 0)
      {
        this.txbTimeOut.Text = "0";
      }

      this.MirametrixSetting.CalibPointSpeed = Convert.ToInt32(this.txbTimeOut.Text);
    }

    /// <summary>
    /// The <see cref="Control.TextChanged"/> event handler for
    /// the <see cref="TextBox"/> <see cref="txbServerPort"/>.
    /// Updates current Mirametrix S2 settings with the server port.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void txbServerPort_TextChanged(object sender, EventArgs e)
    {
      this.MirametrixSetting.ServerPort = Convert.ToInt32(this.txbServerPort.Text);
    }

    /// <summary>
    /// The <see cref="Control.TextChanged"/> event handler for
    /// the <see cref="TextBox"/> <see cref="txbServerAddress"/>.
    /// Updates current Mirametrix S2 settings with the server address.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void txbServerAddress_TextChanged(object sender, EventArgs e)
    {
      this.MirametrixSetting.ServerAddress = this.txbServerAddress.Text;
    }

    /// <summary>
    /// The <see cref="RadioButton.CheckedChanged"/> event handler for
    /// the manual calibration start <see cref="RadioButton"/>s.
    /// Updates the start calibration to manual by calling
    /// /// <see cref="SetCalibrationStartType()"/>
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void rdbMirametrixCalibStartType_CheckedChanged(object sender, EventArgs e)
    {
      this.SetCalibrationStartType();
    }

    /// <summary>
    /// The <see cref="RadioButton.CheckedChanged"/> event handler for
    /// the manual calibration start <see cref="RadioButton"/>s.
    /// Updates the start calibration to manual by calling
    /// /// <see cref="SetCalibrationSpeedType()"/>
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void rdbMirametrixCalibSpeedType_CheckedChanged(object sender, EventArgs e)
    {
      this.SetCalibrationSpeedType();
    }

    /// <summary>
    /// The <see cref="CheckBox.CheckedChanged"/> event handler for
    /// the calibration hidden window <see cref="RadioButton"/>s.
    /// Updates the non-visibility of calibration window after calibration by calling
    /// /// <see cref="SetCalibrationSpeedType()"/>
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty</param>
    private void chbHideCalibWindow_CheckedChanged(object sender, EventArgs e)
    {
      this.SetHiddenWindow();
    }

    /// <summary>
    /// Updates the calibration start
    /// </summary>
    private void SetCalibrationStartType()
    {
      if (this.rdbMirametrixManualCalib.Checked)
      {
        this.MirametrixSetting.CalibrationType = 0;
      }
      else if (this.rdbMirametrixAutoCalib.Checked)
      {
        this.MirametrixSetting.CalibrationType = 1;
      }
    }

    /// <summary>
    /// Updates the visibility of the calibration window
    /// </summary>
    private void SetHiddenWindow()
    {
      if (this.chbHideCalibWindow.Checked)
      {
        this.MirametrixSetting.HideCalibWindow = true;
      }
      else
      {
        this.MirametrixSetting.HideCalibWindow = false;
      }
    }

    /// <summary>
    /// Updates the calibration speed type
    /// </summary>
    private void SetCalibrationSpeedType()
    {
      if (this.rdbMirametrixTimeOutCalib.Checked)
      {
        this.txbTimeOut.Enabled = true;
        this.MirametrixSetting.IsCalibFast = false;
        this.MirametrixSetting.CalibPointSpeed = Convert.ToInt32(this.txbTimeOut.Text);
      }
      else if (this.rdbMirametrixFastCalib.Checked)
      {
        this.txbTimeOut.Enabled = false;
        this.MirametrixSetting.IsCalibFast = true;
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