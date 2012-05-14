using OgamaControls;

namespace Ogama.Modules.Saliency
{
  using Ogama.Modules.Common.Controls;

  partial class SaliencyModule
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SaliencyModule));
      this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
      this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
      this.spcPictureConsole = new System.Windows.Forms.SplitContainer();
      this.spcPicChannels = new System.Windows.Forms.SplitContainer();
      this.spcPicAndList = new System.Windows.Forms.SplitContainer();
      this.tacOptions = new System.Windows.Forms.TabControl();
      this.tabGeneral = new System.Windows.Forms.TabPage();
      this.label2 = new System.Windows.Forms.Label();
      this.nudCountFixations = new System.Windows.Forms.NumericUpDown();
      this.nudMilliseconds = new System.Windows.Forms.NumericUpDown();
      this.rdbStopAfterFixations = new System.Windows.Forms.RadioButton();
      this.rdbStopAfterTime = new System.Windows.Forms.RadioButton();
      this.label1 = new System.Windows.Forms.Label();
      this.tabSelectChannels = new System.Windows.Forms.TabPage();
      this.label3 = new System.Windows.Forms.Label();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.rdbColorS = new System.Windows.Forms.RadioButton();
      this.rdbColorG = new System.Windows.Forms.RadioButton();
      this.rdbColorC = new System.Windows.Forms.RadioButton();
      this.groupBox2 = new System.Windows.Forms.GroupBox();
      this.rdbIntensityN = new System.Windows.Forms.RadioButton();
      this.rdbIntensityI = new System.Windows.Forms.RadioButton();
      this.label4 = new System.Windows.Forms.Label();
      this.nudChannelObjectWeight = new System.Windows.Forms.NumericUpDown();
      this.nudChannelPedestrianWeight = new System.Windows.Forms.NumericUpDown();
      this.nudChannelSkinWeight = new System.Windows.Forms.NumericUpDown();
      this.nudChannelXJunctionWeight = new System.Windows.Forms.NumericUpDown();
      this.nudChannelLJunctionWeight = new System.Windows.Forms.NumericUpDown();
      this.nudChannelTJunctionWeight = new System.Windows.Forms.NumericUpDown();
      this.nudChannelOrientationWeight = new System.Windows.Forms.NumericUpDown();
      this.chbChannelTJunction = new System.Windows.Forms.CheckBox();
      this.chbChannelPedestrian = new System.Windows.Forms.CheckBox();
      this.nudChannelIntensityWeight = new System.Windows.Forms.NumericUpDown();
      this.chbChannelOrientation = new System.Windows.Forms.CheckBox();
      this.chbChannelIntensity = new System.Windows.Forms.CheckBox();
      this.chbChannelLJunction = new System.Windows.Forms.CheckBox();
      this.chbChannelSkin = new System.Windows.Forms.CheckBox();
      this.chbChannelColor = new System.Windows.Forms.CheckBox();
      this.chbChannelObject = new System.Windows.Forms.CheckBox();
      this.nudChannelColorWeight = new System.Windows.Forms.NumericUpDown();
      this.chbChannelXJunction = new System.Windows.Forms.CheckBox();
      this.rdbCustomChannels = new System.Windows.Forms.RadioButton();
      this.cbbPredefinedChannels = new System.Windows.Forms.ComboBox();
      this.rdbPredefinedChannels = new System.Windows.Forms.RadioButton();
      this.tabOptions = new System.Windows.Forms.TabPage();
      this.cbbSimulationViewerType = new System.Windows.Forms.ComboBox();
      this.label7 = new System.Windows.Forms.Label();
      this.cbbWinnerTakeAllType = new System.Windows.Forms.ComboBox();
      this.label6 = new System.Windows.Forms.Label();
      this.cbbSaliencyType = new System.Windows.Forms.ComboBox();
      this.label5 = new System.Windows.Forms.Label();
      this.pnlCanvas = new System.Windows.Forms.Panel();
      this.pnlPicture = new System.Windows.Forms.Panel();
      this.saliencyPicture = new Ogama.Modules.Saliency.SaliencyPicture();
      this.toolStrip1 = new System.Windows.Forms.ToolStrip();
      this.btnSeekNextSlide = new System.Windows.Forms.ToolStripButton();
      this.btnSeekPreviousSlide = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
      this.trialTimeLine = new TrialTimeLine(this.components);
      this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
      this.trbZoom = new OgamaControls.ToolStripTrackBar();
      this.tacChannels = new System.Windows.Forms.TabControl();
      this.tabChannels = new System.Windows.Forms.TabPage();
      this.lsvChannels = new System.Windows.Forms.ListView();
      this.imlChannels = new System.Windows.Forms.ImageList(this.components);
      this.tabSaliencyMaps = new System.Windows.Forms.TabPage();
      this.lsvSalmaps = new System.Windows.Forms.ListView();
      this.imlSalmaps = new System.Windows.Forms.ImageList(this.components);
      this.grpConsole = new System.Windows.Forms.GroupBox();
      this.txbConsole = new System.Windows.Forms.TextBox();
      this.tosTrial = new System.Windows.Forms.ToolStrip();
      this.cbbTrial = new System.Windows.Forms.ToolStripComboBox();
      this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.btnHelp = new System.Windows.Forms.ToolStripButton();
      this.tosGazeDisplay = new System.Windows.Forms.ToolStrip();
      this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
      this.toolStripLabel6 = new System.Windows.Forms.ToolStripLabel();
      this.cbbGazeDisplayMode = new System.Windows.Forms.ToolStripComboBox();
      this.btnGazeConnections = new System.Windows.Forms.ToolStripButton();
      this.btnGazeNumbers = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
      this.btnGazePenStyle = new System.Windows.Forms.ToolStripButton();
      this.nudGazeFixDiameterDiv = new OgamaControls.ToolStripNumericUpDown();
      this.tosCalculation = new System.Windows.Forms.ToolStrip();
      this.btnStartCalculation = new System.Windows.Forms.ToolStripButton();
      this.btnShowHideConsole = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
      this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
      this.btnShowChannels = new System.Windows.Forms.ToolStripButton();
      this.rdbLargeIcon = new OgamaControls.ToolStripRadioButton();
      this.rdbList = new OgamaControls.ToolStripRadioButton();
      this.tmrProgress = new System.Windows.Forms.Timer(this.components);
      this.toolStripContainer2 = new System.Windows.Forms.ToolStripContainer();
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
      this.spcPictureConsole.Panel1.SuspendLayout();
      this.spcPictureConsole.Panel2.SuspendLayout();
      this.spcPictureConsole.SuspendLayout();
      this.spcPicChannels.Panel1.SuspendLayout();
      this.spcPicChannels.Panel2.SuspendLayout();
      this.spcPicChannels.SuspendLayout();
      this.spcPicAndList.Panel1.SuspendLayout();
      this.spcPicAndList.Panel2.SuspendLayout();
      this.spcPicAndList.SuspendLayout();
      this.tacOptions.SuspendLayout();
      this.tabGeneral.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.nudCountFixations)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.nudMilliseconds)).BeginInit();
      this.tabSelectChannels.SuspendLayout();
      this.groupBox1.SuspendLayout();
      this.groupBox2.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.nudChannelObjectWeight)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.nudChannelPedestrianWeight)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.nudChannelSkinWeight)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.nudChannelXJunctionWeight)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.nudChannelLJunctionWeight)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.nudChannelTJunctionWeight)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.nudChannelOrientationWeight)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.nudChannelIntensityWeight)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.nudChannelColorWeight)).BeginInit();
      this.tabOptions.SuspendLayout();
      this.pnlCanvas.SuspendLayout();
      this.pnlPicture.SuspendLayout();
      this.toolStrip1.SuspendLayout();
      this.tacChannels.SuspendLayout();
      this.tabChannels.SuspendLayout();
      this.tabSaliencyMaps.SuspendLayout();
      this.grpConsole.SuspendLayout();
      this.tosTrial.SuspendLayout();
      this.tosGazeDisplay.SuspendLayout();
      this.tosCalculation.SuspendLayout();
      this.toolStripContainer2.BottomToolStripPanel.SuspendLayout();
      this.toolStripContainer2.ContentPanel.SuspendLayout();
      this.toolStripContainer2.SuspendLayout();
      this.SuspendLayout();
      // 
      // toolStripContainer1
      // 
      // 
      // toolStripContainer1.ContentPanel
      // 
      this.toolStripContainer1.ContentPanel.Controls.Add(this.spcPictureConsole);
      this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(801, 524);
      this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
      this.toolStripContainer1.Name = "toolStripContainer1";
      this.toolStripContainer1.Size = new System.Drawing.Size(801, 578);
      this.toolStripContainer1.TabIndex = 1;
      this.toolStripContainer1.Text = "toolStripContainer1";
      // 
      // toolStripContainer1.TopToolStripPanel
      // 
      this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.tosTrial);
      this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.tosGazeDisplay);
      this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.tosCalculation);
      // 
      // spcPictureConsole
      // 
      this.spcPictureConsole.Dock = System.Windows.Forms.DockStyle.Fill;
      this.spcPictureConsole.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
      this.spcPictureConsole.Location = new System.Drawing.Point(0, 0);
      this.spcPictureConsole.Name = "spcPictureConsole";
      this.spcPictureConsole.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // spcPictureConsole.Panel1
      // 
      this.spcPictureConsole.Panel1.Controls.Add(this.spcPicChannels);
      // 
      // spcPictureConsole.Panel2
      // 
      this.spcPictureConsole.Panel2.Controls.Add(this.grpConsole);
      this.spcPictureConsole.Size = new System.Drawing.Size(801, 524);
      this.spcPictureConsole.SplitterDistance = 388;
      this.spcPictureConsole.TabIndex = 12;
      // 
      // spcPicChannels
      // 
      this.spcPicChannels.Dock = System.Windows.Forms.DockStyle.Fill;
      this.spcPicChannels.Location = new System.Drawing.Point(0, 0);
      this.spcPicChannels.Name = "spcPicChannels";
      // 
      // spcPicChannels.Panel1
      // 
      this.spcPicChannels.Panel1.Controls.Add(this.spcPicAndList);
      // 
      // spcPicChannels.Panel2
      // 
      this.spcPicChannels.Panel2.Controls.Add(this.tacChannels);
      this.spcPicChannels.Size = new System.Drawing.Size(801, 388);
      this.spcPicChannels.SplitterDistance = 628;
      this.spcPicChannels.TabIndex = 6;
      // 
      // spcPicAndList
      // 
      this.spcPicAndList.Dock = System.Windows.Forms.DockStyle.Fill;
      this.spcPicAndList.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
      this.spcPicAndList.Location = new System.Drawing.Point(0, 0);
      this.spcPicAndList.Name = "spcPicAndList";
      // 
      // spcPicAndList.Panel1
      // 
      this.spcPicAndList.Panel1.Controls.Add(this.tacOptions);
      // 
      // spcPicAndList.Panel2
      // 
      this.spcPicAndList.Panel2.Controls.Add(this.toolStripContainer2);
      this.spcPicAndList.Size = new System.Drawing.Size(628, 388);
      this.spcPicAndList.SplitterDistance = 199;
      this.spcPicAndList.TabIndex = 0;
      // 
      // tacOptions
      // 
      this.tacOptions.Controls.Add(this.tabGeneral);
      this.tacOptions.Controls.Add(this.tabSelectChannels);
      this.tacOptions.Controls.Add(this.tabOptions);
      this.tacOptions.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tacOptions.Location = new System.Drawing.Point(0, 0);
      this.tacOptions.Name = "tacOptions";
      this.tacOptions.SelectedIndex = 0;
      this.tacOptions.Size = new System.Drawing.Size(199, 388);
      this.tacOptions.TabIndex = 1;
      // 
      // tabGeneral
      // 
      this.tabGeneral.Controls.Add(this.label2);
      this.tabGeneral.Controls.Add(this.nudCountFixations);
      this.tabGeneral.Controls.Add(this.nudMilliseconds);
      this.tabGeneral.Controls.Add(this.rdbStopAfterFixations);
      this.tabGeneral.Controls.Add(this.rdbStopAfterTime);
      this.tabGeneral.Controls.Add(this.label1);
      this.tabGeneral.Location = new System.Drawing.Point(4, 22);
      this.tabGeneral.Name = "tabGeneral";
      this.tabGeneral.Padding = new System.Windows.Forms.Padding(3);
      this.tabGeneral.Size = new System.Drawing.Size(191, 362);
      this.tabGeneral.TabIndex = 0;
      this.tabGeneral.Text = "General";
      this.tabGeneral.UseVisualStyleBackColor = true;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(125, 42);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(63, 13);
      this.label2.TabIndex = 5;
      this.label2.Text = "milliseconds";
      // 
      // nudCountFixations
      // 
      this.nudCountFixations.Location = new System.Drawing.Point(73, 14);
      this.nudCountFixations.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nudCountFixations.Name = "nudCountFixations";
      this.nudCountFixations.Size = new System.Drawing.Size(52, 20);
      this.nudCountFixations.TabIndex = 2;
      this.nudCountFixations.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.nudCountFixations.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
      // 
      // nudMilliseconds
      // 
      this.nudMilliseconds.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
      this.nudMilliseconds.Location = new System.Drawing.Point(73, 40);
      this.nudMilliseconds.Maximum = new decimal(new int[] {
            50000,
            0,
            0,
            0});
      this.nudMilliseconds.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nudMilliseconds.Name = "nudMilliseconds";
      this.nudMilliseconds.Size = new System.Drawing.Size(52, 20);
      this.nudMilliseconds.TabIndex = 4;
      this.nudMilliseconds.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.nudMilliseconds.Value = new decimal(new int[] {
            4000,
            0,
            0,
            0});
      // 
      // rdbStopAfterFixations
      // 
      this.rdbStopAfterFixations.AutoSize = true;
      this.rdbStopAfterFixations.Checked = true;
      this.rdbStopAfterFixations.Location = new System.Drawing.Point(3, 14);
      this.rdbStopAfterFixations.Name = "rdbStopAfterFixations";
      this.rdbStopAfterFixations.Size = new System.Drawing.Size(71, 17);
      this.rdbStopAfterFixations.TabIndex = 0;
      this.rdbStopAfterFixations.TabStop = true;
      this.rdbStopAfterFixations.Text = "Stop after";
      this.rdbStopAfterFixations.UseVisualStyleBackColor = true;
      // 
      // rdbStopAfterTime
      // 
      this.rdbStopAfterTime.AutoSize = true;
      this.rdbStopAfterTime.Location = new System.Drawing.Point(3, 40);
      this.rdbStopAfterTime.Name = "rdbStopAfterTime";
      this.rdbStopAfterTime.Size = new System.Drawing.Size(71, 17);
      this.rdbStopAfterTime.TabIndex = 3;
      this.rdbStopAfterTime.Text = "Stop after";
      this.rdbStopAfterTime.UseVisualStyleBackColor = true;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(125, 16);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(45, 13);
      this.label1.TabIndex = 1;
      this.label1.Text = "fixations";
      // 
      // tabSelectChannels
      // 
      this.tabSelectChannels.Controls.Add(this.label3);
      this.tabSelectChannels.Controls.Add(this.groupBox1);
      this.tabSelectChannels.Controls.Add(this.groupBox2);
      this.tabSelectChannels.Controls.Add(this.label4);
      this.tabSelectChannels.Controls.Add(this.nudChannelObjectWeight);
      this.tabSelectChannels.Controls.Add(this.nudChannelPedestrianWeight);
      this.tabSelectChannels.Controls.Add(this.nudChannelSkinWeight);
      this.tabSelectChannels.Controls.Add(this.nudChannelXJunctionWeight);
      this.tabSelectChannels.Controls.Add(this.nudChannelLJunctionWeight);
      this.tabSelectChannels.Controls.Add(this.nudChannelTJunctionWeight);
      this.tabSelectChannels.Controls.Add(this.nudChannelOrientationWeight);
      this.tabSelectChannels.Controls.Add(this.chbChannelTJunction);
      this.tabSelectChannels.Controls.Add(this.chbChannelPedestrian);
      this.tabSelectChannels.Controls.Add(this.nudChannelIntensityWeight);
      this.tabSelectChannels.Controls.Add(this.chbChannelOrientation);
      this.tabSelectChannels.Controls.Add(this.chbChannelIntensity);
      this.tabSelectChannels.Controls.Add(this.chbChannelLJunction);
      this.tabSelectChannels.Controls.Add(this.chbChannelSkin);
      this.tabSelectChannels.Controls.Add(this.chbChannelColor);
      this.tabSelectChannels.Controls.Add(this.chbChannelObject);
      this.tabSelectChannels.Controls.Add(this.nudChannelColorWeight);
      this.tabSelectChannels.Controls.Add(this.chbChannelXJunction);
      this.tabSelectChannels.Controls.Add(this.rdbCustomChannels);
      this.tabSelectChannels.Controls.Add(this.cbbPredefinedChannels);
      this.tabSelectChannels.Controls.Add(this.rdbPredefinedChannels);
      this.tabSelectChannels.Location = new System.Drawing.Point(4, 22);
      this.tabSelectChannels.Name = "tabSelectChannels";
      this.tabSelectChannels.Padding = new System.Windows.Forms.Padding(3);
      this.tabSelectChannels.Size = new System.Drawing.Size(191, 361);
      this.tabSelectChannels.TabIndex = 1;
      this.tabSelectChannels.Text = "Channels";
      this.tabSelectChannels.UseVisualStyleBackColor = true;
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label3.Location = new System.Drawing.Point(32, 83);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(81, 13);
      this.label3.TabIndex = 7;
      this.label3.Text = "Channel type";
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.rdbColorS);
      this.groupBox1.Controls.Add(this.rdbColorG);
      this.groupBox1.Controls.Add(this.rdbColorC);
      this.groupBox1.Location = new System.Drawing.Point(50, 92);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(92, 27);
      this.groupBox1.TabIndex = 10;
      this.groupBox1.TabStop = false;
      // 
      // rdbColorS
      // 
      this.rdbColorS.AutoSize = true;
      this.rdbColorS.Checked = true;
      this.rdbColorS.Location = new System.Drawing.Point(62, 8);
      this.rdbColorS.Name = "rdbColorS";
      this.rdbColorS.Size = new System.Drawing.Size(32, 17);
      this.rdbColorS.TabIndex = 8;
      this.rdbColorS.TabStop = true;
      this.rdbColorS.Text = "S";
      this.rdbColorS.UseVisualStyleBackColor = true;
      // 
      // rdbColorG
      // 
      this.rdbColorG.AutoSize = true;
      this.rdbColorG.Location = new System.Drawing.Point(32, 8);
      this.rdbColorG.Name = "rdbColorG";
      this.rdbColorG.Size = new System.Drawing.Size(33, 17);
      this.rdbColorG.TabIndex = 8;
      this.rdbColorG.Text = "G";
      this.rdbColorG.UseVisualStyleBackColor = true;
      // 
      // rdbColorC
      // 
      this.rdbColorC.AutoSize = true;
      this.rdbColorC.Location = new System.Drawing.Point(2, 8);
      this.rdbColorC.Name = "rdbColorC";
      this.rdbColorC.Size = new System.Drawing.Size(32, 17);
      this.rdbColorC.TabIndex = 8;
      this.rdbColorC.Text = "C";
      this.rdbColorC.UseVisualStyleBackColor = true;
      // 
      // groupBox2
      // 
      this.groupBox2.Controls.Add(this.rdbIntensityN);
      this.groupBox2.Controls.Add(this.rdbIntensityI);
      this.groupBox2.Location = new System.Drawing.Point(74, 117);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new System.Drawing.Size(67, 26);
      this.groupBox2.TabIndex = 11;
      this.groupBox2.TabStop = false;
      // 
      // rdbIntensityN
      // 
      this.rdbIntensityN.AutoSize = true;
      this.rdbIntensityN.Checked = true;
      this.rdbIntensityN.Location = new System.Drawing.Point(37, 8);
      this.rdbIntensityN.Name = "rdbIntensityN";
      this.rdbIntensityN.Size = new System.Drawing.Size(33, 17);
      this.rdbIntensityN.TabIndex = 9;
      this.rdbIntensityN.TabStop = true;
      this.rdbIntensityN.Text = "N";
      this.rdbIntensityN.UseVisualStyleBackColor = true;
      // 
      // rdbIntensityI
      // 
      this.rdbIntensityI.AutoSize = true;
      this.rdbIntensityI.Location = new System.Drawing.Point(7, 8);
      this.rdbIntensityI.Name = "rdbIntensityI";
      this.rdbIntensityI.Size = new System.Drawing.Size(28, 17);
      this.rdbIntensityI.TabIndex = 9;
      this.rdbIntensityI.Text = "I";
      this.rdbIntensityI.UseVisualStyleBackColor = true;
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label4.Location = new System.Drawing.Point(141, 83);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(47, 13);
      this.label4.TabIndex = 7;
      this.label4.Text = "Weight";
      // 
      // nudChannelObjectWeight
      // 
      this.nudChannelObjectWeight.Location = new System.Drawing.Point(144, 315);
      this.nudChannelObjectWeight.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nudChannelObjectWeight.Name = "nudChannelObjectWeight";
      this.nudChannelObjectWeight.Size = new System.Drawing.Size(40, 20);
      this.nudChannelObjectWeight.TabIndex = 6;
      this.nudChannelObjectWeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.nudChannelObjectWeight.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nudChannelObjectWeight.Visible = false;
      // 
      // nudChannelPedestrianWeight
      // 
      this.nudChannelPedestrianWeight.Location = new System.Drawing.Point(144, 291);
      this.nudChannelPedestrianWeight.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nudChannelPedestrianWeight.Name = "nudChannelPedestrianWeight";
      this.nudChannelPedestrianWeight.Size = new System.Drawing.Size(40, 20);
      this.nudChannelPedestrianWeight.TabIndex = 6;
      this.nudChannelPedestrianWeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.nudChannelPedestrianWeight.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
      // 
      // nudChannelSkinWeight
      // 
      this.nudChannelSkinWeight.Location = new System.Drawing.Point(144, 267);
      this.nudChannelSkinWeight.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nudChannelSkinWeight.Name = "nudChannelSkinWeight";
      this.nudChannelSkinWeight.Size = new System.Drawing.Size(40, 20);
      this.nudChannelSkinWeight.TabIndex = 6;
      this.nudChannelSkinWeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.nudChannelSkinWeight.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
      // 
      // nudChannelXJunctionWeight
      // 
      this.nudChannelXJunctionWeight.Location = new System.Drawing.Point(144, 243);
      this.nudChannelXJunctionWeight.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nudChannelXJunctionWeight.Name = "nudChannelXJunctionWeight";
      this.nudChannelXJunctionWeight.Size = new System.Drawing.Size(40, 20);
      this.nudChannelXJunctionWeight.TabIndex = 6;
      this.nudChannelXJunctionWeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.nudChannelXJunctionWeight.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
      // 
      // nudChannelLJunctionWeight
      // 
      this.nudChannelLJunctionWeight.Location = new System.Drawing.Point(144, 219);
      this.nudChannelLJunctionWeight.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nudChannelLJunctionWeight.Name = "nudChannelLJunctionWeight";
      this.nudChannelLJunctionWeight.Size = new System.Drawing.Size(40, 20);
      this.nudChannelLJunctionWeight.TabIndex = 6;
      this.nudChannelLJunctionWeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.nudChannelLJunctionWeight.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
      // 
      // nudChannelTJunctionWeight
      // 
      this.nudChannelTJunctionWeight.Location = new System.Drawing.Point(144, 195);
      this.nudChannelTJunctionWeight.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nudChannelTJunctionWeight.Name = "nudChannelTJunctionWeight";
      this.nudChannelTJunctionWeight.Size = new System.Drawing.Size(40, 20);
      this.nudChannelTJunctionWeight.TabIndex = 6;
      this.nudChannelTJunctionWeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.nudChannelTJunctionWeight.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
      // 
      // nudChannelOrientationWeight
      // 
      this.nudChannelOrientationWeight.Location = new System.Drawing.Point(144, 147);
      this.nudChannelOrientationWeight.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nudChannelOrientationWeight.Name = "nudChannelOrientationWeight";
      this.nudChannelOrientationWeight.Size = new System.Drawing.Size(40, 20);
      this.nudChannelOrientationWeight.TabIndex = 6;
      this.nudChannelOrientationWeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.nudChannelOrientationWeight.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
      // 
      // chbChannelTJunction
      // 
      this.chbChannelTJunction.AutoSize = true;
      this.chbChannelTJunction.Location = new System.Drawing.Point(6, 196);
      this.chbChannelTJunction.Margin = new System.Windows.Forms.Padding(0);
      this.chbChannelTJunction.Name = "chbChannelTJunction";
      this.chbChannelTJunction.Size = new System.Drawing.Size(115, 17);
      this.chbChannelTJunction.TabIndex = 5;
      this.chbChannelTJunction.Text = "T-junction detector";
      this.chbChannelTJunction.UseVisualStyleBackColor = true;
      this.chbChannelTJunction.CheckedChanged += new System.EventHandler(this.chbChannels_CheckedChanged);
      // 
      // chbChannelPedestrian
      // 
      this.chbChannelPedestrian.AutoSize = true;
      this.chbChannelPedestrian.Location = new System.Drawing.Point(6, 292);
      this.chbChannelPedestrian.Margin = new System.Windows.Forms.Padding(0);
      this.chbChannelPedestrian.Name = "chbChannelPedestrian";
      this.chbChannelPedestrian.Size = new System.Drawing.Size(75, 17);
      this.chbChannelPedestrian.TabIndex = 5;
      this.chbChannelPedestrian.Text = "pedestrian";
      this.chbChannelPedestrian.UseVisualStyleBackColor = true;
      this.chbChannelPedestrian.CheckedChanged += new System.EventHandler(this.chbChannels_CheckedChanged);
      // 
      // nudChannelIntensityWeight
      // 
      this.nudChannelIntensityWeight.Location = new System.Drawing.Point(144, 123);
      this.nudChannelIntensityWeight.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nudChannelIntensityWeight.Name = "nudChannelIntensityWeight";
      this.nudChannelIntensityWeight.Size = new System.Drawing.Size(40, 20);
      this.nudChannelIntensityWeight.TabIndex = 6;
      this.nudChannelIntensityWeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.nudChannelIntensityWeight.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
      // 
      // chbChannelOrientation
      // 
      this.chbChannelOrientation.AutoSize = true;
      this.chbChannelOrientation.Location = new System.Drawing.Point(6, 148);
      this.chbChannelOrientation.Margin = new System.Windows.Forms.Padding(0);
      this.chbChannelOrientation.Name = "chbChannelOrientation";
      this.chbChannelOrientation.Size = new System.Drawing.Size(75, 17);
      this.chbChannelOrientation.TabIndex = 5;
      this.chbChannelOrientation.Text = "orientation";
      this.chbChannelOrientation.UseVisualStyleBackColor = true;
      this.chbChannelOrientation.CheckedChanged += new System.EventHandler(this.chbChannels_CheckedChanged);
      // 
      // chbChannelIntensity
      // 
      this.chbChannelIntensity.AutoSize = true;
      this.chbChannelIntensity.Location = new System.Drawing.Point(6, 124);
      this.chbChannelIntensity.Margin = new System.Windows.Forms.Padding(0);
      this.chbChannelIntensity.Name = "chbChannelIntensity";
      this.chbChannelIntensity.Size = new System.Drawing.Size(64, 17);
      this.chbChannelIntensity.TabIndex = 5;
      this.chbChannelIntensity.Text = "intensity";
      this.chbChannelIntensity.UseVisualStyleBackColor = true;
      this.chbChannelIntensity.CheckedChanged += new System.EventHandler(this.chbChannels_CheckedChanged);
      // 
      // chbChannelLJunction
      // 
      this.chbChannelLJunction.AutoSize = true;
      this.chbChannelLJunction.Location = new System.Drawing.Point(6, 220);
      this.chbChannelLJunction.Margin = new System.Windows.Forms.Padding(0);
      this.chbChannelLJunction.Name = "chbChannelLJunction";
      this.chbChannelLJunction.Size = new System.Drawing.Size(114, 17);
      this.chbChannelLJunction.TabIndex = 5;
      this.chbChannelLJunction.Text = "L-junction detector";
      this.chbChannelLJunction.UseVisualStyleBackColor = true;
      this.chbChannelLJunction.CheckedChanged += new System.EventHandler(this.chbChannels_CheckedChanged);
      // 
      // chbChannelSkin
      // 
      this.chbChannelSkin.AutoSize = true;
      this.chbChannelSkin.Location = new System.Drawing.Point(6, 268);
      this.chbChannelSkin.Margin = new System.Windows.Forms.Padding(0);
      this.chbChannelSkin.Name = "chbChannelSkin";
      this.chbChannelSkin.Size = new System.Drawing.Size(108, 17);
      this.chbChannelSkin.TabIndex = 5;
      this.chbChannelSkin.Text = "skin hue detector";
      this.chbChannelSkin.UseVisualStyleBackColor = true;
      this.chbChannelSkin.CheckedChanged += new System.EventHandler(this.chbChannels_CheckedChanged);
      // 
      // chbChannelColor
      // 
      this.chbChannelColor.AutoSize = true;
      this.chbChannelColor.Location = new System.Drawing.Point(6, 100);
      this.chbChannelColor.Margin = new System.Windows.Forms.Padding(0);
      this.chbChannelColor.Name = "chbChannelColor";
      this.chbChannelColor.Size = new System.Drawing.Size(49, 17);
      this.chbChannelColor.TabIndex = 5;
      this.chbChannelColor.Text = "color";
      this.chbChannelColor.UseVisualStyleBackColor = true;
      this.chbChannelColor.CheckedChanged += new System.EventHandler(this.chbChannels_CheckedChanged);
      // 
      // chbChannelObject
      // 
      this.chbChannelObject.AutoSize = true;
      this.chbChannelObject.Location = new System.Drawing.Point(6, 316);
      this.chbChannelObject.Margin = new System.Windows.Forms.Padding(0);
      this.chbChannelObject.Name = "chbChannelObject";
      this.chbChannelObject.Size = new System.Drawing.Size(102, 17);
      this.chbChannelObject.TabIndex = 5;
      this.chbChannelObject.Text = "object detection";
      this.chbChannelObject.UseVisualStyleBackColor = true;
      this.chbChannelObject.Visible = false;
      this.chbChannelObject.CheckedChanged += new System.EventHandler(this.chbChannels_CheckedChanged);
      // 
      // nudChannelColorWeight
      // 
      this.nudChannelColorWeight.Location = new System.Drawing.Point(144, 99);
      this.nudChannelColorWeight.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nudChannelColorWeight.Name = "nudChannelColorWeight";
      this.nudChannelColorWeight.Size = new System.Drawing.Size(40, 20);
      this.nudChannelColorWeight.TabIndex = 4;
      this.nudChannelColorWeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.nudChannelColorWeight.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
      // 
      // chbChannelXJunction
      // 
      this.chbChannelXJunction.AutoSize = true;
      this.chbChannelXJunction.Location = new System.Drawing.Point(6, 244);
      this.chbChannelXJunction.Margin = new System.Windows.Forms.Padding(0);
      this.chbChannelXJunction.Name = "chbChannelXJunction";
      this.chbChannelXJunction.Size = new System.Drawing.Size(115, 17);
      this.chbChannelXJunction.TabIndex = 3;
      this.chbChannelXJunction.Text = "X-junction detector";
      this.chbChannelXJunction.UseVisualStyleBackColor = true;
      this.chbChannelXJunction.CheckedChanged += new System.EventHandler(this.chbChannels_CheckedChanged);
      // 
      // rdbCustomChannels
      // 
      this.rdbCustomChannels.AutoSize = true;
      this.rdbCustomChannels.Location = new System.Drawing.Point(6, 57);
      this.rdbCustomChannels.Name = "rdbCustomChannels";
      this.rdbCustomChannels.Size = new System.Drawing.Size(121, 17);
      this.rdbCustomChannels.TabIndex = 2;
      this.rdbCustomChannels.Text = "Custom Combination";
      this.rdbCustomChannels.UseVisualStyleBackColor = true;
      // 
      // cbbPredefinedChannels
      // 
      this.cbbPredefinedChannels.Items.AddRange(new object[] {
            "Std: use all standard channels with unit weights",
            "SurpStd: use all standard Surprise channels",
            "PN03contrast: Parkhurst & Niebur\'03 contrast model",
            "Variance: local variance in 16x16 image patches",
            "Scorr: spatial correlation between image patches in a frame"});
      this.cbbPredefinedChannels.Location = new System.Drawing.Point(19, 29);
      this.cbbPredefinedChannels.Name = "cbbPredefinedChannels";
      this.cbbPredefinedChannels.Size = new System.Drawing.Size(145, 21);
      this.cbbPredefinedChannels.TabIndex = 1;
      this.cbbPredefinedChannels.Text = "Std: use all standard channels with unit weights";
      this.cbbPredefinedChannels.SelectionChangeCommitted += new System.EventHandler(this.cbbPredefinedChannels_SelectionChangeCommitted);
      // 
      // rdbPredefinedChannels
      // 
      this.rdbPredefinedChannels.AutoSize = true;
      this.rdbPredefinedChannels.Checked = true;
      this.rdbPredefinedChannels.Location = new System.Drawing.Point(6, 6);
      this.rdbPredefinedChannels.Name = "rdbPredefinedChannels";
      this.rdbPredefinedChannels.Size = new System.Drawing.Size(137, 17);
      this.rdbPredefinedChannels.TabIndex = 0;
      this.rdbPredefinedChannels.TabStop = true;
      this.rdbPredefinedChannels.Text = "Predefined Combination";
      this.rdbPredefinedChannels.UseVisualStyleBackColor = true;
      // 
      // tabOptions
      // 
      this.tabOptions.Controls.Add(this.cbbSimulationViewerType);
      this.tabOptions.Controls.Add(this.label7);
      this.tabOptions.Controls.Add(this.cbbWinnerTakeAllType);
      this.tabOptions.Controls.Add(this.label6);
      this.tabOptions.Controls.Add(this.cbbSaliencyType);
      this.tabOptions.Controls.Add(this.label5);
      this.tabOptions.Location = new System.Drawing.Point(4, 22);
      this.tabOptions.Name = "tabOptions";
      this.tabOptions.Padding = new System.Windows.Forms.Padding(3);
      this.tabOptions.Size = new System.Drawing.Size(191, 361);
      this.tabOptions.TabIndex = 2;
      this.tabOptions.Text = "Options";
      this.tabOptions.UseVisualStyleBackColor = true;
      // 
      // cbbSimulationViewerType
      // 
      this.cbbSimulationViewerType.FormattingEnabled = true;
      this.cbbSimulationViewerType.Items.AddRange(new object[] {
            "None",
            "Std",
            "Compress",
            "EyeMvt",
            "EyeMvt2",
            "EyeSim",
            "ASAC",
            "NerdCam",
            "Stats",
            "RecStats"});
      this.cbbSimulationViewerType.Location = new System.Drawing.Point(126, 63);
      this.cbbSimulationViewerType.Name = "cbbSimulationViewerType";
      this.cbbSimulationViewerType.Size = new System.Drawing.Size(59, 21);
      this.cbbSimulationViewerType.TabIndex = 1;
      this.cbbSimulationViewerType.Text = "Std";
      // 
      // label7
      // 
      this.label7.AutoSize = true;
      this.label7.Location = new System.Drawing.Point(6, 66);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(78, 13);
      this.label7.TabIndex = 0;
      this.label7.Text = "Type of Viewer";
      // 
      // cbbWinnerTakeAllType
      // 
      this.cbbWinnerTakeAllType.FormattingEnabled = true;
      this.cbbWinnerTakeAllType.Items.AddRange(new object[] {
            "None",
            "Std",
            "StdOptim",
            "Fast",
            "Greedy",
            "Notice"});
      this.cbbWinnerTakeAllType.Location = new System.Drawing.Point(126, 36);
      this.cbbWinnerTakeAllType.Name = "cbbWinnerTakeAllType";
      this.cbbWinnerTakeAllType.Size = new System.Drawing.Size(59, 21);
      this.cbbWinnerTakeAllType.TabIndex = 1;
      this.cbbWinnerTakeAllType.Text = "Std";
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Location = new System.Drawing.Point(6, 39);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(122, 13);
      this.label6.TabIndex = 0;
      this.label6.Text = "Type of Winner Take All";
      // 
      // cbbSaliencyType
      // 
      this.cbbSaliencyType.FormattingEnabled = true;
      this.cbbSaliencyType.Items.AddRange(new object[] {
            "Std",
            "Trivial",
            "Fast"});
      this.cbbSaliencyType.Location = new System.Drawing.Point(126, 9);
      this.cbbSaliencyType.Name = "cbbSaliencyType";
      this.cbbSaliencyType.Size = new System.Drawing.Size(59, 21);
      this.cbbSaliencyType.TabIndex = 1;
      this.cbbSaliencyType.Text = "Std";
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(6, 12);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(110, 13);
      this.label5.TabIndex = 0;
      this.label5.Text = "Type of Saliency Map";
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
      this.pnlCanvas.Size = new System.Drawing.Size(425, 362);
      this.pnlCanvas.TabIndex = 2;
      // 
      // pnlPicture
      // 
      this.pnlPicture.Controls.Add(this.saliencyPicture);
      this.pnlPicture.Location = new System.Drawing.Point(113, 79);
      this.pnlPicture.Margin = new System.Windows.Forms.Padding(0);
      this.pnlPicture.Name = "pnlPicture";
      this.pnlPicture.Size = new System.Drawing.Size(300, 200);
      this.pnlPicture.TabIndex = 0;
      // 
      // saliencyPicture
      // 
      this.saliencyPicture.AnimationInterval = 10;
      this.saliencyPicture.BackColor = System.Drawing.Color.Black;
      this.saliencyPicture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
      this.saliencyPicture.InvalidateInterval = 500;
      this.saliencyPicture.Location = new System.Drawing.Point(0, 0);
      this.saliencyPicture.Margin = new System.Windows.Forms.Padding(0);
      this.saliencyPicture.Name = "saliencyPicture";
      this.saliencyPicture.Size = new System.Drawing.Size(300, 200);
      this.saliencyPicture.TabIndex = 0;
      this.saliencyPicture.TabStop = false;
      this.saliencyPicture.ZoomFactor = 0F;
      // 
      // toolStrip1
      // 
      this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
      this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
      this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnSeekNextSlide,
            this.btnSeekPreviousSlide,
            this.toolStripSeparator3,
            this.trialTimeLine,
            this.toolStripSeparator4,
            this.trbZoom});
      this.toolStrip1.Location = new System.Drawing.Point(0, 0);
      this.toolStrip1.Name = "toolStrip1";
      this.toolStrip1.Size = new System.Drawing.Size(425, 26);
      this.toolStrip1.Stretch = true;
      this.toolStrip1.TabIndex = 1;
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
      // toolStripSeparator3
      // 
      this.toolStripSeparator3.Name = "toolStripSeparator3";
      this.toolStripSeparator3.Size = new System.Drawing.Size(6, 26);
      // 
      // trialTimeLine
      // 
      this.trialTimeLine.Duration = 0;
      this.trialTimeLine.Name = "trialTimeLine";
      this.trialTimeLine.ShowCaret = false;
      this.trialTimeLine.ShowTimes = false;
      this.trialTimeLine.Size = new System.Drawing.Size(231, 23);
      this.trialTimeLine.Text = "trialTimeLine";
      // 
      // toolStripSeparator4
      // 
      this.toolStripSeparator4.Name = "toolStripSeparator4";
      this.toolStripSeparator4.Size = new System.Drawing.Size(6, 26);
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
      // tacChannels
      // 
      this.tacChannels.Controls.Add(this.tabChannels);
      this.tacChannels.Controls.Add(this.tabSaliencyMaps);
      this.tacChannels.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tacChannels.Location = new System.Drawing.Point(0, 0);
      this.tacChannels.Name = "tacChannels";
      this.tacChannels.SelectedIndex = 0;
      this.tacChannels.Size = new System.Drawing.Size(169, 388);
      this.tacChannels.TabIndex = 6;
      // 
      // tabChannels
      // 
      this.tabChannels.Controls.Add(this.lsvChannels);
      this.tabChannels.Location = new System.Drawing.Point(4, 22);
      this.tabChannels.Name = "tabChannels";
      this.tabChannels.Padding = new System.Windows.Forms.Padding(3);
      this.tabChannels.Size = new System.Drawing.Size(161, 362);
      this.tabChannels.TabIndex = 0;
      this.tabChannels.Text = "Channels";
      this.tabChannels.UseVisualStyleBackColor = true;
      // 
      // lsvChannels
      // 
      this.lsvChannels.BackColor = System.Drawing.SystemColors.Control;
      this.lsvChannels.Dock = System.Windows.Forms.DockStyle.Fill;
      this.lsvChannels.LargeImageList = this.imlChannels;
      this.lsvChannels.Location = new System.Drawing.Point(3, 3);
      this.lsvChannels.Name = "lsvChannels";
      this.lsvChannels.Size = new System.Drawing.Size(155, 356);
      this.lsvChannels.SmallImageList = this.imlChannels;
      this.lsvChannels.TabIndex = 1;
      this.lsvChannels.UseCompatibleStateImageBehavior = false;
      this.lsvChannels.SelectedIndexChanged += new System.EventHandler(this.lsvChannels_SelectedIndexChanged);
      // 
      // imlChannels
      // 
      this.imlChannels.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
      this.imlChannels.ImageSize = new System.Drawing.Size(16, 16);
      this.imlChannels.TransparentColor = System.Drawing.Color.Transparent;
      // 
      // tabSaliencyMaps
      // 
      this.tabSaliencyMaps.Controls.Add(this.lsvSalmaps);
      this.tabSaliencyMaps.Location = new System.Drawing.Point(4, 22);
      this.tabSaliencyMaps.Name = "tabSaliencyMaps";
      this.tabSaliencyMaps.Padding = new System.Windows.Forms.Padding(3);
      this.tabSaliencyMaps.Size = new System.Drawing.Size(161, 361);
      this.tabSaliencyMaps.TabIndex = 1;
      this.tabSaliencyMaps.Text = "Saliency Map";
      this.tabSaliencyMaps.UseVisualStyleBackColor = true;
      // 
      // lsvSalmaps
      // 
      this.lsvSalmaps.BackColor = System.Drawing.SystemColors.Control;
      this.lsvSalmaps.Dock = System.Windows.Forms.DockStyle.Fill;
      this.lsvSalmaps.LargeImageList = this.imlSalmaps;
      this.lsvSalmaps.Location = new System.Drawing.Point(3, 3);
      this.lsvSalmaps.Name = "lsvSalmaps";
      this.lsvSalmaps.Size = new System.Drawing.Size(155, 355);
      this.lsvSalmaps.SmallImageList = this.imlSalmaps;
      this.lsvSalmaps.TabIndex = 3;
      this.lsvSalmaps.UseCompatibleStateImageBehavior = false;
      this.lsvSalmaps.SelectedIndexChanged += new System.EventHandler(this.lsvSalmaps_SelectedIndexChanged);
      // 
      // imlSalmaps
      // 
      this.imlSalmaps.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
      this.imlSalmaps.ImageSize = new System.Drawing.Size(16, 16);
      this.imlSalmaps.TransparentColor = System.Drawing.Color.Transparent;
      // 
      // grpConsole
      // 
      this.grpConsole.Controls.Add(this.txbConsole);
      this.grpConsole.Dock = System.Windows.Forms.DockStyle.Fill;
      this.grpConsole.Location = new System.Drawing.Point(0, 0);
      this.grpConsole.Name = "grpConsole";
      this.grpConsole.Size = new System.Drawing.Size(801, 132);
      this.grpConsole.TabIndex = 0;
      this.grpConsole.TabStop = false;
      this.grpConsole.Text = "EZVision Console";
      // 
      // txbConsole
      // 
      this.txbConsole.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txbConsole.Location = new System.Drawing.Point(3, 16);
      this.txbConsole.Multiline = true;
      this.txbConsole.Name = "txbConsole";
      this.txbConsole.ReadOnly = true;
      this.txbConsole.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.txbConsole.Size = new System.Drawing.Size(795, 113);
      this.txbConsole.TabIndex = 0;
      // 
      // tosTrial
      // 
      this.tosTrial.DataBindings.Add(new System.Windows.Forms.Binding("Location", global::Ogama.Properties.Settings.Default, "SALToolbarLocation", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.tosTrial.Dock = System.Windows.Forms.DockStyle.None;
      this.tosTrial.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cbbTrial,
            this.toolStripSeparator1,
            this.btnHelp});
      this.tosTrial.Location = global::Ogama.Properties.Settings.Default.SALToolbarLocation;
      this.tosTrial.Name = "tosTrial";
      this.tosTrial.Size = new System.Drawing.Size(243, 25);
      this.tosTrial.TabIndex = 0;
      // 
      // cbbTrial
      // 
      this.cbbTrial.AutoSize = false;
      this.cbbTrial.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbbTrial.Name = "cbbTrial";
      this.cbbTrial.Size = new System.Drawing.Size(200, 23);
      // 
      // toolStripSeparator1
      // 
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
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
      this.tosGazeDisplay.DataBindings.Add(new System.Windows.Forms.Binding("Location", global::Ogama.Properties.Settings.Default, "SALGazeToolbarLocation", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.tosGazeDisplay.Dock = System.Windows.Forms.DockStyle.None;
      this.tosGazeDisplay.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton3,
            this.toolStripSeparator2,
            this.toolStripLabel6,
            this.cbbGazeDisplayMode,
            this.btnGazeConnections,
            this.btnGazeNumbers,
            this.toolStripSeparator9,
            this.btnGazePenStyle,
            this.nudGazeFixDiameterDiv});
      this.tosGazeDisplay.Location = global::Ogama.Properties.Settings.Default.SALGazeToolbarLocation;
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
      // toolStripSeparator2
      // 
      this.toolStripSeparator2.Name = "toolStripSeparator2";
      this.toolStripSeparator2.Size = new System.Drawing.Size(6, 26);
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
      this.nudGazeFixDiameterDiv.Click += new System.EventHandler(this.nudGazeFixDiameterDiv_ValueChanged);
      // 
      // tosCalculation
      // 
      this.tosCalculation.DataBindings.Add(new System.Windows.Forms.Binding("Location", global::Ogama.Properties.Settings.Default, "SALCalculateToolbarLocation", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.tosCalculation.Dock = System.Windows.Forms.DockStyle.None;
      this.tosCalculation.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnStartCalculation,
            this.btnShowHideConsole,
            this.toolStripSeparator7,
            this.toolStripLabel1,
            this.btnShowChannels,
            this.rdbLargeIcon,
            this.rdbList});
      this.tosCalculation.Location = global::Ogama.Properties.Settings.Default.SALCalculateToolbarLocation;
      this.tosCalculation.Name = "tosCalculation";
      this.tosCalculation.Size = new System.Drawing.Size(325, 28);
      this.tosCalculation.TabIndex = 1;
      // 
      // btnStartCalculation
      // 
      this.btnStartCalculation.Image = global::Ogama.Properties.Resources.CalculatorHS;
      this.btnStartCalculation.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnStartCalculation.Name = "btnStartCalculation";
      this.btnStartCalculation.Size = new System.Drawing.Size(76, 25);
      this.btnStartCalculation.Text = "Calculate";
      this.btnStartCalculation.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.btnStartCalculation.ToolTipText = "Starts calculation of attention map.";
      this.btnStartCalculation.Click += new System.EventHandler(this.btnStartCalculation_Click);
      // 
      // btnShowHideConsole
      // 
      this.btnShowHideConsole.Checked = true;
      this.btnShowHideConsole.CheckOnClick = true;
      this.btnShowHideConsole.CheckState = System.Windows.Forms.CheckState.Checked;
      this.btnShowHideConsole.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnShowHideConsole.Image = global::Ogama.Properties.Resources.VSObject_Object;
      this.btnShowHideConsole.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnShowHideConsole.Name = "btnShowHideConsole";
      this.btnShowHideConsole.Size = new System.Drawing.Size(23, 25);
      this.btnShowHideConsole.Text = "toolStripButton1";
      this.btnShowHideConsole.ToolTipText = "Show or hide console ouput of ezvision.";
      this.btnShowHideConsole.Click += new System.EventHandler(this.btnShowHideConsole_Click);
      // 
      // toolStripSeparator7
      // 
      this.toolStripSeparator7.Name = "toolStripSeparator7";
      this.toolStripSeparator7.Size = new System.Drawing.Size(6, 28);
      // 
      // toolStripLabel1
      // 
      this.toolStripLabel1.Name = "toolStripLabel1";
      this.toolStripLabel1.Size = new System.Drawing.Size(56, 25);
      this.toolStripLabel1.Text = "Channels";
      // 
      // btnShowChannels
      // 
      this.btnShowChannels.Checked = true;
      this.btnShowChannels.CheckOnClick = true;
      this.btnShowChannels.CheckState = System.Windows.Forms.CheckState.Checked;
      this.btnShowChannels.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnShowChannels.Image = global::Ogama.Properties.Resources.GenericPicDoc;
      this.btnShowChannels.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnShowChannels.Name = "btnShowChannels";
      this.btnShowChannels.Size = new System.Drawing.Size(23, 25);
      this.btnShowChannels.Text = "Channels";
      this.btnShowChannels.ToolTipText = "Show or hide Visual Cortex channels.";
      this.btnShowChannels.Click += new System.EventHandler(this.btnShowChannels_Click);
      // 
      // rdbLargeIcon
      // 
      this.rdbLargeIcon.AutoCheck = true;
      this.rdbLargeIcon.BackColor = System.Drawing.Color.Transparent;
      this.rdbLargeIcon.CheckAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.rdbLargeIcon.Checked = true;
      this.rdbLargeIcon.Name = "rdbLargeIcon";
      this.rdbLargeIcon.Padding = new System.Windows.Forms.Padding(3);
      this.rdbLargeIcon.Size = new System.Drawing.Size(86, 25);
      this.rdbLargeIcon.Text = "Large Icon";
      this.rdbLargeIcon.ToolTipText = "Show channel images as large icons.";
      this.rdbLargeIcon.CheckedChanged += new System.EventHandler(this.rdbLargeIcon_CheckedChanged);
      // 
      // rdbList
      // 
      this.rdbList.AutoCheck = true;
      this.rdbList.BackColor = System.Drawing.Color.Transparent;
      this.rdbList.CheckAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.rdbList.Checked = false;
      this.rdbList.Name = "rdbList";
      this.rdbList.Size = new System.Drawing.Size(43, 25);
      this.rdbList.Text = "List";
      this.rdbList.ToolTipText = "Show channel images in a list.";
      this.rdbList.CheckedChanged += new System.EventHandler(this.rdbLargeIcon_CheckedChanged);
      // 
      // tmrProgress
      // 
      this.tmrProgress.Interval = 400;
      this.tmrProgress.Tick += new System.EventHandler(this.tmrProgress_Tick);
      // 
      // toolStripContainer2
      // 
      // 
      // toolStripContainer2.BottomToolStripPanel
      // 
      this.toolStripContainer2.BottomToolStripPanel.Controls.Add(this.toolStrip1);
      // 
      // toolStripContainer2.ContentPanel
      // 
      this.toolStripContainer2.ContentPanel.Controls.Add(this.pnlCanvas);
      this.toolStripContainer2.ContentPanel.Margin = new System.Windows.Forms.Padding(0);
      this.toolStripContainer2.ContentPanel.Size = new System.Drawing.Size(425, 362);
      this.toolStripContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.toolStripContainer2.LeftToolStripPanelVisible = false;
      this.toolStripContainer2.Location = new System.Drawing.Point(0, 0);
      this.toolStripContainer2.Margin = new System.Windows.Forms.Padding(0);
      this.toolStripContainer2.Name = "toolStripContainer2";
      this.toolStripContainer2.RightToolStripPanelVisible = false;
      this.toolStripContainer2.Size = new System.Drawing.Size(425, 388);
      this.toolStripContainer2.TabIndex = 1;
      this.toolStripContainer2.Text = "toolStripContainer2";
      this.toolStripContainer2.TopToolStripPanelVisible = false;
      // 
      // SaliencyModule
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(801, 578);
      this.Controls.Add(this.toolStripContainer1);
      this.DataBindings.Add(new System.Windows.Forms.Binding("Location", global::Ogama.Properties.Settings.Default, "SaliencyModuleLocation", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.DoubleBuffered = true;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.KeyPreview = true;
      this.Location = global::Ogama.Properties.Settings.Default.SaliencyModuleLocation;
      this.Logo = global::Ogama.Properties.Resources.Saliency;
      this.Name = "SaliencyModule";
      this.Text = "Saliency Module";
      this.toolTip1.SetToolTip(this, "You can export this image via Edit-Copy or  Edit-SaveImage.");
      this.Load += new System.EventHandler(this.SaliencyModule_Load);
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SaliencyModule_FormClosing);
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
      this.spcPictureConsole.Panel1.ResumeLayout(false);
      this.spcPictureConsole.Panel2.ResumeLayout(false);
      this.spcPictureConsole.ResumeLayout(false);
      this.spcPicChannels.Panel1.ResumeLayout(false);
      this.spcPicChannels.Panel2.ResumeLayout(false);
      this.spcPicChannels.ResumeLayout(false);
      this.spcPicAndList.Panel1.ResumeLayout(false);
      this.spcPicAndList.Panel2.ResumeLayout(false);
      this.spcPicAndList.ResumeLayout(false);
      this.tacOptions.ResumeLayout(false);
      this.tabGeneral.ResumeLayout(false);
      this.tabGeneral.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.nudCountFixations)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.nudMilliseconds)).EndInit();
      this.tabSelectChannels.ResumeLayout(false);
      this.tabSelectChannels.PerformLayout();
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.groupBox2.ResumeLayout(false);
      this.groupBox2.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.nudChannelObjectWeight)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.nudChannelPedestrianWeight)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.nudChannelSkinWeight)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.nudChannelXJunctionWeight)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.nudChannelLJunctionWeight)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.nudChannelTJunctionWeight)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.nudChannelOrientationWeight)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.nudChannelIntensityWeight)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.nudChannelColorWeight)).EndInit();
      this.tabOptions.ResumeLayout(false);
      this.tabOptions.PerformLayout();
      this.pnlCanvas.ResumeLayout(false);
      this.pnlPicture.ResumeLayout(false);
      this.toolStrip1.ResumeLayout(false);
      this.toolStrip1.PerformLayout();
      this.tacChannels.ResumeLayout(false);
      this.tabChannels.ResumeLayout(false);
      this.tabSaliencyMaps.ResumeLayout(false);
      this.grpConsole.ResumeLayout(false);
      this.grpConsole.PerformLayout();
      this.tosTrial.ResumeLayout(false);
      this.tosTrial.PerformLayout();
      this.tosGazeDisplay.ResumeLayout(false);
      this.tosGazeDisplay.PerformLayout();
      this.tosCalculation.ResumeLayout(false);
      this.tosCalculation.PerformLayout();
      this.toolStripContainer2.BottomToolStripPanel.ResumeLayout(false);
      this.toolStripContainer2.BottomToolStripPanel.PerformLayout();
      this.toolStripContainer2.ContentPanel.ResumeLayout(false);
      this.toolStripContainer2.ResumeLayout(false);
      this.toolStripContainer2.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.ToolTip toolTip1;
    private System.Windows.Forms.ToolStripContainer toolStripContainer1;
    private System.Windows.Forms.ToolStrip tosTrial;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    private System.Windows.Forms.ToolStripButton btnHelp;
    private SaliencyPicture saliencyPicture;
    private System.Windows.Forms.SplitContainer spcPictureConsole;
    private System.Windows.Forms.SplitContainer spcPicAndList;
    private System.Windows.Forms.ToolStripComboBox cbbTrial;
    private System.Windows.Forms.Panel pnlCanvas;
    private System.Windows.Forms.Panel pnlPicture;
    private System.Windows.Forms.ToolStrip tosCalculation;
    private System.Windows.Forms.ToolStripButton btnStartCalculation;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
    private System.Windows.Forms.GroupBox grpConsole;
    private System.Windows.Forms.TextBox txbConsole;
    private System.Windows.Forms.RadioButton rdbStopAfterFixations;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.NumericUpDown nudMilliseconds;
    private System.Windows.Forms.RadioButton rdbStopAfterTime;
    private System.Windows.Forms.NumericUpDown nudCountFixations;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.ToolStripButton btnShowChannels;
    private System.Windows.Forms.TabControl tacOptions;
    private System.Windows.Forms.TabPage tabGeneral;
    private System.Windows.Forms.TabPage tabSelectChannels;
    private System.Windows.Forms.NumericUpDown nudChannelColorWeight;
    private System.Windows.Forms.CheckBox chbChannelXJunction;
    private System.Windows.Forms.RadioButton rdbCustomChannels;
    private System.Windows.Forms.ComboBox cbbPredefinedChannels;
    private System.Windows.Forms.RadioButton rdbPredefinedChannels;
    private System.Windows.Forms.NumericUpDown nudChannelLJunctionWeight;
    private System.Windows.Forms.NumericUpDown nudChannelTJunctionWeight;
    private System.Windows.Forms.NumericUpDown nudChannelOrientationWeight;
    private System.Windows.Forms.NumericUpDown nudChannelIntensityWeight;
    private System.Windows.Forms.CheckBox chbChannelIntensity;
    private System.Windows.Forms.CheckBox chbChannelLJunction;
    private System.Windows.Forms.CheckBox chbChannelSkin;
    private System.Windows.Forms.CheckBox chbChannelColor;
    private System.Windows.Forms.CheckBox chbChannelObject;
    private System.Windows.Forms.NumericUpDown nudChannelObjectWeight;
    private System.Windows.Forms.NumericUpDown nudChannelPedestrianWeight;
    private System.Windows.Forms.NumericUpDown nudChannelSkinWeight;
    private System.Windows.Forms.NumericUpDown nudChannelXJunctionWeight;
    private System.Windows.Forms.CheckBox chbChannelTJunction;
    private System.Windows.Forms.CheckBox chbChannelPedestrian;
    private System.Windows.Forms.CheckBox chbChannelOrientation;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.TabPage tabOptions;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.ComboBox cbbSaliencyType;
    private System.Windows.Forms.ComboBox cbbWinnerTakeAllType;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.ComboBox cbbSimulationViewerType;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.ToolStrip tosGazeDisplay;
    private System.Windows.Forms.ToolStripButton toolStripButton3;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    private System.Windows.Forms.ToolStripLabel toolStripLabel6;
    private System.Windows.Forms.ToolStripComboBox cbbGazeDisplayMode;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
    private System.Windows.Forms.ToolStripButton btnGazePenStyle;
    private ToolStripNumericUpDown nudGazeFixDiameterDiv;
    private System.Windows.Forms.Timer tmrProgress;
    private System.Windows.Forms.RadioButton rdbColorS;
    private System.Windows.Forms.RadioButton rdbColorG;
    private System.Windows.Forms.RadioButton rdbColorC;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.RadioButton rdbIntensityI;
    private System.Windows.Forms.RadioButton rdbIntensityN;
    private System.Windows.Forms.GroupBox groupBox2;
    private System.Windows.Forms.SplitContainer spcPicChannels;
    private System.Windows.Forms.TabControl tacChannels;
    private System.Windows.Forms.TabPage tabChannels;
    private System.Windows.Forms.TabPage tabSaliencyMaps;
    private System.Windows.Forms.ListView lsvChannels;
    private System.Windows.Forms.ListView lsvSalmaps;
    private ToolStripRadioButton rdbLargeIcon;
    private ToolStripRadioButton rdbList;
    private System.Windows.Forms.ImageList imlChannels;
    private System.Windows.Forms.ImageList imlSalmaps;
    private System.Windows.Forms.ToolStripButton btnShowHideConsole;
    private System.Windows.Forms.ToolStripLabel toolStripLabel1;
    private System.Windows.Forms.ToolStripButton btnGazeConnections;
    private System.Windows.Forms.ToolStripButton btnGazeNumbers;
    private System.Windows.Forms.ToolStrip toolStrip1;
    private System.Windows.Forms.ToolStripButton btnSeekNextSlide;
    private System.Windows.Forms.ToolStripButton btnSeekPreviousSlide;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
    private TrialTimeLine trialTimeLine;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
    private ToolStripTrackBar trbZoom;
    private System.Windows.Forms.ToolStripContainer toolStripContainer2;

  }
}