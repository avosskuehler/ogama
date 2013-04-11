// <copyright file="OgamaDataSet.cs" company="FU Berlin">
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

namespace Ogama.DataSet
{
  using System;
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.Data;
  using System.Data.SqlClient;
  using System.Drawing;
  using System.IO;
  using System.Windows.Forms;

  using Ogama.DataSet.OgamaDataSetTableAdapters;
  using Ogama.ExceptionHandling;
  using Ogama.MainWindow;
  using Ogama.MainWindow.Dialogs;
  using Ogama.Modules.Common;
  using Ogama.Modules.Common.SlideCollections;
  using Ogama.Modules.Common.Tools;
  using Ogama.Modules.Common.TrialEvents;

  using VectorGraphics.Elements;
  using VectorGraphics.Elements.ElementCollections;
  using VectorGraphics.StopConditions;

  /// <summary>
  /// Gaze and mouse viewer dataset.
  /// Stores and creates SQL connection queries and handling.
  /// </summary>
  public partial class OgamaDataSet
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
    /// Holds TableAdapter for SubjectTable
    /// </summary>
    private TadSubjects tadSubjects;

    /// <summary>
    /// Holds TableAdapter for SubjectParametersTable
    /// </summary>
    private TadSubjectParameters tadSubjectParameters;

    /// <summary>
    /// Holds TableAdapter for TrialTable
    /// </summary>
    private TadTrials tadTrials;

    /// <summary>
    /// Holds TableAdapter for TrialEvents table
    /// </summary>
    private TadTrialEvents tadTrialEvents;

    /// <summary>
    /// Holds TableAdapter for AOITable
    /// </summary>
    private TadAOIs tadAOIs;

    /// <summary>
    /// Holds TableAdapter for GazeFixationsTable
    /// </summary>
    private TadGazeFixations tadGazeFixations;

    /// <summary>
    /// Holds TableAdapter for MouseFixationsTable
    /// </summary>
    private TadMouseFixations tadMouseFixations;

    /// <summary>
    /// Holds TableAdapter for ShapeGroupsTable
    /// </summary>
    private TadShapeGroups tadShapeGroups;

    /// <summary>
    /// Holds TableAdapter for ParamsTable
    /// </summary>
    private TadParams tadParams;

    /// <summary>
    /// Saves the connection to the database
    /// </summary>
    private SqlConnection sqlConnection;

    /// <summary>
    /// Holds RawTableAdapterList, can be accessed through SubjectName
    /// </summary>
    private Dictionary<string, SqlDataAdapter> rawDataAdapterDict = new Dictionary<string, SqlDataAdapter>();

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets or sets TableAdapter for SubjectsTable
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public TadSubjects SubjectsAdapter
    {
      get { return this.tadSubjects; }
      set { this.tadSubjects = value; }
    }

    /// <summary>
    /// Gets or sets TableAdapter for SubjectParametersTable
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public TadSubjectParameters SubjectParametersAdapter
    {
      get { return this.tadSubjectParameters; }
      set { this.tadSubjectParameters = value; }
    }

    /// <summary>
    /// Gets or sets TableAdapter for ParamsTable
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public TadParams ParamsAdapter
    {
      get { return this.tadParams; }
      set { this.tadParams = value; }
    }

    /// <summary>
    /// Gets or sets TableAdapter for TrialsTable
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public TadTrials TrialsAdapter
    {
      get { return this.tadTrials; }
      set { this.tadTrials = value; }
    }

    /// <summary>
    /// Gets or sets TableAdapter for TrialEvents table
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public TadTrialEvents TrialEventsAdapter
    {
      get { return this.tadTrialEvents; }
      set { this.tadTrialEvents = value; }
    }

    /// <summary>
    /// Gets or sets TableAdapter for AOITable
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public TadAOIs AOIsAdapter
    {
      get { return this.tadAOIs; }
      set { this.tadAOIs = value; }
    }

    /// <summary>
    /// Gets or sets TableAdapter for GazeFixationsTable
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public TadGazeFixations GazeFixationsAdapter
    {
      get { return this.tadGazeFixations; }
      set { this.tadGazeFixations = value; }
    }

    /// <summary>
    /// Gets or sets TableAdapter for MouseFixationsTable
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public TadMouseFixations MouseFixationsAdapter
    {
      get { return this.tadMouseFixations; }
      set { this.tadMouseFixations = value; }
    }

    /// <summary>
    /// Gets or sets TableAdapter for ShapeGroupsTable
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public TadShapeGroups ShapeGroupsAdapter
    {
      get { return this.tadShapeGroups; }
      set { this.tadShapeGroups = value; }
    }

    /// <summary>
    /// Gets RawTableAdapterList, can be accessed through SubjectName.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Dictionary<string, SqlDataAdapter> RawDataAdapterDict
    {
      get { return this.rawDataAdapterDict; }
    }

    /// <summary>
    /// Gets the <see cref="SqlConnection"/> for the SQLExpress Database.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public SqlConnection DatabaseConnection
    {
      get { return this.sqlConnection; }
    }

    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION
    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Public methods                                                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region PUBLICMETHODS

