namespace GTLibrary.Calibration
{
  using System;
  using System.Text;
  using System.Xml.Linq;

  using GTLibrary.Utils;

  using GTCommons.Enum;
  using GTSettings;

  public class CalibrationExport
  {
    #region Variables

    private readonly string dataFolder = "";
    private readonly long id;

    #endregion

    #region Constructor

    public CalibrationExport(long id, string dataFolder)
    {
      this.id = id;
      this.dataFolder = dataFolder;
    }

    #endregion

    #region Public methods

    public void GenerateXML(CalibMethod calibration)
    {
      XDocument doc = new XDocument(
                      new XElement("root",
                      new XAttribute("name", "GTCalibration"), // root doc name
                      GenerateEnvironment(),
                      GenerateHardware(),
                      GenerateResults(id, calibration),
                      GenerateCalibrationData(calibration)));

      // Save as  "\Calibration\20110112224355\20110112224355.xml"
      string saveFilePath = dataFolder + "\\" + id + ".xml";

      try
      {
        doc.Save(saveFilePath);
      }
      catch (Exception ex)
      {
        Console.Out.WriteLine("CalibrationExport.cs, error while saving calibration. Message: " + ex.Message);
      }
    }

    #endregion

    #region Private methods

    private XElement GenerateEnvironment()
    {
      var enviroment = new XElement("Environment");

      try
      {
        enviroment = new XElement("Environment",
                      new XElement("MachineName", SanitizeString(Environment.MachineName)),
                      new XElement("CPUName", SystemInfo.CPUName()),
                      new XElement("CPUSpeed", SystemInfo.CPUSpeed()),
                      new XElement("CPULoad", SystemInfo.Instance.GetCPULoad()),
                      new XElement("MemTotal", SystemInfo.Instance.GetTotalMemory()),
                      new XElement("MemLoad", SystemInfo.Instance.GetMemLoad()),
                      new XElement("PrimaryMonitorWidth", ScreenParameters.PrimaryResolution.Width),
                      new XElement("PrimaryMonitoryHeight", ScreenParameters.PrimaryResolution.Height),
                      new XElement("SecondaryMonitorWidth", ScreenParameters.SecondaryResolution.Width),
                      new XElement("SecondaryMonitoryHeight", ScreenParameters.SecondaryResolution.Height),
                      new XElement("UserName", SanitizeString(Environment.UserName)),
                      new XElement("UserDomainName", SanitizeString(Environment.UserDomainName)),
                      new XElement("OSVersion", Environment.OSVersion),
                      new XElement("CLRVersion", Environment.Version),
                      new XElement("IP", SystemInfo.Instance.GetIPAddress())
            );
      }
      catch (Exception ex)
      {
        Console.Out.WriteLine("CalibrationExport, error in GenerateEnvironment: " + ex.Message);
      }

      return enviroment;
    }

    private XElement GenerateHardware()
    {
      return new XElement("Hardware",
                          new XElement("Camera", SanitizeString(GTHardware.Camera.Instance.Device.Name)),
                          new XElement("CameraWidth", GTHardware.Camera.Instance.Device.Width),
                          new XElement("CameraHeight", GTHardware.Camera.Instance.Device.Height),
                          new XElement("CameraFPS", GTHardware.Camera.Instance.Device.FPS),
                          new XElement("TrackingFPS", TrackDB.Instance.TrackingFPS));
    }

    private static XElement GenerateResults(long id, CalibMethod calibration)
    {
      return new XElement("CalibrationResults",
                          new XElement("ID", id),
                          new XElement("NumImages", calibration.NumImages),
                          new XElement("Degrees", calibration.Degrees),
                          new XElement("DegreesLeft", calibration.DegreesLeft),
                          new XElement("DegreesRight", calibration.DegreesRight),
                          new XElement("AverageError", calibration.AverageError),
                          new XElement("AverageErrorLeft", calibration.AverageErrorLeft),
                          new XElement("AverageErrorRight", calibration.AverageErrorRight));
    }

    private static XElement GenerateCalibrationData(CalibMethod calibration)
    {
      var calibrationData = new XElement("CalibrationData");

      foreach (CalibrationTarget ct in calibration.CalibrationTargets)
      {
        for (int j = 0; j < ct.NumImages; j++)
        {
          try
          {
            if (j < ct.pupilCentersLeft.Count && j < ct.glintsLeft.Count &&
                j < ct.estimatedGazeCoordinatesLeft.Count)
            {
              var calData =
                  new XElement("Data",
                               new XElement("T", ct.targetNumber),
                               new XElement("X", ct.targetCoordinates.X),
                               new XElement("Y", ct.targetCoordinates.Y),
                               new XElement("PCLX", Math.Round(ct.pupilCentersLeft[j].X, 4)),
                               new XElement("PCLY", Math.Round(ct.pupilCentersLeft[j].Y, 4)),
                               new XElement("GACLX", Math.Round(ct.glintsLeft[j].AverageCenter.X, 4)),
                               new XElement("GACLY", Math.Round(ct.glintsLeft[j].AverageCenter.Y, 4)),
                               new XElement("EstGLX", Math.Round(ct.estimatedGazeCoordinatesLeft[j].X, 4)),
                               new XElement("EstGLY", Math.Round(ct.estimatedGazeCoordinatesLeft[j].Y, 4))
                      );

              if (Settings.Instance.Processing.TrackingMethod == TrackingMethodEnum.RemoteBinocular)
              {
                if (j < ct.pupilCentersRight.Count && j < ct.glintsRight.Count &&
                    j < ct.estimatedGazeCoordinatesRight.Count)
                {
                  // Add right eye if we're in binocular
                  calData.Add(
                      new XElement("PCRX", Math.Round(ct.pupilCentersRight[j].X, 4)),
                      new XElement("PCRY", Math.Round(ct.pupilCentersRight[j].Y, 4)),
                      new XElement("GACRX", Math.Round(ct.glintsRight[j].AverageCenter.X, 4)),
                      new XElement("GACRY", Math.Round(ct.glintsRight[j].AverageCenter.Y, 4)),
                      new XElement("EstGRX", Math.Round(ct.estimatedGazeCoordinatesRight[j].X, 4)),
                      new XElement("EstGRY", Math.Round(ct.estimatedGazeCoordinatesRight[j].Y, 4))
                      );
                }
              }

              // Add sample to calibrationdata element
              calibrationData.Add(calData);
            }
          }
          catch (Exception ex)
          {
            Console.Out.WriteLine("CalibrationExport.cs, error in GenerateCalibrationData(), message: " + ex.Message);
          }
        } // End per image loop
      } // End foreach

      return calibrationData;
    }

    #endregion

    #region Private helper methods

    private static string SanitizeString(string str)
    {
      if (str == null)
        return "EmptyString";

      var buffer = new StringBuilder(str.Length);

      foreach (char c in str)
      {
        if (IsLegalXmlChar(c))
          buffer.Append(c);
      }

      return buffer.ToString();
    }

    private static bool IsLegalXmlChar(int character)
    {
      return
          character == 0x9 /* == '\t' == 9   */||
              character == 0xA /* == '\n' == 10  */||
              character == 0xD /* == '\r' == 13  */||
              (character >= 0x20 && character <= 0xD7FF) ||
              (character >= 0xE000 && character <= 0xFFFD) ||
              (character >= 0x10000 && character <= 0x10FFFF);
    }

    #endregion

  }
}