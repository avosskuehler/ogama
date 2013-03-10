// <copyright file="NetworkSettings.cs" company="ITU">
// ******************************************************
// GazeTrackingLibrary for ITU GazeTracker
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
// <author>Martin Tall</author>
// <email>tall@stanford.edu</email>
// <modifiedby>Adrian Voßkühler</modifiedby>

using System;
using System.ComponentModel;
using System.Net;
using System.Xml;

namespace GTSettings
{
    public class Network : INotifyPropertyChanged
    {
        #region Constants

        public const string Name = "NetworkSettings";

        #endregion //CONSTANTS

        #region Variables

        private string clientUIPath = "";
        private bool isSendingXML;
        private bool tcpIPServerEnabled = true;
        private string tcpIPServerIPAddress = IPAddress.Loopback.ToString();
        private int tcpIPServerPort = 5555; // default

        private bool udpServerEnabled = true;
        private string udpServerIPAddress = IPAddress.Loopback.ToString();
        private int udpServerPort = 6666; // default
        private bool udpServerSendSmoothedData = true;

        #endregion

        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Get/Set properties

        public bool TCPIPServerEnabled
        {
            get { return tcpIPServerEnabled; }
            set
            {
                tcpIPServerEnabled = value;
                OnPropertyChanged("TCPIPServerEnabled");
            }
        }

        public string TCPIPServerIPAddress
        {
            get { return tcpIPServerIPAddress; }
            set
            {
                tcpIPServerIPAddress = value;
                OnPropertyChanged("TCPIPServerIPAddress");
            }
        }

        public int TCPIPServerPort
        {
            get { return tcpIPServerPort; }
            set
            {
                tcpIPServerPort = value;
                OnPropertyChanged("TCPIPServerPort");
            }
        }


        public bool UDPServerEnabled
        {
            get { return udpServerEnabled; }
            set
            {
                udpServerEnabled = value;
                OnPropertyChanged("UDPServerEnabled");
            }
        }

        public string UDPServerIPAddress
        {
            get { return udpServerIPAddress; }
            set
            {
                udpServerIPAddress = value;
                OnPropertyChanged("UDPServerIPAddress");
            }
        }

        public int UDPServerPort
        {
            get { return udpServerPort; }
            set
            {
                udpServerPort = value;
                OnPropertyChanged("UDPServerPort");
            }
        }

        public bool UDPServerSendSmoothedData
        {
            get { return udpServerSendSmoothedData; }
            set
            {
                udpServerSendSmoothedData = value;
                OnPropertyChanged("UDPServerSendSmoothedData");
            }
        }

        public string ClientUIPath
        {
            get { return clientUIPath; }
            set
            {
                clientUIPath = value;
                //if (null != this.PropertyChanged)
                //    PropertyChanged(this, new PropertyChangedEventArgs("LogFilePath"));
            }
        }

        public bool IsSendingXML
        {
            get { return isSendingXML; }
            set { isSendingXML = value; }
        }

        #endregion //PROPERTIES

        #region Public methods

        public void WriteConfigFile(XmlTextWriter xmlWriter)
        {
            xmlWriter.WriteStartElement(Name);

            Settings.WriteElement(xmlWriter, "TCPIPServerEnabled", TCPIPServerEnabled.ToString());
            Settings.WriteElement(xmlWriter, "TCPIPServerIPAddress", TCPIPServerIPAddress);
            Settings.WriteElement(xmlWriter, "TCPIPServerPort", TCPIPServerPort.ToString());

            Settings.WriteElement(xmlWriter, "UDPServerEnabled", UDPServerEnabled.ToString());
            Settings.WriteElement(xmlWriter, "UDPServerIPAddress", UDPServerIPAddress);
            Settings.WriteElement(xmlWriter, "UDPServerPort", UDPServerPort.ToString());
            Settings.WriteElement(xmlWriter, "UDPServerSendSmoothedData", UDPServerSendSmoothedData.ToString());

            Settings.WriteElement(xmlWriter, "ClientUIPath", ClientUIPath);
            Settings.WriteElement(xmlWriter, "IsSendingXML", isSendingXML.ToString());

            xmlWriter.WriteEndElement();
        }

        public void LoadConfigFile(XmlReader xmlReader)
        {
            string sName = string.Empty;

            while (xmlReader.Read())
            {
                switch (xmlReader.NodeType)
                {
                    case XmlNodeType.Element:
                        sName = xmlReader.Name;
                        break;
                    case XmlNodeType.Text:
                        switch (sName)
                        {
                            case "TCPIPServerEnabled":
                                TCPIPServerEnabled = Boolean.Parse(xmlReader.Value);
                                break;
                            case "TCPIPServerIPAddress":
                                TCPIPServerIPAddress = xmlReader.Value;
                                break;
                            case "TCPIPServerPort":
                                TCPIPServerPort = Int32.Parse(xmlReader.Value);
                                break;

                            case "UDPServerEnabled":
                                UDPServerEnabled = Boolean.Parse(xmlReader.Value);
                                break;
                            case "UDPServerIPAddress":
                                UDPServerIPAddress = xmlReader.Value;
                                break;
                            case "UDPServerPort":
                                UDPServerPort = Int32.Parse(xmlReader.Value);
                                break;
                            case "UDPServerSendSmoothedData":
                                UDPServerSendSmoothedData = Boolean.Parse(xmlReader.Value);
                                break;

                            case "ClientUIPath":
                                ClientUIPath = xmlReader.Value;
                                break;
                            case "IsSendingXML":
                                IsSendingXML = bool.Parse(xmlReader.Value);
                                break;
                        }
                        break;
                }
            }
        }

        #endregion //PUBLICMETHODS

        #region Private methods

        private void OnPropertyChanged(string parameter)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(parameter));
            }
        }

        #endregion //PRIVATEMETHODS
    }
}