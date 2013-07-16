namespace Ogama.Modules.Fixations
{
  using Ogama.Modules.Common.Controls;
  using Ogama.Modules.Common.PictureTemplates;

  partial class FixationsModule
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FixationsModule));
      this.spcPictureTools = new System.Windows.Forms.SplitContainer();
      this.spcPictureSlider = new System.Windows.Forms.SplitContainer();
      this.pnlCanvas = new System.Windows.Forms.Panel();
      this.pnlPicture = new System.Windows.Forms.Panel();
      this.fixationsPicture = new PictureWithFixations();
      this.toolStrip1 = new System.Windows.Forms.ToolStrip();
      this.btnSeekNextSlide = new System.Windows.Forms.ToolStripButton();
      this.btnSeekPreviousSlide = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
      this.trialTimeLine = new TrialTimeLine(this.components);
      this.trbZoom = new OgamaControls.ToolStripTrackBar();
      this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
      this.spcGazeMouseTables = new System.Windows.Forms.SplitContainer();
      this.dgvGazeFixations = new System.Windows.Forms.DataGridView();
      this.iDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.subjectNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.trialIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.trialSequenceDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.countInTrialDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.startTimeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.lengthDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.posXDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.posYDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.cmnuDGV = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.cmnuSelectAll = new System.Windows.Forms.ToolStripMenuItem();
      this.cmnuCopyToClipboard = new System.Windows.Forms.ToolStripMenuItem();
      this.dgvMouseFixations = new System.Windows.Forms.DataGridView();
      this.iDDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.subjectNameDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.trialIDDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.trialSequenceDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.countInTrialDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.startTimeDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.lengthDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.posXDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.posYDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.tosActions = new System.Windows.Forms.ToolStrip();
      this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
      this.btnCalculateFix = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
      this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
      this.btnSelectedSubject = new System.Windows.Forms.ToolStripButton();
      this.btnAllSubjects = new System.Windows.Forms.ToolStripButton();
      this.btnShowOptions = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
      this.btnDeleteFixations = new System.Windows.Forms.ToolStripButton();
      this.btnEliminateData = new System.Windows.Forms.ToolStripButton();
      this.btnShowHideAOI = new System.Windows.Forms.ToolStripButton();
      this.btnShowHideTables = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
      this.nudDriftXCorrection = new OgamaControls.ToolStripNumericUpDown();
      this.nudDriftYCorrection = new OgamaControls.ToolStripNumericUpDown();
      this.btnSaveDriftCorrection = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
      this.toolStripLabel5 = new System.Windows.Forms.ToolStripLabel();
      this.btnImport = new System.Windows.Forms.ToolStripButton();
      this.btnExport = new System.Windows.Forms.ToolStripButton();
      this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
      this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
      this.tosMouseDisplay = new System.Windows.Forms.ToolStrip();
      this.toolStripButton11 = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator12 = new System.Windows.Forms.ToolStripSeparator();
      this.toolStripLabel7 = new System.Windows.Forms.ToolStripLabel();
      this.cbbMouseDisplayMode = new System.Windows.Forms.ToolStripComboBox();
      this.btnMouseConnections = new System.Windows.Forms.ToolStripButton();
      this.btnMouseNumbers = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator13 = new System.Windows.Forms.ToolStripSeparator();
      this.btnMousePenStyle = new System.Windows.Forms.ToolStripButton();
      this.nudMouseFixDiameterDiv = new OgamaControls.ToolStripNumericUpDown();
      this.tosTrialSelection = new System.Windows.Forms.ToolStrip();
      this.cbbSubject = new System.Windows.Forms.ToolStripComboBox();
      this.cbbTrial = new System.Windows.Forms.ToolStripComboBox();
      this.btnEye = new System.Windows.Forms.ToolStripButton();
      this.btnMouse = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
      this.btnHelp = new System.Windows.Forms.ToolStripButton();
      this.tosGazeDisplay = new System.Windows.Forms.ToolStrip();
      this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
      this.toolStripLabel6 = new System.Windows.Forms.ToolStripLabel();
      this.cbbGazeDisplayMode = new System.Windows.Forms.ToolStripComboBox();
      this.btnGazeConnections = new System.Windows.Forms.ToolStripButton();
      this.btnGazeNumbers = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
      this.btnGazePenStyle = new System.Windows.Forms.ToolStripButton();
      this.nudGazeFixDiameterDiv = new OgamaControls.ToolStripNumericUpDown();
      this.bgwCalcFixationsForAllSubjects = new System.ComponentModel.BackgroundWorker();
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
      this.spcPictureTools.Panel1.SuspendLayout();
      this.spcPictureTools.Panel2.SuspendLayout();
      this.spcPictureTools.SuspendLayout();
      this.spcPictureSlider.Panel1.SuspendLayout();
      this.spcPictureSlider.Panel2.SuspendLayout();
      this.spcPictureSlider.SuspendLayout();
      this.pnlCanvas.SuspendLayout();
      this.pnlPicture.SuspendLayout();
      this.toolStrip1.SuspendLayout();
      this.spcGazeMouseTables.Panel1.SuspendLayout();
      this.spcGazeMouseTables.Panel2.SuspendLayout();
      this.spcGazeMouseTables.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.dgvGazeFixations)).BeginInit();
      this.cmnuDGV.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.dgvMouseFixations)).BeginInit();
      this.tosActions.SuspendLayout();
      this.toolStripContainer1.ContentPanel.SuspendLayout();
      this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
      this.toolStripContainer1.SuspendLayout();
      this.tosMouseDisplay.SuspendLayout();
      this.tosTrialSelection.SuspendLayout();
      this.tosGazeDisplay.SuspendLayout();
      this.SuspendLayout();
      // 
      // spcPictureTools
      // 
      this.spcPictureTools.Dock = System.Windows.Forms.DockStyle.Fill;
      this.spcPictureTools.Location = new System.Drawing.Point(0, 0);
      this.spcPictureTools.Margin = new System.Windows.Forms.Padding(0);
      this.spcPictureTools.Name = "spcPictureTools";
      this.spcPictureTools.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // spcPictureTools.Panel1
      // 
      this.spcPictureTools.Panel1.Controls.Add(this.spcPictureSlider);
      // 
      // spcPictureTools.Panel2
      // 
      this.spcPictureTools.Panel2.Controls.Add(this.spcGazeMouseTables);
      this.spcPictureTools.Size = new System.Drawing.Size(1096, 372);
      this.spcPictureTools.SplitterDistance = 215;
      this.spcPictureTools.TabIndex = 2;
      // 
      // spcPictureSlider
      // 
      this.spcPictureSlider.Dock = System.Windows.Forms.DockStyle.Fill;
      this.spcPictureSlider.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
      this.spcPictureSlider.Location = new System.Drawing.Point(0, 0);
      this.spcPictureSlider.Name = "spcPictureSlider";
      this.spcPictureSlider.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // spcPictureSlider.Panel1
      // 
      this.spcPictureSlider.Panel1.Controls.Add(this.pnlCanvas);
      // 
      // spcPictureSlider.Panel2
      // 
      this.spcPictureSlider.Panel2.Controls.Add(this.toolStrip1);
      this.spcPictureSlider.Size = new System.Drawing.Size(1096, 215);
      this.spcPictureSlider.SplitterDistance = 189;
      this.spcPictureSlider.SplitterWidth = 1;
      this.spcPictureSlider.TabIndex = 1;
      // 
      // pnlCanvas
      // 
      this.pnlCanvas.AutoScroll = true;
      this.pnlCanvas.BackColor = global::Ogama.Properties.Settings.Default.BackgroundColorForms;
      this.pnlCanvas.Controls.Add(this.pnlPicture);
      this.pnlCanvas.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::Ogama.Properties.Settings.Default, "BackgroundColorForms", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.pnlCanvas.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pnlCanvas.Location = new System.Drawing.Point(0, 0);
      this.pnlCanvas.Margin = new System.Windows.Forms.Padding(0);
      this.pnlCanvas.Name = "pnlCanvas";
      this.pnlCanvas.Size = new System.Drawing.Size(1096, 189);
      this.pnlCanvas.TabIndex = 3;
      // 
      // pnlPicture
      // 
      this.pnlPicture.BackColor = System.Drawing.Color.Black;
      this.pnlPicture.Controls.Add(this.fixationsPicture);
      this.pnlPicture.Location = new System.Drawing.Point(167, 62);
      this.pnlPicture.Name = "pnlPicture";
      this.pnlPicture.Size = new System.Drawing.Size(200, 100);
      this.pnlPicture.TabIndex = 1;
      // 
      // fixationsPicture
      // 
      this.fixationsPicture.AnimationInterval = 10;
      this.fixationsPicture.BackColor = global::Ogama.Properties.Settings.Default.BackgroundColorForms;
      this.fixationsPicture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
      this.fixationsPicture.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::Ogama.Properties.Settings.Default, "BackgroundColorForms", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.fixationsPicture.InvalidateInterval = 500;
      this.fixationsPicture.Location = new System.Drawing.Point(0, 0);
      this.fixationsPicture.Margin = new System.Windows.Forms.Padding(0);
      this.fixationsPicture.Name = "fixationsPicture";
      this.fixationsPicture.Size = new System.Drawing.Size(200, 100);
      this.fixationsPicture.TabIndex = 0;
      this.fixationsPicture.TabStop = false;
      this.fixationsPicture.ZoomFactor = 0F;
      // 
      // toolStrip1
      // 
      this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
      this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnSeekNextSlide,
            this.btnSeekPreviousSlide,
            this.toolStripSeparator2,
            this.trialTimeLine,
            this.toolStripSeparator4,
            this.trbZoom});
      this.toolStrip1.Location = new System.Drawing.Point(0, 0);
      this.toolStrip1.Name = "toolStrip1";
      this.toolStrip1.Size = new System.Drawing.Size(1096, 26);
      this.toolStrip1.Stretch = true;
      this.toolStrip1.TabIndex = 0;
      this.toolStrip1.Text = "toolStrip1";
      // 
      // btnSeekNextSlide
      // 
      this.btnSeekNextSlide.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnSeekNextSlide.Image = global::Ogama.Properties.Resources.DataContainer_MoveLastHS;
      this.btnSeekNextSlide.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnSeekNextSlide.Name = "btnSeekNextSlide";
      this.btnSeekNextSlide.Size = new System.Drawing.Size(23, 23);
      this.btnSeekNextSlide.Text = "Seek to next slide";
      this.btnSeekNextSlide.Click += new System.EventHandler(this.btnSeekNextSlide_Click);
      // 
      // btnSeekPreviousSlide
      // 
      this.btnSeekPreviousSlide.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnSeekPreviousSlide.Image = global::Ogama.Properties.Resources.DataContainer_MoveFirstHS;
      this.btnSeekPreviousSlide.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnSeekPreviousSlide.Name = "btnSeekPreviousSlide";
      this.btnSeekPreviousSlide.Size = new System.Drawing.Size(23, 23);
      this.btnSeekPreviousSlide.Text = "Seek to previous slide";
      this.btnSeekPreviousSlide.Click += new System.EventHandler(this.btnSeekPreviousSlide_Click);
      // 
      // toolStripSeparator2
      // 
      this.toolStripSeparator2.Name = "toolStripSeparator2";
      this.toolStripSeparator2.Size = new System.Drawing.Size(6, 26);
      // 
      // trialTimeLine
      // 
      this.trialTimeLine.Duration = 0;
      this.trialTimeLine.Name = "trialTimeLine";
      this.trialTimeLine.ShowCaret = false;
      this.trialTimeLine.Size = new System.Drawing.Size(902, 23);
      this.trialTimeLine.Text = "trialTimeLine";
      this.trialTimeLine.SectionStartValueChanged += new OgamaControls.TimeLine.PositionValueChangedEventHandler(this.TrialTimeLine_SectionStartValueChanged);
      this.trialTimeLine.SectionEndValueChanged += new OgamaControls.TimeLine.PositionValueChangedEventHandler(this.TrialTimeLine_SectionEndValueChanged);
      // 
      // trbZoom
      // 
      this.trbZoom.Maximum = 100;
      this.trbZoom.Minimum = 1;
      this.trbZoom.Name = "trbZoom";
      this.trbZoom.Size = new System.Drawing.Size(104, 23);
      this.trbZoom.TickFrequency = 1;
      this.trbZoom.TickStyle = System.Windows.Forms.TickStyle.BottomRight;
      this.trbZoom.ToolTipText = "Zoom, right-click for autozoom";
      this.trbZoom.Value = 1;
      // 
      // toolStripSeparator4
      // 
      this.toolStripSeparator4.Name = "toolStripSeparator4";
      this.toolStripSeparator4.Size = new System.Drawing.Size(6, 26);
      // 
      // spcGazeMouseTables
      // 
      this.spcGazeMouseTables.Dock = System.Windows.Forms.DockStyle.Fill;
      this.spcGazeMouseTables.Location = new System.Drawing.Point(0, 0);
      this.spcGazeMouseTables.Name = "spcGazeMouseTables";
      this.spcGazeMouseTables.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // spcGazeMouseTables.Panel1
      // 
      this.spcGazeMouseTables.Panel1.Controls.Add(this.dgvGazeFixations);
      // 
      // spcGazeMouseTables.Panel2
      // 
      this.spcGazeMouseTables.Panel2.Controls.Add(this.dgvMouseFixations);
      this.spcGazeMouseTables.Size = new System.Drawing.Size(1096, 153);
      this.spcGazeMouseTables.SplitterDistance = 72;
      this.spcGazeMouseTables.TabIndex = 3;
      // 
      // dgvGazeFixations
      // 
      this.dgvGazeFixations.AllowUserToAddRows = false;
      this.dgvGazeFixations.AllowUserToDeleteRows = false;
      this.dgvGazeFixations.AutoGenerateColumns = false;
      this.dgvGazeFixations.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
      this.dgvGazeFixations.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
      this.dgvGazeFixations.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dgvGazeFixations.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.iDDataGridViewTextBoxColumn,
            this.subjectNameDataGridViewTextBoxColumn,
            this.trialIDDataGridViewTextBoxColumn,
            this.trialSequenceDataGridViewTextBoxColumn,
            this.countInTrialDataGridViewTextBoxColumn,
            this.startTimeDataGridViewTextBoxColumn,
            this.lengthDataGridViewTextBoxColumn,
            this.posXDataGridViewTextBoxColumn,
            this.posYDataGridViewTextBoxColumn});
      this.dgvGazeFixations.ContextMenuStrip = this.cmnuDGV;
      this.dgvGazeFixations.DataSource = this.bsoGazeFixations;
      this.dgvGazeFixations.Dock = System.Windows.Forms.DockStyle.Fill;
      this.dgvGazeFixations.Location = new System.Drawing.Point(0, 0);
      this.dgvGazeFixations.Margin = new System.Windows.Forms.Padding(0);
      this.dgvGazeFixations.Name = "dgvGazeFixations";
      this.dgvGazeFixations.ReadOnly = true;
      this.dgvGazeFixations.Size = new System.Drawing.Size(1096, 72);
      this.dgvGazeFixations.TabIndex = 2;
      this.toolTip1.SetToolTip(this.dgvGazeFixations, "Gaze fixation table.");
      // 
      // iDDataGridViewTextBoxColumn
      // 
      this.iDDataGridViewTextBoxColumn.DataPropertyName = "ID";
      this.iDDataGridViewTextBoxColumn.HeaderText = "ID";
      this.iDDataGridViewTextBoxColumn.Name = "iDDataGridViewTextBoxColumn";
      this.iDDataGridViewTextBoxColumn.ReadOnly = true;
      // 
      // subjectNameDataGridViewTextBoxColumn
      // 
      this.subjectNameDataGridViewTextBoxColumn.DataPropertyName = "SubjectName";
      this.subjectNameDataGridViewTextBoxColumn.HeaderText = "SubjectName";
      this.subjectNameDataGridViewTextBoxColumn.Name = "subjectNameDataGridViewTextBoxColumn";
      this.subjectNameDataGridViewTextBoxColumn.ReadOnly = true;
      // 
      // trialIDDataGridViewTextBoxColumn
      // 
      this.trialIDDataGridViewTextBoxColumn.DataPropertyName = "TrialID";
      this.trialIDDataGridViewTextBoxColumn.HeaderText = "TrialID";
      this.trialIDDataGridViewTextBoxColumn.Name = "trialIDDataGridViewTextBoxColumn";
      this.trialIDDataGridViewTextBoxColumn.ReadOnly = true;
      // 
      // trialSequenceDataGridViewTextBoxColumn
      // 
      this.trialSequenceDataGridViewTextBoxColumn.DataPropertyName = "TrialSequence";
      this.trialSequenceDataGridViewTextBoxColumn.HeaderText = "TrialSequence";
      this.trialSequenceDataGridViewTextBoxColumn.Name = "trialSequenceDataGridViewTextBoxColumn";
      this.trialSequenceDataGridViewTextBoxColumn.ReadOnly = true;
      // 
      // countInTrialDataGridViewTextBoxColumn
      // 
      this.countInTrialDataGridViewTextBoxColumn.DataPropertyName = "CountInTrial";
      this.countInTrialDataGridViewTextBoxColumn.HeaderText = "CountInTrial";
      this.countInTrialDataGridViewTextBoxColumn.Name = "countInTrialDataGridViewTextBoxColumn";
      this.countInTrialDataGridViewTextBoxColumn.ReadOnly = true;
      // 
      // startTimeDataGridViewTextBoxColumn
      // 
      this.startTimeDataGridViewTextBoxColumn.DataPropertyName = "StartTime";
      this.startTimeDataGridViewTextBoxColumn.HeaderText = "StartTime";
      this.startTimeDataGridViewTextBoxColumn.Name = "startTimeDataGridViewTextBoxColumn";
      this.startTimeDataGridViewTextBoxColumn.ReadOnly = true;
      // 
      // lengthDataGridViewTextBoxColumn
      // 
      this.lengthDataGridViewTextBoxColumn.DataPropertyName = "Length";
      this.lengthDataGridViewTextBoxColumn.HeaderText = "Length";
      this.lengthDataGridViewTextBoxColumn.Name = "lengthDataGridViewTextBoxColumn";
      this.lengthDataGridViewTextBoxColumn.ReadOnly = true;
      // 
      // posXDataGridViewTextBoxColumn
      // 
      this.posXDataGridViewTextBoxColumn.DataPropertyName = "PosX";
      this.posXDataGridViewTextBoxColumn.HeaderText = "PosX";
      this.posXDataGridViewTextBoxColumn.Name = "posXDataGridViewTextBoxColumn";
      this.posXDataGridViewTextBoxColumn.ReadOnly = true;
      // 
      // posYDataGridViewTextBoxColumn
      // 
      this.posYDataGridViewTextBoxColumn.DataPropertyName = "PosY";
      this.posYDataGridViewTextBoxColumn.HeaderText = "PosY";
      this.posYDataGridViewTextBoxColumn.Name = "posYDataGridViewTextBoxColumn";
      this.posYDataGridViewTextBoxColumn.ReadOnly = true;
      // 
      // cmnuDGV
      // 
      this.cmnuDGV.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmnuSelectAll,
            this.cmnuCopyToClipboard});
      this.cmnuDGV.Name = "mnuContextDGV";
      this.cmnuDGV.Size = new System.Drawing.Size(165, 48);
      // 
      // cmnuSelectAll
      // 
      this.cmnuSelectAll.Name = "cmnuSelectAll";
      this.cmnuSelectAll.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
      this.cmnuSelectAll.Size = new System.Drawing.Size(164, 22);
      this.cmnuSelectAll.Text = "Select All";
      this.cmnuSelectAll.Click += new System.EventHandler(this.cmnuSelectAll_Click);
      // 
      // cmnuCopyToClipboard
      // 
      this.cmnuCopyToClipboard.Image = global::Ogama.Properties.Resources.CopyHS;
      this.cmnuCopyToClipboard.Name = "cmnuCopyToClipboard";
      this.cmnuCopyToClipboard.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
      this.cmnuCopyToClipboard.Size = new System.Drawing.Size(164, 22);
      this.cmnuCopyToClipboard.Text = "Copy";
      this.cmnuCopyToClipboard.Click += new System.EventHandler(this.cmnuCopyToClipboard_Click);
      // 
      // dgvMouseFixations
      // 
      this.dgvMouseFixations.AllowUserToAddRows = false;
      this.dgvMouseFixations.AllowUserToDeleteRows = false;
      this.dgvMouseFixations.AutoGenerateColumns = false;
      this.dgvMouseFixations.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
      this.dgvMouseFixations.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
      this.dgvMouseFixations.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dgvMouseFixations.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.iDDataGridViewTextBoxColumn1,
            this.subjectNameDataGridViewTextBoxColumn1,
            this.trialIDDataGridViewTextBoxColumn1,
            this.trialSequenceDataGridViewTextBoxColumn1,
            this.countInTrialDataGridViewTextBoxColumn1,
            this.startTimeDataGridViewTextBoxColumn1,
            this.lengthDataGridViewTextBoxColumn1,
            this.posXDataGridViewTextBoxColumn1,
            this.posYDataGridViewTextBoxColumn1});
      this.dgvMouseFixations.ContextMenuStrip = this.cmnuDGV;
      this.dgvMouseFixations.DataSource = this.bsoMouseFixations;
      this.dgvMouseFixations.Dock = System.Windows.Forms.DockStyle.Fill;
      this.dgvMouseFixations.Location = new System.Drawing.Point(0, 0);
      this.dgvMouseFixations.Name = "dgvMouseFixations";
      this.dgvMouseFixations.ReadOnly = true;
      this.dgvMouseFixations.Size = new System.Drawing.Size(1096, 77);
      this.dgvMouseFixations.TabIndex = 3;
      this.toolTip1.SetToolTip(this.dgvMouseFixations, "Mouse fixation table.");
      // 
      // iDDataGridViewTextBoxColumn1
      // 
      this.iDDataGridViewTextBoxColumn1.DataPropertyName = "ID";
      this.iDDataGridViewTextBoxColumn1.HeaderText = "ID";
      this.iDDataGridViewTextBoxColumn1.Name = "iDDataGridViewTextBoxColumn1";
      this.iDDataGridViewTextBoxColumn1.ReadOnly = true;
      // 
      // subjectNameDataGridViewTextBoxColumn1
      // 
      this.subjectNameDataGridViewTextBoxColumn1.DataPropertyName = "SubjectName";
      this.subjectNameDataGridViewTextBoxColumn1.HeaderText = "SubjectName";
      this.subjectNameDataGridViewTextBoxColumn1.Name = "subjectNameDataGridViewTextBoxColumn1";
      this.subjectNameDataGridViewTextBoxColumn1.ReadOnly = true;
      // 
      // trialIDDataGridViewTextBoxColumn1
      // 
      this.trialIDDataGridViewTextBoxColumn1.DataPropertyName = "TrialID";
      this.trialIDDataGridViewTextBoxColumn1.HeaderText = "TrialID";
      this.trialIDDataGridViewTextBoxColumn1.Name = "trialIDDataGridViewTextBoxColumn1";
      this.trialIDDataGridViewTextBoxColumn1.ReadOnly = true;
      // 
      // trialSequenceDataGridViewTextBoxColumn1
      // 
      this.trialSequenceDataGridViewTextBoxColumn1.DataPropertyName = "TrialSequence";
      this.trialSequenceDataGridViewTextBoxColumn1.HeaderText = "TrialSequence";
      this.trialSequenceDataGridViewTextBoxColumn1.Name = "trialSequenceDataGridViewTextBoxColumn1";
      this.trialSequenceDataGridViewTextBoxColumn1.ReadOnly = true;
      // 
      // countInTrialDataGridViewTextBoxColumn1
      // 
      this.countInTrialDataGridViewTextBoxColumn1.DataPropertyName = "CountInTrial";
      this.countInTrialDataGridViewTextBoxColumn1.HeaderText = "CountInTrial";
      this.countInTrialDataGridViewTextBoxColumn1.Name = "countInTrialDataGridViewTextBoxColumn1";
      this.countInTrialDataGridViewTextBoxColumn1.ReadOnly = true;
      // 
      // startTimeDataGridViewTextBoxColumn1
      // 
      this.startTimeDataGridViewTextBoxColumn1.DataPropertyName = "StartTime";
      this.startTimeDataGridViewTextBoxColumn1.HeaderText = "StartTime";
      this.startTimeDataGridViewTextBoxColumn1.Name = "startTimeDataGridViewTextBoxColumn1";
      this.startTimeDataGridViewTextBoxColumn1.ReadOnly = true;
      // 
      // lengthDataGridViewTextBoxColumn1
      // 
      this.lengthDataGridViewTextBoxColumn1.DataPropertyName = "Length";
      this.lengthDataGridViewTextBoxColumn1.HeaderText = "Length";
      this.lengthDataGridViewTextBoxColumn1.Name = "lengthDataGridViewTextBoxColumn1";
      this.lengthDataGridViewTextBoxColumn1.ReadOnly = true;
      // 
      // posXDataGridViewTextBoxColumn1
      // 
      this.posXDataGridViewTextBoxColumn1.DataPropertyName = "PosX";
      this.posXDataGridViewTextBoxColumn1.HeaderText = "PosX";
      this.posXDataGridViewTextBoxColumn1.Name = "posXDataGridViewTextBoxColumn1";
      this.posXDataGridViewTextBoxColumn1.ReadOnly = true;
      // 
      // posYDataGridViewTextBoxColumn1
      // 
      this.posYDataGridViewTextBoxColumn1.DataPropertyName = "PosY";
      this.posYDataGridViewTextBoxColumn1.HeaderText = "PosY";
      this.posYDataGridViewTextBoxColumn1.Name = "posYDataGridViewTextBoxColumn1";
      this.posYDataGridViewTextBoxColumn1.ReadOnly = true;
      // 
      // tosActions
      // 
      this.tosActions.DataBindings.Add(new System.Windows.Forms.Binding("Location", global::Ogama.Properties.Settings.Default, "FIXOptionsToolbarLocation", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.tosActions.Dock = System.Windows.Forms.DockStyle.None;
      this.tosActions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel2,
            this.btnCalculateFix,
            this.toolStripSeparator5,
            this.toolStripLabel3,
            this.btnSelectedSubject,
            this.btnAllSubjects,
            this.btnShowOptions,
            this.toolStripSeparator1,
            this.toolStripLabel1,
            this.btnDeleteFixations,
            this.btnEliminateData,
            this.btnShowHideAOI,
            this.btnShowHideTables,
            this.toolStripSeparator3,
            this.nudDriftXCorrection,
            this.nudDriftYCorrection,
            this.btnSaveDriftCorrection,
            this.toolStripSeparator6,
            this.toolStripLabel5,
            this.btnImport,
            this.btnExport});
      this.tosActions.Location = global::Ogama.Properties.Settings.Default.FIXOptionsToolbarLocation;
      this.tosActions.Name = "tosActions";
      this.tosActions.Size = new System.Drawing.Size(678, 26);
      this.tosActions.TabIndex = 1;
      this.tosActions.Text = "toolStrip2";
      // 
      // toolStripLabel2
      // 
      this.toolStripLabel2.Name = "toolStripLabel2";
      this.toolStripLabel2.Size = new System.Drawing.Size(105, 23);
      this.toolStripLabel2.Text = "Calculate Fixations";
      // 
      // btnCalculateFix
      // 
      this.btnCalculateFix.Image = global::Ogama.Properties.Resources.CalculatorHS;
      this.btnCalculateFix.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnCalculateFix.Name = "btnCalculateFix";
      this.btnCalculateFix.Size = new System.Drawing.Size(51, 23);
      this.btnCalculateFix.Text = "Start";
      this.btnCalculateFix.ToolTipText = "Start calculating the gaze and mouse fixations \r\nof selected subject for all tria" +
          "ls.";
      this.btnCalculateFix.Click += new System.EventHandler(this.btnCalculateFix_Click);
      // 
      // toolStripSeparator5
      // 
      this.toolStripSeparator5.Name = "toolStripSeparator5";
      this.toolStripSeparator5.Size = new System.Drawing.Size(6, 26);
      // 
      // toolStripLabel3
      // 
      this.toolStripLabel3.Name = "toolStripLabel3";
      this.toolStripLabel3.Size = new System.Drawing.Size(49, 23);
      this.toolStripLabel3.Text = "Options";
      // 
      // btnSelectedSubject
      // 
      this.btnSelectedSubject.Checked = true;
      this.btnSelectedSubject.CheckOnClick = true;
      this.btnSelectedSubject.CheckState = System.Windows.Forms.CheckState.Checked;
      this.btnSelectedSubject.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnSelectedSubject.Image = global::Ogama.Properties.Resources.user;
      this.btnSelectedSubject.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnSelectedSubject.Name = "btnSelectedSubject";
      this.btnSelectedSubject.Size = new System.Drawing.Size(23, 23);
      this.btnSelectedSubject.ToolTipText = "Calculate fixations only for selected subject.";
      this.btnSelectedSubject.Click += new System.EventHandler(this.btnSelectedSubject_Click);
      // 
      // btnAllSubjects
      // 
      this.btnAllSubjects.CheckOnClick = true;
      this.btnAllSubjects.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnAllSubjects.Image = global::Ogama.Properties.Resources.userAccounts;
      this.btnAllSubjects.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnAllSubjects.Name = "btnAllSubjects";
      this.btnAllSubjects.Size = new System.Drawing.Size(23, 23);
      this.btnAllSubjects.ToolTipText = "Calculate fixations for all subjects.";
      this.btnAllSubjects.Click += new System.EventHandler(this.btnAllSubjects_Click);
      // 
      // btnShowOptions
      // 
      this.btnShowOptions.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnShowOptions.Image = global::Ogama.Properties.Resources.otheroptions;
      this.btnShowOptions.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnShowOptions.Name = "btnShowOptions";
      this.btnShowOptions.Size = new System.Drawing.Size(23, 23);
      this.btnShowOptions.Text = "Show options for fixation calculation";
      this.btnShowOptions.ToolTipText = "Show options for fixation calculation";
      this.btnShowOptions.Click += new System.EventHandler(this.btnShowOptions_Click);
      // 
      // toolStripSeparator1
      // 
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new System.Drawing.Size(6, 26);
      // 
      // toolStripLabel1
      // 
      this.toolStripLabel1.Name = "toolStripLabel1";
      this.toolStripLabel1.Size = new System.Drawing.Size(49, 23);
      this.toolStripLabel1.Text = "Specials";
      // 
      // btnDeleteFixations
      // 
      this.btnDeleteFixations.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnDeleteFixations.Image = global::Ogama.Properties.Resources.DeleteHS;
      this.btnDeleteFixations.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnDeleteFixations.Name = "btnDeleteFixations";
      this.btnDeleteFixations.Size = new System.Drawing.Size(23, 23);
      this.btnDeleteFixations.ToolTipText = "Delete all fixations for given subject.";
      this.btnDeleteFixations.Click += new System.EventHandler(this.btnDeleteFixations_Click);
      // 
      // btnEliminateData
      // 
      this.btnEliminateData.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnEliminateData.Image = global::Ogama.Properties.Resources.RPLBlink;
      this.btnEliminateData.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnEliminateData.Name = "btnEliminateData";
      this.btnEliminateData.Size = new System.Drawing.Size(23, 23);
      this.btnEliminateData.ToolTipText = "Eliminate data of this trial from statistical analysis.";
      this.btnEliminateData.Click += new System.EventHandler(this.btnEliminateData_Click);
      // 
      // btnShowHideAOI
      // 
      this.btnShowHideAOI.CheckOnClick = true;
      this.btnShowHideAOI.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnShowHideAOI.Image = global::Ogama.Properties.Resources.AOILogo;
      this.btnShowHideAOI.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnShowHideAOI.Name = "btnShowHideAOI";
      this.btnShowHideAOI.Size = new System.Drawing.Size(23, 23);
      this.btnShowHideAOI.ToolTipText = "Shows or hides the underlying AOI.";
      this.btnShowHideAOI.Click += new System.EventHandler(this.btnShowHideAOI_Click);
      // 
      // btnShowHideTables
      // 
      this.btnShowHideTables.Checked = true;
      this.btnShowHideTables.CheckOnClick = true;
      this.btnShowHideTables.CheckState = System.Windows.Forms.CheckState.Checked;
      this.btnShowHideTables.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnShowHideTables.Image = global::Ogama.Properties.Resources.TableHS;
      this.btnShowHideTables.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnShowHideTables.Name = "btnShowHideTables";
      this.btnShowHideTables.Size = new System.Drawing.Size(23, 23);
      this.btnShowHideTables.Text = "Shows or hides the fixation data grid views ...";
      this.btnShowHideTables.ToolTipText = "Uncheck to hide the fixation tables.";
      this.btnShowHideTables.Click += new System.EventHandler(this.btnShowHideTables_Click);
      // 
      // toolStripSeparator3
      // 
      this.toolStripSeparator3.Name = "toolStripSeparator3";
      this.toolStripSeparator3.Size = new System.Drawing.Size(6, 26);
      // 
      // nudDriftXCorrection
      // 
      this.nudDriftXCorrection.DecimalPlaces = 0;
      this.nudDriftXCorrection.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
      this.nudDriftXCorrection.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
      this.nudDriftXCorrection.Minimum = new decimal(new int[] {
            500,
            0,
            0,
            -2147483648});
      this.nudDriftXCorrection.Name = "nudDriftXCorrection";
      this.nudDriftXCorrection.Size = new System.Drawing.Size(41, 23);
      this.nudDriftXCorrection.Text = "0";
      this.nudDriftXCorrection.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nudDriftXCorrection.ValueChanged += new System.EventHandler(this.nudDriftCorrection_ValueChanged);
      // 
      // nudDriftYCorrection
      // 
      this.nudDriftYCorrection.DecimalPlaces = 0;
      this.nudDriftYCorrection.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
      this.nudDriftYCorrection.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
      this.nudDriftYCorrection.Minimum = new decimal(new int[] {
            500,
            0,
            0,
            -2147483648});
      this.nudDriftYCorrection.Name = "nudDriftYCorrection";
      this.nudDriftYCorrection.Size = new System.Drawing.Size(41, 23);
      this.nudDriftYCorrection.Text = "0";
      this.nudDriftYCorrection.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nudDriftYCorrection.ValueChanged += new System.EventHandler(this.nudDriftCorrection_ValueChanged);
      // 
      // btnSaveDriftCorrection
      // 
      this.btnSaveDriftCorrection.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnSaveDriftCorrection.Image = global::Ogama.Properties.Resources.saveHS;
      this.btnSaveDriftCorrection.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnSaveDriftCorrection.Name = "btnSaveDriftCorrection";
      this.btnSaveDriftCorrection.Size = new System.Drawing.Size(23, 23);
      this.btnSaveDriftCorrection.Text = "Save drift correction to database.";
      this.btnSaveDriftCorrection.Click += new System.EventHandler(this.btnSaveDriftCorrection_Click);
      // 
      // toolStripSeparator6
      // 
      this.toolStripSeparator6.Name = "toolStripSeparator6";
      this.toolStripSeparator6.Size = new System.Drawing.Size(6, 26);
      // 
      // toolStripLabel5
      // 
      this.toolStripLabel5.Name = "toolStripLabel5";
      this.toolStripLabel5.Size = new System.Drawing.Size(76, 23);
      this.toolStripLabel5.Text = "ImportExport";
      // 
      // btnImport
      // 
      this.btnImport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnImport.Image = global::Ogama.Properties.Resources.MagicWand;
      this.btnImport.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnImport.Name = "btnImport";
      this.btnImport.Size = new System.Drawing.Size(23, 23);
      this.btnImport.Text = "Import Fixations";
      this.btnImport.ToolTipText = "Imports ascii file with fixation into OGAMAs table.";
      this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
      // 
      // btnExport
      // 
      this.btnExport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnExport.Image = global::Ogama.Properties.Resources.ExportData;
      this.btnExport.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnExport.Name = "btnExport";
      this.btnExport.Size = new System.Drawing.Size(23, 23);
      this.btnExport.ToolTipText = "Export fixation table to ascii file.";
      this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
      // 
      // toolStripContainer1
      // 
      // 
      // toolStripContainer1.ContentPanel
      // 
      this.toolStripContainer1.ContentPanel.Controls.Add(this.spcPictureTools);
      this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(1096, 372);
      this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
      this.toolStripContainer1.Name = "toolStripContainer1";
      this.toolStripContainer1.Size = new System.Drawing.Size(1096, 424);
      this.toolStripContainer1.TabIndex = 3;
      this.toolStripContainer1.Text = "toolStripContainer1";
      // 
      // toolStripContainer1.TopToolStripPanel
      // 
      this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.tosMouseDisplay);
      this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.tosTrialSelection);
      this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.tosActions);
      this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.tosGazeDisplay);
      // 
      // tosMouseDisplay
      // 
      this.tosMouseDisplay.DataBindings.Add(new System.Windows.Forms.Binding("Location", global::Ogama.Properties.Settings.Default, "FIXMouseToolbarLocation", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.tosMouseDisplay.Dock = System.Windows.Forms.DockStyle.None;
      this.tosMouseDisplay.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton11,
            this.toolStripSeparator12,
            this.toolStripLabel7,
            this.cbbMouseDisplayMode,
            this.btnMouseConnections,
            this.btnMouseNumbers,
            this.toolStripSeparator13,
            this.btnMousePenStyle,
            this.nudMouseFixDiameterDiv});
      this.tosMouseDisplay.Location = global::Ogama.Properties.Settings.Default.FIXMouseToolbarLocation;
      this.tosMouseDisplay.Name = "tosMouseDisplay";
      this.tosMouseDisplay.Size = new System.Drawing.Size(368, 26);
      this.tosMouseDisplay.TabIndex = 4;
      // 
      // toolStripButton11
      // 
      this.toolStripButton11.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.toolStripButton11.Enabled = false;
      this.toolStripButton11.Image = global::Ogama.Properties.Resources.Mouse;
      this.toolStripButton11.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripButton11.Name = "toolStripButton11";
      this.toolStripButton11.Size = new System.Drawing.Size(23, 23);
      this.toolStripButton11.ToolTipText = "Mouse display modes.";
      // 
      // toolStripSeparator12
      // 
      this.toolStripSeparator12.Name = "toolStripSeparator12";
      this.toolStripSeparator12.Size = new System.Drawing.Size(6, 26);
      // 
      // toolStripLabel7
      // 
      this.toolStripLabel7.Name = "toolStripLabel7";
      this.toolStripLabel7.Size = new System.Drawing.Size(79, 23);
      this.toolStripLabel7.Text = "Display mode";
      // 
      // cbbMouseDisplayMode
      // 
      this.cbbMouseDisplayMode.Name = "cbbMouseDisplayMode";
      this.cbbMouseDisplayMode.Size = new System.Drawing.Size(121, 26);
      this.cbbMouseDisplayMode.Text = global::Ogama.Properties.Settings.Default.MouseFixationsDrawingMode;
      this.cbbMouseDisplayMode.ToolTipText = "Select the display \r\nmode for the mouse fixations.";
      // 
      // btnMouseConnections
      // 
      this.btnMouseConnections.Checked = global::Ogama.Properties.Settings.Default.MouseConnections;
      this.btnMouseConnections.CheckOnClick = true;
      this.btnMouseConnections.CheckState = System.Windows.Forms.CheckState.Checked;
      this.btnMouseConnections.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnMouseConnections.Image = global::Ogama.Properties.Resources.RPLFixCon;
      this.btnMouseConnections.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnMouseConnections.Name = "btnMouseConnections";
      this.btnMouseConnections.Size = new System.Drawing.Size(23, 23);
      this.btnMouseConnections.Text = "Display mouse fixation connections.";
      this.btnMouseConnections.Click += new System.EventHandler(this.btnMouseConnections_Click);
      // 
      // btnMouseNumbers
      // 
      this.btnMouseNumbers.Checked = global::Ogama.Properties.Settings.Default.MouseNumbers;
      this.btnMouseNumbers.CheckOnClick = true;
      this.btnMouseNumbers.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnMouseNumbers.Image = global::Ogama.Properties.Resources.ATMFirst;
      this.btnMouseNumbers.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnMouseNumbers.Name = "btnMouseNumbers";
      this.btnMouseNumbers.Size = new System.Drawing.Size(23, 23);
      this.btnMouseNumbers.Text = "Display mouse fixation numbers.";
      this.btnMouseNumbers.Click += new System.EventHandler(this.btnMouseNumbers_Click);
      // 
      // toolStripSeparator13
      // 
      this.toolStripSeparator13.Name = "toolStripSeparator13";
      this.toolStripSeparator13.Size = new System.Drawing.Size(6, 26);
      // 
      // btnMousePenStyle
      // 
      this.btnMousePenStyle.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnMousePenStyle.Image = global::Ogama.Properties.Resources.Color_fontHS;
      this.btnMousePenStyle.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnMousePenStyle.Name = "btnMousePenStyle";
      this.btnMousePenStyle.Size = new System.Drawing.Size(23, 23);
      this.btnMousePenStyle.ToolTipText = "Modify mouse fixation pen style.";
      this.btnMousePenStyle.Click += new System.EventHandler(this.btnMousePenStyle_Click);
      // 
      // nudMouseFixDiameterDiv
      // 
      this.nudMouseFixDiameterDiv.DecimalPlaces = 1;
      this.nudMouseFixDiameterDiv.Increment = new decimal(new int[] {
            2,
            0,
            0,
            65536});
      this.nudMouseFixDiameterDiv.Maximum = new decimal(new int[] {
            100,
            0,
            0,
            0});
      this.nudMouseFixDiameterDiv.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
      this.nudMouseFixDiameterDiv.Name = "nudMouseFixDiameterDiv";
      this.nudMouseFixDiameterDiv.Size = new System.Drawing.Size(50, 23);
      this.nudMouseFixDiameterDiv.Text = "2,0";
      this.nudMouseFixDiameterDiv.ToolTipText = "Increase this value to reduce \r\nmouse fixation diameters.";
      this.nudMouseFixDiameterDiv.Value = new decimal(new int[] {
            20,
            0,
            0,
            65536});
      this.nudMouseFixDiameterDiv.ValueChanged += new System.EventHandler(this.nudMouseFixDiameterDiv_ValueChanged);
      // 
      // tosTrialSelection
      // 
      this.tosTrialSelection.DataBindings.Add(new System.Windows.Forms.Binding("Location", global::Ogama.Properties.Settings.Default, "FIXTrialToolbarLocation", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.tosTrialSelection.Dock = System.Windows.Forms.DockStyle.None;
      this.tosTrialSelection.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cbbSubject,
            this.cbbTrial,
            this.btnEye,
            this.btnMouse,
            this.toolStripSeparator8,
            this.btnHelp});
      this.tosTrialSelection.Location = global::Ogama.Properties.Settings.Default.FIXTrialToolbarLocation;
      this.tosTrialSelection.Name = "tosTrialSelection";
      this.tosTrialSelection.Size = new System.Drawing.Size(412, 25);
      this.tosTrialSelection.TabIndex = 0;
      // 
      // cbbSubject
      // 
      this.cbbSubject.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbbSubject.Name = "cbbSubject";
      this.cbbSubject.Size = new System.Drawing.Size(121, 25);
      this.cbbSubject.ToolTipText = "Select subject for calculation and display.";
      // 
      // cbbTrial
      // 
      this.cbbTrial.AutoSize = false;
      this.cbbTrial.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbbTrial.Name = "cbbTrial";
      this.cbbTrial.Size = new System.Drawing.Size(200, 23);
      this.cbbTrial.ToolTipText = "Select the trial to display.";
      // 
      // btnEye
      // 
      this.btnEye.Checked = true;
      this.btnEye.CheckOnClick = true;
      this.btnEye.CheckState = System.Windows.Forms.CheckState.Checked;
      this.btnEye.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnEye.Image = global::Ogama.Properties.Resources.Eye;
      this.btnEye.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnEye.Name = "btnEye";
      this.btnEye.Size = new System.Drawing.Size(23, 22);
      this.btnEye.Text = "Check to show gaze fixations.";
      this.btnEye.Click += new System.EventHandler(this.btnEye_Click);
      // 
      // btnMouse
      // 
      this.btnMouse.CheckOnClick = true;
      this.btnMouse.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnMouse.Image = global::Ogama.Properties.Resources.Mouse;
      this.btnMouse.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnMouse.Name = "btnMouse";
      this.btnMouse.Size = new System.Drawing.Size(23, 22);
      this.btnMouse.Text = "Check to show mouse fixations.";
      this.btnMouse.Click += new System.EventHandler(this.btnMouse_Click);
      // 
      // toolStripSeparator8
      // 
      this.toolStripSeparator8.Name = "toolStripSeparator8";
      this.toolStripSeparator8.Size = new System.Drawing.Size(6, 25);
      // 
      // btnHelp
      // 
      this.btnHelp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnHelp.Image = global::Ogama.Properties.Resources.HelpBmp;
      this.btnHelp.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnHelp.Name = "btnHelp";
      this.btnHelp.Size = new System.Drawing.Size(23, 22);
      this.btnHelp.ToolTipText = "Display help for this interface.";
      // 
      // tosGazeDisplay
      // 
      this.tosGazeDisplay.DataBindings.Add(new System.Windows.Forms.Binding("Location", global::Ogama.Properties.Settings.Default, "FIXGazeToolbarLocation", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.tosGazeDisplay.Dock = System.Windows.Forms.DockStyle.None;
      this.tosGazeDisplay.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton3,
            this.toolStripSeparator7,
            this.toolStripLabel6,
            this.cbbGazeDisplayMode,
            this.btnGazeConnections,
            this.btnGazeNumbers,
            this.toolStripSeparator9,
            this.btnGazePenStyle,
            this.nudGazeFixDiameterDiv});
      this.tosGazeDisplay.Location = global::Ogama.Properties.Settings.Default.FIXGazeToolbarLocation;
      this.tosGazeDisplay.Name = "tosGazeDisplay";
      this.tosGazeDisplay.Size = new System.Drawing.Size(368, 26);
      this.tosGazeDisplay.TabIndex = 3;
      // 
      // toolStripButton3
      // 
      this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.toolStripButton3.Enabled = false;
      this.toolStripButton3.Image = global::Ogama.Properties.Resources.Eye;
      this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripButton3.Name = "toolStripButton3";
      this.toolStripButton3.Size = new System.Drawing.Size(23, 23);
      this.toolStripButton3.ToolTipText = "Gaze display modes.";
      // 
      // toolStripSeparator7
      // 
      this.toolStripSeparator7.Name = "toolStripSeparator7";
      this.toolStripSeparator7.Size = new System.Drawing.Size(6, 26);
      // 
      // toolStripLabel6
      // 
      this.toolStripLabel6.Name = "toolStripLabel6";
      this.toolStripLabel6.Size = new System.Drawing.Size(79, 23);
      this.toolStripLabel6.Text = "Display mode";
      // 
      // cbbGazeDisplayMode
      // 
      this.cbbGazeDisplayMode.Name = "cbbGazeDisplayMode";
      this.cbbGazeDisplayMode.Size = new System.Drawing.Size(121, 26);
      this.cbbGazeDisplayMode.Text = global::Ogama.Properties.Settings.Default.GazeFixationsDrawingMode;
      this.cbbGazeDisplayMode.ToolTipText = "Select the display \r\nmode for the gaze fixations.";
      // 
      // btnGazeConnections
      // 
      this.btnGazeConnections.Checked = global::Ogama.Properties.Settings.Default.GazeConnections;
      this.btnGazeConnections.CheckOnClick = true;
      this.btnGazeConnections.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnGazeConnections.Image = global::Ogama.Properties.Resources.RPLFixCon;
      this.btnGazeConnections.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnGazeConnections.Name = "btnGazeConnections";
      this.btnGazeConnections.Size = new System.Drawing.Size(23, 23);
      this.btnGazeConnections.Text = "Display gaze fixation connections.";
      this.btnGazeConnections.Click += new System.EventHandler(this.btnGazeConnections_Click);
      // 
      // btnGazeNumbers
      // 
      this.btnGazeNumbers.Checked = global::Ogama.Properties.Settings.Default.GazeNumbers;
      this.btnGazeNumbers.CheckOnClick = true;
      this.btnGazeNumbers.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnGazeNumbers.Image = global::Ogama.Properties.Resources.ATMFirst;
      this.btnGazeNumbers.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnGazeNumbers.Name = "btnGazeNumbers";
      this.btnGazeNumbers.Size = new System.Drawing.Size(23, 23);
      this.btnGazeNumbers.Text = "Display gaze fixation numbers.";
      this.btnGazeNumbers.Click += new System.EventHandler(this.btnGazeNumbers_Click);
      // 
      // toolStripSeparator9
      // 
      this.toolStripSeparator9.Name = "toolStripSeparator9";
      this.toolStripSeparator9.Size = new System.Drawing.Size(6, 26);
      // 
      // btnGazePenStyle
      // 
      this.btnGazePenStyle.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnGazePenStyle.Image = global::Ogama.Properties.Resources.Color_fontHS;
      this.btnGazePenStyle.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnGazePenStyle.Name = "btnGazePenStyle";
      this.btnGazePenStyle.Size = new System.Drawing.Size(23, 23);
      this.btnGazePenStyle.ToolTipText = "Modify gaze fixation pen style.";
      this.btnGazePenStyle.Click += new System.EventHandler(this.btnGazePenStyle_Click);
      // 
      // nudGazeFixDiameterDiv
      // 
      this.nudGazeFixDiameterDiv.DecimalPlaces = 1;
      this.nudGazeFixDiameterDiv.Increment = new decimal(new int[] {
            2,
            0,
            0,
            65536});
      this.nudGazeFixDiameterDiv.Maximum = new decimal(new int[] {
            100,
            0,
            0,
            0});
      this.nudGazeFixDiameterDiv.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
      this.nudGazeFixDiameterDiv.Name = "nudGazeFixDiameterDiv";
      this.nudGazeFixDiameterDiv.Size = new System.Drawing.Size(50, 23);
      this.nudGazeFixDiameterDiv.Text = "2,0";
      this.nudGazeFixDiameterDiv.ToolTipText = "Increase this value to reduce \r\ngaze fixation diameters.";
      this.nudGazeFixDiameterDiv.Value = new decimal(new int[] {
            20,
            0,
            0,
            65536});
      this.nudGazeFixDiameterDiv.ValueChanged += new System.EventHandler(this.nudGazeFixDiameterDiv_ValueChanged);
      // 
      // bgwCalcFixationsForAllSubjects
      // 
      this.bgwCalcFixationsForAllSubjects.WorkerReportsProgress = true;
      this.bgwCalcFixationsForAllSubjects.WorkerSupportsCancellation = true;
      this.bgwCalcFixationsForAllSubjects.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwCalcFixationsForAllSubjects_DoWork);
      this.bgwCalcFixationsForAllSubjects.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwCalcFixations_RunWorkerCompleted);
      this.bgwCalcFixationsForAllSubjects.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgwCalcFixations_ProgressChanged);
      // 
      // FixationsModule
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1096, 424);
      this.Controls.Add(this.toolStripContainer1);
      this.DataBindings.Add(new System.Windows.Forms.Binding("Location", global::Ogama.Properties.Settings.Default, "FixationsModuleLocation", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.KeyPreview = true;
      this.Location = global::Ogama.Properties.Settings.Default.FixationsModuleLocation;
      this.Logo = global::Ogama.Properties.Resources.FixationsLogo;
      this.Name = "FixationsModule";
      this.Text = "Fixations Module";
      this.Load += new System.EventHandler(this.FixationsModule_Load);
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmFixations_FormClosing);
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
      this.spcPictureTools.Panel1.ResumeLayout(false);
      this.spcPictureTools.Panel2.ResumeLayout(false);
      this.spcPictureTools.ResumeLayout(false);
      this.spcPictureSlider.Panel1.ResumeLayout(false);
      this.spcPictureSlider.Panel2.ResumeLayout(false);
      this.spcPictureSlider.Panel2.PerformLayout();
      this.spcPictureSlider.ResumeLayout(false);
      this.pnlCanvas.ResumeLayout(false);
      this.pnlPicture.ResumeLayout(false);
      this.toolStrip1.ResumeLayout(false);
      this.toolStrip1.PerformLayout();
      this.spcGazeMouseTables.Panel1.ResumeLayout(false);
      this.spcGazeMouseTables.Panel2.ResumeLayout(false);
      this.spcGazeMouseTables.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.dgvGazeFixations)).EndInit();
      this.cmnuDGV.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.dgvMouseFixations)).EndInit();
      this.tosActions.ResumeLayout(false);
      this.tosActions.PerformLayout();
      this.toolStripContainer1.ContentPanel.ResumeLayout(false);
      this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
      this.toolStripContainer1.TopToolStripPanel.PerformLayout();
      this.toolStripContainer1.ResumeLayout(false);
      this.toolStripContainer1.PerformLayout();
      this.tosMouseDisplay.ResumeLayout(false);
      this.tosMouseDisplay.PerformLayout();
      this.tosTrialSelection.ResumeLayout(false);
      this.tosTrialSelection.PerformLayout();
      this.tosGazeDisplay.ResumeLayout(false);
      this.tosGazeDisplay.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.SplitContainer spcPictureTools;
    private System.Windows.Forms.DataGridView dgvGazeFixations;
    private System.Windows.Forms.ToolTip toolTip1;
    private PictureWithFixations fixationsPicture;
    private System.Windows.Forms.ToolStripContainer toolStripContainer1;
    private System.Windows.Forms.ToolStrip tosTrialSelection;
    private System.Windows.Forms.ToolStripComboBox cbbSubject;
    private System.Windows.Forms.ToolStripButton btnHelp;
    private System.ComponentModel.BackgroundWorker bgwCalcFixationsForAllSubjects;
    private System.Windows.Forms.ContextMenuStrip cmnuDGV;
    private System.Windows.Forms.ToolStripMenuItem cmnuSelectAll;
    private System.Windows.Forms.ToolStripMenuItem cmnuCopyToClipboard;
    private System.Windows.Forms.ToolStripComboBox cbbTrial;
    private System.Windows.Forms.ToolStrip tosActions;
    private System.Windows.Forms.ToolStripLabel toolStripLabel2;
    private System.Windows.Forms.ToolStripButton btnCalculateFix;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
    private System.Windows.Forms.ToolStripButton btnSelectedSubject;
    private System.Windows.Forms.ToolStripButton btnAllSubjects;
    private System.Windows.Forms.ToolStripButton btnShowOptions;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    private System.Windows.Forms.ToolStripLabel toolStripLabel1;
    private System.Windows.Forms.ToolStripButton btnDeleteFixations;
    private System.Windows.Forms.ToolStripButton btnExport;
    private System.Windows.Forms.ToolStripLabel toolStripLabel3;
    private System.Windows.Forms.ToolStripButton btnImport;
    private System.Windows.Forms.ToolStripButton btnEliminateData;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
    private System.Windows.Forms.ToolStripLabel toolStripLabel5;
    private System.Windows.Forms.Panel pnlCanvas;
    private System.Windows.Forms.SplitContainer spcGazeMouseTables;
    private System.Windows.Forms.DataGridView dgvMouseFixations;
    private System.Windows.Forms.ToolStripButton btnEye;
    private System.Windows.Forms.ToolStripButton btnMouse;
    private System.Windows.Forms.ToolStrip tosGazeDisplay;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
    private System.Windows.Forms.ToolStripLabel toolStripLabel6;
    private System.Windows.Forms.ToolStripComboBox cbbGazeDisplayMode;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
    private System.Windows.Forms.ToolStripButton btnGazePenStyle;
    private OgamaControls.ToolStripNumericUpDown nudGazeFixDiameterDiv;
    private System.Windows.Forms.ToolStrip tosMouseDisplay;
    private System.Windows.Forms.ToolStripButton toolStripButton11;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator12;
    private System.Windows.Forms.ToolStripLabel toolStripLabel7;
    private System.Windows.Forms.ToolStripComboBox cbbMouseDisplayMode;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator13;
    private System.Windows.Forms.ToolStripButton btnMousePenStyle;
    private OgamaControls.ToolStripNumericUpDown nudMouseFixDiameterDiv;
    private System.Windows.Forms.ToolStripButton toolStripButton3;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
    private System.Windows.Forms.ToolStripButton btnShowHideTables;
    private System.Windows.Forms.SplitContainer spcPictureSlider;
    private System.Windows.Forms.ToolStripButton btnShowHideAOI;
    private System.Windows.Forms.Panel pnlPicture;
    private System.Windows.Forms.ToolStrip toolStrip1;
    private TrialTimeLine trialTimeLine;
    private System.Windows.Forms.ToolStripButton btnSeekNextSlide;
    private System.Windows.Forms.ToolStripButton btnSeekPreviousSlide;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    private System.Windows.Forms.ToolStripButton btnMouseConnections;
    private System.Windows.Forms.ToolStripButton btnMouseNumbers;
    private System.Windows.Forms.ToolStripButton btnGazeConnections;
    private System.Windows.Forms.ToolStripButton btnGazeNumbers;
    private System.Windows.Forms.DataGridViewTextBoxColumn iDDataGridViewTextBoxColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn subjectNameDataGridViewTextBoxColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn trialIDDataGridViewTextBoxColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn trialSequenceDataGridViewTextBoxColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn countInTrialDataGridViewTextBoxColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn startTimeDataGridViewTextBoxColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn lengthDataGridViewTextBoxColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn posXDataGridViewTextBoxColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn posYDataGridViewTextBoxColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn iDDataGridViewTextBoxColumn1;
    private System.Windows.Forms.DataGridViewTextBoxColumn subjectNameDataGridViewTextBoxColumn1;
    private System.Windows.Forms.DataGridViewTextBoxColumn trialIDDataGridViewTextBoxColumn1;
    private System.Windows.Forms.DataGridViewTextBoxColumn trialSequenceDataGridViewTextBoxColumn1;
    private System.Windows.Forms.DataGridViewTextBoxColumn countInTrialDataGridViewTextBoxColumn1;
    private System.Windows.Forms.DataGridViewTextBoxColumn startTimeDataGridViewTextBoxColumn1;
    private System.Windows.Forms.DataGridViewTextBoxColumn lengthDataGridViewTextBoxColumn1;
    private System.Windows.Forms.DataGridViewTextBoxColumn posXDataGridViewTextBoxColumn1;
    private System.Windows.Forms.DataGridViewTextBoxColumn posYDataGridViewTextBoxColumn1;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
    private System.Windows.Forms.ToolStripButton btnSaveDriftCorrection;
    private OgamaControls.ToolStripNumericUpDown nudDriftXCorrection;
    private OgamaControls.ToolStripNumericUpDown nudDriftYCorrection;
    private OgamaControls.ToolStripTrackBar trbZoom;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
  }
}