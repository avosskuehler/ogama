// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SQLiteOgamaDataSetTableAdapters.cs" company="Freie Universität Berlin">
//   OGAMA - open gaze and mouse analyzer 
//   Copyright (C) 2014 Dr. Adrian Voßkühler  
//   Licensed under GPL V3
// </copyright>
// <author>Adrian Voßkühler</author>
// <email>adrian@ogama.net</email>
// <summary>
//   Represents the connection and commands used to retrieve and save data.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Ogama.DataSet.SQLiteOgamaDataSetTableAdapters
{
  using System;
  using System.ComponentModel;
  using System.Data;
  using System.Data.Common;
  using System.Data.SQLite;

  /// <summary>
  ///   Represents the connection and commands used to retrieve and save data.
  /// </summary>
  public class SQLiteTadSubjectParameters : Component
  {
    #region Fields

    /// <summary>
    ///   The _adapter
    /// </summary>
    private SQLiteDataAdapter _adapter;

    /// <summary>
    ///   The _command collection
    /// </summary>
    private SQLiteCommand[] _commandCollection;

    /// <summary>
    ///   The _connection
    /// </summary>
    private SQLiteConnection _connection;

    #endregion

    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="SQLiteTadSubjectParameters" /> class.
    /// </summary>
    public SQLiteTadSubjectParameters()
    {
      this.ClearBeforeFill = true;
    }

    #endregion

    #region Public Properties

    /// <summary>
    ///   Gets or sets a value indicating whether [clear before fill].
    /// </summary>
    /// <value><c>true</c> if [clear before fill]; otherwise, <c>false</c>.</value>
    public bool ClearBeforeFill { get; set; }

    #endregion

    #region Properties

    /// <summary>
    ///   Gets or sets the connection.
    /// </summary>
    /// <value>The connection.</value>
    internal SQLiteConnection Connection
    {
      get
      {
        if (this._connection == null)
        {
          this.InitConnection();
        }

        return this._connection;
      }

      set
      {
        this._connection = value;
        if (this.Adapter.InsertCommand != null)
        {
          this.Adapter.InsertCommand.Connection = value;
        }

        if (this.Adapter.DeleteCommand != null)
        {
          this.Adapter.DeleteCommand.Connection = value;
        }

        if (this.Adapter.UpdateCommand != null)
        {
          this.Adapter.UpdateCommand.Connection = value;
        }

        for (int i = 0; i < this.CommandCollection.Length; i = (i + 1))
        {
          if ((this.CommandCollection[i] != null))
          {
            this.CommandCollection[i].Connection = value;
          }
        }
      }
    }

    /// <summary>
    ///   Gets the command collection.
    /// </summary>
    /// <value>The command collection.</value>
    protected SQLiteCommand[] CommandCollection
    {
      get
      {
        if (this._commandCollection == null)
        {
          this.InitCommandCollection();
        }

        return this._commandCollection;
      }
    }

    /// <summary>
    ///   Gets the adapter.
    /// </summary>
    /// <value>The adapter.</value>
    private SQLiteDataAdapter Adapter
    {
      get
      {
        if (this._adapter == null)
        {
          this.InitAdapter();
        }

        return this._adapter;
      }
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// Deletes the specified original_ identifier.
    /// </summary>
    /// <param name="Original_ID">
    /// The original_ identifier.
    /// </param>
    /// <param name="Original_SubjectName">
    /// Name of the original_ subject.
    /// </param>
    /// <param name="Original_Param">
    /// The original_ parameter.
    /// </param>
    /// <param name="Original_ParamValue">
    /// The original_ parameter value.
    /// </param>
    /// <returns>
    /// System.Int32.
    /// </returns>
    /// <exception cref="System.ArgumentNullException">
    /// Original_SubjectName
    ///   or
    ///   Original_Param
    /// </exception>
    [DataObjectMethodAttribute(DataObjectMethodType.Delete,
     true)]
    public virtual int Delete(
      int Original_ID,
      string Original_SubjectName,
      string Original_Param,
      string Original_ParamValue)
    {
      this.Adapter.DeleteCommand.Parameters[0].Value = Original_ID;
      if (Original_SubjectName == null)
      {
        throw new ArgumentNullException("Original_SubjectName");
      }

      this.Adapter.DeleteCommand.Parameters[1].Value = Original_SubjectName;
      if (Original_Param == null)
      {
        throw new ArgumentNullException("Original_Param");
      }

      this.Adapter.DeleteCommand.Parameters[2].Value = Original_Param;
      if (Original_ParamValue == null)
      {
        this.Adapter.DeleteCommand.Parameters[3].Value = 1;
        this.Adapter.DeleteCommand.Parameters[4].Value = DBNull.Value;
      }
      else
      {
        this.Adapter.DeleteCommand.Parameters[3].Value = 0;
        this.Adapter.DeleteCommand.Parameters[4].Value = Original_ParamValue;
      }

      ConnectionState previousConnectionState = this.Adapter.DeleteCommand.Connection.State;
      if ((this.Adapter.DeleteCommand.Connection.State & ConnectionState.Open)
           != ConnectionState.Open)
      {
        this.Adapter.DeleteCommand.Connection.Open();
      }

      try
      {
        int returnValue = this.Adapter.DeleteCommand.ExecuteNonQuery();
        return returnValue;
      }
      finally
      {
        if (previousConnectionState == ConnectionState.Closed)
        {
          this.Adapter.DeleteCommand.Connection.Close();
        }
      }
    }

    /// <summary>
    /// Deletes the name of the by subject.
    /// </summary>
    /// <param name="Original_SubjectName">
    /// Name of the original_ subject.
    /// </param>
    /// <returns>
    /// System.Int32.
    /// </returns>
    /// <exception cref="System.ArgumentNullException">
    /// Original_SubjectName
    /// </exception>
    [DataObjectMethodAttribute(DataObjectMethodType.Delete, false)]
    public virtual int DeleteBySubjectName(string Original_SubjectName)
    {
      SQLiteCommand command = this.CommandCollection[1];
      if (Original_SubjectName == null)
      {
        throw new ArgumentNullException("Original_SubjectName");
      }

      command.Parameters[0].Value = Original_SubjectName;
      ConnectionState previousConnectionState = command.Connection.State;
      if ((command.Connection.State & ConnectionState.Open)
           != ConnectionState.Open)
      {
        command.Connection.Open();
      }

      int returnValue;
      try
      {
        returnValue = command.ExecuteNonQuery();
      }
      finally
      {
        if (previousConnectionState == ConnectionState.Closed)
        {
          command.Connection.Close();
        }
      }

      return returnValue;
    }

    /// <summary>
    /// Fills the specified data table.
    /// </summary>
    /// <param name="dataTable">
    /// The data table.
    /// </param>
    /// <returns>
    /// System.Int32.
    /// </returns>
    [DataObjectMethodAttribute(DataObjectMethodType.Fill, true)]
    public virtual int Fill(SQLiteOgamaDataSet.SubjectParametersDataTable dataTable)
    {
      this.Adapter.SelectCommand = this.CommandCollection[0];
      if (this.ClearBeforeFill)
      {
        dataTable.Clear();
      }

      int returnValue = this.Adapter.Fill(dataTable);
      return returnValue;
    }

    /// <summary>
    ///   Gets the data.
    /// </summary>
    /// <returns>SQLiteOgamaDataSet.SubjectParametersDataTable.</returns>
    [DataObjectMethodAttribute(DataObjectMethodType.Select,
     true)]
    public virtual SQLiteOgamaDataSet.SubjectParametersDataTable GetData()
    {
      this.Adapter.SelectCommand = this.CommandCollection[0];
      var dataTable = new SQLiteOgamaDataSet.SubjectParametersDataTable();
      this.Adapter.Fill(dataTable);
      return dataTable;
    }

    /// <summary>
    /// Gets the data by subject.
    /// </summary>
    /// <param name="Param1">
    /// The param1.
    /// </param>
    /// <returns>
    /// SQLiteOgamaDataSet.SubjectParametersDataTable.
    /// </returns>
    /// <exception cref="System.ArgumentNullException">
    /// Param1
    /// </exception>
    [DataObjectMethodAttribute(DataObjectMethodType.Select,
     false)]
    public virtual SQLiteOgamaDataSet.SubjectParametersDataTable GetDataBySubject(string Param1)
    {
      this.Adapter.SelectCommand = this.CommandCollection[2];
      if (Param1 == null)
      {
        throw new ArgumentNullException("Param1");
      }

      this.Adapter.SelectCommand.Parameters[0].Value = Param1;
      var dataTable = new SQLiteOgamaDataSet.SubjectParametersDataTable();
      this.Adapter.Fill(dataTable);
      return dataTable;
    }

    /// <summary>
    /// Inserts the specified subject name.
    /// </summary>
    /// <param name="SubjectName">
    /// Name of the subject.
    /// </param>
    /// <param name="Param">
    /// The parameter.
    /// </param>
    /// <param name="ParamValue">
    /// The parameter value.
    /// </param>
    /// <returns>
    /// System.Int32.
    /// </returns>
    /// <exception cref="System.ArgumentNullException">
    /// SubjectName
    ///   or
    ///   Param
    /// </exception>
    [DataObjectMethodAttribute(DataObjectMethodType.Insert,
     true)]
    public virtual int Insert(string SubjectName, string Param, string ParamValue)
    {
      if (SubjectName == null)
      {
        throw new ArgumentNullException("SubjectName");
      }

      this.Adapter.InsertCommand.Parameters[0].Value = SubjectName;
      if (Param == null)
      {
        throw new ArgumentNullException("Param");
      }

      this.Adapter.InsertCommand.Parameters[1].Value = Param;
      if (ParamValue == null)
      {
        this.Adapter.InsertCommand.Parameters[2].Value = DBNull.Value;
      }
      else
      {
        this.Adapter.InsertCommand.Parameters[2].Value = ParamValue;
      }

      ConnectionState previousConnectionState = this.Adapter.InsertCommand.Connection.State;
      if ((this.Adapter.InsertCommand.Connection.State & ConnectionState.Open)
           != ConnectionState.Open)
      {
        this.Adapter.InsertCommand.Connection.Open();
      }

      try
      {
        int returnValue = this.Adapter.InsertCommand.ExecuteNonQuery();
        return returnValue;
      }
      finally
      {
        if (previousConnectionState == ConnectionState.Closed)
        {
          this.Adapter.InsertCommand.Connection.Close();
        }
      }
    }

    /// <summary>
    /// Updates the specified data table.
    /// </summary>
    /// <param name="dataTable">
    /// The data table.
    /// </param>
    /// <returns>
    /// System.Int32.
    /// </returns>
    public virtual int Update(SQLiteOgamaDataSet.SubjectParametersDataTable dataTable)
    {
      return this.Adapter.Update(dataTable);
    }

    /// <summary>
    /// Updates the specified data set.
    /// </summary>
    /// <param name="dataSet">
    /// The data set.
    /// </param>
    /// <returns>
    /// System.Int32.
    /// </returns>
    public virtual int Update(SQLiteOgamaDataSet dataSet)
    {
      return this.Adapter.Update(dataSet, "SubjectParameters");
    }

    /// <summary>
    /// Updates the specified data row.
    /// </summary>
    /// <param name="dataRow">
    /// The data row.
    /// </param>
    /// <returns>
    /// System.Int32.
    /// </returns>
    public virtual int Update(DataRow dataRow)
    {
      return this.Adapter.Update(new[] { dataRow });
    }

    /// <summary>
    /// Updates the specified data rows.
    /// </summary>
    /// <param name="dataRows">
    /// The data rows.
    /// </param>
    /// <returns>
    /// System.Int32.
    /// </returns>
    public virtual int Update(DataRow[] dataRows)
    {
      return this.Adapter.Update(dataRows);
    }

    /// <summary>
    /// Updates the specified subject name.
    /// </summary>
    /// <param name="SubjectName">
    /// Name of the subject.
    /// </param>
    /// <param name="Param">
    /// The parameter.
    /// </param>
    /// <param name="ParamValue">
    /// The parameter value.
    /// </param>
    /// <param name="Original_ID">
    /// The original_ identifier.
    /// </param>
    /// <param name="Original_SubjectName">
    /// Name of the original_ subject.
    /// </param>
    /// <param name="Original_Param">
    /// The original_ parameter.
    /// </param>
    /// <param name="Original_ParamValue">
    /// The original_ parameter value.
    /// </param>
    /// <param name="ID">
    /// The identifier.
    /// </param>
    /// <returns>
    /// System.Int32.
    /// </returns>
    /// <exception cref="System.ArgumentNullException">
    /// SubjectName
    ///   or
    ///   Param
    ///   or
    ///   Original_SubjectName
    ///   or
    ///   Original_Param
    /// </exception>
    [DataObjectMethodAttribute(DataObjectMethodType.Update,
     true)]
    public virtual int Update(
      string SubjectName,
      string Param,
      string ParamValue,
      int Original_ID,
      string Original_SubjectName,
      string Original_Param,
      string Original_ParamValue,
      int ID)
    {
      if (SubjectName == null)
      {
        throw new ArgumentNullException("SubjectName");
      }

      this.Adapter.UpdateCommand.Parameters[0].Value = SubjectName;
      if (Param == null)
      {
        throw new ArgumentNullException("Param");
      }

      this.Adapter.UpdateCommand.Parameters[1].Value = Param;
      if (ParamValue == null)
      {
        this.Adapter.UpdateCommand.Parameters[2].Value = DBNull.Value;
      }
      else
      {
        this.Adapter.UpdateCommand.Parameters[2].Value = ParamValue;
      }

      this.Adapter.UpdateCommand.Parameters[3].Value = Original_ID;
      if (Original_SubjectName == null)
      {
        throw new ArgumentNullException("Original_SubjectName");
      }

      this.Adapter.UpdateCommand.Parameters[4].Value = Original_SubjectName;
      if (Original_Param == null)
      {
        throw new ArgumentNullException("Original_Param");
      }

      this.Adapter.UpdateCommand.Parameters[5].Value = Original_Param;
      if (Original_ParamValue == null)
      {
        this.Adapter.UpdateCommand.Parameters[6].Value = 1;
        this.Adapter.UpdateCommand.Parameters[7].Value = DBNull.Value;
      }
      else
      {
        this.Adapter.UpdateCommand.Parameters[6].Value = 0;
        this.Adapter.UpdateCommand.Parameters[7].Value = Original_ParamValue;
      }

      this.Adapter.UpdateCommand.Parameters[8].Value = ID;
      ConnectionState previousConnectionState = this.Adapter.UpdateCommand.Connection.State;
      if ((this.Adapter.UpdateCommand.Connection.State & ConnectionState.Open)
           != ConnectionState.Open)
      {
        this.Adapter.UpdateCommand.Connection.Open();
      }

      try
      {
        int returnValue = this.Adapter.UpdateCommand.ExecuteNonQuery();
        return returnValue;
      }
      finally
      {
        if (previousConnectionState == ConnectionState.Closed)
        {
          this.Adapter.UpdateCommand.Connection.Close();
        }
      }
    }

    /// <summary>
    /// Updates the specified subject name.
    /// </summary>
    /// <param name="SubjectName">
    /// Name of the subject.
    /// </param>
    /// <param name="Param">
    /// The parameter.
    /// </param>
    /// <param name="ParamValue">
    /// The parameter value.
    /// </param>
    /// <param name="Original_ID">
    /// The original_ identifier.
    /// </param>
    /// <param name="Original_SubjectName">
    /// Name of the original_ subject.
    /// </param>
    /// <param name="Original_Param">
    /// The original_ parameter.
    /// </param>
    /// <param name="Original_ParamValue">
    /// The original_ parameter value.
    /// </param>
    /// <returns>
    /// System.Int32.
    /// </returns>
    [DataObjectMethodAttribute(DataObjectMethodType.Update,
      true)]
    public virtual int Update(
      string SubjectName,
      string Param,
      string ParamValue,
      int Original_ID,
      string Original_SubjectName,
      string Original_Param,
      string Original_ParamValue)
    {
      return this.Update(
        SubjectName,
        Param,
        ParamValue,
        Original_ID,
        Original_SubjectName,
        Original_Param,
        Original_ParamValue,
        Original_ID);
    }

    #endregion

    #region Methods

    /// <summary>
    ///   Initializes the adapter.
    /// </summary>
    private void InitAdapter()
    {
      this._adapter = new SQLiteDataAdapter();
      var tableMapping = new DataTableMapping();
      tableMapping.SourceTable = "Table";
      tableMapping.DataSetTable = "SubjectParameters";
      tableMapping.ColumnMappings.Add("ID", "ID");
      tableMapping.ColumnMappings.Add("SubjectName", "SubjectName");
      tableMapping.ColumnMappings.Add("Param", "Param");
      tableMapping.ColumnMappings.Add("ParamValue", "ParamValue");
      this._adapter.TableMappings.Add(tableMapping);
      this._adapter.DeleteCommand = new SQLiteCommand();
      this._adapter.DeleteCommand.Connection = this.Connection;
      this._adapter.DeleteCommand.CommandText =
        "DELETE FROM [SubjectParameters] WHERE (([ID] = @Original_ID) AND ([SubjectName] ="
        + " @Original_SubjectName) AND ([Param] = @Original_Param) AND ((@IsNull_ParamValue"
        + " = 1 AND [ParamValue] IS NULL) OR ([ParamValue] = @Original_ParamValue)))";
      this._adapter.DeleteCommand.CommandType = CommandType.Text;
      this._adapter.DeleteCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_ID",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "ID",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.DeleteCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_SubjectName",
          DbType.String,
          0,
          ParameterDirection.Input,
          0,
          0,
          "SubjectName",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.DeleteCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_Param",
          DbType.String,
          0,
          ParameterDirection.Input,
          0,
          0,
          "Param",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.DeleteCommand.Parameters.Add(
        new SQLiteParameter(
          "@IsNull_ParamValue",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "ParamValue",
          DataRowVersion.Original,
          true,
          null));
      this._adapter.DeleteCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_ParamValue",
          DbType.String,
          0,
          ParameterDirection.Input,
          0,
          0,
          "ParamValue",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.InsertCommand = new SQLiteCommand();
      this._adapter.InsertCommand.Connection = this.Connection;
      this._adapter.InsertCommand.CommandText =
        "INSERT INTO [SubjectParameters] ([SubjectName], [Param], [ParamValue]) VALUES (@S"
        + "ubjectName, @Param, @ParamValue);\r\nSELECT ID, SubjectName, Param, ParamValue FRO"
        + "M SubjectParameters WHERE (ID = last_insert_rowid())";
      this._adapter.InsertCommand.CommandType = CommandType.Text;
      this._adapter.InsertCommand.Parameters.Add(
        new SQLiteParameter(
          "@SubjectName",
          DbType.String,
          0,
          ParameterDirection.Input,
          0,
          0,
          "SubjectName",
          DataRowVersion.Current,
          false,
          null));
      this._adapter.InsertCommand.Parameters.Add(
        new SQLiteParameter(
          "@Param",
          DbType.String,
          0,
          ParameterDirection.Input,
          0,
          0,
          "Param",
          DataRowVersion.Current,
          false,
          null));
      this._adapter.InsertCommand.Parameters.Add(
        new SQLiteParameter(
          "@ParamValue",
          DbType.String,
          0,
          ParameterDirection.Input,
          0,
          0,
          "ParamValue",
          DataRowVersion.Current,
          false,
          null));
      this._adapter.UpdateCommand = new SQLiteCommand();
      this._adapter.UpdateCommand.Connection = this.Connection;
      this._adapter.UpdateCommand.CommandText =
        @"UPDATE [SubjectParameters] SET [SubjectName] = @SubjectName, [Param] = @Param, [ParamValue] = @ParamValue WHERE (([ID] = @Original_ID) AND ([SubjectName] = @Original_SubjectName) AND ([Param] = @Original_Param) AND ((@IsNull_ParamValue = 1 AND [ParamValue] IS NULL) OR ([ParamValue] = @Original_ParamValue)));
SELECT ID, SubjectName, Param, ParamValue FROM SubjectParameters WHERE (ID = @ID)";
      this._adapter.UpdateCommand.CommandType = CommandType.Text;
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@SubjectName",
          DbType.String,
          0,
          ParameterDirection.Input,
          0,
          0,
          "SubjectName",
          DataRowVersion.Current,
          false,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@Param",
          DbType.String,
          0,
          ParameterDirection.Input,
          0,
          0,
          "Param",
          DataRowVersion.Current,
          false,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@ParamValue",
          DbType.String,
          0,
          ParameterDirection.Input,
          0,
          0,
          "ParamValue",
          DataRowVersion.Current,
          false,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_ID",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "ID",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_SubjectName",
          DbType.String,
          0,
          ParameterDirection.Input,
          0,
          0,
          "SubjectName",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_Param",
          DbType.String,
          0,
          ParameterDirection.Input,
          0,
          0,
          "Param",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@IsNull_ParamValue",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "ParamValue",
          DataRowVersion.Original,
          true,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_ParamValue",
          DbType.String,
          0,
          ParameterDirection.Input,
          0,
          0,
          "ParamValue",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@ID",
          DbType.Int32,
          4,
          ParameterDirection.Input,
          0,
          0,
          "ID",
          DataRowVersion.Current,
          false,
          null));
    }

    /// <summary>
    ///   Initializes the command collection.
    /// </summary>
    private void InitCommandCollection()
    {
      this._commandCollection = new SQLiteCommand[3];
      this._commandCollection[0] = new SQLiteCommand();
      this._commandCollection[0].Connection = this.Connection;
      this._commandCollection[0].CommandText =
        "SELECT     ID, SubjectName, Param, ParamValue\r\nFROM         SubjectParameters";
      this._commandCollection[0].CommandType = CommandType.Text;
      this._commandCollection[1] = new SQLiteCommand();
      this._commandCollection[1].Connection = this.Connection;
      this._commandCollection[1].CommandText =
        "DELETE FROM [SubjectParameters] WHERE ([SubjectName] = @Original_SubjectName)";
      this._commandCollection[1].CommandType = CommandType.Text;
      this._commandCollection[1].Parameters.Add(
        new SQLiteParameter(
          "@Original_SubjectName",
          DbType.String,
          50,
          ParameterDirection.Input,
          0,
          0,
          "SubjectName",
          DataRowVersion.Original,
          false,
          null));
      this._commandCollection[2] = new SQLiteCommand();
      this._commandCollection[2].Connection = this.Connection;
      this._commandCollection[2].CommandText =
        "SELECT     ID, SubjectName, Param, ParamValue\r\nFROM         SubjectParameters\r\nWH"
        + "ERE     (SubjectName = @Param1)";
      this._commandCollection[2].CommandType = CommandType.Text;
      this._commandCollection[2].Parameters.Add(
        new SQLiteParameter(
          "@Param1",
          DbType.String,
          50,
          ParameterDirection.Input,
          0,
          0,
          "SubjectName",
          DataRowVersion.Current,
          false,
          null));
    }

    /// <summary>
    ///   Initializes the connection.
    /// </summary>
    private void InitConnection()
    {
      this._connection = new SQLiteConnection();
      this._connection.ConnectionString =
        global::Ogama.Properties.Settings.Default.OgamaDatabaseTemplateConnectionString;
    }

    #endregion
  }

  /// <summary>
  ///   Represents the connection and commands used to retrieve and save data.
  /// </summary>
  public class SQLiteTadSubjects : Component
  {
    #region Fields

    /// <summary>
    ///   The _adapter
    /// </summary>
    private SQLiteDataAdapter _adapter;

    /// <summary>
    ///   The _command collection
    /// </summary>
    private SQLiteCommand[] _commandCollection;

    /// <summary>
    ///   The _connection
    /// </summary>
    private SQLiteConnection _connection;

    #endregion

    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="SQLiteTadSubjects" /> class.
    /// </summary>
    public SQLiteTadSubjects()
    {
      this.ClearBeforeFill = true;
    }

    #endregion

    #region Public Properties

    /// <summary>
    ///   Gets or sets a value indicating whether [clear before fill].
    /// </summary>
    /// <value><c>true</c> if [clear before fill]; otherwise, <c>false</c>.</value>
    public bool ClearBeforeFill { get; set; }

    #endregion

    #region Properties

    /// <summary>
    ///   Gets or sets the connection.
    /// </summary>
    /// <value>The connection.</value>
    internal SQLiteConnection Connection
    {
      get
      {
        if (this._connection == null)
        {
          this.InitConnection();
        }

        return this._connection;
      }

      set
      {
        this._connection = value;
        if (this.Adapter.InsertCommand != null)
        {
          this.Adapter.InsertCommand.Connection = value;
        }

        if (this.Adapter.DeleteCommand != null)
        {
          this.Adapter.DeleteCommand.Connection = value;
        }

        if (this.Adapter.UpdateCommand != null)
        {
          this.Adapter.UpdateCommand.Connection = value;
        }

        for (int i = 0; i < this.CommandCollection.Length; i = (i + 1))
        {
          if ((this.CommandCollection[i] != null))
          {
            this.CommandCollection[i].Connection = value;
          }
        }
      }
    }

    /// <summary>
    ///   Gets the command collection.
    /// </summary>
    /// <value>The command collection.</value>
    protected SQLiteCommand[] CommandCollection
    {
      get
      {
        if (this._commandCollection == null)
        {
          this.InitCommandCollection();
        }

        return this._commandCollection;
      }
    }

    /// <summary>
    ///   Gets the adapter.
    /// </summary>
    /// <value>The adapter.</value>
    private SQLiteDataAdapter Adapter
    {
      get
      {
        if (this._adapter == null)
        {
          this.InitAdapter();
        }

        return this._adapter;
      }
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// Deletes the specified original_ identifier.
    /// </summary>
    /// <param name="Original_ID">
    /// The original_ identifier.
    /// </param>
    /// <param name="Original_SubjectName">
    /// Name of the original_ subject.
    /// </param>
    /// <param name="Original_Category">
    /// The original_ category.
    /// </param>
    /// <param name="Original_Age">
    /// The original_ age.
    /// </param>
    /// <param name="Original_Sex">
    /// The original_ sex.
    /// </param>
    /// <param name="Original_Handedness">
    /// The original_ handedness.
    /// </param>
    /// <param name="Original_Comments">
    /// The original_ comments.
    /// </param>
    /// <returns>
    /// System.Int32.
    /// </returns>
    /// <exception cref="System.ArgumentNullException">
    /// Original_SubjectName
    /// </exception>
    [DataObjectMethodAttribute(DataObjectMethodType.Delete,
     true)]
    public virtual int Delete(
      long Original_ID,
      string Original_SubjectName,
      string Original_Category,
      int? Original_Age,
      string Original_Sex,
      string Original_Handedness,
      string Original_Comments)
    {
      this.Adapter.DeleteCommand.Parameters[0].Value = Original_ID;
      if (Original_SubjectName == null)
      {
        throw new ArgumentNullException("Original_SubjectName");
      }

      this.Adapter.DeleteCommand.Parameters[1].Value = Original_SubjectName;
      if (Original_Category == null)
      {
        this.Adapter.DeleteCommand.Parameters[2].Value = 1;
        this.Adapter.DeleteCommand.Parameters[3].Value = DBNull.Value;
      }
      else
      {
        this.Adapter.DeleteCommand.Parameters[2].Value = 0;
        this.Adapter.DeleteCommand.Parameters[3].Value = Original_Category;
      }

      if (Original_Age.HasValue)
      {
        this.Adapter.DeleteCommand.Parameters[4].Value = 0;
        this.Adapter.DeleteCommand.Parameters[5].Value = Original_Age.Value;
      }
      else
      {
        this.Adapter.DeleteCommand.Parameters[4].Value = 1;
        this.Adapter.DeleteCommand.Parameters[5].Value = DBNull.Value;
      }

      if (Original_Sex == null)
      {
        this.Adapter.DeleteCommand.Parameters[6].Value = 1;
        this.Adapter.DeleteCommand.Parameters[7].Value = DBNull.Value;
      }
      else
      {
        this.Adapter.DeleteCommand.Parameters[6].Value = 0;
        this.Adapter.DeleteCommand.Parameters[7].Value = Original_Sex;
      }

      if (Original_Handedness == null)
      {
        this.Adapter.DeleteCommand.Parameters[8].Value = 1;
        this.Adapter.DeleteCommand.Parameters[9].Value = DBNull.Value;
      }
      else
      {
        this.Adapter.DeleteCommand.Parameters[8].Value = 0;
        this.Adapter.DeleteCommand.Parameters[9].Value = Original_Handedness;
      }

      if (Original_Comments == null)
      {
        this.Adapter.DeleteCommand.Parameters[10].Value = 1;
        this.Adapter.DeleteCommand.Parameters[11].Value = DBNull.Value;
      }
      else
      {
        this.Adapter.DeleteCommand.Parameters[10].Value = 0;
        this.Adapter.DeleteCommand.Parameters[11].Value = Original_Comments;
      }

      ConnectionState previousConnectionState = this.Adapter.DeleteCommand.Connection.State;
      if ((this.Adapter.DeleteCommand.Connection.State & ConnectionState.Open)
           != ConnectionState.Open)
      {
        this.Adapter.DeleteCommand.Connection.Open();
      }

      try
      {
        int returnValue = this.Adapter.DeleteCommand.ExecuteNonQuery();
        return returnValue;
      }
      finally
      {
        if (previousConnectionState == ConnectionState.Closed)
        {
          this.Adapter.DeleteCommand.Connection.Close();
        }
      }
    }

    /// <summary>
    /// Fills the specified data table.
    /// </summary>
    /// <param name="dataTable">
    /// The data table.
    /// </param>
    /// <returns>
    /// System.Int32.
    /// </returns>
    [DataObjectMethodAttribute(DataObjectMethodType.Fill,
     true)]
    public virtual int Fill(SQLiteOgamaDataSet.SubjectsDataTable dataTable)
    {
      this.Adapter.SelectCommand = this.CommandCollection[0];
      if (this.ClearBeforeFill)
      {
        dataTable.Clear();
      }

      int returnValue = this.Adapter.Fill(dataTable);
      return returnValue;
    }

    /// <summary>
    ///   Gets the categories.
    /// </summary>
    /// <returns>SQLiteOgamaDataSet.SubjectsDataTable.</returns>
    [DataObjectMethodAttribute(DataObjectMethodType.Select,
     false)]
    public virtual SQLiteOgamaDataSet.SubjectsDataTable GetCategories()
    {
      this.Adapter.SelectCommand = this.CommandCollection[1];
      var dataTable = new SQLiteOgamaDataSet.SubjectsDataTable();
      this.Adapter.Fill(dataTable);
      return dataTable;
    }

    /// <summary>
    ///   Gets the category rows.
    /// </summary>
    /// <returns>SQLiteOgamaDataSet.SubjectsDataTable.</returns>
    [DataObjectMethodAttribute(DataObjectMethodType.Select,
     false)]
    public virtual SQLiteOgamaDataSet.SubjectsDataTable GetCategoryRows()
    {
      this.Adapter.SelectCommand = this.CommandCollection[2];
      var dataTable = new SQLiteOgamaDataSet.SubjectsDataTable();
      this.Adapter.Fill(dataTable);
      return dataTable;
    }

    /// <summary>
    ///   Gets the data.
    /// </summary>
    /// <returns>SQLiteOgamaDataSet.SubjectsDataTable.</returns>
    [DataObjectMethodAttribute(DataObjectMethodType.Select,
     true)]
    public virtual SQLiteOgamaDataSet.SubjectsDataTable GetData()
    {
      this.Adapter.SelectCommand = this.CommandCollection[0];
      var dataTable = new SQLiteOgamaDataSet.SubjectsDataTable();
      this.Adapter.Fill(dataTable);
      return dataTable;
    }

    /// <summary>
    /// Gets the data by subject.
    /// </summary>
    /// <param name="Param1">
    /// The param1.
    /// </param>
    /// <returns>
    /// SQLiteOgamaDataSet.SubjectsDataTable.
    /// </returns>
    /// <exception cref="System.ArgumentNullException">
    /// Param1
    /// </exception>
    [DataObjectMethodAttribute(DataObjectMethodType.Select,
     false)]
    public virtual SQLiteOgamaDataSet.SubjectsDataTable GetDataBySubject(string Param1)
    {
      this.Adapter.SelectCommand = this.CommandCollection[3];
      if (Param1 == null)
      {
        throw new ArgumentNullException("Param1");
      }

      this.Adapter.SelectCommand.Parameters[0].Value = Param1;
      var dataTable = new SQLiteOgamaDataSet.SubjectsDataTable();
      this.Adapter.Fill(dataTable);
      return dataTable;
    }

    /// <summary>
    /// Inserts the specified subject name.
    /// </summary>
    /// <param name="SubjectName">
    /// Name of the subject.
    /// </param>
    /// <param name="Category">
    /// The category.
    /// </param>
    /// <param name="Age">
    /// The age.
    /// </param>
    /// <param name="Sex">
    /// The sex.
    /// </param>
    /// <param name="Handedness">
    /// The handedness.
    /// </param>
    /// <param name="Comments">
    /// The comments.
    /// </param>
    /// <returns>
    /// System.Int32.
    /// </returns>
    /// <exception cref="System.ArgumentNullException">
    /// SubjectName
    /// </exception>
    [DataObjectMethodAttribute(DataObjectMethodType.Insert,
     true)]
    public virtual int Insert(
      string SubjectName,
      string Category,
      int? Age,
      string Sex,
      string Handedness,
      string Comments)
    {
      if (SubjectName == null)
      {
        throw new ArgumentNullException("SubjectName");
      }

      this.Adapter.InsertCommand.Parameters[0].Value = SubjectName;
      if (Category == null)
      {
        this.Adapter.InsertCommand.Parameters[1].Value = DBNull.Value;
      }
      else
      {
        this.Adapter.InsertCommand.Parameters[1].Value = Category;
      }

      if (Age.HasValue)
      {
        this.Adapter.InsertCommand.Parameters[2].Value = Age.Value;
      }
      else
      {
        this.Adapter.InsertCommand.Parameters[2].Value = DBNull.Value;
      }

      if (Sex == null)
      {
        this.Adapter.InsertCommand.Parameters[3].Value = DBNull.Value;
      }
      else
      {
        this.Adapter.InsertCommand.Parameters[3].Value = Sex;
      }

      if (Handedness == null)
      {
        this.Adapter.InsertCommand.Parameters[4].Value = DBNull.Value;
      }
      else
      {
        this.Adapter.InsertCommand.Parameters[4].Value = Handedness;
      }

      if (Comments == null)
      {
        this.Adapter.InsertCommand.Parameters[5].Value = DBNull.Value;
      }
      else
      {
        this.Adapter.InsertCommand.Parameters[5].Value = Comments;
      }

      ConnectionState previousConnectionState = this.Adapter.InsertCommand.Connection.State;
      if ((this.Adapter.InsertCommand.Connection.State & ConnectionState.Open)
           != ConnectionState.Open)
      {
        this.Adapter.InsertCommand.Connection.Open();
      }

      try
      {
        int returnValue = this.Adapter.InsertCommand.ExecuteNonQuery();
        return returnValue;
      }
      finally
      {
        if (previousConnectionState == ConnectionState.Closed)
        {
          this.Adapter.InsertCommand.Connection.Close();
        }
      }
    }

    /// <summary>
    /// Updates the specified data table.
    /// </summary>
    /// <param name="dataTable">
    /// The data table.
    /// </param>
    /// <returns>
    /// System.Int32.
    /// </returns>
    public virtual int Update(SQLiteOgamaDataSet.SubjectsDataTable dataTable)
    {
      return this.Adapter.Update(dataTable);
    }

    /// <summary>
    /// Updates the specified data set.
    /// </summary>
    /// <param name="dataSet">
    /// The data set.
    /// </param>
    /// <returns>
    /// System.Int32.
    /// </returns>
    public virtual int Update(SQLiteOgamaDataSet dataSet)
    {
      return this.Adapter.Update(dataSet, "Subjects");
    }

    /// <summary>
    /// Updates the specified data row.
    /// </summary>
    /// <param name="dataRow">
    /// The data row.
    /// </param>
    /// <returns>
    /// System.Int32.
    /// </returns>
    public virtual int Update(DataRow dataRow)
    {
      return this.Adapter.Update(new[] { dataRow });
    }

    /// <summary>
    /// Updates the specified data rows.
    /// </summary>
    /// <param name="dataRows">
    /// The data rows.
    /// </param>
    /// <returns>
    /// System.Int32.
    /// </returns>
    public virtual int Update(DataRow[] dataRows)
    {
      return this.Adapter.Update(dataRows);
    }

    /// <summary>
    /// Updates the specified subject name.
    /// </summary>
    /// <param name="SubjectName">
    /// Name of the subject.
    /// </param>
    /// <param name="Category">
    /// The category.
    /// </param>
    /// <param name="Age">
    /// The age.
    /// </param>
    /// <param name="Sex">
    /// The sex.
    /// </param>
    /// <param name="Handedness">
    /// The handedness.
    /// </param>
    /// <param name="Comments">
    /// The comments.
    /// </param>
    /// <param name="Original_ID">
    /// The original_ identifier.
    /// </param>
    /// <param name="Original_SubjectName">
    /// Name of the original_ subject.
    /// </param>
    /// <param name="Original_Category">
    /// The original_ category.
    /// </param>
    /// <param name="Original_Age">
    /// The original_ age.
    /// </param>
    /// <param name="Original_Sex">
    /// The original_ sex.
    /// </param>
    /// <param name="Original_Handedness">
    /// The original_ handedness.
    /// </param>
    /// <param name="Original_Comments">
    /// The original_ comments.
    /// </param>
    /// <param name="ID">
    /// The identifier.
    /// </param>
    /// <returns>
    /// System.Int32.
    /// </returns>
    /// <exception cref="System.ArgumentNullException">
    /// SubjectName
    ///   or
    ///   Original_SubjectName
    /// </exception>
    [DataObjectMethodAttribute(DataObjectMethodType.Update,
     true)]
    public virtual int Update(
      string SubjectName,
      string Category,
      int? Age,
      string Sex,
      string Handedness,
      string Comments,
      long Original_ID,
      string Original_SubjectName,
      string Original_Category,
      int? Original_Age,
      string Original_Sex,
      string Original_Handedness,
      string Original_Comments,
      long ID)
    {
      if (SubjectName == null)
      {
        throw new ArgumentNullException("SubjectName");
      }

      this.Adapter.UpdateCommand.Parameters[0].Value = SubjectName;
      if (Category == null)
      {
        this.Adapter.UpdateCommand.Parameters[1].Value = DBNull.Value;
      }
      else
      {
        this.Adapter.UpdateCommand.Parameters[1].Value = Category;
      }

      if (Age.HasValue)
      {
        this.Adapter.UpdateCommand.Parameters[2].Value = Age.Value;
      }
      else
      {
        this.Adapter.UpdateCommand.Parameters[2].Value = DBNull.Value;
      }

      if (Sex == null)
      {
        this.Adapter.UpdateCommand.Parameters[3].Value = DBNull.Value;
      }
      else
      {
        this.Adapter.UpdateCommand.Parameters[3].Value = Sex;
      }

      if (Handedness == null)
      {
        this.Adapter.UpdateCommand.Parameters[4].Value = DBNull.Value;
      }
      else
      {
        this.Adapter.UpdateCommand.Parameters[4].Value = Handedness;
      }

      if (Comments == null)
      {
        this.Adapter.UpdateCommand.Parameters[5].Value = DBNull.Value;
      }
      else
      {
        this.Adapter.UpdateCommand.Parameters[5].Value = Comments;
      }

      this.Adapter.UpdateCommand.Parameters[6].Value = Original_ID;
      if (Original_SubjectName == null)
      {
        throw new ArgumentNullException("Original_SubjectName");
      }

      this.Adapter.UpdateCommand.Parameters[7].Value = Original_SubjectName;
      if (Original_Category == null)
      {
        this.Adapter.UpdateCommand.Parameters[8].Value = 1;
        this.Adapter.UpdateCommand.Parameters[9].Value = DBNull.Value;
      }
      else
      {
        this.Adapter.UpdateCommand.Parameters[8].Value = 0;
        this.Adapter.UpdateCommand.Parameters[9].Value = Original_Category;
      }

      if (Original_Age.HasValue)
      {
        this.Adapter.UpdateCommand.Parameters[10].Value = 0;
        this.Adapter.UpdateCommand.Parameters[11].Value = Original_Age.Value;
      }
      else
      {
        this.Adapter.UpdateCommand.Parameters[10].Value = 1;
        this.Adapter.UpdateCommand.Parameters[11].Value = DBNull.Value;
      }

      if (Original_Sex == null)
      {
        this.Adapter.UpdateCommand.Parameters[12].Value = 1;
        this.Adapter.UpdateCommand.Parameters[13].Value = DBNull.Value;
      }
      else
      {
        this.Adapter.UpdateCommand.Parameters[12].Value = 0;
        this.Adapter.UpdateCommand.Parameters[13].Value = Original_Sex;
      }

      if (Original_Handedness == null)
      {
        this.Adapter.UpdateCommand.Parameters[14].Value = 1;
        this.Adapter.UpdateCommand.Parameters[15].Value = DBNull.Value;
      }
      else
      {
        this.Adapter.UpdateCommand.Parameters[14].Value = 0;
        this.Adapter.UpdateCommand.Parameters[15].Value = Original_Handedness;
      }

      if (Original_Comments == null)
      {
        this.Adapter.UpdateCommand.Parameters[16].Value = 1;
        this.Adapter.UpdateCommand.Parameters[17].Value = DBNull.Value;
      }
      else
      {
        this.Adapter.UpdateCommand.Parameters[16].Value = 0;
        this.Adapter.UpdateCommand.Parameters[17].Value = Original_Comments;
      }

      this.Adapter.UpdateCommand.Parameters[18].Value = ID;
      ConnectionState previousConnectionState = this.Adapter.UpdateCommand.Connection.State;
      if ((this.Adapter.UpdateCommand.Connection.State & ConnectionState.Open)
           != ConnectionState.Open)
      {
        this.Adapter.UpdateCommand.Connection.Open();
      }

      try
      {
        int returnValue = this.Adapter.UpdateCommand.ExecuteNonQuery();
        return returnValue;
      }
      finally
      {
        if (previousConnectionState == ConnectionState.Closed)
        {
          this.Adapter.UpdateCommand.Connection.Close();
        }
      }
    }

    /// <summary>
    /// Updates the specified subject name.
    /// </summary>
    /// <param name="SubjectName">
    /// Name of the subject.
    /// </param>
    /// <param name="Category">
    /// The category.
    /// </param>
    /// <param name="Age">
    /// The age.
    /// </param>
    /// <param name="Sex">
    /// The sex.
    /// </param>
    /// <param name="Handedness">
    /// The handedness.
    /// </param>
    /// <param name="Comments">
    /// The comments.
    /// </param>
    /// <param name="Original_ID">
    /// The original_ identifier.
    /// </param>
    /// <param name="Original_SubjectName">
    /// Name of the original_ subject.
    /// </param>
    /// <param name="Original_Category">
    /// The original_ category.
    /// </param>
    /// <param name="Original_Age">
    /// The original_ age.
    /// </param>
    /// <param name="Original_Sex">
    /// The original_ sex.
    /// </param>
    /// <param name="Original_Handedness">
    /// The original_ handedness.
    /// </param>
    /// <param name="Original_Comments">
    /// The original_ comments.
    /// </param>
    /// <returns>
    /// System.Int32.
    /// </returns>
    [DataObjectMethodAttribute(DataObjectMethodType.Update,
     true)]
    public virtual int Update(
      string SubjectName,
      string Category,
      int? Age,
      string Sex,
      string Handedness,
      string Comments,
      long Original_ID,
      string Original_SubjectName,
      string Original_Category,
      int? Original_Age,
      string Original_Sex,
      string Original_Handedness,
      string Original_Comments)
    {
      return this.Update(
        SubjectName,
        Category,
        Age,
        Sex,
        Handedness,
        Comments,
        Original_ID,
        Original_SubjectName,
        Original_Category,
        Original_Age,
        Original_Sex,
        Original_Handedness,
        Original_Comments,
        Original_ID);
    }

    #endregion

    #region Methods

    /// <summary>
    ///   Initializes the adapter.
    /// </summary>
    private void InitAdapter()
    {
      this._adapter = new SQLiteDataAdapter();
      var tableMapping = new DataTableMapping();
      tableMapping.SourceTable = "Table";
      tableMapping.DataSetTable = "Subjects";
      tableMapping.ColumnMappings.Add("ID", "ID");
      tableMapping.ColumnMappings.Add("SubjectName", "SubjectName");
      tableMapping.ColumnMappings.Add("Category", "Category");
      tableMapping.ColumnMappings.Add("Age", "Age");
      tableMapping.ColumnMappings.Add("Sex", "Sex");
      tableMapping.ColumnMappings.Add("Handedness", "Handedness");
      tableMapping.ColumnMappings.Add("Comments", "Comments");
      this._adapter.TableMappings.Add(tableMapping);
      this._adapter.DeleteCommand = new SQLiteCommand();
      this._adapter.DeleteCommand.Connection = this.Connection;
      this._adapter.DeleteCommand.CommandText =
        @"DELETE FROM [Subjects] WHERE (([ID] = @Original_ID) AND ([SubjectName] = @Original_SubjectName) AND ((@IsNull_Category = 1 AND [Category] IS NULL) OR ([Category] = @Original_Category)) AND ((@IsNull_Age = 1 AND [Age] IS NULL) OR ([Age] = @Original_Age)) AND ((@IsNull_Sex = 1 AND [Sex] IS NULL) OR ([Sex] = @Original_Sex)) AND ((@IsNull_Handedness = 1 AND [Handedness] IS NULL) OR ([Handedness] = @Original_Handedness)) AND ((@IsNull_Comments = 1 AND [Comments] IS NULL) OR ([Comments] = @Original_Comments)))";
      this._adapter.DeleteCommand.CommandType = CommandType.Text;
      this._adapter.DeleteCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_ID",
          DbType.Int64,
          0,
          ParameterDirection.Input,
          0,
          0,
          "ID",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.DeleteCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_SubjectName",
          DbType.String,
          0,
          ParameterDirection.Input,
          0,
          0,
          "SubjectName",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.DeleteCommand.Parameters.Add(
        new SQLiteParameter(
          "@IsNull_Category",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "Category",
          DataRowVersion.Original,
          true,
          null));
      this._adapter.DeleteCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_Category",
          DbType.String,
          0,
          ParameterDirection.Input,
          0,
          0,
          "Category",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.DeleteCommand.Parameters.Add(
        new SQLiteParameter(
          "@IsNull_Age",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "Age",
          DataRowVersion.Original,
          true,
          null));
      this._adapter.DeleteCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_Age",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "Age",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.DeleteCommand.Parameters.Add(
        new SQLiteParameter(
          "@IsNull_Sex",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "Sex",
          DataRowVersion.Original,
          true,
          null));
      this._adapter.DeleteCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_Sex",
          DbType.String,
          0,
          ParameterDirection.Input,
          0,
          0,
          "Sex",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.DeleteCommand.Parameters.Add(
        new SQLiteParameter(
          "@IsNull_Handedness",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "Handedness",
          DataRowVersion.Original,
          true,
          null));
      this._adapter.DeleteCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_Handedness",
          DbType.String,
          0,
          ParameterDirection.Input,
          0,
          0,
          "Handedness",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.DeleteCommand.Parameters.Add(
        new SQLiteParameter(
          "@IsNull_Comments",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "Comments",
          DataRowVersion.Original,
          true,
          null));
      this._adapter.DeleteCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_Comments",
          DbType.String,
          0,
          ParameterDirection.Input,
          0,
          0,
          "Comments",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.InsertCommand = new SQLiteCommand();
      this._adapter.InsertCommand.Connection = this.Connection;
      this._adapter.InsertCommand.CommandText =
        @"INSERT INTO [Subjects] ([SubjectName], [Category], [Age], [Sex], [Handedness], [Comments]) VALUES (@SubjectName, @Category, @Age, @Sex, @Handedness, @Comments);
SELECT ID, SubjectName, Category, Age, Sex, Handedness, Comments FROM Subjects WHERE (ID = last_insert_rowid())";
      this._adapter.InsertCommand.CommandType = CommandType.Text;
      this._adapter.InsertCommand.Parameters.Add(
        new SQLiteParameter(
          "@SubjectName",
          DbType.String,
          0,
          ParameterDirection.Input,
          0,
          0,
          "SubjectName",
          DataRowVersion.Current,
          false,
          null));
      this._adapter.InsertCommand.Parameters.Add(
        new SQLiteParameter(
          "@Category",
          DbType.String,
          0,
          ParameterDirection.Input,
          0,
          0,
          "Category",
          DataRowVersion.Current,
          false,
          null));
      this._adapter.InsertCommand.Parameters.Add(
        new SQLiteParameter(
          "@Age",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "Age",
          DataRowVersion.Current,
          false,
          null));
      this._adapter.InsertCommand.Parameters.Add(
        new SQLiteParameter(
          "@Sex",
          DbType.String,
          0,
          ParameterDirection.Input,
          0,
          0,
          "Sex",
          DataRowVersion.Current,
          false,
          null));
      this._adapter.InsertCommand.Parameters.Add(
        new SQLiteParameter(
          "@Handedness",
          DbType.String,
          0,
          ParameterDirection.Input,
          0,
          0,
          "Handedness",
          DataRowVersion.Current,
          false,
          null));
      this._adapter.InsertCommand.Parameters.Add(
        new SQLiteParameter(
          "@Comments",
          DbType.String,
          0,
          ParameterDirection.Input,
          0,
          0,
          "Comments",
          DataRowVersion.Current,
          false,
          null));
      this._adapter.UpdateCommand = new SQLiteCommand();
      this._adapter.UpdateCommand.Connection = this.Connection;
      this._adapter.UpdateCommand.CommandText =
        @"UPDATE [Subjects] SET [SubjectName] = @SubjectName, [Category] = @Category, [Age] = @Age, [Sex] = @Sex, [Handedness] = @Handedness, [Comments] = @Comments WHERE (([ID] = @Original_ID) AND ([SubjectName] = @Original_SubjectName) AND ((@IsNull_Category = 1 AND [Category] IS NULL) OR ([Category] = @Original_Category)) AND ((@IsNull_Age = 1 AND [Age] IS NULL) OR ([Age] = @Original_Age)) AND ((@IsNull_Sex = 1 AND [Sex] IS NULL) OR ([Sex] = @Original_Sex)) AND ((@IsNull_Handedness = 1 AND [Handedness] IS NULL) OR ([Handedness] = @Original_Handedness)) AND ((@IsNull_Comments = 1 AND [Comments] IS NULL) OR ([Comments] = @Original_Comments)));
SELECT ID, SubjectName, Category, Age, Sex, Handedness, Comments FROM Subjects WHERE (ID = @ID)";
      this._adapter.UpdateCommand.CommandType = CommandType.Text;
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@SubjectName",
          DbType.String,
          0,
          ParameterDirection.Input,
          0,
          0,
          "SubjectName",
          DataRowVersion.Current,
          false,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@Category",
          DbType.String,
          0,
          ParameterDirection.Input,
          0,
          0,
          "Category",
          DataRowVersion.Current,
          false,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@Age",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "Age",
          DataRowVersion.Current,
          false,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@Sex",
          DbType.String,
          0,
          ParameterDirection.Input,
          0,
          0,
          "Sex",
          DataRowVersion.Current,
          false,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@Handedness",
          DbType.String,
          0,
          ParameterDirection.Input,
          0,
          0,
          "Handedness",
          DataRowVersion.Current,
          false,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@Comments",
          DbType.String,
          0,
          ParameterDirection.Input,
          0,
          0,
          "Comments",
          DataRowVersion.Current,
          false,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_ID",
          DbType.Int64,
          0,
          ParameterDirection.Input,
          0,
          0,
          "ID",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_SubjectName",
          DbType.String,
          0,
          ParameterDirection.Input,
          0,
          0,
          "SubjectName",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@IsNull_Category",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "Category",
          DataRowVersion.Original,
          true,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_Category",
          DbType.String,
          0,
          ParameterDirection.Input,
          0,
          0,
          "Category",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@IsNull_Age",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "Age",
          DataRowVersion.Original,
          true,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_Age",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "Age",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@IsNull_Sex",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "Sex",
          DataRowVersion.Original,
          true,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_Sex",
          DbType.String,
          0,
          ParameterDirection.Input,
          0,
          0,
          "Sex",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@IsNull_Handedness",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "Handedness",
          DataRowVersion.Original,
          true,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_Handedness",
          DbType.String,
          0,
          ParameterDirection.Input,
          0,
          0,
          "Handedness",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@IsNull_Comments",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "Comments",
          DataRowVersion.Original,
          true,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_Comments",
          DbType.String,
          0,
          ParameterDirection.Input,
          0,
          0,
          "Comments",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@ID",
          DbType.Int64,
          8,
          ParameterDirection.Input,
          0,
          0,
          "ID",
          DataRowVersion.Current,
          false,
          null));
    }

    /// <summary>
    ///   Initializes the command collection.
    /// </summary>
    private void InitCommandCollection()
    {
      this._commandCollection = new SQLiteCommand[4];
      this._commandCollection[0] = new SQLiteCommand();
      this._commandCollection[0].Connection = this.Connection;
      this._commandCollection[0].CommandText =
        "SELECT     ID, SubjectName, Category, Age, Sex, Handedness, Comments\r\nFROM       " + "  Subjects";
      this._commandCollection[0].CommandType = CommandType.Text;
      this._commandCollection[1] = new SQLiteCommand();
      this._commandCollection[1].Connection = this.Connection;
      this._commandCollection[1].CommandText =
        "SELECT     MIN(ID) AS MinID, COUNT(SubjectName) AS SubjectCount, Category\r\nFROM  "
        + "       Subjects\r\nGROUP BY Category";
      this._commandCollection[1].CommandType = CommandType.Text;
      this._commandCollection[2] = new SQLiteCommand();
      this._commandCollection[2].Connection = this.Connection;
      this._commandCollection[2].CommandText =
        "SELECT     COUNT(*) AS [RowCount], Category\r\nFROM         Subjects\r\nGROUP BY Cate" + "gory";
      this._commandCollection[2].CommandType = CommandType.Text;
      this._commandCollection[3] = new SQLiteCommand();
      this._commandCollection[3].Connection = this.Connection;
      this._commandCollection[3].CommandText =
        "SELECT Age, Category, Comments, Handedness, ID, Sex, SubjectName FROM Subjects WH"
        + "ERE (SubjectName = @Param1)";
      this._commandCollection[3].CommandType = CommandType.Text;
      this._commandCollection[3].Parameters.Add(
        new SQLiteParameter(
          "@Param1",
          DbType.String,
          50,
          ParameterDirection.Input,
          0,
          0,
          "SubjectName",
          DataRowVersion.Current,
          false,
          null));
    }

    /// <summary>
    ///   Initializes the connection.
    /// </summary>
    private void InitConnection()
    {
      this._connection = new SQLiteConnection();
      this._connection.ConnectionString =
        global::Ogama.Properties.Settings.Default.OgamaDatabaseTemplateConnectionString;
    }

    #endregion
  }

  /// <summary>
  ///   Represents the connection and commands used to retrieve and save data.
  /// </summary>
  public class SQLiteTadTrials : Component
  {
    #region Fields

    /// <summary>
    ///   The _adapter
    /// </summary>
    private SQLiteDataAdapter _adapter;

    /// <summary>
    ///   The _command collection
    /// </summary>
    private SQLiteCommand[] _commandCollection;

    /// <summary>
    ///   The _connection
    /// </summary>
    private SQLiteConnection _connection;

    #endregion

    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="SQLiteTadTrials" /> class.
    /// </summary>
    public SQLiteTadTrials()
    {
      this.ClearBeforeFill = true;
    }

    #endregion

    #region Public Properties

    /// <summary>
    ///   Gets or sets a value indicating whether [clear before fill].
    /// </summary>
    /// <value><c>true</c> if [clear before fill]; otherwise, <c>false</c>.</value>
    public bool ClearBeforeFill { get; set; }

    #endregion

    #region Properties

    /// <summary>
    ///   Gets or sets the connection.
    /// </summary>
    /// <value>The connection.</value>
    internal SQLiteConnection Connection
    {
      get
      {
        if (this._connection == null)
        {
          this.InitConnection();
        }

        return this._connection;
      }

      set
      {
        this._connection = value;
        if (this.Adapter.InsertCommand != null)
        {
          this.Adapter.InsertCommand.Connection = value;
        }

        if (this.Adapter.DeleteCommand != null)
        {
          this.Adapter.DeleteCommand.Connection = value;
        }

        if (this.Adapter.UpdateCommand != null)
        {
          this.Adapter.UpdateCommand.Connection = value;
        }

        for (int i = 0; i < this.CommandCollection.Length; i = (i + 1))
        {
          if ((this.CommandCollection[i] != null))
          {
            this.CommandCollection[i].Connection = value;
          }
        }
      }
    }

    /// <summary>
    ///   Gets the command collection.
    /// </summary>
    /// <value>The command collection.</value>
    protected SQLiteCommand[] CommandCollection
    {
      get
      {
        if (this._commandCollection == null)
        {
          this.InitCommandCollection();
        }

        return this._commandCollection;
      }
    }

    /// <summary>
    ///   Gets the adapter.
    /// </summary>
    /// <value>The adapter.</value>
    private SQLiteDataAdapter Adapter
    {
      get
      {
        if (this._adapter == null)
        {
          this.InitAdapter();
        }

        return this._adapter;
      }
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// Deletes the specified original_ identifier.
    /// </summary>
    /// <param name="Original_ID">
    /// The original_ identifier.
    /// </param>
    /// <param name="Original_SubjectName">
    /// Name of the original_ subject.
    /// </param>
    /// <param name="Original_TrialID">
    /// The original_ trial identifier.
    /// </param>
    /// <param name="Original_TrialName">
    /// Name of the original_ trial.
    /// </param>
    /// <param name="Original_TrialSequence">
    /// The original_ trial sequence.
    /// </param>
    /// <param name="Original_Category">
    /// The original_ category.
    /// </param>
    /// <param name="Original_TrialStartTime">
    /// The original_ trial start time.
    /// </param>
    /// <param name="Original_Duration">
    /// Duration of the original_.
    /// </param>
    /// <param name="Original_EliminateData">
    /// The original_ eliminate data.
    /// </param>
    /// <returns>
    /// System.Int32.
    /// </returns>
    /// <exception cref="System.ArgumentNullException">
    /// Original_SubjectName
    /// </exception>
    [DataObjectMethodAttribute(DataObjectMethodType.Delete,
     true)]
    public virtual int Delete(
      long Original_ID,
      string Original_SubjectName,
      int Original_TrialID,
      string Original_TrialName,
      int Original_TrialSequence,
      string Original_Category,
      long? Original_TrialStartTime,
      int? Original_Duration,
      string Original_EliminateData)
    {
      this.Adapter.DeleteCommand.Parameters[0].Value = Original_ID;
      if (Original_SubjectName == null)
      {
        throw new ArgumentNullException("Original_SubjectName");
      }

      this.Adapter.DeleteCommand.Parameters[1].Value = Original_SubjectName;
      this.Adapter.DeleteCommand.Parameters[2].Value = Original_TrialID;
      if (Original_TrialName == null)
      {
        this.Adapter.DeleteCommand.Parameters[3].Value = 1;
        this.Adapter.DeleteCommand.Parameters[4].Value = DBNull.Value;
      }
      else
      {
        this.Adapter.DeleteCommand.Parameters[3].Value = 0;
        this.Adapter.DeleteCommand.Parameters[4].Value = Original_TrialName;
      }

      this.Adapter.DeleteCommand.Parameters[5].Value = Original_TrialSequence;
      if (Original_Category == null)
      {
        this.Adapter.DeleteCommand.Parameters[6].Value = 1;
        this.Adapter.DeleteCommand.Parameters[7].Value = DBNull.Value;
      }
      else
      {
        this.Adapter.DeleteCommand.Parameters[6].Value = 0;
        this.Adapter.DeleteCommand.Parameters[7].Value = Original_Category;
      }

      if (Original_TrialStartTime.HasValue)
      {
        this.Adapter.DeleteCommand.Parameters[8].Value = 0;
        this.Adapter.DeleteCommand.Parameters[9].Value = Original_TrialStartTime.Value;
      }
      else
      {
        this.Adapter.DeleteCommand.Parameters[8].Value = 1;
        this.Adapter.DeleteCommand.Parameters[9].Value = DBNull.Value;
      }

      if (Original_Duration.HasValue)
      {
        this.Adapter.DeleteCommand.Parameters[10].Value = 0;
        this.Adapter.DeleteCommand.Parameters[11].Value = Original_Duration.Value;
      }
      else
      {
        this.Adapter.DeleteCommand.Parameters[10].Value = 1;
        this.Adapter.DeleteCommand.Parameters[11].Value = DBNull.Value;
      }

      if (Original_EliminateData == null)
      {
        this.Adapter.DeleteCommand.Parameters[12].Value = 1;
        this.Adapter.DeleteCommand.Parameters[13].Value = DBNull.Value;
      }
      else
      {
        this.Adapter.DeleteCommand.Parameters[12].Value = 0;
        this.Adapter.DeleteCommand.Parameters[13].Value = Original_EliminateData;
      }

      ConnectionState previousConnectionState = this.Adapter.DeleteCommand.Connection.State;
      if ((this.Adapter.DeleteCommand.Connection.State & ConnectionState.Open)
           != ConnectionState.Open)
      {
        this.Adapter.DeleteCommand.Connection.Open();
      }

      try
      {
        int returnValue = this.Adapter.DeleteCommand.ExecuteNonQuery();
        return returnValue;
      }
      finally
      {
        if (previousConnectionState == ConnectionState.Closed)
        {
          this.Adapter.DeleteCommand.Connection.Close();
        }
      }
    }

    /// <summary>
    /// Deletes the name of the by subject.
    /// </summary>
    /// <param name="Original_SubjectName">
    /// Name of the original_ subject.
    /// </param>
    /// <returns>
    /// System.Int32.
    /// </returns>
    /// <exception cref="System.ArgumentNullException">
    /// Original_SubjectName
    /// </exception>
    [DataObjectMethodAttribute(DataObjectMethodType.Delete,
     false)]
    public virtual int DeleteBySubjectName(string Original_SubjectName)
    {
      SQLiteCommand command = this.CommandCollection[1];
      if (Original_SubjectName == null)
      {
        throw new ArgumentNullException("Original_SubjectName");
      }

      command.Parameters[0].Value = Original_SubjectName;
      ConnectionState previousConnectionState = command.Connection.State;
      if ((command.Connection.State & ConnectionState.Open)
           != ConnectionState.Open)
      {
        command.Connection.Open();
      }

      int returnValue;
      try
      {
        returnValue = command.ExecuteNonQuery();
      }
      finally
      {
        if (previousConnectionState == ConnectionState.Closed)
        {
          command.Connection.Close();
        }
      }

      return returnValue;
    }

    /// <summary>
    /// Fills the specified data table.
    /// </summary>
    /// <param name="dataTable">
    /// The data table.
    /// </param>
    /// <returns>
    /// System.Int32.
    /// </returns>
    [DataObjectMethodAttribute(DataObjectMethodType.Fill,
     true)]
    public virtual int Fill(SQLiteOgamaDataSet.TrialsDataTable dataTable)
    {
      this.Adapter.SelectCommand = this.CommandCollection[0];
      if (this.ClearBeforeFill)
      {
        dataTable.Clear();
      }

      int returnValue = this.Adapter.Fill(dataTable);
      return returnValue;
    }

    /// <summary>
    ///   Gets the categories.
    /// </summary>
    /// <returns>SQLiteOgamaDataSet.TrialsDataTable.</returns>
    [DataObjectMethodAttribute(DataObjectMethodType.Select,
     false)]
    public virtual SQLiteOgamaDataSet.TrialsDataTable GetCategories()
    {
      this.Adapter.SelectCommand = this.CommandCollection[2];
      var dataTable = new SQLiteOgamaDataSet.TrialsDataTable();
      this.Adapter.Fill(dataTable);
      return dataTable;
    }

    /// <summary>
    ///   Gets the data.
    /// </summary>
    /// <returns>SQLiteOgamaDataSet.TrialsDataTable.</returns>
    [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
    public virtual SQLiteOgamaDataSet.TrialsDataTable GetData()
    {
      this.Adapter.SelectCommand = this.CommandCollection[0];
      var dataTable = new SQLiteOgamaDataSet.TrialsDataTable();
      this.Adapter.Fill(dataTable);
      return dataTable;
    }

    /// <summary>
    /// Gets the data by subject.
    /// </summary>
    /// <param name="Param1">
    /// The param1.
    /// </param>
    /// <returns>
    /// SQLiteOgamaDataSet.TrialsDataTable.
    /// </returns>
    /// <exception cref="System.ArgumentNullException">
    /// Param1
    /// </exception>
    [DataObjectMethodAttribute(DataObjectMethodType.Select,
     false)]
    public virtual SQLiteOgamaDataSet.TrialsDataTable GetDataBySubject(string Param1)
    {
      this.Adapter.SelectCommand = this.CommandCollection[3];
      if (Param1 == null)
      {
        throw new ArgumentNullException("Param1");
      }

      this.Adapter.SelectCommand.Parameters[0].Value = Param1;
      var dataTable = new SQLiteOgamaDataSet.TrialsDataTable();
      this.Adapter.Fill(dataTable);
      return dataTable;
    }

    /// <summary>
    /// Gets the data by subject and sequence.
    /// </summary>
    /// <param name="Param1">
    /// The param1.
    /// </param>
    /// <param name="Param2">
    /// The param2.
    /// </param>
    /// <returns>
    /// SQLiteOgamaDataSet.TrialsDataTable.
    /// </returns>
    /// <exception cref="System.ArgumentNullException">
    /// Param1
    /// </exception>
    [DataObjectMethodAttribute(DataObjectMethodType.Select,
     false)]
    public virtual SQLiteOgamaDataSet.TrialsDataTable GetDataBySubjectAndSequence(string Param1, int Param2)
    {
      this.Adapter.SelectCommand = this.CommandCollection[4];
      if (Param1 == null)
      {
        throw new ArgumentNullException("Param1");
      }

      this.Adapter.SelectCommand.Parameters[0].Value = Param1;
      this.Adapter.SelectCommand.Parameters[1].Value = Param2;
      var dataTable = new SQLiteOgamaDataSet.TrialsDataTable();
      this.Adapter.Fill(dataTable);
      return dataTable;
    }

    /// <summary>
    /// Gets the data by subject and trial identifier.
    /// </summary>
    /// <param name="Param1">
    /// The param1.
    /// </param>
    /// <param name="Param2">
    /// The param2.
    /// </param>
    /// <returns>
    /// SQLiteOgamaDataSet.TrialsDataTable.
    /// </returns>
    /// <exception cref="System.ArgumentNullException">
    /// Param1
    /// </exception>
    [DataObjectMethodAttribute(DataObjectMethodType.Select,
     false)]
    public virtual SQLiteOgamaDataSet.TrialsDataTable GetDataBySubjectAndTrialID(string Param1, int Param2)
    {
      this.Adapter.SelectCommand = this.CommandCollection[5];
      if (Param1 == null)
      {
        throw new ArgumentNullException("Param1");
      }

      this.Adapter.SelectCommand.Parameters[0].Value = Param1;
      this.Adapter.SelectCommand.Parameters[1].Value = Param2;
      var dataTable = new SQLiteOgamaDataSet.TrialsDataTable();
      this.Adapter.Fill(dataTable);
      return dataTable;
    }

    /// <summary>
    /// Gets the trial sequence by subject and trial identifier.
    /// </summary>
    /// <param name="Param1">
    /// The param1.
    /// </param>
    /// <param name="Param2">
    /// The param2.
    /// </param>
    /// <returns>
    /// SQLiteOgamaDataSet.TrialsDataTable.
    /// </returns>
    /// <exception cref="System.ArgumentNullException">
    /// Param1
    /// </exception>
    [DataObjectMethodAttribute(DataObjectMethodType.Select,
     false)]
    public virtual SQLiteOgamaDataSet.TrialsDataTable GetTrialSequenceBySubjectAndTrialID(string Param1, int Param2)
    {
      this.Adapter.SelectCommand = this.CommandCollection[6];
      if (Param1 == null)
      {
        throw new ArgumentNullException("Param1");
      }

      this.Adapter.SelectCommand.Parameters[0].Value = Param1;
      this.Adapter.SelectCommand.Parameters[1].Value = Param2;
      var dataTable = new SQLiteOgamaDataSet.TrialsDataTable();
      this.Adapter.Fill(dataTable);
      return dataTable;
    }

    /// <summary>
    /// Inserts the specified subject name.
    /// </summary>
    /// <param name="SubjectName">
    /// Name of the subject.
    /// </param>
    /// <param name="TrialID">
    /// The trial identifier.
    /// </param>
    /// <param name="TrialName">
    /// Name of the trial.
    /// </param>
    /// <param name="TrialSequence">
    /// The trial sequence.
    /// </param>
    /// <param name="Category">
    /// The category.
    /// </param>
    /// <param name="TrialStartTime">
    /// The trial start time.
    /// </param>
    /// <param name="Duration">
    /// The duration.
    /// </param>
    /// <param name="EliminateData">
    /// The eliminate data.
    /// </param>
    /// <returns>
    /// System.Int32.
    /// </returns>
    /// <exception cref="System.ArgumentNullException">
    /// SubjectName
    /// </exception>
    [DataObjectMethodAttribute(DataObjectMethodType.Insert,
     true)]
    public virtual int Insert(
      string SubjectName,
      int TrialID,
      string TrialName,
      int TrialSequence,
      string Category,
      long? TrialStartTime,
      int? Duration,
      string EliminateData)
    {
      if (SubjectName == null)
      {
        throw new ArgumentNullException("SubjectName");
      }

      this.Adapter.InsertCommand.Parameters[0].Value = SubjectName;
      this.Adapter.InsertCommand.Parameters[1].Value = TrialID;
      if (TrialName == null)
      {
        this.Adapter.InsertCommand.Parameters[2].Value = DBNull.Value;
      }
      else
      {
        this.Adapter.InsertCommand.Parameters[2].Value = TrialName;
      }

      this.Adapter.InsertCommand.Parameters[3].Value = TrialSequence;
      if (Category == null)
      {
        this.Adapter.InsertCommand.Parameters[4].Value = DBNull.Value;
      }
      else
      {
        this.Adapter.InsertCommand.Parameters[4].Value = Category;
      }

      if (TrialStartTime.HasValue)
      {
        this.Adapter.InsertCommand.Parameters[5].Value = TrialStartTime.Value;
      }
      else
      {
        this.Adapter.InsertCommand.Parameters[5].Value = DBNull.Value;
      }

      if (Duration.HasValue)
      {
        this.Adapter.InsertCommand.Parameters[6].Value = Duration.Value;
      }
      else
      {
        this.Adapter.InsertCommand.Parameters[6].Value = DBNull.Value;
      }

      if (EliminateData == null)
      {
        this.Adapter.InsertCommand.Parameters[7].Value = DBNull.Value;
      }
      else
      {
        this.Adapter.InsertCommand.Parameters[7].Value = EliminateData;
      }

      ConnectionState previousConnectionState = this.Adapter.InsertCommand.Connection.State;
      if ((this.Adapter.InsertCommand.Connection.State & ConnectionState.Open)
           != ConnectionState.Open)
      {
        this.Adapter.InsertCommand.Connection.Open();
      }

      try
      {
        int returnValue = this.Adapter.InsertCommand.ExecuteNonQuery();
        return returnValue;
      }
      finally
      {
        if (previousConnectionState == ConnectionState.Closed)
        {
          this.Adapter.InsertCommand.Connection.Close();
        }
      }
    }

    /// <summary>
    /// Updates the specified data table.
    /// </summary>
    /// <param name="dataTable">
    /// The data table.
    /// </param>
    /// <returns>
    /// System.Int32.
    /// </returns>
    public virtual int Update(SQLiteOgamaDataSet.TrialsDataTable dataTable)
    {
      return this.Adapter.Update(dataTable);
    }

    /// <summary>
    /// Updates the specified data set.
    /// </summary>
    /// <param name="dataSet">
    /// The data set.
    /// </param>
    /// <returns>
    /// System.Int32.
    /// </returns>
    public virtual int Update(SQLiteOgamaDataSet dataSet)
    {
      return this.Adapter.Update(dataSet, "Trials");
    }

    /// <summary>
    /// Updates the specified data row.
    /// </summary>
    /// <param name="dataRow">
    /// The data row.
    /// </param>
    /// <returns>
    /// System.Int32.
    /// </returns>
    public virtual int Update(DataRow dataRow)
    {
      return this.Adapter.Update(new[] { dataRow });
    }

    /// <summary>
    /// Updates the specified data rows.
    /// </summary>
    /// <param name="dataRows">
    /// The data rows.
    /// </param>
    /// <returns>
    /// System.Int32.
    /// </returns>
    public virtual int Update(DataRow[] dataRows)
    {
      return this.Adapter.Update(dataRows);
    }

    /// <summary>
    /// Updates the specified subject name.
    /// </summary>
    /// <param name="SubjectName">
    /// Name of the subject.
    /// </param>
    /// <param name="TrialID">
    /// The trial identifier.
    /// </param>
    /// <param name="TrialName">
    /// Name of the trial.
    /// </param>
    /// <param name="TrialSequence">
    /// The trial sequence.
    /// </param>
    /// <param name="Category">
    /// The category.
    /// </param>
    /// <param name="TrialStartTime">
    /// The trial start time.
    /// </param>
    /// <param name="Duration">
    /// The duration.
    /// </param>
    /// <param name="EliminateData">
    /// The eliminate data.
    /// </param>
    /// <param name="Original_ID">
    /// The original_ identifier.
    /// </param>
    /// <param name="Original_SubjectName">
    /// Name of the original_ subject.
    /// </param>
    /// <param name="Original_TrialID">
    /// The original_ trial identifier.
    /// </param>
    /// <param name="Original_TrialName">
    /// Name of the original_ trial.
    /// </param>
    /// <param name="Original_TrialSequence">
    /// The original_ trial sequence.
    /// </param>
    /// <param name="Original_Category">
    /// The original_ category.
    /// </param>
    /// <param name="Original_TrialStartTime">
    /// The original_ trial start time.
    /// </param>
    /// <param name="Original_Duration">
    /// Duration of the original_.
    /// </param>
    /// <param name="Original_EliminateData">
    /// The original_ eliminate data.
    /// </param>
    /// <param name="ID">
    /// The identifier.
    /// </param>
    /// <returns>
    /// System.Int32.
    /// </returns>
    /// <exception cref="System.ArgumentNullException">
    /// SubjectName
    ///   or
    ///   Original_SubjectName
    /// </exception>
    [DataObjectMethodAttribute(DataObjectMethodType.Update,
     true)]
    public virtual int Update(
      string SubjectName,
      int TrialID,
      string TrialName,
      int TrialSequence,
      string Category,
      long? TrialStartTime,
      int? Duration,
      string EliminateData,
      long Original_ID,
      string Original_SubjectName,
      int Original_TrialID,
      string Original_TrialName,
      int Original_TrialSequence,
      string Original_Category,
      long? Original_TrialStartTime,
      int? Original_Duration,
      string Original_EliminateData,
      long ID)
    {
      if (SubjectName == null)
      {
        throw new ArgumentNullException("SubjectName");
      }

      this.Adapter.UpdateCommand.Parameters[0].Value = SubjectName;
      this.Adapter.UpdateCommand.Parameters[1].Value = TrialID;
      if (TrialName == null)
      {
        this.Adapter.UpdateCommand.Parameters[2].Value = DBNull.Value;
      }
      else
      {
        this.Adapter.UpdateCommand.Parameters[2].Value = TrialName;
      }

      this.Adapter.UpdateCommand.Parameters[3].Value = TrialSequence;
      if (Category == null)
      {
        this.Adapter.UpdateCommand.Parameters[4].Value = DBNull.Value;
      }
      else
      {
        this.Adapter.UpdateCommand.Parameters[4].Value = Category;
      }

      if (TrialStartTime.HasValue)
      {
        this.Adapter.UpdateCommand.Parameters[5].Value = TrialStartTime.Value;
      }
      else
      {
        this.Adapter.UpdateCommand.Parameters[5].Value = DBNull.Value;
      }

      if (Duration.HasValue)
      {
        this.Adapter.UpdateCommand.Parameters[6].Value = Duration.Value;
      }
      else
      {
        this.Adapter.UpdateCommand.Parameters[6].Value = DBNull.Value;
      }

      if (EliminateData == null)
      {
        this.Adapter.UpdateCommand.Parameters[7].Value = DBNull.Value;
      }
      else
      {
        this.Adapter.UpdateCommand.Parameters[7].Value = EliminateData;
      }

      this.Adapter.UpdateCommand.Parameters[8].Value = Original_ID;
      if (Original_SubjectName == null)
      {
        throw new ArgumentNullException("Original_SubjectName");
      }

      this.Adapter.UpdateCommand.Parameters[9].Value = Original_SubjectName;
      this.Adapter.UpdateCommand.Parameters[10].Value = Original_TrialID;
      if (Original_TrialName == null)
      {
        this.Adapter.UpdateCommand.Parameters[11].Value = 1;
        this.Adapter.UpdateCommand.Parameters[12].Value = DBNull.Value;
      }
      else
      {
        this.Adapter.UpdateCommand.Parameters[11].Value = 0;
        this.Adapter.UpdateCommand.Parameters[12].Value = Original_TrialName;
      }

      this.Adapter.UpdateCommand.Parameters[13].Value = Original_TrialSequence;
      if (Original_Category == null)
      {
        this.Adapter.UpdateCommand.Parameters[14].Value = 1;
        this.Adapter.UpdateCommand.Parameters[15].Value = DBNull.Value;
      }
      else
      {
        this.Adapter.UpdateCommand.Parameters[14].Value = 0;
        this.Adapter.UpdateCommand.Parameters[15].Value = Original_Category;
      }

      if (Original_TrialStartTime.HasValue)
      {
        this.Adapter.UpdateCommand.Parameters[16].Value = 0;
        this.Adapter.UpdateCommand.Parameters[17].Value = Original_TrialStartTime.Value;
      }
      else
      {
        this.Adapter.UpdateCommand.Parameters[16].Value = 1;
        this.Adapter.UpdateCommand.Parameters[17].Value = DBNull.Value;
      }

      if (Original_Duration.HasValue)
      {
        this.Adapter.UpdateCommand.Parameters[18].Value = 0;
        this.Adapter.UpdateCommand.Parameters[19].Value = Original_Duration.Value;
      }
      else
      {
        this.Adapter.UpdateCommand.Parameters[18].Value = 1;
        this.Adapter.UpdateCommand.Parameters[19].Value = DBNull.Value;
      }

      if (Original_EliminateData == null)
      {
        this.Adapter.UpdateCommand.Parameters[20].Value = 1;
        this.Adapter.UpdateCommand.Parameters[21].Value = DBNull.Value;
      }
      else
      {
        this.Adapter.UpdateCommand.Parameters[20].Value = 0;
        this.Adapter.UpdateCommand.Parameters[21].Value = Original_EliminateData;
      }

      this.Adapter.UpdateCommand.Parameters[22].Value = ID;
      ConnectionState previousConnectionState = this.Adapter.UpdateCommand.Connection.State;
      if ((this.Adapter.UpdateCommand.Connection.State & ConnectionState.Open)
           != ConnectionState.Open)
      {
        this.Adapter.UpdateCommand.Connection.Open();
      }

      try
      {
        int returnValue = this.Adapter.UpdateCommand.ExecuteNonQuery();
        return returnValue;
      }
      finally
      {
        if (previousConnectionState == ConnectionState.Closed)
        {
          this.Adapter.UpdateCommand.Connection.Close();
        }
      }
    }

    /// <summary>
    /// Updates the specified subject name.
    /// </summary>
    /// <param name="SubjectName">
    /// Name of the subject.
    /// </param>
    /// <param name="TrialID">
    /// The trial identifier.
    /// </param>
    /// <param name="TrialName">
    /// Name of the trial.
    /// </param>
    /// <param name="TrialSequence">
    /// The trial sequence.
    /// </param>
    /// <param name="Category">
    /// The category.
    /// </param>
    /// <param name="TrialStartTime">
    /// The trial start time.
    /// </param>
    /// <param name="Duration">
    /// The duration.
    /// </param>
    /// <param name="EliminateData">
    /// The eliminate data.
    /// </param>
    /// <param name="Original_ID">
    /// The original_ identifier.
    /// </param>
    /// <param name="Original_SubjectName">
    /// Name of the original_ subject.
    /// </param>
    /// <param name="Original_TrialID">
    /// The original_ trial identifier.
    /// </param>
    /// <param name="Original_TrialName">
    /// Name of the original_ trial.
    /// </param>
    /// <param name="Original_TrialSequence">
    /// The original_ trial sequence.
    /// </param>
    /// <param name="Original_Category">
    /// The original_ category.
    /// </param>
    /// <param name="Original_TrialStartTime">
    /// The original_ trial start time.
    /// </param>
    /// <param name="Original_Duration">
    /// Duration of the original_.
    /// </param>
    /// <param name="Original_EliminateData">
    /// The original_ eliminate data.
    /// </param>
    /// <returns>
    /// System.Int32.
    /// </returns>
    [DataObjectMethodAttribute(DataObjectMethodType.Update,
     true)]
    public virtual int Update(
      string SubjectName,
      int TrialID,
      string TrialName,
      int TrialSequence,
      string Category,
      long? TrialStartTime,
      int? Duration,
      string EliminateData,
      long Original_ID,
      string Original_SubjectName,
      int Original_TrialID,
      string Original_TrialName,
      int Original_TrialSequence,
      string Original_Category,
      long? Original_TrialStartTime,
      int? Original_Duration,
      string Original_EliminateData)
    {
      return this.Update(
        SubjectName,
        TrialID,
        TrialName,
        TrialSequence,
        Category,
        TrialStartTime,
        Duration,
        EliminateData,
        Original_ID,
        Original_SubjectName,
        Original_TrialID,
        Original_TrialName,
        Original_TrialSequence,
        Original_Category,
        Original_TrialStartTime,
        Original_Duration,
        Original_EliminateData,
        Original_ID);
    }

    /// <summary>
    /// Updates the eliminate by identifier.
    /// </summary>
    /// <param name="EliminateData">
    /// The eliminate data.
    /// </param>
    /// <param name="Original_ID">
    /// The original_ identifier.
    /// </param>
    /// <returns>
    /// System.Int32.
    /// </returns>
    [DataObjectMethodAttribute(DataObjectMethodType.Update,
     false)]
    public virtual int UpdateEliminateByID(string EliminateData, long Original_ID)
    {
      SQLiteCommand command = this.CommandCollection[7];
      if (EliminateData == null)
      {
        command.Parameters[0].Value = DBNull.Value;
      }
      else
      {
        command.Parameters[0].Value = EliminateData;
      }

      command.Parameters[1].Value = Original_ID;
      ConnectionState previousConnectionState = command.Connection.State;
      if ((command.Connection.State & ConnectionState.Open)
           != ConnectionState.Open)
      {
        command.Connection.Open();
      }

      int returnValue;
      try
      {
        returnValue = command.ExecuteNonQuery();
      }
      finally
      {
        if (previousConnectionState == ConnectionState.Closed)
        {
          command.Connection.Close();
        }
      }

      return returnValue;
    }

    #endregion

    #region Methods

    /// <summary>
    ///   Initializes the adapter.
    /// </summary>
    private void InitAdapter()
    {
      this._adapter = new SQLiteDataAdapter();
      var tableMapping = new DataTableMapping();
      tableMapping.SourceTable = "Table";
      tableMapping.DataSetTable = "Trials";
      tableMapping.ColumnMappings.Add("ID", "ID");
      tableMapping.ColumnMappings.Add("SubjectName", "SubjectName");
      tableMapping.ColumnMappings.Add("TrialID", "TrialID");
      tableMapping.ColumnMappings.Add("TrialName", "TrialName");
      tableMapping.ColumnMappings.Add("TrialSequence", "TrialSequence");
      tableMapping.ColumnMappings.Add("Category", "Category");
      tableMapping.ColumnMappings.Add("TrialStartTime", "TrialStartTime");
      tableMapping.ColumnMappings.Add("Duration", "Duration");
      tableMapping.ColumnMappings.Add("EliminateData", "EliminateData");
      this._adapter.TableMappings.Add(tableMapping);
      this._adapter.DeleteCommand = new SQLiteCommand();
      this._adapter.DeleteCommand.Connection = this.Connection;
      this._adapter.DeleteCommand.CommandText =
        @"DELETE FROM [Trials] WHERE (([ID] = @Original_ID) AND ([SubjectName] = @Original_SubjectName) AND ([TrialID] = @Original_TrialID) AND ((@IsNull_TrialName = 1 AND [TrialName] IS NULL) OR ([TrialName] = @Original_TrialName)) AND ([TrialSequence] = @Original_TrialSequence) AND ((@IsNull_Category = 1 AND [Category] IS NULL) OR ([Category] = @Original_Category)) AND ((@IsNull_TrialStartTime = 1 AND [TrialStartTime] IS NULL) OR ([TrialStartTime] = @Original_TrialStartTime)) AND ((@IsNull_Duration = 1 AND [Duration] IS NULL) OR ([Duration] = @Original_Duration)) AND ((@IsNull_EliminateData = 1 AND [EliminateData] IS NULL) OR ([EliminateData] = @Original_EliminateData)))";
      this._adapter.DeleteCommand.CommandType = CommandType.Text;
      this._adapter.DeleteCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_ID",
          DbType.Int64,
          0,
          ParameterDirection.Input,
          0,
          0,
          "ID",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.DeleteCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_SubjectName",
          DbType.String,
          0,
          ParameterDirection.Input,
          0,
          0,
          "SubjectName",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.DeleteCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_TrialID",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "TrialID",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.DeleteCommand.Parameters.Add(
        new SQLiteParameter(
          "@IsNull_TrialName",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "TrialName",
          DataRowVersion.Original,
          true,
          null));
      this._adapter.DeleteCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_TrialName",
          DbType.String,
          0,
          ParameterDirection.Input,
          0,
          0,
          "TrialName",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.DeleteCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_TrialSequence",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "TrialSequence",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.DeleteCommand.Parameters.Add(
        new SQLiteParameter(
          "@IsNull_Category",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "Category",
          DataRowVersion.Original,
          true,
          null));
      this._adapter.DeleteCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_Category",
          DbType.String,
          0,
          ParameterDirection.Input,
          0,
          0,
          "Category",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.DeleteCommand.Parameters.Add(
        new SQLiteParameter(
          "@IsNull_TrialStartTime",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "TrialStartTime",
          DataRowVersion.Original,
          true,
          null));
      this._adapter.DeleteCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_TrialStartTime",
          DbType.Int64,
          0,
          ParameterDirection.Input,
          0,
          0,
          "TrialStartTime",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.DeleteCommand.Parameters.Add(
        new SQLiteParameter(
          "@IsNull_Duration",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "Duration",
          DataRowVersion.Original,
          true,
          null));
      this._adapter.DeleteCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_Duration",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "Duration",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.DeleteCommand.Parameters.Add(
        new SQLiteParameter(
          "@IsNull_EliminateData",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "EliminateData",
          DataRowVersion.Original,
          true,
          null));
      this._adapter.DeleteCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_EliminateData",
          DbType.String,
          0,
          ParameterDirection.Input,
          0,
          0,
          "EliminateData",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.InsertCommand = new SQLiteCommand();
      this._adapter.InsertCommand.Connection = this.Connection;
      this._adapter.InsertCommand.CommandText =
        @"INSERT INTO [Trials] ([SubjectName], [TrialID], [TrialName], [TrialSequence], [Category], [TrialStartTime], [Duration], [EliminateData]) VALUES (@SubjectName, @TrialID, @TrialName, @TrialSequence, @Category, @TrialStartTime, @Duration, @EliminateData);
SELECT ID, SubjectName, TrialID, TrialName, TrialSequence, Category, TrialStartTime, Duration, EliminateData FROM Trials WHERE (ID = last_insert_rowid())";
      this._adapter.InsertCommand.CommandType = CommandType.Text;
      this._adapter.InsertCommand.Parameters.Add(
        new SQLiteParameter(
          "@SubjectName",
          DbType.String,
          0,
          ParameterDirection.Input,
          0,
          0,
          "SubjectName",
          DataRowVersion.Current,
          false,
          null));
      this._adapter.InsertCommand.Parameters.Add(
        new SQLiteParameter(
          "@TrialID",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "TrialID",
          DataRowVersion.Current,
          false,
          null));
      this._adapter.InsertCommand.Parameters.Add(
        new SQLiteParameter(
          "@TrialName",
          DbType.String,
          0,
          ParameterDirection.Input,
          0,
          0,
          "TrialName",
          DataRowVersion.Current,
          false,
          null));
      this._adapter.InsertCommand.Parameters.Add(
        new SQLiteParameter(
          "@TrialSequence",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "TrialSequence",
          DataRowVersion.Current,
          false,
          null));
      this._adapter.InsertCommand.Parameters.Add(
        new SQLiteParameter(
          "@Category",
          DbType.String,
          0,
          ParameterDirection.Input,
          0,
          0,
          "Category",
          DataRowVersion.Current,
          false,
          null));
      this._adapter.InsertCommand.Parameters.Add(
        new SQLiteParameter(
          "@TrialStartTime",
          DbType.Int64,
          0,
          ParameterDirection.Input,
          0,
          0,
          "TrialStartTime",
          DataRowVersion.Current,
          false,
          null));
      this._adapter.InsertCommand.Parameters.Add(
        new SQLiteParameter(
          "@Duration",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "Duration",
          DataRowVersion.Current,
          false,
          null));
      this._adapter.InsertCommand.Parameters.Add(
        new SQLiteParameter(
          "@EliminateData",
          DbType.String,
          0,
          ParameterDirection.Input,
          0,
          0,
          "EliminateData",
          DataRowVersion.Current,
          false,
          null));
      this._adapter.UpdateCommand = new SQLiteCommand();
      this._adapter.UpdateCommand.Connection = this.Connection;
      this._adapter.UpdateCommand.CommandText =
        @"UPDATE [Trials] SET [SubjectName] = @SubjectName, [TrialID] = @TrialID, [TrialName] = @TrialName, [TrialSequence] = @TrialSequence, [Category] = @Category, [TrialStartTime] = @TrialStartTime, [Duration] = @Duration, [EliminateData] = @EliminateData WHERE (([ID] = @Original_ID) AND ([SubjectName] = @Original_SubjectName) AND ([TrialID] = @Original_TrialID) AND ((@IsNull_TrialName = 1 AND [TrialName] IS NULL) OR ([TrialName] = @Original_TrialName)) AND ([TrialSequence] = @Original_TrialSequence) AND ((@IsNull_Category = 1 AND [Category] IS NULL) OR ([Category] = @Original_Category)) AND ((@IsNull_TrialStartTime = 1 AND [TrialStartTime] IS NULL) OR ([TrialStartTime] = @Original_TrialStartTime)) AND ((@IsNull_Duration = 1 AND [Duration] IS NULL) OR ([Duration] = @Original_Duration)) AND ((@IsNull_EliminateData = 1 AND [EliminateData] IS NULL) OR ([EliminateData] = @Original_EliminateData)));
SELECT ID, SubjectName, TrialID, TrialName, TrialSequence, Category, TrialStartTime, Duration, EliminateData FROM Trials WHERE (ID = @ID)";
      this._adapter.UpdateCommand.CommandType = CommandType.Text;
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@SubjectName",
          DbType.String,
          0,
          ParameterDirection.Input,
          0,
          0,
          "SubjectName",
          DataRowVersion.Current,
          false,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@TrialID",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "TrialID",
          DataRowVersion.Current,
          false,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@TrialName",
          DbType.String,
          0,
          ParameterDirection.Input,
          0,
          0,
          "TrialName",
          DataRowVersion.Current,
          false,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@TrialSequence",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "TrialSequence",
          DataRowVersion.Current,
          false,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@Category",
          DbType.String,
          0,
          ParameterDirection.Input,
          0,
          0,
          "Category",
          DataRowVersion.Current,
          false,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@TrialStartTime",
          DbType.Int64,
          0,
          ParameterDirection.Input,
          0,
          0,
          "TrialStartTime",
          DataRowVersion.Current,
          false,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@Duration",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "Duration",
          DataRowVersion.Current,
          false,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@EliminateData",
          DbType.String,
          0,
          ParameterDirection.Input,
          0,
          0,
          "EliminateData",
          DataRowVersion.Current,
          false,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_ID",
          DbType.Int64,
          0,
          ParameterDirection.Input,
          0,
          0,
          "ID",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_SubjectName",
          DbType.String,
          0,
          ParameterDirection.Input,
          0,
          0,
          "SubjectName",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_TrialID",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "TrialID",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@IsNull_TrialName",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "TrialName",
          DataRowVersion.Original,
          true,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_TrialName",
          DbType.String,
          0,
          ParameterDirection.Input,
          0,
          0,
          "TrialName",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_TrialSequence",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "TrialSequence",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@IsNull_Category",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "Category",
          DataRowVersion.Original,
          true,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_Category",
          DbType.String,
          0,
          ParameterDirection.Input,
          0,
          0,
          "Category",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@IsNull_TrialStartTime",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "TrialStartTime",
          DataRowVersion.Original,
          true,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_TrialStartTime",
          DbType.Int64,
          0,
          ParameterDirection.Input,
          0,
          0,
          "TrialStartTime",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@IsNull_Duration",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "Duration",
          DataRowVersion.Original,
          true,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_Duration",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "Duration",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@IsNull_EliminateData",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "EliminateData",
          DataRowVersion.Original,
          true,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_EliminateData",
          DbType.String,
          0,
          ParameterDirection.Input,
          0,
          0,
          "EliminateData",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@ID",
          DbType.Int64,
          8,
          ParameterDirection.Input,
          0,
          0,
          "ID",
          DataRowVersion.Current,
          false,
          null));
    }

    /// <summary>
    ///   Initializes the command collection.
    /// </summary>
    private void InitCommandCollection()
    {
      this._commandCollection = new SQLiteCommand[8];
      this._commandCollection[0] = new SQLiteCommand();
      this._commandCollection[0].Connection = this.Connection;
      this._commandCollection[0].CommandText =
        "SELECT     ID, SubjectName, TrialID, TrialName, TrialSequence, Category, TrialSta"
        + "rtTime, Duration, EliminateData\r\nFROM         Trials";
      this._commandCollection[0].CommandType = CommandType.Text;
      this._commandCollection[1] = new SQLiteCommand();
      this._commandCollection[1].Connection = this.Connection;
      this._commandCollection[1].CommandText = "DELETE FROM [Trials] WHERE ([SubjectName] = @Original_SubjectName)";
      this._commandCollection[1].CommandType = CommandType.Text;
      this._commandCollection[1].Parameters.Add(
        new SQLiteParameter(
          "@Original_SubjectName",
          DbType.String,
          50,
          ParameterDirection.Input,
          0,
          0,
          "SubjectName",
          DataRowVersion.Original,
          false,
          null));
      this._commandCollection[2] = new SQLiteCommand();
      this._commandCollection[2].Connection = this.Connection;
      this._commandCollection[2].CommandText = "SELECT     Category\r\nFROM         Trials\r\nGROUP BY Category";
      this._commandCollection[2].CommandType = CommandType.Text;
      this._commandCollection[3] = new SQLiteCommand();
      this._commandCollection[3].Connection = this.Connection;
      this._commandCollection[3].CommandText =
        "SELECT Category, Duration, EliminateData, ID, SubjectName, TrialID, TrialName, Tr"
        + "ialSequence, TrialStartTime FROM Trials WHERE (SubjectName = @Param1)";
      this._commandCollection[3].CommandType = CommandType.Text;
      this._commandCollection[3].Parameters.Add(
        new SQLiteParameter(
          "@Param1",
          DbType.String,
          50,
          ParameterDirection.Input,
          0,
          0,
          "SubjectName",
          DataRowVersion.Current,
          false,
          null));
      this._commandCollection[4] = new SQLiteCommand();
      this._commandCollection[4].Connection = this.Connection;
      this._commandCollection[4].CommandText =
        "SELECT Category, Duration, EliminateData, ID, SubjectName, TrialID, TrialName, Tr"
        + "ialSequence, TrialStartTime FROM Trials WHERE (SubjectName = @Param1) AND (Trial" + "Sequence = @Param2)";
      this._commandCollection[4].CommandType = CommandType.Text;
      this._commandCollection[4].Parameters.Add(
        new SQLiteParameter(
          "@Param1",
          DbType.String,
          50,
          ParameterDirection.Input,
          0,
          0,
          "SubjectName",
          DataRowVersion.Current,
          false,
          null));
      this._commandCollection[4].Parameters.Add(
        new SQLiteParameter(
          "@Param2",
          DbType.Int32,
          4,
          ParameterDirection.Input,
          0,
          0,
          "TrialSequence",
          DataRowVersion.Current,
          false,
          null));
      this._commandCollection[5] = new SQLiteCommand();
      this._commandCollection[5].Connection = this.Connection;
      this._commandCollection[5].CommandText =
        "SELECT Category, Duration, EliminateData, ID, SubjectName, TrialID, TrialName, Tr"
        + "ialSequence, TrialStartTime FROM Trials WHERE (SubjectName = @Param1) AND (Trial" + "ID = @Param2)";
      this._commandCollection[5].CommandType = CommandType.Text;
      this._commandCollection[5].Parameters.Add(
        new SQLiteParameter(
          "@Param1",
          DbType.String,
          50,
          ParameterDirection.Input,
          0,
          0,
          "SubjectName",
          DataRowVersion.Current,
          false,
          null));
      this._commandCollection[5].Parameters.Add(
        new SQLiteParameter(
          "@Param2",
          DbType.Int32,
          4,
          ParameterDirection.Input,
          0,
          0,
          "TrialID",
          DataRowVersion.Current,
          false,
          null));
      this._commandCollection[6] = new SQLiteCommand();
      this._commandCollection[6].Connection = this.Connection;
      this._commandCollection[6].CommandText =
        "SELECT     SubjectName, TrialID, TrialSequence\r\nFROM         Trials\r\nWHERE     (S"
        + "ubjectName = @Param1) AND (TrialID = @Param2)\r\nGROUP BY SubjectName, TrialID, Tr" + "ialSequence";
      this._commandCollection[6].CommandType = CommandType.Text;
      this._commandCollection[6].Parameters.Add(
        new SQLiteParameter(
          "@Param1",
          DbType.String,
          50,
          ParameterDirection.Input,
          0,
          0,
          "SubjectName",
          DataRowVersion.Current,
          false,
          null));
      this._commandCollection[6].Parameters.Add(
        new SQLiteParameter(
          "@Param2",
          DbType.Int32,
          4,
          ParameterDirection.Input,
          0,
          0,
          "TrialID",
          DataRowVersion.Current,
          false,
          null));
      this._commandCollection[7] = new SQLiteCommand();
      this._commandCollection[7].Connection = this.Connection;
      this._commandCollection[7].CommandText =
        "UPDATE    Trials\r\nSET              EliminateData = @EliminateData\r\nWHERE     (ID " + "= @Original_ID);   ";
      this._commandCollection[7].CommandType = CommandType.Text;
      this._commandCollection[7].Parameters.Add(
        new SQLiteParameter(
          "@EliminateData",
          DbType.String,
          50,
          ParameterDirection.Input,
          0,
          0,
          "EliminateData",
          DataRowVersion.Current,
          false,
          null));
      this._commandCollection[7].Parameters.Add(
        new SQLiteParameter(
          "@Original_ID",
          DbType.Int64,
          8,
          ParameterDirection.Input,
          0,
          0,
          "ID",
          DataRowVersion.Original,
          false,
          null));
    }

    /// <summary>
    ///   Initializes the connection.
    /// </summary>
    private void InitConnection()
    {
      this._connection = new SQLiteConnection();
      this._connection.ConnectionString =
        global::Ogama.Properties.Settings.Default.OgamaDatabaseTemplateConnectionString;
    }

    #endregion
  }

  /// <summary>
  ///   Represents the connection and commands used to retrieve and save data.
  /// </summary>
  public class SQLiteTadTrialEvents : Component
  {
    #region Fields

    /// <summary>
    ///   The _adapter
    /// </summary>
    private SQLiteDataAdapter _adapter;

    /// <summary>
    ///   The _command collection
    /// </summary>
    private SQLiteCommand[] _commandCollection;

    /// <summary>
    ///   The _connection
    /// </summary>
    private SQLiteConnection _connection;

    #endregion

    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="SQLiteTadTrialEvents" /> class.
    /// </summary>
    public SQLiteTadTrialEvents()
    {
      this.ClearBeforeFill = true;
    }

    #endregion

    #region Public Properties

    /// <summary>
    ///   Gets or sets a value indicating whether [clear before fill].
    /// </summary>
    /// <value><c>true</c> if [clear before fill]; otherwise, <c>false</c>.</value>
    public bool ClearBeforeFill { get; set; }

    #endregion

    #region Properties

    /// <summary>
    ///   Gets or sets the connection.
    /// </summary>
    /// <value>The connection.</value>
    internal SQLiteConnection Connection
    {
      get
      {
        if (this._connection == null)
        {
          this.InitConnection();
        }

        return this._connection;
      }

      set
      {
        this._connection = value;
        if (this.Adapter.InsertCommand != null)
        {
          this.Adapter.InsertCommand.Connection = value;
        }

        if (this.Adapter.DeleteCommand != null)
        {
          this.Adapter.DeleteCommand.Connection = value;
        }

        if (this.Adapter.UpdateCommand != null)
        {
          this.Adapter.UpdateCommand.Connection = value;
        }

        for (int i = 0; i < this.CommandCollection.Length; i = (i + 1))
        {
          if ((this.CommandCollection[i] != null))
          {
            this.CommandCollection[i].Connection = value;
          }
        }
      }
    }

    /// <summary>
    ///   Gets the command collection.
    /// </summary>
    /// <value>The command collection.</value>
    protected SQLiteCommand[] CommandCollection
    {
      get
      {
        if (this._commandCollection == null)
        {
          this.InitCommandCollection();
        }

        return this._commandCollection;
      }
    }

    /// <summary>
    ///   Gets the adapter.
    /// </summary>
    /// <value>The adapter.</value>
    private SQLiteDataAdapter Adapter
    {
      get
      {
        if (this._adapter == null)
        {
          this.InitAdapter();
        }

        return this._adapter;
      }
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// Deletes the specified original_ identifier.
    /// </summary>
    /// <param name="Original_ID">
    /// The original_ identifier.
    /// </param>
    /// <param name="Original_SubjectName">
    /// Name of the original_ subject.
    /// </param>
    /// <param name="Original_TrialSequence">
    /// The original_ trial sequence.
    /// </param>
    /// <param name="Original_EventID">
    /// The original_ event identifier.
    /// </param>
    /// <param name="Original_EventTime">
    /// The original_ event time.
    /// </param>
    /// <param name="Original_EventType">
    /// Type of the original_ event.
    /// </param>
    /// <param name="Original_EventTask">
    /// The original_ event task.
    /// </param>
    /// <param name="Original_EventParam">
    /// The original_ event parameter.
    /// </param>
    /// <returns>
    /// System.Int32.
    /// </returns>
    /// <exception cref="System.ArgumentNullException">
    /// Original_SubjectName
    ///   or
    ///   Original_EventType
    /// </exception>
    [DataObjectMethodAttribute(DataObjectMethodType.Delete,
     true)]
    public virtual int Delete(
      int Original_ID,
      string Original_SubjectName,
      int Original_TrialSequence,
      int Original_EventID,
      long Original_EventTime,
      string Original_EventType,
      string Original_EventTask,
      string Original_EventParam)
    {
      this.Adapter.DeleteCommand.Parameters[0].Value = Original_ID;
      if (Original_SubjectName == null)
      {
        throw new ArgumentNullException("Original_SubjectName");
      }

      this.Adapter.DeleteCommand.Parameters[1].Value = Original_SubjectName;
      this.Adapter.DeleteCommand.Parameters[2].Value = Original_TrialSequence;
      this.Adapter.DeleteCommand.Parameters[3].Value = Original_EventID;
      this.Adapter.DeleteCommand.Parameters[4].Value = Original_EventTime;
      if (Original_EventType == null)
      {
        throw new ArgumentNullException("Original_EventType");
      }

      this.Adapter.DeleteCommand.Parameters[5].Value = Original_EventType;
      if (Original_EventTask == null)
      {
        this.Adapter.DeleteCommand.Parameters[6].Value = 1;
        this.Adapter.DeleteCommand.Parameters[7].Value = DBNull.Value;
      }
      else
      {
        this.Adapter.DeleteCommand.Parameters[6].Value = 0;
        this.Adapter.DeleteCommand.Parameters[7].Value = Original_EventTask;
      }

      if (Original_EventParam == null)
      {
        this.Adapter.DeleteCommand.Parameters[8].Value = 1;
        this.Adapter.DeleteCommand.Parameters[9].Value = DBNull.Value;
      }
      else
      {
        this.Adapter.DeleteCommand.Parameters[8].Value = 0;
        this.Adapter.DeleteCommand.Parameters[9].Value = Original_EventParam;
      }

      ConnectionState previousConnectionState = this.Adapter.DeleteCommand.Connection.State;
      if ((this.Adapter.DeleteCommand.Connection.State & ConnectionState.Open)
           != ConnectionState.Open)
      {
        this.Adapter.DeleteCommand.Connection.Open();
      }

      try
      {
        int returnValue = this.Adapter.DeleteCommand.ExecuteNonQuery();
        return returnValue;
      }
      finally
      {
        if (previousConnectionState == ConnectionState.Closed)
        {
          this.Adapter.DeleteCommand.Connection.Close();
        }
      }
    }

    /// <summary>
    /// Deletes the name of the by subject.
    /// </summary>
    /// <param name="Original_SubjectName">
    /// Name of the original_ subject.
    /// </param>
    /// <returns>
    /// System.Int32.
    /// </returns>
    /// <exception cref="System.ArgumentNullException">
    /// Original_SubjectName
    /// </exception>
    [DataObjectMethodAttribute(DataObjectMethodType.Delete,
     false)]
    public virtual int DeleteBySubjectName(string Original_SubjectName)
    {
      SQLiteCommand command = this.CommandCollection[1];
      if (Original_SubjectName == null)
      {
        throw new ArgumentNullException("Original_SubjectName");
      }

      command.Parameters[0].Value = Original_SubjectName;
      ConnectionState previousConnectionState = command.Connection.State;
      if ((command.Connection.State & ConnectionState.Open)
           != ConnectionState.Open)
      {
        command.Connection.Open();
      }

      int returnValue;
      try
      {
        returnValue = command.ExecuteNonQuery();
      }
      finally
      {
        if (previousConnectionState == ConnectionState.Closed)
        {
          command.Connection.Close();
        }
      }

      return returnValue;
    }

    /// <summary>
    /// Deletes the event by subject name trial sequence event identifier.
    /// </summary>
    /// <param name="Original_SubjectName">
    /// Name of the original_ subject.
    /// </param>
    /// <param name="Original_TrialSequence">
    /// The original_ trial sequence.
    /// </param>
    /// <param name="Original_EventID">
    /// The original_ event identifier.
    /// </param>
    /// <returns>
    /// System.Int32.
    /// </returns>
    /// <exception cref="System.ArgumentNullException">
    /// Original_SubjectName
    /// </exception>
    [DataObjectMethodAttribute(DataObjectMethodType.Delete,
     false)]
    public virtual int DeleteEventBySubjectNameTrialSequenceEventID(
      string Original_SubjectName,
      int Original_TrialSequence,
      int Original_EventID)
    {
      SQLiteCommand command = this.CommandCollection[2];
      if (Original_SubjectName == null)
      {
        throw new ArgumentNullException("Original_SubjectName");
      }

      command.Parameters[0].Value = Original_SubjectName;
      command.Parameters[1].Value = Original_TrialSequence;
      command.Parameters[2].Value = Original_EventID;
      ConnectionState previousConnectionState = command.Connection.State;
      if ((command.Connection.State & ConnectionState.Open)
           != ConnectionState.Open)
      {
        command.Connection.Open();
      }

      int returnValue;
      try
      {
        returnValue = command.ExecuteNonQuery();
      }
      finally
      {
        if (previousConnectionState == ConnectionState.Closed)
        {
          command.Connection.Close();
        }
      }

      return returnValue;
    }

    /// <summary>
    /// Fills the specified data table.
    /// </summary>
    /// <param name="dataTable">
    /// The data table.
    /// </param>
    /// <returns>
    /// System.Int32.
    /// </returns>
    [DataObjectMethodAttribute(DataObjectMethodType.Fill,
     true)]
    public virtual int Fill(SQLiteOgamaDataSet.TrialEventsDataTable dataTable)
    {
      this.Adapter.SelectCommand = this.CommandCollection[0];
      if (this.ClearBeforeFill)
      {
        dataTable.Clear();
      }

      int returnValue = this.Adapter.Fill(dataTable);
      return returnValue;
    }

    /// <summary>
    ///   Gets the data.
    /// </summary>
    /// <returns>SQLiteOgamaDataSet.TrialEventsDataTable.</returns>
    [DataObjectMethodAttribute(DataObjectMethodType.Select,
     true)]
    public virtual SQLiteOgamaDataSet.TrialEventsDataTable GetData()
    {
      this.Adapter.SelectCommand = this.CommandCollection[0];
      var dataTable = new SQLiteOgamaDataSet.TrialEventsDataTable();
      this.Adapter.Fill(dataTable);
      return dataTable;
    }

    /// <summary>
    /// Gets the data by subject.
    /// </summary>
    /// <param name="Param1">
    /// The param1.
    /// </param>
    /// <returns>
    /// SQLiteOgamaDataSet.TrialEventsDataTable.
    /// </returns>
    /// <exception cref="System.ArgumentNullException">
    /// Param1
    /// </exception>
    [DataObjectMethodAttribute(DataObjectMethodType.Select,
     false)]
    public virtual SQLiteOgamaDataSet.TrialEventsDataTable GetDataBySubject(string Param1)
    {
      this.Adapter.SelectCommand = this.CommandCollection[3];
      if (Param1 == null)
      {
        throw new ArgumentNullException("Param1");
      }

      this.Adapter.SelectCommand.Parameters[0].Value = Param1;
      var dataTable = new SQLiteOgamaDataSet.TrialEventsDataTable();
      this.Adapter.Fill(dataTable);
      return dataTable;
    }

    /// <summary>
    /// Gets the data by subject and sequence.
    /// </summary>
    /// <param name="Param1">
    /// The param1.
    /// </param>
    /// <param name="Param2">
    /// The param2.
    /// </param>
    /// <returns>
    /// SQLiteOgamaDataSet.TrialEventsDataTable.
    /// </returns>
    /// <exception cref="System.ArgumentNullException">
    /// Param1
    /// </exception>
    [DataObjectMethodAttribute(DataObjectMethodType.Select,
     false)]
    public virtual SQLiteOgamaDataSet.TrialEventsDataTable GetDataBySubjectAndSequence(string Param1, int Param2)
    {
      this.Adapter.SelectCommand = this.CommandCollection[4];
      if (Param1 == null)
      {
        throw new ArgumentNullException("Param1");
      }

      this.Adapter.SelectCommand.Parameters[0].Value = Param1;
      this.Adapter.SelectCommand.Parameters[1].Value = Param2;
      var dataTable = new SQLiteOgamaDataSet.TrialEventsDataTable();
      this.Adapter.Fill(dataTable);
      return dataTable;
    }

    /// <summary>
    /// Gets the data by subject name trial sequence but only slide change responses.
    /// </summary>
    /// <param name="Param1">
    /// The param1.
    /// </param>
    /// <param name="Param2">
    /// The param2.
    /// </param>
    /// <returns>
    /// SQLiteOgamaDataSet.TrialEventsDataTable.
    /// </returns>
    /// <exception cref="System.ArgumentNullException">
    /// Param1
    /// </exception>
    [DataObjectMethodAttribute(DataObjectMethodType.Select,
     false)]
    public virtual SQLiteOgamaDataSet.TrialEventsDataTable GetDataBySubjectNameTrialSequenceButOnlySlideChangeResponses(
      string Param1,
      int Param2)
    {
      this.Adapter.SelectCommand = this.CommandCollection[5];
      if (Param1 == null)
      {
        throw new ArgumentNullException("Param1");
      }

      this.Adapter.SelectCommand.Parameters[0].Value = Param1;
      this.Adapter.SelectCommand.Parameters[1].Value = Param2;
      var dataTable = new SQLiteOgamaDataSet.TrialEventsDataTable();
      this.Adapter.Fill(dataTable);
      return dataTable;
    }

    /// <summary>
    ///   Gets the maximum event identifier.
    /// </summary>
    /// <returns>System.Nullable&lt;System.Int32&gt;.</returns>
    public virtual int? GetMaxEventID()
    {
      SQLiteCommand command = this.CommandCollection[6];
      ConnectionState previousConnectionState = command.Connection.State;
      if ((command.Connection.State & ConnectionState.Open)
           != ConnectionState.Open)
      {
        command.Connection.Open();
      }

      object returnValue;
      try
      {
        returnValue = command.ExecuteScalar();
      }
      finally
      {
        if (previousConnectionState == ConnectionState.Closed)
        {
          command.Connection.Close();
        }
      }

      if ((returnValue == null) || (returnValue.GetType() == typeof(DBNull)))
      {
        return new int?();
      }

      return (int)(returnValue);
    }

    /// <summary>
    /// Inserts the specified subject name.
    /// </summary>
    /// <param name="SubjectName">
    /// Name of the subject.
    /// </param>
    /// <param name="TrialSequence">
    /// The trial sequence.
    /// </param>
    /// <param name="EventID">
    /// The event identifier.
    /// </param>
    /// <param name="EventTime">
    /// The event time.
    /// </param>
    /// <param name="EventType">
    /// Type of the event.
    /// </param>
    /// <param name="EventTask">
    /// The event task.
    /// </param>
    /// <param name="EventParam">
    /// The event parameter.
    /// </param>
    /// <returns>
    /// System.Int32.
    /// </returns>
    /// <exception cref="System.ArgumentNullException">
    /// SubjectName
    ///   or
    ///   EventType
    /// </exception>
    [DataObjectMethodAttribute(DataObjectMethodType.Insert,
     true)]
    public virtual int Insert(
      string SubjectName,
      int TrialSequence,
      int EventID,
      long EventTime,
      string EventType,
      string EventTask,
      string EventParam)
    {
      if (SubjectName == null)
      {
        throw new ArgumentNullException("SubjectName");
      }

      this.Adapter.InsertCommand.Parameters[0].Value = SubjectName;
      this.Adapter.InsertCommand.Parameters[1].Value = TrialSequence;
      this.Adapter.InsertCommand.Parameters[2].Value = EventID;
      this.Adapter.InsertCommand.Parameters[3].Value = EventTime;
      if (EventType == null)
      {
        throw new ArgumentNullException("EventType");
      }

      this.Adapter.InsertCommand.Parameters[4].Value = EventType;
      if (EventTask == null)
      {
        this.Adapter.InsertCommand.Parameters[5].Value = DBNull.Value;
      }
      else
      {
        this.Adapter.InsertCommand.Parameters[5].Value = EventTask;
      }

      if (EventParam == null)
      {
        this.Adapter.InsertCommand.Parameters[6].Value = DBNull.Value;
      }
      else
      {
        this.Adapter.InsertCommand.Parameters[6].Value = EventParam;
      }

      ConnectionState previousConnectionState = this.Adapter.InsertCommand.Connection.State;
      if ((this.Adapter.InsertCommand.Connection.State & ConnectionState.Open)
           != ConnectionState.Open)
      {
        this.Adapter.InsertCommand.Connection.Open();
      }

      try
      {
        int returnValue = this.Adapter.InsertCommand.ExecuteNonQuery();
        return returnValue;
      }
      finally
      {
        if (previousConnectionState == ConnectionState.Closed)
        {
          this.Adapter.InsertCommand.Connection.Close();
        }
      }
    }

    /// <summary>
    /// Updates the specified data table.
    /// </summary>
    /// <param name="dataTable">
    /// The data table.
    /// </param>
    /// <returns>
    /// System.Int32.
    /// </returns>
    public virtual int Update(SQLiteOgamaDataSet.TrialEventsDataTable dataTable)
    {
      return this.Adapter.Update(dataTable);
    }

    /// <summary>
    /// Updates the specified data set.
    /// </summary>
    /// <param name="dataSet">
    /// The data set.
    /// </param>
    /// <returns>
    /// System.Int32.
    /// </returns>
    public virtual int Update(SQLiteOgamaDataSet dataSet)
    {
      return this.Adapter.Update(dataSet, "TrialEvents");
    }

    /// <summary>
    /// Updates the specified data row.
    /// </summary>
    /// <param name="dataRow">
    /// The data row.
    /// </param>
    /// <returns>
    /// System.Int32.
    /// </returns>
    public virtual int Update(DataRow dataRow)
    {
      return this.Adapter.Update(new[] { dataRow });
    }

    /// <summary>
    /// Updates the specified data rows.
    /// </summary>
    /// <param name="dataRows">
    /// The data rows.
    /// </param>
    /// <returns>
    /// System.Int32.
    /// </returns>
    public virtual int Update(DataRow[] dataRows)
    {
      return this.Adapter.Update(dataRows);
    }

    /// <summary>
    /// Updates the specified subject name.
    /// </summary>
    /// <param name="SubjectName">
    /// Name of the subject.
    /// </param>
    /// <param name="TrialSequence">
    /// The trial sequence.
    /// </param>
    /// <param name="EventID">
    /// The event identifier.
    /// </param>
    /// <param name="EventTime">
    /// The event time.
    /// </param>
    /// <param name="EventType">
    /// Type of the event.
    /// </param>
    /// <param name="EventTask">
    /// The event task.
    /// </param>
    /// <param name="EventParam">
    /// The event parameter.
    /// </param>
    /// <param name="Original_ID">
    /// The original_ identifier.
    /// </param>
    /// <param name="Original_SubjectName">
    /// Name of the original_ subject.
    /// </param>
    /// <param name="Original_TrialSequence">
    /// The original_ trial sequence.
    /// </param>
    /// <param name="Original_EventID">
    /// The original_ event identifier.
    /// </param>
    /// <param name="Original_EventTime">
    /// The original_ event time.
    /// </param>
    /// <param name="Original_EventType">
    /// Type of the original_ event.
    /// </param>
    /// <param name="Original_EventTask">
    /// The original_ event task.
    /// </param>
    /// <param name="Original_EventParam">
    /// The original_ event parameter.
    /// </param>
    /// <param name="ID">
    /// The identifier.
    /// </param>
    /// <returns>
    /// System.Int32.
    /// </returns>
    /// <exception cref="System.ArgumentNullException">
    /// SubjectName
    ///   or
    ///   EventType
    ///   or
    ///   Original_SubjectName
    ///   or
    ///   Original_EventType
    /// </exception>
    [DataObjectMethodAttribute(DataObjectMethodType.Update,
     true)]
    public virtual int Update(
      string SubjectName,
      int TrialSequence,
      int EventID,
      long EventTime,
      string EventType,
      string EventTask,
      string EventParam,
      int Original_ID,
      string Original_SubjectName,
      int Original_TrialSequence,
      int Original_EventID,
      long Original_EventTime,
      string Original_EventType,
      string Original_EventTask,
      string Original_EventParam,
      int ID)
    {
      if (SubjectName == null)
      {
        throw new ArgumentNullException("SubjectName");
      }

      this.Adapter.UpdateCommand.Parameters[0].Value = SubjectName;
      this.Adapter.UpdateCommand.Parameters[1].Value = TrialSequence;
      this.Adapter.UpdateCommand.Parameters[2].Value = EventID;
      this.Adapter.UpdateCommand.Parameters[3].Value = EventTime;
      if (EventType == null)
      {
        throw new ArgumentNullException("EventType");
      }

      this.Adapter.UpdateCommand.Parameters[4].Value = EventType;
      if (EventTask == null)
      {
        this.Adapter.UpdateCommand.Parameters[5].Value = DBNull.Value;
      }
      else
      {
        this.Adapter.UpdateCommand.Parameters[5].Value = EventTask;
      }

      if (EventParam == null)
      {
        this.Adapter.UpdateCommand.Parameters[6].Value = DBNull.Value;
      }
      else
      {
        this.Adapter.UpdateCommand.Parameters[6].Value = EventParam;
      }

      this.Adapter.UpdateCommand.Parameters[7].Value = Original_ID;
      if (Original_SubjectName == null)
      {
        throw new ArgumentNullException("Original_SubjectName");
      }

      this.Adapter.UpdateCommand.Parameters[8].Value = Original_SubjectName;
      this.Adapter.UpdateCommand.Parameters[9].Value = Original_TrialSequence;
      this.Adapter.UpdateCommand.Parameters[10].Value = Original_EventID;
      this.Adapter.UpdateCommand.Parameters[11].Value = Original_EventTime;
      if (Original_EventType == null)
      {
        throw new ArgumentNullException("Original_EventType");
      }

      this.Adapter.UpdateCommand.Parameters[12].Value = Original_EventType;
      if (Original_EventTask == null)
      {
        this.Adapter.UpdateCommand.Parameters[13].Value = 1;
        this.Adapter.UpdateCommand.Parameters[14].Value = DBNull.Value;
      }
      else
      {
        this.Adapter.UpdateCommand.Parameters[13].Value = 0;
        this.Adapter.UpdateCommand.Parameters[14].Value = Original_EventTask;
      }

      if (Original_EventParam == null)
      {
        this.Adapter.UpdateCommand.Parameters[15].Value = 1;
        this.Adapter.UpdateCommand.Parameters[16].Value = DBNull.Value;
      }
      else
      {
        this.Adapter.UpdateCommand.Parameters[15].Value = 0;
        this.Adapter.UpdateCommand.Parameters[16].Value = Original_EventParam;
      }

      this.Adapter.UpdateCommand.Parameters[17].Value = ID;
      ConnectionState previousConnectionState = this.Adapter.UpdateCommand.Connection.State;
      if ((this.Adapter.UpdateCommand.Connection.State & ConnectionState.Open)
           != ConnectionState.Open)
      {
        this.Adapter.UpdateCommand.Connection.Open();
      }

      try
      {
        int returnValue = this.Adapter.UpdateCommand.ExecuteNonQuery();
        return returnValue;
      }
      finally
      {
        if (previousConnectionState == ConnectionState.Closed)
        {
          this.Adapter.UpdateCommand.Connection.Close();
        }
      }
    }

    /// <summary>
    /// Updates the specified subject name.
    /// </summary>
    /// <param name="SubjectName">
    /// Name of the subject.
    /// </param>
    /// <param name="TrialSequence">
    /// The trial sequence.
    /// </param>
    /// <param name="EventID">
    /// The event identifier.
    /// </param>
    /// <param name="EventTime">
    /// The event time.
    /// </param>
    /// <param name="EventType">
    /// Type of the event.
    /// </param>
    /// <param name="EventTask">
    /// The event task.
    /// </param>
    /// <param name="EventParam">
    /// The event parameter.
    /// </param>
    /// <param name="Original_ID">
    /// The original_ identifier.
    /// </param>
    /// <param name="Original_SubjectName">
    /// Name of the original_ subject.
    /// </param>
    /// <param name="Original_TrialSequence">
    /// The original_ trial sequence.
    /// </param>
    /// <param name="Original_EventID">
    /// The original_ event identifier.
    /// </param>
    /// <param name="Original_EventTime">
    /// The original_ event time.
    /// </param>
    /// <param name="Original_EventType">
    /// Type of the original_ event.
    /// </param>
    /// <param name="Original_EventTask">
    /// The original_ event task.
    /// </param>
    /// <param name="Original_EventParam">
    /// The original_ event parameter.
    /// </param>
    /// <returns>
    /// System.Int32.
    /// </returns>
    [DataObjectMethodAttribute(DataObjectMethodType.Update,
     true)]
    public virtual int Update(
      string SubjectName,
      int TrialSequence,
      int EventID,
      long EventTime,
      string EventType,
      string EventTask,
      string EventParam,
      int Original_ID,
      string Original_SubjectName,
      int Original_TrialSequence,
      int Original_EventID,
      long Original_EventTime,
      string Original_EventType,
      string Original_EventTask,
      string Original_EventParam)
    {
      return this.Update(
        SubjectName,
        TrialSequence,
        EventID,
        EventTime,
        EventType,
        EventTask,
        EventParam,
        Original_ID,
        Original_SubjectName,
        Original_TrialSequence,
        Original_EventID,
        Original_EventTime,
        Original_EventType,
        Original_EventTask,
        Original_EventParam,
        Original_ID);
    }

    /// <summary>
    /// Updates the event parameter by subject name trial sequence event identifier.
    /// </summary>
    /// <param name="EventParam">
    /// The event parameter.
    /// </param>
    /// <param name="Original_SubjectName">
    /// Name of the original_ subject.
    /// </param>
    /// <param name="Original_TrialSequence">
    /// The original_ trial sequence.
    /// </param>
    /// <param name="Original_EventID">
    /// The original_ event identifier.
    /// </param>
    /// <returns>
    /// System.Int32.
    /// </returns>
    /// <exception cref="System.ArgumentNullException">
    /// Original_SubjectName
    /// </exception>
    [DataObjectMethodAttribute(DataObjectMethodType.Update,
     false)]
    public virtual int UpdateEventParamBySubjectNameTrialSequenceEventID(
      string EventParam,
      string Original_SubjectName,
      int Original_TrialSequence,
      int Original_EventID)
    {
      SQLiteCommand command = this.CommandCollection[7];
      if (EventParam == null)
      {
        command.Parameters[0].Value = DBNull.Value;
      }
      else
      {
        command.Parameters[0].Value = EventParam;
      }

      if (Original_SubjectName == null)
      {
        throw new ArgumentNullException("Original_SubjectName");
      }

      command.Parameters[1].Value = Original_SubjectName;
      command.Parameters[2].Value = Original_TrialSequence;
      command.Parameters[3].Value = Original_EventID;
      ConnectionState previousConnectionState = command.Connection.State;
      if ((command.Connection.State & ConnectionState.Open)
           != ConnectionState.Open)
      {
        command.Connection.Open();
      }

      int returnValue;
      try
      {
        returnValue = command.ExecuteNonQuery();
      }
      finally
      {
        if (previousConnectionState == ConnectionState.Closed)
        {
          command.Connection.Close();
        }
      }

      return returnValue;
    }

    /// <summary>
    /// Updates the event time by subject sequence identifier.
    /// </summary>
    /// <param name="EventTime">
    /// The event time.
    /// </param>
    /// <param name="Original_SubjectName">
    /// Name of the original_ subject.
    /// </param>
    /// <param name="Original_TrialSequence">
    /// The original_ trial sequence.
    /// </param>
    /// <param name="Original_EventID">
    /// The original_ event identifier.
    /// </param>
    /// <returns>
    /// System.Int32.
    /// </returns>
    /// <exception cref="System.ArgumentNullException">
    /// Original_SubjectName
    /// </exception>
    [DataObjectMethodAttribute(DataObjectMethodType.Update,
     false)]
    public virtual int UpdateEventTimeBySubjectSequenceID(
      long EventTime,
      string Original_SubjectName,
      int Original_TrialSequence,
      int Original_EventID)
    {
      SQLiteCommand command = this.CommandCollection[8];
      command.Parameters[0].Value = EventTime;
      if (Original_SubjectName == null)
      {
        throw new ArgumentNullException("Original_SubjectName");
      }

      command.Parameters[1].Value = Original_SubjectName;
      command.Parameters[2].Value = Original_TrialSequence;
      command.Parameters[3].Value = Original_EventID;
      ConnectionState previousConnectionState = command.Connection.State;
      if ((command.Connection.State & ConnectionState.Open)
           != ConnectionState.Open)
      {
        command.Connection.Open();
      }

      int returnValue;
      try
      {
        returnValue = command.ExecuteNonQuery();
      }
      finally
      {
        if (previousConnectionState == ConnectionState.Closed)
        {
          command.Connection.Close();
        }
      }

      return returnValue;
    }

    #endregion

    #region Methods

    /// <summary>
    ///   Initializes the adapter.
    /// </summary>
    private void InitAdapter()
    {
      this._adapter = new SQLiteDataAdapter();
      var tableMapping = new DataTableMapping();
      tableMapping.SourceTable = "Table";
      tableMapping.DataSetTable = "TrialEvents";
      tableMapping.ColumnMappings.Add("ID", "ID");
      tableMapping.ColumnMappings.Add("SubjectName", "SubjectName");
      tableMapping.ColumnMappings.Add("TrialSequence", "TrialSequence");
      tableMapping.ColumnMappings.Add("EventID", "EventID");
      tableMapping.ColumnMappings.Add("EventTime", "EventTime");
      tableMapping.ColumnMappings.Add("EventType", "EventType");
      tableMapping.ColumnMappings.Add("EventTask", "EventTask");
      tableMapping.ColumnMappings.Add("EventParam", "EventParam");
      this._adapter.TableMappings.Add(tableMapping);
      this._adapter.DeleteCommand = new SQLiteCommand();
      this._adapter.DeleteCommand.Connection = this.Connection;
      this._adapter.DeleteCommand.CommandText =
        @"DELETE FROM [TrialEvents] WHERE (([ID] = @Original_ID) AND ([SubjectName] = @Original_SubjectName) AND ([TrialSequence] = @Original_TrialSequence) AND ([EventID] = @Original_EventID) AND ([EventTime] = @Original_EventTime) AND ([EventType] = @Original_EventType) AND ((@IsNull_EventTask = 1 AND [EventTask] IS NULL) OR ([EventTask] = @Original_EventTask)) AND ((@IsNull_EventParam = 1 AND [EventParam] IS NULL) OR ([EventParam] = @Original_EventParam)))";
      this._adapter.DeleteCommand.CommandType = CommandType.Text;
      this._adapter.DeleteCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_ID",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "ID",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.DeleteCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_SubjectName",
          DbType.String,
          0,
          ParameterDirection.Input,
          0,
          0,
          "SubjectName",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.DeleteCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_TrialSequence",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "TrialSequence",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.DeleteCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_EventID",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "EventID",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.DeleteCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_EventTime",
          DbType.Int64,
          0,
          ParameterDirection.Input,
          0,
          0,
          "EventTime",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.DeleteCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_EventType",
          DbType.String,
          0,
          ParameterDirection.Input,
          0,
          0,
          "EventType",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.DeleteCommand.Parameters.Add(
        new SQLiteParameter(
          "@IsNull_EventTask",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "EventTask",
          DataRowVersion.Original,
          true,
          null));
      this._adapter.DeleteCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_EventTask",
          DbType.String,
          0,
          ParameterDirection.Input,
          0,
          0,
          "EventTask",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.DeleteCommand.Parameters.Add(
        new SQLiteParameter(
          "@IsNull_EventParam",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "EventParam",
          DataRowVersion.Original,
          true,
          null));
      this._adapter.DeleteCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_EventParam",
          DbType.String,
          0,
          ParameterDirection.Input,
          0,
          0,
          "EventParam",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.InsertCommand = new SQLiteCommand();
      this._adapter.InsertCommand.Connection = this.Connection;
      this._adapter.InsertCommand.CommandText =
        @"INSERT INTO [TrialEvents] ([SubjectName], [TrialSequence], [EventID], [EventTime], [EventType], [EventTask], [EventParam]) VALUES (@SubjectName, @TrialSequence, @EventID, @EventTime, @EventType, @EventTask, @EventParam);
SELECT ID, SubjectName, TrialSequence, EventID, EventTime, EventType, EventTask, EventParam FROM TrialEvents WHERE (ID = last_insert_rowid())";
      this._adapter.InsertCommand.CommandType = CommandType.Text;
      this._adapter.InsertCommand.Parameters.Add(
        new SQLiteParameter(
          "@SubjectName",
          DbType.String,
          0,
          ParameterDirection.Input,
          0,
          0,
          "SubjectName",
          DataRowVersion.Current,
          false,
          null));
      this._adapter.InsertCommand.Parameters.Add(
        new SQLiteParameter(
          "@TrialSequence",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "TrialSequence",
          DataRowVersion.Current,
          false,
          null));
      this._adapter.InsertCommand.Parameters.Add(
        new SQLiteParameter(
          "@EventID",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "EventID",
          DataRowVersion.Current,
          false,
          null));
      this._adapter.InsertCommand.Parameters.Add(
        new SQLiteParameter(
          "@EventTime",
          DbType.Int64,
          0,
          ParameterDirection.Input,
          0,
          0,
          "EventTime",
          DataRowVersion.Current,
          false,
          null));
      this._adapter.InsertCommand.Parameters.Add(
        new SQLiteParameter(
          "@EventType",
          DbType.String,
          0,
          ParameterDirection.Input,
          0,
          0,
          "EventType",
          DataRowVersion.Current,
          false,
          null));
      this._adapter.InsertCommand.Parameters.Add(
        new SQLiteParameter(
          "@EventTask",
          DbType.String,
          0,
          ParameterDirection.Input,
          0,
          0,
          "EventTask",
          DataRowVersion.Current,
          false,
          null));
      this._adapter.InsertCommand.Parameters.Add(
        new SQLiteParameter(
          "@EventParam",
          DbType.String,
          0,
          ParameterDirection.Input,
          0,
          0,
          "EventParam",
          DataRowVersion.Current,
          false,
          null));
      this._adapter.UpdateCommand = new SQLiteCommand();
      this._adapter.UpdateCommand.Connection = this.Connection;
      this._adapter.UpdateCommand.CommandText =
        @"UPDATE [TrialEvents] SET [SubjectName] = @SubjectName, [TrialSequence] = @TrialSequence, [EventID] = @EventID, [EventTime] = @EventTime, [EventType] = @EventType, [EventTask] = @EventTask, [EventParam] = @EventParam WHERE (([ID] = @Original_ID) AND ([SubjectName] = @Original_SubjectName) AND ([TrialSequence] = @Original_TrialSequence) AND ([EventID] = @Original_EventID) AND ([EventTime] = @Original_EventTime) AND ([EventType] = @Original_EventType) AND ((@IsNull_EventTask = 1 AND [EventTask] IS NULL) OR ([EventTask] = @Original_EventTask)) AND ((@IsNull_EventParam = 1 AND [EventParam] IS NULL) OR ([EventParam] = @Original_EventParam)));
SELECT ID, SubjectName, TrialSequence, EventID, EventTime, EventType, EventTask, EventParam FROM TrialEvents WHERE (ID = @ID)";
      this._adapter.UpdateCommand.CommandType = CommandType.Text;
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@SubjectName",
          DbType.String,
          0,
          ParameterDirection.Input,
          0,
          0,
          "SubjectName",
          DataRowVersion.Current,
          false,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@TrialSequence",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "TrialSequence",
          DataRowVersion.Current,
          false,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@EventID",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "EventID",
          DataRowVersion.Current,
          false,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@EventTime",
          DbType.Int64,
          0,
          ParameterDirection.Input,
          0,
          0,
          "EventTime",
          DataRowVersion.Current,
          false,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@EventType",
          DbType.String,
          0,
          ParameterDirection.Input,
          0,
          0,
          "EventType",
          DataRowVersion.Current,
          false,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@EventTask",
          DbType.String,
          0,
          ParameterDirection.Input,
          0,
          0,
          "EventTask",
          DataRowVersion.Current,
          false,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@EventParam",
          DbType.String,
          0,
          ParameterDirection.Input,
          0,
          0,
          "EventParam",
          DataRowVersion.Current,
          false,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_ID",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "ID",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_SubjectName",
          DbType.String,
          0,
          ParameterDirection.Input,
          0,
          0,
          "SubjectName",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_TrialSequence",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "TrialSequence",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_EventID",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "EventID",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_EventTime",
          DbType.Int64,
          0,
          ParameterDirection.Input,
          0,
          0,
          "EventTime",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_EventType",
          DbType.String,
          0,
          ParameterDirection.Input,
          0,
          0,
          "EventType",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@IsNull_EventTask",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "EventTask",
          DataRowVersion.Original,
          true,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_EventTask",
          DbType.String,
          0,
          ParameterDirection.Input,
          0,
          0,
          "EventTask",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@IsNull_EventParam",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "EventParam",
          DataRowVersion.Original,
          true,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_EventParam",
          DbType.String,
          0,
          ParameterDirection.Input,
          0,
          0,
          "EventParam",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@ID",
          DbType.Int32,
          4,
          ParameterDirection.Input,
          0,
          0,
          "ID",
          DataRowVersion.Current,
          false,
          null));
    }

    /// <summary>
    ///   Initializes the command collection.
    /// </summary>
    private void InitCommandCollection()
    {
      this._commandCollection = new SQLiteCommand[9];
      this._commandCollection[0] = new SQLiteCommand();
      this._commandCollection[0].Connection = this.Connection;
      this._commandCollection[0].CommandText =
        "SELECT     ID, SubjectName, TrialSequence, EventID, EventTime, EventType, EventTa"
        + "sk, EventParam\r\nFROM         TrialEvents";
      this._commandCollection[0].CommandType = CommandType.Text;
      this._commandCollection[1] = new SQLiteCommand();
      this._commandCollection[1].Connection = this.Connection;
      this._commandCollection[1].CommandText = "DELETE FROM [TrialEvents] WHERE  (SubjectName = @Original_SubjectName)";
      this._commandCollection[1].CommandType = CommandType.Text;
      this._commandCollection[1].Parameters.Add(
        new SQLiteParameter(
          "@Original_SubjectName",
          DbType.String,
          50,
          ParameterDirection.Input,
          0,
          0,
          "SubjectName",
          DataRowVersion.Original,
          false,
          null));
      this._commandCollection[2] = new SQLiteCommand();
      this._commandCollection[2].Connection = this.Connection;
      this._commandCollection[2].CommandText =
        "DELETE FROM [TrialEvents] \r\nWHERE (([SubjectName] = @Original_SubjectName) AND (["
        + "TrialSequence] = @Original_TrialSequence) AND ([EventID] = @Original_EventID))";
      this._commandCollection[2].CommandType = CommandType.Text;
      this._commandCollection[2].Parameters.Add(
        new SQLiteParameter(
          "@Original_SubjectName",
          DbType.String,
          50,
          ParameterDirection.Input,
          0,
          0,
          "SubjectName",
          DataRowVersion.Original,
          false,
          null));
      this._commandCollection[2].Parameters.Add(
        new SQLiteParameter(
          "@Original_TrialSequence",
          DbType.Int32,
          4,
          ParameterDirection.Input,
          0,
          0,
          "TrialSequence",
          DataRowVersion.Original,
          false,
          null));
      this._commandCollection[2].Parameters.Add(
        new SQLiteParameter(
          "@Original_EventID",
          DbType.Int32,
          4,
          ParameterDirection.Input,
          0,
          0,
          "EventID",
          DataRowVersion.Original,
          false,
          null));
      this._commandCollection[3] = new SQLiteCommand();
      this._commandCollection[3].Connection = this.Connection;
      this._commandCollection[3].CommandText =
        "SELECT EventID, EventParam, EventTask, EventTime, EventType, ID, SubjectName, Tri"
        + "alSequence FROM TrialEvents WHERE (SubjectName = @Param1)";
      this._commandCollection[3].CommandType = CommandType.Text;
      this._commandCollection[3].Parameters.Add(
        new SQLiteParameter(
          "@Param1",
          DbType.String,
          50,
          ParameterDirection.Input,
          0,
          0,
          "SubjectName",
          DataRowVersion.Current,
          false,
          null));
      this._commandCollection[4] = new SQLiteCommand();
      this._commandCollection[4].Connection = this.Connection;
      this._commandCollection[4].CommandText =
        "SELECT EventID, EventParam, EventTask, EventTime, EventType, ID, SubjectName, Tri"
        + "alSequence FROM TrialEvents WHERE (SubjectName = @Param1) AND (TrialSequence = @" + "Param2)";
      this._commandCollection[4].CommandType = CommandType.Text;
      this._commandCollection[4].Parameters.Add(
        new SQLiteParameter(
          "@Param1",
          DbType.String,
          50,
          ParameterDirection.Input,
          0,
          0,
          "SubjectName",
          DataRowVersion.Current,
          false,
          null));
      this._commandCollection[4].Parameters.Add(
        new SQLiteParameter(
          "@Param2",
          DbType.Int32,
          4,
          ParameterDirection.Input,
          0,
          0,
          "TrialSequence",
          DataRowVersion.Current,
          false,
          null));
      this._commandCollection[5] = new SQLiteCommand();
      this._commandCollection[5].Connection = this.Connection;
      this._commandCollection[5].CommandText =
        "SELECT     ID, SubjectName, TrialSequence, EventID, EventTime, EventType, EventTa"
        + "sk, EventParam\r\nFROM         TrialEvents\r\nWHERE     (SubjectName = @Param1) AND "
        + "(TrialSequence = @Param2) AND (EventType = \'Response\') AND (EventTask = \'SlideCh" + "ange\')";
      this._commandCollection[5].CommandType = CommandType.Text;
      this._commandCollection[5].Parameters.Add(
        new SQLiteParameter(
          "@Param1",
          DbType.String,
          50,
          ParameterDirection.Input,
          0,
          0,
          "SubjectName",
          DataRowVersion.Current,
          false,
          null));
      this._commandCollection[5].Parameters.Add(
        new SQLiteParameter(
          "@Param2",
          DbType.Int32,
          4,
          ParameterDirection.Input,
          0,
          0,
          "TrialSequence",
          DataRowVersion.Current,
          false,
          null));
      this._commandCollection[6] = new SQLiteCommand();
      this._commandCollection[6].Connection = this.Connection;
      this._commandCollection[6].CommandText = "SELECT     MAX(EventID) AS Expr1\r\nFROM         TrialEvents";
      this._commandCollection[6].CommandType = CommandType.Text;
      this._commandCollection[7] = new SQLiteCommand();
      this._commandCollection[7].Connection = this.Connection;
      this._commandCollection[7].CommandText =
        "UPDATE    TrialEvents\r\nSET              EventParam = @EventParam\r\nWHERE     (Subj"
        + "ectName = @Original_SubjectName) AND (TrialSequence = @Original_TrialSequence) A"
        + "ND (EventID = @Original_EventID)";
      this._commandCollection[7].CommandType = CommandType.Text;
      this._commandCollection[7].Parameters.Add(
        new SQLiteParameter(
          "@EventParam",
          DbType.String,
          4000,
          ParameterDirection.Input,
          0,
          0,
          "EventParam",
          DataRowVersion.Current,
          false,
          null));
      this._commandCollection[7].Parameters.Add(
        new SQLiteParameter(
          "@Original_SubjectName",
          DbType.String,
          50,
          ParameterDirection.Input,
          0,
          0,
          "SubjectName",
          DataRowVersion.Original,
          false,
          null));
      this._commandCollection[7].Parameters.Add(
        new SQLiteParameter(
          "@Original_TrialSequence",
          DbType.Int32,
          4,
          ParameterDirection.Input,
          0,
          0,
          "TrialSequence",
          DataRowVersion.Original,
          false,
          null));
      this._commandCollection[7].Parameters.Add(
        new SQLiteParameter(
          "@Original_EventID",
          DbType.Int32,
          4,
          ParameterDirection.Input,
          0,
          0,
          "EventID",
          DataRowVersion.Original,
          false,
          null));
      this._commandCollection[8] = new SQLiteCommand();
      this._commandCollection[8].Connection = this.Connection;
      this._commandCollection[8].CommandText =
        "UPDATE    TrialEvents\r\nSET           EventTime = @EventTime\r\nWHERE     (SubjectNa"
        + "me = @Original_SubjectName) AND (TrialSequence = @Original_TrialSequence) AND (E"
        + "ventID = @Original_EventID)";
      this._commandCollection[8].CommandType = CommandType.Text;
      this._commandCollection[8].Parameters.Add(
        new SQLiteParameter(
          "@EventTime",
          DbType.Int64,
          8,
          ParameterDirection.Input,
          0,
          0,
          "EventTime",
          DataRowVersion.Current,
          false,
          null));
      this._commandCollection[8].Parameters.Add(
        new SQLiteParameter(
          "@Original_SubjectName",
          DbType.String,
          50,
          ParameterDirection.Input,
          0,
          0,
          "SubjectName",
          DataRowVersion.Original,
          false,
          null));
      this._commandCollection[8].Parameters.Add(
        new SQLiteParameter(
          "@Original_TrialSequence",
          DbType.Int32,
          4,
          ParameterDirection.Input,
          0,
          0,
          "TrialSequence",
          DataRowVersion.Original,
          false,
          null));
      this._commandCollection[8].Parameters.Add(
        new SQLiteParameter(
          "@Original_EventID",
          DbType.Int32,
          4,
          ParameterDirection.Input,
          0,
          0,
          "EventID",
          DataRowVersion.Original,
          false,
          null));
    }

    /// <summary>
    ///   Initializes the connection.
    /// </summary>
    private void InitConnection()
    {
      this._connection = new SQLiteConnection();
      this._connection.ConnectionString =
        global::Ogama.Properties.Settings.Default.OgamaDatabaseTemplateConnectionString;
    }

    #endregion
  }

  /// <summary>
  ///   Represents the connection and commands used to retrieve and save data.
  /// </summary>
  public class SQLiteTadRawdata : Component
  {
    #region Fields

    /// <summary>
    ///   The _adapter
    /// </summary>
    private SQLiteDataAdapter _adapter;

    /// <summary>
    ///   The _command collection
    /// </summary>
    private SQLiteCommand[] _commandCollection;

    /// <summary>
    ///   The _connection
    /// </summary>
    private SQLiteConnection _connection;

    #endregion

    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="SQLiteTadRawdata" /> class.
    /// </summary>
    public SQLiteTadRawdata()
    {
      this.ClearBeforeFill = true;
    }

    #endregion

    #region Public Properties

    /// <summary>
    ///   Gets or sets a value indicating whether [clear before fill].
    /// </summary>
    /// <value><c>true</c> if [clear before fill]; otherwise, <c>false</c>.</value>
    public bool ClearBeforeFill { get; set; }

    #endregion

    #region Properties

    /// <summary>
    ///   Gets or sets the connection.
    /// </summary>
    /// <value>The connection.</value>
    internal SQLiteConnection Connection
    {
      get
      {
        if (this._connection == null)
        {
          this.InitConnection();
        }

        return this._connection;
      }

      set
      {
        this._connection = value;
        if (this.Adapter.InsertCommand != null)
        {
          this.Adapter.InsertCommand.Connection = value;
        }

        if (this.Adapter.DeleteCommand != null)
        {
          this.Adapter.DeleteCommand.Connection = value;
        }

        if (this.Adapter.UpdateCommand != null)
        {
          this.Adapter.UpdateCommand.Connection = value;
        }

        for (int i = 0; i < this.CommandCollection.Length; i = (i + 1))
        {
          if ((this.CommandCollection[i] != null))
          {
            this.CommandCollection[i].Connection = value;
          }
        }
      }
    }

    /// <summary>
    ///   Gets the command collection.
    /// </summary>
    /// <value>The command collection.</value>
    protected SQLiteCommand[] CommandCollection
    {
      get
      {
        if (this._commandCollection == null)
        {
          this.InitCommandCollection();
        }

        return this._commandCollection;
      }
    }

    /// <summary>
    ///   Gets the adapter.
    /// </summary>
    /// <value>The adapter.</value>
    private SQLiteDataAdapter Adapter
    {
      get
      {
        if (this._adapter == null)
        {
          this.InitAdapter();
        }

        return this._adapter;
      }
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// Deletes the specified original_ identifier.
    /// </summary>
    /// <param name="Original_ID">
    /// The original_ identifier.
    /// </param>
    /// <param name="Original_SubjectName">
    /// Name of the original_ subject.
    /// </param>
    /// <param name="Original_TrialSequence">
    /// The original_ trial sequence.
    /// </param>
    /// <param name="Original_Time">
    /// The original_ time.
    /// </param>
    /// <param name="Original_PupilDiaX">
    /// The original_ pupil dia x.
    /// </param>
    /// <param name="Original_PupilDiaY">
    /// The original_ pupil dia y.
    /// </param>
    /// <param name="Original_GazePosX">
    /// The original_ gaze position x.
    /// </param>
    /// <param name="Original_GazePosY">
    /// The original_ gaze position y.
    /// </param>
    /// <param name="Original_MousePosX">
    /// The original_ mouse position x.
    /// </param>
    /// <param name="Original_MousePosY">
    /// The original_ mouse position y.
    /// </param>
    /// <param name="Original_EventID">
    /// The original_ event identifier.
    /// </param>
    /// <returns>
    /// System.Int32.
    /// </returns>
    /// <exception cref="System.ArgumentNullException">
    /// Original_SubjectName
    /// </exception>
    [DataObjectMethodAttribute(DataObjectMethodType.Delete,
     true)]
    public virtual int Delete(
      long Original_ID,
      string Original_SubjectName,
      int Original_TrialSequence,
      long Original_Time,
      double? Original_PupilDiaX,
      double? Original_PupilDiaY,
      double? Original_GazePosX,
      double? Original_GazePosY,
      double? Original_MousePosX,
      double? Original_MousePosY,
      int? Original_EventID)
    {
      this.Adapter.DeleteCommand.Parameters[0].Value = Original_ID;
      if (Original_SubjectName == null)
      {
        throw new ArgumentNullException("Original_SubjectName");
      }

      this.Adapter.DeleteCommand.Parameters[1].Value = Original_SubjectName;
      this.Adapter.DeleteCommand.Parameters[2].Value = Original_TrialSequence;
      this.Adapter.DeleteCommand.Parameters[3].Value = Original_Time;
      if (Original_PupilDiaX.HasValue)
      {
        this.Adapter.DeleteCommand.Parameters[4].Value = 0;
        this.Adapter.DeleteCommand.Parameters[5].Value = Original_PupilDiaX.Value;
      }
      else
      {
        this.Adapter.DeleteCommand.Parameters[4].Value = 1;
        this.Adapter.DeleteCommand.Parameters[5].Value = DBNull.Value;
      }

      if (Original_PupilDiaY.HasValue)
      {
        this.Adapter.DeleteCommand.Parameters[6].Value = 0;
        this.Adapter.DeleteCommand.Parameters[7].Value = Original_PupilDiaY.Value;
      }
      else
      {
        this.Adapter.DeleteCommand.Parameters[6].Value = 1;
        this.Adapter.DeleteCommand.Parameters[7].Value = DBNull.Value;
      }

      if (Original_GazePosX.HasValue)
      {
        this.Adapter.DeleteCommand.Parameters[8].Value = 0;
        this.Adapter.DeleteCommand.Parameters[9].Value = Original_GazePosX.Value;
      }
      else
      {
        this.Adapter.DeleteCommand.Parameters[8].Value = 1;
        this.Adapter.DeleteCommand.Parameters[9].Value = DBNull.Value;
      }

      if (Original_GazePosY.HasValue)
      {
        this.Adapter.DeleteCommand.Parameters[10].Value = 0;
        this.Adapter.DeleteCommand.Parameters[11].Value = Original_GazePosY.Value;
      }
      else
      {
        this.Adapter.DeleteCommand.Parameters[10].Value = 1;
        this.Adapter.DeleteCommand.Parameters[11].Value = DBNull.Value;
      }

      if (Original_MousePosX.HasValue)
      {
        this.Adapter.DeleteCommand.Parameters[12].Value = 0;
        this.Adapter.DeleteCommand.Parameters[13].Value = Original_MousePosX.Value;
      }
      else
      {
        this.Adapter.DeleteCommand.Parameters[12].Value = 1;
        this.Adapter.DeleteCommand.Parameters[13].Value = DBNull.Value;
      }

      if (Original_MousePosY.HasValue)
      {
        this.Adapter.DeleteCommand.Parameters[14].Value = 0;
        this.Adapter.DeleteCommand.Parameters[15].Value = Original_MousePosY.Value;
      }
      else
      {
        this.Adapter.DeleteCommand.Parameters[14].Value = 1;
        this.Adapter.DeleteCommand.Parameters[15].Value = DBNull.Value;
      }

      if (Original_EventID.HasValue)
      {
        this.Adapter.DeleteCommand.Parameters[16].Value = 0;
        this.Adapter.DeleteCommand.Parameters[17].Value = Original_EventID.Value;
      }
      else
      {
        this.Adapter.DeleteCommand.Parameters[16].Value = 1;
        this.Adapter.DeleteCommand.Parameters[17].Value = DBNull.Value;
      }

      ConnectionState previousConnectionState = this.Adapter.DeleteCommand.Connection.State;
      if ((this.Adapter.DeleteCommand.Connection.State & ConnectionState.Open)
           != ConnectionState.Open)
      {
        this.Adapter.DeleteCommand.Connection.Open();
      }

      try
      {
        int returnValue = this.Adapter.DeleteCommand.ExecuteNonQuery();
        return returnValue;
      }
      finally
      {
        if (previousConnectionState == ConnectionState.Closed)
        {
          this.Adapter.DeleteCommand.Connection.Close();
        }
      }
    }

    /// <summary>
    /// Fills the specified data table.
    /// </summary>
    /// <param name="dataTable">
    /// The data table.
    /// </param>
    /// <returns>
    /// System.Int32.
    /// </returns>
    [DataObjectMethodAttribute(DataObjectMethodType.Fill,
     true)]
    public virtual int Fill(SQLiteOgamaDataSet.RawdataDataTable dataTable)
    {
      this.Adapter.SelectCommand = this.CommandCollection[0];
      if (this.ClearBeforeFill)
      {
        dataTable.Clear();
      }

      int returnValue = this.Adapter.Fill(dataTable);
      return returnValue;
    }

    /// <summary>
    ///   Gets the data.
    /// </summary>
    /// <returns>SQLiteOgamaDataSet.RawdataDataTable.</returns>
    [DataObjectMethodAttribute(DataObjectMethodType.Select,
     true)]
    public virtual SQLiteOgamaDataSet.RawdataDataTable GetData()
    {
      this.Adapter.SelectCommand = this.CommandCollection[0];
      var dataTable = new SQLiteOgamaDataSet.RawdataDataTable();
      this.Adapter.Fill(dataTable);
      return dataTable;
    }

    /// <summary>
    /// Inserts the specified subject name.
    /// </summary>
    /// <param name="SubjectName">
    /// Name of the subject.
    /// </param>
    /// <param name="TrialSequence">
    /// The trial sequence.
    /// </param>
    /// <param name="Time">
    /// The time.
    /// </param>
    /// <param name="PupilDiaX">
    /// The pupil dia x.
    /// </param>
    /// <param name="PupilDiaY">
    /// The pupil dia y.
    /// </param>
    /// <param name="GazePosX">
    /// The gaze position x.
    /// </param>
    /// <param name="GazePosY">
    /// The gaze position y.
    /// </param>
    /// <param name="MousePosX">
    /// The mouse position x.
    /// </param>
    /// <param name="MousePosY">
    /// The mouse position y.
    /// </param>
    /// <param name="EventID">
    /// The event identifier.
    /// </param>
    /// <returns>
    /// System.Int32.
    /// </returns>
    /// <exception cref="System.ArgumentNullException">
    /// SubjectName
    /// </exception>
    [DataObjectMethodAttribute(DataObjectMethodType.Insert,
     true)]
    public virtual int Insert(
      string SubjectName,
      int TrialSequence,
      long Time,
      double? PupilDiaX,
      double? PupilDiaY,
      double? GazePosX,
      double? GazePosY,
      double? MousePosX,
      double? MousePosY,
      int? EventID)
    {
      if (SubjectName == null)
      {
        throw new ArgumentNullException("SubjectName");
      }

      this.Adapter.InsertCommand.Parameters[0].Value = SubjectName;
      this.Adapter.InsertCommand.Parameters[1].Value = TrialSequence;
      this.Adapter.InsertCommand.Parameters[2].Value = Time;
      if (PupilDiaX.HasValue)
      {
        this.Adapter.InsertCommand.Parameters[3].Value = PupilDiaX.Value;
      }
      else
      {
        this.Adapter.InsertCommand.Parameters[3].Value = DBNull.Value;
      }

      if (PupilDiaY.HasValue)
      {
        this.Adapter.InsertCommand.Parameters[4].Value = PupilDiaY.Value;
      }
      else
      {
        this.Adapter.InsertCommand.Parameters[4].Value = DBNull.Value;
      }

      if (GazePosX.HasValue)
      {
        this.Adapter.InsertCommand.Parameters[5].Value = GazePosX.Value;
      }
      else
      {
        this.Adapter.InsertCommand.Parameters[5].Value = DBNull.Value;
      }

      if (GazePosY.HasValue)
      {
        this.Adapter.InsertCommand.Parameters[6].Value = GazePosY.Value;
      }
      else
      {
        this.Adapter.InsertCommand.Parameters[6].Value = DBNull.Value;
      }

      if (MousePosX.HasValue)
      {
        this.Adapter.InsertCommand.Parameters[7].Value = MousePosX.Value;
      }
      else
      {
        this.Adapter.InsertCommand.Parameters[7].Value = DBNull.Value;
      }

      if (MousePosY.HasValue)
      {
        this.Adapter.InsertCommand.Parameters[8].Value = MousePosY.Value;
      }
      else
      {
        this.Adapter.InsertCommand.Parameters[8].Value = DBNull.Value;
      }

      if (EventID.HasValue)
      {
        this.Adapter.InsertCommand.Parameters[9].Value = EventID.Value;
      }
      else
      {
        this.Adapter.InsertCommand.Parameters[9].Value = DBNull.Value;
      }

      ConnectionState previousConnectionState = this.Adapter.InsertCommand.Connection.State;
      if ((this.Adapter.InsertCommand.Connection.State & ConnectionState.Open)
           != ConnectionState.Open)
      {
        this.Adapter.InsertCommand.Connection.Open();
      }

      try
      {
        int returnValue = this.Adapter.InsertCommand.ExecuteNonQuery();
        return returnValue;
      }
      finally
      {
        if (previousConnectionState == ConnectionState.Closed)
        {
          this.Adapter.InsertCommand.Connection.Close();
        }
      }
    }

    /// <summary>
    /// Updates the specified data table.
    /// </summary>
    /// <param name="dataTable">
    /// The data table.
    /// </param>
    /// <returns>
    /// System.Int32.
    /// </returns>
    public virtual int Update(SQLiteOgamaDataSet.RawdataDataTable dataTable)
    {
      return this.Adapter.Update(dataTable);
    }

    /// <summary>
    /// Updates the specified data set.
    /// </summary>
    /// <param name="dataSet">
    /// The data set.
    /// </param>
    /// <returns>
    /// System.Int32.
    /// </returns>
    public virtual int Update(SQLiteOgamaDataSet dataSet)
    {
      return this.Adapter.Update(dataSet, "Rawdata");
    }

    /// <summary>
    /// Updates the specified data row.
    /// </summary>
    /// <param name="dataRow">
    /// The data row.
    /// </param>
    /// <returns>
    /// System.Int32.
    /// </returns>
    public virtual int Update(DataRow dataRow)
    {
      return this.Adapter.Update(new[] { dataRow });
    }

    /// <summary>
    /// Updates the specified data rows.
    /// </summary>
    /// <param name="dataRows">
    /// The data rows.
    /// </param>
    /// <returns>
    /// System.Int32.
    /// </returns>
    public virtual int Update(DataRow[] dataRows)
    {
      return this.Adapter.Update(dataRows);
    }

    /// <summary>
    /// Updates the specified subject name.
    /// </summary>
    /// <param name="SubjectName">
    /// Name of the subject.
    /// </param>
    /// <param name="TrialSequence">
    /// The trial sequence.
    /// </param>
    /// <param name="Time">
    /// The time.
    /// </param>
    /// <param name="PupilDiaX">
    /// The pupil dia x.
    /// </param>
    /// <param name="PupilDiaY">
    /// The pupil dia y.
    /// </param>
    /// <param name="GazePosX">
    /// The gaze position x.
    /// </param>
    /// <param name="GazePosY">
    /// The gaze position y.
    /// </param>
    /// <param name="MousePosX">
    /// The mouse position x.
    /// </param>
    /// <param name="MousePosY">
    /// The mouse position y.
    /// </param>
    /// <param name="EventID">
    /// The event identifier.
    /// </param>
    /// <param name="Original_ID">
    /// The original_ identifier.
    /// </param>
    /// <param name="Original_SubjectName">
    /// Name of the original_ subject.
    /// </param>
    /// <param name="Original_TrialSequence">
    /// The original_ trial sequence.
    /// </param>
    /// <param name="Original_Time">
    /// The original_ time.
    /// </param>
    /// <param name="Original_PupilDiaX">
    /// The original_ pupil dia x.
    /// </param>
    /// <param name="Original_PupilDiaY">
    /// The original_ pupil dia y.
    /// </param>
    /// <param name="Original_GazePosX">
    /// The original_ gaze position x.
    /// </param>
    /// <param name="Original_GazePosY">
    /// The original_ gaze position y.
    /// </param>
    /// <param name="Original_MousePosX">
    /// The original_ mouse position x.
    /// </param>
    /// <param name="Original_MousePosY">
    /// The original_ mouse position y.
    /// </param>
    /// <param name="Original_EventID">
    /// The original_ event identifier.
    /// </param>
    /// <param name="ID">
    /// The identifier.
    /// </param>
    /// <returns>
    /// System.Int32.
    /// </returns>
    /// <exception cref="System.ArgumentNullException">
    /// SubjectName
    ///   or
    ///   Original_SubjectName
    /// </exception>
    [DataObjectMethodAttribute(DataObjectMethodType.Update,
     true)]
    public virtual int Update(
      string SubjectName,
      int TrialSequence,
      long Time,
      double? PupilDiaX,
      double? PupilDiaY,
      double? GazePosX,
      double? GazePosY,
      double? MousePosX,
      double? MousePosY,
      int? EventID,
      long Original_ID,
      string Original_SubjectName,
      int Original_TrialSequence,
      long Original_Time,
      double? Original_PupilDiaX,
      double? Original_PupilDiaY,
      double? Original_GazePosX,
      double? Original_GazePosY,
      double? Original_MousePosX,
      double? Original_MousePosY,
      int? Original_EventID,
      long ID)
    {
      if (SubjectName == null)
      {
        throw new ArgumentNullException("SubjectName");
      }

      this.Adapter.UpdateCommand.Parameters[0].Value = SubjectName;
      this.Adapter.UpdateCommand.Parameters[1].Value = TrialSequence;
      this.Adapter.UpdateCommand.Parameters[2].Value = Time;
      if (PupilDiaX.HasValue)
      {
        this.Adapter.UpdateCommand.Parameters[3].Value = PupilDiaX.Value;
      }
      else
      {
        this.Adapter.UpdateCommand.Parameters[3].Value = DBNull.Value;
      }

      if (PupilDiaY.HasValue)
      {
        this.Adapter.UpdateCommand.Parameters[4].Value = PupilDiaY.Value;
      }
      else
      {
        this.Adapter.UpdateCommand.Parameters[4].Value = DBNull.Value;
      }

      if (GazePosX.HasValue)
      {
        this.Adapter.UpdateCommand.Parameters[5].Value = GazePosX.Value;
      }
      else
      {
        this.Adapter.UpdateCommand.Parameters[5].Value = DBNull.Value;
      }

      if (GazePosY.HasValue)
      {
        this.Adapter.UpdateCommand.Parameters[6].Value = GazePosY.Value;
      }
      else
      {
        this.Adapter.UpdateCommand.Parameters[6].Value = DBNull.Value;
      }

      if (MousePosX.HasValue)
      {
        this.Adapter.UpdateCommand.Parameters[7].Value = MousePosX.Value;
      }
      else
      {
        this.Adapter.UpdateCommand.Parameters[7].Value = DBNull.Value;
      }

      if (MousePosY.HasValue)
      {
        this.Adapter.UpdateCommand.Parameters[8].Value = MousePosY.Value;
      }
      else
      {
        this.Adapter.UpdateCommand.Parameters[8].Value = DBNull.Value;
      }

      if (EventID.HasValue)
      {
        this.Adapter.UpdateCommand.Parameters[9].Value = EventID.Value;
      }
      else
      {
        this.Adapter.UpdateCommand.Parameters[9].Value = DBNull.Value;
      }

      this.Adapter.UpdateCommand.Parameters[10].Value = Original_ID;
      if (Original_SubjectName == null)
      {
        throw new ArgumentNullException("Original_SubjectName");
      }

      this.Adapter.UpdateCommand.Parameters[11].Value = Original_SubjectName;
      this.Adapter.UpdateCommand.Parameters[12].Value = Original_TrialSequence;
      this.Adapter.UpdateCommand.Parameters[13].Value = Original_Time;
      if (Original_PupilDiaX.HasValue)
      {
        this.Adapter.UpdateCommand.Parameters[14].Value = 0;
        this.Adapter.UpdateCommand.Parameters[15].Value = Original_PupilDiaX.Value;
      }
      else
      {
        this.Adapter.UpdateCommand.Parameters[14].Value = 1;
        this.Adapter.UpdateCommand.Parameters[15].Value = DBNull.Value;
      }

      if (Original_PupilDiaY.HasValue)
      {
        this.Adapter.UpdateCommand.Parameters[16].Value = 0;
        this.Adapter.UpdateCommand.Parameters[17].Value = Original_PupilDiaY.Value;
      }
      else
      {
        this.Adapter.UpdateCommand.Parameters[16].Value = 1;
        this.Adapter.UpdateCommand.Parameters[17].Value = DBNull.Value;
      }

      if (Original_GazePosX.HasValue)
      {
        this.Adapter.UpdateCommand.Parameters[18].Value = 0;
        this.Adapter.UpdateCommand.Parameters[19].Value = Original_GazePosX.Value;
      }
      else
      {
        this.Adapter.UpdateCommand.Parameters[18].Value = 1;
        this.Adapter.UpdateCommand.Parameters[19].Value = DBNull.Value;
      }

      if (Original_GazePosY.HasValue)
      {
        this.Adapter.UpdateCommand.Parameters[20].Value = 0;
        this.Adapter.UpdateCommand.Parameters[21].Value = Original_GazePosY.Value;
      }
      else
      {
        this.Adapter.UpdateCommand.Parameters[20].Value = 1;
        this.Adapter.UpdateCommand.Parameters[21].Value = DBNull.Value;
      }

      if (Original_MousePosX.HasValue)
      {
        this.Adapter.UpdateCommand.Parameters[22].Value = 0;
        this.Adapter.UpdateCommand.Parameters[23].Value = Original_MousePosX.Value;
      }
      else
      {
        this.Adapter.UpdateCommand.Parameters[22].Value = 1;
        this.Adapter.UpdateCommand.Parameters[23].Value = DBNull.Value;
      }

      if (Original_MousePosY.HasValue)
      {
        this.Adapter.UpdateCommand.Parameters[24].Value = 0;
        this.Adapter.UpdateCommand.Parameters[25].Value = Original_MousePosY.Value;
      }
      else
      {
        this.Adapter.UpdateCommand.Parameters[24].Value = 1;
        this.Adapter.UpdateCommand.Parameters[25].Value = DBNull.Value;
      }

      if (Original_EventID.HasValue)
      {
        this.Adapter.UpdateCommand.Parameters[26].Value = 0;
        this.Adapter.UpdateCommand.Parameters[27].Value = Original_EventID.Value;
      }
      else
      {
        this.Adapter.UpdateCommand.Parameters[26].Value = 1;
        this.Adapter.UpdateCommand.Parameters[27].Value = DBNull.Value;
      }

      this.Adapter.UpdateCommand.Parameters[28].Value = ID;
      ConnectionState previousConnectionState = this.Adapter.UpdateCommand.Connection.State;
      if ((this.Adapter.UpdateCommand.Connection.State & ConnectionState.Open)
           != ConnectionState.Open)
      {
        this.Adapter.UpdateCommand.Connection.Open();
      }

      try
      {
        int returnValue = this.Adapter.UpdateCommand.ExecuteNonQuery();
        return returnValue;
      }
      finally
      {
        if (previousConnectionState == ConnectionState.Closed)
        {
          this.Adapter.UpdateCommand.Connection.Close();
        }
      }
    }

    /// <summary>
    /// Updates the specified subject name.
    /// </summary>
    /// <param name="SubjectName">
    /// Name of the subject.
    /// </param>
    /// <param name="TrialSequence">
    /// The trial sequence.
    /// </param>
    /// <param name="Time">
    /// The time.
    /// </param>
    /// <param name="PupilDiaX">
    /// The pupil dia x.
    /// </param>
    /// <param name="PupilDiaY">
    /// The pupil dia y.
    /// </param>
    /// <param name="GazePosX">
    /// The gaze position x.
    /// </param>
    /// <param name="GazePosY">
    /// The gaze position y.
    /// </param>
    /// <param name="MousePosX">
    /// The mouse position x.
    /// </param>
    /// <param name="MousePosY">
    /// The mouse position y.
    /// </param>
    /// <param name="EventID">
    /// The event identifier.
    /// </param>
    /// <param name="Original_ID">
    /// The original_ identifier.
    /// </param>
    /// <param name="Original_SubjectName">
    /// Name of the original_ subject.
    /// </param>
    /// <param name="Original_TrialSequence">
    /// The original_ trial sequence.
    /// </param>
    /// <param name="Original_Time">
    /// The original_ time.
    /// </param>
    /// <param name="Original_PupilDiaX">
    /// The original_ pupil dia x.
    /// </param>
    /// <param name="Original_PupilDiaY">
    /// The original_ pupil dia y.
    /// </param>
    /// <param name="Original_GazePosX">
    /// The original_ gaze position x.
    /// </param>
    /// <param name="Original_GazePosY">
    /// The original_ gaze position y.
    /// </param>
    /// <param name="Original_MousePosX">
    /// The original_ mouse position x.
    /// </param>
    /// <param name="Original_MousePosY">
    /// The original_ mouse position y.
    /// </param>
    /// <param name="Original_EventID">
    /// The original_ event identifier.
    /// </param>
    /// <returns>
    /// System.Int32.
    /// </returns>
    [DataObjectMethodAttribute(DataObjectMethodType.Update,
     true)]
    public virtual int Update(
      string SubjectName,
      int TrialSequence,
      long Time,
      double? PupilDiaX,
      double? PupilDiaY,
      double? GazePosX,
      double? GazePosY,
      double? MousePosX,
      double? MousePosY,
      int? EventID,
      long Original_ID,
      string Original_SubjectName,
      int Original_TrialSequence,
      long Original_Time,
      double? Original_PupilDiaX,
      double? Original_PupilDiaY,
      double? Original_GazePosX,
      double? Original_GazePosY,
      double? Original_MousePosX,
      double? Original_MousePosY,
      int? Original_EventID)
    {
      return this.Update(
        SubjectName,
        TrialSequence,
        Time,
        PupilDiaX,
        PupilDiaY,
        GazePosX,
        GazePosY,
        MousePosX,
        MousePosY,
        EventID,
        Original_ID,
        Original_SubjectName,
        Original_TrialSequence,
        Original_Time,
        Original_PupilDiaX,
        Original_PupilDiaY,
        Original_GazePosX,
        Original_GazePosY,
        Original_MousePosX,
        Original_MousePosY,
        Original_EventID,
        Original_ID);
    }

    #endregion

    #region Methods

    /// <summary>
    ///   Initializes the adapter.
    /// </summary>
    private void InitAdapter()
    {
      this._adapter = new SQLiteDataAdapter();
      var tableMapping = new DataTableMapping();
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
      this._adapter.TableMappings.Add(tableMapping);
      this._adapter.DeleteCommand = new SQLiteCommand();
      this._adapter.DeleteCommand.Connection = this.Connection;
      this._adapter.DeleteCommand.CommandText =
        @"DELETE FROM [Rawdata] WHERE (([ID] = @Original_ID) AND ([SubjectName] = @Original_SubjectName) AND ([TrialSequence] = @Original_TrialSequence) AND ([Time] = @Original_Time) AND ((@IsNull_PupilDiaX = 1 AND [PupilDiaX] IS NULL) OR ([PupilDiaX] = @Original_PupilDiaX)) AND ((@IsNull_PupilDiaY = 1 AND [PupilDiaY] IS NULL) OR ([PupilDiaY] = @Original_PupilDiaY)) AND ((@IsNull_GazePosX = 1 AND [GazePosX] IS NULL) OR ([GazePosX] = @Original_GazePosX)) AND ((@IsNull_GazePosY = 1 AND [GazePosY] IS NULL) OR ([GazePosY] = @Original_GazePosY)) AND ((@IsNull_MousePosX = 1 AND [MousePosX] IS NULL) OR ([MousePosX] = @Original_MousePosX)) AND ((@IsNull_MousePosY = 1 AND [MousePosY] IS NULL) OR ([MousePosY] = @Original_MousePosY)) AND ((@IsNull_EventID = 1 AND [EventID] IS NULL) OR ([EventID] = @Original_EventID)))";
      this._adapter.DeleteCommand.CommandType = CommandType.Text;
      this._adapter.DeleteCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_ID",
          DbType.Int64,
          0,
          ParameterDirection.Input,
          0,
          0,
          "ID",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.DeleteCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_SubjectName",
          DbType.String,
          0,
          ParameterDirection.Input,
          0,
          0,
          "SubjectName",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.DeleteCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_TrialSequence",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "TrialSequence",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.DeleteCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_Time",
          DbType.Int64,
          0,
          ParameterDirection.Input,
          0,
          0,
          "Time",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.DeleteCommand.Parameters.Add(
        new SQLiteParameter(
          "@IsNull_PupilDiaX",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "PupilDiaX",
          DataRowVersion.Original,
          true,
          null));
      this._adapter.DeleteCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_PupilDiaX",
          DbType.Single,
          0,
          ParameterDirection.Input,
          0,
          0,
          "PupilDiaX",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.DeleteCommand.Parameters.Add(
        new SQLiteParameter(
          "@IsNull_PupilDiaY",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "PupilDiaY",
          DataRowVersion.Original,
          true,
          null));
      this._adapter.DeleteCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_PupilDiaY",
          DbType.Single,
          0,
          ParameterDirection.Input,
          0,
          0,
          "PupilDiaY",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.DeleteCommand.Parameters.Add(
        new SQLiteParameter(
          "@IsNull_GazePosX",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "GazePosX",
          DataRowVersion.Original,
          true,
          null));
      this._adapter.DeleteCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_GazePosX",
          DbType.Single,
          0,
          ParameterDirection.Input,
          0,
          0,
          "GazePosX",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.DeleteCommand.Parameters.Add(
        new SQLiteParameter(
          "@IsNull_GazePosY",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "GazePosY",
          DataRowVersion.Original,
          true,
          null));
      this._adapter.DeleteCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_GazePosY",
          DbType.Single,
          0,
          ParameterDirection.Input,
          0,
          0,
          "GazePosY",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.DeleteCommand.Parameters.Add(
        new SQLiteParameter(
          "@IsNull_MousePosX",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "MousePosX",
          DataRowVersion.Original,
          true,
          null));
      this._adapter.DeleteCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_MousePosX",
          DbType.Single,
          0,
          ParameterDirection.Input,
          0,
          0,
          "MousePosX",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.DeleteCommand.Parameters.Add(
        new SQLiteParameter(
          "@IsNull_MousePosY",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "MousePosY",
          DataRowVersion.Original,
          true,
          null));
      this._adapter.DeleteCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_MousePosY",
          DbType.Single,
          0,
          ParameterDirection.Input,
          0,
          0,
          "MousePosY",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.DeleteCommand.Parameters.Add(
        new SQLiteParameter(
          "@IsNull_EventID",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "EventID",
          DataRowVersion.Original,
          true,
          null));
      this._adapter.DeleteCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_EventID",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "EventID",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.InsertCommand = new SQLiteCommand();
      this._adapter.InsertCommand.Connection = this.Connection;
      this._adapter.InsertCommand.CommandText =
        @"INSERT INTO [Rawdata] ([SubjectName], [TrialSequence], [Time], [PupilDiaX], [PupilDiaY], [GazePosX], [GazePosY], [MousePosX], [MousePosY], [EventID]) VALUES (@SubjectName, @TrialSequence, @Time, @PupilDiaX, @PupilDiaY, @GazePosX, @GazePosY, @MousePosX, @MousePosY, @EventID);
SELECT ID, SubjectName, TrialSequence, Time, PupilDiaX, PupilDiaY, GazePosX, GazePosY, MousePosX, MousePosY, EventID FROM Rawdata WHERE (ID = last_insert_rowid())";
      this._adapter.InsertCommand.CommandType = CommandType.Text;
      this._adapter.InsertCommand.Parameters.Add(
        new SQLiteParameter(
          "@SubjectName",
          DbType.String,
          0,
          ParameterDirection.Input,
          0,
          0,
          "SubjectName",
          DataRowVersion.Current,
          false,
          null));
      this._adapter.InsertCommand.Parameters.Add(
        new SQLiteParameter(
          "@TrialSequence",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "TrialSequence",
          DataRowVersion.Current,
          false,
          null));
      this._adapter.InsertCommand.Parameters.Add(
        new SQLiteParameter(
          "@Time",
          DbType.Int64,
          0,
          ParameterDirection.Input,
          0,
          0,
          "Time",
          DataRowVersion.Current,
          false,
          null));
      this._adapter.InsertCommand.Parameters.Add(
        new SQLiteParameter(
          "@PupilDiaX",
          DbType.Single,
          0,
          ParameterDirection.Input,
          0,
          0,
          "PupilDiaX",
          DataRowVersion.Current,
          false,
          null));
      this._adapter.InsertCommand.Parameters.Add(
        new SQLiteParameter(
          "@PupilDiaY",
          DbType.Single,
          0,
          ParameterDirection.Input,
          0,
          0,
          "PupilDiaY",
          DataRowVersion.Current,
          false,
          null));
      this._adapter.InsertCommand.Parameters.Add(
        new SQLiteParameter(
          "@GazePosX",
          DbType.Single,
          0,
          ParameterDirection.Input,
          0,
          0,
          "GazePosX",
          DataRowVersion.Current,
          false,
          null));
      this._adapter.InsertCommand.Parameters.Add(
        new SQLiteParameter(
          "@GazePosY",
          DbType.Single,
          0,
          ParameterDirection.Input,
          0,
          0,
          "GazePosY",
          DataRowVersion.Current,
          false,
          null));
      this._adapter.InsertCommand.Parameters.Add(
        new SQLiteParameter(
          "@MousePosX",
          DbType.Single,
          0,
          ParameterDirection.Input,
          0,
          0,
          "MousePosX",
          DataRowVersion.Current,
          false,
          null));
      this._adapter.InsertCommand.Parameters.Add(
        new SQLiteParameter(
          "@MousePosY",
          DbType.Single,
          0,
          ParameterDirection.Input,
          0,
          0,
          "MousePosY",
          DataRowVersion.Current,
          false,
          null));
      this._adapter.InsertCommand.Parameters.Add(
        new SQLiteParameter(
          "@EventID",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "EventID",
          DataRowVersion.Current,
          false,
          null));
      this._adapter.UpdateCommand = new SQLiteCommand();
      this._adapter.UpdateCommand.Connection = this.Connection;
      this._adapter.UpdateCommand.CommandText =
        @"UPDATE [Rawdata] SET [SubjectName] = @SubjectName, [TrialSequence] = @TrialSequence, [Time] = @Time, [PupilDiaX] = @PupilDiaX, [PupilDiaY] = @PupilDiaY, [GazePosX] = @GazePosX, [GazePosY] = @GazePosY, [MousePosX] = @MousePosX, [MousePosY] = @MousePosY, [EventID] = @EventID WHERE (([ID] = @Original_ID) AND ([SubjectName] = @Original_SubjectName) AND ([TrialSequence] = @Original_TrialSequence) AND ([Time] = @Original_Time) AND ((@IsNull_PupilDiaX = 1 AND [PupilDiaX] IS NULL) OR ([PupilDiaX] = @Original_PupilDiaX)) AND ((@IsNull_PupilDiaY = 1 AND [PupilDiaY] IS NULL) OR ([PupilDiaY] = @Original_PupilDiaY)) AND ((@IsNull_GazePosX = 1 AND [GazePosX] IS NULL) OR ([GazePosX] = @Original_GazePosX)) AND ((@IsNull_GazePosY = 1 AND [GazePosY] IS NULL) OR ([GazePosY] = @Original_GazePosY)) AND ((@IsNull_MousePosX = 1 AND [MousePosX] IS NULL) OR ([MousePosX] = @Original_MousePosX)) AND ((@IsNull_MousePosY = 1 AND [MousePosY] IS NULL) OR ([MousePosY] = @Original_MousePosY)) AND ((@IsNull_EventID = 1 AND [EventID] IS NULL) OR ([EventID] = @Original_EventID)));
SELECT ID, SubjectName, TrialSequence, Time, PupilDiaX, PupilDiaY, GazePosX, GazePosY, MousePosX, MousePosY, EventID FROM Rawdata WHERE (ID = @ID)";
      this._adapter.UpdateCommand.CommandType = CommandType.Text;
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@SubjectName",
          DbType.String,
          0,
          ParameterDirection.Input,
          0,
          0,
          "SubjectName",
          DataRowVersion.Current,
          false,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@TrialSequence",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "TrialSequence",
          DataRowVersion.Current,
          false,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@Time",
          DbType.Int64,
          0,
          ParameterDirection.Input,
          0,
          0,
          "Time",
          DataRowVersion.Current,
          false,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@PupilDiaX",
          DbType.Single,
          0,
          ParameterDirection.Input,
          0,
          0,
          "PupilDiaX",
          DataRowVersion.Current,
          false,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@PupilDiaY",
          DbType.Single,
          0,
          ParameterDirection.Input,
          0,
          0,
          "PupilDiaY",
          DataRowVersion.Current,
          false,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@GazePosX",
          DbType.Single,
          0,
          ParameterDirection.Input,
          0,
          0,
          "GazePosX",
          DataRowVersion.Current,
          false,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@GazePosY",
          DbType.Single,
          0,
          ParameterDirection.Input,
          0,
          0,
          "GazePosY",
          DataRowVersion.Current,
          false,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@MousePosX",
          DbType.Single,
          0,
          ParameterDirection.Input,
          0,
          0,
          "MousePosX",
          DataRowVersion.Current,
          false,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@MousePosY",
          DbType.Single,
          0,
          ParameterDirection.Input,
          0,
          0,
          "MousePosY",
          DataRowVersion.Current,
          false,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@EventID",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "EventID",
          DataRowVersion.Current,
          false,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_ID",
          DbType.Int64,
          0,
          ParameterDirection.Input,
          0,
          0,
          "ID",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_SubjectName",
          DbType.String,
          0,
          ParameterDirection.Input,
          0,
          0,
          "SubjectName",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_TrialSequence",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "TrialSequence",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_Time",
          DbType.Int64,
          0,
          ParameterDirection.Input,
          0,
          0,
          "Time",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@IsNull_PupilDiaX",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "PupilDiaX",
          DataRowVersion.Original,
          true,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_PupilDiaX",
          DbType.Single,
          0,
          ParameterDirection.Input,
          0,
          0,
          "PupilDiaX",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@IsNull_PupilDiaY",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "PupilDiaY",
          DataRowVersion.Original,
          true,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_PupilDiaY",
          DbType.Single,
          0,
          ParameterDirection.Input,
          0,
          0,
          "PupilDiaY",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@IsNull_GazePosX",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "GazePosX",
          DataRowVersion.Original,
          true,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_GazePosX",
          DbType.Single,
          0,
          ParameterDirection.Input,
          0,
          0,
          "GazePosX",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@IsNull_GazePosY",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "GazePosY",
          DataRowVersion.Original,
          true,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_GazePosY",
          DbType.Single,
          0,
          ParameterDirection.Input,
          0,
          0,
          "GazePosY",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@IsNull_MousePosX",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "MousePosX",
          DataRowVersion.Original,
          true,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_MousePosX",
          DbType.Single,
          0,
          ParameterDirection.Input,
          0,
          0,
          "MousePosX",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@IsNull_MousePosY",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "MousePosY",
          DataRowVersion.Original,
          true,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_MousePosY",
          DbType.Single,
          0,
          ParameterDirection.Input,
          0,
          0,
          "MousePosY",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@IsNull_EventID",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "EventID",
          DataRowVersion.Original,
          true,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_EventID",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "EventID",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@ID",
          DbType.Int64,
          8,
          ParameterDirection.Input,
          0,
          0,
          "ID",
          DataRowVersion.Current,
          false,
          null));
    }

    /// <summary>
    ///   Initializes the command collection.
    /// </summary>
    private void InitCommandCollection()
    {
      this._commandCollection = new SQLiteCommand[1];
      this._commandCollection[0] = new SQLiteCommand();
      this._commandCollection[0].Connection = this.Connection;
      this._commandCollection[0].CommandText =
        "SELECT     ID, SubjectName, TrialSequence, Time, PupilDiaX, PupilDiaY, GazePosX, "
        + "GazePosY, MousePosX, MousePosY, EventID\r\nFROM         Rawdata";
      this._commandCollection[0].CommandType = CommandType.Text;
    }

    /// <summary>
    ///   Initializes the connection.
    /// </summary>
    private void InitConnection()
    {
      this._connection = new SQLiteConnection();
      this._connection.ConnectionString =
        global::Ogama.Properties.Settings.Default.OgamaDatabaseTemplateConnectionString;
    }

    #endregion
  }

  /// <summary>
  ///   Represents the connection and commands used to retrieve and save data.
  /// </summary>
  public class SQLiteTadAOIs : Component
  {
    #region Fields

    /// <summary>
    ///   The _adapter
    /// </summary>
    private SQLiteDataAdapter _adapter;

    /// <summary>
    ///   The _command collection
    /// </summary>
    private SQLiteCommand[] _commandCollection;

    /// <summary>
    ///   The _connection
    /// </summary>
    private SQLiteConnection _connection;

    #endregion

    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="SQLiteTadAOIs" /> class.
    /// </summary>
    public SQLiteTadAOIs()
    {
      this.ClearBeforeFill = true;
    }

    #endregion

    #region Public Properties

    /// <summary>
    ///   Gets or sets a value indicating whether [clear before fill].
    /// </summary>
    /// <value><c>true</c> if [clear before fill]; otherwise, <c>false</c>.</value>
    public bool ClearBeforeFill { get; set; }

    #endregion

    #region Properties

    /// <summary>
    ///   Gets or sets the connection.
    /// </summary>
    /// <value>The connection.</value>
    internal SQLiteConnection Connection
    {
      get
      {
        if (this._connection == null)
        {
          this.InitConnection();
        }

        return this._connection;
      }

      set
      {
        this._connection = value;
        if (this.Adapter.InsertCommand != null)
        {
          this.Adapter.InsertCommand.Connection = value;
        }

        if (this.Adapter.DeleteCommand != null)
        {
          this.Adapter.DeleteCommand.Connection = value;
        }

        if (this.Adapter.UpdateCommand != null)
        {
          this.Adapter.UpdateCommand.Connection = value;
        }

        for (int i = 0; i < this.CommandCollection.Length; i = (i + 1))
        {
          if ((this.CommandCollection[i] != null))
          {
            this.CommandCollection[i].Connection = value;
          }
        }
      }
    }

    /// <summary>
    ///   Gets the command collection.
    /// </summary>
    /// <value>The command collection.</value>
    protected SQLiteCommand[] CommandCollection
    {
      get
      {
        if (this._commandCollection == null)
        {
          this.InitCommandCollection();
        }

        return this._commandCollection;
      }
    }

    /// <summary>
    ///   Gets the adapter.
    /// </summary>
    /// <value>The adapter.</value>
    private SQLiteDataAdapter Adapter
    {
      get
      {
        if (this._adapter == null)
        {
          this.InitAdapter();
        }

        return this._adapter;
      }
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// Deletes the specified original_ identifier.
    /// </summary>
    /// <param name="Original_ID">
    /// The original_ identifier.
    /// </param>
    /// <param name="Original_TrialID">
    /// The original_ trial identifier.
    /// </param>
    /// <param name="Original_SlideNr">
    /// The original_ slide nr.
    /// </param>
    /// <param name="Original_ShapeName">
    /// Name of the original_ shape.
    /// </param>
    /// <param name="Original_ShapeType">
    /// Type of the original_ shape.
    /// </param>
    /// <param name="Original_ShapeNumPts">
    /// The original_ shape number PTS.
    /// </param>
    /// <param name="Original_ShapePts">
    /// The original_ shape PTS.
    /// </param>
    /// <param name="Original_ShapeGroup">
    /// The original_ shape group.
    /// </param>
    /// <returns>
    /// System.Int32.
    /// </returns>
    [DataObjectMethodAttribute(DataObjectMethodType.Delete,
     true)]
    public virtual int Delete(
      int Original_ID,
      int Original_TrialID,
      int Original_SlideNr,
      string Original_ShapeName,
      string Original_ShapeType,
      int? Original_ShapeNumPts,
      string Original_ShapePts,
      string Original_ShapeGroup)
    {
      this.Adapter.DeleteCommand.Parameters[0].Value = Original_ID;
      this.Adapter.DeleteCommand.Parameters[1].Value = Original_TrialID;
      this.Adapter.DeleteCommand.Parameters[2].Value = Original_SlideNr;
      if (Original_ShapeName == null)
      {
        this.Adapter.DeleteCommand.Parameters[3].Value = 1;
        this.Adapter.DeleteCommand.Parameters[4].Value = DBNull.Value;
      }
      else
      {
        this.Adapter.DeleteCommand.Parameters[3].Value = 0;
        this.Adapter.DeleteCommand.Parameters[4].Value = Original_ShapeName;
      }

      if (Original_ShapeType == null)
      {
        this.Adapter.DeleteCommand.Parameters[5].Value = 1;
        this.Adapter.DeleteCommand.Parameters[6].Value = DBNull.Value;
      }
      else
      {
        this.Adapter.DeleteCommand.Parameters[5].Value = 0;
        this.Adapter.DeleteCommand.Parameters[6].Value = Original_ShapeType;
      }

      if (Original_ShapeNumPts.HasValue)
      {
        this.Adapter.DeleteCommand.Parameters[7].Value = 0;
        this.Adapter.DeleteCommand.Parameters[8].Value = Original_ShapeNumPts.Value;
      }
      else
      {
        this.Adapter.DeleteCommand.Parameters[7].Value = 1;
        this.Adapter.DeleteCommand.Parameters[8].Value = DBNull.Value;
      }

      if (Original_ShapePts == null)
      {
        this.Adapter.DeleteCommand.Parameters[9].Value = 1;
        this.Adapter.DeleteCommand.Parameters[10].Value = DBNull.Value;
      }
      else
      {
        this.Adapter.DeleteCommand.Parameters[9].Value = 0;
        this.Adapter.DeleteCommand.Parameters[10].Value = Original_ShapePts;
      }

      if (Original_ShapeGroup == null)
      {
        this.Adapter.DeleteCommand.Parameters[11].Value = 1;
        this.Adapter.DeleteCommand.Parameters[12].Value = DBNull.Value;
      }
      else
      {
        this.Adapter.DeleteCommand.Parameters[11].Value = 0;
        this.Adapter.DeleteCommand.Parameters[12].Value = Original_ShapeGroup;
      }

      ConnectionState previousConnectionState = this.Adapter.DeleteCommand.Connection.State;
      if ((this.Adapter.DeleteCommand.Connection.State & ConnectionState.Open)
           != ConnectionState.Open)
      {
        this.Adapter.DeleteCommand.Connection.Open();
      }

      try
      {
        int returnValue = this.Adapter.DeleteCommand.ExecuteNonQuery();
        return returnValue;
      }
      finally
      {
        if (previousConnectionState == ConnectionState.Closed)
        {
          this.Adapter.DeleteCommand.Connection.Close();
        }
      }
    }

    /// <summary>
    /// Deletes the by identifier.
    /// </summary>
    /// <param name="Original_ID">
    /// The original_ identifier.
    /// </param>
    /// <returns>
    /// System.Int32.
    /// </returns>
    [DataObjectMethodAttribute(DataObjectMethodType.Delete,
     false)]
    public virtual int DeleteByID(int Original_ID)
    {
      SQLiteCommand command = this.CommandCollection[1];
      command.Parameters[0].Value = Original_ID;
      ConnectionState previousConnectionState = command.Connection.State;
      if ((command.Connection.State & ConnectionState.Open)
           != ConnectionState.Open)
      {
        command.Connection.Open();
      }

      int returnValue;
      try
      {
        returnValue = command.ExecuteNonQuery();
      }
      finally
      {
        if (previousConnectionState == ConnectionState.Closed)
        {
          command.Connection.Close();
        }
      }

      return returnValue;
    }

    /// <summary>
    /// Fills the specified data table.
    /// </summary>
    /// <param name="dataTable">
    /// The data table.
    /// </param>
    /// <returns>
    /// System.Int32.
    /// </returns>
    [DataObjectMethodAttribute(DataObjectMethodType.Fill,
     true)]
    public virtual int Fill(SQLiteOgamaDataSet.AOIsDataTable dataTable)
    {
      this.Adapter.SelectCommand = this.CommandCollection[0];
      if (this.ClearBeforeFill)
      {
        dataTable.Clear();
      }

      int returnValue = this.Adapter.Fill(dataTable);
      return returnValue;
    }

    /// <summary>
    ///   Gets the data.
    /// </summary>
    /// <returns>SQLiteOgamaDataSet.AOIsDataTable.</returns>
    [DataObjectMethodAttribute(DataObjectMethodType.Select,
     true)]
    public virtual SQLiteOgamaDataSet.AOIsDataTable GetData()
    {
      this.Adapter.SelectCommand = this.CommandCollection[0];
      var dataTable = new SQLiteOgamaDataSet.AOIsDataTable();
      this.Adapter.Fill(dataTable);
      return dataTable;
    }

    /// <summary>
    /// Gets the data by trial identifier.
    /// </summary>
    /// <param name="Param1">
    /// The param1.
    /// </param>
    /// <returns>
    /// SQLiteOgamaDataSet.AOIsDataTable.
    /// </returns>
    [DataObjectMethodAttribute(DataObjectMethodType.Select,
     false)]
    public virtual SQLiteOgamaDataSet.AOIsDataTable GetDataByTrialID(int Param1)
    {
      this.Adapter.SelectCommand = this.CommandCollection[2];
      this.Adapter.SelectCommand.Parameters[0].Value = Param1;
      var dataTable = new SQLiteOgamaDataSet.AOIsDataTable();
      this.Adapter.Fill(dataTable);
      return dataTable;
    }

    /// <summary>
    /// Gets the data by trial identifier and group.
    /// </summary>
    /// <param name="Param1">
    /// The param1.
    /// </param>
    /// <param name="Param2">
    /// The param2.
    /// </param>
    /// <returns>
    /// SQLiteOgamaDataSet.AOIsDataTable.
    /// </returns>
    [DataObjectMethodAttribute(DataObjectMethodType.Select,
     false)]
    public virtual SQLiteOgamaDataSet.AOIsDataTable GetDataByTrialIDAndGroup(int Param1, string Param2)
    {
      this.Adapter.SelectCommand = this.CommandCollection[3];
      this.Adapter.SelectCommand.Parameters[0].Value = Param1;
      if (Param2 == null)
      {
        this.Adapter.SelectCommand.Parameters[1].Value = DBNull.Value;
      }
      else
      {
        this.Adapter.SelectCommand.Parameters[1].Value = Param2;
      }

      var dataTable = new SQLiteOgamaDataSet.AOIsDataTable();
      this.Adapter.Fill(dataTable);
      return dataTable;
    }

    /// <summary>
    /// Gets the name of the data by trial identifier and shape.
    /// </summary>
    /// <param name="Param1">
    /// The param1.
    /// </param>
    /// <param name="Param2">
    /// The param2.
    /// </param>
    /// <returns>
    /// SQLiteOgamaDataSet.AOIsDataTable.
    /// </returns>
    [DataObjectMethodAttribute(DataObjectMethodType.Select,
     false)]
    public virtual SQLiteOgamaDataSet.AOIsDataTable GetDataByTrialIDAndShapeName(int Param1, string Param2)
    {
      this.Adapter.SelectCommand = this.CommandCollection[4];
      this.Adapter.SelectCommand.Parameters[0].Value = Param1;
      if (Param2 == null)
      {
        this.Adapter.SelectCommand.Parameters[1].Value = DBNull.Value;
      }
      else
      {
        this.Adapter.SelectCommand.Parameters[1].Value = Param2;
      }

      var dataTable = new SQLiteOgamaDataSet.AOIsDataTable();
      this.Adapter.Fill(dataTable);
      return dataTable;
    }

    /// <summary>
    /// Gets the groups by trial identifier.
    /// </summary>
    /// <param name="Param1">
    /// The param1.
    /// </param>
    /// <returns>
    /// SQLiteOgamaDataSet.AOIsDataTable.
    /// </returns>
    [DataObjectMethodAttribute(DataObjectMethodType.Select,
     false)]
    public virtual SQLiteOgamaDataSet.AOIsDataTable GetGroupsByTrialID(int Param1)
    {
      this.Adapter.SelectCommand = this.CommandCollection[5];
      this.Adapter.SelectCommand.Parameters[0].Value = Param1;
      var dataTable = new SQLiteOgamaDataSet.AOIsDataTable();
      this.Adapter.Fill(dataTable);
      return dataTable;
    }

    /// <summary>
    /// Gets the targets by trial identifier.
    /// </summary>
    /// <param name="Param1">
    /// The param1.
    /// </param>
    /// <returns>
    /// SQLiteOgamaDataSet.AOIsDataTable.
    /// </returns>
    [DataObjectMethodAttribute(DataObjectMethodType.Select,
     false)]
    public virtual SQLiteOgamaDataSet.AOIsDataTable GetTargetsByTrialID(int Param1)
    {
      this.Adapter.SelectCommand = this.CommandCollection[6];
      this.Adapter.SelectCommand.Parameters[0].Value = Param1;
      var dataTable = new SQLiteOgamaDataSet.AOIsDataTable();
      this.Adapter.Fill(dataTable);
      return dataTable;
    }

    /// <summary>
    /// Inserts the specified trial identifier.
    /// </summary>
    /// <param name="TrialID">
    /// The trial identifier.
    /// </param>
    /// <param name="SlideNr">
    /// The slide nr.
    /// </param>
    /// <param name="ShapeName">
    /// Name of the shape.
    /// </param>
    /// <param name="ShapeType">
    /// Type of the shape.
    /// </param>
    /// <param name="ShapeNumPts">
    /// The shape number PTS.
    /// </param>
    /// <param name="ShapePts">
    /// The shape PTS.
    /// </param>
    /// <param name="ShapeGroup">
    /// The shape group.
    /// </param>
    /// <returns>
    /// System.Int32.
    /// </returns>
    [DataObjectMethodAttribute(DataObjectMethodType.Insert,
     true)]
    public virtual int Insert(
      int TrialID,
      int SlideNr,
      string ShapeName,
      string ShapeType,
      int? ShapeNumPts,
      string ShapePts,
      string ShapeGroup)
    {
      this.Adapter.InsertCommand.Parameters[0].Value = TrialID;
      this.Adapter.InsertCommand.Parameters[1].Value = SlideNr;
      if (ShapeName == null)
      {
        this.Adapter.InsertCommand.Parameters[2].Value = DBNull.Value;
      }
      else
      {
        this.Adapter.InsertCommand.Parameters[2].Value = ShapeName;
      }

      if (ShapeType == null)
      {
        this.Adapter.InsertCommand.Parameters[3].Value = DBNull.Value;
      }
      else
      {
        this.Adapter.InsertCommand.Parameters[3].Value = ShapeType;
      }

      if (ShapeNumPts.HasValue)
      {
        this.Adapter.InsertCommand.Parameters[4].Value = ShapeNumPts.Value;
      }
      else
      {
        this.Adapter.InsertCommand.Parameters[4].Value = DBNull.Value;
      }

      if (ShapePts == null)
      {
        this.Adapter.InsertCommand.Parameters[5].Value = DBNull.Value;
      }
      else
      {
        this.Adapter.InsertCommand.Parameters[5].Value = ShapePts;
      }

      if (ShapeGroup == null)
      {
        this.Adapter.InsertCommand.Parameters[6].Value = DBNull.Value;
      }
      else
      {
        this.Adapter.InsertCommand.Parameters[6].Value = ShapeGroup;
      }

      ConnectionState previousConnectionState = this.Adapter.InsertCommand.Connection.State;
      if ((this.Adapter.InsertCommand.Connection.State & ConnectionState.Open)
           != ConnectionState.Open)
      {
        this.Adapter.InsertCommand.Connection.Open();
      }

      try
      {
        int returnValue = this.Adapter.InsertCommand.ExecuteNonQuery();
        return returnValue;
      }
      finally
      {
        if (previousConnectionState == ConnectionState.Closed)
        {
          this.Adapter.InsertCommand.Connection.Close();
        }
      }
    }

    /// <summary>
    /// Updates the specified data table.
    /// </summary>
    /// <param name="dataTable">
    /// The data table.
    /// </param>
    /// <returns>
    /// System.Int32.
    /// </returns>
    public virtual int Update(SQLiteOgamaDataSet.AOIsDataTable dataTable)
    {
      return this.Adapter.Update(dataTable);
    }

    /// <summary>
    /// Updates the specified data set.
    /// </summary>
    /// <param name="dataSet">
    /// The data set.
    /// </param>
    /// <returns>
    /// System.Int32.
    /// </returns>
    public virtual int Update(SQLiteOgamaDataSet dataSet)
    {
      return this.Adapter.Update(dataSet, "AOIs");
    }

    /// <summary>
    /// Updates the specified data row.
    /// </summary>
    /// <param name="dataRow">
    /// The data row.
    /// </param>
    /// <returns>
    /// System.Int32.
    /// </returns>
    public virtual int Update(DataRow dataRow)
    {
      return this.Adapter.Update(new[] { dataRow });
    }

    /// <summary>
    /// Updates the specified data rows.
    /// </summary>
    /// <param name="dataRows">
    /// The data rows.
    /// </param>
    /// <returns>
    /// System.Int32.
    /// </returns>
    public virtual int Update(DataRow[] dataRows)
    {
      return this.Adapter.Update(dataRows);
    }

    /// <summary>
    /// Updates the specified trial identifier.
    /// </summary>
    /// <param name="TrialID">
    /// The trial identifier.
    /// </param>
    /// <param name="SlideNr">
    /// The slide nr.
    /// </param>
    /// <param name="ShapeName">
    /// Name of the shape.
    /// </param>
    /// <param name="ShapeType">
    /// Type of the shape.
    /// </param>
    /// <param name="ShapeNumPts">
    /// The shape number PTS.
    /// </param>
    /// <param name="ShapePts">
    /// The shape PTS.
    /// </param>
    /// <param name="ShapeGroup">
    /// The shape group.
    /// </param>
    /// <param name="Original_ID">
    /// The original_ identifier.
    /// </param>
    /// <param name="Original_TrialID">
    /// The original_ trial identifier.
    /// </param>
    /// <param name="Original_SlideNr">
    /// The original_ slide nr.
    /// </param>
    /// <param name="Original_ShapeName">
    /// Name of the original_ shape.
    /// </param>
    /// <param name="Original_ShapeType">
    /// Type of the original_ shape.
    /// </param>
    /// <param name="Original_ShapeNumPts">
    /// The original_ shape number PTS.
    /// </param>
    /// <param name="Original_ShapePts">
    /// The original_ shape PTS.
    /// </param>
    /// <param name="Original_ShapeGroup">
    /// The original_ shape group.
    /// </param>
    /// <param name="ID">
    /// The identifier.
    /// </param>
    /// <returns>
    /// System.Int32.
    /// </returns>
    [DataObjectMethodAttribute(DataObjectMethodType.Update,
     true)]
    public virtual int Update(
      int TrialID,
      int SlideNr,
      string ShapeName,
      string ShapeType,
      int? ShapeNumPts,
      string ShapePts,
      string ShapeGroup,
      int Original_ID,
      int Original_TrialID,
      int Original_SlideNr,
      string Original_ShapeName,
      string Original_ShapeType,
      int? Original_ShapeNumPts,
      string Original_ShapePts,
      string Original_ShapeGroup,
      int ID)
    {
      this.Adapter.UpdateCommand.Parameters[0].Value = TrialID;
      this.Adapter.UpdateCommand.Parameters[1].Value = SlideNr;
      if (ShapeName == null)
      {
        this.Adapter.UpdateCommand.Parameters[2].Value = DBNull.Value;
      }
      else
      {
        this.Adapter.UpdateCommand.Parameters[2].Value = ShapeName;
      }

      if (ShapeType == null)
      {
        this.Adapter.UpdateCommand.Parameters[3].Value = DBNull.Value;
      }
      else
      {
        this.Adapter.UpdateCommand.Parameters[3].Value = ShapeType;
      }

      if (ShapeNumPts.HasValue)
      {
        this.Adapter.UpdateCommand.Parameters[4].Value = ShapeNumPts.Value;
      }
      else
      {
        this.Adapter.UpdateCommand.Parameters[4].Value = DBNull.Value;
      }

      if (ShapePts == null)
      {
        this.Adapter.UpdateCommand.Parameters[5].Value = DBNull.Value;
      }
      else
      {
        this.Adapter.UpdateCommand.Parameters[5].Value = ShapePts;
      }

      if (ShapeGroup == null)
      {
        this.Adapter.UpdateCommand.Parameters[6].Value = DBNull.Value;
      }
      else
      {
        this.Adapter.UpdateCommand.Parameters[6].Value = ShapeGroup;
      }

      this.Adapter.UpdateCommand.Parameters[7].Value = Original_ID;
      this.Adapter.UpdateCommand.Parameters[8].Value = Original_TrialID;
      this.Adapter.UpdateCommand.Parameters[9].Value = Original_SlideNr;
      if (Original_ShapeName == null)
      {
        this.Adapter.UpdateCommand.Parameters[10].Value = 1;
        this.Adapter.UpdateCommand.Parameters[11].Value = DBNull.Value;
      }
      else
      {
        this.Adapter.UpdateCommand.Parameters[10].Value = 0;
        this.Adapter.UpdateCommand.Parameters[11].Value = Original_ShapeName;
      }

      if (Original_ShapeType == null)
      {
        this.Adapter.UpdateCommand.Parameters[12].Value = 1;
        this.Adapter.UpdateCommand.Parameters[13].Value = DBNull.Value;
      }
      else
      {
        this.Adapter.UpdateCommand.Parameters[12].Value = 0;
        this.Adapter.UpdateCommand.Parameters[13].Value = Original_ShapeType;
      }

      if (Original_ShapeNumPts.HasValue)
      {
        this.Adapter.UpdateCommand.Parameters[14].Value = 0;
        this.Adapter.UpdateCommand.Parameters[15].Value = Original_ShapeNumPts.Value;
      }
      else
      {
        this.Adapter.UpdateCommand.Parameters[14].Value = 1;
        this.Adapter.UpdateCommand.Parameters[15].Value = DBNull.Value;
      }

      if (Original_ShapePts == null)
      {
        this.Adapter.UpdateCommand.Parameters[16].Value = 1;
        this.Adapter.UpdateCommand.Parameters[17].Value = DBNull.Value;
      }
      else
      {
        this.Adapter.UpdateCommand.Parameters[16].Value = 0;
        this.Adapter.UpdateCommand.Parameters[17].Value = Original_ShapePts;
      }

      if (Original_ShapeGroup == null)
      {
        this.Adapter.UpdateCommand.Parameters[18].Value = 1;
        this.Adapter.UpdateCommand.Parameters[19].Value = DBNull.Value;
      }
      else
      {
        this.Adapter.UpdateCommand.Parameters[18].Value = 0;
        this.Adapter.UpdateCommand.Parameters[19].Value = Original_ShapeGroup;
      }

      this.Adapter.UpdateCommand.Parameters[20].Value = ID;
      ConnectionState previousConnectionState = this.Adapter.UpdateCommand.Connection.State;
      if ((this.Adapter.UpdateCommand.Connection.State & ConnectionState.Open)
           != ConnectionState.Open)
      {
        this.Adapter.UpdateCommand.Connection.Open();
      }

      try
      {
        int returnValue = this.Adapter.UpdateCommand.ExecuteNonQuery();
        return returnValue;
      }
      finally
      {
        if (previousConnectionState == ConnectionState.Closed)
        {
          this.Adapter.UpdateCommand.Connection.Close();
        }
      }
    }

    /// <summary>
    /// Updates the specified trial identifier.
    /// </summary>
    /// <param name="TrialID">
    /// The trial identifier.
    /// </param>
    /// <param name="SlideNr">
    /// The slide nr.
    /// </param>
    /// <param name="ShapeName">
    /// Name of the shape.
    /// </param>
    /// <param name="ShapeType">
    /// Type of the shape.
    /// </param>
    /// <param name="ShapeNumPts">
    /// The shape number PTS.
    /// </param>
    /// <param name="ShapePts">
    /// The shape PTS.
    /// </param>
    /// <param name="ShapeGroup">
    /// The shape group.
    /// </param>
    /// <param name="Original_ID">
    /// The original_ identifier.
    /// </param>
    /// <param name="Original_TrialID">
    /// The original_ trial identifier.
    /// </param>
    /// <param name="Original_SlideNr">
    /// The original_ slide nr.
    /// </param>
    /// <param name="Original_ShapeName">
    /// Name of the original_ shape.
    /// </param>
    /// <param name="Original_ShapeType">
    /// Type of the original_ shape.
    /// </param>
    /// <param name="Original_ShapeNumPts">
    /// The original_ shape number PTS.
    /// </param>
    /// <param name="Original_ShapePts">
    /// The original_ shape PTS.
    /// </param>
    /// <param name="Original_ShapeGroup">
    /// The original_ shape group.
    /// </param>
    /// <returns>
    /// System.Int32.
    /// </returns>
    [DataObjectMethodAttribute(DataObjectMethodType.Update,
     true)]
    public virtual int Update(
      int TrialID,
      int SlideNr,
      string ShapeName,
      string ShapeType,
      int? ShapeNumPts,
      string ShapePts,
      string ShapeGroup,
      int Original_ID,
      int Original_TrialID,
      int Original_SlideNr,
      string Original_ShapeName,
      string Original_ShapeType,
      int? Original_ShapeNumPts,
      string Original_ShapePts,
      string Original_ShapeGroup)
    {
      return this.Update(
        TrialID,
        SlideNr,
        ShapeName,
        ShapeType,
        ShapeNumPts,
        ShapePts,
        ShapeGroup,
        Original_ID,
        Original_TrialID,
        Original_SlideNr,
        Original_ShapeName,
        Original_ShapeType,
        Original_ShapeNumPts,
        Original_ShapePts,
        Original_ShapeGroup,
        Original_ID);
    }

    #endregion

    #region Methods

    /// <summary>
    ///   Initializes the adapter.
    /// </summary>
    private void InitAdapter()
    {
      this._adapter = new SQLiteDataAdapter();
      var tableMapping = new DataTableMapping();
      tableMapping.SourceTable = "Table";
      tableMapping.DataSetTable = "AOIs";
      tableMapping.ColumnMappings.Add("ID", "ID");
      tableMapping.ColumnMappings.Add("TrialID", "TrialID");
      tableMapping.ColumnMappings.Add("SlideNr", "SlideNr");
      tableMapping.ColumnMappings.Add("ShapeName", "ShapeName");
      tableMapping.ColumnMappings.Add("ShapeType", "ShapeType");
      tableMapping.ColumnMappings.Add("ShapeNumPts", "ShapeNumPts");
      tableMapping.ColumnMappings.Add("ShapePts", "ShapePts");
      tableMapping.ColumnMappings.Add("ShapeGroup", "ShapeGroup");
      this._adapter.TableMappings.Add(tableMapping);
      this._adapter.DeleteCommand = new SQLiteCommand();
      this._adapter.DeleteCommand.Connection = this.Connection;
      this._adapter.DeleteCommand.CommandText =
        @"DELETE FROM [AOIs] WHERE (([ID] = @Original_ID) AND ([TrialID] = @Original_TrialID) AND ([SlideNr] = @Original_SlideNr) AND ((@IsNull_ShapeName = 1 AND [ShapeName] IS NULL) OR ([ShapeName] = @Original_ShapeName)) AND ((@IsNull_ShapeType = 1 AND [ShapeType] IS NULL) OR ([ShapeType] = @Original_ShapeType)) AND ((@IsNull_ShapeNumPts = 1 AND [ShapeNumPts] IS NULL) OR ([ShapeNumPts] = @Original_ShapeNumPts)) AND ((@IsNull_ShapePts = 1 AND [ShapePts] IS NULL) OR ([ShapePts] = @Original_ShapePts)) AND ((@IsNull_ShapeGroup = 1 AND [ShapeGroup] IS NULL) OR ([ShapeGroup] = @Original_ShapeGroup)))";
      this._adapter.DeleteCommand.CommandType = CommandType.Text;
      this._adapter.DeleteCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_ID",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "ID",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.DeleteCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_TrialID",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "TrialID",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.DeleteCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_SlideNr",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "SlideNr",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.DeleteCommand.Parameters.Add(
        new SQLiteParameter(
          "@IsNull_ShapeName",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "ShapeName",
          DataRowVersion.Original,
          true,
          null));
      this._adapter.DeleteCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_ShapeName",
          DbType.String,
          0,
          ParameterDirection.Input,
          0,
          0,
          "ShapeName",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.DeleteCommand.Parameters.Add(
        new SQLiteParameter(
          "@IsNull_ShapeType",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "ShapeType",
          DataRowVersion.Original,
          true,
          null));
      this._adapter.DeleteCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_ShapeType",
          DbType.String,
          0,
          ParameterDirection.Input,
          0,
          0,
          "ShapeType",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.DeleteCommand.Parameters.Add(
        new SQLiteParameter(
          "@IsNull_ShapeNumPts",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "ShapeNumPts",
          DataRowVersion.Original,
          true,
          null));
      this._adapter.DeleteCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_ShapeNumPts",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "ShapeNumPts",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.DeleteCommand.Parameters.Add(
        new SQLiteParameter(
          "@IsNull_ShapePts",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "ShapePts",
          DataRowVersion.Original,
          true,
          null));
      this._adapter.DeleteCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_ShapePts",
          DbType.String,
          0,
          ParameterDirection.Input,
          0,
          0,
          "ShapePts",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.DeleteCommand.Parameters.Add(
        new SQLiteParameter(
          "@IsNull_ShapeGroup",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "ShapeGroup",
          DataRowVersion.Original,
          true,
          null));
      this._adapter.DeleteCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_ShapeGroup",
          DbType.String,
          0,
          ParameterDirection.Input,
          0,
          0,
          "ShapeGroup",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.InsertCommand = new SQLiteCommand();
      this._adapter.InsertCommand.Connection = this.Connection;
      this._adapter.InsertCommand.CommandText =
        @"INSERT INTO [AOIs] ([TrialID], [SlideNr], [ShapeName], [ShapeType], [ShapeNumPts], [ShapePts], [ShapeGroup]) VALUES (@TrialID, @SlideNr, @ShapeName, @ShapeType, @ShapeNumPts, @ShapePts, @ShapeGroup);
SELECT ID, TrialID, SlideNr, ShapeName, ShapeType, ShapeNumPts, ShapePts, ShapeGroup FROM AOIs WHERE (ID = last_insert_rowid())";
      this._adapter.InsertCommand.CommandType = CommandType.Text;
      this._adapter.InsertCommand.Parameters.Add(
        new SQLiteParameter(
          "@TrialID",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "TrialID",
          DataRowVersion.Current,
          false,
          null));
      this._adapter.InsertCommand.Parameters.Add(
        new SQLiteParameter(
          "@SlideNr",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "SlideNr",
          DataRowVersion.Current,
          false,
          null));
      this._adapter.InsertCommand.Parameters.Add(
        new SQLiteParameter(
          "@ShapeName",
          DbType.String,
          0,
          ParameterDirection.Input,
          0,
          0,
          "ShapeName",
          DataRowVersion.Current,
          false,
          null));
      this._adapter.InsertCommand.Parameters.Add(
        new SQLiteParameter(
          "@ShapeType",
          DbType.String,
          0,
          ParameterDirection.Input,
          0,
          0,
          "ShapeType",
          DataRowVersion.Current,
          false,
          null));
      this._adapter.InsertCommand.Parameters.Add(
        new SQLiteParameter(
          "@ShapeNumPts",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "ShapeNumPts",
          DataRowVersion.Current,
          false,
          null));
      this._adapter.InsertCommand.Parameters.Add(
        new SQLiteParameter(
          "@ShapePts",
          DbType.String,
          0,
          ParameterDirection.Input,
          0,
          0,
          "ShapePts",
          DataRowVersion.Current,
          false,
          null));
      this._adapter.InsertCommand.Parameters.Add(
        new SQLiteParameter(
          "@ShapeGroup",
          DbType.String,
          0,
          ParameterDirection.Input,
          0,
          0,
          "ShapeGroup",
          DataRowVersion.Current,
          false,
          null));
      this._adapter.UpdateCommand = new SQLiteCommand();
      this._adapter.UpdateCommand.Connection = this.Connection;
      this._adapter.UpdateCommand.CommandText =
        @"UPDATE [AOIs] SET [TrialID] = @TrialID, [SlideNr] = @SlideNr, [ShapeName] = @ShapeName, [ShapeType] = @ShapeType, [ShapeNumPts] = @ShapeNumPts, [ShapePts] = @ShapePts, [ShapeGroup] = @ShapeGroup WHERE (([ID] = @Original_ID) AND ([TrialID] = @Original_TrialID) AND ([SlideNr] = @Original_SlideNr) AND ((@IsNull_ShapeName = 1 AND [ShapeName] IS NULL) OR ([ShapeName] = @Original_ShapeName)) AND ((@IsNull_ShapeType = 1 AND [ShapeType] IS NULL) OR ([ShapeType] = @Original_ShapeType)) AND ((@IsNull_ShapeNumPts = 1 AND [ShapeNumPts] IS NULL) OR ([ShapeNumPts] = @Original_ShapeNumPts)) AND ((@IsNull_ShapePts = 1 AND [ShapePts] IS NULL) OR ([ShapePts] = @Original_ShapePts)) AND ((@IsNull_ShapeGroup = 1 AND [ShapeGroup] IS NULL) OR ([ShapeGroup] = @Original_ShapeGroup)));
SELECT ID, TrialID, SlideNr, ShapeName, ShapeType, ShapeNumPts, ShapePts, ShapeGroup FROM AOIs WHERE (ID = @ID)";
      this._adapter.UpdateCommand.CommandType = CommandType.Text;
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@TrialID",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "TrialID",
          DataRowVersion.Current,
          false,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@SlideNr",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "SlideNr",
          DataRowVersion.Current,
          false,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@ShapeName",
          DbType.String,
          0,
          ParameterDirection.Input,
          0,
          0,
          "ShapeName",
          DataRowVersion.Current,
          false,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@ShapeType",
          DbType.String,
          0,
          ParameterDirection.Input,
          0,
          0,
          "ShapeType",
          DataRowVersion.Current,
          false,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@ShapeNumPts",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "ShapeNumPts",
          DataRowVersion.Current,
          false,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@ShapePts",
          DbType.String,
          0,
          ParameterDirection.Input,
          0,
          0,
          "ShapePts",
          DataRowVersion.Current,
          false,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@ShapeGroup",
          DbType.String,
          0,
          ParameterDirection.Input,
          0,
          0,
          "ShapeGroup",
          DataRowVersion.Current,
          false,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_ID",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "ID",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_TrialID",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "TrialID",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_SlideNr",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "SlideNr",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@IsNull_ShapeName",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "ShapeName",
          DataRowVersion.Original,
          true,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_ShapeName",
          DbType.String,
          0,
          ParameterDirection.Input,
          0,
          0,
          "ShapeName",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@IsNull_ShapeType",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "ShapeType",
          DataRowVersion.Original,
          true,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_ShapeType",
          DbType.String,
          0,
          ParameterDirection.Input,
          0,
          0,
          "ShapeType",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@IsNull_ShapeNumPts",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "ShapeNumPts",
          DataRowVersion.Original,
          true,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_ShapeNumPts",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "ShapeNumPts",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@IsNull_ShapePts",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "ShapePts",
          DataRowVersion.Original,
          true,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_ShapePts",
          DbType.String,
          0,
          ParameterDirection.Input,
          0,
          0,
          "ShapePts",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@IsNull_ShapeGroup",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "ShapeGroup",
          DataRowVersion.Original,
          true,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_ShapeGroup",
          DbType.String,
          0,
          ParameterDirection.Input,
          0,
          0,
          "ShapeGroup",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@ID",
          DbType.Int32,
          4,
          ParameterDirection.Input,
          0,
          0,
          "ID",
          DataRowVersion.Current,
          false,
          null));
    }

    /// <summary>
    ///   Initializes the command collection.
    /// </summary>
    private void InitCommandCollection()
    {
      this._commandCollection = new SQLiteCommand[7];
      this._commandCollection[0] = new SQLiteCommand();
      this._commandCollection[0].Connection = this.Connection;
      this._commandCollection[0].CommandText =
        "SELECT     ID, TrialID, SlideNr, ShapeName, ShapeType, ShapeNumPts, ShapePts, Sha"
        + "peGroup\r\nFROM         AOIs";
      this._commandCollection[0].CommandType = CommandType.Text;
      this._commandCollection[1] = new SQLiteCommand();
      this._commandCollection[1].Connection = this.Connection;
      this._commandCollection[1].CommandText = "DELETE FROM AOIs\r\nWHERE     (ID = @Original_ID)";
      this._commandCollection[1].CommandType = CommandType.Text;
      this._commandCollection[1].Parameters.Add(
        new SQLiteParameter(
          "@Original_ID",
          DbType.Int32,
          4,
          ParameterDirection.Input,
          0,
          0,
          "ID",
          DataRowVersion.Original,
          false,
          null));
      this._commandCollection[2] = new SQLiteCommand();
      this._commandCollection[2].Connection = this.Connection;
      this._commandCollection[2].CommandText =
        "SELECT ID, ShapeGroup, ShapeName, ShapeNumPts, ShapePts, ShapeType, SlideNr, Tria"
        + "lID FROM AOIs WHERE (TrialID = @Param1)";
      this._commandCollection[2].CommandType = CommandType.Text;
      this._commandCollection[2].Parameters.Add(
        new SQLiteParameter(
          "@Param1",
          DbType.Int32,
          4,
          ParameterDirection.Input,
          0,
          0,
          "TrialID",
          DataRowVersion.Current,
          false,
          null));
      this._commandCollection[3] = new SQLiteCommand();
      this._commandCollection[3].Connection = this.Connection;
      this._commandCollection[3].CommandText =
        "SELECT ID, ShapeGroup, ShapeName, ShapeNumPts, ShapePts, ShapeType, SlideNr, Tria"
        + "lID FROM AOIs WHERE (TrialID = @Param1) AND (ShapeGroup = @Param2)";
      this._commandCollection[3].CommandType = CommandType.Text;
      this._commandCollection[3].Parameters.Add(
        new SQLiteParameter(
          "@Param1",
          DbType.Int32,
          4,
          ParameterDirection.Input,
          0,
          0,
          "TrialID",
          DataRowVersion.Current,
          false,
          null));
      this._commandCollection[3].Parameters.Add(
        new SQLiteParameter(
          "@Param2",
          DbType.String,
          50,
          ParameterDirection.Input,
          0,
          0,
          "ShapeGroup",
          DataRowVersion.Current,
          false,
          null));
      this._commandCollection[4] = new SQLiteCommand();
      this._commandCollection[4].Connection = this.Connection;
      this._commandCollection[4].CommandText =
        "SELECT ID, ShapeGroup, ShapeName, ShapeNumPts, ShapePts, ShapeType, SlideNr, Tria"
        + "lID FROM AOIs WHERE (TrialID = @Param1) AND (ShapeName = @Param2)";
      this._commandCollection[4].CommandType = CommandType.Text;
      this._commandCollection[4].Parameters.Add(
        new SQLiteParameter(
          "@Param1",
          DbType.Int32,
          4,
          ParameterDirection.Input,
          0,
          0,
          "TrialID",
          DataRowVersion.Current,
          false,
          null));
      this._commandCollection[4].Parameters.Add(
        new SQLiteParameter(
          "@Param2",
          DbType.String,
          50,
          ParameterDirection.Input,
          0,
          0,
          "ShapeName",
          DataRowVersion.Current,
          false,
          null));
      this._commandCollection[5] = new SQLiteCommand();
      this._commandCollection[5].Connection = this.Connection;
      this._commandCollection[5].CommandText =
        "SELECT ID, ShapeGroup, ShapeName, ShapeNumPts, ShapePts, ShapeType, SlideNr, Tria"
        + "lID FROM AOIs GROUP BY ShapeGroup, TrialID HAVING (TrialID = @Param1)";
      this._commandCollection[5].CommandType = CommandType.Text;
      this._commandCollection[5].Parameters.Add(
        new SQLiteParameter(
          "@Param1",
          DbType.Int32,
          4,
          ParameterDirection.Input,
          0,
          0,
          "TrialID",
          DataRowVersion.Current,
          false,
          null));
      this._commandCollection[6] = new SQLiteCommand();
      this._commandCollection[6].Connection = this.Connection;
      this._commandCollection[6].CommandText =
        "SELECT ID, ShapeGroup, ShapeName, ShapeNumPts, ShapePts, ShapeType, SlideNr, Tria"
        + "lID FROM AOIs WHERE (TrialID = @Param1) AND (ShapeGroup = \'Target\')";
      this._commandCollection[6].CommandType = CommandType.Text;
      this._commandCollection[6].Parameters.Add(
        new SQLiteParameter(
          "@Param1",
          DbType.Int32,
          4,
          ParameterDirection.Input,
          0,
          0,
          "TrialID",
          DataRowVersion.Current,
          false,
          null));
    }

    /// <summary>
    ///   Initializes the connection.
    /// </summary>
    private void InitConnection()
    {
      this._connection = new SQLiteConnection();
      this._connection.ConnectionString =
        global::Ogama.Properties.Settings.Default.OgamaDatabaseTemplateConnectionString;
    }

    #endregion
  }

  /// <summary>
  ///   Represents the connection and commands used to retrieve and save data.
  /// </summary>
  public class SQLiteTadShapeGroups : Component
  {
    #region Fields

    /// <summary>
    ///   The _adapter
    /// </summary>
    private SQLiteDataAdapter _adapter;

    /// <summary>
    ///   The _command collection
    /// </summary>
    private SQLiteCommand[] _commandCollection;

    /// <summary>
    ///   The _connection
    /// </summary>
    private SQLiteConnection _connection;

    #endregion

    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="SQLiteTadShapeGroups" /> class.
    /// </summary>
    public SQLiteTadShapeGroups()
    {
      this.ClearBeforeFill = true;
    }

    #endregion

    #region Public Properties

    /// <summary>
    ///   Gets or sets a value indicating whether [clear before fill].
    /// </summary>
    /// <value><c>true</c> if [clear before fill]; otherwise, <c>false</c>.</value>
    public bool ClearBeforeFill { get; set; }

    #endregion

    #region Properties

    /// <summary>
    ///   Gets or sets the connection.
    /// </summary>
    /// <value>The connection.</value>
    internal SQLiteConnection Connection
    {
      get
      {
        if (this._connection == null)
        {
          this.InitConnection();
        }

        return this._connection;
      }

      set
      {
        this._connection = value;
        if (this.Adapter.InsertCommand != null)
        {
          this.Adapter.InsertCommand.Connection = value;
        }

        if (this.Adapter.DeleteCommand != null)
        {
          this.Adapter.DeleteCommand.Connection = value;
        }

        if (this.Adapter.UpdateCommand != null)
        {
          this.Adapter.UpdateCommand.Connection = value;
        }

        for (int i = 0; i < this.CommandCollection.Length; i = (i + 1))
        {
          if ((this.CommandCollection[i] != null))
          {
            this.CommandCollection[i].Connection = value;
          }
        }
      }
    }

    /// <summary>
    ///   Gets the command collection.
    /// </summary>
    /// <value>The command collection.</value>
    protected SQLiteCommand[] CommandCollection
    {
      get
      {
        if (this._commandCollection == null)
        {
          this.InitCommandCollection();
        }

        return this._commandCollection;
      }
    }

    /// <summary>
    ///   Gets the adapter.
    /// </summary>
    /// <value>The adapter.</value>
    private SQLiteDataAdapter Adapter
    {
      get
      {
        if (this._adapter == null)
        {
          this.InitAdapter();
        }

        return this._adapter;
      }
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// Deletes the specified original_ identifier.
    /// </summary>
    /// <param name="Original_ID">
    /// The original_ identifier.
    /// </param>
    /// <param name="Original_ShapeGroup">
    /// The original_ shape group.
    /// </param>
    /// <returns>
    /// System.Int32.
    /// </returns>
    /// <exception cref="System.ArgumentNullException">
    /// Original_ShapeGroup
    /// </exception>
    [DataObjectMethodAttribute(DataObjectMethodType.Delete,
     true)]
    public virtual int Delete(int Original_ID, string Original_ShapeGroup)
    {
      this.Adapter.DeleteCommand.Parameters[0].Value = Original_ID;
      if (Original_ShapeGroup == null)
      {
        throw new ArgumentNullException("Original_ShapeGroup");
      }

      this.Adapter.DeleteCommand.Parameters[1].Value = Original_ShapeGroup;
      ConnectionState previousConnectionState = this.Adapter.DeleteCommand.Connection.State;
      if ((this.Adapter.DeleteCommand.Connection.State & ConnectionState.Open)
           != ConnectionState.Open)
      {
        this.Adapter.DeleteCommand.Connection.Open();
      }

      try
      {
        int returnValue = this.Adapter.DeleteCommand.ExecuteNonQuery();
        return returnValue;
      }
      finally
      {
        if (previousConnectionState == ConnectionState.Closed)
        {
          this.Adapter.DeleteCommand.Connection.Close();
        }
      }
    }

    /// <summary>
    /// Fills the specified data table.
    /// </summary>
    /// <param name="dataTable">
    /// The data table.
    /// </param>
    /// <returns>
    /// System.Int32.
    /// </returns>
    [DataObjectMethodAttribute(DataObjectMethodType.Fill,
     true)]
    public virtual int Fill(SQLiteOgamaDataSet.ShapeGroupsDataTable dataTable)
    {
      this.Adapter.SelectCommand = this.CommandCollection[0];
      if (this.ClearBeforeFill)
      {
        dataTable.Clear();
      }

      int returnValue = this.Adapter.Fill(dataTable);
      return returnValue;
    }

    /// <summary>
    ///   Gets the data.
    /// </summary>
    /// <returns>SQLiteOgamaDataSet.ShapeGroupsDataTable.</returns>
    [DataObjectMethodAttribute(DataObjectMethodType.Select,
     true)]
    public virtual SQLiteOgamaDataSet.ShapeGroupsDataTable GetData()
    {
      this.Adapter.SelectCommand = this.CommandCollection[0];
      var dataTable = new SQLiteOgamaDataSet.ShapeGroupsDataTable();
      this.Adapter.Fill(dataTable);
      return dataTable;
    }

    /// <summary>
    /// Gets the data by group.
    /// </summary>
    /// <param name="Param1">
    /// The param1.
    /// </param>
    /// <returns>
    /// SQLiteOgamaDataSet.ShapeGroupsDataTable.
    /// </returns>
    /// <exception cref="System.ArgumentNullException">
    /// Param1
    /// </exception>
    [DataObjectMethodAttribute(DataObjectMethodType.Select,
     false)]
    public virtual SQLiteOgamaDataSet.ShapeGroupsDataTable GetDataByGroup(string Param1)
    {
      this.Adapter.SelectCommand = this.CommandCollection[1];
      if (Param1 == null)
      {
        throw new ArgumentNullException("Param1");
      }

      this.Adapter.SelectCommand.Parameters[0].Value = Param1;
      var dataTable = new SQLiteOgamaDataSet.ShapeGroupsDataTable();
      this.Adapter.Fill(dataTable);
      return dataTable;
    }

    /// <summary>
    /// Inserts the specified shape group.
    /// </summary>
    /// <param name="ShapeGroup">
    /// The shape group.
    /// </param>
    /// <returns>
    /// System.Int32.
    /// </returns>
    /// <exception cref="System.ArgumentNullException">
    /// ShapeGroup
    /// </exception>
    [DataObjectMethodAttribute(DataObjectMethodType.Insert,
     true)]
    public virtual int Insert(string ShapeGroup)
    {
      if (ShapeGroup == null)
      {
        throw new ArgumentNullException("ShapeGroup");
      }

      this.Adapter.InsertCommand.Parameters[0].Value = ShapeGroup;
      ConnectionState previousConnectionState = this.Adapter.InsertCommand.Connection.State;
      if ((this.Adapter.InsertCommand.Connection.State & ConnectionState.Open)
           != ConnectionState.Open)
      {
        this.Adapter.InsertCommand.Connection.Open();
      }

      try
      {
        int returnValue = this.Adapter.InsertCommand.ExecuteNonQuery();
        return returnValue;
      }
      finally
      {
        if (previousConnectionState == ConnectionState.Closed)
        {
          this.Adapter.InsertCommand.Connection.Close();
        }
      }
    }

    /// <summary>
    /// Updates the specified data table.
    /// </summary>
    /// <param name="dataTable">
    /// The data table.
    /// </param>
    /// <returns>
    /// System.Int32.
    /// </returns>
    public virtual int Update(SQLiteOgamaDataSet.ShapeGroupsDataTable dataTable)
    {
      return this.Adapter.Update(dataTable);
    }

    /// <summary>
    /// Updates the specified data set.
    /// </summary>
    /// <param name="dataSet">
    /// The data set.
    /// </param>
    /// <returns>
    /// System.Int32.
    /// </returns>
    public virtual int Update(SQLiteOgamaDataSet dataSet)
    {
      return this.Adapter.Update(dataSet, "ShapeGroups");
    }

    /// <summary>
    /// Updates the specified data row.
    /// </summary>
    /// <param name="dataRow">
    /// The data row.
    /// </param>
    /// <returns>
    /// System.Int32.
    /// </returns>
    public virtual int Update(DataRow dataRow)
    {
      return this.Adapter.Update(new[] { dataRow });
    }

    /// <summary>
    /// Updates the specified data rows.
    /// </summary>
    /// <param name="dataRows">
    /// The data rows.
    /// </param>
    /// <returns>
    /// System.Int32.
    /// </returns>
    public virtual int Update(DataRow[] dataRows)
    {
      return this.Adapter.Update(dataRows);
    }

    /// <summary>
    /// Updates the specified shape group.
    /// </summary>
    /// <param name="ShapeGroup">
    /// The shape group.
    /// </param>
    /// <param name="Original_ID">
    /// The original_ identifier.
    /// </param>
    /// <param name="Original_ShapeGroup">
    /// The original_ shape group.
    /// </param>
    /// <param name="ID">
    /// The identifier.
    /// </param>
    /// <returns>
    /// System.Int32.
    /// </returns>
    /// <exception cref="System.ArgumentNullException">
    /// ShapeGroup
    ///   or
    ///   Original_ShapeGroup
    /// </exception>
    [DataObjectMethodAttribute(DataObjectMethodType.Update,
     true)]
    public virtual int Update(string ShapeGroup, int Original_ID, string Original_ShapeGroup, int ID)
    {
      if (ShapeGroup == null)
      {
        throw new ArgumentNullException("ShapeGroup");
      }

      this.Adapter.UpdateCommand.Parameters[0].Value = ShapeGroup;
      this.Adapter.UpdateCommand.Parameters[1].Value = Original_ID;
      if (Original_ShapeGroup == null)
      {
        throw new ArgumentNullException("Original_ShapeGroup");
      }

      this.Adapter.UpdateCommand.Parameters[2].Value = Original_ShapeGroup;
      this.Adapter.UpdateCommand.Parameters[3].Value = ID;
      ConnectionState previousConnectionState = this.Adapter.UpdateCommand.Connection.State;
      if ((this.Adapter.UpdateCommand.Connection.State & ConnectionState.Open)
           != ConnectionState.Open)
      {
        this.Adapter.UpdateCommand.Connection.Open();
      }

      try
      {
        int returnValue = this.Adapter.UpdateCommand.ExecuteNonQuery();
        return returnValue;
      }
      finally
      {
        if (previousConnectionState == ConnectionState.Closed)
        {
          this.Adapter.UpdateCommand.Connection.Close();
        }
      }
    }

    /// <summary>
    /// Updates the specified shape group.
    /// </summary>
    /// <param name="ShapeGroup">
    /// The shape group.
    /// </param>
    /// <param name="Original_ID">
    /// The original_ identifier.
    /// </param>
    /// <param name="Original_ShapeGroup">
    /// The original_ shape group.
    /// </param>
    /// <returns>
    /// System.Int32.
    /// </returns>
    [DataObjectMethodAttribute(DataObjectMethodType.Update,
     true)]
    public virtual int Update(string ShapeGroup, int Original_ID, string Original_ShapeGroup)
    {
      return this.Update(ShapeGroup, Original_ID, Original_ShapeGroup, Original_ID);
    }

    #endregion

    #region Methods

    /// <summary>
    ///   Initializes the adapter.
    /// </summary>
    private void InitAdapter()
    {
      this._adapter = new SQLiteDataAdapter();
      var tableMapping = new DataTableMapping();
      tableMapping.SourceTable = "Table";
      tableMapping.DataSetTable = "ShapeGroups";
      tableMapping.ColumnMappings.Add("ID", "ID");
      tableMapping.ColumnMappings.Add("ShapeGroup", "ShapeGroup");
      this._adapter.TableMappings.Add(tableMapping);
      this._adapter.DeleteCommand = new SQLiteCommand();
      this._adapter.DeleteCommand.Connection = this.Connection;
      this._adapter.DeleteCommand.CommandText =
        "DELETE FROM [ShapeGroups] WHERE (([ID] = @Original_ID) AND ([ShapeGroup] = @Origi" + "nal_ShapeGroup))";
      this._adapter.DeleteCommand.CommandType = CommandType.Text;
      this._adapter.DeleteCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_ID",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "ID",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.DeleteCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_ShapeGroup",
          DbType.String,
          0,
          ParameterDirection.Input,
          0,
          0,
          "ShapeGroup",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.InsertCommand = new SQLiteCommand();
      this._adapter.InsertCommand.Connection = this.Connection;
      this._adapter.InsertCommand.CommandText =
        "INSERT INTO [ShapeGroups] ([ShapeGroup]) VALUES (@ShapeGroup);\r\nSELECT ID, ShapeG"
        + "roup FROM ShapeGroups WHERE (ID = last_insert_rowid())";
      this._adapter.InsertCommand.CommandType = CommandType.Text;
      this._adapter.InsertCommand.Parameters.Add(
        new SQLiteParameter(
          "@ShapeGroup",
          DbType.String,
          0,
          ParameterDirection.Input,
          0,
          0,
          "ShapeGroup",
          DataRowVersion.Current,
          false,
          null));
      this._adapter.UpdateCommand = new SQLiteCommand();
      this._adapter.UpdateCommand.Connection = this.Connection;
      this._adapter.UpdateCommand.CommandText =
        "UPDATE [ShapeGroups] SET [ShapeGroup] = @ShapeGroup WHERE (([ID] = @Original_ID) "
        + "AND ([ShapeGroup] = @Original_ShapeGroup));\r\nSELECT ID, ShapeGroup FROM ShapeGro" + "ups WHERE (ID = @ID)";
      this._adapter.UpdateCommand.CommandType = CommandType.Text;
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@ShapeGroup",
          DbType.String,
          0,
          ParameterDirection.Input,
          0,
          0,
          "ShapeGroup",
          DataRowVersion.Current,
          false,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_ID",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "ID",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_ShapeGroup",
          DbType.String,
          0,
          ParameterDirection.Input,
          0,
          0,
          "ShapeGroup",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@ID",
          DbType.Int32,
          4,
          ParameterDirection.Input,
          0,
          0,
          "ID",
          DataRowVersion.Current,
          false,
          null));
    }

    /// <summary>
    ///   Initializes the command collection.
    /// </summary>
    private void InitCommandCollection()
    {
      this._commandCollection = new SQLiteCommand[2];
      this._commandCollection[0] = new SQLiteCommand();
      this._commandCollection[0].Connection = this.Connection;
      this._commandCollection[0].CommandText = "SELECT     ID, ShapeGroup\r\nFROM         ShapeGroups";
      this._commandCollection[0].CommandType = CommandType.Text;
      this._commandCollection[1] = new SQLiteCommand();
      this._commandCollection[1].Connection = this.Connection;
      this._commandCollection[1].CommandText = "SELECT ID, ShapeGroup FROM ShapeGroups WHERE (ShapeGroup = @Param1)";
      this._commandCollection[1].CommandType = CommandType.Text;
      this._commandCollection[1].Parameters.Add(
        new SQLiteParameter(
          "@Param1",
          DbType.String,
          50,
          ParameterDirection.Input,
          0,
          0,
          "ShapeGroup",
          DataRowVersion.Current,
          false,
          null));
    }

    /// <summary>
    ///   Initializes the connection.
    /// </summary>
    private void InitConnection()
    {
      this._connection = new SQLiteConnection();
      this._connection.ConnectionString =
        global::Ogama.Properties.Settings.Default.OgamaDatabaseTemplateConnectionString;
    }

    #endregion
  }

  /// <summary>
  ///   Represents the connection and commands used to retrieve and save data.
  /// </summary>
  public class SQLiteTadGazeFixations : Component
  {
    #region Fields

    /// <summary>
    ///   The _adapter
    /// </summary>
    private SQLiteDataAdapter _adapter;

    /// <summary>
    ///   The _command collection
    /// </summary>
    private SQLiteCommand[] _commandCollection;

    /// <summary>
    ///   The _connection
    /// </summary>
    private SQLiteConnection _connection;

    #endregion

    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="SQLiteTadGazeFixations" /> class.
    /// </summary>
    public SQLiteTadGazeFixations()
    {
      this.ClearBeforeFill = true;
    }

    #endregion

    #region Public Properties

    /// <summary>
    ///   Gets or sets a value indicating whether [clear before fill].
    /// </summary>
    /// <value><c>true</c> if [clear before fill]; otherwise, <c>false</c>.</value>
    public bool ClearBeforeFill { get; set; }

    #endregion

    #region Properties

    /// <summary>
    ///   Gets or sets the connection.
    /// </summary>
    /// <value>The connection.</value>
    internal SQLiteConnection Connection
    {
      get
      {
        if (this._connection == null)
        {
          this.InitConnection();
        }

        return this._connection;
      }

      set
      {
        this._connection = value;
        if (this.Adapter.InsertCommand != null)
        {
          this.Adapter.InsertCommand.Connection = value;
        }

        if (this.Adapter.DeleteCommand != null)
        {
          this.Adapter.DeleteCommand.Connection = value;
        }

        if (this.Adapter.UpdateCommand != null)
        {
          this.Adapter.UpdateCommand.Connection = value;
        }

        for (int i = 0; i < this.CommandCollection.Length; i = (i + 1))
        {
          if ((this.CommandCollection[i] != null))
          {
            this.CommandCollection[i].Connection = value;
          }
        }
      }
    }

    /// <summary>
    ///   Gets the command collection.
    /// </summary>
    /// <value>The command collection.</value>
    protected SQLiteCommand[] CommandCollection
    {
      get
      {
        if (this._commandCollection == null)
        {
          this.InitCommandCollection();
        }

        return this._commandCollection;
      }
    }

    /// <summary>
    ///   Gets the adapter.
    /// </summary>
    /// <value>The adapter.</value>
    private SQLiteDataAdapter Adapter
    {
      get
      {
        if (this._adapter == null)
        {
          this.InitAdapter();
        }

        return this._adapter;
      }
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// Deletes the specified original_ subject name.
    /// </summary>
    /// <param name="Original_SubjectName">
    /// Name of the original_ subject.
    /// </param>
    /// <param name="Original_TrialID">
    /// The original_ trial identifier.
    /// </param>
    /// <param name="Original_TrialSequence">
    /// The original_ trial sequence.
    /// </param>
    /// <param name="Original_CountInTrial">
    /// The original_ count in trial.
    /// </param>
    /// <param name="Original_StartTime">
    /// The original_ start time.
    /// </param>
    /// <param name="Original_Length">
    /// Length of the original_.
    /// </param>
    /// <param name="Original_PosX">
    /// The original_ position x.
    /// </param>
    /// <param name="Original_PosY">
    /// The original_ position y.
    /// </param>
    /// <param name="Original_ID">
    /// The original_ identifier.
    /// </param>
    /// <returns>
    /// System.Int32.
    /// </returns>
    /// <exception cref="System.ArgumentNullException">
    /// Original_SubjectName
    /// </exception>
    [DataObjectMethodAttribute(DataObjectMethodType.Delete,
     true)]
    public virtual int Delete(
      string Original_SubjectName,
      int Original_TrialID,
      int Original_TrialSequence,
      int? Original_CountInTrial,
      long? Original_StartTime,
      int? Original_Length,
      double? Original_PosX,
      double? Original_PosY,
      long Original_ID)
    {
      if (Original_SubjectName == null)
      {
        throw new ArgumentNullException("Original_SubjectName");
      }

      this.Adapter.DeleteCommand.Parameters[0].Value = Original_SubjectName;
      this.Adapter.DeleteCommand.Parameters[1].Value = Original_TrialID;
      this.Adapter.DeleteCommand.Parameters[2].Value = Original_TrialSequence;
      if (Original_CountInTrial.HasValue)
      {
        this.Adapter.DeleteCommand.Parameters[3].Value = 0;
        this.Adapter.DeleteCommand.Parameters[4].Value = Original_CountInTrial.Value;
      }
      else
      {
        this.Adapter.DeleteCommand.Parameters[3].Value = 1;
        this.Adapter.DeleteCommand.Parameters[4].Value = DBNull.Value;
      }

      if (Original_StartTime.HasValue)
      {
        this.Adapter.DeleteCommand.Parameters[5].Value = 0;
        this.Adapter.DeleteCommand.Parameters[6].Value = Original_StartTime.Value;
      }
      else
      {
        this.Adapter.DeleteCommand.Parameters[5].Value = 1;
        this.Adapter.DeleteCommand.Parameters[6].Value = DBNull.Value;
      }

      if (Original_Length.HasValue)
      {
        this.Adapter.DeleteCommand.Parameters[7].Value = 0;
        this.Adapter.DeleteCommand.Parameters[8].Value = Original_Length.Value;
      }
      else
      {
        this.Adapter.DeleteCommand.Parameters[7].Value = 1;
        this.Adapter.DeleteCommand.Parameters[8].Value = DBNull.Value;
      }

      if (Original_PosX.HasValue)
      {
        this.Adapter.DeleteCommand.Parameters[9].Value = 0;
        this.Adapter.DeleteCommand.Parameters[10].Value = Original_PosX.Value;
      }
      else
      {
        this.Adapter.DeleteCommand.Parameters[9].Value = 1;
        this.Adapter.DeleteCommand.Parameters[10].Value = DBNull.Value;
      }

      if (Original_PosY.HasValue)
      {
        this.Adapter.DeleteCommand.Parameters[11].Value = 0;
        this.Adapter.DeleteCommand.Parameters[12].Value = Original_PosY.Value;
      }
      else
      {
        this.Adapter.DeleteCommand.Parameters[11].Value = 1;
        this.Adapter.DeleteCommand.Parameters[12].Value = DBNull.Value;
      }

      this.Adapter.DeleteCommand.Parameters[13].Value = Original_ID;
      ConnectionState previousConnectionState = this.Adapter.DeleteCommand.Connection.State;
      if ((this.Adapter.DeleteCommand.Connection.State & ConnectionState.Open)
           != ConnectionState.Open)
      {
        this.Adapter.DeleteCommand.Connection.Open();
      }

      try
      {
        int returnValue = this.Adapter.DeleteCommand.ExecuteNonQuery();
        return returnValue;
      }
      finally
      {
        if (previousConnectionState == ConnectionState.Closed)
        {
          this.Adapter.DeleteCommand.Connection.Close();
        }
      }
    }

    /// <summary>
    ///   Deletes all.
    /// </summary>
    /// <returns>System.Int32.</returns>
    [DataObjectMethodAttribute(DataObjectMethodType.Delete,
     false)]
    public virtual int DeleteAll()
    {
      SQLiteCommand command = this.CommandCollection[1];
      ConnectionState previousConnectionState = command.Connection.State;
      if ((command.Connection.State & ConnectionState.Open)
           != ConnectionState.Open)
      {
        command.Connection.Open();
      }

      int returnValue;
      try
      {
        returnValue = command.ExecuteNonQuery();
      }
      finally
      {
        if (previousConnectionState == ConnectionState.Closed)
        {
          command.Connection.Close();
        }
      }

      return returnValue;
    }

    /// <summary>
    /// Deletes the by subject.
    /// </summary>
    /// <param name="Original_SubjectName">
    /// Name of the original_ subject.
    /// </param>
    /// <returns>
    /// System.Int32.
    /// </returns>
    /// <exception cref="System.ArgumentNullException">
    /// Original_SubjectName
    /// </exception>
    [DataObjectMethodAttribute(DataObjectMethodType.Delete,
     false)]
    public virtual int DeleteBySubject(string Original_SubjectName)
    {
      SQLiteCommand command = this.CommandCollection[2];
      if (Original_SubjectName == null)
      {
        throw new ArgumentNullException("Original_SubjectName");
      }

      command.Parameters[0].Value = Original_SubjectName;
      ConnectionState previousConnectionState = command.Connection.State;
      if ((command.Connection.State & ConnectionState.Open)
           != ConnectionState.Open)
      {
        command.Connection.Open();
      }

      int returnValue;
      try
      {
        returnValue = command.ExecuteNonQuery();
      }
      finally
      {
        if (previousConnectionState == ConnectionState.Closed)
        {
          command.Connection.Close();
        }
      }

      return returnValue;
    }

    /// <summary>
    /// Fills the specified data table.
    /// </summary>
    /// <param name="dataTable">
    /// The data table.
    /// </param>
    /// <returns>
    /// System.Int32.
    /// </returns>
    [DataObjectMethodAttribute(DataObjectMethodType.Fill,
     true)]
    public virtual int Fill(SQLiteOgamaDataSet.GazeFixationsDataTable dataTable)
    {
      this.Adapter.SelectCommand = this.CommandCollection[0];
      if (this.ClearBeforeFill)
      {
        dataTable.Clear();
      }

      int returnValue = this.Adapter.Fill(dataTable);
      return returnValue;
    }

    /// <summary>
    ///   Gets the data.
    /// </summary>
    /// <returns>SQLiteOgamaDataSet.GazeFixationsDataTable.</returns>
    [DataObjectMethodAttribute(DataObjectMethodType.Select,
     true)]
    public virtual SQLiteOgamaDataSet.GazeFixationsDataTable GetData()
    {
      this.Adapter.SelectCommand = this.CommandCollection[0];
      var dataTable = new SQLiteOgamaDataSet.GazeFixationsDataTable();
      this.Adapter.Fill(dataTable);
      return dataTable;
    }

    /// <summary>
    /// Gets the data by subject.
    /// </summary>
    /// <param name="Param1">
    /// The param1.
    /// </param>
    /// <returns>
    /// SQLiteOgamaDataSet.GazeFixationsDataTable.
    /// </returns>
    /// <exception cref="System.ArgumentNullException">
    /// Param1
    /// </exception>
    [DataObjectMethodAttribute(DataObjectMethodType.Select,
     false)]
    public virtual SQLiteOgamaDataSet.GazeFixationsDataTable GetDataBySubject(string Param1)
    {
      this.Adapter.SelectCommand = this.CommandCollection[3];
      if (Param1 == null)
      {
        throw new ArgumentNullException("Param1");
      }

      this.Adapter.SelectCommand.Parameters[0].Value = Param1;
      var dataTable = new SQLiteOgamaDataSet.GazeFixationsDataTable();
      this.Adapter.Fill(dataTable);
      return dataTable;
    }

    /// <summary>
    /// Gets the data by subject and sequence.
    /// </summary>
    /// <param name="Param1">
    /// The param1.
    /// </param>
    /// <param name="Param2">
    /// The param2.
    /// </param>
    /// <returns>
    /// SQLiteOgamaDataSet.GazeFixationsDataTable.
    /// </returns>
    /// <exception cref="System.ArgumentNullException">
    /// Param1
    /// </exception>
    [DataObjectMethodAttribute(DataObjectMethodType.Select,
     false)]
    public virtual SQLiteOgamaDataSet.GazeFixationsDataTable GetDataBySubjectAndSequence(string Param1, int Param2)
    {
      this.Adapter.SelectCommand = this.CommandCollection[4];
      if (Param1 == null)
      {
        throw new ArgumentNullException("Param1");
      }

      this.Adapter.SelectCommand.Parameters[0].Value = Param1;
      this.Adapter.SelectCommand.Parameters[1].Value = Param2;
      var dataTable = new SQLiteOgamaDataSet.GazeFixationsDataTable();
      this.Adapter.Fill(dataTable);
      return dataTable;
    }

    /// <summary>
    /// Gets the data by subject and trial identifier.
    /// </summary>
    /// <param name="Param1">
    /// The param1.
    /// </param>
    /// <param name="Param2">
    /// The param2.
    /// </param>
    /// <returns>
    /// SQLiteOgamaDataSet.GazeFixationsDataTable.
    /// </returns>
    /// <exception cref="System.ArgumentNullException">
    /// Param1
    /// </exception>
    [DataObjectMethodAttribute(DataObjectMethodType.Select,
     false)]
    public virtual SQLiteOgamaDataSet.GazeFixationsDataTable GetDataBySubjectAndTrialID(string Param1, int Param2)
    {
      this.Adapter.SelectCommand = this.CommandCollection[5];
      if (Param1 == null)
      {
        throw new ArgumentNullException("Param1");
      }

      this.Adapter.SelectCommand.Parameters[0].Value = Param1;
      this.Adapter.SelectCommand.Parameters[1].Value = Param2;
      var dataTable = new SQLiteOgamaDataSet.GazeFixationsDataTable();
      this.Adapter.Fill(dataTable);
      return dataTable;
    }

    /// <summary>
    /// Gets the data by trial identifier.
    /// </summary>
    /// <param name="Param1">
    /// The param1.
    /// </param>
    /// <returns>
    /// SQLiteOgamaDataSet.GazeFixationsDataTable.
    /// </returns>
    [DataObjectMethodAttribute(DataObjectMethodType.Select,
     false)]
    public virtual SQLiteOgamaDataSet.GazeFixationsDataTable GetDataByTrialID(int Param1)
    {
      this.Adapter.SelectCommand = this.CommandCollection[6];
      this.Adapter.SelectCommand.Parameters[0].Value = Param1;
      var dataTable = new SQLiteOgamaDataSet.GazeFixationsDataTable();
      this.Adapter.Fill(dataTable);
      return dataTable;
    }

    /// <summary>
    /// Gets the maximum start time of trial.
    /// </summary>
    /// <param name="Param1">
    /// The param1.
    /// </param>
    /// <returns>
    /// System.Object.
    /// </returns>
    public virtual object GetMaxStartTimeOfTrial(int Param1)
    {
      SQLiteCommand command = this.CommandCollection[7];
      command.Parameters[0].Value = Param1;
      ConnectionState previousConnectionState = command.Connection.State;
      if ((command.Connection.State & ConnectionState.Open)
           != ConnectionState.Open)
      {
        command.Connection.Open();
      }

      object returnValue;
      try
      {
        returnValue = command.ExecuteScalar();
      }
      finally
      {
        if (previousConnectionState == ConnectionState.Closed)
        {
          command.Connection.Close();
        }
      }

      if ((returnValue == null) || (returnValue.GetType() == typeof(DBNull)))
      {
        return null;
      }

      return returnValue;
    }

    /// <summary>
    /// Inserts the specified subject name.
    /// </summary>
    /// <param name="SubjectName">
    /// Name of the subject.
    /// </param>
    /// <param name="TrialID">
    /// The trial identifier.
    /// </param>
    /// <param name="TrialSequence">
    /// The trial sequence.
    /// </param>
    /// <param name="CountInTrial">
    /// The count in trial.
    /// </param>
    /// <param name="StartTime">
    /// The start time.
    /// </param>
    /// <param name="Length">
    /// The length.
    /// </param>
    /// <param name="PosX">
    /// The position x.
    /// </param>
    /// <param name="PosY">
    /// The position y.
    /// </param>
    /// <returns>
    /// System.Int32.
    /// </returns>
    /// <exception cref="System.ArgumentNullException">
    /// SubjectName
    /// </exception>
    [DataObjectMethodAttribute(DataObjectMethodType.Insert,
     true)]
    public virtual int Insert(
      string SubjectName,
      int TrialID,
      int TrialSequence,
      int? CountInTrial,
      long? StartTime,
      int? Length,
      double? PosX,
      double? PosY)
    {
      if (SubjectName == null)
      {
        throw new ArgumentNullException("SubjectName");
      }

      this.Adapter.InsertCommand.Parameters[0].Value = SubjectName;
      this.Adapter.InsertCommand.Parameters[1].Value = TrialID;
      this.Adapter.InsertCommand.Parameters[2].Value = TrialSequence;
      if (CountInTrial.HasValue)
      {
        this.Adapter.InsertCommand.Parameters[3].Value = CountInTrial.Value;
      }
      else
      {
        this.Adapter.InsertCommand.Parameters[3].Value = DBNull.Value;
      }

      if (StartTime.HasValue)
      {
        this.Adapter.InsertCommand.Parameters[4].Value = StartTime.Value;
      }
      else
      {
        this.Adapter.InsertCommand.Parameters[4].Value = DBNull.Value;
      }

      if (Length.HasValue)
      {
        this.Adapter.InsertCommand.Parameters[5].Value = Length.Value;
      }
      else
      {
        this.Adapter.InsertCommand.Parameters[5].Value = DBNull.Value;
      }

      if (PosX.HasValue)
      {
        this.Adapter.InsertCommand.Parameters[6].Value = PosX.Value;
      }
      else
      {
        this.Adapter.InsertCommand.Parameters[6].Value = DBNull.Value;
      }

      if (PosY.HasValue)
      {
        this.Adapter.InsertCommand.Parameters[7].Value = PosY.Value;
      }
      else
      {
        this.Adapter.InsertCommand.Parameters[7].Value = DBNull.Value;
      }

      ConnectionState previousConnectionState = this.Adapter.InsertCommand.Connection.State;
      if ((this.Adapter.InsertCommand.Connection.State & ConnectionState.Open)
           != ConnectionState.Open)
      {
        this.Adapter.InsertCommand.Connection.Open();
      }

      try
      {
        int returnValue = this.Adapter.InsertCommand.ExecuteNonQuery();
        return returnValue;
      }
      finally
      {
        if (previousConnectionState == ConnectionState.Closed)
        {
          this.Adapter.InsertCommand.Connection.Close();
        }
      }
    }

    /// <summary>
    /// Updates the specified data table.
    /// </summary>
    /// <param name="dataTable">
    /// The data table.
    /// </param>
    /// <returns>
    /// System.Int32.
    /// </returns>
    public virtual int Update(SQLiteOgamaDataSet.GazeFixationsDataTable dataTable)
    {
      return this.Adapter.Update(dataTable);
    }

    /// <summary>
    /// Updates the specified data set.
    /// </summary>
    /// <param name="dataSet">
    /// The data set.
    /// </param>
    /// <returns>
    /// System.Int32.
    /// </returns>
    public virtual int Update(SQLiteOgamaDataSet dataSet)
    {
      return this.Adapter.Update(dataSet, "GazeFixations");
    }

    /// <summary>
    /// Updates the specified data row.
    /// </summary>
    /// <param name="dataRow">
    /// The data row.
    /// </param>
    /// <returns>
    /// System.Int32.
    /// </returns>
    public virtual int Update(DataRow dataRow)
    {
      return this.Adapter.Update(new[] { dataRow });
    }

    /// <summary>
    /// Updates the specified data rows.
    /// </summary>
    /// <param name="dataRows">
    /// The data rows.
    /// </param>
    /// <returns>
    /// System.Int32.
    /// </returns>
    public virtual int Update(DataRow[] dataRows)
    {
      return this.Adapter.Update(dataRows);
    }

    /// <summary>
    /// Updates the specified subject name.
    /// </summary>
    /// <param name="SubjectName">
    /// Name of the subject.
    /// </param>
    /// <param name="TrialID">
    /// The trial identifier.
    /// </param>
    /// <param name="TrialSequence">
    /// The trial sequence.
    /// </param>
    /// <param name="CountInTrial">
    /// The count in trial.
    /// </param>
    /// <param name="StartTime">
    /// The start time.
    /// </param>
    /// <param name="Length">
    /// The length.
    /// </param>
    /// <param name="PosX">
    /// The position x.
    /// </param>
    /// <param name="PosY">
    /// The position y.
    /// </param>
    /// <param name="Original_SubjectName">
    /// Name of the original_ subject.
    /// </param>
    /// <param name="Original_TrialID">
    /// The original_ trial identifier.
    /// </param>
    /// <param name="Original_TrialSequence">
    /// The original_ trial sequence.
    /// </param>
    /// <param name="Original_CountInTrial">
    /// The original_ count in trial.
    /// </param>
    /// <param name="Original_StartTime">
    /// The original_ start time.
    /// </param>
    /// <param name="Original_Length">
    /// Length of the original_.
    /// </param>
    /// <param name="Original_PosX">
    /// The original_ position x.
    /// </param>
    /// <param name="Original_PosY">
    /// The original_ position y.
    /// </param>
    /// <param name="Original_ID">
    /// The original_ identifier.
    /// </param>
    /// <param name="ID">
    /// The identifier.
    /// </param>
    /// <returns>
    /// System.Int32.
    /// </returns>
    /// <exception cref="System.ArgumentNullException">
    /// SubjectName
    ///   or
    ///   Original_SubjectName
    /// </exception>
    [DataObjectMethodAttribute(DataObjectMethodType.Update,
     true)]
    public virtual int Update(
      string SubjectName,
      int TrialID,
      int TrialSequence,
      int? CountInTrial,
      long? StartTime,
      int? Length,
      double? PosX,
      double? PosY,
      string Original_SubjectName,
      int Original_TrialID,
      int Original_TrialSequence,
      int? Original_CountInTrial,
      long? Original_StartTime,
      int? Original_Length,
      double? Original_PosX,
      double? Original_PosY,
      long Original_ID,
      long ID)
    {
      if (SubjectName == null)
      {
        throw new ArgumentNullException("SubjectName");
      }

      this.Adapter.UpdateCommand.Parameters[0].Value = SubjectName;
      this.Adapter.UpdateCommand.Parameters[1].Value = TrialID;
      this.Adapter.UpdateCommand.Parameters[2].Value = TrialSequence;
      if (CountInTrial.HasValue)
      {
        this.Adapter.UpdateCommand.Parameters[3].Value = CountInTrial.Value;
      }
      else
      {
        this.Adapter.UpdateCommand.Parameters[3].Value = DBNull.Value;
      }

      if (StartTime.HasValue)
      {
        this.Adapter.UpdateCommand.Parameters[4].Value = StartTime.Value;
      }
      else
      {
        this.Adapter.UpdateCommand.Parameters[4].Value = DBNull.Value;
      }

      if (Length.HasValue)
      {
        this.Adapter.UpdateCommand.Parameters[5].Value = Length.Value;
      }
      else
      {
        this.Adapter.UpdateCommand.Parameters[5].Value = DBNull.Value;
      }

      if (PosX.HasValue)
      {
        this.Adapter.UpdateCommand.Parameters[6].Value = PosX.Value;
      }
      else
      {
        this.Adapter.UpdateCommand.Parameters[6].Value = DBNull.Value;
      }

      if (PosY.HasValue)
      {
        this.Adapter.UpdateCommand.Parameters[7].Value = PosY.Value;
      }
      else
      {
        this.Adapter.UpdateCommand.Parameters[7].Value = DBNull.Value;
      }

      if (Original_SubjectName == null)
      {
        throw new ArgumentNullException("Original_SubjectName");
      }

      this.Adapter.UpdateCommand.Parameters[8].Value = Original_SubjectName;
      this.Adapter.UpdateCommand.Parameters[9].Value = Original_TrialID;
      this.Adapter.UpdateCommand.Parameters[10].Value = Original_TrialSequence;
      if (Original_CountInTrial.HasValue)
      {
        this.Adapter.UpdateCommand.Parameters[11].Value = 0;
        this.Adapter.UpdateCommand.Parameters[12].Value = Original_CountInTrial.Value;
      }
      else
      {
        this.Adapter.UpdateCommand.Parameters[11].Value = 1;
        this.Adapter.UpdateCommand.Parameters[12].Value = DBNull.Value;
      }

      if (Original_StartTime.HasValue)
      {
        this.Adapter.UpdateCommand.Parameters[13].Value = 0;
        this.Adapter.UpdateCommand.Parameters[14].Value = Original_StartTime.Value;
      }
      else
      {
        this.Adapter.UpdateCommand.Parameters[13].Value = 1;
        this.Adapter.UpdateCommand.Parameters[14].Value = DBNull.Value;
      }

      if (Original_Length.HasValue)
      {
        this.Adapter.UpdateCommand.Parameters[15].Value = 0;
        this.Adapter.UpdateCommand.Parameters[16].Value = Original_Length.Value;
      }
      else
      {
        this.Adapter.UpdateCommand.Parameters[15].Value = 1;
        this.Adapter.UpdateCommand.Parameters[16].Value = DBNull.Value;
      }

      if (Original_PosX.HasValue)
      {
        this.Adapter.UpdateCommand.Parameters[17].Value = 0;
        this.Adapter.UpdateCommand.Parameters[18].Value = Original_PosX.Value;
      }
      else
      {
        this.Adapter.UpdateCommand.Parameters[17].Value = 1;
        this.Adapter.UpdateCommand.Parameters[18].Value = DBNull.Value;
      }

      if (Original_PosY.HasValue)
      {
        this.Adapter.UpdateCommand.Parameters[19].Value = 0;
        this.Adapter.UpdateCommand.Parameters[20].Value = Original_PosY.Value;
      }
      else
      {
        this.Adapter.UpdateCommand.Parameters[19].Value = 1;
        this.Adapter.UpdateCommand.Parameters[20].Value = DBNull.Value;
      }

      this.Adapter.UpdateCommand.Parameters[21].Value = Original_ID;
      this.Adapter.UpdateCommand.Parameters[22].Value = ID;
      ConnectionState previousConnectionState = this.Adapter.UpdateCommand.Connection.State;
      if ((this.Adapter.UpdateCommand.Connection.State & ConnectionState.Open)
           != ConnectionState.Open)
      {
        this.Adapter.UpdateCommand.Connection.Open();
      }

      try
      {
        int returnValue = this.Adapter.UpdateCommand.ExecuteNonQuery();
        return returnValue;
      }
      finally
      {
        if (previousConnectionState == ConnectionState.Closed)
        {
          this.Adapter.UpdateCommand.Connection.Close();
        }
      }
    }

    /// <summary>
    /// Updates the specified subject name.
    /// </summary>
    /// <param name="SubjectName">
    /// Name of the subject.
    /// </param>
    /// <param name="TrialID">
    /// The trial identifier.
    /// </param>
    /// <param name="TrialSequence">
    /// The trial sequence.
    /// </param>
    /// <param name="CountInTrial">
    /// The count in trial.
    /// </param>
    /// <param name="StartTime">
    /// The start time.
    /// </param>
    /// <param name="Length">
    /// The length.
    /// </param>
    /// <param name="PosX">
    /// The position x.
    /// </param>
    /// <param name="PosY">
    /// The position y.
    /// </param>
    /// <param name="Original_SubjectName">
    /// Name of the original_ subject.
    /// </param>
    /// <param name="Original_TrialID">
    /// The original_ trial identifier.
    /// </param>
    /// <param name="Original_TrialSequence">
    /// The original_ trial sequence.
    /// </param>
    /// <param name="Original_CountInTrial">
    /// The original_ count in trial.
    /// </param>
    /// <param name="Original_StartTime">
    /// The original_ start time.
    /// </param>
    /// <param name="Original_Length">
    /// Length of the original_.
    /// </param>
    /// <param name="Original_PosX">
    /// The original_ position x.
    /// </param>
    /// <param name="Original_PosY">
    /// The original_ position y.
    /// </param>
    /// <param name="Original_ID">
    /// The original_ identifier.
    /// </param>
    /// <returns>
    /// System.Int32.
    /// </returns>
    [DataObjectMethodAttribute(DataObjectMethodType.Update,
     true)]
    public virtual int Update(
      string SubjectName,
      int TrialID,
      int TrialSequence,
      int? CountInTrial,
      long? StartTime,
      int? Length,
      double? PosX,
      double? PosY,
      string Original_SubjectName,
      int Original_TrialID,
      int Original_TrialSequence,
      int? Original_CountInTrial,
      long? Original_StartTime,
      int? Original_Length,
      double? Original_PosX,
      double? Original_PosY,
      long Original_ID)
    {
      return this.Update(
        SubjectName,
        TrialID,
        TrialSequence,
        CountInTrial,
        StartTime,
        Length,
        PosX,
        PosY,
        Original_SubjectName,
        Original_TrialID,
        Original_TrialSequence,
        Original_CountInTrial,
        Original_StartTime,
        Original_Length,
        Original_PosX,
        Original_PosY,
        Original_ID,
        Original_ID);
    }

    #endregion

    #region Methods

    /// <summary>
    ///   Initializes the adapter.
    /// </summary>
    private void InitAdapter()
    {
      this._adapter = new SQLiteDataAdapter();
      var tableMapping = new DataTableMapping();
      tableMapping.SourceTable = "Table";
      tableMapping.DataSetTable = "GazeFixations";
      tableMapping.ColumnMappings.Add("SubjectName", "SubjectName");
      tableMapping.ColumnMappings.Add("TrialID", "TrialID");
      tableMapping.ColumnMappings.Add("TrialSequence", "TrialSequence");
      tableMapping.ColumnMappings.Add("CountInTrial", "CountInTrial");
      tableMapping.ColumnMappings.Add("StartTime", "StartTime");
      tableMapping.ColumnMappings.Add("Length", "Length");
      tableMapping.ColumnMappings.Add("PosX", "PosX");
      tableMapping.ColumnMappings.Add("PosY", "PosY");
      tableMapping.ColumnMappings.Add("ID", "ID");
      this._adapter.TableMappings.Add(tableMapping);
      this._adapter.DeleteCommand = new SQLiteCommand();
      this._adapter.DeleteCommand.Connection = this.Connection;
      this._adapter.DeleteCommand.CommandText =
        @"DELETE FROM [GazeFixations] WHERE (([SubjectName] = @Original_SubjectName) AND ([TrialID] = @Original_TrialID) AND ([TrialSequence] = @Original_TrialSequence) AND ((@IsNull_CountInTrial = 1 AND [CountInTrial] IS NULL) OR ([CountInTrial] = @Original_CountInTrial)) AND ((@IsNull_StartTime = 1 AND [StartTime] IS NULL) OR ([StartTime] = @Original_StartTime)) AND ((@IsNull_Length = 1 AND [Length] IS NULL) OR ([Length] = @Original_Length)) AND ((@IsNull_PosX = 1 AND [PosX] IS NULL) OR ([PosX] = @Original_PosX)) AND ((@IsNull_PosY = 1 AND [PosY] IS NULL) OR ([PosY] = @Original_PosY)) AND ([ID] = @Original_ID))";
      this._adapter.DeleteCommand.CommandType = CommandType.Text;
      this._adapter.DeleteCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_SubjectName",
          DbType.String,
          0,
          ParameterDirection.Input,
          0,
          0,
          "SubjectName",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.DeleteCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_TrialID",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "TrialID",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.DeleteCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_TrialSequence",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "TrialSequence",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.DeleteCommand.Parameters.Add(
        new SQLiteParameter(
          "@IsNull_CountInTrial",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "CountInTrial",
          DataRowVersion.Original,
          true,
          null));
      this._adapter.DeleteCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_CountInTrial",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "CountInTrial",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.DeleteCommand.Parameters.Add(
        new SQLiteParameter(
          "@IsNull_StartTime",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "StartTime",
          DataRowVersion.Original,
          true,
          null));
      this._adapter.DeleteCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_StartTime",
          DbType.Int64,
          0,
          ParameterDirection.Input,
          0,
          0,
          "StartTime",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.DeleteCommand.Parameters.Add(
        new SQLiteParameter(
          "@IsNull_Length",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "Length",
          DataRowVersion.Original,
          true,
          null));
      this._adapter.DeleteCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_Length",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "Length",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.DeleteCommand.Parameters.Add(
        new SQLiteParameter(
          "@IsNull_PosX",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "PosX",
          DataRowVersion.Original,
          true,
          null));
      this._adapter.DeleteCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_PosX",
          DbType.Single,
          0,
          ParameterDirection.Input,
          0,
          0,
          "PosX",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.DeleteCommand.Parameters.Add(
        new SQLiteParameter(
          "@IsNull_PosY",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "PosY",
          DataRowVersion.Original,
          true,
          null));
      this._adapter.DeleteCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_PosY",
          DbType.Single,
          0,
          ParameterDirection.Input,
          0,
          0,
          "PosY",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.DeleteCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_ID",
          DbType.Int64,
          0,
          ParameterDirection.Input,
          0,
          0,
          "ID",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.InsertCommand = new SQLiteCommand();
      this._adapter.InsertCommand.Connection = this.Connection;
      this._adapter.InsertCommand.CommandText =
        @"INSERT INTO [GazeFixations] ([SubjectName], [TrialID], [TrialSequence], [CountInTrial], [StartTime], [Length], [PosX], [PosY]) VALUES (@SubjectName, @TrialID, @TrialSequence, @CountInTrial, @StartTime, @Length, @PosX, @PosY);
SELECT SubjectName, TrialID, TrialSequence, CountInTrial, StartTime, Length, PosX, PosY, ID FROM GazeFixations WHERE (ID = last_insert_rowid())";
      this._adapter.InsertCommand.CommandType = CommandType.Text;
      this._adapter.InsertCommand.Parameters.Add(
        new SQLiteParameter(
          "@SubjectName",
          DbType.String,
          0,
          ParameterDirection.Input,
          0,
          0,
          "SubjectName",
          DataRowVersion.Current,
          false,
          null));
      this._adapter.InsertCommand.Parameters.Add(
        new SQLiteParameter(
          "@TrialID",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "TrialID",
          DataRowVersion.Current,
          false,
          null));
      this._adapter.InsertCommand.Parameters.Add(
        new SQLiteParameter(
          "@TrialSequence",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "TrialSequence",
          DataRowVersion.Current,
          false,
          null));
      this._adapter.InsertCommand.Parameters.Add(
        new SQLiteParameter(
          "@CountInTrial",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "CountInTrial",
          DataRowVersion.Current,
          false,
          null));
      this._adapter.InsertCommand.Parameters.Add(
        new SQLiteParameter(
          "@StartTime",
          DbType.Int64,
          0,
          ParameterDirection.Input,
          0,
          0,
          "StartTime",
          DataRowVersion.Current,
          false,
          null));
      this._adapter.InsertCommand.Parameters.Add(
        new SQLiteParameter(
          "@Length",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "Length",
          DataRowVersion.Current,
          false,
          null));
      this._adapter.InsertCommand.Parameters.Add(
        new SQLiteParameter(
          "@PosX",
          DbType.Single,
          0,
          ParameterDirection.Input,
          0,
          0,
          "PosX",
          DataRowVersion.Current,
          false,
          null));
      this._adapter.InsertCommand.Parameters.Add(
        new SQLiteParameter(
          "@PosY",
          DbType.Single,
          0,
          ParameterDirection.Input,
          0,
          0,
          "PosY",
          DataRowVersion.Current,
          false,
          null));
      this._adapter.UpdateCommand = new SQLiteCommand();
      this._adapter.UpdateCommand.Connection = this.Connection;
      this._adapter.UpdateCommand.CommandText =
        @"UPDATE [GazeFixations] SET [SubjectName] = @SubjectName, [TrialID] = @TrialID, [TrialSequence] = @TrialSequence, [CountInTrial] = @CountInTrial, [StartTime] = @StartTime, [Length] = @Length, [PosX] = @PosX, [PosY] = @PosY WHERE (([SubjectName] = @Original_SubjectName) AND ([TrialID] = @Original_TrialID) AND ([TrialSequence] = @Original_TrialSequence) AND ((@IsNull_CountInTrial = 1 AND [CountInTrial] IS NULL) OR ([CountInTrial] = @Original_CountInTrial)) AND ((@IsNull_StartTime = 1 AND [StartTime] IS NULL) OR ([StartTime] = @Original_StartTime)) AND ((@IsNull_Length = 1 AND [Length] IS NULL) OR ([Length] = @Original_Length)) AND ((@IsNull_PosX = 1 AND [PosX] IS NULL) OR ([PosX] = @Original_PosX)) AND ((@IsNull_PosY = 1 AND [PosY] IS NULL) OR ([PosY] = @Original_PosY)) AND ([ID] = @Original_ID));
SELECT SubjectName, TrialID, TrialSequence, CountInTrial, StartTime, Length, PosX, PosY, ID FROM GazeFixations WHERE (ID = @ID)";
      this._adapter.UpdateCommand.CommandType = CommandType.Text;
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@SubjectName",
          DbType.String,
          0,
          ParameterDirection.Input,
          0,
          0,
          "SubjectName",
          DataRowVersion.Current,
          false,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@TrialID",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "TrialID",
          DataRowVersion.Current,
          false,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@TrialSequence",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "TrialSequence",
          DataRowVersion.Current,
          false,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@CountInTrial",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "CountInTrial",
          DataRowVersion.Current,
          false,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@StartTime",
          DbType.Int64,
          0,
          ParameterDirection.Input,
          0,
          0,
          "StartTime",
          DataRowVersion.Current,
          false,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@Length",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "Length",
          DataRowVersion.Current,
          false,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@PosX",
          DbType.Single,
          0,
          ParameterDirection.Input,
          0,
          0,
          "PosX",
          DataRowVersion.Current,
          false,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@PosY",
          DbType.Single,
          0,
          ParameterDirection.Input,
          0,
          0,
          "PosY",
          DataRowVersion.Current,
          false,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_SubjectName",
          DbType.String,
          0,
          ParameterDirection.Input,
          0,
          0,
          "SubjectName",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_TrialID",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "TrialID",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_TrialSequence",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "TrialSequence",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@IsNull_CountInTrial",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "CountInTrial",
          DataRowVersion.Original,
          true,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_CountInTrial",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "CountInTrial",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@IsNull_StartTime",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "StartTime",
          DataRowVersion.Original,
          true,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_StartTime",
          DbType.Int64,
          0,
          ParameterDirection.Input,
          0,
          0,
          "StartTime",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@IsNull_Length",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "Length",
          DataRowVersion.Original,
          true,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_Length",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "Length",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@IsNull_PosX",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "PosX",
          DataRowVersion.Original,
          true,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_PosX",
          DbType.Single,
          0,
          ParameterDirection.Input,
          0,
          0,
          "PosX",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@IsNull_PosY",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "PosY",
          DataRowVersion.Original,
          true,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_PosY",
          DbType.Single,
          0,
          ParameterDirection.Input,
          0,
          0,
          "PosY",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_ID",
          DbType.Int64,
          0,
          ParameterDirection.Input,
          0,
          0,
          "ID",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@ID",
          DbType.Int64,
          8,
          ParameterDirection.Input,
          0,
          0,
          "ID",
          DataRowVersion.Current,
          false,
          null));
    }

    /// <summary>
    ///   Initializes the command collection.
    /// </summary>
    private void InitCommandCollection()
    {
      this._commandCollection = new SQLiteCommand[8];
      this._commandCollection[0] = new SQLiteCommand();
      this._commandCollection[0].Connection = this.Connection;
      this._commandCollection[0].CommandText =
        "SELECT SubjectName, TrialID, TrialSequence, CountInTrial, StartTime, Length, PosX"
        + ", PosY, ID FROM GazeFixations";
      this._commandCollection[0].CommandType = CommandType.Text;
      this._commandCollection[1] = new SQLiteCommand();
      this._commandCollection[1].Connection = this.Connection;
      this._commandCollection[1].CommandText = "DELETE FROM [GazeFixations] ";
      this._commandCollection[1].CommandType = CommandType.Text;
      this._commandCollection[2] = new SQLiteCommand();
      this._commandCollection[2].Connection = this.Connection;
      this._commandCollection[2].CommandText =
        "DELETE FROM GazeFixations\r\nWHERE     (SubjectName = @Original_SubjectName)";
      this._commandCollection[2].CommandType = CommandType.Text;
      this._commandCollection[2].Parameters.Add(
        new SQLiteParameter(
          "@Original_SubjectName",
          DbType.String,
          50,
          ParameterDirection.Input,
          0,
          0,
          "SubjectName",
          DataRowVersion.Original,
          false,
          null));
      this._commandCollection[3] = new SQLiteCommand();
      this._commandCollection[3].Connection = this.Connection;
      this._commandCollection[3].CommandText =
        "SELECT CountInTrial, ID, Length, PosX, PosY, StartTime, SubjectName, TrialID, Tri"
        + "alSequence FROM GazeFixations WHERE (SubjectName = @Param1)";
      this._commandCollection[3].CommandType = CommandType.Text;
      this._commandCollection[3].Parameters.Add(
        new SQLiteParameter(
          "@Param1",
          DbType.String,
          50,
          ParameterDirection.Input,
          0,
          0,
          "SubjectName",
          DataRowVersion.Current,
          false,
          null));
      this._commandCollection[4] = new SQLiteCommand();
      this._commandCollection[4].Connection = this.Connection;
      this._commandCollection[4].CommandText =
        "SELECT CountInTrial, ID, Length, PosX, PosY, StartTime, SubjectName, TrialID, Tri"
        + "alSequence FROM GazeFixations WHERE (SubjectName = @Param1) AND (TrialSequence =" + " @Param2)";
      this._commandCollection[4].CommandType = CommandType.Text;
      this._commandCollection[4].Parameters.Add(
        new SQLiteParameter(
          "@Param1",
          DbType.String,
          50,
          ParameterDirection.Input,
          0,
          0,
          "SubjectName",
          DataRowVersion.Current,
          false,
          null));
      this._commandCollection[4].Parameters.Add(
        new SQLiteParameter(
          "@Param2",
          DbType.Int32,
          4,
          ParameterDirection.Input,
          0,
          0,
          "TrialSequence",
          DataRowVersion.Current,
          false,
          null));
      this._commandCollection[5] = new SQLiteCommand();
      this._commandCollection[5].Connection = this.Connection;
      this._commandCollection[5].CommandText =
        "SELECT CountInTrial, ID, Length, PosX, PosY, StartTime, SubjectName, TrialID, Tri"
        + "alSequence FROM GazeFixations WHERE (SubjectName = @Param1) AND (TrialID = @Para" + "m2)";
      this._commandCollection[5].CommandType = CommandType.Text;
      this._commandCollection[5].Parameters.Add(
        new SQLiteParameter(
          "@Param1",
          DbType.String,
          50,
          ParameterDirection.Input,
          0,
          0,
          "SubjectName",
          DataRowVersion.Current,
          false,
          null));
      this._commandCollection[5].Parameters.Add(
        new SQLiteParameter(
          "@Param2",
          DbType.Int32,
          4,
          ParameterDirection.Input,
          0,
          0,
          "TrialID",
          DataRowVersion.Current,
          false,
          null));
      this._commandCollection[6] = new SQLiteCommand();
      this._commandCollection[6].Connection = this.Connection;
      this._commandCollection[6].CommandText =
        "SELECT CountInTrial, ID, Length, PosX, PosY, StartTime, SubjectName, TrialID, Tri"
        + "alSequence FROM GazeFixations WHERE (TrialID = @Param1)";
      this._commandCollection[6].CommandType = CommandType.Text;
      this._commandCollection[6].Parameters.Add(
        new SQLiteParameter(
          "@Param1",
          DbType.Int32,
          4,
          ParameterDirection.Input,
          0,
          0,
          "TrialID",
          DataRowVersion.Current,
          false,
          null));
      this._commandCollection[7] = new SQLiteCommand();
      this._commandCollection[7].Connection = this.Connection;
      this._commandCollection[7].CommandText =
        "SELECT     MAX(StartTime) AS MaxTime, TrialID\r\nFROM         GazeFixations\r\nGROUP "
        + "BY TrialID\r\nHAVING      (TrialID= @Param1)";
      this._commandCollection[7].CommandType = CommandType.Text;
      this._commandCollection[7].Parameters.Add(
        new SQLiteParameter(
          "@Param1",
          DbType.Int32,
          4,
          ParameterDirection.Input,
          0,
          0,
          "TrialID",
          DataRowVersion.Current,
          false,
          null));
    }

    /// <summary>
    ///   Initializes the connection.
    /// </summary>
    private void InitConnection()
    {
      this._connection = new SQLiteConnection();
      this._connection.ConnectionString =
        global::Ogama.Properties.Settings.Default.OgamaDatabaseTemplateConnectionString;
    }

    #endregion
  }

  /// <summary>
  ///   Represents the connection and commands used to retrieve and save data.
  /// </summary>
  public class SQLiteTadMouseFixations : Component
  {
    #region Fields

    /// <summary>
    ///   The _adapter
    /// </summary>
    private SQLiteDataAdapter _adapter;

    /// <summary>
    ///   The _command collection
    /// </summary>
    private SQLiteCommand[] _commandCollection;

    /// <summary>
    ///   The _connection
    /// </summary>
    private SQLiteConnection _connection;

    #endregion

    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="SQLiteTadMouseFixations" /> class.
    /// </summary>
    public SQLiteTadMouseFixations()
    {
      this.ClearBeforeFill = true;
    }

    #endregion

    #region Public Properties

    /// <summary>
    ///   Gets or sets a value indicating whether [clear before fill].
    /// </summary>
    /// <value><c>true</c> if [clear before fill]; otherwise, <c>false</c>.</value>
    public bool ClearBeforeFill { get; set; }

    #endregion

    #region Properties

    /// <summary>
    ///   Gets or sets the connection.
    /// </summary>
    /// <value>The connection.</value>
    internal SQLiteConnection Connection
    {
      get
      {
        if (this._connection == null)
        {
          this.InitConnection();
        }

        return this._connection;
      }

      set
      {
        this._connection = value;
        if (this.Adapter.InsertCommand != null)
        {
          this.Adapter.InsertCommand.Connection = value;
        }

        if (this.Adapter.DeleteCommand != null)
        {
          this.Adapter.DeleteCommand.Connection = value;
        }

        if (this.Adapter.UpdateCommand != null)
        {
          this.Adapter.UpdateCommand.Connection = value;
        }

        for (int i = 0; i < this.CommandCollection.Length; i = (i + 1))
        {
          if ((this.CommandCollection[i] != null))
          {
            this.CommandCollection[i].Connection = value;
          }
        }
      }
    }

    /// <summary>
    ///   Gets the command collection.
    /// </summary>
    /// <value>The command collection.</value>
    protected SQLiteCommand[] CommandCollection
    {
      get
      {
        if (this._commandCollection == null)
        {
          this.InitCommandCollection();
        }

        return this._commandCollection;
      }
    }

    /// <summary>
    ///   Gets the adapter.
    /// </summary>
    /// <value>The adapter.</value>
    private SQLiteDataAdapter Adapter
    {
      get
      {
        if (this._adapter == null)
        {
          this.InitAdapter();
        }

        return this._adapter;
      }
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// Deletes the specified original_ subject name.
    /// </summary>
    /// <param name="Original_SubjectName">
    /// Name of the original_ subject.
    /// </param>
    /// <param name="Original_TrialID">
    /// The original_ trial identifier.
    /// </param>
    /// <param name="Original_TrialSequence">
    /// The original_ trial sequence.
    /// </param>
    /// <param name="Original_CountInTrial">
    /// The original_ count in trial.
    /// </param>
    /// <param name="Original_StartTime">
    /// The original_ start time.
    /// </param>
    /// <param name="Original_Length">
    /// Length of the original_.
    /// </param>
    /// <param name="Original_PosX">
    /// The original_ position x.
    /// </param>
    /// <param name="Original_PosY">
    /// The original_ position y.
    /// </param>
    /// <param name="Original_ID">
    /// The original_ identifier.
    /// </param>
    /// <returns>
    /// System.Int32.
    /// </returns>
    /// <exception cref="System.ArgumentNullException">
    /// Original_SubjectName
    /// </exception>
    [DataObjectMethodAttribute(DataObjectMethodType.Delete,
     true)]
    public virtual int Delete(
      string Original_SubjectName,
      int Original_TrialID,
      int Original_TrialSequence,
      int? Original_CountInTrial,
      long? Original_StartTime,
      int? Original_Length,
      double? Original_PosX,
      double? Original_PosY,
      long Original_ID)
    {
      if (Original_SubjectName == null)
      {
        throw new ArgumentNullException("Original_SubjectName");
      }

      this.Adapter.DeleteCommand.Parameters[0].Value = Original_SubjectName;
      this.Adapter.DeleteCommand.Parameters[1].Value = Original_TrialID;
      this.Adapter.DeleteCommand.Parameters[2].Value = Original_TrialSequence;
      if (Original_CountInTrial.HasValue)
      {
        this.Adapter.DeleteCommand.Parameters[3].Value = 0;
        this.Adapter.DeleteCommand.Parameters[4].Value = Original_CountInTrial.Value;
      }
      else
      {
        this.Adapter.DeleteCommand.Parameters[3].Value = 1;
        this.Adapter.DeleteCommand.Parameters[4].Value = DBNull.Value;
      }

      if (Original_StartTime.HasValue)
      {
        this.Adapter.DeleteCommand.Parameters[5].Value = 0;
        this.Adapter.DeleteCommand.Parameters[6].Value = Original_StartTime.Value;
      }
      else
      {
        this.Adapter.DeleteCommand.Parameters[5].Value = 1;
        this.Adapter.DeleteCommand.Parameters[6].Value = DBNull.Value;
      }

      if (Original_Length.HasValue)
      {
        this.Adapter.DeleteCommand.Parameters[7].Value = 0;
        this.Adapter.DeleteCommand.Parameters[8].Value = Original_Length.Value;
      }
      else
      {
        this.Adapter.DeleteCommand.Parameters[7].Value = 1;
        this.Adapter.DeleteCommand.Parameters[8].Value = DBNull.Value;
      }

      if (Original_PosX.HasValue)
      {
        this.Adapter.DeleteCommand.Parameters[9].Value = 0;
        this.Adapter.DeleteCommand.Parameters[10].Value = Original_PosX.Value;
      }
      else
      {
        this.Adapter.DeleteCommand.Parameters[9].Value = 1;
        this.Adapter.DeleteCommand.Parameters[10].Value = DBNull.Value;
      }

      if (Original_PosY.HasValue)
      {
        this.Adapter.DeleteCommand.Parameters[11].Value = 0;
        this.Adapter.DeleteCommand.Parameters[12].Value = Original_PosY.Value;
      }
      else
      {
        this.Adapter.DeleteCommand.Parameters[11].Value = 1;
        this.Adapter.DeleteCommand.Parameters[12].Value = DBNull.Value;
      }

      this.Adapter.DeleteCommand.Parameters[13].Value = Original_ID;
      ConnectionState previousConnectionState = this.Adapter.DeleteCommand.Connection.State;
      if ((this.Adapter.DeleteCommand.Connection.State & ConnectionState.Open)
           != ConnectionState.Open)
      {
        this.Adapter.DeleteCommand.Connection.Open();
      }

      try
      {
        int returnValue = this.Adapter.DeleteCommand.ExecuteNonQuery();
        return returnValue;
      }
      finally
      {
        if (previousConnectionState == ConnectionState.Closed)
        {
          this.Adapter.DeleteCommand.Connection.Close();
        }
      }
    }

    /// <summary>
    ///   Deletes all.
    /// </summary>
    /// <returns>System.Int32.</returns>
    [DataObjectMethodAttribute(DataObjectMethodType.Delete,
     false)]
    public virtual int DeleteAll()
    {
      SQLiteCommand command = this.CommandCollection[1];
      ConnectionState previousConnectionState = command.Connection.State;
      if ((command.Connection.State & ConnectionState.Open)
           != ConnectionState.Open)
      {
        command.Connection.Open();
      }

      int returnValue;
      try
      {
        returnValue = command.ExecuteNonQuery();
      }
      finally
      {
        if (previousConnectionState == ConnectionState.Closed)
        {
          command.Connection.Close();
        }
      }

      return returnValue;
    }

    /// <summary>
    /// Deletes the by subject.
    /// </summary>
    /// <param name="Original_SubjectName">
    /// Name of the original_ subject.
    /// </param>
    /// <returns>
    /// System.Int32.
    /// </returns>
    /// <exception cref="System.ArgumentNullException">
    /// Original_SubjectName
    /// </exception>
    [DataObjectMethodAttribute(DataObjectMethodType.Delete,
     false)]
    public virtual int DeleteBySubject(string Original_SubjectName)
    {
      SQLiteCommand command = this.CommandCollection[2];
      if (Original_SubjectName == null)
      {
        throw new ArgumentNullException("Original_SubjectName");
      }

      command.Parameters[0].Value = Original_SubjectName;
      ConnectionState previousConnectionState = command.Connection.State;
      if ((command.Connection.State & ConnectionState.Open)
           != ConnectionState.Open)
      {
        command.Connection.Open();
      }

      int returnValue;
      try
      {
        returnValue = command.ExecuteNonQuery();
      }
      finally
      {
        if (previousConnectionState == ConnectionState.Closed)
        {
          command.Connection.Close();
        }
      }

      return returnValue;
    }

    /// <summary>
    /// Fills the specified data table.
    /// </summary>
    /// <param name="dataTable">
    /// The data table.
    /// </param>
    /// <returns>
    /// System.Int32.
    /// </returns>
    [DataObjectMethodAttribute(DataObjectMethodType.Fill,
     true)]
    public virtual int Fill(SQLiteOgamaDataSet.MouseFixationsDataTable dataTable)
    {
      this.Adapter.SelectCommand = this.CommandCollection[0];
      if (this.ClearBeforeFill)
      {
        dataTable.Clear();
      }

      int returnValue = this.Adapter.Fill(dataTable);
      return returnValue;
    }

    /// <summary>
    ///   Gets the data.
    /// </summary>
    /// <returns>SQLiteOgamaDataSet.MouseFixationsDataTable.</returns>
    [DataObjectMethodAttribute(DataObjectMethodType.Select,
     true)]
    public virtual SQLiteOgamaDataSet.MouseFixationsDataTable GetData()
    {
      this.Adapter.SelectCommand = this.CommandCollection[0];
      var dataTable = new SQLiteOgamaDataSet.MouseFixationsDataTable();
      this.Adapter.Fill(dataTable);
      return dataTable;
    }

    /// <summary>
    /// Gets the data by subject.
    /// </summary>
    /// <param name="Param1">
    /// The param1.
    /// </param>
    /// <returns>
    /// SQLiteOgamaDataSet.MouseFixationsDataTable.
    /// </returns>
    /// <exception cref="System.ArgumentNullException">
    /// Param1
    /// </exception>
    [DataObjectMethodAttribute(DataObjectMethodType.Select,
     false)]
    public virtual SQLiteOgamaDataSet.MouseFixationsDataTable GetDataBySubject(string Param1)
    {
      this.Adapter.SelectCommand = this.CommandCollection[3];
      if (Param1 == null)
      {
        throw new ArgumentNullException("Param1");
      }

      this.Adapter.SelectCommand.Parameters[0].Value = Param1;
      var dataTable = new SQLiteOgamaDataSet.MouseFixationsDataTable();
      this.Adapter.Fill(dataTable);
      return dataTable;
    }

    /// <summary>
    /// Gets the data by subject and sequence.
    /// </summary>
    /// <param name="Param1">
    /// The param1.
    /// </param>
    /// <param name="Param2">
    /// The param2.
    /// </param>
    /// <returns>
    /// SQLiteOgamaDataSet.MouseFixationsDataTable.
    /// </returns>
    /// <exception cref="System.ArgumentNullException">
    /// Param1
    /// </exception>
    [DataObjectMethodAttribute(DataObjectMethodType.Select,
     false)]
    public virtual SQLiteOgamaDataSet.MouseFixationsDataTable GetDataBySubjectAndSequence(string Param1, int Param2)
    {
      this.Adapter.SelectCommand = this.CommandCollection[4];
      if (Param1 == null)
      {
        throw new ArgumentNullException("Param1");
      }

      this.Adapter.SelectCommand.Parameters[0].Value = Param1;
      this.Adapter.SelectCommand.Parameters[1].Value = Param2;
      var dataTable = new SQLiteOgamaDataSet.MouseFixationsDataTable();
      this.Adapter.Fill(dataTable);
      return dataTable;
    }

    /// <summary>
    /// Gets the data by subject and trial identifier.
    /// </summary>
    /// <param name="Param1">
    /// The param1.
    /// </param>
    /// <param name="Param2">
    /// The param2.
    /// </param>
    /// <returns>
    /// SQLiteOgamaDataSet.MouseFixationsDataTable.
    /// </returns>
    /// <exception cref="System.ArgumentNullException">
    /// Param1
    /// </exception>
    [DataObjectMethodAttribute(DataObjectMethodType.Select,
     false)]
    public virtual SQLiteOgamaDataSet.MouseFixationsDataTable GetDataBySubjectAndTrialID(string Param1, int Param2)
    {
      this.Adapter.SelectCommand = this.CommandCollection[5];
      if (Param1 == null)
      {
        throw new ArgumentNullException("Param1");
      }

      this.Adapter.SelectCommand.Parameters[0].Value = Param1;
      this.Adapter.SelectCommand.Parameters[1].Value = Param2;
      var dataTable = new SQLiteOgamaDataSet.MouseFixationsDataTable();
      this.Adapter.Fill(dataTable);
      return dataTable;
    }

    /// <summary>
    /// Gets the data by trial identifier.
    /// </summary>
    /// <param name="Param1">
    /// The param1.
    /// </param>
    /// <returns>
    /// SQLiteOgamaDataSet.MouseFixationsDataTable.
    /// </returns>
    [DataObjectMethodAttribute(DataObjectMethodType.Select,
     false)]
    public virtual SQLiteOgamaDataSet.MouseFixationsDataTable GetDataByTrialID(int Param1)
    {
      this.Adapter.SelectCommand = this.CommandCollection[6];
      this.Adapter.SelectCommand.Parameters[0].Value = Param1;
      var dataTable = new SQLiteOgamaDataSet.MouseFixationsDataTable();
      this.Adapter.Fill(dataTable);
      return dataTable;
    }

    /// <summary>
    /// Gets the maximum start time of trial.
    /// </summary>
    /// <param name="Param1">
    /// The param1.
    /// </param>
    /// <returns>
    /// System.Object.
    /// </returns>
    public virtual object GetMaxStartTimeOfTrial(int Param1)
    {
      SQLiteCommand command = this.CommandCollection[7];
      command.Parameters[0].Value = Param1;
      ConnectionState previousConnectionState = command.Connection.State;
      if ((command.Connection.State & ConnectionState.Open)
           != ConnectionState.Open)
      {
        command.Connection.Open();
      }

      object returnValue;
      try
      {
        returnValue = command.ExecuteScalar();
      }
      finally
      {
        if (previousConnectionState == ConnectionState.Closed)
        {
          command.Connection.Close();
        }
      }

      if ((returnValue == null) || (returnValue.GetType() == typeof(DBNull)))
      {
        return null;
      }

      return returnValue;
    }

    /// <summary>
    /// Inserts the specified subject name.
    /// </summary>
    /// <param name="SubjectName">
    /// Name of the subject.
    /// </param>
    /// <param name="TrialID">
    /// The trial identifier.
    /// </param>
    /// <param name="TrialSequence">
    /// The trial sequence.
    /// </param>
    /// <param name="CountInTrial">
    /// The count in trial.
    /// </param>
    /// <param name="StartTime">
    /// The start time.
    /// </param>
    /// <param name="Length">
    /// The length.
    /// </param>
    /// <param name="PosX">
    /// The position x.
    /// </param>
    /// <param name="PosY">
    /// The position y.
    /// </param>
    /// <returns>
    /// System.Int32.
    /// </returns>
    /// <exception cref="System.ArgumentNullException">
    /// SubjectName
    /// </exception>
    [DataObjectMethodAttribute(DataObjectMethodType.Insert,
     true)]
    public virtual int Insert(
      string SubjectName,
      int TrialID,
      int TrialSequence,
      int? CountInTrial,
      long? StartTime,
      int? Length,
      double? PosX,
      double? PosY)
    {
      if (SubjectName == null)
      {
        throw new ArgumentNullException("SubjectName");
      }

      this.Adapter.InsertCommand.Parameters[0].Value = SubjectName;
      this.Adapter.InsertCommand.Parameters[1].Value = TrialID;
      this.Adapter.InsertCommand.Parameters[2].Value = TrialSequence;
      if (CountInTrial.HasValue)
      {
        this.Adapter.InsertCommand.Parameters[3].Value = CountInTrial.Value;
      }
      else
      {
        this.Adapter.InsertCommand.Parameters[3].Value = DBNull.Value;
      }

      if (StartTime.HasValue)
      {
        this.Adapter.InsertCommand.Parameters[4].Value = StartTime.Value;
      }
      else
      {
        this.Adapter.InsertCommand.Parameters[4].Value = DBNull.Value;
      }

      if (Length.HasValue)
      {
        this.Adapter.InsertCommand.Parameters[5].Value = Length.Value;
      }
      else
      {
        this.Adapter.InsertCommand.Parameters[5].Value = DBNull.Value;
      }

      if (PosX.HasValue)
      {
        this.Adapter.InsertCommand.Parameters[6].Value = PosX.Value;
      }
      else
      {
        this.Adapter.InsertCommand.Parameters[6].Value = DBNull.Value;
      }

      if (PosY.HasValue)
      {
        this.Adapter.InsertCommand.Parameters[7].Value = PosY.Value;
      }
      else
      {
        this.Adapter.InsertCommand.Parameters[7].Value = DBNull.Value;
      }

      ConnectionState previousConnectionState = this.Adapter.InsertCommand.Connection.State;
      if ((this.Adapter.InsertCommand.Connection.State & ConnectionState.Open)
           != ConnectionState.Open)
      {
        this.Adapter.InsertCommand.Connection.Open();
      }

      try
      {
        int returnValue = this.Adapter.InsertCommand.ExecuteNonQuery();
        return returnValue;
      }
      finally
      {
        if (previousConnectionState == ConnectionState.Closed)
        {
          this.Adapter.InsertCommand.Connection.Close();
        }
      }
    }

    /// <summary>
    /// Updates the specified data table.
    /// </summary>
    /// <param name="dataTable">
    /// The data table.
    /// </param>
    /// <returns>
    /// System.Int32.
    /// </returns>
    public virtual int Update(SQLiteOgamaDataSet.MouseFixationsDataTable dataTable)
    {
      return this.Adapter.Update(dataTable);
    }

    /// <summary>
    /// Updates the specified data set.
    /// </summary>
    /// <param name="dataSet">
    /// The data set.
    /// </param>
    /// <returns>
    /// System.Int32.
    /// </returns>
    public virtual int Update(SQLiteOgamaDataSet dataSet)
    {
      return this.Adapter.Update(dataSet, "MouseFixations");
    }

    /// <summary>
    /// Updates the specified data row.
    /// </summary>
    /// <param name="dataRow">
    /// The data row.
    /// </param>
    /// <returns>
    /// System.Int32.
    /// </returns>
    public virtual int Update(DataRow dataRow)
    {
      return this.Adapter.Update(new[] { dataRow });
    }

    /// <summary>
    /// Updates the specified data rows.
    /// </summary>
    /// <param name="dataRows">
    /// The data rows.
    /// </param>
    /// <returns>
    /// System.Int32.
    /// </returns>
    public virtual int Update(DataRow[] dataRows)
    {
      return this.Adapter.Update(dataRows);
    }

    /// <summary>
    /// Updates the specified subject name.
    /// </summary>
    /// <param name="SubjectName">
    /// Name of the subject.
    /// </param>
    /// <param name="TrialID">
    /// The trial identifier.
    /// </param>
    /// <param name="TrialSequence">
    /// The trial sequence.
    /// </param>
    /// <param name="CountInTrial">
    /// The count in trial.
    /// </param>
    /// <param name="StartTime">
    /// The start time.
    /// </param>
    /// <param name="Length">
    /// The length.
    /// </param>
    /// <param name="PosX">
    /// The position x.
    /// </param>
    /// <param name="PosY">
    /// The position y.
    /// </param>
    /// <param name="Original_SubjectName">
    /// Name of the original_ subject.
    /// </param>
    /// <param name="Original_TrialID">
    /// The original_ trial identifier.
    /// </param>
    /// <param name="Original_TrialSequence">
    /// The original_ trial sequence.
    /// </param>
    /// <param name="Original_CountInTrial">
    /// The original_ count in trial.
    /// </param>
    /// <param name="Original_StartTime">
    /// The original_ start time.
    /// </param>
    /// <param name="Original_Length">
    /// Length of the original_.
    /// </param>
    /// <param name="Original_PosX">
    /// The original_ position x.
    /// </param>
    /// <param name="Original_PosY">
    /// The original_ position y.
    /// </param>
    /// <param name="Original_ID">
    /// The original_ identifier.
    /// </param>
    /// <param name="ID">
    /// The identifier.
    /// </param>
    /// <returns>
    /// System.Int32.
    /// </returns>
    /// <exception cref="System.ArgumentNullException">
    /// SubjectName
    ///   or
    ///   Original_SubjectName
    /// </exception>
    [DataObjectMethodAttribute(DataObjectMethodType.Update,
     true)]
    public virtual int Update(
      string SubjectName,
      int TrialID,
      int TrialSequence,
      int? CountInTrial,
      long? StartTime,
      int? Length,
      double? PosX,
      double? PosY,
      string Original_SubjectName,
      int Original_TrialID,
      int Original_TrialSequence,
      int? Original_CountInTrial,
      long? Original_StartTime,
      int? Original_Length,
      double? Original_PosX,
      double? Original_PosY,
      long Original_ID,
      long ID)
    {
      if (SubjectName == null)
      {
        throw new ArgumentNullException("SubjectName");
      }

      this.Adapter.UpdateCommand.Parameters[0].Value = SubjectName;
      this.Adapter.UpdateCommand.Parameters[1].Value = TrialID;
      this.Adapter.UpdateCommand.Parameters[2].Value = TrialSequence;
      if (CountInTrial.HasValue)
      {
        this.Adapter.UpdateCommand.Parameters[3].Value = CountInTrial.Value;
      }
      else
      {
        this.Adapter.UpdateCommand.Parameters[3].Value = DBNull.Value;
      }

      if (StartTime.HasValue)
      {
        this.Adapter.UpdateCommand.Parameters[4].Value = StartTime.Value;
      }
      else
      {
        this.Adapter.UpdateCommand.Parameters[4].Value = DBNull.Value;
      }

      if (Length.HasValue)
      {
        this.Adapter.UpdateCommand.Parameters[5].Value = Length.Value;
      }
      else
      {
        this.Adapter.UpdateCommand.Parameters[5].Value = DBNull.Value;
      }

      if (PosX.HasValue)
      {
        this.Adapter.UpdateCommand.Parameters[6].Value = PosX.Value;
      }
      else
      {
        this.Adapter.UpdateCommand.Parameters[6].Value = DBNull.Value;
      }

      if (PosY.HasValue)
      {
        this.Adapter.UpdateCommand.Parameters[7].Value = PosY.Value;
      }
      else
      {
        this.Adapter.UpdateCommand.Parameters[7].Value = DBNull.Value;
      }

      if (Original_SubjectName == null)
      {
        throw new ArgumentNullException("Original_SubjectName");
      }

      this.Adapter.UpdateCommand.Parameters[8].Value = Original_SubjectName;
      this.Adapter.UpdateCommand.Parameters[9].Value = Original_TrialID;
      this.Adapter.UpdateCommand.Parameters[10].Value = Original_TrialSequence;
      if (Original_CountInTrial.HasValue)
      {
        this.Adapter.UpdateCommand.Parameters[11].Value = 0;
        this.Adapter.UpdateCommand.Parameters[12].Value = Original_CountInTrial.Value;
      }
      else
      {
        this.Adapter.UpdateCommand.Parameters[11].Value = 1;
        this.Adapter.UpdateCommand.Parameters[12].Value = DBNull.Value;
      }

      if (Original_StartTime.HasValue)
      {
        this.Adapter.UpdateCommand.Parameters[13].Value = 0;
        this.Adapter.UpdateCommand.Parameters[14].Value = Original_StartTime.Value;
      }
      else
      {
        this.Adapter.UpdateCommand.Parameters[13].Value = 1;
        this.Adapter.UpdateCommand.Parameters[14].Value = DBNull.Value;
      }

      if (Original_Length.HasValue)
      {
        this.Adapter.UpdateCommand.Parameters[15].Value = 0;
        this.Adapter.UpdateCommand.Parameters[16].Value = Original_Length.Value;
      }
      else
      {
        this.Adapter.UpdateCommand.Parameters[15].Value = 1;
        this.Adapter.UpdateCommand.Parameters[16].Value = DBNull.Value;
      }

      if (Original_PosX.HasValue)
      {
        this.Adapter.UpdateCommand.Parameters[17].Value = 0;
        this.Adapter.UpdateCommand.Parameters[18].Value = Original_PosX.Value;
      }
      else
      {
        this.Adapter.UpdateCommand.Parameters[17].Value = 1;
        this.Adapter.UpdateCommand.Parameters[18].Value = DBNull.Value;
      }

      if (Original_PosY.HasValue)
      {
        this.Adapter.UpdateCommand.Parameters[19].Value = 0;
        this.Adapter.UpdateCommand.Parameters[20].Value = Original_PosY.Value;
      }
      else
      {
        this.Adapter.UpdateCommand.Parameters[19].Value = 1;
        this.Adapter.UpdateCommand.Parameters[20].Value = DBNull.Value;
      }

      this.Adapter.UpdateCommand.Parameters[21].Value = Original_ID;
      this.Adapter.UpdateCommand.Parameters[22].Value = ID;
      ConnectionState previousConnectionState = this.Adapter.UpdateCommand.Connection.State;
      if ((this.Adapter.UpdateCommand.Connection.State & ConnectionState.Open)
           != ConnectionState.Open)
      {
        this.Adapter.UpdateCommand.Connection.Open();
      }

      try
      {
        int returnValue = this.Adapter.UpdateCommand.ExecuteNonQuery();
        return returnValue;
      }
      finally
      {
        if (previousConnectionState == ConnectionState.Closed)
        {
          this.Adapter.UpdateCommand.Connection.Close();
        }
      }
    }

    /// <summary>
    /// Updates the specified subject name.
    /// </summary>
    /// <param name="SubjectName">
    /// Name of the subject.
    /// </param>
    /// <param name="TrialID">
    /// The trial identifier.
    /// </param>
    /// <param name="TrialSequence">
    /// The trial sequence.
    /// </param>
    /// <param name="CountInTrial">
    /// The count in trial.
    /// </param>
    /// <param name="StartTime">
    /// The start time.
    /// </param>
    /// <param name="Length">
    /// The length.
    /// </param>
    /// <param name="PosX">
    /// The position x.
    /// </param>
    /// <param name="PosY">
    /// The position y.
    /// </param>
    /// <param name="Original_SubjectName">
    /// Name of the original_ subject.
    /// </param>
    /// <param name="Original_TrialID">
    /// The original_ trial identifier.
    /// </param>
    /// <param name="Original_TrialSequence">
    /// The original_ trial sequence.
    /// </param>
    /// <param name="Original_CountInTrial">
    /// The original_ count in trial.
    /// </param>
    /// <param name="Original_StartTime">
    /// The original_ start time.
    /// </param>
    /// <param name="Original_Length">
    /// Length of the original_.
    /// </param>
    /// <param name="Original_PosX">
    /// The original_ position x.
    /// </param>
    /// <param name="Original_PosY">
    /// The original_ position y.
    /// </param>
    /// <param name="Original_ID">
    /// The original_ identifier.
    /// </param>
    /// <returns>
    /// System.Int32.
    /// </returns>
    [DataObjectMethodAttribute(DataObjectMethodType.Update,
     true)]
    public virtual int Update(
      string SubjectName,
      int TrialID,
      int TrialSequence,
      int? CountInTrial,
      long? StartTime,
      int? Length,
      double? PosX,
      double? PosY,
      string Original_SubjectName,
      int Original_TrialID,
      int Original_TrialSequence,
      int? Original_CountInTrial,
      long? Original_StartTime,
      int? Original_Length,
      double? Original_PosX,
      double? Original_PosY,
      long Original_ID)
    {
      return this.Update(
        SubjectName,
        TrialID,
        TrialSequence,
        CountInTrial,
        StartTime,
        Length,
        PosX,
        PosY,
        Original_SubjectName,
        Original_TrialID,
        Original_TrialSequence,
        Original_CountInTrial,
        Original_StartTime,
        Original_Length,
        Original_PosX,
        Original_PosY,
        Original_ID,
        Original_ID);
    }

    #endregion

    #region Methods

    /// <summary>
    ///   Initializes the adapter.
    /// </summary>
    private void InitAdapter()
    {
      this._adapter = new SQLiteDataAdapter();
      var tableMapping = new DataTableMapping();
      tableMapping.SourceTable = "Table";
      tableMapping.DataSetTable = "MouseFixations";
      tableMapping.ColumnMappings.Add("SubjectName", "SubjectName");
      tableMapping.ColumnMappings.Add("TrialID", "TrialID");
      tableMapping.ColumnMappings.Add("TrialSequence", "TrialSequence");
      tableMapping.ColumnMappings.Add("CountInTrial", "CountInTrial");
      tableMapping.ColumnMappings.Add("StartTime", "StartTime");
      tableMapping.ColumnMappings.Add("Length", "Length");
      tableMapping.ColumnMappings.Add("PosX", "PosX");
      tableMapping.ColumnMappings.Add("PosY", "PosY");
      tableMapping.ColumnMappings.Add("ID", "ID");
      this._adapter.TableMappings.Add(tableMapping);
      this._adapter.DeleteCommand = new SQLiteCommand();
      this._adapter.DeleteCommand.Connection = this.Connection;
      this._adapter.DeleteCommand.CommandText =
        @"DELETE FROM [MouseFixations] WHERE (([SubjectName] = @Original_SubjectName) AND ([TrialID] = @Original_TrialID) AND ([TrialSequence] = @Original_TrialSequence) AND ((@IsNull_CountInTrial = 1 AND [CountInTrial] IS NULL) OR ([CountInTrial] = @Original_CountInTrial)) AND ((@IsNull_StartTime = 1 AND [StartTime] IS NULL) OR ([StartTime] = @Original_StartTime)) AND ((@IsNull_Length = 1 AND [Length] IS NULL) OR ([Length] = @Original_Length)) AND ((@IsNull_PosX = 1 AND [PosX] IS NULL) OR ([PosX] = @Original_PosX)) AND ((@IsNull_PosY = 1 AND [PosY] IS NULL) OR ([PosY] = @Original_PosY)) AND ([ID] = @Original_ID))";
      this._adapter.DeleteCommand.CommandType = CommandType.Text;
      this._adapter.DeleteCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_SubjectName",
          DbType.String,
          0,
          ParameterDirection.Input,
          0,
          0,
          "SubjectName",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.DeleteCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_TrialID",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "TrialID",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.DeleteCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_TrialSequence",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "TrialSequence",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.DeleteCommand.Parameters.Add(
        new SQLiteParameter(
          "@IsNull_CountInTrial",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "CountInTrial",
          DataRowVersion.Original,
          true,
          null));
      this._adapter.DeleteCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_CountInTrial",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "CountInTrial",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.DeleteCommand.Parameters.Add(
        new SQLiteParameter(
          "@IsNull_StartTime",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "StartTime",
          DataRowVersion.Original,
          true,
          null));
      this._adapter.DeleteCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_StartTime",
          DbType.Int64,
          0,
          ParameterDirection.Input,
          0,
          0,
          "StartTime",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.DeleteCommand.Parameters.Add(
        new SQLiteParameter(
          "@IsNull_Length",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "Length",
          DataRowVersion.Original,
          true,
          null));
      this._adapter.DeleteCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_Length",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "Length",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.DeleteCommand.Parameters.Add(
        new SQLiteParameter(
          "@IsNull_PosX",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "PosX",
          DataRowVersion.Original,
          true,
          null));
      this._adapter.DeleteCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_PosX",
          DbType.Single,
          0,
          ParameterDirection.Input,
          0,
          0,
          "PosX",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.DeleteCommand.Parameters.Add(
        new SQLiteParameter(
          "@IsNull_PosY",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "PosY",
          DataRowVersion.Original,
          true,
          null));
      this._adapter.DeleteCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_PosY",
          DbType.Single,
          0,
          ParameterDirection.Input,
          0,
          0,
          "PosY",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.DeleteCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_ID",
          DbType.Int64,
          0,
          ParameterDirection.Input,
          0,
          0,
          "ID",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.InsertCommand = new SQLiteCommand();
      this._adapter.InsertCommand.Connection = this.Connection;
      this._adapter.InsertCommand.CommandText =
        @"INSERT INTO [MouseFixations] ([SubjectName], [TrialID], [TrialSequence], [CountInTrial], [StartTime], [Length], [PosX], [PosY]) VALUES (@SubjectName, @TrialID, @TrialSequence, @CountInTrial, @StartTime, @Length, @PosX, @PosY);
SELECT SubjectName, TrialID, TrialSequence, CountInTrial, StartTime, Length, PosX, PosY, ID FROM MouseFixations WHERE (ID = last_insert_rowid())";
      this._adapter.InsertCommand.CommandType = CommandType.Text;
      this._adapter.InsertCommand.Parameters.Add(
        new SQLiteParameter(
          "@SubjectName",
          DbType.String,
          0,
          ParameterDirection.Input,
          0,
          0,
          "SubjectName",
          DataRowVersion.Current,
          false,
          null));
      this._adapter.InsertCommand.Parameters.Add(
        new SQLiteParameter(
          "@TrialID",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "TrialID",
          DataRowVersion.Current,
          false,
          null));
      this._adapter.InsertCommand.Parameters.Add(
        new SQLiteParameter(
          "@TrialSequence",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "TrialSequence",
          DataRowVersion.Current,
          false,
          null));
      this._adapter.InsertCommand.Parameters.Add(
        new SQLiteParameter(
          "@CountInTrial",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "CountInTrial",
          DataRowVersion.Current,
          false,
          null));
      this._adapter.InsertCommand.Parameters.Add(
        new SQLiteParameter(
          "@StartTime",
          DbType.Int64,
          0,
          ParameterDirection.Input,
          0,
          0,
          "StartTime",
          DataRowVersion.Current,
          false,
          null));
      this._adapter.InsertCommand.Parameters.Add(
        new SQLiteParameter(
          "@Length",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "Length",
          DataRowVersion.Current,
          false,
          null));
      this._adapter.InsertCommand.Parameters.Add(
        new SQLiteParameter(
          "@PosX",
          DbType.Single,
          0,
          ParameterDirection.Input,
          0,
          0,
          "PosX",
          DataRowVersion.Current,
          false,
          null));
      this._adapter.InsertCommand.Parameters.Add(
        new SQLiteParameter(
          "@PosY",
          DbType.Single,
          0,
          ParameterDirection.Input,
          0,
          0,
          "PosY",
          DataRowVersion.Current,
          false,
          null));
      this._adapter.UpdateCommand = new SQLiteCommand();
      this._adapter.UpdateCommand.Connection = this.Connection;
      this._adapter.UpdateCommand.CommandText =
        @"UPDATE [MouseFixations] SET [SubjectName] = @SubjectName, [TrialID] = @TrialID, [TrialSequence] = @TrialSequence, [CountInTrial] = @CountInTrial, [StartTime] = @StartTime, [Length] = @Length, [PosX] = @PosX, [PosY] = @PosY WHERE (([SubjectName] = @Original_SubjectName) AND ([TrialID] = @Original_TrialID) AND ([TrialSequence] = @Original_TrialSequence) AND ((@IsNull_CountInTrial = 1 AND [CountInTrial] IS NULL) OR ([CountInTrial] = @Original_CountInTrial)) AND ((@IsNull_StartTime = 1 AND [StartTime] IS NULL) OR ([StartTime] = @Original_StartTime)) AND ((@IsNull_Length = 1 AND [Length] IS NULL) OR ([Length] = @Original_Length)) AND ((@IsNull_PosX = 1 AND [PosX] IS NULL) OR ([PosX] = @Original_PosX)) AND ((@IsNull_PosY = 1 AND [PosY] IS NULL) OR ([PosY] = @Original_PosY)) AND ([ID] = @Original_ID));
SELECT SubjectName, TrialID, TrialSequence, CountInTrial, StartTime, Length, PosX, PosY, ID FROM MouseFixations WHERE (ID = @ID)";
      this._adapter.UpdateCommand.CommandType = CommandType.Text;
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@SubjectName",
          DbType.String,
          0,
          ParameterDirection.Input,
          0,
          0,
          "SubjectName",
          DataRowVersion.Current,
          false,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@TrialID",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "TrialID",
          DataRowVersion.Current,
          false,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@TrialSequence",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "TrialSequence",
          DataRowVersion.Current,
          false,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@CountInTrial",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "CountInTrial",
          DataRowVersion.Current,
          false,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@StartTime",
          DbType.Int64,
          0,
          ParameterDirection.Input,
          0,
          0,
          "StartTime",
          DataRowVersion.Current,
          false,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@Length",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "Length",
          DataRowVersion.Current,
          false,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@PosX",
          DbType.Single,
          0,
          ParameterDirection.Input,
          0,
          0,
          "PosX",
          DataRowVersion.Current,
          false,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@PosY",
          DbType.Single,
          0,
          ParameterDirection.Input,
          0,
          0,
          "PosY",
          DataRowVersion.Current,
          false,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_SubjectName",
          DbType.String,
          0,
          ParameterDirection.Input,
          0,
          0,
          "SubjectName",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_TrialID",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "TrialID",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_TrialSequence",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "TrialSequence",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@IsNull_CountInTrial",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "CountInTrial",
          DataRowVersion.Original,
          true,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_CountInTrial",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "CountInTrial",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@IsNull_StartTime",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "StartTime",
          DataRowVersion.Original,
          true,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_StartTime",
          DbType.Int64,
          0,
          ParameterDirection.Input,
          0,
          0,
          "StartTime",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@IsNull_Length",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "Length",
          DataRowVersion.Original,
          true,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_Length",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "Length",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@IsNull_PosX",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "PosX",
          DataRowVersion.Original,
          true,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_PosX",
          DbType.Single,
          0,
          ParameterDirection.Input,
          0,
          0,
          "PosX",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@IsNull_PosY",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "PosY",
          DataRowVersion.Original,
          true,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_PosY",
          DbType.Single,
          0,
          ParameterDirection.Input,
          0,
          0,
          "PosY",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_ID",
          DbType.Int64,
          0,
          ParameterDirection.Input,
          0,
          0,
          "ID",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@ID",
          DbType.Int64,
          8,
          ParameterDirection.Input,
          0,
          0,
          "ID",
          DataRowVersion.Current,
          false,
          null));
    }

    /// <summary>
    ///   Initializes the command collection.
    /// </summary>
    private void InitCommandCollection()
    {
      this._commandCollection = new SQLiteCommand[8];
      this._commandCollection[0] = new SQLiteCommand();
      this._commandCollection[0].Connection = this.Connection;
      this._commandCollection[0].CommandText =
        "SELECT SubjectName, TrialID, TrialSequence, CountInTrial, StartTime, Length, PosX"
        + ", PosY, ID FROM MouseFixations";
      this._commandCollection[0].CommandType = CommandType.Text;
      this._commandCollection[1] = new SQLiteCommand();
      this._commandCollection[1].Connection = this.Connection;
      this._commandCollection[1].CommandText = "DELETE FROM [MouseFixations]";
      this._commandCollection[1].CommandType = CommandType.Text;
      this._commandCollection[2] = new SQLiteCommand();
      this._commandCollection[2].Connection = this.Connection;
      this._commandCollection[2].CommandText =
        "DELETE FROM MouseFixations\r\nWHERE     (SubjectName = @Original_SubjectName)";
      this._commandCollection[2].CommandType = CommandType.Text;
      this._commandCollection[2].Parameters.Add(
        new SQLiteParameter(
          "@Original_SubjectName",
          DbType.String,
          50,
          ParameterDirection.Input,
          0,
          0,
          "SubjectName",
          DataRowVersion.Original,
          false,
          null));
      this._commandCollection[3] = new SQLiteCommand();
      this._commandCollection[3].Connection = this.Connection;
      this._commandCollection[3].CommandText =
        "SELECT CountInTrial, ID, Length, PosX, PosY, StartTime, SubjectName, TrialID, Tri"
        + "alSequence FROM MouseFixations WHERE (SubjectName = @Param1)";
      this._commandCollection[3].CommandType = CommandType.Text;
      this._commandCollection[3].Parameters.Add(
        new SQLiteParameter(
          "@Param1",
          DbType.String,
          50,
          ParameterDirection.Input,
          0,
          0,
          "SubjectName",
          DataRowVersion.Current,
          false,
          null));
      this._commandCollection[4] = new SQLiteCommand();
      this._commandCollection[4].Connection = this.Connection;
      this._commandCollection[4].CommandText =
        "SELECT CountInTrial, ID, Length, PosX, PosY, StartTime, SubjectName, TrialID, Tri"
        + "alSequence FROM MouseFixations WHERE (SubjectName = @Param1) AND (TrialSequence " + "= @Param2)";
      this._commandCollection[4].CommandType = CommandType.Text;
      this._commandCollection[4].Parameters.Add(
        new SQLiteParameter(
          "@Param1",
          DbType.String,
          50,
          ParameterDirection.Input,
          0,
          0,
          "SubjectName",
          DataRowVersion.Current,
          false,
          null));
      this._commandCollection[4].Parameters.Add(
        new SQLiteParameter(
          "@Param2",
          DbType.Int32,
          4,
          ParameterDirection.Input,
          0,
          0,
          "TrialSequence",
          DataRowVersion.Current,
          false,
          null));
      this._commandCollection[5] = new SQLiteCommand();
      this._commandCollection[5].Connection = this.Connection;
      this._commandCollection[5].CommandText =
        "SELECT CountInTrial, ID, Length, PosX, PosY, StartTime, SubjectName, TrialID, Tri"
        + "alSequence FROM MouseFixations WHERE (SubjectName = @Param1) AND (TrialID = @Par" + "am2)";
      this._commandCollection[5].CommandType = CommandType.Text;
      this._commandCollection[5].Parameters.Add(
        new SQLiteParameter(
          "@Param1",
          DbType.String,
          50,
          ParameterDirection.Input,
          0,
          0,
          "SubjectName",
          DataRowVersion.Current,
          false,
          null));
      this._commandCollection[5].Parameters.Add(
        new SQLiteParameter(
          "@Param2",
          DbType.Int32,
          4,
          ParameterDirection.Input,
          0,
          0,
          "TrialID",
          DataRowVersion.Current,
          false,
          null));
      this._commandCollection[6] = new SQLiteCommand();
      this._commandCollection[6].Connection = this.Connection;
      this._commandCollection[6].CommandText =
        "SELECT CountInTrial, ID, Length, PosX, PosY, StartTime, SubjectName, TrialID, Tri"
        + "alSequence FROM MouseFixations WHERE (TrialID = @Param1)";
      this._commandCollection[6].CommandType = CommandType.Text;
      this._commandCollection[6].Parameters.Add(
        new SQLiteParameter(
          "@Param1",
          DbType.Int32,
          4,
          ParameterDirection.Input,
          0,
          0,
          "TrialID",
          DataRowVersion.Current,
          false,
          null));
      this._commandCollection[7] = new SQLiteCommand();
      this._commandCollection[7].Connection = this.Connection;
      this._commandCollection[7].CommandText =
        "SELECT     MAX(StartTime) AS MaxTime, TrialID\r\nFROM         MouseFixations\r\nGROUP"
        + " BY TrialID\r\nHAVING      (TrialID= @Param1)";
      this._commandCollection[7].CommandType = CommandType.Text;
      this._commandCollection[7].Parameters.Add(
        new SQLiteParameter(
          "@Param1",
          DbType.Int32,
          4,
          ParameterDirection.Input,
          0,
          0,
          "TrialID",
          DataRowVersion.Current,
          false,
          null));
    }

    /// <summary>
    ///   Initializes the connection.
    /// </summary>
    private void InitConnection()
    {
      this._connection = new SQLiteConnection();
      this._connection.ConnectionString =
        global::Ogama.Properties.Settings.Default.OgamaDatabaseTemplateConnectionString;
    }

    #endregion
  }

  /// <summary>
  ///   Represents the connection and commands used to retrieve and save data.
  /// </summary>
  public class SQLiteTadParams : Component
  {
    #region Fields

    /// <summary>
    ///   The _adapter
    /// </summary>
    private SQLiteDataAdapter _adapter;

    /// <summary>
    ///   The _command collection
    /// </summary>
    private SQLiteCommand[] _commandCollection;

    /// <summary>
    ///   The _connection
    /// </summary>
    private SQLiteConnection _connection;

    #endregion

    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="SQLiteTadParams" /> class.
    /// </summary>
    public SQLiteTadParams()
    {
      this.ClearBeforeFill = true;
    }

    #endregion

    #region Public Properties

    /// <summary>
    ///   Gets or sets a value indicating whether [clear before fill].
    /// </summary>
    /// <value><c>true</c> if [clear before fill]; otherwise, <c>false</c>.</value>
    public bool ClearBeforeFill { get; set; }

    #endregion

    #region Properties

    /// <summary>
    ///   Gets or sets the connection.
    /// </summary>
    /// <value>The connection.</value>
    internal SQLiteConnection Connection
    {
      get
      {
        if (this._connection == null)
        {
          this.InitConnection();
        }

        return this._connection;
      }

      set
      {
        this._connection = value;
        if (this.Adapter.InsertCommand != null)
        {
          this.Adapter.InsertCommand.Connection = value;
        }

        if (this.Adapter.DeleteCommand != null)
        {
          this.Adapter.DeleteCommand.Connection = value;
        }

        if (this.Adapter.UpdateCommand != null)
        {
          this.Adapter.UpdateCommand.Connection = value;
        }

        for (int i = 0; i < this.CommandCollection.Length; i = (i + 1))
        {
          if ((this.CommandCollection[i] != null))
          {
            this.CommandCollection[i].Connection = value;
          }
        }
      }
    }

    /// <summary>
    ///   Gets the command collection.
    /// </summary>
    /// <value>The command collection.</value>
    protected SQLiteCommand[] CommandCollection
    {
      get
      {
        if (this._commandCollection == null)
        {
          this.InitCommandCollection();
        }

        return this._commandCollection;
      }
    }

    /// <summary>
    ///   Gets the adapter.
    /// </summary>
    /// <value>The adapter.</value>
    private SQLiteDataAdapter Adapter
    {
      get
      {
        if (this._adapter == null)
        {
          this.InitAdapter();
        }

        return this._adapter;
      }
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// Deletes the specified original_ identifier.
    /// </summary>
    /// <param name="Original_ID">
    /// The original_ identifier.
    /// </param>
    /// <param name="Original_Param">
    /// The original_ parameter.
    /// </param>
    /// <param name="Original_Description">
    /// The original_ description.
    /// </param>
    /// <returns>
    /// System.Int32.
    /// </returns>
    /// <exception cref="System.ArgumentNullException">
    /// Original_Param
    /// </exception>
    [DataObjectMethodAttribute(DataObjectMethodType.Delete,
     true)]
    public virtual int Delete(int Original_ID, string Original_Param, string Original_Description)
    {
      this.Adapter.DeleteCommand.Parameters[0].Value = Original_ID;
      if (Original_Param == null)
      {
        throw new ArgumentNullException("Original_Param");
      }

      this.Adapter.DeleteCommand.Parameters[1].Value = Original_Param;
      if (Original_Description == null)
      {
        this.Adapter.DeleteCommand.Parameters[2].Value = 1;
        this.Adapter.DeleteCommand.Parameters[3].Value = DBNull.Value;
      }
      else
      {
        this.Adapter.DeleteCommand.Parameters[2].Value = 0;
        this.Adapter.DeleteCommand.Parameters[3].Value = Original_Description;
      }

      ConnectionState previousConnectionState = this.Adapter.DeleteCommand.Connection.State;
      if ((this.Adapter.DeleteCommand.Connection.State & ConnectionState.Open)
           != ConnectionState.Open)
      {
        this.Adapter.DeleteCommand.Connection.Open();
      }

      try
      {
        int returnValue = this.Adapter.DeleteCommand.ExecuteNonQuery();
        return returnValue;
      }
      finally
      {
        if (previousConnectionState == ConnectionState.Closed)
        {
          this.Adapter.DeleteCommand.Connection.Close();
        }
      }
    }

    /// <summary>
    /// Fills the specified data table.
    /// </summary>
    /// <param name="dataTable">
    /// The data table.
    /// </param>
    /// <returns>
    /// System.Int32.
    /// </returns>
    [DataObjectMethodAttribute(DataObjectMethodType.Fill,
     true)]
    public virtual int Fill(SQLiteOgamaDataSet.ParamsDataTable dataTable)
    {
      this.Adapter.SelectCommand = this.CommandCollection[0];
      if (this.ClearBeforeFill)
      {
        dataTable.Clear();
      }

      int returnValue = this.Adapter.Fill(dataTable);
      return returnValue;
    }

    /// <summary>
    ///   Gets the data.
    /// </summary>
    /// <returns>SQLiteOgamaDataSet.ParamsDataTable.</returns>
    [DataObjectMethodAttribute(DataObjectMethodType.Select,
     true)]
    public virtual SQLiteOgamaDataSet.ParamsDataTable GetData()
    {
      this.Adapter.SelectCommand = this.CommandCollection[0];
      var dataTable = new SQLiteOgamaDataSet.ParamsDataTable();
      this.Adapter.Fill(dataTable);
      return dataTable;
    }

    /// <summary>
    /// Inserts the specified parameter.
    /// </summary>
    /// <param name="Param">
    /// The parameter.
    /// </param>
    /// <param name="Description">
    /// The description.
    /// </param>
    /// <returns>
    /// System.Int32.
    /// </returns>
    /// <exception cref="System.ArgumentNullException">
    /// Param
    /// </exception>
    [DataObjectMethodAttribute(DataObjectMethodType.Insert,
     true)]
    public virtual int Insert(string Param, string Description)
    {
      if (Param == null)
      {
        throw new ArgumentNullException("Param");
      }

      this.Adapter.InsertCommand.Parameters[0].Value = Param;
      if (Description == null)
      {
        this.Adapter.InsertCommand.Parameters[1].Value = DBNull.Value;
      }
      else
      {
        this.Adapter.InsertCommand.Parameters[1].Value = Description;
      }

      ConnectionState previousConnectionState = this.Adapter.InsertCommand.Connection.State;
      if ((this.Adapter.InsertCommand.Connection.State & ConnectionState.Open)
           != ConnectionState.Open)
      {
        this.Adapter.InsertCommand.Connection.Open();
      }

      try
      {
        int returnValue = this.Adapter.InsertCommand.ExecuteNonQuery();
        return returnValue;
      }
      finally
      {
        if (previousConnectionState == ConnectionState.Closed)
        {
          this.Adapter.InsertCommand.Connection.Close();
        }
      }
    }

    /// <summary>
    /// Updates the specified data table.
    /// </summary>
    /// <param name="dataTable">
    /// The data table.
    /// </param>
    /// <returns>
    /// System.Int32.
    /// </returns>
    public virtual int Update(SQLiteOgamaDataSet.ParamsDataTable dataTable)
    {
      return this.Adapter.Update(dataTable);
    }

    /// <summary>
    /// Updates the specified data set.
    /// </summary>
    /// <param name="dataSet">
    /// The data set.
    /// </param>
    /// <returns>
    /// System.Int32.
    /// </returns>
    public virtual int Update(SQLiteOgamaDataSet dataSet)
    {
      return this.Adapter.Update(dataSet, "Params");
    }

    /// <summary>
    /// Updates the specified data row.
    /// </summary>
    /// <param name="dataRow">
    /// The data row.
    /// </param>
    /// <returns>
    /// System.Int32.
    /// </returns>
    public virtual int Update(DataRow dataRow)
    {
      return this.Adapter.Update(new[] { dataRow });
    }

    /// <summary>
    /// Updates the specified data rows.
    /// </summary>
    /// <param name="dataRows">
    /// The data rows.
    /// </param>
    /// <returns>
    /// System.Int32.
    /// </returns>
    public virtual int Update(DataRow[] dataRows)
    {
      return this.Adapter.Update(dataRows);
    }

    /// <summary>
    /// Updates the specified parameter.
    /// </summary>
    /// <param name="Param">
    /// The parameter.
    /// </param>
    /// <param name="Description">
    /// The description.
    /// </param>
    /// <param name="Original_ID">
    /// The original_ identifier.
    /// </param>
    /// <param name="Original_Param">
    /// The original_ parameter.
    /// </param>
    /// <param name="Original_Description">
    /// The original_ description.
    /// </param>
    /// <param name="ID">
    /// The identifier.
    /// </param>
    /// <returns>
    /// System.Int32.
    /// </returns>
    /// <exception cref="System.ArgumentNullException">
    /// Param
    ///   or
    ///   Original_Param
    /// </exception>
    [DataObjectMethodAttribute(DataObjectMethodType.Update,
     true)]
    public virtual int Update(
      string Param,
      string Description,
      int Original_ID,
      string Original_Param,
      string Original_Description,
      int ID)
    {
      if (Param == null)
      {
        throw new ArgumentNullException("Param");
      }

      this.Adapter.UpdateCommand.Parameters[0].Value = Param;
      if (Description == null)
      {
        this.Adapter.UpdateCommand.Parameters[1].Value = DBNull.Value;
      }
      else
      {
        this.Adapter.UpdateCommand.Parameters[1].Value = Description;
      }

      this.Adapter.UpdateCommand.Parameters[2].Value = Original_ID;
      if (Original_Param == null)
      {
        throw new ArgumentNullException("Original_Param");
      }

      this.Adapter.UpdateCommand.Parameters[3].Value = Original_Param;
      if (Original_Description == null)
      {
        this.Adapter.UpdateCommand.Parameters[4].Value = 1;
        this.Adapter.UpdateCommand.Parameters[5].Value = DBNull.Value;
      }
      else
      {
        this.Adapter.UpdateCommand.Parameters[4].Value = 0;
        this.Adapter.UpdateCommand.Parameters[5].Value = Original_Description;
      }

      this.Adapter.UpdateCommand.Parameters[6].Value = ID;
      ConnectionState previousConnectionState = this.Adapter.UpdateCommand.Connection.State;
      if ((this.Adapter.UpdateCommand.Connection.State & ConnectionState.Open)
           != ConnectionState.Open)
      {
        this.Adapter.UpdateCommand.Connection.Open();
      }

      try
      {
        int returnValue = this.Adapter.UpdateCommand.ExecuteNonQuery();
        return returnValue;
      }
      finally
      {
        if (previousConnectionState == ConnectionState.Closed)
        {
          this.Adapter.UpdateCommand.Connection.Close();
        }
      }
    }

    /// <summary>
    /// Updates the specified parameter.
    /// </summary>
    /// <param name="Param">
    /// The parameter.
    /// </param>
    /// <param name="Description">
    /// The description.
    /// </param>
    /// <param name="Original_ID">
    /// The original_ identifier.
    /// </param>
    /// <param name="Original_Param">
    /// The original_ parameter.
    /// </param>
    /// <param name="Original_Description">
    /// The original_ description.
    /// </param>
    /// <returns>
    /// System.Int32.
    /// </returns>
    [DataObjectMethodAttribute(DataObjectMethodType.Update,
     true)]
    public virtual int Update(
      string Param,
      string Description,
      int Original_ID,
      string Original_Param,
      string Original_Description)
    {
      return this.Update(Param, Description, Original_ID, Original_Param, Original_Description, Original_ID);
    }

    #endregion

    #region Methods

    /// <summary>
    ///   Initializes the adapter.
    /// </summary>
    private void InitAdapter()
    {
      this._adapter = new SQLiteDataAdapter();
      var tableMapping = new DataTableMapping();
      tableMapping.SourceTable = "Table";
      tableMapping.DataSetTable = "Params";
      tableMapping.ColumnMappings.Add("ID", "ID");
      tableMapping.ColumnMappings.Add("Param", "Param");
      tableMapping.ColumnMappings.Add("Description", "Description");
      this._adapter.TableMappings.Add(tableMapping);
      this._adapter.DeleteCommand = new SQLiteCommand();
      this._adapter.DeleteCommand.Connection = this.Connection;
      this._adapter.DeleteCommand.CommandText =
        "DELETE FROM [Params] WHERE (([ID] = @Original_ID) AND ([Param] = @Original_"
        + "Param) AND ((@IsNull_Description = 1 AND [Description] IS NULL) OR ([Description"
        + "] = @Original_Description)))";
      this._adapter.DeleteCommand.CommandType = CommandType.Text;
      this._adapter.DeleteCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_ID",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "ID",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.DeleteCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_Param",
          DbType.String,
          0,
          ParameterDirection.Input,
          0,
          0,
          "Param",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.DeleteCommand.Parameters.Add(
        new SQLiteParameter(
          "@IsNull_Description",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "Description",
          DataRowVersion.Original,
          true,
          null));
      this._adapter.DeleteCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_Description",
          DbType.String,
          0,
          ParameterDirection.Input,
          0,
          0,
          "Description",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.InsertCommand = new SQLiteCommand();
      this._adapter.InsertCommand.Connection = this.Connection;
      this._adapter.InsertCommand.CommandText =
        "INSERT INTO [Params] ([Param], [Description]) VALUES (@Param, @Description)"
        + ";\r\nSELECT ID, Param, Description FROM Params WHERE (ID = last_insert_rowid())";
      this._adapter.InsertCommand.CommandType = CommandType.Text;
      this._adapter.InsertCommand.Parameters.Add(
        new SQLiteParameter(
          "@Param",
          DbType.String,
          0,
          ParameterDirection.Input,
          0,
          0,
          "Param",
          DataRowVersion.Current,
          false,
          null));
      this._adapter.InsertCommand.Parameters.Add(
        new SQLiteParameter(
          "@Description",
          DbType.String,
          0,
          ParameterDirection.Input,
          0,
          0,
          "Description",
          DataRowVersion.Current,
          false,
          null));
      this._adapter.UpdateCommand = new SQLiteCommand();
      this._adapter.UpdateCommand.Connection = this.Connection;
      this._adapter.UpdateCommand.CommandText =
        @"UPDATE [Params] SET [Param] = @Param, [Description] = @Description WHERE (([ID] = @Original_ID) AND ([Param] = @Original_Param) AND ((@IsNull_Description = 1 AND [Description] IS NULL) OR ([Description] = @Original_Description)));
SELECT ID, Param, Description FROM Params WHERE (ID = @ID)";
      this._adapter.UpdateCommand.CommandType = CommandType.Text;
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@Param",
          DbType.String,
          0,
          ParameterDirection.Input,
          0,
          0,
          "Param",
          DataRowVersion.Current,
          false,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@Description",
          DbType.String,
          0,
          ParameterDirection.Input,
          0,
          0,
          "Description",
          DataRowVersion.Current,
          false,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_ID",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "ID",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_Param",
          DbType.String,
          0,
          ParameterDirection.Input,
          0,
          0,
          "Param",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@IsNull_Description",
          DbType.Int32,
          0,
          ParameterDirection.Input,
          0,
          0,
          "Description",
          DataRowVersion.Original,
          true,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@Original_Description",
          DbType.String,
          0,
          ParameterDirection.Input,
          0,
          0,
          "Description",
          DataRowVersion.Original,
          false,
          null));
      this._adapter.UpdateCommand.Parameters.Add(
        new SQLiteParameter(
          "@ID",
          DbType.Int32,
          4,
          ParameterDirection.Input,
          0,
          0,
          "ID",
          DataRowVersion.Current,
          false,
          null));
    }

    /// <summary>
    ///   Initializes the command collection.
    /// </summary>
    private void InitCommandCollection()
    {
      this._commandCollection = new SQLiteCommand[1];
      this._commandCollection[0] = new SQLiteCommand();
      this._commandCollection[0].Connection = this.Connection;
      this._commandCollection[0].CommandText = "SELECT ID, Param, Description FROM Params";
      this._commandCollection[0].CommandType = CommandType.Text;
    }

    /// <summary>
    ///   Initializes the connection.
    /// </summary>
    private void InitConnection()
    {
      this._connection = new SQLiteConnection();
      this._connection.ConnectionString =
        global::Ogama.Properties.Settings.Default.OgamaDatabaseTemplateConnectionString;
    }

    #endregion
  }
}

#pragma warning restore 1591