// <copyright file="DatabaseModule.cs" company="FU Berlin">
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

namespace Ogama.Modules.Database
{
  using System;
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.Data;
  using System.Data.SqlClient;
  using System.Data.SqlTypes;
  using System.Drawing;
  using System.Globalization;
  using System.IO;
  using System.Text;
  using System.Windows.Forms;

  using Ogama.DataSet;
  using Ogama.ExceptionHandling;
  using Ogama.MainWindow;
  using Ogama.Modules.Common;
  using Ogama.Modules.Common.FormTemplates;
  using Ogama.Modules.Common.SlideCollections;
  using Ogama.Modules.Common.Tools;
  using Ogama.Modules.ImportExport.OgamaData;
  using Ogama.Modules.ImportExport.RawData;

  using VectorGraphics.Elements;
  using VectorGraphics.StopConditions;

  /// <summary>
  /// Derived from <see cref="FormWithAccellerators"/>.
  /// This <see cref="Form"/> is the database module. 
  /// It displays all tables from the database:
  /// <see cref="OgamaDataSet.SubjectsDataTable"/>
  /// <see cref="OgamaDataSet.SubjectParametersDataTable"/>
  /// <see cref="OgamaDataSet.TrialsDataTable"/> 
  /// <see cref="OgamaDataSet.TrialEventsDataTable"/>
  /// <see cref="OgamaDataSet.AOIsDataTable"/>
  /// <see cref="OgamaDataSet.GazeFixationsDataTable"/>
  /// <see cref="OgamaDataSet.MouseFixationsDataTable"/>
  /// <see cref="OgamaDataSet.ShapeGroupsDataTable"/>
  /// The user can edit and revise the tables.
  /// </summary>
  /// <remarks>This module is intended to give the option
  /// to take a look at all of the databasis entries, that are
  /// made during recording, import or calculation, to see 
  /// what is stored in behind the user interface.
  /// It contains the basis for all visualization and 
  /// is the module with the import assistant for raw data.</remarks>
  public partial class DatabaseModule : FormWithInterface
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
    /// Holds the current raw datas binding source
    /// </summary>
    private BindingSource bsoRawdata;

