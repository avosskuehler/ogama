// <copyright file="VGCursor.cs" company="FU Berlin">
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
  using System.Drawing.Imaging;
  using System.Reflection;
  using System.Text;
  using System.Windows.Forms;

  using VectorGraphics.Tools.CustomTypeConverter;

  /// <summary>
  /// Inherited from <see cref="VGElement"/>. 
  /// A serializable class that is a vector graphics cursor,
  /// that can have standard mouse cursor shape or be a sharp,
  /// circle or rectangle.
  /// </summary>
  [Serializable]
  public class VGCursor : VGElement
  {
    ///////////////////////////////////////////////////////////////////////////////
    // Defining Constants                                                        //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTANTS

    /// <summary>
    /// Stores a bitmap of the <see cref="Cursors.Arrow"/> in its original size.
    /// </summary>
    private static Bitmap arrowBitmap;

    /// <summary>
    /// Stores a bitmap of the <see cref="Cursors.Cross"/> in its original size.
    /// </summary>
    private static Bitmap crossBitmap;

    /// <summary>
    /// Stores a bitmap of the <see cref="Cursors.Hand"/> in its original size.
    /// </summary>
    private static Bitmap handBitmap;

    /// <summary>
    /// Stores a bitmap of the <see cref="Cursors.Help"/> in its original size.
    /// </summary>
    private static Bitmap helpBitmap;

    /// <summary>
    /// Stores a bitmap of the <see cref="Cursors.SizeAll"/> in its original size.
    /// </summary>
    private static Bitmap sizeAllBitmap;

    /// <summary>
    /// Stores a bitmap of the <see cref="Cursors.UpArrow"/> in its original size.
    /// </summary>
    private static Bitmap upArrowBitmap;

    /// <summary>
    /// Stores a bitmap of the <see cref="Cursors.WaitCursor"/> in its original size.
    /// </summary>
    private static Bitmap waitCursorBitmap;

    /// <summary>
    /// Stores a bitmap of the mouse button cursor in its original size.
    /// </summary>
    private static Bitmap mouseCursorBitmap;

    /// <summary>
    /// Stores a bitmap of the left mouse button cursor in its original size.
    /// </summary>
    private static Bitmap leftMouseCursorBitmap;

    /// <summary>
    /// Stores a bitmap of the right mouse button cursor in its original size.
    /// </summary>
    private static Bitmap rightMouseCursorBitmap;

    #endregion //CONSTANTS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Variables, Enumerations, Events                                  //
    ///////////////////////////////////////////////////////////////////////////////
    #region FIELDS

    /// <summary>
    /// Saves the cursor to use.
    /// </summary>
    private DrawingCursors cursorType;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes static members of the VGCursor class
    /// which are the static standard cursor bitmaps.
    /// </summary>
    static VGCursor()
    {
      CreateCursorBitmaps();
    }

    /// <summary>
    /// Initializes a new instance of the VGCursor class.
    /// </summary>
    /// <param name="newPen">A <see cref="Pen"/> for the edges.</param>
    /// <param name="newCursorType">A <see cref="DrawingCursors"/> to define the type of the cursor.</param>
    /// <param name="newSize">A <see cref="float"/> with the cursors size</param>
    /// <param name="newStyleGroup">A <see cref="VGStyleGroup"/> with the elements category</param>
    public VGCursor(Pen newPen, DrawingCursors newCursorType, float newSize, VGStyleGroup newStyleGroup)
      : base(ShapeDrawAction.Edge, newPen, newStyleGroup, string.Empty, string.Empty)
    {
      this.Bounds = new RectangleF(0, 0, newSize, newSize);
      this.cursorType = newCursorType;
    }

    /// <summary>
    /// Initializes a new instance of the VGCursor class.
    /// </summary>
    /// <param name="newPen">A <see cref="Pen"/> for the edges.</param>
    /// <param name="newBrush">A <see cref="Brush"/> for the fills.<remarks>Currently used only for Mouse button fills.</remarks></param>
    /// <param name="newCursorType">A <see cref="DrawingCursors"/> to define the type of the cursor.</param>
    /// <param name="newSize">A <see cref="float"/> with the cursors size</param>
    /// <param name="newStyleGroup">A <see cref="VGStyleGroup"/> with the elements category</param>
    public VGCursor(
      Pen newPen,
      Brush newBrush,
      DrawingCursors newCursorType,
      float newSize,
      VGStyleGroup newStyleGroup)
      :
      base(ShapeDrawAction.Edge, newPen, newBrush, newStyleGroup, string.Empty, string.Empty)
    {
      this.Bounds = new RectangleF(0, 0, newSize, newSize);
      this.cursorType = newCursorType;
    }

    /// <summary>
    /// Prevents a default instance of the VGCursor class from being created.
    /// Parameterless constructor. Used for serialization.
    /// </summary>
    private VGCursor()
    {
    }

    /// <summary>
    /// Initializes a new instance of the VGCursor class.
    /// Clone Constructor. Creates new cursor element that is
    /// identical to the given cursor.
    /// </summary>
    /// <param name="cursor">VGCursor to clone</param>
    private VGCursor(VGCursor cursor)
      : base(
      cursor.ShapeDrawAction,
      cursor.Pen,
      cursor.Brush,
      cursor.Font,
      cursor.FontColor,
      cursor.Bounds,
      cursor.StyleGroup,
      cursor.Name,
      cursor.ElementGroup,
      cursor.Sound)
    {
      this.cursorType = cursor.CursorType;
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Enumerations                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region ENUMS

    /// <summary>
    /// The list of cursor types that can be drawn.
    /// </summary>
    /// <remarks>A lot is part of the <see cref="Cursors"/> list,
    /// the others are custom cursors.</remarks>
    public enum DrawingCursors
    {
      /// <summary>
      /// The arrow cursor.
      /// </summary>
      Arrow,

      /// <summary>
      /// The crosshair cursor
      /// </summary>
      Cross,

      /// <summary>
      /// The hand cursor, typically used when hovering over a Web link.
      /// </summary>
      Hand,

      /// <summary>
      /// The Help cursor, which is a combination of an arrow and a question mark.
      /// </summary>
      Help,

      /// <summary>
      /// The four-headed sizing cursor, which consists of four joined arrows that point north, south, east, and west.
      /// </summary>
      SizeAll,

      /// <summary>
      /// The up arrow cursor, typically used to identify an insertion point.
      /// </summary>     
      UpArrow,

      /// <summary>
      /// The wait cursor, typically an hourglass shape.
      /// </summary>
      WaitCursor,

      /// <summary>
      /// A circle shaped cursor.
      /// </summary>
      Circle,

      /// <summary>
      /// A sharp shaped cursor.
      /// </summary>
      Sharp,

      /// <summary>
      /// A square shaped cursor.
      /// </summary>
      Square,

      /// <summary>
      /// A mouse with red left button.
      /// </summary>
      MouseLeft,

      /// <summary>
      /// A mouse with red right button.
      /// </summary>
      MouseRight,

      /// <summary>
      /// A mouse shaped cursor.
      /// </summary>
      Mouse,
    }

    #endregion ENUMS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets or sets the type of the cursor to draw.
    /// </summary>
    /// <value>A <see cref="DrawingCursors"/> with the type of the cursor.</value>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Category("Appearance")]
    [Description("The type of the cursor.")]
    public DrawingCursors CursorType
    {
      get { return this.cursorType; }
      set { this.cursorType = value; }
    }

    /// <summary>
    /// Gets the bounding rectangle including the newPen width.
    /// </summary>
    public override RectangleF BigBounds
    {
      get
      {
        RectangleF bigbounds = this.Bounds;
        bigbounds.Inflate(GrabHandle.HANDLESIZE + 2, GrabHandle.HANDLESIZE + 2);

        // Move by hotspot.
        bigbounds.Offset(Cursors.Arrow.HotSpot);

        return bigbounds;
      }
    }

    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Inherited methods                                                         //
    ///////////////////////////////////////////////////////////////////////////////
    #region OVERRIDES

    /// <summary>
    /// Overridden <see cref="VGElement.Draw(Graphics)"/> method. 
    /// Draws the cursor to the given graphics context.
    /// </summary>
    /// <param name="graphics">Graphics context to draw to</param>
    /// <exception cref="ArgumentNullException">Thrown, when graphics object is null.</exception>
    public override void Draw(Graphics graphics)
    {
      if (graphics == null)
      {
        throw new ArgumentNullException("Graphics object should not be null.");
      }

      switch (this.cursorType)
      {
        case DrawingCursors.Arrow:
          // The following line could not be used, because
          // the cursors drawing routine seems to ignore the transformation of
          // the graphics object :-(((
          // Cursors.Arrow.DrawStretched(graphics, Rectangle.Round(GetOffsetBounds(Cursors.Arrow)));
          // Cursors.Arrow.Draw(graphics, Rectangle.Round(GetOffsetBounds(Cursors.Arrow)));
          graphics.DrawImage(arrowBitmap, this.GetOffsetBounds(Cursors.Arrow));
          break;
        case DrawingCursors.Cross:
          graphics.DrawImage(crossBitmap, this.GetOffsetBounds(Cursors.Cross));
          break;
        case DrawingCursors.Hand:
          graphics.DrawImage(handBitmap, this.GetOffsetBounds(Cursors.Hand));
          break;
        case DrawingCursors.Help:
          graphics.DrawImage(helpBitmap, this.GetOffsetBounds(Cursors.Help));
          break;
        case DrawingCursors.SizeAll:
          graphics.DrawImage(sizeAllBitmap, this.GetOffsetBounds(Cursors.SizeAll));
          break;
        case DrawingCursors.UpArrow:
          graphics.DrawImage(upArrowBitmap, this.GetOffsetBounds(Cursors.UpArrow));
          break;
        case DrawingCursors.WaitCursor:
          graphics.DrawImage(waitCursorBitmap, this.GetOffsetBounds(Cursors.WaitCursor));
          break;
        case DrawingCursors.Mouse:
          this.DrawMouseCursor(graphics, MouseButtons.None, this.Bounds);
          break;
        case DrawingCursors.MouseLeft:
          this.DrawMouseCursor(graphics, MouseButtons.Left, this.Bounds);
          break;
        case DrawingCursors.MouseRight:
          this.DrawMouseCursor(graphics, MouseButtons.Right, this.Bounds);
          break;
        case DrawingCursors.Circle:
          graphics.DrawEllipse(this.Pen, Bounds);
          break;
        case DrawingCursors.Sharp:
          PointF[] sharpPoints = new PointF[] 
              {
                new PointF(Bounds.Left, Bounds.Top + (this.Width / 2)),
                new PointF(Bounds.Right, Bounds.Top + (this.Width / 2)),
                new PointF(Bounds.Left + (this.Width / 2), Bounds.Top + (this.Width / 2)),
                new PointF(Bounds.Left + (this.Width / 2), Bounds.Top),
                new PointF(Bounds.Left + (this.Width / 2), Bounds.Bottom),
                new PointF(Bounds.Left + (this.Width / 2), Bounds.Top + (this.Width / 2))
              };
          graphics.DrawPolygon(this.Pen, sharpPoints);
          break;
        case DrawingCursors.Square:
          graphics.DrawRectangle(this.Pen, Rectangle.Round(Bounds));
          break;
      }

      // Don´t draw name and selection frame
      // base.Draw(graphics);
    }

    /// <summary>
    /// Overridden <see cref="VGElement.Reset()"/> method.
    /// Reset the current cursor element to
    /// default value, which is an arrow cursor.
    /// </summary>
    public override void Reset()
    {
      base.Reset();
      this.cursorType = DrawingCursors.Arrow;
    }

    /// <summary>
    /// Overridden <see cref="Object.ToString()"/> method.
    /// Returns the <see cref="VGCursor"/> properties as a human readable string.
    /// </summary>
    /// <returns>A <see cref="string"/> that represents the current <see cref="VGCursor"/>.</returns>
    public override string ToString()
    {
      StringBuilder sb = new StringBuilder();
      sb.Append("VGCursor, Type: ");
      sb.Append(this.cursorType.ToString());
      sb.Append(" ; Pen: ");
      sb.Append(ObjectStringConverter.PenToString(Pen));
      sb.Append(" ; Group: ");
      sb.Append(StyleGroup.ToString());
      sb.Append(" ; Bounds: ");
      sb.Append(Bounds.ToString());
      return sb.ToString();
    }

    /// <summary>
    /// Overridden <see cref="VGElement.ToShortString()"/> method.
    /// Returns the main <see cref="VGCursor"/> properties as a short human readable string.
    /// </summary>
    /// <returns>A <see cref="string"/> that represents the 
    /// current <see cref="VGCursor"/> in short form with its main properties.</returns>
    public override string ToShortString()
    {
      StringBuilder sb = new StringBuilder();
      sb.Append("Cursor: ");
      sb.Append(this.cursorType.ToString());
      return sb.ToString();
    }

    /// <summary>
    /// Overridden <see cref="VGElement.CloneCore()"/> method. 
    /// Creates a excact copy of given cursor.
    /// </summary>
    /// <returns>Excact copy of this cursor</returns>
    protected override VGElement CloneCore()
    {
      return new VGCursor(this);
    }

    #endregion //OVERRIDES

    ///////////////////////////////////////////////////////////////////////////////
    // Methods for doing main class job                                          //
    ///////////////////////////////////////////////////////////////////////////////
    #region METHODS

    /// <summary>
    /// Creates transparent bitmaps of the standard cursors.
    /// </summary>
    private static void CreateCursorBitmaps()
    {
      arrowBitmap = new Bitmap(Cursors.Arrow.Size.Width, Cursors.Arrow.Size.Height, PixelFormat.Format32bppArgb);
      Graphics g = Graphics.FromImage(arrowBitmap);
      Cursors.Arrow.Draw(g, new Rectangle(0, 0, arrowBitmap.Width, arrowBitmap.Height));

      crossBitmap = new Bitmap(Cursors.Cross.Size.Width, Cursors.Cross.Size.Height, PixelFormat.Format32bppArgb);
      g = Graphics.FromImage(crossBitmap);
      Cursors.Cross.Draw(g, new Rectangle(0, 0, crossBitmap.Width, crossBitmap.Height));

      handBitmap = new Bitmap(Cursors.Hand.Size.Width, Cursors.Hand.Size.Height, PixelFormat.Format32bppArgb);
      g = Graphics.FromImage(handBitmap);
      Cursors.Hand.Draw(g, new Rectangle(0, 0, handBitmap.Width, handBitmap.Height));

      helpBitmap = new Bitmap(Cursors.Help.Size.Width, Cursors.Help.Size.Height, PixelFormat.Format32bppArgb);
      g = Graphics.FromImage(helpBitmap);
      Cursors.Help.Draw(g, new Rectangle(0, 0, helpBitmap.Width, helpBitmap.Height));

      sizeAllBitmap = new Bitmap(Cursors.SizeAll.Size.Width, Cursors.SizeAll.Size.Height, PixelFormat.Format32bppArgb);
      g = Graphics.FromImage(sizeAllBitmap);
      Cursors.SizeAll.Draw(g, new Rectangle(0, 0, sizeAllBitmap.Width, sizeAllBitmap.Height));

      upArrowBitmap = new Bitmap(Cursors.UpArrow.Size.Width, Cursors.UpArrow.Size.Height, PixelFormat.Format32bppArgb);
      g = Graphics.FromImage(upArrowBitmap);
      Cursors.UpArrow.Draw(g, new Rectangle(0, 0, upArrowBitmap.Width, upArrowBitmap.Height));

      waitCursorBitmap = new Bitmap(Cursors.WaitCursor.Size.Width, Cursors.WaitCursor.Size.Height, PixelFormat.Format32bppArgb);
      g = Graphics.FromImage(waitCursorBitmap);
      Cursors.WaitCursor.Draw(g, new Rectangle(0, 0, waitCursorBitmap.Width, waitCursorBitmap.Height));

      mouseCursorBitmap = Properties.Resources.Mouse;
      leftMouseCursorBitmap = Properties.Resources.MouseLeft;
      rightMouseCursorBitmap = Properties.Resources.MouseRight;
    }

    /// <summary>
    /// This method draws a custom drawn mouse cursor with pressed button in red.
    /// </summary>
    /// <param name="graphics">The <see cref="Graphics"/> to draw to.</param>
    /// <param name="mouseButtons">The <see cref="MouseButtons"/> that is pressed.</param>
    /// <param name="bounds">The <see cref="RectangleF"/> with the bounds to draw the cursor in.</param>
    private void DrawMouseCursor(Graphics graphics, MouseButtons mouseButtons, RectangleF bounds)
    {
      GraphicsPath path = new GraphicsPath();
      int aThird = (int)(bounds.Height / 3f);
      int aThirdWidth = (int)(bounds.Width / 3f);

      RectangleF topRect = bounds;
      topRect.Height -= 2 * aThird;
      topRect.Width = (int)(7 / 12f * bounds.Width);

      RectangleF middleRect = topRect;
      middleRect.Offset(0.5f * aThirdWidth, topRect.Height);

      RectangleF bottomRect = bounds;
      bottomRect.Width = (int)(7 / 12f * bounds.Width);
      bottomRect.Offset(0.5f * aThirdWidth, 0);
      bottomRect.Height -= (int)(1 / 12f * bounds.Height);

      topRect.Height += aThird;
      topRect.Offset(0.5f * aThirdWidth, 0);

      // Create bounding shape of cursor
      path.AddArc(topRect, 180, 180);
      path.AddArc(bottomRect, 0, 180);
      path.CloseFigure();

      // Fill with shadow color
      graphics.FillPath(SystemBrushes.ButtonShadow, path);

      // Add red colored left mouse button
      if (mouseButtons == MouseButtons.Left)
      {
        GraphicsPath pathLeftButton = new GraphicsPath();
        pathLeftButton.AddArc(topRect, 180, 90);

        pathLeftButton.AddLine(
          (int)(topRect.Left + (topRect.Width / 2f)),
          topRect.Top,
          (int)(topRect.Left + (topRect.Width / 2f)),
          middleRect.Top);

        pathLeftButton.AddLine(
          middleRect.Left,
          middleRect.Top,
          middleRect.Left + (int)(middleRect.Width / 2f),
          middleRect.Top);

        pathLeftButton.CloseFigure();

        graphics.FillPath(this.Brush, pathLeftButton);
      }

      // Add red colored right mouse button
      if (mouseButtons == MouseButtons.Right)
      {
        GraphicsPath pathRightButton = new GraphicsPath();
        pathRightButton.AddArc(topRect, 270, 90);
        pathRightButton.AddLine(
          (int)(topRect.Left + (topRect.Width / 2f)),
          topRect.Top,
          (int)(topRect.Left + (topRect.Width / 2f)),
          middleRect.Top);

        pathRightButton.AddLine(
          middleRect.Left,
          middleRect.Top,
          middleRect.Left + (int)(middleRect.Width / 2f),
          middleRect.Top);

        pathRightButton.CloseFigure();

        graphics.FillPath(this.Brush, pathRightButton);
      }

      // Draw buttons margin button
      graphics.DrawPath(this.Pen, path);

      // Draw buttons mouse-body line separator
      graphics.DrawLine(this.Pen, middleRect.Left, middleRect.Top, middleRect.Right, middleRect.Top);

      // Draw line between buttons
      graphics.DrawLine(
        this.Pen,
        (int)(topRect.Left + (topRect.Width / 2f)),
        topRect.Top,
        (int)(topRect.Left + (topRect.Width / 2f)),
        middleRect.Top);
    }

    #endregion //METHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER

    /// <summary>
    /// Calculates the moved bounds when taking the hot spot
    /// of the cursor into account.
    /// </summary>
    /// <param name="cursor">The <see cref="Cursor"/> with hot spot to calculate moved bounds for.</param>
    /// <returns>A <see cref="RectangleF"/> with the moved bounds.</returns>
    private RectangleF GetOffsetBounds(Cursor cursor)
    {
      float factor = this.Width / cursor.Size.Width;
      RectangleF offsetBounds = this.Bounds;
      offsetBounds.Offset(
        (this.Width / 2) - (int)(cursor.HotSpot.X * factor),
        (this.Height / 2) - (int)(cursor.HotSpot.Y * factor));
      return offsetBounds;
    }

    #endregion //HELPER
  }
}
