// <copyright file="SQLiteOgamaDataSet.cs" company="FU Berlin">
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

namespace Ogama.DataSet
{
  using System;
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.Data;
  using System.Data.SqlClient;
  using System.Data.SQLite;
  using System.Drawing;
  using System.IO;
  using System.Windows.Forms;

  using Ogama.DataSet.SQLiteOgamaDataSetTableAdapters;
  using Ogama.ExceptionHandling;
  using Ogama.MainWindow.Dialogs;
  using Ogama.Modules.Common.SlideCollections;
  using Ogama.Modules.Common.Tools;
  using Ogama.Modules.Common.TrialEvents;

  using VectorGraphics.Elements;
  using VectorGraphics.Elements.ElementCollections;
  using VectorGraphics.StopConditions;

  /// <summary>
  /// Stores and creates SQL connection queries and handling.
  /// </summary>
  public class SQLiteOgamaDataSet : DataSet
  {
    /// <summary>
    /// The table subject parameters
    /// </summary>
    private SubjectParametersDataTable tableSubjectParameters;

    /// <summary>
    /// The table subjects
    /// </summary>
    private SubjectsDataTable tableSubjects;

    /// <summary>
    /// The table trials
    /// </summary>
    private TrialsDataTable tableTrials;

    /// <summary>
    /// The table trial events
    /// </summary>
    private TrialEventsDataTable tableTrialEvents;

    /// <summary>
    /// The table rawdata
    /// </summary>
    private RawdataDataTable tableRawdata;

    /// <summary>
    /// The table ao is
    /// </summary>
    private AOIsDataTable tableAOIs;

    /// <summary>
    /// The table shape groups
    /// </summary>
    private ShapeGroupsDataTable tableShapeGroups;

    /// <summary>
    /// The table gaze fixations
    /// </summary>
    private GazeFixationsDataTable tableGazeFixations;

    /// <summary>
    /// The table mouse fixations
    /// </summary>
    private MouseFixationsDataTable tableMouseFixations;

    /// <summary>
    /// The table parameters
    /// </summary>
    private ParamsDataTable tableParams;

    /// <summary>
    /// The relation f k_ subjects_ subject parameters
    /// </summary>
    private DataRelation relationFK_Subjects_SubjectParameters;

    /// <summary>
    /// The relation f k_ subjects_ trials
    /// </summary>
    private DataRelation relationFK_Subjects_Trials;

    /// <summary>
    /// The relation f k_ trials_ events
    /// </summary>
    private DataRelation relationFK_Trials_Events;

    /// <summary>
    /// The relation trials_ ao is
    /// </summary>
    private DataRelation relationTrials_AOIs;

    /// <summary>
    /// The relation shape groups_ ao is
    /// </summary>
    private DataRelation relationShapeGroups_AOIs;

    /// <summary>
    /// The relation trials_ gaze fixations
    /// </summary>
    private DataRelation relationTrials_GazeFixations;

    /// <summary>
    /// The relation trials_ mouse fixations
    /// </summary>
    private DataRelation relationTrials_MouseFixations;

    /// <summary>
    /// The _schema serialization mode
    /// </summary>
    private SchemaSerializationMode _schemaSerializationMode = SchemaSerializationMode.IncludeSchema;

    /// <summary>
    /// Initializes a new instance of the <see cref="SQLiteOgamaDataSet"/> class.
    /// </summary>
    public SQLiteOgamaDataSet()
    {
      this.BeginInit();
      this.InitClass();
      global::System.ComponentModel.CollectionChangeEventHandler schemaChangedHandler = new global::System.ComponentModel.CollectionChangeEventHandler(this.SchemaChanged);
      base.Tables.CollectionChanged += schemaChangedHandler;
      base.Relations.CollectionChanged += schemaChangedHandler;
      this.EndInit();
    }

    /// <summary>
    /// Initializes a new instance of a <see cref="T:System.Data.DataSet" /> class that has the given serialization information and context.
    /// </summary>
    /// <param name="info">The data needed to serialize or deserialize an object.</param>
    /// <param name="context">The source and destination of a given serialized stream.</param>
    protected SQLiteOgamaDataSet(global::System.Runtime.Serialization.SerializationInfo info, global::System.Runtime.Serialization.StreamingContext context) :
      base(info, context, false)
    {
      if ((this.IsBinarySerialized(info, context) == true))
      {
        this.InitVars(false);
        global::System.ComponentModel.CollectionChangeEventHandler schemaChangedHandler1 = new global::System.ComponentModel.CollectionChangeEventHandler(this.SchemaChanged);
        this.Tables.CollectionChanged += schemaChangedHandler1;
        this.Relations.CollectionChanged += schemaChangedHandler1;
        return;
      }
      string strSchema = ((string)(info.GetValue("XmlSchema", typeof(string))));
      if ((this.DetermineSchemaSerializationMode(info, context) == SchemaSerializationMode.IncludeSchema))
      {
        DataSet ds = new DataSet();
        ds.ReadXmlSchema(new global::System.Xml.XmlTextReader(new global::System.IO.StringReader(strSchema)));
        if ((ds.Tables["SubjectParameters"] != null))
        {
          base.Tables.Add(new SubjectParametersDataTable(ds.Tables["SubjectParameters"]));
        }
        if ((ds.Tables["Subjects"] != null))
        {
          base.Tables.Add(new SubjectsDataTable(ds.Tables["Subjects"]));
        }
        if ((ds.Tables["Trials"] != null))
        {
          base.Tables.Add(new TrialsDataTable(ds.Tables["Trials"]));
        }
        if ((ds.Tables["TrialEvents"] != null))
        {
          base.Tables.Add(new TrialEventsDataTable(ds.Tables["TrialEvents"]));
        }
        if ((ds.Tables["Rawdata"] != null))
        {
          base.Tables.Add(new RawdataDataTable(ds.Tables["Rawdata"]));
        }
        if ((ds.Tables["AOIs"] != null))
        {
          base.Tables.Add(new AOIsDataTable(ds.Tables["AOIs"]));
        }
        if ((ds.Tables["ShapeGroups"] != null))
        {
          base.Tables.Add(new ShapeGroupsDataTable(ds.Tables["ShapeGroups"]));
        }
        if ((ds.Tables["GazeFixations"] != null))
        {
          base.Tables.Add(new GazeFixationsDataTable(ds.Tables["GazeFixations"]));
        }
        if ((ds.Tables["MouseFixations"] != null))
        {
          base.Tables.Add(new MouseFixationsDataTable(ds.Tables["MouseFixations"]));
        }
        if ((ds.Tables["Params"] != null))
        {
          base.Tables.Add(new ParamsDataTable(ds.Tables["Params"]));
        }
        this.DataSetName = ds.DataSetName;
        this.Prefix = ds.Prefix;
        this.Namespace = ds.Namespace;
        this.Locale = ds.Locale;
        this.CaseSensitive = ds.CaseSensitive;
        this.EnforceConstraints = ds.EnforceConstraints;
        this.Merge(ds, false, MissingSchemaAction.Add);
        this.InitVars();
      }
      else
      {
        this.ReadXmlSchema(new global::System.Xml.XmlTextReader(new global::System.IO.StringReader(strSchema)));
      }
      this.GetSerializationData(info, context);
      global::System.ComponentModel.CollectionChangeEventHandler schemaChangedHandler = new global::System.ComponentModel.CollectionChangeEventHandler(this.SchemaChanged);
      base.Tables.CollectionChanged += schemaChangedHandler;
      this.Relations.CollectionChanged += schemaChangedHandler;
    }

    /// <summary>
    /// Gets the subject parameters.
    /// </summary>
    /// <value>The subject parameters.</value>
    public SubjectParametersDataTable SubjectParameters
    {
      get
      {
        return this.tableSubjectParameters;
      }
    }

    /// <summary>
    /// Gets the subjects.
    /// </summary>
    /// <value>The subjects.</value>
    public SubjectsDataTable Subjects
    {
      get
      {
        return this.tableSubjects;
      }
    }

    /// <summary>
    /// Gets the trials.
    /// </summary>
    /// <value>The trials.</value>
    public TrialsDataTable Trials
    {
      get
      {
        return this.tableTrials;
      }
    }

    /// <summary>
    /// Gets the trial events.
    /// </summary>
    /// <value>The trial events.</value>
    public TrialEventsDataTable TrialEvents
    {
      get
      {
        return this.tableTrialEvents;
      }
    }

    /// <summary>
    /// Gets the rawdata.
    /// </summary>
    /// <value>The rawdata.</value>
    public RawdataDataTable Rawdata
    {
      get
      {
        return this.tableRawdata;
      }
    }

    /// <summary>
    /// Gets the ao is.
    /// </summary>
    /// <value>The ao is.</value>
    public AOIsDataTable AOIs
    {
      get
      {
        return this.tableAOIs;
      }
    }

    /// <summary>
    /// Gets the shape groups.
    /// </summary>
    /// <value>The shape groups.</value>
    public ShapeGroupsDataTable ShapeGroups
    {
      get
      {
        return this.tableShapeGroups;
      }
    }

    /// <summary>
    /// Gets the gaze fixations.
    /// </summary>
    /// <value>The gaze fixations.</value>
    public GazeFixationsDataTable GazeFixations
    {
      get
      {
        return this.tableGazeFixations;
      }
    }

    /// <summary>
    /// Gets the mouse fixations.
    /// </summary>
    /// <value>The mouse fixations.</value>
    public MouseFixationsDataTable MouseFixations
    {
      get
      {
        return this.tableMouseFixations;
      }
    }

    /// <summary>
    /// Gets the parameters.
    /// </summary>
    /// <value>The parameters.</value>
    public ParamsDataTable Params
    {
      get
      {
        return this.tableParams;
      }
    }

    /// <summary>
    /// Gets or sets a <see cref="T:System.Data.SchemaSerializationMode" /> for a <see cref="T:System.Data.DataSet" />.
    /// </summary>
    /// <value>The schema serialization mode.</value>
    public override SchemaSerializationMode SchemaSerializationMode
    {
      get
      {
        return this._schemaSerializationMode;
      }
      set
      {
        this._schemaSerializationMode = value;
      }
    }

    /// <summary>
    /// Gets the collection of tables contained in the <see cref="T:System.Data.DataSet" />.
    /// </summary>
    /// <value>The tables.</value>
    public new DataTableCollection Tables
    {
      get
      {
        return base.Tables;
      }
    }

    /// <summary>
    /// Get the collection of relations that link tables and allow navigation from parent tables to child tables.
    /// </summary>
    /// <value>The relations.</value>
    public new DataRelationCollection Relations
    {
      get
      {
        return base.Relations;
      }
    }

    /// <summary>
    /// Deserialize all of the tables data of the DataSet from the binary or XML stream.
    /// </summary>
    protected override void InitializeDerivedDataSet()
    {
      this.BeginInit();
      this.InitClass();
      this.EndInit();
    }

    /// <summary>
    /// Copies the structure of the <see cref="T:System.Data.DataSet" />, including all <see cref="T:System.Data.DataTable" /> schemas, relations, and constraints. Does not copy any data.
    /// </summary>
    /// <returns>A new <see cref="T:System.Data.DataSet" /> with the same schema as the current <see cref="T:System.Data.DataSet" />, but none of the data.</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
    /// </PermissionSet>
    public override DataSet Clone()
    {
      SQLiteOgamaDataSet cln = ((SQLiteOgamaDataSet)(base.Clone()));
      cln.InitVars();
      cln.SchemaSerializationMode = this.SchemaSerializationMode;
      return cln;
    }



    /// <summary>
    /// Gets a value indicating whether <see cref="P:System.Data.DataSet.Tables" /> property should be persisted.
    /// </summary>
    /// <returns>true if the property value has been changed from its default; otherwise false.</returns>
    protected override bool ShouldSerializeTables()
    {
      return false;
    }



    /// <summary>
    /// Gets a value indicating whether <see cref="P:System.Data.DataSet.Relations" /> property should be persisted.
    /// </summary>
    /// <returns>true if the property value has been changed from its default; otherwise false.</returns>
    protected override bool ShouldSerializeRelations()
    {
      return false;
    }



    /// <summary>
    /// Ignores attributes and returns an empty DataSet.
    /// </summary>
    /// <param name="reader">The specified XML reader.</param>
    protected override void ReadXmlSerializable(global::System.Xml.XmlReader reader)
    {
      if ((this.DetermineSchemaSerializationMode(reader) == SchemaSerializationMode.IncludeSchema))
      {
        this.Reset();
        DataSet ds = new DataSet();
        ds.ReadXml(reader);
        if ((ds.Tables["SubjectParameters"] != null))
        {
          base.Tables.Add(new SubjectParametersDataTable(ds.Tables["SubjectParameters"]));
        }
        if ((ds.Tables["Subjects"] != null))
        {
          base.Tables.Add(new SubjectsDataTable(ds.Tables["Subjects"]));
        }
        if ((ds.Tables["Trials"] != null))
        {
          base.Tables.Add(new TrialsDataTable(ds.Tables["Trials"]));
        }
        if ((ds.Tables["TrialEvents"] != null))
        {
          base.Tables.Add(new TrialEventsDataTable(ds.Tables["TrialEvents"]));
        }
        if ((ds.Tables["Rawdata"] != null))
        {
          base.Tables.Add(new RawdataDataTable(ds.Tables["Rawdata"]));
        }
        if ((ds.Tables["AOIs"] != null))
        {
          base.Tables.Add(new AOIsDataTable(ds.Tables["AOIs"]));
        }
        if ((ds.Tables["ShapeGroups"] != null))
        {
          base.Tables.Add(new ShapeGroupsDataTable(ds.Tables["ShapeGroups"]));
        }
        if ((ds.Tables["GazeFixations"] != null))
        {
          base.Tables.Add(new GazeFixationsDataTable(ds.Tables["GazeFixations"]));
        }
        if ((ds.Tables["MouseFixations"] != null))
        {
          base.Tables.Add(new MouseFixationsDataTable(ds.Tables["MouseFixations"]));
        }
        if ((ds.Tables["Params"] != null))
        {
          base.Tables.Add(new ParamsDataTable(ds.Tables["Params"]));
        }
        this.DataSetName = ds.DataSetName;
        this.Prefix = ds.Prefix;
        this.Namespace = ds.Namespace;
        this.Locale = ds.Locale;
        this.CaseSensitive = ds.CaseSensitive;
        this.EnforceConstraints = ds.EnforceConstraints;
        this.Merge(ds, false, MissingSchemaAction.Add);
        this.InitVars();
      }
      else
      {
        this.ReadXml(reader);
        this.InitVars();
      }
    }



    /// <summary>
    /// Returns a serializable <see cref="T:System.Xml.Schema.XMLSchema" /> instance.
    /// </summary>
    /// <returns>The <see cref="T:System.Xml.Schema.XMLSchema" /> instance.</returns>
    protected override global::System.Xml.Schema.XmlSchema GetSchemaSerializable()
    {
      global::System.IO.MemoryStream stream = new global::System.IO.MemoryStream();
      this.WriteXmlSchema(new global::System.Xml.XmlTextWriter(stream, null));
      stream.Position = 0;
      return global::System.Xml.Schema.XmlSchema.Read(new global::System.Xml.XmlTextReader(stream), null);
    }



    /// <summary>
    /// Initializes the vars.
    /// </summary>
    internal void InitVars()
    {
      this.InitVars(true);
    }



    /// <summary>
    /// Initializes the vars.
    /// </summary>
    /// <param name="initTable">if set to <c>true</c> [initialize table].</param>
    internal void InitVars(bool initTable)
    {
      this.tableSubjectParameters = ((SubjectParametersDataTable)(base.Tables["SubjectParameters"]));
      if ((initTable == true))
      {
        if ((this.tableSubjectParameters != null))
        {
          this.tableSubjectParameters.InitVars();
        }
      }
      this.tableSubjects = ((SubjectsDataTable)(base.Tables["Subjects"]));
      if ((initTable == true))
      {
        if ((this.tableSubjects != null))
        {
          this.tableSubjects.InitVars();
        }
      }
      this.tableTrials = ((TrialsDataTable)(base.Tables["Trials"]));
      if ((initTable == true))
      {
        if ((this.tableTrials != null))
        {
          this.tableTrials.InitVars();
        }
      }
      this.tableTrialEvents = ((TrialEventsDataTable)(base.Tables["TrialEvents"]));
      if ((initTable == true))
      {
        if ((this.tableTrialEvents != null))
        {
          this.tableTrialEvents.InitVars();
        }
      }
      this.tableRawdata = ((RawdataDataTable)(base.Tables["Rawdata"]));
      if ((initTable == true))
      {
        if ((this.tableRawdata != null))
        {
          this.tableRawdata.InitVars();
        }
      }
      this.tableAOIs = ((AOIsDataTable)(base.Tables["AOIs"]));
      if ((initTable == true))
      {
        if ((this.tableAOIs != null))
        {
          this.tableAOIs.InitVars();
        }
      }
      this.tableShapeGroups = ((ShapeGroupsDataTable)(base.Tables["ShapeGroups"]));
      if ((initTable == true))
      {
        if ((this.tableShapeGroups != null))
        {
          this.tableShapeGroups.InitVars();
        }
      }
      this.tableGazeFixations = ((GazeFixationsDataTable)(base.Tables["GazeFixations"]));
      if ((initTable == true))
      {
        if ((this.tableGazeFixations != null))
        {
          this.tableGazeFixations.InitVars();
        }
      }
      this.tableMouseFixations = ((MouseFixationsDataTable)(base.Tables["MouseFixations"]));
      if ((initTable == true))
      {
        if ((this.tableMouseFixations != null))
        {
          this.tableMouseFixations.InitVars();
        }
      }
      this.tableParams = ((ParamsDataTable)(base.Tables["Params"]));
      if ((initTable == true))
      {
        if ((this.tableParams != null))
        {
          this.tableParams.InitVars();
        }
      }
      this.relationFK_Subjects_SubjectParameters = this.Relations["FK_Subjects_SubjectParameters"];
      this.relationFK_Subjects_Trials = this.Relations["FK_Subjects_Trials"];
      this.relationFK_Trials_Events = this.Relations["FK_Trials_Events"];
      this.relationTrials_AOIs = this.Relations["Trials_AOIs"];
      this.relationShapeGroups_AOIs = this.Relations["ShapeGroups_AOIs"];
      this.relationTrials_GazeFixations = this.Relations["Trials_GazeFixations"];
      this.relationTrials_MouseFixations = this.Relations["Trials_MouseFixations"];
    }



    /// <summary>
    /// Initializes the class.
    /// </summary>
    private void InitClass()
    {
      this.DataSetName = "SQLiteOgamaDataSet";
      this.Prefix = "";
      this.Namespace = "http://tempuri.org/SQLiteOgamaDataSet.xsd";
      this.EnforceConstraints = true;
      this.SchemaSerializationMode = SchemaSerializationMode.IncludeSchema;
      this.tableSubjectParameters = new SubjectParametersDataTable();
      base.Tables.Add(this.tableSubjectParameters);
      this.tableSubjects = new SubjectsDataTable();
      base.Tables.Add(this.tableSubjects);
      this.tableTrials = new TrialsDataTable();
      base.Tables.Add(this.tableTrials);
      this.tableTrialEvents = new TrialEventsDataTable();
      base.Tables.Add(this.tableTrialEvents);
      this.tableRawdata = new RawdataDataTable();
      base.Tables.Add(this.tableRawdata);
      this.tableAOIs = new AOIsDataTable();
      base.Tables.Add(this.tableAOIs);
      this.tableShapeGroups = new ShapeGroupsDataTable();
      base.Tables.Add(this.tableShapeGroups);
      this.tableGazeFixations = new GazeFixationsDataTable();
      base.Tables.Add(this.tableGazeFixations);
      this.tableMouseFixations = new MouseFixationsDataTable();
      base.Tables.Add(this.tableMouseFixations);
      this.tableParams = new ParamsDataTable();
      base.Tables.Add(this.tableParams);
      ForeignKeyConstraint fkc;
      fkc = new ForeignKeyConstraint("Params_SubjectParameters", new DataColumn[] {
                        this.tableParams.ParamColumn}, new DataColumn[] {
                        this.tableSubjectParameters.ParamColumn});
      this.tableSubjectParameters.Constraints.Add(fkc);
      fkc.AcceptRejectRule = AcceptRejectRule.Cascade;
      fkc.DeleteRule = Rule.Cascade;
      fkc.UpdateRule = Rule.Cascade;
      fkc = new ForeignKeyConstraint("FK_Subjects_SubjectParameters", new DataColumn[] {
                        this.tableSubjects.SubjectNameColumn}, new DataColumn[] {
                        this.tableSubjectParameters.SubjectNameColumn});
      this.tableSubjectParameters.Constraints.Add(fkc);
      fkc.AcceptRejectRule = AcceptRejectRule.None;
      fkc.DeleteRule = Rule.Cascade;
      fkc.UpdateRule = Rule.Cascade;
      fkc = new ForeignKeyConstraint("FK_Subjects_Trials", new DataColumn[] {
                        this.tableSubjects.SubjectNameColumn}, new DataColumn[] {
                        this.tableTrials.SubjectNameColumn});
      this.tableTrials.Constraints.Add(fkc);
      fkc.AcceptRejectRule = AcceptRejectRule.Cascade;
      fkc.DeleteRule = Rule.Cascade;
      fkc.UpdateRule = Rule.Cascade;
      this.relationFK_Subjects_SubjectParameters = new DataRelation("FK_Subjects_SubjectParameters", new DataColumn[] {
                        this.tableSubjects.SubjectNameColumn}, new DataColumn[] {
                        this.tableSubjectParameters.SubjectNameColumn}, false);
      this.Relations.Add(this.relationFK_Subjects_SubjectParameters);
      this.relationFK_Subjects_Trials = new DataRelation("FK_Subjects_Trials", new DataColumn[] {
                        this.tableSubjects.SubjectNameColumn}, new DataColumn[] {
                        this.tableTrials.SubjectNameColumn}, false);
      this.Relations.Add(this.relationFK_Subjects_Trials);
      this.relationFK_Trials_Events = new DataRelation("FK_Trials_Events", new DataColumn[] {
                        this.tableTrials.SubjectNameColumn,
                        this.tableTrials.TrialSequenceColumn}, new DataColumn[] {
                        this.tableTrialEvents.SubjectNameColumn,
                        this.tableTrialEvents.TrialSequenceColumn}, false);
      this.Relations.Add(this.relationFK_Trials_Events);
      this.relationTrials_AOIs = new DataRelation("Trials_AOIs", new DataColumn[] {
                        this.tableTrials.TrialIDColumn}, new DataColumn[] {
                        this.tableAOIs.TrialIDColumn}, false);
      this.Relations.Add(this.relationTrials_AOIs);
      this.relationShapeGroups_AOIs = new DataRelation("ShapeGroups_AOIs", new DataColumn[] {
                        this.tableShapeGroups.ShapeGroupColumn}, new DataColumn[] {
                        this.tableAOIs.ShapeGroupColumn}, false);
      this.Relations.Add(this.relationShapeGroups_AOIs);
      this.relationTrials_GazeFixations = new DataRelation("Trials_GazeFixations", new DataColumn[] {
                        this.tableTrials.SubjectNameColumn,
                        this.tableTrials.TrialSequenceColumn}, new DataColumn[] {
                        this.tableGazeFixations.SubjectNameColumn,
                        this.tableGazeFixations.TrialSequenceColumn}, false);
      this.Relations.Add(this.relationTrials_GazeFixations);
      this.relationTrials_MouseFixations = new DataRelation("Trials_MouseFixations", new DataColumn[] {
                        this.tableTrials.SubjectNameColumn,
                        this.tableTrials.TrialSequenceColumn}, new DataColumn[] {
                        this.tableMouseFixations.SubjectNameColumn,
                        this.tableMouseFixations.TrialSequenceColumn}, false);
      this.Relations.Add(this.relationTrials_MouseFixations);
    }



    /// <summary>
    /// Shoulds the serialize subject parameters.
    /// </summary>
    /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
    private bool ShouldSerializeSubjectParameters()
    {
      return false;
    }



    /// <summary>
    /// Shoulds the serialize subjects.
    /// </summary>
    /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
    private bool ShouldSerializeSubjects()
    {
      return false;
    }



    /// <summary>
    /// Shoulds the serialize trials.
    /// </summary>
    /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
    private bool ShouldSerializeTrials()
    {
      return false;
    }



    /// <summary>
    /// Shoulds the serialize trial events.
    /// </summary>
    /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
    private bool ShouldSerializeTrialEvents()
    {
      return false;
    }



    /// <summary>
    /// Shoulds the serialize rawdata.
    /// </summary>
    /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
    private bool ShouldSerializeRawdata()
    {
      return false;
    }



    /// <summary>
    /// Shoulds the serialize ao is.
    /// </summary>
    /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
    private bool ShouldSerializeAOIs()
    {
      return false;
    }



    /// <summary>
    /// Shoulds the serialize shape groups.
    /// </summary>
    /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
    private bool ShouldSerializeShapeGroups()
    {
      return false;
    }



    /// <summary>
    /// Shoulds the serialize gaze fixations.
    /// </summary>
    /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
    private bool ShouldSerializeGazeFixations()
    {
      return false;
    }



    /// <summary>
    /// Shoulds the serialize mouse fixations.
    /// </summary>
    /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
    private bool ShouldSerializeMouseFixations()
    {
      return false;
    }



    /// <summary>
    /// Shoulds the serialize parameters.
    /// </summary>
    /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
    private bool ShouldSerializeParams()
    {
      return false;
    }



    /// <summary>
    /// Schemas the changed.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="System.ComponentModel.CollectionChangeEventArgs"/> instance containing the event data.</param>
    private void SchemaChanged(object sender, global::System.ComponentModel.CollectionChangeEventArgs e)
    {
      if ((e.Action == global::System.ComponentModel.CollectionChangeAction.Remove))
      {
        this.InitVars();
      }
    }



    /// <summary>
    /// Gets the typed data set schema.
    /// </summary>
    /// <param name="xs">The xs.</param>
    /// <returns>System.Xml.Schema.XmlSchemaComplexType.</returns>
    public static global::System.Xml.Schema.XmlSchemaComplexType GetTypedDataSetSchema(global::System.Xml.Schema.XmlSchemaSet xs)
    {
      SQLiteOgamaDataSet ds = new SQLiteOgamaDataSet();
      global::System.Xml.Schema.XmlSchemaComplexType type = new global::System.Xml.Schema.XmlSchemaComplexType();
      global::System.Xml.Schema.XmlSchemaSequence sequence = new global::System.Xml.Schema.XmlSchemaSequence();
      global::System.Xml.Schema.XmlSchemaAny any = new global::System.Xml.Schema.XmlSchemaAny();
      any.Namespace = ds.Namespace;
      sequence.Items.Add(any);
      type.Particle = sequence;
      global::System.Xml.Schema.XmlSchema dsSchema = ds.GetSchemaSerializable();
      if (xs.Contains(dsSchema.TargetNamespace))
      {
        global::System.IO.MemoryStream s1 = new global::System.IO.MemoryStream();
        global::System.IO.MemoryStream s2 = new global::System.IO.MemoryStream();
        try
        {
          global::System.Xml.Schema.XmlSchema schema = null;
          dsSchema.Write(s1);
          for (global::System.Collections.IEnumerator schemas = xs.Schemas(dsSchema.TargetNamespace).GetEnumerator(); schemas.MoveNext(); )
          {
            schema = ((global::System.Xml.Schema.XmlSchema)(schemas.Current));
            s2.SetLength(0);
            schema.Write(s2);
            if ((s1.Length == s2.Length))
            {
              s1.Position = 0;
              s2.Position = 0;
              for (; ((s1.Position != s1.Length)
                          && (s1.ReadByte() == s2.ReadByte())); )
              {
                ;
              }
              if ((s1.Position == s1.Length))
              {
                return type;
              }
            }
          }
        }
        finally
        {
          if ((s1 != null))
          {
            s1.Close();
          }
          if ((s2 != null))
          {
            s2.Close();
          }
        }
      }
      xs.Add(dsSchema);
      return type;
    }


    /// <summary>
    /// Delegate SubjectParametersRowChangeEventHandler
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The e.</param>
    public delegate void SubjectParametersRowChangeEventHandler(object sender, SubjectParametersRowChangeEvent e);


    /// <summary>
    /// Delegate SubjectsRowChangeEventHandler
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The e.</param>
    public delegate void SubjectsRowChangeEventHandler(object sender, SubjectsRowChangeEvent e);


    /// <summary>
    /// Delegate TrialsRowChangeEventHandler
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The e.</param>
    public delegate void TrialsRowChangeEventHandler(object sender, TrialsRowChangeEvent e);


    /// <summary>
    /// Delegate TrialEventsRowChangeEventHandler
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The e.</param>
    public delegate void TrialEventsRowChangeEventHandler(object sender, TrialEventsRowChangeEvent e);


    /// <summary>
    /// Delegate RawdataRowChangeEventHandler
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The e.</param>
    public delegate void RawdataRowChangeEventHandler(object sender, RawdataRowChangeEvent e);


    /// <summary>
    /// Delegate AOIsRowChangeEventHandler
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The e.</param>
    public delegate void AOIsRowChangeEventHandler(object sender, AOIsRowChangeEvent e);


    /// <summary>
    /// Delegate ShapeGroupsRowChangeEventHandler
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The e.</param>
    public delegate void ShapeGroupsRowChangeEventHandler(object sender, ShapeGroupsRowChangeEvent e);


    /// <summary>
    /// Delegate GazeFixationsRowChangeEventHandler
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The e.</param>
    public delegate void GazeFixationsRowChangeEventHandler(object sender, GazeFixationsRowChangeEvent e);


    /// <summary>
    /// Delegate MouseFixationsRowChangeEventHandler
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The e.</param>
    public delegate void MouseFixationsRowChangeEventHandler(object sender, MouseFixationsRowChangeEvent e);


    /// <summary>
    /// Delegate ParamsRowChangeEventHandler
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The e.</param>
    public delegate void ParamsRowChangeEventHandler(object sender, ParamsRowChangeEvent e);

    /// <summary>
    /// Represents the strongly named DataTable class.
    /// </summary>
    [global::System.Serializable()]
    [global::System.Xml.Serialization.XmlSchemaProviderAttribute("GetTypedTableSchema")]
    public partial class SubjectParametersDataTable : TypedTableBase<SubjectParametersRow>
    {

      /// <summary>
      /// The column identifier
      /// </summary>
      private DataColumn columnID;

      /// <summary>
      /// The column subject name
      /// </summary>
      private DataColumn columnSubjectName;

      /// <summary>
      /// The column parameter
      /// </summary>
      private DataColumn columnParam;

      /// <summary>
      /// The column parameter value
      /// </summary>
      private DataColumn columnParamValue;



      /// <summary>
      /// Initializes a new instance of the <see cref="SubjectParametersDataTable"/> class.
      /// </summary>
      public SubjectParametersDataTable()
      {
        this.TableName = "SubjectParameters";
        this.BeginInit();
        this.InitClass();
        this.EndInit();
      }



      /// <summary>
      /// Initializes a new instance of the <see cref="SubjectParametersDataTable"/> class.
      /// </summary>
      /// <param name="table">The table.</param>
      internal SubjectParametersDataTable(DataTable table)
      {
        this.TableName = table.TableName;
        if ((table.CaseSensitive != table.DataSet.CaseSensitive))
        {
          this.CaseSensitive = table.CaseSensitive;
        }
        if ((table.Locale.ToString() != table.DataSet.Locale.ToString()))
        {
          this.Locale = table.Locale;
        }
        if ((table.Namespace != table.DataSet.Namespace))
        {
          this.Namespace = table.Namespace;
        }
        this.Prefix = table.Prefix;
        this.MinimumCapacity = table.MinimumCapacity;
      }



      /// <summary>
      /// Initializes a new instance of the <see cref="SubjectParametersDataTable"/> class.
      /// </summary>
      /// <param name="info">The information.</param>
      /// <param name="context">The context.</param>
      protected SubjectParametersDataTable(global::System.Runtime.Serialization.SerializationInfo info, global::System.Runtime.Serialization.StreamingContext context) :
        base(info, context)
      {
        this.InitVars();
      }



      /// <summary>
      /// Gets the identifier column.
      /// </summary>
      /// <value>The identifier column.</value>
      public DataColumn IDColumn
      {
        get
        {
          return this.columnID;
        }
      }



      /// <summary>
      /// Gets the subject name column.
      /// </summary>
      /// <value>The subject name column.</value>
      public DataColumn SubjectNameColumn
      {
        get
        {
          return this.columnSubjectName;
        }
      }



      /// <summary>
      /// Gets the parameter column.
      /// </summary>
      /// <value>The parameter column.</value>
      public DataColumn ParamColumn
      {
        get
        {
          return this.columnParam;
        }
      }



      /// <summary>
      /// Gets the parameter value column.
      /// </summary>
      /// <value>The parameter value column.</value>
      public DataColumn ParamValueColumn
      {
        get
        {
          return this.columnParamValue;
        }
      }




      /// <summary>
      /// Gets the count.
      /// </summary>
      /// <value>The count.</value>
      public int Count
      {
        get
        {
          return this.Rows.Count;
        }
      }



      /// <summary>
      /// Gets the <see cref="SubjectParametersRow"/> at the specified index.
      /// </summary>
      /// <param name="index">The index.</param>
      /// <returns>SubjectParametersRow.</returns>
      public SubjectParametersRow this[int index]
      {
        get
        {
          return ((SubjectParametersRow)(this.Rows[index]));
        }
      }


      /// <summary>
      /// Occurs when [subject parameters row changing].
      /// </summary>
      public event SubjectParametersRowChangeEventHandler SubjectParametersRowChanging;


      /// <summary>
      /// Occurs when [subject parameters row changed].
      /// </summary>
      public event SubjectParametersRowChangeEventHandler SubjectParametersRowChanged;


      /// <summary>
      /// Occurs when [subject parameters row deleting].
      /// </summary>
      public event SubjectParametersRowChangeEventHandler SubjectParametersRowDeleting;


      /// <summary>
      /// Occurs when [subject parameters row deleted].
      /// </summary>
      public event SubjectParametersRowChangeEventHandler SubjectParametersRowDeleted;



      /// <summary>
      /// Adds the subject parameters row.
      /// </summary>
      /// <param name="row">The row.</param>
      public void AddSubjectParametersRow(SubjectParametersRow row)
      {
        this.Rows.Add(row);
      }



      /// <summary>
      /// Adds the subject parameters row.
      /// </summary>
      /// <param name="parentSubjectsRowByFK_Subjects_SubjectParameters">The parent subjects row by f k_ subjects_ subject parameters.</param>
      /// <param name="Param">The parameter.</param>
      /// <param name="ParamValue">The parameter value.</param>
      /// <returns>SubjectParametersRow.</returns>
      public SubjectParametersRow AddSubjectParametersRow(SubjectsRow parentSubjectsRowByFK_Subjects_SubjectParameters, string Param, string ParamValue)
      {
        SubjectParametersRow rowSubjectParametersRow = ((SubjectParametersRow)(this.NewRow()));
        object[] columnValuesArray = new object[] {
                        null,
                        null,
                        Param,
                        ParamValue};
        if ((parentSubjectsRowByFK_Subjects_SubjectParameters != null))
        {
          columnValuesArray[1] = parentSubjectsRowByFK_Subjects_SubjectParameters[1];
        }
        rowSubjectParametersRow.ItemArray = columnValuesArray;
        this.Rows.Add(rowSubjectParametersRow);
        return rowSubjectParametersRow;
      }



      /// <summary>
      /// Finds the by identifier.
      /// </summary>
      /// <param name="ID">The identifier.</param>
      /// <returns>SubjectParametersRow.</returns>
      public SubjectParametersRow FindByID(int ID)
      {
        return ((SubjectParametersRow)(this.Rows.Find(new object[] {
                            ID})));
      }



