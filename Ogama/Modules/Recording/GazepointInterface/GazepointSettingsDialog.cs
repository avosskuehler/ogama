// <copyright file="GazepointSettingsDialog.cs" company="Gazepoint">
// ******************************************************
// OGAMA - open gaze and mouse analyzer 
// Copyright (C) 2015 Dr. Adrian Voßkühler  
// ------------------------------------------------------------------------
// This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
// This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
// You should have received a copy of the GNU General Public License along with this program; if not, write to the Free Software Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
// **************************************************************
// </copyright>
// <author>Herval Yatchou</author>
// <email>herval.yatchou@tandemlaunchtech.com</email>
// <modifiedby>Andras Pattantyus, andras@gazept.com</modifiedby>

namespace Ogama.Modules.Recording.GazepointInterface
{
  using System;
  using System.Windows.Forms;

  /// <summary>
  /// Popup form to specify settings for the Gazepoint system.
  /// </summary>
  public partial class GazepointSettingsDialog : Form
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
    /// Saves the current <see cref="GazepointSetting"/>.
    /// </summary>
    private GazepointSetting memGazepointSettings;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the GazepointSettingsDialog class.
    /// </summary>
    public GazepointSettingsDialog()
    {
      this.InitializeComponent();
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets or sets the <see cref="GazepointSetting"/> class.
    /// </summary>
    /// <value>A <see cref="GazepointSetting"/> with the current settings.</value>
    public GazepointSetting GazepointSetting
    {
      get
      {
        return this.memGazepointSettings;
      }

      set
      {
        this.memGazepointSettings = value;
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
    /// <see cref="GazepointSetting"/> member.
    /// </summary>
    private void RealizeNewSettings()
    {
      switch (this.memGazepointSettings.CalibrationType)
      {
        case 0:
          this.rdbGazepointManualCalib.Checked = true;
          break;
        case 1:
          this.rdbGazepointAutoCalib.Checked = true;
          break;
      }

      switch (this.memGazepointSettings.IsCalibFast)
      {
        case false:
          this.rdbGazepointTimeOutCalib.Checked = true;
          this.labelSpeedInSec.Enabled = true;
          this.txbTimeOut.Enabled = true;
          break;
        case true:
          this.labelSpeedInSec.Enabled = false;
          this.txbTimeOut.Enabled = false;
          break;
      }

      switch (this.memGazepointSettings.HideCalibWindow)
      {
        case true:
          this.chbHideCalibWindow.Checked = true;
          break;
        case false:
          this.chbHideCalibWindow.Checked = false;
          break;
      }

      this.txbServerAddress.Text = this.GazepointSetting.ServerAddress;
      this.txbServerPort.Text = this.GazepointSetting.ServerPort.ToString();
      this.txbTimeOut.Text = Convert.ToString(this.GazepointSetting.CalibPointSpeed);
    }

    /// <summary>
    /// The <see cref="Control.TextChanged"/> event handler for
    /// the <see cref="TextBox"/> <see cref="txbTimeOut"/>.
    /// Updates current Gazepoint GP3 settings with the new time out for calibration.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void txbTimeOut_TextChanged(object sender, EventArgs e)
    {
      if (this.txbTimeOut.Text.Length == 0)
      {
        this.txbTimeOut.Text = "0";
      }

      this.GazepointSetting.CalibPointSpeed = Convert.ToInt32(this.txbTimeOut.Text);
    }

    /// <summary>
    /// The <see cref="Control.TextChanged"/> event handler for
    /// the <see cref="TextBox"/> <see cref="txbServerPort"/>.
    /// Updates current Gazepoint GP3 settings with the server port.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void txbServerPort_TextChanged(object sender, EventArgs e)
    {
      this.GazepointSetting.ServerPort = Convert.ToInt32(this.txbServerPort.Text);
    }

    /// <summary>
    /// The <see cref="Control.TextChanged"/> event handler for
    /// the <see cref="TextBox"/> <see cref="txbServerAddress"/>.
    /// Updates current Gazepoint GP3 settings with the server address.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void txbServerAddress_TextChanged(object sender, EventArgs e)
    {
      this.GazepointSetting.ServerAddress = this.txbServerAddress.Text;
    }

    /// <summary>
    /// The <see cref="RadioButton.CheckedChanged"/> event handler for
    /// the manual calibration start <see cref="RadioButton"/>s.
    /// Updates the start calibration to manual by calling
    /// /// <see cref="SetCalibrationStartType()"/>
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void rdbGazepointCalibStartType_CheckedChanged(object sender, EventArgs e)
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
    private void rdbGazepointCalibSpeedType_CheckedChanged(object sender, EventArgs e)
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
      if (this.rdbGazepointManualCalib.Checked)
      {
        this.GazepointSetting.CalibrationType = 0;
      }
      else if (this.rdbGazepointAutoCalib.Checked)
      {
        this.GazepointSetting.CalibrationType = 1;
      }
    }

    /// <summary>
    /// Updates the visibility of the calibration window
    /// </summary>
    private void SetHiddenWindow()
    {
      if (this.chbHideCalibWindow.Checked)
      {
        this.GazepointSetting.HideCalibWindow = true;
      }
      else
      {
        this.GazepointSetting.HideCalibWindow = false;
      }
    }

    /// <summary>
    /// Updates the calibration speed type
    /// </summary>
    private void SetCalibrationSpeedType()
    {
      if (this.rdbGazepointTimeOutCalib.Checked)
      {
        this.txbTimeOut.Enabled = true;
        this.GazepointSetting.CalibPointSpeed = Convert.ToInt32(this.txbTimeOut.Text);
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