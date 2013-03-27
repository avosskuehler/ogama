// <copyright file="TextDialog.cs" company="FU Berlin">
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
  using System.Drawing.Text;
  using System.Text;
  using System.Windows.Forms;

  using Ogama.Modules.Common.Controls;

  using VectorGraphics.Elements;

  /// <summary>
  /// This dialog <see cref="Form"/> is used to initially define a
  /// text instruction that can be added to a slide.
  /// </summary>
  public partial class TextDialog : Form
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
    /// Initializes a new instance of the TextDialog class.
    /// </summary>
    public TextDialog()
    {
      this.InitializeComponent();
      this.audioControl.PathToCopyTo = Document.ActiveDocument.ExperimentSettings.SlideResourcesPath;
      this.SetFont(new Font("Arial", 24f));
      this.txbInstruction.ForeColor = this.clcTextColor.SelectedColor;
      this.btnAlignLeft.Checked = true;
      this.AdjustTextBoxBackground();
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
    public VGText NewText
    {
      get
      {
        VGText text = new VGText(
          this.pbcBorder.DrawAction,
          this.txbInstruction.Text,
          this.txbInstruction.Font,
          this.txbInstruction.ForeColor,
          this.txbInstruction.TextAlign,
          (float)this.nudLineSpacing.Value,
          (float)this.nudPadding.Value,
          this.pbcBorder.NewPen,
          this.pbcBorder.NewBrush,
          this.pbcBorder.NewFont,
          this.pbcBorder.NewFontColor,
          new RectangleF(0, 0, 300, 200),
          VGStyleGroup.AOI_NORMAL,
          this.pbcBorder.NewName,
          string.Empty,
          this.audioControl.Sound);
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
        this.txbInstruction.Text = value.StringToDraw;
        this.txbInstruction.TextAlign = value.Alignment;
        this.nudLineSpacing.Value = (decimal)value.LineSpacing;
        this.nudPadding.Value = (decimal)value.Padding;
        this.txbInstruction.Font = value.TextFont;
        this.txbInstruction.ForeColor = value.TextFontColor;
        this.audioControl.Sound = value.Sound;
        this.clcTextColor.SelectedColor = value.TextFontColor;
        this.SetFont(value.TextFont);
        this.btnAlignLeft.Checked = false;
        switch (value.Alignment)
        {
          case HorizontalAlignment.Center:
            this.btnAlignCenter.Checked = true;
            break;
          case HorizontalAlignment.Left:
            this.btnAlignLeft.Checked = true;
            break;
          case HorizontalAlignment.Right:
            this.btnAlignRight.Checked = true;
            break;
        }

        this.AdjustTextBoxBackground();
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
    /// the <see cref="TextBox"/> on showing.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A empty <see cref="EventArgs"/></param>
    private void TextDialog_Shown(object sender, EventArgs e)
    {
      this.txbInstruction.Focus();
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnAlignLeft"/>.
    /// Sets <see cref="TextBox.TextAlign"/> to <see cref="HorizontalAlignment.Left"/>
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A empty <see cref="EventArgs"/></param>
    private void btnAlignLeft_Click(object sender, EventArgs e)
    {
      this.txbInstruction.TextAlign = HorizontalAlignment.Left;
      this.btnAlignCenter.Checked = false;
      this.btnAlignLeft.Checked = true;
      this.btnAlignRight.Checked = false;
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnAlignCenter"/>.
    /// Sets <see cref="TextBox.TextAlign"/> to <see cref="HorizontalAlignment.Center"/>
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A empty <see cref="EventArgs"/></param>
    private void btnAlignCenter_Click(object sender, EventArgs e)
    {
      this.txbInstruction.TextAlign = HorizontalAlignment.Center;
      this.btnAlignCenter.Checked = true;
      this.btnAlignLeft.Checked = false;
      this.btnAlignRight.Checked = false;
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnAlignRight"/>.
    /// Sets <see cref="TextBox.TextAlign"/> to <see cref="HorizontalAlignment.Right"/>
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A empty <see cref="EventArgs"/></param>
    private void btnAlignRight_Click(object sender, EventArgs e)
    {
      this.txbInstruction.TextAlign = HorizontalAlignment.Right;
      this.btnAlignCenter.Checked = false;
      this.btnAlignLeft.Checked = false;
      this.btnAlignRight.Checked = true;
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

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the <see cref="ToolStripButton"/>
    /// <see cref="btnBold"/>
    /// Updates the font of the textbox with the bold style.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A empty <see cref="EventArgs"/></param>
    private void btnBold_Click(object sender, EventArgs e)
    {
      FontStyle style = this.GetFontStyle();
      this.txbInstruction.Font = new Font(this.txbInstruction.Font, style);
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the <see cref="ToolStripButton"/>
    /// <see cref="btnItalic"/>
    /// Updates the font of the textbox with the italic style.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A empty <see cref="EventArgs"/></param>
    private void btnItalic_Click(object sender, EventArgs e)
    {
      FontStyle style = this.GetFontStyle();
      this.txbInstruction.Font = new Font(this.txbInstruction.Font, style);
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the <see cref="ToolStripButton"/>
    /// <see cref="btnUnderline"/>
    /// Updates the font of the textbox with the underline style.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A empty <see cref="EventArgs"/></param>
    private void btnUnderline_Click(object sender, EventArgs e)
    {
      FontStyle style = this.GetFontStyle();
      this.txbInstruction.Font = new Font(this.txbInstruction.Font, style);
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the <see cref="ToolStripButton"/>
    /// <see cref="btnStrikeout"/>
    /// Updates the font of the textbox with the strikeout style.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A empty <see cref="EventArgs"/></param>
    private void btnStrikeout_Click(object sender, EventArgs e)
    {
      FontStyle style = this.GetFontStyle();
      this.txbInstruction.Font = new Font(this.txbInstruction.Font, style);
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the <see cref="NumericUpDown"/>
    /// for the font size. 
    /// Updates the font of the textbox with the new font size.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A empty <see cref="EventArgs"/></param>
    private void nudFontSize_Click(object sender, EventArgs e)
    {
      FontStyle style = this.GetFontStyle();
      this.txbInstruction.Font = new Font(this.txbInstruction.Font.FontFamily, (float)this.nudFontSize.Value, style);
    }

    /// <summary>
    /// The <see cref="ComboBox.SelectedIndexChanged"/> event handler for the font face.
    /// Updates the font of the textbox with the new value.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A empty <see cref="EventArgs"/></param>
    private void cbbFontFace_SelectedIndexChanged(object sender, EventArgs e)
    {
      FontStyle style = this.GetFontStyle();
      this.txbInstruction.Font = new Font(this.cbbFontFace.Text, (float)this.nudFontSize.Value, style);
    }

    #endregion //WINDOWSEVENTHANDLER

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler for Custom Defined Events                                    //
    ///////////////////////////////////////////////////////////////////////////////
    #region CUSTOMEVENTHANDLER

    /// <summary>
    /// The <see cref="OgamaControls.ColorSelectControl.ColorChanged"/> event handler.
    /// Updates the textbox color and adjust the background if applicable.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A <see cref="OgamaControls.ColorChangedEventArgs"/> with the event data.</param>
    private void clcTextColor_ColorChanged(object sender, OgamaControls.ColorChangedEventArgs e)
    {
      this.txbInstruction.ForeColor = this.clcTextColor.SelectedColor;
      this.AdjustTextBoxBackground();
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
    /// Returns the <see cref="FontStyle"/> according to the checked state of the 
    /// style buttons.
    /// </summary>
    /// <returns>The <see cref="FontStyle"/> flags</returns>
    private FontStyle GetFontStyle()
    {
      FontStyle style = FontStyle.Regular;
      if (this.btnBold.Checked)
      {
        style |= FontStyle.Bold;
      }

      if (this.btnItalic.Checked)
      {
        style |= FontStyle.Italic;
      }

      if (this.btnUnderline.Checked)
      {
        style |= FontStyle.Underline;
      }

      if (this.btnStrikeout.Checked)
      {
        style |= FontStyle.Strikeout;
      }

      return style;
    }

    /// <summary>
    /// This methods sets checks the buttons for the 
    /// given <see cref="FontStyle"/>.
    /// </summary>
    /// <param name="style">The <see cref="FontStyle"/> to be set.</param>
    private void SetFontStyle(FontStyle style)
    {
      this.btnBold.Checked = false;
      this.btnItalic.Checked = false;
      this.btnUnderline.Checked = false;
      this.btnStrikeout.Checked = false;
      if (style == (style | FontStyle.Bold))
      {
        this.btnBold.Checked = true;
      }

      if (style == (style | FontStyle.Italic))
      {
        this.btnItalic.Checked = true;
      }

      if (style == (style | FontStyle.Strikeout))
      {
        this.btnStrikeout.Checked = true;
      }

      if (style == (style | FontStyle.Underline))
      {
        this.btnUnderline.Checked = true;
      }
    }

    /// <summary>
    /// This method changes the background color to black
    /// if the luminance value of the text color is greater than 50 %.
    /// </summary>
    private void AdjustTextBoxBackground()
    {
      // Adjust background Brightness
      float hue = 0.0f;
      float saturation = 0.0f;
      float luminance = 0.0f;
      float transpareny = 0.0f;

      this.RGBToHSL(this.txbInstruction.ForeColor, ref hue, ref saturation, ref luminance, ref transpareny);
      if (luminance > 0.5f)
      {
        this.txbInstruction.BackColor = Color.Black;
      }
      else
      {
        this.txbInstruction.BackColor = Color.White;
      }
    }

    /// <summary>
    /// Setup the controls to match the current font
    /// </summary>
    /// <param name="font">The <see cref="Font"/> to be setup up.</param>
    private void SetFont(Font font)
    {
      // font face
      InstalledFontCollection fontSet = new InstalledFontCollection();
      FontFamily[] fontList = fontSet.Families;
      int selectedIndex = 0;
      for (int ii = 0; ii < fontList.Length; ii++)
      {
        if (fontList[ii].IsStyleAvailable(FontStyle.Bold) &&
           fontList[ii].IsStyleAvailable(FontStyle.Italic) &&
           fontList[ii].IsStyleAvailable(FontStyle.Underline) &&
           fontList[ii].IsStyleAvailable(FontStyle.Strikeout))
        {
          int idx = this.cbbFontFace.Items.Add(fontList[ii].Name);
          if (fontList[ii].Name == font.Name)
          {
            selectedIndex = idx;
          }
        }
      }

      this.cbbFontFace.SelectedIndex = selectedIndex;
      this.SetFontStyle(font.Style);

      // font size
      this.nudFontSize.Value = (decimal)font.Size;

      this.txbInstruction.Font = font;
    }

    #endregion //METHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER

    /// <summary>
    /// Convert RGB color to Hue, Saturation, Luminance. Adapted from C++ code for CColor
    /// obtained from CColor - RGB and HLS combined in one class By Christian Rodemeyer 
    /// at http://codeproject.com/bitmap/ccolor.asp
    /// </summary>
    /// <param name="inColor">The <see cref="Color"/> to be converted</param>
    /// <param name="hue">A <see cref="Single"/> with the Hue 0-360</param>
    /// <param name="saturation">A <see cref="Single"/> with the Saturation (0.0 - 1.0)</param>
    /// <param name="luminance">A <see cref="Single"/> with the Luminance (0.0 - 1.0)</param>
    /// <param name="transparency">A <see cref="Single"/> with the Transparency (0.0 - 1.0)</param>
    private void RGBToHSL(Color inColor, ref float hue, ref float saturation, ref float luminance, ref float transparency)
    {
      float red = (float)inColor.R;
      float green = (float)inColor.G;
      float blue = (float)inColor.B;
      transparency = (float)(1 - inColor.A / 255.0f);

      float minval = red;
      if (green < minval)
      {
        minval = green;
      }

      if (blue < minval)
      {
        minval = blue;
      }

      float maxval = red;
      if (green > maxval)
      {
        maxval = green;
      }

      if (blue > maxval)
      {
        maxval = blue;
      }

      float mdiff = maxval - minval;
      float msum = maxval + minval;

      luminance = msum / 510.0f;
      saturation = 0.0f;
      hue = 0.0f;

      if (maxval == minval)
      {
        saturation = 0.0f;
        hue = 0.0f;
      }
      else
      {
        float rnorm = (maxval - red) / mdiff;
        float gnorm = (maxval - green) / mdiff;
        float bnorm = (maxval - blue) / mdiff;

        if (luminance <= 0.5f)
        {
          saturation = mdiff / msum;
        }
        else
        {
          saturation = mdiff / (510.0f - msum);
        }

        if (red == maxval)
        {
          hue = 60.0f * (6.0f + bnorm - gnorm);
        }

        if (green == maxval)
        {
          hue = 60.0f * (2.0f + rnorm - bnorm);
        }

        if (blue == maxval)
        {
          hue = 60.0f * (4.0f + gnorm - rnorm);
        }

        if (hue > 360.0f)
        {
          hue = hue - 360.0f;
        }
      }
    }

    #endregion //HELPER
  }
}