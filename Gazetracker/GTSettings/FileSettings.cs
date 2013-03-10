// <copyright file="FileSettings.cs" company="ITU">
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
using System.IO;
using System.Xml;

namespace GTSettings
{
    using GTCommons;

    public class FileSettings : INotifyPropertyChanged
    {
        #region CONSTANTS

        public const string Name = "FileSettings";

        #endregion //CONSTANTS

        #region FIELDS

        private string logFilePath;
        private bool loggingEnabled;
        private string settingsDirectory;
        private string settingsName;

        #endregion //FIELDS

        #region CONSTRUCTION

        public FileSettings()
        {
            string appDataPath = GTPath.GetLocalApplicationDataPath() +
                           Path.DirectorySeparatorChar + "Settings";

            if (!Directory.Exists(appDataPath))
            {
                Directory.CreateDirectory(appDataPath);
            }

            settingsDirectory = appDataPath;
            loggingEnabled = false;
            logFilePath = "";
            settingsName = "";
        }

        #endregion //CONSTRUCTION

        #region EVENTS

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion EVENTS

        #region PROPERTIES

        public string SettingsDirectory
        {
            get { return settingsDirectory; }
            set
            {
                settingsDirectory = value;
                OnPropertyChanged("SettingsDirectory");
            }
        }

        public string SettingsName
        {
            get { return settingsName; }
            set
            {
                settingsName = value;
                OnPropertyChanged("SettingsName");
            }
        }

        public bool LoggingEnabled
        {
            get { return loggingEnabled; }
            set
            {
                loggingEnabled = value;
                OnPropertyChanged("LoggingEnabled");
            }
        }

        public string LogFilePath
        {
            get { return logFilePath; }
            set
            {
                logFilePath = value;
                OnPropertyChanged("LogFilePath");
            }
        }

        #endregion //PROPERTIES

        #region PUBLICMETHODS

        public void WriteConfigFile(XmlTextWriter xmlWriter)
        {
            xmlWriter.WriteStartElement(Name);

            Settings.WriteElement(xmlWriter, "SettingsName", SettingsName);
            Settings.WriteElement(xmlWriter, "SettingsDirectory", SettingsDirectory);
            Settings.WriteElement(xmlWriter, "LoggingEnabled", LoggingEnabled.ToString());
            Settings.WriteElement(xmlWriter, "LogFilePath", LogFilePath);

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
                            case "SettingsName":
                                SettingsName = xmlReader.Value;
                                break;
                            case "SettingsDirectory":
                                SettingsDirectory = xmlReader.Value;
                                break;
                            case "LoggingEnabled":
                                LoggingEnabled = Boolean.Parse(xmlReader.Value);
                                break;
                            case "LogFilePath":
                                LogFilePath = xmlReader.Value;
                                break;
                        }
                        break;
                }
            }
        }

        #endregion //PUBLICMETHODS

        #region PRIVATEMETHODS

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