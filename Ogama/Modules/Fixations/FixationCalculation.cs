// <copyright file="FixationCalculation.cs" company="FU Berlin">
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

namespace Ogama.Modules.Fixations
{
  using System;
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.Data;
  using System.Data.SqlClient;
  using System.Drawing;
  using Ogama.DataSet;
  using Ogama.ExceptionHandling;
  using Ogama.Modules.Common;
  using Ogama.Modules.Common.Tools;
  using Ogama.Modules.Common.Types;

  using VectorGraphics.Elements;

  /// <summary>
  /// This class is designed to calculate fixational data out of
  /// raw data tables. It uses an internal <see cref="BackgroundWorker"/>.
  /// The method to call after instantiation is <see cref="CalculateSubjectsFixations(string)"/>.
  /// </summary>
  /// <remarks>The calculation uses the fixation detection algorithms available from
  /// Company:   LC Technologies, Inc.
  ///            1483 Chain Bridge Road, Suite 104
  ///            McLean, VA 22101
  ///            (703) 385-7133
  /// that are ported to C# in the class <see cref="FixationDetection"/>.
  /// </remarks>
  public class FixationCalculation
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
    /// The <see cref="BackgroundWorker"/> that performs the calculation.
    /// </summary>
    private BackgroundWorker bgwCalcFixationsForOneSubject;

    /// <summary>
    /// Maximum distance of two samples to constitute a gaze fixation. 
    /// Measured in pixels of eye monitor.
    /// </summary>
    private int gazeMaxDistance = 20;

    /// <summary>
    /// Minimum number of samples that constitute a gaze fixation.
    /// </summary>
    private int gazeMinSamples = 20;

    /// <summary>
    /// Maximum distance of two samples to constitute a mouse fixation. 
    /// Measured in pixels of eye monitor
    /// </summary>
    private int mouseMaxDistance = 20;

    /// <summary>
    /// Minimum number of samples that constitute a mouse fixation.
    /// </summary>
    private int mouseMinSamples = 10;

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

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the FixationCalculation class.
    /// </summary>
    public FixationCalculation()
    {
      this.bgwCalcFixationsForOneSubject = new BackgroundWorker();
      this.bgwCalcFixationsForOneSubject.WorkerReportsProgress = true;
      this.bgwCalcFixationsForOneSubject.WorkerSupportsCancellation = true;
      this.bgwCalcFixationsForOneSubject.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwCalcFixationsForOneSubject_DoWork);
      this.ReadFixationCalculationSettings();
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Enumerations                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region ENUMS

    /// <summary>
    /// Declare a delegate type for getting a data point from table.
    /// </summary>
    /// <param name="row">A <see cref="DataRow"/> with the raw data tables row from which to get the sampling data.</param>
    /// <param name="stimulusSize">The <see cref="Size"/> of the shown stimulus.</param>
    /// <param name="newPt">OUT An <see cref="PointF"/> with the sampling point or null if one of the values is null.</param>
    /// <returns>A <see cref="SampleValidity"/> value for the row.</returns>
    private delegate SampleValidity GetDataDelegate(
      System.Data.DataRow row, 
      Size stimulusSize, 
      out PointF? newPt);

    #endregion ENUMS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets the <see cref="BackgroundWorker"/> that performs the operation.
    /// Can be grabbed for events.
    /// </summary>
    public BackgroundWorker Worker
    {
      get { return this.bgwCalcFixationsForOneSubject; }
    }

    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Public methods                                                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region PUBLICMETHODS

    /// <summary>
    /// This method calculates the fixations out of the raw data
    /// for the given subject.
    /// </summary>
    /// <param name="subject">A <see cref="string"/> with the subject name.</param>
    public void CalculateSubjectsFixations(string subject)
    {
      // Holds trialsTable corresponding to selected subject
      DataTable trialsTable = Document.ActiveDocument.DocDataSet.TrialsAdapter.GetDataBySubject(subject);

      this.bgwCalcFixationsForOneSubject.RunWorkerAsync(new object[] { subject, trialsTable });
    }

