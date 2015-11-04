// <copyright file="ClientNetworkManager.cs" company="Gazepoint">
// ******************************************************
// OGAMA - open gaze and mouse analyzer 
// Copyright (C) 2015 Dr. Adrian Voßkühler  
// ------------------------------------------------------------------------
// This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
// This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
// You should have received a copy of the GNU General Public License along with this program; if not, write to the Free Software Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
// **************************************************************
// </copyright>
// <author>Herval Yatchou</author>
// <email>herval.yatchou@tandemlaunchtech.com</email>
// <modifiedby>Andras Pattantyus, andras@gazept.com</modifiedby>

namespace Ogama.Modules.Recording.GazepointInterface
{
  using System;
  using System.Collections.Generic;
  using System.IO;
  using System.Linq;
  using System.Net.Sockets;
  using GTCommons.Events;
  using Ogama.ExceptionHandling;

  /// <summary>
  /// Delegate to warn GazepointTacker about incoming message
  /// </summary>
  /// <param name="newMessageArgs">Arguments of the event</param>
  public delegate void NewMessageEventHandler(StringEventArgs newMessageArgs);

  /// <summary>
  /// Class to manage network interaction with the tracker
  /// </summary>    
  public class ClientNetworkManager
  {
    #region FIELDS

    /// <summary>
    /// Vector containing all currently availables connections.
    /// </summary>
    private Connections[] memAvailableConnections;

    /// <summary>
    /// This boolean ensure that all connections are always availables.
    /// </summary>
    private bool memValidConnections;

    /// <summary>
    /// Number of currently valids connections on the network.
    /// </summary>
    private int memNumberOfConnections;

    /// <summary>
    /// Callback to handle new incoming data from the network.
    /// </summary>
    private AsyncCallback memDataReceivedCallback;

    #endregion

    #region CONSTRUCTOR

    /// <summary>
    /// Initializes a new instance of the ClientNetworkManager class.
    /// </summary>
    /// <param name="numbConnections">Number of connections needed. Normally correspond to the number of trackers available</param>
    /// <param name="addresses">Connections addresses</param>
    /// <param name="ports">Connections ports</param>
    /// <param name="connectionsIDs">Connections identification numbers</param>
    public ClientNetworkManager(int numbConnections, string[] addresses, int[] ports, out int[] connectionsIDs)
    {
      this.memNumberOfConnections = numbConnections;
      this.memAvailableConnections = new Connections[this.memNumberOfConnections];
      connectionsIDs = null;
      connectionsIDs = new int[numbConnections];

      for (int i = 0; i < this.memNumberOfConnections; i++)
      {
        connectionsIDs[i] = i;
        this.memAvailableConnections[i] = new Connections(i, addresses[i], ports[i]);
      }

      this.AllConnected = false;
    }

    /// <summary>
    /// Initializes a new instance of the ClientNetworkManager class.
    /// </summary>
    /// <param name="address">Connection address</param>
    /// <param name="port">Connection port</param>
    /// <param name="connectionID">Connection identification number</param>
    public ClientNetworkManager(string address, int port, out int[] connectionID)
    {
      this.memNumberOfConnections = 1;
      connectionID = null;
      connectionID = new int[1];
      connectionID[0] = 0;
      this.memAvailableConnections = new Connections[this.memNumberOfConnections];
      for (int i = 0; i < this.memNumberOfConnections; i++)
      {
        this.memAvailableConnections[i] = new Connections(i, address, port);
      }

      this.AllConnected = false;
    }

    #endregion

    #region EVENTS

    /// <summary>
    /// Event to handle incoming message
    /// </summary>
    public event NewMessageEventHandler MessageReceived;

    #endregion

    #region PROPERTIES

    /// <summary>
    /// Gets or sets a value indicating whether ALL connections succeeded.
    /// Verify all the connections. To be true, ALL the connections must succeed !
    /// </summary>
    public bool AllConnected
    {
      get
      {
        return this.memValidConnections;
      }

      set
      {
        this.memValidConnections = value;
      }
    }

