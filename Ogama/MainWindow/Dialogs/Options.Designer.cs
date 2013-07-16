namespace Ogama.MainWindow.Dialogs
{
  using Ogama.Modules.Common.Controls;

  using OgamaControls;

  partial class Options
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Options));
      this.tabControl1 = new System.Windows.Forms.TabControl();
      this.tbpReplay = new System.Windows.Forms.TabPage();
      this.btnNoDataStyle = new System.Windows.Forms.Button();
      this.psaNoData = new OgamaControls.PenStyleArea();
      this.label35 = new System.Windows.Forms.Label();
      this.pictureBox2 = new System.Windows.Forms.PictureBox();
      this.label22 = new System.Windows.Forms.Label();
      this.label29 = new System.Windows.Forms.Label();
      this.label24 = new System.Windows.Forms.Label();
      this.cbbSpeed = new System.Windows.Forms.ComboBox();
      this.nudMaxPointsPolyline = new System.Windows.Forms.NumericUpDown();
      this.nudFixShown = new System.Windows.Forms.NumericUpDown();
      this.tbpReplayGaze = new System.Windows.Forms.TabPage();
      this.toolStripGaze = new System.Windows.Forms.ToolStrip();
      this.btnGazeModeCursor = new System.Windows.Forms.ToolStripButton();
      this.btnGazeModePath = new System.Windows.Forms.ToolStripButton();
      this.btnGazeModeFix = new System.Windows.Forms.ToolStripButton();
      this.btnGazeModeFixCon = new System.Windows.Forms.ToolStripButton();
      this.btnGazeModeSpot = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
      this.btnGazeCutPath = new System.Windows.Forms.ToolStripButton();
      this.btnGazeBlinks = new System.Windows.Forms.ToolStripButton();
      this.pictureBox4 = new System.Windows.Forms.PictureBox();
      this.pictureBox3 = new System.Windows.Forms.PictureBox();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.label36 = new System.Windows.Forms.Label();
      this.nudGazeCursorSize = new System.Windows.Forms.NumericUpDown();
      this.btnGazeFixConStyle = new System.Windows.Forms.Button();
      this.btnGazeFixStyle = new System.Windows.Forms.Button();
      this.btnGazePathStyle = new System.Windows.Forms.Button();
      this.btnGazeCursorStyle = new System.Windows.Forms.Button();
      this.psaGazeFixCon = new OgamaControls.PenStyleArea();
      this.psaGazeFix = new OgamaControls.PenStyleArea();
      this.psaGazePath = new OgamaControls.PenStyleArea();
      this.psaGazeCursor = new OgamaControls.PenStyleArea();
      this.cbbGazeCursorType = new System.Windows.Forms.ComboBox();
      this.label25 = new System.Windows.Forms.Label();
      this.label23 = new System.Windows.Forms.Label();
      this.label26 = new System.Windows.Forms.Label();
      this.label27 = new System.Windows.Forms.Label();
      this.label28 = new System.Windows.Forms.Label();
      this.label20 = new System.Windows.Forms.Label();
      this.toolStrip4 = new System.Windows.Forms.ToolStrip();
      this.toolStrip2 = new System.Windows.Forms.ToolStrip();
      this.toolStrip3 = new System.Windows.Forms.ToolStrip();
      this.toolStrip1 = new System.Windows.Forms.ToolStrip();
      this.tpbReplayMouse = new System.Windows.Forms.TabPage();
      this.toolStripMouse = new System.Windows.Forms.ToolStrip();
      this.btnMouseModeCursor = new System.Windows.Forms.ToolStripButton();
      this.btnMouseModePath = new System.Windows.Forms.ToolStripButton();
      this.btnMouseModeFix = new System.Windows.Forms.ToolStripButton();
      this.btnMouseModeFixCon = new System.Windows.Forms.ToolStripButton();
      this.btnMouseModeSpot = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
      this.btnMouseCutPath = new System.Windows.Forms.ToolStripButton();
      this.groupBox2 = new System.Windows.Forms.GroupBox();
      this.label37 = new System.Windows.Forms.Label();
      this.nudMouseCursorSize = new System.Windows.Forms.NumericUpDown();
      this.btnMouseFixConStyle = new System.Windows.Forms.Button();
      this.btnMouseFixStyle = new System.Windows.Forms.Button();
      this.btnMousePathStyle = new System.Windows.Forms.Button();
      this.btnMouseCursorStyle = new System.Windows.Forms.Button();
      this.psaMouseFixCon = new OgamaControls.PenStyleArea();
      this.psaMouseFix = new OgamaControls.PenStyleArea();
      this.psaMousePath = new OgamaControls.PenStyleArea();
      this.psaMouseCursor = new OgamaControls.PenStyleArea();
      this.cbbMouseCursorType = new System.Windows.Forms.ComboBox();
      this.label30 = new System.Windows.Forms.Label();
      this.label31 = new System.Windows.Forms.Label();
      this.label32 = new System.Windows.Forms.Label();
      this.label33 = new System.Windows.Forms.Label();
      this.label34 = new System.Windows.Forms.Label();
      this.pictureBox5 = new System.Windows.Forms.PictureBox();
      this.pictureBox6 = new System.Windows.Forms.PictureBox();
      this.label21 = new System.Windows.Forms.Label();
      this.toolStrip5 = new System.Windows.Forms.ToolStrip();
      this.tbpFixationsInterface = new System.Windows.Forms.TabPage();
      this.grpMouseFixations = new System.Windows.Forms.GroupBox();
      this.label3 = new System.Windows.Forms.Label();
      this.label4 = new System.Windows.Forms.Label();
      this.label5 = new System.Windows.Forms.Label();
      this.cbbMouseFixationsDisplayMode = new System.Windows.Forms.ComboBox();
      this.psaMouseFixationsPenStyle = new OgamaControls.PenStyleArea();
      this.fsaMouseFixationsFont = new OgamaControls.FontStyleArea();
      this.btnMouseFixationsPenStyle = new System.Windows.Forms.Button();
      this.btnMouseFixationsFontStyle = new System.Windows.Forms.Button();
      this.grbGazeFixations = new System.Windows.Forms.GroupBox();
      this.label11 = new System.Windows.Forms.Label();
      this.label12 = new System.Windows.Forms.Label();
      this.label13 = new System.Windows.Forms.Label();
      this.cbbGazeFixationsDisplayMode = new System.Windows.Forms.ComboBox();
      this.psaGazeFixationsPenStyle = new OgamaControls.PenStyleArea();
      this.fsaGazeFixationsFont = new OgamaControls.FontStyleArea();
      this.btnGazeFixationsPenStyle = new System.Windows.Forms.Button();
      this.btnGazeFixationsFontStyle = new System.Windows.Forms.Button();
      this.pictureBox9 = new System.Windows.Forms.PictureBox();
      this.tpbAOI = new System.Windows.Forms.TabPage();
      this.pictureBox8 = new System.Windows.Forms.PictureBox();
      this.btnAOISearchRectFont = new System.Windows.Forms.Button();
      this.btnAOITargetFont = new System.Windows.Forms.Button();
      this.btnAOIStandardFont = new System.Windows.Forms.Button();
      this.btnAOIStandardStyle = new System.Windows.Forms.Button();
      this.btnAOITargetStyle = new System.Windows.Forms.Button();
      this.btnAOISearchRectStyle = new System.Windows.Forms.Button();
      this.label19 = new System.Windows.Forms.Label();
      this.label18 = new System.Windows.Forms.Label();
      this.label17 = new System.Windows.Forms.Label();
      this.label14 = new System.Windows.Forms.Label();
      this.label15 = new System.Windows.Forms.Label();
      this.label16 = new System.Windows.Forms.Label();
      this.fsaAOISearchRect = new OgamaControls.FontStyleArea();
      this.fsaAOITarget = new OgamaControls.FontStyleArea();
      this.fsaAOIDefault = new OgamaControls.FontStyleArea();
      this.psaAOIStandard = new OgamaControls.PenStyleArea();
      this.psaAOITarget = new OgamaControls.PenStyleArea();
      this.psaAOISearchRect = new OgamaControls.PenStyleArea();
      this.tbpMain = new System.Windows.Forms.TabPage();
      this.label6 = new System.Windows.Forms.Label();
      this.cbbPresentationMonitor = new System.Windows.Forms.ComboBox();
      this.label8 = new System.Windows.Forms.Label();
      this.pictureBox7 = new System.Windows.Forms.PictureBox();
      this.label39 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.nudRecentFiles = new System.Windows.Forms.NumericUpDown();
      this.clbBackground = new OgamaControls.ColorButton(this.components);
      this.imageList1 = new System.Windows.Forms.ImageList(this.components);
      this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
      this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.toolStripButton8 = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
      this.toolStripButton9 = new System.Windows.Forms.ToolStripButton();
      this.toolStripButton10 = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
      this.btnOK = new System.Windows.Forms.Button();
      this.btnCancel = new System.Windows.Forms.Button();
      this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
      this.panel2 = new System.Windows.Forms.Panel();
      this.label9 = new System.Windows.Forms.Label();
      this.dialogTop1 = new DialogTop();
      this.button1 = new System.Windows.Forms.Button();
      this.btnGazeCursorMode = new System.Windows.Forms.ToolStripButton();
      this.btnGazePathMode = new System.Windows.Forms.ToolStripButton();
      this.btnGazeFixMode = new System.Windows.Forms.ToolStripButton();
      this.btnGazeFixConMode = new System.Windows.Forms.ToolStripButton();
      this.btnGazeSpotlightMode = new System.Windows.Forms.ToolStripButton();
      this.btnGazeCutPathMode = new System.Windows.Forms.ToolStripButton();
      this.btnGazeBlinksMode = new System.Windows.Forms.ToolStripButton();
      this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
      this.toolStripButton5 = new System.Windows.Forms.ToolStripButton();
      this.toolStripButton6 = new System.Windows.Forms.ToolStripButton();
      this.toolStripButton7 = new System.Windows.Forms.ToolStripButton();
      this.btnMouseCursorMode = new System.Windows.Forms.ToolStripButton();
      this.btnMousePathMode = new System.Windows.Forms.ToolStripButton();
      this.btnMouseFixMode = new System.Windows.Forms.ToolStripButton();
      this.btnMouseFixConMode = new System.Windows.Forms.ToolStripButton();
      this.btnMouseSpotlightMode = new System.Windows.Forms.ToolStripButton();
      this.btnMouseCutPathMode = new System.Windows.Forms.ToolStripButton();
      this.tabControl1.SuspendLayout();
      this.tbpReplay.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.nudMaxPointsPolyline)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.nudFixShown)).BeginInit();
      this.tbpReplayGaze.SuspendLayout();
      this.toolStripGaze.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
      this.groupBox1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.nudGazeCursorSize)).BeginInit();
      this.tpbReplayMouse.SuspendLayout();
      this.toolStripMouse.SuspendLayout();
      this.groupBox2.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.nudMouseCursorSize)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
      this.tbpFixationsInterface.SuspendLayout();
      this.grpMouseFixations.SuspendLayout();
      this.grbGazeFixations.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox9)).BeginInit();
      this.tpbAOI.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).BeginInit();
      this.tbpMain.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.nudRecentFiles)).BeginInit();
      this.tableLayoutPanel1.SuspendLayout();
      this.panel2.SuspendLayout();
      this.SuspendLayout();
      // 
      // tabControl1
      // 
      this.tabControl1.Controls.Add(this.tbpReplay);
      this.tabControl1.Controls.Add(this.tbpReplayGaze);
      this.tabControl1.Controls.Add(this.tpbReplayMouse);
      this.tabControl1.Controls.Add(this.tbpFixationsInterface);
      this.tabControl1.Controls.Add(this.tpbAOI);
      this.tabControl1.Controls.Add(this.tbpMain);
      this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tabControl1.ImageList = this.imageList1;
      this.tabControl1.Location = new System.Drawing.Point(3, 69);
      this.tabControl1.Multiline = true;
      this.tabControl1.Name = "tabControl1";
      this.tabControl1.SelectedIndex = 0;
      this.tabControl1.Size = new System.Drawing.Size(495, 277);
      this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.FillToRight;
      this.tabControl1.TabIndex = 0;
      // 
      // tbpReplay
      // 
      this.tbpReplay.Controls.Add(this.btnNoDataStyle);
      this.tbpReplay.Controls.Add(this.psaNoData);
      this.tbpReplay.Controls.Add(this.label35);
      this.tbpReplay.Controls.Add(this.pictureBox2);
      this.tbpReplay.Controls.Add(this.label22);
      this.tbpReplay.Controls.Add(this.label29);
      this.tbpReplay.Controls.Add(this.label24);
      this.tbpReplay.Controls.Add(this.cbbSpeed);
      this.tbpReplay.Controls.Add(this.nudMaxPointsPolyline);
      this.tbpReplay.Controls.Add(this.nudFixShown);
      this.tbpReplay.ImageIndex = 0;
      this.tbpReplay.Location = new System.Drawing.Point(4, 42);
      this.tbpReplay.Name = "tbpReplay";
      this.tbpReplay.Size = new System.Drawing.Size(487, 231);
      this.tbpReplay.TabIndex = 6;
      this.tbpReplay.Text = "Replay Module";
      this.tbpReplay.UseVisualStyleBackColor = true;
      // 
      // btnNoDataStyle
      // 
      this.btnNoDataStyle.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
      this.btnNoDataStyle.Image = global::Ogama.Properties.Resources.RPLFixConPen;
      this.btnNoDataStyle.Location = new System.Drawing.Point(313, 119);
      this.btnNoDataStyle.Margin = new System.Windows.Forms.Padding(0);
      this.btnNoDataStyle.Name = "btnNoDataStyle";
      this.btnNoDataStyle.Size = new System.Drawing.Size(20, 20);
      this.btnNoDataStyle.TabIndex = 19;
      this.btnNoDataStyle.UseVisualStyleBackColor = true;
      this.btnNoDataStyle.Click += new System.EventHandler(this.btnNoDataStyle_Click);
      // 
      // psaNoData
      // 
      this.psaNoData.BackColor = System.Drawing.Color.White;
      this.psaNoData.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.psaNoData.DataBindings.Add(new System.Windows.Forms.Binding("PenColor", global::Ogama.Properties.Settings.Default, "NoDataPenColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.psaNoData.DataBindings.Add(new System.Windows.Forms.Binding("PenDashStyle", global::Ogama.Properties.Settings.Default, "NoDataPenStyle", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.psaNoData.DataBindings.Add(new System.Windows.Forms.Binding("PenSize", global::Ogama.Properties.Settings.Default, "NoDataPenSize", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.psaNoData.Location = new System.Drawing.Point(169, 120);
      this.psaNoData.Name = "psaNoData";
      this.psaNoData.PenColor = global::Ogama.Properties.Settings.Default.NoDataPenColor;
      this.psaNoData.PenDashStyle = global::Ogama.Properties.Settings.Default.NoDataPenStyle;
      this.psaNoData.PenSize = global::Ogama.Properties.Settings.Default.NoDataPenSize;
      this.psaNoData.Size = new System.Drawing.Size(130, 20);
      this.psaNoData.TabIndex = 18;
      // 
      // label35
      // 
      this.label35.AutoSize = true;
      this.label35.Location = new System.Drawing.Point(61, 123);
      this.label35.Name = "label35";
      this.label35.Size = new System.Drawing.Size(68, 13);
      this.label35.TabIndex = 17;
      this.label35.Text = "No Data pen";
      // 
      // pictureBox2
      // 
      this.pictureBox2.Image = global::Ogama.Properties.Resources.ReplayLogo;
      this.pictureBox2.Location = new System.Drawing.Point(3, 3);
      this.pictureBox2.Name = "pictureBox2";
      this.pictureBox2.Size = new System.Drawing.Size(33, 38);
      this.pictureBox2.TabIndex = 16;
      this.pictureBox2.TabStop = false;
      // 
      // label22
      // 
      this.label22.AutoSize = true;
      this.label22.Location = new System.Drawing.Point(61, 88);
      this.label22.Name = "label22";
      this.label22.Size = new System.Drawing.Size(75, 13);
      this.label22.TabIndex = 14;
      this.label22.Text = "Replay speed:";
      // 
      // label29
      // 
      this.label29.Location = new System.Drawing.Point(61, 11);
      this.label29.Name = "label29";
      this.label29.Size = new System.Drawing.Size(182, 30);
      this.label29.TabIndex = 13;
      this.label29.Text = "Maximum number of fixations shown (used during reduced data setting):";
      // 
      // label24
      // 
      this.label24.Location = new System.Drawing.Point(61, 45);
      this.label24.Name = "label24";
      this.label24.Size = new System.Drawing.Size(205, 34);
      this.label24.TabIndex = 12;
      this.label24.Text = "Maximum number of path polyline points (used during reduced data setting):";
      // 
      // cbbSpeed
      // 
      this.cbbSpeed.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Ogama.Properties.Settings.Default, "ReplaySpeed", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.cbbSpeed.FormattingEnabled = true;
      this.cbbSpeed.Items.AddRange(new object[] {
            "0.1x",
            "0.2x",
            "0.3x",
            "0.5x",
            "1.0x",
            "2.0x",
            "3.0x",
            "5.0x",
            "10 x"});
      this.cbbSpeed.Location = new System.Drawing.Point(270, 80);
      this.cbbSpeed.Name = "cbbSpeed";
      this.cbbSpeed.Size = new System.Drawing.Size(63, 21);
      this.cbbSpeed.TabIndex = 15;
      this.cbbSpeed.Text = global::Ogama.Properties.Settings.Default.ReplaySpeed;
      // 
      // nudMaxPointsPolyline
      // 
      this.nudMaxPointsPolyline.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::Ogama.Properties.Settings.Default, "MaxPointsPolyline", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.nudMaxPointsPolyline.Location = new System.Drawing.Point(270, 45);
      this.nudMaxPointsPolyline.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
      this.nudMaxPointsPolyline.Minimum = new decimal(new int[] {
            20,
            0,
            0,
            0});
      this.nudMaxPointsPolyline.Name = "nudMaxPointsPolyline";
      this.nudMaxPointsPolyline.Size = new System.Drawing.Size(63, 20);
      this.nudMaxPointsPolyline.TabIndex = 10;
      this.nudMaxPointsPolyline.Value = global::Ogama.Properties.Settings.Default.MaxPointsPolyline;
      // 
      // nudFixShown
      // 
      this.nudFixShown.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::Ogama.Properties.Settings.Default, "MaxNumberFixations", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.nudFixShown.Location = new System.Drawing.Point(270, 11);
      this.nudFixShown.Maximum = new decimal(new int[] {
            9,
            0,
            0,
            0});
      this.nudFixShown.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
      this.nudFixShown.Name = "nudFixShown";
      this.nudFixShown.Size = new System.Drawing.Size(63, 20);
      this.nudFixShown.TabIndex = 11;
      this.nudFixShown.Value = global::Ogama.Properties.Settings.Default.MaxNumberFixations;
      // 
      // tbpReplayGaze
      // 
      this.tbpReplayGaze.Controls.Add(this.toolStripGaze);
      this.tbpReplayGaze.Controls.Add(this.pictureBox4);
      this.tbpReplayGaze.Controls.Add(this.pictureBox3);
      this.tbpReplayGaze.Controls.Add(this.groupBox1);
      this.tbpReplayGaze.Controls.Add(this.label20);
      this.tbpReplayGaze.Controls.Add(this.toolStrip4);
      this.tbpReplayGaze.Controls.Add(this.toolStrip2);
      this.tbpReplayGaze.Controls.Add(this.toolStrip3);
      this.tbpReplayGaze.Controls.Add(this.toolStrip1);
      this.tbpReplayGaze.ImageIndex = 0;
      this.tbpReplayGaze.Location = new System.Drawing.Point(4, 42);
      this.tbpReplayGaze.Name = "tbpReplayGaze";
      this.tbpReplayGaze.Padding = new System.Windows.Forms.Padding(3);
      this.tbpReplayGaze.Size = new System.Drawing.Size(487, 231);
      this.tbpReplayGaze.TabIndex = 2;
      this.tbpReplayGaze.Text = "Replay Gaze";
      this.tbpReplayGaze.UseVisualStyleBackColor = true;
      // 
      // toolStripGaze
      // 
      this.toolStripGaze.Dock = System.Windows.Forms.DockStyle.None;
      this.toolStripGaze.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
      this.toolStripGaze.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnGazeModeCursor,
            this.btnGazeModePath,
            this.btnGazeModeFix,
            this.btnGazeModeFixCon,
            this.btnGazeModeSpot,
            this.toolStripSeparator9,
            this.btnGazeCutPath,
            this.btnGazeBlinks});
      this.toolStripGaze.Location = new System.Drawing.Point(207, 13);
      this.toolStripGaze.Name = "toolStripGaze";
      this.toolStripGaze.Size = new System.Drawing.Size(170, 25);
      this.toolStripGaze.TabIndex = 19;
      // 
      // btnGazeModeCursor
      // 
      this.btnGazeModeCursor.Checked = global::Ogama.Properties.Settings.Default.GazeModeCursor;
      this.btnGazeModeCursor.CheckOnClick = true;
      this.btnGazeModeCursor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnGazeModeCursor.Image = global::Ogama.Properties.Resources.RPLCursor;
      this.btnGazeModeCursor.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnGazeModeCursor.Name = "btnGazeModeCursor";
      this.btnGazeModeCursor.Size = new System.Drawing.Size(23, 22);
      this.btnGazeModeCursor.ToolTipText = "Display Gaze Cursor";
      this.btnGazeModeCursor.Click += new System.EventHandler(this.btnGazeModeCursor_Click);
      // 
      // btnGazeModePath
      // 
      this.btnGazeModePath.Checked = global::Ogama.Properties.Settings.Default.GazeModePath;
      this.btnGazeModePath.CheckOnClick = true;
      this.btnGazeModePath.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnGazeModePath.Image = global::Ogama.Properties.Resources.RPLPath;
      this.btnGazeModePath.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnGazeModePath.Name = "btnGazeModePath";
      this.btnGazeModePath.Size = new System.Drawing.Size(23, 22);
      this.btnGazeModePath.ToolTipText = "Display Gaze Path";
      this.btnGazeModePath.Click += new System.EventHandler(this.btnGazeModePath_Click);
      // 
      // btnGazeModeFix
      // 
      this.btnGazeModeFix.Checked = global::Ogama.Properties.Settings.Default.GazeModeFixations;
      this.btnGazeModeFix.CheckOnClick = true;
      this.btnGazeModeFix.CheckState = System.Windows.Forms.CheckState.Checked;
      this.btnGazeModeFix.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnGazeModeFix.Image = global::Ogama.Properties.Resources.RPLFix;
      this.btnGazeModeFix.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnGazeModeFix.Name = "btnGazeModeFix";
      this.btnGazeModeFix.Size = new System.Drawing.Size(23, 22);
      this.btnGazeModeFix.ToolTipText = "Display Gaze Fixations";
      this.btnGazeModeFix.Click += new System.EventHandler(this.btnGazeModeFix_Click);
      // 
      // btnGazeModeFixCon
      // 
      this.btnGazeModeFixCon.Checked = global::Ogama.Properties.Settings.Default.GazeModeFixCon;
      this.btnGazeModeFixCon.CheckOnClick = true;
      this.btnGazeModeFixCon.CheckState = System.Windows.Forms.CheckState.Checked;
      this.btnGazeModeFixCon.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnGazeModeFixCon.Image = global::Ogama.Properties.Resources.RPLFixCon;
      this.btnGazeModeFixCon.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnGazeModeFixCon.Name = "btnGazeModeFixCon";
      this.btnGazeModeFixCon.Size = new System.Drawing.Size(23, 22);
      this.btnGazeModeFixCon.ToolTipText = "Display Gaze Fixation Connections";
      this.btnGazeModeFixCon.Click += new System.EventHandler(this.btnGazeModeFixCon_Click);
      // 
      // btnGazeModeSpot
      // 
      this.btnGazeModeSpot.Checked = global::Ogama.Properties.Settings.Default.GazeModeSpotlight;
      this.btnGazeModeSpot.CheckOnClick = true;
      this.btnGazeModeSpot.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnGazeModeSpot.Image = global::Ogama.Properties.Resources.RPLSpot;
      this.btnGazeModeSpot.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnGazeModeSpot.Name = "btnGazeModeSpot";
      this.btnGazeModeSpot.Size = new System.Drawing.Size(23, 22);
      this.btnGazeModeSpot.ToolTipText = "Display Gaze as Spotlight";
      this.btnGazeModeSpot.Click += new System.EventHandler(this.btnGazeModeSpot_Click);
      // 
      // toolStripSeparator9
      // 
      this.toolStripSeparator9.Name = "toolStripSeparator9";
      this.toolStripSeparator9.Size = new System.Drawing.Size(6, 25);
      // 
      // btnGazeCutPath
      // 
      this.btnGazeCutPath.Checked = global::Ogama.Properties.Settings.Default.GazeModeCutPath;
      this.btnGazeCutPath.CheckOnClick = true;
      this.btnGazeCutPath.CheckState = System.Windows.Forms.CheckState.Checked;
      this.btnGazeCutPath.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnGazeCutPath.Image = global::Ogama.Properties.Resources.RPLCut;
      this.btnGazeCutPath.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnGazeCutPath.Name = "btnGazeCutPath";
      this.btnGazeCutPath.Size = new System.Drawing.Size(23, 22);
      this.btnGazeCutPath.ToolTipText = "Reduce the amount of displayed gaze data.";
      this.btnGazeCutPath.Click += new System.EventHandler(this.btnGazeCutPath_Click);
      // 
      // btnGazeBlinks
      // 
      this.btnGazeBlinks.Checked = global::Ogama.Properties.Settings.Default.GazeModeBlinks;
      this.btnGazeBlinks.CheckOnClick = true;
      this.btnGazeBlinks.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnGazeBlinks.Image = global::Ogama.Properties.Resources.RPLBlink;
      this.btnGazeBlinks.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnGazeBlinks.Name = "btnGazeBlinks";
      this.btnGazeBlinks.Size = new System.Drawing.Size(23, 22);
      this.btnGazeBlinks.ToolTipText = "Show blinks while playing";
      this.btnGazeBlinks.Click += new System.EventHandler(this.btnGazeBlinks_Click);
      // 
      // pictureBox4
      // 
      this.pictureBox4.Image = global::Ogama.Properties.Resources.FixationsLogo;
      this.pictureBox4.Location = new System.Drawing.Point(26, 32);
      this.pictureBox4.Name = "pictureBox4";
      this.pictureBox4.Size = new System.Drawing.Size(42, 36);
      this.pictureBox4.TabIndex = 17;
      this.pictureBox4.TabStop = false;
      // 
      // pictureBox3
      // 
      this.pictureBox3.Image = global::Ogama.Properties.Resources.ReplayLogo;
      this.pictureBox3.Location = new System.Drawing.Point(3, 3);
      this.pictureBox3.Name = "pictureBox3";
      this.pictureBox3.Size = new System.Drawing.Size(33, 38);
      this.pictureBox3.TabIndex = 17;
      this.pictureBox3.TabStop = false;
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.label36);
      this.groupBox1.Controls.Add(this.nudGazeCursorSize);
      this.groupBox1.Controls.Add(this.btnGazeFixConStyle);
      this.groupBox1.Controls.Add(this.btnGazeFixStyle);
      this.groupBox1.Controls.Add(this.btnGazePathStyle);
      this.groupBox1.Controls.Add(this.btnGazeCursorStyle);
      this.groupBox1.Controls.Add(this.psaGazeFixCon);
      this.groupBox1.Controls.Add(this.psaGazeFix);
      this.groupBox1.Controls.Add(this.psaGazePath);
      this.groupBox1.Controls.Add(this.psaGazeCursor);
      this.groupBox1.Controls.Add(this.cbbGazeCursorType);
      this.groupBox1.Controls.Add(this.label25);
      this.groupBox1.Controls.Add(this.label23);
      this.groupBox1.Controls.Add(this.label26);
      this.groupBox1.Controls.Add(this.label27);
      this.groupBox1.Controls.Add(this.label28);
      this.groupBox1.Location = new System.Drawing.Point(85, 59);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(303, 131);
      this.groupBox1.TabIndex = 5;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Gaze styles";
      // 
      // label36
      // 
      this.label36.AutoSize = true;
      this.label36.Location = new System.Drawing.Point(217, 21);
      this.label36.Name = "label36";
      this.label36.Size = new System.Drawing.Size(27, 13);
      this.label36.TabIndex = 8;
      this.label36.Text = "Size";
      // 
      // nudGazeCursorSize
      // 
      this.nudGazeCursorSize.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::Ogama.Properties.Settings.Default, "GazeCursorSize", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.nudGazeCursorSize.Location = new System.Drawing.Point(252, 17);
      this.nudGazeCursorSize.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
      this.nudGazeCursorSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nudGazeCursorSize.Name = "nudGazeCursorSize";
      this.nudGazeCursorSize.Size = new System.Drawing.Size(39, 20);
      this.nudGazeCursorSize.TabIndex = 7;
      this.nudGazeCursorSize.Value = global::Ogama.Properties.Settings.Default.GazeCursorSize;
      // 
      // btnGazeFixConStyle
      // 
      this.btnGazeFixConStyle.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
      this.btnGazeFixConStyle.Image = global::Ogama.Properties.Resources.RPLFixConPen;
      this.btnGazeFixConStyle.Location = new System.Drawing.Point(271, 106);
      this.btnGazeFixConStyle.Margin = new System.Windows.Forms.Padding(0);
      this.btnGazeFixConStyle.Name = "btnGazeFixConStyle";
      this.btnGazeFixConStyle.Size = new System.Drawing.Size(20, 20);
      this.btnGazeFixConStyle.TabIndex = 6;
      this.btnGazeFixConStyle.UseVisualStyleBackColor = true;
      this.btnGazeFixConStyle.Click += new System.EventHandler(this.btnGazeFixConStyle_Click);
      // 
      // btnGazeFixStyle
      // 
      this.btnGazeFixStyle.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
      this.btnGazeFixStyle.Image = global::Ogama.Properties.Resources.RPLFixPen;
      this.btnGazeFixStyle.Location = new System.Drawing.Point(271, 84);
      this.btnGazeFixStyle.Margin = new System.Windows.Forms.Padding(0);
      this.btnGazeFixStyle.Name = "btnGazeFixStyle";
      this.btnGazeFixStyle.Size = new System.Drawing.Size(20, 20);
      this.btnGazeFixStyle.TabIndex = 6;
      this.btnGazeFixStyle.UseVisualStyleBackColor = true;
      this.btnGazeFixStyle.Click += new System.EventHandler(this.btnGazeFixStyle_Click);
      // 
      // btnGazePathStyle
      // 
      this.btnGazePathStyle.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
      this.btnGazePathStyle.Image = global::Ogama.Properties.Resources.RPLPathPen;
      this.btnGazePathStyle.Location = new System.Drawing.Point(271, 62);
      this.btnGazePathStyle.Margin = new System.Windows.Forms.Padding(0);
      this.btnGazePathStyle.Name = "btnGazePathStyle";
      this.btnGazePathStyle.Size = new System.Drawing.Size(20, 20);
      this.btnGazePathStyle.TabIndex = 6;
      this.btnGazePathStyle.UseVisualStyleBackColor = true;
      this.btnGazePathStyle.Click += new System.EventHandler(this.btnGazePathStyle_Click);
      // 
      // btnGazeCursorStyle
      // 
      this.btnGazeCursorStyle.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
      this.btnGazeCursorStyle.Image = global::Ogama.Properties.Resources.RPLCursorPen;
      this.btnGazeCursorStyle.Location = new System.Drawing.Point(271, 40);
      this.btnGazeCursorStyle.Margin = new System.Windows.Forms.Padding(0);
      this.btnGazeCursorStyle.Name = "btnGazeCursorStyle";
      this.btnGazeCursorStyle.Size = new System.Drawing.Size(20, 20);
      this.btnGazeCursorStyle.TabIndex = 6;
      this.btnGazeCursorStyle.UseVisualStyleBackColor = true;
      this.btnGazeCursorStyle.Click += new System.EventHandler(this.btnGazeCursorStyle_Click);
      // 
      // psaGazeFixCon
      // 
      this.psaGazeFixCon.BackColor = System.Drawing.Color.White;
      this.psaGazeFixCon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.psaGazeFixCon.DataBindings.Add(new System.Windows.Forms.Binding("PenColor", global::Ogama.Properties.Settings.Default, "GazeFixationConnectionsPenColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.psaGazeFixCon.DataBindings.Add(new System.Windows.Forms.Binding("PenDashStyle", global::Ogama.Properties.Settings.Default, "GazeFixationConnectionsPenStyle", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.psaGazeFixCon.DataBindings.Add(new System.Windows.Forms.Binding("PenSize", global::Ogama.Properties.Settings.Default, "GazeFixationConnectionsPenWidth", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.psaGazeFixCon.Location = new System.Drawing.Point(133, 106);
      this.psaGazeFixCon.Name = "psaGazeFixCon";
      this.psaGazeFixCon.PenColor = global::Ogama.Properties.Settings.Default.GazeFixationConnectionsPenColor;
      this.psaGazeFixCon.PenDashStyle = global::Ogama.Properties.Settings.Default.GazeFixationConnectionsPenStyle;
      this.psaGazeFixCon.PenSize = global::Ogama.Properties.Settings.Default.GazeFixationConnectionsPenWidth;
      this.psaGazeFixCon.Size = new System.Drawing.Size(130, 20);
      this.psaGazeFixCon.TabIndex = 5;
      // 
      // psaGazeFix
      // 
      this.psaGazeFix.BackColor = System.Drawing.Color.White;
      this.psaGazeFix.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.psaGazeFix.DataBindings.Add(new System.Windows.Forms.Binding("PenColor", global::Ogama.Properties.Settings.Default, "GazeFixationsPenColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.psaGazeFix.DataBindings.Add(new System.Windows.Forms.Binding("PenDashStyle", global::Ogama.Properties.Settings.Default, "GazeFixationsPenStyle", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.psaGazeFix.DataBindings.Add(new System.Windows.Forms.Binding("PenSize", global::Ogama.Properties.Settings.Default, "GazeFixationsPenWidth", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.psaGazeFix.Location = new System.Drawing.Point(133, 84);
      this.psaGazeFix.Name = "psaGazeFix";
      this.psaGazeFix.PenColor = global::Ogama.Properties.Settings.Default.GazeFixationsPenColor;
      this.psaGazeFix.PenDashStyle = global::Ogama.Properties.Settings.Default.GazeFixationsPenStyle;
      this.psaGazeFix.PenSize = global::Ogama.Properties.Settings.Default.GazeFixationsPenWidth;
      this.psaGazeFix.Size = new System.Drawing.Size(130, 20);
      this.psaGazeFix.TabIndex = 5;
      // 
      // psaGazePath
      // 
      this.psaGazePath.BackColor = System.Drawing.Color.White;
      this.psaGazePath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.psaGazePath.DataBindings.Add(new System.Windows.Forms.Binding("PenDashStyle", global::Ogama.Properties.Settings.Default, "GazePathStyle", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.psaGazePath.DataBindings.Add(new System.Windows.Forms.Binding("PenColor", global::Ogama.Properties.Settings.Default, "GazePathColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.psaGazePath.DataBindings.Add(new System.Windows.Forms.Binding("PenSize", global::Ogama.Properties.Settings.Default, "GazePathWidth", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.psaGazePath.Location = new System.Drawing.Point(133, 62);
      this.psaGazePath.Name = "psaGazePath";
      this.psaGazePath.PenColor = global::Ogama.Properties.Settings.Default.GazePathColor;
      this.psaGazePath.PenDashStyle = global::Ogama.Properties.Settings.Default.GazePathStyle;
      this.psaGazePath.PenSize = global::Ogama.Properties.Settings.Default.GazePathWidth;
      this.psaGazePath.Size = new System.Drawing.Size(130, 20);
      this.psaGazePath.TabIndex = 5;
      // 
      // psaGazeCursor
      // 
      this.psaGazeCursor.BackColor = System.Drawing.Color.White;
      this.psaGazeCursor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.psaGazeCursor.DataBindings.Add(new System.Windows.Forms.Binding("PenColor", global::Ogama.Properties.Settings.Default, "GazeCursorColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.psaGazeCursor.DataBindings.Add(new System.Windows.Forms.Binding("PenDashStyle", global::Ogama.Properties.Settings.Default, "GazeCursorStyle", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.psaGazeCursor.DataBindings.Add(new System.Windows.Forms.Binding("PenSize", global::Ogama.Properties.Settings.Default, "GazeCursorWidth", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.psaGazeCursor.Location = new System.Drawing.Point(133, 40);
      this.psaGazeCursor.Name = "psaGazeCursor";
      this.psaGazeCursor.PenColor = global::Ogama.Properties.Settings.Default.GazeCursorColor;
      this.psaGazeCursor.PenDashStyle = global::Ogama.Properties.Settings.Default.GazeCursorStyle;
      this.psaGazeCursor.PenSize = global::Ogama.Properties.Settings.Default.GazeCursorWidth;
      this.psaGazeCursor.Size = new System.Drawing.Size(130, 20);
      this.psaGazeCursor.TabIndex = 5;
      // 
      // cbbGazeCursorType
      // 
      this.cbbGazeCursorType.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Ogama.Properties.Settings.Default, "GazeCursorType", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.cbbGazeCursorType.FormattingEnabled = true;
      this.cbbGazeCursorType.Location = new System.Drawing.Point(133, 17);
      this.cbbGazeCursorType.Name = "cbbGazeCursorType";
      this.cbbGazeCursorType.Size = new System.Drawing.Size(78, 21);
      this.cbbGazeCursorType.TabIndex = 4;
      this.cbbGazeCursorType.Text = global::Ogama.Properties.Settings.Default.GazeCursorType;
      // 
      // label25
      // 
      this.label25.AutoSize = true;
      this.label25.Location = new System.Drawing.Point(12, 43);
      this.label25.Name = "label25";
      this.label25.Size = new System.Drawing.Size(58, 13);
      this.label25.TabIndex = 3;
      this.label25.Text = "Cursor pen";
      // 
      // label23
      // 
      this.label23.AutoSize = true;
      this.label23.Location = new System.Drawing.Point(12, 22);
      this.label23.Name = "label23";
      this.label23.Size = new System.Drawing.Size(60, 13);
      this.label23.TabIndex = 3;
      this.label23.Text = "Cursor type";
      // 
      // label26
      // 
      this.label26.AutoSize = true;
      this.label26.Location = new System.Drawing.Point(12, 64);
      this.label26.Name = "label26";
      this.label26.Size = new System.Drawing.Size(50, 13);
      this.label26.TabIndex = 3;
      this.label26.Text = "Path pen";
      // 
      // label27
      // 
      this.label27.AutoSize = true;
      this.label27.Location = new System.Drawing.Point(12, 85);
      this.label27.Name = "label27";
      this.label27.Size = new System.Drawing.Size(69, 13);
      this.label27.TabIndex = 3;
      this.label27.Text = "Fixations pen";
      // 
      // label28
      // 
      this.label28.AutoSize = true;
      this.label28.Location = new System.Drawing.Point(12, 106);
      this.label28.Name = "label28";
      this.label28.Size = new System.Drawing.Size(87, 13);
      this.label28.TabIndex = 3;
      this.label28.Text = "Connections pen";
      // 
      // label20
      // 
      this.label20.AutoSize = true;
      this.label20.Location = new System.Drawing.Point(82, 16);
      this.label20.Name = "label20";
      this.label20.Size = new System.Drawing.Size(104, 13);
      this.label20.TabIndex = 1;
      this.label20.Text = "Gaze drawing mode:";
      // 
      // toolStrip4
      // 
      this.toolStrip4.Dock = System.Windows.Forms.DockStyle.None;
      this.toolStrip4.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
      this.toolStrip4.Location = new System.Drawing.Point(220, 13);
      this.toolStrip4.Name = "toolStrip4";
      this.toolStrip4.Size = new System.Drawing.Size(1, 0);
      this.toolStrip4.TabIndex = 11;
      this.toolStrip4.Text = "toolStrip4";
      // 
      // toolStrip2
      // 
      this.toolStrip2.Dock = System.Windows.Forms.DockStyle.None;
      this.toolStrip2.Location = new System.Drawing.Point(131, 114);
      this.toolStrip2.Name = "toolStrip2";
      this.toolStrip2.Size = new System.Drawing.Size(111, 25);
      this.toolStrip2.TabIndex = 10;
      this.toolStrip2.Text = "toolStrip2";
      this.toolStrip2.Visible = false;
      // 
      // toolStrip3
      // 
      this.toolStrip3.Dock = System.Windows.Forms.DockStyle.None;
      this.toolStrip3.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
      this.toolStrip3.Location = new System.Drawing.Point(130, 141);
      this.toolStrip3.Name = "toolStrip3";
      this.toolStrip3.Size = new System.Drawing.Size(1, 0);
      this.toolStrip3.TabIndex = 6;
      this.toolStrip3.Visible = false;
      // 
      // toolStrip1
      // 
      this.toolStrip1.CanOverflow = false;
      this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
      this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
      this.toolStrip1.Location = new System.Drawing.Point(130, 109);
      this.toolStrip1.Name = "toolStrip1";
      this.toolStrip1.Size = new System.Drawing.Size(1, 0);
      this.toolStrip1.TabIndex = 5;
      this.toolStrip1.Text = "toolStrip1";
      this.toolStrip1.Visible = false;
      // 
      // tpbReplayMouse
      // 
      this.tpbReplayMouse.Controls.Add(this.toolStripMouse);
      this.tpbReplayMouse.Controls.Add(this.groupBox2);
      this.tpbReplayMouse.Controls.Add(this.pictureBox5);
      this.tpbReplayMouse.Controls.Add(this.pictureBox6);
      this.tpbReplayMouse.Controls.Add(this.label21);
      this.tpbReplayMouse.Controls.Add(this.toolStrip5);
      this.tpbReplayMouse.ImageIndex = 0;
      this.tpbReplayMouse.Location = new System.Drawing.Point(4, 42);
      this.tpbReplayMouse.Name = "tpbReplayMouse";
      this.tpbReplayMouse.Size = new System.Drawing.Size(487, 231);
      this.tpbReplayMouse.TabIndex = 5;
      this.tpbReplayMouse.Text = "Replay Mouse";
      this.tpbReplayMouse.UseVisualStyleBackColor = true;
      // 
      // toolStripMouse
      // 
      this.toolStripMouse.Dock = System.Windows.Forms.DockStyle.None;
      this.toolStripMouse.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
      this.toolStripMouse.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnMouseModeCursor,
            this.btnMouseModePath,
            this.btnMouseModeFix,
            this.btnMouseModeFixCon,
            this.btnMouseModeSpot,
            this.toolStripSeparator3,
            this.btnMouseCutPath});
      this.toolStripMouse.Location = new System.Drawing.Point(230, 12);
      this.toolStripMouse.Name = "toolStripMouse";
      this.toolStripMouse.Size = new System.Drawing.Size(147, 25);
      this.toolStripMouse.TabIndex = 22;
      // 
      // btnMouseModeCursor
      // 
      this.btnMouseModeCursor.Checked = global::Ogama.Properties.Settings.Default.MouseModeCursor;
      this.btnMouseModeCursor.CheckOnClick = true;
      this.btnMouseModeCursor.CheckState = System.Windows.Forms.CheckState.Checked;
      this.btnMouseModeCursor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnMouseModeCursor.Image = global::Ogama.Properties.Resources.RPLCursor;
      this.btnMouseModeCursor.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnMouseModeCursor.Name = "btnMouseModeCursor";
      this.btnMouseModeCursor.Size = new System.Drawing.Size(23, 22);
      this.btnMouseModeCursor.ToolTipText = "Display Mouse Cursor";
      this.btnMouseModeCursor.Click += new System.EventHandler(this.btnMouseModeCursor_Click);
      // 
      // btnMouseModePath
      // 
      this.btnMouseModePath.Checked = global::Ogama.Properties.Settings.Default.MouseModePath;
      this.btnMouseModePath.CheckOnClick = true;
      this.btnMouseModePath.CheckState = System.Windows.Forms.CheckState.Checked;
      this.btnMouseModePath.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnMouseModePath.Image = global::Ogama.Properties.Resources.RPLPath;
      this.btnMouseModePath.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnMouseModePath.Name = "btnMouseModePath";
      this.btnMouseModePath.Size = new System.Drawing.Size(23, 22);
      this.btnMouseModePath.ToolTipText = "Display Mouse Path";
      this.btnMouseModePath.Click += new System.EventHandler(this.btnMouseModePath_Click);
      // 
      // btnMouseModeFix
      // 
      this.btnMouseModeFix.Checked = global::Ogama.Properties.Settings.Default.MouseModeFixations;
      this.btnMouseModeFix.CheckOnClick = true;
      this.btnMouseModeFix.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnMouseModeFix.Image = global::Ogama.Properties.Resources.RPLFix;
      this.btnMouseModeFix.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnMouseModeFix.Name = "btnMouseModeFix";
      this.btnMouseModeFix.Size = new System.Drawing.Size(23, 22);
      this.btnMouseModeFix.ToolTipText = "Display Mouse Fixations";
      this.btnMouseModeFix.Click += new System.EventHandler(this.btnMouseModeFix_Click);
      // 
      // btnMouseModeFixCon
      // 
      this.btnMouseModeFixCon.Checked = global::Ogama.Properties.Settings.Default.MouseModeFixCon;
      this.btnMouseModeFixCon.CheckOnClick = true;
      this.btnMouseModeFixCon.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnMouseModeFixCon.Image = global::Ogama.Properties.Resources.RPLFixCon;
      this.btnMouseModeFixCon.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnMouseModeFixCon.Name = "btnMouseModeFixCon";
      this.btnMouseModeFixCon.Size = new System.Drawing.Size(23, 22);
      this.btnMouseModeFixCon.ToolTipText = "Display Mouse Fixation Connections";
      this.btnMouseModeFixCon.Click += new System.EventHandler(this.btnMouseModeFixCon_Click);
      // 
      // btnMouseModeSpot
      // 
      this.btnMouseModeSpot.Checked = global::Ogama.Properties.Settings.Default.MouseModeSpotlight;
      this.btnMouseModeSpot.CheckOnClick = true;
      this.btnMouseModeSpot.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnMouseModeSpot.Image = global::Ogama.Properties.Resources.RPLSpot;
      this.btnMouseModeSpot.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnMouseModeSpot.Name = "btnMouseModeSpot";
      this.btnMouseModeSpot.Size = new System.Drawing.Size(23, 22);
      this.btnMouseModeSpot.ToolTipText = "Display Mouse as Spotlight";
      this.btnMouseModeSpot.Click += new System.EventHandler(this.btnMouseModeSpot_Click);
      // 
      // toolStripSeparator3
      // 
      this.toolStripSeparator3.Name = "toolStripSeparator3";
      this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
      // 
      // btnMouseCutPath
      // 
      this.btnMouseCutPath.Checked = global::Ogama.Properties.Settings.Default.MouseModeCutPath;
      this.btnMouseCutPath.CheckOnClick = true;
      this.btnMouseCutPath.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnMouseCutPath.Image = global::Ogama.Properties.Resources.RPLCut;
      this.btnMouseCutPath.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnMouseCutPath.Name = "btnMouseCutPath";
      this.btnMouseCutPath.Size = new System.Drawing.Size(23, 22);
      this.btnMouseCutPath.ToolTipText = "Reduce the amount of displayed mouse data.";
      this.btnMouseCutPath.Click += new System.EventHandler(this.btnMouseCutPath_Click);
      // 
      // groupBox2
      // 
      this.groupBox2.Controls.Add(this.label37);
      this.groupBox2.Controls.Add(this.nudMouseCursorSize);
      this.groupBox2.Controls.Add(this.btnMouseFixConStyle);
      this.groupBox2.Controls.Add(this.btnMouseFixStyle);
      this.groupBox2.Controls.Add(this.btnMousePathStyle);
      this.groupBox2.Controls.Add(this.btnMouseCursorStyle);
      this.groupBox2.Controls.Add(this.psaMouseFixCon);
      this.groupBox2.Controls.Add(this.psaMouseFix);
      this.groupBox2.Controls.Add(this.psaMousePath);
      this.groupBox2.Controls.Add(this.psaMouseCursor);
      this.groupBox2.Controls.Add(this.cbbMouseCursorType);
      this.groupBox2.Controls.Add(this.label30);
      this.groupBox2.Controls.Add(this.label31);
      this.groupBox2.Controls.Add(this.label32);
      this.groupBox2.Controls.Add(this.label33);
      this.groupBox2.Controls.Add(this.label34);
      this.groupBox2.Location = new System.Drawing.Point(85, 59);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new System.Drawing.Size(303, 131);
      this.groupBox2.TabIndex = 20;
      this.groupBox2.TabStop = false;
      this.groupBox2.Text = "Mouse styles";
      // 
      // label37
      // 
      this.label37.AutoSize = true;
      this.label37.Location = new System.Drawing.Point(217, 22);
      this.label37.Name = "label37";
      this.label37.Size = new System.Drawing.Size(27, 13);
      this.label37.TabIndex = 10;
      this.label37.Text = "Size";
      // 
      // nudMouseCursorSize
      // 
      this.nudMouseCursorSize.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::Ogama.Properties.Settings.Default, "MouseCursorSize", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.nudMouseCursorSize.Location = new System.Drawing.Point(252, 18);
      this.nudMouseCursorSize.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
      this.nudMouseCursorSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nudMouseCursorSize.Name = "nudMouseCursorSize";
      this.nudMouseCursorSize.Size = new System.Drawing.Size(39, 20);
      this.nudMouseCursorSize.TabIndex = 9;
      this.nudMouseCursorSize.Value = global::Ogama.Properties.Settings.Default.MouseCursorSize;
      // 
      // btnMouseFixConStyle
      // 
      this.btnMouseFixConStyle.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
      this.btnMouseFixConStyle.Image = global::Ogama.Properties.Resources.RPLFixConPen;
      this.btnMouseFixConStyle.Location = new System.Drawing.Point(271, 106);
      this.btnMouseFixConStyle.Margin = new System.Windows.Forms.Padding(0);
      this.btnMouseFixConStyle.Name = "btnMouseFixConStyle";
      this.btnMouseFixConStyle.Size = new System.Drawing.Size(20, 20);
      this.btnMouseFixConStyle.TabIndex = 6;
      this.btnMouseFixConStyle.UseVisualStyleBackColor = true;
      this.btnMouseFixConStyle.Click += new System.EventHandler(this.btnMouseFixConStyle_Click);
      // 
      // btnMouseFixStyle
      // 
      this.btnMouseFixStyle.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
      this.btnMouseFixStyle.Image = global::Ogama.Properties.Resources.RPLFixPen;
      this.btnMouseFixStyle.Location = new System.Drawing.Point(271, 84);
      this.btnMouseFixStyle.Margin = new System.Windows.Forms.Padding(0);
      this.btnMouseFixStyle.Name = "btnMouseFixStyle";
      this.btnMouseFixStyle.Size = new System.Drawing.Size(20, 20);
      this.btnMouseFixStyle.TabIndex = 6;
      this.btnMouseFixStyle.UseVisualStyleBackColor = true;
      this.btnMouseFixStyle.Click += new System.EventHandler(this.btnMouseFixStyle_Click);
      // 
      // btnMousePathStyle
      // 
      this.btnMousePathStyle.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
      this.btnMousePathStyle.Image = global::Ogama.Properties.Resources.RPLPathPen;
      this.btnMousePathStyle.Location = new System.Drawing.Point(271, 62);
      this.btnMousePathStyle.Margin = new System.Windows.Forms.Padding(0);
      this.btnMousePathStyle.Name = "btnMousePathStyle";
      this.btnMousePathStyle.Size = new System.Drawing.Size(20, 20);
      this.btnMousePathStyle.TabIndex = 6;
      this.btnMousePathStyle.UseVisualStyleBackColor = true;
      this.btnMousePathStyle.Click += new System.EventHandler(this.btnMousePathStyle_Click);
      // 
      // btnMouseCursorStyle
      // 
      this.btnMouseCursorStyle.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
      this.btnMouseCursorStyle.Image = global::Ogama.Properties.Resources.RPLCursorPen;
      this.btnMouseCursorStyle.Location = new System.Drawing.Point(271, 40);
      this.btnMouseCursorStyle.Margin = new System.Windows.Forms.Padding(0);
      this.btnMouseCursorStyle.Name = "btnMouseCursorStyle";
      this.btnMouseCursorStyle.Size = new System.Drawing.Size(20, 20);
      this.btnMouseCursorStyle.TabIndex = 6;
      this.btnMouseCursorStyle.UseVisualStyleBackColor = true;
      this.btnMouseCursorStyle.Click += new System.EventHandler(this.btnMouseCursorStyle_Click);
      // 
      // psaMouseFixCon
      // 
      this.psaMouseFixCon.BackColor = System.Drawing.Color.White;
      this.psaMouseFixCon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.psaMouseFixCon.DataBindings.Add(new System.Windows.Forms.Binding("PenColor", global::Ogama.Properties.Settings.Default, "MouseFixationConnectionsPenColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.psaMouseFixCon.DataBindings.Add(new System.Windows.Forms.Binding("PenDashStyle", global::Ogama.Properties.Settings.Default, "MouseFixationConnectionsPenStyle", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.psaMouseFixCon.DataBindings.Add(new System.Windows.Forms.Binding("PenSize", global::Ogama.Properties.Settings.Default, "MouseFixationConnectionsPenWidth", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.psaMouseFixCon.Location = new System.Drawing.Point(133, 106);
      this.psaMouseFixCon.Name = "psaMouseFixCon";
      this.psaMouseFixCon.PenColor = global::Ogama.Properties.Settings.Default.MouseFixationConnectionsPenColor;
      this.psaMouseFixCon.PenDashStyle = global::Ogama.Properties.Settings.Default.MouseFixationConnectionsPenStyle;
      this.psaMouseFixCon.PenSize = global::Ogama.Properties.Settings.Default.MouseFixationConnectionsPenWidth;
      this.psaMouseFixCon.Size = new System.Drawing.Size(130, 20);
      this.psaMouseFixCon.TabIndex = 5;
      // 
      // psaMouseFix
      // 
      this.psaMouseFix.BackColor = System.Drawing.Color.White;
      this.psaMouseFix.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.psaMouseFix.DataBindings.Add(new System.Windows.Forms.Binding("PenColor", global::Ogama.Properties.Settings.Default, "MouseFixationsPenColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.psaMouseFix.DataBindings.Add(new System.Windows.Forms.Binding("PenDashStyle", global::Ogama.Properties.Settings.Default, "MouseFixationsPenStyle", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.psaMouseFix.DataBindings.Add(new System.Windows.Forms.Binding("PenSize", global::Ogama.Properties.Settings.Default, "MouseFixationsPenWidth", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.psaMouseFix.Location = new System.Drawing.Point(133, 84);
      this.psaMouseFix.Name = "psaMouseFix";
      this.psaMouseFix.PenColor = global::Ogama.Properties.Settings.Default.MouseFixationsPenColor;
      this.psaMouseFix.PenDashStyle = global::Ogama.Properties.Settings.Default.MouseFixationsPenStyle;
      this.psaMouseFix.PenSize = global::Ogama.Properties.Settings.Default.MouseFixationsPenWidth;
      this.psaMouseFix.Size = new System.Drawing.Size(130, 20);
      this.psaMouseFix.TabIndex = 5;
      // 
      // psaMousePath
      // 
      this.psaMousePath.BackColor = System.Drawing.Color.White;
      this.psaMousePath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.psaMousePath.DataBindings.Add(new System.Windows.Forms.Binding("PenColor", global::Ogama.Properties.Settings.Default, "MousePathColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.psaMousePath.DataBindings.Add(new System.Windows.Forms.Binding("PenDashStyle", global::Ogama.Properties.Settings.Default, "MousePathStyle", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.psaMousePath.DataBindings.Add(new System.Windows.Forms.Binding("PenSize", global::Ogama.Properties.Settings.Default, "MousePathWidth", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.psaMousePath.Location = new System.Drawing.Point(133, 62);
      this.psaMousePath.Name = "psaMousePath";
      this.psaMousePath.PenColor = global::Ogama.Properties.Settings.Default.MousePathColor;
      this.psaMousePath.PenDashStyle = global::Ogama.Properties.Settings.Default.MousePathStyle;
      this.psaMousePath.PenSize = global::Ogama.Properties.Settings.Default.MousePathWidth;
      this.psaMousePath.Size = new System.Drawing.Size(130, 20);
      this.psaMousePath.TabIndex = 5;
      // 
      // psaMouseCursor
      // 
      this.psaMouseCursor.BackColor = System.Drawing.Color.White;
      this.psaMouseCursor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.psaMouseCursor.DataBindings.Add(new System.Windows.Forms.Binding("PenColor", global::Ogama.Properties.Settings.Default, "MouseCursorColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.psaMouseCursor.DataBindings.Add(new System.Windows.Forms.Binding("PenDashStyle", global::Ogama.Properties.Settings.Default, "MouseCursorStyle", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.psaMouseCursor.DataBindings.Add(new System.Windows.Forms.Binding("PenSize", global::Ogama.Properties.Settings.Default, "MouseCursorWidth", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.psaMouseCursor.Location = new System.Drawing.Point(133, 40);
      this.psaMouseCursor.Name = "psaMouseCursor";
      this.psaMouseCursor.PenColor = global::Ogama.Properties.Settings.Default.MouseCursorColor;
      this.psaMouseCursor.PenDashStyle = global::Ogama.Properties.Settings.Default.MouseCursorStyle;
      this.psaMouseCursor.PenSize = global::Ogama.Properties.Settings.Default.MouseCursorWidth;
      this.psaMouseCursor.Size = new System.Drawing.Size(130, 20);
      this.psaMouseCursor.TabIndex = 5;
      // 
      // cbbMouseCursorType
      // 
      this.cbbMouseCursorType.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Ogama.Properties.Settings.Default, "MouseCursorType", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.cbbMouseCursorType.FormattingEnabled = true;
      this.cbbMouseCursorType.Location = new System.Drawing.Point(133, 17);
      this.cbbMouseCursorType.Name = "cbbMouseCursorType";
      this.cbbMouseCursorType.Size = new System.Drawing.Size(75, 21);
      this.cbbMouseCursorType.TabIndex = 4;
      this.cbbMouseCursorType.Text = global::Ogama.Properties.Settings.Default.MouseCursorType;
      // 
      // label30
      // 
      this.label30.AutoSize = true;
      this.label30.Location = new System.Drawing.Point(12, 43);
      this.label30.Name = "label30";
      this.label30.Size = new System.Drawing.Size(58, 13);
      this.label30.TabIndex = 3;
      this.label30.Text = "Cursor pen";
      // 
      // label31
      // 
      this.label31.AutoSize = true;
      this.label31.Location = new System.Drawing.Point(12, 22);
      this.label31.Name = "label31";
      this.label31.Size = new System.Drawing.Size(60, 13);
      this.label31.TabIndex = 3;
      this.label31.Text = "Cursor type";
      // 
      // label32
      // 
      this.label32.AutoSize = true;
      this.label32.Location = new System.Drawing.Point(12, 64);
      this.label32.Name = "label32";
      this.label32.Size = new System.Drawing.Size(50, 13);
      this.label32.TabIndex = 3;
      this.label32.Text = "Path pen";
      // 
      // label33
      // 
      this.label33.AutoSize = true;
      this.label33.Location = new System.Drawing.Point(12, 85);
      this.label33.Name = "label33";
      this.label33.Size = new System.Drawing.Size(69, 13);
      this.label33.TabIndex = 3;
      this.label33.Text = "Fixations pen";
      // 
      // label34
      // 
      this.label34.AutoSize = true;
      this.label34.Location = new System.Drawing.Point(12, 106);
      this.label34.Name = "label34";
      this.label34.Size = new System.Drawing.Size(87, 13);
      this.label34.TabIndex = 3;
      this.label34.Text = "Connections pen";
      // 
      // pictureBox5
      // 
      this.pictureBox5.Image = global::Ogama.Properties.Resources.Mouse;
      this.pictureBox5.Location = new System.Drawing.Point(26, 32);
      this.pictureBox5.Name = "pictureBox5";
      this.pictureBox5.Size = new System.Drawing.Size(39, 37);
      this.pictureBox5.TabIndex = 19;
      this.pictureBox5.TabStop = false;
      // 
      // pictureBox6
      // 
      this.pictureBox6.Image = global::Ogama.Properties.Resources.ReplayLogo;
      this.pictureBox6.Location = new System.Drawing.Point(3, 3);
      this.pictureBox6.Name = "pictureBox6";
      this.pictureBox6.Size = new System.Drawing.Size(33, 38);
      this.pictureBox6.TabIndex = 18;
      this.pictureBox6.TabStop = false;
      // 
      // label21
      // 
      this.label21.AutoSize = true;
      this.label21.Location = new System.Drawing.Point(82, 16);
      this.label21.Name = "label21";
      this.label21.Size = new System.Drawing.Size(111, 13);
      this.label21.TabIndex = 14;
      this.label21.Text = "Mouse drawing mode:";
      // 
      // toolStrip5
      // 
      this.toolStrip5.Dock = System.Windows.Forms.DockStyle.None;
      this.toolStrip5.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
      this.toolStrip5.Location = new System.Drawing.Point(242, 10);
      this.toolStrip5.Name = "toolStrip5";
      this.toolStrip5.Size = new System.Drawing.Size(1, 0);
      this.toolStrip5.TabIndex = 16;
      this.toolStrip5.Visible = false;
      // 
      // tbpFixationsInterface
      // 
      this.tbpFixationsInterface.Controls.Add(this.grpMouseFixations);
      this.tbpFixationsInterface.Controls.Add(this.grbGazeFixations);
      this.tbpFixationsInterface.Controls.Add(this.pictureBox9);
      this.tbpFixationsInterface.ImageIndex = 3;
      this.tbpFixationsInterface.Location = new System.Drawing.Point(4, 42);
      this.tbpFixationsInterface.Name = "tbpFixationsInterface";
      this.tbpFixationsInterface.Padding = new System.Windows.Forms.Padding(3);
      this.tbpFixationsInterface.Size = new System.Drawing.Size(487, 231);
      this.tbpFixationsInterface.TabIndex = 4;
      this.tbpFixationsInterface.Text = "Fixations Module";
      this.tbpFixationsInterface.UseVisualStyleBackColor = true;
      // 
      // grpMouseFixations
      // 
      this.grpMouseFixations.Controls.Add(this.label3);
      this.grpMouseFixations.Controls.Add(this.label4);
      this.grpMouseFixations.Controls.Add(this.label5);
      this.grpMouseFixations.Controls.Add(this.cbbMouseFixationsDisplayMode);
      this.grpMouseFixations.Controls.Add(this.psaMouseFixationsPenStyle);
      this.grpMouseFixations.Controls.Add(this.fsaMouseFixationsFont);
      this.grpMouseFixations.Controls.Add(this.btnMouseFixationsPenStyle);
      this.grpMouseFixations.Controls.Add(this.btnMouseFixationsFontStyle);
      this.grpMouseFixations.Location = new System.Drawing.Point(86, 104);
      this.grpMouseFixations.Name = "grpMouseFixations";
      this.grpMouseFixations.Size = new System.Drawing.Size(286, 98);
      this.grpMouseFixations.TabIndex = 21;
      this.grpMouseFixations.TabStop = false;
      this.grpMouseFixations.Text = "Mouse fixation style";
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(6, 19);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(53, 13);
      this.label3.TabIndex = 3;
      this.label3.Text = "Pen style:";
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(6, 42);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(55, 13);
      this.label4.TabIndex = 3;
      this.label4.Text = "Font style:";
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(6, 67);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(73, 13);
      this.label5.TabIndex = 4;
      this.label5.Text = "Display mode:";
      // 
      // cbbMouseFixationsDisplayMode
      // 
      this.cbbMouseFixationsDisplayMode.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Ogama.Properties.Settings.Default, "MouseFixationsDrawingMode", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.cbbMouseFixationsDisplayMode.FormattingEnabled = true;
      this.cbbMouseFixationsDisplayMode.Location = new System.Drawing.Point(103, 65);
      this.cbbMouseFixationsDisplayMode.Name = "cbbMouseFixationsDisplayMode";
      this.cbbMouseFixationsDisplayMode.Size = new System.Drawing.Size(131, 21);
      this.cbbMouseFixationsDisplayMode.TabIndex = 17;
      this.cbbMouseFixationsDisplayMode.Text = global::Ogama.Properties.Settings.Default.MouseFixationsDrawingMode;
      // 
      // psaMouseFixationsPenStyle
      // 
      this.psaMouseFixationsPenStyle.BackColor = System.Drawing.Color.White;
      this.psaMouseFixationsPenStyle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.psaMouseFixationsPenStyle.DataBindings.Add(new System.Windows.Forms.Binding("PenColor", global::Ogama.Properties.Settings.Default, "MouseFixationsPenColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.psaMouseFixationsPenStyle.DataBindings.Add(new System.Windows.Forms.Binding("PenDashStyle", global::Ogama.Properties.Settings.Default, "MouseFixationsPenStyle", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.psaMouseFixationsPenStyle.DataBindings.Add(new System.Windows.Forms.Binding("PenSize", global::Ogama.Properties.Settings.Default, "MouseFixationsPenWidth", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.psaMouseFixationsPenStyle.Location = new System.Drawing.Point(104, 14);
      this.psaMouseFixationsPenStyle.Name = "psaMouseFixationsPenStyle";
      this.psaMouseFixationsPenStyle.PenColor = global::Ogama.Properties.Settings.Default.MouseFixationsPenColor;
      this.psaMouseFixationsPenStyle.PenDashStyle = global::Ogama.Properties.Settings.Default.MouseFixationsPenStyle;
      this.psaMouseFixationsPenStyle.PenSize = global::Ogama.Properties.Settings.Default.MouseFixationsPenWidth;
      this.psaMouseFixationsPenStyle.Size = new System.Drawing.Size(130, 20);
      this.psaMouseFixationsPenStyle.TabIndex = 13;
      // 
      // fsaMouseFixationsFont
      // 
      this.fsaMouseFixationsFont.BackColor = System.Drawing.Color.White;
      this.fsaMouseFixationsFont.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.fsaMouseFixationsFont.DataBindings.Add(new System.Windows.Forms.Binding("Font", global::Ogama.Properties.Settings.Default, "MouseFixationsFont", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.fsaMouseFixationsFont.DataBindings.Add(new System.Windows.Forms.Binding("FontColor", global::Ogama.Properties.Settings.Default, "MouseFixationsFontColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.fsaMouseFixationsFont.Font = global::Ogama.Properties.Settings.Default.MouseFixationsFont;
      this.fsaMouseFixationsFont.FontAlignment = VectorGraphics.Elements.VGAlignment.Center;
      this.fsaMouseFixationsFont.FontColor = global::Ogama.Properties.Settings.Default.MouseFixationsFontColor;
      this.fsaMouseFixationsFont.Location = new System.Drawing.Point(104, 39);
      this.fsaMouseFixationsFont.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
      this.fsaMouseFixationsFont.Name = "fsaMouseFixationsFont";
      this.fsaMouseFixationsFont.SampleString = "Sample ...";
      this.fsaMouseFixationsFont.Size = new System.Drawing.Size(130, 20);
      this.fsaMouseFixationsFont.TabIndex = 16;
      // 
      // btnMouseFixationsPenStyle
      // 
      this.btnMouseFixationsPenStyle.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
      this.btnMouseFixationsPenStyle.Image = global::Ogama.Properties.Resources.RPLFixConPen;
      this.btnMouseFixationsPenStyle.Location = new System.Drawing.Point(242, 14);
      this.btnMouseFixationsPenStyle.Margin = new System.Windows.Forms.Padding(0);
      this.btnMouseFixationsPenStyle.Name = "btnMouseFixationsPenStyle";
      this.btnMouseFixationsPenStyle.Size = new System.Drawing.Size(20, 20);
      this.btnMouseFixationsPenStyle.TabIndex = 14;
      this.btnMouseFixationsPenStyle.UseVisualStyleBackColor = true;
      this.btnMouseFixationsPenStyle.Click += new System.EventHandler(this.btnGazeFixationsPenStyle_Click);
      // 
      // btnMouseFixationsFontStyle
      // 
      this.btnMouseFixationsFontStyle.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
      this.btnMouseFixationsFontStyle.Image = global::Ogama.Properties.Resources.Color_fontHS;
      this.btnMouseFixationsFontStyle.Location = new System.Drawing.Point(242, 39);
      this.btnMouseFixationsFontStyle.Margin = new System.Windows.Forms.Padding(0);
      this.btnMouseFixationsFontStyle.Name = "btnMouseFixationsFontStyle";
      this.btnMouseFixationsFontStyle.Size = new System.Drawing.Size(20, 20);
      this.btnMouseFixationsFontStyle.TabIndex = 15;
      this.btnMouseFixationsFontStyle.UseVisualStyleBackColor = true;
      this.btnMouseFixationsFontStyle.Click += new System.EventHandler(this.btnGazeFixationsFontStyle_Click);
      // 
      // grbGazeFixations
      // 
      this.grbGazeFixations.Controls.Add(this.label11);
      this.grbGazeFixations.Controls.Add(this.label12);
      this.grbGazeFixations.Controls.Add(this.label13);
      this.grbGazeFixations.Controls.Add(this.cbbGazeFixationsDisplayMode);
      this.grbGazeFixations.Controls.Add(this.psaGazeFixationsPenStyle);
      this.grbGazeFixations.Controls.Add(this.fsaGazeFixationsFont);
      this.grbGazeFixations.Controls.Add(this.btnGazeFixationsPenStyle);
      this.grbGazeFixations.Controls.Add(this.btnGazeFixationsFontStyle);
      this.grbGazeFixations.Location = new System.Drawing.Point(86, 6);
      this.grbGazeFixations.Name = "grbGazeFixations";
      this.grbGazeFixations.Size = new System.Drawing.Size(286, 92);
      this.grbGazeFixations.TabIndex = 21;
      this.grbGazeFixations.TabStop = false;
      this.grbGazeFixations.Text = "Gaze fixation style";
      // 
      // label11
      // 
      this.label11.AutoSize = true;
      this.label11.Location = new System.Drawing.Point(6, 19);
      this.label11.Name = "label11";
      this.label11.Size = new System.Drawing.Size(53, 13);
      this.label11.TabIndex = 3;
      this.label11.Text = "Pen style:";
      // 
      // label12
      // 
      this.label12.AutoSize = true;
      this.label12.Location = new System.Drawing.Point(6, 42);
      this.label12.Name = "label12";
      this.label12.Size = new System.Drawing.Size(55, 13);
      this.label12.TabIndex = 3;
      this.label12.Text = "Font style:";
      // 
      // label13
      // 
      this.label13.AutoSize = true;
      this.label13.Location = new System.Drawing.Point(6, 67);
      this.label13.Name = "label13";
      this.label13.Size = new System.Drawing.Size(73, 13);
      this.label13.TabIndex = 4;
      this.label13.Text = "Display mode:";
      // 
      // cbbGazeFixationsDisplayMode
      // 
      this.cbbGazeFixationsDisplayMode.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Ogama.Properties.Settings.Default, "GazeFixationsDrawingMode", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.cbbGazeFixationsDisplayMode.FormattingEnabled = true;
      this.cbbGazeFixationsDisplayMode.Location = new System.Drawing.Point(103, 65);
      this.cbbGazeFixationsDisplayMode.Name = "cbbGazeFixationsDisplayMode";
      this.cbbGazeFixationsDisplayMode.Size = new System.Drawing.Size(131, 21);
      this.cbbGazeFixationsDisplayMode.TabIndex = 17;
      this.cbbGazeFixationsDisplayMode.Text = global::Ogama.Properties.Settings.Default.GazeFixationsDrawingMode;
      // 
      // psaGazeFixationsPenStyle
      // 
      this.psaGazeFixationsPenStyle.BackColor = System.Drawing.Color.White;
      this.psaGazeFixationsPenStyle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.psaGazeFixationsPenStyle.DataBindings.Add(new System.Windows.Forms.Binding("PenColor", global::Ogama.Properties.Settings.Default, "GazeFixationsPenColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.psaGazeFixationsPenStyle.DataBindings.Add(new System.Windows.Forms.Binding("PenDashStyle", global::Ogama.Properties.Settings.Default, "GazeFixationsPenStyle", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.psaGazeFixationsPenStyle.DataBindings.Add(new System.Windows.Forms.Binding("PenSize", global::Ogama.Properties.Settings.Default, "GazeFixationsPenWidth", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.psaGazeFixationsPenStyle.Location = new System.Drawing.Point(104, 14);
      this.psaGazeFixationsPenStyle.Name = "psaGazeFixationsPenStyle";
      this.psaGazeFixationsPenStyle.PenColor = global::Ogama.Properties.Settings.Default.GazeFixationsPenColor;
      this.psaGazeFixationsPenStyle.PenDashStyle = global::Ogama.Properties.Settings.Default.GazeFixationsPenStyle;
      this.psaGazeFixationsPenStyle.PenSize = global::Ogama.Properties.Settings.Default.GazeFixationsPenWidth;
      this.psaGazeFixationsPenStyle.Size = new System.Drawing.Size(130, 20);
      this.psaGazeFixationsPenStyle.TabIndex = 13;
      // 
      // fsaGazeFixationsFont
      // 
      this.fsaGazeFixationsFont.BackColor = System.Drawing.Color.White;
      this.fsaGazeFixationsFont.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.fsaGazeFixationsFont.DataBindings.Add(new System.Windows.Forms.Binding("Font", global::Ogama.Properties.Settings.Default, "GazeFixationsFont", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.fsaGazeFixationsFont.DataBindings.Add(new System.Windows.Forms.Binding("FontColor", global::Ogama.Properties.Settings.Default, "GazeFixationsFontColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.fsaGazeFixationsFont.Font = global::Ogama.Properties.Settings.Default.GazeFixationsFont;
      this.fsaGazeFixationsFont.FontAlignment = VectorGraphics.Elements.VGAlignment.Center;
      this.fsaGazeFixationsFont.FontColor = global::Ogama.Properties.Settings.Default.GazeFixationsFontColor;
      this.fsaGazeFixationsFont.Location = new System.Drawing.Point(104, 39);
      this.fsaGazeFixationsFont.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
      this.fsaGazeFixationsFont.Name = "fsaGazeFixationsFont";
      this.fsaGazeFixationsFont.SampleString = "Sample ...";
      this.fsaGazeFixationsFont.Size = new System.Drawing.Size(130, 20);
      this.fsaGazeFixationsFont.TabIndex = 16;
      // 
      // btnGazeFixationsPenStyle
      // 
      this.btnGazeFixationsPenStyle.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
      this.btnGazeFixationsPenStyle.Image = global::Ogama.Properties.Resources.RPLFixConPen;
      this.btnGazeFixationsPenStyle.Location = new System.Drawing.Point(242, 14);
      this.btnGazeFixationsPenStyle.Margin = new System.Windows.Forms.Padding(0);
      this.btnGazeFixationsPenStyle.Name = "btnGazeFixationsPenStyle";
      this.btnGazeFixationsPenStyle.Size = new System.Drawing.Size(20, 20);
      this.btnGazeFixationsPenStyle.TabIndex = 14;
      this.btnGazeFixationsPenStyle.UseVisualStyleBackColor = true;
      this.btnGazeFixationsPenStyle.Click += new System.EventHandler(this.btnGazeFixationsPenStyle_Click);
      // 
      // btnGazeFixationsFontStyle
      // 
      this.btnGazeFixationsFontStyle.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
      this.btnGazeFixationsFontStyle.Image = global::Ogama.Properties.Resources.Color_fontHS;
      this.btnGazeFixationsFontStyle.Location = new System.Drawing.Point(242, 39);
      this.btnGazeFixationsFontStyle.Margin = new System.Windows.Forms.Padding(0);
      this.btnGazeFixationsFontStyle.Name = "btnGazeFixationsFontStyle";
      this.btnGazeFixationsFontStyle.Size = new System.Drawing.Size(20, 20);
      this.btnGazeFixationsFontStyle.TabIndex = 15;
      this.btnGazeFixationsFontStyle.UseVisualStyleBackColor = true;
      this.btnGazeFixationsFontStyle.Click += new System.EventHandler(this.btnGazeFixationsFontStyle_Click);
      // 
      // pictureBox9
      // 
      this.pictureBox9.Image = global::Ogama.Properties.Resources.FixationsLogo;
      this.pictureBox9.Location = new System.Drawing.Point(6, 6);
      this.pictureBox9.Name = "pictureBox9";
      this.pictureBox9.Size = new System.Drawing.Size(41, 32);
      this.pictureBox9.TabIndex = 20;
      this.pictureBox9.TabStop = false;
      // 
      // tpbAOI
      // 
      this.tpbAOI.Controls.Add(this.pictureBox8);
      this.tpbAOI.Controls.Add(this.btnAOISearchRectFont);
      this.tpbAOI.Controls.Add(this.btnAOITargetFont);
      this.tpbAOI.Controls.Add(this.btnAOIStandardFont);
      this.tpbAOI.Controls.Add(this.btnAOIStandardStyle);
      this.tpbAOI.Controls.Add(this.btnAOITargetStyle);
      this.tpbAOI.Controls.Add(this.btnAOISearchRectStyle);
      this.tpbAOI.Controls.Add(this.label19);
      this.tpbAOI.Controls.Add(this.label18);
      this.tpbAOI.Controls.Add(this.label17);
      this.tpbAOI.Controls.Add(this.label14);
      this.tpbAOI.Controls.Add(this.label15);
      this.tpbAOI.Controls.Add(this.label16);
      this.tpbAOI.Controls.Add(this.fsaAOISearchRect);
      this.tpbAOI.Controls.Add(this.fsaAOITarget);
      this.tpbAOI.Controls.Add(this.fsaAOIDefault);
      this.tpbAOI.Controls.Add(this.psaAOIStandard);
      this.tpbAOI.Controls.Add(this.psaAOITarget);
      this.tpbAOI.Controls.Add(this.psaAOISearchRect);
      this.tpbAOI.ImageIndex = 1;
      this.tpbAOI.Location = new System.Drawing.Point(4, 42);
      this.tpbAOI.Name = "tpbAOI";
      this.tpbAOI.Padding = new System.Windows.Forms.Padding(3);
      this.tpbAOI.Size = new System.Drawing.Size(487, 231);
      this.tpbAOI.TabIndex = 3;
      this.tpbAOI.Text = "AOI Module";
      this.tpbAOI.UseVisualStyleBackColor = true;
      // 
      // pictureBox8
      // 
      this.pictureBox8.Image = global::Ogama.Properties.Resources.AOILogo;
      this.pictureBox8.Location = new System.Drawing.Point(6, 6);
      this.pictureBox8.Name = "pictureBox8";
      this.pictureBox8.Size = new System.Drawing.Size(43, 44);
      this.pictureBox8.TabIndex = 14;
      this.pictureBox8.TabStop = false;
      // 
      // btnAOISearchRectFont
      // 
      this.btnAOISearchRectFont.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
      this.btnAOISearchRectFont.Image = global::Ogama.Properties.Resources.Color_fontHS_S;
      this.btnAOISearchRectFont.Location = new System.Drawing.Point(357, 171);
      this.btnAOISearchRectFont.Margin = new System.Windows.Forms.Padding(0);
      this.btnAOISearchRectFont.Name = "btnAOISearchRectFont";
      this.btnAOISearchRectFont.Size = new System.Drawing.Size(20, 20);
      this.btnAOISearchRectFont.TabIndex = 12;
      this.btnAOISearchRectFont.UseVisualStyleBackColor = true;
      this.btnAOISearchRectFont.Click += new System.EventHandler(this.btnAOISearchRectFont_Click);
      // 
      // btnAOITargetFont
      // 
      this.btnAOITargetFont.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
      this.btnAOITargetFont.Image = global::Ogama.Properties.Resources.Color_fontHS_T;
      this.btnAOITargetFont.Location = new System.Drawing.Point(357, 109);
      this.btnAOITargetFont.Margin = new System.Windows.Forms.Padding(0);
      this.btnAOITargetFont.Name = "btnAOITargetFont";
      this.btnAOITargetFont.Size = new System.Drawing.Size(20, 20);
      this.btnAOITargetFont.TabIndex = 12;
      this.btnAOITargetFont.UseVisualStyleBackColor = true;
      this.btnAOITargetFont.Click += new System.EventHandler(this.btnAOITargetFont_Click);
      // 
      // btnAOIStandardFont
      // 
      this.btnAOIStandardFont.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
      this.btnAOIStandardFont.Image = global::Ogama.Properties.Resources.Color_fontHS;
      this.btnAOIStandardFont.Location = new System.Drawing.Point(357, 47);
      this.btnAOIStandardFont.Margin = new System.Windows.Forms.Padding(0);
      this.btnAOIStandardFont.Name = "btnAOIStandardFont";
      this.btnAOIStandardFont.Size = new System.Drawing.Size(20, 20);
      this.btnAOIStandardFont.TabIndex = 12;
      this.btnAOIStandardFont.UseVisualStyleBackColor = true;
      this.btnAOIStandardFont.Click += new System.EventHandler(this.btnAOIDefaultFont_Click);
      // 
      // btnAOIStandardStyle
      // 
      this.btnAOIStandardStyle.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
      this.btnAOIStandardStyle.Image = global::Ogama.Properties.Resources.RPLFixConPen;
      this.btnAOIStandardStyle.Location = new System.Drawing.Point(357, 16);
      this.btnAOIStandardStyle.Margin = new System.Windows.Forms.Padding(0);
      this.btnAOIStandardStyle.Name = "btnAOIStandardStyle";
      this.btnAOIStandardStyle.Size = new System.Drawing.Size(20, 20);
      this.btnAOIStandardStyle.TabIndex = 12;
      this.btnAOIStandardStyle.UseVisualStyleBackColor = true;
      this.btnAOIStandardStyle.Click += new System.EventHandler(this.btnAOIStandardStyle_Click);
      // 
      // btnAOITargetStyle
      // 
      this.btnAOITargetStyle.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
      this.btnAOITargetStyle.Image = global::Ogama.Properties.Resources.RPLFixConPen;
      this.btnAOITargetStyle.Location = new System.Drawing.Point(357, 78);
      this.btnAOITargetStyle.Margin = new System.Windows.Forms.Padding(0);
      this.btnAOITargetStyle.Name = "btnAOITargetStyle";
      this.btnAOITargetStyle.Size = new System.Drawing.Size(20, 20);
      this.btnAOITargetStyle.TabIndex = 10;
      this.btnAOITargetStyle.UseVisualStyleBackColor = true;
      this.btnAOITargetStyle.Click += new System.EventHandler(this.btnAOITargetStyle_Click);
      // 
      // btnAOISearchRectStyle
      // 
      this.btnAOISearchRectStyle.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
      this.btnAOISearchRectStyle.Image = global::Ogama.Properties.Resources.RPLFixConPen;
      this.btnAOISearchRectStyle.Location = new System.Drawing.Point(357, 140);
      this.btnAOISearchRectStyle.Margin = new System.Windows.Forms.Padding(0);
      this.btnAOISearchRectStyle.Name = "btnAOISearchRectStyle";
      this.btnAOISearchRectStyle.Size = new System.Drawing.Size(20, 20);
      this.btnAOISearchRectStyle.TabIndex = 8;
      this.btnAOISearchRectStyle.UseVisualStyleBackColor = true;
      this.btnAOISearchRectStyle.Click += new System.EventHandler(this.btnAOISearchRectStyle_Click);
      // 
      // label19
      // 
      this.label19.AutoSize = true;
      this.label19.Location = new System.Drawing.Point(69, 175);
      this.label19.Name = "label19";
      this.label19.Size = new System.Drawing.Size(133, 13);
      this.label19.TabIndex = 6;
      this.label19.Text = "SearchRect AOI font style:";
      // 
      // label18
      // 
      this.label18.AutoSize = true;
      this.label18.Location = new System.Drawing.Point(69, 144);
      this.label18.Name = "label18";
      this.label18.Size = new System.Drawing.Size(133, 13);
      this.label18.TabIndex = 5;
      this.label18.Text = "SearchRect AOI pen style:";
      // 
      // label17
      // 
      this.label17.AutoSize = true;
      this.label17.Location = new System.Drawing.Point(69, 113);
      this.label17.Name = "label17";
      this.label17.Size = new System.Drawing.Size(107, 13);
      this.label17.TabIndex = 6;
      this.label17.Text = "Target AOI font style:";
      // 
      // label14
      // 
      this.label14.AutoSize = true;
      this.label14.Location = new System.Drawing.Point(69, 82);
      this.label14.Name = "label14";
      this.label14.Size = new System.Drawing.Size(107, 13);
      this.label14.TabIndex = 5;
      this.label14.Text = "Target AOI pen style:";
      // 
      // label15
      // 
      this.label15.AutoSize = true;
      this.label15.Location = new System.Drawing.Point(69, 51);
      this.label15.Name = "label15";
      this.label15.Size = new System.Drawing.Size(119, 13);
      this.label15.TabIndex = 6;
      this.label15.Text = "Standard AOI font style:";
      // 
      // label16
      // 
      this.label16.AutoSize = true;
      this.label16.Location = new System.Drawing.Point(69, 20);
      this.label16.Name = "label16";
      this.label16.Size = new System.Drawing.Size(119, 13);
      this.label16.TabIndex = 5;
      this.label16.Text = "Standard AOI pen style:";
      // 
      // fsaAOISearchRect
      // 
      this.fsaAOISearchRect.BackColor = System.Drawing.Color.White;
      this.fsaAOISearchRect.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.fsaAOISearchRect.DataBindings.Add(new System.Windows.Forms.Binding("FontColor", global::Ogama.Properties.Settings.Default, "AOISearchRectFontColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.fsaAOISearchRect.DataBindings.Add(new System.Windows.Forms.Binding("Font", global::Ogama.Properties.Settings.Default, "AOISearchRectFont", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.fsaAOISearchRect.Font = global::Ogama.Properties.Settings.Default.AOISearchRectFont;
      this.fsaAOISearchRect.FontAlignment = VectorGraphics.Elements.VGAlignment.Center;
      this.fsaAOISearchRect.FontColor = global::Ogama.Properties.Settings.Default.AOISearchRectFontColor;
      this.fsaAOISearchRect.Location = new System.Drawing.Point(219, 171);
      this.fsaAOISearchRect.Margin = new System.Windows.Forms.Padding(10, 10, 10, 10);
      this.fsaAOISearchRect.Name = "fsaAOISearchRect";
      this.fsaAOISearchRect.SampleString = "Sample ...";
      this.fsaAOISearchRect.Size = new System.Drawing.Size(130, 20);
      this.fsaAOISearchRect.TabIndex = 13;
      // 
      // fsaAOITarget
      // 
      this.fsaAOITarget.BackColor = System.Drawing.Color.White;
      this.fsaAOITarget.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.fsaAOITarget.DataBindings.Add(new System.Windows.Forms.Binding("FontColor", global::Ogama.Properties.Settings.Default, "AOITargetFontColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.fsaAOITarget.DataBindings.Add(new System.Windows.Forms.Binding("Font", global::Ogama.Properties.Settings.Default, "AOITargetFont", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.fsaAOITarget.Font = global::Ogama.Properties.Settings.Default.AOITargetFont;
      this.fsaAOITarget.FontAlignment = VectorGraphics.Elements.VGAlignment.Center;
      this.fsaAOITarget.FontColor = global::Ogama.Properties.Settings.Default.AOITargetFontColor;
      this.fsaAOITarget.Location = new System.Drawing.Point(219, 109);
      this.fsaAOITarget.Margin = new System.Windows.Forms.Padding(10, 10, 10, 10);
      this.fsaAOITarget.Name = "fsaAOITarget";
      this.fsaAOITarget.SampleString = "Sample ...";
      this.fsaAOITarget.Size = new System.Drawing.Size(130, 20);
      this.fsaAOITarget.TabIndex = 13;
      // 
      // fsaAOIDefault
      // 
      this.fsaAOIDefault.BackColor = System.Drawing.Color.White;
      this.fsaAOIDefault.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.fsaAOIDefault.DataBindings.Add(new System.Windows.Forms.Binding("Font", global::Ogama.Properties.Settings.Default, "AOIStandardFont", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.fsaAOIDefault.DataBindings.Add(new System.Windows.Forms.Binding("FontColor", global::Ogama.Properties.Settings.Default, "AOIStandardFontColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.fsaAOIDefault.Font = global::Ogama.Properties.Settings.Default.AOIStandardFont;
      this.fsaAOIDefault.FontAlignment = VectorGraphics.Elements.VGAlignment.Center;
      this.fsaAOIDefault.FontColor = global::Ogama.Properties.Settings.Default.AOIStandardFontColor;
      this.fsaAOIDefault.Location = new System.Drawing.Point(219, 47);
      this.fsaAOIDefault.Margin = new System.Windows.Forms.Padding(10, 10, 10, 10);
      this.fsaAOIDefault.Name = "fsaAOIDefault";
      this.fsaAOIDefault.SampleString = "Sample ...";
      this.fsaAOIDefault.Size = new System.Drawing.Size(130, 20);
      this.fsaAOIDefault.TabIndex = 13;
      // 
      // psaAOIStandard
      // 
      this.psaAOIStandard.BackColor = System.Drawing.Color.White;
      this.psaAOIStandard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.psaAOIStandard.DataBindings.Add(new System.Windows.Forms.Binding("PenColor", global::Ogama.Properties.Settings.Default, "AOIStandardColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.psaAOIStandard.DataBindings.Add(new System.Windows.Forms.Binding("PenDashStyle", global::Ogama.Properties.Settings.Default, "AOIStandardStyle", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.psaAOIStandard.DataBindings.Add(new System.Windows.Forms.Binding("PenSize", global::Ogama.Properties.Settings.Default, "AOIStandardWidth", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.psaAOIStandard.Location = new System.Drawing.Point(219, 16);
      this.psaAOIStandard.Name = "psaAOIStandard";
      this.psaAOIStandard.PenColor = global::Ogama.Properties.Settings.Default.AOIStandardColor;
      this.psaAOIStandard.PenDashStyle = global::Ogama.Properties.Settings.Default.AOIStandardStyle;
      this.psaAOIStandard.PenSize = global::Ogama.Properties.Settings.Default.AOIStandardWidth;
      this.psaAOIStandard.Size = new System.Drawing.Size(130, 20);
      this.psaAOIStandard.TabIndex = 11;
      // 
      // psaAOITarget
      // 
      this.psaAOITarget.BackColor = System.Drawing.Color.White;
      this.psaAOITarget.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.psaAOITarget.DataBindings.Add(new System.Windows.Forms.Binding("PenColor", global::Ogama.Properties.Settings.Default, "AOITargetColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.psaAOITarget.DataBindings.Add(new System.Windows.Forms.Binding("PenDashStyle", global::Ogama.Properties.Settings.Default, "AOITargetStyle", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.psaAOITarget.DataBindings.Add(new System.Windows.Forms.Binding("PenSize", global::Ogama.Properties.Settings.Default, "AOITargetWidth", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.psaAOITarget.Location = new System.Drawing.Point(219, 78);
      this.psaAOITarget.Name = "psaAOITarget";
      this.psaAOITarget.PenColor = global::Ogama.Properties.Settings.Default.AOITargetColor;
      this.psaAOITarget.PenDashStyle = global::Ogama.Properties.Settings.Default.AOITargetStyle;
      this.psaAOITarget.PenSize = global::Ogama.Properties.Settings.Default.AOITargetWidth;
      this.psaAOITarget.Size = new System.Drawing.Size(130, 20);
      this.psaAOITarget.TabIndex = 9;
      // 
      // psaAOISearchRect
      // 
      this.psaAOISearchRect.BackColor = System.Drawing.Color.White;
      this.psaAOISearchRect.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.psaAOISearchRect.DataBindings.Add(new System.Windows.Forms.Binding("PenColor", global::Ogama.Properties.Settings.Default, "AOISearchRectColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.psaAOISearchRect.DataBindings.Add(new System.Windows.Forms.Binding("PenDashStyle", global::Ogama.Properties.Settings.Default, "AOISearchRectStyle", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.psaAOISearchRect.DataBindings.Add(new System.Windows.Forms.Binding("PenSize", global::Ogama.Properties.Settings.Default, "AOISearchRectWidth", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.psaAOISearchRect.Location = new System.Drawing.Point(219, 140);
      this.psaAOISearchRect.Name = "psaAOISearchRect";
      this.psaAOISearchRect.PenColor = global::Ogama.Properties.Settings.Default.AOISearchRectColor;
      this.psaAOISearchRect.PenDashStyle = global::Ogama.Properties.Settings.Default.AOISearchRectStyle;
      this.psaAOISearchRect.PenSize = global::Ogama.Properties.Settings.Default.AOISearchRectWidth;
      this.psaAOISearchRect.Size = new System.Drawing.Size(130, 20);
      this.psaAOISearchRect.TabIndex = 7;
      // 
      // tbpMain
      // 
      this.tbpMain.Controls.Add(this.label6);
      this.tbpMain.Controls.Add(this.cbbPresentationMonitor);
      this.tbpMain.Controls.Add(this.label8);
      this.tbpMain.Controls.Add(this.pictureBox7);
      this.tbpMain.Controls.Add(this.label39);
      this.tbpMain.Controls.Add(this.label2);
      this.tbpMain.Controls.Add(this.nudRecentFiles);
      this.tbpMain.Controls.Add(this.clbBackground);
      this.tbpMain.ImageKey = "otheroptions.ico";
      this.tbpMain.Location = new System.Drawing.Point(4, 42);
      this.tbpMain.Name = "tbpMain";
      this.tbpMain.Padding = new System.Windows.Forms.Padding(3);
      this.tbpMain.Size = new System.Drawing.Size(487, 231);
      this.tbpMain.TabIndex = 0;
      this.tbpMain.Text = "Other options";
      this.tbpMain.UseVisualStyleBackColor = true;
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Location = new System.Drawing.Point(58, 85);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(234, 13);
      this.label6.TabIndex = 33;
      this.label6.Text = "Presentation Monitor in Multmonitor-Environment";
      // 
      // cbbPresentationMonitor
      // 
      this.cbbPresentationMonitor.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Ogama.Properties.Settings.Default, "PresentationScreenMonitor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.cbbPresentationMonitor.FormattingEnabled = true;
      this.cbbPresentationMonitor.Location = new System.Drawing.Point(305, 82);
      this.cbbPresentationMonitor.Name = "cbbPresentationMonitor";
      this.cbbPresentationMonitor.Size = new System.Drawing.Size(120, 21);
      this.cbbPresentationMonitor.TabIndex = 32;
      this.cbbPresentationMonitor.Text = global::Ogama.Properties.Settings.Default.PresentationScreenMonitor;
      // 
      // label8
      // 
      this.label8.AutoSize = true;
      this.label8.Location = new System.Drawing.Point(58, 52);
      this.label8.Name = "label8";
      this.label8.Size = new System.Drawing.Size(127, 13);
      this.label8.TabIndex = 22;
      this.label8.Text = "Forms background color: ";
      // 
      // pictureBox7
      // 
      this.pictureBox7.Image = global::Ogama.Properties.Resources.otheroptions;
      this.pictureBox7.Location = new System.Drawing.Point(6, 6);
      this.pictureBox7.Name = "pictureBox7";
      this.pictureBox7.Size = new System.Drawing.Size(37, 38);
      this.pictureBox7.TabIndex = 5;
      this.pictureBox7.TabStop = false;
      // 
      // label39
      // 
      this.label39.AutoSize = true;
      this.label39.Location = new System.Drawing.Point(6, 57);
      this.label39.Name = "label39";
      this.label39.Size = new System.Drawing.Size(0, 13);
      this.label39.TabIndex = 2;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(58, 20);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(139, 13);
      this.label2.TabIndex = 0;
      this.label2.Text = "Number of recent files in list:";
      // 
      // nudRecentFiles
      // 
      this.nudRecentFiles.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::Ogama.Properties.Settings.Default, "NumberOfRecentFiles", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.nudRecentFiles.Location = new System.Drawing.Point(305, 13);
      this.nudRecentFiles.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
      this.nudRecentFiles.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nudRecentFiles.Name = "nudRecentFiles";
      this.nudRecentFiles.Size = new System.Drawing.Size(41, 20);
      this.nudRecentFiles.TabIndex = 1;
      this.nudRecentFiles.Value = global::Ogama.Properties.Settings.Default.NumberOfRecentFiles;
      // 
      // clbBackground
      // 
      this.clbBackground.AutoButtonString = "Automatic";
      this.clbBackground.CurrentColor = global::Ogama.Properties.Settings.Default.BackgroundColorForms;
      this.clbBackground.DataBindings.Add(new System.Windows.Forms.Binding("CurrentColor", global::Ogama.Properties.Settings.Default, "BackgroundColorForms", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.clbBackground.Location = new System.Drawing.Point(305, 46);
      this.clbBackground.Margin = new System.Windows.Forms.Padding(0);
      this.clbBackground.Name = "clbBackground";
      this.clbBackground.Size = new System.Drawing.Size(75, 23);
      this.clbBackground.TabIndex = 21;
      this.clbBackground.UseVisualStyleBackColor = true;
      // 
      // imageList1
      // 
      this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
      this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
      this.imageList1.Images.SetKeyName(0, "ReplayLogo.png");
      this.imageList1.Images.SetKeyName(1, "AOILogo.png");
      this.imageList1.Images.SetKeyName(2, "AttentionMapLogo.png");
      this.imageList1.Images.SetKeyName(3, "FixationsLogo.png");
      this.imageList1.Images.SetKeyName(4, "otheroptions.ico");
      // 
      // toolStripSeparator7
      // 
      this.toolStripSeparator7.Name = "toolStripSeparator7";
      this.toolStripSeparator7.Size = new System.Drawing.Size(6, 23);
      // 
      // toolStripButton1
      // 
      this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.toolStripButton1.Enabled = false;
      this.toolStripButton1.Image = global::Ogama.Properties.Resources.Eye;
      this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripButton1.Name = "toolStripButton1";
      this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
      this.toolStripButton1.Text = "toolStripButton4";
      // 
      // toolStripSeparator1
      // 
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
      // 
      // toolStripButton8
      // 
      this.toolStripButton8.CheckOnClick = true;
      this.toolStripButton8.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.toolStripButton8.Image = global::Ogama.Properties.Resources.RPLSpot;
      this.toolStripButton8.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripButton8.Name = "toolStripButton8";
      this.toolStripButton8.Size = new System.Drawing.Size(23, 22);
      this.toolStripButton8.ToolTipText = "Display Gaze as Spotlight";
      // 
      // toolStripSeparator2
      // 
      this.toolStripSeparator2.Name = "toolStripSeparator2";
      this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
      // 
      // toolStripButton9
      // 
      this.toolStripButton9.CheckOnClick = true;
      this.toolStripButton9.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.toolStripButton9.Image = global::Ogama.Properties.Resources.RPLCut;
      this.toolStripButton9.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripButton9.Name = "toolStripButton9";
      this.toolStripButton9.Size = new System.Drawing.Size(23, 22);
      this.toolStripButton9.ToolTipText = "Reduce the amount of displayed gaze data.";
      // 
      // toolStripButton10
      // 
      this.toolStripButton10.CheckOnClick = true;
      this.toolStripButton10.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.toolStripButton10.Image = global::Ogama.Properties.Resources.RPLBlink;
      this.toolStripButton10.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripButton10.Name = "toolStripButton10";
      this.toolStripButton10.Size = new System.Drawing.Size(23, 22);
      this.toolStripButton10.ToolTipText = "Show blinks while playing";
      // 
      // toolStripSeparator10
      // 
      this.toolStripSeparator10.Name = "toolStripSeparator10";
      this.toolStripSeparator10.Size = new System.Drawing.Size(6, 23);
      // 
      // btnOK
      // 
      this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.btnOK.Location = new System.Drawing.Point(332, 13);
      this.btnOK.Name = "btnOK";
      this.btnOK.Size = new System.Drawing.Size(75, 23);
      this.btnOK.TabIndex = 3;
      this.btnOK.Text = "&OK";
      this.btnOK.UseVisualStyleBackColor = true;
      this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
      // 
      // btnCancel
      // 
      this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnCancel.Location = new System.Drawing.Point(413, 13);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(75, 23);
      this.btnCancel.TabIndex = 4;
      this.btnCancel.Text = "&Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
      // 
      // tableLayoutPanel1
      // 
      this.tableLayoutPanel1.ColumnCount = 1;
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 2);
      this.tableLayoutPanel1.Controls.Add(this.tabControl1, 0, 1);
      this.tableLayoutPanel1.Controls.Add(this.dialogTop1, 0, 0);
      this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
      this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
      this.tableLayoutPanel1.Name = "tableLayoutPanel1";
      this.tableLayoutPanel1.RowCount = 3;
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 66F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
      this.tableLayoutPanel1.Size = new System.Drawing.Size(501, 394);
      this.tableLayoutPanel1.TabIndex = 5;
      // 
      // panel2
      // 
      this.panel2.Controls.Add(this.label9);
      this.panel2.Controls.Add(this.btnOK);
      this.panel2.Controls.Add(this.btnCancel);
      this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel2.Location = new System.Drawing.Point(3, 352);
      this.panel2.Name = "panel2";
      this.panel2.Size = new System.Drawing.Size(495, 39);
      this.panel2.TabIndex = 1;
      // 
      // label9
      // 
      this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label9.Location = new System.Drawing.Point(4, 5);
      this.label9.Name = "label9";
      this.label9.Size = new System.Drawing.Size(322, 34);
      this.label9.TabIndex = 5;
      this.label9.Text = "Please note: The changes will not take effect until restarting the corresponding " +
          "modules.";
      // 
      // dialogTop1
      // 
      this.dialogTop1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(237)))));
      this.dialogTop1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("dialogTop1.BackgroundImage")));
      this.dialogTop1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
      this.dialogTop1.Description = "Customize Ogamas appearance ...";
      this.dialogTop1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.dialogTop1.Location = new System.Drawing.Point(0, 0);
      this.dialogTop1.Logo = global::Ogama.Properties.Resources.otheroptions;
      this.dialogTop1.Margin = new System.Windows.Forms.Padding(0);
      this.dialogTop1.Name = "dialogTop1";
      this.dialogTop1.Size = new System.Drawing.Size(501, 66);
      this.dialogTop1.TabIndex = 2;
      // 
      // button1
      // 
      this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
      this.button1.Image = global::Ogama.Properties.Resources.Color_fontHS;
      this.button1.Location = new System.Drawing.Point(299, 40);
      this.button1.Margin = new System.Windows.Forms.Padding(0);
      this.button1.Name = "button1";
      this.button1.Size = new System.Drawing.Size(20, 20);
      this.button1.TabIndex = 12;
      this.button1.UseVisualStyleBackColor = true;
      // 
      // btnGazeCursorMode
      // 
      this.btnGazeCursorMode.Checked = global::Ogama.Properties.Settings.Default.GazeModeCursor;
      this.btnGazeCursorMode.CheckOnClick = true;
      this.btnGazeCursorMode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnGazeCursorMode.Image = global::Ogama.Properties.Resources.RPLCursor;
      this.btnGazeCursorMode.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnGazeCursorMode.Name = "btnGazeCursorMode";
      this.btnGazeCursorMode.Size = new System.Drawing.Size(23, 20);
      this.btnGazeCursorMode.ToolTipText = "Display Gaze Cursor";
      // 
      // btnGazePathMode
      // 
      this.btnGazePathMode.Checked = global::Ogama.Properties.Settings.Default.GazeModePath;
      this.btnGazePathMode.CheckOnClick = true;
      this.btnGazePathMode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnGazePathMode.Image = global::Ogama.Properties.Resources.RPLPath;
      this.btnGazePathMode.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnGazePathMode.Name = "btnGazePathMode";
      this.btnGazePathMode.Size = new System.Drawing.Size(23, 20);
      this.btnGazePathMode.ToolTipText = "Display Gaze Path";
      // 
      // btnGazeFixMode
      // 
      this.btnGazeFixMode.Checked = global::Ogama.Properties.Settings.Default.GazeModeFixations;
      this.btnGazeFixMode.CheckOnClick = true;
      this.btnGazeFixMode.CheckState = System.Windows.Forms.CheckState.Checked;
      this.btnGazeFixMode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnGazeFixMode.Image = global::Ogama.Properties.Resources.RPLFix;
      this.btnGazeFixMode.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnGazeFixMode.Name = "btnGazeFixMode";
      this.btnGazeFixMode.Size = new System.Drawing.Size(23, 20);
      this.btnGazeFixMode.ToolTipText = "Display Gaze Fixations";
      // 
      // btnGazeFixConMode
      // 
      this.btnGazeFixConMode.Checked = global::Ogama.Properties.Settings.Default.GazeModeFixCon;
      this.btnGazeFixConMode.CheckOnClick = true;
      this.btnGazeFixConMode.CheckState = System.Windows.Forms.CheckState.Checked;
      this.btnGazeFixConMode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnGazeFixConMode.Image = global::Ogama.Properties.Resources.RPLFixCon;
      this.btnGazeFixConMode.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnGazeFixConMode.Name = "btnGazeFixConMode";
      this.btnGazeFixConMode.Size = new System.Drawing.Size(23, 20);
      this.btnGazeFixConMode.ToolTipText = "Display Gaze Fixation Connections";
      // 
      // btnGazeSpotlightMode
      // 
      this.btnGazeSpotlightMode.Checked = global::Ogama.Properties.Settings.Default.GazeModeSpotlight;
      this.btnGazeSpotlightMode.CheckOnClick = true;
      this.btnGazeSpotlightMode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnGazeSpotlightMode.Image = global::Ogama.Properties.Resources.RPLSpot;
      this.btnGazeSpotlightMode.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnGazeSpotlightMode.Name = "btnGazeSpotlightMode";
      this.btnGazeSpotlightMode.Size = new System.Drawing.Size(23, 20);
      this.btnGazeSpotlightMode.ToolTipText = "Display Gaze as Spotlight";
      // 
      // btnGazeCutPathMode
      // 
      this.btnGazeCutPathMode.Checked = global::Ogama.Properties.Settings.Default.GazeModeCutPath;
      this.btnGazeCutPathMode.CheckOnClick = true;
      this.btnGazeCutPathMode.CheckState = System.Windows.Forms.CheckState.Checked;
      this.btnGazeCutPathMode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnGazeCutPathMode.Image = global::Ogama.Properties.Resources.RPLCut;
      this.btnGazeCutPathMode.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnGazeCutPathMode.Name = "btnGazeCutPathMode";
      this.btnGazeCutPathMode.Size = new System.Drawing.Size(23, 20);
      this.btnGazeCutPathMode.ToolTipText = "Reduce the amount of displayed gaze data.";
      // 
      // btnGazeBlinksMode
      // 
      this.btnGazeBlinksMode.Checked = global::Ogama.Properties.Settings.Default.GazeModeBlinks;
      this.btnGazeBlinksMode.CheckOnClick = true;
      this.btnGazeBlinksMode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnGazeBlinksMode.Image = global::Ogama.Properties.Resources.RPLBlink;
      this.btnGazeBlinksMode.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnGazeBlinksMode.Name = "btnGazeBlinksMode";
      this.btnGazeBlinksMode.Size = new System.Drawing.Size(23, 20);
      this.btnGazeBlinksMode.ToolTipText = "Show blinks while playing";
      // 
      // toolStripButton2
      // 
      this.toolStripButton2.Checked = global::Ogama.Properties.Settings.Default.GazeModeCursor;
      this.toolStripButton2.CheckOnClick = true;
      this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.toolStripButton2.Image = global::Ogama.Properties.Resources.RPLCursor;
      this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripButton2.Name = "toolStripButton2";
      this.toolStripButton2.Size = new System.Drawing.Size(23, 22);
      this.toolStripButton2.ToolTipText = "Display Gaze Cursor";
      // 
      // toolStripButton5
      // 
      this.toolStripButton5.Checked = global::Ogama.Properties.Settings.Default.GazeModePath;
      this.toolStripButton5.CheckOnClick = true;
      this.toolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.toolStripButton5.Image = global::Ogama.Properties.Resources.RPLPath;
      this.toolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripButton5.Name = "toolStripButton5";
      this.toolStripButton5.Size = new System.Drawing.Size(23, 22);
      this.toolStripButton5.ToolTipText = "Display Gaze Path";
      // 
      // toolStripButton6
      // 
      this.toolStripButton6.Checked = global::Ogama.Properties.Settings.Default.GazeModeFixations;
      this.toolStripButton6.CheckOnClick = true;
      this.toolStripButton6.CheckState = System.Windows.Forms.CheckState.Checked;
      this.toolStripButton6.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.toolStripButton6.Image = global::Ogama.Properties.Resources.RPLFix;
      this.toolStripButton6.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripButton6.Name = "toolStripButton6";
      this.toolStripButton6.Size = new System.Drawing.Size(23, 22);
      this.toolStripButton6.ToolTipText = "Display Gaze Fixations";
      // 
      // toolStripButton7
      // 
      this.toolStripButton7.Checked = global::Ogama.Properties.Settings.Default.GazeModeBlinks;
      this.toolStripButton7.CheckOnClick = true;
      this.toolStripButton7.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.toolStripButton7.Image = global::Ogama.Properties.Resources.RPLFixCon;
      this.toolStripButton7.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripButton7.Name = "toolStripButton7";
      this.toolStripButton7.Size = new System.Drawing.Size(23, 22);
      this.toolStripButton7.ToolTipText = "Display Gaze Fixation Connections";
      // 
      // btnMouseCursorMode
      // 
      this.btnMouseCursorMode.Checked = global::Ogama.Properties.Settings.Default.MouseModeCursor;
      this.btnMouseCursorMode.CheckOnClick = true;
      this.btnMouseCursorMode.CheckState = System.Windows.Forms.CheckState.Checked;
      this.btnMouseCursorMode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnMouseCursorMode.Image = global::Ogama.Properties.Resources.RPLCursor;
      this.btnMouseCursorMode.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnMouseCursorMode.Name = "btnMouseCursorMode";
      this.btnMouseCursorMode.Size = new System.Drawing.Size(23, 20);
      this.btnMouseCursorMode.ToolTipText = "Display Mouse Cursor";
      // 
      // btnMousePathMode
      // 
      this.btnMousePathMode.Checked = global::Ogama.Properties.Settings.Default.MouseModePath;
      this.btnMousePathMode.CheckOnClick = true;
      this.btnMousePathMode.CheckState = System.Windows.Forms.CheckState.Checked;
      this.btnMousePathMode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnMousePathMode.Image = global::Ogama.Properties.Resources.RPLPath;
      this.btnMousePathMode.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnMousePathMode.Name = "btnMousePathMode";
      this.btnMousePathMode.Size = new System.Drawing.Size(23, 20);
      this.btnMousePathMode.ToolTipText = "Display Mouse Path";
      // 
      // btnMouseFixMode
      // 
      this.btnMouseFixMode.Checked = global::Ogama.Properties.Settings.Default.MouseModeFixations;
      this.btnMouseFixMode.CheckOnClick = true;
      this.btnMouseFixMode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnMouseFixMode.Image = global::Ogama.Properties.Resources.RPLFix;
      this.btnMouseFixMode.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnMouseFixMode.Name = "btnMouseFixMode";
      this.btnMouseFixMode.Size = new System.Drawing.Size(23, 20);
      this.btnMouseFixMode.ToolTipText = "Display Mouse Fixations";
      // 
      // btnMouseFixConMode
      // 
      this.btnMouseFixConMode.Checked = global::Ogama.Properties.Settings.Default.MouseModeFixCon;
      this.btnMouseFixConMode.CheckOnClick = true;
      this.btnMouseFixConMode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnMouseFixConMode.Image = global::Ogama.Properties.Resources.RPLFixCon;
      this.btnMouseFixConMode.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnMouseFixConMode.Name = "btnMouseFixConMode";
      this.btnMouseFixConMode.Size = new System.Drawing.Size(23, 20);
      this.btnMouseFixConMode.ToolTipText = "Display Mouse Fixation Connections";
      // 
      // btnMouseSpotlightMode
      // 
      this.btnMouseSpotlightMode.Checked = global::Ogama.Properties.Settings.Default.MouseModeSpotlight;
      this.btnMouseSpotlightMode.CheckOnClick = true;
      this.btnMouseSpotlightMode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnMouseSpotlightMode.Image = global::Ogama.Properties.Resources.RPLSpot;
      this.btnMouseSpotlightMode.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnMouseSpotlightMode.Name = "btnMouseSpotlightMode";
      this.btnMouseSpotlightMode.Size = new System.Drawing.Size(23, 20);
      this.btnMouseSpotlightMode.ToolTipText = "Display Mouse as Spotlight";
      // 
      // btnMouseCutPathMode
      // 
      this.btnMouseCutPathMode.Checked = global::Ogama.Properties.Settings.Default.MouseModeCutPath;
      this.btnMouseCutPathMode.CheckOnClick = true;
      this.btnMouseCutPathMode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnMouseCutPathMode.Image = global::Ogama.Properties.Resources.RPLCut;
      this.btnMouseCutPathMode.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnMouseCutPathMode.Name = "btnMouseCutPathMode";
      this.btnMouseCutPathMode.Size = new System.Drawing.Size(23, 20);
      this.btnMouseCutPathMode.ToolTipText = "Reduce the amount of displayed mouse data.";
      // 
      // Options
      // 
      this.AcceptButton = this.btnOK;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.btnCancel;
      this.ClientSize = new System.Drawing.Size(501, 394);
      this.Controls.Add(this.tableLayoutPanel1);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "Options";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "OGAMA options ...";
      this.tabControl1.ResumeLayout(false);
      this.tbpReplay.ResumeLayout(false);
      this.tbpReplay.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.nudMaxPointsPolyline)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.nudFixShown)).EndInit();
      this.tbpReplayGaze.ResumeLayout(false);
      this.tbpReplayGaze.PerformLayout();
      this.toolStripGaze.ResumeLayout(false);
      this.toolStripGaze.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.nudGazeCursorSize)).EndInit();
      this.tpbReplayMouse.ResumeLayout(false);
      this.tpbReplayMouse.PerformLayout();
      this.toolStripMouse.ResumeLayout(false);
      this.toolStripMouse.PerformLayout();
      this.groupBox2.ResumeLayout(false);
      this.groupBox2.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.nudMouseCursorSize)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
      this.tbpFixationsInterface.ResumeLayout(false);
      this.grpMouseFixations.ResumeLayout(false);
      this.grpMouseFixations.PerformLayout();
      this.grbGazeFixations.ResumeLayout(false);
      this.grbGazeFixations.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox9)).EndInit();
      this.tpbAOI.ResumeLayout(false);
      this.tpbAOI.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).EndInit();
      this.tbpMain.ResumeLayout(false);
      this.tbpMain.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.nudRecentFiles)).EndInit();
      this.tableLayoutPanel1.ResumeLayout(false);
      this.panel2.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.TabControl tabControl1;
    private System.Windows.Forms.TabPage tbpMain;
    private System.Windows.Forms.NumericUpDown nudRecentFiles;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TabPage tbpReplayGaze;
    private System.Windows.Forms.TabPage tpbAOI;
    private System.Windows.Forms.TabPage tbpFixationsInterface;
    private System.Windows.Forms.Label label12;
    private System.Windows.Forms.Label label11;
    private System.Windows.Forms.Label label13;
    private System.Windows.Forms.Label label15;
    private System.Windows.Forms.Label label16;
    private System.Windows.Forms.Label label19;
    private System.Windows.Forms.Label label18;
    private System.Windows.Forms.Label label17;
    private System.Windows.Forms.Label label14;
    private System.Windows.Forms.Label label20;
    private System.Windows.Forms.Label label28;
    private System.Windows.Forms.Label label27;
    private System.Windows.Forms.Label label26;
    private System.Windows.Forms.Label label25;
    private System.Windows.Forms.Label label23;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.ToolStrip toolStrip1;
    private System.Windows.Forms.ToolStripButton btnGazeModeCursor;
    private System.Windows.Forms.ToolStripButton btnGazeModePath;
    private System.Windows.Forms.ToolStripButton btnGazeModeFix;
    private System.Windows.Forms.ToolStripButton btnGazeModeFixCon;
    private System.Windows.Forms.ToolStripButton btnGazeModeSpot;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
    private System.Windows.Forms.ToolStripButton btnGazeCutPath;
    private System.Windows.Forms.ToolStripButton btnGazeBlinks;
    private System.Windows.Forms.ToolStrip toolStrip3;
    private System.Windows.Forms.ToolStripButton btnMouseModeCursor;
    private System.Windows.Forms.ToolStripButton btnMouseModePath;
    private System.Windows.Forms.ToolStripButton btnMouseModeFix;
    private System.Windows.Forms.ToolStripButton btnMouseModeFixCon;
    private System.Windows.Forms.ToolStripButton btnMouseModeSpot;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
    private System.Windows.Forms.ToolStripButton btnMouseCutPath;
    private System.Windows.Forms.ToolStrip toolStrip2;
    private System.Windows.Forms.ToolStripButton toolStripButton1;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    private System.Windows.Forms.ToolStripButton toolStripButton2;
    private System.Windows.Forms.ToolStripButton toolStripButton5;
    private System.Windows.Forms.ToolStripButton toolStripButton6;
    private System.Windows.Forms.ToolStripButton toolStripButton7;
    private System.Windows.Forms.ToolStripButton toolStripButton8;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    private System.Windows.Forms.ToolStripButton toolStripButton9;
    private System.Windows.Forms.ToolStripButton toolStripButton10;
    private System.Windows.Forms.ToolStrip toolStrip4;
    private System.Windows.Forms.ToolStripButton btnGazeCursorMode;
    private System.Windows.Forms.ToolStripButton btnGazePathMode;
    private System.Windows.Forms.ToolStripButton btnGazeFixMode;
    private System.Windows.Forms.ToolStripButton btnGazeFixConMode;
    private System.Windows.Forms.ToolStripButton btnGazeSpotlightMode;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
    private System.Windows.Forms.ToolStripButton btnGazeCutPathMode;
    private System.Windows.Forms.ToolStripButton btnGazeBlinksMode;
    private System.Windows.Forms.TabPage tpbReplayMouse;
    private System.Windows.Forms.TabPage tbpReplay;
    private System.Windows.Forms.ComboBox cbbSpeed;
    private System.Windows.Forms.Label label22;
    private System.Windows.Forms.Label label29;
    private System.Windows.Forms.Label label24;
    private System.Windows.Forms.NumericUpDown nudMaxPointsPolyline;
    private System.Windows.Forms.NumericUpDown nudFixShown;
    private System.Windows.Forms.ToolStrip toolStrip5;
    private System.Windows.Forms.ToolStripButton btnMouseCursorMode;
    private System.Windows.Forms.ToolStripButton btnMousePathMode;
    private System.Windows.Forms.ToolStripButton btnMouseFixMode;
    private System.Windows.Forms.ToolStripButton btnMouseFixConMode;
    private System.Windows.Forms.ToolStripButton btnMouseSpotlightMode;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
    private System.Windows.Forms.ToolStripButton btnMouseCutPathMode;
    private System.Windows.Forms.Label label21;
    private System.Windows.Forms.PictureBox pictureBox2;
    private System.Windows.Forms.ImageList imageList1;
    private System.Windows.Forms.PictureBox pictureBox4;
    private System.Windows.Forms.PictureBox pictureBox3;
    private System.Windows.Forms.PictureBox pictureBox5;
    private System.Windows.Forms.PictureBox pictureBox6;
    private System.Windows.Forms.ComboBox cbbGazeCursorType;
    private PenStyleArea psaGazeCursor;
    private System.Windows.Forms.Button btnGazeCursorStyle;
    private System.Windows.Forms.Button btnOK;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.Button btnGazeFixConStyle;
    private System.Windows.Forms.Button btnGazeFixStyle;
    private System.Windows.Forms.Button btnGazePathStyle;
    private PenStyleArea psaGazeFixCon;
    private PenStyleArea psaGazeFix;
    private PenStyleArea psaGazePath;
    private System.Windows.Forms.GroupBox groupBox2;
    private System.Windows.Forms.Button btnMouseFixConStyle;
    private System.Windows.Forms.Button btnMouseFixStyle;
    private System.Windows.Forms.Button btnMousePathStyle;
    private System.Windows.Forms.Button btnMouseCursorStyle;
    private PenStyleArea psaMouseFixCon;
    private PenStyleArea psaMouseFix;
    private PenStyleArea psaMousePath;
    private PenStyleArea psaMouseCursor;
    private System.Windows.Forms.ComboBox cbbMouseCursorType;
    private System.Windows.Forms.Label label30;
    private System.Windows.Forms.Label label31;
    private System.Windows.Forms.Label label32;
    private System.Windows.Forms.Label label33;
    private System.Windows.Forms.Label label34;
    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    private System.Windows.Forms.Panel panel2;
    private System.Windows.Forms.Button btnNoDataStyle;
    private PenStyleArea psaNoData;
    private System.Windows.Forms.Label label35;
    private System.Windows.Forms.Label label36;
    private System.Windows.Forms.NumericUpDown nudGazeCursorSize;
    private System.Windows.Forms.Label label37;
    private System.Windows.Forms.NumericUpDown nudMouseCursorSize;
    private System.Windows.Forms.Button btnAOIStandardStyle;
    private PenStyleArea psaAOIStandard;
    private System.Windows.Forms.Button btnAOITargetStyle;
    private PenStyleArea psaAOITarget;
    private System.Windows.Forms.Button btnAOISearchRectStyle;
    private PenStyleArea psaAOISearchRect;
    private FontStyleArea fsaAOISearchRect;
    private FontStyleArea fsaAOITarget;
    private FontStyleArea fsaAOIDefault;
    private System.Windows.Forms.Button btnAOIStandardFont;
    private System.Windows.Forms.Button btnAOISearchRectFont;
    private System.Windows.Forms.Button btnAOITargetFont;
    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.ComboBox cbbGazeFixationsDisplayMode;
    private FontStyleArea fsaGazeFixationsFont;
    private System.Windows.Forms.Button btnGazeFixationsFontStyle;
    private System.Windows.Forms.Button btnGazeFixationsPenStyle;
    private PenStyleArea psaGazeFixationsPenStyle;
    private System.Windows.Forms.ToolStrip toolStripGaze;
    private System.Windows.Forms.ToolStrip toolStripMouse;
    private System.Windows.Forms.Label label9;
    private System.Windows.Forms.Label label39;
    private System.Windows.Forms.PictureBox pictureBox9;
    private System.Windows.Forms.PictureBox pictureBox8;
    private System.Windows.Forms.PictureBox pictureBox7;
    private System.Windows.Forms.GroupBox grbGazeFixations;
    private System.Windows.Forms.GroupBox grpMouseFixations;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.ComboBox cbbMouseFixationsDisplayMode;
    private PenStyleArea psaMouseFixationsPenStyle;
    private FontStyleArea fsaMouseFixationsFont;
    private System.Windows.Forms.Button btnMouseFixationsPenStyle;
    private System.Windows.Forms.Button btnMouseFixationsFontStyle;
    private System.Windows.Forms.Label label8;
    private ColorButton clbBackground;
    private DialogTop dialogTop1;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.ComboBox cbbPresentationMonitor;
  }
}
