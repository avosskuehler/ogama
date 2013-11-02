// <copyright file="SMIClient.cs" company="FU Berlin">
// ******************************************************
// OGAMA - open gaze and mouse analyzer 
// Copyright (C) 2013 Dr. Adrian Voßkühler  
// ------------------------------------------------------------------------
// This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
// This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
// You should have received a copy of the GNU General Public License along with this program; if not, write to the Free Software Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
// **************************************************************
// </copyright>
// <author>Adrian Voßkühler</author>
// <email>adrian@ogama.net</email>

namespace Ogama.Modules.Recording.SMIInterface
{
  using System;
  using System.Collections.Generic;
  using System.Drawing;
  using System.Net;
  using System.Net.Sockets;
  using System.Text;
  using System.Threading;
  using System.Windows.Forms;

  using Ogama.ExceptionHandling;
  using Ogama.Modules.Common;
  using Ogama.Modules.Common.CustomEventArgs;
  using Ogama.Modules.Common.Tools;

  /// <summary>
  /// This class is the abstraction layer for the communication
  /// via UDP with the SMI iViewX eyetracker.
  /// </summary>
  public class SMIClient
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
    /// The client which hosts 
    /// the udp connection to the iViewX host computer.
    /// </summary>
    private UdpClient udpClient;

    /// <summary>
    /// The thread which listens to packets received from
    /// the udp connection to the iViewX host computer.
    /// </summary>
    private Thread listenThread;

    /// <summary>
    /// Saves the sample time of the last received gaze sample-
    /// </summary>
    private long lastTime;

    /// <summary>
    /// Saves the tracking status of the smi client object.
    /// </summary>
    private bool isTracking;

    /// <summary>
    /// Saves the SMI settings.
    /// </summary>
    private SMISetting smiSettings;

    /// <summary>
    /// Saves the size of the presentation screen
    /// </summary>
    private Size presentationScreenSize;

    /// <summary>
    /// Flag indicating to stop the <see cref="listenThread"/>.
    /// </summary>
    private bool stopListenThread;

    /// <summary>
    /// Saves the form to display the calibration dots.
    /// </summary>
    private SMICalibrationForm newCalibrationForm;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the SMIClient class.
    /// </summary>
    public SMIClient()
    {
      this.lastTime = 0;
      this.stopListenThread = false;
      Document document = Document.ActiveDocument;
      if (document != null)
      {
        this.presentationScreenSize = Document.ActiveDocument.PresentationSize;
      }
      
    }
		/// <summary>
		/// SMIClient constructor
		/// </summary>
		/// <param name="drawingWidth"></param>
		/// <param name="drawingHeight"></param>
    public SMIClient(int drawingWidth, int drawingHeight)
    {
      this.lastTime = 0;
      this.stopListenThread = false;
      this.presentationScreenSize = new Size(drawingWidth, drawingHeight);
    }

