// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TheEyeTribeSettingsDialog.cs" company="Freie Universität Berlin">
//   OGAMA - open gaze and mouse analyzer 
//   Copyright (C) 2014 Dr. Adrian Voßkühler  
//   Licensed under GPL V3
// </copyright>
// <author>Adrian Voßkühler</author>
// <email>adrian@ogama.net</email>
// <summary>
//   Popup form to specify settings for the eye tribe system.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Ogama.Modules.Recording.TheEyeTribeInterface
{
  using System.Globalization;
  using System.Windows.Forms;

  using Ogama.Modules.Recording.SMIInterface;

  /// <summary>
  ///   Popup form to specify settings for the eye tribe system.
  /// </summary>
  public partial class TheEyeTribeSettingsDialog : Form
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the TheEyeTribeSettingsDialog class.
    /// </summary>
    public TheEyeTribeSettingsDialog()
    {
      this.InitializeComponent();
    }

    #endregion

    #region Public Properties

    /// <summary>
    ///   Gets or sets the <see cref="TheEyeTribeSetting" /> class.
    /// </summary>
    /// <value>A <see cref="TheEyeTribeSetting" /> with the current settings.</value>
    public TheEyeTribeSetting TheEyeTribeSettings
    {
      get
      {
        return this.PullTheEyeTribeSettings();
      }

      set
      {
        this.PushTheEyeTribeSettings(value);
      }
    }

    #endregion

    #region Methods

    /// <summary>
    ///   Pulls the eye tribe settings.
    /// </summary>
    /// <returns>The settings modified by this control</returns>
    private TheEyeTribeSetting PullTheEyeTribeSettings()
    {
      var newSetting = new TheEyeTribeSetting();
      if (this.rdb9PtsCalib.Checked)
      {
        newSetting.CalibrationPointCount = TheEyeTribeCalibrationPoints.Nine;
      }
      else if (this.rdb12PtsCalib.Checked)
      {
        newSetting.CalibrationPointCount = TheEyeTribeCalibrationPoints.Twelve;
      }
      else if (this.rdb16PtsCalib.Checked)
      {
        newSetting.CalibrationPointCount = TheEyeTribeCalibrationPoints.Sixteen;
      }

      newSetting.PointDisplayTime = (int)this.nudDisplayTime.Value;

      newSetting.ServerIPAddress = this.txbServerAddress.Text;
      newSetting.ServerPort = int.Parse(this.txbPort.Text);
      newSetting.CalibrationPointColor = this.clbPointColor.CurrentColor;
      newSetting.DisplayHelp = this.chbDisplayHelp.Checked;
      newSetting.CalibrationBackgroundColor = this.clbBackColor.CurrentColor;
      newSetting.DeviceIndex = this.cbbDeviceIndex.SelectedIndex;
      newSetting.Framerate = this.cbbFramerate.SelectedIndex == 0 ? 30 : 60;

      return newSetting;
    }

    /// <summary>
    /// Updates the forms UI with the new settings from the
    ///   <see cref="TheEyeTribeSetting"/> member.
    /// </summary>
    /// <param name="setting">
    /// The setting to be shown by this control
    /// </param>
    private void PushTheEyeTribeSettings(TheEyeTribeSetting setting)
    {
      switch (setting.CalibrationPointCount)
      {
        case TheEyeTribeCalibrationPoints.Nine:
          this.rdb9PtsCalib.Checked = true;
          break;
        case TheEyeTribeCalibrationPoints.Twelve:
          this.rdb12PtsCalib.Checked = true;
          break;
        case TheEyeTribeCalibrationPoints.Sixteen:
          this.rdb16PtsCalib.Checked = true;
          break;
        default:
          this.rdb9PtsCalib.Checked = true;
          break;
      }

      this.nudDisplayTime.Value = setting.PointDisplayTime;

      this.txbServerAddress.Text = setting.ServerIPAddress;
      this.txbPort.Text = setting.ServerPort.ToString(CultureInfo.InvariantCulture);
      this.clbPointColor.CurrentColor = setting.CalibrationPointColor;
      this.chbDisplayHelp.Checked = setting.DisplayHelp;
      this.clbBackColor.CurrentColor = setting.CalibrationBackgroundColor;
      this.cbbDeviceIndex.SelectedIndex = setting.DeviceIndex;
      this.cbbFramerate.SelectedIndex = setting.Framerate == 30 ? 0 : 1;
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
      if (SMISettingsDlg.CheckIfNumericKey(e.KeyCode))
      {
        e.SuppressKeyPress = true;
      }
    }

    #endregion
  }
}