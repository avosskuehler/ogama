// <copyright file="Client.cs" company="ITU">
// ******************************************************
// GazeTrackingClient for ITU GazeTracker
// Copyright (C) 2010 Martin Tall  
// ------------------------------------------------------------------------
// This program is free software; you can redistribute it and/or modify it 
// under the terms of the GNU General Public License as published by the 
// Free Software Foundation; either version 3 of the License, 
// or (at your option) any later version.
// This program is distributed in the hope that it will be useful, 
// but WITHOUT ANY WARRANTY; without even the implied warranty of 
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU 
// General Public License for more details.
// You should have received a copy of the GNU General Public License 
// along with this program; if not, see http://www.gnu.org/licenses/.
// **************************************************************
// </copyright>
// <author>Adrian Voßkühler</author>
// <email>adrian@ogama.net</email>

namespace GTNetworkClient
{
  using System;
  using System.Net;
  using System.Net.Sockets;
  using System.Text;
  using System.Threading;

  using GTCommons.Events;

  /// <summary>
  /// This class is work-in-progress (2011-09-22)
  /// Not all commands are implemented yet. 
  /// </summary>
  public class Client
  {
    ///////////////////////////////////////////////////////////////////////////////
    // Defining Constants                                                        //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTANTS
    #endregion //CONSTANTS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Variables, Enumerations, Events                                  //
    ///////////////////////////////////////////////////////////////////////////////
    #region FIELDS

    /// <summary>
    /// The calibration client.
    /// </summary>
    private readonly Calibration calibration;

    /// <summary>
    /// The camera client.
    /// </summary>
    private readonly Camera camera;

    /// <summary>
    /// The commands client.
    /// </summary>
    private readonly Commands commands;

    /// <summary>
    /// The gazeData that is send from the gazetracker.
    /// </summary>
    private readonly GazeData gazeData;

    /// <summary>
    /// The log client.
    /// </summary>
    private readonly Log log;

    /// <summary>
    /// The settings client.
    /// </summary>
    private readonly Settings settings;

    /// <summary>
    /// The udp socket for receiveing.
    /// </summary>
    private readonly Socket socketUdpReceive;

    /// <summary>
    /// The stream client.
    /// </summary>
    private readonly Stream stream;

    /// <summary>
    /// The streamformat client.
    /// </summary>
    private readonly StreamFormat streamformat;

    /// <summary>
    /// The tracker client.
    /// </summary>
    private readonly Tracker tracker;

    /// <summary>
    /// The UIControl client.
    /// </summary>
    private readonly UIControl uiControl;

    /// <summary>
    /// The thread to receive gaze samples via udp.
    /// </summary>
    private Thread threadReceiveUdp;

