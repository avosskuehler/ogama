// <copyright file="ImportRawData.cs" company="FU Berlin">
// ******************************************************
// OGAMA - open gaze and mouse analyzer 
// Copyright (C) 2012 Adrian Voßkühler  
// ------------------------------------------------------------------------
// This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
// This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
// You should have received a copy of the GNU General Public License along with this program; if not, write to the Free Software Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
// **************************************************************
// </copyright>
// <author>Adrian Voßkühler</author>
// <email>adrian@ogama.net</email>

namespace Ogama.Modules.ImportExport.RawData
{
  using System;
  using System.Collections.Generic;
  using System.Data;
  using System.Drawing;
  using System.Globalization;
  using System.IO;
  using System.Windows.Forms;
  using System.Xml.Serialization;

  using Ogama.ExceptionHandling;
  using Ogama.MainWindow;
  using Ogama.Modules.Common.SlideCollections;
  using Ogama.Modules.Common.Tools;
  using Ogama.Modules.Common.Types;
  using Ogama.Modules.Fixations;
  using Ogama.Modules.ImportExport.Common;

  using VectorGraphics.Elements;
  using VectorGraphics.Elements.ElementCollections;
  using VectorGraphics.StopConditions;

  /// <summary>
  /// Class for importing raw data through multiple dialogs.
  /// </summary>
  public class ImportRawData
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
    /// List to fill with imported and filtered <see cref="RawData"/>
    /// </summary>
    private static List<RawData> rawDataList;

    /// <summary>
    /// List to fill with generated or imported <see cref="TrialsData"/>
    /// </summary>
    private static List<TrialsData> trialList;

    /// <summary>
    /// List to fill with generated <see cref="SubjectsData"/>
    /// </summary>
    private static List<SubjectsData> subjectList;

    /// <summary>
    /// Saves the specialized settings used during this import session.
    /// </summary>
    private static DetectionSettings detectionSetting;

    /// <summary>
    /// Saves the ASCII file import specialized settings
    /// during this import session.
    /// </summary>
    private static ASCIISettings asciiSetting;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////

    #region CONSTRUCTION

