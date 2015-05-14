// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SQLiteBulkInsert.cs" company="Freie Universität Berlin">
//   OGAMA - open gaze and mouse analyzer 
//   Copyright (C) 2014 Dr. Adrian Voßkühler  
//   Licensed under GPL V3
// </copyright>
// <author>Adrian Voßkühler</author>
// <email>adrian@ogama.net</email>
// <summary>
//   Class SQLiteBulkInsert.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Ogama.DataSet
{
  using System;
  using System.Collections.Generic;
  using System.Data;
  using System.Data.SQLite;
  using System.Text;

  /// <summary>
  ///   Class SQLiteBulkInsert.
  /// </summary>
  public class SQLiteBulkInsert
  {
    #region Fields

    /// <summary>
    ///   The m_begin insert text
    /// </summary>
    private readonly string beginInsertText;

    /// <summary>
    /// The connection
    /// </summary>
    private readonly SQLiteConnection connection;

    /// <summary>
    ///   The sqliteParameters
    /// </summary>
    private readonly Dictionary<string, SQLiteParameter> sqliteParameters = new Dictionary<string, SQLiteParameter>();

    /// <summary>
    ///   The tableName
    /// </summary>
    private readonly string tableName;

    /// <summary>
    ///   The m_allow bulk insert
    /// </summary>
    private bool allowBulkInsert = true;

    /// <summary>
    ///   The M_CMD
    /// </summary>
    private SQLiteCommand sqliteCommand;

    /// <summary>
    ///   The m_commit maximum
    /// </summary>
    private uint commitMax = 10000;

    /// <summary>
    ///   The m_counter
    /// </summary>
    private uint counter;

    /// <summary>
    ///   The m_param delimiter
    /// </summary>
    private string paramDelim = ":";

    /// <summary>
    ///   The m_trans
    /// </summary>
    private SQLiteTransaction transaction;

    #endregion

    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="SQLiteBulkInsert"/> class.
    /// </summary>
    /// <param name="connection">
    /// The database connection.
    /// </param>
    /// <param name="tableName">
    /// Name of the table.
    /// </param>
    public SQLiteBulkInsert(SQLiteConnection connection, string tableName)
    {
      this.connection = connection;
      this.tableName = tableName;

      var query = new StringBuilder(255);
      query.Append("INSERT INTO [");
      query.Append(tableName);
      query.Append("] (");
      this.beginInsertText = query.ToString();
    }

    #endregion

    #region Public Properties

    /// <summary>
    ///   Gets or sets a value indicating whether [allow bulk insert].
    /// </summary>
    /// <value><c>true</c> if [allow bulk insert]; otherwise, <c>false</c>.</value>
    public bool AllowBulkInsert
    {
      get
      {
        return this.allowBulkInsert;
      }

      set
      {
        this.allowBulkInsert = value;
      }
    }

    /// <summary>
    ///   Gets the command text.
    /// </summary>
    /// <value>The command text.</value>
    /// <exception cref="System.Data.SQLite.SQLiteException">You must add at least one parameter.</exception>
    public string CommandText
    {
      get
      {
        if (this.sqliteParameters.Count < 1)
        {
          throw new SQLiteException("You must add at least one parameter.");
        }

        var sb = new StringBuilder(255);
        sb.Append(this.beginInsertText);

        foreach (string param in this.sqliteParameters.Keys)
        {
          sb.Append('[');
          sb.Append(param);
          sb.Append(']');
          sb.Append(", ");
        }

        sb.Remove(sb.Length - 2, 2);

        sb.Append(") VALUES (");

        foreach (string param in this.sqliteParameters.Keys)
        {
          sb.Append(this.paramDelim);
          sb.Append(param);
          sb.Append(", ");
        }

        sb.Remove(sb.Length - 2, 2);

        sb.Append(")");

        return sb.ToString();
      }
    }

    /// <summary>
    ///   Gets or sets the commit maximum.
    /// </summary>
    /// <value>The commit maximum.</value>
    public uint CommitMax
    {
      get
      {
        return this.commitMax;
      }

      set
      {
        this.commitMax = value;
      }
    }

    /// <summary>
    ///   Gets the parameter delimiter.
    /// </summary>
    /// <value>The parameter delimiter.</value>
    public string ParamDelimiter
    {
      get
      {
        return this.paramDelim;
      }
    }

    /// <summary>
    ///   Gets the name of the table.
    /// </summary>
    /// <value>The name of the table.</value>
    public string TableName
    {
      get
      {
        return this.tableName;
      }
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// Adds the parameter.
    /// </summary>
    /// <param name="name">
    /// The name.
    /// </param>
    /// <param name="dbType">
    /// Type of the database.
    /// </param>
    public void AddParameter(string name, DbType dbType)
    {
      var param = new SQLiteParameter(this.paramDelim + name, dbType);
      this.sqliteParameters.Add(name, param);
    }

    /// <summary>
    ///   Flushes this instance.
    /// </summary>
    /// <exception cref="System.Exception">Could not commit transaction. See InnerException for more details</exception>
    public void Flush()
    {
      try
      {
        if (this.transaction != null)
        {
          this.transaction.Commit();
        }
      }
      catch (Exception ex)
      {
        throw new Exception("Could not commit transaction. See InnerException for more details", ex);
      }
      finally
      {
        if (this.transaction != null)
        {
          this.transaction.Dispose();
        }

        this.transaction = null;
        this.counter = 0;
      }
    }

    /// <summary>
    /// Inserts the specified parameter values.
    /// </summary>
    /// <param name="paramValues">
    /// The parameter values.
    /// </param>
    /// <exception cref="System.Exception">
    /// The values array count must be equal to the count of the number of parameters.
    /// </exception>
    public void Insert(object[] paramValues)
    {
      if (paramValues.Length != this.sqliteParameters.Count)
      {
        throw new Exception("The values array count must be equal to the count of the number of parameters.");
      }

      this.counter++;

      if (this.counter == 1)
      {
        if (this.allowBulkInsert)
        {
          this.transaction = this.connection.BeginTransaction();
        }

        this.sqliteCommand = this.connection.CreateCommand();
        foreach (SQLiteParameter par in this.sqliteParameters.Values)
        {
          this.sqliteCommand.Parameters.Add(par);
        }

        this.sqliteCommand.CommandText = this.CommandText;
      }

      int i = 0;
      foreach (SQLiteParameter par in this.sqliteParameters.Values)
      {
        par.Value = paramValues[i];
        i++;
      }

      this.sqliteCommand.ExecuteNonQuery();

      if (this.counter == this.commitMax)
      {
        try
        {
          if (this.transaction != null)
          {
            this.transaction.Commit();
          }
        }
        catch (Exception)
        {
        }
        finally
        {
          if (this.transaction != null)
          {
            this.transaction.Dispose();
            this.transaction = null;
          }

          this.counter = 0;
        }
      }
    }

    #endregion
  }
}