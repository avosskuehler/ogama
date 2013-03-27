// <copyright file="FlashDialog.cs" company="FU Berlin">
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

namespace Ogama.Modules.SlideshowDesign.DesignModule.StimuliDialogs
{
  using System;
  using System.Drawing;
  using System.IO;
  using System.Text;
  using System.Windows.Forms;

  using Ogama.ExceptionHandling;
  using Ogama.Modules.Common.Controls;

  using VectorGraphics.Elements;

  /// <summary>
  /// This dialog <see cref="Form"/> is used to initially define an 
  /// flash movie that can be added to a slide.
  /// </summary>
  public partial class FlashDialog : Form
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
    /// Saves the full filename with path of the flash movie
    /// specified in this dialog.
    /// </summary>
    private string fullFilename;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the FlashDialog class.
    /// </summary>
    public FlashDialog()
    {
      this.InitializeComponent();
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets or sets the <see cref="VGRichText"/> of the instruction
    /// specified in this dialog.
    /// It is placed at 0,0 with size 300,200.
    /// </summary>
    public VGFlash NewFlash
    {
      get
      {
        VGFlash flash = new VGFlash(
          this.pbcBorder.DrawAction,
          this.txbFlashMovieFilename.Text,
          Document.ActiveDocument.ExperimentSettings.SlideResourcesPath,
          this.pbcBorder.NewPen,
          this.pbcBorder.NewBrush,
          this.pbcBorder.NewFont,
          this.pbcBorder.NewFontColor,
          new PointF(0, 0),
          new SizeF(300, 200),
          VGStyleGroup.ACTIVEX,
          this.pbcBorder.NewName,
          string.Empty);

        flash.Sound = this.audioControl.Sound;
        return flash;
      }

      set
      {
        this.pbcBorder.DrawAction = value.ShapeDrawAction;
        this.pbcBorder.NewPen = value.Pen;
        this.pbcBorder.NewBrush = value.Brush;
        this.pbcBorder.NewName = value.Name;
        this.pbcBorder.NewFont = value.Font;
        this.pbcBorder.NewFontColor = value.FontColor;
        this.txbFlashMovieFilename.Text = value.Filename;
        this.audioControl.Sound = value.Sound;
      }
    }

    /// <summary>
    /// Gets or sets the full filename with path of the flash movie
    /// specified in this dialog.
    /// </summary>
    /// <value>A <see cref="string"/> with flash movie filename with path.</value>
    public string FlashFile
    {
      get
      {
        return this.fullFilename;
      }

      set
      {
        if (File.Exists(value))
        {
          this.fullFilename = value;
          this.axShockwaveFlash.LoadMovie(0, value);
        }
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
    /// <see cref="Control.Click"/> event handler 
    /// for the <see cref="Button"/> <see cref="btnOpenFlashMovieFile"/>
    /// Raises a <see cref="OpenFileDialog"/> and updates the <see cref="txbFlashMovieFilename"/>.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A empty <see cref="EventArgs"/></param>
    private void btnOpenFlashMovieFile_Click(object sender, EventArgs e)
    {
      OpenFileDialog dlg = new OpenFileDialog();
      dlg.Filter = "Flash movies (*.SWF)|*.SWF|All files (*.*)|*.*";
      dlg.Title = "Select flash movie ...";
      dlg.InitialDirectory = Document.ActiveDocument.ExperimentSettings.SlideResourcesPath;

      if (dlg.ShowDialog() == DialogResult.OK)
      {
        string fileName = dlg.FileName;
        if (!File.Exists(fileName))
        {
          // Erase textbox entry
          this.txbFlashMovieFilename.Text = string.Empty;
          this.fullFilename = string.Empty;
          return;
        }

        string templatePath = Document.ActiveDocument.ExperimentSettings.SlideResourcesPath;
        this.fullFilename = fileName;
        if (Path.GetDirectoryName(fileName) != templatePath)
        {
          string target = Path.Combine(templatePath, Path.GetFileName(fileName));
          if (!File.Exists(target))
          {
            File.Copy(fileName, target);

            string message = "A copy of this movie file is saved to the following slide resources folder of the current project :" +
              Environment.NewLine + target + Environment.NewLine +
              "This is done because of easy movement of experiments between different computers or locations.";

            ExceptionMethods.ProcessMessage("File copied", message);
          }

          this.fullFilename = target;
        }

        this.txbFlashMovieFilename.Text = Path.GetFileName(this.fullFilename);
        this.toolTip1.SetToolTip(this.txbFlashMovieFilename, this.fullFilename);
        this.axShockwaveFlash.LoadMovie(0, this.fullFilename);
        this.pnlPreview.Refresh();
      }
    }

    /// <summary>
    /// <see cref="Control.Click"/> event handler 
    /// for the <see cref="Button"/> <see cref="btnHelp"/>
    /// Raises a new <see cref="HelpDialog"/> dialog with instructions.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A empty <see cref="EventArgs"/></param>
    private void btnHelp_Click(object sender, EventArgs e)
    {
      HelpDialog dlg = new HelpDialog();
      dlg.HelpCaption = "How to: Interact with the movie.";
      StringBuilder sb = new StringBuilder();
      sb.Append("In the preview and on the slide you can interact with the flash movie just as usual.");
      dlg.HelpMessage = sb.ToString();
      dlg.ShowDialog();
    }

    /// <summary>
    /// <see cref="Control.Click"/> event handler 
    /// for the <see cref="Button"/> <see cref="btnOK"/>
    /// Sets the <see cref="Form.DialogResult"/> property to <see cref="DialogResult.OK"/>.
    /// Set it manually, because otherwise hitting "enter" in TextBox will
    /// finish dialog.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A empty <see cref="EventArgs"/></param>
    private void btnOK_Click(object sender, EventArgs e)
    {
      this.DialogResult = DialogResult.OK;
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