    /// <summary>
    /// Creates TableAdapters and Loads Data from SQLDatabase into TableAdapters
    /// </summary>
    /// <param name="splash">A <see cref="BackgroundWorker"/> for the progress splash
    /// window which can be cancelled and restarted when showing dialogs.</param>
    /// <returns><strong>True</strong> if loading was succesfull, otherwise
    /// <strong>false</strong>.</returns>
    public bool LoadData(BackgroundWorker splash)
    {
      try
      {
        this.sqlConnection = new SqlConnection(Document.ActiveDocument.ExperimentSettings.DatabaseConnectionString);
        this.sqlConnection.Open();

        ((System.ComponentModel.ISupportInitialize)this).BeginInit();
        this.DataSetName = "OgamaDataSet";
        //// this.SchemaSerializationMode = SchemaSerializationMode.IncludeSchema;

        // tadSubjects
        this.tadSubjects = new TadSubjects();
        this.tadSubjects.ClearBeforeFill = true;
        this.tadSubjects.Connection = this.sqlConnection;

        // tadSubjectParameters
        this.tadSubjectParameters = new TadSubjectParameters();
        this.tadSubjectParameters.ClearBeforeFill = true;
        this.tadSubjectParameters.Connection = this.sqlConnection;

        // tadParams
        this.tadParams = new TadParams();
        this.tadParams.ClearBeforeFill = true;
        this.tadParams.Connection = this.sqlConnection;

        // tadTrials
        this.tadTrials = new TadTrials();
        this.tadTrials.ClearBeforeFill = true;
        this.tadTrials.Connection = this.sqlConnection;

        // tadTrialEvents
        this.tadTrialEvents = new TadTrialEvents();
        this.tadTrialEvents.ClearBeforeFill = true;
        this.tadTrialEvents.Connection = this.sqlConnection;

        // tadAOIs
        this.tadAOIs = new TadAOIs();
        this.tadAOIs.ClearBeforeFill = true;
        this.tadAOIs.Connection = this.sqlConnection;

        // tadGazeFixations
        this.tadGazeFixations = new TadGazeFixations();
        this.tadGazeFixations.ClearBeforeFill = true;
        this.tadGazeFixations.Connection = this.sqlConnection;

        // tadMouseFixations
        this.tadMouseFixations = new TadMouseFixations();
        this.tadMouseFixations.ClearBeforeFill = true;
        this.tadMouseFixations.Connection = this.sqlConnection;

        // tadShapeGroups
        this.tadShapeGroups = new TadShapeGroups();
        this.tadShapeGroups.ClearBeforeFill = true;
        this.tadShapeGroups.Connection = this.sqlConnection;

        if (!this.UpgradeDatabase(splash))
        {
          return false;
        }

        // Loads data into the OgamaDataSet tables
        this.tadSubjects.Fill(this.Subjects);
        this.tadParams.Fill(this.Params);
        this.tadShapeGroups.Fill(this.ShapeGroups);
        this.tadSubjectParameters.Fill(this.SubjectParameters);
        this.tadTrials.Fill(this.Trials);
        this.tadTrialEvents.Fill(this.TrialEvents);
        this.tadAOIs.Fill(this.AOIs);
        this.tadGazeFixations.Fill(this.GazeFixations);
        this.tadMouseFixations.Fill(this.MouseFixations);

        this.CreateRawDataAdapters();

        ((System.ComponentModel.ISupportInitialize)this).EndInit();
      }
      catch (Exception ex)
      {
        ExceptionMethods.HandleException(ex);

        if (this.sqlConnection != null)
        {
          this.sqlConnection.Close();
        }

        return false;
      }

      return true;
    }

    /// <summary>
    ///  Create raw data table adapter and add it to AdapterDictionary
    /// </summary>
    public void CreateRawDataAdapters()
    {
      this.rawDataAdapterDict.Clear();
      foreach (DataRow subjectRow in this.Subjects.Rows)
      {
        string subjectName = subjectRow["SubjectName"].ToString();
        SqlDataAdapter adapter = this.CreateAdapterBySubject(subjectName);
        this.rawDataAdapterDict.Add(subjectName, adapter);
      }
    }

    /// <summary>
    /// Creates a RawDataTableAdapter for a given subject
    /// </summary>
    /// <param name="subject">SubjectName as given in subjects table</param>
    /// <returns><see cref="SqlDataAdapter"/> that can be used for queries</returns>
    public SqlDataAdapter CreateAdapterBySubject(string subject)
    {
      SqlDataAdapter adapter = new SqlDataAdapter();
      System.Data.Common.DataTableMapping tableMapping = new System.Data.Common.DataTableMapping();
      tableMapping.SourceTable = "Table";
      tableMapping.DataSetTable = "Rawdata";
      tableMapping.ColumnMappings.Add("ID", "ID");
      tableMapping.ColumnMappings.Add("SubjectName", "SubjectName");
      tableMapping.ColumnMappings.Add("TrialSequence", "TrialSequence");
      tableMapping.ColumnMappings.Add("Time", "Time");
      tableMapping.ColumnMappings.Add("PupilDiaX", "PupilDiaX");
      tableMapping.ColumnMappings.Add("PupilDiaY", "PupilDiaY");
      tableMapping.ColumnMappings.Add("GazePosX", "GazePosX");
      tableMapping.ColumnMappings.Add("GazePosY", "GazePosY");
      tableMapping.ColumnMappings.Add("MousePosX", "MousePosX");
      tableMapping.ColumnMappings.Add("MousePosY", "MousePosY");
      tableMapping.ColumnMappings.Add("EventID", "EventID");
      adapter.TableMappings.Add(tableMapping);

      return adapter;
    }

    #endregion //PUBLICMETHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Inherited methods                                                         //
    ///////////////////////////////////////////////////////////////////////////////
    #region OVERRIDES

    /// <summary>
    /// Cleans Up resources used by this DataSet
    /// </summary>
    /// <param name="disposing">True if is disposing.</param>
    protected override void Dispose(bool disposing)
    {
      if (this.DatabaseConnection != null)
      {
        this.DatabaseConnection.Close();
      }

      if (this.tadShapeGroups != null)
      {
        this.tadShapeGroups.Dispose();
      }

      if (this.tadParams != null)
      {
        this.tadParams.Dispose();
      }

      if (this.tadMouseFixations != null)
      {
        this.tadMouseFixations.Dispose();
      }

      if (this.tadGazeFixations != null)
      {
        this.tadGazeFixations.Dispose();
      }

      if (this.tadSubjects != null)
      {
        this.tadSubjects.Dispose();
      }

      if (this.tadSubjectParameters != null)
      {
        this.tadSubjectParameters.Dispose();
      }

      if (this.tadTrials != null)
      {
        this.tadTrials.Dispose();
      }

      if (this.tadTrialEvents != null)
      {
        this.tadTrialEvents.Dispose();
      }

      if (this.tadAOIs != null)
      {
        this.tadAOIs.Dispose();
      }

      foreach (SqlDataAdapter rawAdapter in this.rawDataAdapterDict.Values)
      {
        rawAdapter.Dispose();
      }

      base.Dispose(disposing);
    }

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
    /// The <see cref="BackgroundWorker.DoWork"/> event handler for
    /// the local <see cref="BackgroundWorker"/> bgwUpgradeSplash.
    /// Shows the <see cref="UpgradeDocumentSplash"/> form with a splash screen
    /// wait dialog.
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">A <see cref="DoWorkEventArgs"/> with the event data.</param>
    private void bgwUpgradeSplash_DoWork(object sender, DoWorkEventArgs e)
    {
      // Get the BackgroundWorker that raised this event.
      BackgroundWorker worker = sender as BackgroundWorker;

      UpgradeDocumentSplash newSplash = new UpgradeDocumentSplash();
      newSplash.Worker = worker;
      newSplash.ShowDialog();
    }

