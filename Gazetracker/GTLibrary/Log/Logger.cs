// <copyright file="Logger.cs" company="ITU">
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
using System.IO;
using System.Text;

using GTNetworkClient;

using GTCommons.Enum;

namespace GTLibrary.Logging
{
    using GTCommons;

    using GTLibrary.Network;

  /// <summary>
    /// This class is used to log messages and data to a file.
    /// </summary>
    public class Logger
    {
        #region CONSTANTS

        #endregion //CONSTANTS

        #region FIELDS

        private FileStream fs;
        private bool isEnabled;
        private bool isSystemLog;
        private string logFilePath;
        private LogLevel logLevel = LogLevel.Essential;
        private StreamWriter logWriter;
        private UDPServer server;

        #endregion //FIELDS

        #region PROPERTIES

        /// <summary>
        /// Gets or sets a value indicating whether the data logging is enabled.
        /// </summary>
        public bool IsEnabled
        {
            get { return isEnabled; }

            set
            {
                // If logger is running and its being turned off, flush and close file
                if (isEnabled && value == false && logWriter != null)
                {
                    logWriter.Flush();
                    logWriter.Close();
                    logWriter = null;

                    if (server != null && IsSystemLog == false)
                    {
                        server.SendMessage(Commands.LogStop);
                    }
                }

                isEnabled = value;

                if (isEnabled && server != null && IsSystemLog == false)
                {
                    server.SendMessage(Commands.LogStart);
                }
            }
        }

        public string LogFilePath
        {
            get { return logFilePath; }

            set
            {
                if (value != logFilePath)
                {
                    logFilePath = value;

                    if (server != null && IsSystemLog == false)
                    {
                        server.SendMessage(Commands.LogPathSet + " " + logFilePath);
                    }
                }
            }
        }

        public UDPServer Server
        {
            set { server = value; }
        }

        public bool IsSystemLog
        {
            get { return isSystemLog; }
            set { isSystemLog = value; }
        }

        //public LogLevel LogLevel
        //{
        //    get { return logLevel; }
        //    set { logLevel = value; }
        //}

        #endregion //PROPERTIES

        #region PUBLICMETHODS

        public void LogData(TrackData trackData)
        {
            WriteLine(BuildLogLine(trackData));
        }

        /// <summary>
        /// Writes the given line into the log file.
        /// </summary>
        /// <param name="line">A <see cref="string"/> with the text to be written.</param>
        public void WriteLine(string line)
        {
            // logwriter is created when logfilepath is set, if not use default "gazeLog.txt"
            if (logWriter == null)
            {
                if (logFilePath == null)
                {
                    fs =
                        new FileStream(
                            GTPath.GetLocalApplicationDataPath() +
                           Path.DirectorySeparatorChar + "gazeLog.txt",
                            FileMode.Create);
                }
                else
                {
                    fs = new FileStream(logFilePath, FileMode.Append);
                }

                logWriter = new StreamWriter(fs);
            }

            if (isSystemLog)
            {
                logWriter.WriteLine(DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() + " " +
                                    line);
            }
            else
            {
                logWriter.WriteLine(line);
            }

            logWriter.Flush();
        }

        /// <summary>
        /// Method to save imgage counter, time stamp, raw coordinates and smoothed coordinates
        /// </summary>
        /// <param name="imgCounter">The Instance frame number</param>
        /// <param name="time">The frame time.</param>
        /// <param name="gazeX">The x coordinate of the Instance gaze position.</param>
        /// <param name="gazeY">The y coordinate of the Instance gaze position.</param>
        public void LogData(int imgCounter, long time, double gazeX, double gazeY)
        {
            string line = imgCounter + "\t" + time + "\t" + gazeX + "\t" + gazeY;
            WriteLine(line);
        }

        #endregion //PUBLICMETHODS

        #region PRIVATEMETHODS

        public string BuildLogLine(TrackData td)
        {
            var sb = new StringBuilder();
            string tab = "\t";

            sb.Append(td.TimeStamp + tab);
            sb.Append(td.FrameNumber + tab);
            sb.Append(td.GazeDataRaw.GazePositionX + tab);
            sb.Append(td.GazeDataRaw.GazePositionY + tab);
            sb.Append(td.GazeDataSmoothed.GazePositionX + tab);
            sb.Append(td.GazeDataSmoothed.GazePositionY + tab);
            sb.Append(td.PupilDataLeft.Diameter + tab);
            sb.Append(td.PupilDataRight.Diameter + tab);

            if (GTSettings.Settings.Instance.Processing.TrackingMethod == TrackingMethodEnum.RemoteBinocular)
                sb.Append(td.PupilDataRight.Diameter + tab);

            if (logLevel == LogLevel.Full)
            {
            }

            return sb.ToString();
        }

        #endregion //PRIVATEMETHODS
    }
}