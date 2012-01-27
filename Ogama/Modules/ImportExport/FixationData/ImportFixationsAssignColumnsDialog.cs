// <copyright file="ImportFixationsAssignColumnsDialog.cs" company="FU Berlin">
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

namespace Ogama.Modules.ImportExport.FixationData
{
  using System;
  using System.Windows.Forms;

  using Ogama.ExceptionHandling;
  using Ogama.Modules.Common.FormTemplates;
  using Ogama.Modules.ImportExport.Common;

  /// <summary>
  /// This dialog <see cref="Form"/> derives from <see cref="FormWithAccellerators"/>
  /// This is the third part of the fixation import assistant.
  /// Here the custom assigning of import columns with ogama columns is specified.
  /// </summary>
  public partial class ImportFixationsAssignColumnsDialog : FormWithAccellerators
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
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the ImportFixationsAssignColumnsDialog class.
    /// </summary>
    public ImportFixationsAssignColumnsDialog()
    {
      this.InitializeComponent();
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES
    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler                                                              //
    ///////////////////////////////////////////////////////////////////////////////
    #region EVENTS

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler for UI, Menu, Buttons, Toolbars etc.                         //
    ///////////////////////////////////////////////////////////////////////////////
    #region WINDOWSEVENTHANDLER

    /// <summary>
    /// The <see cref="Form.Load"/> event handler. Updates UI.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void frmImportFixationsAssignColumns_Load(object sender, EventArgs e)
    {
      this.UpdateUIWithSettings();
    }

    /// <summary>
    /// The <see cref="Form.FormClosing"/> event handler. 
    /// If something is incorrect assigned the dialog result is 
    /// set to <see cref="DialogResult.Retry"/>. 
    /// When trying to close the form and this state is set
    /// closing will be cancelled.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A <see cref="FormClosingEventArgs"/> with the event data.</param>
    private void frmImportFixationsAssignColumns_FormClosing(object sender, FormClosingEventArgs e)
    {
      if (this.DialogResult == DialogResult.Retry)
      {
        e.Cancel = true;
      }
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for
    /// the <see cref="Button"/> <see cref="btnNext"/>.
    /// Save assignments and if it is not succesful set the
    /// <see cref="DialogResult.Retry"/> to cancel closing
    /// during <see cref="Form.FormClosing"/> and have a second try.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnNext_Click(object sender, EventArgs e)
    {
      this.SaveColumnAssignments();
      if (!this.ValidateAssignments())
      {
        this.DialogResult = DialogResult.Retry;
      }
    }

    /// <summary>
    /// The <see cref="DataGridView.CellValidated"/> event handler for
    /// the <see cref="DataGridView"/> <see cref="dGVAssignments"/>.
    /// Saves column assignments to <see cref="ASCIISettings.ColumnAssignments"/>.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A <see cref="DataGridViewCellEventArgs"/> with the event data.</param>
    private void dGVAssignments_CellValidated(object sender, DataGridViewCellEventArgs e)
    {
      this.SaveColumnAssignments();
    }

    /// <summary>
    /// The <see cref="DataGridView.DataError"/> event handler for the
    /// <see cref="DataGridView"/> <see cref="dGVAssignments"/>.
    /// Does special error handling for common errors, e.g.
    /// shows a message box with information how to avoid this error.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A <see cref="DataGridViewDataErrorEventArgs"/> that provides 
    /// data for the DataError event.</param>
    private void dGVAssignments_DataError(object sender, DataGridViewDataErrorEventArgs e)
    {
      this.dGVAssignments.Rows.Clear();
      HandleDataGridViewError(sender, e);
      this.UpdateUIWithSettings();
    }

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
    /// This method saves the current column assignments to
    /// the <see cref="ASCIISettings.ColumnAssignments"/>
    /// It also updates the <see cref="TrialSequenceImportModes"/>
    /// and <see cref="StimuliImportModes"/> of the 
    /// <see cref="ImportFixations.DetectionSetting"/>
    /// </summary>
    private void SaveColumnAssignments()
    {
      ImportFixations.ASCIISettings.ColumnAssignments.Clear();
      foreach (DataGridViewRow row in this.dGVAssignments.Rows)
      {
        string key = row.Cells[0].Value.ToString();
        string value = row.Cells[1].Value == null ? string.Empty : row.Cells[1].Value.ToString();
        ImportFixations.ASCIISettings.ColumnAssignments.Add(key, value);
      }

      // Set UseImportColumnFlag, if the trialID column is assigned
      if (ImportFixations.ASCIISettings.ColumnAssignments["TrialID"] != null &&
        ImportFixations.ASCIISettings.ColumnAssignments["TrialID"] != string.Empty)
      {
        ImportFixations.DetectionSetting.TrialImportMode = TrialSequenceImportModes.UseImportColumn;
      }
      else
      {
        ImportFixations.DetectionSetting.TrialImportMode = TrialSequenceImportModes.UseMSGLines;
      }

      // Set UseImportColumnFlag, if the StimulusFile column is assigned
      if (ImportFixations.ASCIISettings.ColumnAssignments["StimulusFile"] != null &&
        ImportFixations.ASCIISettings.ColumnAssignments["StimulusFile"] != string.Empty)
      {
        ImportFixations.DetectionSetting.StimuliImportMode = StimuliImportModes.UseImportColumn;
      }
      else
      {
        ImportFixations.DetectionSetting.StimuliImportMode = StimuliImportModes.SearchForImageEnding;
      }
    }

    /// <summary>
    /// This method checks for assigning of all minimum relevant columns,
    /// and returns false if some are missing.
    /// </summary>
    /// <returns><strong>True</strong> if succesful, otherwise
    /// <strong>false</strong>.</returns>
    private bool ValidateAssignments()
    {
      // Check for missing time column.
      if (ImportFixations.ASCIISettings.ColumnAssignments["StartTime"] == null ||
        ImportFixations.ASCIISettings.ColumnAssignments["StartTime"] == string.Empty ||
        ImportFixations.ASCIISettings.ColumnAssignments["PosX"] == null ||
        ImportFixations.ASCIISettings.ColumnAssignments["PosX"] == string.Empty ||
        ImportFixations.ASCIISettings.ColumnAssignments["PosY"] == null ||
        ImportFixations.ASCIISettings.ColumnAssignments["PosY"] == string.Empty)
      {
        string message =
          "You have to define at least the trial id, start time, posx and posY columns ...";
        ExceptionMethods.ProcessMessage("Define columns", message);
        return false;
      }

      // If no subject column is specified show subject name dialog.
      if (ImportFixations.ASCIISettings.ColumnAssignments["SubjectName"] == null ||
        ImportFixations.ASCIISettings.ColumnAssignments["SubjectName"] == string.Empty)
      {
        AskForSubjectNameDialog dlg = new AskForSubjectNameDialog(true);
        dlg.SubjectName = ImportFixations.DetectionSetting.SubjectName;
        if (dlg.ShowDialog() == DialogResult.OK)
        {
          ImportFixations.DetectionSetting.SubjectName = dlg.SubjectName;
        }
        else
        {
          return false;
        }
      }

      return true;
    }

    /// <summary>
    /// This method updates all fields of the UI with the
    /// current settings. Especially the columns of the import
    /// file are written in the drop down combo box list.
    /// </summary>
    private void UpdateUIWithSettings()
    {
      this.FillLeftAssignColumn();
      this.FillRightAssignColumn();

      for (int i = 0; i < this.dGVAssignments.Rows.Count; i++)
      {
        string key = (string)this.dGVAssignments[0, i].Value;

        if (ImportFixations.ASCIISettings.ColumnAssignments.ContainsKey(key)
          && ImportFixations.ASCIISettings.ColumnAssignments[key] != string.Empty)
        {
          this.dGVAssignments[1, i].Value = ImportFixations.ASCIISettings.ColumnAssignments[key];
        }
      }
    }

    #endregion //METHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER

    /// <summary>
    /// Fills left column of columns assignment datagridview 
    /// with entrys of Ogama aoi table database columns
    /// </summary>
    private void FillLeftAssignColumn()
    {
      DataGridViewRowCollection rows = this.dGVAssignments.Rows;
      rows.Clear();
      string[] row0 = { "SubjectName", string.Empty };
      string[] row1 = { "TrialID", string.Empty };
      string[] row2 = { "StimulusFile", string.Empty };
      string[] row3 = { "CountInTrial", string.Empty };
      string[] row4 = { "StartTime", string.Empty };
      string[] row5 = { "EndTime", string.Empty };
      string[] row6 = { "Length", string.Empty };
      string[] row7 = { "PosX", string.Empty };
      string[] row8 = { "PosY", string.Empty };
      rows.Add(row0);
      rows.Add(row1);
      rows.Add(row2);
      rows.Add(row3);
      rows.Add(row4);
      rows.Add(row5);
      rows.Add(row6);
      rows.Add(row7);
      rows.Add(row8);
    }

    /// <summary>
    /// Fills right column of columns assignment datagridview 
    /// with entrys of import table columns
    /// </summary>
    private void FillRightAssignColumn()
    {
      // Fill drop down column in assign data grid view with column entries from text file
      this.ColumnImportColumns.Items.Clear();
      this.ColumnImportColumns.Items.Add(string.Empty);
      foreach (string header in ImportFixations.ASCIISettings.ColumnHeaders)
      {
        this.ColumnImportColumns.Items.Add(header);
      }

      // Prefill column assignments, if parts of the import column 
      // headers are contained in ogamas column headers.
      for (int i = 0; i < this.dGVAssignments.Rows.Count; i++)
      {
        string left = this.dGVAssignments[0, i].Value.ToString();

        foreach (string item in this.ColumnImportColumns.Items)
        {
          if (left.Contains(item) && item != string.Empty)
          {
            this.dGVAssignments[1, i].Value = item;
          }
        }
      }
    }

    #endregion //HELPER
  }
}