      /// <summary>
      /// Clones the structure of the <see cref="T:System.Data.DataTable" />, including all <see cref="T:System.Data.DataTable" /> schemas and constraints.
      /// </summary>
      /// <returns>A new <see cref="T:System.Data.DataTable" /> with the same schema as the current <see cref="T:System.Data.DataTable" />.</returns>
      /// <PermissionSet>
      ///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
      ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
      ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
      /// </PermissionSet>
      public override DataTable Clone()
      {
        SubjectParametersDataTable cln = ((SubjectParametersDataTable)(base.Clone()));
        cln.InitVars();
        return cln;
      }



      /// <summary>
      /// Creates a new instance of <see cref="T:System.Data.DataTable" />.
      /// </summary>
      /// <returns>The new expression.</returns>
      protected override DataTable CreateInstance()
      {
        return new SubjectParametersDataTable();
      }



      /// <summary>
      /// Initializes the vars.
      /// </summary>
      internal void InitVars()
      {
        this.columnID = base.Columns["ID"];
        this.columnSubjectName = base.Columns["SubjectName"];
        this.columnParam = base.Columns["Param"];
        this.columnParamValue = base.Columns["ParamValue"];
      }



      /// <summary>
      /// Initializes the class.
      /// </summary>
      private void InitClass()
      {
        this.columnID = new DataColumn("ID", typeof(int), null, MappingType.Element);
        base.Columns.Add(this.columnID);
        this.columnSubjectName = new DataColumn("SubjectName", typeof(string), null, MappingType.Element);
        base.Columns.Add(this.columnSubjectName);
        this.columnParam = new DataColumn("Param", typeof(string), null, MappingType.Element);
        base.Columns.Add(this.columnParam);
        this.columnParamValue = new DataColumn("ParamValue", typeof(string), null, MappingType.Element);
        base.Columns.Add(this.columnParamValue);
        this.Constraints.Add(new UniqueConstraint("Constraint1", new DataColumn[] {
                                this.columnID}, true));
        this.columnID.AutoIncrement = true;
        this.columnID.AutoIncrementSeed = 1;
        this.columnID.AllowDBNull = false;
        this.columnID.ReadOnly = true;
        this.columnID.Unique = true;
        this.columnSubjectName.AllowDBNull = false;
        this.columnSubjectName.MaxLength = 50;
        this.columnParam.AllowDBNull = false;
        this.columnParam.MaxLength = 50;
        this.columnParamValue.MaxLength = 500;
      }



      /// <summary>
      /// News the subject parameters row.
      /// </summary>
      /// <returns>SubjectParametersRow.</returns>
      public SubjectParametersRow NewSubjectParametersRow()
      {
        return ((SubjectParametersRow)(this.NewRow()));
      }



      /// <summary>
      /// Creates a new row from an existing row.
      /// </summary>
      /// <param name="builder">A <see cref="T:System.Data.DataRowBuilder" /> object.</param>
      /// <returns>A <see cref="T:System.Data.DataRow" /> derived class.</returns>
      protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
      {
        return new SubjectParametersRow(builder);
      }



      /// <summary>
      /// Gets the row type.
      /// </summary>
      /// <returns>Returns the type of the <see cref="T:System.Data.DataRow" />.</returns>
      protected override global::System.Type GetRowType()
      {
        return typeof(SubjectParametersRow);
      }



      /// <summary>
      /// Raises the <see cref="E:System.Data.DataTable.RowChanged" /> event.
      /// </summary>
      /// <param name="e">A <see cref="T:System.Data.DataRowChangeEventArgs" /> that contains the event data.</param>
      protected override void OnRowChanged(DataRowChangeEventArgs e)
      {
        base.OnRowChanged(e);
        if ((this.SubjectParametersRowChanged != null))
        {
          this.SubjectParametersRowChanged(this, new SubjectParametersRowChangeEvent(((SubjectParametersRow)(e.Row)), e.Action));
        }
      }



      /// <summary>
      /// Raises the <see cref="E:System.Data.DataTable.RowChanging" /> event.
      /// </summary>
      /// <param name="e">A <see cref="T:System.Data.DataRowChangeEventArgs" /> that contains the event data.</param>
      protected override void OnRowChanging(DataRowChangeEventArgs e)
      {
        base.OnRowChanging(e);
        if ((this.SubjectParametersRowChanging != null))
        {
          this.SubjectParametersRowChanging(this, new SubjectParametersRowChangeEvent(((SubjectParametersRow)(e.Row)), e.Action));
        }
      }



      /// <summary>
      /// Raises the <see cref="E:System.Data.DataTable.RowDeleted" /> event.
      /// </summary>
      /// <param name="e">A <see cref="T:System.Data.DataRowChangeEventArgs" /> that contains the event data.</param>
      protected override void OnRowDeleted(DataRowChangeEventArgs e)
      {
        base.OnRowDeleted(e);
        if ((this.SubjectParametersRowDeleted != null))
        {
          this.SubjectParametersRowDeleted(this, new SubjectParametersRowChangeEvent(((SubjectParametersRow)(e.Row)), e.Action));
        }
      }



      /// <summary>
      /// Raises the <see cref="E:System.Data.DataTable.RowDeleting" /> event.
      /// </summary>
      /// <param name="e">A <see cref="T:System.Data.DataRowChangeEventArgs" /> that contains the event data.</param>
      protected override void OnRowDeleting(DataRowChangeEventArgs e)
      {
        base.OnRowDeleting(e);
        if ((this.SubjectParametersRowDeleting != null))
        {
          this.SubjectParametersRowDeleting(this, new SubjectParametersRowChangeEvent(((SubjectParametersRow)(e.Row)), e.Action));
        }
      }



      /// <summary>
      /// Removes the subject parameters row.
      /// </summary>
      /// <param name="row">The row.</param>
      public void RemoveSubjectParametersRow(SubjectParametersRow row)
      {
        this.Rows.Remove(row);
      }



      /// <summary>
      /// Gets the typed table schema.
      /// </summary>
      /// <param name="xs">The xs.</param>
      /// <returns>System.Xml.Schema.XmlSchemaComplexType.</returns>
      public static global::System.Xml.Schema.XmlSchemaComplexType GetTypedTableSchema(global::System.Xml.Schema.XmlSchemaSet xs)
      {
        global::System.Xml.Schema.XmlSchemaComplexType type = new global::System.Xml.Schema.XmlSchemaComplexType();
        global::System.Xml.Schema.XmlSchemaSequence sequence = new global::System.Xml.Schema.XmlSchemaSequence();
        SQLiteOgamaDataSet ds = new SQLiteOgamaDataSet();
        global::System.Xml.Schema.XmlSchemaAny any1 = new global::System.Xml.Schema.XmlSchemaAny();
        any1.Namespace = "http://www.w3.org/2001/XMLSchema";
        any1.MinOccurs = new decimal(0);
        any1.MaxOccurs = decimal.MaxValue;
        any1.ProcessContents = global::System.Xml.Schema.XmlSchemaContentProcessing.Lax;
        sequence.Items.Add(any1);
        global::System.Xml.Schema.XmlSchemaAny any2 = new global::System.Xml.Schema.XmlSchemaAny();
        any2.Namespace = "urn:schemas-microsoft-com:xml-diffgram-v1";
        any2.MinOccurs = new decimal(1);
        any2.ProcessContents = global::System.Xml.Schema.XmlSchemaContentProcessing.Lax;
        sequence.Items.Add(any2);
        global::System.Xml.Schema.XmlSchemaAttribute attribute1 = new global::System.Xml.Schema.XmlSchemaAttribute();
        attribute1.Name = "namespace";
        attribute1.FixedValue = ds.Namespace;
        type.Attributes.Add(attribute1);
        global::System.Xml.Schema.XmlSchemaAttribute attribute2 = new global::System.Xml.Schema.XmlSchemaAttribute();
        attribute2.Name = "tableTypeName";
        attribute2.FixedValue = "SubjectParametersDataTable";
        type.Attributes.Add(attribute2);
        type.Particle = sequence;
        global::System.Xml.Schema.XmlSchema dsSchema = ds.GetSchemaSerializable();
        if (xs.Contains(dsSchema.TargetNamespace))
        {
          global::System.IO.MemoryStream s1 = new global::System.IO.MemoryStream();
          global::System.IO.MemoryStream s2 = new global::System.IO.MemoryStream();
          try
          {
            global::System.Xml.Schema.XmlSchema schema = null;
            dsSchema.Write(s1);
            for (global::System.Collections.IEnumerator schemas = xs.Schemas(dsSchema.TargetNamespace).GetEnumerator(); schemas.MoveNext(); )
            {
              schema = ((global::System.Xml.Schema.XmlSchema)(schemas.Current));
              s2.SetLength(0);
              schema.Write(s2);
              if ((s1.Length == s2.Length))
              {
                s1.Position = 0;
                s2.Position = 0;
                for (; ((s1.Position != s1.Length)
                            && (s1.ReadByte() == s2.ReadByte())); )
                {
                  ;
                }
                if ((s1.Position == s1.Length))
                {
                  return type;
                }
              }
            }
          }
          finally
          {
            if ((s1 != null))
            {
              s1.Close();
            }
            if ((s2 != null))
            {
              s2.Close();
            }
          }
        }
        xs.Add(dsSchema);
        return type;
      }
    }

    /// <summary>
    /// Represents the strongly named DataTable class.
    /// </summary>
    [global::System.Serializable()]
    [global::System.Xml.Serialization.XmlSchemaProviderAttribute("GetTypedTableSchema")]
    public partial class SubjectsDataTable : TypedTableBase<SubjectsRow>
    {

      /// <summary>
      /// The column identifier
      /// </summary>
      private DataColumn columnID;

      /// <summary>
      /// The column subject name
      /// </summary>
      private DataColumn columnSubjectName;

      /// <summary>
      /// The column category
      /// </summary>
      private DataColumn columnCategory;

      /// <summary>
      /// The column age
      /// </summary>
      private DataColumn columnAge;

      /// <summary>
      /// The column sex
      /// </summary>
      private DataColumn columnSex;

      /// <summary>
      /// The column handedness
      /// </summary>
      private DataColumn columnHandedness;

      /// <summary>
      /// The column comments
      /// </summary>
      private DataColumn columnComments;



      /// <summary>
      /// Initializes a new instance of the <see cref="SubjectsDataTable"/> class.
      /// </summary>
      public SubjectsDataTable()
      {
        this.TableName = "Subjects";
        this.BeginInit();
        this.InitClass();
        this.EndInit();
      }



      /// <summary>
      /// Initializes a new instance of the <see cref="SubjectsDataTable"/> class.
      /// </summary>
      /// <param name="table">The table.</param>
      internal SubjectsDataTable(DataTable table)
      {
        this.TableName = table.TableName;
        if ((table.CaseSensitive != table.DataSet.CaseSensitive))
        {
          this.CaseSensitive = table.CaseSensitive;
        }
        if ((table.Locale.ToString() != table.DataSet.Locale.ToString()))
        {
          this.Locale = table.Locale;
        }
        if ((table.Namespace != table.DataSet.Namespace))
        {
          this.Namespace = table.Namespace;
        }
        this.Prefix = table.Prefix;
        this.MinimumCapacity = table.MinimumCapacity;
      }



      /// <summary>
      /// Initializes a new instance of the <see cref="SubjectsDataTable"/> class.
      /// </summary>
      /// <param name="info">The information.</param>
      /// <param name="context">The context.</param>
      protected SubjectsDataTable(global::System.Runtime.Serialization.SerializationInfo info, global::System.Runtime.Serialization.StreamingContext context) :
        base(info, context)
      {
        this.InitVars();
      }



      /// <summary>
      /// Gets the identifier column.
      /// </summary>
      /// <value>The identifier column.</value>
      public DataColumn IDColumn
      {
        get
        {
          return this.columnID;
        }
      }



      /// <summary>
      /// Gets the subject name column.
      /// </summary>
      /// <value>The subject name column.</value>
      public DataColumn SubjectNameColumn
      {
        get
        {
          return this.columnSubjectName;
        }
      }



      /// <summary>
      /// Gets the category column.
      /// </summary>
      /// <value>The category column.</value>
      public DataColumn CategoryColumn
      {
        get
        {
          return this.columnCategory;
        }
      }



      /// <summary>
      /// Gets the age column.
      /// </summary>
      /// <value>The age column.</value>
      public DataColumn AgeColumn
      {
        get
        {
          return this.columnAge;
        }
      }



      /// <summary>
      /// Gets the sex column.
      /// </summary>
      /// <value>The sex column.</value>
      public DataColumn SexColumn
      {
        get
        {
          return this.columnSex;
        }
      }



      /// <summary>
      /// Gets the handedness column.
      /// </summary>
      /// <value>The handedness column.</value>
      public DataColumn HandednessColumn
      {
        get
        {
          return this.columnHandedness;
        }
      }



      /// <summary>
      /// Gets the comments column.
      /// </summary>
      /// <value>The comments column.</value>
      public DataColumn CommentsColumn
      {
        get
        {
          return this.columnComments;
        }
      }




      /// <summary>
      /// Gets the count.
      /// </summary>
      /// <value>The count.</value>
      public int Count
      {
        get
        {
          return this.Rows.Count;
        }
      }



      /// <summary>
      /// Gets the <see cref="SubjectsRow"/> at the specified index.
      /// </summary>
      /// <param name="index">The index.</param>
      /// <returns>SubjectsRow.</returns>
      public SubjectsRow this[int index]
      {
        get
        {
          return ((SubjectsRow)(this.Rows[index]));
        }
      }


      /// <summary>
      /// Occurs when [subjects row changing].
      /// </summary>
      public event SubjectsRowChangeEventHandler SubjectsRowChanging;


      /// <summary>
      /// Occurs when [subjects row changed].
      /// </summary>
      public event SubjectsRowChangeEventHandler SubjectsRowChanged;


      /// <summary>
      /// Occurs when [subjects row deleting].
      /// </summary>
      public event SubjectsRowChangeEventHandler SubjectsRowDeleting;


      /// <summary>
      /// Occurs when [subjects row deleted].
      /// </summary>
      public event SubjectsRowChangeEventHandler SubjectsRowDeleted;



      /// <summary>
      /// Adds the subjects row.
      /// </summary>
      /// <param name="row">The row.</param>
      public void AddSubjectsRow(SubjectsRow row)
      {
        this.Rows.Add(row);
      }



      /// <summary>
      /// Adds the subjects row.
      /// </summary>
      /// <param name="SubjectName">Name of the subject.</param>
      /// <param name="Category">The category.</param>
      /// <param name="Age">The age.</param>
      /// <param name="Sex">The sex.</param>
      /// <param name="Handedness">The handedness.</param>
      /// <param name="Comments">The comments.</param>
      /// <returns>SubjectsRow.</returns>
      public SubjectsRow AddSubjectsRow(string SubjectName, string Category, int Age, string Sex, string Handedness, string Comments)
      {
        SubjectsRow rowSubjectsRow = ((SubjectsRow)(this.NewRow()));
        object[] columnValuesArray = new object[] {
                        null,
                        SubjectName,
                        Category,
                        Age,
                        Sex,
                        Handedness,
                        Comments};
        rowSubjectsRow.ItemArray = columnValuesArray;
        this.Rows.Add(rowSubjectsRow);
        return rowSubjectsRow;
      }



      /// <summary>
      /// Finds the by identifier.
      /// </summary>
      /// <param name="ID">The identifier.</param>
      /// <returns>SubjectsRow.</returns>
      public SubjectsRow FindByID(long ID)
      {
        return ((SubjectsRow)(this.Rows.Find(new object[] {
                            ID})));
      }



      /// <summary>
      /// Clones the structure of the <see cref="T:System.Data.DataTable" />, including all <see cref="T:System.Data.DataTable" /> schemas and constraints.
      /// </summary>
      /// <returns>A new <see cref="T:System.Data.DataTable" /> with the same schema as the current <see cref="T:System.Data.DataTable" />.</returns>
      /// <PermissionSet>
      ///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
      ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
      ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
      /// </PermissionSet>
      public override DataTable Clone()
      {
        SubjectsDataTable cln = ((SubjectsDataTable)(base.Clone()));
        cln.InitVars();
        return cln;
      }



      /// <summary>
      /// Creates a new instance of <see cref="T:System.Data.DataTable" />.
      /// </summary>
      /// <returns>The new expression.</returns>
      protected override DataTable CreateInstance()
      {
        return new SubjectsDataTable();
      }



      /// <summary>
      /// Initializes the vars.
      /// </summary>
      internal void InitVars()
      {
        this.columnID = base.Columns["ID"];
        this.columnSubjectName = base.Columns["SubjectName"];
        this.columnCategory = base.Columns["Category"];
        this.columnAge = base.Columns["Age"];
        this.columnSex = base.Columns["Sex"];
        this.columnHandedness = base.Columns["Handedness"];
        this.columnComments = base.Columns["Comments"];
      }



      /// <summary>
      /// Initializes the class.
      /// </summary>
      private void InitClass()
      {
        this.columnID = new DataColumn("ID", typeof(long), null, MappingType.Element);
        base.Columns.Add(this.columnID);
        this.columnSubjectName = new DataColumn("SubjectName", typeof(string), null, MappingType.Element);
        base.Columns.Add(this.columnSubjectName);
        this.columnCategory = new DataColumn("Category", typeof(string), null, MappingType.Element);
        base.Columns.Add(this.columnCategory);
        this.columnAge = new DataColumn("Age", typeof(int), null, MappingType.Element);
        base.Columns.Add(this.columnAge);
        this.columnSex = new DataColumn("Sex", typeof(string), null, MappingType.Element);
        base.Columns.Add(this.columnSex);
        this.columnHandedness = new DataColumn("Handedness", typeof(string), null, MappingType.Element);
        base.Columns.Add(this.columnHandedness);
        this.columnComments = new DataColumn("Comments", typeof(string), null, MappingType.Element);
        base.Columns.Add(this.columnComments);
        this.Constraints.Add(new UniqueConstraint("Constraint1", new DataColumn[] {
                                this.columnID}, true));
        this.Constraints.Add(new UniqueConstraint("SubjectsKey1", new DataColumn[] {
                                this.columnID,
                                this.columnSubjectName}, false));
        this.Constraints.Add(new UniqueConstraint("Constraint2", new DataColumn[] {
                                this.columnSubjectName}, false));
        this.columnID.AutoIncrement = true;
        this.columnID.AutoIncrementSeed = 1;
        this.columnID.AllowDBNull = false;
        this.columnID.ReadOnly = true;
        this.columnID.Unique = true;
        this.columnSubjectName.AllowDBNull = false;
        this.columnSubjectName.Unique = true;
        this.columnSubjectName.MaxLength = 50;
        this.columnCategory.MaxLength = 50;
        this.columnSex.MaxLength = 50;
        this.columnHandedness.MaxLength = 50;
        this.columnComments.MaxLength = 4000;
      }



      /// <summary>
      /// News the subjects row.
      /// </summary>
      /// <returns>SubjectsRow.</returns>
      public SubjectsRow NewSubjectsRow()
      {
        return ((SubjectsRow)(this.NewRow()));
      }



      /// <summary>
      /// Creates a new row from an existing row.
      /// </summary>
      /// <param name="builder">A <see cref="T:System.Data.DataRowBuilder" /> object.</param>
      /// <returns>A <see cref="T:System.Data.DataRow" /> derived class.</returns>
      protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
      {
        return new SubjectsRow(builder);
      }



      /// <summary>
      /// Gets the row type.
      /// </summary>
      /// <returns>Returns the type of the <see cref="T:System.Data.DataRow" />.</returns>
      protected override global::System.Type GetRowType()
      {
        return typeof(SubjectsRow);
      }



      /// <summary>
      /// Raises the <see cref="E:System.Data.DataTable.RowChanged" /> event.
      /// </summary>
      /// <param name="e">A <see cref="T:System.Data.DataRowChangeEventArgs" /> that contains the event data.</param>
      protected override void OnRowChanged(DataRowChangeEventArgs e)
      {
        base.OnRowChanged(e);
        if ((this.SubjectsRowChanged != null))
        {
          this.SubjectsRowChanged(this, new SubjectsRowChangeEvent(((SubjectsRow)(e.Row)), e.Action));
        }
      }



      /// <summary>
      /// Raises the <see cref="E:System.Data.DataTable.RowChanging" /> event.
      /// </summary>
      /// <param name="e">A <see cref="T:System.Data.DataRowChangeEventArgs" /> that contains the event data.</param>
      protected override void OnRowChanging(DataRowChangeEventArgs e)
      {
        base.OnRowChanging(e);
        if ((this.SubjectsRowChanging != null))
        {
          this.SubjectsRowChanging(this, new SubjectsRowChangeEvent(((SubjectsRow)(e.Row)), e.Action));
        }
      }



      /// <summary>
      /// Raises the <see cref="E:System.Data.DataTable.RowDeleted" /> event.
      /// </summary>
      /// <param name="e">A <see cref="T:System.Data.DataRowChangeEventArgs" /> that contains the event data.</param>
      protected override void OnRowDeleted(DataRowChangeEventArgs e)
      {
        base.OnRowDeleted(e);
        if ((this.SubjectsRowDeleted != null))
        {
          this.SubjectsRowDeleted(this, new SubjectsRowChangeEvent(((SubjectsRow)(e.Row)), e.Action));
        }
      }



      /// <summary>
      /// Raises the <see cref="E:System.Data.DataTable.RowDeleting" /> event.
      /// </summary>
      /// <param name="e">A <see cref="T:System.Data.DataRowChangeEventArgs" /> that contains the event data.</param>
      protected override void OnRowDeleting(DataRowChangeEventArgs e)
      {
        base.OnRowDeleting(e);
        if ((this.SubjectsRowDeleting != null))
        {
          this.SubjectsRowDeleting(this, new SubjectsRowChangeEvent(((SubjectsRow)(e.Row)), e.Action));
        }
      }



      /// <summary>
      /// Removes the subjects row.
      /// </summary>
      /// <param name="row">The row.</param>
      public void RemoveSubjectsRow(SubjectsRow row)
      {
        this.Rows.Remove(row);
      }



      /// <summary>
      /// Gets the typed table schema.
      /// </summary>
      /// <param name="xs">The xs.</param>
      /// <returns>System.Xml.Schema.XmlSchemaComplexType.</returns>
      public static global::System.Xml.Schema.XmlSchemaComplexType GetTypedTableSchema(global::System.Xml.Schema.XmlSchemaSet xs)
      {
        global::System.Xml.Schema.XmlSchemaComplexType type = new global::System.Xml.Schema.XmlSchemaComplexType();
        global::System.Xml.Schema.XmlSchemaSequence sequence = new global::System.Xml.Schema.XmlSchemaSequence();
        SQLiteOgamaDataSet ds = new SQLiteOgamaDataSet();
        global::System.Xml.Schema.XmlSchemaAny any1 = new global::System.Xml.Schema.XmlSchemaAny();
        any1.Namespace = "http://www.w3.org/2001/XMLSchema";
        any1.MinOccurs = new decimal(0);
        any1.MaxOccurs = decimal.MaxValue;
        any1.ProcessContents = global::System.Xml.Schema.XmlSchemaContentProcessing.Lax;
        sequence.Items.Add(any1);
        global::System.Xml.Schema.XmlSchemaAny any2 = new global::System.Xml.Schema.XmlSchemaAny();
        any2.Namespace = "urn:schemas-microsoft-com:xml-diffgram-v1";
        any2.MinOccurs = new decimal(1);
        any2.ProcessContents = global::System.Xml.Schema.XmlSchemaContentProcessing.Lax;
        sequence.Items.Add(any2);
        global::System.Xml.Schema.XmlSchemaAttribute attribute1 = new global::System.Xml.Schema.XmlSchemaAttribute();
        attribute1.Name = "namespace";
        attribute1.FixedValue = ds.Namespace;
        type.Attributes.Add(attribute1);
        global::System.Xml.Schema.XmlSchemaAttribute attribute2 = new global::System.Xml.Schema.XmlSchemaAttribute();
        attribute2.Name = "tableTypeName";
        attribute2.FixedValue = "SubjectsDataTable";
        type.Attributes.Add(attribute2);
        type.Particle = sequence;
        global::System.Xml.Schema.XmlSchema dsSchema = ds.GetSchemaSerializable();
        if (xs.Contains(dsSchema.TargetNamespace))
        {
          global::System.IO.MemoryStream s1 = new global::System.IO.MemoryStream();
          global::System.IO.MemoryStream s2 = new global::System.IO.MemoryStream();
          try
          {
            global::System.Xml.Schema.XmlSchema schema = null;
            dsSchema.Write(s1);
            for (global::System.Collections.IEnumerator schemas = xs.Schemas(dsSchema.TargetNamespace).GetEnumerator(); schemas.MoveNext(); )
            {
              schema = ((global::System.Xml.Schema.XmlSchema)(schemas.Current));
              s2.SetLength(0);
              schema.Write(s2);
              if ((s1.Length == s2.Length))
              {
                s1.Position = 0;
                s2.Position = 0;
                for (; ((s1.Position != s1.Length)
                            && (s1.ReadByte() == s2.ReadByte())); )
                {
                  ;
                }
                if ((s1.Position == s1.Length))
                {
                  return type;
                }
              }
            }
          }
          finally
          {
            if ((s1 != null))
            {
              s1.Close();
            }
            if ((s2 != null))
            {
              s2.Close();
            }
          }
        }
        xs.Add(dsSchema);
        return type;
      }
    }

    /// <summary>
    /// Represents the strongly named DataTable class.
    /// </summary>
    [global::System.Serializable()]
    [global::System.Xml.Serialization.XmlSchemaProviderAttribute("GetTypedTableSchema")]
    public partial class TrialsDataTable : TypedTableBase<TrialsRow>
    {

      /// <summary>
      /// The column identifier
      /// </summary>
      private DataColumn columnID;

      /// <summary>
      /// The column subject name
      /// </summary>
      private DataColumn columnSubjectName;

      /// <summary>
      /// The column trial identifier
      /// </summary>
      private DataColumn columnTrialID;

      /// <summary>
      /// The column trial name
      /// </summary>
      private DataColumn columnTrialName;

      /// <summary>
      /// The column trial sequence
      /// </summary>
      private DataColumn columnTrialSequence;

      /// <summary>
      /// The column category
      /// </summary>
      private DataColumn columnCategory;

      /// <summary>
      /// The column trial start time
      /// </summary>
      private DataColumn columnTrialStartTime;

      /// <summary>
      /// The column duration
      /// </summary>
      private DataColumn columnDuration;

      /// <summary>
      /// The column eliminate data
      /// </summary>
      private DataColumn columnEliminateData;



      /// <summary>
      /// Initializes a new instance of the <see cref="TrialsDataTable"/> class.
      /// </summary>
      public TrialsDataTable()
      {
        this.TableName = "Trials";
        this.BeginInit();
        this.InitClass();
        this.EndInit();
      }



      /// <summary>
      /// Initializes a new instance of the <see cref="TrialsDataTable"/> class.
      /// </summary>
      /// <param name="table">The table.</param>
      internal TrialsDataTable(DataTable table)
      {
        this.TableName = table.TableName;
        if ((table.CaseSensitive != table.DataSet.CaseSensitive))
        {
          this.CaseSensitive = table.CaseSensitive;
        }
        if ((table.Locale.ToString() != table.DataSet.Locale.ToString()))
        {
          this.Locale = table.Locale;
        }
        if ((table.Namespace != table.DataSet.Namespace))
        {
          this.Namespace = table.Namespace;
        }
        this.Prefix = table.Prefix;
        this.MinimumCapacity = table.MinimumCapacity;
      }



      /// <summary>
      /// Initializes a new instance of the <see cref="TrialsDataTable"/> class.
      /// </summary>
      /// <param name="info">The information.</param>
      /// <param name="context">The context.</param>
      protected TrialsDataTable(global::System.Runtime.Serialization.SerializationInfo info, global::System.Runtime.Serialization.StreamingContext context) :
        base(info, context)
      {
        this.InitVars();
      }



      /// <summary>
      /// Gets the identifier column.
      /// </summary>
      /// <value>The identifier column.</value>
      public DataColumn IDColumn
      {
        get
        {
          return this.columnID;
        }
      }



      /// <summary>
      /// Gets the subject name column.
      /// </summary>
      /// <value>The subject name column.</value>
      public DataColumn SubjectNameColumn
      {
        get
        {
          return this.columnSubjectName;
        }
      }



      /// <summary>
      /// Gets the trial identifier column.
      /// </summary>
      /// <value>The trial identifier column.</value>
      public DataColumn TrialIDColumn
      {
        get
        {
          return this.columnTrialID;
        }
      }



      /// <summary>
      /// Gets the trial name column.
      /// </summary>
      /// <value>The trial name column.</value>
      public DataColumn TrialNameColumn
      {
        get
        {
          return this.columnTrialName;
        }
      }



      /// <summary>
      /// Gets the trial sequence column.
      /// </summary>
      /// <value>The trial sequence column.</value>
      public DataColumn TrialSequenceColumn
      {
        get
        {
          return this.columnTrialSequence;
        }
      }



      /// <summary>
      /// Gets the category column.
      /// </summary>
      /// <value>The category column.</value>
      public DataColumn CategoryColumn
      {
        get
        {
          return this.columnCategory;
        }
      }



      /// <summary>
      /// Gets the trial start time column.
      /// </summary>
      /// <value>The trial start time column.</value>
      public DataColumn TrialStartTimeColumn
      {
        get
        {
          return this.columnTrialStartTime;
        }
      }



      /// <summary>
      /// Gets the duration column.
      /// </summary>
      /// <value>The duration column.</value>
      public DataColumn DurationColumn
      {
        get
        {
          return this.columnDuration;
        }
      }



      /// <summary>
      /// Gets the eliminate data column.
      /// </summary>
      /// <value>The eliminate data column.</value>
      public DataColumn EliminateDataColumn
      {
        get
        {
          return this.columnEliminateData;
        }
      }




      /// <summary>
      /// Gets the count.
      /// </summary>
      /// <value>The count.</value>
      public int Count
      {
        get
        {
          return this.Rows.Count;
        }
      }



      /// <summary>
      /// Gets the <see cref="TrialsRow"/> at the specified index.
      /// </summary>
      /// <param name="index">The index.</param>
      /// <returns>TrialsRow.</returns>
      public TrialsRow this[int index]
      {
        get
        {
          return ((TrialsRow)(this.Rows[index]));
        }
      }


      /// <summary>
      /// Occurs when [trials row changing].
      /// </summary>
      public event TrialsRowChangeEventHandler TrialsRowChanging;


      /// <summary>
      /// Occurs when [trials row changed].
      /// </summary>
      public event TrialsRowChangeEventHandler TrialsRowChanged;


      /// <summary>
      /// Occurs when [trials row deleting].
      /// </summary>
      public event TrialsRowChangeEventHandler TrialsRowDeleting;


      /// <summary>
      /// Occurs when [trials row deleted].
      /// </summary>
      public event TrialsRowChangeEventHandler TrialsRowDeleted;



      /// <summary>
      /// Adds the trials row.
      /// </summary>
      /// <param name="row">The row.</param>
      public void AddTrialsRow(TrialsRow row)
      {
        this.Rows.Add(row);
      }



      /// <summary>
      /// Adds the trials row.
      /// </summary>
      /// <param name="parentSubjectsRowByFK_Subjects_Trials">The parent subjects row by f k_ subjects_ trials.</param>
      /// <param name="TrialID">The trial identifier.</param>
      /// <param name="TrialName">Name of the trial.</param>
      /// <param name="TrialSequence">The trial sequence.</param>
      /// <param name="Category">The category.</param>
      /// <param name="TrialStartTime">The trial start time.</param>
      /// <param name="Duration">The duration.</param>
      /// <param name="EliminateData">The eliminate data.</param>
      /// <returns>TrialsRow.</returns>
      public TrialsRow AddTrialsRow(SubjectsRow parentSubjectsRowByFK_Subjects_Trials, int TrialID, string TrialName, int TrialSequence, string Category, long TrialStartTime, int Duration, string EliminateData)
      {
        TrialsRow rowTrialsRow = ((TrialsRow)(this.NewRow()));
        object[] columnValuesArray = new object[] {
                        null,
                        null,
                        TrialID,
                        TrialName,
                        TrialSequence,
                        Category,
                        TrialStartTime,
                        Duration,
                        EliminateData};
        if ((parentSubjectsRowByFK_Subjects_Trials != null))
        {
          columnValuesArray[1] = parentSubjectsRowByFK_Subjects_Trials[1];
        }
        rowTrialsRow.ItemArray = columnValuesArray;
        this.Rows.Add(rowTrialsRow);
        return rowTrialsRow;
      }



      /// <summary>
      /// Finds the by identifier.
      /// </summary>
      /// <param name="ID">The identifier.</param>
      /// <returns>TrialsRow.</returns>
      public TrialsRow FindByID(long ID)
      {
        return ((TrialsRow)(this.Rows.Find(new object[] {
                            ID})));
      }



      /// <summary>
      /// Clones the structure of the <see cref="T:System.Data.DataTable" />, including all <see cref="T:System.Data.DataTable" /> schemas and constraints.
      /// </summary>
      /// <returns>A new <see cref="T:System.Data.DataTable" /> with the same schema as the current <see cref="T:System.Data.DataTable" />.</returns>
      /// <PermissionSet>
      ///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
      ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
      ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
      /// </PermissionSet>
      public override DataTable Clone()
      {
        TrialsDataTable cln = ((TrialsDataTable)(base.Clone()));
        cln.InitVars();
        return cln;
      }



      /// <summary>
      /// Creates a new instance of <see cref="T:System.Data.DataTable" />.
      /// </summary>
      /// <returns>The new expression.</returns>
      protected override DataTable CreateInstance()
      {
        return new TrialsDataTable();
      }



      /// <summary>
      /// Initializes the vars.
      /// </summary>
      internal void InitVars()
      {
        this.columnID = base.Columns["ID"];
        this.columnSubjectName = base.Columns["SubjectName"];
        this.columnTrialID = base.Columns["TrialID"];
        this.columnTrialName = base.Columns["TrialName"];
        this.columnTrialSequence = base.Columns["TrialSequence"];
        this.columnCategory = base.Columns["Category"];
        this.columnTrialStartTime = base.Columns["TrialStartTime"];
        this.columnDuration = base.Columns["Duration"];
        this.columnEliminateData = base.Columns["EliminateData"];
      }



      /// <summary>
      /// Initializes the class.
      /// </summary>
      private void InitClass()
      {
        this.columnID = new DataColumn("ID", typeof(long), null, MappingType.Element);
        base.Columns.Add(this.columnID);
        this.columnSubjectName = new DataColumn("SubjectName", typeof(string), null, MappingType.Element);
        base.Columns.Add(this.columnSubjectName);
        this.columnTrialID = new DataColumn("TrialID", typeof(int), null, MappingType.Element);
        base.Columns.Add(this.columnTrialID);
        this.columnTrialName = new DataColumn("TrialName", typeof(string), null, MappingType.Element);
        base.Columns.Add(this.columnTrialName);
        this.columnTrialSequence = new DataColumn("TrialSequence", typeof(int), null, MappingType.Element);
        base.Columns.Add(this.columnTrialSequence);
        this.columnCategory = new DataColumn("Category", typeof(string), null, MappingType.Element);
        base.Columns.Add(this.columnCategory);
        this.columnTrialStartTime = new DataColumn("TrialStartTime", typeof(long), null, MappingType.Element);
        base.Columns.Add(this.columnTrialStartTime);
        this.columnDuration = new DataColumn("Duration", typeof(int), null, MappingType.Element);
        base.Columns.Add(this.columnDuration);
        this.columnEliminateData = new DataColumn("EliminateData", typeof(string), null, MappingType.Element);
        base.Columns.Add(this.columnEliminateData);
        this.Constraints.Add(new UniqueConstraint("Constraint3", new DataColumn[] {
                                this.columnSubjectName,
                                this.columnTrialSequence}, false));
        this.Constraints.Add(new UniqueConstraint("Constraint4", new DataColumn[] {
                                this.columnID}, true));
        this.columnID.AutoIncrement = true;
        this.columnID.AutoIncrementSeed = 1;
        this.columnID.AllowDBNull = false;
        this.columnID.ReadOnly = true;
        this.columnID.Unique = true;
        this.columnSubjectName.AllowDBNull = false;
        this.columnSubjectName.MaxLength = 255;
        this.columnTrialID.AllowDBNull = false;
        this.columnTrialName.MaxLength = 255;
        this.columnTrialSequence.AllowDBNull = false;
        this.columnCategory.MaxLength = 255;
        this.columnEliminateData.MaxLength = 50;
      }



