namespace Ogama.Modules.SlideshowDesign
{
  partial class SlideshowModule
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SlideshowModule));
      this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
      this.spcTreeDetail = new System.Windows.Forms.SplitContainer();
      this.spcToolbarTreeView = new System.Windows.Forms.SplitContainer();
      this.toolStrip1 = new System.Windows.Forms.ToolStrip();
      this.btnAddFolder = new System.Windows.Forms.ToolStripButton();
      this.btnLevelDown = new System.Windows.Forms.ToolStripButton();
      this.btnLevelUp = new System.Windows.Forms.ToolStripButton();
      this.btnIndexUp = new System.Windows.Forms.ToolStripButton();
      this.btnIndexDown = new System.Windows.Forms.ToolStripButton();
      this.btnCustomShuffling = new System.Windows.Forms.ToolStripButton();
      this.trvSlideshow = new OgamaControls.MultiselectTreeView();
      this.cmuItemView = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.cmuDisable = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
      this.cmuShuffle = new System.Windows.Forms.ToolStripMenuItem();
      this.cmuDescription = new System.Windows.Forms.ToolStripMenuItem();
      this.cmuCountCombo = new System.Windows.Forms.ToolStripComboBox();
      this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
      this.cmuJoin = new System.Windows.Forms.ToolStripMenuItem();
      this.cmuCombineToTrial = new System.Windows.Forms.ToolStripMenuItem();
      this.cmuDelete = new System.Windows.Forms.ToolStripMenuItem();
      this.imlSlideTypes = new System.Windows.Forms.ImageList(this.components);
      this.spcSlidesProperties = new System.Windows.Forms.SplitContainer();
      this.lsvDetails = new OgamaControls.ObjectListView();
      this.colPosition = ((OgamaControls.OLVColumn)(new OgamaControls.OLVColumn()));
      this.spcPropertiesPreview = new System.Windows.Forms.SplitContainer();
      this.grbSlideProperties = new System.Windows.Forms.GroupBox();
      this.prgSlides = new System.Windows.Forms.PropertyGrid();
      this.grbPreview = new System.Windows.Forms.GroupBox();
      this.pnlCanvas = new System.Windows.Forms.Panel();
      this.pnlPicture = new OgamaControls.DoubleBufferPanel();
      this.slidePreviewPicture = new VectorGraphics.Canvas.Picture();
      this.tosStimuli = new System.Windows.Forms.ToolStrip();
      this.btnInstruction = new System.Windows.Forms.ToolStripButton();
      this.btnRtfInstruction = new System.Windows.Forms.ToolStripButton();
      this.btnImage = new System.Windows.Forms.ToolStripButton();
      this.btnShapes = new System.Windows.Forms.ToolStripButton();
      this.btnMixed = new System.Windows.Forms.ToolStripButton();
      this.btnInternet = new System.Windows.Forms.ToolStripButton();
      this.btnFlash = new System.Windows.Forms.ToolStripButton();
      this.btnDesktop = new System.Windows.Forms.ToolStripButton();
      this.btnBlank = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.btnSaveSlideshow = new System.Windows.Forms.ToolStripButton();
      this.btnImport = new System.Windows.Forms.ToolStripButton();
      this.btnExport = new System.Windows.Forms.ToolStripButton();
      this.btnImportFolderContent = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
      this.btnPreviewSlideshow = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
      this.btnHelp = new System.Windows.Forms.ToolStripButton();
      this.btnPrimary = new System.Windows.Forms.ToolStripButton();
      this.btnSecondary = new System.Windows.Forms.ToolStripButton();
      this.sfdSlideshow = new System.Windows.Forms.SaveFileDialog();
      this.ofdSlideshow = new System.Windows.Forms.OpenFileDialog();
      this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
      this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
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
      this.toolStripContainer1.ContentPanel.SuspendLayout();
      this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
      this.toolStripContainer1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.spcTreeDetail)).BeginInit();
      this.spcTreeDetail.Panel1.SuspendLayout();
      this.spcTreeDetail.Panel2.SuspendLayout();
      this.spcTreeDetail.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.spcToolbarTreeView)).BeginInit();
      this.spcToolbarTreeView.Panel1.SuspendLayout();
      this.spcToolbarTreeView.Panel2.SuspendLayout();
      this.spcToolbarTreeView.SuspendLayout();
      this.toolStrip1.SuspendLayout();
      this.cmuItemView.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.spcSlidesProperties)).BeginInit();
      this.spcSlidesProperties.Panel1.SuspendLayout();
      this.spcSlidesProperties.Panel2.SuspendLayout();
      this.spcSlidesProperties.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.lsvDetails)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.spcPropertiesPreview)).BeginInit();
      this.spcPropertiesPreview.Panel1.SuspendLayout();
      this.spcPropertiesPreview.Panel2.SuspendLayout();
      this.spcPropertiesPreview.SuspendLayout();
      this.grbSlideProperties.SuspendLayout();
      this.grbPreview.SuspendLayout();
      this.pnlCanvas.SuspendLayout();
      this.pnlPicture.SuspendLayout();
      this.tosStimuli.SuspendLayout();
      this.SuspendLayout();
      // 
      // toolStripContainer1
      // 
      // 
      // toolStripContainer1.ContentPanel
      // 
      this.toolStripContainer1.ContentPanel.Controls.Add(this.spcTreeDetail);
      this.toolStripContainer1.ContentPanel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(1294, 854);
      this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
      this.toolStripContainer1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.toolStripContainer1.Name = "toolStripContainer1";
      this.toolStripContainer1.Size = new System.Drawing.Size(1294, 882);
      this.toolStripContainer1.TabIndex = 0;
      this.toolStripContainer1.Text = "toolStripContainer1";
      // 
      // toolStripContainer1.TopToolStripPanel
      // 
      this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.tosStimuli);
      // 
      // spcTreeDetail
      // 
      this.spcTreeDetail.Dock = System.Windows.Forms.DockStyle.Fill;
      this.spcTreeDetail.Location = new System.Drawing.Point(0, 0);
      this.spcTreeDetail.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.spcTreeDetail.Name = "spcTreeDetail";
      // 
      // spcTreeDetail.Panel1
      // 
      this.spcTreeDetail.Panel1.Controls.Add(this.spcToolbarTreeView);
      // 
      // spcTreeDetail.Panel2
      // 
      this.spcTreeDetail.Panel2.Controls.Add(this.spcSlidesProperties);
      this.spcTreeDetail.Size = new System.Drawing.Size(1294, 854);
      this.spcTreeDetail.SplitterDistance = 430;
      this.spcTreeDetail.SplitterWidth = 6;
      this.spcTreeDetail.TabIndex = 1;
      // 
      // spcToolbarTreeView
      // 
      this.spcToolbarTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
      this.spcToolbarTreeView.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
      this.spcToolbarTreeView.IsSplitterFixed = true;
      this.spcToolbarTreeView.Location = new System.Drawing.Point(0, 0);
      this.spcToolbarTreeView.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.spcToolbarTreeView.Name = "spcToolbarTreeView";
      this.spcToolbarTreeView.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // spcToolbarTreeView.Panel1
      // 
      this.spcToolbarTreeView.Panel1.Controls.Add(this.toolStrip1);
      // 
      // spcToolbarTreeView.Panel2
      // 
      this.spcToolbarTreeView.Panel2.Controls.Add(this.trvSlideshow);
      this.spcToolbarTreeView.Size = new System.Drawing.Size(430, 854);
      this.spcToolbarTreeView.SplitterDistance = 25;
      this.spcToolbarTreeView.SplitterWidth = 2;
      this.spcToolbarTreeView.TabIndex = 2;
      // 
      // toolStrip1
      // 
      this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
      this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAddFolder,
            this.btnLevelDown,
            this.btnLevelUp,
            this.btnIndexUp,
            this.btnIndexDown,
            this.btnCustomShuffling});
      this.toolStrip1.Location = new System.Drawing.Point(0, 0);
      this.toolStrip1.Name = "toolStrip1";
      this.toolStrip1.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
      this.toolStrip1.Size = new System.Drawing.Size(430, 25);
      this.toolStrip1.TabIndex = 1;
      // 
      // btnAddFolder
      // 
      this.btnAddFolder.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnAddFolder.Image = global::Ogama.Properties.Resources.FolderNew16;
      this.btnAddFolder.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnAddFolder.Name = "btnAddFolder";
      this.btnAddFolder.Size = new System.Drawing.Size(23, 22);
      this.btnAddFolder.Text = "AddFolder";
      this.btnAddFolder.Click += new System.EventHandler(this.btnAddFolder_Click);
      // 
      // btnLevelDown
      // 
      this.btnLevelDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnLevelDown.Image = global::Ogama.Properties.Resources.OutdentHS;
      this.btnLevelDown.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnLevelDown.Name = "btnLevelDown";
      this.btnLevelDown.Size = new System.Drawing.Size(23, 22);
      this.btnLevelDown.Text = "Level down";
      this.btnLevelDown.Click += new System.EventHandler(this.btnLevelDown_Click);
      // 
      // btnLevelUp
      // 
      this.btnLevelUp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnLevelUp.Image = global::Ogama.Properties.Resources.IndentHS;
      this.btnLevelUp.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnLevelUp.Name = "btnLevelUp";
      this.btnLevelUp.Size = new System.Drawing.Size(23, 22);
      this.btnLevelUp.Text = "Level up";
      this.btnLevelUp.Click += new System.EventHandler(this.btnLevelUp_Click);
      // 
      // btnIndexUp
      // 
      this.btnIndexUp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnIndexUp.Image = global::Ogama.Properties.Resources.TopArrowHS;
      this.btnIndexUp.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnIndexUp.Name = "btnIndexUp";
      this.btnIndexUp.Size = new System.Drawing.Size(23, 22);
      this.btnIndexUp.Text = "Index up";
      this.btnIndexUp.Click += new System.EventHandler(this.btnIndexUp_Click);
      // 
      // btnIndexDown
      // 
      this.btnIndexDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnIndexDown.Image = global::Ogama.Properties.Resources.DownArrowHS;
      this.btnIndexDown.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnIndexDown.Name = "btnIndexDown";
      this.btnIndexDown.Size = new System.Drawing.Size(23, 22);
      this.btnIndexDown.Text = "Index down";
      this.btnIndexDown.Click += new System.EventHandler(this.btnIndexDown_Click);
      // 
      // btnCustomShuffling
      // 
      this.btnCustomShuffling.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnCustomShuffling.Image = global::Ogama.Properties.Resources.DiceHS16;
      this.btnCustomShuffling.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnCustomShuffling.Name = "btnCustomShuffling";
      this.btnCustomShuffling.Size = new System.Drawing.Size(23, 22);
      this.btnCustomShuffling.Text = "Define custom shuffling.";
      this.btnCustomShuffling.Click += new System.EventHandler(this.btnCustomShuffling_Click);
      // 
      // trvSlideshow
      // 
      this.trvSlideshow.AllowDrop = true;
      this.trvSlideshow.ContextMenuStrip = this.cmuItemView;
      this.trvSlideshow.Dock = System.Windows.Forms.DockStyle.Fill;
      this.trvSlideshow.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawAll;
      this.trvSlideshow.ImageKey = "Folder";
      this.trvSlideshow.ImageList = this.imlSlideTypes;
      this.trvSlideshow.Location = new System.Drawing.Point(0, 0);
      this.trvSlideshow.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.trvSlideshow.Name = "trvSlideshow";
      this.trvSlideshow.SelectedImageIndex = 0;
      this.trvSlideshow.SelectionBackColor = System.Drawing.SystemColors.Highlight;
      this.trvSlideshow.SelectionMode = OgamaControls.TreeViewSelectionMode.MultiSelectSameLevel;
      this.trvSlideshow.Size = new System.Drawing.Size(430, 827);
      this.trvSlideshow.TabIndex = 0;
      this.trvSlideshow.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.trvSlideshow_AfterLabelEdit);
      this.trvSlideshow.DrawNode += new System.Windows.Forms.DrawTreeNodeEventHandler(this.trvSlideshow_DrawNode);
      this.trvSlideshow.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.trvSlideshow_ItemDrag);
      this.trvSlideshow.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.trvSlideshow_AfterSelect);
      this.trvSlideshow.DragDrop += new System.Windows.Forms.DragEventHandler(this.trvSlideshow_DragDrop);
      this.trvSlideshow.DragEnter += new System.Windows.Forms.DragEventHandler(this.trvSlideshow_DragEnter);
      this.trvSlideshow.DragOver += new System.Windows.Forms.DragEventHandler(this.trvSlideshow_DragOver);
      this.trvSlideshow.DoubleClick += new System.EventHandler(this.trvSlideshow_DoubleClick);
      this.trvSlideshow.KeyDown += new System.Windows.Forms.KeyEventHandler(this.trvSlideshow_KeyDown);
      // 
      // cmuItemView
      // 
      this.cmuItemView.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmuDisable,
            this.toolStripSeparator4,
            this.cmuShuffle,
            this.cmuDescription,
            this.cmuCountCombo,
            this.toolStripSeparator2,
            this.cmuJoin,
            this.cmuCombineToTrial,
            this.cmuDelete});
      this.cmuItemView.Name = "contextMenuStrip1";
      this.cmuItemView.Size = new System.Drawing.Size(760, 233);
      this.cmuItemView.Opening += new System.ComponentModel.CancelEventHandler(this.cmuItemView_Opening);
      // 
      // cmuDisable
      // 
      this.cmuDisable.CheckOnClick = true;
      this.cmuDisable.Image = global::Ogama.Properties.Resources.Disabled16;
      this.cmuDisable.Name = "cmuDisable";
      this.cmuDisable.Size = new System.Drawing.Size(759, 30);
      this.cmuDisable.Text = "Disable Slide";
      this.cmuDisable.Click += new System.EventHandler(this.cmuDisable_Click);
      // 
      // toolStripSeparator4
      // 
      this.toolStripSeparator4.Name = "toolStripSeparator4";
      this.toolStripSeparator4.Size = new System.Drawing.Size(756, 6);
      this.toolStripSeparator4.Visible = false;
      // 
      // cmuShuffle
      // 
      this.cmuShuffle.CheckOnClick = true;
      this.cmuShuffle.Image = global::Ogama.Properties.Resources.DiceHS16;
      this.cmuShuffle.Name = "cmuShuffle";
      this.cmuShuffle.Size = new System.Drawing.Size(759, 30);
      this.cmuShuffle.Text = "Shuffle subitems of selected item";
      this.cmuShuffle.Click += new System.EventHandler(this.cmuShuffle_Click);
      // 
      // cmuDescription
      // 
      this.cmuDescription.Name = "cmuDescription";
      this.cmuDescription.Size = new System.Drawing.Size(759, 30);
      this.cmuDescription.Text = "Choose the following number of items from the shuffled section during presentatio" +
    "n:";
      // 
      // cmuCountCombo
      // 
      this.cmuCountCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cmuCountCombo.Name = "cmuCountCombo";
      this.cmuCountCombo.Size = new System.Drawing.Size(121, 33);
      this.cmuCountCombo.SelectedIndexChanged += new System.EventHandler(this.cmuCountCombo_SelectedIndexChanged);
      // 
      // toolStripSeparator2
      // 
      this.toolStripSeparator2.Name = "toolStripSeparator2";
      this.toolStripSeparator2.Size = new System.Drawing.Size(756, 6);
      // 
      // cmuJoin
      // 
      this.cmuJoin.CheckOnClick = true;
      this.cmuJoin.Image = global::Ogama.Properties.Resources.ChainHS;
      this.cmuJoin.Name = "cmuJoin";
      this.cmuJoin.Size = new System.Drawing.Size(759, 30);
      this.cmuJoin.Text = "join selected items";
      this.cmuJoin.Visible = false;
      // 
      // cmuCombineToTrial
      // 
      this.cmuCombineToTrial.CheckOnClick = true;
      this.cmuCombineToTrial.Image = global::Ogama.Properties.Resources.OrgChartHS;
      this.cmuCombineToTrial.Name = "cmuCombineToTrial";
      this.cmuCombineToTrial.Size = new System.Drawing.Size(759, 30);
      this.cmuCombineToTrial.Text = "Combine selected slides to one trial";
      this.cmuCombineToTrial.Click += new System.EventHandler(this.cmuCombineToTrial_Click);
      // 
      // cmuDelete
      // 
      this.cmuDelete.Image = global::Ogama.Properties.Resources.DeleteHS;
      this.cmuDelete.Name = "cmuDelete";
      this.cmuDelete.Size = new System.Drawing.Size(759, 30);
      this.cmuDelete.Text = "Delete selected Slides";
      this.cmuDelete.Click += new System.EventHandler(this.cmuDelete_Click);
      // 
      // imlSlideTypes
      // 
      this.imlSlideTypes.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imlSlideTypes.ImageStream")));
      this.imlSlideTypes.TransparentColor = System.Drawing.Color.Transparent;
      this.imlSlideTypes.Images.SetKeyName(0, "Instructions");
      this.imlSlideTypes.Images.SetKeyName(1, "Images");
      this.imlSlideTypes.Images.SetKeyName(2, "Shapes");
      this.imlSlideTypes.Images.SetKeyName(3, "MixedMedia");
      this.imlSlideTypes.Images.SetKeyName(4, "Flash");
      this.imlSlideTypes.Images.SetKeyName(5, "Blank");
      this.imlSlideTypes.Images.SetKeyName(6, "Slide");
      this.imlSlideTypes.Images.SetKeyName(7, "Folder");
      this.imlSlideTypes.Images.SetKeyName(8, "CategoryClosed");
      this.imlSlideTypes.Images.SetKeyName(9, "CategoryOpen");
      this.imlSlideTypes.Images.SetKeyName(10, "Rtf");
      this.imlSlideTypes.Images.SetKeyName(11, "Trial");
      this.imlSlideTypes.Images.SetKeyName(12, "Browser");
      this.imlSlideTypes.Images.SetKeyName(13, "Desktop");
      // 
      // spcSlidesProperties
      // 
      this.spcSlidesProperties.Dock = System.Windows.Forms.DockStyle.Fill;
      this.spcSlidesProperties.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
      this.spcSlidesProperties.Location = new System.Drawing.Point(0, 0);
      this.spcSlidesProperties.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.spcSlidesProperties.Name = "spcSlidesProperties";
      this.spcSlidesProperties.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // spcSlidesProperties.Panel1
      // 
      this.spcSlidesProperties.Panel1.Controls.Add(this.lsvDetails);
      this.spcSlidesProperties.Panel1MinSize = 165;
      // 
      // spcSlidesProperties.Panel2
      // 
      this.spcSlidesProperties.Panel2.Controls.Add(this.spcPropertiesPreview);
      this.spcSlidesProperties.Size = new System.Drawing.Size(858, 854);
      this.spcSlidesProperties.SplitterDistance = 175;
      this.spcSlidesProperties.SplitterWidth = 6;
      this.spcSlidesProperties.TabIndex = 1;
      // 
      // lsvDetails
      // 
      this.lsvDetails.Alignment = System.Windows.Forms.ListViewAlignment.Left;
      this.lsvDetails.AllColumns.Add(this.colPosition);
      this.lsvDetails.AlternateRowBackColor = System.Drawing.Color.Empty;
      this.lsvDetails.CellEditActivation = OgamaControls.ObjectListView.CellEditActivateMode.SingleClick;
      this.lsvDetails.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colPosition});
      this.lsvDetails.Dock = System.Windows.Forms.DockStyle.Fill;
      this.lsvDetails.EmptyListMsg = "No Slides defined";
      this.lsvDetails.FullRowSelect = true;
      this.lsvDetails.GridLines = true;
      this.lsvDetails.Location = new System.Drawing.Point(0, 0);
      this.lsvDetails.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.lsvDetails.Name = "lsvDetails";
      this.lsvDetails.OwnerDraw = true;
      this.lsvDetails.ShowGroups = false;
      this.lsvDetails.ShowItemToolTips = true;
      this.lsvDetails.Size = new System.Drawing.Size(858, 175);
      this.lsvDetails.SmallImageList = this.imlSlideTypes;
      this.lsvDetails.TabIndex = 0;
      this.lsvDetails.UseCompatibleStateImageBehavior = false;
      this.lsvDetails.View = System.Windows.Forms.View.Tile;
      this.lsvDetails.SelectedIndexChanged += new System.EventHandler(this.lsvDetails_SelectedIndexChanged);
      this.lsvDetails.DoubleClick += new System.EventHandler(this.lsvDetails_DoubleClick);
      this.lsvDetails.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lsvDetails_KeyDown);
      // 
      // colPosition
      // 
      this.colPosition.AspectName = "Position";
      this.colPosition.IsEditable = false;
      this.colPosition.Text = "Pos";
      this.colPosition.Width = 50;
      // 
      // spcPropertiesPreview
      // 
      this.spcPropertiesPreview.Dock = System.Windows.Forms.DockStyle.Fill;
      this.spcPropertiesPreview.Location = new System.Drawing.Point(0, 0);
      this.spcPropertiesPreview.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.spcPropertiesPreview.Name = "spcPropertiesPreview";
      // 
      // spcPropertiesPreview.Panel1
      // 
      this.spcPropertiesPreview.Panel1.Controls.Add(this.grbSlideProperties);
      this.spcPropertiesPreview.Panel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.spcPropertiesPreview.Panel1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
      // 
      // spcPropertiesPreview.Panel2
      // 
      this.spcPropertiesPreview.Panel2.Controls.Add(this.grbPreview);
      this.spcPropertiesPreview.Panel2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.spcPropertiesPreview.Panel2.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.spcPropertiesPreview.Size = new System.Drawing.Size(858, 673);
      this.spcPropertiesPreview.SplitterDistance = 285;
      this.spcPropertiesPreview.SplitterWidth = 6;
      this.spcPropertiesPreview.TabIndex = 0;
      // 
      // grbSlideProperties
      // 
      this.grbSlideProperties.Controls.Add(this.prgSlides);
      this.grbSlideProperties.Dock = System.Windows.Forms.DockStyle.Fill;
      this.grbSlideProperties.Location = new System.Drawing.Point(4, 5);
      this.grbSlideProperties.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.grbSlideProperties.Name = "grbSlideProperties";
      this.grbSlideProperties.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.grbSlideProperties.Size = new System.Drawing.Size(277, 663);
      this.grbSlideProperties.TabIndex = 1;
      this.grbSlideProperties.TabStop = false;
      this.grbSlideProperties.Text = "Slide Properties";
      // 
      // prgSlides
      // 
      this.prgSlides.Dock = System.Windows.Forms.DockStyle.Fill;
      this.prgSlides.Location = new System.Drawing.Point(4, 24);
      this.prgSlides.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.prgSlides.Name = "prgSlides";
      this.prgSlides.PropertySort = System.Windows.Forms.PropertySort.Categorized;
      this.prgSlides.Size = new System.Drawing.Size(269, 634);
      this.prgSlides.TabIndex = 0;
      this.prgSlides.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.prgSlides_PropertyValueChanged);
      // 
      // grbPreview
      // 
      this.grbPreview.Controls.Add(this.pnlCanvas);
      this.grbPreview.Dock = System.Windows.Forms.DockStyle.Fill;
      this.grbPreview.Location = new System.Drawing.Point(4, 5);
      this.grbPreview.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.grbPreview.Name = "grbPreview";
      this.grbPreview.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.grbPreview.Size = new System.Drawing.Size(559, 663);
      this.grbPreview.TabIndex = 1;
      this.grbPreview.TabStop = false;
      this.grbPreview.Text = "Slide Preview";
      // 
      // pnlCanvas
      // 
      this.pnlCanvas.AutoScroll = true;
      this.pnlCanvas.BackColor = global::Ogama.Properties.Settings.Default.BackgroundColorForms;
      this.pnlCanvas.Controls.Add(this.pnlPicture);
      this.pnlCanvas.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::Ogama.Properties.Settings.Default, "BackgroundColorForms", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.pnlCanvas.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pnlCanvas.Location = new System.Drawing.Point(4, 24);
      this.pnlCanvas.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.pnlCanvas.Name = "pnlCanvas";
      this.pnlCanvas.Size = new System.Drawing.Size(551, 634);
      this.pnlCanvas.TabIndex = 1;
      // 
      // pnlPicture
      // 
      this.pnlPicture.BackColor = System.Drawing.Color.White;
      this.pnlPicture.Controls.Add(this.slidePreviewPicture);
      this.pnlPicture.Location = new System.Drawing.Point(92, 28);
      this.pnlPicture.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.pnlPicture.Name = "pnlPicture";
      this.pnlPicture.Size = new System.Drawing.Size(626, 385);
      this.pnlPicture.TabIndex = 0;
      // 
      // slidePreviewPicture
      // 
      this.slidePreviewPicture.AnimationInterval = 500;
      this.slidePreviewPicture.BackColor = System.Drawing.Color.Black;
      this.slidePreviewPicture.InvalidateInterval = 500;
      this.slidePreviewPicture.Location = new System.Drawing.Point(0, 0);
      this.slidePreviewPicture.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.slidePreviewPicture.Name = "slidePreviewPicture";
      this.slidePreviewPicture.Size = new System.Drawing.Size(626, 385);
      this.slidePreviewPicture.TabIndex = 0;
      this.slidePreviewPicture.ZoomFactor = 0F;
      this.slidePreviewPicture.DoubleClick += new System.EventHandler(this.slidePreviewPicture_DoubleClick);
      // 
      // tosStimuli
      // 
      this.tosStimuli.DataBindings.Add(new System.Windows.Forms.Binding("Location", global::Ogama.Properties.Settings.Default, "SCRToolbarLocation", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.tosStimuli.Dock = System.Windows.Forms.DockStyle.None;
      this.tosStimuli.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnInstruction,
            this.btnRtfInstruction,
            this.btnImage,
            this.btnShapes,
            this.btnMixed,
            this.btnInternet,
            this.btnFlash,
            this.btnDesktop,
            this.btnBlank,
            this.toolStripSeparator1,
            this.btnSaveSlideshow,
            this.btnImport,
            this.btnExport,
            this.btnImportFolderContent,
            this.toolStripSeparator3,
            this.btnPrimary,
            this.btnSecondary,
            this.toolStripSeparator6,
            this.btnPreviewSlideshow,
            this.toolStripSeparator5,
            this.btnHelp});
      this.tosStimuli.Location = global::Ogama.Properties.Settings.Default.SCRToolbarLocation;
      this.tosStimuli.Name = "tosStimuli";
      this.tosStimuli.Size = new System.Drawing.Size(473, 28);
      this.tosStimuli.TabIndex = 0;
      // 
      // btnInstruction
      // 
      this.btnInstruction.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnInstruction.Image = global::Ogama.Properties.Resources.textdoc1;
      this.btnInstruction.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnInstruction.Name = "btnInstruction";
      this.btnInstruction.Size = new System.Drawing.Size(23, 25);
      this.btnInstruction.Text = "Add instruction slide ...";
      this.btnInstruction.Click += new System.EventHandler(this.btnInstruction_Click);
      // 
      // btnRtfInstruction
      // 
      this.btnRtfInstruction.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnRtfInstruction.Image = global::Ogama.Properties.Resources.Rtf;
      this.btnRtfInstruction.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnRtfInstruction.Name = "btnRtfInstruction";
      this.btnRtfInstruction.Size = new System.Drawing.Size(23, 25);
      this.btnRtfInstruction.Text = "Add Rich Text instruction";
      this.btnRtfInstruction.Click += new System.EventHandler(this.btnRtfInstruction_Click);
      // 
      // btnImage
      // 
      this.btnImage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnImage.Image = global::Ogama.Properties.Resources.GenericPicDoc;
      this.btnImage.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnImage.Name = "btnImage";
      this.btnImage.Size = new System.Drawing.Size(23, 25);
      this.btnImage.Text = "Add image slide ...";
      this.btnImage.Click += new System.EventHandler(this.btnImage_Click);
      // 
      // btnShapes
      // 
      this.btnShapes.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnShapes.Image = global::Ogama.Properties.Resources.GenericShape;
      this.btnShapes.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnShapes.Name = "btnShapes";
      this.btnShapes.Size = new System.Drawing.Size(23, 25);
      this.btnShapes.Text = "Add shape elements slide ...";
      this.btnShapes.Click += new System.EventHandler(this.btnShapes_Click);
      // 
      // btnMixed
      // 
      this.btnMixed.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnMixed.Image = global::Ogama.Properties.Resources.GenMixedMediaDoc;
      this.btnMixed.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnMixed.Name = "btnMixed";
      this.btnMixed.Size = new System.Drawing.Size(23, 25);
      this.btnMixed.Text = "Add mixed slide ...";
      this.btnMixed.Click += new System.EventHandler(this.btnMixed_Click);
      // 
      // btnInternet
      // 
      this.btnInternet.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnInternet.Image = global::Ogama.Properties.Resources.AlignTableCellMiddleLeftJustHS;
      this.btnInternet.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnInternet.Name = "btnInternet";
      this.btnInternet.Size = new System.Drawing.Size(23, 25);
      this.btnInternet.Text = "Add browser slide ...";
      this.btnInternet.Click += new System.EventHandler(this.btnInternet_Click);
      // 
      // btnFlash
      // 
      this.btnFlash.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnFlash.Image = global::Ogama.Properties.Resources.FlashPlayer;
      this.btnFlash.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnFlash.Name = "btnFlash";
      this.btnFlash.Size = new System.Drawing.Size(23, 25);
      this.btnFlash.Text = "Add flash movie slide ...";
      this.btnFlash.Click += new System.EventHandler(this.btnFlash_Click);
      // 
      // btnDesktop
      // 
      this.btnDesktop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnDesktop.Image = global::Ogama.Properties.Resources.Desktop1;
      this.btnDesktop.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnDesktop.Name = "btnDesktop";
      this.btnDesktop.Size = new System.Drawing.Size(23, 25);
      this.btnDesktop.Text = "Add desktop recording ...";
      this.btnDesktop.Click += new System.EventHandler(this.btnDesktop_Click);
      // 
      // btnBlank
      // 
      this.btnBlank.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnBlank.Image = global::Ogama.Properties.Resources.document;
      this.btnBlank.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnBlank.Name = "btnBlank";
      this.btnBlank.Size = new System.Drawing.Size(23, 25);
      this.btnBlank.Text = "Add blank slide ...";
      this.btnBlank.Click += new System.EventHandler(this.btnBlank_Click);
      // 
      // toolStripSeparator1
      // 
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new System.Drawing.Size(6, 28);
      // 
      // btnSaveSlideshow
      // 
      this.btnSaveSlideshow.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnSaveSlideshow.Image = global::Ogama.Properties.Resources.saveHS;
      this.btnSaveSlideshow.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnSaveSlideshow.Name = "btnSaveSlideshow";
      this.btnSaveSlideshow.Size = new System.Drawing.Size(23, 25);
      this.btnSaveSlideshow.Text = "Save slideshow to experiment settings, update context panel and create stimulus f" +
    "iles for each slide ...";
      this.btnSaveSlideshow.Click += new System.EventHandler(this.btnSaveSlideshow_Click);
      // 
      // btnImport
      // 
      this.btnImport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnImport.Image = global::Ogama.Properties.Resources.ImportDocument;
      this.btnImport.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnImport.Name = "btnImport";
      this.btnImport.Size = new System.Drawing.Size(23, 25);
      this.btnImport.Text = "Imports slideshow from file...";
      this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
      // 
      // btnExport
      // 
      this.btnExport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnExport.Image = global::Ogama.Properties.Resources.ExportToDocument;
      this.btnExport.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnExport.Name = "btnExport";
      this.btnExport.Size = new System.Drawing.Size(23, 25);
      this.btnExport.Text = "Exports current slideshow to file...";
      this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
      // 
      // btnImportFolderContent
      // 
      this.btnImportFolderContent.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnImportFolderContent.Image = global::Ogama.Properties.Resources.ImportData;
      this.btnImportFolderContent.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnImportFolderContent.Name = "btnImportFolderContent";
      this.btnImportFolderContent.Size = new System.Drawing.Size(23, 25);
      this.btnImportFolderContent.Text = "Import folder content as slides.";
      this.btnImportFolderContent.Click += new System.EventHandler(this.btnImportFolderContent_Click);
      // 
      // toolStripSeparator3
      // 
      this.toolStripSeparator3.Name = "toolStripSeparator3";
      this.toolStripSeparator3.Size = new System.Drawing.Size(6, 28);
      // 
      // btnPreviewSlideshow
      // 
      this.btnPreviewSlideshow.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnPreviewSlideshow.Image = global::Ogama.Properties.Resources.FormRunHS;
      this.btnPreviewSlideshow.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnPreviewSlideshow.Name = "btnPreviewSlideshow";
      this.btnPreviewSlideshow.Size = new System.Drawing.Size(23, 25);
      this.btnPreviewSlideshow.Text = "Preview slideshow";
      this.btnPreviewSlideshow.ToolTipText = "Preview slideshow...";
      this.btnPreviewSlideshow.Click += new System.EventHandler(this.BtnPreviewSlideshowClick);
      // 
      // toolStripSeparator5
      // 
      this.toolStripSeparator5.Name = "toolStripSeparator5";
      this.toolStripSeparator5.Size = new System.Drawing.Size(6, 28);
      // 
      // btnHelp
      // 
      this.btnHelp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnHelp.Image = global::Ogama.Properties.Resources.HelpBmp;
      this.btnHelp.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnHelp.Name = "btnHelp";
      this.btnHelp.Size = new System.Drawing.Size(23, 25);
      this.btnHelp.Text = "Show context help";
      // 
      // btnPrimary
      // 
      this.btnPrimary.CheckOnClick = true;
      this.btnPrimary.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnPrimary.Image = global::Ogama.Properties.Resources.Monitor1;
      this.btnPrimary.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnPrimary.Name = "btnPrimary";
      this.btnPrimary.Size = new System.Drawing.Size(23, 25);
      this.btnPrimary.Text = "Primary Screen";
      this.btnPrimary.ToolTipText = "Show slide show on primary screen";
      this.btnPrimary.Click += new System.EventHandler(this.BtnPrimaryClick);
      // 
      // btnSecondary
      // 
      this.btnSecondary.CheckOnClick = true;
      this.btnSecondary.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnSecondary.Image = global::Ogama.Properties.Resources.Monitor2;
      this.btnSecondary.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnSecondary.Name = "btnSecondary";
      this.btnSecondary.Size = new System.Drawing.Size(23, 25);
      this.btnSecondary.Text = "Secondary Screen";
      this.btnSecondary.ToolTipText = "Show slide show on secondary screen";
      this.btnSecondary.Click += new System.EventHandler(this.BtnSecondaryClick);
      // 
      // sfdSlideshow
      // 
      this.sfdSlideshow.DefaultExt = "ogs";
      this.sfdSlideshow.FileName = "Slideshow1";
      this.sfdSlideshow.Filter = "OGAMA - Slideshows|*.ogs";
      this.sfdSlideshow.Title = "Specify slideshow save file ...";
      // 
      // ofdSlideshow
      // 
      this.ofdSlideshow.DefaultExt = "ogs";
      this.ofdSlideshow.Filter = "OGAMA - Slideshows|*.ogs";
      this.ofdSlideshow.Title = "Select OGAMA slideshow ...";
      // 
      // dataGridViewTextBoxColumn1
      // 
      this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
      this.dataGridViewTextBoxColumn1.HeaderText = "Pos";
      this.dataGridViewTextBoxColumn1.MinimumWidth = 30;
      this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
      this.dataGridViewTextBoxColumn1.ReadOnly = true;
      this.dataGridViewTextBoxColumn1.ToolTipText = "Position of the slide during presentation.";
      this.dataGridViewTextBoxColumn1.Width = 30;
      // 
      // dataGridViewTextBoxColumn2
      // 
      this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
      this.dataGridViewTextBoxColumn2.FillWeight = 123.0539F;
      this.dataGridViewTextBoxColumn2.HeaderText = "Name";
      this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
      this.dataGridViewTextBoxColumn2.ReadOnly = true;
      this.dataGridViewTextBoxColumn2.ToolTipText = "Unique name of the slide.";
      // 
      // dataGridViewTextBoxColumn3
      // 
      this.dataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
      this.dataGridViewTextBoxColumn3.HeaderText = "Category";
      this.dataGridViewTextBoxColumn3.MinimumWidth = 120;
      this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
      this.dataGridViewTextBoxColumn3.ReadOnly = true;
      this.dataGridViewTextBoxColumn3.ToolTipText = "A category to cluster slides.";
      this.dataGridViewTextBoxColumn3.Width = 120;
      // 
      // toolStripSeparator6
      // 
      this.toolStripSeparator6.Name = "toolStripSeparator6";
      this.toolStripSeparator6.Size = new System.Drawing.Size(6, 28);
      // 
      // SlideshowModule
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1294, 882);
      this.Controls.Add(this.toolStripContainer1);
      this.DataBindings.Add(new System.Windows.Forms.Binding("Location", global::Ogama.Properties.Settings.Default, "SlideshowModuleLocation", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Location = global::Ogama.Properties.Settings.Default.SlideshowModuleLocation;
      this.Logo = global::Ogama.Properties.Resources.Design;
      this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.Name = "SlideshowModule";
      this.Text = "Slideshow design module";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmStimuli_FormClosing);
      this.Load += new System.EventHandler(this.frmStimuli_Load);
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
      this.toolStripContainer1.ContentPanel.ResumeLayout(false);
      this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
      this.toolStripContainer1.TopToolStripPanel.PerformLayout();
      this.toolStripContainer1.ResumeLayout(false);
      this.toolStripContainer1.PerformLayout();
      this.spcTreeDetail.Panel1.ResumeLayout(false);
      this.spcTreeDetail.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.spcTreeDetail)).EndInit();
      this.spcTreeDetail.ResumeLayout(false);
      this.spcToolbarTreeView.Panel1.ResumeLayout(false);
      this.spcToolbarTreeView.Panel1.PerformLayout();
      this.spcToolbarTreeView.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.spcToolbarTreeView)).EndInit();
      this.spcToolbarTreeView.ResumeLayout(false);
      this.toolStrip1.ResumeLayout(false);
      this.toolStrip1.PerformLayout();
      this.cmuItemView.ResumeLayout(false);
      this.spcSlidesProperties.Panel1.ResumeLayout(false);
      this.spcSlidesProperties.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.spcSlidesProperties)).EndInit();
      this.spcSlidesProperties.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.lsvDetails)).EndInit();
      this.spcPropertiesPreview.Panel1.ResumeLayout(false);
      this.spcPropertiesPreview.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.spcPropertiesPreview)).EndInit();
      this.spcPropertiesPreview.ResumeLayout(false);
      this.grbSlideProperties.ResumeLayout(false);
      this.grbPreview.ResumeLayout(false);
      this.pnlCanvas.ResumeLayout(false);
      this.pnlPicture.ResumeLayout(false);
      this.tosStimuli.ResumeLayout(false);
      this.tosStimuli.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.ToolStripContainer toolStripContainer1;
    private System.Windows.Forms.ToolStrip tosStimuli;
    private System.Windows.Forms.ToolStripButton btnInstruction;
    private System.Windows.Forms.ToolStripButton btnFlash;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    private System.Windows.Forms.ToolStripButton btnImage;
    private System.Windows.Forms.ToolStripButton btnSaveSlideshow;
    private System.Windows.Forms.SaveFileDialog sfdSlideshow;
    private System.Windows.Forms.OpenFileDialog ofdSlideshow;
    private System.Windows.Forms.ToolStripButton btnImport;
    private System.Windows.Forms.ToolStripButton btnExport;
    private System.Windows.Forms.ToolStripButton btnMixed;
    private System.Windows.Forms.ToolStripButton btnBlank;
    private System.Windows.Forms.SplitContainer spcSlidesProperties;
    private System.Windows.Forms.ToolStripButton btnShapes;
    private System.Windows.Forms.ToolTip toolTip1;
    private System.Windows.Forms.ImageList imlSlideTypes;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
    private System.Windows.Forms.ToolStripButton btnPreviewSlideshow;
    private System.Windows.Forms.ToolStripButton btnHelp;
    private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
    private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
    private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
    private System.Windows.Forms.ContextMenuStrip cmuItemView;
    private System.Windows.Forms.ToolStripMenuItem cmuJoin;
    private System.Windows.Forms.ToolStripMenuItem cmuShuffle;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
    private OgamaControls.ObjectListView lsvDetails;
    private OgamaControls.OLVColumn colPosition;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
    private System.Windows.Forms.SplitContainer spcPropertiesPreview;
    private System.Windows.Forms.PropertyGrid prgSlides;
    private System.Windows.Forms.GroupBox grbPreview;
    private OgamaControls.DoubleBufferPanel pnlPicture;
    private System.Windows.Forms.Panel pnlCanvas;
    private System.Windows.Forms.GroupBox grbSlideProperties;
    private System.Windows.Forms.SplitContainer spcTreeDetail;
    private System.Windows.Forms.ToolStrip toolStrip1;
    private System.Windows.Forms.ToolStripButton btnLevelUp;
    private System.Windows.Forms.ToolStripButton btnLevelDown;
    private System.Windows.Forms.ToolStripButton btnIndexUp;
    private System.Windows.Forms.ToolStripButton btnIndexDown;
    private System.Windows.Forms.SplitContainer spcToolbarTreeView;
    private OgamaControls.MultiselectTreeView trvSlideshow;
    private System.Windows.Forms.ToolStripMenuItem cmuCombineToTrial;
    private System.Windows.Forms.ToolStripMenuItem cmuDelete;
    private System.Windows.Forms.ToolStripButton btnRtfInstruction;
		private System.Windows.Forms.ToolStripButton btnImportFolderContent;
    private System.Windows.Forms.ToolStripMenuItem cmuDescription;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    private System.Windows.Forms.ToolStripComboBox cmuCountCombo;
    private System.Windows.Forms.ToolStripButton btnCustomShuffling;
    private VectorGraphics.Canvas.Picture slidePreviewPicture;
    private System.Windows.Forms.ToolStripButton btnAddFolder;
    private System.Windows.Forms.ToolStripButton btnInternet;
    private System.Windows.Forms.ToolStripButton btnDesktop;
    private System.Windows.Forms.ToolStripMenuItem cmuDisable;
    private System.Windows.Forms.ToolStripButton btnPrimary;
    private System.Windows.Forms.ToolStripButton btnSecondary;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
  }
}
