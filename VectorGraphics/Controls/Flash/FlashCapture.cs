// <copyright file="FlashCapture.cs" company="FU Berlin">
// ******************************************************
// OGAMA - open gaze and mouse analyzer 
// Copyright (C) 2015 Dr. Adrian Voßkühler  
// ------------------------------------------------------------------------
// This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
// This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
// You should have received a copy of the GNU General Public License along with this program; if not, write to the Free Software Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
// **************************************************************
// </copyright>
// <author>Adrian Voßkühler</author>
// <email>adrian@ogama.net</email>

namespace VectorGraphics.Controls.Flash
{
  using System;
  using System.Drawing;
  using System.Windows.Forms;

  using VectorGraphics.Tools.Interfaces;

  /// <summary>
  /// A <see cref="Form"/> with a thumb sized flash object.
  /// </summary>
  /// <remarks>Used in Thumbs thumbs creation.
  /// It then should load a flash movie, make a screenshot and closes itself.
  /// <para></para>
  /// How to use:
  /// <example>
  /// FlashCapture dlg = new FlashCapture(thumbSize);
  /// dlg.Show();
  /// dlg.flashObject.LoadMovie(0, filename);
  /// do
  /// {
  ///   Application.DoEvents();
  /// } 
  /// while (dlg.flashObject.ReadyState != 4);
  /// Image thumb = ScreenCapture.GetWindowImage(dlg.flashObject.Handle, dlg.flashObject.Size);
  /// thumb.Tag = filename;
  /// dlg.Close();
  /// return thumb;
  /// </example>
  /// </remarks>
  public partial class FlashCapture : Form
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
    /// Shockwave flash object on the form.
    /// </summary>
    private AxFlashControl flashObject;

    /// <summary>
    /// Saves the filename with path to the loaded movie.
    /// </summary>
    private string movieFile;

    /// <summary>
    /// Saves the screenshot of the flash movie.
    /// </summary>
    private Image screenShot;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the FlashCapture class.
    /// </summary>
    public FlashCapture()
    {
      this.InitializeComponent();
    }

    /// <summary>
    /// Initializes a new instance of the FlashCapture class.
    /// </summary>
    /// <param name="formSize">A <see cref="Size"/> with the new size for this dialog.</param>
    /// <param name="filename">A <see cref="string"/> with the movie file to load (including path).</param>
    public FlashCapture(Size formSize, string filename)
    {
      this.InitializeComponent();
      this.movieFile = filename;
      this.Size = formSize;
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FlashCapture));
      this.flashObject = new AxFlashControl();
      ((System.ComponentModel.ISupportInitialize)this.flashObject).BeginInit();
      this.flashObject.Dock = System.Windows.Forms.DockStyle.Fill;
      this.flashObject.Enabled = true;
      this.flashObject.Location = new System.Drawing.Point(0, 0);
      this.flashObject.Name = "flashObject";
      this.flashObject.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("flashObject.OcxState");
      this.flashObject.Size = new System.Drawing.Size(300, 200);
      this.flashObject.TabIndex = 0;
      ((System.ComponentModel.ISupportInitialize)this.flashObject).EndInit();
      this.Controls.Add(this.flashObject);

      this.Location = new Point(Screen.PrimaryScreen.WorkingArea.Width, 0);
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets the Shockwave flash object on the form.
    /// </summary>
    public AxFlashControl FlashObject
    {
      get { return this.flashObject; }
    }

    /// <summary>
    /// Gets the screenshot of the flash movie
    /// </summary>
    public Image ScreenShot
    {
      get { return this.screenShot; }
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
    /// The <see cref="Form.Load"/> event handler. 
    /// Loads the movie into the com object.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void FlashCapture_Load(object sender, EventArgs e)
    {
      this.flashObject.LoadMovie(0, this.movieFile);
      do
      {
        Application.DoEvents();
      }
      while (this.flashObject.ReadyState < 4);
    }

    /// <summary>
    /// The <see cref="Form.Shown"/> event handler. 
    /// Makes a screenshot of the flash movie in its initial state.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void FlashCapture_Shown(object sender, EventArgs e)
    {
      // Grab IViewObject interface from the ocx.  
      IViewObject viewObject = (IViewObject)this.flashObject.GetOcx();

      // Create empty thumb bitmap
      Image thumb = new Bitmap(this.Width, this.Height);
      thumb.Tag = this.movieFile;

      // Check for success
      if (viewObject == null)
      {
        this.screenShot = thumb;
        return;
      }

      // Create graphics for bitmap
      using (Graphics graphics = Graphics.FromImage(thumb))
      {
        // draw
        int hr = -1;

        // Set up RECTL structure
        Microsoft.VisualStudio.OLE.Interop.RECTL bounds = new Microsoft.VisualStudio.OLE.Interop.RECTL();
        bounds.left = 0;
        bounds.top = 0;
        bounds.right = this.Width;
        bounds.bottom = this.Height;

        // get hdc
        IntPtr hdc = graphics.GetHdc();

        // Draw
        hr = viewObject.Draw(
          Microsoft.VisualStudio.OLE.Interop.DVASPECT.DVASPECT_CONTENT,
          -1,
          IntPtr.Zero,
          IntPtr.Zero,
          IntPtr.Zero,
          hdc,
          ref bounds,
          ref bounds,
          IntPtr.Zero,
          (uint)0);

        // Release HDC
        graphics.ReleaseHdc(hdc);
      }

      this.screenShot = thumb;
      this.Close();
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
    #endregion //METHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER
    #endregion //HELPER
  }
}