    #endregion

    #region METHODS

    /// <summary>
    /// Connect on server(s)
    /// </summary>
    /// <returns>Return "true" if connections succeeded, "false" otherwise</returns>
    public bool Connect()
    {
      for (int i = 0; i < this.memNumberOfConnections; i++)
      {
        this.memAvailableConnections[i].Connect();
        this.WaitForData(this.memAvailableConnections[i]);
      }

      this.AllConnected = true;
      return true;
    }

    /// <summary>
    /// Change the connection address of the identified connection
    /// </summary>
    /// <param name="connectionID">Connection identification number</param>
    /// <param name="newAddress">New address to set</param>
    public void ChangeAddress(int connectionID, string newAddress)
    {
      this.memAvailableConnections[connectionID].Address = newAddress;
    }

    /// <summary>
    /// Change the connection port of the identified connection
    /// </summary>
    /// <param name="connectionID">Connection identification number</param>
    /// <param name="newPort">New port to set</param>
    public void ChangePort(int connectionID, int newPort)
    {
      this.memAvailableConnections[connectionID].Port = newPort;
    }

    /// <summary>
    /// Disconnect from server(s)
    /// </summary>
    public void Disconnect()
    {
      for (int i = 0; i < this.memNumberOfConnections; i++)
      {
        this.memAvailableConnections[i].Disconnect();
      }
    }

    /// <summary>
    /// Send message to the server. 
    /// </summary>
    /// <param name="validMessage"> Valid message to send. Ensure that the message is understable by the server </param>
    /// <returns>Boolean returned value indicating whether message was succefully sent (true) or not (false)</returns>
    public bool SendMessage(string validMessage)
    {
      try
      {
        for (int i = 0; i < this.memNumberOfConnections; i++)
        {
          this.memAvailableConnections[i].Send(validMessage);
        }

        return true;
      }
      catch (Exception e)
      {
        string message = "Exception catched in SendMessage(...) Method; Unable to send data to server(s) ! " +
            Environment.NewLine + e.Message;
        ExceptionMethods.ProcessErrorMessage(message);
        return false;
      }
    }

    /// <summary>
    /// Process received message from server. 
    /// This method is called when a asynchronous waiting for data from server(s) completes
    /// </summary>
    /// <param name="callbackResults">Additional data received from the caller</param>
    private void OnDataReceived(IAsyncResult callbackResults)
    {
      Connections serverConnection = (Connections)callbackResults.AsyncState;
      try
      {
        if (serverConnection.ConnectionStream == null || !serverConnection.ConnectionStream.CanRead)
        {
          return;
        }

        int readBytes = serverConnection.ConnectionStream.EndRead(callbackResults);
        string stringFromBytes = System.Text.Encoding.ASCII.GetString(serverConnection.BytesArrayQueue.First(), 0, serverConnection.BytesArrayQueue.First().Length);

        // We remove received bytes when they are processed
        serverConnection.BytesArrayQueue.Dequeue();

        // Warn GazepointTracker class about incoming message
        this.MessageReceived(new StringEventArgs(stringFromBytes));
        this.WaitForData(serverConnection);
      }
      catch (Exception e)
      {
        string message = "Exception catched in OnDataReceived(...) Method : " +
            Environment.NewLine + e.Message;
        ExceptionMethods.ProcessErrorMessage(message);
      }
    }

    /// <summary>
    /// Wait for data from server. 
    /// </summary>
    /// <param name="connection"> Current connection on which we are waiting data </param>
    private void WaitForData(Connections connection)
    {
      try
      {
        if (connection.ConnectionStream == null)
        {
          return;
        }

        if (this.memDataReceivedCallback == null)
        {
          // Create a callback used by the "BeginRead(...)" function
          // Called when data is received
          this.memDataReceivedCallback = new AsyncCallback(this.OnDataReceived);
        }

        connection.BytesArrayQueue.Enqueue(new byte[2048]);  // TODO : We are not sure that 2048 will be sufficient ! More efficient to read exactly the expected number of bytes 
        // Doing this also insure that we are not currently reading the next message, beacause this can cause an exception in the message parsing.
        connection.ConnectionStream.BeginRead(connection.BytesArrayQueue.Last(), 0, connection.BytesArrayQueue.Last().Length, this.memDataReceivedCallback, connection);
      }
      catch (Exception e)
      {
        string message = " Exception catched in WaitForData(...) Method ; Unable to read datas from server : {" + connection.Id + "}" +
            Environment.NewLine + e.Message;
        ExceptionMethods.ProcessErrorMessage(message);
      }
    }

