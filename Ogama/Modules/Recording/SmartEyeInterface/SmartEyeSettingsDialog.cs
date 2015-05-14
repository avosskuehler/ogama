// <copyright file="SmartEyeSettingsDialog.cs" company="Smart Eye AB">
// ******************************************************
// OGAMA - open gaze and mouse analyzer 
// Copyright (C) 2015 Dr. Adrian Voßkühler  
// ------------------------------------------------------------------------
// This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
// This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
// You should have received a copy of the GNU General Public License along with this program; if not, write to the Free Software Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
// **************************************************************
// </copyright>
// <author>Kathrin Scheil</author>
// <email>kathrin.scheil@smarteye.se</email>

namespace Ogama.Modules.Recording.SmartEyeInterface
{
  using System;
  using System.ComponentModel;
  using System.Globalization;
  using System.Linq;
  using System.Net;
  using System.Net.Sockets;
  using System.Windows.Forms;
  using Ogama.ExceptionHandling;

  /// <summary>
  /// Popup form to specify settings for the Smart Eye tracker.
  /// </summary>
  public partial class SmartEyeSettingsDialog : Form
  {
    ///////////////////////////////////////////////////////////////////////////////
    // Defining Variables, Enumerations, Events                                  //
    ///////////////////////////////////////////////////////////////////////////////
    #region FIELDS

    /// <summary>
    /// Saves the current <see cref="SmartEyeSetting"/>.
    /// </summary>
    private SmartEyeSetting smartEyeSettings;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the SmartEyeSettingsDialog class.
    /// </summary>
    public SmartEyeSettingsDialog()
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
    /// Gets or sets the <see cref="SmartEyeSetting"/> class.
    /// </summary>
    /// <value>A <see cref="SmartEyeSetting"/> with the current settings.</value>
    public SmartEyeSetting SmartEyeSettings
    {
      get
      {
        return this.smartEyeSettings;
      }

      set
      {
        this.smartEyeSettings = value;
        this.SetupUIWithNewSettings(this.smartEyeSettings);
      }
    }

    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Methods for doing main class job                                          //
    ///////////////////////////////////////////////////////////////////////////////
    #region METHODS

    /// <summary>
    /// This method updates the SmartEyeSetting out of the
    /// options chosen in the UI.
    /// </summary>
    public void UpdateSmartEyeSettings()
    {
      this.SmartEyeSettings.SmartEyeServerAddress = txbSmartEyeAddress.Text;
      this.SmartEyeSettings.SmartEyeRPCPort = Convert.ToInt32(txbSmartEyePort.Text);
      this.SmartEyeSettings.OgamaPort = Convert.ToInt32(txbOGAMAPort.Text);
      this.SmartEyeSettings.CalibPointColor = clbSmartEyePointColor.CurrentColor;
      this.SmartEyeSettings.CalibBackgroundColor = clbSmartEyeBackColor.CurrentColor;
      this.SmartEyeSettings.RandomizeCalibPointOrder = chbRandomizePointOrder.Checked;

      this.SetNumberOfCalibrationPoints();
      this.SetSizeOfCalibrationPoints();
      this.SetSpeedOfCalibrationPoints();
    }

    /// <summary>
    /// Updates the forms UI with the new settings from the 
    /// <see cref="SmartEyeSetting"/> member.
    /// </summary>
    /// <param name="setting">A <see cref="SmartEyeSetting"/> with the settings
    /// to apply.</param>
    private void SetupUIWithNewSettings(SmartEyeSetting setting)
    {
      switch (this.SmartEyeSettings.CalibPointSize)
      {
        case 44:
          rdbSmartEyeSizeLarge.Checked = true;
          break;
        case 22:
          rdbSmartEyeSizeMedium.Checked = true;
          break;
        case 11:
          rdbSmartEyeSizeSmall.Checked = true;
          break;
        default:
          rdbSmartEyeSizeMedium.Checked = true;
          break;
      }

      switch (this.SmartEyeSettings.CalibPointSpeed)
      {
        case 3:
          rdbSmartEyeSpeedFast.Checked = true;
          break;
        case 2:
          rdbSmartEyeSpeedMedium.Checked = true;
          break;
        case 1:
          rdbSmartEyeSpeedSlow.Checked = true;
          break;
        default:
          rdbSmartEyeSpeedMedium.Checked = true;
          break;
      }

      switch (this.SmartEyeSettings.NumCalibPoints)
      {
        case 3:
          rdbSmartEye3PtsCalib.Checked = true;
          break;
        case 5:
          rdbSmartEye5PtsCalib.Checked = true;
          break;
        case 9:
          rdbSmartEye9PtsCalib.Checked = true;
          break;
        default:
          rdbSmartEye5PtsCalib.Checked = true;
          break;
      }

      txbSmartEyeAddress.Text = setting.SmartEyeServerAddress;
      txbSmartEyePort.Text = setting.SmartEyeRPCPort.ToString();
      txbOGAMAPort.Text = setting.OgamaPort.ToString();
      clbSmartEyeBackColor.CurrentColor = this.SmartEyeSettings.CalibBackgroundColor;
      clbSmartEyePointColor.CurrentColor = this.SmartEyeSettings.CalibPointColor;
      chbRandomizePointOrder.Checked = this.SmartEyeSettings.RandomizeCalibPointOrder;
    }