    /// <summary>
    /// Indicates inhibition of database queries if this flag is set.
    /// </summary>
    private bool isUpdatingData;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the DatabaseModule class.
    /// </summary>
    public DatabaseModule()
    {
      this.InitializeComponent();

      this.InitAccelerators();
      this.InitializeCustomElements();
      this.InitializeDataBindings();
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES
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

    /// <summary>
    /// Overridden. Frees resources of objects that are not disposed
    /// by the designer, mainly private objects.
    /// Is called during the call to <see cref="Control.Dispose(Boolean)"/>.
    /// </summary>
    protected override void CustomDispose()
    {
      if (this.bsoRawdata != null)
      {
        this.bsoRawdata.Dispose();
      }

      base.CustomDispose();
    }

    /// <summary>
    /// Initialize custom elements
    /// </summary>
    protected override void InitializeCustomElements()
    {
      base.InitializeCustomElements();
      this.btnHelp.Click += new EventHandler(this.btnHelp_Click);
    }

    /// <summary>
    /// Initializes accelerator keys. Binds to methods.
    /// </summary>
    protected override void InitAccelerators()
    {
      SetAccelerator(Keys.Escape, new AcceleratorAction(this.OnEscape));
    }

    /// <summary>
    /// Initializes data bindings. Mainly wires the assigned Document dataset to the binding
    /// sources.
    /// </summary>
    protected override void InitializeDataBindings()
    {
      this.bsoRawdata = new BindingSource();

      base.InitializeDataBindings();

      this.dgvRawData.DataSource = this.bsoRawdata;

      // Set read only data grid view styles.
      this.SetDataGridViewColumnsToReadOnlyStyle(
        this.dgvShapeGroups,
        new string[] { "colShapeGroupsID" });

      // Set read only data grid view styles.
      this.SetDataGridViewColumnsToReadOnlyStyle(
        this.dgvParams,
        new string[] { "colParamsID" });

      // Set read only data grid view styles.
      this.SetDataGridViewColumnsToReadOnlyStyle(
        this.dgvSubjectParameters,
        new string[] { "colSubjectParametersID" });

      // Set read only data grid view styles.
      this.SetDataGridViewColumnsToReadOnlyStyle(
        this.dgvSubjects,
        new string[] { "colSubjectsID", "colSubjectsSubjectName" });

      string[] columnArray = new string[] 
      { 
        "colTrialsID", 
        "colTrialsSubjectName",
        "colTrialsTrialID",
        "colTrialsTrialSequence",
        "colTrialsTrialStartTime",
        "colTrialsDuration" 
      };

      this.SetDataGridViewColumnsToReadOnlyStyle(this.dgvTrials, columnArray);

      // SetDataGridViewColumnsToReadOnlyStyle(this.dgvTrialEvents);
      this.SetDataGridViewColumnsToReadOnlyStyle(this.dgvGazeFixations);
      this.SetDataGridViewColumnsToReadOnlyStyle(this.dgvMouseFixations);

      columnArray = new string[] 
      {
        "colAOIsID", 
        "colAOIsTrialID",
        "colAOIsSlideNr",
        "colAOIsShapeType",
        "colAOIsShapeNumPts",
        "colAOIsShapePts" 
      };

      this.SetDataGridViewColumnsToReadOnlyStyle(this.dgvAOIs, columnArray);
    }

    /// <summary>
    /// The <see cref="MainForm.EditCopy"/> event handler for the
    /// parent <see cref="Form"/> <see cref="MainForm"/>.
    /// This method handles the edit copy event from main form
    /// by either copying selected cells in data grid view or 
    /// rendering a copy of the displayed picture 
    /// to clipboard, depending on focus.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    protected override void mainWindow_EditCopy(object sender, EventArgs e)
    {
      if (this.MdiParent.ActiveMdiChild.Name == this.Name)
      {
        try
        {
          this.Cursor = Cursors.WaitCursor;
          Control dataGridView = this.tacTables.SelectedTab.Controls[0];
          if (dataGridView is DataGridView && dataGridView.Focused)
          {
            Clipboard.SetDataObject(((DataGridView)dataGridView).GetClipboardContent());
            ((MainForm)this.MdiParent).StatusLabel.Text = "Table selection exported to clipboard.";
          }
        }
        catch (Exception ex)
        {
          ExceptionMethods.HandleException(ex);
        }
        finally
        {
          this.Cursor = Cursors.Default;
        }
      }
    }

    #endregion //OVERRIDES

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler                                                              //
    ///////////////////////////////////////////////////////////////////////////////
    #region EVENTS

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler for UI, Menu, Buttons, Toolbars etc.                         //
    ///////////////////////////////////////////////////////////////////////////////
    #region WINDOWSEVENTHANDLER

    /// <summary>
    /// <see cref="Form.Load"/> event handler. 
    /// Initializes first display.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void DatabaseModule_Load(object sender, EventArgs e)
    {
      if (this.btnFilterData.Checked)
      {
        this.LoadRawDataIntoDataGridView(0);
      }
      else
      {
        this.PopulateRawDataFromCurrentSubject();
      }
    }

    /// <summary>
    /// The <see cref="Form.FormClosing"/> event handler for this module.
    /// Checks for uncommitted changes to the database and asks the user
    /// to save or cancel.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void DatabaseModule_FormClosing(object sender, FormClosingEventArgs e)
    {
      if (Document.ActiveDocument.DocDataSet.HasChanges())
      {
        DialogResult result = InformationDialog.Show(
          "Commit changes ?",
          "Would you like to commit the changes made to the database ?",
          true,
          MessageBoxIcon.Question);

        switch (result)
        {
          case DialogResult.Cancel:
            e.Cancel = true;
            break;
          case DialogResult.No:
            Document.ActiveDocument.DocDataSet.RejectChanges();
            break;
          case DialogResult.Yes:
            this.UpdateDatabase();
            break;
        }

        ((MainForm)this.MdiParent).RefreshContextPanelSubjects();
      }
    }

    #region Buttons

    /// <summary>
    /// <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnImport"/>.
    /// Imports raw data with import dialog.
    /// </summary>
    /// <seealso cref="ImportExport"/>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnImport_Click(object sender, EventArgs e)
    {
      ImportRawData.Start((MainForm)this.MdiParent);
      ((MainForm)this.MdiParent).RefreshContextPanelSubjects();
    }

    /// <summary>
    /// <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnImportOgamaFormat"/>.
    /// Imports raw data in Ogama format.
    /// </summary>
    /// <seealso cref="ImportExport"/>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnImportOgamaFormat_Click(object sender, EventArgs e)
    {
      ImportOgamaData.Start();
      ((MainForm)this.MdiParent).RefreshContextPanelSubjects();
    }

    /// <summary>
    /// <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnExport"/>.
    /// User clicked export database button. Opens file name dialog for export
    /// file and afterwards write raw data for selected subjects.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnExport_Click(object sender, EventArgs e)
    {
      if (!this.bgwExport.IsBusy)
      {
        string subjectList = string.Empty;
        string separator = "\t";

        // Select full rows of selected subjects.
        if (this.dgvSubjects.SelectedRows.Count == 0)
        {
          foreach (DataGridViewCell cell in this.dgvSubjects.SelectedCells)
          {
            cell.OwningRow.Selected = true;
          }
        }

        // Create subject name list.
        foreach (DataGridViewRow row in this.dgvSubjects.SelectedRows)
        {
          string subjectName = row.Cells["colSubjectsSubjectName"].Value.ToString();
          subjectList += subjectName + ";";
        }

        // Ask for filename, write column descriptions and start exporting.
        if (this.sfdExport.ShowDialog() == DialogResult.OK)
        {
          string filenameExport = this.sfdExport.FileName;
          int filterIndex = this.sfdExport.FilterIndex;

          if (filterIndex == 1)
          {
            // TXT Files
            separator = "\t";
          }
          else if (filterIndex == 2)
          {
            // CSV Files
            separator = ";";
          }

          this.bgwExport.RunWorkerAsync(new string[] { subjectList, separator, filenameExport });
        }
      }
      else
      {
        MessageBox.Show("An export is currently in progress, please wait until it is finished");
      }
    }

    /// <summary>
    /// <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnFilterData"/>.
    /// User clicked filter data button. 
    /// Switches between subject and trial constrained and not constrained
    /// binding sources.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnFilterData_Click(object sender, EventArgs e)
    {
      this.Cursor = Cursors.WaitCursor;
      if (this.btnFilterData.Checked)
      {
        this.dgvSubjectParameters.DataSource = this.bsoFKSubjectsSubjectParameters;
        this.dgvTrials.DataSource = this.bsoFKSubjectsTrials;
        this.dgvAOIs.DataSource = this.bsoTrialsAOIs;
        this.dgvGazeFixations.DataSource = this.bsoTrialsGazeFixations;
        this.dgvMouseFixations.DataSource = this.bsoTrialsMouseFixations;
        this.dgvTrialEvents.DataSource = this.bsoFKTrialsEvents;
        this.LoadRawDataIntoDataGridView(0);
      }
      else
      {
        this.dgvSubjectParameters.DataSource = this.bsoSubjectParameters;
        this.dgvTrials.DataSource = this.bsoTrials;
        this.dgvAOIs.DataSource = this.bsoAOIs;
        this.dgvGazeFixations.DataSource = this.bsoGazeFixations;
        this.dgvMouseFixations.DataSource = this.bsoMouseFixations;
        this.dgvTrialEvents.DataSource = this.bsoTrialEvents;
        this.PopulateRawDataFromCurrentSubject();
      }

      this.Cursor = Cursors.Default;
    }

    /// <summary>
    /// <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnSave"/>.
    /// User clicked save data button. 
    /// Updates the database (except changes in raw data table)
    /// with the changes made.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    /// <remarks>The changes in the raw data table are written 
    /// whenever a cell has been modified, so there is no need to
    /// save them again.</remarks>
    private void btnSave_Click(object sender, EventArgs e)
    {
      this.UpdateDatabase();
    }

    #endregion //Buttons

    #region dgvSubjects

    /// <summary>
    /// The <see cref="DataGridView.UserDeletingRow"/> event handler for the
    /// <see cref="DataGridView"/> <see cref="dgvSubjects"/>.
    /// User deleted row in main table, 
    /// so delete raw data and tables in database for the selected subject.
    /// Sets wait cursor, because it may last a while.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A <see cref="DataGridViewRowCancelEventArgs"/> with the event data.</param>
    private void dgvSubjects_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
    {
      try
      {
        string subject = Convert.ToString(e.Row.Cells["colSubjectsSubjectName"].Value);
        if (subject != string.Empty)
        {
          InformationDialog question = new InformationDialog(
            "Delete subject ?",
            "Do you really want to delete the recording and videos for subject '" + subject + "'",
            true,
            MessageBoxIcon.Question);
          switch (question.ShowDialog())
          {
            case DialogResult.Cancel:
            case DialogResult.No:
              e.Cancel = true;
              return;
            case DialogResult.Yes:
              break;
          }

          this.Cursor = Cursors.WaitCursor;

          // Update other changes
          this.UpdateDatabase();

          this.isUpdatingData = true;

          // Delete raw data table
          Queries.DeleteRawDataTableInDB(subject);
          this.bsoRawdata.DataSource = null;

          // Delete trial events for subject
          int result = Document.ActiveDocument.DocDataSet.TrialEventsAdapter.DeleteBySubjectName(subject);
          result = Document.ActiveDocument.DocDataSet.TrialEventsAdapter.Fill(Document.ActiveDocument.DocDataSet.TrialEvents);

          // Delete trials for subject
          result = Document.ActiveDocument.DocDataSet.TrialsAdapter.DeleteBySubjectName(subject);
          result = Document.ActiveDocument.DocDataSet.TrialsAdapter.Fill(Document.ActiveDocument.DocDataSet.Trials);

          // Delete fixations for subject
          result = Document.ActiveDocument.DocDataSet.GazeFixationsAdapter.DeleteBySubject(subject);
          result = Document.ActiveDocument.DocDataSet.GazeFixationsAdapter.Fill(Document.ActiveDocument.DocDataSet.GazeFixations);
          result = Document.ActiveDocument.DocDataSet.MouseFixationsAdapter.DeleteBySubject(subject);
          result = Document.ActiveDocument.DocDataSet.MouseFixationsAdapter.Fill(Document.ActiveDocument.DocDataSet.MouseFixations);

          // Delete subject parameters for subject
          result = Document.ActiveDocument.DocDataSet.SubjectParametersAdapter.DeleteBySubjectName(subject);
          result = Document.ActiveDocument.DocDataSet.SubjectParametersAdapter.Fill(Document.ActiveDocument.DocDataSet.SubjectParameters);

          // Delete user video file.
          DirectoryInfo info = new DirectoryInfo(Document.ActiveDocument.ExperimentSettings.ThumbsPath);
          foreach (FileInfo videoFile in info.GetFiles(subject + "*"))
          {
            if (videoFile.Exists)
            {
              File.Delete(videoFile.FullName);
            }
          }

          this.isUpdatingData = false;

          // Database update is done 
          // when closing or on explicit saving with button.
        }
      }
      catch (Exception ex)
      {
        ExceptionMethods.HandleException(ex);
      }
      finally
      {
        this.Cursor = Cursors.Default;
      }
    }

    /// <summary>
    /// The <see cref="DataGridView.DataError"/> event handler for the
    /// <see cref="DataGridView"/> <see cref="dgvSubjects"/>.
    /// Does special error handling for common errors, e.g.
    /// shows a message box with information how to avoid this error.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A <see cref="DataGridViewDataErrorEventArgs"/> that provides 
    /// data for the DataError event.</param>
    private void dgvSubjects_DataError(object sender, DataGridViewDataErrorEventArgs e)
    {
      if (e.Exception is NoNullAllowedException)
      {
        // When importing the very first subject, an error occurs, because
        // the data grid view added two lines instead of one. The second has no
        // entry. I don´t know why but handled the NoNullAllowedException in the second row,
        // by deleting it...
        int rownumber = this.dgvSubjects.Rows.GetLastRow(DataGridViewElementStates.Displayed);

        if (this.dgvSubjects.Rows[rownumber - 1].Cells["colSubjectsSubjectName"].Value.ToString() == string.Empty)
        {
          this.dgvSubjects.Rows.Remove(this.dgvSubjects.Rows[rownumber - 1]);
        }
      }

      HandleDataGridViewError(sender, e);
    }

    /// <summary>
    /// The <see cref="DataGridView.RowEnter"/> event handler for the
    /// <see cref="DataGridView"/> <see cref="dgvSubjects"/>.
    /// User selected new row in subject data grid view, 
    /// so fill raw data view with new data if filter mode is unset.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A <see cref="DataGridViewCellEventArgs"/> with the event data.</param>
    private void dgvSubjects_RowEnter(object sender, DataGridViewCellEventArgs e)
    {
      if (!this.btnFilterData.Checked)
      {
        try
        {
          this.PopulateRawDataFromCurrentSubject();
        }
        catch (InvalidOperationException ex)
        {
          // Updating from record thread sometimes is a cross thread call
          // so just log it.
          ExceptionMethods.HandleExceptionSilent(ex);
        }
      }
    }

    #endregion //dgvSubjects

    #region dgvTrials

    /// <summary>
    /// The <see cref="DataGridView.RowEnter"/> event handler for the
    /// <see cref="DataGridView"/> <see cref="dgvTrials"/>.
    /// User selected new row in trials data grid view, 
    /// so fill raw data view with new data if filter mode is set.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A <see cref="DataGridViewCellEventArgs"/> with the event data.</param>
    private void dgvTrials_RowEnter(object sender, DataGridViewCellEventArgs e)
    {
      if (this.btnFilterData.Checked)
      {
        this.LoadRawDataIntoDataGridView(e.RowIndex);
      }
    }

    /// <summary>
    /// The <see cref="DataGridView.DataError"/> event handler for the
    /// current <see cref="DataGridView"/>.
    /// Does special error handling for common errors, e.g.
    /// shows a message box with information how to avoid this error.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A <see cref="DataGridViewDataErrorEventArgs"/> that provides 
    /// data for the DataError event.</param>
    private void dataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
    {
      HandleDataGridViewError(sender, e);
    }

    #endregion //dgvTrials

    #region dgvRawData

    /// <summary>
    /// The <see cref="Control.Validated"/> event handler for the
    /// <see cref="DataGridView"/> <see cref="dgvRawData"/>.
    /// Updates database with changes from raw data table.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/>.</param>
    private void dgvRawData_Validated(object sender, EventArgs e)
    {
      this.UpdateDatabaseWithRawData();
    }

    #endregion //dgvRawData

    /// <summary>
    /// <see cref="Control.Click"/> event handler for the
    /// <see cref="ToolStripMenuItem"/> <see cref="cmsSelect"/>.
    /// User clicked export select all from the context menu.
    /// Check focused data grid view and select all rows.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void cmsSelect_Click(object sender, EventArgs e)
    {
      Control dataGridView = this.tacTables.SelectedTab.Controls[0];
      if (dataGridView is DataGridView)
      {
        ((DataGridView)dataGridView).SelectAll();
      }
    }

    /// <summary>
    /// <see cref="Control.Click"/> event handler for the
    /// <see cref="ToolStripMenuItem"/> <see cref="cmsCopy"/>.
    /// User clicked copy from the context menu.
    /// Call the EditCopy event, to handle the copying in
    /// <see cref="DatabaseModule.mainWindow_EditCopy(object,EventArgs)"/>
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void cmsCopy_Click(object sender, EventArgs e)
    {
      this.mainWindow_EditCopy(this, EventArgs.Empty);
    }

    #endregion //WINDOWSEVENTHANDLER

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler for Custom Defined Events                                    //
    ///////////////////////////////////////////////////////////////////////////////
    #region CUSTOMEVENTHANDLER

    /// <summary>
    /// Breaks backgroundworker when user pressed escape button
    /// </summary>
    private void OnEscape()
    {
      if (this.bgwExport.IsBusy)
      {
        this.Cursor = Cursors.WaitCursor;
        this.bgwExport.CancelAsync();
      }
    }

    #endregion //CUSTOMEVENTHANDLER

    #endregion //EVENTS

    ///////////////////////////////////////////////////////////////////////////////
    // Methods and Eventhandling for Background tasks                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region BACKGROUNDWORKER

    /// <summary>
    /// Background worker thread working method for exporting the database.
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">An <see cref="DoWorkEventArgs"/> with the event data.</param>
    private void bgwExport_DoWork(object sender, DoWorkEventArgs e)
    {
      try
      {
        BackgroundWorker worker = (BackgroundWorker)sender;
        string subjectList = ((string[])e.Argument)[0];
        string[] subjects = subjectList.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
        string separator = ((string[])e.Argument)[1];
        string filenameExport = ((string[])e.Argument)[2];

        using (StreamWriter exportFileWriter = new StreamWriter(filenameExport))
        {
          // Write Documentation
          exportFileWriter.WriteLine("# File: " + Path.GetFileName(filenameExport));
          exportFileWriter.WriteLine("# Created: " + DateTime.Today.Date.ToLongDateString() + "," + DateTime.Now.ToLongTimeString());
          exportFileWriter.WriteLine("# with: " + Application.ProductName + " Version: " + Document.ActiveDocument.ExperimentSettings.OgamaVersion.ToString(3));
          exportFileWriter.WriteLine("# Contents: RawData of following Subjects:");
          exportFileWriter.Write("# Subjects: ");
          exportFileWriter.WriteLine(subjectList);
          exportFileWriter.WriteLine("# Applies to Projekt:" + Document.ActiveDocument.ExperimentSettings.Name);
          exportFileWriter.WriteLine("#");

          // Write Column Names
          exportFileWriter.Write("SubjectName");
          exportFileWriter.Write(separator);
          exportFileWriter.Write("SubjectCategory");
          exportFileWriter.Write(separator);
          exportFileWriter.Write("Age");
          exportFileWriter.Write(separator);
          exportFileWriter.Write("Sex");
          exportFileWriter.Write(separator);
          exportFileWriter.Write("Handedness");
          exportFileWriter.Write(separator);
          exportFileWriter.Write("Comments");
          exportFileWriter.Write(separator);

          exportFileWriter.Write("TrialID");
          exportFileWriter.Write(separator);
          exportFileWriter.Write("TrialName");
          exportFileWriter.Write(separator);
          exportFileWriter.Write("TrialSequence");
          exportFileWriter.Write(separator);
          exportFileWriter.Write("TrialCategory");
          exportFileWriter.Write(separator);
          exportFileWriter.Write("TrialStartTime");
          exportFileWriter.Write(separator);
          exportFileWriter.Write("Duration");
          exportFileWriter.Write(separator);
          exportFileWriter.Write("EliminateData");
          exportFileWriter.Write(separator);

          exportFileWriter.Write("EventEventID");
          exportFileWriter.Write(separator);
          exportFileWriter.Write("EventSequence");
          exportFileWriter.Write(separator);
          exportFileWriter.Write("EventTime");
          exportFileWriter.Write(separator);
          exportFileWriter.Write("EventType");
          exportFileWriter.Write(separator);
          exportFileWriter.Write("EventTask");
          exportFileWriter.Write(separator);
          exportFileWriter.Write("EventParam");
          exportFileWriter.Write(separator);

          exportFileWriter.Write("Time");
          exportFileWriter.Write(separator);
          exportFileWriter.Write("PupilDiaX");
          exportFileWriter.Write(separator);
          exportFileWriter.Write("PupilDiaY");
          exportFileWriter.Write(separator);
          exportFileWriter.Write("GazePosX");
          exportFileWriter.Write(separator);
          exportFileWriter.Write("GazePosY");
          exportFileWriter.Write(separator);
          exportFileWriter.Write("MousePosX");
          exportFileWriter.Write(separator);
          exportFileWriter.Write("MousePosY");
          exportFileWriter.Write(separator);
          exportFileWriter.Write("EventID");
          exportFileWriter.Write(separator);
          exportFileWriter.WriteLine();

          int subjectCounter = 0;
          foreach (string subject in subjects)
          {
            string subjectName = subject;

            DataTable tableSubjects =
              Document.ActiveDocument.DocDataSet.SubjectsAdapter.GetDataBySubject(subjectName);

            worker.ReportProgress(5, "Exporting " + subjectName);

            DataTable tableTrials =
              Document.ActiveDocument.DocDataSet.TrialsAdapter.GetDataBySubject(subjectName);

            worker.ReportProgress(10, "Exporting " + subjectName);

            DataTable tableTrialEvents =
              Document.ActiveDocument.DocDataSet.TrialEventsAdapter.GetDataBySubject(subjectName);

            worker.ReportProgress(10, "Exporting " + subjectName);

            DataTable tableRawData = Queries.GetRawDataBySubject(subjectName);

            worker.ReportProgress(50, "Exporting " + subjectName);

            // Write Data To File
            string trialSequence = string.Empty;
            string lastTrialSequence = string.Empty;
            DataRow[] trialRow = null;
            DataRow[] trialEventsRow = null;
            int progressCounter = 0;
            foreach (DataRow dataRow in tableRawData.Rows)
            {
              trialSequence = dataRow["TrialSequence"].ToString();
              if (trialSequence != lastTrialSequence)
              {
                trialRow = tableTrials.Select("TrialSequence=" + trialSequence + " AND SubjectName='" + subjectName + "'");
                lastTrialSequence = trialSequence;
              }

              // Avoid data from empty trials to be exported.
              if (trialRow.Length > 0)
              {
                exportFileWriter.Write(tableSubjects.Rows[0]["SubjectName"].ToString().Replace(";", ":"));
                exportFileWriter.Write(separator);
                exportFileWriter.Write(tableSubjects.Rows[0]["Category"].ToString().Replace(";", ":"));
                exportFileWriter.Write(separator);
                exportFileWriter.Write(tableSubjects.Rows[0]["Age"].ToString().Replace(";", ":"));
                exportFileWriter.Write(separator);
                exportFileWriter.Write(tableSubjects.Rows[0]["Sex"].ToString().Replace(";", ":"));
                exportFileWriter.Write(separator);
                exportFileWriter.Write(tableSubjects.Rows[0]["Handedness"].ToString().Replace(";", ":"));
                exportFileWriter.Write(separator);
                exportFileWriter.Write(tableSubjects.Rows[0]["Comments"].ToString().Replace(";", ":"));
                exportFileWriter.Write(separator);

                exportFileWriter.Write(trialRow[0]["TrialID"].ToString());
                exportFileWriter.Write(separator);
                exportFileWriter.Write(trialRow[0]["TrialName"].ToString().Replace(";", ":"));
                exportFileWriter.Write(separator);
                exportFileWriter.Write(trialRow[0]["TrialSequence"].ToString());
                exportFileWriter.Write(separator);
                exportFileWriter.Write(trialRow[0]["Category"].ToString().Replace(";", ":"));
                exportFileWriter.Write(separator);
                exportFileWriter.Write(trialRow[0]["TrialStartTime"].ToString());
                exportFileWriter.Write(separator);
                exportFileWriter.Write(trialRow[0]["Duration"].ToString());
                exportFileWriter.Write(separator);
                exportFileWriter.Write(trialRow[0]["EliminateData"].ToString());
                exportFileWriter.Write(separator);

                // Write Event row, if there is an EventID.
                string eventID = dataRow["EventID"].ToString();
                if (eventID != string.Empty)
                {
                  trialEventsRow = tableTrialEvents.Select("TrialSequence=" + trialSequence + " AND EventID='" + eventID + "'");

                  if (trialEventsRow.Length > 0)
                  {
                    exportFileWriter.Write(trialEventsRow[0]["EventID"].ToString());
                    exportFileWriter.Write(separator);
                    exportFileWriter.Write(trialEventsRow[0]["TrialSequence"].ToString());
                    exportFileWriter.Write(separator);
                    exportFileWriter.Write(trialEventsRow[0]["EventTime"].ToString());
                    exportFileWriter.Write(separator);
                    exportFileWriter.Write(trialEventsRow[0]["EventType"].ToString());
                    exportFileWriter.Write(separator);
                    exportFileWriter.Write(trialEventsRow[0]["EventTask"].ToString());
                    exportFileWriter.Write(separator);
                    exportFileWriter.Write(trialEventsRow[0]["EventParam"].ToString());
                    exportFileWriter.Write(separator);
                  }
                }
                else
                {
                  exportFileWriter.Write(string.Empty);
                  exportFileWriter.Write(separator);
                  exportFileWriter.Write(string.Empty);
                  exportFileWriter.Write(separator);
                  exportFileWriter.Write(string.Empty);
                  exportFileWriter.Write(separator);
                  exportFileWriter.Write(string.Empty);
                  exportFileWriter.Write(separator);
                  exportFileWriter.Write(string.Empty);
                  exportFileWriter.Write(separator);
                  exportFileWriter.Write(string.Empty);
                  exportFileWriter.Write(separator);
                }

                exportFileWriter.Write(dataRow["Time"].ToString());
                exportFileWriter.Write(separator);
                exportFileWriter.Write(dataRow.IsNull("PupilDiaX") ? string.Empty : Convert.ToSingle(dataRow["PupilDiaX"]).ToString("N4"));
                exportFileWriter.Write(separator);
                exportFileWriter.Write(dataRow.IsNull("PupilDiaY") ? string.Empty : Convert.ToSingle(dataRow["PupilDiaY"]).ToString("N4"));
                exportFileWriter.Write(separator);
                exportFileWriter.Write(dataRow.IsNull("GazePosX") ? string.Empty : Convert.ToSingle(dataRow["GazePosX"]).ToString("N4"));
                exportFileWriter.Write(separator);
                exportFileWriter.Write(dataRow.IsNull("GazePosY") ? string.Empty : Convert.ToSingle(dataRow["GazePosY"]).ToString("N4"));
                exportFileWriter.Write(separator);
                exportFileWriter.Write(dataRow.IsNull("MousePosX") ? string.Empty : Convert.ToSingle(dataRow["MousePosX"]).ToString("N2"));
                exportFileWriter.Write(separator);
                exportFileWriter.Write(dataRow.IsNull("MousePosY") ? string.Empty : Convert.ToSingle(dataRow["MousePosY"]).ToString("N2"));
                exportFileWriter.Write(separator);
                exportFileWriter.Write(eventID);

                exportFileWriter.WriteLine();
              }

              progressCounter++;

              if (progressCounter % 200 == 0)
              {
                int progress = (int)(progressCounter / (float)tableRawData.Rows.Count * 100 / 2 + 50);
                worker.ReportProgress(progress, "Exporting " + subjectName);

                if (worker.CancellationPending)
                {
                  break;
                }
              }
            }

            tableSubjects.Dispose();
            tableTrials.Dispose();
            tableRawData.Dispose();

            if (worker.CancellationPending)
            {
              break;
            }

            subjectCounter++;
          }
        }
      }
      catch (Exception ex)
      {
        ExceptionMethods.HandleException(ex);
      }
    }

    /// <summary>
    /// This event handler updates the progress bar in MDI Parent form.
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">A <see cref="ProgressChangedEventArgs"/> with the event data</param>
    private void bgwExport_ProgressChanged(object sender, ProgressChangedEventArgs e)
    {
      ((MainForm)this.MdiParent).StatusProgressbar.Value = e.ProgressPercentage;
      ((MainForm)this.MdiParent).StatusLabel.Text = e.UserState.ToString();
    }

    /// <summary>
    /// This event handler deals with the results of the
    /// background operation and updates the UI.
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">A <see cref="RunWorkerCompletedEventArgs"/> with the event data.</param>
    private void bgwExport_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
      ((MainForm)this.MdiParent).StatusLabel.Text = "Ready ...";
      ((MainForm)this.MdiParent).StatusProgressbar.Value = 0;
      this.Cursor = Cursors.Default;
      MessageBox.Show("Selected subjects succesfully exported");
    }

