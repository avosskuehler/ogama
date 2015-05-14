// <copyright file="Queries.cs" company="FU Berlin">
// ******************************************************
// OGAMA - open gaze and mouse analyzer 
// Copyright (C) 2015 Dr. Adrian Voßkühler  
// ------------------------------------------------------------------------
// This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
// This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
// You should have received a copy of the GNU General Public License along with this program; if not, write to the Free Software Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
// **************************************************************
// </copyright>
// <author>Adrian Voßkühler</author>
// <email>adrian@ogama.net</email>

namespace Ogama.Modules.Common.Tools
{
  using System;
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.Data;
  using System.Data.SqlClient;
  using System.Data.SQLite;
  using System.Drawing;
  using System.Linq;
  using System.Text;
  using System.Windows.Forms;

  using Ogama.DataSet;
  using Ogama.ExceptionHandling;
  using Ogama.Modules.Common.TrialEvents;
  using Ogama.Modules.Common.Types;
  using Ogama.Modules.ImportExport.Common;

  using VectorGraphics.Elements;
  using VectorGraphics.StopConditions;
  using VectorGraphics.Tools.CustomTypeConverter;

  /// <summary>
  /// This static class stores methods with OGAMAs custom database queries,
  /// that could not be automatically generated with the Database designer
  /// of Visual Studio.
  /// </summary>
  public static class Queries
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
    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES
    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION
    #endregion //CONSTRUCTION

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
    /// Check for method (method) existence in given class (name)
    /// </summary>
    /// <param name="name">The class to check for the method.</param>
    /// <param name="method">The methods name to be checked for.</param>
    /// <returns>True if method in class exists, otherwise false.</returns>
    public static bool MethodExists(string name, string method)
    {
      try
      {
        // Generate Name of from from given data
        string ns = Application.ProductName;
        string formName = ns + "." + name;
        Type formType = Type.GetType(formName);

        // Check if method (method) exists in given form (name)
        foreach (System.Reflection.MethodInfo methodCurrent in formType.GetMethods())
        {
          if (methodCurrent.Name.Equals(method))
          {
            return true;
          }
        }
      }
      catch
      {
      }

      return false;
    }

    #region EventQueries

    ////public static Dictionary<EventType, List<TrialEvent>> GetTrialEvents(string subjectName, int trialSequence)
    ////{
    ////  Dictionary<EventType, List<TrialEvent>> returnList = new Dictionary<EventType, List<TrialEvent>>();

    ////  SortedList<int, TrialEvent> imageEvents = new SortedList<int, TrialEvent>();
    ////  SortedList<int, TrialEvent> flashEvents = new SortedList<int, TrialEvent>();
    ////  SortedList<int, TrialEvent> audioEvents = new SortedList<int, TrialEvent>();
    ////  SortedList<int, TrialEvent> videoEvents = new SortedList<int, TrialEvent>();
    ////  SortedList<int, TrialEvent> usercamEvents = new SortedList<int, TrialEvent>();
    ////  SortedList<int, TrialEvent> mouseEvents = new SortedList<int, TrialEvent>();
    ////  SortedList<int, TrialEvent> keyEvents = new SortedList<int, TrialEvent>();

    ////  DataTable table =
    ////    Document.ActiveDocument.DocDataSet.TadTrialEvents.GetDataBySubjectAndSequence(subjectName, trialSequence);

    ////  foreach (DataRow row in table.Rows)
    ////  {
    ////    string typeString = row["EventType"].ToString();
    ////    EventType type = EventType.None;
    ////    try
    ////    {
    ////      type = (EventType)Enum.Parse(typeof(EventType), typeString, true);
    ////    }
    ////    catch (ArgumentException)
    ////    {
    ////      continue;
    ////    }
    ////    int id = Convert.ToInt32(row["EventID"]);
    ////    long time = Convert.ToInt64(row["EventTime"]);
    ////    string taskString = row["EventTask"].ToString();
    ////    string param = row["EventParam"].ToString();

    ////    TrialEvent newEvent = null;

    ////    switch (type)
    ////    {
    ////      case EventType.Mouse:
    ////        newEvent = new InputEvent();
    ////        newEvent.ID = id;
    ////        newEvent.Param = param;
    ////        ((InputEvent)newEvent).Task = (InputEventTask)Enum.Parse(typeof(InputEventTask), taskString, true);
    ////        newEvent.Time = time;
    ////        newEvent.Type = type;
    ////        mouseEvents.Add(newEvent);
    ////        break;
    ////      case EventType.Key:
    ////        newEvent = new InputEvent();
    ////        newEvent.ID = id;
    ////        newEvent.Param = param;
    ////        ((InputEvent)newEvent).Task = (InputEventTask)Enum.Parse(typeof(InputEventTask), taskString, true);
    ////        newEvent.Time = time;
    ////        newEvent.Type = type;
    ////        keyEvents.Add(newEvent);
    ////        break;
    ////      case EventType.Slide:
    ////        newEvent = new MediaEvent();
    ////        newEvent.ID = id;
    ////        newEvent.Param = param;
    ////        ((MediaEvent)newEvent).Task = (MediaEventTask)Enum.Parse(typeof(MediaEventTask), taskString, true);
    ////        newEvent.Time = time;
    ////        newEvent.Type = type;
    ////        imageEvents.Add(newEvent);
    ////        break;
    ////      case EventType.Flash:
    ////        newEvent = new MediaEvent();
    ////        newEvent.ID = id;
    ////        newEvent.Param = param;
    ////        ((MediaEvent)newEvent).Task = (MediaEventTask)Enum.Parse(typeof(MediaEventTask), taskString, true);
    ////        newEvent.Time = time;
    ////        newEvent.Type = type;
    ////        flashEvents.Add(newEvent);
    ////        break;
    ////      case EventType.Audio:
    ////        newEvent = new MediaEvent();
    ////        newEvent.ID = id;
    ////        newEvent.Param = param;
    ////        ((MediaEvent)newEvent).Task = (MediaEventTask)Enum.Parse(typeof(MediaEventTask), taskString, true);
    ////        newEvent.Time = time;
    ////        newEvent.Type = type;
    ////        audioEvents.Add(newEvent);
    ////        break;
    ////      case EventType.Video:
    ////        newEvent = new MediaEvent();
    ////        newEvent.ID = id;
    ////        newEvent.Param = param;
    ////        ((MediaEvent)newEvent).Task = (MediaEventTask)Enum.Parse(typeof(MediaEventTask), taskString, true);
    ////        newEvent.Time = time;
    ////        newEvent.Type = type;
    ////        videoEvents.Add(newEvent);
    ////        break;
    ////      case EventType.Usercam:
    ////        newEvent = new MediaEvent();
    ////        newEvent.ID = id;
    ////        newEvent.Param = param;
    ////        ((MediaEvent)newEvent).Task = (MediaEventTask)Enum.Parse(typeof(MediaEventTask), taskString, true);
    ////        newEvent.Time = time;
    ////        newEvent.Type = type;
    ////        usercamEvents.Add(newEvent);
    ////        break;
    ////    }

    ////  }

    ////  returnList.Add(EventType.Audio, audioEvents);
    ////  returnList.Add(EventType.Flash, flashEvents);
    ////  returnList.Add(EventType.Slide, imageEvents);
    ////  returnList.Add(EventType.Key, keyEvents);
    ////  returnList.Add(EventType.Mouse, mouseEvents);
    ////  returnList.Add(EventType.Usercam, usercamEvents);

    ////  return returnList;
    ////}

    /// <summary>
    /// This method parses the trial events table and returns it as a 
    /// <see cref="SortedList{Int32, TrialEvent}"/>.
    /// </summary>
    /// <param name="subjectName">A <see cref="String"/> with the subject name.</param>
    /// <param name="usercamID">Out. The event id of the user camera start time.</param>
    /// <returns>A <see cref="SortedList{Int32, TrialEvent}"/>
    /// with the events occured in this experiment.</returns>
    public static SortedList<int, TrialEvent> GetTrialEvents(string subjectName, out int usercamID)
    {
      DataTable table =
        Document.ActiveDocument.DocDataSet.TrialEventsAdapter.GetDataBySubject(subjectName);

      return ExtractEvents(subjectName, table, out usercamID);
    }

    /// <summary>
    /// This method parses the trial events table and returns it as a 
    /// <see cref="SortedList{Int32, TrialEvent}"/>.
    /// </summary>
    /// <param name="subjectName">A <see cref="String"/> with the subject name.</param>
    /// <param name="trialSequence">The <see cref="Int32"/> with the sequence number
    /// of this trial for the given subject.</param>
    /// <param name="usercamID">Out. The event id of the user camera start time.</param>
    /// <returns>A <see cref="SortedList{Int32, TrialEvent}"/>
    /// with the events occured in this trial.</returns>
    public static SortedList<int, TrialEvent> GetTrialEvents(
      string subjectName,
      int trialSequence,
      out int usercamID)
    {
      DataTable table =
        Document.ActiveDocument.DocDataSet.TrialEventsAdapter.GetDataBySubjectAndSequence(subjectName, trialSequence);
      return ExtractEvents(subjectName, table, out usercamID);
    }

    /// <summary>
    /// This method parses the trial event table for the mouse events made by the given
    /// subject during the trial with the given sequence number
    /// </summary>
    /// <param name="subjectName">A <see cref="String"/> with the subject name.</param>
    /// <param name="trialSequence">The <see cref="Int32"/> with the sequence number
    /// of this trial for the given subject.</param>
    /// <returns>A <see cref="SortedList{Int64, InputEvent}"/> with the mouse events made by the given
    /// subject during the trial with the given sequence number.</returns>
    public static SortedList<long, InputEvent> GetTrialMouseEvents(
      string subjectName,
      int trialSequence)
    {
      SortedList<long, InputEvent> returnList = new SortedList<long, InputEvent>();

      DataTable table =
        Document.ActiveDocument.DocDataSet.TrialEventsAdapter.GetDataBySubjectAndSequence(subjectName, trialSequence);

      foreach (DataRow row in table.Rows)
      {
        string typeString = row["EventType"].ToString();
        EventType type = EventType.None;
        try
        {
          type = (EventType)Enum.Parse(typeof(EventType), typeString, true);
        }
        catch (ArgumentException)
        {
          continue;
        }

        int id = Convert.ToInt32(row["EventID"]);
        long time = Convert.ToInt64(row["EventTime"]);
        string taskString = row["EventTask"].ToString();
        string param = row["EventParam"].ToString();

        InputEvent newEvent = null;

        switch (type)
        {
          case EventType.Mouse:
            newEvent = new InputEvent();
            newEvent.EventID = id;
            newEvent.Param = param;
            ((InputEvent)newEvent).Task = (InputEventTask)Enum.Parse(typeof(InputEventTask), taskString, true);
            newEvent.Time = time;
            newEvent.Type = type;
            if (!returnList.ContainsKey(time))
            {
              returnList.Add(time, newEvent);
            }

            break;
          case EventType.Response:
            newEvent = new InputEvent();
            newEvent.EventID = id;
            newEvent.Param = param;
            ((InputEvent)newEvent).Task = (InputEventTask)Enum.Parse(typeof(InputEventTask), taskString, true);
            newEvent.Time = time;
            newEvent.Type = type;
            StopCondition stc = null;

            try
            {
              stc = (StopCondition)TypeDescriptor.GetConverter(typeof(StopCondition)).ConvertFrom(param);
            }
            catch (Exception)
            {
              // Ignore errors because we just want to catch MouseStopConditions
            }

            if (stc != null & stc is MouseStopCondition & !returnList.ContainsKey(time))
            {
              returnList.Add(time, newEvent);
            }

            break;
        }
      }

      return returnList;
    }

    /// <summary>
    /// This method parses the trial event table for the response made by the given
    /// subject at the end of the trial with the given sequence number
    /// </summary>
    /// <param name="subjectName">A <see cref="String"/> with the subject name.</param>
    /// <param name="trialSequence">The <see cref="Int32"/> with the sequence number
    /// of this trial for the given subject.</param>
    /// <returns>A <see cref="StopCondition"/> with the response made by the given
    /// subject at the end of the trial with the given sequence number.</returns>
    public static StopCondition GetTrialResponse(
      string subjectName,
      int trialSequence)
    {
      StopCondition returnValue = null;

      DataTable table =
        Document.ActiveDocument.DocDataSet.TrialEventsAdapter.GetDataBySubjectAndSequence(subjectName, trialSequence);

      foreach (DataRow row in table.Rows)
      {
        string typeString = row["EventType"].ToString();
        string param = row["EventParam"].ToString();
        string task = row["EventTask"].ToString();

        EventType type = EventType.None;
        try
        {
          type = (EventType)Enum.Parse(typeof(EventType), typeString, true);
        }
        catch (ArgumentException)
        {
          continue;
        }

        switch (type)
        {
          case EventType.Response:
            StopCondition condition;
            try
            {
              condition = (StopCondition)TypeDescriptor.GetConverter(typeof(StopCondition)).ConvertFrom(param);
            }
            catch (ArgumentException)
            {
              continue;
            }

            if (condition != null)
            {
              returnValue = condition;
            }

            return returnValue;
        }
      }

      return returnValue;
    }

    /// <summary>
    /// Updates the marker position of the given <see cref="TrialEvent"/>
    /// </summary>
    /// <param name="trialEvent">The <see cref="TrialEvent"/>
    /// to be updated.</param>
    public static void UpdateMarkerEventByID(TrialEvent trialEvent)
    {
      try
      {
        string expression = "SubjectName = '" + trialEvent.SubjectName + "' AND TrialSequence = '" +
          trialEvent.TrialSequence.ToString() + "' AND EventID = '" +
          trialEvent.EventID.ToString() + "'";
        DataRow[] foundRows;

        // Use the Select method to find all rows matching the filter.
        foundRows = Document.ActiveDocument.DocDataSet.TrialEvents.Select(expression);
        if (foundRows.Length > 0)
        {
          foreach (DataRow row in foundRows)
          {
            row["EventTime"] = trialEvent.Time;
          }
        }

        // Update Database
        int affectedRows =
          Document.ActiveDocument.DocDataSet.TrialEventsAdapter.Update(Document.ActiveDocument.DocDataSet.TrialEvents);
      }
      catch (Exception ex)
      {
        ExceptionMethods.HandleException(ex);
      }
    }

