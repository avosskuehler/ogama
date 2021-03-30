namespace Ogama.Modules.SlideshowDesign.DesignModule
{
  using System.Windows.Forms;

  using Ogama.Modules.Common.Controls;

  partial class SlideDesignModule
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SlideDesignModule));
      System.ComponentModel.StringConverter stringConverter1 = new System.ComponentModel.StringConverter();
      this.ofdBackgroundImage = new System.Windows.Forms.OpenFileDialog();
      this.imlCommands = new System.Windows.Forms.ImageList(this.components);
      this.toolTip = new System.Windows.Forms.ToolTip(this.components);
      this.btnAddSound = new System.Windows.Forms.Button();
      this.btnAddShape = new System.Windows.Forms.Button();
      this.btnAddImage = new System.Windows.Forms.Button();
      this.btnAddRtfInstruction = new System.Windows.Forms.Button();
      this.btnAddInstruction = new System.Windows.Forms.Button();
      this.pcbHelpNaming = new System.Windows.Forms.PictureBox();
      this.cbbCategory = new System.Windows.Forms.ComboBox();
      this.txbName = new System.Windows.Forms.TextBox();
      this.pcbHelpTiming = new System.Windows.Forms.PictureBox();
      this.pcbHelpTesting = new System.Windows.Forms.PictureBox();
      this.pchHelpTargets = new System.Windows.Forms.PictureBox();
      this.pcbLinksHelp = new System.Windows.Forms.PictureBox();
      this.pcbHelpTrigger = new System.Windows.Forms.PictureBox();
      this.chbIsDisabled = new System.Windows.Forms.CheckBox();
      this.panel1 = new System.Windows.Forms.Panel();
      this.btnHelp = new System.Windows.Forms.Button();
      this.btnOK = new System.Windows.Forms.Button();
      this.btnCancel = new System.Windows.Forms.Button();
      this.spcStimuliStandard = new System.Windows.Forms.SplitContainer();
      this.splitContainer3 = new System.Windows.Forms.SplitContainer();
      this.tctStimuli = new System.Windows.Forms.TabControl();
      this.tbpNewStimuli = new System.Windows.Forms.TabPage();
      this.label18 = new System.Windows.Forms.Label();
      this.tbpInstructions = new System.Windows.Forms.TabPage();
      this.txbInstructions = new System.Windows.Forms.TextBox();
      this.tbpRtfInstructions = new System.Windows.Forms.TabPage();
      this.rtbInstructions = new OgamaControls.RTBTextControl();
      this.tbpImages = new System.Windows.Forms.TabPage();
      this.btnOpenImageFile = new System.Windows.Forms.Button();
      this.label8 = new System.Windows.Forms.Label();
      this.label12 = new System.Windows.Forms.Label();
      this.txbImageFilename = new System.Windows.Forms.TextBox();
      this.cbbImageLayout = new System.Windows.Forms.ComboBox();
      this.tbpSounds = new System.Windows.Forms.TabPage();
      this.splitContainer4 = new System.Windows.Forms.SplitContainer();
      this.tacProperties = new System.Windows.Forms.TabControl();
      this.tbpLayout = new System.Windows.Forms.TabPage();
      this.grpDocking = new System.Windows.Forms.GroupBox();
      this.gveLayoutDockStyle = new OgamaControls.GenericValueEditor();
      this.btnLayoutCenterScreen = new System.Windows.Forms.Button();
      this.grpLayoutPosition = new System.Windows.Forms.GroupBox();
      this.label26 = new System.Windows.Forms.Label();
      this.nudLayoutHeight = new System.Windows.Forms.NumericUpDown();
      this.label25 = new System.Windows.Forms.Label();
      this.nudLayoutWidth = new System.Windows.Forms.NumericUpDown();
      this.label24 = new System.Windows.Forms.Label();
      this.label23 = new System.Windows.Forms.Label();
      this.nudLayoutTop = new System.Windows.Forms.NumericUpDown();
      this.nudLayoutLeft = new System.Windows.Forms.NumericUpDown();
      this.tbpCommonProperties = new System.Windows.Forms.TabPage();
      this.pbcElements = new OgamaControls.PenAndBrushControl();
      this.tbpAudioProperties = new System.Windows.Forms.TabPage();
      this.audioControl = new OgamaControls.AudioControl();
      this.tctStandards = new System.Windows.Forms.TabControl();
      this.tbpName = new System.Windows.Forms.TabPage();
      this.label11 = new System.Windows.Forms.Label();
      this.label10 = new System.Windows.Forms.Label();
      this.label5 = new System.Windows.Forms.Label();
      this.tbpTiming = new System.Windows.Forms.TabPage();
      this.chbOnlyWhenInTarget = new System.Windows.Forms.CheckBox();
      this.btnRemoveCondition = new System.Windows.Forms.Button();
      this.btnAddCondition = new System.Windows.Forms.Button();
      this.lsbStopConditions = new System.Windows.Forms.ListBox();
      this.cbbKeys = new System.Windows.Forms.ComboBox();
      this.cbbMouseButtons = new System.Windows.Forms.ComboBox();
      this.label3 = new System.Windows.Forms.Label();
      this.nudTime = new System.Windows.Forms.NumericUpDown();
      this.rdbTime = new System.Windows.Forms.RadioButton();
      this.rdbMouse = new System.Windows.Forms.RadioButton();
      this.rdbKey = new System.Windows.Forms.RadioButton();
      this.tbpTesting = new System.Windows.Forms.TabPage();
      this.label17 = new System.Windows.Forms.Label();
      this.cbbTestingTargets = new System.Windows.Forms.ComboBox();
      this.btnRemoveCorrectResponse = new System.Windows.Forms.Button();
      this.btnAddCorrectResponse = new System.Windows.Forms.Button();
      this.lsbCorrectResponses = new System.Windows.Forms.ListBox();
      this.label7 = new System.Windows.Forms.Label();
      this.cbbTestingKeys = new System.Windows.Forms.ComboBox();
      this.cbbTestingMouseButtons = new System.Windows.Forms.ComboBox();
      this.rdbTestingMouse = new System.Windows.Forms.RadioButton();
      this.rdbTestingKey = new System.Windows.Forms.RadioButton();
      this.tbpTargets = new System.Windows.Forms.TabPage();
      this.lsbTargets = new System.Windows.Forms.ListBox();
      this.btnRemoveTarget = new System.Windows.Forms.Button();
      this.btnAddTargetRectangle = new System.Windows.Forms.Button();
      this.btnAddTargetEllipse = new System.Windows.Forms.Button();
      this.btnAddTargetPolyline = new System.Windows.Forms.Button();
      this.label16 = new System.Windows.Forms.Label();
      this.tbpLinks = new System.Windows.Forms.TabPage();
      this.label20 = new System.Windows.Forms.Label();
      this.label9 = new System.Windows.Forms.Label();
      this.cbbLinksTrial = new System.Windows.Forms.ComboBox();
      this.cbbLinksTargets = new System.Windows.Forms.ComboBox();
      this.btnRemoveLink = new System.Windows.Forms.Button();
      this.btnAddLink = new System.Windows.Forms.Button();
      this.lsbLinks = new System.Windows.Forms.ListBox();
      this.label19 = new System.Windows.Forms.Label();
      this.cbbLinksKeys = new System.Windows.Forms.ComboBox();
      this.cbbLinksMouseButtons = new System.Windows.Forms.ComboBox();
      this.rdbLinksMouse = new System.Windows.Forms.RadioButton();
      this.rdbLinksKey = new System.Windows.Forms.RadioButton();
      this.tbpBackground = new System.Windows.Forms.TabPage();
      this.tabControl1 = new System.Windows.Forms.TabControl();
      this.tabPage1 = new System.Windows.Forms.TabPage();
      this.btnBackgroundColor = new OgamaControls.ColorButton(this.components);
      this.label15 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.tabPage2 = new System.Windows.Forms.TabPage();
      this.label14 = new System.Windows.Forms.Label();
      this.label6 = new System.Windows.Forms.Label();
      this.btnDeleteBackgroundImage = new System.Windows.Forms.Button();
      this.btnBackgroundImage = new System.Windows.Forms.Button();
      this.tabPage3 = new System.Windows.Forms.TabPage();
      this.bkgAudioControl = new OgamaControls.AudioControl();
      this.label13 = new System.Windows.Forms.Label();
      this.pictureBox2 = new System.Windows.Forms.PictureBox();
      this.label4 = new System.Windows.Forms.Label();
      this.tbpMouse = new System.Windows.Forms.TabPage();
      this.lblMouseDescription = new System.Windows.Forms.Label();
      this.pictureBox1 = new System.Windows.Forms.PictureBox();
      this.grbInitialPosition = new System.Windows.Forms.GroupBox();
      this.chbMouseDontChangePosition = new System.Windows.Forms.CheckBox();
      this.psbMouseCursor = new OgamaControls.PositionButton(this.components);
      this.groupBox6 = new System.Windows.Forms.GroupBox();
      this.rdbShowMouseCursor = new System.Windows.Forms.RadioButton();
      this.rdbHideMouseCursor = new System.Windows.Forms.RadioButton();
      this.tbpTrigger = new System.Windows.Forms.TabPage();
      this.label22 = new System.Windows.Forms.Label();
      this.label21 = new System.Windows.Forms.Label();
      this.triggerControl = new OgamaControls.TriggerControl();
      this.tabPage4 = new System.Windows.Forms.TabPage();
      this.label28 = new System.Windows.Forms.Label();
      this.cbbPreSlideFixationTrial = new System.Windows.Forms.ComboBox();
      this.label27 = new System.Windows.Forms.Label();
      this.pnlCanvas = new System.Windows.Forms.Panel();
      this.pnlPicture = new System.Windows.Forms.Panel();
      this.designPicture = new Ogama.Modules.SlideshowDesign.DesignModule.SlidePicture(this.components);
      this.dltForm = new Ogama.Modules.Common.Controls.DialogTop();
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      this.splitContainer2 = new System.Windows.Forms.SplitContainer();
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
      ((System.ComponentModel.ISupportInitialize)(this.pcbHelpNaming)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pcbHelpTiming)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pcbHelpTesting)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pchHelpTargets)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pcbLinksHelp)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pcbHelpTrigger)).BeginInit();
      this.panel1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.spcStimuliStandard)).BeginInit();
      this.spcStimuliStandard.Panel1.SuspendLayout();
      this.spcStimuliStandard.Panel2.SuspendLayout();
      this.spcStimuliStandard.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
      this.splitContainer3.Panel1.SuspendLayout();
      this.splitContainer3.Panel2.SuspendLayout();
      this.splitContainer3.SuspendLayout();
      this.tctStimuli.SuspendLayout();
      this.tbpNewStimuli.SuspendLayout();
      this.tbpInstructions.SuspendLayout();
      this.tbpRtfInstructions.SuspendLayout();
      this.tbpImages.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).BeginInit();
      this.splitContainer4.Panel1.SuspendLayout();
      this.splitContainer4.Panel2.SuspendLayout();
      this.splitContainer4.SuspendLayout();
      this.tacProperties.SuspendLayout();
      this.tbpLayout.SuspendLayout();
      this.grpDocking.SuspendLayout();
      this.grpLayoutPosition.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.nudLayoutHeight)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.nudLayoutWidth)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.nudLayoutTop)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.nudLayoutLeft)).BeginInit();
      this.tbpCommonProperties.SuspendLayout();
      this.tbpAudioProperties.SuspendLayout();
      this.tctStandards.SuspendLayout();
      this.tbpName.SuspendLayout();
      this.tbpTiming.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.nudTime)).BeginInit();
      this.tbpTesting.SuspendLayout();
      this.tbpTargets.SuspendLayout();
      this.tbpLinks.SuspendLayout();
      this.tbpBackground.SuspendLayout();
      this.tabControl1.SuspendLayout();
      this.tabPage1.SuspendLayout();
      this.tabPage2.SuspendLayout();
      this.tabPage3.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
      this.tbpMouse.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
      this.grbInitialPosition.SuspendLayout();
      this.groupBox6.SuspendLayout();
      this.tbpTrigger.SuspendLayout();
      this.tabPage4.SuspendLayout();
      this.pnlCanvas.SuspendLayout();
      this.pnlPicture.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
      this.splitContainer2.Panel1.SuspendLayout();
      this.splitContainer2.Panel2.SuspendLayout();
      this.splitContainer2.SuspendLayout();
      this.SuspendLayout();
      // 
      // ofdBackgroundImage
      // 
      this.ofdBackgroundImage.Filter = "\"Images (*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF|All files (*.*)|*.*\"";
      this.ofdBackgroundImage.Title = "Select background image ...";
      // 
      // imlCommands
      // 
      this.imlCommands.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imlCommands.ImageStream")));
      this.imlCommands.TransparentColor = System.Drawing.Color.Transparent;
      this.imlCommands.Images.SetKeyName(0, "Time");
      this.imlCommands.Images.SetKeyName(1, "Picture");
      this.imlCommands.Images.SetKeyName(2, "Properties");
      this.imlCommands.Images.SetKeyName(3, "Delete");
      this.imlCommands.Images.SetKeyName(4, "Testing");
      this.imlCommands.Images.SetKeyName(5, "Mouse");
      this.imlCommands.Images.SetKeyName(6, "Instructions");
      this.imlCommands.Images.SetKeyName(7, "Images");
      this.imlCommands.Images.SetKeyName(8, "Shapes");
      this.imlCommands.Images.SetKeyName(9, "Flash");
      this.imlCommands.Images.SetKeyName(10, "Sound");
      this.imlCommands.Images.SetKeyName(11, "Naming");
      this.imlCommands.Images.SetKeyName(12, "Link");
      this.imlCommands.Images.SetKeyName(13, "Rtf");
      this.imlCommands.Images.SetKeyName(14, "Trigger");
      this.imlCommands.Images.SetKeyName(15, "Browser");
      // 
      // toolTip
      // 
      this.toolTip.ShowAlways = true;
      // 
      // btnAddSound
      // 
      this.btnAddSound.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.btnAddSound.ImageKey = "Sound";
      this.btnAddSound.ImageList = this.imlCommands;
      this.btnAddSound.Location = new System.Drawing.Point(266, 56);
      this.btnAddSound.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
      this.btnAddSound.Name = "btnAddSound";
      this.btnAddSound.Size = new System.Drawing.Size(50, 48);
      this.btnAddSound.TabIndex = 16;
      this.btnAddSound.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.btnAddSound.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.toolTip.SetToolTip(this.btnAddSound, "Add sound ...");
      this.btnAddSound.UseVisualStyleBackColor = true;
      this.btnAddSound.Click += new System.EventHandler(this.btnAddSound_Click);
      // 
      // btnAddShape
      // 
      this.btnAddShape.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.btnAddShape.ImageKey = "Shapes";
      this.btnAddShape.ImageList = this.imlCommands;
      this.btnAddShape.Location = new System.Drawing.Point(142, 56);
      this.btnAddShape.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
      this.btnAddShape.Name = "btnAddShape";
      this.btnAddShape.Size = new System.Drawing.Size(50, 48);
      this.btnAddShape.TabIndex = 16;
      this.btnAddShape.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.btnAddShape.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.toolTip.SetToolTip(this.btnAddShape, "Add shape ...");
      this.btnAddShape.UseVisualStyleBackColor = true;
      this.btnAddShape.Click += new System.EventHandler(this.btnAddShape_Click);
      // 
      // btnAddImage
      // 
      this.btnAddImage.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.btnAddImage.ImageKey = "Images";
      this.btnAddImage.ImageList = this.imlCommands;
      this.btnAddImage.Location = new System.Drawing.Point(204, 56);
      this.btnAddImage.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
      this.btnAddImage.Name = "btnAddImage";
      this.btnAddImage.Size = new System.Drawing.Size(50, 48);
      this.btnAddImage.TabIndex = 16;
      this.btnAddImage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.btnAddImage.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.toolTip.SetToolTip(this.btnAddImage, "Add image ...");
      this.btnAddImage.UseVisualStyleBackColor = true;
      this.btnAddImage.Click += new System.EventHandler(this.btnAddImage_Click);
      // 
      // btnAddRtfInstruction
      // 
      this.btnAddRtfInstruction.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.btnAddRtfInstruction.ImageKey = "Rtf";
      this.btnAddRtfInstruction.ImageList = this.imlCommands;
      this.btnAddRtfInstruction.Location = new System.Drawing.Point(80, 56);
      this.btnAddRtfInstruction.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
      this.btnAddRtfInstruction.Name = "btnAddRtfInstruction";
      this.btnAddRtfInstruction.Size = new System.Drawing.Size(50, 48);
      this.btnAddRtfInstruction.TabIndex = 4;
      this.btnAddRtfInstruction.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.btnAddRtfInstruction.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.toolTip.SetToolTip(this.btnAddRtfInstruction, "Add Rich Text instruction.");
      this.btnAddRtfInstruction.UseVisualStyleBackColor = true;
      this.btnAddRtfInstruction.Click += new System.EventHandler(this.btnAddRtfInstruction_Click);
      // 
      // btnAddInstruction
      // 
      this.btnAddInstruction.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.btnAddInstruction.ImageKey = "Instructions";
      this.btnAddInstruction.ImageList = this.imlCommands;
      this.btnAddInstruction.Location = new System.Drawing.Point(18, 56);
      this.btnAddInstruction.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
      this.btnAddInstruction.Name = "btnAddInstruction";
      this.btnAddInstruction.Size = new System.Drawing.Size(50, 48);
      this.btnAddInstruction.TabIndex = 4;
      this.btnAddInstruction.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.btnAddInstruction.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.toolTip.SetToolTip(this.btnAddInstruction, "Add instruction");
      this.btnAddInstruction.UseVisualStyleBackColor = true;
      this.btnAddInstruction.Click += new System.EventHandler(this.btnAddInstruction_Click);
      // 
      // pcbHelpNaming
      // 
      this.pcbHelpNaming.Cursor = System.Windows.Forms.Cursors.Help;
      this.pcbHelpNaming.Image = global::Ogama.Properties.Resources.HelpBmp;
      this.pcbHelpNaming.Location = new System.Drawing.Point(564, 10);
      this.pcbHelpNaming.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
      this.pcbHelpNaming.Name = "pcbHelpNaming";
      this.pcbHelpNaming.Size = new System.Drawing.Size(16, 16);
      this.pcbHelpNaming.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
      this.pcbHelpNaming.TabIndex = 14;
      this.pcbHelpNaming.TabStop = false;
      this.toolTip.SetToolTip(this.pcbHelpNaming, "Show context help.");
      this.pcbHelpNaming.Click += new System.EventHandler(this.pcbHelpNaming_Click);
      // 
      // cbbCategory
      // 
      this.cbbCategory.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
      this.cbbCategory.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
      this.cbbCategory.FormattingEnabled = true;
      this.cbbCategory.Items.AddRange(new object[] {
            "",
            "BlankSlide",
            "Instruction",
            "FlashStimulus",
            "MultipleChoiceQuestion"});
      this.cbbCategory.Location = new System.Drawing.Point(230, 119);
      this.cbbCategory.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
      this.cbbCategory.Name = "cbbCategory";
      this.cbbCategory.Size = new System.Drawing.Size(258, 33);
      this.cbbCategory.TabIndex = 9;
      this.toolTip.SetToolTip(this.cbbCategory, "This is an optional property.\r\nSpecify additional categories by typing into the c" +
        "ombo box.");
      // 
      // txbName
      // 
      this.txbName.Location = new System.Drawing.Point(230, 63);
      this.txbName.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
      this.txbName.Name = "txbName";
      this.txbName.Size = new System.Drawing.Size(258, 31);
      this.txbName.TabIndex = 7;
      this.txbName.Text = "InstructionSlide";
      this.toolTip.SetToolTip(this.txbName, "This slidename should be unique");
      // 
      // pcbHelpTiming
      // 
      this.pcbHelpTiming.Cursor = System.Windows.Forms.Cursors.Help;
      this.pcbHelpTiming.Image = global::Ogama.Properties.Resources.HelpBmp;
      this.pcbHelpTiming.Location = new System.Drawing.Point(564, 10);
      this.pcbHelpTiming.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
      this.pcbHelpTiming.Name = "pcbHelpTiming";
      this.pcbHelpTiming.Size = new System.Drawing.Size(16, 16);
      this.pcbHelpTiming.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
      this.pcbHelpTiming.TabIndex = 7;
      this.pcbHelpTiming.TabStop = false;
      this.toolTip.SetToolTip(this.pcbHelpTiming, "Show context help.");
      this.pcbHelpTiming.Click += new System.EventHandler(this.pcbHelpTiming_Click);
      // 
      // pcbHelpTesting
      // 
      this.pcbHelpTesting.Cursor = System.Windows.Forms.Cursors.Help;
      this.pcbHelpTesting.Image = global::Ogama.Properties.Resources.HelpBmp;
      this.pcbHelpTesting.Location = new System.Drawing.Point(564, 10);
      this.pcbHelpTesting.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
      this.pcbHelpTesting.Name = "pcbHelpTesting";
      this.pcbHelpTesting.Size = new System.Drawing.Size(16, 16);
      this.pcbHelpTesting.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
      this.pcbHelpTesting.TabIndex = 14;
      this.pcbHelpTesting.TabStop = false;
      this.toolTip.SetToolTip(this.pcbHelpTesting, "Show context help.");
      this.pcbHelpTesting.Click += new System.EventHandler(this.pcbHelpTesting_Click);
      // 
      // pchHelpTargets
      // 
      this.pchHelpTargets.Cursor = System.Windows.Forms.Cursors.Help;
      this.pchHelpTargets.Image = global::Ogama.Properties.Resources.HelpBmp;
      this.pchHelpTargets.Location = new System.Drawing.Point(564, 10);
      this.pchHelpTargets.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
      this.pchHelpTargets.Name = "pchHelpTargets";
      this.pchHelpTargets.Size = new System.Drawing.Size(16, 16);
      this.pchHelpTargets.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
      this.pchHelpTargets.TabIndex = 13;
      this.pchHelpTargets.TabStop = false;
      this.toolTip.SetToolTip(this.pchHelpTargets, "Show context help.");
      this.pchHelpTargets.Click += new System.EventHandler(this.pchHelpTargets_Click);
      // 
      // pcbLinksHelp
      // 
      this.pcbLinksHelp.Cursor = System.Windows.Forms.Cursors.Help;
      this.pcbLinksHelp.Image = global::Ogama.Properties.Resources.HelpBmp;
      this.pcbLinksHelp.Location = new System.Drawing.Point(564, 10);
      this.pcbLinksHelp.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
      this.pcbLinksHelp.Name = "pcbLinksHelp";
      this.pcbLinksHelp.Size = new System.Drawing.Size(16, 16);
      this.pcbLinksHelp.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
      this.pcbLinksHelp.TabIndex = 30;
      this.pcbLinksHelp.TabStop = false;
      this.toolTip.SetToolTip(this.pcbLinksHelp, "Show context help.");
      this.pcbLinksHelp.Click += new System.EventHandler(this.pcbLinksHelp_Click);
      // 
      // pcbHelpTrigger
      // 
      this.pcbHelpTrigger.Cursor = System.Windows.Forms.Cursors.Help;
      this.pcbHelpTrigger.Image = global::Ogama.Properties.Resources.HelpBmp;
      this.pcbHelpTrigger.Location = new System.Drawing.Point(562, 10);
      this.pcbHelpTrigger.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
      this.pcbHelpTrigger.Name = "pcbHelpTrigger";
      this.pcbHelpTrigger.Size = new System.Drawing.Size(16, 16);
      this.pcbHelpTrigger.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
      this.pcbHelpTrigger.TabIndex = 27;
      this.pcbHelpTrigger.TabStop = false;
      this.toolTip.SetToolTip(this.pcbHelpTrigger, "Show context help.");
      this.pcbHelpTrigger.Click += new System.EventHandler(this.pcbHelpTrigger_Click);
      // 
      // chbIsDisabled
      // 
      this.chbIsDisabled.AutoSize = true;
      this.chbIsDisabled.Location = new System.Drawing.Point(230, 225);
      this.chbIsDisabled.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
      this.chbIsDisabled.Name = "chbIsDisabled";
      this.chbIsDisabled.Size = new System.Drawing.Size(182, 29);
      this.chbIsDisabled.TabIndex = 27;
      this.chbIsDisabled.Text = "Disabled Slide";
      this.toolTip.SetToolTip(this.chbIsDisabled, "Check this, if you don´t want to show this slide in the slideshow.\r\nIf you will u" +
        "se this slide as a pre trial slide, check this to\r\neliminate this slide from ana" +
        "lysis.");
      this.chbIsDisabled.UseVisualStyleBackColor = true;
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.btnHelp);
      this.panel1.Controls.Add(this.btnOK);
      this.panel1.Controls.Add(this.btnCancel);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel1.Location = new System.Drawing.Point(0, 0);
      this.panel1.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(1840, 35);
      this.panel1.TabIndex = 21;
      // 
      // btnHelp
      // 
      this.btnHelp.Image = global::Ogama.Properties.Resources.HelpBmp;
      this.btnHelp.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.btnHelp.Location = new System.Drawing.Point(10, 6);
      this.btnHelp.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
      this.btnHelp.Name = "btnHelp";
      this.btnHelp.Size = new System.Drawing.Size(50, 48);
      this.btnHelp.TabIndex = 24;
      this.btnHelp.UseVisualStyleBackColor = true;
      this.btnHelp.Click += new System.EventHandler(this.btnHelpModule_Click);
      // 
      // btnOK
      // 
      this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.btnOK.Location = new System.Drawing.Point(1490, -26);
      this.btnOK.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
      this.btnOK.Name = "btnOK";
      this.btnOK.Size = new System.Drawing.Size(160, 48);
      this.btnOK.TabIndex = 13;
      this.btnOK.Text = "OK";
      this.btnOK.UseVisualStyleBackColor = true;
      // 
      // btnCancel
      // 
      this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnCancel.Location = new System.Drawing.Point(1662, -26);
      this.btnCancel.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(160, 48);
      this.btnCancel.TabIndex = 13;
      this.btnCancel.Text = "Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      // 
      // spcStimuliStandard
      // 
      this.spcStimuliStandard.Dock = System.Windows.Forms.DockStyle.Fill;
      this.spcStimuliStandard.IsSplitterFixed = true;
      this.spcStimuliStandard.Location = new System.Drawing.Point(0, 0);
      this.spcStimuliStandard.Margin = new System.Windows.Forms.Padding(0);
      this.spcStimuliStandard.Name = "spcStimuliStandard";
      // 
      // spcStimuliStandard.Panel1
      // 
      this.spcStimuliStandard.Panel1.Controls.Add(this.splitContainer3);
      this.spcStimuliStandard.Panel1MinSize = 280;
      // 
      // spcStimuliStandard.Panel2
      // 
      this.spcStimuliStandard.Panel2.Controls.Add(this.pnlCanvas);
      this.spcStimuliStandard.Size = new System.Drawing.Size(1840, 1020);
      this.spcStimuliStandard.SplitterDistance = 560;
      this.spcStimuliStandard.SplitterWidth = 8;
      this.spcStimuliStandard.TabIndex = 18;
      // 
      // splitContainer3
      // 
      this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
      this.splitContainer3.IsSplitterFixed = true;
      this.splitContainer3.Location = new System.Drawing.Point(0, 0);
      this.splitContainer3.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
      this.splitContainer3.Name = "splitContainer3";
      this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // splitContainer3.Panel1
      // 
      this.splitContainer3.Panel1.Controls.Add(this.tctStimuli);
      // 
      // splitContainer3.Panel2
      // 
      this.splitContainer3.Panel2.Controls.Add(this.splitContainer4);
      this.splitContainer3.Size = new System.Drawing.Size(560, 1020);
      this.splitContainer3.SplitterDistance = 120;
      this.splitContainer3.SplitterWidth = 2;
      this.splitContainer3.TabIndex = 27;
      // 
      // tctStimuli
      // 
      this.tctStimuli.Controls.Add(this.tbpNewStimuli);
      this.tctStimuli.Controls.Add(this.tbpInstructions);
      this.tctStimuli.Controls.Add(this.tbpRtfInstructions);
      this.tctStimuli.Controls.Add(this.tbpImages);
      this.tctStimuli.Controls.Add(this.tbpSounds);
      this.tctStimuli.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tctStimuli.ImageList = this.imlCommands;
      this.tctStimuli.Location = new System.Drawing.Point(0, 0);
      this.tctStimuli.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
      this.tctStimuli.Name = "tctStimuli";
      this.tctStimuli.SelectedIndex = 0;
      this.tctStimuli.Size = new System.Drawing.Size(560, 120);
      this.tctStimuli.TabIndex = 20;
      // 
      // tbpNewStimuli
      // 
      this.tbpNewStimuli.Controls.Add(this.label18);
      this.tbpNewStimuli.Controls.Add(this.btnAddSound);
      this.tbpNewStimuli.Controls.Add(this.btnAddShape);
      this.tbpNewStimuli.Controls.Add(this.btnAddImage);
      this.tbpNewStimuli.Controls.Add(this.btnAddRtfInstruction);
      this.tbpNewStimuli.Controls.Add(this.btnAddInstruction);
      this.tbpNewStimuli.Location = new System.Drawing.Point(8, 39);
      this.tbpNewStimuli.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
      this.tbpNewStimuli.Name = "tbpNewStimuli";
      this.tbpNewStimuli.Size = new System.Drawing.Size(544, 73);
      this.tbpNewStimuli.TabIndex = 4;
      this.tbpNewStimuli.Text = "New Items";
      this.tbpNewStimuli.UseVisualStyleBackColor = true;
      // 
      // label18
      // 
      this.label18.AutoSize = true;
      this.label18.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label18.Location = new System.Drawing.Point(12, 12);
      this.label18.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
      this.label18.Name = "label18";
      this.label18.Size = new System.Drawing.Size(334, 36);
      this.label18.TabIndex = 26;
      this.label18.Text = "Create new slide items";
      // 
      // tbpInstructions
      // 
      this.tbpInstructions.Controls.Add(this.txbInstructions);
      this.tbpInstructions.ImageKey = "Instructions";
      this.tbpInstructions.Location = new System.Drawing.Point(8, 39);
      this.tbpInstructions.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
      this.tbpInstructions.Name = "tbpInstructions";
      this.tbpInstructions.Size = new System.Drawing.Size(544, 184);
      this.tbpInstructions.TabIndex = 0;
      this.tbpInstructions.Text = "Instructions";
      this.tbpInstructions.UseVisualStyleBackColor = true;
      // 
      // txbInstructions
      // 
      this.txbInstructions.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txbInstructions.Location = new System.Drawing.Point(0, 0);
      this.txbInstructions.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
      this.txbInstructions.Multiline = true;
      this.txbInstructions.Name = "txbInstructions";
      this.txbInstructions.Size = new System.Drawing.Size(544, 184);
      this.txbInstructions.TabIndex = 0;
      this.txbInstructions.TextChanged += new System.EventHandler(this.txbInstructions_TextChanged);
      // 
      // tbpRtfInstructions
      // 
      this.tbpRtfInstructions.Controls.Add(this.rtbInstructions);
      this.tbpRtfInstructions.ImageKey = "Rtf";
      this.tbpRtfInstructions.Location = new System.Drawing.Point(8, 39);
      this.tbpRtfInstructions.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
      this.tbpRtfInstructions.Name = "tbpRtfInstructions";
      this.tbpRtfInstructions.Size = new System.Drawing.Size(544, 184);
      this.tbpRtfInstructions.TabIndex = 6;
      this.tbpRtfInstructions.Text = "RTF Instructions";
      this.tbpRtfInstructions.UseVisualStyleBackColor = true;
      // 
      // rtbInstructions
      // 
      this.rtbInstructions.AcceptsTab = false;
      this.rtbInstructions.AutoWordSelection = true;
      this.rtbInstructions.BackColor = System.Drawing.SystemColors.Window;
      this.rtbInstructions.DetectURLs = true;
      this.rtbInstructions.Dock = System.Windows.Forms.DockStyle.Fill;
      this.rtbInstructions.Label = "Label";
      this.rtbInstructions.Location = new System.Drawing.Point(0, 0);
      this.rtbInstructions.Margin = new System.Windows.Forms.Padding(12, 12, 12, 12);
      this.rtbInstructions.Name = "rtbInstructions";
      this.rtbInstructions.ReadOnly = false;
      // 
      // 
      // 
      this.rtbInstructions.RichTextBox.AutoWordSelection = true;
      this.rtbInstructions.RichTextBox.BackColor = System.Drawing.SystemColors.Window;
      this.rtbInstructions.RichTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
      this.rtbInstructions.RichTextBox.EnableAutoDragDrop = true;
      this.rtbInstructions.RichTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.rtbInstructions.RichTextBox.Location = new System.Drawing.Point(0, 0);
      this.rtbInstructions.RichTextBox.Name = "rtb1";
      this.rtbInstructions.RichTextBox.Size = new System.Drawing.Size(544, 184);
      this.rtbInstructions.RichTextBox.TabIndex = 1;
      this.rtbInstructions.RichTextBox.ZoomFactor = 0.5F;
      this.rtbInstructions.ShowBold = false;
      this.rtbInstructions.ShowCenterJustify = false;
      this.rtbInstructions.ShowCopy = false;
      this.rtbInstructions.ShowCut = false;
      this.rtbInstructions.ShowFont = false;
      this.rtbInstructions.ShowFontSize = false;
      this.rtbInstructions.ShowItalic = false;
      this.rtbInstructions.ShowLabel = false;
      this.rtbInstructions.ShowLeftJustify = false;
      this.rtbInstructions.ShowPaste = false;
      this.rtbInstructions.ShowRedo = false;
      this.rtbInstructions.ShowRightJustify = false;
      this.rtbInstructions.ShowToolBarText = false;
      this.rtbInstructions.ShowUnderline = false;
      this.rtbInstructions.ShowUndo = false;
      this.rtbInstructions.Size = new System.Drawing.Size(544, 184);
      this.rtbInstructions.TabIndex = 15;
      this.rtbInstructions.ToolbarRenderMode = System.Windows.Forms.ToolStripRenderMode.ManagerRenderMode;
      this.rtbInstructions.ToolbarVisible = false;
      this.rtbInstructions.RtfChanged += new System.EventHandler(this.rtbInstructions_RtfChanged);
      // 
      // tbpImages
      // 
      this.tbpImages.Controls.Add(this.btnOpenImageFile);
      this.tbpImages.Controls.Add(this.label8);
      this.tbpImages.Controls.Add(this.label12);
      this.tbpImages.Controls.Add(this.txbImageFilename);
      this.tbpImages.Controls.Add(this.cbbImageLayout);
      this.tbpImages.ImageKey = "Images";
      this.tbpImages.Location = new System.Drawing.Point(8, 39);
      this.tbpImages.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
      this.tbpImages.Name = "tbpImages";
      this.tbpImages.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
      this.tbpImages.Size = new System.Drawing.Size(544, 184);
      this.tbpImages.TabIndex = 1;
      this.tbpImages.Text = "Images";
      this.tbpImages.UseVisualStyleBackColor = true;
      // 
      // btnOpenImageFile
      // 
      this.btnOpenImageFile.Image = global::Ogama.Properties.Resources.openfolderHS;
      this.btnOpenImageFile.Location = new System.Drawing.Point(482, 13);
      this.btnOpenImageFile.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
      this.btnOpenImageFile.Name = "btnOpenImageFile";
      this.btnOpenImageFile.Size = new System.Drawing.Size(50, 48);
      this.btnOpenImageFile.TabIndex = 18;
      this.btnOpenImageFile.UseVisualStyleBackColor = true;
      this.btnOpenImageFile.Click += new System.EventHandler(this.btnOpenImageFile_Click);
      // 
      // label8
      // 
      this.label8.AutoSize = true;
      this.label8.Location = new System.Drawing.Point(12, 83);
      this.label8.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
      this.label8.Name = "label8";
      this.label8.Size = new System.Drawing.Size(77, 25);
      this.label8.TabIndex = 17;
      this.label8.Text = "Layout";
      // 
      // label12
      // 
      this.label12.AutoSize = true;
      this.label12.Location = new System.Drawing.Point(12, 25);
      this.label12.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
      this.label12.Name = "label12";
      this.label12.Size = new System.Drawing.Size(100, 25);
      this.label12.TabIndex = 16;
      this.label12.Text = "Filename";
      // 
      // txbImageFilename
      // 
      this.txbImageFilename.Location = new System.Drawing.Point(122, 19);
      this.txbImageFilename.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
      this.txbImageFilename.Multiline = true;
      this.txbImageFilename.Name = "txbImageFilename";
      this.txbImageFilename.ReadOnly = true;
      this.txbImageFilename.Size = new System.Drawing.Size(344, 39);
      this.txbImageFilename.TabIndex = 13;
      this.txbImageFilename.TextChanged += new System.EventHandler(this.txbImageFilename_TextChanged);
      // 
      // cbbImageLayout
      // 
      this.cbbImageLayout.FormattingEnabled = true;
      this.cbbImageLayout.Location = new System.Drawing.Point(122, 77);
      this.cbbImageLayout.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
      this.cbbImageLayout.Name = "cbbImageLayout";
      this.cbbImageLayout.Size = new System.Drawing.Size(162, 33);
      this.cbbImageLayout.TabIndex = 15;
      this.cbbImageLayout.Text = "Center";
      this.cbbImageLayout.SelectedIndexChanged += new System.EventHandler(this.cbbImageLayout_SelectedIndexChanged);
      // 
      // tbpSounds
      // 
      this.tbpSounds.ImageKey = "Sound";
      this.tbpSounds.Location = new System.Drawing.Point(8, 39);
      this.tbpSounds.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
      this.tbpSounds.Name = "tbpSounds";
      this.tbpSounds.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
      this.tbpSounds.Size = new System.Drawing.Size(544, 184);
      this.tbpSounds.TabIndex = 5;
      this.tbpSounds.Text = "Sounds";
      this.tbpSounds.UseVisualStyleBackColor = true;
      // 
      // splitContainer4
      // 
      this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer4.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
      this.splitContainer4.IsSplitterFixed = true;
      this.splitContainer4.Location = new System.Drawing.Point(0, 0);
      this.splitContainer4.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
      this.splitContainer4.Name = "splitContainer4";
      this.splitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // splitContainer4.Panel1
      // 
      this.splitContainer4.Panel1.Controls.Add(this.tacProperties);
      // 
      // splitContainer4.Panel2
      // 
      this.splitContainer4.Panel2.Controls.Add(this.tctStandards);
      this.splitContainer4.Size = new System.Drawing.Size(560, 898);
      this.splitContainer4.SplitterDistance = 120;
      this.splitContainer4.TabIndex = 0;
      // 
      // tacProperties
      // 
      this.tacProperties.Controls.Add(this.tbpLayout);
      this.tacProperties.Controls.Add(this.tbpCommonProperties);
      this.tacProperties.Controls.Add(this.tbpAudioProperties);
      this.tacProperties.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tacProperties.Location = new System.Drawing.Point(0, 0);
      this.tacProperties.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
      this.tacProperties.Name = "tacProperties";
      this.tacProperties.SelectedIndex = 0;
      this.tacProperties.Size = new System.Drawing.Size(560, 120);
      this.tacProperties.TabIndex = 8;
      // 
      // tbpLayout
      // 
      this.tbpLayout.Controls.Add(this.grpDocking);
      this.tbpLayout.Controls.Add(this.grpLayoutPosition);
      this.tbpLayout.Location = new System.Drawing.Point(8, 39);
      this.tbpLayout.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
      this.tbpLayout.Name = "tbpLayout";
      this.tbpLayout.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
      this.tbpLayout.Size = new System.Drawing.Size(544, 73);
      this.tbpLayout.TabIndex = 2;
      this.tbpLayout.Text = "Layout";
      this.tbpLayout.UseVisualStyleBackColor = true;
      // 
      // grpDocking
      // 
      this.grpDocking.Controls.Add(this.gveLayoutDockStyle);
      this.grpDocking.Controls.Add(this.btnLayoutCenterScreen);
      this.grpDocking.Location = new System.Drawing.Point(350, 12);
      this.grpDocking.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
      this.grpDocking.Name = "grpDocking";
      this.grpDocking.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
      this.grpDocking.Size = new System.Drawing.Size(184, 158);
      this.grpDocking.TabIndex = 2;
      this.grpDocking.TabStop = false;
      this.grpDocking.Text = "Dock";
      // 
      // gveLayoutDockStyle
      // 
      this.gveLayoutDockStyle.Converter = stringConverter1;
      this.gveLayoutDockStyle.Location = new System.Drawing.Point(8, 35);
      this.gveLayoutDockStyle.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
      this.gveLayoutDockStyle.Name = "gveLayoutDockStyle";
      this.gveLayoutDockStyle.Size = new System.Drawing.Size(160, 31);
      this.gveLayoutDockStyle.TabIndex = 2;
      this.gveLayoutDockStyle.ValueChanged += new System.EventHandler(this.GveLayoutDockStyleValueChanged);
      // 
      // btnLayoutCenterScreen
      // 
      this.btnLayoutCenterScreen.Location = new System.Drawing.Point(8, 85);
      this.btnLayoutCenterScreen.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
      this.btnLayoutCenterScreen.Name = "btnLayoutCenterScreen";
      this.btnLayoutCenterScreen.Size = new System.Drawing.Size(164, 42);
      this.btnLayoutCenterScreen.TabIndex = 1;
      this.btnLayoutCenterScreen.Text = "center screen";
      this.btnLayoutCenterScreen.UseVisualStyleBackColor = true;
      this.btnLayoutCenterScreen.Click += new System.EventHandler(this.BtnLayoutCenterScreenClick);
      // 
      // grpLayoutPosition
      // 
      this.grpLayoutPosition.Controls.Add(this.label26);
      this.grpLayoutPosition.Controls.Add(this.nudLayoutHeight);
      this.grpLayoutPosition.Controls.Add(this.label25);
      this.grpLayoutPosition.Controls.Add(this.nudLayoutWidth);
      this.grpLayoutPosition.Controls.Add(this.label24);
      this.grpLayoutPosition.Controls.Add(this.label23);
      this.grpLayoutPosition.Controls.Add(this.nudLayoutTop);
      this.grpLayoutPosition.Controls.Add(this.nudLayoutLeft);
      this.grpLayoutPosition.Location = new System.Drawing.Point(16, 12);
      this.grpLayoutPosition.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
      this.grpLayoutPosition.Name = "grpLayoutPosition";
      this.grpLayoutPosition.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
      this.grpLayoutPosition.Size = new System.Drawing.Size(330, 158);
      this.grpLayoutPosition.TabIndex = 1;
      this.grpLayoutPosition.TabStop = false;
      this.grpLayoutPosition.Text = "Position/Size";
      // 
      // label26
      // 
      this.label26.AutoSize = true;
      this.label26.Location = new System.Drawing.Point(156, 90);
      this.label26.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
      this.label26.Name = "label26";
      this.label26.Size = new System.Drawing.Size(74, 25);
      this.label26.TabIndex = 4;
      this.label26.Text = "Height";
      // 
      // nudLayoutHeight
      // 
      this.nudLayoutHeight.Location = new System.Drawing.Point(232, 87);
      this.nudLayoutHeight.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
      this.nudLayoutHeight.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
      this.nudLayoutHeight.Name = "nudLayoutHeight";
      this.nudLayoutHeight.Size = new System.Drawing.Size(90, 31);
      this.nudLayoutHeight.TabIndex = 3;
      this.nudLayoutHeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.nudLayoutHeight.ValueChanged += new System.EventHandler(this.NudLayoutHeightValueChanged);
      // 
      // label25
      // 
      this.label25.AutoSize = true;
      this.label25.Location = new System.Drawing.Point(156, 40);
      this.label25.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
      this.label25.Name = "label25";
      this.label25.Size = new System.Drawing.Size(67, 25);
      this.label25.TabIndex = 5;
      this.label25.Text = "Width";
      // 
      // nudLayoutWidth
      // 
      this.nudLayoutWidth.Location = new System.Drawing.Point(232, 37);
      this.nudLayoutWidth.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
      this.nudLayoutWidth.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
      this.nudLayoutWidth.Name = "nudLayoutWidth";
      this.nudLayoutWidth.Size = new System.Drawing.Size(90, 31);
      this.nudLayoutWidth.TabIndex = 2;
      this.nudLayoutWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.nudLayoutWidth.ValueChanged += new System.EventHandler(this.nudLayoutWidth_ValueChanged);
      // 
      // label24
      // 
      this.label24.AutoSize = true;
      this.label24.Location = new System.Drawing.Point(6, 90);
      this.label24.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
      this.label24.Name = "label24";
      this.label24.Size = new System.Drawing.Size(49, 25);
      this.label24.TabIndex = 1;
      this.label24.Text = "Top";
      // 
      // label23
      // 
      this.label23.AutoSize = true;
      this.label23.Location = new System.Drawing.Point(6, 40);
      this.label23.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
      this.label23.Name = "label23";
      this.label23.Size = new System.Drawing.Size(48, 25);
      this.label23.TabIndex = 1;
      this.label23.Text = "Left";
      // 
      // nudLayoutTop
      // 
      this.nudLayoutTop.Location = new System.Drawing.Point(58, 87);
      this.nudLayoutTop.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
      this.nudLayoutTop.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
      this.nudLayoutTop.Minimum = new decimal(new int[] {
            2000,
            0,
            0,
            -2147483648});
      this.nudLayoutTop.Name = "nudLayoutTop";
      this.nudLayoutTop.Size = new System.Drawing.Size(90, 31);
      this.nudLayoutTop.TabIndex = 0;
      this.nudLayoutTop.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.nudLayoutTop.ValueChanged += new System.EventHandler(this.nudLayoutTop_ValueChanged);
      // 
      // nudLayoutLeft
      // 
      this.nudLayoutLeft.Location = new System.Drawing.Point(58, 37);
      this.nudLayoutLeft.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
      this.nudLayoutLeft.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
      this.nudLayoutLeft.Minimum = new decimal(new int[] {
            2000,
            0,
            0,
            -2147483648});
      this.nudLayoutLeft.Name = "nudLayoutLeft";
      this.nudLayoutLeft.Size = new System.Drawing.Size(90, 31);
      this.nudLayoutLeft.TabIndex = 0;
      this.nudLayoutLeft.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.nudLayoutLeft.ValueChanged += new System.EventHandler(this.nudLayoutLeft_ValueChanged);
      // 
      // tbpCommonProperties
      // 
      this.tbpCommonProperties.Controls.Add(this.pbcElements);
      this.tbpCommonProperties.Location = new System.Drawing.Point(8, 39);
      this.tbpCommonProperties.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
      this.tbpCommonProperties.Name = "tbpCommonProperties";
      this.tbpCommonProperties.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
      this.tbpCommonProperties.Size = new System.Drawing.Size(544, 184);
      this.tbpCommonProperties.TabIndex = 0;
      this.tbpCommonProperties.Text = "Pen and Brush";
      this.tbpCommonProperties.UseVisualStyleBackColor = true;
      // 
      // pbcElements
      // 
      this.pbcElements.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pbcElements.DrawAction = VectorGraphics.Elements.ShapeDrawAction.Edge;
      this.pbcElements.Location = new System.Drawing.Point(6, 6);
      this.pbcElements.Margin = new System.Windows.Forms.Padding(12, 12, 12, 12);
      this.pbcElements.Name = "pbcElements";
      this.pbcElements.NewFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.pbcElements.NewFontColor = System.Drawing.SystemColors.GrayText;
      this.pbcElements.NewName = "";
      this.pbcElements.NewTextAlignment = VectorGraphics.Elements.VGAlignment.None;
      this.pbcElements.Size = new System.Drawing.Size(532, 172);
      this.pbcElements.TabIndex = 26;
      this.pbcElements.ShapePropertiesChanged += new System.EventHandler<VectorGraphics.Tools.CustomEventArgs.ShapePropertiesChangedEventArgs>(this.pbcElements_ShapePropertiesChanged);
      // 
      // tbpAudioProperties
      // 
      this.tbpAudioProperties.Controls.Add(this.audioControl);
      this.tbpAudioProperties.Location = new System.Drawing.Point(8, 39);
      this.tbpAudioProperties.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
      this.tbpAudioProperties.Name = "tbpAudioProperties";
      this.tbpAudioProperties.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
      this.tbpAudioProperties.Size = new System.Drawing.Size(544, 184);
      this.tbpAudioProperties.TabIndex = 1;
      this.tbpAudioProperties.Text = "Audio";
      this.tbpAudioProperties.UseVisualStyleBackColor = true;
      // 
      // audioControl
      // 
      this.audioControl.Dock = System.Windows.Forms.DockStyle.Fill;
      this.audioControl.Location = new System.Drawing.Point(6, 6);
      this.audioControl.Margin = new System.Windows.Forms.Padding(12, 12, 12, 12);
      this.audioControl.Name = "audioControl";
      this.audioControl.ShouldPlay = true;
      this.audioControl.Size = new System.Drawing.Size(532, 172);
      this.audioControl.TabIndex = 0;
      // 
      // tctStandards
      // 
      this.tctStandards.Controls.Add(this.tbpName);
      this.tctStandards.Controls.Add(this.tbpTiming);
      this.tctStandards.Controls.Add(this.tbpTesting);
      this.tctStandards.Controls.Add(this.tbpTargets);
      this.tctStandards.Controls.Add(this.tbpLinks);
      this.tctStandards.Controls.Add(this.tbpBackground);
      this.tctStandards.Controls.Add(this.tbpMouse);
      this.tctStandards.Controls.Add(this.tbpTrigger);
      this.tctStandards.Controls.Add(this.tabPage4);
      this.tctStandards.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tctStandards.ImageList = this.imlCommands;
      this.tctStandards.Location = new System.Drawing.Point(0, 0);
      this.tctStandards.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
      this.tctStandards.Name = "tctStandards";
      this.tctStandards.SelectedIndex = 0;
      this.tctStandards.Size = new System.Drawing.Size(560, 774);
      this.tctStandards.TabIndex = 5;
      // 
      // tbpName
      // 
      this.tbpName.Controls.Add(this.chbIsDisabled);
      this.tbpName.Controls.Add(this.label11);
      this.tbpName.Controls.Add(this.pcbHelpNaming);
      this.tbpName.Controls.Add(this.cbbCategory);
      this.tbpName.Controls.Add(this.label10);
      this.tbpName.Controls.Add(this.txbName);
      this.tbpName.Controls.Add(this.label5);
      this.tbpName.ImageKey = "Naming";
      this.tbpName.Location = new System.Drawing.Point(8, 39);
      this.tbpName.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
      this.tbpName.Name = "tbpName";
      this.tbpName.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
      this.tbpName.Size = new System.Drawing.Size(544, 727);
      this.tbpName.TabIndex = 6;
      this.tbpName.Text = "Default";
      this.tbpName.UseVisualStyleBackColor = true;
      // 
      // label11
      // 
      this.label11.AutoSize = true;
      this.label11.Location = new System.Drawing.Point(14, 12);
      this.label11.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
      this.label11.Name = "label11";
      this.label11.Size = new System.Drawing.Size(297, 25);
      this.label11.TabIndex = 26;
      this.label11.Text = "Specify name and category ...";
      // 
      // label10
      // 
      this.label10.AutoSize = true;
      this.label10.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label10.Location = new System.Drawing.Point(44, 125);
      this.label10.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
      this.label10.Name = "label10";
      this.label10.Size = new System.Drawing.Size(155, 37);
      this.label10.TabIndex = 19;
      this.label10.Text = "Category";
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label5.Location = new System.Drawing.Point(44, 67);
      this.label5.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(178, 37);
      this.label5.TabIndex = 0;
      this.label5.Text = "Slidename";
      // 
      // tbpTiming
      // 
      this.tbpTiming.Controls.Add(this.pcbHelpTiming);
      this.tbpTiming.Controls.Add(this.chbOnlyWhenInTarget);
      this.tbpTiming.Controls.Add(this.btnRemoveCondition);
      this.tbpTiming.Controls.Add(this.btnAddCondition);
      this.tbpTiming.Controls.Add(this.lsbStopConditions);
      this.tbpTiming.Controls.Add(this.cbbKeys);
      this.tbpTiming.Controls.Add(this.cbbMouseButtons);
      this.tbpTiming.Controls.Add(this.label3);
      this.tbpTiming.Controls.Add(this.nudTime);
      this.tbpTiming.Controls.Add(this.rdbTime);
      this.tbpTiming.Controls.Add(this.rdbMouse);
      this.tbpTiming.Controls.Add(this.rdbKey);
      this.tbpTiming.ImageKey = "Time";
      this.tbpTiming.Location = new System.Drawing.Point(8, 39);
      this.tbpTiming.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
      this.tbpTiming.Name = "tbpTiming";
      this.tbpTiming.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
      this.tbpTiming.Size = new System.Drawing.Size(544, 418);
      this.tbpTiming.TabIndex = 0;
      this.tbpTiming.Text = "Timing";
      this.tbpTiming.UseVisualStyleBackColor = true;
      // 
      // chbOnlyWhenInTarget
      // 
      this.chbOnlyWhenInTarget.AutoSize = true;
      this.chbOnlyWhenInTarget.Location = new System.Drawing.Point(26, 142);
      this.chbOnlyWhenInTarget.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
      this.chbOnlyWhenInTarget.Name = "chbOnlyWhenInTarget";
      this.chbOnlyWhenInTarget.Size = new System.Drawing.Size(264, 29);
      this.chbOnlyWhenInTarget.TabIndex = 5;
      this.chbOnlyWhenInTarget.Text = "but only in target areas";
      this.chbOnlyWhenInTarget.UseVisualStyleBackColor = true;
      this.chbOnlyWhenInTarget.Visible = false;
      // 
      // btnRemoveCondition
      // 
      this.btnRemoveCondition.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.btnRemoveCondition.ImageKey = "Delete";
      this.btnRemoveCondition.ImageList = this.imlCommands;
      this.btnRemoveCondition.Location = new System.Drawing.Point(332, 188);
      this.btnRemoveCondition.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
      this.btnRemoveCondition.Name = "btnRemoveCondition";
      this.btnRemoveCondition.Size = new System.Drawing.Size(200, 44);
      this.btnRemoveCondition.TabIndex = 6;
      this.btnRemoveCondition.Text = "Remove";
      this.btnRemoveCondition.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.btnRemoveCondition.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.btnRemoveCondition.UseVisualStyleBackColor = true;
      this.btnRemoveCondition.Click += new System.EventHandler(this.btnRemoveCondition_Click);
      // 
      // btnAddCondition
      // 
      this.btnAddCondition.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.btnAddCondition.ImageKey = "Time";
      this.btnAddCondition.ImageList = this.imlCommands;
      this.btnAddCondition.Location = new System.Drawing.Point(332, 135);
      this.btnAddCondition.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
      this.btnAddCondition.Name = "btnAddCondition";
      this.btnAddCondition.Size = new System.Drawing.Size(200, 44);
      this.btnAddCondition.TabIndex = 6;
      this.btnAddCondition.Text = "Add condition";
      this.btnAddCondition.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.btnAddCondition.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.btnAddCondition.UseVisualStyleBackColor = true;
      this.btnAddCondition.Click += new System.EventHandler(this.btnAddCondition_Click);
      // 
      // lsbStopConditions
      // 
      this.lsbStopConditions.FormattingEnabled = true;
      this.lsbStopConditions.HorizontalScrollbar = true;
      this.lsbStopConditions.ItemHeight = 25;
      this.lsbStopConditions.Location = new System.Drawing.Point(20, 240);
      this.lsbStopConditions.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
      this.lsbStopConditions.Name = "lsbStopConditions";
      this.lsbStopConditions.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
      this.lsbStopConditions.Size = new System.Drawing.Size(508, 154);
      this.lsbStopConditions.TabIndex = 5;
      this.lsbStopConditions.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ListBoxKeyDown);
      // 
      // cbbKeys
      // 
      this.cbbKeys.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbbKeys.FormattingEnabled = true;
      this.cbbKeys.Location = new System.Drawing.Point(154, 190);
      this.cbbKeys.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
      this.cbbKeys.Name = "cbbKeys";
      this.cbbKeys.Size = new System.Drawing.Size(132, 33);
      this.cbbKeys.Sorted = true;
      this.cbbKeys.TabIndex = 2;
      // 
      // cbbMouseButtons
      // 
      this.cbbMouseButtons.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbbMouseButtons.FormattingEnabled = true;
      this.cbbMouseButtons.Location = new System.Drawing.Point(154, 94);
      this.cbbMouseButtons.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
      this.cbbMouseButtons.Name = "cbbMouseButtons";
      this.cbbMouseButtons.Size = new System.Drawing.Size(132, 33);
      this.cbbMouseButtons.TabIndex = 2;
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(12, 12);
      this.label3.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(340, 25);
      this.label3.TabIndex = 4;
      this.label3.Text = "The slide should be completed by:";
      // 
      // nudTime
      // 
      this.nudTime.DecimalPlaces = 2;
      this.nudTime.Location = new System.Drawing.Point(154, 48);
      this.nudTime.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
      this.nudTime.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
      this.nudTime.Name = "nudTime";
      this.nudTime.Size = new System.Drawing.Size(136, 31);
      this.nudTime.TabIndex = 1;
      this.nudTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.nudTime.ThousandsSeparator = true;
      // 
      // rdbTime
      // 
      this.rdbTime.AutoSize = true;
      this.rdbTime.Checked = true;
      this.rdbTime.Location = new System.Drawing.Point(20, 48);
      this.rdbTime.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
      this.rdbTime.Name = "rdbTime";
      this.rdbTime.Size = new System.Drawing.Size(127, 29);
      this.rdbTime.TabIndex = 3;
      this.rdbTime.TabStop = true;
      this.rdbTime.Text = "Time (/s)";
      this.rdbTime.UseVisualStyleBackColor = true;
      // 
      // rdbMouse
      // 
      this.rdbMouse.AutoSize = true;
      this.rdbMouse.Location = new System.Drawing.Point(20, 96);
      this.rdbMouse.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
      this.rdbMouse.Name = "rdbMouse";
      this.rdbMouse.Size = new System.Drawing.Size(108, 29);
      this.rdbMouse.TabIndex = 4;
      this.rdbMouse.Text = "Mouse";
      this.rdbMouse.UseVisualStyleBackColor = true;
      // 
      // rdbKey
      // 
      this.rdbKey.AutoSize = true;
      this.rdbKey.Location = new System.Drawing.Point(20, 192);
      this.rdbKey.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
      this.rdbKey.Name = "rdbKey";
      this.rdbKey.Size = new System.Drawing.Size(80, 29);
      this.rdbKey.TabIndex = 6;
      this.rdbKey.Text = "Key";
      this.rdbKey.UseVisualStyleBackColor = true;
      // 
      // tbpTesting
      // 
      this.tbpTesting.Controls.Add(this.label17);
      this.tbpTesting.Controls.Add(this.cbbTestingTargets);
      this.tbpTesting.Controls.Add(this.btnRemoveCorrectResponse);
      this.tbpTesting.Controls.Add(this.btnAddCorrectResponse);
      this.tbpTesting.Controls.Add(this.lsbCorrectResponses);
      this.tbpTesting.Controls.Add(this.pcbHelpTesting);
      this.tbpTesting.Controls.Add(this.label7);
      this.tbpTesting.Controls.Add(this.cbbTestingKeys);
      this.tbpTesting.Controls.Add(this.cbbTestingMouseButtons);
      this.tbpTesting.Controls.Add(this.rdbTestingMouse);
      this.tbpTesting.Controls.Add(this.rdbTestingKey);
      this.tbpTesting.ImageKey = "Testing";
      this.tbpTesting.Location = new System.Drawing.Point(8, 39);
      this.tbpTesting.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
      this.tbpTesting.Name = "tbpTesting";
      this.tbpTesting.Size = new System.Drawing.Size(544, 418);
      this.tbpTesting.TabIndex = 3;
      this.tbpTesting.Text = "Testing";
      this.tbpTesting.UseVisualStyleBackColor = true;
      // 
      // label17
      // 
      this.label17.AutoSize = true;
      this.label17.Location = new System.Drawing.Point(20, 196);
      this.label17.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
      this.label17.Name = "label17";
      this.label17.Size = new System.Drawing.Size(30, 25);
      this.label17.TabIndex = 24;
      this.label17.Text = "at";
      // 
      // cbbTestingTargets
      // 
      this.cbbTestingTargets.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbbTestingTargets.FormattingEnabled = true;
      this.cbbTestingTargets.Location = new System.Drawing.Point(66, 190);
      this.cbbTestingTargets.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
      this.cbbTestingTargets.Name = "cbbTestingTargets";
      this.cbbTestingTargets.Size = new System.Drawing.Size(222, 33);
      this.cbbTestingTargets.TabIndex = 23;
      // 
      // btnRemoveCorrectResponse
      // 
      this.btnRemoveCorrectResponse.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.btnRemoveCorrectResponse.ImageKey = "Delete";
      this.btnRemoveCorrectResponse.ImageList = this.imlCommands;
      this.btnRemoveCorrectResponse.Location = new System.Drawing.Point(332, 188);
      this.btnRemoveCorrectResponse.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
      this.btnRemoveCorrectResponse.Name = "btnRemoveCorrectResponse";
      this.btnRemoveCorrectResponse.Size = new System.Drawing.Size(200, 44);
      this.btnRemoveCorrectResponse.TabIndex = 22;
      this.btnRemoveCorrectResponse.Text = "Remove";
      this.btnRemoveCorrectResponse.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.btnRemoveCorrectResponse.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.btnRemoveCorrectResponse.UseVisualStyleBackColor = true;
      this.btnRemoveCorrectResponse.Click += new System.EventHandler(this.btnRemoveCorrectResponse_Click);
      // 
      // btnAddCorrectResponse
      // 
      this.btnAddCorrectResponse.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.btnAddCorrectResponse.ImageKey = "Testing";
      this.btnAddCorrectResponse.ImageList = this.imlCommands;
      this.btnAddCorrectResponse.Location = new System.Drawing.Point(332, 140);
      this.btnAddCorrectResponse.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
      this.btnAddCorrectResponse.Name = "btnAddCorrectResponse";
      this.btnAddCorrectResponse.Size = new System.Drawing.Size(200, 44);
      this.btnAddCorrectResponse.TabIndex = 21;
      this.btnAddCorrectResponse.Text = "Add response";
      this.btnAddCorrectResponse.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.btnAddCorrectResponse.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.btnAddCorrectResponse.UseVisualStyleBackColor = true;
      this.btnAddCorrectResponse.Click += new System.EventHandler(this.btnAddCorrectResponse_Click);
      // 
      // lsbCorrectResponses
      // 
      this.lsbCorrectResponses.FormattingEnabled = true;
      this.lsbCorrectResponses.HorizontalScrollbar = true;
      this.lsbCorrectResponses.ItemHeight = 25;
      this.lsbCorrectResponses.Location = new System.Drawing.Point(20, 240);
      this.lsbCorrectResponses.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
      this.lsbCorrectResponses.Name = "lsbCorrectResponses";
      this.lsbCorrectResponses.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
      this.lsbCorrectResponses.Size = new System.Drawing.Size(508, 154);
      this.lsbCorrectResponses.TabIndex = 20;
      this.lsbCorrectResponses.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ListBoxKeyDown);
      // 
      // label7
      // 
      this.label7.Location = new System.Drawing.Point(6, 19);
      this.label7.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(530, 62);
      this.label7.TabIndex = 7;
      this.label7.Text = "Please specify the responses that are a correct answer to this slide.";
      // 
      // cbbTestingKeys
      // 
      this.cbbTestingKeys.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbbTestingKeys.FormattingEnabled = true;
      this.cbbTestingKeys.Location = new System.Drawing.Point(156, 94);
      this.cbbTestingKeys.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
      this.cbbTestingKeys.Name = "cbbTestingKeys";
      this.cbbTestingKeys.Size = new System.Drawing.Size(132, 33);
      this.cbbTestingKeys.Sorted = true;
      this.cbbTestingKeys.TabIndex = 5;
      // 
      // cbbTestingMouseButtons
      // 
      this.cbbTestingMouseButtons.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbbTestingMouseButtons.FormattingEnabled = true;
      this.cbbTestingMouseButtons.Location = new System.Drawing.Point(156, 142);
      this.cbbTestingMouseButtons.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
      this.cbbTestingMouseButtons.Name = "cbbTestingMouseButtons";
      this.cbbTestingMouseButtons.Size = new System.Drawing.Size(132, 33);
      this.cbbTestingMouseButtons.TabIndex = 6;
      // 
      // rdbTestingMouse
      // 
      this.rdbTestingMouse.AutoSize = true;
      this.rdbTestingMouse.Location = new System.Drawing.Point(20, 144);
      this.rdbTestingMouse.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
      this.rdbTestingMouse.Name = "rdbTestingMouse";
      this.rdbTestingMouse.Size = new System.Drawing.Size(108, 29);
      this.rdbTestingMouse.TabIndex = 3;
      this.rdbTestingMouse.Text = "Mouse";
      this.rdbTestingMouse.UseVisualStyleBackColor = true;
      // 
      // rdbTestingKey
      // 
      this.rdbTestingKey.AutoSize = true;
      this.rdbTestingKey.Checked = true;
      this.rdbTestingKey.Location = new System.Drawing.Point(20, 96);
      this.rdbTestingKey.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
      this.rdbTestingKey.Name = "rdbTestingKey";
      this.rdbTestingKey.Size = new System.Drawing.Size(80, 29);
      this.rdbTestingKey.TabIndex = 4;
      this.rdbTestingKey.TabStop = true;
      this.rdbTestingKey.Text = "Key";
      this.rdbTestingKey.UseVisualStyleBackColor = true;
      // 
      // tbpTargets
      // 
      this.tbpTargets.Controls.Add(this.lsbTargets);
      this.tbpTargets.Controls.Add(this.btnRemoveTarget);
      this.tbpTargets.Controls.Add(this.btnAddTargetRectangle);
      this.tbpTargets.Controls.Add(this.btnAddTargetEllipse);
      this.tbpTargets.Controls.Add(this.btnAddTargetPolyline);
      this.tbpTargets.Controls.Add(this.label16);
      this.tbpTargets.Controls.Add(this.pchHelpTargets);
      this.tbpTargets.ImageKey = "Shapes";
      this.tbpTargets.Location = new System.Drawing.Point(8, 39);
      this.tbpTargets.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
      this.tbpTargets.Name = "tbpTargets";
      this.tbpTargets.Size = new System.Drawing.Size(544, 418);
      this.tbpTargets.TabIndex = 5;
      this.tbpTargets.Text = "Targets";
      this.tbpTargets.UseVisualStyleBackColor = true;
      // 
      // lsbTargets
      // 
      this.lsbTargets.FormattingEnabled = true;
      this.lsbTargets.HorizontalScrollbar = true;
      this.lsbTargets.ItemHeight = 25;
      this.lsbTargets.Location = new System.Drawing.Point(262, 69);
      this.lsbTargets.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
      this.lsbTargets.Name = "lsbTargets";
      this.lsbTargets.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
      this.lsbTargets.Size = new System.Drawing.Size(272, 329);
      this.lsbTargets.TabIndex = 19;
      this.lsbTargets.Click += new System.EventHandler(this.lsbTargets_Click);
      // 
      // btnRemoveTarget
      // 
      this.btnRemoveTarget.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.btnRemoveTarget.ImageKey = "Delete";
      this.btnRemoveTarget.ImageList = this.imlCommands;
      this.btnRemoveTarget.Location = new System.Drawing.Point(10, 260);
      this.btnRemoveTarget.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
      this.btnRemoveTarget.Name = "btnRemoveTarget";
      this.btnRemoveTarget.Size = new System.Drawing.Size(240, 48);
      this.btnRemoveTarget.TabIndex = 18;
      this.btnRemoveTarget.Text = "Remove target";
      this.btnRemoveTarget.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.btnRemoveTarget.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.btnRemoveTarget.UseVisualStyleBackColor = true;
      this.btnRemoveTarget.Click += new System.EventHandler(this.btnRemoveTarget_Click);
      // 
      // btnAddTargetRectangle
      // 
      this.btnAddTargetRectangle.Image = global::Ogama.Properties.Resources.Rectangle;
      this.btnAddTargetRectangle.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.btnAddTargetRectangle.Location = new System.Drawing.Point(10, 69);
      this.btnAddTargetRectangle.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
      this.btnAddTargetRectangle.Name = "btnAddTargetRectangle";
      this.btnAddTargetRectangle.Size = new System.Drawing.Size(240, 52);
      this.btnAddTargetRectangle.TabIndex = 17;
      this.btnAddTargetRectangle.Text = "      Add rectangle ...";
      this.btnAddTargetRectangle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.btnAddTargetRectangle.UseVisualStyleBackColor = true;
      this.btnAddTargetRectangle.Click += new System.EventHandler(this.btnAddTargetRectangle_Click);
      // 
      // btnAddTargetEllipse
      // 
      this.btnAddTargetEllipse.Image = global::Ogama.Properties.Resources.Ellipse;
      this.btnAddTargetEllipse.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.btnAddTargetEllipse.Location = new System.Drawing.Point(10, 133);
      this.btnAddTargetEllipse.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
      this.btnAddTargetEllipse.Name = "btnAddTargetEllipse";
      this.btnAddTargetEllipse.Size = new System.Drawing.Size(240, 52);
      this.btnAddTargetEllipse.TabIndex = 16;
      this.btnAddTargetEllipse.Text = "      Add ellipse ...";
      this.btnAddTargetEllipse.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.btnAddTargetEllipse.UseVisualStyleBackColor = true;
      this.btnAddTargetEllipse.Click += new System.EventHandler(this.btnAddTargetEllipse_Click);
      // 
      // btnAddTargetPolyline
      // 
      this.btnAddTargetPolyline.Image = global::Ogama.Properties.Resources.Polyline;
      this.btnAddTargetPolyline.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.btnAddTargetPolyline.Location = new System.Drawing.Point(10, 196);
      this.btnAddTargetPolyline.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
      this.btnAddTargetPolyline.Name = "btnAddTargetPolyline";
      this.btnAddTargetPolyline.Size = new System.Drawing.Size(240, 52);
      this.btnAddTargetPolyline.TabIndex = 15;
      this.btnAddTargetPolyline.Text = "      Add polyline ...";
      this.btnAddTargetPolyline.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.btnAddTargetPolyline.UseVisualStyleBackColor = true;
      this.btnAddTargetPolyline.Click += new System.EventHandler(this.btnAddTargetPolyline_Click);
      // 
      // label16
      // 
      this.label16.AutoSize = true;
      this.label16.Location = new System.Drawing.Point(10, 15);
      this.label16.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
      this.label16.Name = "label16";
      this.label16.Size = new System.Drawing.Size(499, 25);
      this.label16.TabIndex = 14;
      this.label16.Text = "Define shapes, that specify valid click target areas:";
      // 
      // tbpLinks
      // 
      this.tbpLinks.Controls.Add(this.label20);
      this.tbpLinks.Controls.Add(this.label9);
      this.tbpLinks.Controls.Add(this.cbbLinksTrial);
      this.tbpLinks.Controls.Add(this.cbbLinksTargets);
      this.tbpLinks.Controls.Add(this.btnRemoveLink);
      this.tbpLinks.Controls.Add(this.btnAddLink);
      this.tbpLinks.Controls.Add(this.lsbLinks);
      this.tbpLinks.Controls.Add(this.pcbLinksHelp);
      this.tbpLinks.Controls.Add(this.label19);
      this.tbpLinks.Controls.Add(this.cbbLinksKeys);
      this.tbpLinks.Controls.Add(this.cbbLinksMouseButtons);
      this.tbpLinks.Controls.Add(this.rdbLinksMouse);
      this.tbpLinks.Controls.Add(this.rdbLinksKey);
      this.tbpLinks.ImageKey = "Link";
      this.tbpLinks.Location = new System.Drawing.Point(8, 39);
      this.tbpLinks.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
      this.tbpLinks.Name = "tbpLinks";
      this.tbpLinks.Size = new System.Drawing.Size(544, 418);
      this.tbpLinks.TabIndex = 7;
      this.tbpLinks.Text = "Links";
      this.tbpLinks.UseVisualStyleBackColor = true;
      // 
      // label20
      // 
      this.label20.AutoSize = true;
      this.label20.Location = new System.Drawing.Point(14, 198);
      this.label20.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
      this.label20.Name = "label20";
      this.label20.Size = new System.Drawing.Size(69, 25);
      this.label20.TabIndex = 35;
      this.label20.Text = "link to";
      // 
      // label9
      // 
      this.label9.AutoSize = true;
      this.label9.Location = new System.Drawing.Point(52, 150);
      this.label9.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
      this.label9.Name = "label9";
      this.label9.Size = new System.Drawing.Size(30, 25);
      this.label9.TabIndex = 35;
      this.label9.Text = "at";
      // 
      // cbbLinksTrial
      // 
      this.cbbLinksTrial.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbbLinksTrial.FormattingEnabled = true;
      this.cbbLinksTrial.Location = new System.Drawing.Point(96, 192);
      this.cbbLinksTrial.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
      this.cbbLinksTrial.Name = "cbbLinksTrial";
      this.cbbLinksTrial.Size = new System.Drawing.Size(182, 33);
      this.cbbLinksTrial.TabIndex = 34;
      // 
      // cbbLinksTargets
      // 
      this.cbbLinksTargets.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbbLinksTargets.FormattingEnabled = true;
      this.cbbLinksTargets.Location = new System.Drawing.Point(96, 144);
      this.cbbLinksTargets.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
      this.cbbLinksTargets.Name = "cbbLinksTargets";
      this.cbbLinksTargets.Size = new System.Drawing.Size(182, 33);
      this.cbbLinksTargets.TabIndex = 34;
      // 
      // btnRemoveLink
      // 
      this.btnRemoveLink.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.btnRemoveLink.ImageKey = "Delete";
      this.btnRemoveLink.ImageList = this.imlCommands;
      this.btnRemoveLink.Location = new System.Drawing.Point(332, 190);
      this.btnRemoveLink.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
      this.btnRemoveLink.Name = "btnRemoveLink";
      this.btnRemoveLink.Size = new System.Drawing.Size(200, 44);
      this.btnRemoveLink.TabIndex = 33;
      this.btnRemoveLink.Text = "Remove link";
      this.btnRemoveLink.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.btnRemoveLink.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.btnRemoveLink.UseVisualStyleBackColor = true;
      this.btnRemoveLink.Click += new System.EventHandler(this.btnRemoveLink_Click);
      // 
      // btnAddLink
      // 
      this.btnAddLink.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.btnAddLink.ImageKey = "Link";
      this.btnAddLink.ImageList = this.imlCommands;
      this.btnAddLink.Location = new System.Drawing.Point(332, 142);
      this.btnAddLink.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
      this.btnAddLink.Name = "btnAddLink";
      this.btnAddLink.Size = new System.Drawing.Size(200, 44);
      this.btnAddLink.TabIndex = 32;
      this.btnAddLink.Text = "Add link";
      this.btnAddLink.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.btnAddLink.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.btnAddLink.UseVisualStyleBackColor = true;
      this.btnAddLink.Click += new System.EventHandler(this.btnAddLink_Click);
      // 
      // lsbLinks
      // 
      this.lsbLinks.FormattingEnabled = true;
      this.lsbLinks.HorizontalScrollbar = true;
      this.lsbLinks.ItemHeight = 25;
      this.lsbLinks.Location = new System.Drawing.Point(20, 240);
      this.lsbLinks.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
      this.lsbLinks.Name = "lsbLinks";
      this.lsbLinks.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
      this.lsbLinks.Size = new System.Drawing.Size(508, 154);
      this.lsbLinks.TabIndex = 31;
      this.lsbLinks.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ListBoxKeyDown);
      // 
      // label19
      // 
      this.label19.AutoSize = true;
      this.label19.Location = new System.Drawing.Point(6, 19);
      this.label19.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
      this.label19.Name = "label19";
      this.label19.Size = new System.Drawing.Size(478, 25);
      this.label19.TabIndex = 29;
      this.label19.Text = "Please specify links from this slide to other trials.";
      // 
      // cbbLinksKeys
      // 
      this.cbbLinksKeys.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbbLinksKeys.FormattingEnabled = true;
      this.cbbLinksKeys.Location = new System.Drawing.Point(146, 46);
      this.cbbLinksKeys.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
      this.cbbLinksKeys.Name = "cbbLinksKeys";
      this.cbbLinksKeys.Size = new System.Drawing.Size(132, 33);
      this.cbbLinksKeys.Sorted = true;
      this.cbbLinksKeys.TabIndex = 27;
      // 
      // cbbLinksMouseButtons
      // 
      this.cbbLinksMouseButtons.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbbLinksMouseButtons.FormattingEnabled = true;
      this.cbbLinksMouseButtons.Location = new System.Drawing.Point(146, 94);
      this.cbbLinksMouseButtons.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
      this.cbbLinksMouseButtons.Name = "cbbLinksMouseButtons";
      this.cbbLinksMouseButtons.Size = new System.Drawing.Size(132, 33);
      this.cbbLinksMouseButtons.TabIndex = 28;
      // 
      // rdbLinksMouse
      // 
      this.rdbLinksMouse.AutoSize = true;
      this.rdbLinksMouse.Location = new System.Drawing.Point(20, 96);
      this.rdbLinksMouse.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
      this.rdbLinksMouse.Name = "rdbLinksMouse";
      this.rdbLinksMouse.Size = new System.Drawing.Size(108, 29);
      this.rdbLinksMouse.TabIndex = 25;
      this.rdbLinksMouse.Text = "Mouse";
      this.rdbLinksMouse.UseVisualStyleBackColor = true;
      // 
      // rdbLinksKey
      // 
      this.rdbLinksKey.AutoSize = true;
      this.rdbLinksKey.Checked = true;
      this.rdbLinksKey.Location = new System.Drawing.Point(20, 48);
      this.rdbLinksKey.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
      this.rdbLinksKey.Name = "rdbLinksKey";
      this.rdbLinksKey.Size = new System.Drawing.Size(80, 29);
      this.rdbLinksKey.TabIndex = 26;
      this.rdbLinksKey.TabStop = true;
      this.rdbLinksKey.Text = "Key";
      this.rdbLinksKey.UseVisualStyleBackColor = true;
      // 
      // tbpBackground
      // 
      this.tbpBackground.Controls.Add(this.tabControl1);
      this.tbpBackground.Controls.Add(this.pictureBox2);
      this.tbpBackground.Controls.Add(this.label4);
      this.tbpBackground.ImageKey = "Picture";
      this.tbpBackground.Location = new System.Drawing.Point(8, 39);
      this.tbpBackground.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
      this.tbpBackground.Name = "tbpBackground";
      this.tbpBackground.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
      this.tbpBackground.Size = new System.Drawing.Size(544, 418);
      this.tbpBackground.TabIndex = 1;
      this.tbpBackground.Text = "Background";
      this.tbpBackground.UseVisualStyleBackColor = true;
      // 
      // tabControl1
      // 
      this.tabControl1.Controls.Add(this.tabPage1);
      this.tabControl1.Controls.Add(this.tabPage2);
      this.tabControl1.Controls.Add(this.tabPage3);
      this.tabControl1.Location = new System.Drawing.Point(12, 90);
      this.tabControl1.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
      this.tabControl1.Name = "tabControl1";
      this.tabControl1.SelectedIndex = 0;
      this.tabControl1.Size = new System.Drawing.Size(520, 317);
      this.tabControl1.TabIndex = 15;
      // 
      // tabPage1
      // 
      this.tabPage1.Controls.Add(this.btnBackgroundColor);
      this.tabPage1.Controls.Add(this.label15);
      this.tabPage1.Controls.Add(this.label2);
      this.tabPage1.Location = new System.Drawing.Point(8, 39);
      this.tabPage1.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
      this.tabPage1.Name = "tabPage1";
      this.tabPage1.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
      this.tabPage1.Size = new System.Drawing.Size(504, 270);
      this.tabPage1.TabIndex = 0;
      this.tabPage1.Text = "Solid Color";
      this.tabPage1.UseVisualStyleBackColor = true;
      // 
      // btnBackgroundColor
      // 
      this.btnBackgroundColor.AutoButtonString = "Automatic";
      this.btnBackgroundColor.CurrentColor = System.Drawing.Color.Black;
      this.btnBackgroundColor.Location = new System.Drawing.Point(176, 77);
      this.btnBackgroundColor.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
      this.btnBackgroundColor.Name = "btnBackgroundColor";
      this.btnBackgroundColor.Size = new System.Drawing.Size(150, 44);
      this.btnBackgroundColor.TabIndex = 11;
      this.btnBackgroundColor.UseVisualStyleBackColor = true;
      this.btnBackgroundColor.ColorChanged += new System.EventHandler(this.btnBackgroundColor_ColorChanged);
      // 
      // label15
      // 
      this.label15.AutoSize = true;
      this.label15.Location = new System.Drawing.Point(22, 87);
      this.label15.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
      this.label15.Name = "label15";
      this.label15.Size = new System.Drawing.Size(142, 25);
      this.label15.TabIndex = 12;
      this.label15.Text = "Specify color:";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(12, 12);
      this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(364, 25);
      this.label2.TabIndex = 12;
      this.label2.Text = "Fill slide with solid background color:";
      // 
      // tabPage2
      // 
      this.tabPage2.Controls.Add(this.label14);
      this.tabPage2.Controls.Add(this.label6);
      this.tabPage2.Controls.Add(this.btnDeleteBackgroundImage);
      this.tabPage2.Controls.Add(this.btnBackgroundImage);
      this.tabPage2.Location = new System.Drawing.Point(8, 39);
      this.tabPage2.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
      this.tabPage2.Name = "tabPage2";
      this.tabPage2.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
      this.tabPage2.Size = new System.Drawing.Size(504, 270);
      this.tabPage2.TabIndex = 1;
      this.tabPage2.Text = "Image";
      this.tabPage2.UseVisualStyleBackColor = true;
      // 
      // label14
      // 
      this.label14.AutoSize = true;
      this.label14.Location = new System.Drawing.Point(12, 77);
      this.label14.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
      this.label14.Name = "label14";
      this.label14.Size = new System.Drawing.Size(232, 25);
      this.label14.TabIndex = 11;
      this.label14.Text = "Select or delete image:";
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Location = new System.Drawing.Point(12, 12);
      this.label6.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(413, 25);
      this.label6.TabIndex = 11;
      this.label6.Text = "Fill slide background with streched image.";
      // 
      // btnDeleteBackgroundImage
      // 
      this.btnDeleteBackgroundImage.Image = global::Ogama.Properties.Resources.DeleteHS;
      this.btnDeleteBackgroundImage.Location = new System.Drawing.Point(416, 67);
      this.btnDeleteBackgroundImage.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
      this.btnDeleteBackgroundImage.Name = "btnDeleteBackgroundImage";
      this.btnDeleteBackgroundImage.Size = new System.Drawing.Size(46, 44);
      this.btnDeleteBackgroundImage.TabIndex = 10;
      this.btnDeleteBackgroundImage.UseVisualStyleBackColor = true;
      this.btnDeleteBackgroundImage.Click += new System.EventHandler(this.btnDeleteBackgroundImage_Click);
      // 
      // btnBackgroundImage
      // 
      this.btnBackgroundImage.Location = new System.Drawing.Point(254, 67);
      this.btnBackgroundImage.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
      this.btnBackgroundImage.Name = "btnBackgroundImage";
      this.btnBackgroundImage.Size = new System.Drawing.Size(150, 44);
      this.btnBackgroundImage.TabIndex = 10;
      this.btnBackgroundImage.Text = "Image";
      this.btnBackgroundImage.UseVisualStyleBackColor = true;
      this.btnBackgroundImage.Click += new System.EventHandler(this.btnBackgroundImage_Click);
      // 
      // tabPage3
      // 
      this.tabPage3.Controls.Add(this.bkgAudioControl);
      this.tabPage3.Controls.Add(this.label13);
      this.tabPage3.Location = new System.Drawing.Point(8, 39);
      this.tabPage3.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
      this.tabPage3.Name = "tabPage3";
      this.tabPage3.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
      this.tabPage3.Size = new System.Drawing.Size(504, 270);
      this.tabPage3.TabIndex = 2;
      this.tabPage3.Text = "Music";
      this.tabPage3.UseVisualStyleBackColor = true;
      // 
      // bkgAudioControl
      // 
      this.bkgAudioControl.Location = new System.Drawing.Point(6, 63);
      this.bkgAudioControl.Margin = new System.Windows.Forms.Padding(12, 12, 12, 12);
      this.bkgAudioControl.Name = "bkgAudioControl";
      this.bkgAudioControl.ShouldPlay = false;
      this.bkgAudioControl.Size = new System.Drawing.Size(498, 138);
      this.bkgAudioControl.TabIndex = 13;
      // 
      // label13
      // 
      this.label13.AutoSize = true;
      this.label13.Location = new System.Drawing.Point(12, 12);
      this.label13.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
      this.label13.Name = "label13";
      this.label13.Size = new System.Drawing.Size(349, 25);
      this.label13.TabIndex = 12;
      this.label13.Text = "Select or delete background music.";
      // 
      // pictureBox2
      // 
      this.pictureBox2.Image = global::Ogama.Properties.Resources.rc_tif;
      this.pictureBox2.Location = new System.Drawing.Point(12, 12);
      this.pictureBox2.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
      this.pictureBox2.Name = "pictureBox2";
      this.pictureBox2.Size = new System.Drawing.Size(74, 67);
      this.pictureBox2.TabIndex = 14;
      this.pictureBox2.TabStop = false;
      // 
      // label4
      // 
      this.label4.Location = new System.Drawing.Point(100, 12);
      this.label4.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(424, 62);
      this.label4.TabIndex = 4;
      this.label4.Text = "Specify the background properties for this slide.";
      // 
      // tbpMouse
      // 
      this.tbpMouse.Controls.Add(this.lblMouseDescription);
      this.tbpMouse.Controls.Add(this.pictureBox1);
      this.tbpMouse.Controls.Add(this.grbInitialPosition);
      this.tbpMouse.Controls.Add(this.groupBox6);
      this.tbpMouse.ImageKey = "Mouse";
      this.tbpMouse.Location = new System.Drawing.Point(8, 39);
      this.tbpMouse.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
      this.tbpMouse.Name = "tbpMouse";
      this.tbpMouse.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
      this.tbpMouse.Size = new System.Drawing.Size(544, 418);
      this.tbpMouse.TabIndex = 4;
      this.tbpMouse.Text = "Mouse";
      this.tbpMouse.UseVisualStyleBackColor = true;
      // 
      // lblMouseDescription
      // 
      this.lblMouseDescription.Location = new System.Drawing.Point(100, 12);
      this.lblMouseDescription.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
      this.lblMouseDescription.Name = "lblMouseDescription";
      this.lblMouseDescription.Size = new System.Drawing.Size(404, 67);
      this.lblMouseDescription.TabIndex = 5;
      this.lblMouseDescription.Text = "Specify visibility and initial position for the mouse cursor in this trial.";
      // 
      // pictureBox1
      // 
      this.pictureBox1.Image = global::Ogama.Properties.Resources.Mouse;
      this.pictureBox1.Location = new System.Drawing.Point(12, 12);
      this.pictureBox1.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
      this.pictureBox1.Name = "pictureBox1";
      this.pictureBox1.Size = new System.Drawing.Size(74, 67);
      this.pictureBox1.TabIndex = 4;
      this.pictureBox1.TabStop = false;
      // 
      // grbInitialPosition
      // 
      this.grbInitialPosition.Controls.Add(this.chbMouseDontChangePosition);
      this.grbInitialPosition.Controls.Add(this.psbMouseCursor);
      this.grbInitialPosition.Location = new System.Drawing.Point(104, 225);
      this.grbInitialPosition.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
      this.grbInitialPosition.Name = "grbInitialPosition";
      this.grbInitialPosition.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
      this.grbInitialPosition.Size = new System.Drawing.Size(400, 108);
      this.grbInitialPosition.TabIndex = 3;
      this.grbInitialPosition.TabStop = false;
      this.grbInitialPosition.Text = "Initial position";
      // 
      // chbMouseDontChangePosition
      // 
      this.chbMouseDontChangePosition.AutoSize = true;
      this.chbMouseDontChangePosition.Location = new System.Drawing.Point(214, 44);
      this.chbMouseDontChangePosition.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
      this.chbMouseDontChangePosition.Name = "chbMouseDontChangePosition";
      this.chbMouseDontChangePosition.Size = new System.Drawing.Size(170, 29);
      this.chbMouseDontChangePosition.TabIndex = 1;
      this.chbMouseDontChangePosition.Text = "don´t change";
      this.chbMouseDontChangePosition.UseVisualStyleBackColor = true;
      // 
      // psbMouseCursor
      // 
      this.psbMouseCursor.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
      this.psbMouseCursor.CurrentPosition = new System.Drawing.Point(100, 100);
      this.psbMouseCursor.Location = new System.Drawing.Point(34, 37);
      this.psbMouseCursor.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
      this.psbMouseCursor.MinimumSize = new System.Drawing.Size(160, 42);
      this.psbMouseCursor.Name = "psbMouseCursor";
      this.psbMouseCursor.Size = new System.Drawing.Size(160, 42);
      this.psbMouseCursor.StimulusScreenSize = new System.Drawing.Size(0, 0);
      this.psbMouseCursor.TabIndex = 0;
      this.psbMouseCursor.Text = "(100,100)";
      this.psbMouseCursor.TextAlign = System.Drawing.ContentAlignment.TopLeft;
      this.psbMouseCursor.UseVisualStyleBackColor = true;
      // 
      // groupBox6
      // 
      this.groupBox6.Controls.Add(this.rdbShowMouseCursor);
      this.groupBox6.Controls.Add(this.rdbHideMouseCursor);
      this.groupBox6.Location = new System.Drawing.Point(104, 85);
      this.groupBox6.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
      this.groupBox6.Name = "groupBox6";
      this.groupBox6.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
      this.groupBox6.Size = new System.Drawing.Size(400, 129);
      this.groupBox6.TabIndex = 2;
      this.groupBox6.TabStop = false;
      this.groupBox6.Text = "Visibility";
      // 
      // rdbShowMouseCursor
      // 
      this.rdbShowMouseCursor.AutoSize = true;
      this.rdbShowMouseCursor.Checked = true;
      this.rdbShowMouseCursor.Location = new System.Drawing.Point(34, 37);
      this.rdbShowMouseCursor.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
      this.rdbShowMouseCursor.Name = "rdbShowMouseCursor";
      this.rdbShowMouseCursor.Size = new System.Drawing.Size(232, 29);
      this.rdbShowMouseCursor.TabIndex = 1;
      this.rdbShowMouseCursor.TabStop = true;
      this.rdbShowMouseCursor.Text = "Show mouse cursor";
      this.rdbShowMouseCursor.UseVisualStyleBackColor = true;
      this.rdbShowMouseCursor.CheckedChanged += new System.EventHandler(this.rdbShowMouseCursor_CheckedChanged);
      // 
      // rdbHideMouseCursor
      // 
      this.rdbHideMouseCursor.AutoSize = true;
      this.rdbHideMouseCursor.Location = new System.Drawing.Point(34, 81);
      this.rdbHideMouseCursor.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
      this.rdbHideMouseCursor.Name = "rdbHideMouseCursor";
      this.rdbHideMouseCursor.Size = new System.Drawing.Size(223, 29);
      this.rdbHideMouseCursor.TabIndex = 1;
      this.rdbHideMouseCursor.Text = "Hide mouse cursor";
      this.rdbHideMouseCursor.UseVisualStyleBackColor = true;
      // 
      // tbpTrigger
      // 
      this.tbpTrigger.Controls.Add(this.label22);
      this.tbpTrigger.Controls.Add(this.label21);
      this.tbpTrigger.Controls.Add(this.pcbHelpTrigger);
      this.tbpTrigger.Controls.Add(this.triggerControl);
      this.tbpTrigger.ImageKey = "Trigger";
      this.tbpTrigger.Location = new System.Drawing.Point(8, 39);
      this.tbpTrigger.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
      this.tbpTrigger.Name = "tbpTrigger";
      this.tbpTrigger.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
      this.tbpTrigger.Size = new System.Drawing.Size(544, 418);
      this.tbpTrigger.TabIndex = 8;
      this.tbpTrigger.Text = "Trigger";
      this.tbpTrigger.UseVisualStyleBackColor = true;
      // 
      // label22
      // 
      this.label22.AutoSize = true;
      this.label22.Location = new System.Drawing.Point(396, 231);
      this.label22.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
      this.label22.Name = "label22";
      this.label22.Size = new System.Drawing.Size(117, 25);
      this.label22.TabIndex = 30;
      this.label22.Text = "0378,D800";
      // 
      // label21
      // 
      this.label21.AutoSize = true;
      this.label21.Location = new System.Drawing.Point(12, 12);
      this.label21.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
      this.label21.Name = "label21";
      this.label21.Size = new System.Drawing.Size(396, 25);
      this.label21.TabIndex = 28;
      this.label21.Text = "Specify optional trigger signal to send ...";
      // 
      // triggerControl
      // 
      this.triggerControl.Location = new System.Drawing.Point(16, 62);
      this.triggerControl.Margin = new System.Windows.Forms.Padding(12, 12, 12, 12);
      this.triggerControl.Name = "triggerControl";
      this.triggerControl.Size = new System.Drawing.Size(388, 271);
      this.triggerControl.TabIndex = 29;
      // 
      // tabPage4
      // 
      this.tabPage4.Controls.Add(this.label28);
      this.tabPage4.Controls.Add(this.cbbPreSlideFixationTrial);
      this.tabPage4.Controls.Add(this.label27);
      this.tabPage4.Location = new System.Drawing.Point(8, 39);
      this.tabPage4.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
      this.tabPage4.Name = "tabPage4";
      this.tabPage4.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
      this.tabPage4.Size = new System.Drawing.Size(544, 418);
      this.tabPage4.TabIndex = 9;
      this.tabPage4.Text = "Pre Slide Fixation Trial";
      this.tabPage4.UseVisualStyleBackColor = true;
      // 
      // label28
      // 
      this.label28.AutoSize = true;
      this.label28.Location = new System.Drawing.Point(22, 119);
      this.label28.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
      this.label28.Name = "label28";
      this.label28.Size = new System.Drawing.Size(120, 25);
      this.label28.TabIndex = 36;
      this.label28.Text = "Select Trial";
      // 
      // cbbPreSlideFixationTrial
      // 
      this.cbbPreSlideFixationTrial.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbbPreSlideFixationTrial.FormattingEnabled = true;
      this.cbbPreSlideFixationTrial.Location = new System.Drawing.Point(160, 113);
      this.cbbPreSlideFixationTrial.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
      this.cbbPreSlideFixationTrial.Name = "cbbPreSlideFixationTrial";
      this.cbbPreSlideFixationTrial.Size = new System.Drawing.Size(182, 33);
      this.cbbPreSlideFixationTrial.TabIndex = 35;
      // 
      // label27
      // 
      this.label27.AutoSize = true;
      this.label27.Location = new System.Drawing.Point(12, 23);
      this.label27.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
      this.label27.Name = "label27";
      this.label27.Size = new System.Drawing.Size(536, 50);
      this.label27.TabIndex = 29;
      this.label27.Text = "Specify the trial to be shown before this slide is shown,\r\nthis can be a fixation" +
    " sharp or anything else.";
      // 
      // pnlCanvas
      // 
      this.pnlCanvas.BackColor = System.Drawing.SystemColors.Control;
      this.pnlCanvas.Controls.Add(this.pnlPicture);
      this.pnlCanvas.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pnlCanvas.Location = new System.Drawing.Point(0, 0);
      this.pnlCanvas.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
      this.pnlCanvas.Name = "pnlCanvas";
      this.pnlCanvas.Size = new System.Drawing.Size(1272, 1020);
      this.pnlCanvas.TabIndex = 8;
      // 
      // pnlPicture
      // 
      this.pnlPicture.Controls.Add(this.designPicture);
      this.pnlPicture.Location = new System.Drawing.Point(48, 44);
      this.pnlPicture.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
      this.pnlPicture.Name = "pnlPicture";
      this.pnlPicture.Size = new System.Drawing.Size(1100, 840);
      this.pnlPicture.TabIndex = 8;
      // 
      // designPicture
      // 
      this.designPicture.AnimationInterval = 100;
      this.designPicture.BackColor = System.Drawing.Color.Black;
      this.designPicture.InvalidateInterval = 500;
      this.designPicture.Location = new System.Drawing.Point(0, 0);
      this.designPicture.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
      this.designPicture.Name = "designPicture";
      this.designPicture.Size = new System.Drawing.Size(1100, 840);
      this.designPicture.TabIndex = 7;
      this.designPicture.TabStop = false;
      this.designPicture.ZoomFactor = 0F;
      this.designPicture.ShapeAdded += new VectorGraphics.Tools.CustomEventArgs.ShapeEventHandler(this.picPreview_ShapeAdded);
      this.designPicture.ShapeDeleted += new VectorGraphics.Tools.CustomEventArgs.ShapeEventHandler(this.picPreview_ShapeDeleted);
      this.designPicture.ShapeSelected += new VectorGraphics.Tools.CustomEventArgs.ShapeEventHandler(this.picPreview_ShapeSelected);
      this.designPicture.ShapeDeselected += new System.EventHandler(this.PicPreviewShapeDeselected);
      this.designPicture.ShapeChanged += new VectorGraphics.Tools.CustomEventArgs.ShapeEventHandler(this.DesignPictureShapeChanged);
      this.designPicture.ShapeDoubleClick += new VectorGraphics.Tools.CustomEventArgs.ShapeEventHandler(this.PicPreviewShapeDoubleClick);
      // 
      // dltForm
      // 
      this.dltForm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(237)))));
      this.dltForm.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("dltForm.BackgroundImage")));
      this.dltForm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
      this.dltForm.Description = "Instruction stimuli are used to display a message to the subject or fill in a pau" +
    "se slide.";
      this.dltForm.Dock = System.Windows.Forms.DockStyle.Top;
      this.dltForm.Location = new System.Drawing.Point(0, 0);
      this.dltForm.Logo = ((System.Drawing.Image)(resources.GetObject("dltForm.Logo")));
      this.dltForm.Margin = new System.Windows.Forms.Padding(0);
      this.dltForm.Name = "dltForm";
      this.dltForm.Size = new System.Drawing.Size(1840, 115);
      this.dltForm.TabIndex = 20;
      // 
      // splitContainer1
      // 
      this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
      this.splitContainer1.IsSplitterFixed = true;
      this.splitContainer1.Location = new System.Drawing.Point(0, 0);
      this.splitContainer1.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
      this.splitContainer1.Name = "splitContainer1";
      this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // splitContainer1.Panel1
      // 
      this.splitContainer1.Panel1.Controls.Add(this.dltForm);
      // 
      // splitContainer1.Panel2
      // 
      this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
      this.splitContainer1.Size = new System.Drawing.Size(1840, 1125);
      this.splitContainer1.SplitterDistance = 60;
      this.splitContainer1.SplitterWidth = 2;
      this.splitContainer1.TabIndex = 9;
      // 
      // splitContainer2
      // 
      this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
      this.splitContainer2.IsSplitterFixed = true;
      this.splitContainer2.Location = new System.Drawing.Point(0, 0);
      this.splitContainer2.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
      this.splitContainer2.Name = "splitContainer2";
      this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // splitContainer2.Panel1
      // 
      this.splitContainer2.Panel1.Controls.Add(this.spcStimuliStandard);
      // 
      // splitContainer2.Panel2
      // 
      this.splitContainer2.Panel2.Controls.Add(this.panel1);
      this.splitContainer2.Panel2MinSize = 35;
      this.splitContainer2.Size = new System.Drawing.Size(1840, 1063);
      this.splitContainer2.SplitterDistance = 1020;
      this.splitContainer2.SplitterWidth = 8;
      this.splitContainer2.TabIndex = 0;
      // 
      // SlideDesignModule
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.btnCancel;
      this.ClientSize = new System.Drawing.Size(1840, 1125);
      this.Controls.Add(this.splitContainer1);
      this.DataBindings.Add(new System.Windows.Forms.Binding("Location", global::Ogama.Properties.Settings.Default, "SlidedesignModuleLocation", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.DoubleBuffered = true;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Location = global::Ogama.Properties.Settings.Default.SlidedesignModuleLocation;
      this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "SlideDesignModule";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Create new stimulus slide ...";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmStimulusDesignFormClosing);
      this.Load += new System.EventHandler(this.SlideDesignModuleLoad);
      this.Shown += new System.EventHandler(this.SlideDesignModuleShown);
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
      ((System.ComponentModel.ISupportInitialize)(this.pcbHelpNaming)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pcbHelpTiming)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pcbHelpTesting)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pchHelpTargets)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pcbLinksHelp)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pcbHelpTrigger)).EndInit();
      this.panel1.ResumeLayout(false);
      this.spcStimuliStandard.Panel1.ResumeLayout(false);
      this.spcStimuliStandard.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.spcStimuliStandard)).EndInit();
      this.spcStimuliStandard.ResumeLayout(false);
      this.splitContainer3.Panel1.ResumeLayout(false);
      this.splitContainer3.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
      this.splitContainer3.ResumeLayout(false);
      this.tctStimuli.ResumeLayout(false);
      this.tbpNewStimuli.ResumeLayout(false);
      this.tbpNewStimuli.PerformLayout();
      this.tbpInstructions.ResumeLayout(false);
      this.tbpInstructions.PerformLayout();
      this.tbpRtfInstructions.ResumeLayout(false);
      this.tbpImages.ResumeLayout(false);
      this.tbpImages.PerformLayout();
      this.splitContainer4.Panel1.ResumeLayout(false);
      this.splitContainer4.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).EndInit();
      this.splitContainer4.ResumeLayout(false);
      this.tacProperties.ResumeLayout(false);
      this.tbpLayout.ResumeLayout(false);
      this.grpDocking.ResumeLayout(false);
      this.grpLayoutPosition.ResumeLayout(false);
      this.grpLayoutPosition.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.nudLayoutHeight)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.nudLayoutWidth)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.nudLayoutTop)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.nudLayoutLeft)).EndInit();
      this.tbpCommonProperties.ResumeLayout(false);
      this.tbpAudioProperties.ResumeLayout(false);
      this.tctStandards.ResumeLayout(false);
      this.tbpName.ResumeLayout(false);
      this.tbpName.PerformLayout();
      this.tbpTiming.ResumeLayout(false);
      this.tbpTiming.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.nudTime)).EndInit();
      this.tbpTesting.ResumeLayout(false);
      this.tbpTesting.PerformLayout();
      this.tbpTargets.ResumeLayout(false);
      this.tbpTargets.PerformLayout();
      this.tbpLinks.ResumeLayout(false);
      this.tbpLinks.PerformLayout();
      this.tbpBackground.ResumeLayout(false);
      this.tabControl1.ResumeLayout(false);
      this.tabPage1.ResumeLayout(false);
      this.tabPage1.PerformLayout();
      this.tabPage2.ResumeLayout(false);
      this.tabPage2.PerformLayout();
      this.tabPage3.ResumeLayout(false);
      this.tabPage3.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
      this.tbpMouse.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
      this.grbInitialPosition.ResumeLayout(false);
      this.grbInitialPosition.PerformLayout();
      this.groupBox6.ResumeLayout(false);
      this.groupBox6.PerformLayout();
      this.tbpTrigger.ResumeLayout(false);
      this.tbpTrigger.PerformLayout();
      this.tabPage4.ResumeLayout(false);
      this.tabPage4.PerformLayout();
      this.pnlCanvas.ResumeLayout(false);
      this.pnlPicture.ResumeLayout(false);
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
      this.splitContainer1.ResumeLayout(false);
      this.splitContainer2.Panel1.ResumeLayout(false);
      this.splitContainer2.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
      this.splitContainer2.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Button btnBackgroundImage;
    private System.Windows.Forms.ComboBox cbbKeys;
    private System.Windows.Forms.ComboBox cbbMouseButtons;
    private System.Windows.Forms.NumericUpDown nudTime;
    private System.Windows.Forms.RadioButton rdbTime;
    private System.Windows.Forms.RadioButton rdbKey;
    private System.Windows.Forms.RadioButton rdbMouse;
    private OgamaControls.ColorButton btnBackgroundColor;
    private System.Windows.Forms.OpenFileDialog ofdBackgroundImage;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Button btnDeleteBackgroundImage;
    private System.Windows.Forms.TabControl tctStandards;
    private System.Windows.Forms.TabPage tbpTiming;
    private System.Windows.Forms.TabPage tbpBackground;
    private System.Windows.Forms.Button btnAddCondition;
    private System.Windows.Forms.ListBox lsbStopConditions;
    private System.Windows.Forms.Button btnRemoveCondition;
    private System.Windows.Forms.ImageList imlCommands;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.SplitContainer spcStimuliStandard;
    private TabPage tbpTesting;
    private Label label7;
    private ComboBox cbbTestingKeys;
    private ComboBox cbbTestingMouseButtons;
    private RadioButton rdbTestingMouse;
    private RadioButton rdbTestingKey;
    private ToolTip toolTip;
    private SlidePicture designPicture;
    private CheckBox chbOnlyWhenInTarget;
    private Panel pnlCanvas;
    private TabPage tbpMouse;
    private OgamaControls.PositionButton psbMouseCursor;
    private Label lblMouseDescription;
    private PictureBox pictureBox1;
    private GroupBox grbInitialPosition;
    private GroupBox groupBox6;
    private RadioButton rdbShowMouseCursor;
    private RadioButton rdbHideMouseCursor;
    private TabControl tctStimuli;
    private TabPage tbpInstructions;
    private TabPage tbpImages;
    private Button btnOpenImageFile;
    private Label label8;
    private Label label12;
    private TextBox txbImageFilename;
    private ComboBox cbbImageLayout;
    private TabPage tbpTargets;
    private PictureBox pcbHelpTiming;
    private PictureBox pchHelpTargets;
    private ListBox lsbTargets;
    private Button btnRemoveTarget;
    private Button btnAddTargetRectangle;
    private Button btnAddTargetEllipse;
    private Button btnAddTargetPolyline;
    private Label label16;
    private PictureBox pcbHelpTesting;
    private Label label17;
    private ComboBox cbbTestingTargets;
    private Button btnRemoveCorrectResponse;
    private Button btnAddCorrectResponse;
    private ListBox lsbCorrectResponses;
    private PictureBox pictureBox2;
    private TabPage tbpNewStimuli;
    private Button btnAddInstruction;
    private Button btnAddImage;
    private DialogTop dltForm;
    private Panel panel1;
    private Button btnOK;
    private Button btnCancel;
    private Label label5;
    private TextBox txbName;
    private Label label10;
    private ComboBox cbbCategory;
    private Label label18;
    private Button btnAddShape;
    private OgamaControls.PenAndBrushControl pbcElements;
    private Button btnHelp;
    private Button btnAddSound;
    private TabPage tbpSounds;
    private TabPage tbpName;
    private Label label11;
    private PictureBox pcbHelpNaming;
    private Label label13;
    private TabControl tabControl1;
    private TabPage tabPage1;
    private TabPage tabPage2;
    private TabPage tabPage3;
    private Label label15;
    private Label label14;
    private TabControl tacProperties;
    private TabPage tbpCommonProperties;
    private TabPage tbpAudioProperties;
    private OgamaControls.AudioControl audioControl;
    private OgamaControls.AudioControl bkgAudioControl;
    private TabPage tbpLinks;
    private Label label9;
    private ComboBox cbbLinksTargets;
    private Button btnRemoveLink;
    private Button btnAddLink;
    private ListBox lsbLinks;
    private Label label19;
    private ComboBox cbbLinksKeys;
    private ComboBox cbbLinksMouseButtons;
    private RadioButton rdbLinksMouse;
    private RadioButton rdbLinksKey;
    private Label label20;
    private ComboBox cbbLinksTrial;
    private PictureBox pcbLinksHelp;
    private Button btnAddRtfInstruction;
    private TabPage tbpRtfInstructions;
    private OgamaControls.RTBTextControl rtbInstructions;
    private TextBox txbInstructions;
    private TabPage tbpTrigger;
    private Label label21;
    private PictureBox pcbHelpTrigger;
    private OgamaControls.TriggerControl triggerControl;
		private Label label22;
    private Panel pnlPicture;
    private CheckBox chbMouseDontChangePosition;
    private SplitContainer splitContainer3;
    private SplitContainer splitContainer4;
    private SplitContainer splitContainer1;
    private SplitContainer splitContainer2;
    private TabPage tbpLayout;
    private GroupBox grpLayoutPosition;
    private Label label24;
    private Label label23;
    private NumericUpDown nudLayoutTop;
    private NumericUpDown nudLayoutLeft;
    private GroupBox grpDocking;
    private Button btnLayoutCenterScreen;
    private Label label26;
    private NumericUpDown nudLayoutHeight;
    private Label label25;
    private NumericUpDown nudLayoutWidth;
    private OgamaControls.GenericValueEditor gveLayoutDockStyle;
    private CheckBox chbIsDisabled;
    private TabPage tabPage4;
    private Label label28;
    private ComboBox cbbPreSlideFixationTrial;
    private Label label27;
  }
}
