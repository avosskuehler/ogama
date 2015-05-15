// <copyright file="VGText.cs" company="FU Berlin">
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

namespace VectorGraphics.Elements
{
  using System;
  using System.ComponentModel;
  using System.Drawing;
  using System.Text;
  using System.Windows.Forms;
  using System.Xml.Serialization;

  using VectorGraphics.Tools;
  using VectorGraphics.Tools.CustomTypeConverter;

  /// <summary>
  /// Inherited from <see cref="VGElement"/>. 
  /// A serializable class that is a vector graphics text,
  /// drawn with a specific font and font color.
  /// </summary>
  [Serializable]
  public class VGText : VGElement
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
    /// Text to be drawn.
    /// </summary>
    private string text;

    /// <summary>
    /// The font that should be used for drawing of the text.
    /// </summary>
    private Font textFont;

    /// <summary>
    /// The font color that should be used for drawing of the text.
    /// </summary>
    private Color textFontColor;

    /// <summary>
    /// Saves the <see cref="HorizontalAlignment"/> of the text.
    /// </summary>
    private HorizontalAlignment textAlignment;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the VGText class.
    /// </summary>
    /// <param name="newShapeDrawAction">Drawing action: Edge, Fill, Both</param>
    /// <param name="newText">string to display</param>
    /// <param name="newFont">Font to use for the text</param>
    /// <param name="newFontColor">Color for the text</param>
    /// <param name="newAlignment">The <see cref="HorizontalAlignment"/> for the text.</param>
    /// <param name="newLineSpacing">The <see cref="float"/> with the new line spacing for the text.</param>
    /// <param name="newPadding">The <see cref="float"/> with the new text padding.</param>
    /// <param name="newPen">Pen for bounds</param>
    /// <param name="newBrush">Brush for fills</param>
    /// <param name="newNameFont">Font for name</param>
    /// <param name="newNameFontColor">Color for name</param>
    /// <param name="newBounds">Bounds of element</param>
    /// <param name="newStyleGroup">Group Enumeration, <see cref="VGStyleGroup"/></param>
    /// <param name="newName">Name of Element</param>
    /// <param name="newElementGroup">Element group description</param>
    /// <param name="newSound">Audio contents of Element</param>
    public VGText(
      ShapeDrawAction newShapeDrawAction,
      string newText,
      Font newFont,
      Color newFontColor,
      HorizontalAlignment newAlignment,
      float newLineSpacing,
      float newPadding,
      Pen newPen,
      Brush newBrush,
      Font newNameFont,
      Color newNameFontColor,
      RectangleF newBounds,
      VGStyleGroup newStyleGroup,
      string newName,
      string newElementGroup,
      AudioFile newSound)
      : base(
      newShapeDrawAction,
      newPen,
      newBrush,
      newNameFont,
      newNameFontColor,
      newBounds,
      newStyleGroup,
      newName,
      newElementGroup,
      newSound)
    {
      this.textFontColor = newFontColor;
      this.textAlignment = newAlignment;
      this.text = newText;
      this.textFont = newFont;
      this.LineSpacing = newLineSpacing != 0 ? newLineSpacing : 1.0f;
      this.Padding = newPadding;
    }

    /// <summary>
    /// Prevents a default instance of the VGText class from being created.
    /// Parameterless constructor. Used for serialization.
    /// </summary>
    private VGText()
    {
    }