    /// <summary>
    /// Deletes the given <see cref="TrialEvent"/> marker
    /// </summary>
    /// <param name="trialEvent">The <see cref="TrialEvent"/>
    /// marker to be deleted.</param>
    public static void DeleteMarkerEventByID(TrialEvent trialEvent)
    {
      try
      {
        string expression = "SubjectName = '" + trialEvent.SubjectName + "' AND TrialSequence = '" +
          trialEvent.TrialSequence.ToString() + "' AND EventID = '" +
          trialEvent.EventID.ToString() + "'";
        DataRow[] foundRows;

        // Use the Select method to find all rows matching the filter.
        foundRows = Document.ActiveDocument.DocDataSet.TrialEvents.Select(expression);
        if (foundRows.Length > 0)
        {
          foreach (DataRow row in foundRows)
          {
            row.Delete();
          }
        }

        // Update Database
        int affectedRows =
          Document.ActiveDocument.DocDataSet.TrialEventsAdapter.Update(Document.ActiveDocument.DocDataSet.TrialEvents);
      }
      catch (Exception ex)
      {
        ExceptionMethods.HandleException(ex);
      }
    }

    #endregion //EventQueries

    #region SampleChecks

    /// <summary>
    /// Reads sampling data from correct columns for gaze data.
    /// </summary>
    /// <param name="row">DataTable row with sampling data</param>
    /// <param name="stimulusSize">The size of the stimulus..</param>
    /// <param name="newPt">Out. Retrieves new sampling point.</param>
    /// <returns>A <see cref="SampleValidity"/> value indidacting the state of the 
    /// gaze data validity.</returns>
    public static SampleValidity GetGazeData(DataRow row, Size stimulusSize, out PointF? newPt)
    {
      int numRow = 6;
      return GetSampleData(row, numRow, stimulusSize, out newPt);
    }

    /// <summary>
    /// Reads sampling data from correct columns for mouse data.
    /// </summary>
    /// <param name="row">DataTable row with sampling data</param>
    /// <param name="stimulusSize">The size of the stimulus..</param>
    /// <param name="newPt">Out. Retrieves new sampling point.</param>
    /// <returns>A <see cref="SampleValidity"/> value indidacting the state of the 
    /// mouse data validity.</returns>
    public static SampleValidity GetMouseData(DataRow row, Size stimulusSize, out PointF? newPt)
    {
      int numRow = 8;
      return GetSampleData(row, numRow, stimulusSize, out newPt);
    }

    /// <summary>
    /// Returns true if given <see cref="PointF"/> is out of the rectangle given from
    /// <see cref="Properties.ExperimentSettings"/> size of eye monitor.
    /// </summary>
    /// <param name="pt">PointF to check</param>
    /// <param name="stimulusSize">The size of the stimulus..</param>
    /// <returns><strong>True</strong> if given point is in stimulus screen
    /// rectangle, otherwise <strong>false</strong>.</returns>
    public static bool OutOfScreen(PointF pt, Size stimulusSize)
    {
      if (pt.X > stimulusSize.Width || pt.X < 0 ||
          pt.Y > stimulusSize.Height || pt.Y < 0 ||
          (pt.X == 0 && pt.Y == 0))
      {
        return true;
      }
      else
      {
        return false;
      }
    }

    #endregion //SampleChecks

    #region DatabaseUpdateQueries

    /// <summary>
    /// Updates row in raw data table from given subject and ID
    /// with data given in parameter row.
    /// Uses given subjects raw data table.
    /// </summary>
    /// <param name="row">A <see cref="DataRow"/> with the new values</param>
    /// <param name="subject">A <see cref="string"/> with the subject name to which the data belongs.</param>
    /// <param name="originalID">A <see cref="int"/> with the ID of the raw data row.</param>
    /// <returns>A <see cref="int"/> with the number of updated rows.</returns>
    /// <exception cref="ArgumentNullException">Thrown, when row or subject is null or empty.</exception>
    public static int UpdateRowBySubjectAndID(DataRow row, string subject, int originalID)
    {
      if (row == null)
      {
        throw new ArgumentNullException();
      }

      if (subject == string.Empty)
      {
        throw new ArgumentNullException();
      }

      SQLiteDataAdapter adapter = Document.ActiveDocument.DocDataSet.RawDataAdapterDict[subject];

      // Create the SelectCommand.
      var command = new SQLiteCommand(
        "UPDATE [" + subject +
        "Rawdata] SET [SubjectName] = @SubjectName, " +
          "[TrialSequence] = @TrialSequence , [Time] = @Time, [PupilDiaX] = @PupilDiaX, " +
          "[PupilDiaY] = @PupilDiaY , [GazePosX] = @GazePosX, [GazePosY] = @GazePosY, " +
          "[MousePosX] = @MousePosX , [MousePosY] = @MousePosY, [EventID] = @EventID " +
          "WHERE [ID] = @Original_ID",
          Document.ActiveDocument.DocDataSet.DatabaseConnection);

      // Add the parameters for the SelectCommand.
      command.Parameters.Add("@TrialSequence", DbType.Int32);
      command.Parameters.Add("@SubjectName", DbType.String);
      command.Parameters.Add("@Time", DbType.Int64);
      command.Parameters.Add("@PupilDiaX", DbType.Single);
      command.Parameters.Add("@PupilDiaY", DbType.Single);
      command.Parameters.Add("@GazePosX", DbType.Single);
      command.Parameters.Add("@GazePosY", DbType.Single);
      command.Parameters.Add("@MousePosX", DbType.Single);
      command.Parameters.Add("@MousePosY", DbType.Single);
      command.Parameters.Add("@EventID", DbType.Int32);
      command.Parameters.Add("@Original_ID", DbType.Int64);

      // Set SelectCommand
      adapter.UpdateCommand = command;

      // Set Parameters
      adapter.UpdateCommand.Parameters[0].Value = Convert.ToInt32(row["TrialSequence"]);
      adapter.UpdateCommand.Parameters[1].Value = row["SubjectName"].ToString();
      adapter.UpdateCommand.Parameters[2].Value = Convert.ToInt64(row["Time"]);
      adapter.UpdateCommand.Parameters[3].Value = row.IsNull("PupilDiaX") ? Convert.DBNull : Convert.ToSingle(row["PupilDiaX"]);
      adapter.UpdateCommand.Parameters[4].Value = row.IsNull("PupilDiaY") ? Convert.DBNull : Convert.ToSingle(row["PupilDiaY"]);
      adapter.UpdateCommand.Parameters[5].Value = row.IsNull("GazePosX") ? Convert.DBNull : Convert.ToSingle(row["GazePosX"]);
      adapter.UpdateCommand.Parameters[6].Value = row.IsNull("GazePosY") ? Convert.DBNull : Convert.ToSingle(row["GazePosY"]);
      adapter.UpdateCommand.Parameters[7].Value = row.IsNull("MousePosX") ? Convert.DBNull : Convert.ToSingle(row["MousePosX"]);
      adapter.UpdateCommand.Parameters[8].Value = row.IsNull("MousePosY") ? Convert.DBNull : Convert.ToSingle(row["MousePosY"]);
      adapter.UpdateCommand.Parameters[9].Value = row.IsNull("EventID") ? Convert.DBNull : Convert.ToInt32(row["EventID"]);
      adapter.UpdateCommand.Parameters[10].Value = originalID;

      DataRow[] newRowArray = new DataRow[1];
      newRowArray[0] = row;

      // Update row
      int numUpdatedRows = adapter.Update(newRowArray);

      return numUpdatedRows;
    }

    /// <summary>
    /// Updates row in raw data table from given subject and ID
    /// with data given in parameter row.
    /// Uses given subjects raw data table.
    /// </summary>
    /// <param name="table">A <see cref="SQLiteOgamaDataSet.RawdataDataTable"/> with the new values</param>
    /// <param name="subject">A <see cref="string"/> with the subject name to which the data belongs.</param>
    /// <returns>A <see cref="int"/> with the number of updated rows.</returns>
    /// <exception cref="ArgumentNullException">Thrown, when row or subject is null or empty.</exception>
    public static int UpdateRawDataBySubject(SQLiteOgamaDataSet.RawdataDataTable table, string subject)
    {
      if (table == null)
      {
        throw new ArgumentNullException();
      }

      if (subject == string.Empty)
      {
        throw new ArgumentNullException();
      }

      SQLiteDataAdapter adapter = Document.ActiveDocument.DocDataSet.RawDataAdapterDict[subject];

      adapter.UpdateCommand = new SQLiteCommand();
      adapter.UpdateCommand.Connection = Document.ActiveDocument.DocDataSet.DatabaseConnection;
      adapter.UpdateCommand.CommandText = @"UPDATE [" + subject + @"Rawdata] SET [SubjectName] = @SubjectName, [TrialSequence] = @TrialSequence, [Time] = @Time, [PupilDiaX] = @PupilDiaX, [PupilDiaY] = @PupilDiaY, [GazePosX] = @GazePosX, [GazePosY] = @GazePosY, [MousePosX] = @MousePosX, [MousePosY] = @MousePosY, [EventID] = @EventID WHERE (([ID] = @Original_ID) AND ([SubjectName] = @Original_SubjectName) AND ([TrialSequence] = @Original_TrialSequence) AND ([Time] = @Original_Time) AND ((@IsNull_PupilDiaX = 1 AND [PupilDiaX] IS NULL) OR ([PupilDiaX] = @Original_PupilDiaX)) AND ((@IsNull_PupilDiaY = 1 AND [PupilDiaY] IS NULL) OR ([PupilDiaY] = @Original_PupilDiaY)) AND ((@IsNull_GazePosX = 1 AND [GazePosX] IS NULL) OR ([GazePosX] = @Original_GazePosX)) AND ((@IsNull_GazePosY = 1 AND [GazePosY] IS NULL) OR ([GazePosY] = @Original_GazePosY)) AND ((@IsNull_MousePosX = 1 AND [MousePosX] IS NULL) OR ([MousePosX] = @Original_MousePosX)) AND ((@IsNull_MousePosY = 1 AND [MousePosY] IS NULL) OR ([MousePosY] = @Original_MousePosY)) AND ((@IsNull_EventID = 1 AND [EventID] IS NULL) OR ([EventID] = @Original_EventID)));
SELECT ID, SubjectName, TrialSequence, Time, PupilDiaX, PupilDiaY, GazePosX, GazePosY, MousePosX, MousePosY, EventID FROM " + subject + "Rawdata WHERE (ID = @ID)";
      adapter.UpdateCommand.CommandType = CommandType.Text;
      adapter.UpdateCommand.Parameters.Add(new SQLiteParameter("@SubjectName", DbType.String, 0, ParameterDirection.Input, 0, 0, "SubjectName", DataRowVersion.Current, false, null));
      adapter.UpdateCommand.Parameters.Add(new SQLiteParameter("@TrialSequence", DbType.Int32, 0, ParameterDirection.Input, 0, 0, "TrialSequence", DataRowVersion.Current, false, null));
      adapter.UpdateCommand.Parameters.Add(new SQLiteParameter("@Time", DbType.Int64, 0, ParameterDirection.Input, 0, 0, "Time", DataRowVersion.Current, false, null));
      adapter.UpdateCommand.Parameters.Add(new SQLiteParameter("@PupilDiaX", DbType.Single, 0, ParameterDirection.Input, 0, 0, "PupilDiaX", DataRowVersion.Current, false, null));
      adapter.UpdateCommand.Parameters.Add(new SQLiteParameter("@PupilDiaY", DbType.Single, 0, ParameterDirection.Input, 0, 0, "PupilDiaY", DataRowVersion.Current, false, null));
      adapter.UpdateCommand.Parameters.Add(new SQLiteParameter("@GazePosX", DbType.Single, 0, ParameterDirection.Input, 0, 0, "GazePosX", DataRowVersion.Current, false, null));
      adapter.UpdateCommand.Parameters.Add(new SQLiteParameter("@GazePosY", DbType.Single, 0, ParameterDirection.Input, 0, 0, "GazePosY", DataRowVersion.Current, false, null));
      adapter.UpdateCommand.Parameters.Add(new SQLiteParameter("@MousePosX", DbType.Single, 0, ParameterDirection.Input, 0, 0, "MousePosX", DataRowVersion.Current, false, null));
      adapter.UpdateCommand.Parameters.Add(new SQLiteParameter("@MousePosY", DbType.Single, 0, ParameterDirection.Input, 0, 0, "MousePosY", DataRowVersion.Current, false, null));
      adapter.UpdateCommand.Parameters.Add(new SQLiteParameter("@EventID", DbType.Int32, 0, ParameterDirection.Input, 0, 0, "EventID", DataRowVersion.Current, false, null));
      adapter.UpdateCommand.Parameters.Add(new SQLiteParameter("@Original_ID", DbType.Int64, 0, ParameterDirection.Input, 0, 0, "ID", DataRowVersion.Original, false, null));
      adapter.UpdateCommand.Parameters.Add(new SQLiteParameter("@Original_SubjectName", DbType.String, 0, ParameterDirection.Input, 0, 0, "SubjectName", DataRowVersion.Original, false, null));
      adapter.UpdateCommand.Parameters.Add(new SQLiteParameter("@Original_TrialSequence", DbType.Int32, 0, ParameterDirection.Input, 0, 0, "TrialSequence", DataRowVersion.Original, false, null));
      adapter.UpdateCommand.Parameters.Add(new SQLiteParameter("@Original_Time", DbType.Int64, 0, ParameterDirection.Input, 0, 0, "Time", DataRowVersion.Original, false, null));
      adapter.UpdateCommand.Parameters.Add(new SQLiteParameter("@IsNull_PupilDiaX", DbType.Int32, 0, ParameterDirection.Input, 0, 0, "PupilDiaX", DataRowVersion.Original, true, null));
      adapter.UpdateCommand.Parameters.Add(new SQLiteParameter("@Original_PupilDiaX", DbType.Single, 0, ParameterDirection.Input, 0, 0, "PupilDiaX", DataRowVersion.Original, false, null));
      adapter.UpdateCommand.Parameters.Add(new SQLiteParameter("@IsNull_PupilDiaY", DbType.Int32, 0, ParameterDirection.Input, 0, 0, "PupilDiaY", DataRowVersion.Original, true, null));
      adapter.UpdateCommand.Parameters.Add(new SQLiteParameter("@Original_PupilDiaY", DbType.Single, 0, ParameterDirection.Input, 0, 0, "PupilDiaY", DataRowVersion.Original, false, null));
      adapter.UpdateCommand.Parameters.Add(new SQLiteParameter("@IsNull_GazePosX", DbType.Int32, 0, ParameterDirection.Input, 0, 0, "GazePosX", DataRowVersion.Original, true, null));
      adapter.UpdateCommand.Parameters.Add(new SQLiteParameter("@Original_GazePosX", DbType.Single, 0, ParameterDirection.Input, 0, 0, "GazePosX", DataRowVersion.Original, false, null));
      adapter.UpdateCommand.Parameters.Add(new SQLiteParameter("@IsNull_GazePosY", DbType.Int32, 0, ParameterDirection.Input, 0, 0, "GazePosY", DataRowVersion.Original, true, null));
      adapter.UpdateCommand.Parameters.Add(new SQLiteParameter("@Original_GazePosY", DbType.Single, 0, ParameterDirection.Input, 0, 0, "GazePosY", DataRowVersion.Original, false, null));
      adapter.UpdateCommand.Parameters.Add(new SQLiteParameter("@IsNull_MousePosX", DbType.Int32, 0, ParameterDirection.Input, 0, 0, "MousePosX", DataRowVersion.Original, true, null));
      adapter.UpdateCommand.Parameters.Add(new SQLiteParameter("@Original_MousePosX", DbType.Single, 0, ParameterDirection.Input, 0, 0, "MousePosX", DataRowVersion.Original, false, null));
      adapter.UpdateCommand.Parameters.Add(new SQLiteParameter("@IsNull_MousePosY", DbType.Int32, 0, ParameterDirection.Input, 0, 0, "MousePosY", DataRowVersion.Original, true, null));
      adapter.UpdateCommand.Parameters.Add(new SQLiteParameter("@Original_MousePosY", DbType.Single, 0, ParameterDirection.Input, 0, 0, "MousePosY", DataRowVersion.Original, false, null));
      adapter.UpdateCommand.Parameters.Add(new SQLiteParameter("@IsNull_EventID", DbType.Int32, 0, ParameterDirection.Input, 0, 0, "EventID", DataRowVersion.Original, true, null));
      adapter.UpdateCommand.Parameters.Add(new SQLiteParameter("@Original_EventID", DbType.Int32, 0, ParameterDirection.Input, 0, 0, "EventID", DataRowVersion.Original, false, null));
      adapter.UpdateCommand.Parameters.Add(new SQLiteParameter("@ID", DbType.Int64, 8, ParameterDirection.Input, 0, 0, "ID", DataRowVersion.Current, false, null));

      // Update row
      int numUpdatedRows = adapter.Update(table);

      return numUpdatedRows;
    }

