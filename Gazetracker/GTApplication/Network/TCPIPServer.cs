using System;
using System.ComponentModel;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

using GTNetworkClient;
using GTLibrary.Logging;
using GTCommons;
using System.Windows.Threading;

namespace GTApplication.Network
{
	public class TCPIPServer
	{
    //private bool isEnabled;
		private Thread listenThread;
		private const int messageLength = 512; //bytes
		private TcpClient tcpClient;
		private TcpListener tcpListener;
		private bool isRunning = false;

		#region Commands

		#region Delegates

		public delegate void TrackerStatusHandler();

		#endregion

    //public event TrackerStatusHandler OnTrackerStatus;

		#region Calibration

		#region Delegates

		public delegate void CalibrationAbortHandler();

		/* Henrik & Javier Test */
		public delegate void CalibrationFeedbackPointHandler(
			long time, int packagenumber, int targetX, int targetY, int gazeX, int gazeY, float distance,
			int acquisitionTime);
		public delegate void CalibrationUpdateMethod(int method);
		/* test end */

		public delegate void CalibrationParametersHandler(CalibrationParameters calParams);

		public delegate void CalibrationStartHandler();

		#endregion

    //public event CalibrationStartHandler OnCalibrationStart;

    //public event CalibrationAbortHandler OnCalibrationAbort;

    //public event CalibrationParametersHandler OnCalibrationParameters;

		/* Henrik & Javier Test */
    //public event CalibrationFeedbackPointHandler OnCalibrationFeedbackPoint; //new calibration feedback point
    //public event CalibrationUpdateMethod onCalibrationUpdateMethod; //change the update method in the tracker
		/* test end */

		#endregion

		#region DataStream

		#region Delegates

		public delegate void DataStreamStartHandler();

		public delegate void DataStreamStopHandler();

		#endregion

    //public event DataStreamStartHandler OnDataStreamStart;

    //public event DataStreamStartHandler OnDataStreamStop;

		#endregion

		#region Log

		#region Delegates

		public delegate void LogPathGetHandler();

		public delegate void LogPathSetHandler(string path);

		public delegate void LogStartHandler();

		public delegate void LogStopHandler();

		public delegate void LogWriteLineHandler(string line);

		#endregion

    //public event LogStartHandler OnLogStart;
    //public event LogStopHandler OnLogStop;

    //public event LogWriteLineHandler OnLogWriteLine;

    //public event LogPathGetHandler OnLogPathGet;

    //public event LogPathSetHandler OnLogPathSet;

		#endregion

		#region Camera

		#region Delegates

		public delegate void CameraSettingsHandler(GTSettings.Camera camSettings);

		#endregion

    //public event CameraSettingsHandler OnCameraSettings;

		#endregion

		#region GTApplication

		#region Delegates

		public delegate void UIMinimizeHandler();

		public delegate void UIRestoreHandler();

		public delegate void UISettingsCameraHandle();

		public delegate void UISettingsHandle();

		#endregion

    //public event UIMinimizeHandler OnUIMinimize;
    //public event UIRestoreHandler OnUIRestore;

    //public event UISettingsHandle OnUISettings;

    //public event UISettingsCameraHandle OnUISettingsCamera;

		#endregion

		#endregion


		public TCPIPServer()
		{
			tcpClient = new TcpClient();
			GTSettings.Settings.Instance.Network.PropertyChanged += Network_PropertyChanged;
		}

		private void Network_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			//if (e.PropertyName == "TCPIPServerEnabled")
			//{
			//    if (Settings.Instance.Network.TCPIPServerEnabled && isRunning)
			//        Start();
			//    else
			//        Stop();
			//}

			if (e.PropertyName == "TCPIPServerIPAddress" && isRunning == true)
			{
				Stop();
				Start();
			}
		}

		public void Start()
		{
			// Global TCP/IP listner
            tcpListener = new TcpListener(IPAddress.Parse(GTSettings.Settings.Instance.Network.TCPIPServerIPAddress), GTSettings.Settings.Instance.Network.TCPIPServerPort);

			// Stop previous if running
			if (listenThread != null && listenThread.IsAlive)
				listenThread.Join();

			// Runs in a thread
			listenThread = new Thread(StartListening);
			listenThread.SetApartmentState(ApartmentState.STA);
			listenThread.Start();

            Console.Out.WriteLine("Network: Tcp/ip server started on " + GTSettings.Settings.Instance.Network.TCPIPServerIPAddress + " : " + GTSettings.Settings.Instance.Network.TCPIPServerPort);
		}

		private void Stop()
		{
			StopListening();
			Console.Out.WriteLine("Network: Tcp/Ip server stopped.");
		}

		public void StartListening()
		{
			if (isRunning)
				return;

			IPEndPoint ipEndPoint;
            ipEndPoint = new IPEndPoint(IPAddress.Parse(GTSettings.Settings.Instance.Network.TCPIPServerIPAddress), GTSettings.Settings.Instance.Network.TCPIPServerPort);
			tcpListener = new TcpListener(ipEndPoint);

			if (tcpListener != null)
			{
				try
				{
					tcpListener.Start();
					tcpListener.BeginAcceptTcpClient(OnAcceptConnection, tcpListener);
				}
				catch (Exception)
				{
					System.Windows.MessageBox.Show("Could not start the TCP/IP command server, has the IP changed?");
					isRunning = false;
					return;
				}
			}

			isRunning = true;
		}

