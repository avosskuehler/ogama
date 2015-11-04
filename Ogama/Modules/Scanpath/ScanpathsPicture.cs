// <copyright file="ScanpathsPicture.cs" company="FU Berlin">
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

namespace Ogama.Modules.Scanpath
{
  using System;
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.Data;
  using System.Drawing;

  using Ogama.Modules.Common.PictureTemplates;
  using Ogama.Modules.Common.Types;

  using VectorGraphics.Elements;
  using VectorGraphics.Elements.ElementCollections;
  using VectorGraphics.Tools.CustomEventArgs;

  /// <summary>
  /// Derived from <see cref="PictureWithFixations"/>. 
  /// Used to display vector graphic elements for the Scanpaths module.
  /// </summary>
  public partial class ScanpathsPicture : PictureWithFixations
  {
    ///////////////////////////////////////////////////////////////////////////////
    // Defining Constants                                                        //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTANTS

    /// <summary>
    /// The array of unique character identifiers for max 26 grid elements.
    /// </summary>
    private static string[] identifierList = new string[] 
    { 
      "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" 
    };

    /// <summary>
    /// The array of unique character identifiers for more than 676 grid elements.
    /// </summary>
    private static string[] identifierListLong;

    /// <summary>
    /// The array of unique character identifiers for 27-676 grid elements.
    /// </summary>
    private static string[] identifierListExtraLong;

    /// <summary>
    /// The currently used list of identifier strings for the grid or AOI elements.
    /// </summary>
    private static string[] currentIdentifierList;

    #endregion //CONSTANTS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Variables, Enumerations, Events                                  //
    ///////////////////////////////////////////////////////////////////////////////
    #region FIELDS

    /// <summary>
    /// The millisecond of the time the last animation timer tick was successfully
    /// processed
    /// </summary>
    private int lastPlayTime;

    /// <summary>
    /// The millisecond up to which the fixations should be drawn.
    /// </summary>
    private int playTime;

    /// <summary>
    /// Saves the starttime of the animation in ticks.
    /// </summary>
    private long animationStartTime;

    /// <summary>
    /// Saves the subjects for which the scanpath should be shown.
    /// </summary>
    private SortedDictionary<string, ScanpathProperties> subjects;

    /// <summary>
    /// Saves the number of grid dividers if the scanpath GridBase is set to 
    /// <see cref="GridBase.Rectangular"/>
    /// </summary>
    private int gridFactor;

    /// <summary>
    /// Saves the type of picture sections, that are used for
    /// defining the comparsion strings.
    /// </summary>
    private GridBase gridBase;

    /// <summary>
    /// Saves a levensthein string as a step by step connection
    /// of subsequent picture segments.
    /// </summary>
    private string defaultSegmentString;

    /// <summary>
    /// True, if the levensthein string should ignore
    /// subsequent fixations on the same AOI.
    /// </summary>
    private bool ignoreSubsequentHits;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes static members of the ScanpathsPicture class.
    /// </summary>
    static ScanpathsPicture()
    {
      InitializeCharacterLists();
    }

    /// <summary>
    /// Initializes a new instance of the ScanpathsPicture class.
    /// </summary>
    public ScanpathsPicture()
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
    /// Gets an array of unique character identifiers for up to 26 grid elements.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public static string[] IdentifierList
    {
      get { return identifierList; }
    }

    /// <summary>
    /// Gets an array of unique character identifiers for more than 26 up to 676 grid elements.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public static string[] IdentifierListLong
    {
      get { return identifierListLong; }
    }

    /// <summary>
    /// Gets an array of unique character identifiers for more than 676 grid elements.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public static string[] IdentifierListExtraLong
    {
      get { return identifierListExtraLong; }
    }

    /// <summary>
    /// Gets an array of unique character identifiers for the grid or AOI elements.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public static string[] CurrentIdentifierList
    {
      get { return currentIdentifierList; }
    }