    #endregion DatabaseUpdateQueries

    #region DatabaseSelectQueries

    /// <summary>
    /// This method returns a <see cref="List{Int32}"/> of trials that are recorded
    /// with the same trialID, which means the same slide is shown multiple times,
    /// only the sequence number is different.
    /// By default there is only on sequence number for each trial ID,
    /// but in some special situations like linking from slide to other slides
    /// this occurs.
    /// </summary>
    /// <param name="subjectName">A <see cref="String"/> with the subject name</param>
    /// <param name="trialID">An <see cref="int"/> with the unique trial ID</param>
    /// <returns>A <see cref="List{Int32}"/> of trial sequence numbers.</returns>
    public static List<int> GetSequencesBySubjectAndTrialID(string subjectName, int trialID)
    {
      List<int> sequences = new List<int>();
      DataTable table = Document.ActiveDocument.DocDataSet.TrialsAdapter.GetTrialSequenceBySubjectAndTrialID(subjectName, trialID);

      foreach (DataRow row in table.Rows)
      {
        sequences.Add((int)row["TrialSequence"]);
      }

      return sequences;
    }

    /// <summary>
    /// This method returns the ID for the given trial sequence of the
    /// given subject.
    /// </summary>
    /// <param name="subjectName">A <see cref="String"/> with the subject name</param>
    /// <param name="trialSequence">An <see cref="int"/> with the unique trial sequence</param>
    /// <returns>An <see cref="Int32"/> with the trial ID, or -1 if this sequence wasn´t found
    /// for this subject.</returns>
    public static int GetTrialIDForSequence(string subjectName, int trialSequence)
    {
      DataTable table = Document.ActiveDocument.DocDataSet.TrialsAdapter.GetDataBySubjectAndSequence(subjectName, trialSequence);
      if (table.Rows.Count > 0)
      {
        return (int)table.Rows[0]["TrialID"];
      }

      return -1;
    }

    /// <summary>
    /// Gets raw data table with rows found for the Query:
    /// SELECT * WHERE Trial=Param1 AND Response=Param2 ORDER BY Time
    /// Uses given subjects raw data table.
    /// </summary>
    /// <param name="subject">A <see cref="string"/> with the subject name to which the data belongs.</param>
    /// <param name="trialSequence">A <see cref="int"/> with the trial sequence number. (Param1)</param>
    /// <param name="response">A <see cref="string"/> with the response. (Param2)</param>
    /// <returns>A <see cref="SQLiteOgamaDataSet.RawdataDataTable"/> with found rows.</returns>
    /// <exception cref="ArgumentNullException">Thrown, when subject is null or empty.</exception>
    public static SQLiteOgamaDataSet.RawdataDataTable GetDataBySubjectTrialResponse(
      string subject,
      int trialSequence,
      string response)
    {
      if (subject == string.Empty)
      {
        throw new ArgumentNullException();
      }

      if (!Document.ActiveDocument.DocDataSet.RawDataAdapterDict.ContainsKey(subject))
      {
        return null;
      }

      SQLiteDataAdapter adapter = Document.ActiveDocument.DocDataSet.RawDataAdapterDict[subject];

      // Create the SelectCommand.
      var command = new SQLiteCommand(
        "SELECT " + subject + "Rawdata.* FROM [" +
        subject + "Rawdata] WHERE (TrialSequence = @Param1) AND (Response = @Param2)" +
        " ORDER BY Time",
        Document.ActiveDocument.DocDataSet.DatabaseConnection);

      // Add the parameters for the SelectCommand.
      command.Parameters.Add("@Param1", DbType.Int32);
      command.Parameters.Add("@Param2", DbType.String);

      // Set SelectCommand
      adapter.SelectCommand = command;

      // Set Parameters
      adapter.SelectCommand.Parameters[0].Value = trialSequence;
      adapter.SelectCommand.Parameters[1].Value = response;

      // Create DataTable
      var dataTable = new SQLiteOgamaDataSet.RawdataDataTable();

      // Fill it with Data referring to Subject and Trial
      adapter.Fill(dataTable);

      return dataTable;
    }

    /// <summary>
    /// Gets raw data table with rows found for the Query:
    /// SELECT * WHERE Trial=Param1 AND MousePosX IS NOT NULL ORDER BY Time
    /// Uses given subjects raw data table
    /// </summary>
    /// <param name="subject">A <see cref="string"/> with the subject name to which the data belongs.</param>
    /// <param name="trialSequence">A <see cref="int"/> with the trial sequence number. (Param1)</param>
    /// <returns>A <see cref="SQLiteOgamaDataSet.RawdataDataTable"/> with found rows.</returns>
    /// <exception cref="ArgumentNullException">Thrown, when subject is null or empty.</exception>
    public static SQLiteOgamaDataSet.RawdataDataTable GetMouseDataBySubjectTrial(string subject, int trialSequence)
    {
      if (subject == string.Empty)
      {
        throw new ArgumentNullException();
      }

      if (!Document.ActiveDocument.DocDataSet.RawDataAdapterDict.ContainsKey(subject))
      {
        return null;
      }

      SQLiteDataAdapter adapter = Document.ActiveDocument.DocDataSet.RawDataAdapterDict[subject];

      // Create the SelectCommand.
      var command = new SQLiteCommand(
        "SELECT " + subject + "Rawdata.* FROM [" +
        subject + "Rawdata] WHERE (TrialSequence = @Param1) AND (MousePosX IS NOT NULL)" +
        " ORDER BY Time",
        Document.ActiveDocument.DocDataSet.DatabaseConnection);

      // Add the parameters for the SelectCommand.
      command.Parameters.Add("@Param1", DbType.Int32);

      // Set SelectCommand
      adapter.SelectCommand = command;

      // Set Parameters
      adapter.SelectCommand.Parameters[0].Value = trialSequence;

      // Create DataTable
      var dataTable = new SQLiteOgamaDataSet.RawdataDataTable();

      // Fill it with Data referring to Subject and Trial
      adapter.Fill(dataTable);

      return dataTable;
    }

    /// <summary>
    /// Gets raw data table with rows found for the Query:
    /// SELECT * WHERE TrialSequence = @TrialSequence ORDER BY SubjectName, TrialSequence, Time
    /// Uses given subjects raw data table.
    /// </summary>
    /// <param name="subject">A <see cref="string"/> with the subject name to which the data belongs.</param>
    /// <param name="trialSequence">A <see cref="int"/> with the trial sequence number. (Param1)</param>
    /// <returns>A <see cref="SQLiteOgamaDataSet.RawdataDataTable"/> with found rows.</returns>
    /// <exception cref="ArgumentNullException">Thrown, when subject is null or empty.</exception>
    public static SQLiteOgamaDataSet.RawdataDataTable GetRawDataBySubjectAndTrialSequence(string subject, int trialSequence)
    {
      if (subject == string.Empty)
      {
        throw new ArgumentNullException();
      }

      if (!Document.ActiveDocument.DocDataSet.RawDataAdapterDict.ContainsKey(subject))
      {
        return null;
      }

      SQLiteDataAdapter adapter = Document.ActiveDocument.DocDataSet.RawDataAdapterDict[subject];

      // Create the SelectCommand.
      var command = new SQLiteCommand(
        "SELECT " + subject + "Rawdata.* FROM [" + subject + "Rawdata] WHERE TrialSequence = @TrialSequence" +
        " ORDER BY SubjectName, TrialSequence, Time",
        Document.ActiveDocument.DocDataSet.DatabaseConnection);

      // Add the parameters for the SelectCommand.
      command.Parameters.Add("@TrialSequence", DbType.Int32);

      // Set SelectCommand
      adapter.SelectCommand = command;

      // Set Parameters
      adapter.SelectCommand.Parameters[0].Value = trialSequence;

      // Create DataTable
      var dataTable = new SQLiteOgamaDataSet.RawdataDataTable();

      // Fill it with Data 
      adapter.Fill(dataTable);

      // Give correct name
      dataTable.TableName = subject + "Rawdata";

      return dataTable;
    }

    /// <summary>
    /// Gets raw data table with rows found for the Query:
    /// SELECT * WHERE (TrialSequence = @TrialSequence) AND (EventID is NULL) ORDER BY SubjectName, TrialSequence, Time
    /// Uses given subjects raw data table.
    /// </summary>
    /// <param name="subject">A <see cref="string"/> with the subject name to which the data belongs.</param>
    /// <param name="trialSequence">A <see cref="int"/> with the trial sequence number. (Param1)</param>
    /// <returns>A <see cref="SQLiteOgamaDataSet.RawdataDataTable"/> with found rows.</returns>
    /// <exception cref="ArgumentNullException">Thrown, when subject is null or empty.</exception>
    public static DataTable GetRawDataBySubjectAndTrialSequenceWithoutEvents(string subject, int trialSequence)
    {
      if (subject == string.Empty)
      {
        throw new ArgumentNullException();
      }

      if (!Document.ActiveDocument.DocDataSet.RawDataAdapterDict.ContainsKey(subject))
      {
        return null;
      }

      SQLiteDataAdapter adapter = Document.ActiveDocument.DocDataSet.RawDataAdapterDict[subject];

      // Create the SelectCommand.
      var command = new SQLiteCommand(
        "SELECT " + subject + "Rawdata.* FROM [" + subject + "Rawdata] WHERE (TrialSequence = @TrialSequence) AND (EventID is NULL)" +
        " ORDER BY SubjectName, TrialSequence, Time",
        Document.ActiveDocument.DocDataSet.DatabaseConnection);

      // Add the parameters for the SelectCommand.
      command.Parameters.Add("@TrialSequence", DbType.Int32);

      // Set SelectCommand
      adapter.SelectCommand = command;

      // Set Parameters
      adapter.SelectCommand.Parameters[0].Value = trialSequence;

      // Create DataTable
      var dataTable = new SQLiteOgamaDataSet.RawdataDataTable();

      // Fill it with Data 
      adapter.Fill(dataTable);

      // Give correct name
      dataTable.TableName = subject + "Rawdata";

      return dataTable;
    }

    /// <summary>
    /// Gets the whole raw data table for the given subject using the query:
    /// SELECT SubjectRawdata.* FROM [Subject]
    /// Uses given subjects whole raw data table.
    /// </summary>
    /// <param name="subject">A <see cref="string"/> with the subject name to which the data belongs.</param>
    /// <returns>The whole <see cref="SQLiteOgamaDataSet.RawdataDataTable"/> for the given subject.</returns>
    /// <exception cref="ArgumentNullException">Thrown, when subject is null or empty.</exception>
    public static SQLiteOgamaDataSet.RawdataDataTable GetRawDataBySubject(string subject)
    {
      if (subject == string.Empty)
      {
        throw new ArgumentNullException();
      }

      if (!Document.ActiveDocument.DocDataSet.RawDataAdapterDict.ContainsKey(subject))
      {
        return null;
      }

      SQLiteDataAdapter adapter = Document.ActiveDocument.DocDataSet.RawDataAdapterDict[subject];

      // Create the SelectCommand.
      var command = new SQLiteCommand(
        "SELECT " + subject + "Rawdata.* FROM [" + subject + "Rawdata]",
        Document.ActiveDocument.DocDataSet.DatabaseConnection);

      // Set SelectCommand
      adapter.SelectCommand = command;

      // Create DataTable
      var dataTable = new SQLiteOgamaDataSet.RawdataDataTable();

      // Fill it with Data referring to Subject and Trial
      adapter.Fill(dataTable);
      return dataTable;
    }

