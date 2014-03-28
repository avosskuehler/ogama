using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Threading;

namespace Ogama.Modules.Recording.SMIInterface.RedM
{
  /// <summary>
  /// 
  /// </summary>
  public class EyeTrackingController
  {
    /// <summary>
    /// API Struct definition. See the manual for further description.
    /// </summary>
    public struct SystemInfoStruct
    {
      /// <summary>
      /// The samplerate
      /// </summary>
      public int samplerate;
      /// <summary>
      /// The i v_ major version
      /// </summary>
      public int iV_MajorVersion;
      /// <summary>
      /// The i v_ minor version
      /// </summary>
      public int iV_MinorVersion;
      /// <summary>
      /// The i v_ buildnumber
      /// </summary>
      public int iV_Buildnumber;
      /// <summary>
      /// The ap i_ major version
      /// </summary>
      public int API_MajorVersion;
      /// <summary>
      /// The ap i_ minor version
      /// </summary>
      public int API_MinorVersion;
      /// <summary>
      /// The ap i_ buildnumber
      /// </summary>
      public int API_Buildnumber;
      /// <summary>
      /// The i v_ et system
      /// </summary>
      public int iV_ETSystem;
    };

    /// <summary>
    /// 
    /// </summary>
    public struct CalibrationPointStruct
    {
      /// <summary>
      /// The number
      /// </summary>
      public int number;
      /// <summary>
      /// The position x
      /// </summary>
      public int positionX;
      /// <summary>
      /// The position y
      /// </summary>
      public int positionY;
    };


    /// <summary>
    /// 
    /// </summary>
    public struct EyeDataStruct
    {
      /// <summary>
      /// The gaze x
      /// </summary>
      public double gazeX;
      /// <summary>
      /// The gaze y
      /// </summary>
      public double gazeY;
      /// <summary>
      /// The diam
      /// </summary>
      public double diam;
      /// <summary>
      /// The eye position x
      /// </summary>
      public double eyePositionX;
      /// <summary>
      /// The eye position y
      /// </summary>
      public double eyePositionY;
      /// <summary>
      /// The eye position z
      /// </summary>
      public double eyePositionZ;
    };


    /// <summary>
    /// 
    /// </summary>
    public struct SampleStruct
    {
      /// <summary>
      /// The timestamp{CC2D43FA-BBC4-448A-9D0B-7B57ADF2655C}
      /// </summary>
      public Int64 timestamp;
      /// <summary>
      /// The left eye{CC2D43FA-BBC4-448A-9D0B-7B57ADF2655C}
      /// </summary>
      public EyeDataStruct leftEye;
      /// <summary>
      /// The right eye{CC2D43FA-BBC4-448A-9D0B-7B57ADF2655C}
      /// </summary>
      public EyeDataStruct rightEye;
      /// <summary>
      /// The plane number{CC2D43FA-BBC4-448A-9D0B-7B57ADF2655C}
      /// </summary>
      public int planeNumber;
    };

    /// <summary>
    /// 
    /// </summary>
    public struct EventStruct
    {
      /// <summary>
      /// The event type{CC2D43FA-BBC4-448A-9D0B-7B57ADF2655C}
      /// </summary>
      public char eventType;
      /// <summary>
      /// The eye{CC2D43FA-BBC4-448A-9D0B-7B57ADF2655C}
      /// </summary>
      public char eye;
      /// <summary>
      /// The start time{CC2D43FA-BBC4-448A-9D0B-7B57ADF2655C}
      /// </summary>
      public Int64 startTime;
      /// <summary>
      /// The end time{CC2D43FA-BBC4-448A-9D0B-7B57ADF2655C}
      /// </summary>
      public Int64 endTime;
      /// <summary>
      /// The duration{CC2D43FA-BBC4-448A-9D0B-7B57ADF2655C}
      /// </summary>
      public Int64 duration;
      /// <summary>
      /// The position x{CC2D43FA-BBC4-448A-9D0B-7B57ADF2655C}
      /// </summary>
      public double positionX;
      /// <summary>
      /// The position y{CC2D43FA-BBC4-448A-9D0B-7B57ADF2655C}
      /// </summary>
      public double positionY;
    };

    /// <summary>
    /// 
    /// </summary>
    public struct AccuracyStruct
    {
      /// <summary>
      /// The deviation x left{CC2D43FA-BBC4-448A-9D0B-7B57ADF2655C}
      /// </summary>
      public double deviationXLeft;
      /// <summary>
      /// The deviation y left{CC2D43FA-BBC4-448A-9D0B-7B57ADF2655C}
      /// </summary>
      public double deviationYLeft;
      /// <summary>
      /// The deviation x right{CC2D43FA-BBC4-448A-9D0B-7B57ADF2655C}
      /// </summary>
      public double deviationXRight;
      /// <summary>
      /// The deviation y right{CC2D43FA-BBC4-448A-9D0B-7B57ADF2655C}
      /// </summary>
      public double deviationYRight;
    };