    #endregion //BACKGROUNDWORKER

    ///////////////////////////////////////////////////////////////////////////////
    // Methods for doing main class job                                          //
    ///////////////////////////////////////////////////////////////////////////////
    #region METHODS

    /// <summary>
    /// This method populates the raw data grid view with the whole data from the 
    /// currently selected subject row.
    /// </summary>
    private void PopulateRawDataFromCurrentSubject()
    {
      if (this.dgvSubjects.Rows.Count > 0)
      {
        if (this.dgvSubjects.SelectedRows.Count == 0)
        {
          this.dgvSubjects.Rows[0].Selected = true;
        }

        DataGridViewRow currentRow = this.dgvSubjects.Rows[this.dgvSubjects.SelectedRows[0].Index];
        string subjectName = currentRow.Cells["colSubjectsSubjectName"].Value.ToString();
        DataTable table = Queries.GetRawDataBySubject(subjectName);
        this.bsoRawdata.DataSource = table;
      }
    }

    /// <summary>
    /// Update the databases raw data tables with the user's changes,
    /// by using the table adapters.
    /// </summary>
    private void UpdateDatabaseWithRawData()
    {
      DataTable rawTable = null;
      try
      {
        DataGridViewRow currentRow = this.dgvTrials.CurrentRow;
        if (currentRow == null)
        {
          return;
        }

        string subjectName = (string)currentRow.Cells["colTrialsSubjectName"].Value;

        this.bsoRawdata = (BindingSource)this.dgvRawData.DataSource;
        rawTable = (DataTable)this.bsoRawdata.DataSource;

        DataTable modifiedRecords = rawTable.GetChanges(DataRowState.Modified);
        if (modifiedRecords != null)
        {
          foreach (DataRow rawRow in modifiedRecords.Rows)
          {
            Queries.UpdateRowBySubjectAndID(rawRow, subjectName, Convert.ToInt32(rawRow["ID"]));
          }
        }

        if (modifiedRecords != null)
        {
          modifiedRecords.Dispose();
        }

        if (rawTable != null)
        {
          rawTable.Dispose();
        }
      }
      catch (Exception ex)
      {
        if (rawTable != null)
        {
          rawTable.RejectChanges();
        }

        Document.ActiveDocument.DocDataSet.RejectChanges();

        ExceptionMethods.HandleException(ex);
      }

      if (rawTable != null)
      {
        rawTable.AcceptChanges();
      }
    }

