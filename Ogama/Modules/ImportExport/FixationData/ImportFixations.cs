// <copyright file="ImportFixations.cs" company="FU Berlin">
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

namespace Ogama.Modules.ImportExport.FixationData
{
  using System;
  using System.Collections.Generic;
  using System.Data;
  using System.Globalization;
  using System.IO;
  using System.Text;
  using System.Windows.Forms;
  using System.Xml.Serialization;

  using Ogama.ExceptionHandling;
  using Ogama.Modules.Common.Tools;
  using Ogama.Modules.Common.Types;
  using Ogama.Modules.ImportExport.Common;

  /// <summary>
  /// Class for importing fixations through multiple dialogs.
  /// </summary>
  /// <remarks>Please note that the imported fixations are treated as
  /// gaze fixations, mouse fixations are currently not importable.
  /// This import interface is mainly created to enable the import
  /// of gaze recorder generated fixations of people that do not 
  /// wan´t to rely on OGAMA´s internal fixation calculation algorithm.</remarks>
  public class ImportFixations
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
    /// List to fill with generated <see cref="SubjectsData"/>
    /// </summary>
    private static List<SubjectsData> subjectList;

    /// <summary>
    /// List to fill with generated or imported <see cref="TrialsData"/>
    /// </summary>
    private static List<TrialsData> trialList;

