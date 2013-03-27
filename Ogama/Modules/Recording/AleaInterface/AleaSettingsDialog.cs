// <copyright file="AleaSettingsDialog.cs" company="alea technologies">
// ******************************************************
// OGAMA - open gaze and mouse analyzer 
// Copyright (C) 2013 Dr. Adrian Voßkühler  
// ------------------------------------------------------------------------
// This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
// This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
// You should have received a copy of the GNU General Public License along with this program; if not, write to the Free Software Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
// **************************************************************
// </copyright>
// <author>Martin Werner</author>
// <email>martin.werner@alea-technologies.de</email>
 
namespace Ogama.Modules.Recording.AleaInterface
{
  using System;
  using System.Windows.Forms;

  /// <summary>
  /// Popup form to specify settings for the alea system.
  /// </summary>
  public partial class AleaSettingsDialog : Form
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
    /// Saves the current <see cref="AleaSetting"/>.
    /// </summary>
    private AleaSetting aleaSettings;
                
    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the AleaSettingsDialog class.
    /// </summary>
    public AleaSettingsDialog()
    {
      this.InitializeComponent();
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets or sets the <see cref="AleaSetting"/> class.
    /// </summary>
    /// <value>A <see cref="AleaSetting"/> with the current settings.</value>
    public AleaSetting AleaSettings
    {
      get
      {
        return this.aleaSettings;
      }

      set
      {
        this.aleaSettings = value;
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
    /// Updates current alea settings with the new server address.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void txbServerAddress_TextChanged(object sender, EventArgs e)
    {
      this.aleaSettings.ServerAddress = this.txbServerAddress.Text;
    }

    /// <summary>
    /// The <see cref="Control.TextChanged"/> event handler for
    /// the <see cref="TextBox"/> <see cref="txbServerPort"/>.
    /// Updates current alea settings with the new server port.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void txbServerPort_TextChanged(object sender, EventArgs e)
    {
      this.aleaSettings.ServerPort = Convert.ToInt32(this.txbServerPort.Text);            
    }

    /// <summary>
    /// The <see cref="Control.TextChanged"/> event handler for
    /// the <see cref="TextBox"/> <see cref="txbClientAddress"/>.
    /// Updates current alea settings with the new client address.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void txbClientAddress_TextChanged(object sender, EventArgs e)
    {
      this.aleaSettings.ClientAddress = this.txbClientAddress.Text;
    }

    /// <summary>
    /// The <see cref="Control.TextChanged"/> event handler for
    /// the <see cref="TextBox"/> <see cref="txbClientPort"/>.
    /// Updates current alea settings with the new client port.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void txbClientPort_TextChanged(object sender, EventArgs e)
    {
      this.aleaSettings.ClientPort = Convert.ToInt32(this.txbClientPort.Text);
    }

    /// <summary>
    /// The <see cref="RadioButton.CheckedChanged"/> event handler for
    /// the num points <see cref="RadioButton"/>s.
    /// Updates current alea number of calibration point setting by calling
    /// <see cref="SetNumberOfCalibrationPoints()"/>
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void rdbAleaNumPtsCalib_CheckedChanged(object sender, EventArgs e)
    {
      this.SetNumberOfCalibrationPoints();
    }

    /// <summary>
    /// The <see cref="RadioButton.CheckedChanged"/> event handler for
    /// the Area of Calibration.
    /// Updates current calibration area by calling
    /// <see cref="SetCalibrationArea()"/>
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void rdbAleaArea_CheckedChanged(object sender, EventArgs e)
    {
      this.SetCalibrationArea();
    }

    /// <summary>
    /// The <see cref="RadioButton.CheckedChanged"/> event handler for
    /// the claibration eye.
    /// Updates current calibration eye by calling
    /// <see cref="SetEye()"/>
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void rdbAleaEye_CheckedChanged(object sender, EventArgs e)
    {
      this.SetEye();
    }

    /// <summary>
    /// The <see cref="CheckBox.CheckedChanged"/> event handler for
    /// the <see cref="CheckBox"/> <see cref="chbRandomizePointOrder"/>.
    /// Updates current alea calibration point randomization setting.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void chbRandomizePointOrder_CheckedChanged(object sender, EventArgs e)
    {
      this.aleaSettings.RandomizeCalibPointOrder = this.chbRandomizePointOrder.Checked;            
    }

    /// <summary>
    /// The <see cref="CheckBox.CheckedChanged"/> event handler for
    /// the <see cref="CheckBox"/> <see cref="chbSkipBadPoints"/>.
    /// Updates current alea skip bad points setting.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void chbSkipBadPoints_CheckedChanged(object sender, EventArgs e)
    {
      this.aleaSettings.SkipBadPoints = this.chbSkipBadPoints.Checked;
    }

    /// <summary>
    /// The <see cref="CheckBox.CheckedChanged"/> event handler for
    /// the <see cref="CheckBox"/> <see cref="chbPlayAudioFeedback"/>.
    /// Updates current alea play audio feedback setting.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void chbPlayAudioFeedback_CheckedChanged(object sender, EventArgs e)
    {
      this.aleaSettings.PlayAudioFeedback = this.chbPlayAudioFeedback.Checked;
    }

    /// <summary>
    /// The <see cref="CheckBox.CheckedChanged"/> event handler for
    /// the <see cref="CheckBox"/> <see cref="chbSlowMode"/>.
    /// Updates current alea slow mode setting.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void chbSlowMode_CheckedChanged(object sender, EventArgs e)
    {
      this.aleaSettings.SlowMode = this.chbSlowMode.Checked;
    }

    /// <summary>
    /// The <see cref="OgamaControls.ColorButton.ColorChanged"/> event handler for
    /// the <see cref="OgamaControls.ColorButton"/> <see cref="clbAleaPointColor"/>.
    /// Updates current alea calibration point color setting.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void clbAleaPointColor_ColorChanged(object sender, EventArgs e)
    {
      this.aleaSettings.CalibPointColor = this.clbAleaPointColor.CurrentColor;
    }

    /// <summary>
    /// The <see cref="OgamaControls.ColorButton.ColorChanged"/> event handler for
    /// the <see cref="OgamaControls.ColorButton"/> <see cref="clbAleaBackColor"/>.
    /// Updates current alea calibration background color setting.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void clbAleaBackColor_ColorChanged(object sender, EventArgs e)
    {
      this.aleaSettings.CalibBackgroundColor = this.clbAleaBackColor.CurrentColor;
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
    /// <see cref="AleaSetting"/> member.
    /// </summary>
    private void RealizeNewSettings()
    {                     
      switch (this.aleaSettings.NumCalibPoint)
      {
        case 1:
          this.rdbAlea1PtsCalib.Checked = true;
          break;
        case 5:
          this.rdbAlea5PtsCalib.Checked = true;
          break;
        case 9:
          this.rdbAlea9PtsCalib.Checked = true;
          break;
        case 16:
          this.rdbAlea16PtsCalib.Checked = true;
          break;
        default:
          this.rdbAlea9PtsCalib.Checked = true;
          break;
      }

      switch (this.aleaSettings.CalibArea)
      {
        case 0: 
          this.rdbAleaAreaFull.Checked = true;
          break;
        case 1: 
          this.rdbAleaAreaCenter.Checked = true;
          break;
        case 2: 
          this.rdbAleaAreaBottom.Checked = true;
          break;
        default:
          this.rdbAleaAreaFull.Checked = true;
          break;
      }

      switch (this.aleaSettings.Eye)
      {
        case 0:
          this.rdbAleaEyeBoth.Checked = true;
          break;
        case 1: 
          this.rdbAleaEyeLeft.Checked = true;
          break;
        case 2: 
          this.rdbAleaEyeRight.Checked = true;
          break;
        default:
          this.rdbAleaEyeBoth.Checked = true;
          break;
      }
        
      this.txbServerAddress.Text = this.aleaSettings.ServerAddress;
      this.txbServerPort.Text = this.aleaSettings.ServerPort.ToString();
      this.txbClientAddress.Text = this.aleaSettings.ClientAddress;
      this.txbClientPort.Text = this.aleaSettings.ClientPort.ToString();

      this.chbRandomizePointOrder.Checked = this.aleaSettings.RandomizeCalibPointOrder;
      this.chbSkipBadPoints.Checked = this.aleaSettings.SkipBadPoints;
      this.chbPlayAudioFeedback.Checked = this.aleaSettings.PlayAudioFeedback;
      this.chbSlowMode.Checked = this.aleaSettings.SlowMode;
        
      this.clbAleaBackColor.CurrentColor = this.aleaSettings.CalibBackgroundColor;
      this.clbAleaPointColor.CurrentColor = this.aleaSettings.CalibPointColor;        
    }

    /// <summary>
    /// Updates the current alea settings with
    /// the number of points to use when calibrating.
    /// </summary>
    private void SetNumberOfCalibrationPoints()
    {
      if (this.rdbAlea16PtsCalib.Checked)
      {
        this.aleaSettings.NumCalibPoint = 16;                 
      }
      else if (this.rdbAlea9PtsCalib.Checked)
      {
        this.aleaSettings.NumCalibPoint = 9;
      }
      else if (this.rdbAlea5PtsCalib.Checked)
      {
        this.aleaSettings.NumCalibPoint = 5;
      }
      else if (this.rdbAlea1PtsCalib.Checked)
      {
        this.aleaSettings.NumCalibPoint = 1;
      }
    }

    /// <summary>
    /// Updates the current alea settings with
    /// the calibration area.
    /// </summary>
    private void SetCalibrationArea()
    {
      if (this.rdbAleaAreaFull.Checked)
      {
        this.aleaSettings.CalibArea = 0; 
      }
      else if (this.rdbAleaAreaCenter.Checked)
      {
        this.aleaSettings.CalibArea = 1;
      }
      else if (this.rdbAleaAreaBottom.Checked)
      {
        this.aleaSettings.CalibArea = 2;
      }
    }

    /// <summary>
    /// Updates the current alea settings with
    /// the calibration eye.
    /// </summary>
    private void SetEye()
    {
      if (this.rdbAleaEyeBoth.Checked)
      {
        this.aleaSettings.Eye = 0;
      }
      else if (this.rdbAleaEyeLeft.Checked)
      {
        this.aleaSettings.Eye = 1;
      }
      else if (this.rdbAleaEyeRight.Checked)
      {
        this.aleaSettings.Eye = 2;
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