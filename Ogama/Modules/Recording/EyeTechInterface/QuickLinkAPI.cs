/**
 * QuickLinkAPI.h (v5.2) parsed by P/Invoke Interop Assistent 1.0
 * http://clrinterop.codeplex.com/releases/view/14120
 * 
 * NOTICE:
 * The original .h file sometimes used C++ references (&). Due to the Interop
 * Assistent (and I am guessing C# too) not being able to directly handle these
 * operations I have replaced them by pointers (*) (Which seems to work for the 
 * GetCalibrationOptions function. 
 */
using Microsoft.Win32;

namespace Ogama.Modules.Recording.EyeTechInterface
{
  public class NativeConstants
  {
    public const string QUICK_LINK_API_H = "";
    public const string QUICK_LINK_API = "__declspec(dllimport)";
    public const string QUICK_LINK_CALL_CONVEN = "__stdcall";
  }

  public enum CalibrationErrorEx
  {
    CALIBRATIONEX_OK = 0,
    CALIBRATIONEX_NO_EYE_SELECTED = 1,
    CALIBRATIONEX_NOT_INITIALIZED = 2,
    CALIBRATIONEX_NO_NEW_TARGETS = 3,
    CALIBRATIONEX_INVALID_TARGET_HANDLE = 4,
    CALIBRATIONEX_RIGHT_EYE_NOT_FOUND = 5,
    CALIBRATIONEX_LEFT_EYE_NOT_FOUND = 6,
    CALIBRATIONEX_NO_EYE_FOUND = 7,
    CALIBRATIONEX_NOT_ALL_TARGETS_CALIBRATED = 8,
    CALIBRATIONEX_INVALID_INDEX = 9,
    CALIBRATIONEX_INTERNAL_TIMEOUT = 10,
  }

  public enum CalibrationStyle
  {
    CAL_STYLE_5_POINT = 0,
    CAL_STYLE_9_POINT = 1,
    CAL_STYLE_16_POINT = 2,
  }

  public enum ClickMethod
  {
    CLICK_METHOD_NONE = 0,
    CLICK_METHOD_BLINK = 1,
    CLICK_METHOD_DWELL = 2,
  }

  public enum EyesToProcess
  {
    EYES_TO_PROC_SINGLE_LEFT = 0,
    EYES_TO_PROC_SINGLE_RIGHT = 1,
    EYES_TO_PROC_DUAL_LEFT_OR_RIGHT = 2,
    EYES_TO_PROC_DUAL_LEFT_AND_RIGHT = 3,
  }

  public enum ProcessPriority
  {
    PROC_PRIORITY_0 = 0,
    PROC_PRIORITY_1 = 1,
    PROC_PRIORITY_2 = 2,
    PROC_PRIORITY_3 = 3,
  }

  public enum CameraGainMethod
  {
    CAM_GAIN_METHOD_AUTO = 0,
    CAM_GAIN_METHOD_MANUAL = 1,
  }

  public enum CameraGPIOOutput
  {
    CAM_GPIO_OUT_CUSTOM = 0,
    CAM_GPIO_OUT_LEFT_TRACKING_STATUS = 1,
    CAM_GPIO_OUT_RIGHT_TRACKING_STATUS = 2,
  }

  public enum CameraCustomGPIOOutputValue
  {
    CAM_GPIO_OUT_VALUE_NO_CHANGE = 0,
    CAM_GPIO_OUT_VALUE_HIGH = 1,
    CAM_GPIO_OUT_VALUE_LOW = 2,
  }

  public enum ToolBarImageDisplay
  {
    IMG_DISP_LIVE_IMAGE = 0,
    IMG_DISP_PSEUDO_IMAGE = 1,
  }

  public enum QGWindowState
  {
    QGWS_HOME = 0,
    QGWS_EYE_TOOLS = 1,
    QGWS_EYE_TOOLS_IMAGE = 2,
    QGWS_HIDEN = 3,
  }