    /// <summary>
    /// Finalizes an instance of the SMIClient class
    /// </summary>
    ~SMIClient()
    {
      this.udpClient.Close();
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Enumerations                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region ENUMS

    /// <summary>
    /// Event. Raised, when new gaze data is available.
    /// </summary>
    public event GazeDataChangedEventHandler GazeDataAvailable;

    /// <summary>
    /// Event. Raised, when calibration has finished.
    /// </summary>
    public event EventHandler CalibrationFinished;

    #endregion ENUMS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets a value indicating whether the connection of the smi client object is available
    /// </summary>
    public bool IsConnected
    {
      get { return this.TestForConnection(); }
    }

    /// <summary>
    /// Gets a value indicating whether the smi client object is tracking.
    /// </summary>
    public bool IsTracking
    {
      get { return this.isTracking; }
    }

    /// <summary>
    /// Gets or sets the <see cref="SMISetting"/> to be used within this client.
    /// </summary>
    public SMISetting Settings
    {
      get
      {
        return this.smiSettings;
      }

      set
      {
        this.smiSettings = value;
        this.CreateUDPPort();
      }
    }

    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Public methods                                                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region PUBLICMETHODS

    /// <summary>
    /// Configures the SMI iViewX System.
    /// </summary>
    public void Configure()
    {
      // wait for valid data: turn on
      this.SendString("ET_CPA 0 1");

      // randomize point order:  turn off
      this.SendString("ET_CPA 1 0");

      // auto accept: turn on
      this.SendString("ET_CPA 2 1");

      // Data Stream Format
      this.SendString("ET_FRM \"%ET %TS %DX %DY %SX %SY\"");

      // Stop stream if it is running
      this.StopStreaming();
    }

    /// <summary>
    /// Connects the udp client to the iViewX Server.
    /// </summary>
    public void Connect()
    {
      try
      {
        this.udpClient.Connect(this.smiSettings.SMIServerAddress, this.smiSettings.SMIServerPort);
        this.Configure();
        
      }
      catch (Exception ex)
      {
        ExceptionMethods.HandleExceptionSilent(ex);
      }
    }

    /// <summary>
    /// Disconnects the udp client connection.
    /// </summary>
    public void Disconnect()
    {
      try
      {
        this.udpClient.Close();        
      }
      catch (Exception ex)
      {
        ExceptionMethods.HandleExceptionSilent(ex);
      }
    }

    /// <summary>
    /// Starts the data streaming by sending the ET_STR command to
    /// iViewX
    /// </summary>
    public void StartStreaming()
    {
      this.SendString("ET_STR");
    }

    /// <summary>
    /// Starts the data streaming by sending the ET_EST command to
    /// iViewX
    /// </summary>
    public void StopStreaming()
    {
      this.SendString("ET_EST");
    }

    /// <summary>
    /// Starts the listening thread to receive messages from the iViewX
    /// udp connection.
    /// </summary>
    public void StartTracking()
    {
      try
      {
        this.stopListenThread = false;
        this.listenThread = new Thread(new ThreadStart(this.StartListen));
        this.listenThread.Start();
        this.isTracking = true;
      }
      catch (Exception ex)
      {
        this.listenThread.Abort();
        ExceptionMethods.HandleExceptionSilent(ex);
      }
    }

    /// <summary>
    /// Stops the tracking by sending the ET_EST command and
    /// cancelling the listen thread
    /// </summary>
    public void StopTracking()
    {

      this.SendString("ET_EST");

      if (this.listenThread.IsAlive)
      {
        this.listenThread.Abort();
      }

      this.stopListenThread = true;
      this.isTracking = false;
      
    }

    /// <summary>
    /// Returns the sample time of the last received sample.
    /// </summary>
    /// <returns>A <see cref="Int64"/> with the sample time of the last received sample.</returns>
    public long GetTimeStamp()
    {
      return this.lastTime;
    }

    /// <summary>
    /// This method performs a unrandomized nine point calibration.
    /// </summary>
    public void Calibrate()
    {
      if (!this.IsConnected)
      {
        this.Connect();
      }
      
      // Get presentation size
      Size presentationSize = PresentationScreen.GetPresentationResolution();

      // Set calibration area size
      this.SendString("ET_CSZ " + presentationSize.Width.ToString() + " " + presentationSize.Height.ToString());


      // Set calibration points
      int fivePercentX = (int)(0.05f * presentationSize.Width);
      int fivePercentY = (int)(0.05f * presentationSize.Height);
      int xHalf = presentationSize.Width / 2;
      int yHalf = presentationSize.Height / 2;//was Width
      Point topLeft = new Point(fivePercentX, fivePercentY);
      Point topMiddle = new Point(xHalf, fivePercentY);
      Point topRight = new Point(presentationSize.Width - fivePercentX, fivePercentY);
      Point middleLeft = new Point(fivePercentX, yHalf);
      Point center = new Point(xHalf, yHalf);
      Point middleRight = new Point(presentationSize.Width - fivePercentX, yHalf);
      Point bottomLeft = new Point(fivePercentX, presentationSize.Height - fivePercentY);
      Point bottomMiddle = new Point(xHalf, presentationSize.Height - fivePercentY);
      Point bottomRight = new Point(presentationSize.Width - fivePercentX, presentationSize.Height - fivePercentY);
      List<Point> calibrationPoints = new List<Point>();
      calibrationPoints.Add(center);
      calibrationPoints.Add(topLeft);
      calibrationPoints.Add(topRight);
      calibrationPoints.Add(bottomLeft);
      calibrationPoints.Add(bottomRight);
      calibrationPoints.Add(middleLeft);
      calibrationPoints.Add(topMiddle);
      calibrationPoints.Add(middleRight);
      calibrationPoints.Add(bottomMiddle);

      ////this.SendString("ET_PNT 1 " + center.X.ToString() + " " + center.Y.ToString());
      ////this.SendString("ET_PNT 2 " + topLeft.X.ToString() + " " + topLeft.Y.ToString());
      ////this.SendString("ET_PNT 3 " + topRight.X.ToString() + " " + topRight.Y.ToString());
      ////this.SendString("ET_PNT 4 " + bottomLeft.X.ToString() + " " + bottomLeft.Y.ToString());
      ////this.SendString("ET_PNT 5 " + bottomRight.X.ToString() + " " + bottomRight.Y.ToString());

      this.newCalibrationForm = new SMICalibrationForm();
      this.newCalibrationForm.CalibPointColor = this.smiSettings.CalibPointColor;
      this.newCalibrationForm.CalibPointSize = this.smiSettings.CalibPointSize;
      this.newCalibrationForm.BackColor = this.smiSettings.CalibBackgroundColor;
      this.newCalibrationForm.Width = presentationSize.Width;
      this.newCalibrationForm.Height = presentationSize.Height;
      this.newCalibrationForm.CalibrationPoints = calibrationPoints;

      
      PresentationScreen.PutFormOnPresentationScreen(this.newCalibrationForm, true);
      
      this.newCalibrationForm.Show();

      // Starts calibration.
      this.SendString("ET_CAL 9");

      // Wait 2 seconds at the center point.
      int counter = 0;
      do
      {
        Application.DoEvents();
        Thread.Sleep(100);
        counter++;
      }
      while (counter < 50);

      // Accepts calibration point.
      this.SendString("ET_ACC");
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
    #region EVENTS

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler for UI, Menu, Buttons, Toolbars etc.                         //
    ///////////////////////////////////////////////////////////////////////////////
    #region WINDOWSEVENTHANDLER
    #endregion //WINDOWSEVENTHANDLER

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler for Custom Defined Events                                    //
    ///////////////////////////////////////////////////////////////////////////////
    #region CUSTOMEVENTHANDLER

    /// <summary>
    /// Raised when new gaze data is available.
    /// </summary>
    /// <param name="e"><see cref="GazeDataChangedEventArgs"/> event arguments</param>.
    private void OnGazeDataAvailable(GazeDataChangedEventArgs e)
    {
      if (this.GazeDataAvailable != null)
      {
        this.GazeDataAvailable(this, e);
      }
    }

    /// <summary>
    /// Raised when CalibrationFinished
    /// </summary>
    /// <param name="e">Empty <see cref="EventArgs"/> event arguments</param>.
    private void OnCalibrationFinished(EventArgs e)
    {
      if (this.CalibrationFinished != null)
      {
        this.CalibrationFinished(this, e);
      }
    }

    #endregion //CUSTOMEVENTHANDLER

    #endregion //EVENTS

    ///////////////////////////////////////////////////////////////////////////////
    // Methods and Eventhandling for Background tasks                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region BACKGROUNDWORKER
    #endregion //BACKGROUNDWORKER

    ///////////////////////////////////////////////////////////////////////////////
    // Methods for doing main class job                                          //
    ///////////////////////////////////////////////////////////////////////////////
    #region PRIVATEMETHODS

    /// <summary>
    /// This method creates a new <see cref="UdpClient"/> for the current server.
    /// </summary>
    private void CreateUDPPort()
    {
      if (this.smiSettings == null)
      {
        throw new ArgumentNullException("SMISettings are not set");
      }

      // This constructor assigns the local port number.
      this.udpClient = new UdpClient(this.smiSettings.OGAMAServerPort);
    }

    /// <summary>
    /// This is the callback method to receive the samples from the udp stream.
    /// </summary>
    private void StartListen()
    {
      // IPEndPoint object will allow us to read datagrams sent from any source.
      IPEndPoint remoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);

      while (!this.stopListenThread)
      {
        if (this.udpClient.Client == null)
        {
          break;
        }
        try
        {
          // Blocks until a message returns on this socket from a remote host.
          byte[] receiveBytes = this.udpClient.Receive(ref remoteIpEndPoint);
          string returnData = Encoding.ASCII.GetString(receiveBytes);
          if (returnData.Length > 0)
          {
            this.NewBytesReceived(returnData);
          }
        }
        catch (Exception ex)
        {
          ExceptionMethods.HandleExceptionSilent(ex);
        }

      }
    }

    /// <summary>
    /// This method is called every time a new command string has been received
    /// from the iViewX
    /// </summary>
    /// <param name="receivedString">A <see cref="String"/> that has been received
    /// and should be parsed.</param>
    private void NewBytesReceived(string receivedString)
    {
      if (receivedString.Length < 6)
      {
        return;
      }

      string command = receivedString.Substring(0, 6).Trim();
      char[] seperator = { ' ' };
      switch (command)
      {
        case "ET_CHG":
          // Indicates calibration point change. Available only during calibration.
          string[] tmp = receivedString.Split(seperator);
          int pointNumber = Convert.ToInt32(tmp[1]);
          //Console.WriteLine("NewBytesReceived, ET_CHG, pointnumber:"+pointNumber);
          this.ShowNewCalibrationPoint(pointNumber);
          break;
        case "ET_FIN":
          // The command is sent by iView X when a calibration has finished successfully.
          this.newCalibrationForm.ThreadSafeClose();
          this.OnCalibrationFinished(new EventArgs());
          break;
        case "ET_SPL":
          // The command is sent by iView everytime a data sample is generated and data streaming is on.
          this.OnGazeDataAvailable(new GazeDataChangedEventArgs(this.ExtractTrackerData(receivedString)));
          break;
        case "ET_IMG":
          // Sent by iView X with single mime-encoded eye video images, after the ET_SIM command has been received.
          this.SendNewEyeImage(receivedString);
          break;
      }
    }

    /// <summary>
    /// This method is currently not implemented.
    /// </summary>
    /// <param name="receivedString">The <see cref="String"/> with the command line
    /// from the iViewX containg the image data.</param>
    private void SendNewEyeImage(string receivedString)
    {
      ////string command = receivedString.Substring(0, 6).Trim();
      ////char[] seperator = { ' ' };
      ////string[] tmp = receivedString.Split(seperator);
      ////int videoWidth = Convert.ToInt32(tmp[1]);
      ////int videoHeight = Convert.ToInt32(tmp[2]);
      ////int count = 9 + tmp[1].Length + tmp[2].Length;
      ////string bitmapData = receivedString.Substring(count, receivedString.Length);
      ////// Create an instance of StringReader and attach it to the string.
      ////MemoryStream memStream = new MemoryStream(StrToByteArray(bitmapData));
      ////Bitmap videoFrame = new Bitmap(memStream);
      ////videoFrame.Save(@"c:\Dumps\test.jpg");
    }

    /// <summary>
    /// This method is called whenever a ET_CHG message is received.
    /// It updates the calibration form to show the new calibration
    /// point at the new location.
    /// </summary>
    /// <param name="pointNumber">An <see cref="Int32"/> with the new 
    /// calibration point number (1-based)</param>
    private void ShowNewCalibrationPoint(int pointNumber)
    {
      if (this.newCalibrationForm != null)
      {
        this.newCalibrationForm.ShowCalibrationPoint(pointNumber);
      }
    }

    /// <summary>
    /// Extracts the received sample into a <see cref="GazeData"/>
    /// struct.
		/// A dataStr may contain the following data:
		/// ET_SPL 91555722812 b 420 420 564 564 17.219431 17.855097 17.219431 17.855097 -53.704  9.674 13.589 15.935 624.140 612.472 419.313368 680.213202 455.167761 443.716013 4.72 4.72 3
    /// </summary>
    /// <param name="dataStr">The <see cref="String"/> with the data line from the iViewX.</param>
    /// <returns>A filled <see cref="GazeData"/> that represents the line
    /// according to the <see cref="SMISetting"/></returns>
    public GazeData ExtractTrackerData(string dataStr)
    {
      
			//Console.WriteLine("ExtractTrackerData:" + dataStr);

			GazeData data = new GazeData();
      char[] seperator = { ' ' };
      string[] tmp = dataStr.Split(seperator);
			
      try
      {	
				this.lastTime = Convert.ToInt64(tmp[1]);
      }
      catch (System.Exception)
      {

      }
			string availableEye = tmp[2];
      switch (availableEye)
      {
        case "b":
          data.PupilDiaX = Convert.ToSingle(tmp[3]);
          data.PupilDiaY = Convert.ToSingle(tmp[5]);
          float gazePosXLeft = Convert.ToSingle(tmp[7]) / this.presentationScreenSize.Height;
          float gazePosXRight = Convert.ToSingle(tmp[8]) / this.presentationScreenSize.Height;
          float gazePosYLeft = Convert.ToSingle(tmp[9]) / this.presentationScreenSize.Height;
          float gazePosYRight = Convert.ToSingle(tmp[10]) / this.presentationScreenSize.Height;

          switch (this.smiSettings.AvailableEye)
          {
            case AvailableEye.Left:
              data.GazePosX = gazePosXLeft;
              data.GazePosY = gazePosYLeft;
              break;
            case AvailableEye.Right:
              data.GazePosX = gazePosXRight;
              data.GazePosY = gazePosYRight;
              break;
            case AvailableEye.Both:
              // Use the mean
              data.GazePosX = (gazePosXLeft + gazePosXRight) / 2;
              data.GazePosY = (gazePosYLeft + gazePosYRight) / 2;
              break;
          }

          break;
        case "l":
        case "r":
          data.PupilDiaX = Convert.ToSingle(tmp[3]);
          data.PupilDiaY = Convert.ToSingle(tmp[4]);
          data.GazePosX = Convert.ToSingle(tmp[5]) / this.presentationScreenSize.Width;
          data.GazePosY = Convert.ToSingle(tmp[6]) / this.presentationScreenSize.Height;
          break;
      }
     
      
      data.Time = this.lastTime;
      return data;
    }

    /// <summary>
    /// This method tests for a valid udp connection to the iViewX.
    /// </summary>
    /// <returns><strong>True</strong> if connection is succesfully established,
    /// otherwise <strong>false</strong></returns>
    private bool TestForConnection()
    {
      if (this.udpClient.Client == null)
      {
        return false;
      }

      if (!this.udpClient.Client.Connected)
      {
        return false;
      }

      // This is how you can determine whether a socket is still connected.
      bool blockingState = this.udpClient.Client.Blocking;
      try
      {
        byte[] tmp = new byte[1];
        this.udpClient.Client.Blocking = false;
        this.udpClient.Client.Send(tmp, 0, 0);
      }
      catch (SocketException)
      {
        return false;
      }
      finally
      {
        this.udpClient.Client.Blocking = blockingState;
      }

      return true;
    }

    #endregion //PRIVATEMETHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER

    /// <summary>
    /// Converts a string to a byte array.
    /// </summary>
    /// <param name="str">The <see cref="String"/> to convert</param>
    /// <returns>A <see cref="Byte"/> array representing the given string</returns>
    private static byte[] StrToByteArray(string str)
    {
      System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
      return encoding.GetBytes(str);
    }

    /// <summary>
    /// Sends a message to the host to which you have connected.
    /// It will be added a line feed at the end of your message.
    /// </summary>
    /// <param name="stringToSend">The <see cref="String"/> with the message to send.</param>
    private void SendString(string stringToSend)
    {
      byte[] sendBytes = Encoding.ASCII.GetBytes(stringToSend + " \n");
      this.udpClient.Send(sendBytes, sendBytes.Length);
    }

    #endregion //HELPER
  }
}
