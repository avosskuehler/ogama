// <copyright file="ExperimentSettings.cs" company="FU Berlin">
// ******************************************************
// OGAMA - open gaze and mouse analyzer 
// Copyright (C) 2013 Dr. Adrian Voßkühler  
// ------------------------------------------------------------------------
// This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
// This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
// You should have received a copy of the GNU General Public License along with this program; if not, write to the Free Software Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
// **************************************************************
// </copyright>
// <author>Adrian Voßkühler</author>
// <email>adrian@ogama.net</email>

namespace Ogama.Properties
{
  using System;
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.Drawing;
  using System.IO;
  using System.Reflection;
  using System.Windows.Forms;
  using System.Xml.Serialization;

  using Ogama.ExceptionHandling;
  using Ogama.Modules.Common;
  using Ogama.Modules.Common.SlideCollections;
  using Ogama.Modules.Scanpath.Colorization;

  using VectorGraphics;

  /// <summary>
  /// Class to store experiment specific settings like database file,
  /// connection string and images path.
  /// </summary>
  [Serializable]
  public class ExperimentSettings
  {
    ///////////////////////////////////////////////////////////////////////////////
    // Defining Constants                                                        //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTANTS
    #endregion //CONSTANTS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Variables, Enumerations, Events                                  //
    ///////////////////////////////////////////////////////////////////////////////
    #region FIELDS

    /// <summary>
    /// List of slides with the current slide show for the experiment, if
    /// there is one specified.
    /// </summary>
    private Slideshow slideShow;

    /// <summary>
    /// Gaze raw data sampling rate.
    /// </summary>
    private int gazeSamplingRate;

    /// <summary>
    /// Maximum distance in pixels 
    /// that a gaze point may vary from the average gaze fixation point 
    /// and still be considered part of the fixation.
    /// </summary>
    private int gazeMaxDistance;

    /// <summary>Minimum number of gaze samples 
    /// that can be considered a fixation.
    /// </summary>
    private int gazeMinSamples;

    /// <summary>
    /// Diameter ratio of gaze fixations. 
    /// </summary>
    private float gazeDiameterDiv;

    /// <summary>
    /// Mouse raw data sampling rate.
    /// </summary>
    private int mouseSamplingRate;

    /// <summary>
    /// Maximum distance in pixels 
    /// that a mouse point may vary from the average mouse fixation point 
    /// and still be considered part of the fixation.
    /// </summary>
    private int mouseMaxDistance;

    /// <summary>
    /// Minimum number of mouse samples 
    /// that can be considered a fixation.
    /// </summary>
    private int mouseMinSamples;

    /// <summary>
    /// Diameter ratio of mouse fixations. 
    /// </summary>
    private float mouseDiameterDiv;

    /// <summary>
    /// Length of the delay line for the fixation detection.
    /// </summary>
    private int fixationRingSize;

    /// <summary>
    /// Indicates whether to merge consecutive fixations within max distance
    /// into one fixation.
    /// </summary>
    private bool mergeConsecutiveFixations;

    /// <summary>
    /// Indicates whether to eliminate fixations shorter than limitForFirstFixation
    /// from the fixation list.
    /// </summary>
    private bool eliminateFirstFixation;

    /// <summary>
    /// A value in milliseconds indicating the length up to which
    /// the first fixation is eliminated when the flag <see cref="eliminateFirstFixation"/>
    /// is set
    /// </summary>
    private int limitForFirstFixation;

    /// <summary>
    /// Indicates whether to simply eliminate first fixation without any additional 
    /// conditions.
    /// </summary>
    private bool eliminateFirstFixationSimple;

    /// <summary>
    /// Width of Stimulus Presentation Monitor.
    /// </summary>
    private int widthStimulusScreen;

    /// <summary>
    /// Height of Stimulus Presentation Monitor.
    /// </summary>
    private int heightStimulusScreen;

    /// <summary>
    /// Full file path for this settings file
    /// </summary>
    private string documentPath;

    /// <summary>
    /// Name of this experiment
    /// </summary>
    private string experimentName;

    /// <summary>
    /// Name of the SQLServer instance
    /// </summary>
    private string sqlInstanceName;