  [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
  public struct DPoint
  {
    public double x;
    public double y;
  }

  [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
  public struct LPoint
  {
    public int x;
    public int y;
  }

  [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
  public struct EyeData
  {
    public DPoint Pupil;
    public DPoint Glint1;
    public DPoint Glint2;
    public LPoint GazePoint;
    public double PupilDiameter;
    [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)]
    public bool Found;
    [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)]
    public bool Calibrated;
  }

  [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
  public struct ImageData
  {
    public double Time;
    public EyeData LeftEye;
    public EyeData RightEye;
    /// unsigned char*
    [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.LPStr)]
    public string PixelData;
    public int Width;
    public int Height;
  }

  [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
  public struct GPCollectionData
  {
    public int X;
    public int Y;
    public double Time;
  }

  [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
  public struct ClickingOptions
  {
    [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)]
    public bool Click_AudibleFeedback;
    public uint Click_Delay;
    public uint Click_ZoomFactor;
    public ClickMethod Click_Method;
    public uint Blink_PrimaryTime;
    public uint Blink_SecondaryTime;
    public uint Blink_CancelTime;
    [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)]
    public bool Blink_EnableSecondaryClick;
    [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)]
    public bool Blink_BothEyesRequired;
    [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)]
    public bool Blink_VisualFeedback;
    public uint Dwell_BoxSize;
    public uint Dwell_Time;
  }

  [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
  public struct CalibrationOptions
  {
    public int Calibration_TargetTime;
    public CalibrationStyle Calibration_Style;
  }

  [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
  public struct ProcessingOptions
  {

    /// unsigned int
    public uint Processing_SmoothingFactor;

    /// EyesToProcess->Anonymous_d66d7daa_4dcd_4947_8d42_2619a0ab89cc
    public EyesToProcess Processing_EyesToProcess;

    /// boolean
    [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)]
    public bool Processing_EnableCapture;

    /// boolean
    [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)]
    public bool Processing_EnableProcessing;

    /// boolean
    [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)]
    public bool Processing_EnableDisplay;

    /// boolean
    [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)]
    public bool Processing_EnableCursorMovement;

    /// boolean
    [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)]
    public bool Processing_EnableClicking;

    /// unsigned int
    public uint Processing_MaxProcessTime;

    /// ProcessPriority->Anonymous_c7988c45_ec73_49c7_b042_dce7dbe5365b
    public ProcessPriority Processing_ProcessPriority;
  }

  [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
  public struct CameraOptions
  {

    /// unsigned int
    public uint Camera_BusBandwidthPercentage;

    /// unsigned int
    public uint Camera_ImageROIPercentage;

    /// CameraGainMethod->Anonymous_fe97665f_6c0c_4e10_aff8_91e1f8486280
    public CameraGainMethod Camera_GainMethod;

    /// double
    public double Camera_GainValue;

    /// CameraGPIOOutput->Anonymous_6a49a8f4_4411_4793_b3a6_4cf1727cfeae
    public CameraGPIOOutput Camera_GPIO_1;

    /// CameraGPIOOutput->Anonymous_6a49a8f4_4411_4793_b3a6_4cf1727cfeae
    public CameraGPIOOutput Camera_GPIO_2;

    /// CameraGPIOOutput->Anonymous_6a49a8f4_4411_4793_b3a6_4cf1727cfeae
    public CameraGPIOOutput Camera_GPIO_3;
  }

  [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
  public struct ToolbarOptions
  {

    /// unsigned int
    public uint Toolbar_ButtonSizeX;

    /// unsigned int
    public uint Toolbar_ButtonSizeY;

    /// ToolBarImageDisplay->Anonymous_bf0318f8_0c65_4d63_8607_49f233abe625
    public ToolBarImageDisplay Toolbar_ImageDisplayType;
  }

  public class NativeMethods
  {
    // QuickLinkAPI.dll location
    string libraryLocation = "";

    // Function looks in the registry if the API is installed on the current system.
    // "QuickLinkAPI.dll" -> this.libraryLocation
    // Seems to be useless due to the fact that attributes can't be changed as variables

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

    /// Return Type: boolean
    [System.Runtime.InteropServices.DllImportAttribute("QuickLinkAPI.dll", EntryPoint = "GetQGOnFlag", CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
    [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)]
    public static extern bool GetQGOnFlag();


    /// Return Type: void
    [System.Runtime.InteropServices.DllImportAttribute("QuickLinkAPI.dll", EntryPoint = "ExitQuickGlance", CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
    public static extern void ExitQuickGlance();


    /// Return Type: void
    [System.Runtime.InteropServices.DllImportAttribute("QuickLinkAPI.dll", EntryPoint = "SetEnableEyeControl", CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
    public static extern void SetEnableEyeControl();


    /// Return Type: void
    [System.Runtime.InteropServices.DllImportAttribute("QuickLinkAPI.dll", EntryPoint = "ResetEnableEyeControl", CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
    public static extern void ResetEnableEyeControl();


    /// Return Type: void
    ///Enable: boolean
    [System.Runtime.InteropServices.DllImportAttribute("QuickLinkAPI.dll", EntryPoint = "SetEyeControl", CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
    public static extern void SetEyeControl([System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)] bool Enable);


    /// Return Type: boolean
    [System.Runtime.InteropServices.DllImportAttribute("QuickLinkAPI.dll", EntryPoint = "GetEyeControl", CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
    [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)]
    public static extern bool GetEyeControl();


    /// Return Type: void
    ///MWState: QGWindowState->Anonymous_cd0c0d4e_f20f_4897_905c_1dd89fe6e816
    [System.Runtime.InteropServices.DllImportAttribute("QuickLinkAPI.dll", EntryPoint = "SetMWState", CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
    public static extern void SetMWState(QGWindowState MWState);


    /// Return Type: void
    [System.Runtime.InteropServices.DllImportAttribute("QuickLinkAPI.dll", EntryPoint = "ToggleLargeImage", CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
    public static extern void ToggleLargeImage();


    /// Return Type: void
    [System.Runtime.InteropServices.DllImportAttribute("QuickLinkAPI.dll", EntryPoint = "ShowLargeImage", CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
    public static extern void ShowLargeImage();


    /// Return Type: void
    [System.Runtime.InteropServices.DllImportAttribute("QuickLinkAPI.dll", EntryPoint = "HideLargeImage", CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
    public static extern void HideLargeImage();


    /// Return Type: void
    [System.Runtime.InteropServices.DllImportAttribute("QuickLinkAPI.dll", EntryPoint = "MoveLeftRight", CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
    public static extern void MoveLeftRight();


    /// Return Type: void
    [System.Runtime.InteropServices.DllImportAttribute("QuickLinkAPI.dll", EntryPoint = "MoveUpDown", CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
    public static extern void MoveUpDown();


    /// Return Type: double
    [System.Runtime.InteropServices.DllImportAttribute("QuickLinkAPI.dll", EntryPoint = "GetSDKVersion", CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
    public static extern double GetSDKVersion();


    /// Return Type: void
    ///CopyImage: boolean
    [System.Runtime.InteropServices.DllImportAttribute("QuickLinkAPI.dll", EntryPoint = "StartRawCapture", CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
    public static extern void StartRawCapture([System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)] bool CopyImage);


    /// Return Type: void
    [System.Runtime.InteropServices.DllImportAttribute("QuickLinkAPI.dll", EntryPoint = "StopRawCapture", CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
    public static extern void StopRawCapture();


    /// Return Type: void
    ///CopyImage: boolean
    [System.Runtime.InteropServices.DllImportAttribute("QuickLinkAPI.dll", EntryPoint = "SetCopyImageFlag", CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
    public static extern void SetCopyImageFlag([System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)] bool CopyImage);


    /// Return Type: boolean
    ///MaxTimeout: unsigned int
    ///Data: ImageData*
    [System.Runtime.InteropServices.DllImportAttribute("QuickLinkAPI.dll", EntryPoint = "GetImageData", CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
    [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)]
    public static extern bool GetImageData(uint MaxTimeout, ref ImageData Data);


    /// Return Type: boolean
    ///MaxTimeout: unsigned int
    ///Data: ImageData*
    ///Latency: double*
    [System.Runtime.InteropServices.DllImportAttribute("QuickLinkAPI.dll", EntryPoint = "GetImageDataAndLatency", CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
    [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)]
    public static extern bool GetImageDataAndLatency(uint MaxTimeout, ref ImageData Data, ref double Latency);


    /// Return Type: void
    ///NumPoints: unsigned int
    [System.Runtime.InteropServices.DllImportAttribute("QuickLinkAPI.dll", EntryPoint = "StartBulkCapture", CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
    public static extern void StartBulkCapture(uint NumPoints);


    /// Return Type: void
    [System.Runtime.InteropServices.DllImportAttribute("QuickLinkAPI.dll", EntryPoint = "StopBulkCapture", CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
    public static extern void StopBulkCapture();


    /// Return Type: boolean
    ///NumCaptured: unsigned int*
    ///NumNotCaptured: unsigned int*
    ///NumRead: unsigned int*
    ///NumNotRead: unsigned int*
    ///Capturing: boolean*
    [System.Runtime.InteropServices.DllImportAttribute("QuickLinkAPI.dll", EntryPoint = "QueryBulkCapture", CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
    [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)]
    public static extern bool QueryBulkCapture(ref uint NumCaptured, ref uint NumNotCaptured, ref uint NumRead, ref uint NumNotRead, ref bool Capturing);


    /// Return Type: boolean
    ///MaxTimeout: unsigned int
    ///Data: ImageData*
    [System.Runtime.InteropServices.DllImportAttribute("QuickLinkAPI.dll", EntryPoint = "ReadBulkCapture", CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
    [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)]
    public static extern bool ReadBulkCapture(uint MaxTimeout, ref ImageData Data);


    /// Return Type: void
    [System.Runtime.InteropServices.DllImportAttribute("QuickLinkAPI.dll", EntryPoint = "InternalCalibration1", CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
    public static extern void InternalCalibration1();


    /// Return Type: CalibrationErrorEx->Anonymous_f0d31251_262e_4684_a5ef_268e4cdfa4aa
    ///CalibrationIndex: unsigned int
    [System.Runtime.InteropServices.DllImportAttribute("QuickLinkAPI.dll", EntryPoint = "InitializeCalibrationEx", CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
    public static extern CalibrationErrorEx InitializeCalibrationEx(uint CalibrationIndex);


    /// Return Type: CalibrationErrorEx->Anonymous_f0d31251_262e_4684_a5ef_268e4cdfa4aa
    ///X: int*
    ///Y: int*
    ///TargetHandle: int*
    [System.Runtime.InteropServices.DllImportAttribute("QuickLinkAPI.dll", EntryPoint = "GetNewTargetPositionEx", CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
    public static extern CalibrationErrorEx GetNewTargetPositionEx(ref int X, ref int Y, ref int TargetHandle);


    /// Return Type: CalibrationErrorEx->Anonymous_f0d31251_262e_4684_a5ef_268e4cdfa4aa
    ///X: int*
    ///Y: int*
    ///TargetHandle: int*
    [System.Runtime.InteropServices.DllImportAttribute("QuickLinkAPI.dll", EntryPoint = "GetWorstTargetPositionEx", CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
    public static extern CalibrationErrorEx GetWorstTargetPositionEx(ref int X, ref int Y, ref int TargetHandle);


    /// Return Type: CalibrationErrorEx->Anonymous_f0d31251_262e_4684_a5ef_268e4cdfa4aa
    ///TargetHandle: int
    [System.Runtime.InteropServices.DllImportAttribute("QuickLinkAPI.dll", EntryPoint = "CalibrateEx", CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
    public static extern CalibrationErrorEx CalibrateEx(int TargetHandle);


    /// Return Type: CalibrationErrorEx->Anonymous_f0d31251_262e_4684_a5ef_268e4cdfa4aa
    ///left: double*
    ///right: double*
    [System.Runtime.InteropServices.DllImportAttribute("QuickLinkAPI.dll", EntryPoint = "GetScoreEx", CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
    public static extern CalibrationErrorEx GetScoreEx(ref double left, ref double right);


    /// Return Type: CalibrationErrorEx->Anonymous_f0d31251_262e_4684_a5ef_268e4cdfa4aa
    [System.Runtime.InteropServices.DllImportAttribute("QuickLinkAPI.dll", EntryPoint = "ApplyCalibrationEx", CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
    public static extern CalibrationErrorEx ApplyCalibrationEx();


    /// Return Type: void
    ///DeltaX: int
    ///DeltaY: int
    [System.Runtime.InteropServices.DllImportAttribute("QuickLinkAPI.dll", EntryPoint = "CalibrationBiasEx", CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
    public static extern void CalibrationBiasEx(int DeltaX, int DeltaY);


    /// Return Type: CalibrationErrorEx->Anonymous_f0d31251_262e_4684_a5ef_268e4cdfa4aa
    ///CalibrationIndex: unsigned int
    [System.Runtime.InteropServices.DllImportAttribute("QuickLinkAPI.dll", EntryPoint = "OpenCalibrationEx", CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
    public static extern CalibrationErrorEx OpenCalibrationEx(uint CalibrationIndex);


    /// Return Type: boolean
    ///Options: ClickingOptions*
    [System.Runtime.InteropServices.DllImportAttribute("QuickLinkAPI.dll", EntryPoint = "GetClickingOptions", CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
    [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)]
    public static extern bool GetClickingOptions(ref ClickingOptions Options);


    /// Return Type: boolean
    ///Options: CalibrationOptions*
    [System.Runtime.InteropServices.DllImportAttribute("QuickLinkAPI.dll", EntryPoint = "GetCalibrationOptions", CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
    [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)]
    public static extern bool GetCalibrationOptions(ref CalibrationOptions Options);


    /// Return Type: boolean
    ///Options: ProcessingOptions*
    [System.Runtime.InteropServices.DllImportAttribute("QuickLinkAPI.dll", EntryPoint = "GetProcessingOptions", CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
    [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)]
    public static extern bool GetProcessingOptions(ref ProcessingOptions Options);


    /// Return Type: boolean
    ///Options: CameraOptions*
    [System.Runtime.InteropServices.DllImportAttribute("QuickLinkAPI.dll", EntryPoint = "GetCameraOptions", CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
    [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)]
    public static extern bool GetCameraOptions(ref CameraOptions Options);


    /// Return Type: boolean
    ///Options: ToolbarOptions*
    [System.Runtime.InteropServices.DllImportAttribute("QuickLinkAPI.dll", EntryPoint = "GetToolbarOptions", CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
    [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)]
    public static extern bool GetToolbarOptions(ref ToolbarOptions Options);


    /// Return Type: boolean
    ///Options: ClickingOptions
    [System.Runtime.InteropServices.DllImportAttribute("QuickLinkAPI.dll", EntryPoint = "SetClickingOptions", CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
    [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)]
    public static extern bool SetClickingOptions(ClickingOptions Options);


    /// Return Type: boolean
    ///Options: CalibrationOptions
    [System.Runtime.InteropServices.DllImportAttribute("QuickLinkAPI.dll", EntryPoint = "SetCalibrationOptions", CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
    [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)]
    public static extern bool SetCalibrationOptions(CalibrationOptions Options);


    /// Return Type: boolean
    ///Options: ProcessingOptions
    [System.Runtime.InteropServices.DllImportAttribute("QuickLinkAPI.dll", EntryPoint = "SetProcessingOptions", CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
    [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)]
    public static extern bool SetProcessingOptions(ProcessingOptions Options);


    /// Return Type: boolean
    ///Options: CameraOptions
    [System.Runtime.InteropServices.DllImportAttribute("QuickLinkAPI.dll", EntryPoint = "SetCameraOptions", CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
    [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)]
    public static extern bool SetCameraOptions(CameraOptions Options);


    /// Return Type: boolean
    ///Options: ToolbarOptions
    [System.Runtime.InteropServices.DllImportAttribute("QuickLinkAPI.dll", EntryPoint = "SetToolbarOptions", CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
    [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)]
    public static extern bool SetToolbarOptions(ToolbarOptions Options);


    /// Return Type: void
    ///WindowHandle: void*
    ///PrimaryMessage: unsigned int
    ///SecondaryMessage: unsigned int
    [System.Runtime.InteropServices.DllImportAttribute("QuickLinkAPI.dll", EntryPoint = "RegisterClickEvent", CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
    public static extern void RegisterClickEvent(System.IntPtr WindowHandle, uint PrimaryMessage, uint SecondaryMessage);


    /// Return Type: boolean
    ///SerialNumber: int*
    [System.Runtime.InteropServices.DllImportAttribute("QuickLinkAPI.dll", EntryPoint = "GetSerialNumber", CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
    [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)]
    public static extern bool GetSerialNumber(ref int SerialNumber);


    /// Return Type: boolean
    ///GPIO_1: CameraCustomGPIOOutputValue->Anonymous_50c6f25c_a420_4bb1_be48_4d694a4f0a2d
    ///GPIO_2: CameraCustomGPIOOutputValue->Anonymous_50c6f25c_a420_4bb1_be48_4d694a4f0a2d
    ///GPIO_3: CameraCustomGPIOOutputValue->Anonymous_50c6f25c_a420_4bb1_be48_4d694a4f0a2d
    [System.Runtime.InteropServices.DllImportAttribute("QuickLinkAPI.dll", EntryPoint = "SetCustomGPIO", CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
    [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)]
    public static extern bool SetCustomGPIO(CameraCustomGPIOOutputValue GPIO_1, CameraCustomGPIOOutputValue GPIO_2, CameraCustomGPIOOutputValue GPIO_3);
  }
}
