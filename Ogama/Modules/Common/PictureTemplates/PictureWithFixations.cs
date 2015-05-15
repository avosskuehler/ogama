// <copyright file="PictureWithFixations.cs" company="FU Berlin">
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

namespace Ogama.Modules.Common.PictureTemplates
{
  using System;
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.Data;
  using System.Drawing;
  using System.Drawing.Drawing2D;
  using System.Drawing.Imaging;
  using System.Windows.Forms;

  using Ogama.ExceptionHandling;
  using Ogama.Modules.AttentionMap;
  using Ogama.Modules.Common.Types;

  using VectorGraphics.Canvas;
  using VectorGraphics.Elements;
  using VectorGraphics.Elements.ElementCollections;
  using VectorGraphics.Tools.CustomEventArgs;
  using VectorGraphics.Tools.CustomTypeConverter;

  /// <summary>
  /// Derived from <see cref="Picture"/>.
  /// Used to display vector graphic elements for the modules that display fixations.
  /// </summary>
  public partial class PictureWithFixations : Picture
  {
    ///////////////////////////////////////////////////////////////////////////////
    // Defining Constants                                                        //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTANTS

    /// <summary>
    /// This value is the size of the fixed size fixation dots in the
    /// <see cref="FixationDrawingMode.Dots"/> drawing mode in pixel.
    /// </summary>
    private const int DOTSIZE = 12;

    #endregion //CONSTANTS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Variables, Enumerations, Events                                  //
    ///////////////////////////////////////////////////////////////////////////////
    #region FIELDS

    /// <summary>
    /// This region is the inverted region of the spotlights.
    /// </summary>
    private VGRegion spotlightRegion;

    /// <summary>
    /// This member saves the location of the foregoing fixation.
    /// </summary>
    private PointF foregoingFixationLocation = PointF.Empty;

    /// <summary>
    /// Pen used for gaze fixation circles
    /// </summary>
    private Pen gazeFixationsPen;

    /// <summary>
    /// Pen used for gaze fixation connections
    /// </summary>
    private Pen gazeFixationConnectionsPen;

    /// <summary>
    /// Font for gaze fixation numeration
    /// </summary>
    private Font gazeFixationsFont;

    /// <summary>
    /// Color used for gaze fixation numeration
    /// </summary>
    private Color gazeFixationsFontColor;

    /// <summary>
    /// Divisor to reduce diameter of gaze fixations according to time length.
    /// </summary>
    private float gazeFixationsDiameterDivisor;

    /// <summary>
    /// Saves whether the gaze fixations should be connected with lines.
    /// </summary>
    private bool gazeConnections;

    /// <summary>
    /// Saves whether the gaze fixations should be enumerated.
    /// </summary>
    private bool gazeNumbers;

    /// <summary>
    /// Pen used for mouse fixation circles
    /// </summary>
    private Pen mouseFixationsPen;

    /// <summary>
    /// Pen used for mouse fixation connections
    /// </summary>
    private Pen mouseFixationConnectionsPen;

    /// <summary>
    /// Font for mouse fixation numeration
    /// </summary>
    private Font mouseFixationsFont;

    /// <summary>
    /// Color used for mouse fixation numeration
    /// </summary>
    private Color mouseFixationsFontColor;

    /// <summary>
    /// Divisor to reduce diameter of mouse fixations according to time length.
    /// </summary>
    private float mouseFixationsDiameterDivisor;

    /// <summary>
    /// Saves whether the mouse fixations should be connected with lines.
    /// </summary>
    private bool mouseConnections;

    /// <summary>
    /// Saves whether the mouse fixations should be enumerated.
    /// </summary>
    private bool mouseNumbers;

    /// <summary>
    /// Flag that saves drawing mode enumeration for gaze drawing.
    /// </summary>
    /// <see cref="FixationDrawingMode"/>
    private FixationDrawingMode gazeDrawingMode;

    /// <summary>
    /// Flag that saves drawing mode enumeration for mouse drawing.
    /// </summary>
    /// <see cref="FixationDrawingMode"/>
    private FixationDrawingMode mouseDrawingMode;

    /// <summary>
    /// This table holds the gaze fixation data for the selected image.
    /// </summary>
    private DataTable gazeFixations;

    /// <summary>
    /// This table holds the mouse fixation data for the selected image.
    /// </summary>
    private DataTable mouseFixations;

    /// <summary>
    /// This DataView holds the subset of gaze fixations for
    /// a specific subject.
    /// </summary>
    private DataView gazeFixationsView;

    /// <summary>
    /// This DataView holds the subset of mouse fixations for
    /// a specific subject.
    /// </summary>
    private DataView mouseFixationsView;

    /// <summary>
    /// Saves the type of samples that should be drawn, gaze, mouse or both.
    /// </summary>
    private SampleType sampleTypeToDraw;

    /// <summary>
    /// Saves the AOI data for the current trial.
    /// </summary>
    private DataTable aoiTable;

    /// <summary>
    /// Bitmap to fill with gradient to grab single color values.
    /// </summary>
    private PaletteBitmap colorMap;

    /// <summary>
    /// Bitmap to fill with heat map to overlay on background.
    /// </summary>
    private PaletteBitmap heatMap;

