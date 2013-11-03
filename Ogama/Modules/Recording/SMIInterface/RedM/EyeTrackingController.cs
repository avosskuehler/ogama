using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Threading;

namespace Ogama.Modules.Recording.SMIInterface.RedM
{

    public class EyeTrackingController
    {

        // API Struct definition. See the manual for further description. 
        public struct SystemInfoStruct
        {
            public int samplerate;
            public int iV_MajorVersion;
            public int iV_MinorVersion;
            public int iV_Buildnumber;
            public int API_MajorVersion;
            public int API_MinorVersion;
            public int API_Buildnumber;
            public int iV_ETSystem;
        };

        public struct CalibrationPointStruct
        {
            public int number;
            public int positionX;
            public int positionY;
        };


        public struct EyeDataStruct
        {
            public double gazeX;
            public double gazeY;
            public double diam;
            public double eyePositionX;
            public double eyePositionY;
            public double eyePositionZ;
        };


        public struct SampleStruct
        {
            public Int64 timestamp;
            public EyeDataStruct leftEye;
            public EyeDataStruct rightEye;
            public int planeNumber;
        };
        

        public struct EventStruct
        {
            public char eventType;
            public char eye;
            public Int64 startTime;
            public Int64 endTime;
            public Int64 duration;
            public double positionX;
            public double positionY;
        };


        public struct AccuracyStruct
        {
            public double deviationXLeft;
            public double deviationYLeft;
            public double deviationXRight;
            public double deviationYRight;
        };


        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct CalibrationStruct
        {
            public int method;				        
            public int visualization;			    
            public int displayDevice;				
            public int speed;					    
            public int autoAccept;			        
            public int foregroundColor;	            
            public int backgroundColor;	            
            public int targetShape;		            
            public int targetSize;		            
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public string targetFilename;
        };
        

        public struct StandAloneModeGeometryStruct
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public string setupName;
            public int stimX;
            public int stimY;
            public int stimHeightOverFloor;
            public int redHeightOverFloor;
            public int redStimDist;
            public int redInclAngle;
        };


