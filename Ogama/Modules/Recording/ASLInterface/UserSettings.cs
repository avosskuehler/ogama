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
namespace Ogama.Modules.Recording.ASLInterface
{
  using System.IO;
  using System.Windows.Forms;

#if ASL
  using ASL_Utilities;
#endif

  /// <summary>
  /// Class to save and load settings for the eye tracking system.
  /// </summary> 
  public class UserSettings
  {
    ///////////////////////////////////////////////////////////////////////////////
    // Defining Constants                                                        //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTANTS

    /// <summary>
    /// The application path for the serial out viewer.
    /// </summary>
    private const string Applicationpath = "SerialOutViewer";

    #endregion //CONSTANTS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Variables, Enumerations, Events                                  //
    ///////////////////////////////////////////////////////////////////////////////
    #region FIELDS

    /// <summary>
    /// Settings file name
    /// </summary>
    private string settingsFile;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the UserSettings class with Default parameters.
    /// </summary>
    public UserSettings()
    {
      this.ComPortNo = 1;
      this.Streaming = true;
      this.EyeHead = false;
      this.ConfigFile = null;
      this.WriteLogFile = false;

      this.DefaultConfigFile = Properties.Settings.Default.EyeTrackerSettingsPath + "ASLStandardStreaming.cfg";
      this.settingsFile = Properties.Settings.Default.EyeTrackerSettingsPath + "ASLUserSettings.cfg";

 #if ASL
     var logPath = Path.Combine(
        AslUtil.GetCommonDataFolder(),
        Applicationpath);
      Directory.CreateDirectory(logPath);
      this.LogFile = Path.Combine(logPath, "log.csv");
#endif
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining events, enums, delegates                                         //
    ///////////////////////////////////////////////////////////////////////////////
    #region EVENTS
    #endregion EVENTS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets or sets COM port number
    /// </summary>
    public int ComPortNo { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether that Eye Tracker is in Streaming-mode
    /// </summary>
    public bool Streaming { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether that Eye Tracker is in EyeHead integration mode 
    /// </summary>
    public bool EyeHead { get; set; }

    /// <summary>
    /// Gets or sets the Configuration file name
    /// </summary>
    public string ConfigFile { get; set; }

    /// <summary>
    /// Gets or sets the Default configuration file name
    /// </summary>
    public string DefaultConfigFile { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether read data must been written into log-file 
    /// </summary>
    public bool WriteLogFile { get; set; }

    /// <summary>
    /// Gets or sets the Log-file name
    /// </summary>
    public string LogFile { get; set; }

    /// <summary>
    /// Gets or sets the Baud-rate for the connection
    /// </summary>
    public int BaudRate { get; set; }

    /// <summary>
    /// Gets or sets the Eye-tracker update rate
    /// </summary>
    public int UpdateRate { get; set; }

    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Public methods                                                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region PUBLICMETHODS

#if ASL
    /// <summary>
    /// Unserialize an UserSettings object
    /// </summary>
    /// <param name="fileName">Target file name </param>
    /// <returns>UserSettings object</returns>
    public static UserSettings Load(string fileName)
    {
      string errorMsg;
      var result = (UserSettings)AslUtil.LoadXml(typeof(UserSettings), fileName, true/*create new*/, out errorMsg);
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
    /// <returns>UserSettings object</returns>
    public UserSettings Load()
    {
      this.settingsFile = Ogama.Properties.Settings.Default.EyeTrackerSettingsPath + "ASLUserSettings.cfg";
      string errorMsg;
      var result = (UserSettings)AslUtil.LoadXml(typeof(UserSettings), this.settingsFile, true/*create new*/, out errorMsg);
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
      AslUtil.StoreXml(this, this.settingsFile);
    }

    /// <summary>
    /// Serialize an UserSettings object
    /// </summary>
    /// <param name="fileName">Target file name</param>
    public void Store(string fileName)
    {
      AslUtil.StoreXml(this, fileName);
    }
#endif

    #endregion //PUBLICMETHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Inherited methods                                                         //
    ///////////////////////////////////////////////////////////////////////////////
    #region OVERRIDES
    #endregion //OVERRIDES

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler                                                              //
    ///////////////////////////////////////////////////////////////////////////////
    #region EVENTHANDLER
    #endregion //EVENTHANDLER

    ///////////////////////////////////////////////////////////////////////////////
    // Methods and Eventhandling for Background tasks                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region THREAD
    #endregion //THREAD

    ///////////////////////////////////////////////////////////////////////////////
    // Methods for doing main class job                                          //
    ///////////////////////////////////////////////////////////////////////////////
    #region PRIVATEMETHODS
    #endregion //PRIVATEMETHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER
    #endregion //HELPER
  }
} // UserSettings Class