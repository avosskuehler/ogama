// <copyright file="VGArrow.cs" company="FU Berlin">
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
  using System.Text;
  using System.Windows.Forms;
  using System.Xml.Serialization;

  using VectorGraphics.Tools;
  using VectorGraphics.Tools.CustomTypeConverter;

  /// <summary>
  /// Inherited from <see cref="VGElement"/>. 
  /// A serializable class that is a vector graphics arrow drawn with a specific pen.
  /// </summary>
  [Serializable]
  public class VGArrow : VGElement
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
    /// Saves the starting point of the arrow.
    /// </summary>
    private PointF firstPoint;

    /// <summary>
    /// Saves the ending point of the arrow.
    /// </summary>
    private PointF secondPoint;

    /// <summary>
    /// The distance of the arrows tip from the 
    /// first point of the connecting line.
    /// </summary>
    private float firstPointDistance;

    /// <summary>
    /// The distance of the arrows tip from the 
    /// second point of the connecting line.
    /// </summary>
    private float secondPointDistance;

    /// <summary>
    /// The weight of the arrows tip at the 
    /// first point of the connecting line.
    /// </summary>
    private float firstPointWeight;

    /// <summary>
    /// The weight of the arrows tip at the 
    /// second point of the connecting line.
    /// </summary>
    private float secondPointWeight;

    /// <summary>
    /// A <see cref="String"/> with the formatting
    /// string for the weights.
    /// </summary>
    private string formatString;

    /// <summary>
    /// A <see cref="String"/> with an additional unit for the weights
    /// </summary>
    private string addOnString;

    /// <summary>
    /// The <see cref="Font"/> for the weight values.
    /// </summary>
    private Font weightFont;

    /// <summary>
    /// The <see cref="Color"/> for the weight values font.
    /// </summary>
    private Color weightFontColor;

    /// <summary>
    /// This factor scales the arrow sizes.
    /// </summary>
    private float scaleFactor;

    /// <summary>
    /// Indicates, whether both points are set.
    /// </summary>
    private bool pointsAreSet;

    /// <summary>
    /// Indicates whether to show or hide the
    /// weight values.
    /// </summary>
    private bool hideWeights;

    #endregion FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the VGArrow class.
    /// </summary>
    /// <param name="newShapeDrawAction">Drawing action: Edge, Fill, Both</param>
    /// <param name="newPen">Pen for edges</param>
    public VGArrow(ShapeDrawAction newShapeDrawAction, Pen newPen)
      : base(newShapeDrawAction, newPen)
    {
      this.InitializeDefaults();
      this.Bounds = Rectangle.Empty;
      this.firstPoint = PointF.Empty;
      this.secondPoint = PointF.Empty;
    }

    /// <summary>
    /// Initializes a new instance of the VGArrow class.
    /// </summary>
    /// <param name="newShapeDrawAction">Drawing action: Edge, Fill, Both</param>
    /// <param name="newPen">Pen for edges</param>
    /// <param name="pt1">first point</param>
    /// <param name="pt2">second point</param>
    public VGArrow(ShapeDrawAction newShapeDrawAction, Pen newPen, PointF pt1, PointF pt2)
      : base(newShapeDrawAction, newPen)
    {
      this.InitializeDefaults();
      this.firstPoint = pt1;
      this.secondPoint = pt2;
      this.Bounds = this.GetBounds();
      this.pointsAreSet = true;
    }

    /// <summary>
    /// Initializes a new instance of the VGArrow class.
    /// </summary>
    /// <param name="newShapeDrawAction">Drawing action: Edge, Fill, Both</param>
    /// <param name="newPen">Pen for edges</param>
    /// <param name="pt1">first point</param>
    /// <param name="pt2">second point</param>
    /// <param name="newStyleGroup">Group Enumeration, <see cref="VGStyleGroup"/></param>
    /// <param name="newName">Name of Element</param>
    /// <param name="newElementGroup">Element group description</param>
    public VGArrow(
      ShapeDrawAction newShapeDrawAction,
      Pen newPen,
      PointF pt1,
      PointF pt2,
      VGStyleGroup newStyleGroup,
      string newName,
      string newElementGroup)
      : base(newShapeDrawAction, newPen, newStyleGroup, newName, newElementGroup)
    {
      this.InitializeDefaults();
      this.firstPoint = pt1;
      this.secondPoint = pt2;
      this.Bounds = this.GetBounds();
      this.pointsAreSet = true;
    }

    /// <summary>
    /// Initializes a new instance of the VGArrow class.
    /// </summary>
    /// <param name="newShapeDrawAction">Drawing action: Edge, Fill, Both</param>
    /// <param name="newPen">Pen for edges</param>
    /// <param name="newStyleGroup">Group Enumeration, <see cref="VGStyleGroup"/></param>
    /// <param name="newName">Name of Element</param>
    /// <param name="newElementGroup">Element group description</param>
    public VGArrow(
      ShapeDrawAction newShapeDrawAction, 
      Pen newPen, 
      VGStyleGroup newStyleGroup,
      string newName,
      string newElementGroup)
      : base(newShapeDrawAction, newPen, newStyleGroup, newName, newElementGroup)
    {
      this.InitializeDefaults();
      this.Bounds = Rectangle.Empty;
      this.firstPoint = PointF.Empty;
      this.secondPoint = PointF.Empty;
    }

    /// <summary>
    /// Initializes a new instance of the VGArrow class.
    /// </summary>
    /// <param name="newShapeDrawAction">Drawing action: Edge, Fill, Both</param>
    /// <param name="newPen">Pen for edges</param>
    /// <param name="newFont">Font for text</param>
    /// <param name="newFontColor">Color for text</param>
    /// <param name="newStyleGroup">Group Enumeration, <see cref="VGStyleGroup"/></param>
    /// <param name="newName">Name of Element</param>
    /// <param name="newElementGroup">Element group description</param>
    public VGArrow(
      ShapeDrawAction newShapeDrawAction,
      Pen newPen,
      Font newFont,
      Color newFontColor,
      VGStyleGroup newStyleGroup,
      string newName,
      string newElementGroup)
      : base(newShapeDrawAction, newPen, newFont, newFontColor, newStyleGroup, newName, newElementGroup)
    {
      this.InitializeDefaults();
      this.Bounds = Rectangle.Empty;
      this.firstPoint = PointF.Empty;
      this.secondPoint = PointF.Empty;
    }

    /// <summary>
    /// Prevents a default instance of the VGArrow class from being created.
    /// Parameterless constructor. Used for serialization.
    /// </summary>
    private VGArrow()
    {
      this.InitializeDefaults();
    }

    /// <summary>
    /// Initializes a new instance of the VGArrow class.
    /// Clone Constructor. Creates new arrow that is
    /// identical to the given arrow.
    /// </summary>
    /// <param name="arrow">arrow to clone</param>
    private VGArrow(VGArrow arrow)
      : base(
      arrow.ShapeDrawAction,
      arrow.Pen,
      arrow.Brush,
      arrow.Font,
      arrow.FontColor,
      arrow.Bounds,
      arrow.StyleGroup,
      arrow.Name,
      arrow.ElementGroup,
      arrow.Sound)
    {
      this.firstPoint = arrow.firstPoint;
      this.secondPoint = arrow.secondPoint;
      this.firstPointWeight = arrow.firstPointWeight;
      this.secondPointWeight = arrow.secondPointWeight;
      this.formatString = arrow.formatString;
      this.weightFont = arrow.weightFont == null ? null : (Font)arrow.weightFont.Clone();
      this.weightFontColor = arrow.weightFontColor;
      this.pointsAreSet = true;
      this.firstPointDistance = arrow.firstPointDistance;
      this.secondPointDistance = arrow.secondPointDistance;
      this.hideWeights = arrow.hideWeights;
      this.scaleFactor = arrow.scaleFactor;
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets or sets the starting point of the arrow.
    /// </summary>
    /// <value>A <see cref="PointF"/> with the starting point location.</value>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Category("Position")]
    [Description("The first point of the arrow.")]
    [TypeConverter(typeof(PointFConverter))]
    public PointF FirstPoint
    {
      get { return this.firstPoint; }
      set { this.firstPoint = value; }
    }

    /// <summary>
    /// Gets or sets the ending point of the arrow.
    /// </summary>
    /// <value>A <see cref="PointF"/> with the ending point location.</value>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Category("Position")]
    [Description("The second point of the arrow.")]
    [TypeConverter(typeof(PointFConverter))]
    public PointF SecondPoint
    {
      get
      {
        return this.secondPoint;
      }

      set
      {
        this.secondPoint = value;
        this.pointsAreSet = true;
        this.Bounds = this.GetBounds();
      }
    }

    /// <summary>
    /// Gets or sets the weight for the starting point of the arrow.
    /// </summary>
    /// <value>A <see cref="Single"/> with the weight for the starting point location.</value>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Category("Appearance")]
    [Description("The weight of the first point of the arrow in pixels.")]
    public float FirstPointWeight
    {
      get { return this.firstPointWeight; }
      set { this.firstPointWeight = value; }
    }

    /// <summary>
    /// Gets or sets the weight for the ending point of the arrow.
    /// </summary>
    /// <value>A <see cref="Single"/> with the weight for the ending point location.</value>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Category("Appearance")]
    [Description("The weight of the second point of the arrow in pixels.")]
    public float SecondPointWeight
    {
      get { return this.secondPointWeight; }
      set { this.secondPointWeight = value; }
    }

    /// <summary>
    /// Gets or sets the distance of the arrow tip form the first point of the arrow in pixels.
    /// </summary>
    /// <value>A <see cref="Single"/> with the distance of the arrow tip form the first point of the arrow in pixels.</value>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Category("Appearance")]
    [Description("The distance of the arrow tip from the first point of the arrow in pixels.")]
    public float FirstPointDistance
    {
      get { return this.firstPointDistance; }
      set { this.firstPointDistance = value; }
    }

    /// <summary>
    /// Gets or sets the distance of the arrow tip form the second point of the arrow in pixels.
    /// </summary>
    /// <value>A <see cref="Single"/> with the distance of the arrow tip form the second point of the arrow in pixels.</value>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Category("Appearance")]
    [Description("The distance of the arrow tip from the second point of the arrow in pixels.")]
    public float SecondPointDistance
    {
      get { return this.secondPointDistance; }
      set { this.secondPointDistance = value; }
    }

    /// <summary>
    /// Gets or sets the scale factor for the weighting values to pixel transformation.
    /// </summary>
    /// <value>A <see cref="Single"/> with the scale factor for the weighting values to pixel transformation.</value>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Category("Appearance")]
    [Description("The scale factor for the weighting values to pixel transformation.")]
    public float ScaleFactor
    {
      get { return this.scaleFactor; }
      set { this.scaleFactor = value; }
    }

    /// <summary>
    /// Gets or sets the formatting string for the weighting values.
    /// </summary>
    /// <value>A <see cref="String"/> with the formatting string for the weighting values.</value>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Category("Appearance")]
    [Description("The formatting string for the weighting values.")]
    public string FormatString
    {
      get { return this.formatString; }
      set { this.formatString = value; }
    }

    /// <summary>
    /// Gets or sets the unit addon string for the weighting values.
    /// </summary>
    /// <value>A <see cref="String"/> with the unit addon string for the weighting values.</value>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Category("Appearance")]
    [Description("The unit add on string for the weighting values.")]
    public string AddOnString
    {
      get { return this.addOnString; }
      set { this.addOnString = value; }
    }

    /// <summary>
    /// Gets or sets the font for the weighting values.
    /// </summary>
    /// <value>A <see cref="Font"/> for the weighting values.</value>
    [XmlIgnoreAttribute]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Category("Appearance")]
    [Description("The Font for the weighting values.")]
    public Font WeightFont
    {
      get { return this.weightFont; }
      set { this.weightFont = value; }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to hide the weighting values.
    /// </summary>
    /// <value>A <see cref="Boolean"/> whether to hide the weighting values.</value>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Category("Appearance")]
    [Description("Hide the weighting values.")]
    public bool HideWeights
    {
      get { return this.hideWeights; }
      set { this.hideWeights = value; }
    }

    /// <summary>
    /// Gets or sets the SerializedWeightFont.
    /// Serializes the <see cref="WeightFont"/> property to XML.
    /// </summary>
    /// <value>A <see cref="string"/> with the string representation of the shapes weightFont.</value>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public string SerializedWeightFont
    {
      get
      {
        if (this.weightFont != null)
        {
          TypeConverter fontConverter = TypeDescriptor.GetConverter(typeof(Font));
          return fontConverter.ConvertToInvariantString(this.weightFont);
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
              this.weightFont = ObjectStringConverter.StringToFontOld(value);
            }
            else
            {
              TypeConverter fontConverter = TypeDescriptor.GetConverter(typeof(Font));
              this.weightFont = (Font)fontConverter.ConvertFromInvariantString(value);
            }
          }
          catch (ArgumentException)
          {
            // Paranoia check for very old versions.
            this.weightFont = new Font(SystemFonts.MenuFont.FontFamily, 28f, GraphicsUnit.Point);
          }
        }
        else
        {
          this.weightFont = null;
        }
      }
    }

    /// <summary>
    /// Gets or sets the font color for the weighting values.
    /// </summary>
    /// <value>A <see cref="Color"/> for the weighting values.</value>
    [XmlIgnoreAttribute]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Category("Appearance")]
    [Description("The font color for the weighting values.")]
    public Color WeightFontColor
    {
      get { return this.weightFontColor; }
      set { this.weightFontColor = value; }
    }

    /// <summary>
    /// Gets or sets the SerializedWeightFontColor.
    /// Serializes and deserializes the <see cref="WeightFontColor"/> to XML,
    /// because XMLSerializer can not serialize <see cref="Color"/> values.
    /// </summary>
    /// <value>A <see cref="string"/> with the string representation of the shapes weightFontColor color.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string SerializedWeightFontColor
    {
      get { return ObjectStringConverter.ColorToHtmlAlpha(this.weightFontColor); }
      set { this.weightFontColor = ObjectStringConverter.HtmlAlphaToColor(value); }
    }

    /// <summary>
    /// Gets or sets the arrows bounding rectangle.
    /// Overridden <see cref="VGElement.Bounds"/>
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override RectangleF Bounds
    {
      get
      {
        return this.GetBounds();
      }

      set
      {
        base.Bounds = value;
        PointF topLeft = value.Location;
        PointF topRight = new PointF(topLeft.X + value.Width, topLeft.Y);
        PointF bottomRight = new PointF(topLeft.X + value.Width, topLeft.Y + value.Height);
        PointF bottomLeft = new PointF(topLeft.X, topLeft.Y + value.Height);
        if (this.firstPoint.Y <= this.secondPoint.Y)
        {
          this.firstPoint = topLeft;
          this.secondPoint = bottomRight;
        }
        else
        {
          this.firstPoint = bottomLeft;
          this.secondPoint = topRight;
        }

        if (this.IsInEditMode)
        {
          this.AddGrabHandles();
        }
      }
    }

    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Public methods                                                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region PUBLICMETHODS

    /// <summary>
    /// This method clears the points of this arrow without
    /// changing its drawing properties as <see cref="Reset()"/> does.
    /// </summary>
    public void Clear()
    {
      this.firstPoint = PointF.Empty;
      this.secondPoint = PointF.Empty;
      this.firstPointWeight = 0f;
      this.secondPointWeight = 0f;
      this.pointsAreSet = false;
    }

    /// <summary>
    /// This method recalculates the bounding rectangle of this arrow.
    /// </summary>
    public void RecalculateBounds()
    {
      this.Bounds = this.GetBounds();
    }

    #endregion //PUBLICMETHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Inherited methods                                                         //
    ///////////////////////////////////////////////////////////////////////////////
    #region OVERRIDES

    /// <summary>
    /// Overridden <see cref="VGElement.Draw(Graphics)"/>.  
    /// Draws the arrow to the given graphics context.
    /// </summary>
    /// <param name="graphics">Graphics context to draw to</param>
    /// <exception cref="ArgumentNullException">Thrown, when graphics object is null.</exception>
    public override void Draw(Graphics graphics)
    {
      if (graphics == null)
      {
        throw new ArgumentNullException("Graphics object should not be null.");
      }

      int spacing = 5;

      // Drawarrow
      if (this.pointsAreSet)
      {
        float angle = (float)Math.Atan2(this.secondPoint.Y - this.firstPoint.Y, this.secondPoint.X - this.firstPoint.X);
        angle = (float)(angle * 180 / Math.PI);
        Matrix rotate = new Matrix();
        rotate.RotateAt((float)(-angle), this.firstPoint);

        PointF[] pts = new PointF[] { this.firstPoint, this.secondPoint };
        rotate.TransformPoints(pts);

        float width1 = this.firstPointWeight * this.scaleFactor;
        float width2 = this.secondPointWeight * this.scaleFactor;

        // Draw left and right arrow
        PointF tip1 = new PointF(
          pts[0].X + this.firstPointDistance,
          pts[0].Y);
        PointF topCorner1 = new PointF(
          pts[0].X + 1.5f * width1 + this.firstPointDistance,
          pts[0].Y + width1);
        PointF topLeftBar1 = new PointF(
          pts[0].X + 1.5f * width1 + this.firstPointDistance,
          pts[0].Y + 0.5f * width1);
        PointF topRightBar1 = new PointF(
          pts[1].X - 1.5f * width2 - spacing - this.secondPointDistance,
          pts[1].Y + 0.5f * width1);
        PointF bottomRightBar1 = new PointF(
          pts[1].X - 1.5f * width2 - spacing - this.secondPointDistance,
          pts[1].Y - 0.5f * width1);
        PointF bottomLeftBar1 = new PointF(
          pts[0].X + 1.5f * width1 + this.firstPointDistance,
          pts[0].Y - 0.5f * width1);
        PointF bottomCorner1 = new PointF(
          pts[0].X + 1.5f * width1 + this.firstPointDistance,
          pts[0].Y - width1);

        PointF tip2 = new PointF(
          pts[1].X - this.secondPointDistance,
          pts[1].Y);
        PointF bottomCorner2 = new PointF(
          pts[1].X - 1.5f * width2 - this.secondPointDistance,
          pts[1].Y - width2);
        PointF bottomRightBar2 = new PointF(
          pts[1].X - 1.5f * width2 - this.secondPointDistance,
          pts[1].Y - 0.5f * width2);
        PointF bottomLeftBar2 = new PointF(
          pts[0].X + 1.5f * width1 + spacing + this.firstPointDistance,
          pts[0].Y - 0.5f * width2);
        PointF topLeftBar2 = new PointF(
          pts[0].X + 1.5f * width1 + spacing + this.firstPointDistance,
          pts[0].Y + 0.5f * width2);
        PointF topRightBar2 = new PointF(
          pts[1].X - 1.5f * width2 - this.secondPointDistance,
          pts[1].Y + 0.5f * width2);
        PointF topCorner2 = new PointF(
          pts[1].X - 1.5f * width2 - this.secondPointDistance,
          pts[1].Y + width2);

        GraphicsPath firstArrow = new GraphicsPath();
        GraphicsPath secondArrow = new GraphicsPath();

        firstArrow.AddLines(
          new PointF[] 
              { 
                bottomRightBar1,
                bottomLeftBar1,
                bottomCorner1,
                tip1,
                topCorner1,
                topLeftBar1,
                topRightBar1
              });

        secondArrow.AddLines(
          new PointF[] 
              { 
                topLeftBar2,
                topRightBar2,
                topCorner2,
                tip2,
                bottomCorner2,
                bottomRightBar2,
                bottomLeftBar2
              });

        PointF[] firstPts = firstArrow.PathPoints;
        PointF[] secondPts = secondArrow.PathPoints;

        rotate.Invert();
        rotate.TransformPoints(firstPts);
        rotate.TransformPoints(secondPts);

        firstArrow.Reset();
        secondArrow.Reset();
        firstArrow.AddLines(firstPts);
        secondArrow.AddLines(secondPts);

        if ((this.ShapeDrawAction & ShapeDrawAction.Fill) == ShapeDrawAction.Fill)
        {
          if (width1 > 0)
          {
            graphics.FillPath(this.Brush, firstArrow);
          }

          if (width2 > 0)
          {
            graphics.FillPath(this.Brush, secondArrow);
          }
        }

        if ((this.ShapeDrawAction & ShapeDrawAction.Edge) == ShapeDrawAction.Edge)
        {
          if (width1 > 0)
          {
            graphics.DrawPath(this.Pen, firstArrow);
          }

          if (width2 > 0)
          {
            graphics.DrawPath(this.Pen, secondArrow);
          }
        }

        if (!this.hideWeights)
        {
          string firstValue = this.firstPointWeight.ToString(this.formatString) + this.addOnString;
          string secondValue = this.secondPointWeight.ToString(this.formatString) + this.addOnString;

          SizeF sizeText1 = graphics.MeasureString(firstValue, this.weightFont);
          SizeF sizeText2 = graphics.MeasureString(secondValue, this.weightFont);

          PointF arrowBase1 = new PointF(
            pts[0].X + 1f * width1 + 30 + this.firstPointDistance,
            pts[0].Y);
          PointF arrowBase2 = new PointF(
            pts[1].X - 1f * width2 - 30 - this.secondPointDistance,
            pts[1].Y);
          PointF[] txtPoints = new PointF[] { arrowBase1, arrowBase2 };
          rotate.TransformPoints(txtPoints);

          RectangleF textBounds1 = new RectangleF(
            txtPoints[0].X - sizeText1.Width / 2 - 1,
            txtPoints[0].Y - sizeText1.Height / 2 - 1,
            sizeText1.Width + 2,
            sizeText1.Height + 1);

          RectangleF textBounds2 = new RectangleF(
            txtPoints[1].X - sizeText2.Width / 2 - 1,
            txtPoints[1].Y - sizeText2.Height / 2 - 1,
            sizeText2.Width + 2,
            sizeText2.Height + 1);

          txtPoints[0].X -= sizeText1.Width / 2;
          txtPoints[1].X -= sizeText2.Width / 2;
          txtPoints[0].Y -= sizeText1.Height / 2;
          txtPoints[1].Y -= sizeText2.Height / 2;

          if (width1 > 0)
          {
            graphics.FillRectangle(new SolidBrush(Color.FromArgb(125, 255, 255, 255)), textBounds1);
            graphics.DrawString(firstValue, this.weightFont, new SolidBrush(this.weightFontColor), txtPoints[0]);
          }

          if (width2 > 0)
          {
            graphics.FillRectangle(new SolidBrush(Color.FromArgb(125, 255, 255, 255)), textBounds2);
            graphics.DrawString(secondValue, this.weightFont, new SolidBrush(this.weightFontColor), txtPoints[1]);
          }
        }
      }

      // Draw name and selection frame if applicable
      base.Draw(graphics);
    }

    /// <summary>
    /// Overridden <see cref="VGElement.GrabHandleMoved(ref GrabHandle, Point)"/>. 
    /// Resets bounds of the arrow
    /// according to the movement of the given grab handle
    /// </summary>
    /// <param name="handle">GrabHandle that moved</param>
    /// <param name="handleMovement">Movement in stimulus coordinates</param>
    public override void GrabHandleMoved(ref GrabHandle handle, Point handleMovement)
    {
      if (handle.GrabHandlePosition == GrabHandle.HandlePosition.Center)
      {
        PointF newCenter = new PointF(this.Center.X - handleMovement.X, this.Center.Y - handleMovement.Y);
        this.Center = newCenter;
        this.AddGrabHandles();
      }
      else
      {
        if (VGPolyline.Distance(handle.Center, this.firstPoint) < GrabHandle.HANDLESIZE)
        {
          this.firstPoint.X -= handleMovement.X;
          this.firstPoint.Y -= handleMovement.Y;
        }
        else if (VGPolyline.Distance(handle.Center, this.secondPoint) < GrabHandle.HANDLESIZE)
        {
          this.secondPoint.X -= handleMovement.X;
          this.secondPoint.Y -= handleMovement.Y;
        }

        PointF newHandleLocation = new PointF(
          handle.Location.X - handleMovement.X,
          handle.Location.Y - handleMovement.Y);

        handle.Location = newHandleLocation;
      }

      this.Modified = true;
    }

    /// <summary>
    /// Overridden <see cref="VGElement.GetPoints()"/>. 
    /// Get the starting and ending point of
    /// the arrow in a list.
    /// </summary>
    /// <returns>A <see cref="List{PointF}"/> with the arrows starting
    /// and ending point.</returns>
    public override List<PointF> GetPoints()
    {
      List<PointF> pointList = new List<PointF>();
      pointList.Add(this.FirstPoint);
      pointList.Add(this.SecondPoint);
      return pointList;
    }

    /// <summary>
    /// Overridden <see cref="VGElement.GetPointCount()"/>. 
    /// Gets number of points of the arrow which are two :-)
    /// </summary>
    /// <returns>Two. (Number of points that constitute this arrow.</returns>
    public override int GetPointCount()
    {
      return 2;
    }

    /// <summary>
    /// Overridden <see cref="VGElement.Reset()"/>. 
    /// Reset the current arrow element to
    /// default values.
    /// </summary>
    public override void Reset()
    {
      base.Reset();
      this.firstPoint = PointF.Empty;
      this.secondPoint = PointF.Empty;
      this.firstPointWeight = 0f;
      this.secondPointWeight = 0f;
      this.firstPointDistance = 0f;
      this.secondPointDistance = 0f;
      this.pointsAreSet = false;
    }

    /// <summary>
    /// Overridden <see cref="Object.ToString()"/> method.
    /// Returns the <see cref="VGArrow"/> properties as a human readable string.
    /// </summary>
    /// <returns>A <see cref="string"/> that represents this <see cref="VGArrow"/>.</returns>
    public override string ToString()
    {
      StringBuilder sb = new StringBuilder();
      sb.Append("VGArrow, Name: ");
      sb.Append(this.Name);
      sb.Append(" ; Group: ");
      sb.Append(this.StyleGroup.ToString());
      sb.Append(" ; Point1: ");
      sb.Append(this.firstPoint.ToString());
      sb.Append(" ; Point2: ");
      sb.Append(this.secondPoint.ToString());
      sb.Append(" ; Pen: ");
      sb.Append(ObjectStringConverter.PenToString(this.Pen));
      sb.Append(" ; Bounds: ");
      sb.Append(this.Bounds.ToString());
      return sb.ToString();
    }

    /// <summary>
    /// Overridden <see cref="VGElement.ToShortString()"/> method.
    /// Returns the main <see cref="VGArrow"/> properties as a human readable string.
    /// </summary>
    /// <returns>A <see cref="string"/> that represents the 
    /// current <see cref="VGArrow"/> in short form with its main properties.</returns>
    public override string ToShortString()
    {
      StringBuilder sb = new StringBuilder();
      sb.Append("Arrow ");
      sb.Append(this.Name);
      return sb.ToString();
    }

    /// <summary>
    /// Overridden <see cref="VGElement.CloneCore()"/>.  
    /// Creates an excact copy of given arrow.
    /// </summary>
    /// <returns>Excact copy of this <see cref="VGArrow"/></returns>
    protected override VGElement CloneCore()
    {
      return new VGArrow(this);
    }

    /// <summary>
    /// Overridden <see cref="VGElement.NewPosition(Matrix)"/>. 
    /// Recalculates the starting and ending point
    /// coordinates when arrow is moved.
    /// </summary>
    /// <param name="translationMatrix">Translation Matrix.</param>
    protected override void NewPosition(Matrix translationMatrix)
    {
      PointF[] both = new PointF[] { this.firstPoint, this.secondPoint };
      translationMatrix.TransformPoints(both);
      this.firstPoint = both[0];
      this.secondPoint = both[1];
    }

    /// <summary>
    /// Overridden <see cref="VGElement.AddGrabHandles()"/>. 
    /// Adds a grab handle for the starting and ending
    /// point of the arrow with a sizeall cursor.
    /// </summary>
    protected override void AddGrabHandles()
    {
      this.AddGrabHandles(true, false, false, false, false, false, false, false, false);

      PointF center1Pt = new PointF(
        this.firstPoint.X - GrabHandle.HANDLESIZE / 2,
        this.firstPoint.Y - GrabHandle.HANDLESIZE / 2);

      PointF center2Pt = new PointF(
        this.secondPoint.X - GrabHandle.HANDLESIZE / 2,
        this.secondPoint.Y - GrabHandle.HANDLESIZE / 2);

      GrabHandle handleFirstPoint = new GrabHandle(
        center1Pt,
        Cursors.SizeNWSE,
        GrabHandle.HandlePosition.Left);
      this.GrabHandles.Add(handleFirstPoint);

      GrabHandle handleSecondPoint = new GrabHandle(
        center2Pt,
        Cursors.SizeNWSE,
        GrabHandle.HandlePosition.Right);
      this.GrabHandles.Add(handleSecondPoint);
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

    /// <summary>
    /// Set default values for members variables.
    /// </summary>
    private void InitializeDefaults()
    {
      this.weightFont = (Font)VGElement.DefaultFont.Clone();
      this.weightFontColor = VGElement.DefaultFontColor;
      this.firstPointDistance = 0f;
      this.secondPointDistance = 0f;
      this.formatString = "N0";
    }

    /// <summary>
    /// This method calculates the bounding rectangle for the given
    /// arrow.
    /// </summary>
    /// <returns>A <see cref="RectangleF"/> with the bounding rectangle
    /// for this arrow.</returns>
    private RectangleF GetBounds()
    {
      PointF upperLeftPt = new PointF();
      PointF lowerRightPt = new PointF();
      if (this.firstPoint.X <= this.secondPoint.X)
      {
        upperLeftPt.X = this.firstPoint.X;
        lowerRightPt.X = this.secondPoint.X;
      }
      else
      {
        upperLeftPt.X = this.secondPoint.X;
        lowerRightPt.X = this.firstPoint.X;
      }

      if (this.firstPoint.Y <= this.secondPoint.Y)
      {
        upperLeftPt.Y = this.firstPoint.Y;
        lowerRightPt.Y = this.secondPoint.Y;
      }
      else
      {
        upperLeftPt.Y = this.secondPoint.Y;
        lowerRightPt.Y = this.firstPoint.Y;
      }

      return new RectangleF(
        upperLeftPt.X,
        upperLeftPt.Y,
        lowerRightPt.X - upperLeftPt.X,
        lowerRightPt.Y - upperLeftPt.Y);
    }

    #endregion //METHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER
    #endregion //HELPER
  }
}