    #endregion //BACKGROUNDWORKER

    ///////////////////////////////////////////////////////////////////////////////
    // Methods for doing main class job                                          //
    ///////////////////////////////////////////////////////////////////////////////
    #region METHODS

    /// <summary>
    /// This method renames and adds columns and tables in the given database,
    /// reflecting the changes made in the versions history
    /// </summary>
    /// <param name="splash">A <see cref="BackgroundWorker"/> for the progress splash
    /// window which can be cancelled and restarted when showing dialogs.</param>
    /// <returns><strong>True</strong> if upgrade was succesfull, and <strong>false</strong>
    /// if unsuccesfull or cancelled by user.</returns>
    private bool UpgradeDatabase(BackgroundWorker splash)
    {
      if (Queries.ColumnExists("Subjects", "Param1") || Queries.TableExists("TableMainExperiment"))
      {
        // Create splash worker for progress splash.
        BackgroundWorker bgwUpgradeSplash = new BackgroundWorker();
        bgwUpgradeSplash.WorkerSupportsCancellation = true;
        bgwUpgradeSplash.DoWork += new DoWorkEventHandler(this.bgwUpgradeSplash_DoWork);

        // Stop showing loading splash screen if it is running
        if (splash != null && splash.IsBusy)
        {
          splash.CancelAsync();
        }

        // Give time to close
        Application.DoEvents();

        string message = "The database will be updated to the current OGAMA V2 database format.";
        DialogResult result = InformationDialog.Show("Database update needed", message, true, MessageBoxIcon.Question);
        switch (result)
        {
          case DialogResult.Cancel:
          case DialogResult.No:
            return false;
          case DialogResult.Yes:
            break;
        }

        bgwUpgradeSplash.RunWorkerAsync();

        // Give time to start
        Application.DoEvents();

        ///////////////////////////////////////////
        // 0.X -> 1.X changes                    //
        ///////////////////////////////////////////

        if (Queries.TableExists("TableMainExperiment"))
        {
          // Table AOIs
          Queries.RenameTable("TableAOI", "AOIs");
          Queries.RenameColumn("AOIs", "ImageFile", "StimulusFile");

          // Fixation tables
          this.AddGazeFixationsTable();
          this.AddMouseFixationsTable();
          Queries.RemoveTable("TableFixations");

          // Subjects table
          Queries.RenameTable("TableMainExperiment", "Subjects");
          Queries.ChangeColumnType("Subjects", "Sex", "varchar(50) NULL");
          Queries.ChangeColumnType("Subjects", "Param1", "varchar(200) NULL");
          Queries.ChangeColumnType("Subjects", "Param2", "varchar(200) NULL");
          Queries.ChangeColumnType("Subjects", "Param3", "varchar(200) NULL");

          // Trials table
          Queries.RenameTable("TableSubject", "Trials");
          Queries.ChangeColumnType("Trials", "Categorie", "varchar(50) NULL");
          Queries.RenameColumn("Trials", "ImageFile", "StimulusFile");
          Queries.RenameColumn("Trials", "SubjectsChoice", "ResponseCorrectness");
          Queries.ChangeColumnType("Trials", "ResponseCorrectness", "varchar(50) NULL");
          Queries.AddColumn("Trials", "SubjectsResponse", "varchar(50) NULL");

          // Raw Data tables
          Queries.RemoveTable("TableRawData");
          this.UpgradeRawDataTablesV0ToV1();
        }

        ///////////////////////////////////////////
        // 1.X -> 2.0 changes                    //
        ///////////////////////////////////////////

        // Add new tables
        this.AddShapeGroupsTable();
        this.AddTrialEventsTable();
        this.AddSubjectParametersTable();

        // Add identity columns if not there already
        Queries.AddIdentityColumn("Subjects");
        Queries.AddIdentityColumn("Trials");
        Queries.AddIdentityColumn("GazeFixations");
        Queries.AddIdentityColumn("MouseFixations");

        // Get slides (which were in 1.X the same as trials)
        // by reading the stimulusFiles, each file is a slide.
        List<string> stimulusFiles = this.GetStimulusFiles();

        // Table subjects
        this.UpgradeSubjectParams();
        Queries.RenameColumn("Subjects", "Categorie", "Category");
        Queries.RemoveColumn("Subjects", "Param1");
        Queries.RemoveColumn("Subjects", "Param2");
        Queries.RemoveColumn("Subjects", "Param3");

        // Table trials
        this.UpgradeResponses(bgwUpgradeSplash);
        Queries.RenameColumn("Trials", "TrialID", "TrialSequence");
        Queries.AddColumn("Trials", "TrialID", "int");
        this.AddTrialIDsforStimulusFiles("Trials", stimulusFiles);
        Queries.ChangeColumnType("Trials", "TrialID", "int NOT NULL");
        Queries.RenameColumn("Trials", "Categorie", "Category");
        Queries.RenameColumn("Trials", "StimulusFile", "TrialName");
        Queries.ChangeColumnType("Trials", "TrialName", "varchar(50)");
        Queries.RemoveColumn("Trials", "SubjectsResponse");
        Queries.RemoveColumn("Trials", "ResponseCorrectness");

        // Table AOIs
        this.UpgradeAOITable();
        Queries.AddColumn("AOIs", "TrialID", "int");
        this.AddTrialIDsforStimulusFiles("AOIs", stimulusFiles);
        Queries.ChangeColumnType("AOIs", "TrialID", "int NOT NULL");
        Queries.AddColumn("AOIs", "SlideNr", "int NOT NULL DEFAULT 0");
        Queries.RenameColumn("AOIs", "Target", "ShapeGroup");
        Queries.RemoveColumn("AOIs", "StimulusFile");
        this.RenamePolygonToPolylineInAOITable();

        // Table GazeFixations
        Queries.RenameColumn("GazeFixations", "TrialID", "TrialSequence");
        Queries.AddColumn("GazeFixations", "TrialID", "int");
        this.AddTrialIDsforStimulusFiles("GazeFixations", stimulusFiles);
        Queries.ChangeColumnType("GazeFixations", "TrialID", "int NOT NULL");
        Queries.RemoveColumn("GazeFixations", "StimulusFile");

        // Table MouseFixations
        Queries.RenameColumn("MouseFixations", "TrialID", "TrialSequence");
        Queries.AddColumn("MouseFixations", "TrialID", "int");
        this.AddTrialIDsforStimulusFiles("MouseFixations", stimulusFiles);
        Queries.ChangeColumnType("MouseFixations", "TrialID", "int NOT NULL");
        Queries.RemoveColumn("MouseFixations", "StimulusFile");

        // All RawData tables
        this.UpgradeRawDataTablesV1ToV2();

        // Move folder contents
        this.MoveStimuli();

        // Check for empty slideshows of imported experiments
        if (Document.ActiveDocument.ExperimentSettings.SlideShow.Nodes.Count == 0
          && stimulusFiles.Count > 0)
        {
          this.GenerateTrials(stimulusFiles);
        }

        this.RearrangeColumnLayoutInTables();

        // Hide database upgrade splash
        bgwUpgradeSplash.CancelAsync();

        // Give time to close
        Application.DoEvents();

        // Show success dialog
        message = "Congratulations ! Your database has now Ogama 2.0 format."
          + Environment.NewLine +
          "This was quite a hard work for your computer ... stay tuned.";
        InformationDialog.Show(
          "Upgrade succesfull",
          message,
          false,
          MessageBoxIcon.Information);

        // Show loading splash screen if it is not running
        if (splash != null && !splash.IsBusy)
        {
          splash.RunWorkerAsync();
        }
      }

      ///////////////////////////////////////////
      // 2.1 -> 2.2 changes                    //
      ///////////////////////////////////////////

      if (!Queries.TableExists("Params"))
      {
        this.AddParamsTable();
        this.UpdateParamsTableWithExistingParams();
      }

      return true;
    }

