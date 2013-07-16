// <copyright file="VGLine.cs" company="FU Berlin">
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
  /// A serializable class that is a vector graphics line drawn with a specific pen.
  /// </summary>
  [Serializable]
  public class VGLine : VGElement
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
    /// Saves the starting point of the line.
    /// </summary>
    private PointF firstPoint;

    /// <summary>
    /// Saves the ending point of the line.
    /// </summary>
    private PointF secondPoint;

    /// <summary>
    /// Indicates, whether both points are set.
    /// </summary>
    private bool pointsAreSet;

    #endregion FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the VGLine class.
    /// </summary>
    /// <param name="newShapeDrawAction">Drawing action: Edge, Fill, Both</param>
    /// <param name="newPen">Pen for edges</param>
    public VGLine(ShapeDrawAction newShapeDrawAction, Pen newPen)
      : base(newShapeDrawAction, newPen)
    {
      this.Bounds = Rectangle.Empty;
      this.firstPoint = PointF.Empty;
      this.secondPoint = PointF.Empty;
    }

    /// <summary>
    /// Initializes a new instance of the VGLine class.
    /// </summary>
    /// <param name="newShapeDrawAction">Drawing action: Edge, Fill, Both</param>
    /// <param name="newPen">Pen for edges</param>
    /// <param name="pt1">first point</param>
    /// <param name="pt2">second point</param>
    public VGLine(ShapeDrawAction newShapeDrawAction, Pen newPen, PointF pt1, PointF pt2)
      : base(newShapeDrawAction, newPen)
    {
      this.firstPoint = pt1;
      this.secondPoint = pt2;
      this.Bounds = this.GetBounds();
      this.pointsAreSet = true;
    }

    /// <summary>
    /// Initializes a new instance of the VGLine class.
    /// </summary>
    /// <param name="newShapeDrawAction">Drawing action: Edge, Fill, Both</param>
    /// <param name="newPen">Pen for edges</param>
    /// <param name="pt1">first point</param>
    /// <param name="pt2">second point</param>
    /// <param name="newStyleGroup">Group Enumeration, <see cref="VGStyleGroup"/></param>
    /// <param name="newName">Name of Element</param>
    /// <param name="newElementGroup">Element group description</param>
    public VGLine(
      ShapeDrawAction newShapeDrawAction,
      Pen newPen,
      PointF pt1,
      PointF pt2,
      VGStyleGroup newStyleGroup,
      string newName,
      string newElementGroup)
      : base(newShapeDrawAction, newPen, newStyleGroup, newName, newElementGroup)
    {
      this.firstPoint = pt1;
      this.secondPoint = pt2;
      this.Bounds = this.GetBounds();
      this.pointsAreSet = true;
    }

    /// <summary>
    /// Initializes a new instance of the VGLine class.
    /// </summary>
    /// <param name="newShapeDrawAction">Drawing action: Edge, Fill, Both</param>
    /// <param name="newPen">Pen for edges</param>
    /// <param name="newStyleGroup">Group Enumeration, <see cref="VGStyleGroup"/></param>
    /// <param name="newName">Name of Element</param>
    /// <param name="newElementGroup">Element group description</param>
    public VGLine(
      ShapeDrawAction newShapeDrawAction, 
      Pen newPen, 
      VGStyleGroup newStyleGroup,
      string newName,
      string newElementGroup)
      : base(newShapeDrawAction, newPen, newStyleGroup, newName, newElementGroup)
    {
      this.Bounds = Rectangle.Empty;
      this.firstPoint = PointF.Empty;
      this.secondPoint = PointF.Empty;
    }

    /// <summary>
    /// Initializes a new instance of the VGLine class.
    /// </summary>
    /// <param name="newShapeDrawAction">Drawing action: Edge, Fill, Both</param>
    /// <param name="newPen">Pen for edges</param>
    /// <param name="newFont">Font for text</param>
    /// <param name="newFontColor">Color for text</param>
    /// <param name="newStyleGroup">Group Enumeration, <see cref="VGStyleGroup"/></param>
    /// <param name="newName">Name of Element</param>
    /// <param name="newElementGroup">Element group description</param>
    public VGLine(
      ShapeDrawAction newShapeDrawAction,
      Pen newPen,
      Font newFont,
      Color newFontColor,
      VGStyleGroup newStyleGroup,
      string newName,
      string newElementGroup)
      : base(newShapeDrawAction, newPen, newFont, newFontColor, newStyleGroup, newName, newElementGroup)
    {
      this.Bounds = Rectangle.Empty;
      this.firstPoint = PointF.Empty;
      this.secondPoint = PointF.Empty;
    }

    /// <summary>
    /// Prevents a default instance of the VGLine class from being created.
    /// Parameterless constructor. Used for serialization.
    /// </summary>
    private VGLine()
    {
    }

    /// <summary>
    /// Initializes a new instance of the VGLine class.
    /// Clone Constructor. Creates new line that is
    /// identical to the given line.
    /// </summary>
    /// <param name="line">Line to clone</param>
    private VGLine(VGLine line)
      : base(
      line.ShapeDrawAction,
      line.Pen,
      line.Brush,
      line.Font,
      line.FontColor,
      line.Bounds,
      line.StyleGroup,
      line.Name,
      line.ElementGroup,
      line.Sound)
    {
      this.firstPoint = line.firstPoint;
      this.secondPoint = line.secondPoint;
      this.pointsAreSet = true;
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets or sets the starting point of the line.
    /// </summary>
    /// <value>A <see cref="PointF"/> with the starting point location.</value>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Category("Position")]
    [Description("The first point of the line.")]
    [TypeConverter(typeof(PointFConverter))]
    public PointF FirstPoint
    {
      get { return this.firstPoint; }
      set { this.firstPoint = value; }
    }

    /// <summary>
    /// Gets or sets the ending point of the line.
    /// </summary>
    /// <value>A <see cref="PointF"/> with the ending point location.</value>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Category("Position")]
    [Description("The second point of the line.")]
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
      }
    }

    /// <summary>
    /// Gets or sets the lines bounding rectangle.
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
    /// This method clears the points of this line without
    /// changing its drawing properties as <see cref="Reset()"/> does.
    /// </summary>
    public void Clear()
    {
      this.firstPoint = PointF.Empty;
      this.secondPoint = PointF.Empty;
      this.pointsAreSet = false;
    }

    /// <summary>
    /// This method recalculates the bounding rectangle of this line.
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
    /// Overridden <see cref="VGElement.Contains(PointF)"/>.
    /// This is overridden because when the line is horizontal or vertical
    /// it could not be clicked, because the bounding rect has no area.
    /// </summary>
    /// <param name="pt">The <see cref="PointF"/> to check whether it is contained
    /// in this line.</param>
    /// <returns><strong>True</strong> if point is in line, otherwise <strong>false</strong></returns>
    public override bool Contains(PointF pt)
    {
      RectangleF bounds = this.Bounds;
      if (bounds.Width * bounds.Height < 20)
      {
        bounds.Y -= 2;
        bounds.Height += 4;
        bounds.X -= 2;
        bounds.Width += 4;
      }

      return bounds.Contains(pt);
    }

    /// <summary>
    /// Overridden <see cref="VGElement.Draw(Graphics)"/>.  
    /// Draws the line to the given graphics context.
    /// </summary>
    /// <param name="graphics">Graphics context to draw to</param>
    /// <exception cref="ArgumentNullException">Thrown, when graphics object is null.</exception>
    public override void Draw(Graphics graphics)
    {
      if (graphics == null)
      {
        throw new ArgumentNullException("Graphics object should not be null.");
      }

      // DrawLine
      if ((this.ShapeDrawAction & ShapeDrawAction.Edge) == ShapeDrawAction.Edge)
      {
        if (this.pointsAreSet)
        {
          graphics.DrawLine(this.Pen, this.firstPoint, this.secondPoint);
        }
      }

      // Draw name and selection frame if applicable
      base.Draw(graphics);
    }

    /// <summary>
    /// Overridden <see cref="VGElement.GrabHandleMoved(ref GrabHandle, Point)"/>. 
    /// Resets bounds of the line
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
    /// the line in a list.
    /// </summary>
    /// <returns>A <see cref="List{PointF}"/> with the lines starting
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
    /// Gets number of points of the line which are two :-)
    /// </summary>
    /// <returns>Two. (Number of points that constitute this line.</returns>
    public override int GetPointCount()
    {
      return 2;
    }

    /// <summary>
    /// Overridden <see cref="VGElement.Reset()"/>. 
    /// Reset the current line element to
    /// default values.
    /// </summary>
    public override void Reset()
    {
      base.Reset();
      this.firstPoint = PointF.Empty;
      this.secondPoint = PointF.Empty;
      this.pointsAreSet = false;
    }

    /// <summary>
    /// Overridden <see cref="Object.ToString()"/> method.
    /// Returns the <see cref="VGLine"/> properties as a human readable string.
    /// </summary>
    /// <returns>A <see cref="string"/> that represents this <see cref="VGLine"/>.</returns>
    public override string ToString()
    {
      StringBuilder sb = new StringBuilder();
      sb.Append("VGLine, Name: ");
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
    /// Returns the main <see cref="VGLine"/> properties as a human readable string.
    /// </summary>
    /// <returns>A <see cref="string"/> that represents the 
    /// current <see cref="VGLine"/> in short form with its main properties.</returns>
    public override string ToShortString()
    {
      StringBuilder sb = new StringBuilder();
      sb.Append("Line ");
      sb.Append(this.Name);
      return sb.ToString();
    }

    /// <summary>
    /// Overridden <see cref="VGElement.CloneCore()"/>.  
    /// Creates an excact copy of given line.
    /// </summary>
    /// <returns>Excact copy of this <see cref="VGLine"/></returns>
    protected override VGElement CloneCore()
    {
      return new VGLine(this);
    }

    /// <summary>
    /// Overridden <see cref="VGElement.NewPosition(Matrix)"/>. 
    /// Recalculates the starting and ending point
    /// coordinates when line is moved.
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
    /// point of the line with a sizeall cursor.
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
    /// This method calculates the bounding rectangle for the given
    /// line.
    /// </summary>
    /// <returns>A <see cref="RectangleF"/> with the bounding rectangle
    /// for this line.</returns>
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

      RectangleF bounds = new RectangleF(
        upperLeftPt.X,
        upperLeftPt.Y,
        lowerRightPt.X - upperLeftPt.X,
        lowerRightPt.Y - upperLeftPt.Y);

      return bounds;
    }

    #endregion //METHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER
    #endregion //HELPER
  }
}
