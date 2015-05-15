// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HowToActivateHaytham.cs" company="Freie Universität Berlin">
//   OGAMA - open gaze and mouse analyzer 
//   Copyright (C) 2015 Dr. Adrian Voßkühler  
//   Licensed under GPL V3
// </copyright>
// <author>Adrian Voßkühler</author>
// <email>adrian@ogama.net</email>
// <summary>
//   A small popup <see cref="Form" /> for showing a dialog on how to
//   install the ITU Gazetracker recording option.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Ogama.Modules.Recording.HaythamInterface
{
  using System;
  using System.Windows.Forms;

  /// <summary>
  ///   A small popup <see cref="Form" /> for showing a dialog on how to
  ///   install the ITU Haytham recording option.
  /// </summary>
  public partial class HowToActivateHaytham : Form
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="HowToActivateHaytham" /> class.
    /// </summary>
    public HowToActivateHaytham()
    {
      this.InitializeComponent();
    }

    #endregion

    #region Methods

    /// <summary>
    /// User clicked to open the Haytham Website.
    /// </summary>
    /// <param name="sender">
    /// Source of the event.
    /// </param>
    /// <param name="e">
    /// A <see cref="LinkLabelLinkClickedEventArgs"/> with the event data.
    /// </param>
    private void LlbItuWebsiteLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      System.Diagnostics.Process.Start("http://www.itu.dk/research/eye/");
    }

    /// <summary>
    /// User clicked the Haytham logo, so open their website.
    /// </summary>
    /// <param name="sender">
    /// Source of the event.
    /// </param>
    /// <param name="e">
    /// An empty <see cref="EventArgs"/>
    /// </param>
    private void PcbItuLogoClick(object sender, EventArgs e)
    {
      System.Diagnostics.Process.Start("http://www.itu.dk/research/eye/");
    }

    #endregion
  }
}