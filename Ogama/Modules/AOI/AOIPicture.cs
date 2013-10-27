// <copyright file="AOIPicture.cs" company="FU Berlin">
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

namespace Ogama.Modules.AOI
{
  using System;
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.Data;
  using System.Drawing;
  using System.Drawing.Drawing2D;
  using System.Windows.Forms;
  using Ogama.ExceptionHandling;
  using Ogama.Modules.AOI.Dialogs;
  using Ogama.Modules.Common.Types;

  using VectorGraphics.Canvas;
  using VectorGraphics.Elements;
  using VectorGraphics.Elements.ElementCollections;
  using VectorGraphics.Tools.CustomEventArgs;
  using VectorGraphics.Tools.CustomTypeConverter;

  /// <summary>
  /// Derived from <see cref="PictureModifiable"/>. 
  /// Used to display vector graphic elements in the Areas of interest module.
  /// Implements the database connection for the shapes.
  /// </summary>
  public partial class AOIPicture : PictureModifiable
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
    /// The factor to multiply fixations durations to get the diameter.
    /// </summary>
    private float bubbleFactor;

    /// <summary>
    /// The factor to multiply transition arrows to get the diameter.
    /// </summary>
    private float arrowFactor;

    /// <summary>
    /// This DataView holds the subset of gaze fixations for
    /// a specific trial and slidenr.
    /// </summary>
    private DataView gazeFixationsView;

    /// <summary>
    /// This DataView holds the subset of mouse fixations for
    /// a specific trial and slidenr.
    /// </summary>
    private DataView mouseFixationsView;

    /// <summary>
    /// This member saves the current collection of aoi shapes.
    /// </summary>
    private VGElementCollection aoiCollection;

    /// <summary>
    /// The Dictionary of aoi names with their <see cref="AOIStatistic"/>.
    /// </summary>
    private Dictionary<string, AOIStatistic> aoiStatistics;

    /// <summary>
    /// Pen for bubble shapes.
    /// </summary>
    private Pen bubblePen;

    /// <summary>
    /// Brush for bubble shapes.
    /// </summary>
    private Brush bubbleBrush;

    /// <summary>
    /// Font for bubble shapes.
    /// </summary>
    private Font bubbleFont;

    /// <summary>
    /// Color for bubble shapes font.
    /// </summary>
    private Color bubbleFontColor;

    /// <summary>
    /// The VGAlignment for bubble shapes.
    /// </summary>
    private VGAlignment bubbleTextAlignment;

    /// <summary>
    /// The <see cref="ShapeDrawAction"/> for bubble shapes.
    /// </summary>
    private ShapeDrawAction bubbleDrawAction;

    /// <summary>
    /// Pen for arrow shapes.
    /// </summary>
    private Pen arrowPen;

    /// <summary>
    /// Brush for arrow shapes.
    /// </summary>
    private Brush arrowBrush;

    /// <summary>
    /// Font for arrow shapes.
    /// </summary>
    private Font arrowFont;

    /// <summary>
    /// Color for arrow shapes font.
    /// </summary>
    private Color arrowFontColor;

    /// <summary>
    /// The VGAlignment for arrow shapes.
    /// </summary>
    private VGAlignment arrowTextAlignment;

    /// <summary>
    /// The <see cref="ShapeDrawAction"/> for arrow shapes.
    /// </summary>
    private ShapeDrawAction arrowDrawAction;

