using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Ogama.Modules.Recording.SMIInterface.RedM
{
	public class SmiRedmWrapper
	{
		EyeTrackingController ETDevice;
		EyeTrackingController.CalibrationStruct m_CalibrationData;
		EyeTrackingController.AccuracyStruct m_AccuracyData;
		EyeTrackingController.SampleStruct m_SampleData;
		EyeTrackingController.EventStruct m_EventData;


		// callback routine declaration
		private delegate void CalibrationCallback(EyeTrackingController.CalibrationPointStruct calibrationPointData);
		private delegate void GetSampleCallback(EyeTrackingController.SampleStruct sampleData);
		private delegate void GetEventCallback(EyeTrackingController.EventStruct eventData);
		private delegate void GetEyeImageCallback(EyeTrackingController.ImageStruct imageData);
		private delegate void GetSceneVideoCallback(EyeTrackingController.ImageStruct imageData);
		private delegate void GetTrackingMonitorCallback(EyeTrackingController.ImageStruct imageData);


		// callback function instances
		CalibrationCallback m_CalibrationCallback;
		GetSampleCallback m_SampleCallback;
		GetEventCallback m_EventCallback;
		GetEyeImageCallback m_EyeImageCallback;
		GetSceneVideoCallback m_SceneVideoCallback;
		GetTrackingMonitorCallback m_TrackingMonitorCallback;


		// delegates which will be used by invoking functions 
		public delegate void DisplayTrackingMonitor();
		public delegate void DisplayEyeImage();
		public delegate void DisplaySceneVideo();
		public delegate void DisplayLog1();
		public delegate void DisplayLog2();
		public delegate void DisplayLog3();

		public string sendip { get; set; }
		public int sendport { get; set; }
		public string receiveip { get; set; }
		public int receiveport { get; set; }
		private int displayDevice = 0;
		private SmiRedmWrapperListener listener;

		public void register(SmiRedmWrapperListener listener)
		{
			this.listener = listener;
		}

		public void setDisplayDevice(int display)
		{
			if (display != 0 || display != 1)
			{
				throw new Exception("only values of {0,1} expected.");
			}
			this.displayDevice = display;
		}

		void GetTrackingMonitorCallbackFunction(EyeTrackingController.ImageStruct image)
		{
		
		}

		void GetEyeImageCallbackFunction(EyeTrackingController.ImageStruct image)
		{
		
		}

		void GetSceneVideoCallbackFunction(EyeTrackingController.ImageStruct image)
		{
			
		}

		void GetSampleCallbackFunction(EyeTrackingController.SampleStruct sampleData)
		{
			string data = ("Data from SampleCallback - timestamp: " + sampleData.timestamp.ToString() +
					" - GazeRX: " + sampleData.rightEye.gazeX.ToString() +
					" - GazeRY: " + sampleData.rightEye.gazeY.ToString() +
					" - GazeLX: " + sampleData.leftEye.gazeX.ToString() +
					" - GazeLY: " + sampleData.leftEye.gazeY.ToString() +
					" - DiamRX: " + sampleData.rightEye.diam.ToString() +
					" - DiamLX: " + sampleData.leftEye.diam.ToString() +
					" - DistanceR: " + sampleData.rightEye.eyePositionZ.ToString() +
					" - DistanceL: " + sampleData.leftEye.eyePositionZ.ToString());
			//log("GetSampleCallbackFunction: " + data);
			if (listener != null)
			{
				listener.onSampleData(sampleData);
			}
		}


		void GetEventCallbackFunction(EyeTrackingController.EventStruct eventData)
		{
			string data = ("Data from EventCallback - eye: " + eventData.eye.ToString() +
					" Event: " + eventData.eventType + " startTime: " + eventData.startTime.ToString() +
					" End:" + eventData.endTime.ToString() + " duration:" + eventData.duration.ToString() +
					" PosX:" + eventData.positionX.ToString() + " PosY:" + eventData.positionY.ToString());
			//log("GetEventCallbackFunction: " + data);
		}

		void CalibrationCallbackFunction(EyeTrackingController.CalibrationPointStruct calibrationPoint)
		{
			string data = ("Data from CalibrationCallback - Number:" + calibrationPoint.number + " PosX:" + calibrationPoint.positionX + " PosY:" + calibrationPoint.positionY);
			log("CalibrationCallbackFunction: " + data);
		}

		public void initialize()
		{
			ETDevice = new EyeTrackingController();
			 m_CalibrationCallback = new CalibrationCallback(CalibrationCallbackFunction);
       m_SampleCallback = new GetSampleCallback(GetSampleCallbackFunction);
       m_EventCallback = new GetEventCallback(GetEventCallbackFunction);
       m_EyeImageCallback = new GetEyeImageCallback(GetEyeImageCallbackFunction);
       m_SceneVideoCallback = new GetSceneVideoCallback(GetSceneVideoCallbackFunction);
       m_TrackingMonitorCallback = new GetTrackingMonitorCallback(GetTrackingMonitorCallbackFunction);
		}

		public void disconnecting()
		{
			int ret = 0;

			try
			{
				ret = ETDevice.iV_Disconnect();
				if (ret == 1) log("iV_Disconnect: connection established");
				if (ret == 124) log("iV_Disconnect error: could not delete sockets");
			}
			catch (System.Exception exc)
			{
				log("Exception during iV_Disconnect: " + exc.Message);
			}
		}

		public void connect()
		{
			int ret = 0;

			try
			{
				
				
				ETDevice.iV_SetCalibrationCallback(m_CalibrationCallback);
				ETDevice.iV_SetSampleCallback(m_SampleCallback);
				ETDevice.iV_SetEventCallback(m_EventCallback);
				ETDevice.iV_SetEyeImageCallback(m_EyeImageCallback);
				ETDevice.iV_SetSceneVideoCallback(m_SceneVideoCallback);
				ETDevice.iV_SetTrackingMonitorCallback(m_TrackingMonitorCallback);
				log("connect, sendip: " + sendip + ", sendport: " + sendport + ", receiveip: " + receiveip + ", receiveport: " + receiveport);
				ret = ETDevice.iV_Connect(new StringBuilder(sendip), Convert.ToInt32(sendport), new StringBuilder(receiveip), Convert.ToInt32(receiveport));
				if (ret == 1) log("iV_Connect: connection established");
				if (ret == 100) log("iV_Connect: failed to establish connection");
				if (ret == 112) log("iV_Connect error: wrong parameter");
				if (ret == 123) log("iV_Connect error: failed to bind sockets");

			}
			catch (Exception exc)
			{
				log("Exception during iV_Connect: " + exc.Message);
			}

		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="calibrationPoints" 1,2,5,8,9,13></param>
		/// <param name="displayDevice" 0,1></param>
		/// <param name="targetSize" 20></param>
		public void calibrate(	int calibrationPoints, int displayDevice, int targetSize)
		{
			int ret = 0;
			
			try
			{

				m_CalibrationData.displayDevice = displayDevice;
				m_CalibrationData.autoAccept = 1;
				m_CalibrationData.method = calibrationPoints;
				m_CalibrationData.visualization = 1;
				m_CalibrationData.speed = 0;
				m_CalibrationData.targetShape = 2;
				m_CalibrationData.backgroundColor = 230;
				m_CalibrationData.foregroundColor = 250;
				m_CalibrationData.targetSize = targetSize;
				m_CalibrationData.targetFilename = "";

				ret = ETDevice.iV_SetupCalibration(ref m_CalibrationData);
				if (ret == 1) log("iV_SetupCalibration: Calibration set up successfully");
				if (ret == 101) log("iV_SetupCalibration: no connection established");
				if (ret == 111) log("iV_SetupCalibration: eye tracking device required for this function is not connected");
				if (ret == 112) log("iV_SetupCalibration: parameter out of range");
				if (ret == 113) log("iV_SetupCalibration: eye tracking device required for this calibration method is not connected");

				ret = ETDevice.iV_Calibrate();
				if (ret == 1) log("iV_Calibrate: Calibration started successfully");
				if (ret == 3) log("iV_Calibrate: Calibration was aborted");
				if (ret == 101) log("iV_Calibrate: no connection established");
				if (ret == 111) log("iV_Calibrate: eye tracking device required for this function is not connected");
				if (ret == 113) log("iV_Calibrate: eye tracking device required for this calibration method is not connected");
				if (ret == 131) log("iV_Calibrate: no response from iViewX");

			}
			catch (System.Exception exc)
			{
				log("Calibration Exception: " + exc.Message);
			}

		}

		public void validate()
		{
			int ret = 0;

			try
			{
				ret = ETDevice.iV_Validate();
				if (ret == 1) log("iV_Validate: Validation started successfully");
				if (ret == 3) log("iV_Validate: Validation was aborted");
				if (ret == 101) log("iV_Validate: no connection established");
				if (ret == 111) log("iV_Validate: eye tracking device required for this function is not connected");
				if (ret == 113) log("iV_Validate: eye tracking device required for this validation method is not connected");


			}
			catch (System.Exception exc)
			{
				log("Validation Exception: " + exc.Message);
			}
		}

		public void getcalibrationpoint(string calibrationID)
		{
			int ret = 0;

			try
			{
				ret = ETDevice.iV_SaveCalibration(new StringBuilder(calibrationID));
				if (ret == 101) log("iV_SaveCalibration: no connection established");
				if (ret == 111) log("iV_SaveCalibration: eye tracking device required for this function is not connected");
				if (ret == 113) log("iV_SaveCalibration: eye tracking device required for this calibration method is not connected");
			}
			catch (System.Exception exc)
			{
				log("Save Calibration Exception: " + exc.Message);
			}
		}

		public void startrecording()
		{
			int ret = 0;

			try
			{
				ret = ETDevice.iV_StartRecording();
				if (ret == 101) log("iV_StartRecording: no connection established");
				if (ret == 111) log("iV_StartRecording: eye tracking device required for this function is not connected");
			}
			catch (System.Exception exc)
			{
				log("iV_StartRecording Exception: " + exc.Message);
			}
		}

		public void stoprecording()
		{
			int ret = 0;

			try
			{
				ret = ETDevice.iV_StopRecording();
				if (ret == 101) log("iV_StopRecording: no connection established");
				if (ret == 111) log("iV_StopRecording: eye tracking device required for this function is not connected");
			}
			catch (System.Exception exc)
			{
				log("iV_StopRecording Exception: " + exc.Message);
			}
		}

		public string getaccuracy()
		{
			int ret = 0;
			string result = "";
			try
			{
				ret = ETDevice.iV_GetAccuracy(ref m_AccuracyData, 1);
				if (ret == 1) result = "Dev X: " + m_AccuracyData.deviationXLeft + " Dev Y: " + m_AccuracyData.deviationYLeft;
				if (ret == 2) result = "iV_GetAccuracy: no new data  available";
				if (ret == 101) result = "iV_GetAccuracy: no connection established";
				if (ret == 103) result = "iV_GetAccuracy: system is not validated ";
				if (ret == 112) result = "iV_GetAccuracy: parameter out of range";
			}
			catch (System.Exception exc)
			{
				result = "iV_GetAccuracy Exception: " + exc.Message;
			}
			return result;
		}

		protected void log(string s)
		{
			Console.WriteLine("LOG:" + s);
		}


	}
}
