using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms.VisualStyles;

namespace OgamaControls
{
  /// <summary>
  /// 
  /// </summary>
  public class TimeLineMarker
  {
    /// <summary>
    /// The width of the time line markers
    /// </summary>
    public const int MARKERWIDTH = 10;

    private int markerTime;

    private Color markerColor;

    private TrackBarThumbState state;

    private int eventID;

    //private RectangleF bounds;

    /// <summary>
    /// 
    /// </summary>
    public TrackBarThumbState State
    {
      get { return this.state; }
      set { this.state = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    public int EventID
    {
      get { return this.eventID; }
      set { this.eventID = value; }
    }

    ///// <summary>
    ///// 
    ///// </summary>
    //public RectangleF Bounds
    //{
    //  get { return this.bounds; }
    //  set { this.bounds = value; }
    //}

    /// <summary>
    /// 
    /// </summary>
    public int Time
    {
      get { return markerTime; }
      set { markerTime = value; }
    }
    /// <summary>
    /// 
    /// </summary>
    public Color MarkerColor
    {
      get { return markerColor; }
      set { markerColor = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    public TimeLineMarker()
    {
      this.state = TrackBarThumbState.Normal;
      this.markerColor = Color.Yellow;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="newEventID"></param>
    /// <param name="newTime"></param>
    /// <param name="newMarkerColor"></param>
    public TimeLineMarker(int newEventID, int newTime, Color newMarkerColor)
    {
      this.eventID = newEventID;
      this.markerTime = newTime;
      this.markerColor = newMarkerColor;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="g"></param>
    /// <param name="xPosition"></param>
    /// <param name="timeLineHeight"></param>
    public void Draw(Graphics g, int xPosition, int timeLineHeight)
    {
      GraphicsPath markerPath = new GraphicsPath();
      PointF topLeft = new PointF(xPosition - MARKERWIDTH / 2, 1);
      PointF topRight = new PointF(xPosition + MARKERWIDTH / 2, 1);
      //PointF topMiddleRight = new PointF(xPosition + 1, timeLineHeight / 3);
      PointF topMiddle = new PointF(xPosition, timeLineHeight / 2 + 1);
      //PointF bottomMiddleRight = new PointF(xPosition + 1, 2 * timeLineHeight / 3);
      //PointF bottomRight = new PointF(xPosition + markerWidth / 2, timeLineHeight);
      //PointF bottomLeft = new PointF(xPosition - markerWidth / 2, timeLineHeight);
      //PointF bottomMiddleLeft = new PointF(xPosition - 1, 2 * timeLineHeight / 3);
      //PointF topMiddleLeft = new PointF(xPosition - 1, timeLineHeight / 3);
      Point gradientLeft = new Point(xPosition - (MARKERWIDTH / 2) - 2, 0);
      Point gradientRight = new Point(xPosition + MARKERWIDTH, timeLineHeight);

      //markerPath.AddLines(new PointF[]
      //        {
      //          topLeft,
      //          topRight,
      //          topMiddleRight,
      //          bottomMiddleRight,
      //          bottomRight,
      //          bottomLeft,
      //          bottomMiddleLeft,
      //          topMiddleLeft,
      //          topLeft
      //        }
      //);
      markerPath.AddLines(new PointF[]
              {
                topLeft,
                topRight,
                topMiddle,
                topLeft,
              }
      );

      LinearGradientBrush markerFillBrush = null;

      switch (this.state)
      {
        case TrackBarThumbState.Disabled:
          markerFillBrush = new LinearGradientBrush(
                  gradientLeft,
                  gradientRight,
                  Color.Gray,
                  Color.DarkGray);
          break;
        case TrackBarThumbState.Hot:
          markerFillBrush = new LinearGradientBrush(
                  gradientLeft,
                  gradientRight,
                  Color.FromArgb(255,100,100),
                  Color.DarkRed); 
          break;
        case TrackBarThumbState.Normal:
        default:
          markerFillBrush = new LinearGradientBrush(
                  gradientLeft,
                  gradientRight,
                  this.MarkerColor,
                  Color.Red);
          break;
        case TrackBarThumbState.Pressed:
          markerFillBrush = new LinearGradientBrush(
                 gradientLeft,
                 gradientRight,
                 Color.Red,
                 Color.DarkRed); 
          break;
      }


      g.FillPath(markerFillBrush, markerPath);
      //g.DrawPath(new Pen(this.MarkerColor, 2f), markerPath);
    }
  }
}