    /// <summary>
    /// Sets the start of the animation timer in ticks.
    /// That is the value retrieved via <code>DateTime.Now.Ticks</code>.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public long AnimationStartTime
    {
      set { this.animationStartTime = value; }
    }

    /// <summary>
    /// Gets or sets the type of picture sections, that are used for
    /// defining the comparsion strings.
    /// </summary>
    [Category("Appearance")]
    [Description("The picture division setting")]
    public GridBase GridBasis
    {
      get
      {
        return this.gridBase;
      }

      set
      {
        this.gridBase = value;
        this.SetCorrectIdentifierList();
        this.RecalculateDefaultString();
      }
    }

    /// <summary>
    /// Gets or sets the number of grid dividers if the scanpath GridBase is set to 
    /// <see cref="GridBase.Rectangular"/>
    /// </summary>
    [Category("Appearance")]
    [Description("The number of grid segments in one direction.")]
    public int GridFactor
    {
      get
      {
        return this.gridFactor;
      }

      set
      {
        this.gridFactor = value;
        this.SetCorrectIdentifierList();
        this.RecalculateDefaultString();
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the levensthein string should ignore
    /// subsequent fixations on the same AOI.
    /// </summary>
    [Category("Appearance")]
    [Description("True, if the levensthein string should ignore subsequent fixations at the same AOI.")]
    public bool IgnoreSubsequentFixations
    {
      get { return this.ignoreSubsequentHits; }
      set { this.ignoreSubsequentHits = value; }
    }

    /// <summary>
    /// Sets the subjects for which the scanpath should be shown.
    /// </summary>
    /// <value>A <see cref="Dictionary{String,ScanpathProperties}"/> with the subject properties.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public SortedDictionary<string, ScanpathProperties> Subjects
    {
      set { this.subjects = value; }
    }

    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Public methods                                                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region PUBLICMETHODS

    /// <summary>
    /// This method clears the time span that should be drawn, instead everything
    /// from this trial is drawn or resets the time span to the time line duration,
    /// depending on the parameter
    /// </summary>
    /// <param name="makeEmpty"><strong>True</strong> if time line should be erased,
    /// <strong>false</strong> if timeline should be reset to duration.</param>
    public void ResetTimeSpan(bool makeEmpty)
    {
      if (makeEmpty)
      {
        this.playTime = this.SectionStartTime;
        this.lastPlayTime = this.SectionStartTime;
      }
      else
      {
        this.playTime = this.SectionEndTime;
        this.lastPlayTime = this.SectionStartTime;
      }
    }

    /// <summary>
    /// This method calculates a string representation of the fixational scanpath
    /// of the given subject under the current settings.
    /// </summary>
    /// <param name="name">A <see cref="string"/> with the subject name.</param>
    /// <returns>The calculated scanpath string representation.</returns>
    public string CalcPathString(string name)
    {
      string returnScanpathString = string.Empty;
      if (!this.CheckForValidFixations())
      {
        return returnScanpathString;
      }

      this.SetSubjectFilter(name, null, null);

      switch (this.gridBase)
      {
        case GridBase.None:
          // Do nothing returns empty string and zero distance
          break;
        case GridBase.Rectangular:
          // Get grid segments
          VGElementCollection segments =
            this.Elements.FindAllGroupMembers(VGStyleGroup.SCA_GRID_RECTANGLE);

          // Iterate fixations and add name of grid segment to levensthein string,
          // if fixation center is in bounds of current segment.
          for (int i = 0; i < this.GazeFixationsView.Count; i++)
          {
            DataRow row = this.GazeFixationsView[i].Row;

            // Skip samples that are out of timing section bounds
            long fixationStartTime =
              row.IsNull("StartTime") ? 0 : Convert.ToInt64(row["StartTime"]);

            if (fixationStartTime < this.SectionStartTime)
            {
              continue;
            }

            if (fixationStartTime > this.SectionEndTime)
            {
              break;
            }

            foreach (VGRectangle rect in segments)
            {
              PointF fixationCenter = new PointF(Convert.ToSingle(row["PosX"]), Convert.ToSingle(row["PosY"]));
              if (rect.Contains(fixationCenter))
              {
                if (this.ignoreSubsequentHits && returnScanpathString.EndsWith(rect.Name))
                {
                  continue;
                }

                returnScanpathString += rect.Name;
                break;
              }
            }
          }

          break;
        case GridBase.AOIs:

          if (this.AOITable != null)
          {
            VGElementCollection aoiList = this.GetAOIElements(this.AOITable);

            // Iterate fixations and add name of grid segment to levensthein string,
            // if fixation center is in bounds of current segment.
            for (int i = 0; i < this.GazeFixationsView.Count; i++)
            {
              DataRow row = this.GazeFixationsView[i].Row;

              // Skip samples that are out of timing section bounds
              long fixationStartTime = row.IsNull("StartTime") ? 0 :
                Convert.ToInt64(row["StartTime"]);
              if (fixationStartTime < this.SectionStartTime)
              {
                continue;
              }

              if (fixationStartTime > this.SectionEndTime)
              {
                break;
              }

              bool outOfAOI = true;

              foreach (VGElement aoi in aoiList)
              {
                PointF fixationCenter = new PointF(Convert.ToSingle(row["PosX"]), Convert.ToSingle(row["PosY"]));
                if (aoi.Contains(fixationCenter))
                {
                  outOfAOI = false;
                  if (this.ignoreSubsequentHits && returnScanpathString.EndsWith(aoi.Name))
                  {
                    continue;
                  }

                  returnScanpathString += aoi.Name;
                  break;
                }
              }

              if (outOfAOI)
              {
                if (!this.ignoreSubsequentHits || !returnScanpathString.EndsWith("#"))
                {
                  returnScanpathString += "#";
                }
              }
            }
          }

          break;
      }

      return returnScanpathString;
    }

    /// <summary>
    /// Draws the fixations at the current caret time position.
    /// </summary>
    /// <param name="caretTime">The time in milliseconds of the caret slider position</param>
    /// <param name="timespan">The time in milliseconds that defines the timespan to be drawn.</param>
    public void DrawTimeSection(int caretTime, int timespan)
    {
      this.ResetPicture();
      this.lastPlayTime = caretTime - timespan;
      this.playTime = caretTime;
      this.DrawFixations(true);
    }

    #endregion //PUBLICMETHODS

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
    // Inherited methods                                                         //
    ///////////////////////////////////////////////////////////////////////////////
    #region OVERRIDES

    /// <summary>
    /// Master Drawing routine, switches <see cref="FixationDrawingMode"/> for
    /// gaze and mouse samples.
    /// </summary>
    /// <param name="resetPicture"><strong>True</strong> if picture contents should be
    /// cleared before redrawing, otherwise <strong>false</strong>.</param>
    public override void DrawFixations(bool resetPicture)
    {
      if (!this.CheckForValidFixations())
      {
        return;
      }

      if (resetPicture)
      {
        // Clears picture foreground contents
        this.ResetPicture();
      }

      if (this.subjects != null && this.subjects.Count > 0)
      {
        foreach (KeyValuePair<string, ScanpathProperties> kvp in this.subjects)
        {
          // this.SetSubjectFilter(kvp.Key, this.playTime - 500 > 0 ? this.playTime - 1000 : 0, this.playTime);
          this.SetSubjectFilter(kvp.Key, this.lastPlayTime, this.playTime);

          this.GazeFixationConnectionsPen = kvp.Value.GazeStyle.ConnectionPen;
          this.GazeFixationsFont = kvp.Value.GazeStyle.Font;
          this.GazeFixationsFontColor = kvp.Value.GazeStyle.FontColor;
          this.GazeFixationsPen = kvp.Value.GazeStyle.FixationPen;
          this.MouseFixationConnectionsPen = kvp.Value.MouseStyle.ConnectionPen;
          this.MouseFixationsFont = kvp.Value.MouseStyle.Font;
          this.MouseFixationsFontColor = kvp.Value.MouseStyle.FontColor;
          this.MouseFixationsPen = kvp.Value.MouseStyle.FixationPen;
          this.DrawFixationsForCurrentSubject();
        }

        this.ApplyAttentionMap(SampleType.Both);
      }

      this.DrawForeground(resetPicture);
    }

    /// <summary>
    /// Erases element list and sets new background.
    /// </summary>
    public override void ResetPicture()
    {
      base.ResetPicture();
      this.RemoveGrid();
      this.DrawGrid();
      this.ResetForegoingFixationLocation();
      this.Invalidate();
    }

    /// <summary>
    /// Overridden. Calls picture update method. Invoked from Picture Animation timer tick method.
    /// </summary>
    /// <param name="sender">sending frame, normally base picture class timer</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    /// <remarks>Starts updating the readed samples for the timespan that
    /// is over since the last update.</remarks>
    protected override void ForegroundTimerTick(object sender, EventArgs e)
    {
      TimeSpan timeLeft = new TimeSpan(DateTime.Now.Ticks - this.animationStartTime);
      this.playTime = (int)timeLeft.TotalMilliseconds;
      this.DrawFixations(false);

      bool isFinished = false;

      if (this.playTime > this.SectionEndTime)
      {
        isFinished = true;
      }

      int percentComplete = (int)(this.playTime / (float)this.SectionEndTime * 100);
      ProgressEventArgs pea = new ProgressEventArgs(isFinished, percentComplete, (int)this.playTime);
      this.OnProgress(pea);
      this.lastPlayTime = this.playTime;
    }

    /// <summary>
    /// Overridden. Does the same as the base method,
    /// but renames all AOIs with an increasing number.
    /// </summary>
    /// <param name="aoiTable">The <see cref="DataTable"/> with the AOIs.</param>
    /// <returns>A <see cref="List{VGElement}"/> with the renamed shapes.</returns>
    protected override VGElementCollection GetAOIElements(DataTable aoiTable)
    {
      VGElementCollection aoiElements = base.GetAOIElements(aoiTable);

      for (int i = 0; i < aoiElements.Count; i++)
      {
        VGElement element = aoiElements[i];

        // Modify shape names to fit Levenshtein settings.
        string shapeName = currentIdentifierList[i];
        element.Name = shapeName;
        aoiElements[i] = element;
      }

      return aoiElements;
    }

    #endregion //OVERRIDES

    ///////////////////////////////////////////////////////////////////////////////
    // Methods for doing main class job                                          //
    ///////////////////////////////////////////////////////////////////////////////
    #region METHODS

    /// <summary>
    /// Initializes the static grid or AOI identifier lists in the form
    /// Aa,Ab,Ac ... and Aaa, Aab, Aac etc.
    /// </summary>
    private static void InitializeCharacterLists()
    {
      List<string> longList = new List<string>();
      List<string> veryLongList = new List<string>();
      for (int i = 0; i < 26; i++)
      {
        for (int j = 0; j < 26; j++)
        {
          longList.Add(identifierList[i] + identifierList[j].ToLower());
          for (int k = 0; k < 26; k++)
          {
            veryLongList.Add(identifierList[i] + identifierList[j].ToLower() + identifierList[k].ToLower());
          }
        }
      }

      identifierListLong = longList.ToArray();
      identifierListExtraLong = veryLongList.ToArray();
    }

    /// <summary>
    /// Initializes standard graphic elements.
    /// </summary>
    private void InitializeElements()
    {
      this.gridBase = GridBase.None;
      this.gridFactor = 5;
    }

    /// <summary>
    /// This method draws a named grid if applicable.
    /// </summary>
    private void DrawGrid()
    {
      // Draw Grid if applicable.
      switch (this.gridBase)
      {
        case GridBase.None:
          // Don´t draw grid
          break;
        case GridBase.Rectangular:
          this.DrawRectangularGrid();
          break;
        case GridBase.AOIs:
          // Draw AOIs if there are any.
          if (this.AOITable != null)
          {
            this.DrawAOI(this.AOITable);
          }

          break;
      }
    }

    /// <summary>
    /// This method removes the grid elements from the picture.
    /// </summary>
    private void RemoveGrid()
    {
      this.Elements.RemoveAll(this.Elements.FindAllGroupMembers(VGStyleGroup.SCA_GRID_RECTANGLE));
      this.Elements.RemoveAll(this.Elements.FindAllGroupMembers(VGStyleGroup.SCA_GRID_AOI));
    }

    /// <summary>
    /// This method draws a rectangular grid with gridFactor divided length.
    /// </summary>
    private void DrawRectangularGrid()
    {
      int widthData = Document.ActiveDocument.ExperimentSettings.WidthStimulusScreen;
      int heightData = Document.ActiveDocument.ExperimentSettings.HeightStimulusScreen;

      float xDistance = (widthData - 2) / (float)this.gridFactor;
      float yDistance = (heightData - 2) / (float)this.gridFactor;

      // For each row
      for (int i = 0; i < this.gridFactor; i++)
      {
        // For each column
        for (int j = 0; j < this.gridFactor; j++)
        {
          // Create a rectangle with a new string index
          RectangleF bounds = new RectangleF(j * xDistance, i * yDistance, xDistance, yDistance);
          VGRectangle rect = new VGRectangle(
            ShapeDrawAction.NameAndEdge,
            new Pen(Color.Gray, 1.0f),
            new Font(VGRectangle.DefaultFont.FontFamily, 14.0f),
            Color.Gray,
            bounds,
            VGStyleGroup.SCA_GRID_RECTANGLE,
            currentIdentifierList[i * this.gridFactor + j],
            string.Empty);

          this.Elements.Add(rect);
        }
      }
    }

    /// <summary>
    /// This method iterates the current grid items to calculate a string
    /// that is concatenated from characters following the grid items from top left to bottom right
    /// in rectangular mode and item numbering in aoi mode.
    /// </summary>
    private void RecalculateDefaultString()
    {
      this.defaultSegmentString = string.Empty;
      switch (this.gridBase)
      {
        case GridBase.Rectangular:
          for (int i = 0; i < this.gridFactor * this.gridFactor; i++)
          {
            this.defaultSegmentString += currentIdentifierList[i];
          }

          break;
        case GridBase.AOIs:
          // Skip if no data available
          if (this.AOITable == null)
          {
            return;
          }

          int aoiCount = this.AOITable.Rows.Count;
          for (int i = 0; i < aoiCount; i++)
          {
            this.defaultSegmentString += currentIdentifierList[i];
          }

          break;
      }
    }

    /// <summary>
    /// This method chooses the identifier list of correct length
    /// for the given number of AOI elements or grid items.
    /// </summary>
    private void SetCorrectIdentifierList()
    {
      currentIdentifierList = identifierList;

      switch (this.gridBase)
      {
        case GridBase.Rectangular:
          if (this.gridFactor > 5)
          {
            currentIdentifierList = identifierListLong;
          }

          if (this.gridFactor > 26)
          {
            currentIdentifierList = identifierListExtraLong;
          }

          break;
        case GridBase.AOIs:
          // Skip if no data available
          if (this.AOITable == null)
          {
            return;
          }

          int aoiCount = this.AOITable.Rows.Count;
          if (aoiCount > 26)
          {
            currentIdentifierList = identifierListLong;
          }

          if (aoiCount > 676)
          {
            currentIdentifierList = identifierListExtraLong;
          }

          break;
      }
    }

    #endregion //METHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER
    #endregion //HELPER
  }
}
