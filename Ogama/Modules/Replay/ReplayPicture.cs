// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReplayPicture.cs" company="Freie Universität Berlin">
//   OGAMA - open gaze and mouse analyzer 
//   Copyright (C) 2014 Dr. Adrian Voßkühler  
//   Licensed under GPL V3
// </copyright>
// <author>Adrian Voßkühler</author>
// <email>adrian@ogama.net</email>
// --------------------------------------------------------------------------------------------------------------------
namespace Ogama.Modules.Replay
{
  using System;
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.Data;
  using System.Drawing;
  using System.Drawing.Drawing2D;
  using System.Media;
  using System.Windows.Forms;

  using Ogama.DataSet;
  using Ogama.ExceptionHandling;
  using Ogama.MainWindow;
  using Ogama.Modules.Common.CustomEventArgs;
  using Ogama.Modules.Common.FormTemplates;
  using Ogama.Modules.Common.Tools;
  using Ogama.Modules.Common.TrialEvents;
  using Ogama.Modules.Common.Types;

  using VectorGraphics.Canvas;
  using VectorGraphics.Elements;
  using VectorGraphics.Elements.ElementCollections;
  using VectorGraphics.StopConditions;
  using VectorGraphics.Tools;
  using VectorGraphics.Tools.CustomEventArgs;

  /// <summary>
  /// Derived from <see cref="Picture"/>. 
  /// Used to display vector graphic elements of the replay Interface.
  /// </summary>
  public partial class ReplayPicture : Picture
  {
    ///////////////////////////////////////////////////////////////////////////////
    // Defining Constants                                                        //
    ///////////////////////////////////////////////////////////////////////////////

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Variables, Enumerations, Events                                  //
    ///////////////////////////////////////////////////////////////////////////////
    #region FIELDS

    // Timing /////////////////////////////////////////////////////////////////////

    /// <summary>
    /// Timespan, that is lost by pausing the replay.
    /// </summary>
    /// <remarks>Needed to available pausing while replay.</remarks>
    private TimeSpan pauseTime = TimeSpan.Zero;

    /// <summary>
    /// Time the user pressed pause button.
    /// </summary>
    /// <remarks>Needed to available pausing while replay.</remarks>
    private DateTime pauseTimeStart;

    /// <summary>
    /// Saves current state of the replay loop.
    /// </summary>
    private LoopState currentLoopState;

    // Fixation  Calculating Objects //////////////////////////////////////////

    /// <summary>
    /// Object for calculating gaze fixations.
    /// </summary>
    private FixationDetection objFixGazeDetection;

    /// <summary>
    /// Object for calculating mouse fixations.
    /// </summary>
    private FixationDetection objFixMouseDetection;

    // Settings from Application Settings///////////////////////////////////////

    /// <summary>
    /// Maximum number of points in polylines when <see cref="isGazeDiscreteLength"/> flag is set.
    /// </summary>
    private int maxLengthPath;

    /// <summary>
    /// Number of fixation circles displayed when one of the DiscreteLength flags is set.
    /// </summary>
    private int numFixToShow;

    /// <summary>
    /// Divisor to reduce diameter of gaze fixations according to time length.
    /// </summary>
    private float gazeFixDiameterDiv;

    /// <summary>
    /// Divisor to reduce diameter of mouse fixations according to time length.
    /// </summary>
    private float mouseFixDiameterDiv;

    /// <summary>
    /// Maximum distance of two samples to constitute a fixation. 
    /// Measured in pixels of eye monitor
    /// </summary>
    private int gazeMaxDistance;

    /// <summary>
    /// Maximum distance of two samples to constitute a fixation. 
    /// Measured in pixels of eye monitor
    /// </summary>
    private int mouseMaxDistance;

    /// <summary>
    /// Minimum number of samples that constitute a gaze fixation.
    /// </summary>
    private int gazeMinSamples;

    /// <summary>
    /// Minimum number of samples that constitute a mouse fixation.
    /// </summary>
    private int mouseMinSamples;

    // Settings from Form /////////////////////////////////////////////////////

    /// <summary>
    /// Flag that saves drawing mode enumeration for gaze drawing.
    /// </summary>
    /// <see cref="ReplayDrawingModes"/>
    private ReplayDrawingModes gazeDrawingMode;

    /// <summary>
    /// Flag that saves drawing mode enumeration for mouse drawing.
    /// </summary>
    /// <see cref="ReplayDrawingModes"/>
    private ReplayDrawingModes mouseDrawingMode;

    /// <summary>
    /// Factor that gives the current replay speed.
    /// </summary>
    /// <remarks>Is set in UI from dropdown.</remarks>
    private float speed;

    /// <summary>
    /// Flag. True if user switched the automatic length cutting for gaze replay on.
    /// </summary>
    /// <remarks>Automatic length cutting removes old points in a polyline when 
    /// exceeding the limit <see cref="maxLengthPath"/> points.
    /// On the other side it limits the number of displayed fixations to 
    /// <see cref="NumFixToShow"/>.</remarks>
    private bool isGazeDiscreteLength;

    /// <summary>
    /// Flag. True if user switched the automatic length cutting for mouse replay on.
    /// </summary>
    /// <remarks>Automatic length cutting removes old points in a polyline when 
    /// exceeding the limit <see cref="maxLengthPath"/> points.
    /// On the other side it limits the number of displayed fixations to 
    /// <see cref="NumFixToShow"/>.</remarks>
    private bool isMouseDiscreteLength;

    /// <summary>
    /// Flag. True if user switched automatic blink display on.
    /// </summary>
    /// <remarks>Automatic blink display switches an grayed background image over the
    /// scene if no data or out of monitor data is detected.</remarks>
    private bool showBlinks;

    /// <summary>
    /// This table should hold the whole trial data.
    /// </summary>
    private DataTable replayTable;

    // Graphic Objects /////////////////////////////////////////////////////////

    /// <summary>
    /// List of fixation circles from the gaze data, to be updated while playing.
    /// </summary>
    /// <remarks>This is somehow a new Layer on the picture surface. And
    /// can be accessed more easily for removing older fixations, when
    /// <see cref="isGazeDiscreteLength"/> flag is set.</remarks>
    private VGElementCollection gazeFixations;

    /// <summary>
    /// List of fixation circles from the mouse data, to be updated while playing.
    /// </summary>
    /// <remarks>This is somehow a new Layer on the picture surface. And
    /// can be accessed more easily for removing older fixations, when
    /// <see cref="isGazeDiscreteLength"/> flag is set.</remarks>
    private VGElementCollection mouseFixations;

    // Predefined vector graphic elements//////////////////////////////////////

    /// <summary>
    /// A rectangle which displays the visible part of the screen, when stimuli
    /// are greater then monitor resolution and are to be scrolled.
    /// Used for web pages mainly.
    /// </summary>
    private VGRectangle visiblePartOfScreen;

    /// <summary>
    /// Semi-transparent rectangle, that grays scene, when overlayed on scene.
    /// </summary>
    /// <remarks>Used for displaying blinks and out of monitors</remarks>
    private VGRectangle rectBlink;

    /// <summary>
    /// Circle with background image fill.
    /// </summary>
    /// <remarks>Used in Gaze Spotlight mode.<see cref="ReplayDrawingModes"/></remarks>
    private VGEllipse gazePicEllipse;

    /// <summary>
    /// Circle with background image fill.
    /// </summary>
    /// <remarks>Used in Mouse Spotlight mode.<see cref="ReplayDrawingModes"/></remarks>
    private VGEllipse mousePicEllipse;

    /// <summary>
    /// Circle with transparent fill.
    /// </summary>
    /// <remarks>Used in gaze Fixation mode.<see cref="ReplayDrawingModes"/></remarks>
    private VGEllipse gazeFixEllipse;

    /// <summary>
    /// Circle with transparent fill.
    /// </summary>
    /// <remarks>Used in mouse Fixation mode.<see cref="ReplayDrawingModes"/></remarks>
    private VGEllipse mouseFixEllipse;

    /// <summary>
    /// Polyline, that is constituted by the gaze samples.
    /// </summary>
    /// <remarks>Used in gaze Path mode.<see cref="ReplayDrawingModes"/></remarks>
    private VGPolyline gazeRawPolyline;

    /// <summary>
    /// Polyline, that is constituted by the mouse samples.
    /// </summary>
    /// <remarks>Used in mouse Path mode.<see cref="ReplayDrawingModes"/></remarks>
    private VGPolyline mouseRawPolyline;

    /// <summary>
    /// Polyline of connections between fixation circles.
    /// </summary>
    /// <remarks>Used in gaze Fixation Connection modes.<see cref="ReplayDrawingModes"/></remarks>
    private VGPolyline gazeFixConPolyline;

    /// <summary>
    /// Polyline of connections between fixation circles.
    /// </summary>
    /// <remarks>Used in mouse Fixation Connection modes.<see cref="ReplayDrawingModes"/></remarks>
    private VGPolyline mouseFixConPolyline;

    /// <summary>
    /// Line of connection between last fixation and currentyl estimated new fixation location.
    /// </summary>
    /// <remarks>Used in gaze Fixation Connection modes.<see cref="ReplayDrawingModes"/></remarks>
    private VGLine gazeFixConLine;

    /// <summary>
    /// Line of connection between last fixation and currentyl estimated new fixation location.
    /// </summary>
    /// <remarks>Used in mouse Fixation Connection modes.<see cref="ReplayDrawingModes"/></remarks>
    private VGLine mouseFixConLine;

    /// <summary>
    /// Shape of gaze cursor
    /// </summary>
    /// <remarks>Used in gaze cursor mode.<see cref="ReplayDrawingModes"/></remarks>
    private VGCursor gazeCursor;

    /// <summary>
    /// Shape of mouse cursor
    /// </summary>
    /// <remarks>Used in mouse cursor mode.<see cref="ReplayDrawingModes"/></remarks>
    private VGCursor mouseCursor;

    // Drawing Elements ////////////////////////////////////////////////////////

    /// <summary>
    /// Pen for gaze cursor
    /// </summary>
    private Pen penGazeCursor;

    /// <summary>
    /// Pen for gaze path
    /// </summary>
    private Pen penGazePath;

    /// <summary>
    /// Pen for gaze fixations
    /// </summary>
    private Pen penGazeFixation;

    /// <summary>
    /// Pen for gaze fixation connections
    /// </summary>
    private Pen penGazeFixationConnection;

    /// <summary>
    /// Pen for lines, that show connections in between gaze samples 
    /// had no data or where out of monitor.
    /// </summary>
    private Pen penGazeNoData;

    /// <summary>
    /// Pen for mouse cursor
    /// </summary>
    private Pen penMouseCursor;

    /// <summary>
    /// Pen for mouse path
    /// </summary>
    private Pen penMousePath;

    /// <summary>
    /// Pen for mouse fixations
    /// </summary>
    private Pen penMouseFixation;

    /// <summary>
    /// Pen for mouse fixation connections
    /// </summary>
    private Pen penMouseFixationConnection;

    /// <summary>
    /// The stream with the sound for the left mouse click.
    /// </summary>
    private SoundPlayer sndLeftClick;

    /// <summary>
    /// The stream with the sound for the right mouse click.
    /// </summary>
    private SoundPlayer sndRightClick;

    /// <summary>
    /// Set to true if the video export should be cancelled.
    /// </summary>
    private bool cancelVideoExport;

    /// <summary>
    /// Saves the time the fixation is first displayed.
    /// </summary>
    private long onsetTime;

