// <copyright file="Statistic.cs" company="FU Berlin">
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

namespace Ogama.Modules.Statistics
{
  using System;
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.Data;
  using System.Drawing;
  using System.Windows.Forms;

  using Ogama.Modules.AOI;
  using Ogama.Modules.Common.Tools;
  using Ogama.Modules.Common.TrialEvents;
  using Ogama.Modules.Common.Types;
  using Ogama.Modules.Statistics.Variables;

  using VectorGraphics.Elements;
  using VectorGraphics.Elements.ElementCollections;
  using VectorGraphics.StopConditions;
  using VectorGraphics.Tools.CustomTypeConverter;

  /// <summary>
  /// A class for calculation of statistical variables.
  /// Should be initialized using the specialized constructor.
  /// </summary>
  public class Statistic
  {
    ///////////////////////////////////////////////////////////////////////////////
    // Defining Constants                                                        //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTANTS

    /// <summary>
    /// Tolerance value for widening areas of interest to get a better hit rate
    /// </summary>
    private static int tolerance;

    #endregion //CONSTANTS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Variables, Enumerations, Events                                  //
    ///////////////////////////////////////////////////////////////////////////////
    #region FIELDS

    /// <summary>
    /// Flags. Stores the variable selection states for subject parameters.
    /// </summary>
    private SubjectParams subjectParams;

    /// <summary>
    /// Flags. Stores the variable selection states for trial parameters.
    /// </summary>
    private TrialParams trialParams;

    /// <summary>
    /// Flags. Stores the variable selection states for gaze parameters.
    /// </summary>
    private GazeParams gazeParams;

    /// <summary>
    /// Stores a list of custom gaze variables.
    /// </summary>
    private List<CustomVariable> gazeCustomParams;

    /// <summary>
    /// Flags. Stores the variable selection states for mouse parameters.
    /// </summary>
    private MouseParams mouseParams;

    /// <summary>
    /// Stores a list of custom mouse variables.
    /// </summary>
    private List<CustomVariable> mouseCustomParams;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the Statistic class.
    /// Initializes variables to calculate by setting the flag members
    /// of this Statistics object.
    /// </summary>
    /// <param name="newSubjectParams">A <see cref="SubjectParams"/> with the flags set to the
    /// subject variables that should be calculated.</param>
    /// <param name="newTrialParams">A <see cref="TrialParams"/> with the flags set to the
    /// trial variables that should be calculated.</param>
    /// <param name="newGazeParams">A <see cref="GazeParams"/> with the flags set to the
    /// gaze variables that should be calculated.</param>
    /// <param name="newGazeCustomParams">A <see cref="List{CustomVariable}"/> with
    /// custom defined gaze variables. Will only be used, when <see cref="GazeParams.Custom"/>
    /// is set.</param>
    /// <param name="newMouseParams">A <see cref="MouseParams"/> with the flags set to the
    /// mouse variables that should be calculated.</param>
    /// <param name="newMouseCustomParams">A <see cref="List{CustomVariable}"/> with
    /// custom defined mouse variables. Will only be used, when <see cref="MouseParams.Custom"/>
    /// is set.</param>
    /// <param name="newTolerance">A <see cref="int"/> tolerance value for 
    /// widening areas of interest to get a better hit rate in pixel.</param>
    public Statistic(
      SubjectParams newSubjectParams,
      TrialParams newTrialParams,
      GazeParams newGazeParams,
      List<CustomVariable> newGazeCustomParams,
      MouseParams newMouseParams,
      List<CustomVariable> newMouseCustomParams,
      int newTolerance)
    {
      this.subjectParams = newSubjectParams;
      this.trialParams = newTrialParams;
      this.gazeParams = newGazeParams;
      this.gazeCustomParams = newGazeCustomParams != null ? newGazeCustomParams : new List<CustomVariable>();
      this.mouseParams = newMouseParams;
      this.mouseCustomParams = newMouseCustomParams != null ? newMouseCustomParams : new List<CustomVariable>();
      tolerance = newTolerance;
    }

