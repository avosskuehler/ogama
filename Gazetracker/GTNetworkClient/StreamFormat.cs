namespace GTNetworkClient
{
  public class StreamFormat
  {
    #region EyeTrackingType enum

    public enum EyeTrackingType
    {
      Left,
      Right,
      Binocular
    }

    #endregion

    public const string StrTimeStampMilliseconds = "%TS";
    public const string StrTimeStampMicroseconds = "%TU";
    public const string StrGazePositionX = "%GX";
    public const string StrGazePositionY = "%GY";
    public const string StrGazeZPosition = "%GZ";
    public const string StrPupilDiameter = "%D";
    //public const string StrPupilDiameterR = "%DR";
    public const string StrPupilPositionX = "%PX";
    public const string StrPupilPositionY = "%PY";
    public const string StrCornealReflexPositionX = "%CX";
    public const string StrCornealReflexPositionY = "%CY";
    public const string StrHeadPositionX = "%HX";
    public const string StrHeadPositionY = "%HY";
    public const string StrHeadPositionZ = "%HZ";
    public const string StrEyeTrackingType = "%ET";

    private bool cornealReflexPosition;
    private bool eyeTypeInfo = false;
    private bool gazePosition = true;
    private bool headPosition;
    private bool pupilDiameter = true;
    private bool pupilPosition;
    private bool timeStampMicroseconds;
    private bool timeStampMilliseconds = true;


    public StreamFormat()
    {
      EyetrackingType = EyeTrackingType.Right; // Default
    }

    public void Parse(string formatStr)
    {
      char[] seperator = { ' ' };
      string[] cmd = formatStr.Split(seperator, 5);

      switch (cmd[0])
      {
        case StrTimeStampMilliseconds:
          timeStampMilliseconds = true;
          break;

        case StrTimeStampMicroseconds:
          timeStampMicroseconds = true;
          break;

        case StrGazePositionX:
          gazePosition = true;
          break;

        case StrGazePositionY:
          gazePosition = true;
          break;

        case StrGazeZPosition:
          gazePosition = true;
          break;

        case StrPupilDiameter:
          pupilDiameter = true;
          break;

        //case StrPupilDiameterR:
        //    pupilDiameter = true;
        //    break;

        case StrPupilPositionX:
          pupilPosition = true;
          break;

        case StrPupilPositionY:
          pupilPosition = true;
          break;

        case StrCornealReflexPositionX:
          cornealReflexPosition = true;
          break;

        case StrCornealReflexPositionY:
          cornealReflexPosition = true;
          break;

        case StrHeadPositionX:
          headPosition = true;
          break;

        case StrHeadPositionY:
          headPosition = true;
          break;

        case StrHeadPositionZ:
          headPosition = true;
          break;
      }
    }

    #region Format

    public string GetFormatString()
    {
      string format = "";

      if (timeStampMilliseconds)
        format = StrTimeStampMilliseconds;
      else if (timeStampMicroseconds)
        format = StrTimeStampMicroseconds;


      if (gazePosition)
        format += " " + StrGazePositionX + " " + StrGazePositionY;

      if (pupilDiameter)
        format += " " + StrPupilDiameter;

      if (pupilPosition)
        format += " " + StrPupilPositionX + " " + StrPupilPositionY;

      if (cornealReflexPosition)
        format += " " + StrCornealReflexPositionX + " " + StrCornealReflexPositionY;

      if (headPosition)
        format += " " + StrHeadPositionX + " " + StrHeadPositionY + " " + StrHeadPositionZ;

      if (eyeTypeInfo)
        format += " " + StrEyeTrackingType;

      return format;
    }

    public string GetFormatStringDescription()
    {
      string desc = "";

      if (timeStampMilliseconds)
        desc = "MilliSec";
      else if (timeStampMicroseconds)
        desc = "MicroSec";

      if (pupilDiameter)
        desc += " " + "PupDiam X" + " " + "PupDiam Y";

      if (pupilPosition)
        desc += " " + "PupPosX" + " " + "PupPosY";

      if (cornealReflexPosition)
        desc += " " + "CornRflxPosX" + " " + "CornRflxPosY";

      if (gazePosition)
        desc += " " + "GazePosX" + " " + "GazePosY";

      if (eyeTypeInfo)
        desc += " " + "Eye";

      return desc;
    }

    #endregion

    #region Get/Set

    public bool TimeStampMilliseconds
    {
      get { return timeStampMilliseconds; }
      set { timeStampMilliseconds = value; }
    }

    public bool TimeStampMicroseconds
    {
      get { return timeStampMicroseconds; }
      set { timeStampMicroseconds = value; }
    }

    public bool PupilDiameter
    {
      get { return pupilDiameter; }
      set { pupilDiameter = value; }
    }

    public bool PupilPosition
    {
      get { return pupilPosition; }
      set { pupilPosition = value; }
    }

    public bool ConrnealReflexPosition
    {
      get { return cornealReflexPosition; }
      set { cornealReflexPosition = value; }
    }

    public bool GazePosition
    {
      get { return gazePosition; }
      set { gazePosition = value; }
    }

    public bool HeadPosition
    {
      get { return headPosition; }
      set { headPosition = value; }
    }

    public EyeTrackingType EyetrackingType { get; set; }

    #endregion
  }
}