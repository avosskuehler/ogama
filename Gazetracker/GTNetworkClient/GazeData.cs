using System;
using System.Windows.Forms;
//using GazeTrackingLibrary.EyeMovementDetection;

namespace GTNetworkClient
{
  // <Summary>
  // The class maintains information on the gaze position and other data provided by the eye tracker
  //
  // V.01 extractTrackerData(string)
  //      - Extracts the gazePositionX
  //      - Extracts the gazePositionY
  //
  // V.02 extractTrackerData(string)
  //      - Quick and dirty method of smoothing (average of _smoothSampleSize)
  //
  // V.03 
  //      - Implemented the AverageWindow Smoother from the ITU Gaze Tracker library 
  //      - Extracts corneal reflex position 
  //      - Estimates distance from screen and fovea visual angle, see DistanceFromDisplay
  //        works so-so, nodding or turing head upwards create the same effect as moving head forward/backwards
  //        Gives a rough estimate..

  // </Summary>

  public class GazeData
  {
    // Variables 

    // Temp variables for working..

    #region Delegates

    public delegate void FixationHandler(GazeData gazeData);

    public delegate void GazeDataHandler(GazeData gazeData);

    public delegate void GazeDataSmoothHandler(GazeData gazeData);

    #endregion

    private readonly int screenResolution = 1200; // default, gets set by the const.

    private double GazeX;
    private double GazeY;

    // Calculating distance to screen and fovea pixel resolution (experimental)
    private double foveaVisualAngle = 2.5;
    private int rowCount;
    private int screenInches = 20;
    private long timestampLast;

    public GazeData()
    {
      screenResolution = Screen.PrimaryScreen.Bounds.Width;
      //smoother = new GazeTrackingLibrary.EyeMovementDetection.AverageWindow(_smoothDataNumberOfSamples);
      //smoother = new GazeTrackingLibrary.EyeMovementDetection.ExponentialSmoother(_smoothDataNumberOfSamples,0,5);      
    }

    public event GazeDataHandler OnGazeData;

    public event GazeDataSmoothHandler OnSmoothedGazeData;

    public event FixationHandler OnFixation;


