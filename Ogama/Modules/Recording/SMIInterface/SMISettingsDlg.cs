// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SMISettingsDlg.cs" company="Freie Universität Berlin">
//   OGAMA - open gaze and mouse analyzer 
//   Copyright (C) 2015 Dr. Adrian Voßkühler  
//   Licensed under GPL V3
// </copyright>
// <author>Adrian Voßkühler</author>
// <email>adrian@ogama.net</email>
// <summary>
//   Popup form to specify settings for the SMI iViewX system.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Ogama.Modules.Recording.SMIInterface
{
  using System;
  using System.Linq;
  using System.Net;
  using System.Net.Sockets;
  using System.Windows.Forms;

  /// <summary>
  ///   Popup form to specify settings for the SMI iViewX system.
  /// </summary>
  public partial class SMISettingsDlg : Form
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the SMISettingsDlg class.
    /// </summary>
    public SMISettingsDlg()
    {
      this.InitializeComponent();
      this.SetOgamaServerIP();
    }

    #endregion

    #region Public Properties

    /// <summary>
    ///   Gets or sets the <see cref="SMISetting" /> class.
    /// </summary>
    /// <value>A <see cref="SMISetting" /> with the current settings.</value>
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

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// Check if key entered is "numeric".
    /// </summary>
    /// <param name="key">
    /// A <see cref="Keys"/> to check.
    /// </param>
    /// <returns>
    /// <strong>True</strong> if given key is numeric or backspace,
    ///   otherwise <strong>false</strong>
    /// </returns>
    public static bool CheckIfNumericKey(Keys key)
    {
      if (key == Keys.Back)
      {
        // backspace?        
        return true;
      }

      if ((key >= Keys.D0) && (key <= Keys.D9))
      {
        // digit from top of keyboard?        
        return true;
      }

      if ((key >= Keys.NumPad0) && (key <= Keys.NumPad9))
      {
        // digit from keypad?        
        return true;
      }

      // no "numeric" key
      return false;
    }

    #endregion

    #region Methods

    /// <summary>
    ///   This method generates a new SMISetting out of the
    ///   options choosen in the UI.
    /// </summary>
    /// <returns>
    ///   A <see cref="SMISetting" /> containing the values of the
    ///   dialog.
    /// </returns>
    private SMISetting GenerateSMISettings()
    {
      var newSetting = new SMISetting
                         {
                           SMIServerAddress = this.txbiViewXAddress.Text,
                           SMIServerPort = Convert.ToInt32(this.txbiViewXPort.Text),
                           OGAMAServerPort = Convert.ToInt32(this.txbOGAMAPort.Text),
                           CalibPointColor = this.clbSMIPointColor.CurrentColor,
                           CalibBackgroundColor = this.clbSMIBackColor.CurrentColor
                         };

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
    ///   Reads the ip address of the computer on which OGAMA is running
    /// </summary>
    private void SetOgamaServerIP()
    {
      // Getting Ip address of local machine...
      // First get the host name of local machine.
      string strHostName = Dns.GetHostName();

      // Then using host name, get the IP address list..
      IPAddress[] addr = Dns.GetHostAddresses(strHostName);

      if (addr.Length > 0)
      {
        IPAddress add = addr.First(ip => ip.AddressFamily == AddressFamily.InterNetwork);
        this.txbOGAMAAddress.Text = add.ToString();
      }
      else
      {
        this.txbOGAMAAddress.Text = "No local IP found";
      }
    }

    /// <summary>
    /// Updates the forms UI with the new settings from the
    ///   <see cref="SMISetting"/> member.
    /// </summary>
    /// <param name="setting">
    /// A <see cref="SMISetting"/> with the settings
    ///   to apply.
    /// </param>
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

    /// <summary>
    /// The <see cref="Control.KeyDown"/> event handler for
    ///   the port <see cref="TextBox"/>es
    /// </summary>
    /// <param name="sender">
    /// Source of the event.
    /// </param>
    /// <param name="e">
    /// The <see cref="KeyEventArgs"/> with the event data.
    /// </param>
    private void TxbPortKeyDown(object sender, KeyEventArgs e)
    {
      if (CheckIfNumericKey(e.KeyCode))
      {
        e.SuppressKeyPress = true;
      }
    }

    #endregion
  }
}