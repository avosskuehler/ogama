// <copyright file="VGPolyline.cs" company="FU Berlin">
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
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.Drawing;
  using System.Drawing.Drawing2D;
  using System.Text;
  using System.Windows.Forms;
  using System.Xml.Serialization;

  using VectorGraphics.Tools.CustomTypeConverter;

  /// <summary>
  /// Inherited from <see cref="VGElement"/>. 
  /// A serializable class that is a vector graphics polyline,
  /// that can be closed or open and drawn with bounds and/or filled.
  /// </summary>
  /// <remarks>Note that the bounding line is centered over the bounds,
  /// so if you have a 10 px thick edge, your polyline will
  /// be 5 px bigger at each side than its bounds.</remarks>
  [Serializable]
  public class VGPolyline : VGElement
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
    /// Initializes a new instance of the VGPolyline class.
    /// </summary>
    /// <param name="newShapeDrawAction">Drawing action: Edge, Fill, Both</param>
    /// <param name="newPen">Pen for edges</param>
    public VGPolyline(ShapeDrawAction newShapeDrawAction, Pen newPen)
      : base(newShapeDrawAction, newPen)
    {
      this.Path = new GraphicsPath();
    }

    /// <summary>
    /// Initializes a new instance of the VGPolyline class.
    /// </summary>
    /// <param name="newShapeDrawAction">Drawing action: Edge, Fill, Both</param>
    /// <param name="newPen">Pen for edges</param>
    /// <param name="newStyleGroup">Group Enumeration, <see cref="VGStyleGroup"/></param>
    /// <param name="newName">Name of Element</param>
    /// <param name="newElementGroup">Element group description</param>
    public VGPolyline(
      ShapeDrawAction newShapeDrawAction,
      Pen newPen,
      VGStyleGroup newStyleGroup,
      string newName,
      string newElementGroup)
      : base(newShapeDrawAction, newPen, newStyleGroup, newName, newElementGroup)
    {
      this.Path = new GraphicsPath();
    }

    /// <summary>
    /// Initializes a new instance of the VGPolyline class.
    /// </summary>
    /// <param name="newShapeDrawAction">Drawing action: Edge, Fill, Both</param>
    /// <param name="newPen">Pen for edges</param>
    /// <param name="newFont">Font for text</param>
    /// <param name="newFontColor">Color for text</param>
    /// <param name="newStyleGroup">Group Enumeration, <see cref="VGStyleGroup"/></param>
    /// <param name="newName">Name of Element</param>
    /// <param name="newElementGroup">Element group description</param>
    public VGPolyline(
      ShapeDrawAction newShapeDrawAction,
      Pen newPen,
      Font newFont,
      Color newFontColor,
      VGStyleGroup newStyleGroup,
      string newName,
      string newElementGroup)
      : base(newShapeDrawAction, newPen, newFont, newFontColor, newStyleGroup, newName, newElementGroup)
    {
      this.Path = new GraphicsPath();
    }

    /// <summary>
    /// Initializes a new instance of the VGPolyline class.
    /// </summary>
    /// <param name="newShapeDrawAction">Drawing action: Edge, Fill, Both</param>
    /// <param name="newPen">Pen for edges</param>
    /// <param name="newBrush">Brush for fills</param>
    /// <param name="newFont">Font for text</param>
    /// <param name="newFontColor">Color for text</param>
    /// <param name="newStyleGroup">Group Enumeration, <see cref="VGStyleGroup"/></param>
    /// <param name="newName">Name of Element</param>
    /// <param name="newElementGroup">Element group description</param>
    public VGPolyline(
      ShapeDrawAction newShapeDrawAction,
      Pen newPen,
      Brush newBrush,
      Font newFont,
      Color newFontColor,
      VGStyleGroup newStyleGroup,
      string newName,
      string newElementGroup)
      : base(
      newShapeDrawAction,
      newPen,
      newBrush,
      newFont,
      newFontColor,
      RectangleF.Empty,
      newStyleGroup,
      newName,
      newElementGroup,
      null)
    {
      this.Path = new GraphicsPath();
    }

    /// <summary>
    /// Initializes a new instance of the VGPolyline class.
    /// </summary>
    /// <param name="newShapeDrawAction">Drawing action: Edge, Fill, Both</param>
    /// <param name="newPen">Pen for edges</param>
    /// <param name="pts">Point list that constitutes polyline</param>
    public VGPolyline(ShapeDrawAction newShapeDrawAction, Pen newPen, PointF[] pts)
      : base(newShapeDrawAction, newPen)
    {
      this.Path = new GraphicsPath();
      if (pts.Length >= 2)
      {
        this.Path.AddLines(pts);
      }
      else if (pts.Length == 1)
      {
        this.FirstPt = pts[0];
      }
    }

    /// <summary>
    /// Initializes a new instance of the VGPolyline class.
    /// </summary>
    /// <param name="newShapeDrawAction">Drawing action: Edge, Fill, Both</param>
    /// <param name="newPen">Pen for edges</param>
    /// <param name="pts">Point list that constitutes polyline</param>
    /// <param name="newStyleGroup">Group Enumeration, <see cref="VGStyleGroup"/></param>
    /// <param name="newName">Name of Element</param>
    /// <param name="newElementGroup">Element group description</param>
    public VGPolyline(
      ShapeDrawAction newShapeDrawAction,
      Pen newPen,
      PointF[] pts,
      VGStyleGroup newStyleGroup,
      string newName,
      string newElementGroup)
      : base(newShapeDrawAction, newPen, newStyleGroup, newName, newElementGroup)
    {
      this.Path = new GraphicsPath();
      if (pts.Length >= 2)
      {
        this.Path.AddLines(pts);
      }
      else if (pts.Length == 1)
      {
        this.FirstPt = pts[0];
      }
    }

    /// <summary>
    /// Prevents a default instance of the VGPolyline class from being created.
    /// Parameterless constructor. Used for serialization.
    /// </summary>
    private VGPolyline()
    {
    }

    /// <summary>
    /// Initializes a new instance of the VGPolyline class.
    /// Clone Constructor. Creates new polyline element that is
    /// identical to the given polyline.
    /// </summary>
    /// <param name="newPolyline">Polyline to clone</param>
    private VGPolyline(VGPolyline newPolyline)
      : base(
      newPolyline.ShapeDrawAction,
      newPolyline.Pen,
      newPolyline.Brush,
      newPolyline.Font,
      newPolyline.FontColor,
      newPolyline.Bounds,
      newPolyline.StyleGroup,
      newPolyline.Name,
      newPolyline.ElementGroup,
      newPolyline.Sound)
    {
      this.Path = new GraphicsPath();
      this.Path = newPolyline.GetPathCopy();
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets or sets the graphics path that encapsulates the points
    /// of the polyline.
    /// </summary>
    /// <value>A <see cref="GraphicsPath"/> with the polyline points.</value>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false), XmlIgnore]
    public GraphicsPath Path { get; set; }

    /// <summary>
    /// Gets or sets the SerializableGraphicsPath.
    /// Serializes the graphics path to string.
    /// </summary>
    /// <value>A <see cref="string"/> representation of the <see cref="GraphicsPath"/></value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string SerializableGraphicsPath
    {
      get { return ObjectStringConverter.GraphicsPathToString(this.Path); }
      set { this.Path = ObjectStringConverter.StringToGraphicsPath(value); }
    }

    /// <summary>
    /// Gets last point in pointlist.
    /// </summary>
    /// <value>A <see cref="PointF"/> with the last point of the polyline.</value>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [XmlIgnoreAttribute]
    public PointF LastPt
    {
      get { return this.Path.GetLastPoint(); }
    }

    /// <summary>
    /// Gets or sets the first point in pointlist.
    /// </summary>
    /// <value>A <see cref="PointF"/> with the first point of the polyline.</value>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false), XmlIgnore]
    public PointF FirstPt { get; set; }

    /// <summary>
    /// Gets or sets polylines bounding rectangle.
    /// Overriden <see cref="VGElement.Bounds"/>. 
    /// </summary>
    /// <value>A <see cref="RectangleF"/> with the polylines bounds.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override RectangleF Bounds
    {
      get
      {
        return this.Path != null ? this.Path.GetBounds() : RectangleF.Empty;
      }

      set
      {
        base.Bounds = value;
      }
    }

    /// <summary>
    /// Gets the center of the polyline
    /// </summary>
    /// <value>A <see cref="PointF"/> with the center of this polyline.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override PointF Center
    {
      get
      {
        var bounds = this.Bounds;
        var center = new PointF(bounds.X + bounds.Width / 2, bounds.Y + bounds.Height / 2);
        return center;
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the polyline is closed.
    /// </summary>
    /// <value>A <see cref="Boolean"/> that is <strong>true</strong>,
    /// if the polyline is closed, otherwise <strong>false</strong>.</value>
    [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool IsClosed { get; set; }

    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Public methods                                                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region PUBLICMETHODS

    /// <summary>
    /// Calculate Distance of two Vectors per Phythagoras
    /// </summary>
    /// <param name="pt1">Point 1 to calculate the distance for.</param>
    /// <param name="pt2">Point 2 to calculate the distance for.</param>
    /// <returns>Distance of the given Points in picture coordinates.</returns>
    public static float Distance(PointF pt1, PointF pt2)
    {
      double squaredX = Math.Pow(Convert.ToDouble(pt1.X - pt2.X), 2);
      double squaredY = Math.Pow(Convert.ToDouble(pt1.Y - pt2.Y), 2);
      return Convert.ToSingle(Math.Sqrt(squaredX + squaredY));
    }

    /// <summary>
    /// This method clears the points of this polyline without
    /// changing its drawing properties as <see cref="Reset()"/> does.
    /// </summary>
    public void Clear()
    {
      this.Path.Reset();
      this.FirstPt = PointF.Empty;
      this.IsClosed = false;
    }

    /// <summary>
    /// Adds given point at the end of the graphics path.
    /// </summary>
    /// <param name="pt">Point to add</param>
    public void AddPt(PointF pt)
    {
      if (this.Path.PointCount >= 1)
      {
        if (!this.SamePosition(pt, this.Path.GetLastPoint()))
        {
          this.Path.AddLine(this.LastPt, pt);
        }
      }
      else if (this.Path.PointCount == 0)
      {
        if (!this.FirstPt.IsEmpty)
        {
          this.Path.AddLine(this.FirstPt, pt);
        }
        else
        {
          this.FirstPt = pt;
        }
      }
    }

    /// <summary>
    /// Adds given points at the end of the graphics path.
    /// </summary>
    /// <param name="pts">A <see cref="List{PointF}"/> with points to add.</param>
    public void AddPts(List<PointF> pts)
    {
      if (pts.Count > 1)
      {
        this.Path.AddLines(pts.ToArray());
      }
      else if (pts.Count == 1)
      {
        this.AddPt(pts[0]);
      }
    }

    /// <summary>
    /// Removes last point in the pointlist from the polyline
    /// </summary>
    public void RemoveLastPt()
    {
      if (this.Path.PointCount >= 2)
      {
        PointF[] newPts = new PointF[this.Path.PointCount - 1];
        Array.Copy(this.Path.PathPoints, 0, newPts, 0, this.Path.PointCount - 1);
        this.Path.Reset();
        this.Path.AddLines(newPts);
      }
    }

    /// <summary>
    /// Removes last point in the pointlist from the polyline
    /// </summary>
    /// <param name="number">An <see cref="Int32"/> with the number of 
    /// points to be removed.</param>
    public void RemoveLastPts(int number)
    {
      if (this.Path.PointCount > number + 1)
      {
        PointF[] newPts = new PointF[this.Path.PointCount - number];
        Array.Copy(this.Path.PathPoints, 0, newPts, 0, this.Path.PointCount - number);
        this.Path.Reset();
        this.Path.AddLines(newPts);
      }
    }

    /// <summary>
    /// Removes first point in the pointlist from the polyline
    /// </summary>
    public void RemoveFirstPt()
    {
      if (this.Path.PointCount >= 2)
      {
        PointF[] newPts = new PointF[this.Path.PointCount - 1];
        Array.Copy(this.Path.PathPoints, 1, newPts, 0, this.Path.PointCount - 1);
        this.Path.Reset();
        this.Path.AddLines(newPts);
      }
      else
      {
        this.FirstPt = PointF.Empty;
        this.IsClosed = false;
        this.Path.Reset();
      }
    }

    /// <summary>
    /// Removes the given number of points from to head of the polyline
    /// </summary>
    /// <param name="number">An <see cref="Int32"/> with the number of 
    /// points to be removed.</param>
    public void RemoveFirstPts(int number)
    {
      if (this.Path.PointCount >= number)
      {
        PointF[] newPts = new PointF[this.Path.PointCount - number];
        Array.Copy(this.Path.PathPoints, number, newPts, 0, this.Path.PointCount - number);
        this.Path.Reset();
        this.Path.AddLines(newPts);
      }
      else
      {
        this.FirstPt = PointF.Empty;
        this.IsClosed = false;
        this.Path.Reset();
      }
    }

    /// <summary>
    /// Closes polyline by adding the first 
    /// point to the end of the polyline.
    /// </summary>
    public void ClosePolyline()
    {
      if (this.Path.PathPoints[0] != this.Path.PathPoints[this.Path.PointCount - 1])
      {
        this.AddPt(this.Path.PathPoints[0]);
      }

      this.IsClosed = true;
    }

    /// <summary>
    /// Returns length of polyline in pixels.
    /// </summary>
    /// <returns>Length of polyline in pixels</returns>
    public float GetLength()
    {
      float length = 0f;
      if (this.Path.PointCount >= 2)
      {
        for (int i = 0; i < this.Path.PointCount - 1; i++)
        {
          length += Distance(this.Path.PathPoints[i], this.Path.PathPoints[i + 1]);
        }
      }

      return length;
    }

    /// <summary>
    /// Recalculates the bounds of this polyline.
    /// Must be called before position values are requested.
    /// </summary>
    /// <remarks>Automatic calculation is disabled due to performance reasons.</remarks>
    public void RecalculateBounds()
    {
      UpdateBoundsWithoutRaisingNewPosition(this.Path.GetBounds());
    }

    #endregion //PUBLICMETHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Inherited methods                                                         //
    ///////////////////////////////////////////////////////////////////////////////
    #region OVERRIDES

    /// <summary>
    /// Overridden <see cref="VGElement.GetPointCount()"/>. 
    /// Gets number of points in pointlist.
    /// </summary>
    /// <returns>Number of points in graphics path list.</returns>
    public override int GetPointCount()
    {
      return this.Path.PointCount;
    }

    /// <summary>
    /// Overridden <see cref="VGElement.Draw(Graphics)"/> method. 
    /// Draws the polyline to the given graphics context.
    /// </summary>
    /// <param name="graphics">Graphics context to draw to</param>
    /// <exception cref="ArgumentNullException">Thrown, when graphics object is null.</exception>
    public override void Draw(Graphics graphics)
    {
      if (graphics == null)
      {
        throw new ArgumentNullException("Graphics object should not be null.");
      }

      if (this.Path.PointCount > 1)
      {
        GraphicsPath outlinePath = (GraphicsPath)this.Path.Clone();

        // Draw path
        if ((ShapeDrawAction & ShapeDrawAction.Fill) == ShapeDrawAction.Fill)
        {
          graphics.FillPath(Brush, this.Path);
        }

        if ((ShapeDrawAction & ShapeDrawAction.Edge) == ShapeDrawAction.Edge)
        {
          graphics.DrawPath(Pen, outlinePath);
        }
      }

      // Draw name and selection frame if applicable
      base.Draw(graphics);
    }

    /// <summary>
    /// Overridden <see cref="VGElement.Contains(PointF)"/>. 
    /// Detects if given point is in polyline region.
    /// </summary>
    /// <param name="pt">Point to test.</param>
    /// <returns><strong>True</strong> if point is in region,
    /// otherwise <strong>false</strong>.</returns>
    public override bool Contains(PointF pt)
    {
      GraphicsPath bigPath = (GraphicsPath)this.Path.Clone();
      bigPath.FillMode = FillMode.Winding;

      if (this.IsInEditMode)
      {
        Pen handlePen = new Pen(Color.White, GrabHandle.HANDLESIZE);
        handlePen.Alignment = PenAlignment.Outset;
        bigPath.Widen(handlePen);
      }

      bigPath.AddPath(this.Path, true);

      return bigPath.IsVisible(pt);
    }

    /// <summary>
    /// Overridden <see cref="VGElement.Contains(PointF, Int32)"/>. 
    /// Detects if given point is in element region widened by the tolerance value.
    /// </summary>
    /// <param name="pt">Point to check.</param>
    /// <param name="tolerance">An <see cref="int"/> tolerance value for 
    /// widening areas of interest to get a better hit rate in pixel.</param>
    /// <returns><strong>True</strong> if point is in region, otherwise <strong>false</strong></returns>
    public override bool Contains(PointF pt, int tolerance)
    {
      GraphicsPath bigPath = (GraphicsPath)this.Path.Clone();
      bigPath.FillMode = FillMode.Winding;

      Pen tolerancePen = new Pen(Color.White, tolerance * 2);
      bigPath.Widen(tolerancePen);

      bigPath.AddPath(this.Path, true);

      return bigPath.IsVisible(pt);
    }

    /// <summary>
    /// Overridden <see cref="VGElement.GrabHandleMoved(ref GrabHandle, Point)"/>. 
    /// Resets bounds of the poyline according to the movement of the given grab handle.
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
        List<PointF> currentPoints = this.GetPoints();
        bool recreateGrabHandles = false;
        for (int i = 0; i < currentPoints.Count; i++)
        {
          if (Distance(handle.Center, currentPoints[i]) < GrabHandle.HANDLESIZE)
          {
            PointF movedPt = currentPoints[i];
            movedPt.X -= handleMovement.X;
            movedPt.Y -= handleMovement.Y;
            currentPoints[i] = movedPt;
            if (i == 0)
            {
              currentPoints[this.GetPointCount() - 1] = movedPt;
              recreateGrabHandles = true;
            }
            else if (i == this.GetPointCount() - 1)
            {
              currentPoints[0] = movedPt;
              recreateGrabHandles = true;
            }

            break;
          }
        }

        this.Path.Reset();
        this.Path.AddLines(currentPoints.ToArray());

        PointF newHandleLocation = new PointF(
          handle.Location.X - handleMovement.X,
          handle.Location.Y - handleMovement.Y);

        handle.Location = newHandleLocation;

        if (recreateGrabHandles)
        {
          this.AddGrabHandles();
        }
      }

      this.Modified = true;
    }

    /// <summary>
    /// Overridden <see cref="VGElement.GetPoints()"/>. 
    /// Gets point list of this polyline.
    /// </summary>
    /// <returns>A <see cref="List{PointF}"/> with the points of the path.</returns>
    public override List<PointF> GetPoints()
    {
      List<PointF> pointList = new List<PointF>();
      if (this.Path != null && this.Path.PointCount > 0)
      {
        foreach (PointF pt in this.Path.PathPoints)
        {
          pointList.Add(pt);
        }
      }

      return pointList;
    }

    /// <summary>
    /// Overridden <see cref="VGElement.Reset()"/>. 
    /// Reset the current polyline element to
    /// default values.
    /// </summary>
    public override void Reset()
    {
      base.Reset();
      this.FirstPt = PointF.Empty;
      this.IsClosed = false;
      this.Path.Reset();
    }

    /// <summary>
    /// Overridden <see cref="Object.ToString()"/> method.
    /// Returns the <see cref="VGPolyline"/> properties as a human readable string.
    /// </summary>
    /// <returns>A <see cref="string"/> that represents this <see cref="VGPolyline"/>.</returns>
    public override string ToString()
    {
      StringBuilder sb = new StringBuilder();
      sb.Append("VGPolyline, Name: ");
      sb.Append(this.Name);
      sb.Append(" ; ShapeDrawAction: ");
      sb.Append(this.ShapeDrawAction.ToString());
      sb.Append(" ; Pen: ");
      sb.Append(ObjectStringConverter.PenToString(this.Pen));
      sb.Append(" ; Brush: ");
      sb.Append(ObjectStringConverter.BrushToString(this.Brush));
      sb.Append(" ; Group: ");
      sb.Append(this.StyleGroup.ToString());
      sb.Append(" ; Bounds: ");
      sb.Append(this.Bounds.ToString());
      sb.Append(" ; NumberOfPoints: ");
      sb.Append(this.GetPointCount());
      return sb.ToString();
    }

    /// <summary>
    /// Overridden <see cref="VGElement.ToShortString()"/> method.
    /// Returns the main <see cref="VGPolyline"/> properties as a human readable string.
    /// </summary>
    /// <returns>A <see cref="string"/> that represents the 
    /// current <see cref="VGPolyline"/> in short form with its main properties.</returns>
    public override string ToShortString()
    {
      StringBuilder sb = new StringBuilder();
      sb.Append("Polyline ");
      sb.Append(this.Name);
      sb.Append(", Draw: ");
      sb.Append(this.ShapeDrawAction.ToString());
      return sb.ToString();
    }

    /// <summary>
    /// Overridden <see cref="VGElement.CloneCore()"/> method. 
    /// Creates a excact copy of given <see cref="VGPolyline"/>.
    /// </summary>
    /// <returns>Excact copy of this <see cref="VGPolyline"/>.</returns>
    protected override VGElement CloneCore()
    {
      return new VGPolyline(this);
    }

    /// <summary>
    /// Overridden <see cref="VGElement.NewPosition(Matrix)"/>. 
    /// Transforms graphic path referring to given translation.
    /// </summary>
    /// <param name="translationMatrix">Translation Matrix</param>
    protected override void NewPosition(Matrix translationMatrix)
    {
      if (this.Path != null)
      {
        this.Path.Transform(translationMatrix);
      }
    }

    /// <summary>
    /// Overridden <see cref="VGElement.AddGrabHandles()"/>. 
    /// Adds a grab handle for each vertex of the polyline with a sizeall cursor
    /// </summary>
    protected override void AddGrabHandles()
    {
      this.AddGrabHandles(true, false, false, false, false, false, false, false, false);

      foreach (PointF point in this.GetPoints())
      {
        Point grabHandleLocation = new Point(
          (int)(point.X - GrabHandle.HANDLESIZE / 2),
          (int)(point.Y - GrabHandle.HANDLESIZE / 2));

        GrabHandle handle = new GrabHandle(
          grabHandleLocation,
          Cursors.SizeNESW,
          GrabHandle.HandlePosition.Top);

        this.GrabHandles.Add(handle);
      }
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
    /// Gets a copy of the graphics path.
    /// </summary>
    /// <returns>Exact copy of the polylines graphic path.</returns>
    private GraphicsPath GetPathCopy()
    {
      if (this.Path == null)
      {
        this.Path = new GraphicsPath();
      }

      return (GraphicsPath)this.Path.Clone();
    }

    #endregion //METHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER

    /// <summary>
    /// Checks whether the given two points are at the same 
    /// position, when rounded up to two decimals.
    /// </summary>
    /// <param name="pt1">First point to test.</param>
    /// <param name="pt2">Second point to test.</param>
    /// <returns><strong>True</strong> if both points
    /// cn be considered as equal.</returns>
    private bool SamePosition(PointF pt1, PointF pt2)
    {
      if ((Math.Round(pt1.X, 2) == Math.Round(pt2.X, 2)) &&
        (Math.Round(pt1.Y, 2) == Math.Round(pt2.Y, 2)))
      {
        return true;
      }

      return false;
    }

    #endregion //HELPER
  }
}