    /// <summary>
    /// Array of attention map distributions
    /// </summary>
    private float[,] distributionArray;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the PictureWithFixations class.
    /// </summary>
    public PictureWithFixations()
      : base()
    {
      this.InitializeComponent();
      this.InitializeElements();
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets the bitmap to fill with gradient to grab single color values.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public PaletteBitmap ColorMap
    {
      get { return this.colorMap; }
    }

    /// <summary>
    /// Gets the bitmap to fill with heat map to overlay on background.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public PaletteBitmap HeatMap
    {
      get { return this.heatMap; }
    }

    /// <summary>
    /// Gets or sets the AOI data table that should be drawn
    /// </summary>
    /// <value>A <see cref="DataTable"/> with the AOI data for the current trial.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public DataTable AOITable
    {
      get { return this.aoiTable; }
      set { this.aoiTable = value; }
    }

    #region GAZE

    /// <summary>
    /// Gets the <see cref="DataView"/> with the subset of gaze fixations for
    /// a specific subject.
    /// </summary>
    public DataView GazeFixationsView
    {
      get { return this.gazeFixationsView; }
    }

    /// <summary>
    /// Gets or sets the gaze fixation data for the selected trial.
    /// </summary>
    /// <value>A <see cref="DataTable"/> with the gaze fixations for the trial.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public DataTable GazeFixations
    {
      get
      {
        return this.gazeFixations;
      }

      set
      {
        this.gazeFixations = value;

        // Create corresponding DataView with empty row filter.
        if (value != null)
        {
          this.gazeFixationsView = new DataView(
            this.gazeFixations,
           string.Empty,
           "CountInTrial",
           DataViewRowState.CurrentRows);
        }
      }
    }

    /// <summary>
    /// Gets or sets gaze fixation pen.
    /// </summary>
    /// <value>A <see cref="Pen"/> with the style for the gaze fixation drawing.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Pen GazeFixationsPen
    {
      get { return this.gazeFixationsPen; }
      set { this.gazeFixationsPen = value; }
    }

    /// <summary>
    /// Gets or sets gaze fixation connection pen.
    /// </summary>
    /// <value>A <see cref="Pen"/> with the style for the gaze fixation connection drawing.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Pen GazeFixationConnectionsPen
    {
      get { return this.gazeFixationConnectionsPen; }
      set { this.gazeFixationConnectionsPen = value; }
    }

    /// <summary>
    /// Gets or sets font for gaze fixation enumeration
    /// </summary>
    /// <value>A <see cref="Font"/> with the style for the gaze fixation numbers.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Font GazeFixationsFont
    {
      get { return this.gazeFixationsFont; }
      set { this.gazeFixationsFont = value; }
    }

    /// <summary>
    /// Gets or sets color for gaze fixation enumeration.
    /// </summary>
    /// <value>A <see cref="Color"/> for the gaze fixation numbers.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Color GazeFixationsFontColor
    {
      get { return this.gazeFixationsFontColor; }
      set { this.gazeFixationsFontColor = value; }
    }

    /// <summary>
    /// Sets gaze fixation diameter divisor.
    /// </summary>
    /// <value>A <see cref="Single"/> with the divisor to 
    /// reduce diameter of gaze fixations according to time length. </value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public float GazeFixationsDiameterDivisor
    {
      set
      {
        this.gazeFixationsDiameterDivisor = value;
        this.DrawFixations(true);
      }
    }

    /// <summary>
    /// Sets a value indicating whether the gaze fixations should be connected with lines.
    /// </summary>
    /// <value>A <see cref="Boolean"/> indicating whether the gaze fixations
    /// should be connected with lines.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool GazeConnections
    {
      set
      {
        this.gazeConnections = value;
        this.DrawFixations(true);
      }
    }

    /// <summary>
    /// Sets a value indicating whether the gaze fixations should be be enumerated.
    /// </summary>
    /// <value>A <see cref="Boolean"/> indicating whether the gaze fixations
    /// should be enumerated.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool GazeNumbers
    {
      set
      {
        this.gazeNumbers = value;
        this.DrawFixations(true);
      }
    }

    /// <summary>
    /// Gets or sets gaze fixation drawing mode
    /// </summary>
    /// <value>A <see cref="FixationDrawingMode"/> enumeration member.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public FixationDrawingMode GazeDrawingMode
    {
      get
      {
        return this.gazeDrawingMode;
      }

      set
      {
        this.gazeDrawingMode = value;
        this.DrawFixations(true);
      }
    }

    #endregion //GAZE

    #region MOUSE

    /// <summary>
    /// Gets the <see cref="DataView"/> with the subset of mouse fixations for
    /// a specific subject.
    /// </summary>
    public DataView MouseFixationsView
    {
      get { return this.gazeFixationsView; }
    }

    /// <summary>
    /// Gets or sets the mouse fixation data for the selected trial.
    /// </summary>
    /// <value>A <see cref="DataTable"/> with the mouse fixations for the trial.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public DataTable MouseFixations
    {
      get
      {
        return this.mouseFixations;
      }

      set
      {
        this.mouseFixations = value;

        if (value != null)
        {
          // Create corresponding DataView with empty row filter.
          this.mouseFixationsView = new DataView(
            this.mouseFixations,
            string.Empty,
            "CountInTrial",
            DataViewRowState.CurrentRows);
        }
      }
    }

    /// <summary>
    /// Gets or sets mouse fixation pen.
    /// </summary>
    /// <value>A <see cref="Pen"/> with the style for the mouse fixation drawing.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Pen MouseFixationsPen
    {
      get { return this.mouseFixationsPen; }
      set { this.mouseFixationsPen = value; }
    }

    /// <summary>
    /// Gets or sets mouse fixation connection pen.
    /// </summary>
    /// <value>A <see cref="Pen"/> with the style for the mouse fixation connection drawing.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Pen MouseFixationConnectionsPen
    {
      get { return this.mouseFixationConnectionsPen; }
      set { this.mouseFixationConnectionsPen = value; }
    }

    /// <summary>
    /// Gets or sets font for mouse fixation enumeration
    /// </summary>
    /// <value>A <see cref="Font"/> with the style for the mouse fixation numbers.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Font MouseFixationsFont
    {
      get { return this.mouseFixationsFont; }
      set { this.mouseFixationsFont = value; }
    }

    /// <summary>
    /// Gets or sets color for mouse fixation enumeration
    /// </summary>
    /// <value>A <see cref="Color"/> for the mouse fixation numbers.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Color MouseFixationsFontColor
    {
      get { return this.mouseFixationsFontColor; }
      set { this.mouseFixationsFontColor = value; }
    }

    /// <summary>
    /// Sets mouse fixation diameter divisor.
    /// </summary>
    /// <value>A <see cref="Single"/> with the divisor to 
    /// reduce diameter of mouse fixations according to time length. </value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public float MouseFixationsDiameterDivisor
    {
      set
      {
        this.mouseFixationsDiameterDivisor = value;
        this.DrawFixations(true);
      }
    }

    /// <summary>
    /// Sets a value indicating whether the gaze fixations should be connected with lines.
    /// </summary>
    /// <value>A <see cref="Boolean"/> indicating whether the gaze fixations
    /// should be connected with lines.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool MouseConnections
    {
      set
      {
        this.mouseConnections = value;
        this.DrawFixations(true);
      }
    }

    /// <summary>
    /// Sets a value indicating whether the gaze fixations should be be enumerated.
    /// </summary>
    /// <value>A <see cref="Boolean"/> indicating whether the mouse fixations
    /// should be enumerated.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool MouseNumbers
    {
      set
      {
        this.mouseNumbers = value;
        this.DrawFixations(true);
      }
    }

    /// <summary>
    /// Gets or sets mouse fixation drawing mode
    /// </summary>
    /// <value>A <see cref="FixationDrawingMode"/> enumeration member.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public FixationDrawingMode MouseDrawingMode
    {
      get
      {
        return this.mouseDrawingMode;
      }

      set
      {
        this.mouseDrawingMode = value;
        this.DrawFixations(true);
      }
    }

    /// <summary>
    /// Sets the type of samples that should be drawn
    /// </summary>
    /// <value>A <see cref="SampleType"/> which determines which samples
    /// should be drawn, gaze, mouse or both.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public SampleType SampleTypeToDraw
    {
      set { this.sampleTypeToDraw = value; }
    }

    #endregion //MOUSE

    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Public methods                                                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region PUBLICMETHODS

    /// <summary>
    /// This method saves the current pen and font styles to the application settings.
    /// </summary>
    public void SaveStylesToApplicationSettings()
    {
      // AppSettings are always set except when in design mode
      if (Properties.Settings.Default != null)
      {
        Properties.Settings.Default.GazeFixationsPenColor = this.gazeFixationsPen.Color;
        Properties.Settings.Default.GazeFixationsPenWidth = this.gazeFixationsPen.Width;
        Properties.Settings.Default.GazeFixationsPenStyle = this.gazeFixationsPen.DashStyle;

        Properties.Settings.Default.GazeFixationConnectionsPenColor = this.gazeFixationConnectionsPen.Color;
        Properties.Settings.Default.GazeFixationConnectionsPenWidth = this.gazeFixationConnectionsPen.Width;
        Properties.Settings.Default.GazeFixationConnectionsPenStyle = this.gazeFixationConnectionsPen.DashStyle;

        Properties.Settings.Default.GazeFixationsFont = (Font)this.gazeFixationsFont.Clone();
        Properties.Settings.Default.GazeFixationsFontColor = this.gazeFixationsFontColor;

        Properties.Settings.Default.GazeConnections = this.gazeConnections;
        Properties.Settings.Default.GazeNumbers = this.gazeNumbers;

        Properties.Settings.Default.GazeFixationsDrawingMode = this.gazeDrawingMode.ToString();

        Properties.Settings.Default.MouseFixationsPenColor = this.mouseFixationsPen.Color;
        Properties.Settings.Default.MouseFixationsPenWidth = this.mouseFixationsPen.Width;
        Properties.Settings.Default.MouseFixationsPenStyle = this.mouseFixationsPen.DashStyle;

        Properties.Settings.Default.MouseFixationConnectionsPenColor = this.mouseFixationConnectionsPen.Color;
        Properties.Settings.Default.MouseFixationConnectionsPenWidth = this.mouseFixationConnectionsPen.Width;
        Properties.Settings.Default.MouseFixationConnectionsPenStyle = this.mouseFixationConnectionsPen.DashStyle;

        Properties.Settings.Default.MouseFixationsFont = (Font)this.mouseFixationsFont.Clone();
        Properties.Settings.Default.MouseFixationsFontColor = this.mouseFixationsFontColor;

        Properties.Settings.Default.MouseConnections = this.mouseConnections;
        Properties.Settings.Default.MouseNumbers = this.mouseNumbers;

        Properties.Settings.Default.MouseFixationsDrawingMode = this.mouseDrawingMode.ToString();

        Properties.Settings.Default.Save();
      }
    }

    /// <summary>
    /// Eventhandler for the <see cref="OgamaControls.Dialogs.PenStyleDlg.PenChanged"/> event. 
    /// Updates all graphic elements from the given group
    /// with the new pen.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A <see cref="PenChangedEventArgs"/> that hold new group and pen</param>
    public void PenChanged(object sender, PenChangedEventArgs e)
    {
      VGElementCollection sublist = this.Elements.FindAllGroupMembers(e.ElementGroup);
      foreach (VGElement element in sublist)
      {
        element.Pen = e.Pen;
      }

      this.DrawForeground(false);

      switch (e.ElementGroup)
      {
        case VGStyleGroup.FIX_GAZE_ELEMENT:
          this.gazeFixationsPen = e.Pen;
          break;
        case VGStyleGroup.FIX_MOUSE_ELEMENT:
          this.mouseFixationsPen = e.Pen;
          break;
      }
    }

    /// <summary>
    /// Eventhandler for the <see cref="OgamaControls.Dialogs.FontStyleDlg.FontStyleChanged"/> event. 
    /// Updates all graphic elements from the given group
    /// with the new font.
    /// </summary>
    /// <param name="sender">message sender</param>
    /// <param name="e">Font change event arguments that hold new group and font and brush</param>
    public void FontStyleChanged(object sender, FontChangedEventArgs e)
    {
      VGElementCollection sublist = this.Elements.FindAllGroupMembers(e.ElementGroup);
      foreach (VGElement element in sublist)
      {
        element.Font = e.Font;
        element.FontColor = e.FontColor;
      }

      this.DrawForeground(false);

      switch (e.ElementGroup)
      {
        case VGStyleGroup.FIX_GAZE_ELEMENT:
          this.gazeFixationsFont = e.Font;
          this.gazeFixationsFontColor = e.FontColor;
          break;
        case VGStyleGroup.FIX_MOUSE_ELEMENT:
          this.mouseFixationsFont = e.Font;
          this.mouseFixationsFontColor = e.FontColor;
          break;
      }
    }

    /// <summary>
    /// Master Drawing routine, switches <see cref="FixationDrawingMode"/> for
    /// gaze and mouse samples.
    /// </summary>
    /// <param name="resetPicture"><strong>True</strong>, if picture should be
    /// erased and reset before drawing, otherwise <strong>false</strong>.</param>
    public virtual void DrawFixations(bool resetPicture)
    {
      this.Cursor = Cursors.WaitCursor;

      if (resetPicture)
      {
        this.ResetPicture();
        this.ResetForegoingFixationLocation();
      }

      // Draw AOIs if there are any.
      if (this.AOITable != null)
      {
        this.DrawAOI(this.AOITable);
      }

      if (!this.CheckForValidFixations())
      {
        this.DrawForeground(resetPicture);
        this.Cursor = Cursors.Default;
        return;
      }

      this.DrawFixationsForCurrentSubject();

      this.ApplyAttentionMap(SampleType.Both);

      this.DrawForeground(resetPicture);

      this.Cursor = Cursors.Default;
    }

    /// <summary>
    /// Creates vector graphic elements from the
    /// shapes that are listed in the given database table.
    /// </summary>
    /// <param name="aoiTable">Areas of interest <see cref="DataTable"/>.</param>
    public void DrawAOI(DataTable aoiTable)
    {
      VGElementCollection aois = this.GetAOIElements(aoiTable);
      foreach (VGElement element in aois)
      {
        this.Elements.Add(element);
      }
    }

    /// <summary>
    /// This method converts the AOI table with areas of interest from the database
    /// into a list of <see cref="VGElement"/>s.
    /// </summary>
    /// <param name="aoiTable">The <see cref="DataTable"/> with the AOIs.</param>
    /// <returns>A <see cref="List{VGElement}"/> with the shapes.</returns>
    protected virtual VGElementCollection GetAOIElements(DataTable aoiTable)
    {
      Pen defaultPen = new Pen(Properties.Settings.Default.AOIStandardColor, Properties.Settings.Default.AOIStandardWidth);
      Pen targetPen = new Pen(Properties.Settings.Default.AOITargetColor, Properties.Settings.Default.AOITargetWidth);
      Pen searchRectPen = new Pen(Properties.Settings.Default.AOISearchRectColor, Properties.Settings.Default.AOISearchRectWidth);
      Font defaultFont = (Font)Properties.Settings.Default.AOIStandardFont.Clone();
      Font targetFont = (Font)Properties.Settings.Default.AOITargetFont.Clone();
      Font searchRectFont = (Font)Properties.Settings.Default.AOISearchRectFont.Clone();
      Color defaultFontColor = Properties.Settings.Default.AOIStandardFontColor;
      Color targetFontColor = Properties.Settings.Default.AOITargetFontColor;
      Color searchRectFontColor = Properties.Settings.Default.AOISearchRectFontColor;

      VGElementCollection aoiList = new VGElementCollection();
      int counter = 0;

      try
      {
        foreach (DataRow row in aoiTable.Rows)
        {
          string strPtList = row["ShapePts"].ToString();
          string shapeName = row["ShapeName"].ToString();
          Pen usedPen = defaultPen;
          Font usedFont = defaultFont;
          Color usedFontColor = defaultFontColor;
          VGStyleGroup usedStyleGroup = VGStyleGroup.AOI_NORMAL;
          List<PointF> pointList = ObjectStringConverter.StringToPointFList(strPtList);
          string usedElementGroup = row["ShapeGroup"].ToString();
          switch (usedElementGroup)
          {
            case "Target":
              usedPen = targetPen;
              usedFont = targetFont;
              usedFontColor = targetFontColor;
              usedStyleGroup = VGStyleGroup.SCA_GRID_AOI;
              break;
            case "SearchRect":
              usedPen = searchRectPen;
              usedFont = searchRectFont;
              usedFontColor = searchRectFontColor;
              usedStyleGroup = VGStyleGroup.SCA_GRID_AOI;
              break;
            default:
              usedPen = defaultPen;
              usedFont = defaultFont;
              usedFontColor = defaultFontColor;
              usedStyleGroup = VGStyleGroup.SCA_GRID_AOI;
              break;
          }

          // Create the shape depending on ShapeType
          RectangleF boundingRect = new RectangleF();
          switch (row["ShapeType"].ToString())
          {
            case "Rectangle":
              boundingRect.Location = pointList[0];
              boundingRect.Width = pointList[2].X - pointList[0].X;
              boundingRect.Height = pointList[2].Y - pointList[0].Y;

              // Create Rect with defined stroke
              VGRectangle newRect = new VGRectangle(
                ShapeDrawAction.NameAndEdge,
                usedPen,
                usedFont,
                usedFontColor,
                boundingRect,
                usedStyleGroup,
                shapeName,
                usedElementGroup);

              aoiList.Add(newRect);
              break;
            case "Ellipse":
              boundingRect.Location = pointList[0];
              boundingRect.Width = pointList[2].X - pointList[0].X;
              boundingRect.Height = pointList[2].Y - pointList[0].Y;

              // Create Rect with defined stroke
              VGEllipse newEllipse = new VGEllipse(
                ShapeDrawAction.NameAndEdge,
                usedPen,
                usedFont,
                usedFontColor,
                boundingRect,
                usedStyleGroup,
                shapeName,
                usedElementGroup);

              aoiList.Add(newEllipse);
              break;
            case "Polyline":
              // Create Polyline with defined stroke
              VGPolyline newPolyline = new VGPolyline(
                ShapeDrawAction.NameAndEdge,
                usedPen,
                usedFont,
                usedFontColor,
                usedStyleGroup,
                shapeName,
                usedElementGroup);

              foreach (PointF point in pointList)
              {
                newPolyline.AddPt(point);
              }

              newPolyline.ClosePolyline();
              aoiList.Add(newPolyline);
              boundingRect = newPolyline.Bounds;
              break;
          }

          counter++;
        }
      }
      catch (Exception ex)
      {
        ExceptionMethods.HandleException(ex);
      }

      return aoiList;
    }

    /// <summary>
    /// Erases the foregoingFixationLocation
    /// </summary>
    protected void ResetForegoingFixationLocation()
    {
      this.foregoingFixationLocation = PointF.Empty;
      int eyeMonX = this.PresentationSize.Width;
      int eyeMonY = this.PresentationSize.Height;

      // Clear old attentionMaps
      this.distributionArray = new float[eyeMonX, eyeMonY];
    }

    /// <summary>
    /// This function sets a subject filter on the given fixation data table
    /// </summary>
    /// <param name="name">The subjects name for which the fixations should be shown.</param>
    /// <param name="starttime">A nullable <see cref="Int64"/> with the starttime
    /// of the time span to draw.</param>
    /// <param name="endtime">A nullable <see cref="Int64"/> with the ending time
    /// of the time span to draw.</param>
    protected void SetSubjectFilter(string name, long? starttime, long? endtime)
    {
      string subjectFilter = "(SubjectName='" + name + "')";

      // Set subject filter of datatable views if applicable.
      if (starttime != null && endtime != null)
      {
        subjectFilter = "(SubjectName='" + name + "' AND (Starttime>" + starttime.Value.ToString() + " AND Starttime<" + endtime.Value.ToString() + "))";
      }

      if (this.gazeFixationsView != null)
      {
        this.gazeFixationsView.RowFilter = subjectFilter;
      }

      if (this.mouseFixationsView != null)
      {
        this.mouseFixationsView.RowFilter = subjectFilter;
      }
    }

    /// <summary>
    /// THis method returns <strong>true</strong> if data
    /// is available for the choosen sample type.
    /// </summary>
    /// <returns><strong>True</strong> if data
    /// is available for the choosen sample type, otherwise <strong>false</strong>.</returns>
    protected bool CheckForValidFixations()
    {
      // Check for valid gaze fixation table.
      if ((this.sampleTypeToDraw == (this.sampleTypeToDraw | SampleType.Gaze))
        && this.gazeFixationsView == null)
      {
        return false;
      }

      // Check for valid mouse fixation table.
      if ((this.sampleTypeToDraw == (this.sampleTypeToDraw | SampleType.Mouse))
        && this.mouseFixationsView == null)
      {
        return false;
      }

      return true;
    }

    /// <summary>
    /// This method calls <see cref="DrawSamples(SampleType)"/>
    /// for mouse and gaze samples.
    /// </summary>
    protected void DrawFixationsForCurrentSubject()
    {
      this.spotlightRegion.Region.MakeEmpty();

      if ((this.sampleTypeToDraw == (this.sampleTypeToDraw | SampleType.Gaze))
        && this.gazeFixations != null)
      {
        this.DrawSamples(SampleType.Gaze);
      }

      if ((this.sampleTypeToDraw == (this.sampleTypeToDraw | SampleType.Mouse))
        && this.mouseFixations != null)
      {
        this.DrawSamples(SampleType.Mouse);
      }
    }

    #endregion //PUBLICMETHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Inherited methods                                                         //
    ///////////////////////////////////////////////////////////////////////////////
    #region OVERRIDES

    /// <summary>
    /// Overridden <see cref="Picture.CalculateTransformMatrix"/>. 
    /// Creates transparent bitmap for drawing and the corresponding graphics
    /// with correct transformation matrix.
    /// Updates heat map size with the presentation size.
    /// </summary>
    protected override void CalculateTransformMatrix()
    {
      base.CalculateTransformMatrix();
      if (this.heatMap.Width != this.PresentationSize.Width ||
         this.heatMap.Height != this.PresentationSize.Height)
      {
        this.CreateHeatMapBitmap(this.PresentationSize);
      }
    }

    /// <summary>
    /// Overridden. Frees resources of objects that are not disposed
    /// by the designer, mainly private objects.
    /// Is called during the call to <see cref="Control.Dispose(Boolean)"/>.
    /// </summary>
    protected override void CustomDispose()
    {
      if (this.spotlightRegion != null)
      {
        this.spotlightRegion.Dispose();
      }

      this.gazeFixationsPen.Dispose();
      this.gazeFixationConnectionsPen.Dispose();
      this.gazeFixationsFont.Dispose();
      this.mouseFixationsPen.Dispose();
      this.mouseFixationConnectionsPen.Dispose();
      this.mouseFixationsFont.Dispose();

      if (this.gazeFixations != null)
      {
        this.gazeFixations.Dispose();
      }

      if (this.mouseFixations != null)
      {
        this.mouseFixations.Dispose();
      }

      if (this.gazeFixations != null)
      {
        this.gazeFixationsView.Dispose();
      }

      if (this.mouseFixationsView != null)
      {
        this.mouseFixationsView.Dispose();
      }

      if (this.aoiTable != null)
      {
        this.aoiTable.Dispose();
      }

      if (this.colorMap != null)
      {
        this.colorMap.Dispose();
      }

      if (this.heatMap != null)
      {
        this.heatMap.Dispose();
      }

      base.CustomDispose();
    }

    #endregion //OVERRIDES

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler                                                              //
    ///////////////////////////////////////////////////////////////////////////////
    #region EVENTS

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler for Custom Defined Events                                    //
    ///////////////////////////////////////////////////////////////////////////////
    #region CUSTOMEVENTHANDLER
    #endregion //CUSTOMEVENTHANDLER

    #endregion //EVENTS

    ///////////////////////////////////////////////////////////////////////////////
    // Methods for doing main class job                                          //
    ///////////////////////////////////////////////////////////////////////////////
    #region METHODS

    /// <summary>
    /// This method creates the attention map with the current 
    /// visualization settings from the given distribution array.
    /// </summary>
    /// <param name="sampleType">The <see cref="SampleType"/>
    /// the attention map is created for.</param>
    protected void ApplyAttentionMap(SampleType sampleType)
    {
      int eyeMonX = this.PresentationSize.Width;
      int eyeMonY = this.PresentationSize.Height;

      // Finish attention map drawing if applicable
      if ((sampleType == (sampleType | SampleType.Gaze) &&
       this.gazeDrawingMode == FixationDrawingMode.AttentionMap) ||
      (sampleType == (sampleType | SampleType.Mouse) &&
      this.mouseDrawingMode == FixationDrawingMode.AttentionMap))
      {
        AttentionMaps.RescaleArray(this.distributionArray, -1);

        Bitmap heatMapBitmap = AttentionMaps.CreateHeatMap(
          this.heatMap,
          this.colorMap,
          new Size(eyeMonX, eyeMonY),
          this.distributionArray);

        VGImage newImage = new VGImage(heatMapBitmap, ImageLayout.Center, new Size(eyeMonX, eyeMonY));
        newImage.Name = "HeatMap";
        heatMapBitmap.Dispose();

        this.Elements.Remove("HeatMap");
        this.Elements.Add(newImage);
        this.Elements.ToHead(newImage);
      }

      if (this.gazeDrawingMode == FixationDrawingMode.Spotlight ||
this.mouseDrawingMode == FixationDrawingMode.Spotlight)
      {
        this.Elements.Add(this.spotlightRegion);
        this.Elements.ToHead(this.spotlightRegion);
      }
    }

    /// <summary>
    /// Initializes standard graphic elements.
    /// </summary>
    private void InitializeElements()
    {
      if (Properties.Settings.Default != null)
      {
        Ogama.Properties.Settings set = Properties.Settings.Default;
        this.gazeFixationsPen = new Pen(set.GazeFixationsPenColor, set.GazeFixationsPenWidth);
        this.gazeFixationsPen.DashStyle = set.GazeFixationsPenStyle;
        this.gazeFixationConnectionsPen = new Pen(set.GazeFixationConnectionsPenColor, set.GazeFixationConnectionsPenWidth);
        this.gazeFixationConnectionsPen.DashStyle = set.GazeFixationConnectionsPenStyle;
        this.gazeFixationsFont = (Font)set.GazeFixationsFont.Clone();
        this.gazeFixationsFontColor = set.GazeFixationsFontColor;
        this.gazeConnections = set.GazeConnections;
        this.gazeNumbers = set.GazeNumbers;

        this.mouseFixationsPen = new Pen(set.MouseFixationsPenColor, set.MouseFixationsPenWidth);
        this.mouseFixationsPen.DashStyle = set.MouseFixationsPenStyle;
        this.mouseFixationConnectionsPen = new Pen(set.MouseFixationsPenColor, set.MouseFixationsPenWidth);
        this.mouseFixationConnectionsPen.DashStyle = set.MouseFixationsPenStyle;
        this.mouseFixationsFont = (Font)set.MouseFixationsFont.Clone();
        this.mouseFixationsFontColor = set.MouseFixationsFontColor;
        this.mouseConnections = set.MouseConnections;
        this.mouseNumbers = set.MouseNumbers;
        this.spotlightRegion = new VGRegion(ShapeDrawAction.Fill, this.GrayBrush);
        this.spotlightRegion.Inverted = true;

        this.gazeDrawingMode = (FixationDrawingMode)Enum.Parse(typeof(FixationDrawingMode), set.GazeFixationsDrawingMode);
        this.mouseDrawingMode = (FixationDrawingMode)Enum.Parse(typeof(FixationDrawingMode), set.MouseFixationsDrawingMode);
      }

      Bitmap colorMapBitmap = new Bitmap(AttentionMaps.NUMCOLORS, 1, PixelFormat.Format32bppArgb);

      // Cache the gradient by painting it onto a bitmap
      using (Graphics bitmapGraphics = Graphics.FromImage(colorMapBitmap))
      {
        Rectangle bmpRct = new Rectangle(0, 0, colorMapBitmap.Width, colorMapBitmap.Height);
        AttentionMaps.Rainbow.PaintGradientWithDirectionOverride(
                                     bitmapGraphics,
                                     bmpRct,
                                     LinearGradientMode.Horizontal);
      }

      this.colorMap = new PaletteBitmap(colorMapBitmap);

      this.CreateHeatMapBitmap(new Size(100, 100));
    }

    /// <summary>
    ///  Creates a newly sized heatmap template to be filled 
    ///  with the data
    /// </summary>
    /// <param name="stimulusSize">A <see cref="Size"/> containing the new stimulus size.</param>
    private void CreateHeatMapBitmap(Size stimulusSize)
    {
      if (this.heatMap != null)
      {
        this.heatMap.Dispose();
        this.heatMap = null;
      }

      if (this.heatMap == null ||
        this.heatMap.Width != stimulusSize.Width ||
        this.heatMap.Height != stimulusSize.Height)
      {
        Bitmap heatMapBitmap = new Bitmap(stimulusSize.Width, stimulusSize.Height, PixelFormat.Format32bppArgb);
        this.heatMap = new PaletteBitmap(heatMapBitmap);
        heatMapBitmap.Dispose();
      }
    }

    /// <summary>
    /// Draw fixation circles over original image with line connections in between.
    /// </summary>
    /// <param name="sampleType">The <see cref="SampleType"/> to draw. Can be gaze or mouse.</param>
    private void DrawSamples(SampleType sampleType)
    {
      try
      {
        DataView usedFixationTable = null;
        VGStyleGroup usedGroup = VGStyleGroup.None;
        Pen usedPen = null;
        Font usedFont = null;
        Color usedFontColor = Color.Empty;
        VGLine usedConnectionLine = new VGLine(ShapeDrawAction.Edge, Pens.Red);
        FixationDrawingMode usedFixationDrawingMode = FixationDrawingMode.None;

        // Initialize variables
        bool drawLine = false;
        bool drawNumber = false;
        int eyeMonX = this.PresentationSize.Width;
        int eyeMonY = this.PresentationSize.Height;

        // Set used variables
        switch (sampleType)
        {
          case SampleType.Gaze:
            usedFixationTable = this.gazeFixationsView;
            usedGroup = VGStyleGroup.FIX_GAZE_ELEMENT;
            usedPen = this.gazeFixationsPen;
            usedFont = this.gazeFixationsFont;
            usedFontColor = this.gazeFixationsFontColor;
            usedConnectionLine.Pen = this.gazeFixationConnectionsPen;
            usedFixationDrawingMode = this.gazeDrawingMode;
            if (this.gazeConnections)
            {
              drawLine = true;
            }

            if (this.gazeNumbers)
            {
              drawNumber = true;
            }

            break;
          case SampleType.Mouse:
            usedFixationTable = this.mouseFixationsView;
            usedGroup = VGStyleGroup.FIX_MOUSE_ELEMENT;
            usedPen = this.mouseFixationsPen;
            usedFont = this.mouseFixationsFont;
            usedFontColor = this.mouseFixationsFontColor;
            usedConnectionLine.Pen = this.mouseFixationConnectionsPen;
            usedFixationDrawingMode = this.mouseDrawingMode;
            if (this.mouseConnections)
            {
              drawLine = true;
            }

            if (this.mouseNumbers)
            {
              drawNumber = true;
            }

            break;
        }

        // TextureBrush bkgBrush = null;
        // if (usedFixationDrawingMode == FixationDrawingMode.Circles)
        // {
        //  Bitmap bkg = new Bitmap(eyeMonX, eyeMonY);
        //  Bitmap transparentBkg = new Bitmap(eyeMonX, eyeMonY);
        //  using (Graphics graphics = Graphics.FromImage(bkg))
        //  {
        //    ColorMatrix colorMatrix = new ColorMatrix();
        //    colorMatrix.Matrix33 = 0.7f;
        //    ImageAttributes attributes = new ImageAttributes();
        //    attributes.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
        //    if (this.BGSlide != null)
        //    {
        //      Slide.DrawSlideAsync(this.BGSlide, graphics);
        //    }
        //    using (Graphics transGraphics = Graphics.FromImage(transparentBkg))
        //    {
        //      transGraphics.DrawImage(bkg, new Rectangle(0, 0, bkg.Width, bkg.Height), 0, 0, bkg.Width, bkg.Height, GraphicsUnit.Pixel, attributes);
        //      bkgBrush = new TextureBrush(transparentBkg);
        //    }
        //  }
        //  bkg.Dispose();
        //  transparentBkg.Dispose();
        // }

        // Loop Fixations and draw each one
        for (int i = 0; i < usedFixationTable.Count; i++)
        {
          DataRow row = usedFixationTable[i].Row;

          // Skip samples that are out of timing section bounds
          long fixationStartTime = row.IsNull("StartTime") ? 0 : Convert.ToInt64(row["StartTime"]);

          if (fixationStartTime < this.SectionStartTime)
          {
            continue;
          }

          if (fixationStartTime > this.SectionEndTime)
          {
            break;
          }

          float x = !row.IsNull("PosX") ? Convert.ToSingle(row["PosX"]) : 0;
          float y = !row.IsNull("PosY") ? Convert.ToSingle(row["PosY"]) : 0;
          PointF newFixationLocation = new PointF(x, y);
          float factor = (int)row["Length"];

          if (drawLine && this.foregoingFixationLocation != PointF.Empty)
          {
            // Add Point to connectionline
            usedConnectionLine.FirstPoint = this.foregoingFixationLocation;
            usedConnectionLine.SecondPoint = newFixationLocation;
            this.Elements.Add((VGElement)usedConnectionLine.Clone());
          }

          this.foregoingFixationLocation = newFixationLocation;

          // Create fixation description if applicable
          string name = string.Empty;
          if (drawNumber)
          {
            name = row["CountInTrial"].ToString();
          }

          switch (usedFixationDrawingMode)
          {
            case FixationDrawingMode.None:
              if (drawNumber)
              {
                // Create Ellipse with DOTSIZEd bounding rect
                int placeHolderSize = (int)(usedFont.Size * DOTSIZE);
                RectangleF numberBoundingRect = new RectangleF(
                  x - placeHolderSize / 2,
                  y - placeHolderSize / 2,
                  placeHolderSize,
                  placeHolderSize);

                // Draw Ellipse
                VGEllipse newNameEllipse = new VGEllipse(
                  ShapeDrawAction.Name,
                  (Pen)usedPen.Clone(),
                  new SolidBrush(usedPen.Color),
                  (Font)usedFont.Clone(),
                  usedFontColor,
                  numberBoundingRect,
                  usedGroup,
                  name,
                  string.Empty);

                this.Elements.Add(newNameEllipse);
              }

              break;
            case FixationDrawingMode.Dots:
              // Create Ellipse with DOTSIZEd bounding rect
              RectangleF dotBoundingRect = new RectangleF(
                x - DOTSIZE / 2,
                y - DOTSIZE / 2,
                DOTSIZE,
                DOTSIZE);

              Font ont = (Font)usedFont.Clone();

              // Draw Ellipse
              VGEllipse newEllipse = new VGEllipse(
                ShapeDrawAction.NameAndFill,
                (Pen)usedPen.Clone(),
                new SolidBrush(usedPen.Color),
                (Font)usedFont.Clone(),
                usedFontColor,
                dotBoundingRect,
                usedGroup,
                name,
                string.Empty);

              this.Elements.Add(newEllipse);
              break;
            case FixationDrawingMode.Circles:
              RectangleF fixBoundingRect = this.GetBoundingRectFromRow(sampleType, row);
              newEllipse = new VGEllipse(
                ShapeDrawAction.NameAndEdge,
                (Pen)usedPen.Clone(),
                null,                // (TextureBrush)bkgBrush.Clone(),
                (Font)usedFont.Clone(),
                usedFontColor,
                fixBoundingRect,
                usedGroup,
                name,
                string.Empty);

              this.Elements.Add(newEllipse);
              break;
            case FixationDrawingMode.Spotlight:
              if (drawNumber)
              {
                dotBoundingRect = new RectangleF(x - DOTSIZE / 2, y - DOTSIZE / 2, DOTSIZE, DOTSIZE);
                newEllipse = new VGEllipse(
                  ShapeDrawAction.Name,
                  (Pen)usedPen.Clone(),
                  new SolidBrush(usedPen.Color),
                  (Font)usedFont.Clone(),
                  usedFontColor,
                  dotBoundingRect,
                  usedGroup,
                  name,
                  string.Empty);

                this.Elements.Add(newEllipse);
              }

              fixBoundingRect = this.GetBoundingRectFromRow(sampleType, row);
              this.spotlightRegion.AddEllipse(fixBoundingRect);
              break;
            case FixationDrawingMode.AttentionMap:
              if (drawNumber)
              {
                dotBoundingRect = new RectangleF(x - DOTSIZE / 2, y - DOTSIZE / 2, DOTSIZE, DOTSIZE);
                newEllipse = new VGEllipse(
                  ShapeDrawAction.Name,
                  (Pen)usedPen.Clone(),
                  new SolidBrush(usedPen.Color),
                  (Font)usedFont.Clone(),
                  usedFontColor,
                  dotBoundingRect,
                  usedGroup,
                  name,
                  string.Empty);

                this.Elements.Add(newEllipse);
              }

              float[,] kernelMultiplied = AttentionMaps.MultiplyKernel(factor, AttentionMaps.KernelSize);
              AttentionMaps.AddKernelToArray(
                this.distributionArray,
                (int)x,
                (int)y,
                eyeMonX,
                eyeMonY,
                AttentionMaps.KernelSize,
                kernelMultiplied);

              break;
          }
        }

        // if (bkgBrush != null)
        // {
        //  bkgBrush.Dispose();
        // }
      }
      catch (Exception ex)
      {
        ExceptionMethods.HandleException(ex);
      }
    }

    #endregion //METHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER

    /// <summary>
    /// Reads row to calculate fixations bounds.
    /// </summary>
    /// <param name="sampleType">The <see cref="SampleType"/> to draw. Can be gaze or mouse.</param>
    /// <param name="row">A <see cref="DataRow"/> with the fixations table row.</param>
    /// <returns>A <see cref="RectangleF"/>with the bounding rectangle for the 
    /// given fixation row.</returns>
    private RectangleF GetBoundingRectFromRow(SampleType sampleType, DataRow row)
    {
      float x = !row.IsNull("PosX") ? Convert.ToSingle(row["PosX"]) : 0;
      float y = !row.IsNull("PosY") ? Convert.ToSingle(row["PosY"]) : 0;

      // Calculate Bounding Rectangle
      float divisor = 1f;
      if (sampleType == (sampleType | SampleType.Gaze))
      {
        divisor = this.gazeFixationsDiameterDivisor;
      }
      else if (sampleType == (sampleType | SampleType.Mouse))
      {
        divisor = this.mouseFixationsDiameterDivisor;
      }

      float fixationDiameter = Convert.ToSingle(row["Length"]) / divisor;
      RectangleF fixBoundingRect = new RectangleF();
      fixBoundingRect.X = x - fixationDiameter;
      fixBoundingRect.Y = y - fixationDiameter;
      fixBoundingRect.Width = fixationDiameter * 2;
      fixBoundingRect.Height = fixationDiameter * 2;
      return fixBoundingRect;
    }

    #endregion //HELPER
  }
}
