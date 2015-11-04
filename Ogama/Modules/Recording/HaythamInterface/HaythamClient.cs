// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HaythamClient.cs" company="Freie Universität Berlin">
//   OGAMA - open gaze and mouse analyzer 
//   Copyright (C) 2015 Dr. Adrian Voßkühler  
//   Licensed under GPL V3
// </copyright>
// <author>Adrian Voßkühler</author>
// <email>adrian@ogama.net</email>
// <summary>
//   The haytham client.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Ogama.Modules.Recording.HaythamInterface
{
  using System;
  using System.Drawing;
  using System.IO;
  using System.Linq;
  using System.Net;
  using System.Net.Sockets;
  using System.Threading;
  using System.Windows.Media.Media3D;

  using Haytham.ExtData.Service;

  using Ogama.Modules.Common.CustomEventArgs;
  using Ogama.Modules.Common.Tools;
  using Ogama.Modules.Recording.TrackerBase;

  /// <summary>
  ///   The haytham client.
  /// </summary>
  public class HaythamClient
  {
    /// <summary>
    ///   Saves the size of the presentation screen to calibrate
    ///   the incoming gaze samples to 0..1 values.
    /// </summary>
    private Size presentationScreenSize;

    /// <summary>
    /// Thread for receiving data from server
    /// </summary>
    private Thread inputoutputThread;

    /// <summary>
    /// Indicates that the message poll thread is running
    /// </summary>
    private bool isRunning;

    #region Public Events

    /// <summary>
    ///   Occurs when calibration finished.
    /// </summary>
    public event EventHandler CalibrationFinished;

    /// <summary>
    ///   Occurs when gaze data is received.
    /// </summary>
    public event GazeDataChangedEventHandler GazeDataReceived;

    /// <summary>
    ///   Event. Raised, when new track status data is available.
    /// </summary>
    public event TrackStatusDataChangedEventHandler TrackStatusDataChanged;

    #endregion

    #region Public Properties

    /// <summary>
    ///   Gets or sets the server ip address.
    /// </summary>
    /// <value>
    ///   The server ip address.
    /// </value>
    public IPAddress ServerIPAddress { get; set; }

    /// <summary>
    /// Gets or sets the client to establish connection
    /// </summary>
    public TcpClient TcpClient { get; set; }

    /// <summary>
    /// Gets or sets the writer facilitates writing to the stream
    /// </summary>
    public BinaryWriter Writer { get; set; }

    /// <summary>
    /// Gets or sets the reader facilitates reading from the stream 
    /// </summary>
    public BinaryReader Reader { get; set; }

    /// <summary>
    /// Gets or sets the network data stream
    /// </summary>
    public NetworkStream Stream { get; set; }

    /// <summary>
    /// Gets or sets the name of the client
    /// </summary>
    public string ClientName { get; set; }

    #endregion

    #region Methods

    /// <summary>
    /// Connects this instance to the haytham server.
    /// </summary>
    /// <returns>
    /// Returns true if successful.
    /// </returns>
    public bool Connect()
    {
      this.TcpClient = new TcpClient();
      try
      {
        this.TcpClient.Connect(this.ServerIPAddress, 50000);
        this.Stream = this.TcpClient.GetStream();
        this.Writer = new BinaryWriter(this.Stream);
        this.Reader = new BinaryReader(this.Stream);

        // send name and type
        this.Writer.Write("Monitor"); // type
        this.Writer.Write("Ogama"); // name

        this.ClientName = this.Reader.ReadString(); // get approved name

        this.presentationScreenSize = Document.ActiveDocument.PresentationSize;

        if (this.TcpClient.Connected)
        {
          // Send "Status_Gaze" -> false to not receive default gaze data
          this.Writer.Write("Status_Gaze");
          this.Writer.Write("False");

          // Send "Status_Commands" -> true to receive commands, especially calibration finished message
          this.Writer.Write("Status_Commands");
          this.Writer.Write("True");

          // Send "Status_Eye" -> true to receive complete gaze data
          this.Writer.Write("Status_Eye");
          this.Writer.Write("True");

          // Send "Size" with values to set tracking size
          this.Writer.Write("Size");
          this.Writer.Write(this.presentationScreenSize.Width);
          this.Writer.Write(this.presentationScreenSize.Height);

          this.Writer.Write("PresentationScreen");
          this.Writer.Write(PresentationScreen.GetPresentationScreen().Primary);

          // start a new thread for receiving messages
          this.inputoutputThread = new Thread(this.ListenThread);
          this.isRunning = true;
          this.inputoutputThread.Start();
        }
      }
      catch (Exception)
      {
        return false;
      }

      return true;
    }

    ///// <summary>
    /////   Calibrates the haytham client.
    ///// </summary>
    //public void Calibrate()
    //{
    //  this.Writer.Write("PresentationScreen");
    //  this.Writer.Write(PresentationScreen.GetPresentationScreen().Primary);

    //  // Send "Calibrate" -> true to start calibration
    //  this.Writer.Write("Calibrate");
    //  this.Writer.Write(CalibrationMethod.Point4.ToString());
    //}

    /// <summary>
    ///   Disconnects this instance.
    /// </summary>
    public void Disconnect()
    {
      this.isRunning = false;
      this.TcpClient.Close();
    }

    /// <summary>
    /// Listens for messages from the tcp stream
    /// </summary>
    public void ListenThread()
    {
      try
      {
        // receive messages that is sent to client
        while (true)
        {
          // Stop thread if requested
          if (!this.isRunning)
          {
            break;
          }

          string msg = this.Reader.ReadString();
          this.ProcessMessage(msg);
        }
      }
      catch (IOException)
      {
        // DisplayMessage("Connection failed\r\n", textBox2);
        // DisplayMessage("Waiting for connection...\r\n", textBox2);
        // Reconnect("Monitor");
        // DisplayMessage("Connection successful\r\n", textBox2);
        // ListenThread();
      }
    }

    /// <summary>
    /// Processes the message.
    /// </summary>
    /// <param name="msg">The message from the haytham server.</param>
    private void ProcessMessage(string msg)
    {
      if (msg == "Commands|CalibrationFinished|")
      {
        this.OnCalibrationFinished(EventArgs.Empty);
        return;
      }

      string[] msgArray = this.ConvertMsgToArray(msg);

      if (msg.StartsWith("Eye|"))
      {
        var newData = new GazeData();

        // Get time
        var timeInTicks = long.Parse(msgArray[0]);

        // Convert to milliseconds
        newData.Time = (long)(timeInTicks / 10000.0);

        // Get Position in screen coordinates
        var absoluteX = float.Parse(msgArray[1]);
        var absoluteY = float.Parse(msgArray[2]);

        // Convert to relative coordinates, cause this is expected by Ogama
        newData.GazePosX = absoluteX / this.presentationScreenSize.Width;
        newData.GazePosY = absoluteY / this.presentationScreenSize.Height;

        newData.PupilDiaX = int.Parse(msgArray[3]);

        this.OnGazeDataReceived(new GazeDataChangedEventArgs(newData));

        var newTrackStatusData = new TrackStatusData();
        newTrackStatusData.TrackedEyes = Eye.Left;
        var isPupilFound = bool.Parse(msgArray[4]);
        newTrackStatusData.LeftEyeValidity = isPupilFound ? Validity.Good : Validity.Missing;
        var pupilPosX = float.Parse(msgArray[5]);
        var pupilPosY = float.Parse(msgArray[6]);
        newTrackStatusData.LeftEyePosition = new Vector3D(pupilPosX, pupilPosY, 0);
        this.OnTrackStatusDataChanged(new TrackStatusDataChangedEventArgs(newTrackStatusData));
      }
    }

    /// <summary>
    /// This method converts the haytham special formatted message
    /// into a string array of values.
    /// </summary>
    /// <param name="msg">The haytham special formatted message </param>
    /// <returns> a string array of values.</returns>
    private string[] ConvertMsgToArray(string msg)
    {
      var arr = msg.Split('|');

      // skip first keyword
      var msgArr = arr.Skip(1).ToArray();

      return msgArr;
    }

    /// <summary>
    /// The OnCalibrationFinished method raises the event by invoking
    ///   the delegates. The sender is always this, the current instance
    ///   of the class.
    /// </summary>
    /// <param name="e">
    /// An empty <see cref="EventArgs"/>
    /// </param>
    private void OnCalibrationFinished(EventArgs e)
    {
      // Invokes the delegates. 
      if (this.CalibrationFinished != null)
      {
        this.CalibrationFinished(this, e);
      }
    }

    /// <summary>
    /// The OnGazeDataReceived method raises the event by invoking
    ///   the delegates. The sender is always this, the current instance
    ///   of the class.
    /// </summary>
    /// <param name="e">
    /// An empty <see cref="EventArgs"/>
    /// </param>
    private void OnGazeDataReceived(GazeDataChangedEventArgs e)
    {
      // Invokes the delegates. 
      if (this.GazeDataReceived != null)
      {
        this.GazeDataReceived(this, e);
      }
    }

    /// <summary>
    /// Calls listeners methods when new TrackStatus is available.
    /// </summary>
    /// <param name="e"> <see cref="TrackStatusDataChangedEventArgs"/> event arguments </param>
    private void OnTrackStatusDataChanged(TrackStatusDataChangedEventArgs e)
    {
      if (this.TrackStatusDataChanged != null)
      {
        this.TrackStatusDataChanged(this, e);
      }
    }

    #endregion
  }
}