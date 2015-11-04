// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HaythamSettingsDialog.cs" company="Freie Universität Berlin">
//   OGAMA - open gaze and mouse analyzer 
//   Copyright (C) 2015 Dr. Adrian Voßkühler  
//   Licensed under GPL V3
// </copyright>
// <author>Adrian Voßkühler</author>
// <email>adrian@ogama.net</email>
// <summary>
//   Popup form to specify settings for the Haytham system which uses
//   a tcp connection.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Ogama.Modules.Recording.HaythamInterface
{
  using System;
  using System.Windows.Forms;

  /// <summary>
  ///   Popup form to specify settings for the Haytham system which uses
  ///   a tcp connection
  /// </summary>
  public partial class HaythamSettingsDialog : Form
  {
    #region Fields

    /// <summary>
    ///   Saves the current <see cref="HaythamSetting" />.
    /// </summary>
    private HaythamSetting haythamSetting;

    #endregion

    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the HaythamSettingsDialog class.
    /// </summary>
    public HaythamSettingsDialog()
    {
      this.InitializeComponent();
    }

    #endregion

    #region Public Properties

    /// <summary>
    ///   Gets or sets the <see cref="HaythamSetting" /> class.
    /// </summary>
    /// <value>A <see cref="HaythamSetting" /> with the current settings.</value>
    public HaythamSetting HaythamSetting
    {
      get
      {
        return this.haythamSetting;
      }

      set
      {
        this.haythamSetting = value;
        this.RealizeNewSettings();
      }
    }

    #endregion

    #region Methods

    /// <summary>
    ///   Updates the forms UI with the new settings from the
    ///   <see cref="HaythamSetting" /> member.
    /// </summary>
    private void RealizeNewSettings()
    {
      this.txbServerAddress.Text = this.haythamSetting.HaythamServerIPAddress;
    }

    /// <summary>
    /// The <see cref="Control.TextChanged"/> event handler for
    ///   the <see cref="TextBox"/> <see cref="txbServerAddress"/>.
    ///   Updates current haytham settings with the new server address.
    /// </summary>
    /// <param name="sender">
    /// Source of the event.
    /// </param>
    /// <param name="e">
    /// An empty <see cref="EventArgs"/>
    /// </param>
    private void TxbServerAddressTextChanged(object sender, EventArgs e)
    {
      this.haythamSetting.HaythamServerIPAddress = this.txbServerAddress.Text;
    }

    #endregion
  }
}