      /// <summary>
      /// News the trials row.
      /// </summary>
      /// <returns>TrialsRow.</returns>
      public TrialsRow NewTrialsRow()
      {
        return ((TrialsRow)(this.NewRow()));
      }



      /// <summary>
      /// Creates a new row from an existing row.
      /// </summary>
      /// <param name="builder">A <see cref="T:System.Data.DataRowBuilder" /> object.</param>
      /// <returns>A <see cref="T:System.Data.DataRow" /> derived class.</returns>
      protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
      {
        return new TrialsRow(builder);
      }



      /// <summary>
      /// Gets the row type.
      /// </summary>
      /// <returns>Returns the type of the <see cref="T:System.Data.DataRow" />.</returns>
      protected override global::System.Type GetRowType()
      {
        return typeof(TrialsRow);
      }



      /// <summary>
      /// Raises the <see cref="E:System.Data.DataTable.RowChanged" /> event.
      /// </summary>
      /// <param name="e">A <see cref="T:System.Data.DataRowChangeEventArgs" /> that contains the event data.</param>
      protected override void OnRowChanged(DataRowChangeEventArgs e)
      {
        base.OnRowChanged(e);
        if ((this.TrialsRowChanged != null))
        {
          this.TrialsRowChanged(this, new TrialsRowChangeEvent(((TrialsRow)(e.Row)), e.Action));
        }
      }



      /// <summary>
      /// Raises the <see cref="E:System.Data.DataTable.RowChanging" /> event.
      /// </summary>
      /// <param name="e">A <see cref="T:System.Data.DataRowChangeEventArgs" /> that contains the event data.</param>
      protected override void OnRowChanging(DataRowChangeEventArgs e)
      {
        base.OnRowChanging(e);
        if ((this.TrialsRowChanging != null))
        {
          this.TrialsRowChanging(this, new TrialsRowChangeEvent(((TrialsRow)(e.Row)), e.Action));
        }
      }



      /// <summary>
      /// Raises the <see cref="E:System.Data.DataTable.RowDeleted" /> event.
      /// </summary>
      /// <param name="e">A <see cref="T:System.Data.DataRowChangeEventArgs" /> that contains the event data.</param>
      protected override void OnRowDeleted(DataRowChangeEventArgs e)
      {
        base.OnRowDeleted(e);
        if ((this.TrialsRowDeleted != null))
        {
          this.TrialsRowDeleted(this, new TrialsRowChangeEvent(((TrialsRow)(e.Row)), e.Action));
        }
      }



      /// <summary>
      /// Raises the <see cref="E:System.Data.DataTable.RowDeleting" /> event.
      /// </summary>
      /// <param name="e">A <see cref="T:System.Data.DataRowChangeEventArgs" /> that contains the event data.</param>
      protected override void OnRowDeleting(DataRowChangeEventArgs e)
      {
        base.OnRowDeleting(e);
        if ((this.TrialsRowDeleting != null))
        {
          this.TrialsRowDeleting(this, new TrialsRowChangeEvent(((TrialsRow)(e.Row)), e.Action));
        }
      }



      /// <summary>
      /// Removes the trials row.
      /// </summary>
      /// <param name="row">The row.</param>
      public void RemoveTrialsRow(TrialsRow row)
      {
        this.Rows.Remove(row);
      }



      /// <summary>
      /// Gets the typed table schema.
      /// </summary>
      /// <param name="xs">The xs.</param>
      /// <returns>System.Xml.Schema.XmlSchemaComplexType.</returns>
      public static global::System.Xml.Schema.XmlSchemaComplexType GetTypedTableSchema(global::System.Xml.Schema.XmlSchemaSet xs)
      {
        global::System.Xml.Schema.XmlSchemaComplexType type = new global::System.Xml.Schema.XmlSchemaComplexType();
        global::System.Xml.Schema.XmlSchemaSequence sequence = new global::System.Xml.Schema.XmlSchemaSequence();
        SQLiteOgamaDataSet ds = new SQLiteOgamaDataSet();
        global::System.Xml.Schema.XmlSchemaAny any1 = new global::System.Xml.Schema.XmlSchemaAny();
        any1.Namespace = "http://www.w3.org/2001/XMLSchema";
        any1.MinOccurs = new decimal(0);
        any1.MaxOccurs = decimal.MaxValue;
        any1.ProcessContents = global::System.Xml.Schema.XmlSchemaContentProcessing.Lax;
        sequence.Items.Add(any1);
        global::System.Xml.Schema.XmlSchemaAny any2 = new global::System.Xml.Schema.XmlSchemaAny();
        any2.Namespace = "urn:schemas-microsoft-com:xml-diffgram-v1";
        any2.MinOccurs = new decimal(1);
        any2.ProcessContents = global::System.Xml.Schema.XmlSchemaContentProcessing.Lax;
        sequence.Items.Add(any2);
        global::System.Xml.Schema.XmlSchemaAttribute attribute1 = new global::System.Xml.Schema.XmlSchemaAttribute();
        attribute1.Name = "namespace";
        attribute1.FixedValue = ds.Namespace;
        type.Attributes.Add(attribute1);
        global::System.Xml.Schema.XmlSchemaAttribute attribute2 = new global::System.Xml.Schema.XmlSchemaAttribute();
        attribute2.Name = "tableTypeName";
        attribute2.FixedValue = "TrialsDataTable";
        type.Attributes.Add(attribute2);
        type.Particle = sequence;
        global::System.Xml.Schema.XmlSchema dsSchema = ds.GetSchemaSerializable();
        if (xs.Contains(dsSchema.TargetNamespace))
        {
          global::System.IO.MemoryStream s1 = new global::System.IO.MemoryStream();
          global::System.IO.MemoryStream s2 = new global::System.IO.MemoryStream();
          try
          {
            global::System.Xml.Schema.XmlSchema schema = null;
            dsSchema.Write(s1);
            for (global::System.Collections.IEnumerator schemas = xs.Schemas(dsSchema.TargetNamespace).GetEnumerator(); schemas.MoveNext(); )
            {
              schema = ((global::System.Xml.Schema.XmlSchema)(schemas.Current));
              s2.SetLength(0);
              schema.Write(s2);
              if ((s1.Length == s2.Length))
              {
                s1.Position = 0;
                s2.Position = 0;
                for (; ((s1.Position != s1.Length)
                            && (s1.ReadByte() == s2.ReadByte())); )
                {
                  ;
                }
                if ((s1.Position == s1.Length))
                {
                  return type;
                }
              }
            }
          }
          finally
          {
            if ((s1 != null))
            {
              s1.Close();
            }
            if ((s2 != null))
            {
              s2.Close();
            }
          }
        }
        xs.Add(dsSchema);
        return type;
      }
    }

    /// <summary>
    /// Represents the strongly named DataTable class.
    /// </summary>
    [global::System.Serializable()]
    [global::System.Xml.Serialization.XmlSchemaProviderAttribute("GetTypedTableSchema")]
    public partial class TrialEventsDataTable : TypedTableBase<TrialEventsRow>
    {

      /// <summary>
      /// The column identifier
      /// </summary>
      private DataColumn columnID;

      /// <summary>
      /// The column subject name
      /// </summary>
      private DataColumn columnSubjectName;

      /// <summary>
      /// The column trial sequence
      /// </summary>
      private DataColumn columnTrialSequence;

      /// <summary>
      /// The column event identifier
      /// </summary>
      private DataColumn columnEventID;

      /// <summary>
      /// The column event time
      /// </summary>
      private DataColumn columnEventTime;

      /// <summary>
      /// The column event type
      /// </summary>
      private DataColumn columnEventType;

      /// <summary>
      /// The column event task
      /// </summary>
      private DataColumn columnEventTask;

      /// <summary>
      /// The column event parameter
      /// </summary>
      private DataColumn columnEventParam;



      /// <summary>
      /// Initializes a new instance of the <see cref="TrialEventsDataTable"/> class.
      /// </summary>
      public TrialEventsDataTable()
      {
        this.TableName = "TrialEvents";
        this.BeginInit();
        this.InitClass();
        this.EndInit();
      }



      /// <summary>
      /// Initializes a new instance of the <see cref="TrialEventsDataTable"/> class.
      /// </summary>
      /// <param name="table">The table.</param>
      internal TrialEventsDataTable(DataTable table)
      {
        this.TableName = table.TableName;
        if ((table.CaseSensitive != table.DataSet.CaseSensitive))
        {
          this.CaseSensitive = table.CaseSensitive;
        }
        if ((table.Locale.ToString() != table.DataSet.Locale.ToString()))
        {
          this.Locale = table.Locale;
        }
        if ((table.Namespace != table.DataSet.Namespace))
        {
          this.Namespace = table.Namespace;
        }
        this.Prefix = table.Prefix;
        this.MinimumCapacity = table.MinimumCapacity;
      }



      /// <summary>
      /// Initializes a new instance of the <see cref="TrialEventsDataTable"/> class.
      /// </summary>
      /// <param name="info">The information.</param>
      /// <param name="context">The context.</param>
      protected TrialEventsDataTable(global::System.Runtime.Serialization.SerializationInfo info, global::System.Runtime.Serialization.StreamingContext context) :
        base(info, context)
      {
        this.InitVars();
      }



      /// <summary>
      /// Gets the identifier column.
      /// </summary>
      /// <value>The identifier column.</value>
      public DataColumn IDColumn
      {
        get
        {
          return this.columnID;
        }
      }



      /// <summary>
      /// Gets the subject name column.
      /// </summary>
      /// <value>The subject name column.</value>
      public DataColumn SubjectNameColumn
      {
        get
        {
          return this.columnSubjectName;
        }
      }



      /// <summary>
      /// Gets the trial sequence column.
      /// </summary>
      /// <value>The trial sequence column.</value>
      public DataColumn TrialSequenceColumn
      {
        get
        {
          return this.columnTrialSequence;
        }
      }



      /// <summary>
      /// Gets the event identifier column.
      /// </summary>
      /// <value>The event identifier column.</value>
      public DataColumn EventIDColumn
      {
        get
        {
          return this.columnEventID;
        }
      }



      /// <summary>
      /// Gets the event time column.
      /// </summary>
      /// <value>The event time column.</value>
      public DataColumn EventTimeColumn
      {
        get
        {
          return this.columnEventTime;
        }
      }



      /// <summary>
      /// Gets the event type column.
      /// </summary>
      /// <value>The event type column.</value>
      public DataColumn EventTypeColumn
      {
        get
        {
          return this.columnEventType;
        }
      }



      /// <summary>
      /// Gets the event task column.
      /// </summary>
      /// <value>The event task column.</value>
      public DataColumn EventTaskColumn
      {
        get
        {
          return this.columnEventTask;
        }
      }



      /// <summary>
      /// Gets the event parameter column.
      /// </summary>
      /// <value>The event parameter column.</value>
      public DataColumn EventParamColumn
      {
        get
        {
          return this.columnEventParam;
        }
      }




      /// <summary>
      /// Gets the count.
      /// </summary>
      /// <value>The count.</value>
      public int Count
      {
        get
        {
          return this.Rows.Count;
        }
      }



      /// <summary>
      /// Gets the <see cref="TrialEventsRow"/> at the specified index.
      /// </summary>
      /// <param name="index">The index.</param>
      /// <returns>TrialEventsRow.</returns>
      public TrialEventsRow this[int index]
      {
        get
        {
          return ((TrialEventsRow)(this.Rows[index]));
        }
      }


      /// <summary>
      /// Occurs when [trial events row changing].
      /// </summary>
      public event TrialEventsRowChangeEventHandler TrialEventsRowChanging;


      /// <summary>
      /// Occurs when [trial events row changed].
      /// </summary>
      public event TrialEventsRowChangeEventHandler TrialEventsRowChanged;


      /// <summary>
      /// Occurs when [trial events row deleting].
      /// </summary>
      public event TrialEventsRowChangeEventHandler TrialEventsRowDeleting;


      /// <summary>
      /// Occurs when [trial events row deleted].
      /// </summary>
      public event TrialEventsRowChangeEventHandler TrialEventsRowDeleted;



      /// <summary>
      /// Adds the trial events row.
      /// </summary>
      /// <param name="row">The row.</param>
      public void AddTrialEventsRow(TrialEventsRow row)
      {
        this.Rows.Add(row);
      }



      /// <summary>
      /// Adds the trial events row.
      /// </summary>
      /// <param name="SubjectName">Name of the subject.</param>
      /// <param name="TrialSequence">The trial sequence.</param>
      /// <param name="EventID">The event identifier.</param>
      /// <param name="EventTime">The event time.</param>
      /// <param name="EventType">Type of the event.</param>
      /// <param name="EventTask">The event task.</param>
      /// <param name="EventParam">The event parameter.</param>
      /// <returns>TrialEventsRow.</returns>
      public TrialEventsRow AddTrialEventsRow(string SubjectName, int TrialSequence, int EventID, long EventTime, string EventType, string EventTask, string EventParam)
      {
        TrialEventsRow rowTrialEventsRow = ((TrialEventsRow)(this.NewRow()));
        object[] columnValuesArray = new object[] {
                        null,
                        SubjectName,
                        TrialSequence,
                        EventID,
                        EventTime,
                        EventType,
                        EventTask,
                        EventParam};
        rowTrialEventsRow.ItemArray = columnValuesArray;
        this.Rows.Add(rowTrialEventsRow);
        return rowTrialEventsRow;
      }



      /// <summary>
      /// Finds the by identifier.
      /// </summary>
      /// <param name="ID">The identifier.</param>
      /// <returns>TrialEventsRow.</returns>
      public TrialEventsRow FindByID(int ID)
      {
        return ((TrialEventsRow)(this.Rows.Find(new object[] {
                            ID})));
      }



      /// <summary>
      /// Clones the structure of the <see cref="T:System.Data.DataTable" />, including all <see cref="T:System.Data.DataTable" /> schemas and constraints.
      /// </summary>
      /// <returns>A new <see cref="T:System.Data.DataTable" /> with the same schema as the current <see cref="T:System.Data.DataTable" />.</returns>
      /// <PermissionSet>
      ///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
      ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
      ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
      /// </PermissionSet>
      public override DataTable Clone()
      {
        TrialEventsDataTable cln = ((TrialEventsDataTable)(base.Clone()));
        cln.InitVars();
        return cln;
      }



      /// <summary>
      /// Creates a new instance of <see cref="T:System.Data.DataTable" />.
      /// </summary>
      /// <returns>The new expression.</returns>
      protected override DataTable CreateInstance()
      {
        return new TrialEventsDataTable();
      }



      /// <summary>
      /// Initializes the vars.
      /// </summary>
      internal void InitVars()
      {
        this.columnID = base.Columns["ID"];
        this.columnSubjectName = base.Columns["SubjectName"];
        this.columnTrialSequence = base.Columns["TrialSequence"];
        this.columnEventID = base.Columns["EventID"];
        this.columnEventTime = base.Columns["EventTime"];
        this.columnEventType = base.Columns["EventType"];
        this.columnEventTask = base.Columns["EventTask"];
        this.columnEventParam = base.Columns["EventParam"];
      }



      /// <summary>
      /// Initializes the class.
      /// </summary>
      private void InitClass()
      {
        this.columnID = new DataColumn("ID", typeof(int), null, MappingType.Element);
        base.Columns.Add(this.columnID);
        this.columnSubjectName = new DataColumn("SubjectName", typeof(string), null, MappingType.Element);
        base.Columns.Add(this.columnSubjectName);
        this.columnTrialSequence = new DataColumn("TrialSequence", typeof(int), null, MappingType.Element);
        base.Columns.Add(this.columnTrialSequence);
        this.columnEventID = new DataColumn("EventID", typeof(int), null, MappingType.Element);
        base.Columns.Add(this.columnEventID);
        this.columnEventTime = new DataColumn("EventTime", typeof(long), null, MappingType.Element);
        base.Columns.Add(this.columnEventTime);
        this.columnEventType = new DataColumn("EventType", typeof(string), null, MappingType.Element);
        base.Columns.Add(this.columnEventType);
        this.columnEventTask = new DataColumn("EventTask", typeof(string), null, MappingType.Element);
        base.Columns.Add(this.columnEventTask);
        this.columnEventParam = new DataColumn("EventParam", typeof(string), null, MappingType.Element);
        base.Columns.Add(this.columnEventParam);
        this.Constraints.Add(new UniqueConstraint("Constraint1", new DataColumn[] {
                                this.columnID}, true));
        this.columnID.AutoIncrement = true;
        this.columnID.AutoIncrementSeed = 1;
        this.columnID.AllowDBNull = false;
        this.columnID.ReadOnly = true;
        this.columnID.Unique = true;
        this.columnSubjectName.AllowDBNull = false;
        this.columnSubjectName.MaxLength = 50;
        this.columnTrialSequence.AllowDBNull = false;
        this.columnEventID.AllowDBNull = false;
        this.columnEventTime.AllowDBNull = false;
        this.columnEventType.AllowDBNull = false;
        this.columnEventType.MaxLength = 50;
        this.columnEventTask.MaxLength = 50;
        this.columnEventParam.MaxLength = 4000;
      }



      /// <summary>
      /// News the trial events row.
      /// </summary>
      /// <returns>TrialEventsRow.</returns>
      public TrialEventsRow NewTrialEventsRow()
      {
        return ((TrialEventsRow)(this.NewRow()));
      }



      /// <summary>
      /// Creates a new row from an existing row.
      /// </summary>
      /// <param name="builder">A <see cref="T:System.Data.DataRowBuilder" /> object.</param>
      /// <returns>A <see cref="T:System.Data.DataRow" /> derived class.</returns>
      protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
      {
        return new TrialEventsRow(builder);
      }



      /// <summary>
      /// Gets the row type.
      /// </summary>
      /// <returns>Returns the type of the <see cref="T:System.Data.DataRow" />.</returns>
      protected override global::System.Type GetRowType()
      {
        return typeof(TrialEventsRow);
      }



      /// <summary>
      /// Raises the <see cref="E:System.Data.DataTable.RowChanged" /> event.
      /// </summary>
      /// <param name="e">A <see cref="T:System.Data.DataRowChangeEventArgs" /> that contains the event data.</param>
      protected override void OnRowChanged(DataRowChangeEventArgs e)
      {
        base.OnRowChanged(e);
        if ((this.TrialEventsRowChanged != null))
        {
          this.TrialEventsRowChanged(this, new TrialEventsRowChangeEvent(((TrialEventsRow)(e.Row)), e.Action));
        }
      }



      /// <summary>
      /// Raises the <see cref="E:System.Data.DataTable.RowChanging" /> event.
      /// </summary>
      /// <param name="e">A <see cref="T:System.Data.DataRowChangeEventArgs" /> that contains the event data.</param>
      protected override void OnRowChanging(DataRowChangeEventArgs e)
      {
        base.OnRowChanging(e);
        if ((this.TrialEventsRowChanging != null))
        {
          this.TrialEventsRowChanging(this, new TrialEventsRowChangeEvent(((TrialEventsRow)(e.Row)), e.Action));
        }
      }



      /// <summary>
      /// Raises the <see cref="E:System.Data.DataTable.RowDeleted" /> event.
      /// </summary>
      /// <param name="e">A <see cref="T:System.Data.DataRowChangeEventArgs" /> that contains the event data.</param>
      protected override void OnRowDeleted(DataRowChangeEventArgs e)
      {
        base.OnRowDeleted(e);
        if ((this.TrialEventsRowDeleted != null))
        {
          this.TrialEventsRowDeleted(this, new TrialEventsRowChangeEvent(((TrialEventsRow)(e.Row)), e.Action));
        }
      }



      /// <summary>
      /// Raises the <see cref="E:System.Data.DataTable.RowDeleting" /> event.
      /// </summary>
      /// <param name="e">A <see cref="T:System.Data.DataRowChangeEventArgs" /> that contains the event data.</param>
      protected override void OnRowDeleting(DataRowChangeEventArgs e)
      {
        base.OnRowDeleting(e);
        if ((this.TrialEventsRowDeleting != null))
        {
          this.TrialEventsRowDeleting(this, new TrialEventsRowChangeEvent(((TrialEventsRow)(e.Row)), e.Action));
        }
      }



      /// <summary>
      /// Removes the trial events row.
      /// </summary>
      /// <param name="row">The row.</param>
      public void RemoveTrialEventsRow(TrialEventsRow row)
      {
        this.Rows.Remove(row);
      }



      /// <summary>
      /// Gets the typed table schema.
      /// </summary>
      /// <param name="xs">The xs.</param>
      /// <returns>System.Xml.Schema.XmlSchemaComplexType.</returns>
      public static global::System.Xml.Schema.XmlSchemaComplexType GetTypedTableSchema(global::System.Xml.Schema.XmlSchemaSet xs)
      {
        global::System.Xml.Schema.XmlSchemaComplexType type = new global::System.Xml.Schema.XmlSchemaComplexType();
        global::System.Xml.Schema.XmlSchemaSequence sequence = new global::System.Xml.Schema.XmlSchemaSequence();
        SQLiteOgamaDataSet ds = new SQLiteOgamaDataSet();
        global::System.Xml.Schema.XmlSchemaAny any1 = new global::System.Xml.Schema.XmlSchemaAny();
        any1.Namespace = "http://www.w3.org/2001/XMLSchema";
        any1.MinOccurs = new decimal(0);
        any1.MaxOccurs = decimal.MaxValue;
        any1.ProcessContents = global::System.Xml.Schema.XmlSchemaContentProcessing.Lax;
        sequence.Items.Add(any1);
        global::System.Xml.Schema.XmlSchemaAny any2 = new global::System.Xml.Schema.XmlSchemaAny();
        any2.Namespace = "urn:schemas-microsoft-com:xml-diffgram-v1";
        any2.MinOccurs = new decimal(1);
        any2.ProcessContents = global::System.Xml.Schema.XmlSchemaContentProcessing.Lax;
        sequence.Items.Add(any2);
        global::System.Xml.Schema.XmlSchemaAttribute attribute1 = new global::System.Xml.Schema.XmlSchemaAttribute();
        attribute1.Name = "namespace";
        attribute1.FixedValue = ds.Namespace;
        type.Attributes.Add(attribute1);
        global::System.Xml.Schema.XmlSchemaAttribute attribute2 = new global::System.Xml.Schema.XmlSchemaAttribute();
        attribute2.Name = "tableTypeName";
        attribute2.FixedValue = "TrialEventsDataTable";
        type.Attributes.Add(attribute2);
        type.Particle = sequence;
        global::System.Xml.Schema.XmlSchema dsSchema = ds.GetSchemaSerializable();
        if (xs.Contains(dsSchema.TargetNamespace))
        {
          global::System.IO.MemoryStream s1 = new global::System.IO.MemoryStream();
          global::System.IO.MemoryStream s2 = new global::System.IO.MemoryStream();
          try
          {
            global::System.Xml.Schema.XmlSchema schema = null;
            dsSchema.Write(s1);
            for (global::System.Collections.IEnumerator schemas = xs.Schemas(dsSchema.TargetNamespace).GetEnumerator(); schemas.MoveNext(); )
            {
              schema = ((global::System.Xml.Schema.XmlSchema)(schemas.Current));
              s2.SetLength(0);
              schema.Write(s2);
              if ((s1.Length == s2.Length))
              {
                s1.Position = 0;
                s2.Position = 0;
                for (; ((s1.Position != s1.Length)
                            && (s1.ReadByte() == s2.ReadByte())); )
                {
                  ;
                }
                if ((s1.Position == s1.Length))
                {
                  return type;
                }
              }
            }
          }
          finally
          {
            if ((s1 != null))
            {
              s1.Close();
            }
            if ((s2 != null))
            {
              s2.Close();
            }
          }
        }
        xs.Add(dsSchema);
        return type;
      }
    }

    /// <summary>
    /// Represents the strongly named DataTable class.
    /// </summary>
    [global::System.Serializable()]
    [global::System.Xml.Serialization.XmlSchemaProviderAttribute("GetTypedTableSchema")]
    public partial class RawdataDataTable : TypedTableBase<RawdataRow>
    {

      /// <summary>
      /// The column identifier
      /// </summary>
      private DataColumn columnID;

      /// <summary>
      /// The column subject name
      /// </summary>
      private DataColumn columnSubjectName;

      /// <summary>
      /// The column trial sequence
      /// </summary>
      private DataColumn columnTrialSequence;

      /// <summary>
      /// The column time
      /// </summary>
      private DataColumn columnTime;

      /// <summary>
      /// The column pupil dia x
      /// </summary>
      private DataColumn columnPupilDiaX;

      /// <summary>
      /// The column pupil dia y
      /// </summary>
      private DataColumn columnPupilDiaY;

      /// <summary>
      /// The column gaze position x
      /// </summary>
      private DataColumn columnGazePosX;

      /// <summary>
      /// The column gaze position y
      /// </summary>
      private DataColumn columnGazePosY;

      /// <summary>
      /// The column mouse position x
      /// </summary>
      private DataColumn columnMousePosX;

      /// <summary>
      /// The column mouse position y
      /// </summary>
      private DataColumn columnMousePosY;

      /// <summary>
      /// The column event identifier
      /// </summary>
      private DataColumn columnEventID;



      /// <summary>
      /// Initializes a new instance of the <see cref="RawdataDataTable"/> class.
      /// </summary>
      public RawdataDataTable()
      {
        this.TableName = "Rawdata";
        this.BeginInit();
        this.InitClass();
        this.EndInit();
      }



      /// <summary>
      /// Initializes a new instance of the <see cref="RawdataDataTable"/> class.
      /// </summary>
      /// <param name="table">The table.</param>
      internal RawdataDataTable(DataTable table)
      {
        this.TableName = table.TableName;
        if ((table.CaseSensitive != table.DataSet.CaseSensitive))
        {
          this.CaseSensitive = table.CaseSensitive;
        }
        if ((table.Locale.ToString() != table.DataSet.Locale.ToString()))
        {
          this.Locale = table.Locale;
        }
        if ((table.Namespace != table.DataSet.Namespace))
        {
          this.Namespace = table.Namespace;
        }
        this.Prefix = table.Prefix;
        this.MinimumCapacity = table.MinimumCapacity;
      }



      /// <summary>
      /// Initializes a new instance of the <see cref="RawdataDataTable"/> class.
      /// </summary>
      /// <param name="info">The information.</param>
      /// <param name="context">The context.</param>
      protected RawdataDataTable(global::System.Runtime.Serialization.SerializationInfo info, global::System.Runtime.Serialization.StreamingContext context) :
        base(info, context)
      {
        this.InitVars();
      }



      /// <summary>
      /// Gets the identifier column.
      /// </summary>
      /// <value>The identifier column.</value>
      public DataColumn IDColumn
      {
        get
        {
          return this.columnID;
        }
      }



      /// <summary>
      /// Gets the subject name column.
      /// </summary>
      /// <value>The subject name column.</value>
      public DataColumn SubjectNameColumn
      {
        get
        {
          return this.columnSubjectName;
        }
      }



      /// <summary>
      /// Gets the trial sequence column.
      /// </summary>
      /// <value>The trial sequence column.</value>
      public DataColumn TrialSequenceColumn
      {
        get
        {
          return this.columnTrialSequence;
        }
      }



      /// <summary>
      /// Gets the time column.
      /// </summary>
      /// <value>The time column.</value>
      public DataColumn TimeColumn
      {
        get
        {
          return this.columnTime;
        }
      }



      /// <summary>
      /// Gets the pupil dia x column.
      /// </summary>
      /// <value>The pupil dia x column.</value>
      public DataColumn PupilDiaXColumn
      {
        get
        {
          return this.columnPupilDiaX;
        }
      }



      /// <summary>
      /// Gets the pupil dia y column.
      /// </summary>
      /// <value>The pupil dia y column.</value>
      public DataColumn PupilDiaYColumn
      {
        get
        {
          return this.columnPupilDiaY;
        }
      }



      /// <summary>
      /// Gets the gaze position x column.
      /// </summary>
      /// <value>The gaze position x column.</value>
      public DataColumn GazePosXColumn
      {
        get
        {
          return this.columnGazePosX;
        }
      }



      /// <summary>
      /// Gets the gaze position y column.
      /// </summary>
      /// <value>The gaze position y column.</value>
      public DataColumn GazePosYColumn
      {
        get
        {
          return this.columnGazePosY;
        }
      }



      /// <summary>
      /// Gets the mouse position x column.
      /// </summary>
      /// <value>The mouse position x column.</value>
      public DataColumn MousePosXColumn
      {
        get
        {
          return this.columnMousePosX;
        }
      }



      /// <summary>
      /// Gets the mouse position y column.
      /// </summary>
      /// <value>The mouse position y column.</value>
      public DataColumn MousePosYColumn
      {
        get
        {
          return this.columnMousePosY;
        }
      }



      /// <summary>
      /// Gets the event identifier column.
      /// </summary>
      /// <value>The event identifier column.</value>
      public DataColumn EventIDColumn
      {
        get
        {
          return this.columnEventID;
        }
      }




      /// <summary>
      /// Gets the count.
      /// </summary>
      /// <value>The count.</value>
      public int Count
      {
        get
        {
          return this.Rows.Count;
        }
      }



      /// <summary>
      /// Gets the <see cref="RawdataRow"/> at the specified index.
      /// </summary>
      /// <param name="index">The index.</param>
      /// <returns>RawdataRow.</returns>
      public RawdataRow this[int index]
      {
        get
        {
          return ((RawdataRow)(this.Rows[index]));
        }
      }


      /// <summary>
      /// Occurs when [rawdata row changing].
      /// </summary>
      public event RawdataRowChangeEventHandler RawdataRowChanging;


      /// <summary>
      /// Occurs when [rawdata row changed].
      /// </summary>
      public event RawdataRowChangeEventHandler RawdataRowChanged;


      /// <summary>
      /// Occurs when [rawdata row deleting].
      /// </summary>
      public event RawdataRowChangeEventHandler RawdataRowDeleting;


      /// <summary>
      /// Occurs when [rawdata row deleted].
      /// </summary>
      public event RawdataRowChangeEventHandler RawdataRowDeleted;



      /// <summary>
      /// Adds the rawdata row.
      /// </summary>
      /// <param name="row">The row.</param>
      public void AddRawdataRow(RawdataRow row)
      {
        this.Rows.Add(row);
      }



      /// <summary>
      /// Adds the rawdata row.
      /// </summary>
      /// <param name="SubjectName">Name of the subject.</param>
      /// <param name="TrialSequence">The trial sequence.</param>
      /// <param name="Time">The time.</param>
      /// <param name="PupilDiaX">The pupil dia x.</param>
      /// <param name="PupilDiaY">The pupil dia y.</param>
      /// <param name="GazePosX">The gaze position x.</param>
      /// <param name="GazePosY">The gaze position y.</param>
      /// <param name="MousePosX">The mouse position x.</param>
      /// <param name="MousePosY">The mouse position y.</param>
      /// <param name="EventID">The event identifier.</param>
      /// <returns>RawdataRow.</returns>
      public RawdataRow AddRawdataRow(string SubjectName, int TrialSequence, long Time, double PupilDiaX, double PupilDiaY, double GazePosX, double GazePosY, double MousePosX, double MousePosY, int EventID)
      {
        RawdataRow rowRawdataRow = ((RawdataRow)(this.NewRow()));
        object[] columnValuesArray = new object[] {
                        null,
                        SubjectName,
                        TrialSequence,
                        Time,
                        PupilDiaX,
                        PupilDiaY,
                        GazePosX,
                        GazePosY,
                        MousePosX,
                        MousePosY,
                        EventID};
        rowRawdataRow.ItemArray = columnValuesArray;
        this.Rows.Add(rowRawdataRow);
        return rowRawdataRow;
      }



      /// <summary>
      /// Finds the by identifier.
      /// </summary>
      /// <param name="ID">The identifier.</param>
      /// <returns>RawdataRow.</returns>
      public RawdataRow FindByID(long ID)
      {
        return ((RawdataRow)(this.Rows.Find(new object[] {
                            ID})));
      }



      /// <summary>
      /// Clones the structure of the <see cref="T:System.Data.DataTable" />, including all <see cref="T:System.Data.DataTable" /> schemas and constraints.
      /// </summary>
      /// <returns>A new <see cref="T:System.Data.DataTable" /> with the same schema as the current <see cref="T:System.Data.DataTable" />.</returns>
      /// <PermissionSet>
      ///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
      ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
      ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
      /// </PermissionSet>
      public override DataTable Clone()
      {
        RawdataDataTable cln = ((RawdataDataTable)(base.Clone()));
        cln.InitVars();
        return cln;
      }



      /// <summary>
      /// Creates a new instance of <see cref="T:System.Data.DataTable" />.
      /// </summary>
      /// <returns>The new expression.</returns>
      protected override DataTable CreateInstance()
      {
        return new RawdataDataTable();
      }



      /// <summary>
      /// Initializes the vars.
      /// </summary>
      internal void InitVars()
      {
        this.columnID = base.Columns["ID"];
        this.columnSubjectName = base.Columns["SubjectName"];
        this.columnTrialSequence = base.Columns["TrialSequence"];
        this.columnTime = base.Columns["Time"];
        this.columnPupilDiaX = base.Columns["PupilDiaX"];
        this.columnPupilDiaY = base.Columns["PupilDiaY"];
        this.columnGazePosX = base.Columns["GazePosX"];
        this.columnGazePosY = base.Columns["GazePosY"];
        this.columnMousePosX = base.Columns["MousePosX"];
        this.columnMousePosY = base.Columns["MousePosY"];
        this.columnEventID = base.Columns["EventID"];
      }



      /// <summary>
      /// Initializes the class.
      /// </summary>
      private void InitClass()
      {
        this.columnID = new DataColumn("ID", typeof(long), null, MappingType.Element);
        base.Columns.Add(this.columnID);
        this.columnSubjectName = new DataColumn("SubjectName", typeof(string), null, MappingType.Element);
        base.Columns.Add(this.columnSubjectName);
        this.columnTrialSequence = new DataColumn("TrialSequence", typeof(int), null, MappingType.Element);
        base.Columns.Add(this.columnTrialSequence);
        this.columnTime = new DataColumn("Time", typeof(long), null, MappingType.Element);
        base.Columns.Add(this.columnTime);
        this.columnPupilDiaX = new DataColumn("PupilDiaX", typeof(double), null, MappingType.Element);
        base.Columns.Add(this.columnPupilDiaX);
        this.columnPupilDiaY = new DataColumn("PupilDiaY", typeof(double), null, MappingType.Element);
        base.Columns.Add(this.columnPupilDiaY);
        this.columnGazePosX = new DataColumn("GazePosX", typeof(double), null, MappingType.Element);
        base.Columns.Add(this.columnGazePosX);
        this.columnGazePosY = new DataColumn("GazePosY", typeof(double), null, MappingType.Element);
        base.Columns.Add(this.columnGazePosY);
        this.columnMousePosX = new DataColumn("MousePosX", typeof(double), null, MappingType.Element);
        base.Columns.Add(this.columnMousePosX);
        this.columnMousePosY = new DataColumn("MousePosY", typeof(double), null, MappingType.Element);
        base.Columns.Add(this.columnMousePosY);
        this.columnEventID = new DataColumn("EventID", typeof(int), null, MappingType.Element);
        base.Columns.Add(this.columnEventID);
        this.Constraints.Add(new UniqueConstraint("Constraint1", new DataColumn[] {
                                this.columnID}, true));
        this.columnID.AutoIncrement = true;
        this.columnID.AllowDBNull = false;
        this.columnID.ReadOnly = true;
        this.columnID.Unique = true;
        this.columnSubjectName.AllowDBNull = false;
        this.columnSubjectName.MaxLength = 50;
        this.columnTrialSequence.AllowDBNull = false;
        this.columnTime.AllowDBNull = false;
      }