    public void extractTrackerData(StreamFormat format, string dataStr)
    {
      rowCount++;

      double XRight = 0;
      double XLeft = 0;

      double YRight = 0;
      double YLeft = 0;

      char[] seperator = { ' ' };
      string[] tmp = dataStr.Split(seperator, 15);

      string[] formatStr = format.GetFormatString().Split(seperator);

      int colCount = 1; // Starts at 1 because 0 is ET_SPL (data)

      foreach (string fstr in formatStr)
      {
        if (tmp.Length == colCount)
        {
          // safety check to avoid reading empty columns
          break;
        }

        switch (fstr)
        {
          case StreamFormat.StrTimeStampMilliseconds:
            TimeStamp = long.Parse(tmp[colCount]);
            break;

          case StreamFormat.StrTimeStampMicroseconds:
            TimeStamp = long.Parse(tmp[colCount]);
            break;

          case StreamFormat.StrGazePositionX:

            if (format.EyetrackingType == StreamFormat.EyeTrackingType.Left)
              XLeft = Convert.ToDouble(tmp[colCount]);
            else if (format.EyetrackingType == StreamFormat.EyeTrackingType.Right)
              XRight = Convert.ToDouble(tmp[colCount]);
            else if (format.EyetrackingType == StreamFormat.EyeTrackingType.Binocular)
            {
              XLeft = Convert.ToDouble(tmp[colCount]);
              colCount = colCount + 1; // Two values for binocular
              XRight = Convert.ToDouble(tmp[colCount]);
            }
            break;

          case StreamFormat.StrGazePositionY:
            if (format.EyetrackingType == StreamFormat.EyeTrackingType.Left)
              YLeft = Convert.ToDouble(tmp[colCount]);
            else if (format.EyetrackingType == StreamFormat.EyeTrackingType.Right)
              YRight = Convert.ToDouble(tmp[colCount]);
            else if (format.EyetrackingType == StreamFormat.EyeTrackingType.Binocular)
            {
              YRight = Convert.ToDouble(tmp[colCount]);
              colCount = colCount + 1; // Two values for binocular
              YLeft = Convert.ToDouble(tmp[colCount]);
            }
            break;

          case StreamFormat.StrPupilDiameter:

            if (format.EyetrackingType == StreamFormat.EyeTrackingType.Left)
              PupilDiameterLeft = Convert.ToInt32(tmp[colCount]);

            else if (format.EyetrackingType == StreamFormat.EyeTrackingType.Right)
              PupilDiameterRight = Convert.ToInt32(tmp[colCount]);

            else if (format.EyetrackingType == StreamFormat.EyeTrackingType.Binocular)
            {
              PupilDiameterLeft = Convert.ToInt32(tmp[colCount]);
              colCount = colCount + 1; // Two values for binocular
              PupilDiameterRight = Convert.ToInt32(tmp[colCount]);
            }
            break;


          //case StreamFormat.StrPupilDiameterR:

          //    if (format.EyetrackingType == StreamFormat.EyeTrackingType.Left)
          //        PupilDiameterRight = Convert.ToInt32(tmp[colCount]);

          //    else if (format.EyetrackingType == StreamFormat.EyeTrackingType.Right)
          //        PupilDiameterRightY = Convert.ToInt32(tmp[colCount]);

          //    else if (format.EyetrackingType == StreamFormat.EyeTrackingType.Binocular)
          //    {
          //        PupilDiameterLeftY = Convert.ToInt32(tmp[colCount]);
          //        colCount = colCount + 1; // Two values for binocular
          //        PupilDiameterRightY = Convert.ToInt32(tmp[colCount]);
          //    }
          //    break;


          case StreamFormat.StrPupilPositionX:

            if (format.EyetrackingType == StreamFormat.EyeTrackingType.Left)
              PupilPositionLeftX = Convert.ToInt32(tmp[colCount]);

            else if (format.EyetrackingType == StreamFormat.EyeTrackingType.Right)
              PupilPositionRightX = Convert.ToInt32(tmp[colCount]);

            else if (format.EyetrackingType == StreamFormat.EyeTrackingType.Binocular)
            {
              PupilPositionLeftX = Convert.ToInt32(tmp[colCount]);
              colCount = colCount + 1; // Two values for binocular
              PupilPositionRightX = Convert.ToInt32(tmp[colCount]);
            }
            break;


          case StreamFormat.StrPupilPositionY:

            if (format.EyetrackingType == StreamFormat.EyeTrackingType.Left)
              PupilPositionLeftY = Convert.ToInt32(tmp[colCount]);

            else if (format.EyetrackingType == StreamFormat.EyeTrackingType.Right)
              PupilPositionLeftY = Convert.ToInt32(tmp[colCount]);

            else if (format.EyetrackingType == StreamFormat.EyeTrackingType.Binocular)
            {
              PupilPositionLeftY = Convert.ToInt32(tmp[colCount]);
              colCount = colCount + 1; // Two values for binocular
              PupilPositionLeftY = Convert.ToInt32(tmp[colCount]);
            }
            break;


          case StreamFormat.StrCornealReflexPositionX:

            if (format.EyetrackingType == StreamFormat.EyeTrackingType.Left)
            {
              try
              {
                CornealReflexPositionLeftX = Convert.ToInt32(tmp[colCount]);
              }
              catch (Exception ex)
              {
                Console.Out.WriteLine("GazeData. Error parsing Corneal Reflex X-Position: " + ex.Message);
              }
            }

            else if (format.EyetrackingType == StreamFormat.EyeTrackingType.Right)
            {
              try
              {
                CornealReflexPositionRightX = Convert.ToInt32(tmp[colCount]);
              }
              catch (Exception ex)
              {
                Console.Out.WriteLine("GazeData. Error parsing Corneal Reflex X-Position: " + ex.Message);
              }
            }

            else if (format.EyetrackingType == StreamFormat.EyeTrackingType.Binocular)
            {
              try
              {
                CornealReflexPositionLeftX = Convert.ToInt32(tmp[colCount]);
                colCount = colCount + 1; // Two values for binocular
                CornealReflexPositionRightX = Convert.ToInt32(tmp[colCount]);
              }
              catch (Exception ex)
              {
                Console.Out.WriteLine("GazeData. Error parsing Corneal Reflex X-Position: " + ex.Message);
              }
            }
            break;


          case StreamFormat.StrCornealReflexPositionY:

            if (format.EyetrackingType == StreamFormat.EyeTrackingType.Left)
            {
              try
              {
                CornealReflexPositionLeftY = Convert.ToInt32(tmp[colCount]);
              }
              catch (Exception ex)
              {
                Console.Out.WriteLine("GazeData. Error parsing Corneal Reflex Y-Position: " + ex.Message);
              }
            }
            else if (format.EyetrackingType == StreamFormat.EyeTrackingType.Right)
            {
              try
              {
                CornealReflexPositionRightY = Convert.ToInt32(tmp[colCount]);
              }
              catch (Exception ex)
              {
                Console.Out.WriteLine("GazeData. Error parsing Corneal Reflex Y-Position: " + ex.Message);
              }
            }
            else if (format.EyetrackingType == StreamFormat.EyeTrackingType.Binocular)
            {
              try
              {
                CornealReflexPositionLeftY = Convert.ToInt32(tmp[colCount]);
                colCount = colCount + 1; // Two values for binocular
                CornealReflexPositionRightY = Convert.ToInt32(tmp[colCount]);
              }
              catch (Exception ex)
              {
                Console.Out.WriteLine("GazeData. Error parsing Corneal Reflex Y-Position: " + ex.Message);
              }
            }
            break;

          case StreamFormat.StrEyeTrackingType:
            // Set by the stream format.. but
            // l = left
            // r = right
            // b = binocular
            break;
        }
        colCount++;
      }


      // Check values, if one is left is 0 set it to right and vice versa 

      try
      {
        if (XLeft == 0 && XRight == 0)
        {
          XRight = GazeX;
          XLeft = GazeX;
        }
        else if (XLeft == 0 && XRight != 0)
        {
          XLeft = XRight;
        }
        else if (XRight == 0 && XLeft != 0)
        {
          XRight = XLeft;
        }


        if (YLeft == 0 && YRight == 0)
        {
          YRight = GazeY;
          YLeft = GazeY;
        }
        else if (YLeft == 0 && YRight != 0)
        {
          YLeft = YRight;
        }
        else if (YRight == 0 && YLeft != 0)
        {
          YRight = YLeft;
        }
      }
      catch (Exception ex)
      {
        Console.Out.WriteLine("ETClient.GazeData.ExtractTrackerData. Could not parse X/Y. Data: " + dataStr +
                              " Message: " + ex.Message);
      }

      // Average gaze postition (to be implemented in tracker?)
      GazeX = (XLeft + XRight) / 2;
      GazeY = (YLeft + YRight) / 2;

      if (TimeStamp != timestampLast)
      {
        SetGazePositionValues(GazeX, GazeY);
        SetGazePositionSmoothedValues(GazeX, GazeY);
        timestampLast = TimeStamp;
      }

      //GazeTrackingLibrary.GTPoint gtp = new GazeTrackingLibrary.GTPoint(XLast, YLast);
      //SetGazePositionSmoothedValues(smoother.Smooth(gtp).X, smoother.Smooth(gtp).Y);
    }