    /// <summary>
    /// Gets gaze fixation data table rows by given Where statement using the query:
    /// SELECT GazeFixations.* FROM [GazeFixations] WHERE WhereStatement 
    /// ORDER BY SubjectName,TrialID,Length
    /// </summary>
    /// <param name="whereStatement">A <see cref="string"/> with the SQL WHERE 
    /// statement for the query to use.</param>
    /// <returns>A <see cref="SQLiteOgamaDataSet.GazeFixationsDataTable"/> with found rows.</returns>
    /// <exception cref="ArgumentNullException">Thrown, when WhereStatement is empty.</exception>
    public static SQLiteOgamaDataSet.GazeFixationsDataTable GetGazeFixDataByWhereStatement(string whereStatement)
    {
      if (whereStatement == string.Empty)
      {
        throw new ArgumentNullException();
      }

      SQLiteDataAdapter adapter = new SQLiteDataAdapter();
      System.Data.Common.DataTableMapping tableMapping = new System.Data.Common.DataTableMapping();
      tableMapping.SourceTable = "Table";
      tableMapping.DataSetTable = "TableGazeFixations";
      tableMapping.ColumnMappings.Add("ID", "ID");
      tableMapping.ColumnMappings.Add("SubjectName", "SubjectName");
      tableMapping.ColumnMappings.Add("TrialID", "TrialID");
      tableMapping.ColumnMappings.Add("TrialSequence", "TrialSequence");
      tableMapping.ColumnMappings.Add("CountInTrial", "CountInTrial");
      tableMapping.ColumnMappings.Add("StartTime", "StartTime");
      tableMapping.ColumnMappings.Add("Length", "Length");
      tableMapping.ColumnMappings.Add("PosX", "PosX");
      tableMapping.ColumnMappings.Add("PosY", "PosY");

      adapter.TableMappings.Add(tableMapping);

      // Create the SelectCommand.
      var command = new SQLiteCommand(
        "SELECT GazeFixations.* FROM [GazeFixations] " +
        "WHERE " + whereStatement +
        " ORDER BY SubjectName,TrialID,Length",
        Document.ActiveDocument.DocDataSet.DatabaseConnection);

      // Set SelectCommand
      adapter.SelectCommand = command;

      // Create DataTable
      var dataTable = new SQLiteOgamaDataSet.GazeFixationsDataTable();

      // Fill it with Data referring to Subject and Trial
      adapter.Fill(dataTable);

      return dataTable;
    }

    /// <summary>
    /// Gets mouse fixation data table rows by given Where statement using the query:
    /// SELECT MouseFixations.* FROM [MouseFixations] WHERE WhereStatement 
    /// ORDER BY SubjectName,TrialID,Length
    /// </summary>
    /// <param name="whereStatement">A <see cref="string"/> with the SQL WHERE 
    /// statement for the query to use.</param>
    /// <returns>A <see cref="SQLiteOgamaDataSet.MouseFixationsDataTable"/> with found rows.</returns>
    /// <exception cref="ArgumentNullException">Thrown, when WhereStatement is empty.</exception>
    public static SQLiteOgamaDataSet.MouseFixationsDataTable GetMouseFixDataByWhereStatement(string whereStatement)
    {
      if (whereStatement == string.Empty)
      {
        throw new ArgumentNullException();
      }

      SQLiteDataAdapter adapter = new SQLiteDataAdapter();
      System.Data.Common.DataTableMapping tableMapping = new System.Data.Common.DataTableMapping();
      tableMapping.SourceTable = "Table";
      tableMapping.DataSetTable = "TableMouseFixations";
      tableMapping.ColumnMappings.Add("ID", "ID");
      tableMapping.ColumnMappings.Add("SubjectName", "SubjectName");
      tableMapping.ColumnMappings.Add("TrialID", "TrialID");
      tableMapping.ColumnMappings.Add("TrialSequence", "TrialSequence");
      tableMapping.ColumnMappings.Add("CountInTrial", "CountInTrial");
      tableMapping.ColumnMappings.Add("StartTime", "StartTime");
      tableMapping.ColumnMappings.Add("Length", "Length");
      tableMapping.ColumnMappings.Add("PosX", "PosX");
      tableMapping.ColumnMappings.Add("PosY", "PosY");

      adapter.TableMappings.Add(tableMapping);

      // Create the SelectCommand.
      var command = new SQLiteCommand(
        "SELECT MouseFixations.* FROM [MouseFixations] " +
        "WHERE " + whereStatement +
        " ORDER BY SubjectName,TrialID,Length",
        Document.ActiveDocument.DocDataSet.DatabaseConnection);

      // Set SelectCommand
      adapter.SelectCommand = command;

      // Create DataTable
      var dataTable = new SQLiteOgamaDataSet.MouseFixationsDataTable();

      // Fill it with Data referring to Subject and Trial
      adapter.Fill(dataTable);

      return dataTable;
    }

    #endregion //DatabaseSelectQueries

    #region DatabaseAdminQueries

    /// <summary>
    /// This method checks for a column with the given name in the given table
    /// and adds it with the given SQL Type, if it was not found.
    /// </summary>
    /// <param name="tableName">A <see cref="string"/> with the table name.</param>
    /// <param name="columnName">A <see cref="string"/> with the column name.</param>
    /// <param name="columnType">A <see cref="string"/> with the SQL column type of the new column.</param>
    public static void AddColumn(string tableName, string columnName, string columnType)
    {
      if (!Queries.ColumnExists(tableName, columnName))
      {
        string addQuery = "ALTER TABLE " + tableName + " ADD " + columnName + " " + columnType;
        Queries.ExecuteSQLCommand(addQuery);
      }
    }

    /// <summary>
    /// This method checks for a column with the given name in the given table
    /// and fills it with '0' values, if it was found.
    /// </summary>
    /// <param name="tableName">A <see cref="string"/> with the table name.</param>
    /// <param name="columnName">A <see cref="string"/> with the column name.</param>
    public static void FillColumnWithZeros(string tableName, string columnName)
    {
      if (Queries.ColumnExists(tableName, columnName))
      {
        Queries.ExecuteSQLCommand("UPDATE " + tableName + " SET " + columnName + " = 0");
      }
    }

    /// <summary>
    /// This method checks for a column with the given name in the given table
    /// and copies it with its data to the new column, if it was found.
    /// </summary>
    /// <param name="tableName">A <see cref="string"/> with the table name.</param>
    /// <param name="columnNameToCopy">A <see cref="string"/> with the column name to be copied.</param>
    /// <param name="newColumnName">A <see cref="string"/> with the new column 
    /// name which will received the copied data and structure.</param>
    public static void CopyColumn(string tableName, string columnNameToCopy, string newColumnName)
    {
      if (Queries.ColumnExists(tableName, columnNameToCopy))
      {
        Queries.ExecuteSQLCommand("UPDATE " + tableName + " SET " + newColumnName + " = " + columnNameToCopy);
      }
    }

    /// <summary>
    /// This method checks for a column with the given name in the given table
    /// and changes its SQL Type, if it was found.
    /// </summary>
    /// <param name="tableName">A <see cref="string"/> with the table name.</param>
    /// <param name="columnName">A <see cref="string"/> with the column name.</param>
    /// <param name="columnType">A <see cref="string"/> with the SQL column type of the new column.</param>
    public static void ChangeColumnType(string tableName, string columnName, string columnType)
    {
      if (Queries.ColumnExists(tableName, columnName))
      {
        string query = "ALTER TABLE " + tableName + " ALTER COLUMN " + columnName + " " + columnType;
        Queries.ExecuteSQLCommand(query);
      }
    }

    /// <summary>
    /// This method checks for a column with the given name in the given table
    /// and removes it, if it was found.
    /// </summary>
    /// <param name="tableName">A <see cref="string"/> with the table name.</param>
    /// <param name="columnName">A <see cref="string"/> with the column name.</param>
    public static void RemoveColumn(string tableName, string columnName)
    {
      if (Queries.ColumnExists(tableName, columnName))
      {
        Queries.ExecuteSQLCommand("ALTER TABLE " + tableName + " DROP COLUMN " + columnName);
      }
    }

    /// <summary>
    /// This method checks for a table with the given name
    /// and removes it, if it was found.
    /// </summary>
    /// <param name="tableName">A <see cref="string"/> with the name of the table to be removed.</param>
    public static void RemoveTable(string tableName)
    {
      if (Queries.TableExists(tableName))
      {
        Queries.ExecuteSQLCommand("DROP TABLE " + tableName);
      }
    }

    /// <summary>
    /// This method sets the identiy column on column "ID"
    /// if it is not there already
    /// </summary>
    /// <param name="tableName">A <see cref="string"/> with the table name.</param>
    public static void AddIdentityColumn(string tableName)
    {
      if (Queries.ColumnExists(tableName, "ID"))
      {
        // Drop constraints
        // The following line gets the schema table that holds all of the columns from every table.
        DataTable tables = Document.ActiveDocument.DocDataSet.DatabaseConnection.GetSchema("Indexes");

        // The following line filters through all of the table and get’s 
        // just the column with name ‘ShapeGroups’.
        DataRow[] selTables = tables.Select("TABLE_NAME='" + tableName + "'");

        if (selTables.Length > 0)
        {
          foreach (DataRow constraintRow in selTables)
          {
            string constraintName = selTables[0]["constraint_name"].ToString();
            string dropQuery = "ALTER TABLE " + tableName + " DROP CONSTRAINT " + constraintName;
            Queries.ExecuteSQLCommand(dropQuery);
          }
        }

        ////The following line did not work...
        ////string dropQuery = "ALTER TABLE dbo." + tableName + " DROP PRIMARY KEY";
        ////Queries.ExecuteSQLCommand(dropQuery);

        RemoveColumn(tableName, "ID");

        string identityQuery = "ALTER TABLE " + tableName + " ADD ID bigint NOT NULL IDENTITY (1,1)";
        Queries.ExecuteSQLCommand(identityQuery);

        string primaryKeyQuery = "ALTER TABLE " + tableName + " ADD CONSTRAINT PK_" + tableName +
          " PRIMARY KEY (ID)";
        Queries.ExecuteSQLCommand(primaryKeyQuery);
      }
    }

    /// <summary>
    /// This method checks for a table with the given name
    /// and renames it, if it was found.
    /// </summary>
    /// <param name="oldTableName">A <see cref="string"/> with the old table name to be renamed.</param>
    /// <param name="newTableName">A <see cref="string"/> with the new table name.</param>
    public static void RenameTable(string oldTableName, string newTableName)
    {
      if (Queries.TableExists(oldTableName))
      {
        string command = "SP_RENAME '" + oldTableName + "', '" + newTableName + "'";
        int result = Queries.ExecuteSQLCommand(command);
      }
    }

    /// <summary>
    /// This method checks for a column with the given name in the given table
    /// and renames it, if it was found.
    /// </summary>
    /// <param name="tableName">A <see cref="string"/> with the table name.</param>
    /// <param name="oldColumnName">A <see cref="string"/> with the old column name to be renamed.</param>
    /// <param name="newColumnName">A <see cref="string"/> with the new column name.</param>
    public static void RenameColumn(string tableName, string oldColumnName, string newColumnName)
    {
      if (Queries.ColumnExists(tableName, oldColumnName) && !Queries.ColumnExists(tableName, newColumnName))
      {
        // The following query does not work with MS SQL Server
        // string command ="ALTER TABLE " + tableName + " RENAME COLUMN '" + oldColumnName + "' to '" + newColumnName + "'";
        string command = "SP_RENAME '" + tableName + "." + oldColumnName + "', '" + newColumnName + "'";

        int result = Queries.ExecuteSQLCommand(command);
      }
    }

    /// <summary>
    /// This method creates a temporary table with the column definitions given in the parameter
    /// named dbo.Tmp_TABLENAME. Then it copies all the data from the table given by tablename
    /// into the newly created table with IDENTITY_INSERT Off.
    /// When this is done an exact copy but with correct column layout as given in 
    /// 'columnDefinitions' parameter from the 'tableName' table has been created.
    /// No this method resets IDENTITY_INSERT on, drops the original table and
    /// renames the temporary table to 'tableName'.
    /// Finally one primary key constraint on the column 'ID' is created.
    /// </summary>
    /// <param name="tableName">A <see cref="String"/> with the table to be copied.</param>
    /// <param name="columnDefinitions">A <see cref="String"/> with the SQL column definitions,
    /// e.g. [ID] [bigint] IDENTITY(1,1) NOT NULL, [SubjectName] [varchar](50) COLLATE Latin1_General_CI_AS NOT NULL, [TrialSequence] [int] NOT NULL</param>
    /// <param name="columns">A <see cref="String"/> with the column names separated by commas, but without the ID column
    /// e.g. SubjectName, TrialSequence, Time</param>
    public static void RecreateTable(string tableName, string columnDefinitions, string columns)
    {
      string recreateQuery = "CREATE TABLE Tmp_" + tableName + "(" + columnDefinitions + " ) ON [PRIMARY]";
      string identityInsertOffQuery = "SET IDENTITY_INSERT Tmp_" + tableName + " OFF";
      string moveQuery = "IF EXISTS(SELECT * FROM " + tableName + ") EXEC('INSERT INTO dbo.Tmp_" + tableName + " (" + columns + ") SELECT " + columns + " FROM dbo." + tableName + " WITH (HOLDLOCK TABLOCKX)')";
      string identityInsertOnQuery = "SET IDENTITY_INSERT Tmp_" + tableName + " On";
      string dropTableQuery = "DROP TABLE " + tableName;
      string renameTableQuery = "EXECUTE sp_rename N'Tmp_" + tableName + "', N'" + tableName + "', 'OBJECT' ";
      string addprimaryKeyQuery = "ALTER TABLE " + tableName + " ADD CONSTRAINT PK_" + tableName + " PRIMARY KEY CLUSTERED ( ID ) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]";

      ExecuteSQLCommand(recreateQuery);
      ExecuteSQLCommand(identityInsertOffQuery);
      ExecuteSQLCommand(moveQuery);
      ExecuteSQLCommand(identityInsertOnQuery);
      ExecuteSQLCommand(dropTableQuery);
      ExecuteSQLCommand(renameTableQuery);
      ExecuteSQLCommand(addprimaryKeyQuery);
    }