      /// <summary>
      /// News the rawdata row.
      /// </summary>
      /// <returns>RawdataRow.</returns>
      public RawdataRow NewRawdataRow()
      {
        return ((RawdataRow)(this.NewRow()));
      }



      /// <summary>
      /// Creates a new row from an existing row.
      /// </summary>
      /// <param name="builder">A <see cref="T:System.Data.DataRowBuilder" /> object.</param>
      /// <returns>A <see cref="T:System.Data.DataRow" /> derived class.</returns>
      protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
      {
        return new RawdataRow(builder);
      }



      /// <summary>
      /// Gets the row type.
      /// </summary>
      /// <returns>Returns the type of the <see cref="T:System.Data.DataRow" />.</returns>
      protected override global::System.Type GetRowType()
      {
        return typeof(RawdataRow);
      }



      /// <summary>
      /// Raises the <see cref="E:System.Data.DataTable.RowChanged" /> event.
      /// </summary>
      /// <param name="e">A <see cref="T:System.Data.DataRowChangeEventArgs" /> that contains the event data.</param>
      protected override void OnRowChanged(DataRowChangeEventArgs e)
      {
        base.OnRowChanged(e);
        if ((this.RawdataRowChanged != null))
        {
          this.RawdataRowChanged(this, new RawdataRowChangeEvent(((RawdataRow)(e.Row)), e.Action));
        }
      }



      /// <summary>
      /// Raises the <see cref="E:System.Data.DataTable.RowChanging" /> event.
      /// </summary>
      /// <param name="e">A <see cref="T:System.Data.DataRowChangeEventArgs" /> that contains the event data.</param>
      protected override void OnRowChanging(DataRowChangeEventArgs e)
      {
        base.OnRowChanging(e);
        if ((this.RawdataRowChanging != null))
        {
          this.RawdataRowChanging(this, new RawdataRowChangeEvent(((RawdataRow)(e.Row)), e.Action));
        }
      }



      /// <summary>
      /// Raises the <see cref="E:System.Data.DataTable.RowDeleted" /> event.
      /// </summary>
      /// <param name="e">A <see cref="T:System.Data.DataRowChangeEventArgs" /> that contains the event data.</param>
      protected override void OnRowDeleted(DataRowChangeEventArgs e)
      {
        base.OnRowDeleted(e);
        if ((this.RawdataRowDeleted != null))
        {
          this.RawdataRowDeleted(this, new RawdataRowChangeEvent(((RawdataRow)(e.Row)), e.Action));
        }
      }



      /// <summary>
      /// Raises the <see cref="E:System.Data.DataTable.RowDeleting" /> event.
      /// </summary>
      /// <param name="e">A <see cref="T:System.Data.DataRowChangeEventArgs" /> that contains the event data.</param>
      protected override void OnRowDeleting(DataRowChangeEventArgs e)
      {
        base.OnRowDeleting(e);
        if ((this.RawdataRowDeleting != null))
        {
          this.RawdataRowDeleting(this, new RawdataRowChangeEvent(((RawdataRow)(e.Row)), e.Action));
        }
      }



      /// <summary>
      /// Removes the rawdata row.
      /// </summary>
      /// <param name="row">The row.</param>
      public void RemoveRawdataRow(RawdataRow row)
      {
        this.Rows.Remove(row);
      }



      /// <summary>
      /// Gets the typed table schema.
      /// </summary>
      /// <param name="xs">The xs.</param>
      /// <returns>System.Xml.Schema.XmlSchemaComplexType.</returns>
      public static global::System.Xml.Schema.XmlSchemaComplexType GetTypedTableSchema(global::System.Xml.Schema.XmlSchemaSet xs)
      {
        global::System.Xml.Schema.XmlSchemaComplexType type = new global::System.Xml.Schema.XmlSchemaComplexType();
        global::System.Xml.Schema.XmlSchemaSequence sequence = new global::System.Xml.Schema.XmlSchemaSequence();
        SQLiteOgamaDataSet ds = new SQLiteOgamaDataSet();
        global::System.Xml.Schema.XmlSchemaAny any1 = new global::System.Xml.Schema.XmlSchemaAny();
        any1.Namespace = "http://www.w3.org/2001/XMLSchema";
        any1.MinOccurs = new decimal(0);
        any1.MaxOccurs = decimal.MaxValue;
        any1.ProcessContents = global::System.Xml.Schema.XmlSchemaContentProcessing.Lax;
        sequence.Items.Add(any1);
        global::System.Xml.Schema.XmlSchemaAny any2 = new global::System.Xml.Schema.XmlSchemaAny();
        any2.Namespace = "urn:schemas-microsoft-com:xml-diffgram-v1";
        any2.MinOccurs = new decimal(1);
        any2.ProcessContents = global::System.Xml.Schema.XmlSchemaContentProcessing.Lax;
        sequence.Items.Add(any2);
        global::System.Xml.Schema.XmlSchemaAttribute attribute1 = new global::System.Xml.Schema.XmlSchemaAttribute();
        attribute1.Name = "namespace";
        attribute1.FixedValue = ds.Namespace;
        type.Attributes.Add(attribute1);
        global::System.Xml.Schema.XmlSchemaAttribute attribute2 = new global::System.Xml.Schema.XmlSchemaAttribute();
        attribute2.Name = "tableTypeName";
        attribute2.FixedValue = "RawdataDataTable";
        type.Attributes.Add(attribute2);
        type.Particle = sequence;
        global::System.Xml.Schema.XmlSchema dsSchema = ds.GetSchemaSerializable();
        if (xs.Contains(dsSchema.TargetNamespace))
        {
          global::System.IO.MemoryStream s1 = new global::System.IO.MemoryStream();
          global::System.IO.MemoryStream s2 = new global::System.IO.MemoryStream();
          try
          {
            global::System.Xml.Schema.XmlSchema schema = null;
            dsSchema.Write(s1);
            for (global::System.Collections.IEnumerator schemas = xs.Schemas(dsSchema.TargetNamespace).GetEnumerator(); schemas.MoveNext(); )
            {
              schema = ((global::System.Xml.Schema.XmlSchema)(schemas.Current));
              s2.SetLength(0);
              schema.Write(s2);
              if ((s1.Length == s2.Length))
              {
                s1.Position = 0;
                s2.Position = 0;
                for (; ((s1.Position != s1.Length)
                            && (s1.ReadByte() == s2.ReadByte())); )
                {
                  ;
                }
                if ((s1.Position == s1.Length))
                {
                  return type;
                }
              }
            }
          }
          finally
          {
            if ((s1 != null))
            {
              s1.Close();
            }
            if ((s2 != null))
            {
              s2.Close();
            }
          }
        }
        xs.Add(dsSchema);
        return type;
      }
    }

    /// <summary>
    /// Represents the strongly named DataTable class.
    /// </summary>
    [global::System.Serializable()]
    [global::System.Xml.Serialization.XmlSchemaProviderAttribute("GetTypedTableSchema")]
    public partial class AOIsDataTable : TypedTableBase<AOIsRow>
    {

      /// <summary>
      /// The column identifier
      /// </summary>
      private DataColumn columnID;

      /// <summary>
      /// The column trial identifier
      /// </summary>
      private DataColumn columnTrialID;

      /// <summary>
      /// The column slide nr
      /// </summary>
      private DataColumn columnSlideNr;

      /// <summary>
      /// The column shape name
      /// </summary>
      private DataColumn columnShapeName;

      /// <summary>
      /// The column shape type
      /// </summary>
      private DataColumn columnShapeType;

      /// <summary>
      /// The column shape number PTS
      /// </summary>
      private DataColumn columnShapeNumPts;

      /// <summary>
      /// The column shape PTS
      /// </summary>
      private DataColumn columnShapePts;

      /// <summary>
      /// The column shape group
      /// </summary>
      private DataColumn columnShapeGroup;



      /// <summary>
      /// Initializes a new instance of the <see cref="AOIsDataTable"/> class.
      /// </summary>
      public AOIsDataTable()
      {
        this.TableName = "AOIs";
        this.BeginInit();
        this.InitClass();
        this.EndInit();
      }



      /// <summary>
      /// Initializes a new instance of the <see cref="AOIsDataTable"/> class.
      /// </summary>
      /// <param name="table">The table.</param>
      internal AOIsDataTable(DataTable table)
      {
        this.TableName = table.TableName;
        if ((table.CaseSensitive != table.DataSet.CaseSensitive))
        {
          this.CaseSensitive = table.CaseSensitive;
        }
        if ((table.Locale.ToString() != table.DataSet.Locale.ToString()))
        {
          this.Locale = table.Locale;
        }
        if ((table.Namespace != table.DataSet.Namespace))
        {
          this.Namespace = table.Namespace;
        }
        this.Prefix = table.Prefix;
        this.MinimumCapacity = table.MinimumCapacity;
      }



      /// <summary>
      /// Initializes a new instance of the <see cref="AOIsDataTable"/> class.
      /// </summary>
      /// <param name="info">The information.</param>
      /// <param name="context">The context.</param>
      protected AOIsDataTable(global::System.Runtime.Serialization.SerializationInfo info, global::System.Runtime.Serialization.StreamingContext context) :
        base(info, context)
      {
        this.InitVars();
      }



      /// <summary>
      /// Gets the identifier column.
      /// </summary>
      /// <value>The identifier column.</value>
      public DataColumn IDColumn
      {
        get
        {
          return this.columnID;
        }
      }



      /// <summary>
      /// Gets the trial identifier column.
      /// </summary>
      /// <value>The trial identifier column.</value>
      public DataColumn TrialIDColumn
      {
        get
        {
          return this.columnTrialID;
        }
      }



      /// <summary>
      /// Gets the slide nr column.
      /// </summary>
      /// <value>The slide nr column.</value>
      public DataColumn SlideNrColumn
      {
        get
        {
          return this.columnSlideNr;
        }
      }



      /// <summary>
      /// Gets the shape name column.
      /// </summary>
      /// <value>The shape name column.</value>
      public DataColumn ShapeNameColumn
      {
        get
        {
          return this.columnShapeName;
        }
      }



      /// <summary>
      /// Gets the shape type column.
      /// </summary>
      /// <value>The shape type column.</value>
      public DataColumn ShapeTypeColumn
      {
        get
        {
          return this.columnShapeType;
        }
      }



      /// <summary>
      /// Gets the shape number PTS column.
      /// </summary>
      /// <value>The shape number PTS column.</value>
      public DataColumn ShapeNumPtsColumn
      {
        get
        {
          return this.columnShapeNumPts;
        }
      }



      /// <summary>
      /// Gets the shape PTS column.
      /// </summary>
      /// <value>The shape PTS column.</value>
      public DataColumn ShapePtsColumn
      {
        get
        {
          return this.columnShapePts;
        }
      }



      /// <summary>
      /// Gets the shape group column.
      /// </summary>
      /// <value>The shape group column.</value>
      public DataColumn ShapeGroupColumn
      {
        get
        {
          return this.columnShapeGroup;
        }
      }




      /// <summary>
      /// Gets the count.
      /// </summary>
      /// <value>The count.</value>
      public int Count
      {
        get
        {
          return this.Rows.Count;
        }
      }



      /// <summary>
      /// Gets the <see cref="AOIsRow"/> at the specified index.
      /// </summary>
      /// <param name="index">The index.</param>
      /// <returns>AOIsRow.</returns>
      public AOIsRow this[int index]
      {
        get
        {
          return ((AOIsRow)(this.Rows[index]));
        }
      }


      /// <summary>
      /// Occurs when [ao is row changing].
      /// </summary>
      public event AOIsRowChangeEventHandler AOIsRowChanging;


      /// <summary>
      /// Occurs when [ao is row changed].
      /// </summary>
      public event AOIsRowChangeEventHandler AOIsRowChanged;


      /// <summary>
      /// Occurs when [ao is row deleting].
      /// </summary>
      public event AOIsRowChangeEventHandler AOIsRowDeleting;


      /// <summary>
      /// Occurs when [ao is row deleted].
      /// </summary>
      public event AOIsRowChangeEventHandler AOIsRowDeleted;



      /// <summary>
      /// Adds the ao is row.
      /// </summary>
      /// <param name="row">The row.</param>
      public void AddAOIsRow(AOIsRow row)
      {
        this.Rows.Add(row);
      }



      /// <summary>
      /// Adds the ao is row.
      /// </summary>
      /// <param name="parentTrialsRowByTrials_AOIs">The parent trials row by trials_ ao is.</param>
      /// <param name="SlideNr">The slide nr.</param>
      /// <param name="ShapeName">Name of the shape.</param>
      /// <param name="ShapeType">Type of the shape.</param>
      /// <param name="ShapeNumPts">The shape number PTS.</param>
      /// <param name="ShapePts">The shape PTS.</param>
      /// <param name="parentShapeGroupsRowByShapeGroups_AOIs">The parent shape groups row by shape groups_ ao is.</param>
      /// <returns>AOIsRow.</returns>
      public AOIsRow AddAOIsRow(TrialsRow parentTrialsRowByTrials_AOIs, int SlideNr, string ShapeName, string ShapeType, int ShapeNumPts, string ShapePts, ShapeGroupsRow parentShapeGroupsRowByShapeGroups_AOIs)
      {
        AOIsRow rowAOIsRow = ((AOIsRow)(this.NewRow()));
        object[] columnValuesArray = new object[] {
                        null,
                        null,
                        SlideNr,
                        ShapeName,
                        ShapeType,
                        ShapeNumPts,
                        ShapePts,
                        null};
        if ((parentTrialsRowByTrials_AOIs != null))
        {
          columnValuesArray[1] = parentTrialsRowByTrials_AOIs[2];
        }
        if ((parentShapeGroupsRowByShapeGroups_AOIs != null))
        {
          columnValuesArray[7] = parentShapeGroupsRowByShapeGroups_AOIs[1];
        }
        rowAOIsRow.ItemArray = columnValuesArray;
        this.Rows.Add(rowAOIsRow);
        return rowAOIsRow;
      }



      /// <summary>
      /// Finds the by identifier.
      /// </summary>
      /// <param name="ID">The identifier.</param>
      /// <returns>AOIsRow.</returns>
      public AOIsRow FindByID(int ID)
      {
        return ((AOIsRow)(this.Rows.Find(new object[] {
                            ID})));
      }



      /// <summary>
      /// Clones the structure of the <see cref="T:System.Data.DataTable" />, including all <see cref="T:System.Data.DataTable" /> schemas and constraints.
      /// </summary>
      /// <returns>A new <see cref="T:System.Data.DataTable" /> with the same schema as the current <see cref="T:System.Data.DataTable" />.</returns>
      /// <PermissionSet>
      ///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
      ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
      ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
      /// </PermissionSet>
      public override DataTable Clone()
      {
        AOIsDataTable cln = ((AOIsDataTable)(base.Clone()));
        cln.InitVars();
        return cln;
      }



      /// <summary>
      /// Creates a new instance of <see cref="T:System.Data.DataTable" />.
      /// </summary>
      /// <returns>The new expression.</returns>
      protected override DataTable CreateInstance()
      {
        return new AOIsDataTable();
      }



      /// <summary>
      /// Initializes the vars.
      /// </summary>
      internal void InitVars()
      {
        this.columnID = base.Columns["ID"];
        this.columnTrialID = base.Columns["TrialID"];
        this.columnSlideNr = base.Columns["SlideNr"];
        this.columnShapeName = base.Columns["ShapeName"];
        this.columnShapeType = base.Columns["ShapeType"];
        this.columnShapeNumPts = base.Columns["ShapeNumPts"];
        this.columnShapePts = base.Columns["ShapePts"];
        this.columnShapeGroup = base.Columns["ShapeGroup"];
      }



      /// <summary>
      /// Initializes the class.
      /// </summary>
      private void InitClass()
      {
        this.columnID = new DataColumn("ID", typeof(int), null, MappingType.Element);
        base.Columns.Add(this.columnID);
        this.columnTrialID = new DataColumn("TrialID", typeof(int), null, MappingType.Element);
        base.Columns.Add(this.columnTrialID);
        this.columnSlideNr = new DataColumn("SlideNr", typeof(int), null, MappingType.Element);
        base.Columns.Add(this.columnSlideNr);
        this.columnShapeName = new DataColumn("ShapeName", typeof(string), null, MappingType.Element);
        base.Columns.Add(this.columnShapeName);
        this.columnShapeType = new DataColumn("ShapeType", typeof(string), null, MappingType.Element);
        base.Columns.Add(this.columnShapeType);
        this.columnShapeNumPts = new DataColumn("ShapeNumPts", typeof(int), null, MappingType.Element);
        base.Columns.Add(this.columnShapeNumPts);
        this.columnShapePts = new DataColumn("ShapePts", typeof(string), null, MappingType.Element);
        base.Columns.Add(this.columnShapePts);
        this.columnShapeGroup = new DataColumn("ShapeGroup", typeof(string), null, MappingType.Element);
        base.Columns.Add(this.columnShapeGroup);
        this.Constraints.Add(new UniqueConstraint("Constraint1", new DataColumn[] {
                                this.columnID}, true));
        this.columnID.AutoIncrement = true;
        this.columnID.AutoIncrementSeed = 1;
        this.columnID.AllowDBNull = false;
        this.columnID.ReadOnly = true;
        this.columnID.Unique = true;
        this.columnTrialID.AllowDBNull = false;
        this.columnSlideNr.AllowDBNull = false;
        this.columnShapeName.MaxLength = 50;
        this.columnShapeType.MaxLength = 50;
        this.columnShapePts.MaxLength = 4000;
        this.columnShapeGroup.MaxLength = 50;
      }



      /// <summary>
      /// News the ao is row.
      /// </summary>
      /// <returns>AOIsRow.</returns>
      public AOIsRow NewAOIsRow()
      {
        return ((AOIsRow)(this.NewRow()));
      }



      /// <summary>
      /// Creates a new row from an existing row.
      /// </summary>
      /// <param name="builder">A <see cref="T:System.Data.DataRowBuilder" /> object.</param>
      /// <returns>A <see cref="T:System.Data.DataRow" /> derived class.</returns>
      protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
      {
        return new AOIsRow(builder);
      }



      /// <summary>
      /// Gets the row type.
      /// </summary>
      /// <returns>Returns the type of the <see cref="T:System.Data.DataRow" />.</returns>
      protected override global::System.Type GetRowType()
      {
        return typeof(AOIsRow);
      }



      /// <summary>
      /// Raises the <see cref="E:System.Data.DataTable.RowChanged" /> event.
      /// </summary>
      /// <param name="e">A <see cref="T:System.Data.DataRowChangeEventArgs" /> that contains the event data.</param>
      protected override void OnRowChanged(DataRowChangeEventArgs e)
      {
        base.OnRowChanged(e);
        if ((this.AOIsRowChanged != null))
        {
          this.AOIsRowChanged(this, new AOIsRowChangeEvent(((AOIsRow)(e.Row)), e.Action));
        }
      }



      /// <summary>
      /// Raises the <see cref="E:System.Data.DataTable.RowChanging" /> event.
      /// </summary>
      /// <param name="e">A <see cref="T:System.Data.DataRowChangeEventArgs" /> that contains the event data.</param>
      protected override void OnRowChanging(DataRowChangeEventArgs e)
      {
        base.OnRowChanging(e);
        if ((this.AOIsRowChanging != null))
        {
          this.AOIsRowChanging(this, new AOIsRowChangeEvent(((AOIsRow)(e.Row)), e.Action));
        }
      }



      /// <summary>
      /// Raises the <see cref="E:System.Data.DataTable.RowDeleted" /> event.
      /// </summary>
      /// <param name="e">A <see cref="T:System.Data.DataRowChangeEventArgs" /> that contains the event data.</param>
      protected override void OnRowDeleted(DataRowChangeEventArgs e)
      {
        base.OnRowDeleted(e);
        if ((this.AOIsRowDeleted != null))
        {
          this.AOIsRowDeleted(this, new AOIsRowChangeEvent(((AOIsRow)(e.Row)), e.Action));
        }
      }



      /// <summary>
      /// Raises the <see cref="E:System.Data.DataTable.RowDeleting" /> event.
      /// </summary>
      /// <param name="e">A <see cref="T:System.Data.DataRowChangeEventArgs" /> that contains the event data.</param>
      protected override void OnRowDeleting(DataRowChangeEventArgs e)
      {
        base.OnRowDeleting(e);
        if ((this.AOIsRowDeleting != null))
        {
          this.AOIsRowDeleting(this, new AOIsRowChangeEvent(((AOIsRow)(e.Row)), e.Action));
        }
      }



      /// <summary>
      /// Removes the ao is row.
      /// </summary>
      /// <param name="row">The row.</param>
      public void RemoveAOIsRow(AOIsRow row)
      {
        this.Rows.Remove(row);
      }



      /// <summary>
      /// Gets the typed table schema.
      /// </summary>
      /// <param name="xs">The xs.</param>
      /// <returns>System.Xml.Schema.XmlSchemaComplexType.</returns>
      public static global::System.Xml.Schema.XmlSchemaComplexType GetTypedTableSchema(global::System.Xml.Schema.XmlSchemaSet xs)
      {
        global::System.Xml.Schema.XmlSchemaComplexType type = new global::System.Xml.Schema.XmlSchemaComplexType();
        global::System.Xml.Schema.XmlSchemaSequence sequence = new global::System.Xml.Schema.XmlSchemaSequence();
        SQLiteOgamaDataSet ds = new SQLiteOgamaDataSet();
        global::System.Xml.Schema.XmlSchemaAny any1 = new global::System.Xml.Schema.XmlSchemaAny();
        any1.Namespace = "http://www.w3.org/2001/XMLSchema";
        any1.MinOccurs = new decimal(0);
        any1.MaxOccurs = decimal.MaxValue;
        any1.ProcessContents = global::System.Xml.Schema.XmlSchemaContentProcessing.Lax;
        sequence.Items.Add(any1);
        global::System.Xml.Schema.XmlSchemaAny any2 = new global::System.Xml.Schema.XmlSchemaAny();
        any2.Namespace = "urn:schemas-microsoft-com:xml-diffgram-v1";
        any2.MinOccurs = new decimal(1);
        any2.ProcessContents = global::System.Xml.Schema.XmlSchemaContentProcessing.Lax;
        sequence.Items.Add(any2);
        global::System.Xml.Schema.XmlSchemaAttribute attribute1 = new global::System.Xml.Schema.XmlSchemaAttribute();
        attribute1.Name = "namespace";
        attribute1.FixedValue = ds.Namespace;
        type.Attributes.Add(attribute1);
        global::System.Xml.Schema.XmlSchemaAttribute attribute2 = new global::System.Xml.Schema.XmlSchemaAttribute();
        attribute2.Name = "tableTypeName";
        attribute2.FixedValue = "AOIsDataTable";
        type.Attributes.Add(attribute2);
        type.Particle = sequence;
        global::System.Xml.Schema.XmlSchema dsSchema = ds.GetSchemaSerializable();
        if (xs.Contains(dsSchema.TargetNamespace))
        {
          global::System.IO.MemoryStream s1 = new global::System.IO.MemoryStream();
          global::System.IO.MemoryStream s2 = new global::System.IO.MemoryStream();
          try
          {
            global::System.Xml.Schema.XmlSchema schema = null;
            dsSchema.Write(s1);
            for (global::System.Collections.IEnumerator schemas = xs.Schemas(dsSchema.TargetNamespace).GetEnumerator(); schemas.MoveNext(); )
            {
              schema = ((global::System.Xml.Schema.XmlSchema)(schemas.Current));
              s2.SetLength(0);
              schema.Write(s2);
              if ((s1.Length == s2.Length))
              {
                s1.Position = 0;
                s2.Position = 0;
                for (; ((s1.Position != s1.Length)
                            && (s1.ReadByte() == s2.ReadByte())); )
                {
                  ;
                }
                if ((s1.Position == s1.Length))
                {
                  return type;
                }
              }
            }
          }
          finally
          {
            if ((s1 != null))
            {
              s1.Close();
            }
            if ((s2 != null))
            {
              s2.Close();
            }
          }
        }
        xs.Add(dsSchema);
        return type;
      }
    }

    /// <summary>
    /// Represents the strongly named DataTable class.
    /// </summary>
    [global::System.Serializable()]
    [global::System.Xml.Serialization.XmlSchemaProviderAttribute("GetTypedTableSchema")]
    public partial class ShapeGroupsDataTable : TypedTableBase<ShapeGroupsRow>
    {

      /// <summary>
      /// The column identifier
      /// </summary>
      private DataColumn columnID;

      /// <summary>
      /// The column shape group
      /// </summary>
      private DataColumn columnShapeGroup;



      /// <summary>
      /// Initializes a new instance of the <see cref="ShapeGroupsDataTable"/> class.
      /// </summary>
      public ShapeGroupsDataTable()
      {
        this.TableName = "ShapeGroups";
        this.BeginInit();
        this.InitClass();
        this.EndInit();
      }



      /// <summary>
      /// Initializes a new instance of the <see cref="ShapeGroupsDataTable"/> class.
      /// </summary>
      /// <param name="table">The table.</param>
      internal ShapeGroupsDataTable(DataTable table)
      {
        this.TableName = table.TableName;
        if ((table.CaseSensitive != table.DataSet.CaseSensitive))
        {
          this.CaseSensitive = table.CaseSensitive;
        }
        if ((table.Locale.ToString() != table.DataSet.Locale.ToString()))
        {
          this.Locale = table.Locale;
        }
        if ((table.Namespace != table.DataSet.Namespace))
        {
          this.Namespace = table.Namespace;
        }
        this.Prefix = table.Prefix;
        this.MinimumCapacity = table.MinimumCapacity;
      }



      /// <summary>
      /// Initializes a new instance of the <see cref="ShapeGroupsDataTable"/> class.
      /// </summary>
      /// <param name="info">The information.</param>
      /// <param name="context">The context.</param>
      protected ShapeGroupsDataTable(global::System.Runtime.Serialization.SerializationInfo info, global::System.Runtime.Serialization.StreamingContext context) :
        base(info, context)
      {
        this.InitVars();
      }



      /// <summary>
      /// Gets the identifier column.
      /// </summary>
      /// <value>The identifier column.</value>
      public DataColumn IDColumn
      {
        get
        {
          return this.columnID;
        }
      }



      /// <summary>
      /// Gets the shape group column.
      /// </summary>
      /// <value>The shape group column.</value>
      public DataColumn ShapeGroupColumn
      {
        get
        {
          return this.columnShapeGroup;
        }
      }




      /// <summary>
      /// Gets the count.
      /// </summary>
      /// <value>The count.</value>
      public int Count
      {
        get
        {
          return this.Rows.Count;
        }
      }



      /// <summary>
      /// Gets the <see cref="ShapeGroupsRow"/> at the specified index.
      /// </summary>
      /// <param name="index">The index.</param>
      /// <returns>ShapeGroupsRow.</returns>
      public ShapeGroupsRow this[int index]
      {
        get
        {
          return ((ShapeGroupsRow)(this.Rows[index]));
        }
      }


      /// <summary>
      /// Occurs when [shape groups row changing].
      /// </summary>
      public event ShapeGroupsRowChangeEventHandler ShapeGroupsRowChanging;


      /// <summary>
      /// Occurs when [shape groups row changed].
      /// </summary>
      public event ShapeGroupsRowChangeEventHandler ShapeGroupsRowChanged;


      /// <summary>
      /// Occurs when [shape groups row deleting].
      /// </summary>
      public event ShapeGroupsRowChangeEventHandler ShapeGroupsRowDeleting;


      /// <summary>
      /// Occurs when [shape groups row deleted].
      /// </summary>
      public event ShapeGroupsRowChangeEventHandler ShapeGroupsRowDeleted;



      /// <summary>
      /// Adds the shape groups row.
      /// </summary>
      /// <param name="row">The row.</param>
      public void AddShapeGroupsRow(ShapeGroupsRow row)
      {
        this.Rows.Add(row);
      }



      /// <summary>
      /// Adds the shape groups row.
      /// </summary>
      /// <param name="ShapeGroup">The shape group.</param>
      /// <returns>ShapeGroupsRow.</returns>
      public ShapeGroupsRow AddShapeGroupsRow(string ShapeGroup)
      {
        ShapeGroupsRow rowShapeGroupsRow = ((ShapeGroupsRow)(this.NewRow()));
        object[] columnValuesArray = new object[] {
                        null,
                        ShapeGroup};
        rowShapeGroupsRow.ItemArray = columnValuesArray;
        this.Rows.Add(rowShapeGroupsRow);
        return rowShapeGroupsRow;
      }



      /// <summary>
      /// Finds the by identifier.
      /// </summary>
      /// <param name="ID">The identifier.</param>
      /// <returns>ShapeGroupsRow.</returns>
      public ShapeGroupsRow FindByID(int ID)
      {
        return ((ShapeGroupsRow)(this.Rows.Find(new object[] {
                            ID})));
      }



      /// <summary>
      /// Clones the structure of the <see cref="T:System.Data.DataTable" />, including all <see cref="T:System.Data.DataTable" /> schemas and constraints.
      /// </summary>
      /// <returns>A new <see cref="T:System.Data.DataTable" /> with the same schema as the current <see cref="T:System.Data.DataTable" />.</returns>
      /// <PermissionSet>
      ///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
      ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
      ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
      /// </PermissionSet>
      public override DataTable Clone()
      {
        ShapeGroupsDataTable cln = ((ShapeGroupsDataTable)(base.Clone()));
        cln.InitVars();
        return cln;
      }



      /// <summary>
      /// Creates a new instance of <see cref="T:System.Data.DataTable" />.
      /// </summary>
      /// <returns>The new expression.</returns>
      protected override DataTable CreateInstance()
      {
        return new ShapeGroupsDataTable();
      }



      /// <summary>
      /// Initializes the vars.
      /// </summary>
      internal void InitVars()
      {
        this.columnID = base.Columns["ID"];
        this.columnShapeGroup = base.Columns["ShapeGroup"];
      }



      /// <summary>
      /// Initializes the class.
      /// </summary>
      private void InitClass()
      {
        this.columnID = new DataColumn("ID", typeof(int), null, MappingType.Element);
        base.Columns.Add(this.columnID);
        this.columnShapeGroup = new DataColumn("ShapeGroup", typeof(string), null, MappingType.Element);
        base.Columns.Add(this.columnShapeGroup);
        this.Constraints.Add(new UniqueConstraint("Constraint1", new DataColumn[] {
                                this.columnID}, true));
        this.columnID.AutoIncrement = true;
        this.columnID.AutoIncrementSeed = 1;
        this.columnID.AllowDBNull = false;
        this.columnID.ReadOnly = true;
        this.columnID.Unique = true;
        this.columnShapeGroup.AllowDBNull = false;
        this.columnShapeGroup.MaxLength = 50;
      }



      /// <summary>
      /// News the shape groups row.
      /// </summary>
      /// <returns>ShapeGroupsRow.</returns>
      public ShapeGroupsRow NewShapeGroupsRow()
      {
        return ((ShapeGroupsRow)(this.NewRow()));
      }



      /// <summary>
      /// Creates a new row from an existing row.
      /// </summary>
      /// <param name="builder">A <see cref="T:System.Data.DataRowBuilder" /> object.</param>
      /// <returns>A <see cref="T:System.Data.DataRow" /> derived class.</returns>
      protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
      {
        return new ShapeGroupsRow(builder);
      }



      /// <summary>
      /// Gets the row type.
      /// </summary>
      /// <returns>Returns the type of the <see cref="T:System.Data.DataRow" />.</returns>
      protected override global::System.Type GetRowType()
      {
        return typeof(ShapeGroupsRow);
      }



      /// <summary>
      /// Raises the <see cref="E:System.Data.DataTable.RowChanged" /> event.
      /// </summary>
      /// <param name="e">A <see cref="T:System.Data.DataRowChangeEventArgs" /> that contains the event data.</param>
      protected override void OnRowChanged(DataRowChangeEventArgs e)
      {
        base.OnRowChanged(e);
        if ((this.ShapeGroupsRowChanged != null))
        {
          this.ShapeGroupsRowChanged(this, new ShapeGroupsRowChangeEvent(((ShapeGroupsRow)(e.Row)), e.Action));
        }
      }



      /// <summary>
      /// Raises the <see cref="E:System.Data.DataTable.RowChanging" /> event.
      /// </summary>
      /// <param name="e">A <see cref="T:System.Data.DataRowChangeEventArgs" /> that contains the event data.</param>
      protected override void OnRowChanging(DataRowChangeEventArgs e)
      {
        base.OnRowChanging(e);
        if ((this.ShapeGroupsRowChanging != null))
        {
          this.ShapeGroupsRowChanging(this, new ShapeGroupsRowChangeEvent(((ShapeGroupsRow)(e.Row)), e.Action));
        }
      }



      /// <summary>
      /// Raises the <see cref="E:System.Data.DataTable.RowDeleted" /> event.
      /// </summary>
      /// <param name="e">A <see cref="T:System.Data.DataRowChangeEventArgs" /> that contains the event data.</param>
      protected override void OnRowDeleted(DataRowChangeEventArgs e)
      {
        base.OnRowDeleted(e);
        if ((this.ShapeGroupsRowDeleted != null))
        {
          this.ShapeGroupsRowDeleted(this, new ShapeGroupsRowChangeEvent(((ShapeGroupsRow)(e.Row)), e.Action));
        }
      }



      /// <summary>
      /// Raises the <see cref="E:System.Data.DataTable.RowDeleting" /> event.
      /// </summary>
      /// <param name="e">A <see cref="T:System.Data.DataRowChangeEventArgs" /> that contains the event data.</param>
      protected override void OnRowDeleting(DataRowChangeEventArgs e)
      {
        base.OnRowDeleting(e);
        if ((this.ShapeGroupsRowDeleting != null))
        {
          this.ShapeGroupsRowDeleting(this, new ShapeGroupsRowChangeEvent(((ShapeGroupsRow)(e.Row)), e.Action));
        }
      }



      /// <summary>
      /// Removes the shape groups row.
      /// </summary>
      /// <param name="row">The row.</param>
      public void RemoveShapeGroupsRow(ShapeGroupsRow row)
      {
        this.Rows.Remove(row);
      }



