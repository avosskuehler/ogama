using System;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms.VisualStyles;

namespace OgamaControls
{
  /// <summary>
  /// A range slider control with a caret.
  /// Inherits <see cref="Control"/>.
  /// Uses the <see cref="TrackBarRenderer"/> to draw the items.
  /// </summary>
  [ToolboxBitmap(typeof(TrackBar))]
  public class RangeTrackBar : Control
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
    /// Delegate declaration of CaretValueChanged event
    /// </summary>
    /// <param name="sender">sender of CaretValueChanged event</param>
    /// <param name="e">event arguments</param>
    public delegate void PositionValueChangedEventHandler(object sender, PositionValueChangedEventArguments e);

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

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
    [Category("Behavior")]
    [DefaultValue(true)]
    [Description("Maximal value of slider in milliseconds")]
    public Boolean ShowCaret
    {
      get { return _showCaret; }
      set { _showCaret = value; }
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
          if (value > SectionEndTime || value < SectionStartTime)
          {
            throw (new ArgumentException("Current Time has to be in section range"));
          }
          _caretThumbPosition = value / (double)Duration;
          _caretThumbBounds.X = CurrentCaretThumbXCoordinate() - _halfThumbWidth +
              this.Padding.Left + (int)_timeTextSize.Width;
          this.Invalidate();
        }
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
          _leftThumbBounds.X = CurrentLeftThumbXCoordinate() - _halfThumbWidth +
             this.Padding.Left + (int)_timeTextSize.Width;
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
          _rightThumbBounds.X = CurrentRightThumbXCoordinate() - _halfThumbWidth +
        this.Padding.Left + (int)_timeTextSize.Width;
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
    public RangeTrackBar()
    {
      this.DoubleBuffered = true;
      this.ResizeRedraw = true;
      _showCaret = true;
      // Create the ToolTips for the Start and End time.
      _toolTipStartTime = new ToolTip();
      _toolTipEndTime = new ToolTip();

      // Calculate the initial sizes of the bar and thumb.
      SetupTrackBar();
    }