    /// <summary>
    /// Calculates fixations from raw data and writes them into 
    /// the database in the fixations tables.
    /// </summary>
    /// <param name="sampleType">The <see cref="SampleType"/> of the data, gaze or mouse</param>
    /// <param name="subject">A <see cref="String"/> with the subject name</param>
    /// <param name="trialsTable">The database trials <see cref="DataTable"/></param>
    /// <param name="worker">The <see cref="BackgroundWorker"/> of the calculation</param>
    /// <param name="e">The <see cref="DoWorkEventArgs"/> of the <see cref="BackgroundWorker"/></param>
    public void CalcFixations(
      SampleType sampleType,
      string subject,
      System.Data.DataTable trialsTable,
      BackgroundWorker worker,
      DoWorkEventArgs e)
    {
      // Fixation  Calculating Objects //////////////////////////////////////////
      FixationDetection objFixationDetection = new FixationDetection();

      // Instantiate the delegate using the method as a parameter
      GetDataDelegate getDataMethod = null;
      int minSamples = 0;
      int maxDistance = 0;

      if (sampleType == (sampleType | SampleType.Gaze))
      {
        getDataMethod = Queries.GetGazeData;
        minSamples = this.gazeMinSamples;
        maxDistance = this.gazeMaxDistance;
      }
      else if (sampleType == (sampleType | SampleType.Mouse))
      {
        getDataMethod = Queries.GetMouseData;
        minSamples = this.mouseMinSamples;
        maxDistance = this.mouseMaxDistance;
      }

      bool point_found_delayed;
      /* sample gazepoint-found flag,      */
      /*   min_fix_samples ago             */
      float x_delayed;      /* sample gazepoint coordinates,     */
      float y_delayed;      /*   min_fix_samples ago             */
      float deviation_delayed;
      /* deviation of the gaze from the    */
      /*   present fixation,               */
      /*   min_fix_samples ago             */
      /* Fixation data - delayed:          */
      float x_fix_delayed = new float();       /* fixation point as estimated       */
      float y_fix_delayed = new float();       /*   min_fix_samples ago             */
      int saccade_duration_delayed;
      /* duration of the saccade           */
      /*   preceeding the preset fixation  */
      /*   (samples)                       */
      long fix_start_time = new long();
      int fix_duration_delayed_samples = new int(); /* duration of the present fixation  */
      long fix_duration_delayed_milliseconds = new long(); /* duration of the present fixation  */

      EyeMotionState currentState = new EyeMotionState();

      // OtherVars
      int counterTrial = 0;
      PointF trialLastFixCenter = new PointF(0, 0);

      // Loop Rows=Trials
      foreach (DataRow trialRow in trialsTable.Rows)
      {
        List<Fixation> fixations = new List<Fixation>();

        // Reinitialize Fixation detection object
        // to ensure no overlay between fixations of 
        // trials that follow each other.
        objFixationDetection.InitFixation(minSamples);

        int trialID = (int)trialRow["TrialID"];
        int trialSequence = (int)trialRow["TrialSequence"];

        // Holds RawData corresponding to selected trial and subject
        DataTable rawDataTable = Queries.GetRawDataBySubjectAndTrialSequence(subject, trialSequence);
        long trialStartTime = rawDataTable.Rows.Count > 0 ? (long)rawDataTable.Rows[0]["Time"] : 0;

        int counterRows = 0;
        int counterFix = 0;
        bool isFixation = false;

        // Loop RawData
        foreach (DataRow rowRaw in rawDataTable.Rows)
        {
          PointF? newPt;
          SampleValidity isValidData = getDataMethod(
            rowRaw, 
            Document.ActiveDocument.PresentationSize,
            out newPt);

          bool useSample = false;
          switch (isValidData)
          {
            case SampleValidity.Valid:
              useSample = true;
              break;
            case SampleValidity.Empty:
              useSample = true;
              break;
            case SampleValidity.Null:
              break;
            case SampleValidity.OutOfStimulus:
              useSample = true;
              break;
          }

          if (useSample)
          {
            PointF dataPoint = newPt.Value;

            currentState = objFixationDetection.DetectFixation(
              dataPoint.IsEmpty ? false : true,
              Convert.ToInt64(rowRaw[3]),
              dataPoint.X,
              dataPoint.Y,
              maxDistance,
              minSamples,
              out point_found_delayed,
              out x_delayed,
              out y_delayed,
              out deviation_delayed,
              out x_fix_delayed,
              out y_fix_delayed,
              out saccade_duration_delayed,
              out fix_start_time,
              out fix_duration_delayed_milliseconds,
              out fix_duration_delayed_samples);

            switch (currentState)
            {
              case EyeMotionState.FIXATING:
                if (!isFixation)
                {
                  isFixation = true;
                }

                break;
              case EyeMotionState.FIXATION_COMPLETED:
                PointF fixationCenter = new PointF(x_fix_delayed, y_fix_delayed);

                // if (!Queries.OutOfScreen(fixationCenter)) TODO
                {
                  Fixation completedFixation = new Fixation();
                  completedFixation.CountInTrial = counterFix + 1;
                  completedFixation.Length = fix_duration_delayed_milliseconds;
                  completedFixation.PosX = x_fix_delayed;
                  completedFixation.PosY = y_fix_delayed;
                  completedFixation.SampleType = sampleType;
                  completedFixation.StartTime = fix_start_time - trialStartTime;
                  completedFixation.SubjectName = (string)trialRow["SubjectName"];
                  completedFixation.TrialID = (int)trialRow["TrialID"];
                  completedFixation.TrialSequence = (int)trialRow["TrialSequence"];

                  if (this.eliminateFirstFixation && counterFix == 0 && VGPolyline.Distance(fixationCenter, trialLastFixCenter) < maxDistance && fix_duration_delayed_milliseconds < this.limitForFirstFixation)
                  {
                    // Eliminate if applicable
                  }
                  else
                  {
                    fixations.Add(completedFixation);
                    counterFix++;
                  }
                }

                isFixation = false;
                break;
              case EyeMotionState.ERROR:
                break;
              default:
                break;
            }
          }

          counterRows++;

          // End RawData Loop
        }

        // Save last Fix if it has not been saved by a following saccade
        if (isFixation && fix_duration_delayed_milliseconds > 0)
        {
          Fixation lastFixation = new Fixation();
          lastFixation.CountInTrial = counterFix + 1;
          lastFixation.Length = fix_duration_delayed_milliseconds;
          lastFixation.PosX = x_fix_delayed;
          lastFixation.PosY = y_fix_delayed;
          lastFixation.SampleType = sampleType;
          lastFixation.StartTime = fix_start_time - trialStartTime;
          lastFixation.SubjectName = (string)trialRow["SubjectName"];
          lastFixation.TrialID = (int)trialRow["TrialID"];
          lastFixation.TrialSequence = (int)trialRow["TrialSequence"];
          fixations.Add(lastFixation);
        }

        // Save last FixCenter for Eliminating first Fix of new trial if choosen in UI
        trialLastFixCenter.X = x_fix_delayed;
        trialLastFixCenter.Y = y_fix_delayed;

        if (this.mergeConsecutiveFixations)
        {
          // Look for consecutive fixations that are beneath each other
          // (within GazeMaxDistance) because of
          // miscalculation due to blinks or missing data.
          List<Fixation> mergedFixations = new List<Fixation>();
          if (fixations.Count > 1)
          {
            Fixation foregoingFixation = fixations[0];
            bool merged = false;
            int mergedCounter = 0;
            int consecutiveMerges = 0;
            int trialFixCounter = 1;
            PointF foregoingFixationCenter = new PointF(foregoingFixation.PosX, foregoingFixation.PosY);
            for (int i = 1; i < fixations.Count; i++)
            {
              // Test if consecutive calculated fixations lie in GazeMaxDistanceRange
              PointF fixationCenter = new PointF(fixations[i].PosX, fixations[i].PosY);
              int distance = (int)VGPolyline.Distance(foregoingFixationCenter, fixationCenter);
              if (distance < maxDistance)
              {
                long sumOfDuration = foregoingFixation.Length + fixations[i].Length;
                foregoingFixation.PosX = (foregoingFixation.PosX * foregoingFixation.Length +
                  fixations[i].PosX * fixations[i].Length) / sumOfDuration;
                foregoingFixation.PosY = (foregoingFixation.PosY * foregoingFixation.Length +
                  fixations[i].PosY * fixations[i].Length) / sumOfDuration;
                foregoingFixation.Length = fixations[i].StartTime - foregoingFixation.StartTime + fixations[i].Length;
                merged = true;
                mergedCounter++;
                consecutiveMerges++;
              }
              else
              {
                if (!merged)
                {
                  foregoingFixation.CountInTrial = trialFixCounter;
                  mergedFixations.Add(foregoingFixation);
                  trialFixCounter++;
                }
                else
                {
                  merged = false;
                  foregoingFixation.CountInTrial -= mergedCounter - 1;
                  foregoingFixation.CountInTrial = trialFixCounter;
                  mergedFixations.Add(foregoingFixation);
                  consecutiveMerges = 0;
                  trialFixCounter++;
                }

                foregoingFixation = fixations[i];
                foregoingFixationCenter = fixationCenter;
              }
            }

            if (!merged)
            {
              foregoingFixation.CountInTrial = trialFixCounter;
              mergedFixations.Add(foregoingFixation);
              trialFixCounter++;
            }
          }
          else
          {
            mergedFixations = fixations;
          }

          foreach (Fixation fixationToSave in mergedFixations)
          {
            this.SaveFixationToTable(fixationToSave);
          }
        }
        else
        {
          // Don not merge fixations, use the originals.
          foreach (Fixation fixationToSave in fixations)
          {
            this.SaveFixationToTable(fixationToSave);
          }
        }

        // increase TrialCounter
        counterTrial++;

        if (worker != null)
        {
          if (worker.CancellationPending)
          {
            e.Cancel = true;
            break;
          }
          else
          {
            // Report progress as a percentage of the total task.
            int percentComplete = Convert.ToInt32(Convert.ToSingle(counterTrial) / trialsTable.Rows.Count * 100);
            worker.ReportProgress(percentComplete);
          }
        }

        // End Trial Loop
      }

      // Save Data to MDF File
      this.WriteToMDF(sampleType);
    }

