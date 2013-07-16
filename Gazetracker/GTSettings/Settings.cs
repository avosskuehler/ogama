// <copyright file="GTSettings.cs" company="ITU">
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

using System;
using System.Collections;
using System.IO;
using System.Text;
using System.Xml;

namespace GTSettings
{
  using GTCommons;

  /// <summary>
  /// Singleton class for static settings.
  /// </summary>
  public sealed class Settings
  {
    #region CONSTANTS

    public const string Name = "GazeTrackerConfiguration";

    #endregion //CONSTANTS

    #region FIELDS

    /// <summary>
    /// The static instance of the singleton class.
    /// </summary>
    private static readonly Settings instance = new Settings();

    /// <summary>
    /// Holds all the settings used in the automatic tuning routines
    /// </summary>
    private readonly Autotune autotuneSettings;

    /// <summary>
    /// Holds the settings for the calibration.
    /// </summary>
    private readonly Calibration calibrationSettings;

    /// <summary>
    /// Holds the settings for the video input device
    /// </summary>
    private readonly Camera cameraSettings;

    /// <summary>
    /// Holds all the settings used in the eye movement classification and smoothing routines
    /// </summary>
    private readonly EyeMovement eyeMovementSettings;


    /// <summary>
    /// Holds all the settings used in the image processing.
    /// </summary>
    private readonly Eyestracker eyestrackerSettings;

    /// <summary>
    /// Holds all the settings used in the image processing.
    /// </summary>
    private readonly Eyetracker eyetrackerSettings;

    /// <summary>
    /// Holds the settings for settings and log files.
    /// </summary>
    private readonly FileSettings fileSettings;

    /// <summary>
    /// The <see cref="FileSystemWatcher"/> to watch the settings directory.
    /// </summary>
    private readonly FileSystemWatcher myWatcher;

    /// <summary>
    /// Holdsthe settings for the network connections and streams.
    /// </summary>
    private readonly Network networkSettings;

    /// <summary>
    /// Holdsthe settings for the network connections and streams.
    /// </summary>
    private readonly Cloud cloudSettings;

    /// <summary>
    /// Holds all the settings used in the image processing.
    /// </summary>
    private readonly Processing processingSettings;

    /// <summary>
    /// Holds all the settings used in the visualization routines
    /// </summary>
    private readonly Visualization visualizationSettings;

    /// <summary>
    /// The <see cref="XmlTextWriter"/> used to write the
    /// XMLsettings file.
    /// </summary>
    private XmlTextWriter xmlWriter;

    #endregion //FIELDS

    #region CONSTRUCTION

    /// <summary>
    /// Prevents a default instance of the GTSettings class from being created.
    /// </summary>
    private Settings()
    {
      fileSettings = new FileSettings();
      processingSettings = new Processing();
      eyestrackerSettings = new Eyestracker();
      eyetrackerSettings = new Eyetracker();
      cameraSettings = new Camera();
      calibrationSettings = new Calibration();
      networkSettings = new Network();
      visualizationSettings = new Visualization();
      autotuneSettings = new Autotune();
      eyeMovementSettings = new EyeMovement();
      cloudSettings = new Cloud();

      // Observe Config directory and update combobox when new files are written/changed/deleted..
      myWatcher = new FileSystemWatcher(GTPath.GetLocalApplicationDataPath() +
                     Path.DirectorySeparatorChar + "Settings",
                                        "*.xml");
      myWatcher.Path = fileSettings.SettingsDirectory;
      myWatcher.EnableRaisingEvents = true;
      myWatcher.IncludeSubdirectories = false;
      myWatcher.Created += WatcherSettingsDirUpdated;
      myWatcher.Changed += WatcherSettingsDirUpdated;
      myWatcher.Deleted += WatcherSettingsDirUpdated;
    }

    #endregion //CONSTRUCTION

    #region Get/Set properties

    /// <summary>
    /// Gets the singleton instance of the settings class
    /// that contains the Instance settings.
    /// </summary>
    public static Settings Instance
    {
      get { return instance; }
    }

    /// <summary>
    /// Gets all the settings files in the settings directory.
    /// </summary>
    public ArrayList SetupFiles
    {
      get
      {
        var itemsList = new ArrayList();
        itemsList.Add(" "); // First item blank

        if (fileSettings != null)
        {
          var di = new DirectoryInfo(fileSettings.SettingsDirectory);

          foreach (FileInfo file in di.GetFiles())
          {
            if (File.Exists(file.FullName))
              if (Path.GetExtension(file.FullName).Equals(".xml"))
                itemsList.Add(file.Name);
          }
        }

        return itemsList;
      }
    }