      /// <summary>
      /// Gets the typed table schema.
      /// </summary>
      /// <param name="xs">The xs.</param>
      /// <returns>System.Xml.Schema.XmlSchemaComplexType.</returns>
      public static global::System.Xml.Schema.XmlSchemaComplexType GetTypedTableSchema(global::System.Xml.Schema.XmlSchemaSet xs)
      {
        global::System.Xml.Schema.XmlSchemaComplexType type = new global::System.Xml.Schema.XmlSchemaComplexType();
        global::System.Xml.Schema.XmlSchemaSequence sequence = new global::System.Xml.Schema.XmlSchemaSequence();
        SQLiteOgamaDataSet ds = new SQLiteOgamaDataSet();
        global::System.Xml.Schema.XmlSchemaAny any1 = new global::System.Xml.Schema.XmlSchemaAny();
        any1.Namespace = "http://www.w3.org/2001/XMLSchema";
        any1.MinOccurs = new decimal(0);
        any1.MaxOccurs = decimal.MaxValue;
        any1.ProcessContents = global::System.Xml.Schema.XmlSchemaContentProcessing.Lax;
        sequence.Items.Add(any1);
        global::System.Xml.Schema.XmlSchemaAny any2 = new global::System.Xml.Schema.XmlSchemaAny();
        any2.Namespace = "urn:schemas-microsoft-com:xml-diffgram-v1";
        any2.MinOccurs = new decimal(1);
        any2.ProcessContents = global::System.Xml.Schema.XmlSchemaContentProcessing.Lax;
        sequence.Items.Add(any2);
        global::System.Xml.Schema.XmlSchemaAttribute attribute1 = new global::System.Xml.Schema.XmlSchemaAttribute();
        attribute1.Name = "namespace";
        attribute1.FixedValue = ds.Namespace;
        type.Attributes.Add(attribute1);
        global::System.Xml.Schema.XmlSchemaAttribute attribute2 = new global::System.Xml.Schema.XmlSchemaAttribute();
        attribute2.Name = "tableTypeName";
        attribute2.FixedValue = "ShapeGroupsDataTable";
        type.Attributes.Add(attribute2);
        type.Particle = sequence;
        global::System.Xml.Schema.XmlSchema dsSchema = ds.GetSchemaSerializable();
        if (xs.Contains(dsSchema.TargetNamespace))
        {
          global::System.IO.MemoryStream s1 = new global::System.IO.MemoryStream();
          global::System.IO.MemoryStream s2 = new global::System.IO.MemoryStream();
          try
          {
            global::System.Xml.Schema.XmlSchema schema = null;
            dsSchema.Write(s1);
            for (global::System.Collections.IEnumerator schemas = xs.Schemas(dsSchema.TargetNamespace).GetEnumerator(); schemas.MoveNext(); )
            {
              schema = ((global::System.Xml.Schema.XmlSchema)(schemas.Current));
              s2.SetLength(0);
              schema.Write(s2);
              if ((s1.Length == s2.Length))
              {
                s1.Position = 0;
                s2.Position = 0;
                for (; ((s1.Position != s1.Length)
                            && (s1.ReadByte() == s2.ReadByte())); )
                {
                  ;
                }
                if ((s1.Position == s1.Length))
                {
                  return type;
                }
              }
            }
          }
          finally
          {
            if ((s1 != null))
            {
              s1.Close();
            }
            if ((s2 != null))
            {
              s2.Close();
            }
          }
        }
        xs.Add(dsSchema);
        return type;
      }
    }

    /// <summary>
    /// Represents the strongly named DataTable class.
    /// </summary>
    [global::System.Serializable()]
    [global::System.Xml.Serialization.XmlSchemaProviderAttribute("GetTypedTableSchema")]
    public partial class GazeFixationsDataTable : TypedTableBase<GazeFixationsRow>
    {

      /// <summary>
      /// The column identifier
      /// </summary>
      private DataColumn columnID;

      /// <summary>
      /// The column subject name
      /// </summary>
      private DataColumn columnSubjectName;

      /// <summary>
      /// The column trial identifier
      /// </summary>
      private DataColumn columnTrialID;

      /// <summary>
      /// The column trial sequence
      /// </summary>
      private DataColumn columnTrialSequence;

      /// <summary>
      /// The column count in trial
      /// </summary>
      private DataColumn columnCountInTrial;

      /// <summary>
      /// The column start time
      /// </summary>
      private DataColumn columnStartTime;

      /// <summary>
      /// The column length
      /// </summary>
      private DataColumn columnLength;

      /// <summary>
      /// The column position x
      /// </summary>
      private DataColumn columnPosX;

      /// <summary>
      /// The column position y
      /// </summary>
      private DataColumn columnPosY;



      /// <summary>
      /// Initializes a new instance of the <see cref="GazeFixationsDataTable"/> class.
      /// </summary>
      public GazeFixationsDataTable()
      {
        this.TableName = "GazeFixations";
        this.BeginInit();
        this.InitClass();
        this.EndInit();
      }



      /// <summary>
      /// Initializes a new instance of the <see cref="GazeFixationsDataTable"/> class.
      /// </summary>
      /// <param name="table">The table.</param>
      internal GazeFixationsDataTable(DataTable table)
      {
        this.TableName = table.TableName;
        if ((table.CaseSensitive != table.DataSet.CaseSensitive))
        {
          this.CaseSensitive = table.CaseSensitive;
        }
        if ((table.Locale.ToString() != table.DataSet.Locale.ToString()))
        {
          this.Locale = table.Locale;
        }
        if ((table.Namespace != table.DataSet.Namespace))
        {
          this.Namespace = table.Namespace;
        }
        this.Prefix = table.Prefix;
        this.MinimumCapacity = table.MinimumCapacity;
      }



      /// <summary>
      /// Initializes a new instance of the <see cref="GazeFixationsDataTable"/> class.
      /// </summary>
      /// <param name="info">The information.</param>
      /// <param name="context">The context.</param>
      protected GazeFixationsDataTable(global::System.Runtime.Serialization.SerializationInfo info, global::System.Runtime.Serialization.StreamingContext context) :
        base(info, context)
      {
        this.InitVars();
      }



      /// <summary>
      /// Gets the identifier column.
      /// </summary>
      /// <value>The identifier column.</value>
      public DataColumn IDColumn
      {
        get
        {
          return this.columnID;
        }
      }



      /// <summary>
      /// Gets the subject name column.
      /// </summary>
      /// <value>The subject name column.</value>
      public DataColumn SubjectNameColumn
      {
        get
        {
          return this.columnSubjectName;
        }
      }



      /// <summary>
      /// Gets the trial identifier column.
      /// </summary>
      /// <value>The trial identifier column.</value>
      public DataColumn TrialIDColumn
      {
        get
        {
          return this.columnTrialID;
        }
      }



      /// <summary>
      /// Gets the trial sequence column.
      /// </summary>
      /// <value>The trial sequence column.</value>
      public DataColumn TrialSequenceColumn
      {
        get
        {
          return this.columnTrialSequence;
        }
      }



      /// <summary>
      /// Gets the count in trial column.
      /// </summary>
      /// <value>The count in trial column.</value>
      public DataColumn CountInTrialColumn
      {
        get
        {
          return this.columnCountInTrial;
        }
      }



      /// <summary>
      /// Gets the start time column.
      /// </summary>
      /// <value>The start time column.</value>
      public DataColumn StartTimeColumn
      {
        get
        {
          return this.columnStartTime;
        }
      }



      /// <summary>
      /// Gets the length column.
      /// </summary>
      /// <value>The length column.</value>
      public DataColumn LengthColumn
      {
        get
        {
          return this.columnLength;
        }
      }



      /// <summary>
      /// Gets the position x column.
      /// </summary>
      /// <value>The position x column.</value>
      public DataColumn PosXColumn
      {
        get
        {
          return this.columnPosX;
        }
      }



      /// <summary>
      /// Gets the position y column.
      /// </summary>
      /// <value>The position y column.</value>
      public DataColumn PosYColumn
      {
        get
        {
          return this.columnPosY;
        }
      }




      /// <summary>
      /// Gets the count.
      /// </summary>
      /// <value>The count.</value>
      public int Count
      {
        get
        {
          return this.Rows.Count;
        }
      }



      /// <summary>
      /// Gets the <see cref="GazeFixationsRow"/> at the specified index.
      /// </summary>
      /// <param name="index">The index.</param>
      /// <returns>GazeFixationsRow.</returns>
      public GazeFixationsRow this[int index]
      {
        get
        {
          return ((GazeFixationsRow)(this.Rows[index]));
        }
      }


      /// <summary>
      /// Occurs when [gaze fixations row changing].
      /// </summary>
      public event GazeFixationsRowChangeEventHandler GazeFixationsRowChanging;


      /// <summary>
      /// Occurs when [gaze fixations row changed].
      /// </summary>
      public event GazeFixationsRowChangeEventHandler GazeFixationsRowChanged;


      /// <summary>
      /// Occurs when [gaze fixations row deleting].
      /// </summary>
      public event GazeFixationsRowChangeEventHandler GazeFixationsRowDeleting;


      /// <summary>
      /// Occurs when [gaze fixations row deleted].
      /// </summary>
      public event GazeFixationsRowChangeEventHandler GazeFixationsRowDeleted;



      /// <summary>
      /// Adds the gaze fixations row.
      /// </summary>
      /// <param name="row">The row.</param>
      public void AddGazeFixationsRow(GazeFixationsRow row)
      {
        this.Rows.Add(row);
      }



      /// <summary>
      /// Adds the gaze fixations row.
      /// </summary>
      /// <param name="SubjectName">Name of the subject.</param>
      /// <param name="TrialID">The trial identifier.</param>
      /// <param name="TrialSequence">The trial sequence.</param>
      /// <param name="CountInTrial">The count in trial.</param>
      /// <param name="StartTime">The start time.</param>
      /// <param name="Length">The length.</param>
      /// <param name="PosX">The position x.</param>
      /// <param name="PosY">The position y.</param>
      /// <returns>GazeFixationsRow.</returns>
      public GazeFixationsRow AddGazeFixationsRow(string SubjectName, int TrialID, int TrialSequence, int CountInTrial, long StartTime, int Length, double PosX, double PosY)
      {
        GazeFixationsRow rowGazeFixationsRow = ((GazeFixationsRow)(this.NewRow()));
        object[] columnValuesArray = new object[] {
                        null,
                        SubjectName,
                        TrialID,
                        TrialSequence,
                        CountInTrial,
                        StartTime,
                        Length,
                        PosX,
                        PosY};
        rowGazeFixationsRow.ItemArray = columnValuesArray;
        this.Rows.Add(rowGazeFixationsRow);
        return rowGazeFixationsRow;
      }



      /// <summary>
      /// Finds the by identifier.
      /// </summary>
      /// <param name="ID">The identifier.</param>
      /// <returns>GazeFixationsRow.</returns>
      public GazeFixationsRow FindByID(long ID)
      {
        return ((GazeFixationsRow)(this.Rows.Find(new object[] {
                            ID})));
      }



      /// <summary>
      /// Clones the structure of the <see cref="T:System.Data.DataTable" />, including all <see cref="T:System.Data.DataTable" /> schemas and constraints.
      /// </summary>
      /// <returns>A new <see cref="T:System.Data.DataTable" /> with the same schema as the current <see cref="T:System.Data.DataTable" />.</returns>
      /// <PermissionSet>
      ///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
      ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
      ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
      /// </PermissionSet>
      public override DataTable Clone()
      {
        GazeFixationsDataTable cln = ((GazeFixationsDataTable)(base.Clone()));
        cln.InitVars();
        return cln;
      }



      /// <summary>
      /// Creates a new instance of <see cref="T:System.Data.DataTable" />.
      /// </summary>
      /// <returns>The new expression.</returns>
      protected override DataTable CreateInstance()
      {
        return new GazeFixationsDataTable();
      }



      /// <summary>
      /// Initializes the vars.
      /// </summary>
      internal void InitVars()
      {
        this.columnID = base.Columns["ID"];
        this.columnSubjectName = base.Columns["SubjectName"];
        this.columnTrialID = base.Columns["TrialID"];
        this.columnTrialSequence = base.Columns["TrialSequence"];
        this.columnCountInTrial = base.Columns["CountInTrial"];
        this.columnStartTime = base.Columns["StartTime"];
        this.columnLength = base.Columns["Length"];
        this.columnPosX = base.Columns["PosX"];
        this.columnPosY = base.Columns["PosY"];
      }



      /// <summary>
      /// Initializes the class.
      /// </summary>
      private void InitClass()
      {
        this.columnID = new DataColumn("ID", typeof(long), null, MappingType.Element);
        base.Columns.Add(this.columnID);
        this.columnSubjectName = new DataColumn("SubjectName", typeof(string), null, MappingType.Element);
        base.Columns.Add(this.columnSubjectName);
        this.columnTrialID = new DataColumn("TrialID", typeof(int), null, MappingType.Element);
        base.Columns.Add(this.columnTrialID);
        this.columnTrialSequence = new DataColumn("TrialSequence", typeof(int), null, MappingType.Element);
        base.Columns.Add(this.columnTrialSequence);
        this.columnCountInTrial = new DataColumn("CountInTrial", typeof(int), null, MappingType.Element);
        base.Columns.Add(this.columnCountInTrial);
        this.columnStartTime = new DataColumn("StartTime", typeof(long), null, MappingType.Element);
        base.Columns.Add(this.columnStartTime);
        this.columnLength = new DataColumn("Length", typeof(int), null, MappingType.Element);
        base.Columns.Add(this.columnLength);
        this.columnPosX = new DataColumn("PosX", typeof(double), null, MappingType.Element);
        base.Columns.Add(this.columnPosX);
        this.columnPosY = new DataColumn("PosY", typeof(double), null, MappingType.Element);
        base.Columns.Add(this.columnPosY);
        this.Constraints.Add(new UniqueConstraint("Constraint1", new DataColumn[] {
                                this.columnID}, true));
        this.columnID.AutoIncrement = true;
        this.columnID.AutoIncrementSeed = 1;
        this.columnID.AllowDBNull = false;
        this.columnID.ReadOnly = true;
        this.columnID.Unique = true;
        this.columnSubjectName.AllowDBNull = false;
        this.columnSubjectName.MaxLength = 50;
        this.columnTrialID.AllowDBNull = false;
        this.columnTrialSequence.AllowDBNull = false;
      }



      /// <summary>
      /// News the gaze fixations row.
      /// </summary>
      /// <returns>GazeFixationsRow.</returns>
      public GazeFixationsRow NewGazeFixationsRow()
      {
        return ((GazeFixationsRow)(this.NewRow()));
      }



      /// <summary>
      /// Creates a new row from an existing row.
      /// </summary>
      /// <param name="builder">A <see cref="T:System.Data.DataRowBuilder" /> object.</param>
      /// <returns>A <see cref="T:System.Data.DataRow" /> derived class.</returns>
      protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
      {
        return new GazeFixationsRow(builder);
      }



      /// <summary>
      /// Gets the row type.
      /// </summary>
      /// <returns>Returns the type of the <see cref="T:System.Data.DataRow" />.</returns>
      protected override global::System.Type GetRowType()
      {
        return typeof(GazeFixationsRow);
      }



      /// <summary>
      /// Raises the <see cref="E:System.Data.DataTable.RowChanged" /> event.
      /// </summary>
      /// <param name="e">A <see cref="T:System.Data.DataRowChangeEventArgs" /> that contains the event data.</param>
      protected override void OnRowChanged(DataRowChangeEventArgs e)
      {
        base.OnRowChanged(e);
        if ((this.GazeFixationsRowChanged != null))
        {
          this.GazeFixationsRowChanged(this, new GazeFixationsRowChangeEvent(((GazeFixationsRow)(e.Row)), e.Action));
        }
      }



      /// <summary>
      /// Raises the <see cref="E:System.Data.DataTable.RowChanging" /> event.
      /// </summary>
      /// <param name="e">A <see cref="T:System.Data.DataRowChangeEventArgs" /> that contains the event data.</param>
      protected override void OnRowChanging(DataRowChangeEventArgs e)
      {
        base.OnRowChanging(e);
        if ((this.GazeFixationsRowChanging != null))
        {
          this.GazeFixationsRowChanging(this, new GazeFixationsRowChangeEvent(((GazeFixationsRow)(e.Row)), e.Action));
        }
      }



      /// <summary>
      /// Raises the <see cref="E:System.Data.DataTable.RowDeleted" /> event.
      /// </summary>
      /// <param name="e">A <see cref="T:System.Data.DataRowChangeEventArgs" /> that contains the event data.</param>
      protected override void OnRowDeleted(DataRowChangeEventArgs e)
      {
        base.OnRowDeleted(e);
        if ((this.GazeFixationsRowDeleted != null))
        {
          this.GazeFixationsRowDeleted(this, new GazeFixationsRowChangeEvent(((GazeFixationsRow)(e.Row)), e.Action));
        }
      }



      /// <summary>
      /// Raises the <see cref="E:System.Data.DataTable.RowDeleting" /> event.
      /// </summary>
      /// <param name="e">A <see cref="T:System.Data.DataRowChangeEventArgs" /> that contains the event data.</param>
      protected override void OnRowDeleting(DataRowChangeEventArgs e)
      {
        base.OnRowDeleting(e);
        if ((this.GazeFixationsRowDeleting != null))
        {
          this.GazeFixationsRowDeleting(this, new GazeFixationsRowChangeEvent(((GazeFixationsRow)(e.Row)), e.Action));
        }
      }



      /// <summary>
      /// Removes the gaze fixations row.
      /// </summary>
      /// <param name="row">The row.</param>
      public void RemoveGazeFixationsRow(GazeFixationsRow row)
      {
        this.Rows.Remove(row);
      }



      /// <summary>
      /// Gets the typed table schema.
      /// </summary>
      /// <param name="xs">The xs.</param>
      /// <returns>System.Xml.Schema.XmlSchemaComplexType.</returns>
      public static global::System.Xml.Schema.XmlSchemaComplexType GetTypedTableSchema(global::System.Xml.Schema.XmlSchemaSet xs)
      {
        global::System.Xml.Schema.XmlSchemaComplexType type = new global::System.Xml.Schema.XmlSchemaComplexType();
        global::System.Xml.Schema.XmlSchemaSequence sequence = new global::System.Xml.Schema.XmlSchemaSequence();
        SQLiteOgamaDataSet ds = new SQLiteOgamaDataSet();
        global::System.Xml.Schema.XmlSchemaAny any1 = new global::System.Xml.Schema.XmlSchemaAny();
        any1.Namespace = "http://www.w3.org/2001/XMLSchema";
        any1.MinOccurs = new decimal(0);
        any1.MaxOccurs = decimal.MaxValue;
        any1.ProcessContents = global::System.Xml.Schema.XmlSchemaContentProcessing.Lax;
        sequence.Items.Add(any1);
        global::System.Xml.Schema.XmlSchemaAny any2 = new global::System.Xml.Schema.XmlSchemaAny();
        any2.Namespace = "urn:schemas-microsoft-com:xml-diffgram-v1";
        any2.MinOccurs = new decimal(1);
        any2.ProcessContents = global::System.Xml.Schema.XmlSchemaContentProcessing.Lax;
        sequence.Items.Add(any2);
        global::System.Xml.Schema.XmlSchemaAttribute attribute1 = new global::System.Xml.Schema.XmlSchemaAttribute();
        attribute1.Name = "namespace";
        attribute1.FixedValue = ds.Namespace;
        type.Attributes.Add(attribute1);
        global::System.Xml.Schema.XmlSchemaAttribute attribute2 = new global::System.Xml.Schema.XmlSchemaAttribute();
        attribute2.Name = "tableTypeName";
        attribute2.FixedValue = "GazeFixationsDataTable";
        type.Attributes.Add(attribute2);
        type.Particle = sequence;
        global::System.Xml.Schema.XmlSchema dsSchema = ds.GetSchemaSerializable();
        if (xs.Contains(dsSchema.TargetNamespace))
        {
          global::System.IO.MemoryStream s1 = new global::System.IO.MemoryStream();
          global::System.IO.MemoryStream s2 = new global::System.IO.MemoryStream();
          try
          {
            global::System.Xml.Schema.XmlSchema schema = null;
            dsSchema.Write(s1);
            for (global::System.Collections.IEnumerator schemas = xs.Schemas(dsSchema.TargetNamespace).GetEnumerator(); schemas.MoveNext(); )
            {
              schema = ((global::System.Xml.Schema.XmlSchema)(schemas.Current));
              s2.SetLength(0);
              schema.Write(s2);
              if ((s1.Length == s2.Length))
              {
                s1.Position = 0;
                s2.Position = 0;
                for (; ((s1.Position != s1.Length)
                            && (s1.ReadByte() == s2.ReadByte())); )
                {
                  ;
                }
                if ((s1.Position == s1.Length))
                {
                  return type;
                }
              }
            }
          }
          finally
          {
            if ((s1 != null))
            {
              s1.Close();
            }
            if ((s2 != null))
            {
              s2.Close();
            }
          }
        }
        xs.Add(dsSchema);
        return type;
      }
    }

    /// <summary>
    /// Represents the strongly named DataTable class.
    /// </summary>
    [global::System.Serializable()]
    [global::System.Xml.Serialization.XmlSchemaProviderAttribute("GetTypedTableSchema")]
    public partial class MouseFixationsDataTable : TypedTableBase<MouseFixationsRow>
    {

      /// <summary>
      /// The column identifier
      /// </summary>
      private DataColumn columnID;

      /// <summary>
      /// The column subject name
      /// </summary>
      private DataColumn columnSubjectName;

      /// <summary>
      /// The column trial identifier
      /// </summary>
      private DataColumn columnTrialID;

      /// <summary>
      /// The column trial sequence
      /// </summary>
      private DataColumn columnTrialSequence;

      /// <summary>
      /// The column count in trial
      /// </summary>
      private DataColumn columnCountInTrial;

      /// <summary>
      /// The column start time
      /// </summary>
      private DataColumn columnStartTime;

      /// <summary>
      /// The column length
      /// </summary>
      private DataColumn columnLength;

      /// <summary>
      /// The column position x
      /// </summary>
      private DataColumn columnPosX;

      /// <summary>
      /// The column position y
      /// </summary>
      private DataColumn columnPosY;



      /// <summary>
      /// Initializes a new instance of the <see cref="MouseFixationsDataTable"/> class.
      /// </summary>
      public MouseFixationsDataTable()
      {
        this.TableName = "MouseFixations";
        this.BeginInit();
        this.InitClass();
        this.EndInit();
      }



      /// <summary>
      /// Initializes a new instance of the <see cref="MouseFixationsDataTable"/> class.
      /// </summary>
      /// <param name="table">The table.</param>
      internal MouseFixationsDataTable(DataTable table)
      {
        this.TableName = table.TableName;
        if ((table.CaseSensitive != table.DataSet.CaseSensitive))
        {
          this.CaseSensitive = table.CaseSensitive;
        }
        if ((table.Locale.ToString() != table.DataSet.Locale.ToString()))
        {
          this.Locale = table.Locale;
        }
        if ((table.Namespace != table.DataSet.Namespace))
        {
          this.Namespace = table.Namespace;
        }
        this.Prefix = table.Prefix;
        this.MinimumCapacity = table.MinimumCapacity;
      }



      /// <summary>
      /// Initializes a new instance of the <see cref="MouseFixationsDataTable"/> class.
      /// </summary>
      /// <param name="info">The information.</param>
      /// <param name="context">The context.</param>
      protected MouseFixationsDataTable(global::System.Runtime.Serialization.SerializationInfo info, global::System.Runtime.Serialization.StreamingContext context) :
        base(info, context)
      {
        this.InitVars();
      }



      /// <summary>
      /// Gets the identifier column.
      /// </summary>
      /// <value>The identifier column.</value>
      public DataColumn IDColumn
      {
        get
        {
          return this.columnID;
        }
      }



      /// <summary>
      /// Gets the subject name column.
      /// </summary>
      /// <value>The subject name column.</value>
      public DataColumn SubjectNameColumn
      {
        get
        {
          return this.columnSubjectName;
        }
      }



      /// <summary>
      /// Gets the trial identifier column.
      /// </summary>
      /// <value>The trial identifier column.</value>
      public DataColumn TrialIDColumn
      {
        get
        {
          return this.columnTrialID;
        }
      }



      /// <summary>
      /// Gets the trial sequence column.
      /// </summary>
      /// <value>The trial sequence column.</value>
      public DataColumn TrialSequenceColumn
      {
        get
        {
          return this.columnTrialSequence;
        }
      }



      /// <summary>
      /// Gets the count in trial column.
      /// </summary>
      /// <value>The count in trial column.</value>
      public DataColumn CountInTrialColumn
      {
        get
        {
          return this.columnCountInTrial;
        }
      }



      /// <summary>
      /// Gets the start time column.
      /// </summary>
      /// <value>The start time column.</value>
      public DataColumn StartTimeColumn
      {
        get
        {
          return this.columnStartTime;
        }
      }



      /// <summary>
      /// Gets the length column.
      /// </summary>
      /// <value>The length column.</value>
      public DataColumn LengthColumn
      {
        get
        {
          return this.columnLength;
        }
      }



      /// <summary>
      /// Gets the position x column.
      /// </summary>
      /// <value>The position x column.</value>
      public DataColumn PosXColumn
      {
        get
        {
          return this.columnPosX;
        }
      }



      /// <summary>
      /// Gets the position y column.
      /// </summary>
      /// <value>The position y column.</value>
      public DataColumn PosYColumn
      {
        get
        {
          return this.columnPosY;
        }
      }




      /// <summary>
      /// Gets the count.
      /// </summary>
      /// <value>The count.</value>
      public int Count
      {
        get
        {
          return this.Rows.Count;
        }
      }



      /// <summary>
      /// Gets the <see cref="MouseFixationsRow"/> at the specified index.
      /// </summary>
      /// <param name="index">The index.</param>
      /// <returns>MouseFixationsRow.</returns>
      public MouseFixationsRow this[int index]
      {
        get
        {
          return ((MouseFixationsRow)(this.Rows[index]));
        }
      }


      /// <summary>
      /// Occurs when [mouse fixations row changing].
      /// </summary>
      public event MouseFixationsRowChangeEventHandler MouseFixationsRowChanging;


      /// <summary>
      /// Occurs when [mouse fixations row changed].
      /// </summary>
      public event MouseFixationsRowChangeEventHandler MouseFixationsRowChanged;


      /// <summary>
      /// Occurs when [mouse fixations row deleting].
      /// </summary>
      public event MouseFixationsRowChangeEventHandler MouseFixationsRowDeleting;


      /// <summary>
      /// Occurs when [mouse fixations row deleted].
      /// </summary>
      public event MouseFixationsRowChangeEventHandler MouseFixationsRowDeleted;



      /// <summary>
      /// Adds the mouse fixations row.
      /// </summary>
      /// <param name="row">The row.</param>
      public void AddMouseFixationsRow(MouseFixationsRow row)
      {
        this.Rows.Add(row);
      }



      /// <summary>
      /// Adds the mouse fixations row.
      /// </summary>
      /// <param name="SubjectName">Name of the subject.</param>
      /// <param name="TrialID">The trial identifier.</param>
      /// <param name="TrialSequence">The trial sequence.</param>
      /// <param name="CountInTrial">The count in trial.</param>
      /// <param name="StartTime">The start time.</param>
      /// <param name="Length">The length.</param>
      /// <param name="PosX">The position x.</param>
      /// <param name="PosY">The position y.</param>
      /// <returns>MouseFixationsRow.</returns>
      public MouseFixationsRow AddMouseFixationsRow(string SubjectName, int TrialID, int TrialSequence, int CountInTrial, long StartTime, int Length, double PosX, double PosY)
      {
        MouseFixationsRow rowMouseFixationsRow = ((MouseFixationsRow)(this.NewRow()));
        object[] columnValuesArray = new object[] {
                        null,
                        SubjectName,
                        TrialID,
                        TrialSequence,
                        CountInTrial,
                        StartTime,
                        Length,
                        PosX,
                        PosY};
        rowMouseFixationsRow.ItemArray = columnValuesArray;
        this.Rows.Add(rowMouseFixationsRow);
        return rowMouseFixationsRow;
      }



      /// <summary>
      /// Finds the by identifier.
      /// </summary>
      /// <param name="ID">The identifier.</param>
      /// <returns>MouseFixationsRow.</returns>
      public MouseFixationsRow FindByID(long ID)
      {
        return ((MouseFixationsRow)(this.Rows.Find(new object[] {
                            ID})));
      }



      /// <summary>
      /// Clones the structure of the <see cref="T:System.Data.DataTable" />, including all <see cref="T:System.Data.DataTable" /> schemas and constraints.
      /// </summary>
      /// <returns>A new <see cref="T:System.Data.DataTable" /> with the same schema as the current <see cref="T:System.Data.DataTable" />.</returns>
      /// <PermissionSet>
      ///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
      ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
      ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
      /// </PermissionSet>
      public override DataTable Clone()
      {
        MouseFixationsDataTable cln = ((MouseFixationsDataTable)(base.Clone()));
        cln.InitVars();
        return cln;
      }



      /// <summary>
      /// Creates a new instance of <see cref="T:System.Data.DataTable" />.
      /// </summary>
      /// <returns>The new expression.</returns>
      protected override DataTable CreateInstance()
      {
        return new MouseFixationsDataTable();
      }



      /// <summary>
      /// Initializes the vars.
      /// </summary>
      internal void InitVars()
      {
        this.columnID = base.Columns["ID"];
        this.columnSubjectName = base.Columns["SubjectName"];
        this.columnTrialID = base.Columns["TrialID"];
        this.columnTrialSequence = base.Columns["TrialSequence"];
        this.columnCountInTrial = base.Columns["CountInTrial"];
        this.columnStartTime = base.Columns["StartTime"];
        this.columnLength = base.Columns["Length"];
        this.columnPosX = base.Columns["PosX"];
        this.columnPosY = base.Columns["PosY"];
      }



      /// <summary>
      /// Initializes the class.
      /// </summary>
      private void InitClass()
      {
        this.columnID = new DataColumn("ID", typeof(long), null, MappingType.Element);
        base.Columns.Add(this.columnID);
        this.columnSubjectName = new DataColumn("SubjectName", typeof(string), null, MappingType.Element);
        base.Columns.Add(this.columnSubjectName);
        this.columnTrialID = new DataColumn("TrialID", typeof(int), null, MappingType.Element);
        base.Columns.Add(this.columnTrialID);
        this.columnTrialSequence = new DataColumn("TrialSequence", typeof(int), null, MappingType.Element);
        base.Columns.Add(this.columnTrialSequence);
        this.columnCountInTrial = new DataColumn("CountInTrial", typeof(int), null, MappingType.Element);
        base.Columns.Add(this.columnCountInTrial);
        this.columnStartTime = new DataColumn("StartTime", typeof(long), null, MappingType.Element);
        base.Columns.Add(this.columnStartTime);
        this.columnLength = new DataColumn("Length", typeof(int), null, MappingType.Element);
        base.Columns.Add(this.columnLength);
        this.columnPosX = new DataColumn("PosX", typeof(double), null, MappingType.Element);
        base.Columns.Add(this.columnPosX);
        this.columnPosY = new DataColumn("PosY", typeof(double), null, MappingType.Element);
        base.Columns.Add(this.columnPosY);
        this.Constraints.Add(new UniqueConstraint("Constraint1", new DataColumn[] {
                                this.columnID}, true));
        this.columnID.AutoIncrement = true;
        this.columnID.AutoIncrementSeed = 1;
        this.columnID.AllowDBNull = false;
        this.columnID.ReadOnly = true;
        this.columnID.Unique = true;
        this.columnSubjectName.AllowDBNull = false;
        this.columnSubjectName.MaxLength = 50;
        this.columnTrialID.AllowDBNull = false;
        this.columnTrialSequence.AllowDBNull = false;
      }



      /// <summary>
      /// News the mouse fixations row.
      /// </summary>
      /// <returns>MouseFixationsRow.</returns>
      public MouseFixationsRow NewMouseFixationsRow()
      {
        return ((MouseFixationsRow)(this.NewRow()));
      }



      /// <summary>
      /// Creates a new row from an existing row.
      /// </summary>
      /// <param name="builder">A <see cref="T:System.Data.DataRowBuilder" /> object.</param>
      /// <returns>A <see cref="T:System.Data.DataRow" /> derived class.</returns>
      protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
      {
        return new MouseFixationsRow(builder);
      }



      /// <summary>
      /// Gets the row type.
      /// </summary>
      /// <returns>Returns the type of the <see cref="T:System.Data.DataRow" />.</returns>
      protected override global::System.Type GetRowType()
      {
        return typeof(MouseFixationsRow);
      }



      /// <summary>
      /// Raises the <see cref="E:System.Data.DataTable.RowChanged" /> event.
      /// </summary>
      /// <param name="e">A <see cref="T:System.Data.DataRowChangeEventArgs" /> that contains the event data.</param>
      protected override void OnRowChanged(DataRowChangeEventArgs e)
      {
        base.OnRowChanged(e);
        if ((this.MouseFixationsRowChanged != null))
        {
          this.MouseFixationsRowChanged(this, new MouseFixationsRowChangeEvent(((MouseFixationsRow)(e.Row)), e.Action));
        }
      }



      /// <summary>
      /// Raises the <see cref="E:System.Data.DataTable.RowChanging" /> event.
      /// </summary>
      /// <param name="e">A <see cref="T:System.Data.DataRowChangeEventArgs" /> that contains the event data.</param>
      protected override void OnRowChanging(DataRowChangeEventArgs e)
      {
        base.OnRowChanging(e);
        if ((this.MouseFixationsRowChanging != null))
        {
          this.MouseFixationsRowChanging(this, new MouseFixationsRowChangeEvent(((MouseFixationsRow)(e.Row)), e.Action));
        }
      }



      /// <summary>
      /// Raises the <see cref="E:System.Data.DataTable.RowDeleted" /> event.
      /// </summary>
      /// <param name="e">A <see cref="T:System.Data.DataRowChangeEventArgs" /> that contains the event data.</param>
      protected override void OnRowDeleted(DataRowChangeEventArgs e)
      {
        base.OnRowDeleted(e);
        if ((this.MouseFixationsRowDeleted != null))
        {
          this.MouseFixationsRowDeleted(this, new MouseFixationsRowChangeEvent(((MouseFixationsRow)(e.Row)), e.Action));
        }
      }



      /// <summary>
      /// Raises the <see cref="E:System.Data.DataTable.RowDeleting" /> event.
      /// </summary>
      /// <param name="e">A <see cref="T:System.Data.DataRowChangeEventArgs" /> that contains the event data.</param>
      protected override void OnRowDeleting(DataRowChangeEventArgs e)
      {
        base.OnRowDeleting(e);
        if ((this.MouseFixationsRowDeleting != null))
        {
          this.MouseFixationsRowDeleting(this, new MouseFixationsRowChangeEvent(((MouseFixationsRow)(e.Row)), e.Action));
        }
      }



      /// <summary>
      /// Removes the mouse fixations row.
      /// </summary>
      /// <param name="row">The row.</param>
      public void RemoveMouseFixationsRow(MouseFixationsRow row)
      {
        this.Rows.Remove(row);
      }



