// <copyright file="VGElement.cs" company="FU Berlin">
// ******************************************************
// OGAMA - open gaze and mouse analyzer 
// Copyright (C) 2012 Adrian Voßkühler  
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
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.Drawing;
  using System.Drawing.Drawing2D;
  using System.Runtime.Serialization.Formatters.Binary;
  using System.Text;
  using System.Windows.Forms;
  using System.Xml.Serialization;

  using VectorGraphics.Tools;
  using VectorGraphics.Tools.CustomTypeConverter;

  /// <summary>
  /// The abstract parent class for all vector graphic elements.
  /// Is serializable and implements <see cref="ICloneable"/>
  /// </summary>
  /// <remarks>When drawing edges the line is drawn inside the bounds,
  /// except the <see cref="VGPolyline"/> class, where the edge
  /// is drawn on the center of the bounds.</remarks>
  [Serializable]
  [XmlInclude(typeof(VGCursor)),
  XmlInclude(typeof(VGLine)),
  XmlInclude(typeof(VGPolyline)),
  XmlInclude(typeof(VGRectangle)),
  XmlInclude(typeof(VGSharp)),
  XmlInclude(typeof(VGEllipse)),
  XmlInclude(typeof(VGRegion)),
  XmlInclude(typeof(VGText)),
  XmlInclude(typeof(VGRichText)),
  XmlInclude(typeof(VGSound)),
  XmlInclude(typeof(VGImage)),
  XmlInclude(typeof(VGScrollImage)),
  XmlInclude(typeof(VGArrow)),
  XmlInclude(typeof(VGFlash)),
  XmlInclude(typeof(VGBrowser))]
  [TypeConverter(typeof(VGElementConverter))]
  public abstract class VGElement : ICloneable, IDisposable
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
    /// Pen to draw edges.
    /// </summary>
    private Pen pen;

    /// <summary>
    /// DrawAction: Edge, Fill or Both.
    /// </summary>
    private ShapeDrawAction shapeDrawAction;

    /// <summary>
    /// Flag. True, if shape should be resizable and be 
    /// drawn with grab handles.
    /// </summary>
    private bool isInEditMode;

    /// <summary>
    /// Name for this Graphic object, used for reference.
    /// </summary>
    private string name;

    /// <summary>
    /// Groupname for this Graphic object, used for changing styles of
    /// a group of style-equal objects
    /// </summary>
    private VGStyleGroup styleGroup;

    /// <summary>
    /// String to group bulk of elements.
    /// </summary>
    private string elementGroup;

    /// <summary>
    /// Coordinate of top left corner of the element.
    /// </summary>
    private PointF location;

    /// <summary>
    /// Bounding width and height of the element.
    /// </summary>
    private SizeF size;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes static members of the VGElement class.
    /// </summary>
    static VGElement()
    {
      VGElement.DefaultPen = Pens.Aqua;
      VGElement.DefaultBrush = Brushes.Aqua;
      VGElement.DefaultFont = SystemFonts.DefaultFont;
      VGElement.DefaultFontColor = SystemColors.WindowText;
    }

    /// <summary>
    /// Initializes a new instance of the VGElement class.
    /// Parameterless Constructor. Used for serialization.
    /// </summary>
    public VGElement()
    {
    }

    /// <summary>
    /// Initializes a new instance of the VGElement class.
    /// </summary>
    /// <param name="newShapeDrawAction">Drawing action: Edge, Fill, Both</param>
    /// <param name="newPen">Pen for edges</param>
    /// <param name="newBrush">Brush for text and fills</param>
    /// <param name="newBounds">Bounds of element</param>
    /// <param name="newStyleGroup">Group Enumeration, <see cref="VGStyleGroup"/></param>
    /// <param name="newName">Name of Element</param>
    /// <param name="newElementGroup">Element group description</param>
    public VGElement(
      ShapeDrawAction newShapeDrawAction,
      Pen newPen,
      Brush newBrush,
      RectangleF newBounds,
      VGStyleGroup newStyleGroup,
      string newName,
      string newElementGroup)
    {
      this.InitStandards();
      this.pen = newPen == null ? null : (Pen)newPen.Clone();
      this.Brush = newBrush == null ? null : (Brush)newBrush.Clone();
      this.Bounds = newBounds;
      this.styleGroup = newStyleGroup;
      this.name = newName;
      this.elementGroup = newElementGroup;
      this.shapeDrawAction = newShapeDrawAction;
    }

    /// <summary>
    /// Initializes a new instance of the VGElement class.
    /// </summary>
    /// <param name="newShapeDrawAction">Drawing action: Edge, Fill, Both</param>
    /// <param name="newPen">Pen for edges</param>
    /// <param name="newBrush">Brush for text and fills</param>
    /// <param name="newFont">Font for text</param>
    /// <param name="newFontColor">Color for text</param>
    /// <param name="newBounds">Bounds of element</param>
    /// <param name="newStyleGroup">Group Enumeration, <see cref="VGStyleGroup"/></param>
    /// <param name="newName">Name of Element</param>
    /// <param name="newElementGroup">Element group description</param>
    /// <param name="newSound">The <see cref="AudioFile"/> to play with this element.</param>
    public VGElement(
      ShapeDrawAction newShapeDrawAction,
      Pen newPen,
      Brush newBrush,
      Font newFont,
      Color newFontColor,
      RectangleF newBounds,
      VGStyleGroup newStyleGroup,
      string newName,
      string newElementGroup,
      AudioFile newSound)
    {
      this.InitStandards();
      this.pen = newPen == null ? null : (Pen)newPen.Clone();
      this.Brush = newBrush == null ? null : (Brush)newBrush.Clone();
      this.Bounds = newBounds;
      this.styleGroup = newStyleGroup;
      this.name = newName;
      this.elementGroup = newElementGroup;
      this.Font = newFont == null ? null : (Font)newFont.Clone();
      this.FontColor = newFontColor;
      this.shapeDrawAction = newShapeDrawAction;
      if (newSound != null)
      {
        this.Sound = (AudioFile)newSound.Clone();
      }
    }

    /// <summary>
    /// Initializes a new instance of the VGElement class.
    /// </summary>
    /// <param name="newShapeDrawAction">Drawing action: Edge, Fill, Both</param>
    /// <param name="newPen">Pen for edges</param>
    public VGElement(ShapeDrawAction newShapeDrawAction, Pen newPen)
    {
      this.InitStandards();
      this.shapeDrawAction = newShapeDrawAction;
      this.pen = newPen == null ? null : (Pen)newPen.Clone();
    }

    /// <summary>
    /// Initializes a new instance of the VGElement class.
    /// </summary>
    /// <param name="newShapeDrawAction">Drawing action: Edge, Fill, Both</param>
    /// <param name="newPen">Pen for edges</param>
    /// <param name="newBrush">Brush for fills</param>
    public VGElement(ShapeDrawAction newShapeDrawAction, Pen newPen, Brush newBrush)
    {
      this.InitStandards();
      this.shapeDrawAction = newShapeDrawAction;
      this.pen = newPen == null ? null : (Pen)newPen.Clone();
      this.Brush = newBrush == null ? null : (Brush)newBrush.Clone();
    }

    /// <summary>
    /// Initializes a new instance of the VGElement class.
    /// </summary>
    /// <param name="newShapeDrawAction">Drawing action: Edge, Fill, Both</param>
    /// <param name="newPen">Pen for edges</param>
    /// <param name="newBrush">Brush for fills</param>
    /// <param name="newStyleGroup">Group Enumeration, <see cref="VGStyleGroup"/></param>
    /// <param name="newName">Name of Element</param>
    /// <param name="newElementGroup">Element group description</param>
    public VGElement(
      ShapeDrawAction newShapeDrawAction,
      Pen newPen,
      Brush newBrush,
      VGStyleGroup newStyleGroup,
      string newName,
      string newElementGroup)
    {
      this.InitStandards();
      this.styleGroup = newStyleGroup;
      this.name = newName;
      this.elementGroup = newElementGroup;
      this.shapeDrawAction = newShapeDrawAction;
      this.pen = newPen == null ? null : (Pen)newPen.Clone();
      this.Brush = newBrush == null ? null : (Brush)newBrush.Clone();
    }

    /// <summary>
    /// Initializes a new instance of the VGElement class.
    /// </summary>
    /// <param name="newShapeDrawAction">Drawing action: Edge, Fill, Both</param>
    /// <param name="newPen">Pen for edges</param>
    /// <param name="newStyleGroup">Group Enumeration, <see cref="VGStyleGroup"/></param>
    /// <param name="newName">Name of Element</param>
    /// <param name="newElementGroup">Element group description</param>
    public VGElement(
      ShapeDrawAction newShapeDrawAction,
      Pen newPen,
      VGStyleGroup newStyleGroup,
      string newName,
      string newElementGroup)
    {
      this.InitStandards();
      this.styleGroup = newStyleGroup;
      this.name = newName;
      this.elementGroup = newElementGroup;
      this.shapeDrawAction = newShapeDrawAction;
      this.pen = newPen == null ? null : (Pen)newPen.Clone();
    }

    /// <summary>
    /// Initializes a new instance of the VGElement class.
    /// </summary>
    /// <param name="newShapeDrawAction">Drawing action: Edge, Fill, Both</param>
    /// <param name="newPen">Pen for edges</param>
    /// <param name="newFont">Font for text</param>
    /// <param name="newFontColor">Color for text</param>
    /// <param name="newStyleGroup">Group Enumeration, <see cref="VGStyleGroup"/></param>
    /// <param name="newName">Name of Element</param>
    /// <param name="newElementGroup">Element group description</param>
    public VGElement(
      ShapeDrawAction newShapeDrawAction,
      Pen newPen,
      Font newFont,
      Color newFontColor,
      VGStyleGroup newStyleGroup,
      string newName,
      string newElementGroup)
    {
      this.InitStandards();
      this.styleGroup = newStyleGroup;
      this.name = newName;
      this.elementGroup = newElementGroup;
      this.Font = newFont == null ? null : (Font)newFont.Clone();
      this.FontColor = newFontColor;
      this.shapeDrawAction = newShapeDrawAction;
      this.pen = newPen == null ? null : (Pen)newPen.Clone();
    }

    /// <summary>
    /// Initializes a new instance of the VGElement class.
    /// </summary>
    /// <param name="newShapeDrawAction">Drawing action: Edge, Fill, Both</param>
    /// <param name="newBrush">Brush for text and fills</param>
    public VGElement(ShapeDrawAction newShapeDrawAction, Brush newBrush)
    {
      this.InitStandards();
      this.shapeDrawAction = newShapeDrawAction;
      this.Brush = newBrush == null ? null : (Brush)newBrush.Clone();
    }

    /// <summary>
    /// Initializes a new instance of the VGElement class.
    /// </summary>
    /// <param name="newShapeDrawAction">Drawing action: Edge, Fill, Both</param>
    /// <param name="newBrush">Brush for text and fills</param>
    /// <param name="newStyleGroup">Group Enumeration, <see cref="VGStyleGroup"/></param>
    /// <param name="newName">Name of Element</param>
    /// <param name="newElementGroup">Element group description</param>
    public VGElement(
      ShapeDrawAction newShapeDrawAction,
      Brush newBrush,
      VGStyleGroup newStyleGroup,
      string newName,
      string newElementGroup)
    {
      this.InitStandards();
      this.styleGroup = newStyleGroup;
      this.name = newName;
      this.elementGroup = newElementGroup;
      this.shapeDrawAction = newShapeDrawAction;
      this.Brush = newBrush == null ? null : (Brush)newBrush.Clone();
    }

    /// <summary>
    /// Initializes a new instance of the VGElement class.
    /// </summary>
    /// <param name="newShapeDrawAction">Drawing action: Edge, Fill, Both</param>
    /// <param name="newPen">Pen for edges</param>
    /// <param name="newBounds">Bounds of element</param>
    public VGElement(ShapeDrawAction newShapeDrawAction, Pen newPen, RectangleF newBounds)
    {
      this.InitStandards();
      this.shapeDrawAction = newShapeDrawAction;
      this.pen = newPen == null ? null : (Pen)newPen.Clone();
      this.Bounds = newBounds;
    }

    /// <summary>
    /// Initializes a new instance of the VGElement class.
    /// </summary>
    /// <param name="newShapeDrawAction">Drawing action: Edge, Fill, Both</param>
    /// <param name="newPen">Pen for edges</param>
    /// <param name="newBounds">Bounds of element</param>
    /// <param name="newStyleGroup">Group Enumeration, <see cref="VGStyleGroup"/></param>
    /// <param name="newName">Name of Element</param>
    /// <param name="newElementGroup">Element group description</param>
    public VGElement(
      ShapeDrawAction newShapeDrawAction,
      Pen newPen,
      RectangleF newBounds,
      VGStyleGroup newStyleGroup,
      string newName,
      string newElementGroup)
    {
      this.InitStandards();
      this.styleGroup = newStyleGroup;
      this.name = newName;
      this.elementGroup = newElementGroup;
      this.shapeDrawAction = newShapeDrawAction;
      this.pen = newPen == null ? null : (Pen)newPen.Clone();
      this.Bounds = newBounds;
    }

    /// <summary>
    /// Initializes a new instance of the VGElement class.
    /// </summary>
    /// <param name="newShapeDrawAction">Drawing action: Edge, Fill, Both</param>
    /// <param name="newPen">Pen for edges</param>
    /// <param name="newFont">Font for text</param>
    /// <param name="newFontColor">Color for text</param>
    /// <param name="newBounds">Bounds of element</param>
    /// <param name="newStyleGroup">Group Enumeration, <see cref="VGStyleGroup"/></param>
    /// <param name="newName">Name of Element</param>
    /// <param name="newElementGroup">Element group description</param>
    public VGElement(
      ShapeDrawAction newShapeDrawAction,
      Pen newPen,
      Font newFont,
      Color newFontColor,
      RectangleF newBounds,
      VGStyleGroup newStyleGroup,
      string newName,
      string newElementGroup)
    {
      this.InitStandards();
      this.styleGroup = newStyleGroup;
      this.name = newName;
      this.elementGroup = newElementGroup;
      this.shapeDrawAction = newShapeDrawAction;
      this.pen = newPen == null ? null : (Pen)newPen.Clone();
      this.Font = newFont == null ? null : (Font)newFont.Clone();
      this.FontColor = newFontColor;

      this.Bounds = newBounds;
    }

    /// <summary>
    /// Initializes a new instance of the VGElement class.
    /// </summary>
    /// <param name="newShapeDrawAction">Drawing action: Edge, Fill, Both</param>
    /// <param name="newBrush">Brush for text and fills</param>
    /// <param name="newBounds">Bounds of element</param>
    public VGElement(ShapeDrawAction newShapeDrawAction, Brush newBrush, RectangleF newBounds)
    {
      this.InitStandards();
      this.shapeDrawAction = newShapeDrawAction;
      this.Brush = newBrush == null ? null : (Brush)newBrush.Clone();
      this.Bounds = newBounds;
    }

    /// <summary>
    /// Initializes a new instance of the VGElement class.
    /// </summary>
    /// <param name="newShapeDrawAction">Drawing action: Edge, Fill, Both</param>
    /// <param name="newBrush">Brush for text and fills</param>
    /// <param name="newBounds">Bounds of element</param>
    /// <param name="newStyleGroup">Group Enumeration, <see cref="VGStyleGroup"/></param>
    /// <param name="newName">Name of Element</param>
    /// <param name="newElementGroup">Element group description</param>
    public VGElement(
      ShapeDrawAction newShapeDrawAction,
      Brush newBrush,
      RectangleF newBounds,
      VGStyleGroup newStyleGroup,
      string newName,
      string newElementGroup)
    {
      this.InitStandards();
      this.styleGroup = newStyleGroup;
      this.name = newName;
      this.elementGroup = newElementGroup;
      this.shapeDrawAction = newShapeDrawAction;
      this.Brush = newBrush == null ? null : (Brush)newBrush.Clone();
      this.Bounds = newBounds;
    }

    /// <summary>
    /// Initializes a new instance of the VGElement class.
    /// </summary>
    /// <param name="newShapeDrawAction">Drawing action: Edge, Fill, Both</param>
    /// <param name="newBrush">Brush for text and fills</param>
    /// <param name="newFont">Font for text</param>
    /// <param name="newFontColor">Color for text</param>
    /// <param name="newBounds">Bounds of element</param>
    /// <param name="newStyleGroup">Group Enumeration, <see cref="VGStyleGroup"/></param>
    /// <param name="newName">Name of Element</param>
    /// <param name="newElementGroup">Element group description</param>
    public VGElement(
      ShapeDrawAction newShapeDrawAction,
      Brush newBrush,
      Font newFont,
      Color newFontColor,
      RectangleF newBounds,
      VGStyleGroup newStyleGroup,
      string newName,
      string newElementGroup)
    {
      this.InitStandards();
      this.styleGroup = newStyleGroup;
      this.name = newName;
      this.elementGroup = newElementGroup;
      this.shapeDrawAction = newShapeDrawAction;
      this.Brush = newBrush == null ? null : (Brush)newBrush.Clone();
      this.Font = newFont == null ? null : (Font)newFont.Clone();
      this.FontColor = newFontColor;
      this.Bounds = newBounds;
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets a default aqua colored solid pen.
    /// </summary>
    [XmlIgnore, DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
    public static Pen DefaultPen { get; private set; }

    /// <summary>
    /// Gets a default aqua colored solid brush;
    /// </summary>
    [XmlIgnore, DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
    public static Brush DefaultBrush { get; private set; }

    /// <summary>
    /// Gets a default font;
    /// </summary>
    [XmlIgnore, DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
    public static Font DefaultFont { get; private set; }

    /// <summary>
    /// Gets a default aqua color for text
    /// </summary>
    [XmlIgnore, DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
    public static Color DefaultFontColor { get; private set; }

    /// <summary>
    /// Gets or sets name of graphic element
    /// </summary>
    /// <value>A <see cref="string"/> with the name of this element,
    /// can be empty.</value>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Category("Description")]
    [Description("A unique identifier for that shape")]
    public string Name
    {
      get { return this.name; }
      set { this.name = value; }
    }

    /// <summary>
    /// Gets or sets group enumeration of graphic element for setting style properties.
    /// </summary>
    /// <value>A <see cref="VGStyleGroup"/> for this element,
    /// that can categorize multiple elements into one design group,
    /// can be <see cref="VGStyleGroup.None"/>.</value>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Category("Description")]
    [Description("The vector graphics style group this shape belongs to.")]
    public VGStyleGroup StyleGroup
    {
      get { return this.styleGroup; }
      set { this.styleGroup = value; }
    }

    /// <summary>
    /// Gets or sets group description of graphic element
    /// </summary>
    /// <value>A <see cref="String"/> for this element,
    /// that can categorize multiple elements into one group.</value>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Category("Description")]
    [Description("The vector graphics group this shape belongs to.")]
    public string ElementGroup
    {
      get { return this.elementGroup; }
      set { this.elementGroup = value; }
    }

    /// <summary>
    /// Gets or sets used newPen for graphic element
    /// </summary>
    /// <value>A <see cref="Pen"/> for drawing the bounds of this shape.</value>
    [XmlIgnoreAttribute]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Category("Appearance")]
    [Description("The newPen to use for the bounding line.")]
    [TypeConverter(typeof(PenConverter))]
    public virtual Pen Pen
    {
      get { return this.pen; }
      set { this.pen = value; }
    }

    /// <summary>
    /// Gets or sets the SerializedPen.
    /// Serializes the 'Pen' Pen to XML.
    /// </summary>
    /// <value>A <see cref="string"/> with the string representation of the shapes newPen.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string SerializedPen
    {
      get { return ObjectStringConverter.PenToString(this.pen); }
      set { this.pen = ObjectStringConverter.StringToPen(value); }
    }

    /// <summary>
    /// Gets or sets used newBrush for graphic element
    /// </summary>
    /// <value>A <see cref="Brush"/> for drawing the interior of this shape.</value>
    [XmlIgnore, DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Category("Appearance"), Description("The newBrush to use for the interior of this shape if it is in fill mode."), TypeConverter(typeof(BrushConverter))]
    public Brush Brush { get; set; }

    /// <summary>
    /// Gets or sets the SerializedBrush.
    /// Serializes the 'Brush' Brush to XML.
    /// </summary>
    /// <value>A <see cref="string"/> with the string representation of the shapes newBrush.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string SerializedBrush
    {
      get { return ObjectStringConverter.BrushToString(this.Brush); }
      set { this.Brush = ObjectStringConverter.StringToBrush(value); }
    }

    /// <summary>
    /// Gets or sets newFont of text element
    /// </summary>
    /// <value>A <see cref="Font"/> for drawing the name of this shape.</value>
    [XmlIgnore, DesignerSerializationVisibility(DesignerSerializationVisibility.Visible), Category("Appearance"), Description("The newFont to use for the text to draw the name of this shape.")]
    public Font Font { get; set; }

    /// <summary>
    /// Gets or sets the SerializedFont.
    /// Serializes the <see cref="Font"/> property to XML.
    /// </summary>
    /// <value>A <see cref="string"/> with the string representation of the shapes newFont.</value>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public string SerializedFont
    {
      get
      {
        if (this.Font != null)
        {
          TypeConverter fontConverter = TypeDescriptor.GetConverter(typeof(Font));
          return fontConverter.ConvertToInvariantString(this.Font);
        }
        else
        {
          return null;
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
              this.Font = ObjectStringConverter.StringToFontOld(value);
            }
            else
            {
              TypeConverter fontConverter = TypeDescriptor.GetConverter(typeof(Font));
              this.Font = (Font)fontConverter.ConvertFromInvariantString(value);
            }
          }
          catch (ArgumentException)
          {
            // Paranoia check for very old versions.
            this.Font = new Font(SystemFonts.MenuFont.FontFamily, 28f, GraphicsUnit.Point);
          }
        }
        else
        {
          this.Font = null;
        }
      }
    }

    /// <summary>
    /// Gets or sets used color for the graphic elements newFont.
    /// </summary>
    /// <value>A <see cref="Color"/> for the newFont of this shape.</value>
    [XmlIgnore, DesignerSerializationVisibility(DesignerSerializationVisibility.Visible), Category("Appearance"), Description("The newFont color to use for the text to draw the name of this shape.")]
    public Color FontColor { get; set; }

    /// <summary>
    /// Gets or sets the SerializedFontColor.
    /// Serializes and deserializes the <see cref="FontColor"/> to XML,
    /// because XMLSerializer can not serialize <see cref="Color"/> values.
    /// </summary>
    /// <value>A <see cref="string"/> with the string representation of the shapes newFont color.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string SerializedFontColor
    {
      get { return ObjectStringConverter.ColorToHtmlAlpha(this.FontColor); }
      set { this.FontColor = ObjectStringConverter.HtmlAlphaToColor(value); }
    }

    /// <summary>
    /// Gets or sets shape drawing mode enumeration, edge, fill or both
    /// </summary>
    /// <value>The <see cref="ShapeDrawAction"/> of this shape.</value>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Category("Appearance")]
    [Description("A value what to draw, can be edge or fill or both.")]
    public virtual ShapeDrawAction ShapeDrawAction
    {
      get { return this.shapeDrawAction; }
      set { this.shapeDrawAction = value; }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the shape is in edit mode.
    /// Set to true, if shape should be resizable and drawn with grab handles
    /// </summary>
    /// <value>A <see cref="bool"/> which is <strong>true</strong>,
    /// if shape should be resizable and drawn with grab handles, otherwise
    /// <strong>false</strong>.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [XmlIgnoreAttribute]
    public bool IsInEditMode
    {
      get
      {
        return this.isInEditMode;
      }

      set
      {
        this.isInEditMode = value;

        // Adds or removes grab handles depending on edit mode
        if (this.isInEditMode)
        {
          this.AddGrabHandles();
        }
        else
        {
          this.RemoveGrabHandles();
        }
      }
    }

    /// <summary>
    /// Gets the bounding rectangle including the newPen width.
    /// </summary>
    [XmlIgnoreAttribute]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public virtual RectangleF BigBounds
    {
      get
      {
        RectangleF bigbounds = this.Bounds;
        bigbounds.Inflate(GrabHandle.HANDLESIZE + 2, GrabHandle.HANDLESIZE + 2);
        if (bigbounds.Left < 0)
        {
          bigbounds.X = 0;
        }

        if (bigbounds.Top < 0)
        {
          bigbounds.Y = 0;
        }

        return bigbounds;
      }
    }

    /// <summary>
    /// Gets or sets bounding rectangle. Invokes NewPosition method when setting bounds.
    /// </summary>
    /// <value>A <see cref="RectangleF"/> with the rectangular bounds
    /// of this shape.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [XmlIgnoreAttribute]
    public virtual RectangleF Bounds
    {
      get
      {
        return new RectangleF(this.location, this.size);
      }

      set
      {
        PointF newLocation = new PointF(value.Left, value.Top);
        Matrix mx = this.CalcTranslationMatrix(newLocation, this.location);
        this.NewPosition(mx);
        this.location = newLocation;
        this.size.Width = value.Width;
        this.size.Height = value.Height;

        if (this.isInEditMode)
        {
          this.AddGrabHandles();
        }
      }
    }

    /// <summary>
    /// Gets or sets center of graphics element
    /// </summary>
    /// <value>A <see cref="PointF"/> with the center of this shape.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [XmlIgnoreAttribute]
    public virtual PointF Center
    {
      get
      {
        return new PointF(
          this.location.X + this.size.Width / 2,
          this.location.Y + this.size.Height / 2);
      }

      set
      {
        PointF newLocation = new PointF(
          value.X - this.size.Width / 2,
          value.Y - this.size.Height / 2);

        Matrix mx = this.CalcTranslationMatrix(newLocation, this.location);
        this.NewPosition(mx);

        this.location = newLocation;

        if (this.isInEditMode)
        {
          this.AddGrabHandles();
        }
      }
    }

    /// <summary>
    /// Gets or sets size of element.
    /// </summary>
    /// <value>A <see cref="SizeF"/> with the size of this shape.</value>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Category("Layout")]
    [Description("The size of this shape.")]
    public virtual SizeF Size
    {
      get
      {
        return this.size;
      }

      set
      {
        this.size = value;
        if (this.isInEditMode)
        {
          this.AddGrabHandles();
        }
      }
    }

    /// <summary>
    /// Gets height of element.
    /// </summary>
    /// <value>A <see cref="Single"/> with the height of this shape.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [XmlIgnoreAttribute]
    public float Height
    {
      get { return this.size.Height; }
    }

    /// <summary>
    /// Gets width of element.
    /// </summary>
    /// <value>A <see cref="Single"/> with the width of this shape.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [XmlIgnoreAttribute]
    public float Width
    {
      get { return this.size.Width; }
    }

    /// <summary>
    /// Gets or sets upper left corner of graphic element
    /// </summary>
    /// <value>A <see cref="PointF"/> with the location of this shape.</value>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Category("Layout")]
    [Description("The top left corner of this shape.")]
    [TypeConverter(typeof(PointFConverter))]
    public PointF Location
    {
      get
      {
        return this.location;
      }

      set
      {
        Matrix mx = this.CalcTranslationMatrix(value, this.location);
        this.NewPosition(mx);
        this.location = value;

        if (this.isInEditMode)
        {
          this.AddGrabHandles();
        }
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the visibility of the graphic object.
    /// If set to false, object will not be drawn in paint method.
    /// </summary>
    /// <value>A <see cref="bool"/> which is <strong>true</strong>,
    /// if shape should be be drawn during OnPaint, otherwise
    /// <strong>false</strong>.</value>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible), Category("Appearance"), Description("Flag. True, if shape should be visible.")]
    public bool Visible { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the object is modified.
    /// If set to true, object will saved into database upon mouse up event.
    /// </summary>
    /// <value>A <see cref="Boolean"/> which is <strong>true</strong>,
    /// if shapes properties were modified since last serialization, otherwise
    /// <strong>false</strong>.</value>
    [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), XmlIgnore]
    public bool Modified { get; set; }

    /// <summary>
    /// Gets or sets the list of grab handles for the current graphic element.
    /// Used for iterating in mouse over and redrawing
    /// </summary>
    /// <value>A <see cref="List{GrabHandle}"/> with the
    /// adornments of this shape.</value>
    [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), XmlIgnore]
    public List<GrabHandle> GrabHandles { get; set; }

    /// <summary>
    /// Gets or sets the modifier keys from the parent picture.
    /// </summary>
    /// <value>A <see cref="Keys"/> with the modifier keys of the
    /// owning picture.</value>
    [XmlIgnore, Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Keys ModifierKeys { get; set; }

    /// <summary>
    /// Gets or sets the modifier keys from the parent picture.
    /// </summary>
    /// <value>A <see cref="string"/> with the serializable representation 
    /// of the modifier keys of the owning picture.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string SerializableModifierKeys
    {
      get { return this.ModifierKeys.ToString(); }
      set { this.ModifierKeys = (Keys)Enum.Parse(typeof(Keys), value); }
    }

    /// <summary>
    /// Gets or sets an <see cref="AudioFile"/> for the sound
    /// to be played when this <see cref="VGElement"/> is displayed.
    /// </summary>
    [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public AudioFile Sound { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="VGAlignment"/> for the name position.
    /// </summary>
    [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public VGAlignment TextAlignment { get; set; }

    /// <summary>
    /// Gets or sets a (trial) time at which this element is first displayed.
    /// </summary>
    [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public long OnsetTime { get; set; }

    /// <summary>
    /// Gets or sets a (trial) time at which this element has been updated for the last time.
    /// </summary>
    [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public long EndTime { get; set; }

    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Public methods                                                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region PUBLICMETHODS

    /// <summary>
    /// This static method serializes the current <see cref="VGElement"/>
    /// to a memorystream which itself is converted to a base64 string.
    /// This is needed for lossless copying and retreiving from clipboard.
    /// </summary>
    /// <param name="objectToSerialize">The <see cref="VGElement"/>
    /// to be serialized.</param>
    /// <returns>A base64 encoded string with the memory stream of the 
    /// <see cref="VGElement"/>.</returns>
    public static string Serialize(VGElement objectToSerialize)
    {
      string serialString = null;
      using (System.IO.MemoryStream ms1 = new System.IO.MemoryStream())
      {
        XmlSerializer serializer = new XmlSerializer(typeof(VGElement));
        serializer.Serialize(ms1, objectToSerialize);
        byte[] arrayByte = ms1.ToArray();
        serialString = Convert.ToBase64String(arrayByte);
      }

      return serialString;
    }

    /// <summary>
    /// This static method deserializes a <see cref="VGElement"/>
    /// from a base64 encoded memorystream string.
    /// This is needed for lossless copying and retreiving from clipboard.
    /// </summary>
    /// <param name="serializationString">A base64 encoded string with the memory stream of the 
    /// <see cref="VGElement"/> to deserialize.</param>
    /// <returns>The encoded <see cref="VGElement"/>. or null if conversion failed.</returns>
    public static VGElement Deserialize(string serializationString)
    {
      VGElement deserialObject = null;
      try
      {
        byte[] arrayByte = Convert.FromBase64String(serializationString);
        using (System.IO.MemoryStream ms1 = new System.IO.MemoryStream(arrayByte))
        {
          XmlSerializer serializer = new XmlSerializer(typeof(VGElement));
          deserialObject = (VGElement)serializer.Deserialize(ms1);
        }
      }
      catch (Exception)
      {
        // it is ok to catch everything we just return null if 
        // we failed.
      }

      return deserialObject;
    }

    #endregion //PUBLICMETHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Inherited methods                                                         //
    ///////////////////////////////////////////////////////////////////////////////
    #region OVERRIDES

    /// <summary>
    /// Virtual. An override should draw the element. 
    /// This base implementation draws the name, selection frame grab handles,
    /// if applicable.
    /// </summary>
    /// <param name="graphics">Graphics to draw to</param>
    /// <exception cref="ArgumentNullException">Thrown, when graphics object is null.</exception>
    public virtual void Draw(Graphics graphics)
    {
      if (graphics == null)
      {
        throw new ArgumentNullException("Graphics object should not be null.");
      }

      // Draw Graphic element name
      if (((this.shapeDrawAction & ShapeDrawAction.Name) == ShapeDrawAction.Name) && (this.name != string.Empty))
      {
        graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

        if (this.Size.IsEmpty && this is VGPolyline)
        {
          VGPolyline poly = (VGPolyline)this;
          poly.RecalculateBounds();
        }

        SizeF sizeText = graphics.MeasureString(this.name, this.Font);
        PointF textTopLeft = new PointF();
        StringFormat sf = new StringFormat();
        switch (this.TextAlignment)
        {
          case VGAlignment.Bottom:
            textTopLeft.X = this.Center.X - sizeText.Width / 2;
            textTopLeft.Y = this.Bounds.Bottom;
            graphics.DrawString(this.name, this.Font, new SolidBrush(this.FontColor), textTopLeft, sf);
            break;
          case VGAlignment.None:
          case VGAlignment.Center:
            textTopLeft.X = this.Center.X - sizeText.Width / 2;
            textTopLeft.Y = this.Center.Y - sizeText.Height / 2;
            graphics.DrawString(this.name, this.Font, new SolidBrush(this.FontColor), textTopLeft, sf);
            break;
          case VGAlignment.Left:
            textTopLeft.X = this.Location.X;
            textTopLeft.Y = this.Center.Y + sizeText.Width / 2;
            sf.FormatFlags = StringFormatFlags.DirectionVertical;

            // Save graphics state.
            graphics.RotateTransform(180);
            graphics.DrawString(
              this.name,
              this.Font,
              new SolidBrush(this.FontColor),
              -textTopLeft.X,
              -textTopLeft.Y,
              sf);
            graphics.RotateTransform(-180);
            break;
          case VGAlignment.Right:
            textTopLeft.X = this.Bounds.Right;
            textTopLeft.Y = this.Center.Y - sizeText.Width / 2;
            sf.FormatFlags = StringFormatFlags.DirectionVertical;
            graphics.DrawString(this.name, this.Font, new SolidBrush(this.FontColor), textTopLeft, sf);
            break;
          case VGAlignment.Top:
            textTopLeft.X = this.Center.X - sizeText.Width / 2;
            textTopLeft.Y = this.Location.Y - sizeText.Height;
            graphics.DrawString(this.name, this.Font, new SolidBrush(this.FontColor), textTopLeft, sf);
            break;
        }
      }

      if (this.isInEditMode)
      {
        Rectangle outRect = this.GetSelectionFrameBounds();
        Rectangle inRect = outRect;
        inRect.Inflate(-GrabHandle.HANDLESIZE, -GrabHandle.HANDLESIZE);
        ControlPaint.DrawSelectionFrame(graphics, true, outRect, inRect, Color.Blue);

        this.DrawHandles(graphics);
      }
    }

    /// <summary>
    /// Virtual. An override should reinitialze the grab handles of the element. 
    /// Resets bounds of the graphic element according to the movement of the given grab handle
    /// </summary>
    /// <param name="handle">GrabHandle that moved</param>
    /// <param name="handleMovement">Movement in stimulus coordinates</param>
    public virtual void GrabHandleMoved(ref GrabHandle handle, Point handleMovement)
    {
      switch (handle.GrabHandlePosition)
      {
        case GrabHandle.HandlePosition.Top:
          this.Bounds = new RectangleF(
            this.Location.X,
            this.Location.Y - handleMovement.Y,
            this.Bounds.Width,
            this.Bounds.Height + handleMovement.Y);
          break;
        case GrabHandle.HandlePosition.Down:
          this.Bounds = new RectangleF(
            this.Location.X,
            this.Location.Y,
            this.Bounds.Width,
            this.Bounds.Height - handleMovement.Y);
          break;
        case GrabHandle.HandlePosition.Left:
          this.Bounds = new RectangleF(
            this.Location.X - handleMovement.X,
            this.Location.Y,
            this.Bounds.Width + handleMovement.X,
            this.Bounds.Height);
          break;
        case GrabHandle.HandlePosition.Right:
          this.Bounds = new RectangleF(
            this.Location.X,
            this.Location.Y,
            this.Bounds.Width - handleMovement.X,
            this.Bounds.Height);
          break;
        case GrabHandle.HandlePosition.Center:
          PointF newCenter = new PointF(
            this.Center.X - handleMovement.X,
            this.Center.Y - handleMovement.Y);
          this.Center = newCenter;
          break;
        case GrabHandle.HandlePosition.TopLeft:
          this.Bounds = new RectangleF(
            this.Location.X - handleMovement.X,
            this.Location.Y - handleMovement.Y,
            this.Bounds.Width + handleMovement.X,
            this.Bounds.Height + handleMovement.Y);
          break;
        case GrabHandle.HandlePosition.TopRight:
          this.Bounds = new RectangleF(
            this.Location.X,
            this.Location.Y - handleMovement.Y,
            this.Bounds.Width - handleMovement.X,
            this.Bounds.Height + handleMovement.Y);
          break;
        case GrabHandle.HandlePosition.BottomLeft:
          this.Bounds = new RectangleF(
            this.Location.X - handleMovement.X,
            this.Location.Y,
            this.Bounds.Width + handleMovement.X,
            this.Bounds.Height - handleMovement.Y);
          break;
        case GrabHandle.HandlePosition.BottomRight:
          this.Bounds = new RectangleF(
            this.Location.X,
            this.Location.Y,
            this.Bounds.Width - handleMovement.X,
            this.Bounds.Height - handleMovement.Y);
          break;
      }

      this.AddGrabHandles();
      this.Modified = true;
    }

    /// <summary>
    /// Virtual. An override should calculate wheter given point is in shape region. 
    /// Detects if given point is in element region.
    /// </summary>
    /// <param name="pt">Check Point</param>
    /// <returns><strong>True</strong> if point is in region, otherwise <strong>false</strong></returns>
    public virtual bool Contains(PointF pt)
    {
      RectangleF bigBounds = this.Bounds;
      if (this.isInEditMode)
      {
        bigBounds.Inflate(GrabHandle.HANDLESIZE, GrabHandle.HANDLESIZE);
      }

      return bigBounds.Contains(pt);
    }

    /// <summary>
    /// Virtual. An override should calculate whether given point is in shape region,
    /// including the tolerance widened area.
    /// Detects if given point is in element region widened by the tolerance value.
    /// </summary>
    /// <param name="pt">Point to check.</param>
    /// <param name="tolerance">An <see cref="int"/> tolerance value for 
    /// widening areas of interest to get a better hit rate in pixel.</param>
    /// <returns><strong>True</strong> if point is in region, otherwise <strong>false</strong></returns>
    public virtual bool Contains(PointF pt, int tolerance)
    {
      RectangleF bigBounds = this.Bounds;
      bigBounds.Inflate(tolerance, tolerance);

      return bigBounds.Contains(pt);
    }

    /// <summary>
    /// Virtual. An override should return all points
    /// that constitute this shape.
    /// This base class implementation returns the corner points of the bounding rectangle.
    /// </summary>
    /// <returns>The points of the bounding rectangle of this shape.</returns>
    public virtual List<PointF> GetPoints()
    {
      List<PointF> pointList = new List<PointF>();
      pointList.Add(this.Location);
      PointF upperRight = new PointF(this.Location.X + this.Size.Width, this.Location.Y);
      pointList.Add(upperRight);
      pointList.Add(PointF.Add(this.Location, this.Size));
      PointF lowerLeft = new PointF(this.Location.X, this.Location.Y + this.Size.Height);
      pointList.Add(lowerLeft);
      return pointList;
    }

    /// <summary>
    /// Virtual. An override should return the number of points of this shape.
    /// This base class implementation returns 4.
    /// </summary>
    /// <returns>The actual number of point of that shape.</returns>
    public virtual int GetPointCount()
    {
      return 4;
    }

    /// <summary>
    /// Virtual. An override should reset this shape to default values.
    /// This base class implementation resets the base class fields.
    /// </summary>
    public virtual void Reset()
    {
      this.TextAlignment = VGAlignment.Center;
      this.Brush = DefaultBrush;
      this.Font = DefaultFont;
      this.FontColor = DefaultFontColor;
      this.Visible = true;
      this.GrabHandles.Clear();
      this.styleGroup = VGStyleGroup.None;
      this.isInEditMode = false;
      this.Modified = false;
      this.location = new PointF(0, 0);
      this.ModifierKeys = Keys.None;
      this.pen = DefaultPen;
      this.shapeDrawAction = ShapeDrawAction.Edge;
      this.size = new SizeF(100, 100);
      this.name = string.Empty;
    }

    /// <summary>
    /// Overridden <see cref="Object.ToString()"/> method.
    /// Returns a <see cref="string"/> that represents the current <see cref="VGElement"/>. 
    /// </summary>
    /// <returns>A <see cref="string"/> that represents the 
    /// current <see cref="VGElement"/></returns>
    public override string ToString()
    {
      return "VGElement";
    }

    /// <summary>
    /// Returns a <see cref="string"/> that represents the current 
    /// <see cref="VGElement"/> in short form.
    /// </summary>
    /// <returns>A <see cref="string"/> that represents the 
    /// current <see cref="VGElement"/> in short form with its main properties.</returns>
    public virtual string ToShortString()
    {
      return "VGElement";
    }

    /// <summary>
    /// Determines whether two <see cref="VGElement"/> instances are equal.
    /// </summary>
    /// <param name="obj">The <see cref="Object"/> to compare with the current 
    /// <see cref="VGElement"/>.</param>
    /// <returns><strong>True</strong> if the specified Object is equal 
    /// to the current <see cref="VGElement"/>; otherwise, <strong>false</strong>. </returns>
    public override bool Equals(object obj)
    {
      if (obj is VGElement)
      {
        VGElement element = (VGElement)obj;
        if (element.Name == this.Name && element.GetType() == this.GetType()
          && element.Bounds == this.Bounds)
        {
          return true;
        }
      }

      return false;
    }

    /// <summary>
    /// Serves as a hash function for a particular type. 
    /// <strong>GetHashCode</strong> is suitable for use in hashing algorithms 
    /// and data structures like a hash table. 
    /// It needs to be overridden when <see cref="Object.Equals(object)"/>
    /// is overriden.
    /// </summary>
    /// <returns>An <see cref="int"/> with the
    /// hash code for the current <see cref="VGElement"/>. </returns>
    public override int GetHashCode()
    {
      return this.Name.GetHashCode() ^ this.Bounds.GetHashCode() ^ this.GetType().GetHashCode();
    }

    /// <summary>
    /// Releases the resources used by the element.
    /// </summary>
    public virtual void Dispose()
    {
      if (this.GrabHandles != null)
      {
        this.GrabHandles.Clear();
      }
    }

    /// <summary>
    /// Creates a excact copy of given graphic element
    /// </summary>
    /// <returns>Excact copy of this graphic element</returns>
    public object Clone()
    {
      return this.CloneCore();
    }

    /// <summary>
    /// Updates the location and size of the element without raising
    /// the NewPosition method.
    /// </summary>
    /// <remarks>Used in the <see cref="VGPolyline"/> method 
    /// <see cref="VGPolyline.RecalculateBounds()"/></remarks>
    /// <param name="bounds">New bounds.</param>
    protected void UpdateBoundsWithoutRaisingNewPosition(RectangleF bounds)
    {
      this.location = bounds.Location;
      this.size = bounds.Size;

      if (this.isInEditMode)
      {
        this.AddGrabHandles();
      }
    }

    /// <summary>
    /// Virtual. An override should recalculate 
    /// member coordinates, that are not recalulated in the base class (bounds).
    /// Invoked, when new bounds are set.
    /// </summary>
    /// <param name="translationMatrix">Translation Matrix, that performs the translation.</param>
    protected virtual void NewPosition(Matrix translationMatrix)
    {
    }

    /// <summary>
    /// Virtual. An override should add specialized grab handles 
    /// depending on shape form.
    /// This base class implementation adds all 7 grab handles of the bounding rectangle.
    /// </summary>
    /// <remarks>Overrides should call 
    /// <see cref="AddGrabHandles(Boolean,Boolean,Boolean,Boolean,Boolean,Boolean,Boolean,Boolean,Boolean)"/>
    /// </remarks>
    protected virtual void AddGrabHandles()
    {
      this.AddGrabHandles(true, true, true, true, true, true, true, true, true);
    }

    /// <summary>
    /// Abstract. Should create a excact copy of the given graphic element
    /// </summary>
    /// <returns>Excact copy of this graphic element</returns>
    protected abstract VGElement CloneCore();

    /// <summary>
    /// Removes all grab handles from the list.
    /// </summary>
    protected void RemoveGrabHandles()
    {
      if (this.GrabHandles != null)
      {
        this.GrabHandles.Clear();
      }
    }

    /// <summary>
    /// Adds a box with up to 8 grab handles around the rectangle.
    /// </summary>
    /// <param name="center">True, if center should me movable and shown with a grab handle.</param>
    /// <param name="topLeft">True, if top left corner should me movable and shown with a grab handle.</param>
    /// <param name="topMiddle">True, if top middle corner should me movable and shown with a grab handle.</param>
    /// <param name="topRight">True, if top right corner should me movable and shown with a grab handle.</param>
    /// <param name="middleLeft">True, if middle left corner should me movable and shown with a grab handle.</param>
    /// <param name="middleRight">True, if middle right corner should me movable and shown with a grab handle.</param>
    /// <param name="bottomLeft">True, if bottom left corner should me movable and shown with a grab handle.</param>
    /// <param name="bottomMiddle">True, if bottom middle corner should me movable and shown with a grab handle.</param>
    /// <param name="bottomRight">True, if bottom right corner should me movable and shown with a grab handle.</param>
    protected void AddGrabHandles(
      bool center,
      bool topLeft,
      bool topMiddle,
      bool topRight,
      bool middleLeft,
      bool middleRight,
      bool bottomLeft,
      bool bottomMiddle,
      bool bottomRight)
    {
      if (this.GrabHandles == null)
      {
        // Create list if has not been created yet.
        this.GrabHandles = new List<GrabHandle>();
      }
      else
      {
        // Clear eventually existing grab handles
        this.GrabHandles.Clear();
      }

      Rectangle rectBounds = this.GetSelectionFrameBounds();

      // Create grab handles in respect to given parameters and add them to the grab handle list.
      if (center)
      {
        Point location = new Point(
          rectBounds.X + rectBounds.Width / 2 - GrabHandle.HANDLESIZE / 2,
          rectBounds.Y + rectBounds.Height / 2 - GrabHandle.HANDLESIZE / 2);

        GrabHandle ctr = new GrabHandle(
          location,
          Cursors.SizeAll,
          GrabHandle.HandlePosition.Center);
        this.GrabHandles.Add(ctr);
      }

      if (topLeft)
      {
        GrabHandle tl = new GrabHandle(
          new Point(rectBounds.X, rectBounds.Y),
          Cursors.SizeNWSE,
          GrabHandle.HandlePosition.TopLeft);
        this.GrabHandles.Add(tl);
      }

      if (topMiddle)
      {
        GrabHandle tm = new GrabHandle(
          new Point(rectBounds.X + rectBounds.Width / 2 - GrabHandle.HANDLESIZE / 2, rectBounds.Y),
          Cursors.SizeNS,
          GrabHandle.HandlePosition.Top);
        this.GrabHandles.Add(tm);
      }

      if (topRight)
      {
        GrabHandle tr = new GrabHandle(
          new Point(rectBounds.X + rectBounds.Width - GrabHandle.HANDLESIZE, rectBounds.Y),
          Cursors.SizeNESW,
          GrabHandle.HandlePosition.TopRight);
        this.GrabHandles.Add(tr);
      }

      if (middleLeft)
      {
        GrabHandle ml = new GrabHandle(
          new Point(rectBounds.X, rectBounds.Y + rectBounds.Height / 2 - GrabHandle.HANDLESIZE / 2),
          Cursors.SizeWE,
          GrabHandle.HandlePosition.Left);
        this.GrabHandles.Add(ml);
      }

      if (middleRight)
      {
        Point location = new Point(
          rectBounds.X + rectBounds.Width - GrabHandle.HANDLESIZE,
          rectBounds.Y + rectBounds.Height / 2 - GrabHandle.HANDLESIZE / 2);
        GrabHandle mr = new GrabHandle(
          location,
          Cursors.SizeWE,
          GrabHandle.HandlePosition.Right);
        this.GrabHandles.Add(mr);
      }

      if (bottomLeft)
      {
        GrabHandle bl = new GrabHandle(
          new Point(rectBounds.X, rectBounds.Y + rectBounds.Height - GrabHandle.HANDLESIZE),
          Cursors.SizeNESW,
          GrabHandle.HandlePosition.BottomLeft);
        this.GrabHandles.Add(bl);
      }

      if (bottomMiddle)
      {
        Point location = new Point(
          rectBounds.X + rectBounds.Width / 2 - GrabHandle.HANDLESIZE / 2,
          rectBounds.Y + rectBounds.Height - GrabHandle.HANDLESIZE);
        GrabHandle bm = new GrabHandle(
          location,
          Cursors.SizeNS,
          GrabHandle.HandlePosition.Down);
        this.GrabHandles.Add(bm);
      }

      if (bottomRight)
      {
        Point location = new Point(
          rectBounds.X + rectBounds.Width - GrabHandle.HANDLESIZE,
          rectBounds.Y + rectBounds.Height - GrabHandle.HANDLESIZE);
        GrabHandle br = new GrabHandle(
          location,
          Cursors.SizeNWSE,
          GrabHandle.HandlePosition.BottomRight);
        this.GrabHandles.Add(br);
      }
    }

    /// <summary>
    /// Draws grab handles on reference points of that shape
    /// </summary>
    /// <param name="graphics">Graphics to draw to</param>
    /// <exception cref="ArgumentNullException">Thrown, when graphics object is null.</exception>
    protected void DrawHandles(Graphics graphics)
    {
      if (graphics == null)
      {
        throw new ArgumentNullException("Graphics object should not be null.");
      }

      foreach (GrabHandle handle in this.GrabHandles)
      {
        handle.Draw(graphics);
      }
    }

    /// <summary>
    /// If the drawing styles are set, draws the fill 
    /// and the the edge into the inner bounds of this element.
    /// </summary>
    /// <param name="graphics">The <see cref="Graphics"/> to draw to.</param>
    /// <returns>A <see cref="RectangleF"/> with the calculated
    /// inner bounds of the element (This is the bounds minus the
    /// pen sized edge, if it is drawn.</returns>
    protected RectangleF DrawFillAndEdge(Graphics graphics)
    {
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
        RectangleF halfInlineRect = this.Bounds;
        halfInlineRect.Inflate(-this.Pen.Width / 2, -this.Pen.Width / 2);

        graphics.DrawRectangle(this.Pen, halfInlineRect.X, halfInlineRect.Y, halfInlineRect.Width, halfInlineRect.Height);
      }

      return innerBounds;
    }

    #endregion //OVERRIDES

    ///////////////////////////////////////////////////////////////////////////////
    // Methods for doing main class job                                          //
    ///////////////////////////////////////////////////////////////////////////////
    #region METHODS

    /// <summary>
    /// This method returns the outline bounds of the selection frame
    /// for this element, including calculation of the newPen thickness.
    /// </summary>
    /// <returns>A <see cref="Rectangle"/> with the bounds of the
    /// selection frame to be used in ControlPaint.DrawSelectionFrame(...)</returns>
    private Rectangle GetSelectionFrameBounds()
    {
      RectangleF rectBounds = this.Bounds;
      rectBounds.Inflate(GrabHandle.HANDLESIZE, GrabHandle.HANDLESIZE);
      return Rectangle.Round(rectBounds);
    }

    /// <summary>
    /// Set standard values for members variables.
    /// </summary>
    private void InitStandards()
    {
      this.TextAlignment = VGAlignment.Center;
      this.Visible = true;
      this.Brush = (Brush)VGElement.DefaultBrush.Clone();
      this.pen = (Pen)VGElement.DefaultPen.Clone();
      this.Font = (Font)VGElement.DefaultFont.Clone();
      this.FontColor = VGElement.DefaultFontColor;
      this.styleGroup = VGStyleGroup.None;
      this.name = string.Empty;
      this.GrabHandles = new List<GrabHandle>();
      this.location = PointF.Empty;
      this.size = SizeF.Empty;
      this.Sound = new AudioFile();
    }

    #endregion //METHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER

    /// <summary>
    /// Calulates translation matrix for given new top,left corner
    /// </summary>
    /// <param name="topLeftNew">A <see cref="PointF"/> with the new coordinate of new upper left corner.</param>
    /// <param name="topLeftOld">A <see cref="PointF"/> with the old coordinate of new upper left corner.</param>
    /// <returns>A <see cref="Matrix"/> with the translation matrix.</returns>
    private Matrix CalcTranslationMatrix(PointF topLeftNew, PointF topLeftOld)
    {
      Matrix transMatrix = new Matrix();
      transMatrix.Translate(topLeftNew.X - topLeftOld.X, topLeftNew.Y - topLeftOld.Y);
      return transMatrix;
    }

    #endregion //HELPER
  }
}
