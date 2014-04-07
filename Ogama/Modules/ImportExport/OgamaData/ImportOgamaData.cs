// <copyright file="ImportOgamaData.cs" company="FU Berlin">
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

namespace Ogama.Modules.ImportExport.OgamaData
{
  using System;
  using System.Collections.Generic;
  using System.Data;
  using System.Globalization;
  using System.IO;
  using System.Windows.Forms;

  using Ogama.ExceptionHandling;
  using Ogama.Modules.Common.Tools;
  using Ogama.Modules.ImportExport.Common;

  /// <summary>
  /// Class for importing raw data through multiple dialogs.
  /// </summary>
  public class ImportOgamaData
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
    /// List to fill with generated or imported <see cref="TrialEventsData"/>
    /// </summary>
    private static List<TrialEventsData> trialEventsList;

    /// <summary>
    /// List to fill with generated <see cref="SubjectsData"/>
    /// </summary>
    private static List<SubjectsData> subjectList;

    /// <summary>
    /// Saves the ASCII file import specialized settings
    /// during this import session.
    /// </summary>
    private static ASCIISettings asciiSettings;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes static members of the ImportOgamaData class.
    /// </summary>
    static ImportOgamaData()
    {
      subjectList = new List<SubjectsData>();
      trialList = new List<TrialsData>();
      trialEventsList = new List<TrialEventsData>();
      rawDataList = new List<RawData>();
      asciiSettings = new ASCIISettings();
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets the ASCII file import specialized settings
    /// during this import session.
    /// </summary>
    /// <value>A <see cref="ASCIISettings"/>.</value>
    /// <seealso cref="ASCIISettings"/>
    public static ASCIISettings ASCIISettings
    {
      get { return asciiSettings; }
    }

    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Public methods                                                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region PUBLICMETHODS

    /// <summary>
    /// Opens a file dialog to choose the ogamas export file that 
    /// should be imported into the programs database.
    /// </summary>
    public static void Start()
    {
      try
      {
        asciiSettings = new ASCIISettings();

        if (asciiSettings.FileDialog.ShowDialog() == DialogResult.OK)
        {
          // Save filename
          string filename = asciiSettings.FileDialog.FileName;

          // Save import file
          asciiSettings.Filename = filename;

          NumberFormatInfo nfi = CultureInfo.CurrentCulture.NumberFormat;

          // Set locale separator character
          asciiSettings.DecimalSeparatorCharacter = nfi.NumberDecimalSeparator.ToCharArray()[0];

          // Show import splash window
          asciiSettings.WaitingSplash.RunWorkerAsync();

          // Give some time to show the splash ...
          Application.DoEvents();

          // Generate the import tables
          GenerateSubjectTrialRawdataList();

          // Save the import into ogamas database and the mdf file.
          var successful = SaveImportIntoTablesAndDB();

          // Import has finished.
          asciiSettings.WaitingSplash.CancelAsync();

          // Inform user about success.
          if (successful)
          {
            ExceptionMethods.ProcessMessage("Success", "Import data successfully written to database.");
          }
          else
          {
            string message = "Import had errors. Some or all of the import data " +
              "could not be written the database.";
            ExceptionMethods.ProcessErrorMessage(message);
          }
        }
      }
      catch (Exception ex)
      {
        string message = "Something failed during import." + Environment.NewLine
          + "Please try again with other settings. " + Environment.NewLine +
          "Error: " + ex.Message;
        ExceptionMethods.ProcessErrorMessage(message);

        if (asciiSettings.WaitingSplash.IsBusy)
        {
          asciiSettings.WaitingSplash.CancelAsync();
        }
      }
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
    /// Generate all the lists of subject, trial and raw data.
    /// </summary>
    /// <remarks>This is the heart of the class. If something does not work as expected,
    /// first have a look here.</remarks>
    private static void GenerateSubjectTrialRawdataList()
    {
      rawDataList.Clear();
      trialList.Clear();
      subjectList.Clear();
      trialEventsList.Clear();

      // Use the decimal separator specified.
      NumberFormatInfo nfi = CultureInfo.GetCultureInfo("en-US").NumberFormat;
      if (asciiSettings.DecimalSeparatorCharacter == ',')
      {
        nfi = CultureInfo.GetCultureInfo("de-DE").NumberFormat;
      }

      // Begin reading File
      string lastSubject = string.Empty;
      int lastTrialID = -5;
      string line = string.Empty;
      int counter = 0;
      try
      {
        // Open file
        using (StreamReader importReader = new StreamReader(asciiSettings.Filename))
        {
          string[] columns;
          Dictionary<string, int> columnNames = new Dictionary<string, int>();

          // Read every line of ImportFile
          while ((line = importReader.ReadLine()) != null)
          {
            // ignore empty lines
            if (line.Trim() == string.Empty)
            {
              continue;
            }

            // Ignore Quotes 
            if (line.StartsWith("#"))
            {
              continue;
            }

            // Skip first line filled with column titles
            if (counter == 0)
            {
              columns = line.Split('\t');
              for (int i = 0; i < columns.Length; i++)
              {
                columnNames.Add(columns[i], i);
              }

              counter++;
              continue;
            }

            // Split line in columns
            string[] items = line.Split('\t');

            // read subjects data
            string subjectName = items[columnNames["SubjectName"]];
            if (subjectName != lastSubject)
            {
              SubjectsData newSubjectData = new SubjectsData();
              newSubjectData.SubjectName = subjectName;
              newSubjectData.Category = items[columnNames["SubjectCategory"]];
              int result;
              if (int.TryParse(items[columnNames["Age"]], out result))
              {
                newSubjectData.Age = result;
              }

              newSubjectData.Sex = items[columnNames["Sex"]];
              newSubjectData.Handedness = items[columnNames["Handedness"]];
              newSubjectData.Comments = items[columnNames["Comments"]];

              if (!subjectList.Contains(newSubjectData))
              {
                subjectList.Add(newSubjectData);
              }

              lastSubject = subjectName;
            }

            // read trials data
            int trialID = Convert.ToInt32(items[columnNames["TrialID"]]);
            if (trialID != lastTrialID)
            {
              TrialsData newTrialsData = new TrialsData();
              newTrialsData.SubjectName = subjectName;
              newTrialsData.TrialID = trialID;
              newTrialsData.TrialName = items[columnNames["TrialName"]];
              newTrialsData.TrialSequence = Convert.ToInt32(items[columnNames["TrialSequence"]]);
              newTrialsData.Category = items[columnNames["TrialCategory"]];
              newTrialsData.TrialStartTime = Convert.ToInt64(items[columnNames["TrialStartTime"]]);
              int result;
              if (int.TryParse(items[columnNames["Duration"]], out result))
              {
                newTrialsData.Duration = result;
              }

              if (items[columnNames["EliminateData"]] != string.Empty)
              {
                newTrialsData.EliminateData = true;
              }

              if (!trialList.Contains(newTrialsData))
              {
                trialList.Add(newTrialsData);
              }

              lastTrialID = trialID;
            }

            // read trials data
            string eventeventID = items[columnNames["EventEventID"]];
            if (eventeventID != string.Empty)
            {
              TrialEventsData newTrialEventsData = new TrialEventsData();
              newTrialEventsData.EventID = Convert.ToInt32(items[columnNames["EventEventID"]]);
              newTrialEventsData.EventParam = items[columnNames["EventParam"]];
              newTrialEventsData.EventTask = items[columnNames["EventTask"]];
              newTrialEventsData.EventTime = Convert.ToInt64(items[columnNames["EventTime"]]);
              newTrialEventsData.EventType = items[columnNames["EventType"]];
              newTrialEventsData.SubjectName = subjectName;
              newTrialEventsData.TrialSequence = Convert.ToInt32(items[columnNames["TrialSequence"]]);
              if (!trialEventsList.Contains(newTrialEventsData))
              {
                trialEventsList.Add(newTrialEventsData);
              }
            }

            // Create Ogama columns placeholder
            RawData newRawData = new RawData();

            // Save time value
            newRawData.Time = Convert.ToInt64(items[columnNames["Time"]]);
            newRawData.SubjectName = subjectName;
            newRawData.TrialSequence = Convert.ToInt32(items[columnNames["TrialSequence"]]);
            newRawData.Category = items[columnNames["TrialCategory"]];

            if (items[columnNames["PupilDiaX"]] != string.Empty)
            {
              newRawData.PupilDiaX = Convert.ToSingle(items[columnNames["PupilDiaX"]], nfi);
            }

            if (items[columnNames["PupilDiaY"]] != string.Empty)
            {
              newRawData.PupilDiaY = Convert.ToSingle(items[columnNames["PupilDiaY"]], nfi);
            }

            if (items[columnNames["GazePosX"]] != string.Empty)
            {
              newRawData.GazePosX = Convert.ToSingle(items[columnNames["GazePosX"]], nfi);
            }

            if (items[columnNames["GazePosY"]] != string.Empty)
            {
              newRawData.GazePosY = Convert.ToSingle(items[columnNames["GazePosY"]], nfi);
            }

            if (items[columnNames["MousePosX"]] != string.Empty)
            {
              newRawData.MousePosX = Convert.ToSingle(items[columnNames["MousePosX"]], nfi);
            }

            if (items[columnNames["MousePosY"]] != string.Empty)
            {
              newRawData.MousePosY = Convert.ToSingle(items[columnNames["MousePosY"]], nfi);
            }

            if (items[columnNames["EventID"]] != string.Empty)
            {
              newRawData.EventID = Convert.ToInt32(items[columnNames["EventID"]]);
            }

            // Add the parsed raw data row to the list.
            rawDataList.Add(newRawData);
          }
        }
      }
      catch (Exception ex)
      {
        ExceptionMethods.HandleException(ex);
      }
    }

    /// <summary>
    /// This method writes the data that is written in the lists during
    /// import to OGAMAs dataset.
    /// If this could be successfully done the whole new data is
    /// written to the database (.mdf).
    /// </summary>
    /// <returns><strong>True</strong> if successful, otherwise
    /// <strong>false</strong>.</returns>
    private static bool SaveImportIntoTablesAndDB()
    {
      Dictionary<string, List<RawData>> rawDataBySubject = SplitRawDataListBySubjects(rawDataList);
      Dictionary<string, List<TrialsData>> trialDataBySubject = SplitTrialDataListBySubjects(trialList);
      Dictionary<string, List<TrialEventsData>> trialEventsDataBySubject = SplitTrialEventsDataListBySubjects(trialEventsList);

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
          List<TrialEventsData> subjectTrialEventsData = trialEventsDataBySubject[subject.SubjectName];
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

          // Save trial information to dataset
          if (!Queries.WriteTrialEventsDataListToDataSet(subjectTrialEventsData))
          {
            throw new DataException("The new trials table could not be written into the dataset.");
          }
        }

        // Update subjects and trials table in the mdf database
        int affectedRows = Document.ActiveDocument.DocDataSet.TrialEventsAdapter.Update(
         Document.ActiveDocument.DocDataSet.TrialEvents);
        affectedRows = Document.ActiveDocument.DocDataSet.TrialsAdapter.Update(
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
        subjectList.Clear();
        trialList.Clear();
        rawDataList.Clear();
        trialEventsList.Clear();
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

      if (wholeRawDataList.Count == 0)
      {
        throw new ArgumentException("RawDataList was empty");
      }

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
    /// This method splits the given trial events data list into a 
    /// dictionary of trial event data lists separated by subjects.
    /// </summary>
    /// <param name="wholeTrialEventsList">A <see cref="List{TrialEventsData}"/>
    /// with all the imported samples.</param>
    /// <returns>A Dictionary with the splitted input.</returns>
    private static Dictionary<string, List<TrialEventsData>> SplitTrialEventsDataListBySubjects(List<TrialEventsData> wholeTrialEventsList)
    {
      // Create the return dictionary
      Dictionary<string, List<TrialEventsData>> trialEventsDataBySubject = new Dictionary<string, List<TrialEventsData>>();

      // Get First subject name
      string lastSubjectName = wholeTrialEventsList[0].SubjectName;

      // Create list for current subject
      List<TrialEventsData> currentList = new List<TrialEventsData>();

      // Iterate whole raw data list and add for each subject a 
      // new entry in the trialEventsDataBySubject list.
      foreach (TrialEventsData data in wholeTrialEventsList)
      {
        if (data.SubjectName != lastSubjectName)
        {
          trialEventsDataBySubject.Add(lastSubjectName, currentList);
          currentList = new List<TrialEventsData>();
          lastSubjectName = data.SubjectName;
        }

        currentList.Add(data);
      }

      // Add last subject
      trialEventsDataBySubject.Add(lastSubjectName, currentList);

      // Return list.
      return trialEventsDataBySubject;
    }

    #endregion //METHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER
    #endregion //HELPER
  }
}