    /// <summary>
    /// This method inserts a primary key on column "ID"
    /// if it is not there already
    /// </summary>
    /// <param name="tableName">A <see cref="string"/> with the table name.</param>
    public static void AddPrimaryKeyOnIDColumn(string tableName)
    {
      if (ColumnExists(tableName, "ID"))
      {
        // Add primary key to table if not done already.
        if (GetPrimaryKey(tableName) == string.Empty)
        {
          string primaryKeyQuery = "ALTER TABLE " + tableName + " ADD PRIMARY KEY (ID)";
          Queries.ExecuteSQLCommand(primaryKeyQuery);
        }
      }
    }

    /// <summary>
    /// This method checks if the given column exists in the given table.
    /// </summary>
    /// <param name="tableName">A <see cref="string"/> with the table name.</param>
    /// <param name="columnName">A <see cref="string"/> with the column name.</param>
    /// <returns><strong>True</strong> if the table has a column with the given name,
    /// otherwise <strong>false</strong>.</returns>
    public static bool ColumnExists(string tableName, string columnName)
    {
      // The following lines check for the column VideoStartTime
      // in the table trials.
      // This column is new in Version 2.0 and is now added to older versions
      // to enable the user video support and omit failures during fill.
      // The following line gets the schema table that holds all of the columns from every table.
      DataTable columns = Document.ActiveDocument.DocDataSet.DatabaseConnection.GetSchema("columns");

      // The following line filters through all of the columns and get’s 
      // just the column with name ‘VideoStartTime’ and belongs to the table ‘Trials’.
      DataRow[] selColumns = columns.Select("COLUMN_NAME='" + columnName + "' AND TABLE_NAME='" + tableName + "'");

      // If there are any columns that fit the filter than the length
      // of selColumns will be greater than zero. If it doesn’t exist 
      // than selColumns.Length will be 0 and you know that the column doesn’t exist.
      if (selColumns.Length == 0)
      {
        return false;
      }
      else
      {
        return true;
      }
    }

    /// <summary>
    /// This method check for the given table in the database
    /// </summary>
    /// <param name="tableName">A <see cref="string"/> with the table name</param>
    /// <returns><strong>True</strong> if table exists, otherwise <strong>false</strong>.</returns>
    public static bool TableExists(string tableName)
    {
      // The following line gets the schema table that holds all of the columns from every table.
      DataTable tables = Document.ActiveDocument.DocDataSet.DatabaseConnection.GetSchema("Tables");

      // The following line filters through all of the table and get’s 
      // just the column with name ‘ShapeGroups’.
      DataRow[] selTables = tables.Select("TABLE_NAME='" + tableName + "'");

      // If there are any table that fit the filter than the length
      // of selTables will be greater than zero. If it doesn’t exist 
      // than selTables.Length will be 0 and you know that the table doesn’t exist.
      if (selTables.Length == 0)
      {
        return false;
      }
      else
      {
        return true;
      }
    }

    /// <summary>
    /// This static method creates a new raw data table for
    /// the given subject with the name [subjectNameRawdata]
    /// using the SQL Statement:
    /// CREATE TABLE [" + subjectName + "Rawdata]([ID] [bigint] IDENTITY(1,1) NOT NULL,
    /// [SubjectName] [varchar](50) COLLATE Latin1_General_CI_AS NOT NULL,
    /// [TrialID] [int] NOT NULL,[Time][bigint] NOT NULL,[PupilDiaX] [float] NULL,
    /// [PupilDiaY][float] NULL,[GazePosX] [float] NULL,[GazePosY] [float] NULL,
    /// [MousePosX][float] NULL,[MousePosY] [float] NULL,[EventID] [int] NULL,
    /// CONSTRAINT[PK_" + subjectName + "Rawdata]PRIMARY KEY CLUSTERED(
    /// /// [ID] ASC)WITH(PAD_INDEX = OFF,IGNORE_DUP_KEY = OFF)ON [PRIMARY])ON[PRIMARY];";
    /// </summary>
    /// <param name="subjectName">A <see cref="string"/> with the subject name.</param>
    public static void CreateRawDataTableInDB(string subjectName)
    {
      if (subjectName == null)
      {
        return;
      }

      string tableName = subjectName + "Rawdata";

      if (TableExists(tableName))
      {
        DialogResult result = InformationDialog.Show(
          "Overwrite existing table ?",
          "The table you would like to create already exists, do you want to overwrite the table ?",
          true,
          MessageBoxIcon.Question);

        switch (result)
        {
          case DialogResult.Cancel:
            return;
          case DialogResult.No:
            return;
          case DialogResult.Yes:
            Queries.DeleteRawDataTableInDB(subjectName);
            break;
        }
      }

      // Create Empty Table from RawDataTemplate in DB
      string queryString = "CREATE TABLE [" + subjectName + "Rawdata]([ID]	integer PRIMARY KEY AUTOINCREMENT NOT NULL,[SubjectName]	varchar(50) NOT NULL COLLATE NOCASE, [TrialSequence]	integer NOT NULL, [Time]	integer NOT NULL, [PupilDiaX]	float, [PupilDiaY]	float, [GazePosX]	float, [GazePosY]	float, [MousePosX]	float, [MousePosY]	float, [EventID]	integer);";
      var command = new SQLiteCommand(queryString, Document.ActiveDocument.DocDataSet.DatabaseConnection);
      SQLiteDataReader reader = command.ExecuteReader();
      try
      {
        while (reader.Read())
        {
          Console.WriteLine(string.Format("{0}, {1}", reader[0], reader[1]));
        }
      }
      finally
      {
        // Always call Close when done reading.
        reader.Close();
      }
    }

    /// <summary>
    /// Deletes the raw data table for the given subject from the database
    /// using the SQL statement:
    /// DROP TABLE [" + Subject + "Rawdata];
    /// </summary>
    /// <param name="subject">A <see cref="string"/> with the subject name 
    /// to remove data for.</param>
    public static void DeleteRawDataTableInDB(string subject)
    {
      if (subject == null)
      {
        return;
      }

      string tableName = subject + "Rawdata";

      if (TableExists(tableName))
      {
        // Delete Entrys in Rawdata
        string queryString = "DROP TABLE [" + subject + "Rawdata];";
        var command = new SQLiteCommand(queryString, Document.ActiveDocument.DocDataSet.DatabaseConnection);
        SQLiteDataReader reader = command.ExecuteReader();
        try
        {
          while (reader.Read())
          {
            Console.WriteLine(string.Format("{0}, {1}", reader[0], reader[1]));
          }
        }
        finally
        {
          // Always call Close when done reading.
          reader.Close();
        }

        Document.ActiveDocument.DocDataSet.RawDataAdapterDict.Remove(subject);
      }
    }

    /// <summary>
    /// Executes a Transact-SQL statement against the connection 
    /// and returns the number of rows affected. 
    /// </summary>
    /// <param name="query">A <see cref="string"/> with a Transact-SQL statement.</param>
    /// <returns>For UPDATE, INSERT, and DELETE statements, the return value 
    /// is the number of rows affected by the command. 
    /// For all other types of statements, the return value is -1. 
    /// If a rollback occurs, the return value is also -1.</returns>
    public static int ExecuteSQLCommand(string query)
    {
      int retValue = -1;
      var command = new SQLiteCommand(query, Document.ActiveDocument.DocDataSet.DatabaseConnection);
      retValue = command.ExecuteNonQuery();

      return retValue;
    }

    /// <summary>
    /// This static method returns the column name of
    /// the primary key if there is any
    /// </summary>
    /// <param name="tableName">A <see cref="String"/> with the table in which 
    /// to get the primary key.</param>
    /// <returns>A <see cref="String"/> with the primary key column or an empty string
    /// if no primary key column is found.</returns>
    public static string GetPrimaryKey(string tableName)
    {
      string names = string.Empty;
      string primaryKeyColumnName = string.Empty;

      // sp_pkeys is SQL Server default stored procedure
      // you pass it only table Name, it will return
      // primary key column
      var command = new SQLiteCommand("sp_pkeys", Document.ActiveDocument.DocDataSet.DatabaseConnection);
      command.CommandType = CommandType.StoredProcedure;
      command.Parameters.Add("@table_name", DbType.String).Value = tableName;

      SQLiteDataReader reader = command.ExecuteReader();
      while (reader.Read())
      {
        // the primary key column resides at index 4 
        primaryKeyColumnName = reader[3].ToString();
      }

      return primaryKeyColumnName;
    }

    #endregion DatabaseAdminQueries

    #region DatabaseBulkCopy

    /// <summary>
    /// This method uses the <see cref="SqlBulkCopy"/> for fast
    /// copying the rawdata of the given subject 
    /// from OGAMAs database table to the database file (.mdf).
    /// </summary>
    /// <param name="subjectName">A <see cref="string"/> with the subject name.</param>
    public static void WriteRawDataWithBulkStatement(string subjectName)
    {
      var conn = Document.ActiveDocument.DocDataSet.DatabaseConnection;
      string tableName = subjectName + "Rawdata";
      var sbi = new SQLiteBulkInsert(conn, tableName);
      sbi.AddParameter("ID", DbType.Int64);
      sbi.AddParameter("SubjectName", DbType.String);
      sbi.AddParameter("TrialSequence", DbType.Int32);
      sbi.AddParameter("Time", DbType.Int64);
      sbi.AddParameter("PupilDiaX", DbType.Single);
      sbi.AddParameter("PupilDiaY", DbType.Single);
      sbi.AddParameter("GazePosX", DbType.Single);
      sbi.AddParameter("GazePosY", DbType.Single);
      sbi.AddParameter("MousePosX", DbType.Single);
      sbi.AddParameter("MousePosY", DbType.Single);
      sbi.AddParameter("EventID", DbType.Int32);

      var table = Document.ActiveDocument.DocDataSet.Tables[tableName];
      foreach (var row in table.Rows)
      {
        var rawRow = row as SQLiteOgamaDataSet.RawdataRow;
        sbi.Insert(new object[] { rawRow.ID, rawRow.SubjectName, rawRow.TrialSequence, rawRow.Time, rawRow.PupilDiaX, rawRow.PupilDiaY, rawRow.GazePosX, rawRow.GazePosY, rawRow.MousePosX, rawRow.MousePosY, rawRow.EventID });
      }
      sbi.Flush();

      //using (SqlBulkCopy bcp = new SqlBulkCopy(Document.ActiveDocument.DocDataSet.DatabaseConnection))
      //{
      //  bcp.BulkCopyTimeout = 6000;
      //  //// Write from the source to the destination.
      //  string tableName = subjectName + "Rawdata";
      //  bcp.DestinationTableName = "dbo." + tableName;
      //  bcp.WriteToServer(Document.ActiveDocument.DocDataSet.Tables[tableName]);
      //  bcp.Close();
      //}
    }

    /// <summary>
    /// This method uses the <see cref="SqlBulkCopy"/> for fast
    /// copying the rawdata of the given subjects raw data table
    /// to the database file (.mdf).
    /// </summary>
    /// <param name="subjectName">A <see cref="string"/> with the subject name.</param>
    /// <param name="rawDataTable">A <see cref="DataTable"/> with the subjects sampled data.</param>
    public static void WriteRawDataWithBulkStatement(string subjectName, DataTable rawDataTable)
    {
      var conn = Document.ActiveDocument.DocDataSet.DatabaseConnection;
      var sbi = new SQLiteBulkInsert(conn, rawDataTable.TableName);
      sbi.AddParameter("ID", DbType.Int64);
      sbi.AddParameter("SubjectName", DbType.String);
      sbi.AddParameter("TrialSequence", DbType.Int32);
      sbi.AddParameter("Time", DbType.Int64);
      sbi.AddParameter("PupilDiaX", DbType.Single);
      sbi.AddParameter("PupilDiaY", DbType.Single);
      sbi.AddParameter("GazePosX", DbType.Single);
      sbi.AddParameter("GazePosY", DbType.Single);
      sbi.AddParameter("MousePosX", DbType.Single);
      sbi.AddParameter("MousePosY", DbType.Single);
      sbi.AddParameter("EventID", DbType.Int32);

      foreach (var row in rawDataTable.Rows)
      {
        var rawRow = row as SQLiteOgamaDataSet.RawdataRow;
        sbi.Insert(new object[] { rawRow.ID, rawRow.SubjectName, rawRow.TrialSequence, rawRow.Time, rawRow.PupilDiaX, rawRow.PupilDiaY, rawRow.GazePosX, rawRow.GazePosY, rawRow.MousePosX, rawRow.MousePosY, rawRow.EventID });
      }
      sbi.Flush();

      //using (SqlBulkCopy bcp = new SqlBulkCopy(Document.ActiveDocument.DocDataSet.DatabaseConnection))
      //{
      //  bcp.BulkCopyTimeout = 300;
      //  //// Write from the source to the destination.
      //  string tableName = subjectName + "Rawdata";
      //  bcp.DestinationTableName = "dbo." + tableName;
      //  bcp.WriteToServer(rawDataTable);
      //  bcp.Close();
      //}
    }

    #endregion DatabaseBulkCopy

    #region DataSetUpdates

    /// <summary>
    /// This method writes a new <see cref="SubjectsData"/> information
    /// to the subjects data table of the dataset.
    /// </summary>
    /// <remarks>This change has to be afterwards written to the database file
    /// with a call to 
    /// <code>int affectedRows = Document.ActiveDocument.DocDataSet.TadSubjects.Update(
    /// Document.ActiveDocument.DocDataSet.Subjects);</code></remarks>
    /// <param name="subjectData">A <see cref="SubjectsData"/> with the 
    /// new subject information.</param>
    /// <returns><strong>True</strong>, if successful otherwise, <strong>false</strong>.</returns>
    public static bool WriteSubjectToDataSet(SubjectsData subjectData)
    {
      SQLiteOgamaDataSet.SubjectsRow workSubjectData;
      workSubjectData = Document.ActiveDocument.DocDataSet.Subjects.NewSubjectsRow();
      workSubjectData.SubjectName = subjectData.SubjectName;

      if (subjectData.Category == null)
      {
        workSubjectData.SetCategoryNull();
      }
      else
      {
        workSubjectData.Category = subjectData.Category;
      }

      if (subjectData.Age == null)
      {
        workSubjectData.SetAgeNull();
      }
      else
      {
        workSubjectData.Age = (int)subjectData.Age;
      }

      if (subjectData.Sex == null)
      {
        workSubjectData.SetSexNull();
      }
      else
      {
        workSubjectData.Sex = subjectData.Sex;
      }

      if (subjectData.Handedness == null)
      {
        workSubjectData.SetHandednessNull();
      }
      else
      {
        workSubjectData.Handedness = subjectData.Handedness;
      }

      if (subjectData.Comments == null)
      {
        workSubjectData.SetCommentsNull();
      }
      else
      {
        workSubjectData.Comments = subjectData.Comments;
      }

      try
      {
        Document.ActiveDocument.DocDataSet.Subjects.BeginLoadData();
        Document.ActiveDocument.DocDataSet.Subjects.AddSubjectsRow(workSubjectData);
        Document.ActiveDocument.DocDataSet.Subjects.EndLoadData();
      }
      catch (Exception ex)
      {
        ExceptionMethods.HandleException(ex);

        return false;
      }

      return true;
    }

