namespace OgamaControls
{
  /// <summary>
  /// 
  /// </summary>
  public class TimeLineRange
  {
    private int startTime;

    private int endTime;

    /// <summary>
    /// 
    /// </summary>
    public int StartTime
    {
      get { return startTime; }
      set { startTime = value; }
    }
    /// <summary>
    /// 
    /// </summary>
    public int EndTime
    {
      get { return endTime; }
      set { endTime = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    public bool IsSet
    {
      get { return (endTime - startTime > 0); }
    }

    /// <summary>
    /// 
    /// </summary>
    public TimeLineRange()
    { 
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="newStartTime"></param>
    /// <param name="newEndTime"></param>
    public TimeLineRange(int newStartTime, int newEndTime)
    {
      startTime = newStartTime;
      endTime = newEndTime;
    }
  }
}
