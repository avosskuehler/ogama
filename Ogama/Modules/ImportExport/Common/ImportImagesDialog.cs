// <copyright file="ImportImagesDialog.cs" company="FU Berlin">
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

namespace Ogama.Modules.ImportExport.Common
{
  using System;
  using System.Collections.Generic;
  using System.IO;
  using System.Windows.Forms;

  using Ogama.ExceptionHandling;
  using Ogama.Modules.Common;
  using Ogama.Modules.Common.FormTemplates;
  using Ogama.Modules.Common.Tools;
  using Ogama.Modules.ImportExport.FixationData;
  using Ogama.Modules.ImportExport.RawData;

  /// <summary>
  /// This dialog <see cref="Form"/> derives from <see cref="FormWithAccellerators"/>
  /// This is the fifth part of the import assistants.
  /// Here the custom definition or generation of trials is specified.
  /// </summary>
  /// <remarks>The following modes are known: <see cref="TrialSequenceImportModes"/>.</remarks>
  /// <seealso cref="TrialSequenceImportModes"/>
  public partial class ImportImagesDialog : FormWithAccellerators
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
    /// Saves the import trial file rows.
    /// </summary>
    private List<string[]> imagesImportRows;

    /// <summary>
    /// Saves the import trial file headers.
    /// </summary>
    private List<string> imagefileColumnHeaders;

    /// <summary>
    /// Saves the number of preview data lines.
    /// </summary>
    private int numberOfImportLines;

    /// <summary>
    /// Saves a flag, whether this form is currently initialzing.
    /// </summary>
    /// <remarks>Used to disable update during <see cref="Form.Load"/></remarks>
    private bool isInitializing;

    /// <summary>
    /// Saves the <see cref="ASCIISettings"/> with the current import settings.
    /// </summary>
    private ASCIISettings asciiSetting;