    /// <summary>
    /// Initializes a new instance of the VGText class.
    /// Clone Constructor. Creates new text that is
    /// identical to the given <see cref="VGText"/>.
    /// </summary>
    /// <param name="cloneText">Text element to clone.</param>
    private VGText(VGText cloneText)
      : base(
      cloneText.ShapeDrawAction,
      cloneText.Pen,
      cloneText.Brush,
      cloneText.Font,
      cloneText.FontColor,
      cloneText.Bounds,
      cloneText.StyleGroup,
      cloneText.Name,
      cloneText.ElementGroup,
      cloneText.Sound)
    {
      this.textFontColor = cloneText.TextFontColor;
      this.text = cloneText.StringToDraw;
      this.textAlignment = cloneText.Alignment;
      this.textFont = cloneText.TextFont == null ? this.Font : (Font)cloneText.TextFont.Clone();
      this.LineSpacing = cloneText.LineSpacing != 0 ? cloneText.LineSpacing : 1.0f;
      this.Padding = cloneText.Padding;
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets or sets the text of this element.
    /// </summary>
    /// <value>A <see cref="string"/> with the instruction.</value>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Category("Appearance")]
    [Description("The string to draw as instruction.")]
    public string StringToDraw
    {
      get { return this.text; }
      set { this.text = value; }
    }

    /// <summary>
    /// Gets or sets the horizontal alignment of the text.
    /// </summary>
    /// <value>A <see cref="HorizontalAlignment"/> with the alignment for the text.</value>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Category("Appearance")]
    [Description("The horizonal alignment of the text.")]
    public HorizontalAlignment Alignment
    {
      get { return this.textAlignment; }
      set { this.textAlignment = value; }
    }

    /// <summary>
    /// Gets or sets font of text element
    /// </summary>
    /// <value>A <see cref="Font"/> for drawing the name of this shape.</value>
    [XmlIgnoreAttribute]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Category("Appearance")]
    [Description("The font to use for the text to draw.")]
    public Font TextFont
    {
      get
      {
        // Ogama V0.X versions used the inherited font property
        // instead of the new textFont, so update it here.
        return this.textFont ?? (this.textFont = this.Font);
      }

      set
      {
        this.textFont = value;
      }
    }

    /// <summary>
    /// Gets or sets SerializedTextFont.
    /// Serializes the <see cref="TextFont"/> property to XML.
    /// </summary>
    /// <value>A <see cref="string"/> with the string representation of the shapes font.</value>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public string SerializedTextFont
    {
      get
      {
        TypeConverter fontConverter = TypeDescriptor.GetConverter(typeof(Font));
        if (this.textFont != null)
        {
          return fontConverter.ConvertToInvariantString(this.textFont);
        }
        else
        {
          return fontConverter.ConvertToInvariantString(this.Font);
        }
      }

      set
      {
        if (!string.IsNullOrEmpty(value))
        {
          try
          {
            if (value.Contains("#"))
            {
              // Ogama Version 0.X format
              this.textFont = ObjectStringConverter.StringToFontOld(value);
            }
            else
            {
              TypeConverter fontConverter = TypeDescriptor.GetConverter(typeof(Font));
              this.textFont = (Font)fontConverter.ConvertFromInvariantString(value);
            }
          }
          catch (ArgumentException)
          {
            // Paranoia check for older versions.
            this.textFont = new Font(SystemFonts.MenuFont.FontFamily, 28f, GraphicsUnit.Point);
          }
        }
        else
        {
          this.textFont = this.Font;
        }
      }
    }