    /// <summary>
    /// This method commits changes made in the tables to the database
    /// by calling the update method on each table adapter.
    /// When this was successfull the changes are accepted, otherwise
    /// they are rejected.
    /// </summary>
    private void UpdateDatabase()
    {
      try
      {
        this.Validate();
        this.bsoSubjects.EndEdit();
        this.bsoSubjectParameters.EndEdit();
        this.bsoParams.EndEdit();
        this.bsoTrials.EndEdit();
        this.bsoTrialEvents.EndEdit();
        this.bsoGazeFixations.EndEdit();
        this.bsoMouseFixations.EndEdit();
        this.bsoAOIs.EndEdit();
        this.bsoShapeGroups.EndEdit();

        OgamaDataSet dataset = Document.ActiveDocument.DocDataSet;

        int affectedRows = dataset.SubjectsAdapter.Update(dataset.Subjects);
        affectedRows = dataset.SubjectParametersAdapter.Update(dataset.SubjectParameters);
        affectedRows = dataset.ParamsAdapter.Update(dataset.Params);
        affectedRows = dataset.TrialsAdapter.Update(dataset.Trials);
        affectedRows = dataset.TrialEventsAdapter.Update(dataset.TrialEvents);
        affectedRows = dataset.GazeFixationsAdapter.Update(dataset.GazeFixations);
        affectedRows = dataset.MouseFixationsAdapter.Update(dataset.MouseFixations);
        affectedRows = dataset.AOIsAdapter.Update(dataset.AOIs);
        affectedRows = dataset.ShapeGroupsAdapter.Update(dataset.ShapeGroups);

        dataset.AcceptChanges();
      }
      catch (Exception ex)
      {
        string message = "Could not write changes to database." +
          Environment.NewLine +
          "The following exception occured: " +
          ex.Message;
        ExceptionMethods.ProcessErrorMessage(message);
        Document.ActiveDocument.DocDataSet.RejectChanges();
      }
    }

