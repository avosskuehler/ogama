// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ImportFixations.cs" company="Freie Universität Berlin">
//   OGAMA - open gaze and mouse analyzer 
//   Copyright (C) 2014 Dr. Adrian Voßkühler  
//   Licensed under GPL V3
// </copyright>
// <author>Adrian Voßkühler</author>
// <email>adrian@ogama.net</email>
// <summary>
//   Class for importing fixations through multiple dialogs.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Ogama.Modules.ImportExport.FixationData
{
  using System;
  using System.Collections.Generic;
  using System.Data;
  using System.Globalization;
  using System.IO;
  using System.Linq;
  using System.Text;
  using System.Windows.Forms;
  using System.Xml.Serialization;

  using Ogama.ExceptionHandling;
  using Ogama.MainWindow;
  using Ogama.Modules.Common.SlideCollections;
  using Ogama.Modules.Common.Tools;
  using Ogama.Modules.Common.Types;
  using Ogama.Modules.ImportExport.Common;
  using Ogama.Modules.ImportExport.RawData;

  /// <summary>
  ///   Class for importing fixations through multiple dialogs.
  /// </summary>
  /// <remarks>
  ///   Please note that the imported fixations are treated as
  ///   gaze fixations, mouse fixations are currently not importable.
  ///   This import interface is mainly created to enable the import
  ///   of gaze recorder generated fixations of people that do not
  ///   wan´t to rely on OGAMA´s internal fixation calculation algorithm.
  /// </remarks>
  public class ImportFixations
  {
    #region Static Fields

    /// <summary>
    ///   List to fill with imported and filtered <see cref="FixationData" />
    /// </summary>
    private static readonly List<FixationData> FixationDataList;

    /// <summary>
    ///   List to fill with generated <see cref="SubjectsData" />
    /// </summary>
    private static readonly List<SubjectsData> SubjectList;

    /// <summary>
    ///   List to fill with generated or imported <see cref="TrialsData" />
    /// </summary>
    private static readonly List<TrialsData> TrialList;

    /// <summary>
    ///   Saves the ASCII file import specialized settings
    ///   during this import session.
    /// </summary>
    private static ASCIISettings asciiSetting;

    /// <summary>
    ///   Saves the specialized settings used during this import session.
    /// </summary>
    private static DetectionSettings detectionSetting;

    #endregion

    #region Constructors and Destructors

    /// <summary>
    ///   Initializes static members of the ImportFixations class.
    /// </summary>
    static ImportFixations()
    {
      SubjectList = new List<SubjectsData>();
      TrialList = new List<TrialsData>();
      FixationDataList = new List<FixationData>();
      detectionSetting = new DetectionSettings();
      asciiSetting = new ASCIISettings();
    }

    #endregion

    #region Public Properties

    /// <summary>
    ///   Gets the ASCII file import specialized settings
    ///   during this import session.
    /// </summary>
    /// <value>A <see cref="ASCIISettings" />.</value>
    /// <seealso cref="ASCIISettings" />
    public static ASCIISettings ASCIISettings
    {
      get
      {
        return asciiSetting;
      }
    }

    /// <summary>
    ///   Gets the specialized settings used during this import session.
    /// </summary>
    /// <value>A <see cref="DetectionSettings" />.</value>
    /// <seealso cref="DetectionSettings" />
    public static DetectionSettings DetectionSetting
    {
      get
      {
        return detectionSetting;
      }
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// Generates the trial list for the current import settings
    ///   up to the number of lines that are given.
    /// </summary>
    /// <param name="numberOfImportLines">
    /// An <see cref="int"/>
    ///   with the max number of lines to import.
    ///   Set it to -1 to use all lines.
    /// </param>
    /// <returns>
    /// A <see cref="List{TrialsData}"/> with the calculated trials.
    /// </returns>
    public static List<TrialsData> GetTrialList(int numberOfImportLines)
    {
      // Convert the import file into ogama column format
      if (!GenerateOgamaFixationDataList(numberOfImportLines))
      {
        return null;
      }

      // Generate the trial list from the raw data with the current settings.
      GenerateOgamaSubjectAndTrialList();

      return TrialList;
    }

    /// <summary>
    ///   Starts a multiple dialog routine (fixations import assistant)
    ///   for reading fixation log files into OGAMAs database.
    /// </summary>
    public static void Start()//(MainForm mainWindow)
    {
      try
      {
        asciiSetting = new ASCIISettings();
        detectionSetting = new DetectionSettings();

        var objfrmImportAssistent = new ImportFixationsAssistentDialog();
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

            var objfrmImportReadFile = new ImportParseFileDialog(ref asciiSetting);
          ReadFile:
            DialogResult resultRawData = objfrmImportReadFile.ShowDialog();
            if (resultRawData == DialogResult.OK)
            {
              var objfrmImportFixationsAssignColumns = new ImportFixationsAssignColumnsDialog();

            MakeAssignments:
              DialogResult resultAssign = objfrmImportFixationsAssignColumns.ShowDialog();
              if (resultAssign == DialogResult.OK)
              {
                var objfrmImportTrials = new ImportTrialsDialog(ref asciiSetting, ref detectionSetting);

                //CheckTrials:
                DialogResult resultTrials = objfrmImportTrials.ShowDialog();
                if (resultTrials == DialogResult.OK)
                {
                  //var objfrmImportImages = new ImportImagesDialog(ref asciiSetting, ref detectionSetting);

                  //DialogResult resultImages = objfrmImportImages.ShowDialog();
                  //if (resultImages == DialogResult.OK)
                  //{
                  if (InformationDialog.Show(
                    "Save ?",
                    "Would you like to save the import settings ?",
                    true,
                    MessageBoxIcon.Question) == DialogResult.Yes)
                  {
                    SaveImportSettings();
                  }

                  // Inform user about deletion.
                  string cleanupMessage = "If you continue the import process, " + Environment.NewLine + "all existing "
                                   + "gaze fixation data is deleted before the imported fixations "
                                   + "are written to the database.";
                  if (MessageBox.Show(
                    cleanupMessage,
                    Application.ProductName,
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Information) == DialogResult.Cancel)
                  {
                    return;
                  }

                  // Show import splash window
                  asciiSetting.WaitingSplash.RunWorkerAsync();

                  // Give some time to show the splash ...
                  Application.DoEvents();

                  // Read log file again, but complete
                  if (!GenerateOgamaFixationDataList(-1))
                  {
                    asciiSetting.WaitingSplash.CancelAsync();
                    return;
                  }

                  // Generate the trials
                  GenerateOgamaSubjectAndTrialList();

                  // Save the import into ogamas database and the mdf file.
                  var successful = SaveImportIntoTablesAndDB();

                  //// Create slideshow trials
                  //ImportRawData.GenerateOgamaSlideshowTrials(detectionSetting, mainWindow);

                  // Import has finished.
                  asciiSetting.WaitingSplash.CancelAsync();

                  // Inform user about success.
                  if (successful)
                  {
                    string message = "Fixation data successfully written to database.";// + Environment.NewLine +
                    //"Please don´t forget to move the stimuli images to the SlideResources subfolder" +
                    //"of the experiment, otherwise no images will be shown.";
                    ExceptionMethods.ProcessMessage("Success", message);
                  }
                  else
                  {
                    string message = "Import had errors. Some or all of the import data " +
                      "could not be written the database.";
                    ExceptionMethods.ProcessErrorMessage(message);
                  }
                  //}
                  //else if (resultImages == DialogResult.Cancel)
                  //{
                  //  goto CheckTrials;
                  //}
                }
                else if (resultTrials == DialogResult.Cancel)
                {
                  goto MakeAssignments;
                }
                else if (resultTrials == DialogResult.Abort)
                {

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
                         + "Please try again with other settings." + Environment.NewLine + "Error: " + ex.Message;
        ExceptionMethods.ProcessErrorMessage(message);

        if (asciiSetting.WaitingSplash.IsBusy)
        {
          asciiSetting.WaitingSplash.CancelAsync();
        }
      }
    }

    #endregion

    #region Methods

    /// <summary>
    ///   This method shows a dialog, that gives the option
    ///   to use a import settings file during import.
    /// </summary>
    /// <remarks>
    ///   This functionality is extremely useful, when multiple log
    ///   files of the same type should be imported.
    /// </remarks>
    private static void AskforUsingSettingsFile()
    {
      DialogResult result = InformationDialog.Show(
        "Use settings ?",
        "Would you like to use a settings file ?",
        true,
        MessageBoxIcon.Question);
      if (result == DialogResult.Yes)
      {
        var ofdOpenSettings = new OpenFileDialog
                                {
                                  DefaultExt = "ois",
                                  FileName = "*.ois",
                                  FilterIndex = 1,
                                  //InitialDirectory = Properties.Settings.Default.ImportSettingsPath,
                                  Filter = "Ogama import settings files|*.ois",
                                  Title = "Please select import settings file ..."
                                };
        if (ofdOpenSettings.ShowDialog() == DialogResult.OK)
        {
          string settingsFilename = ofdOpenSettings.FileName;
          DeserializeSettings(settingsFilename);
          detectionSetting.SubjectName = "Subject1";
          detectionSetting.SavedSettings = true;
        }
      }
    }

    /// <summary>
    /// Reads an OGAMA import settings file.
    /// </summary>
    /// <param name="filePath">
    /// A <see cref="string"/> with the path to the
    ///   OGAMA import settings xml file.
    /// </param>
    private static void DeserializeSettings(string filePath)
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
      }
    }

    /// <summary>
    /// Generate the list of fixations under the current
    ///   parsing conditions.
    /// </summary>
    /// <param name="numberOfImportLines">
    /// An <see cref="int"/>
    ///   with the max number of lines to import.
    ///   Set it to -1 to use all lines.
    /// </param>
    /// <remarks>
    /// This is the heart of the class. If something does not work as expected,
    ///   first have a look here.
    /// </remarks>
    private static bool GenerateOgamaFixationDataList(int numberOfImportLines)
    {
      // Retrieve existing slideshow trials (to check matching filenames for 
      // correct trial ID numbering
      List<Trial> trials = Document.ActiveDocument.ExperimentSettings.SlideShow.Trials;
      List<string> trialNames = Document.ActiveDocument.ExperimentSettings.SlideShow.GetTrialNames();

      // Clear existing values
      FixationDataList.Clear();
      detectionSetting.ImageDictionary.Clear();

      // Use the decimal separator specified.
      NumberFormatInfo nfi = CultureInfo.GetCultureInfo("en-US").NumberFormat;
      if (asciiSetting.DecimalSeparatorCharacter == ',')
      {
        nfi = CultureInfo.GetCultureInfo("de-DE").NumberFormat;
      }

      // Enumerate the columns in the import file and assign their title.
      var columnsImportNum = new Dictionary<string, int>();
      for (int i = 0; i < asciiSetting.ColumnHeaders.Count; i++)
      {
        columnsImportNum.Add(asciiSetting.ColumnHeaders[i], i);
      }

      // Get the assigned titles of the import columns for Ogamas columns
      string strSubjectNameImportColumn = asciiSetting.ColumnAssignments["SubjectName"];
      string strTrialSequenceImportColumn = asciiSetting.ColumnAssignments["TrialSequence"];
      string strTrialIDImportColumn = asciiSetting.ColumnAssignments["TrialID"];
      string strTrialImageImportColumn = asciiSetting.ColumnAssignments["TrialImage"];
      string strCountInTrialImportColumn = asciiSetting.ColumnAssignments["CountInTrial"];
      string strStartTimeImportColumn = asciiSetting.ColumnAssignments["StartTime"];
      string strEndTimeImportColumn = asciiSetting.ColumnAssignments["EndTime"];
      string strLengthImportColumn = asciiSetting.ColumnAssignments["Length"];
      string strPosXImportColumn = asciiSetting.ColumnAssignments["PosX"];
      string strPosYImportColumn = asciiSetting.ColumnAssignments["PosY"];

      // Convert the names into column counters.
      int numSubjectImportColumn = strSubjectNameImportColumn == string.Empty
                                     ? -1
                                     : columnsImportNum[strSubjectNameImportColumn];
      int numTrialSequenceImportColumn = strTrialSequenceImportColumn == string.Empty
                                           ? -1
                                           : columnsImportNum[strTrialSequenceImportColumn];
      int numTrialIDImportColumn = strTrialIDImportColumn == string.Empty
                                     ? -1
                                     : columnsImportNum[strTrialIDImportColumn];
      int numTrialImageImportColumn = strTrialImageImportColumn == string.Empty ? -1 : columnsImportNum[strTrialImageImportColumn];
      int numCountInTrialImportColumn = strCountInTrialImportColumn == string.Empty
                                          ? -1
                                          : columnsImportNum[strCountInTrialImportColumn];
      int numStartTimeImportColumn = strStartTimeImportColumn == string.Empty
                                       ? -1
                                       : columnsImportNum[strStartTimeImportColumn];
      int numEndTimeImportColumn = strEndTimeImportColumn == string.Empty
                                     ? -1
                                     : columnsImportNum[strEndTimeImportColumn];
      int numLengthImportColumn = strLengthImportColumn == string.Empty ? -1 : columnsImportNum[strLengthImportColumn];
      int numPosXImportColumn = strPosXImportColumn == string.Empty ? -1 : columnsImportNum[strPosXImportColumn];
      int numPosYImportColumn = strPosYImportColumn == string.Empty ? -1 : columnsImportNum[strPosYImportColumn];

      // Create a list of starting times from the list of the trial dialog
      // if this mode is selected.
      if (detectionSetting.TrialImportMode == TrialSequenceImportModes.UseAssignmentTable)
      {
        // If first given trial start time does not start with zero,
        // add a "zero trial" for the very first samples
        if (detectionSetting.TrialSequenceToStarttimeAssignments.Count == 0)
        {
          // In this trial import modes an empty TrialIDToStarttimeAssignments
          // List is not allowed.
          return false;
        }
      }
      else
      {
        detectionSetting.TrialSequenceToTrialIDAssignments.Clear();
        detectionSetting.TrialSequenceToStarttimeAssignments.Clear();
      }

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
        using (var importReader = new StreamReader(asciiSetting.Filename))
        {
          // Clear old entries except parses from table
          if (detectionSetting.StimuliImportMode != StimuliImportModes.UseAssignmentTable)
          {
            detectionSetting.ImageDictionary.Clear();
            detectionSetting.TrialIDToImageAssignments.Clear();
          }

          // Read every line of ImportFile
          string line;
          while ((line = importReader.ReadLine()) != null)
          {
            // ignore empty lines
            if (line.Trim() == string.Empty)
            {
              continue;
            }

            // Ignore Quotes if applicable
            if (asciiSetting.IgnoreQuotes
                && line.Trim().Substring(0, asciiSetting.IgnoreQuotationString.Length)
                == asciiSetting.IgnoreQuotationString)
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
            if (asciiSetting.UseQuotes && !line.Contains(asciiSetting.UseQuotationString))
            {
              continue;
            }

            // ignore lines with ignore trigger
            if (asciiSetting.IgnoreTriggerStringLines && line.Contains(asciiSetting.IgnoreTriggerString))
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
            var newFixationData = new FixationData();

            // Calculate time value
            double timeInFileTime = Convert.ToDouble(items[numStartTimeImportColumn], nfi);
            long timeInMs = Convert.ToInt64(timeInFileTime * detectionSetting.TimeFactor);

            // Write count in trial
            if (numCountInTrialImportColumn != -1)
            {
              newFixationData.CountInTrial = IOHelpers.IsNumeric(items[numCountInTrialImportColumn]) ? Convert.ToInt32(items[numCountInTrialImportColumn], nfi) : counter;
            }
            else
            {
              newFixationData.CountInTrial = counter;
            }

            // Write subject name value
            newFixationData.SubjectName = numSubjectImportColumn != -1 ? items[numSubjectImportColumn] : detectionSetting.SubjectName;

            // Write Trial Sequence
            switch (detectionSetting.TrialImportMode)
            {
              case TrialSequenceImportModes.UseMSGLines:
                break;
              case TrialSequenceImportModes.UseAssignmentTable:
                if (!isLastTrial && timeInMs + asciiSetting.StartTime >= trial2Time.Values[trialCounter])
                {
                  trialCounter++;
                  if (trialCounter >= trial2Time.Count)
                  {
                    isLastTrial = true;
                  }
                }

                currentTrialSequence = trial2Time.Keys[trialCounter - 1];
                break;
              case TrialSequenceImportModes.UseImportColumn:
                if (numTrialSequenceImportColumn != -1)
                {
                  currentTrialSequence = Convert.ToInt32(items[numTrialSequenceImportColumn]);
                }

                break;
            }

            newFixationData.TrialSequence = currentTrialSequence;

            // Get trial start time from database
            long currentTrialStarttime = 0;
            try
            {
              var matchingTrial =
                Document.ActiveDocument.DocDataSet.TrialsAdapter.GetDataBySubjectAndSequence(
                  newFixationData.SubjectName,
                  newFixationData.TrialSequence);
              currentTrialStarttime = matchingTrial[0].TrialStartTime;
            }
            catch (Exception)
            {
              var message = string.Format(
                "There is no trial for this fixation data in the experiment. (Subject: {0}, TrialSequence: {1}, not found)" +
                Environment.NewLine + "Before importing fixations, ensure that the subject and trials are already imported into the database e.g. via raw data import."
                + Environment.NewLine + "The fixation import assistant will now close.",
                newFixationData.SubjectName,
                newFixationData.TrialSequence);
              ExceptionMethods.ProcessMessage("Subjects entry is missing", message);

              return false;
            }

            // Save starttime
            if (counter == 0 || (asciiSetting.ColumnTitlesAtFirstRow && counter == 1))
            {
              asciiSetting.StartTime = currentTrialStarttime;
              if (timeInMs > 0)
              {
                asciiSetting.StartTime = timeInMs;
              }
            }

            // Write trialID to detection settings.
            if (numTrialIDImportColumn != -1)
            {
              int? columnTrialID = Convert.ToInt32(items[numTrialIDImportColumn]);
              if (!detectionSetting.TrialSequenceToTrialIDAssignments.ContainsKey(currentTrialSequence))
              {
                detectionSetting.TrialSequenceToTrialIDAssignments.Add(currentTrialSequence, columnTrialID.Value);
              }
            }

            // Write TrialID
            if (numTrialIDImportColumn != -1)
            {
              newFixationData.TrialID = IOHelpers.IsNumeric(items[numTrialIDImportColumn]) ? Convert.ToInt32(items[numTrialIDImportColumn], nfi) : currentTrialSequence;
            }
            else
            {
              newFixationData.TrialID = currentTrialSequence;
            }


            // Save time value
            newFixationData.StartTime = timeInMs - currentTrialStarttime - asciiSetting.StartTime;

            // If starttimes are already relative to trial beginning
            if (newFixationData.StartTime < 0)
            {
              newFixationData.StartTime = timeInMs - asciiSetting.StartTime;
            }

            long endtime = 0;

            // Read ending time
            if (numEndTimeImportColumn != -1)
            {
              // Calculate time value
              double endtimeInFileTime = IOHelpers.IsNumeric(items[numEndTimeImportColumn]) ? Convert.ToDouble(items[numEndTimeImportColumn], nfi) : 0;
              endtime = Convert.ToInt64(endtimeInFileTime * detectionSetting.TimeFactor);
            }

            // Write fixation duration
            if (numLengthImportColumn != -1)
            {
              newFixationData.Length = IOHelpers.IsNumeric(items[numLengthImportColumn]) ? Convert.ToInt32(items[numLengthImportColumn], nfi) : 0;
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
              newFixationData.PosX = IOHelpers.IsNumeric(items[numPosXImportColumn]) ? Convert.ToSingle(items[numPosXImportColumn], nfi) : 0;
            }

            if (numPosYImportColumn != -1)
            {
              newFixationData.PosY = IOHelpers.IsNumeric(items[numPosYImportColumn]) ? Convert.ToSingle(items[numPosYImportColumn], nfi) : 0;
            }

            // Add the parsed raw data row to the list.
            FixationDataList.Add(newFixationData);

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
          var message = new StringBuilder();
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

      return true;
    }

    /// <summary>
    ///   This method iterates the imported fixation rows to
    ///   catch the trial changes that are detected during the call
    ///   of <see cref="GenerateOgamaFixationDataList(int)" />.
    ///   The trials are then written into the trial list.
    /// </summary>
    private static void GenerateOgamaSubjectAndTrialList()
    {
      // Clear foregoing imports.
      TrialList.Clear();
      SubjectList.Clear();

      if (FixationDataList.Count == 0)
      {
        return;
      }

      // Initializes variables
      var lastSequence = -5;
      var lastSubject = "#";

      // Iterate raw data list
      for (int i = 0; i < FixationDataList.Count; i++)
      {
        var importRow = FixationDataList[i];
        int currentSequence = importRow.TrialSequence;
        string currentSubject = importRow.SubjectName;

        FixationDataList[i] = importRow;

        // If subject has changed write new subject table entry.
        if (currentSubject != lastSubject)
        {
          var newSubjectsData = new SubjectsData { SubjectName = currentSubject };
          SubjectList.Add(newSubjectsData);
          lastSubject = currentSubject;
        }

        // If trial has changed parse the trial information to 
        // create a trial entry in the trialList.
        if (currentSequence != lastSequence)
        {
          string subject = importRow.SubjectName ?? "Subject1";

          // Create trial row
          var newTrialData = new TrialsData
                               {
                                 SubjectName = subject,
                                 TrialSequence = importRow.TrialSequence,
                                 TrialID = importRow.TrialID,
                                 TrialName = importRow.TrialID.ToString(CultureInfo.InvariantCulture)
                               };

          TrialList.Add(newTrialData);

          lastSequence = currentSequence;
        }
      }
    }

    /// <summary>
    ///   This method writes the fixation data that is written in the lists during
    ///   import to OGAMAs dataset.
    ///   If this could be successfully done the whole new data is
    ///   written to the database.
    /// </summary>
    /// <returns><strong>True</strong> if successful, otherwise
    /// <strong>false</strong>.</returns>
    private static bool SaveImportIntoTablesAndDB()
    {
      try
      {
        if (!Queries.WriteFixationListToDataSet(SampleType.Gaze, FixationDataList))
        {
          throw new DataException("The new fixation table could not be written into the dataset.");
        }

        // Update fixations table in the database
        Document.ActiveDocument.DocDataSet.GazeFixationsAdapter.Update(Document.ActiveDocument.DocDataSet.GazeFixations);
        Document.ActiveDocument.DocDataSet.AcceptChanges();
      }
      catch (Exception ex)
      {
        ExceptionMethods.HandleException(ex);

        // CleanUp
        Document.ActiveDocument.DocDataSet.RejectChanges();

        return false;
      }
      finally
      {
        FixationDataList.Clear();
      }

      return true;
    }

     /// <summary>
    ///   This method shows a dialog asking for saving the current
    ///   import settings to hard disk.
    ///   They are persisted in xml format.
    /// </summary>
    private static void SaveImportSettings()
    {
      var ofdSaveSettings = new SaveFileDialog
                              {
                                DefaultExt = "ois",
                                FileName = "*.ois",
                                FilterIndex = 1,
                                Filter = "Ogama import settings files|*.ois",
                                Title = "Please specify settings filename"
                              };
      if (ofdSaveSettings.ShowDialog() == DialogResult.OK)
      {
        SerializeSettings(ofdSaveSettings.FileName);
      }
    }

    /// <summary>
    /// Saves the current import setting to a OGAMA import settings file.
    ///   Extension "ois"
    /// </summary>
    /// <param name="filePath">
    /// A <see cref="string"/> with the path to the
    ///   OGAMA target import settings xml file.
    /// </param>
    private static void SerializeSettings(string filePath)
    {
      try
      {
        using (TextWriter writer = new StreamWriter(filePath))
        {
          var settings = new MergedSettings { AsciiSetting = asciiSetting, DetectionSetting = detectionSetting };
          var serializer = new XmlSerializer(typeof(MergedSettings));
          serializer.Serialize(writer, settings);
        }
      }
      catch (Exception ex)
      {
        ExceptionMethods.HandleException(ex);
      }
    }

    #endregion
  }
}