    /// <summary>
    /// Saves a custom non-standard connection string which
    /// can be defined via the tools->database connection dialog.
    /// </summary>
    private string customConnectionString;

    /// <summary>
    /// Saves the gaze colorization parameters of the scanpath interface.
    /// </summary>
    private ColorizationParameters gazeColorParams;

    /// <summary>
    /// Saves the mouse colorization parameters of the scanpath interface.
    /// </summary>
    private ColorizationParameters mouseColorParams;

    /// <summary>
    /// Saves the OGAMA version that this experiment is currently working with.
    /// </summary>
    private Version ogamaVersion;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the ExperimentSettings class.
    /// </summary>
    public ExperimentSettings()
    {
      this.widthStimulusScreen = 1024;
      this.heightStimulusScreen = 768;
      this.documentPath = string.Empty;
      this.experimentName = string.Empty;
      this.sqlInstanceName = "SQLEXPRESS";
      this.gazeSamplingRate = 60;
      this.mouseSamplingRate = 10;
      this.gazeMaxDistance = 20;
      this.mouseMaxDistance = 20;
      this.gazeMinSamples = 5;
      this.mouseMinSamples = 10;
      this.gazeDiameterDiv = 2;
      this.mouseDiameterDiv = 5;
      this.fixationRingSize = 31;
      this.mergeConsecutiveFixations = true;
      this.eliminateFirstFixation = false;
      this.limitForFirstFixation = 300;
      this.eliminateFirstFixationSimple = false;
      this.slideShow = new Slideshow();
      this.customConnectionString = string.Empty;
      this.gazeColorParams = new ColorizationParameters();
      this.mouseColorParams = new ColorizationParameters();
      this.ogamaVersion = new Version(); // Assembly.GetExecutingAssembly().GetName().Version;
      this.ScreenCaptureFramerate = 10;
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets or sets the OGAMA version that this experiment is currently working with.
    /// </summary>
    [XmlIgnore]
    public Version OgamaVersion
    {
      get
      {
        return this.ogamaVersion;
      }

      set
      {
        if (value != this.ogamaVersion)
        {
          this.ogamaVersion = value;
        }
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to merge consecutive fixations within max distance
    /// into one fixation.
    /// </summary>
    public bool MergeConsecutiveFixations
    {
      get { return this.mergeConsecutiveFixations; }
      set { this.mergeConsecutiveFixations = value; }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to eliminate fixations shorter than <see cref="LimitForFirstFixation"/>
    /// from the fixation list.
    /// </summary>
    public bool EliminateFirstFixation
    {
      get { return this.eliminateFirstFixation; }
      set { this.eliminateFirstFixation = value; }
    }

    /// <summary>
    /// Gets or sets a value in milliseconds indicating the length up to which
    /// the first fixation is eliminated when the flag <see cref="EliminateFirstFixation"/>
    /// is set.
    /// </summary>
    public int LimitForFirstFixation
    {
      get { return this.limitForFirstFixation; }
      set { this.limitForFirstFixation = value; }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to simply eliminate fixations without any
    /// additional conditions.
    /// </summary>
    public bool EliminateFirstFixationSimple
    {
        get { return this.eliminateFirstFixationSimple; }
        set { this.eliminateFirstFixationSimple = value; }
    }

    /// <summary>
    /// Gets or sets the SerializableVersion number which is
    /// a string representing the ogama version of this experiment.
    /// Serializes or deserializes the <see cref="OgamaVersion"/> property.
    /// </summary>
    /// <value>A <see cref="string"/> with the serializable representation 
    /// of the version number.</value>
    public string SerializableVersion
    {
      get { return this.ogamaVersion.ToString(4); }
      set { this.ogamaVersion = new Version(value); }
    }

    /// <summary>
    /// Gets or sets Gaze raw data sampling rate.
    /// </summary>
    /// <value>An <see cref="int"/> with the gaze samling rate.</value>
    public int GazeSamplingRate
    {
      get { return this.gazeSamplingRate; }
      set { this.gazeSamplingRate = value; }
    }

    /// <summary>
    /// Gets or sets maximum distance in pixels 
    /// that a gaze point may vary from the average gaze fixation point 
    /// and still be considered part of the fixation.
    /// </summary>
    /// <value>An <see cref="int"/> with the maximum distance for a gaze fixation.</value>
    public int GazeMaxDistance
    {
      get { return this.gazeMaxDistance; }
      set { this.gazeMaxDistance = value; }
    }

    /// <summary>
    /// Gets or sets minimum number of gaze samples 
    /// that can be considered a fixation.
    /// </summary>
    /// <value>An <see cref="int"/> with the minimum number of samples for a gaze fixation.</value>
    public int GazeMinSamples
    {
      get { return this.gazeMinSamples; }
      set { this.gazeMinSamples = value; }
    }

    /// <summary>
    /// Gets or sets diameter ratio of gaze fixations. 
    /// </summary>
    /// <value>A <see cref="Single"/> with the gaze diameter divisor.</value>
    public float GazeDiameterDiv
    {
      get { return this.gazeDiameterDiv; }
      set { this.gazeDiameterDiv = value; }
    }

    /// <summary>
    /// Gets or sets mouse raw data sampling rate.
    /// </summary>
    /// <value>An <see cref="int"/> with the mouse samling rate.</value>
    public int MouseSamplingRate
    {
      get { return this.mouseSamplingRate; }
      set { this.mouseSamplingRate = value; }
    }

    /// <summary>
    /// Gets or sets maximum distance in pixels 
    /// that a mouse point may vary from the average mouse fixation point 
    /// and still be considered part of the fixation.
    /// </summary>
    /// <value>An <see cref="int"/> with the maximum distance for a mouse fixation.</value>
    public int MouseMaxDistance
    {
      get { return this.mouseMaxDistance; }
      set { this.mouseMaxDistance = value; }
    }

    /// <summary>
    /// Gets or sets minimum number of mouse samples 
    /// that can be considered a fixation.
    /// </summary>
    /// <value>An <see cref="int"/> with the minimum number of samples for a gaze fixation.</value>
    public int MouseMinSamples
    {
      get { return this.mouseMinSamples; }
      set { this.mouseMinSamples = value; }
    }

    /// <summary>
    /// Gets or sets diameter ratio of mouse fixations. 
    /// </summary>
    /// <value>A <see cref="Single"/> with the gaze diameter divisor.</value>
    public float MouseDiameterDiv
    {
      get { return this.mouseDiameterDiv; }
      set { this.mouseDiameterDiv = value; }
    }

    /// <summary>
    /// Gets or sets the length of the delay line for the fixation detection.
    /// </summary>
    /// <value>An <see cref="int"/> with the array size for fixation detection.</value>
    public int FixationRingSize
    {
      get { return this.fixationRingSize; }
      set { this.fixationRingSize = value; }
    }

    /// <summary>
    /// Gets or sets width of Stimulus Presentation Monitor.
    /// </summary>
    /// <value>An <see cref="int"/> with the horizontal screen resolution of
    /// the presentation screen.</value>
    public int WidthStimulusScreen
    {
      get { return this.widthStimulusScreen; }
      set { this.widthStimulusScreen = value; }
    }

    /// <summary>
    /// Gets or sets height of Stimulus Presentation Monitor.
    /// </summary>
    /// <value>An <see cref="int"/> with the vertical screen resolution of
    /// the presentation screen.</value>
    public int HeightStimulusScreen
    {
      get { return this.heightStimulusScreen; }
      set { this.heightStimulusScreen = value; }
    }

    /// <summary>
    /// Gets or sets file path for this Document (XML) file
    /// </summary>
    /// <value>A <see cref="string"/> with the path to the experiment file.</value>
    [XmlIgnore]
    public string DocumentPath
    {
      get { return this.documentPath; }
      set { this.documentPath = value; }
    }

    /// <summary>
    /// Gets full file name with path for the Document.
    /// </summary>
    /// <value>A <see cref="string"/> with the experiment file.</value>
    public string DocumentFilename
    {
      get { return this.CreateDocumentFilename(); }
    }

    /// <summary>
    /// Gets full file path for the slide resources folder
    /// </summary>
    /// <value>A <see cref="string"/> with the path to the slide resources.</value>
    public string SlideResourcesPath
    {
      get { return this.CreateSlideResourcesPath(); }
    }

    /// <summary>
    /// Gets full file path for the thumbs folder
    /// </summary>
    /// <value>A <see cref="string"/> with the path to the image thumbs.</value>
    public string ThumbsPath
    {
      get { return this.CreateThumbsPath(); }
    }

    /// <summary>
    /// Gets full file path for the database folder
    /// </summary>
    /// <value>A <see cref="string"/> with the path to the database files.</value>
    public string DatabasePath
    {
      get { return this.CreateDatabasePath(); }
    }

    /// <summary>
    /// Gets full file path for the database mdf file.
    /// </summary>
    /// <value>A <see cref="string"/> with the database file.</value>
    public string DatabaseMDFFile
    {
      get { return Path.Combine(this.CreateDatabasePath(), this.experimentName + ".mdf"); }
    }

    /// <summary>
    /// Gets full file path for the database ldf LOG file.
    /// </summary>
    /// <value>A <see cref="string"/> with the database log file.</value>
    public string DatabaseLDFFile
    {
      get { return Path.Combine(this.CreateDatabasePath(), this.experimentName + "_log.ldf"); }
    }

    /// <summary>
    /// Gets or sets the experiments name.
    /// </summary>
    /// <value>A <see cref="string"/> with the experiment name.</value>
    public string Name
    {
      get { return this.experimentName; }
      set { this.experimentName = value; }
    }

    /// <summary>
    /// Gets the database connection string
    /// </summary>
    /// <value>A <see cref="string"/> with the connection string for the database.</value>
    public string DatabaseConnectionString
    {
      get { return this.CreateDatabaseConnectionString(); }
    }

    /// <summary>
    /// Gets the database connection string
    /// </summary>
    /// <value>A <see cref="string"/> with the connection string for the database.</value>
    public string ServerConnectionString
    {
      get { return this.CreateServerConnectionString(); }
    }

    /// <summary>
    /// Gets or sets the sql instance server name.
    /// </summary>
    public string SqlInstanceName
    {
      get { return this.sqlInstanceName; }
      set { this.sqlInstanceName = value; }
    }

    /// <summary>
    /// Gets or sets a custom non-standard connection string which
    /// can be defined via the tools->database connection dialog.
    /// </summary>
    public string CustomConnectionString
    {
      get { return this.customConnectionString; }
      set { this.customConnectionString = value; }
    }

    /// <summary>
    /// Gets or sets the experiments slide show.
    /// </summary>
    /// <value>A <see cref="List{Slide}"/> with the slides for this experiment.</value>
    public Slideshow SlideShow
    {
      get { return this.slideShow; }
      set { this.slideShow = value; }
    }

    /// <summary>
    /// Gets or sets the gaze colorization parameters of the scanpath interface.
    /// </summary>
    public ColorizationParameters GazeColorParams
    {
      get { return this.gazeColorParams; }
      set { this.gazeColorParams = value; }
    }

    /// <summary>
    /// Gets or sets the mouse colorization parameters of the scanpath interface.
    /// </summary>
    public ColorizationParameters MouseColorParams
    {
      get { return this.mouseColorParams; }
      set { this.mouseColorParams = value; }
    }

    /// <summary>
    /// Gets or sets the framerate in frames per second to 
    /// use in the screen capturing. Default is 10.
    /// </summary>
    /// <value>A <see cref="Int32"/> with the framerate in frames per second .</value>
    public int ScreenCaptureFramerate { get; set; }

    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Public methods                                                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region PUBLICMETHODS

    /// <summary>
    /// Serializes the experiment settings into the given file in a xml structure.
    /// </summary>
    /// <remarks>The xml file should have the .oga extension to achieve its affiliation to OGAMA</remarks>
    /// <param name="settings">The <see cref="ExperimentSettings"/> object to serialize.</param>
    /// <param name="filePath">Full file path to the .oga xml settings file.</param>
    /// <returns><strong>True</strong> if succesful.</returns>
    public static bool Serialize(ExperimentSettings settings, string filePath)
    {
      try
      {
        using (TextWriter writer = new StreamWriter(filePath))
        {
          // Create an instance of the XmlSerializer class;
          // specify the type of object to serialize 
          XmlSerializer serializer = new XmlSerializer(typeof(ExperimentSettings));

          // Serialize the ExperimentSettings, and close the TextWriter.
          serializer.Serialize(writer, settings);
        }
      }
      catch (Exception ex)
      {
        ExceptionMethods.HandleException(ex);
        return false;
      }

      return true;
    }

    /// <summary>
    /// Deserializes the experiment settings from the given xml file.
    /// </summary>
    /// <remarks>The xml file is named *.oga to achieve its affiliation to OGAMA</remarks>
    /// <param name="filePath">Full file path to the .oga xml settings file.</param>
    /// <returns>A <see cref="ExperimentSettings"/> object if succesful 
    /// or <strong>null</strong> if failed.</returns>
    public static ExperimentSettings Deserialize(string filePath)
    {
      try
      {
        ExperimentSettings settings;

        // A FileStream is needed to read the XML Document.
        using (FileStream fs = new FileStream(filePath, FileMode.Open))
        {
          // Create an instance of the XmlSerializer class;
          // specify the type of object to be deserialized 
          XmlSerializer serializer = new XmlSerializer(typeof(ExperimentSettings));

          ////* If the XML Document has been altered with unknown 
          ////nodes or attributes, handle them with the 
          ////UnknownNode and UnknownAttribute events.*/
          ////serializer.UnknownNode += new XmlNodeEventHandler(serializer_UnknownNode);
          ////serializer.UnknownAttribute += new XmlAttributeEventHandler(serializer_UnknownAttribute);

          /* Use the Deserialize method to restore the object's state with
          data from the XML Document. */
          settings = (ExperimentSettings)serializer.Deserialize(fs);
        }

        return settings;
      }
      catch (Exception ex)
      {
        if (ex.InnerException is WarningException)
        {
          // This is the case when the user cancels the upgrading
          throw new WarningException("Upgrading cancelled due to user request");
        }

        ExceptionMethods.HandleException(ex);
      }

      return null;
    }

    /// <summary>
    /// Use the standard connection string and delete a custom string.
    /// </summary>
    public void ResetConnectionStringToDefault()
    {
      this.customConnectionString = string.Empty;
    }

    /// <summary>
    /// This method writes the current assemply version
    /// into the <see cref="OgamaVersion"/> property.
    /// </summary>
    /// <returns><strong>True</strong> if version of opened file
    /// is less or equal to the ogama application version
    /// that is opening it, otherwise <strong>false</strong></returns>
    public bool UpdateVersion()
    {
      Version current = Assembly.GetExecutingAssembly().GetName().Version;
      if (this.OgamaVersion > current)
      {
        // Opening an experiment file made with a newer ogama version
        string message = "You are trying to open an experiment file that has "
          + "been created or saved with a never version of OGAMA: V" +
          this.OgamaVersion.ToString(4) + Environment.NewLine +
          "This could damage your experiment. " +
          "Please use the recent version of OGAMA by doing an update." +
          Environment.NewLine + "OGAMA will now close this experiment for " +
          "safety reasons." +
          Environment.NewLine + Environment.NewLine +
          "Hitting cancel or no will try to ignore this problem.";
        DialogResult result = InformationDialog.Show(
          "Opening experiment with an old version  of OGAMA.",
          message,
          true,
          MessageBoxIcon.Warning);
        switch (result)
        {
          case DialogResult.Cancel:
          case DialogResult.No:
            break;
          case DialogResult.Yes:
            return false;
        }
      }

      this.OgamaVersion = current;
      return true;
    }

    /// <summary>
    /// This method checks for moved experiment locations
    /// and updates the database path.
    /// </summary>
    /// <returns><strong>True</strong> if database connection is succesfully revised,
    /// <strong>false</strong> if user selected to cancel the connection revision.</returns>
    public bool CheckDatabasePath()
    {
      if (!this.DatabaseConnectionString.Contains(this.Name))
      {
        string message = "The database file to this experiment that is currently used," +
          "is not in the 'Database' subfolder of the experiment " +
          "or it is not correctly named in the form 'experimentname.mdf' ." +
          Environment.NewLine +
          "We strongly recommend not to move or rename the database files." +
          Environment.NewLine +
          "This can occur, when an experiment is duplicated via Copy and Paste. " +
          "and afterwards has beeing renamed." +
          Environment.NewLine +
          "Would you like to use the default database connection to the file:" +
          Environment.NewLine + this.DatabaseMDFFile + "?";
        DialogResult result = InformationDialog.Show(
          "Database has not default settings",
          message,
          true,
          MessageBoxIcon.Warning);

        switch (result)
        {
          case DialogResult.Cancel:
            return false;
          case DialogResult.No:
            return true;
          case DialogResult.Yes:
            this.ResetConnectionStringToDefault();
            return true;
        }
      }

      return true;
    }

    #endregion //PUBLICMETHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler                                                              //
    ///////////////////////////////////////////////////////////////////////////////
    #region EVENTS

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler for UI, Menu, Buttons, Toolbars etc.                         //
    ///////////////////////////////////////////////////////////////////////////////
    #region WINDOWSEVENTHANDLER
    #endregion //WINDOWSEVENTHANDLER

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler for Custom Defined Events                                    //
    ///////////////////////////////////////////////////////////////////////////////
    #region CUSTOMEVENTHANDLER
    #endregion //CUSTOMEVENTHANDLER

    #endregion //EVENTS

    ///////////////////////////////////////////////////////////////////////////////
    // Methods and Eventhandling for Background tasks                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region BACKGROUNDWORKER
    #endregion //BACKGROUNDWORKER

    ///////////////////////////////////////////////////////////////////////////////
    // Inherited methods                                                         //
    ///////////////////////////////////////////////////////////////////////////////
    #region OVERRIDES
    #endregion //OVERRIDES

    ///////////////////////////////////////////////////////////////////////////////
    // Methods for doing main class job                                          //
    ///////////////////////////////////////////////////////////////////////////////
    #region METHODS

    /// <summary>
    /// Builds connection string.
    /// </summary>
    /// <returns>A <see cref="string"/> with the database connection string.</returns>
    private string CreateDatabaseConnectionString()
    {
      string usedConnection = string.Empty;
      if (this.customConnectionString != string.Empty)
      {
        usedConnection = this.customConnectionString;
      }
      else
      {
        usedConnection = @"Data Source=.\" + this.sqlInstanceName + ";Initial Catalog=" +
          this.experimentName + ";Integrated Security=True;User Instance=True;Connection Timeout=30";
      }

      return usedConnection;
    }

    /// <summary>
    /// Generates connection string to server without an initial catalog
    /// </summary>
    /// <returns>A <see cref="string"/> with the server connection string.</returns>
    private string CreateServerConnectionString()
    {
      string usedConnection = @"Data Source=.\" + this.sqlInstanceName + ";Integrated Security=True;User Instance=True;Connection Timeout=30";

      return usedConnection;
    }

    /// <summary>
    /// Builds the Document filename with path.
    /// </summary>
    /// <returns>A <see cref="string"/> with the Document file name with path.</returns>
    private string CreateDocumentFilename()
    {
      string returnString = Path.Combine(this.documentPath, this.experimentName + ".oga");
      return returnString;
    }

    /// <summary>
    /// Builds the slides resources path from settingsfile.
    /// </summary>
    /// <returns>A <see cref="string"/> with the slides resources path.</returns>
    private string CreateSlideResourcesPath()
    {
      string returnString = Path.Combine(this.documentPath, "SlideResources");
      returnString += Path.DirectorySeparatorChar;
      return returnString;
    }

    /// <summary>
    /// Builds the thumbs path from settingsfile.
    /// </summary>
    /// <returns>A <see cref="string"/> with the thumbs path.</returns>
    private string CreateThumbsPath()
    {
      string returnString = Path.Combine(this.documentPath, "Thumbs");
      returnString += Path.DirectorySeparatorChar;
      return returnString;
    }

    /// <summary>
    /// Builds the database path from settingsfile.
    /// </summary>
    /// <returns>A <see cref="string"/> with the database path.</returns>
    private string CreateDatabasePath()
    {
      string returnString = Path.Combine(this.documentPath, "Database");
      returnString += Path.DirectorySeparatorChar;
      return returnString;
    }

    #endregion //METHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER
    #endregion //HELPER
  }
}
