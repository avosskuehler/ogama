// <copyright file="RichTextDialog.cs" company="FU Berlin">
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
  using System.Text;
  using System.Windows.Forms;

  using Ogama.Modules.Common.Controls;

  using VectorGraphics.Elements;
  using VectorGraphics.Tools.CustomEventArgs;

  /// <summary>
  /// This dialog <see cref="Form"/> is used to initially define an 
  /// rich text formatted instruction that can be added to a slide.
  /// </summary>
  public partial class RichTextDialog : Form
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
    /// Initializes a new instance of the RichTextDialog class.
    /// </summary>
    public RichTextDialog()
    {
      this.InitializeComponent();
      this.UpdateShapeProperties(this.pbcBorder.DrawAction, this.pbcBorder.NewBrush);
      this.audioControl.PathToCopyTo = Document.ActiveDocument.ExperimentSettings.SlideResourcesPath;
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
    public VGRichText NewRichText
    {
      get
      {
        VGRichText text = new VGRichText(
          this.pbcBorder.DrawAction,
          this.rtbInstruction.RichTextBox.Rtf,
          this.pbcBorder.DrawAction != ShapeDrawAction.Edge,
          this.pbcBorder.NewPen,
          this.pbcBorder.NewBrush,
          this.pbcBorder.NewFont,
          this.pbcBorder.NewFontColor,
          new PointF(0, 0), 
          new SizeF(300, 200),
          VGStyleGroup.AOI_NORMAL,
          this.pbcBorder.NewName,
          string.Empty);

        text.Sound = this.audioControl.Sound;
        return text;
      }

      set
      {
        this.pbcBorder.DrawAction = value.ShapeDrawAction;
        this.pbcBorder.NewPen = value.Pen;
        this.pbcBorder.NewBrush = value.Brush;
        this.pbcBorder.NewName = value.Name;
        this.pbcBorder.NewFont = value.Font;
        this.pbcBorder.NewFontColor = value.FontColor;
        this.UpdateShapeProperties(this.pbcBorder.DrawAction, this.pbcBorder.NewBrush);
        this.rtbInstruction.RichTextBox.Rtf = value.RtfToDraw;
        this.audioControl.Sound = value.Sound;
      }
    }

    /// <summary>
    /// Sets the <see cref="Color"/> of the background of this dialog.
    /// </summary>
    public Color RichTextBackgroundColor
    {
      set
      {
        this.pbcBorder.NewBrush = new SolidBrush(value);
        this.UpdateShapeProperties(this.pbcBorder.DrawAction, this.pbcBorder.NewBrush);
        if (value.ToArgb() == this.rtbInstruction.RichTextBox.ForeColor.ToArgb())
        {
          if (value == Color.Black)
          {
            this.rtbInstruction.RichTextBox.ForeColor = Color.WhiteSmoke;
          }
          else
          {
            this.rtbInstruction.RichTextBox.ForeColor = Color.Black;
          }
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
    /// The <see cref="Form.Shown"/> event handler that focuses
    /// the RichTextBox on showing.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A empty <see cref="EventArgs"/></param>
    private void RichTextDialog_Shown(object sender, EventArgs e)
    {
      this.rtbInstruction.Focus();
    }

    /// <summary>
    /// <see cref="Control.Click"/> event handler 
    /// for the <see cref="Button"/> <see cref="btnHelp"/>
    /// Raises a new <see cref="HelpDialog"/> dialog with instructions
    /// on how to define a new instruction and its position on the picture.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A empty <see cref="EventArgs"/></param>
    private void btnHelp_Click(object sender, EventArgs e)
    {
      HelpDialog dlg = new HelpDialog();
      dlg.HelpCaption = "How to: Define the position of the instruction.";
      StringBuilder sb = new StringBuilder();
      sb.Append("When the button is clicked you can specify position ");
      sb.Append("and text wrapping by specifying the bounding rectangle of the instruction with ");
      sb.AppendLine("the left mouse button (click and pull).");
      dlg.HelpMessage = sb.ToString();
      dlg.ShowDialog();
    }

    /// <summary>
    /// <see cref="Control.Click"/> event handler 
    /// for the <see cref="Button"/> <see cref="btnOK"/>
    /// Sets the <see cref="Form.DialogResult"/> property to <see cref="DialogResult.OK"/>.
    /// Set it manually, because otherwise hitting "enter" in RichTextBox will
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

    /// <summary>
    /// <see cref="OgamaControls.PenAndBrushControl.ShapePropertiesChanged"/> event handler 
    /// for the <see cref="OgamaControls.PenAndBrushControl"/> <see cref="pbcBorder"/>
    /// Sets the <see cref="Control.BackgroundImage"/> property to 
    /// the newly created brush fill.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A <see cref="ShapePropertiesChangedEventArgs"/> with the event data.</param>
    private void pbcBorder_ShapePropertiesChanged(object sender, ShapePropertiesChangedEventArgs e)
    {
      this.UpdateShapeProperties(e.ShapeDrawAction, e.Brush);
    }

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
    /// This method updates the edge and fill properties of the rich text
    /// with the given values
    /// </summary>
    /// <param name="newShapeDrawAction">The new <see cref="ShapeDrawAction"/></param>
    /// <param name="brush">The new <see cref="Brush"/> for the fill.</param>
    private void UpdateShapeProperties(ShapeDrawAction newShapeDrawAction, Brush brush)
    {
      this.rtbInstruction.BackColor = Color.Transparent;
      this.rtbInstruction.BackgroundImage = Properties.Resources.CheckBoard;

      if ((newShapeDrawAction & ShapeDrawAction.Fill) == ShapeDrawAction.Fill)
      {
        this.FillBackground(brush);
      }

      this.Refresh();
    }

    /// <summary>
    /// Fills the background of the RichTextBox with the given brush.
    /// </summary>
    /// <param name="brush">A <see cref="Brush"/> to use for the fill.</param>
    private void FillBackground(Brush brush)
    {
      if (brush is SolidBrush)
      {
        this.rtbInstruction.BackgroundImage = null;
        SolidBrush sb = (SolidBrush)brush;
        this.rtbInstruction.BackColor = sb.Color;
      }
      else
      {
        this.rtbInstruction.BackColor = Color.Transparent;
        Bitmap bmp = new Bitmap(this.rtbInstruction.Width, this.rtbInstruction.Height);
        using (Graphics graBmp = Graphics.FromImage(bmp))
        {
          graBmp.FillRectangle(brush, 0, 0, bmp.Width, bmp.Height);
          this.rtbInstruction.BackgroundImage = bmp;
        }
      }
    }
    #endregion //METHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER
    #endregion //HELPER
  }
}