    /// <summary>
    /// Prevents a default instance of the Statistic class from being created.
    /// </summary>
    private Statistic()
    {
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES
    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Public methods                                                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region PUBLICMETHODS

    /// <summary>
    /// Calculates the area of an area of interest depending on shape.
    /// </summary>
    /// <param name="aoiRow">AOI Table row which describes this area of interest</param>
    /// <returns>area of AOI</returns>
    public static float GetAOISize(DataRow aoiRow)
    {
      float aoiSize = 0f;
      if (aoiRow != null)
      {
        RectangleF boundingRect = new RectangleF();
        string strPtList = aoiRow["ShapePts"].ToString();
        List<PointF> pointList = ObjectStringConverter.StringToPointFList(strPtList);

        switch (Convert.ToString(aoiRow["ShapeType"]))
        {
          case "None":
            break;
          case "Rectangle":
            boundingRect.Location = pointList[0];
            boundingRect.Width = pointList[2].X - pointList[0].X;
            boundingRect.Height = pointList[2].Y - pointList[0].Y;

            // Calculate Area
            aoiSize = boundingRect.Width * boundingRect.Height;
            break;
          case "Ellipse":
            boundingRect.Location = pointList[0];
            boundingRect.Width = pointList[2].X - pointList[0].X;
            boundingRect.Height = pointList[2].Y - pointList[0].Y;

            // Calculate Area
            aoiSize = boundingRect.Width * boundingRect.Height * (float)Math.PI / 4;
            break;
          case "Polyline":
            // Add First Point at End of List to have closed figure before calculating
            pointList.Add(pointList[0]);

            // Calculate Area
            for (int i = 0; i < pointList.Count - 1; i++)
            {
              aoiSize += (pointList[i].X - pointList[i + 1].X) * (pointList[i].Y + pointList[i + 1].Y) / 2;
            }

            aoiSize = Math.Abs(aoiSize);
            break;
        }
      }

      return aoiSize;
    }

    /// <summary>
    /// Calculates whether the fixation given in datarow hits one of the 
    /// areas of interest given in the AOI Table and returns the 
    /// name of the AOI and its category.
    /// </summary>
    /// <param name="aoiCollection"><see cref="VGElementCollection"/> of areas of interest for current trial</param>
    /// <param name="fixationRow">fixational row</param>
    /// <returns>List of AOI name/group pairs that are hitted from fixation.</returns>
    public static List<string[]> FixationHitsAOI(
      VGElementCollection aoiCollection,
      DataRowView fixationRow)
    {
      List<string[]> hittedAOIs = new List<string[]>();
      string aoiName = "nowhere";
      string aoiGroup = "nowhere";

      foreach (VGElement aoiElement in aoiCollection)
      {
        // Check for intersection between newPath and Clicklist or Fixationlist
        PointF searchPoint = new PointF(
          Convert.ToSingle(fixationRow["PosX"]),
          Convert.ToSingle(fixationRow["PosY"]));

        if (aoiElement.Contains(searchPoint, tolerance))
        {
          aoiGroup = aoiElement.ElementGroup == null ? string.Empty : aoiElement.ElementGroup.Trim();
          aoiName = aoiElement.Name.Trim();
          if (aoiGroup == string.Empty || aoiGroup == " ")
          {
            aoiGroup = "nowhere";
          }

          string[] hittedAOI = { aoiName, aoiGroup };
          hittedAOIs.Add(hittedAOI);
        }
      }

      return hittedAOIs;
    }

    /// <summary>
    /// This method calculates the transition matrix
    /// of the given trial between the AOIs of this trial.
    /// </summary>
    /// <param name="fixations">The fixations of the current trial</param>
    /// <param name="trialsAOIs">A <see cref="VGElementCollection"/> with the AOIs.</param>
    /// <returns>An two dimensional <see cref="Array"/> with 
    /// integers indicating the number of transitions between AOI in row and column.</returns>
    public static Array CreateTransitionMatrixForSingleAOIs(
      DataView fixations,
      VGElementCollection trialsAOIs)
    {
      int aoiCount = trialsAOIs.Count;

      // Create transition matrix
      Array transitionMatrix = Array.CreateInstance(typeof(int), aoiCount + 1, aoiCount + 1);

      // Assign names and indices of AOI.
      Dictionary<string, int> groupIndexAssignment = new Dictionary<string, int>();
      groupIndexAssignment.Add("nowhere", 0);
      for (int i = 0; i < aoiCount; i++)
      {
        string aoiGroupEntry = trialsAOIs[i].Name;
        if (!groupIndexAssignment.ContainsKey(aoiGroupEntry))
        {
          groupIndexAssignment.Add(aoiGroupEntry, i + 1);
        }
      }

      // Iterate selected subjects
      string foregoingHittedAOIName = string.Empty;
      string foregoingSubjectName = string.Empty;
      foreach (DataRowView fixationRow in fixations)
      {
        string subjectName = fixationRow["SubjectName"].ToString();
        if (subjectName != foregoingSubjectName)
        {
          foregoingHittedAOIName = string.Empty;
          foregoingSubjectName = subjectName;
        }

        string hittedAOIName = "nowhere";
        string hittedAOIGroup = "nowhere";
        List<string[]> hittedAOIs = Statistic.FixationHitsAOI(trialsAOIs, fixationRow);
        if (hittedAOIs.Count > 0)
        {
          // Take only first hitted AOI
          hittedAOIName = hittedAOIs[0][0];
          hittedAOIGroup = hittedAOIs[0][1];
        }

        if (foregoingHittedAOIName != string.Empty)
        {
          int indexOfHittedGroup = groupIndexAssignment[hittedAOIName];
          int indexOfForegoingGroup = groupIndexAssignment[foregoingHittedAOIName];
          int oldEntry = (int)transitionMatrix.GetValue(indexOfForegoingGroup, indexOfHittedGroup);
          int newEntry = oldEntry + 1;
          transitionMatrix.SetValue(newEntry, indexOfForegoingGroup, indexOfHittedGroup);
        }

        foregoingHittedAOIName = hittedAOIName;
      }

      return transitionMatrix;
    }

    /// <summary>
    /// This static method calculates the average fixation duration
    /// at the AOIs given in the table.
    /// </summary>
    /// <param name="fixationTable">A <see cref="DataTable"/> with the fixations
    /// to use.</param>
    /// <param name="aois">An <see cref="VGElementCollection"/> with the AOIs to calculate the parameter for.</param>
    /// <returns>An <see cref="int"/> with the average fixation duration at the AOIs in ms.</returns>
    public static AOIStatistic CalcAOIStatistic(DataView fixationTable, VGElementCollection aois)
    {
      AOIStatistic aoiStatistic = new AOIStatistic();

      // NO Target AOI defined
      aoiStatistic.FixationDurationMean = -2;
      aoiStatistic.FixationDurationMedian = -2;
      aoiStatistic.FixationCount = -2;
      aoiStatistic.SumOfTimeOfAllFixations = -2;
      aoiStatistic.FirstHitTimeAfterBeeingOutside = -2;
      aoiStatistic.SaccadeDuration = -2;
      aoiStatistic.SaccadeLength = -2;
      aoiStatistic.SaccadeVelocity = -2;

      if (aois.Count == 0)
      {
        return aoiStatistic;
      }

      DataTable fixationsInAOIs = (DataTable)fixationTable.Table.Clone();
      fixationsInAOIs.Clear();

      int hitCount = 0;

      bool wasOutside = false;
      aoiStatistic.FirstHitTimeAfterBeeingOutside = -1;
      foreach (DataRowView fixRow in fixationTable)
      {
        if (IsFixAtTarget(aois, fixRow) != null)
        {
          hitCount++;
          aoiStatistic.HitTimes.Add(hitCount, (long)fixRow["StartTime"]);
          fixationsInAOIs.Rows.Add(fixRow.Row.ItemArray);

          if (wasOutside && aoiStatistic.FirstHitTimeAfterBeeingOutside == -1)
          {
            aoiStatistic.FirstHitTimeAfterBeeingOutside = (long)fixRow["StartTime"];
          }
        }
        else
        {
          wasOutside = true;
        }
      }

      // Fixation Durations
      double[] fixationsDurations = GetFixationDurationsArray(fixationsInAOIs);
      Descriptive descriptives = new Descriptive(fixationsDurations);

      // -1 : no fixations in AOI
      if (fixationsDurations.Length > 0)
      {
        descriptives.Analyze(); // analyze the data
      }

      aoiStatistic.FixationDurationMean = fixationsDurations.Length == 0 ? -1 : descriptives.Result.Mean;
      aoiStatistic.FixationDurationMedian = fixationsDurations.Length == 0 ? -1 : descriptives.Result.Median;
      aoiStatistic.FixationCount = fixationsDurations.Length == 0 ? -1 : (int)descriptives.Result.Count;
      aoiStatistic.SumOfTimeOfAllFixations = fixationsDurations.Length == 0 ? -1 : descriptives.Result.Sum;

      // Saccade Durations
      double saccadeDuration;
      double saccadeLength;
      double saccadeVelocity;

      GetSaccadeArrays(fixationsInAOIs, out saccadeDuration, out saccadeLength, out saccadeVelocity);

      aoiStatistic.SaccadeDuration = saccadeDuration;
      aoiStatistic.SaccadeLength = saccadeLength;
      aoiStatistic.SaccadeVelocity = saccadeVelocity;

      return aoiStatistic;
    }

    /// <summary>
    /// Method for calculating subject statistics variables.
    /// </summary>
    /// <param name="newRow">current row of statistic data grid view to fill with variables</param>
    /// <param name="subjectRow">Subject table row for current subject.</param>
    public void FillSubjectColumns(DataGridViewRow newRow, DataRowView subjectRow)
    {
      // Write Subject Columns from MainTable
      if (this.subjectParams == (this.subjectParams | SubjectParams.SubjectName))
      {
        newRow.Cells["SUB_NAME"].Value = subjectRow["SubjectName"];
      }

      if (this.subjectParams == (this.subjectParams | SubjectParams.Category))
      {
        newRow.Cells["SUB_CATE"].Value = subjectRow["Category"];
      }

      if (this.subjectParams == (this.subjectParams | SubjectParams.Age))
      {
        newRow.Cells["SUB_AGE"].Value = subjectRow["Age"];
      }

      if (this.subjectParams == (this.subjectParams | SubjectParams.Sex))
      {
        newRow.Cells["SUB_SEX"].Value = subjectRow["Sex"];
      }

      if (this.subjectParams == (this.subjectParams | SubjectParams.Handedness))
      {
        newRow.Cells["SUB_HAND"].Value = subjectRow["Handedness"];
      }

      if (this.subjectParams == (this.subjectParams | SubjectParams.Comments))
      {
        newRow.Cells["SUB_COMM"].Value = subjectRow["Comments"];
      }

      if (this.subjectParams == (this.subjectParams | SubjectParams.Custom))
      {
        DataTable customParams =
          Document.ActiveDocument.DocDataSet.SubjectParametersAdapter.GetDataBySubject(subjectRow["SubjectName"].ToString());

        int counter = 0;

        foreach (DataRow paramRow in customParams.Rows)
        {
          newRow.Cells["SUB_C" + counter.ToString()].Value = paramRow["ParamValue"];
          counter++;
        }
      }
    }

    /// <summary>
    /// Method for calculating trial statistics variables.
    /// Calls the static methods used for calculating the variables.
    /// </summary>
    /// <param name="newRow">current row of statistic data grid view to fill with variables</param>
    /// <param name="trialRow">Trial table row for current subject.</param>
    /// <param name="subjectName">Current calculated subjects name</param>
    public void FillTrialColumns(DataGridViewRow newRow, DataRowView trialRow, string subjectName)
    {
      int trialID = (int)trialRow["TrialID"];

      // Write Trial Columns from SubjectTable
      if (this.trialParams == (this.trialParams | TrialParams.TrialID))
      {
        newRow.Cells["TRI_ID"].Value = trialRow["TrialID"];
      }

      if (this.trialParams == (this.trialParams | TrialParams.Name))
      {
        newRow.Cells["TRI_NAME"].Value = trialRow["TrialName"];
      }

      if (this.trialParams == (this.trialParams | TrialParams.Sequence))
      {
        newRow.Cells["TRI_SEQU"].Value = trialRow["TrialSequence"];
      }

      if (this.trialParams == (this.trialParams | TrialParams.Category))
      {
        newRow.Cells["TRI_CATE"].Value = trialRow["Category"];
      }

      if (this.trialParams == (this.trialParams | TrialParams.Starttime))
      {
        newRow.Cells["TRI_TIME"].Value = trialRow["TrialStartTime"];
      }

      if (this.trialParams == (this.trialParams | TrialParams.Duration))
      {
        newRow.Cells["TRI_DURA"].Value = trialRow["Duration"];
      }

      if (this.trialParams == (this.trialParams | TrialParams.Elimination))
      {
        if (trialRow["EliminateData"].ToString() == string.Empty ||
          trialRow["EliminateData"].ToString() == " ")
        {
          newRow.Cells["TRI_ELIM"].Value = 0;
        }
        else
        {
          newRow.Cells["TRI_ELIM"].Value = 1;
        }
      }

      bool? responseCorrectness;
      StopCondition response = GetResponse(trialRow, out responseCorrectness);

      if (response != null)
      {
        if (this.trialParams == (this.trialParams | TrialParams.Response))
        {
          newRow.Cells["TRI_RESP"].Value = response.ToString();
        }

        if (this.trialParams == (this.trialParams | TrialParams.ResponseCorrectness))
        {
          newRow.Cells["TRI_SOLU"].Value = responseCorrectness.HasValue ? responseCorrectness.Value ? "1" : "0" : "-1";
        }

        if (this.trialParams == (this.trialParams | TrialParams.ResponseAOI))
        {
          newRow.Cells["TRI_RAOI"].Value = GetResponsesAOI(trialRow, response);
        }
      }

      if (this.trialParams == (this.trialParams | TrialParams.Targetsize))
      {
        newRow.Cells["TRI_TASI"].Value = CalcTargetSize(trialID) * 100;
      }

      if (this.trialParams == (this.trialParams | TrialParams.AOISize))
      {
        newRow.Cells["TRI_AOIS"].Value = CalcAOISize(trialID) * 100;
      }
    }

    /// <summary>
    /// Submethod for calculating gaze and mouse polyline related statistics variables
    /// Calls the static methods used for calculating the variables.
    /// </summary>
    /// <param name="newRow">current row of statistic data grid view to fill with variables</param>
    /// <param name="trialRow">Trial table row for current subject.</param>
    /// <param name="subjectName">Current calculated subjects name</param>
    public void FillPolylineRelatedColumns(DataGridViewRow newRow, DataRowView trialRow, string subjectName)
    {
      int trialID = (int)trialRow["TrialID"];
      int trialSequence = (int)trialRow["TrialSequence"];
      float duration = Convert.ToSingle(trialRow["Duration"]);

      float averageDistance = 0;
      int countBlinkLoss = 0; // "No (0,0) values";
      int countOutOfMonitorLoss = 0; // "NoOutOfMonitor samples"
      int countGazeSamples = 0; // No valid gaze samples
      float pathLengthMouse = -1; // no mouse data

      if ((this.mouseParams == (this.mouseParams | MouseParams.Pathlength))
    || (this.mouseParams == (this.mouseParams | MouseParams.PathlengthPS))
    || (this.mouseParams == (this.mouseParams | MouseParams.AverageDistanceToGaze))
    || (this.trialParams == (this.trialParams | TrialParams.Dataloss))
    || (this.trialParams == (this.trialParams | TrialParams.DatalossPC))
    || (this.trialParams == (this.trialParams | TrialParams.OutOfMonitor))
    || (this.trialParams == (this.trialParams | TrialParams.OutOfMonitorPC)))
      {
        CalcRawDataRelatedVariables(
          subjectName,
          trialSequence,
          ref countBlinkLoss,
          ref countOutOfMonitorLoss,
          ref pathLengthMouse,
          ref averageDistance,
          ref countGazeSamples);
      }

      // TrialDataLoss
      if (this.trialParams == (this.trialParams | TrialParams.Dataloss))
      {
        newRow.Cells["TRI_LOSS"].Value = countBlinkLoss;
      }

      if (this.trialParams == (this.trialParams | TrialParams.DatalossPC))
      {
        newRow.Cells["TRI_LOpc"].Value = countGazeSamples > 0 ? countBlinkLoss / (float)countGazeSamples * 100 : 0;
      }

      if (this.trialParams == (this.trialParams | TrialParams.OutOfMonitor))
      {
        newRow.Cells["TRI_LOMO"].Value = countOutOfMonitorLoss;
      }

      if (this.trialParams == (this.trialParams | TrialParams.OutOfMonitorPC))
      {
        newRow.Cells["TRI_LMpc"].Value = countGazeSamples > 0 ? countOutOfMonitorLoss / (float)countGazeSamples * 100 : 0;
      }

      // MousePath
      if (this.mouseParams == (this.mouseParams | MouseParams.Pathlength))
      {
        newRow.Cells["MSE_PATH"].Value = pathLengthMouse;
      }

      if (this.mouseParams == (this.mouseParams | MouseParams.PathlengthPS))
      {
        newRow.Cells["MSE_PApS"].Value = pathLengthMouse != -1 ? pathLengthMouse / duration * 1000 : -1;
      }

      // MouseAndGazeDistance
      if (this.mouseParams == (this.mouseParams | MouseParams.AverageDistanceToGaze))
      {
        newRow.Cells["GMF_AVDI"].Value = averageDistance;
      }
    }

    /// <summary>
    /// Submethod for calculating mouse statistics variables.
    /// Calls the static methods used for calculating the variables.
    /// </summary>
    /// <param name="newRow">current row of statistic data grid view to fill with variables</param>
    /// <param name="trialRow">Trial table row for current subject.</param>
    /// <param name="subjectName">current calculated subjects name</param>
    /// <param name="targetAOIs">A <see cref="VGElementCollection"/> with the group of target AOIs.</param>
    /// <param name="searchRectAOIs">A <see cref="VGElementCollection"/> with the group of search rect AOIs.</param>
    public void FillMouseColumns(
      DataGridViewRow newRow,
      DataRowView trialRow,
      string subjectName,
      VGElementCollection targetAOIs,
      VGElementCollection searchRectAOIs)
    {
      int trialID = (int)trialRow["TrialID"];
      int trialSequence = (int)trialRow["TrialSequence"];
      float duration = Convert.ToSingle(trialRow["Duration"]);

      SortedList<long, InputEvent> mouseEvents = Queries.GetTrialMouseEvents(subjectName, trialSequence);
      List<MouseStopCondition> mousStopConditions = Statistic.ExtractMouseStopConditions(mouseEvents);

      // Mouse Clicks
      int leftClickCount = CalcLeftClickCount(mousStopConditions);
      if (this.mouseParams == (this.mouseParams | MouseParams.LeftClicks))
      {
        newRow.Cells["MCLILCOU"].Value = leftClickCount;
      }

      if (this.mouseParams == (this.mouseParams | MouseParams.LeftClicksPS))
      {
        newRow.Cells["MCLILCpS"].Value = leftClickCount > 0 ? leftClickCount / duration * 1000 : -1;
      }

      int rightClickCount = CalcRightClickCount(mousStopConditions);
      if (this.mouseParams == (this.mouseParams | MouseParams.RightClicks))
      {
        newRow.Cells["MCLIRCOU"].Value = rightClickCount;
      }

      if (this.mouseParams == (this.mouseParams | MouseParams.RightClicksPS))
      {
        newRow.Cells["MCLIRCpS"].Value = rightClickCount > 0 ? rightClickCount / duration * 1000 : -1;
      }

      // Mouse fixations
      DataTable fixationTable =
        Document.ActiveDocument.DocDataSet.MouseFixationsAdapter.GetDataBySubjectAndSequence(subjectName, trialSequence);
      DataView fixationsView = new DataView(fixationTable);

      if (this.mouseParams == (this.mouseParams | MouseParams.FixationsUntilFirstMouseClick))
      {
        newRow.Cells["MFIXCO1C"].Value = CalcNumberOfFixationsUntilFirstMouseClick(mouseEvents, fixationTable);
      }

      if (this.mouseParams == (this.mouseParams | MouseParams.TimeToFirstClick))
      {
        newRow.Cells["MTIM1CLI"].Value = CalcTimeUntilFirstClick(mouseEvents);
      }

      // Fixations general
      if (this.mouseParams == (this.mouseParams | MouseParams.Fixations))
      {
        newRow.Cells["MFIXCOUN"].Value = fixationTable.Rows.Count;
      }

      if (this.mouseParams == (this.mouseParams | MouseParams.FixationsPS))
      {
        newRow.Cells["MFIXCOpS"].Value = fixationTable.Rows.Count > 0 ? fixationTable.Rows.Count / duration * 1000 : -1;
      }

      // Fixation durations
      double[] fixationsDurations = GetFixationDurationsArray(fixationTable);
      Descriptive descriptives = new Descriptive(fixationsDurations);
      if (fixationsDurations.Length > 0)
      {
        descriptives.Analyze(); // analyze the data
      }

      if (this.mouseParams == (this.mouseParams | MouseParams.FixationDurationMean))
      {
        newRow.Cells["MFIXDURA"].Value = fixationsDurations.Length == 0 ? -1 : descriptives.Result.Mean;
      }

      if (this.mouseParams == (this.mouseParams | MouseParams.FixationDurationMedian))
      {
        newRow.Cells["MFIXDUME"].Value = fixationsDurations.Length == 0 ? -1 : descriptives.Result.Median;
      }

      if (this.mouseParams == (this.mouseParams | MouseParams.FixationSaccadeRatio))
      {
        newRow.Cells["MFIXDUpS"].Value = fixationsDurations.Length == 0 ? -1 : descriptives.Result.Sum / duration * 1000;
      }

      if (this.mouseParams == (this.mouseParams | MouseParams.TimeToFirstFixInSearchRect))
      {
        if (searchRectAOIs.Count > 0)
        {
          AOIStatistic aoiStatistic = CalcAOIStatistic(fixationsView, searchRectAOIs);

          // Fixations targets
          newRow.Cells["MFIXRECT"].Value = aoiStatistic.HitTimes.Count > 0 ? aoiStatistic.HitTimes[1] : -1;
        }
        else
        {
          newRow.Cells["MFIXRECT"].Value = -2;
        }
      }

      if (targetAOIs.Count > 0)
      {
        AOIStatistic aoiStatistic = CalcAOIStatistic(fixationsView, targetAOIs);

        if (this.mouseParams == (this.mouseParams | MouseParams.TimeToFirstFixAtTarget))
        {
          newRow.Cells["MFIXTARG"].Value = aoiStatistic.HitTimes.Count > 0 ? aoiStatistic.HitTimes[1] : -1;
        }

        if (this.mouseParams == (this.mouseParams | MouseParams.TimeToSecondFixAtTarget))
        {
          newRow.Cells["MFIX2TAR"].Value = aoiStatistic.HitTimes.Count > 1 ? aoiStatistic.HitTimes[2] : -1;
        }

        if (this.mouseParams == (this.mouseParams | MouseParams.TimeAtTarget))
        {
          newRow.Cells["MFIXTAFT"].Value = aoiStatistic.SumOfTimeOfAllFixations;
        }
      }
      else
      {
        if (this.mouseParams == (this.mouseParams | MouseParams.TimeToFirstFixAtTarget))
        {
          newRow.Cells["MFIXTARG"].Value = -1;
        }

        if (this.mouseParams == (this.mouseParams | MouseParams.TimeToSecondFixAtTarget))
        {
          newRow.Cells["MFIX2TAR"].Value = -1;
        }

        if (this.mouseParams == (this.mouseParams | MouseParams.TimeAtTarget))
        {
          newRow.Cells["MFIXTAFT"].Value = -1;
        }
      }

      // Fixation connections path length
      float pathLengthMouse = CalcPathLengthOfFixationConnections(fixationTable);
      if (this.mouseParams == (this.mouseParams | MouseParams.AverageSaccadeLength))
      {
        newRow.Cells["MFIXSALE"].Value = pathLengthMouse > 0 ? pathLengthMouse / fixationTable.Rows.Count : -1;
      }

      if (this.mouseParams == (this.mouseParams | MouseParams.AverageSaccadeVelocity))
      {
        newRow.Cells["MFIXSAVE"].Value = CalcAverageSaccadeVelocity(fixationTable);
      }

      // Custom fixation variables
      if (this.mouseParams == (this.mouseParams | MouseParams.Custom))
      {
        foreach (CustomVariable var in this.mouseCustomParams)
        {
          if (var.IsAOIGroup)
          {
            // AOI specifies AOI group
            DataTable aoiTable
              = Document.ActiveDocument.DocDataSet.AOIsAdapter.GetDataByTrialIDAndGroup(trialID, var.AOIName);

            if (aoiTable.Rows.Count > 0)
            {
              VGElementCollection aois = new VGElementCollection();
              foreach (DataRow row in aoiTable.Rows)
              {
                string strPtList = row["ShapePts"].ToString();
                string aoiType = row["ShapeType"].ToString();
                string aoiName = row["ShapeName"].ToString();
                string aoiGroup = row["ShapeGroup"].ToString();

                VGElement aoi = Queries.GetVGElementFromDatabase(aoiType, aoiName, aoiGroup, strPtList);
                aois.Add(aoi);
              }

              AOIStatistic aoiStatistic = CalcAOIStatistic(fixationsView, aois);

              switch (var.ParamType)
              {
                case ParamTypes.CompleteTime:
                  newRow.Cells[var.ColumnName].Value = aoiStatistic.SumOfTimeOfAllFixations;
                  break;
                case ParamTypes.TimeUntil:
                  newRow.Cells[var.ColumnName].Value = aoiStatistic.HitTimes.Count > var.Number - 1 ? aoiStatistic.HitTimes[var.Number] : -1;
                  break;
                case ParamTypes.NumberOf:
                  newRow.Cells[var.ColumnName].Value = aoiStatistic.FixationCount;
                  break;
                case ParamTypes.FixationDurationMean:
                  newRow.Cells[var.ColumnName].Value = aoiStatistic.FixationDurationMean;
                  break;
                case ParamTypes.FixationDurationMedian:
                  newRow.Cells[var.ColumnName].Value = aoiStatistic.FixationDurationMedian;
                  break;
                case ParamTypes.Clicks:
                  newRow.Cells[var.ColumnName].Value =
                    CalcNumberOfClicksAtAOIGroup(mousStopConditions, trialID, var.AOIName, var.MouseButton);
                  break;
                case ParamTypes.SaccadeDuration:
                  newRow.Cells[var.ColumnName].Value = aoiStatistic.SaccadeDuration;
                  break;
                case ParamTypes.SaccadeLength:
                  newRow.Cells[var.ColumnName].Value = aoiStatistic.SaccadeLength;
                  break;
                case ParamTypes.SaccadeVelocity:
                  newRow.Cells[var.ColumnName].Value = aoiStatistic.SaccadeVelocity;
                  break;
              }
            }

            aoiTable.Dispose();
          }
          else
          {
            // AOI specifies a single AOI
            DataTable aoiTable
              = Document.ActiveDocument.DocDataSet.AOIsAdapter.GetDataByTrialIDAndShapeName(trialID, var.AOIName);

            if (aoiTable.Rows.Count == 1)
            {
              string strPtList = aoiTable.Rows[0]["ShapePts"].ToString();
              string aoiType = aoiTable.Rows[0]["ShapeType"].ToString();
              string aoiName = aoiTable.Rows[0]["ShapeName"].ToString();
              string aoiGroup = aoiTable.Rows[0]["ShapeGroup"].ToString();

              VGElement aoi = Queries.GetVGElementFromDatabase(aoiType, aoiName, aoiGroup, strPtList);
              VGElementCollection aoiContainer = new VGElementCollection();
              aoiContainer.Add(aoi);
              AOIStatistic aoiStatistic = CalcAOIStatistic(fixationsView, aoiContainer);

              switch (var.ParamType)
              {
                case ParamTypes.CompleteTime:
                  newRow.Cells[var.ColumnName].Value = aoiStatistic.SumOfTimeOfAllFixations;
                  break;
                case ParamTypes.TimeUntil:
                  newRow.Cells[var.ColumnName].Value = aoiStatistic.HitTimes.Count > var.Number - 1 ? aoiStatistic.HitTimes[var.Number] : -1;
                  break;
                case ParamTypes.NumberOf:
                  newRow.Cells[var.ColumnName].Value = aoiStatistic.FixationCount;
                  break;
                case ParamTypes.FixationDurationMean:
                  newRow.Cells[var.ColumnName].Value = aoiStatistic.FixationDurationMean;
                  break;
                case ParamTypes.FixationDurationMedian:
                  newRow.Cells[var.ColumnName].Value = aoiStatistic.FixationDurationMedian;
                  break;
                case ParamTypes.Clicks:
                  newRow.Cells[var.ColumnName].Value =
                    CalcNumberOfClicksAtAOISingle(mousStopConditions, trialID, var.AOIName, var.MouseButton);
                  break;
                case ParamTypes.SaccadeDuration:
                  newRow.Cells[var.ColumnName].Value = aoiStatistic.SaccadeDuration;
                  break;
                case ParamTypes.SaccadeLength:
                  newRow.Cells[var.ColumnName].Value = aoiStatistic.SaccadeLength;
                  break;
                case ParamTypes.SaccadeVelocity:
                  newRow.Cells[var.ColumnName].Value = aoiStatistic.SaccadeVelocity;
                  break;
              }
            }

            aoiTable.Dispose();
          }
        }
      }

      fixationTable.Dispose();
    }

    /// <summary>
    /// Submethod for calculating gaze statistics variables
    /// Calls the static methods used for calculating the variables.
    /// </summary>
    /// <param name="newRow">current row of statistic data grid view to fill with variables</param>
    /// <param name="trialRow">Trial table row for current subject.</param>
    /// <param name="subjectName">current calculated subjects name</param>
    /// <param name="targetAOIs">A <see cref="VGElementCollection"/> with the group of target AOIs.</param>
    /// <param name="searchRectAOIs">A <see cref="VGElementCollection"/> with the group of search rect AOIs.</param>
    public void FillGazeColumns(
      DataGridViewRow newRow,
      DataRowView trialRow,
      string subjectName,
      VGElementCollection targetAOIs,
      VGElementCollection searchRectAOIs)
    {
      // Calculate or read Fixation Columns 
      float duration = Convert.ToSingle(trialRow["Duration"]);
      int trialID = (int)trialRow["TrialID"];
      int trialSequence = (int)trialRow["TrialSequence"];

      DataTable fixationTable =
        Document.ActiveDocument.DocDataSet.GazeFixationsAdapter.GetDataBySubjectAndSequence(subjectName, trialSequence);
      DataView fixationView = new DataView(fixationTable);

      SortedList<long, InputEvent> mouseEvents = Queries.GetTrialMouseEvents(subjectName, trialSequence);

      // Fixation Counts
      if (this.gazeParams == (this.gazeParams | GazeParams.Fixations))
      {
        newRow.Cells["GFIXCOUN"].Value = fixationTable.Rows.Count;
      }

      if (this.gazeParams == (this.gazeParams | GazeParams.FixationsPS))
      {
        newRow.Cells["GFIXCOpS"].Value = fixationTable.Rows.Count / duration * 1000;
      }

      if (this.gazeParams == (this.gazeParams | GazeParams.FixationsUntilFirstMouseClick))
      {
        newRow.Cells["GFIXCO1C"].Value = CalcNumberOfFixationsUntilFirstMouseClick(mouseEvents, fixationTable);
      }

      // Fixation Duration
      double[] fixationsDurations = GetFixationDurationsArray(fixationTable);
      Descriptive descriptives = new Descriptive(fixationsDurations);

      if (fixationsDurations.Length > 0)
      {
        descriptives.Analyze(); // analyze the data
      }

      if (this.gazeParams == (this.gazeParams | GazeParams.FixationDurationMean))
      {
        newRow.Cells["GFIXDURA"].Value = fixationsDurations.Length == 0 ? -1 : descriptives.Result.Mean;
      }

      if (this.gazeParams == (this.gazeParams | GazeParams.FixationDurationMedian))
      {
        newRow.Cells["GFIXDUME"].Value = fixationsDurations.Length == 0 ? -1 : descriptives.Result.Median;
      }

      if (this.gazeParams == (this.gazeParams | GazeParams.FixationSaccadeRatio))
      {
        newRow.Cells["GFIXDUpS"].Value = fixationsDurations.Length == 0 ? -1 : descriptives.Result.Sum / duration * 1000;
      }

      if (this.gazeParams == (this.gazeParams | GazeParams.TimeToFirstFixInSearchRect))
      {
        if (searchRectAOIs.Count > 0)
        {
          AOIStatistic aoiStatistic = CalcAOIStatistic(fixationView, searchRectAOIs);
          newRow.Cells["GFIXRECT"].Value = aoiStatistic.HitTimes.Count > 0 ? aoiStatistic.HitTimes[1] : -1;
        }
        else
        {
          newRow.Cells["GFIXRECT"].Value = -2;
        }
      }

      if (targetAOIs.Count > 0)
      {
        AOIStatistic aoiStatistic = CalcAOIStatistic(fixationView, targetAOIs);

        // Target Fixations
        if (this.gazeParams == (this.gazeParams | GazeParams.TimeAtTarget))
        {
          newRow.Cells["GFIXTAFT"].Value = aoiStatistic.SumOfTimeOfAllFixations;
        }

        if (this.gazeParams == (this.gazeParams | GazeParams.TimeToFirstFixAtTarget))
        {
          newRow.Cells["GFIXTARG"].Value = aoiStatistic.HitTimes.Count > 0 ? aoiStatistic.HitTimes[1] : -1;
        }

        if (this.gazeParams == (this.gazeParams | GazeParams.TimeToSecondFixAtTarget))
        {
          newRow.Cells["GFIX2TAR"].Value = aoiStatistic.HitTimes.Count > 1 ? aoiStatistic.HitTimes[2] : -1;
        }
      }
      else
      {
        if (this.gazeParams == (this.gazeParams | GazeParams.TimeAtTarget))
        {
          newRow.Cells["GFIXTAFT"].Value = -1;
        }

        if (this.gazeParams == (this.gazeParams | GazeParams.TimeToFirstFixAtTarget))
        {
          newRow.Cells["GFIXTARG"].Value = -1;
        }

        if (this.gazeParams == (this.gazeParams | GazeParams.TimeToSecondFixAtTarget))
        {
          newRow.Cells["GFIX2TAR"].Value = -1;
        }
      }

      // Fixation connections path length
      float pathLengthGaze = CalcPathLengthOfFixationConnections(fixationTable);
      if (this.gazeParams == (this.gazeParams | GazeParams.Pathlength))
      {
        newRow.Cells["GFIXPATH"].Value = pathLengthGaze;
      }

      if (this.gazeParams == (this.gazeParams | GazeParams.PathlengthPS))
      {
        newRow.Cells["GFIXPApS"].Value = pathLengthGaze > 0 ? pathLengthGaze / duration * 1000 : -1;
      }

      if (this.gazeParams == (this.gazeParams | GazeParams.AverageSaccadeLength))
      {
        newRow.Cells["GFIXSALE"].Value = pathLengthGaze > 0 ? pathLengthGaze / (fixationTable.Rows.Count - 1) : -1;
      }

      if (this.gazeParams == (this.gazeParams | GazeParams.AverageSaccadeVelocity))
      {
        newRow.Cells["GFIXSAVE"].Value = CalcAverageSaccadeVelocity(fixationTable);
      }

      // Custom fixation variables
      if (this.gazeParams == (this.gazeParams | GazeParams.Custom))
      {
        foreach (CustomVariable var in this.gazeCustomParams)
        {
          if (var.IsAOIGroup)
          {
            // AOI specifies AOI group
            DataTable aoiTable
              = Document.ActiveDocument.DocDataSet.AOIsAdapter.GetDataByTrialIDAndGroup(trialID, var.AOIName);

            VGElementCollection aois = new VGElementCollection();
            foreach (DataRow row in aoiTable.Rows)
            {
              string strPtList = row["ShapePts"].ToString();
              string aoiType = row["ShapeType"].ToString();
              string aoiName = row["ShapeName"].ToString();
              string shapeGroup = row["ShapeGroup"].ToString();

              VGElement aoi = Queries.GetVGElementFromDatabase(aoiType, aoiName, shapeGroup, strPtList);
              aois.Add(aoi);
            }

            AOIStatistic aoiStatistic = CalcAOIStatistic(fixationView, aois);

            switch (var.ParamType)
            {
              case ParamTypes.CompleteTime:
                newRow.Cells[var.ColumnName].Value = aoiStatistic.SumOfTimeOfAllFixations;
                break;
              case ParamTypes.TimeUntil:
                newRow.Cells[var.ColumnName].Value = aoiStatistic.HitTimes.Count > var.Number - 1 ? aoiStatistic.HitTimes[var.Number] : -1;
                break;
              case ParamTypes.NumberOf:
                newRow.Cells[var.ColumnName].Value = aoiStatistic.FixationCount;
                break;
              case ParamTypes.FixationDurationMean:
                newRow.Cells[var.ColumnName].Value = aoiStatistic.FixationDurationMean;
                break;
              case ParamTypes.FixationDurationMedian:
                newRow.Cells[var.ColumnName].Value = aoiStatistic.FixationDurationMedian;
                break;
              case ParamTypes.Regressions:
                newRow.Cells[var.ColumnName].Value = CalcRegressions(fixationView, aois, var.Number);
                break;
              case ParamTypes.SaccadeDuration:
                newRow.Cells[var.ColumnName].Value = aoiStatistic.SaccadeDuration;
                break;
              case ParamTypes.SaccadeLength:
                newRow.Cells[var.ColumnName].Value = aoiStatistic.SaccadeLength;
                break;
              case ParamTypes.SaccadeVelocity:
                newRow.Cells[var.ColumnName].Value = aoiStatistic.SaccadeVelocity;
                break;
            }
          }
          else if (var.AOIName != string.Empty)
          {
            // AOI specifies a single AOI
            DataTable aoiTable
              = Document.ActiveDocument.DocDataSet.AOIsAdapter.GetDataByTrialIDAndShapeName(trialID, var.AOIName);

            if (aoiTable.Rows.Count == 1)
            {
              string strPtList = aoiTable.Rows[0]["ShapePts"].ToString();
              string aoiType = aoiTable.Rows[0]["ShapeType"].ToString();
              string aoiName = aoiTable.Rows[0]["ShapeName"].ToString();
              string shapeGroup = aoiTable.Rows[0]["ShapeGroup"].ToString();

              VGElement aoi = Queries.GetVGElementFromDatabase(aoiType, aoiName, shapeGroup, strPtList);
              VGElementCollection aoiContainer = new VGElementCollection();
              aoiContainer.Add(aoi);
              AOIStatistic aoiStatistic = CalcAOIStatistic(fixationView, aoiContainer);

              switch (var.ParamType)
              {
                case ParamTypes.CompleteTime:
                  newRow.Cells[var.ColumnName].Value = aoiStatistic.SumOfTimeOfAllFixations;
                  break;
                case ParamTypes.TimeUntil:
                  newRow.Cells[var.ColumnName].Value = aoiStatistic.HitTimes.Count > var.Number - 1 ? aoiStatistic.HitTimes[var.Number] : -1;
                  break;
                case ParamTypes.NumberOf:
                  newRow.Cells[var.ColumnName].Value = aoiStatistic.FixationCount;
                  break;
                case ParamTypes.FixationDurationMean:
                  newRow.Cells[var.ColumnName].Value = aoiStatistic.FixationDurationMean;
                  break;
                case ParamTypes.FixationDurationMedian:
                  newRow.Cells[var.ColumnName].Value = aoiStatistic.FixationDurationMedian;
                  break;
                case ParamTypes.Regressions:
                  newRow.Cells[var.ColumnName].Value = CalcRegressions(fixationView, aoiContainer, var.Number);
                  break;
                case ParamTypes.SaccadeDuration:
                  newRow.Cells[var.ColumnName].Value = aoiStatistic.SaccadeDuration;
                  break;
                case ParamTypes.SaccadeLength:
                  newRow.Cells[var.ColumnName].Value = aoiStatistic.SaccadeLength;
                  break;
                case ParamTypes.SaccadeVelocity:
                  newRow.Cells[var.ColumnName].Value = aoiStatistic.SaccadeVelocity;
                  break;
              }
            }
            else
            {
              newRow.Cells[var.ColumnName].Value = -2;
            }
          }
          else if (var.ParamType == ParamTypes.Regressions)
          {
            newRow.Cells[var.ColumnName].Value = CalcRegressions(fixationView, null, var.Number);
          }
          else
          {
            newRow.Cells[var.ColumnName].Value = -2;
          }
        }
      }

      fixationTable.Dispose();
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

    #region TrialRelatedCalculations

    /// <summary>
    /// This static method calculates the size of all AOIs.
    /// </summary>
    /// <param name="trialID">A <see cref="string"/> with the trial id.</param>
    /// <returns>A <see cref="Single"/> with the size of all AOIs.</returns>
    private static float CalcAOISize(int trialID)
    {
      // "NoAOISDefined"
      float aoiSize = -0.01f;

      // Get AOIData
      DataTable aoiTable
        = Document.ActiveDocument.DocDataSet.AOIsAdapter.GetDataByTrialID(trialID);
      float screenArea = Document.ActiveDocument.ExperimentSettings.WidthStimulusScreen
        * Document.ActiveDocument.ExperimentSettings.HeightStimulusScreen;

      if (aoiTable.Rows.Count > 0)
      {
        float area = 0f;
        int counter = 0;
        foreach (DataRow row in aoiTable.Rows)
        {
          if (row["ShapeGroup"].ToString() != "SearchRect")
          {
            area += GetAOISize(row);
            counter++;
          }
        }

        if (counter != 0)
        {
          // Area = Area / counter;
          aoiSize = area / screenArea;
        }
        else
        {
          aoiSize = -0.02f; // "NoAOISExceptSearchRectDefined";
        }
      }

      aoiTable.Dispose();

      return aoiSize;
    }

    /// <summary>
    /// This static method calculates the size of all AOI marked as "Target".
    /// </summary>
    /// <param name="trialID">A <see cref="string"/> with the trial ID.</param>
    /// <returns>A <see cref="Single"/> with the AOI size of all
    /// "Target"s in square pixel.</returns>
    private static float CalcTargetSize(int trialID)
    {
      float targetSize = -0.01f;  // "NoTargetDefined";
      float screenArea = Document.ActiveDocument.ExperimentSettings.WidthStimulusScreen
        * Document.ActiveDocument.ExperimentSettings.HeightStimulusScreen;

      // Get AOIData
      DataTable aoiTable
        = Document.ActiveDocument.DocDataSet.AOIsAdapter.GetTargetsByTrialID(trialID);

      float area = 0;

      // Calculate target AOIs area.
      foreach (DataRow row in aoiTable.Rows)
      {
        area += GetAOISize(row);
      }

      // Only if there are any target aois, replace standard value of -1;
      if (aoiTable.Rows.Count > 0)
      {
        targetSize = area / screenArea;
      }

      aoiTable.Dispose();

      return targetSize;
    }

    /// <summary>
    /// This method gets the response of the subject for the given trial
    /// </summary>
    /// <param name="trialRow">A <see cref="DataRow"/> with the trial data.</param>
    /// <param name="correctness">Out. <strong>True</strong> indicates a correct response,
    /// <strong>false</strong> indicates a wrong response. <strong>Null</strong>,
    /// if trial had no testing conditions.</param>
    /// <returns>A <see cref="StopCondition"/> with the response</returns>
    private static StopCondition GetResponse(DataRowView trialRow, out bool? correctness)
    {
      string subjectName = trialRow["SubjectName"].ToString();
      int trialSequence = (int)trialRow["TrialSequence"];
      StopCondition response = Queries.GetTrialResponse(subjectName, trialSequence);

      if (response == null)
      {
        correctness = null;
        return null;
      }

      if (response.IsCorrectResponse.HasValue)
      {
        if (response.IsCorrectResponse.Value)
        {
          correctness = true;
        }
        else
        {
          correctness = false;
        }
      }
      else
      {
        correctness = null;
      }

      return response;
    }

    /// <summary>
    /// Returns the name and group of the AOI that was under the mouse
    /// cursor when the trial was finished.
    /// Only valid, when the response was a mouse response
    /// </summary>
    /// <param name="trialRow">Trial table row for current subject.</param>
    /// <param name="response">The response <see cref="StopCondition"/>.</param>
    /// <returns>The AOI name and group that was clicked.
    /// <strong>-1</strong> if response was not a mouse response.
    /// <strong>-2</strong> if no AOI were defined
    /// <strong>-3</strong> if no AOI was hit</returns>
    private static string GetResponsesAOI(DataRowView trialRow, StopCondition response)
    {
      string subjectName = trialRow["SubjectName"].ToString();
      int trialID = (int)trialRow["TrialID"];
      int trialSequence = (int)trialRow["TrialSequence"];

      if (!(response is MouseStopCondition))
      {
        return "-1"; // Response was not a mouse response.
      }

      // Get AOIData
      DataTable aoiTable
        = Document.ActiveDocument.DocDataSet.AOIsAdapter.GetDataByTrialID(trialID);

      if (aoiTable.Rows.Count == 0)
      {
        return "-2"; // No AOI defined
      }

      Point clickLocation = ((MouseStopCondition)response).ClickLocation;

      string aoiGroup = string.Empty;
      string aoiName = string.Empty;
      foreach (DataRow aoiRow in aoiTable.Rows)
      {
        string strPtList = Convert.ToString(aoiRow["ShapePts"]);
        string aoiType = Convert.ToString(aoiRow["ShapeType"]);
        string name = aoiRow["ShapeName"].ToString();
        string shapeGroup = aoiRow["ShapeGroup"].ToString();

        VGElement aoiElement = Queries.GetVGElementFromDatabase(aoiType, name, shapeGroup, strPtList);

        if (aoiElement.Contains(clickLocation, tolerance))
        {
          string group = aoiRow["ShapeGroup"].ToString();
          if (group != "SearchRect")
          {
            aoiGroup = group;
            aoiName = name;
            break;
          }
        }
      }

      aoiTable.Dispose();

      // no AOI hitted.
      string aoi = "-3";

      if ((aoiName != string.Empty && aoiName != " ")
        || (aoiGroup != string.Empty && aoiGroup != " "))
      {
        aoi = aoiName + "(Group: " + aoiGroup + ")";
      }

      return aoi;
    }

    #endregion TrialRelatedCalculations

    /// <summary>
    /// This static method calculates the raw data related variables as there 
    /// are distance of gaze and mouse path along with loss and out of
    /// monitor values.
    /// </summary>
    /// <param name="subjectName">A <see cref="string"/> with the subject name.</param>
    /// <param name="trialSequence">An <see cref="int"/> with the trial sequence number.</param>
    /// <param name="countBlinkLoss">Ref. Counts the samples the are lost due to blinks.</param>
    /// <param name="countOutOfMonitorLoss">Ref. Counts the samples the are lost due to out of monitor samples.</param>
    /// <param name="pathLengthMouse">Ref. Returns the path length of the mouse path.</param>
    /// <param name="averageDistance">Ref. Returns the average distance of gaze and mouse path in pixel.</param>
    /// <param name="countSamples">Ref. Counts the number of samples.</param>
    private static void CalcRawDataRelatedVariables(
      string subjectName,
      int trialSequence,
      ref int countBlinkLoss,
      ref int countOutOfMonitorLoss,
      ref float pathLengthMouse,
      ref float averageDistance,
      ref int countSamples)
    {
      // Get RawData
      DataTable rawDataTable =
        Queries.GetRawDataBySubjectAndTrialSequenceWithoutEvents(subjectName, trialSequence);

      // In this section only mouse polyline is created, 
      // because the gaze path length is calculated as the 
      // distance between fixations to avoid
      // including microsaccade movements.
      VGPolyline polylineMouse = new VGPolyline(ShapeDrawAction.Edge, Pens.Red);
      float sumDistance = 0;
      int distancesCount = 0;

      // Loop RawData and drawPolyline
      foreach (DataRow row in rawDataTable.Rows)
      {
        PointF? newGazePoint;
        SampleValidity isGazeValidData = Queries.GetGazeData(
          row,
          Document.ActiveDocument.PresentationSize,
          out newGazePoint);

        switch (isGazeValidData)
        {
          case SampleValidity.Valid:
            break;
          case SampleValidity.Empty:
            countBlinkLoss++;
            break;
          case SampleValidity.Null:
            countBlinkLoss++;
            break;
          case SampleValidity.OutOfStimulus:
            countOutOfMonitorLoss++;
            break;
        }

        PointF? newMousePoint;
        SampleValidity isMouseValidData = Queries.GetMouseData(
          row,
          Document.ActiveDocument.PresentationSize,
          out newMousePoint);

        switch (isMouseValidData)
        {
          case SampleValidity.Valid:
          case SampleValidity.Empty:
          case SampleValidity.OutOfStimulus:
            // mouse is always detected and never out of screen
            polylineMouse.AddPt(newMousePoint.Value);
            break;
          case SampleValidity.Null:
            break;
        }

        // Calculate Distance of Gaze And Mouse Path
        if (isGazeValidData == SampleValidity.Valid &&
          (isMouseValidData == SampleValidity.Valid || isMouseValidData == SampleValidity.Empty))
        {
          sumDistance += VGPolyline.Distance(newGazePoint.Value, newMousePoint.Value);
          distancesCount++;
        }
      }

      if (polylineMouse.GetPointCount() >= 2)
      {
        pathLengthMouse = polylineMouse.GetLength();
      }
      else if (polylineMouse.GetPointCount() == 1)
      {
        pathLengthMouse = 0;        // No Movement in MouseData
      }

      if (distancesCount > 0)
      {
        averageDistance = sumDistance / distancesCount;
      }
      else
      {
        averageDistance = -1;
      }

      countSamples = rawDataTable.Rows.Count;

      rawDataTable.Dispose();
    }

    #region MouseRelatedCalculations

    /// <summary>
    /// This static method calculates the time until the subject first clicked 
    /// with the mouse.
    /// </summary>
    /// <param name="mouseEvents">The <see cref="SortedList{Int64,InputEvent}"/> with the mouse event list.</param>
    /// <returns>An <see cref="Int64"/> with the time in milliseconds 
    /// until first mouseclick.</returns>
    private static long CalcTimeUntilFirstClick(SortedList<long, InputEvent> mouseEvents)
    {
      long timeOfClick = -1;

      if (mouseEvents.Count > 0)
      {
        timeOfClick = mouseEvents.Keys[0];
      }

      return timeOfClick;
    }

    /// <summary>
    /// This static method calculates the number of left mouse clicks
    /// of given trial.
    /// </summary>
    /// <param name="mouseStopConditions">A <see cref="List{MouseStopCondition}"/> with the mouse events.</param>
    /// <returns>An <see cref="int"/> with the number of left mouse clicks.</returns>
    private static int CalcLeftClickCount(List<MouseStopCondition> mouseStopConditions)
    {
      return CalcClickCountForButton(mouseStopConditions, MouseButtons.Left);
    }

    /// <summary>
    /// This static method calculates the number of right mouse clicks
    /// of given trial.
    /// </summary>
    /// <param name="mouseStopConditions">A <see cref="List{MouseStopCondition}"/> with the mouse events.</param>
    /// <returns>An <see cref="int"/> with the number of right mouse clicks.</returns>
    private static int CalcRightClickCount(List<MouseStopCondition> mouseStopConditions)
    {
      return CalcClickCountForButton(mouseStopConditions, MouseButtons.Right);
    }

    /// <summary>
    /// This static method calculates the number of mouse clicks of given trial
    /// for the given <see cref="MouseButtons"/>
    /// </summary>
    /// <param name="mouseStopConditions">A <see cref="List{MouseStopCondition}"/> with the mouse events.</param>
    /// <param name="button">The <see cref="MouseButtons"/> to check for.</param>
    /// <returns>An <see cref="int"/> with the number of mouse clicks of the specific button.</returns>
    private static int CalcClickCountForButton(List<MouseStopCondition> mouseStopConditions, MouseButtons button)
    {
      int clickCounter = 0;
      foreach (MouseStopCondition msc in mouseStopConditions)
      {
        if (msc.StopMouseButton == button)
        {
          clickCounter++;
        }
      }

      return clickCounter;
    }

    /// <summary>
    /// This static method extracts the mouse stop conditions out of the given list of 
    /// <see cref="InputEvent"/>s.
    /// </summary>
    /// <param name="mouseEvents">A <see cref="SortedList{Int64, InputEvent}"/> of input events.</param>
    /// <returns>A <see cref="List{MouseStopCondition}"/> with the mouse events as StopConditions.</returns>
    private static List<MouseStopCondition> ExtractMouseStopConditions(SortedList<long, InputEvent> mouseEvents)
    {
      List<MouseStopCondition> mouseStopConditions = new List<MouseStopCondition>();
      foreach (InputEvent mouseEvent in mouseEvents.Values)
      {
        if (mouseEvent.Task == InputEventTask.Down || mouseEvent.Type == EventType.Response)
        {
          StopCondition condition;
          try
          {
            condition = (StopCondition)TypeDescriptor.GetConverter(typeof(StopCondition)).ConvertFrom(mouseEvent.Param);
          }
          catch (ArgumentException)
          {
            continue;
          }

          if (condition is MouseStopCondition)
          {
            mouseStopConditions.Add((MouseStopCondition)condition);
          }
        }
      }

      return mouseStopConditions;
    }

    /// <summary>
    /// This static method calculates the number of clicks at the given AOI group.
    /// </summary>
    /// <param name="mouseStopConditions">A <see cref="List{MouseStopCondition}"/> with the clicks.</param>
    /// <param name="trialID">An <see cref="Int32"/> with the trialID</param>
    /// <param name="aoiGroup">A <see cref="String"/> with the name of the aoi group.</param>
    /// <param name="button">A <see cref="MouseButtons"/> with the button to count for.</param>
    /// <returns>An <see cref="Int32"/> with the number of clicks of the given 
    /// mouse button in the trial with the given ID
    /// at the given AOI group.</returns>
    private static int CalcNumberOfClicksAtAOIGroup(
      List<MouseStopCondition> mouseStopConditions,
      int trialID,
      string aoiGroup,
      MouseButtons button)
    {
      // Get AOIData
      DataTable aoiTable
        = Document.ActiveDocument.DocDataSet.AOIsAdapter.GetDataByTrialIDAndGroup(trialID, aoiGroup);

      int value = CalcNumberOfClicksAtAOIs(mouseStopConditions, aoiTable, button);

      aoiTable.Dispose();
      return value;
    }

    /// <summary>
    /// This static method calculates the number of clicks at the given AOI.
    /// </summary>
    /// <param name="mouseStopConditions">A <see cref="List{MouseStopCondition}"/> with the clicks.</param>
    /// <param name="trialID">An <see cref="Int32"/> with the trialID</param>
    /// <param name="aoiName">A <see cref="String"/> with the name of the aoi.</param>
    /// <param name="button">A <see cref="MouseButtons"/> with the button to count for.</param>
    /// <returns>An <see cref="Int32"/> with the number of clicks of the given 
    /// mouse button in the trial with the given ID
    /// at the given AOI.</returns>
    private static int CalcNumberOfClicksAtAOISingle(
      List<MouseStopCondition> mouseStopConditions,
      int trialID,
      string aoiName,
      MouseButtons button)
    {
      // Get AOIData
      DataTable aoiTable
        = Document.ActiveDocument.DocDataSet.AOIsAdapter.GetDataByTrialIDAndShapeName(trialID, aoiName);

      int value = CalcNumberOfClicksAtAOIs(mouseStopConditions, aoiTable, button);

      aoiTable.Dispose();
      return value;
    }

    /// <summary>
    /// This static method calculates the number of clicks at the given AOI or AOI group.
    /// </summary>
    /// <param name="mouseStopConditions">A <see cref="List{MouseStopCondition}"/> with the clicks.</param>
    /// <param name="aoiTable">A <see cref="DataTable"/> with the aoi or aois.</param>
    /// <param name="button">A <see cref="MouseButtons"/> with the button to count for.</param>
    /// <returns>An <see cref="Int32"/> with the number of clicks of the given 
    /// mouse button at the given AOI(s).</returns>
    private static int CalcNumberOfClicksAtAOIs(
      List<MouseStopCondition> mouseStopConditions,
      DataTable aoiTable,
      MouseButtons button)
    {
      VGElementCollection aois = new VGElementCollection();
      foreach (DataRow row in aoiTable.Rows)
      {
        string strPtList = row["ShapePts"].ToString();
        string aoiType = row["ShapeType"].ToString();
        string aoiName = row["ShapeName"].ToString();
        string shapeGroup = row["ShapeGroup"].ToString();

        VGElement aoi = Queries.GetVGElementFromDatabase(aoiType, aoiName, shapeGroup, strPtList);
        aois.Add(aoi);
      }

      // Never over AOI
      int numberOfClicksAtAOI = -1;

      List<Point> clickLocations = ParseConditionsForLocations(mouseStopConditions, button);

      if (aois.Count == 0)
      {
        // no AOIs definded
        return -2;
      }

      if (clickLocations.Count == 0)
      {
        // no clicks of given type at all
        return -3;
      }

      int count = 0;
      foreach (Point clickLocation in clickLocations)
      {
        int hitIndex = IsClickAtTarget(aois, clickLocation);
        if (hitIndex >= 0)
        {
          count++;
        }
      }

      numberOfClicksAtAOI = count == 0 ? -1 : count;

      // -1: Never over AOI
      // -2; no AOIs definded
      // -3; no clicks of given type at all
      return numberOfClicksAtAOI;
    }

    /// <summary>
    /// This static method extracts the mouse click locations 
    /// out of the given list of <see cref="MouseStopCondition"/>s.
    /// </summary>
    /// <param name="mouseEvents">A <see cref="List{MouseStopCondition}"/> with the mouse events as StopConditions.</param>
    /// <param name="button">The <see cref="MouseButtons"/> to use.</param>
    /// <returns>A <see cref="List{Point}"/> with the click locations.</returns>
    private static List<Point> ParseConditionsForLocations(
      List<MouseStopCondition> mouseEvents,
      MouseButtons button)
    {
      List<Point> returnList = new List<Point>();
      foreach (MouseStopCondition msc in mouseEvents)
      {
        if (msc.StopMouseButton == button)
        {
          returnList.Add(msc.ClickLocation);
        }
      }

      return returnList;
    }

    #endregion //MouseRelatedCalculations

    #region FixationRelatedCalculations

    /// <summary>
    /// This static method calculates the number of regressions in the given AOI 
    /// using the given table of fixations and line height.
    /// </summary>
    /// <param name="fixationView">A <see cref="DataView"/> with the list of fixations</param>
    /// <param name="aois">A <see cref="VGElementCollection"/> with the AOIs.</param>
    /// <param name="lineHeight">The line height of the textual area.</param>
    /// <returns>An <see cref="Int32"/> with the number of regressions.</returns>
    private static int CalcRegressions(
      DataView fixationView,
      VGElementCollection aois,
      int lineHeight)
    {
      int regressionCount = 0;

      // Check if we are using AOIs and we did not found any.
      if (aois != null && aois.Count == 0)
      {
        // -2 = this AOIs was not definded in this trial
        regressionCount = -2;
        return regressionCount;
      }

      PointF lastLocation = PointF.Empty;
      VGElement lastHittedAOI = null;
      foreach (DataRowView fixRow in fixationView)
      {
        // Calculate overall regressions
        if (aois != null)
        {
          // Calculate regressions in single AOI
          if (aois.Count == 1)
          {
            if (IsFixAtTarget(aois, fixRow) == null)
            {
              // Reset last location if fixation was outside AOI
              // so first refixation in AOI will restart the count
              lastLocation = PointF.Empty;
              continue;
            }
          }
          else
          {
            // Calculate regressions in AOI groups
            VGElement hittedAOI = IsFixAtTarget(aois, fixRow);
            if (hittedAOI == null)
            {
              // Reset last location if fixation was outside AOI
              // so first refixation in AOI will restart the count
              lastLocation = PointF.Empty;
              lastHittedAOI = null;
              continue;
            }
            else if (hittedAOI != lastHittedAOI)
            {
              // Reset last location if fixation was outside last AOI
              // but inside other AOI of same group
              // so first refixation in AOI will restart the count
              lastLocation = PointF.Empty;
              lastHittedAOI = hittedAOI;
              continue;
            }
          }
        }

        // Get Fixation location
        PointF currentLocation = new PointF(
          Convert.ToSingle(fixRow["PosX"]),
          Convert.ToSingle(fixRow["PosY"]));

        if (currentLocation.X > lastLocation.X)
        {
          // Default forward, no regression
          lastLocation = currentLocation;
          continue;
        }

        if (currentLocation.Y > lastLocation.Y + lineHeight)
        {
          // Default line change, no regression
          lastLocation = currentLocation;
          continue;
        }

        if (currentLocation.Y < lastLocation.Y - 2 * lineHeight)
        {
          // Big Line skipping upwards, no regression
          lastLocation = currentLocation;
          continue;
        }

        // Either go back in the line or go back vertical,
        // both is a regression
        lastLocation = currentLocation;
        regressionCount++;
      }

      return regressionCount;
    }

    #endregion //FixationRelatedCalculations

    #region PathRelatedCalculations

    /// <summary>
    /// This static method calculates the average saccade velocity by
    /// the mean of all fixation connection velocitys.
    /// </summary>
    /// <param name="fixationTable">A <see cref="DataTable"/> with the fixations
    /// to use.</param>
    /// <returns>A <see cref="Single"/> with the average saccade velocity in pixel per second.</returns>
    private static float CalcAverageSaccadeVelocity(DataTable fixationTable)
    {
      if (fixationTable.Rows.Count <= 1)
      {
        return -1;
      }

      List<float> saccadeVelocitys = new List<float>();
      PointF previousFixationCenter = new PointF(
        Convert.ToSingle(fixationTable.Rows[0]["PosX"]),
        Convert.ToSingle(fixationTable.Rows[0]["PosY"]));
      long previousFixationEndTime =
        (long)fixationTable.Rows[0]["StartTime"] + (int)fixationTable.Rows[0]["Length"];

      foreach (DataRow row in fixationTable.Rows)
      {
        long startTime = (long)row["StartTime"];
        int duration = (int)row["Length"];
        float posX = !row.IsNull("PosX") ? Convert.ToSingle(row["PosX"]) : 0;
        float posY = !row.IsNull("PosY") ? Convert.ToSingle(row["PosY"]) : 0;
        if (posX != 0 || posY != 0)
        {
          PointF nextFixationCenter = new PointF(posX, posY);
          float distance = VGPolyline.Distance(previousFixationCenter, nextFixationCenter);
          int time = (int)(startTime - previousFixationEndTime);
          saccadeVelocitys.Add(distance / time);
          previousFixationCenter = nextFixationCenter;
          previousFixationEndTime = startTime + duration;
        }
      }

      float averageSaccadeVelocity = 0f;
      foreach (float velocityValue in saccadeVelocitys)
      {
        averageSaccadeVelocity += velocityValue;
      }

      averageSaccadeVelocity = averageSaccadeVelocity / (saccadeVelocitys.Count - 1);

      return averageSaccadeVelocity;
    }

    /// <summary>
    /// This static method calculates the sum of fixation connections,
    /// which is a gaze path length.
    /// </summary>
    /// <param name="fixationTable">A <see cref="DataTable"/> with the fixations
    /// to use.</param>
    /// <returns>A <see cref="Single"/> with the path length in pixel.</returns>
    private static float CalcPathLengthOfFixationConnections(DataTable fixationTable)
    {
      // Fixation PathLength
      // no fixation available
      float pathLengthGaze = -1f;

      VGPolyline polylineGaze = new VGPolyline(ShapeDrawAction.Edge, Pens.Green);
      foreach (DataRow row in fixationTable.Rows)
      {
        float gazeX = !row.IsNull("PosX") ? Convert.ToSingle(row["PosX"]) : 0;
        float gazeY = !row.IsNull("PosY") ? Convert.ToSingle(row["PosY"]) : 0;
        if (gazeX != 0 || gazeY != 0)
        {
          polylineGaze.AddPt(new PointF(gazeX, gazeY));
        }
      }

      if (polylineGaze.GetPointCount() >= 2)
      {
        // Connections available
        pathLengthGaze = polylineGaze.GetLength();
      }
      else if (polylineGaze.GetPointCount() == 1)
      {
        // Only one fixation, so no connections
        pathLengthGaze = 0;
      }

      return pathLengthGaze;
    }

    #endregion //PathRelatedCalculations

    #endregion //METHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER

    /// <summary>
    /// This static method calculates the number of fixations until
    /// subject first pressed the mouse.
    /// </summary>
    /// <param name="mouseEvents">The <see cref="SortedList{Int64,InputEvent}"/> with the mouse event list.</param>
    /// <param name="fixationTable">A <see cref="DataTable"/> with the fixations
    /// to use.</param>
    /// <returns>An <see cref="int"/> with the number of fixations.</returns>
    private static int CalcNumberOfFixationsUntilFirstMouseClick(
      SortedList<long, InputEvent> mouseEvents,
      DataTable fixationTable)
    {
      // No mouse clicks at all
      int fixCountUntilFirstMouseClick = -1;

      if (mouseEvents.Count > 0)
      {
        fixCountUntilFirstMouseClick = 0;
        long timeOfFirstClick = mouseEvents.Keys[0];
        foreach (DataRow fixRow in fixationTable.Rows)
        {
          if (Convert.ToInt64(fixRow["StartTime"]) < timeOfFirstClick)
          {
            fixCountUntilFirstMouseClick++;
          }
          else
          {
            break;
          }
        }
      }

      return fixCountUntilFirstMouseClick;
    }

    /// <summary>
    /// Calculates whether the fixation given in datarow hits one of the 
    /// areas of interest given in the AOI Table
    /// </summary>
    /// <param name="aois">A <see cref="VGElementCollection"/> with the areas of interest for current trial.</param>
    /// <param name="fixationRow">fixational row</param>
    /// <returns>Row number of aoi that is hitted from fixation.</returns>
    private static VGElement IsFixAtTarget(VGElementCollection aois, DataRowView fixationRow)
    {
      foreach (VGElement aoi in aois)
      {
        // Check for intersection
        PointF searchPoint = new PointF(
          Convert.ToSingle(fixationRow["PosX"]),
          Convert.ToSingle(fixationRow["PosY"]));

        if (aoi.Contains(searchPoint, tolerance))
        {
          return aoi;
        }
      }

      return null;
    }

    /// <summary>
    /// Calculates whether the fixation given in datarow hits one of the 
    /// areas of interest given in the AOI collection.
    /// </summary>
    /// <param name="aoi">A <see cref="VGElement"/> with the area of interest to check for.</param>
    /// <param name="fixationRow">fixational row</param>
    /// <returns>True if aoi is hitted from fixation.</returns>
    private static bool IsFixAtTarget(VGElement aoi, DataRowView fixationRow)
    {
      // Check for intersection
      PointF searchPoint = new PointF(
        Convert.ToSingle(fixationRow["PosX"]),
        Convert.ToSingle(fixationRow["PosY"]));

      if (aoi.Contains(searchPoint, tolerance))
      {
        return true;
      }

      return false;
    }

    /// <summary>
    /// Calculates whether the mouse click position given in datarow hits one of the 
    /// areas of interest given in the AOI Table
    /// </summary>
    /// <param name="aois">A <see cref="VGElementCollection"/> with the areas of interest for current trial.</param>
    /// <param name="clickLocation">A <see cref="Point"/> with the location of the click in screen coordinates.</param>
    /// <returns>Row number of AOI that is hitted from the mouse click.</returns>
    private static int IsClickAtTarget(VGElementCollection aois, Point clickLocation)
    {
      // Never over AOI
      int hitIndex = -1;

      if (aois.Count == 0)
      {
        // NO AOIs definded
        hitIndex = -2;
        return hitIndex;
      }

      int rowCounter = 0;
      foreach (VGElement target in aois)
      {
        // Check for intersection between newPath and Clicklist or Fixationlist
        if (target.Contains(clickLocation, tolerance))
        {
          hitIndex = rowCounter;
          break;
        }

        rowCounter++;
      }

      return hitIndex;
    }

    /// <summary>
    /// Calculates the time to the first fixation in given area of interest.
    /// AOI table has normally only one row.
    /// </summary>
    /// <param name="aois">A <see cref="VGElementCollection"/> with the areas of interest for current trial.</param>
    /// <param name="inputTable">Table with fixation list.</param>
    /// <param name="numberOfFixation">Set to number of fixation that should be
    /// detected (first, second, third)</param>
    /// <returns>Number of fixation that is the "numberOfFixation" (first, second, third) in given AOI</returns>
    private static int GetNumberOfFixationInGivenAOIList(
      VGElementCollection aois,
      DataTable inputTable,
      int numberOfFixation)
    {
      // Never over AOI
      int hitIndex = -1;

      if (aois.Count == 0)
      {
        // NO AOIs definded
        hitIndex = -2;
        return hitIndex;
      }

      int rowCounter = 0;
      int hitCount = 0;

      // Check for intersection between pathIncludingAllAOIs and Clicklist or Fixationlist
      foreach (DataRow inputRow in inputTable.Rows)
      {
        PointF searchPoint = new PointF(
          Convert.ToSingle(inputRow["PosX"]),
          Convert.ToSingle(inputRow["PosY"]));

        foreach (VGElement aoi in aois)
        {
          if (aoi.Contains(searchPoint, tolerance))
          {
            // That means given point lies in current AOI.
            hitCount++;
            if (hitCount == numberOfFixation)
            {
              hitIndex = rowCounter;
              break;
            }
          }
        }

        rowCounter++;
      }

      return hitIndex;
    }

    /// <summary>
    /// This methods returns the fixation durations from the given table of fixations
    /// from the database as a double array.
    /// </summary>
    /// <param name="fixationTable">A <see cref="DataTable"/> with the fixations.</param>
    /// <returns>An unsorted array of doubles with the fixation durations.</returns>
    private static double[] GetFixationDurationsArray(DataTable fixationTable)
    {
      List<double> fixationDurations = new List<double>();
      foreach (DataRow fixationRow in fixationTable.Rows)
      {
        fixationDurations.Add(Convert.ToDouble(fixationRow["Length"]));
      }

      return fixationDurations.ToArray();
    }

    /// <summary>
    /// This static methods calculates the mean saccade duration, length and velocity from the given table of fixations
    /// from the database
    /// </summary>
    /// <param name="fixationsInAOIs">A <see cref="DataTable"/> with the fixations.</param>
    /// <param name="saccadeDuration">The mean saccade duration</param>
    /// <param name="saccadeLength">The mean saccade length</param>
    /// <param name="saccadeVelocity">The mean saccade velocity</param>
    private static void GetSaccadeArrays(
      DataTable fixationsInAOIs,
      out double saccadeDuration,
      out double saccadeLength,
      out double saccadeVelocity)
    {
      List<double> saccadeDurations = new List<double>();
      List<double> saccadeLengths = new List<double>();
      List<double> saccadeVelocitys = new List<double>();

      PointF lastFixationCenter = PointF.Empty;
      double lastFixationEndTime = 0;
      int lastCountInTrial = 0;

      for (int i = 0; i < fixationsInAOIs.Rows.Count; i++)
      {
        DataRow row = fixationsInAOIs.Rows[i];
        double posX = Convert.ToDouble(row["PosX"]);
        double posY = Convert.ToDouble(row["PosY"]);
        PointF fixationCenter = new PointF((float)posX, (float)posY);
        double fixationDuration = Convert.ToDouble(row["Length"]);
        double fixationStartTime = Convert.ToDouble(row["StartTime"]);
        int countInTrial = Convert.ToInt32(row["CountInTrial"]);

        if (i > 0 && (countInTrial == lastCountInTrial + 1))
        {
          double currentSaccadeDuration = fixationStartTime - lastFixationEndTime;
          double currentSaccadeLength = VGPolyline.Distance(fixationCenter, lastFixationCenter);
          saccadeDurations.Add(currentSaccadeDuration);
          saccadeLengths.Add(currentSaccadeLength);
          saccadeVelocitys.Add(currentSaccadeLength / currentSaccadeDuration);
        }

        lastFixationEndTime = fixationStartTime + fixationDuration;
        lastFixationCenter = fixationCenter;
        lastCountInTrial = countInTrial;
      }

      // -1 : no saccades in AOI
      // Calculate saccade duration mean
      Descriptive descriptives = new Descriptive(saccadeDurations.ToArray());
      if (saccadeDurations.Count > 0)
      {
        descriptives.Analyze(); // analyze the data
      }

      saccadeDuration = saccadeDurations.Count == 0 ? -1 : descriptives.Result.Mean;

      // Calculate saccade length mean
      descriptives = new Descriptive(saccadeLengths.ToArray());

      if (saccadeLengths.Count > 0)
      {
        descriptives.Analyze(); // analyze the data
      }

      saccadeLength = saccadeLengths.Count == 0 ? -1 : descriptives.Result.Mean;

      // Calculate saccade velocitys mean
      descriptives = new Descriptive(saccadeVelocitys.ToArray());

      if (saccadeVelocitys.Count > 0)
      {
        descriptives.Analyze(); // analyze the data
      }

      // -1 : no fixations in AOI
      saccadeVelocity = saccadeVelocitys.Count == 0 ? -1 : descriptives.Result.Mean;
    }

    #endregion //HELPER
  }
}
