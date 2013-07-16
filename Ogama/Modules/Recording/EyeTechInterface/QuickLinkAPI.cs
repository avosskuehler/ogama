// --------------------------------------------------------------------------------------------------------------------
// <copyright file="QuickLinkAPI.cs" company="">
//   
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Ogama.Modules.Recording.EyeTechInterface
{
  using System;
  using System.Runtime.InteropServices;

  using Microsoft.Win32;

  /// <summary>
  /// The native constants.
  /// </summary>
  public class NativeConstants
  {
    #region Constants

    /// <summary>
    /// The quic k_ lin k_ api.
    /// </summary>
    public const string QUICK_LINK_API = "__declspec(dllimport)";

    /// <summary>
    /// The quic k_ lin k_ ap i_ h.
    /// </summary>
    public const string QUICK_LINK_API_H = "";

    /// <summary>
    /// The quic k_ lin k_ cal l_ conven.
    /// </summary>
    public const string QUICK_LINK_CALL_CONVEN = "__stdcall";

    #endregion
  }

  /// <summary>
  /// The calibration error ex.
  /// </summary>
  public enum CalibrationErrorEx
  {
    /// <summary>
    /// The calibratione x_ ok.
    /// </summary>
    CALIBRATIONEX_OK = 0, 

    /// <summary>
    /// The calibratione x_ n o_ ey e_ selected.
    /// </summary>
    CALIBRATIONEX_NO_EYE_SELECTED = 1, 

    /// <summary>
    /// The calibratione x_ no t_ initialized.
    /// </summary>
    CALIBRATIONEX_NOT_INITIALIZED = 2, 

    /// <summary>
    /// The calibratione x_ n o_ ne w_ targets.
    /// </summary>
    CALIBRATIONEX_NO_NEW_TARGETS = 3, 

    /// <summary>
    /// The calibratione x_ invali d_ targe t_ handle.
    /// </summary>
    CALIBRATIONEX_INVALID_TARGET_HANDLE = 4, 

    /// <summary>
    /// The calibratione x_ righ t_ ey e_ no t_ found.
    /// </summary>
    CALIBRATIONEX_RIGHT_EYE_NOT_FOUND = 5, 

    /// <summary>
    /// The calibratione x_ lef t_ ey e_ no t_ found.
    /// </summary>
    CALIBRATIONEX_LEFT_EYE_NOT_FOUND = 6, 

    /// <summary>
    /// The calibratione x_ n o_ ey e_ found.
    /// </summary>
    CALIBRATIONEX_NO_EYE_FOUND = 7, 

    /// <summary>
    /// The calibratione x_ no t_ al l_ target s_ calibrated.
    /// </summary>
    CALIBRATIONEX_NOT_ALL_TARGETS_CALIBRATED = 8, 

    /// <summary>
    /// The calibratione x_ invali d_ index.
    /// </summary>
    CALIBRATIONEX_INVALID_INDEX = 9, 

    /// <summary>
    /// The calibratione x_ interna l_ timeout.
    /// </summary>
    CALIBRATIONEX_INTERNAL_TIMEOUT = 10, 
  }

  /// <summary>
  /// The calibration style.
  /// </summary>
  public enum CalibrationStyle
  {
    /// <summary>
    /// The ca l_ styl e_5_ point.
    /// </summary>
    CAL_STYLE_5_POINT = 0, 

    /// <summary>
    /// The ca l_ styl e_9_ point.
    /// </summary>
    CAL_STYLE_9_POINT = 1, 

    /// <summary>
    /// The ca l_ styl e_16_ point.
    /// </summary>
    CAL_STYLE_16_POINT = 2, 
  }

  /// <summary>
  /// The click method.
  /// </summary>
  public enum ClickMethod
  {
    /// <summary>
    /// The clic k_ metho d_ none.
    /// </summary>
    CLICK_METHOD_NONE = 0, 

    /// <summary>
    /// The clic k_ metho d_ blink.
    /// </summary>
    CLICK_METHOD_BLINK = 1, 

    /// <summary>
    /// The clic k_ metho d_ dwell.
    /// </summary>
    CLICK_METHOD_DWELL = 2, 
  }

  /// <summary>
  /// The eyes to process.
  /// </summary>
  public enum EyesToProcess
  {
    /// <summary>
    /// The eye s_ t o_ pro c_ singl e_ left.
    /// </summary>
    EYES_TO_PROC_SINGLE_LEFT = 0, 

    /// <summary>
    /// The eye s_ t o_ pro c_ singl e_ right.
    /// </summary>
    EYES_TO_PROC_SINGLE_RIGHT = 1, 

    /// <summary>
    /// The eye s_ t o_ pro c_ dua l_ lef t_ o r_ right.
    /// </summary>
    EYES_TO_PROC_DUAL_LEFT_OR_RIGHT = 2, 

    /// <summary>
    /// The eye s_ t o_ pro c_ dua l_ lef t_ an d_ right.
    /// </summary>
    EYES_TO_PROC_DUAL_LEFT_AND_RIGHT = 3, 
  }

  /// <summary>
  /// The process priority.
  /// </summary>
  public enum ProcessPriority
  {
    /// <summary>
    /// The pro c_ priorit y_0.
    /// </summary>
    PROC_PRIORITY_0 = 0, 

    /// <summary>
    /// The pro c_ priorit y_1.
    /// </summary>
    PROC_PRIORITY_1 = 1, 

    /// <summary>
    /// The pro c_ priorit y_2.
    /// </summary>
    PROC_PRIORITY_2 = 2, 

    /// <summary>
    /// The pro c_ priorit y_3.
    /// </summary>
    PROC_PRIORITY_3 = 3, 
  }

  /// <summary>
  /// The camera gain method.
  /// </summary>
  public enum CameraGainMethod
  {
    /// <summary>
    /// The ca m_ gai n_ metho d_ auto.
    /// </summary>
    CAM_GAIN_METHOD_AUTO = 0, 

    /// <summary>
    /// The ca m_ gai n_ metho d_ manual.
    /// </summary>
    CAM_GAIN_METHOD_MANUAL = 1, 
  }

  /// <summary>
  /// The camera gpio output.
  /// </summary>
  public enum CameraGPIOOutput
  {
    /// <summary>
    /// The ca m_ gpi o_ ou t_ custom.
    /// </summary>
    CAM_GPIO_OUT_CUSTOM = 0, 

    /// <summary>
    /// The ca m_ gpi o_ ou t_ lef t_ trackin g_ status.
    /// </summary>
    CAM_GPIO_OUT_LEFT_TRACKING_STATUS = 1, 

    /// <summary>
    /// The ca m_ gpi o_ ou t_ righ t_ trackin g_ status.
    /// </summary>
    CAM_GPIO_OUT_RIGHT_TRACKING_STATUS = 2, 
  }

  /// <summary>
  /// The camera custom gpio output value.
  /// </summary>
  public enum CameraCustomGPIOOutputValue
  {
    /// <summary>
    /// The ca m_ gpi o_ ou t_ valu e_ n o_ change.
    /// </summary>
    CAM_GPIO_OUT_VALUE_NO_CHANGE = 0, 

    /// <summary>
    /// The ca m_ gpi o_ ou t_ valu e_ high.
    /// </summary>
    CAM_GPIO_OUT_VALUE_HIGH = 1, 

    /// <summary>
    /// The ca m_ gpi o_ ou t_ valu e_ low.
    /// </summary>
    CAM_GPIO_OUT_VALUE_LOW = 2, 
  }

  /// <summary>
  /// The tool bar image display.
  /// </summary>
  public enum ToolBarImageDisplay
  {
    /// <summary>
    /// The im g_ dis p_ liv e_ image.
    /// </summary>
    IMG_DISP_LIVE_IMAGE = 0, 

    /// <summary>
    /// The im g_ dis p_ pseud o_ image.
    /// </summary>
    IMG_DISP_PSEUDO_IMAGE = 1, 
  }

  /// <summary>
  /// The qg window state.
  /// </summary>
  public enum QGWindowState
  {
    /// <summary>
    /// The qgw s_ home.
    /// </summary>
    QGWS_HOME = 0, 

    /// <summary>
    /// The qgw s_ ey e_ tools.
    /// </summary>
    QGWS_EYE_TOOLS = 1, 

    /// <summary>
    /// The qgw s_ ey e_ tool s_ image.
    /// </summary>
    QGWS_EYE_TOOLS_IMAGE = 2, 

    /// <summary>
    /// The qgw s_ hiden.
    /// </summary>
    QGWS_HIDEN = 3, 
  }

  /// <summary>
  /// The d point.
  /// </summary>
  [StructLayout(LayoutKind.Sequential)]
  public struct DPoint
  {
    /// <summary>
    /// The x.
    /// </summary>
    public double x;

    /// <summary>
    /// The y.
    /// </summary>
    public double y;
  }

  /// <summary>
  /// The l point.
  /// </summary>
  [StructLayout(LayoutKind.Sequential)]
  public struct LPoint
  {
    /// <summary>
    /// The x.
    /// </summary>
    public int x;

    /// <summary>
    /// The y.
    /// </summary>
    public int y;
  }

  /// <summary>
  /// The eye data.
  /// </summary>
  [StructLayout(LayoutKind.Sequential)]
  public struct EyeData
  {
    /// <summary>
    /// The pupil.
    /// </summary>
    public DPoint Pupil;

    /// <summary>
    /// The glint 1.
    /// </summary>
    public DPoint Glint1;

    /// <summary>
    /// The glint 2.
    /// </summary>
    public DPoint Glint2;

    /// <summary>
    /// The gaze point.
    /// </summary>
    public LPoint GazePoint;

    /// <summary>
    /// The pupil diameter.
    /// </summary>
    public double PupilDiameter;

    /// <summary>
    /// The found.
    /// </summary>
    [MarshalAs(UnmanagedType.I1)]
    public bool Found;

    /// <summary>
    /// The calibrated.
    /// </summary>
    [MarshalAs(UnmanagedType.I1)]
    public bool Calibrated;
  }

  /// <summary>
  /// The image data.
  /// </summary>
  [StructLayout(LayoutKind.Sequential)]
  public struct ImageData
  {
    /// <summary>
    /// The time.
    /// </summary>
    public double Time;

    /// <summary>
    /// The left eye.
    /// </summary>
    public EyeData LeftEye;

    /// <summary>
    /// The right eye.
    /// </summary>
    public EyeData RightEye;

    /// <summary>
    /// The pixel data.
    /// </summary>
    /// unsigned char*
    [MarshalAs(UnmanagedType.LPStr)]
    public string PixelData;

    /// <summary>
    /// The width.
    /// </summary>
    public int Width;

    /// <summary>
    /// The height.
    /// </summary>
    public int Height;
  }

  /// <summary>
  /// The gp collection data.
  /// </summary>
  [StructLayout(LayoutKind.Sequential)]
  public struct GPCollectionData
  {
    /// <summary>
    /// The x.
    /// </summary>
    public int X;

    /// <summary>
    /// The y.
    /// </summary>
    public int Y;

    /// <summary>
    /// The time.
    /// </summary>
    public double Time;
  }

  /// <summary>
  /// The clicking options.
  /// </summary>
  [StructLayout(LayoutKind.Sequential)]
  public struct ClickingOptions
  {
    /// <summary>
    /// The click_ audible feedback.
    /// </summary>
    [MarshalAs(UnmanagedType.I1)]
    public bool Click_AudibleFeedback;

    /// <summary>
    /// The click_ delay.
    /// </summary>
    public uint Click_Delay;

    /// <summary>
    /// The click_ zoom factor.
    /// </summary>
    public uint Click_ZoomFactor;

    /// <summary>
    /// The click_ method.
    /// </summary>
    public ClickMethod Click_Method;

    /// <summary>
    /// The blink_ primary time.
    /// </summary>
    public uint Blink_PrimaryTime;

    /// <summary>
    /// The blink_ secondary time.
    /// </summary>
    public uint Blink_SecondaryTime;

    /// <summary>
    /// The blink_ cancel time.
    /// </summary>
    public uint Blink_CancelTime;

    /// <summary>
    /// The blink_ enable secondary click.
    /// </summary>
    [MarshalAs(UnmanagedType.I1)]
    public bool Blink_EnableSecondaryClick;

    /// <summary>
    /// The blink_ both eyes required.
    /// </summary>
    [MarshalAs(UnmanagedType.I1)]
    public bool Blink_BothEyesRequired;

    /// <summary>
    /// The blink_ visual feedback.
    /// </summary>
    [MarshalAs(UnmanagedType.I1)]
    public bool Blink_VisualFeedback;

    /// <summary>
    /// The dwell_ box size.
    /// </summary>
    public uint Dwell_BoxSize;

    /// <summary>
    /// The dwell_ time.
    /// </summary>
    public uint Dwell_Time;
  }

  /// <summary>
  /// The calibration options.
  /// </summary>
  [StructLayout(LayoutKind.Sequential)]
  public struct CalibrationOptions
  {
    /// <summary>
    /// The calibration_ target time.
    /// </summary>
    public int Calibration_TargetTime;

    /// <summary>
    /// The calibration_ style.
    /// </summary>
    public CalibrationStyle Calibration_Style;
  }

  /// <summary>
  /// The processing options.
  /// </summary>
  [StructLayout(LayoutKind.Sequential)]
  public struct ProcessingOptions
  {
    /// <summary>
    /// The processing_ smoothing factor.
    /// </summary>
    /// unsigned int
    public uint Processing_SmoothingFactor;

    /// <summary>
    /// The processing_ eyes to process.
    /// </summary>
    /// EyesToProcess-&gt;Anonymous_d66d7daa_4dcd_4947_8d42_2619a0ab89cc
    public EyesToProcess Processing_EyesToProcess;

    /// <summary>
    /// The processing_ enable capture.
    /// </summary>
    /// boolean
    [MarshalAs(UnmanagedType.I1)]
    public bool Processing_EnableCapture;

    /// <summary>
    /// The processing_ enable processing.
    /// </summary>
    /// boolean
    [MarshalAs(UnmanagedType.I1)]
    public bool Processing_EnableProcessing;

    /// <summary>
    /// The processing_ enable display.
    /// </summary>
    /// boolean
    [MarshalAs(UnmanagedType.I1)]
    public bool Processing_EnableDisplay;

    /// <summary>
    /// The processing_ enable cursor movement.
    /// </summary>
    /// boolean
    [MarshalAs(UnmanagedType.I1)]
    public bool Processing_EnableCursorMovement;

    /// <summary>
    /// The processing_ enable clicking.
    /// </summary>
    /// boolean
    [MarshalAs(UnmanagedType.I1)]
    public bool Processing_EnableClicking;

    /// <summary>
    /// The processing_ max process time.
    /// </summary>
    /// unsigned int
    public uint Processing_MaxProcessTime;

    /// <summary>
    /// The processing_ process priority.
    /// </summary>
    /// ProcessPriority-&gt;Anonymous_c7988c45_ec73_49c7_b042_dce7dbe5365b
    public ProcessPriority Processing_ProcessPriority;
  }

  /// <summary>
  /// The camera options.
  /// </summary>
  [StructLayout(LayoutKind.Sequential)]
  public struct CameraOptions
  {
    /// <summary>
    /// The camera_ bus bandwidth percentage.
    /// </summary>
    /// unsigned int
    public uint Camera_BusBandwidthPercentage;

    /// <summary>
    /// The camera_ image roi percentage.
    /// </summary>
    /// unsigned int
    public uint Camera_ImageROIPercentage;

    /// <summary>
    /// The camera_ gain method.
    /// </summary>
    /// CameraGainMethod-&gt;Anonymous_fe97665f_6c0c_4e10_aff8_91e1f8486280
    public CameraGainMethod Camera_GainMethod;

    /// <summary>
    /// The camera_ gain value.
    /// </summary>
    /// double
    public double Camera_GainValue;

    /// <summary>
    /// The camera_ gpi o_1.
    /// </summary>
    /// CameraGPIOOutput-&gt;Anonymous_6a49a8f4_4411_4793_b3a6_4cf1727cfeae
    public CameraGPIOOutput Camera_GPIO_1;

    /// <summary>
    /// The camera_ gpi o_2.
    /// </summary>
    /// CameraGPIOOutput-&gt;Anonymous_6a49a8f4_4411_4793_b3a6_4cf1727cfeae
    public CameraGPIOOutput Camera_GPIO_2;

    /// <summary>
    /// The camera_ gpi o_3.
    /// </summary>
    /// CameraGPIOOutput-&gt;Anonymous_6a49a8f4_4411_4793_b3a6_4cf1727cfeae
    public CameraGPIOOutput Camera_GPIO_3;
  }

  /// <summary>
  /// The toolbar options.
  /// </summary>
  [StructLayout(LayoutKind.Sequential)]
  public struct ToolbarOptions
  {
    /// <summary>
    /// The toolbar_ button size x.
    /// </summary>
    /// unsigned int
    public uint Toolbar_ButtonSizeX;

    /// <summary>
    /// The toolbar_ button size y.
    /// </summary>
    /// unsigned int
    public uint Toolbar_ButtonSizeY;

    /// <summary>
    /// The toolbar_ image display type.
    /// </summary>
    /// ToolBarImageDisplay-&gt;Anonymous_bf0318f8_0c65_4d63_8607_49f233abe625
    public ToolBarImageDisplay Toolbar_ImageDisplayType;
  }

  /// <summary>
  /// The native methods.
  /// </summary>
  public class NativeMethods
  {
    // QuickLinkAPI.dll location
    /// <summary>
    /// The library location.
    /// </summary>
    private string libraryLocation = string.Empty;

    // Function looks in the registry if the API is installed on the current system.
    // "QuickLinkAPI.dll" -> this.libraryLocation
    // Seems to be useless due to the fact that attributes can't be changed as variables

    /// <summary>
    /// The init native methods.
    /// </summary>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public bool InitNativeMethods()
    {
      RegistryKey key = Registry.LocalMachine.OpenSubKey(@"Software\EyeTech Digital Systems\QuickLinkAPI", false);
      if (key != null)
      {
        this.libraryLocation = (string)key.GetValue("Path");
      }
      else
      {
        return false;
      }

      return true;
    }

    /// <summary>
    /// The get qg on flag.
    /// </summary>
    /// Return Type: boolean
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    [DllImport("QuickLinkAPI.dll", EntryPoint = "GetQGOnFlag", CallingConvention = CallingConvention.StdCall)]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool GetQGOnFlag();

    /// <summary>
    /// The exit quick glance.
    /// </summary>
    /// Return Type: void
    [DllImport("QuickLinkAPI.dll", EntryPoint = "ExitQuickGlance", CallingConvention = CallingConvention.StdCall)]
    public static extern void ExitQuickGlance();

    /// <summary>
    /// The set enable eye control.
    /// </summary>
    /// Return Type: void
    [DllImport("QuickLinkAPI.dll", EntryPoint = "SetEnableEyeControl", CallingConvention = CallingConvention.StdCall)]
    public static extern void SetEnableEyeControl();

    /// <summary>
    /// The reset enable eye control.
    /// </summary>
    /// Return Type: void
    [DllImport("QuickLinkAPI.dll", EntryPoint = "ResetEnableEyeControl", CallingConvention = CallingConvention.StdCall)]
    public static extern void ResetEnableEyeControl();

    /// <summary>
    /// The set eye control.
    /// </summary>
    /// <param name="Enable">
    /// The Enable.
    /// </param>
    /// Return Type: void
    /// Enable: boolean
    [DllImport("QuickLinkAPI.dll", EntryPoint = "SetEyeControl", CallingConvention = CallingConvention.StdCall)]
    public static extern void SetEyeControl([MarshalAs(UnmanagedType.I1)] bool Enable);

    /// <summary>
    /// The get eye control.
    /// </summary>
    /// Return Type: boolean
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    [DllImport("QuickLinkAPI.dll", EntryPoint = "GetEyeControl", CallingConvention = CallingConvention.StdCall)]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool GetEyeControl();

    /// <summary>
    /// The set mw state.
    /// </summary>
    /// <param name="MWState">
    /// The MW State.
    /// </param>
    /// Return Type: void
    /// MWState: QGWindowState-&gt;Anonymous_cd0c0d4e_f20f_4897_905c_1dd89fe6e816
    [DllImport("QuickLinkAPI.dll", EntryPoint = "SetMWState", CallingConvention = CallingConvention.StdCall)]
    public static extern void SetMWState(QGWindowState MWState);

    /// <summary>
    /// The toggle large image.
    /// </summary>
    /// Return Type: void
    [DllImport("QuickLinkAPI.dll", EntryPoint = "ToggleLargeImage", CallingConvention = CallingConvention.StdCall)]
    public static extern void ToggleLargeImage();

    /// <summary>
    /// The show large image.
    /// </summary>
    /// Return Type: void
    [DllImport("QuickLinkAPI.dll", EntryPoint = "ShowLargeImage", CallingConvention = CallingConvention.StdCall)]
    public static extern void ShowLargeImage();

    /// <summary>
    /// The hide large image.
    /// </summary>
    /// Return Type: void
    [DllImport("QuickLinkAPI.dll", EntryPoint = "HideLargeImage", CallingConvention = CallingConvention.StdCall)]
    public static extern void HideLargeImage();

    /// <summary>
    /// The move left right.
    /// </summary>
    /// Return Type: void
    [DllImport("QuickLinkAPI.dll", EntryPoint = "MoveLeftRight", CallingConvention = CallingConvention.StdCall)]
    public static extern void MoveLeftRight();

    /// <summary>
    /// The move up down.
    /// </summary>
    /// Return Type: void
    [DllImport("QuickLinkAPI.dll", EntryPoint = "MoveUpDown", CallingConvention = CallingConvention.StdCall)]
    public static extern void MoveUpDown();

    /// <summary>
    /// The get sdk version.
    /// </summary>
    /// Return Type: double
    /// <returns>
    /// The <see cref="double"/>.
    /// </returns>
    [DllImport("QuickLinkAPI.dll", EntryPoint = "GetSDKVersion", CallingConvention = CallingConvention.StdCall)]
    public static extern double GetSDKVersion();

    /// <summary>
    /// The start raw capture.
    /// </summary>
    /// <param name="CopyImage">
    /// The Copy Image.
    /// </param>
    /// Return Type: void
    /// CopyImage: boolean
    [DllImport("QuickLinkAPI.dll", EntryPoint = "StartRawCapture", CallingConvention = CallingConvention.StdCall)]
    public static extern void StartRawCapture([MarshalAs(UnmanagedType.I1)] bool CopyImage);

    /// <summary>
    /// The stop raw capture.
    /// </summary>
    /// Return Type: void
    [DllImport("QuickLinkAPI.dll", EntryPoint = "StopRawCapture", CallingConvention = CallingConvention.StdCall)]
    public static extern void StopRawCapture();

    /// <summary>
    /// The set copy image flag.
    /// </summary>
    /// <param name="CopyImage">
    /// The Copy Image.
    /// </param>
    /// Return Type: void
    /// CopyImage: boolean
    [DllImport("QuickLinkAPI.dll", EntryPoint = "SetCopyImageFlag", CallingConvention = CallingConvention.StdCall)]
    public static extern void SetCopyImageFlag([MarshalAs(UnmanagedType.I1)] bool CopyImage);

    /// <summary>
    /// The get image data.
    /// </summary>
    /// <param name="MaxTimeout">
    /// The Max Timeout.
    /// </param>
    /// <param name="Data">
    /// The Data.
    /// </param>
    /// Return Type: boolean
    /// MaxTimeout: unsigned int
    /// Data: ImageData*
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    [DllImport("QuickLinkAPI.dll", EntryPoint = "GetImageData", CallingConvention = CallingConvention.StdCall)]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool GetImageData(uint MaxTimeout, ref ImageData Data);

    /// <summary>
    /// The get image data and latency.
    /// </summary>
    /// <param name="MaxTimeout">
    /// The Max Timeout.
    /// </param>
    /// <param name="Data">
    /// The Data.
    /// </param>
    /// <param name="Latency">
    /// The Latency.
    /// </param>
    /// Return Type: boolean
    /// MaxTimeout: unsigned int
    /// Data: ImageData*
    /// Latency: double*
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    [DllImport("QuickLinkAPI.dll", EntryPoint = "GetImageDataAndLatency", CallingConvention = CallingConvention.StdCall)
    ]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool GetImageDataAndLatency(uint MaxTimeout, ref ImageData Data, ref double Latency);

    /// <summary>
    /// The start bulk capture.
    /// </summary>
    /// <param name="NumPoints">
    /// The Num Points.
    /// </param>
    /// Return Type: void
    /// NumPoints: unsigned int
    [DllImport("QuickLinkAPI.dll", EntryPoint = "StartBulkCapture", CallingConvention = CallingConvention.StdCall)]
    public static extern void StartBulkCapture(uint NumPoints);

    /// <summary>
    /// The stop bulk capture.
    /// </summary>
    /// Return Type: void
    [DllImport("QuickLinkAPI.dll", EntryPoint = "StopBulkCapture", CallingConvention = CallingConvention.StdCall)]
    public static extern void StopBulkCapture();

    /// <summary>
    /// The query bulk capture.
    /// </summary>
    /// <param name="NumCaptured">
    /// The Num Captured.
    /// </param>
    /// <param name="NumNotCaptured">
    /// The Num Not Captured.
    /// </param>
    /// <param name="NumRead">
    /// The Num Read.
    /// </param>
    /// <param name="NumNotRead">
    /// The Num Not Read.
    /// </param>
    /// <param name="Capturing">
    /// The Capturing.
    /// </param>
    /// Return Type: boolean
    /// NumCaptured: unsigned int*
    /// NumNotCaptured: unsigned int*
    /// NumRead: unsigned int*
    /// NumNotRead: unsigned int*
    /// Capturing: boolean*
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    [DllImport("QuickLinkAPI.dll", EntryPoint = "QueryBulkCapture", CallingConvention = CallingConvention.StdCall)]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool QueryBulkCapture(
      ref uint NumCaptured, ref uint NumNotCaptured, ref uint NumRead, ref uint NumNotRead, ref bool Capturing);

    /// <summary>
    /// The read bulk capture.
    /// </summary>
    /// <param name="MaxTimeout">
    /// The Max Timeout.
    /// </param>
    /// <param name="Data">
    /// The Data.
    /// </param>
    /// Return Type: boolean
    /// MaxTimeout: unsigned int
    /// Data: ImageData*
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    [DllImport("QuickLinkAPI.dll", EntryPoint = "ReadBulkCapture", CallingConvention = CallingConvention.StdCall)]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool ReadBulkCapture(uint MaxTimeout, ref ImageData Data);

    /// <summary>
    /// The internal calibration 1.
    /// </summary>
    /// Return Type: void
    [DllImport("QuickLinkAPI.dll", EntryPoint = "InternalCalibration1", CallingConvention = CallingConvention.StdCall)]
    public static extern void InternalCalibration1();

    /// <summary>
    /// The initialize calibration ex.
    /// </summary>
    /// <param name="CalibrationIndex">
    /// The Calibration Index.
    /// </param>
    /// Return Type: CalibrationErrorEx-&gt;Anonymous_f0d31251_262e_4684_a5ef_268e4cdfa4aa
    /// CalibrationIndex: unsigned int
    /// <returns>
    /// The <see cref="CalibrationErrorEx"/>.
    /// </returns>
    [DllImport("QuickLinkAPI.dll", EntryPoint = "InitializeCalibrationEx", CallingConvention = CallingConvention.StdCall
      )]
    public static extern CalibrationErrorEx InitializeCalibrationEx(uint CalibrationIndex);

    /// <summary>
    /// The get new target position ex.
    /// </summary>
    /// <param name="X">
    /// The X.
    /// </param>
    /// <param name="Y">
    /// The Y.
    /// </param>
    /// <param name="TargetHandle">
    /// The Target Handle.
    /// </param>
    /// Return Type: CalibrationErrorEx-&gt;Anonymous_f0d31251_262e_4684_a5ef_268e4cdfa4aa
    /// X: int*
    /// Y: int*
    /// TargetHandle: int*
    /// <returns>
    /// The <see cref="CalibrationErrorEx"/>.
    /// </returns>
    [DllImport("QuickLinkAPI.dll", EntryPoint = "GetNewTargetPositionEx", CallingConvention = CallingConvention.StdCall)
    ]
    public static extern CalibrationErrorEx GetNewTargetPositionEx(ref int X, ref int Y, ref int TargetHandle);

    /// <summary>
    /// The get worst target position ex.
    /// </summary>
    /// <param name="X">
    /// The X.
    /// </param>
    /// <param name="Y">
    /// The Y.
    /// </param>
    /// <param name="TargetHandle">
    /// The Target Handle.
    /// </param>
    /// Return Type: CalibrationErrorEx-&gt;Anonymous_f0d31251_262e_4684_a5ef_268e4cdfa4aa
    /// X: int*
    /// Y: int*
    /// TargetHandle: int*
    /// <returns>
    /// The <see cref="CalibrationErrorEx"/>.
    /// </returns>
    [DllImport("QuickLinkAPI.dll", EntryPoint = "GetWorstTargetPositionEx", 
      CallingConvention = CallingConvention.StdCall)]
    public static extern CalibrationErrorEx GetWorstTargetPositionEx(ref int X, ref int Y, ref int TargetHandle);

    /// <summary>
    /// The calibrate ex.
    /// </summary>
    /// <param name="TargetHandle">
    /// The Target Handle.
    /// </param>
    /// Return Type: CalibrationErrorEx-&gt;Anonymous_f0d31251_262e_4684_a5ef_268e4cdfa4aa
    /// TargetHandle: int
    /// <returns>
    /// The <see cref="CalibrationErrorEx"/>.
    /// </returns>
    [DllImport("QuickLinkAPI.dll", EntryPoint = "CalibrateEx", CallingConvention = CallingConvention.StdCall)]
    public static extern CalibrationErrorEx CalibrateEx(int TargetHandle);

    /// <summary>
    /// The get score ex.
    /// </summary>
    /// <param name="left">
    /// The left.
    /// </param>
    /// <param name="right">
    /// The right.
    /// </param>
    /// Return Type: CalibrationErrorEx-&gt;Anonymous_f0d31251_262e_4684_a5ef_268e4cdfa4aa
    /// left: double*
    /// right: double*
    /// <returns>
    /// The <see cref="CalibrationErrorEx"/>.
    /// </returns>
    [DllImport("QuickLinkAPI.dll", EntryPoint = "GetScoreEx", CallingConvention = CallingConvention.StdCall)]
    public static extern CalibrationErrorEx GetScoreEx(ref double left, ref double right);

    /// <summary>
    /// The apply calibration ex.
    /// </summary>
    /// Return Type: CalibrationErrorEx-&gt;Anonymous_f0d31251_262e_4684_a5ef_268e4cdfa4aa
    /// <returns>
    /// The <see cref="CalibrationErrorEx"/>.
    /// </returns>
    [DllImport("QuickLinkAPI.dll", EntryPoint = "ApplyCalibrationEx", CallingConvention = CallingConvention.StdCall)]
    public static extern CalibrationErrorEx ApplyCalibrationEx();

    /// <summary>
    /// The calibration bias ex.
    /// </summary>
    /// <param name="DeltaX">
    /// The Delta X.
    /// </param>
    /// <param name="DeltaY">
    /// The Delta Y.
    /// </param>
    /// Return Type: void
    /// DeltaX: int
    /// DeltaY: int
    [DllImport("QuickLinkAPI.dll", EntryPoint = "CalibrationBiasEx", CallingConvention = CallingConvention.StdCall)]
    public static extern void CalibrationBiasEx(int DeltaX, int DeltaY);

    /// <summary>
    /// The open calibration ex.
    /// </summary>
    /// <param name="CalibrationIndex">
    /// The Calibration Index.
    /// </param>
    /// Return Type: CalibrationErrorEx-&gt;Anonymous_f0d31251_262e_4684_a5ef_268e4cdfa4aa
    /// CalibrationIndex: unsigned int
    /// <returns>
    /// The <see cref="CalibrationErrorEx"/>.
    /// </returns>
    [DllImport("QuickLinkAPI.dll", EntryPoint = "OpenCalibrationEx", CallingConvention = CallingConvention.StdCall)]
    public static extern CalibrationErrorEx OpenCalibrationEx(uint CalibrationIndex);

    /// <summary>
    /// The get clicking options.
    /// </summary>
    /// <param name="Options">
    /// The Options.
    /// </param>
    /// Return Type: boolean
    /// Options: ClickingOptions*
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    [DllImport("QuickLinkAPI.dll", EntryPoint = "GetClickingOptions", CallingConvention = CallingConvention.StdCall)]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool GetClickingOptions(ref ClickingOptions Options);

    /// <summary>
    /// The get calibration options.
    /// </summary>
    /// <param name="Options">
    /// The Options.
    /// </param>
    /// Return Type: boolean
    /// Options: CalibrationOptions*
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    [DllImport("QuickLinkAPI.dll", EntryPoint = "GetCalibrationOptions", CallingConvention = CallingConvention.StdCall)]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool GetCalibrationOptions(ref CalibrationOptions Options);

    /// <summary>
    /// The get processing options.
    /// </summary>
    /// <param name="Options">
    /// The Options.
    /// </param>
    /// Return Type: boolean
    /// Options: ProcessingOptions*
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    [DllImport("QuickLinkAPI.dll", EntryPoint = "GetProcessingOptions", CallingConvention = CallingConvention.StdCall)]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool GetProcessingOptions(ref ProcessingOptions Options);

    /// <summary>
    /// The get camera options.
    /// </summary>
    /// <param name="Options">
    /// The Options.
    /// </param>
    /// Return Type: boolean
    /// Options: CameraOptions*
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    [DllImport("QuickLinkAPI.dll", EntryPoint = "GetCameraOptions", CallingConvention = CallingConvention.StdCall)]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool GetCameraOptions(ref CameraOptions Options);

    /// <summary>
    /// The get toolbar options.
    /// </summary>
    /// <param name="Options">
    /// The Options.
    /// </param>
    /// Return Type: boolean
    /// Options: ToolbarOptions*
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    [DllImport("QuickLinkAPI.dll", EntryPoint = "GetToolbarOptions", CallingConvention = CallingConvention.StdCall)]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool GetToolbarOptions(ref ToolbarOptions Options);

    /// <summary>
    /// The set clicking options.
    /// </summary>
    /// <param name="Options">
    /// The Options.
    /// </param>
    /// Return Type: boolean
    /// Options: ClickingOptions
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    [DllImport("QuickLinkAPI.dll", EntryPoint = "SetClickingOptions", CallingConvention = CallingConvention.StdCall)]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool SetClickingOptions(ClickingOptions Options);

    /// <summary>
    /// The set calibration options.
    /// </summary>
    /// <param name="Options">
    /// The Options.
    /// </param>
    /// Return Type: boolean
    /// Options: CalibrationOptions
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    [DllImport("QuickLinkAPI.dll", EntryPoint = "SetCalibrationOptions", CallingConvention = CallingConvention.StdCall)]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool SetCalibrationOptions(CalibrationOptions Options);

    /// <summary>
    /// The set processing options.
    /// </summary>
    /// <param name="Options">
    /// The Options.
    /// </param>
    /// Return Type: boolean
    /// Options: ProcessingOptions
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    [DllImport("QuickLinkAPI.dll", EntryPoint = "SetProcessingOptions", CallingConvention = CallingConvention.StdCall)]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool SetProcessingOptions(ProcessingOptions Options);

    /// <summary>
    /// The set camera options.
    /// </summary>
    /// <param name="Options">
    /// The Options.
    /// </param>
    /// Return Type: boolean
    /// Options: CameraOptions
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    [DllImport("QuickLinkAPI.dll", EntryPoint = "SetCameraOptions", CallingConvention = CallingConvention.StdCall)]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool SetCameraOptions(CameraOptions Options);

    /// <summary>
    /// The set toolbar options.
    /// </summary>
    /// <param name="Options">
    /// The Options.
    /// </param>
    /// Return Type: boolean
    /// Options: ToolbarOptions
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    [DllImport("QuickLinkAPI.dll", EntryPoint = "SetToolbarOptions", CallingConvention = CallingConvention.StdCall)]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool SetToolbarOptions(ToolbarOptions Options);

    /// <summary>
    /// The register click event.
    /// </summary>
    /// <param name="WindowHandle">
    /// The Window Handle.
    /// </param>
    /// <param name="PrimaryMessage">
    /// The Primary Message.
    /// </param>
    /// <param name="SecondaryMessage">
    /// The Secondary Message.
    /// </param>
    /// Return Type: void
    /// WindowHandle: void*
    /// PrimaryMessage: unsigned int
    /// SecondaryMessage: unsigned int
    [DllImport("QuickLinkAPI.dll", EntryPoint = "RegisterClickEvent", CallingConvention = CallingConvention.StdCall)]
    public static extern void RegisterClickEvent(IntPtr WindowHandle, uint PrimaryMessage, uint SecondaryMessage);

    /// <summary>
    /// The get serial number.
    /// </summary>
    /// <param name="SerialNumber">
    /// The Serial Number.
    /// </param>
    /// Return Type: boolean
    /// SerialNumber: int*
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    [DllImport("QuickLinkAPI.dll", EntryPoint = "GetSerialNumber", CallingConvention = CallingConvention.StdCall)]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool GetSerialNumber(ref int SerialNumber);

    /// <summary>
    /// The set custom gpio.
    /// </summary>
    /// <param name="GPIO_1">
    /// The GPI O_1.
    /// </param>
    /// <param name="GPIO_2">
    /// The GPI O_2.
    /// </param>
    /// <param name="GPIO_3">
    /// The GPI O_3.
    /// </param>
    /// Return Type: boolean
    /// GPIO_1: CameraCustomGPIOOutputValue-&gt;Anonymous_50c6f25c_a420_4bb1_be48_4d694a4f0a2d
    /// GPIO_2: CameraCustomGPIOOutputValue-&gt;Anonymous_50c6f25c_a420_4bb1_be48_4d694a4f0a2d
    /// GPIO_3: CameraCustomGPIOOutputValue-&gt;Anonymous_50c6f25c_a420_4bb1_be48_4d694a4f0a2d
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    [DllImport("QuickLinkAPI.dll", EntryPoint = "SetCustomGPIO", CallingConvention = CallingConvention.StdCall)]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool SetCustomGPIO(
      CameraCustomGPIOOutputValue GPIO_1, CameraCustomGPIOOutputValue GPIO_2, CameraCustomGPIOOutputValue GPIO_3);
  }
}