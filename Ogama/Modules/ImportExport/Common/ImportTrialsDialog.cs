// <copyright file="ImportTrialsDialog.cs" company="FU Berlin">
// ******************************************************
// OGAMA - open gaze and mouse analyzer 
// Copyright (C) 2015 Dr. Adrian Voßkühler  
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
  using Ogama.Modules.Common.FormTemplates;
  using Ogama.Modules.ImportExport.FixationData;
  using Ogama.Modules.ImportExport.RawData;

  /// <summary>
  /// This dialog <see cref="Form"/> derives from <see cref="FormWithAccellerators"/>
  /// and is the fourth part of the import process.
  /// It is intended to generate or read the trials from the logfile, 
  /// that are describing the different slides of the presentation.
  /// </summary>
  public partial class ImportTrialsDialog : FormWithAccellerators
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
    private List<string[]> trialImportRows;

    /// <summary>
    /// Saves the import trial file headers.
    /// </summary>
    private List<string> trialColumnHeaders;

    /// <summary>
    /// Number of preview data lines.
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
    /// Initializes a new instance of the ImportTrialsDialog class.
    /// </summary>
    /// <param name="newAsciiSettings">Ref. The <see cref="ASCIISettings"/> to use.</param>
    /// <param name="newDetectionSettings">Ref. The <see cref="DetectionSettings"/> to use.</param>
    public ImportTrialsDialog(ref ASCIISettings newAsciiSettings, ref DetectionSettings newDetectionSettings)
    {
      this.InitializeComponent();
      this.numberOfImportLines = 20000;
      this.detectionSetting = newDetectionSettings;
      this.asciiSetting = newAsciiSettings;
      this.trialImportRows = new List<string[]>();
      this.trialColumnHeaders = new List<string>();
      this.cbbTimeUnit.SelectedIndex = 1;
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
    /// The <see cref="Form.Load"/> event handler. Updates UI referring to import settings.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void frmImportTrials_Load(object sender, EventArgs e)
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
    private void frmImportTrials_FormClosing(object sender, FormClosingEventArgs e)
    {
      if (this.DialogResult == DialogResult.Retry)
      {
        e.Cancel = true;
      }
    }

    /// <summary>
    /// The <see cref="Control.TextChanged"/> event handler for
    /// the <see cref="TextBox"/> <see cref="txbTrialMSG"/>.
    /// Text that distinguishes trials in raw data file has changed,
    /// so update import settings.
    /// </summary>
    /// <remarks>The update of the preview is done in the
    /// <see cref="Control.Leave"/> event handler of this <see cref="TextBox"/>.</remarks>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void txbTrialMSG_TextChanged(object sender, EventArgs e)
    {
      this.detectionSetting.TrialTriggerString = this.txbTrialMSG.Text;
    }

    /// <summary>
    /// The <see cref="Control.Leave"/> event handler for
    /// the <see cref="TextBox"/> <see cref="txbTrialMSG"/>.
    /// User changed trial msg name, so update preview.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void txbTrialMSG_Leave(object sender, EventArgs e)
    {
      this.UpdatePreview();
    }

    /// <summary>
    /// The <see cref="RadioButton.CheckedChanged"/> event handler for
    /// the <see cref="RadioButton"/>s.
    /// User switched the import mode radio buttons,
    /// so update the <see cref="DetectionSettings.TrialImportMode"/>
    /// change the according UI layout and update the preview data grid view.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void rdbTrialMode_CheckedChanged(object sender, EventArgs e)
    {
      this.SetImportMode();
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for
    /// the <see cref="Button"/> <see cref="btnNext"/>.
    /// User clicked on next button, 
    /// so read trial table if applicable in import settings.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnNext_Click(object sender, EventArgs e)
    {
      if (!this.SaveTrialImportDataGridView())
      {
        this.DialogResult = DialogResult.Retry;
      }
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for
    /// the <see cref="Button"/> <see cref="btnOpenAssignFile"/>.
    /// User would like to select an assignment table.
    /// So parse it with current import settings to find matching 
    /// start time - trialID columns.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnOpenAssignFile_Click(object sender, EventArgs e)
    {
      if (this.ofdAssignmentFile.ShowDialog() == DialogResult.OK)
      {
        this.detectionSetting.TrialImportFile = this.ofdAssignmentFile.FileName;
        this.ReadTrialsTable(this.ofdAssignmentFile.FileName);
      }
    }

    /// <summary>
    /// The <see cref="NumericUpDown.ValueChanged"/> event handler for
    /// the <see cref="NumericUpDown"/> <see cref="nudImportLines"/>.
    /// User changed number of import lines numeric up down,
    /// so update preview.
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
    /// the <see cref="ComboBox"/> <see cref="cbbTimeUnit"/>.
    /// User changed time unit of import time column,
    /// so reset <see cref="DetectionSettings.TimeFactor"/> and update preview.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void cbbTimeUnit_SelectionChangeCommitted(object sender, EventArgs e)
    {
      int unit = this.cbbTimeUnit.SelectedIndex;
      double timeFactor = 0;
      switch (unit)
      {
        case 0:
          timeFactor = 1000D;
          break;
        case 1:
          timeFactor = 1D;
          break;
        case 2:
          timeFactor = 0.001D;
          break;
        default:
          timeFactor = 1D;
          break;
      }

      this.detectionSetting.TimeFactor = timeFactor;
      this.UpdatePreview();
    }

    /// <summary>
    /// The <see cref="ComboBox.SelectionChangeCommitted"/> event handler for
    /// the assignment column selection <see cref="ComboBox"/>es.
    /// User changed import column for starting time , so update preview.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void cbbAssignColumn_SelectionChangeCommitted(object sender, EventArgs e)
    {
      this.RepopulateTrialAssignmentTable();
    }

    /// <summary>
    /// The <see cref="DataGridView.CellLeave"/> event handler for
    /// the <see cref="DataGridView"/> <see cref="dgvAssignments"/>.
    /// Updates preview when trial table was updated.
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
        string message = "This is not a valid entry, it has to be a unique number." +
          Environment.NewLine + "Error: " + ex.Message;
        this.dgvAssignments[e.ColumnIndex, e.RowIndex].ErrorText = message;
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
      this.rdbEnterTableTrialID.Checked = true;
      this.txbImportAssignFile.Text = string.Empty;
      this.detectionSetting.TrialImportFile = string.Empty;
      this.cbbAssignStartingTimeColumn.Items.Clear();
      this.cbbAssignTrialIDColumn.Items.Clear();
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
    /// This method imports the given table with starting time and trialid
    /// into the assignment data grid view. If more than two columns are in
    /// the file an assignment section is shown.
    /// </summary>
    /// <param name="trialImportFile">A <see cref="string"/> with the trial import
    /// ascii file.</param>
    private void ReadTrialsTable(string trialImportFile)
    {
      this.txbImportAssignFile.Text = Path.GetFileName(trialImportFile);

      this.trialColumnHeaders.Clear();
      this.trialImportRows.Clear();
      this.trialImportRows = this.asciiSetting.ParseFile(
        trialImportFile,
        this.numberOfImportLines,
        ref this.trialColumnHeaders);

      this.cbbAssignStartingTimeColumn.Items.Clear();
      this.cbbAssignTrialSequenceColumn.Items.Clear();
      this.cbbAssignTrialIDColumn.Items.Clear();

      if (this.trialColumnHeaders.Count == 0)
      {
        string message = "There are no columns in import file, so we stop importing. "
        + Environment.NewLine +
        "This import uses the same settings as the raw file import (quotation characters etc.)";
        ExceptionMethods.ProcessErrorMessage(message);
        return;
      }
      else if (this.trialColumnHeaders.Count == 1)
      {
        this.cbbAssignStartingTimeColumn.Items.AddRange(this.trialColumnHeaders.ToArray());
        this.cbbAssignTrialIDColumn.Items.Add("Automatic numbering");
        this.cbbAssignTrialSequenceColumn.Items.Add("Automatic numbering");
        this.cbbAssignStartingTimeColumn.SelectedIndex = 0;
        this.cbbAssignTrialIDColumn.SelectedIndex = 0;
        this.cbbAssignTrialSequenceColumn.SelectedIndex = 0;
        this.RepopulateTrialAssignmentTable();
      }
      else
      {
        this.cbbAssignStartingTimeColumn.Items.AddRange(this.trialColumnHeaders.ToArray());
        this.cbbAssignTrialIDColumn.Items.Add("Automatic numbering");
        this.cbbAssignTrialIDColumn.Items.AddRange(this.trialColumnHeaders.ToArray());
        this.cbbAssignTrialSequenceColumn.Items.Add("Automatic numbering");
        this.cbbAssignTrialSequenceColumn.Items.AddRange(this.trialColumnHeaders.ToArray());
        this.cbbAssignStartingTimeColumn.SelectedIndex = 0;
        this.cbbAssignTrialIDColumn.SelectedIndex = 2;
        this.cbbAssignTrialSequenceColumn.SelectedIndex = 2;
        this.RepopulateTrialAssignmentTable();

        if (this.trialColumnHeaders.Count > 2)
        {
          this.spcTableDropDowns.Panel2Collapsed = false;
          string message = "There are more than two columns in import file, " +
            "please specify the starting time column and the trial ID column.";
          ExceptionMethods.ProcessMessage("Please choose columns", message);
        }
      }
    }

    /// <summary>
    /// If an assignment table (start time - trialID) is specified, update the import settings
    /// with the new settings.
    /// </summary>
    /// <returns><strong>True</strong>, if column assignment is valid,
    /// otherwise <strong>false</strong>.</returns>
    private bool SaveTrialImportDataGridView()
    {
      switch (this.detectionSetting.TrialImportMode)
      {
        case TrialSequenceImportModes.UseImportColumn:
          break;
        case TrialSequenceImportModes.UseMSGLines:
          break;
        case TrialSequenceImportModes.UseAssignmentTable:
          this.detectionSetting.TrialSequenceToStarttimeAssignments.Clear();
          this.detectionSetting.TrialSequenceToTrialIDAssignments.Clear();
          foreach (DataGridViewRow row in this.dgvAssignments.Rows)
          {
            if (row.IsNewRow)
            {
              break;
            }

            int trialSequence = 0;
            int trialID = 0;
            long startTime = 0;

            if (row.Cells["columnAssignTrialSequence"].Value == null ||
              !int.TryParse(row.Cells["columnAssignTrialSequence"].Value.ToString(), out trialSequence))
            {
              return false;
            }

            if (row.Cells["columnAssignTrialID"].Value == null ||
              !int.TryParse(row.Cells["columnAssignTrialID"].Value.ToString(), out trialID))
            {
              return false;
            }

            if (row.Cells["columnAssignStartTime"].Value == null ||
              !long.TryParse(row.Cells["columnAssignStartTime"].Value.ToString(), out startTime))
            {
              return false;
            }

            if (!this.detectionSetting.TrialSequenceToStarttimeAssignments.ContainsKey(trialSequence))
            {
              long timeInMs = Convert.ToInt64(startTime * this.detectionSetting.TimeFactor);
              this.detectionSetting.TrialSequenceToStarttimeAssignments.Add(trialSequence, timeInMs);
            }

            if (!this.detectionSetting.TrialSequenceToTrialIDAssignments.ContainsKey(trialSequence))
            {
              this.detectionSetting.TrialSequenceToTrialIDAssignments.Add(trialSequence, trialID);
            }
          }

          break;
      }

      return true;
    }

    /// <summary>
    /// User changed import settings, so update trial import data grid view.
    /// </summary>
    private void UpdatePreview()
    {
      if (!this.isInitializing)
      {
        this.Cursor = Cursors.WaitCursor;

        // Clear preview data grid view
        this.dgvTrialsPreview.Rows.Clear();

        // Save trial start time to trial id assignments if applicable
        if (!this.SaveTrialImportDataGridView())
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
              this.dgvTrialsPreview.Rows.Add(new object[] 
                {
                  trial.SubjectName,
                  trial.TrialSequence,
                  trial.TrialID,
                  trial.Category,
                  trial.TrialStartTime,
                  trial.Duration
                });
            }

            break;
          case ImportTypes.Fixations:
            var trials = ImportFixations.GetTrialList(this.numberOfImportLines);
            if (trials == null)
            {
              this.DialogResult = DialogResult.Abort;
              this.Close();
              break;
            }

            foreach (TrialsData trial in trials)
            {
              this.dgvTrialsPreview.Rows.Add(new object[] 
                {
                  trial.SubjectName,
                  trial.TrialSequence,
                  trial.TrialID
                });
            }

            break;
        }

        this.Cursor = Cursors.Default;
      }
    }

    /// <summary>
    /// This method fills the assignment data grid view
    /// with the chosen columns of the trial import file.
    /// </summary>
    private void RepopulateTrialAssignmentTable()
    {
      // If combo boxes didn´t have a selection
      // Cancel the repopulation
      if (this.cbbAssignStartingTimeColumn.SelectedItem == null ||
        this.cbbAssignTrialIDColumn.SelectedItem == null ||
        this.cbbAssignTrialSequenceColumn.SelectedItem == null)
      {
        return;
      }

      // Erase existing table entries.
      this.dgvAssignments.Rows.Clear();

      // Get index of selected import file columns
      int columnIndexStartingTime = 0;
      int columnIndexTrialID = 0;
      int columnIndexTrialSequence = 0;
      for (int i = 0; i < this.trialColumnHeaders.Count; i++)
      {
        string header = this.trialColumnHeaders[i];
        if (header == this.cbbAssignStartingTimeColumn.SelectedItem.ToString())
        {
          columnIndexStartingTime = i;
        }

        if (header == this.cbbAssignTrialIDColumn.SelectedItem.ToString())
        {
          columnIndexTrialID = i;
        }

        if (header == this.cbbAssignTrialSequenceColumn.SelectedItem.ToString())
        {
          columnIndexTrialSequence = i;
        }
      }

      // Populate with new entrys.
      if (this.cbbAssignTrialSequenceColumn.SelectedItem.ToString() == "Automatic numbering")
      {
        if (this.cbbAssignTrialIDColumn.SelectedItem.ToString() == "Automatic numbering")
        {
          for (int i = 0; i < this.trialImportRows.Count; i++)
          {
            string[] item = this.trialImportRows[i];
            this.dgvAssignments.Rows.Add(new string[] 
              { 
                i.ToString(), 
                i.ToString(), 
                item[columnIndexStartingTime] 
              });
          }
        }
        else
        {
          for (int i = 0; i < this.trialImportRows.Count; i++)
          {
            string[] item = this.trialImportRows[i];
            this.dgvAssignments.Rows.Add(new string[] 
              { 
                i.ToString(), 
                item[columnIndexTrialID], 
                item[columnIndexStartingTime]
              });
          }
        }
      }
      else
      {
        if (this.cbbAssignTrialIDColumn.SelectedItem.ToString() == "Automatic numbering")
        {
          foreach (string[] items in this.trialImportRows)
          {
            this.dgvAssignments.Rows.Add(new string[] 
              { 
                items[columnIndexTrialSequence], 
                items[columnIndexTrialSequence],
                items[columnIndexStartingTime] 
              });
          }
        }
        else
        {
          foreach (string[] items in this.trialImportRows)
          {
            this.dgvAssignments.Rows.Add(new string[] 
              { 
                items[columnIndexTrialSequence],
                items[columnIndexTrialID], 
                items[columnIndexStartingTime] 
              });
          }
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
      this.rdbUseTrialColumn.Enabled = false;

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

      if (this.detectionSetting.TimeFactor == 1000)
      {
        this.cbbTimeUnit.SelectedIndex = 0;
      }
      else if (this.detectionSetting.TimeFactor == 1)
      {
        this.cbbTimeUnit.SelectedIndex = 1;
      }
      else if (this.detectionSetting.TimeFactor.Equals(0.001d))
      {
        this.cbbTimeUnit.SelectedIndex = 2;
      }

      switch (this.detectionSetting.TrialImportMode)
      {
        case TrialSequenceImportModes.UseMSGLines:
          this.rdbUseMSGLines.Checked = true;
          this.SwitchVisibility(false, false);
          break;
        case TrialSequenceImportModes.UseAssignmentTable:
          if (this.detectionSetting.TrialImportFile != string.Empty)
          {
            this.rdbImportTableTrialID.Checked = true;
            this.txbImportAssignFile.Text = Path.GetFileName(this.detectionSetting.TrialImportFile);

            this.trialColumnHeaders.Clear();
            this.trialImportRows.Clear();
            this.trialImportRows = this.asciiSetting.ParseFile(
              this.detectionSetting.TrialImportFile,
              this.numberOfImportLines,
              ref this.trialColumnHeaders);

            this.cbbAssignStartingTimeColumn.Items.Clear();
            this.cbbAssignTrialIDColumn.Items.Clear();
            this.cbbAssignTrialSequenceColumn.Items.Clear();

            this.cbbAssignStartingTimeColumn.Items.AddRange(this.trialColumnHeaders.ToArray());
            this.cbbAssignTrialIDColumn.Items.Add("Automatic numbering");
            this.cbbAssignTrialIDColumn.Items.AddRange(this.trialColumnHeaders.ToArray());
            this.cbbAssignTrialSequenceColumn.Items.Add("Automatic numbering");
            this.cbbAssignTrialSequenceColumn.Items.AddRange(this.trialColumnHeaders.ToArray());
            this.SwitchVisibility(true, true);
          }
          else
          {
            this.rdbEnterTableTrialID.Checked = true;
            this.SwitchVisibility(true, false);
          }

          foreach (KeyValuePair<int, long> pair in this.detectionSetting.TrialSequenceToStarttimeAssignments)
          {
            int trialID = (int)pair.Key;
            if (this.detectionSetting.TrialSequenceToTrialIDAssignments.ContainsKey(pair.Key))
            {
              trialID = this.detectionSetting.TrialSequenceToTrialIDAssignments[pair.Key];
            }

            this.dgvAssignments.Rows.Add(new object[] { pair.Key, trialID, (long)(pair.Value / this.detectionSetting.TimeFactor) });
          }

          break;
        case TrialSequenceImportModes.UseImportColumn:
          this.rdbUseTrialColumn.Checked = true;
          this.rdbUseMSGLines.Enabled = false;
          this.rdbImportTableTrialID.Enabled = false;
          this.rdbEnterTableTrialID.Enabled = false;
          this.txbTrialMSG.Enabled = false;
          this.txbTrialSequenceColumn.Text = this.asciiSetting.ColumnAssignments["TrialSequence"];
          this.SwitchVisibility(false, false);
          break;
        default:
          break;
      }

      this.txbTrialMSG.Text = this.detectionSetting.TrialTriggerString;
      this.txbTrialSequenceColumn.Text = this.asciiSetting.ColumnAssignments["TrialSequence"];

      this.isInitializing = false;
      this.UpdatePreview();
    }

    #endregion //METHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER

    /// <summary>
    /// Changes visibility of the image import table and file controls
    /// </summary>
    /// <param name="isTableVisible"><strong>True</strong>, 
    /// if assignment data grid view should be visible.</param>
    /// <param name="isFileVisible"><strong>True</strong>, 
    /// if filename text box and open button should be visible.</param>
    private void SwitchVisibility(bool isTableVisible, bool isFileVisible)
    {
      this.grbFileName.Visible = isFileVisible;
      this.dgvAssignments.Visible = isTableVisible;
      this.txbImportAssignFile.Visible = isFileVisible;
      this.btnOpenAssignFile.Visible = isFileVisible;
      this.spcTableDropDowns.Panel2Collapsed = !this.rdbImportTableTrialID.Checked;
    }

    /// <summary>
    /// This method updates the trial import mode.
    /// </summary>
    private void SetImportMode()
    {
      if (this.rdbUseTrialColumn.Checked)
      {
        this.detectionSetting.TrialImportMode = TrialSequenceImportModes.UseImportColumn;
        this.SwitchVisibility(false, false);
      }
      else if (this.rdbUseMSGLines.Checked)
      {
        this.detectionSetting.TrialImportMode = TrialSequenceImportModes.UseMSGLines;
        this.SwitchVisibility(false, false);
      }
      else if (this.rdbEnterTableTrialID.Checked)
      {
        this.detectionSetting.TrialImportMode = TrialSequenceImportModes.UseAssignmentTable;
        this.SwitchVisibility(true, false);
      }
      else if (this.rdbImportTableTrialID.Checked)
      {
        this.detectionSetting.TrialImportMode = TrialSequenceImportModes.UseAssignmentTable;
        this.SwitchVisibility(true, true);
      }

      this.UpdatePreview();
    }

    #endregion //HELPER
  }
}