    /// <summary>
    /// Gets the settings used in the image processing.
    /// </summary>
    public Processing Processing
    {
      get { return processingSettings; }
    }

    /// <summary>
    /// Gets the settings used in the head tracking ROI routines
    /// </summary>
    public Eyestracker Eyestracker
    {
      get { return eyestrackerSettings; }
    }

    /// <summary>
    /// Gets the settings used in the eye tracking ROI routines
    /// </summary>
    public Eyetracker Eyetracker
    {
      get { return eyetrackerSettings; }
    }

    /// <summary>
    /// Gets the settings for the video input device
    /// </summary>
    public Camera Camera
    {
      get { return cameraSettings; }
    }

    /// <summary>
    /// Gets the settings for the calibration.
    /// </summary>
    public Calibration Calibration
    {
      get { return calibrationSettings; }
    }

    /// <summary>
    /// Gets the settings for the network connections and streams.
    /// </summary>
    public Network Network
    {
      get { return networkSettings; }
    }

    public Cloud CloudSettings
    {
      get { return cloudSettings; }
    }

    /// <summary>
    /// Gets the settings used in the visualization routines
    /// </summary>
    public Visualization Visualization
    {
      get { return visualizationSettings; }
    }

    /// <summary>
    /// Gets the settings used in the visualization routines
    /// </summary>
    public Autotune Autotune
    {
      get { return autotuneSettings; }
    }

    /// <summary>
    /// Gets the settings used in the fixation detection and smoothing routines
    /// </summary>
    public EyeMovement EyeMovement
    {
      get { return eyeMovementSettings; }
    }

    /// <summary>
    /// Gets the settings for settings and log files.
    /// </summary>
    public FileSettings FileSettings
    {
      get { return fileSettings; }
    }

    #endregion //PROPERTIES

    #region Public methods

    /// <summary>
    /// This static method writes a specific xml key value pair on
    /// the given xmlwriter.
    /// </summary>
    /// <param name="xmlWriter">The <see cref="XmlTextWriter"/> to use.</param>
    /// <param name="key">A <see cref="string"/> with the settings key.</param>
    /// <param name="value">A <see cref="string"/> with the settings value.</param>
    public static void WriteElement(XmlTextWriter xmlWriter, string key, string value)
    {
      xmlWriter.WriteStartElement(key);
      xmlWriter.WriteString(value);
      xmlWriter.WriteEndElement();
    }

    /// <summary>
    /// This method writes the Instance configuration in a
    /// xml settings file.
    /// </summary>
    public void WriteConfigFile()
    {
      if (fileSettings.SettingsName.Length < 1)
      {
        fileSettings.SettingsName = "Settings " + DateTime.Now.ToString("d");
      }

      try
      {
        var filename = fileSettings.SettingsDirectory + Path.DirectorySeparatorChar + fileSettings.SettingsName + ".xml";
        xmlWriter = new XmlTextWriter(filename, Encoding.UTF8) { Formatting = Formatting.Indented };
        xmlWriter.WriteStartDocument();
        xmlWriter.WriteStartElement(Name);

        fileSettings.WriteConfigFile(xmlWriter);
        processingSettings.WriteConfigFile(xmlWriter);
        eyestrackerSettings.WriteConfigFile(xmlWriter);
        eyetrackerSettings.WriteConfigFile(xmlWriter);
        calibrationSettings.WriteConfigFile(xmlWriter);
        cameraSettings.WriteConfigFile(xmlWriter);
        networkSettings.WriteConfigFile(xmlWriter);
        cloudSettings.WriteConfigFile(xmlWriter);
        visualizationSettings.WriteConfigFile(xmlWriter);
        autotuneSettings.WriteConfigFile(xmlWriter);
        eyeMovementSettings.WriteConfigFile(xmlWriter);

        xmlWriter.WriteEndElement(); // GazeTrackerConfiguration
        xmlWriter.WriteEndDocument();
        xmlWriter.Flush();
        xmlWriter.Close();
      }
      catch (Exception)
      {
        xmlWriter.Close();
        //ErrorLogger.ProcessException(ex, false);
      }
    }