      /// <summary>
      /// Gets the typed table schema.
      /// </summary>
      /// <param name="xs">The xs.</param>
      /// <returns>System.Xml.Schema.XmlSchemaComplexType.</returns>
      public static global::System.Xml.Schema.XmlSchemaComplexType GetTypedTableSchema(global::System.Xml.Schema.XmlSchemaSet xs)
      {
        global::System.Xml.Schema.XmlSchemaComplexType type = new global::System.Xml.Schema.XmlSchemaComplexType();
        global::System.Xml.Schema.XmlSchemaSequence sequence = new global::System.Xml.Schema.XmlSchemaSequence();
        SQLiteOgamaDataSet ds = new SQLiteOgamaDataSet();
        global::System.Xml.Schema.XmlSchemaAny any1 = new global::System.Xml.Schema.XmlSchemaAny();
        any1.Namespace = "http://www.w3.org/2001/XMLSchema";
        any1.MinOccurs = new decimal(0);
        any1.MaxOccurs = decimal.MaxValue;
        any1.ProcessContents = global::System.Xml.Schema.XmlSchemaContentProcessing.Lax;
        sequence.Items.Add(any1);
        global::System.Xml.Schema.XmlSchemaAny any2 = new global::System.Xml.Schema.XmlSchemaAny();
        any2.Namespace = "urn:schemas-microsoft-com:xml-diffgram-v1";
        any2.MinOccurs = new decimal(1);
        any2.ProcessContents = global::System.Xml.Schema.XmlSchemaContentProcessing.Lax;
        sequence.Items.Add(any2);
        global::System.Xml.Schema.XmlSchemaAttribute attribute1 = new global::System.Xml.Schema.XmlSchemaAttribute();
        attribute1.Name = "namespace";
        attribute1.FixedValue = ds.Namespace;
        type.Attributes.Add(attribute1);
        global::System.Xml.Schema.XmlSchemaAttribute attribute2 = new global::System.Xml.Schema.XmlSchemaAttribute();
        attribute2.Name = "tableTypeName";
        attribute2.FixedValue = "MouseFixationsDataTable";
        type.Attributes.Add(attribute2);
        type.Particle = sequence;
        global::System.Xml.Schema.XmlSchema dsSchema = ds.GetSchemaSerializable();
        if (xs.Contains(dsSchema.TargetNamespace))
        {
          global::System.IO.MemoryStream s1 = new global::System.IO.MemoryStream();
          global::System.IO.MemoryStream s2 = new global::System.IO.MemoryStream();
          try
          {
            global::System.Xml.Schema.XmlSchema schema = null;
            dsSchema.Write(s1);
            for (global::System.Collections.IEnumerator schemas = xs.Schemas(dsSchema.TargetNamespace).GetEnumerator(); schemas.MoveNext(); )
            {
              schema = ((global::System.Xml.Schema.XmlSchema)(schemas.Current));
              s2.SetLength(0);
              schema.Write(s2);
              if ((s1.Length == s2.Length))
              {
                s1.Position = 0;
                s2.Position = 0;
                for (; ((s1.Position != s1.Length)
                            && (s1.ReadByte() == s2.ReadByte())); )
                {
                  ;
                }
                if ((s1.Position == s1.Length))
                {
                  return type;
                }
              }
            }
          }
          finally
          {
            if ((s1 != null))
            {
              s1.Close();
            }
            if ((s2 != null))
            {
              s2.Close();
            }
          }
        }
        xs.Add(dsSchema);
        return type;
      }
    }

    /// <summary>
    /// Represents the strongly named DataTable class.
    /// </summary>
    [global::System.Serializable()]
    [global::System.Xml.Serialization.XmlSchemaProviderAttribute("GetTypedTableSchema")]
    public partial class ParamsDataTable : TypedTableBase<ParamsRow>
    {

      /// <summary>
      /// The column identifier
      /// </summary>
      private DataColumn columnID;

      /// <summary>
      /// The column parameter
      /// </summary>
      private DataColumn columnParam;

      /// <summary>
      /// The column description
      /// </summary>
      private DataColumn columnDescription;



      /// <summary>
      /// Initializes a new instance of the <see cref="ParamsDataTable"/> class.
      /// </summary>
      public ParamsDataTable()
      {
        this.TableName = "Params";
        this.BeginInit();
        this.InitClass();
        this.EndInit();
      }



      /// <summary>
      /// Initializes a new instance of the <see cref="ParamsDataTable"/> class.
      /// </summary>
      /// <param name="table">The table.</param>
      internal ParamsDataTable(DataTable table)
      {
        this.TableName = table.TableName;
        if ((table.CaseSensitive != table.DataSet.CaseSensitive))
        {
          this.CaseSensitive = table.CaseSensitive;
        }
        if ((table.Locale.ToString() != table.DataSet.Locale.ToString()))
        {
          this.Locale = table.Locale;
        }
        if ((table.Namespace != table.DataSet.Namespace))
        {
          this.Namespace = table.Namespace;
        }
        this.Prefix = table.Prefix;
        this.MinimumCapacity = table.MinimumCapacity;
      }



      /// <summary>
      /// Initializes a new instance of the <see cref="ParamsDataTable"/> class.
      /// </summary>
      /// <param name="info">The information.</param>
      /// <param name="context">The context.</param>
      protected ParamsDataTable(global::System.Runtime.Serialization.SerializationInfo info, global::System.Runtime.Serialization.StreamingContext context) :
        base(info, context)
      {
        this.InitVars();
      }



      /// <summary>
      /// Gets the identifier column.
      /// </summary>
      /// <value>The identifier column.</value>
      public DataColumn IDColumn
      {
        get
        {
          return this.columnID;
        }
      }



      /// <summary>
      /// Gets the parameter column.
      /// </summary>
      /// <value>The parameter column.</value>
      public DataColumn ParamColumn
      {
        get
        {
          return this.columnParam;
        }
      }



      /// <summary>
      /// Gets the description column.
      /// </summary>
      /// <value>The description column.</value>
      public DataColumn DescriptionColumn
      {
        get
        {
          return this.columnDescription;
        }
      }




      /// <summary>
      /// Gets the count.
      /// </summary>
      /// <value>The count.</value>
      public int Count
      {
        get
        {
          return this.Rows.Count;
        }
      }



      /// <summary>
      /// Gets the <see cref="ParamsRow"/> at the specified index.
      /// </summary>
      /// <param name="index">The index.</param>
      /// <returns>ParamsRow.</returns>
      public ParamsRow this[int index]
      {
        get
        {
          return ((ParamsRow)(this.Rows[index]));
        }
      }


      /// <summary>
      /// Occurs when [parameters row changing].
      /// </summary>
      public event ParamsRowChangeEventHandler ParamsRowChanging;


      /// <summary>
      /// Occurs when [parameters row changed].
      /// </summary>
      public event ParamsRowChangeEventHandler ParamsRowChanged;


      /// <summary>
      /// Occurs when [parameters row deleting].
      /// </summary>
      public event ParamsRowChangeEventHandler ParamsRowDeleting;


      /// <summary>
      /// Occurs when [parameters row deleted].
      /// </summary>
      public event ParamsRowChangeEventHandler ParamsRowDeleted;



      /// <summary>
      /// Adds the parameters row.
      /// </summary>
      /// <param name="row">The row.</param>
      public void AddParamsRow(ParamsRow row)
      {
        this.Rows.Add(row);
      }



      /// <summary>
      /// Adds the parameters row.
      /// </summary>
      /// <param name="Param">The parameter.</param>
      /// <param name="Description">The description.</param>
      /// <returns>ParamsRow.</returns>
      public ParamsRow AddParamsRow(string Param, string Description)
      {
        ParamsRow rowParamsRow = ((ParamsRow)(this.NewRow()));
        object[] columnValuesArray = new object[] {
                        null,
                        Param,
                        Description};
        rowParamsRow.ItemArray = columnValuesArray;
        this.Rows.Add(rowParamsRow);
        return rowParamsRow;
      }



      /// <summary>
      /// Finds the by identifier.
      /// </summary>
      /// <param name="ID">The identifier.</param>
      /// <returns>ParamsRow.</returns>
      public ParamsRow FindByID(int ID)
      {
        return ((ParamsRow)(this.Rows.Find(new object[] {
                            ID})));
      }



      /// <summary>
      /// Clones the structure of the <see cref="T:System.Data.DataTable" />, including all <see cref="T:System.Data.DataTable" /> schemas and constraints.
      /// </summary>
      /// <returns>A new <see cref="T:System.Data.DataTable" /> with the same schema as the current <see cref="T:System.Data.DataTable" />.</returns>
      /// <PermissionSet>
      ///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
      ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
      ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
      /// </PermissionSet>
      public override DataTable Clone()
      {
        ParamsDataTable cln = ((ParamsDataTable)(base.Clone()));
        cln.InitVars();
        return cln;
      }



      /// <summary>
      /// Creates a new instance of <see cref="T:System.Data.DataTable" />.
      /// </summary>
      /// <returns>The new expression.</returns>
      protected override DataTable CreateInstance()
      {
        return new ParamsDataTable();
      }



      /// <summary>
      /// Initializes the vars.
      /// </summary>
      internal void InitVars()
      {
        this.columnID = base.Columns["ID"];
        this.columnParam = base.Columns["Param"];
        this.columnDescription = base.Columns["Description"];
      }



      /// <summary>
      /// Initializes the class.
      /// </summary>
      private void InitClass()
      {
        this.columnID = new DataColumn("ID", typeof(int), null, MappingType.Element);
        base.Columns.Add(this.columnID);
        this.columnParam = new DataColumn("Param", typeof(string), null, MappingType.Element);
        base.Columns.Add(this.columnParam);
        this.columnDescription = new DataColumn("Description", typeof(string), null, MappingType.Element);
        base.Columns.Add(this.columnDescription);
        this.Constraints.Add(new UniqueConstraint("Constraint1", new DataColumn[] {
                                this.columnID}, true));
        this.Constraints.Add(new UniqueConstraint("Constraint2", new DataColumn[] {
                                this.columnParam}, false));
        this.columnID.AutoIncrement = true;
        this.columnID.AutoIncrementSeed = 1;
        this.columnID.AllowDBNull = false;
        this.columnID.ReadOnly = true;
        this.columnID.Unique = true;
        this.columnParam.AllowDBNull = false;
        this.columnParam.Unique = true;
        this.columnParam.MaxLength = 50;
        this.columnDescription.MaxLength = 400;
      }



      /// <summary>
      /// News the parameters row.
      /// </summary>
      /// <returns>ParamsRow.</returns>
      public ParamsRow NewParamsRow()
      {
        return ((ParamsRow)(this.NewRow()));
      }



      /// <summary>
      /// Creates a new row from an existing row.
      /// </summary>
      /// <param name="builder">A <see cref="T:System.Data.DataRowBuilder" /> object.</param>
      /// <returns>A <see cref="T:System.Data.DataRow" /> derived class.</returns>
      protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
      {
        return new ParamsRow(builder);
      }



      /// <summary>
      /// Gets the row type.
      /// </summary>
      /// <returns>Returns the type of the <see cref="T:System.Data.DataRow" />.</returns>
      protected override global::System.Type GetRowType()
      {
        return typeof(ParamsRow);
      }



      /// <summary>
      /// Raises the <see cref="E:System.Data.DataTable.RowChanged" /> event.
      /// </summary>
      /// <param name="e">A <see cref="T:System.Data.DataRowChangeEventArgs" /> that contains the event data.</param>
      protected override void OnRowChanged(DataRowChangeEventArgs e)
      {
        base.OnRowChanged(e);
        if ((this.ParamsRowChanged != null))
        {
          this.ParamsRowChanged(this, new ParamsRowChangeEvent(((ParamsRow)(e.Row)), e.Action));
        }
      }



      /// <summary>
      /// Raises the <see cref="E:System.Data.DataTable.RowChanging" /> event.
      /// </summary>
      /// <param name="e">A <see cref="T:System.Data.DataRowChangeEventArgs" /> that contains the event data.</param>
      protected override void OnRowChanging(DataRowChangeEventArgs e)
      {
        base.OnRowChanging(e);
        if ((this.ParamsRowChanging != null))
        {
          this.ParamsRowChanging(this, new ParamsRowChangeEvent(((ParamsRow)(e.Row)), e.Action));
        }
      }



      /// <summary>
      /// Raises the <see cref="E:System.Data.DataTable.RowDeleted" /> event.
      /// </summary>
      /// <param name="e">A <see cref="T:System.Data.DataRowChangeEventArgs" /> that contains the event data.</param>
      protected override void OnRowDeleted(DataRowChangeEventArgs e)
      {
        base.OnRowDeleted(e);
        if ((this.ParamsRowDeleted != null))
        {
          this.ParamsRowDeleted(this, new ParamsRowChangeEvent(((ParamsRow)(e.Row)), e.Action));
        }
      }



      /// <summary>
      /// Raises the <see cref="E:System.Data.DataTable.RowDeleting" /> event.
      /// </summary>
      /// <param name="e">A <see cref="T:System.Data.DataRowChangeEventArgs" /> that contains the event data.</param>
      protected override void OnRowDeleting(DataRowChangeEventArgs e)
      {
        base.OnRowDeleting(e);
        if ((this.ParamsRowDeleting != null))
        {
          this.ParamsRowDeleting(this, new ParamsRowChangeEvent(((ParamsRow)(e.Row)), e.Action));
        }
      }



      /// <summary>
      /// Removes the parameters row.
      /// </summary>
      /// <param name="row">The row.</param>
      public void RemoveParamsRow(ParamsRow row)
      {
        this.Rows.Remove(row);
      }



      /// <summary>
      /// Gets the typed table schema.
      /// </summary>
      /// <param name="xs">The xs.</param>
      /// <returns>System.Xml.Schema.XmlSchemaComplexType.</returns>
      public static global::System.Xml.Schema.XmlSchemaComplexType GetTypedTableSchema(global::System.Xml.Schema.XmlSchemaSet xs)
      {
        global::System.Xml.Schema.XmlSchemaComplexType type = new global::System.Xml.Schema.XmlSchemaComplexType();
        global::System.Xml.Schema.XmlSchemaSequence sequence = new global::System.Xml.Schema.XmlSchemaSequence();
        SQLiteOgamaDataSet ds = new SQLiteOgamaDataSet();
        global::System.Xml.Schema.XmlSchemaAny any1 = new global::System.Xml.Schema.XmlSchemaAny();
        any1.Namespace = "http://www.w3.org/2001/XMLSchema";
        any1.MinOccurs = new decimal(0);
        any1.MaxOccurs = decimal.MaxValue;
        any1.ProcessContents = global::System.Xml.Schema.XmlSchemaContentProcessing.Lax;
        sequence.Items.Add(any1);
        global::System.Xml.Schema.XmlSchemaAny any2 = new global::System.Xml.Schema.XmlSchemaAny();
        any2.Namespace = "urn:schemas-microsoft-com:xml-diffgram-v1";
        any2.MinOccurs = new decimal(1);
        any2.ProcessContents = global::System.Xml.Schema.XmlSchemaContentProcessing.Lax;
        sequence.Items.Add(any2);
        global::System.Xml.Schema.XmlSchemaAttribute attribute1 = new global::System.Xml.Schema.XmlSchemaAttribute();
        attribute1.Name = "namespace";
        attribute1.FixedValue = ds.Namespace;
        type.Attributes.Add(attribute1);
        global::System.Xml.Schema.XmlSchemaAttribute attribute2 = new global::System.Xml.Schema.XmlSchemaAttribute();
        attribute2.Name = "tableTypeName";
        attribute2.FixedValue = "ParamsDataTable";
        type.Attributes.Add(attribute2);
        type.Particle = sequence;
        global::System.Xml.Schema.XmlSchema dsSchema = ds.GetSchemaSerializable();
        if (xs.Contains(dsSchema.TargetNamespace))
        {
          global::System.IO.MemoryStream s1 = new global::System.IO.MemoryStream();
          global::System.IO.MemoryStream s2 = new global::System.IO.MemoryStream();
          try
          {
            global::System.Xml.Schema.XmlSchema schema = null;
            dsSchema.Write(s1);
            for (global::System.Collections.IEnumerator schemas = xs.Schemas(dsSchema.TargetNamespace).GetEnumerator(); schemas.MoveNext(); )
            {
              schema = ((global::System.Xml.Schema.XmlSchema)(schemas.Current));
              s2.SetLength(0);
              schema.Write(s2);
              if ((s1.Length == s2.Length))
              {
                s1.Position = 0;
                s2.Position = 0;
                for (; ((s1.Position != s1.Length)
                            && (s1.ReadByte() == s2.ReadByte())); )
                {
                  ;
                }
                if ((s1.Position == s1.Length))
                {
                  return type;
                }
              }
            }
          }
          finally
          {
            if ((s1 != null))
            {
              s1.Close();
            }
            if ((s2 != null))
            {
              s2.Close();
            }
          }
        }
        xs.Add(dsSchema);
        return type;
      }
    }

    /// <summary>
    /// Represents strongly named DataRow class.
    /// </summary>
    public partial class SubjectParametersRow : DataRow
    {

      /// <summary>
      /// The table subject parameters
      /// </summary>
      private SubjectParametersDataTable tableSubjectParameters;



      /// <summary>
      /// Initializes a new instance of the <see cref="SubjectParametersRow"/> class.
      /// </summary>
      /// <param name="rb">The rb.</param>
      internal SubjectParametersRow(DataRowBuilder rb) :
        base(rb)
      {
        this.tableSubjectParameters = ((SubjectParametersDataTable)(this.Table));
      }



      /// <summary>
      /// Gets or sets the identifier.
      /// </summary>
      /// <value>The identifier.</value>
      public int ID
      {
        get
        {
          return ((int)(this[this.tableSubjectParameters.IDColumn]));
        }
        set
        {
          this[this.tableSubjectParameters.IDColumn] = value;
        }
      }



      /// <summary>
      /// Gets or sets the name of the subject.
      /// </summary>
      /// <value>The name of the subject.</value>
      public string SubjectName
      {
        get
        {
          return ((string)(this[this.tableSubjectParameters.SubjectNameColumn]));
        }
        set
        {
          this[this.tableSubjectParameters.SubjectNameColumn] = value;
        }
      }



      /// <summary>
      /// Gets or sets the parameter.
      /// </summary>
      /// <value>The parameter.</value>
      public string Param
      {
        get
        {
          return ((string)(this[this.tableSubjectParameters.ParamColumn]));
        }
        set
        {
          this[this.tableSubjectParameters.ParamColumn] = value;
        }
      }



      /// <summary>
      /// Gets or sets the parameter value.
      /// </summary>
      /// <value>The parameter value.</value>
      /// <exception cref="System.Data.StrongTypingException">The value for column \'ParamValue\' in table \'SubjectParameters\' is DBNull.</exception>
      public string ParamValue
      {
        get
        {
          try
          {
            return ((string)(this[this.tableSubjectParameters.ParamValueColumn]));
          }
          catch (global::System.InvalidCastException e)
          {
            throw new StrongTypingException("The value for column \'ParamValue\' in table \'SubjectParameters\' is DBNull.", e);
          }
        }
        set
        {
          this[this.tableSubjectParameters.ParamValueColumn] = value;
        }
      }



      /// <summary>
      /// Gets or sets the subjects row.
      /// </summary>
      /// <value>The subjects row.</value>
      public SubjectsRow SubjectsRow
      {
        get
        {
          return ((SubjectsRow)(this.GetParentRow(this.Table.ParentRelations["FK_Subjects_SubjectParameters"])));
        }
        set
        {
          this.SetParentRow(value, this.Table.ParentRelations["FK_Subjects_SubjectParameters"]);
        }
      }



      /// <summary>
      /// Determines whether [is parameter value null].
      /// </summary>
      /// <returns><c>true</c> if [is parameter value null]; otherwise, <c>false</c>.</returns>
      public bool IsParamValueNull()
      {
        return this.IsNull(this.tableSubjectParameters.ParamValueColumn);
      }



      /// <summary>
      /// Sets the parameter value null.
      /// </summary>
      public void SetParamValueNull()
      {
        this[this.tableSubjectParameters.ParamValueColumn] = global::System.Convert.DBNull;
      }
    }

    /// <summary>
    /// Represents strongly named DataRow class.
    /// </summary>
    public partial class SubjectsRow : DataRow
    {

      /// <summary>
      /// The table subjects
      /// </summary>
      private SubjectsDataTable tableSubjects;



      /// <summary>
      /// Initializes a new instance of the <see cref="SubjectsRow"/> class.
      /// </summary>
      /// <param name="rb">The rb.</param>
      internal SubjectsRow(DataRowBuilder rb) :
        base(rb)
      {
        this.tableSubjects = ((SubjectsDataTable)(this.Table));
      }



      /// <summary>
      /// Gets or sets the identifier.
      /// </summary>
      /// <value>The identifier.</value>
      public long ID
      {
        get
        {
          return ((long)(this[this.tableSubjects.IDColumn]));
        }
        set
        {
          this[this.tableSubjects.IDColumn] = value;
        }
      }



      /// <summary>
      /// Gets or sets the name of the subject.
      /// </summary>
      /// <value>The name of the subject.</value>
      public string SubjectName
      {
        get
        {
          return ((string)(this[this.tableSubjects.SubjectNameColumn]));
        }
        set
        {
          this[this.tableSubjects.SubjectNameColumn] = value;
        }
      }



      /// <summary>
      /// Gets or sets the category.
      /// </summary>
      /// <value>The category.</value>
      /// <exception cref="System.Data.StrongTypingException">The value for column \'Category\' in table \'Subjects\' is DBNull.</exception>
      public string Category
      {
        get
        {
          try
          {
            return ((string)(this[this.tableSubjects.CategoryColumn]));
          }
          catch (global::System.InvalidCastException e)
          {
            throw new StrongTypingException("The value for column \'Category\' in table \'Subjects\' is DBNull.", e);
          }
        }
        set
        {
          this[this.tableSubjects.CategoryColumn] = value;
        }
      }



      /// <summary>
      /// Gets or sets the age.
      /// </summary>
      /// <value>The age.</value>
      /// <exception cref="System.Data.StrongTypingException">The value for column \'Age\' in table \'Subjects\' is DBNull.</exception>
      public int Age
      {
        get
        {
          try
          {
            return ((int)(this[this.tableSubjects.AgeColumn]));
          }
          catch (global::System.InvalidCastException e)
          {
            throw new StrongTypingException("The value for column \'Age\' in table \'Subjects\' is DBNull.", e);
          }
        }
        set
        {
          this[this.tableSubjects.AgeColumn] = value;
        }
      }



      /// <summary>
      /// Gets or sets the sex.
      /// </summary>
      /// <value>The sex.</value>
      /// <exception cref="System.Data.StrongTypingException">The value for column \'Sex\' in table \'Subjects\' is DBNull.</exception>
      public string Sex
      {
        get
        {
          try
          {
            return ((string)(this[this.tableSubjects.SexColumn]));
          }
          catch (global::System.InvalidCastException e)
          {
            throw new StrongTypingException("The value for column \'Sex\' in table \'Subjects\' is DBNull.", e);
          }
        }
        set
        {
          this[this.tableSubjects.SexColumn] = value;
        }
      }



      /// <summary>
      /// Gets or sets the handedness.
      /// </summary>
      /// <value>The handedness.</value>
      /// <exception cref="System.Data.StrongTypingException">The value for column \'Handedness\' in table \'Subjects\' is DBNull.</exception>
      public string Handedness
      {
        get
        {
          try
          {
            return ((string)(this[this.tableSubjects.HandednessColumn]));
          }
          catch (global::System.InvalidCastException e)
          {
            throw new StrongTypingException("The value for column \'Handedness\' in table \'Subjects\' is DBNull.", e);
          }
        }
        set
        {
          this[this.tableSubjects.HandednessColumn] = value;
        }
      }



      /// <summary>
      /// Gets or sets the comments.
      /// </summary>
      /// <value>The comments.</value>
      /// <exception cref="System.Data.StrongTypingException">The value for column \'Comments\' in table \'Subjects\' is DBNull.</exception>
      public string Comments
      {
        get
        {
          try
          {
            return ((string)(this[this.tableSubjects.CommentsColumn]));
          }
          catch (global::System.InvalidCastException e)
          {
            throw new StrongTypingException("The value for column \'Comments\' in table \'Subjects\' is DBNull.", e);
          }
        }
        set
        {
          this[this.tableSubjects.CommentsColumn] = value;
        }
      }



      /// <summary>
      /// Determines whether [is category null].
      /// </summary>
      /// <returns><c>true</c> if [is category null]; otherwise, <c>false</c>.</returns>
      public bool IsCategoryNull()
      {
        return this.IsNull(this.tableSubjects.CategoryColumn);
      }



      /// <summary>
      /// Sets the category null.
      /// </summary>
      public void SetCategoryNull()
      {
        this[this.tableSubjects.CategoryColumn] = global::System.Convert.DBNull;
      }



      /// <summary>
      /// Determines whether [is age null].
      /// </summary>
      /// <returns><c>true</c> if [is age null]; otherwise, <c>false</c>.</returns>
      public bool IsAgeNull()
      {
        return this.IsNull(this.tableSubjects.AgeColumn);
      }



      /// <summary>
      /// Sets the age null.
      /// </summary>
      public void SetAgeNull()
      {
        this[this.tableSubjects.AgeColumn] = global::System.Convert.DBNull;
      }



      /// <summary>
      /// Determines whether [is sex null].
      /// </summary>
      /// <returns><c>true</c> if [is sex null]; otherwise, <c>false</c>.</returns>
      public bool IsSexNull()
      {
        return this.IsNull(this.tableSubjects.SexColumn);
      }



      /// <summary>
      /// Sets the sex null.
      /// </summary>
      public void SetSexNull()
      {
        this[this.tableSubjects.SexColumn] = global::System.Convert.DBNull;
      }



      /// <summary>
      /// Determines whether [is handedness null].
      /// </summary>
      /// <returns><c>true</c> if [is handedness null]; otherwise, <c>false</c>.</returns>
      public bool IsHandednessNull()
      {
        return this.IsNull(this.tableSubjects.HandednessColumn);
      }



      /// <summary>
      /// Sets the handedness null.
      /// </summary>
      public void SetHandednessNull()
      {
        this[this.tableSubjects.HandednessColumn] = global::System.Convert.DBNull;
      }



      /// <summary>
      /// Determines whether [is comments null].
      /// </summary>
      /// <returns><c>true</c> if [is comments null]; otherwise, <c>false</c>.</returns>
      public bool IsCommentsNull()
      {
        return this.IsNull(this.tableSubjects.CommentsColumn);
      }



      /// <summary>
      /// Sets the comments null.
      /// </summary>
      public void SetCommentsNull()
      {
        this[this.tableSubjects.CommentsColumn] = global::System.Convert.DBNull;
      }



      /// <summary>
      /// Gets the trials rows.
      /// </summary>
      /// <returns>TrialsRow[].</returns>
      public TrialsRow[] GetTrialsRows()
      {
        if ((this.Table.ChildRelations["FK_Subjects_Trials"] == null))
        {
          return new TrialsRow[0];
        }
        else
        {
          return ((TrialsRow[])(base.GetChildRows(this.Table.ChildRelations["FK_Subjects_Trials"])));
        }
      }



      /// <summary>
      /// Gets the subject parameters rows.
      /// </summary>
      /// <returns>SubjectParametersRow[].</returns>
      public SubjectParametersRow[] GetSubjectParametersRows()
      {
        if ((this.Table.ChildRelations["FK_Subjects_SubjectParameters"] == null))
        {
          return new SubjectParametersRow[0];
        }
        else
        {
          return ((SubjectParametersRow[])(base.GetChildRows(this.Table.ChildRelations["FK_Subjects_SubjectParameters"])));
        }
      }
    }

    /// <summary>
    /// Represents strongly named DataRow class.
    /// </summary>
    public partial class TrialsRow : DataRow
    {

      /// <summary>
      /// The table trials
      /// </summary>
      private TrialsDataTable tableTrials;



      /// <summary>
      /// Initializes a new instance of the <see cref="TrialsRow"/> class.
      /// </summary>
      /// <param name="rb">The rb.</param>
      internal TrialsRow(DataRowBuilder rb) :
        base(rb)
      {
        this.tableTrials = ((TrialsDataTable)(this.Table));
      }



      /// <summary>
      /// Gets or sets the identifier.
      /// </summary>
      /// <value>The identifier.</value>
      public long ID
      {
        get
        {
          return ((long)(this[this.tableTrials.IDColumn]));
        }
        set
        {
          this[this.tableTrials.IDColumn] = value;
        }
      }



      /// <summary>
      /// Gets or sets the name of the subject.
      /// </summary>
      /// <value>The name of the subject.</value>
      public string SubjectName
      {
        get
        {
          return ((string)(this[this.tableTrials.SubjectNameColumn]));
        }
        set
        {
          this[this.tableTrials.SubjectNameColumn] = value;
        }
      }



      /// <summary>
      /// Gets or sets the trial identifier.
      /// </summary>
      /// <value>The trial identifier.</value>
      public int TrialID
      {
        get
        {
          return ((int)(this[this.tableTrials.TrialIDColumn]));
        }
        set
        {
          this[this.tableTrials.TrialIDColumn] = value;
        }
      }



      /// <summary>
      /// Gets or sets the name of the trial.
      /// </summary>
      /// <value>The name of the trial.</value>
      /// <exception cref="System.Data.StrongTypingException">The value for column \'TrialName\' in table \'Trials\' is DBNull.</exception>
      public string TrialName
      {
        get
        {
          try
          {
            return ((string)(this[this.tableTrials.TrialNameColumn]));
          }
          catch (global::System.InvalidCastException e)
          {
            throw new StrongTypingException("The value for column \'TrialName\' in table \'Trials\' is DBNull.", e);
          }
        }
        set
        {
          this[this.tableTrials.TrialNameColumn] = value;
        }
      }



      /// <summary>
      /// Gets or sets the trial sequence.
      /// </summary>
      /// <value>The trial sequence.</value>
      public int TrialSequence
      {
        get
        {
          return ((int)(this[this.tableTrials.TrialSequenceColumn]));
        }
        set
        {
          this[this.tableTrials.TrialSequenceColumn] = value;
        }
      }



      /// <summary>
      /// Gets or sets the category.
      /// </summary>
      /// <value>The category.</value>
      /// <exception cref="System.Data.StrongTypingException">The value for column \'Category\' in table \'Trials\' is DBNull.</exception>
      public string Category
      {
        get
        {
          try
          {
            return ((string)(this[this.tableTrials.CategoryColumn]));
          }
          catch (global::System.InvalidCastException e)
          {
            throw new StrongTypingException("The value for column \'Category\' in table \'Trials\' is DBNull.", e);
          }
        }
        set
        {
          this[this.tableTrials.CategoryColumn] = value;
        }
      }



      /// <summary>
      /// Gets or sets the trial start time.
      /// </summary>
      /// <value>The trial start time.</value>
      /// <exception cref="System.Data.StrongTypingException">The value for column \'TrialStartTime\' in table \'Trials\' is DBNull.</exception>
      public long TrialStartTime
      {
        get
        {
          try
          {
            return ((long)(this[this.tableTrials.TrialStartTimeColumn]));
          }
          catch (global::System.InvalidCastException e)
          {
            throw new StrongTypingException("The value for column \'TrialStartTime\' in table \'Trials\' is DBNull.", e);
          }
        }
        set
        {
          this[this.tableTrials.TrialStartTimeColumn] = value;
        }
      }



      /// <summary>
      /// Gets or sets the duration.
      /// </summary>
      /// <value>The duration.</value>
      /// <exception cref="System.Data.StrongTypingException">The value for column \'Duration\' in table \'Trials\' is DBNull.</exception>
      public int Duration
      {
        get
        {
          try
          {
            return ((int)(this[this.tableTrials.DurationColumn]));
          }
          catch (global::System.InvalidCastException e)
          {
            throw new StrongTypingException("The value for column \'Duration\' in table \'Trials\' is DBNull.", e);
          }
        }
        set
        {
          this[this.tableTrials.DurationColumn] = value;
        }
      }



      /// <summary>
      /// Gets or sets the eliminate data.
      /// </summary>
      /// <value>The eliminate data.</value>
      /// <exception cref="System.Data.StrongTypingException">The value for column \'EliminateData\' in table \'Trials\' is DBNull.</exception>
      public string EliminateData
      {
        get
        {
          try
          {
            return ((string)(this[this.tableTrials.EliminateDataColumn]));
          }
          catch (global::System.InvalidCastException e)
          {
            throw new StrongTypingException("The value for column \'EliminateData\' in table \'Trials\' is DBNull.", e);
          }
        }
        set
        {
          this[this.tableTrials.EliminateDataColumn] = value;
        }
      }



      /// <summary>
      /// Gets or sets the subjects row.
      /// </summary>
      /// <value>The subjects row.</value>
      public SubjectsRow SubjectsRow
      {
        get
        {
          return ((SubjectsRow)(this.GetParentRow(this.Table.ParentRelations["FK_Subjects_Trials"])));
        }
        set
        {
          this.SetParentRow(value, this.Table.ParentRelations["FK_Subjects_Trials"]);
        }
      }



      /// <summary>
      /// Determines whether [is trial name null].
      /// </summary>
      /// <returns><c>true</c> if [is trial name null]; otherwise, <c>false</c>.</returns>
      public bool IsTrialNameNull()
      {
        return this.IsNull(this.tableTrials.TrialNameColumn);
      }



      /// <summary>
      /// Sets the trial name null.
      /// </summary>
      public void SetTrialNameNull()
      {
        this[this.tableTrials.TrialNameColumn] = global::System.Convert.DBNull;
      }



      /// <summary>
      /// Determines whether [is category null].
      /// </summary>
      /// <returns><c>true</c> if [is category null]; otherwise, <c>false</c>.</returns>
      public bool IsCategoryNull()
      {
        return this.IsNull(this.tableTrials.CategoryColumn);
      }



      /// <summary>
      /// Sets the category null.
      /// </summary>
      public void SetCategoryNull()
      {
        this[this.tableTrials.CategoryColumn] = global::System.Convert.DBNull;
      }



      /// <summary>
      /// Determines whether [is trial start time null].
      /// </summary>
      /// <returns><c>true</c> if [is trial start time null]; otherwise, <c>false</c>.</returns>
      public bool IsTrialStartTimeNull()
      {
        return this.IsNull(this.tableTrials.TrialStartTimeColumn);
      }



      /// <summary>
      /// Sets the trial start time null.
      /// </summary>
      public void SetTrialStartTimeNull()
      {
        this[this.tableTrials.TrialStartTimeColumn] = global::System.Convert.DBNull;
      }



      /// <summary>
      /// Determines whether [is duration null].
      /// </summary>
      /// <returns><c>true</c> if [is duration null]; otherwise, <c>false</c>.</returns>
      public bool IsDurationNull()
      {
        return this.IsNull(this.tableTrials.DurationColumn);
      }



      /// <summary>
      /// Sets the duration null.
      /// </summary>
      public void SetDurationNull()
      {
        this[this.tableTrials.DurationColumn] = global::System.Convert.DBNull;
      }



      /// <summary>
      /// Determines whether [is eliminate data null].
      /// </summary>
      /// <returns><c>true</c> if [is eliminate data null]; otherwise, <c>false</c>.</returns>
      public bool IsEliminateDataNull()
      {
        return this.IsNull(this.tableTrials.EliminateDataColumn);
      }



      /// <summary>
      /// Sets the eliminate data null.
      /// </summary>
      public void SetEliminateDataNull()
      {
        this[this.tableTrials.EliminateDataColumn] = global::System.Convert.DBNull;
      }



      /// <summary>
      /// Gets the trial events rows.
      /// </summary>
      /// <returns>TrialEventsRow[].</returns>
      public TrialEventsRow[] GetTrialEventsRows()
      {
        if ((this.Table.ChildRelations["FK_Trials_Events"] == null))
        {
          return new TrialEventsRow[0];
        }
        else
        {
          return ((TrialEventsRow[])(base.GetChildRows(this.Table.ChildRelations["FK_Trials_Events"])));
        }
      }



      /// <summary>
      /// Gets the ao is rows.
      /// </summary>
      /// <returns>AOIsRow[].</returns>
      public AOIsRow[] GetAOIsRows()
      {
        if ((this.Table.ChildRelations["Trials_AOIs"] == null))
        {
          return new AOIsRow[0];
        }
        else
        {
          return ((AOIsRow[])(base.GetChildRows(this.Table.ChildRelations["Trials_AOIs"])));
        }
      }



      /// <summary>
      /// Gets the gaze fixations rows.
      /// </summary>
      /// <returns>GazeFixationsRow[].</returns>
      public GazeFixationsRow[] GetGazeFixationsRows()
      {
        if ((this.Table.ChildRelations["Trials_GazeFixations"] == null))
        {
          return new GazeFixationsRow[0];
        }
        else
        {
          return ((GazeFixationsRow[])(base.GetChildRows(this.Table.ChildRelations["Trials_GazeFixations"])));
        }
      }



