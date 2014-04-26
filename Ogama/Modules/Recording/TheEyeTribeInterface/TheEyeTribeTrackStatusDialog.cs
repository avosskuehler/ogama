// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TheEyeTribeTrackStatusDialog.cs" company="Freie Universität Berlin">
//   OGAMA - open gaze and mouse analyzer 
//   Copyright (C) 2014 Dr. Adrian Voßkühler  
//   Licensed under GPL V3
// </copyright>
// <author>Adrian Voßkühler</author>
// <email>adrian@ogama.net</email>
// <summary>
//   A dialog which exposes a track status meter to the subject.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Ogama.Modules.Recording.TheEyeTribeInterface
{
  using System.Windows.Forms;

  /// <summary>
  ///   A dialog which exposes a track status meter to the subject.
  /// </summary>
  public partial class TheEyeTribeTrackStatusDialog : Form
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="TheEyeTribeTrackStatusDialog" /> class.
    /// </summary>
    public TheEyeTribeTrackStatusDialog()
    {
      this.InitializeComponent();
      this.trackBoxStatus.Connect();
    }


    #endregion

    /// <summary>
    /// Disconnects the eye tribe track status when dialog form is closing.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="FormClosingEventArgs"/> instance containing the event data.</param>
    private void TheEyeTribeTrackStatusDialogFormClosing(object sender, FormClosingEventArgs e)
    {
      this.trackBoxStatus.Disconnect();
    }
  }
}