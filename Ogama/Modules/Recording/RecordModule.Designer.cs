namespace Ogama.Modules.Recording
{
  using System.Windows.Forms;

  partial class RecordModule
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RecordModule));
      this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
      this.sctRecording = new System.Windows.Forms.SplitContainer();
      this.spcPanelUserCam = new System.Windows.Forms.SplitContainer();
      this.spcPanelRecordTime = new System.Windows.Forms.SplitContainer();
      this.tclEyetracker = new System.Windows.Forms.TabControl();
      this.tbpGazetrackerIPClient = new System.Windows.Forms.TabPage();
      this.panel11 = new System.Windows.Forms.Panel();
      this.label17 = new System.Windows.Forms.Label();
      this.btnGazetrackerIPLaunch = new System.Windows.Forms.Button();
      this.imlButtons = new System.Windows.Forms.ImageList(this.components);
      this.groupBox15 = new System.Windows.Forms.GroupBox();
      this.txbGazetrackerIPStatus = new System.Windows.Forms.TextBox();
      this.label54 = new System.Windows.Forms.Label();
      this.txbGazetrackerIPSubject = new System.Windows.Forms.TextBox();
      this.btnGazetrackerIPRecord = new System.Windows.Forms.Button();
      this.label80 = new System.Windows.Forms.Label();
      this.label82 = new System.Windows.Forms.Label();
      this.btnGazetrackerIPSubject = new System.Windows.Forms.Button();
      this.btnGazetrackerIPConnect = new System.Windows.Forms.Button();
      this.tbpGazetrackerDirectClient = new System.Windows.Forms.TabPage();
      this.panel3 = new System.Windows.Forms.Panel();
      this.groupBox4 = new System.Windows.Forms.GroupBox();
      this.splitContainer23 = new System.Windows.Forms.SplitContainer();
      this.eyeVideoControlGazetracker = new GTOgamaClient.Controls.EyeVideoControl();
      this.btnGazetrackerDirectClientShowOnPresentationScreen = new System.Windows.Forms.Button();
      this.txbGazetrackerDirectClientSubject = new System.Windows.Forms.TextBox();
      this.btnGazetrackerDirectClientRecord = new System.Windows.Forms.Button();
      this.btnGazetrackerDirectClientCalibrate = new System.Windows.Forms.Button();
      this.label19 = new System.Windows.Forms.Label();
      this.label20 = new System.Windows.Forms.Label();
      this.label47 = new System.Windows.Forms.Label();
      this.label83 = new System.Windows.Forms.Label();
      this.label81 = new System.Windows.Forms.Label();
      this.btnGazetrackerDirectClientSubject = new System.Windows.Forms.Button();
      this.btnGazetrackerDirectClientAdjust = new System.Windows.Forms.Button();
      this.btnGazetrackerDirectClientConnect = new System.Windows.Forms.Button();
      this.tbpTobii = new System.Windows.Forms.TabPage();
      this.panel1 = new System.Windows.Forms.Panel();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.spcTobiiControls = new System.Windows.Forms.SplitContainer();
      this.spcTobiiTrackStatus = new System.Windows.Forms.SplitContainer();
      this.btnTobiiShowOnPresentationScreen = new System.Windows.Forms.Button();
      this.spcTobiiCalibPlot = new System.Windows.Forms.SplitContainer();
      this.btnTobiiRecalibrate = new System.Windows.Forms.Button();
      this.btnTobiiAcceptCalibration = new System.Windows.Forms.Button();
      this.txbTobiiSubjectName = new System.Windows.Forms.TextBox();
      this.btnTobiiRecord = new System.Windows.Forms.Button();
      this.btnTobiiCalibrate = new System.Windows.Forms.Button();
      this.label3 = new System.Windows.Forms.Label();
      this.label4 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.label1 = new System.Windows.Forms.Label();
      this.btnTobiiSubjectName = new System.Windows.Forms.Button();
      this.btnTobiiConnect = new System.Windows.Forms.Button();
      this.tbpGazepoint = new System.Windows.Forms.TabPage();
      this.label93 = new System.Windows.Forms.Label();
      this.label94 = new System.Windows.Forms.Label();
      this.label95 = new System.Windows.Forms.Label();
      this.label96 = new System.Windows.Forms.Label();
      this.txbGazepointSubjectName = new System.Windows.Forms.TextBox();
      this.btnGazepointRecord = new System.Windows.Forms.Button();
      this.btnGazepointCalibrate = new System.Windows.Forms.Button();
      this.btnGazepointSubjectName = new System.Windows.Forms.Button();
      this.btnGazepointConnect = new System.Windows.Forms.Button();
      this.groupBox17 = new System.Windows.Forms.GroupBox();
      this.spcGazepointControls = new System.Windows.Forms.SplitContainer();
      this.spcGazepointTrackStatus = new System.Windows.Forms.SplitContainer();
      this.tBGWarning = new System.Windows.Forms.TextBox();
      this.btnGazepointShowOnPresentationScreen = new System.Windows.Forms.Button();
      this.spcGazepointCalibPlot = new System.Windows.Forms.SplitContainer();
      this.labelCalibrationResultGazepoint = new System.Windows.Forms.Label();
      this.btnGazepointRecalibrate = new System.Windows.Forms.Button();
      this.btnGazepointAcceptCalibration = new System.Windows.Forms.Button();
      this.tbpMirametrix = new System.Windows.Forms.TabPage();
      this.panel12 = new System.Windows.Forms.Panel();
      this.label88 = new System.Windows.Forms.Label();
      this.label89 = new System.Windows.Forms.Label();
      this.label90 = new System.Windows.Forms.Label();
      this.label91 = new System.Windows.Forms.Label();
      this.groupBox16 = new System.Windows.Forms.GroupBox();
      this.spcMirametrixControls = new System.Windows.Forms.SplitContainer();
      this.spcMirametrixTrackStatus = new System.Windows.Forms.SplitContainer();
      this.tBWarning = new System.Windows.Forms.TextBox();
      this.btnMirametrixShowOnPresentationScreen = new System.Windows.Forms.Button();
      this.spcMirametrixCalibPlot = new System.Windows.Forms.SplitContainer();
      this.labelCalibrationResultMirametrix = new System.Windows.Forms.Label();
      this.btnMirametrixRecalibrate = new System.Windows.Forms.Button();
      this.btnMirametrixAcceptCalibration = new System.Windows.Forms.Button();
      this.txbMirametrixSubjectName = new System.Windows.Forms.TextBox();
      this.btnMirametrixRecord = new System.Windows.Forms.Button();
      this.btnMirametrixCalibrate = new System.Windows.Forms.Button();
      this.btnMirametrixSubjectName = new System.Windows.Forms.Button();
      this.btnMirametrixConnect = new System.Windows.Forms.Button();
      this.tbpAlea = new System.Windows.Forms.TabPage();
      this.groupBox2 = new System.Windows.Forms.GroupBox();
      this.spcAleaControls = new System.Windows.Forms.SplitContainer();
      this.spcAleaTrackStatus = new System.Windows.Forms.SplitContainer();
      this.btnAleaShowOnPresentationScreen = new System.Windows.Forms.Button();
      this.spcAleaCalibPlot = new System.Windows.Forms.SplitContainer();
      this.labelCalibrationResult = new System.Windows.Forms.Label();
      this.label11 = new System.Windows.Forms.Label();
      this.btnAleaRecalibrate = new System.Windows.Forms.Button();
      this.btnAleaAcceptCalibration = new System.Windows.Forms.Button();
      this.txbAleaSubjectName = new System.Windows.Forms.TextBox();
      this.btnAleaRecord = new System.Windows.Forms.Button();
      this.btnAleaCalibrate = new System.Windows.Forms.Button();
      this.label7 = new System.Windows.Forms.Label();
      this.label8 = new System.Windows.Forms.Label();
      this.label9 = new System.Windows.Forms.Label();
      this.label10 = new System.Windows.Forms.Label();
      this.btnAleaSubjectName = new System.Windows.Forms.Button();
      this.btnAleaConnect = new System.Windows.Forms.Button();
      this.tbpSMIiViewX = new System.Windows.Forms.TabPage();
      this.groupBox3 = new System.Windows.Forms.GroupBox();
      this.textBox1 = new System.Windows.Forms.TextBox();
      this.txbSMIiViewXSubjectName = new System.Windows.Forms.TextBox();
      this.btnSMIiViewXRecord = new System.Windows.Forms.Button();
      this.btnSMIiViewXCalibrate = new System.Windows.Forms.Button();
      this.label13 = new System.Windows.Forms.Label();
      this.label14 = new System.Windows.Forms.Label();
      this.label15 = new System.Windows.Forms.Label();
      this.label16 = new System.Windows.Forms.Label();
      this.btnSMIiViewXSubjectName = new System.Windows.Forms.Button();
      this.btnSMIiViewXConnect = new System.Windows.Forms.Button();
      this.tbpAsl = new System.Windows.Forms.TabPage();
      this.txbAslSubjectName = new System.Windows.Forms.TextBox();
      this.btnAslRecord = new System.Windows.Forms.Button();
      this.btnAslCalibrate = new System.Windows.Forms.Button();
      this.label75 = new System.Windows.Forms.Label();
      this.recordLabel = new System.Windows.Forms.Label();
      this.label76 = new System.Windows.Forms.Label();
      this.connectLabel = new System.Windows.Forms.Label();
      this.btnAslSubjectName = new System.Windows.Forms.Button();
      this.btnAslConnect = new System.Windows.Forms.Button();
      this.tbpEyeTech = new System.Windows.Forms.TabPage();
      this.groupBox20 = new System.Windows.Forms.GroupBox();
      this.textBox15 = new System.Windows.Forms.TextBox();
      this.txbEyeTechSubjectName = new System.Windows.Forms.TextBox();
      this.btnEyeTechRecord = new System.Windows.Forms.Button();
      this.btnEyeTechCalibrate = new System.Windows.Forms.Button();
      this.label84 = new System.Windows.Forms.Label();
      this.label85 = new System.Windows.Forms.Label();
      this.label86 = new System.Windows.Forms.Label();
      this.label87 = new System.Windows.Forms.Label();
      this.btnEyeTechSubjectName = new System.Windows.Forms.Button();
      this.btnEyeTechConnect = new System.Windows.Forms.Button();
      this.tbpMouseOnly = new System.Windows.Forms.TabPage();
      this.label5 = new System.Windows.Forms.Label();
      this.label6 = new System.Windows.Forms.Label();
      this.btnMouseOnlySubject = new System.Windows.Forms.Button();
      this.txbMouseOnlySubjectName = new System.Windows.Forms.TextBox();
      this.btnMouseOnlyRecord = new System.Windows.Forms.Button();
      this.tbpNoDevice = new System.Windows.Forms.TabPage();
      this.label12 = new System.Windows.Forms.Label();
      this.btnNoDeviceTabSelectTracker = new System.Windows.Forms.Button();
      this.imlEyetracker = new System.Windows.Forms.ImageList(this.components);
      this.panel4 = new System.Windows.Forms.Panel();
      this.lblRecordedTime = new System.Windows.Forms.Label();
      this.label21 = new System.Windows.Forms.Label();
      this.grpUsercam = new System.Windows.Forms.GroupBox();
      this.splitContainer2 = new System.Windows.Forms.SplitContainer();
      this.panel2 = new System.Windows.Forms.Panel();
      this.chbRecordAudio = new System.Windows.Forms.CheckBox();
      this.chbRecordVideo = new System.Windows.Forms.CheckBox();
      this.webcamPreview = new OgamaControls.Webcam();
      this.toolStripContainer2 = new System.Windows.Forms.ToolStripContainer();
      this.toolStrip2 = new System.Windows.Forms.ToolStrip();
      this.trbZoom = new OgamaControls.ToolStripTrackBar();
      this.pnlCanvas = new System.Windows.Forms.Panel();
      this.pnlPicture = new System.Windows.Forms.Panel();
      this.recordPicture = new Ogama.Modules.Recording.RecordingPicture();
      this.toolStrip1 = new System.Windows.Forms.ToolStrip();
      this.btnPrimary = new System.Windows.Forms.ToolStripButton();
      this.btnSecondary = new System.Windows.Forms.ToolStripButton();
      this.btnScreenCaptureSettings = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
      this.btnSelectTracker = new System.Windows.Forms.ToolStripButton();
      this.btnTrackerSettings = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
      this.btnUsercam = new System.Windows.Forms.ToolStripButton();
      this.btnWebcamSettings = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.btnTrigger = new System.Windows.Forms.ToolStripButton();
      this.btnTriggerSettings = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
      this.btnSmoothing = new System.Windows.Forms.ToolStripButton();
      this.btnHelp = new System.Windows.Forms.ToolStripButton();
      this.imlSlides = new System.Windows.Forms.ImageList(this.components);
      this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
      this.bgwSaveSplash = new System.ComponentModel.BackgroundWorker();
      this.bgwCalcFixations = new System.ComponentModel.BackgroundWorker();
      this.tmrWaitForPresentationEnd = new System.Windows.Forms.Timer(this.components);
      this.tmrRecordClock = new System.Windows.Forms.Timer(this.components);
      this.tabPage1 = new System.Windows.Forms.TabPage();
      this.panel5 = new System.Windows.Forms.Panel();
      this.groupBox5 = new System.Windows.Forms.GroupBox();
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      this.splitContainer3 = new System.Windows.Forms.SplitContainer();
      this.pictureBox1 = new System.Windows.Forms.PictureBox();
      this.button1 = new System.Windows.Forms.Button();
      this.splitContainer4 = new System.Windows.Forms.SplitContainer();
      this.panel6 = new System.Windows.Forms.Panel();
      this.button2 = new System.Windows.Forms.Button();
      this.button3 = new System.Windows.Forms.Button();
      this.textBox2 = new System.Windows.Forms.TextBox();
      this.button4 = new System.Windows.Forms.Button();
      this.button5 = new System.Windows.Forms.Button();
      this.label22 = new System.Windows.Forms.Label();
      this.label23 = new System.Windows.Forms.Label();
      this.label24 = new System.Windows.Forms.Label();
      this.label25 = new System.Windows.Forms.Label();
      this.button6 = new System.Windows.Forms.Button();
      this.button7 = new System.Windows.Forms.Button();
      this.tabPage2 = new System.Windows.Forms.TabPage();
      this.panel7 = new System.Windows.Forms.Panel();
      this.groupBox6 = new System.Windows.Forms.GroupBox();
      this.splitContainer5 = new System.Windows.Forms.SplitContainer();
      this.splitContainer6 = new System.Windows.Forms.SplitContainer();
      this.button8 = new System.Windows.Forms.Button();
      this.splitContainer7 = new System.Windows.Forms.SplitContainer();
      this.button9 = new System.Windows.Forms.Button();
      this.button10 = new System.Windows.Forms.Button();
      this.textBox3 = new System.Windows.Forms.TextBox();
      this.button11 = new System.Windows.Forms.Button();
      this.button12 = new System.Windows.Forms.Button();
      this.label26 = new System.Windows.Forms.Label();
      this.label27 = new System.Windows.Forms.Label();
      this.label28 = new System.Windows.Forms.Label();
      this.label29 = new System.Windows.Forms.Label();
      this.button13 = new System.Windows.Forms.Button();
      this.button14 = new System.Windows.Forms.Button();
      this.tabPage3 = new System.Windows.Forms.TabPage();
      this.groupBox7 = new System.Windows.Forms.GroupBox();
      this.splitContainer8 = new System.Windows.Forms.SplitContainer();
      this.splitContainer9 = new System.Windows.Forms.SplitContainer();
      this.button15 = new System.Windows.Forms.Button();
      this.splitContainer10 = new System.Windows.Forms.SplitContainer();
      this.label30 = new System.Windows.Forms.Label();
      this.label31 = new System.Windows.Forms.Label();
      this.button16 = new System.Windows.Forms.Button();
      this.button17 = new System.Windows.Forms.Button();
      this.textBox4 = new System.Windows.Forms.TextBox();
      this.button18 = new System.Windows.Forms.Button();
      this.button19 = new System.Windows.Forms.Button();
      this.label32 = new System.Windows.Forms.Label();
      this.label33 = new System.Windows.Forms.Label();
      this.label34 = new System.Windows.Forms.Label();
      this.label35 = new System.Windows.Forms.Label();
      this.button20 = new System.Windows.Forms.Button();
      this.button21 = new System.Windows.Forms.Button();
      this.tabPage4 = new System.Windows.Forms.TabPage();
      this.groupBox8 = new System.Windows.Forms.GroupBox();
      this.textBox5 = new System.Windows.Forms.TextBox();
      this.textBox6 = new System.Windows.Forms.TextBox();
      this.button22 = new System.Windows.Forms.Button();
      this.button23 = new System.Windows.Forms.Button();
      this.label36 = new System.Windows.Forms.Label();
      this.label37 = new System.Windows.Forms.Label();
      this.label38 = new System.Windows.Forms.Label();
      this.label39 = new System.Windows.Forms.Label();
      this.button24 = new System.Windows.Forms.Button();
      this.button25 = new System.Windows.Forms.Button();
      this.tabPage5 = new System.Windows.Forms.TabPage();
      this.label40 = new System.Windows.Forms.Label();
      this.label41 = new System.Windows.Forms.Label();
      this.button26 = new System.Windows.Forms.Button();
      this.textBox7 = new System.Windows.Forms.TextBox();
      this.button27 = new System.Windows.Forms.Button();
      this.tabPage6 = new System.Windows.Forms.TabPage();
      this.label42 = new System.Windows.Forms.Label();
      this.button28 = new System.Windows.Forms.Button();
      this.label48 = new System.Windows.Forms.Label();
      this.button30 = new System.Windows.Forms.Button();
      this.groupBox10 = new System.Windows.Forms.GroupBox();
      this.splitContainer11 = new System.Windows.Forms.SplitContainer();
      this.splitContainer12 = new System.Windows.Forms.SplitContainer();
      this.button31 = new System.Windows.Forms.Button();
      this.splitContainer13 = new System.Windows.Forms.SplitContainer();
      this.button32 = new System.Windows.Forms.Button();
      this.button33 = new System.Windows.Forms.Button();
      this.textBox8 = new System.Windows.Forms.TextBox();
      this.button34 = new System.Windows.Forms.Button();
      this.button35 = new System.Windows.Forms.Button();
      this.label49 = new System.Windows.Forms.Label();
      this.label50 = new System.Windows.Forms.Label();
      this.label51 = new System.Windows.Forms.Label();
      this.label52 = new System.Windows.Forms.Label();
      this.button36 = new System.Windows.Forms.Button();
      this.button37 = new System.Windows.Forms.Button();
      this.tabPage7 = new System.Windows.Forms.TabPage();
      this.panel8 = new System.Windows.Forms.Panel();
      this.label18 = new System.Windows.Forms.Label();
      this.button29 = new System.Windows.Forms.Button();
      this.groupBox9 = new System.Windows.Forms.GroupBox();
      this.splitContainer14 = new System.Windows.Forms.SplitContainer();
      this.splitContainer15 = new System.Windows.Forms.SplitContainer();
      this.button38 = new System.Windows.Forms.Button();
      this.splitContainer16 = new System.Windows.Forms.SplitContainer();
      this.button39 = new System.Windows.Forms.Button();
      this.button40 = new System.Windows.Forms.Button();
      this.textBox9 = new System.Windows.Forms.TextBox();
      this.button41 = new System.Windows.Forms.Button();
      this.button42 = new System.Windows.Forms.Button();
      this.label43 = new System.Windows.Forms.Label();
      this.label44 = new System.Windows.Forms.Label();
      this.label45 = new System.Windows.Forms.Label();
      this.label46 = new System.Windows.Forms.Label();
      this.label53 = new System.Windows.Forms.Label();
      this.button43 = new System.Windows.Forms.Button();
      this.btnITUCamera = new System.Windows.Forms.Button();
      this.button44 = new System.Windows.Forms.Button();
      this.tbpITUPS3 = new System.Windows.Forms.TabPage();
      this.panel9 = new System.Windows.Forms.Panel();
      this.label55 = new System.Windows.Forms.Label();
      this.btnITUPS3Camera = new System.Windows.Forms.Button();
      this.label56 = new System.Windows.Forms.Label();
      this.btnITUPS3Adjust = new System.Windows.Forms.Button();
      this.groupBox11 = new System.Windows.Forms.GroupBox();
      this.spcITUPS3Controls = new System.Windows.Forms.SplitContainer();
      this.spcITUPS3TrackStatus = new System.Windows.Forms.SplitContainer();
      this.btnITUPS3ShowOnPresentationScreen = new System.Windows.Forms.Button();
      this.spcITUPS3CalibPlot = new System.Windows.Forms.SplitContainer();
      this.btnITUPS3Recalibrate = new System.Windows.Forms.Button();
      this.btnITUPS3AcceptCalibration = new System.Windows.Forms.Button();
      this.txbITUPS3SubjectName = new System.Windows.Forms.TextBox();
      this.btnITUPS3Record = new System.Windows.Forms.Button();
      this.btnITUPS3Calibrate = new System.Windows.Forms.Button();
      this.label57 = new System.Windows.Forms.Label();
      this.label58 = new System.Windows.Forms.Label();
      this.label59 = new System.Windows.Forms.Label();
      this.label60 = new System.Windows.Forms.Label();
      this.btnITUPS3SubjectName = new System.Windows.Forms.Button();
      this.btnITUPS3Connect = new System.Windows.Forms.Button();
      this.tabPage8 = new System.Windows.Forms.TabPage();
      this.panel10 = new System.Windows.Forms.Panel();
      this.groupBox12 = new System.Windows.Forms.GroupBox();
      this.splitContainer17 = new System.Windows.Forms.SplitContainer();
      this.splitContainer18 = new System.Windows.Forms.SplitContainer();
      this.button45 = new System.Windows.Forms.Button();
      this.splitContainer19 = new System.Windows.Forms.SplitContainer();
      this.button46 = new System.Windows.Forms.Button();
      this.button47 = new System.Windows.Forms.Button();
      this.textBox10 = new System.Windows.Forms.TextBox();
      this.button48 = new System.Windows.Forms.Button();
      this.button49 = new System.Windows.Forms.Button();
      this.label61 = new System.Windows.Forms.Label();
      this.label62 = new System.Windows.Forms.Label();
      this.label63 = new System.Windows.Forms.Label();
      this.label64 = new System.Windows.Forms.Label();
      this.button50 = new System.Windows.Forms.Button();
      this.button51 = new System.Windows.Forms.Button();
      this.tabPage9 = new System.Windows.Forms.TabPage();
      this.groupBox13 = new System.Windows.Forms.GroupBox();
      this.splitContainer20 = new System.Windows.Forms.SplitContainer();
      this.splitContainer21 = new System.Windows.Forms.SplitContainer();
      this.button52 = new System.Windows.Forms.Button();
      this.splitContainer22 = new System.Windows.Forms.SplitContainer();
      this.label65 = new System.Windows.Forms.Label();
      this.label66 = new System.Windows.Forms.Label();
      this.button53 = new System.Windows.Forms.Button();
      this.button54 = new System.Windows.Forms.Button();
      this.textBox11 = new System.Windows.Forms.TextBox();
      this.button55 = new System.Windows.Forms.Button();
      this.button56 = new System.Windows.Forms.Button();
      this.label67 = new System.Windows.Forms.Label();
      this.label68 = new System.Windows.Forms.Label();
      this.label69 = new System.Windows.Forms.Label();
      this.label70 = new System.Windows.Forms.Label();
      this.button57 = new System.Windows.Forms.Button();
      this.button58 = new System.Windows.Forms.Button();
      this.tabPage10 = new System.Windows.Forms.TabPage();
      this.groupBox14 = new System.Windows.Forms.GroupBox();
      this.textBox12 = new System.Windows.Forms.TextBox();
      this.textBox13 = new System.Windows.Forms.TextBox();
      this.button59 = new System.Windows.Forms.Button();
      this.button60 = new System.Windows.Forms.Button();
      this.label71 = new System.Windows.Forms.Label();
      this.label72 = new System.Windows.Forms.Label();
      this.label73 = new System.Windows.Forms.Label();
      this.label74 = new System.Windows.Forms.Label();
      this.button61 = new System.Windows.Forms.Button();
      this.button62 = new System.Windows.Forms.Button();
      this.tabPage11 = new System.Windows.Forms.TabPage();
      this.label77 = new System.Windows.Forms.Label();
      this.label78 = new System.Windows.Forms.Label();
      this.button63 = new System.Windows.Forms.Button();
      this.textBox14 = new System.Windows.Forms.TextBox();
      this.button64 = new System.Windows.Forms.Button();
      this.tabPage12 = new System.Windows.Forms.TabPage();
      this.label79 = new System.Windows.Forms.Label();
      this.button65 = new System.Windows.Forms.Button();
      this.tbpSMIRedM = new System.Windows.Forms.TabPage();
      this.groupBox18 = new System.Windows.Forms.GroupBox();
      this.textBox16 = new System.Windows.Forms.TextBox();
      this.txbSMIRedMSubject = new System.Windows.Forms.TextBox();
      this.btnSMIRedMRecord = new System.Windows.Forms.Button();
      this.btnSMIRedMCalibrate = new System.Windows.Forms.Button();
      this.label92 = new System.Windows.Forms.Label();
      this.label97 = new System.Windows.Forms.Label();
      this.label98 = new System.Windows.Forms.Label();
      this.label99 = new System.Windows.Forms.Label();
      this.btnSMIRedMSubject = new System.Windows.Forms.Button();
      this.btnSMIRedMConnect = new System.Windows.Forms.Button();
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
      ((System.ComponentModel.ISupportInitialize)(this.sctRecording)).BeginInit();
      this.sctRecording.Panel1.SuspendLayout();
      this.sctRecording.Panel2.SuspendLayout();
      this.sctRecording.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.spcPanelUserCam)).BeginInit();
      this.spcPanelUserCam.Panel1.SuspendLayout();
      this.spcPanelUserCam.Panel2.SuspendLayout();
      this.spcPanelUserCam.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.spcPanelRecordTime)).BeginInit();
      this.spcPanelRecordTime.Panel1.SuspendLayout();
      this.spcPanelRecordTime.Panel2.SuspendLayout();
      this.spcPanelRecordTime.SuspendLayout();
      this.tclEyetracker.SuspendLayout();
      this.tbpGazetrackerIPClient.SuspendLayout();
      this.panel11.SuspendLayout();
      this.groupBox15.SuspendLayout();
      this.tbpGazetrackerDirectClient.SuspendLayout();
      this.panel3.SuspendLayout();
      this.groupBox4.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer23)).BeginInit();
      this.splitContainer23.Panel1.SuspendLayout();
      this.splitContainer23.Panel2.SuspendLayout();
      this.splitContainer23.SuspendLayout();
      this.tbpTobii.SuspendLayout();
      this.panel1.SuspendLayout();
      this.groupBox1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.spcTobiiControls)).BeginInit();
      this.spcTobiiControls.Panel1.SuspendLayout();
      this.spcTobiiControls.Panel2.SuspendLayout();
      this.spcTobiiControls.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.spcTobiiTrackStatus)).BeginInit();
      this.spcTobiiTrackStatus.Panel2.SuspendLayout();
      this.spcTobiiTrackStatus.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.spcTobiiCalibPlot)).BeginInit();
      this.spcTobiiCalibPlot.Panel2.SuspendLayout();
      this.spcTobiiCalibPlot.SuspendLayout();
      this.tbpGazepoint.SuspendLayout();
      this.groupBox17.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.spcGazepointControls)).BeginInit();
      this.spcGazepointControls.Panel1.SuspendLayout();
      this.spcGazepointControls.Panel2.SuspendLayout();
      this.spcGazepointControls.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.spcGazepointTrackStatus)).BeginInit();
      this.spcGazepointTrackStatus.Panel1.SuspendLayout();
      this.spcGazepointTrackStatus.Panel2.SuspendLayout();
      this.spcGazepointTrackStatus.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.spcGazepointCalibPlot)).BeginInit();
      this.spcGazepointCalibPlot.Panel1.SuspendLayout();
      this.spcGazepointCalibPlot.Panel2.SuspendLayout();
      this.spcGazepointCalibPlot.SuspendLayout();
      this.tbpMirametrix.SuspendLayout();
      this.panel12.SuspendLayout();
      this.groupBox16.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.spcMirametrixControls)).BeginInit();
      this.spcMirametrixControls.Panel1.SuspendLayout();
      this.spcMirametrixControls.Panel2.SuspendLayout();
      this.spcMirametrixControls.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.spcMirametrixTrackStatus)).BeginInit();
      this.spcMirametrixTrackStatus.Panel1.SuspendLayout();
      this.spcMirametrixTrackStatus.Panel2.SuspendLayout();
      this.spcMirametrixTrackStatus.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.spcMirametrixCalibPlot)).BeginInit();
      this.spcMirametrixCalibPlot.Panel1.SuspendLayout();
      this.spcMirametrixCalibPlot.Panel2.SuspendLayout();
      this.spcMirametrixCalibPlot.SuspendLayout();
      this.tbpAlea.SuspendLayout();
      this.groupBox2.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.spcAleaControls)).BeginInit();
      this.spcAleaControls.Panel1.SuspendLayout();
      this.spcAleaControls.Panel2.SuspendLayout();
      this.spcAleaControls.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.spcAleaTrackStatus)).BeginInit();
      this.spcAleaTrackStatus.Panel2.SuspendLayout();
      this.spcAleaTrackStatus.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.spcAleaCalibPlot)).BeginInit();
      this.spcAleaCalibPlot.Panel1.SuspendLayout();
      this.spcAleaCalibPlot.Panel2.SuspendLayout();
      this.spcAleaCalibPlot.SuspendLayout();
      this.tbpSMIiViewX.SuspendLayout();
      this.groupBox3.SuspendLayout();
      this.tbpAsl.SuspendLayout();
      this.tbpEyeTech.SuspendLayout();
      this.groupBox20.SuspendLayout();
      this.tbpMouseOnly.SuspendLayout();
      this.tbpNoDevice.SuspendLayout();
      this.panel4.SuspendLayout();
      this.grpUsercam.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
      this.splitContainer2.Panel1.SuspendLayout();
      this.splitContainer2.Panel2.SuspendLayout();
      this.splitContainer2.SuspendLayout();
      this.panel2.SuspendLayout();
      this.toolStripContainer2.BottomToolStripPanel.SuspendLayout();
      this.toolStripContainer2.ContentPanel.SuspendLayout();
      this.toolStripContainer2.SuspendLayout();
      this.toolStrip2.SuspendLayout();
      this.pnlCanvas.SuspendLayout();
      this.pnlPicture.SuspendLayout();
      this.toolStrip1.SuspendLayout();
      this.tabPage1.SuspendLayout();
      this.panel5.SuspendLayout();
      this.groupBox5.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
      this.splitContainer3.Panel1.SuspendLayout();
      this.splitContainer3.Panel2.SuspendLayout();
      this.splitContainer3.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).BeginInit();
      this.splitContainer4.Panel1.SuspendLayout();
      this.splitContainer4.Panel2.SuspendLayout();
      this.splitContainer4.SuspendLayout();
      this.tabPage2.SuspendLayout();
      this.panel7.SuspendLayout();
      this.groupBox6.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer5)).BeginInit();
      this.splitContainer5.Panel1.SuspendLayout();
      this.splitContainer5.Panel2.SuspendLayout();
      this.splitContainer5.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer6)).BeginInit();
      this.splitContainer6.Panel2.SuspendLayout();
      this.splitContainer6.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer7)).BeginInit();
      this.splitContainer7.Panel2.SuspendLayout();
      this.splitContainer7.SuspendLayout();
      this.tabPage3.SuspendLayout();
      this.groupBox7.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer8)).BeginInit();
      this.splitContainer8.Panel1.SuspendLayout();
      this.splitContainer8.Panel2.SuspendLayout();
      this.splitContainer8.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer9)).BeginInit();
      this.splitContainer9.Panel2.SuspendLayout();
      this.splitContainer9.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer10)).BeginInit();
      this.splitContainer10.Panel1.SuspendLayout();
      this.splitContainer10.Panel2.SuspendLayout();
      this.splitContainer10.SuspendLayout();
      this.tabPage4.SuspendLayout();
      this.groupBox8.SuspendLayout();
      this.tabPage5.SuspendLayout();
      this.tabPage6.SuspendLayout();
      this.groupBox10.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer11)).BeginInit();
      this.splitContainer11.Panel1.SuspendLayout();
      this.splitContainer11.Panel2.SuspendLayout();
      this.splitContainer11.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer12)).BeginInit();
      this.splitContainer12.Panel2.SuspendLayout();
      this.splitContainer12.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer13)).BeginInit();
      this.splitContainer13.Panel2.SuspendLayout();
      this.splitContainer13.SuspendLayout();
      this.tabPage7.SuspendLayout();
      this.panel8.SuspendLayout();
      this.groupBox9.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer14)).BeginInit();
      this.splitContainer14.Panel1.SuspendLayout();
      this.splitContainer14.Panel2.SuspendLayout();
      this.splitContainer14.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer15)).BeginInit();
      this.splitContainer15.Panel2.SuspendLayout();
      this.splitContainer15.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer16)).BeginInit();
      this.splitContainer16.Panel2.SuspendLayout();
      this.splitContainer16.SuspendLayout();
      this.tbpITUPS3.SuspendLayout();
      this.panel9.SuspendLayout();
      this.groupBox11.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.spcITUPS3Controls)).BeginInit();
      this.spcITUPS3Controls.Panel1.SuspendLayout();
      this.spcITUPS3Controls.Panel2.SuspendLayout();
      this.spcITUPS3Controls.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.spcITUPS3TrackStatus)).BeginInit();
      this.spcITUPS3TrackStatus.Panel2.SuspendLayout();
      this.spcITUPS3TrackStatus.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.spcITUPS3CalibPlot)).BeginInit();
      this.spcITUPS3CalibPlot.Panel2.SuspendLayout();
      this.spcITUPS3CalibPlot.SuspendLayout();
      this.tabPage8.SuspendLayout();
      this.panel10.SuspendLayout();
      this.groupBox12.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer17)).BeginInit();
      this.splitContainer17.Panel1.SuspendLayout();
      this.splitContainer17.Panel2.SuspendLayout();
      this.splitContainer17.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer18)).BeginInit();
      this.splitContainer18.Panel2.SuspendLayout();
      this.splitContainer18.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer19)).BeginInit();
      this.splitContainer19.Panel2.SuspendLayout();
      this.splitContainer19.SuspendLayout();
      this.tabPage9.SuspendLayout();
      this.groupBox13.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer20)).BeginInit();
      this.splitContainer20.Panel1.SuspendLayout();
      this.splitContainer20.Panel2.SuspendLayout();
      this.splitContainer20.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer21)).BeginInit();
      this.splitContainer21.Panel2.SuspendLayout();
      this.splitContainer21.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer22)).BeginInit();
      this.splitContainer22.Panel1.SuspendLayout();
      this.splitContainer22.Panel2.SuspendLayout();
      this.splitContainer22.SuspendLayout();
      this.tabPage10.SuspendLayout();
      this.groupBox14.SuspendLayout();
      this.tabPage11.SuspendLayout();
      this.tabPage12.SuspendLayout();
      this.tbpSMIRedM.SuspendLayout();
      this.groupBox18.SuspendLayout();
      this.SuspendLayout();
      // 
      // toolStripContainer1
      // 
      // 
      // toolStripContainer1.ContentPanel
      // 
      this.toolStripContainer1.ContentPanel.Controls.Add(this.sctRecording);
      this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(863, 645);
      this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
      this.toolStripContainer1.Name = "toolStripContainer1";
      this.toolStripContainer1.Size = new System.Drawing.Size(863, 670);
      this.toolStripContainer1.TabIndex = 0;
      this.toolStripContainer1.Text = "toolStripContainer1";
      // 
      // toolStripContainer1.TopToolStripPanel
      // 
      this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip1);
      // 
      // sctRecording
      // 
      this.sctRecording.Dock = System.Windows.Forms.DockStyle.Fill;
      this.sctRecording.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
      this.sctRecording.IsSplitterFixed = true;
      this.sctRecording.Location = new System.Drawing.Point(0, 0);
      this.sctRecording.Name = "sctRecording";
      // 
      // sctRecording.Panel1
      // 
      this.sctRecording.Panel1.Controls.Add(this.spcPanelUserCam);
      this.sctRecording.Panel1MinSize = 220;
      // 
      // sctRecording.Panel2
      // 
      this.sctRecording.Panel2.Controls.Add(this.toolStripContainer2);
      this.sctRecording.Size = new System.Drawing.Size(863, 645);
      this.sctRecording.SplitterDistance = 220;
      this.sctRecording.TabIndex = 2;
      // 
      // spcPanelUserCam
      // 
      this.spcPanelUserCam.Dock = System.Windows.Forms.DockStyle.Fill;
      this.spcPanelUserCam.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
      this.spcPanelUserCam.IsSplitterFixed = true;
      this.spcPanelUserCam.Location = new System.Drawing.Point(0, 0);
      this.spcPanelUserCam.Name = "spcPanelUserCam";
      this.spcPanelUserCam.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // spcPanelUserCam.Panel1
      // 
      this.spcPanelUserCam.Panel1.Controls.Add(this.spcPanelRecordTime);
      // 
      // spcPanelUserCam.Panel2
      // 
      this.spcPanelUserCam.Panel2.Controls.Add(this.grpUsercam);
      this.spcPanelUserCam.Panel2.Padding = new System.Windows.Forms.Padding(5);
      this.spcPanelUserCam.Panel2MinSize = 20;
      this.spcPanelUserCam.Size = new System.Drawing.Size(220, 645);
      this.spcPanelUserCam.SplitterDistance = 441;
      this.spcPanelUserCam.TabIndex = 21;
      // 
      // spcPanelRecordTime
      // 
      this.spcPanelRecordTime.Dock = System.Windows.Forms.DockStyle.Fill;
      this.spcPanelRecordTime.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
      this.spcPanelRecordTime.IsSplitterFixed = true;
      this.spcPanelRecordTime.Location = new System.Drawing.Point(0, 0);
      this.spcPanelRecordTime.Name = "spcPanelRecordTime";
      this.spcPanelRecordTime.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // spcPanelRecordTime.Panel1
      // 
      this.spcPanelRecordTime.Panel1.Controls.Add(this.tclEyetracker);
      // 
      // spcPanelRecordTime.Panel2
      // 
      this.spcPanelRecordTime.Panel2.Controls.Add(this.panel4);
      this.spcPanelRecordTime.Size = new System.Drawing.Size(220, 441);
      this.spcPanelRecordTime.SplitterDistance = 412;
      this.spcPanelRecordTime.TabIndex = 1;
      // 
      // tclEyetracker
      // 
      this.tclEyetracker.Controls.Add(this.tbpGazetrackerIPClient);
      this.tclEyetracker.Controls.Add(this.tbpGazetrackerDirectClient);
      this.tclEyetracker.Controls.Add(this.tbpTobii);
      this.tclEyetracker.Controls.Add(this.tbpGazepoint);
      this.tclEyetracker.Controls.Add(this.tbpMirametrix);
      this.tclEyetracker.Controls.Add(this.tbpAlea);
      this.tclEyetracker.Controls.Add(this.tbpSMIiViewX);
      this.tclEyetracker.Controls.Add(this.tbpAsl);
      this.tclEyetracker.Controls.Add(this.tbpEyeTech);
      this.tclEyetracker.Controls.Add(this.tbpMouseOnly);
      this.tclEyetracker.Controls.Add(this.tbpNoDevice);
      this.tclEyetracker.Controls.Add(this.tbpSMIRedM);
      this.tclEyetracker.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tclEyetracker.ImageList = this.imlEyetracker;
      this.tclEyetracker.Location = new System.Drawing.Point(0, 0);
      this.tclEyetracker.Name = "tclEyetracker";
      this.tclEyetracker.SelectedIndex = 0;
      this.tclEyetracker.Size = new System.Drawing.Size(220, 412);
      this.tclEyetracker.TabIndex = 1;
      this.tclEyetracker.SelectedIndexChanged += new System.EventHandler(this.TclEyetrackerSelectedIndexChanged);
      // 
      // tbpGazetrackerIPClient
      // 
      this.tbpGazetrackerIPClient.Controls.Add(this.panel11);
      this.tbpGazetrackerIPClient.ImageKey = "ITU";
      this.tbpGazetrackerIPClient.Location = new System.Drawing.Point(4, 23);
      this.tbpGazetrackerIPClient.Name = "tbpGazetrackerIPClient";
      this.tbpGazetrackerIPClient.Padding = new System.Windows.Forms.Padding(3);
      this.tbpGazetrackerIPClient.Size = new System.Drawing.Size(212, 385);
      this.tbpGazetrackerIPClient.TabIndex = 8;
      this.tbpGazetrackerIPClient.Text = "Gazetracker via UDP";
      this.tbpGazetrackerIPClient.UseVisualStyleBackColor = true;
      // 
      // panel11
      // 
      this.panel11.AutoScroll = true;
      this.panel11.Controls.Add(this.label17);
      this.panel11.Controls.Add(this.btnGazetrackerIPLaunch);
      this.panel11.Controls.Add(this.groupBox15);
      this.panel11.Controls.Add(this.label54);
      this.panel11.Controls.Add(this.txbGazetrackerIPSubject);
      this.panel11.Controls.Add(this.btnGazetrackerIPRecord);
      this.panel11.Controls.Add(this.label80);
      this.panel11.Controls.Add(this.label82);
      this.panel11.Controls.Add(this.btnGazetrackerIPSubject);
      this.panel11.Controls.Add(this.btnGazetrackerIPConnect);
      this.panel11.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel11.Location = new System.Drawing.Point(3, 3);
      this.panel11.Name = "panel11";
      this.panel11.Size = new System.Drawing.Size(206, 379);
      this.panel11.TabIndex = 11;
      // 
      // label17
      // 
      this.label17.AutoSize = true;
      this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label17.Location = new System.Drawing.Point(4, 131);
      this.label17.Margin = new System.Windows.Forms.Padding(0);
      this.label17.Name = "label17";
      this.label17.Size = new System.Drawing.Size(25, 24);
      this.label17.TabIndex = 33;
      this.label17.Text = "1.";
      // 
      // btnGazetrackerIPLaunch
      // 
      this.btnGazetrackerIPLaunch.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
      this.btnGazetrackerIPLaunch.ImageKey = "ITU";
      this.btnGazetrackerIPLaunch.ImageList = this.imlButtons;
      this.btnGazetrackerIPLaunch.Location = new System.Drawing.Point(28, 131);
      this.btnGazetrackerIPLaunch.Name = "btnGazetrackerIPLaunch";
      this.btnGazetrackerIPLaunch.Size = new System.Drawing.Size(73, 23);
      this.btnGazetrackerIPLaunch.TabIndex = 32;
      this.btnGazetrackerIPLaunch.Text = "Launch";
      this.btnGazetrackerIPLaunch.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.btnGazetrackerIPLaunch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.btnGazetrackerIPLaunch.UseVisualStyleBackColor = true;
      // 
      // imlButtons
      // 
      this.imlButtons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imlButtons.ImageStream")));
      this.imlButtons.TransparentColor = System.Drawing.Color.Transparent;
      this.imlButtons.Images.SetKeyName(0, "Connect");
      this.imlButtons.Images.SetKeyName(1, "Subject");
      this.imlButtons.Images.SetKeyName(2, "Calibrate");
      this.imlButtons.Images.SetKeyName(3, "Record");
      this.imlButtons.Images.SetKeyName(4, "Video");
      this.imlButtons.Images.SetKeyName(5, "Sound");
      this.imlButtons.Images.SetKeyName(6, "Adjust");
      this.imlButtons.Images.SetKeyName(7, "ITU");
      this.imlButtons.Images.SetKeyName(8, "PS3");
      // 
      // groupBox15
      // 
      this.groupBox15.Controls.Add(this.txbGazetrackerIPStatus);
      this.groupBox15.Dock = System.Windows.Forms.DockStyle.Top;
      this.groupBox15.Location = new System.Drawing.Point(0, 0);
      this.groupBox15.Name = "groupBox15";
      this.groupBox15.Size = new System.Drawing.Size(206, 120);
      this.groupBox15.TabIndex = 31;
      this.groupBox15.TabStop = false;
      this.groupBox15.Text = "Track status";
      // 
      // txbGazetrackerIPStatus
      // 
      this.txbGazetrackerIPStatus.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txbGazetrackerIPStatus.Location = new System.Drawing.Point(3, 16);
      this.txbGazetrackerIPStatus.Multiline = true;
      this.txbGazetrackerIPStatus.Name = "txbGazetrackerIPStatus";
      this.txbGazetrackerIPStatus.Size = new System.Drawing.Size(200, 101);
      this.txbGazetrackerIPStatus.TabIndex = 0;
      this.txbGazetrackerIPStatus.Text = "Please note:\r\n- The track status and the quality of\r\n   the calibration are shown" +
    " in the \r\n   gazetracker application.\r\n- Turn off the firewalls if connecting\r\n " +
    "  fails.";
      // 
      // label54
      // 
      this.label54.AutoSize = true;
      this.label54.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label54.Location = new System.Drawing.Point(4, 183);
      this.label54.Margin = new System.Windows.Forms.Padding(0);
      this.label54.Name = "label54";
      this.label54.Size = new System.Drawing.Size(25, 24);
      this.label54.TabIndex = 22;
      this.label54.Text = "3.";
      // 
      // txbGazetrackerIPSubject
      // 
      this.txbGazetrackerIPSubject.Location = new System.Drawing.Point(106, 185);
      this.txbGazetrackerIPSubject.Name = "txbGazetrackerIPSubject";
      this.txbGazetrackerIPSubject.ReadOnly = true;
      this.txbGazetrackerIPSubject.Size = new System.Drawing.Size(91, 20);
      this.txbGazetrackerIPSubject.TabIndex = 11;
      this.txbGazetrackerIPSubject.Text = "Subject1";
      // 
      // btnGazetrackerIPRecord
      // 
      this.btnGazetrackerIPRecord.Enabled = false;
      this.btnGazetrackerIPRecord.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
      this.btnGazetrackerIPRecord.ImageKey = "Record";
      this.btnGazetrackerIPRecord.ImageList = this.imlButtons;
      this.btnGazetrackerIPRecord.Location = new System.Drawing.Point(28, 212);
      this.btnGazetrackerIPRecord.Name = "btnGazetrackerIPRecord";
      this.btnGazetrackerIPRecord.Size = new System.Drawing.Size(73, 23);
      this.btnGazetrackerIPRecord.TabIndex = 17;
      this.btnGazetrackerIPRecord.Text = "&Record";
      this.btnGazetrackerIPRecord.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      // 
      // label80
      // 
      this.label80.AutoSize = true;
      this.label80.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label80.Location = new System.Drawing.Point(4, 210);
      this.label80.Name = "label80";
      this.label80.Size = new System.Drawing.Size(25, 24);
      this.label80.TabIndex = 13;
      this.label80.Text = "4.";
      // 
      // label82
      // 
      this.label82.AutoSize = true;
      this.label82.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label82.Location = new System.Drawing.Point(4, 157);
      this.label82.Margin = new System.Windows.Forms.Padding(0);
      this.label82.Name = "label82";
      this.label82.Size = new System.Drawing.Size(25, 24);
      this.label82.TabIndex = 13;
      this.label82.Text = "2.";
      // 
      // btnGazetrackerIPSubject
      // 
      this.btnGazetrackerIPSubject.Enabled = false;
      this.btnGazetrackerIPSubject.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
      this.btnGazetrackerIPSubject.ImageKey = "Subject";
      this.btnGazetrackerIPSubject.ImageList = this.imlButtons;
      this.btnGazetrackerIPSubject.Location = new System.Drawing.Point(28, 184);
      this.btnGazetrackerIPSubject.Name = "btnGazetrackerIPSubject";
      this.btnGazetrackerIPSubject.Size = new System.Drawing.Size(73, 23);
      this.btnGazetrackerIPSubject.TabIndex = 12;
      this.btnGazetrackerIPSubject.Text = "Subject";
      this.btnGazetrackerIPSubject.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.btnGazetrackerIPSubject.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.btnGazetrackerIPSubject.UseVisualStyleBackColor = true;
      // 
      // btnGazetrackerIPConnect
      // 
      this.btnGazetrackerIPConnect.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
      this.btnGazetrackerIPConnect.ImageKey = "Connect";
      this.btnGazetrackerIPConnect.ImageList = this.imlButtons;
      this.btnGazetrackerIPConnect.Location = new System.Drawing.Point(28, 157);
      this.btnGazetrackerIPConnect.Name = "btnGazetrackerIPConnect";
      this.btnGazetrackerIPConnect.Size = new System.Drawing.Size(73, 23);
      this.btnGazetrackerIPConnect.TabIndex = 12;
      this.btnGazetrackerIPConnect.Text = "Connect";
      this.btnGazetrackerIPConnect.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.btnGazetrackerIPConnect.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.btnGazetrackerIPConnect.UseVisualStyleBackColor = true;
      // 
      // tbpGazetrackerDirectClient
      // 
      this.tbpGazetrackerDirectClient.Controls.Add(this.panel3);
      this.tbpGazetrackerDirectClient.ImageKey = "ITU";
      this.tbpGazetrackerDirectClient.Location = new System.Drawing.Point(4, 23);
      this.tbpGazetrackerDirectClient.Name = "tbpGazetrackerDirectClient";
      this.tbpGazetrackerDirectClient.Padding = new System.Windows.Forms.Padding(3);
      this.tbpGazetrackerDirectClient.Size = new System.Drawing.Size(212, 385);
      this.tbpGazetrackerDirectClient.TabIndex = 9;
      this.tbpGazetrackerDirectClient.Text = "Gazetracker";
      this.tbpGazetrackerDirectClient.UseVisualStyleBackColor = true;
      // 
      // panel3
      // 
      this.panel3.Controls.Add(this.groupBox4);
      this.panel3.Controls.Add(this.txbGazetrackerDirectClientSubject);
      this.panel3.Controls.Add(this.btnGazetrackerDirectClientRecord);
      this.panel3.Controls.Add(this.btnGazetrackerDirectClientCalibrate);
      this.panel3.Controls.Add(this.label19);
      this.panel3.Controls.Add(this.label20);
      this.panel3.Controls.Add(this.label47);
      this.panel3.Controls.Add(this.label83);
      this.panel3.Controls.Add(this.label81);
      this.panel3.Controls.Add(this.btnGazetrackerDirectClientSubject);
      this.panel3.Controls.Add(this.btnGazetrackerDirectClientAdjust);
      this.panel3.Controls.Add(this.btnGazetrackerDirectClientConnect);
      this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel3.Location = new System.Drawing.Point(3, 3);
      this.panel3.Name = "panel3";
      this.panel3.Size = new System.Drawing.Size(206, 379);
      this.panel3.TabIndex = 11;
      // 
      // groupBox4
      // 
      this.groupBox4.Controls.Add(this.splitContainer23);
      this.groupBox4.Location = new System.Drawing.Point(5, 3);
      this.groupBox4.Name = "groupBox4";
      this.groupBox4.Size = new System.Drawing.Size(196, 183);
      this.groupBox4.TabIndex = 20;
      this.groupBox4.TabStop = false;
      this.groupBox4.Text = "Track status";
      // 
      // splitContainer23
      // 
      this.splitContainer23.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer23.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
      this.splitContainer23.IsSplitterFixed = true;
      this.splitContainer23.Location = new System.Drawing.Point(3, 16);
      this.splitContainer23.Name = "splitContainer23";
      this.splitContainer23.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // splitContainer23.Panel1
      // 
      this.splitContainer23.Panel1.Controls.Add(this.eyeVideoControlGazetracker);
      // 
      // splitContainer23.Panel2
      // 
      this.splitContainer23.Panel2.Controls.Add(this.btnGazetrackerDirectClientShowOnPresentationScreen);
      this.splitContainer23.Size = new System.Drawing.Size(190, 164);
      this.splitContainer23.SplitterDistance = 135;
      this.splitContainer23.TabIndex = 22;
      // 
      // eyeVideoControlGazetracker
      // 
      this.eyeVideoControlGazetracker.Dock = System.Windows.Forms.DockStyle.Fill;
      this.eyeVideoControlGazetracker.IsNativeResolution = false;
      this.eyeVideoControlGazetracker.Location = new System.Drawing.Point(0, 0);
      this.eyeVideoControlGazetracker.Name = "eyeVideoControlGazetracker";
      this.eyeVideoControlGazetracker.Size = new System.Drawing.Size(190, 135);
      this.eyeVideoControlGazetracker.TabIndex = 0;
      // 
      // btnGazetrackerDirectClientShowOnPresentationScreen
      // 
      this.btnGazetrackerDirectClientShowOnPresentationScreen.BackColor = System.Drawing.Color.Transparent;
      this.btnGazetrackerDirectClientShowOnPresentationScreen.Dock = System.Windows.Forms.DockStyle.Fill;
      this.btnGazetrackerDirectClientShowOnPresentationScreen.Location = new System.Drawing.Point(0, 0);
      this.btnGazetrackerDirectClientShowOnPresentationScreen.Name = "btnGazetrackerDirectClientShowOnPresentationScreen";
      this.btnGazetrackerDirectClientShowOnPresentationScreen.Size = new System.Drawing.Size(190, 25);
      this.btnGazetrackerDirectClientShowOnPresentationScreen.TabIndex = 21;
      this.btnGazetrackerDirectClientShowOnPresentationScreen.Text = "Show on presentation screen";
      this.btnGazetrackerDirectClientShowOnPresentationScreen.UseVisualStyleBackColor = false;
      // 
      // txbGazetrackerDirectClientSubject
      // 
      this.txbGazetrackerDirectClientSubject.Location = new System.Drawing.Point(108, 257);
      this.txbGazetrackerDirectClientSubject.Name = "txbGazetrackerDirectClientSubject";
      this.txbGazetrackerDirectClientSubject.ReadOnly = true;
      this.txbGazetrackerDirectClientSubject.Size = new System.Drawing.Size(91, 20);
      this.txbGazetrackerDirectClientSubject.TabIndex = 11;
      this.txbGazetrackerDirectClientSubject.Text = "Subject1";
      // 
      // btnGazetrackerDirectClientRecord
      // 
      this.btnGazetrackerDirectClientRecord.Enabled = false;
      this.btnGazetrackerDirectClientRecord.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
      this.btnGazetrackerDirectClientRecord.ImageKey = "Record";
      this.btnGazetrackerDirectClientRecord.ImageList = this.imlButtons;
      this.btnGazetrackerDirectClientRecord.Location = new System.Drawing.Point(30, 313);
      this.btnGazetrackerDirectClientRecord.Name = "btnGazetrackerDirectClientRecord";
      this.btnGazetrackerDirectClientRecord.Size = new System.Drawing.Size(72, 23);
      this.btnGazetrackerDirectClientRecord.TabIndex = 17;
      this.btnGazetrackerDirectClientRecord.Text = "&Record";
      this.btnGazetrackerDirectClientRecord.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      // 
      // btnGazetrackerDirectClientCalibrate
      // 
      this.btnGazetrackerDirectClientCalibrate.Enabled = false;
      this.btnGazetrackerDirectClientCalibrate.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
      this.btnGazetrackerDirectClientCalibrate.ImageKey = "Calibrate";
      this.btnGazetrackerDirectClientCalibrate.ImageList = this.imlButtons;
      this.btnGazetrackerDirectClientCalibrate.Location = new System.Drawing.Point(30, 284);
      this.btnGazetrackerDirectClientCalibrate.Name = "btnGazetrackerDirectClientCalibrate";
      this.btnGazetrackerDirectClientCalibrate.Size = new System.Drawing.Size(72, 23);
      this.btnGazetrackerDirectClientCalibrate.TabIndex = 17;
      this.btnGazetrackerDirectClientCalibrate.Text = "&Calibrate";
      this.btnGazetrackerDirectClientCalibrate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      // 
      // label19
      // 
      this.label19.AutoSize = true;
      this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label19.Location = new System.Drawing.Point(6, 284);
      this.label19.Name = "label19";
      this.label19.Size = new System.Drawing.Size(25, 24);
      this.label19.TabIndex = 13;
      this.label19.Text = "4.";
      // 
      // label20
      // 
      this.label20.AutoSize = true;
      this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label20.Location = new System.Drawing.Point(6, 313);
      this.label20.Name = "label20";
      this.label20.Size = new System.Drawing.Size(25, 24);
      this.label20.TabIndex = 13;
      this.label20.Text = "5.";
      // 
      // label47
      // 
      this.label47.AutoSize = true;
      this.label47.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label47.Location = new System.Drawing.Point(6, 255);
      this.label47.Name = "label47";
      this.label47.Size = new System.Drawing.Size(25, 24);
      this.label47.TabIndex = 13;
      this.label47.Text = "3.";
      // 
      // label83
      // 
      this.label83.AutoSize = true;
      this.label83.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label83.Location = new System.Drawing.Point(5, 227);
      this.label83.Margin = new System.Windows.Forms.Padding(0);
      this.label83.Name = "label83";
      this.label83.Size = new System.Drawing.Size(25, 24);
      this.label83.TabIndex = 13;
      this.label83.Text = "2.";
      // 
      // label81
      // 
      this.label81.AutoSize = true;
      this.label81.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label81.Location = new System.Drawing.Point(5, 198);
      this.label81.Margin = new System.Windows.Forms.Padding(0);
      this.label81.Name = "label81";
      this.label81.Size = new System.Drawing.Size(25, 24);
      this.label81.TabIndex = 13;
      this.label81.Text = "1.";
      // 
      // btnGazetrackerDirectClientSubject
      // 
      this.btnGazetrackerDirectClientSubject.Enabled = false;
      this.btnGazetrackerDirectClientSubject.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
      this.btnGazetrackerDirectClientSubject.ImageKey = "Subject";
      this.btnGazetrackerDirectClientSubject.ImageList = this.imlButtons;
      this.btnGazetrackerDirectClientSubject.Location = new System.Drawing.Point(30, 256);
      this.btnGazetrackerDirectClientSubject.Name = "btnGazetrackerDirectClientSubject";
      this.btnGazetrackerDirectClientSubject.Size = new System.Drawing.Size(72, 23);
      this.btnGazetrackerDirectClientSubject.TabIndex = 12;
      this.btnGazetrackerDirectClientSubject.Text = "Subject";
      this.btnGazetrackerDirectClientSubject.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.btnGazetrackerDirectClientSubject.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.btnGazetrackerDirectClientSubject.UseVisualStyleBackColor = true;
      // 
      // btnGazetrackerDirectClientAdjust
      // 
      this.btnGazetrackerDirectClientAdjust.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
      this.btnGazetrackerDirectClientAdjust.ImageKey = "Adjust";
      this.btnGazetrackerDirectClientAdjust.ImageList = this.imlButtons;
      this.btnGazetrackerDirectClientAdjust.Location = new System.Drawing.Point(29, 227);
      this.btnGazetrackerDirectClientAdjust.Name = "btnGazetrackerDirectClientAdjust";
      this.btnGazetrackerDirectClientAdjust.Size = new System.Drawing.Size(73, 23);
      this.btnGazetrackerDirectClientAdjust.TabIndex = 12;
      this.btnGazetrackerDirectClientAdjust.Text = "Adjust";
      this.btnGazetrackerDirectClientAdjust.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.btnGazetrackerDirectClientAdjust.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.btnGazetrackerDirectClientAdjust.UseVisualStyleBackColor = true;
      // 
      // btnGazetrackerDirectClientConnect
      // 
      this.btnGazetrackerDirectClientConnect.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
      this.btnGazetrackerDirectClientConnect.ImageKey = "Connect";
      this.btnGazetrackerDirectClientConnect.ImageList = this.imlButtons;
      this.btnGazetrackerDirectClientConnect.Location = new System.Drawing.Point(29, 198);
      this.btnGazetrackerDirectClientConnect.Name = "btnGazetrackerDirectClientConnect";
      this.btnGazetrackerDirectClientConnect.Size = new System.Drawing.Size(73, 23);
      this.btnGazetrackerDirectClientConnect.TabIndex = 12;
      this.btnGazetrackerDirectClientConnect.Text = "Connect";
      this.btnGazetrackerDirectClientConnect.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.btnGazetrackerDirectClientConnect.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.btnGazetrackerDirectClientConnect.UseVisualStyleBackColor = true;
      // 
      // tbpTobii
      // 
      this.tbpTobii.Controls.Add(this.panel1);
      this.tbpTobii.ImageKey = "Tobii";
      this.tbpTobii.Location = new System.Drawing.Point(4, 23);
      this.tbpTobii.Name = "tbpTobii";
      this.tbpTobii.Padding = new System.Windows.Forms.Padding(3);
      this.tbpTobii.Size = new System.Drawing.Size(212, 385);
      this.tbpTobii.TabIndex = 0;
      this.tbpTobii.Text = "Tobii";
      this.tbpTobii.UseVisualStyleBackColor = true;
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.groupBox1);
      this.panel1.Controls.Add(this.txbTobiiSubjectName);
      this.panel1.Controls.Add(this.btnTobiiRecord);
      this.panel1.Controls.Add(this.btnTobiiCalibrate);
      this.panel1.Controls.Add(this.label3);
      this.panel1.Controls.Add(this.label4);
      this.panel1.Controls.Add(this.label2);
      this.panel1.Controls.Add(this.label1);
      this.panel1.Controls.Add(this.btnTobiiSubjectName);
      this.panel1.Controls.Add(this.btnTobiiConnect);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel1.Location = new System.Drawing.Point(3, 3);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(206, 379);
      this.panel1.TabIndex = 11;
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.spcTobiiControls);
      this.groupBox1.Location = new System.Drawing.Point(5, 3);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(196, 183);
      this.groupBox1.TabIndex = 20;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Track status";
      // 
      // spcTobiiControls
      // 
      this.spcTobiiControls.Dock = System.Windows.Forms.DockStyle.Fill;
      this.spcTobiiControls.Location = new System.Drawing.Point(3, 16);
      this.spcTobiiControls.Name = "spcTobiiControls";
      this.spcTobiiControls.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // spcTobiiControls.Panel1
      // 
      this.spcTobiiControls.Panel1.Controls.Add(this.spcTobiiTrackStatus);
      // 
      // spcTobiiControls.Panel2
      // 
      this.spcTobiiControls.Panel2.Controls.Add(this.spcTobiiCalibPlot);
      this.spcTobiiControls.Size = new System.Drawing.Size(190, 164);
      this.spcTobiiControls.SplitterDistance = 80;
      this.spcTobiiControls.TabIndex = 22;
      // 
      // spcTobiiTrackStatus
      // 
      this.spcTobiiTrackStatus.Dock = System.Windows.Forms.DockStyle.Fill;
      this.spcTobiiTrackStatus.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
      this.spcTobiiTrackStatus.IsSplitterFixed = true;
      this.spcTobiiTrackStatus.Location = new System.Drawing.Point(0, 0);
      this.spcTobiiTrackStatus.Name = "spcTobiiTrackStatus";
      this.spcTobiiTrackStatus.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // spcTobiiTrackStatus.Panel1
      // 
      this.spcTobiiTrackStatus.Panel1.BackColor = System.Drawing.Color.Transparent;
      // 
      // spcTobiiTrackStatus.Panel2
      // 
      this.spcTobiiTrackStatus.Panel2.Controls.Add(this.btnTobiiShowOnPresentationScreen);
      this.spcTobiiTrackStatus.Panel2MinSize = 22;
      this.spcTobiiTrackStatus.Size = new System.Drawing.Size(190, 80);
      this.spcTobiiTrackStatus.SplitterDistance = 51;
      this.spcTobiiTrackStatus.TabIndex = 20;
      // 
      // btnTobiiShowOnPresentationScreen
      // 
      this.btnTobiiShowOnPresentationScreen.BackColor = System.Drawing.Color.Transparent;
      this.btnTobiiShowOnPresentationScreen.Dock = System.Windows.Forms.DockStyle.Fill;
      this.btnTobiiShowOnPresentationScreen.Enabled = false;
      this.btnTobiiShowOnPresentationScreen.Location = new System.Drawing.Point(0, 0);
      this.btnTobiiShowOnPresentationScreen.Name = "btnTobiiShowOnPresentationScreen";
      this.btnTobiiShowOnPresentationScreen.Size = new System.Drawing.Size(190, 25);
      this.btnTobiiShowOnPresentationScreen.TabIndex = 21;
      this.btnTobiiShowOnPresentationScreen.Text = "Show on presentation screen";
      this.btnTobiiShowOnPresentationScreen.UseVisualStyleBackColor = false;
      // 
      // spcTobiiCalibPlot
      // 
      this.spcTobiiCalibPlot.Dock = System.Windows.Forms.DockStyle.Fill;
      this.spcTobiiCalibPlot.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
      this.spcTobiiCalibPlot.IsSplitterFixed = true;
      this.spcTobiiCalibPlot.Location = new System.Drawing.Point(0, 0);
      this.spcTobiiCalibPlot.Name = "spcTobiiCalibPlot";
      this.spcTobiiCalibPlot.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // spcTobiiCalibPlot.Panel2
      // 
      this.spcTobiiCalibPlot.Panel2.Controls.Add(this.btnTobiiRecalibrate);
      this.spcTobiiCalibPlot.Panel2.Controls.Add(this.btnTobiiAcceptCalibration);
      this.spcTobiiCalibPlot.Panel2MinSize = 22;
      this.spcTobiiCalibPlot.Size = new System.Drawing.Size(190, 80);
      this.spcTobiiCalibPlot.SplitterDistance = 51;
      this.spcTobiiCalibPlot.TabIndex = 1;
      // 
      // btnTobiiRecalibrate
      // 
      this.btnTobiiRecalibrate.Dock = System.Windows.Forms.DockStyle.Right;
      this.btnTobiiRecalibrate.Location = new System.Drawing.Point(115, 0);
      this.btnTobiiRecalibrate.Name = "btnTobiiRecalibrate";
      this.btnTobiiRecalibrate.Size = new System.Drawing.Size(75, 25);
      this.btnTobiiRecalibrate.TabIndex = 0;
      this.btnTobiiRecalibrate.Text = "Recalibrate";
      this.btnTobiiRecalibrate.UseVisualStyleBackColor = true;
      // 
      // btnTobiiAcceptCalibration
      // 
      this.btnTobiiAcceptCalibration.Dock = System.Windows.Forms.DockStyle.Left;
      this.btnTobiiAcceptCalibration.Location = new System.Drawing.Point(0, 0);
      this.btnTobiiAcceptCalibration.Name = "btnTobiiAcceptCalibration";
      this.btnTobiiAcceptCalibration.Size = new System.Drawing.Size(75, 25);
      this.btnTobiiAcceptCalibration.TabIndex = 0;
      this.btnTobiiAcceptCalibration.Text = "Accept";
      this.btnTobiiAcceptCalibration.UseVisualStyleBackColor = true;
      // 
      // txbTobiiSubjectName
      // 
      this.txbTobiiSubjectName.Location = new System.Drawing.Point(107, 229);
      this.txbTobiiSubjectName.Name = "txbTobiiSubjectName";
      this.txbTobiiSubjectName.ReadOnly = true;
      this.txbTobiiSubjectName.Size = new System.Drawing.Size(91, 20);
      this.txbTobiiSubjectName.TabIndex = 11;
      this.txbTobiiSubjectName.Text = "Subject1";
      // 
      // btnTobiiRecord
      // 
      this.btnTobiiRecord.Enabled = false;
      this.btnTobiiRecord.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
      this.btnTobiiRecord.ImageKey = "Record";
      this.btnTobiiRecord.ImageList = this.imlButtons;
      this.btnTobiiRecord.Location = new System.Drawing.Point(29, 285);
      this.btnTobiiRecord.Name = "btnTobiiRecord";
      this.btnTobiiRecord.Size = new System.Drawing.Size(72, 23);
      this.btnTobiiRecord.TabIndex = 17;
      this.btnTobiiRecord.Text = "&Record";
      this.btnTobiiRecord.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      // 
      // btnTobiiCalibrate
      // 
      this.btnTobiiCalibrate.Enabled = false;
      this.btnTobiiCalibrate.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
      this.btnTobiiCalibrate.ImageKey = "Calibrate";
      this.btnTobiiCalibrate.ImageList = this.imlButtons;
      this.btnTobiiCalibrate.Location = new System.Drawing.Point(29, 256);
      this.btnTobiiCalibrate.Name = "btnTobiiCalibrate";
      this.btnTobiiCalibrate.Size = new System.Drawing.Size(72, 23);
      this.btnTobiiCalibrate.TabIndex = 17;
      this.btnTobiiCalibrate.Text = "&Calibrate";
      this.btnTobiiCalibrate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label3.Location = new System.Drawing.Point(5, 256);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(25, 24);
      this.label3.TabIndex = 13;
      this.label3.Text = "3.";
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label4.Location = new System.Drawing.Point(5, 285);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(25, 24);
      this.label4.TabIndex = 13;
      this.label4.Text = "4.";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label2.Location = new System.Drawing.Point(5, 227);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(25, 24);
      this.label2.TabIndex = 13;
      this.label2.Text = "2.";
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label1.Location = new System.Drawing.Point(5, 198);
      this.label1.Margin = new System.Windows.Forms.Padding(0);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(25, 24);
      this.label1.TabIndex = 13;
      this.label1.Text = "1.";
      // 
      // btnTobiiSubjectName
      // 
      this.btnTobiiSubjectName.Enabled = false;
      this.btnTobiiSubjectName.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
      this.btnTobiiSubjectName.ImageKey = "Subject";
      this.btnTobiiSubjectName.ImageList = this.imlButtons;
      this.btnTobiiSubjectName.Location = new System.Drawing.Point(29, 228);
      this.btnTobiiSubjectName.Name = "btnTobiiSubjectName";
      this.btnTobiiSubjectName.Size = new System.Drawing.Size(72, 23);
      this.btnTobiiSubjectName.TabIndex = 12;
      this.btnTobiiSubjectName.Text = "Subject";
      this.btnTobiiSubjectName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.btnTobiiSubjectName.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.btnTobiiSubjectName.UseVisualStyleBackColor = true;
      // 
      // btnTobiiConnect
      // 
      this.btnTobiiConnect.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
      this.btnTobiiConnect.ImageKey = "Connect";
      this.btnTobiiConnect.ImageList = this.imlButtons;
      this.btnTobiiConnect.Location = new System.Drawing.Point(29, 198);
      this.btnTobiiConnect.Name = "btnTobiiConnect";
      this.btnTobiiConnect.Size = new System.Drawing.Size(73, 23);
      this.btnTobiiConnect.TabIndex = 12;
      this.btnTobiiConnect.Text = "Connect";
      this.btnTobiiConnect.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.btnTobiiConnect.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.btnTobiiConnect.UseVisualStyleBackColor = true;
      // 
      // tbpGazepoint
      // 
      this.tbpGazepoint.Controls.Add(this.label93);
      this.tbpGazepoint.Controls.Add(this.label94);
      this.tbpGazepoint.Controls.Add(this.label95);
      this.tbpGazepoint.Controls.Add(this.label96);
      this.tbpGazepoint.Controls.Add(this.txbGazepointSubjectName);
      this.tbpGazepoint.Controls.Add(this.btnGazepointRecord);
      this.tbpGazepoint.Controls.Add(this.btnGazepointCalibrate);
      this.tbpGazepoint.Controls.Add(this.btnGazepointSubjectName);
      this.tbpGazepoint.Controls.Add(this.btnGazepointConnect);
      this.tbpGazepoint.Controls.Add(this.groupBox17);
      this.tbpGazepoint.ImageKey = "Gazepoint_GP3";
      this.tbpGazepoint.Location = new System.Drawing.Point(4, 23);
      this.tbpGazepoint.Name = "tbpGazepoint";
      this.tbpGazepoint.Padding = new System.Windows.Forms.Padding(3);
      this.tbpGazepoint.Size = new System.Drawing.Size(212, 385);
      this.tbpGazepoint.TabIndex = 12;
      this.tbpGazepoint.Text = "Gazepoint";
      this.tbpGazepoint.UseVisualStyleBackColor = true;
      // 
      // label93
      // 
      this.label93.AutoSize = true;
      this.label93.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label93.Location = new System.Drawing.Point(8, 257);
      this.label93.Name = "label93";
      this.label93.Size = new System.Drawing.Size(25, 24);
      this.label93.TabIndex = 32;
      this.label93.Text = "3.";
      // 
      // label94
      // 
      this.label94.AutoSize = true;
      this.label94.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label94.Location = new System.Drawing.Point(8, 286);
      this.label94.Name = "label94";
      this.label94.Size = new System.Drawing.Size(25, 24);
      this.label94.TabIndex = 33;
      this.label94.Text = "4.";
      // 
      // label95
      // 
      this.label95.AutoSize = true;
      this.label95.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label95.Location = new System.Drawing.Point(8, 228);
      this.label95.Name = "label95";
      this.label95.Size = new System.Drawing.Size(25, 24);
      this.label95.TabIndex = 30;
      this.label95.Text = "2.";
      // 
      // label96
      // 
      this.label96.AutoSize = true;
      this.label96.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label96.Location = new System.Drawing.Point(8, 199);
      this.label96.Margin = new System.Windows.Forms.Padding(0);
      this.label96.Name = "label96";
      this.label96.Size = new System.Drawing.Size(25, 24);
      this.label96.TabIndex = 31;
      this.label96.Text = "1.";
      // 
      // txbGazepointSubjectName
      // 
      this.txbGazepointSubjectName.Location = new System.Drawing.Point(110, 232);
      this.txbGazepointSubjectName.Name = "txbGazepointSubjectName";
      this.txbGazepointSubjectName.ReadOnly = true;
      this.txbGazepointSubjectName.Size = new System.Drawing.Size(91, 20);
      this.txbGazepointSubjectName.TabIndex = 25;
      this.txbGazepointSubjectName.Text = "Subject1";
      // 
      // btnGazepointRecord
      // 
      this.btnGazepointRecord.Enabled = false;
      this.btnGazepointRecord.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
      this.btnGazepointRecord.ImageKey = "Record";
      this.btnGazepointRecord.ImageList = this.imlButtons;
      this.btnGazepointRecord.Location = new System.Drawing.Point(32, 288);
      this.btnGazepointRecord.Name = "btnGazepointRecord";
      this.btnGazepointRecord.Size = new System.Drawing.Size(72, 23);
      this.btnGazepointRecord.TabIndex = 28;
      this.btnGazepointRecord.Text = "&Record";
      this.btnGazepointRecord.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      // 
      // btnGazepointCalibrate
      // 
      this.btnGazepointCalibrate.Enabled = false;
      this.btnGazepointCalibrate.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
      this.btnGazepointCalibrate.ImageKey = "Calibrate";
      this.btnGazepointCalibrate.ImageList = this.imlButtons;
      this.btnGazepointCalibrate.Location = new System.Drawing.Point(32, 259);
      this.btnGazepointCalibrate.Name = "btnGazepointCalibrate";
      this.btnGazepointCalibrate.Size = new System.Drawing.Size(72, 23);
      this.btnGazepointCalibrate.TabIndex = 29;
      this.btnGazepointCalibrate.Text = "&Calibrate";
      this.btnGazepointCalibrate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      // 
      // btnGazepointSubjectName
      // 
      this.btnGazepointSubjectName.Enabled = false;
      this.btnGazepointSubjectName.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
      this.btnGazepointSubjectName.ImageKey = "Subject";
      this.btnGazepointSubjectName.ImageList = this.imlButtons;
      this.btnGazepointSubjectName.Location = new System.Drawing.Point(32, 231);
      this.btnGazepointSubjectName.Name = "btnGazepointSubjectName";
      this.btnGazepointSubjectName.Size = new System.Drawing.Size(72, 23);
      this.btnGazepointSubjectName.TabIndex = 26;
      this.btnGazepointSubjectName.Text = "Subject";
      this.btnGazepointSubjectName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.btnGazepointSubjectName.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.btnGazepointSubjectName.UseVisualStyleBackColor = true;
      // 
      // btnGazepointConnect
      // 
      this.btnGazepointConnect.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
      this.btnGazepointConnect.ImageKey = "Connect";
      this.btnGazepointConnect.ImageList = this.imlButtons;
      this.btnGazepointConnect.Location = new System.Drawing.Point(32, 201);
      this.btnGazepointConnect.Name = "btnGazepointConnect";
      this.btnGazepointConnect.Size = new System.Drawing.Size(73, 23);
      this.btnGazepointConnect.TabIndex = 27;
      this.btnGazepointConnect.Text = "Connect";
      this.btnGazepointConnect.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.btnGazepointConnect.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.btnGazepointConnect.UseVisualStyleBackColor = true;
      // 
      // groupBox17
      // 
      this.groupBox17.Controls.Add(this.spcGazepointControls);
      this.groupBox17.Location = new System.Drawing.Point(8, 6);
      this.groupBox17.Name = "groupBox17";
      this.groupBox17.Size = new System.Drawing.Size(196, 183);
      this.groupBox17.TabIndex = 21;
      this.groupBox17.TabStop = false;
      this.groupBox17.Text = "Track status";
      // 
      // spcGazepointControls
      // 
      this.spcGazepointControls.Dock = System.Windows.Forms.DockStyle.Fill;
      this.spcGazepointControls.Location = new System.Drawing.Point(3, 16);
      this.spcGazepointControls.Name = "spcGazepointControls";
      this.spcGazepointControls.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // spcGazepointControls.Panel1
      // 
      this.spcGazepointControls.Panel1.Controls.Add(this.spcGazepointTrackStatus);
      // 
      // spcGazepointControls.Panel2
      // 
      this.spcGazepointControls.Panel2.Controls.Add(this.spcGazepointCalibPlot);
      this.spcGazepointControls.Size = new System.Drawing.Size(190, 164);
      this.spcGazepointControls.SplitterDistance = 80;
      this.spcGazepointControls.TabIndex = 22;
      // 
      // spcGazepointTrackStatus
      // 
      this.spcGazepointTrackStatus.Dock = System.Windows.Forms.DockStyle.Fill;
      this.spcGazepointTrackStatus.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
      this.spcGazepointTrackStatus.IsSplitterFixed = true;
      this.spcGazepointTrackStatus.Location = new System.Drawing.Point(0, 0);
      this.spcGazepointTrackStatus.Name = "spcGazepointTrackStatus";
      this.spcGazepointTrackStatus.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // spcGazepointTrackStatus.Panel1
      // 
      this.spcGazepointTrackStatus.Panel1.BackColor = System.Drawing.Color.Transparent;
      this.spcGazepointTrackStatus.Panel1.Controls.Add(this.tBGWarning);
      // 
      // spcGazepointTrackStatus.Panel2
      // 
      this.spcGazepointTrackStatus.Panel2.Controls.Add(this.btnGazepointShowOnPresentationScreen);
      this.spcGazepointTrackStatus.Panel2MinSize = 22;
      this.spcGazepointTrackStatus.Size = new System.Drawing.Size(190, 80);
      this.spcGazepointTrackStatus.SplitterDistance = 51;
      this.spcGazepointTrackStatus.TabIndex = 20;
      // 
      // tBGWarning
      // 
      this.tBGWarning.Location = new System.Drawing.Point(0, 0);
      this.tBGWarning.Name = "tBGWarning";
      this.tBGWarning.Size = new System.Drawing.Size(100, 20);
      this.tBGWarning.TabIndex = 0;
      // 
      // btnGazepointShowOnPresentationScreen
      // 
      this.btnGazepointShowOnPresentationScreen.BackColor = System.Drawing.Color.Transparent;
      this.btnGazepointShowOnPresentationScreen.Dock = System.Windows.Forms.DockStyle.Fill;
      this.btnGazepointShowOnPresentationScreen.Enabled = false;
      this.btnGazepointShowOnPresentationScreen.Location = new System.Drawing.Point(0, 0);
      this.btnGazepointShowOnPresentationScreen.Name = "btnGazepointShowOnPresentationScreen";
      this.btnGazepointShowOnPresentationScreen.Size = new System.Drawing.Size(190, 25);
      this.btnGazepointShowOnPresentationScreen.TabIndex = 21;
      this.btnGazepointShowOnPresentationScreen.Text = "Show on presentation screen";
      this.btnGazepointShowOnPresentationScreen.UseVisualStyleBackColor = false;
      // 
      // spcGazepointCalibPlot
      // 
      this.spcGazepointCalibPlot.Dock = System.Windows.Forms.DockStyle.Fill;
      this.spcGazepointCalibPlot.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
      this.spcGazepointCalibPlot.IsSplitterFixed = true;
      this.spcGazepointCalibPlot.Location = new System.Drawing.Point(0, 0);
      this.spcGazepointCalibPlot.Name = "spcGazepointCalibPlot";
      this.spcGazepointCalibPlot.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // spcGazepointCalibPlot.Panel1
      // 
      this.spcGazepointCalibPlot.Panel1.Controls.Add(this.labelCalibrationResultGazepoint);
      // 
      // spcGazepointCalibPlot.Panel2
      // 
      this.spcGazepointCalibPlot.Panel2.Controls.Add(this.btnGazepointRecalibrate);
      this.spcGazepointCalibPlot.Panel2.Controls.Add(this.btnGazepointAcceptCalibration);
      this.spcGazepointCalibPlot.Panel2MinSize = 22;
      this.spcGazepointCalibPlot.Size = new System.Drawing.Size(190, 80);
      this.spcGazepointCalibPlot.SplitterDistance = 51;
      this.spcGazepointCalibPlot.TabIndex = 1;
      // 
      // labelCalibrationResultGazepoint
      // 
      this.labelCalibrationResultGazepoint.AutoSize = true;
      this.labelCalibrationResultGazepoint.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.labelCalibrationResultGazepoint.Location = new System.Drawing.Point(69, 30);
      this.labelCalibrationResultGazepoint.Name = "labelCalibrationResultGazepoint";
      this.labelCalibrationResultGazepoint.Size = new System.Drawing.Size(50, 13);
      this.labelCalibrationResultGazepoint.TabIndex = 2;
      this.labelCalibrationResultGazepoint.Text = "Not Set";
      // 
      // btnGazepointRecalibrate
      // 
      this.btnGazepointRecalibrate.Dock = System.Windows.Forms.DockStyle.Right;
      this.btnGazepointRecalibrate.Location = new System.Drawing.Point(115, 0);
      this.btnGazepointRecalibrate.Name = "btnGazepointRecalibrate";
      this.btnGazepointRecalibrate.Size = new System.Drawing.Size(75, 25);
      this.btnGazepointRecalibrate.TabIndex = 0;
      this.btnGazepointRecalibrate.Text = "Recalibrate";
      this.btnGazepointRecalibrate.UseVisualStyleBackColor = true;
      // 
      // btnGazepointAcceptCalibration
      // 
      this.btnGazepointAcceptCalibration.Dock = System.Windows.Forms.DockStyle.Left;
      this.btnGazepointAcceptCalibration.Location = new System.Drawing.Point(0, 0);
      this.btnGazepointAcceptCalibration.Name = "btnGazepointAcceptCalibration";
      this.btnGazepointAcceptCalibration.Size = new System.Drawing.Size(75, 25);
      this.btnGazepointAcceptCalibration.TabIndex = 0;
      this.btnGazepointAcceptCalibration.Text = "Accept";
      this.btnGazepointAcceptCalibration.UseVisualStyleBackColor = true;
      // 
      // tbpMirametrix
      // 
      this.tbpMirametrix.Controls.Add(this.panel12);
      this.tbpMirametrix.ImageKey = "Mirametrix";
      this.tbpMirametrix.Location = new System.Drawing.Point(4, 23);
      this.tbpMirametrix.Name = "tbpMirametrix";
      this.tbpMirametrix.Padding = new System.Windows.Forms.Padding(3);
      this.tbpMirametrix.Size = new System.Drawing.Size(212, 385);
      this.tbpMirametrix.TabIndex = 11;
      this.tbpMirametrix.Text = "Mirametrix";
      this.tbpMirametrix.UseVisualStyleBackColor = true;
      // 
      // panel12
      // 
      this.panel12.Controls.Add(this.label88);
      this.panel12.Controls.Add(this.label89);
      this.panel12.Controls.Add(this.label90);
      this.panel12.Controls.Add(this.label91);
      this.panel12.Controls.Add(this.groupBox16);
      this.panel12.Controls.Add(this.txbMirametrixSubjectName);
      this.panel12.Controls.Add(this.btnMirametrixRecord);
      this.panel12.Controls.Add(this.btnMirametrixCalibrate);
      this.panel12.Controls.Add(this.btnMirametrixSubjectName);
      this.panel12.Controls.Add(this.btnMirametrixConnect);
      this.panel12.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel12.Location = new System.Drawing.Point(3, 3);
      this.panel12.Name = "panel12";
      this.panel12.Size = new System.Drawing.Size(206, 379);
      this.panel12.TabIndex = 11;
      // 
      // label88
      // 
      this.label88.AutoSize = true;
      this.label88.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label88.Location = new System.Drawing.Point(5, 254);
      this.label88.Name = "label88";
      this.label88.Size = new System.Drawing.Size(25, 24);
      this.label88.TabIndex = 23;
      this.label88.Text = "3.";
      // 
      // label89
      // 
      this.label89.AutoSize = true;
      this.label89.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label89.Location = new System.Drawing.Point(5, 283);
      this.label89.Name = "label89";
      this.label89.Size = new System.Drawing.Size(25, 24);
      this.label89.TabIndex = 24;
      this.label89.Text = "4.";
      // 
      // label90
      // 
      this.label90.AutoSize = true;
      this.label90.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label90.Location = new System.Drawing.Point(5, 225);
      this.label90.Name = "label90";
      this.label90.Size = new System.Drawing.Size(25, 24);
      this.label90.TabIndex = 21;
      this.label90.Text = "2.";
      // 
      // label91
      // 
      this.label91.AutoSize = true;
      this.label91.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label91.Location = new System.Drawing.Point(5, 196);
      this.label91.Margin = new System.Windows.Forms.Padding(0);
      this.label91.Name = "label91";
      this.label91.Size = new System.Drawing.Size(25, 24);
      this.label91.TabIndex = 22;
      this.label91.Text = "1.";
      // 
      // groupBox16
      // 
      this.groupBox16.Controls.Add(this.spcMirametrixControls);
      this.groupBox16.Location = new System.Drawing.Point(5, 3);
      this.groupBox16.Name = "groupBox16";
      this.groupBox16.Size = new System.Drawing.Size(196, 183);
      this.groupBox16.TabIndex = 20;
      this.groupBox16.TabStop = false;
      this.groupBox16.Text = "Track status";
      // 
      // spcMirametrixControls
      // 
      this.spcMirametrixControls.Dock = System.Windows.Forms.DockStyle.Fill;
      this.spcMirametrixControls.Location = new System.Drawing.Point(3, 16);
      this.spcMirametrixControls.Name = "spcMirametrixControls";
      this.spcMirametrixControls.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // spcMirametrixControls.Panel1
      // 
      this.spcMirametrixControls.Panel1.Controls.Add(this.spcMirametrixTrackStatus);
      // 
      // spcMirametrixControls.Panel2
      // 
      this.spcMirametrixControls.Panel2.Controls.Add(this.spcMirametrixCalibPlot);
      this.spcMirametrixControls.Size = new System.Drawing.Size(190, 164);
      this.spcMirametrixControls.SplitterDistance = 80;
      this.spcMirametrixControls.TabIndex = 22;
      // 
      // spcMirametrixTrackStatus
      // 
      this.spcMirametrixTrackStatus.Dock = System.Windows.Forms.DockStyle.Fill;
      this.spcMirametrixTrackStatus.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
      this.spcMirametrixTrackStatus.IsSplitterFixed = true;
      this.spcMirametrixTrackStatus.Location = new System.Drawing.Point(0, 0);
      this.spcMirametrixTrackStatus.Name = "spcMirametrixTrackStatus";
      this.spcMirametrixTrackStatus.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // spcMirametrixTrackStatus.Panel1
      // 
      this.spcMirametrixTrackStatus.Panel1.BackColor = System.Drawing.Color.Transparent;
      this.spcMirametrixTrackStatus.Panel1.Controls.Add(this.tBWarning);
      // 
      // spcMirametrixTrackStatus.Panel2
      // 
      this.spcMirametrixTrackStatus.Panel2.Controls.Add(this.btnMirametrixShowOnPresentationScreen);
      this.spcMirametrixTrackStatus.Panel2MinSize = 22;
      this.spcMirametrixTrackStatus.Size = new System.Drawing.Size(190, 80);
      this.spcMirametrixTrackStatus.SplitterDistance = 51;
      this.spcMirametrixTrackStatus.TabIndex = 20;
      // 
      // tBWarning
      // 
      this.tBWarning.Location = new System.Drawing.Point(0, 0);
      this.tBWarning.Name = "tBWarning";
      this.tBWarning.Size = new System.Drawing.Size(100, 20);
      this.tBWarning.TabIndex = 0;
      // 
      // btnMirametrixShowOnPresentationScreen
      // 
      this.btnMirametrixShowOnPresentationScreen.BackColor = System.Drawing.Color.Transparent;
      this.btnMirametrixShowOnPresentationScreen.Dock = System.Windows.Forms.DockStyle.Fill;
      this.btnMirametrixShowOnPresentationScreen.Enabled = false;
      this.btnMirametrixShowOnPresentationScreen.Location = new System.Drawing.Point(0, 0);
      this.btnMirametrixShowOnPresentationScreen.Name = "btnMirametrixShowOnPresentationScreen";
      this.btnMirametrixShowOnPresentationScreen.Size = new System.Drawing.Size(190, 25);
      this.btnMirametrixShowOnPresentationScreen.TabIndex = 21;
      this.btnMirametrixShowOnPresentationScreen.Text = "Show on presentation screen";
      this.btnMirametrixShowOnPresentationScreen.UseVisualStyleBackColor = false;
      // 
      // spcMirametrixCalibPlot
      // 
      this.spcMirametrixCalibPlot.Dock = System.Windows.Forms.DockStyle.Fill;
      this.spcMirametrixCalibPlot.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
      this.spcMirametrixCalibPlot.IsSplitterFixed = true;
      this.spcMirametrixCalibPlot.Location = new System.Drawing.Point(0, 0);
      this.spcMirametrixCalibPlot.Name = "spcMirametrixCalibPlot";
      this.spcMirametrixCalibPlot.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // spcMirametrixCalibPlot.Panel1
      // 
      this.spcMirametrixCalibPlot.Panel1.Controls.Add(this.labelCalibrationResultMirametrix);
      // 
      // spcMirametrixCalibPlot.Panel2
      // 
      this.spcMirametrixCalibPlot.Panel2.Controls.Add(this.btnMirametrixRecalibrate);
      this.spcMirametrixCalibPlot.Panel2.Controls.Add(this.btnMirametrixAcceptCalibration);
      this.spcMirametrixCalibPlot.Panel2MinSize = 22;
      this.spcMirametrixCalibPlot.Size = new System.Drawing.Size(190, 80);
      this.spcMirametrixCalibPlot.SplitterDistance = 51;
      this.spcMirametrixCalibPlot.TabIndex = 1;
      // 
      // labelCalibrationResultMirametrix
      // 
      this.labelCalibrationResultMirametrix.AutoSize = true;
      this.labelCalibrationResultMirametrix.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.labelCalibrationResultMirametrix.Location = new System.Drawing.Point(69, 30);
      this.labelCalibrationResultMirametrix.Name = "labelCalibrationResultMirametrix";
      this.labelCalibrationResultMirametrix.Size = new System.Drawing.Size(50, 13);
      this.labelCalibrationResultMirametrix.TabIndex = 2;
      this.labelCalibrationResultMirametrix.Text = "Not Set";
      // 
      // btnMirametrixRecalibrate
      // 
      this.btnMirametrixRecalibrate.Dock = System.Windows.Forms.DockStyle.Right;
      this.btnMirametrixRecalibrate.Location = new System.Drawing.Point(115, 0);
      this.btnMirametrixRecalibrate.Name = "btnMirametrixRecalibrate";
      this.btnMirametrixRecalibrate.Size = new System.Drawing.Size(75, 25);
      this.btnMirametrixRecalibrate.TabIndex = 0;
      this.btnMirametrixRecalibrate.Text = "Recalibrate";
      this.btnMirametrixRecalibrate.UseVisualStyleBackColor = true;
      // 
      // btnMirametrixAcceptCalibration
      // 
      this.btnMirametrixAcceptCalibration.Dock = System.Windows.Forms.DockStyle.Left;
      this.btnMirametrixAcceptCalibration.Location = new System.Drawing.Point(0, 0);
      this.btnMirametrixAcceptCalibration.Name = "btnMirametrixAcceptCalibration";
      this.btnMirametrixAcceptCalibration.Size = new System.Drawing.Size(75, 25);
      this.btnMirametrixAcceptCalibration.TabIndex = 0;
      this.btnMirametrixAcceptCalibration.Text = "Accept";
      this.btnMirametrixAcceptCalibration.UseVisualStyleBackColor = true;
      // 
      // txbMirametrixSubjectName
      // 
      this.txbMirametrixSubjectName.Location = new System.Drawing.Point(107, 229);
      this.txbMirametrixSubjectName.Name = "txbMirametrixSubjectName";
      this.txbMirametrixSubjectName.ReadOnly = true;
      this.txbMirametrixSubjectName.Size = new System.Drawing.Size(91, 20);
      this.txbMirametrixSubjectName.TabIndex = 11;
      this.txbMirametrixSubjectName.Text = "Subject1";
      // 
      // btnMirametrixRecord
      // 
      this.btnMirametrixRecord.Enabled = false;
      this.btnMirametrixRecord.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
      this.btnMirametrixRecord.ImageKey = "Record";
      this.btnMirametrixRecord.ImageList = this.imlButtons;
      this.btnMirametrixRecord.Location = new System.Drawing.Point(29, 285);
      this.btnMirametrixRecord.Name = "btnMirametrixRecord";
      this.btnMirametrixRecord.Size = new System.Drawing.Size(72, 23);
      this.btnMirametrixRecord.TabIndex = 17;
      this.btnMirametrixRecord.Text = "&Record";
      this.btnMirametrixRecord.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      // 
      // btnMirametrixCalibrate
      // 
      this.btnMirametrixCalibrate.Enabled = false;
      this.btnMirametrixCalibrate.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
      this.btnMirametrixCalibrate.ImageKey = "Calibrate";
      this.btnMirametrixCalibrate.ImageList = this.imlButtons;
      this.btnMirametrixCalibrate.Location = new System.Drawing.Point(29, 256);
      this.btnMirametrixCalibrate.Name = "btnMirametrixCalibrate";
      this.btnMirametrixCalibrate.Size = new System.Drawing.Size(72, 23);
      this.btnMirametrixCalibrate.TabIndex = 17;
      this.btnMirametrixCalibrate.Text = "&Calibrate";
      this.btnMirametrixCalibrate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      // 
      // btnMirametrixSubjectName
      // 
      this.btnMirametrixSubjectName.Enabled = false;
      this.btnMirametrixSubjectName.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
      this.btnMirametrixSubjectName.ImageKey = "Subject";
      this.btnMirametrixSubjectName.ImageList = this.imlButtons;
      this.btnMirametrixSubjectName.Location = new System.Drawing.Point(29, 228);
      this.btnMirametrixSubjectName.Name = "btnMirametrixSubjectName";
      this.btnMirametrixSubjectName.Size = new System.Drawing.Size(72, 23);
      this.btnMirametrixSubjectName.TabIndex = 12;
      this.btnMirametrixSubjectName.Text = "Subject";
      this.btnMirametrixSubjectName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.btnMirametrixSubjectName.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.btnMirametrixSubjectName.UseVisualStyleBackColor = true;
      // 
      // btnMirametrixConnect
      // 
      this.btnMirametrixConnect.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
      this.btnMirametrixConnect.ImageKey = "Connect";
      this.btnMirametrixConnect.ImageList = this.imlButtons;
      this.btnMirametrixConnect.Location = new System.Drawing.Point(29, 198);
      this.btnMirametrixConnect.Name = "btnMirametrixConnect";
      this.btnMirametrixConnect.Size = new System.Drawing.Size(73, 23);
      this.btnMirametrixConnect.TabIndex = 12;
      this.btnMirametrixConnect.Text = "Connect";
      this.btnMirametrixConnect.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.btnMirametrixConnect.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.btnMirametrixConnect.UseVisualStyleBackColor = true;
      // 
      // tbpAlea
      // 
      this.tbpAlea.Controls.Add(this.groupBox2);
      this.tbpAlea.Controls.Add(this.txbAleaSubjectName);
      this.tbpAlea.Controls.Add(this.btnAleaRecord);
      this.tbpAlea.Controls.Add(this.btnAleaCalibrate);
      this.tbpAlea.Controls.Add(this.label7);
      this.tbpAlea.Controls.Add(this.label8);
      this.tbpAlea.Controls.Add(this.label9);
      this.tbpAlea.Controls.Add(this.label10);
      this.tbpAlea.Controls.Add(this.btnAleaSubjectName);
      this.tbpAlea.Controls.Add(this.btnAleaConnect);
      this.tbpAlea.ImageKey = "Alea";
      this.tbpAlea.Location = new System.Drawing.Point(4, 23);
      this.tbpAlea.Name = "tbpAlea";
      this.tbpAlea.Padding = new System.Windows.Forms.Padding(3);
      this.tbpAlea.Size = new System.Drawing.Size(212, 385);
      this.tbpAlea.TabIndex = 2;
      this.tbpAlea.Text = "Alea";
      this.tbpAlea.UseVisualStyleBackColor = true;
      // 
      // groupBox2
      // 
      this.groupBox2.Controls.Add(this.spcAleaControls);
      this.groupBox2.Location = new System.Drawing.Point(8, 6);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new System.Drawing.Size(196, 183);
      this.groupBox2.TabIndex = 27;
      this.groupBox2.TabStop = false;
      this.groupBox2.Text = "Track status";
      // 
      // spcAleaControls
      // 
      this.spcAleaControls.Dock = System.Windows.Forms.DockStyle.Fill;
      this.spcAleaControls.Location = new System.Drawing.Point(3, 16);
      this.spcAleaControls.Name = "spcAleaControls";
      this.spcAleaControls.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // spcAleaControls.Panel1
      // 
      this.spcAleaControls.Panel1.Controls.Add(this.spcAleaTrackStatus);
      // 
      // spcAleaControls.Panel2
      // 
      this.spcAleaControls.Panel2.Controls.Add(this.spcAleaCalibPlot);
      this.spcAleaControls.Size = new System.Drawing.Size(190, 164);
      this.spcAleaControls.SplitterDistance = 80;
      this.spcAleaControls.TabIndex = 22;
      // 
      // spcAleaTrackStatus
      // 
      this.spcAleaTrackStatus.Dock = System.Windows.Forms.DockStyle.Fill;
      this.spcAleaTrackStatus.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
      this.spcAleaTrackStatus.IsSplitterFixed = true;
      this.spcAleaTrackStatus.Location = new System.Drawing.Point(0, 0);
      this.spcAleaTrackStatus.Name = "spcAleaTrackStatus";
      this.spcAleaTrackStatus.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // spcAleaTrackStatus.Panel1
      // 
      this.spcAleaTrackStatus.Panel1.BackColor = System.Drawing.Color.Transparent;
      // 
      // spcAleaTrackStatus.Panel2
      // 
      this.spcAleaTrackStatus.Panel2.Controls.Add(this.btnAleaShowOnPresentationScreen);
      this.spcAleaTrackStatus.Panel2MinSize = 22;
      this.spcAleaTrackStatus.Size = new System.Drawing.Size(190, 80);
      this.spcAleaTrackStatus.SplitterDistance = 51;
      this.spcAleaTrackStatus.TabIndex = 20;
      // 
      // btnAleaShowOnPresentationScreen
      // 
      this.btnAleaShowOnPresentationScreen.BackColor = System.Drawing.Color.Transparent;
      this.btnAleaShowOnPresentationScreen.Dock = System.Windows.Forms.DockStyle.Fill;
      this.btnAleaShowOnPresentationScreen.Enabled = false;
      this.btnAleaShowOnPresentationScreen.Location = new System.Drawing.Point(0, 0);
      this.btnAleaShowOnPresentationScreen.Name = "btnAleaShowOnPresentationScreen";
      this.btnAleaShowOnPresentationScreen.Size = new System.Drawing.Size(190, 25);
      this.btnAleaShowOnPresentationScreen.TabIndex = 21;
      this.btnAleaShowOnPresentationScreen.Text = "Show on presentation screen";
      this.btnAleaShowOnPresentationScreen.UseVisualStyleBackColor = false;
      // 
      // spcAleaCalibPlot
      // 
      this.spcAleaCalibPlot.Dock = System.Windows.Forms.DockStyle.Fill;
      this.spcAleaCalibPlot.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
      this.spcAleaCalibPlot.IsSplitterFixed = true;
      this.spcAleaCalibPlot.Location = new System.Drawing.Point(0, 0);
      this.spcAleaCalibPlot.Name = "spcAleaCalibPlot";
      this.spcAleaCalibPlot.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // spcAleaCalibPlot.Panel1
      // 
      this.spcAleaCalibPlot.Panel1.BackColor = System.Drawing.Color.Transparent;
      this.spcAleaCalibPlot.Panel1.Controls.Add(this.labelCalibrationResult);
      this.spcAleaCalibPlot.Panel1.Controls.Add(this.label11);
      // 
      // spcAleaCalibPlot.Panel2
      // 
      this.spcAleaCalibPlot.Panel2.Controls.Add(this.btnAleaRecalibrate);
      this.spcAleaCalibPlot.Panel2.Controls.Add(this.btnAleaAcceptCalibration);
      this.spcAleaCalibPlot.Panel2MinSize = 22;
      this.spcAleaCalibPlot.Size = new System.Drawing.Size(190, 80);
      this.spcAleaCalibPlot.SplitterDistance = 51;
      this.spcAleaCalibPlot.TabIndex = 1;
      // 
      // labelCalibrationResult
      // 
      this.labelCalibrationResult.AutoSize = true;
      this.labelCalibrationResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.labelCalibrationResult.Location = new System.Drawing.Point(69, 32);
      this.labelCalibrationResult.Name = "labelCalibrationResult";
      this.labelCalibrationResult.Size = new System.Drawing.Size(50, 13);
      this.labelCalibrationResult.TabIndex = 1;
      this.labelCalibrationResult.Text = "Not Set";
      // 
      // label11
      // 
      this.label11.AutoSize = true;
      this.label11.Location = new System.Drawing.Point(50, 10);
      this.label11.Name = "label11";
      this.label11.Size = new System.Drawing.Size(89, 13);
      this.label11.TabIndex = 0;
      this.label11.Text = "Calibration Result";
      // 
      // btnAleaRecalibrate
      // 
      this.btnAleaRecalibrate.Dock = System.Windows.Forms.DockStyle.Right;
      this.btnAleaRecalibrate.Location = new System.Drawing.Point(115, 0);
      this.btnAleaRecalibrate.Name = "btnAleaRecalibrate";
      this.btnAleaRecalibrate.Size = new System.Drawing.Size(75, 25);
      this.btnAleaRecalibrate.TabIndex = 0;
      this.btnAleaRecalibrate.Text = "Recalibrate";
      this.btnAleaRecalibrate.UseVisualStyleBackColor = true;
      // 
      // btnAleaAcceptCalibration
      // 
      this.btnAleaAcceptCalibration.Dock = System.Windows.Forms.DockStyle.Left;
      this.btnAleaAcceptCalibration.Location = new System.Drawing.Point(0, 0);
      this.btnAleaAcceptCalibration.Name = "btnAleaAcceptCalibration";
      this.btnAleaAcceptCalibration.Size = new System.Drawing.Size(75, 25);
      this.btnAleaAcceptCalibration.TabIndex = 0;
      this.btnAleaAcceptCalibration.Text = "Accept";
      this.btnAleaAcceptCalibration.UseVisualStyleBackColor = true;
      // 
      // txbAleaSubjectName
      // 
      this.txbAleaSubjectName.Location = new System.Drawing.Point(110, 232);
      this.txbAleaSubjectName.Name = "txbAleaSubjectName";
      this.txbAleaSubjectName.ReadOnly = true;
      this.txbAleaSubjectName.Size = new System.Drawing.Size(91, 20);
      this.txbAleaSubjectName.TabIndex = 18;
      this.txbAleaSubjectName.Text = "Subject1";
      // 
      // btnAleaRecord
      // 
      this.btnAleaRecord.Enabled = false;
      this.btnAleaRecord.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
      this.btnAleaRecord.ImageKey = "Record";
      this.btnAleaRecord.ImageList = this.imlButtons;
      this.btnAleaRecord.Location = new System.Drawing.Point(32, 288);
      this.btnAleaRecord.Name = "btnAleaRecord";
      this.btnAleaRecord.Size = new System.Drawing.Size(72, 23);
      this.btnAleaRecord.TabIndex = 26;
      this.btnAleaRecord.Text = "&Record";
      this.btnAleaRecord.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      // 
      // btnAleaCalibrate
      // 
      this.btnAleaCalibrate.Enabled = false;
      this.btnAleaCalibrate.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
      this.btnAleaCalibrate.ImageKey = "Calibrate";
      this.btnAleaCalibrate.ImageList = this.imlButtons;
      this.btnAleaCalibrate.Location = new System.Drawing.Point(32, 259);
      this.btnAleaCalibrate.Name = "btnAleaCalibrate";
      this.btnAleaCalibrate.Size = new System.Drawing.Size(72, 23);
      this.btnAleaCalibrate.TabIndex = 25;
      this.btnAleaCalibrate.Text = "&Calibrate";
      this.btnAleaCalibrate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      // 
      // label7
      // 
      this.label7.AutoSize = true;
      this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label7.Location = new System.Drawing.Point(8, 259);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(25, 24);
      this.label7.TabIndex = 24;
      this.label7.Text = "3.";
      // 
      // label8
      // 
      this.label8.AutoSize = true;
      this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label8.Location = new System.Drawing.Point(8, 288);
      this.label8.Name = "label8";
      this.label8.Size = new System.Drawing.Size(25, 24);
      this.label8.TabIndex = 22;
      this.label8.Text = "4.";
      // 
      // label9
      // 
      this.label9.AutoSize = true;
      this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label9.Location = new System.Drawing.Point(8, 230);
      this.label9.Name = "label9";
      this.label9.Size = new System.Drawing.Size(25, 24);
      this.label9.TabIndex = 23;
      this.label9.Text = "2.";
      // 
      // label10
      // 
      this.label10.AutoSize = true;
      this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label10.Location = new System.Drawing.Point(8, 201);
      this.label10.Margin = new System.Windows.Forms.Padding(0);
      this.label10.Name = "label10";
      this.label10.Size = new System.Drawing.Size(25, 24);
      this.label10.TabIndex = 21;
      this.label10.Text = "1.";
      // 
      // btnAleaSubjectName
      // 
      this.btnAleaSubjectName.Enabled = false;
      this.btnAleaSubjectName.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
      this.btnAleaSubjectName.ImageKey = "Subject";
      this.btnAleaSubjectName.ImageList = this.imlButtons;
      this.btnAleaSubjectName.Location = new System.Drawing.Point(32, 231);
      this.btnAleaSubjectName.Name = "btnAleaSubjectName";
      this.btnAleaSubjectName.Size = new System.Drawing.Size(72, 23);
      this.btnAleaSubjectName.TabIndex = 20;
      this.btnAleaSubjectName.Text = "Subject";
      this.btnAleaSubjectName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.btnAleaSubjectName.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.btnAleaSubjectName.UseVisualStyleBackColor = true;
      // 
      // btnAleaConnect
      // 
      this.btnAleaConnect.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
      this.btnAleaConnect.ImageKey = "Connect";
      this.btnAleaConnect.ImageList = this.imlButtons;
      this.btnAleaConnect.Location = new System.Drawing.Point(32, 201);
      this.btnAleaConnect.Name = "btnAleaConnect";
      this.btnAleaConnect.Size = new System.Drawing.Size(73, 23);
      this.btnAleaConnect.TabIndex = 19;
      this.btnAleaConnect.Text = "Connect";
      this.btnAleaConnect.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.btnAleaConnect.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.btnAleaConnect.UseVisualStyleBackColor = true;
      // 
      // tbpSMI
      // 
      this.tbpSMIiViewX.Controls.Add(this.groupBox3);
      this.tbpSMIiViewX.Controls.Add(this.txbSMIiViewXSubjectName);
      this.tbpSMIiViewX.Controls.Add(this.btnSMIiViewXRecord);
      this.tbpSMIiViewX.Controls.Add(this.btnSMIiViewXCalibrate);
      this.tbpSMIiViewX.Controls.Add(this.label13);
      this.tbpSMIiViewX.Controls.Add(this.label14);
      this.tbpSMIiViewX.Controls.Add(this.label15);
      this.tbpSMIiViewX.Controls.Add(this.label16);
      this.tbpSMIiViewX.Controls.Add(this.btnSMIiViewXSubjectName);
      this.tbpSMIiViewX.Controls.Add(this.btnSMIiViewXConnect);
      this.tbpSMIiViewX.ImageKey = "SMI";
      this.tbpSMIiViewX.Location = new System.Drawing.Point(4, 23);
      this.tbpSMIiViewX.Name = "tbpSMIiViewX";
      this.tbpSMIiViewX.Padding = new System.Windows.Forms.Padding(3);
      this.tbpSMIiViewX.Size = new System.Drawing.Size(212, 385);
      this.tbpSMIiViewX.TabIndex = 4;
      this.tbpSMIiViewX.Text = "SMI iViewX";
      this.tbpSMIiViewX.UseVisualStyleBackColor = true;
      // 
      // groupBox3
      // 
      this.groupBox3.Controls.Add(this.textBox1);
      this.groupBox3.Location = new System.Drawing.Point(8, 8);
      this.groupBox3.Name = "groupBox3";
      this.groupBox3.Size = new System.Drawing.Size(196, 183);
      this.groupBox3.TabIndex = 30;
      this.groupBox3.TabStop = false;
      this.groupBox3.Text = "Track status";
      // 
      // textBox1
      // 
      this.textBox1.Location = new System.Drawing.Point(6, 19);
      this.textBox1.Multiline = true;
      this.textBox1.Name = "textBox1";
      this.textBox1.Size = new System.Drawing.Size(184, 137);
      this.textBox1.TabIndex = 0;
      this.textBox1.Text = resources.GetString("textBox1.Text");
      // 
      // txbSMIiViewXSubjectName
      // 
      this.txbSMIiViewXSubjectName.Location = new System.Drawing.Point(110, 234);
      this.txbSMIiViewXSubjectName.Name = "txbSMIiViewXSubjectName";
      this.txbSMIiViewXSubjectName.ReadOnly = true;
      this.txbSMIiViewXSubjectName.Size = new System.Drawing.Size(91, 20);
      this.txbSMIiViewXSubjectName.TabIndex = 21;
      this.txbSMIiViewXSubjectName.Text = "Subject1";
      // 
      // btnSMIiViewXRecord
      // 
      this.btnSMIiViewXRecord.Enabled = false;
      this.btnSMIiViewXRecord.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
      this.btnSMIiViewXRecord.ImageKey = "Record";
      this.btnSMIiViewXRecord.ImageList = this.imlButtons;
      this.btnSMIiViewXRecord.Location = new System.Drawing.Point(32, 290);
      this.btnSMIiViewXRecord.Name = "btnSMIiViewXRecord";
      this.btnSMIiViewXRecord.Size = new System.Drawing.Size(72, 23);
      this.btnSMIiViewXRecord.TabIndex = 28;
      this.btnSMIiViewXRecord.Text = "&Record";
      this.btnSMIiViewXRecord.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      // 
      // btnSMIiViewXCalibrate
      // 
      this.btnSMIiViewXCalibrate.Enabled = false;
      this.btnSMIiViewXCalibrate.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
      this.btnSMIiViewXCalibrate.ImageKey = "Calibrate";
      this.btnSMIiViewXCalibrate.ImageList = this.imlButtons;
      this.btnSMIiViewXCalibrate.Location = new System.Drawing.Point(32, 261);
      this.btnSMIiViewXCalibrate.Name = "btnSMIiViewXCalibrate";
      this.btnSMIiViewXCalibrate.Size = new System.Drawing.Size(72, 23);
      this.btnSMIiViewXCalibrate.TabIndex = 29;
      this.btnSMIiViewXCalibrate.Text = "&Calibrate";
      this.btnSMIiViewXCalibrate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      // 
      // label13
      // 
      this.label13.AutoSize = true;
      this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label13.Location = new System.Drawing.Point(8, 261);
      this.label13.Name = "label13";
      this.label13.Size = new System.Drawing.Size(25, 24);
      this.label13.TabIndex = 27;
      this.label13.Text = "3.";
      // 
      // label14
      // 
      this.label14.AutoSize = true;
      this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label14.Location = new System.Drawing.Point(8, 290);
      this.label14.Name = "label14";
      this.label14.Size = new System.Drawing.Size(25, 24);
      this.label14.TabIndex = 26;
      this.label14.Text = "4.";
      // 
      // label15
      // 
      this.label15.AutoSize = true;
      this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label15.Location = new System.Drawing.Point(8, 232);
      this.label15.Name = "label15";
      this.label15.Size = new System.Drawing.Size(25, 24);
      this.label15.TabIndex = 24;
      this.label15.Text = "2.";
      // 
      // label16
      // 
      this.label16.AutoSize = true;
      this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label16.Location = new System.Drawing.Point(8, 203);
      this.label16.Margin = new System.Windows.Forms.Padding(0);
      this.label16.Name = "label16";
      this.label16.Size = new System.Drawing.Size(25, 24);
      this.label16.TabIndex = 25;
      this.label16.Text = "1.";
      // 
      // btnSMIiViewXSubjectName
      // 
      this.btnSMIiViewXSubjectName.Enabled = false;
      this.btnSMIiViewXSubjectName.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
      this.btnSMIiViewXSubjectName.ImageKey = "Subject";
      this.btnSMIiViewXSubjectName.ImageList = this.imlButtons;
      this.btnSMIiViewXSubjectName.Location = new System.Drawing.Point(32, 233);
      this.btnSMIiViewXSubjectName.Name = "btnSMIiViewXSubjectName";
      this.btnSMIiViewXSubjectName.Size = new System.Drawing.Size(72, 23);
      this.btnSMIiViewXSubjectName.TabIndex = 22;
      this.btnSMIiViewXSubjectName.Text = "Subject";
      this.btnSMIiViewXSubjectName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.btnSMIiViewXSubjectName.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.btnSMIiViewXSubjectName.UseVisualStyleBackColor = true;
      // 
      // btnSMIiViewXConnect
      // 
      this.btnSMIiViewXConnect.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
      this.btnSMIiViewXConnect.ImageKey = "Connect";
      this.btnSMIiViewXConnect.ImageList = this.imlButtons;
      this.btnSMIiViewXConnect.Location = new System.Drawing.Point(32, 203);
      this.btnSMIiViewXConnect.Name = "btnSMIiViewXConnect";
      this.btnSMIiViewXConnect.Size = new System.Drawing.Size(73, 23);
      this.btnSMIiViewXConnect.TabIndex = 23;
      this.btnSMIiViewXConnect.Text = "Connect";
      this.btnSMIiViewXConnect.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.btnSMIiViewXConnect.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.btnSMIiViewXConnect.UseVisualStyleBackColor = true;
      // 
      // tbpAsl
      // 
      this.tbpAsl.Controls.Add(this.txbAslSubjectName);
      this.tbpAsl.Controls.Add(this.btnAslRecord);
      this.tbpAsl.Controls.Add(this.btnAslCalibrate);
      this.tbpAsl.Controls.Add(this.label75);
      this.tbpAsl.Controls.Add(this.recordLabel);
      this.tbpAsl.Controls.Add(this.label76);
      this.tbpAsl.Controls.Add(this.connectLabel);
      this.tbpAsl.Controls.Add(this.btnAslSubjectName);
      this.tbpAsl.Controls.Add(this.btnAslConnect);
      this.tbpAsl.ImageKey = "ASL";
      this.tbpAsl.Location = new System.Drawing.Point(4, 23);
      this.tbpAsl.Name = "tbpAsl";
      this.tbpAsl.Padding = new System.Windows.Forms.Padding(3);
      this.tbpAsl.Size = new System.Drawing.Size(212, 385);
      this.tbpAsl.TabIndex = 7;
      this.tbpAsl.Text = "ASL";
      this.tbpAsl.UseVisualStyleBackColor = true;
      // 
      // txbAslSubjectName
      // 
      this.txbAslSubjectName.Location = new System.Drawing.Point(110, 269);
      this.txbAslSubjectName.Name = "txbAslSubjectName";
      this.txbAslSubjectName.ReadOnly = true;
      this.txbAslSubjectName.Size = new System.Drawing.Size(91, 20);
      this.txbAslSubjectName.TabIndex = 31;
      this.txbAslSubjectName.Text = "Subject1";
      // 
      // btnAslRecord
      // 
      this.btnAslRecord.Enabled = false;
      this.btnAslRecord.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
      this.btnAslRecord.ImageKey = "Record";
      this.btnAslRecord.ImageList = this.imlButtons;
      this.btnAslRecord.Location = new System.Drawing.Point(32, 325);
      this.btnAslRecord.Name = "btnAslRecord";
      this.btnAslRecord.Size = new System.Drawing.Size(72, 23);
      this.btnAslRecord.TabIndex = 38;
      this.btnAslRecord.Text = "&Record";
      this.btnAslRecord.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      // 
      // btnAslCalibrate
      // 
      this.btnAslCalibrate.Enabled = false;
      this.btnAslCalibrate.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
      this.btnAslCalibrate.ImageKey = "Calibrate";
      this.btnAslCalibrate.ImageList = this.imlButtons;
      this.btnAslCalibrate.Location = new System.Drawing.Point(32, 296);
      this.btnAslCalibrate.Name = "btnAslCalibrate";
      this.btnAslCalibrate.Size = new System.Drawing.Size(72, 23);
      this.btnAslCalibrate.TabIndex = 39;
      this.btnAslCalibrate.Text = "&Calibrate";
      this.btnAslCalibrate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      // 
      // label75
      // 
      this.label75.AutoSize = true;
      this.label75.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label75.Location = new System.Drawing.Point(8, 296);
      this.label75.Name = "label75";
      this.label75.Size = new System.Drawing.Size(25, 24);
      this.label75.TabIndex = 37;
      this.label75.Text = "3.";
      // 
      // recordLabel
      // 
      this.recordLabel.AutoSize = true;
      this.recordLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.recordLabel.Location = new System.Drawing.Point(8, 325);
      this.recordLabel.Name = "recordLabel";
      this.recordLabel.Size = new System.Drawing.Size(25, 24);
      this.recordLabel.TabIndex = 36;
      this.recordLabel.Text = "4.";
      // 
      // label76
      // 
      this.label76.AutoSize = true;
      this.label76.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label76.Location = new System.Drawing.Point(8, 267);
      this.label76.Name = "label76";
      this.label76.Size = new System.Drawing.Size(25, 24);
      this.label76.TabIndex = 34;
      this.label76.Text = "2.";
      // 
      // connectLabel
      // 
      this.connectLabel.AutoSize = true;
      this.connectLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.connectLabel.Location = new System.Drawing.Point(8, 238);
      this.connectLabel.Margin = new System.Windows.Forms.Padding(0);
      this.connectLabel.Name = "connectLabel";
      this.connectLabel.Size = new System.Drawing.Size(25, 24);
      this.connectLabel.TabIndex = 35;
      this.connectLabel.Text = "1.";
      // 
      // btnAslSubjectName
      // 
      this.btnAslSubjectName.Enabled = false;
      this.btnAslSubjectName.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
      this.btnAslSubjectName.ImageKey = "Subject";
      this.btnAslSubjectName.ImageList = this.imlButtons;
      this.btnAslSubjectName.Location = new System.Drawing.Point(32, 268);
      this.btnAslSubjectName.Name = "btnAslSubjectName";
      this.btnAslSubjectName.Size = new System.Drawing.Size(72, 23);
      this.btnAslSubjectName.TabIndex = 32;
      this.btnAslSubjectName.Text = "Subject";
      this.btnAslSubjectName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.btnAslSubjectName.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.btnAslSubjectName.UseVisualStyleBackColor = true;
      // 
      // btnAslConnect
      // 
      this.btnAslConnect.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
      this.btnAslConnect.ImageKey = "Connect";
      this.btnAslConnect.ImageList = this.imlButtons;
      this.btnAslConnect.Location = new System.Drawing.Point(32, 238);
      this.btnAslConnect.Name = "btnAslConnect";
      this.btnAslConnect.Size = new System.Drawing.Size(73, 23);
      this.btnAslConnect.TabIndex = 33;
      this.btnAslConnect.Text = "Connect";
      this.btnAslConnect.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.btnAslConnect.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.btnAslConnect.UseVisualStyleBackColor = true;
      // 
      // tbpEyeTech
      // 
      this.tbpEyeTech.Controls.Add(this.groupBox20);
      this.tbpEyeTech.Controls.Add(this.txbEyeTechSubjectName);
      this.tbpEyeTech.Controls.Add(this.btnEyeTechRecord);
      this.tbpEyeTech.Controls.Add(this.btnEyeTechCalibrate);
      this.tbpEyeTech.Controls.Add(this.label84);
      this.tbpEyeTech.Controls.Add(this.label85);
      this.tbpEyeTech.Controls.Add(this.label86);
      this.tbpEyeTech.Controls.Add(this.label87);
      this.tbpEyeTech.Controls.Add(this.btnEyeTechSubjectName);
      this.tbpEyeTech.Controls.Add(this.btnEyeTechConnect);
      this.tbpEyeTech.ImageKey = "SMI";
      this.tbpEyeTech.Location = new System.Drawing.Point(4, 23);
      this.tbpEyeTech.Name = "tbpEyeTech";
      this.tbpEyeTech.Padding = new System.Windows.Forms.Padding(3);
      this.tbpEyeTech.Size = new System.Drawing.Size(212, 385);
      this.tbpEyeTech.TabIndex = 8;
      this.tbpEyeTech.Text = "EyeTech";
      this.tbpEyeTech.UseVisualStyleBackColor = true;
      // 
      // groupBox20
      // 
      this.groupBox20.Controls.Add(this.textBox15);
      this.groupBox20.Location = new System.Drawing.Point(8, 8);
      this.groupBox20.Name = "groupBox20";
      this.groupBox20.Size = new System.Drawing.Size(196, 183);
      this.groupBox20.TabIndex = 30;
      this.groupBox20.TabStop = false;
      this.groupBox20.Text = "Track status";
      // 
      // textBox15
      // 
      this.textBox15.Location = new System.Drawing.Point(6, 19);
      this.textBox15.Multiline = true;
      this.textBox15.Name = "textBox15";
      this.textBox15.Size = new System.Drawing.Size(184, 137);
      this.textBox15.TabIndex = 0;
      // 
      // txbEyeTechSubjectName
      // 
      this.txbEyeTechSubjectName.Location = new System.Drawing.Point(110, 234);
      this.txbEyeTechSubjectName.Name = "txbEyeTechSubjectName";
      this.txbEyeTechSubjectName.ReadOnly = true;
      this.txbEyeTechSubjectName.Size = new System.Drawing.Size(91, 20);
      this.txbEyeTechSubjectName.TabIndex = 21;
      this.txbEyeTechSubjectName.Text = "Subject1";
      // 
      // btnEyeTechRecord
      // 
      this.btnEyeTechRecord.Enabled = false;
      this.btnEyeTechRecord.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
      this.btnEyeTechRecord.ImageKey = "Record";
      this.btnEyeTechRecord.ImageList = this.imlButtons;
      this.btnEyeTechRecord.Location = new System.Drawing.Point(32, 290);
      this.btnEyeTechRecord.Name = "btnEyeTechRecord";
      this.btnEyeTechRecord.Size = new System.Drawing.Size(72, 23);
      this.btnEyeTechRecord.TabIndex = 28;
      this.btnEyeTechRecord.Text = "&Record";
      this.btnEyeTechRecord.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      // 
      // btnEyeTechCalibrate
      // 
      this.btnEyeTechCalibrate.Enabled = false;
      this.btnEyeTechCalibrate.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
      this.btnEyeTechCalibrate.ImageKey = "Calibrate";
      this.btnEyeTechCalibrate.ImageList = this.imlButtons;
      this.btnEyeTechCalibrate.Location = new System.Drawing.Point(32, 261);
      this.btnEyeTechCalibrate.Name = "btnEyeTechCalibrate";
      this.btnEyeTechCalibrate.Size = new System.Drawing.Size(72, 23);
      this.btnEyeTechCalibrate.TabIndex = 29;
      this.btnEyeTechCalibrate.Text = "&Calibrate";
      this.btnEyeTechCalibrate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      // 
      // label84
      // 
      this.label84.AutoSize = true;
      this.label84.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label84.Location = new System.Drawing.Point(5, 256);
      this.label84.Name = "label84";
      this.label84.Size = new System.Drawing.Size(25, 24);
      this.label84.TabIndex = 13;
      this.label84.Text = "3.";
      // 
      // label85
      // 
      this.label85.AutoSize = true;
      this.label85.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label85.Location = new System.Drawing.Point(5, 285);
      this.label85.Name = "label85";
      this.label85.Size = new System.Drawing.Size(25, 24);
      this.label85.TabIndex = 13;
      this.label85.Text = "4.";
      // 
      // label86
      // 
      this.label86.AutoSize = true;
      this.label86.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label86.Location = new System.Drawing.Point(5, 227);
      this.label86.Name = "label86";
      this.label86.Size = new System.Drawing.Size(25, 24);
      this.label86.TabIndex = 13;
      this.label86.Text = "2.";
      // 
      // label87
      // 
      this.label87.AutoSize = true;
      this.label87.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label87.Location = new System.Drawing.Point(5, 198);
      this.label87.Margin = new System.Windows.Forms.Padding(0);
      this.label87.Name = "label87";
      this.label87.Size = new System.Drawing.Size(25, 24);
      this.label87.TabIndex = 13;
      this.label87.Text = "1.";
      // 
      // btnEyeTechSubjectName
      // 
      this.btnEyeTechSubjectName.Enabled = false;
      this.btnEyeTechSubjectName.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
      this.btnEyeTechSubjectName.ImageKey = "Subject";
      this.btnEyeTechSubjectName.ImageList = this.imlButtons;
      this.btnEyeTechSubjectName.Location = new System.Drawing.Point(32, 233);
      this.btnEyeTechSubjectName.Name = "btnEyeTechSubjectName";
      this.btnEyeTechSubjectName.Size = new System.Drawing.Size(72, 23);
      this.btnEyeTechSubjectName.TabIndex = 22;
      this.btnEyeTechSubjectName.Text = "Subject";
      this.btnEyeTechSubjectName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.btnEyeTechSubjectName.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.btnEyeTechSubjectName.UseVisualStyleBackColor = true;
      // 
      // btnEyeTechConnect
      // 
      this.btnEyeTechConnect.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
      this.btnEyeTechConnect.ImageKey = "Connect";
      this.btnEyeTechConnect.ImageList = this.imlButtons;
      this.btnEyeTechConnect.Location = new System.Drawing.Point(32, 203);
      this.btnEyeTechConnect.Name = "btnEyeTechConnect";
      this.btnEyeTechConnect.Size = new System.Drawing.Size(73, 23);
      this.btnEyeTechConnect.TabIndex = 23;
      this.btnEyeTechConnect.Text = "Connect";
      this.btnEyeTechConnect.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.btnEyeTechConnect.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.btnEyeTechConnect.UseVisualStyleBackColor = true;
      // 
      // tbpMouseOnly
      // 
      this.tbpMouseOnly.Controls.Add(this.label5);
      this.tbpMouseOnly.Controls.Add(this.label6);
      this.tbpMouseOnly.Controls.Add(this.btnMouseOnlySubject);
      this.tbpMouseOnly.Controls.Add(this.txbMouseOnlySubjectName);
      this.tbpMouseOnly.Controls.Add(this.btnMouseOnlyRecord);
      this.tbpMouseOnly.ImageKey = "Mouse";
      this.tbpMouseOnly.Location = new System.Drawing.Point(4, 23);
      this.tbpMouseOnly.Name = "tbpMouseOnly";
      this.tbpMouseOnly.Padding = new System.Windows.Forms.Padding(3);
      this.tbpMouseOnly.Size = new System.Drawing.Size(212, 385);
      this.tbpMouseOnly.TabIndex = 1;
      this.tbpMouseOnly.Text = "MouseOnly";
      this.tbpMouseOnly.UseVisualStyleBackColor = true;
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label5.Location = new System.Drawing.Point(7, 42);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(25, 24);
      this.label5.TabIndex = 21;
      this.label5.Text = "2.";
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label6.Location = new System.Drawing.Point(7, 13);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(25, 24);
      this.label6.TabIndex = 20;
      this.label6.Text = "1.";
      // 
      // btnMouseOnlySubject
      // 
      this.btnMouseOnlySubject.ImageKey = "Subject";
      this.btnMouseOnlySubject.ImageList = this.imlButtons;
      this.btnMouseOnlySubject.Location = new System.Drawing.Point(32, 13);
      this.btnMouseOnlySubject.Name = "btnMouseOnlySubject";
      this.btnMouseOnlySubject.Size = new System.Drawing.Size(71, 23);
      this.btnMouseOnlySubject.TabIndex = 19;
      this.btnMouseOnlySubject.Text = "Subject";
      this.btnMouseOnlySubject.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.btnMouseOnlySubject.UseVisualStyleBackColor = true;
      // 
      // txbMouseOnlySubjectName
      // 
      this.txbMouseOnlySubjectName.Location = new System.Drawing.Point(115, 15);
      this.txbMouseOnlySubjectName.Name = "txbMouseOnlySubjectName";
      this.txbMouseOnlySubjectName.ReadOnly = true;
      this.txbMouseOnlySubjectName.Size = new System.Drawing.Size(87, 20);
      this.txbMouseOnlySubjectName.TabIndex = 18;
      this.txbMouseOnlySubjectName.Text = "Subject1";
      // 
      // btnMouseOnlyRecord
      // 
      this.btnMouseOnlyRecord.BackColor = System.Drawing.Color.Transparent;
      this.btnMouseOnlyRecord.Enabled = false;
      this.btnMouseOnlyRecord.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
      this.btnMouseOnlyRecord.ImageKey = "Record";
      this.btnMouseOnlyRecord.ImageList = this.imlButtons;
      this.btnMouseOnlyRecord.Location = new System.Drawing.Point(32, 42);
      this.btnMouseOnlyRecord.Name = "btnMouseOnlyRecord";
      this.btnMouseOnlyRecord.Size = new System.Drawing.Size(71, 23);
      this.btnMouseOnlyRecord.TabIndex = 14;
      this.btnMouseOnlyRecord.Text = "Record";
      this.btnMouseOnlyRecord.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.btnMouseOnlyRecord.UseVisualStyleBackColor = false;
      // 
      // tbpNoDevice
      // 
      this.tbpNoDevice.Controls.Add(this.label12);
      this.tbpNoDevice.Controls.Add(this.btnNoDeviceTabSelectTracker);
      this.tbpNoDevice.ImageKey = "None";
      this.tbpNoDevice.Location = new System.Drawing.Point(4, 23);
      this.tbpNoDevice.Name = "tbpNoDevice";
      this.tbpNoDevice.Padding = new System.Windows.Forms.Padding(3);
      this.tbpNoDevice.Size = new System.Drawing.Size(212, 385);
      this.tbpNoDevice.TabIndex = 3;
      this.tbpNoDevice.Text = "No tracking device selected";
      this.tbpNoDevice.UseVisualStyleBackColor = true;
      // 
      // label12
      // 
      this.label12.Location = new System.Drawing.Point(12, 10);
      this.label12.Name = "label12";
      this.label12.Size = new System.Drawing.Size(194, 46);
      this.label12.TabIndex = 1;
      this.label12.Text = "No tracking device available.\r\nClick the button to see which tracking devices are" +
    " supported/available.";
      // 
      // btnNoDeviceTabSelectTracker
      // 
      this.btnNoDeviceTabSelectTracker.Image = global::Ogama.Properties.Resources.LegendHS;
      this.btnNoDeviceTabSelectTracker.Location = new System.Drawing.Point(27, 59);
      this.btnNoDeviceTabSelectTracker.Name = "btnNoDeviceTabSelectTracker";
      this.btnNoDeviceTabSelectTracker.Size = new System.Drawing.Size(149, 34);
      this.btnNoDeviceTabSelectTracker.TabIndex = 0;
      this.btnNoDeviceTabSelectTracker.Text = "Select tracking device";
      this.btnNoDeviceTabSelectTracker.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.btnNoDeviceTabSelectTracker.UseVisualStyleBackColor = true;
      this.btnNoDeviceTabSelectTracker.Click += new System.EventHandler(this.BtnNoDeviceTabSelectTrackerClick);
      // 
      // imlEyetracker
      // 
      this.imlEyetracker.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imlEyetracker.ImageStream")));
      this.imlEyetracker.TransparentColor = System.Drawing.Color.Transparent;
      this.imlEyetracker.Images.SetKeyName(0, "Tobii");
      this.imlEyetracker.Images.SetKeyName(1, "Mouse");
      this.imlEyetracker.Images.SetKeyName(2, "Alea");
      this.imlEyetracker.Images.SetKeyName(3, "None");
      this.imlEyetracker.Images.SetKeyName(4, "SMI");
      this.imlEyetracker.Images.SetKeyName(5, "ASL");
      this.imlEyetracker.Images.SetKeyName(6, "ITUPS3");
      this.imlEyetracker.Images.SetKeyName(7, "ITU");
      this.imlEyetracker.Images.SetKeyName(8, "Mirametrix");
      this.imlEyetracker.Images.SetKeyName(9, "Gazepoint_GP3");
      // 
      // panel4
      // 
      this.panel4.Controls.Add(this.lblRecordedTime);
      this.panel4.Controls.Add(this.label21);
      this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel4.Location = new System.Drawing.Point(0, 0);
      this.panel4.Name = "panel4";
      this.panel4.Size = new System.Drawing.Size(220, 25);
      this.panel4.TabIndex = 0;
      // 
      // lblRecordedTime
      // 
      this.lblRecordedTime.Dock = System.Windows.Forms.DockStyle.Right;
      this.lblRecordedTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblRecordedTime.Location = new System.Drawing.Point(120, 0);
      this.lblRecordedTime.Name = "lblRecordedTime";
      this.lblRecordedTime.Padding = new System.Windows.Forms.Padding(0, 0, 20, 0);
      this.lblRecordedTime.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
      this.lblRecordedTime.Size = new System.Drawing.Size(100, 25);
      this.lblRecordedTime.TabIndex = 1;
      this.lblRecordedTime.Text = "00:00:00";
      this.lblRecordedTime.TextAlign = System.Drawing.ContentAlignment.TopRight;
      // 
      // label21
      // 
      this.label21.AutoSize = true;
      this.label21.Location = new System.Drawing.Point(29, 7);
      this.label21.Name = "label21";
      this.label21.Size = new System.Drawing.Size(79, 13);
      this.label21.TabIndex = 0;
      this.label21.Text = "Recorded time:";
      // 
      // grpUsercam
      // 
      this.grpUsercam.Controls.Add(this.splitContainer2);
      this.grpUsercam.Dock = System.Windows.Forms.DockStyle.Fill;
      this.grpUsercam.Location = new System.Drawing.Point(5, 5);
      this.grpUsercam.Name = "grpUsercam";
      this.grpUsercam.Size = new System.Drawing.Size(210, 190);
      this.grpUsercam.TabIndex = 0;
      this.grpUsercam.TabStop = false;
      this.grpUsercam.Text = "Usercam";
      // 
      // splitContainer2
      // 
      this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
      this.splitContainer2.IsSplitterFixed = true;
      this.splitContainer2.Location = new System.Drawing.Point(3, 16);
      this.splitContainer2.Name = "splitContainer2";
      this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // splitContainer2.Panel1
      // 
      this.splitContainer2.Panel1.Controls.Add(this.panel2);
      this.splitContainer2.Panel1MinSize = 20;
      // 
      // splitContainer2.Panel2
      // 
      this.splitContainer2.Panel2.Controls.Add(this.webcamPreview);
      this.splitContainer2.Size = new System.Drawing.Size(204, 171);
      this.splitContainer2.SplitterDistance = 25;
      this.splitContainer2.TabIndex = 1;
      // 
      // panel2
      // 
      this.panel2.Controls.Add(this.chbRecordAudio);
      this.panel2.Controls.Add(this.chbRecordVideo);
      this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel2.Location = new System.Drawing.Point(0, 0);
      this.panel2.Name = "panel2";
      this.panel2.Size = new System.Drawing.Size(204, 25);
      this.panel2.TabIndex = 0;
      // 
      // chbRecordAudio
      // 
      this.chbRecordAudio.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.chbRecordAudio.ImageKey = "Sound";
      this.chbRecordAudio.ImageList = this.imlButtons;
      this.chbRecordAudio.Location = new System.Drawing.Point(108, 1);
      this.chbRecordAudio.Name = "chbRecordAudio";
      this.chbRecordAudio.Size = new System.Drawing.Size(97, 24);
      this.chbRecordAudio.TabIndex = 0;
      this.chbRecordAudio.Text = "save sound";
      this.chbRecordAudio.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.toolTip1.SetToolTip(this.chbRecordAudio, "Record the sound from the user microphone to file.");
      this.chbRecordAudio.UseVisualStyleBackColor = true;
      // 
      // chbRecordVideo
      // 
      this.chbRecordVideo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.chbRecordVideo.ImageKey = "Video";
      this.chbRecordVideo.ImageList = this.imlButtons;
      this.chbRecordVideo.Location = new System.Drawing.Point(3, 3);
      this.chbRecordVideo.Name = "chbRecordVideo";
      this.chbRecordVideo.Size = new System.Drawing.Size(96, 22);
      this.chbRecordVideo.TabIndex = 0;
      this.chbRecordVideo.Text = "save video";
      this.chbRecordVideo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.toolTip1.SetToolTip(this.chbRecordVideo, "Record the user video from the user cam to file.");
      this.chbRecordVideo.UseVisualStyleBackColor = true;
      // 
      // webcamPreview
      // 
      this.webcamPreview.Dock = System.Windows.Forms.DockStyle.Fill;
      this.webcamPreview.Location = new System.Drawing.Point(0, 0);
      this.webcamPreview.Name = "webcamPreview";
      this.webcamPreview.Size = new System.Drawing.Size(204, 142);
      this.webcamPreview.TabIndex = 0;
      this.webcamPreview.WebcamAvailable += new OgamaControls.CaptureModeEventHandler(this.WebcamPreviewWebcamAvailable);
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
      this.toolStripContainer2.ContentPanel.Size = new System.Drawing.Size(639, 594);
      this.toolStripContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.toolStripContainer2.Location = new System.Drawing.Point(0, 0);
      this.toolStripContainer2.Name = "toolStripContainer2";
      this.toolStripContainer2.Size = new System.Drawing.Size(639, 645);
      this.toolStripContainer2.TabIndex = 1;
      this.toolStripContainer2.Text = "toolStripContainer2";
      // 
      // toolStrip2
      // 
      this.toolStrip2.Dock = System.Windows.Forms.DockStyle.None;
      this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
      this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.trbZoom});
      this.toolStrip2.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
      this.toolStrip2.Location = new System.Drawing.Point(0, 0);
      this.toolStrip2.Name = "toolStrip2";
      this.toolStrip2.Size = new System.Drawing.Size(639, 26);
      this.toolStrip2.Stretch = true;
      this.toolStrip2.TabIndex = 0;
      this.toolStrip2.Text = "toolStrip2";
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
      this.pnlCanvas.Name = "pnlCanvas";
      this.pnlCanvas.Size = new System.Drawing.Size(639, 594);
      this.pnlCanvas.TabIndex = 4;
      // 
      // pnlPicture
      // 
      this.pnlPicture.Controls.Add(this.recordPicture);
      this.pnlPicture.Location = new System.Drawing.Point(117, 87);
      this.pnlPicture.Name = "pnlPicture";
      this.pnlPicture.Size = new System.Drawing.Size(200, 100);
      this.pnlPicture.TabIndex = 0;
      // 
      // recordPicture
      // 
      this.recordPicture.AnimationInterval = 40;
      this.recordPicture.BackColor = System.Drawing.Color.Black;
      this.recordPicture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
      this.recordPicture.InvalidateInterval = 500;
      this.recordPicture.Location = new System.Drawing.Point(0, 0);
      this.recordPicture.Name = "recordPicture";
      this.recordPicture.Size = new System.Drawing.Size(200, 100);
      this.recordPicture.TabIndex = 0;
      this.recordPicture.TabStop = false;
      this.recordPicture.ZoomFactor = 0F;
      // 
      // toolStrip1
      // 
      this.toolStrip1.DataBindings.Add(new System.Windows.Forms.Binding("Location", global::Ogama.Properties.Settings.Default, "RECToolbarLocation", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
      this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnPrimary,
            this.btnSecondary,
            this.btnScreenCaptureSettings,
            this.toolStripSeparator2,
            this.btnSelectTracker,
            this.btnTrackerSettings,
            this.toolStripSeparator4,
            this.btnUsercam,
            this.btnWebcamSettings,
            this.toolStripSeparator1,
            this.btnTrigger,
            this.btnTriggerSettings,
            this.toolStripSeparator3,
            this.btnSmoothing,
            this.btnHelp});
      this.toolStrip1.Location = global::Ogama.Properties.Settings.Default.RECToolbarLocation;
      this.toolStrip1.Name = "toolStrip1";
      this.toolStrip1.Size = new System.Drawing.Size(663, 25);
      this.toolStrip1.TabIndex = 0;
      // 
      // btnPrimary
      // 
      this.btnPrimary.CheckOnClick = true;
      this.btnPrimary.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnPrimary.Image = global::Ogama.Properties.Resources.Monitor1;
      this.btnPrimary.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnPrimary.Name = "btnPrimary";
      this.btnPrimary.Size = new System.Drawing.Size(23, 22);
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
      this.btnSecondary.Size = new System.Drawing.Size(23, 22);
      this.btnSecondary.Text = "Secondary Screen";
      this.btnSecondary.ToolTipText = "Show slide show on secondary screen";
      this.btnSecondary.Click += new System.EventHandler(this.BtnSecondaryClick);
      // 
      // btnScreenCaptureSettings
      // 
      this.btnScreenCaptureSettings.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
      this.btnScreenCaptureSettings.Image = global::Ogama.Properties.Resources.PropertiesHS;
      this.btnScreenCaptureSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnScreenCaptureSettings.Name = "btnScreenCaptureSettings";
      this.btnScreenCaptureSettings.Size = new System.Drawing.Size(136, 22);
      this.btnScreenCaptureSettings.Text = "Screen Capture Settings";
      this.btnScreenCaptureSettings.ToolTipText = "Modify Screen Capture Settings";
      this.btnScreenCaptureSettings.Click += new System.EventHandler(this.BtnScreenCaptureSettingsClick);
      // 
      // toolStripSeparator2
      // 
      this.toolStripSeparator2.Name = "toolStripSeparator2";
      this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
      // 
      // btnSelectTracker
      // 
      this.btnSelectTracker.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnSelectTracker.Image = global::Ogama.Properties.Resources.LegendHS;
      this.btnSelectTracker.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnSelectTracker.Name = "btnSelectTracker";
      this.btnSelectTracker.Size = new System.Drawing.Size(23, 22);
      this.btnSelectTracker.Text = "Select installed tracking device";
      this.btnSelectTracker.Click += new System.EventHandler(this.BtnSelectTrackerClick);
      // 
      // btnTrackerSettings
      // 
      this.btnTrackerSettings.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
      this.btnTrackerSettings.Image = global::Ogama.Properties.Resources.PropertiesHS;
      this.btnTrackerSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnTrackerSettings.Name = "btnTrackerSettings";
      this.btnTrackerSettings.Size = new System.Drawing.Size(94, 22);
      this.btnTrackerSettings.Text = "Tracker settings";
      this.btnTrackerSettings.ToolTipText = "Change tracker settings ..";
      this.btnTrackerSettings.Click += new System.EventHandler(this.BtnTrackerSettingsClick);
      // 
      // toolStripSeparator4
      // 
      this.toolStripSeparator4.Name = "toolStripSeparator4";
      this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
      // 
      // btnUsercam
      // 
      this.btnUsercam.Checked = true;
      this.btnUsercam.CheckOnClick = true;
      this.btnUsercam.CheckState = System.Windows.Forms.CheckState.Checked;
      this.btnUsercam.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnUsercam.Image = global::Ogama.Properties.Resources.video;
      this.btnUsercam.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnUsercam.Name = "btnUsercam";
      this.btnUsercam.Size = new System.Drawing.Size(23, 22);
      this.btnUsercam.Text = "User camera On/off";
      this.btnUsercam.ToolTipText = "Show or hide user camera.";
      this.btnUsercam.Click += new System.EventHandler(this.BtnUsercamClick);
      // 
      // btnWebcamSettings
      // 
      this.btnWebcamSettings.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
      this.btnWebcamSettings.Image = global::Ogama.Properties.Resources.PropertiesHS;
      this.btnWebcamSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnWebcamSettings.Name = "btnWebcamSettings";
      this.btnWebcamSettings.Size = new System.Drawing.Size(96, 22);
      this.btnWebcamSettings.Text = "Camera settings";
      this.btnWebcamSettings.Click += new System.EventHandler(this.BtnWebcamSettingsClick);
      // 
      // toolStripSeparator1
      // 
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
      // 
      // btnTrigger
      // 
      this.btnTrigger.CheckOnClick = true;
      this.btnTrigger.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnTrigger.Image = global::Ogama.Properties.Resources.Event;
      this.btnTrigger.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnTrigger.Name = "btnTrigger";
      this.btnTrigger.Size = new System.Drawing.Size(23, 22);
      this.btnTrigger.Text = "Trigger On/Off";
      this.btnTrigger.ToolTipText = "When this button is checked, general and slide triggers will be sent.";
      // 
      // btnTriggerSettings
      // 
      this.btnTriggerSettings.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
      this.btnTriggerSettings.Image = global::Ogama.Properties.Resources.PropertiesHS;
      this.btnTriggerSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnTriggerSettings.Name = "btnTriggerSettings";
      this.btnTriggerSettings.Size = new System.Drawing.Size(93, 22);
      this.btnTriggerSettings.Text = "Trigger settings";
      this.btnTriggerSettings.Click += new System.EventHandler(this.BtnTriggerSettingsClick);
      // 
      // toolStripSeparator3
      // 
      this.toolStripSeparator3.Name = "toolStripSeparator3";
      this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
      // 
      // btnSmoothing
      // 
      this.btnSmoothing.CheckOnClick = true;
      this.btnSmoothing.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
      this.btnSmoothing.Image = ((System.Drawing.Image)(resources.GetObject("btnSmoothing.Image")));
      this.btnSmoothing.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnSmoothing.Name = "btnSmoothing";
      this.btnSmoothing.Size = new System.Drawing.Size(70, 22);
      this.btnSmoothing.Text = "Smoothing";
      this.btnSmoothing.Click += new System.EventHandler(this.BtnSmoothingClick);
      // 
      // btnHelp
      // 
      this.btnHelp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnHelp.Image = global::Ogama.Properties.Resources.HelpBmp;
      this.btnHelp.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnHelp.Name = "btnHelp";
      this.btnHelp.Size = new System.Drawing.Size(23, 22);
      this.btnHelp.Text = "Show interface help.";
      // 
      // imlSlides
      // 
      this.imlSlides.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
      this.imlSlides.ImageSize = new System.Drawing.Size(150, 100);
      this.imlSlides.TransparentColor = System.Drawing.Color.Transparent;
      // 
      // bgwSaveSplash
      // 
      this.bgwSaveSplash.WorkerSupportsCancellation = true;
      this.bgwSaveSplash.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BgwSaveSplashDoWork);
      // 
      // bgwCalcFixations
      // 
      this.bgwCalcFixations.WorkerReportsProgress = true;
      this.bgwCalcFixations.WorkerSupportsCancellation = true;
      this.bgwCalcFixations.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BgwSaveSplashDoWork);
      // 
      // tmrWaitForPresentationEnd
      // 
      this.tmrWaitForPresentationEnd.Interval = 1000;
      this.tmrWaitForPresentationEnd.Tick += new System.EventHandler(this.TmrWaitForPresentationEndTick);
      // 
      // tmrRecordClock
      // 
      this.tmrRecordClock.Interval = 1000;
      this.tmrRecordClock.Tick += new System.EventHandler(this.TmrRecordClockTick);
      // 
      // tabPage1
      // 
      this.tabPage1.Controls.Add(this.panel5);
      this.tabPage1.ImageKey = "(none)";
      this.tabPage1.Location = new System.Drawing.Point(4, 23);
      this.tabPage1.Name = "tabPage1";
      this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage1.Size = new System.Drawing.Size(212, 322);
      this.tabPage1.TabIndex = 5;
      this.tabPage1.Text = "ITU";
      this.tabPage1.UseVisualStyleBackColor = true;
      // 
      // panel5
      // 
      this.panel5.Controls.Add(this.groupBox5);
      this.panel5.Controls.Add(this.textBox2);
      this.panel5.Controls.Add(this.button4);
      this.panel5.Controls.Add(this.button5);
      this.panel5.Controls.Add(this.label22);
      this.panel5.Controls.Add(this.label23);
      this.panel5.Controls.Add(this.label24);
      this.panel5.Controls.Add(this.label25);
      this.panel5.Controls.Add(this.button6);
      this.panel5.Controls.Add(this.button7);
      this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel5.Location = new System.Drawing.Point(3, 3);
      this.panel5.Name = "panel5";
      this.panel5.Size = new System.Drawing.Size(206, 316);
      this.panel5.TabIndex = 11;
      // 
      // groupBox5
      // 
      this.groupBox5.Controls.Add(this.splitContainer1);
      this.groupBox5.Location = new System.Drawing.Point(5, 3);
      this.groupBox5.Name = "groupBox5";
      this.groupBox5.Size = new System.Drawing.Size(196, 183);
      this.groupBox5.TabIndex = 20;
      this.groupBox5.TabStop = false;
      this.groupBox5.Text = "Track status";
      // 
      // splitContainer1
      // 
      this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer1.Location = new System.Drawing.Point(3, 16);
      this.splitContainer1.Name = "splitContainer1";
      this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // splitContainer1.Panel1
      // 
      this.splitContainer1.Panel1.Controls.Add(this.splitContainer3);
      // 
      // splitContainer1.Panel2
      // 
      this.splitContainer1.Panel2.Controls.Add(this.splitContainer4);
      this.splitContainer1.Size = new System.Drawing.Size(190, 164);
      this.splitContainer1.SplitterDistance = 80;
      this.splitContainer1.TabIndex = 22;
      // 
      // splitContainer3
      // 
      this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
      this.splitContainer3.IsSplitterFixed = true;
      this.splitContainer3.Location = new System.Drawing.Point(0, 0);
      this.splitContainer3.Name = "splitContainer3";
      this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // splitContainer3.Panel1
      // 
      this.splitContainer3.Panel1.BackColor = System.Drawing.Color.Transparent;
      this.splitContainer3.Panel1.Controls.Add(this.pictureBox1);
      // 
      // splitContainer3.Panel2
      // 
      this.splitContainer3.Panel2.Controls.Add(this.button1);
      this.splitContainer3.Panel2MinSize = 22;
      this.splitContainer3.Size = new System.Drawing.Size(190, 80);
      this.splitContainer3.SplitterDistance = 51;
      this.splitContainer3.TabIndex = 20;
      // 
      // pictureBox1
      // 
      this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pictureBox1.Location = new System.Drawing.Point(0, 0);
      this.pictureBox1.Name = "pictureBox1";
      this.pictureBox1.Size = new System.Drawing.Size(190, 51);
      this.pictureBox1.TabIndex = 0;
      this.pictureBox1.TabStop = false;
      // 
      // button1
      // 
      this.button1.BackColor = System.Drawing.Color.Transparent;
      this.button1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.button1.Enabled = false;
      this.button1.Location = new System.Drawing.Point(0, 0);
      this.button1.Name = "button1";
      this.button1.Size = new System.Drawing.Size(190, 25);
      this.button1.TabIndex = 21;
      this.button1.Text = "Show on presentation screen";
      this.button1.UseVisualStyleBackColor = false;
      // 
      // splitContainer4
      // 
      this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer4.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
      this.splitContainer4.IsSplitterFixed = true;
      this.splitContainer4.Location = new System.Drawing.Point(0, 0);
      this.splitContainer4.Name = "splitContainer4";
      this.splitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // splitContainer4.Panel1
      // 
      this.splitContainer4.Panel1.Controls.Add(this.panel6);
      // 
      // splitContainer4.Panel2
      // 
      this.splitContainer4.Panel2.Controls.Add(this.button2);
      this.splitContainer4.Panel2.Controls.Add(this.button3);
      this.splitContainer4.Panel2MinSize = 22;
      this.splitContainer4.Size = new System.Drawing.Size(190, 80);
      this.splitContainer4.SplitterDistance = 51;
      this.splitContainer4.TabIndex = 1;
      // 
      // panel6
      // 
      this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel6.Location = new System.Drawing.Point(0, 0);
      this.panel6.Name = "panel6";
      this.panel6.Size = new System.Drawing.Size(190, 51);
      this.panel6.TabIndex = 0;
      // 
      // button2
      // 
      this.button2.Dock = System.Windows.Forms.DockStyle.Right;
      this.button2.Location = new System.Drawing.Point(115, 0);
      this.button2.Name = "button2";
      this.button2.Size = new System.Drawing.Size(75, 25);
      this.button2.TabIndex = 0;
      this.button2.Text = "Recalibrate";
      this.button2.UseVisualStyleBackColor = true;
      // 
      // button3
      // 
      this.button3.Dock = System.Windows.Forms.DockStyle.Left;
      this.button3.Location = new System.Drawing.Point(0, 0);
      this.button3.Name = "button3";
      this.button3.Size = new System.Drawing.Size(75, 25);
      this.button3.TabIndex = 0;
      this.button3.Text = "Accept";
      this.button3.UseVisualStyleBackColor = true;
      // 
      // textBox2
      // 
      this.textBox2.Location = new System.Drawing.Point(107, 229);
      this.textBox2.Name = "textBox2";
      this.textBox2.ReadOnly = true;
      this.textBox2.Size = new System.Drawing.Size(91, 20);
      this.textBox2.TabIndex = 11;
      this.textBox2.Text = "Subject1";
      // 
      // button4
      // 
      this.button4.Enabled = false;
      this.button4.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
      this.button4.ImageKey = "Record";
      this.button4.ImageList = this.imlButtons;
      this.button4.Location = new System.Drawing.Point(29, 285);
      this.button4.Name = "button4";
      this.button4.Size = new System.Drawing.Size(72, 23);
      this.button4.TabIndex = 17;
      this.button4.Text = "&Record";
      this.button4.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      // 
      // button5
      // 
      this.button5.Enabled = false;
      this.button5.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
      this.button5.ImageKey = "Calibrate";
      this.button5.ImageList = this.imlButtons;
      this.button5.Location = new System.Drawing.Point(29, 256);
      this.button5.Name = "button5";
      this.button5.Size = new System.Drawing.Size(72, 23);
      this.button5.TabIndex = 17;
      this.button5.Text = "&Calibrate";
      this.button5.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      // 
      // label22
      // 
      this.label22.AutoSize = true;
      this.label22.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label22.Location = new System.Drawing.Point(5, 256);
      this.label22.Name = "label22";
      this.label22.Size = new System.Drawing.Size(25, 24);
      this.label22.TabIndex = 13;
      this.label22.Text = "3.";
      // 
      // label23
      // 
      this.label23.AutoSize = true;
      this.label23.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label23.Location = new System.Drawing.Point(5, 285);
      this.label23.Name = "label23";
      this.label23.Size = new System.Drawing.Size(25, 24);
      this.label23.TabIndex = 13;
      this.label23.Text = "4.";
      // 
      // label24
      // 
      this.label24.AutoSize = true;
      this.label24.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label24.Location = new System.Drawing.Point(5, 227);
      this.label24.Name = "label24";
      this.label24.Size = new System.Drawing.Size(25, 24);
      this.label24.TabIndex = 13;
      this.label24.Text = "2.";
      // 
      // label25
      // 
      this.label25.AutoSize = true;
      this.label25.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label25.Location = new System.Drawing.Point(5, 198);
      this.label25.Margin = new System.Windows.Forms.Padding(0);
      this.label25.Name = "label25";
      this.label25.Size = new System.Drawing.Size(25, 24);
      this.label25.TabIndex = 13;
      this.label25.Text = "1.";
      // 
      // button6
      // 
      this.button6.Enabled = false;
      this.button6.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
      this.button6.ImageKey = "Subject";
      this.button6.ImageList = this.imlButtons;
      this.button6.Location = new System.Drawing.Point(29, 228);
      this.button6.Name = "button6";
      this.button6.Size = new System.Drawing.Size(72, 23);
      this.button6.TabIndex = 12;
      this.button6.Text = "Subject";
      this.button6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.button6.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.button6.UseVisualStyleBackColor = true;
      // 
      // button7
      // 
      this.button7.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
      this.button7.ImageKey = "Connect";
      this.button7.ImageList = this.imlButtons;
      this.button7.Location = new System.Drawing.Point(29, 198);
      this.button7.Name = "button7";
      this.button7.Size = new System.Drawing.Size(73, 23);
      this.button7.TabIndex = 12;
      this.button7.Text = "Connect";
      this.button7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.button7.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.button7.UseVisualStyleBackColor = true;
      // 
      // tabPage2
      // 
      this.tabPage2.Controls.Add(this.panel7);
      this.tabPage2.ImageKey = "Tobii";
      this.tabPage2.Location = new System.Drawing.Point(4, 23);
      this.tabPage2.Name = "tabPage2";
      this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage2.Size = new System.Drawing.Size(212, 322);
      this.tabPage2.TabIndex = 0;
      this.tabPage2.Text = "Tobii";
      this.tabPage2.UseVisualStyleBackColor = true;
      // 
      // panel7
      // 
      this.panel7.Controls.Add(this.groupBox6);
      this.panel7.Controls.Add(this.textBox3);
      this.panel7.Controls.Add(this.button11);
      this.panel7.Controls.Add(this.button12);
      this.panel7.Controls.Add(this.label26);
      this.panel7.Controls.Add(this.label27);
      this.panel7.Controls.Add(this.label28);
      this.panel7.Controls.Add(this.label29);
      this.panel7.Controls.Add(this.button13);
      this.panel7.Controls.Add(this.button14);
      this.panel7.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel7.Location = new System.Drawing.Point(3, 3);
      this.panel7.Name = "panel7";
      this.panel7.Size = new System.Drawing.Size(206, 316);
      this.panel7.TabIndex = 11;
      // 
      // groupBox6
      // 
      this.groupBox6.Controls.Add(this.splitContainer5);
      this.groupBox6.Location = new System.Drawing.Point(5, 3);
      this.groupBox6.Name = "groupBox6";
      this.groupBox6.Size = new System.Drawing.Size(196, 183);
      this.groupBox6.TabIndex = 20;
      this.groupBox6.TabStop = false;
      this.groupBox6.Text = "Track status";
      // 
      // splitContainer5
      // 
      this.splitContainer5.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer5.Location = new System.Drawing.Point(3, 16);
      this.splitContainer5.Name = "splitContainer5";
      this.splitContainer5.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // splitContainer5.Panel1
      // 
      this.splitContainer5.Panel1.Controls.Add(this.splitContainer6);
      // 
      // splitContainer5.Panel2
      // 
      this.splitContainer5.Panel2.Controls.Add(this.splitContainer7);
      this.splitContainer5.Size = new System.Drawing.Size(190, 164);
      this.splitContainer5.SplitterDistance = 80;
      this.splitContainer5.TabIndex = 22;
      // 
      // splitContainer6
      // 
      this.splitContainer6.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer6.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
      this.splitContainer6.IsSplitterFixed = true;
      this.splitContainer6.Location = new System.Drawing.Point(0, 0);
      this.splitContainer6.Name = "splitContainer6";
      this.splitContainer6.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // splitContainer6.Panel1
      // 
      this.splitContainer6.Panel1.BackColor = System.Drawing.Color.Transparent;
      // 
      // splitContainer6.Panel2
      // 
      this.splitContainer6.Panel2.Controls.Add(this.button8);
      this.splitContainer6.Panel2MinSize = 22;
      this.splitContainer6.Size = new System.Drawing.Size(190, 80);
      this.splitContainer6.SplitterDistance = 51;
      this.splitContainer6.TabIndex = 20;
      // 
      // button8
      // 
      this.button8.BackColor = System.Drawing.Color.Transparent;
      this.button8.Dock = System.Windows.Forms.DockStyle.Fill;
      this.button8.Enabled = false;
      this.button8.Location = new System.Drawing.Point(0, 0);
      this.button8.Name = "button8";
      this.button8.Size = new System.Drawing.Size(190, 25);
      this.button8.TabIndex = 21;
      this.button8.Text = "Show on presentation screen";
      this.button8.UseVisualStyleBackColor = false;
      // 
      // splitContainer7
      // 
      this.splitContainer7.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer7.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
      this.splitContainer7.IsSplitterFixed = true;
      this.splitContainer7.Location = new System.Drawing.Point(0, 0);
      this.splitContainer7.Name = "splitContainer7";
      this.splitContainer7.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // splitContainer7.Panel2
      // 
      this.splitContainer7.Panel2.Controls.Add(this.button9);
      this.splitContainer7.Panel2.Controls.Add(this.button10);
      this.splitContainer7.Panel2MinSize = 22;
      this.splitContainer7.Size = new System.Drawing.Size(190, 80);
      this.splitContainer7.SplitterDistance = 51;
      this.splitContainer7.TabIndex = 1;
      // 
      // button9
      // 
      this.button9.Dock = System.Windows.Forms.DockStyle.Right;
      this.button9.Location = new System.Drawing.Point(115, 0);
      this.button9.Name = "button9";
      this.button9.Size = new System.Drawing.Size(75, 25);
      this.button9.TabIndex = 0;
      this.button9.Text = "Recalibrate";
      this.button9.UseVisualStyleBackColor = true;
      // 
      // button10
      // 
      this.button10.Dock = System.Windows.Forms.DockStyle.Left;
      this.button10.Location = new System.Drawing.Point(0, 0);
      this.button10.Name = "button10";
      this.button10.Size = new System.Drawing.Size(75, 25);
      this.button10.TabIndex = 0;
      this.button10.Text = "Accept";
      this.button10.UseVisualStyleBackColor = true;
      // 
      // textBox3
      // 
      this.textBox3.Location = new System.Drawing.Point(107, 229);
      this.textBox3.Name = "textBox3";
      this.textBox3.ReadOnly = true;
      this.textBox3.Size = new System.Drawing.Size(91, 20);
      this.textBox3.TabIndex = 11;
      this.textBox3.Text = "Subject1";
      // 
      // button11
      // 
      this.button11.Enabled = false;
      this.button11.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
      this.button11.ImageKey = "Record";
      this.button11.ImageList = this.imlButtons;
      this.button11.Location = new System.Drawing.Point(29, 285);
      this.button11.Name = "button11";
      this.button11.Size = new System.Drawing.Size(72, 23);
      this.button11.TabIndex = 17;
      this.button11.Text = "&Record";
      this.button11.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      // 
      // button12
      // 
      this.button12.Enabled = false;
      this.button12.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
      this.button12.ImageKey = "Calibrate";
      this.button12.ImageList = this.imlButtons;
      this.button12.Location = new System.Drawing.Point(29, 256);
      this.button12.Name = "button12";
      this.button12.Size = new System.Drawing.Size(72, 23);
      this.button12.TabIndex = 17;
      this.button12.Text = "&Calibrate";
      this.button12.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      // 
      // label26
      // 
      this.label26.AutoSize = true;
      this.label26.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label26.Location = new System.Drawing.Point(5, 256);
      this.label26.Name = "label26";
      this.label26.Size = new System.Drawing.Size(25, 24);
      this.label26.TabIndex = 13;
      this.label26.Text = "3.";
      // 
      // label27
      // 
      this.label27.AutoSize = true;
      this.label27.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label27.Location = new System.Drawing.Point(5, 285);
      this.label27.Name = "label27";
      this.label27.Size = new System.Drawing.Size(25, 24);
      this.label27.TabIndex = 13;
      this.label27.Text = "4.";
      // 
      // label28
      // 
      this.label28.AutoSize = true;
      this.label28.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label28.Location = new System.Drawing.Point(5, 227);
      this.label28.Name = "label28";
      this.label28.Size = new System.Drawing.Size(25, 24);
      this.label28.TabIndex = 13;
      this.label28.Text = "2.";
      // 
      // label29
      // 
      this.label29.AutoSize = true;
      this.label29.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label29.Location = new System.Drawing.Point(5, 198);
      this.label29.Margin = new System.Windows.Forms.Padding(0);
      this.label29.Name = "label29";
      this.label29.Size = new System.Drawing.Size(25, 24);
      this.label29.TabIndex = 13;
      this.label29.Text = "1.";
      // 
      // button13
      // 
      this.button13.Enabled = false;
      this.button13.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
      this.button13.ImageKey = "Subject";
      this.button13.ImageList = this.imlButtons;
      this.button13.Location = new System.Drawing.Point(29, 228);
      this.button13.Name = "button13";
      this.button13.Size = new System.Drawing.Size(72, 23);
      this.button13.TabIndex = 12;
      this.button13.Text = "Subject";
      this.button13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.button13.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.button13.UseVisualStyleBackColor = true;
      // 
      // button14
      // 
      this.button14.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
      this.button14.ImageKey = "Connect";
      this.button14.ImageList = this.imlButtons;
      this.button14.Location = new System.Drawing.Point(29, 198);
      this.button14.Name = "button14";
      this.button14.Size = new System.Drawing.Size(73, 23);
      this.button14.TabIndex = 12;
      this.button14.Text = "Connect";
      this.button14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.button14.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.button14.UseVisualStyleBackColor = true;
      // 
      // tabPage3
      // 
      this.tabPage3.Controls.Add(this.groupBox7);
      this.tabPage3.Controls.Add(this.textBox4);
      this.tabPage3.Controls.Add(this.button18);
      this.tabPage3.Controls.Add(this.button19);
      this.tabPage3.Controls.Add(this.label32);
      this.tabPage3.Controls.Add(this.label33);
      this.tabPage3.Controls.Add(this.label34);
      this.tabPage3.Controls.Add(this.label35);
      this.tabPage3.Controls.Add(this.button20);
      this.tabPage3.Controls.Add(this.button21);
      this.tabPage3.ImageKey = "Alea";
      this.tabPage3.Location = new System.Drawing.Point(4, 23);
      this.tabPage3.Name = "tabPage3";
      this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage3.Size = new System.Drawing.Size(212, 322);
      this.tabPage3.TabIndex = 2;
      this.tabPage3.Text = "Alea";
      this.tabPage3.UseVisualStyleBackColor = true;
      // 
      // groupBox7
      // 
      this.groupBox7.Controls.Add(this.splitContainer8);
      this.groupBox7.Location = new System.Drawing.Point(8, 6);
      this.groupBox7.Name = "groupBox7";
      this.groupBox7.Size = new System.Drawing.Size(196, 183);
      this.groupBox7.TabIndex = 27;
      this.groupBox7.TabStop = false;
      this.groupBox7.Text = "Track status";
      // 
      // splitContainer8
      // 
      this.splitContainer8.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer8.Location = new System.Drawing.Point(3, 16);
      this.splitContainer8.Name = "splitContainer8";
      this.splitContainer8.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // splitContainer8.Panel1
      // 
      this.splitContainer8.Panel1.Controls.Add(this.splitContainer9);
      // 
      // splitContainer8.Panel2
      // 
      this.splitContainer8.Panel2.Controls.Add(this.splitContainer10);
      this.splitContainer8.Size = new System.Drawing.Size(190, 164);
      this.splitContainer8.SplitterDistance = 80;
      this.splitContainer8.TabIndex = 22;
      // 
      // splitContainer9
      // 
      this.splitContainer9.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer9.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
      this.splitContainer9.IsSplitterFixed = true;
      this.splitContainer9.Location = new System.Drawing.Point(0, 0);
      this.splitContainer9.Name = "splitContainer9";
      this.splitContainer9.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // splitContainer9.Panel1
      // 
      this.splitContainer9.Panel1.BackColor = System.Drawing.Color.Transparent;
      // 
      // splitContainer9.Panel2
      // 
      this.splitContainer9.Panel2.Controls.Add(this.button15);
      this.splitContainer9.Panel2MinSize = 22;
      this.splitContainer9.Size = new System.Drawing.Size(190, 80);
      this.splitContainer9.SplitterDistance = 51;
      this.splitContainer9.TabIndex = 20;
      // 
      // button15
      // 
      this.button15.BackColor = System.Drawing.Color.Transparent;
      this.button15.Dock = System.Windows.Forms.DockStyle.Fill;
      this.button15.Enabled = false;
      this.button15.Location = new System.Drawing.Point(0, 0);
      this.button15.Name = "button15";
      this.button15.Size = new System.Drawing.Size(190, 25);
      this.button15.TabIndex = 21;
      this.button15.Text = "Show on presentation screen";
      this.button15.UseVisualStyleBackColor = false;
      // 
      // splitContainer10
      // 
      this.splitContainer10.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer10.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
      this.splitContainer10.IsSplitterFixed = true;
      this.splitContainer10.Location = new System.Drawing.Point(0, 0);
      this.splitContainer10.Name = "splitContainer10";
      this.splitContainer10.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // splitContainer10.Panel1
      // 
      this.splitContainer10.Panel1.BackColor = System.Drawing.Color.Transparent;
      this.splitContainer10.Panel1.Controls.Add(this.label30);
      this.splitContainer10.Panel1.Controls.Add(this.label31);
      // 
      // splitContainer10.Panel2
      // 
      this.splitContainer10.Panel2.Controls.Add(this.button16);
      this.splitContainer10.Panel2.Controls.Add(this.button17);
      this.splitContainer10.Panel2MinSize = 22;
      this.splitContainer10.Size = new System.Drawing.Size(190, 80);
      this.splitContainer10.SplitterDistance = 51;
      this.splitContainer10.TabIndex = 1;
      // 
      // label30
      // 
      this.label30.AutoSize = true;
      this.label30.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label30.Location = new System.Drawing.Point(69, 32);
      this.label30.Name = "label30";
      this.label30.Size = new System.Drawing.Size(50, 13);
      this.label30.TabIndex = 1;
      this.label30.Text = "Not Set";
      // 
      // label31
      // 
      this.label31.AutoSize = true;
      this.label31.Location = new System.Drawing.Point(50, 10);
      this.label31.Name = "label31";
      this.label31.Size = new System.Drawing.Size(89, 13);
      this.label31.TabIndex = 0;
      this.label31.Text = "Calibration Result";
      // 
      // button16
      // 
      this.button16.Dock = System.Windows.Forms.DockStyle.Right;
      this.button16.Location = new System.Drawing.Point(115, 0);
      this.button16.Name = "button16";
      this.button16.Size = new System.Drawing.Size(75, 25);
      this.button16.TabIndex = 0;
      this.button16.Text = "Recalibrate";
      this.button16.UseVisualStyleBackColor = true;
      // 
      // button17
      // 
      this.button17.Dock = System.Windows.Forms.DockStyle.Left;
      this.button17.Location = new System.Drawing.Point(0, 0);
      this.button17.Name = "button17";
      this.button17.Size = new System.Drawing.Size(75, 25);
      this.button17.TabIndex = 0;
      this.button17.Text = "Accept";
      this.button17.UseVisualStyleBackColor = true;
      // 
      // textBox4
      // 
      this.textBox4.Location = new System.Drawing.Point(110, 232);
      this.textBox4.Name = "textBox4";
      this.textBox4.ReadOnly = true;
      this.textBox4.Size = new System.Drawing.Size(91, 20);
      this.textBox4.TabIndex = 18;
      this.textBox4.Text = "Subject1";
      // 
      // button18
      // 
      this.button18.Enabled = false;
      this.button18.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
      this.button18.ImageKey = "Record";
      this.button18.ImageList = this.imlButtons;
      this.button18.Location = new System.Drawing.Point(32, 288);
      this.button18.Name = "button18";
      this.button18.Size = new System.Drawing.Size(72, 23);
      this.button18.TabIndex = 26;
      this.button18.Text = "&Record";
      this.button18.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      // 
      // button19
      // 
      this.button19.Enabled = false;
      this.button19.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
      this.button19.ImageKey = "Calibrate";
      this.button19.ImageList = this.imlButtons;
      this.button19.Location = new System.Drawing.Point(32, 259);
      this.button19.Name = "button19";
      this.button19.Size = new System.Drawing.Size(72, 23);
      this.button19.TabIndex = 25;
      this.button19.Text = "&Calibrate";
      this.button19.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      // 
      // label32
      // 
      this.label32.AutoSize = true;
      this.label32.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label32.Location = new System.Drawing.Point(8, 259);
      this.label32.Name = "label32";
      this.label32.Size = new System.Drawing.Size(25, 24);
      this.label32.TabIndex = 24;
      this.label32.Text = "3.";
      // 
      // label33
      // 
      this.label33.AutoSize = true;
      this.label33.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label33.Location = new System.Drawing.Point(8, 288);
      this.label33.Name = "label33";
      this.label33.Size = new System.Drawing.Size(25, 24);
      this.label33.TabIndex = 22;
      this.label33.Text = "4.";
      // 
      // label34
      // 
      this.label34.AutoSize = true;
      this.label34.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label34.Location = new System.Drawing.Point(8, 230);
      this.label34.Name = "label34";
      this.label34.Size = new System.Drawing.Size(25, 24);
      this.label34.TabIndex = 23;
      this.label34.Text = "2.";
      // 
      // label35
      // 
      this.label35.AutoSize = true;
      this.label35.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label35.Location = new System.Drawing.Point(8, 201);
      this.label35.Margin = new System.Windows.Forms.Padding(0);
      this.label35.Name = "label35";
      this.label35.Size = new System.Drawing.Size(25, 24);
      this.label35.TabIndex = 21;
      this.label35.Text = "1.";
      // 
      // button20
      // 
      this.button20.Enabled = false;
      this.button20.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
      this.button20.ImageKey = "Subject";
      this.button20.ImageList = this.imlButtons;
      this.button20.Location = new System.Drawing.Point(32, 231);
      this.button20.Name = "button20";
      this.button20.Size = new System.Drawing.Size(72, 23);
      this.button20.TabIndex = 20;
      this.button20.Text = "Subject";
      this.button20.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.button20.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.button20.UseVisualStyleBackColor = true;
      // 
      // button21
      // 
      this.button21.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
      this.button21.ImageKey = "Connect";
      this.button21.ImageList = this.imlButtons;
      this.button21.Location = new System.Drawing.Point(32, 201);
      this.button21.Name = "button21";
      this.button21.Size = new System.Drawing.Size(73, 23);
      this.button21.TabIndex = 19;
      this.button21.Text = "Connect";
      this.button21.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.button21.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.button21.UseVisualStyleBackColor = true;
      // 
      // tabPage4
      // 
      this.tabPage4.Controls.Add(this.groupBox8);
      this.tabPage4.Controls.Add(this.textBox6);
      this.tabPage4.Controls.Add(this.button22);
      this.tabPage4.Controls.Add(this.button23);
      this.tabPage4.Controls.Add(this.label36);
      this.tabPage4.Controls.Add(this.label37);
      this.tabPage4.Controls.Add(this.label38);
      this.tabPage4.Controls.Add(this.label39);
      this.tabPage4.Controls.Add(this.button24);
      this.tabPage4.Controls.Add(this.button25);
      this.tabPage4.ImageKey = "SMI";
      this.tabPage4.Location = new System.Drawing.Point(4, 23);
      this.tabPage4.Name = "tabPage4";
      this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage4.Size = new System.Drawing.Size(212, 322);
      this.tabPage4.TabIndex = 4;
      this.tabPage4.Text = "SMI";
      this.tabPage4.UseVisualStyleBackColor = true;
      // 
      // groupBox8
      // 
      this.groupBox8.Controls.Add(this.textBox5);
      this.groupBox8.Location = new System.Drawing.Point(8, 8);
      this.groupBox8.Name = "groupBox8";
      this.groupBox8.Size = new System.Drawing.Size(196, 183);
      this.groupBox8.TabIndex = 30;
      this.groupBox8.TabStop = false;
      this.groupBox8.Text = "Track status";
      // 
      // textBox5
      // 
      this.textBox5.Location = new System.Drawing.Point(6, 19);
      this.textBox5.Multiline = true;
      this.textBox5.Name = "textBox5";
      this.textBox5.Size = new System.Drawing.Size(184, 137);
      this.textBox5.TabIndex = 0;
      this.textBox5.Text = resources.GetString("textBox5.Text");
      // 
      // textBox6
      // 
      this.textBox6.Location = new System.Drawing.Point(110, 234);
      this.textBox6.Name = "textBox6";
      this.textBox6.ReadOnly = true;
      this.textBox6.Size = new System.Drawing.Size(91, 20);
      this.textBox6.TabIndex = 21;
      this.textBox6.Text = "Subject1";
      // 
      // button22
      // 
      this.button22.Enabled = false;
      this.button22.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
      this.button22.ImageKey = "Record";
      this.button22.ImageList = this.imlButtons;
      this.button22.Location = new System.Drawing.Point(32, 290);
      this.button22.Name = "button22";
      this.button22.Size = new System.Drawing.Size(72, 23);
      this.button22.TabIndex = 28;
      this.button22.Text = "&Record";
      this.button22.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      // 
      // button23
      // 
      this.button23.Enabled = false;
      this.button23.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
      this.button23.ImageKey = "Calibrate";
      this.button23.ImageList = this.imlButtons;
      this.button23.Location = new System.Drawing.Point(32, 261);
      this.button23.Name = "button23";
      this.button23.Size = new System.Drawing.Size(72, 23);
      this.button23.TabIndex = 29;
      this.button23.Text = "&Calibrate";
      this.button23.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      // 
      // label36
      // 
      this.label36.AutoSize = true;
      this.label36.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label36.Location = new System.Drawing.Point(8, 261);
      this.label36.Name = "label36";
      this.label36.Size = new System.Drawing.Size(25, 24);
      this.label36.TabIndex = 27;
      this.label36.Text = "3.";
      // 
      // label37
      // 
      this.label37.AutoSize = true;
      this.label37.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label37.Location = new System.Drawing.Point(8, 290);
      this.label37.Name = "label37";
      this.label37.Size = new System.Drawing.Size(25, 24);
      this.label37.TabIndex = 26;
      this.label37.Text = "4.";
      // 
      // label38
      // 
      this.label38.AutoSize = true;
      this.label38.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label38.Location = new System.Drawing.Point(8, 232);
      this.label38.Name = "label38";
      this.label38.Size = new System.Drawing.Size(25, 24);
      this.label38.TabIndex = 24;
      this.label38.Text = "2.";
      // 
      // label39
      // 
      this.label39.AutoSize = true;
      this.label39.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label39.Location = new System.Drawing.Point(8, 203);
      this.label39.Margin = new System.Windows.Forms.Padding(0);
      this.label39.Name = "label39";
      this.label39.Size = new System.Drawing.Size(25, 24);
      this.label39.TabIndex = 25;
      this.label39.Text = "1.";
      // 
      // button24
      // 
      this.button24.Enabled = false;
      this.button24.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
      this.button24.ImageKey = "Subject";
      this.button24.ImageList = this.imlButtons;
      this.button24.Location = new System.Drawing.Point(32, 233);
      this.button24.Name = "button24";
      this.button24.Size = new System.Drawing.Size(72, 23);
      this.button24.TabIndex = 22;
      this.button24.Text = "Subject";
      this.button24.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.button24.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.button24.UseVisualStyleBackColor = true;
      // 
      // button25
      // 
      this.button25.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
      this.button25.ImageKey = "Connect";
      this.button25.ImageList = this.imlButtons;
      this.button25.Location = new System.Drawing.Point(32, 203);
      this.button25.Name = "button25";
      this.button25.Size = new System.Drawing.Size(73, 23);
      this.button25.TabIndex = 23;
      this.button25.Text = "Connect";
      this.button25.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.button25.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.button25.UseVisualStyleBackColor = true;
      // 
      // tabPage5
      // 
      this.tabPage5.Controls.Add(this.label40);
      this.tabPage5.Controls.Add(this.label41);
      this.tabPage5.Controls.Add(this.button26);
      this.tabPage5.Controls.Add(this.textBox7);
      this.tabPage5.Controls.Add(this.button27);
      this.tabPage5.ImageKey = "Mouse";
      this.tabPage5.Location = new System.Drawing.Point(4, 23);
      this.tabPage5.Name = "tabPage5";
      this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage5.Size = new System.Drawing.Size(212, 322);
      this.tabPage5.TabIndex = 1;
      this.tabPage5.Text = "MouseOnly";
      this.tabPage5.UseVisualStyleBackColor = true;
      // 
      // label40
      // 
      this.label40.AutoSize = true;
      this.label40.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label40.Location = new System.Drawing.Point(7, 42);
      this.label40.Name = "label40";
      this.label40.Size = new System.Drawing.Size(25, 24);
      this.label40.TabIndex = 21;
      this.label40.Text = "2.";
      // 
      // label41
      // 
      this.label41.AutoSize = true;
      this.label41.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label41.Location = new System.Drawing.Point(7, 13);
      this.label41.Name = "label41";
      this.label41.Size = new System.Drawing.Size(25, 24);
      this.label41.TabIndex = 20;
      this.label41.Text = "1.";
      // 
      // button26
      // 
      this.button26.ImageKey = "Subject";
      this.button26.ImageList = this.imlButtons;
      this.button26.Location = new System.Drawing.Point(32, 13);
      this.button26.Name = "button26";
      this.button26.Size = new System.Drawing.Size(71, 23);
      this.button26.TabIndex = 19;
      this.button26.Text = "Subject";
      this.button26.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.button26.UseVisualStyleBackColor = true;
      // 
      // textBox7
      // 
      this.textBox7.Location = new System.Drawing.Point(115, 15);
      this.textBox7.Name = "textBox7";
      this.textBox7.ReadOnly = true;
      this.textBox7.Size = new System.Drawing.Size(87, 20);
      this.textBox7.TabIndex = 18;
      this.textBox7.Text = "Subject1";
      // 
      // button27
      // 
      this.button27.BackColor = System.Drawing.Color.Transparent;
      this.button27.Enabled = false;
      this.button27.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
      this.button27.ImageKey = "Record";
      this.button27.ImageList = this.imlButtons;
      this.button27.Location = new System.Drawing.Point(32, 42);
      this.button27.Name = "button27";
      this.button27.Size = new System.Drawing.Size(71, 23);
      this.button27.TabIndex = 14;
      this.button27.Text = "Record";
      this.button27.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.button27.UseVisualStyleBackColor = false;
      // 
      // tabPage6
      // 
      this.tabPage6.Controls.Add(this.label42);
      this.tabPage6.Controls.Add(this.button28);
      this.tabPage6.ImageKey = "None";
      this.tabPage6.Location = new System.Drawing.Point(4, 23);
      this.tabPage6.Name = "tabPage6";
      this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage6.Size = new System.Drawing.Size(212, 322);
      this.tabPage6.TabIndex = 3;
      this.tabPage6.Text = "No tracking device selected";
      this.tabPage6.UseVisualStyleBackColor = true;
      // 
      // label42
      // 
      this.label42.Location = new System.Drawing.Point(12, 10);
      this.label42.Name = "label42";
      this.label42.Size = new System.Drawing.Size(194, 46);
      this.label42.TabIndex = 1;
      this.label42.Text = "No tracking device available.\r\nClick the button to see which tracking devices are" +
    " supported/available.";
      // 
      // button28
      // 
      this.button28.Image = global::Ogama.Properties.Resources.LegendHS;
      this.button28.Location = new System.Drawing.Point(27, 59);
      this.button28.Name = "button28";
      this.button28.Size = new System.Drawing.Size(149, 34);
      this.button28.TabIndex = 0;
      this.button28.Text = "Select tracking device";
      this.button28.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.button28.UseVisualStyleBackColor = true;
      // 
      // label48
      // 
      this.label48.AutoSize = true;
      this.label48.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label48.Location = new System.Drawing.Point(5, 226);
      this.label48.Margin = new System.Windows.Forms.Padding(0);
      this.label48.Name = "label48";
      this.label48.Size = new System.Drawing.Size(25, 24);
      this.label48.TabIndex = 22;
      this.label48.Text = "2.";
      // 
      // button30
      // 
      this.button30.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
      this.button30.ImageKey = "Adjust";
      this.button30.ImageList = this.imlButtons;
      this.button30.Location = new System.Drawing.Point(29, 226);
      this.button30.Name = "button30";
      this.button30.Size = new System.Drawing.Size(73, 23);
      this.button30.TabIndex = 21;
      this.button30.Text = "Adjust";
      this.button30.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.button30.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.button30.UseVisualStyleBackColor = true;
      // 
      // groupBox10
      // 
      this.groupBox10.Controls.Add(this.splitContainer11);
      this.groupBox10.Location = new System.Drawing.Point(5, 3);
      this.groupBox10.Name = "groupBox10";
      this.groupBox10.Size = new System.Drawing.Size(196, 183);
      this.groupBox10.TabIndex = 20;
      this.groupBox10.TabStop = false;
      this.groupBox10.Text = "Track status";
      // 
      // splitContainer11
      // 
      this.splitContainer11.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer11.Location = new System.Drawing.Point(3, 16);
      this.splitContainer11.Name = "splitContainer11";
      this.splitContainer11.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // splitContainer11.Panel1
      // 
      this.splitContainer11.Panel1.Controls.Add(this.splitContainer12);
      // 
      // splitContainer11.Panel2
      // 
      this.splitContainer11.Panel2.Controls.Add(this.splitContainer13);
      this.splitContainer11.Size = new System.Drawing.Size(190, 164);
      this.splitContainer11.SplitterDistance = 80;
      this.splitContainer11.TabIndex = 22;
      // 
      // splitContainer12
      // 
      this.splitContainer12.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer12.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
      this.splitContainer12.IsSplitterFixed = true;
      this.splitContainer12.Location = new System.Drawing.Point(0, 0);
      this.splitContainer12.Name = "splitContainer12";
      this.splitContainer12.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // splitContainer12.Panel1
      // 
      this.splitContainer12.Panel1.BackColor = System.Drawing.Color.Transparent;
      // 
      // splitContainer12.Panel2
      // 
      this.splitContainer12.Panel2.Controls.Add(this.button31);
      this.splitContainer12.Panel2MinSize = 22;
      this.splitContainer12.Size = new System.Drawing.Size(190, 80);
      this.splitContainer12.SplitterDistance = 51;
      this.splitContainer12.TabIndex = 20;
      // 
      // button31
      // 
      this.button31.BackColor = System.Drawing.Color.Transparent;
      this.button31.Dock = System.Windows.Forms.DockStyle.Fill;
      this.button31.Enabled = false;
      this.button31.Location = new System.Drawing.Point(0, 0);
      this.button31.Name = "button31";
      this.button31.Size = new System.Drawing.Size(190, 25);
      this.button31.TabIndex = 21;
      this.button31.Text = "Show on presentation screen";
      this.button31.UseVisualStyleBackColor = false;
      // 
      // splitContainer13
      // 
      this.splitContainer13.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer13.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
      this.splitContainer13.IsSplitterFixed = true;
      this.splitContainer13.Location = new System.Drawing.Point(0, 0);
      this.splitContainer13.Name = "splitContainer13";
      this.splitContainer13.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // splitContainer13.Panel2
      // 
      this.splitContainer13.Panel2.Controls.Add(this.button32);
      this.splitContainer13.Panel2.Controls.Add(this.button33);
      this.splitContainer13.Panel2MinSize = 22;
      this.splitContainer13.Size = new System.Drawing.Size(190, 80);
      this.splitContainer13.SplitterDistance = 51;
      this.splitContainer13.TabIndex = 1;
      // 
      // button32
      // 
      this.button32.Dock = System.Windows.Forms.DockStyle.Right;
      this.button32.Location = new System.Drawing.Point(115, 0);
      this.button32.Name = "button32";
      this.button32.Size = new System.Drawing.Size(75, 25);
      this.button32.TabIndex = 0;
      this.button32.Text = "Recalibrate";
      this.button32.UseVisualStyleBackColor = true;
      // 
      // button33
      // 
      this.button33.Dock = System.Windows.Forms.DockStyle.Left;
      this.button33.Location = new System.Drawing.Point(0, 0);
      this.button33.Name = "button33";
      this.button33.Size = new System.Drawing.Size(75, 25);
      this.button33.TabIndex = 0;
      this.button33.Text = "Accept";
      this.button33.UseVisualStyleBackColor = true;
      // 
      // textBox8
      // 
      this.textBox8.Location = new System.Drawing.Point(107, 255);
      this.textBox8.Name = "textBox8";
      this.textBox8.ReadOnly = true;
      this.textBox8.Size = new System.Drawing.Size(91, 20);
      this.textBox8.TabIndex = 11;
      this.textBox8.Text = "Subject1";
      // 
      // button34
      // 
      this.button34.Enabled = false;
      this.button34.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
      this.button34.ImageKey = "Record";
      this.button34.ImageList = this.imlButtons;
      this.button34.Location = new System.Drawing.Point(29, 311);
      this.button34.Name = "button34";
      this.button34.Size = new System.Drawing.Size(72, 23);
      this.button34.TabIndex = 17;
      this.button34.Text = "&Record";
      this.button34.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      // 
      // button35
      // 
      this.button35.Enabled = false;
      this.button35.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
      this.button35.ImageKey = "Calibrate";
      this.button35.ImageList = this.imlButtons;
      this.button35.Location = new System.Drawing.Point(29, 282);
      this.button35.Name = "button35";
      this.button35.Size = new System.Drawing.Size(72, 23);
      this.button35.TabIndex = 17;
      this.button35.Text = "&Calibrate";
      this.button35.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      // 
      // label49
      // 
      this.label49.AutoSize = true;
      this.label49.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label49.Location = new System.Drawing.Point(5, 282);
      this.label49.Name = "label49";
      this.label49.Size = new System.Drawing.Size(25, 24);
      this.label49.TabIndex = 13;
      this.label49.Text = "4.";
      // 
      // label50
      // 
      this.label50.AutoSize = true;
      this.label50.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label50.Location = new System.Drawing.Point(5, 311);
      this.label50.Name = "label50";
      this.label50.Size = new System.Drawing.Size(25, 24);
      this.label50.TabIndex = 13;
      this.label50.Text = "5.";
      // 
      // label51
      // 
      this.label51.AutoSize = true;
      this.label51.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label51.Location = new System.Drawing.Point(5, 253);
      this.label51.Name = "label51";
      this.label51.Size = new System.Drawing.Size(25, 24);
      this.label51.TabIndex = 13;
      this.label51.Text = "3.";
      // 
      // label52
      // 
      this.label52.AutoSize = true;
      this.label52.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label52.Location = new System.Drawing.Point(5, 198);
      this.label52.Margin = new System.Windows.Forms.Padding(0);
      this.label52.Name = "label52";
      this.label52.Size = new System.Drawing.Size(25, 24);
      this.label52.TabIndex = 13;
      this.label52.Text = "1.";
      // 
      // button36
      // 
      this.button36.Enabled = false;
      this.button36.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
      this.button36.ImageKey = "Subject";
      this.button36.ImageList = this.imlButtons;
      this.button36.Location = new System.Drawing.Point(29, 254);
      this.button36.Name = "button36";
      this.button36.Size = new System.Drawing.Size(72, 23);
      this.button36.TabIndex = 12;
      this.button36.Text = "Subject";
      this.button36.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.button36.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.button36.UseVisualStyleBackColor = true;
      // 
      // button37
      // 
      this.button37.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
      this.button37.ImageKey = "Connect";
      this.button37.ImageList = this.imlButtons;
      this.button37.Location = new System.Drawing.Point(29, 198);
      this.button37.Name = "button37";
      this.button37.Size = new System.Drawing.Size(73, 23);
      this.button37.TabIndex = 12;
      this.button37.Text = "Connect";
      this.button37.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.button37.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.button37.UseVisualStyleBackColor = true;
      // 
      // tabPage7
      // 
      this.tabPage7.Controls.Add(this.panel8);
      this.tabPage7.ImageKey = "ITU";
      this.tabPage7.Location = new System.Drawing.Point(4, 23);
      this.tabPage7.Name = "tabPage7";
      this.tabPage7.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage7.Size = new System.Drawing.Size(212, 392);
      this.tabPage7.TabIndex = 5;
      this.tabPage7.Text = "ITU";
      this.tabPage7.UseVisualStyleBackColor = true;
      // 
      // panel8
      // 
      this.panel8.Controls.Add(this.label18);
      this.panel8.Controls.Add(this.button29);
      this.panel8.Controls.Add(this.groupBox9);
      this.panel8.Controls.Add(this.textBox9);
      this.panel8.Controls.Add(this.button41);
      this.panel8.Controls.Add(this.button42);
      this.panel8.Controls.Add(this.label43);
      this.panel8.Controls.Add(this.label44);
      this.panel8.Controls.Add(this.label45);
      this.panel8.Controls.Add(this.label46);
      this.panel8.Controls.Add(this.label53);
      this.panel8.Controls.Add(this.button43);
      this.panel8.Controls.Add(this.btnITUCamera);
      this.panel8.Controls.Add(this.button44);
      this.panel8.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel8.Location = new System.Drawing.Point(3, 3);
      this.panel8.Name = "panel8";
      this.panel8.Size = new System.Drawing.Size(206, 386);
      this.panel8.TabIndex = 11;
      // 
      // label18
      // 
      this.label18.AutoSize = true;
      this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label18.Location = new System.Drawing.Point(5, 256);
      this.label18.Margin = new System.Windows.Forms.Padding(0);
      this.label18.Name = "label18";
      this.label18.Size = new System.Drawing.Size(25, 24);
      this.label18.TabIndex = 22;
      this.label18.Text = "3.";
      // 
      // button29
      // 
      this.button29.Enabled = false;
      this.button29.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
      this.button29.ImageKey = "Adjust";
      this.button29.ImageList = this.imlButtons;
      this.button29.Location = new System.Drawing.Point(29, 256);
      this.button29.Name = "button29";
      this.button29.Size = new System.Drawing.Size(73, 23);
      this.button29.TabIndex = 21;
      this.button29.Text = "Adjust";
      this.button29.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.button29.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.button29.UseVisualStyleBackColor = true;
      // 
      // groupBox9
      // 
      this.groupBox9.Controls.Add(this.splitContainer14);
      this.groupBox9.Location = new System.Drawing.Point(5, 3);
      this.groupBox9.Name = "groupBox9";
      this.groupBox9.Size = new System.Drawing.Size(196, 183);
      this.groupBox9.TabIndex = 20;
      this.groupBox9.TabStop = false;
      this.groupBox9.Text = "Track status";
      // 
      // splitContainer14
      // 
      this.splitContainer14.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer14.Location = new System.Drawing.Point(3, 16);
      this.splitContainer14.Name = "splitContainer14";
      this.splitContainer14.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // splitContainer14.Panel1
      // 
      this.splitContainer14.Panel1.Controls.Add(this.splitContainer15);
      // 
      // splitContainer14.Panel2
      // 
      this.splitContainer14.Panel2.Controls.Add(this.splitContainer16);
      this.splitContainer14.Size = new System.Drawing.Size(190, 164);
      this.splitContainer14.SplitterDistance = 80;
      this.splitContainer14.TabIndex = 22;
      // 
      // splitContainer15
      // 
      this.splitContainer15.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer15.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
      this.splitContainer15.IsSplitterFixed = true;
      this.splitContainer15.Location = new System.Drawing.Point(0, 0);
      this.splitContainer15.Name = "splitContainer15";
      this.splitContainer15.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // splitContainer15.Panel1
      // 
      this.splitContainer15.Panel1.BackColor = System.Drawing.Color.Transparent;
      // 
      // splitContainer15.Panel2
      // 
      this.splitContainer15.Panel2.Controls.Add(this.button38);
      this.splitContainer15.Panel2MinSize = 22;
      this.splitContainer15.Size = new System.Drawing.Size(190, 80);
      this.splitContainer15.SplitterDistance = 51;
      this.splitContainer15.TabIndex = 20;
      // 
      // button38
      // 
      this.button38.BackColor = System.Drawing.Color.Transparent;
      this.button38.Dock = System.Windows.Forms.DockStyle.Fill;
      this.button38.Enabled = false;
      this.button38.Location = new System.Drawing.Point(0, 0);
      this.button38.Name = "button38";
      this.button38.Size = new System.Drawing.Size(190, 25);
      this.button38.TabIndex = 21;
      this.button38.Text = "Show on presentation screen";
      this.button38.UseVisualStyleBackColor = false;
      // 
      // splitContainer16
      // 
      this.splitContainer16.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer16.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
      this.splitContainer16.IsSplitterFixed = true;
      this.splitContainer16.Location = new System.Drawing.Point(0, 0);
      this.splitContainer16.Name = "splitContainer16";
      this.splitContainer16.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // splitContainer16.Panel2
      // 
      this.splitContainer16.Panel2.Controls.Add(this.button39);
      this.splitContainer16.Panel2.Controls.Add(this.button40);
      this.splitContainer16.Panel2MinSize = 22;
      this.splitContainer16.Size = new System.Drawing.Size(190, 80);
      this.splitContainer16.SplitterDistance = 51;
      this.splitContainer16.TabIndex = 1;
      // 
      // button39
      // 
      this.button39.Dock = System.Windows.Forms.DockStyle.Right;
      this.button39.Location = new System.Drawing.Point(115, 0);
      this.button39.Name = "button39";
      this.button39.Size = new System.Drawing.Size(75, 25);
      this.button39.TabIndex = 0;
      this.button39.Text = "Recalibrate";
      this.button39.UseVisualStyleBackColor = true;
      // 
      // button40
      // 
      this.button40.Dock = System.Windows.Forms.DockStyle.Left;
      this.button40.Location = new System.Drawing.Point(0, 0);
      this.button40.Name = "button40";
      this.button40.Size = new System.Drawing.Size(75, 25);
      this.button40.TabIndex = 0;
      this.button40.Text = "Accept";
      this.button40.UseVisualStyleBackColor = true;
      // 
      // textBox9
      // 
      this.textBox9.Location = new System.Drawing.Point(107, 285);
      this.textBox9.Name = "textBox9";
      this.textBox9.ReadOnly = true;
      this.textBox9.Size = new System.Drawing.Size(91, 20);
      this.textBox9.TabIndex = 11;
      this.textBox9.Text = "Subject1";
      // 
      // button41
      // 
      this.button41.Enabled = false;
      this.button41.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
      this.button41.ImageKey = "Record";
      this.button41.ImageList = this.imlButtons;
      this.button41.Location = new System.Drawing.Point(29, 341);
      this.button41.Name = "button41";
      this.button41.Size = new System.Drawing.Size(73, 23);
      this.button41.TabIndex = 17;
      this.button41.Text = "&Record";
      this.button41.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      // 
      // button42
      // 
      this.button42.Enabled = false;
      this.button42.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
      this.button42.ImageKey = "Calibrate";
      this.button42.ImageList = this.imlButtons;
      this.button42.Location = new System.Drawing.Point(29, 312);
      this.button42.Name = "button42";
      this.button42.Size = new System.Drawing.Size(73, 23);
      this.button42.TabIndex = 17;
      this.button42.Text = "&Calibrate";
      this.button42.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      // 
      // label43
      // 
      this.label43.AutoSize = true;
      this.label43.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label43.Location = new System.Drawing.Point(5, 312);
      this.label43.Name = "label43";
      this.label43.Size = new System.Drawing.Size(25, 24);
      this.label43.TabIndex = 13;
      this.label43.Text = "5.";
      // 
      // label44
      // 
      this.label44.AutoSize = true;
      this.label44.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label44.Location = new System.Drawing.Point(5, 341);
      this.label44.Name = "label44";
      this.label44.Size = new System.Drawing.Size(25, 24);
      this.label44.TabIndex = 13;
      this.label44.Text = "6.";
      // 
      // label45
      // 
      this.label45.AutoSize = true;
      this.label45.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label45.Location = new System.Drawing.Point(5, 283);
      this.label45.Name = "label45";
      this.label45.Size = new System.Drawing.Size(25, 24);
      this.label45.TabIndex = 13;
      this.label45.Text = "4.";
      // 
      // label46
      // 
      this.label46.AutoSize = true;
      this.label46.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label46.Location = new System.Drawing.Point(5, 227);
      this.label46.Margin = new System.Windows.Forms.Padding(0);
      this.label46.Name = "label46";
      this.label46.Size = new System.Drawing.Size(25, 24);
      this.label46.TabIndex = 13;
      this.label46.Text = "2.";
      // 
      // label53
      // 
      this.label53.AutoSize = true;
      this.label53.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label53.Location = new System.Drawing.Point(5, 198);
      this.label53.Margin = new System.Windows.Forms.Padding(0);
      this.label53.Name = "label53";
      this.label53.Size = new System.Drawing.Size(25, 24);
      this.label53.TabIndex = 13;
      this.label53.Text = "1.";
      // 
      // button43
      // 
      this.button43.Enabled = false;
      this.button43.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
      this.button43.ImageKey = "Subject";
      this.button43.ImageList = this.imlButtons;
      this.button43.Location = new System.Drawing.Point(29, 284);
      this.button43.Name = "button43";
      this.button43.Size = new System.Drawing.Size(73, 23);
      this.button43.TabIndex = 12;
      this.button43.Text = "Subject";
      this.button43.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.button43.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.button43.UseVisualStyleBackColor = true;
      // 
      // btnITUCamera
      // 
      this.btnITUCamera.Enabled = false;
      this.btnITUCamera.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
      this.btnITUCamera.ImageKey = "ITU";
      this.btnITUCamera.ImageList = this.imlButtons;
      this.btnITUCamera.Location = new System.Drawing.Point(29, 227);
      this.btnITUCamera.Name = "btnITUCamera";
      this.btnITUCamera.Size = new System.Drawing.Size(73, 23);
      this.btnITUCamera.TabIndex = 12;
      this.btnITUCamera.Text = "Camera";
      this.btnITUCamera.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.btnITUCamera.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.btnITUCamera.UseVisualStyleBackColor = true;
      // 
      // button44
      // 
      this.button44.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
      this.button44.ImageKey = "Connect";
      this.button44.ImageList = this.imlButtons;
      this.button44.Location = new System.Drawing.Point(29, 198);
      this.button44.Name = "button44";
      this.button44.Size = new System.Drawing.Size(73, 23);
      this.button44.TabIndex = 12;
      this.button44.Text = "Connect";
      this.button44.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.button44.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.button44.UseVisualStyleBackColor = true;
      // 
      // tbpITUPS3
      // 
      this.tbpITUPS3.Controls.Add(this.panel9);
      this.tbpITUPS3.ImageKey = "ITUPS3";
      this.tbpITUPS3.Location = new System.Drawing.Point(4, 23);
      this.tbpITUPS3.Name = "tbpITUPS3";
      this.tbpITUPS3.Padding = new System.Windows.Forms.Padding(3);
      this.tbpITUPS3.Size = new System.Drawing.Size(212, 392);
      this.tbpITUPS3.TabIndex = 6;
      this.tbpITUPS3.Text = "PS3";
      this.tbpITUPS3.UseVisualStyleBackColor = true;
      // 
      // panel9
      // 
      this.panel9.Controls.Add(this.label55);
      this.panel9.Controls.Add(this.btnITUPS3Camera);
      this.panel9.Controls.Add(this.label56);
      this.panel9.Controls.Add(this.btnITUPS3Adjust);
      this.panel9.Controls.Add(this.groupBox11);
      this.panel9.Controls.Add(this.txbITUPS3SubjectName);
      this.panel9.Controls.Add(this.btnITUPS3Record);
      this.panel9.Controls.Add(this.btnITUPS3Calibrate);
      this.panel9.Controls.Add(this.label57);
      this.panel9.Controls.Add(this.label58);
      this.panel9.Controls.Add(this.label59);
      this.panel9.Controls.Add(this.label60);
      this.panel9.Controls.Add(this.btnITUPS3SubjectName);
      this.panel9.Controls.Add(this.btnITUPS3Connect);
      this.panel9.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel9.Location = new System.Drawing.Point(3, 3);
      this.panel9.Name = "panel9";
      this.panel9.Size = new System.Drawing.Size(206, 386);
      this.panel9.TabIndex = 11;
      // 
      // label55
      // 
      this.label55.AutoSize = true;
      this.label55.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label55.Location = new System.Drawing.Point(5, 227);
      this.label55.Margin = new System.Windows.Forms.Padding(0);
      this.label55.Name = "label55";
      this.label55.Size = new System.Drawing.Size(25, 24);
      this.label55.TabIndex = 26;
      this.label55.Text = "2.";
      // 
      // btnITUPS3Camera
      // 
      this.btnITUPS3Camera.Enabled = false;
      this.btnITUPS3Camera.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
      this.btnITUPS3Camera.ImageKey = "PS3";
      this.btnITUPS3Camera.ImageList = this.imlButtons;
      this.btnITUPS3Camera.Location = new System.Drawing.Point(29, 227);
      this.btnITUPS3Camera.Name = "btnITUPS3Camera";
      this.btnITUPS3Camera.Size = new System.Drawing.Size(73, 23);
      this.btnITUPS3Camera.TabIndex = 25;
      this.btnITUPS3Camera.Text = "Camera";
      this.btnITUPS3Camera.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.btnITUPS3Camera.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.btnITUPS3Camera.UseVisualStyleBackColor = true;
      // 
      // label56
      // 
      this.label56.AutoSize = true;
      this.label56.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label56.Location = new System.Drawing.Point(5, 256);
      this.label56.Margin = new System.Windows.Forms.Padding(0);
      this.label56.Name = "label56";
      this.label56.Size = new System.Drawing.Size(25, 24);
      this.label56.TabIndex = 24;
      this.label56.Text = "3.";
      // 
      // btnITUPS3Adjust
      // 
      this.btnITUPS3Adjust.Enabled = false;
      this.btnITUPS3Adjust.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
      this.btnITUPS3Adjust.ImageKey = "Adjust";
      this.btnITUPS3Adjust.ImageList = this.imlButtons;
      this.btnITUPS3Adjust.Location = new System.Drawing.Point(29, 256);
      this.btnITUPS3Adjust.Name = "btnITUPS3Adjust";
      this.btnITUPS3Adjust.Size = new System.Drawing.Size(73, 23);
      this.btnITUPS3Adjust.TabIndex = 23;
      this.btnITUPS3Adjust.Text = "Adjust";
      this.btnITUPS3Adjust.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.btnITUPS3Adjust.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.btnITUPS3Adjust.UseVisualStyleBackColor = true;
      // 
      // groupBox11
      // 
      this.groupBox11.Controls.Add(this.spcITUPS3Controls);
      this.groupBox11.Location = new System.Drawing.Point(5, 3);
      this.groupBox11.Name = "groupBox11";
      this.groupBox11.Size = new System.Drawing.Size(196, 183);
      this.groupBox11.TabIndex = 20;
      this.groupBox11.TabStop = false;
      this.groupBox11.Text = "Track status";
      // 
      // spcITUPS3Controls
      // 
      this.spcITUPS3Controls.Dock = System.Windows.Forms.DockStyle.Fill;
      this.spcITUPS3Controls.Location = new System.Drawing.Point(3, 16);
      this.spcITUPS3Controls.Name = "spcITUPS3Controls";
      this.spcITUPS3Controls.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // spcITUPS3Controls.Panel1
      // 
      this.spcITUPS3Controls.Panel1.Controls.Add(this.spcITUPS3TrackStatus);
      // 
      // spcITUPS3Controls.Panel2
      // 
      this.spcITUPS3Controls.Panel2.Controls.Add(this.spcITUPS3CalibPlot);
      this.spcITUPS3Controls.Size = new System.Drawing.Size(190, 164);
      this.spcITUPS3Controls.SplitterDistance = 80;
      this.spcITUPS3Controls.TabIndex = 22;
      // 
      // spcITUPS3TrackStatus
      // 
      this.spcITUPS3TrackStatus.Dock = System.Windows.Forms.DockStyle.Fill;
      this.spcITUPS3TrackStatus.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
      this.spcITUPS3TrackStatus.IsSplitterFixed = true;
      this.spcITUPS3TrackStatus.Location = new System.Drawing.Point(0, 0);
      this.spcITUPS3TrackStatus.Name = "spcITUPS3TrackStatus";
      this.spcITUPS3TrackStatus.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // spcITUPS3TrackStatus.Panel1
      // 
      this.spcITUPS3TrackStatus.Panel1.BackColor = System.Drawing.Color.Transparent;
      // 
      // spcITUPS3TrackStatus.Panel2
      // 
      this.spcITUPS3TrackStatus.Panel2.Controls.Add(this.btnITUPS3ShowOnPresentationScreen);
      this.spcITUPS3TrackStatus.Panel2MinSize = 22;
      this.spcITUPS3TrackStatus.Size = new System.Drawing.Size(190, 80);
      this.spcITUPS3TrackStatus.SplitterDistance = 51;
      this.spcITUPS3TrackStatus.TabIndex = 20;
      // 
      // btnITUPS3ShowOnPresentationScreen
      // 
      this.btnITUPS3ShowOnPresentationScreen.BackColor = System.Drawing.Color.Transparent;
      this.btnITUPS3ShowOnPresentationScreen.Dock = System.Windows.Forms.DockStyle.Fill;
      this.btnITUPS3ShowOnPresentationScreen.Enabled = false;
      this.btnITUPS3ShowOnPresentationScreen.Location = new System.Drawing.Point(0, 0);
      this.btnITUPS3ShowOnPresentationScreen.Name = "btnITUPS3ShowOnPresentationScreen";
      this.btnITUPS3ShowOnPresentationScreen.Size = new System.Drawing.Size(190, 25);
      this.btnITUPS3ShowOnPresentationScreen.TabIndex = 21;
      this.btnITUPS3ShowOnPresentationScreen.Text = "Show on presentation screen";
      this.btnITUPS3ShowOnPresentationScreen.UseVisualStyleBackColor = false;
      // 
      // spcITUPS3CalibPlot
      // 
      this.spcITUPS3CalibPlot.Dock = System.Windows.Forms.DockStyle.Fill;
      this.spcITUPS3CalibPlot.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
      this.spcITUPS3CalibPlot.IsSplitterFixed = true;
      this.spcITUPS3CalibPlot.Location = new System.Drawing.Point(0, 0);
      this.spcITUPS3CalibPlot.Name = "spcITUPS3CalibPlot";
      this.spcITUPS3CalibPlot.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // spcITUPS3CalibPlot.Panel2
      // 
      this.spcITUPS3CalibPlot.Panel2.Controls.Add(this.btnITUPS3Recalibrate);
      this.spcITUPS3CalibPlot.Panel2.Controls.Add(this.btnITUPS3AcceptCalibration);
      this.spcITUPS3CalibPlot.Panel2MinSize = 22;
      this.spcITUPS3CalibPlot.Size = new System.Drawing.Size(190, 80);
      this.spcITUPS3CalibPlot.SplitterDistance = 51;
      this.spcITUPS3CalibPlot.TabIndex = 1;
      // 
      // btnITUPS3Recalibrate
      // 
      this.btnITUPS3Recalibrate.Dock = System.Windows.Forms.DockStyle.Right;
      this.btnITUPS3Recalibrate.Location = new System.Drawing.Point(115, 0);
      this.btnITUPS3Recalibrate.Name = "btnITUPS3Recalibrate";
      this.btnITUPS3Recalibrate.Size = new System.Drawing.Size(75, 25);
      this.btnITUPS3Recalibrate.TabIndex = 0;
      this.btnITUPS3Recalibrate.Text = "Recalibrate";
      this.btnITUPS3Recalibrate.UseVisualStyleBackColor = true;
      // 
      // btnITUPS3AcceptCalibration
      // 
      this.btnITUPS3AcceptCalibration.Dock = System.Windows.Forms.DockStyle.Left;
      this.btnITUPS3AcceptCalibration.Location = new System.Drawing.Point(0, 0);
      this.btnITUPS3AcceptCalibration.Name = "btnITUPS3AcceptCalibration";
      this.btnITUPS3AcceptCalibration.Size = new System.Drawing.Size(75, 25);
      this.btnITUPS3AcceptCalibration.TabIndex = 0;
      this.btnITUPS3AcceptCalibration.Text = "Accept";
      this.btnITUPS3AcceptCalibration.UseVisualStyleBackColor = true;
      // 
      // txbITUPS3SubjectName
      // 
      this.txbITUPS3SubjectName.Location = new System.Drawing.Point(107, 286);
      this.txbITUPS3SubjectName.Name = "txbITUPS3SubjectName";
      this.txbITUPS3SubjectName.ReadOnly = true;
      this.txbITUPS3SubjectName.Size = new System.Drawing.Size(91, 20);
      this.txbITUPS3SubjectName.TabIndex = 11;
      this.txbITUPS3SubjectName.Text = "Subject1";
      // 
      // btnITUPS3Record
      // 
      this.btnITUPS3Record.Enabled = false;
      this.btnITUPS3Record.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
      this.btnITUPS3Record.ImageKey = "Record";
      this.btnITUPS3Record.ImageList = this.imlButtons;
      this.btnITUPS3Record.Location = new System.Drawing.Point(29, 342);
      this.btnITUPS3Record.Name = "btnITUPS3Record";
      this.btnITUPS3Record.Size = new System.Drawing.Size(73, 23);
      this.btnITUPS3Record.TabIndex = 17;
      this.btnITUPS3Record.Text = "&Record";
      this.btnITUPS3Record.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      // 
      // btnITUPS3Calibrate
      // 
      this.btnITUPS3Calibrate.Enabled = false;
      this.btnITUPS3Calibrate.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
      this.btnITUPS3Calibrate.ImageKey = "Calibrate";
      this.btnITUPS3Calibrate.ImageList = this.imlButtons;
      this.btnITUPS3Calibrate.Location = new System.Drawing.Point(29, 313);
      this.btnITUPS3Calibrate.Name = "btnITUPS3Calibrate";
      this.btnITUPS3Calibrate.Size = new System.Drawing.Size(73, 23);
      this.btnITUPS3Calibrate.TabIndex = 17;
      this.btnITUPS3Calibrate.Text = "&Calibrate";
      this.btnITUPS3Calibrate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      // 
      // label57
      // 
      this.label57.AutoSize = true;
      this.label57.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label57.Location = new System.Drawing.Point(5, 313);
      this.label57.Name = "label57";
      this.label57.Size = new System.Drawing.Size(25, 24);
      this.label57.TabIndex = 13;
      this.label57.Text = "5.";
      // 
      // label58
      // 
      this.label58.AutoSize = true;
      this.label58.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label58.Location = new System.Drawing.Point(5, 342);
      this.label58.Name = "label58";
      this.label58.Size = new System.Drawing.Size(25, 24);
      this.label58.TabIndex = 13;
      this.label58.Text = "6.";
      // 
      // label59
      // 
      this.label59.AutoSize = true;
      this.label59.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label59.Location = new System.Drawing.Point(5, 284);
      this.label59.Name = "label59";
      this.label59.Size = new System.Drawing.Size(25, 24);
      this.label59.TabIndex = 13;
      this.label59.Text = "4.";
      // 
      // label60
      // 
      this.label60.AutoSize = true;
      this.label60.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label60.Location = new System.Drawing.Point(5, 198);
      this.label60.Margin = new System.Windows.Forms.Padding(0);
      this.label60.Name = "label60";
      this.label60.Size = new System.Drawing.Size(25, 24);
      this.label60.TabIndex = 13;
      this.label60.Text = "1.";
      // 
      // btnITUPS3SubjectName
      // 
      this.btnITUPS3SubjectName.Enabled = false;
      this.btnITUPS3SubjectName.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
      this.btnITUPS3SubjectName.ImageKey = "Subject";
      this.btnITUPS3SubjectName.ImageList = this.imlButtons;
      this.btnITUPS3SubjectName.Location = new System.Drawing.Point(29, 285);
      this.btnITUPS3SubjectName.Name = "btnITUPS3SubjectName";
      this.btnITUPS3SubjectName.Size = new System.Drawing.Size(73, 23);
      this.btnITUPS3SubjectName.TabIndex = 12;
      this.btnITUPS3SubjectName.Text = "Subject";
      this.btnITUPS3SubjectName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.btnITUPS3SubjectName.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.btnITUPS3SubjectName.UseVisualStyleBackColor = true;
      // 
      // btnITUPS3Connect
      // 
      this.btnITUPS3Connect.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
      this.btnITUPS3Connect.ImageKey = "Connect";
      this.btnITUPS3Connect.ImageList = this.imlButtons;
      this.btnITUPS3Connect.Location = new System.Drawing.Point(29, 198);
      this.btnITUPS3Connect.Name = "btnITUPS3Connect";
      this.btnITUPS3Connect.Size = new System.Drawing.Size(73, 23);
      this.btnITUPS3Connect.TabIndex = 12;
      this.btnITUPS3Connect.Text = "Connect";
      this.btnITUPS3Connect.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.btnITUPS3Connect.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.btnITUPS3Connect.UseVisualStyleBackColor = true;
      // 
      // tabPage8
      // 
      this.tabPage8.Controls.Add(this.panel10);
      this.tabPage8.ImageKey = "Tobii";
      this.tabPage8.Location = new System.Drawing.Point(4, 23);
      this.tabPage8.Name = "tabPage8";
      this.tabPage8.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage8.Size = new System.Drawing.Size(212, 392);
      this.tabPage8.TabIndex = 0;
      this.tabPage8.Text = "Tobii";
      this.tabPage8.UseVisualStyleBackColor = true;
      // 
      // panel10
      // 
      this.panel10.Controls.Add(this.groupBox12);
      this.panel10.Controls.Add(this.textBox10);
      this.panel10.Controls.Add(this.button48);
      this.panel10.Controls.Add(this.button49);
      this.panel10.Controls.Add(this.label61);
      this.panel10.Controls.Add(this.label62);
      this.panel10.Controls.Add(this.label63);
      this.panel10.Controls.Add(this.label64);
      this.panel10.Controls.Add(this.button50);
      this.panel10.Controls.Add(this.button51);
      this.panel10.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel10.Location = new System.Drawing.Point(3, 3);
      this.panel10.Name = "panel10";
      this.panel10.Size = new System.Drawing.Size(206, 386);
      this.panel10.TabIndex = 11;
      // 
      // groupBox12
      // 
      this.groupBox12.Controls.Add(this.splitContainer17);
      this.groupBox12.Location = new System.Drawing.Point(5, 3);
      this.groupBox12.Name = "groupBox12";
      this.groupBox12.Size = new System.Drawing.Size(196, 183);
      this.groupBox12.TabIndex = 20;
      this.groupBox12.TabStop = false;
      this.groupBox12.Text = "Track status";
      // 
      // splitContainer17
      // 
      this.splitContainer17.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer17.Location = new System.Drawing.Point(3, 16);
      this.splitContainer17.Name = "splitContainer17";
      this.splitContainer17.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // splitContainer17.Panel1
      // 
      this.splitContainer17.Panel1.Controls.Add(this.splitContainer18);
      // 
      // splitContainer17.Panel2
      // 
      this.splitContainer17.Panel2.Controls.Add(this.splitContainer19);
      this.splitContainer17.Size = new System.Drawing.Size(190, 164);
      this.splitContainer17.SplitterDistance = 80;
      this.splitContainer17.TabIndex = 22;
      // 
      // splitContainer18
      // 
      this.splitContainer18.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer18.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
      this.splitContainer18.IsSplitterFixed = true;
      this.splitContainer18.Location = new System.Drawing.Point(0, 0);
      this.splitContainer18.Name = "splitContainer18";
      this.splitContainer18.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // splitContainer18.Panel1
      // 
      this.splitContainer18.Panel1.BackColor = System.Drawing.Color.Transparent;
      // 
      // splitContainer18.Panel2
      // 
      this.splitContainer18.Panel2.Controls.Add(this.button45);
      this.splitContainer18.Panel2MinSize = 22;
      this.splitContainer18.Size = new System.Drawing.Size(190, 80);
      this.splitContainer18.SplitterDistance = 51;
      this.splitContainer18.TabIndex = 20;
      // 
      // button45
      // 
      this.button45.BackColor = System.Drawing.Color.Transparent;
      this.button45.Dock = System.Windows.Forms.DockStyle.Fill;
      this.button45.Enabled = false;
      this.button45.Location = new System.Drawing.Point(0, 0);
      this.button45.Name = "button45";
      this.button45.Size = new System.Drawing.Size(190, 25);
      this.button45.TabIndex = 21;
      this.button45.Text = "Show on presentation screen";
      this.button45.UseVisualStyleBackColor = false;
      // 
      // splitContainer19
      // 
      this.splitContainer19.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer19.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
      this.splitContainer19.IsSplitterFixed = true;
      this.splitContainer19.Location = new System.Drawing.Point(0, 0);
      this.splitContainer19.Name = "splitContainer19";
      this.splitContainer19.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // splitContainer19.Panel2
      // 
      this.splitContainer19.Panel2.Controls.Add(this.button46);
      this.splitContainer19.Panel2.Controls.Add(this.button47);
      this.splitContainer19.Panel2MinSize = 22;
      this.splitContainer19.Size = new System.Drawing.Size(190, 80);
      this.splitContainer19.SplitterDistance = 51;
      this.splitContainer19.TabIndex = 1;
      // 
      // button46
      // 
      this.button46.Dock = System.Windows.Forms.DockStyle.Right;
      this.button46.Location = new System.Drawing.Point(115, 0);
      this.button46.Name = "button46";
      this.button46.Size = new System.Drawing.Size(75, 25);
      this.button46.TabIndex = 0;
      this.button46.Text = "Recalibrate";
      this.button46.UseVisualStyleBackColor = true;
      // 
      // button47
      // 
      this.button47.Dock = System.Windows.Forms.DockStyle.Left;
      this.button47.Location = new System.Drawing.Point(0, 0);
      this.button47.Name = "button47";
      this.button47.Size = new System.Drawing.Size(75, 25);
      this.button47.TabIndex = 0;
      this.button47.Text = "Accept";
      this.button47.UseVisualStyleBackColor = true;
      // 
      // textBox10
      // 
      this.textBox10.Location = new System.Drawing.Point(107, 229);
      this.textBox10.Name = "textBox10";
      this.textBox10.ReadOnly = true;
      this.textBox10.Size = new System.Drawing.Size(91, 20);
      this.textBox10.TabIndex = 11;
      this.textBox10.Text = "Subject1";
      // 
      // button48
      // 
      this.button48.Enabled = false;
      this.button48.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
      this.button48.ImageKey = "Record";
      this.button48.ImageList = this.imlButtons;
      this.button48.Location = new System.Drawing.Point(29, 285);
      this.button48.Name = "button48";
      this.button48.Size = new System.Drawing.Size(72, 23);
      this.button48.TabIndex = 17;
      this.button48.Text = "&Record";
      this.button48.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      // 
      // button49
      // 
      this.button49.Enabled = false;
      this.button49.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
      this.button49.ImageKey = "Calibrate";
      this.button49.ImageList = this.imlButtons;
      this.button49.Location = new System.Drawing.Point(29, 256);
      this.button49.Name = "button49";
      this.button49.Size = new System.Drawing.Size(72, 23);
      this.button49.TabIndex = 17;
      this.button49.Text = "&Calibrate";
      this.button49.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      // 
      // label61
      // 
      this.label61.AutoSize = true;
      this.label61.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label61.Location = new System.Drawing.Point(5, 256);
      this.label61.Name = "label61";
      this.label61.Size = new System.Drawing.Size(25, 24);
      this.label61.TabIndex = 13;
      this.label61.Text = "3.";
      // 
      // label62
      // 
      this.label62.AutoSize = true;
      this.label62.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label62.Location = new System.Drawing.Point(5, 285);
      this.label62.Name = "label62";
      this.label62.Size = new System.Drawing.Size(25, 24);
      this.label62.TabIndex = 13;
      this.label62.Text = "4.";
      // 
      // label63
      // 
      this.label63.AutoSize = true;
      this.label63.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label63.Location = new System.Drawing.Point(5, 227);
      this.label63.Name = "label63";
      this.label63.Size = new System.Drawing.Size(25, 24);
      this.label63.TabIndex = 13;
      this.label63.Text = "2.";
      // 
      // label64
      // 
      this.label64.AutoSize = true;
      this.label64.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label64.Location = new System.Drawing.Point(5, 198);
      this.label64.Margin = new System.Windows.Forms.Padding(0);
      this.label64.Name = "label64";
      this.label64.Size = new System.Drawing.Size(25, 24);
      this.label64.TabIndex = 13;
      this.label64.Text = "1.";
      // 
      // button50
      // 
      this.button50.Enabled = false;
      this.button50.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
      this.button50.ImageKey = "Subject";
      this.button50.ImageList = this.imlButtons;
      this.button50.Location = new System.Drawing.Point(29, 228);
      this.button50.Name = "button50";
      this.button50.Size = new System.Drawing.Size(72, 23);
      this.button50.TabIndex = 12;
      this.button50.Text = "Subject";
      this.button50.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.button50.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.button50.UseVisualStyleBackColor = true;
      // 
      // button51
      // 
      this.button51.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
      this.button51.ImageKey = "Connect";
      this.button51.ImageList = this.imlButtons;
      this.button51.Location = new System.Drawing.Point(29, 198);
      this.button51.Name = "button51";
      this.button51.Size = new System.Drawing.Size(73, 23);
      this.button51.TabIndex = 12;
      this.button51.Text = "Connect";
      this.button51.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.button51.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.button51.UseVisualStyleBackColor = true;
      // 
      // tabPage9
      // 
      this.tabPage9.Controls.Add(this.groupBox13);
      this.tabPage9.Controls.Add(this.textBox11);
      this.tabPage9.Controls.Add(this.button55);
      this.tabPage9.Controls.Add(this.button56);
      this.tabPage9.Controls.Add(this.label67);
      this.tabPage9.Controls.Add(this.label68);
      this.tabPage9.Controls.Add(this.label69);
      this.tabPage9.Controls.Add(this.label70);
      this.tabPage9.Controls.Add(this.button57);
      this.tabPage9.Controls.Add(this.button58);
      this.tabPage9.ImageKey = "Alea";
      this.tabPage9.Location = new System.Drawing.Point(4, 23);
      this.tabPage9.Name = "tabPage9";
      this.tabPage9.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage9.Size = new System.Drawing.Size(212, 392);
      this.tabPage9.TabIndex = 2;
      this.tabPage9.Text = "Alea";
      this.tabPage9.UseVisualStyleBackColor = true;
      // 
      // groupBox13
      // 
      this.groupBox13.Controls.Add(this.splitContainer20);
      this.groupBox13.Location = new System.Drawing.Point(8, 6);
      this.groupBox13.Name = "groupBox13";
      this.groupBox13.Size = new System.Drawing.Size(196, 183);
      this.groupBox13.TabIndex = 27;
      this.groupBox13.TabStop = false;
      this.groupBox13.Text = "Track status";
      // 
      // splitContainer20
      // 
      this.splitContainer20.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer20.Location = new System.Drawing.Point(3, 16);
      this.splitContainer20.Name = "splitContainer20";
      this.splitContainer20.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // splitContainer20.Panel1
      // 
      this.splitContainer20.Panel1.Controls.Add(this.splitContainer21);
      // 
      // splitContainer20.Panel2
      // 
      this.splitContainer20.Panel2.Controls.Add(this.splitContainer22);
      this.splitContainer20.Size = new System.Drawing.Size(190, 164);
      this.splitContainer20.SplitterDistance = 80;
      this.splitContainer20.TabIndex = 22;
      // 
      // splitContainer21
      // 
      this.splitContainer21.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer21.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
      this.splitContainer21.IsSplitterFixed = true;
      this.splitContainer21.Location = new System.Drawing.Point(0, 0);
      this.splitContainer21.Name = "splitContainer21";
      this.splitContainer21.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // splitContainer21.Panel1
      // 
      this.splitContainer21.Panel1.BackColor = System.Drawing.Color.Transparent;
      // 
      // splitContainer21.Panel2
      // 
      this.splitContainer21.Panel2.Controls.Add(this.button52);
      this.splitContainer21.Panel2MinSize = 22;
      this.splitContainer21.Size = new System.Drawing.Size(190, 80);
      this.splitContainer21.SplitterDistance = 51;
      this.splitContainer21.TabIndex = 20;
      // 
      // button52
      // 
      this.button52.BackColor = System.Drawing.Color.Transparent;
      this.button52.Dock = System.Windows.Forms.DockStyle.Fill;
      this.button52.Enabled = false;
      this.button52.Location = new System.Drawing.Point(0, 0);
      this.button52.Name = "button52";
      this.button52.Size = new System.Drawing.Size(190, 25);
      this.button52.TabIndex = 21;
      this.button52.Text = "Show on presentation screen";
      this.button52.UseVisualStyleBackColor = false;
      // 
      // splitContainer22
      // 
      this.splitContainer22.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer22.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
      this.splitContainer22.IsSplitterFixed = true;
      this.splitContainer22.Location = new System.Drawing.Point(0, 0);
      this.splitContainer22.Name = "splitContainer22";
      this.splitContainer22.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // splitContainer22.Panel1
      // 
      this.splitContainer22.Panel1.BackColor = System.Drawing.Color.Transparent;
      this.splitContainer22.Panel1.Controls.Add(this.label65);
      this.splitContainer22.Panel1.Controls.Add(this.label66);
      // 
      // splitContainer22.Panel2
      // 
      this.splitContainer22.Panel2.Controls.Add(this.button53);
      this.splitContainer22.Panel2.Controls.Add(this.button54);
      this.splitContainer22.Panel2MinSize = 22;
      this.splitContainer22.Size = new System.Drawing.Size(190, 80);
      this.splitContainer22.SplitterDistance = 51;
      this.splitContainer22.TabIndex = 1;
      // 
      // label65
      // 
      this.label65.AutoSize = true;
      this.label65.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label65.Location = new System.Drawing.Point(69, 32);
      this.label65.Name = "label65";
      this.label65.Size = new System.Drawing.Size(50, 13);
      this.label65.TabIndex = 1;
      this.label65.Text = "Not Set";
      // 
      // label66
      // 
      this.label66.AutoSize = true;
      this.label66.Location = new System.Drawing.Point(50, 10);
      this.label66.Name = "label66";
      this.label66.Size = new System.Drawing.Size(89, 13);
      this.label66.TabIndex = 0;
      this.label66.Text = "Calibration Result";
      // 
      // button53
      // 
      this.button53.Dock = System.Windows.Forms.DockStyle.Right;
      this.button53.Location = new System.Drawing.Point(115, 0);
      this.button53.Name = "button53";
      this.button53.Size = new System.Drawing.Size(75, 25);
      this.button53.TabIndex = 0;
      this.button53.Text = "Recalibrate";
      this.button53.UseVisualStyleBackColor = true;
      // 
      // button54
      // 
      this.button54.Dock = System.Windows.Forms.DockStyle.Left;
      this.button54.Location = new System.Drawing.Point(0, 0);
      this.button54.Name = "button54";
      this.button54.Size = new System.Drawing.Size(75, 25);
      this.button54.TabIndex = 0;
      this.button54.Text = "Accept";
      this.button54.UseVisualStyleBackColor = true;
      // 
      // textBox11
      // 
      this.textBox11.Location = new System.Drawing.Point(110, 232);
      this.textBox11.Name = "textBox11";
      this.textBox11.ReadOnly = true;
      this.textBox11.Size = new System.Drawing.Size(91, 20);
      this.textBox11.TabIndex = 18;
      this.textBox11.Text = "Subject1";
      // 
      // button55
      // 
      this.button55.Enabled = false;
      this.button55.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
      this.button55.ImageKey = "Record";
      this.button55.ImageList = this.imlButtons;
      this.button55.Location = new System.Drawing.Point(32, 288);
      this.button55.Name = "button55";
      this.button55.Size = new System.Drawing.Size(72, 23);
      this.button55.TabIndex = 26;
      this.button55.Text = "&Record";
      this.button55.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      // 
      // button56
      // 
      this.button56.Enabled = false;
      this.button56.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
      this.button56.ImageKey = "Calibrate";
      this.button56.ImageList = this.imlButtons;
      this.button56.Location = new System.Drawing.Point(32, 259);
      this.button56.Name = "button56";
      this.button56.Size = new System.Drawing.Size(72, 23);
      this.button56.TabIndex = 25;
      this.button56.Text = "&Calibrate";
      this.button56.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      // 
      // label67
      // 
      this.label67.AutoSize = true;
      this.label67.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label67.Location = new System.Drawing.Point(8, 259);
      this.label67.Name = "label67";
      this.label67.Size = new System.Drawing.Size(25, 24);
      this.label67.TabIndex = 24;
      this.label67.Text = "3.";
      // 
      // label68
      // 
      this.label68.AutoSize = true;
      this.label68.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label68.Location = new System.Drawing.Point(8, 288);
      this.label68.Name = "label68";
      this.label68.Size = new System.Drawing.Size(25, 24);
      this.label68.TabIndex = 22;
      this.label68.Text = "4.";
      // 
      // label69
      // 
      this.label69.AutoSize = true;
      this.label69.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label69.Location = new System.Drawing.Point(8, 230);
      this.label69.Name = "label69";
      this.label69.Size = new System.Drawing.Size(25, 24);
      this.label69.TabIndex = 23;
      this.label69.Text = "2.";
      // 
      // label70
      // 
      this.label70.AutoSize = true;
      this.label70.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label70.Location = new System.Drawing.Point(8, 201);
      this.label70.Margin = new System.Windows.Forms.Padding(0);
      this.label70.Name = "label70";
      this.label70.Size = new System.Drawing.Size(25, 24);
      this.label70.TabIndex = 21;
      this.label70.Text = "1.";
      // 
      // button57
      // 
      this.button57.Enabled = false;
      this.button57.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
      this.button57.ImageKey = "Subject";
      this.button57.ImageList = this.imlButtons;
      this.button57.Location = new System.Drawing.Point(32, 231);
      this.button57.Name = "button57";
      this.button57.Size = new System.Drawing.Size(72, 23);
      this.button57.TabIndex = 20;
      this.button57.Text = "Subject";
      this.button57.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.button57.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.button57.UseVisualStyleBackColor = true;
      // 
      // button58
      // 
      this.button58.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
      this.button58.ImageKey = "Connect";
      this.button58.ImageList = this.imlButtons;
      this.button58.Location = new System.Drawing.Point(32, 201);
      this.button58.Name = "button58";
      this.button58.Size = new System.Drawing.Size(73, 23);
      this.button58.TabIndex = 19;
      this.button58.Text = "Connect";
      this.button58.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.button58.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.button58.UseVisualStyleBackColor = true;
      // 
      // tabPage10
      // 
      this.tabPage10.Controls.Add(this.groupBox14);
      this.tabPage10.Controls.Add(this.textBox13);
      this.tabPage10.Controls.Add(this.button59);
      this.tabPage10.Controls.Add(this.button60);
      this.tabPage10.Controls.Add(this.label71);
      this.tabPage10.Controls.Add(this.label72);
      this.tabPage10.Controls.Add(this.label73);
      this.tabPage10.Controls.Add(this.label74);
      this.tabPage10.Controls.Add(this.button61);
      this.tabPage10.Controls.Add(this.button62);
      this.tabPage10.ImageKey = "SMI";
      this.tabPage10.Location = new System.Drawing.Point(4, 23);
      this.tabPage10.Name = "tabPage10";
      this.tabPage10.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage10.Size = new System.Drawing.Size(212, 392);
      this.tabPage10.TabIndex = 4;
      this.tabPage10.Text = "SMI";
      this.tabPage10.UseVisualStyleBackColor = true;
      // 
      // groupBox14
      // 
      this.groupBox14.Controls.Add(this.textBox12);
      this.groupBox14.Location = new System.Drawing.Point(8, 8);
      this.groupBox14.Name = "groupBox14";
      this.groupBox14.Size = new System.Drawing.Size(196, 183);
      this.groupBox14.TabIndex = 30;
      this.groupBox14.TabStop = false;
      this.groupBox14.Text = "Track status";
      // 
      // textBox12
      // 
      this.textBox12.Location = new System.Drawing.Point(6, 19);
      this.textBox12.Multiline = true;
      this.textBox12.Name = "textBox12";
      this.textBox12.Size = new System.Drawing.Size(184, 137);
      this.textBox12.TabIndex = 0;
      this.textBox12.Text = resources.GetString("textBox12.Text");
      // 
      // textBox13
      // 
      this.textBox13.Location = new System.Drawing.Point(110, 234);
      this.textBox13.Name = "textBox13";
      this.textBox13.ReadOnly = true;
      this.textBox13.Size = new System.Drawing.Size(91, 20);
      this.textBox13.TabIndex = 21;
      this.textBox13.Text = "Subject1";
      // 
      // button59
      // 
      this.button59.Enabled = false;
      this.button59.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
      this.button59.ImageKey = "Record";
      this.button59.ImageList = this.imlButtons;
      this.button59.Location = new System.Drawing.Point(32, 290);
      this.button59.Name = "button59";
      this.button59.Size = new System.Drawing.Size(72, 23);
      this.button59.TabIndex = 28;
      this.button59.Text = "&Record";
      this.button59.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      // 
      // button60
      // 
      this.button60.Enabled = false;
      this.button60.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
      this.button60.ImageKey = "Calibrate";
      this.button60.ImageList = this.imlButtons;
      this.button60.Location = new System.Drawing.Point(32, 261);
      this.button60.Name = "button60";
      this.button60.Size = new System.Drawing.Size(72, 23);
      this.button60.TabIndex = 29;
      this.button60.Text = "&Calibrate";
      this.button60.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      // 
      // label71
      // 
      this.label71.AutoSize = true;
      this.label71.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label71.Location = new System.Drawing.Point(8, 261);
      this.label71.Name = "label71";
      this.label71.Size = new System.Drawing.Size(25, 24);
      this.label71.TabIndex = 27;
      this.label71.Text = "3.";
      // 
      // label72
      // 
      this.label72.AutoSize = true;
      this.label72.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label72.Location = new System.Drawing.Point(8, 290);
      this.label72.Name = "label72";
      this.label72.Size = new System.Drawing.Size(25, 24);
      this.label72.TabIndex = 26;
      this.label72.Text = "4.";
      // 
      // label73
      // 
      this.label73.AutoSize = true;
      this.label73.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label73.Location = new System.Drawing.Point(8, 232);
      this.label73.Name = "label73";
      this.label73.Size = new System.Drawing.Size(25, 24);
      this.label73.TabIndex = 24;
      this.label73.Text = "2.";
      // 
      // label74
      // 
      this.label74.AutoSize = true;
      this.label74.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label74.Location = new System.Drawing.Point(8, 203);
      this.label74.Margin = new System.Windows.Forms.Padding(0);
      this.label74.Name = "label74";
      this.label74.Size = new System.Drawing.Size(25, 24);
      this.label74.TabIndex = 25;
      this.label74.Text = "1.";
      // 
      // button61
      // 
      this.button61.Enabled = false;
      this.button61.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
      this.button61.ImageKey = "Subject";
      this.button61.ImageList = this.imlButtons;
      this.button61.Location = new System.Drawing.Point(32, 233);
      this.button61.Name = "button61";
      this.button61.Size = new System.Drawing.Size(72, 23);
      this.button61.TabIndex = 22;
      this.button61.Text = "Subject";
      this.button61.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.button61.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.button61.UseVisualStyleBackColor = true;
      // 
      // button62
      // 
      this.button62.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
      this.button62.ImageKey = "Connect";
      this.button62.ImageList = this.imlButtons;
      this.button62.Location = new System.Drawing.Point(32, 203);
      this.button62.Name = "button62";
      this.button62.Size = new System.Drawing.Size(73, 23);
      this.button62.TabIndex = 23;
      this.button62.Text = "Connect";
      this.button62.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.button62.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.button62.UseVisualStyleBackColor = true;
      // 
      // tabPage11
      // 
      this.tabPage11.Controls.Add(this.label77);
      this.tabPage11.Controls.Add(this.label78);
      this.tabPage11.Controls.Add(this.button63);
      this.tabPage11.Controls.Add(this.textBox14);
      this.tabPage11.Controls.Add(this.button64);
      this.tabPage11.ImageKey = "Mouse";
      this.tabPage11.Location = new System.Drawing.Point(4, 23);
      this.tabPage11.Name = "tabPage11";
      this.tabPage11.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage11.Size = new System.Drawing.Size(212, 392);
      this.tabPage11.TabIndex = 1;
      this.tabPage11.Text = "MouseOnly";
      this.tabPage11.UseVisualStyleBackColor = true;
      // 
      // label77
      // 
      this.label77.AutoSize = true;
      this.label77.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label77.Location = new System.Drawing.Point(7, 42);
      this.label77.Name = "label77";
      this.label77.Size = new System.Drawing.Size(25, 24);
      this.label77.TabIndex = 21;
      this.label77.Text = "2.";
      // 
      // label78
      // 
      this.label78.AutoSize = true;
      this.label78.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label78.Location = new System.Drawing.Point(7, 13);
      this.label78.Name = "label78";
      this.label78.Size = new System.Drawing.Size(25, 24);
      this.label78.TabIndex = 20;
      this.label78.Text = "1.";
      // 
      // button63
      // 
      this.button63.ImageKey = "Subject";
      this.button63.ImageList = this.imlButtons;
      this.button63.Location = new System.Drawing.Point(32, 13);
      this.button63.Name = "button63";
      this.button63.Size = new System.Drawing.Size(71, 23);
      this.button63.TabIndex = 19;
      this.button63.Text = "Subject";
      this.button63.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.button63.UseVisualStyleBackColor = true;
      // 
      // textBox14
      // 
      this.textBox14.Location = new System.Drawing.Point(115, 15);
      this.textBox14.Name = "textBox14";
      this.textBox14.ReadOnly = true;
      this.textBox14.Size = new System.Drawing.Size(87, 20);
      this.textBox14.TabIndex = 18;
      this.textBox14.Text = "Subject1";
      // 
      // button64
      // 
      this.button64.BackColor = System.Drawing.Color.Transparent;
      this.button64.Enabled = false;
      this.button64.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
      this.button64.ImageKey = "Record";
      this.button64.ImageList = this.imlButtons;
      this.button64.Location = new System.Drawing.Point(32, 42);
      this.button64.Name = "button64";
      this.button64.Size = new System.Drawing.Size(71, 23);
      this.button64.TabIndex = 14;
      this.button64.Text = "Record";
      this.button64.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.button64.UseVisualStyleBackColor = false;
      // 
      // tabPage12
      // 
      this.tabPage12.Controls.Add(this.label79);
      this.tabPage12.Controls.Add(this.button65);
      this.tabPage12.ImageKey = "None";
      this.tabPage12.Location = new System.Drawing.Point(4, 23);
      this.tabPage12.Name = "tabPage12";
      this.tabPage12.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage12.Size = new System.Drawing.Size(212, 392);
      this.tabPage12.TabIndex = 3;
      this.tabPage12.Text = "No tracking device selected";
      this.tabPage12.UseVisualStyleBackColor = true;
      // 
      // label79
      // 
      this.label79.Location = new System.Drawing.Point(12, 10);
      this.label79.Name = "label79";
      this.label79.Size = new System.Drawing.Size(194, 46);
      this.label79.TabIndex = 1;
      this.label79.Text = "No tracking device available.\r\nClick the button to see which tracking devices are" +
    " supported/available.";
      // 
      // button65
      // 
      this.button65.Image = global::Ogama.Properties.Resources.LegendHS;
      this.button65.Location = new System.Drawing.Point(27, 59);
      this.button65.Name = "button65";
      this.button65.Size = new System.Drawing.Size(149, 34);
      this.button65.TabIndex = 0;
      this.button65.Text = "Select tracking device";
      this.button65.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.button65.UseVisualStyleBackColor = true;
      // 
      // tbpSMIRedM
      // 
      this.tbpSMIRedM.Controls.Add(this.groupBox18);
      this.tbpSMIRedM.Controls.Add(this.txbSMIRedMSubject);
      this.tbpSMIRedM.Controls.Add(this.btnSMIRedMRecord);
      this.tbpSMIRedM.Controls.Add(this.btnSMIRedMCalibrate);
      this.tbpSMIRedM.Controls.Add(this.label92);
      this.tbpSMIRedM.Controls.Add(this.label97);
      this.tbpSMIRedM.Controls.Add(this.label98);
      this.tbpSMIRedM.Controls.Add(this.label99);
      this.tbpSMIRedM.Controls.Add(this.btnSMIRedMSubject);
      this.tbpSMIRedM.Controls.Add(this.btnSMIRedMConnect);
      this.tbpSMIRedM.Location = new System.Drawing.Point(4, 23);
      this.tbpSMIRedM.Name = "tbpSMIRedM";
      this.tbpSMIRedM.Padding = new System.Windows.Forms.Padding(3);
      this.tbpSMIRedM.Size = new System.Drawing.Size(212, 385);
      this.tbpSMIRedM.TabIndex = 13;
      this.tbpSMIRedM.Text = "SMI RedM";
      this.tbpSMIRedM.UseVisualStyleBackColor = true;
      // 
      // groupBox18
      // 
      this.groupBox18.Controls.Add(this.textBox16);
      this.groupBox18.Location = new System.Drawing.Point(8, 5);
      this.groupBox18.Name = "groupBox18";
      this.groupBox18.Size = new System.Drawing.Size(196, 183);
      this.groupBox18.TabIndex = 40;
      this.groupBox18.TabStop = false;
      this.groupBox18.Text = "Track status";
      // 
      // textBox16
      // 
      this.textBox16.Location = new System.Drawing.Point(6, 19);
      this.textBox16.Multiline = true;
      this.textBox16.Name = "textBox16";
      this.textBox16.Size = new System.Drawing.Size(184, 137);
      this.textBox16.TabIndex = 0;
      this.textBox16.Text = resources.GetString("textBox16.Text");
      // 
      // txbSMIRedMSubject
      // 
      this.txbSMIRedMSubject.Location = new System.Drawing.Point(110, 231);
      this.txbSMIRedMSubject.Name = "txbSMIRedMSubject";
      this.txbSMIRedMSubject.ReadOnly = true;
      this.txbSMIRedMSubject.Size = new System.Drawing.Size(91, 20);
      this.txbSMIRedMSubject.TabIndex = 31;
      this.txbSMIRedMSubject.Text = "Subject1";
      // 
      // btnSMIRedMRecord
      // 
      this.btnSMIRedMRecord.Enabled = false;
      this.btnSMIRedMRecord.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
      this.btnSMIRedMRecord.ImageKey = "Record";
      this.btnSMIRedMRecord.ImageList = this.imlButtons;
      this.btnSMIRedMRecord.Location = new System.Drawing.Point(32, 287);
      this.btnSMIRedMRecord.Name = "btnSMIRedMRecord";
      this.btnSMIRedMRecord.Size = new System.Drawing.Size(72, 23);
      this.btnSMIRedMRecord.TabIndex = 38;
      this.btnSMIRedMRecord.Text = "&Record";
      this.btnSMIRedMRecord.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      // 
      // btnSMIRedMCalibrate
      // 
      this.btnSMIRedMCalibrate.Enabled = false;
      this.btnSMIRedMCalibrate.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
      this.btnSMIRedMCalibrate.ImageKey = "Calibrate";
      this.btnSMIRedMCalibrate.ImageList = this.imlButtons;
      this.btnSMIRedMCalibrate.Location = new System.Drawing.Point(32, 258);
      this.btnSMIRedMCalibrate.Name = "btnSMIRedMCalibrate";
      this.btnSMIRedMCalibrate.Size = new System.Drawing.Size(72, 23);
      this.btnSMIRedMCalibrate.TabIndex = 39;
      this.btnSMIRedMCalibrate.Text = "&Calibrate";
      this.btnSMIRedMCalibrate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      // 
      // label92
      // 
      this.label92.AutoSize = true;
      this.label92.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label92.Location = new System.Drawing.Point(8, 258);
      this.label92.Name = "label92";
      this.label92.Size = new System.Drawing.Size(25, 24);
      this.label92.TabIndex = 37;
      this.label92.Text = "3.";
      // 
      // label97
      // 
      this.label97.AutoSize = true;
      this.label97.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label97.Location = new System.Drawing.Point(8, 287);
      this.label97.Name = "label97";
      this.label97.Size = new System.Drawing.Size(25, 24);
      this.label97.TabIndex = 36;
      this.label97.Text = "4.";
      // 
      // label98
      // 
      this.label98.AutoSize = true;
      this.label98.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label98.Location = new System.Drawing.Point(8, 229);
      this.label98.Name = "label98";
      this.label98.Size = new System.Drawing.Size(25, 24);
      this.label98.TabIndex = 34;
      this.label98.Text = "2.";
      // 
      // label99
      // 
      this.label99.AutoSize = true;
      this.label99.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label99.Location = new System.Drawing.Point(8, 200);
      this.label99.Margin = new System.Windows.Forms.Padding(0);
      this.label99.Name = "label99";
      this.label99.Size = new System.Drawing.Size(25, 24);
      this.label99.TabIndex = 35;
      this.label99.Text = "1.";
      // 
      // btnSMIRedMSubject
      // 
      this.btnSMIRedMSubject.Enabled = false;
      this.btnSMIRedMSubject.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
      this.btnSMIRedMSubject.ImageKey = "Subject";
      this.btnSMIRedMSubject.ImageList = this.imlButtons;
      this.btnSMIRedMSubject.Location = new System.Drawing.Point(32, 230);
      this.btnSMIRedMSubject.Name = "btnSMIRedMSubject";
      this.btnSMIRedMSubject.Size = new System.Drawing.Size(72, 23);
      this.btnSMIRedMSubject.TabIndex = 32;
      this.btnSMIRedMSubject.Text = "Subject";
      this.btnSMIRedMSubject.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.btnSMIRedMSubject.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.btnSMIRedMSubject.UseVisualStyleBackColor = true;
      // 
      // btnSMIRedMConnect
      // 
      this.btnSMIRedMConnect.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
      this.btnSMIRedMConnect.ImageKey = "Connect";
      this.btnSMIRedMConnect.ImageList = this.imlButtons;
      this.btnSMIRedMConnect.Location = new System.Drawing.Point(32, 200);
      this.btnSMIRedMConnect.Name = "btnSMIRedMConnect";
      this.btnSMIRedMConnect.Size = new System.Drawing.Size(73, 23);
      this.btnSMIRedMConnect.TabIndex = 33;
      this.btnSMIRedMConnect.Text = "Connect";
      this.btnSMIRedMConnect.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.btnSMIRedMConnect.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.btnSMIRedMConnect.UseVisualStyleBackColor = true;
      // 
      // RecordModule
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(863, 670);
      this.Controls.Add(this.toolStripContainer1);
      this.DataBindings.Add(new System.Windows.Forms.Binding("Location", global::Ogama.Properties.Settings.Default, "RecordingModuleLocation", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.DoubleBuffered = true;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Location = global::Ogama.Properties.Settings.Default.RecordingModuleLocation;
      this.Logo = global::Ogama.Properties.Resources.Record;
      this.Name = "RecordModule";
      this.Text = "Recording module";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RecordModuleFormClosing);
      this.Load += new System.EventHandler(this.RecordModuleLoad);
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
      this.sctRecording.Panel1.ResumeLayout(false);
      this.sctRecording.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.sctRecording)).EndInit();
      this.sctRecording.ResumeLayout(false);
      this.spcPanelUserCam.Panel1.ResumeLayout(false);
      this.spcPanelUserCam.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.spcPanelUserCam)).EndInit();
      this.spcPanelUserCam.ResumeLayout(false);
      this.spcPanelRecordTime.Panel1.ResumeLayout(false);
      this.spcPanelRecordTime.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.spcPanelRecordTime)).EndInit();
      this.spcPanelRecordTime.ResumeLayout(false);
      this.tclEyetracker.ResumeLayout(false);
      this.tbpGazetrackerIPClient.ResumeLayout(false);
      this.panel11.ResumeLayout(false);
      this.panel11.PerformLayout();
      this.groupBox15.ResumeLayout(false);
      this.groupBox15.PerformLayout();
      this.tbpGazetrackerDirectClient.ResumeLayout(false);
      this.panel3.ResumeLayout(false);
      this.panel3.PerformLayout();
      this.groupBox4.ResumeLayout(false);
      this.splitContainer23.Panel1.ResumeLayout(false);
      this.splitContainer23.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer23)).EndInit();
      this.splitContainer23.ResumeLayout(false);
      this.tbpTobii.ResumeLayout(false);
      this.panel1.ResumeLayout(false);
      this.panel1.PerformLayout();
      this.groupBox1.ResumeLayout(false);
      this.spcTobiiControls.Panel1.ResumeLayout(false);
      this.spcTobiiControls.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.spcTobiiControls)).EndInit();
      this.spcTobiiControls.ResumeLayout(false);
      this.spcTobiiTrackStatus.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.spcTobiiTrackStatus)).EndInit();
      this.spcTobiiTrackStatus.ResumeLayout(false);
      this.spcTobiiCalibPlot.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.spcTobiiCalibPlot)).EndInit();
      this.spcTobiiCalibPlot.ResumeLayout(false);
      this.tbpGazepoint.ResumeLayout(false);
      this.tbpGazepoint.PerformLayout();
      this.groupBox17.ResumeLayout(false);
      this.spcGazepointControls.Panel1.ResumeLayout(false);
      this.spcGazepointControls.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.spcGazepointControls)).EndInit();
      this.spcGazepointControls.ResumeLayout(false);
      this.spcGazepointTrackStatus.Panel1.ResumeLayout(false);
      this.spcGazepointTrackStatus.Panel1.PerformLayout();
      this.spcGazepointTrackStatus.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.spcGazepointTrackStatus)).EndInit();
      this.spcGazepointTrackStatus.ResumeLayout(false);
      this.spcGazepointCalibPlot.Panel1.ResumeLayout(false);
      this.spcGazepointCalibPlot.Panel1.PerformLayout();
      this.spcGazepointCalibPlot.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.spcGazepointCalibPlot)).EndInit();
      this.spcGazepointCalibPlot.ResumeLayout(false);
      this.tbpMirametrix.ResumeLayout(false);
      this.panel12.ResumeLayout(false);
      this.panel12.PerformLayout();
      this.groupBox16.ResumeLayout(false);
      this.spcMirametrixControls.Panel1.ResumeLayout(false);
      this.spcMirametrixControls.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.spcMirametrixControls)).EndInit();
      this.spcMirametrixControls.ResumeLayout(false);
      this.spcMirametrixTrackStatus.Panel1.ResumeLayout(false);
      this.spcMirametrixTrackStatus.Panel1.PerformLayout();
      this.spcMirametrixTrackStatus.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.spcMirametrixTrackStatus)).EndInit();
      this.spcMirametrixTrackStatus.ResumeLayout(false);
      this.spcMirametrixCalibPlot.Panel1.ResumeLayout(false);
      this.spcMirametrixCalibPlot.Panel1.PerformLayout();
      this.spcMirametrixCalibPlot.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.spcMirametrixCalibPlot)).EndInit();
      this.spcMirametrixCalibPlot.ResumeLayout(false);
      this.tbpAlea.ResumeLayout(false);
      this.tbpAlea.PerformLayout();
      this.groupBox2.ResumeLayout(false);
      this.spcAleaControls.Panel1.ResumeLayout(false);
      this.spcAleaControls.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.spcAleaControls)).EndInit();
      this.spcAleaControls.ResumeLayout(false);
      this.spcAleaTrackStatus.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.spcAleaTrackStatus)).EndInit();
      this.spcAleaTrackStatus.ResumeLayout(false);
      this.spcAleaCalibPlot.Panel1.ResumeLayout(false);
      this.spcAleaCalibPlot.Panel1.PerformLayout();
      this.spcAleaCalibPlot.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.spcAleaCalibPlot)).EndInit();
      this.spcAleaCalibPlot.ResumeLayout(false);
      this.tbpSMIiViewX.ResumeLayout(false);
      this.tbpSMIiViewX.PerformLayout();
      this.groupBox3.ResumeLayout(false);
      this.groupBox3.PerformLayout();
      this.tbpAsl.ResumeLayout(false);
      this.tbpAsl.PerformLayout();
      this.tbpEyeTech.ResumeLayout(false);
      this.tbpEyeTech.PerformLayout();
      this.groupBox20.ResumeLayout(false);
      this.groupBox20.PerformLayout();
      this.tbpMouseOnly.ResumeLayout(false);
      this.tbpMouseOnly.PerformLayout();
      this.tbpNoDevice.ResumeLayout(false);
      this.panel4.ResumeLayout(false);
      this.panel4.PerformLayout();
      this.grpUsercam.ResumeLayout(false);
      this.splitContainer2.Panel1.ResumeLayout(false);
      this.splitContainer2.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
      this.splitContainer2.ResumeLayout(false);
      this.panel2.ResumeLayout(false);
      this.toolStripContainer2.BottomToolStripPanel.ResumeLayout(false);
      this.toolStripContainer2.BottomToolStripPanel.PerformLayout();
      this.toolStripContainer2.ContentPanel.ResumeLayout(false);
      this.toolStripContainer2.ResumeLayout(false);
      this.toolStripContainer2.PerformLayout();
      this.toolStrip2.ResumeLayout(false);
      this.toolStrip2.PerformLayout();
      this.pnlCanvas.ResumeLayout(false);
      this.pnlPicture.ResumeLayout(false);
      this.toolStrip1.ResumeLayout(false);
      this.toolStrip1.PerformLayout();
      this.tabPage1.ResumeLayout(false);
      this.panel5.ResumeLayout(false);
      this.panel5.PerformLayout();
      this.groupBox5.ResumeLayout(false);
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
      this.splitContainer1.ResumeLayout(false);
      this.splitContainer3.Panel1.ResumeLayout(false);
      this.splitContainer3.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
      this.splitContainer3.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
      this.splitContainer4.Panel1.ResumeLayout(false);
      this.splitContainer4.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).EndInit();
      this.splitContainer4.ResumeLayout(false);
      this.tabPage2.ResumeLayout(false);
      this.panel7.ResumeLayout(false);
      this.panel7.PerformLayout();
      this.groupBox6.ResumeLayout(false);
      this.splitContainer5.Panel1.ResumeLayout(false);
      this.splitContainer5.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer5)).EndInit();
      this.splitContainer5.ResumeLayout(false);
      this.splitContainer6.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer6)).EndInit();
      this.splitContainer6.ResumeLayout(false);
      this.splitContainer7.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer7)).EndInit();
      this.splitContainer7.ResumeLayout(false);
      this.tabPage3.ResumeLayout(false);
      this.tabPage3.PerformLayout();
      this.groupBox7.ResumeLayout(false);
      this.splitContainer8.Panel1.ResumeLayout(false);
      this.splitContainer8.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer8)).EndInit();
      this.splitContainer8.ResumeLayout(false);
      this.splitContainer9.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer9)).EndInit();
      this.splitContainer9.ResumeLayout(false);
      this.splitContainer10.Panel1.ResumeLayout(false);
      this.splitContainer10.Panel1.PerformLayout();
      this.splitContainer10.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer10)).EndInit();
      this.splitContainer10.ResumeLayout(false);
      this.tabPage4.ResumeLayout(false);
      this.tabPage4.PerformLayout();
      this.groupBox8.ResumeLayout(false);
      this.groupBox8.PerformLayout();
      this.tabPage5.ResumeLayout(false);
      this.tabPage5.PerformLayout();
      this.tabPage6.ResumeLayout(false);
      this.groupBox10.ResumeLayout(false);
      this.splitContainer11.Panel1.ResumeLayout(false);
      this.splitContainer11.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer11)).EndInit();
      this.splitContainer11.ResumeLayout(false);
      this.splitContainer12.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer12)).EndInit();
      this.splitContainer12.ResumeLayout(false);
      this.splitContainer13.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer13)).EndInit();
      this.splitContainer13.ResumeLayout(false);
      this.tabPage7.ResumeLayout(false);
      this.panel8.ResumeLayout(false);
      this.panel8.PerformLayout();
      this.groupBox9.ResumeLayout(false);
      this.splitContainer14.Panel1.ResumeLayout(false);
      this.splitContainer14.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer14)).EndInit();
      this.splitContainer14.ResumeLayout(false);
      this.splitContainer15.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer15)).EndInit();
      this.splitContainer15.ResumeLayout(false);
      this.splitContainer16.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer16)).EndInit();
      this.splitContainer16.ResumeLayout(false);
      this.tbpITUPS3.ResumeLayout(false);
      this.panel9.ResumeLayout(false);
      this.panel9.PerformLayout();
      this.groupBox11.ResumeLayout(false);
      this.spcITUPS3Controls.Panel1.ResumeLayout(false);
      this.spcITUPS3Controls.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.spcITUPS3Controls)).EndInit();
      this.spcITUPS3Controls.ResumeLayout(false);
      this.spcITUPS3TrackStatus.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.spcITUPS3TrackStatus)).EndInit();
      this.spcITUPS3TrackStatus.ResumeLayout(false);
      this.spcITUPS3CalibPlot.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.spcITUPS3CalibPlot)).EndInit();
      this.spcITUPS3CalibPlot.ResumeLayout(false);
      this.tabPage8.ResumeLayout(false);
      this.panel10.ResumeLayout(false);
      this.panel10.PerformLayout();
      this.groupBox12.ResumeLayout(false);
      this.splitContainer17.Panel1.ResumeLayout(false);
      this.splitContainer17.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer17)).EndInit();
      this.splitContainer17.ResumeLayout(false);
      this.splitContainer18.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer18)).EndInit();
      this.splitContainer18.ResumeLayout(false);
      this.splitContainer19.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer19)).EndInit();
      this.splitContainer19.ResumeLayout(false);
      this.tabPage9.ResumeLayout(false);
      this.tabPage9.PerformLayout();
      this.groupBox13.ResumeLayout(false);
      this.splitContainer20.Panel1.ResumeLayout(false);
      this.splitContainer20.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer20)).EndInit();
      this.splitContainer20.ResumeLayout(false);
      this.splitContainer21.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer21)).EndInit();
      this.splitContainer21.ResumeLayout(false);
      this.splitContainer22.Panel1.ResumeLayout(false);
      this.splitContainer22.Panel1.PerformLayout();
      this.splitContainer22.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer22)).EndInit();
      this.splitContainer22.ResumeLayout(false);
      this.tabPage10.ResumeLayout(false);
      this.tabPage10.PerformLayout();
      this.groupBox14.ResumeLayout(false);
      this.groupBox14.PerformLayout();
      this.tabPage11.ResumeLayout(false);
      this.tabPage11.PerformLayout();
      this.tabPage12.ResumeLayout(false);
      this.tbpSMIRedM.ResumeLayout(false);
      this.tbpSMIRedM.PerformLayout();
      this.groupBox18.ResumeLayout(false);
      this.groupBox18.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.ToolStripContainer toolStripContainer1;
    private System.Windows.Forms.ImageList imlSlides;
    private System.Windows.Forms.ImageList imlEyetracker;
    private System.Windows.Forms.SplitContainer sctRecording;
    private System.Windows.Forms.ToolTip toolTip1;
    private System.Windows.Forms.Panel pnlCanvas;
    private System.Windows.Forms.ToolStrip toolStrip1;
    private System.Windows.Forms.SplitContainer spcPanelUserCam;
    private System.Windows.Forms.GroupBox grpUsercam;
    private System.Windows.Forms.ToolStripButton btnUsercam;
    private System.Windows.Forms.SplitContainer splitContainer2;
    private System.Windows.Forms.Panel panel2;
    private System.Windows.Forms.CheckBox chbRecordAudio;
    private System.Windows.Forms.CheckBox chbRecordVideo;
    private System.Windows.Forms.ImageList imlButtons;
    private System.Windows.Forms.ToolStripButton btnTrigger;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    private System.Windows.Forms.ToolStripButton btnWebcamSettings;
    private System.Windows.Forms.ToolStripButton btnTriggerSettings;
    private System.ComponentModel.BackgroundWorker bgwSaveSplash;
    private System.Windows.Forms.Panel pnlPicture;
    private RecordingPicture recordPicture;
    private System.ComponentModel.BackgroundWorker bgwCalcFixations;
    private System.Windows.Forms.ToolStripButton btnScreenCaptureSettings;
    private System.Windows.Forms.ToolStripButton btnPrimary;
    private System.Windows.Forms.ToolStripButton btnSecondary;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
    private System.Windows.Forms.ToolStripButton btnSelectTracker;
    private System.Windows.Forms.ToolStripButton btnTrackerSettings;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
    private System.Windows.Forms.ToolStripButton btnSmoothing;
    private System.Windows.Forms.ToolStripButton btnHelp;
    private OgamaControls.Webcam webcamPreview;
    private System.Windows.Forms.Timer tmrWaitForPresentationEnd;
    private System.Windows.Forms.SplitContainer spcPanelRecordTime;
    private System.Windows.Forms.Panel panel4;
    private System.Windows.Forms.Label lblRecordedTime;
    private System.Windows.Forms.Label label21;
    private System.Windows.Forms.Timer tmrRecordClock;
    private System.Windows.Forms.TabPage tabPage1;
    private System.Windows.Forms.Panel panel5;
    private System.Windows.Forms.GroupBox groupBox5;
    private System.Windows.Forms.SplitContainer splitContainer1;
    private System.Windows.Forms.SplitContainer splitContainer3;
    private System.Windows.Forms.PictureBox pictureBox1;
    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.SplitContainer splitContainer4;
    private System.Windows.Forms.Panel panel6;
    private System.Windows.Forms.Button button2;
    private System.Windows.Forms.Button button3;
    private System.Windows.Forms.TextBox textBox2;
    private System.Windows.Forms.Button button4;
    private System.Windows.Forms.Button button5;
    private System.Windows.Forms.Label label22;
    private System.Windows.Forms.Label label23;
    private System.Windows.Forms.Label label24;
    private System.Windows.Forms.Label label25;
    private System.Windows.Forms.Button button6;
    private System.Windows.Forms.Button button7;
    private System.Windows.Forms.TabPage tabPage2;
    private System.Windows.Forms.Panel panel7;
    private System.Windows.Forms.GroupBox groupBox6;
    private System.Windows.Forms.SplitContainer splitContainer5;
    private System.Windows.Forms.SplitContainer splitContainer6;
    private System.Windows.Forms.Button button8;
    private System.Windows.Forms.SplitContainer splitContainer7;
    private System.Windows.Forms.Button button9;
    private System.Windows.Forms.Button button10;
    private System.Windows.Forms.TextBox textBox3;
    private System.Windows.Forms.Button button11;
    private System.Windows.Forms.Button button12;
    private System.Windows.Forms.Label label26;
    private System.Windows.Forms.Label label27;
    private System.Windows.Forms.Label label28;
    private System.Windows.Forms.Label label29;
    private System.Windows.Forms.Button button13;
    private System.Windows.Forms.Button button14;
    private System.Windows.Forms.TabPage tabPage3;
    private System.Windows.Forms.GroupBox groupBox7;
    private System.Windows.Forms.SplitContainer splitContainer8;
    private System.Windows.Forms.SplitContainer splitContainer9;
    private System.Windows.Forms.Button button15;
    private System.Windows.Forms.SplitContainer splitContainer10;
    private System.Windows.Forms.Label label30;
    private System.Windows.Forms.Label label31;
    private System.Windows.Forms.Button button16;
    private System.Windows.Forms.Button button17;
    private System.Windows.Forms.TextBox textBox4;
    private System.Windows.Forms.Button button18;
    private System.Windows.Forms.Button button19;
    private System.Windows.Forms.Label label32;
    private System.Windows.Forms.Label label33;
    private System.Windows.Forms.Label label34;
    private System.Windows.Forms.Label label35;
    private System.Windows.Forms.Button button20;
    private System.Windows.Forms.Button button21;
    private System.Windows.Forms.TabPage tabPage4;
    private System.Windows.Forms.GroupBox groupBox8;
    private System.Windows.Forms.TextBox textBox5;
    private System.Windows.Forms.TextBox textBox6;
    private System.Windows.Forms.Button button22;
    private System.Windows.Forms.Button button23;
    private System.Windows.Forms.Label label36;
    private System.Windows.Forms.Label label37;
    private System.Windows.Forms.Label label38;
    private System.Windows.Forms.Label label39;
    private System.Windows.Forms.Button button24;
    private System.Windows.Forms.Button button25;
    private System.Windows.Forms.TabPage tabPage5;
    private System.Windows.Forms.Label label40;
    private System.Windows.Forms.Label label41;
    private System.Windows.Forms.Button button26;
    private System.Windows.Forms.TextBox textBox7;
    private System.Windows.Forms.Button button27;
    private System.Windows.Forms.TabPage tabPage6;
    private System.Windows.Forms.Label label42;
    private System.Windows.Forms.Button button28;
    private System.Windows.Forms.Label label48;
    private System.Windows.Forms.Button button30;
    private System.Windows.Forms.GroupBox groupBox10;
    private System.Windows.Forms.SplitContainer splitContainer11;
    private System.Windows.Forms.SplitContainer splitContainer12;
    private System.Windows.Forms.Button button31;
    private System.Windows.Forms.SplitContainer splitContainer13;
    private System.Windows.Forms.Button button32;
    private System.Windows.Forms.Button button33;
    private System.Windows.Forms.TextBox textBox8;
    private System.Windows.Forms.Button button34;
    private System.Windows.Forms.Button button35;
    private System.Windows.Forms.Label label49;
    private System.Windows.Forms.Label label50;
    private System.Windows.Forms.Label label51;
    private System.Windows.Forms.Label label52;
    private System.Windows.Forms.Button button36;
    private System.Windows.Forms.Button button37;
    private System.Windows.Forms.TabPage tabPage7;
    private System.Windows.Forms.Panel panel8;
    private System.Windows.Forms.Label label18;
    private System.Windows.Forms.Button button29;
    private System.Windows.Forms.GroupBox groupBox9;
    private System.Windows.Forms.SplitContainer splitContainer14;
    private System.Windows.Forms.SplitContainer splitContainer15;
    private System.Windows.Forms.Button button38;
    private System.Windows.Forms.SplitContainer splitContainer16;
    private System.Windows.Forms.Button button39;
    private System.Windows.Forms.Button button40;
    private System.Windows.Forms.TextBox textBox9;
    private System.Windows.Forms.Button button41;
    private System.Windows.Forms.Button button42;
    private System.Windows.Forms.Label label43;
    private System.Windows.Forms.Label label44;
    private System.Windows.Forms.Label label45;
    private System.Windows.Forms.Label label46;
    private System.Windows.Forms.Label label53;
    private System.Windows.Forms.Button button43;
    private System.Windows.Forms.Button btnITUCamera;
    private System.Windows.Forms.Button button44;
    private System.Windows.Forms.TabPage tbpITUPS3;
    private System.Windows.Forms.Panel panel9;
    private System.Windows.Forms.Label label55;
    private System.Windows.Forms.Button btnITUPS3Camera;
    private System.Windows.Forms.Label label56;
    private System.Windows.Forms.Button btnITUPS3Adjust;
    private System.Windows.Forms.GroupBox groupBox11;
    private System.Windows.Forms.SplitContainer spcITUPS3Controls;
    private System.Windows.Forms.SplitContainer spcITUPS3TrackStatus;
    private System.Windows.Forms.Button btnITUPS3ShowOnPresentationScreen;
    private System.Windows.Forms.SplitContainer spcITUPS3CalibPlot;
    private System.Windows.Forms.Button btnITUPS3Recalibrate;
    private System.Windows.Forms.Button btnITUPS3AcceptCalibration;
    private System.Windows.Forms.TextBox txbITUPS3SubjectName;
    private System.Windows.Forms.Button btnITUPS3Record;
    private System.Windows.Forms.Button btnITUPS3Calibrate;
    private System.Windows.Forms.TabPage tbpEyeTech;
    private System.Windows.Forms.TextBox textBox15;
    private System.Windows.Forms.TextBox txbEyeTechSubjectName;
    private System.Windows.Forms.Button btnEyeTechRecord;
    private System.Windows.Forms.Button btnEyeTechCalibrate;
    private System.Windows.Forms.Button btnEyeTechSubjectName;
    private System.Windows.Forms.Button btnEyeTechConnect;
    private System.Windows.Forms.Label label57;
    private System.Windows.Forms.Label label58;
    private System.Windows.Forms.Label label59;
    private System.Windows.Forms.Label label60;
    private System.Windows.Forms.Button btnITUPS3SubjectName;
    private System.Windows.Forms.Button btnITUPS3Connect;
    private System.Windows.Forms.TabPage tabPage8;
    private System.Windows.Forms.Panel panel10;
    private System.Windows.Forms.GroupBox groupBox12;
    private System.Windows.Forms.SplitContainer splitContainer17;
    private System.Windows.Forms.SplitContainer splitContainer18;
    private System.Windows.Forms.Button button45;
    private System.Windows.Forms.SplitContainer splitContainer19;
    private System.Windows.Forms.Button button46;
    private System.Windows.Forms.Button button47;
    private System.Windows.Forms.TextBox textBox10;
    private System.Windows.Forms.Button button48;
    private System.Windows.Forms.Button button49;
    private System.Windows.Forms.Label label61;
    private System.Windows.Forms.Label label62;
    private System.Windows.Forms.Label label63;
    private System.Windows.Forms.Label label64;
    private System.Windows.Forms.Button button50;
    private System.Windows.Forms.Button button51;
    private System.Windows.Forms.TabPage tabPage9;
    private System.Windows.Forms.GroupBox groupBox13;
    private System.Windows.Forms.SplitContainer splitContainer20;
    private System.Windows.Forms.SplitContainer splitContainer21;
    private System.Windows.Forms.Button button52;
    private System.Windows.Forms.SplitContainer splitContainer22;
    private System.Windows.Forms.Label label65;
    private System.Windows.Forms.Label label66;
    private System.Windows.Forms.Button button53;
    private System.Windows.Forms.Button button54;
    private System.Windows.Forms.TextBox textBox11;
    private System.Windows.Forms.Button button55;
    private System.Windows.Forms.Button button56;
    private System.Windows.Forms.Label label67;
    private System.Windows.Forms.Label label68;
    private System.Windows.Forms.Label label69;
    private System.Windows.Forms.Label label70;
    private System.Windows.Forms.Button button57;
    private System.Windows.Forms.Button button58;
    private System.Windows.Forms.TabPage tabPage10;
    private System.Windows.Forms.GroupBox groupBox14;
    private System.Windows.Forms.TextBox textBox12;
    private System.Windows.Forms.TextBox textBox13;
    private System.Windows.Forms.Button button59;
    private System.Windows.Forms.Button button60;
    private System.Windows.Forms.Label label71;
    private System.Windows.Forms.Label label72;
    private System.Windows.Forms.Label label73;
    private System.Windows.Forms.Label label74;
    private System.Windows.Forms.Button button61;
    private System.Windows.Forms.Button button62;
    private System.Windows.Forms.TabPage tbpAsl;
    private System.Windows.Forms.TextBox txbAslSubjectName;
    private System.Windows.Forms.Button btnAslRecord;
    private System.Windows.Forms.Button btnAslCalibrate;
    private System.Windows.Forms.Label label75;
    private System.Windows.Forms.Label recordLabel;
    private System.Windows.Forms.Label label76;
    private System.Windows.Forms.Label connectLabel;
    private System.Windows.Forms.Button btnAslSubjectName;
    private System.Windows.Forms.Button btnAslConnect;
    private System.Windows.Forms.TabPage tabPage11;
    private System.Windows.Forms.Label label77;
    private System.Windows.Forms.Label label78;
    private System.Windows.Forms.Button button63;
    private System.Windows.Forms.TextBox textBox14;
    private System.Windows.Forms.Button button64;
    private System.Windows.Forms.TabPage tabPage12;
    private System.Windows.Forms.Label label79;
    private System.Windows.Forms.Button button65;
    private System.Windows.Forms.ToolStripContainer toolStripContainer2;
    private System.Windows.Forms.ToolStrip toolStrip2;
    private OgamaControls.ToolStripTrackBar trbZoom;
    private System.Windows.Forms.TabControl tclEyetracker;
    private System.Windows.Forms.TabPage tbpGazetrackerIPClient;
    private System.Windows.Forms.Panel panel11;
    private System.Windows.Forms.Label label17;
    private System.Windows.Forms.Button btnGazetrackerIPLaunch;
    private System.Windows.Forms.GroupBox groupBox15;
    private System.Windows.Forms.TextBox txbGazetrackerIPStatus;
    private System.Windows.Forms.Label label54;
    private System.Windows.Forms.TextBox txbGazetrackerIPSubject;
    private System.Windows.Forms.Button btnGazetrackerIPRecord;
    private System.Windows.Forms.Label label80;
    private System.Windows.Forms.Label label82;
    private System.Windows.Forms.Button btnGazetrackerIPSubject;
    private System.Windows.Forms.Button btnGazetrackerIPConnect;
    private System.Windows.Forms.TabPage tbpGazetrackerDirectClient;
    private System.Windows.Forms.Panel panel3;
    private System.Windows.Forms.GroupBox groupBox4;
    private System.Windows.Forms.SplitContainer splitContainer23;
    private GTOgamaClient.Controls.EyeVideoControl eyeVideoControlGazetracker;
    private System.Windows.Forms.Button btnGazetrackerDirectClientShowOnPresentationScreen;
    private System.Windows.Forms.TextBox txbGazetrackerDirectClientSubject;
    private System.Windows.Forms.Button btnGazetrackerDirectClientRecord;
    private System.Windows.Forms.Button btnGazetrackerDirectClientCalibrate;
    private System.Windows.Forms.Label label19;
    private System.Windows.Forms.Label label20;
    private System.Windows.Forms.Label label47;
    private System.Windows.Forms.Label label83;
    private System.Windows.Forms.Label label81;
    private System.Windows.Forms.Button btnGazetrackerDirectClientSubject;
    private System.Windows.Forms.Button btnGazetrackerDirectClientAdjust;
    private System.Windows.Forms.Button btnGazetrackerDirectClientConnect;
    private System.Windows.Forms.TabPage tbpTobii;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.SplitContainer spcTobiiControls;
    private System.Windows.Forms.SplitContainer spcTobiiTrackStatus;
    private System.Windows.Forms.Button btnTobiiShowOnPresentationScreen;
    private System.Windows.Forms.SplitContainer spcTobiiCalibPlot;
    private System.Windows.Forms.Button btnTobiiRecalibrate;
    private System.Windows.Forms.Button btnTobiiAcceptCalibration;
    private System.Windows.Forms.TextBox txbTobiiSubjectName;
    private System.Windows.Forms.Button btnTobiiRecord;
    private System.Windows.Forms.Button btnTobiiCalibrate;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Button btnTobiiSubjectName;
    private System.Windows.Forms.Button btnTobiiConnect;
    private System.Windows.Forms.TabPage tbpAlea;
    private System.Windows.Forms.GroupBox groupBox2;
    private System.Windows.Forms.SplitContainer spcAleaControls;
    private System.Windows.Forms.SplitContainer spcAleaTrackStatus;
    private System.Windows.Forms.Button btnAleaShowOnPresentationScreen;
    private System.Windows.Forms.SplitContainer spcAleaCalibPlot;
    private System.Windows.Forms.Label labelCalibrationResult;
    private System.Windows.Forms.Label label11;
    private System.Windows.Forms.Button btnAleaRecalibrate;
    private System.Windows.Forms.Button btnAleaAcceptCalibration;
    private System.Windows.Forms.TextBox txbAleaSubjectName;
    private System.Windows.Forms.Button btnAleaRecord;
    private System.Windows.Forms.Button btnAleaCalibrate;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.Label label8;
    private System.Windows.Forms.Label label9;
    private System.Windows.Forms.Label label10;
    private System.Windows.Forms.Button btnAleaSubjectName;
    private System.Windows.Forms.Button btnAleaConnect;
    private System.Windows.Forms.TabPage tbpSMIiViewX;
    private System.Windows.Forms.GroupBox groupBox3;
    private System.Windows.Forms.TextBox textBox1;
    private System.Windows.Forms.TextBox txbSMIiViewXSubjectName;
    private System.Windows.Forms.Button btnSMIiViewXRecord;
    private System.Windows.Forms.Button btnSMIiViewXCalibrate;
    private System.Windows.Forms.Label label13;
    private System.Windows.Forms.Label label14;
    private System.Windows.Forms.Label label15;
    private System.Windows.Forms.Label label16;
    private System.Windows.Forms.Button btnSMIiViewXSubjectName;
    private System.Windows.Forms.Button btnSMIiViewXConnect;
    private System.Windows.Forms.TabPage tbpMouseOnly;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.Button btnMouseOnlySubject;
    private System.Windows.Forms.TextBox txbMouseOnlySubjectName;
    private System.Windows.Forms.Button btnMouseOnlyRecord;
    private System.Windows.Forms.TabPage tbpNoDevice;
    private System.Windows.Forms.Label label12;
    private System.Windows.Forms.Button btnNoDeviceTabSelectTracker;
    private System.Windows.Forms.TabPage tbpMirametrix;
    private System.Windows.Forms.Panel panel12;
    private System.Windows.Forms.GroupBox groupBox16;
    private System.Windows.Forms.GroupBox groupBox20;
    private System.Windows.Forms.SplitContainer spcMirametrixControls;
    private System.Windows.Forms.SplitContainer spcMirametrixTrackStatus;
    private System.Windows.Forms.Button btnMirametrixShowOnPresentationScreen;
    private System.Windows.Forms.SplitContainer spcMirametrixCalibPlot;
    private System.Windows.Forms.Button btnMirametrixRecalibrate;
    private System.Windows.Forms.Button btnMirametrixAcceptCalibration;
    private System.Windows.Forms.TextBox txbMirametrixSubjectName;
    private System.Windows.Forms.Button btnMirametrixRecord;
    private System.Windows.Forms.Button btnMirametrixCalibrate;
    private System.Windows.Forms.Label label84;
    private System.Windows.Forms.Label label85;
    private System.Windows.Forms.Label label86;
    private System.Windows.Forms.Label label87;
    private System.Windows.Forms.Button btnMirametrixSubjectName;
    private System.Windows.Forms.Button btnMirametrixConnect;
    private System.Windows.Forms.Label labelCalibrationResultMirametrix;
    private System.Windows.Forms.TextBox tBWarning;
    private Label label88;
    private Label label89;
    private Label label90;
    private Label label91;
    private TabPage tbpGazepoint;
    private Label label93;
    private Label label94;
    private Label label95;
    private Label label96;
    private TextBox txbGazepointSubjectName;
    private Button btnGazepointRecord;
    private Button btnGazepointCalibrate;
    private Button btnGazepointSubjectName;
    private Button btnGazepointConnect;
    private GroupBox groupBox17;
    private SplitContainer spcGazepointControls;
    private SplitContainer spcGazepointTrackStatus;
    private TextBox tBGWarning;
    private Button btnGazepointShowOnPresentationScreen;
    private SplitContainer spcGazepointCalibPlot;
    private Label labelCalibrationResultGazepoint;
    private Button btnGazepointRecalibrate;
    private Button btnGazepointAcceptCalibration;
    private TabPage tbpSMIRedM;
    private GroupBox groupBox18;
    private TextBox textBox16;
    private TextBox txbSMIRedMSubject;
    private Button btnSMIRedMRecord;
    private Button btnSMIRedMCalibrate;
    private Label label92;
    private Label label97;
    private Label label98;
    private Label label99;
    private Button btnSMIRedMSubject;
    private Button btnSMIRedMConnect;
  }
}