    /// <summary>
    /// Reads the IP address of the computer on which OGAMA is running
    /// </summary>
    private void SetOGAMAServerIP()
    {
      // Getting Ip address of local machine...
      // First get the host name of local machine.
      var strHostName = Dns.GetHostName();

      // Then using host name, get the IP address list..
      var addr = Dns.GetHostAddresses(strHostName);

      if (addr.Length > 0)
      {
        var add = addr.First(ip => ip.AddressFamily == AddressFamily.InterNetwork);
        txbOGAMAAddress.Text = add.ToString();
      }
      else
      {
        txbOGAMAAddress.Text = "No local IP found";
      }
    }

    /// <summary>
    /// Updates the current SmartEye settings with
    /// the number of points to use when calibrating.
    /// </summary>
    private void SetNumberOfCalibrationPoints()
    {
      if (rdbSmartEye9PtsCalib.Checked)
      {
        this.SmartEyeSettings.NumCalibPoints = 9;
      }
      else if (rdbSmartEye5PtsCalib.Checked)
      {
        this.SmartEyeSettings.NumCalibPoints = 5;
      }
      else if (rdbSmartEye3PtsCalib.Checked)
      {
        this.SmartEyeSettings.NumCalibPoints = 3;
      }
    }

    /// <summary>
    /// Updates the current SmartEye settings with
    /// the number of points to use during calibration.
    /// </summary>
    private void SetSizeOfCalibrationPoints()
    {
      if (rdbSmartEyeSizeLarge.Checked)
      {
        this.SmartEyeSettings.CalibPointSize = 44;
      }
      else if (rdbSmartEyeSizeMedium.Checked)
      {
        this.SmartEyeSettings.CalibPointSize = 22;
      }
      else if (rdbSmartEyeSizeSmall.Checked)
      {
        this.SmartEyeSettings.CalibPointSize = 11;
      }
    }

    /// <summary>
    /// Updates the current SmartEye settings with
    /// the speed of the calibration during calibration.
    /// </summary>
    private void SetSpeedOfCalibrationPoints()
    {
      if (rdbSmartEyeSpeedFast.Checked)
      {
        this.SmartEyeSettings.CalibPointSpeed = 3;
      }
      else if (rdbSmartEyeSpeedMedium.Checked)
      {
        this.SmartEyeSettings.CalibPointSpeed = 2;
      }
      else if (rdbSmartEyeSpeedSlow.Checked)
      {
        this.SmartEyeSettings.CalibPointSpeed = 1;
      }
    }

    /// <summary>
    /// Validates the Smart Eye IP Address.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The event args.</param>
    private void TxbSmartEyeAddress_Validating(object sender, CancelEventArgs e)
    {
      var str = this.txbSmartEyeAddress.Text;
      if (string.IsNullOrEmpty(str))
      {
        e.Cancel = true;
        ExceptionMethods.ProcessErrorMessage("Please enter a valid Smart Eye IP Address.");
        return;
      }

      var parts = str.Split('.');
      if (parts.Length != 4)
      {
        e.Cancel = true;
        ExceptionMethods.ProcessErrorMessage("Please enter a valid Smart Eye IP Address comprising four octets, seperated by decimals.");
        return;
      }

      foreach (var p in parts)
      {
        int intPart;
        if (!int.TryParse(p, NumberStyles.Integer, CultureInfo.CurrentCulture.NumberFormat, out intPart))
        {
          e.Cancel = true;
          ExceptionMethods.ProcessErrorMessage("Please enter a valid Smart Eye IP Address - each octet of an IP Address should be a number.");
          return;
        }

        if (intPart < 0 || intPart > 255)
        {
          e.Cancel = true;
          ExceptionMethods.ProcessErrorMessage("Please enter a valid Smart Eye IP Address - each octet of an IP Address should be between 0 and 255.");
          return;
        }
      }
    }

    /// <summary>
    /// Validates the Smart Eye Port Address.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The event args.</param>
    private void TxbSmartEyePort_Validating(object sender, CancelEventArgs e)
    {
      int intPart;
      if (!int.TryParse(this.txbSmartEyePort.Text, NumberStyles.Integer, CultureInfo.CurrentCulture.NumberFormat, out intPart))
      {
        e.Cancel = true;
        ExceptionMethods.ProcessErrorMessage("Please enter a numerical value for the Smart Eye Port.");
        return;
      }

      if (intPart < 0 || intPart > 65535)
      {
        e.Cancel = true;
        ExceptionMethods.ProcessErrorMessage("Please enter a numerical value for the Smart Eye Port between 0 and 65535.");
        return;
      }
    }

    /// <summary>
    /// Validates the OGAMA Port Address.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The event args.</param>
    private void TxbOGAMAPort_Validating(object sender, CancelEventArgs e)
    {
      int intPart;
      if (!int.TryParse(this.txbOGAMAPort.Text, NumberStyles.Integer, CultureInfo.CurrentCulture.NumberFormat, out intPart))
      {
        e.Cancel = true;
        ExceptionMethods.ProcessErrorMessage("Please enter a numerical value for the OGAMA Port.");
        return;
      }

      if (intPart < 0 || intPart > 65535)
      {
        e.Cancel = true;
        ExceptionMethods.ProcessErrorMessage("Please enter a numerical value for the OGAMA Port between 0 and 65535.");
        return;
      }
    }

    /// <summary>
    /// Validate all connection settings before closing form.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The event args.</param>
    private void BtnOK_Click(object sender, EventArgs e)
    {
      CancelEventArgs args = new CancelEventArgs();

      this.TxbSmartEyeAddress_Validating(sender, args);
      this.TxbSmartEyePort_Validating(sender, args);
      this.TxbOGAMAPort_Validating(sender, args);

      if (args.Cancel)
      {
        this.DialogResult = DialogResult.None;
      }
    }

    #endregion //METHODS
  }
}