namespace GTCommons 
{
    public class Protocol 
    {

        public const string TrackerStatus = "TRACKER_STATUS";
        public const string EyeTrackerParameters = "TRACKER_ENGINE_PARAMS";
        public const string SampleRate = "TRACKER_SAMPLERATE";

        public const string UIMinimize = "UI_MINIMIZE";
        public const string UIRestore = "UI_RESTORE";
        public const string UISettings = "UI_SETTINGS";
        public const string UISettingsCamera = "UI_SETTINGS_CAM";

        public const string CalibrationAcceptPoint = "CAL_ACCEPT_POINT";
        public const string CalibrationAbort = "CAL_ABORT";
        public const string CalibrationStart = "CAL_START";
        public const string CalibrationPointChange = "CAL_POINT_CHANGE";
        public const string CalibrationParameters = "CAL_PARAMS";
        public const string CalibrationAreaSize = "CAL_AREA_SIZE";
        public const string CalibrationDefault = "CAL_DEFAULT";
        public const string CalibrationEnd = "CAL_END";
        public const string CalibrationPoint = "CAL_POINT";
        public const string CalibrationStartDriftCorrection = "CAL_START_DRIFT";
        public const string CalibrationCheckLevel = "CAL_CHECK_LEVEL";
        public const string CalibrationValidate = "CAL_VALIDATE";
        public const string CalibrationQuality = "CAL_QUALITY";
        public const string CalibrationUpdateMethod = "CAL_UPDATE_METHOD";

        public const string StreamStart = "STREAM_START";
        public const string StreamStop = "STREAM_STOP";
        public const string StreamFormat = "STREAM_FORMAT";
        public const string StreamData = "STREAM_DATA";

        public const string LogStart = "LOG_START";
        public const string LogStop = "LOG_STOP";
        public const string LogPathSet = "LOG_PATH_SET";
        public const string LogPathGet = "LOG_PATH_GET";
        public const string LogWriteLine = "LOG_WRITE";

        public const string CameraSetDeviceMode = "CAM_SET_DEVICE_MODE";
        public const string CameraParameters = "CAM_PARAMETERS";
        public const string CameraIsFound = "CAM_ISFOUND";

        public const string MouseDriverStart = "MOUSE_DRIVER_START";
        public const string MouseDriverStop = "MOUSE_DRIVER_STOP";
        public const string SmoothGazeOn = "SMOOTHING_ON";
        public const string SmoothGazeOff = "SMOOTHING_OFF";

        // Not implemented yet..
        public const string VideoRecordingStart = "VIDEOREC_START";
        public const string VideoRecordingStop = "VIDEOREC_STOP";
        public const string VideoRecordingSave = "VIDEO_SAVE";
        public const string VideoRecordingClearBuffer = "VIDEO_CLEAR";

        public const string EyeImageStartSending = "EYEIMAGE_START";
        public const string EyeImageStopSending = "EYEIMAGE_STOP";
        public const string EyeImageCaptured = "EYEIMAGE_CAPTURE";
        public const string EyeImageRecordingStart = "EYEIMAGE_RECORDING_START";
        public const string EyeImageRecordingStop = "EYEIMAGE_RECORDING_STOP";

    }
}
