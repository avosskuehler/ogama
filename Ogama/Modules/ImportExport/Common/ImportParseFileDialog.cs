// <copyright file="ImportParseFileDialog.cs" company="FU Berlin">
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

namespace Ogama.Modules.ImportExport.Common
{
  using System;
  using System.Collections.Generic;
  using System.IO;
  using System.Windows.Forms;

  using Ogama.ExceptionHandling;
  using Ogama.Modules.Common.FormTemplates;

  /// <summary>
  /// This dialog <see cref="Form"/> derives from <see cref="FormWithAccellerators"/>
  /// and is the second part of the import process.
  /// It is intented to specify the settings needed to correct read
  /// a given ascii raw data file.
  /// The user can specify quote characters and other options and a 
  /// data grid view previews the under the current settings readed data.
  /// </summary>
  /// <remarks>It uses and updates the <see cref="ASCIISettings"/> given during
  /// construction.</remarks>
  public partial class ImportParseFileDialog : FormWithAccellerators
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
    /// Saves the number of preview data lines.
    /// </summary>
    private int numberOfImportLines;

    /// <summary>
    /// Saves the <see cref="ASCIISettings"/> with the current import settings.
    /// </summary>
    private ASCIISettings asciiSetting;

    /// <summary>
    /// Saves a flag, whether this form is currently initialzing.
    /// </summary>
    /// <remarks>Used to disable update during <see cref="Form.Load"/></remarks>
    private bool isInitializing;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the ImportParseFileDialog class.
    /// </summary>
    /// <param name="importSettings">ref. A <see cref="ASCIISettings"/>
    /// with the settings to use and update.</param>
    public ImportParseFileDialog(ref ASCIISettings importSettings)
    {
      this.InitializeComponent();
      this.asciiSetting = importSettings;
      this.numberOfImportLines = 1000;
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
    /// The <see cref="Form.Load"/> event handler. Updates the UI 
    /// with the current <see cref="ASCIISettings"/>.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void frmImportReadFile_Load(object sender, EventArgs e)
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
    private void frmImportReadFile_FormClosing(object sender, FormClosingEventArgs e)
    {
      if (this.DialogResult == DialogResult.Retry)
      {
        e.Cancel = true;
      }
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for
    /// the <see cref="Button"/> <see cref="btnNext"/>.
    /// Check parsing and if it is not succesful set the
    /// <see cref="DialogResult.Retry"/> to cancel closing
    /// during <see cref="Form.FormClosing"/> and have a second try.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnNext_Click(object sender, EventArgs e)
    {
      if (!this.ValidateParsing())
      {
        this.DialogResult = DialogResult.Retry;
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
      this.UpdatePreview(false);
    }

    /// <summary>
    /// The <see cref="CheckBox.CheckedChanged"/> event handler for
    /// the <see cref="CheckBox"/> <see cref="chbIgnoreSmallLines"/>.
    /// User switched "small line number ignore" state, so update preview.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void chbIgnoreSmallLines_CheckedChanged(object sender, EventArgs e)
    {
      this.asciiSetting.IgnoreSmallLines = this.chbIgnoreSmallLines.Checked;
      this.UpdatePreview(true);
    }

    /// <summary>
    /// The <see cref="CheckBox.CheckedChanged"/> event handler for
    /// the <see cref="CheckBox"/> <see cref="chbFirstLineColumnTitle"/>.
    /// User switched "column title is in first line" state, so update preview.
    /// </summary>
    /// <remarks>If this is called during <see cref="Form.Load"/> event
    /// the <see cref="ASCIISettings.ColumnHeaders"/> are not reset,
    /// otherwise they have to be resetted.</remarks>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void chbFirstLineColumnTitle_CheckedChanged(object sender, EventArgs e)
    {
      this.asciiSetting.ColumnTitlesAtFirstRow = this.chbFirstLineColumnTitle.Checked;
      if (!this.isInitializing)
      {
        this.asciiSetting.ColumnHeaders.Clear();
      }

      this.UpdatePreview(true);
    }

    /// <summary>
    /// The <see cref="CheckBox.CheckedChanged"/> event handler for
    /// the <see cref="CheckBox"/> <see cref="chbPreviousLineColumnTitle"/>.
    /// User switched "column title is above first line" state, so update preview.
    /// </summary>
    /// <remarks>If this is called during <see cref="Form.Load"/> event
    /// the <see cref="ASCIISettings.ColumnHeaders"/> are not reset,
    /// otherwise they have to be resetted.</remarks>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void chbPreviousLineColumnTitle_CheckedChanged(object sender, EventArgs e)
    {
        this.asciiSetting.ColumnTitlesAtPreviousRow = this.chbPreviousLineColumnTitle.Checked;
        if (!this.isInitializing)
        {
            this.asciiSetting.ColumnHeaders.Clear();
        }

        this.UpdatePreview(true);

    }

    /// <summary>
    /// The <see cref="CheckBox.CheckedChanged"/> event handler for
    /// the <see cref="CheckBox"/> <see cref="chbIgnoreLinesQuoted"/>.
    /// User switched "ignore lines with less columns than in first line" state, so update preview.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void chbIgnoreLinesQuoted_CheckedChanged(object sender, EventArgs e)
    {
      this.asciiSetting.IgnoreQuotes = this.chbIgnoreLinesQuoted.Checked;
      this.UpdatePreview(true);
    }

    /// <summary>
    /// The <see cref="CheckBox.CheckedChanged"/> event handler for
    /// the <see cref="CheckBox"/> <see cref="chbUseLines"/>.
    /// User switched "use only lines with the quotation string" state, so update preview.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void chbUseLines_CheckedChanged(object sender, EventArgs e)
    {
      this.asciiSetting.UseQuotes = this.chbUseLines.Checked;
      this.UpdatePreview(true);
    }

    /// <summary>
    /// The <see cref="Control.TextChanged"/> event handler for
    /// the <see cref="TextBox"/> <see cref="txbUseQuote"/>.
    /// User modified quotation string for used lines, so update preview.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void txbUseQuote_TextChanged(object sender, EventArgs e)
    {
      this.asciiSetting.UseQuotationString = this.txbUseQuote.Text;
      this.UpdatePreview(true);
    }

    /// <summary>
    /// The <see cref="Control.TextChanged"/> event handler for
    /// the <see cref="TextBox"/> <see cref="txbIgnoreTrigger"/>.
    /// User modified ignore trigger string, so update preview.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void txbIgnoreTrigger_TextChanged(object sender, EventArgs e)
    {
      this.asciiSetting.IgnoreTriggerString = this.txbIgnoreTrigger.Text;
      this.UpdatePreview(true);
    }

    /// <summary>
    /// The <see cref="CheckBox.CheckedChanged"/> event handler for
    /// the <see cref="CheckBox"/> <see cref="chbIgnoreLinesContaining"/>.
    /// User switched "ignore lines containing trigger string" state, so update preview.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void chbIgnoreLinesContaining_CheckedChanged(object sender, EventArgs e)
    {
      this.asciiSetting.IgnoreTriggerStringLines = this.chbIgnoreLinesContaining.Checked;
      this.UpdatePreview(true);
    }

    /// <summary>
    /// The <see cref="CheckBox.CheckedChanged"/> event handler for
    /// the <see cref="CheckBox"/> <see cref="chbIgnoreNotNumberLines"/>.
    /// User switched "ignore Lines that don´t start with a number" state, so update preview.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void chbIgnoreNotNumberLines_CheckedChanged(object sender, EventArgs e)
    {
      this.asciiSetting.IgnoreNotNumeralLines = this.chbIgnoreNotNumberLines.Checked;
      this.UpdatePreview(true);
    }

    /// <summary>
    /// The <see cref="Control.TextChanged"/> event handler for
    /// the <see cref="TextBox"/> <see cref="txbQuote"/>.
    /// User modified quote character, so update preview.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void txbQuote_TextChanged(object sender, EventArgs e)
    {
      this.asciiSetting.IgnoreQuotationString = this.txbQuote.Text;
      this.UpdatePreview(true);
    }

    /// <summary>
    /// The <see cref="ComboBox.SelectedIndexChanged"/> event handler for
    /// the <see cref="ComboBox"/> <see cref="cbbColumnSeparator"/>.
    /// User modified column separator character, so update preview.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void cbbColumnSeparator_SelectedIndexChanged(object sender, EventArgs e)
    {
      switch (this.cbbColumnSeparator.SelectedIndex)
      {
        case 0:
          this.asciiSetting.ColumnSeparatorCharacter = '\t';
          break;
        case 1:
          this.asciiSetting.ColumnSeparatorCharacter = ' ';
          break;
        case 2:
          this.asciiSetting.ColumnSeparatorCharacter = ',';
          break;
        case 3:
          this.asciiSetting.ColumnSeparatorCharacter = '.';
          break;
        case 4:
          this.asciiSetting.ColumnSeparatorCharacter = ';';
          break;
        default:
          this.asciiSetting.ColumnSeparatorCharacter = '\t';
          break;
      }

      this.UpdatePreview(true);
    }

    /// <summary>
    /// The <see cref="ComboBox.SelectedIndexChanged"/> event handler for
    /// the <see cref="ComboBox"/> <see cref="cbbDecimalSeparator"/>.
    /// User modified decimal separator character, so update preview.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void cbbDecimalSeparator_SelectedIndexChanged(object sender, EventArgs e)
    {
      switch (this.cbbDecimalSeparator.SelectedIndex)
      {
        case 0:
          this.asciiSetting.DecimalSeparatorCharacter = ',';
          break;
        case 1:
          this.asciiSetting.DecimalSeparatorCharacter = '.';
          break;
        default:
          this.asciiSetting.DecimalSeparatorCharacter = '.';
          break;
      }

      this.UpdatePreview(true);
    }

    /// <summary>
    /// The <see cref="Control.TextChanged"/> event handler for
    /// the <see cref="TextBox"/> <see cref="txbImportRawFile"/>.
    /// User modified import file name, so update preview.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void txbImportRawFile_TextChanged(object sender, EventArgs e)
    {
      this.UpdatePreview(true);
    }

    /// <summary>
    /// The <see cref="DataGridView.ColumnHeaderMouseClick"/> event handler for
    /// the <see cref="DataGridView"/> <see cref="dGVPreviewImport"/>.
    /// Column header clicked. Used for changing column header text with dialog.
    /// If the user clicked on the column header a <see cref="AskForColumTitleDialog"/>
    /// dialog is shown and the <see cref="ASCIISettings.ColumnHeaders"/>
    /// is updated.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void dGVPreviewImport_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
    {
      AskForColumTitleDialog dlg = new AskForColumTitleDialog();
      string oldHeader = this.dGVPreviewImport.Columns[e.ColumnIndex].HeaderText;
      dlg.ColumnTitle = oldHeader;
      if (dlg.ShowDialog() == DialogResult.OK)
      {
        this.dGVPreviewImport.Columns[e.ColumnIndex].HeaderText = dlg.ColumnTitle;
      }

      // Rewrite column headers dictionary.
      this.asciiSetting.ColumnHeaders.Clear();
      for (int i = 0; i < this.dGVPreviewImport.Columns.Count; i++)
      {
        this.asciiSetting.ColumnHeaders.Add(this.dGVPreviewImport.Columns[i].HeaderText);
      }

      // Update columnassignments if applicable
      string key = string.Empty;
      if (this.asciiSetting.ColumnAssignments.ContainsValue(oldHeader))
      {
        foreach (KeyValuePair<string, string> kvp in this.asciiSetting.ColumnAssignments)
        {
          if (kvp.Value == oldHeader)
          {
            key = kvp.Key;
          }
        }

        this.asciiSetting.ColumnAssignments[key] = dlg.ColumnTitle;
      }

      this.UpdatePreview(false);
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
    /// This method updates the UI components according 
    /// to the current <see cref="ASCIISettings"/>.
    /// </summary>
    private void UpdateUIWithSettings()
    {
      this.isInitializing = true;
      switch (this.asciiSetting.ColumnSeparatorCharacter)
      {
        case '\t':
          this.cbbColumnSeparator.SelectedIndex = 0;
          break;
        case ' ':
          this.cbbColumnSeparator.SelectedIndex = 1;
          break;
        case ',':
          this.cbbColumnSeparator.SelectedIndex = 2;
          break;
        case '.':
          this.cbbColumnSeparator.SelectedIndex = 3;
          break;
        case ';':
          this.cbbColumnSeparator.SelectedIndex = 4;
          break;
        default:
          this.cbbColumnSeparator.SelectedIndex = 0;
          break;
      }

      switch (this.asciiSetting.DecimalSeparatorCharacter)
      {
        case ',':
          this.cbbDecimalSeparator.SelectedIndex = 0;
          break;
        case '.':
          this.cbbDecimalSeparator.SelectedIndex = 1;
          break;
        default:
          this.cbbDecimalSeparator.SelectedIndex = 1;
          break;
      }

      this.chbIgnoreLinesContaining.Checked = this.asciiSetting.IgnoreTriggerStringLines;
      this.txbIgnoreTrigger.Text = this.asciiSetting.IgnoreTriggerString;
      this.chbIgnoreLinesQuoted.Checked = this.asciiSetting.IgnoreQuotes;
      this.chbIgnoreSmallLines.Checked = this.asciiSetting.IgnoreSmallLines;
      this.chbUseLines.Checked = this.asciiSetting.UseQuotes;
      this.txbUseQuote.Text = this.asciiSetting.UseQuotationString;
      this.txbQuote.Text = this.asciiSetting.IgnoreQuotationString;
      this.chbFirstLineColumnTitle.Checked = this.asciiSetting.ColumnTitlesAtFirstRow;
      this.chbIgnoreNotNumberLines.Checked = this.asciiSetting.IgnoreNotNumeralLines;
      this.txbImportRawFile.Text = Path.GetFileName(this.asciiSetting.Filename);
      this.isInitializing = false;
      if (this.asciiSetting.ColumnHeaders.Count > 0)
      {
        this.UpdatePreview(false);
      }
      else
      {
        this.UpdatePreview(true);
      }
    }

    /// <summary>
    /// Recalculates the preview datagridview by calling
    /// the <see cref="ASCIISettings.ParseFile(string,int,ref List{string})"/>
    /// method and adding the parses rows to the <see cref="dGVPreviewImport"/>.
    /// </summary>
    /// <param name="overwriteColumnHeadersFromFile"><strong>True</strong>,
    /// if the column headers should be reread from the parsed file,
    /// if the should be saved as they currently are, <strong>false</strong>.</param>
    private void UpdatePreview(bool overwriteColumnHeadersFromFile)
    {
      if (!this.isInitializing)
      {
        this.Cursor = Cursors.WaitCursor;

        List<string> columnHeaders = new List<string>();

        // Read import file with given settings.
        List<string[]> rows = this.asciiSetting.ParseFile(
          this.asciiSetting.Filename,
          this.numberOfImportLines,
          ref columnHeaders);

        // Reset column headers, if import file has more or less columns,
        // than settings import description (wrong settings file used)
        if (columnHeaders.Count != this.asciiSetting.ColumnHeaders.Count)
        {
          overwriteColumnHeadersFromFile = true;
        }

        // Save generated column headers,
        // but if they have equal size, use the first generated column headers,
        // because they could have been overwritten with new column 
        // names from the user by clicking on the header in the form.
        if (overwriteColumnHeadersFromFile)
        {
          this.asciiSetting.ColumnHeaders = columnHeaders;
          this.asciiSetting.ColumnAssignments.Clear();
        }

        // Clear preview data grid view
        this.dGVPreviewImport.Columns.Clear();
        this.dGVPreviewImport.Rows.Clear();

        // Skip if import setting count to much columns (settings must be wrong)
        if (columnHeaders.Count < 200)
        {
          // Add headers of preview data grid view
          foreach (string columnHeader in this.asciiSetting.ColumnHeaders)
          {
            int index = this.dGVPreviewImport.Columns.Add(columnHeader, columnHeader);
            this.dGVPreviewImport.Columns[index].SortMode = DataGridViewColumnSortMode.NotSortable;
          }

          // Add import rows to preview data grid view
          foreach (string[] items in rows)
          {
            this.dGVPreviewImport.Rows.Add(items);
          }
        }

        this.Cursor = Cursors.Default;
      }
    }

    #endregion //METHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER

    /// <summary>
    /// This method checks for correct parsing
    /// and returns false if no columns are found in the import file.
    /// </summary>
    /// <returns><strong>True</strong> if succesful, otherwise
    /// <strong>false</strong>.</returns>
    private bool ValidateParsing()
    {
      if (this.asciiSetting.ColumnHeaders.Count == 0)
      {
        string message = "Under the given parsing conditions no data column could be read ..." +
          Environment.NewLine + "Please try again with other settings until the preview table " +
          "shows the import file in a correct manner.";
        InformationDialog.Show("No data column found ...", message, false, MessageBoxIcon.Warning);
        return false;
      }

      List<string> test = new List<string>();
      foreach (string header in this.asciiSetting.ColumnHeaders)
      {
        if (test.Contains(header))
        {
          string message = "The import columns should have unique column headers." + Environment.NewLine +
            "The column header: '" + header + "' is not unique." + Environment.NewLine +
            "Please go back and change by clicking on the header.";
          InformationDialog.Show("Duplicated column name ...", message, false, MessageBoxIcon.Warning);
          return false;
        }

        test.Add(header);
      }

      return true;
    }

    #endregion //HELPER
    
    /// <summary>
    /// The <see cref="CheckBox.CheckedChanged"/> event handler for
    /// the <see cref="CheckBox"/> <see cref="chbUseLines"/>.
    /// User switched "ignore double timestamps" state, so update preview.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void chbIgnoreDoubleTime_CheckedChanged(object sender, EventArgs e)
    {
        this.asciiSetting.IgnoreDoubles = this.chbIgnoreDoubleTime.Checked;
        this.UpdatePreview(true);
    }
    
  }
}

