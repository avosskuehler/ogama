// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ImportAOIAssignColumnsDialog.cs" company="Freie Universität Berlin">
//   OGAMA - open gaze and mouse analyzer 
//   Copyright (C) 2014 Dr. Adrian Voßkühler  
//   Licensed under GPL V3
// </copyright>
// <author>Adrian Voßkühler</author>
// <email>adrian@ogama.net</email>
// <summary>
//   Derived from <see cref="FormWithAccellerators" />.
//   This is the third part of the AOI import assistant.
//   Here the custom assigning of import columns with ogama columns is specified.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Ogama.Modules.ImportExport.AOIData
{
  using System;
  using System.Windows.Forms;

  using Ogama.Modules.Common.FormTemplates;
  using Ogama.Modules.ImportExport.Common;

  /// <summary>
  ///   Derived from <see cref="FormWithAccellerators" />.
  ///   This is the third part of the AOI import assistant.
  ///   Here the custom assigning of import columns with ogama columns is specified.
  /// </summary>
  public partial class ImportAOIAssignColumnsDialog : FormWithAccellerators
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the ImportAOIAssignColumnsDialog class.
    /// </summary>
    public ImportAOIAssignColumnsDialog()
    {
      this.InitializeComponent();
    }

    #endregion

    #region Methods

    /// <summary>
    ///   Fills left column of columns assignment datagridview
    ///   with entrys of Ogama aoi table database columns,
    ///   or simple rectangle import columns.
    /// </summary>
    private void FillLeftAssignColumn()
    {
      DataGridViewRowCollection rows = this.dGVAssignments.Rows;
      rows.Clear();
      switch (ImportAOI.AoiSettings.ImportMode)
      {
        case AOIImportModes.UseSimpleRectangles:
          rows.Add(new[] { "TrialID", string.Empty });
          rows.Add(new[] { "NameOfShape", string.Empty });
          rows.Add(new[] { "Left top corner X", string.Empty });
          rows.Add(new[] { "Left top corner Y", string.Empty });
          rows.Add(new[] { "Right bottom corner X", string.Empty });
          rows.Add(new[] { "Right bottom corner Y", string.Empty });
          rows.Add(new[] { "ShapeGroup", string.Empty });
          break;
        case AOIImportModes.UseOgamaColumns:
          rows.Add(new[] { "TrialID", string.Empty });
          rows.Add(new[] { "SlideNr", string.Empty });
          rows.Add(new[] { "ShapeName", string.Empty });
          rows.Add(new[] { "ShapeType", string.Empty });
          rows.Add(new[] { "ShapeNumPts", string.Empty });
          rows.Add(new[] { "ShapePts", string.Empty });
          rows.Add(new[] { "ShapeGroup", string.Empty });
          break;
      }
    }

    /// <summary>
    ///   Fills right column of columns assignment datagridview
    ///   with entrys of import table columns.
    /// </summary>
    private void FillRightAssignColumn()
    {
      // Fill drop down column in assign data grid view with column entries from text file
      this.ColumnTextFileColumns.Items.Clear();
      this.ColumnTextFileColumns.Items.Add(string.Empty);
      foreach (string header in ImportAOI.FileImport.ColumnHeaders)
      {
        this.ColumnTextFileColumns.Items.Add(header);
      }

      // Prefill column assignments, if parts of the import column 
      // headers are contained in ogamas column headers.
      for (int i = 0; i < this.dGVAssignments.Rows.Count; i++)
      {
        string left = this.dGVAssignments[0, i].Value.ToString();

        foreach (string item in this.ColumnTextFileColumns.Items)
        {
          if (left.Contains(item) && item != string.Empty)
          {
            this.dGVAssignments[1, i].Value = item;
          }
        }
      }
    }

    /// <summary>
    /// This method checks for a assigned column value for the
    ///   given colum and shows a error message if this
    ///   column is not assigned.
    /// </summary>
    /// <param name="columnName">
    /// A <see cref="string"/> with the ogama
    ///   column name that has to be assigned.
    /// </param>
    /// <returns>
    /// <strong>True</strong> if column is assigned, otherwise
    ///   <strong>false</strong>.
    /// </returns>
    private bool IsColumnAssigned(string columnName)
    {
      if (ImportAOI.FileImport.ColumnAssignments[columnName] == null
          || ImportAOI.FileImport.ColumnAssignments[columnName] == string.Empty)
      {
        MessageBox.Show(
          "You have to define a " + columnName + " column ...", 
          Application.ProductName, 
          MessageBoxButtons.OK, 
          MessageBoxIcon.Information);
        return false;
      }

      return true;
    }

    /// <summary>
    ///   This method saves the current column assignments to
    ///   the <see cref="ASCIISettings.ColumnAssignments" />
    ///   of the static <see cref="ImportAOI" />.
    /// </summary>
    private void SaveColumnAssignments()
    {
      ImportAOI.FileImport.ColumnAssignments.Clear();
      foreach (DataGridViewRow row in this.dGVAssignments.Rows)
      {
        string key = row.Cells[0].Value.ToString();
        string value = row.Cells[1].Value == null ? string.Empty : row.Cells[1].Value.ToString();
        ImportAOI.FileImport.ColumnAssignments.Add(key, value);
      }
    }

    /// <summary>
    ///   This method sets the current <see cref="AOIImportModes" />
    ///   in the settings, referring
    ///   to current radio button selection.
    /// </summary>
    private void SetImportMode()
    {
      if (this.rdbOgamaFormat.Checked)
      {
        ImportAOI.AoiSettings.ImportMode = AOIImportModes.UseOgamaColumns;
      }
      else if (this.rdbSimpleRects.Checked)
      {
        ImportAOI.AoiSettings.ImportMode = AOIImportModes.UseSimpleRectangles;
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
        var key = (string)this.dGVAssignments.Rows[i].Cells[0].Value;

        if (ImportAOI.FileImport.ColumnAssignments.ContainsKey(key))
        {
          this.dGVAssignments.Rows[i].Cells[1].Value = ImportAOI.FileImport.ColumnAssignments[key];
        }
      }
    }

    /// <summary>
    ///   This method checks for assigning of all minimum relevant columns,
    ///   and returns false if some are missing.
    ///   That depends on the import mode.
    /// </summary>
    /// <returns>
    ///   <strong>True</strong> if successful, otherwise
    ///   <strong>false</strong>.
    /// </returns>
    private bool ValidateAssignments()
    {
      // Check for missing columns.
      switch (ImportAOI.AoiSettings.ImportMode)
      {
        case AOIImportModes.UseSimpleRectangles:
          if (!this.IsColumnAssigned("TrialID"))
          {
            return false;
          }

          if (!this.IsColumnAssigned("Left top corner X"))
          {
            return false;
          }

          if (!this.IsColumnAssigned("Left top corner Y"))
          {
            return false;
          }

          if (!this.IsColumnAssigned("Right bottom corner X"))
          {
            return false;
          }

          if (!this.IsColumnAssigned("Right bottom corner Y"))
          {
            return false;
          }

          break;
        case AOIImportModes.UseOgamaColumns:
          if (!this.IsColumnAssigned("TrialID"))
          {
            return false;
          }

          if (!this.IsColumnAssigned("ShapeType"))
          {
            return false;
          }

          if (!this.IsColumnAssigned("ShapeNumPts"))
          {
            return false;
          }

          if (!this.IsColumnAssigned("ShapePts"))
          {
            return false;
          }

          break;
      }

      return true;
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for
    ///   the <see cref="Button"/> <see cref="btnImport"/>.
    ///   Saves the assignments in the static settings and if
    ///   validating is successful returns a <see cref="DialogResult.OK"/>.
    ///   otherwise the <see cref="DialogResult.Retry"/> flag is set,
    ///   which will cancel the closing of the form in the
    ///   <see cref="Form.FormClosing"/> event handler.
    /// </summary>
    /// <param name="sender">
    /// Source of the event.
    /// </param>
    /// <param name="e">
    /// An empty <see cref="EventArgs"/>
    /// </param>
    private void BtnImportClick(object sender, EventArgs e)
    {
      this.SaveColumnAssignments();
      if (!this.ValidateAssignments())
      {
        this.DialogResult = DialogResult.Retry;
      }
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
    private void FrmImportAOIAssignColumnsFormClosing(object sender, FormClosingEventArgs e)
    {
      if (this.DialogResult == DialogResult.Retry)
      {
        e.Cancel = true;
      }
    }

    /// <summary>
    /// The <see cref="Form.Load"/> event handler.
    ///   Initializes the UI with the current settings.
    /// </summary>
    /// <param name="sender">
    /// Source of the event.
    /// </param>
    /// <param name="e">
    /// An empty <see cref="EventArgs"/>.
    /// </param>
    private void FrmImportAOIAssignColumnsLoad(object sender, EventArgs e)
    {
      this.UpdateUIWithSettings();
    }

    /// <summary>
    /// The <see cref="RadioButton.CheckedChanged"/> event handler for
    ///   the <see cref="RadioButton"/> <see cref="rdbOgamaFormat"/>
    ///   and <see cref="rdbSimpleRects"/>.
    ///   User switched import mode, so set import mode and update assignment columns.
    /// </summary>
    /// <param name="sender">
    /// Source of the event.
    /// </param>
    /// <param name="e">
    /// An empty <see cref="EventArgs"/>
    /// </param>
    private void RdbImportFormatCheckedChanged(object sender, EventArgs e)
    {
      this.SetImportMode();
      this.UpdateUIWithSettings();
    }

    #endregion
  }
}