namespace Ogama.Modules.AOI
{
  using Ogama.Modules.Common.Controls;

  using VectorGraphics.Tools.CustomEventArgs;

  partial class AOIModule
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AOIModule));
      this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
      this.dgvAOIs = new System.Windows.Forms.DataGridView();
      this.colID = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colTrialID = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colSlideNr = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colShapeName = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colShapeType = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colShapeNumPts = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colShapePts = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colShapeGroup = new OgamaControls.DataGridViewQuickComboBoxColumn();
      this.cmnuDataGridView = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.cmnuCopyToClipboard = new System.Windows.Forms.ToolStripMenuItem();
      this.cmnuPasteFromClipboard = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
      this.cmnuSelectAll = new System.Windows.Forms.ToolStripMenuItem();
      this.cmnuSelectElement = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
      this.cmuAddShapeGroup = new System.Windows.Forms.ToolStripMenuItem();
      this.trvSubjects = new OgamaControls.CheckboxTreeView(this.components);
      this.imlTreeViewSubjects = new System.Windows.Forms.ImageList(this.components);
      this.sfdExport = new System.Windows.Forms.SaveFileDialog();
      this.spcPictureTools = new System.Windows.Forms.SplitContainer();
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      this.spcStatisticsPicture = new System.Windows.Forms.SplitContainer();
      this.toolStripContainer2 = new System.Windows.Forms.ToolStripContainer();
      this.tabControl1 = new System.Windows.Forms.TabControl();
      this.tbpFixationOptions = new System.Windows.Forms.TabPage();
      this.groupBox3 = new System.Windows.Forms.GroupBox();
      this.rdbRelativeTransitions = new System.Windows.Forms.RadioButton();
      this.rdbHideTransitions = new System.Windows.Forms.RadioButton();
      this.rdbAbsoluteTransitions = new System.Windows.Forms.RadioButton();
      this.groupBox2 = new System.Windows.Forms.GroupBox();
      this.rdbTransitions = new System.Windows.Forms.RadioButton();
      this.chbHideAOIDescription = new System.Windows.Forms.CheckBox();
      this.rdbGazeAverageFixationDuration = new System.Windows.Forms.RadioButton();
      this.rdbGazeCompleteFixationTime = new System.Windows.Forms.RadioButton();
      this.rdbGazeNumberOfFixations = new System.Windows.Forms.RadioButton();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.chbArrowBrush = new System.Windows.Forms.CheckBox();
      this.nudArrowFactor = new System.Windows.Forms.NumericUpDown();
      this.btnArrowBrush = new System.Windows.Forms.Button();
      this.imlToolbar = new System.Windows.Forms.ImageList(this.components);
      this.chbArrowFont = new System.Windows.Forms.CheckBox();
      this.btnArrowFont = new System.Windows.Forms.Button();
      this.chbArrowPen = new System.Windows.Forms.CheckBox();
      this.btnArrowPen = new System.Windows.Forms.Button();
      this.label3 = new System.Windows.Forms.Label();
      this.grpArrow = new System.Windows.Forms.GroupBox();
      this.chbBubbleBrush = new System.Windows.Forms.CheckBox();
      this.nudBubbleFactor = new System.Windows.Forms.NumericUpDown();
      this.chbBubbleFont = new System.Windows.Forms.CheckBox();
      this.chbBubblePen = new System.Windows.Forms.CheckBox();
      this.btnBubbleBrush = new System.Windows.Forms.Button();
      this.btnBubbleFont = new System.Windows.Forms.Button();
      this.btnBubblePen = new System.Windows.Forms.Button();
      this.label1 = new System.Windows.Forms.Label();
      this.tbpFixationSubjects = new System.Windows.Forms.TabPage();
      this.splitContainer3 = new System.Windows.Forms.SplitContainer();
      this.btnDeselectAllSubjects = new System.Windows.Forms.Button();
      this.btnSelectAllSubjects = new System.Windows.Forms.Button();
      this.toolStrip3 = new System.Windows.Forms.ToolStrip();
      this.toolStripLabel4 = new System.Windows.Forms.ToolStripLabel();
      this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
      this.btnEye = new System.Windows.Forms.ToolStripButton();
      this.btnMouse = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
      this.btnRecalculateStatistics = new System.Windows.Forms.ToolStripButton();
      this.pnlCanvas = new System.Windows.Forms.Panel();
      this.pnlPicture = new System.Windows.Forms.Panel();
      this.aoiPicture = new Ogama.Modules.AOI.AOIPicture();
      this.toolStrip2 = new System.Windows.Forms.ToolStrip();
      this.btnSeekNextSlide = new System.Windows.Forms.ToolStripButton();
      this.btnSeekPreviousSlide = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
      this.trialTimeLine = new TrialTimeLine(this.components);
      this.toolStrip1 = new System.Windows.Forms.ToolStrip();
      this.cbbTrial = new System.Windows.Forms.ToolStripComboBox();
      this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
      this.btnNewRectangle = new System.Windows.Forms.ToolStripButton();
      this.btnNewEllipse = new System.Windows.Forms.ToolStripButton();
      this.btnNewPolyline = new System.Windows.Forms.ToolStripButton();
      this.btnNewAOIGrid = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
      this.btnAddShapeGroup = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
      this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
      this.btnReadAOITable = new System.Windows.Forms.ToolStripButton();
      this.btnImportTargets = new System.Windows.Forms.ToolStripButton();
      this.btnExportAOITable = new System.Windows.Forms.ToolStripButton();
      this.btnDeleteAOIs = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
      this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
      this.btnShowAOIStatistics = new System.Windows.Forms.ToolStripButton();
      this.btnShowHideTable = new System.Windows.Forms.ToolStripButton();
      this.btnStyleNormal = new System.Windows.Forms.ToolStripButton();
      this.btnStyleTarget = new System.Windows.Forms.ToolStripButton();
      this.btnStyleSearchRect = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
      this.btnSaveAOI = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
      this.btnHelp = new System.Windows.Forms.ToolStripButton();
      this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
      this.saveFileDialogExport = new System.Windows.Forms.SaveFileDialog();
      this.toolStripSeparator12 = new System.Windows.Forms.ToolStripSeparator();
      this.trbZoom = new OgamaControls.ToolStripTrackBar();
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
      ((System.ComponentModel.ISupportInitialize)(this.dgvAOIs)).BeginInit();
      this.cmnuDataGridView.SuspendLayout();
      this.spcPictureTools.Panel1.SuspendLayout();
      this.spcPictureTools.Panel2.SuspendLayout();
      this.spcPictureTools.SuspendLayout();
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      this.spcStatisticsPicture.Panel1.SuspendLayout();
      this.spcStatisticsPicture.Panel2.SuspendLayout();
      this.spcStatisticsPicture.SuspendLayout();
      this.toolStripContainer2.ContentPanel.SuspendLayout();
      this.toolStripContainer2.TopToolStripPanel.SuspendLayout();
      this.toolStripContainer2.SuspendLayout();
      this.tabControl1.SuspendLayout();
      this.tbpFixationOptions.SuspendLayout();
      this.groupBox3.SuspendLayout();
      this.groupBox2.SuspendLayout();
      this.groupBox1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.nudArrowFactor)).BeginInit();
      this.grpArrow.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.nudBubbleFactor)).BeginInit();
      this.tbpFixationSubjects.SuspendLayout();
      this.splitContainer3.Panel1.SuspendLayout();
      this.splitContainer3.Panel2.SuspendLayout();
      this.splitContainer3.SuspendLayout();
      this.toolStrip3.SuspendLayout();
      this.pnlCanvas.SuspendLayout();
      this.pnlPicture.SuspendLayout();
      this.toolStrip2.SuspendLayout();
      this.toolStrip1.SuspendLayout();
      this.toolStripContainer1.ContentPanel.SuspendLayout();
      this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
      this.toolStripContainer1.SuspendLayout();
      this.SuspendLayout();
      // 
      // dgvAOIs
      // 
      this.dgvAOIs.AllowDrop = true;
      this.dgvAOIs.AllowUserToAddRows = false;
      this.dgvAOIs.AutoGenerateColumns = false;
      this.dgvAOIs.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colID,
            this.colTrialID,
            this.colSlideNr,
            this.colShapeName,
            this.colShapeType,
            this.colShapeNumPts,
            this.colShapePts,
            this.colShapeGroup});
      this.dgvAOIs.ContextMenuStrip = this.cmnuDataGridView;
      this.dgvAOIs.DataSource = this.bsoAOIs;
      this.dgvAOIs.Dock = System.Windows.Forms.DockStyle.Fill;
      this.dgvAOIs.Location = new System.Drawing.Point(0, 0);
      this.dgvAOIs.Name = "dgvAOIs";
      this.dgvAOIs.Size = new System.Drawing.Size(1013, 141);
      this.dgvAOIs.TabIndex = 3;
      this.toolTip1.SetToolTip(this.dgvAOIs, "Right-click for context-menu.");
      this.dgvAOIs.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvAOIs_CellValueChanged);
      this.dgvAOIs.UserDeletedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.dGVAOIs_UserDeletedRow);
      this.dgvAOIs.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dGVAOIs_DataError);
      // 
      // colID
      // 
      this.colID.DataPropertyName = "ID";
      this.colID.HeaderText = "ID";
      this.colID.MinimumWidth = 60;
      this.colID.Name = "colID";
      this.colID.ReadOnly = true;
      this.colID.Width = 60;
      // 
      // colTrialID
      // 
      this.colTrialID.DataPropertyName = "TrialID";
      this.colTrialID.HeaderText = "TrialID";
      this.colTrialID.MinimumWidth = 60;
      this.colTrialID.Name = "colTrialID";
      this.colTrialID.Width = 60;
      // 
      // colSlideNr
      // 
      this.colSlideNr.DataPropertyName = "SlideNr";
      this.colSlideNr.HeaderText = "Slide";
      this.colSlideNr.MinimumWidth = 60;
      this.colSlideNr.Name = "colSlideNr";
      this.colSlideNr.Width = 60;
      // 
      // colShapeName
      // 
      this.colShapeName.DataPropertyName = "ShapeName";
      this.colShapeName.HeaderText = "Name";
      this.colShapeName.MinimumWidth = 60;
      this.colShapeName.Name = "colShapeName";
      this.colShapeName.Width = 120;
      // 
      // colShapeType
      // 
      this.colShapeType.DataPropertyName = "ShapeType";
      this.colShapeType.HeaderText = "Type";
      this.colShapeType.MinimumWidth = 60;
      this.colShapeType.Name = "colShapeType";
      this.colShapeType.Width = 120;
      // 
      // colShapeNumPts
      // 
      this.colShapeNumPts.DataPropertyName = "ShapeNumPts";
      this.colShapeNumPts.HeaderText = "PtCount";
      this.colShapeNumPts.MinimumWidth = 60;
      this.colShapeNumPts.Name = "colShapeNumPts";
      this.colShapeNumPts.Width = 60;
      // 
      // colShapePts
      // 
      this.colShapePts.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
      this.colShapePts.DataPropertyName = "ShapePts";
      this.colShapePts.HeaderText = "Points";
      this.colShapePts.Name = "colShapePts";
      // 
      // colShapeGroup
      // 
      this.colShapeGroup.DataPropertyName = "ShapeGroup";
      this.colShapeGroup.DisplayMember = "ShapeGroup";
      this.colShapeGroup.HeaderText = "Group";
      this.colShapeGroup.MinimumWidth = 60;
      this.colShapeGroup.Name = "colShapeGroup";
      this.colShapeGroup.Resizable = System.Windows.Forms.DataGridViewTriState.True;
      this.colShapeGroup.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
      this.colShapeGroup.ValueMember = "ShapeGroup";
      this.colShapeGroup.Width = 120;
      // 
      // cmnuDataGridView
      // 
      this.cmnuDataGridView.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmnuCopyToClipboard,
            this.cmnuPasteFromClipboard,
            this.toolStripSeparator8,
            this.cmnuSelectAll,
            this.cmnuSelectElement,
            this.toolStripSeparator7,
            this.cmuAddShapeGroup});
      this.cmnuDataGridView.Name = "cmnuDataGridView";
      this.cmnuDataGridView.Size = new System.Drawing.Size(191, 126);
      // 
      // cmnuCopyToClipboard
      // 
      this.cmnuCopyToClipboard.Image = global::Ogama.Properties.Resources.CopyHS;
      this.cmnuCopyToClipboard.Name = "cmnuCopyToClipboard";
      this.cmnuCopyToClipboard.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
      this.cmnuCopyToClipboard.Size = new System.Drawing.Size(190, 22);
      this.cmnuCopyToClipboard.Text = "Copy";
      this.cmnuCopyToClipboard.Click += new System.EventHandler(this.cmnuCopyToClipboard_Click);
      // 
      // cmnuPasteFromClipboard
      // 
      this.cmnuPasteFromClipboard.Image = global::Ogama.Properties.Resources.PasteHS;
      this.cmnuPasteFromClipboard.Name = "cmnuPasteFromClipboard";
      this.cmnuPasteFromClipboard.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
      this.cmnuPasteFromClipboard.Size = new System.Drawing.Size(190, 22);
      this.cmnuPasteFromClipboard.Text = "Paste";
      this.cmnuPasteFromClipboard.Click += new System.EventHandler(this.cmnuPasteFromClipboard_Click);
      // 
      // toolStripSeparator8
      // 
      this.toolStripSeparator8.Name = "toolStripSeparator8";
      this.toolStripSeparator8.Size = new System.Drawing.Size(187, 6);
      // 
      // cmnuSelectAll
      // 
      this.cmnuSelectAll.Name = "cmnuSelectAll";
      this.cmnuSelectAll.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
      this.cmnuSelectAll.Size = new System.Drawing.Size(190, 22);
      this.cmnuSelectAll.Text = "Select All";
      this.cmnuSelectAll.Click += new System.EventHandler(this.cmnuSelectAll_Click);
      // 
      // cmnuSelectElement
      // 
      this.cmnuSelectElement.Image = global::Ogama.Properties.Resources.AOILogo;
      this.cmnuSelectElement.Name = "cmnuSelectElement";
      this.cmnuSelectElement.Size = new System.Drawing.Size(190, 22);
      this.cmnuSelectElement.Text = "Select Element";
      this.cmnuSelectElement.Click += new System.EventHandler(this.cmnuSelectElement_Click);
      // 
      // toolStripSeparator7
      // 
      this.toolStripSeparator7.Name = "toolStripSeparator7";
      this.toolStripSeparator7.Size = new System.Drawing.Size(187, 6);
      // 
      // cmuAddShapeGroup
      // 
      this.cmuAddShapeGroup.Image = global::Ogama.Properties.Resources.VSObject_Class;
      this.cmuAddShapeGroup.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.cmuAddShapeGroup.Name = "cmuAddShapeGroup";
      this.cmuAddShapeGroup.Size = new System.Drawing.Size(190, 22);
      this.cmuAddShapeGroup.Text = "Add new shape group";
      this.cmuAddShapeGroup.Click += new System.EventHandler(this.cmuAddShapeGroup_Click);
      // 
      // trvSubjects
      // 
      this.trvSubjects.CheckBoxes = true;
      this.trvSubjects.Dock = System.Windows.Forms.DockStyle.Fill;
      this.trvSubjects.ImageIndex = 0;
      this.trvSubjects.ImageList = this.imlTreeViewSubjects;
      this.trvSubjects.Location = new System.Drawing.Point(0, 0);
      this.trvSubjects.Name = "trvSubjects";
      this.trvSubjects.SelectedImageIndex = 0;
      this.trvSubjects.Size = new System.Drawing.Size(186, 408);
      this.trvSubjects.TabIndex = 0;
      this.toolTip1.SetToolTip(this.trvSubjects, "Select or deselect the subjects that you want to be included in calculation.");
      this.trvSubjects.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.trvSubjects_AfterCheck);
      // 
      // imlTreeViewSubjects
      // 
      this.imlTreeViewSubjects.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imlTreeViewSubjects.ImageStream")));
      this.imlTreeViewSubjects.TransparentColor = System.Drawing.Color.Transparent;
      this.imlTreeViewSubjects.Images.SetKeyName(0, "Categorie");
      this.imlTreeViewSubjects.Images.SetKeyName(1, "Subject");
      // 
      // sfdExport
      // 
      this.sfdExport.DefaultExt = "txt";
      this.sfdExport.FileName = "*.txt";
      this.sfdExport.Filter = "Text files|*.txt";
      this.sfdExport.Title = "Save areas of interest table ...";
      // 
      // spcPictureTools
      // 
      this.spcPictureTools.Dock = System.Windows.Forms.DockStyle.Fill;
      this.spcPictureTools.Location = new System.Drawing.Point(0, 0);
      this.spcPictureTools.Name = "spcPictureTools";
      this.spcPictureTools.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // spcPictureTools.Panel1
      // 
      this.spcPictureTools.Panel1.Controls.Add(this.splitContainer1);
      // 
      // spcPictureTools.Panel2
      // 
      this.spcPictureTools.Panel2.Controls.Add(this.dgvAOIs);
      this.spcPictureTools.Size = new System.Drawing.Size(1013, 658);
      this.spcPictureTools.SplitterDistance = 513;
      this.spcPictureTools.TabIndex = 3;
      // 
      // splitContainer1
      // 
      this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
      this.splitContainer1.Location = new System.Drawing.Point(0, 0);
      this.splitContainer1.Name = "splitContainer1";
      this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // splitContainer1.Panel1
      // 
      this.splitContainer1.Panel1.Controls.Add(this.spcStatisticsPicture);
      // 
      // splitContainer1.Panel2
      // 
      this.splitContainer1.Panel2.Controls.Add(this.toolStrip2);
      this.splitContainer1.Size = new System.Drawing.Size(1013, 513);
      this.splitContainer1.SplitterDistance = 487;
      this.splitContainer1.SplitterWidth = 1;
      this.splitContainer1.TabIndex = 1;
      // 
      // spcStatisticsPicture
      // 
      this.spcStatisticsPicture.Dock = System.Windows.Forms.DockStyle.Fill;
      this.spcStatisticsPicture.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
      this.spcStatisticsPicture.IsSplitterFixed = true;
      this.spcStatisticsPicture.Location = new System.Drawing.Point(0, 0);
      this.spcStatisticsPicture.Name = "spcStatisticsPicture";
      // 
      // spcStatisticsPicture.Panel1
      // 
      this.spcStatisticsPicture.Panel1.Controls.Add(this.toolStripContainer2);
      this.spcStatisticsPicture.Panel1MinSize = 200;
      // 
      // spcStatisticsPicture.Panel2
      // 
      this.spcStatisticsPicture.Panel2.Controls.Add(this.pnlCanvas);
      this.spcStatisticsPicture.Size = new System.Drawing.Size(1013, 487);
      this.spcStatisticsPicture.SplitterDistance = 200;
      this.spcStatisticsPicture.TabIndex = 1;
      // 
      // toolStripContainer2
      // 
      // 
      // toolStripContainer2.ContentPanel
      // 
      this.toolStripContainer2.ContentPanel.Controls.Add(this.tabControl1);
      this.toolStripContainer2.ContentPanel.Size = new System.Drawing.Size(200, 462);
      this.toolStripContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.toolStripContainer2.Location = new System.Drawing.Point(0, 0);
      this.toolStripContainer2.Name = "toolStripContainer2";
      this.toolStripContainer2.Size = new System.Drawing.Size(200, 487);
      this.toolStripContainer2.TabIndex = 0;
      this.toolStripContainer2.Text = "toolStripContainer2";
      // 
      // toolStripContainer2.TopToolStripPanel
      // 
      this.toolStripContainer2.TopToolStripPanel.Controls.Add(this.toolStrip3);
      // 
      // tabControl1
      // 
      this.tabControl1.Controls.Add(this.tbpFixationOptions);
      this.tabControl1.Controls.Add(this.tbpFixationSubjects);
      this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tabControl1.ImageList = this.imlToolbar;
      this.tabControl1.Location = new System.Drawing.Point(0, 0);
      this.tabControl1.Name = "tabControl1";
      this.tabControl1.SelectedIndex = 0;
      this.tabControl1.Size = new System.Drawing.Size(200, 462);
      this.tabControl1.TabIndex = 43;
      // 
      // tbpFixationOptions
      // 
      this.tbpFixationOptions.Controls.Add(this.groupBox3);
      this.tbpFixationOptions.Controls.Add(this.groupBox2);
      this.tbpFixationOptions.Controls.Add(this.groupBox1);
      this.tbpFixationOptions.Controls.Add(this.grpArrow);
      this.tbpFixationOptions.ImageKey = "Properties";
      this.tbpFixationOptions.Location = new System.Drawing.Point(4, 23);
      this.tbpFixationOptions.Name = "tbpFixationOptions";
      this.tbpFixationOptions.Padding = new System.Windows.Forms.Padding(3);
      this.tbpFixationOptions.Size = new System.Drawing.Size(192, 435);
      this.tbpFixationOptions.TabIndex = 0;
      this.tbpFixationOptions.Text = "Options";
      this.tbpFixationOptions.UseVisualStyleBackColor = true;
      // 
      // groupBox3
      // 
      this.groupBox3.Controls.Add(this.rdbRelativeTransitions);
      this.groupBox3.Controls.Add(this.rdbHideTransitions);
      this.groupBox3.Controls.Add(this.rdbAbsoluteTransitions);
      this.groupBox3.Location = new System.Drawing.Point(4, 143);
      this.groupBox3.Name = "groupBox3";
      this.groupBox3.Size = new System.Drawing.Size(183, 89);
      this.groupBox3.TabIndex = 47;
      this.groupBox3.TabStop = false;
      this.groupBox3.Text = "Transitions";
      // 
      // rdbRelativeTransitions
      // 
      this.rdbRelativeTransitions.AutoSize = true;
      this.rdbRelativeTransitions.Location = new System.Drawing.Point(6, 65);
      this.rdbRelativeTransitions.Name = "rdbRelativeTransitions";
      this.rdbRelativeTransitions.Size = new System.Drawing.Size(138, 17);
      this.rdbRelativeTransitions.TabIndex = 44;
      this.rdbRelativeTransitions.TabStop = true;
      this.rdbRelativeTransitions.Text = "relative transition values";
      this.rdbRelativeTransitions.UseVisualStyleBackColor = true;
      this.rdbRelativeTransitions.CheckedChanged += new System.EventHandler(this.rdbStatistic_CheckedChanged);
      // 
      // rdbHideTransitions
      // 
      this.rdbHideTransitions.AutoSize = true;
      this.rdbHideTransitions.Checked = true;
      this.rdbHideTransitions.Location = new System.Drawing.Point(6, 19);
      this.rdbHideTransitions.Name = "rdbHideTransitions";
      this.rdbHideTransitions.Size = new System.Drawing.Size(45, 17);
      this.rdbHideTransitions.TabIndex = 38;
      this.rdbHideTransitions.TabStop = true;
      this.rdbHideTransitions.Text = "hide";
      this.rdbHideTransitions.UseVisualStyleBackColor = true;
      this.rdbHideTransitions.CheckedChanged += new System.EventHandler(this.rdbStatistic_CheckedChanged);
      // 
      // rdbAbsoluteTransitions
      // 
      this.rdbAbsoluteTransitions.AutoSize = true;
      this.rdbAbsoluteTransitions.Location = new System.Drawing.Point(6, 42);
      this.rdbAbsoluteTransitions.Name = "rdbAbsoluteTransitions";
      this.rdbAbsoluteTransitions.Size = new System.Drawing.Size(144, 17);
      this.rdbAbsoluteTransitions.TabIndex = 44;
      this.rdbAbsoluteTransitions.TabStop = true;
      this.rdbAbsoluteTransitions.Text = "absolute transition values";
      this.rdbAbsoluteTransitions.UseVisualStyleBackColor = true;
      this.rdbAbsoluteTransitions.CheckedChanged += new System.EventHandler(this.rdbStatistic_CheckedChanged);
      // 
      // groupBox2
      // 
      this.groupBox2.Controls.Add(this.rdbTransitions);
      this.groupBox2.Controls.Add(this.chbHideAOIDescription);
      this.groupBox2.Controls.Add(this.rdbGazeAverageFixationDuration);
      this.groupBox2.Controls.Add(this.rdbGazeCompleteFixationTime);
      this.groupBox2.Controls.Add(this.rdbGazeNumberOfFixations);
      this.groupBox2.Location = new System.Drawing.Point(4, 3);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new System.Drawing.Size(183, 134);
      this.groupBox2.TabIndex = 46;
      this.groupBox2.TabStop = false;
      this.groupBox2.Text = "AOI Parameter";
      // 
      // rdbTransitions
      // 
      this.rdbTransitions.AutoSize = true;
      this.rdbTransitions.Checked = true;
      this.rdbTransitions.Location = new System.Drawing.Point(6, 19);
      this.rdbTransitions.Name = "rdbTransitions";
      this.rdbTransitions.Size = new System.Drawing.Size(45, 17);
      this.rdbTransitions.TabIndex = 38;
      this.rdbTransitions.TabStop = true;
      this.rdbTransitions.Text = "hide";
      this.rdbTransitions.UseVisualStyleBackColor = true;
      this.rdbTransitions.CheckedChanged += new System.EventHandler(this.rdbStatistic_CheckedChanged);
      // 
      // chbHideAOIDescription
      // 
      this.chbHideAOIDescription.AutoSize = true;
      this.chbHideAOIDescription.Location = new System.Drawing.Point(6, 111);
      this.chbHideAOIDescription.Name = "chbHideAOIDescription";
      this.chbHideAOIDescription.Size = new System.Drawing.Size(121, 17);
      this.chbHideAOIDescription.TabIndex = 43;
      this.chbHideAOIDescription.Text = "hide AOI description";
      this.chbHideAOIDescription.UseVisualStyleBackColor = true;
      this.chbHideAOIDescription.CheckedChanged += new System.EventHandler(this.chbHideAOIDescription_CheckedChanged);
      // 
      // rdbGazeAverageFixationDuration
      // 
      this.rdbGazeAverageFixationDuration.AutoSize = true;
      this.rdbGazeAverageFixationDuration.Location = new System.Drawing.Point(6, 88);
      this.rdbGazeAverageFixationDuration.Name = "rdbGazeAverageFixationDuration";
      this.rdbGazeAverageFixationDuration.Size = new System.Drawing.Size(141, 17);
      this.rdbGazeAverageFixationDuration.TabIndex = 39;
      this.rdbGazeAverageFixationDuration.Text = "average fixation duration";
      this.rdbGazeAverageFixationDuration.UseVisualStyleBackColor = true;
      this.rdbGazeAverageFixationDuration.CheckedChanged += new System.EventHandler(this.rdbStatistic_CheckedChanged);
      // 
      // rdbGazeCompleteFixationTime
      // 
      this.rdbGazeCompleteFixationTime.AutoSize = true;
      this.rdbGazeCompleteFixationTime.Location = new System.Drawing.Point(6, 42);
      this.rdbGazeCompleteFixationTime.Name = "rdbGazeCompleteFixationTime";
      this.rdbGazeCompleteFixationTime.Size = new System.Drawing.Size(126, 17);
      this.rdbGazeCompleteFixationTime.TabIndex = 36;
      this.rdbGazeCompleteFixationTime.Text = "complete fixation time";
      this.rdbGazeCompleteFixationTime.UseVisualStyleBackColor = true;
      this.rdbGazeCompleteFixationTime.CheckedChanged += new System.EventHandler(this.rdbStatistic_CheckedChanged);
      // 
      // rdbGazeNumberOfFixations
      // 
      this.rdbGazeNumberOfFixations.AutoSize = true;
      this.rdbGazeNumberOfFixations.Location = new System.Drawing.Point(6, 66);
      this.rdbGazeNumberOfFixations.Name = "rdbGazeNumberOfFixations";
      this.rdbGazeNumberOfFixations.Size = new System.Drawing.Size(113, 17);
      this.rdbGazeNumberOfFixations.TabIndex = 37;
      this.rdbGazeNumberOfFixations.Text = "number of fixations";
      this.rdbGazeNumberOfFixations.UseVisualStyleBackColor = true;
      this.rdbGazeNumberOfFixations.CheckedChanged += new System.EventHandler(this.rdbStatistic_CheckedChanged);
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.chbArrowBrush);
      this.groupBox1.Controls.Add(this.nudArrowFactor);
      this.groupBox1.Controls.Add(this.btnArrowBrush);
      this.groupBox1.Controls.Add(this.chbArrowFont);
      this.groupBox1.Controls.Add(this.btnArrowFont);
      this.groupBox1.Controls.Add(this.chbArrowPen);
      this.groupBox1.Controls.Add(this.btnArrowPen);
      this.groupBox1.Controls.Add(this.label3);
      this.groupBox1.Location = new System.Drawing.Point(97, 238);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(90, 135);
      this.groupBox1.TabIndex = 44;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Arrow style";
      // 
      // chbArrowBrush
      // 
      this.chbArrowBrush.AutoSize = true;
      this.chbArrowBrush.Checked = true;
      this.chbArrowBrush.CheckState = System.Windows.Forms.CheckState.Checked;
      this.chbArrowBrush.Location = new System.Drawing.Point(6, 83);
      this.chbArrowBrush.Name = "chbArrowBrush";
      this.chbArrowBrush.Size = new System.Drawing.Size(53, 17);
      this.chbArrowBrush.TabIndex = 1;
      this.chbArrowBrush.Text = "Brush";
      this.chbArrowBrush.UseVisualStyleBackColor = true;
      this.chbArrowBrush.CheckedChanged += new System.EventHandler(this.ArrowStyleCheckedChanged);
      // 
      // nudArrowFactor
      // 
      this.nudArrowFactor.DecimalPlaces = 1;
      this.nudArrowFactor.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
      this.nudArrowFactor.Location = new System.Drawing.Point(37, 106);
      this.nudArrowFactor.Name = "nudArrowFactor";
      this.nudArrowFactor.Size = new System.Drawing.Size(50, 20);
      this.nudArrowFactor.TabIndex = 40;
      this.nudArrowFactor.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.nudArrowFactor.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nudArrowFactor.ValueChanged += new System.EventHandler(this.nudArrowFactor_ValueChanged);
      // 
      // btnArrowBrush
      // 
      this.btnArrowBrush.ImageKey = "Brush";
      this.btnArrowBrush.ImageList = this.imlToolbar;
      this.btnArrowBrush.Location = new System.Drawing.Point(64, 77);
      this.btnArrowBrush.Name = "btnArrowBrush";
      this.btnArrowBrush.Size = new System.Drawing.Size(23, 23);
      this.btnArrowBrush.TabIndex = 0;
      this.btnArrowBrush.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.btnArrowBrush.UseVisualStyleBackColor = true;
      this.btnArrowBrush.Click += new System.EventHandler(this.btnArrowBrush_Click);
      // 
      // imlToolbar
      // 
      this.imlToolbar.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imlToolbar.ImageStream")));
      this.imlToolbar.TransparentColor = System.Drawing.Color.Transparent;
      this.imlToolbar.Images.SetKeyName(0, "Subjects");
      this.imlToolbar.Images.SetKeyName(1, "Properties");
      this.imlToolbar.Images.SetKeyName(2, "Design");
      this.imlToolbar.Images.SetKeyName(3, "Color");
      this.imlToolbar.Images.SetKeyName(4, "Font");
      this.imlToolbar.Images.SetKeyName(5, "Brush");
      // 
      // chbArrowFont
      // 
      this.chbArrowFont.AutoSize = true;
      this.chbArrowFont.Checked = true;
      this.chbArrowFont.CheckState = System.Windows.Forms.CheckState.Checked;
      this.chbArrowFont.Location = new System.Drawing.Point(6, 54);
      this.chbArrowFont.Name = "chbArrowFont";
      this.chbArrowFont.Size = new System.Drawing.Size(47, 17);
      this.chbArrowFont.TabIndex = 1;
      this.chbArrowFont.Text = "Font";
      this.chbArrowFont.UseVisualStyleBackColor = true;
      this.chbArrowFont.CheckedChanged += new System.EventHandler(this.ArrowStyleCheckedChanged);
      // 
      // btnArrowFont
      // 
      this.btnArrowFont.ImageKey = "Font";
      this.btnArrowFont.ImageList = this.imlToolbar;
      this.btnArrowFont.Location = new System.Drawing.Point(64, 48);
      this.btnArrowFont.Name = "btnArrowFont";
      this.btnArrowFont.Size = new System.Drawing.Size(23, 23);
      this.btnArrowFont.TabIndex = 0;
      this.btnArrowFont.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.btnArrowFont.UseVisualStyleBackColor = true;
      this.btnArrowFont.Click += new System.EventHandler(this.btnArrowFont_Click);
      // 
      // chbArrowPen
      // 
      this.chbArrowPen.AutoSize = true;
      this.chbArrowPen.Checked = true;
      this.chbArrowPen.CheckState = System.Windows.Forms.CheckState.Checked;
      this.chbArrowPen.Location = new System.Drawing.Point(6, 25);
      this.chbArrowPen.Name = "chbArrowPen";
      this.chbArrowPen.Size = new System.Drawing.Size(45, 17);
      this.chbArrowPen.TabIndex = 1;
      this.chbArrowPen.Text = "Pen";
      this.chbArrowPen.UseVisualStyleBackColor = true;
      this.chbArrowPen.CheckedChanged += new System.EventHandler(this.ArrowStyleCheckedChanged);
      // 
      // btnArrowPen
      // 
      this.btnArrowPen.ImageKey = "Color";
      this.btnArrowPen.ImageList = this.imlToolbar;
      this.btnArrowPen.Location = new System.Drawing.Point(64, 19);
      this.btnArrowPen.Name = "btnArrowPen";
      this.btnArrowPen.Size = new System.Drawing.Size(23, 23);
      this.btnArrowPen.TabIndex = 0;
      this.btnArrowPen.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.btnArrowPen.UseVisualStyleBackColor = true;
      this.btnArrowPen.Click += new System.EventHandler(this.btnArrowPen_Click);
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(3, 108);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(32, 13);
      this.label3.TabIndex = 42;
      this.label3.Text = "scale";
      // 
      // grpArrow
      // 
      this.grpArrow.Controls.Add(this.chbBubbleBrush);
      this.grpArrow.Controls.Add(this.nudBubbleFactor);
      this.grpArrow.Controls.Add(this.chbBubbleFont);
      this.grpArrow.Controls.Add(this.chbBubblePen);
      this.grpArrow.Controls.Add(this.btnBubbleBrush);
      this.grpArrow.Controls.Add(this.btnBubbleFont);
      this.grpArrow.Controls.Add(this.btnBubblePen);
      this.grpArrow.Controls.Add(this.label1);
      this.grpArrow.Location = new System.Drawing.Point(4, 238);
      this.grpArrow.Name = "grpArrow";
      this.grpArrow.Size = new System.Drawing.Size(90, 135);
      this.grpArrow.TabIndex = 45;
      this.grpArrow.TabStop = false;
      this.grpArrow.Text = "Bubble style";
      // 
      // chbBubbleBrush
      // 
      this.chbBubbleBrush.AutoSize = true;
      this.chbBubbleBrush.Checked = true;
      this.chbBubbleBrush.CheckState = System.Windows.Forms.CheckState.Checked;
      this.chbBubbleBrush.Location = new System.Drawing.Point(6, 81);
      this.chbBubbleBrush.Name = "chbBubbleBrush";
      this.chbBubbleBrush.Size = new System.Drawing.Size(53, 17);
      this.chbBubbleBrush.TabIndex = 1;
      this.chbBubbleBrush.Text = "Brush";
      this.chbBubbleBrush.UseVisualStyleBackColor = true;
      this.chbBubbleBrush.CheckedChanged += new System.EventHandler(this.BubbleStyleCheckedChanged);
      // 
      // nudBubbleFactor
      // 
      this.nudBubbleFactor.DecimalPlaces = 1;
      this.nudBubbleFactor.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
      this.nudBubbleFactor.Location = new System.Drawing.Point(34, 106);
      this.nudBubbleFactor.Name = "nudBubbleFactor";
      this.nudBubbleFactor.Size = new System.Drawing.Size(50, 20);
      this.nudBubbleFactor.TabIndex = 40;
      this.nudBubbleFactor.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.nudBubbleFactor.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
      this.nudBubbleFactor.ValueChanged += new System.EventHandler(this.nudBubbleFactor_ValueChanged);
      // 
      // chbBubbleFont
      // 
      this.chbBubbleFont.AutoSize = true;
      this.chbBubbleFont.Checked = true;
      this.chbBubbleFont.CheckState = System.Windows.Forms.CheckState.Checked;
      this.chbBubbleFont.Location = new System.Drawing.Point(6, 52);
      this.chbBubbleFont.Name = "chbBubbleFont";
      this.chbBubbleFont.Size = new System.Drawing.Size(47, 17);
      this.chbBubbleFont.TabIndex = 1;
      this.chbBubbleFont.Text = "Font";
      this.chbBubbleFont.UseVisualStyleBackColor = true;
      this.chbBubbleFont.CheckedChanged += new System.EventHandler(this.BubbleStyleCheckedChanged);
      // 
      // chbBubblePen
      // 
      this.chbBubblePen.AutoSize = true;
      this.chbBubblePen.Checked = true;
      this.chbBubblePen.CheckState = System.Windows.Forms.CheckState.Checked;
      this.chbBubblePen.Location = new System.Drawing.Point(6, 23);
      this.chbBubblePen.Name = "chbBubblePen";
      this.chbBubblePen.Size = new System.Drawing.Size(45, 17);
      this.chbBubblePen.TabIndex = 1;
      this.chbBubblePen.Text = "Pen";
      this.chbBubblePen.UseVisualStyleBackColor = true;
      this.chbBubblePen.CheckedChanged += new System.EventHandler(this.BubbleStyleCheckedChanged);
      // 
      // btnBubbleBrush
      // 
      this.btnBubbleBrush.ImageKey = "Brush";
      this.btnBubbleBrush.ImageList = this.imlToolbar;
      this.btnBubbleBrush.Location = new System.Drawing.Point(61, 77);
      this.btnBubbleBrush.Name = "btnBubbleBrush";
      this.btnBubbleBrush.Size = new System.Drawing.Size(23, 23);
      this.btnBubbleBrush.TabIndex = 0;
      this.btnBubbleBrush.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.btnBubbleBrush.UseVisualStyleBackColor = true;
      this.btnBubbleBrush.Click += new System.EventHandler(this.btnBubbleBrush_Click);
      // 
      // btnBubbleFont
      // 
      this.btnBubbleFont.ImageKey = "Font";
      this.btnBubbleFont.ImageList = this.imlToolbar;
      this.btnBubbleFont.Location = new System.Drawing.Point(61, 48);
      this.btnBubbleFont.Name = "btnBubbleFont";
      this.btnBubbleFont.Size = new System.Drawing.Size(23, 23);
      this.btnBubbleFont.TabIndex = 0;
      this.btnBubbleFont.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.btnBubbleFont.UseVisualStyleBackColor = true;
      this.btnBubbleFont.Click += new System.EventHandler(this.btnBubbleFont_Click);
      // 
      // btnBubblePen
      // 
      this.btnBubblePen.ImageKey = "Color";
      this.btnBubblePen.ImageList = this.imlToolbar;
      this.btnBubblePen.Location = new System.Drawing.Point(61, 19);
      this.btnBubblePen.Name = "btnBubblePen";
      this.btnBubblePen.Size = new System.Drawing.Size(23, 23);
      this.btnBubblePen.TabIndex = 0;
      this.btnBubblePen.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.btnBubblePen.UseVisualStyleBackColor = true;
      this.btnBubblePen.Click += new System.EventHandler(this.btnBubblePen_Click);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(2, 108);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(32, 13);
      this.label1.TabIndex = 42;
      this.label1.Text = "scale";
      // 
      // tbpFixationSubjects
      // 
      this.tbpFixationSubjects.Controls.Add(this.splitContainer3);
      this.tbpFixationSubjects.ImageKey = "Subjects";
      this.tbpFixationSubjects.Location = new System.Drawing.Point(4, 23);
      this.tbpFixationSubjects.Name = "tbpFixationSubjects";
      this.tbpFixationSubjects.Padding = new System.Windows.Forms.Padding(3);
      this.tbpFixationSubjects.Size = new System.Drawing.Size(192, 435);
      this.tbpFixationSubjects.TabIndex = 1;
      this.tbpFixationSubjects.Text = "Subjects";
      this.tbpFixationSubjects.UseVisualStyleBackColor = true;
      // 
      // splitContainer3
      // 
      this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
      this.splitContainer3.IsSplitterFixed = true;
      this.splitContainer3.Location = new System.Drawing.Point(3, 3);
      this.splitContainer3.Name = "splitContainer3";
      this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // splitContainer3.Panel1
      // 
      this.splitContainer3.Panel1.Controls.Add(this.btnDeselectAllSubjects);
      this.splitContainer3.Panel1.Controls.Add(this.btnSelectAllSubjects);
      this.splitContainer3.Panel1MinSize = 20;
      // 
      // splitContainer3.Panel2
      // 
      this.splitContainer3.Panel2.Controls.Add(this.trvSubjects);
      this.splitContainer3.Size = new System.Drawing.Size(186, 429);
      this.splitContainer3.SplitterDistance = 20;
      this.splitContainer3.SplitterWidth = 1;
      this.splitContainer3.TabIndex = 21;
      // 
      // btnDeselectAllSubjects
      // 
      this.btnDeselectAllSubjects.Dock = System.Windows.Forms.DockStyle.Right;
      this.btnDeselectAllSubjects.Location = new System.Drawing.Point(116, 0);
      this.btnDeselectAllSubjects.Name = "btnDeselectAllSubjects";
      this.btnDeselectAllSubjects.Size = new System.Drawing.Size(70, 20);
      this.btnDeselectAllSubjects.TabIndex = 19;
      this.btnDeselectAllSubjects.Text = "Deselect all";
      this.btnDeselectAllSubjects.UseVisualStyleBackColor = true;
      this.btnDeselectAllSubjects.Click += new System.EventHandler(this.btnDeselectAllSubjects_Click);
      // 
      // btnSelectAllSubjects
      // 
      this.btnSelectAllSubjects.Dock = System.Windows.Forms.DockStyle.Left;
      this.btnSelectAllSubjects.Location = new System.Drawing.Point(0, 0);
      this.btnSelectAllSubjects.Name = "btnSelectAllSubjects";
      this.btnSelectAllSubjects.Size = new System.Drawing.Size(70, 20);
      this.btnSelectAllSubjects.TabIndex = 20;
      this.btnSelectAllSubjects.Text = "Select all";
      this.btnSelectAllSubjects.UseVisualStyleBackColor = true;
      this.btnSelectAllSubjects.Click += new System.EventHandler(this.btnSelectAllSubjects_Click);
      // 
      // toolStrip3
      // 
      this.toolStrip3.AllowMerge = false;
      this.toolStrip3.Dock = System.Windows.Forms.DockStyle.None;
      this.toolStrip3.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
      this.toolStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel4,
            this.toolStripSeparator9,
            this.btnEye,
            this.btnMouse,
            this.toolStripSeparator10,
            this.btnRecalculateStatistics});
      this.toolStrip3.Location = new System.Drawing.Point(0, 0);
      this.toolStrip3.Name = "toolStrip3";
      this.toolStrip3.Size = new System.Drawing.Size(200, 25);
      this.toolStrip3.Stretch = true;
      this.toolStrip3.TabIndex = 0;
      // 
      // toolStripLabel4
      // 
      this.toolStripLabel4.Name = "toolStripLabel4";
      this.toolStripLabel4.Size = new System.Drawing.Size(76, 22);
      this.toolStripLabel4.Text = "AOI Statistics";
      // 
      // toolStripSeparator9
      // 
      this.toolStripSeparator9.Name = "toolStripSeparator9";
      this.toolStripSeparator9.Size = new System.Drawing.Size(6, 25);
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
      this.btnEye.Text = "Use gaze data for aoi statistic";
      this.btnEye.CheckedChanged += new System.EventHandler(this.btnEye_CheckedChanged);
      // 
      // btnMouse
      // 
      this.btnMouse.CheckOnClick = true;
      this.btnMouse.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnMouse.Image = global::Ogama.Properties.Resources.Mouse;
      this.btnMouse.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnMouse.Name = "btnMouse";
      this.btnMouse.Size = new System.Drawing.Size(23, 22);
      this.btnMouse.Text = "use mouse data for aoi statistic";
      this.btnMouse.CheckedChanged += new System.EventHandler(this.btnMouse_CheckedChanged);
      // 
      // toolStripSeparator10
      // 
      this.toolStripSeparator10.Name = "toolStripSeparator10";
      this.toolStripSeparator10.Size = new System.Drawing.Size(6, 25);
      // 
      // btnRecalculateStatistics
      // 
      this.btnRecalculateStatistics.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnRecalculateStatistics.Image = global::Ogama.Properties.Resources.CalculatorHS;
      this.btnRecalculateStatistics.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnRecalculateStatistics.Name = "btnRecalculateStatistics";
      this.btnRecalculateStatistics.Size = new System.Drawing.Size(23, 22);
      this.btnRecalculateStatistics.Text = "Recalculate";
      this.btnRecalculateStatistics.Click += new System.EventHandler(this.btnRecalculateStatistics_Click);
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
      this.pnlCanvas.Size = new System.Drawing.Size(809, 487);
      this.pnlCanvas.TabIndex = 2;
      // 
      // pnlPicture
      // 
      this.pnlPicture.Controls.Add(this.aoiPicture);
      this.pnlPicture.Location = new System.Drawing.Point(177, 70);
      this.pnlPicture.Margin = new System.Windows.Forms.Padding(0);
      this.pnlPicture.Name = "pnlPicture";
      this.pnlPicture.Size = new System.Drawing.Size(300, 200);
      this.pnlPicture.TabIndex = 0;
      // 
      // aoiPicture
      // 
      this.aoiPicture.AnimationInterval = 100;
      this.aoiPicture.BackColor = global::Ogama.Properties.Settings.Default.BackgroundColorForms;
      this.aoiPicture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
      this.aoiPicture.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::Ogama.Properties.Settings.Default, "BackgroundColorForms", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.aoiPicture.InvalidateInterval = 500;
      this.aoiPicture.Location = new System.Drawing.Point(0, 0);
      this.aoiPicture.Margin = new System.Windows.Forms.Padding(0);
      this.aoiPicture.Name = "aoiPicture";
      this.aoiPicture.Size = new System.Drawing.Size(300, 200);
      this.aoiPicture.TabIndex = 0;
      this.aoiPicture.TabStop = false;
      this.aoiPicture.ZoomFactor = 0F;
      this.aoiPicture.ShapeChanged += new ShapeEventHandler(this.aoiPicture_ShapeChanged);
      this.aoiPicture.ShapeDeleted += new ShapeEventHandler(this.aoiPicture_ShapeDeleted);
      this.aoiPicture.ShapeAdded += new ShapeEventHandler(this.aoiPicture_ShapeAdded);
      // 
      // toolStrip2
      // 
      this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
      this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnSeekNextSlide,
            this.btnSeekPreviousSlide,
            this.toolStripSeparator6,
            this.trialTimeLine,
            this.toolStripSeparator12,
            this.trbZoom});
      this.toolStrip2.Location = new System.Drawing.Point(0, 0);
      this.toolStrip2.Name = "toolStrip2";
      this.toolStrip2.Size = new System.Drawing.Size(1013, 26);
      this.toolStrip2.Stretch = true;
      this.toolStrip2.TabIndex = 0;
      this.toolStrip2.Text = "toolStrip2";
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
      this.btnSeekPreviousSlide.Text = "Seek to previous slide.";
      this.btnSeekPreviousSlide.Click += new System.EventHandler(this.BtnSeekPreviousSlideClick);
      // 
      // toolStripSeparator6
      // 
      this.toolStripSeparator6.Name = "toolStripSeparator6";
      this.toolStripSeparator6.Size = new System.Drawing.Size(6, 26);
      // 
      // trialTimeLine
      // 
      this.trialTimeLine.Duration = 0;
      this.trialTimeLine.Name = "trialTimeLine";
      this.trialTimeLine.ShowCaret = false;
      this.trialTimeLine.ShowTimes = false;
      this.trialTimeLine.Size = new System.Drawing.Size(819, 23);
      this.trialTimeLine.Text = "trialTimeLine";
      // 
      // toolStrip1
      // 
      this.toolStrip1.DataBindings.Add(new System.Windows.Forms.Binding("Location", global::Ogama.Properties.Settings.Default, "AOIToolbarLocation", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
      this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cbbTrial,
            this.toolStripSeparator1,
            this.toolStripLabel1,
            this.btnNewRectangle,
            this.btnNewEllipse,
            this.btnNewPolyline,
            this.btnNewAOIGrid,
            this.toolStripSeparator11,
            this.btnAddShapeGroup,
            this.toolStripSeparator2,
            this.toolStripLabel2,
            this.btnReadAOITable,
            this.btnImportTargets,
            this.btnExportAOITable,
            this.btnDeleteAOIs,
            this.toolStripSeparator3,
            this.toolStripLabel3,
            this.btnShowAOIStatistics,
            this.btnShowHideTable,
            this.btnStyleNormal,
            this.btnStyleTarget,
            this.btnStyleSearchRect,
            this.toolStripSeparator4,
            this.btnSaveAOI,
            this.toolStripSeparator5,
            this.btnHelp});
      this.toolStrip1.Location = global::Ogama.Properties.Settings.Default.AOIToolbarLocation;
      this.toolStrip1.Name = "toolStrip1";
      this.toolStrip1.Size = new System.Drawing.Size(845, 25);
      this.toolStrip1.TabIndex = 1;
      this.toolStrip1.Text = "toolStrip1";
      // 
      // cbbTrial
      // 
      this.cbbTrial.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbbTrial.Name = "cbbTrial";
      this.cbbTrial.Size = new System.Drawing.Size(250, 25);
      this.cbbTrial.ToolTipText = "select stimulus image";
      // 
      // toolStripSeparator1
      // 
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
      // 
      // toolStripLabel1
      // 
      this.toolStripLabel1.Name = "toolStripLabel1";
      this.toolStripLabel1.Size = new System.Drawing.Size(64, 22);
      this.toolStripLabel1.Text = "Create AOI";
      // 
      // btnNewRectangle
      // 
      this.btnNewRectangle.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnNewRectangle.Image = global::Ogama.Properties.Resources.Rectangle;
      this.btnNewRectangle.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnNewRectangle.Name = "btnNewRectangle";
      this.btnNewRectangle.Size = new System.Drawing.Size(23, 22);
      this.btnNewRectangle.ToolTipText = "create new rectangular AOI";
      this.btnNewRectangle.Click += new System.EventHandler(this.btnNewRectangle_Click);
      // 
      // btnNewEllipse
      // 
      this.btnNewEllipse.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnNewEllipse.Image = global::Ogama.Properties.Resources.Ellipse;
      this.btnNewEllipse.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnNewEllipse.Name = "btnNewEllipse";
      this.btnNewEllipse.Size = new System.Drawing.Size(23, 22);
      this.btnNewEllipse.ToolTipText = "create new ellipsoid AOI";
      this.btnNewEllipse.Click += new System.EventHandler(this.btnNewEllipse_Click);
      // 
      // btnNewPolyline
      // 
      this.btnNewPolyline.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnNewPolyline.Image = global::Ogama.Properties.Resources.Polyline;
      this.btnNewPolyline.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnNewPolyline.Name = "btnNewPolyline";
      this.btnNewPolyline.Size = new System.Drawing.Size(23, 22);
      this.btnNewPolyline.ToolTipText = "create new polylined AOI";
      this.btnNewPolyline.Click += new System.EventHandler(this.btnNewPolyline_Click);
      // 
      // btnNewAOIGrid
      // 
      this.btnNewAOIGrid.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnNewAOIGrid.Image = global::Ogama.Properties.Resources.ShowGridlines2HS;
      this.btnNewAOIGrid.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnNewAOIGrid.Name = "btnNewAOIGrid";
      this.btnNewAOIGrid.Size = new System.Drawing.Size(23, 22);
      this.btnNewAOIGrid.Text = "Add AOI grid";
      this.btnNewAOIGrid.Click += new System.EventHandler(this.btnNewAOIGrid_Click);
      // 
      // toolStripSeparator11
      // 
      this.toolStripSeparator11.Name = "toolStripSeparator11";
      this.toolStripSeparator11.Size = new System.Drawing.Size(6, 25);
      // 
      // btnAddShapeGroup
      // 
      this.btnAddShapeGroup.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnAddShapeGroup.Image = global::Ogama.Properties.Resources.Filter2HS;
      this.btnAddShapeGroup.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnAddShapeGroup.Name = "btnAddShapeGroup";
      this.btnAddShapeGroup.Size = new System.Drawing.Size(23, 22);
      this.btnAddShapeGroup.Text = "Add shape group";
      this.btnAddShapeGroup.Click += new System.EventHandler(this.btnAddShapeGroup_Click);
      // 
      // toolStripSeparator2
      // 
      this.toolStripSeparator2.Name = "toolStripSeparator2";
      this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
      // 
      // toolStripLabel2
      // 
      this.toolStripLabel2.Name = "toolStripLabel2";
      this.toolStripLabel2.Size = new System.Drawing.Size(76, 22);
      this.toolStripLabel2.Text = "ImportExport";
      // 
      // btnReadAOITable
      // 
      this.btnReadAOITable.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnReadAOITable.Image = global::Ogama.Properties.Resources.MagicWand;
      this.btnReadAOITable.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnReadAOITable.Name = "btnReadAOITable";
      this.btnReadAOITable.Size = new System.Drawing.Size(23, 22);
      this.btnReadAOITable.ToolTipText = "import AOI data";
      this.btnReadAOITable.Click += new System.EventHandler(this.btnImportReadAOITable_Click);
      // 
      // btnImportTargets
      // 
      this.btnImportTargets.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnImportTargets.Image = global::Ogama.Properties.Resources.GenericShape;
      this.btnImportTargets.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnImportTargets.Name = "btnImportTargets";
      this.btnImportTargets.Size = new System.Drawing.Size(23, 22);
      this.btnImportTargets.Text = "Import targets of slideshow";
      this.btnImportTargets.ToolTipText = "Convert target areas of the slideshow into areas of interest.";
      this.btnImportTargets.Click += new System.EventHandler(this.btnImportTargets_Click);
      // 
      // btnExportAOITable
      // 
      this.btnExportAOITable.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnExportAOITable.Image = ((System.Drawing.Image)(resources.GetObject("btnExportAOITable.Image")));
      this.btnExportAOITable.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnExportAOITable.Name = "btnExportAOITable";
      this.btnExportAOITable.Size = new System.Drawing.Size(23, 22);
      this.btnExportAOITable.ToolTipText = "export AOI data";
      this.btnExportAOITable.Click += new System.EventHandler(this.btnExportAOITable_Click);
      // 
      // btnDeleteAOIs
      // 
      this.btnDeleteAOIs.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnDeleteAOIs.Image = ((System.Drawing.Image)(resources.GetObject("btnDeleteAOIs.Image")));
      this.btnDeleteAOIs.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnDeleteAOIs.Name = "btnDeleteAOIs";
      this.btnDeleteAOIs.Size = new System.Drawing.Size(23, 22);
      this.btnDeleteAOIs.ToolTipText = "delete the whole AOI database";
      this.btnDeleteAOIs.Click += new System.EventHandler(this.btnDeleteAOIs_Click);
      // 
      // toolStripSeparator3
      // 
      this.toolStripSeparator3.Name = "toolStripSeparator3";
      this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
      // 
      // toolStripLabel3
      // 
      this.toolStripLabel3.Name = "toolStripLabel3";
      this.toolStripLabel3.Size = new System.Drawing.Size(37, 22);
      this.toolStripLabel3.Text = "Styles";
      // 
      // btnShowAOIStatistics
      // 
      this.btnShowAOIStatistics.Checked = true;
      this.btnShowAOIStatistics.CheckOnClick = true;
      this.btnShowAOIStatistics.CheckState = System.Windows.Forms.CheckState.Checked;
      this.btnShowAOIStatistics.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnShowAOIStatistics.Image = global::Ogama.Properties.Resources.Scanpath;
      this.btnShowAOIStatistics.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnShowAOIStatistics.Name = "btnShowAOIStatistics";
      this.btnShowAOIStatistics.Size = new System.Drawing.Size(23, 22);
      this.btnShowAOIStatistics.Text = "Show statistics toolbar";
      this.btnShowAOIStatistics.Click += new System.EventHandler(this.btnShowAOIStatistics_Click);
      // 
      // btnShowHideTable
      // 
      this.btnShowHideTable.Checked = true;
      this.btnShowHideTable.CheckOnClick = true;
      this.btnShowHideTable.CheckState = System.Windows.Forms.CheckState.Checked;
      this.btnShowHideTable.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnShowHideTable.Image = global::Ogama.Properties.Resources.TableHS;
      this.btnShowHideTable.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnShowHideTable.Name = "btnShowHideTable";
      this.btnShowHideTable.Size = new System.Drawing.Size(23, 22);
      this.btnShowHideTable.ToolTipText = "Check this button to show the AOI database table.";
      this.btnShowHideTable.Click += new System.EventHandler(this.btnShowHideTable_Click);
      // 
      // btnStyleNormal
      // 
      this.btnStyleNormal.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnStyleNormal.Image = ((System.Drawing.Image)(resources.GetObject("btnStyleNormal.Image")));
      this.btnStyleNormal.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnStyleNormal.Name = "btnStyleNormal";
      this.btnStyleNormal.Size = new System.Drawing.Size(23, 22);
      this.btnStyleNormal.ToolTipText = "modify text and pen style \r\nof default elements";
      this.btnStyleNormal.Click += new System.EventHandler(this.btnStyleNormal_Click);
      // 
      // btnStyleTarget
      // 
      this.btnStyleTarget.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnStyleTarget.Image = ((System.Drawing.Image)(resources.GetObject("btnStyleTarget.Image")));
      this.btnStyleTarget.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnStyleTarget.Name = "btnStyleTarget";
      this.btnStyleTarget.Size = new System.Drawing.Size(23, 22);
      this.btnStyleTarget.ToolTipText = "modify text and pen style \r\nof target elements";
      this.btnStyleTarget.Click += new System.EventHandler(this.btnStyleTarget_Click);
      // 
      // btnStyleSearchRect
      // 
      this.btnStyleSearchRect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnStyleSearchRect.Image = ((System.Drawing.Image)(resources.GetObject("btnStyleSearchRect.Image")));
      this.btnStyleSearchRect.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnStyleSearchRect.Name = "btnStyleSearchRect";
      this.btnStyleSearchRect.Size = new System.Drawing.Size(23, 22);
      this.btnStyleSearchRect.ToolTipText = "modify text and pen style \r\nof search rectangle elements";
      this.btnStyleSearchRect.Click += new System.EventHandler(this.btnStyleSearchRect_Click);
      // 
      // toolStripSeparator4
      // 
      this.toolStripSeparator4.Name = "toolStripSeparator4";
      this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
      // 
      // btnSaveAOI
      // 
      this.btnSaveAOI.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnSaveAOI.Image = global::Ogama.Properties.Resources.saveHS;
      this.btnSaveAOI.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnSaveAOI.Name = "btnSaveAOI";
      this.btnSaveAOI.Size = new System.Drawing.Size(23, 22);
      this.btnSaveAOI.ToolTipText = "Immediately submit changes to database";
      this.btnSaveAOI.Click += new System.EventHandler(this.btnSaveAOI_Click);
      // 
      // toolStripSeparator5
      // 
      this.toolStripSeparator5.Name = "toolStripSeparator5";
      this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
      // 
      // btnHelp
      // 
      this.btnHelp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnHelp.Image = global::Ogama.Properties.Resources.HelpBmp;
      this.btnHelp.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnHelp.Name = "btnHelp";
      this.btnHelp.Size = new System.Drawing.Size(23, 22);
      this.btnHelp.ToolTipText = "show help for this window";
      // 
      // toolStripContainer1
      // 
      // 
      // toolStripContainer1.ContentPanel
      // 
      this.toolStripContainer1.ContentPanel.Controls.Add(this.spcPictureTools);
      this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(1013, 658);
      this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
      this.toolStripContainer1.Name = "toolStripContainer1";
      this.toolStripContainer1.Size = new System.Drawing.Size(1013, 683);
      this.toolStripContainer1.TabIndex = 4;
      this.toolStripContainer1.Text = "toolStripContainer1";
      // 
      // toolStripContainer1.TopToolStripPanel
      // 
      this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip1);
      // 
      // saveFileDialogExport
      // 
      this.saveFileDialogExport.DefaultExt = "txt";
      this.saveFileDialogExport.FileName = "*.txt";
      this.saveFileDialogExport.Filter = "Text files|*.txt";
      this.saveFileDialogExport.Title = "Save areas of interest table ...";
      // 
      // toolStripSeparator12
      // 
      this.toolStripSeparator12.Name = "toolStripSeparator12";
      this.toolStripSeparator12.Size = new System.Drawing.Size(6, 26);
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
      // AOIModule
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1013, 683);
      this.Controls.Add(this.toolStripContainer1);
      this.DataBindings.Add(new System.Windows.Forms.Binding("Location", global::Ogama.Properties.Settings.Default, "AOIModuleLocation", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.KeyPreview = true;
      this.Location = global::Ogama.Properties.Settings.Default.AOIModuleLocation;
      this.Logo = global::Ogama.Properties.Resources.AOILogo;
      this.Name = "AOIModule";
      this.Text = "Areas of Interest (AOI) Module";
      this.Load += new System.EventHandler(this.frmAOIWindow_Load);
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmAOIWindow_FormClosing);
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
      ((System.ComponentModel.ISupportInitialize)(this.dgvAOIs)).EndInit();
      this.cmnuDataGridView.ResumeLayout(false);
      this.spcPictureTools.Panel1.ResumeLayout(false);
      this.spcPictureTools.Panel2.ResumeLayout(false);
      this.spcPictureTools.ResumeLayout(false);
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel2.ResumeLayout(false);
      this.splitContainer1.Panel2.PerformLayout();
      this.splitContainer1.ResumeLayout(false);
      this.spcStatisticsPicture.Panel1.ResumeLayout(false);
      this.spcStatisticsPicture.Panel2.ResumeLayout(false);
      this.spcStatisticsPicture.ResumeLayout(false);
      this.toolStripContainer2.ContentPanel.ResumeLayout(false);
      this.toolStripContainer2.TopToolStripPanel.ResumeLayout(false);
      this.toolStripContainer2.TopToolStripPanel.PerformLayout();
      this.toolStripContainer2.ResumeLayout(false);
      this.toolStripContainer2.PerformLayout();
      this.tabControl1.ResumeLayout(false);
      this.tbpFixationOptions.ResumeLayout(false);
      this.groupBox3.ResumeLayout(false);
      this.groupBox3.PerformLayout();
      this.groupBox2.ResumeLayout(false);
      this.groupBox2.PerformLayout();
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.nudArrowFactor)).EndInit();
      this.grpArrow.ResumeLayout(false);
      this.grpArrow.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.nudBubbleFactor)).EndInit();
      this.tbpFixationSubjects.ResumeLayout(false);
      this.splitContainer3.Panel1.ResumeLayout(false);
      this.splitContainer3.Panel2.ResumeLayout(false);
      this.splitContainer3.ResumeLayout(false);
      this.toolStrip3.ResumeLayout(false);
      this.toolStrip3.PerformLayout();
      this.pnlCanvas.ResumeLayout(false);
      this.pnlPicture.ResumeLayout(false);
      this.toolStrip2.ResumeLayout(false);
      this.toolStrip2.PerformLayout();
      this.toolStrip1.ResumeLayout(false);
      this.toolStrip1.PerformLayout();
      this.toolStripContainer1.ContentPanel.ResumeLayout(false);
      this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
      this.toolStripContainer1.TopToolStripPanel.PerformLayout();
      this.toolStripContainer1.ResumeLayout(false);
      this.toolStripContainer1.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.ToolTip toolTip1;
    private System.Windows.Forms.SaveFileDialog sfdExport;
    private System.Windows.Forms.SplitContainer spcPictureTools;
    private System.Windows.Forms.ToolStripContainer toolStripContainer1;
    private System.Windows.Forms.ToolStrip toolStrip1;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    private System.Windows.Forms.ToolStripButton btnNewRectangle;
    private System.Windows.Forms.ToolStripButton btnNewEllipse;
    private System.Windows.Forms.ToolStripButton btnNewPolyline;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    private System.Windows.Forms.ToolStripButton btnReadAOITable;
    private System.Windows.Forms.ToolStripButton btnExportAOITable;
    private System.Windows.Forms.ToolStripButton btnDeleteAOIs;
    private AOIPicture aoiPicture;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
    private System.Windows.Forms.ToolStripButton btnHelp;
    private System.Windows.Forms.ToolStripButton btnStyleNormal;
    private System.Windows.Forms.ToolStripButton btnStyleTarget;
    private System.Windows.Forms.ToolStripButton btnStyleSearchRect;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
    private System.Windows.Forms.DataGridView dgvAOIs;
    private System.Windows.Forms.ToolStripComboBox cbbTrial;
    private System.Windows.Forms.SaveFileDialog saveFileDialogExport;
    private System.Windows.Forms.ToolStripLabel toolStripLabel1;
    private System.Windows.Forms.ToolStripLabel toolStripLabel2;
    private System.Windows.Forms.ToolStripLabel toolStripLabel3;
    private System.Windows.Forms.Panel pnlCanvas;
    private System.Windows.Forms.Panel pnlPicture;
    private System.Windows.Forms.ToolStripButton btnShowHideTable;
    private System.Windows.Forms.ContextMenuStrip cmnuDataGridView;
    private System.Windows.Forms.ToolStripMenuItem cmnuSelectAll;
    private System.Windows.Forms.ToolStripMenuItem cmnuCopyToClipboard;
    private System.Windows.Forms.ToolStripMenuItem cmnuPasteFromClipboard;
    private System.Windows.Forms.ToolStripMenuItem cmnuSelectElement;
    private System.Windows.Forms.ToolStripButton btnSaveAOI;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
    private System.Windows.Forms.SplitContainer splitContainer1;
    private System.Windows.Forms.ToolStrip toolStrip2;
    private TrialTimeLine trialTimeLine;
    private System.Windows.Forms.ToolStripButton btnSeekNextSlide;
    private System.Windows.Forms.ToolStripButton btnSeekPreviousSlide;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
    private System.Windows.Forms.ToolStripMenuItem cmuAddShapeGroup;
    private System.Windows.Forms.ToolStripButton btnImportTargets;
    private System.Windows.Forms.SplitContainer spcStatisticsPicture;
    private System.Windows.Forms.ToolStripContainer toolStripContainer2;
    private System.Windows.Forms.ToolStrip toolStrip3;
    private System.Windows.Forms.ToolStripLabel toolStripLabel4;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
    private System.Windows.Forms.ToolStripButton btnEye;
    private System.Windows.Forms.ToolStripButton btnMouse;
    private System.Windows.Forms.RadioButton rdbGazeCompleteFixationTime;
    private System.Windows.Forms.RadioButton rdbTransitions;
    private System.Windows.Forms.RadioButton rdbGazeAverageFixationDuration;
    private System.Windows.Forms.RadioButton rdbGazeNumberOfFixations;
    private System.Windows.Forms.NumericUpDown nudBubbleFactor;
    private System.Windows.Forms.TabControl tabControl1;
    private System.Windows.Forms.TabPage tbpFixationOptions;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TabPage tbpFixationSubjects;
    private System.Windows.Forms.ImageList imlToolbar;
    private System.Windows.Forms.SplitContainer splitContainer3;
    private System.Windows.Forms.Button btnDeselectAllSubjects;
    private System.Windows.Forms.Button btnSelectAllSubjects;
    private System.Windows.Forms.NumericUpDown nudArrowFactor;
    private System.Windows.Forms.ToolStripButton btnShowAOIStatistics;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
    private System.Windows.Forms.ToolStripButton btnRecalculateStatistics;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.CheckBox chbArrowBrush;
    private System.Windows.Forms.Button btnArrowBrush;
    private System.Windows.Forms.CheckBox chbArrowFont;
    private System.Windows.Forms.Button btnArrowFont;
    private System.Windows.Forms.CheckBox chbArrowPen;
    private System.Windows.Forms.Button btnArrowPen;
    private System.Windows.Forms.GroupBox grpArrow;
    private System.Windows.Forms.CheckBox chbBubbleBrush;
    private System.Windows.Forms.CheckBox chbBubbleFont;
    private System.Windows.Forms.CheckBox chbBubblePen;
    private System.Windows.Forms.Button btnBubbleBrush;
    private System.Windows.Forms.Button btnBubbleFont;
    private System.Windows.Forms.Button btnBubblePen;
    private System.Windows.Forms.CheckBox chbHideAOIDescription;
    private System.Windows.Forms.GroupBox groupBox2;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.ToolStripButton btnNewAOIGrid;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator11;
    private System.Windows.Forms.ToolStripButton btnAddShapeGroup;
    private System.Windows.Forms.RadioButton rdbAbsoluteTransitions;
    private System.Windows.Forms.GroupBox groupBox3;
    private System.Windows.Forms.RadioButton rdbRelativeTransitions;
    private System.Windows.Forms.RadioButton rdbHideTransitions;
    private OgamaControls.CheckboxTreeView trvSubjects;
    private System.Windows.Forms.ImageList imlTreeViewSubjects;
    private System.Windows.Forms.DataGridViewTextBoxColumn colID;
    private System.Windows.Forms.DataGridViewTextBoxColumn colTrialID;
    private System.Windows.Forms.DataGridViewTextBoxColumn colSlideNr;
    private System.Windows.Forms.DataGridViewTextBoxColumn colShapeName;
    private System.Windows.Forms.DataGridViewTextBoxColumn colShapeType;
    private System.Windows.Forms.DataGridViewTextBoxColumn colShapeNumPts;
    private System.Windows.Forms.DataGridViewTextBoxColumn colShapePts;
    private OgamaControls.DataGridViewQuickComboBoxColumn colShapeGroup;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator12;
    private OgamaControls.ToolStripTrackBar trbZoom;
  }
}