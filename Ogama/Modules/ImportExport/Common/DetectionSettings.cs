﻿// <copyright file="DetectionSettings.cs" company="FU Berlin">
// ******************************************************
// OGAMA - open gaze and mouse analyzer 
// Copyright (C) 2010 Adrian Voßkühler  
// ------------------------------------------------------------------------
// This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
// This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
// You should have received a copy of the GNU General Public License along with this program; if not, write to the Free Software Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
// **************************************************************
// </copyright>
// <author>Adrian Voßkühler</author>
// <email>adrian.vosskuehler@fu-berlin.de</email>

namespace Ogama.Modules.ImportExport.Common
{
  using System;
  using System.Collections.Generic;

  using Ogama.Modules.Common.Types;

  /// <summary>
  /// This class owns a list of settings that will be used 
  /// during import of data with trials and stimulu bitmaps.
  /// It is used for the Fixation and RawData Import.
  /// </summary>
  [Serializable]
  public class DetectionSettings
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
    /// Flag. True, if this class has been read from a file.
    /// </summary>
    private bool savedSettings;

    /// <summary>
    /// File name string of stimuli assignments between TrialIDs and Stimuli image files.
    /// </summary>
    private string stimuliImportFile;

    /// <summary>
    /// File name string of trial assignments between Starttimes and TrialIDs.
    /// </summary>
    private string trialImportFile;

    /// <summary>
    /// Flag. True, if importer should use iViewX MSG quoted lines to
    /// detect image files.
    /// </summary>
    private StimuliImportModes stimuliImportMode;

    /// <summary>
    /// Filename extension for image file names in import file to search for.
    /// </summary>
    private string stimuliFileExtension;

    /// <summary>
    /// Flag. True, if importer should use message lines
    /// with specified trigger string to distinguish between trials.
    /// </summary>
    private TrialSequenceImportModes trialImportMode;

    /// <summary>
    /// string that has to be in a import file line to notify a new trial.
    /// </summary>
    private string trialTriggerString;

    /// <summary>
    /// Factor for time column scaling to millisecons.
    /// </summary>
    /// <remarks>If import time column is in milliseconds, the factor is 1.
    /// If import time column is in microseconds, the factor is 0.001.</remarks>
    private double timeFactor;

    /// <summary>
    /// Subject name asked for and used, when no column for subject names is assigned.
    /// </summary>
    private string subjectName;

    /// <summary>
    /// TrialID - Imagefilename assignments.
    /// </summary>
    private XMLSerializableDictionary<int, string> trialIDToImageAssignments;

    /// <summary>
    /// Trial Sequence - Trial ID assignments.
    /// </summary>
    private XMLSerializableDictionary<int, int> trialSequenceToTrialIDAssignments;

    /// <summary>
    /// Trial Sequence - Starttime assignments.
    /// </summary>
    private XMLSerializableSortedList<int, long> trialSequenceToStarttimeAssignments;

    /// <summary>
    /// Imagename to trialID dictionary.
    /// </summary>
    private XMLSerializableDictionary<int, string> imageDictionary;