        public struct MonitorAttachedGeometryStruct
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public string setupName;
            public int stimX;
            public int stimY;
            public int redStimDistHeight;
            public int redStimDistDepth;
            public int redInclAngle;
        };


        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct ImageStruct
        {
            public int imageHeight;
            public int imageWidth;
            public int imageSize;
            public IntPtr imageBuffer;
        };


        public struct DateStruct
        {
            public int day;
            public int month;
            public int year;
        };


        public struct AOIRectangleStruct
        {
            public int x1;
            public int x2;
            public int y1;
            public int y2;
        };

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct AOIStruct
        {
            public int enabled;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public string aoiName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public string aoiGroup;
            public AOIRectangleStruct position;
            public int fixationHit;
            public int outputValue;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public string outputMessage;
            public char eye;
        };



        // API Function definition. See the manual for further description. 

        [DllImport("iViewXAPI.dll", EntryPoint = "iV_AbortCalibration")]
        private static extern int Unmanaged_AbortCalibration();

        [DllImport("iViewXAPI.dll", EntryPoint = "iV_AcceptCalibrationPoint")]
        private static extern int Unmanaged_AcceptCalibrationPoint();

        [DllImport("iViewXAPI.dll", EntryPoint = "iV_Calibrate")]
        private static extern int Unmanaged_Calibrate();
        
        [DllImport("iViewXAPI.dll", EntryPoint = "iV_ChangeCalibrationPoint")]
        private static extern int Unmanaged_ChangeCalibrationPoint(int number, int positionX, int positionY);

        [DllImport("iViewXAPI.dll", EntryPoint = "iV_ClearAOI")]
        private static extern int Unmanaged_ClearAOI();

        [DllImport("iViewXAPI.dll", EntryPoint = "iV_ClearRecordingBuffer")]
        private static extern int Unmanaged_ClearRecordingBuffer();

        [DllImport("iViewXAPI.dll", EntryPoint = "iV_ConfigureFilter")]
        private static extern int Unmanaged_ConfigureFilter(int filter, ref byte data);

        [DllImport("iViewXAPI.dll", EntryPoint = "iV_Connect")]
        private static extern int Unmanaged_Connect(StringBuilder sendIPAddress, int sendPort, StringBuilder recvIPAddress, int receivePort);

        [DllImport("iViewXAPI.dll", EntryPoint = "iV_ContinueEyetracking")]
        private static extern int Unmanaged_ContinueEyetracking();

        [DllImport("iViewXAPI.dll", EntryPoint = "iV_ContinueRecording")]
        private static extern int Unmanaged_ContinueRecording(StringBuilder etMessage);

        [DllImport("iViewXAPI.dll", EntryPoint = "iV_DefineAOI")]
        private static extern int Unmanaged_DefineAOI(ref AOIStruct aoiData);

        [DllImport("iViewXAPI.dll", EntryPoint = "iV_DefineAOIPort")]
        private static extern int Unmanaged_DefineAOIPort(int port);
               
        [DllImport("iViewXAPI.dll", EntryPoint = "iV_DeleteMonitorAttachedGeometry")]
        private static extern int Unmanaged_DeleteMonitorAttachedGeometry(StringBuilder name);

        [DllImport("iViewXAPI.dll", EntryPoint = "iV_DeleteStandAloneGeometry")]
        private static extern int Unmanaged_DeleteStandAloneGeometry(StringBuilder name);

        [DllImport("iViewXAPI.dll", EntryPoint = "iV_DisableAOI")]
        private static extern int Unmanaged_DisableAOI(StringBuilder name);

        [DllImport("iViewXAPI.dll", EntryPoint = "iV_DisableAOIGroup")]
        private static extern int Unmanaged_DisableAOIGroup(StringBuilder group);

        [DllImport("iViewXAPI.dll", EntryPoint = "iV_DisableGazeDataFilter")]
        private static extern int Unmanaged_DisableGazeDataFilter();

        [DllImport("iViewXAPI.dll", EntryPoint = "iV_DisableProcessorHighPerformanceMode")]
        private static extern int Unmanaged_DisableProcessorHighPerformanceMode();

        [DllImport("iViewXAPI.dll", EntryPoint = "iV_Disconnect")]
        private static extern int Unmanaged_Disconnect();

        [DllImport("iViewXAPI.dll", EntryPoint = "iV_EnableAOI")]
        private static extern int Unmanaged_EnableAOI(StringBuilder name);

        [DllImport("iViewXAPI.dll", EntryPoint = "iV_EnableAOIGroup")]
        private static extern int Unmanaged_EnableAOIGroup(StringBuilder group);

        [DllImport("iViewXAPI.dll", EntryPoint = "iV_EnableGazeDataFilter")]
        private static extern int Unmanaged_EnableGazeDataFilter();

        [DllImport("iViewXAPI.dll", EntryPoint = "iV_EnableProcessorHighPerformanceMode")]
        private static extern int Unmanaged_EnableProcessorHighPerformanceMode();

        [DllImport("iViewXAPI.dll", EntryPoint = "iV_GetAccuracy")]
        private static extern int Unmanaged_GetAccuracy(ref AccuracyStruct accuracyData, int visualization);

        [DllImport("iViewXAPI.dll", EntryPoint = "iV_GetAccuracyImage")]
        private static extern int Unmanaged_GetAccuracyImage(ref ImageStruct imageData);

        [DllImport("iViewXAPI.dll", EntryPoint = "iV_GetAOIOutputValue")]
        private static extern int Unmanaged_GetAOIOutputValue(ref int aoiOutputValue);

        [DllImport("iViewXAPI.dll", EntryPoint = "iV_GetCalibrationParameter")]
        private static extern int Unmanaged_GetCalibrationParameter(ref CalibrationStruct calibrationData);

        [DllImport("iViewXAPI.dll", EntryPoint = "iV_GetCalibrationPoint")]
        private static extern int Unmanaged_GetCalibrationPoint(int calibrationPointNumber, ref CalibrationPointStruct calibrationPoint);

        [DllImport("iViewXAPI.dll", EntryPoint = "iV_GetCurrentCalibrationPoint")]
        private static extern int Unmanaged_GetCurrentCalibrationPoint(ref CalibrationPointStruct actualCalibrationPoint);

        [DllImport("iViewXAPI.dll", EntryPoint = "iV_GetCurrentTimestamp")]
        private static extern int Unmanaged_GetCurrentTimestamp(ref Int64 currentTimestamp);

        [DllImport("iViewXAPI.dll", EntryPoint = "iV_GetEvent")]
        private static extern int Unmanaged_GetEvent(ref EventStruct eventDataSample);

        [DllImport("iViewXAPI.dll", EntryPoint = "iV_GetEyeImage")]
        private static extern int Unmanaged_GetEyeImage(ref ImageStruct imageData);

        [DllImport("iViewXAPI.dll", EntryPoint = "iV_GetFeatureKey")]
        private static extern int Unmanaged_GetFeatureKey(ref Int64 featureKey);

        [DllImport("iViewXAPI.dll", EntryPoint = "iV_GetGeometryProfiles")]
        private static extern int Unmanaged_GetGeometryProfiles(int maxSize, ref StringBuilder profileNames);

        [DllImport("iViewXAPI.dll", EntryPoint = "iV_GetLicenseDueDate")]
        private static extern int Unmanaged_GetLicenseDueDate(ref DateStruct licenseDueDate);

        [DllImport("iViewXAPI.dll", EntryPoint = "iV_GetMonitorAttachedGeometry")]
        private static extern int Unmanaged_GetMonitorAttachedGeometry(StringBuilder profileName, ref MonitorAttachedGeometryStruct monitorAttachedGeometry);

        [DllImport("iViewXAPI.dll", EntryPoint = "iV_GetSample")]
        private static extern int Unmanaged_GetSample(ref SampleStruct rawDataSample);

        [DllImport("iViewXAPI.dll", EntryPoint = "iV_GetSceneVideo")]
        private static extern int Unmanaged_GetSceneVideo(ref ImageStruct imageData);

        [DllImport("iViewXAPI.dll", EntryPoint = "iV_GetSerialNumber")]
        private static extern int Unmanaged_GetSerialNumber(ref StringBuilder serialNumber);

        [DllImport("iViewXAPI.dll", EntryPoint = "iV_GetSystemInfo")]
        private static extern int Unmanaged_GetSystemInfo(ref SystemInfoStruct systemInfoData);

        [DllImport("iViewXAPI.dll", EntryPoint = "iV_GetTrackingMonitor")]
        private static extern int Unmanaged_GetTrackingMonitor(ref ImageStruct imageData);

        [DllImport("iViewXAPI.dll", EntryPoint = "iV_IsConnected")]
        private static extern int Unmanaged_IsConnected();

        [DllImport("iViewXAPI.dll", EntryPoint = "iV_LoadCalibration")]
        private static extern int Unmanaged_LoadCalibration(StringBuilder name);

        [DllImport("iViewXAPI.dll", EntryPoint = "iV_Log")]
        private static extern int Unmanaged_Log(StringBuilder logMessage);

        [DllImport("iViewXAPI.dll", EntryPoint = "iV_PauseEyetracking")]
        private static extern int Unmanaged_PauseEyetracking();

        [DllImport("iViewXAPI.dll", EntryPoint = "iV_PauseRecording")]
        private static extern int Unmanaged_PauseRecording();

        [DllImport("iViewXAPI.dll", EntryPoint = "iV_Quit")]
        private static extern int Unmanaged_Quit();

        [DllImport("iViewXAPI.dll", EntryPoint = "iV_ReleaseAOIPort")]
        private static extern int Unmanaged_ReleaseAOIPort();

        [DllImport("iViewXAPI.dll", EntryPoint = "iV_RemoveAOI")]
        private static extern int Unmanaged_RemoveAOI(StringBuilder  aoiName);

        [DllImport("iViewXAPI.dll", EntryPoint = "iV_ResetCalibrationPoints")]
        private static extern int Unmanaged_ResetCalibrationPoints();

        [DllImport("iViewXAPI.dll", EntryPoint = "iV_SaveCalibration")]
        private static extern int Unmanaged_SaveCalibration(StringBuilder  aoiName);

        [DllImport("iViewXAPI.dll", EntryPoint = "iV_SaveData")]
        private static extern int Unmanaged_SaveData(StringBuilder  filename, StringBuilder description, StringBuilder user, int overwrite);

        [DllImport("iViewXAPI.dll", EntryPoint = "iV_SendCommand")]
        private static extern int Unmanaged_SendCommand(StringBuilder etMessage);

        [DllImport("iViewXAPI.dll", EntryPoint = "iV_SendImageMessage")]
        private static extern int Unmanaged_SendImageMessage(StringBuilder etMessage);

        [DllImport("iViewXAPI.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "iV_SetCalibrationCallback")]
        private static extern void Unmanaged_SetCalibrationCallback(MulticastDelegate calibrationCallbackFunction);

        [DllImport("iViewXAPI.dll", EntryPoint = "iV_SetConnectionTimeout")]
        private static extern int Unmanaged_SetConnectionTimeout(int time);

        [DllImport("iViewXAPI.dll", EntryPoint = "iV_SetGeometryProfile")]
        private static extern int Unmanaged_SetGeometryProfile(StringBuilder profileName);

        [DllImport("iViewXAPI.dll", EntryPoint = "iV_SetResolution")]
        private static extern int Unmanaged_SetResolution(int stimulusWidth, int stimulusHeight);

        [DllImport("iViewXAPI.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "iV_SetEventCallback")]
        private static extern void Unmanaged_SetEventCallback(MulticastDelegate eventCallbackFunction);

        [DllImport("iViewXAPI.dll", EntryPoint = "iV_SetEventDetectionParameter")]
        private static extern int Unmanaged_SetEventDetectionParameter(int minDuration, int maxDispersion);

        [DllImport("iViewXAPI.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "iV_SetEyeImageCallback")]
        private static extern void Unmanaged_SetEyeImageCallback(MulticastDelegate eyeImageCallbackFunction);

        [DllImport("iViewXAPI.dll", EntryPoint = "iV_SetLicense")]
        private static extern int Unmanaged_SetLicense(StringBuilder licenseKey);

        [DllImport("iViewXAPI.dll", EntryPoint = "iV_SetLogger")]
        private static extern int Unmanaged_SetLogger(int logLevel, StringBuilder filename);

        [DllImport("iViewXAPI.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "iV_SetSampleCallback")]
        private static extern void Unmanaged_SetSampleCallback(MulticastDelegate sampleCallbackFunction);

        [DllImport("iViewXAPI.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "iV_SetSceneVideoCallback")]
        private static extern void Unmanaged_SetSceneVideoCallback(MulticastDelegate sceneVideoCallbackFunction);

        [DllImport("iViewXAPI.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "iV_SetTrackingMonitorCallback")]
        private static extern void Unmanaged_SetTrackingMonitorCallback(MulticastDelegate trackingMonitorCallbackFunction);

        [DllImport("iViewXAPI.dll", EntryPoint = "iV_SetTrackingParameter")]
        private static extern int Unmanaged_SetTrackingParameter(int ET_PARAM_EYE, int ET_PARAM, int value);

        [DllImport("iViewXAPI.dll", EntryPoint = "iV_SetupCalibration")]
        private static extern int Unmanaged_SetupCalibration(ref CalibrationStruct calibrationData);

        [DllImport("iViewXAPI.dll", EntryPoint = "iV_SetupMonitorAttachedGeometry")]
        private static extern int Unmanaged_SetupMonitorAttachedGeometry(ref MonitorAttachedGeometryStruct monitorAttachedGeometry);

        [DllImport("iViewXAPI.dll", EntryPoint = "iV_SetupStandAloneMode")]
        private static extern int Unmanaged_SetupStandAloneMode(ref StandAloneModeGeometryStruct standAloneModeGeometry);

        [DllImport("iViewXAPI.dll", EntryPoint = "iV_ShowAccuracyMonitor")]
        private static extern int Unmanaged_ShowAccuracyMonitor();

        [DllImport("iViewXAPI.dll", EntryPoint = "iV_ShowEyeImageMonitor")]
        private static extern int Unmanaged_ShowEyeImageMonitor();

        [DllImport("iViewXAPI.dll", EntryPoint = "iV_ShowSceneVideoMonitor")]
        private static extern int Unmanaged_ShowSceneVideoMonitor();

        [DllImport("iViewXAPI.dll", EntryPoint = "iV_ShowTrackingMonitor")]
        private static extern int Unmanaged_ShowTrackingMonitor();

        [DllImport("iViewXAPI.dll", EntryPoint = "iV_Start")]
        private static extern int Unmanaged_Start(int etApplication);

        [DllImport("iViewXAPI.dll", EntryPoint = "iV_StartRecording")]
        private static extern int Unmanaged_StartRecording();

        [DllImport("iViewXAPI.dll", EntryPoint = "iV_StopRecording")]
        private static extern int Unmanaged_StopRecording();

        [DllImport("iViewXAPI.dll", EntryPoint = "iV_TestTTL")]
        private static extern int Unmanaged_TestTTL(long value);

        [DllImport("iViewXAPI.dll", EntryPoint = "iV_Validate")]
        private static extern int Unmanaged_Validate();






        public int iV_AbortCalibration()
        {
            return Unmanaged_AbortCalibration();
        }

        public int iV_AcceptCalibrationPoint()
        {
            return Unmanaged_AcceptCalibrationPoint();
        }

        public int iV_Calibrate()
        {
            return Unmanaged_Calibrate();
        }

        public int iV_ChangeCalibrationPoint(int number, int positionX, int positionY)
        {
            return Unmanaged_ChangeCalibrationPoint(number, positionX, positionY);
        }

        public int iV_ClearAOI()
        {
            return Unmanaged_ClearAOI();
        }

        public int iV_ClearRecordingBuffer()
        {
            return Unmanaged_ClearRecordingBuffer();
        }

        public int iV_ConfigureFilter(int filter, ref byte data)
        {
            return Unmanaged_ConfigureFilter(filter, ref data);
        }

        public int iV_Connect(StringBuilder sendIP, int sendPort, StringBuilder receiveIP, int receivePort)
        {
            return Unmanaged_Connect(sendIP, sendPort, receiveIP, receivePort);
        }

        public int iV_ContinueEyetracking()
        {
            return Unmanaged_ContinueEyetracking();
        }

        public int iV_ContinueRecording(StringBuilder trialname)
        {
            return Unmanaged_ContinueRecording(trialname);
        }

        public int iV_DefineAOI(ref AOIStruct aoi)
        {
            return Unmanaged_DefineAOI(ref aoi);
        }

        public int iV_DefineAOIPort(int port)
        {
            return Unmanaged_DefineAOIPort(port);
        }

        public int iV_DeleteMonitorAttachedGeometry(StringBuilder name)
        {
            return Unmanaged_DeleteMonitorAttachedGeometry(name);
        }

        public int iV_DeleteStandAloneGeometry(StringBuilder name)
        {
            return Unmanaged_DeleteStandAloneGeometry(name);
        }

        public int iV_DisableAOI(StringBuilder aoiName)
        {
            return Unmanaged_DisableAOI(aoiName);
        }

        public int iV_DisableAOIGroup(StringBuilder aoiGroup)
        {
            return Unmanaged_DisableAOIGroup(aoiGroup);
        }

        public int iV_DisableGazeDataFilter()
        {
            return Unmanaged_DisableGazeDataFilter();
        }

        public int iV_DisableProcessorHighPerformanceMode()
        {
            return Unmanaged_DisableProcessorHighPerformanceMode();
        }

        public int iV_Disconnect()
        {
            return Unmanaged_Disconnect();
        }

        public int iV_EnableAOI(StringBuilder aoiName)
        {
            return Unmanaged_EnableAOI(aoiName);
        }

        public int iV_EnableAOIGroup(StringBuilder aoiGroup)
        {
            return Unmanaged_EnableAOIGroup(aoiGroup);
        }

        public int iV_EnableGazeDataFilter()
        {
            return Unmanaged_EnableGazeDataFilter();
        }

        public int iV_EnableProcessorHighPerformanceMode()
        {
            return Unmanaged_EnableProcessorHighPerformanceMode();
        }

        public int iV_GetAccuracy(ref AccuracyStruct accuracyData, int visualization)
        {
            return Unmanaged_GetAccuracy(ref accuracyData, visualization);
        }

        public int iV_GetAccuracyImage(ref ImageStruct image)
        {
            return Unmanaged_GetAccuracyImage(ref image);
        }

        public int iV_GetAOIOutputValue(ref int aoiOutputValue)
        {
            return Unmanaged_GetAOIOutputValue(ref aoiOutputValue);
        }

        public int iV_GetCalibrationParameter(ref CalibrationStruct calibrationData)
        {
            return Unmanaged_GetCalibrationParameter(ref calibrationData);
        }

        public int iV_GetCalibrationPoint(int calibrationPointNumber, ref CalibrationPointStruct calibrationPoint)
        {
            return Unmanaged_GetCalibrationPoint(calibrationPointNumber, ref calibrationPoint);
        }

        public int iV_GetCurrentCalibrationPoint(ref CalibrationPointStruct currentCalibrationPoint)
        {
            return Unmanaged_GetCurrentCalibrationPoint(ref currentCalibrationPoint);
        }

        public int iV_GetCurrentTimestamp(ref Int64 currentTimestamp)
        {
            return Unmanaged_GetCurrentTimestamp(ref currentTimestamp);
        }

        public int iV_GetEvent(ref EventStruct eventDataSample)
        {
            return Unmanaged_GetEvent(ref eventDataSample);
        }

        public int iV_GetEyeImage(ref ImageStruct image)
        {
            return Unmanaged_GetEyeImage(ref image);
        }

        public int iV_GetFeatureKey(ref Int64 featureKey)
        {
            return Unmanaged_GetFeatureKey(ref featureKey);
        }

        public int iV_GetGeometryProfiles(int maxSize, ref StringBuilder profileNames)
        {
            return Unmanaged_GetGeometryProfiles(maxSize, ref profileNames);
        }

        public int iV_GetLicenseDueDate(ref DateStruct licenseDueDate)
        {
            return Unmanaged_GetLicenseDueDate(ref licenseDueDate);
        }

        public int iV_GetMonitorAttachedGeometry(StringBuilder profileName, ref MonitorAttachedGeometryStruct monitorAttachedGeometry)
        {
            return Unmanaged_GetMonitorAttachedGeometry(profileName, ref monitorAttachedGeometry);
        }

        public int iV_GetSample(ref SampleStruct rawDataSample)
        {
            return Unmanaged_GetSample(ref rawDataSample);
        }

        public int iV_GetSceneVideo(ref ImageStruct image)
        {
            return Unmanaged_GetSceneVideo(ref image);
        }

        public int iV_GetSerialNumber(ref StringBuilder serialNumber)
        {
            return Unmanaged_GetSerialNumber(ref serialNumber);
        }

        public int iV_GetSystemInfo(ref SystemInfoStruct systemInfo)
        {
            return Unmanaged_GetSystemInfo(ref systemInfo);
        }

        public int iV_GetTrackingMonitor(ref ImageStruct image)
        {
            return Unmanaged_GetTrackingMonitor(ref image);
        }

        public int iV_IsConnected()
        {
            return Unmanaged_IsConnected();
        }

        public int iV_LoadCalibration(StringBuilder name)
        {
            return Unmanaged_LoadCalibration(name);
        }

        public int iV_Log(StringBuilder message)
        {
            return Unmanaged_Log(message);
        }

        public int iV_PauseEyetracking()
        {
            return Unmanaged_PauseEyetracking();
        }

        public int iV_PauseRecording()
        {
            return Unmanaged_PauseRecording();
        }

        public int iV_Quit()
        {
            return Unmanaged_Quit();
        }

        public int iV_ReleaseAOIPort()
        {
            return Unmanaged_ReleaseAOIPort();
        }
        public int iV_RemoveAOI(StringBuilder aoiName)
        {
            return Unmanaged_RemoveAOI(aoiName);
        }
                       
        public int iV_ResetCalibrationPoints()
        {
            return Unmanaged_ResetCalibrationPoints();
        }

        public int iV_SaveCalibration(StringBuilder name)
        {
            return Unmanaged_SaveCalibration(name);
        }

        public int iV_SaveData(StringBuilder filename, StringBuilder description, StringBuilder user, int overwrite)
        {
            return Unmanaged_SaveData(filename, description, user, overwrite);
        }

        public int iV_SendCommand(StringBuilder etMessage)
        {
            return Unmanaged_SendCommand(etMessage);
        }

        public int iV_SendImageMessage(StringBuilder message)
        {
            return Unmanaged_SendImageMessage(message);
        }

        public void iV_SetCalibrationCallback(MulticastDelegate calibrationCallback)
        {
            Unmanaged_SetCalibrationCallback(calibrationCallback);
        }

        public void iV_SetConnectionTimeout(int time)
        {
            Unmanaged_SetConnectionTimeout(time);
        }

        public void iV_SetGeometryProfile(StringBuilder profileName)
        {
            Unmanaged_SetGeometryProfile(profileName);
        }

        public void iV_SetResolution(int stimulusWidth, int stimulusHeight)
        {
            Unmanaged_SetResolution(stimulusWidth, stimulusHeight);
        }

        public void iV_SetEventCallback(MulticastDelegate eventCallback)
        {
            Unmanaged_SetEventCallback(eventCallback);
        }

        public int iV_SetEventDetectionParameter(int minDuration, int maxDispersion)
        {
            return Unmanaged_SetEventDetectionParameter(minDuration, maxDispersion);
        }

        public void iV_SetEyeImageCallback(MulticastDelegate eyeImageCallback)
        {
            Unmanaged_SetEyeImageCallback(eyeImageCallback);
        }

        public int iV_SetLicense(StringBuilder key)
        {
            return Unmanaged_SetLicense(key);
        }

        public int iV_SetLogger(int logLevel, StringBuilder filename)
        {
            return Unmanaged_SetLogger(logLevel, filename);
        }

        public void iV_SetSampleCallback(MulticastDelegate sampleCallback)
        {
            Unmanaged_SetSampleCallback(sampleCallback);
        }

        public void iV_SetSceneVideoCallback(MulticastDelegate sceneVideoCallback)
        {
            Unmanaged_SetSceneVideoCallback(sceneVideoCallback);
        }

        public void iV_SetTrackingMonitorCallback(MulticastDelegate trackingMonitorCallback)
        {
            Unmanaged_SetTrackingMonitorCallback(trackingMonitorCallback);
        }

        public void iV_SetTrackingParameter(int ET_PARAM_EYE, int ET_PARAM, int value)
        {
            Unmanaged_SetTrackingParameter(ET_PARAM_EYE, ET_PARAM, value);
        }

        public int iV_SetupCalibration(ref CalibrationStruct calibrationData)
        {
            return Unmanaged_SetupCalibration(ref calibrationData);
        }

        public int iV_SetupMonitorAttachedGeometry(ref MonitorAttachedGeometryStruct monitorAttachedGeometry)
        {
            return Unmanaged_SetupMonitorAttachedGeometry(ref monitorAttachedGeometry);
        }

        public int iV_SetupStandAloneMode(ref StandAloneModeGeometryStruct standAloneModeGeometry)
        {
            return Unmanaged_SetupStandAloneMode(ref standAloneModeGeometry);
        }

        public int iV_ShowAccuracyMonitor()
        {
            return Unmanaged_ShowAccuracyMonitor();
        }

        public int iV_ShowEyeImageMonitor()
        {
            return Unmanaged_ShowEyeImageMonitor();
        }
				/// <summary>
				/// 
				/// </summary>
				/// <returns></returns>
        public int iV_ShowSceneVideoMonitor()
        {
            return Unmanaged_ShowSceneVideoMonitor();
        }
			/// <summary>
			/// 
			/// </summary>
			/// <returns></returns>
        public int iV_ShowTrackingMonitor()
        {
            return Unmanaged_ShowTrackingMonitor();
        }

			/// <summary>
			/// 
			/// </summary>
			/// <param name="etApplication"></param>
			/// <returns></returns>
        public int iV_Start(int etApplication)
        {
            return Unmanaged_Start(etApplication);
        }

        public int iV_StartRecording()
        {
            return Unmanaged_StartRecording();
        }

        public int iV_StopRecording()
        {
            return Unmanaged_StopRecording();
        }

        public int iV_TestTTL(int value)
        {
            return Unmanaged_TestTTL(value);
        }

        public int iV_Validate()
        {
            return Unmanaged_Validate();
        }

    }
}
