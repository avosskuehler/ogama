// <copyright file="SaveVideoSplash.cs" company="FU Berlin">
// ******************************************************
// OGAMA - open gaze and mouse analyzer 
// Copyright (C) 2013 Dr. Adrian Voßkühler  
// ------------------------------------------------------------------------
// This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
// This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
// You should have received a copy of the GNU General Public License along with this program; if not, write to the Free Software Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
// **************************************************************
// </copyright>
// <author>Adrian Voßkühler</author>
// <email>adrian@ogama.net</email>

namespace Ogama.Modules.Replay.Video
{
  using System;
  using System.Windows.Forms;

  using Ogama.Modules.Common.FormTemplates;

  /// <summary>
  /// Splash <see cref="Form"/> with animated gif and marquee progress bar.
  /// Is invoked from <see cref="ReplayModule"/>, when video is beeing exported.
  /// Informs the user, that export is going on.
  /// </summary>
  public partial class SaveVideoSplash : FormWithAccellerators
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
    /// Saves the height of the form with visible video preview
    /// </summary>
    private int heightWithPreview;

    /// <summary>
    /// Saves the height of the form with hidden video preview
    /// </summary>
    private int heightWithoutPreview;

    /// <summary>
    /// Saves the parent <see cref="ReplayModule"/> which handles the ESC button.
    /// </summary>
    private ReplayModule replayModule;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the SaveVideoSplash class.
    /// </summary>
    /// <param name="parent">The <see cref="ReplayModule"/> that creates this form.</param>
    public SaveVideoSplash(ReplayModule parent)
    {
      this.replayModule = parent;
      this.InitializeComponent();
      this.InitAccelerators();
      this.heightWithPreview = this.Height;
      this.heightWithoutPreview = this.Height - this.previewPanel.Height;
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining events, enums, delegates                                         //
    ///////////////////////////////////////////////////////////////////////////////
    #region EVENTS

    /// <summary>
    /// A delegate to handle cross thread calls to the progressbar.
    /// </summary>
    /// <param name="newValue">An <see cref="Int32"/> with the new progress
    /// bar value in percent.</param>
    private delegate void SetIntCallback(int newValue);

    #endregion EVENTS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets the <see cref="Control"/> to the panel in which 
    /// the video preview should be shown.
    /// </summary>
    public Control PreviewWindow
    {
      get { return this.previewPanel; }
    }

    /// <summary>
    /// Sets the <see cref="String"/> header
    /// of the top dialog
    /// </summary>
    public string Header
    {
      set { this.dialogTop.Description = value; }
    }

    /// <summary>
    /// Sets a value indicating whether the video preview window
    /// of this form is visible or not.
    /// </summary>
    public bool IsPreviewVisible
    {
      set
      {
        if (value)
        {
          this.spcPreviewSplash.Panel1Collapsed = false;
          this.Height = this.heightWithPreview;
        }
        else
        {
          this.spcPreviewSplash.Panel1Collapsed = true;
          this.Height = this.heightWithoutPreview;
        }
      }
    }

    /// <summary>
    /// Sets the percentage of the progress bar.
    /// </summary>
    public int ProgressPercentage
    {
      set
      {
        this.SetPercentage(value);
      }
    }

    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Public methods                                                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region PUBLICMETHODS
    #endregion //PUBLICMETHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Inherited methods                                                         //
    ///////////////////////////////////////////////////////////////////////////////
    #region OVERRIDES

    /// <summary>
    /// Overridden. Initializes accelerator keys: ESC.
    /// </summary>
    protected override void InitAccelerators()
    {
      base.InitAccelerators();
      this.SetAccelerator(Keys.Escape, new AcceleratorAction(this.OnEscape));
    }

    #endregion //OVERRIDES

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler                                                              //
    ///////////////////////////////////////////////////////////////////////////////
    #region EVENTHANDLER

    /// <summary>
    /// The <see cref="Form.FormClosing"/> event handler which
    /// cancels the video export if user clicked on the Close button.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A <see cref="FormClosingEventArgs"/> with the event params.</param>
    private void SaveVideoSplash_FormClosing(object sender, FormClosingEventArgs e)
    {
      if (e.CloseReason == CloseReason.UserClosing)
      {
        this.OnEscape();
      }
    }

    #endregion //EVENTHANDLER

    ///////////////////////////////////////////////////////////////////////////////
    // Methods and Eventhandling for Background tasks                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region THREAD
    #endregion //THREAD

    ///////////////////////////////////////////////////////////////////////////////
    // Methods for doing main class job                                          //
    ///////////////////////////////////////////////////////////////////////////////
    #region PRIVATEMETHODS

    /// <summary>
    /// Thread safe version of this.progressBar.Value = percentage;
    /// </summary>
    /// <param name="percentage">An <see cref="Int32"/> with the new progress
    /// bar value in percent.</param>
    private void SetPercentage(int percentage)
    {
      // InvokeRequired required compares the thread ID of the
      // calling thread to the thread ID of the creating thread.
      // If these threads are different, it returns true.
      if (this.progressBar.InvokeRequired)
      {
        SetIntCallback d = new SetIntCallback(this.SetPercentage);
        this.progressBar.BeginInvoke(d, new object[] { percentage });
      }
      else
      {
        this.progressBar.Value = percentage;
      }
    }

    /// <summary>
    /// Handles the ESC button, calls the abort method
    /// in the replay module.
    /// </summary>
    private void OnEscape()
    {
      if (this.replayModule != null)
      {
        this.replayModule.OnEscape();
      }
    }

    #endregion //PRIVATEMETHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER
    #endregion //HELPER
  }
}