    /// <summary>
    /// This method recreates all tables in the database except ShapeGroups, TrialEvents, SubjectParameters
    /// (they were created before) to ensure the correct column arrangement with identity column
    /// at the first index. This ensures that automatically generated insertion and select
    /// statements from the dataset will align with the correct columns.
    /// </summary>
    /// <remarks>Don´t say the order of the columns in an SQL database is irrelevant,
    /// this is true for handmade queries, but not for the Visual Studio auto-generated
    /// queries, which rely on the same column layout in database and mdf file.</remarks>
    private void RearrangeColumnLayoutInTables()
    {
      string tableName = "Subjects";
      string columnDefinitions = "[ID] [bigint] IDENTITY(1,1) NOT NULL,[SubjectName] [varchar](50) COLLATE Latin1_General_CI_AS NOT NULL,[Category] [varchar](50) COLLATE Latin1_General_CI_AS NULL,[Age] [int] NULL,[Sex] [varchar](50) COLLATE Latin1_General_CI_AS NULL,[Handedness] [varchar](50) COLLATE Latin1_General_CI_AS NULL,[Comments] [varchar](4000) COLLATE Latin1_General_CI_AS NULL";
      string columns = "SubjectName, Category, Age, Sex, Handedness, Comments";

      Queries.RecreateTable(tableName, columnDefinitions, columns);

      tableName = "Trials";
      columnDefinitions = "[ID] [bigint] IDENTITY(1,1) NOT NULL, [SubjectName] [varchar](50) COLLATE Latin1_General_CI_AS NOT NULL, [TrialID] [int] NOT NULL, [TrialName] [varchar](50) COLLATE Latin1_General_CI_AS NULL, [TrialSequence] [int] NOT NULL, [Category] [varchar](50) COLLATE Latin1_General_CI_AS NULL, [TrialStartTime] [bigint] NULL, [Duration] [int] NULL, [EliminateData] [varchar](50) COLLATE Latin1_General_CI_AS NULL";
      columns = "SubjectName, TrialID, TrialName, TrialSequence, Category, TrialStartTime, Duration, EliminateData";

      Queries.RecreateTable(tableName, columnDefinitions, columns);

      tableName = "AOIs";
      columnDefinitions = "[ID] [int] IDENTITY(1,1) NOT NULL, [TrialID] [int] NOT NULL, [SlideNr] [int] NOT NULL, [ShapeName] [varchar](50) COLLATE Latin1_General_CI_AS NULL, [ShapeType] [varchar](50) COLLATE Latin1_General_CI_AS NULL, [ShapeNumPts] [int] NULL, [ShapePts] [varchar](4000) COLLATE Latin1_General_CI_AS NULL, [ShapeGroup] [varchar](50) COLLATE Latin1_General_CI_AS NULL";
      columns = "TrialID, SlideNr, ShapeName, ShapeType, ShapeNumPts, ShapePts, ShapeGroup";

      Queries.RecreateTable(tableName, columnDefinitions, columns);

      tableName = "GazeFixations";
      columnDefinitions = "[ID] [bigint] IDENTITY(1,1) NOT NULL,[SubjectName] [varchar](50) COLLATE Latin1_General_CI_AS NOT NULL,[TrialID] [int] NOT NULL,[TrialSequence] [int] NOT NULL,[CountInTrial] [int] NULL,[StartTime] [bigint] NULL,[Length] [int] NULL,[PosX] [float] NULL,[PosY] [float] NULL";
      columns = "SubjectName, TrialID, TrialSequence, CountInTrial, StartTime, Length, PosX, PosY";

      Queries.RecreateTable(tableName, columnDefinitions, columns);

      tableName = "MouseFixations";
      Queries.RecreateTable(tableName, columnDefinitions, columns);

      // The following line gets the schema table that holds all of the columns from every table.
      DataTable tables = this.sqlConnection.GetSchema("Tables");

      foreach (DataRow tableRow in tables.Rows)
      {
        tableName = tableRow["TABLE_NAME"].ToString();
        if (tableName.Contains("Rawdata"))
        {
          columnDefinitions = "[ID] [bigint] IDENTITY(1,1) NOT NULL, [SubjectName] [varchar](50) COLLATE Latin1_General_CI_AS NOT NULL, [TrialSequence] [int] NOT NULL, [Time] [bigint] NOT NULL, [PupilDiaX] [float] NULL, [PupilDiaY] [float] NULL, [GazePosX] [float] NULL, [GazePosY] [float] NULL, [MousePosX] [float] NULL, [MousePosY] [float] NULL, [EventID] [int] NULL";
          columns = "SubjectName, TrialSequence, Time, PupilDiaX, PupilDiaY, GazePosX, GazePosY, MousePosX, MousePosY, EventID";

          Queries.RecreateTable(tableName, columnDefinitions, columns);
        }
      }
    }