    private void SetGazePositionValues(double newGazeX, double newGazeY)
    {
      //if (newGazeX != 0 && newGazeY != 0)
      //{
      GazePositionX = newGazeX;
      GazePositionY = newGazeY;

      if (OnGazeData != null)
        OnGazeData(this);
      //}
    }


    private void SetGazePositionSmoothedValues(double newGazeX, double newGazeY)
    {
      //if (newGazeX != 0 && newGazeY != 0)
      //{
      GazePositionXSmoothed = newGazeX;
      GazePositionYSmoothed = newGazeY;

      //if (IsFixationDetectionEnabled)
      //    eyeMovements.CalculateEyeMovement(this.GazePositionXSmoothed, this.GazePositionYSmoothed);

      if (OnSmoothedGazeData != null)
        OnSmoothedGazeData(this);
      //}
    }


    public void FixationDetection(int fixWindowSize, int maxVelocity, int distanceToScreen)
    {
      //IsFixationDetectionEnabled = true;
      //eyeMovements = new GazeTrackingLibrary.EyeMovementDetection.EyeMovements(fixWindowSize, maxVelocity, distanceToScreen);
      //eyeMovements.OnFixation += new EyeMovements.FixationHandler(eyeMovements_OnFixation);
    }


    private void eyeMovements_OnFixation(double x, double y)
    {
      if (OnFixation != null)
        OnFixation(this);
    }

