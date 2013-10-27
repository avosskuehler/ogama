using System;
using System.ComponentModel;
using System.Net;
using System.Net.Sockets;
using System.Text;
using GTNetworkClient;

namespace GTLibrary.Network
{
  using GTLibrary.Logging;

  public class UDPServer
    {
        #region Variables

        private readonly Socket socket;
        private readonly StreamFormat streamFormat;
        private IPEndPoint endPoint;

        private bool isEnabled;
        //private long prevTime;

        //private double prevX;
        //private double prevY;
        private bool sendSmoothedData = true;

        #endregion

        #region Constructor

        public UDPServer()
        {
            streamFormat = new StreamFormat();
            streamFormat.TimeStampMilliseconds = true;
            streamFormat.GazePosition = true;

            // Listen for changes in settings, start/stop
            GTSettings.Settings.Instance.Network.PropertyChanged += Network_PropertyChanged;

            try
            {
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            }
            catch (Exception ex)
            {
                ErrorLogger.ProcessException(ex, false);
            }
        }

        #endregion

        #region Get/Set

        public bool IsEnabled
        {
            get { return isEnabled; }
            set
            {
                if (isEnabled && value == false)
                    Stop();
                else if (isEnabled == false && value)
                    Start();

                isEnabled = value;
            }
        }

        public bool IsStreamingGazeData { get; set; }

        public bool SendSmoothedData
        {
            get { return sendSmoothedData; }
            set { sendSmoothedData = value; }
        }

        #endregion

        #region OnEvents

        private void Network_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "UDPServerEnabled")
            {
                if (GTSettings.Settings.Instance.Network.UDPServerEnabled)
                    Start();
                else
                    Stop();
            }

            if (e.PropertyName == "IPAddress")
            {
                Stop();
                Start();
            }
        }

        #endregion

        #region Start/Stop/Close

        private void Start()
        {
            IsStreamingGazeData = true;
            Console.Out.WriteLine("Network: UDP server started on " + 
                GTSettings.Settings.Instance.Network.UDPServerIPAddress +
                ":" + GTSettings.Settings.Instance.Network.UDPServerPort);
        }

        private void Stop()
        {
            IsStreamingGazeData = false;
            Console.Out.WriteLine("Network: UDP server stopped.");
        }

        public void Close()
        {
            try
            {
                isEnabled = false;
                socket.Close();
                socket.Disconnect(true);
            }
            catch (Exception e)
            {
                ErrorLogger.ProcessException(e, false);
            }
        }

        #endregion

        #region Send data/message

        public void SendTrackData(TrackData trackData)
        {
           // SendMessage(trackData.ToString());        
        }

        public void SendMessage(string message)
        {
            if (endPoint == null)
                endPoint = new IPEndPoint(IPAddress.Parse(GTSettings.Settings.Instance.Network.UDPServerIPAddress),
                                          GTSettings.Settings.Instance.Network.UDPServerPort);

            try
            {
                byte[] sendbuf = Encoding.ASCII.GetBytes(message);
                socket.SendTo(sendbuf, endPoint);
            }
            catch (Exception ex)
            {
                ErrorLogger.ProcessException(ex, false);
            }
        }

        public void SendMessage(string message, string parameter)
        {
            SendMessage(message + " " + parameter);
        }

        public void SendMessage(string message, int parameter)
        {
            SendMessage(message + " " + parameter);
        }

        public void SendGazeData(double x, double y)
        {
            long InstanceTime = DateTime.Now.Ticks/TimeSpan.TicksPerMillisecond;

            string message = Commands.StreamData + " ";

            if (streamFormat.TimeStampMilliseconds)
            {
                // ToDo: support milliseconds or micro seconds..
                //InstanceTime = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
                message += InstanceTime + " ";
            }

            if (streamFormat.GazePosition)
                message += Math.Round(x, 3) + " " + Math.Round(y, 3);

            SendMessage(message);
        }

        public void SendGazeData(double x, double y, double diameter)
        {
            streamFormat.PupilDiameter = true;

            long InstanceTime = DateTime.Now.Ticks/TimeSpan.TicksPerMillisecond;

            string message = Commands.StreamData + " ";

            if (streamFormat.TimeStampMilliseconds)
            {
                // ToDo: support milliseconds or micro seconds..
                //InstanceTime = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
                message += InstanceTime;
            }

            if (streamFormat.GazePosition)
                message += " " + Math.Round(x, 3) + " " + Math.Round(y, 3);

            if (streamFormat.PupilDiameter)
                message += " " + Math.Round(diameter, 3);

            SendMessage(message);
        }

        #endregion
    }
}