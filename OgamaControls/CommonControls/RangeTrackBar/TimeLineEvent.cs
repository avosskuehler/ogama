using System.Drawing;

namespace OgamaControls
{
  /// <summary>
  /// 
  /// </summary>
  public class TimeLineEvent
  {
    private int time;

    private string imageKey;

    private float strokeWidth;

    private TimeLinePosition position;

    private Color strokeColor;

    /// <summary>
    /// 
    /// </summary>
    public int Time
    {
      get { return time; }
      set { time = value; }
    }
    /// <summary>
    /// 
    /// </summary>
    public Color StrokeColor
    {
      get { return strokeColor; }
      set { strokeColor = value; }
    }
    /// <summary>
    /// 
    /// </summary>
    public TimeLinePosition Position
    {
      get { return position; }
      set { position = value; }
    }
    /// <summary>
    /// 
    /// </summary>
    public string ImageKey
    {
      get { return imageKey; }
      set { imageKey = value; }
    }
    /// <summary>
    /// 
    /// </summary>
    public float StrokeWidth
    {
      get { return strokeWidth; }
      set { strokeWidth = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    public TimeLineEvent()
    { 
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="newTime"></param>
    /// <param name="newImageKey"></param>
    /// <param name="newStrokeWidth"></param>
    /// <param name="newStrokeColor"></param>
    /// <param name="newPosition"></param>
    public TimeLineEvent(int newTime, string newImageKey, float newStrokeWidth, Color newStrokeColor, TimeLinePosition newPosition)
    {
      time = newTime;
      imageKey = newImageKey;
      strokeWidth = newStrokeWidth;
      strokeColor = newStrokeColor;
      position = newPosition;
    }
  }
}