    #endregion //PUBLICMETHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Inherited methods                                                         //
    ///////////////////////////////////////////////////////////////////////////////
    #region OVERRIDES
    #endregion //OVERRIDES

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

    /// <summary>
    /// The <see cref="BackgroundWorker.DoWork"/> event handler for the
    /// <see cref="BackgroundWorker"/> <see cref="bgwCalcFixationsForOneSubject"/>.
    /// Background worker thread working method for calculating
    /// the fixation data of one selected subject.
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">A <see cref="DoWorkEventArgs"/> with the event data.</param>
    private void bgwCalcFixationsForOneSubject_DoWork(object sender, DoWorkEventArgs e)
    {
      // Get the BackgroundWorker that raised this event.
      BackgroundWorker worker = sender as BackgroundWorker;

      object[] arguments = (object[])e.Argument;

      this.CalcFixations(
        SampleType.Gaze,
        (string)arguments[0],
        (OgamaDataSet.TrialsDataTable)arguments[1],
        worker,
        e);

      this.CalcFixations(
        SampleType.Mouse,
        (string)arguments[0],
        (OgamaDataSet.TrialsDataTable)arguments[1],
        worker,
        e);
    }

    #endregion //BACKGROUNDWORKER

    ///////////////////////////////////////////////////////////////////////////////
    // Methods for doing main class job                                          //
    ///////////////////////////////////////////////////////////////////////////////
    #region PRIVATEMETHODS