    /// <summary>
    /// Saves the <see cref="DetectionSettings"/> with the current import settings.
    /// </summary>
    private DetectionSettings detectionSetting;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////

    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the ImportImagesDialog class.
    /// </summary>
    /// <param name="newAsciiSettings">Ref. The <see cref="ASCIISettings"/> to use.</param>
    /// <param name="newDetectionSettings">Ref. The <see cref="DetectionSettings"/> to use.</param>
    public ImportImagesDialog(ref ASCIISettings newAsciiSettings, ref DetectionSettings newDetectionSettings)
    {
      this.InitializeComponent();
      this.numberOfImportLines = 20000;
      this.detectionSetting = newDetectionSettings;
      this.asciiSetting = newAsciiSettings;
      this.imagesImportRows = new List<string[]>();
      this.imagefileColumnHeaders = new List<string>();
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
    private void frmImportImages_Load(object sender, EventArgs e)
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
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void frmImportImages_FormClosing(object sender, FormClosingEventArgs e)
    {
      if (this.DialogResult == DialogResult.Retry)
      {
        e.Cancel = true;
      }
    }

    /// <summary>
    /// The <see cref="RadioButton.CheckedChanged"/> event handler for
    /// the import mode <see cref="RadioButton"/>s.
    /// User selected new import mode radio button,
    /// so update settings and preview.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void rdbImportMode_CheckedChanged(object sender, EventArgs e)
    {
      this.SetImportMode();
    }

    /// <summary>
    /// The <see cref="Control.TextChanged"/> event handler for
    /// the <see cref="TextBox"/> <see cref="txbStimulusFileEndings"/>.
    /// User modified image file endings textbox,
    /// so update settings and preview.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void txbStimulusFileEndings_TextChanged(object sender, EventArgs e)
    {
      this.detectionSetting.StimuliFileExtension = this.txbStimulusFileEndings.Text;
      this.UpdatePreview();
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for
    /// the <see cref="Button"/> <see cref="btnImport"/>.
    /// Saves the imported images and validates the filenames. If
    /// validating is succesful returns a <see cref="DialogResult.OK"/>.
    /// otherwise the <see cref="DialogResult.Retry"/> flag is set,
    /// which will cancel the closing of the form in the
    /// <see cref="Form.FormClosing"/> event handler.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnImport_Click(object sender, EventArgs e)
    {
      if (!this.SaveImageImportDataGridView() || !this.CheckforValidFilenames())
      {
        this.DialogResult = DialogResult.Retry;
      }
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for
    /// the <see cref="Button"/> <see cref="btnOpenAssignFile"/>.
    /// The user selected to open a assignment file for
    /// the trialID - image assignment.
    /// Shows open file dialog and updates the images
    /// data grid view.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnOpenAssignFile_Click(object sender, EventArgs e)
    {
      if (this.ofdAssignmentFile.ShowDialog() == DialogResult.OK)
      {
        this.detectionSetting.StimuliImportFile = this.ofdAssignmentFile.FileName;
        this.ReadImagesTable(this.ofdAssignmentFile.FileName);
      }
    }

    /// <summary>
    /// The <see cref="NumericUpDown.ValueChanged"/> event handler for
    /// the <see cref="NumericUpDown"/> <see cref="nudImportLines"/>.
    /// User modified "number of import preview" lines, so update preview.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void nudImportLines_ValueChanged(object sender, EventArgs e)
    {
      this.numberOfImportLines = (int)this.nudImportLines.Value;
      this.UpdatePreview();
    }

    /// <summary>
    /// The <see cref="ComboBox.SelectionChangeCommitted"/> event handler for
    /// the <see cref="ComboBox"/> <see cref="cbbStimulusFileColumn"/>.
    /// User changed import column for starting time , so update preview.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void cbbStimulusFileColumn_SelectionChangeCommitted(object sender, EventArgs e)
    {
      this.RepopulateImageAssignmentTable();
    }

    /// <summary>
    /// The <see cref="ComboBox.SelectionChangeCommitted"/> event handler for
    /// the <see cref="ComboBox"/> <see cref="cbbTrialIDColumn"/>.
    /// User changed import column for trial ID , so update preview.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void cbbTrialIDColumn_SelectionChangeCommitted(object sender, EventArgs e)
    {
      this.RepopulateImageAssignmentTable();
    }

    /// <summary>
    /// The <see cref="DataGridView.CellLeave"/> event handler for
    /// the <see cref="DataGridView"/> <see cref="dgvAssignments"/>.
    /// Updates the preview if a cell has been touched.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A <see cref="DataGridViewCellEventArgs"/> with the event data.</param>
    private void dgvAssignments_CellLeave(object sender, DataGridViewCellEventArgs e)
    {
      this.UpdatePreview();
    }

    /// <summary>
    /// The <see cref="DataGridView.CellValidating"/> event handler for
    /// the <see cref="DataGridView"/> <see cref="dgvAssignments"/>.
    /// Checks for numbers in TrialID column.
    /// Shows an error icon with message at the cell.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A <see cref="DataGridViewCellValidatingEventArgs"/>
    /// with the event data.</param>
    private void dgvAssignments_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
    {
      if (e.ColumnIndex == 0)
      {
        try
        {
          if (e.FormattedValue.ToString() != string.Empty)
          {
            Convert.ToInt64(e.FormattedValue);
            this.dgvAssignments[e.ColumnIndex, e.RowIndex].ErrorText = string.Empty;
          }
        }
        catch (Exception ex)
        {
          string message = "This is not a valid trial sequence number, it has to be a unique number."
                           + Environment.NewLine + "Error: " + ex.Message;
          this.dgvAssignments[e.ColumnIndex, e.RowIndex].ErrorText = message;
        }
      }
    }

    /// <summary>
    /// The <see cref="DataGridView.EditingControlShowing"/> event handler for
    /// the <see cref="DataGridView"/> <see cref="dgvAssignments"/>.
    /// User started to edit assignment table, so switch from
    /// "import table" to "enter table" mode.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A <see cref="DataGridViewEditingControlShowingEventArgs"/>
    /// with the event data.</param>
    private void dgvAssignments_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
    {
      this.rdbEnterTableImages.Checked = true;
      this.txbImportAssignFile.Text = string.Empty;
      this.detectionSetting.StimuliImportFile = string.Empty;
      this.cbbStimulusFileColumn.Items.Clear();
      this.cbbTrialIDColumn.Items.Clear();
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
    /// This method parses the given file to find trialid to image assignments.
    /// If more than two columns are in the file the user is called to
    /// define the valid columns.
    /// </summary>
    /// <remarks>This method uses the same <see cref="ASCIISettings"/>
    /// as the import for the raw data.</remarks>
    /// <param name="imagesImportFile">A <see cref="string"/> with the import file.</param>
    private void ReadImagesTable(string imagesImportFile)
    {
      this.txbImportAssignFile.Text = Path.GetFileName(this.detectionSetting.StimuliImportFile);

      this.imagefileColumnHeaders.Clear();
      this.imagesImportRows.Clear();
      this.imagesImportRows = this.asciiSetting.ParseFile(
        imagesImportFile, this.numberOfImportLines, ref this.imagefileColumnHeaders);

      this.cbbStimulusFileColumn.Items.Clear();
      this.cbbTrialIDColumn.Items.Clear();

      if (this.imagefileColumnHeaders.Count == 0)
      {
        string message = "There are no columns in import file, so we stop importing. " + Environment.NewLine
                         + "This import uses the same settings as the raw file import (quotation characters etc.)";
        ExceptionMethods.ProcessErrorMessage(message);
        return;
      }
      else if (this.imagefileColumnHeaders.Count == 1)
      {
        this.cbbStimulusFileColumn.Items.AddRange(this.imagefileColumnHeaders.ToArray());
        this.cbbTrialIDColumn.Items.Add("Automatic numbering");
        this.cbbStimulusFileColumn.SelectedIndex = 0;
        this.cbbTrialIDColumn.SelectedIndex = 0;
        this.RepopulateImageAssignmentTable();
      }
      else
      {
        this.cbbStimulusFileColumn.Items.AddRange(this.imagefileColumnHeaders.ToArray());
        this.cbbTrialIDColumn.Items.Add("Automatic numbering");
        this.cbbTrialIDColumn.Items.AddRange(this.imagefileColumnHeaders.ToArray());
        this.cbbStimulusFileColumn.SelectedIndex = 1;
        this.cbbTrialIDColumn.SelectedIndex = 1;
        this.RepopulateImageAssignmentTable();

        if (this.imagefileColumnHeaders.Count > 2)
        {
          this.spcTableDropDowns.Panel2Collapsed = false;
          string message = "There are more than two columns in import file, "
                           + "please specify the starting time column and the trial ID column.";
          ExceptionMethods.ProcessMessage("Please choose columns", message);
        }
      }
    }

    /// <summary>
    /// This method update the import settings
    /// with the new settings, if an assignment 
    /// table (trialID-imagefile) is specified, 
    /// </summary>
    /// <returns><strong>True</strong>, if column assignment is valid,
    /// otherwise <strong>false</strong>.</returns>
    private bool SaveImageImportDataGridView()
    {
      switch (this.detectionSetting.StimuliImportMode)
      {
        case StimuliImportModes.UseImportColumn:
          break;
        case StimuliImportModes.UseiViewXMSG:
          break;
        case StimuliImportModes.SearchForImageEnding:
          break;
        case StimuliImportModes.UseAssignmentTable:
          this.detectionSetting.TrialIDToImageAssignments.Clear();
          foreach (DataGridViewRow row in this.dgvAssignments.Rows)
          {
            if (row.IsNewRow)
            {
              break;
            }

            string stimulusFile = string.Empty;
            int trialID = 0;
            if (row.Cells["columnAssignStimulusFile"].Value != null)
            {
              stimulusFile = row.Cells["columnAssignStimulusFile"].Value.ToString();
            }
            else
            {
              return false;
            }

            if (row.Cells["columnAssignTrialID"].Value == null
                || !int.TryParse(row.Cells["columnAssignTrialID"].Value.ToString(), out trialID))
            {
              return false;
            }

            if (!this.detectionSetting.TrialIDToImageAssignments.ContainsKey(trialID))
            {
              this.detectionSetting.TrialIDToImageAssignments.Add(trialID, stimulusFile);
            }
          }

          break;
      }

      return true;
    }

    /// <summary>
    /// User changed import settings, so update image import data grid view.
    /// </summary>
    private void UpdatePreview()
    {
      // Do not call this for every UI component during Form.Load event.
      if (!this.isInitializing)
      {
        this.Cursor = Cursors.WaitCursor;

        // Clear preview data grid view
        this.dgvTrialsPreview.Rows.Clear();

        // Save trialID-Image file assignments if applicable
        if (!this.SaveImageImportDataGridView())
        {
          this.Cursor = Cursors.Default;
          return;
        }

        // Update preview
        switch (this.detectionSetting.ImportType)
        {
          case ImportTypes.Rawdata:
            foreach (TrialsData trial in ImportRawData.GetTrialList(this.numberOfImportLines))
            {
              this.dgvTrialsPreview.Rows.Add(
                new object[]
                  {
                    trial.SubjectName, trial.TrialSequence, trial.TrialID, trial.Category, trial.TrialName,
                    trial.TrialStartTime, trial.Duration
                  });
            }

            break;
          case ImportTypes.Fixations:
            foreach (TrialsData trial in ImportFixations.GetTrialList(this.numberOfImportLines))
            {
              this.dgvTrialsPreview.Rows.Add(
                new object[] { trial.SubjectName, trial.TrialSequence, trial.TrialID, string.Empty, trial.TrialName });
            }

            break;
        }

        this.Cursor = Cursors.Default;
      }
    }

    /// <summary>
    /// This method fills the assignment data grid view
    /// with the chosen columns of the image import file.
    /// </summary>
    private void RepopulateImageAssignmentTable()
    {
      // If combo boxes didn´t have a selection
      // Cancel the repopulation
      if (this.cbbStimulusFileColumn.SelectedItem == null || this.cbbTrialIDColumn.SelectedItem == null)
      {
        return;
      }

      // Erase table
      this.dgvAssignments.Rows.Clear();

      // Get index of selected import file columns
      int columnIndexStimulusFile = 0;
      int columnIndexTrialID = 0;
      for (int i = 0; i < this.imagefileColumnHeaders.Count; i++)
      {
        string header = this.imagefileColumnHeaders[i];
        if (header == this.cbbStimulusFileColumn.SelectedItem.ToString())
        {
          columnIndexStimulusFile = i;
        }

        if (header == this.cbbTrialIDColumn.SelectedItem.ToString())
        {
          columnIndexTrialID = i;
        }
      }

      // Populate with new entrys.
      if (this.cbbTrialIDColumn.SelectedItem.ToString() == "Automatic numbering")
      {
        for (int i = 0; i < this.imagesImportRows.Count; i++)
        {
          string[] item = this.imagesImportRows[i];
          this.dgvAssignments.Rows.Add(new string[] { i.ToString(), item[columnIndexStimulusFile] });
        }
      }
      else
      {
        foreach (string[] items in this.imagesImportRows)
        {
          this.dgvAssignments.Rows.Add(new string[] { items[columnIndexTrialID], items[columnIndexStimulusFile] });
        }
      }

      this.UpdatePreview();
    }

    /// <summary>
    /// This method updates all fields of the UI with the
    /// current settings.
    /// </summary>
    private void UpdateUIWithSettings()
    {
      this.isInitializing = true;

      this.spcTableDropDowns.Panel2Collapsed = true;

      // Hide columns, that are not needed.
      switch (this.detectionSetting.ImportType)
      {
        case ImportTypes.Rawdata:
          // Hide columns that are not assigned
          if (this.asciiSetting.ColumnAssignments["TrialCategory"] == string.Empty)
          {
            this.columnCategory.Visible = false;
          }

          break;
        case ImportTypes.Fixations:
          this.columnCategory.Visible = false;
          this.columnDuration.Visible = false;
          this.columnTrialStartTime.Visible = false;
          break;
      }

      switch (this.detectionSetting.StimuliImportMode)
      {
        case StimuliImportModes.UseiViewXMSG:
          this.rdbiViewX.Checked = true;
          this.SwitchVisibility(false, false);
          break;
        case StimuliImportModes.UseAssignmentTable:
          if (this.detectionSetting.StimuliImportFile != string.Empty)
          {
            this.rdbImportTableImages.Checked = true;
            this.txbImportAssignFile.Text = Path.GetFileName(this.detectionSetting.StimuliImportFile);

            this.imagefileColumnHeaders.Clear();
            this.imagesImportRows.Clear();
            this.imagesImportRows = this.asciiSetting.ParseFile(
              this.detectionSetting.StimuliImportFile, this.numberOfImportLines, ref this.imagefileColumnHeaders);

            this.cbbStimulusFileColumn.Items.Clear();
            this.cbbTrialIDColumn.Items.Clear();

            this.cbbStimulusFileColumn.Items.AddRange(this.imagefileColumnHeaders.ToArray());
            this.cbbTrialIDColumn.Items.Add("Automatic numbering");
            this.cbbTrialIDColumn.Items.AddRange(this.imagefileColumnHeaders.ToArray());
            this.SwitchVisibility(true, true);
          }
          else
          {
            this.rdbEnterTableImages.Checked = true;
            this.SwitchVisibility(true, false);
          }

          foreach (KeyValuePair<int, string> pair in this.detectionSetting.TrialIDToImageAssignments)
          {
            this.dgvAssignments.Rows.Add(new object[] { pair.Key, pair.Value });
          }

          break;
        case StimuliImportModes.UseImportColumn:
          this.rdbUseStimulusFileColumn.Checked = true;
          this.rdbiViewX.Enabled = false;
          this.rdbSearchForImageEndings.Enabled = false;
          this.rdbImportTableImages.Enabled = false;
          this.rdbEnterTableImages.Enabled = false;
          this.txbStimulusFileEndings.Enabled = false;
          this.txbStimulusFileColumn.Text = this.asciiSetting.ColumnAssignments["TrialImage"];
          this.SwitchVisibility(false, false);
          break;
        case StimuliImportModes.SearchForImageEnding:
          this.rdbSearchForImageEndings.Checked = true;
          this.SwitchVisibility(false, false);
          break;
      }

      this.txbStimulusFileColumn.Text = this.asciiSetting.ColumnAssignments["TrialImage"];
      this.txbStimulusFileEndings.Text = this.detectionSetting.StimuliFileExtension;

      this.isInitializing = false;
      this.UpdatePreview();
    }

    #endregion //METHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////

    #region HELPER

    /// <summary>
    /// Changes visibility of the image import table and file controls.
    /// </summary>
    /// <param name="isTableVisible"><strong>True</strong>, 
    /// if assignment data grid view should be visible.</param>
    /// <param name="isFileVisible"><strong>True</strong>, 
    /// if filename text box and open button should be visible.</param>
    private void SwitchVisibility(bool isTableVisible, bool isFileVisible)
    {
      this.dgvAssignments.Visible = isTableVisible;
      this.txbImportAssignFile.Visible = isFileVisible;
      this.btnOpenAssignFile.Visible = isFileVisible;
      this.spcTableDropDowns.Panel2Collapsed = !this.rdbImportTableImages.Checked;
    }

    /// <summary>
    /// This method updates the <see cref="StimuliImportModes"/>.
    /// </summary>
    private void SetImportMode()
    {
      if (!this.detectionSetting.SavedSettings)
      {
        this.detectionSetting.TrialIDToImageAssignments.Clear();
      }

      this.dgvAssignments.Rows.Clear();
      if (this.rdbUseStimulusFileColumn.Checked)
      {
        this.detectionSetting.StimuliImportMode = StimuliImportModes.UseImportColumn;
        this.SwitchVisibility(false, false);
      }
      else if (this.rdbSearchForImageEndings.Checked)
      {
        this.detectionSetting.StimuliImportMode = StimuliImportModes.SearchForImageEnding;
        this.SwitchVisibility(false, false);
      }
      else if (this.rdbiViewX.Checked)
      {
        this.detectionSetting.StimuliImportMode = StimuliImportModes.UseiViewXMSG;
        this.SwitchVisibility(false, false);
      }
      else if (this.rdbEnterTableImages.Checked)
      {
        this.detectionSetting.StimuliImportMode = StimuliImportModes.UseAssignmentTable;
        this.SwitchVisibility(true, false);
      }
      else if (this.rdbImportTableImages.Checked)
      {
        this.detectionSetting.StimuliImportMode = StimuliImportModes.UseAssignmentTable;
        this.SwitchVisibility(true, true);
      }

      this.UpdatePreview();
    }

    /// <summary>
    /// This method parses the stimulus file names in the import for
    /// forbidden characters like white space, and shows a notification.
    /// </summary>
    /// <returns><strong>True</strong>, if filenames are valid,
    /// otherwise <strong>false</strong>.</returns>
    private bool CheckforValidFilenames()
    {
      int fileErrorCounter = 0;
      foreach (string filename in this.detectionSetting.TrialIDToImageAssignments.Values)
      {
        if (!IOHelpers.IsValidFilename(filename))
        {
          fileErrorCounter++;
        }
      }

      if (fileErrorCounter > 0)
      {
        string message = "It seems that at least " + fileErrorCounter.ToString() + " stimulus filenames are not valid, "
                         + Environment.NewLine + " because they contain white-space "
                         + " or other unallowed characters." + Environment.NewLine
                         + "Would you like to revise the names ?";
        if (InformationDialog.Show("Invalid filename", message, true, MessageBoxIcon.Information) == DialogResult.Yes)
        {
          return false;
        }
      }

      return true;
    }

    #endregion //HELPER
  }
}