    /// <summary>
    /// Checks for existing trials and then loads the data from the dataset into
    /// the <see cref="dgvRawData"/> with setting its binding source.
    /// </summary>
    /// <param name="trialTableRowIndex">The row index of the trials table for
    /// which trial to load the raw data into the <see cref="dgvRawData"/></param>
    private void LoadRawDataIntoDataGridView(int trialTableRowIndex)
    {
      // This avoids update during record update.
      if (this.InvokeRequired || this.isUpdatingData)
      {
        return;
      }

      if (this.dgvTrials.DataSource != null)
      {
        try
        {
          this.Cursor = Cursors.WaitCursor;

          if (this.dgvTrials.Rows.Count > trialTableRowIndex)
          {
            DataGridViewRow currentRow = this.dgvTrials.Rows[trialTableRowIndex];
            string subjectName = currentRow.Cells["colTrialsSubjectName"].Value.ToString();
            int trialID = (int)currentRow.Cells["colTrialsTrialID"].Value;
            int trialSequence = (int)currentRow.Cells["colTrialsTrialSequence"].Value;
            DataTable table = Queries.GetRawDataBySubjectAndTrialSequence(subjectName, trialSequence);

            // Set up the data source.
            this.bsoRawdata.DataSource = table;

            // Setting a new binding source for the data grid view
            // leads to deletion of formattings,
            // so set data grid view column styles again.
            if (this.dgvRawData.Columns.Count > 0)
            {
              this.dgvRawData.Columns["ID"].ReadOnly = true;
              this.dgvRawData.Columns["SubjectName"].ReadOnly = true;
              this.dgvRawData.Columns["TrialSequence"].ReadOnly = true;
              this.dgvRawData.Columns["ID"].DefaultCellStyle = ReadOnlyCellStyle;
              this.dgvRawData.Columns["SubjectName"].DefaultCellStyle = ReadOnlyCellStyle;
              this.dgvRawData.Columns["TrialSequence"].DefaultCellStyle = ReadOnlyCellStyle;

              DataGridViewCellStyle singleCellStyle = new DataGridViewCellStyle();
              singleCellStyle = this.dgvRawData.DefaultCellStyle.Clone();
              singleCellStyle.Format = "N6";
              this.dgvRawData.Columns["PupilDiaX"].DefaultCellStyle = singleCellStyle;
              this.dgvRawData.Columns["PupilDiaY"].DefaultCellStyle = singleCellStyle;
              this.dgvRawData.Columns["GazePosX"].DefaultCellStyle = singleCellStyle;
              this.dgvRawData.Columns["GazePosY"].DefaultCellStyle = singleCellStyle;
            }
          }
        }
        catch (Exception ex)
        {
          ExceptionMethods.HandleException(ex);
        }
        finally
        {
          this.Cursor = Cursors.Default;
        }
      }
    }

