using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Ogama.Modules.Database
{
  partial class DatabaseModule
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      CustomDispose();
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DatabaseModule));
      this.dgvSubjects = new System.Windows.Forms.DataGridView();
      this.colSubjectsID = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colSubjectsSubjectName = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colSubjectsCategory = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colSubjectsAge = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colSubjectsSex = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colSubjectsHandedness = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colSubjectsComments = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.cmsDataGridView = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.cmsSelect = new System.Windows.Forms.ToolStripMenuItem();
      this.cmsCopy = new System.Windows.Forms.ToolStripMenuItem();
      this.dgvRawData = new System.Windows.Forms.DataGridView();
      this.dgvTrials = new System.Windows.Forms.DataGridView();
      this.colTrialsID = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colTrialsSubjectName = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colTrialsTrialSequence = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colTrialsTrialID = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colTrialsTrialName = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colTrialsCategory = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colTrialsTrialStartTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colTrialsDuration = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colTrialsEliminateData = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
      this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
      this.tacTables = new System.Windows.Forms.TabControl();
      this.tbpSubjects = new System.Windows.Forms.TabPage();
      this.tbpSubjectParameters = new System.Windows.Forms.TabPage();
      this.dgvSubjectParameters = new System.Windows.Forms.DataGridView();
      this.colSubjectParametersID = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colSubjectParametersSubjectName = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colSubjectParametersParam = new System.Windows.Forms.DataGridViewComboBoxColumn();
      this.colSubjectParametersSubjectNameParamValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.tbpParams = new System.Windows.Forms.TabPage();
      this.dgvParams = new System.Windows.Forms.DataGridView();
      this.colParamsID = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.paramDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.descriptionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.tbpTrials = new System.Windows.Forms.TabPage();
      this.tbpTrialEvents = new System.Windows.Forms.TabPage();
      this.dgvTrialEvents = new System.Windows.Forms.DataGridView();
      this.colTrialEventsID = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colTrialEventsSubjectName = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colTrialEventsTrialSequence = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colTrialEventsEventID = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colTrialEventsEventTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colTrialEventsEventType = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colTrialEventsEventTask = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colTrialEventsEventParam = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.tbpRawdata = new System.Windows.Forms.TabPage();
      this.tbpGazeFixations = new System.Windows.Forms.TabPage();
      this.dgvGazeFixations = new System.Windows.Forms.DataGridView();
      this.colGazeFixationsID = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colGazeFixationsSubjectName = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colGazeFixationsTrialID = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colGazeFixationsTrialSequence = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colGazeFixationsCountInTrial = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colGazeFixationsStartTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colGazeFixationsLength = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colGazeFixationsPosX = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colGazeFixationsPosY = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.tbpMouseFixations = new System.Windows.Forms.TabPage();
      this.dgvMouseFixations = new System.Windows.Forms.DataGridView();
      this.colMouseFixationsID = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colMouseFixationsSubjectName = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colMouseFixationsTrialID = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colMouseFixationsTrialSequence = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colMouseFixationsCountInTrial = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colMouseFixationsStartTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colMouseFixationsLength = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colMouseFixationsPosX = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colMouseFixationsPosY = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.tbpAOI = new System.Windows.Forms.TabPage();
      this.dgvAOIs = new System.Windows.Forms.DataGridView();
      this.colAOIsID = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colAOIsTrialID = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colAOIsSlideNr = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colAOIsShapeName = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colAOIsShapeType = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colAOIsShapeNumPts = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colAOIsShapePts = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colAOIsShapeGroup = new System.Windows.Forms.DataGridViewComboBoxColumn();
      this.tbpShapeGroups = new System.Windows.Forms.TabPage();
      this.dgvShapeGroups = new System.Windows.Forms.DataGridView();
      this.colShapeGroupsID = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colShapeGroupsShapeGroup = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.imlTabs = new System.Windows.Forms.ImageList(this.components);
      this.toolStrip1 = new System.Windows.Forms.ToolStrip();
      this.btnImport = new System.Windows.Forms.ToolStripButton();
      this.btnImportOgamaFormat = new System.Windows.Forms.ToolStripButton();
      this.btnExport = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.btnSave = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
      this.btnFilterData = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
      this.btnHelp = new System.Windows.Forms.ToolStripButton();
      this.btnSpecial = new System.Windows.Forms.ToolStripButton();
      this.sfdExport = new System.Windows.Forms.SaveFileDialog();
      this.bgwExport = new System.ComponentModel.BackgroundWorker();
      ((System.ComponentModel.ISupportInitialize)(this.bsoSubjects)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.bsoFKSubjectsTrials)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.ogamaDataSet)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.bsoFKTrialsEvents)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.bsoTrialsAOIs)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.bsoTrialsGazeFixations)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.bsoTrialsMouseFixations)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.bsoShapeGroups)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.bsoFKSubjectsSubjectParameters)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.bsoSubjectParameters)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.bsoTrialEvents)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.bsoTrials)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.bsoGazeFixations)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.bsoMouseFixations)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.bsoAOIs)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.bsoParams)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.dgvSubjects)).BeginInit();
      this.cmsDataGridView.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.dgvRawData)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.dgvTrials)).BeginInit();
      this.toolStripContainer1.ContentPanel.SuspendLayout();
      this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
      this.toolStripContainer1.SuspendLayout();
      this.tacTables.SuspendLayout();
      this.tbpSubjects.SuspendLayout();
      this.tbpSubjectParameters.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.dgvSubjectParameters)).BeginInit();
      this.tbpParams.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.dgvParams)).BeginInit();
      this.tbpTrials.SuspendLayout();
      this.tbpTrialEvents.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.dgvTrialEvents)).BeginInit();
      this.tbpRawdata.SuspendLayout();
      this.tbpGazeFixations.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.dgvGazeFixations)).BeginInit();
      this.tbpMouseFixations.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.dgvMouseFixations)).BeginInit();
      this.tbpAOI.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.dgvAOIs)).BeginInit();
      this.tbpShapeGroups.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.dgvShapeGroups)).BeginInit();
      this.toolStrip1.SuspendLayout();
      this.SuspendLayout();
      // 
      // bsoSubjects
      // 
      this.bsoSubjects.AllowNew = false;
      // 
      // bsoFKTrialsEvents
      // 
      this.bsoFKTrialsEvents.Sort = "EventTime";
      // 
      // dgvSubjects
      // 
      this.dgvSubjects.AllowUserToAddRows = false;
      this.dgvSubjects.AutoGenerateColumns = false;
      this.dgvSubjects.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
      this.dgvSubjects.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colSubjectsID,
            this.colSubjectsSubjectName,
            this.colSubjectsCategory,
            this.colSubjectsAge,
            this.colSubjectsSex,
            this.colSubjectsHandedness,
            this.colSubjectsComments});
      this.dgvSubjects.ContextMenuStrip = this.cmsDataGridView;
      this.dgvSubjects.DataSource = this.bsoSubjects;
      this.dgvSubjects.Dock = System.Windows.Forms.DockStyle.Fill;
      this.dgvSubjects.Location = new System.Drawing.Point(3, 3);
      this.dgvSubjects.Name = "dgvSubjects";
      this.dgvSubjects.Size = new System.Drawing.Size(650, 291);
      this.dgvSubjects.TabIndex = 0;
      this.toolTip1.SetToolTip(this.dgvSubjects, "Select a row to filter the data in the preceding tables.\r\nRight click for a conte" +
        "xt menu.");
      this.dgvSubjects.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgvSubjects_DataError);
      this.dgvSubjects.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSubjects_RowEnter);
      this.dgvSubjects.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.dgvSubjects_UserDeletingRow);
      // 
      // colSubjectsID
      // 
      this.colSubjectsID.DataPropertyName = "ID";
      this.colSubjectsID.HeaderText = "ID";
      this.colSubjectsID.MinimumWidth = 30;
      this.colSubjectsID.Name = "colSubjectsID";
      this.colSubjectsID.ReadOnly = true;
      this.colSubjectsID.Width = 60;
      // 
      // colSubjectsSubjectName
      // 
      this.colSubjectsSubjectName.DataPropertyName = "SubjectName";
      this.colSubjectsSubjectName.HeaderText = "SubjectName";
      this.colSubjectsSubjectName.MinimumWidth = 30;
      this.colSubjectsSubjectName.Name = "colSubjectsSubjectName";
      this.colSubjectsSubjectName.Width = 80;
      // 
      // colSubjectsCategory
      // 
      this.colSubjectsCategory.DataPropertyName = "Category";
      this.colSubjectsCategory.HeaderText = "Category";
      this.colSubjectsCategory.MinimumWidth = 30;
      this.colSubjectsCategory.Name = "colSubjectsCategory";
      this.colSubjectsCategory.Width = 60;
      // 
      // colSubjectsAge
      // 
      this.colSubjectsAge.DataPropertyName = "Age";
      this.colSubjectsAge.HeaderText = "Age";
      this.colSubjectsAge.MinimumWidth = 30;
      this.colSubjectsAge.Name = "colSubjectsAge";
      this.colSubjectsAge.Width = 40;
      // 
      // colSubjectsSex
      // 
      this.colSubjectsSex.DataPropertyName = "Sex";
      this.colSubjectsSex.HeaderText = "Sex";
      this.colSubjectsSex.MinimumWidth = 30;
      this.colSubjectsSex.Name = "colSubjectsSex";
      this.colSubjectsSex.Width = 40;
      // 
      // colSubjectsHandedness
      // 
      this.colSubjectsHandedness.DataPropertyName = "Handedness";
      this.colSubjectsHandedness.HeaderText = "Handedness";
      this.colSubjectsHandedness.MinimumWidth = 30;
      this.colSubjectsHandedness.Name = "colSubjectsHandedness";
      this.colSubjectsHandedness.Width = 80;
      // 
      // colSubjectsComments
      // 
      this.colSubjectsComments.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
      this.colSubjectsComments.DataPropertyName = "Comments";
      this.colSubjectsComments.HeaderText = "Comments";
      this.colSubjectsComments.MinimumWidth = 30;
      this.colSubjectsComments.Name = "colSubjectsComments";
      // 
      // cmsDataGridView
      // 
      this.cmsDataGridView.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmsSelect,
            this.cmsCopy});
      this.cmsDataGridView.Name = "cmsDataGridView";
      this.cmsDataGridView.Size = new System.Drawing.Size(219, 48);
      // 
      // cmsSelect
      // 
      this.cmsSelect.Name = "cmsSelect";
      this.cmsSelect.Size = new System.Drawing.Size(218, 22);
      this.cmsSelect.Text = "Select all rows";
      this.cmsSelect.Click += new System.EventHandler(this.cmsSelect_Click);
      // 
      // cmsCopy
      // 
      this.cmsCopy.Image = global::Ogama.Properties.Resources.CopyHS;
      this.cmsCopy.Name = "cmsCopy";
      this.cmsCopy.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
      this.cmsCopy.Size = new System.Drawing.Size(218, 22);
      this.cmsCopy.Text = "Copy selected rows";
      this.cmsCopy.Click += new System.EventHandler(this.cmsCopy_Click);
      // 
      // dgvRawData
      // 
      this.dgvRawData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
      this.dgvRawData.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
      this.dgvRawData.ContextMenuStrip = this.cmsDataGridView;
      this.dgvRawData.Dock = System.Windows.Forms.DockStyle.Fill;
      this.dgvRawData.Location = new System.Drawing.Point(3, 3);
      this.dgvRawData.Name = "dgvRawData";
      this.dgvRawData.Size = new System.Drawing.Size(650, 291);
      this.dgvRawData.TabIndex = 2;
      this.toolTip1.SetToolTip(this.dgvRawData, "This table displays only the data of the selected trial or subject depending on f" +
        "ilter mode.\r\nRight click for a context menu.");
      this.dgvRawData.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridView_DataError);
      this.dgvRawData.Validated += new System.EventHandler(this.dgvRawData_Validated);
      // 
      // dgvTrials
      // 
      this.dgvTrials.AllowUserToAddRows = false;
      this.dgvTrials.AutoGenerateColumns = false;
      this.dgvTrials.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
      this.dgvTrials.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colTrialsID,
            this.colTrialsSubjectName,
            this.colTrialsTrialSequence,
            this.colTrialsTrialID,
            this.colTrialsTrialName,
            this.colTrialsCategory,
            this.colTrialsTrialStartTime,
            this.colTrialsDuration,
            this.colTrialsEliminateData});
      this.dgvTrials.ContextMenuStrip = this.cmsDataGridView;
      this.dgvTrials.DataSource = this.bsoFKSubjectsTrials;
      this.dgvTrials.Dock = System.Windows.Forms.DockStyle.Fill;
      this.dgvTrials.Location = new System.Drawing.Point(3, 3);
      this.dgvTrials.Name = "dgvTrials";
      this.dgvTrials.Size = new System.Drawing.Size(650, 291);
      this.dgvTrials.TabIndex = 3;
      this.toolTip1.SetToolTip(this.dgvTrials, "Select trial row to filter the data of the preceding tables.\r\nRight click for a c" +
        "ontext menu.");
      this.dgvTrials.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridView_DataError);
      this.dgvTrials.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTrials_RowEnter);
      // 
      // colTrialsID
      // 
      this.colTrialsID.DataPropertyName = "ID";
      this.colTrialsID.HeaderText = "ID";
      this.colTrialsID.MinimumWidth = 30;
      this.colTrialsID.Name = "colTrialsID";
      this.colTrialsID.ReadOnly = true;
      this.colTrialsID.Width = 60;
      // 
      // colTrialsSubjectName
      // 
      this.colTrialsSubjectName.DataPropertyName = "SubjectName";
      this.colTrialsSubjectName.HeaderText = "Subjectname";
      this.colTrialsSubjectName.MinimumWidth = 30;
      this.colTrialsSubjectName.Name = "colTrialsSubjectName";
      this.colTrialsSubjectName.Width = 80;
      // 
      // colTrialsTrialSequence
      // 
      this.colTrialsTrialSequence.DataPropertyName = "TrialSequence";
      this.colTrialsTrialSequence.HeaderText = "Sequence";
      this.colTrialsTrialSequence.MinimumWidth = 30;
      this.colTrialsTrialSequence.Name = "colTrialsTrialSequence";
      this.colTrialsTrialSequence.Width = 80;
      // 
      // colTrialsTrialID
      // 
      this.colTrialsTrialID.DataPropertyName = "TrialID";
      this.colTrialsTrialID.HeaderText = "TrialID";
      this.colTrialsTrialID.MinimumWidth = 30;
      this.colTrialsTrialID.Name = "colTrialsTrialID";
      this.colTrialsTrialID.Width = 40;
      // 
      // colTrialsTrialName
      // 
      this.colTrialsTrialName.DataPropertyName = "TrialName";
      this.colTrialsTrialName.HeaderText = "Name";
      this.colTrialsTrialName.MinimumWidth = 30;
      this.colTrialsTrialName.Name = "colTrialsTrialName";
      this.colTrialsTrialName.Width = 80;
      // 
      // colTrialsCategory
      // 
      this.colTrialsCategory.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
      this.colTrialsCategory.DataPropertyName = "Category";
      this.colTrialsCategory.HeaderText = "Category";
      this.colTrialsCategory.MinimumWidth = 30;
      this.colTrialsCategory.Name = "colTrialsCategory";
      // 
      // colTrialsTrialStartTime
      // 
      this.colTrialsTrialStartTime.DataPropertyName = "TrialStartTime";
      this.colTrialsTrialStartTime.HeaderText = "StartTime";
      this.colTrialsTrialStartTime.MinimumWidth = 30;
      this.colTrialsTrialStartTime.Name = "colTrialsTrialStartTime";
      this.colTrialsTrialStartTime.Width = 60;
      // 
      // colTrialsDuration
      // 
      this.colTrialsDuration.DataPropertyName = "Duration";
      this.colTrialsDuration.HeaderText = "Duration";
      this.colTrialsDuration.MinimumWidth = 30;
      this.colTrialsDuration.Name = "colTrialsDuration";
      this.colTrialsDuration.Width = 60;
      // 
      // colTrialsEliminateData
      // 
      this.colTrialsEliminateData.DataPropertyName = "EliminateData";
      this.colTrialsEliminateData.HeaderText = "Eliminate";
      this.colTrialsEliminateData.MinimumWidth = 30;
      this.colTrialsEliminateData.Name = "colTrialsEliminateData";
      this.colTrialsEliminateData.Width = 60;
      // 
      // toolStripContainer1
      // 
      // 
      // toolStripContainer1.ContentPanel
      // 
      this.toolStripContainer1.ContentPanel.Controls.Add(this.tacTables);
      this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(664, 324);
      this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
      this.toolStripContainer1.Name = "toolStripContainer1";
      this.toolStripContainer1.Size = new System.Drawing.Size(664, 349);
      this.toolStripContainer1.TabIndex = 3;
      this.toolStripContainer1.Text = "toolStripContainer1";
      // 
      // toolStripContainer1.TopToolStripPanel
      // 
      this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip1);
      // 
      // tacTables
      // 
      this.tacTables.Controls.Add(this.tbpSubjects);
      this.tacTables.Controls.Add(this.tbpSubjectParameters);
      this.tacTables.Controls.Add(this.tbpParams);
      this.tacTables.Controls.Add(this.tbpTrials);
      this.tacTables.Controls.Add(this.tbpTrialEvents);
      this.tacTables.Controls.Add(this.tbpRawdata);
      this.tacTables.Controls.Add(this.tbpGazeFixations);
      this.tacTables.Controls.Add(this.tbpMouseFixations);
      this.tacTables.Controls.Add(this.tbpAOI);
      this.tacTables.Controls.Add(this.tbpShapeGroups);
      this.tacTables.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tacTables.ImageList = this.imlTabs;
      this.tacTables.Location = new System.Drawing.Point(0, 0);
      this.tacTables.Name = "tacTables";
      this.tacTables.SelectedIndex = 0;
      this.tacTables.Size = new System.Drawing.Size(664, 324);
      this.tacTables.TabIndex = 1;
      // 
      // tbpSubjects
      // 
      this.tbpSubjects.Controls.Add(this.dgvSubjects);
      this.tbpSubjects.ImageKey = "Subjects";
      this.tbpSubjects.Location = new System.Drawing.Point(4, 23);
      this.tbpSubjects.Name = "tbpSubjects";
      this.tbpSubjects.Padding = new System.Windows.Forms.Padding(3);
      this.tbpSubjects.Size = new System.Drawing.Size(656, 297);
      this.tbpSubjects.TabIndex = 0;
      this.tbpSubjects.Text = "Subjects";
      this.tbpSubjects.UseVisualStyleBackColor = true;
      // 
      // tbpSubjectParameters
      // 
      this.tbpSubjectParameters.Controls.Add(this.dgvSubjectParameters);
      this.tbpSubjectParameters.ImageKey = "SubjectProperties";
      this.tbpSubjectParameters.Location = new System.Drawing.Point(4, 23);
      this.tbpSubjectParameters.Name = "tbpSubjectParameters";
      this.tbpSubjectParameters.Padding = new System.Windows.Forms.Padding(3);
      this.tbpSubjectParameters.Size = new System.Drawing.Size(656, 297);
      this.tbpSubjectParameters.TabIndex = 8;
      this.tbpSubjectParameters.Text = "Subject parameters";
      this.tbpSubjectParameters.UseVisualStyleBackColor = true;
      // 
      // dgvSubjectParameters
      // 
      this.dgvSubjectParameters.AutoGenerateColumns = false;
      this.dgvSubjectParameters.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dgvSubjectParameters.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colSubjectParametersID,
            this.colSubjectParametersSubjectName,
            this.colSubjectParametersParam,
            this.colSubjectParametersSubjectNameParamValue});
      this.dgvSubjectParameters.DataSource = this.bsoFKSubjectsSubjectParameters;
      this.dgvSubjectParameters.Dock = System.Windows.Forms.DockStyle.Fill;
      this.dgvSubjectParameters.Location = new System.Drawing.Point(3, 3);
      this.dgvSubjectParameters.Name = "dgvSubjectParameters";
      this.dgvSubjectParameters.Size = new System.Drawing.Size(650, 291);
      this.dgvSubjectParameters.TabIndex = 0;
      // 
      // colSubjectParametersID
      // 
      this.colSubjectParametersID.DataPropertyName = "ID";
      this.colSubjectParametersID.HeaderText = "ID";
      this.colSubjectParametersID.MinimumWidth = 30;
      this.colSubjectParametersID.Name = "colSubjectParametersID";
      this.colSubjectParametersID.ReadOnly = true;
      this.colSubjectParametersID.Width = 60;
      // 
      // colSubjectParametersSubjectName
      // 
      this.colSubjectParametersSubjectName.DataPropertyName = "SubjectName";
      this.colSubjectParametersSubjectName.HeaderText = "SubjectName";
      this.colSubjectParametersSubjectName.MinimumWidth = 80;
      this.colSubjectParametersSubjectName.Name = "colSubjectParametersSubjectName";
      // 
      // colSubjectParametersParam
      // 
      this.colSubjectParametersParam.DataPropertyName = "Param";
      this.colSubjectParametersParam.DataSource = this.bsoParams;
      this.colSubjectParametersParam.DisplayMember = "Param";
      this.colSubjectParametersParam.HeaderText = "Param";
      this.colSubjectParametersParam.MinimumWidth = 80;
      this.colSubjectParametersParam.Name = "colSubjectParametersParam";
      this.colSubjectParametersParam.Resizable = System.Windows.Forms.DataGridViewTriState.True;
      this.colSubjectParametersParam.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
      this.colSubjectParametersParam.ValueMember = "Param";
      // 
      // colSubjectParametersSubjectNameParamValue
      // 
      this.colSubjectParametersSubjectNameParamValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
      this.colSubjectParametersSubjectNameParamValue.DataPropertyName = "ParamValue";
      this.colSubjectParametersSubjectNameParamValue.HeaderText = "ParamValue";
      this.colSubjectParametersSubjectNameParamValue.MinimumWidth = 60;
      this.colSubjectParametersSubjectNameParamValue.Name = "colSubjectParametersSubjectNameParamValue";
      // 
      // tbpParams
      // 
      this.tbpParams.Controls.Add(this.dgvParams);
      this.tbpParams.ImageKey = "Params";
      this.tbpParams.Location = new System.Drawing.Point(4, 23);
      this.tbpParams.Name = "tbpParams";
      this.tbpParams.Padding = new System.Windows.Forms.Padding(3);
      this.tbpParams.Size = new System.Drawing.Size(656, 297);
      this.tbpParams.TabIndex = 9;
      this.tbpParams.Text = "CustomParams";
      this.tbpParams.UseVisualStyleBackColor = true;
      // 
      // dgvParams
      // 
      this.dgvParams.AutoGenerateColumns = false;
      this.dgvParams.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dgvParams.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colParamsID,
            this.paramDataGridViewTextBoxColumn,
            this.descriptionDataGridViewTextBoxColumn});
      this.dgvParams.DataSource = this.bsoParams;
      this.dgvParams.Dock = System.Windows.Forms.DockStyle.Fill;
      this.dgvParams.Location = new System.Drawing.Point(3, 3);
      this.dgvParams.Name = "dgvParams";
      this.dgvParams.Size = new System.Drawing.Size(650, 291);
      this.dgvParams.TabIndex = 0;
      // 
      // colParamsID
      // 
      this.colParamsID.DataPropertyName = "ID";
      this.colParamsID.HeaderText = "ID";
      this.colParamsID.Name = "colParamsID";
      this.colParamsID.ReadOnly = true;
      // 
      // paramDataGridViewTextBoxColumn
      // 
      this.paramDataGridViewTextBoxColumn.DataPropertyName = "Param";
      this.paramDataGridViewTextBoxColumn.HeaderText = "Param";
      this.paramDataGridViewTextBoxColumn.Name = "paramDataGridViewTextBoxColumn";
      // 
      // descriptionDataGridViewTextBoxColumn
      // 
      this.descriptionDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
      this.descriptionDataGridViewTextBoxColumn.DataPropertyName = "Description";
      this.descriptionDataGridViewTextBoxColumn.HeaderText = "Description";
      this.descriptionDataGridViewTextBoxColumn.Name = "descriptionDataGridViewTextBoxColumn";
      // 
      // tbpTrials
      // 
      this.tbpTrials.Controls.Add(this.dgvTrials);
      this.tbpTrials.ImageKey = "Trials";
      this.tbpTrials.Location = new System.Drawing.Point(4, 23);
      this.tbpTrials.Name = "tbpTrials";
      this.tbpTrials.Padding = new System.Windows.Forms.Padding(3);
      this.tbpTrials.Size = new System.Drawing.Size(656, 297);
      this.tbpTrials.TabIndex = 1;
      this.tbpTrials.Text = "Trials";
      this.tbpTrials.UseVisualStyleBackColor = true;
      // 
      // tbpTrialEvents
      // 
      this.tbpTrialEvents.Controls.Add(this.dgvTrialEvents);
      this.tbpTrialEvents.ImageKey = "Events";
      this.tbpTrialEvents.Location = new System.Drawing.Point(4, 23);
      this.tbpTrialEvents.Name = "tbpTrialEvents";
      this.tbpTrialEvents.Padding = new System.Windows.Forms.Padding(3);
      this.tbpTrialEvents.Size = new System.Drawing.Size(656, 297);
      this.tbpTrialEvents.TabIndex = 5;
      this.tbpTrialEvents.Text = "Trial Events";
      this.tbpTrialEvents.UseVisualStyleBackColor = true;
      // 
      // dgvTrialEvents
      // 
      this.dgvTrialEvents.AllowUserToAddRows = false;
      this.dgvTrialEvents.AutoGenerateColumns = false;
      this.dgvTrialEvents.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
      this.dgvTrialEvents.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colTrialEventsID,
            this.colTrialEventsSubjectName,
            this.colTrialEventsTrialSequence,
            this.colTrialEventsEventID,
            this.colTrialEventsEventTime,
            this.colTrialEventsEventType,
            this.colTrialEventsEventTask,
            this.colTrialEventsEventParam});
      this.dgvTrialEvents.ContextMenuStrip = this.cmsDataGridView;
      this.dgvTrialEvents.DataSource = this.bsoFKTrialsEvents;
      this.dgvTrialEvents.Dock = System.Windows.Forms.DockStyle.Fill;
      this.dgvTrialEvents.Location = new System.Drawing.Point(3, 3);
      this.dgvTrialEvents.Name = "dgvTrialEvents";
      this.dgvTrialEvents.Size = new System.Drawing.Size(650, 291);
      this.dgvTrialEvents.TabIndex = 0;
      this.dgvTrialEvents.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridView_DataError);
      // 
      // colTrialEventsID
      // 
      this.colTrialEventsID.DataPropertyName = "ID";
      this.colTrialEventsID.HeaderText = "ID";
      this.colTrialEventsID.MinimumWidth = 30;
      this.colTrialEventsID.Name = "colTrialEventsID";
      this.colTrialEventsID.ReadOnly = true;
      this.colTrialEventsID.Width = 60;
      // 
      // colTrialEventsSubjectName
      // 
      this.colTrialEventsSubjectName.DataPropertyName = "SubjectName";
      this.colTrialEventsSubjectName.HeaderText = "SubjectName";
      this.colTrialEventsSubjectName.MinimumWidth = 30;
      this.colTrialEventsSubjectName.Name = "colTrialEventsSubjectName";
      this.colTrialEventsSubjectName.Width = 80;
      // 
      // colTrialEventsTrialSequence
      // 
      this.colTrialEventsTrialSequence.DataPropertyName = "TrialSequence";
      this.colTrialEventsTrialSequence.HeaderText = "TrialSequence";
      this.colTrialEventsTrialSequence.MinimumWidth = 30;
      this.colTrialEventsTrialSequence.Name = "colTrialEventsTrialSequence";
      this.colTrialEventsTrialSequence.Width = 80;
      // 
      // colTrialEventsEventID
      // 
      this.colTrialEventsEventID.DataPropertyName = "EventID";
      this.colTrialEventsEventID.HeaderText = "EventID";
      this.colTrialEventsEventID.MinimumWidth = 30;
      this.colTrialEventsEventID.Name = "colTrialEventsEventID";
      this.colTrialEventsEventID.Width = 60;
      // 
      // colTrialEventsEventTime
      // 
      this.colTrialEventsEventTime.DataPropertyName = "EventTime";
      this.colTrialEventsEventTime.HeaderText = "EventTime";
      this.colTrialEventsEventTime.MinimumWidth = 30;
      this.colTrialEventsEventTime.Name = "colTrialEventsEventTime";
      this.colTrialEventsEventTime.Width = 60;
      // 
      // colTrialEventsEventType
      // 
      this.colTrialEventsEventType.DataPropertyName = "EventType";
      this.colTrialEventsEventType.HeaderText = "EventType";
      this.colTrialEventsEventType.MinimumWidth = 30;
      this.colTrialEventsEventType.Name = "colTrialEventsEventType";
      this.colTrialEventsEventType.Width = 80;
      // 
      // colTrialEventsEventTask
      // 
      this.colTrialEventsEventTask.DataPropertyName = "EventTask";
      this.colTrialEventsEventTask.HeaderText = "EventTask";
      this.colTrialEventsEventTask.MinimumWidth = 30;
      this.colTrialEventsEventTask.Name = "colTrialEventsEventTask";
      this.colTrialEventsEventTask.Width = 80;
      // 
      // colTrialEventsEventParam
      // 
      this.colTrialEventsEventParam.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
      this.colTrialEventsEventParam.DataPropertyName = "EventParam";
      this.colTrialEventsEventParam.HeaderText = "EventParam";
      this.colTrialEventsEventParam.MinimumWidth = 30;
      this.colTrialEventsEventParam.Name = "colTrialEventsEventParam";
      // 
      // tbpRawdata
      // 
      this.tbpRawdata.Controls.Add(this.dgvRawData);
      this.tbpRawdata.ImageKey = "RawData";
      this.tbpRawdata.Location = new System.Drawing.Point(4, 23);
      this.tbpRawdata.Name = "tbpRawdata";
      this.tbpRawdata.Padding = new System.Windows.Forms.Padding(3);
      this.tbpRawdata.Size = new System.Drawing.Size(656, 297);
      this.tbpRawdata.TabIndex = 2;
      this.tbpRawdata.Text = "Raw data";
      this.tbpRawdata.UseVisualStyleBackColor = true;
      // 
      // tbpGazeFixations
      // 
      this.tbpGazeFixations.Controls.Add(this.dgvGazeFixations);
      this.tbpGazeFixations.ImageKey = "GazeFixations";
      this.tbpGazeFixations.Location = new System.Drawing.Point(4, 23);
      this.tbpGazeFixations.Name = "tbpGazeFixations";
      this.tbpGazeFixations.Padding = new System.Windows.Forms.Padding(3);
      this.tbpGazeFixations.Size = new System.Drawing.Size(656, 297);
      this.tbpGazeFixations.TabIndex = 3;
      this.tbpGazeFixations.Text = "Gaze Fixations";
      this.tbpGazeFixations.UseVisualStyleBackColor = true;
      // 
      // dgvGazeFixations
      // 
      this.dgvGazeFixations.AllowUserToAddRows = false;
      this.dgvGazeFixations.AutoGenerateColumns = false;
      this.dgvGazeFixations.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
      this.dgvGazeFixations.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colGazeFixationsID,
            this.colGazeFixationsSubjectName,
            this.colGazeFixationsTrialID,
            this.colGazeFixationsTrialSequence,
            this.colGazeFixationsCountInTrial,
            this.colGazeFixationsStartTime,
            this.colGazeFixationsLength,
            this.colGazeFixationsPosX,
            this.colGazeFixationsPosY});
      this.dgvGazeFixations.ContextMenuStrip = this.cmsDataGridView;
      this.dgvGazeFixations.DataSource = this.bsoTrialsGazeFixations;
      this.dgvGazeFixations.Dock = System.Windows.Forms.DockStyle.Fill;
      this.dgvGazeFixations.Location = new System.Drawing.Point(3, 3);
      this.dgvGazeFixations.Name = "dgvGazeFixations";
      this.dgvGazeFixations.Size = new System.Drawing.Size(650, 291);
      this.dgvGazeFixations.TabIndex = 0;
      this.dgvGazeFixations.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridView_DataError);
      // 
      // colGazeFixationsID
      // 
      this.colGazeFixationsID.DataPropertyName = "ID";
      this.colGazeFixationsID.HeaderText = "ID";
      this.colGazeFixationsID.MinimumWidth = 30;
      this.colGazeFixationsID.Name = "colGazeFixationsID";
      this.colGazeFixationsID.ReadOnly = true;
      this.colGazeFixationsID.Width = 60;
      // 
      // colGazeFixationsSubjectName
      // 
      this.colGazeFixationsSubjectName.DataPropertyName = "SubjectName";
      this.colGazeFixationsSubjectName.HeaderText = "SubjectName";
      this.colGazeFixationsSubjectName.MinimumWidth = 30;
      this.colGazeFixationsSubjectName.Name = "colGazeFixationsSubjectName";
      this.colGazeFixationsSubjectName.Width = 80;
      // 
      // colGazeFixationsTrialID
      // 
      this.colGazeFixationsTrialID.DataPropertyName = "TrialID";
      this.colGazeFixationsTrialID.HeaderText = "TrialID";
      this.colGazeFixationsTrialID.MinimumWidth = 30;
      this.colGazeFixationsTrialID.Name = "colGazeFixationsTrialID";
      this.colGazeFixationsTrialID.Width = 60;
      // 
      // colGazeFixationsTrialSequence
      // 
      this.colGazeFixationsTrialSequence.DataPropertyName = "TrialSequence";
      this.colGazeFixationsTrialSequence.HeaderText = "TrialSequence";
      this.colGazeFixationsTrialSequence.MinimumWidth = 30;
      this.colGazeFixationsTrialSequence.Name = "colGazeFixationsTrialSequence";
      this.colGazeFixationsTrialSequence.Width = 80;
      // 
      // colGazeFixationsCountInTrial
      // 
      this.colGazeFixationsCountInTrial.DataPropertyName = "CountInTrial";
      this.colGazeFixationsCountInTrial.HeaderText = "CountInTrial";
      this.colGazeFixationsCountInTrial.MinimumWidth = 30;
      this.colGazeFixationsCountInTrial.Name = "colGazeFixationsCountInTrial";
      this.colGazeFixationsCountInTrial.Width = 80;
      // 
      // colGazeFixationsStartTime
      // 
      this.colGazeFixationsStartTime.DataPropertyName = "StartTime";
      this.colGazeFixationsStartTime.HeaderText = "StartTime";
      this.colGazeFixationsStartTime.MinimumWidth = 30;
      this.colGazeFixationsStartTime.Name = "colGazeFixationsStartTime";
      this.colGazeFixationsStartTime.Width = 80;
      // 
      // colGazeFixationsLength
      // 
      this.colGazeFixationsLength.DataPropertyName = "Length";
      this.colGazeFixationsLength.HeaderText = "Length";
      this.colGazeFixationsLength.MinimumWidth = 30;
      this.colGazeFixationsLength.Name = "colGazeFixationsLength";
      this.colGazeFixationsLength.Width = 80;
      // 
      // colGazeFixationsPosX
      // 
      this.colGazeFixationsPosX.DataPropertyName = "PosX";
      this.colGazeFixationsPosX.HeaderText = "PosX";
      this.colGazeFixationsPosX.MinimumWidth = 30;
      this.colGazeFixationsPosX.Name = "colGazeFixationsPosX";
      this.colGazeFixationsPosX.Width = 60;
      // 
      // colGazeFixationsPosY
      // 
      this.colGazeFixationsPosY.DataPropertyName = "PosY";
      this.colGazeFixationsPosY.HeaderText = "PosY";
      this.colGazeFixationsPosY.MinimumWidth = 30;
      this.colGazeFixationsPosY.Name = "colGazeFixationsPosY";
      this.colGazeFixationsPosY.Width = 60;
      // 
      // tbpMouseFixations
      // 
      this.tbpMouseFixations.Controls.Add(this.dgvMouseFixations);
      this.tbpMouseFixations.ImageKey = "MouseFixations";
      this.tbpMouseFixations.Location = new System.Drawing.Point(4, 23);
      this.tbpMouseFixations.Name = "tbpMouseFixations";
      this.tbpMouseFixations.Padding = new System.Windows.Forms.Padding(3);
      this.tbpMouseFixations.Size = new System.Drawing.Size(656, 297);
      this.tbpMouseFixations.TabIndex = 7;
      this.tbpMouseFixations.Text = "Mouse Fixations";
      this.tbpMouseFixations.UseVisualStyleBackColor = true;
      // 
      // dgvMouseFixations
      // 
      this.dgvMouseFixations.AllowUserToAddRows = false;
      this.dgvMouseFixations.AutoGenerateColumns = false;
      this.dgvMouseFixations.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
      this.dgvMouseFixations.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colMouseFixationsID,
            this.colMouseFixationsSubjectName,
            this.colMouseFixationsTrialID,
            this.colMouseFixationsTrialSequence,
            this.colMouseFixationsCountInTrial,
            this.colMouseFixationsStartTime,
            this.colMouseFixationsLength,
            this.colMouseFixationsPosX,
            this.colMouseFixationsPosY});
      this.dgvMouseFixations.ContextMenuStrip = this.cmsDataGridView;
      this.dgvMouseFixations.DataSource = this.bsoTrialsMouseFixations;
      this.dgvMouseFixations.Dock = System.Windows.Forms.DockStyle.Fill;
      this.dgvMouseFixations.Location = new System.Drawing.Point(3, 3);
      this.dgvMouseFixations.Name = "dgvMouseFixations";
      this.dgvMouseFixations.Size = new System.Drawing.Size(650, 291);
      this.dgvMouseFixations.TabIndex = 0;
      this.dgvMouseFixations.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridView_DataError);
      // 
      // colMouseFixationsID
      // 
      this.colMouseFixationsID.DataPropertyName = "ID";
      this.colMouseFixationsID.HeaderText = "ID";
      this.colMouseFixationsID.MinimumWidth = 30;
      this.colMouseFixationsID.Name = "colMouseFixationsID";
      this.colMouseFixationsID.ReadOnly = true;
      this.colMouseFixationsID.Width = 60;
      // 
      // colMouseFixationsSubjectName
      // 
      this.colMouseFixationsSubjectName.DataPropertyName = "SubjectName";
      this.colMouseFixationsSubjectName.HeaderText = "SubjectName";
      this.colMouseFixationsSubjectName.MinimumWidth = 30;
      this.colMouseFixationsSubjectName.Name = "colMouseFixationsSubjectName";
      this.colMouseFixationsSubjectName.Width = 80;
      // 
      // colMouseFixationsTrialID
      // 
      this.colMouseFixationsTrialID.DataPropertyName = "TrialID";
      this.colMouseFixationsTrialID.HeaderText = "TrialID";
      this.colMouseFixationsTrialID.MinimumWidth = 30;
      this.colMouseFixationsTrialID.Name = "colMouseFixationsTrialID";
      this.colMouseFixationsTrialID.Width = 60;
      // 
      // colMouseFixationsTrialSequence
      // 
      this.colMouseFixationsTrialSequence.DataPropertyName = "TrialSequence";
      this.colMouseFixationsTrialSequence.HeaderText = "TrialSequence";
      this.colMouseFixationsTrialSequence.MinimumWidth = 30;
      this.colMouseFixationsTrialSequence.Name = "colMouseFixationsTrialSequence";
      this.colMouseFixationsTrialSequence.Width = 80;
      // 
      // colMouseFixationsCountInTrial
      // 
      this.colMouseFixationsCountInTrial.DataPropertyName = "CountInTrial";
      this.colMouseFixationsCountInTrial.HeaderText = "CountInTrial";
      this.colMouseFixationsCountInTrial.MinimumWidth = 30;
      this.colMouseFixationsCountInTrial.Name = "colMouseFixationsCountInTrial";
      this.colMouseFixationsCountInTrial.Width = 80;
      // 
      // colMouseFixationsStartTime
      // 
      this.colMouseFixationsStartTime.DataPropertyName = "StartTime";
      this.colMouseFixationsStartTime.HeaderText = "StartTime";
      this.colMouseFixationsStartTime.MinimumWidth = 30;
      this.colMouseFixationsStartTime.Name = "colMouseFixationsStartTime";
      this.colMouseFixationsStartTime.Width = 80;
      // 
      // colMouseFixationsLength
      // 
      this.colMouseFixationsLength.DataPropertyName = "Length";
      this.colMouseFixationsLength.HeaderText = "Length";
      this.colMouseFixationsLength.MinimumWidth = 30;
      this.colMouseFixationsLength.Name = "colMouseFixationsLength";
      this.colMouseFixationsLength.Width = 80;
      // 
      // colMouseFixationsPosX
      // 
      this.colMouseFixationsPosX.DataPropertyName = "PosX";
      this.colMouseFixationsPosX.HeaderText = "PosX";
      this.colMouseFixationsPosX.MinimumWidth = 30;
      this.colMouseFixationsPosX.Name = "colMouseFixationsPosX";
      this.colMouseFixationsPosX.Width = 60;
      // 
      // colMouseFixationsPosY
      // 
      this.colMouseFixationsPosY.DataPropertyName = "PosY";
      this.colMouseFixationsPosY.HeaderText = "PosY";
      this.colMouseFixationsPosY.MinimumWidth = 30;
      this.colMouseFixationsPosY.Name = "colMouseFixationsPosY";
      this.colMouseFixationsPosY.Width = 60;
      // 
      // tbpAOI
      // 
      this.tbpAOI.Controls.Add(this.dgvAOIs);
      this.tbpAOI.ImageKey = "AOIs";
      this.tbpAOI.Location = new System.Drawing.Point(4, 23);
      this.tbpAOI.Name = "tbpAOI";
      this.tbpAOI.Padding = new System.Windows.Forms.Padding(3);
      this.tbpAOI.Size = new System.Drawing.Size(656, 297);
      this.tbpAOI.TabIndex = 4;
      this.tbpAOI.Text = "Areas of Interest";
      this.tbpAOI.UseVisualStyleBackColor = true;
      // 
      // dgvAOIs
      // 
      this.dgvAOIs.AutoGenerateColumns = false;
      this.dgvAOIs.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
      this.dgvAOIs.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colAOIsID,
            this.colAOIsTrialID,
            this.colAOIsSlideNr,
            this.colAOIsShapeName,
            this.colAOIsShapeType,
            this.colAOIsShapeNumPts,
            this.colAOIsShapePts,
            this.colAOIsShapeGroup});
      this.dgvAOIs.ContextMenuStrip = this.cmsDataGridView;
      this.dgvAOIs.DataSource = this.bsoTrialsAOIs;
      this.dgvAOIs.Dock = System.Windows.Forms.DockStyle.Fill;
      this.dgvAOIs.Location = new System.Drawing.Point(3, 3);
      this.dgvAOIs.Name = "dgvAOIs";
      this.dgvAOIs.Size = new System.Drawing.Size(650, 291);
      this.dgvAOIs.TabIndex = 0;
      this.dgvAOIs.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridView_DataError);
      // 
      // colAOIsID
      // 
      this.colAOIsID.DataPropertyName = "ID";
      this.colAOIsID.HeaderText = "ID";
      this.colAOIsID.MinimumWidth = 30;
      this.colAOIsID.Name = "colAOIsID";
      this.colAOIsID.ReadOnly = true;
      this.colAOIsID.Width = 60;
      // 
      // colAOIsTrialID
      // 
      this.colAOIsTrialID.DataPropertyName = "TrialID";
      this.colAOIsTrialID.HeaderText = "TrialID";
      this.colAOIsTrialID.MinimumWidth = 30;
      this.colAOIsTrialID.Name = "colAOIsTrialID";
      this.colAOIsTrialID.Width = 60;
      // 
      // colAOIsSlideNr
      // 
      this.colAOIsSlideNr.DataPropertyName = "SlideNr";
      this.colAOIsSlideNr.HeaderText = "SlideNr";
      this.colAOIsSlideNr.MinimumWidth = 30;
      this.colAOIsSlideNr.Name = "colAOIsSlideNr";
      this.colAOIsSlideNr.Width = 60;
      // 
      // colAOIsShapeName
      // 
      this.colAOIsShapeName.DataPropertyName = "ShapeName";
      this.colAOIsShapeName.HeaderText = "Name";
      this.colAOIsShapeName.MinimumWidth = 30;
      this.colAOIsShapeName.Name = "colAOIsShapeName";
      this.colAOIsShapeName.Width = 80;
      // 
      // colAOIsShapeType
      // 
      this.colAOIsShapeType.DataPropertyName = "ShapeType";
      this.colAOIsShapeType.HeaderText = "Type";
      this.colAOIsShapeType.MinimumWidth = 30;
      this.colAOIsShapeType.Name = "colAOIsShapeType";
      this.colAOIsShapeType.Width = 80;
      // 
      // colAOIsShapeNumPts
      // 
      this.colAOIsShapeNumPts.DataPropertyName = "ShapeNumPts";
      this.colAOIsShapeNumPts.HeaderText = "NumPts";
      this.colAOIsShapeNumPts.MinimumWidth = 30;
      this.colAOIsShapeNumPts.Name = "colAOIsShapeNumPts";
      this.colAOIsShapeNumPts.Width = 60;
      // 
      // colAOIsShapePts
      // 
      this.colAOIsShapePts.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
      this.colAOIsShapePts.DataPropertyName = "ShapePts";
      this.colAOIsShapePts.HeaderText = "Points";
      this.colAOIsShapePts.MinimumWidth = 30;
      this.colAOIsShapePts.Name = "colAOIsShapePts";
      // 
      // colAOIsShapeGroup
      // 
      this.colAOIsShapeGroup.DataPropertyName = "ShapeGroup";
      this.colAOIsShapeGroup.DataSource = this.bsoShapeGroups;
      this.colAOIsShapeGroup.DisplayMember = "ShapeGroup";
      this.colAOIsShapeGroup.HeaderText = "Group";
      this.colAOIsShapeGroup.MinimumWidth = 30;
      this.colAOIsShapeGroup.Name = "colAOIsShapeGroup";
      this.colAOIsShapeGroup.Resizable = System.Windows.Forms.DataGridViewTriState.True;
      this.colAOIsShapeGroup.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
      this.colAOIsShapeGroup.ValueMember = "ShapeGroup";
      this.colAOIsShapeGroup.Width = 80;
      // 
      // tbpShapeGroups
      // 
      this.tbpShapeGroups.Controls.Add(this.dgvShapeGroups);
      this.tbpShapeGroups.ImageKey = "ShapeGroups";
      this.tbpShapeGroups.Location = new System.Drawing.Point(4, 23);
      this.tbpShapeGroups.Name = "tbpShapeGroups";
      this.tbpShapeGroups.Padding = new System.Windows.Forms.Padding(3);
      this.tbpShapeGroups.Size = new System.Drawing.Size(656, 297);
      this.tbpShapeGroups.TabIndex = 6;
      this.tbpShapeGroups.Text = "Shape Groups";
      this.tbpShapeGroups.UseVisualStyleBackColor = true;
      // 
      // dgvShapeGroups
      // 
      this.dgvShapeGroups.AutoGenerateColumns = false;
      this.dgvShapeGroups.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
      this.dgvShapeGroups.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colShapeGroupsID,
            this.colShapeGroupsShapeGroup});
      this.dgvShapeGroups.ContextMenuStrip = this.cmsDataGridView;
      this.dgvShapeGroups.DataSource = this.bsoShapeGroups;
      this.dgvShapeGroups.Dock = System.Windows.Forms.DockStyle.Fill;
      this.dgvShapeGroups.Location = new System.Drawing.Point(3, 3);
      this.dgvShapeGroups.Name = "dgvShapeGroups";
      this.dgvShapeGroups.Size = new System.Drawing.Size(650, 291);
      this.dgvShapeGroups.TabIndex = 0;
      this.dgvShapeGroups.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridView_DataError);
      // 
      // colShapeGroupsID
      // 
      this.colShapeGroupsID.DataPropertyName = "ID";
      this.colShapeGroupsID.HeaderText = "ID";
      this.colShapeGroupsID.MinimumWidth = 30;
      this.colShapeGroupsID.Name = "colShapeGroupsID";
      this.colShapeGroupsID.ReadOnly = true;
      this.colShapeGroupsID.Width = 60;
      // 
      // colShapeGroupsShapeGroup
      // 
      this.colShapeGroupsShapeGroup.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
      this.colShapeGroupsShapeGroup.DataPropertyName = "ShapeGroup";
      this.colShapeGroupsShapeGroup.HeaderText = "ShapeGroup";
      this.colShapeGroupsShapeGroup.MinimumWidth = 30;
      this.colShapeGroupsShapeGroup.Name = "colShapeGroupsShapeGroup";
      // 
      // imlTabs
      // 
      this.imlTabs.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imlTabs.ImageStream")));
      this.imlTabs.TransparentColor = System.Drawing.Color.Magenta;
      this.imlTabs.Images.SetKeyName(0, "Subjects");
      this.imlTabs.Images.SetKeyName(1, "SubjectProperties");
      this.imlTabs.Images.SetKeyName(2, "GazeFixations");
      this.imlTabs.Images.SetKeyName(3, "MouseFixations");
      this.imlTabs.Images.SetKeyName(4, "Trials");
      this.imlTabs.Images.SetKeyName(5, "RawData");
      this.imlTabs.Images.SetKeyName(6, "Events");
      this.imlTabs.Images.SetKeyName(7, "ShapeGroups");
      this.imlTabs.Images.SetKeyName(8, "AOIs");
      this.imlTabs.Images.SetKeyName(9, "Params");
      // 
      // toolStrip1
      // 
      this.toolStrip1.DataBindings.Add(new System.Windows.Forms.Binding("Location", global::Ogama.Properties.Settings.Default, "DTBToolbarLocation", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
      this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnImport,
            this.btnImportOgamaFormat,
            this.btnExport,
            this.toolStripSeparator1,
            this.btnSave,
            this.toolStripSeparator3,
            this.btnFilterData,
            this.toolStripSeparator2,
            this.btnHelp,
            this.btnSpecial});
      this.toolStrip1.Location = global::Ogama.Properties.Settings.Default.DTBToolbarLocation;
      this.toolStrip1.Name = "toolStrip1";
      this.toolStrip1.Size = new System.Drawing.Size(312, 25);
      this.toolStrip1.TabIndex = 0;
      // 
      // btnImport
      // 
      this.btnImport.Image = global::Ogama.Properties.Resources.MagicWand;
      this.btnImport.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnImport.Name = "btnImport";
      this.btnImport.Size = new System.Drawing.Size(113, 22);
      this.btnImport.Text = "Import Assistant";
      this.btnImport.ToolTipText = "Start import assistant to import eye- and/or mousetracker\r\ndata from ASCII files." +
    "";
      this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
      // 
      // btnImportOgamaFormat
      // 
      this.btnImportOgamaFormat.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnImportOgamaFormat.Image = global::Ogama.Properties.Resources.ImportData;
      this.btnImportOgamaFormat.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnImportOgamaFormat.Name = "btnImportOgamaFormat";
      this.btnImportOgamaFormat.Size = new System.Drawing.Size(23, 22);
      this.btnImportOgamaFormat.ToolTipText = "Import data that was previously exported with OGAMA.";
      this.btnImportOgamaFormat.Click += new System.EventHandler(this.btnImportOgamaFormat_Click);
      // 
      // btnExport
      // 
      this.btnExport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnExport.Image = global::Ogama.Properties.Resources.ExportData;
      this.btnExport.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnExport.Name = "btnExport";
      this.btnExport.Size = new System.Drawing.Size(23, 22);
      this.btnExport.ToolTipText = "Export tables into ASCII-files.";
      this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
      // 
      // toolStripSeparator1
      // 
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
      // 
      // btnSave
      // 
      this.btnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnSave.Image = global::Ogama.Properties.Resources.saveHS;
      this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnSave.Name = "btnSave";
      this.btnSave.Size = new System.Drawing.Size(23, 22);
      this.btnSave.Text = "Commit changes";
      this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
      // 
      // toolStripSeparator3
      // 
      this.toolStripSeparator3.Name = "toolStripSeparator3";
      this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
      // 
      // btnFilterData
      // 
      this.btnFilterData.Checked = true;
      this.btnFilterData.CheckOnClick = true;
      this.btnFilterData.CheckState = System.Windows.Forms.CheckState.Checked;
      this.btnFilterData.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnFilterData.Image = global::Ogama.Properties.Resources.Filter2HS;
      this.btnFilterData.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnFilterData.Name = "btnFilterData";
      this.btnFilterData.Size = new System.Drawing.Size(23, 22);
      this.btnFilterData.Text = "Filter by subject and trial";
      this.btnFilterData.Click += new System.EventHandler(this.btnFilterData_Click);
      // 
      // toolStripSeparator2
      // 
      this.toolStripSeparator2.Name = "toolStripSeparator2";
      this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
      // 
      // btnHelp
      // 
      this.btnHelp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnHelp.Image = global::Ogama.Properties.Resources.HelpBmp;
      this.btnHelp.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnHelp.Name = "btnHelp";
      this.btnHelp.Size = new System.Drawing.Size(23, 22);
      this.btnHelp.ToolTipText = "Displays help for this interface.";
      // 
      // btnSpecial
      // 
      this.btnSpecial.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnSpecial.Image = ((System.Drawing.Image)(resources.GetObject("btnSpecial.Image")));
      this.btnSpecial.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnSpecial.Name = "btnSpecial";
      this.btnSpecial.Size = new System.Drawing.Size(23, 22);
      this.btnSpecial.Text = "toolStripButton1";
      this.btnSpecial.ToolTipText = "Special Conversions";
      this.btnSpecial.Visible = false;
      this.btnSpecial.Click += new System.EventHandler(this.btnSpecial_Click);
      // 
      // sfdExport
      // 
      this.sfdExport.DefaultExt = "*.txt";
      this.sfdExport.FileName = "*.txt";
      this.sfdExport.Filter = "Text files - *.txt|*.txt|comma separated values - *.csv|*.csv";
      this.sfdExport.Title = "Choose file to save raw data ...";
      // 
      // bgwExport
      // 
      this.bgwExport.WorkerReportsProgress = true;
      this.bgwExport.WorkerSupportsCancellation = true;
      this.bgwExport.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwExport_DoWork);
      this.bgwExport.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgwExport_ProgressChanged);
      this.bgwExport.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwExport_RunWorkerCompleted);
      // 
      // DatabaseModule
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(664, 349);
      this.Controls.Add(this.toolStripContainer1);
      this.DataBindings.Add(new System.Windows.Forms.Binding("Location", global::Ogama.Properties.Settings.Default, "DatabaseModuleLocation", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Location = global::Ogama.Properties.Settings.Default.DatabaseModuleLocation;
      this.Logo = global::Ogama.Properties.Resources.DatabaseLogo;
      this.Name = "DatabaseModule";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Database Module";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DatabaseModule_FormClosing);
      this.Load += new System.EventHandler(this.DatabaseModule_Load);
      ((System.ComponentModel.ISupportInitialize)(this.bsoSubjects)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.bsoFKSubjectsTrials)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.ogamaDataSet)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.bsoFKTrialsEvents)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.bsoTrialsAOIs)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.bsoTrialsGazeFixations)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.bsoTrialsMouseFixations)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.bsoShapeGroups)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.bsoFKSubjectsSubjectParameters)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.bsoSubjectParameters)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.bsoTrialEvents)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.bsoTrials)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.bsoGazeFixations)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.bsoMouseFixations)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.bsoAOIs)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.bsoParams)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.dgvSubjects)).EndInit();
      this.cmsDataGridView.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.dgvRawData)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.dgvTrials)).EndInit();
      this.toolStripContainer1.ContentPanel.ResumeLayout(false);
      this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
      this.toolStripContainer1.TopToolStripPanel.PerformLayout();
      this.toolStripContainer1.ResumeLayout(false);
      this.toolStripContainer1.PerformLayout();
      this.tacTables.ResumeLayout(false);
      this.tbpSubjects.ResumeLayout(false);
      this.tbpSubjectParameters.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.dgvSubjectParameters)).EndInit();
      this.tbpParams.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.dgvParams)).EndInit();
      this.tbpTrials.ResumeLayout(false);
      this.tbpTrialEvents.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.dgvTrialEvents)).EndInit();
      this.tbpRawdata.ResumeLayout(false);
      this.tbpGazeFixations.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.dgvGazeFixations)).EndInit();
      this.tbpMouseFixations.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.dgvMouseFixations)).EndInit();
      this.tbpAOI.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.dgvAOIs)).EndInit();
      this.tbpShapeGroups.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.dgvShapeGroups)).EndInit();
      this.toolStrip1.ResumeLayout(false);
      this.toolStrip1.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private DataGridView dgvRawData;
    private DataGridView dgvSubjects;
    private DataGridView dgvTrials;
    private ToolTip toolTip1;
    private ToolStripContainer toolStripContainer1;
    private ToolStrip toolStrip1;
    private ToolStripButton btnExport;
    private ToolStripSeparator toolStripSeparator1;
    private ToolStripButton btnHelp;
    private SaveFileDialog sfdExport;
    private ContextMenuStrip cmsDataGridView;
    private ToolStripMenuItem cmsSelect;
    private ToolStripMenuItem cmsCopy;
    private ToolStripButton btnSpecial;
    private ToolStripButton btnImportOgamaFormat;
    private BackgroundWorker bgwExport;
    private TabControl tacTables;
    private TabPage tbpSubjects;
    private TabPage tbpTrials;
    private TabPage tbpRawdata;
    private TabPage tbpGazeFixations;
    private TabPage tbpAOI;
    private DataGridView dgvTrialEvents;
    private TabPage tbpTrialEvents;
    private DataGridView dgvGazeFixations;
		private DataGridView dgvMouseFixations;
		private DataGridView dgvAOIs;
    private ImageList imlTabs;
		private ToolStripButton btnFilterData;
    private ToolStripSeparator toolStripSeparator2;
    private TabPage tbpShapeGroups;
    private DataGridView dgvShapeGroups;
    private ToolStripButton btnSave;
    private ToolStripSeparator toolStripSeparator3;
    private DataGridViewTextBoxColumn colSubjectsID;
    private DataGridViewTextBoxColumn colSubjectsSubjectName;
    private DataGridViewTextBoxColumn colSubjectsCategory;
    private DataGridViewTextBoxColumn colSubjectsAge;
    private DataGridViewTextBoxColumn colSubjectsSex;
    private DataGridViewTextBoxColumn colSubjectsHandedness;
    private DataGridViewTextBoxColumn colSubjectsComments;
    private DataGridViewTextBoxColumn colTrialsID;
    private DataGridViewTextBoxColumn colTrialsSubjectName;
    private DataGridViewTextBoxColumn colTrialsTrialSequence;
    private DataGridViewTextBoxColumn colTrialsTrialID;
    private DataGridViewTextBoxColumn colTrialsTrialName;
    private DataGridViewTextBoxColumn colTrialsCategory;
    private DataGridViewTextBoxColumn colTrialsTrialStartTime;
    private DataGridViewTextBoxColumn colTrialsDuration;
    private DataGridViewTextBoxColumn colTrialsEliminateData;
    private DataGridViewTextBoxColumn colTrialEventsID;
    private DataGridViewTextBoxColumn colTrialEventsSubjectName;
    private DataGridViewTextBoxColumn colTrialEventsTrialSequence;
    private DataGridViewTextBoxColumn colTrialEventsEventID;
    private DataGridViewTextBoxColumn colTrialEventsEventTime;
    private DataGridViewTextBoxColumn colTrialEventsEventType;
    private DataGridViewTextBoxColumn colTrialEventsEventTask;
    private DataGridViewTextBoxColumn colTrialEventsEventParam;
    private DataGridViewTextBoxColumn colGazeFixationsID;
    private DataGridViewTextBoxColumn colGazeFixationsSubjectName;
    private DataGridViewTextBoxColumn colGazeFixationsTrialID;
    private DataGridViewTextBoxColumn colGazeFixationsTrialSequence;
    private DataGridViewTextBoxColumn colGazeFixationsCountInTrial;
    private DataGridViewTextBoxColumn colGazeFixationsStartTime;
    private DataGridViewTextBoxColumn colGazeFixationsLength;
    private DataGridViewTextBoxColumn colGazeFixationsPosX;
    private DataGridViewTextBoxColumn colGazeFixationsPosY;
    private DataGridViewTextBoxColumn colMouseFixationsID;
    private DataGridViewTextBoxColumn colMouseFixationsSubjectName;
    private DataGridViewTextBoxColumn colMouseFixationsTrialID;
    private DataGridViewTextBoxColumn colMouseFixationsTrialSequence;
    private DataGridViewTextBoxColumn colMouseFixationsCountInTrial;
    private DataGridViewTextBoxColumn colMouseFixationsStartTime;
    private DataGridViewTextBoxColumn colMouseFixationsLength;
    private DataGridViewTextBoxColumn colMouseFixationsPosX;
    private DataGridViewTextBoxColumn colMouseFixationsPosY;
    private DataGridViewTextBoxColumn colAOIsID;
    private DataGridViewTextBoxColumn colAOIsTrialID;
    private DataGridViewTextBoxColumn colAOIsSlideNr;
    private DataGridViewTextBoxColumn colAOIsShapeName;
    private DataGridViewTextBoxColumn colAOIsShapeType;
    private DataGridViewTextBoxColumn colAOIsShapeNumPts;
    private DataGridViewTextBoxColumn colAOIsShapePts;
    private DataGridViewComboBoxColumn colAOIsShapeGroup;
    private DataGridViewTextBoxColumn colShapeGroupsID;
    private DataGridViewTextBoxColumn colShapeGroupsShapeGroup;
    private TabPage tbpMouseFixations;
    private TabPage tbpSubjectParameters;
    private DataGridView dgvSubjectParameters;
    private TabPage tbpParams;
    private DataGridView dgvParams;
    private DataGridViewTextBoxColumn colSubjectParametersID;
    private DataGridViewTextBoxColumn colSubjectParametersSubjectName;
    private DataGridViewComboBoxColumn colSubjectParametersParam;
    private DataGridViewTextBoxColumn colSubjectParametersSubjectNameParamValue;
    private DataGridViewTextBoxColumn colParamsID;
    private DataGridViewTextBoxColumn paramDataGridViewTextBoxColumn;
    private DataGridViewTextBoxColumn descriptionDataGridViewTextBoxColumn;
    private ToolStripButton btnImport;
  }
}