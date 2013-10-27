using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms.VisualStyles;
using System.Windows.Forms.Design;

namespace OgamaControls
{
  /// <summary>
  /// A range slider control with a caret.
  /// Inherits <see cref="ToolStripItem"/>.
  /// Uses the <see cref="TrackBarRenderer"/> to draw the items.
  /// </summary>
  [ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.All)]
  [ToolboxBitmap(typeof(TrackBar))]
  public class TimeLine : ToolStripItem
  {
    ///////////////////////////////////////////////////////////////////////////////
    // Defining Constants                                                        //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTANTS

    /// <summary>
    /// A static <see cref="Font"/> for the time values.
    /// </summary>
    private static Font TimeFont = SystemFonts.MenuFont;

    /// <summary>
    /// A static <see cref="Brush"/> for the font for the time values.
    /// </summary>
    private static Brush TimeFontBrush = SystemBrushes.MenuText;

    #endregion //CONSTANTS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Variables, Enumerations, Events                                  //
    ///////////////////////////////////////////////////////////////////////////////
    #region FIELDS

    /// <summary>
    /// A <see cref="Rectangle"/> with the bounds for the sliders track line.
    /// </summary>
    private Rectangle _trackBounds = new Rectangle();

    /// <summary>
    /// A <see cref="Rectangle"/> with the bounds for the start time thumb.
    /// </summary>
    private Rectangle _leftThumbBounds = new Rectangle();
    /// <summary>
    /// A <see cref="Rectangle"/> with the bounds for the caret thumb.
    /// </summary>
    private Rectangle _caretThumbBounds = new Rectangle();
    /// <summary>
    /// A <see cref="Rectangle"/> with the bounds for the end time thumb.
    /// </summary>
    private Rectangle _rightThumbBounds = new Rectangle();

    /// <summary>
    /// A <see cref="Double"/> with the position of the start time in percent of the sliders
    /// <see cref="Duration"/>.
    /// </summary>
    private double _leftThumbPosition = 0f;
    /// <summary>
    /// A <see cref="Double"/> with the position of the caret time in percent of the sliders
    /// <see cref="Duration"/>.
    /// </summary>
    private double _caretThumbPosition = 0.5f;
    /// <summary>
    /// A <see cref="Double"/> with the position of the ending time in percent of the sliders
    /// <see cref="Duration"/>.
    /// </summary>
    private double _rightThumbPosition = 1f;

    /// <summary>
    /// A <see cref="Boolean"/> value, whether the start time thumb is clicked or not.
    /// </summary>
    private bool _leftThumbClicked = false;
    /// <summary>
    /// A <see cref="Boolean"/> value, whether the start time thumb is clicked or not.
    /// </summary>
    private bool _caretThumbClicked = false;
    /// <summary>
    /// A <see cref="Boolean"/> value, whether the start time thumb is clicked or not.
    /// </summary>
    private bool _rightThumbClicked = false;

    /// <summary>
    /// A <see cref="TrackBarThumbState"/> with the current start time thumbs state.
    /// </summary>
    private TrackBarThumbState _leftThumbState = TrackBarThumbState.Normal;
    /// <summary>
    /// A <see cref="TrackBarThumbState"/> with the current caret time thumbs state.
    /// </summary>
    private TrackBarThumbState _caretThumbState = TrackBarThumbState.Normal;
    /// <summary>
    /// A <see cref="TrackBarThumbState"/> with the current ending time thumbs state.
    /// </summary>
    private TrackBarThumbState _rightThumbState = TrackBarThumbState.Normal;

    /// <summary>
    /// A <see cref="int"/> which stores an half of the thumbs width.
    /// </summary>
    private int _halfThumbWidth;

    /// <summary>
    /// A <see cref="SizeF"/> with the measured text size of "00:00:000".
    /// </summary>
    private SizeF _timeTextSize;

    /// <summary>
    /// A <see cref="int"/> with the duration of this slider.
    /// That is its maximum value and the reference for the thumbs.
    /// </summary>
    private int _duration;

    /// <summary>
    /// A <see cref="ToolTip"/> to show the start time.
    /// </summary>
    private ToolTip _toolTipStartTime;

    /// <summary>
    /// A <see cref="ToolTip"/> to show the ending time.
    /// </summary>
    private ToolTip _toolTipEndTime;

    /// <summary>
    /// A flag whether to show or hide a caret at the <see cref="CurrentTime"/>
    /// position.
    /// </summary>
    private Boolean _showCaret;

    /// <summary>
    /// A flag indicating whether to show or hide the time values.
    /// </summary>
    private Boolean _showTimes;

    /// <summary>
    /// Saves the images that visualize the referring events on the timeline.
    /// </summary>
    private ImageList eventImages;

    /// <summary>
    /// Indicates a moving caret.
    /// </summary>
    private bool caretIsMoving = false;

    /// <summary>
    /// Saves a time span that should be highlighted.
    /// </summary>
    private TimeLineRange highlightedTimeRange;

    /// <summary>
    /// Holds a list of events on the current timeline.
    /// </summary>
    private TimeLineEventCollection timeLineEvents;

    /// <summary>
    /// Contains the collection of markers on the current timeline.
    /// </summary>
    private TimeLineMarkerCollection timeLineMarkers;

    /// <summary>
    /// Saves the old time of the marker beeing moved.
    /// </summary>
    private int movingMarkerEventID;

    /// <summary>
    /// The list of image keys indicating which events should not be drawn.
    /// </summary>
    private List<string> eventFilterList;

    /// <summary>
    /// An event that is raised when the carets position has changed.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Category("Position"), Description("Occurs when sliders caret position changes.")]
    public event PositionValueChangedEventHandler CaretValueChanged;

    /// <summary>
    /// An event that is raised when the sections start position has changed.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Category("Position"), Description("Occurs when sliders section start position changes.")]
    public event PositionValueChangedEventHandler SectionStartValueChanged;

    /// <summary>
    /// An event that is raised when the sections end position has changed.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Category("Position"), Description("Occurs when sliders section end position changes.")]
    public event PositionValueChangedEventHandler SectionEndValueChanged;

    /// <summary>
    /// 
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Category("Position"), Description("Occurs when sliders caret starts moving via mouse down event.")]
    public event EventHandler CaretMovingStarted;

    /// <summary>
    /// 
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Category("Position"), Description("Occurs when sliders caret stops moving via mouse up event.")]
    public event EventHandler CaretMovingFinished;

    /// <summary>
    /// An event that is raised when the markers position has changed.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Category("Position"), Description("Occurs when marker position changes.")]
    public event MarkerPositionChangedEventHandler MarkerPositionChanged;

    /// <summary>
    /// An event that is raised when a marker is deleted by dragging it away from the controls bounds.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Category("Position"), Description("Occurs when marker is deleted.")]
    public event MarkerPositionChangedEventHandler MarkerDeleted;

    /// <summary>
    /// Delegate declaration of PositionValueChanged event
    /// </summary>
    /// <param name="sender">sender of PositionValueChanged event</param>
    /// <param name="e">event arguments</param>
    public delegate void PositionValueChangedEventHandler(object sender, PositionValueChangedEventArguments e);

    /// <summary>
    /// Delegate declaration of MarkerPositionChanged event
    /// </summary>
    /// <param name="sender">sender of MarkerPositionChanged event</param>
    /// <param name="e">event arguments</param>
    public delegate void MarkerPositionChangedEventHandler(object sender, MarkerPositionChangedEventArguments e);

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets or sets an highlighted TimeRange
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public TimeLineRange HighlightedTimeLineRange
    {
      get { return this.highlightedTimeRange; }
      set { this.highlightedTimeRange = value; }
    }

    /// <summary>
    /// Gets or sets events on the timeline.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public TimeLineEventCollection TimeLineEvents
    {
      get { return this.timeLineEvents; }
      set
      {
        //if (this.eventFilterList.Contains(")
        //{

        //}
        this.timeLineEvents = value;
        this.timeLineEvents.Sort();
      }
    }

    /// <summary>
    /// Gets or sets markers on the timeline.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public TimeLineMarkerCollection TimeLineMarkers
    {
      get { return this.timeLineMarkers; }
      set
      {
        this.timeLineMarkers = value;
        this.timeLineMarkers.Sort();
      }
    }

    /// <summary>
    /// Gets or sets the image key filter on the timeline.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public List<string> EventFilterList
    {
      get { return this.eventFilterList; }
      set { this.eventFilterList = value; }
    }

    /// <summary>
    /// Gets or sets the event visualization images.
    /// </summary>
    /// <value>An <see cref="ImageList"/> with the images to be used
    /// in the time line event visualization.</value>
    [Category("Appearance")]
    [Description("ImageList to be used in the time line event visualization.")]
    public ImageList EventImages
    {
      get { return this.eventImages; }
      set { this.eventImages = value; }
    }

    /// <summary>
    /// Gets or sets the sliders maximum value.
    /// Should be given in milliseconds.
    /// </summary>
    /// <value>A <see cref="int"/> with the duration of this slider.
    /// That is its maximum value and the reference for the thumbs.</value>
    [Category("Positions")]
    [DefaultValue(100)]
    [Description("Maximal value of slider in milliseconds")]
    public int Duration
    {
      get { return _duration; }
      set
      {
        if (value >= 0)
        {
          _duration = value;
          if (_duration == 0)
          {
            _caretThumbPosition = 0;
          }
        }
      }
    }

    /// <summary>
    /// Gets or sets a flag whether to show or hide the caret with the
    /// track bars current position.
    /// </summary>
    /// <value><strong>True</strong>, if track bar should show a caret at the
    /// <see cref="CurrentTime"/> position.</value>
    [Category("Appearance")]
    [DefaultValue(true)]
    [Description("Show or hide the caret to define specific position on the time line.")]
    public Boolean ShowCaret
    {
      get { return _showCaret; }
      set { _showCaret = value; }
    }

    /// <summary>
    /// Gets or sets a flag indicating whether to show or hide the time values.
    /// </summary>
    /// <value><strong>True</strong>, if track bar should show timing values.</value>
    [Category("Appearance")]
    [DefaultValue(true)]
    [Description("Maximal value of slider in milliseconds")]
    public Boolean ShowTimes
    {
      get { return _showTimes; }
      set { _showTimes = value; }
    }

    /// <summary>
    /// Gets or sets the position of the caret.
    /// Should be given in milliseconds.
    /// </summary>
    /// <value>A <see cref="int"/> with the milliseconds of the current time position.</value>
    /// <exception cref="ArgumentException">Thrown, when CurrentTime is not in the range of the valid section.</exception>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public int CurrentTime
    {
      get { return (int)(_caretThumbPosition * Duration); }
      set
      {
        if (Duration > 0)
        {
          if (value > SectionEndTime)
          {
            value = this.SectionEndTime;
          }

          if (value < SectionStartTime)
          {
            value = this.SectionStartTime;
          }

          _caretThumbPosition = value / (double)Duration;
          _caretThumbBounds.X = GetXCoordinateForPosition(_caretThumbPosition) - _halfThumbWidth;

          // If forcing a refresh the UI freezes when there are too many 
          // events displayed in the timeline.
          // if not forcing refresh the timeline does not get updated.
          VectorGraphics.Tools.AsyncHelper.FireAsync(new MethodInvoker(this.RefreshParentThreadSafe));
          //this.Invalidate();

          //this.Parent.Refresh();//Invalidate();
        }
      }
    }

    private void RefreshParentThreadSafe()
    {
      if (this.Parent.InvokeRequired)
      {
        this.Parent.BeginInvoke(new MethodInvoker(this.RefreshParentThreadSafe));
      }
      else
      {
        this.Parent.Refresh();
      }
    }

    /// <summary>
    /// Gets or sets the start time of the section to play in milliseconds.
    /// </summary>
    /// <value>A <see cref="int"/> with the milliseconds of the start time of the section.</value>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public int SectionStartTime
    {
      get { return (int)(_leftThumbPosition * Duration); }
      set
      {
        if (Duration > 0)
        {
          _leftThumbPosition = value / (double)Duration;
          _leftThumbBounds.X = GetXCoordinateForPosition(_leftThumbPosition) - _halfThumbWidth;
          this.Invalidate();
        }
      }
    }

    /// <summary>
    /// Gets or sets the ending time of the section to play in milliseconds.
    /// </summary>
    /// <value>A <see cref="int"/> with the milliseconds of the ending time of the section.</value>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public int SectionEndTime
    {
      get { return (int)(_rightThumbPosition * Duration); }
      set
      {
        if (Duration > 0)
        {
          _rightThumbPosition = value / (double)Duration;
          _rightThumbBounds.X = GetXCoordinateForPosition(_rightThumbPosition) - _halfThumbWidth;
          this.Invalidate();
        }
      }
    }


    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Constructor. Initializes tooltips and slider.
    /// </summary>
    public TimeLine()
    {
      _showCaret = true;
      _showTimes = true;
      // Create the ToolTips for the Start and End time.
      _toolTipStartTime = new ToolTip();
      _toolTipEndTime = new ToolTip();
      this.eventImages = new ImageList();
      this.timeLineEvents = new TimeLineEventCollection();
      this.timeLineMarkers = new TimeLineMarkerCollection();
      this.eventFilterList = new List<string>();
      this.highlightedTimeRange = new TimeLineRange(0, 0);
    }

    /// <summary>
    /// Calculate the sizes of the bar and thumbs rectangles.
    /// </summary>
    public void SetupTrackBar()
    {
      if (this.Owner != null)
      {
        using (Graphics g = this.Owner.CreateGraphics())
        {
          if (TrackBarRenderer.IsSupported)
          {
            // Calculate the size of the thumbs.
            _leftThumbBounds.Size =
                TrackBarRenderer.GetTopPointingThumbSize(g,
                TrackBarThumbState.Normal);

            _rightThumbBounds.Size =
                TrackBarRenderer.GetTopPointingThumbSize(g,
                TrackBarThumbState.Normal);

            _caretThumbBounds.Size =
                TrackBarRenderer.GetTopPointingThumbSize(g,
                TrackBarThumbState.Normal);
          }
          else
          {
            // Calculate the size of the thumbs.
            _leftThumbBounds.Size = new Size(10, this.Height);
            _rightThumbBounds.Size = new Size(10, this.Height);
            _caretThumbBounds.Size = new Size(10, this.Height);
          }

          if (_showTimes)
          {
            _timeTextSize = g.MeasureString("00:00:000", TimeFont);
          }
          else
          {
            _timeTextSize = new SizeF(5, 20);
          }
        }

        _halfThumbWidth = _leftThumbBounds.Width / 2;

        // Calculate the size of the track bar.
        _trackBounds.X = this.ContentRectangle.X + _halfThumbWidth +
          this.Padding.Left + (int)_timeTextSize.Width;
        _trackBounds.Y = _leftThumbBounds.Height / 2;
        _trackBounds.Width = this.ContentRectangle.Width - _rightThumbBounds.Width -
          this.Padding.Left - this.Padding.Right - 2 * (int)_timeTextSize.Width;
        _trackBounds.Height = 4;

        _leftThumbBounds.X = GetXCoordinateForPosition(_leftThumbPosition) - _halfThumbWidth;
        _rightThumbBounds.X = GetXCoordinateForPosition(_rightThumbPosition) - _halfThumbWidth;
        _caretThumbBounds.X = GetXCoordinateForPosition(_caretThumbPosition) - _halfThumbWidth;

        _leftThumbBounds.Y = this.Padding.Top;// +(int)_timeTextSize.Height;
        _rightThumbBounds.Y = this.Padding.Top;// +(int)_timeTextSize.Height;
        _caretThumbBounds.Y = this.Padding.Top;// +(int)_timeTextSize.Height;
      }
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
    /// The protected OnCaretValueChanged method raises the 
    /// <see cref="CaretValueChanged"/>event by invoking the delegates
    /// </summary>
    /// <param name="e">A <see cref="PositionValueChangedEventArguments"/> with the new time.</param>
    protected void OnCaretValueChanged(PositionValueChangedEventArguments e)
    {
      if (CaretValueChanged != null)
      {
        // Invokes the delegates. 
        CaretValueChanged(this, e);
      }
    }

    /// <summary>
    /// The protected OnCaretMovingStarted method raises the 
    /// <see cref="CaretMovingStarted"/>event by invoking the delegates
    /// </summary>
    /// <param name="e">An empty<see cref="EventArgs"/>.</param>
    protected void OnCaretMovingStarted(EventArgs e)
    {
      if (CaretMovingStarted != null)
      {
        // Invokes the delegates. 
        CaretMovingStarted(this, e);
      }
    }

    /// <summary>
    /// The protected OnCaretMovingFinished method raises the 
    /// <see cref="CaretMovingFinished"/>event by invoking the delegates
    /// </summary>
    /// <param name="e">An empty<see cref="EventArgs"/>.</param>
    protected void OnCaretMovingFinished(EventArgs e)
    {
      if (CaretMovingFinished != null)
      {
        // Invokes the delegates. 
        CaretMovingFinished(this, e);
      }
    }


    /// <summary>
    /// The protected OnSectionStartValueChanged method raises the 
    /// <see cref="SectionStartValueChanged"/>event by invoking the delegates
    /// </summary>
    /// <param name="e">A <see cref="PositionValueChangedEventArguments"/> with the new time
    /// of the sections start.</param>
    protected void OnSectionStartValueChanged(PositionValueChangedEventArguments e)
    {
      if (SectionStartValueChanged != null)
      {
        // Invokes the delegates. 
        SectionStartValueChanged(this, e);
      }
    }

    /// <summary>
    /// The protected OnSectionEndValueChanged method raises the 
    /// <see cref="SectionEndValueChanged"/>event by invoking the delegates
    /// </summary>
    /// <param name="e">A <see cref="PositionValueChangedEventArguments"/> with the new time
    /// of the sections end.</param>
    protected void OnSectionEndValueChanged(PositionValueChangedEventArguments e)
    {
      if (SectionEndValueChanged != null)
      {
        // Invokes the delegates. 
        SectionEndValueChanged(this, e);
      }
    }

    /// <summary>
    /// The protected OnMarkerPositionChanged method raises the 
    /// <see cref="MarkerPositionChanged"/>event by invoking the delegates
    /// </summary>
    /// <param name="e">A <see cref="MarkerPositionChangedEventArguments"/> with the old and new 
    /// marker times.</param>
    protected void OnMarkerPositionChanged(MarkerPositionChangedEventArguments e)
    {
      if (MarkerPositionChanged != null)
      {
        // Invokes the delegates. 
        MarkerPositionChanged(this, e);
      }
    }

    /// <summary>
    /// The protected OnMarkerDeleted method raises the 
    /// <see cref="MarkerDeleted"/> event by invoking the delegates
    /// </summary>
    /// <param name="e">A <see cref="MarkerPositionChangedEventArguments"/> with the old 
    /// marker time.</param>
    protected void OnMarkerDeleted(MarkerPositionChangedEventArguments e)
    {
      if (MarkerDeleted != null)
      {
        // Invokes the delegates. 
        MarkerDeleted(this, e);
      }
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
    /// 
    /// </summary>
    protected override Size DefaultSize
    {
      get
      {
        return new Size(200, base.DefaultSize.Height);
      }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="constrainingSize"></param>
    /// <returns></returns>
    public override Size GetPreferredSize(Size constrainingSize)
    {
      // Use the default size if the text box is on the overflow menu
      // or is on a vertical ToolStrip.
      if (IsOnOverflow || Owner.Orientation == Orientation.Vertical)
      {
        return DefaultSize;
      }

      // Declare a variable to store the total available width as 
      // it is calculated, starting with the display width of the 
      // owning ToolStrip.
      int width = Owner.DisplayRectangle.Width;

      // Subtract the width of the overflow button if it is displayed. 
      if (Owner.OverflowButton.Visible)
      {
        width = width - Owner.OverflowButton.Width -
            Owner.OverflowButton.Margin.Horizontal;
      }

      // Declare a variable to maintain a count of ToolStripSpringTextBox 
      // items currently displayed in the owning ToolStrip. 
      int timeLineCount = 0;

      foreach (ToolStripItem item in Owner.Items)
      {
        // Ignore items on the overflow menu.
        if (item.IsOnOverflow) continue;

        if (item is TimeLine)
        {
          // For TimeLine items, increment the count and 
          // subtract the margin width from the total available width.
          timeLineCount++;
          width -= item.Margin.Horizontal;
        }
        else
        {
          // For all other items, subtract the full width from the total
          // available width.
          width = width - item.Width - item.Margin.Horizontal;
        }
      }

      // If there are multiple ToolStripSpringTextBox items in the owning
      // ToolStrip, divide the total available width between them. 
      if (timeLineCount > 1) width /= timeLineCount;

      // If the available width is less than the default width, use the
      // default width, forcing one or more items onto the overflow menu.
      if (width < DefaultSize.Width) width = DefaultSize.Width;

      // Retrieve the preferred size from the base class, but change the
      // width to the calculated width. 
      Size size = base.GetPreferredSize(constrainingSize);
      size.Width = width;
      return size;
    }

    /// <summary>
    /// 
    /// </summary>
    protected override void OnBoundsChanged()
    {
      base.OnBoundsChanged();
      SetupTrackBar();
    }

    /// <summary>
    /// Overriden. Occurs when the control is redrawn. 
    /// Draws the whole track bar and its thumbs, along with the timing.
    /// If section start and end thumbs are hot or pressed, show tooltip
    /// with timing information.
    /// </summary>
    /// <param name="e">A <see cref="PaintEventArgs"/> that contains the event data. </param>
    protected override void OnPaint(PaintEventArgs e)
    {
      base.OnPaint(e);

      if (this.Owner != null)
      {
        // Draw the background. This includes drawing a highlighted 
        // border when the mouse is in the client area.
        ToolStripItemRenderEventArgs ea = new ToolStripItemRenderEventArgs(
             e.Graphics,
             this);
        this.Owner.Renderer.DrawItemBackground(ea);


        //Draws the interior of the trackline in green, if it is in the section between start and end thumb.
        Rectangle fillRectangle = _trackBounds;
        fillRectangle.Inflate(-1, -1);
        fillRectangle.X = GetXCoordinateForPosition(_leftThumbPosition);
        fillRectangle.Width = GetXCoordinateForPosition(_rightThumbPosition) - fillRectangle.X;

        //Visual Styles have to be enabled.
        if (!TrackBarRenderer.IsSupported)
        {
          _trackBounds.Y -= 2;
          e.Graphics.DrawRectangle(Pens.Black, _trackBounds);
          _trackBounds.Y += 2;

          fillRectangle.Y -= 2;
          fillRectangle.Height += 1;
          e.Graphics.FillRectangle(Brushes.Green, fillRectangle);
        }
        else
        {
          //Draws the track line.
          TrackBarRenderer.DrawHorizontalTrack(e.Graphics, _trackBounds);
          e.Graphics.FillRectangle(Brushes.Green, fillRectangle);
        }

        if (_duration > 0)
        {

          // Draw highlighted rectangle
          if (this.highlightedTimeRange.IsSet)
          {
            Rectangle highlightRect = fillRectangle;
            double positionStartTime = this.highlightedTimeRange.StartTime / (double)Duration;
            double positionEndTime = this.highlightedTimeRange.EndTime / (double)Duration;
            highlightRect.X = GetXCoordinateForPosition(positionStartTime);
            highlightRect.Width = GetXCoordinateForPosition(positionEndTime) - highlightRect.X;
            //highlightRect.Inflate(0, 2);
            e.Graphics.FillRectangle(new SolidBrush(Color.Yellow), highlightRect);
          }

          // Draw event items
          foreach (TimeLineEvent timeEvent in this.timeLineEvents)
          {
            // Skip filtered keys (special types of events)
            if (this.eventFilterList.Contains(timeEvent.ImageKey))
            {
              continue;
            }

            double position = timeEvent.Time / (double)Duration;
            int xPosition = GetXCoordinateForPosition(position);
            Rectangle timeEventBounds = new Rectangle((int)(xPosition - timeEvent.StrokeWidth / 2),
              _trackBounds.Y - 4,
              (int)timeEvent.StrokeWidth, 10);
            e.Graphics.FillRectangle(new SolidBrush(timeEvent.StrokeColor), timeEventBounds);
            Image icon = this.eventImages.Images[timeEvent.ImageKey];
            Rectangle timeEventImageLocation = new Rectangle((int)(xPosition - icon.Width / 2f),
              _trackBounds.Y,
              icon.Width, icon.Height);
            switch (timeEvent.Position)
            {
              case TimeLinePosition.None:
                break;
              case TimeLinePosition.Above:
                timeEventImageLocation.Y -= icon.Height + 2;
                break;
              case TimeLinePosition.Center:
                break;
              case TimeLinePosition.Below:
                timeEventImageLocation.Y += _trackBounds.Height;
                break;
            }

            e.Graphics.DrawImage(icon, timeEventImageLocation);
          }

          // Draw timeline marker
          foreach (TimeLineMarker marker in this.timeLineMarkers)
          {
            double position = marker.Time / (double)Duration;
            int xPosition = GetXCoordinateForPosition(position);
            marker.Draw(e.Graphics, xPosition, this.Height);
          }
        }

        if (TrackBarRenderer.IsSupported)
        {

          //Draw the thumbs.
          TrackBarRenderer.DrawHorizontalThumb(e.Graphics,
              _leftThumbBounds, _leftThumbState);
          TrackBarRenderer.DrawHorizontalThumb(e.Graphics,
              _rightThumbBounds, _rightThumbState);
          if (_showCaret)
          {
            TrackBarRenderer.DrawTopPointingThumb(e.Graphics,
                _caretThumbBounds, _caretThumbState);
          }
        }
        else
        {

          e.Graphics.FillRectangle(Brushes.Green, _leftThumbBounds);
          e.Graphics.DrawRectangle(Pens.DarkGray, _leftThumbBounds);
          e.Graphics.FillRectangle(Brushes.Green, _rightThumbBounds);
          e.Graphics.DrawRectangle(Pens.DarkGray, _rightThumbBounds);
          if (_showCaret)
          {
            e.Graphics.FillEllipse(Brushes.Red, _caretThumbBounds);
            e.Graphics.DrawEllipse(Pens.DarkGray, _caretThumbBounds);
          }
        }
        //If track bar is in caret mode,
        //show tooltip with the section start time above the center of the left thumb
        //if the mouse is over it.
        if (_showCaret && _showTimes)
        {
          if (_leftThumbState == TrackBarThumbState.Hot || _leftThumbState == TrackBarThumbState.Pressed)
          {
            _toolTipStartTime.Show(ConvertToSeconds(this.SectionStartTime), this.Parent,
              GetXCoordinateForPosition(_leftThumbPosition) - (int)(_timeTextSize.Width / 2),
              (int)(_leftThumbBounds.Top - _timeTextSize.Height - 2));
          }
          else
          {
            _toolTipStartTime.Hide(this.Parent);
          }
        }

        //If track bar is in caret mode,
        //show tooltips with the section end time above the center of the right thumb
        //if the mouse is over it.
        if (_showCaret && _showTimes)
        {
          if (_rightThumbState == TrackBarThumbState.Hot || _rightThumbState == TrackBarThumbState.Pressed)
          {
            _toolTipEndTime.Show(ConvertToSeconds(this.SectionEndTime), this.Parent,
              GetXCoordinateForPosition(_rightThumbPosition) - (int)(_timeTextSize.Width / 2),
              (int)(_leftThumbBounds.Top - _timeTextSize.Height - 2));
          }
          else
          {
            _toolTipEndTime.Hide(this.Parent);
          }
        }


        if (_showTimes)
        {
          //Depending on caret drawing mode show either the track bars duration value
          //at the right of the control or the section end time value.
          int rightTime = 0;
          if (_showCaret)
          {
            //Use the Duration time string at the right of the control.
            rightTime = this.Duration;
          }
          else
          {
            //Use the section end time string at the right of the control.
            rightTime = this.SectionEndTime;
          }

          //Draw the string at the right of the control.
          e.Graphics.DrawString(ConvertToSeconds(rightTime), TimeLine.TimeFont,
            TimeLine.TimeFontBrush,
            this.Padding.Left + (int)_timeTextSize.Width + _trackBounds.Width + _leftThumbBounds.Width,
            this.Padding.Top + _leftThumbBounds.Height / 2 -
            _timeTextSize.Height / 2);//+ (int)_timeTextSize.Height);

          //Depending on caret drawing mode show either the carets time value
          //at the left of the control or the section start time value.
          int leftTime = 0;
          if (_showCaret)
          {
            //Use the current time string at the left of the control.
            leftTime = this.CurrentTime;
          }
          else
          {
            //Use the section start time string at the left of the control.
            leftTime = this.SectionStartTime;
          }

          //Draw the string at the left of the control.
          e.Graphics.DrawString(ConvertToSeconds(leftTime), TimeLine.TimeFont,
            TimeLine.TimeFontBrush,
            this.Padding.Left,
            this.Padding.Top + _leftThumbBounds.Height / 2 -
            _timeTextSize.Height / 2);// + (int)_timeTextSize.Height);
        }
      }
    }

    /// <summary>
    /// Overriden. Raised when mouse down occured.
    /// Determine whether the user has clicked a track bar thumb.
    /// </summary>
    /// <param name="e">A <see cref="MouseEventArgs"/> that contains the event data. </param>
    protected override void OnMouseDown(MouseEventArgs e)
    {
      //if (!TrackBarRenderer.IsSupported)
      //  return;
      if (_duration != 0)
      {
        if (this._caretThumbBounds.Contains(e.Location) && _showCaret)
        {
          _caretThumbClicked = true;
          _caretThumbState = TrackBarThumbState.Pressed;
        }
        else if (this._leftThumbBounds.Contains(e.Location))
        {
          _leftThumbClicked = true;
          _leftThumbState = TrackBarThumbState.Pressed;
        }
        else if (this._rightThumbBounds.Contains(e.Location))
        {
          _rightThumbClicked = true;
          _rightThumbState = TrackBarThumbState.Pressed;
        }

        foreach (TimeLineMarker marker in this.timeLineMarkers)
        {
          Rectangle bounds = new Rectangle(
            GetXCoordinateForPosition(marker.Time / (double)this.Duration) - TimeLineMarker.MARKERWIDTH / 2,
            0,
            TimeLineMarker.MARKERWIDTH,
            this.Height);

          if (bounds.Contains(e.Location))
          {
            marker.State = TrackBarThumbState.Pressed;
            this.movingMarkerEventID = marker.EventID;
          }
        }

        this.Invalidate();
      }
    }

    /// <summary>
    /// Overriden. Raised when mouse up occured.
    /// Redraw the track bar thumb if the user has moved it.
    /// </summary>
    /// <param name="e">A <see cref="MouseEventArgs"/> that contains the event data. </param>
    protected override void OnMouseUp(MouseEventArgs e)
    {
      //if (!TrackBarRenderer.IsSupported)
      //  return;

      if (_duration != 0)
      {
        if (_leftThumbClicked == true)
        {
          //if (e.Location.X > _trackBounds.X &&
          //    e.Location.X < (_trackBounds.X +
          //    _trackBounds.Width - _leftThumbBounds.Width))
          //{
          //  _leftThumbClicked = false;
          //  _leftThumbState = TrackBarThumbState.Hot;
          //  this.Invalidate();
          //}

          _leftThumbClicked = false;
        }
        else if (_rightThumbClicked == true)
        {
          //if (e.Location.X > _trackBounds.X &&
          //    e.Location.X < (_trackBounds.X +
          //    _trackBounds.Width - _leftThumbBounds.Width))
          //{
          //  _rightThumbClicked = false;
          //  _rightThumbState = TrackBarThumbState.Hot;
          //  this.Invalidate();
          //}

          _rightThumbClicked = false;
        }
        else if (_caretThumbClicked == true)
        {
          //if (e.Location.X > _trackBounds.X &&
          //    e.Location.X < (_trackBounds.X +
          //    _trackBounds.Width - _leftThumbBounds.Width))
          //{
          //  _caretThumbClicked = false;
          //  _caretThumbState = TrackBarThumbState.Hot;
          //  this.Invalidate();
          //}

          _caretThumbClicked = false;
        }

        foreach (TimeLineMarker marker in this.timeLineMarkers)
        {
          if (marker.State == TrackBarThumbState.Pressed)
          {
            //if (e.Location.X > _trackBounds.X &&
            //    e.Location.X < (_trackBounds.X +
            //    _trackBounds.Width - _leftThumbBounds.Width))
            //{
            //  _caretThumbClicked = false;
            //  _caretThumbState = TrackBarThumbState.Hot;
            //  this.Invalidate();
            //}
            marker.State = TrackBarThumbState.Normal;
            this.OnMarkerPositionChanged(new MarkerPositionChangedEventArguments(this.movingMarkerEventID, marker.Time));
          }
        }
      }

      if (caretIsMoving)
      {
        OnCaretMovingFinished(EventArgs.Empty);
        caretIsMoving = false;
      }
    }

    /// <summary>
    /// Overriden. Raised when mouse move occured.
    /// Track cursor movements.
    /// </summary>
    /// <param name="e">A <see cref="MouseEventArgs"/> that contains the event data. </param>
    protected override void OnMouseMove(MouseEventArgs e)
    {
      //if (!TrackBarRenderer.IsSupported)
      //  return;

      bool caretMoved = false;

      if (_duration != 0)
      {
        // The user is moving the thumb.
        if (_leftThumbClicked == true)
        {
          if (e.X >= _halfThumbWidth + this.Padding.Left + (int)_timeTextSize.Width &&
            e.X < GetXCoordinateForPosition(_rightThumbPosition))
          {
            _leftThumbPosition = (e.X - _halfThumbWidth -
              this.Padding.Left - (int)_timeTextSize.Width) / (double)_trackBounds.Width;
            _leftThumbBounds.X = GetXCoordinateForPosition(_leftThumbPosition) - _halfThumbWidth;
            OnSectionStartValueChanged(new PositionValueChangedEventArguments(this.SectionStartTime));
          }
          //if caret would be out of bounds reposition caret.
          if (e.X >= GetXCoordinateForPosition(_caretThumbPosition)
            && e.X <= GetXCoordinateForPosition(_rightThumbPosition))
          {
            _caretThumbPosition = (e.X - _halfThumbWidth -
              this.Padding.Left - (int)_timeTextSize.Width) / (double)_trackBounds.Width;
            _caretThumbBounds.X = GetXCoordinateForPosition(_caretThumbPosition) - _halfThumbWidth;
            caretMoved = true;
            OnCaretValueChanged(new PositionValueChangedEventArguments(this.CurrentTime));
          }

          this.Parent.Refresh();
          return;
        }
        // The cursor is passing over the track.
        else
        {
          if (_leftThumbBounds.Contains(e.Location))
          {
            _leftThumbState = TrackBarThumbState.Hot;
            this.Invalidate();
            return;
          }
          else
          {
            _leftThumbState = TrackBarThumbState.Normal;
          }
        }



        // The user is moving the thumb.
        if (_rightThumbClicked == true)
        {
          if (e.X > GetXCoordinateForPosition(_leftThumbPosition) &&
            e.X <= _trackBounds.Width + _halfThumbWidth + this.Padding.Left + (int)_timeTextSize.Width)
          {
            _rightThumbPosition = (e.X - _halfThumbWidth -
              this.Padding.Left - (int)_timeTextSize.Width) / (double)_trackBounds.Width;
            _rightThumbBounds.X = GetXCoordinateForPosition(_rightThumbPosition) - _halfThumbWidth;
            OnSectionEndValueChanged(new PositionValueChangedEventArguments(this.SectionEndTime));
          }
          //if caret would be out of bounds reposition caret.
          if (e.X <= GetXCoordinateForPosition(_caretThumbPosition)
            && e.X >= GetXCoordinateForPosition(_leftThumbPosition))
          {
            _caretThumbPosition = (e.X - _halfThumbWidth -
              this.Padding.Left - (int)_timeTextSize.Width) / (double)_trackBounds.Width;
            _caretThumbBounds.X = GetXCoordinateForPosition(_caretThumbPosition) - _halfThumbWidth;
            caretMoved = true;
            OnCaretValueChanged(new PositionValueChangedEventArguments(this.CurrentTime));
          }

          this.Parent.Refresh();
          return;
        }
        // The cursor is passing over the track.
        else
        {
          if (_rightThumbBounds.Contains(e.Location))
          {
            _rightThumbState = TrackBarThumbState.Hot;
            this.Invalidate();
            return;
          }
          else
          {
            _rightThumbState = TrackBarThumbState.Normal;
          }
        }

        // The user is moving the thumb.
        if (_caretThumbClicked == true)
        {
          if (e.X >= GetXCoordinateForPosition(_leftThumbPosition) &&
            e.X <= GetXCoordinateForPosition(_rightThumbPosition))
          {
            _caretThumbPosition = (e.X - _halfThumbWidth -
              this.Padding.Left - (int)_timeTextSize.Width) / (double)_trackBounds.Width;
            _caretThumbBounds.X = GetXCoordinateForPosition(_caretThumbPosition) - _halfThumbWidth;
            caretMoved = true;
            OnCaretValueChanged(new PositionValueChangedEventArguments(this.CurrentTime));
          }

          this.Parent.Refresh();
          return;
        }
        // The cursor is passing over the track.
        else
        {
          if (_caretThumbBounds.Contains(e.Location))
          {
            _caretThumbState = TrackBarThumbState.Hot;
            this.Invalidate();
            return;
          }
          else
          {
            _caretThumbState = TrackBarThumbState.Normal;
          }
        }

        foreach (TimeLineMarker marker in this.timeLineMarkers)
        {
          // The user is moving the marker.
          if (marker.State == TrackBarThumbState.Pressed)
          {
            if (e.X >= GetXCoordinateForPosition(_leftThumbPosition) &&
              e.X <= GetXCoordinateForPosition(_rightThumbPosition))
            {
              marker.Time = (int)(((e.X - TimeLineMarker.MARKERWIDTH / 2 -
                this.Padding.Left - (int)_timeTextSize.Width) / (double)_trackBounds.Width) * this.Duration);
            }
          }
          // The cursor is passing over the track.
          else
          {
            Rectangle bounds = new Rectangle(
              GetXCoordinateForPosition(marker.Time / (double)Duration) - TimeLineMarker.MARKERWIDTH / 2,
              0,
              TimeLineMarker.MARKERWIDTH,
              this.Height);

            if (bounds.Contains(e.Location))
            {
              marker.State = TrackBarThumbState.Hot;
            }
            else
            {
              marker.State = TrackBarThumbState.Normal;
            }
          }
        }

        if (!caretIsMoving && caretMoved)
        {
          OnCaretMovingStarted(EventArgs.Empty);
          caretIsMoving = true;
        }

        this.Parent.Refresh();
      }
    }

    /// <summary>
    /// Overriden. Raised when mouse pointer leaves the control. 
    /// Reset clicked states.
    /// </summary>
    /// <param name="e">An empty <see cref="EventArgs"/>.</param>
    protected override void OnMouseLeave(EventArgs e)
    {
      base.OnMouseLeave(e);
      _leftThumbClicked = false;
      _rightThumbClicked = false;
      _caretThumbClicked = false;

      // Deselect marker or delete moved marker
      // (moving outside bounds of timeline is treated as deletion.
      TimeLineMarker markerToDelete = null;
      foreach (TimeLineMarker marker in this.timeLineMarkers)
      {
        switch (marker.State)
        {
          case TrackBarThumbState.Disabled:
            break;
          case TrackBarThumbState.Hot:
            marker.State = TrackBarThumbState.Normal;
            break;
          case TrackBarThumbState.Normal:
            break;
          case TrackBarThumbState.Pressed:
            marker.State = TrackBarThumbState.Normal;
            this.OnMarkerDeleted(new MarkerPositionChangedEventArguments(this.movingMarkerEventID, -1));
            markerToDelete = marker;
            break;
          default:
            break;
        }
      }

      if (markerToDelete != null)
      {
        this.timeLineMarkers.Remove(markerToDelete);
      }

      // Notify end of caret moving
      if (caretIsMoving)
      {
        OnCaretMovingFinished(EventArgs.Empty);
        caretIsMoving = false;
      }
    }

    #endregion //OVERRIDES

    ///////////////////////////////////////////////////////////////////////////////
    // Methods for doing main class job                                          //
    ///////////////////////////////////////////////////////////////////////////////
    #region METHODS

    /// <summary>
    /// This method moves the caret slider to the next time line event position,
    /// if there is any in the direction given by the parameter
    /// </summary>
    /// <param name="forward"><strong>True</strong> if the next event in forward direction,
    /// should be choosen, otherwise <strong>false</strong>.</param>
    public void MoveToNextMarker(bool forward)
    {
      this.timeLineMarkers.Sort();
      TimeLineMarker lastMarker = null;
      int newTime = -1;

      if (forward)
      {
        foreach (TimeLineMarker marker in this.timeLineMarkers)
        {
          if (this.CurrentTime >= marker.Time - 5) // 5 is a safety value for rounding errors
          {
            continue;
          }
          newTime = marker.Time;
          break;
        }

        if (newTime == -1)
        {
          newTime = this.SectionEndTime;
        }
      }
      else
      {
        foreach (TimeLineMarker marker in this.timeLineMarkers)
        {
          if (this.CurrentTime > marker.Time)
          {
            lastMarker = marker;
            continue;
          }
          newTime = lastMarker != null ? lastMarker.Time : this.SectionStartTime;
          break;
        }
        if (newTime == -1)
        {
          newTime = lastMarker != null ? lastMarker.Time : this.SectionStartTime;
        }
      }
      this.CurrentTime = newTime;

      // Raise event.
      OnCaretValueChanged(new PositionValueChangedEventArguments(this.CurrentTime));
    }

    /// <summary>
    /// Converts milliseconds value into a string format "00:00:000"
    /// </summary>
    /// <param name="value">An <see cref="int"/> with the time in milliseconds.</param>
    /// <returns>A <see cref="string"/> representation of the time in the format "00:00:000".</returns>
    private string ConvertToSeconds(int value)
    {
      int minutes = (int)(value / 60000f);
      int seconds = (int)(value / 1000f - minutes * 60);
      int milliseconds = value - seconds * 1000 - minutes * 60000;
      string formattedText = minutes.ToString("00") + ":" + seconds.ToString("00") + ":" + milliseconds.ToString("000");
      return formattedText;
    }

    /// <summary>
    /// Sets trackbar position to zero.
    /// </summary>
    public void ResetTimeLine()
    {
      this.CurrentTime = this.SectionStartTime;
    }

    /// <summary>
    /// This method moves the caret slider to the next time line event position,
    /// if there is any in the direction given by the parameter
    /// </summary>
    /// <param name="forward"><strong>True</strong> if the next event in forward direction,
    /// should be choosen, otherwise <strong>false</strong>.</param>
    public void MoveToNextTimeLineEvent(bool forward)
    {
      this.timeLineEvents.Sort();
      TimeLineEvent lastEvent = null;
      int newTime = -1;

      if (forward)
      {
        foreach (TimeLineEvent timeEvent in this.timeLineEvents)
        {
          if (this.CurrentTime >= timeEvent.Time - 5) // 5 is a safety value for rounding errors
          {
            continue;
          }
          newTime = timeEvent.Time;
          break;
        }

        if (newTime == -1)
        {
          newTime = this.SectionEndTime;
        }
      }
      else
      {
        foreach (TimeLineEvent timeEvent in this.timeLineEvents)
        {
          if (this.CurrentTime > timeEvent.Time)
          {
            lastEvent = timeEvent;
            continue;
          }
          newTime = lastEvent != null ? lastEvent.Time : this.SectionStartTime;
          break;
        }
        if (newTime == -1)
        {
          newTime = lastEvent != null ? lastEvent.Time : this.SectionStartTime;
        }
      }
      this.CurrentTime = newTime;

      // Raise event.
      OnCaretValueChanged(new PositionValueChangedEventArguments(this.CurrentTime));
    }

    #endregion //METHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER

    /// <summary>
    /// Returns the current left thumbs X coordinate.
    /// </summary>
    /// <returns>A <see cref="int"/> with the left thumbs X coordinate in pixels relative to the tracks bounds.</returns>
    private int GetXCoordinateForPosition(double position)
    {
      return ((int)(_trackBounds.Width * position) + this.Padding.Left + (int)_timeTextSize.Width + _halfThumbWidth);
    }

    /// <summary>
    /// Class that contains the data for the slider value changed event. Derives from System.EventArgs.
    /// </summary>
    public class PositionValueChangedEventArguments : EventArgs
    {
      ///////////////////////////////////////////////////////////////////////////////
      // Defining Variables, Enumerations, Events                                  //
      ///////////////////////////////////////////////////////////////////////////////
      #region FIELDS
      /// <summary>
      /// string containing current timing position in ms.
      /// </summary>
      private readonly int _currentMillisecond;
      #endregion //FIELDS

      ///////////////////////////////////////////////////////////////////////////////
      // Defining Properties                                                       //
      ///////////////////////////////////////////////////////////////////////////////
      #region PROPERTIES
      /// <summary>
      /// Gets  a string containing current timing position in ms
      /// </summary>
      public int Millisecond
      {
        get { return _currentMillisecond; }
      }
      #endregion //PROPERTIES

      ///////////////////////////////////////////////////////////////////////////////
      // Construction and Initializing methods                                     //
      ///////////////////////////////////////////////////////////////////////////////
      #region CONSTRUCTION
      /// <summary>
      /// Constructor. Initializes fields.
      /// </summary>
      /// <param name="currentMillisecond">Integer containing current timing position in ms.</param>
      public PositionValueChangedEventArguments(int currentMillisecond)
      {
        this._currentMillisecond = currentMillisecond;
      }
      #endregion //CONSTRUCTION
    }

    /// <summary>
    /// Class that contains the data for the marker position changed event. Derives from System.EventArgs.
    /// </summary>
    public class MarkerPositionChangedEventArguments : EventArgs
    {
      ///////////////////////////////////////////////////////////////////////////////
      // Defining Variables, Enumerations, Events                                  //
      ///////////////////////////////////////////////////////////////////////////////
      #region FIELDS

      /// <summary>
      /// An <see cref="Int32"/> containing the markers event ID.
      /// </summary>
      private readonly int markerEventID;

      /// <summary>
      /// An <see cref="Int32"/> containing the new marker time in ms.
      /// </summary>
      private readonly int newTime;

      #endregion //FIELDS

      ///////////////////////////////////////////////////////////////////////////////
      // Defining Properties                                                       //
      ///////////////////////////////////////////////////////////////////////////////
      #region PROPERTIES

      /// <summary>
      /// Gets the <see cref="Int32"/> containing the markers event ID.
      /// </summary>
      public int MarkerEventID
      {
        get { return markerEventID; }
      }

      /// <summary>
      /// Gets the <see cref="Int32"/> containing new timing position in ms of the marker.
      /// </summary>
      public int NewTime
      {
        get { return this.newTime; }
      }

      #endregion //PROPERTIES

      ///////////////////////////////////////////////////////////////////////////////
      // Construction and Initializing methods                                     //
      ///////////////////////////////////////////////////////////////////////////////
      #region CONSTRUCTION

      /// <summary>
      /// Constructor. Initializes fields.
      /// </summary>
      /// <param name="newMarkerEventID">An <see cref="Int32"/> containing the markers event ID.</param>
      /// <param name="newMillisecond">An <see cref="Int32"/> containing new marker timing position in ms.</param>
      public MarkerPositionChangedEventArguments(int newMarkerEventID, int newMillisecond)
      {
        this.markerEventID = newMarkerEventID;
        this.newTime = newMillisecond;
      }

      #endregion //CONSTRUCTION
    }

    #endregion //HELPER
  }
}
