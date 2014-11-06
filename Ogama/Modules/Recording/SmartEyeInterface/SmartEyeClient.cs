// <copyright file="SmartEyeClient.cs" company="Smart Eye AB">
// ******************************************************
// OGAMA - open gaze and mouse analyzer 
// Copyright (C) 2014 Dr. Adrian Voßkühler  
// ------------------------------------------------------------------------
// This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
// This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
// You should have received a copy of the GNU General Public License along with this program; if not, write to the Free Software Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
// **************************************************************
// </copyright>
// <author>Kathrin Scheil</author>
// <email>kathrin.scheil@smarteye.se</email>

namespace Ogama.Modules.Recording.SmartEyeInterface
{
  using System;
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.Diagnostics;
  using System.Drawing;
  using System.Linq;
  using System.Net;
  using System.Threading;
  using System.Windows.Forms;
  using Microsoft.Win32;
  using Ogama.ExceptionHandling;
  using Ogama.Modules.Common.CustomEventArgs;
  using Ogama.Modules.Recording.Dialogs;
  using SmartEye.Network;
  using SmartEye.Rpc.Json;
  using SmartEye.Tracker;
  using SmartEye.WorldModel;

  /// <summary>
  /// The Smart Eye client class.
  /// </summary>
  public class SmartEyeClient : INotifyPropertyChanged
  {
    ///////////////////////////////////////////////////////////////////////////////
    // Defining Variables, Enumerations, Events                                  //
    ///////////////////////////////////////////////////////////////////////////////
    #region FIELDS

    /// <summary>
    /// The Smart Eye settings.
    /// </summary>
    private SmartEyeSetting smartEyeSettings;

    /// <summary>
    /// Is UDP socket receiving data?
    /// </summary>
    private bool udpIsConnected;

    /// <summary>
    /// List of data IDs the Smart Eye tracker should send.
    /// </summary>
    private List<TrackerDataId> dataIds;

    #endregion FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the <see cref="SmartEyeClient"/> class.
    /// </summary>
    /// <param name="setting">The settings</param>
    public SmartEyeClient(SmartEyeSetting setting)
    {
      this.smartEyeSettings = setting;

      this.IsClosingDown = false;

      this.RpcIsConnected = false;

      this.dataIds = new List<TrackerDataId>();

      this.KillRunningEyeTrackerProcess();

      Process smartEyeTrackingProcess;

      var smartEyeVersion = Registry.GetValue(@"HKEY_CURRENT_USER\Software\Smart Eye AB\Shared", "EyeTrackerCoreVersion", null);
      if (smartEyeVersion != null)
      {
        var smartEyeTrackingPath = Registry.GetValue(
          string.Format(@"HKEY_CURRENT_USER\Software\Smart Eye AB\Eye tracker core {0}\DefaultPaths", smartEyeVersion.ToString()),
          "ProgramDirectory",
          null);
        if (smartEyeTrackingPath != null)
        {
          smartEyeTrackingProcess = new Process
          {
            StartInfo =
            {
              WorkingDirectory = smartEyeTrackingPath.ToString(),
              FileName = smartEyeTrackingPath + "\\eye_tracker_core.exe",
              UseShellExecute = false,
              RedirectStandardError = true
            }
          };
        }
        else
        {
          ConnectionFailedDialog dlg = new ConnectionFailedDialog();
          dlg.ErrorMessage = "Cannot start Smart Eye Tracker Core Process, registry parameters not correct.";
          dlg.ShowDialog();
          return;
        }
      }
      else
      {
        ConnectionFailedDialog dlg = new ConnectionFailedDialog();
        dlg.ErrorMessage = "Cannot start Smart Eye Tracker Core Process, registry parameters not correct.";
        dlg.ShowDialog();
        return;
      }

      try
      {
        if (!smartEyeTrackingProcess.Start())
        {
          ConnectionFailedDialog dlg = new ConnectionFailedDialog();
          dlg.ErrorMessage = "Cannot start Smart Eye Tracker Core Process." + Environment.NewLine + Environment.NewLine +
          "If this error is recurring, please make sure the hardware is connected and set up correctly, and try to reconnect.";
          dlg.ShowDialog();
          return;
        }
      }
      catch (Exception ex)
      {
        ConnectionFailedDialog dlg = new ConnectionFailedDialog();
        dlg.ErrorMessage = "Cannot start Smart Eye Tracker Core Process, failed with the following message: " +
             Environment.NewLine + ex.Message + Environment.NewLine + Environment.NewLine +
            "If this error is recurring, please make sure the hardware is connected and set up correctly, and try to reconnect.";
        dlg.ShowDialog();
        ExceptionMethods.HandleExceptionSilent(ex);
        return;
      }

      this.CreateRPC();
      this.CreateUDP(this.smartEyeSettings.SmartEyeServerAddress, this.smartEyeSettings.OgamaPort);
    }

