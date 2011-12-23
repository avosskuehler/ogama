// <copyright file="ClientNetworkManager.cs" company="Mirametrix">
// ******************************************************
// OGAMA - open gaze and mouse analyzer 
// Copyright (C) 2010 Adrian Voßkühler  
// ------------------------------------------------------------------------
// This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
// This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
// You should have received a copy of the GNU General Public License along with this program; if not, write to the Free Software Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
// **************************************************************
// </copyright>
// <author>Herval Yatchou</author>
// <email>herval.yatchou@tandemlaunchtech.com</email>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace Ogama.Modules.Recording.MirametrixInterface
{
    /// <summary>
    /// Delegate to warn MirametrixTacker about incoming message
    /// </summary>
    /// <param name="newMessageArgs">Arguments of the event</param>
    public delegate void NewMessageEventHandler( MessageReceivedEventArgs newMessageArgs);

    
    class ClientNetworkManager
    {
        #region Constructors and Destructors

        /// <summary>
        /// Network manager class
        /// </summary>
        /// <param name="numberOfConnections"></param>
        /// <param name="adresses"></param>
        /// <param name="ports"></param>
        public ClientNetworkManager(int numberOfConnections, string[] adresses, int[] ports)
        {
            m_number_of_connections = numberOfConnections;
            m_availables_connections = new Connections[m_number_of_connections];
            for (int i = 0; i < numberOfConnections; i++)
            {
                m_availables_connections[i] = new Connections(i, adresses[i], ports[i]);
            }
            AllConnected = false;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Event to handle incoming message
        /// </summary>
        public event NewMessageEventHandler MessageReceived;

        /// <summary>
        /// Connect on server(s)
        /// </summary>
        /// <returns></returns>
        public bool Connect() {
            for (int i = 0; i < m_number_of_connections; i++)
            {
                m_availables_connections[i].Connect();
                WaitForData(m_availables_connections[i]);               
            }
            AllConnected = true;
            return true;
        }

        /// <summary>
        /// Disconnect from server(s)
        /// </summary>
        public void Disconnect() {
            for (int i = 0; i < m_number_of_connections; i++)
            {
                m_availables_connections[i].Disconnect();
            }
        }

        /// <summary>
        /// Verify all the connections. To be true, ALL the connections must succeed !
        /// </summary>
        public bool AllConnected
        {
            get
            {
                return m_valid_connections;
            }
            set
            {
                m_valid_connections = value;
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

                // Warn MirametrixTracker class about incoming message
                MessageReceived(new MessageReceivedEventArgs(stringFromBytes));
                WaitForData(serverConnection);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception catched in OnDataReceived(...) Method : ", e);
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
                if (m_DataReceivedCallback == null)
                {
                    // Create a callback used by the "BeginRead(...)" function
                    // Called when data is received
                    m_DataReceivedCallback = new AsyncCallback(OnDataReceived);                
                }

                connection.BytesArrayQueue.Enqueue(new byte[2048]);  // TODO : We are not sure that 2048 will be sufficient ! More efficient to read exactly the expected number of bytes 
                                                                     // Doing this also insure that we are not currently reading the next message, beacause this can cause an exception in the message parsing.
                connection.ConnectionStream.BeginRead(connection.BytesArrayQueue.Last(), 0, connection.BytesArrayQueue.Last().Length, m_DataReceivedCallback, connection);
            }
            catch (Exception e)
            {
                Console.WriteLine(" Exception catched in WaitForData(...) Method ; Unable to read datas from server : {" + connection.Id + "}", e);
            }
        }

        /// <summary>
        /// Send message to the server. 
        /// </summary>
        /// <param name="validMessage"> Valid message to send. Ensure that the message is understable by the server </param>
        /// <returns></returns>
        public bool SendMessage(string validMessage)
        {
            try
            {
                for (int i = 0; i < m_number_of_connections; i++)
                {
                    m_availables_connections[i].Send(validMessage);
                }
                return true;
            }
            catch(Exception e) {
                Console.WriteLine("Exception catched in SendMessage(...) Method; Unable to send data to server(s) ! ", e);
                return false;
            }
        }
        #endregion

        #region Properties

        private Connections[] m_availables_connections;
        private bool m_valid_connections;
        private int m_number_of_connections;
        private AsyncCallback m_DataReceivedCallback;

        private class Connections
        {
            /// <summary>
            /// Connection class
            /// </summary>
            /// <param name="id">Index of the connection</param>
            /// <param name="adress">Connexion adress</param>
            /// <param name="port">Connection port</param>
            public Connections(int id, string adress, int port)
            {
                _connectionId = id;
                _connectionAdress = adress;
                _connectionPort = port;
                m_byte_array = new Queue<byte[]>();
                 
            }
            /// <summary>
            ///  Connect to the server with current connection
            /// </summary>
           
            public void Connect(){
                _tcpConnection = new TcpClient(_connectionAdress, _connectionPort);
                _stream = _tcpConnection.GetStream();
                _streamWriter = new StreamWriter(_stream);
            }

            /// <summary>
            /// Disconnect from server with current connection
            /// </summary>
            public void Disconnect() {
                if (_streamWriter != null)
                {
                    _streamWriter.Close();
                }
                if (_stream != null)
                {
                    _stream.Close();
                }
                if (_tcpConnection != null)
                {
                    _tcpConnection.Close();
                }         
                m_byte_array.Clear();
            }

            /// <summary>
            ///  Send a message from current connection to the corresponding server
            /// </summary>
            /// <param name="message">Message to send</param>
            public void Send(string message) {
                _streamWriter.Write(message);
                _streamWriter.Flush();
            }

            /// <summary>
            /// Get the Id of the current connection
            /// </summary>
            public int Id{
                get{
                    return _connectionId;
                }
            }

            /// <summary>
            /// Get bytes array queue of current connection
            /// </summary>
            public Queue<byte[]> BytesArrayQueue
            {
                get
                {
                    return m_byte_array;
                }
            }

            /// <summary>
            /// Get the connection stream of current connection
            /// </summary>
            public NetworkStream ConnectionStream
            {
                get
                {
                    return _stream;
                }
            } 
            private int _connectionId;
            private TcpClient _tcpConnection;
            private NetworkStream _stream;
            private StreamWriter _streamWriter;
            private string _connectionAdress;
            private int _connectionPort;
            private Queue<byte[]> m_byte_array;     // Byte array queue to store incoming messages
        };
        #endregion
    }
}