    /// <summary>
    /// 
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct CalibrationStruct
    {
      /// <summary>
      /// The method{CC2D43FA-BBC4-448A-9D0B-7B57ADF2655C}
      /// </summary>
      public int method;
      /// <summary>
      /// The visualization{CC2D43FA-BBC4-448A-9D0B-7B57ADF2655C}
      /// </summary>
      public int visualization;
      /// <summary>
      /// The display device{CC2D43FA-BBC4-448A-9D0B-7B57ADF2655C}
      /// </summary>
      public int displayDevice;
      /// <summary>
      /// The speed{CC2D43FA-BBC4-448A-9D0B-7B57ADF2655C}
      /// </summary>
      public int speed;
      /// <summary>
      /// The automatic accept{CC2D43FA-BBC4-448A-9D0B-7B57ADF2655C}
      /// </summary>
      public int autoAccept;
      /// <summary>
      /// The foreground color{CC2D43FA-BBC4-448A-9D0B-7B57ADF2655C}
      /// </summary>
      public int foregroundColor;
      /// <summary>
      /// The background color{CC2D43FA-BBC4-448A-9D0B-7B57ADF2655C}
      /// </summary>
      public int backgroundColor;
      /// <summary>
      /// The target shape{CC2D43FA-BBC4-448A-9D0B-7B57ADF2655C}
      /// </summary>
      public int targetShape;
      /// <summary>
      /// The target size{CC2D43FA-BBC4-448A-9D0B-7B57ADF2655C}
      /// </summary>
      public int targetSize;
      /// <summary>
      /// The target filename{CC2D43FA-BBC4-448A-9D0B-7B57ADF2655C}
      /// </summary>
      [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
      public string targetFilename;
    };

    /// <summary>
    /// 
    /// </summary>
    public struct StandAloneModeGeometryStruct
    {
      /// <summary>
      /// The setup name{CC2D43FA-BBC4-448A-9D0B-7B57ADF2655C}
      /// </summary>
      [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
      public string setupName;
      /// <summary>
      /// The stim x{CC2D43FA-BBC4-448A-9D0B-7B57ADF2655C}
      /// </summary>
      public int stimX;
      /// <summary>
      /// The stim y{CC2D43FA-BBC4-448A-9D0B-7B57ADF2655C}
      /// </summary>
      public int stimY;
      /// <summary>
      /// The stim height over floor{CC2D43FA-BBC4-448A-9D0B-7B57ADF2655C}
      /// </summary>
      public int stimHeightOverFloor;
      /// <summary>
      /// The red height over floor{CC2D43FA-BBC4-448A-9D0B-7B57ADF2655C}
      /// </summary>
      public int redHeightOverFloor;
      /// <summary>
      /// The red stim dist{CC2D43FA-BBC4-448A-9D0B-7B57ADF2655C}
      /// </summary>
      public int redStimDist;
      /// <summary>
      /// The red incl angle{CC2D43FA-BBC4-448A-9D0B-7B57ADF2655C}
      /// </summary>
      public int redInclAngle;
    };

    /// <summary>
    /// 
    /// </summary>
    public struct MonitorAttachedGeometryStruct
    {
      /// <summary>
      /// The setup name{CC2D43FA-BBC4-448A-9D0B-7B57ADF2655C}
      /// </summary>
      [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
      public string setupName;
      /// <summary>
      /// The stim x{CC2D43FA-BBC4-448A-9D0B-7B57ADF2655C}
      /// </summary>
      public int stimX;
      /// <summary>
      /// The stim y{CC2D43FA-BBC4-448A-9D0B-7B57ADF2655C}
      /// </summary>
      public int stimY;
      /// <summary>
      /// The red stim dist height{CC2D43FA-BBC4-448A-9D0B-7B57ADF2655C}
      /// </summary>
      public int redStimDistHeight;
      /// <summary>
      /// The red stim dist depth{CC2D43FA-BBC4-448A-9D0B-7B57ADF2655C}
      /// </summary>
      public int redStimDistDepth;
      /// <summary>
      /// The red incl angle{CC2D43FA-BBC4-448A-9D0B-7B57ADF2655C}
      /// </summary>
      public int redInclAngle;
    };

    /// <summary>
    /// 
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct ImageStruct
    {
      /// <summary>
      /// The image height{CC2D43FA-BBC4-448A-9D0B-7B57ADF2655C}
      /// </summary>
      public int imageHeight;
      /// <summary>
      /// The image width{CC2D43FA-BBC4-448A-9D0B-7B57ADF2655C}
      /// </summary>
      public int imageWidth;
      /// <summary>
      /// The image size{CC2D43FA-BBC4-448A-9D0B-7B57ADF2655C}
      /// </summary>
      public int imageSize;
      /// <summary>
      /// The image buffer{CC2D43FA-BBC4-448A-9D0B-7B57ADF2655C}
      /// </summary>
      public IntPtr imageBuffer;
    };

    /// <summary>
    /// 
    /// </summary>
    public struct DateStruct
    {
      /// <summary>
      /// The day{CC2D43FA-BBC4-448A-9D0B-7B57ADF2655C}
      /// </summary>
      public int day;
      /// <summary>
      /// The month{CC2D43FA-BBC4-448A-9D0B-7B57ADF2655C}
      /// </summary>
      public int month;
      /// <summary>
      /// The year{CC2D43FA-BBC4-448A-9D0B-7B57ADF2655C}
      /// </summary>
      public int year;
    };

    /// <summary>
    /// 
    /// </summary>
    public struct AOIRectangleStruct
    {
      /// <summary>
      /// The x1{CC2D43FA-BBC4-448A-9D0B-7B57ADF2655C}
      /// </summary>
      public int x1;
      /// <summary>
      /// The x2{CC2D43FA-BBC4-448A-9D0B-7B57ADF2655C}
      /// </summary>
      public int x2;
      /// <summary>
      /// The y1{CC2D43FA-BBC4-448A-9D0B-7B57ADF2655C}
      /// </summary>
      public int y1;
      /// <summary>
      /// The y2{CC2D43FA-BBC4-448A-9D0B-7B57ADF2655C}
      /// </summary>
      public int y2;
    };

    /// <summary>
    /// 
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct AOIStruct
    {
      /// <summary>
      /// The enabled{CC2D43FA-BBC4-448A-9D0B-7B57ADF2655C}
      /// </summary>
      public int enabled;
      /// <summary>
      /// The aoi name{CC2D43FA-BBC4-448A-9D0B-7B57ADF2655C}
      /// </summary>
      [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
      public string aoiName;
      /// <summary>
      /// The aoi group{CC2D43FA-BBC4-448A-9D0B-7B57ADF2655C}
      /// </summary>
      [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
      public string aoiGroup;
      /// <summary>
      /// The position{CC2D43FA-BBC4-448A-9D0B-7B57ADF2655C}
      /// </summary>
      public AOIRectangleStruct position;
      /// <summary>
      /// The fixation hit{CC2D43FA-BBC4-448A-9D0B-7B57ADF2655C}
      /// </summary>
      public int fixationHit;
      /// <summary>
      /// The output value{CC2D43FA-BBC4-448A-9D0B-7B57ADF2655C}
      /// </summary>
      public int outputValue;
      /// <summary>
      /// The output message{CC2D43FA-BBC4-448A-9D0B-7B57ADF2655C}
      /// </summary>
      [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
      public string outputMessage;
      /// <summary>
      /// The eye{CC2D43FA-BBC4-448A-9D0B-7B57ADF2655C}
      /// </summary>
      public char eye;
    };



    // API Function definition. See the manual for further description. 

    /// <summary>
    /// Unmanaged_s the abort calibration.
    /// </summary>
    /// <returns></returns>
    [DllImport("iViewXAPI.dll", EntryPoint = "iV_AbortCalibration")]
    private static extern int Unmanaged_AbortCalibration();

    /// <summary>
    /// Unmanaged_s the accept calibration point.
    /// </summary>
    /// <returns></returns>
    [DllImport("iViewXAPI.dll", EntryPoint = "iV_AcceptCalibrationPoint")]
    private static extern int Unmanaged_AcceptCalibrationPoint();

    /// <summary>
    /// Unmanaged_s the calibrate.
    /// </summary>
    /// <returns></returns>
    [DllImport("iViewXAPI.dll", EntryPoint = "iV_Calibrate")]
    private static extern int Unmanaged_Calibrate();

    /// <summary>
    /// Unmanaged_s the change calibration point.
    /// </summary>
    /// <param name="number">The number.</param>
    /// <param name="positionX">The position x.</param>
    /// <param name="positionY">The position y.</param>
    /// <returns></returns>
    [DllImport("iViewXAPI.dll", EntryPoint = "iV_ChangeCalibrationPoint")]
    private static extern int Unmanaged_ChangeCalibrationPoint(int number, int positionX, int positionY);

    /// <summary>
    /// Unmanaged_s the clear aoi.
    /// </summary>
    /// <returns></returns>
    [DllImport("iViewXAPI.dll", EntryPoint = "iV_ClearAOI")]
    private static extern int Unmanaged_ClearAOI();

    /// <summary>
    /// Unmanaged_s the clear recording buffer.
    /// </summary>
    /// <returns></returns>
    [DllImport("iViewXAPI.dll", EntryPoint = "iV_ClearRecordingBuffer")]
    private static extern int Unmanaged_ClearRecordingBuffer();

    /// <summary>
    /// Unmanaged_s the configure filter.
    /// </summary>
    /// <param name="filter">The filter.</param>
    /// <param name="data">The data.</param>
    /// <returns></returns>
    [DllImport("iViewXAPI.dll", EntryPoint = "iV_ConfigureFilter")]
    private static extern int Unmanaged_ConfigureFilter(int filter, ref byte data);

    /// <summary>
    /// Unmanaged_s the connect.
    /// </summary>
    /// <param name="sendIPAddress">The send ip address.</param>
    /// <param name="sendPort">The send port.</param>
    /// <param name="recvIPAddress">The recv ip address.</param>
    /// <param name="receivePort">The receive port.</param>
    /// <returns></returns>
    [DllImport("iViewXAPI.dll", EntryPoint = "iV_Connect")]
    private static extern int Unmanaged_Connect(StringBuilder sendIPAddress, int sendPort, StringBuilder recvIPAddress, int receivePort);

    /// <summary>
    /// Unmanaged_s the continue eyetracking.
    /// </summary>
    /// <returns></returns>
    [DllImport("iViewXAPI.dll", EntryPoint = "iV_ContinueEyetracking")]
    private static extern int Unmanaged_ContinueEyetracking();

    /// <summary>
    /// Unmanaged_s the continue recording.
    /// </summary>
    /// <param name="etMessage">The et message.</param>
    /// <returns></returns>
    [DllImport("iViewXAPI.dll", EntryPoint = "iV_ContinueRecording")]
    private static extern int Unmanaged_ContinueRecording(StringBuilder etMessage);

    /// <summary>
    /// Unmanaged_s the define aoi.
    /// </summary>
    /// <param name="aoiData">The aoi data.</param>
    /// <returns></returns>
    [DllImport("iViewXAPI.dll", EntryPoint = "iV_DefineAOI")]
    private static extern int Unmanaged_DefineAOI(ref AOIStruct aoiData);

    /// <summary>
    /// Unmanaged_s the define aoi port.
    /// </summary>
    /// <param name="port">The port.</param>
    /// <returns></returns>
    [DllImport("iViewXAPI.dll", EntryPoint = "iV_DefineAOIPort")]
    private static extern int Unmanaged_DefineAOIPort(int port);

    /// <summary>
    /// Unmanaged_s the delete monitor attached geometry.
    /// </summary>
    /// <param name="name">The name.</param>
    /// <returns></returns>
    [DllImport("iViewXAPI.dll", EntryPoint = "iV_DeleteMonitorAttachedGeometry")]
    private static extern int Unmanaged_DeleteMonitorAttachedGeometry(StringBuilder name);

    /// <summary>
    /// Unmanaged_s the delete stand alone geometry.
    /// </summary>
    /// <param name="name">The name.</param>
    /// <returns></returns>
    [DllImport("iViewXAPI.dll", EntryPoint = "iV_DeleteStandAloneGeometry")]
    private static extern int Unmanaged_DeleteStandAloneGeometry(StringBuilder name);

    /// <summary>
    /// Unmanaged_s the disable aoi.
    /// </summary>
    /// <param name="name">The name.</param>
    /// <returns></returns>
    [DllImport("iViewXAPI.dll", EntryPoint = "iV_DisableAOI")]
    private static extern int Unmanaged_DisableAOI(StringBuilder name);

    /// <summary>
    /// Unmanaged_s the disable aoi group.
    /// </summary>
    /// <param name="group">The group.</param>
    /// <returns></returns>
    [DllImport("iViewXAPI.dll", EntryPoint = "iV_DisableAOIGroup")]
    private static extern int Unmanaged_DisableAOIGroup(StringBuilder group);

    /// <summary>
    /// Unmanaged_s the disable gaze data filter.
    /// </summary>
    /// <returns></returns>
    [DllImport("iViewXAPI.dll", EntryPoint = "iV_DisableGazeDataFilter")]
    private static extern int Unmanaged_DisableGazeDataFilter();

    /// <summary>
    /// Unmanaged_s the disable processor high performance mode.
    /// </summary>
    /// <returns></returns>
    [DllImport("iViewXAPI.dll", EntryPoint = "iV_DisableProcessorHighPerformanceMode")]
    private static extern int Unmanaged_DisableProcessorHighPerformanceMode();

    /// <summary>
    /// Unmanaged_s the disconnect.
    /// </summary>
    /// <returns></returns>
    [DllImport("iViewXAPI.dll", EntryPoint = "iV_Disconnect")]
    private static extern int Unmanaged_Disconnect();

    /// <summary>
    /// Unmanaged_s the enable aoi.
    /// </summary>
    /// <param name="name">The name.</param>
    /// <returns></returns>
    [DllImport("iViewXAPI.dll", EntryPoint = "iV_EnableAOI")]
    private static extern int Unmanaged_EnableAOI(StringBuilder name);

    /// <summary>
    /// Unmanaged_s the enable aoi group.
    /// </summary>
    /// <param name="group">The group.</param>
    /// <returns></returns>
    [DllImport("iViewXAPI.dll", EntryPoint = "iV_EnableAOIGroup")]
    private static extern int Unmanaged_EnableAOIGroup(StringBuilder group);

    /// <summary>
    /// Unmanaged_s the enable gaze data filter.
    /// </summary>
    /// <returns></returns>
    [DllImport("iViewXAPI.dll", EntryPoint = "iV_EnableGazeDataFilter")]
    private static extern int Unmanaged_EnableGazeDataFilter();

    /// <summary>
    /// Unmanaged_s the enable processor high performance mode.
    /// </summary>
    /// <returns></returns>
    [DllImport("iViewXAPI.dll", EntryPoint = "iV_EnableProcessorHighPerformanceMode")]
    private static extern int Unmanaged_EnableProcessorHighPerformanceMode();

    /// <summary>
    /// Unmanaged_s the get accuracy.
    /// </summary>
    /// <param name="accuracyData">The accuracy data.</param>
    /// <param name="visualization">The visualization.</param>
    /// <returns></returns>
    [DllImport("iViewXAPI.dll", EntryPoint = "iV_GetAccuracy")]
    private static extern int Unmanaged_GetAccuracy(ref AccuracyStruct accuracyData, int visualization);

    /// <summary>
    /// Unmanaged_s the get accuracy image.
    /// </summary>
    /// <param name="imageData">The image data.</param>
    /// <returns></returns>
    [DllImport("iViewXAPI.dll", EntryPoint = "iV_GetAccuracyImage")]
    private static extern int Unmanaged_GetAccuracyImage(ref ImageStruct imageData);

    /// <summary>
    /// Unmanaged_s the get aoi output value.
    /// </summary>
    /// <param name="aoiOutputValue">The aoi output value.</param>
    /// <returns></returns>
    [DllImport("iViewXAPI.dll", EntryPoint = "iV_GetAOIOutputValue")]
    private static extern int Unmanaged_GetAOIOutputValue(ref int aoiOutputValue);

    /// <summary>
    /// Unmanaged_s the get calibration parameter.
    /// </summary>
    /// <param name="calibrationData">The calibration data.</param>
    /// <returns></returns>
    [DllImport("iViewXAPI.dll", EntryPoint = "iV_GetCalibrationParameter")]
    private static extern int Unmanaged_GetCalibrationParameter(ref CalibrationStruct calibrationData);

    /// <summary>
    /// Unmanaged_s the get calibration point.
    /// </summary>
    /// <param name="calibrationPointNumber">The calibration point number.</param>
    /// <param name="calibrationPoint">The calibration point.</param>
    /// <returns></returns>
    [DllImport("iViewXAPI.dll", EntryPoint = "iV_GetCalibrationPoint")]
    private static extern int Unmanaged_GetCalibrationPoint(int calibrationPointNumber, ref CalibrationPointStruct calibrationPoint);

    /// <summary>
    /// Unmanaged_s the get current calibration point.
    /// </summary>
    /// <param name="actualCalibrationPoint">The actual calibration point.</param>
    /// <returns></returns>
    [DllImport("iViewXAPI.dll", EntryPoint = "iV_GetCurrentCalibrationPoint")]
    private static extern int Unmanaged_GetCurrentCalibrationPoint(ref CalibrationPointStruct actualCalibrationPoint);

    /// <summary>
    /// Unmanaged_s the get current timestamp.
    /// </summary>
    /// <param name="currentTimestamp">The current timestamp.</param>
    /// <returns></returns>
    [DllImport("iViewXAPI.dll", EntryPoint = "iV_GetCurrentTimestamp")]
    private static extern int Unmanaged_GetCurrentTimestamp(ref Int64 currentTimestamp);

    /// <summary>
    /// Unmanaged_s the get event.
    /// </summary>
    /// <param name="eventDataSample">The event data sample.</param>
    /// <returns></returns>
    [DllImport("iViewXAPI.dll", EntryPoint = "iV_GetEvent")]
    private static extern int Unmanaged_GetEvent(ref EventStruct eventDataSample);

    /// <summary>
    /// Unmanaged_s the get eye image.
    /// </summary>
    /// <param name="imageData">The image data.</param>
    /// <returns></returns>
    [DllImport("iViewXAPI.dll", EntryPoint = "iV_GetEyeImage")]
    private static extern int Unmanaged_GetEyeImage(ref ImageStruct imageData);

    /// <summary>
    /// Unmanaged_s the get feature key.
    /// </summary>
    /// <param name="featureKey">The feature key.</param>
    /// <returns></returns>
    [DllImport("iViewXAPI.dll", EntryPoint = "iV_GetFeatureKey")]
    private static extern int Unmanaged_GetFeatureKey(ref Int64 featureKey);

    /// <summary>
    /// Unmanaged_s the get geometry profiles.
    /// </summary>
    /// <param name="maxSize">The maximum size.</param>
    /// <param name="profileNames">The profile names.</param>
    /// <returns></returns>
    [DllImport("iViewXAPI.dll", EntryPoint = "iV_GetGeometryProfiles")]
    private static extern int Unmanaged_GetGeometryProfiles(int maxSize, ref StringBuilder profileNames);

    /// <summary>
    /// Unmanaged_s the get license due date.
    /// </summary>
    /// <param name="licenseDueDate">The license due date.</param>
    /// <returns></returns>
    [DllImport("iViewXAPI.dll", EntryPoint = "iV_GetLicenseDueDate")]
    private static extern int Unmanaged_GetLicenseDueDate(ref DateStruct licenseDueDate);

    /// <summary>
    /// Unmanaged_s the get monitor attached geometry.
    /// </summary>
    /// <param name="profileName">Name of the profile.</param>
    /// <param name="monitorAttachedGeometry">The monitor attached geometry.</param>
    /// <returns></returns>
    [DllImport("iViewXAPI.dll", EntryPoint = "iV_GetMonitorAttachedGeometry")]
    private static extern int Unmanaged_GetMonitorAttachedGeometry(StringBuilder profileName, ref MonitorAttachedGeometryStruct monitorAttachedGeometry);

    /// <summary>
    /// Unmanaged_s the get sample.
    /// </summary>
    /// <param name="rawDataSample">The raw data sample.</param>
    /// <returns></returns>
    [DllImport("iViewXAPI.dll", EntryPoint = "iV_GetSample")]
    private static extern int Unmanaged_GetSample(ref SampleStruct rawDataSample);

    /// <summary>
    /// Unmanaged_s the get scene video.
    /// </summary>
    /// <param name="imageData">The image data.</param>
    /// <returns></returns>
    [DllImport("iViewXAPI.dll", EntryPoint = "iV_GetSceneVideo")]
    private static extern int Unmanaged_GetSceneVideo(ref ImageStruct imageData);

    /// <summary>
    /// Unmanaged_s the get serial number.
    /// </summary>
    /// <param name="serialNumber">The serial number.</param>
    /// <returns></returns>
    [DllImport("iViewXAPI.dll", EntryPoint = "iV_GetSerialNumber")]
    private static extern int Unmanaged_GetSerialNumber(ref StringBuilder serialNumber);

    /// <summary>
    /// Unmanaged_s the get system information.
    /// </summary>
    /// <param name="systemInfoData">The system information data.</param>
    /// <returns></returns>
    [DllImport("iViewXAPI.dll", EntryPoint = "iV_GetSystemInfo")]
    private static extern int Unmanaged_GetSystemInfo(ref SystemInfoStruct systemInfoData);

    /// <summary>
    /// Unmanaged_s the get tracking monitor.
    /// </summary>
    /// <param name="imageData">The image data.</param>
    /// <returns></returns>
    [DllImport("iViewXAPI.dll", EntryPoint = "iV_GetTrackingMonitor")]
    private static extern int Unmanaged_GetTrackingMonitor(ref ImageStruct imageData);

    /// <summary>
    /// Unmanaged_s the is connected.
    /// </summary>
    /// <returns></returns>
    [DllImport("iViewXAPI.dll", EntryPoint = "iV_IsConnected")]
    private static extern int Unmanaged_IsConnected();

    /// <summary>
    /// Unmanaged_s the load calibration.
    /// </summary>
    /// <param name="name">The name.</param>
    /// <returns></returns>
    [DllImport("iViewXAPI.dll", EntryPoint = "iV_LoadCalibration")]
    private static extern int Unmanaged_LoadCalibration(StringBuilder name);

    /// <summary>
    /// Unmanaged_s the log.
    /// </summary>
    /// <param name="logMessage">The log message.</param>
    /// <returns></returns>
    [DllImport("iViewXAPI.dll", EntryPoint = "iV_Log")]
    private static extern int Unmanaged_Log(StringBuilder logMessage);

    /// <summary>
    /// Unmanaged_s the pause eyetracking.
    /// </summary>
    /// <returns></returns>
    [DllImport("iViewXAPI.dll", EntryPoint = "iV_PauseEyetracking")]
    private static extern int Unmanaged_PauseEyetracking();

    /// <summary>
    /// Unmanaged_s the pause recording.
    /// </summary>
    /// <returns></returns>
    [DllImport("iViewXAPI.dll", EntryPoint = "iV_PauseRecording")]
    private static extern int Unmanaged_PauseRecording();

    /// <summary>
    /// Unmanaged_s the quit.
    /// </summary>
    /// <returns></returns>
    [DllImport("iViewXAPI.dll", EntryPoint = "iV_Quit")]
    private static extern int Unmanaged_Quit();

    /// <summary>
    /// Unmanaged_s the release aoi port.
    /// </summary>
    /// <returns></returns>
    [DllImport("iViewXAPI.dll", EntryPoint = "iV_ReleaseAOIPort")]
    private static extern int Unmanaged_ReleaseAOIPort();

    /// <summary>
    /// Unmanaged_s the remove aoi.
    /// </summary>
    /// <param name="aoiName">Name of the aoi.</param>
    /// <returns></returns>
    [DllImport("iViewXAPI.dll", EntryPoint = "iV_RemoveAOI")]
    private static extern int Unmanaged_RemoveAOI(StringBuilder aoiName);

    /// <summary>
    /// Unmanaged_s the reset calibration points.
    /// </summary>
    /// <returns></returns>
    [DllImport("iViewXAPI.dll", EntryPoint = "iV_ResetCalibrationPoints")]
    private static extern int Unmanaged_ResetCalibrationPoints();

    /// <summary>
    /// Unmanaged_s the save calibration.
    /// </summary>
    /// <param name="aoiName">Name of the aoi.</param>
    /// <returns></returns>
    [DllImport("iViewXAPI.dll", EntryPoint = "iV_SaveCalibration")]
    private static extern int Unmanaged_SaveCalibration(StringBuilder aoiName);

    /// <summary>
    /// Unmanaged_s the save data.
    /// </summary>
    /// <param name="filename">The filename.</param>
    /// <param name="description">The description.</param>
    /// <param name="user">The user.</param>
    /// <param name="overwrite">The overwrite.</param>
    /// <returns></returns>
    [DllImport("iViewXAPI.dll", EntryPoint = "iV_SaveData")]
    private static extern int Unmanaged_SaveData(StringBuilder filename, StringBuilder description, StringBuilder user, int overwrite);

    /// <summary>
    /// Unmanaged_s the send command.
    /// </summary>
    /// <param name="etMessage">The et message.</param>
    /// <returns></returns>
    [DllImport("iViewXAPI.dll", EntryPoint = "iV_SendCommand")]
    private static extern int Unmanaged_SendCommand(StringBuilder etMessage);

    /// <summary>
    /// Unmanaged_s the send image message.
    /// </summary>
    /// <param name="etMessage">The et message.</param>
    /// <returns></returns>
    [DllImport("iViewXAPI.dll", EntryPoint = "iV_SendImageMessage")]
    private static extern int Unmanaged_SendImageMessage(StringBuilder etMessage);

    /// <summary>
    /// Unmanaged_s the set calibration callback.
    /// </summary>
    /// <param name="calibrationCallbackFunction">The calibration callback function.</param>
    [DllImport("iViewXAPI.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "iV_SetCalibrationCallback")]
    private static extern void Unmanaged_SetCalibrationCallback(MulticastDelegate calibrationCallbackFunction);

    /// <summary>
    /// Unmanaged_s the set connection timeout.
    /// </summary>
    /// <param name="time">The time.</param>
    /// <returns></returns>
    [DllImport("iViewXAPI.dll", EntryPoint = "iV_SetConnectionTimeout")]
    private static extern int Unmanaged_SetConnectionTimeout(int time);

    /// <summary>
    /// Unmanaged_s the set geometry profile.
    /// </summary>
    /// <param name="profileName">Name of the profile.</param>
    /// <returns></returns>
    [DllImport("iViewXAPI.dll", EntryPoint = "iV_SetGeometryProfile")]
    private static extern int Unmanaged_SetGeometryProfile(StringBuilder profileName);

    /// <summary>
    /// Unmanaged_s the set resolution.
    /// </summary>
    /// <param name="stimulusWidth">Width of the stimulus.</param>
    /// <param name="stimulusHeight">Height of the stimulus.</param>
    /// <returns></returns>
    [DllImport("iViewXAPI.dll", EntryPoint = "iV_SetResolution")]
    private static extern int Unmanaged_SetResolution(int stimulusWidth, int stimulusHeight);

    /// <summary>
    /// Unmanaged_s the set event callback.
    /// </summary>
    /// <param name="eventCallbackFunction">The event callback function.</param>
    [DllImport("iViewXAPI.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "iV_SetEventCallback")]
    private static extern void Unmanaged_SetEventCallback(MulticastDelegate eventCallbackFunction);

    /// <summary>
    /// Unmanaged_s the set event detection parameter.
    /// </summary>
    /// <param name="minDuration">The minimum duration.</param>
    /// <param name="maxDispersion">The maximum dispersion.</param>
    /// <returns></returns>
    [DllImport("iViewXAPI.dll", EntryPoint = "iV_SetEventDetectionParameter")]
    private static extern int Unmanaged_SetEventDetectionParameter(int minDuration, int maxDispersion);

    /// <summary>
    /// Unmanaged_s the set eye image callback.
    /// </summary>
    /// <param name="eyeImageCallbackFunction">The eye image callback function.</param>
    [DllImport("iViewXAPI.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "iV_SetEyeImageCallback")]
    private static extern void Unmanaged_SetEyeImageCallback(MulticastDelegate eyeImageCallbackFunction);

    /// <summary>
    /// Unmanaged_s the set license.
    /// </summary>
    /// <param name="licenseKey">The license key.</param>
    /// <returns></returns>
    [DllImport("iViewXAPI.dll", EntryPoint = "iV_SetLicense")]
    private static extern int Unmanaged_SetLicense(StringBuilder licenseKey);

    /// <summary>
    /// Unmanaged_s the set logger.
    /// </summary>
    /// <param name="logLevel">The log level.</param>
    /// <param name="filename">The filename.</param>
    /// <returns></returns>
    [DllImport("iViewXAPI.dll", EntryPoint = "iV_SetLogger")]
    private static extern int Unmanaged_SetLogger(int logLevel, StringBuilder filename);

    /// <summary>
    /// Unmanaged_s the set sample callback.
    /// </summary>
    /// <param name="sampleCallbackFunction">The sample callback function.</param>
    [DllImport("iViewXAPI.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "iV_SetSampleCallback")]
    private static extern void Unmanaged_SetSampleCallback(MulticastDelegate sampleCallbackFunction);

    /// <summary>
    /// Unmanaged_s the set scene video callback.
    /// </summary>
    /// <param name="sceneVideoCallbackFunction">The scene video callback function.</param>
    [DllImport("iViewXAPI.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "iV_SetSceneVideoCallback")]
    private static extern void Unmanaged_SetSceneVideoCallback(MulticastDelegate sceneVideoCallbackFunction);

    /// <summary>
    /// Unmanaged_s the set tracking monitor callback.
    /// </summary>
    /// <param name="trackingMonitorCallbackFunction">The tracking monitor callback function.</param>
    [DllImport("iViewXAPI.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "iV_SetTrackingMonitorCallback")]
    private static extern void Unmanaged_SetTrackingMonitorCallback(MulticastDelegate trackingMonitorCallbackFunction);

    /// <summary>
    /// Unmanaged_s the set tracking parameter.
    /// </summary>
    /// <param name="ET_PARAM_EYE">The e t_ para m_ eye.</param>
    /// <param name="ET_PARAM">The e t_ parameter.</param>
    /// <param name="value">The value.</param>
    /// <returns></returns>
    [DllImport("iViewXAPI.dll", EntryPoint = "iV_SetTrackingParameter")]
    private static extern int Unmanaged_SetTrackingParameter(int ET_PARAM_EYE, int ET_PARAM, int value);

    /// <summary>
    /// Unmanaged_s the setup calibration.
    /// </summary>
    /// <param name="calibrationData">The calibration data.</param>
    /// <returns></returns>
    [DllImport("iViewXAPI.dll", EntryPoint = "iV_SetupCalibration")]
    private static extern int Unmanaged_SetupCalibration(ref CalibrationStruct calibrationData);

    /// <summary>
    /// Unmanaged_s the setup monitor attached geometry.
    /// </summary>
    /// <param name="monitorAttachedGeometry">The monitor attached geometry.</param>
    /// <returns></returns>
    [DllImport("iViewXAPI.dll", EntryPoint = "iV_SetupMonitorAttachedGeometry")]
    private static extern int Unmanaged_SetupMonitorAttachedGeometry(ref MonitorAttachedGeometryStruct monitorAttachedGeometry);

    /// <summary>
    /// Unmanaged_s the setup stand alone mode.
    /// </summary>
    /// <param name="standAloneModeGeometry">The stand alone mode geometry.</param>
    /// <returns></returns>
    [DllImport("iViewXAPI.dll", EntryPoint = "iV_SetupStandAloneMode")]
    private static extern int Unmanaged_SetupStandAloneMode(ref StandAloneModeGeometryStruct standAloneModeGeometry);

    /// <summary>
    /// Unmanaged_s the show accuracy monitor.
    /// </summary>
    /// <returns></returns>
    [DllImport("iViewXAPI.dll", EntryPoint = "iV_ShowAccuracyMonitor")]
    private static extern int Unmanaged_ShowAccuracyMonitor();

    /// <summary>
    /// Unmanaged_s the show eye image monitor.
    /// </summary>
    /// <returns></returns>
    [DllImport("iViewXAPI.dll", EntryPoint = "iV_ShowEyeImageMonitor")]
    private static extern int Unmanaged_ShowEyeImageMonitor();

    /// <summary>
    /// Unmanaged_s the show scene video monitor.
    /// </summary>
    /// <returns></returns>
    [DllImport("iViewXAPI.dll", EntryPoint = "iV_ShowSceneVideoMonitor")]
    private static extern int Unmanaged_ShowSceneVideoMonitor();

    /// <summary>
    /// Unmanaged_s the show tracking monitor.
    /// </summary>
    /// <returns></returns>
    [DllImport("iViewXAPI.dll", EntryPoint = "iV_ShowTrackingMonitor")]
    private static extern int Unmanaged_ShowTrackingMonitor();

    /// <summary>
    /// Unmanaged_s the start.
    /// </summary>
    /// <param name="etApplication">The et application.</param>
    /// <returns></returns>
    [DllImport("iViewXAPI.dll", EntryPoint = "iV_Start")]
    private static extern int Unmanaged_Start(int etApplication);

    /// <summary>
    /// Unmanaged_s the start recording.
    /// </summary>
    /// <returns></returns>
    [DllImport("iViewXAPI.dll", EntryPoint = "iV_StartRecording")]
    private static extern int Unmanaged_StartRecording();

    /// <summary>
    /// Unmanaged_s the stop recording.
    /// </summary>
    /// <returns></returns>
    [DllImport("iViewXAPI.dll", EntryPoint = "iV_StopRecording")]
    private static extern int Unmanaged_StopRecording();

    /// <summary>
    /// Unmanaged_s the test TTL.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns></returns>
    [DllImport("iViewXAPI.dll", EntryPoint = "iV_TestTTL")]
    private static extern int Unmanaged_TestTTL(long value);

    /// <summary>
    /// Unmanaged_s the validate.
    /// </summary>
    /// <returns></returns>
    [DllImport("iViewXAPI.dll", EntryPoint = "iV_Validate")]
    private static extern int Unmanaged_Validate();






    /// <summary>
    /// Is the v_ abort calibration.
    /// </summary>
    /// <returns></returns>
    public int iV_AbortCalibration()
    {
      return Unmanaged_AbortCalibration();
    }

    /// <summary>
    /// Is the v_ accept calibration point.
    /// </summary>
    /// <returns></returns>
    public int iV_AcceptCalibrationPoint()
    {
      return Unmanaged_AcceptCalibrationPoint();
    }

    /// <summary>
    /// Is the v_ calibrate.
    /// </summary>
    /// <returns></returns>
    public int iV_Calibrate()
    {
      return Unmanaged_Calibrate();
    }

    /// <summary>
    /// Is the v_ change calibration point.
    /// </summary>
    /// <param name="number">The number.</param>
    /// <param name="positionX">The position x.</param>
    /// <param name="positionY">The position y.</param>
    /// <returns></returns>
    public int iV_ChangeCalibrationPoint(int number, int positionX, int positionY)
    {
      return Unmanaged_ChangeCalibrationPoint(number, positionX, positionY);
    }

    /// <summary>
    /// Is the v_ clear aoi.
    /// </summary>
    /// <returns></returns>
    public int iV_ClearAOI()
    {
      return Unmanaged_ClearAOI();
    }

    /// <summary>
    /// Is the v_ clear recording buffer.
    /// </summary>
    /// <returns></returns>
    public int iV_ClearRecordingBuffer()
    {
      return Unmanaged_ClearRecordingBuffer();
    }

    /// <summary>
    /// Is the v_ configure filter.
    /// </summary>
    /// <param name="filter">The filter.</param>
    /// <param name="data">The data.</param>
    /// <returns></returns>
    public int iV_ConfigureFilter(int filter, ref byte data)
    {
      return Unmanaged_ConfigureFilter(filter, ref data);
    }

    /// <summary>
    /// Is the v_ connect.
    /// </summary>
    /// <param name="sendIP">The send ip.</param>
    /// <param name="sendPort">The send port.</param>
    /// <param name="receiveIP">The receive ip.</param>
    /// <param name="receivePort">The receive port.</param>
    /// <returns></returns>
    public int iV_Connect(StringBuilder sendIP, int sendPort, StringBuilder receiveIP, int receivePort)
    {
      return Unmanaged_Connect(sendIP, sendPort, receiveIP, receivePort);
    }

    /// <summary>
    /// Is the v_ continue eyetracking.
    /// </summary>
    /// <returns></returns>
    public int iV_ContinueEyetracking()
    {
      return Unmanaged_ContinueEyetracking();
    }

    /// <summary>
    /// Is the v_ continue recording.
    /// </summary>
    /// <param name="trialname">The trialname.</param>
    /// <returns></returns>
    public int iV_ContinueRecording(StringBuilder trialname)
    {
      return Unmanaged_ContinueRecording(trialname);
    }

    /// <summary>
    /// Is the v_ define aoi.
    /// </summary>
    /// <param name="aoi">The aoi.</param>
    /// <returns></returns>
    public int iV_DefineAOI(ref AOIStruct aoi)
    {
      return Unmanaged_DefineAOI(ref aoi);
    }

    /// <summary>
    /// Is the v_ define aoi port.
    /// </summary>
    /// <param name="port">The port.</param>
    /// <returns></returns>
    public int iV_DefineAOIPort(int port)
    {
      return Unmanaged_DefineAOIPort(port);
    }

    /// <summary>
    /// Is the v_ delete monitor attached geometry.
    /// </summary>
    /// <param name="name">The name.</param>
    /// <returns></returns>
    public int iV_DeleteMonitorAttachedGeometry(StringBuilder name)
    {
      return Unmanaged_DeleteMonitorAttachedGeometry(name);
    }

    /// <summary>
    /// Is the v_ delete stand alone geometry.
    /// </summary>
    /// <param name="name">The name.</param>
    /// <returns></returns>
    public int iV_DeleteStandAloneGeometry(StringBuilder name)
    {
      return Unmanaged_DeleteStandAloneGeometry(name);
    }

    /// <summary>
    /// Is the v_ disable aoi.
    /// </summary>
    /// <param name="aoiName">Name of the aoi.</param>
    /// <returns></returns>
    public int iV_DisableAOI(StringBuilder aoiName)
    {
      return Unmanaged_DisableAOI(aoiName);
    }

    /// <summary>
    /// Is the v_ disable aoi group.
    /// </summary>
    /// <param name="aoiGroup">The aoi group.</param>
    /// <returns></returns>
    public int iV_DisableAOIGroup(StringBuilder aoiGroup)
    {
      return Unmanaged_DisableAOIGroup(aoiGroup);
    }

    /// <summary>
    /// Is the v_ disable gaze data filter.
    /// </summary>
    /// <returns></returns>
    public int iV_DisableGazeDataFilter()
    {
      return Unmanaged_DisableGazeDataFilter();
    }

    /// <summary>
    /// Is the v_ disable processor high performance mode.
    /// </summary>
    /// <returns></returns>
    public int iV_DisableProcessorHighPerformanceMode()
    {
      return Unmanaged_DisableProcessorHighPerformanceMode();
    }

    /// <summary>
    /// Is the v_ disconnect.
    /// </summary>
    /// <returns></returns>
    public int iV_Disconnect()
    {
      return Unmanaged_Disconnect();
    }

    /// <summary>
    /// Is the v_ enable aoi.
    /// </summary>
    /// <param name="aoiName">Name of the aoi.</param>
    /// <returns></returns>
    public int iV_EnableAOI(StringBuilder aoiName)
    {
      return Unmanaged_EnableAOI(aoiName);
    }

    /// <summary>
    /// Is the v_ enable aoi group.
    /// </summary>
    /// <param name="aoiGroup">The aoi group.</param>
    /// <returns></returns>
    public int iV_EnableAOIGroup(StringBuilder aoiGroup)
    {
      return Unmanaged_EnableAOIGroup(aoiGroup);
    }

    /// <summary>
    /// Is the v_ enable gaze data filter.
    /// </summary>
    /// <returns></returns>
    public int iV_EnableGazeDataFilter()
    {
      return Unmanaged_EnableGazeDataFilter();
    }

    /// <summary>
    /// Is the v_ enable processor high performance mode.
    /// </summary>
    /// <returns></returns>
    public int iV_EnableProcessorHighPerformanceMode()
    {
      return Unmanaged_EnableProcessorHighPerformanceMode();
    }

    /// <summary>
    /// Is the v_ get accuracy.
    /// </summary>
    /// <param name="accuracyData">The accuracy data.</param>
    /// <param name="visualization">The visualization.</param>
    /// <returns></returns>
    public int iV_GetAccuracy(ref AccuracyStruct accuracyData, int visualization)
    {
      return Unmanaged_GetAccuracy(ref accuracyData, visualization);
    }

    /// <summary>
    /// Is the v_ get accuracy image.
    /// </summary>
    /// <param name="image">The image.</param>
    /// <returns></returns>
    public int iV_GetAccuracyImage(ref ImageStruct image)
    {
      return Unmanaged_GetAccuracyImage(ref image);
    }

    /// <summary>
    /// Is the v_ get aoi output value.
    /// </summary>
    /// <param name="aoiOutputValue">The aoi output value.</param>
    /// <returns></returns>
    public int iV_GetAOIOutputValue(ref int aoiOutputValue)
    {
      return Unmanaged_GetAOIOutputValue(ref aoiOutputValue);
    }

    /// <summary>
    /// Is the v_ get calibration parameter.
    /// </summary>
    /// <param name="calibrationData">The calibration data.</param>
    /// <returns></returns>
    public int iV_GetCalibrationParameter(ref CalibrationStruct calibrationData)
    {
      return Unmanaged_GetCalibrationParameter(ref calibrationData);
    }

    /// <summary>
    /// Is the v_ get calibration point.
    /// </summary>
    /// <param name="calibrationPointNumber">The calibration point number.</param>
    /// <param name="calibrationPoint">The calibration point.</param>
    /// <returns></returns>
    public int iV_GetCalibrationPoint(int calibrationPointNumber, ref CalibrationPointStruct calibrationPoint)
    {
      return Unmanaged_GetCalibrationPoint(calibrationPointNumber, ref calibrationPoint);
    }

    /// <summary>
    /// Is the v_ get current calibration point.
    /// </summary>
    /// <param name="currentCalibrationPoint">The current calibration point.</param>
    /// <returns></returns>
    public int iV_GetCurrentCalibrationPoint(ref CalibrationPointStruct currentCalibrationPoint)
    {
      return Unmanaged_GetCurrentCalibrationPoint(ref currentCalibrationPoint);
    }

    /// <summary>
    /// Is the v_ get current timestamp.
    /// </summary>
    /// <param name="currentTimestamp">The current timestamp.</param>
    /// <returns></returns>
    public int iV_GetCurrentTimestamp(ref Int64 currentTimestamp)
    {
      return Unmanaged_GetCurrentTimestamp(ref currentTimestamp);
    }

    /// <summary>
    /// Is the v_ get event.
    /// </summary>
    /// <param name="eventDataSample">The event data sample.</param>
    /// <returns></returns>
    public int iV_GetEvent(ref EventStruct eventDataSample)
    {
      return Unmanaged_GetEvent(ref eventDataSample);
    }

    /// <summary>
    /// Is the v_ get eye image.
    /// </summary>
    /// <param name="image">The image.</param>
    /// <returns></returns>
    public int iV_GetEyeImage(ref ImageStruct image)
    {
      return Unmanaged_GetEyeImage(ref image);
    }

    /// <summary>
    /// Is the v_ get feature key.
    /// </summary>
    /// <param name="featureKey">The feature key.</param>
    /// <returns></returns>
    public int iV_GetFeatureKey(ref Int64 featureKey)
    {
      return Unmanaged_GetFeatureKey(ref featureKey);
    }

    /// <summary>
    /// Is the v_ get geometry profiles.
    /// </summary>
    /// <param name="maxSize">The maximum size.</param>
    /// <param name="profileNames">The profile names.</param>
    /// <returns></returns>
    public int iV_GetGeometryProfiles(int maxSize, ref StringBuilder profileNames)
    {
      return Unmanaged_GetGeometryProfiles(maxSize, ref profileNames);
    }

    /// <summary>
    /// Is the v_ get license due date.
    /// </summary>
    /// <param name="licenseDueDate">The license due date.</param>
    /// <returns></returns>
    public int iV_GetLicenseDueDate(ref DateStruct licenseDueDate)
    {
      return Unmanaged_GetLicenseDueDate(ref licenseDueDate);
    }

    /// <summary>
    /// Is the v_ get monitor attached geometry.
    /// </summary>
    /// <param name="profileName">Name of the profile.</param>
    /// <param name="monitorAttachedGeometry">The monitor attached geometry.</param>
    /// <returns></returns>
    public int iV_GetMonitorAttachedGeometry(StringBuilder profileName, ref MonitorAttachedGeometryStruct monitorAttachedGeometry)
    {
      return Unmanaged_GetMonitorAttachedGeometry(profileName, ref monitorAttachedGeometry);
    }

    /// <summary>
    /// Is the v_ get sample.
    /// </summary>
    /// <param name="rawDataSample">The raw data sample.</param>
    /// <returns></returns>
    public int iV_GetSample(ref SampleStruct rawDataSample)
    {
      return Unmanaged_GetSample(ref rawDataSample);
    }

    /// <summary>
    /// Is the v_ get scene video.
    /// </summary>
    /// <param name="image">The image.</param>
    /// <returns></returns>
    public int iV_GetSceneVideo(ref ImageStruct image)
    {
      return Unmanaged_GetSceneVideo(ref image);
    }

    /// <summary>
    /// Is the v_ get serial number.
    /// </summary>
    /// <param name="serialNumber">The serial number.</param>
    /// <returns></returns>
    public int iV_GetSerialNumber(ref StringBuilder serialNumber)
    {
      return Unmanaged_GetSerialNumber(ref serialNumber);
    }

    /// <summary>
    /// Is the v_ get system information.
    /// </summary>
    /// <param name="systemInfo">The system information.</param>
    /// <returns></returns>
    public int iV_GetSystemInfo(ref SystemInfoStruct systemInfo)
    {
      return Unmanaged_GetSystemInfo(ref systemInfo);
    }

    /// <summary>
    /// Is the v_ get tracking monitor.
    /// </summary>
    /// <param name="image">The image.</param>
    /// <returns></returns>
    public int iV_GetTrackingMonitor(ref ImageStruct image)
    {
      return Unmanaged_GetTrackingMonitor(ref image);
    }

    /// <summary>
    /// Is the v_ is connected.
    /// </summary>
    /// <returns></returns>
    public int iV_IsConnected()
    {
      return Unmanaged_IsConnected();
    }

    /// <summary>
    /// Is the v_ load calibration.
    /// </summary>
    /// <param name="name">The name.</param>
    /// <returns></returns>
    public int iV_LoadCalibration(StringBuilder name)
    {
      return Unmanaged_LoadCalibration(name);
    }

    /// <summary>
    /// Is the v_ log.
    /// </summary>
    /// <param name="message">The message.</param>
    /// <returns></returns>
    public int iV_Log(StringBuilder message)
    {
      return Unmanaged_Log(message);
    }

    /// <summary>
    /// Is the v_ pause eyetracking.
    /// </summary>
    /// <returns></returns>
    public int iV_PauseEyetracking()
    {
      return Unmanaged_PauseEyetracking();
    }

    /// <summary>
    /// Is the v_ pause recording.
    /// </summary>
    /// <returns></returns>
    public int iV_PauseRecording()
    {
      return Unmanaged_PauseRecording();
    }

    /// <summary>
    /// Is the v_ quit.
    /// </summary>
    /// <returns></returns>
    public int iV_Quit()
    {
      return Unmanaged_Quit();
    }

    /// <summary>
    /// Is the v_ release aoi port.
    /// </summary>
    /// <returns></returns>
    public int iV_ReleaseAOIPort()
    {
      return Unmanaged_ReleaseAOIPort();
    }
    /// <summary>
    /// Is the v_ remove aoi.
    /// </summary>
    /// <param name="aoiName">Name of the aoi.</param>
    /// <returns></returns>
    public int iV_RemoveAOI(StringBuilder aoiName)
    {
      return Unmanaged_RemoveAOI(aoiName);
    }

    /// <summary>
    /// Is the v_ reset calibration points.
    /// </summary>
    /// <returns></returns>
    public int iV_ResetCalibrationPoints()
    {
      return Unmanaged_ResetCalibrationPoints();
    }

    /// <summary>
    /// Is the v_ save calibration.
    /// </summary>
    /// <param name="name">The name.</param>
    /// <returns></returns>
    public int iV_SaveCalibration(StringBuilder name)
    {
      return Unmanaged_SaveCalibration(name);
    }

    /// <summary>
    /// Is the v_ save data.
    /// </summary>
    /// <param name="filename">The filename.</param>
    /// <param name="description">The description.</param>
    /// <param name="user">The user.</param>
    /// <param name="overwrite">The overwrite.</param>
    /// <returns></returns>
    public int iV_SaveData(StringBuilder filename, StringBuilder description, StringBuilder user, int overwrite)
    {
      return Unmanaged_SaveData(filename, description, user, overwrite);
    }

    /// <summary>
    /// Is the v_ send command.
    /// </summary>
    /// <param name="etMessage">The et message.</param>
    /// <returns></returns>
    public int iV_SendCommand(StringBuilder etMessage)
    {
      return Unmanaged_SendCommand(etMessage);
    }

    /// <summary>
    /// Is the v_ send image message.
    /// </summary>
    /// <param name="message">The message.</param>
    /// <returns></returns>
    public int iV_SendImageMessage(StringBuilder message)
    {
      return Unmanaged_SendImageMessage(message);
    }

    /// <summary>
    /// Is the v_ set calibration callback.
    /// </summary>
    /// <param name="calibrationCallback">The calibration callback.</param>
    public void iV_SetCalibrationCallback(MulticastDelegate calibrationCallback)
    {
      Unmanaged_SetCalibrationCallback(calibrationCallback);
    }

    /// <summary>
    /// Is the v_ set connection timeout.
    /// </summary>
    /// <param name="time">The time.</param>
    public void iV_SetConnectionTimeout(int time)
    {
      Unmanaged_SetConnectionTimeout(time);
    }

    /// <summary>
    /// Is the v_ set geometry profile.
    /// </summary>
    /// <param name="profileName">Name of the profile.</param>
    public void iV_SetGeometryProfile(StringBuilder profileName)
    {
      Unmanaged_SetGeometryProfile(profileName);
    }

    /// <summary>
    /// Is the v_ set resolution.
    /// </summary>
    /// <param name="stimulusWidth">Width of the stimulus.</param>
    /// <param name="stimulusHeight">Height of the stimulus.</param>
    public void iV_SetResolution(int stimulusWidth, int stimulusHeight)
    {
      Unmanaged_SetResolution(stimulusWidth, stimulusHeight);
    }

    /// <summary>
    /// Is the v_ set event callback.
    /// </summary>
    /// <param name="eventCallback">The event callback.</param>
    public void iV_SetEventCallback(MulticastDelegate eventCallback)
    {
      Unmanaged_SetEventCallback(eventCallback);
    }

    /// <summary>
    /// Is the v_ set event detection parameter.
    /// </summary>
    /// <param name="minDuration">The minimum duration.</param>
    /// <param name="maxDispersion">The maximum dispersion.</param>
    /// <returns></returns>
    public int iV_SetEventDetectionParameter(int minDuration, int maxDispersion)
    {
      return Unmanaged_SetEventDetectionParameter(minDuration, maxDispersion);
    }

    /// <summary>
    /// Is the v_ set eye image callback.
    /// </summary>
    /// <param name="eyeImageCallback">The eye image callback.</param>
    public void iV_SetEyeImageCallback(MulticastDelegate eyeImageCallback)
    {
      Unmanaged_SetEyeImageCallback(eyeImageCallback);
    }

    /// <summary>
    /// Is the v_ set license.
    /// </summary>
    /// <param name="key">The key.</param>
    /// <returns></returns>
    public int iV_SetLicense(StringBuilder key)
    {
      return Unmanaged_SetLicense(key);
    }

    /// <summary>
    /// Is the v_ set logger.
    /// </summary>
    /// <param name="logLevel">The log level.</param>
    /// <param name="filename">The filename.</param>
    /// <returns></returns>
    public int iV_SetLogger(int logLevel, StringBuilder filename)
    {
      return Unmanaged_SetLogger(logLevel, filename);
    }

    /// <summary>
    /// Is the v_ set sample callback.
    /// </summary>
    /// <param name="sampleCallback">The sample callback.</param>
    public void iV_SetSampleCallback(MulticastDelegate sampleCallback)
    {
      Unmanaged_SetSampleCallback(sampleCallback);
    }

    /// <summary>
    /// Is the v_ set scene video callback.
    /// </summary>
    /// <param name="sceneVideoCallback">The scene video callback.</param>
    public void iV_SetSceneVideoCallback(MulticastDelegate sceneVideoCallback)
    {
      Unmanaged_SetSceneVideoCallback(sceneVideoCallback);
    }

    /// <summary>
    /// Is the v_ set tracking monitor callback.
    /// </summary>
    /// <param name="trackingMonitorCallback">The tracking monitor callback.</param>
    public void iV_SetTrackingMonitorCallback(MulticastDelegate trackingMonitorCallback)
    {
      Unmanaged_SetTrackingMonitorCallback(trackingMonitorCallback);
    }

    /// <summary>
    /// Is the v_ set tracking parameter.
    /// </summary>
    /// <param name="ET_PARAM_EYE">The e t_ para m_ eye.</param>
    /// <param name="ET_PARAM">The e t_ parameter.</param>
    /// <param name="value">The value.</param>
    public void iV_SetTrackingParameter(int ET_PARAM_EYE, int ET_PARAM, int value)
    {
      Unmanaged_SetTrackingParameter(ET_PARAM_EYE, ET_PARAM, value);
    }

    /// <summary>
    /// Is the v_ setup calibration.
    /// </summary>
    /// <param name="calibrationData">The calibration data.</param>
    /// <returns></returns>
    public int iV_SetupCalibration(ref CalibrationStruct calibrationData)
    {
      return Unmanaged_SetupCalibration(ref calibrationData);
    }

    /// <summary>
    /// Is the v_ setup monitor attached geometry.
    /// </summary>
    /// <param name="monitorAttachedGeometry">The monitor attached geometry.</param>
    /// <returns></returns>
    public int iV_SetupMonitorAttachedGeometry(ref MonitorAttachedGeometryStruct monitorAttachedGeometry)
    {
      return Unmanaged_SetupMonitorAttachedGeometry(ref monitorAttachedGeometry);
    }

    /// <summary>
    /// Is the v_ setup stand alone mode.
    /// </summary>
    /// <param name="standAloneModeGeometry">The stand alone mode geometry.</param>
    /// <returns></returns>
    public int iV_SetupStandAloneMode(ref StandAloneModeGeometryStruct standAloneModeGeometry)
    {
      return Unmanaged_SetupStandAloneMode(ref standAloneModeGeometry);
    }

    /// <summary>
    /// Is the v_ show accuracy monitor.
    /// </summary>
    /// <returns></returns>
    public int iV_ShowAccuracyMonitor()
    {
      return Unmanaged_ShowAccuracyMonitor();
    }

    /// <summary>
    /// Is the v_ show eye image monitor.
    /// </summary>
    /// <returns></returns>
    public int iV_ShowEyeImageMonitor()
    {
      return Unmanaged_ShowEyeImageMonitor();
    }
    /// <summary>
    /// Is the v_ show scene video monitor.
    /// </summary>
    /// <returns></returns>
    public int iV_ShowSceneVideoMonitor()
    {
      return Unmanaged_ShowSceneVideoMonitor();
    }
    /// <summary>
    /// Is the v_ show tracking monitor.
    /// </summary>
    /// <returns></returns>
    public int iV_ShowTrackingMonitor()
    {
      return Unmanaged_ShowTrackingMonitor();
    }

    /// <summary>
    /// Is the v_ start.
    /// </summary>
    /// <param name="etApplication">The et application.</param>
    /// <returns></returns>
    public int iV_Start(int etApplication)
    {
      return Unmanaged_Start(etApplication);
    }

    /// <summary>
    /// Is the v_ start recording.
    /// </summary>
    /// <returns></returns>
    public int iV_StartRecording()
    {
      return Unmanaged_StartRecording();
    }

    /// <summary>
    /// Is the v_ stop recording.
    /// </summary>
    /// <returns></returns>
    public int iV_StopRecording()
    {
      return Unmanaged_StopRecording();
    }

    /// <summary>
    /// Is the v_ test TTL.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns></returns>
    public int iV_TestTTL(int value)
    {
      return Unmanaged_TestTTL(value);
    }

    /// <summary>
    /// Is the v_ validate.
    /// </summary>
    /// <returns></returns>
    public int iV_Validate()
    {
      return Unmanaged_Validate();
    }

  }
}