    #endregion //METHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER

    /// <summary>
    /// This method sets only the columns with names listed in the second parameter 
    /// of the given <see cref="DataGridView"/> to read only style.
    /// </summary>
    /// <param name="dataGridView">The <see cref="DataGridView"/>
    /// for which the columns should be marked to be read-only.</param>
    /// <param name="columnNames">An array of <see cref="string"/>
    /// with the column names to be set read-only.</param>
    private void SetDataGridViewColumnsToReadOnlyStyle(DataGridView dataGridView, string[] columnNames)
    {
      if (dataGridView.Columns.Count > 0)
      {
        foreach (string columnName in columnNames)
        {
          dataGridView.Columns[columnName].ReadOnly = true;
          dataGridView.Columns[columnName].DefaultCellStyle = ReadOnlyCellStyle;
        }
      }
    }

    /// <summary>
    /// This method is temporarily used for updating old experiments to
    /// Ogama 1.0.0 format.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnSpecial_Click(object sender, EventArgs e)
    {
      //string query = "SELECT Trials.Category, ROUND(Avg(CAST(GazeFixations.Length AS Float)),2) FROM Trials,Subjects,GazeFixations WHERE Trials.SubjectName = Subjects.SubjectName AND GazeFixations.TrialID = Trials.TrialID AND GazeFixations.TrialSequence = Trials.TrialSequence AND GazeFixations.SubjectName = Trials.SubjectName AND Subjects.Category = 'Developer' GROUP BY Trials.Category";
      //Queries.ExecuteSQLCommand(query);
      //Slideshow slideshow = Document.ActiveDocument.ExperimentSettings.SlideShow;
      //Dictionary<string, int> name2idAssignment = new Dictionary<string, int>();
      //foreach (Trial trial in slideshow.Trials)
      //{
      //  name2idAssignment.Add(trial.Name, trial.ID);
      //}

      //bool doNotUpdate = false;
      //DataTable trials = Document.ActiveDocument.DocDataSet.Trials;
      //List<string> missingTrials = new List<string>();

      //foreach (DataRow trialsRow in trials.Rows)
      //{
      //  string trialName = Path.GetFileNameWithoutExtension(trialsRow["TrialName"].ToString());
      //  if (name2idAssignment.ContainsKey(trialName))
      //  {
      //    trialsRow["TrialID"] = name2idAssignment[trialName];
      //  }
      //  else
      //  {
      //    if (!missingTrials.Contains(trialName))
      //    {
      //      missingTrials.Add(trialName);
      //    }

      //    doNotUpdate = true;
      //  }
      //}

      //// Update trials
      //if (!doNotUpdate)
      //{
      //  int affectedRows = Document.ActiveDocument.DocDataSet.TrialsAdapter.Update((OgamaDataSet.TrialsDataTable)trials);
      //}
      //else
      //{
      //  Console.WriteLine("MissingTrialNames: ");
      //  foreach (string item in missingTrials)
      //  {
      //    Console.WriteLine(item);
      //  }
      //}

      ////DataTable trials = Document.ActiveDocument.DocDataSet.Trials;
      ////DataView trialView = new DataView(trials);
      ////trialView.Sort = "SubjectName ASC, TrialSequence ASC";
      ////string lastSubjectName = string.Empty;
      ////long vpnStartTime = 0;
      ////foreach (DataRowView trialsRow in trialView)
      ////{
      ////  string subjectName = trialsRow["SubjectName"].ToString();
      ////  int trialSequence = (int)trialsRow["TrialSequence"];
      ////  //if (subjectName != lastSubjectName)
      ////  //{
      ////  //  lastSubjectName = subjectName;
      ////    if (trialSequence == 0)
      ////    {
      ////      trialsRow.Delete();
      ////      //vpnStartTime = (int)trialsRow["Duration"];
      ////    }
      ////  //}
      ////  //else
      ////  //{
      ////  //  trialsRow["TrialStartTime"] = (long)trialsRow["TrialStartTime"] - vpnStartTime;
      ////  //}
      ////}

      ////int affectedRows=Document.ActiveDocument.DocDataSet.TrialsAdapter.Update((OgamaDataSet.TrialsDataTable)trialView.Table);

      ////// Reduce SearchRects
      ////using (SqlConnection connection = new SqlConnection(Document.ActiveDocument.ExperimentSettings.ConnectionString))
      ////{
      ////  connection.Open();

      ////  string queryString = "SELECT AOIs.* FROM [dbo].[AOIs]";
      ////  SqlDataAdapter adapter = new SqlDataAdapter();
      ////  adapter.SelectCommand = new SqlCommand(queryString, connection);
      ////  Ogama.DataSet.OgamaDataSet.AOIsDataTable aoiTable = new Ogama.DataSet.OgamaDataSet.AOIsDataTable();
      ////  adapter.Fill(aoiTable);

      ////  // Iterate all aoi from the aoi table
      ////  foreach (DataRow aoiRow in aoiTable.Rows)
      ////  {
      ////    string shapePts = aoiRow["ShapePts"].ToString();
      ////    if (shapePts == "P1:(0,0;0,0) P2:(1024,0;0,0) P3:(1024,0;675,0) P4:(0,0;675,0)")
      ////    {
      ////      aoiRow["ShapePts"] = "P1:(0,0;0,0) P2:(1024,0;0,0) P3:(1024,0;640,0) P4:(0,0;640,0)";
      ////    }
      ////  }

      ////  int affectedRows = Document.ActiveDocument.DocDataSet.AOIsAdapter.Update(aoiTable);
      ////}

      ////DataTable trialEvents = Document.ActiveDocument.DocDataSet.TrialEvents;
      ////DataView eventsView = new DataView(trialEvents);
      ////eventsView.Sort = "SubjectName ASC, TrialSequence ASC, EventTime ASC";
      ////string responseParam = "Time: 4000 ms";
      ////foreach (DataRowView trialEventsRow in eventsView)
      ////{
      ////  string subjectName = trialEventsRow["SubjectName"].ToString();
      ////  int trialSequence = (int)trialEventsRow["TrialSequence"];
      ////  EventType eventType = (EventType)Enum.Parse(typeof(EventType), trialEventsRow["EventType"].ToString());
      ////  InputEventTask eventTask = (InputEventTask)Enum.Parse(typeof(InputEventTask), trialEventsRow["EventTask"].ToString());
      ////  string eventParam = trialEventsRow["EventParam"].ToString();
      ////  if (eventTask == InputEventTask.Down)
      ////  {
      ////    responseParam = eventParam;
      ////  }
      ////  else if (eventTask == InputEventTask.SlideChange)
      ////  {
      ////    int affectedRows = Document.ActiveDocument.DocDataSet.TrialEventsAdapter.UpdateEventParamBySubjectNameTrialSequenceEventID(
      ////      responseParam,
      ////      subjectName,
      ////      trialSequence,
      ////      (int)trialEventsRow["EventID"]);
      ////    responseParam = "Time: 4000 ms";
      ////  }
      ////}

      ////DataTable trialEvents = Document.ActiveDocument.DocDataSet.TrialEvents;
      ////DataView eventsView = new DataView(trialEvents);
      ////eventsView.Sort = "SubjectName ASC, TrialSequence ASC, EventTime ASC";
      ////foreach (DataRowView trialEventsRow in eventsView)
      ////{
      ////  string subjectName = trialEventsRow["SubjectName"].ToString();
      ////  int trialSequence = (int)trialEventsRow["TrialSequence"];
      ////  InputEventTask eventTask = (InputEventTask)Enum.Parse(typeof(InputEventTask), trialEventsRow["EventTask"].ToString());

      ////  if (eventTask == InputEventTask.SlideChange)
      ////  {
      ////    DataTable trials = Document.ActiveDocument.DocDataSet.TrialsAdapter.GetDataBySubjectAndSequence(subjectName, trialSequence);
      ////    if (trials.Rows.Count != 1)
      ////    {
      ////      continue;
      ////    }

      ////    int trialID = (int)trials.Rows[0]["TrialID"];

      ////    StopCondition response = Queries.GetTrialResponse(subjectName, trialSequence);
      ////    if (response is TimeStopCondition)
      ////    {
      ////      continue;
      ////    }

      ////    DataTable aois = Document.ActiveDocument.DocDataSet.AOIsAdapter.GetDataByTrialIDAndGroup(trialID, "Target");

      ////    // Create aoi elements from data view
      ////    VGElementCollection aoiCollection = new VGElementCollection();

      ////    foreach (DataRow row in aois.Rows)
      ////    {
      ////      // retrieve shape parameters from cell values.
      ////      string shapeName = row["ShapeName"].ToString();
      ////      string strPtList = row["ShapePts"].ToString();
      ////      List<PointF> pointList = VectorGraphics.CustomTypeConverter.ObjectStringConverter.StringToPointFList(strPtList);
      ////      RectangleF boundingRect = RectangleF.Empty;

      ////      // Create the shape depending on ShapeType
      ////      switch (row["ShapeType"].ToString())
      ////      {
      ////        case "Rectangle":
      ////          boundingRect.Location = pointList[0];
      ////          boundingRect.Width = pointList[2].X - pointList[0].X;
      ////          boundingRect.Height = pointList[2].Y - pointList[0].Y;

      ////          // Create Rect with defined stroke
      ////          VGRectangle newRect = new VGRectangle(
      ////            ShapeDrawAction.Edge,
      ////            Pens.Red,
      ////            boundingRect);
      ////          aoiCollection.Add(newRect);
      ////          break;
      ////        case "Ellipse":
      ////          boundingRect.Location = pointList[0];
      ////          boundingRect.Width = pointList[2].X - pointList[0].X;
      ////          boundingRect.Height = pointList[2].Y - pointList[0].Y;

      ////          // Create Rect with defined stroke
      ////          VGEllipse newEllipse = new VGEllipse(
      ////            ShapeDrawAction.Edge,
      ////            Pens.Red,
      ////            boundingRect);
      ////          aoiCollection.Add(newEllipse);
      ////          break;
      ////        case "Polyline":
      ////          // Create Polyline with defined stroke
      ////          VGPolyline newPolyline = new VGPolyline(
      ////            ShapeDrawAction.Edge,
      ////            Pens.Red);
      ////          foreach (PointF point in pointList)
      ////          {
      ////            newPolyline.AddPt(point);
      ////          }

      ////          newPolyline.ClosePolyline();
      ////          aoiCollection.Add(newPolyline);
      ////          break;
      ////      }
      ////    }

      ////    bool targetHitted = false;
      ////    foreach (VGElement aoiElement in aoiCollection)
      ////    {
      ////      // Check for intersection between newPath and Clicklist or Fixationlist
      ////      PointF searchPoint = ((MouseStopCondition)response).ClickLocation;

      ////      if (aoiElement.Contains(searchPoint))
      ////      {
      ////        targetHitted = true;
      ////        break;
      ////      }
      ////    }

      ////    // if target exists (correct condition specified)
      ////    if (aoiCollection.Count == 0)
      ////    {
      ////      if (((MouseStopCondition)response).StopMouseButton == MouseButtons.Right)
      ////      {
      ////        response.IsCorrectResponse = true;
      ////      }
      ////      else
      ////      {
      ////        response.IsCorrectResponse = false;
      ////      }
      ////    }

      ////    int affectedRows = Document.ActiveDocument.DocDataSet.TrialEventsAdapter.UpdateEventParamBySubjectNameTrialSequenceEventID(
      ////      response.ToString(),
      ////      subjectName,
      ////      trialSequence,
      ////      (int)trialEventsRow["EventID"]);
      ////  }
      ////}

      ////DataTable trialEvents = Document.ActiveDocument.DocDataSet.TrialEvents;
      ////string responseParam
      ////foreach (DataRow trialEventsRow in trialEvents.Rows)
      ////{
      ////  string subjectName = trialEventsRow["SubjectName"].ToString();
      ////  int trialSequence = (int)trialEventsRow["TrialSequence"];
      ////  string eventParam = trialEventsRow["EventParam"].ToString();
      ////  if (eventParam == "Time")
      ////  {
      ////    int affectedRows = Document.ActiveDocument.DocDataSet.TrialEventsAdapter.UpdateEventParamBySubjectNameTrialSequenceEventID(
      ////      "Time: 2000 ms",
      ////      subjectName,
      ////      trialSequence,
      ////      (int)trialEventsRow["EventID"]);
      ////  }
      ////}

      ////DataTable trials = Document.ActiveDocument.DocDataSet.Trials;
      ////foreach (DataRow trialRow in trials.Rows)
      ////{
      ////  string subjectName = trialRow["SubjectName"].ToString();
      ////  int trialSequence = (int)trialRow["TrialSequence"];
      ////  int id = 0;
      ////  SortedList<long, InputEvent> trialEvents = Queries.GetTrialMouseEvents(subjectName, trialSequence);//Queries.GetTrialEvents(subjectName, trialSequence, out id);
      ////  foreach (InputEvent trialEvent in trialEvents.Values)
      ////  {
      ////    if (trialEvent.Type == EventType.Response)
      ////    {
      ////      if (trialEvent.Param.StartsWith("Time"))
      ////      {
      ////      }
      ////    }
      ////  }
      ////}

      ////DataTable subjects = Document.ActiveDocument.DocDataSet.SubjectsAdapter.GetData();
      ////foreach (DataRow subjectRow in subjects.Rows)
      ////{
      ////  string subject = subjectRow["SubjectName"].ToString();
      ////  DataTable rawData = Queries.GetRawDataBySubject(subject);
      ////  foreach (DataRow rawRow in rawData.Rows)
      ////  {
      ////    if (!rawRow.IsNull("GazePosX") || !rawRow.IsNull("GazePosY"))
      ////    {
      ////      double gazeX = (double)rawRow["GazePosX"];
      ////      double gazeY = (double)rawRow["GazePosY"];

      ////      if ((gazeX == 0 && gazeY == 0) || gazeX > 200 || gazeY > 200)
      ////      {
      ////        continue;
      ////      }

      ////      if (gazeX < 0)
      ////      {
      ////        gazeX = Math.Abs(gazeX);
      ////      }

      ////      if (gazeY < 0)
      ////      {
      ////        gazeY = Math.Abs(gazeY);
      ////      }

      ////      double blankGazeX = gazeX - Math.Floor(gazeX);
      ////      double blankGazeY = gazeY - Math.Floor(gazeY);

      ////      if (blankGazeX == 0 && blankGazeY == 0)
      ////      {
      ////        rawRow["GazePosX"] = 0;
      ////        rawRow["GazePosY"] = 0;
      ////        int updated = Queries.UpdateRowBySubjectAndID(rawRow, subject, Convert.ToInt32(rawRow["ID"]));
      ////      }
      ////    }
      ////  }
      ////}

      ////DataTable subjects = Document.ActiveDocument.DocDataSet.SubjectsAdapter.GetData();
      ////foreach (DataRow subjectRow in subjects.Rows)
      ////{
      ////  string subject = subjectRow["SubjectName"].ToString();
      ////  DataTable rawData = Queries.GetRawDataBySubject(subject);
      ////  foreach (DataRow rawRow in rawData.Rows)
      ////  {
      ////    if (!rawRow.IsNull("EventID"))
      ////    {
      ////      double mouseX = (double)rawRow["MousePosX"];
      ////      double mouseY = (double)rawRow["MousePosY"];
      ////      Point mouseLocation = new Point((int)mouseX, (int)mouseY);
      ////      int sequence = (int)rawRow["TrialSequence"];
      ////      int eventID = (int)rawRow["EventID"];

      ////      DataTable trials = Document.ActiveDocument.DocDataSet.TrialsAdapter.GetDataBySubjectAndSequence(subject, sequence);
      ////      if (trials.Rows.Count == 1)
      ////      {
      ////        int trialID = (int)trials.Rows[0]["TrialID"];
      ////        Trial trial = Document.ActiveDocument.ExperimentSettings.SlideShow.GetTrialByID(trialID);
      ////        if (trial.Count != 1)
      ////        {
      ////          throw new ArgumentException();
      ////        }

      ////        bool? correct = null;
      ////        string targetName = string.Empty;

      ////        foreach (VGElement target in trial[0].TargetShapes)
      ////        {
      ////          if (target.Contains(mouseLocation))
      ////          {
      ////            targetName = target.Name;
      ////            correct = true;
      ////            break;
      ////          }

      ////          correct = false;
      ////        }

      ////        MouseStopCondition msc = new MouseStopCondition(MouseButtons.Left, false, targetName, null, mouseLocation);
      ////        Document.ActiveDocument.DocDataSet.TrialEventsAdapter.UpdateEventParamBySubjectNameTrialSequenceEventID(msc.ToString(), subject, sequence, eventID);

      ////        DataTable trialEvents = Document.ActiveDocument.DocDataSet.TrialEventsAdapter.GetDataBySubjectNameTrialSequenceButOnlySlideChangeResponses(subject,sequence);
      ////        if (trialEvents.Rows.Count == 1)
      ////        {
      ////          msc.IsCorrectResponse = correct;
      ////          int responseEventID=(int)trialEvents.Rows[0]["EventID"];
      ////          Document.ActiveDocument.DocDataSet.TrialEventsAdapter.UpdateEventParamBySubjectNameTrialSequenceEventID(msc.ToString(), subject, sequence, responseEventID);
      ////        }
      ////        else
      ////        {
      ////          throw new ArgumentException();
      ////        }
      ////      }
      ////      else
      ////      {
      ////        throw new ArgumentException();
      ////      }
      ////    }
      ////  }
      ////}

      ////DataTable trialEvents = Document.ActiveDocument.DocDataSet.TrialEvents;
      ////foreach (DataRow trialEventRow in trialEvents.Rows)
      ////{
      ////  string param = trialEventRow["EventParam"].ToString();
      ////  if (param.StartsWith("Left"))
      ////  {
      ////    trialEventRow["EventParam"] = "Mouse: " + param;
      ////  }
      ////  else if (param == "Mouse: Left")
      ////  {
      ////    trialEventRow["EventParam"] = param + " (512,184)";
      ////  }
      ////}

      ////int affectedRows = Document.ActiveDocument.DocDataSet.TrialEventsAdapter.Update(Document.ActiveDocument.DocDataSet.TrialEvents);

      //// DataTable subjects = Document.ActiveDocument.DocDataSet.TadSubjects.GetData();
      ////    foreach (DataRow subjectRow in subjects.Rows)
      ////    {
      ////      string subject = subjectRow["SubjectName"].ToString();
      ////      DataTable trials = Document.ActiveDocument.DocDataSet.TadTrials.GetDataBySubject(subject);
      ////      foreach (DataRow row in trials.Rows)
      ////      {
      ////        int trialID = (int)row["TrialID"];
      ////        int trialSequence = (int)row["tTialSequence"];
      ////        DataTable rawData = Queries.GetRawDataBySubjectAndTrialSequence(subject, trialSequence);
      ////        Int64 trialStartTime = (Int64)row["TrialStartTime"];
      ////        if (row["ResponseCorrectness"].ToString() == "0")
      ////        {
      ////          row["ResponseCorrectness"] = null;
      ////        }
      ////        else if (row["ResponseCorrectness"].ToString() == "1")
      ////        {
      ////          row["ResponseCorrectness"] = "right";
      ////        }
      ////        else if (row["ResponseCorrectness"].ToString() == "2")
      ////        {
      ////          row["ResponseCorrectness"] = "wrong";
      ////        }

      ////        if (row["StimulusFile"].ToString().Contains("_menu"))
      ////        {
      ////          row["SubjectsResponse"] = "Mousebuttons: Left";
      ////        }
      ////        else if (row["StimulusFile"].ToString().Contains("On.bmp"))
      ////        {
      ////          row["SubjectsResponse"] = "Time: 2000 ms";
      ////        }
      ////        else if (row["StimulusFile"].ToString().Contains("Off.bmp"))
      ////        {
      ////          row["SubjectsResponse"] = "Mousebuttons: Left";
      ////        }
      ////        int updated=Document.ActiveDocument.DocDataSet.TadTrials.Update(row);

      ////        foreach (DataRow rawRow in rawData.Rows)
      ////        {
      ////          if (rawRow["Response"].ToString()=="None")
      ////          {
      ////            rawRow["Response"] = null;
      ////            updated = Queries.UpdateRowBySubjectAndID(rawRow,
      //// rawRow["SubjectName"].ToString(), Convert.ToInt32(rawRow["ID"]));

      ////          }
      ////          else if (rawRow["Response"].ToString() == "Left")
      ////          {
      ////            rawRow["Response"] = "Mousebuttons: Left";
      ////            updated = Queries.UpdateRowBySubjectAndID(rawRow,
      //// rawRow["SubjectName"].ToString(), Convert.ToInt32(rawRow["ID"]));

      ////          }
      ////          else if (rawRow["Response"].ToString() == "Right")
      ////          {
      ////            rawRow["Response"] = "Mousebuttons: Right"; 
      ////            updated = Queries.UpdateRowBySubjectAndID(rawRow,
      //// rawRow["SubjectName"].ToString(), Convert.ToInt32(rawRow["ID"]));

      ////          }
      ////        }
      ////      }
      ////    }
    }

    #endregion //HELPER
  }
}