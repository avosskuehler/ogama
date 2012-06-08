// <copyright file="FormWithAccellerators.cs" company="FU Berlin">
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

namespace Ogama.Modules.Common.FormTemplates
{
  using System;
  using System.Collections;
  using System.ComponentModel;
  using System.Data;
  using System.Drawing;
  using System.Windows.Forms;

  using Ogama.ExceptionHandling;

  /// <summary>
  /// Inherits <see cref="Form"/>.
  /// Abstract class to implement keyboard accelleration in forms.
  /// </summary>
  public partial class FormWithAccellerators : Form
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
    /// Dictionary of accelerators
    /// </summary>
    private readonly IDictionary accelerators;

    /// <summary>
    /// A cell style for read only columns.
    /// </summary>
    private DataGridViewCellStyle readOnlyCellStyle;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the FormWithAccellerators class.
    /// Initializes accelerator table.
    /// </summary>
    public FormWithAccellerators()
    {
      this.accelerators = new Hashtable();

      this.readOnlyCellStyle = new DataGridViewCellStyle();
      this.readOnlyCellStyle.BackColor = SystemColors.ControlLight;
      this.readOnlyCellStyle.ForeColor = SystemColors.GrayText;
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Enumerations                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region ENUMS

    /// <summary>
    /// Delegate. Starts action that is assigned to accelerator.
    /// </summary>
    protected delegate void AcceleratorAction();

    #endregion ENUMS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets or sets CellStyle for read only columns.
    /// </summary>
    /// <value>The <see cref="DataGridViewCellStyle"/> that should be used for 
    /// read only column cell styles.</value>
    [Description("Cell Style for read only columns")]
    [Category("Appearance")]
    public DataGridViewCellStyle ReadOnlyCellStyle
    {
      get { return this.readOnlyCellStyle; }
      set { this.readOnlyCellStyle = value; }
    }

    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Public methods                                                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region PUBLICMETHODS

    /// <summary>
    /// Specialized data grid view error handler.
    /// Shows some message boxes for known errors and otherwise raises global policy
    /// exception handling.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A <see cref="DataGridViewDataErrorEventArgs"/> that provides 
    /// data for the DataError event.</param>
    public static void HandleDataGridViewError(object sender, DataGridViewDataErrorEventArgs e)
    {
      string errorMessage = string.Empty;
      if (e.Exception.GetBaseException() is NoNullAllowedException)
      {
        errorMessage = "This column is not allowed to be empty." +
          Environment.NewLine + "Please enter a value for that column.";
        ExceptionMethods.ProcessErrorMessage(errorMessage);

        SelectTextOfEditingControlTextBox(sender);
      }
      else if (e.Exception.GetBaseException() is FormatException)
      {
        errorMessage = "The entered value has not the correct format for that column." +
          Environment.NewLine + "Some columns require integer values, some other accept only strings.";
        ExceptionMethods.ProcessErrorMessage(errorMessage);

        SelectTextOfEditingControlTextBox(sender);
      }
      else if (e.Exception.GetBaseException() is ConstraintException)
      {
        errorMessage = "The entered value is violating data constraint rules." +
          Environment.NewLine + "For example: the subject name column has to be unique.";
        ExceptionMethods.ProcessErrorMessage(errorMessage);

        SelectTextOfEditingControlTextBox(sender);
      }
      else if (e.Exception.GetBaseException() is ArgumentException)
      {
        errorMessage = "The entered value is violating columns argument rules." +
          Environment.NewLine + "For example: the given value does not matches the columns type.";
        ExceptionMethods.ProcessErrorMessage(errorMessage);

        SelectTextOfEditingControlTextBox(sender);
      }
      else
      {
        ExceptionMethods.HandleException(e.Exception);
      }
    }

    /// <summary>
    /// This method sets all columns in the given <see cref="DataGridView"/>
    /// to read only style.
    /// </summary>
    /// <param name="dataGridView">The <see cref="DataGridView"/>
    /// for which the columns should be marked to be read-only.</param>
    protected void SetDataGridViewColumnsToReadOnlyStyle(DataGridView dataGridView)
    {
      if (dataGridView.Columns.Count > 0)
      {
        foreach (DataGridViewColumn column in dataGridView.Columns)
        {
          column.ReadOnly = true;
          column.DefaultCellStyle = this.ReadOnlyCellStyle;
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

    /// <summary>
    /// Initializes accelerator keys.
    /// </summary>
    protected virtual void InitAccelerators()
    {
    }

    /// <summary>
    /// Overridden. Invokes accelerator action delegate.
    /// </summary>
    /// <param name="msg">Message send from windows</param>
    /// <param name="keyData">Key params</param>
    /// <returns><strong>True</strong> if succesfully processed, otherwise
    /// <strong>false</strong>.</returns>
    protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
    {
      AcceleratorAction action = this.accelerators[keyData] as AcceleratorAction;
      if (action != null)
      {
        action();
        return true;
      }

      return base.ProcessCmdKey(ref msg, keyData);
    }

    #endregion //OVERRIDES

    ///////////////////////////////////////////////////////////////////////////////
    // Methods for doing main class job                                          //
    ///////////////////////////////////////////////////////////////////////////////
    #region METHODS

    /// <summary>
    /// Sets delegate for given key.
    /// </summary>
    /// <param name="keyCombination">Key to wire action to.</param>
    /// <param name="action">Delegate function.</param>
    protected void SetAccelerator(Keys keyCombination, AcceleratorAction action)
    {
      this.accelerators[keyCombination] = action;
    }

    /// <summary>
    /// This method casts the sender to a <see cref="DataGridView"/>
    /// and checks whether it has a EditingControl, if this
    /// is of type <see cref="TextBox"/>, select the text in it.
    /// </summary>
    /// <param name="sender">A <see cref="DataGridView"/>.</param>
    private static void SelectTextOfEditingControlTextBox(object sender)
    {
      DataGridView view = (DataGridView)sender;
      if (view.EditingControl != null && view.EditingControl is TextBox)
      {
        TextBox textBoxControl = (TextBox)view.EditingControl;
        textBoxControl.SelectAll();
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
