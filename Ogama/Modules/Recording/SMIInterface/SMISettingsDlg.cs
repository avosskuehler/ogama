// <copyright file="SMISettingsDlg.cs" company="FU Berlin">
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

namespace Ogama.Modules.Recording.SMIInterface
{
  using System;
  using System.Net;
  using System.Windows.Forms;

  /// <summary>
  /// Popup form to specify settings for the SMI iViewX system.
  /// </summary>
  public partial class SMISettingsDlg : Form
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
    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the SMISettingsDlg class.
    /// </summary>
    public SMISettingsDlg()
    {
      this.InitializeComponent();
      this.SetOGAMAServerIP();
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets or sets the <see cref="SMISetting"/> class.
    /// </summary>
    /// <value>A <see cref="SMISetting"/> with the current settings.</value>
    public SMISetting SMISettings
    {
      get
      {
        return this.GenerateSMISettings();
      }

      set
      {
        this.SetupUIWithNewSettings(value);
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
    /// The <see cref="Control.KeyDown"/> event handler for
    /// the port <see cref="TextBox"/>es
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">The <see cref="KeyEventArgs"/> with the event data.</param>
    private void txbPort_KeyDown(object sender, KeyEventArgs e)
    {
      if (this.CheckIfNumericKey(e.KeyCode))
      {
        e.SuppressKeyPress = true;
      }
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
    /// Reads the ip address of the computer on which OGAMA is running
    /// </summary>
    private void SetOGAMAServerIP()
    {
      // Getting Ip address of local machine...
      // First get the host name of local machine.
      string strHostName = Dns.GetHostName();

      // Then using host name, get the IP address list..
      IPAddress[] addr = Dns.GetHostAddresses(strHostName);

      if (addr.Length > 0)
      {
        this.txbOGAMAAddress.Text = addr[0].ToString();
      }
      else
      {
        this.txbOGAMAAddress.Text = "No local IP found";
      }
    }

    /// <summary>
    /// This method generates a new SMISetting out of the
    /// options choosen in the UI.
    /// </summary>
    /// <returns>A <see cref="SMISetting"/> containing the values of the
    /// dialog.</returns>
    private SMISetting GenerateSMISettings()
    {
      SMISetting newSetting = new SMISetting();
      newSetting.SMIServerAddress = this.txbiViewXAddress.Text;
      newSetting.SMIServerPort = Convert.ToInt32(this.txbiViewXPort.Text);
      newSetting.OGAMAServerPort = Convert.ToInt32(this.txbOGAMAPort.Text);
      newSetting.CalibPointColor = this.clbSMIPointColor.CurrentColor;
      newSetting.CalibBackgroundColor = this.clbSMIBackColor.CurrentColor;

      if (this.rdbSMISizeLarge.Checked)
      {
        newSetting.CalibPointSize = CalibrationPointSize.Large;
      }
      else if (this.rdbSMISizeMedium.Checked)
      {
        newSetting.CalibPointSize = CalibrationPointSize.Medium;
      }
      else if (this.rdbSMISizeSmall.Checked)
      {
        newSetting.CalibPointSize = CalibrationPointSize.Small;
      }

      if (this.rdbSMIEyeBoth.Checked)
      {
        newSetting.AvailableEye = AvailableEye.Both;
      }
      else if (this.rdbSMIEyeLeft.Checked)
      {
        newSetting.AvailableEye = AvailableEye.Left;
      }
      else if (this.rdbSMIEyeRight.Checked)
      {
        newSetting.AvailableEye = AvailableEye.Right;
      }

      return newSetting;
    }

    /// <summary>
    /// Updates the forms UI with the new settings from the 
    /// <see cref="SMISetting"/> member.
    /// </summary>
    /// <param name="setting">A <see cref="SMISetting"/> with the settings
    /// to apply.</param>
    private void SetupUIWithNewSettings(SMISetting setting)
    {
      switch (setting.CalibPointSize)
      {
        case CalibrationPointSize.Small:
          this.rdbSMISizeSmall.Checked = true;
          break;
        default:
        case CalibrationPointSize.Medium:
          this.rdbSMISizeMedium.Checked = true;
          break;
        case CalibrationPointSize.Large:
          this.rdbSMISizeLarge.Checked = true;
          break;
      }

      this.txbiViewXAddress.Text = setting.SMIServerAddress;
      this.txbiViewXPort.Text = setting.SMIServerPort.ToString();
      this.txbOGAMAPort.Text = setting.OGAMAServerPort.ToString();
      this.clbSMIBackColor.CurrentColor = setting.CalibBackgroundColor;
      this.clbSMIPointColor.CurrentColor = setting.CalibPointColor;
    }

    #endregion //METHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER

    /// <summary>
    /// Check if key entered is "numeric".
    /// </summary>
    /// <param name="key">A <see cref="Keys"/> to check.</param>
    /// <returns><strong>True</strong> if given key is numeric or backspace,
    /// otherwise <strong>false</strong></returns>
    private bool CheckIfNumericKey(Keys key)
    {
      if (key == Keys.Back)
      {
        // backspace?        
        return true;
      }
      else if ((key >= Keys.D0) && (key <= Keys.D9))
      {
        // digit from top of keyboard?        
        return true;
      }
      else if ((key >= Keys.NumPad0) && (key <= Keys.NumPad9))
      {
        // digit from keypad?        
        return true;
      }
      else
      {
        // no "numeric" key
        return false;
      }
    }

    #endregion //HELPER
  }
}