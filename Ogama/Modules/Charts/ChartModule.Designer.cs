namespace Ogama.Modules.Charts
{
  using Ogama.Modules.Common.Controls;
  using Ogama.Modules.Common.PictureTemplates;

  partial class ChartModule
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChartModule));
      this.spcChartOptions = new System.Windows.Forms.SplitContainer();
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      this.tbcParameters = new System.Windows.Forms.TabControl();
      this.tbpSubject = new System.Windows.Forms.TabPage();
      this.tabPage2 = new System.Windows.Forms.TabPage();
      this.tbcChartOptions = new System.Windows.Forms.TabControl();
      this.tbpLegend = new System.Windows.Forms.TabPage();
      this.txbSeriesTitle = new System.Windows.Forms.TextBox();
      this.txbLegend = new System.Windows.Forms.TextBox();
      this.txbDiagramTitle = new System.Windows.Forms.TextBox();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.rdbLegendPositionOutside = new System.Windows.Forms.RadioButton();
      this.rdbLegendPositionInside = new System.Windows.Forms.RadioButton();
      this.label2 = new System.Windows.Forms.Label();
      this.chbShowLegend = new System.Windows.Forms.CheckBox();
      this.label1 = new System.Windows.Forms.Label();
      this.tabPage4 = new System.Windows.Forms.TabPage();
      this.tbpCharttype = new System.Windows.Forms.TabPage();
      this.rdbChartTypeBubble = new System.Windows.Forms.RadioButton();
      this.imlChartTypes = new System.Windows.Forms.ImageList(this.components);
      this.rdbChartTypePie = new System.Windows.Forms.RadioButton();
      this.rdbChartTypeColumn = new System.Windows.Forms.RadioButton();
      this.rdbChartTypeArea = new System.Windows.Forms.RadioButton();
      this.rdbChartTypeScatter = new System.Windows.Forms.RadioButton();
      this.rdbChartTypeLine = new System.Windows.Forms.RadioButton();
      this.tbpAxes = new System.Windows.Forms.TabPage();
      this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
      this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
      this.tosTrialSelection = new System.Windows.Forms.ToolStrip();
      this.cbbSubject = new System.Windows.Forms.ToolStripComboBox();
      this.cbbTrial = new System.Windows.Forms.ToolStripComboBox();
      this.btnEye = new System.Windows.Forms.ToolStripButton();
      this.btnMouse = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
      this.btnHelp = new System.Windows.Forms.ToolStripButton();
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
      ((System.ComponentModel.ISupportInitialize)(this.spcChartOptions)).BeginInit();
      this.spcChartOptions.Panel2.SuspendLayout();
      this.spcChartOptions.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      this.tbcParameters.SuspendLayout();
      this.tbcChartOptions.SuspendLayout();
      this.tbpLegend.SuspendLayout();
      this.groupBox1.SuspendLayout();
      this.tbpCharttype.SuspendLayout();
      this.toolStripContainer1.ContentPanel.SuspendLayout();
      this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
      this.toolStripContainer1.SuspendLayout();
      this.tosTrialSelection.SuspendLayout();
      this.SuspendLayout();
      // 
      // spcChartOptions
      // 
      this.spcChartOptions.Dock = System.Windows.Forms.DockStyle.Fill;
      this.spcChartOptions.Location = new System.Drawing.Point(0, 0);
      this.spcChartOptions.Margin = new System.Windows.Forms.Padding(0);
      this.spcChartOptions.Name = "spcChartOptions";
      // 
      // spcChartOptions.Panel2
      // 
      this.spcChartOptions.Panel2.Controls.Add(this.splitContainer1);
      this.spcChartOptions.Size = new System.Drawing.Size(1096, 399);
      this.spcChartOptions.SplitterDistance = 692;
      this.spcChartOptions.TabIndex = 2;
      // 
      // splitContainer1
      // 
      this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer1.Location = new System.Drawing.Point(0, 0);
      this.splitContainer1.Name = "splitContainer1";
      this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // splitContainer1.Panel1
      // 
      this.splitContainer1.Panel1.Controls.Add(this.tbcParameters);
      // 
      // splitContainer1.Panel2
      // 
      this.splitContainer1.Panel2.Controls.Add(this.tbcChartOptions);
      this.splitContainer1.Size = new System.Drawing.Size(400, 399);
      this.splitContainer1.SplitterDistance = 133;
      this.splitContainer1.TabIndex = 0;
      // 
      // tbcParameters
      // 
      this.tbcParameters.Controls.Add(this.tbpSubject);
      this.tbcParameters.Controls.Add(this.tabPage2);
      this.tbcParameters.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tbcParameters.Location = new System.Drawing.Point(0, 0);
      this.tbcParameters.Name = "tbcParameters";
      this.tbcParameters.SelectedIndex = 0;
      this.tbcParameters.Size = new System.Drawing.Size(400, 133);
      this.tbcParameters.TabIndex = 0;
      // 
      // tbpSubject
      // 
      this.tbpSubject.Location = new System.Drawing.Point(4, 22);
      this.tbpSubject.Name = "tbpSubject";
      this.tbpSubject.Padding = new System.Windows.Forms.Padding(3);
      this.tbpSubject.Size = new System.Drawing.Size(392, 107);
      this.tbpSubject.TabIndex = 0;
      this.tbpSubject.Text = "Subject Charts";
      this.tbpSubject.UseVisualStyleBackColor = true;
      // 
      // tabPage2
      // 
      this.tabPage2.Location = new System.Drawing.Point(4, 22);
      this.tabPage2.Name = "tabPage2";
      this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage2.Size = new System.Drawing.Size(392, 106);
      this.tabPage2.TabIndex = 1;
      this.tabPage2.Text = "tabPage2";
      this.tabPage2.UseVisualStyleBackColor = true;
      // 
      // tbcChartOptions
      // 
      this.tbcChartOptions.Controls.Add(this.tbpLegend);
      this.tbcChartOptions.Controls.Add(this.tabPage4);
      this.tbcChartOptions.Controls.Add(this.tbpCharttype);
      this.tbcChartOptions.Controls.Add(this.tbpAxes);
      this.tbcChartOptions.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tbcChartOptions.Location = new System.Drawing.Point(0, 0);
      this.tbcChartOptions.Name = "tbcChartOptions";
      this.tbcChartOptions.SelectedIndex = 0;
      this.tbcChartOptions.Size = new System.Drawing.Size(400, 262);
      this.tbcChartOptions.TabIndex = 0;
      // 
      // tbpLegend
      // 
      this.tbpLegend.Controls.Add(this.txbSeriesTitle);
      this.tbpLegend.Controls.Add(this.txbLegend);
      this.tbpLegend.Controls.Add(this.txbDiagramTitle);
      this.tbpLegend.Controls.Add(this.groupBox1);
      this.tbpLegend.Controls.Add(this.label2);
      this.tbpLegend.Controls.Add(this.chbShowLegend);
      this.tbpLegend.Controls.Add(this.label1);
      this.tbpLegend.Location = new System.Drawing.Point(4, 22);
      this.tbpLegend.Name = "tbpLegend";
      this.tbpLegend.Padding = new System.Windows.Forms.Padding(3);
      this.tbpLegend.Size = new System.Drawing.Size(392, 236);
      this.tbpLegend.TabIndex = 0;
      this.tbpLegend.Text = "Legend";
      this.tbpLegend.UseVisualStyleBackColor = true;
      // 
      // txbSeriesTitle
      // 
      this.txbSeriesTitle.Location = new System.Drawing.Point(98, 58);
      this.txbSeriesTitle.Name = "txbSeriesTitle";
      this.txbSeriesTitle.Size = new System.Drawing.Size(124, 20);
      this.txbSeriesTitle.TabIndex = 4;
      this.txbSeriesTitle.TextChanged += new System.EventHandler(this.txbSeriesTitle_TextChanged);
      // 
      // txbLegend
      // 
      this.txbLegend.Location = new System.Drawing.Point(98, 32);
      this.txbLegend.Name = "txbLegend";
      this.txbLegend.Size = new System.Drawing.Size(124, 20);
      this.txbLegend.TabIndex = 4;
      this.txbLegend.TextChanged += new System.EventHandler(this.txbLegend_TextChanged);
      // 
      // txbDiagramTitle
      // 
      this.txbDiagramTitle.Location = new System.Drawing.Point(98, 6);
      this.txbDiagramTitle.Name = "txbDiagramTitle";
      this.txbDiagramTitle.Size = new System.Drawing.Size(124, 20);
      this.txbDiagramTitle.TabIndex = 4;
      this.txbDiagramTitle.TextChanged += new System.EventHandler(this.txbDiagramTitle_TextChanged);
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.rdbLegendPositionOutside);
      this.groupBox1.Controls.Add(this.rdbLegendPositionInside);
      this.groupBox1.Location = new System.Drawing.Point(257, 6);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(118, 82);
      this.groupBox1.TabIndex = 3;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Legend position";
      // 
      // rdbLegendPositionOutside
      // 
      this.rdbLegendPositionOutside.AutoSize = true;
      this.rdbLegendPositionOutside.Location = new System.Drawing.Point(25, 50);
      this.rdbLegendPositionOutside.Name = "rdbLegendPositionOutside";
      this.rdbLegendPositionOutside.Size = new System.Drawing.Size(59, 17);
      this.rdbLegendPositionOutside.TabIndex = 0;
      this.rdbLegendPositionOutside.TabStop = true;
      this.rdbLegendPositionOutside.Text = "outside";
      this.rdbLegendPositionOutside.UseVisualStyleBackColor = true;
      this.rdbLegendPositionOutside.CheckedChanged += new System.EventHandler(this.rdbLegendPosition_CheckedChanged);
      // 
      // rdbLegendPositionInside
      // 
      this.rdbLegendPositionInside.AutoSize = true;
      this.rdbLegendPositionInside.Checked = true;
      this.rdbLegendPositionInside.Location = new System.Drawing.Point(25, 27);
      this.rdbLegendPositionInside.Name = "rdbLegendPositionInside";
      this.rdbLegendPositionInside.Size = new System.Drawing.Size(52, 17);
      this.rdbLegendPositionInside.TabIndex = 0;
      this.rdbLegendPositionInside.TabStop = true;
      this.rdbLegendPositionInside.Text = "inside";
      this.rdbLegendPositionInside.UseVisualStyleBackColor = true;
      this.rdbLegendPositionInside.CheckedChanged += new System.EventHandler(this.rdbLegendPosition_CheckedChanged);
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(19, 58);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(55, 13);
      this.label2.TabIndex = 2;
      this.label2.Text = "Series title";
      // 
      // chbShowLegend
      // 
      this.chbShowLegend.AutoSize = true;
      this.chbShowLegend.Checked = true;
      this.chbShowLegend.CheckState = System.Windows.Forms.CheckState.Checked;
      this.chbShowLegend.Location = new System.Drawing.Point(20, 35);
      this.chbShowLegend.Name = "chbShowLegend";
      this.chbShowLegend.Size = new System.Drawing.Size(62, 17);
      this.chbShowLegend.TabIndex = 1;
      this.chbShowLegend.Text = "Legend";
      this.chbShowLegend.UseVisualStyleBackColor = true;
      this.chbShowLegend.CheckedChanged += new System.EventHandler(this.chbShowLegend_CheckedChanged);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(14, 11);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(65, 13);
      this.label1.TabIndex = 0;
      this.label1.Text = "Diagram title";
      // 
      // tabPage4
      // 
      this.tabPage4.Location = new System.Drawing.Point(4, 22);
      this.tabPage4.Name = "tabPage4";
      this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage4.Size = new System.Drawing.Size(392, 235);
      this.tabPage4.TabIndex = 1;
      this.tabPage4.Text = "Colors";
      this.tabPage4.UseVisualStyleBackColor = true;
      // 
      // tbpCharttype
      // 
      this.tbpCharttype.Controls.Add(this.rdbChartTypeBubble);
      this.tbpCharttype.Controls.Add(this.rdbChartTypePie);
      this.tbpCharttype.Controls.Add(this.rdbChartTypeColumn);
      this.tbpCharttype.Controls.Add(this.rdbChartTypeArea);
      this.tbpCharttype.Controls.Add(this.rdbChartTypeScatter);
      this.tbpCharttype.Controls.Add(this.rdbChartTypeLine);
      this.tbpCharttype.Location = new System.Drawing.Point(4, 22);
      this.tbpCharttype.Name = "tbpCharttype";
      this.tbpCharttype.Padding = new System.Windows.Forms.Padding(3);
      this.tbpCharttype.Size = new System.Drawing.Size(392, 236);
      this.tbpCharttype.TabIndex = 2;
      this.tbpCharttype.Text = "Charttype";
      this.tbpCharttype.UseVisualStyleBackColor = true;
      // 
      // rdbChartTypeBubble
      // 
      this.rdbChartTypeBubble.AutoSize = true;
      this.rdbChartTypeBubble.ImageKey = "Bubble";
      this.rdbChartTypeBubble.ImageList = this.imlChartTypes;
      this.rdbChartTypeBubble.Location = new System.Drawing.Point(197, 96);
      this.rdbChartTypeBubble.Name = "rdbChartTypeBubble";
      this.rdbChartTypeBubble.Size = new System.Drawing.Size(122, 32);
      this.rdbChartTypeBubble.TabIndex = 0;
      this.rdbChartTypeBubble.Text = "Bubble Series";
      this.rdbChartTypeBubble.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.rdbChartTypeBubble.UseVisualStyleBackColor = true;
      // 
      // imlChartTypes
      // 
      this.imlChartTypes.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imlChartTypes.ImageStream")));
      this.imlChartTypes.TransparentColor = System.Drawing.Color.Transparent;
      this.imlChartTypes.Images.SetKeyName(0, "Area");
      this.imlChartTypes.Images.SetKeyName(1, "Bubble");
      this.imlChartTypes.Images.SetKeyName(2, "Column");
      this.imlChartTypes.Images.SetKeyName(3, "Line");
      this.imlChartTypes.Images.SetKeyName(4, "Pie");
      this.imlChartTypes.Images.SetKeyName(5, "Scatter");
      // 
      // rdbChartTypePie
      // 
      this.rdbChartTypePie.AutoSize = true;
      this.rdbChartTypePie.ImageKey = "Pie";
      this.rdbChartTypePie.ImageList = this.imlChartTypes;
      this.rdbChartTypePie.Location = new System.Drawing.Point(197, 58);
      this.rdbChartTypePie.Name = "rdbChartTypePie";
      this.rdbChartTypePie.Size = new System.Drawing.Size(104, 32);
      this.rdbChartTypePie.TabIndex = 0;
      this.rdbChartTypePie.Text = "Pie Series";
      this.rdbChartTypePie.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.rdbChartTypePie.UseVisualStyleBackColor = true;
      // 
      // rdbChartTypeColumn
      // 
      this.rdbChartTypeColumn.AutoSize = true;
      this.rdbChartTypeColumn.ImageKey = "Column";
      this.rdbChartTypeColumn.ImageList = this.imlChartTypes;
      this.rdbChartTypeColumn.Location = new System.Drawing.Point(197, 20);
      this.rdbChartTypeColumn.Name = "rdbChartTypeColumn";
      this.rdbChartTypeColumn.Size = new System.Drawing.Size(124, 32);
      this.rdbChartTypeColumn.TabIndex = 0;
      this.rdbChartTypeColumn.Text = "Column Series";
      this.rdbChartTypeColumn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.rdbChartTypeColumn.UseVisualStyleBackColor = true;
      // 
      // rdbChartTypeArea
      // 
      this.rdbChartTypeArea.AutoSize = true;
      this.rdbChartTypeArea.ImageKey = "Area";
      this.rdbChartTypeArea.ImageList = this.imlChartTypes;
      this.rdbChartTypeArea.Location = new System.Drawing.Point(17, 96);
      this.rdbChartTypeArea.Name = "rdbChartTypeArea";
      this.rdbChartTypeArea.Size = new System.Drawing.Size(111, 32);
      this.rdbChartTypeArea.TabIndex = 0;
      this.rdbChartTypeArea.Text = "Area Series";
      this.rdbChartTypeArea.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.rdbChartTypeArea.UseVisualStyleBackColor = true;
      // 
      // rdbChartTypeScatter
      // 
      this.rdbChartTypeScatter.AutoSize = true;
      this.rdbChartTypeScatter.ImageKey = "Scatter";
      this.rdbChartTypeScatter.ImageList = this.imlChartTypes;
      this.rdbChartTypeScatter.Location = new System.Drawing.Point(17, 58);
      this.rdbChartTypeScatter.Name = "rdbChartTypeScatter";
      this.rdbChartTypeScatter.Size = new System.Drawing.Size(123, 32);
      this.rdbChartTypeScatter.TabIndex = 0;
      this.rdbChartTypeScatter.Text = "Scatter Series";
      this.rdbChartTypeScatter.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.rdbChartTypeScatter.UseVisualStyleBackColor = true;
      // 
      // rdbChartTypeLine
      // 
      this.rdbChartTypeLine.AutoSize = true;
      this.rdbChartTypeLine.Checked = true;
      this.rdbChartTypeLine.ImageKey = "Line";
      this.rdbChartTypeLine.ImageList = this.imlChartTypes;
      this.rdbChartTypeLine.Location = new System.Drawing.Point(17, 20);
      this.rdbChartTypeLine.Name = "rdbChartTypeLine";
      this.rdbChartTypeLine.Size = new System.Drawing.Size(109, 32);
      this.rdbChartTypeLine.TabIndex = 0;
      this.rdbChartTypeLine.TabStop = true;
      this.rdbChartTypeLine.Text = "Line Series";
      this.rdbChartTypeLine.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.rdbChartTypeLine.UseVisualStyleBackColor = true;
      this.rdbChartTypeLine.CheckedChanged += new System.EventHandler(this.rdbChartType_CheckedChanged);
      // 
      // tbpAxes
      // 
      this.tbpAxes.Location = new System.Drawing.Point(4, 22);
      this.tbpAxes.Name = "tbpAxes";
      this.tbpAxes.Padding = new System.Windows.Forms.Padding(3);
      this.tbpAxes.Size = new System.Drawing.Size(392, 235);
      this.tbpAxes.TabIndex = 3;
      this.tbpAxes.Text = "Axes";
      this.tbpAxes.UseVisualStyleBackColor = true;
      // 
      // toolStripContainer1
      // 
      // 
      // toolStripContainer1.ContentPanel
      // 
      this.toolStripContainer1.ContentPanel.Controls.Add(this.spcChartOptions);
      this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(1096, 399);
      this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
      this.toolStripContainer1.Name = "toolStripContainer1";
      this.toolStripContainer1.Size = new System.Drawing.Size(1096, 424);
      this.toolStripContainer1.TabIndex = 3;
      this.toolStripContainer1.Text = "toolStripContainer1";
      // 
      // toolStripContainer1.TopToolStripPanel
      // 
      this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.tosTrialSelection);
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
      // bgwCalcFixationsForAllSubjects
      // 
      this.bgwCalcFixationsForAllSubjects.WorkerReportsProgress = true;
      this.bgwCalcFixationsForAllSubjects.WorkerSupportsCancellation = true;
      // 
      // ChartModule
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1096, 424);
      this.Controls.Add(this.toolStripContainer1);
      this.DataBindings.Add(new System.Windows.Forms.Binding("Location", global::Ogama.Properties.Settings.Default, "ChartModulLocation", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.KeyPreview = true;
      this.Location = global::Ogama.Properties.Settings.Default.ChartModulLocation;
      this.Logo = global::Ogama.Properties.Resources.Chart32;
      this.Name = "ChartModule";
      this.Text = "Chart Module";
      this.Load += new System.EventHandler(this.ChartModule_Load);
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
      this.spcChartOptions.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.spcChartOptions)).EndInit();
      this.spcChartOptions.ResumeLayout(false);
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
      this.splitContainer1.ResumeLayout(false);
      this.tbcParameters.ResumeLayout(false);
      this.tbcChartOptions.ResumeLayout(false);
      this.tbpLegend.ResumeLayout(false);
      this.tbpLegend.PerformLayout();
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.tbpCharttype.ResumeLayout(false);
      this.tbpCharttype.PerformLayout();
      this.toolStripContainer1.ContentPanel.ResumeLayout(false);
      this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
      this.toolStripContainer1.TopToolStripPanel.PerformLayout();
      this.toolStripContainer1.ResumeLayout(false);
      this.toolStripContainer1.PerformLayout();
      this.tosTrialSelection.ResumeLayout(false);
      this.tosTrialSelection.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.SplitContainer spcChartOptions;
    private System.Windows.Forms.ToolTip toolTip1;
    private System.Windows.Forms.ToolStripContainer toolStripContainer1;
    private System.Windows.Forms.ToolStrip tosTrialSelection;
    private System.Windows.Forms.ToolStripComboBox cbbSubject;
    private System.Windows.Forms.ToolStripButton btnHelp;
    private System.ComponentModel.BackgroundWorker bgwCalcFixationsForAllSubjects;
    private System.Windows.Forms.ToolStripComboBox cbbTrial;
    private System.Windows.Forms.ToolStripButton btnEye;
    private System.Windows.Forms.ToolStripButton btnMouse;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
    private System.Windows.Forms.SplitContainer splitContainer1;
    private System.Windows.Forms.TabControl tbcParameters;
    private System.Windows.Forms.TabPage tbpSubject;
    private System.Windows.Forms.TabPage tabPage2;
    private System.Windows.Forms.TabControl tbcChartOptions;
    private System.Windows.Forms.TabPage tbpLegend;
    private System.Windows.Forms.TabPage tabPage4;
    private System.Windows.Forms.TabPage tbpCharttype;
    private System.Windows.Forms.TabPage tbpAxes;
    private System.Windows.Forms.TextBox txbSeriesTitle;
    private System.Windows.Forms.TextBox txbLegend;
    private System.Windows.Forms.TextBox txbDiagramTitle;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.RadioButton rdbLegendPositionOutside;
    private System.Windows.Forms.RadioButton rdbLegendPositionInside;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.CheckBox chbShowLegend;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.RadioButton rdbChartTypeLine;
    private System.Windows.Forms.ImageList imlChartTypes;
    private System.Windows.Forms.RadioButton rdbChartTypeScatter;
    private System.Windows.Forms.RadioButton rdbChartTypeBubble;
    private System.Windows.Forms.RadioButton rdbChartTypePie;
    private System.Windows.Forms.RadioButton rdbChartTypeColumn;
    private System.Windows.Forms.RadioButton rdbChartTypeArea;
  }
}