		private void StopListening()
		{
			if (!isRunning)
				return;

			if (tcpListener != null)
				tcpListener.Stop();

			isRunning = false;
		}


		private void OnAcceptConnection(IAsyncResult result)
		{
		    if (result.AsyncState != null)
		    {
		        TcpListener listener = result.AsyncState as TcpListener;

		        try
		        {
		            if (this.isRunning)
		            {
		                //start accepting the next connection...
		                listener.BeginAcceptTcpClient(OnAcceptConnection, listener);
		            }
		            else
		            {
		                //someone called Stop() - don't call EndAcceptTcpClient because
		                //it will throw an ObjectDisposedException
		                return;
		            }

		            // Parse command
		            ParseClientCommand(listener.EndAcceptTcpClient(result)); // returns a TcpClient
		        }
		        catch (Exception ex)
		        {
		            Console.Out.WriteLine("OnAcceptConnection: " + ex.Message);
		        }
		    }
		}

		private void ParseClientCommand(TcpClient client)
		{
			if (client.Connected == false)
				return;

			NetworkStream clientStream = client.GetStream();
			byte[] message = new byte[messageLength]; // used to be 4096

			while (true)
			{
				Thread.Sleep(10);

				int bytesRead = 0;

				try
				{
					//blocks until a client sends a message
					bytesRead = clientStream.Read(message, 0, messageLength);
				}
				catch (Exception ex)
				{
					//a socket error has occured
					ErrorLogger.ProcessException(ex, true);
					break;
				}

				//if (bytesRead == 0)
				//{
				//    //the client has disconnected from the server
				//    break;
				//}

				// Message is encoded
				ASCIIEncoding encoder = new ASCIIEncoding();

				// Extract command
				string command = encoder.GetString(message, 0, bytesRead);

				// Raise event/command
				if (command.Length > 0)
				{
					GTCommands.Instance.Dispatcher.Invoke(DispatcherPriority.ApplicationIdle, new Action(delegate
					{
						GTCommands.Instance.ParseAndExecuteCommand(command);
					}));
				}
			}
		}



		private void ReadClientStream(TcpClient client)
		{
			try
			{
				NetworkStream clientStream = tcpClient.GetStream();
				byte[] message = new byte[messageLength]; // used to be 4096

				while (true)
				{
					int bytesRead = 0;

					try
					{
						//blocks until a client sends a message
						bytesRead = clientStream.Read(message, 0, messageLength);
					}
					catch (Exception ex)
					{
						//a socket error has occured
						ErrorLogger.ProcessException(ex, true);
						break;
					}

					if (bytesRead == 0)
					{
						//the client has disconnected from the server
						break;
					}

					// Message is encoded
					ASCIIEncoding encoder = new ASCIIEncoding();

					// Extract command
					string command = encoder.GetString(message, 0, bytesRead);

					// Raise event/command
					GTCommands.Instance.ParseAndExecuteCommand(command);
				}
			}
			catch (Exception ex)
			{
				Console.Out.WriteLine("TCPIPServer: " + ex.Message);
			}
		}


		//public static void onAcceptConnection(IAsyncResult ar)
		//{
		//    // Get the listener that handles the client request.
		//    TcpListener listener = (TcpListener)ar.AsyncState;

		//    // End the operation and display the received data on the console.
		//    TcpClient client = listener.EndAcceptTcpClient(ar);

		//    NetworkStream clientStream = tcpClient.GetStream();

		//    byte[] message = new byte[messageLength]; // used to be 4096

		//    while (true)
		//    {
		//        int bytesRead = 0;

		//        try
		//        {
		//            //blocks until a client sends a message
		//            bytesRead = clientStream.Read(message, 0, messageLength);
		//        }
		//        catch (Exception ex)
		//        {
		//            //a socket error has occured
		//            ErrorLogger.ProcessException(ex, true);
		//            break;
		//        }

		//        if (bytesRead == 0)
		//        {
		//            //the client has disconnected from the server
		//            break;
		//        }

		//        // Message is encoded
		//        ASCIIEncoding encoder = new ASCIIEncoding();

		//        // Extract command
		//        string command = encoder.GetString(message, 0, bytesRead);

		//        // Raise event/command
		//        GTCommands.Instance.ParseAndExecuteCommand(command);

		//    }

		//    //tcpClient.Close(); //I'm keeping the connection open
		//}




		private void SendMessage(string message)
		{
			NetworkStream clientStream = tcpClient.GetStream();
			var encoder = new ASCIIEncoding();
			byte[] buffer = encoder.GetBytes(message);

			clientStream.Write(buffer, 0, buffer.Length);
			clientStream.Flush();
		}

		#region Get/Set


		internal void Close()
		{
			if (tcpClient != null)
				tcpClient.Close();

			listenThread.Abort();
		}

		#endregion


	}
}