    /// <summary>
    /// This method writes a new <see cref="List{TrialsData}"/> information
    /// to the trials data table of the dataset.
    /// </summary>
    /// <remarks>This change has to be afterwards written to the database file
    /// with a call to 
    /// <code>int affectedRows = Document.ActiveDocument.DocDataSet.TadTrials.Update(
    /// Document.ActiveDocument.DocDataSet.Trials);</code></remarks>
    /// <param name="lstTrialData">A <see cref="List{TrialsData}"/> with the 
    /// new trials information.</param>
    /// <returns><strong>True</strong>, if successful otherwise, <strong>false</strong>.</returns>
    public static bool WriteTrialsDataListToDataSet(List<TrialsData> lstTrialData)
    {
      // Notify start loading data into the dataset.
      Document.ActiveDocument.DocDataSet.Trials.BeginLoadData();
      try
      {
        foreach (TrialsData trial in lstTrialData)
        {
          // Check for duplicate entries
          if (Document.ActiveDocument.DocDataSet.Trials.Count(o => o.SubjectName == trial.SubjectName && o.TrialID == trial.TrialID && o.TrialSequence == trial.TrialSequence) > 0)
          {
            continue;
          }

          // Insert Data in Trials Table
          var workTrialData = Document.ActiveDocument.DocDataSet.Trials.NewTrialsRow();
          workTrialData.SubjectName = trial.SubjectName;
          workTrialData.TrialID = trial.TrialID;
          workTrialData.TrialName = trial.TrialName;
          workTrialData.TrialSequence = trial.TrialSequence;
          workTrialData.Category = trial.Category;
          workTrialData.TrialStartTime = trial.TrialStartTime;
          workTrialData.Duration = trial.Duration;
          if (trial.EliminateData.HasValue)
          {
            workTrialData.EliminateData = "Yes";
          }

          Document.ActiveDocument.DocDataSet.Trials.AddTrialsRow(workTrialData);
        }
      }
      catch (Exception ex)
      {
        ExceptionMethods.HandleException(ex);

        return false;
      }
      finally
      {
        // Notify the end of loading data into the dataset.
        Document.ActiveDocument.DocDataSet.Trials.EndLoadData();
      }

      return true;
    }

    /// <summary>
    /// This method writes a new <see cref="List{TrialEventsData}"/> information
    /// to the trial events data table of the dataset.
    /// </summary>
    /// <remarks>This change has to be afterwards written to the database file
    /// with a call to 
    /// <code>int affectedRows = Document.ActiveDocument.DocDataSet.TadTrialEvents.Update(
    /// Document.ActiveDocument.DocDataSet.TrialEvents);</code></remarks>
    /// <param name="lstTrialEventsData">A <see cref="List{TrialEventsData}"/> with the 
    /// new trial events information.</param>
    /// <returns><strong>True</strong>, if successful otherwise, <strong>false</strong>.</returns>
    public static bool WriteTrialEventsDataListToDataSet(List<TrialEventsData> lstTrialEventsData)
    {
      // Notify start loading data into the dataset.
      Document.ActiveDocument.DocDataSet.TrialEvents.BeginLoadData();
      try
      {
        foreach (TrialEventsData trial in lstTrialEventsData)
        {
          // Insert Data in Trials Table
          SQLiteOgamaDataSet.TrialEventsRow workTrialEventsData;
          workTrialEventsData = Document.ActiveDocument.DocDataSet.TrialEvents.NewTrialEventsRow();
          workTrialEventsData.SubjectName = trial.SubjectName;
          workTrialEventsData.EventID = trial.EventID;
          workTrialEventsData.EventParam = trial.EventParam;
          workTrialEventsData.EventTask = trial.EventTask;
          workTrialEventsData.EventTime = trial.EventTime;
          workTrialEventsData.EventType = trial.EventType;
          workTrialEventsData.TrialSequence = trial.TrialSequence;

          Document.ActiveDocument.DocDataSet.TrialEvents.AddTrialEventsRow(workTrialEventsData);
        }
      }
      catch (Exception ex)
      {
        ExceptionMethods.HandleException(ex);

        return false;
      }
      finally
      {
        // Notify the end of loading data into the dataset.
        Document.ActiveDocument.DocDataSet.TrialEvents.EndLoadData();
      }

      return true;
    }

    /// <summary>
    /// This method writes a new <see cref="List{TrialEvent}"/> information
    /// to the trial events table of the dataset.
    /// </summary>
    /// <remarks>This change has to be afterwards written to the database file
    /// with a call to 
    /// <code>int affectedRows = Document.ActiveDocument.DocDataSet.TadTrialEvents.Update(
    /// Document.ActiveDocument.DocDataSet.TrialEvents);</code></remarks>
    /// <param name="trialEventList">A <see cref="List{TrialEvent}"/> with the 
    /// new trial event information.</param>
    /// <returns><strong>True</strong>, if successful otherwise, <strong>false</strong>.</returns>
    public static bool WriteTrialEventsToDataSet(List<TrialEvent> trialEventList)
    {
      if (trialEventList == null)
      {
        return true;
      }

      // Notify start loading data into the dataset.
      Document.ActiveDocument.DocDataSet.TrialEvents.BeginLoadData();
      try
      {
        foreach (TrialEvent trialEvent in trialEventList.Where(trialEvent => trialEvent != null))
        {
          // Insert Data in Trials Table
          var workTrialEventRow = Document.ActiveDocument.DocDataSet.TrialEvents.NewTrialEventsRow();
          workTrialEventRow.EventID = trialEvent.EventID;
          workTrialEventRow.EventParam = trialEvent.Param;
          workTrialEventRow.SubjectName = trialEvent.SubjectName;
          workTrialEventRow.EventTime = trialEvent.Time;
          workTrialEventRow.TrialSequence = trialEvent.TrialSequence;
          if (trialEvent is MediaEvent)
          {
            workTrialEventRow.EventTask = ((MediaEvent)trialEvent).Task.ToString();
          }
          else if (trialEvent is InputEvent)
          {
            workTrialEventRow.EventTask = ((InputEvent)trialEvent).Task.ToString();
          }

          workTrialEventRow.EventType = trialEvent.Type.ToString();

          Document.ActiveDocument.DocDataSet.TrialEvents.AddTrialEventsRow(workTrialEventRow);
        }
      }
      catch (Exception ex)
      {
        ExceptionMethods.HandleException(ex);

        return false;
      }
      finally
      {
        // Notify the end of loading data into the dataset.
        Document.ActiveDocument.DocDataSet.TrialEvents.EndLoadData();
      }

      return true;
    }

    /// <summary>
    /// This method writes a new <see cref="List{RawData}"/> information
    /// to the trials data table of the dataset.
    /// </summary>
    /// <remarks>This change has to be afterwards written to the database file
    /// with a call to 
    /// <code>Queries.CreateRawDataTableInDB(subjectName);
    /// Queries.WriteRawDataWithBulkStatement(subjectName);</code></remarks>
    /// <param name="subjectName">A <see cref="string"/> with the subject name.</param>
    /// <param name="lstRawData">A <see cref="List{RawData}"/> with the 
    /// new raw data.</param>
    /// <returns><strong>True</strong>, if successful otherwise, <strong>false</strong>.</returns>
    public static bool WriteRawDataListToDataSet(string subjectName, List<RawData> lstRawData)
    {
      // Create Subjects rawdata table
      var subjectRawDataTable = new SQLiteOgamaDataSet.RawdataDataTable();

      // Give it correct name
      subjectRawDataTable.TableName = subjectName + "Rawdata";
      try
      {
        SaveDataToTable(lstRawData.ToArray(), subjectRawDataTable);

        // Add the raw data table to the DataTableCollection.
        Document.ActiveDocument.DocDataSet.Tables.Add(subjectRawDataTable);
      }
      catch (Exception ex)
      {
        ExceptionMethods.HandleException(ex);

        return false;
      }
      finally
      {
        subjectRawDataTable.Dispose();
      }

      return true;
    }

    /// <summary>
    /// This method saves the given collection of <see cref="RawData"/> into the
    /// given <see cref="SQLiteOgamaDataSet.RawdataDataTable"/>
    /// </summary>
    /// <param name="lstRawData">A <see cref="RawData"/> with the new samples.</param>
    /// <param name="subjectRawDataTable">The <see cref="SQLiteOgamaDataSet.RawdataDataTable"/> to be filled.</param>
    /// <returns><strong>True</strong> if successful otherwise <strong>false</strong>.</returns>
    public static bool SaveDataToTable(RawData[] lstRawData, SQLiteOgamaDataSet.RawdataDataTable subjectRawDataTable)
    {
      try
      {
        // Notify the beginning of adding rows to subjectRawDataTable
        subjectRawDataTable.BeginLoadData();

        foreach (RawData data in lstRawData)
        {
          // Save data into datarow.
          SQLiteOgamaDataSet.RawdataRow workRowRawData =
            subjectRawDataTable.NewRawdataRow();

          workRowRawData.SubjectName = data.SubjectName;
          workRowRawData.TrialSequence = data.TrialSequence;
          workRowRawData.Time = data.Time;

          if (!data.PupilDiaX.HasValue)
          {
            workRowRawData.SetPupilDiaXNull();
          }
          else
          {
            workRowRawData.PupilDiaX = data.PupilDiaX.Value;
          }

          if (!data.PupilDiaY.HasValue)
          {
            workRowRawData.SetPupilDiaYNull();
          }
          else
          {
            workRowRawData.PupilDiaY = data.PupilDiaY.Value;
          }

          if (!data.GazePosX.HasValue)
          {
            workRowRawData.SetGazePosXNull();
          }
          else
          {
            workRowRawData.GazePosX = data.GazePosX.Value;
          }

          if (!data.GazePosY.HasValue)
          {
            workRowRawData.SetGazePosYNull();
          }
          else
          {
            workRowRawData.GazePosY = data.GazePosY.Value;
          }

          if (!data.MousePosX.HasValue)
          {
            workRowRawData.SetMousePosXNull();
          }
          else
          {
            workRowRawData.MousePosX = data.MousePosX.Value;
          }

          if (!data.MousePosY.HasValue)
          {
            workRowRawData.SetMousePosYNull();
          }
          else
          {
            workRowRawData.MousePosY = data.MousePosY.Value;
          }

          if (!data.EventID.HasValue)
          {
            workRowRawData.SetEventIDNull();
          }
          else
          {
            workRowRawData.EventID = data.EventID.Value;
          }

          subjectRawDataTable.AddRawdataRow(workRowRawData);
        }

        subjectRawDataTable.EndLoadData();
      }
      catch (Exception ex)
      {
        ExceptionMethods.HandleException(ex);

        return false;
      }

      return true;
    }

    /// <summary>
    /// This method writes a new <see cref="List{AOIData}"/> information
    /// to the AOI data table of the dataset.
    /// </summary>
    /// <remarks>This change has to be afterwards written to the database file
    /// with a call to 
    /// <code>int affectedRows = Document.ActiveDocument.DocDataSet.TadAOIs.Update(
    /// Document.ActiveDocument.DocDataSet.AOIs);</code></remarks>
    /// <param name="lstAOIData">A <see cref="List{AOIData}"/> with the 
    /// new AOI information.</param>
    /// <param name="splashDialog">A <see cref="System.ComponentModel.BackgroundWorker"/> with the 
    /// background splash loading dialog worker which shows progress.</param>
    /// <returns><strong>True</strong>, if successful otherwise, <strong>false</strong>.</returns>
    public static bool WriteAOIDataListToDataSet(List<AOIData> lstAOIData, System.ComponentModel.BackgroundWorker splashDialog)
    {
      // Notify start loading data into the dataset.
      Document.ActiveDocument.DocDataSet.AOIs.BeginLoadData();
      try
      {
        foreach (AOIData aoi in lstAOIData)
        {
          WriteAOIDataToDataSet(aoi, splashDialog);
        }
      }
      catch (Exception ex)
      {
        ExceptionMethods.HandleException(ex);

        return false;
      }
      finally
      {
        // Notify the end of loading data into the dataset.
        Document.ActiveDocument.DocDataSet.AOIs.EndLoadData();
      }

      return true;
    }

    /// <summary>
    /// This method writes a new <see cref="AOIData"/> information
    /// to the AOI data table of the dataset.
    /// </summary>
    /// <remarks>This change has to be afterwards written to the database file
    /// with a call to 
    /// <code>int affectedRows = Document.ActiveDocument.DocDataSet.TadAOIs.Update(
    /// Document.ActiveDocument.DocDataSet.AOIs);</code></remarks>
    /// <param name="aoi">A <see cref="AOIData"/> with the 
    /// new AOI information.</param>
    /// <param name="splashDialog">A <see cref="System.ComponentModel.BackgroundWorker"/> with the 
    /// background splash loading dialog worker which shows progress.</param>
    public static bool WriteAOIDataToDataSet(AOIData aoi, System.ComponentModel.BackgroundWorker splashDialog)
    {
      // Validate AOIData
      if (!ValidateShape(ref aoi, splashDialog))
      {
        return false;
      }

      // Insert Data in AOI Table
      SQLiteOgamaDataSet.AOIsRow workAOIData;
      workAOIData = Document.ActiveDocument.DocDataSet.AOIs.NewAOIsRow();
      workAOIData.ShapeName = aoi.ShapeName;
      workAOIData.ShapeNumPts = aoi.ShapeNumPts;
      workAOIData.ShapePts = aoi.ShapePts;
      workAOIData.ShapeType = aoi.ShapeType.ToString().Replace("VectorGraphics.VG", string.Empty);
      workAOIData.TrialID = aoi.TrialID;
      workAOIData.ShapeGroup = aoi.Group;
      workAOIData.SlideNr = aoi.SlideNr;

      Document.ActiveDocument.DocDataSet.AOIs.AddAOIsRow(workAOIData);

      return true;
    }