    #endregion CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler                                                              //
    ///////////////////////////////////////////////////////////////////////////////
    #region EVENTS

    /// <summary>
    /// Event. Raised, when a property changed.
    /// </summary>
    public event PropertyChangedEventHandler PropertyChanged;

    /// <summary>
    /// Event. Raised, when new gaze data is available.
    /// </summary>
    public event GazeDataReceivedEventHandler GazeDataAvailable;

    #endregion EVENTS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets the UDP socket to get gaze data.
    /// </summary>
    public UdpSocketClient UdpSocket { get; private set; }

    /// <summary>
    /// Gets the RPC client to send commands.
    /// </summary>
    public JsonRpcClient RpcClient { get; private set; }

    /// <summary>
    /// Gets or sets a value indicating whether UDP socket is receiving data
    /// </summary>
    public bool UdpIsConnected
    {
      get
      {
        return this.udpIsConnected;
      }

      set
      {
        if (this.udpIsConnected == value)
        {
          return;
        }

        this.udpIsConnected = value;
        this.OnPropertyChanged("UdpIsConnected");
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether connection to Smart Eye RPC server is established.
    /// </summary>
    public bool RpcIsConnected { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether closing down connection to inhibit reconnection
    /// </summary>
    public bool IsClosingDown { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether calibrating in client to inhibit reconnection
    /// </summary>
    public bool IsCalibrating { get; set; }

    /// <summary>
    /// Gets or sets a value of the RPC major version in use
    /// </summary>
    public int MajorVersion { get; set; }

    /// <summary>
    /// Gets or sets a value of the RPC minor version in use
    /// </summary>
    public int MinorVersion { get; set; }

    #endregion PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Public methods                                                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region PUBLICMETHODS

    /// <summary>
    /// Create the Smart Eye RPC client.
    /// </summary>
    public void CreateRPC()
    {
      this.IsClosingDown = false;
      this.IsCalibrating = false;

      if (this.RpcClient != null)
      {
        try
        {
          this.RpcClient.Disconnect();
        }
        catch (Exception ex)
        {
          if (this.smartEyeSettings.SilentMode)
          {
            ExceptionMethods.HandleExceptionSilent(ex);
          }
          else
          {
            ExceptionMethods.HandleException(ex);
          }
        }
      }

      this.RpcIsConnected = false;
      this.RpcClient = new JsonRpcClient();
      this.ConnectRPC();
    }

    /// <summary>
    /// Connect to the Smart Eye RPC server.
    /// </summary>
    public void ConnectRPC()
    {
      try
      {
        this.RpcClient.Connect(this.smartEyeSettings.SmartEyeServerAddress, this.smartEyeSettings.SmartEyeRPCPort);
      }
      catch (Exception ex)
      {
        if (this.smartEyeSettings.SilentMode)
        {
          ExceptionMethods.HandleExceptionSilent(ex);
        }
        else
        {
          ExceptionMethods.HandleException(ex);
        }

        return;
      }

      this.RpcIsConnected = true;

      try
      {
        int majorVersion, minorVersion;
        this.RpcClient.GetVersion(out majorVersion, out minorVersion);
        this.MajorVersion = majorVersion;
        this.MinorVersion = minorVersion;
      }
      catch (Exception ex)
      {
        if (this.smartEyeSettings.SilentMode)
        {
          ExceptionMethods.HandleExceptionSilent(ex);
        }
        else
        {
          ExceptionMethods.HandleException(ex);
        }
      }

      this.SetupNetworkDataSubscription();

      try
      {
        this.RpcClient.StartTracking();
      }
      catch (Exception ex)
      {
        if (this.smartEyeSettings.SilentMode)
        {
          ExceptionMethods.HandleExceptionSilent(ex);
        }
        else
        {
          ExceptionMethods.HandleException(ex);
        }
      }
    }

    /// <summary>
    /// Set up the UDP data stream in the Smart Eye tracker core
    /// </summary>
    public void SetupNetworkDataSubscription()
    {
      var dataList = new List<TrackerDataId>();

      dataList.AddRange(new[] 
      { 
        TrackerDataId.GazeDirectionQ, TrackerDataId.HeadPositionQ, 
        TrackerDataId.RealTimeClock, TrackerDataId.ClosestWorldIntersection, 
        TrackerDataId.LeftPupilDiameter, TrackerDataId.RightPupilDiameter 
      });

      string dataIds = dataList.Aggregate(string.Empty, (current, dataId) => current + (TrackerDataTypes.GetData(dataId).Name + ";"));

      try
      {
        this.RpcClient.OpenDataStreamUDP(this.smartEyeSettings.OgamaPort, dataIds);
      }
      catch (Exception ex)
      {
        if (this.smartEyeSettings.SilentMode)
        {
          ExceptionMethods.HandleExceptionSilent(ex);
        }
        else
        {
          ExceptionMethods.HandleException(ex);
        }
      }
    }

    /// <summary>
    /// Create UDP connection to get live gaze data.
    /// </summary>
    /// <param name="host">The Smart Eye server IP address.</param>
    /// <param name="port">The port Ogama is listening to (and Smart Eye is sending to).</param>
    public void CreateUDP(string host, int port)
    {
      this.IsClosingDown = false;

      IPAddress ip = IPAddress.Parse(host);
      IPEndPoint ipEndPoint = new IPEndPoint(ip, port);

      if (this.UdpSocket != null)
      {
        this.UdpSocket.PacketReceived -= this.OnGazePacketReceived;
        this.UdpSocket.Close();
      }

      this.UdpSocket = new UdpSocketClient(ipEndPoint);

      this.UdpSocket.PacketReceived += this.OnGazePacketReceived;
      this.UdpSocket.PropertyChanged += this.OnUdpPropertyChanged;

      this.UdpIsConnected = this.UdpSocket.IsReceivingData;
    }

    /// <summary>
    /// Connect to the UDP socket with current settings
    /// </summary>
    /// <returns>True if connection was successful</returns>
    public bool ConnectUDP()
    {
      this.SetupNetworkDataSubscription();

      return this.UdpIsConnected = this.UdpSocket.IsReceivingData;
    }

    /// <summary>
    /// Ping the PRC server
    /// </summary>
    /// <returns>True if connected to RPC</returns>
    public bool PingRPC()
    {
      try
      {
        this.RpcClient.Ping();
        this.RpcIsConnected = true;
        return true;
      }
      catch (Exception)
      {
        this.RpcIsConnected = false;
        return false;
      }
    }

    /// <summary>
    /// Dispose the SmartEyeClient.
    /// </summary>
    public void Dispose()
    {
      this.IsClosingDown = true;

      if (this.RpcClient != null)
      {
        try
        {
          this.RpcClient.Shutdown();
        }
        catch (Exception ex)
        {
          ExceptionMethods.HandleExceptionSilent(ex);

          this.KillRunningEyeTrackerProcess();
        }
      }
      else
      {
        ExceptionMethods.ProcessMessage("Warning", "Smart Eye Tracker Core could not be shut down.");
      }

      this.RpcClient.Dispose();
      this.DisconnectUDP();

      this.RpcClient = null;
      this.UdpSocket = null;
    }

    /// <summary>
    /// Handle property changed events.
    /// </summary>
    /// <param name="name">Name of changed property.</param>
    protected void OnPropertyChanged(string name)
    {
      PropertyChangedEventHandler handler = this.PropertyChanged;
      if (handler != null)
      {
        handler(this, new PropertyChangedEventArgs(name));
      }
    }

    #endregion PUBLICMETHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler                                                              //
    ///////////////////////////////////////////////////////////////////////////////
    #region EVENTHANDLER

    /// <summary>
    /// Update UDP socket properties
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The property changed arguments.</param>
    private void OnUdpPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
      if (e != null && e.PropertyName == "IsReceivingData")
      {
        this.UdpIsConnected = this.UdpSocket.IsReceivingData;
        if (!this.UdpIsConnected && !this.IsClosingDown && !this.IsCalibrating)
        {
          this.ConnectUDP();
        }
      }
    }

    /// <summary>
    /// Raised when new gaze data packet is received
    /// </summary>
    /// <param name="sender"><see cref="object"/> The sender</param>.
    /// <param name="e"><see cref="PacketReceivedEventArgs"/> event arguments</param>.
    private void OnGazePacketReceived(object sender, PacketReceivedEventArgs e)
    {
      this.UdpIsConnected = true;

      if (this.GazeDataAvailable != null)
      {
        SmartEyeGazeData gazeData;
        if (this.ParseGazeData(e.Packet, out gazeData))
        {
          this.GazeDataAvailable(this, new GazeDataReceivedEventArgs(gazeData));
        }
      }
    }

    #endregion EVENTHANDLER

    ///////////////////////////////////////////////////////////////////////////////
    // Methods for doing main class job                                          //
    ///////////////////////////////////////////////////////////////////////////////
    #region PRIVATEMETHODS

    /// <summary>
    /// Parse the gaze data packet into a <see cref="SmartEyeGazeData"/> gazeData object.
    /// </summary>
    /// <param name="packet">The network packet.</param>
    /// <param name="gazeData">The gaze data.</param>
    /// <returns>True if parsing was successful.</returns>
    private bool ParseGazeData(NetworkDataPacket packet, out SmartEyeGazeData gazeData)
    {
      gazeData = new SmartEyeGazeData();
      NetworkData data;

      if (packet.TryGetValue(TrackerDataId.GazeDirectionQ, out data))
      {
        gazeData.GazeQuality = data.GetValue<double>();
      }

      if (packet.TryGetValue(TrackerDataId.HeadPositionQ, out data))
      {
        gazeData.HeadQuality = data.GetValue<double>();
      }

      if (packet.TryGetValue(TrackerDataId.RealTimeClock, out data))
      {
        gazeData.RealTime = (long)data.GetValue<ulong>();
        gazeData.Time = (long)(gazeData.RealTime / 10000);
      }

      if (packet.TryGetValue(TrackerDataId.ClosestWorldIntersection, out data))
      {
        var intersection = data.GetValue<WorldIntersection>();
        gazeData.GazePosX = (float)intersection.ObjectPoint.X / Document.ActiveDocument.PresentationSize.Width;
        gazeData.GazePosY = (float)intersection.ObjectPoint.Y / Document.ActiveDocument.PresentationSize.Height;
      }

      if (packet.TryGetValue(TrackerDataId.LeftPupilDiameter, out data))
      {
        gazeData.PupilDiaX = (float)data.GetValue<double>();
      }

      if (packet.TryGetValue(TrackerDataId.RightPupilDiameter, out data))
      {
        gazeData.PupilDiaY = (float)data.GetValue<double>();
      }

      return true;
    }

    /// <summary>
    /// Disconnect from the RPC server.
    /// </summary>
    private void DisconnectRPC()
    {
      if (this.RpcClient != null)
      {
        try
        {
          this.RpcClient.Disconnect();
        }
        catch (Exception ex)
        {
          ExceptionMethods.HandleExceptionSilent(ex);
        }
      }
    }

    /// <summary>
    /// Disconnect the UDP socket.
    /// </summary>
    private void DisconnectUDP()
    {
      if (this.UdpSocket != null)
      {
        try
        {
          this.UdpSocket.PacketReceived -= this.OnGazePacketReceived;
          this.UdpSocket.Close();
          ////this.UdpSocket.Dispose();
        }
        catch (Exception ex)
        {
          ExceptionMethods.HandleExceptionSilent(ex);
        }
      }
    }

    /// <summary>
    /// Disconnect both RPC and UDP connections.
    /// </summary>
    private void Disconnect()
    {
      this.DisconnectRPC();
      this.DisconnectUDP();
    }

    /// <summary>
    /// Kill a running eye tracker core process - used when most presumably crashed
    /// </summary>
    private void KillRunningEyeTrackerProcess()
    {
      var p = Process.GetProcessesByName("eye_tracker_core");
      if (p.Length > 0)
      {
        Process runningETC = p.First();
        runningETC.Kill();

        Thread.Sleep(2000);
      }
    }

    #endregion PRIVATEMETHODS
  }
}
