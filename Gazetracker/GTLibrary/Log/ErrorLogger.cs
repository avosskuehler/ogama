// <copyright file="ErrorLogger.cs" company="ITU">
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
// <author>Adrian Voßkühler</author>
// <email>adrian@ogama.net</email>
// <modifiedby>Alastair Jeremy</modifiedby>

using System;
using System.IO;
using System.Reflection;
using System.Windows;
using log4net;

namespace GTLibrary.Logging
{
    using GTCommons;

    /// <summary>
    /// This class is used to log errors and exceptions into a file that can 
    /// be used for debug purposes of user systems. (Can be send to support)
    /// Its members are static so it can be called from every code line
    /// without instatiation.
    /// </summary>
    public class ErrorLogger
    {
        #region FIELDS

        /// <summary>
        /// Logger for log4net logging
        /// </summary>
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Indicator that can be used for high speed DEBUG level logging. Note that using this flag will prevent automated reload
        /// of log4net configuration for that log statement during program operation
        /// </summary>
        private static readonly bool isDebugEnabled = log.IsDebugEnabled;

        /// <summary>
        /// The <see cref="FileStream"/> that gets the messages.
        /// </summary>
        private static FileStream fs;

        /// <summary>
        /// The <see cref="StreamWriter"/> that performs the writing to file.
        /// </summary>
        private static StreamWriter logWriter;

        #endregion //FIELDS

        #region CONSTRUCTION

        /// <summary>
        /// Prevents a default instance of the ErrorLogger class from being created.
        /// This class is only used statically.
        /// </summary>
        private ErrorLogger()
        {
        }

        /// <summary>
        /// Finalizes an instance of the ErrorLogger class by closing the file connection.
        /// </summary>
        ~ErrorLogger()
        {
            if (logWriter != null)
            {
                logWriter.Close();
            }
        }

        #endregion //CONSTRUCTION

        #region EVENTS

        #region Delegates

        /// <summary>
        /// This is the delegate for the <see cref="TrackerError"/> event.
        /// </summary>
        /// <param name="message">A <see cref="string"/> with the 
        /// message to show in the error dialog.</param>
        public delegate void TrackerErrorMessageHandler(string message);

        #endregion

        /// <summary>
        /// This event is raised when a customized ITU GazeTracker dialog 
        /// should be shown with a specific message.
        /// </summary>
        public static event TrackerErrorMessageHandler TrackerError;

        #endregion EVENTS

        #region PROPERTIES

        #endregion //PROPERTIES

        #region PUBLICMETHODS

        /// <summary>
        /// This method writes the given line into the ErrorLog.txt file.
        /// </summary>
        /// <param name="line">A <see cref="string"/> toi be written to file.</param>
        public static void WriteLine(string line)
        {
            try
            {
                // Use always ErrorLog.txt in LocalApplicationData
                if (logWriter == null)
                {
                    fs =
                        new FileStream(GTPath.GetLocalApplicationDataPath() +
                           Path.DirectorySeparatorChar + "GTErrorLog.txt",
                            FileMode.Append);
                    logWriter = new StreamWriter(fs);
                }

                logWriter.WriteLine(line);
                logWriter.Flush();
                Console.WriteLine(line);
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("ErrorLogger.cs, exception in WriteLine(string). Message: " + ex.Message);
            }
        }

        /// <summary>
        /// This method writes the MethodName and Excpetion message into the ErrorLog.txt file.
        /// </summary>
        /// <param name="ex">The <see cref="Exception"/> to log.</param>
        /// <param name="showMessageBox">True, if this exception should also be displayed in a message box.</param>
        public static void ProcessException(Exception ex, bool showMessageBox)
        {
            log.Warn("Error in: " + ex.TargetSite + "; Message: " + ex.Message);

            WriteLine("Error in: " + ex.TargetSite);
            WriteLine("Message: " + ex.Message);

            if (showMessageBox)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// This method raises the TrackerError event with the given message,
        /// so that subscribers can display the appropriate Error Window.
        /// </summary>
        /// <param name="message">A <see cref="String"/> with the message to display.</param>
        public static void RaiseGazeTrackerMessage(string message)
        {
            OnTrackerError(message);
            Console.WriteLine(message);
        }

        #endregion //PUBLICMETHODS

        #region OVERRIDES

        #endregion //OVERRIDES

        #region EVENTHANDLER

        /// <summary>
        /// Raises the <see cref="TrackerError"/> event with the given message.
        /// </summary>
        /// <param name="message">A <see cref="String"/> with the message to display.</param>
        private static void OnTrackerError(string message)
        {
            if (TrackerError != null)
            {
                TrackerError(message);
            }
        }

        #endregion //EVENTHANDLER

        #region THREAD

        #endregion //THREAD

        #region PRIVATEMETHODS

        #endregion //PRIVATEMETHODS

        #region HELPER

        #endregion //HELPER

        ///////////////////////////////////////////////////////////////////////////////
        // Defining Variables, Enumerations, Events                                  //
        ///////////////////////////////////////////////////////////////////////////////

        ///////////////////////////////////////////////////////////////////////////////
        // Construction and Initializing methods                                     //
        ///////////////////////////////////////////////////////////////////////////////

        ///////////////////////////////////////////////////////////////////////////////
        // Defining events, enums, delegates                                         //
        ///////////////////////////////////////////////////////////////////////////////

        ///////////////////////////////////////////////////////////////////////////////
        // Defining Properties                                                       //
        ///////////////////////////////////////////////////////////////////////////////

        ///////////////////////////////////////////////////////////////////////////////
        // Public methods                                                            //
        ///////////////////////////////////////////////////////////////////////////////

        ///////////////////////////////////////////////////////////////////////////////
        // Inherited methods                                                         //
        ///////////////////////////////////////////////////////////////////////////////

        ///////////////////////////////////////////////////////////////////////////////
        // Eventhandler                                                              //
        ///////////////////////////////////////////////////////////////////////////////

        ///////////////////////////////////////////////////////////////////////////////
        // Methods and Eventhandling for Background tasks                            //
        ///////////////////////////////////////////////////////////////////////////////

        ///////////////////////////////////////////////////////////////////////////////
        // Methods for doing main class job                                          //
        ///////////////////////////////////////////////////////////////////////////////

        ///////////////////////////////////////////////////////////////////////////////
        // Small helping Methods                                                     //
        ///////////////////////////////////////////////////////////////////////////////
    }
}