    /// <summary>
    /// Writes given fixation data into the data sets fixation tables.
    /// </summary>
    /// <param name="fixationToSave">Fixation to be written to database</param>
    private void SaveFixationToTable(Fixation fixationToSave)
    {
      DataTable fixTable = null;
      if (fixationToSave.SampleType == (fixationToSave.SampleType | SampleType.Gaze))
      {
        fixTable = Document.ActiveDocument.DocDataSet.GazeFixations;
      }
      else if (fixationToSave.SampleType == (fixationToSave.SampleType | SampleType.Mouse))
      {
        fixTable = Document.ActiveDocument.DocDataSet.MouseFixations;
      }

      // Create New Fixation DataRow
      DataRow workRowFixationData;
      workRowFixationData = fixTable.NewRow();

      // Fill with Data
      workRowFixationData["SubjectName"] = fixationToSave.SubjectName;
      workRowFixationData["TrialID"] = fixationToSave.TrialID;
      workRowFixationData["TrialSequence"] = fixationToSave.TrialSequence;
      workRowFixationData["CountInTrial"] = fixationToSave.CountInTrial;
      workRowFixationData["StartTime"] = fixationToSave.StartTime;
      workRowFixationData["Length"] = fixationToSave.Length;
      workRowFixationData["PosX"] = fixationToSave.PosX;
      workRowFixationData["PosY"] = fixationToSave.PosY;

      // Write into DB
      try
      {
        fixTable.Rows.Add(workRowFixationData);
      }
      catch (Exception ex)
      {
        ExceptionMethods.HandleException(ex);
      }
    }