    /// <summary>
    /// This method moves all files from the Stimuli folder to the SlideResources folder.
    /// </summary>
    private void MoveStimuli()
    {
      string stimuliPath = Path.Combine(Document.ActiveDocument.ExperimentSettings.DocumentPath, "Stimuli");
      DirectoryInfo dirInfoStimuli = new DirectoryInfo(stimuliPath);
      string resourcesPath = Document.ActiveDocument.ExperimentSettings.SlideResourcesPath;
      if (dirInfoStimuli.Exists)
      {
        foreach (FileInfo file in dirInfoStimuli.GetFiles())
        {
          string moveFileName = Path.Combine(resourcesPath, file.Name);
          if (!File.Exists(moveFileName))
          {
            File.Move(file.FullName, moveFileName);
          }
        }
      }
    }

    /// <summary>
    /// This method inserts for every stimulus file a new node into
    /// the slideshow tree with an id given by the stimulus index in the list.
    /// This method is only valid when no other nodes are in the slideshow already.
    /// </summary>
    /// <param name="stimulusFiles">A <see cref="List{String}"/> with the stimulus files.</param>
    private void GenerateTrials(List<string> stimulusFiles)
    {
      foreach (string stimulus in stimulusFiles)
      {
        // Create stopconditions
        StopConditionCollection responses = new StopConditionCollection();
        MouseStopCondition stopCondition = new MouseStopCondition(
          MouseButtons.Left,
          true,
          string.Empty,
          null,
          Point.Empty);
        responses.Add(stopCondition);

        // Create new slide
        string name = System.IO.Path.GetFileNameWithoutExtension(stimulus);
        Slide newSlide = new Slide(
          name,
          Color.Black,
          null,
          responses,
          new StopConditionCollection(),
          "ImportedSlide",
          Document.ActiveDocument.PresentationSize);
        newSlide.MouseCursorVisible = true;
        newSlide.BackgroundColor = Color.White;
        newSlide.Modified = true;

        // Create the stimulus item and add it to the slide
        VGImage image = new VGImage(
          ShapeDrawAction.None,
          (Pen)Pens.Black.Clone(),
          (Brush)Brushes.Black.Clone(),
          (Font)SystemFonts.DefaultFont.Clone(),
          Color.White,
          stimulus,
          Document.ActiveDocument.ExperimentSettings.SlideResourcesPath,
          ImageLayout.Center,
          1f,
          Document.ActiveDocument.PresentationSize,
          VGStyleGroup.None,
          string.Empty,
          string.Empty,
          false);
        newSlide.VGStimuli.Add(image);

        // Add trial node to slideshow
        SlideshowTreeNode slideNode = new SlideshowTreeNode(newSlide.Name);
        slideNode.Name = stimulusFiles.IndexOf(stimulus).ToString();
        slideNode.Slide = newSlide;
        Document.ActiveDocument.ExperimentSettings.SlideShow.Nodes.Add(slideNode);
      }

      Document.ActiveDocument.Modified = true;
    }

    /// <summary>
    /// This method parses the trials table for the trialIDs of the stimulus files.
    /// </summary>
    /// <returns>A <see cref="List{String}"/> with all stimuli files of the 
    /// experiment sorted by appearance in the database.</returns>
    private List<string> GetStimulusFiles()
    {
      var trialIDs = new List<string>();

      if (Queries.ColumnExists("Trials", "StimulusFile"))
      {
        const string TrialQueryString = "SELECT Trials.* FROM [dbo].[Trials]";
        var trialAdapter = new SqlDataAdapter
          {
            SelectCommand = new SqlCommand(TrialQueryString, this.sqlConnection)
          };

        var trialsTable = new DataTable("Trials");
        trialAdapter.Fill(trialsTable);

        foreach (DataRow trialRow in trialsTable.Rows)
        {
          var stimulusFile = trialRow["StimulusFile"].ToString();
          if (!trialIDs.Contains(stimulusFile))
          {
            trialIDs.Add(stimulusFile);
          }
        }
      }

      return trialIDs;
    }

    /// <summary>
    /// This method gets the StimulusFile column values and
    /// fills the empty trialID column with the index of the retreived StimulusFile 
    /// to have a unique represenation for the stimulusFile.
    /// </summary>
    /// <param name="tableName">A <see cref="String"/> with the name of the table with
    /// at least has a filled column 'StimulusFile' which is filled and an empty
    /// column 'TrialID' which should be filled.</param>
    /// <param name="stimulusFiles">A <see cref="List{String}"/> with the stimulus files.</param>
    private void AddTrialIDsforStimulusFiles(string tableName, List<string> stimulusFiles)
    {
      if (Queries.ColumnExists(tableName, "StimulusFile") && Queries.ColumnExists(tableName, "TrialID"))
      {
        string queryString = "SELECT " + tableName + ".* FROM dbo." + tableName;
        SqlDataAdapter adapter = new SqlDataAdapter();
        adapter.SelectCommand = new SqlCommand(queryString, this.sqlConnection);

        DataTable table = new DataTable(tableName);
        adapter.Fill(table);

        for (int i = 0; i < table.Rows.Count; i++)
        {
          string stimulusFile = table.Rows[i]["StimulusFile"].ToString();
          table.Rows[i]["TrialID"] = stimulusFiles.IndexOf(stimulusFile);
        }

        // Commit changes back to database by erasing is first
        // and then bulk copy the whole table
        string deleteQuery = "DELETE FROM dbo." + tableName;
        Queries.ExecuteSQLCommand(deleteQuery);

        using (SqlBulkCopy bcp = new SqlBulkCopy(this.sqlConnection))
        {
          bcp.DestinationTableName = "dbo." + tableName;

          // Write the data table back to the database
          bcp.WriteToServer(table);
        }
      }
    }

    /// <summary>
    /// This method reads the AOI shape groups and adds them to the new
    /// shape group table.
    /// </summary>
    private void UpgradeAOITable()
    {
      if (!Queries.ColumnExists("AOIs", "Target"))
      {
        return;
      }

      const string QueryString = "SELECT AOIs.* FROM [dbo].[AOIs] WHERE [Target] <> ''";
      var adapter = new SqlDataAdapter { SelectCommand = new SqlCommand(QueryString, this.sqlConnection) };
      var aoiTable = new DataTable("AOIs");
      adapter.Fill(aoiTable);

      var shapeGroups = new List<string>();

      // Get all named shapegroups from the aoi table
      foreach (DataRow aoiRow in aoiTable.Rows)
      {
        string shapeGroup = aoiRow["Target"].ToString();
        if (!shapeGroups.Contains(shapeGroup))
        {
          shapeGroups.Add(shapeGroup);
        }
      }

      // Insert default values
      if (!shapeGroups.Contains(string.Empty))
      {
        shapeGroups.Add(string.Empty);
      }

      if (!shapeGroups.Contains("Target"))
      {
        shapeGroups.Add("Target");
      }

      if (!shapeGroups.Contains("SearchRect"))
      {
        shapeGroups.Add("SearchRect");
      }

      // Insert shape groups into database
      foreach (string group in shapeGroups)
      {
        this.tadShapeGroups.Insert(group);
      }
    }