    /// <summary>
    /// This method writes a new <see cref="List{FixationData}"/> information
    /// to the given fixations data table of the dataset.
    /// </summary>
    /// <remarks>This change has to be afterwards written to the database file
    /// with a call to 
    /// <code>int affectedRows = Document.ActiveDocument.DocDataSet.TadAOIs.Update(
    /// Document.ActiveDocument.DocDataSet.AOIs);</code></remarks>
    /// <param name="sampleType">A <see cref="SampleType"/> of which the sampling
    /// data is of. That switches the fixations table: gaze or mouse.</param>
    /// <param name="lstFixations">A <see cref="List{AOIData}"/> with the 
    /// new AOI information.</param>
    /// <returns><strong>True</strong>, if successful otherwise, <strong>false</strong>.</returns>
    public static bool WriteFixationListToDataSet(SampleType sampleType, List<FixationData> lstFixations)
    {
      // Get all subjects in the list
      var subjects = new List<string>();
      foreach (FixationData fix in lstFixations)
      {
        if (!subjects.Contains(fix.SubjectName))
        {
          subjects.Add(fix.SubjectName);
        }
      }

      // Delete existing fixation entrys for the subjects
      foreach (string subject in subjects)
      {
        switch (sampleType)
        {
          case SampleType.Gaze:
            int deleted = Document.ActiveDocument.DocDataSet.GazeFixationsAdapter.DeleteBySubject(subject);
            Document.ActiveDocument.DocDataSet.GazeFixationsAdapter.Fill(Document.ActiveDocument.DocDataSet.GazeFixations);
            break;
          case SampleType.Mouse:
            deleted = Document.ActiveDocument.DocDataSet.MouseFixationsAdapter.DeleteBySubject(subject);
            Document.ActiveDocument.DocDataSet.MouseFixationsAdapter.Fill(Document.ActiveDocument.DocDataSet.MouseFixations);
            break;
        }
      }

      switch (sampleType)
      {
        case SampleType.Gaze:
          // Notify start loading data into the dataset.
          Document.ActiveDocument.DocDataSet.GazeFixations.BeginLoadData();
          try
          {
            foreach (FixationData fixation in lstFixations)
            {
              // Insert Data in gaze fixations Table
              SQLiteOgamaDataSet.GazeFixationsRow workFixationsData;
              workFixationsData = Document.ActiveDocument.DocDataSet.GazeFixations.NewGazeFixationsRow();
              workFixationsData.SubjectName = fixation.SubjectName;
              workFixationsData.TrialID = fixation.TrialID;
              workFixationsData.TrialSequence = fixation.TrialSequence;
              workFixationsData.CountInTrial = fixation.CountInTrial;
              workFixationsData.StartTime = fixation.StartTime;
              workFixationsData.Length = fixation.Length;
              workFixationsData.PosX = (int)fixation.PosX;
              workFixationsData.PosY = (int)fixation.PosY;

              Document.ActiveDocument.DocDataSet.GazeFixations.AddGazeFixationsRow(workFixationsData);
            }
          }
          catch (Exception ex)
          {
            ExceptionMethods.HandleException(ex);

            return false;
          }
          finally
          {
            // Notify the end of loading data into the dataset.
            Document.ActiveDocument.DocDataSet.GazeFixations.EndLoadData();
          }

          return true;
        case SampleType.Mouse:
          Document.ActiveDocument.DocDataSet.MouseFixations.BeginLoadData();
          try
          {
            foreach (FixationData fixation in lstFixations)
            {
              // Insert Data in mouse fixations Table
              SQLiteOgamaDataSet.MouseFixationsRow workFixationsData;
              workFixationsData = Document.ActiveDocument.DocDataSet.MouseFixations.NewMouseFixationsRow();
              workFixationsData.SubjectName = fixation.SubjectName;
              workFixationsData.TrialID = fixation.TrialID;
              workFixationsData.TrialSequence = fixation.TrialSequence;
              workFixationsData.CountInTrial = fixation.CountInTrial;
              workFixationsData.StartTime = fixation.StartTime;
              workFixationsData.Length = fixation.Length;
              workFixationsData.PosX = (int)fixation.PosX;
              workFixationsData.PosY = (int)fixation.PosY;

              Document.ActiveDocument.DocDataSet.MouseFixations.AddMouseFixationsRow(workFixationsData);
            }
          }
          catch (Exception ex)
          {
            ExceptionMethods.HandleException(ex);

            return false;
          }
          finally
          {
            // Notify the end of loading data into the dataset.
            Document.ActiveDocument.DocDataSet.MouseFixations.EndLoadData();
          }

          return true;
      }

      return false;
    }

    #endregion //DataSetUpdates

    #endregion //METHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER

    /// <summary>
    /// This method enables/disables the constraints in the dbo.Trials table
    /// </summary>
    /// <param name="enable">Flag. <strong>True</strong> if constraints should
    /// be enabled, otherwise <strong>false</strong>.</param>
    public static void SetTrialConstraints(bool enable)
    {
      string addon = enable ? string.Empty : "NO";
      string queryString = "ALTER TABLE Trials " + addon + "CHECK CONSTRAINT ALL;";

      var command = new SQLiteCommand(queryString, Document.ActiveDocument.DocDataSet.DatabaseConnection);
      SQLiteDataReader reader = command.ExecuteReader();
      try
      {
        while (reader.Read())
        {
          Console.WriteLine(string.Format("{0}, {1}", reader[0], reader[1]));
        }
      }
      finally
      {
        // Always call Close when done reading.
        reader.Close();
      }
    }

    /// <summary>
    /// This method checks whether given subject already exists in the
    /// database by calling the <code>TadSubjects.GetDataBySubject(subjectName)</code>
    /// method.
    /// </summary>
    /// <param name="subjectName">A <see cref="string"/> with the subject name.</param>
    /// <returns><strong>True</strong>, if successful, otherwise <strong>false</strong>.</returns>
    public static bool CheckDatabaseForExistingSubject(string subjectName)
    {
      // Check for existing subject data
      SQLiteOgamaDataSet.SubjectsDataTable subjectsTable =
        Document.ActiveDocument.DocDataSet.SubjectsAdapter.GetDataBySubject(subjectName);
      if (subjectsTable.Rows.Count > 0)
      {
        string message = "A subject with this name is already present in the " +
          "experiments database. This name has to be unique.";

        InformationDialog.Show(
          "Please change the subject name.",
          message,
          false,
          MessageBoxIcon.Information);

        return true;
      }

      return false;
    }

    /// <summary>
    /// This method checks the given <see cref="AOIData"/> shape for invalid characters
    /// and tests if it is already used in the database.
    /// It shows referring message boxes when the name is not valid
    /// and automatically removes invalid characters.
    /// </summary>
    /// <param name="shape">Ref. A <see cref="AOIData"/> with
    /// the area of interest to test.</param>
    /// <param name="splashDialog">A <see cref="System.ComponentModel.BackgroundWorker"/> with the 
    /// background splash loading dialog worker which shows progress.</param>
    /// <returns><strong>True</strong>, if the AOI is valid, 
    /// otherwise <strong>false</strong>.</returns>
    public static bool ValidateShape(ref AOIData shape, System.ComponentModel.BackgroundWorker splashDialog)
    {
      shape.ShapeName.Trim();

      StringBuilder sb = new StringBuilder();
      bool changed = false;
      for (int i = 0; i < shape.ShapeName.Length; i++)
      {
        if (char.IsLetterOrDigit(shape.ShapeName[i]))
        {
          sb.Append(shape.ShapeName[i]);
        }
        else
        {
          changed = true;
        }
      }

      shape.ShapeName = sb.ToString();

      if (shape.ShapeName == string.Empty)
      {
        if (splashDialog != null && splashDialog.IsBusy)
        {
          splashDialog.CancelAsync();
        }

        InformationDialog.Show(
          "Empty shape name",
          "Please enter at least one character for the shape name",
          false,
          MessageBoxIcon.Information);

        // Show loading splash screen if it is not running
        if (splashDialog != null && !splashDialog.IsBusy)
        {
          splashDialog.RunWorkerAsync();
        }

        return false;
      }
      else if (IOHelpers.IsNumeric(shape.ShapeName[0]))
      {
        if (splashDialog != null && splashDialog.IsBusy)
        {
          splashDialog.CancelAsync();
        }

        InformationDialog.Show(
          "Invalid shape name",
          "Please do not use a number for the first character of the shape name.",
          false,
          MessageBoxIcon.Information);

        // Show loading splash screen if it is not running
        if (splashDialog != null && !splashDialog.IsBusy)
        {
          splashDialog.RunWorkerAsync();
        }

        return false;
      }
      else
      {
        if (changed)
        {
          if (splashDialog != null && splashDialog.IsBusy)
          {
            splashDialog.CancelAsync();
          }

          string message = "Please note: Non letter and non digit characters are removed from the shape name." +
            Environment.NewLine + "Other characters are not allowed for the database entry.";

          ExceptionMethods.ProcessMessage("Shape name modified", message);

          // Show loading splash screen if it is not running
          if (splashDialog != null && !splashDialog.IsBusy)
          {
            splashDialog.RunWorkerAsync();
          }

          return true;
        }

        if (!CheckForAOIDuplicate(shape, splashDialog))
        {
          return false;
        }
      }

      return true;
    }

    /// <summary>
    /// This method checks the given subject name for invalid characters
    /// and tests if it is already used in the database.
    /// It shows referring message boxes when the name is not valid
    /// and automatically removes invalid characters.
    /// </summary>
    /// <param name="subjectName">Ref. A <see cref="string"/> with
    /// the subject name to test.</param>
    /// <param name="silent"><strong>True</strong> if no errors should be thrown,
    /// otherwise <strong>false</strong>. (Verbose mode)</param>
    /// <returns><strong>True</strong>, if the subject name is valid, 
    /// otherwise <strong>false</strong>.</returns>
    public static bool ValidateSubjectName(ref string subjectName, bool silent)
    {
      subjectName.Trim();

      StringBuilder sb = new StringBuilder();
      bool changed = false;
      for (int i = 0; i < subjectName.Length; i++)
      {
        if (char.IsLetterOrDigit(subjectName[i]))
        {
          sb.Append(subjectName[i]);
        }
        else
        {
          changed = true;
        }
      }

      subjectName = sb.ToString();

      if (subjectName == string.Empty)
      {
        if (!silent)
        {
          ExceptionMethods.ProcessMessage(
            "Empty subject name",
            "Please enter at least one character for the subject name");
        }

        return false;
      }
      else if (IOHelpers.IsNumeric(subjectName[0]))
      {
        if (!silent)
        {
          ExceptionMethods.ProcessMessage(
            "Invalid subject name",
            "Please do not use a number for the first character of the subject name.");
        }

        return false;
      }
      else
      {
        if (changed)
        {
          if (!silent)
          {
            string message = "Please note: Non letter and non digit characters are removed " +
              "from the subject name." +
              Environment.NewLine + "Other characters are not allowed for the database entry.";

            ExceptionMethods.ProcessMessage("Invalid subject name", message);
          }

          return false;
        }

        SQLiteOgamaDataSet.SubjectsDataTable subjectsTable =
          Document.ActiveDocument.DocDataSet.SubjectsAdapter.GetDataBySubject(subjectName);

        if (subjectsTable.Rows.Count > 0)
        {
          if (!silent)
          {
            string message = "A subject with this name is already present in the " +
               "experiments database. This name has to be unique." + Environment.NewLine +
               "Please change the subject name.";
            ExceptionMethods.ProcessMessage("Subject already present", message);
          }

          return false;
        }
      }

      return true;
    }

    /// <summary>
    /// This method checks if the given subject name is already used in the database.
    /// It shows referring message boxes when the name is not there.
    /// </summary>
    /// <param name="subjectName">A <see cref="string"/> with
    /// the subject name to test.</param>
    /// <returns><strong>True</strong>, if the subject name is already used, 
    /// otherwise <strong>false</strong>.</returns>
    public static bool IsSubjectNameInDatabase(string subjectName)
    {
      SQLiteOgamaDataSet.SubjectsDataTable subjectsTable =
        Document.ActiveDocument.DocDataSet.SubjectsAdapter.GetDataBySubject(subjectName);
      if (subjectsTable.Rows.Count == 0)
      {
        string message = "A subject with this name is not present in the " +
          "experiments database." + Environment.NewLine +
          "Please change the subject name.";
        ExceptionMethods.ProcessMessage("Subject not present", message);
        return false;
      }

      return true;
    }

    /// <summary>
    /// This method checks the given column name for invalid characters.
    /// It shows referring message boxes when the name is not valid
    /// and automatically removes invalid characters.
    /// </summary>
    /// <param name="columnName">Ref. A <see cref="string"/> with
    /// the column name to test.</param>
    /// <returns><strong>True</strong>, if the column name is valid, 
    /// otherwise <strong>false</strong>.</returns>
    public static bool ValidateColumnName(ref string columnName)
    {
      columnName.Trim();

      StringBuilder sb = new StringBuilder();
      bool changed = false;
      for (int i = 0; i < columnName.Length; i++)
      {
        if (char.IsLetterOrDigit(columnName[i]))
        {
          sb.Append(columnName[i]);
        }
        else
        {
          changed = true;
        }
      }

      columnName = sb.ToString();

      if (columnName == string.Empty)
      {
        ExceptionMethods.ProcessMessage("Empty name", "Please enter at least one character for the subject name");
        return false;
      }
      else if (IOHelpers.IsNumeric(columnName[0]))
      {
        ExceptionMethods.ProcessMessage("Invalid name", "Please do not use a number for the first character of the subject name.");
        return false;
      }
      else if (changed)
      {
        string message = "Please note: Non letter and non digit characters are removed from the subject name." +
          Environment.NewLine + "Other characters are not allowed for the database entry.";
        ExceptionMethods.ProcessMessage("Name modified", message);
        return false;
      }

      return true;
    }