    /// <summary>
    /// This method loads the configuration of the given file into the Instance
    /// settings instance.
    /// </summary>
    /// <param name="filename">A <see cref="string"/> with the filename without
    /// path of the settings file to read.</param>
    public void LoadConfigFile(string filename)
    {
      if (File.Exists(fileSettings.SettingsDirectory + Path.DirectorySeparatorChar + filename))
      {
        XmlReader xmlReader = new XmlTextReader(fileSettings.SettingsDirectory + Path.DirectorySeparatorChar + filename);
        if (xmlReader != null)
        {
          try
          {
            xmlReader.ReadToFollowing(FileSettings.Name);
            fileSettings.LoadConfigFile(xmlReader.ReadSubtree());

            xmlReader.ReadToFollowing(Processing.Name); // "ProcessingSettings");
            processingSettings.LoadConfigFile(xmlReader.ReadSubtree());

            xmlReader.ReadToFollowing(Eyestracker.Name);
            eyestrackerSettings.LoadConfigFile(xmlReader.ReadSubtree());

            xmlReader.ReadToFollowing(Eyetracker.Name);
            eyetrackerSettings.LoadConfigFile(xmlReader.ReadSubtree());

            xmlReader.ReadToFollowing(Calibration.Name);
            calibrationSettings.LoadConfigFile(xmlReader.ReadSubtree());

            xmlReader.ReadToFollowing(Camera.Name);
            cameraSettings.LoadConfigFile(xmlReader.ReadSubtree());

            xmlReader.ReadToFollowing(Network.Name);
            networkSettings.LoadConfigFile(xmlReader.ReadSubtree());

            xmlReader.ReadToFollowing(Cloud.Name);
            cloudSettings.LoadConfigFile(xmlReader.ReadSubtree());

            xmlReader.ReadToFollowing(Visualization.Name);
            visualizationSettings.LoadConfigFile(xmlReader.ReadSubtree());

            xmlReader.ReadToFollowing(Autotune.Name);
            autotuneSettings.LoadConfigFile(xmlReader.ReadSubtree());

            xmlReader.ReadToFollowing(EyeMovement.Name);
            eyeMovementSettings.LoadConfigFile(xmlReader.ReadSubtree());

            xmlReader.Close();
          }
          catch (Exception)
          {
            xmlReader.Close();
            // MessageBox.Show("Error loading settings: " + ex.Message);
            // ErrorLogger.ProcessException(ex, false);
          }
        }
      }
    }

    /// <summary>
    /// This method loads the latest saved configuration from
    /// disk into the Instance settings instance.
    /// </summary>
    /// <returns>True if succesfull, otherwise false.</returns>
    public bool LoadLatestConfiguration()
    {
      int numSettings = 0;

      try
      {
        string[] files = Directory.GetFiles(fileSettings.SettingsDirectory, "*.xml");
        for (int i = 0; i < files.Length; i++)
        {
          string lastWriteTime = File.GetLastWriteTime(files[i]).ToString("yyyyMMddHHmmss");
          files[i] = lastWriteTime + files[i];
          numSettings++;
        }

        Array.Sort(files);

        if (numSettings > 0)
        {
          string newest = Path.GetFileName(files[files.Length - 1].Substring(15));
          LoadConfigFile(newest);
          return true;
        }
        else
        {
          return false;
        }
      }
      catch (Exception)
      {
        // ErrorLogger.ProcessException(ex, false);
        return false;
      }
    }

    public string GetLatestConfigurationFile()
    {
      int numSettings = 0;

      try
      {
        string[] files = Directory.GetFiles(fileSettings.SettingsDirectory, "*.xml");
        for (int i = 0; i < files.Length; i++)
        {
          string lastWriteTime = File.GetLastWriteTime(files[i]).ToString("yyyyMMddHHmmss");
          files[i] = lastWriteTime + files[i];
          numSettings++;
        }

        Array.Sort(files);

        if (numSettings > 0)
        {
          string newest = Path.GetFileName(files[files.Length - 1].Substring(15));
          return newest;
        }
        else
          return "";
      }
      catch (Exception)
      {
        // ErrorLogger.ProcessException(ex, false);
        return "";
      }
    }

    #endregion //PUBLICMETHODS

    #region EventHandler

    /// <summary>
    /// The event handler for the Created, changed and deleted events of
    /// the <see cref="FileSystemWatcher"/>.
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">A <see cref="FileSystemEventArgs"/> with the event data.</param>
    private void WatcherSettingsDirUpdated(object sender, FileSystemEventArgs e)
    {
      // Get the file-list, will update bindings..
      ArrayList tmp = SetupFiles;
    }

    #endregion //EVENTHANDLER
  }
}