    /// <summary>
    /// Gets or sets used color for the graphic elements font.
    /// </summary>
    /// <value>A <see cref="Color"/> for the font of this shape.</value>
    [XmlIgnoreAttribute]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Category("Appearance")]
    [Description("The font color to use for the text.")]
    public Color TextFontColor
    {
      get
      {
        // Ogama V0.X versions used the inherited font property
        // instead of the new textFont, so update it here.
        if (this.textFontColor == Color.Empty)
        {
          this.textFontColor = this.FontColor;
        }

        return this.textFontColor;
      }

      set
      {
        this.textFontColor = value;
      }
    }

    /// <summary>
    /// Gets or sets SerializedTextFontColor.
    /// Serializes and deserializes the <see cref="TextFontColor"/> to XML,
    /// because XMLSerializer can not serialize <see cref="Color"/> values.
    /// </summary>
    /// <value>A <see cref="string"/> with the string representation of the shapes font color.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string SerializedTextFontColor
    {
      get
      {
        return ObjectStringConverter.ColorToHtmlAlpha(this.textFontColor);
      }

      set
      {
        if (string.IsNullOrEmpty(value))
        {
          value = this.SerializedFontColor;
        }

        this.textFontColor = ObjectStringConverter.HtmlAlphaToColor(value);
      }
    }

    /// <summary>
    /// Gets or sets the factor for the line spacing of the text.
    /// </summary>
    /// <value>A <see cref="Single"/> with the factor for the line spacing for the text.</value>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible), Category("Appearance"), Description("The line spacing of the text."), DefaultValue(1.0f)]
    public float LineSpacing { get; set; }

    /// <summary>
    /// Gets or sets the text padding of the text.
    /// </summary>
    /// <value>A <see cref="Single"/> with the text padding of the text.</value>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible), Category("Appearance"), Description("The padding for the text from its bounds."), DefaultValue(6.0f)]
    public float Padding { get; set; }

    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Inherited methods                                                         //
    ///////////////////////////////////////////////////////////////////////////////
    #region OVERRIDES

    /// <summary>
    /// Overridden <see cref="VGElement.Draw(Graphics)"/>.  
    /// Draws text with the owning brush and font onto given 
    /// graphics context.
    /// </summary>
    /// <param name="graphics">Graphics context to draw on.</param>
    /// <exception cref="ArgumentNullException">Thrown, when graphics object is null.</exception>
    public override void Draw(Graphics graphics)
    {
      if (graphics == null)
      {
        throw new ArgumentNullException("Graphics object should not be null.");
      }

      if (this.LineSpacing == 0)
      {
        this.LineSpacing = 1;
      }

      graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

      // create a new string format object
      StringFormat sf = StringFormat.GenericTypographic;

      // set the alignment
      switch (this.textAlignment)
      {
        case HorizontalAlignment.Center:
          sf.Alignment = StringAlignment.Center;
          break;
        case HorizontalAlignment.Left:
          sf.Alignment = StringAlignment.Near;
          break;
        case HorizontalAlignment.Right:
          sf.Alignment = StringAlignment.Far;
          break;
      }

      if (this.Bounds.Width <= 0 || this.Bounds.Height <= 0)
      {
        this.Bounds = new RectangleF(this.Location, graphics.MeasureString(this.text, this.textFont, this.Location, sf));
      }

      RectangleF halfInlineRect = this.Bounds;
      if (this.Padding * 2 < halfInlineRect.Width && this.Padding < halfInlineRect.Height)
      {
        halfInlineRect.Inflate(-this.Padding, -this.Padding);
      }

      RectangleF innerBounds = this.Bounds;
      if ((ShapeDrawAction & ShapeDrawAction.Edge) == ShapeDrawAction.Edge)
      {
        innerBounds.Inflate(-this.Pen.Width, -this.Pen.Width);
      }

      if ((ShapeDrawAction & ShapeDrawAction.Fill) == ShapeDrawAction.Fill)
      {
        graphics.FillRectangle(this.Brush, innerBounds);
      }

      if ((ShapeDrawAction & ShapeDrawAction.Edge) == ShapeDrawAction.Edge)
      {
        graphics.DrawRectangle(this.Pen, halfInlineRect.X, halfInlineRect.Y, halfInlineRect.Width, halfInlineRect.Height);
      }

      RectangleF textBounds = innerBounds;
      if (this.Padding * 2 < textBounds.Width && this.Padding < textBounds.Height)
      {
        textBounds.Inflate(-this.Padding, -this.Padding);
      }

      SizeF fit = new SizeF(textBounds.Width, this.TextFont.Height);
      int spacing = (int)(this.LineSpacing * this.TextFont.Height);
      int line = 0;
      RectangleF lineBounds = textBounds;
      lineBounds.Height = spacing;
      for (int ix = 0; ix < this.text.Length;)
      {
        int chars, lines;
        graphics.MeasureString(this.text.Substring(ix), this.TextFont, fit, sf, out chars, out lines);
        if (chars > 0)
        {
          graphics.DrawString(
          this.text.Substring(ix, chars),
          this.TextFont,
          new SolidBrush(this.textFontColor),
          lineBounds,
          sf);
        }

        ++line;
        ix += chars;
        lineBounds.Offset(0, spacing);
      }

      RectangleF newBounds = this.Bounds;
      newBounds.Height = spacing * line;
      if ((ShapeDrawAction & ShapeDrawAction.Edge) == ShapeDrawAction.Edge)
      {
        newBounds.Height += this.Pen.Width + 2 * this.Padding;
      }

      this.Bounds = newBounds;

      // Draw name and selection frame if applicable
      base.Draw(graphics);
    }

    /// <summary>
    /// Overridden <see cref="VGElement.Reset()"/>. 
    /// Resets the current text element to
    /// default values (empty instruction).
    /// </summary>
    public override void Reset()
    {
      base.Reset();
      this.textAlignment = HorizontalAlignment.Center;
      this.text = string.Empty;
      this.textFont = SystemFonts.MenuFont;
      this.textFontColor = Color.Blue;
      this.LineSpacing = 1;
    }

    /// <summary>
    /// Overridden <see cref="Object.ToString()"/> method.
    /// Returns the <see cref="VGText"/> properties as a human readable string.
    /// </summary>
    /// <returns>A <see cref="string"/> that represents this <see cref="VGText"/>.</returns>
    public override string ToString()
    {
      StringBuilder sb = new StringBuilder();
      sb.Append("VGText, Name: ");
      sb.Append(this.Name);
      sb.Append(" ; '");
      sb.Append(this.text);
      sb.Append("' ; Alignment: ");
      sb.Append(this.textAlignment.ToString());
      sb.Append(" ; Pen: ");
      sb.Append(ObjectStringConverter.PenToString(this.Pen));
      sb.Append(" ; Group: ");
      sb.Append(this.StyleGroup.ToString());
      sb.Append(" ; Bounds: ");
      sb.Append(this.Bounds.ToString());
      return sb.ToString();
    }

    /// <summary>
    /// Overridden <see cref="VGElement.ToShortString()"/> method.
    /// Returns the main <see cref="VGText"/> properties as a human readable string.
    /// </summary>
    /// <returns>A <see cref="string"/> that represents the 
    /// current <see cref="VGText"/> in short form with its main properties.</returns>
    public override string ToShortString()
    {
      StringBuilder sb = new StringBuilder();
      sb.Append("Text ");
      sb.Append(this.text.Substring(0, this.text.Length > 12 ? Math.Max(12, this.text.Length - 1) : this.text.Length));
      sb.Append(" ...");
      return sb.ToString();
    }

    /// <summary>
    /// Overridden <see cref="VGElement.CloneCore()"/>.  
    /// Creates an excact copy of given <see cref="VGText"/>.
    /// </summary>
    /// <returns>Excact copy of this text element.</returns>
    protected override VGElement CloneCore()
    {
      return new VGText(this);
    }

    /// <summary>
    /// Overridden <see cref="VGElement.AddGrabHandles()"/>. 
    /// Adds a middle right, bottom middle and bottom right 
    /// grab handle to the current text.
    /// </summary>
    protected override void AddGrabHandles()
    {
      this.AddGrabHandles(true, false, false, false, false, true, false, false, false);
    }

    #endregion //OVERRIDES

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler                                                              //
    ///////////////////////////////////////////////////////////////////////////////
    #region EVENTS

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler for UI, Menu, Buttons, Toolbars etc.                         //
    ///////////////////////////////////////////////////////////////////////////////
    #region WINDOWSEVENTHANDLER
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
    // Methods for doing main class job                                          //
    ///////////////////////////////////////////////////////////////////////////////
    #region METHODS

    /////// <summary>
    /////// Use this routine to recalculate bounds and position after new text or font is set.
    /////// </summary>
    /////// <param name="g">Graphics to draw to.</param>
    ////public void RecalculateBounds(Graphics g)
    ////{
    ////  SizeF textSize = g.MeasureString(text, textFont);
    ////  PointF currentCenter = this.Center;
    ////  RectangleF textRect = new RectangleF(0, 0, textSize.Width, textSize.Height);
    ////  this.Bounds = textRect;
    ////  this.Center = currentCenter;
    ////}

    #endregion //METHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER
    #endregion //HELPER
  }
}