    /// <summary>
    /// Indicates hidden AOI description.
    /// </summary>
    private bool hideAOIDescription;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the AOIPicture class.
    /// </summary>
    public AOIPicture()
      : base()
    {
      this.InitializeComponent();
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Sets the factor to multiply fixations durations to get the diameter.
    /// </summary>
    public float BubbleFactor
    {
      set { this.bubbleFactor = value * 0.001f; }
    }

    /// <summary>
    /// Sets the factor to multiply transition arrows to get the diameter.
    /// </summary>
    public float ArrowFactor
    {
      set { this.arrowFactor = value; }
    }

    /// <summary>
    /// Sets a value indicating whether the AOI description should be hidden.
    /// </summary>
    public bool HideAOIDescription
    {
      set { this.hideAOIDescription = value; }
    }

    /// <summary>
    /// Gets or sets the gaze fixation data view for the selected trial.
    /// </summary>
    /// <value>A <see cref="DataView"/> with the gaze fixations for the trial.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public DataView GazeFixationsView
    {
      get { return this.gazeFixationsView; }
      set { this.gazeFixationsView = value; }
    }

    /// <summary>
    /// Gets or sets the mouse fixation data view for the selected trial.
    /// </summary>
    /// <value>A <see cref="DataView"/> with the mouse fixations for the trial.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public DataView MouseFixationsView
    {
      get { return this.mouseFixationsView; }
      set { this.mouseFixationsView = value; }
    }

    /// <summary>
    /// Gets or sets the aoi collection for the selected trial.
    /// </summary>
    /// <value>A <see cref="VGElementCollection"/> with the AOIS for the selected trial.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public VGElementCollection AoiCollection
    {
      get { return this.aoiCollection; }
      set { this.aoiCollection = value; }
    }

    /// <summary>
    /// Gets or sets pen for bubble shapes.
    /// </summary>
    /// <value>A <see cref="Pen"/> for bubble shapes.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Pen BubblePen
    {
      get { return this.bubblePen; }
      set { this.bubblePen = value; }
    }

    /// <summary>
    /// Gets or sets pen for arrow shapes.
    /// </summary>
    /// <value>A <see cref="Pen"/> for arrow shapes.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Pen ArrowPen
    {
      get { return this.arrowPen; }
      set { this.arrowPen = value; }
    }

    /// <summary>
    /// Gets or sets the font for bubble shapes.
    /// </summary>
    /// <value>A <see cref="Font"/> used for bubble shapes text.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Font BubbleFont
    {
      get { return this.bubbleFont; }
      set { this.bubbleFont = value; }
    }

    /// <summary>
    /// Gets or sets the font for arrow shapes.
    /// </summary>
    /// <value>A <see cref="Font"/> used for arrow shapes text.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Font ArrowFont
    {
      get { return this.arrowFont; }
      set { this.arrowFont = value; }
    }

    /// <summary>
    /// Gets or sets color of bubble shapes.
    /// </summary>
    /// <value>A <see cref="Color"/> used for bubble shapes text.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Color BubbleFontColor
    {
      get { return this.bubbleFontColor; }
      set { this.bubbleFontColor = value; }
    }

    /// <summary>
    /// Gets or sets color of arrow shapes.
    /// </summary>
    /// <value>A <see cref="Color"/> used for arrow shapes text.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Color ArrowFontColor
    {
      get { return this.arrowFontColor; }
      set { this.arrowFontColor = value; }
    }

    /// <summary>
    /// Gets or sets brush for bubble shapes.
    /// </summary>
    /// <value>A <see cref="Brush"/> for bubble shapes.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Brush BubbleBrush
    {
      get { return this.bubbleBrush; }
      set { this.bubbleBrush = value; }
    }

    /// <summary>
    /// Gets or sets brush for arrow shapes.
    /// </summary>
    /// <value>A <see cref="Brush"/> for arrow shapes.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Brush ArrowBrush
    {
      get { return this.arrowBrush; }
      set { this.arrowBrush = value; }
    }

    /// <summary>
    /// Gets or sets the <see cref="VGAlignment"/> for bubble shapes.
    /// </summary>
    /// <value>A <see cref="VGAlignment"/> used for bubble shapes text.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public VGAlignment BubbleTextAlignment
    {
      get { return this.bubbleTextAlignment; }
      set { this.bubbleTextAlignment = value; }
    }

    /// <summary>
    /// Gets or sets the <see cref="VGAlignment"/> for arrow shapes.
    /// </summary>
    /// <value>A <see cref="VGAlignment"/> used for arrow shapes text.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public VGAlignment ArrowTextAlignment
    {
      get { return this.arrowTextAlignment; }
      set { this.arrowTextAlignment = value; }
    }

    /// <summary>
    /// Gets or sets the <see cref="ShapeDrawAction"/> for bubble shapes.
    /// </summary>
    /// <value>A <see cref="ShapeDrawAction"/> used for bubble shapes.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public ShapeDrawAction BubbleDrawAction
    {
      get { return this.bubbleDrawAction; }
      set { this.bubbleDrawAction = value; }
    }

    /// <summary>
    /// Gets or sets the <see cref="ShapeDrawAction"/> for arrow shapes.
    /// </summary>
    /// <value>A <see cref="ShapeDrawAction"/> used for arrow shapes.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public ShapeDrawAction ArrowDrawAction
    {
      get { return this.arrowDrawAction; }
      set { this.arrowDrawAction = value; }
    }

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
        Properties.Settings.Default.AOITargetColor = this.TargetPen.Color;
        Properties.Settings.Default.AOITargetWidth = this.TargetPen.Width;
        Properties.Settings.Default.AOITargetStyle = this.TargetPen.DashStyle;

        Properties.Settings.Default.AOIStandardColor = this.DefaultPen.Color;
        Properties.Settings.Default.AOIStandardWidth = this.DefaultPen.Width;
        Properties.Settings.Default.AOIStandardStyle = this.DefaultPen.DashStyle;

        Properties.Settings.Default.AOISearchRectColor = this.SearchRectPen.Color;
        Properties.Settings.Default.AOISearchRectWidth = this.SearchRectPen.Width;
        Properties.Settings.Default.AOISearchRectStyle = this.SearchRectPen.DashStyle;

        Properties.Settings.Default.AOIBubbleColor = this.BubblePen.Color;
        Properties.Settings.Default.AOIBubbleWidth = this.BubblePen.Width;
        Properties.Settings.Default.AOIBubbleStyle = this.BubblePen.DashStyle;

        Properties.Settings.Default.AOIArrowColor = this.ArrowPen.Color;
        Properties.Settings.Default.AOIArrowWidth = this.ArrowPen.Width;
        Properties.Settings.Default.AOIArrowStyle = this.ArrowPen.DashStyle;

        Properties.Settings.Default.AOITargetFont = (Font)this.TargetFont.Clone();
        Properties.Settings.Default.AOITargetFontColor = this.TargetFontColor;

        Properties.Settings.Default.AOIStandardFont = (Font)this.DefaultFonts.Clone();
        Properties.Settings.Default.AOIStandardFontColor = this.DefaultFontColor;

        Properties.Settings.Default.AOISearchRectFont = (Font)this.SearchRectFont.Clone();
        Properties.Settings.Default.AOISearchRectFontColor = this.SearchRectFontColor;

        Properties.Settings.Default.AOIBubbleFont = (Font)this.bubbleFont.Clone();
        Properties.Settings.Default.AOIBubbleFontColor = this.bubbleFontColor;

        Properties.Settings.Default.AOIArrowFont = (Font)this.arrowFont.Clone();
        Properties.Settings.Default.AOIArrowFontColor = this.arrowFontColor;

        Properties.Settings.Default.AOITargetTextAlignment = this.TargetTextAlignment.ToString();
        Properties.Settings.Default.AOISearchRectTextAlignment = this.SearchRectTextAlignment.ToString();
        Properties.Settings.Default.AOIDefaultTextAlignment = this.DefaultTextAlignment.ToString();
        Properties.Settings.Default.AOIBubbleTextAlignment = this.bubbleTextAlignment.ToString();
        Properties.Settings.Default.AOIArrowTextAlignment = this.arrowTextAlignment.ToString();

        Properties.Settings.Default.AOIBubbleDrawAction = this.bubbleDrawAction.ToString();
        Properties.Settings.Default.AOIArrowDrawAction = this.arrowDrawAction.ToString();

        Properties.Settings.Default.Save();
      }
    }

    /// <summary>
    /// This method creates a rectangular grid of AOI named from
    /// A-Z or Aa to Zz depending on grid size.
    /// The given <see cref="DataGridView"/> supplies the names
    /// and the number of rows and columns.
    /// </summary>
    /// <param name="dataGridView">A <see cref="DataGridView"/>
    /// with the named rows and columns for the new rectangular AOI grid.</param>
    public void CreateAOIGrid(DataGridView dataGridView)
    {
      try
      {
        // Calculate grid sizes
        int numRows = dataGridView.Rows.Count;
        int numColumns = dataGridView.Columns.Count;
        float cellHeight = (float)Document.ActiveDocument.PresentationSize.Height / numRows;
        float cellWidth = (float)Document.ActiveDocument.PresentationSize.Width / numColumns;

        // Iterate through data grid view and creat for each cell a rectangular AOI
        for (int i = 0; i < numRows; i++)
        {
          for (int j = 0; j < numColumns; j++)
          {
            // Calculate grid cell bounds.
            RectangleF boundingRect = new RectangleF();
            boundingRect.Y = i * cellHeight;
            boundingRect.X = j * cellWidth;
            boundingRect.Width = cellWidth;
            boundingRect.Height = cellHeight;

            // Create Rect with default stroke
            VGRectangle newRect = new VGRectangle(
              this.hideAOIDescription ? ShapeDrawAction.Edge : ShapeDrawAction.NameAndEdge,
              this.DefaultPen,
              this.DefaultFonts,
              this.DefaultFontColor,
              boundingRect,
              VGStyleGroup.AOI_NORMAL,
              dataGridView.Rows[i].Cells[j].Value.ToString(),
              string.Empty);
            newRect.TextAlignment = this.DefaultTextAlignment;
            this.aoiCollection.Add(newRect);

            // Raise event
            base.OnShapeAdded(new ShapeEventArgs(newRect));
          }
        }

        this.DrawForeground(true);
      }
      catch (Exception ex)
      {
        ExceptionMethods.HandleException(ex);
      }
    }

    /// <summary>
    /// Loads the shapes that are listed in the given database table
    /// and creates corresponding graphic elements.
    /// </summary>
    /// <param name="areaOfInterestTableRows">Areas of interest table as 
    /// a <see cref="DataGridViewRowCollection"/></param>
    public void LoadShapesFromDataGridView(DataGridViewRowCollection areaOfInterestTableRows)
    {
      try
      {
        // Create aoi elements from data view
        this.aoiCollection = new VGElementCollection();

        foreach (DataGridViewRow row in areaOfInterestTableRows)
        {
          if (!row.IsNewRow)
          {
            // retrieve shape parameters from cell values.
            string shapeName = row.Cells["colShapeName"].Value.ToString();
            string strPtList = row.Cells["colShapePts"].Value.ToString();
            Pen usedPen = this.DefaultPen;
            Font usedFont = this.DefaultFonts;
            Color usedFontColor = this.DefaultFontColor;
            VGAlignment usedAlignment = this.DefaultTextAlignment;
            VGStyleGroup usedStyleGroup = VGStyleGroup.AOI_NORMAL;
            List<PointF> pointList = ObjectStringConverter.StringToPointFList(strPtList);
            string usedElementGroup = row.Cells["colShapeGroup"].Value.ToString();
            switch (usedElementGroup)
            {
              case "Target":
                usedPen = this.TargetPen;
                usedFont = this.TargetFont;
                usedFontColor = this.TargetFontColor;
                usedStyleGroup = VGStyleGroup.AOI_TARGET;
                usedAlignment = this.TargetTextAlignment;
                break;
              case "SearchRect":
                usedPen = this.SearchRectPen;
                usedFont = this.SearchRectFont;
                usedFontColor = this.SearchRectFontColor;
                usedStyleGroup = VGStyleGroup.AOI_SEARCHRECT;
                usedAlignment = this.SearchRectTextAlignment;
                break;
              default:
                usedPen = this.DefaultPen;
                usedFont = this.DefaultFonts;
                usedFontColor = this.DefaultFontColor;
                usedStyleGroup = VGStyleGroup.AOI_NORMAL;
                usedAlignment = this.DefaultTextAlignment;
                break;
            }

            // Create the shape depending on ShapeType
            RectangleF boundingRect = new RectangleF();
            switch (row.Cells["colShapeType"].Value.ToString())
            {
              case "Rectangle":
                boundingRect.Location = pointList[0];
                boundingRect.Width = pointList[2].X - pointList[0].X;
                boundingRect.Height = pointList[2].Y - pointList[0].Y;

                // Create Rect with defined stroke
                VGRectangle newRect = new VGRectangle(
                  this.hideAOIDescription ? ShapeDrawAction.Edge : ShapeDrawAction.NameAndEdge,
                  usedPen,
                  usedFont,
                  usedFontColor,
                  boundingRect,
                  usedStyleGroup,
                  shapeName,
                  usedElementGroup);
                newRect.TextAlignment = usedAlignment;
                this.aoiCollection.Add(newRect);
                break;
              case "Ellipse":
                boundingRect.Location = pointList[0];
                boundingRect.Width = pointList[2].X - pointList[0].X;
                boundingRect.Height = pointList[2].Y - pointList[0].Y;

                // Create Rect with defined stroke
                VGEllipse newEllipse = new VGEllipse(
                  this.hideAOIDescription ? ShapeDrawAction.Edge : ShapeDrawAction.NameAndEdge,
                  usedPen,
                  usedFont,
                  usedFontColor,
                  boundingRect,
                  usedStyleGroup,
                  shapeName,
                  usedElementGroup);
                newEllipse.TextAlignment = usedAlignment;
                this.aoiCollection.Add(newEllipse);
                break;
              case "Polyline":
                // Create Polyline with defined stroke
                VGPolyline newPolyline = new VGPolyline(
                  this.hideAOIDescription ? ShapeDrawAction.Edge : ShapeDrawAction.NameAndEdge,
                  usedPen,
                  usedFont,
                  usedFontColor,
                  usedStyleGroup,
                  shapeName,
                  usedElementGroup);
                newPolyline.TextAlignment = usedAlignment;
                foreach (PointF point in pointList)
                {
                  newPolyline.AddPt(point);
                }

                newPolyline.ClosePolyline();
                this.aoiCollection.Add(newPolyline);
                boundingRect = newPolyline.Bounds;
                break;
            }
          }
        }

        // Reset Elements (deselect and clear all)
        ResetPicture();

        this.Elements.AddRange(this.aoiCollection);

        // If there were a selected element before updating, try
        // to select it again.
        if (this.SelectedElement != null)
        {
          foreach (VGElement element in this.Elements)
          {
            if (VGPolyline.Distance(element.Location, this.SelectedElement.Location) < 1)
            {
              this.SelectedElement = element;
              element.IsInEditMode = true;
            }
          }
        }

        this.DrawForeground(true);
      }
      catch (Exception ex)
      {
        ExceptionMethods.HandleException(ex);
      }
    }

    /// <summary>
    /// This method selects a shape by its name property.
    /// </summary>
    /// <param name="nameOfShapeToSelect">A <see cref="string"/> with the name
    /// of the shape to select.</param>
    public void SelectShape(string nameOfShapeToSelect)
    {
      ResetSelectedElement();

      foreach (VGElement element in this.Elements)
      {
        if (element.Name == nameOfShapeToSelect)
        {
          element.IsInEditMode = true;
          this.SelectedElement = element;
          break;
        }
      }

      this.DrawForeground(false);
    }

    /// <summary>
    /// This method draws the statistic bubbles and transisiton arrows
    /// of given <see cref="SampleType"/> in given <see cref="VisualizationModes"/>.
    /// </summary>
    /// <param name="mode">The <see cref="VisualizationModes"/> to be used.</param>
    /// <param name="sampleType">The <see cref="SampleType"/> of the data.</param>
    public void DrawAOIStatistics(VisualizationModes mode, SampleType sampleType)
    {
      // Skip if no data available
      if (this.aoiStatistics == null)
      {
        return;
      }

      // Get all statistical elements
      var statisticalElements =
        this.Elements.FindAllGroupMembers(VGStyleGroup.AOI_STATISTICS_ARROW);
      statisticalElements.AddRange(this.Elements.FindAllGroupMembers(VGStyleGroup.AOI_STATISTICS_BUBBLE));

      this.Elements.RemoveAll(statisticalElements);

      var distances = new List<int>();

      var aoisWithoutSearchRects = new VGElementCollection();
      foreach (VGElement aoi in this.aoiCollection)
      {
        if (aoi.StyleGroup != VGStyleGroup.AOI_SEARCHRECT)
        {
          aoisWithoutSearchRects.Add(aoi);
        }
      }

      foreach (VGElement aoi in aoisWithoutSearchRects)
      {
        if (!this.aoiStatistics.ContainsKey(aoi.Name))
        {
          continue;
        }

        AOIStatistic aoiStatistic = this.aoiStatistics[aoi.Name];
        if ((mode & VisualizationModes.FixationTime) == VisualizationModes.FixationTime)
        {
          if (aoiStatistic.SumOfTimeOfAllFixations > 0)
          {
            var bounds = this.GetCircleBounds(aoi.Center, aoiStatistic.SumOfTimeOfAllFixations, mode);
            var ellipse = new VGEllipse(
                this.bubbleDrawAction,
                this.bubblePen,
                this.bubbleBrush,
                this.bubbleFont,
                this.bubbleFontColor,
                bounds,
                VGStyleGroup.AOI_STATISTICS_BUBBLE,
                aoiStatistic.SumOfTimeOfAllFixations.ToString() + " ms",
                string.Empty);

            ellipse.TextAlignment = this.bubbleTextAlignment;
            distances.Add((int)(bounds.Width / 2));
            this.Elements.Add(ellipse);
          }
        }

        if ((mode & VisualizationModes.NumberOfFixations) == VisualizationModes.NumberOfFixations)
        {
          if (aoiStatistic.FixationCount > 0)
          {
            var bounds = this.GetCircleBounds(aoi.Center, aoiStatistic.FixationCount, mode);
            var ellipse = new VGEllipse(
                this.bubbleDrawAction,
                this.bubblePen,
                this.bubbleBrush,
                this.bubbleFont,
                this.bubbleFontColor,
                bounds,
                VGStyleGroup.AOI_STATISTICS_BUBBLE,
                aoiStatistic.FixationCount.ToString(),
                string.Empty);

            ellipse.TextAlignment = this.bubbleTextAlignment;
            distances.Add((int)(bounds.Width / 2));
            this.Elements.Add(ellipse);
          }
        }

        if ((mode & VisualizationModes.AverageFixationDuration) == VisualizationModes.AverageFixationDuration)
        {
          if (aoiStatistic.FixationDurationMean > 0)
          {
            var bounds = this.GetCircleBounds(aoi.Center, aoiStatistic.FixationDurationMean, mode);
            var ellipse = new VGEllipse(
                this.bubbleDrawAction,
                this.bubblePen,
                this.bubbleBrush,
                this.bubbleFont,
                this.bubbleFontColor,
                bounds,
                VGStyleGroup.AOI_STATISTICS_BUBBLE,
                aoiStatistic.FixationDurationMean.ToString("N0") + " ms",
                string.Empty);
            ellipse.TextAlignment = this.bubbleTextAlignment;
            distances.Add((int)(bounds.Width / 2));
            this.Elements.Add(ellipse);
          }
        }
      }

      // Check for absolute or relative transition values
      bool absolute = (mode & VisualizationModes.AbsoluteTransitions) == VisualizationModes.AbsoluteTransitions;

      if ((mode & VisualizationModes.RelativeTransitions) == VisualizationModes.RelativeTransitions
        || (mode & VisualizationModes.AbsoluteTransitions) == VisualizationModes.AbsoluteTransitions)
      {
        DataView trialsFixations = null;

        // Get filtered fixations data view
        switch (sampleType)
        {
          case SampleType.Gaze:
            trialsFixations = this.gazeFixationsView;
            break;
          case SampleType.Mouse:
            trialsFixations = this.mouseFixationsView;
            break;
        }

        var transitions = Statistics.Statistic.CreateTransitionMatrixForSingleAOIs(
          trialsFixations,
          aoisWithoutSearchRects);

        float transitionSum = 0;

        // Calculate transition total sum only if relative values are requested.
        if (!absolute)
        {
          for (int i = 0; i <= transitions.GetUpperBound(0); i++)
          {
            // Skip nowhere transitions
            if (i == 0)
            {
              continue;
            }

            for (int j = 0; j <= transitions.GetUpperBound(1); j++)
            {
              // Only take values outside the diagonal
              if (i == j || j == 0)
              {
                continue;
              }

              transitionSum += (int)transitions.GetValue(i, j);
            }
          }
        }

        // Write transitionMatrix to dgv
        for (int i = transitions.GetLowerBound(0); i <= transitions.GetUpperBound(0); i++)
        {
          // Skip nowhere transitions
          if (i == 0)
          {
            continue;
          }

          for (int j = transitions.GetLowerBound(1); j <= transitions.GetUpperBound(1); j++)
          {
            // Only take values above the diagonal
            if (i >= j)
            {
              continue;
            }

            float transAtoB = 0;
            float transBtoA = 0;
            if (absolute)
            {
              transAtoB = (int)transitions.GetValue(i, j);
              transBtoA = (int)transitions.GetValue(j, i);
            }
            else
            {
              transAtoB = (float)Math.Round((int)transitions.GetValue(i, j) / transitionSum * 100, 1);
              transBtoA = (float)Math.Round((int)transitions.GetValue(j, i) / transitionSum * 100, 1);
            }

            if (transAtoB > 0 || transBtoA > 0)
            {
              bool showNumbers = false;
              if ((this.arrowDrawAction & ShapeDrawAction.Name) == ShapeDrawAction.Name)
              {
                this.arrowDrawAction |= ~ShapeDrawAction.Name;
                showNumbers = true;
              }

              VGArrow arrow = new VGArrow(this.arrowDrawAction, this.arrowPen);
              arrow.Brush = this.arrowBrush;
              if (!showNumbers)
              {
                arrow.HideWeights = true;
              }

              PointF location1 = aoisWithoutSearchRects[i - 1].Center;
              PointF location2 = aoisWithoutSearchRects[j - 1].Center;
              int distanceFirst = distances.Count >= i ? distances[i - 1] : 15;
              int distanceSecond = distances.Count >= j ? distances[j - 1] : 15;
              if (location1.X <= location2.X)
              {
                arrow.FirstPoint = location1;
                arrow.SecondPoint = location2;
                arrow.FirstPointDistance = distanceFirst;
                arrow.SecondPointDistance = distanceSecond;
                arrow.FirstPointWeight = transBtoA;
                arrow.SecondPointWeight = transAtoB;
              }
              else
              {
                arrow.FirstPoint = location2;
                arrow.SecondPoint = location1;
                arrow.FirstPointDistance = distanceSecond;
                arrow.SecondPointDistance = distanceFirst;
                arrow.FirstPointWeight = transAtoB;
                arrow.SecondPointWeight = transBtoA;
              }

              arrow.FormatString = "N0";
              if (!absolute)
              {
                if (arrow.FirstPointWeight < 1 && arrow.SecondPointWeight < 1)
                {
                  arrow.FormatString = "N1";
                }

                arrow.AddOnString = "%";
              }

              arrow.WeightFont = this.arrowFont;
              arrow.WeightFontColor = this.arrowFontColor;
              arrow.ScaleFactor = this.arrowFactor;
              arrow.StyleGroup = VGStyleGroup.AOI_STATISTICS_ARROW;
              arrow.TextAlignment = this.arrowTextAlignment;
              this.Elements.Add(arrow);
            }
          }
        }
      }

      this.DrawForeground(true);
    }

    /// <summary>
    /// Calculates the statistical parameters for the current trial and <see cref="SampleType"/>
    /// </summary>
    /// <param name="sampleType">The <see cref="SampleType"/> of which the data should be used.</param>
    public void CalculateAOIStatistics(SampleType sampleType)
    {
      this.aoiStatistics = new Dictionary<string, AOIStatistic>();
      foreach (VGElement aoi in this.aoiCollection)
      {
        AOIStatistic aoiStatistic = new AOIStatistic();
        VGElementCollection aoiContainer = new VGElementCollection();
        aoiContainer.Add(aoi);

        switch (sampleType)
        {
          case SampleType.Gaze:
            aoiStatistic = Statistics.Statistic.CalcAOIStatistic(this.gazeFixationsView, aoiContainer);
            break;
          case SampleType.Mouse:
            aoiStatistic = Statistics.Statistic.CalcAOIStatistic(this.mouseFixationsView, aoiContainer);
            break;
        }

        if (!this.aoiStatistics.ContainsKey(aoi.Name))
        {
          this.aoiStatistics.Add(aoi.Name, aoiStatistic);
        }
      }
    }

    /// <summary>
    /// This method updates the style of the elements from the given <see cref="VGStyleGroup"/>
    /// with the new value and updates the pictures styles.
    /// </summary>
    /// <param name="vGStyleGroup">The <see cref="VGStyleGroup"/> whichs style should
    /// be changed</param>
    /// <param name="e">The <see cref="ShapePropertiesChangedEventArgs"/> with the new style.</param>
    public void ShapePropertiesChanged(VGStyleGroup vGStyleGroup, ShapePropertiesChangedEventArgs e)
    {
      VGElementCollection sublist = this.Elements.FindAllGroupMembers(vGStyleGroup);

      bool showNumbers = false;
      ShapeDrawAction drawAction = e.ShapeDrawAction;

      if (vGStyleGroup == VGStyleGroup.AOI_STATISTICS_ARROW)
      {
        if ((e.ShapeDrawAction & ShapeDrawAction.Name) == ShapeDrawAction.Name)
        {
          drawAction &= ~ShapeDrawAction.Name;
          showNumbers = true;
        }
      }

      foreach (VGElement element in sublist)
      {
        element.Pen = e.Pen;
        element.Brush = e.Brush;
        element.TextAlignment = e.TextAlignment;
        element.ShapeDrawAction = drawAction;
        if (element is VGArrow)
        {
          ((VGArrow)element).WeightFont = e.NewFont;
          ((VGArrow)element).WeightFontColor = e.NewFontColor;
          if (showNumbers)
          {
            ((VGArrow)element).HideWeights = false;
          }
          else
          {
            ((VGArrow)element).HideWeights = true;
          }
        }
        else
        {
          element.Font = e.NewFont;
          element.FontColor = e.NewFontColor;
        }
      }

      this.DrawForeground(true);

      switch (vGStyleGroup)
      {
        case VGStyleGroup.AOI_STATISTICS_ARROW:
          this.arrowPen = e.Pen;
          this.arrowBrush = e.Brush;
          this.arrowFont = e.NewFont;
          this.arrowFontColor = e.NewFontColor;
          this.arrowTextAlignment = e.TextAlignment;
          this.arrowDrawAction = e.ShapeDrawAction;
          break;
        case VGStyleGroup.AOI_STATISTICS_BUBBLE:
          this.bubblePen = e.Pen;
          this.bubbleBrush = e.Brush;
          this.bubbleFont = e.NewFont;
          this.bubbleFontColor = e.NewFontColor;
          this.bubbleTextAlignment = e.TextAlignment;
          this.bubbleDrawAction = e.ShapeDrawAction;
          break;
      }
    }

    #endregion //PUBLICMETHODS

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
    /// Handles the Paste event by pasting an <see cref="VGElement"/> from the
    /// clipboard into the picture.
    /// </summary>
    public override void OnPaste()
    {
      // Retrieves the data from the clipboard.
      object test = Clipboard.GetData(DataFormats.StringFormat);
      if (test == null)
      {
        return;
      }

      VGElement t = VGElement.Deserialize(test.ToString());

      // Determines whether the data is in a format you can use.
      if (t != null)
      {
        VGElement elementToAdd = t;
        this.ResetSelectedElement();

        if (!this.Elements.Contains(elementToAdd))
        {
          // New Graphic element created, so notify listeners.
          this.OnShapeAdded(new ShapeEventArgs(elementToAdd));
        }
      }
    }

    /// <summary>
    /// Overridden <see cref="PictureModifiable.ShapeAdded"/> event handler. 
    /// Adds a shape name user dialog to the shape creation process.
    /// </summary>
    /// <param name="e">The <see cref="ShapeEventArgs"/> with the new shape.</param>
    protected override void OnShapeAdded(ShapeEventArgs e)
    {
      // Show NameDialog if name is empty
      if (e.Shape.Name == string.Empty)
      {
        NameShapeDlg newDlg = new NameShapeDlg();
        if (newDlg.ShowDialog() == DialogResult.OK)
        {
          // Set new shapes name
          e.Shape.Name = newDlg.ShapeName;

          // Raise event
          base.OnShapeAdded(e);

          // Save to aoi collection
          this.aoiCollection.Add(e.Shape);
        }
        else
        {
          Elements.Remove(e.Shape);
          this.DrawForeground(false);
        }
      }
      else
      {
        // Raise event
        base.OnShapeAdded(e);

        // Save to aoi collection
        this.aoiCollection.Add(e.Shape);
      }
    }

    #endregion //CUSTOMEVENTHANDLER

    #endregion //EVENTS

    ///////////////////////////////////////////////////////////////////////////////
    // Inherited methods                                                         //
    ///////////////////////////////////////////////////////////////////////////////
    #region OVERRIDES

    /// <summary>
    /// Overridden. Frees resources of objects that are not disposed
    /// by the designer, mainly private objects.
    /// Is called during the call to <see cref="Control.Dispose(Boolean)"/>.
    /// </summary>
    protected override void CustomDispose()
    {
      if (this.gazeFixationsView != null)
      {
        this.gazeFixationsView.Dispose();
      }

      if (this.mouseFixationsView != null)
      {
        this.mouseFixationsView.Dispose();
      }

      foreach (VGElement element in this.aoiCollection)
      {
        element.Dispose();
      }

      this.aoiCollection.Clear();

      this.bubblePen.Dispose();
      this.bubbleBrush.Dispose();
      this.bubbleFont.Dispose();
      this.arrowPen.Dispose();
      this.arrowBrush.Dispose();
      this.arrowFont.Dispose();
      base.CustomDispose();
    }

    /// <summary>
    /// Overridden. Initializes the drawing elements of this <see cref="AOIPicture"/>
    /// by calling the <c>Properties.Settings.Default</c> values with
    /// <see cref="PictureModifiable.InitializeElements(Pen,Pen,Pen,Pen,Font,Color,Font,Color,Font,Color, VGAlignment, VGAlignment, VGAlignment)"/>.
    /// </summary>
    protected override void InitializePictureDefaultElements()
    {
      // AppSettings are always set except when in design mode
      if (Properties.Settings.Default != null)
      {
        Pen targetPen = new Pen(Properties.Settings.Default.AOITargetColor, Properties.Settings.Default.AOITargetWidth);
        targetPen.DashStyle = Properties.Settings.Default.AOITargetStyle;
        targetPen.LineJoin = LineJoin.Miter;

        Pen dottedPen = new Pen(Properties.Settings.Default.AOIStandardColor, Properties.Settings.Default.AOIStandardWidth);
        dottedPen.DashStyle = DashStyle.Dash;
        dottedPen.LineJoin = LineJoin.Miter;

        Pen defaultPen = new Pen(Properties.Settings.Default.AOIStandardColor, Properties.Settings.Default.AOIStandardWidth);
        defaultPen.DashStyle = Properties.Settings.Default.AOIStandardStyle;
        defaultPen.LineJoin = LineJoin.Miter;

        Pen searchRectPen = new Pen(Properties.Settings.Default.AOISearchRectColor, Properties.Settings.Default.AOISearchRectWidth);
        searchRectPen.DashStyle = Properties.Settings.Default.AOISearchRectStyle;
        searchRectPen.LineJoin = LineJoin.Miter;

        this.bubblePen = new Pen(Properties.Settings.Default.AOIBubbleColor, Properties.Settings.Default.AOIBubbleWidth);
        this.bubblePen.DashStyle = Properties.Settings.Default.AOIBubbleStyle;
        this.bubblePen.LineJoin = LineJoin.Miter;

        this.arrowPen = new Pen(Properties.Settings.Default.AOIArrowColor, Properties.Settings.Default.AOIArrowWidth);
        this.arrowPen.DashStyle = Properties.Settings.Default.AOIArrowStyle;
        this.arrowPen.LineJoin = LineJoin.Miter;

        Font targetFont = (Font)Properties.Settings.Default.AOITargetFont.Clone();
        Color targetFontColor = Properties.Settings.Default.AOITargetFontColor;

        Font defaultFont = (Font)Properties.Settings.Default.AOIStandardFont.Clone();
        Color defaultFontColor = Properties.Settings.Default.AOIStandardFontColor;

        Font searchRectFont = (Font)Properties.Settings.Default.AOISearchRectFont.Clone();
        Color searchRectFontColor = Properties.Settings.Default.AOISearchRectFontColor;

        this.bubbleFont = (Font)Properties.Settings.Default.AOIBubbleFont.Clone();
        this.bubbleFontColor = Properties.Settings.Default.AOIBubbleFontColor;
        this.bubbleBrush = new SolidBrush(Color.FromArgb(128, Properties.Settings.Default.AOIBubbleBrushColor));

        this.arrowFont = (Font)Properties.Settings.Default.AOIArrowFont.Clone();
        this.arrowFontColor = Properties.Settings.Default.AOIArrowFontColor;
        this.arrowBrush = new SolidBrush(Color.FromArgb(128, Properties.Settings.Default.AOIArrowBrushColor));

        VGAlignment defaultTextAlignment = (VGAlignment)Enum.Parse(typeof(VGAlignment), Properties.Settings.Default.AOIDefaultTextAlignment);
        VGAlignment targetTextAlignment = (VGAlignment)Enum.Parse(typeof(VGAlignment), Properties.Settings.Default.AOITargetTextAlignment);
        VGAlignment searchRectTextAlignment = (VGAlignment)Enum.Parse(typeof(VGAlignment), Properties.Settings.Default.AOISearchRectTextAlignment);
        this.bubbleTextAlignment = (VGAlignment)Enum.Parse(typeof(VGAlignment), Properties.Settings.Default.AOIBubbleTextAlignment);
        this.arrowTextAlignment = (VGAlignment)Enum.Parse(typeof(VGAlignment), Properties.Settings.Default.AOIArrowTextAlignment);

        this.bubbleDrawAction = (ShapeDrawAction)Enum.Parse(typeof(ShapeDrawAction), Properties.Settings.Default.AOIBubbleDrawAction);
        this.arrowDrawAction = (ShapeDrawAction)Enum.Parse(typeof(ShapeDrawAction), Properties.Settings.Default.AOIArrowDrawAction);

        this.InitializeElements(
          targetPen,
          dottedPen,
          defaultPen,
          searchRectPen,
          targetFont,
          targetFontColor,
          defaultFont,
          defaultFontColor,
          searchRectFont,
          searchRectFontColor,
          targetTextAlignment,
          defaultTextAlignment,
          searchRectTextAlignment);
      }
    }

    #endregion //Overrides

    ///////////////////////////////////////////////////////////////////////////////
    // Methods for doing main class job                                          //
    ///////////////////////////////////////////////////////////////////////////////
    #region METHODS
    #endregion //METHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER

    /// <summary>
    /// This method calculates the bounding rectangle of a circle
    /// with the given value and <see cref="VisualizationModes"/>
    /// at the given center.
    /// </summary>
    /// <param name="pointF">A <see cref="PointF"/> with the circles center.</param>
    /// <param name="sum">The <see cref="Double"/> with the circles value.</param>
    /// <param name="mode">The <see cref="VisualizationModes"/> to use.</param>
    /// <returns>A <see cref="RectangleF"/> with the circles bounding rect.</returns>
    private RectangleF GetCircleBounds(PointF pointF, double sum, VisualizationModes mode)
    {
      float offset = (float)(sum * this.bubbleFactor);

      if ((mode & VisualizationModes.NumberOfFixations) == VisualizationModes.NumberOfFixations)
      {
        offset *= 500;
      }

      if ((mode & VisualizationModes.AverageFixationDuration) == VisualizationModes.AverageFixationDuration)
      {
        offset *= 50;
      }

      RectangleF bounds = new RectangleF(pointF.X - offset, pointF.Y - offset, offset * 2, offset * 2);
      return bounds;
    }

    #endregion //HELPER
  }
}