    /// <summary>
    /// The tcpClient.
    /// </summary>
    private TcpClient tcpClient;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the Client class.
    /// </summary>
    public Client()
    {
      // Will attempt to load "GazeTrackerSettings.xml" from execution dir. or set default
      this.settings = new Settings();

      this.socketUdpReceive = new Socket(
        AddressFamily.InterNetwork,
        SocketType.Dgram,
        ProtocolType.Udp);

      this.commands = new Commands();

      this.stream = new Stream(this);
      this.streamformat = new StreamFormat
        {
          TimeStampMilliseconds = true,
          GazePosition = true,
          EyetrackingType = StreamFormat.EyeTrackingType.Left
        };
      this.stream.StreamFormat = this.streamformat;

      this.tracker = new Tracker();
      this.camera = new Camera(this);
      this.calibration = new Calibration(this);
      this.uiControl = new UIControl(this);
      this.log = new Log(this);

      // Not fully implemented yet..
      this.MouseControl = new MouseControl();

      // On new gaze data
      this.gazeData = new GazeData();
      this.gazeData.OnSmoothedGazeData += this.MouseControl.Move;
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining events, enums, delegates                                         //
    ///////////////////////////////////////////////////////////////////////////////
    #region EVENTS

    /// <summary>
    /// Delegate. Handles connection changed event.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="success">True, if connection is established,
    /// otherwise false.</param>
    public delegate void ConnectHandler(object sender, bool success);

    /// <summary>
    /// Event. Raised whenever the client connection has changed.
    /// </summary>
    public event ConnectHandler ClientConnectionChanged;

    /// <summary>
    /// Event. Raised whenever the client has catched an error.
    /// </summary>
    public event StringEventHandler ErrorOccured;

    #endregion EVENTS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets or sets a value indicating whether the client is running.
    /// </summary>
    public bool IsRunning { get; set; }

    /// <summary>
    /// Gets or sets MouseControl.
    /// </summary>
    public MouseControl MouseControl { get; set; }

    /// <summary>
    /// Gets GazeData.
    /// </summary>
    public GazeData GazeData
    {
      get { return this.stream.GazeData; }
    }

    /// <summary>
    /// Gets or sets IPAddress.
    /// </summary>
    public IPAddress IPAddress
    {
      get { return this.settings.IPAddress; }
      set { this.settings.IPAddress = value; }
    }

    /// <summary>
    /// Gets or sets IPAddressString.
    /// </summary>
    public string IPAddressString
    {
      get { return this.settings.IPAddress.ToString(); }
      set { this.settings.IPAddress = IPAddress.Parse(value); }
    }

    /// <summary>
    /// Gets or sets PortReceive.
    /// </summary>
    public int PortReceive
    {
      get { return this.settings.UDPServerPort; }
      set { this.settings.UDPServerPort = value; }
    }

    /// <summary>
    /// Gets or sets PortSend.
    /// </summary>
    public int PortSend
    {
      get { return this.settings.TCPIPServerPort; }
      set { this.settings.TCPIPServerPort = value; }
    }

    /// <summary>
    /// Gets Calibration.
    /// </summary>
    public Calibration Calibration
    {
      get { return this.calibration; }
    }

    /// <summary>
    /// Gets Stream.
    /// </summary>
    public Stream Stream
    {
      get { return this.stream; }
    }

    /// <summary>
    /// Gets Commands.
    /// </summary>
    public Commands Commands
    {
      get { return this.commands; }
    }

    /// <summary>
    /// Gets Log.
    /// </summary>
    public Log Log
    {
      get { return this.log; }
    }

    /// <summary>
    /// Gets Camera.
    /// </summary>
    public Camera Camera
    {
      get { return this.camera; }
    }

    /// <summary>
    /// Gets UIControl.
    /// </summary>
    public UIControl UIControl
    {
      get { return this.uiControl; }
    }

    /// <summary>
    /// Gets Settings.
    /// </summary>
    public Settings Settings
    {
      get { return this.settings; }
    }

    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Public methods                                                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region PUBLICMETHODS

    /// <summary>
    /// Starts the client by connecting to a running gazetracker instance,
    /// which has the network options enabled.
    /// </summary>
    public void Connect()
    {
      if (this.IsRunning)
      {
        return;
      }

      try
      {
        this.tcpClient = new TcpClient();
        this.tcpClient.Connect(new IPEndPoint(this.settings.IPAddress, this.settings.TCPIPServerPort));
        this.tcpClient.NoDelay = true;
        this.tcpClient.SendTimeout = 500;

        this.threadReceiveUdp = new Thread(this.StartListen);
        this.threadReceiveUdp.Start();

        this.IsRunning = true;
      }
      catch (Exception ex)
      {
        this.Disconnect();
        var errorMessage = "Error while connecting to eye tracker. Message: " + ex.Message;
        throw new Exception(errorMessage, ex);
      }
    }

    /// <summary>
    /// Stops the client server and sends the connection changed event.
    /// </summary>
    public void Disconnect()
    {
      try
      {
        this.IsRunning = false;

        if (this.tcpClient != null) // && this.tcpClient.Connected)
        {
          this.tcpClient.Close();
        }

        if (this.socketUdpReceive != null && this.socketUdpReceive.Connected)
        {
          this.socketUdpReceive.Disconnect(true);
          this.socketUdpReceive.Shutdown(SocketShutdown.Both);
          this.socketUdpReceive.Close(2);
        }

        this.OnClientConnectionChanged(false);
      }
      catch (Exception ex)
      {
        this.IsRunning = false;
        var errorMessage = "Could not stop the UDP thread. Message: " + ex.Message;
        this.OnErrorOccured(new StringEventArgs(errorMessage));
      }
    }

    /// <summary>
    /// Sending simple string commands to the tracker
    /// </summary>
    /// <param name="cmd">A command <see cref="String"/></param>
    /// <returns>True if succesful, otherwise false</returns>
    public bool SendCommand(string cmd)
    {
      return SendCommand(cmd, null);
    }

    /// <summary>
    /// Sending string commands with parameters to the tracker
    /// </summary>
    /// <param name="cmd">A command <see cref="String"/></param>
    /// <param name="value">A <see cref="String"/> containing the parameter value</param>
    /// <returns>True if succesful, otherwise false</returns>
    public bool SendCommand(string cmd, string value)
    {
      if (cmd == null || this.tcpClient == null)
      {
        return false;
      }

      try
      {
        using (var clientStream = this.tcpClient.GetStream())
        {
          var encoder = new ASCIIEncoding();

          byte[] buffer = value != null ? encoder.GetBytes(cmd + " " + value) : encoder.GetBytes(cmd);

          clientStream.Write(buffer, 0, buffer.Length);
          clientStream.Flush();
        }

        return true;
      }
      catch (Exception ex)
      {
        var errorMessage = "Error during sending a command. Message: " + ex.Message;
        this.OnErrorOccured(new StringEventArgs(errorMessage));
        this.OnClientConnectionChanged(false);

        return false;
      }
    }

    #endregion //PUBLICMETHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Inherited methods                                                         //
    ///////////////////////////////////////////////////////////////////////////////
    #region OVERRIDES
    #endregion //OVERRIDES

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler                                                              //
    ///////////////////////////////////////////////////////////////////////////////
    #region EVENTHANDLER

    /// <summary>
    /// Raises the <see cref="ErrorOccured"/> event.
    /// </summary>
    /// <param name="e">A <see cref="StringEventArgs"/> with the event data.</param>.
    private void OnErrorOccured(StringEventArgs e)
    {
      if (this.ErrorOccured != null)
      {
        this.ErrorOccured(this, e);
      }
    }

    /// <summary>
    /// Raises the <see cref="ClientConnectionChanged"/> event.
    /// </summary>
    /// <param name="success">A <see cref="Boolean"/> which should
    /// be true, when the connections is successfully established.</param>.
    private void OnClientConnectionChanged(bool success)
    {
      if (this.ClientConnectionChanged != null)
      {
        this.ClientConnectionChanged(this, success);
      }
    }

    #endregion //EVENTHANDLER

    ///////////////////////////////////////////////////////////////////////////////
    // Methods and Eventhandling for Background tasks                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region THREAD
    #endregion //THREAD

    ///////////////////////////////////////////////////////////////////////////////
    // Methods for doing main class job                                          //
    ///////////////////////////////////////////////////////////////////////////////
    #region PRIVATEMETHODS

    /// <summary>
    /// This is the main loop for receiving data from the tracker.. 
    /// </summary>
    private void StartListen()
    {
      EndPoint remoteEp = new IPEndPoint(this.settings.IPAddress, this.settings.UDPServerPort);

      try
      {
        try
        {
          Dns.GetHostEntry(this.settings.IPAddress);
        }
        catch (Exception e)
        {
          var errorMessage = "Error connecting socket. Message: " + e.Message;
          this.OnErrorOccured(new StringEventArgs(errorMessage));
          this.OnClientConnectionChanged(false);
          return;
        }

        // Hook it up.. 
        if (!this.socketUdpReceive.IsBound)
        {
          this.socketUdpReceive.Bind(remoteEp);
        }

        this.OnClientConnectionChanged(true);

        // True as long as the program is running..  read tracker data (yes, sort of ugly)
        while (this.IsRunning)
        {
          var received = new byte[256];

          try
          {
            this.socketUdpReceive.ReceiveFrom(received, ref remoteEp);
          }
          catch (Exception ex)
          {
            this.OnClientConnectionChanged(false);
            var errorMessage = "Could not receive data. Message: " + ex.Message;
            this.OnErrorOccured(new StringEventArgs(errorMessage));
          }

          // This is needed, because we would otherwise not break the while
          // loop, we never get at the start of the loop again after
          // stopping the udp and we will hang in ExtractDataAndRaiseEvent.
          // Using this the thread is normally finished and no need to abort it
          // eliminating its threadabortexception
          if (!this.IsRunning)
          {
            break;
          }

          // Reformat to string and remove the empty bits \0\0\0 etc.
          var datareceived = Encoding.ASCII.GetString(received).Trim('\0');

          if (datareceived.Length <= 0)
          {
            continue;
          }

          char[] seperator = { '_' };
          var data = datareceived.Split(seperator, 20);
          switch (data[0])
          {
            case "TRACKER":
              this.tracker.ExtractDataAndRaiseEvent(datareceived);
              break;
            case "STREAM":
              this.stream.ExtractDataAndRaiseEvent(datareceived);
              break;
            case "CAL":
              this.calibration.ExtractDataAndRaiseEvent(datareceived);
              break;
            case "LOG":
              this.log.ExtractDataAndRaiseEvent(datareceived);
              break;
            case "UI":
              this.uiControl.ExtractDataAndRaiseEvent(datareceived);
              break;
          }
        }
      }
      catch (Exception ex)
      {
        var errorMessage = "CA socket error has occured. Message: " + ex.Message;
        this.OnErrorOccured(new StringEventArgs(errorMessage));
        this.OnClientConnectionChanged(false);
      }
    }

    #endregion //PRIVATEMETHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER
    #endregion //HELPER
  }
}