    /// <summary>
    /// This method returns an default colored <see cref="VGElement"/>
    /// that represents the object in the database described by the three parameters
    /// </summary>
    /// <param name="shapeType">A <see cref="String"/> with the shape type Rectangle, Ellipse or Polyline</param>
    /// <param name="shapeName">A <see cref="String"/> with the shape name</param>
    /// <param name="shapeGroup">A <see cref="String"/> with the shapes group</param>
    /// <param name="strPtList">A <see cref="String"/> with the list of points to be converted
    /// with <see cref="ObjectStringConverter.StringToPointFList(String)"/></param>
    /// <returns>A <see cref="VGElement"/> that represents the object in the database described by the three parameters.</returns>
    public static VGElement GetVGElementFromDatabase(
      string shapeType,
      string shapeName,
      string shapeGroup,
      string strPtList)
    {
      // Create the shape depending on ShapeType
      RectangleF boundingRect = new RectangleF();
      List<PointF> pointList = ObjectStringConverter.StringToPointFList(strPtList);

      switch (shapeType)
      {
        case "Rectangle":
          boundingRect.Location = pointList[0];
          boundingRect.Width = pointList[2].X - pointList[0].X;
          boundingRect.Height = pointList[2].Y - pointList[0].Y;

          // Create Rect with defined stroke
          VGRectangle newRect = new VGRectangle(
            ShapeDrawAction.NameAndEdge,
            Pens.Red,
            SystemFonts.MenuFont,
            Color.Black,
            boundingRect,
            VGStyleGroup.None,
            shapeName,
            shapeGroup);
          newRect.TextAlignment = VGAlignment.Center;
          return newRect;
        case "Ellipse":
          boundingRect.Location = pointList[0];
          boundingRect.Width = pointList[2].X - pointList[0].X;
          boundingRect.Height = pointList[2].Y - pointList[0].Y;

          // Create Rect with defined stroke
          VGEllipse newEllipse = new VGEllipse(
            ShapeDrawAction.NameAndEdge,
            Pens.Red,
            SystemFonts.MenuFont,
            Color.Black,
            boundingRect,
            VGStyleGroup.None,
            shapeName,
            shapeGroup);
          newEllipse.TextAlignment = VGAlignment.Center;
          return newEllipse;
        case "Polyline":
          // Create Polyline with defined stroke
          VGPolyline newPolyline = new VGPolyline(
            ShapeDrawAction.NameAndEdge,
            Pens.Red,
            SystemFonts.MenuFont,
            Color.Black,
            VGStyleGroup.None,
            shapeName,
            shapeGroup);
          newPolyline.TextAlignment = VGAlignment.Center;
          foreach (PointF point in pointList)
          {
            newPolyline.AddPt(point);
          }

          newPolyline.ClosePolyline();
          boundingRect = newPolyline.Bounds;
          return newPolyline;
      }

      return null;
    }

    /// <summary>
    /// Extracts the events in a sorted trial events list for the given subject and trial
    /// events data table.
    /// </summary>
    /// <param name="subjectName">A <see cref="String"/> with the subject name.</param>
    /// <param name="table">The trial events data table containing the data from
    /// the database, can be for one or more trials.</param>
    /// <param name="usercamID">Out. The event id of the user camera start time.</param>
    /// <returns>A <see cref="SortedList{Int32, TrialEvent}"/>
    /// with the events occured in this experiment.</returns>
    private static SortedList<int, TrialEvent> ExtractEvents(
      string subjectName,
      DataTable table,
      out int usercamID)
    {
      usercamID = -1;
      int lastTrialSequence = -1;
      long timeToAdd = 0;
      SortedList<int, TrialEvent> returnList = new SortedList<int, TrialEvent>();
      if (table.Rows.Count > 0)
      {
        lastTrialSequence = Convert.ToInt32(table.Rows[0]["TrialSequence"]);
      }

      foreach (DataRow row in table.Rows)
      {
        string typeString = row["EventType"].ToString();
        EventType type = EventType.None;
        try
        {
          type = (EventType)Enum.Parse(typeof(EventType), typeString, true);
        }
        catch (ArgumentException)
        {
          continue;
        }

        int trialSequence = Convert.ToInt32(row["TrialSequence"]);
        if (trialSequence != lastTrialSequence)
        {
          DataTable trialTable =
  Document.ActiveDocument.DocDataSet.TrialsAdapter.GetDataBySubjectAndSequence(subjectName, trialSequence);

          // Check for deleted trials, but left raw data...
          if (trialTable.Rows.Count == 0)
          {
            continue;
          }

          lastTrialSequence = trialSequence;
          long trialStartTime = Convert.ToInt64(trialTable.Rows[0]["TrialStartTime"]);
          timeToAdd = trialStartTime;
        }

        int eventID = Convert.ToInt32(row["EventID"]);
        long time = Convert.ToInt64(row["EventTime"]) + timeToAdd;
        string taskString = row["EventTask"].ToString();
        string param = row["EventParam"].ToString();

        TrialEvent newEvent = null;

        switch (type)
        {
          case EventType.None:
            break;
          case EventType.Mouse:
            newEvent = new InputEvent();
            newEvent.SubjectName = subjectName;
            newEvent.TrialSequence = trialSequence;
            newEvent.EventID = eventID;
            newEvent.Param = param;
            ((InputEvent)newEvent).Task = (InputEventTask)Enum.Parse(typeof(InputEventTask), taskString, true);
            newEvent.Time = time;
            newEvent.Type = type;
            returnList.Add(eventID, newEvent);
            break;
          case EventType.Key:
            newEvent = new InputEvent();
            newEvent.SubjectName = subjectName;
            newEvent.TrialSequence = trialSequence;
            newEvent.EventID = eventID;
            newEvent.Param = param;
            ((InputEvent)newEvent).Task = (InputEventTask)Enum.Parse(typeof(InputEventTask), taskString, true);
            newEvent.Time = time;
            newEvent.Type = type;
            returnList.Add(eventID, newEvent);
            break;
          case EventType.Slide:
            newEvent = new MediaEvent();
            newEvent.SubjectName = subjectName;
            newEvent.TrialSequence = trialSequence;
            newEvent.EventID = eventID;
            newEvent.Param = param;
            ((MediaEvent)newEvent).Task = (MediaEventTask)Enum.Parse(typeof(MediaEventTask), taskString, true);
            newEvent.Time = time;
            newEvent.Type = type;
            returnList.Add(eventID, newEvent);
            break;
          case EventType.Flash:
            newEvent = new MediaEvent();
            newEvent.SubjectName = subjectName;
            newEvent.TrialSequence = trialSequence;
            newEvent.EventID = eventID;
            newEvent.Param = param;
            ((MediaEvent)newEvent).Task = (MediaEventTask)Enum.Parse(typeof(MediaEventTask), taskString, true);
            newEvent.Time = time;
            newEvent.Type = type;
            returnList.Add(eventID, newEvent);
            break;
          case EventType.Audio:
            newEvent = new MediaEvent();
            newEvent.SubjectName = subjectName;
            newEvent.TrialSequence = trialSequence;
            newEvent.EventID = eventID;
            newEvent.Param = param;
            ((MediaEvent)newEvent).Task = (MediaEventTask)Enum.Parse(typeof(MediaEventTask), taskString, true);
            newEvent.Time = time;
            newEvent.Type = type;
            returnList.Add(eventID, newEvent);
            break;
          case EventType.Video:
            newEvent = new MediaEvent();
            newEvent.SubjectName = subjectName;
            newEvent.TrialSequence = trialSequence;
            newEvent.EventID = eventID;
            newEvent.Param = param;
            ((MediaEvent)newEvent).Task = (MediaEventTask)Enum.Parse(typeof(MediaEventTask), taskString, true);
            newEvent.Time = time;
            newEvent.Type = type;
            returnList.Add(eventID, newEvent);
            break;
          case EventType.Usercam:
            newEvent = new MediaEvent();
            newEvent.SubjectName = subjectName;
            newEvent.TrialSequence = trialSequence;
            newEvent.EventID = eventID;
            newEvent.Param = param;
            ((MediaEvent)newEvent).Task = (MediaEventTask)Enum.Parse(typeof(MediaEventTask), taskString, true);
            newEvent.Time = time;
            newEvent.Type = type;
            usercamID = eventID;
            returnList.Add(eventID, newEvent);
            break;
          case EventType.Response:
            newEvent = new InputEvent();
            newEvent.SubjectName = subjectName;
            newEvent.TrialSequence = trialSequence;
            newEvent.EventID = eventID;
            newEvent.Param = param;
            ((InputEvent)newEvent).Task = (InputEventTask)Enum.Parse(typeof(InputEventTask), taskString, true);
            newEvent.Time = time;
            newEvent.Type = type;
            if (!returnList.ContainsKey(eventID))
            {
              returnList.Add(eventID, newEvent);
            }
            break;
          case EventType.Marker:
            newEvent = new MediaEvent();
            newEvent.SubjectName = subjectName;
            newEvent.TrialSequence = trialSequence;
            newEvent.EventID = eventID;
            newEvent.Param = param;
            ((MediaEvent)newEvent).Task = (MediaEventTask)Enum.Parse(typeof(MediaEventTask), taskString, true);
            newEvent.Time = time;
            newEvent.Type = type;
            returnList.Add(eventID, newEvent);
            break;
          case EventType.Scroll:
            newEvent = new MediaEvent();
            newEvent.SubjectName = subjectName;
            newEvent.TrialSequence = trialSequence;
            newEvent.EventID = eventID;
            newEvent.Param = param;
            ((MediaEvent)newEvent).Task = (MediaEventTask)Enum.Parse(typeof(MediaEventTask), taskString, true);
            newEvent.Time = time;
            newEvent.Type = type;
            returnList.Add(eventID, newEvent);
            break;
          case EventType.Webpage:
            newEvent = new MediaEvent();
            newEvent.SubjectName = subjectName;
            newEvent.TrialSequence = trialSequence;
            newEvent.EventID = eventID;
            newEvent.Param = param;
            ((MediaEvent)newEvent).Task = (MediaEventTask)Enum.Parse(typeof(MediaEventTask), taskString, true);
            newEvent.Time = time;
            newEvent.Type = type;
            returnList.Add(eventID, newEvent);
            break;
          default:
            break;
        }
      }

      return returnList;
    }

    /// <summary>
    /// Reads sampling data from correct columns for gaze or mouse data.
    /// </summary>
    /// <param name="row">DataTable row with sampling data</param>
    /// <param name="numRow">Number of the X row in the database.</param>
    /// <param name="stimulusSize">The size of the stimulus.</param>
    /// <param name="newPt">Out. Retrieves new sampling point.</param>
    /// <returns>A <see cref="SampleValidity"/> value indidacting the state of the data
    /// validity.</returns>
    private static SampleValidity GetSampleData(
      DataRow row,
      int numRow,
      Size stimulusSize,
      out PointF? newPt)
    {
      if (!row.IsNull(numRow) && !row.IsNull(numRow + 1))
      {
        // Read Values from Table
        float x = Convert.ToSingle(row[numRow]);
        float y = Convert.ToSingle(row[numRow + 1]);
        newPt = new PointF(x, y);
        if (newPt.Value.IsEmpty)
        {
          return SampleValidity.Empty;
        }

        RectangleF bounds = new RectangleF(PointF.Empty, stimulusSize);
        if (bounds.Contains(newPt.Value))
        {
          return SampleValidity.Valid;
        }
        else
        {
          return SampleValidity.OutOfStimulus;
        }
      }
      else
      {
        newPt = null;
        return SampleValidity.Null;
      }
    }

    /// <summary>
    /// This method checks the given <see cref="AOIData"/> 
    /// if it is already used in the database.
    /// </summary>
    /// <param name="shape">Ref. A <see cref="AOIData"/> with
    /// the area of interest to test.</param>
    /// <param name="splashDialog">A <see cref="System.ComponentModel.BackgroundWorker"/> with the 
    /// background splash loading dialog worker which shows progress.</param>
    /// <returns><strong>True</strong>, if an AOI with same name and stimulus assignment
    /// already exists in the database, 
    /// otherwise <strong>false</strong>.</returns>
    private static bool CheckForAOIDuplicate(AOIData shape, System.ComponentModel.BackgroundWorker splashDialog)
    {
      SQLiteOgamaDataSet.AOIsDataTable aoisTable =
        Document.ActiveDocument.DocDataSet.AOIsAdapter.GetDataByTrialIDAndShapeName(shape.TrialID, shape.ShapeName);
      if (aoisTable.Rows.Count > 0)
      {
        if (splashDialog != null && splashDialog.IsBusy)
        {
          splashDialog.CancelAsync();
        }

        string message = "The area of interest " + Environment.NewLine +
          shape.ShapeType.ToString() + ": " + shape.ShapeName +
          "already exists." + Environment.NewLine +
          "Would you like to overwrite it ?";

        DialogResult duplicateQuestion =
          InformationDialog.Show("Overwrite AOI ?", message, true, MessageBoxIcon.Question);

        // Show loading splash screen if it is not running
        if (splashDialog != null && !splashDialog.IsBusy)
        {
          splashDialog.RunWorkerAsync();
        }

        if (duplicateQuestion == DialogResult.No || duplicateQuestion == DialogResult.Cancel)
        {
          return false;
        }

        // User clicked yes so update the existing entry
        // Presuming the DataTable has a column named ShapeName.
        string expression = "ShapeName = '" + shape.ShapeName + "' AND TrialID = '" +
          shape.TrialID + "'";
        DataRow[] foundRows;

        // Use the Select method to find all rows matching the filter.
        foundRows = Document.ActiveDocument.DocDataSet.AOIs.Select(expression);
        if (foundRows.Length > 0)
        {
          foreach (DataRow row in foundRows)
          {
            row["ShapeType"] = shape.ShapeType;
            row["ShapeNumPts"] = shape.ShapeNumPts;
            row["ShapePts"] = shape.ShapePts;
          }
        }

        return false;
      }

      return true;
    }

    #endregion //HELPER
  }
}
