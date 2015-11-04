#region - Terms of Usage -
/*
 * Copyright 2006 Sameera Perera
 *
 *  Licensed under the Apache License, Version 2.0 (the "License");
 *  you may not use this file except in compliance with the License.
 *  You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 *  Unless required by applicable law or agreed to in writing, software
 *  distributed under the License is distributed on an "AS IS" BASIS,
 *  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 *  See the License for the specific language governing permissions and
 *  limitations under the License.
 *
*/
#endregion

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace OgamaControls
{
  [DesignTimeVisible(false)]
  class GradientBuilder : Control
  {
    ///////////////////////////////////////////////////////////////////////////////
    // Defining Constants                                                        //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTANTS
    /// <summary>
    /// Default padding with around gradient rectangle
    /// </summary>
    private const int DEFAULT_PADDING = 10;
    #endregion //CONSTANTS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Variables, Enumerations, Events                                  //
    ///////////////////////////////////////////////////////////////////////////////
    #region FIELDS

    /// <summary>
    /// Rectangle that bounds gradient bar.
    /// </summary>
    private Rectangle gradBarRect;
    /// <summary>
    /// Rectangle that bounds the strip for the color markers.
    /// </summary>
    private Rectangle markerStrip;
    /// <summary>
    /// Rectangle with compensated values for the color marker strip
    /// </summary>
    private Rectangle compensatedMarkerStrip;

    /// <summary>
    /// List of color blend markers.
    /// </summary>
    private List<Marker> markers;

    /// <summary>
    /// Currently selected marker.
    /// </summary>
    private Marker selectedMarker = null;

    /// <summary>
    /// Currently moving marker.
    /// </summary>
    private Marker movingMarker = null;

    /// <summary>
    /// Bitmap of the current gradient.
    /// </summary>
    private Bitmap gradientBmp = null;

    /// <summary>
    /// Flag. True, if bitmap should be renewed.
    /// </summary>
    private bool isBitmapInvalid = true;

    /// <summary>
    /// Current gradient object.
    /// </summary>
    private Gradient gradient;

    /// <summary>
    /// Flag. True if markers should be set silent.
    /// </summary>
    private bool silentMarkers = false;

    /// <summary>
    /// Event. Raised when a marker is selected
    /// </summary>
    public event EventHandler MarkerSelected;

    /// <summary>
    /// Event. Raised, when gradient changed
    /// </summary>
    public event EventHandler GradientChanged;


    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES
    /// <summary>
    /// Gets or sets gradient object of the control
    /// </summary>
    [DefaultValue(null), Browsable(false)]
    public Gradient Gradient
    {
      get { return gradient; }
      set
      {
        gradient = value;
        // Create markers from the gradient
        ColorBlend blend = gradient.ColorBlend;

        markers.Clear();

        Marker marker = new Marker(0.0f, blend.Colors[0], false);
        marker.MarkerUpdated += new EventHandler(marker_MarkerUpdated);
        markers.Add(marker);

        for (int i = 1; i < blend.Colors.Length - 1; i++)
        {
          marker = new Marker(blend.Positions[i], blend.Colors[i]);
          marker.MarkerUpdated += new EventHandler(marker_MarkerUpdated);
          markers.Add(marker);
        }

        marker = new Marker(1.0f, blend.Colors[blend.Colors.Length - 1], false);
        marker.MarkerUpdated += new EventHandler(marker_MarkerUpdated);
        markers.Add(marker);

        this.Invalidate();
        OnGradientChanged(false);
      }
    }

    /// <summary>
    /// Gets or sets whether the GradientChanged event is fired when a marker
    /// is dragged.
    /// </summary>
    [DefaultValue(false)]
    public bool SilentMarkers
    {
      get { return silentMarkers; }
      set { silentMarkers = value; }
    }

    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Constructor. Initializes gradient builder control.
    /// </summary>
    public GradientBuilder()
    {
      SetStyle(ControlStyles.AllPaintingInWmPaint, true);
      SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
      SetStyle(ControlStyles.ResizeRedraw, true);
      SetStyle(ControlStyles.UserPaint, true);

      this.gradient = new Gradient();
      this.gradient.GradientDirection = LinearGradientMode.Horizontal;
      this.markers = new List<Marker>(2);

      Marker startMarker, endMarker;
      startMarker = new Marker(0.0f, Color.Black, false);
      endMarker = new Marker(1.0f, Color.White, false);
      markers.Add(startMarker);
      markers.Add(endMarker);

      startMarker.MarkerUpdated += new EventHandler(marker_MarkerUpdated);
      endMarker.MarkerUpdated += new EventHandler(marker_MarkerUpdated);
    }

    #endregion //CONSTRUCTION

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

    /// <summary>
    /// Invoked, when gradient changed. Updates gradient object with new color blends.
    /// </summary>
    /// <param name="suppressEvent">True, if gradient changed event should be suppressed.</param>
    protected virtual void OnGradientChanged(bool suppressEvent)
    {
      isBitmapInvalid = true;
      gradient.ColorBlend = GetBlendFromMarkers();
      Invalidate();

      if (!suppressEvent && GradientChanged != null)
        GradientChanged(this.Parent, EventArgs.Empty);
    }

    /// <summary>
    /// Raises MarkerSelected event.
    /// </summary>
    /// <param name="selectedMarker">currently selected Marker</param>
    protected virtual void OnMarkerSelected(Marker selectedMarker)
    {
      this.selectedMarker = selectedMarker;

      Rectangle invalidRect = markerStrip;
      invalidRect.Inflate((int)selectedMarker.Bounds.Width, 0);
      this.Invalidate(invalidRect);

      if (MarkerSelected != null)
        MarkerSelected(selectedMarker, EventArgs.Empty);
    }

    /// <summary>
    /// Raises <c>OnGradientChanged</c> event when marker position changed.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    void marker_MarkerUpdated(object sender, EventArgs e)
    {
      OnGradientChanged(false);
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

    /// <summary>
    /// Override. Recalculates gradient bitmap on resizing.
    /// </summary>
    /// <param name="e">Empty event args</param>
    protected override void OnResize(EventArgs e)
    {
      base.OnResize(e);
      Recalculate();
    }

    /// <summary>
    /// Override. Paints gradient and marker of the control.
    /// </summary>
    /// <param name="e"></param>
    protected override void OnPaint(PaintEventArgs e)
    {
      base.OnPaintBackground(e);

      Graphics g = e.Graphics;
      // Determine what to draw. Don't rebuild the gradient when we have a 
      // cached bitmap, or when there is a marker being moved.
      bool suppressBmp = (movingMarker != null);

      // Draw the markers
      DrawMarkers(g);

      // Paint the checkerboard style background 
      using (HatchBrush hb = new HatchBrush(HatchStyle.LargeCheckerBoard, Color.White, Color.LightGray))
        g.FillRectangle(hb, gradBarRect);

      // If we are rebuilding the gradient, get the appropriate color blend
      if (!isBitmapInvalid)
        g.DrawImage(this.gradientBmp, gradBarRect);
      else if (suppressBmp)
      {
        // Simply paint the gradient when a marker is being moved. No use
        // painting it on to a bitmap just yet.
        this.gradient.PaintGradientWithDirectionOverride(
                        g, gradBarRect, LinearGradientMode.Horizontal);
      }
      else
      {
        //if (this.gradientBmp == null)
          this.gradientBmp = new Bitmap(gradBarRect.Width,
                                      gradBarRect.Height,
                                      PixelFormat.Format32bppArgb);

        // Cache the gradient by painting it onto a bitmap
        Graphics gBmp = Graphics.FromImage(gradientBmp);
        Rectangle bmpRct = new Rectangle(0, 0, gradBarRect.Width, gradBarRect.Height);

        using (HatchBrush hb = new HatchBrush(HatchStyle.LargeCheckerBoard, Color.White, Color.LightGray))
          gBmp.FillRectangle(hb, bmpRct);
        this.gradient.PaintGradientWithDirectionOverride(
                                    gBmp, bmpRct,
                                    LinearGradientMode.Horizontal);
        g.DrawImage(this.gradientBmp, gradBarRect);
      }

      // Draw the border around the gradient
      g.DrawRectangle(Pens.Black, gradBarRect);
    }

    /// <summary>
    /// Override. Can a marker (other than the first or the last) receive this 
    /// mouse down event?
    /// </summary>
    /// <param name="e">MouseEventArgs</param>
    protected override void OnMouseDown(MouseEventArgs e)
    {
      for (int i = 1; i < markers.Count - 1; i++)
        if (markers[i].Bounds.Contains(e.X, e.Y))
        {
          movingMarker = markers[i];
          break;
        }
      if (movingMarker != null)
      {
        selectedMarker = movingMarker;
        OnMarkerSelected(movingMarker);
      }
      else
      {
        if (markers[0].Bounds.Contains(e.X, e.Y))
          OnMarkerSelected(markers[0]);
        else if (markers[markers.Count - 1].Bounds.Contains(e.X, e.Y))
          OnMarkerSelected(markers[markers.Count - 1]);
      }
      Invalidate();
    }

    /// <summary>
    /// Override. Move the marker along the marker strip. If the marker is dragged out
    /// of the marker strip then the marker is removed.
    /// </summary>
    /// <param name="e">MouseEventArgs</param>
    protected override void OnMouseMove(MouseEventArgs e)
    {
      if (markerStrip.Contains(e.X, e.Y) && movingMarker != null)
      {
        this.Cursor = Cursors.Hand;
        float a = markerStrip.Width;
        movingMarker.SetPositionSilently((e.X - DEFAULT_PADDING) / a);
        OnGradientChanged(silentMarkers);
      }
      else if (movingMarker != null) // Remove the marker
      {
        //this.markers.Remove(movingMarker);
        //this.movingMarker = null;
        this.Cursor = Cursors.No;
        OnGradientChanged(false);
      }
    }

    /// <summary>
    /// Override. Updates gradient with changes.
    /// </summary>
    /// <param name="e">MouseEventArgs</param>
    protected override void OnMouseUp(MouseEventArgs e)
    {
      this.Cursor = Cursors.Default;
      if (movingMarker != null) // A movement was done. Resort the array
      {
        markers.Sort(CompareMarkers);
        Invalidate(gradBarRect);
        //selectedMarker = movingMarker;

        //OnMarkerSelected(movingMarker);

        if (!markerStrip.Contains(e.X, e.Y))
        {
          this.markers.Remove(movingMarker);
          this.movingMarker = null;
        }
        // Notify that we have a new gradient
        OnGradientChanged(false);
      }
      //else if (markers[0].Bounds.Contains(e.X, e.Y))
      //  OnMarkerSelected(markers[0]);
      //else if (markers[markers.Count - 1].Bounds.Contains(e.X, e.Y))
      //  OnMarkerSelected(markers[markers.Count - 1]);

      movingMarker = null;
    }

    /// <summary>
    /// Override.  If an location on the marker strip is double clicked where there
    /// are no existing markers, a new marker is added.
    /// </summary>
    /// <param name="e"></param>
    protected override void OnMouseDoubleClick(MouseEventArgs e)
    {
      Point pt = new Point(e.X, e.Y);

      if (gradBarRect.Contains(pt))
      {
        int i = 0;
        for (; i < markers.Count; i++)
          if (markers[i].Bounds.Contains(pt))
            return;
          else if (markers[i].Bounds.X > pt.X)
            break;

        float a, b;
        a = (float)e.X - DEFAULT_PADDING;
        b = (float)markerStrip.Width;

        Marker newMarker = new Marker(a / b);
        newMarker.UpdateBounds(compensatedMarkerStrip);

        // Select the color for the new marker. It should be the current 
        // color of the gradient at the marker point.
        if (this.gradientBmp != null)
          newMarker.Color = gradientBmp.GetPixel(
                                  (int)newMarker.Bounds.X, 0);

        newMarker.MarkerUpdated += new EventHandler(marker_MarkerUpdated);
        markers.Insert(i, newMarker);

        // Notify that we have a new gradient
        OnGradientChanged(false);
        OnMarkerSelected(newMarker);
      }
    }

    #endregion //OVERRIDES

    ///////////////////////////////////////////////////////////////////////////////
    // Methods for doing main class job                                          //
    ///////////////////////////////////////////////////////////////////////////////
    #region METHODS
    /// <summary>
    /// Resize bounds of gradient bar and marker strip.
    /// </summary>
    private void Recalculate()
    {

      gradBarRect = new Rectangle(DEFAULT_PADDING, DEFAULT_PADDING,
                      this.ClientRectangle.Width - DEFAULT_PADDING * 2,
                      this.ClientRectangle.Height - DEFAULT_PADDING * 3);
      markerStrip = new Rectangle(DEFAULT_PADDING, gradBarRect.Bottom+2,
                      gradBarRect.Width,
                      Marker.MARKER_SIZE);

      compensatedMarkerStrip = new Rectangle(
                  markerStrip.X, markerStrip.Y,
                  markerStrip.Width,// - DEFAULT_PADDING-2,
                  markerStrip.Height);
      foreach (Marker mrk in markers)
        mrk.UpdateBounds(compensatedMarkerStrip);

      isBitmapInvalid = true;
    }

    /// <summary>
    /// Draw Markers in the list
    /// </summary>
    /// <param name="g">graphics context</param>
    private void DrawMarkers(Graphics g)
    {
      foreach (Marker mrk in markers)
      {
        mrk.UpdateBounds(compensatedMarkerStrip);

        Pen shapePen=new Pen(Color.Transparent,1.5f);
        shapePen.DashCap = DashCap.Round;
        if (mrk.Equals(this.selectedMarker))
          shapePen.Color = Color.Black;

        GraphicsPath triangle = new GraphicsPath();
        triangle.AddLines(new PointF[] { 
        new PointF(mrk.Bounds.X + mrk.Bounds.Width / 2, mrk.Bounds.Y),
        new PointF(mrk.Bounds.X+mrk.Bounds.Width,mrk.Bounds.Y+mrk.Bounds.Height),
        new PointF(mrk.Bounds.X,mrk.Bounds.Y + mrk.Bounds.Height)
        });
        triangle.CloseFigure();

        using (HatchBrush hb = new HatchBrush(HatchStyle.LargeCheckerBoard, Color.White, Color.LightGray))
        {
          g.FillPath(hb, triangle);
        }

        using (SolidBrush br = new SolidBrush(mrk.Color))
        {
          g.FillPath(br, triangle);
        }

        g.DrawPath(shapePen,triangle);
      }
    }

    /// <summary>
    /// Calculates a colorblend object from marker positions to apply to gradient.
    /// </summary>
    /// <returns>Color blend for use in gradient.</returns>
    private ColorBlend GetBlendFromMarkers()
    {
      Color[] colors = new Color[markers.Count];
      float[] positions = new float[markers.Count];

      for (int i = 0; i < colors.Length; i++)
      {
        colors[i] = markers[i].Color;
        positions[i] = markers[i].Position;
      }
      ColorBlend blend = new ColorBlend(markers.Count);
      blend.Colors = colors;
      blend.Positions = positions;
      return blend;
    }
    #endregion //METHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER

    /// <summary>
    /// Compares to markers and returns true, if they are at the same position.
    /// </summary>
    /// <param name="a">Marker 1</param>
    /// <param name="b">Marker 2</param>
    /// <returns>true if at the same place</returns>
    private static int CompareMarkers(Marker a, Marker b)
    {
      return a.Position.CompareTo(b.Position);
    }

    #endregion //HELPER

  }
}
