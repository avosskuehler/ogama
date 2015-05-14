// <copyright file="OgamaDataSet.cs" company="FU Berlin">
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
    private SubjectParametersDataTable tableSubjectParameters;

    private SubjectsDataTable tableSubjects;

    private TrialsDataTable tableTrials;

    private TrialEventsDataTable tableTrialEvents;

    private RawdataDataTable tableRawdata;

    private AOIsDataTable tableAOIs;

    private ShapeGroupsDataTable tableShapeGroups;

    private GazeFixationsDataTable tableGazeFixations;

    private MouseFixationsDataTable tableMouseFixations;

    private ParamsDataTable tableParams;

    private DataRelation relationFK_Subjects_SubjectParameters;

    private DataRelation relationFK_Subjects_Trials;

    private DataRelation relationFK_Trials_Events;

    private DataRelation relationTrials_AOIs;

    private DataRelation relationShapeGroups_AOIs;

    private DataRelation relationTrials_GazeFixations;

    private DataRelation relationTrials_MouseFixations;

    private SchemaSerializationMode _schemaSerializationMode = SchemaSerializationMode.IncludeSchema;

    public SQLiteOgamaDataSet()
    {
      this.BeginInit();
      this.InitClass();
      global::System.ComponentModel.CollectionChangeEventHandler schemaChangedHandler = new global::System.ComponentModel.CollectionChangeEventHandler(this.SchemaChanged);
      base.Tables.CollectionChanged += schemaChangedHandler;
      base.Relations.CollectionChanged += schemaChangedHandler;
      this.EndInit();
    }

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

    public SubjectParametersDataTable SubjectParameters
    {
      get
      {
        return this.tableSubjectParameters;
      }
    }

    public SubjectsDataTable Subjects
    {
      get
      {
        return this.tableSubjects;
      }
    }

    public TrialsDataTable Trials
    {
      get
      {
        return this.tableTrials;
      }
    }

    public TrialEventsDataTable TrialEvents
    {
      get
      {
        return this.tableTrialEvents;
      }
    }

    public RawdataDataTable Rawdata
    {
      get
      {
        return this.tableRawdata;
      }
    }

    public AOIsDataTable AOIs
    {
      get
      {
        return this.tableAOIs;
      }
    }

    public ShapeGroupsDataTable ShapeGroups
    {
      get
      {
        return this.tableShapeGroups;
      }
    }

    public GazeFixationsDataTable GazeFixations
    {
      get
      {
        return this.tableGazeFixations;
      }
    }

    public MouseFixationsDataTable MouseFixations
    {
      get
      {
        return this.tableMouseFixations;
      }
    }

    public ParamsDataTable Params
    {
      get
      {
        return this.tableParams;
      }
    }

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

    public new DataTableCollection Tables
    {
      get
      {
        return base.Tables;
      }
    }

    public new DataRelationCollection Relations
    {
      get
      {
        return base.Relations;
      }
    }

    protected override void InitializeDerivedDataSet()
    {
      this.BeginInit();
      this.InitClass();
      this.EndInit();
    }

    public override DataSet Clone()
    {
      SQLiteOgamaDataSet cln = ((SQLiteOgamaDataSet)(base.Clone()));
      cln.InitVars();
      cln.SchemaSerializationMode = this.SchemaSerializationMode;
      return cln;
    }



    protected override bool ShouldSerializeTables()
    {
      return false;
    }



    protected override bool ShouldSerializeRelations()
    {
      return false;
    }



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



    protected override global::System.Xml.Schema.XmlSchema GetSchemaSerializable()
    {
      global::System.IO.MemoryStream stream = new global::System.IO.MemoryStream();
      this.WriteXmlSchema(new global::System.Xml.XmlTextWriter(stream, null));
      stream.Position = 0;
      return global::System.Xml.Schema.XmlSchema.Read(new global::System.Xml.XmlTextReader(stream), null);
    }



    internal void InitVars()
    {
      this.InitVars(true);
    }



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



    private bool ShouldSerializeSubjectParameters()
    {
      return false;
    }



    private bool ShouldSerializeSubjects()
    {
      return false;
    }



    private bool ShouldSerializeTrials()
    {
      return false;
    }



    private bool ShouldSerializeTrialEvents()
    {
      return false;
    }



    private bool ShouldSerializeRawdata()
    {
      return false;
    }



    private bool ShouldSerializeAOIs()
    {
      return false;
    }



    private bool ShouldSerializeShapeGroups()
    {
      return false;
    }



    private bool ShouldSerializeGazeFixations()
    {
      return false;
    }



    private bool ShouldSerializeMouseFixations()
    {
      return false;
    }



    private bool ShouldSerializeParams()
    {
      return false;
    }



    private void SchemaChanged(object sender, global::System.ComponentModel.CollectionChangeEventArgs e)
    {
      if ((e.Action == global::System.ComponentModel.CollectionChangeAction.Remove))
      {
        this.InitVars();
      }
    }



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


    public delegate void SubjectParametersRowChangeEventHandler(object sender, SubjectParametersRowChangeEvent e);


    public delegate void SubjectsRowChangeEventHandler(object sender, SubjectsRowChangeEvent e);


    public delegate void TrialsRowChangeEventHandler(object sender, TrialsRowChangeEvent e);


    public delegate void TrialEventsRowChangeEventHandler(object sender, TrialEventsRowChangeEvent e);


    public delegate void RawdataRowChangeEventHandler(object sender, RawdataRowChangeEvent e);


    public delegate void AOIsRowChangeEventHandler(object sender, AOIsRowChangeEvent e);


    public delegate void ShapeGroupsRowChangeEventHandler(object sender, ShapeGroupsRowChangeEvent e);


    public delegate void GazeFixationsRowChangeEventHandler(object sender, GazeFixationsRowChangeEvent e);


    public delegate void MouseFixationsRowChangeEventHandler(object sender, MouseFixationsRowChangeEvent e);


    public delegate void ParamsRowChangeEventHandler(object sender, ParamsRowChangeEvent e);

    /// <summary>
    ///Represents the strongly named DataTable class.
    ///</summary>
    [global::System.Serializable()]
    [global::System.Xml.Serialization.XmlSchemaProviderAttribute("GetTypedTableSchema")]
    public partial class SubjectParametersDataTable : TypedTableBase<SubjectParametersRow>
    {

      private DataColumn columnID;

      private DataColumn columnSubjectName;

      private DataColumn columnParam;

      private DataColumn columnParamValue;



      public SubjectParametersDataTable()
      {
        this.TableName = "SubjectParameters";
        this.BeginInit();
        this.InitClass();
        this.EndInit();
      }



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



      protected SubjectParametersDataTable(global::System.Runtime.Serialization.SerializationInfo info, global::System.Runtime.Serialization.StreamingContext context) :
        base(info, context)
      {
        this.InitVars();
      }



      public DataColumn IDColumn
      {
        get
        {
          return this.columnID;
        }
      }



      public DataColumn SubjectNameColumn
      {
        get
        {
          return this.columnSubjectName;
        }
      }



      public DataColumn ParamColumn
      {
        get
        {
          return this.columnParam;
        }
      }



      public DataColumn ParamValueColumn
      {
        get
        {
          return this.columnParamValue;
        }
      }




      public int Count
      {
        get
        {
          return this.Rows.Count;
        }
      }



      public SubjectParametersRow this[int index]
      {
        get
        {
          return ((SubjectParametersRow)(this.Rows[index]));
        }
      }


      public event SubjectParametersRowChangeEventHandler SubjectParametersRowChanging;


      public event SubjectParametersRowChangeEventHandler SubjectParametersRowChanged;


      public event SubjectParametersRowChangeEventHandler SubjectParametersRowDeleting;


      public event SubjectParametersRowChangeEventHandler SubjectParametersRowDeleted;



      public void AddSubjectParametersRow(SubjectParametersRow row)
      {
        this.Rows.Add(row);
      }



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



      public SubjectParametersRow FindByID(int ID)
      {
        return ((SubjectParametersRow)(this.Rows.Find(new object[] {
                            ID})));
      }



      public override DataTable Clone()
      {
        SubjectParametersDataTable cln = ((SubjectParametersDataTable)(base.Clone()));
        cln.InitVars();
        return cln;
      }



      protected override DataTable CreateInstance()
      {
        return new SubjectParametersDataTable();
      }



      internal void InitVars()
      {
        this.columnID = base.Columns["ID"];
        this.columnSubjectName = base.Columns["SubjectName"];
        this.columnParam = base.Columns["Param"];
        this.columnParamValue = base.Columns["ParamValue"];
      }



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



      public SubjectParametersRow NewSubjectParametersRow()
      {
        return ((SubjectParametersRow)(this.NewRow()));
      }



      protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
      {
        return new SubjectParametersRow(builder);
      }



      protected override global::System.Type GetRowType()
      {
        return typeof(SubjectParametersRow);
      }



      protected override void OnRowChanged(DataRowChangeEventArgs e)
      {
        base.OnRowChanged(e);
        if ((this.SubjectParametersRowChanged != null))
        {
          this.SubjectParametersRowChanged(this, new SubjectParametersRowChangeEvent(((SubjectParametersRow)(e.Row)), e.Action));
        }
      }



      protected override void OnRowChanging(DataRowChangeEventArgs e)
      {
        base.OnRowChanging(e);
        if ((this.SubjectParametersRowChanging != null))
        {
          this.SubjectParametersRowChanging(this, new SubjectParametersRowChangeEvent(((SubjectParametersRow)(e.Row)), e.Action));
        }
      }



      protected override void OnRowDeleted(DataRowChangeEventArgs e)
      {
        base.OnRowDeleted(e);
        if ((this.SubjectParametersRowDeleted != null))
        {
          this.SubjectParametersRowDeleted(this, new SubjectParametersRowChangeEvent(((SubjectParametersRow)(e.Row)), e.Action));
        }
      }



      protected override void OnRowDeleting(DataRowChangeEventArgs e)
      {
        base.OnRowDeleting(e);
        if ((this.SubjectParametersRowDeleting != null))
        {
          this.SubjectParametersRowDeleting(this, new SubjectParametersRowChangeEvent(((SubjectParametersRow)(e.Row)), e.Action));
        }
      }



      public void RemoveSubjectParametersRow(SubjectParametersRow row)
      {
        this.Rows.Remove(row);
      }



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
    ///Represents the strongly named DataTable class.
    ///</summary>
    [global::System.Serializable()]
    [global::System.Xml.Serialization.XmlSchemaProviderAttribute("GetTypedTableSchema")]
    public partial class SubjectsDataTable : TypedTableBase<SubjectsRow>
    {

      private DataColumn columnID;

      private DataColumn columnSubjectName;

      private DataColumn columnCategory;

      private DataColumn columnAge;

      private DataColumn columnSex;

      private DataColumn columnHandedness;

      private DataColumn columnComments;



      public SubjectsDataTable()
      {
        this.TableName = "Subjects";
        this.BeginInit();
        this.InitClass();
        this.EndInit();
      }



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



      protected SubjectsDataTable(global::System.Runtime.Serialization.SerializationInfo info, global::System.Runtime.Serialization.StreamingContext context) :
        base(info, context)
      {
        this.InitVars();
      }



      public DataColumn IDColumn
      {
        get
        {
          return this.columnID;
        }
      }



      public DataColumn SubjectNameColumn
      {
        get
        {
          return this.columnSubjectName;
        }
      }



      public DataColumn CategoryColumn
      {
        get
        {
          return this.columnCategory;
        }
      }



      public DataColumn AgeColumn
      {
        get
        {
          return this.columnAge;
        }
      }



      public DataColumn SexColumn
      {
        get
        {
          return this.columnSex;
        }
      }



      public DataColumn HandednessColumn
      {
        get
        {
          return this.columnHandedness;
        }
      }



      public DataColumn CommentsColumn
      {
        get
        {
          return this.columnComments;
        }
      }




      public int Count
      {
        get
        {
          return this.Rows.Count;
        }
      }



      public SubjectsRow this[int index]
      {
        get
        {
          return ((SubjectsRow)(this.Rows[index]));
        }
      }


      public event SubjectsRowChangeEventHandler SubjectsRowChanging;


      public event SubjectsRowChangeEventHandler SubjectsRowChanged;


      public event SubjectsRowChangeEventHandler SubjectsRowDeleting;


      public event SubjectsRowChangeEventHandler SubjectsRowDeleted;



      public void AddSubjectsRow(SubjectsRow row)
      {
        this.Rows.Add(row);
      }



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



      public SubjectsRow FindByID(long ID)
      {
        return ((SubjectsRow)(this.Rows.Find(new object[] {
                            ID})));
      }



      public override DataTable Clone()
      {
        SubjectsDataTable cln = ((SubjectsDataTable)(base.Clone()));
        cln.InitVars();
        return cln;
      }



      protected override DataTable CreateInstance()
      {
        return new SubjectsDataTable();
      }



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



      public SubjectsRow NewSubjectsRow()
      {
        return ((SubjectsRow)(this.NewRow()));
      }



      protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
      {
        return new SubjectsRow(builder);
      }



      protected override global::System.Type GetRowType()
      {
        return typeof(SubjectsRow);
      }



      protected override void OnRowChanged(DataRowChangeEventArgs e)
      {
        base.OnRowChanged(e);
        if ((this.SubjectsRowChanged != null))
        {
          this.SubjectsRowChanged(this, new SubjectsRowChangeEvent(((SubjectsRow)(e.Row)), e.Action));
        }
      }



      protected override void OnRowChanging(DataRowChangeEventArgs e)
      {
        base.OnRowChanging(e);
        if ((this.SubjectsRowChanging != null))
        {
          this.SubjectsRowChanging(this, new SubjectsRowChangeEvent(((SubjectsRow)(e.Row)), e.Action));
        }
      }



      protected override void OnRowDeleted(DataRowChangeEventArgs e)
      {
        base.OnRowDeleted(e);
        if ((this.SubjectsRowDeleted != null))
        {
          this.SubjectsRowDeleted(this, new SubjectsRowChangeEvent(((SubjectsRow)(e.Row)), e.Action));
        }
      }



      protected override void OnRowDeleting(DataRowChangeEventArgs e)
      {
        base.OnRowDeleting(e);
        if ((this.SubjectsRowDeleting != null))
        {
          this.SubjectsRowDeleting(this, new SubjectsRowChangeEvent(((SubjectsRow)(e.Row)), e.Action));
        }
      }



      public void RemoveSubjectsRow(SubjectsRow row)
      {
        this.Rows.Remove(row);
      }



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
    ///Represents the strongly named DataTable class.
    ///</summary>
    [global::System.Serializable()]
    [global::System.Xml.Serialization.XmlSchemaProviderAttribute("GetTypedTableSchema")]
    public partial class TrialsDataTable : TypedTableBase<TrialsRow>
    {

      private DataColumn columnID;

      private DataColumn columnSubjectName;

      private DataColumn columnTrialID;

      private DataColumn columnTrialName;

      private DataColumn columnTrialSequence;

      private DataColumn columnCategory;

      private DataColumn columnTrialStartTime;

      private DataColumn columnDuration;

      private DataColumn columnEliminateData;



      public TrialsDataTable()
      {
        this.TableName = "Trials";
        this.BeginInit();
        this.InitClass();
        this.EndInit();
      }



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



      protected TrialsDataTable(global::System.Runtime.Serialization.SerializationInfo info, global::System.Runtime.Serialization.StreamingContext context) :
        base(info, context)
      {
        this.InitVars();
      }



      public DataColumn IDColumn
      {
        get
        {
          return this.columnID;
        }
      }



      public DataColumn SubjectNameColumn
      {
        get
        {
          return this.columnSubjectName;
        }
      }



      public DataColumn TrialIDColumn
      {
        get
        {
          return this.columnTrialID;
        }
      }



      public DataColumn TrialNameColumn
      {
        get
        {
          return this.columnTrialName;
        }
      }



      public DataColumn TrialSequenceColumn
      {
        get
        {
          return this.columnTrialSequence;
        }
      }



      public DataColumn CategoryColumn
      {
        get
        {
          return this.columnCategory;
        }
      }



      public DataColumn TrialStartTimeColumn
      {
        get
        {
          return this.columnTrialStartTime;
        }
      }



      public DataColumn DurationColumn
      {
        get
        {
          return this.columnDuration;
        }
      }



      public DataColumn EliminateDataColumn
      {
        get
        {
          return this.columnEliminateData;
        }
      }




      public int Count
      {
        get
        {
          return this.Rows.Count;
        }
      }



      public TrialsRow this[int index]
      {
        get
        {
          return ((TrialsRow)(this.Rows[index]));
        }
      }


      public event TrialsRowChangeEventHandler TrialsRowChanging;


      public event TrialsRowChangeEventHandler TrialsRowChanged;


      public event TrialsRowChangeEventHandler TrialsRowDeleting;


      public event TrialsRowChangeEventHandler TrialsRowDeleted;



      public void AddTrialsRow(TrialsRow row)
      {
        this.Rows.Add(row);
      }



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



      public TrialsRow FindByID(long ID)
      {
        return ((TrialsRow)(this.Rows.Find(new object[] {
                            ID})));
      }



      public override DataTable Clone()
      {
        TrialsDataTable cln = ((TrialsDataTable)(base.Clone()));
        cln.InitVars();
        return cln;
      }



      protected override DataTable CreateInstance()
      {
        return new TrialsDataTable();
      }



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



      public TrialsRow NewTrialsRow()
      {
        return ((TrialsRow)(this.NewRow()));
      }



      protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
      {
        return new TrialsRow(builder);
      }



      protected override global::System.Type GetRowType()
      {
        return typeof(TrialsRow);
      }



      protected override void OnRowChanged(DataRowChangeEventArgs e)
      {
        base.OnRowChanged(e);
        if ((this.TrialsRowChanged != null))
        {
          this.TrialsRowChanged(this, new TrialsRowChangeEvent(((TrialsRow)(e.Row)), e.Action));
        }
      }



      protected override void OnRowChanging(DataRowChangeEventArgs e)
      {
        base.OnRowChanging(e);
        if ((this.TrialsRowChanging != null))
        {
          this.TrialsRowChanging(this, new TrialsRowChangeEvent(((TrialsRow)(e.Row)), e.Action));
        }
      }



      protected override void OnRowDeleted(DataRowChangeEventArgs e)
      {
        base.OnRowDeleted(e);
        if ((this.TrialsRowDeleted != null))
        {
          this.TrialsRowDeleted(this, new TrialsRowChangeEvent(((TrialsRow)(e.Row)), e.Action));
        }
      }



      protected override void OnRowDeleting(DataRowChangeEventArgs e)
      {
        base.OnRowDeleting(e);
        if ((this.TrialsRowDeleting != null))
        {
          this.TrialsRowDeleting(this, new TrialsRowChangeEvent(((TrialsRow)(e.Row)), e.Action));
        }
      }



      public void RemoveTrialsRow(TrialsRow row)
      {
        this.Rows.Remove(row);
      }



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
    ///Represents the strongly named DataTable class.
    ///</summary>
    [global::System.Serializable()]
    [global::System.Xml.Serialization.XmlSchemaProviderAttribute("GetTypedTableSchema")]
    public partial class TrialEventsDataTable : TypedTableBase<TrialEventsRow>
    {

      private DataColumn columnID;

      private DataColumn columnSubjectName;

      private DataColumn columnTrialSequence;

      private DataColumn columnEventID;

      private DataColumn columnEventTime;

      private DataColumn columnEventType;

      private DataColumn columnEventTask;

      private DataColumn columnEventParam;



      public TrialEventsDataTable()
      {
        this.TableName = "TrialEvents";
        this.BeginInit();
        this.InitClass();
        this.EndInit();
      }



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



      protected TrialEventsDataTable(global::System.Runtime.Serialization.SerializationInfo info, global::System.Runtime.Serialization.StreamingContext context) :
        base(info, context)
      {
        this.InitVars();
      }



      public DataColumn IDColumn
      {
        get
        {
          return this.columnID;
        }
      }



      public DataColumn SubjectNameColumn
      {
        get
        {
          return this.columnSubjectName;
        }
      }



      public DataColumn TrialSequenceColumn
      {
        get
        {
          return this.columnTrialSequence;
        }
      }



      public DataColumn EventIDColumn
      {
        get
        {
          return this.columnEventID;
        }
      }



      public DataColumn EventTimeColumn
      {
        get
        {
          return this.columnEventTime;
        }
      }



      public DataColumn EventTypeColumn
      {
        get
        {
          return this.columnEventType;
        }
      }



      public DataColumn EventTaskColumn
      {
        get
        {
          return this.columnEventTask;
        }
      }



      public DataColumn EventParamColumn
      {
        get
        {
          return this.columnEventParam;
        }
      }




      public int Count
      {
        get
        {
          return this.Rows.Count;
        }
      }



      public TrialEventsRow this[int index]
      {
        get
        {
          return ((TrialEventsRow)(this.Rows[index]));
        }
      }


      public event TrialEventsRowChangeEventHandler TrialEventsRowChanging;


      public event TrialEventsRowChangeEventHandler TrialEventsRowChanged;


      public event TrialEventsRowChangeEventHandler TrialEventsRowDeleting;


      public event TrialEventsRowChangeEventHandler TrialEventsRowDeleted;



      public void AddTrialEventsRow(TrialEventsRow row)
      {
        this.Rows.Add(row);
      }



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



      public TrialEventsRow FindByID(int ID)
      {
        return ((TrialEventsRow)(this.Rows.Find(new object[] {
                            ID})));
      }



      public override DataTable Clone()
      {
        TrialEventsDataTable cln = ((TrialEventsDataTable)(base.Clone()));
        cln.InitVars();
        return cln;
      }



      protected override DataTable CreateInstance()
      {
        return new TrialEventsDataTable();
      }



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



      public TrialEventsRow NewTrialEventsRow()
      {
        return ((TrialEventsRow)(this.NewRow()));
      }



      protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
      {
        return new TrialEventsRow(builder);
      }



      protected override global::System.Type GetRowType()
      {
        return typeof(TrialEventsRow);
      }



      protected override void OnRowChanged(DataRowChangeEventArgs e)
      {
        base.OnRowChanged(e);
        if ((this.TrialEventsRowChanged != null))
        {
          this.TrialEventsRowChanged(this, new TrialEventsRowChangeEvent(((TrialEventsRow)(e.Row)), e.Action));
        }
      }



      protected override void OnRowChanging(DataRowChangeEventArgs e)
      {
        base.OnRowChanging(e);
        if ((this.TrialEventsRowChanging != null))
        {
          this.TrialEventsRowChanging(this, new TrialEventsRowChangeEvent(((TrialEventsRow)(e.Row)), e.Action));
        }
      }



      protected override void OnRowDeleted(DataRowChangeEventArgs e)
      {
        base.OnRowDeleted(e);
        if ((this.TrialEventsRowDeleted != null))
        {
          this.TrialEventsRowDeleted(this, new TrialEventsRowChangeEvent(((TrialEventsRow)(e.Row)), e.Action));
        }
      }



      protected override void OnRowDeleting(DataRowChangeEventArgs e)
      {
        base.OnRowDeleting(e);
        if ((this.TrialEventsRowDeleting != null))
        {
          this.TrialEventsRowDeleting(this, new TrialEventsRowChangeEvent(((TrialEventsRow)(e.Row)), e.Action));
        }
      }



      public void RemoveTrialEventsRow(TrialEventsRow row)
      {
        this.Rows.Remove(row);
      }



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
    ///Represents the strongly named DataTable class.
    ///</summary>
    [global::System.Serializable()]
    [global::System.Xml.Serialization.XmlSchemaProviderAttribute("GetTypedTableSchema")]
    public partial class RawdataDataTable : TypedTableBase<RawdataRow>
    {

      private DataColumn columnID;

      private DataColumn columnSubjectName;

      private DataColumn columnTrialSequence;

      private DataColumn columnTime;

      private DataColumn columnPupilDiaX;

      private DataColumn columnPupilDiaY;

      private DataColumn columnGazePosX;

      private DataColumn columnGazePosY;

      private DataColumn columnMousePosX;

      private DataColumn columnMousePosY;

      private DataColumn columnEventID;



      public RawdataDataTable()
      {
        this.TableName = "Rawdata";
        this.BeginInit();
        this.InitClass();
        this.EndInit();
      }



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



      protected RawdataDataTable(global::System.Runtime.Serialization.SerializationInfo info, global::System.Runtime.Serialization.StreamingContext context) :
        base(info, context)
      {
        this.InitVars();
      }



      public DataColumn IDColumn
      {
        get
        {
          return this.columnID;
        }
      }



      public DataColumn SubjectNameColumn
      {
        get
        {
          return this.columnSubjectName;
        }
      }



      public DataColumn TrialSequenceColumn
      {
        get
        {
          return this.columnTrialSequence;
        }
      }



      public DataColumn TimeColumn
      {
        get
        {
          return this.columnTime;
        }
      }



      public DataColumn PupilDiaXColumn
      {
        get
        {
          return this.columnPupilDiaX;
        }
      }



      public DataColumn PupilDiaYColumn
      {
        get
        {
          return this.columnPupilDiaY;
        }
      }



      public DataColumn GazePosXColumn
      {
        get
        {
          return this.columnGazePosX;
        }
      }



      public DataColumn GazePosYColumn
      {
        get
        {
          return this.columnGazePosY;
        }
      }



      public DataColumn MousePosXColumn
      {
        get
        {
          return this.columnMousePosX;
        }
      }



      public DataColumn MousePosYColumn
      {
        get
        {
          return this.columnMousePosY;
        }
      }



      public DataColumn EventIDColumn
      {
        get
        {
          return this.columnEventID;
        }
      }




      public int Count
      {
        get
        {
          return this.Rows.Count;
        }
      }



      public RawdataRow this[int index]
      {
        get
        {
          return ((RawdataRow)(this.Rows[index]));
        }
      }


      public event RawdataRowChangeEventHandler RawdataRowChanging;


      public event RawdataRowChangeEventHandler RawdataRowChanged;


      public event RawdataRowChangeEventHandler RawdataRowDeleting;


      public event RawdataRowChangeEventHandler RawdataRowDeleted;



      public void AddRawdataRow(RawdataRow row)
      {
        this.Rows.Add(row);
      }



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



      public RawdataRow FindByID(long ID)
      {
        return ((RawdataRow)(this.Rows.Find(new object[] {
                            ID})));
      }



      public override DataTable Clone()
      {
        RawdataDataTable cln = ((RawdataDataTable)(base.Clone()));
        cln.InitVars();
        return cln;
      }



      protected override DataTable CreateInstance()
      {
        return new RawdataDataTable();
      }



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



      public RawdataRow NewRawdataRow()
      {
        return ((RawdataRow)(this.NewRow()));
      }



      protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
      {
        return new RawdataRow(builder);
      }



      protected override global::System.Type GetRowType()
      {
        return typeof(RawdataRow);
      }



      protected override void OnRowChanged(DataRowChangeEventArgs e)
      {
        base.OnRowChanged(e);
        if ((this.RawdataRowChanged != null))
        {
          this.RawdataRowChanged(this, new RawdataRowChangeEvent(((RawdataRow)(e.Row)), e.Action));
        }
      }



      protected override void OnRowChanging(DataRowChangeEventArgs e)
      {
        base.OnRowChanging(e);
        if ((this.RawdataRowChanging != null))
        {
          this.RawdataRowChanging(this, new RawdataRowChangeEvent(((RawdataRow)(e.Row)), e.Action));
        }
      }



      protected override void OnRowDeleted(DataRowChangeEventArgs e)
      {
        base.OnRowDeleted(e);
        if ((this.RawdataRowDeleted != null))
        {
          this.RawdataRowDeleted(this, new RawdataRowChangeEvent(((RawdataRow)(e.Row)), e.Action));
        }
      }



      protected override void OnRowDeleting(DataRowChangeEventArgs e)
      {
        base.OnRowDeleting(e);
        if ((this.RawdataRowDeleting != null))
        {
          this.RawdataRowDeleting(this, new RawdataRowChangeEvent(((RawdataRow)(e.Row)), e.Action));
        }
      }



      public void RemoveRawdataRow(RawdataRow row)
      {
        this.Rows.Remove(row);
      }



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
    ///Represents the strongly named DataTable class.
    ///</summary>
    [global::System.Serializable()]
    [global::System.Xml.Serialization.XmlSchemaProviderAttribute("GetTypedTableSchema")]
    public partial class AOIsDataTable : TypedTableBase<AOIsRow>
    {

      private DataColumn columnID;

      private DataColumn columnTrialID;

      private DataColumn columnSlideNr;

      private DataColumn columnShapeName;

      private DataColumn columnShapeType;

      private DataColumn columnShapeNumPts;

      private DataColumn columnShapePts;

      private DataColumn columnShapeGroup;



      public AOIsDataTable()
      {
        this.TableName = "AOIs";
        this.BeginInit();
        this.InitClass();
        this.EndInit();
      }



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



      protected AOIsDataTable(global::System.Runtime.Serialization.SerializationInfo info, global::System.Runtime.Serialization.StreamingContext context) :
        base(info, context)
      {
        this.InitVars();
      }



      public DataColumn IDColumn
      {
        get
        {
          return this.columnID;
        }
      }



      public DataColumn TrialIDColumn
      {
        get
        {
          return this.columnTrialID;
        }
      }



      public DataColumn SlideNrColumn
      {
        get
        {
          return this.columnSlideNr;
        }
      }



      public DataColumn ShapeNameColumn
      {
        get
        {
          return this.columnShapeName;
        }
      }



      public DataColumn ShapeTypeColumn
      {
        get
        {
          return this.columnShapeType;
        }
      }



      public DataColumn ShapeNumPtsColumn
      {
        get
        {
          return this.columnShapeNumPts;
        }
      }



      public DataColumn ShapePtsColumn
      {
        get
        {
          return this.columnShapePts;
        }
      }



      public DataColumn ShapeGroupColumn
      {
        get
        {
          return this.columnShapeGroup;
        }
      }




      public int Count
      {
        get
        {
          return this.Rows.Count;
        }
      }



      public AOIsRow this[int index]
      {
        get
        {
          return ((AOIsRow)(this.Rows[index]));
        }
      }


      public event AOIsRowChangeEventHandler AOIsRowChanging;


      public event AOIsRowChangeEventHandler AOIsRowChanged;


      public event AOIsRowChangeEventHandler AOIsRowDeleting;


      public event AOIsRowChangeEventHandler AOIsRowDeleted;



      public void AddAOIsRow(AOIsRow row)
      {
        this.Rows.Add(row);
      }



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



      public AOIsRow FindByID(int ID)
      {
        return ((AOIsRow)(this.Rows.Find(new object[] {
                            ID})));
      }



      public override DataTable Clone()
      {
        AOIsDataTable cln = ((AOIsDataTable)(base.Clone()));
        cln.InitVars();
        return cln;
      }



      protected override DataTable CreateInstance()
      {
        return new AOIsDataTable();
      }



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



      public AOIsRow NewAOIsRow()
      {
        return ((AOIsRow)(this.NewRow()));
      }



      protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
      {
        return new AOIsRow(builder);
      }



      protected override global::System.Type GetRowType()
      {
        return typeof(AOIsRow);
      }



      protected override void OnRowChanged(DataRowChangeEventArgs e)
      {
        base.OnRowChanged(e);
        if ((this.AOIsRowChanged != null))
        {
          this.AOIsRowChanged(this, new AOIsRowChangeEvent(((AOIsRow)(e.Row)), e.Action));
        }
      }



      protected override void OnRowChanging(DataRowChangeEventArgs e)
      {
        base.OnRowChanging(e);
        if ((this.AOIsRowChanging != null))
        {
          this.AOIsRowChanging(this, new AOIsRowChangeEvent(((AOIsRow)(e.Row)), e.Action));
        }
      }



      protected override void OnRowDeleted(DataRowChangeEventArgs e)
      {
        base.OnRowDeleted(e);
        if ((this.AOIsRowDeleted != null))
        {
          this.AOIsRowDeleted(this, new AOIsRowChangeEvent(((AOIsRow)(e.Row)), e.Action));
        }
      }



      protected override void OnRowDeleting(DataRowChangeEventArgs e)
      {
        base.OnRowDeleting(e);
        if ((this.AOIsRowDeleting != null))
        {
          this.AOIsRowDeleting(this, new AOIsRowChangeEvent(((AOIsRow)(e.Row)), e.Action));
        }
      }



      public void RemoveAOIsRow(AOIsRow row)
      {
        this.Rows.Remove(row);
      }



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
    ///Represents the strongly named DataTable class.
    ///</summary>
    [global::System.Serializable()]
    [global::System.Xml.Serialization.XmlSchemaProviderAttribute("GetTypedTableSchema")]
    public partial class ShapeGroupsDataTable : TypedTableBase<ShapeGroupsRow>
    {

      private DataColumn columnID;

      private DataColumn columnShapeGroup;



      public ShapeGroupsDataTable()
      {
        this.TableName = "ShapeGroups";
        this.BeginInit();
        this.InitClass();
        this.EndInit();
      }



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



      protected ShapeGroupsDataTable(global::System.Runtime.Serialization.SerializationInfo info, global::System.Runtime.Serialization.StreamingContext context) :
        base(info, context)
      {
        this.InitVars();
      }



      public DataColumn IDColumn
      {
        get
        {
          return this.columnID;
        }
      }



      public DataColumn ShapeGroupColumn
      {
        get
        {
          return this.columnShapeGroup;
        }
      }




      public int Count
      {
        get
        {
          return this.Rows.Count;
        }
      }



      public ShapeGroupsRow this[int index]
      {
        get
        {
          return ((ShapeGroupsRow)(this.Rows[index]));
        }
      }


      public event ShapeGroupsRowChangeEventHandler ShapeGroupsRowChanging;


      public event ShapeGroupsRowChangeEventHandler ShapeGroupsRowChanged;


      public event ShapeGroupsRowChangeEventHandler ShapeGroupsRowDeleting;


      public event ShapeGroupsRowChangeEventHandler ShapeGroupsRowDeleted;



      public void AddShapeGroupsRow(ShapeGroupsRow row)
      {
        this.Rows.Add(row);
      }



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



      public ShapeGroupsRow FindByID(int ID)
      {
        return ((ShapeGroupsRow)(this.Rows.Find(new object[] {
                            ID})));
      }



      public override DataTable Clone()
      {
        ShapeGroupsDataTable cln = ((ShapeGroupsDataTable)(base.Clone()));
        cln.InitVars();
        return cln;
      }



      protected override DataTable CreateInstance()
      {
        return new ShapeGroupsDataTable();
      }



      internal void InitVars()
      {
        this.columnID = base.Columns["ID"];
        this.columnShapeGroup = base.Columns["ShapeGroup"];
      }



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



      public ShapeGroupsRow NewShapeGroupsRow()
      {
        return ((ShapeGroupsRow)(this.NewRow()));
      }



      protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
      {
        return new ShapeGroupsRow(builder);
      }



      protected override global::System.Type GetRowType()
      {
        return typeof(ShapeGroupsRow);
      }



      protected override void OnRowChanged(DataRowChangeEventArgs e)
      {
        base.OnRowChanged(e);
        if ((this.ShapeGroupsRowChanged != null))
        {
          this.ShapeGroupsRowChanged(this, new ShapeGroupsRowChangeEvent(((ShapeGroupsRow)(e.Row)), e.Action));
        }
      }



      protected override void OnRowChanging(DataRowChangeEventArgs e)
      {
        base.OnRowChanging(e);
        if ((this.ShapeGroupsRowChanging != null))
        {
          this.ShapeGroupsRowChanging(this, new ShapeGroupsRowChangeEvent(((ShapeGroupsRow)(e.Row)), e.Action));
        }
      }



      protected override void OnRowDeleted(DataRowChangeEventArgs e)
      {
        base.OnRowDeleted(e);
        if ((this.ShapeGroupsRowDeleted != null))
        {
          this.ShapeGroupsRowDeleted(this, new ShapeGroupsRowChangeEvent(((ShapeGroupsRow)(e.Row)), e.Action));
        }
      }



      protected override void OnRowDeleting(DataRowChangeEventArgs e)
      {
        base.OnRowDeleting(e);
        if ((this.ShapeGroupsRowDeleting != null))
        {
          this.ShapeGroupsRowDeleting(this, new ShapeGroupsRowChangeEvent(((ShapeGroupsRow)(e.Row)), e.Action));
        }
      }



      public void RemoveShapeGroupsRow(ShapeGroupsRow row)
      {
        this.Rows.Remove(row);
      }



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
    ///Represents the strongly named DataTable class.
    ///</summary>
    [global::System.Serializable()]
    [global::System.Xml.Serialization.XmlSchemaProviderAttribute("GetTypedTableSchema")]
    public partial class GazeFixationsDataTable : TypedTableBase<GazeFixationsRow>
    {

      private DataColumn columnID;

      private DataColumn columnSubjectName;

      private DataColumn columnTrialID;

      private DataColumn columnTrialSequence;

      private DataColumn columnCountInTrial;

      private DataColumn columnStartTime;

      private DataColumn columnLength;

      private DataColumn columnPosX;

      private DataColumn columnPosY;



      public GazeFixationsDataTable()
      {
        this.TableName = "GazeFixations";
        this.BeginInit();
        this.InitClass();
        this.EndInit();
      }



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



      protected GazeFixationsDataTable(global::System.Runtime.Serialization.SerializationInfo info, global::System.Runtime.Serialization.StreamingContext context) :
        base(info, context)
      {
        this.InitVars();
      }



      public DataColumn IDColumn
      {
        get
        {
          return this.columnID;
        }
      }



      public DataColumn SubjectNameColumn
      {
        get
        {
          return this.columnSubjectName;
        }
      }



      public DataColumn TrialIDColumn
      {
        get
        {
          return this.columnTrialID;
        }
      }



      public DataColumn TrialSequenceColumn
      {
        get
        {
          return this.columnTrialSequence;
        }
      }



      public DataColumn CountInTrialColumn
      {
        get
        {
          return this.columnCountInTrial;
        }
      }



      public DataColumn StartTimeColumn
      {
        get
        {
          return this.columnStartTime;
        }
      }



      public DataColumn LengthColumn
      {
        get
        {
          return this.columnLength;
        }
      }



      public DataColumn PosXColumn
      {
        get
        {
          return this.columnPosX;
        }
      }



      public DataColumn PosYColumn
      {
        get
        {
          return this.columnPosY;
        }
      }




      public int Count
      {
        get
        {
          return this.Rows.Count;
        }
      }



      public GazeFixationsRow this[int index]
      {
        get
        {
          return ((GazeFixationsRow)(this.Rows[index]));
        }
      }


      public event GazeFixationsRowChangeEventHandler GazeFixationsRowChanging;


      public event GazeFixationsRowChangeEventHandler GazeFixationsRowChanged;


      public event GazeFixationsRowChangeEventHandler GazeFixationsRowDeleting;


      public event GazeFixationsRowChangeEventHandler GazeFixationsRowDeleted;



      public void AddGazeFixationsRow(GazeFixationsRow row)
      {
        this.Rows.Add(row);
      }



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



      public GazeFixationsRow FindByID(long ID)
      {
        return ((GazeFixationsRow)(this.Rows.Find(new object[] {
                            ID})));
      }



      public override DataTable Clone()
      {
        GazeFixationsDataTable cln = ((GazeFixationsDataTable)(base.Clone()));
        cln.InitVars();
        return cln;
      }



      protected override DataTable CreateInstance()
      {
        return new GazeFixationsDataTable();
      }



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



      public GazeFixationsRow NewGazeFixationsRow()
      {
        return ((GazeFixationsRow)(this.NewRow()));
      }



      protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
      {
        return new GazeFixationsRow(builder);
      }



      protected override global::System.Type GetRowType()
      {
        return typeof(GazeFixationsRow);
      }



      protected override void OnRowChanged(DataRowChangeEventArgs e)
      {
        base.OnRowChanged(e);
        if ((this.GazeFixationsRowChanged != null))
        {
          this.GazeFixationsRowChanged(this, new GazeFixationsRowChangeEvent(((GazeFixationsRow)(e.Row)), e.Action));
        }
      }



      protected override void OnRowChanging(DataRowChangeEventArgs e)
      {
        base.OnRowChanging(e);
        if ((this.GazeFixationsRowChanging != null))
        {
          this.GazeFixationsRowChanging(this, new GazeFixationsRowChangeEvent(((GazeFixationsRow)(e.Row)), e.Action));
        }
      }



      protected override void OnRowDeleted(DataRowChangeEventArgs e)
      {
        base.OnRowDeleted(e);
        if ((this.GazeFixationsRowDeleted != null))
        {
          this.GazeFixationsRowDeleted(this, new GazeFixationsRowChangeEvent(((GazeFixationsRow)(e.Row)), e.Action));
        }
      }



      protected override void OnRowDeleting(DataRowChangeEventArgs e)
      {
        base.OnRowDeleting(e);
        if ((this.GazeFixationsRowDeleting != null))
        {
          this.GazeFixationsRowDeleting(this, new GazeFixationsRowChangeEvent(((GazeFixationsRow)(e.Row)), e.Action));
        }
      }



      public void RemoveGazeFixationsRow(GazeFixationsRow row)
      {
        this.Rows.Remove(row);
      }



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
    ///Represents the strongly named DataTable class.
    ///</summary>
    [global::System.Serializable()]
    [global::System.Xml.Serialization.XmlSchemaProviderAttribute("GetTypedTableSchema")]
    public partial class MouseFixationsDataTable : TypedTableBase<MouseFixationsRow>
    {

      private DataColumn columnID;

      private DataColumn columnSubjectName;

      private DataColumn columnTrialID;

      private DataColumn columnTrialSequence;

      private DataColumn columnCountInTrial;

      private DataColumn columnStartTime;

      private DataColumn columnLength;

      private DataColumn columnPosX;

      private DataColumn columnPosY;



      public MouseFixationsDataTable()
      {
        this.TableName = "MouseFixations";
        this.BeginInit();
        this.InitClass();
        this.EndInit();
      }



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



      protected MouseFixationsDataTable(global::System.Runtime.Serialization.SerializationInfo info, global::System.Runtime.Serialization.StreamingContext context) :
        base(info, context)
      {
        this.InitVars();
      }



      public DataColumn IDColumn
      {
        get
        {
          return this.columnID;
        }
      }



      public DataColumn SubjectNameColumn
      {
        get
        {
          return this.columnSubjectName;
        }
      }



      public DataColumn TrialIDColumn
      {
        get
        {
          return this.columnTrialID;
        }
      }



      public DataColumn TrialSequenceColumn
      {
        get
        {
          return this.columnTrialSequence;
        }
      }



      public DataColumn CountInTrialColumn
      {
        get
        {
          return this.columnCountInTrial;
        }
      }



      public DataColumn StartTimeColumn
      {
        get
        {
          return this.columnStartTime;
        }
      }



      public DataColumn LengthColumn
      {
        get
        {
          return this.columnLength;
        }
      }



      public DataColumn PosXColumn
      {
        get
        {
          return this.columnPosX;
        }
      }



      public DataColumn PosYColumn
      {
        get
        {
          return this.columnPosY;
        }
      }




      public int Count
      {
        get
        {
          return this.Rows.Count;
        }
      }



      public MouseFixationsRow this[int index]
      {
        get
        {
          return ((MouseFixationsRow)(this.Rows[index]));
        }
      }


      public event MouseFixationsRowChangeEventHandler MouseFixationsRowChanging;


      public event MouseFixationsRowChangeEventHandler MouseFixationsRowChanged;


      public event MouseFixationsRowChangeEventHandler MouseFixationsRowDeleting;


      public event MouseFixationsRowChangeEventHandler MouseFixationsRowDeleted;



      public void AddMouseFixationsRow(MouseFixationsRow row)
      {
        this.Rows.Add(row);
      }



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



      public MouseFixationsRow FindByID(long ID)
      {
        return ((MouseFixationsRow)(this.Rows.Find(new object[] {
                            ID})));
      }



      public override DataTable Clone()
      {
        MouseFixationsDataTable cln = ((MouseFixationsDataTable)(base.Clone()));
        cln.InitVars();
        return cln;
      }



      protected override DataTable CreateInstance()
      {
        return new MouseFixationsDataTable();
      }



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



      public MouseFixationsRow NewMouseFixationsRow()
      {
        return ((MouseFixationsRow)(this.NewRow()));
      }



      protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
      {
        return new MouseFixationsRow(builder);
      }



      protected override global::System.Type GetRowType()
      {
        return typeof(MouseFixationsRow);
      }



      protected override void OnRowChanged(DataRowChangeEventArgs e)
      {
        base.OnRowChanged(e);
        if ((this.MouseFixationsRowChanged != null))
        {
          this.MouseFixationsRowChanged(this, new MouseFixationsRowChangeEvent(((MouseFixationsRow)(e.Row)), e.Action));
        }
      }



      protected override void OnRowChanging(DataRowChangeEventArgs e)
      {
        base.OnRowChanging(e);
        if ((this.MouseFixationsRowChanging != null))
        {
          this.MouseFixationsRowChanging(this, new MouseFixationsRowChangeEvent(((MouseFixationsRow)(e.Row)), e.Action));
        }
      }



      protected override void OnRowDeleted(DataRowChangeEventArgs e)
      {
        base.OnRowDeleted(e);
        if ((this.MouseFixationsRowDeleted != null))
        {
          this.MouseFixationsRowDeleted(this, new MouseFixationsRowChangeEvent(((MouseFixationsRow)(e.Row)), e.Action));
        }
      }



      protected override void OnRowDeleting(DataRowChangeEventArgs e)
      {
        base.OnRowDeleting(e);
        if ((this.MouseFixationsRowDeleting != null))
        {
          this.MouseFixationsRowDeleting(this, new MouseFixationsRowChangeEvent(((MouseFixationsRow)(e.Row)), e.Action));
        }
      }



      public void RemoveMouseFixationsRow(MouseFixationsRow row)
      {
        this.Rows.Remove(row);
      }



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
    ///Represents the strongly named DataTable class.
    ///</summary>
    [global::System.Serializable()]
    [global::System.Xml.Serialization.XmlSchemaProviderAttribute("GetTypedTableSchema")]
    public partial class ParamsDataTable : TypedTableBase<ParamsRow>
    {

      private DataColumn columnID;

      private DataColumn columnParam;

      private DataColumn columnDescription;



      public ParamsDataTable()
      {
        this.TableName = "Params";
        this.BeginInit();
        this.InitClass();
        this.EndInit();
      }



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



      protected ParamsDataTable(global::System.Runtime.Serialization.SerializationInfo info, global::System.Runtime.Serialization.StreamingContext context) :
        base(info, context)
      {
        this.InitVars();
      }



      public DataColumn IDColumn
      {
        get
        {
          return this.columnID;
        }
      }



      public DataColumn ParamColumn
      {
        get
        {
          return this.columnParam;
        }
      }



      public DataColumn DescriptionColumn
      {
        get
        {
          return this.columnDescription;
        }
      }




      public int Count
      {
        get
        {
          return this.Rows.Count;
        }
      }



      public ParamsRow this[int index]
      {
        get
        {
          return ((ParamsRow)(this.Rows[index]));
        }
      }


      public event ParamsRowChangeEventHandler ParamsRowChanging;


      public event ParamsRowChangeEventHandler ParamsRowChanged;


      public event ParamsRowChangeEventHandler ParamsRowDeleting;


      public event ParamsRowChangeEventHandler ParamsRowDeleted;



      public void AddParamsRow(ParamsRow row)
      {
        this.Rows.Add(row);
      }



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



      public ParamsRow FindByID(int ID)
      {
        return ((ParamsRow)(this.Rows.Find(new object[] {
                            ID})));
      }



      public override DataTable Clone()
      {
        ParamsDataTable cln = ((ParamsDataTable)(base.Clone()));
        cln.InitVars();
        return cln;
      }



      protected override DataTable CreateInstance()
      {
        return new ParamsDataTable();
      }



      internal void InitVars()
      {
        this.columnID = base.Columns["ID"];
        this.columnParam = base.Columns["Param"];
        this.columnDescription = base.Columns["Description"];
      }



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



      public ParamsRow NewParamsRow()
      {
        return ((ParamsRow)(this.NewRow()));
      }



      protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
      {
        return new ParamsRow(builder);
      }



      protected override global::System.Type GetRowType()
      {
        return typeof(ParamsRow);
      }



      protected override void OnRowChanged(DataRowChangeEventArgs e)
      {
        base.OnRowChanged(e);
        if ((this.ParamsRowChanged != null))
        {
          this.ParamsRowChanged(this, new ParamsRowChangeEvent(((ParamsRow)(e.Row)), e.Action));
        }
      }



      protected override void OnRowChanging(DataRowChangeEventArgs e)
      {
        base.OnRowChanging(e);
        if ((this.ParamsRowChanging != null))
        {
          this.ParamsRowChanging(this, new ParamsRowChangeEvent(((ParamsRow)(e.Row)), e.Action));
        }
      }



      protected override void OnRowDeleted(DataRowChangeEventArgs e)
      {
        base.OnRowDeleted(e);
        if ((this.ParamsRowDeleted != null))
        {
          this.ParamsRowDeleted(this, new ParamsRowChangeEvent(((ParamsRow)(e.Row)), e.Action));
        }
      }



      protected override void OnRowDeleting(DataRowChangeEventArgs e)
      {
        base.OnRowDeleting(e);
        if ((this.ParamsRowDeleting != null))
        {
          this.ParamsRowDeleting(this, new ParamsRowChangeEvent(((ParamsRow)(e.Row)), e.Action));
        }
      }



      public void RemoveParamsRow(ParamsRow row)
      {
        this.Rows.Remove(row);
      }



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
    ///Represents strongly named DataRow class.
    ///</summary>
    public partial class SubjectParametersRow : DataRow
    {

      private SubjectParametersDataTable tableSubjectParameters;



      internal SubjectParametersRow(DataRowBuilder rb) :
        base(rb)
      {
        this.tableSubjectParameters = ((SubjectParametersDataTable)(this.Table));
      }



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



      public bool IsParamValueNull()
      {
        return this.IsNull(this.tableSubjectParameters.ParamValueColumn);
      }



      public void SetParamValueNull()
      {
        this[this.tableSubjectParameters.ParamValueColumn] = global::System.Convert.DBNull;
      }
    }

    /// <summary>
    ///Represents strongly named DataRow class.
    ///</summary>
    public partial class SubjectsRow : DataRow
    {

      private SubjectsDataTable tableSubjects;



      internal SubjectsRow(DataRowBuilder rb) :
        base(rb)
      {
        this.tableSubjects = ((SubjectsDataTable)(this.Table));
      }



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



      public bool IsCategoryNull()
      {
        return this.IsNull(this.tableSubjects.CategoryColumn);
      }



      public void SetCategoryNull()
      {
        this[this.tableSubjects.CategoryColumn] = global::System.Convert.DBNull;
      }



      public bool IsAgeNull()
      {
        return this.IsNull(this.tableSubjects.AgeColumn);
      }



      public void SetAgeNull()
      {
        this[this.tableSubjects.AgeColumn] = global::System.Convert.DBNull;
      }



      public bool IsSexNull()
      {
        return this.IsNull(this.tableSubjects.SexColumn);
      }



      public void SetSexNull()
      {
        this[this.tableSubjects.SexColumn] = global::System.Convert.DBNull;
      }



      public bool IsHandednessNull()
      {
        return this.IsNull(this.tableSubjects.HandednessColumn);
      }



      public void SetHandednessNull()
      {
        this[this.tableSubjects.HandednessColumn] = global::System.Convert.DBNull;
      }



      public bool IsCommentsNull()
      {
        return this.IsNull(this.tableSubjects.CommentsColumn);
      }



      public void SetCommentsNull()
      {
        this[this.tableSubjects.CommentsColumn] = global::System.Convert.DBNull;
      }



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
    ///Represents strongly named DataRow class.
    ///</summary>
    public partial class TrialsRow : DataRow
    {

      private TrialsDataTable tableTrials;



      internal TrialsRow(DataRowBuilder rb) :
        base(rb)
      {
        this.tableTrials = ((TrialsDataTable)(this.Table));
      }



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



      public bool IsTrialNameNull()
      {
        return this.IsNull(this.tableTrials.TrialNameColumn);
      }



      public void SetTrialNameNull()
      {
        this[this.tableTrials.TrialNameColumn] = global::System.Convert.DBNull;
      }



      public bool IsCategoryNull()
      {
        return this.IsNull(this.tableTrials.CategoryColumn);
      }



      public void SetCategoryNull()
      {
        this[this.tableTrials.CategoryColumn] = global::System.Convert.DBNull;
      }



      public bool IsTrialStartTimeNull()
      {
        return this.IsNull(this.tableTrials.TrialStartTimeColumn);
      }



      public void SetTrialStartTimeNull()
      {
        this[this.tableTrials.TrialStartTimeColumn] = global::System.Convert.DBNull;
      }



      public bool IsDurationNull()
      {
        return this.IsNull(this.tableTrials.DurationColumn);
      }



      public void SetDurationNull()
      {
        this[this.tableTrials.DurationColumn] = global::System.Convert.DBNull;
      }



      public bool IsEliminateDataNull()
      {
        return this.IsNull(this.tableTrials.EliminateDataColumn);
      }



      public void SetEliminateDataNull()
      {
        this[this.tableTrials.EliminateDataColumn] = global::System.Convert.DBNull;
      }



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
    ///Represents strongly named DataRow class.
    ///</summary>
    public partial class TrialEventsRow : DataRow
    {

      private TrialEventsDataTable tableTrialEvents;



      internal TrialEventsRow(DataRowBuilder rb) :
        base(rb)
      {
        this.tableTrialEvents = ((TrialEventsDataTable)(this.Table));
      }



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



      public bool IsEventTaskNull()
      {
        return this.IsNull(this.tableTrialEvents.EventTaskColumn);
      }



      public void SetEventTaskNull()
      {
        this[this.tableTrialEvents.EventTaskColumn] = global::System.Convert.DBNull;
      }



      public bool IsEventParamNull()
      {
        return this.IsNull(this.tableTrialEvents.EventParamColumn);
      }



      public void SetEventParamNull()
      {
        this[this.tableTrialEvents.EventParamColumn] = global::System.Convert.DBNull;
      }
    }

    /// <summary>
    ///Represents strongly named DataRow class.
    ///</summary>
    public partial class RawdataRow : DataRow
    {

      private RawdataDataTable tableRawdata;



      internal RawdataRow(DataRowBuilder rb) :
        base(rb)
      {
        this.tableRawdata = ((RawdataDataTable)(this.Table));
      }



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



      public double PupilDiaX
      {
        get
        {
          try
          {
            return ((double)(this[this.tableRawdata.PupilDiaXColumn]));
          }
          catch (global::System.InvalidCastException e)
          {
            throw new StrongTypingException("The value for column \'PupilDiaX\' in table \'Rawdata\' is DBNull.", e);
          }
        }
        set
        {
          this[this.tableRawdata.PupilDiaXColumn] = value;
        }
      }



      public double PupilDiaY
      {
        get
        {
          try
          {
            return ((double)(this[this.tableRawdata.PupilDiaYColumn]));
          }
          catch (global::System.InvalidCastException e)
          {
            throw new StrongTypingException("The value for column \'PupilDiaY\' in table \'Rawdata\' is DBNull.", e);
          }
        }
        set
        {
          this[this.tableRawdata.PupilDiaYColumn] = value;
        }
      }



      public double GazePosX
      {
        get
        {
          try
          {
            return ((double)(this[this.tableRawdata.GazePosXColumn]));
          }
          catch (global::System.InvalidCastException e)
          {
            throw new StrongTypingException("The value for column \'GazePosX\' in table \'Rawdata\' is DBNull.", e);
          }
        }
        set
        {
          this[this.tableRawdata.GazePosXColumn] = value;
        }
      }



      public double GazePosY
      {
        get
        {
          try
          {
            return ((double)(this[this.tableRawdata.GazePosYColumn]));
          }
          catch (global::System.InvalidCastException e)
          {
            throw new StrongTypingException("The value for column \'GazePosY\' in table \'Rawdata\' is DBNull.", e);
          }
        }
        set
        {
          this[this.tableRawdata.GazePosYColumn] = value;
        }
      }



      public double MousePosX
      {
        get
        {
          try
          {
            return ((double)(this[this.tableRawdata.MousePosXColumn]));
          }
          catch (global::System.InvalidCastException e)
          {
            throw new StrongTypingException("The value for column \'MousePosX\' in table \'Rawdata\' is DBNull.", e);
          }
        }
        set
        {
          this[this.tableRawdata.MousePosXColumn] = value;
        }
      }



      public double MousePosY
      {
        get
        {
          try
          {
            return ((double)(this[this.tableRawdata.MousePosYColumn]));
          }
          catch (global::System.InvalidCastException e)
          {
            throw new StrongTypingException("The value for column \'MousePosY\' in table \'Rawdata\' is DBNull.", e);
          }
        }
        set
        {
          this[this.tableRawdata.MousePosYColumn] = value;
        }
      }



      public int EventID
      {
        get
        {
          try
          {
            return ((int)(this[this.tableRawdata.EventIDColumn]));
          }
          catch (global::System.InvalidCastException e)
          {
            throw new StrongTypingException("The value for column \'EventID\' in table \'Rawdata\' is DBNull.", e);
          }
        }
        set
        {
          this[this.tableRawdata.EventIDColumn] = value;
        }
      }



      public bool IsPupilDiaXNull()
      {
        return this.IsNull(this.tableRawdata.PupilDiaXColumn);
      }



      public void SetPupilDiaXNull()
      {
        this[this.tableRawdata.PupilDiaXColumn] = global::System.Convert.DBNull;
      }



      public bool IsPupilDiaYNull()
      {
        return this.IsNull(this.tableRawdata.PupilDiaYColumn);
      }



      public void SetPupilDiaYNull()
      {
        this[this.tableRawdata.PupilDiaYColumn] = global::System.Convert.DBNull;
      }



      public bool IsGazePosXNull()
      {
        return this.IsNull(this.tableRawdata.GazePosXColumn);
      }



      public void SetGazePosXNull()
      {
        this[this.tableRawdata.GazePosXColumn] = global::System.Convert.DBNull;
      }



      public bool IsGazePosYNull()
      {
        return this.IsNull(this.tableRawdata.GazePosYColumn);
      }



      public void SetGazePosYNull()
      {
        this[this.tableRawdata.GazePosYColumn] = global::System.Convert.DBNull;
      }



      public bool IsMousePosXNull()
      {
        return this.IsNull(this.tableRawdata.MousePosXColumn);
      }



      public void SetMousePosXNull()
      {
        this[this.tableRawdata.MousePosXColumn] = global::System.Convert.DBNull;
      }



      public bool IsMousePosYNull()
      {
        return this.IsNull(this.tableRawdata.MousePosYColumn);
      }



      public void SetMousePosYNull()
      {
        this[this.tableRawdata.MousePosYColumn] = global::System.Convert.DBNull;
      }



      public bool IsEventIDNull()
      {
        return this.IsNull(this.tableRawdata.EventIDColumn);
      }



      public void SetEventIDNull()
      {
        this[this.tableRawdata.EventIDColumn] = global::System.Convert.DBNull;
      }
    }

    /// <summary>
    ///Represents strongly named DataRow class.
    ///</summary>
    public partial class AOIsRow : DataRow
    {

      private AOIsDataTable tableAOIs;



      internal AOIsRow(DataRowBuilder rb) :
        base(rb)
      {
        this.tableAOIs = ((AOIsDataTable)(this.Table));
      }



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



      public bool IsShapeNameNull()
      {
        return this.IsNull(this.tableAOIs.ShapeNameColumn);
      }



      public void SetShapeNameNull()
      {
        this[this.tableAOIs.ShapeNameColumn] = global::System.Convert.DBNull;
      }



      public bool IsShapeTypeNull()
      {
        return this.IsNull(this.tableAOIs.ShapeTypeColumn);
      }



      public void SetShapeTypeNull()
      {
        this[this.tableAOIs.ShapeTypeColumn] = global::System.Convert.DBNull;
      }



      public bool IsShapeNumPtsNull()
      {
        return this.IsNull(this.tableAOIs.ShapeNumPtsColumn);
      }



      public void SetShapeNumPtsNull()
      {
        this[this.tableAOIs.ShapeNumPtsColumn] = global::System.Convert.DBNull;
      }



      public bool IsShapePtsNull()
      {
        return this.IsNull(this.tableAOIs.ShapePtsColumn);
      }



      public void SetShapePtsNull()
      {
        this[this.tableAOIs.ShapePtsColumn] = global::System.Convert.DBNull;
      }



      public bool IsShapeGroupNull()
      {
        return this.IsNull(this.tableAOIs.ShapeGroupColumn);
      }



      public void SetShapeGroupNull()
      {
        this[this.tableAOIs.ShapeGroupColumn] = global::System.Convert.DBNull;
      }
    }

    /// <summary>
    ///Represents strongly named DataRow class.
    ///</summary>
    public partial class ShapeGroupsRow : DataRow
    {

      private ShapeGroupsDataTable tableShapeGroups;



      internal ShapeGroupsRow(DataRowBuilder rb) :
        base(rb)
      {
        this.tableShapeGroups = ((ShapeGroupsDataTable)(this.Table));
      }



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
    ///Represents strongly named DataRow class.
    ///</summary>
    public partial class GazeFixationsRow : DataRow
    {

      private GazeFixationsDataTable tableGazeFixations;



      internal GazeFixationsRow(DataRowBuilder rb) :
        base(rb)
      {
        this.tableGazeFixations = ((GazeFixationsDataTable)(this.Table));
      }



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



      public bool IsCountInTrialNull()
      {
        return this.IsNull(this.tableGazeFixations.CountInTrialColumn);
      }



      public void SetCountInTrialNull()
      {
        this[this.tableGazeFixations.CountInTrialColumn] = global::System.Convert.DBNull;
      }



      public bool IsStartTimeNull()
      {
        return this.IsNull(this.tableGazeFixations.StartTimeColumn);
      }



      public void SetStartTimeNull()
      {
        this[this.tableGazeFixations.StartTimeColumn] = global::System.Convert.DBNull;
      }



      public bool IsLengthNull()
      {
        return this.IsNull(this.tableGazeFixations.LengthColumn);
      }



      public void SetLengthNull()
      {
        this[this.tableGazeFixations.LengthColumn] = global::System.Convert.DBNull;
      }



      public bool IsPosXNull()
      {
        return this.IsNull(this.tableGazeFixations.PosXColumn);
      }



      public void SetPosXNull()
      {
        this[this.tableGazeFixations.PosXColumn] = global::System.Convert.DBNull;
      }



      public bool IsPosYNull()
      {
        return this.IsNull(this.tableGazeFixations.PosYColumn);
      }



      public void SetPosYNull()
      {
        this[this.tableGazeFixations.PosYColumn] = global::System.Convert.DBNull;
      }
    }

    /// <summary>
    ///Represents strongly named DataRow class.
    ///</summary>
    public class MouseFixationsRow : DataRow
    {

      private MouseFixationsDataTable tableMouseFixations;



      internal MouseFixationsRow(DataRowBuilder rb) :
        base(rb)
      {
        this.tableMouseFixations = ((MouseFixationsDataTable)(this.Table));
      }



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



      public bool IsCountInTrialNull()
      {
        return this.IsNull(this.tableMouseFixations.CountInTrialColumn);
      }



      public void SetCountInTrialNull()
      {
        this[this.tableMouseFixations.CountInTrialColumn] = global::System.Convert.DBNull;
      }



      public bool IsStartTimeNull()
      {
        return this.IsNull(this.tableMouseFixations.StartTimeColumn);
      }



      public void SetStartTimeNull()
      {
        this[this.tableMouseFixations.StartTimeColumn] = global::System.Convert.DBNull;
      }



      public bool IsLengthNull()
      {
        return this.IsNull(this.tableMouseFixations.LengthColumn);
      }



      public void SetLengthNull()
      {
        this[this.tableMouseFixations.LengthColumn] = global::System.Convert.DBNull;
      }



      public bool IsPosXNull()
      {
        return this.IsNull(this.tableMouseFixations.PosXColumn);
      }



      public void SetPosXNull()
      {
        this[this.tableMouseFixations.PosXColumn] = global::System.Convert.DBNull;
      }



      public bool IsPosYNull()
      {
        return this.IsNull(this.tableMouseFixations.PosYColumn);
      }



      public void SetPosYNull()
      {
        this[this.tableMouseFixations.PosYColumn] = global::System.Convert.DBNull;
      }
    }

    /// <summary>
    ///Represents strongly named DataRow class.
    ///</summary>
    public class ParamsRow : DataRow
    {

      private ParamsDataTable tableParams;



      internal ParamsRow(DataRowBuilder rb) :
        base(rb)
      {
        this.tableParams = ((ParamsDataTable)(this.Table));
      }



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



      public bool IsDescriptionNull()
      {
        return this.IsNull(this.tableParams.DescriptionColumn);
      }



      public void SetDescriptionNull()
      {
        this[this.tableParams.DescriptionColumn] = global::System.Convert.DBNull;
      }
    }

    /// <summary>
    ///Row event argument class
    ///</summary>

    public class SubjectParametersRowChangeEvent : global::System.EventArgs
    {

      private SubjectParametersRow eventRow;

      private DataRowAction eventAction;



      public SubjectParametersRowChangeEvent(SubjectParametersRow row, DataRowAction action)
      {
        this.eventRow = row;
        this.eventAction = action;
      }



      public SubjectParametersRow Row
      {
        get
        {
          return this.eventRow;
        }
      }



      public DataRowAction Action
      {
        get
        {
          return this.eventAction;
        }
      }
    }

    /// <summary>
    ///Row event argument class
    ///</summary>

    public class SubjectsRowChangeEvent : global::System.EventArgs
    {

      private SubjectsRow eventRow;

      private DataRowAction eventAction;



      public SubjectsRowChangeEvent(SubjectsRow row, DataRowAction action)
      {
        this.eventRow = row;
        this.eventAction = action;
      }



      public SubjectsRow Row
      {
        get
        {
          return this.eventRow;
        }
      }



      public DataRowAction Action
      {
        get
        {
          return this.eventAction;
        }
      }
    }

    /// <summary>
    ///Row event argument class
    ///</summary>

    public class TrialsRowChangeEvent : global::System.EventArgs
    {

      private TrialsRow eventRow;

      private DataRowAction eventAction;



      public TrialsRowChangeEvent(TrialsRow row, DataRowAction action)
      {
        this.eventRow = row;
        this.eventAction = action;
      }



      public TrialsRow Row
      {
        get
        {
          return this.eventRow;
        }
      }



      public DataRowAction Action
      {
        get
        {
          return this.eventAction;
        }
      }
    }

    /// <summary>
    ///Row event argument class
    ///</summary>

    public class TrialEventsRowChangeEvent : global::System.EventArgs
    {

      private TrialEventsRow eventRow;

      private DataRowAction eventAction;



      public TrialEventsRowChangeEvent(TrialEventsRow row, DataRowAction action)
      {
        this.eventRow = row;
        this.eventAction = action;
      }



      public TrialEventsRow Row
      {
        get
        {
          return this.eventRow;
        }
      }



      public DataRowAction Action
      {
        get
        {
          return this.eventAction;
        }
      }
    }

    /// <summary>
    ///Row event argument class
    ///</summary>

    public class RawdataRowChangeEvent : global::System.EventArgs
    {

      private RawdataRow eventRow;

      private DataRowAction eventAction;



      public RawdataRowChangeEvent(RawdataRow row, DataRowAction action)
      {
        this.eventRow = row;
        this.eventAction = action;
      }



      public RawdataRow Row
      {
        get
        {
          return this.eventRow;
        }
      }



      public DataRowAction Action
      {
        get
        {
          return this.eventAction;
        }
      }
    }

    /// <summary>
    ///Row event argument class
    ///</summary>

    public class AOIsRowChangeEvent : global::System.EventArgs
    {

      private AOIsRow eventRow;

      private DataRowAction eventAction;



      public AOIsRowChangeEvent(AOIsRow row, DataRowAction action)
      {
        this.eventRow = row;
        this.eventAction = action;
      }



      public AOIsRow Row
      {
        get
        {
          return this.eventRow;
        }
      }



      public DataRowAction Action
      {
        get
        {
          return this.eventAction;
        }
      }
    }

    /// <summary>
    ///Row event argument class
    ///</summary>

    public class ShapeGroupsRowChangeEvent : global::System.EventArgs
    {

      private ShapeGroupsRow eventRow;

      private DataRowAction eventAction;



      public ShapeGroupsRowChangeEvent(ShapeGroupsRow row, DataRowAction action)
      {
        this.eventRow = row;
        this.eventAction = action;
      }



      public ShapeGroupsRow Row
      {
        get
        {
          return this.eventRow;
        }
      }

      public DataRowAction Action
      {
        get
        {
          return this.eventAction;
        }
      }
    }

    /// <summary>
    ///Row event argument class
    ///</summary>
    public class GazeFixationsRowChangeEvent : global::System.EventArgs
    {

      private GazeFixationsRow eventRow;

      private DataRowAction eventAction;



      public GazeFixationsRowChangeEvent(GazeFixationsRow row, DataRowAction action)
      {
        this.eventRow = row;
        this.eventAction = action;
      }



      public GazeFixationsRow Row
      {
        get
        {
          return this.eventRow;
        }
      }



      public DataRowAction Action
      {
        get
        {
          return this.eventAction;
        }
      }
    }

    /// <summary>
    ///Row event argument class
    ///</summary>

    public class MouseFixationsRowChangeEvent : global::System.EventArgs
    {

      private MouseFixationsRow eventRow;

      private DataRowAction eventAction;



      public MouseFixationsRowChangeEvent(MouseFixationsRow row, DataRowAction action)
      {
        this.eventRow = row;
        this.eventAction = action;
      }



      public MouseFixationsRow Row
      {
        get
        {
          return this.eventRow;
        }
      }



      public DataRowAction Action
      {
        get
        {
          return this.eventAction;
        }
      }
    }

    /// <summary>
    ///Row event argument class
    ///</summary>

    public class ParamsRowChangeEvent : global::System.EventArgs
    {

      private ParamsRow eventRow;

      private DataRowAction eventAction;



      public ParamsRowChangeEvent(ParamsRow row, DataRowAction action)
      {
        this.eventRow = row;
        this.eventAction = action;
      }



      public ParamsRow Row
      {
        get
        {
          return this.eventRow;
        }
      }



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
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Dictionary<string, SQLiteDataAdapter> RawDataAdapterDict
    {
      get { return this.rawDataAdapterDict; }
    }

    /// <summary>
    /// Gets the <see cref="SqlConnection"/> for the SQLExpress Database.
    /// </summary>
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
    /// <param name="splash">A <see cref="BackgroundWorker"/> for the progress splash
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
    ///  Create raw data table adapter and add it to AdapterDictionary
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
    /// <returns><see cref="SqlDataAdapter"/> that can be used for queries</returns>
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
    /// <param name="splash">A <see cref="BackgroundWorker"/> for the progress splash
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
    /// <param name="tableName">A <see cref="String"/> with the name of the table with
    /// at least has a filled column 'StimulusFile' which is filled and an empty
    /// column 'TrialID' which should be filled.</param>
    /// <param name="stimulusFiles">A <see cref="List{String}"/> with the stimulus files.</param>
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
    /// <param name="splash">A <see cref="BackgroundWorker"/> for the progress splash
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
