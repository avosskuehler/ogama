using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace GazeTrackerClient
{
    public class ClientData: IDisposable
    {
        private Thread workerThread = null;
        UdpClient client;
        IPEndPoint anyIP;

        IPAddress ipAddress = IPAddress.Loopback;
        int port = 64555;

        string dataReceived = "0";
        string previousDataReceived = "1";
        GazeData gazeData = new GazeData();

        MouseControl gazeMouse = new MouseControl();
        private bool mouseRedirect = false;

        public delegate void GazeDataHandler(GazeData gData);
        public event GazeDataHandler OnGazeData;



        public ClientData()
        {
            client = new UdpClient(port);
            workerThread = new Thread(this.Worker);
        }

        public ClientData(int udpPort)
        {
            client = new UdpClient(udpPort);
            workerThread = new Thread(this.Worker);
        }


        public void Start()
        {  
            if (workerThread.ThreadState == ThreadState.Unstarted)
                workerThread.Start();
            else if (workerThread.ThreadState == ThreadState.Stopped)
                workerThread.Start();
            else if (workerThread.ThreadState == ThreadState.Suspended)
                workerThread.Resume();
        }

        public void Stop()
        { 
            if(workerThread.IsAlive)
                workerThread.Suspend();
        }

        public void Dispose()
        {
            client.Close();
            workerThread.Join();
        }


        private void Worker()
        {
            while (true)
            {
                    try
                    {
                        anyIP = new IPEndPoint(IPAddress, 0);
                        byte[] data = client.Receive(ref anyIP);
                        string dataReceived = Encoding.UTF8.GetString(data);

                        if (dataReceived != previousDataReceived)
                        {
                            previousDataReceived = dataReceived;

                            string[] tmp = dataReceived.Split(' ');
                            gazeData.TimeStamp = int.Parse(tmp[0]);
                            gazeData.GazePositionX = double.Parse(tmp[1]);
                            gazeData.GazePositionY = double.Parse(tmp[2]);

                            if (mouseRedirect)
                            {
                                gazeMouse.Move(gazeData);
                            }

                            if(OnGazeData != null)
                                OnGazeData(gazeData);

                        }
                    }
                    catch (Exception ex)
                    {
                        Console.Out.WriteLine("Error in UDPClient, could not parse data : " + ex.Message);
                    }
                    
                    Thread.Sleep(10);
            }
         }



        #region get / set


        public IPAddress IPAddress
        {
            get { return ipAddress; }
            set { ipAddress = value; }
        }

        public int Port 
        {
            get { return port; }
            set { 
                port = value;
                client = new UdpClient(port);
            }
        }

        public GazeData GazeData 
        {
            get { return gazeData; }
            set { gazeData = value; }
        }

        public bool MouseRedirect
        {
            get { return mouseRedirect; }
            set { mouseRedirect = value; }
        }

        #endregion

        
    }
}
