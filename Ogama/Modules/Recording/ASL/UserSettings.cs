// <copyright file="UserSettings.cs" company="Applied Science Laboratories">
// ******************************************************
// Serial out port viewer
// Copyright (C) 2006 Applied Science Group  
// **************************************************************
// </copyright>
// <author>
//  Applied Science Laboratories
//  Modifications : Smaïl KHAMED
//  </author>
// <email>virginie.feraud@univ-tlse2.fr</email>

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using ASL_Utilities;
using System.Windows.Forms;

    /// <summary>
    /// Class to save and load settings for the eye tracking system.
    /// </summary> 
    public class UserSettings
    {
        ///////////////////////////////////////////////////////////////////////////////
        // Defining Constants                                                        //
        ///////////////////////////////////////////////////////////////////////////////
        #region CONSTANTS
        
        private const string APPLICATION_PATH = "SerialOutViewer";
        
        #endregion //CONSTANTS

        ///////////////////////////////////////////////////////////////////////////////
        // Defining Enumerations                                                     //
        ///////////////////////////////////////////////////////////////////////////////
        #region ENUMS
        #endregion // ENUMS

        ///////////////////////////////////////////////////////////////////////////////
        // Defining Variables, Enumerations, Events                                  //
        ///////////////////////////////////////////////////////////////////////////////
        #region FIELDS

        /// <summary>
        /// Settings file name
        /// </summary>
        private string settingsFile;

        /// <summary>
        /// COM port number
        /// </summary>
        public int comPortNo = 1;

        /// <summary>
        /// Indicates that Eye Tracker is in Streaming-mode
        /// </summary>
        public bool streaming = true;

        /// <summary>
        /// Indicates that Eye Tracker is in EyeHead integration mode 
        /// </summary>
        public bool eyeHead = false;

        /// <summary>
        /// Configuration file name
        /// </summary>
        public string configFile = null;

        /// <summary>
        /// Default configuration file name
        /// </summary>
        public string defaultConfigFile = null;

        /// <summary>
        /// Indicates if read data must been write into log-file 
        /// </summary>
        public bool writeLogFile = false;

        /// <summary>
        /// Log-file name
        /// </summary>
        public string logFile = null;

        /// <summary>
        /// Indicates if warning must be display when using default configuration file
        /// </summary>
        public bool displayWarning = true;

        /// <summary>
        /// Baud-rate
        /// </summary>
        public int baudRate;

        /// <summary>
        /// Eye-tracker update rate
        /// </summary>
        public int updateRate;

        #endregion //FIELDS
        
        #region CONSTRUCTION

        /// <summary>
        /// Initializes a new instance of the UserSettings class with Default parameters.
        /// </summary>
        public UserSettings()
        {
            this.defaultConfigFile = Ogama.Properties.Settings.Default.EyeTrackerSettingsPath + "ASL5000Settings.cfg"; 
            this.settingsFile = Ogama.Properties.Settings.Default.EyeTrackerSettingsPath + "ASLUserSettings.cfg";

            string logPath = Path.Combine(
                    AslUtil.GetCommonDataFolder(),
                    APPLICATION_PATH);
            Directory.CreateDirectory(logPath);
            this.logFile = Path.Combine(logPath, "log.csv");
        }

        #endregion //CONSTRUCTION

        #region METHODS
        /// <summary>
        /// Unserialize an UserSettings object
        /// </summary>
        /// <returns>UserSettings object</returns>
        public UserSettings Load()
        {
            this.settingsFile = Ogama.Properties.Settings.Default.EyeTrackerSettingsPath + "ASLUserSettings.cfg";
            string errorMsg;
            UserSettings result = (UserSettings)AslUtil.LoadXml(typeof(UserSettings), settingsFile, true/*create new*/, out errorMsg);
            if (result == null)
            { 
                MessageBox.Show(errorMsg, "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                result = new UserSettings();
            }

            return result;
        }

        /// <summary>
        /// Unserialize an UserSettings object
        /// </summary>
        /// <param name="fileName">Target file name </param>
        /// <returns>UserSettings object</returns>
        public static UserSettings Load(string fileName)
        {
            string errorMsg;
            UserSettings result = (UserSettings)AslUtil.LoadXml(typeof(UserSettings), fileName, true/*create new*/, out errorMsg);
            if (result == null)
            {
                MessageBox.Show(errorMsg, "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                result = new UserSettings();
            }

            return result;
        }

        /// <summary>
        /// Serialize an UserSettings object
        /// </summary>
        public void Store()
        {
            AslUtil.StoreXml(this, settingsFile);
        }

        /// <summary>
        /// Serialize an UserSettings object
        /// </summary>
        /// <param name="fileName">Target file name</param>
        public void Store(string fileName)
        {
            AslUtil.StoreXml(this, fileName);
        }
        #endregion //METHODS

    }//UserSettings Class
