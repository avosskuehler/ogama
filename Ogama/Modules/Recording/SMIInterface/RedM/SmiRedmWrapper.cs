using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Ogama.Modules.Recording.SMIInterface.RedM
{
  /// <summary>
  /// 
  /// </summary>
	public class SmiRedmWrapper
	{
    /// <summary>
    /// The et device
    /// </summary>
		EyeTrackingController ETDevice;
    /// <summary>
    /// The m_ calibration data
    /// </summary>
		EyeTrackingController.CalibrationStruct m_CalibrationData;
    /// <summary>
    /// The m_ accuracy data
    /// </summary>
		EyeTrackingController.AccuracyStruct m_AccuracyData;
    ///// <summary>
    ///// The m_ sample data
    ///// </summary>
    //EyeTrackingController.SampleStruct m_SampleData;
    ///// <summary>
    ///// The m_ event data
    ///// </summary>
    //EyeTrackingController.EventStruct m_EventData;


		// callback routine declaration
    /// <summary>
    /// 
    /// </summary>
    /// <param name="calibrationPointData">The calibration point data.</param>
		private delegate void CalibrationCallback(EyeTrackingController.CalibrationPointStruct calibrationPointData);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sampleData">The sample data.</param>
		private delegate void GetSampleCallback(EyeTrackingController.SampleStruct sampleData);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="eventData">The event data.</param>
		private delegate void GetEventCallback(EyeTrackingController.EventStruct eventData);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="imageData">The image data.</param>
		private delegate void GetEyeImageCallback(EyeTrackingController.ImageStruct imageData);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="imageData">The image data.</param>
		private delegate void GetSceneVideoCallback(EyeTrackingController.ImageStruct imageData);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="imageData">The image data.</param>
		private delegate void GetTrackingMonitorCallback(EyeTrackingController.ImageStruct imageData);


		// callback function instances
    /// <summary>
    /// The m_ calibration callback
    /// </summary>
		CalibrationCallback m_CalibrationCallback;
    /// <summary>
    /// The m_ sample callback
    /// </summary>
		GetSampleCallback m_SampleCallback;
    /// <summary>
    /// The m_ event callback
    /// </summary>
		GetEventCallback m_EventCallback;
    /// <summary>
    /// The m_ eye image callback
    /// </summary>
		GetEyeImageCallback m_EyeImageCallback;
    /// <summary>
    /// The m_ scene video callback
    /// </summary>
		GetSceneVideoCallback m_SceneVideoCallback;
    /// <summary>
    /// The m_ tracking monitor callback
    /// </summary>
		GetTrackingMonitorCallback m_TrackingMonitorCallback;


		// delegates which will be used by invoking functions 
    /// <summary>
    /// 
    /// </summary>
		public delegate void DisplayTrackingMonitor();
    /// <summary>
    /// 
    /// </summary>
		public delegate void DisplayEyeImage();
    /// <summary>
    /// 
    /// </summary>
		public delegate void DisplaySceneVideo();
    /// <summary>
    /// 
    /// </summary>
		public delegate void DisplayLog1();
    /// <summary>
    /// 
    /// </summary>
		public delegate void DisplayLog2();
    /// <summary>
    /// 
    /// </summary>
		public delegate void DisplayLog3();

    /// <summary>
    /// Gets or sets the sendip.
    /// </summary>
    /// <value>
    /// The sendip.
    /// </value>
		public string sendip { get; set; }
    /// <summary>
    /// Gets or sets the sendport.
    /// </summary>
    /// <value>
    /// The sendport.
    /// </value>
		public int sendport { get; set; }
    /// <summary>
    /// Gets or sets the receiveip.
    /// </summary>
    /// <value>
    /// The receiveip.
    /// </value>
		public string receiveip { get; set; }
    /// <summary>
    /// Gets or sets the receiveport.
    /// </summary>
    /// <value>
    /// The receiveport.
    /// </value>
		public int receiveport { get; set; }
    /// <summary>
    /// The display device
    /// </summary>
		private int displayDevice = 0;
    /// <summary>
    /// The listener
    /// </summary>
		private SmiRedmWrapperListener listener;

    /// <summary>
    /// Registers the specified listener.
    /// </summary>
    /// <param name="listener">The listener.</param>
		public void register(SmiRedmWrapperListener listener)
		{
			this.listener = listener;
		}

    /// <summary>
    /// Sets the display device.
    /// </summary>
    /// <param name="display">The display.</param>
    /// <exception cref="System.Exception">only values of {0,1} expected.</exception>
		public void setDisplayDevice(int display)
		{
			if (display != 0 || display != 1)
			{
				throw new Exception("only values of {0,1} expected.");
			}
			this.displayDevice = display;
		}

    /// <summary>
    /// Gets the tracking monitor callback function.
    /// </summary>
    /// <param name="image">The image.</param>
		void GetTrackingMonitorCallbackFunction(EyeTrackingController.ImageStruct image)
		{
		
		}

    /// <summary>
    /// Gets the eye image callback function.
    /// </summary>
    /// <param name="image">The image.</param>
		void GetEyeImageCallbackFunction(EyeTrackingController.ImageStruct image)
		{
		
		}

    /// <summary>
    /// Gets the scene video callback function.
    /// </summary>
    /// <param name="image">The image.</param>
		void GetSceneVideoCallbackFunction(EyeTrackingController.ImageStruct image)
		{
			
		}

    /// <summary>
    /// Gets the sample callback function.
    /// </summary>
    /// <param name="sampleData">The sample data.</param>
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


    /// <summary>
    /// Gets the event callback function.
    /// </summary>
    /// <param name="eventData">The event data.</param>
		void GetEventCallbackFunction(EyeTrackingController.EventStruct eventData)
		{
			string data = ("Data from EventCallback - eye: " + eventData.eye.ToString() +
					" Event: " + eventData.eventType + " startTime: " + eventData.startTime.ToString() +
					" End:" + eventData.endTime.ToString() + " duration:" + eventData.duration.ToString() +
					" PosX:" + eventData.positionX.ToString() + " PosY:" + eventData.positionY.ToString());
			//log("GetEventCallbackFunction: " + data);
		}

    /// <summary>
    /// Calibrations the callback function.
    /// </summary>
    /// <param name="calibrationPoint">The calibration point.</param>
		void CalibrationCallbackFunction(EyeTrackingController.CalibrationPointStruct calibrationPoint)
		{
			string data = ("Data from CalibrationCallback - Number:" + calibrationPoint.number + " PosX:" + calibrationPoint.positionX + " PosY:" + calibrationPoint.positionY);
			log("CalibrationCallbackFunction: " + data);
		}

    /// <summary>
    /// Initializes this instance.
    /// </summary>
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

    /// <summary>
    /// Disconnectings this instance.
    /// </summary>
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

    /// <summary>
    /// Connects this instance.
    /// </summary>
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
    /// Calibrates the specified calibration points.
    /// </summary>
    /// <param name="calibrationPoints">The calibration points.</param>
    /// <param name="displayDevice">The display device.</param>
    /// <param name="targetSize">Size of the target.</param>
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

    /// <summary>
    /// Validates this instance.
    /// </summary>
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

    /// <summary>
    /// Getcalibrationpoints the specified calibration identifier.
    /// </summary>
    /// <param name="calibrationID">The calibration identifier.</param>
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

    /// <summary>
    /// Startrecordings this instance.
    /// </summary>
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

    /// <summary>
    /// Stoprecordings this instance.
    /// </summary>
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

    /// <summary>
    /// Getaccuracies this instance.
    /// </summary>
    /// <returns></returns>
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

    /// <summary>
    /// Logs the specified s.
    /// </summary>
    /// <param name="s">The s.</param>
		protected void log(string s)
		{
			Console.WriteLine("LOG:" + s);
		}


	}
}