      /// <summary>
      /// Gets the mouse fixations rows.
      /// </summary>
      /// <returns>MouseFixationsRow[].</returns>
      public MouseFixationsRow[] GetMouseFixationsRows()
      {
        if ((this.Table.ChildRelations["Trials_MouseFixations"] == null))
        {
          return new MouseFixationsRow[0];
        }
        else
        {
          return ((MouseFixationsRow[])(base.GetChildRows(this.Table.ChildRelations["Trials_MouseFixations"])));
        }
      }
    }

    /// <summary>
    /// Represents strongly named DataRow class.
    /// </summary>
    public partial class TrialEventsRow : DataRow
    {

      /// <summary>
      /// The table trial events
      /// </summary>
      private TrialEventsDataTable tableTrialEvents;



      /// <summary>
      /// Initializes a new instance of the <see cref="TrialEventsRow"/> class.
      /// </summary>
      /// <param name="rb">The rb.</param>
      internal TrialEventsRow(DataRowBuilder rb) :
        base(rb)
      {
        this.tableTrialEvents = ((TrialEventsDataTable)(this.Table));
      }



      /// <summary>
      /// Gets or sets the identifier.
      /// </summary>
      /// <value>The identifier.</value>
      public int ID
      {
        get
        {
          return ((int)(this[this.tableTrialEvents.IDColumn]));
        }
        set
        {
          this[this.tableTrialEvents.IDColumn] = value;
        }
      }



      /// <summary>
      /// Gets or sets the name of the subject.
      /// </summary>
      /// <value>The name of the subject.</value>
      public string SubjectName
      {
        get
        {
          return ((string)(this[this.tableTrialEvents.SubjectNameColumn]));
        }
        set
        {
          this[this.tableTrialEvents.SubjectNameColumn] = value;
        }
      }



      /// <summary>
      /// Gets or sets the trial sequence.
      /// </summary>
      /// <value>The trial sequence.</value>
      public int TrialSequence
      {
        get
        {
          return ((int)(this[this.tableTrialEvents.TrialSequenceColumn]));
        }
        set
        {
          this[this.tableTrialEvents.TrialSequenceColumn] = value;
        }
      }



      /// <summary>
      /// Gets or sets the event identifier.
      /// </summary>
      /// <value>The event identifier.</value>
      public int EventID
      {
        get
        {
          return ((int)(this[this.tableTrialEvents.EventIDColumn]));
        }
        set
        {
          this[this.tableTrialEvents.EventIDColumn] = value;
        }
      }



      /// <summary>
      /// Gets or sets the event time.
      /// </summary>
      /// <value>The event time.</value>
      public long EventTime
      {
        get
        {
          return ((long)(this[this.tableTrialEvents.EventTimeColumn]));
        }
        set
        {
          this[this.tableTrialEvents.EventTimeColumn] = value;
        }
      }



      /// <summary>
      /// Gets or sets the type of the event.
      /// </summary>
      /// <value>The type of the event.</value>
      public string EventType
      {
        get
        {
          return ((string)(this[this.tableTrialEvents.EventTypeColumn]));
        }
        set
        {
          this[this.tableTrialEvents.EventTypeColumn] = value;
        }
      }



      /// <summary>
      /// Gets or sets the event task.
      /// </summary>
      /// <value>The event task.</value>
      /// <exception cref="System.Data.StrongTypingException">The value for column \'EventTask\' in table \'TrialEvents\' is DBNull.</exception>
      public string EventTask
      {
        get
        {
          try
          {
            return ((string)(this[this.tableTrialEvents.EventTaskColumn]));
          }
          catch (global::System.InvalidCastException e)
          {
            throw new StrongTypingException("The value for column \'EventTask\' in table \'TrialEvents\' is DBNull.", e);
          }
        }
        set
        {
          this[this.tableTrialEvents.EventTaskColumn] = value;
        }
      }



      /// <summary>
      /// Gets or sets the event parameter.
      /// </summary>
      /// <value>The event parameter.</value>
      /// <exception cref="System.Data.StrongTypingException">The value for column \'EventParam\' in table \'TrialEvents\' is DBNull.</exception>
      public string EventParam
      {
        get
        {
          try
          {
            return ((string)(this[this.tableTrialEvents.EventParamColumn]));
          }
          catch (global::System.InvalidCastException e)
          {
            throw new StrongTypingException("The value for column \'EventParam\' in table \'TrialEvents\' is DBNull.", e);
          }
        }
        set
        {
          this[this.tableTrialEvents.EventParamColumn] = value;
        }
      }



      /// <summary>
      /// Gets or sets the trials row parent.
      /// </summary>
      /// <value>The trials row parent.</value>
      public TrialsRow TrialsRowParent
      {
        get
        {
          return ((TrialsRow)(this.GetParentRow(this.Table.ParentRelations["FK_Trials_Events"])));
        }
        set
        {
          this.SetParentRow(value, this.Table.ParentRelations["FK_Trials_Events"]);
        }
      }



      /// <summary>
      /// Determines whether [is event task null].
      /// </summary>
      /// <returns><c>true</c> if [is event task null]; otherwise, <c>false</c>.</returns>
      public bool IsEventTaskNull()
      {
        return this.IsNull(this.tableTrialEvents.EventTaskColumn);
      }



      /// <summary>
      /// Sets the event task null.
      /// </summary>
      public void SetEventTaskNull()
      {
        this[this.tableTrialEvents.EventTaskColumn] = global::System.Convert.DBNull;
      }



      /// <summary>
      /// Determines whether [is event parameter null].
      /// </summary>
      /// <returns><c>true</c> if [is event parameter null]; otherwise, <c>false</c>.</returns>
      public bool IsEventParamNull()
      {
        return this.IsNull(this.tableTrialEvents.EventParamColumn);
      }



      /// <summary>
      /// Sets the event parameter null.
      /// </summary>
      public void SetEventParamNull()
      {
        this[this.tableTrialEvents.EventParamColumn] = global::System.Convert.DBNull;
      }
    }

    /// <summary>
    /// Represents strongly named DataRow class.
    /// </summary>
    public partial class RawdataRow : DataRow
    {

      /// <summary>
      /// The table rawdata
      /// </summary>
      private RawdataDataTable tableRawdata;



      /// <summary>
      /// Initializes a new instance of the <see cref="RawdataRow"/> class.
      /// </summary>
      /// <param name="rb">The rb.</param>
      internal RawdataRow(DataRowBuilder rb) :
        base(rb)
      {
        this.tableRawdata = ((RawdataDataTable)(this.Table));
      }



      /// <summary>
      /// Gets or sets the identifier.
      /// </summary>
      /// <value>The identifier.</value>
      public long ID
      {
        get
        {
          return ((long)(this[this.tableRawdata.IDColumn]));
        }
        set
        {
          this[this.tableRawdata.IDColumn] = value;
        }
      }



      /// <summary>
      /// Gets or sets the name of the subject.
      /// </summary>
      /// <value>The name of the subject.</value>
      public string SubjectName
      {
        get
        {
          return ((string)(this[this.tableRawdata.SubjectNameColumn]));
        }
        set
        {
          this[this.tableRawdata.SubjectNameColumn] = value;
        }
      }



      /// <summary>
      /// Gets or sets the trial sequence.
      /// </summary>
      /// <value>The trial sequence.</value>
      public int TrialSequence
      {
        get
        {
          return ((int)(this[this.tableRawdata.TrialSequenceColumn]));
        }
        set
        {
          this[this.tableRawdata.TrialSequenceColumn] = value;
        }
      }



      /// <summary>
      /// Gets or sets the time.
      /// </summary>
      /// <value>The time.</value>
      public long Time
      {
        get
        {
          return ((long)(this[this.tableRawdata.TimeColumn]));
        }
        set
        {
          this[this.tableRawdata.TimeColumn] = value;
        }
      }



      /// <summary>
      /// Gets or sets the pupil dia x.
      /// </summary>
      /// <value>The pupil dia x.</value>
      /// <exception cref="System.Data.StrongTypingException">The value for column \'PupilDiaX\' in table \'Rawdata\' is DBNull.</exception>
      public double? PupilDiaX
      {
        get
        {
          if (this.IsNull(this.tableRawdata.PupilDiaXColumn))
          {
            return null;
          }

          return (double)this[this.tableRawdata.PupilDiaXColumn];
        }
        set
        {
          this[this.tableRawdata.PupilDiaXColumn] = value;
        }
      }



      /// <summary>
      /// Gets or sets the pupil dia y.
      /// </summary>
      /// <value>The pupil dia y.</value>
      /// <exception cref="System.Data.StrongTypingException">The value for column \'PupilDiaY\' in table \'Rawdata\' is DBNull.</exception>
      public double? PupilDiaY
      {
        get
        {
          if (this.IsNull(this.tableRawdata.PupilDiaYColumn))
          {
            return null;
          }

          return (double)this[this.tableRawdata.PupilDiaYColumn];
        }
        set
        {
          this[this.tableRawdata.PupilDiaYColumn] = value;
        }
      }



      /// <summary>
      /// Gets or sets the gaze position x.
      /// </summary>
      /// <value>The gaze position x.</value>
      /// <exception cref="System.Data.StrongTypingException">The value for column \'GazePosX\' in table \'Rawdata\' is DBNull.</exception>
      public double? GazePosX
      {
        get
        {
          if (this.IsNull(this.tableRawdata.GazePosXColumn))
          {
            return null;
          }

          return (double)this[this.tableRawdata.GazePosXColumn];
        }
        set
        {
          this[this.tableRawdata.GazePosXColumn] = value;
        }
      }



      /// <summary>
      /// Gets or sets the gaze position y.
      /// </summary>
      /// <value>The gaze position y.</value>
      /// <exception cref="System.Data.StrongTypingException">The value for column \'GazePosY\' in table \'Rawdata\' is DBNull.</exception>
      public double? GazePosY
      {
        get
        {
          if (this.IsNull(this.tableRawdata.GazePosYColumn))
          {
            return null;
          }

          return (double)this[this.tableRawdata.GazePosYColumn];
        }
        set
        {
          this[this.tableRawdata.GazePosYColumn] = value;
        }
      }



      /// <summary>
      /// Gets or sets the mouse position x.
      /// </summary>
      /// <value>The mouse position x.</value>
      /// <exception cref="System.Data.StrongTypingException">The value for column \'MousePosX\' in table \'Rawdata\' is DBNull.</exception>
      public double? MousePosX
      {
        get
        {
          if (this.IsNull(this.tableRawdata.MousePosXColumn))
          {
            return null;
          }

          return (double)this[this.tableRawdata.MousePosXColumn];
        }
        set
        {
          this[this.tableRawdata.MousePosXColumn] = value;
        }
      }



      /// <summary>
      /// Gets or sets the mouse position y.
      /// </summary>
      /// <value>The mouse position y.</value>
      /// <exception cref="System.Data.StrongTypingException">The value for column \'MousePosY\' in table \'Rawdata\' is DBNull.</exception>
      public double? MousePosY
      {
        get
        {
          if (this.IsNull(this.tableRawdata.MousePosYColumn))
          {
            return null;
          }

          return (double)this[this.tableRawdata.MousePosYColumn];

          //try
          //{
          //  return ((double)(this[this.tableRawdata.MousePosYColumn]));
          //}
          //catch (global::System.InvalidCastException e)
          //{
          //  throw new StrongTypingException("The value for column \'MousePosY\' in table \'Rawdata\' is DBNull.", e);
          //}
        }
        set
        {
          this[this.tableRawdata.MousePosYColumn] = value;
        }
      }



      /// <summary>
      /// Gets or sets the event identifier.
      /// </summary>
      /// <value>The event identifier.</value>
      /// <exception cref="System.Data.StrongTypingException">The value for column \'EventID\' in table \'Rawdata\' is DBNull.</exception>
      public int? EventID
      {
        get
        {
          if (this.IsNull(this.tableRawdata.EventIDColumn))
          {
            return null;
          }

          return (int)this[this.tableRawdata.EventIDColumn];
        }

        set
        {
          this[this.tableRawdata.EventIDColumn] = value;
        }
      }



      /// <summary>
      /// Determines whether [is pupil dia x null].
      /// </summary>
      /// <returns><c>true</c> if [is pupil dia x null]; otherwise, <c>false</c>.</returns>
      public bool IsPupilDiaXNull()
      {
        return this.IsNull(this.tableRawdata.PupilDiaXColumn);
      }



      /// <summary>
      /// Sets the pupil dia x null.
      /// </summary>
      public void SetPupilDiaXNull()
      {
        this[this.tableRawdata.PupilDiaXColumn] = global::System.Convert.DBNull;
      }



      /// <summary>
      /// Determines whether [is pupil dia y null].
      /// </summary>
      /// <returns><c>true</c> if [is pupil dia y null]; otherwise, <c>false</c>.</returns>
      public bool IsPupilDiaYNull()
      {
        return this.IsNull(this.tableRawdata.PupilDiaYColumn);
      }



      /// <summary>
      /// Sets the pupil dia y null.
      /// </summary>
      public void SetPupilDiaYNull()
      {
        this[this.tableRawdata.PupilDiaYColumn] = global::System.Convert.DBNull;
      }



      /// <summary>
      /// Determines whether [is gaze position x null].
      /// </summary>
      /// <returns><c>true</c> if [is gaze position x null]; otherwise, <c>false</c>.</returns>
      public bool IsGazePosXNull()
      {
        return this.IsNull(this.tableRawdata.GazePosXColumn);
      }



      /// <summary>
      /// Sets the gaze position x null.
      /// </summary>
      public void SetGazePosXNull()
      {
        this[this.tableRawdata.GazePosXColumn] = global::System.Convert.DBNull;
      }



      /// <summary>
      /// Determines whether [is gaze position y null].
      /// </summary>
      /// <returns><c>true</c> if [is gaze position y null]; otherwise, <c>false</c>.</returns>
      public bool IsGazePosYNull()
      {
        return this.IsNull(this.tableRawdata.GazePosYColumn);
      }



      /// <summary>
      /// Sets the gaze position y null.
      /// </summary>
      public void SetGazePosYNull()
      {
        this[this.tableRawdata.GazePosYColumn] = global::System.Convert.DBNull;
      }



      /// <summary>
      /// Determines whether [is mouse position x null].
      /// </summary>
      /// <returns><c>true</c> if [is mouse position x null]; otherwise, <c>false</c>.</returns>
      public bool IsMousePosXNull()
      {
        return this.IsNull(this.tableRawdata.MousePosXColumn);
      }



      /// <summary>
      /// Sets the mouse position x null.
      /// </summary>
      public void SetMousePosXNull()
      {
        this[this.tableRawdata.MousePosXColumn] = global::System.Convert.DBNull;
      }



      /// <summary>
      /// Determines whether [is mouse position y null].
      /// </summary>
      /// <returns><c>true</c> if [is mouse position y null]; otherwise, <c>false</c>.</returns>
      public bool IsMousePosYNull()
      {
        return this.IsNull(this.tableRawdata.MousePosYColumn);
      }



      /// <summary>
      /// Sets the mouse position y null.
      /// </summary>
      public void SetMousePosYNull()
      {
        this[this.tableRawdata.MousePosYColumn] = global::System.Convert.DBNull;
      }



      /// <summary>
      /// Determines whether [is event identifier null].
      /// </summary>
      /// <returns><c>true</c> if [is event identifier null]; otherwise, <c>false</c>.</returns>
      public bool IsEventIDNull()
      {
        return this.IsNull(this.tableRawdata.EventIDColumn);
      }



      /// <summary>
      /// Sets the event identifier null.
      /// </summary>
      public void SetEventIDNull()
      {
        this[this.tableRawdata.EventIDColumn] = global::System.Convert.DBNull;
      }
    }

    /// <summary>
    /// Represents strongly named DataRow class.
    /// </summary>
    public partial class AOIsRow : DataRow
    {

      /// <summary>
      /// The table ao is
      /// </summary>
      private AOIsDataTable tableAOIs;



      /// <summary>
      /// Initializes a new instance of the <see cref="AOIsRow"/> class.
      /// </summary>
      /// <param name="rb">The rb.</param>
      internal AOIsRow(DataRowBuilder rb) :
        base(rb)
      {
        this.tableAOIs = ((AOIsDataTable)(this.Table));
      }



      /// <summary>
      /// Gets or sets the identifier.
      /// </summary>
      /// <value>The identifier.</value>
      public int ID
      {
        get
        {
          return ((int)(this[this.tableAOIs.IDColumn]));
        }
        set
        {
          this[this.tableAOIs.IDColumn] = value;
        }
      }



      /// <summary>
      /// Gets or sets the trial identifier.
      /// </summary>
      /// <value>The trial identifier.</value>
      public int TrialID
      {
        get
        {
          return ((int)(this[this.tableAOIs.TrialIDColumn]));
        }
        set
        {
          this[this.tableAOIs.TrialIDColumn] = value;
        }
      }



      /// <summary>
      /// Gets or sets the slide nr.
      /// </summary>
      /// <value>The slide nr.</value>
      public int SlideNr
      {
        get
        {
          return ((int)(this[this.tableAOIs.SlideNrColumn]));
        }
        set
        {
          this[this.tableAOIs.SlideNrColumn] = value;
        }
      }



      /// <summary>
      /// Gets or sets the name of the shape.
      /// </summary>
      /// <value>The name of the shape.</value>
      /// <exception cref="System.Data.StrongTypingException">The value for column \'ShapeName\' in table \'AOIs\' is DBNull.</exception>
      public string ShapeName
      {
        get
        {
          try
          {
            return ((string)(this[this.tableAOIs.ShapeNameColumn]));
          }
          catch (global::System.InvalidCastException e)
          {
            throw new StrongTypingException("The value for column \'ShapeName\' in table \'AOIs\' is DBNull.", e);
          }
        }
        set
        {
          this[this.tableAOIs.ShapeNameColumn] = value;
        }
      }



      /// <summary>
      /// Gets or sets the type of the shape.
      /// </summary>
      /// <value>The type of the shape.</value>
      /// <exception cref="System.Data.StrongTypingException">The value for column \'ShapeType\' in table \'AOIs\' is DBNull.</exception>
      public string ShapeType
      {
        get
        {
          try
          {
            return ((string)(this[this.tableAOIs.ShapeTypeColumn]));
          }
          catch (global::System.InvalidCastException e)
          {
            throw new StrongTypingException("The value for column \'ShapeType\' in table \'AOIs\' is DBNull.", e);
          }
        }
        set
        {
          this[this.tableAOIs.ShapeTypeColumn] = value;
        }
      }



      /// <summary>
      /// Gets or sets the shape number PTS.
      /// </summary>
      /// <value>The shape number PTS.</value>
      /// <exception cref="System.Data.StrongTypingException">The value for column \'ShapeNumPts\' in table \'AOIs\' is DBNull.</exception>
      public int ShapeNumPts
      {
        get
        {
          try
          {
            return ((int)(this[this.tableAOIs.ShapeNumPtsColumn]));
          }
          catch (global::System.InvalidCastException e)
          {
            throw new StrongTypingException("The value for column \'ShapeNumPts\' in table \'AOIs\' is DBNull.", e);
          }
        }
        set
        {
          this[this.tableAOIs.ShapeNumPtsColumn] = value;
        }
      }



      /// <summary>
      /// Gets or sets the shape PTS.
      /// </summary>
      /// <value>The shape PTS.</value>
      /// <exception cref="System.Data.StrongTypingException">The value for column \'ShapePts\' in table \'AOIs\' is DBNull.</exception>
      public string ShapePts
      {
        get
        {
          try
          {
            return ((string)(this[this.tableAOIs.ShapePtsColumn]));
          }
          catch (global::System.InvalidCastException e)
          {
            throw new StrongTypingException("The value for column \'ShapePts\' in table \'AOIs\' is DBNull.", e);
          }
        }
        set
        {
          this[this.tableAOIs.ShapePtsColumn] = value;
        }
      }



      /// <summary>
      /// Gets or sets the shape group.
      /// </summary>
      /// <value>The shape group.</value>
      /// <exception cref="System.Data.StrongTypingException">The value for column \'ShapeGroup\' in table \'AOIs\' is DBNull.</exception>
      public string ShapeGroup
      {
        get
        {
          try
          {
            return ((string)(this[this.tableAOIs.ShapeGroupColumn]));
          }
          catch (global::System.InvalidCastException e)
          {
            throw new StrongTypingException("The value for column \'ShapeGroup\' in table \'AOIs\' is DBNull.", e);
          }
        }
        set
        {
          this[this.tableAOIs.ShapeGroupColumn] = value;
        }
      }



      /// <summary>
      /// Gets or sets the trials row.
      /// </summary>
      /// <value>The trials row.</value>
      public TrialsRow TrialsRow
      {
        get
        {
          return ((TrialsRow)(this.GetParentRow(this.Table.ParentRelations["Trials_AOIs"])));
        }
        set
        {
          this.SetParentRow(value, this.Table.ParentRelations["Trials_AOIs"]);
        }
      }



      /// <summary>
      /// Gets or sets the shape groups row.
      /// </summary>
      /// <value>The shape groups row.</value>
      public ShapeGroupsRow ShapeGroupsRow
      {
        get
        {
          return ((ShapeGroupsRow)(this.GetParentRow(this.Table.ParentRelations["ShapeGroups_AOIs"])));
        }
        set
        {
          this.SetParentRow(value, this.Table.ParentRelations["ShapeGroups_AOIs"]);
        }
      }



      /// <summary>
      /// Determines whether [is shape name null].
      /// </summary>
      /// <returns><c>true</c> if [is shape name null]; otherwise, <c>false</c>.</returns>
      public bool IsShapeNameNull()
      {
        return this.IsNull(this.tableAOIs.ShapeNameColumn);
      }



      /// <summary>
      /// Sets the shape name null.
      /// </summary>
      public void SetShapeNameNull()
      {
        this[this.tableAOIs.ShapeNameColumn] = global::System.Convert.DBNull;
      }



      /// <summary>
      /// Determines whether [is shape type null].
      /// </summary>
      /// <returns><c>true</c> if [is shape type null]; otherwise, <c>false</c>.</returns>
      public bool IsShapeTypeNull()
      {
        return this.IsNull(this.tableAOIs.ShapeTypeColumn);
      }



      /// <summary>
      /// Sets the shape type null.
      /// </summary>
      public void SetShapeTypeNull()
      {
        this[this.tableAOIs.ShapeTypeColumn] = global::System.Convert.DBNull;
      }



      /// <summary>
      /// Determines whether [is shape number PTS null].
      /// </summary>
      /// <returns><c>true</c> if [is shape number PTS null]; otherwise, <c>false</c>.</returns>
      public bool IsShapeNumPtsNull()
      {
        return this.IsNull(this.tableAOIs.ShapeNumPtsColumn);
      }



      /// <summary>
      /// Sets the shape number PTS null.
      /// </summary>
      public void SetShapeNumPtsNull()
      {
        this[this.tableAOIs.ShapeNumPtsColumn] = global::System.Convert.DBNull;
      }



      /// <summary>
      /// Determines whether [is shape PTS null].
      /// </summary>
      /// <returns><c>true</c> if [is shape PTS null]; otherwise, <c>false</c>.</returns>
      public bool IsShapePtsNull()
      {
        return this.IsNull(this.tableAOIs.ShapePtsColumn);
      }



      /// <summary>
      /// Sets the shape PTS null.
      /// </summary>
      public void SetShapePtsNull()
      {
        this[this.tableAOIs.ShapePtsColumn] = global::System.Convert.DBNull;
      }



      /// <summary>
      /// Determines whether [is shape group null].
      /// </summary>
      /// <returns><c>true</c> if [is shape group null]; otherwise, <c>false</c>.</returns>
      public bool IsShapeGroupNull()
      {
        return this.IsNull(this.tableAOIs.ShapeGroupColumn);
      }



      /// <summary>
      /// Sets the shape group null.
      /// </summary>
      public void SetShapeGroupNull()
      {
        this[this.tableAOIs.ShapeGroupColumn] = global::System.Convert.DBNull;
      }
    }

    /// <summary>
    /// Represents strongly named DataRow class.
    /// </summary>
    public partial class ShapeGroupsRow : DataRow
    {

      /// <summary>
      /// The table shape groups
      /// </summary>
      private ShapeGroupsDataTable tableShapeGroups;



      /// <summary>
      /// Initializes a new instance of the <see cref="ShapeGroupsRow"/> class.
      /// </summary>
      /// <param name="rb">The rb.</param>
      internal ShapeGroupsRow(DataRowBuilder rb) :
        base(rb)
      {
        this.tableShapeGroups = ((ShapeGroupsDataTable)(this.Table));
      }



      /// <summary>
      /// Gets or sets the identifier.
      /// </summary>
      /// <value>The identifier.</value>
      public int ID
      {
        get
        {
          return ((int)(this[this.tableShapeGroups.IDColumn]));
        }
        set
        {
          this[this.tableShapeGroups.IDColumn] = value;
        }
      }



      /// <summary>
      /// Gets or sets the shape group.
      /// </summary>
      /// <value>The shape group.</value>
      public string ShapeGroup
      {
        get
        {
          return ((string)(this[this.tableShapeGroups.ShapeGroupColumn]));
        }
        set
        {
          this[this.tableShapeGroups.ShapeGroupColumn] = value;
        }
      }



      /// <summary>
      /// Gets the ao is rows.
      /// </summary>
      /// <returns>AOIsRow[].</returns>
      public AOIsRow[] GetAOIsRows()
      {
        if ((this.Table.ChildRelations["ShapeGroups_AOIs"] == null))
        {
          return new AOIsRow[0];
        }
        else
        {
          return ((AOIsRow[])(base.GetChildRows(this.Table.ChildRelations["ShapeGroups_AOIs"])));
        }
      }
    }

    /// <summary>
    /// Represents strongly named DataRow class.
    /// </summary>
    public partial class GazeFixationsRow : DataRow
    {

      /// <summary>
      /// The table gaze fixations
      /// </summary>
      private GazeFixationsDataTable tableGazeFixations;



      /// <summary>
      /// Initializes a new instance of the <see cref="GazeFixationsRow"/> class.
      /// </summary>
      /// <param name="rb">The rb.</param>
      internal GazeFixationsRow(DataRowBuilder rb) :
        base(rb)
      {
        this.tableGazeFixations = ((GazeFixationsDataTable)(this.Table));
      }



      /// <summary>
      /// Gets or sets the identifier.
      /// </summary>
      /// <value>The identifier.</value>
      public long ID
      {
        get
        {
          return ((long)(this[this.tableGazeFixations.IDColumn]));
        }
        set
        {
          this[this.tableGazeFixations.IDColumn] = value;
        }
      }



      /// <summary>
      /// Gets or sets the name of the subject.
      /// </summary>
      /// <value>The name of the subject.</value>
      public string SubjectName
      {
        get
        {
          return ((string)(this[this.tableGazeFixations.SubjectNameColumn]));
        }
        set
        {
          this[this.tableGazeFixations.SubjectNameColumn] = value;
        }
      }



      /// <summary>
      /// Gets or sets the trial identifier.
      /// </summary>
      /// <value>The trial identifier.</value>
      public int TrialID
      {
        get
        {
          return ((int)(this[this.tableGazeFixations.TrialIDColumn]));
        }
        set
        {
          this[this.tableGazeFixations.TrialIDColumn] = value;
        }
      }



      /// <summary>
      /// Gets or sets the trial sequence.
      /// </summary>
      /// <value>The trial sequence.</value>
      public int TrialSequence
      {
        get
        {
          return ((int)(this[this.tableGazeFixations.TrialSequenceColumn]));
        }
        set
        {
          this[this.tableGazeFixations.TrialSequenceColumn] = value;
        }
      }



      /// <summary>
      /// Gets or sets the count in trial.
      /// </summary>
      /// <value>The count in trial.</value>
      /// <exception cref="System.Data.StrongTypingException">The value for column \'CountInTrial\' in table \'GazeFixations\' is DBNull.</exception>
      public int CountInTrial
      {
        get
        {
          try
          {
            return ((int)(this[this.tableGazeFixations.CountInTrialColumn]));
          }
          catch (global::System.InvalidCastException e)
          {
            throw new StrongTypingException("The value for column \'CountInTrial\' in table \'GazeFixations\' is DBNull.", e);
          }
        }
        set
        {
          this[this.tableGazeFixations.CountInTrialColumn] = value;
        }
      }



      /// <summary>
      /// Gets or sets the start time.
      /// </summary>
      /// <value>The start time.</value>
      /// <exception cref="System.Data.StrongTypingException">The value for column \'StartTime\' in table \'GazeFixations\' is DBNull.</exception>
      public long StartTime
      {
        get
        {
          try
          {
            return ((long)(this[this.tableGazeFixations.StartTimeColumn]));
          }
          catch (global::System.InvalidCastException e)
          {
            throw new StrongTypingException("The value for column \'StartTime\' in table \'GazeFixations\' is DBNull.", e);
          }
        }
        set
        {
          this[this.tableGazeFixations.StartTimeColumn] = value;
        }
      }



      /// <summary>
      /// Gets or sets the length.
      /// </summary>
      /// <value>The length.</value>
      /// <exception cref="System.Data.StrongTypingException">The value for column \'Length\' in table \'GazeFixations\' is DBNull.</exception>
      public int Length
      {
        get
        {
          try
          {
            return ((int)(this[this.tableGazeFixations.LengthColumn]));
          }
          catch (global::System.InvalidCastException e)
          {
            throw new StrongTypingException("The value for column \'Length\' in table \'GazeFixations\' is DBNull.", e);
          }
        }
        set
        {
          this[this.tableGazeFixations.LengthColumn] = value;
        }
      }



      /// <summary>
      /// Gets or sets the position x.
      /// </summary>
      /// <value>The position x.</value>
      /// <exception cref="System.Data.StrongTypingException">The value for column \'PosX\' in table \'GazeFixations\' is DBNull.</exception>
      public double PosX
      {
        get
        {
          try
          {
            return ((double)(this[this.tableGazeFixations.PosXColumn]));
          }
          catch (global::System.InvalidCastException e)
          {
            throw new StrongTypingException("The value for column \'PosX\' in table \'GazeFixations\' is DBNull.", e);
          }
        }
        set
        {
          this[this.tableGazeFixations.PosXColumn] = value;
        }
      }



      /// <summary>
      /// Gets or sets the position y.
      /// </summary>
      /// <value>The position y.</value>
      /// <exception cref="System.Data.StrongTypingException">The value for column \'PosY\' in table \'GazeFixations\' is DBNull.</exception>
      public double PosY
      {
        get
        {
          try
          {
            return ((double)(this[this.tableGazeFixations.PosYColumn]));
          }
          catch (global::System.InvalidCastException e)
          {
            throw new StrongTypingException("The value for column \'PosY\' in table \'GazeFixations\' is DBNull.", e);
          }
        }
        set
        {
          this[this.tableGazeFixations.PosYColumn] = value;
        }
      }



      /// <summary>
      /// Gets or sets the trials row parent.
      /// </summary>
      /// <value>The trials row parent.</value>
      public TrialsRow TrialsRowParent
      {
        get
        {
          return ((TrialsRow)(this.GetParentRow(this.Table.ParentRelations["Trials_GazeFixations"])));
        }
        set
        {
          this.SetParentRow(value, this.Table.ParentRelations["Trials_GazeFixations"]);
        }
      }



      /// <summary>
      /// Determines whether [is count in trial null].
      /// </summary>
      /// <returns><c>true</c> if [is count in trial null]; otherwise, <c>false</c>.</returns>
      public bool IsCountInTrialNull()
      {
        return this.IsNull(this.tableGazeFixations.CountInTrialColumn);
      }



      /// <summary>
      /// Sets the count in trial null.
      /// </summary>
      public void SetCountInTrialNull()
      {
        this[this.tableGazeFixations.CountInTrialColumn] = global::System.Convert.DBNull;
      }



      /// <summary>
      /// Determines whether [is start time null].
      /// </summary>
      /// <returns><c>true</c> if [is start time null]; otherwise, <c>false</c>.</returns>
      public bool IsStartTimeNull()
      {
        return this.IsNull(this.tableGazeFixations.StartTimeColumn);
      }



      /// <summary>
      /// Sets the start time null.
      /// </summary>
      public void SetStartTimeNull()
      {
        this[this.tableGazeFixations.StartTimeColumn] = global::System.Convert.DBNull;
      }



      /// <summary>
      /// Determines whether [is length null].
      /// </summary>
      /// <returns><c>true</c> if [is length null]; otherwise, <c>false</c>.</returns>
      public bool IsLengthNull()
      {
        return this.IsNull(this.tableGazeFixations.LengthColumn);
      }



      /// <summary>
      /// Sets the length null.
      /// </summary>
      public void SetLengthNull()
      {
        this[this.tableGazeFixations.LengthColumn] = global::System.Convert.DBNull;
      }



      /// <summary>
      /// Determines whether [is position x null].
      /// </summary>
      /// <returns><c>true</c> if [is position x null]; otherwise, <c>false</c>.</returns>
      public bool IsPosXNull()
      {
        return this.IsNull(this.tableGazeFixations.PosXColumn);
      }



      /// <summary>
      /// Sets the position x null.
      /// </summary>
      public void SetPosXNull()
      {
        this[this.tableGazeFixations.PosXColumn] = global::System.Convert.DBNull;
      }



      /// <summary>
      /// Determines whether [is position y null].
      /// </summary>
      /// <returns><c>true</c> if [is position y null]; otherwise, <c>false</c>.</returns>
      public bool IsPosYNull()
      {
        return this.IsNull(this.tableGazeFixations.PosYColumn);
      }



      /// <summary>
      /// Sets the position y null.
      /// </summary>
      public void SetPosYNull()
      {
        this[this.tableGazeFixations.PosYColumn] = global::System.Convert.DBNull;
      }
    }

    /// <summary>
    /// Represents strongly named DataRow class.
    /// </summary>
    public class MouseFixationsRow : DataRow
    {

      /// <summary>
      /// The table mouse fixations
      /// </summary>
      private MouseFixationsDataTable tableMouseFixations;



      /// <summary>
      /// Initializes a new instance of the <see cref="MouseFixationsRow"/> class.
      /// </summary>
      /// <param name="rb">The rb.</param>
      internal MouseFixationsRow(DataRowBuilder rb) :
        base(rb)
      {
        this.tableMouseFixations = ((MouseFixationsDataTable)(this.Table));
      }



      /// <summary>
      /// Gets or sets the identifier.
      /// </summary>
      /// <value>The identifier.</value>
      public long ID
      {
        get
        {
          return ((long)(this[this.tableMouseFixations.IDColumn]));
        }
        set
        {
          this[this.tableMouseFixations.IDColumn] = value;
        }
      }



      /// <summary>
      /// Gets or sets the name of the subject.
      /// </summary>
      /// <value>The name of the subject.</value>
      public string SubjectName
      {
        get
        {
          return ((string)(this[this.tableMouseFixations.SubjectNameColumn]));
        }
        set
        {
          this[this.tableMouseFixations.SubjectNameColumn] = value;
        }
      }



      /// <summary>
      /// Gets or sets the trial identifier.
      /// </summary>
      /// <value>The trial identifier.</value>
      public int TrialID
      {
        get
        {
          return ((int)(this[this.tableMouseFixations.TrialIDColumn]));
        }
        set
        {
          this[this.tableMouseFixations.TrialIDColumn] = value;
        }
      }



      /// <summary>
      /// Gets or sets the trial sequence.
      /// </summary>
      /// <value>The trial sequence.</value>
      public int TrialSequence
      {
        get
        {
          return ((int)(this[this.tableMouseFixations.TrialSequenceColumn]));
        }
        set
        {
          this[this.tableMouseFixations.TrialSequenceColumn] = value;
        }
      }