    #region Get Distance from screen and foveal pixel size (very experimental!)

    private int lastDistFromScreen = 100;

    public int DistanceFromDisplay
    {
      get
      {
        // Affecting values CornealReflexPositionRightY/CornealReflexPositionLeftY
        // Close  1000, 1000
        // Medium 3000, 3000
        // Far    7000, 7000 
        if (CornealReflexPositionLeftY != 0 && CornealReflexPositionRightY != 0)
        {
          int avg = Convert.ToInt32((CornealReflexPositionLeftY + CornealReflexPositionRightY) / 2);
          lastDistFromScreen = ((avg / 100) - 20) / 2;
        }
        return lastDistFromScreen;
      }
    }

    public int FoveaResolution
    {
      get
      {
        // ViewingDistance * TAN(RADIANS(VisualAngle)) * Resolution / (screenInches*2, 54*0, 8)
        // See http://yangming.huang.googlepages.com/research2

        double radian = Math.Tan(Radians(foveaVisualAngle));
        double screen = screenResolution / (screenInches * 2.54 * 0.8);
        double pixelRadius = DistanceFromDisplay * radian * screen;

        Console.Out.WriteLine("Distance: " + DistanceFromDisplay + " Fovea: " + pixelRadius);

        return Convert.ToInt32(pixelRadius);
      }
    }


    public int HeadPosition
    {
      get
      {
        // TODO..
        // Affecting values CornealReflexPositionRightX / CornealReflexPositionLeftX
        // Center 21000, 28000
        // Far left 34500, 41700
        // Far right 1000, 9000 

        return 0;
      }
    }

    private double Radians(double angle)
    {
      double rad = Math.PI * angle / 180.0;
      return rad;
    }

    #endregion

    #region Get/Set

    //public bool SmoothData
    //{
    //    get { return _smoothData; }
    //    set { _smoothData = value; }
    //}

    //public int SmoothDataNumberOfSamples
    //{
    //    get { return _smoothDataNumberOfSamples; }
    //    set {

    //        _smoothDataNumberOfSamples = value;
    //        smoother = new GazeTrackingLibrary.EyeMovementDetection.AverageWindow(_smoothDataNumberOfSamples);
    //        //smoother = new GazeTrackingLibrary.EyeMovementDetection.ExponentialSmoother(_smoothDataNumberOfSamples, 0, 5);
    //    }
    //}

    public long TimeStamp { get; set; }


    // Gaze positions
    public double GazePositionX { get; set; }

    public double GazePositionY { get; set; }

    public double GazePositionXSmoothed { get; set; }

    public double GazePositionYSmoothed { get; set; }


    // Pupil position
    public int PupilDiameterLeft { get; set; }

    public int PupilDiameterRight { get; set; }

    //public int PupilDiameterLeftY { get; set; }

    //public int PupilDiameterRightY { get; set; }


    // Pupil position
    public int PupilPositionLeftX { get; set; }

    public int PupilPositionRightX { get; set; }

    public int PupilPositionLeftY { get; set; }

    public int PupilPositionRightY { get; set; }


    // Corneal reflex position
    public int CornealReflexPositionLeftX { get; set; }

    public int CornealReflexPositionLeftY { get; set; }

    public int CornealReflexPositionRightX { get; set; }

    public int CornealReflexPositionRightY { get; set; }

    #endregion
  }
}