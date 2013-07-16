namespace Ogama.Modules.Scanpath
{
  using Ogama.Modules.Common.Controls;

  partial class ScanpathsModule
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
      this.CustomDispose();
      if (disposing && (this.components != null))
      {
        this.components.Dispose();
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScanpathsModule));
      this.pnlCanvas = new System.Windows.Forms.Panel();
      this.pnlPicture = new System.Windows.Forms.Panel();
      this.scanpathsPicture = new ScanpathsPicture();
      this.tosTimeLine = new System.Windows.Forms.ToolStrip();
      this.btnStart = new System.Windows.Forms.ToolStripButton();
      this.btnStop = new System.Windows.Forms.ToolStripButton();
      this.btnRewind = new System.Windows.Forms.ToolStripButton();
      this.trialTimeLine = new TrialTimeLine(this.components);
      this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
      this.trbZoom = new OgamaControls.ToolStripTrackBar();
      this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
      this.trvSubjects = new OgamaControls.CheckboxTreeView(this.components);
      this.imlTreeViewSubjects = new System.Windows.Forms.ImageList(this.components);
      this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
      this.spcListPic = new System.Windows.Forms.SplitContainer();
      this.spcPicTable = new System.Windows.Forms.SplitContainer();
      this.tscPictureTimeline = new System.Windows.Forms.ToolStripContainer();
      this.spcTables = new System.Windows.Forms.SplitContainer();
      this.grbLociSimilarity = new System.Windows.Forms.GroupBox();
      this.dgvLociSimilarity = new System.Windows.Forms.DataGridView();
      this.grpSequenceSimilarity = new System.Windows.Forms.GroupBox();
      this.dgvSequenceSimilarity = new System.Windows.Forms.DataGridView();
      this.tosTrialSelection = new System.Windows.Forms.ToolStrip();
      this.cbbTrial = new System.Windows.Forms.ToolStripComboBox();
      this.btnEye = new System.Windows.Forms.ToolStripButton();
      this.btnMouse = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
      this.btnLevenstheinToolbar = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.btnTimeLine = new System.Windows.Forms.ToolStripButton();
      this.nudTimeSpan = new OgamaControls.ToolStripNumericUpDown();
      this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
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
      this.tosLevenshtein = new System.Windows.Forms.ToolStrip();
      this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
      this.btnGrid = new System.Windows.Forms.ToolStripButton();
      this.nudGridFactor = new OgamaControls.ToolStripNumericUpDown();
      this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
      this.btnAOI = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
      this.btnRemoveSubsequentHits = new System.Windows.Forms.ToolStripButton();
      this.btnGroupLevenshteinOutput = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
      this.btnLociSimilarity = new System.Windows.Forms.ToolStripButton();
      this.btnSequenceSimilarity = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
      this.btnExport = new System.Windows.Forms.ToolStripButton();
      this.sfdSimiliarity = new System.Windows.Forms.SaveFileDialog();
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
      this.pnlCanvas.SuspendLayout();
      this.pnlPicture.SuspendLayout();
      this.tosTimeLine.SuspendLayout();
      this.toolStripContainer1.ContentPanel.SuspendLayout();
      this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
      this.toolStripContainer1.SuspendLayout();
      this.spcListPic.Panel1.SuspendLayout();
      this.spcListPic.Panel2.SuspendLayout();
      this.spcListPic.SuspendLayout();
      this.spcPicTable.Panel1.SuspendLayout();
      this.spcPicTable.Panel2.SuspendLayout();
      this.spcPicTable.SuspendLayout();
      this.tscPictureTimeline.BottomToolStripPanel.SuspendLayout();
      this.tscPictureTimeline.ContentPanel.SuspendLayout();
      this.tscPictureTimeline.SuspendLayout();
      this.spcTables.Panel1.SuspendLayout();
      this.spcTables.Panel2.SuspendLayout();
      this.spcTables.SuspendLayout();
      this.grbLociSimilarity.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.dgvLociSimilarity)).BeginInit();
      this.grpSequenceSimilarity.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.dgvSequenceSimilarity)).BeginInit();
      this.tosTrialSelection.SuspendLayout();
      this.tosGazeDisplay.SuspendLayout();
      this.tosMouseDisplay.SuspendLayout();
      this.tosLevenshtein.SuspendLayout();
      this.SuspendLayout();
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
      this.pnlCanvas.Size = new System.Drawing.Size(688, 207);
      this.pnlCanvas.TabIndex = 3;
      // 
      // pnlPicture
      // 
      this.pnlPicture.Controls.Add(this.scanpathsPicture);
      this.pnlPicture.Location = new System.Drawing.Point(289, 0);
      this.pnlPicture.Margin = new System.Windows.Forms.Padding(0);
      this.pnlPicture.Name = "pnlPicture";
      this.pnlPicture.Size = new System.Drawing.Size(300, 200);
      this.pnlPicture.TabIndex = 0;
      // 
      // scanpathsPicture
      // 
      this.scanpathsPicture.AnimationInterval = 100;
      this.scanpathsPicture.BackColor = System.Drawing.Color.Black;
      this.scanpathsPicture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
      this.scanpathsPicture.GridBasis = GridBase.Rectangular;
      this.scanpathsPicture.GridFactor = 2;
      this.scanpathsPicture.IgnoreSubsequentFixations = false;
      this.scanpathsPicture.InvalidateInterval = 500;
      this.scanpathsPicture.Location = new System.Drawing.Point(0, 0);
      this.scanpathsPicture.Margin = new System.Windows.Forms.Padding(0);
      this.scanpathsPicture.Name = "scanpathsPicture";
      this.scanpathsPicture.Size = new System.Drawing.Size(300, 200);
      this.scanpathsPicture.TabIndex = 0;
      this.scanpathsPicture.TabStop = false;
      this.scanpathsPicture.ZoomFactor = 0F;
      // 
      // tosTimeLine
      // 
      this.tosTimeLine.Dock = System.Windows.Forms.DockStyle.None;
      this.tosTimeLine.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
      this.tosTimeLine.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnStart,
            this.btnStop,
            this.btnRewind,
            this.trialTimeLine,
            this.toolStripSeparator10,
            this.trbZoom});
      this.tosTimeLine.Location = new System.Drawing.Point(3, 0);
      this.tosTimeLine.Name = "tosTimeLine";
      this.tosTimeLine.Size = new System.Drawing.Size(476, 26);
      this.tosTimeLine.TabIndex = 3;
      this.tosTimeLine.Text = "toolStrip5";
      // 
      // btnStart
      // 
      this.btnStart.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnStart.Image = ((System.Drawing.Image)(resources.GetObject("btnStart.Image")));
      this.btnStart.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnStart.Name = "btnStart";
      this.btnStart.Size = new System.Drawing.Size(23, 23);
      this.btnStart.ToolTipText = "Play trial";
      this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
      // 
      // btnStop
      // 
      this.btnStop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnStop.Enabled = false;
      this.btnStop.Image = ((System.Drawing.Image)(resources.GetObject("btnStop.Image")));
      this.btnStop.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnStop.Name = "btnStop";
      this.btnStop.Size = new System.Drawing.Size(23, 23);
      this.btnStop.ToolTipText = "Stop trial";
      this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
      // 
      // btnRewind
      // 
      this.btnRewind.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnRewind.Image = ((System.Drawing.Image)(resources.GetObject("btnRewind.Image")));
      this.btnRewind.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnRewind.Name = "btnRewind";
      this.btnRewind.Size = new System.Drawing.Size(23, 23);
      this.btnRewind.ToolTipText = "Rewind trial";
      this.btnRewind.Click += new System.EventHandler(this.btnRewind_Click);
      // 
      // trialTimeLine
      // 
      this.trialTimeLine.Duration = 0;
      this.trialTimeLine.Name = "trialTimeLine";
      this.trialTimeLine.Size = new System.Drawing.Size(265, 23);
      this.trialTimeLine.SectionStartValueChanged += new OgamaControls.TimeLine.PositionValueChangedEventHandler(this.TrialTimeLine_SectionStartValueChanged);
      this.trialTimeLine.SectionEndValueChanged += new OgamaControls.TimeLine.PositionValueChangedEventHandler(this.TrialTimeLine_SectionEndValueChanged);
      this.trialTimeLine.CaretValueChanged += new OgamaControls.TimeLine.PositionValueChangedEventHandler(this.TrialTimeLine_CaretValueChanged);
      // 
      // toolStripSeparator10
      // 
      this.toolStripSeparator10.Name = "toolStripSeparator10";
      this.toolStripSeparator10.Size = new System.Drawing.Size(6, 26);
      // 
      // trbZoom
      // 
      this.trbZoom.Maximum = 100;
      this.trbZoom.Minimum = 1;
      this.trbZoom.Name = "trbZoom";
      this.trbZoom.Size = new System.Drawing.Size(104, 23);
      this.trbZoom.TickFrequency = 1;
      this.trbZoom.TickStyle = System.Windows.Forms.TickStyle.None;
      this.trbZoom.ToolTipText = "Zoom, right-click for autozoom";
      this.trbZoom.Value = 1;
      // 
      // trvSubjects
      // 
      this.trvSubjects.CheckBoxes = true;
      this.trvSubjects.Dock = System.Windows.Forms.DockStyle.Fill;
      this.trvSubjects.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawText;
      this.trvSubjects.ImageIndex = 0;
      this.trvSubjects.ImageList = this.imlTreeViewSubjects;
      this.trvSubjects.Location = new System.Drawing.Point(0, 0);
      this.trvSubjects.Name = "trvSubjects";
      this.trvSubjects.SelectedImageIndex = 0;
      this.trvSubjects.Size = new System.Drawing.Size(179, 346);
      this.trvSubjects.TabIndex = 12;
      this.toolTip1.SetToolTip(this.trvSubjects, "Select or deselect the subjects that you want to be included in calculation.");
      this.trvSubjects.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.trvSubjects_AfterCheck);
      this.trvSubjects.DrawNode += new System.Windows.Forms.DrawTreeNodeEventHandler(this.trvSubjects_DrawNode);
      // 
      // imlTreeViewSubjects
      // 
      this.imlTreeViewSubjects.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imlTreeViewSubjects.ImageStream")));
      this.imlTreeViewSubjects.TransparentColor = System.Drawing.Color.Transparent;
      this.imlTreeViewSubjects.Images.SetKeyName(0, "Categorie");
      this.imlTreeViewSubjects.Images.SetKeyName(1, "Subject");
      // 
      // toolStripContainer1
      // 
      // 
      // toolStripContainer1.ContentPanel
      // 
      this.toolStripContainer1.ContentPanel.Controls.Add(this.spcListPic);
      this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(871, 346);
      this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
      this.toolStripContainer1.Name = "toolStripContainer1";
      this.toolStripContainer1.Size = new System.Drawing.Size(871, 424);
      this.toolStripContainer1.TabIndex = 3;
      this.toolStripContainer1.Text = "toolStripContainer1";
      // 
      // toolStripContainer1.TopToolStripPanel
      // 
      this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.tosTrialSelection);
      this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.tosGazeDisplay);
      this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.tosMouseDisplay);
      this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.tosLevenshtein);
      // 
      // spcListPic
      // 
      this.spcListPic.Dock = System.Windows.Forms.DockStyle.Fill;
      this.spcListPic.Location = new System.Drawing.Point(0, 0);
      this.spcListPic.Name = "spcListPic";
      // 
      // spcListPic.Panel1
      // 
      this.spcListPic.Panel1.Controls.Add(this.trvSubjects);
      // 
      // spcListPic.Panel2
      // 
      this.spcListPic.Panel2.Controls.Add(this.spcPicTable);
      this.spcListPic.Size = new System.Drawing.Size(871, 346);
      this.spcListPic.SplitterDistance = 179;
      this.spcListPic.TabIndex = 1;
      // 
      // spcPicTable
      // 
      this.spcPicTable.Dock = System.Windows.Forms.DockStyle.Fill;
      this.spcPicTable.Location = new System.Drawing.Point(0, 0);
      this.spcPicTable.Name = "spcPicTable";
      this.spcPicTable.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // spcPicTable.Panel1
      // 
      this.spcPicTable.Panel1.Controls.Add(this.tscPictureTimeline);
      // 
      // spcPicTable.Panel2
      // 
      this.spcPicTable.Panel2.Controls.Add(this.spcTables);
      this.spcPicTable.Size = new System.Drawing.Size(688, 346);
      this.spcPicTable.SplitterDistance = 233;
      this.spcPicTable.TabIndex = 1;
      // 
      // tscPictureTimeline
      // 
      // 
      // tscPictureTimeline.BottomToolStripPanel
      // 
      this.tscPictureTimeline.BottomToolStripPanel.Controls.Add(this.tosTimeLine);
      // 
      // tscPictureTimeline.ContentPanel
      // 
      this.tscPictureTimeline.ContentPanel.Controls.Add(this.pnlCanvas);
      this.tscPictureTimeline.ContentPanel.Margin = new System.Windows.Forms.Padding(0);
      this.tscPictureTimeline.ContentPanel.Size = new System.Drawing.Size(688, 207);
      this.tscPictureTimeline.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tscPictureTimeline.LeftToolStripPanelVisible = false;
      this.tscPictureTimeline.Location = new System.Drawing.Point(0, 0);
      this.tscPictureTimeline.Margin = new System.Windows.Forms.Padding(0);
      this.tscPictureTimeline.Name = "tscPictureTimeline";
      this.tscPictureTimeline.RightToolStripPanelVisible = false;
      this.tscPictureTimeline.Size = new System.Drawing.Size(688, 233);
      this.tscPictureTimeline.TabIndex = 1;
      this.tscPictureTimeline.Text = "toolStripContainer2";
      this.tscPictureTimeline.TopToolStripPanelVisible = false;
      // 
      // spcTables
      // 
      this.spcTables.Dock = System.Windows.Forms.DockStyle.Fill;
      this.spcTables.Location = new System.Drawing.Point(0, 0);
      this.spcTables.Name = "spcTables";
      // 
      // spcTables.Panel1
      // 
      this.spcTables.Panel1.Controls.Add(this.grbLociSimilarity);
      this.spcTables.Panel1MinSize = 100;
      // 
      // spcTables.Panel2
      // 
      this.spcTables.Panel2.Controls.Add(this.grpSequenceSimilarity);
      this.spcTables.Panel2MinSize = 100;
      this.spcTables.Size = new System.Drawing.Size(688, 109);
      this.spcTables.SplitterDistance = 300;
      this.spcTables.TabIndex = 1;
      // 
      // grbLociSimilarity
      // 
      this.grbLociSimilarity.Controls.Add(this.dgvLociSimilarity);
      this.grbLociSimilarity.Dock = System.Windows.Forms.DockStyle.Fill;
      this.grbLociSimilarity.Location = new System.Drawing.Point(0, 0);
      this.grbLociSimilarity.Name = "grbLociSimilarity";
      this.grbLociSimilarity.Size = new System.Drawing.Size(300, 109);
      this.grbLociSimilarity.TabIndex = 1;
      this.grbLociSimilarity.TabStop = false;
      this.grbLociSimilarity.Text = "Loci Similarity";
      // 
      // dgvLociSimilarity
      // 
      this.dgvLociSimilarity.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dgvLociSimilarity.Dock = System.Windows.Forms.DockStyle.Fill;
      this.dgvLociSimilarity.Location = new System.Drawing.Point(3, 16);
      this.dgvLociSimilarity.Name = "dgvLociSimilarity";
      this.dgvLociSimilarity.Size = new System.Drawing.Size(294, 90);
      this.dgvLociSimilarity.TabIndex = 0;
      // 
      // grpSequenceSimilarity
      // 
      this.grpSequenceSimilarity.Controls.Add(this.dgvSequenceSimilarity);
      this.grpSequenceSimilarity.Dock = System.Windows.Forms.DockStyle.Fill;
      this.grpSequenceSimilarity.Location = new System.Drawing.Point(0, 0);
      this.grpSequenceSimilarity.Name = "grpSequenceSimilarity";
      this.grpSequenceSimilarity.Size = new System.Drawing.Size(384, 109);
      this.grpSequenceSimilarity.TabIndex = 2;
      this.grpSequenceSimilarity.TabStop = false;
      this.grpSequenceSimilarity.Text = "Sequence Similarity";
      // 
      // dgvSequenceSimilarity
      // 
      this.dgvSequenceSimilarity.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dgvSequenceSimilarity.Dock = System.Windows.Forms.DockStyle.Fill;
      this.dgvSequenceSimilarity.Location = new System.Drawing.Point(3, 16);
      this.dgvSequenceSimilarity.Name = "dgvSequenceSimilarity";
      this.dgvSequenceSimilarity.Size = new System.Drawing.Size(378, 90);
      this.dgvSequenceSimilarity.TabIndex = 1;
      // 
      // tosTrialSelection
      // 
      this.tosTrialSelection.DataBindings.Add(new System.Windows.Forms.Binding("Location", global::Ogama.Properties.Settings.Default, "SCAStimuliToolbarLocation", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.tosTrialSelection.Dock = System.Windows.Forms.DockStyle.None;
      this.tosTrialSelection.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cbbTrial,
            this.btnEye,
            this.btnMouse,
            this.toolStripSeparator8,
            this.btnLevenstheinToolbar,
            this.toolStripSeparator1,
            this.btnTimeLine,
            this.nudTimeSpan,
            this.toolStripSeparator6,
            this.btnHelp});
      this.tosTrialSelection.Location = global::Ogama.Properties.Settings.Default.SCAStimuliToolbarLocation;
      this.tosTrialSelection.Name = "tosTrialSelection";
      this.tosTrialSelection.Size = new System.Drawing.Size(347, 25);
      this.tosTrialSelection.TabIndex = 0;
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
      // btnLevenstheinToolbar
      // 
      this.btnLevenstheinToolbar.CheckOnClick = true;
      this.btnLevenstheinToolbar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnLevenstheinToolbar.Image = global::Ogama.Properties.Resources.TextboxHS;
      this.btnLevenstheinToolbar.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnLevenstheinToolbar.Name = "btnLevenstheinToolbar";
      this.btnLevenstheinToolbar.Size = new System.Drawing.Size(23, 22);
      this.btnLevenstheinToolbar.Text = "Levenshtein Tools";
      this.btnLevenstheinToolbar.ToolTipText = "Check to display Levenshtein Distance calculation tools.";
      this.btnLevenstheinToolbar.Click += new System.EventHandler(this.btnLevenstheinToolbar_Click);
      // 
      // toolStripSeparator1
      // 
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
      // 
      // btnTimeLine
      // 
      this.btnTimeLine.CheckOnClick = true;
      this.btnTimeLine.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnTimeLine.Image = global::Ogama.Properties.Resources.Control_TrackBar;
      this.btnTimeLine.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnTimeLine.Name = "btnTimeLine";
      this.btnTimeLine.Size = new System.Drawing.Size(23, 22);
      this.btnTimeLine.Text = "Show/Hide time line.";
      this.btnTimeLine.Click += new System.EventHandler(this.btnTimeLine_Click);
      // 
      // nudTimeSpan
      // 
      this.nudTimeSpan.DecimalPlaces = 0;
      this.nudTimeSpan.Increment = new decimal(new int[] {
            500,
            0,
            0,
            0});
      this.nudTimeSpan.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
      this.nudTimeSpan.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
      this.nudTimeSpan.Name = "nudTimeSpan";
      this.nudTimeSpan.Size = new System.Drawing.Size(47, 22);
      this.nudTimeSpan.Text = "2000";
      this.nudTimeSpan.ToolTipText = "Time span (ms) to show during moving time line caret.";
      this.nudTimeSpan.Value = new decimal(new int[] {
            2000,
            0,
            0,
            0});
      this.nudTimeSpan.Visible = false;
      // 
      // toolStripSeparator6
      // 
      this.toolStripSeparator6.Name = "toolStripSeparator6";
      this.toolStripSeparator6.Size = new System.Drawing.Size(6, 25);
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
      this.tosGazeDisplay.DataBindings.Add(new System.Windows.Forms.Binding("Location", global::Ogama.Properties.Settings.Default, "SCAGazeToolbarLocation", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
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
      this.tosGazeDisplay.Location = global::Ogama.Properties.Settings.Default.SCAGazeToolbarLocation;
      this.tosGazeDisplay.Name = "tosGazeDisplay";
      this.tosGazeDisplay.Size = new System.Drawing.Size(368, 26);
      this.tosGazeDisplay.TabIndex = 4;
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
      this.btnGazePenStyle.Image = global::Ogama.Properties.Resources.DisplayInColorHS;
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
      // tosMouseDisplay
      // 
      this.tosMouseDisplay.DataBindings.Add(new System.Windows.Forms.Binding("Location", global::Ogama.Properties.Settings.Default, "SCAMouseToolbarLocation", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
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
      this.tosMouseDisplay.Location = global::Ogama.Properties.Settings.Default.SCAMouseToolbarLocation;
      this.tosMouseDisplay.Name = "tosMouseDisplay";
      this.tosMouseDisplay.Size = new System.Drawing.Size(368, 26);
      this.tosMouseDisplay.TabIndex = 5;
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
      this.btnMousePenStyle.Image = global::Ogama.Properties.Resources.DisplayInColorHS;
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
      // tosLevenshtein
      // 
      this.tosLevenshtein.DataBindings.Add(new System.Windows.Forms.Binding("Location", global::Ogama.Properties.Settings.Default, "SCALevenshteinToolbarLocation", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.tosLevenshtein.Dock = System.Windows.Forms.DockStyle.None;
      this.tosLevenshtein.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.btnGrid,
            this.nudGridFactor,
            this.toolStripSeparator2,
            this.btnAOI,
            this.toolStripSeparator3,
            this.btnRemoveSubsequentHits,
            this.btnGroupLevenshteinOutput,
            this.toolStripSeparator4,
            this.btnLociSimilarity,
            this.btnSequenceSimilarity,
            this.toolStripSeparator5,
            this.btnExport});
      this.tosLevenshtein.Location = global::Ogama.Properties.Settings.Default.SCALevenshteinToolbarLocation;
      this.tosLevenshtein.Name = "tosLevenshtein";
      this.tosLevenshtein.Size = new System.Drawing.Size(364, 26);
      this.tosLevenshtein.TabIndex = 6;
      // 
      // toolStripLabel1
      // 
      this.toolStripLabel1.Name = "toolStripLabel1";
      this.toolStripLabel1.Size = new System.Drawing.Size(126, 23);
      this.toolStripLabel1.Text = "Levenshtein properties";
      // 
      // btnGrid
      // 
      this.btnGrid.Checked = true;
      this.btnGrid.CheckOnClick = true;
      this.btnGrid.CheckState = System.Windows.Forms.CheckState.Checked;
      this.btnGrid.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnGrid.Image = global::Ogama.Properties.Resources.ShowGridlines2HS;
      this.btnGrid.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnGrid.Name = "btnGrid";
      this.btnGrid.Size = new System.Drawing.Size(23, 23);
      this.btnGrid.ToolTipText = "Use a rectangular grid to divide the stimulus picture into subsequences.";
      this.btnGrid.Click += new System.EventHandler(this.btnGrid_Click);
      // 
      // nudGridFactor
      // 
      this.nudGridFactor.DecimalPlaces = 0;
      this.nudGridFactor.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nudGridFactor.Maximum = new decimal(new int[] {
            130,
            0,
            0,
            0});
      this.nudGridFactor.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
      this.nudGridFactor.Name = "nudGridFactor";
      this.nudGridFactor.Size = new System.Drawing.Size(41, 23);
      this.nudGridFactor.Text = "5";
      this.nudGridFactor.ToolTipText = "Divider for Grid.";
      this.nudGridFactor.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
      this.nudGridFactor.ValueChanged += new System.EventHandler(this.nudGridFactor_ValueChanged);
      // 
      // toolStripSeparator2
      // 
      this.toolStripSeparator2.Name = "toolStripSeparator2";
      this.toolStripSeparator2.Size = new System.Drawing.Size(6, 26);
      // 
      // btnAOI
      // 
      this.btnAOI.CheckOnClick = true;
      this.btnAOI.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnAOI.Image = global::Ogama.Properties.Resources.AOILogo;
      this.btnAOI.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnAOI.Name = "btnAOI";
      this.btnAOI.Size = new System.Drawing.Size(23, 23);
      this.btnAOI.ToolTipText = "Use a areas of interest to divide the stimulus picture into subsequences.";
      this.btnAOI.Click += new System.EventHandler(this.btnAOI_Click);
      // 
      // toolStripSeparator3
      // 
      this.toolStripSeparator3.Name = "toolStripSeparator3";
      this.toolStripSeparator3.Size = new System.Drawing.Size(6, 26);
      // 
      // btnRemoveSubsequentHits
      // 
      this.btnRemoveSubsequentHits.CheckOnClick = true;
      this.btnRemoveSubsequentHits.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnRemoveSubsequentHits.Image = global::Ogama.Properties.Resources.IgnoreDoubles;
      this.btnRemoveSubsequentHits.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnRemoveSubsequentHits.Name = "btnRemoveSubsequentHits";
      this.btnRemoveSubsequentHits.Size = new System.Drawing.Size(23, 23);
      this.btnRemoveSubsequentHits.Text = "Ignore subsequent fixations in the same AOI.";
      this.btnRemoveSubsequentHits.Click += new System.EventHandler(this.btnRemoveSubsequentHits_Click);
      // 
      // btnGroupLevenshteinOutput
      // 
      this.btnGroupLevenshteinOutput.CheckOnClick = true;
      this.btnGroupLevenshteinOutput.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnGroupLevenshteinOutput.Image = global::Ogama.Properties.Resources.OrgChartHS;
      this.btnGroupLevenshteinOutput.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnGroupLevenshteinOutput.Name = "btnGroupLevenshteinOutput";
      this.btnGroupLevenshteinOutput.Size = new System.Drawing.Size(23, 23);
      this.btnGroupLevenshteinOutput.Text = "Calculate average similarities across groups";
      this.btnGroupLevenshteinOutput.Click += new System.EventHandler(this.btnGroupLevenshteinOutput_Click);
      // 
      // toolStripSeparator4
      // 
      this.toolStripSeparator4.Name = "toolStripSeparator4";
      this.toolStripSeparator4.Size = new System.Drawing.Size(6, 26);
      // 
      // btnLociSimilarity
      // 
      this.btnLociSimilarity.Checked = true;
      this.btnLociSimilarity.CheckOnClick = true;
      this.btnLociSimilarity.CheckState = System.Windows.Forms.CheckState.Checked;
      this.btnLociSimilarity.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnLociSimilarity.Image = ((System.Drawing.Image)(resources.GetObject("btnLociSimilarity.Image")));
      this.btnLociSimilarity.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnLociSimilarity.Name = "btnLociSimilarity";
      this.btnLociSimilarity.Size = new System.Drawing.Size(23, 23);
      this.btnLociSimilarity.Text = "Show/Hide loci similarity table.";
      this.btnLociSimilarity.CheckedChanged += new System.EventHandler(this.btnSimilarity_CheckedChanged);
      // 
      // btnSequenceSimilarity
      // 
      this.btnSequenceSimilarity.Checked = true;
      this.btnSequenceSimilarity.CheckOnClick = true;
      this.btnSequenceSimilarity.CheckState = System.Windows.Forms.CheckState.Checked;
      this.btnSequenceSimilarity.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnSequenceSimilarity.Image = ((System.Drawing.Image)(resources.GetObject("btnSequenceSimilarity.Image")));
      this.btnSequenceSimilarity.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnSequenceSimilarity.Name = "btnSequenceSimilarity";
      this.btnSequenceSimilarity.Size = new System.Drawing.Size(23, 23);
      this.btnSequenceSimilarity.Text = "Show/Hide sequence similarity table.";
      this.btnSequenceSimilarity.CheckedChanged += new System.EventHandler(this.btnSimilarity_CheckedChanged);
      // 
      // toolStripSeparator5
      // 
      this.toolStripSeparator5.Name = "toolStripSeparator5";
      this.toolStripSeparator5.Size = new System.Drawing.Size(6, 26);
      // 
      // btnExport
      // 
      this.btnExport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnExport.Image = global::Ogama.Properties.Resources.ExportData;
      this.btnExport.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnExport.Name = "btnExport";
      this.btnExport.Size = new System.Drawing.Size(23, 23);
      this.btnExport.Text = "Export";
      this.btnExport.ToolTipText = "Export similarity measurements to data sheet";
      this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
      // 
      // sfdSimiliarity
      // 
      this.sfdSimiliarity.DefaultExt = "txt";
      this.sfdSimiliarity.FileName = "Similarity.txt";
      this.sfdSimiliarity.Filter = "Text files|*.txt";
      this.sfdSimiliarity.Title = "Specify filename for similarity data sheet.";
      // 
      // ScanpathsModule
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(871, 424);
      this.Controls.Add(this.toolStripContainer1);
      this.DataBindings.Add(new System.Windows.Forms.Binding("Location", global::Ogama.Properties.Settings.Default, "ScanpathsModuleLocation", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.KeyPreview = true;
      this.Location = global::Ogama.Properties.Settings.Default.ScanpathsModuleLocation;
      this.Logo = global::Ogama.Properties.Resources.Scanpath;
      this.Name = "ScanpathsModule";
      this.Text = "Scanpaths Module";
      this.Load += new System.EventHandler(this.frmFixations_Load);
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
      this.pnlCanvas.ResumeLayout(false);
      this.pnlPicture.ResumeLayout(false);
      this.tosTimeLine.ResumeLayout(false);
      this.tosTimeLine.PerformLayout();
      this.toolStripContainer1.ContentPanel.ResumeLayout(false);
      this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
      this.toolStripContainer1.TopToolStripPanel.PerformLayout();
      this.toolStripContainer1.ResumeLayout(false);
      this.toolStripContainer1.PerformLayout();
      this.spcListPic.Panel1.ResumeLayout(false);
      this.spcListPic.Panel2.ResumeLayout(false);
      this.spcListPic.ResumeLayout(false);
      this.spcPicTable.Panel1.ResumeLayout(false);
      this.spcPicTable.Panel2.ResumeLayout(false);
      this.spcPicTable.ResumeLayout(false);
      this.tscPictureTimeline.BottomToolStripPanel.ResumeLayout(false);
      this.tscPictureTimeline.BottomToolStripPanel.PerformLayout();
      this.tscPictureTimeline.ContentPanel.ResumeLayout(false);
      this.tscPictureTimeline.ResumeLayout(false);
      this.tscPictureTimeline.PerformLayout();
      this.spcTables.Panel1.ResumeLayout(false);
      this.spcTables.Panel2.ResumeLayout(false);
      this.spcTables.ResumeLayout(false);
      this.grbLociSimilarity.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.dgvLociSimilarity)).EndInit();
      this.grpSequenceSimilarity.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.dgvSequenceSimilarity)).EndInit();
      this.tosTrialSelection.ResumeLayout(false);
      this.tosTrialSelection.PerformLayout();
      this.tosGazeDisplay.ResumeLayout(false);
      this.tosGazeDisplay.PerformLayout();
      this.tosMouseDisplay.ResumeLayout(false);
      this.tosMouseDisplay.PerformLayout();
      this.tosLevenshtein.ResumeLayout(false);
      this.tosLevenshtein.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.ToolTip toolTip1;
    private ScanpathsPicture scanpathsPicture;
    private System.Windows.Forms.ToolStripContainer toolStripContainer1;
    private System.Windows.Forms.ToolStrip tosTrialSelection;
    private System.Windows.Forms.ToolStripButton btnHelp;
    private System.Windows.Forms.ToolStripComboBox cbbTrial;
    private System.Windows.Forms.Panel pnlCanvas;
    private System.Windows.Forms.Panel pnlPicture;
    private System.Windows.Forms.ToolStripButton btnEye;
    private System.Windows.Forms.ToolStripButton btnMouse;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
    private System.Windows.Forms.ToolStrip tosGazeDisplay;
    private System.Windows.Forms.ToolStripButton toolStripButton3;
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
    private System.Windows.Forms.SplitContainer spcListPic;
    private OgamaControls.CheckboxTreeView trvSubjects;
    private System.Windows.Forms.ImageList imlTreeViewSubjects;
    private System.Windows.Forms.ToolStripButton btnMouseConnections;
    private System.Windows.Forms.ToolStripButton btnMouseNumbers;
    private System.Windows.Forms.ToolStripButton btnGazeConnections;
    private System.Windows.Forms.ToolStripButton btnGazeNumbers;
    private System.Windows.Forms.ToolStrip tosLevenshtein;
    private System.Windows.Forms.ToolStripButton btnGrid;
    private OgamaControls.ToolStripNumericUpDown nudGridFactor;
    private System.Windows.Forms.ToolStripLabel toolStripLabel1;
    private System.Windows.Forms.ToolStripButton btnAOI;
    private System.Windows.Forms.SplitContainer spcPicTable;
    private System.Windows.Forms.DataGridView dgvLociSimilarity;
    private System.Windows.Forms.ToolStripButton btnLevenstheinToolbar;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
    private System.Windows.Forms.ToolStripButton btnRemoveSubsequentHits;
    private System.Windows.Forms.SplitContainer spcTables;
    private System.Windows.Forms.DataGridView dgvSequenceSimilarity;
    private System.Windows.Forms.GroupBox grbLociSimilarity;
    private System.Windows.Forms.GroupBox grpSequenceSimilarity;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
    private System.Windows.Forms.ToolStripButton btnLociSimilarity;
    private System.Windows.Forms.ToolStripButton btnSequenceSimilarity;
    private System.Windows.Forms.ToolStrip tosTimeLine;
    private System.Windows.Forms.ToolStripButton btnStart;
    private System.Windows.Forms.ToolStripButton btnStop;
    private System.Windows.Forms.ToolStripButton btnRewind;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
    private System.Windows.Forms.ToolStripButton btnExport;
    private System.Windows.Forms.SaveFileDialog sfdSimiliarity;
    private TrialTimeLine trialTimeLine;
    private OgamaControls.ToolStripNumericUpDown nudTimeSpan;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
    private System.Windows.Forms.ToolStripButton btnTimeLine;
    private System.Windows.Forms.ToolStripButton btnGroupLevenshteinOutput;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
    private OgamaControls.ToolStripTrackBar trbZoom;
    private System.Windows.Forms.ToolStripContainer tscPictureTimeline;
  }
}