    /// <summary>
    /// Initializes static members of the ImportRawData class.
    /// </summary>
    static ImportRawData()
    {
      subjectList = new List<SubjectsData>();
      trialList = new List<TrialsData>();
      rawDataList = new List<RawData>();

      detectionSetting = new DetectionSettings();
      asciiSetting = new ASCIISettings();
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets the specialized settings used during this import session.
    /// </summary>
    /// <value>A <see cref="DetectionSettings"/>.</value>
    /// <seealso cref="DetectionSettings"/>
    public static DetectionSettings DetectionSetting
    {
      get { return detectionSetting; }
    }

    /// <summary>
    /// Gets the ASCII file import specialized settings
    /// during this import session.
    /// </summary>
    /// <value>A <see cref="ASCIISettings"/>.</value>
    /// <seealso cref="ASCIISettings"/>
    public static ASCIISettings ASCIISettings
    {
      get { return asciiSetting; }
    }

    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Public methods                                                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region PUBLICMETHODS

    /// <summary>
    /// Starts a multiple dialog routine (raw data import assistant)
    /// for reading raw data files into the programs database
    /// </summary>
    /// <param name="mainWindow">The <see cref="MainForm"/> to get access to the status label.</param>
    public static void Start(MainForm mainWindow)
    {
      try
      {
        asciiSetting = new ASCIISettings();
        detectionSetting = new DetectionSettings();

        ImportRawDataAssistentDialog objfrmImportAssistent = new ImportRawDataAssistentDialog();
        if (objfrmImportAssistent.ShowDialog() == DialogResult.OK)
        {
        OpenFile:
          if (asciiSetting.FileDialog.ShowDialog() == DialogResult.OK)
          {
            // Save filename
            string filename = asciiSetting.FileDialog.FileName;

            // Ask the user to use a settings file
            // and loads it into the importsettings, if true.
            AskforUsingSettingsFile();

            // Save import file
            asciiSetting.Filename = filename;

            // Set import mode
            detectionSetting.ImportType = ImportTypes.Rawdata;

            ImportParseFileDialog objfrmImportReadFile = new ImportParseFileDialog(ref asciiSetting);
          ReadFile:
            DialogResult resultRawData = objfrmImportReadFile.ShowDialog();
            if (resultRawData == DialogResult.OK)
            {
              ImportRawDataAssignColumnsDialog objfrmImportRawDataAssignColumns
                = new ImportRawDataAssignColumnsDialog();

            MakeAssignments:
              DialogResult resultAssign = objfrmImportRawDataAssignColumns.ShowDialog();
              if (resultAssign == DialogResult.OK)
              {
                ImportTrialsDialog objfrmImportTrials = new ImportTrialsDialog(
                  ref asciiSetting,
                  ref detectionSetting);

              CheckTrials:
                DialogResult resultTrials = objfrmImportTrials.ShowDialog();
                if (resultTrials == DialogResult.OK)
                {
                  ImportImagesDialog objfrmImportImages = new ImportImagesDialog(
                    ref asciiSetting,
                  ref detectionSetting);

                  DialogResult resultImages = objfrmImportImages.ShowDialog();
                  if (resultImages == DialogResult.OK)
                  {
                    if (MessageBox.Show(
                      "Would you like to save the import settings ?",
                      Application.ProductName,
                      MessageBoxButtons.YesNo,
                      MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                      SaveImportSettings(asciiSetting, detectionSetting);
                    }

                    // Show import splash window
                    asciiSetting.WaitingSplash.RunWorkerAsync();

                    // Give some time to show the splash ...
                    Application.DoEvents();

                    // Read log file again, but complete
                    GenerateOgamaRawDataList(-1);

                    // Generate the trials
                    GenerateOgamaSubjectAndTrialList();

                    // Save the import into ogamas database and the mdf file.
                    var succesful = SaveImportIntoTablesAndDB();

                    // Create slideshow trials
                    GenerateOgamaSlideshowTrials(detectionSetting, mainWindow);

                    // Calculate Fixations
                    CalculateFixations(mainWindow);

                    // Clear lists
                    subjectList.Clear();
                    trialList.Clear();
                    rawDataList.Clear();

                    // Import has finished.
                    asciiSetting.WaitingSplash.CancelAsync();

                    // Inform user about success.
                    if (succesful)
                    {
                      string message = "Import data succesfully written to database." + Environment.NewLine +
                        "Please don´t forget to move the stimuli images to the SlideResources subfolder" +
                        "of the experiment, otherwise no images will be shown.";
                      ExceptionMethods.ProcessMessage("Success", message);
                    }
                    else
                    {
                      string message = "Import had errors. Some or all of the import data " +
                        "could not be written the database.";
                      ExceptionMethods.ProcessErrorMessage(message);
                    }
                  }
                  else if (resultImages == DialogResult.Cancel)
                  {
                    goto CheckTrials;
                  }
                }
                else if (resultTrials == DialogResult.Cancel)
                {
                  goto MakeAssignments;
                }
              }
              else if (resultAssign == DialogResult.Cancel)
              {
                goto ReadFile;
              }
            }
            else if (resultRawData == DialogResult.Cancel)
            {
              goto OpenFile;
            }
          }
        }
      }
      catch (Exception ex)
      {
        string message = "Something failed during import." + Environment.NewLine
          + "Please try again with other settings. " + Environment.NewLine +
          "Error: " + ex.Message;
        ExceptionMethods.ProcessErrorMessage(message);
        if (asciiSetting.WaitingSplash.IsBusy)
        {
          asciiSetting.WaitingSplash.CancelAsync();
        }
      }
    }

    /// <summary>
    /// Generates the trial list for the current import settings
    /// up to the number of lines that are given.
    /// </summary>
    /// <param name="numberOfImportLines">An <see cref="int"/>
    /// with the max number of lines to import.
    /// Set it to -1 to use all lines.</param>
    /// <returns>A <see cref="List{TrialsData}"/> with the calculated trials.</returns>
    public static List<TrialsData> GetTrialList(int numberOfImportLines)
    {
      // Convert the import file into ogama column format
      GenerateOgamaRawDataList(numberOfImportLines);

      // Generate the trial list from the raw data with the current settings.
      GenerateOgamaSubjectAndTrialList();

      return trialList;
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
    /// This method calculates the fixations for the subjects
    /// that are currently imported.
    /// </summary>
    /// <param name="mainWindow">The <see cref="MainForm"/> to get access to the status label.</param>
    private static void CalculateFixations(MainForm mainWindow)
    {
      mainWindow.StatusLabel.Text = "Calculating Fixations ...";

      foreach (SubjectsData subject in subjectList)
      {
        // Get trial data of current subject
        DataTable trialsTable = Document.ActiveDocument.DocDataSet.TrialsAdapter.GetDataBySubject(subject.SubjectName);

        // Calculate fixations
        FixationCalculation calculationObject = new FixationCalculation();
        calculationObject.CalcFixations(SampleType.Gaze, subject.SubjectName, trialsTable, null, null);
        calculationObject.CalcFixations(SampleType.Mouse, subject.SubjectName, trialsTable, null, null);
      }

      mainWindow.StatusLabel.Text = "Fixation calculation done ...";
    }

    /// <summary>
    /// Generate the list of raw data under the current
    /// parsing conditions.
    /// </summary>
    /// <param name="numberOfImportLines">An <see cref="int"/>
    /// with the max number of lines to import.
    /// Set it to -1 to use all lines.</param>
    /// <remarks>This is the heart of the class. If something does not work as expected,
    /// first have a look here.</remarks>
    private static void GenerateOgamaRawDataList(int numberOfImportLines)
    {
      // Clear existing values
      rawDataList.Clear();
      double lastTimeInFileTime = 0;

      // Retrieve existing slideshow trials (to check matching filenames for 
      // correct trial ID numbering
      List<Trial> trials = Document.ActiveDocument.ExperimentSettings.SlideShow.Trials;
      List<string> trialNames = Document.ActiveDocument.ExperimentSettings.SlideShow.GetTrialNames();

      // Use the decimal separator specified.
      NumberFormatInfo nfi = CultureInfo.GetCultureInfo("en-US").NumberFormat;
      if (asciiSetting.DecimalSeparatorCharacter == ',')
      {
        nfi = CultureInfo.GetCultureInfo("de-DE").NumberFormat;
      }

      // Enumerate the columns in the import file and assign their title.
      Dictionary<string, int> columnsImportNum = new Dictionary<string, int>();
      for (int i = 0; i < asciiSetting.ColumnHeaders.Count; i++)
      {
        if (!columnsImportNum.ContainsKey(asciiSetting.ColumnHeaders[i]))
        {
          columnsImportNum.Add(asciiSetting.ColumnHeaders[i], i);
        }
        else
        {
          return;
        }
      }

      // Get the assigned titles of the import columns for Ogamas columns
      string strSubjectNameImportColumn = asciiSetting.ColumnAssignments["SubjectName"];
      string strTrialSequenceImportColumn = asciiSetting.ColumnAssignments["TrialSequence"];
      string strTrialIDImportColumn = asciiSetting.ColumnAssignments["TrialID"];
      string strTrialImageImportColumn = asciiSetting.ColumnAssignments["TrialImage"];
      string strCategoryImportColumn = asciiSetting.ColumnAssignments["TrialCategory"];
      string strTimeImportColumn = asciiSetting.ColumnAssignments["Time"];
      string strPupilDiaXImportColumn = asciiSetting.ColumnAssignments["PupilDiaX"];
      string strPupilDiaYImportColumn = asciiSetting.ColumnAssignments["PupilDiaY"];
      string strGazePosXImportColumn = asciiSetting.ColumnAssignments["GazePosX"];
      string strGazePosYImportColumn = asciiSetting.ColumnAssignments["GazePosY"];
      string strMousePosXImportColumn = asciiSetting.ColumnAssignments["MousePosX"];
      string strMousePosYImportColumn = asciiSetting.ColumnAssignments["MousePosY"];

      // Convert the names into column counters.
      int numSubjectImportColumn = strSubjectNameImportColumn == string.Empty ? -1 : columnsImportNum[strSubjectNameImportColumn];
      int numTrialSequenceImportColumn = strTrialSequenceImportColumn == string.Empty ? -1 : columnsImportNum[strTrialSequenceImportColumn];
      int numTrialIDImportColumn = strTrialIDImportColumn == string.Empty ? -1 : columnsImportNum[strTrialIDImportColumn];
      int numTrialImageImportColumn = strTrialImageImportColumn == string.Empty ? -1 : columnsImportNum[strTrialImageImportColumn];
      int numCategoryImportColumn = strCategoryImportColumn == string.Empty ? -1 : columnsImportNum[strCategoryImportColumn];
      int numTimeImportColumn = strTimeImportColumn == string.Empty ? -1 : columnsImportNum[strTimeImportColumn];
      int numPupilDiaXImportColumn = strPupilDiaXImportColumn == string.Empty ? -1 : columnsImportNum[strPupilDiaXImportColumn];
      int numPupilDiaYImportColumn = strPupilDiaYImportColumn == string.Empty ? -1 : columnsImportNum[strPupilDiaYImportColumn];
      int numGazePosXImportColumn = strGazePosXImportColumn == string.Empty ? -1 : columnsImportNum[strGazePosXImportColumn];
      int numGazePosYImportColumn = strGazePosYImportColumn == string.Empty ? -1 : columnsImportNum[strGazePosYImportColumn];
      int numMousePosXImportColumn = strMousePosXImportColumn == string.Empty ? -1 : columnsImportNum[strMousePosXImportColumn];
      int numMousePosYImportColumn = strMousePosYImportColumn == string.Empty ? -1 : columnsImportNum[strMousePosYImportColumn];

      // Create a list of starting times from the list of the trial dialog
      // if this mode is selected.
      if (detectionSetting.TrialImportMode == TrialSequenceImportModes.UseAssignmentTable)
      {
        // If first given trial start time does not start with zero,
        // add a "zero trial" for the very first samples
        if (detectionSetting.TrialSequenceToStarttimeAssignments.Count > 0)
        {
          ////if (!detectionSetting.TrialSequenceToStarttimeAssignments.ContainsValue(0))
          ////{
          ////  int zeroTrialSequence = 0;
          ////  if (detectionSetting.TrialSequenceToStarttimeAssignments.ContainsKey(0))
          ////  {
          ////    zeroTrialSequence = -1;
          ////  }

          ////  detectionSetting.TrialSequenceToStarttimeAssignments.Add(zeroTrialSequence, 0);
          ////}
        }
        else
        {
          // In this trial import modes an empty TrialSequenceToStarttimeAssignments
          // List is not allowed.
          return;
        }
      }
      else
      {
        detectionSetting.TrialSequenceToTrialIDAssignments.Clear();
        detectionSetting.TrialSequenceToStarttimeAssignments.Clear();
      }

      string line = string.Empty;
      int counter = 0;
      int columncount = 0;
      int trialCounter = 0;
      int currentTrialSequence = 0;
      bool isLastTrial = false;
      string lastSubjectName = "#";
      string currentSubjectName = "#";
      SortedList<int, long> trial2Time = detectionSetting.TrialSequenceToStarttimeAssignments;
      if (trial2Time.Count > 0)
      {
        currentTrialSequence = trial2Time.Keys[0];
      }

      // Begin reading File
      try
      {
        // Open file
        using (StreamReader importReader = new StreamReader(asciiSetting.Filename))
        {
          // field for checking for doubled times
          long lastTimeInMs = -100;

          // Clear old entries except parses from table
          if (detectionSetting.StimuliImportMode != StimuliImportModes.UseAssignmentTable)
          {
            detectionSetting.ImageDictionary.Clear();
            detectionSetting.TrialIDToImageAssignments.Clear();
          }

          // Read every line of ImportFile
          while ((line = importReader.ReadLine()) != null)
          {
            // ignore empty lines
            if (line.Trim() == string.Empty)
            {
              continue;
            }

            // Ignore Quotes if applicable
            if (asciiSetting.IgnoreQuotes &&
              line.Trim().Substring(0, asciiSetting.IgnoreQuotationString.Length) ==
              asciiSetting.IgnoreQuotationString)
            {
              continue;
            }

            // Check for trial detection specifications
            switch (detectionSetting.TrialImportMode)
            {
              case TrialSequenceImportModes.UseMSGLines:
                // Check for MSG lines containing the trigger string if applicable
                if (line.Contains(detectionSetting.TrialTriggerString))
                {
                  currentTrialSequence = trialCounter;
                  trialCounter++;
                }

                break;
              case TrialSequenceImportModes.UseAssignmentTable:
              case TrialSequenceImportModes.UseImportColumn:
                // No trial counting needed
                break;
            }

            // Check for image detection specifications
            switch (detectionSetting.StimuliImportMode)
            {
              // Read iViewX Scene Image Lines if applicable
              case StimuliImportModes.UseiViewXMSG:
                // Two cases: Lines with "scene image: bitmap.bmp" or 
                // lines with "# Message: bitmap.bmp" possible.
                ExtractImageNameFromiViewXMSG(line, "Scene Image: ", currentTrialSequence);
                ExtractImageNameFromiViewXMSG(line, "# Message", currentTrialSequence);
                break;

              // Search for Image filenames if applicable
              case StimuliImportModes.SearchForImageEnding:
                if (line.Contains("." + detectionSetting.StimuliFileExtension))
                {
                  string[] itemsImageLine = line.Trim().Split(asciiSetting.ColumnSeparatorCharacter);
                  string fileName = string.Empty;
                  foreach (string str in itemsImageLine)
                  {
                    string[] subItemsImageLine = str.Trim().Split(' ');
                    foreach (string str2 in subItemsImageLine)
                    {
                      if (str2.Contains("." + detectionSetting.StimuliFileExtension))
                      {
                        fileName = str2;
                        fileName = fileName.Trim();
                        fileName = fileName.Replace("'", string.Empty);
                        fileName = Path.GetFileName(fileName);
                      }
                    }
                  }

                  // Only save the first image for each trial...
                  if (!detectionSetting.ImageDictionary.ContainsKey(currentTrialSequence))
                  {
                    detectionSetting.ImageDictionary.Add(currentTrialSequence, fileName);
                  }

                  string stimulusName = Path.GetFileNameWithoutExtension(fileName);
                  int trialID = currentTrialSequence;
                  if (trialNames.Contains(stimulusName))
                  {
                    Trial trialFound = trials.Find(delegate(Trial t) { return t.Name == stimulusName; });
                    if (trialFound == null)
                    {
                      // This trial picture was seen before in import process
                      foreach (KeyValuePair<int, string> kvp in detectionSetting.TrialIDToImageAssignments)
                      {
                        if (Path.GetFileNameWithoutExtension(kvp.Value) == stimulusName)
                        {
                          trialID = kvp.Key;
                          break;
                        }
                      }
                    }
                    else
                    {
                      // This trial is in the slideshow already
                      trialID = trialFound.ID;
                    }
                  }
                  else
                  {
                    trialNames.Add(stimulusName);
                  }

                  if (!detectionSetting.TrialIDToImageAssignments.ContainsKey(trialID))
                  {
                    detectionSetting.TrialIDToImageAssignments.Add(trialID, fileName);
                  }

                  if (!detectionSetting.TrialSequenceToTrialIDAssignments.ContainsKey(currentTrialSequence))
                  {
                    detectionSetting.TrialSequenceToTrialIDAssignments.Add(currentTrialSequence, trialID);
                  }
                }

                break;
              case StimuliImportModes.UseAssignmentTable:
              case StimuliImportModes.UseImportColumn:
                // No image extraction needed.
                break;
            }

            // Ignore lines that do not have the "use only" quotation
            // string
            if (asciiSetting.UseQuotes &&
              !line.Contains(asciiSetting.UseQuotationString))
            {
              continue;
            }

            // ignore lines with ignore trigger
            if (asciiSetting.IgnoreTriggerStringLines &&
              line.Contains(asciiSetting.IgnoreTriggerString))
            {
              continue;
            }

            // Split Tab separated line items
            string[] items = line.Split(asciiSetting.ColumnSeparatorCharacter);

            // Set columncount in first valid line
            if (counter == 0)
            {
              columncount = items.Length;
            }

            // Skip first line if filled with column titles
            if (asciiSetting.ColumnTitlesAtFirstRow && counter == 0)
            {
              counter++;
              continue;
            }

            // Use only numeric starting lines if applicable
            if (asciiSetting.IgnoreNotNumeralLines && !IOHelpers.IsNumeric(line[0]))
            {
              continue;
            }

            // Skip small lines if applicable
            if (asciiSetting.IgnoreSmallLines && columncount != items.Length)
            {
              continue;
            }

            // Create Ogama columns placeholder
            RawData newRawData = new RawData();

            // Calculate time value
            double timeInFileTime = Convert.ToDouble(items[numTimeImportColumn], nfi);
            long timeInMs = Convert.ToInt64(timeInFileTime * detectionSetting.TimeFactor);

            // If there is a data < starttime, skip them
            if (detectionSetting.TrialSequenceToStarttimeAssignments.Count > 0)
            {
              if (timeInMs < detectionSetting.TrialSequenceToStarttimeAssignments.Values[0])
              {
                continue;
              }
            }

            // Save starttime
            if (counter == 0 || (asciiSetting.ColumnTitlesAtFirstRow && counter == 1))
            {
              asciiSetting.StartTime = timeInMs;
              trial2Time[currentTrialSequence] = timeInMs;
              detectionSetting.TrialSequenceToStarttimeAssignments[currentTrialSequence] = timeInMs;
            }

            // Check for duplicate time entries
            if (timeInFileTime == lastTimeInFileTime)
            {
              string message = string.Format(
                "Two consecutive raw data samples had the same sampling time {0}."
                + Environment.NewLine + "Time in FileTime is {1}" + Environment.NewLine +
                "PrevTime in FileTime is {2}" + Environment.NewLine +
                "This indicates an logfile error or wrong timescale."
                + Environment.NewLine + "Please try to change the timescale to milliseconds.",
                timeInMs,
                timeInFileTime,
                lastTimeInFileTime);
              ExceptionMethods.ProcessErrorMessage(message);
              return;
            }

            // Set time check value
            lastTimeInMs = timeInMs;
            lastTimeInFileTime = timeInFileTime;

            // Save time value
            newRawData.Time = timeInMs - asciiSetting.StartTime;

            // Write subject name value
            if (numSubjectImportColumn != -1)
            {
              currentSubjectName = items[numSubjectImportColumn];
              newRawData.SubjectName = currentSubjectName;
              if (currentSubjectName != lastSubjectName)
              {
                lastSubjectName = currentSubjectName;
                currentTrialSequence = 0;
                trialCounter = 0;
              }
            }
            else
            {
              newRawData.SubjectName = detectionSetting.SubjectName;
            }

            // Write Trial Sequence
            switch (detectionSetting.TrialImportMode)
            {
              case TrialSequenceImportModes.UseMSGLines:
                newRawData.TrialSequence = currentTrialSequence;
                break;
              case TrialSequenceImportModes.UseAssignmentTable:
                if (!isLastTrial &&
                  newRawData.Time + asciiSetting.StartTime >= trial2Time.Values[trialCounter])
                {
                  trialCounter++;
                  if (trialCounter >= trial2Time.Count)
                  {
                    isLastTrial = true;
                  }
                }

                currentTrialSequence = trial2Time.Keys[trialCounter - 1];
                newRawData.TrialSequence = currentTrialSequence;
                break;
              case TrialSequenceImportModes.UseImportColumn:
                if (numTrialSequenceImportColumn != -1)
                {
                  currentTrialSequence = Convert.ToInt32(items[numTrialSequenceImportColumn]);
                  newRawData.TrialSequence = currentTrialSequence;
                }

                break;
              default:
                break;
            }

            int? columnTrialID = null;

            // Write trialID to detection settings.
            if (numTrialIDImportColumn != -1)
            {
              columnTrialID = Convert.ToInt32(items[numTrialIDImportColumn]);
              if (!detectionSetting.TrialSequenceToTrialIDAssignments.ContainsKey(currentTrialSequence))
              {
                detectionSetting.TrialSequenceToTrialIDAssignments.Add(currentTrialSequence, columnTrialID.Value);
              }
            }

            // Write Stimulus file to detection settings.
            if (numTrialImageImportColumn != -1)
            {
              int usedTrialID = currentTrialSequence;
              if (columnTrialID.HasValue)
              {
                usedTrialID = columnTrialID.Value;
              }

              if (!detectionSetting.TrialIDToImageAssignments.ContainsKey(usedTrialID))
              {
                detectionSetting.TrialIDToImageAssignments.Add(usedTrialID, items[numTrialImageImportColumn]);
              }

              if (!detectionSetting.TrialSequenceToTrialIDAssignments.ContainsKey(currentTrialSequence))
              {
                detectionSetting.TrialSequenceToTrialIDAssignments.Add(currentTrialSequence, usedTrialID);
              }
            }

            // Write trial category
            if (numCategoryImportColumn != -1)
            {
              newRawData.Category = items[numCategoryImportColumn].ToString();
            }

            // Write PupilDiameters
            if (numPupilDiaXImportColumn != -1)
            {
              if (IOHelpers.IsNumeric(items[numPupilDiaXImportColumn]))
              {
                newRawData.PupilDiaX = Convert.ToSingle(items[numPupilDiaXImportColumn], nfi);
              }
            }

            if (numPupilDiaYImportColumn != -1)
            {
              if (IOHelpers.IsNumeric(items[numPupilDiaYImportColumn]))
              {
                newRawData.PupilDiaY = Convert.ToSingle(items[numPupilDiaYImportColumn], nfi);
              }
            }

            // Write gaze positions
            if (numGazePosXImportColumn != -1)
            {
              if (IOHelpers.IsNumeric(items[numGazePosXImportColumn]))
              {
                newRawData.GazePosX = Convert.ToSingle(items[numGazePosXImportColumn], nfi);
              }
            }

            if (numGazePosYImportColumn != -1)
            {
              if (IOHelpers.IsNumeric(items[numGazePosYImportColumn]))
              {
                newRawData.GazePosY = Convert.ToSingle(items[numGazePosYImportColumn], nfi);
              }
            }

            // Write mouse positions
            if (numMousePosXImportColumn != -1)
            {
              if (IOHelpers.IsNumeric(items[numMousePosXImportColumn]))
              {
                newRawData.MousePosX = Convert.ToSingle(items[numMousePosXImportColumn], nfi);
              }
            }

            if (numMousePosYImportColumn != -1)
            {
              if (IOHelpers.IsNumeric(items[numMousePosYImportColumn]))
              {
                newRawData.MousePosY = Convert.ToSingle(items[numMousePosYImportColumn], nfi);
              }
            }

            // Add the parsed raw data row to the list.
            rawDataList.Add(newRawData);

            // Increase counter
            counter++;

            // Cancel import, if only a part for preview should be imported.
            if (counter > numberOfImportLines && numberOfImportLines >= 0)
            {
              break;
            }
          }

          // reached end of file of number of import lines
        }
      }
      catch (Exception ex)
      {
        ExceptionMethods.HandleException(ex);
      }
    }

    /// <summary>
    /// This method iterates the imported raw data rows to
    /// catch the trial changes that are detected during the call
    /// of <see cref="GenerateOgamaRawDataList(int)"/>.
    /// The trials are then written into the trial list.
    /// </summary>
    private static void GenerateOgamaSubjectAndTrialList()
    {
      // Clear foregoing imports.
      trialList.Clear();
      subjectList.Clear();

      if (rawDataList.Count == 0)
      {
        // string message = "The parsing of the log file into OGAMAs " +
        //  "Raw data columns failed. No lines have been succesfully read. " +
        //  Environment.NewLine +
        //  "So the trial generation could not be started." +
        //  Environment.NewLine + "Please change the import settings and try again";
        // ExceptionMethods.ProcessErrorMessage(message);
        return;
      }

      // Initializes variables
      int currentSequence = 0;
      int lastSequence = -5;
      string currentSubject = "#";
      string lastSubject = "#";
      int overallTrialCounter = 0;
      int trialCounter = 0;
      int subjectCounter = 0;

      // Iterate raw data list
      for (int i = 0; i < rawDataList.Count; i++)
      {
        RawData importRow = rawDataList[i];
        currentSequence = importRow.TrialSequence;
        currentSubject = importRow.SubjectName;

        // If subject has changed write new subject table entry.
        if (currentSubject != lastSubject)
        {
          SubjectsData newSubjectsData = new SubjectsData();
          newSubjectsData.SubjectName = currentSubject;
          subjectList.Add(newSubjectsData);

          if (subjectCounter > 0)
          {
            TrialsData tempSubjetData = trialList[overallTrialCounter - 1];
            tempSubjetData.Duration =
              (int)(rawDataList[i - 1].Time - tempSubjetData.TrialStartTime);
            trialList[overallTrialCounter - 1] = tempSubjetData;
          }

          lastSubject = currentSubject;
          lastSequence = -5;
          trialCounter = 0;
          subjectCounter++;
        }

        // If trial has changed parse the trial information to 
        // create a trial entry in the trialList.
        if (currentSequence != lastSequence)
        {
          string subject = importRow.SubjectName != null ? importRow.SubjectName : "Subject1";
          string categorie = importRow.Category != null ? importRow.Category : string.Empty;
          int trialID = currentSequence;
          if (detectionSetting.TrialSequenceToTrialIDAssignments.ContainsKey(currentSequence))
          {
            trialID = detectionSetting.TrialSequenceToTrialIDAssignments[currentSequence];
          }

          string image = "No image file specified";

          switch (detectionSetting.StimuliImportMode)
          {
            case StimuliImportModes.UseiViewXMSG:
              if (detectionSetting.ImageDictionary.ContainsKey(currentSequence))
              {
                image = detectionSetting.ImageDictionary[currentSequence];
              }

              break;
            case StimuliImportModes.UseImportColumn:
            case StimuliImportModes.UseAssignmentTable:
              if (detectionSetting.TrialSequenceToTrialIDAssignments.ContainsKey(currentSequence))
              {
                trialID = detectionSetting.TrialSequenceToTrialIDAssignments[currentSequence];
                if (detectionSetting.TrialIDToImageAssignments.ContainsKey(trialID))
                {
                  image = detectionSetting.TrialIDToImageAssignments[trialID];
                }
              }

              break;
            case StimuliImportModes.SearchForImageEnding:
              if (detectionSetting.ImageDictionary.ContainsKey(currentSequence))
              {
                image = detectionSetting.ImageDictionary[currentSequence];
              }

              break;
          }

          // Add empty trial to sequence numbering.
          if (detectionSetting.StimuliImportMode != StimuliImportModes.UseImportColumn
            && image == "No image file specified")
          {
            if (!detectionSetting.TrialSequenceToTrialIDAssignments.ContainsKey(currentSequence))
            {
              detectionSetting.TrialSequenceToTrialIDAssignments.Add(currentSequence, 0);
            }
          }

          long time = 0;
          switch (detectionSetting.TrialImportMode)
          {
            ////// Use the table timing
            ////if (detectionSetting.TrialSequenceToStarttimeAssignments.ContainsKey(currentSequence))
            ////{
            ////  time = detectionSetting.TrialSequenceToStarttimeAssignments[currentSequence];
            ////}
            //// break;

            case TrialSequenceImportModes.UseAssignmentTable:
            case TrialSequenceImportModes.UseMSGLines:
            case TrialSequenceImportModes.UseImportColumn:
              // Use the raw data timing
              time = importRow.Time;
              break;
          }

          // Create trial row
          TrialsData newTrialData = new TrialsData();
          newTrialData.SubjectName = subject;
          newTrialData.TrialSequence = currentSequence;
          newTrialData.TrialID = trialID;
          newTrialData.TrialName = image;
          newTrialData.Category = categorie;
          newTrialData.TrialStartTime = time;
          newTrialData.Duration = -1;
          trialList.Add(newTrialData);

          lastSequence = currentSequence;
          trialCounter++;
          overallTrialCounter++;

          // Calculate trial duration for foregoing trial.
          if (trialCounter > 1)
          {
            TrialsData tempSubjetData = trialList[trialCounter - 2];
            int duration = 0;
            switch (detectionSetting.TrialImportMode)
            {
              case TrialSequenceImportModes.UseAssignmentTable:
                // Use the table timing
                duration = (int)(time - tempSubjetData.TrialStartTime);
                break;
              case TrialSequenceImportModes.UseMSGLines:
              case TrialSequenceImportModes.UseImportColumn:
                // Use the raw data timing
                duration = (int)(rawDataList[i].Time - tempSubjetData.TrialStartTime);
                break;
            }

            //// If there is lot of time (>200ms) left between last and current trial
            //// don´t use the space in between for the duration value.
            ////if (rawDataList[i].Time - rawDataList[i - 1].Time > 200)
            ////{
            ////  duration = (int)(rawDataList[i - 1].Time - tempSubjetData.TrialStartTime);
            ////}

            tempSubjetData.Duration = duration;
            trialList[trialCounter - 2] = tempSubjetData;
          }
        }
      }

      // Reached end of rawdatalist, so add last trial duration value from last entry
      if (trialCounter >= 1)
      {
        TrialsData tempSubjetData = trialList[overallTrialCounter - 1];
        tempSubjetData.Duration =
          (int)(rawDataList[rawDataList.Count - 1].Time - tempSubjetData.TrialStartTime);
        trialList[overallTrialCounter - 1] = tempSubjetData;
      }
    }

    /// <summary>
    /// This static method creates a slide with a sized image 
    /// for each trial and adds it to the slideshow.
    /// </summary>
    /// <param name="detectonSettings">The <see cref="DetectionSettings"/>
    /// used in this import.</param>
    /// <param name="mainWindow">The <see cref="MainForm"/> to get access to the status label.</param>
    private static void GenerateOgamaSlideshowTrials(DetectionSettings detectonSettings, MainForm mainWindow)
    {
      // Stores found stimuli files
      List<string> trialNames = Document.ActiveDocument.ExperimentSettings.SlideShow.GetTrialNames();

      foreach (KeyValuePair<int, int> kvp in detectionSetting.TrialSequenceToTrialIDAssignments)
      {
        int trialID = kvp.Value;
        string file = string.Empty;
        if (detectionSetting.TrialIDToImageAssignments.ContainsKey(trialID))
        {
          file = detectionSetting.TrialIDToImageAssignments[trialID];
        }

        string filename = Path.GetFileNameWithoutExtension(file);

        // Create slide
        var stopConditions = new StopConditionCollection
          {
            new MouseStopCondition(MouseButtons.Left, true, string.Empty, null, Point.Empty)
          };

        VGImage stimulusImage = null;

        if (file != string.Empty)
        {
          stimulusImage = new VGImage(
            ShapeDrawAction.None,
            Pens.Black,
            Brushes.Black,
            SystemFonts.MenuFont,
            Color.White,
            Path.GetFileName(file),
            Document.ActiveDocument.ExperimentSettings.SlideResourcesPath,
            ImageLayout.Zoom,
            1f,
            Document.ActiveDocument.PresentationSize,
            VGStyleGroup.None,
            filename,
            string.Empty,
            true)
            {
              Size = Document.ActiveDocument.PresentationSize
            };
        }

        var newSlide = new Slide(
          filename, Color.White, null, stopConditions, null, string.Empty, Document.ActiveDocument.PresentationSize)
          {
            Modified = true,
            MouseCursorVisible = true
          };

        // Only add stimulus if an image exists
        if (file != string.Empty)
        {
          newSlide.VGStimuli.Add(stimulusImage);
        }
        else
        {
          newSlide.Name = "No stimulus detected";
        }

        // Create trial
        trialID = int.Parse(Document.ActiveDocument.ExperimentSettings.SlideShow.GetUnusedNodeID());

        var newTrial = new Trial(filename, trialID)
          {
            Name = filename
          };

        newTrial.Add(newSlide);

        if (trialNames.Contains(filename)
          ||
          (filename == string.Empty && trialNames.Contains("No stimulus detected")))
        {
          // Trial already exists
          continue;
        }

        trialNames.Add(filename);

        // Create slide node
        var slideNode = new SlideshowTreeNode(newSlide.Name)
        {
          Name = trialID.ToString(CultureInfo.InvariantCulture),
          Slide = newSlide
        };

        // Add slide node to slideshow
        Document.ActiveDocument.ExperimentSettings.SlideShow.Nodes.Add(slideNode);

        ////if (stimulusImage != null)
        ////{
        ////  stimulusImage.Dispose();
        ////}

        ////newSlide.Dispose();

        Document.ActiveDocument.Modified = true;
      }

      mainWindow.StatusLabel.Text = "Saving slideshow to file ...";
      if (!Document.ActiveDocument.SaveSettingsToFile(Document.ActiveDocument.ExperimentSettings.DocumentFilename))
      {
        ExceptionMethods.ProcessErrorMessage("Couldn't save slideshow to experiment settings.");
      }

      mainWindow.StatusLabel.Text = "Refreshing context panel ...";
      mainWindow.RefreshContextPanelImageTabs();
      mainWindow.StatusLabel.Text = "Ready ...";
      mainWindow.StatusProgressbar.Value = 0;
    }

    /// <summary>
    /// This method writes the data that is written in the lists during
    /// import to OGAMAs dataset.
    /// If this could be succesfully done the whole new data is
    /// written to the database (.mdf).
    /// </summary>
    /// <returns><strong>True</strong> if succesful, otherwise
    /// <strong>false</strong>.</returns>
    private static bool SaveImportIntoTablesAndDB()
    {
      Dictionary<string, List<RawData>> rawDataBySubject = SplitRawDataListBySubjects(rawDataList);
      Dictionary<string, List<TrialsData>> trialDataBySubject = SplitTrialDataListBySubjects(trialList);
      int subjectErrorCounter = 0;
      try
      {
        foreach (SubjectsData subject in subjectList)
        {
          string testSub = subject.SubjectName;
          if (!Queries.ValidateSubjectName(ref testSub, false))
          {
            subjectErrorCounter++;
            continue;
          }

          List<RawData> subjectRawData = rawDataBySubject[subject.SubjectName];
          List<TrialsData> subjectTrialsData = trialDataBySubject[subject.SubjectName];
          if (!Queries.WriteRawDataListToDataSet(subject.SubjectName, subjectRawData))
          {
            throw new DataException("The new raw data table could not be written into the dataset.");
          }

          // Creates an empty raw data table in the mdf database
          Queries.CreateRawDataTableInDB(subject.SubjectName);

          // Write RawDataTable into File with Bulk Statement
          Queries.WriteRawDataWithBulkStatement(subject.SubjectName);

          // Save subject information to dataset
          if (!Queries.WriteSubjectToDataSet(subject))
          {
            throw new DataException("The new subject information could not be written into the dataset.");
          }

          // Save trial information to dataset
          if (!Queries.WriteTrialsDataListToDataSet(subjectTrialsData))
          {
            throw new DataException("The new trials table could not be written into the dataset.");
          }
        }

        Document.ActiveDocument.DocDataSet.EnforceConstraints = false;

        // Update subjects and trials table in the mdf database
        int affectedRows = Document.ActiveDocument.DocDataSet.TrialsAdapter.Update(
          Document.ActiveDocument.DocDataSet.Trials);
        affectedRows = Document.ActiveDocument.DocDataSet.SubjectsAdapter.Update(
         Document.ActiveDocument.DocDataSet.Subjects);

        Document.ActiveDocument.DocDataSet.AcceptChanges();
        Document.ActiveDocument.DocDataSet.CreateRawDataAdapters();
      }
      catch (Exception ex)
      {
        ExceptionMethods.HandleException(ex);

        // CleanUp
        // First reject changes (remove trial and subject table modifications)
        Document.ActiveDocument.DocDataSet.RejectChanges();

        foreach (SubjectsData subject in subjectList)
        {
          // Remove eventually added raw data table in dataset
          if (Document.ActiveDocument.DocDataSet.Tables.Contains(subject.SubjectName + "Rawdata"))
          {
            Document.ActiveDocument.DocDataSet.Tables.Remove(subject.SubjectName + "Rawdata");
          }

          // Remove raw data table in database file (.mdf)
          Queries.DeleteRawDataTableInDB(subject.SubjectName);
        }

        return false;
      }
      finally
      {
        Document.ActiveDocument.DocDataSet.EnforceConstraints = true;
      }

      if (subjectErrorCounter > 0)
      {
        string message = subjectErrorCounter.ToString() + " subject(s) have unallowed names or " +
          "their names already exist in the experiments database." +
          Environment.NewLine + "Please modify your import file and change the subject name(s), or delete " +
          "the existing database entry.";
        ExceptionMethods.ProcessMessage("Unallowed subject names", message);
        return false;
      }

      return true;
    }

    /// <summary>
    /// This method splits the given raw data list into a 
    /// dictionary of raw data lists separated by subjects.
    /// </summary>
    /// <remarks>This is done to enable writing a raw data table for each subject.</remarks>
    /// <param name="wholeRawDataList">A <see cref="List{RawData}"/>
    /// with all the imported samples.</param>
    /// <returns>A Dictionary with the splitted input.</returns>
    private static Dictionary<string, List<RawData>> SplitRawDataListBySubjects(List<RawData> wholeRawDataList)
    {
      // Create the return dictionary
      Dictionary<string, List<RawData>> rawDataBySubject = new Dictionary<string, List<RawData>>();

      // Get First subject name
      string lastSubjectName = wholeRawDataList[0].SubjectName;

      // Create list for current subject
      List<RawData> currentList = new List<RawData>();

      // Iterate whole raw data list and add for each subject a 
      // new entry in the rawDataBySubject list.
      foreach (RawData data in wholeRawDataList)
      {
        if (data.SubjectName != lastSubjectName)
        {
          rawDataBySubject.Add(lastSubjectName, currentList);
          currentList = new List<RawData>();
          lastSubjectName = data.SubjectName;
        }

        currentList.Add(data);
      }

      // Add last subject
      rawDataBySubject.Add(lastSubjectName, currentList);

      // Return list.
      return rawDataBySubject;
    }

    /// <summary>
    /// This method splits the given trials data list into a 
    /// dictionary of trial data lists separated by subjects.
    /// </summary>
    /// <param name="wholeTrialDataList">A <see cref="List{TrialsData}"/>
    /// with all the imported samples.</param>
    /// <returns>A Dictionary with the splitted input.</returns>
    private static Dictionary<string, List<TrialsData>> SplitTrialDataListBySubjects(List<TrialsData> wholeTrialDataList)
    {
      // Create the return dictionary
      Dictionary<string, List<TrialsData>> trialDataBySubject = new Dictionary<string, List<TrialsData>>();

      // Get First subject name
      string lastSubjectName = wholeTrialDataList[0].SubjectName;

      // Create list for current subject
      List<TrialsData> currentList = new List<TrialsData>();

      // Iterate whole raw data list and add for each subject a 
      // new entry in the rawDataBySubject list.
      foreach (TrialsData data in wholeTrialDataList)
      {
        if (data.SubjectName != lastSubjectName)
        {
          trialDataBySubject.Add(lastSubjectName, currentList);
          currentList = new List<TrialsData>();
          lastSubjectName = data.SubjectName;
        }

        currentList.Add(data);
      }

      // Add last subject
      trialDataBySubject.Add(lastSubjectName, currentList);

      // Return list.
      return trialDataBySubject;
    }

    /// <summary>
    /// This method shows a dialog, that gives the option
    /// to use a import settings file during import.
    /// </summary>
    /// <remarks>This functionality is extremely useful, when multiple log
    /// files of the same type should be imported.</remarks>
    private static void AskforUsingSettingsFile()
    {
      if (MessageBox.Show(
        "Would you like to use a settings file ?",
        Application.ProductName,
        MessageBoxButtons.YesNo,
        MessageBoxIcon.Question) == DialogResult.Yes)
      {
        OpenFileDialog ofdOpenSettings = new OpenFileDialog();
        ofdOpenSettings.DefaultExt = "ois";
        ofdOpenSettings.FileName = "*.ois";
        ofdOpenSettings.FilterIndex = 1;
        ofdOpenSettings.InitialDirectory = Properties.Settings.Default.ImportSettingsPath;
        ofdOpenSettings.Filter = "Ogama import settings files|*.ois";
        ofdOpenSettings.Title = "Please select import settings file ...";
        if (ofdOpenSettings.ShowDialog() == DialogResult.OK)
        {
          string settingsFilename = ofdOpenSettings.FileName;
          ImportRawData.DeserializeSettings(settingsFilename);
          detectionSetting.SubjectName = "Subject1";
          detectionSetting.SavedSettings = true;
        }
      }
    }

    /// <summary>
    /// This method shows a dialog asking for saving the current
    /// import settings to hard disk.
    /// They are persited in xml format.
    /// </summary>
    /// <param name="logFileImportSettings">A <see cref="ASCIISettings"/> with the ascii parsing settings.</param>
    /// <param name="rawdataSettings">A <see cref="DetectionSettings"/> with the raw data parsing settings.</param>
    private static void SaveImportSettings(
      ASCIISettings logFileImportSettings,
      DetectionSettings rawdataSettings)
    {
      var ofdSaveSettings = new SaveFileDialog
        {
          DefaultExt = "ois",
          FileName = "*.ois",
          FilterIndex = 1,
          Filter = "Ogama import settings files|*.ois",
          Title = "Please specify settings filename",
          InitialDirectory = Properties.Settings.Default.ImportSettingsPath
        };

      if (ofdSaveSettings.ShowDialog() == DialogResult.OK)
      {
        SerializeSettings(ofdSaveSettings.FileName);
      }
    }

    #endregion //METHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER

    /// <summary>
    /// This method extracts a filename of a iViewX msg line
    /// with the given trigger string.
    /// </summary>
    /// <param name="line">The line to search for.</param>
    /// <param name="triggerString">The trigger string in the message after which the filename
    /// appears.</param>
    /// <param name="currentTrialID">The ID of the current trial.</param>
    private static void ExtractImageNameFromiViewXMSG(string line, string triggerString, int currentTrialID)
    {
      if (line.Contains(triggerString))
      {
        string[] items = line.Trim().Split(asciiSetting.ColumnSeparatorCharacter);
        foreach (string item in items)
        {
          // Find item with trigger string
          if (item.Contains(triggerString))
          {
            // Remove "Scene Image" Prefix to get the filename
            string imagepath = item.Replace(triggerString, string.Empty);
            string stimulusFile = Path.GetFileName(imagepath).Trim();
            if (!detectionSetting.ImageDictionary.ContainsKey(currentTrialID))
            {
              detectionSetting.ImageDictionary.Add(currentTrialID, stimulusFile);
            }
            else
            {
              detectionSetting.ImageDictionary[currentTrialID] = stimulusFile;
            }

            detectionSetting.TrialSequenceToTrialIDAssignments[currentTrialID] = currentTrialID;

            if (!detectionSetting.TrialIDToImageAssignments.ContainsKey(currentTrialID))
            {
              detectionSetting.TrialIDToImageAssignments.Add(currentTrialID, stimulusFile);
            }

            break;
          }
        }
      }
    }

    /// <summary>
    /// Saves the current import setting to a OGAMA import settings file.
    /// Extension ".ois"
    /// </summary>
    /// <param name="filePath">A <see cref="string"/> with the path to the 
    /// OGAMA target import settings xml file.</param>
    private static void SerializeSettings(string filePath)
    {
      try
      {
        using (TextWriter writer = new StreamWriter(filePath))
        {
          var settings = new MergedSettings
            {
              AsciiSetting = asciiSetting,
              DetectionSetting = detectionSetting
            };

          var serializer = new XmlSerializer(typeof(MergedSettings));
          serializer.Serialize(writer, settings);
        }
      }
      catch (Exception ex)
      {
        ExceptionMethods.HandleException(ex);
      }
    }

    /// <summary>
    /// Reads an OGAMA import settings file.
    /// </summary>
    /// <param name="filePath">A <see cref="string"/> with the path to the 
    /// OGAMA import settings xml file.</param>
    /// <returns><strong>True</strong> if successful, 
    /// otherwise <strong>null</strong>.</returns>
    private static bool DeserializeSettings(string filePath)
    {
      try
      {
        using (var fs = new FileStream(filePath, FileMode.Open))
        {
          // Create an instance of the XmlSerializer class;
          // specify the type of object to be deserialized 
          var serializer = new XmlSerializer(typeof(MergedSettings));

          /* Use the Deserialize method to restore the object's state with
          data from the XML document. */
          var settings = (MergedSettings)serializer.Deserialize(fs);

          asciiSetting = settings.AsciiSetting;
          detectionSetting = settings.DetectionSetting;
        }
      }
      catch (Exception ex)
      {
        ExceptionMethods.HandleException(ex);

        return false;
      }

      return true;
    }

    #endregion //HELPER
  }
}