    #endregion

    /// <summary>
    /// Class for each connection needed
    /// </summary>
    private class Connections
    {
      /// <summary>
      /// Identification number of each connection
      /// </summary>
      private int memConnectionId;

      /// <summary>
      /// TCP connection of each connection
      /// </summary>
      private TcpClient memTcpConnection;

      /// <summary>
      /// Provides the underlying stream of data for network access.
      /// </summary>
      private NetworkStream memStream;

      /// <summary>
      /// Text writer for writing characters to a stream in a particular encoding.
      /// Used to write any informations that we want to send over the network.
      /// </summary>
      private StreamWriter memStreamWriter;

      /// <summary>
      /// Address of the connection
      /// </summary>
      private string memConnectionAddress;

      /// <summary>
      /// Connection port of the connection address.
      /// </summary>
      private int memConnectionPort;

      /// <summary>
      /// Byte array queue to store incoming messages
      /// </summary>
      private Queue<byte[]> memByteArray;

      /// <summary>
      /// Initializes a new instance of the Connections class.
      /// </summary>
      /// <param name="id">Connection index</param>
      /// <param name="address">Connection address</param>
      /// <param name="port">Connection port</param>
      public Connections(int id, string address, int port)
      {
        this.memConnectionId = id;
        this.memConnectionAddress = address;
        this.memConnectionPort = port;
        this.memByteArray = new Queue<byte[]>();
      }

      /// <summary>
      /// Gets the Id of the current connection
      /// </summary>
      public int Id
      {
        get
        {
          return this.memConnectionId;
        }
      }

      /// <summary>
      /// Gets or sets the address of the current connection
      /// </summary>
      public string Address
      {
        get
        {
          return this.memConnectionAddress;
        }

        set
        {
          this.memConnectionAddress = value;
        }
      }

      /// <summary>
      /// Gets or sets the port of the current connection
      /// </summary>
      public int Port
      {
        get
        {
          return this.memConnectionPort;
        }

        set
        {
          this.memConnectionPort = value;
        }
      }

      /// <summary>
      /// Gets bytes array queue of current connection
      /// </summary>
      public Queue<byte[]> BytesArrayQueue
      {
        get
        {
          return this.memByteArray;
        }
      }

      /// <summary>
      /// Gets the connection stream of current connection
      /// </summary>
      public NetworkStream ConnectionStream
      {
        get
        {
          return this.memStream;
        }
      }

      /// <summary>
      ///  Connect to the server with current connection
      /// </summary>           
      public void Connect()
      {
        this.memTcpConnection = new TcpClient(this.memConnectionAddress, this.memConnectionPort);
        this.memStream = this.memTcpConnection.GetStream();
        this.memStreamWriter = new StreamWriter(this.memStream);
      }

      /// <summary>
      /// Disconnect from server with current connection
      /// </summary>
      public void Disconnect()
      {
        if (this.memStreamWriter != null)
        {
          this.memStreamWriter.Close();
        }

        if (this.memStream != null)
        {
          this.memStream.Close();
        }

        if (this.memTcpConnection != null)
        {
          this.memTcpConnection.Close();
        }

        this.memByteArray.Clear();
      }

      /// <summary>
      ///  Send a message from current connection to the corresponding server
      /// </summary>
      /// <param name="message">Message to send</param>
      public void Send(string message)
      {
        this.memStreamWriter.Write(message);
        this.memStreamWriter.Flush();
      }
    }
  }
}