    /// <summary>
    /// Calculate the sizes of the bar and thumbs rectangles.
    /// </summary>
    private void SetupTrackBar()
    {

      using (Graphics g = this.CreateGraphics())
      {
        if (TrackBarRenderer.IsSupported)
        //return;
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

        _timeTextSize = g.MeasureString("00:00:000", TimeFont);
      }
      _halfThumbWidth = _leftThumbBounds.Width / 2;
      // Calculate the size of the track bar.
      _trackBounds.X = ClientRectangle.X + _halfThumbWidth +
        this.Padding.Left + (int)_timeTextSize.Width;
      _trackBounds.Y = ClientRectangle.Y + _leftThumbBounds.Height / 2 +
        this.Padding.Top;// +(int)_timeTextSize.Height;
      _trackBounds.Width = ClientRectangle.Width - _rightThumbBounds.Width -
        this.Padding.Left - this.Padding.Right - 2 * (int)_timeTextSize.Width;
      _trackBounds.Height = 4;

      _leftThumbBounds.X = CurrentLeftThumbXCoordinate() - _halfThumbWidth +
        this.Padding.Left + (int)_timeTextSize.Width;
      _rightThumbBounds.X = CurrentRightThumbXCoordinate() - _halfThumbWidth + this.Padding.Left
        + (int)_timeTextSize.Width;
      _caretThumbBounds.X = CurrentCaretThumbXCoordinate() - _halfThumbWidth +
        this.Padding.Left + (int)_timeTextSize.Width;

      _leftThumbBounds.Y = this.Padding.Top;// +(int)_timeTextSize.Height;
      _rightThumbBounds.Y = this.Padding.Top;// +(int)_timeTextSize.Height;
      _caretThumbBounds.Y = this.Padding.Top;// +(int)_timeTextSize.Height;
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
    /// Overriden. Occurs when the control's padding changes. 
    /// Recalculation of sizes of the bar and thumbs rectangles needed.
    /// </summary>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    protected override void OnPaddingChanged(EventArgs e)
    {
      base.OnPaddingChanged(e);
      SetupTrackBar();
    }

    /// <summary>
    /// Overriden. Occurs when the control's size changes. 
    /// Recalculation of sizes of the bar and thumbs rectangles needed.
    /// </summary>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    protected override void OnSizeChanged(EventArgs e)
    {
      base.OnSizeChanged(e);
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
      //Visual Styles have to be enabled.
      if (!TrackBarRenderer.IsSupported)
      {
        _trackBounds.Y -= 2;
        e.Graphics.DrawRectangle(Pens.Black, _trackBounds);
        _trackBounds.Y += 2;
        //return;
      }
      else
      {
        //Draws the track line.
        TrackBarRenderer.DrawHorizontalTrack(e.Graphics, _trackBounds);
      }

      //Draws the interior of the trackline in green, if it is in the section between start and end thumb.
      Rectangle fillRectangle = _trackBounds;
      fillRectangle.Inflate(-1, -1);
      fillRectangle.X = CurrentLeftThumbXCoordinate() + this.Padding.Left + (int)_timeTextSize.Width;
      fillRectangle.Width = CurrentRightThumbXCoordinate() - fillRectangle.X +
        this.Padding.Left + (int)_timeTextSize.Width;

      if (TrackBarRenderer.IsSupported)
      {
      e.Graphics.FillRectangle(Brushes.Green, fillRectangle);

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
        fillRectangle.Y -= 2;
        fillRectangle.Height += 1;
        e.Graphics.FillRectangle(Brushes.Green, fillRectangle);

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
      if (_showCaret)
      {
        if (_leftThumbState == TrackBarThumbState.Hot || _leftThumbState == TrackBarThumbState.Pressed)
        {
          _toolTipStartTime.Show(ConvertToSeconds(this.SectionStartTime), this,
            CurrentLeftThumbXCoordinate() + this.Padding.Left +
            (int)_timeTextSize.Width - (int)(_timeTextSize.Width / 2),
            (int)(_leftThumbBounds.Top - _timeTextSize.Height - 2));
        }
        else
        {
          _toolTipStartTime.Hide(this);
        }
      }

      //If track bar is in caret mode,
      //show tooltips with the section end time above the center of the right thumb
      //if the mouse is over it.
      if (_showCaret)
      {
        if (_rightThumbState == TrackBarThumbState.Hot || _rightThumbState == TrackBarThumbState.Pressed)
        {
          _toolTipEndTime.Show(ConvertToSeconds(this.SectionEndTime), this,
            CurrentRightThumbXCoordinate() + this.Padding.Left +
            (int)_timeTextSize.Width - (int)(_timeTextSize.Width / 2),
            (int)(_leftThumbBounds.Top - _timeTextSize.Height - 2));
        }
        else
        {
          _toolTipEndTime.Hide(this);
        }
      }


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
      e.Graphics.DrawString(ConvertToSeconds(rightTime), RangeTrackBar.TimeFont,
        RangeTrackBar.TimeFontBrush,
        this.Padding.Left + (int)_timeTextSize.Width + _trackBounds.Width + _leftThumbBounds.Width,
        this.Padding.Top + _leftThumbBounds.Height / 2 -
        _timeTextSize.Height / 2);//+ (int)_timeTextSize.Height);

      //Depending on caret drawing mode show either the carets time value
      //at the left of the control or the section start time value.
      int leftTime=0;
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
      e.Graphics.DrawString(ConvertToSeconds(leftTime), RangeTrackBar.TimeFont,
        RangeTrackBar.TimeFontBrush,
        this.Padding.Left,
        this.Padding.Top + _leftThumbBounds.Height / 2 -
        _timeTextSize.Height / 2);// + (int)_timeTextSize.Height);

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
          if (e.Location.X > _trackBounds.X &&
              e.Location.X < (_trackBounds.X +
              _trackBounds.Width - _leftThumbBounds.Width))
          {
            _leftThumbClicked = false;
            _leftThumbState = TrackBarThumbState.Hot;
            this.Invalidate();
          }

          _leftThumbClicked = false;
        }
        else if (_rightThumbClicked == true)
        {
          if (e.Location.X > _trackBounds.X &&
              e.Location.X < (_trackBounds.X +
              _trackBounds.Width - _leftThumbBounds.Width))
          {
            _rightThumbClicked = false;
            _rightThumbState = TrackBarThumbState.Hot;
            this.Invalidate();
          }

          _rightThumbClicked = false;
        }
        else if (_caretThumbClicked == true)
        {
          if (e.Location.X > _trackBounds.X &&
              e.Location.X < (_trackBounds.X +
              _trackBounds.Width - _leftThumbBounds.Width))
          {
            _caretThumbClicked = false;
            _caretThumbState = TrackBarThumbState.Hot;
            this.Invalidate();
          }

          _caretThumbClicked = false;
        }
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

      if (_duration != 0)
      {
        // The user is moving the thumb.
        if (_leftThumbClicked == true)
        {
          if (e.X >= _halfThumbWidth + this.Padding.Left + (int)_timeTextSize.Width &&
            e.X < CurrentRightThumbXCoordinate() + this.Padding.Left + (int)_timeTextSize.Width)
          {
            _leftThumbPosition = (e.X - _halfThumbWidth -
              this.Padding.Left - (int)_timeTextSize.Width) / (double)_trackBounds.Width;
            _leftThumbBounds.X = CurrentLeftThumbXCoordinate() - _halfThumbWidth +
              this.Padding.Left + (int)_timeTextSize.Width;
            OnSectionStartValueChanged(new PositionValueChangedEventArguments(this.SectionStartTime));
          }
          //if caret would be out of bounds reposition caret.
          if (e.X >= CurrentCaretThumbXCoordinate() + this.Padding.Left + (int)_timeTextSize.Width
            && e.X <= CurrentRightThumbXCoordinate() + this.Padding.Left + (int)_timeTextSize.Width)
          {
            _caretThumbPosition = (e.X - _halfThumbWidth -
              this.Padding.Left - (int)_timeTextSize.Width) / (double)_trackBounds.Width;
            _caretThumbBounds.X = CurrentCaretThumbXCoordinate() - _halfThumbWidth +
              this.Padding.Left + (int)_timeTextSize.Width;
            OnCaretValueChanged(new PositionValueChangedEventArguments(this.CurrentTime));
          }
        }
        // The cursor is passing over the track.
        else
        {
          _leftThumbState = _leftThumbBounds.Contains(e.Location) ?
              TrackBarThumbState.Hot : TrackBarThumbState.Normal;
        }



        // The user is moving the thumb.
        if (_rightThumbClicked == true)
        {
          if (e.X > CurrentLeftThumbXCoordinate() + this.Padding.Left + (int)_timeTextSize.Width &&
            e.X <= _trackBounds.Width + _halfThumbWidth + this.Padding.Left + (int)_timeTextSize.Width)
          {
            _rightThumbPosition = (e.X - _halfThumbWidth -
              this.Padding.Left - (int)_timeTextSize.Width) / (double)_trackBounds.Width;
            _rightThumbBounds.X = CurrentRightThumbXCoordinate() - _halfThumbWidth +
              this.Padding.Left + (int)_timeTextSize.Width;
            OnSectionEndValueChanged(new PositionValueChangedEventArguments(this.SectionEndTime));
          }
          //if caret would be out of bounds reposition caret.
          if (e.X <= CurrentCaretThumbXCoordinate() + this.Padding.Left + (int)_timeTextSize.Width
            && e.X >= CurrentLeftThumbXCoordinate() + this.Padding.Left + (int)_timeTextSize.Width)
          {
            _caretThumbPosition = (e.X - _halfThumbWidth -
              this.Padding.Left - (int)_timeTextSize.Width) / (double)_trackBounds.Width;
            _caretThumbBounds.X = CurrentCaretThumbXCoordinate() - _halfThumbWidth +
              this.Padding.Left + (int)_timeTextSize.Width;
            OnCaretValueChanged(new PositionValueChangedEventArguments(this.CurrentTime));
          }
        }
        // The cursor is passing over the track.
        else
        {
          _rightThumbState = _rightThumbBounds.Contains(e.Location) ?
              TrackBarThumbState.Hot : TrackBarThumbState.Normal;
        }

        // The user is moving the thumb.
        if (_caretThumbClicked == true)
        {
          if (e.X >= CurrentLeftThumbXCoordinate() + this.Padding.Left + (int)_timeTextSize.Width &&
            e.X <= CurrentRightThumbXCoordinate() + this.Padding.Left + (int)_timeTextSize.Width)
          {
            _caretThumbPosition = (e.X - _halfThumbWidth -
              this.Padding.Left - (int)_timeTextSize.Width) / (double)_trackBounds.Width;
            _caretThumbBounds.X = CurrentCaretThumbXCoordinate() - _halfThumbWidth +
              this.Padding.Left + (int)_timeTextSize.Width;
            OnCaretValueChanged(new PositionValueChangedEventArguments(this.CurrentTime));
          }
        }
        // The cursor is passing over the track.
        else
        {
          _caretThumbState = _caretThumbBounds.Contains(e.Location) ?
              TrackBarThumbState.Hot : TrackBarThumbState.Normal;
        }

        //Refresh();
        //this.Invalidate();
      }
    }

    #endregion //OVERRIDES

    ///////////////////////////////////////////////////////////////////////////////
    // Methods for doing main class job                                          //
    ///////////////////////////////////////////////////////////////////////////////
    #region METHODS

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
    public void ResetTrackBar()
    {
      this.CurrentTime = this.SectionStartTime;
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
    private int CurrentLeftThumbXCoordinate()
    {
      return ((int)(_trackBounds.Width * _leftThumbPosition) + _halfThumbWidth);
    }

    /// <summary>
    /// Returns the current right thumbs X coordinate.
    /// </summary>
    /// <returns>A <see cref="int"/> with the right thumbs X coordinate in pixels relative to the tracks bounds.</returns>
    private int CurrentRightThumbXCoordinate()
    {
      return ((int)(_trackBounds.Width * _rightThumbPosition) + _halfThumbWidth);
    }

    /// <summary>
    /// Returns the current caret thumbs X coordinate.
    /// </summary>
    /// <returns>A <see cref="int"/> with the caret thumbs X coordinate in pixels relative to the tracks bounds.</returns>
    private int CurrentCaretThumbXCoordinate()
    {
      return ((int)(_trackBounds.Width * _caretThumbPosition) + _halfThumbWidth);
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

    #endregion //HELPER
  }
}