    /// <summary>
    /// The type of data the import file contains, can be fixations or rawdata.
    /// </summary>
    private ImportTypes importType;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the DetectionSettings class.
    /// </summary>
    public DetectionSettings()
    {
      this.timeFactor = 1D;
      this.stimuliImportMode = StimuliImportModes.UseiViewXMSG;
      this.trialImportMode = TrialSequenceImportModes.UseMSGLines;
      this.trialTriggerString = "msg";
      this.stimuliImportFile = string.Empty;
      this.stimuliFileExtension = "bmp";
      this.subjectName = string.Empty;
      this.trialImportFile = string.Empty;
      this.trialSequenceToStarttimeAssignments = new XMLSerializableSortedList<int, long>();
      this.trialIDToImageAssignments = new XMLSerializableDictionary<int, string>();
      this.trialSequenceToTrialIDAssignments = new XMLSerializableDictionary<int, int>();
      this.imageDictionary = new XMLSerializableDictionary<int, string>();
      this.savedSettings = false;
      this.importType = ImportTypes.Rawdata;
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets or sets a value indicating whether 
    /// this class has been read from a file.
    /// </summary>
    /// <value>A <see cref="Boolean"/> indicating whether this settings
    /// are loaded from a file.</value>
    public bool SavedSettings
    {
      get { return this.savedSettings; }
      set { this.savedSettings = value; }
    }

    /// <summary>
    /// Gets or sets the file for the stimuli assignments between 
    /// TrialIDs and Stimuli image files.
    /// </summary>
    /// <value>A <see cref="string"/> with the file of the 
    /// trialid-stimuli file assignments.</value>
    public string StimuliImportFile
    {
      get { return this.stimuliImportFile; }
      set { this.stimuliImportFile = value; }
    }

    /// <summary>
    /// Gets or sets the file for the stimuli assignments between 
    /// TrialIDs and StartTime image files.
    /// </summary>
    /// <value>A <see cref="string"/> with the file of the 
    /// trialid-stimuli file assignments.</value>
    public string TrialImportFile
    {
      get { return this.trialImportFile; }
      set { this.trialImportFile = value; }
    }

    /// <summary>
    /// Gets or sets the import mode for the stimuli.
    /// </summary>
    /// <value>A <see cref="StimuliImportModes"/> that indicates
    /// the way to catch the stimuli.</value>
    public StimuliImportModes StimuliImportMode
    {
      get { return this.stimuliImportMode; }
      set { this.stimuliImportMode = value; }
    }

    /// <summary>
    /// Gets or sets the filename extension for 
    /// image file names in import file to search for.
    /// </summary>
    /// <value>A <see cref="string"/> with the extension without dot to
    /// search for in the log files.</value>
    public string StimuliFileExtension
    {
      get { return this.stimuliFileExtension; }
      set { this.stimuliFileExtension = value; }
    }

    /// <summary>
    /// Gets or sets the import mode for the trials.
    /// </summary>
    /// <value>A <see cref="TrialSequenceImportModes"/> that indicates
    /// the way to catch the trials.</value>
    public TrialSequenceImportModes TrialImportMode
    {
      get { return this.trialImportMode; }
      set { this.trialImportMode = value; }
    }

    /// <summary>
    /// Gets or sets a string that has to be in a 
    /// import file line to notify a new trial.
    /// </summary>
    /// <value>A <see cref="string"/> indicating the begin of a new trial
    /// in the log file.</value>
    public string TrialTriggerString
    {
      get { return this.trialTriggerString; }
      set { this.trialTriggerString = value; }
    }

    /// <summary>
    /// Gets or sets the factor for time column scaling to milliseconds.
    /// </summary>
    /// <remarks>If import time column is in milliseconds, the factor is 1.
    /// If import time column is in microseconds, the factor is 0.001.</remarks>
    /// <value>A <see cref="Single"/> with the time conversion factor
    /// to milliseconds.</value>
    public double TimeFactor
    {
      get { return this.timeFactor; }
      set { this.timeFactor = value; }
    }

    /// <summary>
    /// Gets or sets a subject name asked for and used, 
    /// when no column for subject names is assigned.
    /// </summary>
    /// <value>A <see cref="string"/> with the subject name to use,
    /// if no import column is specified.</value>
    public string SubjectName
    {
      get { return this.subjectName; }
      set { this.subjectName = value; }
    }

    /// <summary>
    /// Gets or sets a <see cref="IDictionary{Int32,String}"/>
    /// with the TrialID to Imagefilename assignments, that is used during
    /// importing the trialid to imagefile assignments.
    /// </summary>
    /// <value>A <see cref="XMLSerializableDictionary{Int32, String}"/>
    /// in which each trial number is assigned a stimulus image file.</value>
    public XMLSerializableDictionary<int, string> TrialIDToImageAssignments
    {
      get { return this.trialIDToImageAssignments; }
      set { this.trialIDToImageAssignments = value; }
    }

    /// <summary>
    /// Gets or sets a <see cref="SortedList{Int32,Int64}"/>
    /// with the TrialID to StartingTime assignments.
    /// </summary>
    /// <value>A <see cref="XMLSerializableSortedList{Int32, Int64}"/>
    /// in which each trial number is assigned a trial starting time.</value>
    public XMLSerializableSortedList<int, long> TrialSequenceToStarttimeAssignments
    {
      get { return this.trialSequenceToStarttimeAssignments; }
      set { this.trialSequenceToStarttimeAssignments = value; }
    }

    /// <summary>
    /// Gets or sets a <see cref="SortedList{Int32,Int32}"/>
    /// with the Trial Sequence to TrialID assignments.
    /// </summary>
    /// <value>A <see cref="XMLSerializableSortedList{Int32, Int32}"/>
    /// in which each trial sequence number is assigned a trial id.</value>
    public XMLSerializableDictionary<int, int> TrialSequenceToTrialIDAssignments
    {
      get { return this.trialSequenceToTrialIDAssignments; }
      set { this.trialSequenceToTrialIDAssignments = value; }
    }

    /// <summary>
    /// Gets or sets a <see cref="IDictionary{Int32,String}"/>
    /// with the Trial Sequence to Imagefilename assignments that is used during
    /// automated generation of stimulus trialid assignments.
    /// </summary>
    /// <value>A <see cref="XMLSerializableDictionary{Int32, String}"/>
    /// in which each trial number is assigned a stimulus image file.</value>
    public XMLSerializableDictionary<int, string> ImageDictionary
    {
      get { return this.imageDictionary; }
      set { this.imageDictionary = value; }
    }

    /// <summary>
    /// Gets or sets the type of data the import file contains,
    /// can be fixations or rawdata.
    /// </summary>
    /// <value>A <see cref="ImportTypes"/> indicating the content of the log file.</value>
    public ImportTypes ImportType
    {
      get { return this.importType; }
      set { this.importType = value; }
    }

    #endregion //PROPERTIES

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
    #endregion //METHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER
    #endregion //HELPER
  }
}
