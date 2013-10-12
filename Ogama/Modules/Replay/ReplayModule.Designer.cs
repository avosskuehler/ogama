namespace Ogama.Modules.Replay
{
  using Ogama.Modules.Common.Controls;
  using Ogama.Modules.Common.CustomEventArgs;

  using VectorGraphics.Tools.CustomEventArgs;

  partial class ReplayModule
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReplayModule));
      this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
      this.toolStrip5 = new System.Windows.Forms.ToolStrip();
      this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
      this.btnStart = new System.Windows.Forms.ToolStripButton();
      this.btnStartContinous = new System.Windows.Forms.ToolStripButton();
      this.btnPause = new System.Windows.Forms.ToolStripButton();
      this.btnStop = new System.Windows.Forms.ToolStripButton();
      this.btnRewind = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator12 = new System.Windows.Forms.ToolStripSeparator();
      this.btnSeekToNextEvent = new System.Windows.Forms.ToolStripButton();
      this.btnSeekToPreviousEvent = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator14 = new System.Windows.Forms.ToolStripSeparator();
      this.btnSeekToNextMarker = new System.Windows.Forms.ToolStripButton();
      this.btnSeekToPreviousMarker = new System.Windows.Forms.ToolStripButton();
      this.btnAddMarker = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator13 = new System.Windows.Forms.ToolStripSeparator();
      this.trialTimeLine = new TrialTimeLine(this.components);
      this.toolStripSeparator16 = new System.Windows.Forms.ToolStripSeparator();
      this.trbZoom = new OgamaControls.ToolStripTrackBar();
      this.pnlCanvas = new System.Windows.Forms.Panel();
      this.pnlPicture = new System.Windows.Forms.Panel();
      this.replayPicture = new Ogama.Modules.Replay.ReplayPicture();
      this.toolStrip2 = new System.Windows.Forms.ToolStrip();
      this.toolStripButton5 = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
      this.btnGazeCursorTyp = new System.Windows.Forms.ToolStripButton();
      this.btnGazeCursorPenStyle = new System.Windows.Forms.ToolStripButton();
      this.btnGazePathPenStyle = new System.Windows.Forms.ToolStripButton();
      this.btnGazeFixPenStyle = new System.Windows.Forms.ToolStripButton();
      this.btnGazeFixConPenStyle = new System.Windows.Forms.ToolStripButton();
      this.toolStrip4 = new System.Windows.Forms.ToolStrip();
      this.toolStripButton6 = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
      this.btnMouseCursorTyp = new System.Windows.Forms.ToolStripButton();
      this.btnMouseCursorPenStyle = new System.Windows.Forms.ToolStripButton();
      this.btnMousePathPenStyle = new System.Windows.Forms.ToolStripButton();
      this.btnMouseFixPenStyle = new System.Windows.Forms.ToolStripButton();
      this.btnMouseFixConPenStyle = new System.Windows.Forms.ToolStripButton();
      this.tosTrial = new System.Windows.Forms.ToolStrip();
      this.cbbSubject = new System.Windows.Forms.ToolStripComboBox();
      this.cbbTrial = new System.Windows.Forms.ToolStripComboBox();
      this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
      this.cbbSpeed = new System.Windows.Forms.ToolStripComboBox();
      this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
      this.btnEnableAudio = new System.Windows.Forms.ToolStripButton();
      this.btnShowUsercam = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
      this.btnShowVisiblePartOfScreen = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator17 = new System.Windows.Forms.ToolStripSeparator();
      this.btnRecord = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
      this.btnHelp = new System.Windows.Forms.ToolStripButton();
      this.tosGaze = new System.Windows.Forms.ToolStrip();
      this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
      this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
      this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
      this.btnGazeModeCursor = new System.Windows.Forms.ToolStripButton();
      this.btnGazeModePath = new System.Windows.Forms.ToolStripButton();
      this.btnGazeModeFix = new System.Windows.Forms.ToolStripButton();
      this.btnGazeModeFixCon = new System.Windows.Forms.ToolStripButton();
      this.btnGazeModeSpot = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
      this.nudGazeFixDiameterDiv = new OgamaControls.ToolStripNumericUpDown();
      this.btnGazeCutPath = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
      this.btnGazeBlinks = new System.Windows.Forms.ToolStripButton();
      this.cbbNumFixationsToShow = new System.Windows.Forms.ToolStripComboBox();
      this.tosMouse = new System.Windows.Forms.ToolStrip();
      this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
      this.toolStripLabel4 = new System.Windows.Forms.ToolStripLabel();
      this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
      this.btnMouseModeCursor = new System.Windows.Forms.ToolStripButton();
      this.btnMouseModePath = new System.Windows.Forms.ToolStripButton();
      this.btnMouseModeFix = new System.Windows.Forms.ToolStripButton();
      this.btnMouseModeFixCon = new System.Windows.Forms.ToolStripButton();
      this.btnMouseModeSpot = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
      this.nudMouseFixDiameterDiv = new OgamaControls.ToolStripNumericUpDown();
      this.btnMouseCutPath = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator15 = new System.Windows.Forms.ToolStripSeparator();
      this.btnMouseModeClicks = new System.Windows.Forms.ToolStripButton();
      this.sfdVideo = new System.Windows.Forms.SaveFileDialog();
      this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
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
      this.toolStripContainer1.BottomToolStripPanel.SuspendLayout();
      this.toolStripContainer1.ContentPanel.SuspendLayout();
      this.toolStripContainer1.RightToolStripPanel.SuspendLayout();
      this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
      this.toolStripContainer1.SuspendLayout();
      this.toolStrip5.SuspendLayout();
      this.pnlCanvas.SuspendLayout();
      this.pnlPicture.SuspendLayout();
      this.toolStrip2.SuspendLayout();
      this.toolStrip4.SuspendLayout();
      this.tosTrial.SuspendLayout();
      this.tosGaze.SuspendLayout();
      this.tosMouse.SuspendLayout();
      this.SuspendLayout();
      // 
      // toolStripContainer1
      // 
      // 
      // toolStripContainer1.BottomToolStripPanel
      // 
      this.toolStripContainer1.BottomToolStripPanel.Controls.Add(this.toolStrip5);
      // 
      // toolStripContainer1.ContentPanel
      // 
      this.toolStripContainer1.ContentPanel.Controls.Add(this.pnlCanvas);
      this.toolStripContainer1.ContentPanel.Margin = new System.Windows.Forms.Padding(0);
      this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(661, 283);
      this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
      this.toolStripContainer1.Margin = new System.Windows.Forms.Padding(0);
      this.toolStripContainer1.Name = "toolStripContainer1";
      // 
      // toolStripContainer1.RightToolStripPanel
      // 
      this.toolStripContainer1.RightToolStripPanel.Controls.Add(this.toolStrip2);
      this.toolStripContainer1.RightToolStripPanel.Controls.Add(this.toolStrip4);
      this.toolStripContainer1.Size = new System.Drawing.Size(685, 386);
      this.toolStripContainer1.TabIndex = 4;
      this.toolStripContainer1.Text = "toolStripContainer1";
      // 
      // toolStripContainer1.TopToolStripPanel
      // 
      this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.tosTrial);
      this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.tosGaze);
      this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.tosMouse);
      // 
      // toolStrip5
      // 
      this.toolStrip5.Dock = System.Windows.Forms.DockStyle.None;
      this.toolStrip5.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
      this.toolStrip5.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel3,
            this.btnStart,
            this.btnStartContinous,
            this.btnPause,
            this.btnStop,
            this.btnRewind,
            this.toolStripSeparator12,
            this.btnSeekToNextEvent,
            this.btnSeekToPreviousEvent,
            this.toolStripSeparator14,
            this.btnSeekToNextMarker,
            this.btnSeekToPreviousMarker,
            this.btnAddMarker,
            this.toolStripSeparator13,
            this.trialTimeLine,
            this.toolStripSeparator16,
            this.trbZoom});
      this.toolStrip5.Location = new System.Drawing.Point(0, 0);
      this.toolStrip5.Name = "toolStrip5";
      this.toolStrip5.Size = new System.Drawing.Size(685, 26);
      this.toolStrip5.Stretch = true;
      this.toolStrip5.TabIndex = 2;
      this.toolStrip5.Text = "toolStrip5";
      // 
      // toolStripLabel3
      // 
      this.toolStripLabel3.Margin = new System.Windows.Forms.Padding(3, 1, 0, 2);
      this.toolStripLabel3.Name = "toolStripLabel3";
      this.toolStripLabel3.Size = new System.Drawing.Size(88, 23);
      this.toolStripLabel3.Text = "Replay controls";
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
      // btnStartContinous
      // 
      this.btnStartContinous.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnStartContinous.Image = ((System.Drawing.Image)(resources.GetObject("btnStartContinous.Image")));
      this.btnStartContinous.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnStartContinous.Name = "btnStartContinous";
      this.btnStartContinous.Size = new System.Drawing.Size(23, 23);
      this.btnStartContinous.Text = "Play trials continously";
      this.btnStartContinous.Click += new System.EventHandler(this.btnStartContinous_Click);
      // 
      // btnPause
      // 
      this.btnPause.CheckOnClick = true;
      this.btnPause.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnPause.Enabled = false;
      this.btnPause.Image = ((System.Drawing.Image)(resources.GetObject("btnPause.Image")));
      this.btnPause.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnPause.Name = "btnPause";
      this.btnPause.Size = new System.Drawing.Size(23, 23);
      this.btnPause.ToolTipText = "Pause/Continue playing";
      this.btnPause.Visible = false;
      this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
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
      // toolStripSeparator12
      // 
      this.toolStripSeparator12.Name = "toolStripSeparator12";
      this.toolStripSeparator12.Size = new System.Drawing.Size(6, 26);
      // 
      // btnSeekToNextEvent
      // 
      this.btnSeekToNextEvent.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnSeekToNextEvent.Image = global::Ogama.Properties.Resources.DataContainer_MoveLastHS;
      this.btnSeekToNextEvent.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnSeekToNextEvent.Name = "btnSeekToNextEvent";
      this.btnSeekToNextEvent.Size = new System.Drawing.Size(23, 23);
      this.btnSeekToNextEvent.Text = "Seek to next event.";
      this.btnSeekToNextEvent.Click += new System.EventHandler(this.btnSeekToNextEvent_Click);
      // 
      // btnSeekToPreviousEvent
      // 
      this.btnSeekToPreviousEvent.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnSeekToPreviousEvent.Image = global::Ogama.Properties.Resources.DataContainer_MoveFirstHS;
      this.btnSeekToPreviousEvent.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnSeekToPreviousEvent.Name = "btnSeekToPreviousEvent";
      this.btnSeekToPreviousEvent.Size = new System.Drawing.Size(23, 23);
      this.btnSeekToPreviousEvent.Text = "Seek to previous event.";
      this.btnSeekToPreviousEvent.Click += new System.EventHandler(this.btnSeekToPreviousEvent_Click);
      // 
      // toolStripSeparator14
      // 
      this.toolStripSeparator14.Name = "toolStripSeparator14";
      this.toolStripSeparator14.Size = new System.Drawing.Size(6, 26);
      // 
      // btnSeekToNextMarker
      // 
      this.btnSeekToNextMarker.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnSeekToNextMarker.Image = global::Ogama.Properties.Resources.MoveNext;
      this.btnSeekToNextMarker.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnSeekToNextMarker.Name = "btnSeekToNextMarker";
      this.btnSeekToNextMarker.Size = new System.Drawing.Size(23, 23);
      this.btnSeekToNextMarker.Text = "Seek to next marker";
      this.btnSeekToNextMarker.Click += new System.EventHandler(this.btnSeekToNextMarker_Click);
      // 
      // btnSeekToPreviousMarker
      // 
      this.btnSeekToPreviousMarker.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnSeekToPreviousMarker.Image = global::Ogama.Properties.Resources.MoveFirst;
      this.btnSeekToPreviousMarker.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnSeekToPreviousMarker.Name = "btnSeekToPreviousMarker";
      this.btnSeekToPreviousMarker.Size = new System.Drawing.Size(23, 23);
      this.btnSeekToPreviousMarker.Text = "Seek to previous marker";
      this.btnSeekToPreviousMarker.Click += new System.EventHandler(this.btnSeekToPreviousMarker_Click);
      // 
      // btnAddMarker
      // 
      this.btnAddMarker.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnAddMarker.Image = global::Ogama.Properties.Resources.Event;
      this.btnAddMarker.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnAddMarker.Name = "btnAddMarker";
      this.btnAddMarker.Size = new System.Drawing.Size(23, 23);
      this.btnAddMarker.Text = "Add Marker";
      this.btnAddMarker.Click += new System.EventHandler(this.btnAddMarker_Click);
      // 
      // toolStripSeparator13
      // 
      this.toolStripSeparator13.Name = "toolStripSeparator13";
      this.toolStripSeparator13.Size = new System.Drawing.Size(6, 26);
      // 
      // trialTimeLine
      // 
      this.trialTimeLine.Duration = 0;
      this.trialTimeLine.Name = "trialTimeLine";
      this.trialTimeLine.Size = new System.Drawing.Size(228, 23);
      this.trialTimeLine.MarkerDeleted += new OgamaControls.TimeLine.MarkerPositionChangedEventHandler(this.TimeSlider_MarkerDeleted);
      this.trialTimeLine.CaretMovingStarted += new System.EventHandler(this.TimeSlider_CaretMovingStarted);
      this.trialTimeLine.MarkerPositionChanged += new OgamaControls.TimeLine.MarkerPositionChangedEventHandler(this.TimeSlider_MarkerPositionChanged);
      this.trialTimeLine.CaretValueChanged += new OgamaControls.TimeLine.PositionValueChangedEventHandler(this.TimeSlider_CaretValueChanged);
      // 
      // toolStripSeparator16
      // 
      this.toolStripSeparator16.Name = "toolStripSeparator16";
      this.toolStripSeparator16.Size = new System.Drawing.Size(6, 26);
      // 
      // trbZoom
      // 
      this.trbZoom.Maximum = 100;
      this.trbZoom.Minimum = 1;
      this.trbZoom.Name = "trbZoom";
      this.trbZoom.Size = new System.Drawing.Size(80, 23);
      this.trbZoom.Text = "toolStripTrackBar1";
      this.trbZoom.TickFrequency = 1;
      this.trbZoom.TickStyle = System.Windows.Forms.TickStyle.None;
      this.trbZoom.ToolTipText = "Zoom, right-click for autozoom";
      this.trbZoom.Value = 10;
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
      this.pnlCanvas.Size = new System.Drawing.Size(661, 283);
      this.pnlCanvas.TabIndex = 2;
      // 
      // pnlPicture
      // 
      this.pnlPicture.Controls.Add(this.replayPicture);
      this.pnlPicture.Location = new System.Drawing.Point(307, 83);
      this.pnlPicture.Name = "pnlPicture";
      this.pnlPicture.Size = new System.Drawing.Size(200, 100);
      this.pnlPicture.TabIndex = 1;
      // 
      // replayPicture
      // 
      this.replayPicture.AnimationInterval = 40;
      this.replayPicture.BackColor = System.Drawing.Color.Black;
      this.replayPicture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
      this.replayPicture.InvalidateInterval = 500;
      this.replayPicture.Location = new System.Drawing.Point(0, 0);
      this.replayPicture.Margin = new System.Windows.Forms.Padding(0);
      this.replayPicture.Name = "replayPicture";
      this.replayPicture.Size = new System.Drawing.Size(200, 100);
      this.replayPicture.TabIndex = 0;
      this.replayPicture.TabStop = false;
      this.replayPicture.ZoomFactor = 0F;
      this.replayPicture.TrialEventIDFound += new TrialEventIDFoundEventHandler(this.replayPicture_TrialEventIDFound);
      this.replayPicture.TrialSequenceChanged += new TrialSequenceChangedEventHandler(this.replayPicture_TrialSequenceChanged);
      this.replayPicture.Progress += new ProgressEventHandler(this.replayPicture_Progress);
      // 
      // toolStrip2
      // 
      this.toolStrip2.Dock = System.Windows.Forms.DockStyle.None;
      this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton5,
            this.toolStripSeparator6,
            this.btnGazeCursorTyp,
            this.btnGazeCursorPenStyle,
            this.btnGazePathPenStyle,
            this.btnGazeFixPenStyle,
            this.btnGazeFixConPenStyle});
      this.toolStrip2.Location = new System.Drawing.Point(0, 3);
      this.toolStrip2.Name = "toolStrip2";
      this.toolStrip2.Size = new System.Drawing.Size(24, 155);
      this.toolStrip2.TabIndex = 5;
      // 
      // toolStripButton5
      // 
      this.toolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.toolStripButton5.Enabled = false;
      this.toolStripButton5.Image = global::Ogama.Properties.Resources.Eye;
      this.toolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripButton5.Name = "toolStripButton5";
      this.toolStripButton5.Size = new System.Drawing.Size(22, 20);
      this.toolStripButton5.ToolTipText = "Change gaze pen styles.";
      // 
      // toolStripSeparator6
      // 
      this.toolStripSeparator6.Name = "toolStripSeparator6";
      this.toolStripSeparator6.Size = new System.Drawing.Size(22, 6);
      // 
      // btnGazeCursorTyp
      // 
      this.btnGazeCursorTyp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnGazeCursorTyp.Image = global::Ogama.Properties.Resources.RPLCursorTyp;
      this.btnGazeCursorTyp.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnGazeCursorTyp.Name = "btnGazeCursorTyp";
      this.btnGazeCursorTyp.Size = new System.Drawing.Size(22, 20);
      this.btnGazeCursorTyp.ToolTipText = "Select eye cursor shape and size.";
      this.btnGazeCursorTyp.Click += new System.EventHandler(this.btnGazeCursorTyp_Click);
      // 
      // btnGazeCursorPenStyle
      // 
      this.btnGazeCursorPenStyle.AutoSize = false;
      this.btnGazeCursorPenStyle.BackColor = System.Drawing.SystemColors.Control;
      this.btnGazeCursorPenStyle.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnGazeCursorPenStyle.Image = global::Ogama.Properties.Resources.RPLCursorPen;
      this.btnGazeCursorPenStyle.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnGazeCursorPenStyle.Name = "btnGazeCursorPenStyle";
      this.btnGazeCursorPenStyle.Size = new System.Drawing.Size(20, 20);
      this.btnGazeCursorPenStyle.ToolTipText = "select gaze cursor pen style";
      this.btnGazeCursorPenStyle.Click += new System.EventHandler(this.btnGazeCursorPenStyle_Click);
      // 
      // btnGazePathPenStyle
      // 
      this.btnGazePathPenStyle.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnGazePathPenStyle.Image = global::Ogama.Properties.Resources.RPLPathPen;
      this.btnGazePathPenStyle.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnGazePathPenStyle.Name = "btnGazePathPenStyle";
      this.btnGazePathPenStyle.Size = new System.Drawing.Size(22, 20);
      this.btnGazePathPenStyle.ToolTipText = "Select gaze path pen style";
      this.btnGazePathPenStyle.Click += new System.EventHandler(this.btnGazePathPenStyle_Click);
      // 
      // btnGazeFixPenStyle
      // 
      this.btnGazeFixPenStyle.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnGazeFixPenStyle.Image = global::Ogama.Properties.Resources.RPLFixPen;
      this.btnGazeFixPenStyle.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnGazeFixPenStyle.Name = "btnGazeFixPenStyle";
      this.btnGazeFixPenStyle.Size = new System.Drawing.Size(22, 20);
      this.btnGazeFixPenStyle.ToolTipText = "Select gaze fixations pen style";
      this.btnGazeFixPenStyle.Click += new System.EventHandler(this.btnGazeFixPenStyle_Click);
      // 
      // btnGazeFixConPenStyle
      // 
      this.btnGazeFixConPenStyle.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnGazeFixConPenStyle.Image = global::Ogama.Properties.Resources.RPLFixConPen;
      this.btnGazeFixConPenStyle.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnGazeFixConPenStyle.Name = "btnGazeFixConPenStyle";
      this.btnGazeFixConPenStyle.Size = new System.Drawing.Size(22, 20);
      this.btnGazeFixConPenStyle.ToolTipText = "Select gaze fixation connections pen style";
      this.btnGazeFixConPenStyle.Click += new System.EventHandler(this.btnGazeFixConPenStyle_Click);
      // 
      // toolStrip4
      // 
      this.toolStrip4.Dock = System.Windows.Forms.DockStyle.None;
      this.toolStrip4.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton6,
            this.toolStripSeparator8,
            this.btnMouseCursorTyp,
            this.btnMouseCursorPenStyle,
            this.btnMousePathPenStyle,
            this.btnMouseFixPenStyle,
            this.btnMouseFixConPenStyle});
      this.toolStrip4.Location = new System.Drawing.Point(0, 158);
      this.toolStrip4.Name = "toolStrip4";
      this.toolStrip4.Size = new System.Drawing.Size(24, 125);
      this.toolStrip4.TabIndex = 6;
      // 
      // toolStripButton6
      // 
      this.toolStripButton6.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.toolStripButton6.Enabled = false;
      this.toolStripButton6.Image = global::Ogama.Properties.Resources.Mouse;
      this.toolStripButton6.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripButton6.Name = "toolStripButton6";
      this.toolStripButton6.Size = new System.Drawing.Size(22, 20);
      this.toolStripButton6.ToolTipText = "Change mouse pen styles.";
      // 
      // toolStripSeparator8
      // 
      this.toolStripSeparator8.Name = "toolStripSeparator8";
      this.toolStripSeparator8.Size = new System.Drawing.Size(22, 6);
      // 
      // btnMouseCursorTyp
      // 
      this.btnMouseCursorTyp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnMouseCursorTyp.Image = global::Ogama.Properties.Resources.RPLCursorTyp;
      this.btnMouseCursorTyp.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnMouseCursorTyp.Name = "btnMouseCursorTyp";
      this.btnMouseCursorTyp.Size = new System.Drawing.Size(22, 20);
      this.btnMouseCursorTyp.ToolTipText = "Select mouse cursor shape and size.";
      this.btnMouseCursorTyp.Click += new System.EventHandler(this.btnMouseCursorTyp_Click);
      // 
      // btnMouseCursorPenStyle
      // 
      this.btnMouseCursorPenStyle.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnMouseCursorPenStyle.Image = global::Ogama.Properties.Resources.RPLCursorPen;
      this.btnMouseCursorPenStyle.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnMouseCursorPenStyle.Name = "btnMouseCursorPenStyle";
      this.btnMouseCursorPenStyle.Size = new System.Drawing.Size(22, 20);
      this.btnMouseCursorPenStyle.ToolTipText = "Select mouse cursor pen style";
      this.btnMouseCursorPenStyle.Click += new System.EventHandler(this.btnMouseCursorPenStyle_Click);
      // 
      // btnMousePathPenStyle
      // 
      this.btnMousePathPenStyle.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnMousePathPenStyle.Image = global::Ogama.Properties.Resources.RPLPathPen;
      this.btnMousePathPenStyle.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnMousePathPenStyle.Name = "btnMousePathPenStyle";
      this.btnMousePathPenStyle.Size = new System.Drawing.Size(22, 20);
      this.btnMousePathPenStyle.ToolTipText = "Select mouse path pen style";
      this.btnMousePathPenStyle.Click += new System.EventHandler(this.btnMousePathPenStyle_Click);
      // 
      // btnMouseFixPenStyle
      // 
      this.btnMouseFixPenStyle.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnMouseFixPenStyle.Image = global::Ogama.Properties.Resources.RPLFixPen;
      this.btnMouseFixPenStyle.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnMouseFixPenStyle.Name = "btnMouseFixPenStyle";
      this.btnMouseFixPenStyle.Size = new System.Drawing.Size(22, 20);
      this.btnMouseFixPenStyle.ToolTipText = "Select mouse fixations pen style";
      this.btnMouseFixPenStyle.Click += new System.EventHandler(this.btnMouseFixPenStyle_Click);
      // 
      // btnMouseFixConPenStyle
      // 
      this.btnMouseFixConPenStyle.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnMouseFixConPenStyle.Image = global::Ogama.Properties.Resources.RPLFixConPen;
      this.btnMouseFixConPenStyle.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnMouseFixConPenStyle.Name = "btnMouseFixConPenStyle";
      this.btnMouseFixConPenStyle.Size = new System.Drawing.Size(22, 20);
      this.btnMouseFixConPenStyle.ToolTipText = "Select mouse fixation connections pen style";
      this.btnMouseFixConPenStyle.Click += new System.EventHandler(this.btnMouseFixConPenStyle_Click);
      // 
      // tosTrial
      // 
      this.tosTrial.AllowItemReorder = true;
      this.tosTrial.DataBindings.Add(new System.Windows.Forms.Binding("Location", global::Ogama.Properties.Settings.Default, "RPLTrialToolbarLocation", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.tosTrial.Dock = System.Windows.Forms.DockStyle.None;
      this.tosTrial.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cbbSubject,
            this.cbbTrial,
            this.toolStripSeparator1,
            this.toolStripLabel1,
            this.cbbSpeed,
            this.toolStripSeparator7,
            this.btnEnableAudio,
            this.btnShowUsercam,
            this.toolStripSeparator11,
            this.btnShowVisiblePartOfScreen,
            this.toolStripSeparator17,
            this.btnRecord,
            this.toolStripSeparator2,
            this.btnHelp});
      this.tosTrial.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
      this.tosTrial.Location = global::Ogama.Properties.Settings.Default.RPLTrialToolbarLocation;
      this.tosTrial.Name = "tosTrial";
      this.tosTrial.Size = new System.Drawing.Size(619, 25);
      this.tosTrial.TabIndex = 0;
      // 
      // cbbSubject
      // 
      this.cbbSubject.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbbSubject.Name = "cbbSubject";
      this.cbbSubject.Size = new System.Drawing.Size(100, 25);
      this.cbbSubject.ToolTipText = "Select subject to display.";
      // 
      // cbbTrial
      // 
      this.cbbTrial.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbbTrial.Name = "cbbTrial";
      this.cbbTrial.Size = new System.Drawing.Size(200, 25);
      this.cbbTrial.ToolTipText = "Select the trial to display.";
      // 
      // toolStripSeparator1
      // 
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
      // 
      // toolStripLabel1
      // 
      this.toolStripLabel1.Name = "toolStripLabel1";
      this.toolStripLabel1.Size = new System.Drawing.Size(42, 22);
      this.toolStripLabel1.Text = "Speed:";
      // 
      // cbbSpeed
      // 
      this.cbbSpeed.AutoSize = false;
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
      this.cbbSpeed.Name = "cbbSpeed";
      this.cbbSpeed.Size = new System.Drawing.Size(45, 23);
      this.cbbSpeed.Text = global::Ogama.Properties.Settings.Default.ReplaySpeed;
      this.cbbSpeed.ToolTipText = "Select replay speed. \r\n< 1x is slow motion";
      // 
      // toolStripSeparator7
      // 
      this.toolStripSeparator7.Name = "toolStripSeparator7";
      this.toolStripSeparator7.Size = new System.Drawing.Size(6, 25);
      // 
      // btnEnableAudio
      // 
      this.btnEnableAudio.Checked = true;
      this.btnEnableAudio.CheckOnClick = true;
      this.btnEnableAudio.CheckState = System.Windows.Forms.CheckState.Checked;
      this.btnEnableAudio.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnEnableAudio.Image = global::Ogama.Properties.Resources.sound16;
      this.btnEnableAudio.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnEnableAudio.Name = "btnEnableAudio";
      this.btnEnableAudio.Size = new System.Drawing.Size(23, 22);
      this.btnEnableAudio.Text = "Enable audio";
      this.btnEnableAudio.ToolTipText = "Enable or disable audio";
      // 
      // btnShowUsercam
      // 
      this.btnShowUsercam.Checked = true;
      this.btnShowUsercam.CheckOnClick = true;
      this.btnShowUsercam.CheckState = System.Windows.Forms.CheckState.Checked;
      this.btnShowUsercam.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnShowUsercam.Image = global::Ogama.Properties.Resources.UserVideo;
      this.btnShowUsercam.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnShowUsercam.Name = "btnShowUsercam";
      this.btnShowUsercam.Size = new System.Drawing.Size(23, 22);
      this.btnShowUsercam.Text = "Show user video";
      this.btnShowUsercam.ToolTipText = "Show or hide user video";
      this.btnShowUsercam.Click += new System.EventHandler(this.btnShowUsercam_Click);
      // 
      // toolStripSeparator11
      // 
      this.toolStripSeparator11.Name = "toolStripSeparator11";
      this.toolStripSeparator11.Size = new System.Drawing.Size(6, 25);
      // 
      // btnShowVisiblePartOfScreen
      // 
      this.btnShowVisiblePartOfScreen.CheckOnClick = true;
      this.btnShowVisiblePartOfScreen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnShowVisiblePartOfScreen.Image = ((System.Drawing.Image)(resources.GetObject("btnShowVisiblePartOfScreen.Image")));
      this.btnShowVisiblePartOfScreen.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnShowVisiblePartOfScreen.Name = "btnShowVisiblePartOfScreen";
      this.btnShowVisiblePartOfScreen.Size = new System.Drawing.Size(23, 22);
      this.btnShowVisiblePartOfScreen.Text = "Show visible part of screen";
      this.btnShowVisiblePartOfScreen.Click += new System.EventHandler(this.btnShowVisiblePartOfScreen_Click);
      // 
      // toolStripSeparator17
      // 
      this.toolStripSeparator17.Name = "toolStripSeparator17";
      this.toolStripSeparator17.Size = new System.Drawing.Size(6, 25);
      // 
      // btnRecord
      // 
      this.btnRecord.Image = global::Ogama.Properties.Resources.video;
      this.btnRecord.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnRecord.Name = "btnRecord";
      this.btnRecord.Size = new System.Drawing.Size(92, 22);
      this.btnRecord.Text = "Export to avi";
      this.btnRecord.ToolTipText = "Renders this trial with the current display settings\r\ninto an compressed avi vide" +
          "o file.";
      this.btnRecord.Click += new System.EventHandler(this.btnRecord_Click);
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
      this.btnHelp.ToolTipText = "Displays help for this module";
      // 
      // tosGaze
      // 
      this.tosGaze.DataBindings.Add(new System.Windows.Forms.Binding("Location", global::Ogama.Properties.Settings.Default, "RPLGazeToolbarLocation", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.tosGaze.Dock = System.Windows.Forms.DockStyle.None;
      this.tosGaze.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton4,
            this.toolStripLabel2,
            this.toolStripSeparator5,
            this.btnGazeModeCursor,
            this.btnGazeModePath,
            this.btnGazeModeFix,
            this.btnGazeModeFixCon,
            this.btnGazeModeSpot,
            this.toolStripSeparator9,
            this.nudGazeFixDiameterDiv,
            this.btnGazeCutPath,
            this.toolStripSeparator10,
            this.btnGazeBlinks,
            this.cbbNumFixationsToShow});
      this.tosGaze.Location = global::Ogama.Properties.Settings.Default.RPLGazeToolbarLocation;
      this.tosGaze.Name = "tosGaze";
      this.tosGaze.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
      this.tosGaze.Size = new System.Drawing.Size(412, 26);
      this.tosGaze.TabIndex = 4;
      // 
      // toolStripButton4
      // 
      this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.toolStripButton4.Enabled = false;
      this.toolStripButton4.Image = global::Ogama.Properties.Resources.Eye;
      this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripButton4.Name = "toolStripButton4";
      this.toolStripButton4.Size = new System.Drawing.Size(23, 23);
      // 
      // toolStripLabel2
      // 
      this.toolStripLabel2.Name = "toolStripLabel2";
      this.toolStripLabel2.Size = new System.Drawing.Size(111, 23);
      this.toolStripLabel2.Text = "Gaze display modes";
      this.toolStripLabel2.ToolTipText = "Check or uncheck the gaze display modes.";
      // 
      // toolStripSeparator5
      // 
      this.toolStripSeparator5.Name = "toolStripSeparator5";
      this.toolStripSeparator5.Size = new System.Drawing.Size(6, 26);
      // 
      // btnGazeModeCursor
      // 
      this.btnGazeModeCursor.Checked = global::Ogama.Properties.Settings.Default.GazeModeCursor;
      this.btnGazeModeCursor.CheckOnClick = true;
      this.btnGazeModeCursor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnGazeModeCursor.Image = global::Ogama.Properties.Resources.RPLCursor;
      this.btnGazeModeCursor.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnGazeModeCursor.Name = "btnGazeModeCursor";
      this.btnGazeModeCursor.Size = new System.Drawing.Size(23, 23);
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
      this.btnGazeModePath.Size = new System.Drawing.Size(23, 23);
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
      this.btnGazeModeFix.Size = new System.Drawing.Size(23, 23);
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
      this.btnGazeModeFixCon.Size = new System.Drawing.Size(23, 23);
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
      this.btnGazeModeSpot.Size = new System.Drawing.Size(23, 23);
      this.btnGazeModeSpot.ToolTipText = "Display Gaze as Spotlight";
      this.btnGazeModeSpot.Click += new System.EventHandler(this.btnGazeModeSpot_Click);
      // 
      // toolStripSeparator9
      // 
      this.toolStripSeparator9.Name = "toolStripSeparator9";
      this.toolStripSeparator9.Size = new System.Drawing.Size(6, 26);
      // 
      // nudGazeFixDiameterDiv
      // 
      this.nudGazeFixDiameterDiv.DecimalPlaces = 1;
      this.nudGazeFixDiameterDiv.Increment = new decimal(new int[] {
            1,
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
      this.nudGazeFixDiameterDiv.ToolTipText = "Increase this value to reduce gaze fixation diameters.";
      this.nudGazeFixDiameterDiv.Value = new decimal(new int[] {
            20,
            0,
            0,
            65536});
      this.nudGazeFixDiameterDiv.ValueChanged += new System.EventHandler(this.nudGazeFixDivameterDiv_ValueChanged);
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
      this.btnGazeCutPath.Size = new System.Drawing.Size(23, 23);
      this.btnGazeCutPath.ToolTipText = "Reduce the amount of displayed gaze data.";
      this.btnGazeCutPath.Click += new System.EventHandler(this.btnGazeCutPath_Click);
      // 
      // toolStripSeparator10
      // 
      this.toolStripSeparator10.Name = "toolStripSeparator10";
      this.toolStripSeparator10.Size = new System.Drawing.Size(6, 26);
      // 
      // btnGazeBlinks
      // 
      this.btnGazeBlinks.Checked = global::Ogama.Properties.Settings.Default.GazeModeBlinks;
      this.btnGazeBlinks.CheckOnClick = true;
      this.btnGazeBlinks.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnGazeBlinks.Image = global::Ogama.Properties.Resources.RPLBlink;
      this.btnGazeBlinks.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnGazeBlinks.Name = "btnGazeBlinks";
      this.btnGazeBlinks.Size = new System.Drawing.Size(23, 23);
      this.btnGazeBlinks.ToolTipText = "Show blinks during replay.";
      this.btnGazeBlinks.Click += new System.EventHandler(this.btnGazeBlinks_Click);
      // 
      // cbbNumFixationsToShow
      // 
      this.cbbNumFixationsToShow.AutoSize = false;
      this.cbbNumFixationsToShow.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbbNumFixationsToShow.Items.AddRange(new object[] {
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9"});
      this.cbbNumFixationsToShow.Name = "cbbNumFixationsToShow";
      this.cbbNumFixationsToShow.Size = new System.Drawing.Size(35, 23);
      this.cbbNumFixationsToShow.Sorted = true;
      this.cbbNumFixationsToShow.ToolTipText = "Maximum number of fixations to show, when in reduced data mode.";
      this.cbbNumFixationsToShow.SelectedIndexChanged += new System.EventHandler(this.cbbNumFixationsToShow_SelectedIndexChanged);
      // 
      // tosMouse
      // 
      this.tosMouse.DataBindings.Add(new System.Windows.Forms.Binding("Location", global::Ogama.Properties.Settings.Default, "RPLMouseToolbarLocation", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.tosMouse.Dock = System.Windows.Forms.DockStyle.None;
      this.tosMouse.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton3,
            this.toolStripLabel4,
            this.toolStripSeparator4,
            this.btnMouseModeCursor,
            this.btnMouseModePath,
            this.btnMouseModeFix,
            this.btnMouseModeFixCon,
            this.btnMouseModeSpot,
            this.toolStripSeparator3,
            this.nudMouseFixDiameterDiv,
            this.btnMouseCutPath,
            this.toolStripSeparator15,
            this.btnMouseModeClicks});
      this.tosMouse.Location = global::Ogama.Properties.Settings.Default.RPLMouseToolbarLocation;
      this.tosMouse.Name = "tosMouse";
      this.tosMouse.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
      this.tosMouse.Size = new System.Drawing.Size(386, 26);
      this.tosMouse.TabIndex = 5;
      this.tosMouse.Click += new System.EventHandler(this.btnMouseModeCursor_Click);
      // 
      // toolStripButton3
      // 
      this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.toolStripButton3.Enabled = false;
      this.toolStripButton3.Image = global::Ogama.Properties.Resources.Mouse;
      this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripButton3.Name = "toolStripButton3";
      this.toolStripButton3.Size = new System.Drawing.Size(23, 23);
      // 
      // toolStripLabel4
      // 
      this.toolStripLabel4.Name = "toolStripLabel4";
      this.toolStripLabel4.Size = new System.Drawing.Size(122, 23);
      this.toolStripLabel4.Text = "Mouse display modes";
      this.toolStripLabel4.ToolTipText = "Check or uncheck the mouse display modes.";
      // 
      // toolStripSeparator4
      // 
      this.toolStripSeparator4.Name = "toolStripSeparator4";
      this.toolStripSeparator4.Size = new System.Drawing.Size(6, 26);
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
      this.btnMouseModeCursor.Size = new System.Drawing.Size(23, 23);
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
      this.btnMouseModePath.Size = new System.Drawing.Size(23, 23);
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
      this.btnMouseModeFix.Size = new System.Drawing.Size(23, 23);
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
      this.btnMouseModeFixCon.Size = new System.Drawing.Size(23, 23);
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
      this.btnMouseModeSpot.Size = new System.Drawing.Size(23, 23);
      this.btnMouseModeSpot.ToolTipText = "Display Mouse as Spotlight";
      this.btnMouseModeSpot.Click += new System.EventHandler(this.btnMouseModeSpot_Click);
      // 
      // toolStripSeparator3
      // 
      this.toolStripSeparator3.Name = "toolStripSeparator3";
      this.toolStripSeparator3.Size = new System.Drawing.Size(6, 26);
      // 
      // nudMouseFixDiameterDiv
      // 
      this.nudMouseFixDiameterDiv.DecimalPlaces = 1;
      this.nudMouseFixDiameterDiv.Increment = new decimal(new int[] {
            1,
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
      this.nudMouseFixDiameterDiv.Text = "1,0";
      this.nudMouseFixDiameterDiv.ToolTipText = "Increase this value to reduce mouse fixation diameters.";
      this.nudMouseFixDiameterDiv.Value = new decimal(new int[] {
            10,
            0,
            0,
            65536});
      this.nudMouseFixDiameterDiv.ValueChanged += new System.EventHandler(this.nudMouseFixDiameterDiv_ValueChanged);
      // 
      // btnMouseCutPath
      // 
      this.btnMouseCutPath.Checked = global::Ogama.Properties.Settings.Default.MouseModeCutPath;
      this.btnMouseCutPath.CheckOnClick = true;
      this.btnMouseCutPath.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnMouseCutPath.Image = global::Ogama.Properties.Resources.RPLCut;
      this.btnMouseCutPath.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnMouseCutPath.Name = "btnMouseCutPath";
      this.btnMouseCutPath.Size = new System.Drawing.Size(23, 23);
      this.btnMouseCutPath.ToolTipText = "Reduce the amount of displayed mouse data.";
      this.btnMouseCutPath.Click += new System.EventHandler(this.btnMouseCutPath_Click);
      // 
      // toolStripSeparator15
      // 
      this.toolStripSeparator15.Name = "toolStripSeparator15";
      this.toolStripSeparator15.Size = new System.Drawing.Size(6, 26);
      // 
      // btnMouseModeClicks
      // 
      this.btnMouseModeClicks.CheckOnClick = true;
      this.btnMouseModeClicks.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnMouseModeClicks.Image = global::Ogama.Properties.Resources.HandCursor;
      this.btnMouseModeClicks.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnMouseModeClicks.Name = "btnMouseModeClicks";
      this.btnMouseModeClicks.Size = new System.Drawing.Size(23, 23);
      this.btnMouseModeClicks.Text = "Show/Hide Mouseclicks";
      this.btnMouseModeClicks.Click += new System.EventHandler(this.btnMouseModeClicks_Click);
      // 
      // sfdVideo
      // 
      this.sfdVideo.DefaultExt = "avi";
      this.sfdVideo.FileName = "*.avi";
      this.sfdVideo.Filter = "AVI - Video files|*.avi";
      this.sfdVideo.SupportMultiDottedExtensions = true;
      this.sfdVideo.Title = "Select file to save video to ...";
      // 
      // ReplayModule
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(685, 386);
      this.Controls.Add(this.toolStripContainer1);
      this.DataBindings.Add(new System.Windows.Forms.Binding("Location", global::Ogama.Properties.Settings.Default, "ReplayModuleLocation", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.KeyPreview = true;
      this.Location = global::Ogama.Properties.Settings.Default.ReplayModuleLocation;
      this.Logo = global::Ogama.Properties.Resources.RPL;
      this.Name = "ReplayModule";
      this.Text = "Replay Module";
      this.TransparencyKey = System.Drawing.SystemColors.Control;
      this.Load += new System.EventHandler(this.ReplayModule_Load);
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ReplayModule_FormClosing);
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
      this.toolStripContainer1.BottomToolStripPanel.ResumeLayout(false);
      this.toolStripContainer1.BottomToolStripPanel.PerformLayout();
      this.toolStripContainer1.ContentPanel.ResumeLayout(false);
      this.toolStripContainer1.RightToolStripPanel.ResumeLayout(false);
      this.toolStripContainer1.RightToolStripPanel.PerformLayout();
      this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
      this.toolStripContainer1.TopToolStripPanel.PerformLayout();
      this.toolStripContainer1.ResumeLayout(false);
      this.toolStripContainer1.PerformLayout();
      this.toolStrip5.ResumeLayout(false);
      this.toolStrip5.PerformLayout();
      this.pnlCanvas.ResumeLayout(false);
      this.pnlPicture.ResumeLayout(false);
      this.toolStrip2.ResumeLayout(false);
      this.toolStrip2.PerformLayout();
      this.toolStrip4.ResumeLayout(false);
      this.toolStrip4.PerformLayout();
      this.tosTrial.ResumeLayout(false);
      this.tosTrial.PerformLayout();
      this.tosGaze.ResumeLayout(false);
      this.tosGaze.PerformLayout();
      this.tosMouse.ResumeLayout(false);
      this.tosMouse.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.ToolStripContainer toolStripContainer1;
    private System.Windows.Forms.ToolStrip tosTrial;
    private System.Windows.Forms.ToolStripComboBox cbbSubject;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    private System.Windows.Forms.ToolStripComboBox cbbSpeed;
    private System.Windows.Forms.ToolStripLabel toolStripLabel1;
    private System.Windows.Forms.ToolStrip tosGaze;
    private System.Windows.Forms.ToolStripButton btnGazeModeCursor;
    private System.Windows.Forms.ToolStripButton btnGazeModePath;
    private System.Windows.Forms.ToolStripButton btnGazeModeFix;
    private System.Windows.Forms.ToolStripButton btnGazeModeFixCon;
    private System.Windows.Forms.ToolStripButton btnGazeModeSpot;
    private System.Windows.Forms.ToolStrip toolStrip2;
    private System.Windows.Forms.ToolStripButton btnGazeCursorPenStyle;
    private System.Windows.Forms.ToolStripButton btnGazePathPenStyle;
    private System.Windows.Forms.ToolStripButton btnGazeFixPenStyle;
    private ReplayPicture replayPicture;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
    private System.Windows.Forms.ToolStripButton btnGazeCutPath;
    private System.Windows.Forms.ToolStripButton btnGazeBlinks;
    private System.Windows.Forms.ToolStripButton btnGazeFixConPenStyle;
    private System.Windows.Forms.ToolStripButton btnGazeCursorTyp;
    private System.Windows.Forms.ToolStrip tosMouse;
    private System.Windows.Forms.ToolStripButton toolStripButton3;
    private System.Windows.Forms.ToolStripButton btnMouseModeCursor;
    private System.Windows.Forms.ToolStripButton btnMouseModePath;
    private System.Windows.Forms.ToolStripButton btnMouseModeFix;
    private System.Windows.Forms.ToolStripButton btnMouseModeFixCon;
    private System.Windows.Forms.ToolStripButton btnMouseModeSpot;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
    private System.Windows.Forms.ToolStripButton btnMouseCutPath;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
    private System.Windows.Forms.ToolStripButton toolStripButton4;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
    private System.Windows.Forms.ToolStripButton toolStripButton5;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
    private System.Windows.Forms.ToolStrip toolStrip4;
    private System.Windows.Forms.ToolStripButton toolStripButton6;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
    private System.Windows.Forms.ToolStripButton btnMouseCursorTyp;
    private System.Windows.Forms.ToolStripButton btnMouseCursorPenStyle;
    private System.Windows.Forms.ToolStripButton btnMousePathPenStyle;
    private System.Windows.Forms.ToolStripButton btnMouseFixPenStyle;
    private System.Windows.Forms.ToolStripButton btnMouseFixConPenStyle;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
    private System.Windows.Forms.ToolStripButton btnHelp;
    private System.Windows.Forms.ToolStripButton btnRecord;
    private System.Windows.Forms.SaveFileDialog sfdVideo;
    private System.Windows.Forms.ToolStrip toolStrip5;
    private System.Windows.Forms.ToolStripLabel toolStripLabel3;
    private System.Windows.Forms.ToolStripButton btnStart;
    private System.Windows.Forms.ToolStripButton btnPause;
    private System.Windows.Forms.ToolStripButton btnStop;
    private System.Windows.Forms.ToolStripButton btnRewind;
    private System.Windows.Forms.ToolStripLabel toolStripLabel4;
    private System.Windows.Forms.ToolStripLabel toolStripLabel2;
    private System.Windows.Forms.ToolStripComboBox cbbNumFixationsToShow;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
    private System.Windows.Forms.ToolStripComboBox cbbTrial;
    private OgamaControls.ToolStripNumericUpDown nudGazeFixDiameterDiv;
    private OgamaControls.ToolStripNumericUpDown nudMouseFixDiameterDiv;
    private System.Windows.Forms.Panel pnlCanvas;
    private System.Windows.Forms.ToolTip toolTip1;
    private System.Windows.Forms.Panel pnlPicture;
    private TrialTimeLine trialTimeLine;
    private System.Windows.Forms.ToolStripButton btnEnableAudio;
    private System.Windows.Forms.ToolStripButton btnShowUsercam;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator11;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator12;
    private System.Windows.Forms.ToolStripButton btnSeekToNextEvent;
    private System.Windows.Forms.ToolStripButton btnSeekToPreviousEvent;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator13;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator14;
    private System.Windows.Forms.ToolStripButton btnSeekToNextMarker;
    private System.Windows.Forms.ToolStripButton btnSeekToPreviousMarker;
    private System.Windows.Forms.ToolStripButton btnAddMarker;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator15;
    private System.Windows.Forms.ToolStripButton btnMouseModeClicks;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator16;
    private OgamaControls.ToolStripTrackBar trbZoom;
    private System.Windows.Forms.ToolStripButton btnShowVisiblePartOfScreen;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator17;
    private System.Windows.Forms.ToolStripButton btnStartContinous;

  }
}