    /// <summary>
    /// Contains the value of the currently displayed trial sequence
    /// to keep track of changes for sending trial sequence changed events.
    /// </summary>
    private int currentTrialSequence;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the ReplayPicture class.
    /// </summary>
    public ReplayPicture()
      : base()
    {
      this.InitializeComponent();
      this.InitializeFields();
      this.InitializeElements();
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Enumerations                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region ENUMS

    /// <summary>
    /// This delegate enables calling the <see cref="RenderFrame(ref int,long,Size, ref bool)"/>
    /// function from another thread
    /// </summary>
    /// <param name="startSample">Ref. An <see cref="int"/> with the sample number of the start sample.</param>
    /// <param name="endTime">End time in milliseconds</param>
    /// <param name="videoSize">size of video</param>
    /// <param name="reachedEnd">Ref. The picture runs out of data.</param>
    /// <returns>A <see cref="Bitmap"/> with the rendered frame.</returns>
    public delegate Bitmap RenderFrameHandler(ref int startSample, long endTime, Size videoSize, ref bool reachedEnd);

    /// <summary>
    /// Event. Raised when event id was found.
    /// </summary>
    public event TrialEventIDFoundEventHandler TrialEventIDFound;

    /// <summary>
    /// Event. Raised whenever a new trial sequence is found in the raw data.
    /// This occurs only during continous replay mode.
    /// </summary>
    public event TrialSequenceChangedEventHandler TrialSequenceChanged;

    /// <summary>
    /// Event. Raised when recorded mouse position has moved.
    /// </summary>
    public event MouseEventHandler MouseMoved;

    #endregion ENUMS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    // Timing /////////////////////////////////////////////////////////////////

    /// <summary>
    /// Gets or sets time when user pressed pause button
    /// </summary>
    /// <value>A <see cref="DateTime"/> with the time value the pause has startet.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public DateTime PauseTimeStart
    {
      get { return this.pauseTimeStart; }
      set { this.pauseTimeStart = value; }
    }

    /// <summary>
    /// Gets or sets the duration of the pause time.
    /// </summary>
    /// <value>A <see cref="TimeSpan"/> with the complete pausing time.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public TimeSpan PauseTime
    {
      get { return this.pauseTime; }
      set { this.pauseTime = value; }
    }

    /// <summary>
    /// Gets or sets the current replay position in milliseconds
    /// </summary>
    /// <value>An <see cref="int"/> with the replay position in milliseconds.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int ReplayPosition
    {
      get
      {
        return Convert.ToInt32(this.replayTable.Rows[this.currentLoopState.RowCounter]["Time"]);
      }

      set
      {
        var timeInSamples = this.GetSynchronizedSampleCount(value);
        this.currentLoopState.RowCounter = timeInSamples;
        this.currentLoopState.ProcessBeginningTime = DateTime.Now.AddMilliseconds(-value / this.speed);
      }
    }

    // Settings from Form /////////////////////////////////////////////////////

    /// <summary>
    /// Gets or sets gaze drawing mode.
    /// </summary>
    /// <seealso cref="ReplayDrawingModes"/>
    /// <value>A <see cref="ReplayDrawingModes"/> with the gaze drawing mode flags.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public ReplayDrawingModes GazeDrawingMode
    {
      get { return this.gazeDrawingMode; }
      set { this.gazeDrawingMode = value; }
    }

    /// <summary>
    /// Gets or sets mouse drawing mode.
    /// </summary>
    /// <seealso cref="ReplayDrawingModes"/>
    /// <value>A <see cref="ReplayDrawingModes"/> with the mouse drawing mode flags.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public ReplayDrawingModes MouseDrawingMode
    {
      get { return this.mouseDrawingMode; }
      set { this.mouseDrawingMode = value; }
    }

    /// <summary>
    /// Gets or sets factor for replay speed.
    /// </summary>
    /// <value>A <see cref="Single"/> with the replay speed.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public float Speed
    {
      get { return this.speed; }
      set { this.speed = value; }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the rectangle showing the visible
    /// part of the screen will be show or not.
    /// </summary>
    /// <value>A <see cref="Boolean"/> value that is <strong>true</strong>,
    /// when the rectangle should be shown.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool ShowVisiblePartOfScreen
    {
      get { return this.visiblePartOfScreen.Visible; }
      set { this.visiblePartOfScreen.Visible = value; }
    }

    /// <summary>
    /// Gets or sets maximum number of fixations to show in replay reduce data mode.
    /// </summary>
    /// <value>A <see cref="int"/> with the maximum numver of fixations to show,
    /// when <see cref="GazeDiscreteLength"/> or <see cref="MouseDiscreteLength"/>
    /// is set.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int NumFixToShow
    {
      get { return this.numFixToShow; }
      set { this.numFixToShow = value; }
    }

    /// <summary>
    /// Gets or sets a value indicating whether gaze path and fixations 
    /// should be truncated to fixed length
    /// </summary>
    /// <value>A <see cref="Boolean"/> value that is <strong>true</strong>,
    /// when the gaze path should be truncated after <see cref="NumFixToShow"/></value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool GazeDiscreteLength
    {
      get { return this.isGazeDiscreteLength; }
      set { this.isGazeDiscreteLength = value; }
    }

    /// <summary>
    /// Gets or sets a value indicating whether mouse path and fixations 
    /// should be truncated to fixed length
    /// </summary>
    /// <value>A <see cref="Boolean"/> value that is <strong>true</strong>,
    /// when the mouse path should be truncated after <see cref="NumFixToShow"/></value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool MouseDiscreteLength
    {
      get { return this.isMouseDiscreteLength; }
      set { this.isMouseDiscreteLength = value; }
    }

    /// <summary>
    /// Gets or sets gaze fixations diameter divisor.
    /// </summary>
    /// <value>A <see cref="Single"/> with the divisor for the gaze fixation
    /// diameters.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public float GazeFixationsDiameterDivisior
    {
      get { return this.gazeFixDiameterDiv; }
      set { this.gazeFixDiameterDiv = value; }
    }

    /// <summary>
    /// Gets or sets mouse fixations diameter divisor.
    /// </summary>
    /// <value>A <see cref="Single"/> with the divisor for the mouse fixation
    /// diameters.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public float MouseFixationsDiameterDivisor
    {
      get { return this.mouseFixDiameterDiv; }
      set { this.mouseFixDiameterDiv = value; }
    }

    /// <summary>
    /// Gets or sets a value indicating whether blinks should be displayed 
    /// by sending a semi transparent gray surface onto the scene
    /// </summary>
    /// <value>A <see cref="Boolean"/> that is <strong>true</strong>,
    /// when the module should visualize blinks with a blank slide.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool Blinks
    {
      get { return this.showBlinks; }
      set { this.showBlinks = value; }
    }

    /// <summary>
    /// Gets or sets new data table with sampling information from
    /// <see cref="SQLiteOgamaDataSet.RawdataDataTable"/></summary>
    /// <value>A <see cref="DataTable"/> that holds the samples for replay.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public DataTable ReplayDataTable
    {
      get
      {
        return this.replayTable;
      }

      set
      {
        this.replayTable = value;
        this.ResetPicture();
      }
    }

    // Graphic Objects /////////////////////////////////////////////////////////

    /// <summary>
    /// Gets or sets gaze cursor
    /// </summary>
    /// <value>A <see cref="VGCursor"/> that is used in the
    /// <see cref="ReplayDrawingModes.Cursor"/> gaze drawing mode.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public VGCursor GazeCursor
    {
      get { return this.gazeCursor; }
      set { this.gazeCursor = value; }
    }

    /// <summary>
    /// Gets or sets mouse cursor
    /// </summary>
    /// <value>A <see cref="VGCursor"/> that is used in the 
    /// <see cref="ReplayDrawingModes.Cursor"/> mouse drawing mode.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public VGCursor MouseCursor
    {
      get { return this.mouseCursor; }
      set { this.mouseCursor = value; }
    }

    // Drawing Elements ////////////////////////////////////////////////////////

    /// <summary>
    /// Gets or sets the gaze cursor pen.
    /// </summary>
    /// <value>A <see cref="Pen"/> that is used in the
    /// <see cref="ReplayDrawingModes.Cursor"/> gaze drawing mode for the cursor.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Pen PenGazeCursor
    {
      get { return this.penGazeCursor; }
      set { this.penGazeCursor = value; }
    }

    /// <summary>
    /// Gets or sets the gaze path pen.
    /// </summary>
    /// <value>A <see cref="Pen"/> that is used in the
    /// <see cref="ReplayDrawingModes.Path"/> and
    /// <see cref="ReplayDrawingModes.FixationConnections"/> gaze drawing modes
    /// for the paths.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Pen PenGazePath
    {
      get { return this.penGazePath; }
      set { this.penGazePath = value; }
    }

    /// <summary>
    /// Gets or sets gaze fixations pen.
    /// </summary>
    /// <value>A <see cref="Pen"/> that is used in the
    /// <see cref="ReplayDrawingModes.Fixations"/> gaze drawing mode
    /// for the fixations.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Pen PenGazeFixation
    {
      get { return this.penGazeFixation; }
      set { this.penGazeFixation = value; }
    }

    /// <summary>
    /// Gets or sets gaze fixation connections pen.
    /// </summary>
    /// <value>A <see cref="Pen"/> that is used in the
    /// <see cref="ReplayDrawingModes.FixationConnections"/> gaze drawing mode
    /// for the fixation connections.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Pen PenGazeFixationConnection
    {
      get { return this.penGazeFixationConnection; }
      set { this.penGazeFixationConnection = value; }
    }

    /// <summary>
    /// Gets or sets pen for no data connections.
    /// </summary>
    /// <value>A <see cref="Pen"/> that is used in the
    /// <see cref="ReplayDrawingModes.Fixations"/> gaze drawing mode
    /// for the connections that indicate empty gaze data.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Pen PenGazeNoData
    {
      get { return this.penGazeNoData; }
      set { this.penGazeNoData = value; }
    }

    /// <summary>
    /// Gets or sets mouse cursor pen.
    /// </summary>
    /// <value>A <see cref="Pen"/> that is used in the
    /// <see cref="ReplayDrawingModes.Cursor"/> mouse drawing mode for the cursor.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Pen PenMouseCursor
    {
      get { return this.penMouseCursor; }
      set { this.penMouseCursor = value; }
    }

    /// <summary>
    /// Gets or sets the mouse path pen.
    /// </summary>
    /// <value>A <see cref="Pen"/> that is used in the
    /// <see cref="ReplayDrawingModes.Path"/> and
    /// <see cref="ReplayDrawingModes.FixationConnections"/> mouse drawing modes
    /// for the paths.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Pen PenMousePath
    {
      get { return this.penMousePath; }
      set { this.penGazeCursor = value; }
    }

    /// <summary>
    /// Gets or sets mouse fixations pen.
    /// </summary>
    /// <value>A <see cref="Pen"/> that is used in the
    /// <see cref="ReplayDrawingModes.Fixations"/> mouse drawing mode
    /// for the fixations.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Pen PenMouseFixation
    {
      get { return this.penMouseFixation; }
      set { this.penMouseFixation = value; }
    }

    /// <summary>
    /// Gets or sets mouse fixation connections pen.
    /// </summary>
    /// <value>A <see cref="Pen"/> that is used in the
    /// <see cref="ReplayDrawingModes.FixationConnections"/> mouse drawing mode
    /// for the fixation connections.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Pen PenMouseFixationConnection
    {
      get { return this.penMouseFixationConnection; }
      set { this.penMouseFixationConnection = value; }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the video export should be cancelled.
    /// </summary>
    /// <value>A <see cref="Boolean"/> that holds a flag indicating the cancellation
    /// of the video export process.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool CancelVideoExport
    {
      get { return this.cancelVideoExport; }
      set { this.cancelVideoExport = value; }
    }

    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Public methods                                                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region PUBLICMETHODS

    /// <summary>
    /// When pictures settings property is set, this updates referring fields.
    /// </summary>
    public void InitializeSettings()
    {
      this.gazeMaxDistance = Document.ActiveDocument.ExperimentSettings.GazeMaxDistance;
      this.mouseMaxDistance = Document.ActiveDocument.ExperimentSettings.MouseMaxDistance;
      this.gazeMinSamples = Document.ActiveDocument.ExperimentSettings.GazeMinSamples;
      this.mouseMinSamples = Document.ActiveDocument.ExperimentSettings.MouseMinSamples;

      this.gazeFixDiameterDiv = Document.ActiveDocument.ExperimentSettings.GazeDiameterDiv;
      this.mouseFixDiameterDiv = Document.ActiveDocument.ExperimentSettings.MouseDiameterDiv;
    }

    /// <summary>
    /// Drawing initialization. Add elements referring to current drawing modes.
    /// </summary>
    /// <returns><strong>True</strong> if mouse or gaze data is available.</returns>
    public bool InitDrawingElements()
    {
      if (this.replayTable != null && this.replayTable.Rows.Count > 0)
      {
        // Determine StartTime to Initialize Progress detection
        var firstRow = this.replayTable.Rows[0];
        var trialStartTimeInMS = !firstRow.IsNull(3) ? Convert.ToInt64(firstRow[3]) : 0;
        var processBeginningTime = DateTime.Now;

        var gazeDataAvailable = false;
        var mouseDataAvailable = false;

        // Check for first valid eye and mouse sample
        PointF? test;
        for (var i = 0; i < this.replayTable.Rows.Count; i++)
        {
          if (!gazeDataAvailable)
          {
            if (this.CheckSamples(this.replayTable.Rows[i], SampleType.Gaze, out test))
            {
              gazeDataAvailable = true;
              if (mouseDataAvailable)
              {
                break;
              }
            }
          }

          if (!mouseDataAvailable)
          {
            if (this.CheckSamples(this.replayTable.Rows[i], SampleType.Mouse, out test))
            {
              mouseDataAvailable = true;
              if (gazeDataAvailable)
              {
                break;
              }
            }
          }
        }

        var parent = (FormWithPicture)this.OwningForm;
        if (parent.MdiParent != null)
        {
          if (!gazeDataAvailable && !mouseDataAvailable)
          {
            ((MainForm)parent.MdiParent).StatusRightLabel.Text = "No gaze or mouse data available for this trial.";
            return false;
          }
          else if (!gazeDataAvailable)
          {
            ((MainForm)parent.MdiParent).StatusRightLabel.Text = "No gaze data available for this trial.";
          }
          else if (!mouseDataAvailable)
          {
            ((MainForm)parent.MdiParent).StatusRightLabel.Text = "No mouse data available for this trial.";
          }
        }

        // Init currentLoopState
        this.currentLoopState.IsBlink = false;
        this.currentLoopState.GazeLastFixCenter.X = 0;
        this.currentLoopState.GazeLastFixCenter.Y = 0;
        this.currentLoopState.MouseLastFixCenter.X = 0;
        this.currentLoopState.MouseLastFixCenter.Y = 0;
        this.currentLoopState.IsOutOfMonitor = false;
        this.currentLoopState.RowCounter = 0;
        this.currentLoopState.TrialStartTimeInMS = trialStartTimeInMS;
        this.currentLoopState.ProcessBeginningTime = processBeginningTime;

        // Reset Cursor and Spotlight positions
        this.gazeCursor.Center = new PointF(-500, 0);
        this.gazePicEllipse.Center = new PointF(-500, 0);
        this.gazeFixEllipse.Center = new PointF(-500, 0);
        this.mouseCursor.Center = new PointF(-500, 0);
        this.mousePicEllipse.Center = new PointF(-500, 0);
        this.mouseFixEllipse.Center = new PointF(-500, 0);

        // Remove all old elements from drawing list.
        this.Elements.Clear();

        // At first ad Spotlights, because they have to be at top of background
        // and lines and circles on top of them.
        if (gazeDataAvailable && (this.gazeDrawingMode & ReplayDrawingModes.Spotlight) == ReplayDrawingModes.Spotlight)
        {
          // Add Spotlight Element: Ellipse with OriginalImageBrush
          this.Elements.Add(this.gazePicEllipse);
        }

        if (mouseDataAvailable && (this.mouseDrawingMode & ReplayDrawingModes.Spotlight) == ReplayDrawingModes.Spotlight)
        {
          // Add Spotlight Element: Ellipse with OriginalImageBrush
          this.Elements.Add(this.mousePicEllipse);
        }

        if (gazeDataAvailable && (this.gazeDrawingMode & ReplayDrawingModes.Path) == ReplayDrawingModes.Path)
        {
          // Add GazePolyline Element
          this.Elements.Add(this.gazeRawPolyline);
        }

        if (mouseDataAvailable && (this.mouseDrawingMode & ReplayDrawingModes.Path) == ReplayDrawingModes.Path)
        {
          // Add GazePolyline Element
          this.Elements.Add(this.mouseRawPolyline);
        }

        if (gazeDataAvailable && (this.gazeDrawingMode & ReplayDrawingModes.FixationConnections) == ReplayDrawingModes.FixationConnections)
        {
          this.objFixGazeDetection.InitFixation(this.gazeMinSamples);

          // Add FixGazePolyline Element
          this.Elements.Add(this.gazeFixConPolyline);
          this.Elements.Add(this.gazeFixConLine);
        }

        if (mouseDataAvailable && (this.mouseDrawingMode & ReplayDrawingModes.FixationConnections) == ReplayDrawingModes.FixationConnections)
        {
          this.objFixMouseDetection.InitFixation(this.mouseMinSamples);

          // Add FixGazePolyline Element
          this.Elements.Add(this.mouseFixConPolyline);
          this.Elements.Add(this.mouseFixConLine);
        }

        if (gazeDataAvailable && (this.gazeDrawingMode & ReplayDrawingModes.Fixations) == ReplayDrawingModes.Fixations)
        {
          this.objFixGazeDetection.InitFixation(this.gazeMinSamples);

          // Add FixationEllipse Element
          this.Elements.Add(this.gazeFixEllipse);
        }

        if (mouseDataAvailable && (this.mouseDrawingMode & ReplayDrawingModes.Fixations) == ReplayDrawingModes.Fixations)
        {
          this.objFixMouseDetection.InitFixation(this.mouseMinSamples);

          // Add FixationEllipse Element
          this.Elements.Add(this.mouseFixEllipse);
        }

        if (gazeDataAvailable && (this.gazeDrawingMode & ReplayDrawingModes.Cursor) == ReplayDrawingModes.Cursor)
        {
          // Add FixationEllipse Element
          this.Elements.Add(this.gazeCursor);
        }

        if (mouseDataAvailable && (this.mouseDrawingMode & ReplayDrawingModes.Cursor) == ReplayDrawingModes.Cursor)
        {
          // Add FixationEllipse Element
          this.Elements.Add(this.mouseCursor);
        }

        // Init VG Elements referring to DrawingModes
        var customColor = Color.FromArgb(200, Color.Black);
        var shadowBrush = new SolidBrush(customColor);
        this.rectBlink = new VGRectangle(
          ShapeDrawAction.Fill, 
          shadowBrush, 
          new RectangleF(0, 0, Document.ActiveDocument.ExperimentSettings.WidthStimulusScreen, Document.ActiveDocument.ExperimentSettings.HeightStimulusScreen));
        this.rectBlink.Visible = false;

        this.Elements.Add(this.rectBlink);

        // Readd the visible bounds rectangle
        this.Elements.Add(this.visiblePartOfScreen);

        this.DrawForeground(true);

        return true;
      }
      else
      {
        InformationDialog.Show(
          "Please note", 
          "No gaze or mouse data available for these settings", 
          false, 
          MessageBoxIcon.Warning);
        return false;
      }
    }

    /// <summary>
    /// Draws all graphic data up to given timing in milliseconds.
    /// </summary>
    /// <param name="endTime">
    /// end time in milliseconds
    /// </param>
    public void RenderUpToGivenTime(long endTime)
    {
      if (endTime == 0)
      {
        return;
      }

      this.RenderTimeRange((int)0, endTime);
    }

    /// <summary>
    /// Draws all graphic data in the given timing range.
    /// </summary>
    /// <param name="startTimeInMS">
    /// start time in milliseconds
    /// </param>
    /// <param name="endTimeInMS">
    /// end time in milliseconds
    /// </param>
    public void RenderTimeRangeInMS(int startTimeInMS, int endTimeInMS)
    {
      // Calculate samples in range.
      var startTimeInSamples = this.GetSynchronizedSampleCount(startTimeInMS);
      var endTimeInSamples = this.GetSynchronizedSampleCount(endTimeInMS);
      this.RenderTimeRange(startTimeInSamples, endTimeInSamples);
    }

    /// <summary>
    /// Renders all samples with current drawing settings up to given time into the returned bitmap.
    /// </summary>
    /// <param name="startSample">
    /// Ref. An <see cref="int"/> with the sample number of the start sample.
    /// </param>
    /// <param name="endTime">
    /// End time in milliseconds
    /// </param>
    /// <param name="videoSize">
    /// size of video
    /// </param>
    /// <param name="reachedEnd">
    /// Ref. The picture runs out of data.
    /// </param>
    /// <returns>
    /// A <see cref="Bitmap"/> with the rendered frame.
    /// </returns>
    public Bitmap RenderFrame(ref int startSample, long endTime, Size videoSize, ref bool reachedEnd)
    {
      // Calculate samples in range.
      var endTimeInSamples = this.GetSynchronizedSampleCount(endTime);

      if (endTimeInSamples >= this.replayTable.Rows.Count)
      {
        reachedEnd = true;
      }

      this.RenderTimeRange(startSample, endTimeInSamples);
      startSample = endTimeInSamples + 1;
      var currentFrame = this.RenderToImage();
      return Images.RescaleImage(videoSize, (Bitmap)currentFrame, false);
    }

    /// <summary>
    /// Draws a mouse click cursor at the given point
    /// </summary>
    /// <param name="pt">
    /// A <see cref="PointF"/> with the center of the mouse click.
    /// </param>
    /// <param name="button">
    /// A <see cref="MouseButtons"/> value that indicates the clicked button.
    /// </param>
    public void DrawMouseClick(PointF pt, MouseButtons button)
    {
      var cursorType = VGCursor.DrawingCursors.Mouse;
      switch (button)
      {
        case MouseButtons.Left:
          cursorType = VGCursor.DrawingCursors.MouseLeft;
          break;
        case MouseButtons.None:
          return;
        case MouseButtons.Right:
          cursorType = VGCursor.DrawingCursors.MouseRight;
          break;
      }

      var mouseClick = new VGCursor(
        Pens.White, 
        Brushes.Red, 
        cursorType, 
        20f, 
        VGStyleGroup.RPL_MOUSE_CLICK);

      mouseClick.Center = pt;

      this.Elements.Add(mouseClick);
    }

    /// <summary>
    /// This method saves the current pen and font styles to the application settings.
    /// </summary>
    public void SaveStylesToApplicationSettings()
    {
      // AppSettings are always set except when in design mode
      if (Properties.Settings.Default != null)
      {
        Properties.Settings.Default.GazeCursorColor = this.penGazeCursor.Color;
        Properties.Settings.Default.GazeCursorWidth = this.penGazeCursor.Width;
        Properties.Settings.Default.GazeCursorStyle = this.penGazeCursor.DashStyle;

        Properties.Settings.Default.GazePathColor = this.penGazePath.Color;
        Properties.Settings.Default.GazePathWidth = this.penGazePath.Width;
        Properties.Settings.Default.GazePathStyle = this.penGazePath.DashStyle;

        Properties.Settings.Default.GazeFixationsPenColor = this.penGazeFixation.Color;
        Properties.Settings.Default.GazeFixationsPenWidth = this.penGazeFixation.Width;
        Properties.Settings.Default.GazeFixationsPenStyle = this.penGazeFixation.DashStyle;

        Properties.Settings.Default.GazeFixationConnectionsPenColor = this.penGazeFixationConnection.Color;
        Properties.Settings.Default.GazeFixationConnectionsPenWidth = this.penGazeFixationConnection.Width;
        Properties.Settings.Default.GazeFixationConnectionsPenStyle = this.penGazeFixationConnection.DashStyle;

        Properties.Settings.Default.GazeNoDataColor = this.penGazeNoData.Color;
        Properties.Settings.Default.GazeNoDataWidth = this.penGazeNoData.Width;
        Properties.Settings.Default.GazeNoDataStyle = this.penGazeNoData.DashStyle;

        Properties.Settings.Default.MouseCursorColor = this.penMouseCursor.Color;
        Properties.Settings.Default.MouseCursorWidth = this.penMouseCursor.Width;
        Properties.Settings.Default.MouseCursorStyle = this.penMouseCursor.DashStyle;

        Properties.Settings.Default.MousePathColor = this.penMousePath.Color;
        Properties.Settings.Default.MousePathWidth = this.penMousePath.Width;
        Properties.Settings.Default.MousePathStyle = this.penMousePath.DashStyle;

        Properties.Settings.Default.MouseFixationsPenColor = this.penMouseFixation.Color;
        Properties.Settings.Default.MouseFixationsPenWidth = this.penMouseFixation.Width;
        Properties.Settings.Default.MouseFixationsPenStyle = this.penMouseFixation.DashStyle;

        Properties.Settings.Default.MouseFixationConnectionsPenColor = this.penMouseFixationConnection.Color;
        Properties.Settings.Default.MouseFixationConnectionsPenWidth = this.penMouseFixationConnection.Width;
        Properties.Settings.Default.MouseFixationConnectionsPenStyle = this.penMouseFixationConnection.DashStyle;

        Properties.Settings.Default.GazeCursorSize = (decimal)this.gazeCursor.Size.Width;
        Properties.Settings.Default.GazeCursorType = this.gazeCursor.CursorType.ToString();

        Properties.Settings.Default.MouseCursorSize = (decimal)this.mouseCursor.Size.Width;
        Properties.Settings.Default.MouseCursorType = this.mouseCursor.CursorType.ToString();

        Properties.Settings.Default.Save();
      }
    }

    /// <summary>
    /// <see cref="OgamaControls.Dialogs.PenStyleDlg.PenChanged"/> event handler. 
    /// Updates all graphic elements from the given group
    /// with the new pen.
    /// </summary>
    /// <param name="sender">
    /// Source of the event.
    /// </param>
    /// <param name="e">
    /// A <see cref="PenChangedEventArgs"/> that hold new group and pen
    /// </param>
    public void PenChanged(object sender, PenChangedEventArgs e)
    {
      var sublist = this.Elements.FindAllGroupMembers(e.ElementGroup);
      foreach (VGElement element in sublist)
      {
        element.Pen = e.Pen;
      }

      this.DrawForeground(true);

      switch (e.ElementGroup)
      {
        case VGStyleGroup.RPL_PEN_GAZE_CURSOR:
          this.penGazeCursor = e.Pen;
          break;
        case VGStyleGroup.RPL_PEN_GAZE_PATH:
          this.penGazePath = e.Pen;
          break;
        case VGStyleGroup.RPL_PEN_GAZE_FIX:
          this.penGazeFixation = e.Pen;
          break;
        case VGStyleGroup.RPL_PEN_GAZE_FIXCON:
          this.penGazeFixationConnection = e.Pen;
          break;
        case VGStyleGroup.RPL_PEN_GAZE_NODATA:
          this.penGazeNoData = e.Pen;
          break;
        case VGStyleGroup.RPL_PEN_MOUSE_CURSOR:
          this.penMouseCursor = e.Pen;
          break;
        case VGStyleGroup.RPL_PEN_MOUSE_PATH:
          this.penMousePath = e.Pen;
          break;
        case VGStyleGroup.RPL_PEN_MOUSE_FIX:
          this.penMouseFixation = e.Pen;
          break;
        case VGStyleGroup.RPL_PEN_MOUSE_FIXCON:
          this.penMouseFixationConnection = e.Pen;
          break;
        default:
          this.penGazeCursor = e.Pen;
          break;
      }
    }

    /// <summary>
    /// <see cref="DrawingCursorChanged"/> event handler. 
    /// Is Raised when the cursor element has changed.
    /// Updates all graphic elements from the given group
    /// with the new cursor shape.
    /// </summary>
    /// <param name="sender">
    /// Source of the event.
    /// </param>
    /// <param name="e">
    /// A <see cref="ShapeEventArgs"/> with the new cursor shape.
    /// </param>
    public void DrawingCursorChanged(object sender, ShapeEventArgs e)
    {
      switch (e.Shape.StyleGroup)
      {
        case VGStyleGroup.RPL_PEN_GAZE_CURSOR:
          e.Shape.Pen = this.penGazeCursor;
          this.gazeCursor = (VGCursor)e.Shape;
          break;
        case VGStyleGroup.RPL_PEN_MOUSE_CURSOR:
          e.Shape.Pen = this.penMouseCursor;
          this.mouseCursor = (VGCursor)e.Shape;
          break;
      }

      var sublist = this.Elements.FindAllGroupMembers(e.Shape.StyleGroup);
      if (sublist.Count == 1)
      {
        var storeCenter = sublist[0].Center;
        this.Elements.Remove(sublist[0]);
        e.Shape.Center = storeCenter;
        this.Elements.Add(e.Shape);
      }

      this.DrawForeground(true);
    }

    #endregion //PUBLICMETHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Inherited methods                                                         //
    ///////////////////////////////////////////////////////////////////////////////
    #region OVERRIDES

    /// <summary>
    /// Overridden. Removes all Elements from Element List and clears picture.
    /// </summary>
    public override void ResetPicture()
    {
      base.ResetPicture();
      if (this.gazeFixConPolyline != null)
      {
        // Hide all Elements
        this.gazeFixConPolyline.Clear();
        this.gazeRawPolyline.Clear();
        this.gazeFixConLine.Clear();
        this.mouseFixConPolyline.Clear();
        this.mouseRawPolyline.Clear();
        this.mouseFixConLine.Clear();
        this.gazeFixations.Clear();
        this.mouseFixations.Clear();
        this.pauseTime = TimeSpan.Zero;
        this.gazeFixEllipse.Bounds = RectangleF.Empty;
        this.objFixGazeDetection.InitFixation(this.gazeMinSamples);
        this.objFixMouseDetection.InitFixation(this.mouseMinSamples);
        this.visiblePartOfScreen.Location = PointF.Empty;
        this.Invalidate();
      }
    }

    /// <summary>
    /// Overridden. Calls picture update method. Invoked from Picture Animation timer tick method.
    /// </summary>
    /// <param name="sender">
    /// sending frame, normally base picture class timer
    /// </param>
    /// <param name="e">
    /// An empty <see cref="EventArgs"/>
    /// </param>
    /// <remarks>
    /// Starts updating the readed samples for the timespan that
    /// is over since the last update.
    /// </remarks>
    protected override void ForegroundTimerTick(object sender, EventArgs e)
    {
      this.DisplayUpdating();
      base.ForegroundTimerTick(sender, e);
    }

    /// <summary>
    /// Overridden. Frees resources of objects that are not disposed
    /// by the designer, mainly private objects.
    /// Is called during the call to <see cref="Control.Dispose(Boolean)"/>.
    /// </summary>
    protected override void CustomDispose()
    {
      if (this.replayTable != null)
      {
        this.replayTable.Dispose();
      }

      this.gazeFixations.Clear();
      this.mouseFixations.Clear();
      if (this.rectBlink != null)
      {
        this.rectBlink.Dispose();
      }

      this.gazePicEllipse.Dispose();
      this.mousePicEllipse.Dispose();
      this.gazeFixEllipse.Dispose();
      this.mouseFixEllipse.Dispose();
      this.gazeRawPolyline.Dispose();
      this.mouseRawPolyline.Dispose();
      this.gazeFixConPolyline.Dispose();
      this.mouseFixConPolyline.Dispose();
      this.gazeFixConLine.Dispose();
      this.mouseFixConLine.Dispose();
      this.gazeCursor.Dispose();
      this.mouseCursor.Dispose();
      this.penGazeCursor.Dispose();
      this.penGazePath.Dispose();
      this.penGazeFixation.Dispose();
      this.penGazeFixationConnection.Dispose();
      this.penGazeNoData.Dispose();
      this.penMouseCursor.Dispose();
      this.penMousePath.Dispose();
      this.penMouseFixation.Dispose();
      this.penMouseFixationConnection.Dispose();
      this.sndLeftClick.Dispose();
      this.sndRightClick.Dispose();

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

    /// <summary>
    /// The <see cref="TrialEventIDFound"/> event handler.
    /// Raised when trial event id was found in the raw data.
    /// </summary>
    /// <param name="e">
    /// A <see cref="TrialEventIDFoundEventArgs"/> with the new event id.
    /// </param>
    /// .
    private void OnTrialEventIDFound(TrialEventIDFoundEventArgs e)
    {
      if (this.TrialEventIDFound != null)
      {
        this.TrialEventIDFound(this, e);
      }
    }

    /// <summary>
    /// The <see cref="TrialSequenceChanged"/> event handler.
    /// Raised when trial sequence change was found in the raw data.
    /// </summary>
    /// <param name="e">
    /// A <see cref="TrialSequenceChangedEventArgs"/> with the new trial sequence.
    /// </param>
    /// .
    private void OnTrialSequenceChanged(TrialSequenceChangedEventArgs e)
    {
      if (this.TrialSequenceChanged != null)
      {
        this.TrialSequenceChanged(this, e);
      }
    }

    /// <summary>
    /// The <see cref="MouseMoved"/> event handler.
    /// Raised when mouse movement was found in the raw data.
    /// </summary>
    /// <param name="e">
    /// A <see cref="MouseEventArgs"/> with the event data.
    /// </param>
    /// .
    private void OnMouseMoved(MouseEventArgs e)
    {
      if (this.MouseMoved != null)
      {
        this.MouseMoved(this, e);
      }
    }

    #endregion //CUSTOMEVENTHANDLER

    #endregion //EVENTS

    ///////////////////////////////////////////////////////////////////////////////
    // Methods for doing main class job                                          //
    ///////////////////////////////////////////////////////////////////////////////
    #region METHODS

    /// <summary>
    /// Initializes fields if applicable;
    /// </summary>
    private void InitializeFields()
    {
      // For Designer support check for empty Application settings
      if (Properties.Settings.Default != null)
      {
        this.currentLoopState = new LoopState();
        this.currentLoopState.ProcessBeginningTime = DateTime.Now;

        this.showBlinks = Properties.Settings.Default.GazeModeBlinks;
        this.isGazeDiscreteLength = Properties.Settings.Default.GazeModeCutPath;
        this.isMouseDiscreteLength = Properties.Settings.Default.MouseModeCutPath;

        var speedValue = Properties.Settings.Default.ReplaySpeed;
        switch (speedValue)
        {
          case "10x": this.speed = 10f;
            break;
          case "5x": this.speed = 5f;
            break;
          case "3x": this.speed = 3f;
            break;
          case "2x": this.speed = 2f;
            break;
          case "1x": this.speed = 1f;
            break;
          case "0.5x": this.speed = 0.5f;
            break;
          case "0.3x": this.speed = 0.33f;
            break;
          case "0.2x": this.speed = 0.2f;
            break;
          case "0.1x": this.speed = 0.1f;
            break;
          default: this.speed = 1f;
            break;
        }

        this.objFixGazeDetection = new FixationDetection();
        this.objFixMouseDetection = new FixationDetection();

        this.gazeFixations = new VGElementCollection();
        this.mouseFixations = new VGElementCollection();

        if (Properties.Settings.Default.GazeModeCursor)
        {
          this.gazeDrawingMode |= ReplayDrawingModes.Cursor;
        }

        if (Properties.Settings.Default.GazeModePath)
        {
          this.gazeDrawingMode |= ReplayDrawingModes.Path;
        }

        if (Properties.Settings.Default.GazeModeFixations)
        {
          this.gazeDrawingMode |= ReplayDrawingModes.Fixations;
        }

        if (Properties.Settings.Default.GazeModeFixCon)
        {
          this.gazeDrawingMode |= ReplayDrawingModes.FixationConnections;
        }

        if (Properties.Settings.Default.GazeModeSpotlight)
        {
          this.gazeDrawingMode |= ReplayDrawingModes.Spotlight;
        }

        if (Properties.Settings.Default.MouseModeCursor)
        {
          this.mouseDrawingMode |= ReplayDrawingModes.Cursor;
        }

        if (Properties.Settings.Default.MouseModePath)
        {
          this.mouseDrawingMode |= ReplayDrawingModes.Path;
        }

        if (Properties.Settings.Default.MouseModeFixations)
        {
          this.mouseDrawingMode |= ReplayDrawingModes.Fixations;
        }

        if (Properties.Settings.Default.MouseModeFixCon)
        {
          this.mouseDrawingMode |= ReplayDrawingModes.FixationConnections;
        }

        if (Properties.Settings.Default.MouseModeSpotlight)
        {
          this.mouseDrawingMode |= ReplayDrawingModes.Spotlight;
        }

        this.maxLengthPath = (int)Properties.Settings.Default.MaxPointsPolyline;
        this.numFixToShow = (int)Properties.Settings.Default.MaxNumberFixations;

        // Initialize and load sounds for the replay of mouse clicks.
        this.sndLeftClick = new SoundPlayer();
        this.sndLeftClick.Stream = Properties.Resources.clickLeft;
        this.sndLeftClick.LoadAsync();

        this.sndRightClick = new SoundPlayer();
        this.sndRightClick.Stream = Properties.Resources.clickRight;
        this.sndRightClick.LoadAsync();
      }
    }

    /// <summary>
    /// Initializes standard values of drawing elements
    /// </summary>
    private void InitializeElements()
    {
      try
      {
        this.penGazeCursor = new Pen(Properties.Settings.Default.GazeCursorColor, Properties.Settings.Default.GazeCursorWidth);
        this.penGazeCursor.DashStyle = Properties.Settings.Default.GazeCursorStyle;

        this.penGazePath = new Pen(Properties.Settings.Default.GazePathColor, Properties.Settings.Default.GazePathWidth);
        this.penGazePath.DashStyle = Properties.Settings.Default.GazePathStyle;
        this.penGazePath.LineJoin = LineJoin.Round;

        this.penGazeFixation = new Pen(Properties.Settings.Default.GazeFixationsPenColor, Properties.Settings.Default.GazeFixationsPenWidth);
        this.penGazeFixation.DashStyle = Properties.Settings.Default.GazeFixationsPenStyle;

        this.penGazeFixationConnection = new Pen(Properties.Settings.Default.GazeFixationConnectionsPenColor, Properties.Settings.Default.GazeFixationConnectionsPenWidth);
        this.penGazeFixation.DashStyle = Properties.Settings.Default.GazeFixationConnectionsPenStyle;

        this.penGazeNoData = new Pen(Properties.Settings.Default.GazeNoDataColor, Properties.Settings.Default.GazeNoDataWidth);
        this.penGazeNoData.DashStyle = Properties.Settings.Default.GazeNoDataStyle;

        this.penMouseCursor = new Pen(Properties.Settings.Default.MouseCursorColor, Properties.Settings.Default.MouseCursorWidth);
        this.penMouseCursor.DashStyle = Properties.Settings.Default.MouseCursorStyle;

        this.penMousePath = new Pen(Properties.Settings.Default.MousePathColor, Properties.Settings.Default.MousePathWidth);
        this.penMousePath.DashStyle = Properties.Settings.Default.MousePathStyle;
        this.penMousePath.LineJoin = LineJoin.Round;

        this.penMouseFixation = new Pen(Properties.Settings.Default.MouseFixationsPenColor, Properties.Settings.Default.MouseFixationsPenWidth);
        this.penMouseFixation.DashStyle = Properties.Settings.Default.MouseFixationsPenStyle;

        this.penMouseFixationConnection = new Pen(Properties.Settings.Default.MouseFixationConnectionsPenColor, Properties.Settings.Default.MouseFixationConnectionsPenWidth);
        this.penMouseFixationConnection.DashStyle = Properties.Settings.Default.MouseFixationConnectionsPenStyle;

        this.gazePicEllipse = new VGEllipse(ShapeDrawAction.Fill, this.GrayBrush);
        this.gazePicEllipse.Inverted = true;
        this.gazePicEllipse.ElementGroup = "Default";
        this.mousePicEllipse = new VGEllipse(ShapeDrawAction.Fill, this.GrayBrush);
        this.mousePicEllipse.Inverted = true;
        this.mousePicEllipse.ElementGroup = "Default";

        this.gazeRawPolyline = new VGPolyline(ShapeDrawAction.Edge, this.penGazePath, VGStyleGroup.RPL_PEN_GAZE_PATH, string.Empty, string.Empty);
        this.gazeRawPolyline.ElementGroup = "Default";
        this.mouseRawPolyline = new VGPolyline(ShapeDrawAction.Edge, this.penMousePath, VGStyleGroup.RPL_PEN_MOUSE_PATH, string.Empty, string.Empty);
        this.mouseRawPolyline.ElementGroup = "Default";
        this.gazeFixEllipse = new VGEllipse(ShapeDrawAction.Edge, this.penGazeFixation, VGStyleGroup.RPL_PEN_GAZE_FIX, string.Empty, string.Empty);
        this.gazeFixEllipse.ElementGroup = "Default";
        this.mouseFixEllipse = new VGEllipse(ShapeDrawAction.Edge, this.penMouseFixation, VGStyleGroup.RPL_PEN_MOUSE_FIX, string.Empty, string.Empty);
        this.mouseFixEllipse.ElementGroup = "Default";
        this.gazeFixConPolyline = new VGPolyline(ShapeDrawAction.Edge, this.penGazeFixationConnection, VGStyleGroup.RPL_PEN_GAZE_FIXCON, string.Empty, string.Empty);
        this.gazeFixConPolyline.ElementGroup = "Default";
        this.mouseFixConPolyline = new VGPolyline(ShapeDrawAction.Edge, this.penMouseFixationConnection, VGStyleGroup.RPL_PEN_MOUSE_FIXCON, string.Empty, string.Empty);
        this.mouseFixConPolyline.ElementGroup = "Default";
        this.gazeFixConLine = new VGLine(ShapeDrawAction.Edge, this.penGazeFixationConnection, VGStyleGroup.RPL_PEN_GAZE_FIXCON, string.Empty, string.Empty);
        this.gazeFixConLine.ElementGroup = "Default";
        this.mouseFixConLine = new VGLine(ShapeDrawAction.Edge, this.penMouseFixationConnection, VGStyleGroup.RPL_PEN_MOUSE_FIXCON, string.Empty, string.Empty);
        this.mouseFixConLine.ElementGroup = "Default";

        var gazeCursorSize = (float)Properties.Settings.Default.GazeCursorSize;
        var gazeCursorType = (VGCursor.DrawingCursors)Enum.Parse(
          typeof(VGCursor.DrawingCursors), Properties.Settings.Default.GazeCursorType);
        this.gazeCursor = new VGCursor(this.penGazeCursor, gazeCursorType, gazeCursorSize, VGStyleGroup.RPL_PEN_GAZE_CURSOR);
        this.gazeCursor.ElementGroup = "Default";

        var mouseCursorSize = (float)Properties.Settings.Default.MouseCursorSize;
        var mouseCursorType = (VGCursor.DrawingCursors)Enum.Parse(
          typeof(VGCursor.DrawingCursors), Properties.Settings.Default.MouseCursorType);
        this.mouseCursor = new VGCursor(this.penMouseCursor, mouseCursorType, mouseCursorSize, VGStyleGroup.RPL_PEN_MOUSE_CURSOR);
        this.mouseCursor.ElementGroup = "Default";

        if (Document.ActiveDocument != null)
        {
          this.visiblePartOfScreen = new VGRectangle(
            ShapeDrawAction.Edge, 
            Pens.Red, 
            Document.ActiveDocument.PresentationSizeRectangle);
          this.visiblePartOfScreen.Visible = false;
        }
      }
      catch (Exception ex)
      {
        ExceptionMethods.HandleException(ex);
      }
    }

    /// <summary>
    /// Main animation method. Loops current samples and updates
    /// drawing elements referring to time left since last call.
    /// </summary>
    /// <remarks>Switches drawing modes and calls referring special drawing methods.</remarks>
    private void DisplayUpdating()
    {
      // Initialize progress variables
      var isFinished = false;
      var percentComplete = 0;

      if (this.currentLoopState.RowCounter < this.replayTable.Rows.Count)
      {
        var processEndingTime = DateTime.Now.Ticks - this.pauseTime.Ticks;
        var differenceInTicks = processEndingTime - this.currentLoopState.ProcessBeginningTime.Ticks;
        var differenceInMS = differenceInTicks / 10000f * this.speed;

        this.RenderTimeRange(this.currentLoopState.RowCounter, (long)differenceInMS);
      }

      // Report progress as a percentage of the total task.
      percentComplete = Convert.ToInt32(Convert.ToSingle(this.currentLoopState.RowCounter) /
        this.replayTable.Rows.Count * 100);

      var rowTimeInMS = 0;

      if (this.currentLoopState.RowCounter >= this.replayTable.Rows.Count)
      {
        // Stop Updating
        isFinished = true;
      }
      else
      {
        // Get current DataRow
        var row = this.replayTable.Rows[this.currentLoopState.RowCounter];

        // Catch actual TrialTime
        rowTimeInMS = (int)(Convert.ToInt64(row[3]) - this.currentLoopState.TrialStartTimeInMS);
      }

      var pea = new ProgressEventArgs(isFinished, percentComplete, rowTimeInMS);
      this.OnProgress(pea);
    }

    /// <summary>
    /// Draws all graphic data in the given timing range.
    /// </summary>
    /// <param name="startTimeInSamples">
    /// start time in samples
    /// </param>
    /// <param name="endTimeInMS">
    /// end time in milliseconds
    /// </param>
    private void RenderTimeRange(int startTimeInSamples, long endTimeInMS)
    {
      // Calculate samples in range.
      var endTimeInSamples = this.GetSynchronizedSampleCount(endTimeInMS);
      this.RenderTimeRange(startTimeInSamples, endTimeInSamples);
      //Console.WriteLine(startTimeInSamples + "-" + endTimeInSamples);
    }

    /// <summary>
    /// Draws all graphic data in the given timing range.
    /// </summary>
    /// <param name="startTimeInSamples">
    /// start time in samples
    /// </param>
    /// <param name="endTimeInSamples">
    /// end time in samples
    /// </param>
    private void RenderTimeRange(int startTimeInSamples, int endTimeInSamples)
    {
      // avoid empty drawing
      if (startTimeInSamples == endTimeInSamples)
      {
        return;
      }

      // Get number of sample data rows.
      var rowsCount = this.replayTable.Rows.Count;

      if (endTimeInSamples > startTimeInSamples)
      {
        // Calculate number of samples to render
        var numberOfSamplesToRender = endTimeInSamples - startTimeInSamples;

        // Paranoia check
        if (startTimeInSamples + numberOfSamplesToRender > rowsCount)
        {
          numberOfSamplesToRender = rowsCount - startTimeInSamples - 1;
        }

        var rows = new DataRow[Math.Abs(numberOfSamplesToRender)];

        for (var i = startTimeInSamples; i < startTimeInSamples + numberOfSamplesToRender; i++)
        {
          rows[i - startTimeInSamples] = this.replayTable.Rows[i];
        }

        this.DrawSamples(rows, false);
        this.currentLoopState.RowCounter += numberOfSamplesToRender;
      }
    }

    /// <summary>
    /// This method draws the samples in the given data row array with
    /// the currently selected <see cref="ReplayDrawingModes"/> and
    /// sets the visibility of the blink rectangle.
    /// It calls internally the overload DrawSamples(DataRow[],ReplayDrawingModes,SampleType,Boolean).
    /// </summary>
    /// <param name="rows">
    /// An array of <see cref="DataRow"/> with
    /// the sampling data.
    /// </param>
    /// <param name="remove">
    /// Indicates whether to remove the given samples or not.
    /// </param>
    private void DrawSamples(DataRow[] rows, bool remove)
    {
      try
      {
        // Gaze variables
        PointF? newGazeSamplePoint;
        bool isValidGazeData;
        var lastValidGazeSample = new TimedPoint();
        var firstValidGazeSample = new TimedPoint(-1, PointF.Empty);
        var validGazeFixationSamples = new List<TimedPoint>();
        var validGazePathSamples = new List<PointF>();

        // Mouse variables
        PointF? newMouseSamplePoint;
        bool isValidMouseData;
        var lastValidMouseSample = new TimedPoint();
        var firstValidMouseSample = new TimedPoint(-1, PointF.Empty);
        var validMouseFixationSamples = new List<TimedPoint>();
        var validMousePathSamples = new List<PointF>();
        var validMouseClicks = new List<MouseStopCondition>();

        foreach (var row in rows)
        {
          // Check trial sequence change
          var trialSequence = (int)row["TrialSequence"];

          if (trialSequence != this.currentTrialSequence)
          {
            this.OnTrialSequenceChanged(new TrialSequenceChangedEventArgs(trialSequence));
            this.currentTrialSequence = trialSequence;
          }

          // Check events
          if (!row.IsNull("EventID"))
          {
            var eventID = (int)row["EventID"];
            if (((ReplayModule)this.OwningForm).TrialEvents.ContainsKey(eventID))
            {
              if (!remove)
              {
                this.OnTrialEventIDFound(new TrialEventIDFoundEventArgs(eventID));
              }

              var occuredEvent = ((ReplayModule)this.OwningForm).TrialEvents[eventID];
              switch (occuredEvent.Type)
              {
                case EventType.None:
                  break;
                case EventType.Mouse:
                  switch (((InputEvent)occuredEvent).Task)
                  {
                    case InputEventTask.Down:
                      var parameter = occuredEvent.Param;
                      var msc =
                        (MouseStopCondition)TypeDescriptor.GetConverter(typeof(StopCondition)).ConvertFrom(parameter);
                      validMouseClicks.Add(msc);
                      break;
                  }

                  break;
                case EventType.Key:
                  break;
                case EventType.Slide:
                  break;
                case EventType.Flash:
                  break;
                case EventType.Audio:
                  break;
                case EventType.Video:
                  break;
                case EventType.Usercam:
                  break;
                case EventType.Response:
                  break;
                case EventType.Marker:
                  break;
                case EventType.Scroll:

                  // Update scroll position
                  var scrollOffsets = occuredEvent.Param.Split(';');
                  var newScrollPosition = new Point(
                    Convert.ToInt32(scrollOffsets[0]), 
                    Convert.ToInt32(scrollOffsets[1]));
                  this.visiblePartOfScreen.Location = newScrollPosition;
                  break;
                case EventType.Webpage:
                  break;
                default:
                  break;
              }
            }
          }

          // Iterate through rows fetching valid mouse samples and sending move events
          isValidMouseData = this.CheckSamples(row, SampleType.Mouse, out newMouseSamplePoint);
          var sampleTime = Convert.ToInt64(row[3].ToString());

          if (isValidMouseData)
          {
            validMouseFixationSamples.Add(
              new TimedPoint(sampleTime, newMouseSamplePoint.Value));

            if (!newMouseSamplePoint.Value.IsEmpty && !Queries.OutOfScreen(newMouseSamplePoint.Value, this.PresentationSize))
            {
              if (Point.Round(newMouseSamplePoint.Value) != Point.Round(lastValidMouseSample.Position))
              {
                validMousePathSamples.Add(newMouseSamplePoint.Value);
              }

              lastValidMouseSample = new TimedPoint(sampleTime, newMouseSamplePoint.Value);

              if (firstValidMouseSample.Time == -1)
              {
                firstValidMouseSample = new TimedPoint(sampleTime, newMouseSamplePoint.Value);
              }
            }
          }

          // Iterate through rows fetching valid samples
          if (this.gazeDrawingMode != ReplayDrawingModes.None)
          {
            isValidGazeData = this.CheckSamples(row, SampleType.Gaze, out newGazeSamplePoint);
            if (isValidGazeData)
            {
              validGazeFixationSamples.Add(
                new TimedPoint(sampleTime, newGazeSamplePoint.Value));

              if (!newGazeSamplePoint.Value.IsEmpty && !Queries.OutOfScreen(
                newGazeSamplePoint.Value, 
                this.PresentationSize))
              {
                if (Point.Round(newGazeSamplePoint.Value) != Point.Round(lastValidGazeSample.Position))
                {
                  validGazePathSamples.Add(newGazeSamplePoint.Value);
                }

                lastValidGazeSample = new TimedPoint(sampleTime, newGazeSamplePoint.Value);

                if (firstValidGazeSample.Time == -1)
                {
                  firstValidGazeSample = new TimedPoint(sampleTime, newGazeSamplePoint.Value);
                }
              }
            }
          }
        }

        if (remove)
        {
          // Remove gaze
          this.RemoveValidSampleRange(
            this.gazeDrawingMode, 
            SampleType.Gaze, 
            firstValidGazeSample, 
            validGazeFixationSamples, 
            validGazePathSamples);

          // Remove mouse
          this.RemoveValidSampleRange(
            this.mouseDrawingMode, 
            SampleType.Mouse, 
            firstValidMouseSample, 
            validMouseFixationSamples, 
            validMousePathSamples);
        }
        else
        {
          // Draw gaze
          this.DrawValidSampleRange(
            this.gazeDrawingMode, 
            SampleType.Gaze, 
            lastValidGazeSample, 
            validGazeFixationSamples, 
            validGazePathSamples, 
            null);

          // Draw mouse
          this.DrawValidSampleRange(
            this.mouseDrawingMode, 
            SampleType.Mouse, 
            lastValidMouseSample, 
            validMouseFixationSamples, 
            validMousePathSamples, 
            validMouseClicks);
        }

        if (this.showBlinks)
        {
          if (this.currentLoopState.IsBlink)
          {
            this.rectBlink.Visible = true;
          }
          else
          {
            this.rectBlink.Visible = false;
          }
        }
      }
      catch (Exception ex)
      {
        ExceptionMethods.HandleException(ex);
      }
    }

    /// <summary>
    /// This method removes all given samples drawing elements from
    /// the canvas.
    /// </summary>
    /// <param name="drawingMode">
    /// The <see cref="ReplayDrawingModes"/> to use.
    /// </param>
    /// <param name="sampleType">
    /// The <see cref="SampleType"/> to use
    /// </param>
    /// <param name="firstValidSample">
    /// A <see cref="TimedPoint"/> with the first valid sample
    /// to be removed.
    /// </param>
    /// <param name="validFixationSamples">
    /// A <see cref="List{TimedPoint}"/> with 
    /// valid fixation samples.
    /// </param>
    /// <param name="validPathSamples">
    /// A <see cref="List{PointF}"/> with 
    /// valid path samples.
    /// </param>
    private void RemoveValidSampleRange(
      ReplayDrawingModes drawingMode, 
      SampleType sampleType, 
      TimedPoint firstValidSample, 
      List<TimedPoint> validFixationSamples, 
      List<PointF> validPathSamples)
    {
      if (((drawingMode & ReplayDrawingModes.Fixations) == ReplayDrawingModes.Fixations)
         || ((drawingMode & ReplayDrawingModes.FixationConnections) == ReplayDrawingModes.FixationConnections))
      {
        this.RemoveFixations(firstValidSample);
      }

      if ((drawingMode & ReplayDrawingModes.Path) == ReplayDrawingModes.Path)
      {
        this.RemovePathPoints(validPathSamples, sampleType);
      }

      if ((drawingMode & ReplayDrawingModes.Clicks) == ReplayDrawingModes.Clicks)
      {
        this.RemoveMouseClicks();
      }

      if (!this.currentLoopState.IsBlink)
      {
        if ((drawingMode & ReplayDrawingModes.Cursor) == ReplayDrawingModes.Cursor)
        {
          this.DrawCursor(firstValidSample, sampleType);
        }

        if ((drawingMode & ReplayDrawingModes.Spotlight) == ReplayDrawingModes.Spotlight)
        {
          this.DrawSpotlight(firstValidSample, sampleType);
        }
      }
    }

    /// <summary>
    /// This method draws the given range of samples with the given drawing modes.
    /// </summary>
    /// <param name="drawingMode">
    /// The <see cref="ReplayDrawingModes"/> to use.
    /// </param>
    /// <param name="sampleType">
    /// The <see cref="SampleType"/> to use
    /// </param>
    /// <param name="lastValidSample">
    /// A <see cref="TimedPoint"/> with the last valid sample.
    /// </param>
    /// <param name="validFixationSamples">
    /// A <see cref="List{TimedPoint}"/> with 
    /// valid fixation samples.
    /// </param>
    /// <param name="validPathSamples">
    /// A <see cref="List{PointF}"/> with 
    /// valid path samples.
    /// </param>
    /// <param name="validMouseClicks">
    /// A <see cref="List{MouseStopCondition}"/> with 
    /// valid mouse clicks.
    /// </param>
    private void DrawValidSampleRange(
      ReplayDrawingModes drawingMode, 
      SampleType sampleType, 
      TimedPoint lastValidSample, 
      List<TimedPoint> validFixationSamples, 
      List<PointF> validPathSamples, 
      List<MouseStopCondition> validMouseClicks)
    {
      if (((drawingMode & ReplayDrawingModes.Fixations) == ReplayDrawingModes.Fixations)
         || ((drawingMode & ReplayDrawingModes.FixationConnections) == ReplayDrawingModes.FixationConnections))
      {
        foreach (var point in validFixationSamples)
        {
          this.DrawFixations(point.Time, point.Position, sampleType, false);
        }

        if (validFixationSamples.Count > 0)
        {
          var lastvalidFixationSample = validFixationSamples[validFixationSamples.Count - 1];
          this.DrawFixations(lastvalidFixationSample.Time, lastvalidFixationSample.Position, sampleType, true);
        }
      }

      if ((drawingMode & ReplayDrawingModes.Clicks) == ReplayDrawingModes.Clicks)
      {
        if (validMouseClicks == null)
        {
          throw new ArgumentNullException("Mouse click list is null, but drawing is activated.");
        }

        this.DrawMouseClicks(validMouseClicks);
      }

      if ((drawingMode & ReplayDrawingModes.Path) == ReplayDrawingModes.Path)
      {
        this.DrawPaths(validPathSamples, sampleType, lastValidSample.Time);
      }

      if (!this.currentLoopState.IsBlink)
      {
        if ((drawingMode & ReplayDrawingModes.Cursor) == ReplayDrawingModes.Cursor)
        {
          this.DrawCursor(lastValidSample, sampleType);
        }

        if ((drawingMode & ReplayDrawingModes.Spotlight) == ReplayDrawingModes.Spotlight)
        {
          this.DrawSpotlight(lastValidSample, sampleType);
        }
      }
    }

    /// <summary>
    /// Draws the mouse clicks in the given list.
    /// </summary>
    /// <param name="validMouseClicks">
    /// A <see cref="List{MouseStopCondition}"/>
    /// with the mouse clicks.
    /// </param>
    private void DrawMouseClicks(List<MouseStopCondition> validMouseClicks)
    {
      foreach (var msc in validMouseClicks)
      {
        this.DrawMouseClick(msc.ClickLocation, msc.StopMouseButton);
      }
    }

    /// <summary>
    /// Removes all mouse clicks elements from the picture.
    /// </summary>
    private void RemoveMouseClicks()
    {
      var clickElements = this.Elements.FindAllGroupMembers(VGStyleGroup.RPL_MOUSE_CLICK);
      this.Elements.RemoveAll(clickElements);
    }

    /// <summary>
    /// Updates mouse or gaze cursor position
    /// </summary>
    /// <param name="newPt">
    /// A <see cref="PointF"/> with the new sampling data.
    /// </param>
    /// <param name="toDraw">
    /// The <see cref="SampleType"/> to draw.
    /// </param>
    private void DrawCursor(TimedPoint newPt, SampleType toDraw)
    {
      if (newPt.Position.IsEmpty)
      {
        return;
      }

      switch (toDraw)
      {
        case SampleType.Gaze:
          this.gazeCursor.Center = newPt.Position;
          break;
        case SampleType.Mouse:
          this.mouseCursor.Center = newPt.Position;
          break;
      }
    }

    /// <summary>
    /// This method adds the new valid sample points to the <see cref="SampleType"/>
    /// specific <see cref="VGPolyline"/>.
    /// </summary>
    /// <param name="validSamples">
    /// A <see cref="List{PointF}"/> with the new
    /// sample data.
    /// </param>
    /// <param name="toDraw">
    /// The <see cref="SampleType"/> to draw.
    /// </param>
    /// <param name="lastSampleTime">
    /// The time estimation of the last polyline point.
    /// </param>
    private void DrawPaths(List<PointF> validSamples, SampleType toDraw, long lastSampleTime)
    {
      switch (toDraw)
      {
        case SampleType.Gaze:
          this.AddPtsToPolyline(this.gazeRawPolyline, validSamples, this.isGazeDiscreteLength, lastSampleTime);
          break;
        case SampleType.Mouse:
          this.AddPtsToPolyline(this.mouseRawPolyline, validSamples, this.isMouseDiscreteLength, lastSampleTime);
          break;
      }
    }

    /// <summary>
    /// This method removes the given list of points from the path of
    /// the given sample type.
    /// </summary>
    /// <param name="validPathSamples">
    /// A <see cref="List{PointF}"/>
    /// with the samples to be removed.
    /// </param>
    /// <param name="sampleType">
    /// The <see cref="SampleType"/> this list
    /// belongs to.
    /// </param>
    private void RemovePathPoints(List<PointF> validPathSamples, SampleType sampleType)
    {
      if (validPathSamples.Count > 1)
      {
        switch (sampleType)
        {
          case SampleType.Gaze:
            this.gazeRawPolyline.RemoveLastPts(validPathSamples.Count);
            break;
          case SampleType.Mouse:
            this.mouseRawPolyline.RemoveLastPts(validPathSamples.Count);
            break;
        }
      }
    }

    /// <summary>
    /// Draws fixations and fixation connections.
    /// Updates fixation objects and calculates fixation parameters.
    /// </summary>
    /// <param name="pointTime">
    /// The time estimation of the new point.
    /// </param>
    /// <param name="newPt">
    /// A <see cref="PointF"/> with the new sampling data.
    /// </param>
    /// <param name="toDraw">
    /// The <see cref="SampleType"/> to draw.
    /// </param>
    /// <param name="lastPointInSampleRange">
    /// A <see cref="Boolean"/>
    /// indicating whether this sample is the last one in the range of
    /// the current update, so this is to update the current fixations diameter.
    /// </param>
    private void DrawFixations(
      long pointTime, 
      PointF newPt, 
      SampleType toDraw, 
      bool lastPointInSampleRange)
    {
      bool point_found_delayed;
      float x_delayed;
      float y_delayed;
      float deviation_delayed;
      float x_fix_delayed;
      float y_fix_delayed;
      int saccade_duration_delayed;
      long fix_start_time;
      int fix_duration_delayed_samples;
      long fix_duration_delayed_milliseconds;

      VGPolyline usedPolyline;
      VGLine usedLine;
      VGEllipse usedEllipse;
      Pen usedFixationPen;
      PointF usedFixCenterPt;
      var usedBoundingRect = new RectangleF();
      VGStyleGroup usedGroup;
      EyeMotionState currentState;
      float divisor;
      switch (toDraw)
      {
        case SampleType.Gaze:
        default:
          currentState = this.objFixGazeDetection.DetectFixation(
            newPt.IsEmpty ? false : true, 
            pointTime, 
            newPt.X, 
            newPt.Y, 
             this.gazeMaxDistance, 
             this.gazeMinSamples, 
            out point_found_delayed, 
            out x_delayed, 
            out y_delayed, 
            out deviation_delayed, 
            out x_fix_delayed, 
            out y_fix_delayed, 
            out saccade_duration_delayed, 
            out fix_start_time, 
            out fix_duration_delayed_milliseconds, 
            out fix_duration_delayed_samples);
          usedPolyline = this.gazeFixConPolyline;
          usedLine = this.gazeFixConLine;
          usedEllipse = this.gazeFixEllipse;
          usedFixationPen = this.penGazeFixation;
          usedFixCenterPt = this.currentLoopState.GazeLastFixCenter;
          usedGroup = VGStyleGroup.RPL_PEN_GAZE_FIX;
          divisor = this.gazeFixDiameterDiv;
          break;
        case SampleType.Mouse:
          currentState = this.objFixMouseDetection.DetectFixation(
            newPt.IsEmpty ? false : true, 
            pointTime, 
            newPt.X, 
            newPt.Y, 
             this.mouseMaxDistance, 
             this.mouseMinSamples, 
            out point_found_delayed, 
            out x_delayed, 
            out y_delayed, 
            out deviation_delayed, 
            out x_fix_delayed, 
            out y_fix_delayed, 
            out saccade_duration_delayed, 
            out fix_start_time, 
            out fix_duration_delayed_milliseconds, 
            out fix_duration_delayed_samples);
          usedPolyline = this.mouseFixConPolyline;
          usedLine = this.mouseFixConLine;
          usedEllipse = this.mouseFixEllipse;
          usedFixationPen = this.penMouseFixation;
          usedFixCenterPt = this.currentLoopState.MouseLastFixCenter;
          usedGroup = VGStyleGroup.RPL_PEN_MOUSE_FIX;
          divisor = this.mouseFixDiameterDiv;
          break;
      }

      // Calculate Bounding Rectangle
      var fixationDiameter = Convert.ToSingle(fix_duration_delayed_samples) / divisor;
      usedBoundingRect.X = x_fix_delayed - fixationDiameter;
      usedBoundingRect.Y = y_fix_delayed - fixationDiameter;
      usedBoundingRect.Width = fixationDiameter * 2;
      usedBoundingRect.Height = fixationDiameter * 2;

      var fixationCenter = new PointF(x_fix_delayed, y_fix_delayed);

      switch (currentState)
      {
        case EyeMotionState.MOVING:
          if (!Queries.OutOfScreen(newPt, this.PresentationSize))
          {
            if (lastPointInSampleRange)
            {
              this.UpdateLastPtInLine(usedLine, newPt, pointTime);
            }
          }
          else
          {
            this.currentLoopState.IsOutOfMonitor = true;
          }

          this.onsetTime = pointTime;
          break;

        case EyeMotionState.FIXATING:
          if (lastPointInSampleRange)
          {
            usedEllipse.Bounds = usedBoundingRect;
            this.UpdateLastPtInLine(usedLine, fixationCenter, pointTime);
          }

          break;

        case EyeMotionState.FIXATION_COMPLETED:
          if (!Queries.OutOfScreen(fixationCenter, this.PresentationSize))
          {
            if (this.currentLoopState.IsOutOfMonitor)
            {
              this.currentLoopState.IsOutOfMonitor = false;
              var dottedLine = new VGLine(ShapeDrawAction.Edge, this.penGazeNoData, VGStyleGroup.RPL_PEN_GAZE_NODATA, string.Empty, string.Empty);
              dottedLine.FirstPoint = usedFixCenterPt;
              dottedLine.SecondPoint = fixationCenter;
              dottedLine.OnsetTime = pointTime;
              dottedLine.EndTime = pointTime;
              switch (toDraw)
              {
                case SampleType.Gaze:
                  if (((this.gazeDrawingMode & ReplayDrawingModes.FixationConnections) == ReplayDrawingModes.FixationConnections) &&
                      (!this.isGazeDiscreteLength))
                  {
                    this.Elements.Add(dottedLine);
                  }

                  break;
                case SampleType.Mouse:
                  if (((this.mouseDrawingMode & ReplayDrawingModes.FixationConnections) == ReplayDrawingModes.FixationConnections) &&
                      (!this.isMouseDiscreteLength))
                  {
                    this.Elements.Add(dottedLine);
                  }

                  break;
              }
            }

            var ellipse2 = new VGEllipse(
              ShapeDrawAction.Edge, 
              usedFixationPen, 
              usedBoundingRect, 
              usedGroup, 
              string.Empty, 
              string.Empty);
            ellipse2.OnsetTime = this.onsetTime;
            ellipse2.EndTime = pointTime;

            switch (toDraw)
            {
              default:
              case SampleType.Gaze:
                this.UpdateFirstPtInLine(usedLine, fixationCenter, pointTime);
                this.AddPtToPolyline(usedPolyline, fixationCenter, this.isGazeDiscreteLength, pointTime);
                this.currentLoopState.GazeLastFixCenter = fixationCenter;

                this.gazeFixations.Add(ellipse2);

                // Switch to fixed length if choosen
                if (this.isGazeDiscreteLength && (this.gazeFixations.Count >= this.numFixToShow))
                {
                  this.Elements.Remove(this.gazeFixations[0]);
                  this.gazeFixations.RemoveAt(0);
                  while (usedPolyline.GetPointCount() >= this.numFixToShow)
                  {
                    usedPolyline.RemoveFirstPt();
                  }
                }

                if ((this.gazeDrawingMode & ReplayDrawingModes.Fixations) == ReplayDrawingModes.Fixations)
                {
                  Elements.Add(ellipse2);
                }

                break;
              case SampleType.Mouse:
                this.UpdateFirstPtInLine(usedLine, fixationCenter, pointTime);
                this.AddPtToPolyline(usedPolyline, fixationCenter, this.isMouseDiscreteLength, pointTime);
                this.currentLoopState.MouseLastFixCenter = fixationCenter;
                this.mouseFixations.Add(ellipse2);

                // Switch to fixed length if choosen
                if (this.isMouseDiscreteLength && (this.mouseFixations.Count >= this.numFixToShow))
                {
                  this.Elements.Remove(this.mouseFixations[0]);
                  this.mouseFixations.RemoveAt(0);
                  while (usedPolyline.GetPointCount() >= this.numFixToShow)
                  {
                    usedPolyline.RemoveFirstPt();
                  }
                }

                if ((this.mouseDrawingMode & ReplayDrawingModes.Fixations) == ReplayDrawingModes.Fixations)
                {
                  this.Elements.Add(ellipse2);
                }

                break;
            }
          }
          else
          {
            this.currentLoopState.IsOutOfMonitor = true;
          }

          break;
        case EyeMotionState.ERROR:
          break;
        default:
          break;
      }
    }

    /// <summary>
    /// This method removes the elements for which the onset time
    /// is greater than the time of the given
    /// first valid sample.
    /// </summary>
    /// <param name="firstValidSample">
    /// A <see cref="TimedPoint"/>
    /// with the sample that is the first one not to be removed
    /// </param>
    private void RemoveFixations(TimedPoint firstValidSample)
    {
      var elementsToRemove = new VGElementCollection();
      var removedGazeFixationsCount = 0;
      var removedMouseFixationsCount = 0;
      foreach (VGElement element in this.Elements)
      {
        if (element.ElementGroup != "Default")
        {
          if (element.OnsetTime > firstValidSample.Time)
          {
            if (element.StyleGroup == VGStyleGroup.RPL_PEN_GAZE_FIX)
            {
              removedGazeFixationsCount++;
            }
            else if (element.StyleGroup == VGStyleGroup.RPL_PEN_MOUSE_FIX)
            {
              removedMouseFixationsCount++;
            }

            elementsToRemove.Add(element);
          }
          else if (element.EndTime > firstValidSample.Time)
          {
            // Scale ellipse
          }
        }
      }

      this.gazeFixations.RemoveRange(this.gazeFixations.Count - removedGazeFixationsCount, removedGazeFixationsCount);
      this.mouseFixations.RemoveRange(this.mouseFixations.Count - removedMouseFixationsCount, removedMouseFixationsCount);

      this.gazeFixConPolyline.RemoveLastPts(removedGazeFixationsCount);
      this.mouseFixConPolyline.RemoveLastPts(removedMouseFixationsCount);

      this.Elements.RemoveAll(elementsToRemove);
    }

    /// <summary>
    /// Updates spotlight circle position with new sampling point.
    /// </summary>
    /// <param name="newPt">
    /// A <see cref="PointF"/> with the new sampling data.
    /// </param>
    /// <param name="toDraw">
    /// The <see cref="SampleType"/> to draw.
    /// </param>
    private void DrawSpotlight(TimedPoint newPt, SampleType toDraw)
    {
      switch (toDraw)
      {
        case SampleType.Gaze:

          // Draw circle
          var gazeRadius = this.gazeFixDiameterDiv * 10;
          var gazeBubbleRect = new RectangleF(
            newPt.Position.X - gazeRadius, 
            newPt.Position.Y - gazeRadius, 
            2 * gazeRadius, 
            2 * gazeRadius);
          this.gazePicEllipse.Bounds = gazeBubbleRect;
          break;
        case SampleType.Mouse:

          // Draw circle
          var mouseRadius = this.mouseFixDiameterDiv * 10;
          var mouseBubbleRect = new RectangleF(
            newPt.Position.X - mouseRadius, 
            newPt.Position.Y - mouseRadius, 
            2 * mouseRadius, 
            2 * mouseRadius);
          this.mousePicEllipse.Bounds = mouseBubbleRect;
          break;
      }
    }

    /// <summary>
    /// This method returns the sample row thats time is as near as possible
    /// to the given time in ms.
    /// </summary>
    /// <param name="timeInMS">
    /// A <see cref="long"/> with the time in milliseconds.
    /// </param>
    /// <returns>
    /// The sample rows number thats row time is as near as possible
    /// to the given time in ms.
    /// </returns>
    private int GetSynchronizedSampleCount(long timeInMS)
    {
      // Skip if there is no data
      if (this.replayTable == null)
      {
        return 0;
      }

      // Get number of sample data rows.
      var rowsCount = this.replayTable.Rows.Count;

      if (rowsCount == 0)
      {
        return 0;
      }

      var estimatedTimeInSamples = (int)(timeInMS / (1000f / Document.ActiveDocument.ExperimentSettings.GazeSamplingRate));

      // Paranoia check 
      if (estimatedTimeInSamples >= rowsCount || estimatedTimeInSamples < 0)
      {
        estimatedTimeInSamples = rowsCount - 1;
      }

      // Get current DataRow
      var row = this.replayTable.Rows[estimatedTimeInSamples];

      // measure times
      var rowTime = Convert.ToInt64(row[3]) - this.currentLoopState.TrialStartTimeInMS;

      // catch actual PointTime
      while (rowTime > timeInMS)
      {
        row = this.replayTable.Rows[estimatedTimeInSamples];
        rowTime = Convert.ToInt64(row[3]) - this.currentLoopState.TrialStartTimeInMS;
        if (estimatedTimeInSamples > 0)
        {
          estimatedTimeInSamples--;
        }
        else
        {
          break;
        }
      }

      while (rowTime < timeInMS)
      {
        row = this.replayTable.Rows[estimatedTimeInSamples];
        rowTime = Convert.ToInt64(row[3]) - this.currentLoopState.TrialStartTimeInMS;
        if (estimatedTimeInSamples < rowsCount - 1)
        {
          estimatedTimeInSamples++;
        }
        else
        {
          break;
        }
      }

      return estimatedTimeInSamples + 1;
    }

    /// <summary>
    /// Checks for valid data resp. blinks
    /// </summary>
    /// <param name="row">
    /// current sample row
    /// </param>
    /// <param name="toDraw">
    /// gaze or mouse
    /// </param>
    /// <param name="newSamplePoint">
    /// Out. Has new sampling point if valid, otherwise null.
    /// </param>
    /// <returns>
    /// True if sample is valid.
    /// </returns>
    private bool CheckSamples(DataRow row, SampleType toDraw, out PointF? newSamplePoint)
    {
      PointF? newPt = null;
      var isValidData = SampleValidity.None;

      switch (toDraw)
      {
        case SampleType.Gaze:
          isValidData = Queries.GetGazeData(row, this.PresentationSize, out newPt);
          break;
        case SampleType.Mouse:
          isValidData = Queries.GetMouseData(row, this.PresentationSize, out newPt);
          break;
      }

      newSamplePoint = newPt;

      this.currentLoopState.IsBlink = false;

      switch (isValidData)
      {
        case SampleValidity.None:
          return false;
        case SampleValidity.Valid:
          return true;
        case SampleValidity.Empty:
          if (toDraw == SampleType.Gaze)
          {
            this.currentLoopState.IsBlink = true;
          }

          return true;
        case SampleValidity.Null:
          return false;
        case SampleValidity.OutOfStimulus:
          return false;
      }

      return false;
    }

    #endregion //METHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER

    /// <summary>
    /// This method changes the first point of the <see cref="VGLine"/>
    /// object that holds the current moving point.
    /// </summary>
    /// <param name="line">
    /// The <see cref="VGLine"/> with the line to modify
    /// </param>
    /// <param name="currPt">
    /// The <see cref="PointF"/> with the new first point.
    /// </param>
    /// <param name="time">
    /// The <see cref="Int64"/> with the points time.
    /// </param>
    private void UpdateFirstPtInLine(VGLine line, PointF currPt, long time)
    {
      line.FirstPoint = currPt;
      line.OnsetTime = time;
    }

    /// <summary>
    /// Moves coordinates of last point in line to new position.
    /// If this line has no first point, first add this.
    /// </summary>
    /// <param name="line">
    /// A <see cref="VGLine"/> to modify
    /// </param>
    /// <param name="currPt">
    /// The <see cref="PointF"/> with the new last point.
    /// </param>
    /// <param name="time">
    /// The <see cref="Int64"/> with the points time.
    /// </param>
    private void UpdateLastPtInLine(VGLine line, PointF currPt, long time)
    {
      if (line.FirstPoint.IsEmpty)
      {
        line.FirstPoint = currPt;
        line.OnsetTime = time;
      }
      else
      {
        line.SecondPoint = currPt;
        line.EndTime = time;
      }
    }

    /// <summary>
    /// Adds new point to given polyline, and truncates to <see cref="maxLengthPath"/>
    /// if DiscreteLength flag is set.
    /// </summary>
    /// <param name="polyline">
    /// Polyline to modify
    /// </param>
    /// <param name="currPt">
    /// new Position
    /// </param>
    /// <param name="discreteLength">
    /// <strong>True</strong>, if path should be truncated,
    /// otherwise <strong>false</strong>.
    /// </param>
    /// <param name="time">
    /// The <see cref="Int64"/> with the points time.
    /// </param>
    private void AddPtToPolyline(VGPolyline polyline, PointF currPt, bool discreteLength, long time)
    {
      // Add Point to Polyline
      polyline.AddPt(currPt);
      polyline.EndTime = time;

      // Switch to fixed length if choosen in UI
      if (discreteLength && (polyline.GetPointCount() > this.maxLengthPath))
      {
        polyline.RemoveFirstPt();
      }
    }

    /// <summary>
    /// Adds new points to given polyline, and truncates to <see cref="maxLengthPath"/>
    /// if DiscreteLength flag is set.
    /// </summary>
    /// <param name="polyline">
    /// Polyline to modify
    /// </param>
    /// <param name="validSamples">
    /// A <see cref="List{PointF}"/> with the new samples
    /// to be added to the <see cref="VGPolyline"/>.
    /// </param>
    /// <param name="discreteLength">
    /// <strong>True</strong>, if path should be truncated,
    /// otherwise <strong>false</strong>.
    /// </param>
    /// <param name="time">
    /// The <see cref="Int64"/> with the last points time.
    /// </param>
    private void AddPtsToPolyline(VGPolyline polyline, List<PointF> validSamples, bool discreteLength, long time)
    {
      // Add Point to Polyline
      polyline.AddPts(validSamples);
      polyline.EndTime = time;

      // Switch to fixed length if choosen in UI
      if (discreteLength && (polyline.GetPointCount() > this.maxLengthPath))
      {
        polyline.RemoveFirstPts(polyline.GetPointCount() - this.maxLengthPath);
      }
    }

    #endregion //HELPER
  }
}