    /// <summary>
    /// This method replaces all occurences of "Polygon" with
    /// "Polyline" in older versions AOI Table, column ShapeType.
    /// </summary>
    private void RenamePolygonToPolylineInAOITable()
    {
      // Rename Polygon -> Polyline
      const string QueryString = "SELECT AOIs.* FROM [dbo].[AOIs]";
      var adapter = new SqlDataAdapter
        {
          SelectCommand = new SqlCommand(QueryString, this.sqlConnection)
        };

      var aoiTable = new AOIsDataTable();
      adapter.Fill(aoiTable);

      // Iterate all aoi from the aoi table
      foreach (DataRow aoiRow in aoiTable.Rows)
      {
        var shapeType = aoiRow["ShapeType"].ToString();
        if (shapeType == "Polygon")
        {
          aoiRow["ShapeType"] = "Polyline";
        }
      }

      int affectedRows = this.tadAOIs.Update(aoiTable);
    }

    /// <summary>
    /// This method moves the response and response correctness columns of the old trial table
    /// to the new trial events table.
    /// </summary>
    /// <param name="splash">A <see cref="BackgroundWorker"/> for the progress splash
    /// window which can be cancelled and restarted when showing dialogs.</param>
    private void UpgradeResponses(BackgroundWorker splash)
    {
      if (Queries.ColumnExists("Trials", "SubjectsResponse"))
      {
        string queryString = "SELECT Trials.* FROM [dbo].[Trials]";
        SqlDataAdapter adapter = new SqlDataAdapter();
        adapter.SelectCommand = new SqlCommand(queryString, this.sqlConnection);
        DataTable trialsTable = new DataTable("Trials");
        adapter.Fill(trialsTable);

        int eventCount = this.tadTrialEvents.GetData().Count;

        int responseConverterErrorCount = 0;
        int responseEmptyErrorCount = 0;
        foreach (DataRow trialRow in trialsTable.Rows)
        {
          string subjectName = trialRow["SubjectName"].ToString();
          string subjectsResponse = trialRow["SubjectsResponse"].ToString();
          string subjectsResponseCorrectness = trialRow["ResponseCorrectness"].ToString();
          long trialStartTime = Convert.ToInt64(trialRow["TrialStartTime"]);
          int duration = Convert.ToInt32(trialRow["Duration"]);
          int trialID = Convert.ToInt32(trialRow["TrialID"]);

          StopCondition response = null;
          try
          {
            response = (StopCondition)TypeDescriptor.GetConverter(typeof(StopCondition)).ConvertFrom(subjectsResponse);

            // If subjectsResponse field was Empty string (this is by default not possible, but 
            // due to some historical testing purpose sometimes possible).
            // So if no response is known, consider to had a Mouse : Left response.
            // That enables the converting of eventually written responses correctnesses
            if (response == null)
            {
              response = new MouseStopCondition(
                MouseButtons.Left,
                false,
                string.Empty,
                null,
                Point.Empty);

              responseEmptyErrorCount++;
            }

            // Parse correctness
            if (subjectsResponseCorrectness != string.Empty)
            {
              // Correct answers were marked with "correct answer" in V1.6 and later and before just "right"
              if (subjectsResponseCorrectness.Contains("correct") || subjectsResponseCorrectness.Contains("right"))
              {
                response.IsCorrectResponse = true;
              }
              else if (subjectsResponseCorrectness.Contains("wrong"))
              {
                response.IsCorrectResponse = false;
              }
            }
          }
          catch (Exception)
          {
            // Converting failed, so count errors
            responseConverterErrorCount++;
          }

          this.tadTrialEvents.Insert(
            subjectName,
            trialID,
            eventCount,
            duration,
            EventType.Response.ToString(),
            InputEventTask.SlideChange.ToString(),
            response != null ? response.ToString() : string.Empty);

          eventCount++;
        }

        // Stop showing converter splash screen if it is running
        if ((responseEmptyErrorCount > 0 || responseConverterErrorCount > 0)
          && (splash != null && splash.IsBusy))
        {
          splash.CancelAsync();
        }

        if (responseEmptyErrorCount > 0)
        {
          string message = "Please note: " + responseEmptyErrorCount.ToString() +
            " responses were empty." + Environment.NewLine +
            "They were considered to be Mouse: Left responses. ";
          ExceptionMethods.ProcessMessage("Empty responses found", message);
        }

        if (responseConverterErrorCount > 0)
        {
          string message = "Please note: " + responseConverterErrorCount.ToString() +
            " responses could not be converted succesfully." + Environment.NewLine +
            "This error is not critical, but statistical parameters 'Response' " +
            "and 'ResponseCorrectnes' will be not correct.";
          ExceptionMethods.ProcessMessage("Unknown responses found", message);
        }

        // Show converter splash screen if it is not running
        if (splash != null && !splash.IsBusy)
        {
          splash.RunWorkerAsync();
        }
      }
    }

    /// <summary>
    /// This method iterates through all tables checks for raw data tables
    /// and upgrades them renaming TrialID to TrialSequence and
    /// moving responses and flash state into the trial events table.
    /// </summary>
    private void UpgradeRawDataTablesV0ToV1()
    {
      // The following line gets the schema table that holds all of the columns from every table.
      DataTable tables = this.sqlConnection.GetSchema("Tables");

      foreach (DataRow tableRow in tables.Rows)
      {
        string tableName = tableRow["TABLE_NAME"].ToString();
        if (tableName.Contains("RawData"))
        {
          if (Queries.ColumnExists(tableName, "ClickEvent"))
          {
            Queries.RenameColumn(tableName, "ClickEvent", "Response");
            Queries.AddColumn(tableName, "FlashState", "varchar(5000)");
          }

          if (tableName.Contains("TableRawData"))
          {
            string newTableName = tableName.Replace("TableRawData", "Rawdata");
            Queries.RenameTable(tableName, newTableName);
          }
        }
      }
    }