      /// <summary>
      /// Gets or sets the count in trial.
      /// </summary>
      /// <value>The count in trial.</value>
      /// <exception cref="System.Data.StrongTypingException">The value for column \'CountInTrial\' in table \'MouseFixations\' is DBNull.</exception>
      public int CountInTrial
      {
        get
        {
          try
          {
            return ((int)(this[this.tableMouseFixations.CountInTrialColumn]));
          }
          catch (global::System.InvalidCastException e)
          {
            throw new StrongTypingException("The value for column \'CountInTrial\' in table \'MouseFixations\' is DBNull.", e);
          }
        }
        set
        {
          this[this.tableMouseFixations.CountInTrialColumn] = value;
        }
      }



      /// <summary>
      /// Gets or sets the start time.
      /// </summary>
      /// <value>The start time.</value>
      /// <exception cref="System.Data.StrongTypingException">The value for column \'StartTime\' in table \'MouseFixations\' is DBNull.</exception>
      public long StartTime
      {
        get
        {
          try
          {
            return ((long)(this[this.tableMouseFixations.StartTimeColumn]));
          }
          catch (global::System.InvalidCastException e)
          {
            throw new StrongTypingException("The value for column \'StartTime\' in table \'MouseFixations\' is DBNull.", e);
          }
        }
        set
        {
          this[this.tableMouseFixations.StartTimeColumn] = value;
        }
      }



      /// <summary>
      /// Gets or sets the length.
      /// </summary>
      /// <value>The length.</value>
      /// <exception cref="System.Data.StrongTypingException">The value for column \'Length\' in table \'MouseFixations\' is DBNull.</exception>
      public int Length
      {
        get
        {
          try
          {
            return ((int)(this[this.tableMouseFixations.LengthColumn]));
          }
          catch (global::System.InvalidCastException e)
          {
            throw new StrongTypingException("The value for column \'Length\' in table \'MouseFixations\' is DBNull.", e);
          }
        }
        set
        {
          this[this.tableMouseFixations.LengthColumn] = value;
        }
      }



      /// <summary>
      /// Gets or sets the position x.
      /// </summary>
      /// <value>The position x.</value>
      /// <exception cref="System.Data.StrongTypingException">The value for column \'PosX\' in table \'MouseFixations\' is DBNull.</exception>
      public double PosX
      {
        get
        {
          try
          {
            return ((double)(this[this.tableMouseFixations.PosXColumn]));
          }
          catch (global::System.InvalidCastException e)
          {
            throw new StrongTypingException("The value for column \'PosX\' in table \'MouseFixations\' is DBNull.", e);
          }
        }
        set
        {
          this[this.tableMouseFixations.PosXColumn] = value;
        }
      }



      /// <summary>
      /// Gets or sets the position y.
      /// </summary>
      /// <value>The position y.</value>
      /// <exception cref="System.Data.StrongTypingException">The value for column \'PosY\' in table \'MouseFixations\' is DBNull.</exception>
      public double PosY
      {
        get
        {
          try
          {
            return ((double)(this[this.tableMouseFixations.PosYColumn]));
          }
          catch (global::System.InvalidCastException e)
          {
            throw new StrongTypingException("The value for column \'PosY\' in table \'MouseFixations\' is DBNull.", e);
          }
        }
        set
        {
          this[this.tableMouseFixations.PosYColumn] = value;
        }
      }



      /// <summary>
      /// Gets or sets the trials row parent.
      /// </summary>
      /// <value>The trials row parent.</value>
      public TrialsRow TrialsRowParent
      {
        get
        {
          return ((TrialsRow)(this.GetParentRow(this.Table.ParentRelations["Trials_MouseFixations"])));
        }
        set
        {
          this.SetParentRow(value, this.Table.ParentRelations["Trials_MouseFixations"]);
        }
      }



      /// <summary>
      /// Determines whether [is count in trial null].
      /// </summary>
      /// <returns><c>true</c> if [is count in trial null]; otherwise, <c>false</c>.</returns>
      public bool IsCountInTrialNull()
      {
        return this.IsNull(this.tableMouseFixations.CountInTrialColumn);
      }



      /// <summary>
      /// Sets the count in trial null.
      /// </summary>
      public void SetCountInTrialNull()
      {
        this[this.tableMouseFixations.CountInTrialColumn] = global::System.Convert.DBNull;
      }



      /// <summary>
      /// Determines whether [is start time null].
      /// </summary>
      /// <returns><c>true</c> if [is start time null]; otherwise, <c>false</c>.</returns>
      public bool IsStartTimeNull()
      {
        return this.IsNull(this.tableMouseFixations.StartTimeColumn);
      }



      /// <summary>
      /// Sets the start time null.
      /// </summary>
      public void SetStartTimeNull()
      {
        this[this.tableMouseFixations.StartTimeColumn] = global::System.Convert.DBNull;
      }



      /// <summary>
      /// Determines whether [is length null].
      /// </summary>
      /// <returns><c>true</c> if [is length null]; otherwise, <c>false</c>.</returns>
      public bool IsLengthNull()
      {
        return this.IsNull(this.tableMouseFixations.LengthColumn);
      }



      /// <summary>
      /// Sets the length null.
      /// </summary>
      public void SetLengthNull()
      {
        this[this.tableMouseFixations.LengthColumn] = global::System.Convert.DBNull;
      }



      /// <summary>
      /// Determines whether [is position x null].
      /// </summary>
      /// <returns><c>true</c> if [is position x null]; otherwise, <c>false</c>.</returns>
      public bool IsPosXNull()
      {
        return this.IsNull(this.tableMouseFixations.PosXColumn);
      }



      /// <summary>
      /// Sets the position x null.
      /// </summary>
      public void SetPosXNull()
      {
        this[this.tableMouseFixations.PosXColumn] = global::System.Convert.DBNull;
      }



      /// <summary>
      /// Determines whether [is position y null].
      /// </summary>
      /// <returns><c>true</c> if [is position y null]; otherwise, <c>false</c>.</returns>
      public bool IsPosYNull()
      {
        return this.IsNull(this.tableMouseFixations.PosYColumn);
      }



      /// <summary>
      /// Sets the position y null.
      /// </summary>
      public void SetPosYNull()
      {
        this[this.tableMouseFixations.PosYColumn] = global::System.Convert.DBNull;
      }
    }

    /// <summary>
    /// Represents strongly named DataRow class.
    /// </summary>
    public class ParamsRow : DataRow
    {

      /// <summary>
      /// The table parameters
      /// </summary>
      private ParamsDataTable tableParams;



      /// <summary>
      /// Initializes a new instance of the <see cref="ParamsRow"/> class.
      /// </summary>
      /// <param name="rb">The rb.</param>
      internal ParamsRow(DataRowBuilder rb) :
        base(rb)
      {
        this.tableParams = ((ParamsDataTable)(this.Table));
      }



      /// <summary>
      /// Gets or sets the identifier.
      /// </summary>
      /// <value>The identifier.</value>
      public int ID
      {
        get
        {
          return ((int)(this[this.tableParams.IDColumn]));
        }
        set
        {
          this[this.tableParams.IDColumn] = value;
        }
      }



      /// <summary>
      /// Gets or sets the parameter.
      /// </summary>
      /// <value>The parameter.</value>
      public string Param
      {
        get
        {
          return ((string)(this[this.tableParams.ParamColumn]));
        }
        set
        {
          this[this.tableParams.ParamColumn] = value;
        }
      }



      /// <summary>
      /// Gets or sets the description.
      /// </summary>
      /// <value>The description.</value>
      /// <exception cref="System.Data.StrongTypingException">The value for column \'Description\' in table \'Params\' is DBNull.</exception>
      public string Description
      {
        get
        {
          try
          {
            return ((string)(this[this.tableParams.DescriptionColumn]));
          }
          catch (global::System.InvalidCastException e)
          {
            throw new StrongTypingException("The value for column \'Description\' in table \'Params\' is DBNull.", e);
          }
        }
        set
        {
          this[this.tableParams.DescriptionColumn] = value;
        }
      }



      /// <summary>
      /// Determines whether [is description null].
      /// </summary>
      /// <returns><c>true</c> if [is description null]; otherwise, <c>false</c>.</returns>
      public bool IsDescriptionNull()
      {
        return this.IsNull(this.tableParams.DescriptionColumn);
      }



      /// <summary>
      /// Sets the description null.
      /// </summary>
      public void SetDescriptionNull()
      {
        this[this.tableParams.DescriptionColumn] = global::System.Convert.DBNull;
      }
    }

    /// <summary>
    /// Row event argument class
    /// </summary>

    public class SubjectParametersRowChangeEvent : global::System.EventArgs
    {

      /// <summary>
      /// The event row
      /// </summary>
      private SubjectParametersRow eventRow;

      /// <summary>
      /// The event action
      /// </summary>
      private DataRowAction eventAction;



      /// <summary>
      /// Initializes a new instance of the <see cref="SubjectParametersRowChangeEvent"/> class.
      /// </summary>
      /// <param name="row">The row.</param>
      /// <param name="action">The action.</param>
      public SubjectParametersRowChangeEvent(SubjectParametersRow row, DataRowAction action)
      {
        this.eventRow = row;
        this.eventAction = action;
      }



      /// <summary>
      /// Gets the row.
      /// </summary>
      /// <value>The row.</value>
      public SubjectParametersRow Row
      {
        get
        {
          return this.eventRow;
        }
      }



      /// <summary>
      /// Gets the action.
      /// </summary>
      /// <value>The action.</value>
      public DataRowAction Action
      {
        get
        {
          return this.eventAction;
        }
      }
    }

    /// <summary>
    /// Row event argument class
    /// </summary>

    public class SubjectsRowChangeEvent : global::System.EventArgs
    {

      /// <summary>
      /// The event row
      /// </summary>
      private SubjectsRow eventRow;

      /// <summary>
      /// The event action
      /// </summary>
      private DataRowAction eventAction;



      /// <summary>
      /// Initializes a new instance of the <see cref="SubjectsRowChangeEvent"/> class.
      /// </summary>
      /// <param name="row">The row.</param>
      /// <param name="action">The action.</param>
      public SubjectsRowChangeEvent(SubjectsRow row, DataRowAction action)
      {
        this.eventRow = row;
        this.eventAction = action;
      }



      /// <summary>
      /// Gets the row.
      /// </summary>
      /// <value>The row.</value>
      public SubjectsRow Row
      {
        get
        {
          return this.eventRow;
        }
      }



      /// <summary>
      /// Gets the action.
      /// </summary>
      /// <value>The action.</value>
      public DataRowAction Action
      {
        get
        {
          return this.eventAction;
        }
      }
    }

    /// <summary>
    /// Row event argument class
    /// </summary>

    public class TrialsRowChangeEvent : global::System.EventArgs
    {

      /// <summary>
      /// The event row
      /// </summary>
      private TrialsRow eventRow;

      /// <summary>
      /// The event action
      /// </summary>
      private DataRowAction eventAction;



      /// <summary>
      /// Initializes a new instance of the <see cref="TrialsRowChangeEvent"/> class.
      /// </summary>
      /// <param name="row">The row.</param>
      /// <param name="action">The action.</param>
      public TrialsRowChangeEvent(TrialsRow row, DataRowAction action)
      {
        this.eventRow = row;
        this.eventAction = action;
      }



      /// <summary>
      /// Gets the row.
      /// </summary>
      /// <value>The row.</value>
      public TrialsRow Row
      {
        get
        {
          return this.eventRow;
        }
      }



      /// <summary>
      /// Gets the action.
      /// </summary>
      /// <value>The action.</value>
      public DataRowAction Action
      {
        get
        {
          return this.eventAction;
        }
      }
    }

    /// <summary>
    /// Row event argument class
    /// </summary>

    public class TrialEventsRowChangeEvent : global::System.EventArgs
    {

      /// <summary>
      /// The event row
      /// </summary>
      private TrialEventsRow eventRow;

      /// <summary>
      /// The event action
      /// </summary>
      private DataRowAction eventAction;



      /// <summary>
      /// Initializes a new instance of the <see cref="TrialEventsRowChangeEvent"/> class.
      /// </summary>
      /// <param name="row">The row.</param>
      /// <param name="action">The action.</param>
      public TrialEventsRowChangeEvent(TrialEventsRow row, DataRowAction action)
      {
        this.eventRow = row;
        this.eventAction = action;
      }



      /// <summary>
      /// Gets the row.
      /// </summary>
      /// <value>The row.</value>
      public TrialEventsRow Row
      {
        get
        {
          return this.eventRow;
        }
      }



      /// <summary>
      /// Gets the action.
      /// </summary>
      /// <value>The action.</value>
      public DataRowAction Action
      {
        get
        {
          return this.eventAction;
        }
      }
    }

    /// <summary>
    /// Row event argument class
    /// </summary>

    public class RawdataRowChangeEvent : global::System.EventArgs
    {

      /// <summary>
      /// The event row
      /// </summary>
      private RawdataRow eventRow;

      /// <summary>
      /// The event action
      /// </summary>
      private DataRowAction eventAction;



      /// <summary>
      /// Initializes a new instance of the <see cref="RawdataRowChangeEvent"/> class.
      /// </summary>
      /// <param name="row">The row.</param>
      /// <param name="action">The action.</param>
      public RawdataRowChangeEvent(RawdataRow row, DataRowAction action)
      {
        this.eventRow = row;
        this.eventAction = action;
      }



      /// <summary>
      /// Gets the row.
      /// </summary>
      /// <value>The row.</value>
      public RawdataRow Row
      {
        get
        {
          return this.eventRow;
        }
      }



      /// <summary>
      /// Gets the action.
      /// </summary>
      /// <value>The action.</value>
      public DataRowAction Action
      {
        get
        {
          return this.eventAction;
        }
      }
    }

    /// <summary>
    /// Row event argument class
    /// </summary>

    public class AOIsRowChangeEvent : global::System.EventArgs
    {

      /// <summary>
      /// The event row
      /// </summary>
      private AOIsRow eventRow;

      /// <summary>
      /// The event action
      /// </summary>
      private DataRowAction eventAction;



      /// <summary>
      /// Initializes a new instance of the <see cref="AOIsRowChangeEvent"/> class.
      /// </summary>
      /// <param name="row">The row.</param>
      /// <param name="action">The action.</param>
      public AOIsRowChangeEvent(AOIsRow row, DataRowAction action)
      {
        this.eventRow = row;
        this.eventAction = action;
      }



      /// <summary>
      /// Gets the row.
      /// </summary>
      /// <value>The row.</value>
      public AOIsRow Row
      {
        get
        {
          return this.eventRow;
        }
      }



      /// <summary>
      /// Gets the action.
      /// </summary>
      /// <value>The action.</value>
      public DataRowAction Action
      {
        get
        {
          return this.eventAction;
        }
      }
    }

    /// <summary>
    /// Row event argument class
    /// </summary>

    public class ShapeGroupsRowChangeEvent : global::System.EventArgs
    {

      /// <summary>
      /// The event row
      /// </summary>
      private ShapeGroupsRow eventRow;

      /// <summary>
      /// The event action
      /// </summary>
      private DataRowAction eventAction;



      /// <summary>
      /// Initializes a new instance of the <see cref="ShapeGroupsRowChangeEvent"/> class.
      /// </summary>
      /// <param name="row">The row.</param>
      /// <param name="action">The action.</param>
      public ShapeGroupsRowChangeEvent(ShapeGroupsRow row, DataRowAction action)
      {
        this.eventRow = row;
        this.eventAction = action;
      }



      /// <summary>
      /// Gets the row.
      /// </summary>
      /// <value>The row.</value>
      public ShapeGroupsRow Row
      {
        get
        {
          return this.eventRow;
        }
      }

      /// <summary>
      /// Gets the action.
      /// </summary>
      /// <value>The action.</value>
      public DataRowAction Action
      {
        get
        {
          return this.eventAction;
        }
      }
    }

    /// <summary>
    /// Row event argument class
    /// </summary>
    public class GazeFixationsRowChangeEvent : global::System.EventArgs
    {

      /// <summary>
      /// The event row
      /// </summary>
      private GazeFixationsRow eventRow;

      /// <summary>
      /// The event action
      /// </summary>
      private DataRowAction eventAction;



      /// <summary>
      /// Initializes a new instance of the <see cref="GazeFixationsRowChangeEvent"/> class.
      /// </summary>
      /// <param name="row">The row.</param>
      /// <param name="action">The action.</param>
      public GazeFixationsRowChangeEvent(GazeFixationsRow row, DataRowAction action)
      {
        this.eventRow = row;
        this.eventAction = action;
      }



      /// <summary>
      /// Gets the row.
      /// </summary>
      /// <value>The row.</value>
      public GazeFixationsRow Row
      {
        get
        {
          return this.eventRow;
        }
      }



      /// <summary>
      /// Gets the action.
      /// </summary>
      /// <value>The action.</value>
      public DataRowAction Action
      {
        get
        {
          return this.eventAction;
        }
      }
    }

    /// <summary>
    /// Row event argument class
    /// </summary>

    public class MouseFixationsRowChangeEvent : global::System.EventArgs
    {

      /// <summary>
      /// The event row
      /// </summary>
      private MouseFixationsRow eventRow;

      /// <summary>
      /// The event action
      /// </summary>
      private DataRowAction eventAction;



      /// <summary>
      /// Initializes a new instance of the <see cref="MouseFixationsRowChangeEvent"/> class.
      /// </summary>
      /// <param name="row">The row.</param>
      /// <param name="action">The action.</param>
      public MouseFixationsRowChangeEvent(MouseFixationsRow row, DataRowAction action)
      {
        this.eventRow = row;
        this.eventAction = action;
      }



      /// <summary>
      /// Gets the row.
      /// </summary>
      /// <value>The row.</value>
      public MouseFixationsRow Row
      {
        get
        {
          return this.eventRow;
        }
      }



      /// <summary>
      /// Gets the action.
      /// </summary>
      /// <value>The action.</value>
      public DataRowAction Action
      {
        get
        {
          return this.eventAction;
        }
      }
    }

    /// <summary>
    /// Row event argument class
    /// </summary>

    public class ParamsRowChangeEvent : global::System.EventArgs
    {

      /// <summary>
      /// The event row
      /// </summary>
      private ParamsRow eventRow;

      /// <summary>
      /// The event action
      /// </summary>
      private DataRowAction eventAction;



      /// <summary>
      /// Initializes a new instance of the <see cref="ParamsRowChangeEvent"/> class.
      /// </summary>
      /// <param name="row">The row.</param>
      /// <param name="action">The action.</param>
      public ParamsRowChangeEvent(ParamsRow row, DataRowAction action)
      {
        this.eventRow = row;
        this.eventAction = action;
      }



      /// <summary>
      /// Gets the row.
      /// </summary>
      /// <value>The row.</value>
      public ParamsRow Row
      {
        get
        {
          return this.eventRow;
        }
      }



      /// <summary>
      /// Gets the action.
      /// </summary>
      /// <value>The action.</value>
      public DataRowAction Action
      {
        get
        {
          return this.eventAction;
        }
      }
    }


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
    private SQLiteTadSubjects tadSubjects;

    /// <summary>
    /// Holds TableAdapter for SubjectParametersTable
    /// </summary>
    private SQLiteTadSubjectParameters tadSubjectParameters;

    /// <summary>
    /// Holds TableAdapter for TrialTable
    /// </summary>
    private SQLiteTadTrials tadTrials;

    /// <summary>
    /// Holds TableAdapter for TrialEvents table
    /// </summary>
    private SQLiteTadTrialEvents tadTrialEvents;

    /// <summary>
    /// Holds TableAdapter for AOITable
    /// </summary>
    private SQLiteTadAOIs tadAOIs;

    /// <summary>
    /// Holds TableAdapter for GazeFixationsTable
    /// </summary>
    private SQLiteTadGazeFixations tadGazeFixations;

    /// <summary>
    /// Holds TableAdapter for MouseFixationsTable
    /// </summary>
    private SQLiteTadMouseFixations tadMouseFixations;

    /// <summary>
    /// Holds TableAdapter for ShapeGroupsTable
    /// </summary>
    private SQLiteTadShapeGroups tadShapeGroups;

    /// <summary>
    /// Holds TableAdapter for ParamsTable
    /// </summary>
    private SQLiteTadParams tadParams;

    /// <summary>
    /// Saves the connection to the database
    /// </summary>
    private SQLiteConnection sqlConnection;

    /// <summary>
    /// Holds RawTableAdapterList, can be accessed through SubjectName
    /// </summary>
    private Dictionary<string, SQLiteDataAdapter> rawDataAdapterDict = new Dictionary<string, SQLiteDataAdapter>();

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets or sets TableAdapter for SubjectsTable
    /// </summary>
    /// <value>The subjects adapter.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public SQLiteTadSubjects SubjectsAdapter
    {
      get { return this.tadSubjects; }
      set { this.tadSubjects = value; }
    }

    /// <summary>
    /// Gets or sets TableAdapter for SubjectParametersTable
    /// </summary>
    /// <value>The subject parameters adapter.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public SQLiteTadSubjectParameters SubjectParametersAdapter
    {
      get { return this.tadSubjectParameters; }
      set { this.tadSubjectParameters = value; }
    }

    /// <summary>
    /// Gets or sets TableAdapter for ParamsTable
    /// </summary>
    /// <value>The parameters adapter.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public SQLiteTadParams ParamsAdapter
    {
      get { return this.tadParams; }
      set { this.tadParams = value; }
    }

    /// <summary>
    /// Gets or sets TableAdapter for TrialsTable
    /// </summary>
    /// <value>The trials adapter.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public SQLiteTadTrials TrialsAdapter
    {
      get { return this.tadTrials; }
      set { this.tadTrials = value; }
    }

    /// <summary>
    /// Gets or sets TableAdapter for TrialEvents table
    /// </summary>
    /// <value>The trial events adapter.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public SQLiteTadTrialEvents TrialEventsAdapter
    {
      get { return this.tadTrialEvents; }
      set { this.tadTrialEvents = value; }
    }

    /// <summary>
    /// Gets or sets TableAdapter for AOITable
    /// </summary>
    /// <value>The ao is adapter.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public SQLiteTadAOIs AOIsAdapter
    {
      get { return this.tadAOIs; }
      set { this.tadAOIs = value; }
    }

    /// <summary>
    /// Gets or sets TableAdapter for GazeFixationsTable
    /// </summary>
    /// <value>The gaze fixations adapter.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public SQLiteTadGazeFixations GazeFixationsAdapter
    {
      get { return this.tadGazeFixations; }
      set { this.tadGazeFixations = value; }
    }

    /// <summary>
    /// Gets or sets TableAdapter for MouseFixationsTable
    /// </summary>
    /// <value>The mouse fixations adapter.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public SQLiteTadMouseFixations MouseFixationsAdapter
    {
      get { return this.tadMouseFixations; }
      set { this.tadMouseFixations = value; }
    }

    /// <summary>
    /// Gets or sets TableAdapter for ShapeGroupsTable
    /// </summary>
    /// <value>The shape groups adapter.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public SQLiteTadShapeGroups ShapeGroupsAdapter
    {
      get { return this.tadShapeGroups; }
      set { this.tadShapeGroups = value; }
    }

    /// <summary>
    /// Gets RawTableAdapterList, can be accessed through SubjectName.
    /// </summary>
    /// <value>The raw data adapter dictionary.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Dictionary<string, SQLiteDataAdapter> RawDataAdapterDict
    {
      get { return this.rawDataAdapterDict; }
    }

    /// <summary>
    /// Gets the <see cref="SqlConnection" /> for the SQLExpress Database.
    /// </summary>
    /// <value>The database connection.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public SQLiteConnection DatabaseConnection
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
    /// <param name="splash">A <see cref="BackgroundWorker" /> for the progress splash
    /// window which can be cancelled and restarted when showing dialogs.</param>
    /// <returns><strong>True</strong> if loading was successful, otherwise
    /// <strong>false</strong>.</returns>
    public bool LoadData(BackgroundWorker splash)
    {
      try
      {
        this.sqlConnection = new SQLiteConnection(Document.ActiveDocument.ExperimentSettings.DatabaseConnectionString);
        this.sqlConnection.Open();

        this.DataSetName = "OgamaDataSet";
        //// this.SchemaSerializationMode = SchemaSerializationMode.IncludeSchema;

        // tadSubjects
        this.tadSubjects = new SQLiteTadSubjects();
        this.tadSubjects.ClearBeforeFill = true;
        this.tadSubjects.Connection = this.sqlConnection;

        // tadSubjectParameters
        this.tadSubjectParameters = new SQLiteTadSubjectParameters();
        this.tadSubjectParameters.ClearBeforeFill = true;
        this.tadSubjectParameters.Connection = this.sqlConnection;

        // tadParams
        this.tadParams = new SQLiteTadParams();
        this.tadParams.ClearBeforeFill = true;
        this.tadParams.Connection = this.sqlConnection;

        // tadTrials
        this.tadTrials = new SQLiteTadTrials();
        this.tadTrials.ClearBeforeFill = true;
        this.tadTrials.Connection = this.sqlConnection;

        // tadTrialEvents
        this.tadTrialEvents = new SQLiteTadTrialEvents();
        this.tadTrialEvents.ClearBeforeFill = true;
        this.tadTrialEvents.Connection = this.sqlConnection;

        // tadAOIs
        this.tadAOIs = new SQLiteTadAOIs();
        this.tadAOIs.ClearBeforeFill = true;
        this.tadAOIs.Connection = this.sqlConnection;

        // tadGazeFixations
        this.tadGazeFixations = new SQLiteTadGazeFixations();
        this.tadGazeFixations.ClearBeforeFill = true;
        this.tadGazeFixations.Connection = this.sqlConnection;

        // tadMouseFixations
        this.tadMouseFixations = new SQLiteTadMouseFixations();
        this.tadMouseFixations.ClearBeforeFill = true;
        this.tadMouseFixations.Connection = this.sqlConnection;

        // tadShapeGroups
        this.tadShapeGroups = new SQLiteTadShapeGroups();
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
    /// Create raw data table adapter and add it to AdapterDictionary
    /// </summary>
    public void CreateRawDataAdapters()
    {
      this.rawDataAdapterDict.Clear();
      foreach (DataRow subjectRow in this.Subjects.Rows)
      {
        string subjectName = subjectRow["SubjectName"].ToString();
        SQLiteDataAdapter adapter = this.CreateAdapterBySubject(subjectName);
        this.rawDataAdapterDict.Add(subjectName, adapter);
      }
    }

    /// <summary>
    /// Creates a RawDataTableAdapter for a given subject
    /// </summary>
    /// <param name="subject">SubjectName as given in subjects table</param>
    /// <returns><see cref="SqlDataAdapter" /> that can be used for queries</returns>
    public SQLiteDataAdapter CreateAdapterBySubject(string subject)
    {
      var adapter = new SQLiteDataAdapter();
      var tableMapping = new System.Data.Common.DataTableMapping();
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

      foreach (SQLiteDataAdapter rawAdapter in this.rawDataAdapterDict.Values)
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
    /// The <see cref="BackgroundWorker.DoWork" /> event handler for
    /// the local <see cref="BackgroundWorker" /> bgwUpgradeSplash.
    /// Shows the <see cref="UpgradeDocumentSplash" /> form with a splash screen
    /// wait dialog.
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">A <see cref="DoWorkEventArgs" /> with the event data.</param>
    private void bgwUpgradeSplash_DoWork(object sender, DoWorkEventArgs e)
    {
      // Get the BackgroundWorker that raised this event.
      var worker = sender as BackgroundWorker;

      var newSplash = new UpgradeDocumentSplash();
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
    /// <param name="splash">A <see cref="BackgroundWorker" /> for the progress splash
    /// window which can be cancelled and restarted when showing dialogs.</param>
    /// <returns><strong>True</strong> if upgrade was successful, and <strong>false</strong>
    /// if unsuccessful or cancelled by user.</returns>
    private bool UpgradeDatabase(BackgroundWorker splash)
    {
      if (Queries.ColumnExists("Subjects", "Param1") || Queries.TableExists("TableMainExperiment"))
      {
        // Create splash worker for progress splash.
        var bgwUpgradeSplash = new BackgroundWorker();
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
          "Upgrade successful",
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
    /// <param name="stimulusFiles">A <see cref="List{String}" /> with the stimulus files.</param>
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
    /// <returns>A <see cref="List{String}" /> with all stimuli files of the
    /// experiment sorted by appearance in the database.</returns>
    private List<string> GetStimulusFiles()
    {
      var trialIDs = new List<string>();

      if (Queries.ColumnExists("Trials", "StimulusFile"))
      {
        const string TrialQueryString = "SELECT Trials.* FROM [Trials]";
        var trialAdapter = new SQLiteDataAdapter
        {
          SelectCommand = new SQLiteCommand(TrialQueryString, this.sqlConnection)
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
    /// <param name="tableName">A <see cref="String" /> with the name of the table with
    /// at least has a filled column 'StimulusFile' which is filled and an empty
    /// column 'TrialID' which should be filled.</param>
    /// <param name="stimulusFiles">A <see cref="List{String}" /> with the stimulus files.</param>
    private void AddTrialIDsforStimulusFiles(string tableName, List<string> stimulusFiles)
    {
      if (Queries.ColumnExists(tableName, "StimulusFile") && Queries.ColumnExists(tableName, "TrialID"))
      {
        string queryString = "SELECT " + tableName + ".* FROM " + tableName;
        var adapter = new SQLiteDataAdapter();
        adapter.SelectCommand = new SQLiteCommand(queryString, this.sqlConnection);

        var table = new DataTable(tableName);
        adapter.Fill(table);

        for (int i = 0; i < table.Rows.Count; i++)
        {
          string stimulusFile = table.Rows[i]["StimulusFile"].ToString();
          table.Rows[i]["TrialID"] = stimulusFiles.IndexOf(stimulusFile);
        }

        adapter.Update(table);

        //// Commit changes back to database by erasing is first
        //// and then bulk copy the whole table
        //string deleteQuery = "DELETE FROM dbo." + tableName;
        //Queries.ExecuteSQLCommand(deleteQuery);

        //using (var cmd = new SQLiteCommand(this.sqlConnection))
        //{
        //  using (var transaction = this.sqlConnection.BeginTransaction())
        //  {
        //    // 100,000 inserts
        //    for (var i = 0; i < 1000000; i++)
        //    {
        //      cmd.CommandText =
        //          "INSERT INTO Person (FirstName, LastName) VALUES ('John', 'Doe');";
        //      cmd.ExecuteNonQuery();
        //    }

        //    transaction.Commit();
        //  }
        //}

        //using (var bcp = new SQlite(this.sqlConnection))
        //{
        //  bcp.DestinationTableName = "dbo." + tableName;

        //  // Write the data table back to the database
        //  bcp.WriteToServer(table);
        //}
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

      const string QueryString = "SELECT AOIs.* FROM [AOIs] WHERE [Target] <> ''";
      var adapter = new SQLiteDataAdapter { SelectCommand = new SQLiteCommand(QueryString, this.sqlConnection) };
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
      const string QueryString = "SELECT AOIs.* FROM [AOIs]";
      var adapter = new SQLiteDataAdapter
      {
        SelectCommand = new SQLiteCommand(QueryString, this.sqlConnection)
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
    /// <param name="splash">A <see cref="BackgroundWorker" /> for the progress splash
    /// window which can be cancelled and restarted when showing dialogs.</param>
    private void UpgradeResponses(BackgroundWorker splash)
    {
      if (Queries.ColumnExists("Trials", "SubjectsResponse"))
      {
        string queryString = "SELECT Trials.* FROM [Trials]";
        var adapter = new SQLiteDataAdapter();
        adapter.SelectCommand = new SQLiteCommand(queryString, this.sqlConnection);
        var trialsTable = new DataTable("Trials");
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
            " responses could not be converted successfully." + Environment.NewLine +
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
            string queryString = "SELECT " + tableName + ".* FROM dbo." + tableName +
              " WHERE (Response IS NOT NULL AND Response <> 'None') OR FlashState <> ''";
            var adapter = new SQLiteDataAdapter();
            adapter.SelectCommand = new SQLiteCommand(queryString, this.sqlConnection);

            var builder = new SQLiteCommandBuilder(adapter);
            builder.QuotePrefix = "[";
            builder.QuoteSuffix = "]";

            var rawTable = new DataTable(tableName);
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
          " responses could not be converted successfully." + Environment.NewLine +
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
        string queryString = "SELECT Subjects.* FROM [Subjects]";
        var adapter = new SQLiteDataAdapter();
        adapter.SelectCommand = new SQLiteCommand(queryString, this.sqlConnection);
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
      string queryString = "SELECT SubjectParameters.* FROM [SubjectParameters]";
      var adapter = new SQLiteDataAdapter();
      adapter.SelectCommand = new SQLiteCommand(queryString, this.sqlConnection);
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
        string queryString = "CREATE TABLE [ShapeGroups]([ID] [int] IDENTITY(1,1) NOT NULL,[ShapeGroup] [varchar](50) COLLATE Latin1_General_CI_AS NOT NULL, CONSTRAINT [PK_ShapeGroups] PRIMARY KEY CLUSTERED ([ID] ASC)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]) ON [PRIMARY];";
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
        string queryString = "CREATE TABLE [Params]([ID] [int] IDENTITY(1,1) NOT NULL,[Param] [varchar](50) COLLATE Latin1_General_CI_AS NOT NULL, [Description] [varchar](400) COLLATE Latin1_General_CI_AS NULL, CONSTRAINT [PK_Params] PRIMARY KEY CLUSTERED ([ID] ASC)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]) ON [PRIMARY];";
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
        string queryString = "CREATE TABLE [SubjectParameters]( [ID] [int] IDENTITY(1,1) NOT NULL, [SubjectName] [varchar](50) COLLATE Latin1_General_CI_AS NOT NULL, [Param] [varchar](50) COLLATE Latin1_General_CI_AS NOT NULL, [ParamValue] [varchar](500) COLLATE Latin1_General_CI_AS NULL, CONSTRAINT [PK_SubjectParameters] PRIMARY KEY CLUSTERED ( [ID] ASC)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]) ON [PRIMARY];";
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
        string queryString = "CREATE TABLE [TrialEvents]( [ID] [int] IDENTITY(1,1) NOT NULL, [SubjectName] [varchar](50) COLLATE Latin1_General_CI_AS NOT NULL, [TrialSequence] [int] NOT NULL, [EventID] [int] NOT NULL, [EventTime] [bigint] NOT NULL, [EventType] [varchar](50) COLLATE Latin1_General_CI_AS NOT NULL, [EventTask] [varchar](50) COLLATE Latin1_General_CI_AS NULL, [EventParam] [varchar](4000) COLLATE Latin1_General_CI_AS NULL, CONSTRAINT [PK_Events] PRIMARY KEY CLUSTERED ( [ID] ASC)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]) ON [PRIMARY]";
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
        string queryString = "CREATE TABLE [MouseFixations]([ID] [bigint] IDENTITY(1,1) NOT NULL,[SubjectName] [varchar](50) COLLATE Latin1_General_CI_AS NOT NULL,[TrialID] [int] NOT NULL,[TrialSequence] [int] NOT NULL,[CountInTrial] [int] NULL,[StartTime] [bigint] NULL,[Length] [int] NULL,[PosX] [float] NULL,[PosY] [float] NULL,CONSTRAINT [PK_TableMouseFixations] PRIMARY KEY CLUSTERED ( [ID] ASC)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]) ON [PRIMARY]";
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
        string queryString = "CREATE TABLE [GazeFixations]([ID] [bigint] IDENTITY(1,1) NOT NULL,[SubjectName] [varchar](50) COLLATE Latin1_General_CI_AS NOT NULL,[TrialID] [int] NOT NULL,[TrialSequence] [int] NOT NULL,[CountInTrial] [int] NULL,[StartTime] [bigint] NULL,[Length] [int] NULL,[PosX] [float] NULL,[PosY] [float] NULL,CONSTRAINT [PK_TableGazeFixations] PRIMARY KEY CLUSTERED ( [ID] ASC )WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]) ON [PRIMARY]";
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