    /// <summary>
    /// Writes fixation data table into MDF File database with bulk statement.
    /// </summary>
    /// <remarks>The bulk statement reduces the time consumption for large amount
    /// of data.</remarks>
    /// <param name="sampleType">A <see cref="SampleType"/> which indicates
    /// the sampling data source, gaze or mouse.</param>
    private void WriteToMDF(SampleType sampleType)
    {
      try
      {
        string fixTableName = string.Empty;
        DataTable fixTable = null;
        if (sampleType == (sampleType | SampleType.Gaze))
        {
          fixTableName = "dbo.GazeFixations";
          fixTable = Document.ActiveDocument.DocDataSet.GazeFixations;
        }
        else if (sampleType == (sampleType | SampleType.Mouse))
        {
          fixTableName = "dbo.MouseFixations";
          fixTable = Document.ActiveDocument.DocDataSet.MouseFixations;
        }

        // Delete Entrys in current tables because BulkInsert will insert all rows again
        string queryString = "DELETE FROM " + fixTableName + ";";
        SqlCommand command = new SqlCommand(queryString, Document.ActiveDocument.DocDataSet.DatabaseConnection);
        SqlDataReader reader = command.ExecuteReader();
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

        // Write new Entrys
        using (SqlBulkCopy bcp = new SqlBulkCopy(Document.ActiveDocument.DocDataSet.DatabaseConnection))
        {
          bcp.BulkCopyTimeout = 6000;

          // Write from the source to the destination.
          bcp.DestinationTableName = fixTableName;
          bcp.WriteToServer(fixTable);
          bcp.Close();
        }
      }
      catch (Exception ex)
      {
        ExceptionMethods.HandleException(ex);
      }
    }

    /// <summary>
    /// This method reads the fixation calculation settings from the current 
    /// ExperimentSettings
    /// </summary>
    private void ReadFixationCalculationSettings()
    {
      this.gazeMaxDistance = Document.ActiveDocument.ExperimentSettings.GazeMaxDistance;
      this.gazeMinSamples = Document.ActiveDocument.ExperimentSettings.GazeMinSamples;

      this.mouseMaxDistance = Document.ActiveDocument.ExperimentSettings.MouseMaxDistance;
      this.mouseMinSamples = Document.ActiveDocument.ExperimentSettings.MouseMinSamples;

      this.mergeConsecutiveFixations = Document.ActiveDocument.ExperimentSettings.MergeConsecutiveFixations;
      this.eliminateFirstFixation = Document.ActiveDocument.ExperimentSettings.EliminateFirstFixation;
      this.limitForFirstFixation = Document.ActiveDocument.ExperimentSettings.LimitForFirstFixation;
    }

    #endregion //PRIVATEMETHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER
    #endregion //HELPER
  }
}