    /// <summary>
    /// This method iterates through all tables checks for raw data tables
    /// and upgrades them renaming TrialID to TrialSequence and
    /// moving responses and flash state into the trial events table.
    /// </summary>
    private void UpgradeRawDataTablesV1ToV2()
    {
      // The following line gets the schema table that holds all of the columns from every table.
      DataTable tables = this.sqlConnection.GetSchema("Tables");

      int responseConverterErrorCount = 0;

      foreach (DataRow tableRow in tables.Rows)
      {
        string tableName = tableRow["TABLE_NAME"].ToString();
        if (tableName.Contains("Rawdata"))
        {
          if (Queries.ColumnExists(tableName, "Response") && Queries.ColumnExists(tableName, "FlashState"))
          {
            string subjectName = tableName.Replace("Rawdata", string.Empty);
            int eventCount = this.tadTrialEvents.GetDataBySubject(subjectName).Count;
            Queries.RenameColumn(tableName, "TrialID", "TrialSequence");
            Queries.AddColumn(tableName, "EventID", "int");
            Queries.AddPrimaryKeyOnIDColumn(tableName);
            string queryString = "SELECT dbo." + tableName + ".* FROM dbo." + tableName +
              " WHERE (Response IS NOT NULL AND Response <> 'None') OR FlashState <> ''";
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = new SqlCommand(queryString, this.sqlConnection);

            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            builder.QuotePrefix = "[";
            builder.QuoteSuffix = "]";

            DataTable rawTable = new DataTable(tableName);
            adapter.Fill(rawTable);

            int lastTrialSequence = -5;
            long trialStartTime = 0;
            foreach (DataRow rawRow in rawTable.Rows)
            {
              string responseField = rawRow["Response"].ToString();
              string flashstate = rawRow["FlashState"].ToString();

              if (responseField != string.Empty || flashstate != string.Empty)
              {
                int trialSequence = Convert.ToInt32(rawRow["TrialSequence"]);
                long time = Convert.ToInt64(rawRow["Time"]);
                float mouseX = rawRow.IsNull("MousePosX") ? -1f : Convert.ToSingle(rawRow["MousePosX"]);
                float mouseY = rawRow.IsNull("MousePosY") ? -1f : Convert.ToSingle(rawRow["MousePosY"]);

                // Get start time of trial
                if (lastTrialSequence != trialSequence)
                {
                  DataTable trial = this.tadTrials.GetDataBySubjectAndSequence(subjectName, trialSequence);
                  if (trial.Rows.Count == 1)
                  {
                    trialStartTime = Convert.ToInt64(trial.Rows[0]["TrialStartTime"]);
                  }

                  lastTrialSequence = trialSequence;
                }

                if (responseField != string.Empty)
                {
                  StopCondition response = null;
                  try
                  {
                    response = (StopCondition)TypeDescriptor.GetConverter(typeof(StopCondition)).ConvertFrom(responseField);

                    // Ignore empty response fields
                    if (response == null)
                    {
                      continue;
                    }

                    EventType eventType = EventType.None;
                    InputEventTask task = InputEventTask.None;
                    if (response is TimeStopCondition)
                    {
                      // TimeStopConditions are beeing ignored,
                      // because they always indicate a slide change
                      // which will be written to the trial events
                      // out of the old trials table
                    }
                    else if (response is KeyStopCondition)
                    {
                      eventType = EventType.Key;
                      task = InputEventTask.Down;
                    }
                    else if (response is MouseStopCondition)
                    {
                      eventType = EventType.Mouse;
                      task = InputEventTask.Down;
                      if (mouseX > -1 && mouseY > -1)
                      {
                        ((MouseStopCondition)response).ClickLocation = new Point((int)mouseX, (int)mouseY);
                      }
                    }

                    // Insert event.
                    this.tadTrialEvents.Insert(
                      subjectName,
                      trialSequence,
                      eventCount,
                      time - trialStartTime,
                      eventType.ToString(),
                      task.ToString(),
                      response != null ? response.ToString() : string.Empty);

                    rawRow["EventID"] = eventCount;

                    eventCount++;
                  }
                  catch (Exception)
                  {
                    // Converting failed, so count errors
                    responseConverterErrorCount++;
                  }
                }

                if (flashstate != string.Empty)
                {
                  // Insert event.
                  this.tadTrialEvents.Insert(
                    subjectName,
                    trialSequence,
                    eventCount,
                    time - trialStartTime,
                    EventType.Flash.ToString(),
                    MediaEventTask.Seek.ToString(),
                    flashstate);

                  eventCount++;
                }
              }
            }

            int affectedRows = adapter.Update(rawTable);
            Queries.RemoveColumn(tableName, "Response");
            Queries.RemoveColumn(tableName, "FlashState");
          }
        }
      }

      // Notify problems.
      if (responseConverterErrorCount > 0)
      {
        string message = "Please note: " + responseConverterErrorCount.ToString() +
          " responses could not be converted succesfully." + Environment.NewLine +
          "This error is not critical, but some Mouse or Key events will be missing.";
        ExceptionMethods.ProcessMessage("Unknown responses found", message);
      }
    }

    /// <summary>
    /// This method moves the parameters called param1,param2,param3 in the subject table
    /// to the new table SubjectParameters.
    /// </summary>
    private void UpgradeSubjectParams()
    {
      if (Queries.ColumnExists("Subjects", "Param1"))
      {
        string queryString = "SELECT Subjects.* FROM [dbo].[Subjects]";
        SqlDataAdapter adapter = new SqlDataAdapter();
        adapter.SelectCommand = new SqlCommand(queryString, this.sqlConnection);
        DataTable subjectTable = new DataTable("Subjects");
        adapter.Fill(subjectTable);

        foreach (DataRow subjectRow in subjectTable.Rows)
        {
          string subjectName = subjectRow["SubjectName"].ToString();
          string param1 = subjectRow["Param1"].ToString();
          string param2 = subjectRow["Param2"].ToString();
          string param3 = subjectRow["Param3"].ToString();
          this.tadSubjectParameters.Insert(subjectName, "Param1", param1);
          this.tadSubjectParameters.Insert(subjectName, "Param2", param2);
          this.tadSubjectParameters.Insert(subjectName, "Param3", param3);
        }
      }
    }

