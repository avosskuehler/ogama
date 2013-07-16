using OgamaControls;

namespace Ogama.Modules.AttentionMap
{
  using Ogama.Modules.Common.Controls;

  partial class AttentionMapModule
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AttentionMapModule));
      OgamaControls.Gradient gradient2 = new OgamaControls.Gradient();
      System.Drawing.Drawing2D.ColorBlend colorBlend2 = new System.Drawing.Drawing2D.ColorBlend();
      this.bgwCalcMap = new System.ComponentModel.BackgroundWorker();
      this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
      this.trvSubjects = new OgamaControls.CheckboxTreeView(this.components);
      this.imlTreeViewSubjects = new System.Windows.Forms.ImageList(this.components);
      this.gradientControl = new OgamaControls.GradientTypeEditorUI();
      this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
      this.spcPictureGradient = new System.Windows.Forms.SplitContainer();
      this.spcPicAndList = new System.Windows.Forms.SplitContainer();
      this.toolStripContainer2 = new System.Windows.Forms.ToolStripContainer();
      this.toolStrip2 = new System.Windows.Forms.ToolStrip();
      this.btnSeekNextSlide = new System.Windows.Forms.ToolStripButton();
      this.btnSeekPreviousSlide = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
      this.trialTimeLine = new TrialTimeLine(this.components);
      this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
      this.trbZoom = new OgamaControls.ToolStripTrackBar();
      this.pnlCanvas = new System.Windows.Forms.Panel();
      this.pnlPicture = new System.Windows.Forms.Panel();
      this.attentionMapPicture = new Ogama.Modules.AttentionMap.AttentionMapPicture();
      this.tosTrial = new System.Windows.Forms.ToolStrip();
      this.cbbTrial = new System.Windows.Forms.ToolStripComboBox();
      this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.btnEye = new System.Windows.Forms.ToolStripButton();
      this.btnMouse = new System.Windows.Forms.ToolStripButton();
      this.btnMouseClicks = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
      this.btnWeight = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
      this.mnuGradients = new System.Windows.Forms.ToolStripDropDownButton();
      this.mnuTrafficLight = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuRainbow = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuBlackMask = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuCustomGradient = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
      this.mnuShowGradientBuilder = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
      this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
      this.nudKernelSize = new OgamaControls.ToolStripNumericUpDown();
      this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
      this.btnHelp = new System.Windows.Forms.ToolStripButton();
      this.tosCalculation = new System.Windows.Forms.ToolStrip();
      this.btnStartCalculation = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
      this.btnAllFix = new System.Windows.Forms.ToolStripButton();
      this.btnSpecialFix = new System.Windows.Forms.ToolStripButton();
      this.nudFixNumber = new OgamaControls.ToolStripNumericUpDown();
      this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
      this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
      this.toolStripNumericUpDown1 = new OgamaControls.ToolStripNumericUpDown();
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
      this.spcPictureGradient.Panel1.SuspendLayout();
      this.spcPictureGradient.Panel2.SuspendLayout();
      this.spcPictureGradient.SuspendLayout();
      this.spcPicAndList.Panel1.SuspendLayout();
      this.spcPicAndList.Panel2.SuspendLayout();
      this.spcPicAndList.SuspendLayout();
      this.toolStripContainer2.BottomToolStripPanel.SuspendLayout();
      this.toolStripContainer2.ContentPanel.SuspendLayout();
      this.toolStripContainer2.SuspendLayout();
      this.toolStrip2.SuspendLayout();
      this.pnlCanvas.SuspendLayout();
      this.pnlPicture.SuspendLayout();
      this.tosTrial.SuspendLayout();
      this.tosCalculation.SuspendLayout();
      this.SuspendLayout();
      // 
      // bgwCalcMap
      // 
      this.bgwCalcMap.WorkerReportsProgress = true;
      this.bgwCalcMap.WorkerSupportsCancellation = true;
      this.bgwCalcMap.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwCalcMap_DoWork);
      this.bgwCalcMap.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwCalcMap_RunWorkerCompleted);
      this.bgwCalcMap.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgwCalcMap_ProgressChanged);
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
      this.trvSubjects.Size = new System.Drawing.Size(150, 341);
      this.trvSubjects.TabIndex = 11;
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
      // gradientControl
      // 
      colorBlend2.Colors = new System.Drawing.Color[] {
        System.Drawing.Color.Transparent,
        System.Drawing.Color.Transparent};
      colorBlend2.Positions = new float[] {
        0F,
        1F};
      gradient2.ColorBlend = colorBlend2;
      gradient2.GradientDirection = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
      this.gradientControl.Gradient = gradient2;
      this.gradientControl.Location = new System.Drawing.Point(0, 0);
      this.gradientControl.Name = "gradientControl";
      this.gradientControl.Size = new System.Drawing.Size(660, 86);
      this.gradientControl.TabIndex = 1;
      this.toolTip1.SetToolTip(this.gradientControl, "Define additional color stops by clicking on the gradient.\r\nModify color stops by" +
              " clicking on the triangles.\r\nRemove a color stop by dragging it out of the gradi" +
              "ent.");
      this.gradientControl.GradientChanged += new System.EventHandler(this.gradientControl_GradientChanged);
      // 
      // toolStripContainer1
      // 
      // 
      // toolStripContainer1.ContentPanel
      // 
      this.toolStripContainer1.ContentPanel.Controls.Add(this.spcPictureGradient);
      this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(785, 431);
      this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
      this.toolStripContainer1.Name = "toolStripContainer1";
      this.toolStripContainer1.Size = new System.Drawing.Size(785, 482);
      this.toolStripContainer1.TabIndex = 1;
      this.toolStripContainer1.Text = "toolStripContainer1";
      // 
      // toolStripContainer1.TopToolStripPanel
      // 
      this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.tosTrial);
      this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.tosCalculation);
      // 
      // spcPictureGradient
      // 
      this.spcPictureGradient.Dock = System.Windows.Forms.DockStyle.Fill;
      this.spcPictureGradient.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
      this.spcPictureGradient.Location = new System.Drawing.Point(0, 0);
      this.spcPictureGradient.Name = "spcPictureGradient";
      this.spcPictureGradient.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // spcPictureGradient.Panel1
      // 
      this.spcPictureGradient.Panel1.Controls.Add(this.spcPicAndList);
      // 
      // spcPictureGradient.Panel2
      // 
      this.spcPictureGradient.Panel2.Controls.Add(this.gradientControl);
      this.spcPictureGradient.Size = new System.Drawing.Size(785, 431);
      this.spcPictureGradient.SplitterDistance = 341;
      this.spcPictureGradient.TabIndex = 12;
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
      this.spcPicAndList.Panel1.Controls.Add(this.trvSubjects);
      // 
      // spcPicAndList.Panel2
      // 
      this.spcPicAndList.Panel2.Controls.Add(this.toolStripContainer2);
      this.spcPicAndList.Size = new System.Drawing.Size(785, 341);
      this.spcPicAndList.SplitterDistance = 150;
      this.spcPicAndList.TabIndex = 0;
      // 
      // toolStripContainer2
      // 
      // 
      // toolStripContainer2.BottomToolStripPanel
      // 
      this.toolStripContainer2.BottomToolStripPanel.Controls.Add(this.toolStrip2);
      // 
      // toolStripContainer2.ContentPanel
      // 
      this.toolStripContainer2.ContentPanel.Controls.Add(this.pnlCanvas);
      this.toolStripContainer2.ContentPanel.Margin = new System.Windows.Forms.Padding(0);
      this.toolStripContainer2.ContentPanel.Size = new System.Drawing.Size(631, 315);
      this.toolStripContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.toolStripContainer2.LeftToolStripPanelVisible = false;
      this.toolStripContainer2.Location = new System.Drawing.Point(0, 0);
      this.toolStripContainer2.Margin = new System.Windows.Forms.Padding(0);
      this.toolStripContainer2.Name = "toolStripContainer2";
      this.toolStripContainer2.RightToolStripPanelVisible = false;
      this.toolStripContainer2.Size = new System.Drawing.Size(631, 341);
      this.toolStripContainer2.TabIndex = 1;
      this.toolStripContainer2.Text = "toolStripContainer2";
      this.toolStripContainer2.TopToolStripPanelVisible = false;
      // 
      // toolStrip2
      // 
      this.toolStrip2.Dock = System.Windows.Forms.DockStyle.None;
      this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
      this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnSeekNextSlide,
            this.btnSeekPreviousSlide,
            this.toolStripSeparator3,
            this.trialTimeLine,
            this.toolStripSeparator10,
            this.trbZoom});
      this.toolStrip2.Location = new System.Drawing.Point(0, 0);
      this.toolStrip2.Name = "toolStrip2";
      this.toolStrip2.Size = new System.Drawing.Size(631, 26);
      this.toolStrip2.Stretch = true;
      this.toolStrip2.TabIndex = 1;
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
      this.trialTimeLine.Size = new System.Drawing.Size(437, 23);
      this.trialTimeLine.Text = "trialTimeLine";
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
      this.pnlCanvas.Size = new System.Drawing.Size(631, 315);
      this.pnlCanvas.TabIndex = 2;
      // 
      // pnlPicture
      // 
      this.pnlPicture.Controls.Add(this.attentionMapPicture);
      this.pnlPicture.Location = new System.Drawing.Point(113, 79);
      this.pnlPicture.Margin = new System.Windows.Forms.Padding(0);
      this.pnlPicture.Name = "pnlPicture";
      this.pnlPicture.Size = new System.Drawing.Size(300, 200);
      this.pnlPicture.TabIndex = 0;
      // 
      // attentionMapPicture
      // 
      this.attentionMapPicture.AnimationInterval = 10;
      this.attentionMapPicture.BackColor = System.Drawing.Color.Black;
      this.attentionMapPicture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
      this.attentionMapPicture.InvalidateInterval = 500;
      this.attentionMapPicture.Location = new System.Drawing.Point(0, 0);
      this.attentionMapPicture.Margin = new System.Windows.Forms.Padding(0);
      this.attentionMapPicture.Name = "attentionMapPicture";
      this.attentionMapPicture.Size = new System.Drawing.Size(300, 200);
      this.attentionMapPicture.TabIndex = 0;
      this.attentionMapPicture.TabStop = false;
      this.attentionMapPicture.ZoomFactor = 0F;
      // 
      // tosTrial
      // 
      this.tosTrial.DataBindings.Add(new System.Windows.Forms.Binding("Location", global::Ogama.Properties.Settings.Default, "ATMTrialToolbarLocation", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.tosTrial.Dock = System.Windows.Forms.DockStyle.None;
      this.tosTrial.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cbbTrial,
            this.toolStripSeparator1,
            this.btnEye,
            this.btnMouse,
            this.btnMouseClicks,
            this.toolStripSeparator8,
            this.btnWeight,
            this.toolStripSeparator6,
            this.mnuGradients,
            this.toolStripSeparator2,
            this.toolStripLabel1,
            this.nudKernelSize,
            this.toolStripSeparator4,
            this.btnHelp});
      this.tosTrial.Location = global::Ogama.Properties.Settings.Default.ATMTrialToolbarLocation;
      this.tosTrial.Name = "tosTrial";
      this.tosTrial.Size = new System.Drawing.Size(647, 26);
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
      this.toolStripSeparator1.Size = new System.Drawing.Size(6, 26);
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
      this.btnEye.Size = new System.Drawing.Size(23, 23);
      this.btnEye.Text = "Use gaze data for attention maps.";
      this.btnEye.Click += new System.EventHandler(this.btnEye_Click);
      // 
      // btnMouse
      // 
      this.btnMouse.CheckOnClick = true;
      this.btnMouse.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnMouse.Image = global::Ogama.Properties.Resources.Mouse;
      this.btnMouse.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnMouse.Name = "btnMouse";
      this.btnMouse.Size = new System.Drawing.Size(23, 23);
      this.btnMouse.Text = "Use mouse fixation data for attention maps.";
      this.btnMouse.Click += new System.EventHandler(this.btnMouse_Click);
      // 
      // btnMouseClicks
      // 
      this.btnMouseClicks.CheckOnClick = true;
      this.btnMouseClicks.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnMouseClicks.Image = global::Ogama.Properties.Resources.HandCursor;
      this.btnMouseClicks.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnMouseClicks.Name = "btnMouseClicks";
      this.btnMouseClicks.Size = new System.Drawing.Size(23, 23);
      this.btnMouseClicks.Text = "Use mouse click data for attention maps.";
      this.btnMouseClicks.Click += new System.EventHandler(this.btnMouseClicks_Click);
      // 
      // toolStripSeparator8
      // 
      this.toolStripSeparator8.Name = "toolStripSeparator8";
      this.toolStripSeparator8.Size = new System.Drawing.Size(6, 26);
      // 
      // btnWeight
      // 
      this.btnWeight.CheckOnClick = true;
      this.btnWeight.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnWeight.Image = global::Ogama.Properties.Resources.Weight;
      this.btnWeight.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnWeight.Name = "btnWeight";
      this.btnWeight.Size = new System.Drawing.Size(23, 23);
      this.btnWeight.Text = "Weight fixations";
      this.btnWeight.ToolTipText = "Weight fixations by length";
      // 
      // toolStripSeparator6
      // 
      this.toolStripSeparator6.Name = "toolStripSeparator6";
      this.toolStripSeparator6.Size = new System.Drawing.Size(6, 26);
      // 
      // mnuGradients
      // 
      this.mnuGradients.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuTrafficLight,
            this.mnuRainbow,
            this.mnuBlackMask,
            this.mnuCustomGradient,
            this.toolStripSeparator5,
            this.mnuShowGradientBuilder});
      this.mnuGradients.Image = global::Ogama.Properties.Resources.DisplayInColorHS;
      this.mnuGradients.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.mnuGradients.Name = "mnuGradients";
      this.mnuGradients.Size = new System.Drawing.Size(136, 23);
      this.mnuGradients.Text = "Select color palette";
      this.mnuGradients.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
      this.mnuGradients.ToolTipText = "Select color palette for the attention map.";
      // 
      // mnuTrafficLight
      // 
      this.mnuTrafficLight.Checked = true;
      this.mnuTrafficLight.CheckState = System.Windows.Forms.CheckState.Checked;
      this.mnuTrafficLight.Name = "mnuTrafficLight";
      this.mnuTrafficLight.Size = new System.Drawing.Size(190, 22);
      this.mnuTrafficLight.Text = "Traffic light";
      this.mnuTrafficLight.Paint += new System.Windows.Forms.PaintEventHandler(this.mnuTrafficLight_Paint);
      this.mnuTrafficLight.Click += new System.EventHandler(this.mnuTrafficLight_Click);
      // 
      // mnuRainbow
      // 
      this.mnuRainbow.ForeColor = System.Drawing.SystemColors.ControlText;
      this.mnuRainbow.Name = "mnuRainbow";
      this.mnuRainbow.Size = new System.Drawing.Size(190, 22);
      this.mnuRainbow.Text = "Rainbow";
      this.mnuRainbow.Paint += new System.Windows.Forms.PaintEventHandler(this.mnuRainbow_Paint);
      this.mnuRainbow.Click += new System.EventHandler(this.mnuRainbow_Click);
      // 
      // mnuBlackMask
      // 
      this.mnuBlackMask.ForeColor = System.Drawing.SystemColors.ControlText;
      this.mnuBlackMask.Name = "mnuBlackMask";
      this.mnuBlackMask.Size = new System.Drawing.Size(190, 22);
      this.mnuBlackMask.Text = "Black Mask";
      this.mnuBlackMask.Paint += new System.Windows.Forms.PaintEventHandler(this.mnuBlackMask_Paint);
      this.mnuBlackMask.Click += new System.EventHandler(this.mnuBlackMask_Click);
      // 
      // mnuCustomGradient
      // 
      this.mnuCustomGradient.Name = "mnuCustomGradient";
      this.mnuCustomGradient.Size = new System.Drawing.Size(190, 22);
      this.mnuCustomGradient.Text = "Custom";
      this.mnuCustomGradient.Paint += new System.Windows.Forms.PaintEventHandler(this.mnuCustomGradient_Paint);
      this.mnuCustomGradient.Click += new System.EventHandler(this.mnuCustomGradient_Click);
      // 
      // toolStripSeparator5
      // 
      this.toolStripSeparator5.Name = "toolStripSeparator5";
      this.toolStripSeparator5.Size = new System.Drawing.Size(187, 6);
      // 
      // mnuShowGradientBuilder
      // 
      this.mnuShowGradientBuilder.CheckOnClick = true;
      this.mnuShowGradientBuilder.Image = global::Ogama.Properties.Resources.DisplayInColorHS;
      this.mnuShowGradientBuilder.Name = "mnuShowGradientBuilder";
      this.mnuShowGradientBuilder.Size = new System.Drawing.Size(190, 22);
      this.mnuShowGradientBuilder.Text = "Show gradient builder";
      this.mnuShowGradientBuilder.Click += new System.EventHandler(this.mnuShowGradientBuilder_Click);
      // 
      // toolStripSeparator2
      // 
      this.toolStripSeparator2.Name = "toolStripSeparator2";
      this.toolStripSeparator2.Size = new System.Drawing.Size(6, 26);
      // 
      // toolStripLabel1
      // 
      this.toolStripLabel1.Name = "toolStripLabel1";
      this.toolStripLabel1.Size = new System.Drawing.Size(111, 23);
      this.toolStripLabel1.Text = "Gaussian kernel size";
      // 
      // nudKernelSize
      // 
      this.nudKernelSize.DecimalPlaces = 0;
      this.nudKernelSize.Increment = new decimal(new int[] {
            2,
            0,
            0,
            0});
      this.nudKernelSize.Maximum = new decimal(new int[] {
            401,
            0,
            0,
            0});
      this.nudKernelSize.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
      this.nudKernelSize.Name = "nudKernelSize";
      this.nudKernelSize.Size = new System.Drawing.Size(41, 23);
      this.nudKernelSize.Text = "201";
      this.nudKernelSize.ToolTipText = "This size determines the dimension of each fixation in pixel.\r\nIncreasing this va" +
          "lue smoothes the fixation distribution visualization.";
      this.nudKernelSize.Value = new decimal(new int[] {
            201,
            0,
            0,
            0});
      this.nudKernelSize.ValueChanged += new System.EventHandler(this.nudKernelSize_ValueChanged);
      // 
      // toolStripSeparator4
      // 
      this.toolStripSeparator4.Name = "toolStripSeparator4";
      this.toolStripSeparator4.Size = new System.Drawing.Size(6, 26);
      // 
      // btnHelp
      // 
      this.btnHelp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnHelp.Image = global::Ogama.Properties.Resources.HelpBmp;
      this.btnHelp.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnHelp.Name = "btnHelp";
      this.btnHelp.Size = new System.Drawing.Size(23, 23);
      this.btnHelp.ToolTipText = "Display help for this module.";
      // 
      // tosCalculation
      // 
      this.tosCalculation.DataBindings.Add(new System.Windows.Forms.Binding("Location", global::Ogama.Properties.Settings.Default, "ATMCalculateToolbarLocation", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.tosCalculation.Dock = System.Windows.Forms.DockStyle.None;
      this.tosCalculation.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnStartCalculation,
            this.toolStripSeparator7,
            this.btnAllFix,
            this.btnSpecialFix,
            this.nudFixNumber,
            this.toolStripSeparator9,
            this.toolStripLabel2,
            this.toolStripNumericUpDown1});
      this.tosCalculation.Location = global::Ogama.Properties.Settings.Default.ATMCalculateToolbarLocation;
      this.tosCalculation.Name = "tosCalculation";
      this.tosCalculation.Size = new System.Drawing.Size(192, 25);
      this.tosCalculation.TabIndex = 1;
      // 
      // btnStartCalculation
      // 
      this.btnStartCalculation.Image = global::Ogama.Properties.Resources.CalculatorHS;
      this.btnStartCalculation.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnStartCalculation.Name = "btnStartCalculation";
      this.btnStartCalculation.Size = new System.Drawing.Size(87, 22);
      this.btnStartCalculation.Text = "Recalculate";
      this.btnStartCalculation.ToolTipText = "Starts calculation of attention map.";
      this.btnStartCalculation.Click += new System.EventHandler(this.btnStartCalculation_Click);
      // 
      // toolStripSeparator7
      // 
      this.toolStripSeparator7.Name = "toolStripSeparator7";
      this.toolStripSeparator7.Size = new System.Drawing.Size(6, 25);
      // 
      // btnAllFix
      // 
      this.btnAllFix.Checked = true;
      this.btnAllFix.CheckOnClick = true;
      this.btnAllFix.CheckState = System.Windows.Forms.CheckState.Checked;
      this.btnAllFix.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnAllFix.Image = global::Ogama.Properties.Resources.ATMAll;
      this.btnAllFix.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnAllFix.Name = "btnAllFix";
      this.btnAllFix.Size = new System.Drawing.Size(23, 22);
      this.btnAllFix.ToolTipText = "Use all fixations in calculation.";
      this.btnAllFix.Click += new System.EventHandler(this.btnAllFix_Click);
      // 
      // btnSpecialFix
      // 
      this.btnSpecialFix.CheckOnClick = true;
      this.btnSpecialFix.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnSpecialFix.Image = global::Ogama.Properties.Resources.ATMFirst;
      this.btnSpecialFix.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnSpecialFix.Name = "btnSpecialFix";
      this.btnSpecialFix.Size = new System.Drawing.Size(23, 22);
      this.btnSpecialFix.ToolTipText = "Use only a special fixation in calculation. ";
      this.btnSpecialFix.Click += new System.EventHandler(this.btnSpecialFix_Click);
      // 
      // nudFixNumber
      // 
      this.nudFixNumber.DecimalPlaces = 0;
      this.nudFixNumber.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nudFixNumber.Maximum = new decimal(new int[] {
            100,
            0,
            0,
            0});
      this.nudFixNumber.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nudFixNumber.Name = "nudFixNumber";
      this.nudFixNumber.Size = new System.Drawing.Size(41, 22);
      this.nudFixNumber.Text = "1";
      this.nudFixNumber.ToolTipText = "The number of the fixation in this trial to use in calculation.\r\nFor example, sho" +
          "w only first fixation (1) or second (2).";
      this.nudFixNumber.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
      // 
      // toolStripSeparator9
      // 
      this.toolStripSeparator9.Name = "toolStripSeparator9";
      this.toolStripSeparator9.Size = new System.Drawing.Size(6, 25);
      this.toolStripSeparator9.Visible = false;
      // 
      // toolStripLabel2
      // 
      this.toolStripLabel2.Name = "toolStripLabel2";
      this.toolStripLabel2.Size = new System.Drawing.Size(57, 22);
      this.toolStripLabel2.Text = "Absolute ";
      this.toolStripLabel2.Visible = false;
      // 
      // toolStripNumericUpDown1
      // 
      this.toolStripNumericUpDown1.DecimalPlaces = 0;
      this.toolStripNumericUpDown1.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.toolStripNumericUpDown1.Maximum = new decimal(new int[] {
            100,
            0,
            0,
            0});
      this.toolStripNumericUpDown1.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.toolStripNumericUpDown1.Name = "toolStripNumericUpDown1";
      this.toolStripNumericUpDown1.Size = new System.Drawing.Size(41, 22);
      this.toolStripNumericUpDown1.Text = "0";
      this.toolStripNumericUpDown1.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.toolStripNumericUpDown1.Visible = false;
      // 
      // AttentionMapModule
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(785, 482);
      this.Controls.Add(this.toolStripContainer1);
      this.DataBindings.Add(new System.Windows.Forms.Binding("Location", global::Ogama.Properties.Settings.Default, "AttentionMapModuleLocation", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.DoubleBuffered = true;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.KeyPreview = true;
      this.Location = global::Ogama.Properties.Settings.Default.AttentionMapModuleLocation;
      this.Logo = global::Ogama.Properties.Resources.AttentionMapLogo;
      this.Name = "AttentionMapModule";
      this.Text = "Attention Map Module";
      this.toolTip1.SetToolTip(this, "You can export this image via Edit-Copy or  Edit-SaveImage.");
      this.Load += new System.EventHandler(this.AttentionMapModule_Load);
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
      this.spcPictureGradient.Panel1.ResumeLayout(false);
      this.spcPictureGradient.Panel2.ResumeLayout(false);
      this.spcPictureGradient.ResumeLayout(false);
      this.spcPicAndList.Panel1.ResumeLayout(false);
      this.spcPicAndList.Panel2.ResumeLayout(false);
      this.spcPicAndList.ResumeLayout(false);
      this.toolStripContainer2.BottomToolStripPanel.ResumeLayout(false);
      this.toolStripContainer2.BottomToolStripPanel.PerformLayout();
      this.toolStripContainer2.ContentPanel.ResumeLayout(false);
      this.toolStripContainer2.ResumeLayout(false);
      this.toolStripContainer2.PerformLayout();
      this.toolStrip2.ResumeLayout(false);
      this.toolStrip2.PerformLayout();
      this.pnlCanvas.ResumeLayout(false);
      this.pnlPicture.ResumeLayout(false);
      this.tosTrial.ResumeLayout(false);
      this.tosTrial.PerformLayout();
      this.tosCalculation.ResumeLayout(false);
      this.tosCalculation.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.ComponentModel.BackgroundWorker bgwCalcMap;
    private System.Windows.Forms.ToolTip toolTip1;
    private System.Windows.Forms.ToolStripContainer toolStripContainer1;
    private System.Windows.Forms.ToolStrip tosTrial;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    private System.Windows.Forms.ToolStripButton btnHelp;
    private AttentionMapPicture attentionMapPicture;
    private GradientTypeEditorUI gradientControl;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
    private System.Windows.Forms.ToolStripDropDownButton mnuGradients;
    private System.Windows.Forms.ToolStripMenuItem mnuRainbow;
    private System.Windows.Forms.ToolStripMenuItem mnuBlackMask;
    private System.Windows.Forms.ToolStripMenuItem mnuTrafficLight;
    private System.Windows.Forms.ToolStripMenuItem mnuShowGradientBuilder;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
    private System.Windows.Forms.SplitContainer spcPictureGradient;
    private System.Windows.Forms.SplitContainer spcPicAndList;
    private System.Windows.Forms.ToolStripComboBox cbbTrial;
    private OgamaControls.CheckboxTreeView trvSubjects;
    private System.Windows.Forms.ImageList imlTreeViewSubjects;
    private System.Windows.Forms.ToolStripMenuItem mnuCustomGradient;
    private System.Windows.Forms.Panel pnlCanvas;
    private System.Windows.Forms.Panel pnlPicture;
    private System.Windows.Forms.ToolStripButton btnEye;
    private System.Windows.Forms.ToolStripButton btnMouse;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
    private System.Windows.Forms.ToolStrip tosCalculation;
    private System.Windows.Forms.ToolStripButton btnStartCalculation;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
    private System.Windows.Forms.ToolStripButton btnAllFix;
    private System.Windows.Forms.ToolStripButton btnSpecialFix;
    
    private ToolStripNumericUpDown nudFixNumber;
    private ToolStripNumericUpDown nudKernelSize;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    private System.Windows.Forms.ToolStripLabel toolStripLabel1;
    private System.Windows.Forms.ToolStrip toolStrip2;
    private System.Windows.Forms.ToolStripButton btnSeekNextSlide;
    private System.Windows.Forms.ToolStripButton btnSeekPreviousSlide;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
    private TrialTimeLine trialTimeLine;
    private System.Windows.Forms.ToolStripButton btnMouseClicks;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
    private System.Windows.Forms.ToolStripButton btnWeight;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
    private System.Windows.Forms.ToolStripLabel toolStripLabel2;
    private ToolStripNumericUpDown toolStripNumericUpDown1;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
    private ToolStripTrackBar trbZoom;
    private System.Windows.Forms.ToolStripContainer toolStripContainer2;

  }
}