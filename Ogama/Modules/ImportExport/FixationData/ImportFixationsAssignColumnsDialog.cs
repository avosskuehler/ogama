// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ImportFixationsAssignColumnsDialog.cs" company="Freie Universität Berlin">
//   OGAMA - open gaze and mouse analyzer 
//   Copyright (C) 2015 Dr. Adrian Voßkühler  
//   Licensed under GPL V3
// </copyright>
// <author>Adrian Voßkühler</author>
// <email>adrian@ogama.net</email>
// <summary>
//   This dialog <see cref="Form" /> derives from <see cref="FormWithAccellerators" />
//   This is the third part of the fixation import assistant.
//   Here the custom assigning of import columns with ogama columns is specified.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Ogama.Modules.ImportExport.FixationData
{
  using System;
  using System.Windows.Forms;

  using Ogama.ExceptionHandling;
  using Ogama.Modules.Common.FormTemplates;
  using Ogama.Modules.ImportExport.Common;

  /// <summary>
  ///   This dialog <see cref="Form" /> derives from <see cref="FormWithAccellerators" />
  ///   This is the third part of the fixation import assistant.
  ///   Here the custom assigning of import columns with ogama columns is specified.
  /// </summary>
  public partial class ImportFixationsAssignColumnsDialog : FormWithAccellerators
  {
    ///////////////////////////////////////////////////////////////////////////////
    // Defining Constants                                                        //
    ///////////////////////////////////////////////////////////////////////////////

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Variables, Enumerations, Events                                  //
    ///////////////////////////////////////////////////////////////////////////////

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the ImportFixationsAssignColumnsDialog class.
    /// </summary>
    public ImportFixationsAssignColumnsDialog()
    {
      this.InitializeComponent();
    }

    #endregion

    #region Methods

    /// <summary>
    ///   Fills left column of columns assignment data grid view
    ///   with entries of Ogama aoi table database columns
    /// </summary>
    private void FillLeftAssignColumn()
    {
      DataGridViewRowCollection rows = this.dGVAssignments.Rows;
      rows.Clear();
      string[] row0 = { "SubjectName", string.Empty };
      string[] row1 = { "TrialSequence", string.Empty };
      string[] row2 = { "TrialID", string.Empty };
      string[] row3 = { "TrialImage", string.Empty };
      string[] row4 = { "CountInTrial", string.Empty };
      string[] row5 = { "StartTime", string.Empty };
      string[] row6 = { "EndTime", string.Empty };
      string[] row7 = { "Length", string.Empty };
      string[] row8 = { "PosX", string.Empty };
      string[] row9 = { "PosY", string.Empty };
      rows.Add(row0);
      rows.Add(row1);
      rows.Add(row2);
      rows.Add(row3);
      rows.Add(row4);
      rows.Add(row5);
      rows.Add(row6);
      rows.Add(row7);
      rows.Add(row8);
      rows.Add(row9);
    }

    /// <summary>
    ///   Fills right column of columns assignment data grid view
    ///   with entries of import table columns
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

    /// <summary>
    ///   This method saves the current column assignments to
    ///   the <see cref="ASCIISettings.ColumnAssignments" />
    ///   It also updates the <see cref="TrialSequenceImportModes" />
    ///   and <see cref="StimuliImportModes" /> of the
    ///   <see cref="ImportFixations.DetectionSetting" />
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
      if (ImportFixations.ASCIISettings.ColumnAssignments["TrialSequence"] != null
          && ImportFixations.ASCIISettings.ColumnAssignments["TrialSequence"] != string.Empty)
      {
        ImportFixations.DetectionSetting.TrialImportMode = TrialSequenceImportModes.UseImportColumn;
      }
      else
      {
        ImportFixations.DetectionSetting.TrialImportMode = TrialSequenceImportModes.UseMSGLines;
      }

      // Set UseImportColumnFlag, if the StimulusFile column is assigned
      if (ImportFixations.ASCIISettings.ColumnAssignments["TrialID"] != null
          && ImportFixations.ASCIISettings.ColumnAssignments["TrialID"] != string.Empty)
      {
        ImportFixations.DetectionSetting.StimuliImportMode = StimuliImportModes.UseImportColumn;
      }
      else
      {
        ImportFixations.DetectionSetting.StimuliImportMode = StimuliImportModes.SearchForImageEnding;
      }
    }

    /// <summary>
    ///   This method updates all fields of the UI with the
    ///   current settings. Especially the columns of the import
    ///   file are written in the drop down combo box list.
    /// </summary>
    private void UpdateUIWithSettings()
    {
      this.FillLeftAssignColumn();
      this.FillRightAssignColumn();

      for (int i = 0; i < this.dGVAssignments.Rows.Count; i++)
      {
        var key = (string)this.dGVAssignments[0, i].Value;

        if (ImportFixations.ASCIISettings.ColumnAssignments.ContainsKey(key)
            && ImportFixations.ASCIISettings.ColumnAssignments[key] != string.Empty)
        {
          this.dGVAssignments[1, i].Value = ImportFixations.ASCIISettings.ColumnAssignments[key];
        }
      }
    }

    /// <summary>
    ///   This method checks for assigning of all minimum relevant columns,
    ///   and returns false if some are missing.
    /// </summary>
    /// <returns>
    ///   <strong>True</strong> if successful, otherwise
    ///   <strong>false</strong>.
    /// </returns>
    private bool ValidateAssignments()
    {
      // Check for missing time column.
      if (ImportFixations.ASCIISettings.ColumnAssignments["StartTime"] == null
          || ImportFixations.ASCIISettings.ColumnAssignments["StartTime"] == string.Empty
          || ImportFixations.ASCIISettings.ColumnAssignments["PosX"] == null
          || ImportFixations.ASCIISettings.ColumnAssignments["PosX"] == string.Empty
          || ImportFixations.ASCIISettings.ColumnAssignments["PosY"] == null
          || ImportFixations.ASCIISettings.ColumnAssignments["PosY"] == string.Empty)
      {
        string message = "You have to define at least the trial id, start time, posx and posY columns ...";
        ExceptionMethods.ProcessMessage("Define columns", message);
        return false;
      }

      // If no subject column is specified show subject name dialog.
      if (ImportFixations.ASCIISettings.ColumnAssignments["SubjectName"] == null
          || ImportFixations.ASCIISettings.ColumnAssignments["SubjectName"] == string.Empty)
      {
        var dlg = new AskForSubjectNameDialog(true);
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

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler                                                              //
    ///////////////////////////////////////////////////////////////////////////////

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler for UI, Menu, Buttons, Toolbars etc.                         //
    ///////////////////////////////////////////////////////////////////////////////

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for
    ///   the <see cref="Button"/> <see cref="btnNext"/>.
    ///   Save assignments and if it is not successful set the
    ///   <see cref="DialogResult.Retry"/> to cancel closing
    ///   during <see cref="Form.FormClosing"/> and have a second try.
    /// </summary>
    /// <param name="sender">
    /// Source of the event.
    /// </param>
    /// <param name="e">
    /// An empty <see cref="EventArgs"/>
    /// </param>
    private void BtnNextClick(object sender, EventArgs e)
    {
      this.SaveColumnAssignments();
      if (!this.ValidateAssignments())
      {
        this.DialogResult = DialogResult.Retry;
      }
    }

    /// <summary>
    /// The <see cref="DataGridView.CellValidated"/> event handler for
    ///   the <see cref="DataGridView"/> <see cref="dGVAssignments"/>.
    ///   Saves column assignments to <see cref="ASCIISettings.ColumnAssignments"/>.
    /// </summary>
    /// <param name="sender">
    /// Source of the event.
    /// </param>
    /// <param name="e">
    /// A <see cref="DataGridViewCellEventArgs"/> with the event data.
    /// </param>
    private void DGvAssignmentsCellValidated(object sender, DataGridViewCellEventArgs e)
    {
      this.SaveColumnAssignments();
    }

    /// <summary>
    /// The <see cref="DataGridView.DataError"/> event handler for the
    ///   <see cref="DataGridView"/> <see cref="dGVAssignments"/>.
    ///   Does special error handling for common errors, e.g.
    ///   shows a message box with information how to avoid this error.
    /// </summary>
    /// <param name="sender">
    /// Source of the event.
    /// </param>
    /// <param name="e">
    /// A <see cref="DataGridViewDataErrorEventArgs"/> that provides
    ///   data for the DataError event.
    /// </param>
    private void DGvAssignmentsDataError(object sender, DataGridViewDataErrorEventArgs e)
    {
      this.dGVAssignments.Rows.Clear();
      HandleDataGridViewError(sender, e);
      this.UpdateUIWithSettings();
    }

    /// <summary>
    /// The <see cref="Form.FormClosing"/> event handler.
    ///   If something is incorrect assigned the dialog result is
    ///   set to <see cref="DialogResult.Retry"/>.
    ///   When trying to close the form and this state is set
    ///   closing will be cancelled.
    /// </summary>
    /// <param name="sender">
    /// Source of the event.
    /// </param>
    /// <param name="e">
    /// A <see cref="FormClosingEventArgs"/> with the event data.
    /// </param>
    private void FrmImportFixationsAssignColumnsFormClosing(object sender, FormClosingEventArgs e)
    {
      if (this.DialogResult == DialogResult.Retry)
      {
        e.Cancel = true;
      }
    }

    /// <summary>
    /// The <see cref="Form.Load"/> event handler. Updates UI.
    /// </summary>
    /// <param name="sender">
    /// Source of the event.
    /// </param>
    /// <param name="e">
    /// An empty <see cref="EventArgs"/>
    /// </param>
    private void FrmImportFixationsAssignColumnsLoad(object sender, EventArgs e)
    {
      this.UpdateUIWithSettings();
    }

    #endregion

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler for Custom Defined Events                                    //
    ///////////////////////////////////////////////////////////////////////////////

    ///////////////////////////////////////////////////////////////////////////////
    // Methods and Eventhandling for Background tasks                            //
    ///////////////////////////////////////////////////////////////////////////////

    ///////////////////////////////////////////////////////////////////////////////
    // Inherited methods                                                         //
    ///////////////////////////////////////////////////////////////////////////////

    ///////////////////////////////////////////////////////////////////////////////
    // Methods for doing main class job                                          //
    ///////////////////////////////////////////////////////////////////////////////
  }
}