// <copyright file="DefaultVariable.cs" company="FU Berlin">
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

namespace Ogama.Modules.Statistics.Variables
{
  using System;
  using System.Windows.Forms;

  /// <summary>
  /// This class is used to define a default variable that can be calculated
  /// via the statistics interface.
  /// </summary>
  public abstract class DefaultVariable
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
    /// The short name (8 characters) of the column in which the value should be written.
    /// </summary>
    private string columnName;

    /// <summary>
    /// The long description of the column that describes the variable.
    /// </summary>
    private string columnDescription;

    /// <summary>
    /// The <see cref="Type"/> of the column contents.
    /// </summary>
    private Type columnType;

    /// <summary>
    /// A <see cref="string"/> with the column formatting. (e.g. "N2")
    /// </summary>
    private string columnFormat;

    /// <summary>
    /// The <see cref="CheckBox"/> that enables or disables this variable.
    /// </summary>
    private CheckBox checkBox;

    /// <summary>
    /// A <see cref="string"/> describing the return values of this
    /// variable.
    /// </summary>
    private string returnValues;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the DefaultVariable class.
    /// </summary>
    /// <param name="newColumnName">The short name (8 characters) of the column in which the value should be written.</param>
    /// <param name="newColumnDescription">The long description of the column that describes the variable.</param>
    /// <param name="newColumnType">The <see cref="Type"/> of the column contents.</param>
    /// <param name="newColumnFormat">A <see cref="string"/> with the column formatting. (e.g. "N2")</param>
    /// <param name="newCheckBox">The <see cref="CheckBox"/> that enables or disables this variable.</param>
    /// <param name="newReturnValues">A <see cref="string"/> describing the return values of this
    /// variable.</param>
    public DefaultVariable(
      string newColumnName,
      string newColumnDescription,
      Type newColumnType,
      string newColumnFormat,
      CheckBox newCheckBox,
      string newReturnValues)
    {
      this.columnName = newColumnName;
      this.columnDescription = newColumnDescription;
      this.columnType = newColumnType;
      this.columnFormat = newColumnFormat;
      this.checkBox = newCheckBox;
      this.returnValues = newReturnValues;
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Enumerations                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region ENUMS
    #endregion ENUMS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets the short name (8 characters) of the column in which the value should be written.
    /// </summary>
    public string ColumnName
    {
      get { return this.columnName; }
    }

    /// <summary>
    /// Gets the long description of the column that describes the variable.
    /// </summary>
    public string ColumnDescription
    {
      get { return this.columnDescription; }
    }

    /// <summary>
    /// Gets the <see cref="Type"/> of the column contents.
    /// </summary>
    public Type ColumnType
    {
      get { return this.columnType; }
    }

    /// <summary>
    /// Gets a <see cref="string"/> with the column formatting. (e.g. "N2")
    /// </summary>
    public string ColumnFormat
    {
      get { return this.columnFormat; }
    }

    /// <summary>
    /// Gets the <see cref="CheckBox"/> that enables or disables this variable.
    /// </summary>
    public CheckBox CheckBox
    {
      get { return this.checkBox; }
    }

    /// <summary>
    /// Gets the <see cref="string"/> describing the return values of this
    /// variable.
    /// </summary>
    public string ReturnValues
    {
      get { return this.returnValues; }
    }

    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Public methods                                                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region PUBLICMETHODS
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
    #endregion //BACKGROUNDWORKER

    ///////////////////////////////////////////////////////////////////////////////
    // Methods for doing main class job                                          //
    ///////////////////////////////////////////////////////////////////////////////
    #region PRIVATEMETHODS
    #endregion //PRIVATEMETHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER
    #endregion //HELPER
  }
}