    /// <summary>
    /// List to fill with imported and filtered <see cref="FixationData"/>
    /// </summary>
    private static List<FixationData> fixationDataList;

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
    /// Initializes static members of the ImportFixations class.
    /// </summary>
    static ImportFixations()
    {
      subjectList = new List<SubjectsData>();
      trialList = new List<TrialsData>();
      fixationDataList = new List<FixationData>();
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
    /// Starts a multiple dialog routine (fixations import assistant)
    /// for reading fixation log files into OGAMAs database.
    /// </summary>
    public static void Start()
    {
      try
      {
        asciiSetting = new ASCIISettings();
        detectionSetting = new DetectionSettings();

        ImportFixationsAssistentDialog objfrmImportAssistent = new ImportFixationsAssistentDialog();
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
            detectionSetting.ImportType = ImportTypes.Fixations;

            ImportParseFileDialog objfrmImportReadFile = new ImportParseFileDialog(ref asciiSetting);
          ReadFile:
            DialogResult resultRawData = objfrmImportReadFile.ShowDialog();
            if (resultRawData == DialogResult.OK)
            {
              ImportFixationsAssignColumnsDialog objfrmImportFixationsAssignColumns
                = new ImportFixationsAssignColumnsDialog();

            MakeAssignments:
              DialogResult resultAssign = objfrmImportFixationsAssignColumns.ShowDialog();
              if (resultAssign == DialogResult.OK)
              {
                ImportTrialsDialog objfrmImportTrials = new ImportTrialsDialog(
                  ref asciiSetting,
                  ref detectionSetting);

                DialogResult resultTrials = objfrmImportTrials.ShowDialog();
                if (resultTrials == DialogResult.OK)
                {
                  if (InformationDialog.Show(
                    "Save ?",
                    "Would you like to save the import settings ?",
                    true,
                    MessageBoxIcon.Question) == DialogResult.Yes)
                  {
                    SaveImportSettings(asciiSetting, detectionSetting);
                  }

                  // Inform user about deletion.
                  string message = "If you continue the import process, " +
                    Environment.NewLine + "all existing " +
                    "gaze fixation data is deleted before the imported fixations " +
                    "are written to the database.";
                  if (MessageBox.Show(message, Application.ProductName, MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.Cancel)
                  {
                    return;
                  }

                  // Show import splash window
                  asciiSetting.WaitingSplash.RunWorkerAsync();

                  // Give some time to show the splash ...
                  Application.DoEvents();

                  // Read log file again, but complete
                  GenerateOgamaFixationDataList(-1);

                  // Generate the trials
                  GenerateOgamaSubjectAndTrialList();

                  // Save the import into ogamas database and the mdf file.
                  SaveImportIntoTablesAndDB();

                  // Import has finished.
                  asciiSetting.WaitingSplash.CancelAsync();

                  // Inform user about success.
                  ExceptionMethods.ProcessMessage("Success", "Import data succesfully written to database.");
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
          + "Please try again with other settings." + Environment.NewLine
          + "Error: " + ex.Message;
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
      GenerateOgamaFixationDataList(numberOfImportLines);

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
    /// Generate the list of fixations under the current
    /// parsing conditions.
    /// </summary>
    /// <param name="numberOfImportLines">An <see cref="int"/>
    /// with the max number of lines to import.
    /// Set it to -1 to use all lines.</param>
    /// <remarks>This is the heart of the class. If something does not work as expected,
    /// first have a look here.</remarks>
    private static void GenerateOgamaFixationDataList(int numberOfImportLines)
    {
      // Clear existing values
      fixationDataList.Clear();
      detectionSetting.ImageDictionary.Clear();

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
        columnsImportNum.Add(asciiSetting.ColumnHeaders[i], i);
      }

      // Get the assigned titles of the import columns for Ogamas columns
      string strSubjectNameImportColumn = asciiSetting.ColumnAssignments["SubjectName"];
      string strTrialSequenceImportColumn = asciiSetting.ColumnAssignments["TrialSequence"];
      string strTrialIDImportColumn = asciiSetting.ColumnAssignments["TrialID"];
      string strCountInTrialImportColumn = asciiSetting.ColumnAssignments["CountInTrial"];
      string strStartTimeImportColumn = asciiSetting.ColumnAssignments["StartTime"];
      string strEndTimeImportColumn = asciiSetting.ColumnAssignments["EndTime"];
      string strLengthImportColumn = asciiSetting.ColumnAssignments["Length"];
      string strPosXImportColumn = asciiSetting.ColumnAssignments["PosX"];
      string strPosYImportColumn = asciiSetting.ColumnAssignments["PosY"];

      // Convert the names into column counters.
      int numSubjectImportColumn = strSubjectNameImportColumn == string.Empty ? -1 : columnsImportNum[strSubjectNameImportColumn];
      int numTrialSequenceImportColumn = strTrialSequenceImportColumn == string.Empty ? -1 : columnsImportNum[strTrialSequenceImportColumn];
      int numTrialIDImportColumn = strTrialIDImportColumn == string.Empty ? -1 : columnsImportNum[strTrialIDImportColumn];
      int numCountInTrialImportColumn = strCountInTrialImportColumn == string.Empty ? -1 : columnsImportNum[strCountInTrialImportColumn];
      int numStartTimeImportColumn = strStartTimeImportColumn == string.Empty ? -1 : columnsImportNum[strStartTimeImportColumn];
      int numEndTimeImportColumn = strEndTimeImportColumn == string.Empty ? -1 : columnsImportNum[strEndTimeImportColumn];
      int numLengthImportColumn = strLengthImportColumn == string.Empty ? -1 : columnsImportNum[strLengthImportColumn];
      int numPosXImportColumn = strPosXImportColumn == string.Empty ? -1 : columnsImportNum[strPosXImportColumn];
      int numPosYImportColumn = strPosYImportColumn == string.Empty ? -1 : columnsImportNum[strPosYImportColumn];

      // Create a list of starting times from the list of the trial dialog
      // if this mode is selected.
      if (detectionSetting.TrialImportMode == TrialSequenceImportModes.UseAssignmentTable)
      {
        // If first given trial start time does not start with zero,
        // add a "zero trial" for the very first samples
        if (detectionSetting.TrialSequenceToStarttimeAssignments.Count > 0)
        {
          if (!detectionSetting.TrialSequenceToStarttimeAssignments.ContainsValue(0))
          {
            int zeroTrialNr = 0;
            if (detectionSetting.TrialSequenceToStarttimeAssignments.ContainsKey(0))
            {
              zeroTrialNr = -1;
            }

            detectionSetting.TrialSequenceToStarttimeAssignments.Add(zeroTrialNr, 0);
          }
        }
        else
        {
          // In this trial import modes an empty TrialIDToStarttimeAssignments
          // List is not allowed.
          return;
        }
      }

      string line = string.Empty;
      int counter = 0;
      int columncount = 0;
      int trialCounter = 0;
      int currentTrialSequence = 0;
      int lengthConverterErrorCounter = 0;
      bool isLastTrial = false;
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
                  trialCounter++;
                  currentTrialSequence = trialCounter;
                }

                break;
              case TrialSequenceImportModes.UseAssignmentTable:
              case TrialSequenceImportModes.UseImportColumn:
                // No trial counting needed
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

            // Use only numeric starting lines if applicable
            if (asciiSetting.IgnoreNotNumeralLines && !IOHelpers.IsNumeric(line[0]))
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

            // Skip small lines if applicable
            if (asciiSetting.IgnoreSmallLines && columncount != items.Length)
            {
              continue;
            }

            // Skip first line if filled with column titles
            if (asciiSetting.ColumnTitlesAtFirstRow && counter == 0)
            {
              counter++;
              continue;
            }

            // Create Ogama columns placeholder
            FixationData newFixationData = new FixationData();

            // Calculate time value
            double timeInFileTime = Convert.ToDouble(items[numStartTimeImportColumn], nfi);
            long timeInMs = Convert.ToInt64(timeInFileTime * detectionSetting.TimeFactor);

            // Save starttime
            if (counter == 0 || (asciiSetting.ColumnTitlesAtFirstRow && counter == 1))
            {
              asciiSetting.StartTime = timeInMs;
            }

            // Save time value
            newFixationData.StartTime = timeInMs - asciiSetting.StartTime;

            // Write subject name value
            if (numSubjectImportColumn != -1)
            {
              newFixationData.SubjectName = items[numSubjectImportColumn];
            }
            else
            {
              newFixationData.SubjectName = detectionSetting.SubjectName;
            }

            // Write Trial ID
            switch (detectionSetting.TrialImportMode)
            {
              case TrialSequenceImportModes.UseMSGLines:
                newFixationData.TrialID = currentTrialSequence;
                break;
              case TrialSequenceImportModes.UseAssignmentTable:
                if (!isLastTrial &&
                  timeInMs + asciiSetting.StartTime >= trial2Time.Values[trialCounter])
                {
                  trialCounter++;
                  if (trialCounter >= trial2Time.Count)
                  {
                    isLastTrial = true;
                  }
                }

                currentTrialSequence = trial2Time.Keys[trialCounter - 1];
                newFixationData.TrialSequence = currentTrialSequence;
                break;
              case TrialSequenceImportModes.UseImportColumn:
                if (numTrialIDImportColumn != -1)
                {
                  currentTrialSequence = Convert.ToInt32(items[numTrialSequenceImportColumn]);
                  newFixationData.TrialSequence = currentTrialSequence;
                }

                break;
              default:
                break;
            }

            // Write TrialID
            if (numTrialIDImportColumn != -1)
            {
              if (IOHelpers.IsNumeric(items[numTrialIDImportColumn]))
              {
                newFixationData.TrialID = Convert.ToInt32(items[numTrialIDImportColumn], nfi);
              }
              else
              {
                newFixationData.TrialID = 0;
              }
            }

            // Write count in trial
            if (numCountInTrialImportColumn != -1)
            {
              if (IOHelpers.IsNumeric(items[numCountInTrialImportColumn]))
              {
                newFixationData.CountInTrial = Convert.ToInt32(items[numCountInTrialImportColumn], nfi);
              }
              else
              {
                newFixationData.CountInTrial = counter;
              }
            }
            else
            {
              newFixationData.CountInTrial = counter;
            }

            long endtime = 0;

            // Read ending time
            if (numEndTimeImportColumn != -1)
            {
              if (IOHelpers.IsNumeric(items[numEndTimeImportColumn]))
              {
                endtime = Convert.ToInt64(items[numEndTimeImportColumn], nfi);
              }
              else
              {
                endtime = 0;
              }
            }

            // Write fixation duration
            if (numLengthImportColumn != -1)
            {
              if (IOHelpers.IsNumeric(items[numLengthImportColumn]))
              {
                newFixationData.Length = Convert.ToInt32(items[numLengthImportColumn], nfi);
              }
              else
              {
                newFixationData.Length = 0;
              }
            }

            // If no length column is specified or import failed
            // try to calculate from starting and ending time
            if (newFixationData.Length == 0)
            {
              newFixationData.Length = (int)(endtime - asciiSetting.StartTime - newFixationData.StartTime);
            }

            // Look for errors.
            if (newFixationData.Length == 0)
            {
              lengthConverterErrorCounter++;
              continue;
            }

            // Write fixation positions
            if (numPosXImportColumn != -1)
            {
              if (IOHelpers.IsNumeric(items[numPosXImportColumn]))
              {
                newFixationData.PosX = Convert.ToSingle(items[numPosXImportColumn], nfi);
              }
              else
              {
                newFixationData.PosX = 0;
              }
            }

            if (numPosYImportColumn != -1)
            {
              if (IOHelpers.IsNumeric(items[numPosYImportColumn]))
              {
                newFixationData.PosY = Convert.ToSingle(items[numPosYImportColumn], nfi);
              }
              else
              {
                newFixationData.PosY = 0;
              }
            }

            // Add the parsed raw data row to the list.
            fixationDataList.Add(newFixationData);

            // Increase counter
            counter++;

            // Cancel import, if only a part for preview should be imported.
            if (counter > numberOfImportLines && numberOfImportLines >= 0)
            {
              break;
            }
          }

          // reached end of file
        }

        // Show parsing errors.
        if (lengthConverterErrorCounter > 0)
        {
          StringBuilder message = new StringBuilder();
          message.Append(lengthConverterErrorCounter);
          message.Append(" fixation durations could not be parsed.");
          message.Append("These fixations are omitted from import.");

          ExceptionMethods.ProcessErrorMessage(message.ToString());
        }
      }
      catch (Exception ex)
      {
        ExceptionMethods.HandleException(ex);
      }
    }

    /// <summary>
    /// This method iterates the imported fixation rows to
    /// catch the trial changes that are detected during the call
    /// of <see cref="GenerateOgamaFixationDataList(int)"/>.
    /// The trials are then written into the trial list.
    /// </summary>
    private static void GenerateOgamaSubjectAndTrialList()
    {
      // Clear foregoing imports.
      trialList.Clear();
      subjectList.Clear();

      if (fixationDataList.Count == 0)
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
      int currentSequence = -5;
      int lastSequence = -5;
      string currentSubject = "#";
      string lastSubject = "#";
      int trialCounter = 0;

      // Iterate raw data list
      for (int i = 0; i < fixationDataList.Count; i++)
      {
        FixationData importRow = fixationDataList[i];
        currentSequence = importRow.TrialSequence;
        currentSubject = importRow.SubjectName;

        fixationDataList[i] = importRow;

        // If subject has changed write new subject table entry.
        if (currentSubject != lastSubject)
        {
          SubjectsData newSubjectsData = new SubjectsData();
          newSubjectsData.SubjectName = currentSubject;
          subjectList.Add(newSubjectsData);
          lastSubject = currentSubject;
        }

        // If trial has changed parse the trial information to 
        // create a trial entry in the trialList.
        if (currentSequence != lastSequence)
        {
          string subject = importRow.SubjectName != null ? importRow.SubjectName : "Subject1";

          // Create trial row
          TrialsData newTrialData = new TrialsData();
          newTrialData.SubjectName = subject;
          newTrialData.TrialSequence = currentSequence;
          newTrialData.TrialID = importRow.TrialID;
          newTrialData.TrialName = importRow.TrialID.ToString();
          trialList.Add(newTrialData);

          lastSequence = currentSequence;
          trialCounter++;
        }
      }
    }

    /// <summary>
    /// This method writes the fixational data that is written in the lists during
    /// import to OGAMAs dataset.
    /// If this could be succesfully done the whole new data is
    /// written to the database (.mdf).
    /// </summary>
    private static void SaveImportIntoTablesAndDB()
    {
      try
      {
        if (!Queries.WriteFixationListToDataSet(SampleType.Gaze, fixationDataList))
        {
          throw new DataException("The new fixation table could not be written into the dataset.");
        }

        // Update subjects and trials table in the mdf database
        int affectedRows = Document.ActiveDocument.DocDataSet.GazeFixationsAdapter.Update(
          Document.ActiveDocument.DocDataSet.GazeFixations);

        Document.ActiveDocument.DocDataSet.AcceptChanges();
      }
      catch (Exception ex)
      {
        ExceptionMethods.HandleException(ex);

        // CleanUp
        Document.ActiveDocument.DocDataSet.RejectChanges();
      }
      finally
      {
        fixationDataList.Clear();
      }
    }

    /// <summary>
    /// This method shows a dialog, that gives the option
    /// to use a import settings file during import.
    /// </summary>
    /// <remarks>This functionality is extremely useful, when multiple log
    /// files of the same type should be imported.</remarks>
    private static void AskforUsingSettingsFile()
    {
      DialogResult result =
        InformationDialog.Show("Use settings ?", "Would you like to use a settings file ?", true, MessageBoxIcon.Question);
      if (result == DialogResult.Yes)
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
          ImportFixations.DeserializeSettings(settingsFilename);
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
      SaveFileDialog ofdSaveSettings = new SaveFileDialog();
      ofdSaveSettings.DefaultExt = "ois";
      ofdSaveSettings.FileName = "*.ois";
      ofdSaveSettings.FilterIndex = 1;
      ofdSaveSettings.Filter = "Ogama import settings files|*.ois";
      ofdSaveSettings.Title = "Please specify settings filename";
      if (ofdSaveSettings.ShowDialog() == DialogResult.OK)
      {
        ImportFixations.SerializeSettings(ofdSaveSettings.FileName);
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
    /// <returns><strong>True</strong> if successful, 
    /// otherwise <strong>false</strong>.</returns>
    private static bool SerializeSettings(string filePath)
    {
      try
      {
        using (TextWriter writer = new StreamWriter(filePath))
        {
          MergedSettings settings = new MergedSettings();
          settings.AsciiSetting = asciiSetting;
          settings.DetectionSetting = detectionSetting;

          XmlSerializer serializer = new XmlSerializer(typeof(MergedSettings));
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
        using (FileStream fs = new FileStream(filePath, FileMode.Open))
        {
          MergedSettings settings = new MergedSettings();

          // Create an instance of the XmlSerializer class;
          // specify the type of object to be deserialized 
          XmlSerializer serializer = new XmlSerializer(typeof(MergedSettings));

          /* Use the Deserialize method to restore the object's state with
          data from the XML document. */
          settings = (MergedSettings)serializer.Deserialize(fs);

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
