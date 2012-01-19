using System.Windows;
using GTCommons.Commands;
using System;
using System.Collections.Generic;

namespace GTCommons
{
    public class GTCommands : Window
    {
        #region Variables 

        private static GTCommands instance;

        private readonly AutotuneCommands autotuneCommands;
        private readonly CalibrationCommands calibrationCommands;
        private readonly CameraCommands cameraCommands;
        private readonly SettingsCommands settingsCommands;
        private readonly TrackerViewerCommands videoViewerCommands;

        #endregion

        #region Events

        public static readonly RoutedEvent NetworkClientEvent = EventManager.RegisterRoutedEvent("NetworkClientEvent",
                                                                                                 RoutingStrategy.Bubble,
                                                                                                 typeof (RoutedEventHandler),
                                                                                                 typeof (GTCommands));

        public static readonly RoutedEvent TrackQualityEvent = EventManager.RegisterRoutedEvent("TrackStatsEvent",
                                                                                                RoutingStrategy.Bubble,
                                                                                                typeof (RoutedEventHandler),
                                                                                                typeof (GTCommands));

        #endregion

        #region Constructor

        private GTCommands()
        {
            settingsCommands = new SettingsCommands();
            calibrationCommands = new CalibrationCommands();
            autotuneCommands = new AutotuneCommands();
            videoViewerCommands = new TrackerViewerCommands();
            cameraCommands = new CameraCommands();
        }

        #endregion

        #region EventHandlers

        public event RoutedEventHandler OnNetworkClient
        {
            add { base.AddHandler(NetworkClientEvent, value); }
            remove { base.RemoveHandler(NetworkClientEvent, value); }
        }

        public event RoutedEventHandler OnTrackingQuality
        {
            add { base.AddHandler(TrackQualityEvent, value); }
            remove { base.RemoveHandler(TrackQualityEvent, value); }
        }

        #endregion

        #region Raise events

        public void NetworkClient()
        {
            var args1 = new RoutedEventArgs();
            args1 = new RoutedEventArgs(NetworkClientEvent, this);
            RaiseEvent(args1);
        }

        public void TrackQuality()
        {
            var args1 = new RoutedEventArgs();
            args1 = new RoutedEventArgs(TrackQualityEvent, this);
            RaiseEvent(args1);
        }

        #endregion

        #region Get/Set

        public SettingsCommands Settings
        {
            get { return settingsCommands; }
        }

        public CalibrationCommands Calibration
        {
            get { return calibrationCommands; }
        }

        public AutotuneCommands Autotune
        {
            get { return autotuneCommands; }
        }

        public TrackerViewerCommands TrackerViewer
        {
            get { return videoViewerCommands; }
        }

        public CameraCommands Camera
        {
            get { return cameraCommands; }
        }

        public static GTCommands Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GTCommands();
                }

                return instance;
            }
        }

        #endregion

		#region Public methods (parse and execute)

		public void ParseAndExecuteCommand(string command)
		{
			if (command == null) return;

			char[] seperator = { ' ' };
			string[] cmd = command.Split(seperator, 50);

			string cmdStr = cmd[0];
			string cmdParam = "";

			if (cmd.Length > 1)
				cmdParam = cmd[1];

			Console.Out.WriteLine("Network command: " + cmdStr + " " + cmdParam);

			switch (cmdStr)
			{
				//    case Commands.TrackerStatus:
				//        if (OnTrackerStatus != null)
				//            OnTrackerStatus();
				//        break;


				//    #region Data Output

				//   case Commands.StreamStart:
				//        if (OnDataStreamStart != null)
				//            OnDataStreamStart();
				//        break;

				//    case Commands.StreamStop:
				//        if (OnDataStreamStop != null)
				//            OnDataStreamStop();
				//        break;

				//    case Commands.StreamFormat:
				//        // No returning parameters
				//        break;

				//    #endregion


				//    #region Calibration

				case Protocol.CalibrationStart:
					Calibration.Start();
					break;

				//    case Commands.CalibrationAcceptPoint:
				//        // No returning parameter/data 
				//        break;

				case Protocol.CalibrationAbort:
					Calibration.Abort();
					break;


				//    case Commands.CalibrationPointChange:
				//        //OnCalibrationPointChange(Int32.Parse(cmd[1])); // Next point number
				//        break;

				//    case Commands.CalibrationParameters:

				//        CalibrationParameters calParams = new CalibrationParameters();
				//        calParams.ExtractParametersFromString(cmdParam);

				//        if (OnCalibrationParameters != null)
				//            OnCalibrationParameters(calParams);

				//        break;


				//    //case CalibrationAreaSize:
				//    //    break;

				//    //case CalibratitonEnd:
				//    //    if (OnCalibratitonEnd != null)
				//    //        OnCalibratitonEnd();
				//    //    break;

				//    //case CalibrationCheckLevel:
				//    //    if (OnCalibrationCheckLevel != null)
				//    //        OnCalibrationCheckLevel(Int32.Parse(cmd[1]));
				//    //    break;

				case Protocol.CalibrationPoint:
					//Console.WriteLine("New calibration point from dedicated interface: " + command);

					//How many points have been buffeded?
					List<int> CalPointsIndex = new List<int>();
					for (int c = 0; c < cmd.Length; c++)
						if (cmd[c] == "CAL_POINT")
							CalPointsIndex.Add(c);

					for (int c = 0; c < CalPointsIndex.Count; c++)
					{
						//OnCalibrationFeedbackPoint(
						//    long.Parse(cmd[CalPointsIndex[c] + 1]),     //time
						//    int.Parse(cmd[CalPointsIndex[c] + 2]),      //packace number
						//    int.Parse(cmd[CalPointsIndex[c] + 3]),      //targetX
						//    int.Parse(cmd[CalPointsIndex[c] + 4]),      //targetY
						//    int.Parse(cmd[CalPointsIndex[c] + 5]),      //gazeX
						//    int.Parse(cmd[CalPointsIndex[c] + 6]),      //gazeY
						//    float.Parse(cmd[CalPointsIndex[c] + 7]),    //distance - will not be used
						//    int.Parse(cmd[CalPointsIndex[c] + 8]));     //acquisition time 
					}
					break;

				case Protocol.CalibrationUpdateMethod:
					Console.Write("Calibration Update Method Changed to:" + cmdParam);
					//onCalibrationUpdateMethod(int.Parse(cmdParam));
					//Settings.Instance.Calibration.RecalibrationType = GazeTrackingLibrary.Utils.RecalibrationTypeEnum.Continuous
					break;


				//    //case CalibrationStartDriftCorrection:
				//    //    break;

				//    //case CalibrationValidate:
				//    //  if (OnCalibrationValidate != null)
				//    //     OnCalibrationValidate( cmd[1], double.Parse(cmd[2]), double.Parse(cmd[3]), double.Parse(cmd[4]));
				//    //break;

				//    #endregion


				#region Logging

				#endregion

				//    case Commands.CameraParameters:

				//        // Update camera settings
				//        Settings.Instance.Camera.ExtractParametersFromString(cmdParam);

				//        //if (OnCameraSettings != null)
				//        //    OnCameraSettings(camSettings);

				//        break;

				#region GT U.I

				#endregion
			}
		}

		#endregion
    }
}