    /// <summary>
    /// This method parses the subjectparams table for already entered params
    /// and adds them to the new params table.
    /// </summary>
    private void UpdateParamsTableWithExistingParams()
    {
      string queryString = "SELECT SubjectParameters.* FROM [dbo].[SubjectParameters]";
      SqlDataAdapter adapter = new SqlDataAdapter();
      adapter.SelectCommand = new SqlCommand(queryString, this.sqlConnection);
      DataTable subjectParamsTable = new DataTable("SubjectParameters");
      adapter.Fill(subjectParamsTable);

      List<string> customParams = new List<string>();
      foreach (DataRow subjectRow in subjectParamsTable.Rows)
      {
        string subjectName = subjectRow["SubjectName"].ToString();
        string param = subjectRow["Param"].ToString();
        string paramValue = subjectRow["ParamValue"].ToString();

        if (!customParams.Contains(param))
        {
          customParams.Add(param);
        }
      }

      foreach (string customParam in customParams)
      {
        this.tadParams.Insert(customParam, string.Empty);
      }
    }

    /// <summary>
    /// This method checks for the table ShapeGroups in the database
    /// This table is new in Version 2.0 and is now added to older versions.
    /// </summary>
    private void AddShapeGroupsTable()
    {
      if (!Queries.TableExists("ShapeGroups"))
      {
        string queryString = "CREATE TABLE [dbo].[ShapeGroups]([ID] [int] IDENTITY(1,1) NOT NULL,[ShapeGroup] [varchar](50) COLLATE Latin1_General_CI_AS NOT NULL, CONSTRAINT [PK_ShapeGroups] PRIMARY KEY CLUSTERED ([ID] ASC)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]) ON [PRIMARY];";
        Queries.ExecuteSQLCommand(queryString);
      }
    }

    /// <summary>
    /// This method checks for the table Params in the database
    /// This table is new in Version 2.2 and is now added to older versions.
    /// </summary>
    private void AddParamsTable()
    {
      if (!Queries.TableExists("Params"))
      {
        string queryString = "CREATE TABLE [dbo].[Params]([ID] [int] IDENTITY(1,1) NOT NULL,[Param] [varchar](50) COLLATE Latin1_General_CI_AS NOT NULL, [Description] [varchar](400) COLLATE Latin1_General_CI_AS NULL, CONSTRAINT [PK_Params] PRIMARY KEY CLUSTERED ([ID] ASC)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]) ON [PRIMARY];";
        Queries.ExecuteSQLCommand(queryString);
      }
    }

    /// <summary>
    /// This method checks for the table SubjectParameters in the database
    /// This table is new in Version 1.8 ? and is now added to older versions.
    /// </summary>
    private void AddSubjectParametersTable()
    {
      if (!Queries.TableExists("SubjectParameters"))
      {
        string queryString = "CREATE TABLE [dbo].[SubjectParameters]( [ID] [int] IDENTITY(1,1) NOT NULL, [SubjectName] [varchar](50) COLLATE Latin1_General_CI_AS NOT NULL, [Param] [varchar](50) COLLATE Latin1_General_CI_AS NOT NULL, [ParamValue] [varchar](500) COLLATE Latin1_General_CI_AS NULL, CONSTRAINT [PK_SubjectParameters] PRIMARY KEY CLUSTERED ( [ID] ASC)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]) ON [PRIMARY];";
        Queries.ExecuteSQLCommand(queryString);
      }
    }

    /// <summary>
    /// This method checks for the table TrialEvents in the database
    /// This table is new in Version 2.0 and is now added to older versions.
    /// </summary>
    private void AddTrialEventsTable()
    {
      if (!Queries.TableExists("TrialEvents"))
      {
        string queryString = "CREATE TABLE [dbo].[TrialEvents]( [ID] [int] IDENTITY(1,1) NOT NULL, [SubjectName] [varchar](50) COLLATE Latin1_General_CI_AS NOT NULL, [TrialSequence] [int] NOT NULL, [EventID] [int] NOT NULL, [EventTime] [bigint] NOT NULL, [EventType] [varchar](50) COLLATE Latin1_General_CI_AS NOT NULL, [EventTask] [varchar](50) COLLATE Latin1_General_CI_AS NULL, [EventParam] [varchar](4000) COLLATE Latin1_General_CI_AS NULL, CONSTRAINT [PK_Events] PRIMARY KEY CLUSTERED ( [ID] ASC)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]) ON [PRIMARY]";
        Queries.ExecuteSQLCommand(queryString);
      }
    }

    /// <summary>
    /// This method checks for the table MouseFixations in the database
    /// This table is new in Version 1.X and is now added to older versions.
    /// </summary>
    private void AddMouseFixationsTable()
    {
      if (!Queries.TableExists("MouseFixations"))
      {
        string queryString = "CREATE TABLE [dbo].[MouseFixations]([ID] [bigint] IDENTITY(1,1) NOT NULL,[SubjectName] [varchar](50) COLLATE Latin1_General_CI_AS NOT NULL,[TrialID] [int] NOT NULL,[TrialSequence] [int] NOT NULL,[CountInTrial] [int] NULL,[StartTime] [bigint] NULL,[Length] [int] NULL,[PosX] [float] NULL,[PosY] [float] NULL,CONSTRAINT [PK_TableMouseFixations] PRIMARY KEY CLUSTERED ( [ID] ASC)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]) ON [PRIMARY]";
        Queries.ExecuteSQLCommand(queryString);
      }
    }

    /// <summary>
    /// This method checks for the table GazeFixations in the database
    /// This table is new in Version 1.X and is now added to older versions.
    /// </summary>
    private void AddGazeFixationsTable()
    {
      if (!Queries.TableExists("GazeFixations"))
      {
        string queryString = "CREATE TABLE [dbo].[GazeFixations]([ID] [bigint] IDENTITY(1,1) NOT NULL,[SubjectName] [varchar](50) COLLATE Latin1_General_CI_AS NOT NULL,[TrialID] [int] NOT NULL,[TrialSequence] [int] NOT NULL,[CountInTrial] [int] NULL,[StartTime] [bigint] NULL,[Length] [int] NULL,[PosX] [float] NULL,[PosY] [float] NULL,CONSTRAINT [PK_TableGazeFixations] PRIMARY KEY CLUSTERED ( [ID] ASC )WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]) ON [PRIMARY]";
        Queries.ExecuteSQLCommand(queryString);
      }
    }

    #endregion //METHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER
    #endregion //HELPER
  }
}
