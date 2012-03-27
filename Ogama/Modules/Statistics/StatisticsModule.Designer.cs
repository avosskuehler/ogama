namespace Ogama.Modules.Statistics
{
  using Ogama.Modules.Common.Controls;

  /// <summary>
  /// 
  /// </summary>
  partial class StatisticsModule
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
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StatisticsModule));
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
      this.dGVExportTable = new System.Windows.Forms.DataGridView();
      this.cmuDGV = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.cmuRecalculate = new System.Windows.Forms.ToolStripMenuItem();
      this.cmuRemoveColumn = new System.Windows.Forms.ToolStripMenuItem();
      this.chbSUBHandedness = new System.Windows.Forms.CheckBox();
      this.chbSUBAge = new System.Windows.Forms.CheckBox();
      this.chbSUBComments = new System.Windows.Forms.CheckBox();
      this.chbSUBCategory = new System.Windows.Forms.CheckBox();
      this.chbSUBSex = new System.Windows.Forms.CheckBox();
      this.chbSUBID = new System.Windows.Forms.CheckBox();
      this.chbTRIEliminate = new System.Windows.Forms.CheckBox();
      this.chbTRIAOISize = new System.Windows.Forms.CheckBox();
      this.chbTRITargetsize = new System.Windows.Forms.CheckBox();
      this.chbTRIDataLoss = new System.Windows.Forms.CheckBox();
      this.chbTRIResponse = new System.Windows.Forms.CheckBox();
      this.chbTRIDuration = new System.Windows.Forms.CheckBox();
      this.chbTRICategory = new System.Windows.Forms.CheckBox();
      this.chbTRIName = new System.Windows.Forms.CheckBox();
      this.chbTRITrialID = new System.Windows.Forms.CheckBox();
      this.chbGAZFixTimeAtTarget = new System.Windows.Forms.CheckBox();
      this.chbGAZTime2SecondFixAtTarget = new System.Windows.Forms.CheckBox();
      this.chbGAZCountFix2FirstClick = new System.Windows.Forms.CheckBox();
      this.chbGAZTime2FirstFixAtTarget = new System.Windows.Forms.CheckBox();
      this.chbGAZTime2FirstFixInSearchRect = new System.Windows.Forms.CheckBox();
      this.chbGAZFixDurationMean = new System.Windows.Forms.CheckBox();
      this.chbGAZFixations = new System.Windows.Forms.CheckBox();
      this.chbMSETime2FirstClick = new System.Windows.Forms.CheckBox();
      this.chbMSETime2FirstFixAtTarget = new System.Windows.Forms.CheckBox();
      this.chbMSETime2FirstFixInSearchRect = new System.Windows.Forms.CheckBox();
      this.chbMSEPathlength = new System.Windows.Forms.CheckBox();
      this.chbMSERightClicks = new System.Windows.Forms.CheckBox();
      this.chbMSELeftClicks = new System.Windows.Forms.CheckBox();
      this.bgwCalculateStandards = new System.ComponentModel.BackgroundWorker();
      this.sfdExport = new System.Windows.Forms.SaveFileDialog();
      this.toolTipStatistic = new System.Windows.Forms.ToolTip(this.components);
      this.chbTRISamplesOutOfMonitor = new System.Windows.Forms.CheckBox();
      this.chbTRIDataLossPC = new System.Windows.Forms.CheckBox();
      this.chbTRISamplesOutOfMonitorPC = new System.Windows.Forms.CheckBox();
      this.chbTRIStarttime = new System.Windows.Forms.CheckBox();
      this.chbGAZSaccadeLength = new System.Windows.Forms.CheckBox();
      this.chbGAZSaccadeVelocity = new System.Windows.Forms.CheckBox();
      this.chbGAZFixationsPS = new System.Windows.Forms.CheckBox();
      this.chbGAZFixationSaccadesRatio = new System.Windows.Forms.CheckBox();
      this.chbMSEPathlengthPS = new System.Windows.Forms.CheckBox();
      this.chbMSERightClicksPS = new System.Windows.Forms.CheckBox();
      this.chbMSELeftClicksPS = new System.Windows.Forms.CheckBox();
      this.nudTolerance = new System.Windows.Forms.NumericUpDown();
      this.chbTRIResponseCorrectness = new System.Windows.Forms.CheckBox();
      this.groupBox10 = new System.Windows.Forms.GroupBox();
      this.chbMSECountFix2FirstClick = new System.Windows.Forms.CheckBox();
      this.checkBox1 = new System.Windows.Forms.CheckBox();
      this.checkBox2 = new System.Windows.Forms.CheckBox();
      this.checkBox3 = new System.Windows.Forms.CheckBox();
      this.checkBox4 = new System.Windows.Forms.CheckBox();
      this.checkBox5 = new System.Windows.Forms.CheckBox();
      this.chbMSEFixations = new System.Windows.Forms.CheckBox();
      this.chbMSEFixationsPS = new System.Windows.Forms.CheckBox();
      this.chbMSEFixDurationMean = new System.Windows.Forms.CheckBox();
      this.chbMSEFixationSaccadesRatio = new System.Windows.Forms.CheckBox();
      this.chbMSETime2SecondFixAtTarget = new System.Windows.Forms.CheckBox();
      this.chbMSEFixTimeAtTarget = new System.Windows.Forms.CheckBox();
      this.chbMSEAverageDistance = new System.Windows.Forms.CheckBox();
      this.dgvTransitions = new System.Windows.Forms.DataGridView();
      this.chbTRIAOIofResponse = new System.Windows.Forms.CheckBox();
      this.chbTRISequence = new System.Windows.Forms.CheckBox();
      this.rdbGazeCompleteFixationTime = new System.Windows.Forms.RadioButton();
      this.rdbGazeTimeUntil = new System.Windows.Forms.RadioButton();
      this.rdbGazeFixationDurationMean = new System.Windows.Forms.RadioButton();
      this.rdbGazeNumberOfFixations = new System.Windows.Forms.RadioButton();
      this.cbbGazeAOISingle = new System.Windows.Forms.ComboBox();
      this.cbbGazeAOIGroups = new System.Windows.Forms.ComboBox();
      this.rdbMouseClickAOI = new System.Windows.Forms.RadioButton();
      this.rdbMouseFixationDurationMean = new System.Windows.Forms.RadioButton();
      this.rdbMouseNumberOfFixations = new System.Windows.Forms.RadioButton();
      this.rdbMouseCompleteFixationTime = new System.Windows.Forms.RadioButton();
      this.rdbMouseTimeUntil = new System.Windows.Forms.RadioButton();
      this.chbMSESaccadeLength = new System.Windows.Forms.CheckBox();
      this.chbMSESaccadeVelocity = new System.Windows.Forms.CheckBox();
      this.chbGAZPathlength = new System.Windows.Forms.CheckBox();
      this.chbGAZPathlengthPS = new System.Windows.Forms.CheckBox();
      this.chbGAZFixDurationMedian = new System.Windows.Forms.CheckBox();
      this.chbMSEFixDurationMedian = new System.Windows.Forms.CheckBox();
      this.cbbGazeRegressionAOISingle = new System.Windows.Forms.ComboBox();
      this.cbbGazeRegressionAOIGroups = new System.Windows.Forms.ComboBox();
      this.tacMeta = new System.Windows.Forms.TabControl();
      this.tbpMetaStandard = new System.Windows.Forms.TabPage();
      this.tscStandard = new System.Windows.Forms.ToolStripContainer();
      this.spcTacDgv = new System.Windows.Forms.SplitContainer();
      this.tacStandard = new System.Windows.Forms.TabControl();
      this.tbpSubject = new System.Windows.Forms.TabPage();
      this.splitContainer4 = new System.Windows.Forms.SplitContainer();
      this.dialogTop3 = new DialogTop();
      this.groupBox24 = new System.Windows.Forms.GroupBox();
      this.trvSubjects = new OgamaControls.CheckboxTreeView(this.components);
      this.imlTreeViewSubjects = new System.Windows.Forms.ImageList(this.components);
      this.groupBox21 = new System.Windows.Forms.GroupBox();
      this.clbSUBCustomparameters = new System.Windows.Forms.CheckedListBox();
      this.groupBox6 = new System.Windows.Forms.GroupBox();
      this.groupBox5 = new System.Windows.Forms.GroupBox();
      this.tbpTrial = new System.Windows.Forms.TabPage();
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      this.dialogTop1 = new DialogTop();
      this.tpbTrial = new System.Windows.Forms.TabControl();
      this.tpbTrialStandard = new System.Windows.Forms.TabPage();
      this.groupBox17 = new System.Windows.Forms.GroupBox();
      this.btnStandardDeselectAllTrials = new System.Windows.Forms.Button();
      this.btnStandardSelectAllTrials = new System.Windows.Forms.Button();
      this.trvTrialsDefault = new OgamaControls.CheckboxTreeView(this.components);
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.tbpTrialAdditional = new System.Windows.Forms.TabPage();
      this.groupBox2 = new System.Windows.Forms.GroupBox();
      this.groupBox3 = new System.Windows.Forms.GroupBox();
      this.groupBox4 = new System.Windows.Forms.GroupBox();
      this.tbpGaze = new System.Windows.Forms.TabPage();
      this.splitContainer3 = new System.Windows.Forms.SplitContainer();
      this.label8 = new System.Windows.Forms.Label();
      this.dialogTop2 = new DialogTop();
      this.tacGazeParams = new System.Windows.Forms.TabControl();
      this.tbpGazeDefault = new System.Windows.Forms.TabPage();
      this.groupBox7 = new System.Windows.Forms.GroupBox();
      this.groupBox22 = new System.Windows.Forms.GroupBox();
      this.groupBox9 = new System.Windows.Forms.GroupBox();
      this.groupBox14 = new System.Windows.Forms.GroupBox();
      this.tbpGazeAOI = new System.Windows.Forms.TabPage();
      this.groupBox8 = new System.Windows.Forms.GroupBox();
      this.groupBox15 = new System.Windows.Forms.GroupBox();
      this.rdbGazeAOISingle = new System.Windows.Forms.RadioButton();
      this.rdbGazeAOIGroup = new System.Windows.Forms.RadioButton();
      this.btnGazeAddCustomVariable = new System.Windows.Forms.Button();
      this.nudGazeTimeUntilNumberOf = new System.Windows.Forms.NumericUpDown();
      this.rdbGazeSaccadeVelocity = new System.Windows.Forms.RadioButton();
      this.rdbGazeFixationDurationMedian = new System.Windows.Forms.RadioButton();
      this.rdbGazeSaccadeLength = new System.Windows.Forms.RadioButton();
      this.rdbGazeSaccadeDuration = new System.Windows.Forms.RadioButton();
      this.label11 = new System.Windows.Forms.Label();
      this.tbpGazeRegressions = new System.Windows.Forms.TabPage();
      this.label6 = new System.Windows.Forms.Label();
      this.btnGazeAddCustomRegressionVariable = new System.Windows.Forms.Button();
      this.label5 = new System.Windows.Forms.Label();
      this.nudGazeRegressionLineHeight = new System.Windows.Forms.NumericUpDown();
      this.rdbGazeRegressionAOISingle = new System.Windows.Forms.RadioButton();
      this.rdbGazeRegressionAll = new System.Windows.Forms.RadioButton();
      this.rdbGazeRegressionAOIGroup = new System.Windows.Forms.RadioButton();
      this.tbpMouse = new System.Windows.Forms.TabPage();
      this.splitContainer5 = new System.Windows.Forms.SplitContainer();
      this.label1 = new System.Windows.Forms.Label();
      this.dialogTop4 = new DialogTop();
      this.tacMouse = new System.Windows.Forms.TabControl();
      this.tbpMouseDefault = new System.Windows.Forms.TabPage();
      this.groupBox23 = new System.Windows.Forms.GroupBox();
      this.groupBox13 = new System.Windows.Forms.GroupBox();
      this.groupBox12 = new System.Windows.Forms.GroupBox();
      this.tbpMouseAOI = new System.Windows.Forms.TabPage();
      this.groupBox11 = new System.Windows.Forms.GroupBox();
      this.groupBox16 = new System.Windows.Forms.GroupBox();
      this.rdbMouseSaccadeVelocity = new System.Windows.Forms.RadioButton();
      this.rdbMouseSaccadeLength = new System.Windows.Forms.RadioButton();
      this.rdbMouseSaccadeDuration = new System.Windows.Forms.RadioButton();
      this.cbbMouseClickButton = new System.Windows.Forms.ComboBox();
      this.rdbMouseAOISingle = new System.Windows.Forms.RadioButton();
      this.rdbMouseAOIGroup = new System.Windows.Forms.RadioButton();
      this.rdbMouseFixationDurationMedian = new System.Windows.Forms.RadioButton();
      this.cbbMouseAOISingle = new System.Windows.Forms.ComboBox();
      this.btnMouseAddCustomVariable = new System.Windows.Forms.Button();
      this.nudMouseNumberOf = new System.Windows.Forms.NumericUpDown();
      this.label10 = new System.Windows.Forms.Label();
      this.cbbMouseAOIGroup = new System.Windows.Forms.ComboBox();
      this.tbpOptions = new System.Windows.Forms.TabPage();
      this.splitContainer6 = new System.Windows.Forms.SplitContainer();
      this.dialogTop5 = new DialogTop();
      this.label3 = new System.Windows.Forms.Label();
      this.chb8CharacterRow = new System.Windows.Forms.CheckBox();
      this.label4 = new System.Windows.Forms.Label();
      this.imlTabImages = new System.Windows.Forms.ImageList(this.components);
      this.tosStandard = new System.Windows.Forms.ToolStrip();
      this.btnFillWithData = new System.Windows.Forms.ToolStripButton();
      this.btnCancelCalculation = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.btnExport = new System.Windows.Forms.ToolStripButton();
      this.btnSelectAll = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
      this.btnHelp = new System.Windows.Forms.ToolStripButton();
      this.tbpMetaTransitions = new System.Windows.Forms.TabPage();
      this.tscTransitions = new System.Windows.Forms.ToolStripContainer();
      this.splitContainer2 = new System.Windows.Forms.SplitContainer();
      this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
      this.groupBox19 = new System.Windows.Forms.GroupBox();
      this.btnTransitionsDeselectAllTrials = new System.Windows.Forms.Button();
      this.pictureBox7 = new System.Windows.Forms.PictureBox();
      this.btnTransitionsSelectAllTrials = new System.Windows.Forms.Button();
      this.label15 = new System.Windows.Forms.Label();
      this.trvTrialsAOI = new OgamaControls.CheckboxTreeView(this.components);
      this.groupBox18 = new System.Windows.Forms.GroupBox();
      this.splitContainer7 = new System.Windows.Forms.SplitContainer();
      this.pictureBox6 = new System.Windows.Forms.PictureBox();
      this.label14 = new System.Windows.Forms.Label();
      this.trvTransitionsSubjects = new OgamaControls.CheckboxTreeView(this.components);
      this.groupBox20 = new System.Windows.Forms.GroupBox();
      this.rdbTransitionUseAOIGroups = new System.Windows.Forms.RadioButton();
      this.label2 = new System.Windows.Forms.Label();
      this.rdbTransitionsUseTrial = new System.Windows.Forms.RadioButton();
      this.tosTransitions = new System.Windows.Forms.ToolStrip();
      this.btnEye = new System.Windows.Forms.ToolStripButton();
      this.btnMouse = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
      this.btnTransitionsStartCalculation = new System.Windows.Forms.ToolStripButton();
      this.btnTransitionsAbortCalculation = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
      this.btnTransitionsExportTable = new System.Windows.Forms.ToolStripButton();
      this.bgwCalculateTransitions = new System.ComponentModel.BackgroundWorker();
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
      ((System.ComponentModel.ISupportInitialize)(this.dGVExportTable)).BeginInit();
      this.cmuDGV.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.nudTolerance)).BeginInit();
      this.groupBox10.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.dgvTransitions)).BeginInit();
      this.tacMeta.SuspendLayout();
      this.tbpMetaStandard.SuspendLayout();
      this.tscStandard.ContentPanel.SuspendLayout();
      this.tscStandard.TopToolStripPanel.SuspendLayout();
      this.tscStandard.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.spcTacDgv)).BeginInit();
      this.spcTacDgv.Panel1.SuspendLayout();
      this.spcTacDgv.Panel2.SuspendLayout();
      this.spcTacDgv.SuspendLayout();
      this.tacStandard.SuspendLayout();
      this.tbpSubject.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).BeginInit();
      this.splitContainer4.Panel1.SuspendLayout();
      this.splitContainer4.Panel2.SuspendLayout();
      this.splitContainer4.SuspendLayout();
      this.groupBox24.SuspendLayout();
      this.groupBox21.SuspendLayout();
      this.groupBox6.SuspendLayout();
      this.groupBox5.SuspendLayout();
      this.tbpTrial.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      this.tpbTrial.SuspendLayout();
      this.tpbTrialStandard.SuspendLayout();
      this.groupBox17.SuspendLayout();
      this.groupBox1.SuspendLayout();
      this.tbpTrialAdditional.SuspendLayout();
      this.groupBox2.SuspendLayout();
      this.groupBox3.SuspendLayout();
      this.groupBox4.SuspendLayout();
      this.tbpGaze.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
      this.splitContainer3.Panel1.SuspendLayout();
      this.splitContainer3.Panel2.SuspendLayout();
      this.splitContainer3.SuspendLayout();
      this.tacGazeParams.SuspendLayout();
      this.tbpGazeDefault.SuspendLayout();
      this.groupBox7.SuspendLayout();
      this.groupBox22.SuspendLayout();
      this.groupBox9.SuspendLayout();
      this.groupBox14.SuspendLayout();
      this.tbpGazeAOI.SuspendLayout();
      this.groupBox8.SuspendLayout();
      this.groupBox15.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.nudGazeTimeUntilNumberOf)).BeginInit();
      this.tbpGazeRegressions.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.nudGazeRegressionLineHeight)).BeginInit();
      this.tbpMouse.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer5)).BeginInit();
      this.splitContainer5.Panel1.SuspendLayout();
      this.splitContainer5.Panel2.SuspendLayout();
      this.splitContainer5.SuspendLayout();
      this.tacMouse.SuspendLayout();
      this.tbpMouseDefault.SuspendLayout();
      this.groupBox23.SuspendLayout();
      this.groupBox13.SuspendLayout();
      this.groupBox12.SuspendLayout();
      this.tbpMouseAOI.SuspendLayout();
      this.groupBox11.SuspendLayout();
      this.groupBox16.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.nudMouseNumberOf)).BeginInit();
      this.tbpOptions.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer6)).BeginInit();
      this.splitContainer6.Panel1.SuspendLayout();
      this.splitContainer6.Panel2.SuspendLayout();
      this.splitContainer6.SuspendLayout();
      this.tosStandard.SuspendLayout();
      this.tbpMetaTransitions.SuspendLayout();
      this.tscTransitions.ContentPanel.SuspendLayout();
      this.tscTransitions.TopToolStripPanel.SuspendLayout();
      this.tscTransitions.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
      this.splitContainer2.Panel1.SuspendLayout();
      this.splitContainer2.Panel2.SuspendLayout();
      this.splitContainer2.SuspendLayout();
      this.tableLayoutPanel1.SuspendLayout();
      this.groupBox19.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).BeginInit();
      this.groupBox18.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer7)).BeginInit();
      this.splitContainer7.Panel1.SuspendLayout();
      this.splitContainer7.Panel2.SuspendLayout();
      this.splitContainer7.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
      this.groupBox20.SuspendLayout();
      this.tosTransitions.SuspendLayout();
      this.SuspendLayout();
      // 
      // dGVExportTable
      // 
      this.dGVExportTable.AllowUserToOrderColumns = true;
      dataGridViewCellStyle1.NullValue = null;
      this.dGVExportTable.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
      dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
      dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
      dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
      dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
      this.dGVExportTable.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
      this.dGVExportTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dGVExportTable.ContextMenuStrip = this.cmuDGV;
      dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
      dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
      dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
      dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
      this.dGVExportTable.DefaultCellStyle = dataGridViewCellStyle3;
      this.dGVExportTable.Dock = System.Windows.Forms.DockStyle.Fill;
      this.dGVExportTable.Location = new System.Drawing.Point(0, 0);
      this.dGVExportTable.Name = "dGVExportTable";
      this.dGVExportTable.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
      this.dGVExportTable.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.dGVExportTable.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
      this.dGVExportTable.Size = new System.Drawing.Size(775, 82);
      this.dGVExportTable.TabIndex = 0;
      this.dGVExportTable.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dGVExportTable_MouseDown);
      // 
      // cmuDGV
      // 
      this.cmuDGV.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmuRecalculate,
            this.cmuRemoveColumn});
      this.cmuDGV.Name = "cmuDGV";
      this.cmuDGV.Size = new System.Drawing.Size(167, 48);
      // 
      // cmuRecalculate
      // 
      this.cmuRecalculate.Image = global::Ogama.Properties.Resources.CalculatorHS;
      this.cmuRecalculate.Name = "cmuRecalculate";
      this.cmuRecalculate.Size = new System.Drawing.Size(166, 22);
      this.cmuRecalculate.Text = "Recalculate Table";
      this.cmuRecalculate.Click += new System.EventHandler(this.cmuRecalculate_Click);
      // 
      // cmuRemoveColumn
      // 
      this.cmuRemoveColumn.Name = "cmuRemoveColumn";
      this.cmuRemoveColumn.Size = new System.Drawing.Size(166, 22);
      this.cmuRemoveColumn.Text = "Remove Column";
      this.cmuRemoveColumn.Click += new System.EventHandler(this.cmuRemoveColumn_Click);
      // 
      // chbSUBHandedness
      // 
      this.chbSUBHandedness.AutoSize = true;
      this.chbSUBHandedness.Location = new System.Drawing.Point(6, 113);
      this.chbSUBHandedness.Name = "chbSUBHandedness";
      this.chbSUBHandedness.Size = new System.Drawing.Size(86, 17);
      this.chbSUBHandedness.TabIndex = 5;
      this.chbSUBHandedness.Text = "Handedness";
      this.toolTipStatistic.SetToolTip(this.chbSUBHandedness, "e.g. left or right");
      this.chbSUBHandedness.UseVisualStyleBackColor = true;
      this.chbSUBHandedness.CheckedChanged += new System.EventHandler(this.chbSubjectDefault_CheckedChanged);
      // 
      // chbSUBAge
      // 
      this.chbSUBAge.AutoSize = true;
      this.chbSUBAge.Location = new System.Drawing.Point(6, 67);
      this.chbSUBAge.Name = "chbSUBAge";
      this.chbSUBAge.Size = new System.Drawing.Size(45, 17);
      this.chbSUBAge.TabIndex = 5;
      this.chbSUBAge.Text = "Age";
      this.toolTipStatistic.SetToolTip(this.chbSUBAge, "subjects age");
      this.chbSUBAge.UseVisualStyleBackColor = true;
      this.chbSUBAge.CheckedChanged += new System.EventHandler(this.chbSubjectDefault_CheckedChanged);
      // 
      // chbSUBComments
      // 
      this.chbSUBComments.AutoSize = true;
      this.chbSUBComments.Location = new System.Drawing.Point(6, 21);
      this.chbSUBComments.Name = "chbSUBComments";
      this.chbSUBComments.Size = new System.Drawing.Size(75, 17);
      this.chbSUBComments.TabIndex = 4;
      this.chbSUBComments.Text = "Comments";
      this.toolTipStatistic.SetToolTip(this.chbSUBComments, "Comments on subject");
      this.chbSUBComments.UseVisualStyleBackColor = true;
      this.chbSUBComments.CheckedChanged += new System.EventHandler(this.chbSubjectDefault_CheckedChanged);
      // 
      // chbSUBCategory
      // 
      this.chbSUBCategory.AutoSize = true;
      this.chbSUBCategory.Location = new System.Drawing.Point(6, 44);
      this.chbSUBCategory.Name = "chbSUBCategory";
      this.chbSUBCategory.Size = new System.Drawing.Size(68, 17);
      this.chbSUBCategory.TabIndex = 2;
      this.chbSUBCategory.Text = "Category";
      this.toolTipStatistic.SetToolTip(this.chbSUBCategory, "possible given subject categorie, e.g. beginner, professional");
      this.chbSUBCategory.UseVisualStyleBackColor = true;
      this.chbSUBCategory.CheckedChanged += new System.EventHandler(this.chbSubjectDefault_CheckedChanged);
      // 
      // chbSUBSex
      // 
      this.chbSUBSex.AutoSize = true;
      this.chbSUBSex.Location = new System.Drawing.Point(6, 90);
      this.chbSUBSex.Name = "chbSUBSex";
      this.chbSUBSex.Size = new System.Drawing.Size(44, 17);
      this.chbSUBSex.TabIndex = 1;
      this.chbSUBSex.Text = "Sex";
      this.toolTipStatistic.SetToolTip(this.chbSUBSex, "e.g. female or male");
      this.chbSUBSex.UseVisualStyleBackColor = true;
      this.chbSUBSex.CheckedChanged += new System.EventHandler(this.chbSubjectDefault_CheckedChanged);
      // 
      // chbSUBID
      // 
      this.chbSUBID.AutoSize = true;
      this.chbSUBID.Checked = true;
      this.chbSUBID.CheckState = System.Windows.Forms.CheckState.Checked;
      this.chbSUBID.Enabled = false;
      this.chbSUBID.Location = new System.Drawing.Point(6, 21);
      this.chbSUBID.Name = "chbSUBID";
      this.chbSUBID.Size = new System.Drawing.Size(54, 17);
      this.chbSUBID.TabIndex = 0;
      this.chbSUBID.Text = "Name";
      this.toolTipStatistic.SetToolTip(this.chbSUBID, "Subjects unique name.");
      this.chbSUBID.UseVisualStyleBackColor = true;
      this.chbSUBID.CheckedChanged += new System.EventHandler(this.chbSubjectDefault_CheckedChanged);
      // 
      // chbTRIEliminate
      // 
      this.chbTRIEliminate.Location = new System.Drawing.Point(6, 108);
      this.chbTRIEliminate.Name = "chbTRIEliminate";
      this.chbTRIEliminate.Size = new System.Drawing.Size(138, 30);
      this.chbTRIEliminate.TabIndex = 8;
      this.chbTRIEliminate.Text = "Eliminate y/n";
      this.toolTipStatistic.SetToolTip(this.chbTRIEliminate, "During the revision this trial seemed to be not \r\nreliable so it should been elim" +
        "inated from further analysis.\r\n\r\nvalue = 0, if trial tables column \"EliminateDat" +
        "a\"\r\nis null, otherwise value=1");
      this.chbTRIEliminate.UseVisualStyleBackColor = true;
      this.chbTRIEliminate.CheckedChanged += new System.EventHandler(this.chbTrialDefault_CheckedChanged);
      // 
      // chbTRIAOISize
      // 
      this.chbTRIAOISize.Location = new System.Drawing.Point(6, 44);
      this.chbTRIAOISize.Name = "chbTRIAOISize";
      this.chbTRIAOISize.Size = new System.Drawing.Size(138, 30);
      this.chbTRIAOISize.TabIndex = 7;
      this.chbTRIAOISize.Text = "AOI size\r\n(% of stimulus size)";
      this.toolTipStatistic.SetToolTip(this.chbTRIAOISize, "Area of all areas of interest,\r\nexcept those marked as \"SearchRect\",\r\nin percent " +
        "from screen size\r\n\r\nA value of -1 means:\r\nno AOIs defined\r\nA value of -2 means:\r" +
        "\nno AOIs except \"SearchRect\" defined.");
      this.chbTRIAOISize.UseVisualStyleBackColor = true;
      this.chbTRIAOISize.CheckedChanged += new System.EventHandler(this.chbTrialDefault_CheckedChanged);
      // 
      // chbTRITargetsize
      // 
      this.chbTRITargetsize.Location = new System.Drawing.Point(6, 12);
      this.chbTRITargetsize.Name = "chbTRITargetsize";
      this.chbTRITargetsize.Size = new System.Drawing.Size(138, 30);
      this.chbTRITargetsize.TabIndex = 6;
      this.chbTRITargetsize.Text = "Target AOI size \r\n(% of stimulus size)";
      this.toolTipStatistic.SetToolTip(this.chbTRITargetsize, "Area of all areas of interest, with the\r\n\"Target\" category,\r\nin percent from scre" +
        "en size\r\n\r\nA value of -1 means:\r\nno \"Target\" AOI defined");
      this.chbTRITargetsize.UseVisualStyleBackColor = true;
      this.chbTRITargetsize.CheckedChanged += new System.EventHandler(this.chbTrialDefault_CheckedChanged);
      // 
      // chbTRIDataLoss
      // 
      this.chbTRIDataLoss.Location = new System.Drawing.Point(6, 12);
      this.chbTRIDataLoss.Name = "chbTRIDataLoss";
      this.chbTRIDataLoss.Size = new System.Drawing.Size(138, 30);
      this.chbTRIDataLoss.TabIndex = 5;
      this.chbTRIDataLoss.Text = "Blank samples because of blinks (count)";
      this.toolTipStatistic.SetToolTip(this.chbTRIDataLoss, "Count of gaze samples, \r\nthat have a value of (0,0).\r\n\r\nA value of 0  means:\r\nNo " +
        "gaze data is lost through missing \r\ndetection or blinks.");
      this.chbTRIDataLoss.UseVisualStyleBackColor = true;
      this.chbTRIDataLoss.CheckedChanged += new System.EventHandler(this.chbTrialDefault_CheckedChanged);
      // 
      // chbTRIResponse
      // 
      this.chbTRIResponse.Location = new System.Drawing.Point(6, 12);
      this.chbTRIResponse.Name = "chbTRIResponse";
      this.chbTRIResponse.Size = new System.Drawing.Size(138, 30);
      this.chbTRIResponse.TabIndex = 3;
      this.chbTRIResponse.Text = "Response";
      this.toolTipStatistic.SetToolTip(this.chbTRIResponse, "A string with the event that stopped the trial (e.g. Mouse: Left)");
      this.chbTRIResponse.UseVisualStyleBackColor = true;
      this.chbTRIResponse.CheckedChanged += new System.EventHandler(this.chbTrialDefault_CheckedChanged);
      // 
      // chbTRIDuration
      // 
      this.chbTRIDuration.AutoSize = true;
      this.chbTRIDuration.Location = new System.Drawing.Point(6, 19);
      this.chbTRIDuration.Name = "chbTRIDuration";
      this.chbTRIDuration.Size = new System.Drawing.Size(66, 17);
      this.chbTRIDuration.TabIndex = 2;
      this.chbTRIDuration.Text = "Duration";
      this.toolTipStatistic.SetToolTip(this.chbTRIDuration, "trial duration in milliseconds");
      this.chbTRIDuration.UseVisualStyleBackColor = true;
      this.chbTRIDuration.CheckedChanged += new System.EventHandler(this.chbTrialDefault_CheckedChanged);
      // 
      // chbTRICategory
      // 
      this.chbTRICategory.AutoSize = true;
      this.chbTRICategory.Location = new System.Drawing.Point(6, 88);
      this.chbTRICategory.Name = "chbTRICategory";
      this.chbTRICategory.Size = new System.Drawing.Size(68, 17);
      this.chbTRICategory.TabIndex = 0;
      this.chbTRICategory.Text = "Category";
      this.toolTipStatistic.SetToolTip(this.chbTRICategory, "optional stimulus category, e.g. testing, practice, award");
      this.chbTRICategory.UseVisualStyleBackColor = true;
      this.chbTRICategory.CheckedChanged += new System.EventHandler(this.chbTrialDefault_CheckedChanged);
      // 
      // chbTRIName
      // 
      this.chbTRIName.AutoSize = true;
      this.chbTRIName.Location = new System.Drawing.Point(6, 65);
      this.chbTRIName.Name = "chbTRIName";
      this.chbTRIName.Size = new System.Drawing.Size(54, 17);
      this.chbTRIName.TabIndex = 1;
      this.chbTRIName.Text = "Name";
      this.toolTipStatistic.SetToolTip(this.chbTRIName, "Trial name");
      this.chbTRIName.UseVisualStyleBackColor = true;
      this.chbTRIName.CheckedChanged += new System.EventHandler(this.chbTrialDefault_CheckedChanged);
      // 
      // chbTRITrialID
      // 
      this.chbTRITrialID.AutoSize = true;
      this.chbTRITrialID.Location = new System.Drawing.Point(6, 42);
      this.chbTRITrialID.Name = "chbTRITrialID";
      this.chbTRITrialID.Size = new System.Drawing.Size(60, 17);
      this.chbTRITrialID.TabIndex = 0;
      this.chbTRITrialID.Text = "Trial ID";
      this.toolTipStatistic.SetToolTip(this.chbTRITrialID, "Unique trial identification");
      this.chbTRITrialID.UseVisualStyleBackColor = true;
      this.chbTRITrialID.CheckedChanged += new System.EventHandler(this.chbTrialDefault_CheckedChanged);
      // 
      // chbGAZFixTimeAtTarget
      // 
      this.chbGAZFixTimeAtTarget.Location = new System.Drawing.Point(6, 109);
      this.chbGAZFixTimeAtTarget.Name = "chbGAZFixTimeAtTarget";
      this.chbGAZFixTimeAtTarget.Size = new System.Drawing.Size(138, 30);
      this.chbGAZFixTimeAtTarget.TabIndex = 7;
      this.chbGAZFixTimeAtTarget.Text = "complete fixation time in target area.";
      this.toolTipStatistic.SetToolTip(this.chbGAZFixTimeAtTarget, "Sum of fixation time\r\nat the area of interest quoted\r\nin AOI interface as \"Target" +
        "\"\r\n\r\nA value of -1 means: \r\nno \"Target\" AOI defined.");
      this.chbGAZFixTimeAtTarget.UseVisualStyleBackColor = true;
      this.chbGAZFixTimeAtTarget.CheckedChanged += new System.EventHandler(this.chbGazeDefault_CheckedChanged);
      // 
      // chbGAZTime2SecondFixAtTarget
      // 
      this.chbGAZTime2SecondFixAtTarget.Location = new System.Drawing.Point(6, 77);
      this.chbGAZTime2SecondFixAtTarget.Name = "chbGAZTime2SecondFixAtTarget";
      this.chbGAZTime2SecondFixAtTarget.Size = new System.Drawing.Size(138, 30);
      this.chbGAZTime2SecondFixAtTarget.TabIndex = 6;
      this.chbGAZTime2SecondFixAtTarget.Text = "time until second fixation in target area.";
      this.toolTipStatistic.SetToolTip(this.chbGAZTime2SecondFixAtTarget, resources.GetString("chbGAZTime2SecondFixAtTarget.ToolTip"));
      this.chbGAZTime2SecondFixAtTarget.UseVisualStyleBackColor = true;
      this.chbGAZTime2SecondFixAtTarget.CheckedChanged += new System.EventHandler(this.chbGazeDefault_CheckedChanged);
      // 
      // chbGAZCountFix2FirstClick
      // 
      this.chbGAZCountFix2FirstClick.Location = new System.Drawing.Point(6, 13);
      this.chbGAZCountFix2FirstClick.Name = "chbGAZCountFix2FirstClick";
      this.chbGAZCountFix2FirstClick.Size = new System.Drawing.Size(138, 30);
      this.chbGAZCountFix2FirstClick.TabIndex = 5;
      this.chbGAZCountFix2FirstClick.Text = "fixations until first click (count)";
      this.toolTipStatistic.SetToolTip(this.chbGAZCountFix2FirstClick, "Number of fixations for each trial\r\nuntil first mouse click.\r\n\r\nA value of -1 mea" +
        "ns:\r\nNo left or right mouse click\r\nin this trial.");
      this.chbGAZCountFix2FirstClick.UseVisualStyleBackColor = true;
      this.chbGAZCountFix2FirstClick.CheckedChanged += new System.EventHandler(this.chbGazeDefault_CheckedChanged);
      // 
      // chbGAZTime2FirstFixAtTarget
      // 
      this.chbGAZTime2FirstFixAtTarget.Location = new System.Drawing.Point(6, 45);
      this.chbGAZTime2FirstFixAtTarget.Name = "chbGAZTime2FirstFixAtTarget";
      this.chbGAZTime2FirstFixAtTarget.Size = new System.Drawing.Size(138, 30);
      this.chbGAZTime2FirstFixAtTarget.TabIndex = 3;
      this.chbGAZTime2FirstFixAtTarget.Text = "time until first fixation in target area.";
      this.toolTipStatistic.SetToolTip(this.chbGAZTime2FirstFixAtTarget, resources.GetString("chbGAZTime2FirstFixAtTarget.ToolTip"));
      this.chbGAZTime2FirstFixAtTarget.UseVisualStyleBackColor = true;
      this.chbGAZTime2FirstFixAtTarget.CheckedChanged += new System.EventHandler(this.chbGazeDefault_CheckedChanged);
      // 
      // chbGAZTime2FirstFixInSearchRect
      // 
      this.chbGAZTime2FirstFixInSearchRect.Location = new System.Drawing.Point(6, 13);
      this.chbGAZTime2FirstFixInSearchRect.Name = "chbGAZTime2FirstFixInSearchRect";
      this.chbGAZTime2FirstFixInSearchRect.Size = new System.Drawing.Size(138, 30);
      this.chbGAZTime2FirstFixInSearchRect.TabIndex = 2;
      this.chbGAZTime2FirstFixInSearchRect.Text = "time until first fixation in searchrect area.";
      this.toolTipStatistic.SetToolTip(this.chbGAZTime2FirstFixInSearchRect, resources.GetString("chbGAZTime2FirstFixInSearchRect.ToolTip"));
      this.chbGAZTime2FirstFixInSearchRect.UseVisualStyleBackColor = true;
      this.chbGAZTime2FirstFixInSearchRect.CheckedChanged += new System.EventHandler(this.chbGazeDefault_CheckedChanged);
      // 
      // chbGAZFixDurationMean
      // 
      this.chbGAZFixDurationMean.Location = new System.Drawing.Point(6, 61);
      this.chbGAZFixDurationMean.Name = "chbGAZFixDurationMean";
      this.chbGAZFixDurationMean.Size = new System.Drawing.Size(138, 30);
      this.chbGAZFixDurationMean.TabIndex = 1;
      this.chbGAZFixDurationMean.Text = "fixation duration mean";
      this.toolTipStatistic.SetToolTip(this.chbGAZFixDurationMean, "Sum of all fixation durations \r\ndivided by fixation count.\r\n\r\nA value of -1 means" +
        ":\r\nNo fixation at all.");
      this.chbGAZFixDurationMean.UseVisualStyleBackColor = true;
      this.chbGAZFixDurationMean.CheckedChanged += new System.EventHandler(this.chbGazeDefault_CheckedChanged);
      // 
      // chbGAZFixations
      // 
      this.chbGAZFixations.Location = new System.Drawing.Point(6, 13);
      this.chbGAZFixations.Name = "chbGAZFixations";
      this.chbGAZFixations.Size = new System.Drawing.Size(138, 30);
      this.chbGAZFixations.TabIndex = 0;
      this.chbGAZFixations.Text = "fixations (count)";
      this.toolTipStatistic.SetToolTip(this.chbGAZFixations, "Number of fixations during each trial.");
      this.chbGAZFixations.UseVisualStyleBackColor = true;
      this.chbGAZFixations.CheckedChanged += new System.EventHandler(this.chbGazeDefault_CheckedChanged);
      // 
      // chbMSETime2FirstClick
      // 
      this.chbMSETime2FirstClick.Location = new System.Drawing.Point(6, 109);
      this.chbMSETime2FirstClick.Name = "chbMSETime2FirstClick";
      this.chbMSETime2FirstClick.Size = new System.Drawing.Size(138, 30);
      this.chbMSETime2FirstClick.TabIndex = 4;
      this.chbMSETime2FirstClick.Text = "time until first click.";
      this.toolTipStatistic.SetToolTip(this.chbMSETime2FirstClick, "Time until subjects first mouse click.\r\n\r\nA value of -1 means: \r\nmouse was never " +
        "clicked");
      this.chbMSETime2FirstClick.UseVisualStyleBackColor = true;
      this.chbMSETime2FirstClick.CheckedChanged += new System.EventHandler(this.chbMouseDefault_CheckedChanged);
      // 
      // chbMSETime2FirstFixAtTarget
      // 
      this.chbMSETime2FirstFixAtTarget.Location = new System.Drawing.Point(6, 45);
      this.chbMSETime2FirstFixAtTarget.Name = "chbMSETime2FirstFixAtTarget";
      this.chbMSETime2FirstFixAtTarget.Size = new System.Drawing.Size(138, 30);
      this.chbMSETime2FirstFixAtTarget.TabIndex = 3;
      this.chbMSETime2FirstFixAtTarget.Text = "time until first fixation in target area.";
      this.toolTipStatistic.SetToolTip(this.chbMSETime2FirstFixAtTarget, resources.GetString("chbMSETime2FirstFixAtTarget.ToolTip"));
      this.chbMSETime2FirstFixAtTarget.UseVisualStyleBackColor = true;
      this.chbMSETime2FirstFixAtTarget.CheckedChanged += new System.EventHandler(this.chbMouseDefault_CheckedChanged);
      // 
      // chbMSETime2FirstFixInSearchRect
      // 
      this.chbMSETime2FirstFixInSearchRect.Location = new System.Drawing.Point(6, 13);
      this.chbMSETime2FirstFixInSearchRect.Name = "chbMSETime2FirstFixInSearchRect";
      this.chbMSETime2FirstFixInSearchRect.Size = new System.Drawing.Size(138, 30);
      this.chbMSETime2FirstFixInSearchRect.TabIndex = 2;
      this.chbMSETime2FirstFixInSearchRect.Text = "time until first fixation in searchrect area.";
      this.toolTipStatistic.SetToolTip(this.chbMSETime2FirstFixInSearchRect, resources.GetString("chbMSETime2FirstFixInSearchRect.ToolTip"));
      this.chbMSETime2FirstFixInSearchRect.UseVisualStyleBackColor = true;
      this.chbMSETime2FirstFixInSearchRect.CheckedChanged += new System.EventHandler(this.chbMouseDefault_CheckedChanged);
      // 
      // chbMSEPathlength
      // 
      this.chbMSEPathlength.Location = new System.Drawing.Point(6, 13);
      this.chbMSEPathlength.Name = "chbMSEPathlength";
      this.chbMSEPathlength.Size = new System.Drawing.Size(138, 30);
      this.chbMSEPathlength.TabIndex = 1;
      this.chbMSEPathlength.Text = "pathlength (px)";
      this.toolTipStatistic.SetToolTip(this.chbMSEPathlength, "Length of the mouse path polyline in pixels.\r\n\r\nA value of -1 means:\r\nno mouse da" +
        "ta ");
      this.chbMSEPathlength.UseVisualStyleBackColor = true;
      this.chbMSEPathlength.CheckedChanged += new System.EventHandler(this.chbMouseDefault_CheckedChanged);
      // 
      // chbMSERightClicks
      // 
      this.chbMSERightClicks.Location = new System.Drawing.Point(6, 45);
      this.chbMSERightClicks.Name = "chbMSERightClicks";
      this.chbMSERightClicks.Size = new System.Drawing.Size(75, 30);
      this.chbMSERightClicks.TabIndex = 0;
      this.chbMSERightClicks.Text = "right click (count)";
      this.toolTipStatistic.SetToolTip(this.chbMSERightClicks, "Absolute number of right mouse clicks per trial.");
      this.chbMSERightClicks.UseVisualStyleBackColor = true;
      this.chbMSERightClicks.CheckedChanged += new System.EventHandler(this.chbMouseDefault_CheckedChanged);
      // 
      // chbMSELeftClicks
      // 
      this.chbMSELeftClicks.Location = new System.Drawing.Point(6, 13);
      this.chbMSELeftClicks.Name = "chbMSELeftClicks";
      this.chbMSELeftClicks.Size = new System.Drawing.Size(67, 30);
      this.chbMSELeftClicks.TabIndex = 0;
      this.chbMSELeftClicks.Text = "left click (count)";
      this.toolTipStatistic.SetToolTip(this.chbMSELeftClicks, "Absolute number of left mouse clicks per trial.");
      this.chbMSELeftClicks.UseVisualStyleBackColor = true;
      this.chbMSELeftClicks.CheckedChanged += new System.EventHandler(this.chbMouseDefault_CheckedChanged);
      // 
      // bgwCalculateStandards
      // 
      this.bgwCalculateStandards.WorkerReportsProgress = true;
      this.bgwCalculateStandards.WorkerSupportsCancellation = true;
      this.bgwCalculateStandards.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwCalculation_DoWork);
      this.bgwCalculateStandards.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgwCalculation_ProgressChanged);
      this.bgwCalculateStandards.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwCalculation_RunWorkerCompleted);
      // 
      // sfdExport
      // 
      this.sfdExport.DefaultExt = "txt";
      this.sfdExport.FileName = "*.txt";
      this.sfdExport.Filter = "Text files|*.txt";
      this.sfdExport.Title = "Select text file to save data...";
      // 
      // toolTipStatistic
      // 
      this.toolTipStatistic.AutomaticDelay = 200;
      this.toolTipStatistic.AutoPopDelay = 8000;
      this.toolTipStatistic.InitialDelay = 200;
      this.toolTipStatistic.ReshowDelay = 40;
      this.toolTipStatistic.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
      this.toolTipStatistic.ToolTipTitle = "Description:";
      // 
      // chbTRISamplesOutOfMonitor
      // 
      this.chbTRISamplesOutOfMonitor.Location = new System.Drawing.Point(6, 76);
      this.chbTRISamplesOutOfMonitor.Name = "chbTRISamplesOutOfMonitor";
      this.chbTRISamplesOutOfMonitor.Size = new System.Drawing.Size(138, 30);
      this.chbTRISamplesOutOfMonitor.TabIndex = 5;
      this.chbTRISamplesOutOfMonitor.Text = "Samples out of monitor (count)";
      this.toolTipStatistic.SetToolTip(this.chbTRISamplesOutOfMonitor, "Count of valid gaze samples, \r\nthat were out of screen area.");
      this.chbTRISamplesOutOfMonitor.UseVisualStyleBackColor = true;
      this.chbTRISamplesOutOfMonitor.CheckedChanged += new System.EventHandler(this.chbTrialDefault_CheckedChanged);
      // 
      // chbTRIDataLossPC
      // 
      this.chbTRIDataLossPC.Location = new System.Drawing.Point(6, 44);
      this.chbTRIDataLossPC.Name = "chbTRIDataLossPC";
      this.chbTRIDataLossPC.Size = new System.Drawing.Size(138, 30);
      this.chbTRIDataLossPC.TabIndex = 5;
      this.chbTRIDataLossPC.Text = "as foresaid\r\n(% of trial samples)";
      this.toolTipStatistic.SetToolTip(this.chbTRIDataLossPC, "as foresaid, but in percent of all data samples");
      this.chbTRIDataLossPC.UseVisualStyleBackColor = true;
      this.chbTRIDataLossPC.CheckedChanged += new System.EventHandler(this.chbTrialDefault_CheckedChanged);
      // 
      // chbTRISamplesOutOfMonitorPC
      // 
      this.chbTRISamplesOutOfMonitorPC.Location = new System.Drawing.Point(6, 108);
      this.chbTRISamplesOutOfMonitorPC.Name = "chbTRISamplesOutOfMonitorPC";
      this.chbTRISamplesOutOfMonitorPC.Size = new System.Drawing.Size(138, 30);
      this.chbTRISamplesOutOfMonitorPC.TabIndex = 5;
      this.chbTRISamplesOutOfMonitorPC.Text = "as foresaid\r\n(% of trial samples)";
      this.toolTipStatistic.SetToolTip(this.chbTRISamplesOutOfMonitorPC, "as foresaid, but in percent of all data samples");
      this.chbTRISamplesOutOfMonitorPC.UseVisualStyleBackColor = true;
      this.chbTRISamplesOutOfMonitorPC.CheckedChanged += new System.EventHandler(this.chbTrialDefault_CheckedChanged);
      // 
      // chbTRIStarttime
      // 
      this.chbTRIStarttime.AutoSize = true;
      this.chbTRIStarttime.Location = new System.Drawing.Point(6, 42);
      this.chbTRIStarttime.Name = "chbTRIStarttime";
      this.chbTRIStarttime.Size = new System.Drawing.Size(67, 17);
      this.chbTRIStarttime.TabIndex = 2;
      this.chbTRIStarttime.Text = "Starttime";
      this.toolTipStatistic.SetToolTip(this.chbTRIStarttime, "trial start time from the log files timing");
      this.chbTRIStarttime.UseVisualStyleBackColor = true;
      this.chbTRIStarttime.CheckedChanged += new System.EventHandler(this.chbTrialDefault_CheckedChanged);
      // 
      // chbGAZSaccadeLength
      // 
      this.chbGAZSaccadeLength.Location = new System.Drawing.Point(6, 13);
      this.chbGAZSaccadeLength.Name = "chbGAZSaccadeLength";
      this.chbGAZSaccadeLength.Size = new System.Drawing.Size(138, 30);
      this.chbGAZSaccadeLength.TabIndex = 4;
      this.chbGAZSaccadeLength.Text = "average saccade length (px)";
      this.toolTipStatistic.SetToolTip(this.chbGAZSaccadeLength, "Mean of all lengths of the straight\r\nline connections between consecutive\r\nfixati" +
        "ons.\r\n\r\nA value of -1 means:\r\nno gaze data ");
      this.chbGAZSaccadeLength.UseVisualStyleBackColor = true;
      this.chbGAZSaccadeLength.CheckedChanged += new System.EventHandler(this.chbGazeDefault_CheckedChanged);
      // 
      // chbGAZSaccadeVelocity
      // 
      this.chbGAZSaccadeVelocity.Location = new System.Drawing.Point(6, 45);
      this.chbGAZSaccadeVelocity.Name = "chbGAZSaccadeVelocity";
      this.chbGAZSaccadeVelocity.Size = new System.Drawing.Size(138, 30);
      this.chbGAZSaccadeVelocity.TabIndex = 4;
      this.chbGAZSaccadeVelocity.Text = "saccade velocity (px/s)";
      this.toolTipStatistic.SetToolTip(this.chbGAZSaccadeVelocity, "Average saccade velocity calculated as\r\nmean of all saccades length/timespan valu" +
        "es.\r\n\r\nA value of -1 means:\r\nno gaze data ");
      this.chbGAZSaccadeVelocity.UseVisualStyleBackColor = true;
      this.chbGAZSaccadeVelocity.CheckedChanged += new System.EventHandler(this.chbGazeDefault_CheckedChanged);
      // 
      // chbGAZFixationsPS
      // 
      this.chbGAZFixationsPS.Location = new System.Drawing.Point(6, 36);
      this.chbGAZFixationsPS.Name = "chbGAZFixationsPS";
      this.chbGAZFixationsPS.Size = new System.Drawing.Size(138, 30);
      this.chbGAZFixationsPS.TabIndex = 0;
      this.chbGAZFixationsPS.Text = "fixations (/s)";
      this.toolTipStatistic.SetToolTip(this.chbGAZFixationsPS, "as foresaid but per second");
      this.chbGAZFixationsPS.UseVisualStyleBackColor = true;
      this.chbGAZFixationsPS.CheckedChanged += new System.EventHandler(this.chbGazeDefault_CheckedChanged);
      // 
      // chbGAZFixationSaccadesRatio
      // 
      this.chbGAZFixationSaccadesRatio.Location = new System.Drawing.Point(6, 109);
      this.chbGAZFixationSaccadesRatio.Name = "chbGAZFixationSaccadesRatio";
      this.chbGAZFixationSaccadesRatio.Size = new System.Drawing.Size(138, 30);
      this.chbGAZFixationSaccadesRatio.TabIndex = 1;
      this.chbGAZFixationSaccadesRatio.Text = "fixations/saccades ratio";
      this.toolTipStatistic.SetToolTip(this.chbGAZFixationSaccadesRatio, "Sum of fixation times divided\r\nby trial duration.\r\n(OK. This is not every time a " +
        "real\r\nfixation - saccade ratio, but ...) ");
      this.chbGAZFixationSaccadesRatio.UseVisualStyleBackColor = true;
      this.chbGAZFixationSaccadesRatio.CheckedChanged += new System.EventHandler(this.chbGazeDefault_CheckedChanged);
      // 
      // chbMSEPathlengthPS
      // 
      this.chbMSEPathlengthPS.Location = new System.Drawing.Point(6, 45);
      this.chbMSEPathlengthPS.Name = "chbMSEPathlengthPS";
      this.chbMSEPathlengthPS.Size = new System.Drawing.Size(138, 30);
      this.chbMSEPathlengthPS.TabIndex = 1;
      this.chbMSEPathlengthPS.Text = "pathlength (px/s)";
      this.toolTipStatistic.SetToolTip(this.chbMSEPathlengthPS, "Mouse path velocity.\r\nThat is the length of the mouse \r\npath polyline in pixels p" +
        "er second.");
      this.chbMSEPathlengthPS.UseVisualStyleBackColor = true;
      this.chbMSEPathlengthPS.CheckedChanged += new System.EventHandler(this.chbMouseDefault_CheckedChanged);
      // 
      // chbMSERightClicksPS
      // 
      this.chbMSERightClicksPS.Location = new System.Drawing.Point(75, 45);
      this.chbMSERightClicksPS.Name = "chbMSERightClicksPS";
      this.chbMSERightClicksPS.Size = new System.Drawing.Size(75, 30);
      this.chbMSERightClicksPS.TabIndex = 0;
      this.chbMSERightClicksPS.Text = "right clicks (/s)";
      this.toolTipStatistic.SetToolTip(this.chbMSERightClicksPS, "as foresaid but per second");
      this.chbMSERightClicksPS.UseVisualStyleBackColor = true;
      this.chbMSERightClicksPS.CheckedChanged += new System.EventHandler(this.chbMouseDefault_CheckedChanged);
      // 
      // chbMSELeftClicksPS
      // 
      this.chbMSELeftClicksPS.Location = new System.Drawing.Point(75, 13);
      this.chbMSELeftClicksPS.Name = "chbMSELeftClicksPS";
      this.chbMSELeftClicksPS.Size = new System.Drawing.Size(70, 30);
      this.chbMSELeftClicksPS.TabIndex = 0;
      this.chbMSELeftClicksPS.Text = "left clicks (/s)";
      this.toolTipStatistic.SetToolTip(this.chbMSELeftClicksPS, "as foresaid but per second");
      this.chbMSELeftClicksPS.UseVisualStyleBackColor = true;
      this.chbMSELeftClicksPS.CheckedChanged += new System.EventHandler(this.chbMouseDefault_CheckedChanged);
      // 
      // nudTolerance
      // 
      this.nudTolerance.Location = new System.Drawing.Point(154, 17);
      this.nudTolerance.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
      this.nudTolerance.Name = "nudTolerance";
      this.nudTolerance.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
      this.nudTolerance.Size = new System.Drawing.Size(46, 20);
      this.nudTolerance.TabIndex = 10;
      this.nudTolerance.Tag = "";
      this.toolTipStatistic.SetToolTip(this.nudTolerance, resources.GetString("nudTolerance.ToolTip"));
      this.nudTolerance.ValueChanged += new System.EventHandler(this.nudTolerance_ValueChanged);
      // 
      // chbTRIResponseCorrectness
      // 
      this.chbTRIResponseCorrectness.Location = new System.Drawing.Point(6, 44);
      this.chbTRIResponseCorrectness.Name = "chbTRIResponseCorrectness";
      this.chbTRIResponseCorrectness.Size = new System.Drawing.Size(138, 30);
      this.chbTRIResponseCorrectness.TabIndex = 3;
      this.chbTRIResponseCorrectness.Text = "Correctness of response";
      this.toolTipStatistic.SetToolTip(this.chbTRIResponseCorrectness, "Did the subject decided correct ?\r\n 1 : correct answered\r\n 0 : wrong answered\r\n-1" +
        " : no testing conditions specified\r\n");
      this.chbTRIResponseCorrectness.UseVisualStyleBackColor = true;
      this.chbTRIResponseCorrectness.CheckedChanged += new System.EventHandler(this.chbTrialDefault_CheckedChanged);
      // 
      // groupBox10
      // 
      this.groupBox10.Controls.Add(this.chbMSERightClicksPS);
      this.groupBox10.Controls.Add(this.chbMSERightClicks);
      this.groupBox10.Controls.Add(this.chbMSELeftClicks);
      this.groupBox10.Controls.Add(this.chbMSECountFix2FirstClick);
      this.groupBox10.Controls.Add(this.chbMSETime2FirstClick);
      this.groupBox10.Controls.Add(this.chbMSELeftClicksPS);
      this.groupBox10.Location = new System.Drawing.Point(318, 6);
      this.groupBox10.Name = "groupBox10";
      this.groupBox10.Size = new System.Drawing.Size(150, 140);
      this.groupBox10.TabIndex = 15;
      this.groupBox10.TabStop = false;
      this.groupBox10.Text = "Mouse clicks";
      this.toolTipStatistic.SetToolTip(this.groupBox10, "To specify custom trigger strings in the response column for left and \r\nright mou" +
        "se clicks, please have a look at the options tab.");
      // 
      // chbMSECountFix2FirstClick
      // 
      this.chbMSECountFix2FirstClick.Location = new System.Drawing.Point(6, 77);
      this.chbMSECountFix2FirstClick.Name = "chbMSECountFix2FirstClick";
      this.chbMSECountFix2FirstClick.Size = new System.Drawing.Size(138, 30);
      this.chbMSECountFix2FirstClick.TabIndex = 5;
      this.chbMSECountFix2FirstClick.Text = "fixations until first click (count).";
      this.toolTipStatistic.SetToolTip(this.chbMSECountFix2FirstClick, "Number of fixations for each trial\r\nuntil first mouse click.\r\n\r\nA value of -1 mea" +
        "ns:\r\nNo left or right mouse click\r\nin this trial.");
      this.chbMSECountFix2FirstClick.UseVisualStyleBackColor = true;
      this.chbMSECountFix2FirstClick.CheckedChanged += new System.EventHandler(this.chbMouseDefault_CheckedChanged);
      // 
      // checkBox1
      // 
      this.checkBox1.AutoSize = true;
      this.checkBox1.Location = new System.Drawing.Point(6, 19);
      this.checkBox1.Name = "checkBox1";
      this.checkBox1.Size = new System.Drawing.Size(103, 17);
      this.checkBox1.TabIndex = 0;
      this.checkBox1.Text = "Fixations (count)";
      this.toolTipStatistic.SetToolTip(this.checkBox1, "Number of fixations during each trial.");
      this.checkBox1.UseVisualStyleBackColor = true;
      // 
      // checkBox2
      // 
      this.checkBox2.AutoSize = true;
      this.checkBox2.Location = new System.Drawing.Point(6, 38);
      this.checkBox2.Name = "checkBox2";
      this.checkBox2.Size = new System.Drawing.Size(86, 17);
      this.checkBox2.TabIndex = 0;
      this.checkBox2.Text = "Fixations (/s)";
      this.toolTipStatistic.SetToolTip(this.checkBox2, "as foresaid but per second");
      this.checkBox2.UseVisualStyleBackColor = true;
      // 
      // checkBox3
      // 
      this.checkBox3.Location = new System.Drawing.Point(6, 54);
      this.checkBox3.Name = "checkBox3";
      this.checkBox3.Size = new System.Drawing.Size(116, 30);
      this.checkBox3.TabIndex = 5;
      this.checkBox3.Text = "Fixations until first click (count)";
      this.toolTipStatistic.SetToolTip(this.checkBox3, "Number of fixations for each trial until first mouse click.");
      this.checkBox3.UseVisualStyleBackColor = true;
      // 
      // checkBox4
      // 
      this.checkBox4.Location = new System.Drawing.Point(6, 79);
      this.checkBox4.Name = "checkBox4";
      this.checkBox4.Size = new System.Drawing.Size(134, 35);
      this.checkBox4.TabIndex = 1;
      this.checkBox4.Text = "Average Fixation Duration";
      this.toolTipStatistic.SetToolTip(this.checkBox4, "Sum of all fixation durations \r\ndivided by fixation count");
      this.checkBox4.UseVisualStyleBackColor = true;
      // 
      // checkBox5
      // 
      this.checkBox5.AutoSize = true;
      this.checkBox5.Location = new System.Drawing.Point(6, 116);
      this.checkBox5.Name = "checkBox5";
      this.checkBox5.Size = new System.Drawing.Size(141, 17);
      this.checkBox5.TabIndex = 1;
      this.checkBox5.Text = "Fixations/saccades ratio";
      this.toolTipStatistic.SetToolTip(this.checkBox5, "Sum of fixation times divided\r\nby trial duration.\r\n(OK. This is not every time a " +
        "real\r\nfixation - saccade ratio, but ...) ");
      this.checkBox5.UseVisualStyleBackColor = true;
      // 
      // chbMSEFixations
      // 
      this.chbMSEFixations.Location = new System.Drawing.Point(6, 13);
      this.chbMSEFixations.Name = "chbMSEFixations";
      this.chbMSEFixations.Size = new System.Drawing.Size(138, 30);
      this.chbMSEFixations.TabIndex = 0;
      this.chbMSEFixations.Text = "fixations (count)";
      this.toolTipStatistic.SetToolTip(this.chbMSEFixations, "Number of fixations during each trial.");
      this.chbMSEFixations.UseVisualStyleBackColor = true;
      this.chbMSEFixations.CheckedChanged += new System.EventHandler(this.chbMouseDefault_CheckedChanged);
      // 
      // chbMSEFixationsPS
      // 
      this.chbMSEFixationsPS.Location = new System.Drawing.Point(6, 35);
      this.chbMSEFixationsPS.Name = "chbMSEFixationsPS";
      this.chbMSEFixationsPS.Size = new System.Drawing.Size(138, 30);
      this.chbMSEFixationsPS.TabIndex = 0;
      this.chbMSEFixationsPS.Text = "fixations (/s)";
      this.toolTipStatistic.SetToolTip(this.chbMSEFixationsPS, "as foresaid but per second");
      this.chbMSEFixationsPS.UseVisualStyleBackColor = true;
      this.chbMSEFixationsPS.CheckedChanged += new System.EventHandler(this.chbMouseDefault_CheckedChanged);
      // 
      // chbMSEFixDurationMean
      // 
      this.chbMSEFixDurationMean.Location = new System.Drawing.Point(6, 57);
      this.chbMSEFixDurationMean.Name = "chbMSEFixDurationMean";
      this.chbMSEFixDurationMean.Size = new System.Drawing.Size(138, 30);
      this.chbMSEFixDurationMean.TabIndex = 1;
      this.chbMSEFixDurationMean.Text = "fixation duration mean";
      this.toolTipStatistic.SetToolTip(this.chbMSEFixDurationMean, "Sum of all fixation durations \r\ndivided by fixation count.\r\n\r\nA value of -1 means" +
        ":\r\nNo fixation at all.");
      this.chbMSEFixDurationMean.UseVisualStyleBackColor = true;
      this.chbMSEFixDurationMean.CheckedChanged += new System.EventHandler(this.chbMouseDefault_CheckedChanged);
      // 
      // chbMSEFixationSaccadesRatio
      // 
      this.chbMSEFixationSaccadesRatio.Location = new System.Drawing.Point(6, 101);
      this.chbMSEFixationSaccadesRatio.Name = "chbMSEFixationSaccadesRatio";
      this.chbMSEFixationSaccadesRatio.Size = new System.Drawing.Size(138, 30);
      this.chbMSEFixationSaccadesRatio.TabIndex = 1;
      this.chbMSEFixationSaccadesRatio.Text = "fixations/saccades ratio";
      this.toolTipStatistic.SetToolTip(this.chbMSEFixationSaccadesRatio, "Sum of fixation times divided\r\nby trial duration.\r\n(OK. This is not every time a " +
        "real\r\nfixation - saccade ratio, but ...) ");
      this.chbMSEFixationSaccadesRatio.UseVisualStyleBackColor = true;
      this.chbMSEFixationSaccadesRatio.CheckedChanged += new System.EventHandler(this.chbMouseDefault_CheckedChanged);
      // 
      // chbMSETime2SecondFixAtTarget
      // 
      this.chbMSETime2SecondFixAtTarget.Location = new System.Drawing.Point(6, 77);
      this.chbMSETime2SecondFixAtTarget.Name = "chbMSETime2SecondFixAtTarget";
      this.chbMSETime2SecondFixAtTarget.Size = new System.Drawing.Size(138, 30);
      this.chbMSETime2SecondFixAtTarget.TabIndex = 8;
      this.chbMSETime2SecondFixAtTarget.Text = "time until second fixation in target area.";
      this.toolTipStatistic.SetToolTip(this.chbMSETime2SecondFixAtTarget, resources.GetString("chbMSETime2SecondFixAtTarget.ToolTip"));
      this.chbMSETime2SecondFixAtTarget.UseVisualStyleBackColor = true;
      this.chbMSETime2SecondFixAtTarget.CheckedChanged += new System.EventHandler(this.chbMouseDefault_CheckedChanged);
      // 
      // chbMSEFixTimeAtTarget
      // 
      this.chbMSEFixTimeAtTarget.Location = new System.Drawing.Point(6, 109);
      this.chbMSEFixTimeAtTarget.Name = "chbMSEFixTimeAtTarget";
      this.chbMSEFixTimeAtTarget.Size = new System.Drawing.Size(138, 30);
      this.chbMSEFixTimeAtTarget.TabIndex = 9;
      this.chbMSEFixTimeAtTarget.Text = "complete fixation time in target area.";
      this.toolTipStatistic.SetToolTip(this.chbMSEFixTimeAtTarget, "Sum of fixation time\r\nat the area of interest quoted\r\nin AOI interface as \"Target" +
        "\"\r\n\r\nA value of -1 means: \r\nno \"Target\" AOI defined.");
      this.chbMSEFixTimeAtTarget.UseVisualStyleBackColor = true;
      this.chbMSEFixTimeAtTarget.CheckedChanged += new System.EventHandler(this.chbMouseDefault_CheckedChanged);
      // 
      // chbMSEAverageDistance
      // 
      this.chbMSEAverageDistance.Location = new System.Drawing.Point(6, 45);
      this.chbMSEAverageDistance.Name = "chbMSEAverageDistance";
      this.chbMSEAverageDistance.Size = new System.Drawing.Size(138, 30);
      this.chbMSEAverageDistance.TabIndex = 6;
      this.chbMSEAverageDistance.Text = "average distance of gaze and mouse path";
      this.toolTipStatistic.SetToolTip(this.chbMSEAverageDistance, resources.GetString("chbMSEAverageDistance.ToolTip"));
      this.chbMSEAverageDistance.UseVisualStyleBackColor = true;
      this.chbMSEAverageDistance.CheckedChanged += new System.EventHandler(this.chbMouseDefault_CheckedChanged);
      // 
      // dgvTransitions
      // 
      this.dgvTransitions.AllowUserToOrderColumns = true;
      dataGridViewCellStyle4.NullValue = null;
      this.dgvTransitions.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
      dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
      dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
      dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
      dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
      this.dgvTransitions.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
      this.dgvTransitions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dgvTransitions.ContextMenuStrip = this.cmuDGV;
      dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
      dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
      dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
      dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
      this.dgvTransitions.DefaultCellStyle = dataGridViewCellStyle6;
      this.dgvTransitions.Dock = System.Windows.Forms.DockStyle.Fill;
      this.dgvTransitions.Location = new System.Drawing.Point(0, 0);
      this.dgvTransitions.Name = "dgvTransitions";
      this.dgvTransitions.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
      this.dgvTransitions.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.dgvTransitions.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
      this.dgvTransitions.Size = new System.Drawing.Size(775, 146);
      this.dgvTransitions.TabIndex = 1;
      this.toolTipStatistic.SetToolTip(this.dgvTransitions, "Number of fixations for each trial\r\nuntil first mouse click.\r\n\r\nA value of -1 mea" +
        "ns:\r\nNo left or right mouse click\r\nin this trial.");
      // 
      // chbTRIAOIofResponse
      // 
      this.chbTRIAOIofResponse.Location = new System.Drawing.Point(6, 80);
      this.chbTRIAOIofResponse.Name = "chbTRIAOIofResponse";
      this.chbTRIAOIofResponse.Size = new System.Drawing.Size(138, 30);
      this.chbTRIAOIofResponse.TabIndex = 3;
      this.chbTRIAOIofResponse.Text = "AOI of Response";
      this.toolTipStatistic.SetToolTip(this.chbTRIAOIofResponse, resources.GetString("chbTRIAOIofResponse.ToolTip"));
      this.chbTRIAOIofResponse.UseVisualStyleBackColor = true;
      this.chbTRIAOIofResponse.CheckedChanged += new System.EventHandler(this.chbTrialDefault_CheckedChanged);
      // 
      // chbTRISequence
      // 
      this.chbTRISequence.AutoSize = true;
      this.chbTRISequence.Location = new System.Drawing.Point(6, 19);
      this.chbTRISequence.Name = "chbTRISequence";
      this.chbTRISequence.Size = new System.Drawing.Size(75, 17);
      this.chbTRISequence.TabIndex = 1;
      this.chbTRISequence.Text = "Sequence";
      this.toolTipStatistic.SetToolTip(this.chbTRISequence, "The sequence number for the trial.");
      this.chbTRISequence.UseVisualStyleBackColor = true;
      this.chbTRISequence.CheckedChanged += new System.EventHandler(this.chbTrialDefault_CheckedChanged);
      // 
      // rdbGazeCompleteFixationTime
      // 
      this.rdbGazeCompleteFixationTime.AutoSize = true;
      this.rdbGazeCompleteFixationTime.Location = new System.Drawing.Point(6, 19);
      this.rdbGazeCompleteFixationTime.Name = "rdbGazeCompleteFixationTime";
      this.rdbGazeCompleteFixationTime.Size = new System.Drawing.Size(126, 17);
      this.rdbGazeCompleteFixationTime.TabIndex = 25;
      this.rdbGazeCompleteFixationTime.Text = "complete fixation time";
      this.toolTipStatistic.SetToolTip(this.rdbGazeCompleteFixationTime, "Sum of fixation time\r\nat the specified area of interest \r\nor area of interest gro" +
        "up\r\nin milliseconds.\r\n\r\nA value of -1 means: \r\nthis AOI/AOI group was not define" +
        "d.");
      this.rdbGazeCompleteFixationTime.UseVisualStyleBackColor = true;
      // 
      // rdbGazeTimeUntil
      // 
      this.rdbGazeTimeUntil.AutoSize = true;
      this.rdbGazeTimeUntil.Location = new System.Drawing.Point(6, 41);
      this.rdbGazeTimeUntil.Name = "rdbGazeTimeUntil";
      this.rdbGazeTimeUntil.Size = new System.Drawing.Size(66, 17);
      this.rdbGazeTimeUntil.TabIndex = 24;
      this.rdbGazeTimeUntil.Text = "time until";
      this.toolTipStatistic.SetToolTip(this.rdbGazeTimeUntil, resources.GetString("rdbGazeTimeUntil.ToolTip"));
      this.rdbGazeTimeUntil.UseVisualStyleBackColor = true;
      // 
      // rdbGazeFixationDurationMean
      // 
      this.rdbGazeFixationDurationMean.AutoSize = true;
      this.rdbGazeFixationDurationMean.Location = new System.Drawing.Point(6, 85);
      this.rdbGazeFixationDurationMean.Name = "rdbGazeFixationDurationMean";
      this.rdbGazeFixationDurationMean.Size = new System.Drawing.Size(128, 17);
      this.rdbGazeFixationDurationMean.TabIndex = 26;
      this.rdbGazeFixationDurationMean.Text = "fixation duration mean";
      this.toolTipStatistic.SetToolTip(this.rdbGazeFixationDurationMean, "Sum of all fixation durations \r\ndivided by fixation count at the\r\nspecific area o" +
        "f interest or\r\narea of interest group.\r\n\r\nA value of -1 means:\r\nNo fixation at a" +
        "ll.");
      this.rdbGazeFixationDurationMean.UseVisualStyleBackColor = true;
      // 
      // rdbGazeNumberOfFixations
      // 
      this.rdbGazeNumberOfFixations.AutoSize = true;
      this.rdbGazeNumberOfFixations.Checked = true;
      this.rdbGazeNumberOfFixations.Location = new System.Drawing.Point(6, 63);
      this.rdbGazeNumberOfFixations.Name = "rdbGazeNumberOfFixations";
      this.rdbGazeNumberOfFixations.Size = new System.Drawing.Size(113, 17);
      this.rdbGazeNumberOfFixations.TabIndex = 26;
      this.rdbGazeNumberOfFixations.TabStop = true;
      this.rdbGazeNumberOfFixations.Text = "number of fixations";
      this.toolTipStatistic.SetToolTip(this.rdbGazeNumberOfFixations, "Number of fixations\r\nat the specified area of interest \r\nor area of interest grou" +
        "p.\r\n\r\nA value of -1 means: \r\nthis AOI/AOI group was not defined.");
      this.rdbGazeNumberOfFixations.UseVisualStyleBackColor = true;
      // 
      // cbbGazeAOISingle
      // 
      this.cbbGazeAOISingle.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
      this.cbbGazeAOISingle.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
      this.cbbGazeAOISingle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbbGazeAOISingle.FormattingEnabled = true;
      this.cbbGazeAOISingle.Location = new System.Drawing.Point(415, 62);
      this.cbbGazeAOISingle.Name = "cbbGazeAOISingle";
      this.cbbGazeAOISingle.Size = new System.Drawing.Size(100, 21);
      this.cbbGazeAOISingle.TabIndex = 23;
      this.toolTipStatistic.SetToolTip(this.cbbGazeAOISingle, "Choose an area of interest.");
      this.cbbGazeAOISingle.DropDown += new System.EventHandler(this.cbbGazeAOISingle_DropDown);
      // 
      // cbbGazeAOIGroups
      // 
      this.cbbGazeAOIGroups.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
      this.cbbGazeAOIGroups.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
      this.cbbGazeAOIGroups.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbbGazeAOIGroups.FormattingEnabled = true;
      this.cbbGazeAOIGroups.Location = new System.Drawing.Point(415, 40);
      this.cbbGazeAOIGroups.Name = "cbbGazeAOIGroups";
      this.cbbGazeAOIGroups.Size = new System.Drawing.Size(100, 21);
      this.cbbGazeAOIGroups.TabIndex = 23;
      this.toolTipStatistic.SetToolTip(this.cbbGazeAOIGroups, "Choose an area of interest group.");
      this.cbbGazeAOIGroups.DropDown += new System.EventHandler(this.cbbGazeAOIGroups_DropDown);
      // 
      // rdbMouseClickAOI
      // 
      this.rdbMouseClickAOI.AutoSize = true;
      this.rdbMouseClickAOI.Location = new System.Drawing.Point(6, 118);
      this.rdbMouseClickAOI.Name = "rdbMouseClickAOI";
      this.rdbMouseClickAOI.Size = new System.Drawing.Size(100, 17);
      this.rdbMouseClickAOI.TabIndex = 30;
      this.rdbMouseClickAOI.Text = "clicks of button:";
      this.toolTipStatistic.SetToolTip(this.rdbMouseClickAOI, resources.GetString("rdbMouseClickAOI.ToolTip"));
      this.rdbMouseClickAOI.UseVisualStyleBackColor = true;
      // 
      // rdbMouseFixationDurationMean
      // 
      this.rdbMouseFixationDurationMean.AutoSize = true;
      this.rdbMouseFixationDurationMean.Location = new System.Drawing.Point(6, 79);
      this.rdbMouseFixationDurationMean.Name = "rdbMouseFixationDurationMean";
      this.rdbMouseFixationDurationMean.Size = new System.Drawing.Size(128, 17);
      this.rdbMouseFixationDurationMean.TabIndex = 30;
      this.rdbMouseFixationDurationMean.Text = "fixation duration mean";
      this.toolTipStatistic.SetToolTip(this.rdbMouseFixationDurationMean, "Sum of all fixation durations \r\ndivided by fixation count at the\r\nspecific area o" +
        "f interest or\r\narea of interest group.\r\n\r\nA value of -1 means:\r\nNo fixation at a" +
        "ll.\r\n");
      this.rdbMouseFixationDurationMean.UseVisualStyleBackColor = true;
      // 
      // rdbMouseNumberOfFixations
      // 
      this.rdbMouseNumberOfFixations.AutoSize = true;
      this.rdbMouseNumberOfFixations.Checked = true;
      this.rdbMouseNumberOfFixations.Location = new System.Drawing.Point(6, 59);
      this.rdbMouseNumberOfFixations.Name = "rdbMouseNumberOfFixations";
      this.rdbMouseNumberOfFixations.Size = new System.Drawing.Size(113, 17);
      this.rdbMouseNumberOfFixations.TabIndex = 26;
      this.rdbMouseNumberOfFixations.TabStop = true;
      this.rdbMouseNumberOfFixations.Text = "number of fixations";
      this.toolTipStatistic.SetToolTip(this.rdbMouseNumberOfFixations, "Number of fixations\r\nat the specified area of interest \r\nor area of interest grou" +
        "p.\r\n\r\nA value of -1 means: \r\nthis AOI/AOI group was not defined.");
      this.rdbMouseNumberOfFixations.UseVisualStyleBackColor = true;
      // 
      // rdbMouseCompleteFixationTime
      // 
      this.rdbMouseCompleteFixationTime.AutoSize = true;
      this.rdbMouseCompleteFixationTime.Location = new System.Drawing.Point(6, 19);
      this.rdbMouseCompleteFixationTime.Name = "rdbMouseCompleteFixationTime";
      this.rdbMouseCompleteFixationTime.Size = new System.Drawing.Size(126, 17);
      this.rdbMouseCompleteFixationTime.TabIndex = 25;
      this.rdbMouseCompleteFixationTime.Text = "complete fixation time";
      this.toolTipStatistic.SetToolTip(this.rdbMouseCompleteFixationTime, "Sum of fixation time\r\nat the specified area of interest \r\nor area of interest gro" +
        "up\r\nin milliseconds.\r\n\r\nA value of -1 means: \r\nthis AOI/AOI group was not define" +
        "d.\r\n");
      this.rdbMouseCompleteFixationTime.UseVisualStyleBackColor = true;
      // 
      // rdbMouseTimeUntil
      // 
      this.rdbMouseTimeUntil.AutoSize = true;
      this.rdbMouseTimeUntil.Location = new System.Drawing.Point(6, 39);
      this.rdbMouseTimeUntil.Name = "rdbMouseTimeUntil";
      this.rdbMouseTimeUntil.Size = new System.Drawing.Size(66, 17);
      this.rdbMouseTimeUntil.TabIndex = 24;
      this.rdbMouseTimeUntil.Text = "time until";
      this.toolTipStatistic.SetToolTip(this.rdbMouseTimeUntil, resources.GetString("rdbMouseTimeUntil.ToolTip"));
      this.rdbMouseTimeUntil.UseVisualStyleBackColor = true;
      // 
      // chbMSESaccadeLength
      // 
      this.chbMSESaccadeLength.Location = new System.Drawing.Point(6, 13);
      this.chbMSESaccadeLength.Name = "chbMSESaccadeLength";
      this.chbMSESaccadeLength.Size = new System.Drawing.Size(138, 30);
      this.chbMSESaccadeLength.TabIndex = 4;
      this.chbMSESaccadeLength.Text = "average saccade length (px)";
      this.toolTipStatistic.SetToolTip(this.chbMSESaccadeLength, "Mean of all lengths of the straight\r\nline connections between consecutive\r\nfixati" +
        "ons.\r\n\r\nA value of -1 means:\r\nno gaze data ");
      this.chbMSESaccadeLength.UseVisualStyleBackColor = true;
      this.chbMSESaccadeLength.CheckedChanged += new System.EventHandler(this.chbMouseDefault_CheckedChanged);
      // 
      // chbMSESaccadeVelocity
      // 
      this.chbMSESaccadeVelocity.Location = new System.Drawing.Point(6, 45);
      this.chbMSESaccadeVelocity.Name = "chbMSESaccadeVelocity";
      this.chbMSESaccadeVelocity.Size = new System.Drawing.Size(138, 30);
      this.chbMSESaccadeVelocity.TabIndex = 4;
      this.chbMSESaccadeVelocity.Text = "saccade velocity (px/s)";
      this.toolTipStatistic.SetToolTip(this.chbMSESaccadeVelocity, "Average saccade velocity calculated as\r\nmean of all saccades length/timespan valu" +
        "es.\r\n\r\nA value of -1 means:\r\nno gaze data ");
      this.chbMSESaccadeVelocity.UseVisualStyleBackColor = true;
      this.chbMSESaccadeVelocity.CheckedChanged += new System.EventHandler(this.chbMouseDefault_CheckedChanged);
      // 
      // chbGAZPathlength
      // 
      this.chbGAZPathlength.Location = new System.Drawing.Point(6, 13);
      this.chbGAZPathlength.Name = "chbGAZPathlength";
      this.chbGAZPathlength.Size = new System.Drawing.Size(138, 30);
      this.chbGAZPathlength.TabIndex = 4;
      this.chbGAZPathlength.Text = "pathlength between fixations (px)";
      this.toolTipStatistic.SetToolTip(this.chbGAZPathlength, "Length of the gaze fixation connections\r\nin pixels.\r\n\r\nA value of -1 means:\r\nno g" +
        "aze data available.");
      this.chbGAZPathlength.UseVisualStyleBackColor = true;
      this.chbGAZPathlength.CheckedChanged += new System.EventHandler(this.chbGazeDefault_CheckedChanged);
      // 
      // chbGAZPathlengthPS
      // 
      this.chbGAZPathlengthPS.Location = new System.Drawing.Point(6, 45);
      this.chbGAZPathlengthPS.Name = "chbGAZPathlengthPS";
      this.chbGAZPathlengthPS.Size = new System.Drawing.Size(138, 30);
      this.chbGAZPathlengthPS.TabIndex = 4;
      this.chbGAZPathlengthPS.Text = "pathlength between fixations (/s)";
      this.toolTipStatistic.SetToolTip(this.chbGAZPathlengthPS, "Gaze path velocity. \r\nThat is the length of the gaze fixation connections\r\nin pix" +
        "els per second.\r\n\r\nA value of -1 means:\r\nno gaze data available.");
      this.chbGAZPathlengthPS.UseVisualStyleBackColor = true;
      this.chbGAZPathlengthPS.CheckedChanged += new System.EventHandler(this.chbGazeDefault_CheckedChanged);
      // 
      // chbGAZFixDurationMedian
      // 
      this.chbGAZFixDurationMedian.Location = new System.Drawing.Point(6, 86);
      this.chbGAZFixDurationMedian.Name = "chbGAZFixDurationMedian";
      this.chbGAZFixDurationMedian.Size = new System.Drawing.Size(138, 30);
      this.chbGAZFixDurationMedian.TabIndex = 1;
      this.chbGAZFixDurationMedian.Text = "fixation duration median";
      this.toolTipStatistic.SetToolTip(this.chbGAZFixDurationMedian, "Median at 50 percentile.\r\n\r\nA value of -1 means:\r\nNo fixation at all.");
      this.chbGAZFixDurationMedian.UseVisualStyleBackColor = true;
      this.chbGAZFixDurationMedian.CheckedChanged += new System.EventHandler(this.chbGazeDefault_CheckedChanged);
      // 
      // chbMSEFixDurationMedian
      // 
      this.chbMSEFixDurationMedian.Location = new System.Drawing.Point(6, 79);
      this.chbMSEFixDurationMedian.Name = "chbMSEFixDurationMedian";
      this.chbMSEFixDurationMedian.Size = new System.Drawing.Size(138, 30);
      this.chbMSEFixDurationMedian.TabIndex = 2;
      this.chbMSEFixDurationMedian.Text = "fixation duration median";
      this.toolTipStatistic.SetToolTip(this.chbMSEFixDurationMedian, "Median at 50 percentile.\r\n\r\nA value of -1 means:\r\nNo fixation at all.");
      this.chbMSEFixDurationMedian.UseVisualStyleBackColor = true;
      this.chbMSEFixDurationMedian.CheckedChanged += new System.EventHandler(this.chbMouseDefault_CheckedChanged);
      // 
      // cbbGazeRegressionAOISingle
      // 
      this.cbbGazeRegressionAOISingle.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
      this.cbbGazeRegressionAOISingle.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
      this.cbbGazeRegressionAOISingle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbbGazeRegressionAOISingle.FormattingEnabled = true;
      this.cbbGazeRegressionAOISingle.Location = new System.Drawing.Point(102, 85);
      this.cbbGazeRegressionAOISingle.Name = "cbbGazeRegressionAOISingle";
      this.cbbGazeRegressionAOISingle.Size = new System.Drawing.Size(100, 21);
      this.cbbGazeRegressionAOISingle.TabIndex = 34;
      this.toolTipStatistic.SetToolTip(this.cbbGazeRegressionAOISingle, "Choose an area of interest.");
      this.cbbGazeRegressionAOISingle.DropDown += new System.EventHandler(this.cbbGazeRegressionAOISingle_DropDown);
      // 
      // cbbGazeRegressionAOIGroups
      // 
      this.cbbGazeRegressionAOIGroups.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
      this.cbbGazeRegressionAOIGroups.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
      this.cbbGazeRegressionAOIGroups.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbbGazeRegressionAOIGroups.FormattingEnabled = true;
      this.cbbGazeRegressionAOIGroups.Location = new System.Drawing.Point(102, 63);
      this.cbbGazeRegressionAOIGroups.Name = "cbbGazeRegressionAOIGroups";
      this.cbbGazeRegressionAOIGroups.Size = new System.Drawing.Size(100, 21);
      this.cbbGazeRegressionAOIGroups.TabIndex = 35;
      this.toolTipStatistic.SetToolTip(this.cbbGazeRegressionAOIGroups, "Choose an area of interest group.");
      this.cbbGazeRegressionAOIGroups.DropDown += new System.EventHandler(this.cbbGazeRegressionAOIGroups_DropDown);
      // 
      // tacMeta
      // 
      this.tacMeta.Controls.Add(this.tbpMetaStandard);
      this.tacMeta.Controls.Add(this.tbpMetaTransitions);
      this.tacMeta.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tacMeta.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tacMeta.ImageList = this.imlTabImages;
      this.tacMeta.ItemSize = new System.Drawing.Size(150, 30);
      this.tacMeta.Location = new System.Drawing.Point(5, 5);
      this.tacMeta.Name = "tacMeta";
      this.tacMeta.SelectedIndex = 0;
      this.tacMeta.Size = new System.Drawing.Size(789, 425);
      this.tacMeta.TabIndex = 17;
      // 
      // tbpMetaStandard
      // 
      this.tbpMetaStandard.BackColor = System.Drawing.SystemColors.Control;
      this.tbpMetaStandard.Controls.Add(this.tscStandard);
      this.tbpMetaStandard.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tbpMetaStandard.ImageKey = "Options";
      this.tbpMetaStandard.Location = new System.Drawing.Point(4, 34);
      this.tbpMetaStandard.Name = "tbpMetaStandard";
      this.tbpMetaStandard.Padding = new System.Windows.Forms.Padding(3);
      this.tbpMetaStandard.Size = new System.Drawing.Size(781, 387);
      this.tbpMetaStandard.TabIndex = 0;
      this.tbpMetaStandard.Text = "Standard variables";
      // 
      // tscStandard
      // 
      // 
      // tscStandard.ContentPanel
      // 
      this.tscStandard.ContentPanel.Controls.Add(this.spcTacDgv);
      this.tscStandard.ContentPanel.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
      this.tscStandard.ContentPanel.Size = new System.Drawing.Size(775, 356);
      this.tscStandard.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tscStandard.Location = new System.Drawing.Point(3, 3);
      this.tscStandard.Name = "tscStandard";
      this.tscStandard.Size = new System.Drawing.Size(775, 381);
      this.tscStandard.TabIndex = 0;
      this.tscStandard.Text = "toolStripContainer2";
      // 
      // tscStandard.TopToolStripPanel
      // 
      this.tscStandard.TopToolStripPanel.Controls.Add(this.tosStandard);
      // 
      // spcTacDgv
      // 
      this.spcTacDgv.Dock = System.Windows.Forms.DockStyle.Fill;
      this.spcTacDgv.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
      this.spcTacDgv.IsSplitterFixed = true;
      this.spcTacDgv.Location = new System.Drawing.Point(0, 5);
      this.spcTacDgv.Margin = new System.Windows.Forms.Padding(0);
      this.spcTacDgv.Name = "spcTacDgv";
      this.spcTacDgv.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // spcTacDgv.Panel1
      // 
      this.spcTacDgv.Panel1.Controls.Add(this.tacStandard);
      this.spcTacDgv.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
      // 
      // spcTacDgv.Panel2
      // 
      this.spcTacDgv.Panel2.Controls.Add(this.dGVExportTable);
      this.spcTacDgv.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.No;
      this.spcTacDgv.Size = new System.Drawing.Size(775, 351);
      this.spcTacDgv.SplitterDistance = 265;
      this.spcTacDgv.TabIndex = 15;
      // 
      // tacStandard
      // 
      this.tacStandard.Controls.Add(this.tbpSubject);
      this.tacStandard.Controls.Add(this.tbpTrial);
      this.tacStandard.Controls.Add(this.tbpGaze);
      this.tacStandard.Controls.Add(this.tbpMouse);
      this.tacStandard.Controls.Add(this.tbpOptions);
      this.tacStandard.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tacStandard.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tacStandard.ImageList = this.imlTabImages;
      this.tacStandard.Location = new System.Drawing.Point(0, 0);
      this.tacStandard.Margin = new System.Windows.Forms.Padding(0);
      this.tacStandard.Name = "tacStandard";
      this.tacStandard.SelectedIndex = 0;
      this.tacStandard.Size = new System.Drawing.Size(775, 265);
      this.tacStandard.TabIndex = 1;
      // 
      // tbpSubject
      // 
      this.tbpSubject.Controls.Add(this.splitContainer4);
      this.tbpSubject.ImageIndex = 0;
      this.tbpSubject.Location = new System.Drawing.Point(4, 23);
      this.tbpSubject.Name = "tbpSubject";
      this.tbpSubject.Size = new System.Drawing.Size(767, 238);
      this.tbpSubject.TabIndex = 0;
      this.tbpSubject.Text = "Subject information";
      this.tbpSubject.UseVisualStyleBackColor = true;
      // 
      // splitContainer4
      // 
      this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer4.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
      this.splitContainer4.IsSplitterFixed = true;
      this.splitContainer4.Location = new System.Drawing.Point(0, 0);
      this.splitContainer4.Name = "splitContainer4";
      this.splitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // splitContainer4.Panel1
      // 
      this.splitContainer4.Panel1.Controls.Add(this.dialogTop3);
      // 
      // splitContainer4.Panel2
      // 
      this.splitContainer4.Panel2.Controls.Add(this.groupBox24);
      this.splitContainer4.Panel2.Controls.Add(this.groupBox21);
      this.splitContainer4.Panel2.Controls.Add(this.groupBox6);
      this.splitContainer4.Panel2.Controls.Add(this.groupBox5);
      this.splitContainer4.Size = new System.Drawing.Size(767, 238);
      this.splitContainer4.SplitterDistance = 60;
      this.splitContainer4.TabIndex = 15;
      // 
      // dialogTop3
      // 
      this.dialogTop3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(237)))));
      this.dialogTop3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("dialogTop3.BackgroundImage")));
      this.dialogTop3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
      this.dialogTop3.Description = "Select the subjects that should be included in the calculation and select the sub" +
    "ject columns that should be exported to the statistics table.";
      this.dialogTop3.Dock = System.Windows.Forms.DockStyle.Fill;
      this.dialogTop3.Location = new System.Drawing.Point(0, 0);
      this.dialogTop3.Logo = global::Ogama.Properties.Resources.user1;
      this.dialogTop3.Name = "dialogTop3";
      this.dialogTop3.Size = new System.Drawing.Size(767, 60);
      this.dialogTop3.TabIndex = 0;
      // 
      // groupBox24
      // 
      this.groupBox24.Controls.Add(this.trvSubjects);
      this.groupBox24.Location = new System.Drawing.Point(3, 4);
      this.groupBox24.Name = "groupBox24";
      this.groupBox24.Size = new System.Drawing.Size(150, 152);
      this.groupBox24.TabIndex = 15;
      this.groupBox24.TabStop = false;
      this.groupBox24.Text = "Subjects";
      // 
      // trvSubjects
      // 
      this.trvSubjects.CheckBoxes = true;
      this.trvSubjects.Dock = System.Windows.Forms.DockStyle.Fill;
      this.trvSubjects.ImageIndex = 0;
      this.trvSubjects.ImageList = this.imlTreeViewSubjects;
      this.trvSubjects.Location = new System.Drawing.Point(3, 16);
      this.trvSubjects.Name = "trvSubjects";
      this.trvSubjects.SelectedImageIndex = 0;
      this.trvSubjects.Size = new System.Drawing.Size(144, 133);
      this.trvSubjects.TabIndex = 0;
      this.trvSubjects.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.trvSubjects_AfterCheck);
      // 
      // imlTreeViewSubjects
      // 
      this.imlTreeViewSubjects.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imlTreeViewSubjects.ImageStream")));
      this.imlTreeViewSubjects.TransparentColor = System.Drawing.Color.Transparent;
      this.imlTreeViewSubjects.Images.SetKeyName(0, "Categorie");
      this.imlTreeViewSubjects.Images.SetKeyName(1, "Subject");
      // 
      // groupBox21
      // 
      this.groupBox21.Controls.Add(this.clbSUBCustomparameters);
      this.groupBox21.Location = new System.Drawing.Point(471, 5);
      this.groupBox21.Name = "groupBox21";
      this.groupBox21.Size = new System.Drawing.Size(150, 151);
      this.groupBox21.TabIndex = 13;
      this.groupBox21.TabStop = false;
      this.groupBox21.Text = "custom parameters";
      // 
      // clbSUBCustomparameters
      // 
      this.clbSUBCustomparameters.Dock = System.Windows.Forms.DockStyle.Fill;
      this.clbSUBCustomparameters.FormattingEnabled = true;
      this.clbSUBCustomparameters.Location = new System.Drawing.Point(3, 16);
      this.clbSUBCustomparameters.Name = "clbSUBCustomparameters";
      this.clbSUBCustomparameters.Size = new System.Drawing.Size(144, 132);
      this.clbSUBCustomparameters.TabIndex = 0;
      this.clbSUBCustomparameters.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.clbSUBCustomparameters_ItemCheck);
      // 
      // groupBox6
      // 
      this.groupBox6.Controls.Add(this.chbSUBComments);
      this.groupBox6.Location = new System.Drawing.Point(315, 5);
      this.groupBox6.Name = "groupBox6";
      this.groupBox6.Size = new System.Drawing.Size(150, 151);
      this.groupBox6.TabIndex = 13;
      this.groupBox6.TabStop = false;
      this.groupBox6.Text = "further options";
      // 
      // groupBox5
      // 
      this.groupBox5.Controls.Add(this.chbSUBHandedness);
      this.groupBox5.Controls.Add(this.chbSUBAge);
      this.groupBox5.Controls.Add(this.chbSUBCategory);
      this.groupBox5.Controls.Add(this.chbSUBSex);
      this.groupBox5.Controls.Add(this.chbSUBID);
      this.groupBox5.Location = new System.Drawing.Point(159, 4);
      this.groupBox5.Name = "groupBox5";
      this.groupBox5.Size = new System.Drawing.Size(150, 152);
      this.groupBox5.TabIndex = 12;
      this.groupBox5.TabStop = false;
      this.groupBox5.Text = "name and properties";
      // 
      // tbpTrial
      // 
      this.tbpTrial.Controls.Add(this.splitContainer1);
      this.tbpTrial.ImageIndex = 1;
      this.tbpTrial.Location = new System.Drawing.Point(4, 23);
      this.tbpTrial.Name = "tbpTrial";
      this.tbpTrial.Size = new System.Drawing.Size(767, 238);
      this.tbpTrial.TabIndex = 1;
      this.tbpTrial.Text = "Trial information";
      this.tbpTrial.UseVisualStyleBackColor = true;
      // 
      // splitContainer1
      // 
      this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
      this.splitContainer1.IsSplitterFixed = true;
      this.splitContainer1.Location = new System.Drawing.Point(0, 0);
      this.splitContainer1.Margin = new System.Windows.Forms.Padding(0);
      this.splitContainer1.Name = "splitContainer1";
      this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // splitContainer1.Panel1
      // 
      this.splitContainer1.Panel1.Controls.Add(this.dialogTop1);
      this.splitContainer1.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
      this.splitContainer1.Panel1MinSize = 60;
      // 
      // splitContainer1.Panel2
      // 
      this.splitContainer1.Panel2.Controls.Add(this.tpbTrial);
      this.splitContainer1.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.No;
      this.splitContainer1.Size = new System.Drawing.Size(767, 238);
      this.splitContainer1.SplitterDistance = 60;
      this.splitContainer1.TabIndex = 19;
      // 
      // dialogTop1
      // 
      this.dialogTop1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(237)))));
      this.dialogTop1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("dialogTop1.BackgroundImage")));
      this.dialogTop1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
      this.dialogTop1.Description = "Select the trial parameter columns that should be exported to the statistics tabl" +
    "e.";
      this.dialogTop1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.dialogTop1.Location = new System.Drawing.Point(0, 0);
      this.dialogTop1.Logo = global::Ogama.Properties.Resources.ThumbnailLoadingHS;
      this.dialogTop1.Name = "dialogTop1";
      this.dialogTop1.Size = new System.Drawing.Size(150, 60);
      this.dialogTop1.TabIndex = 18;
      // 
      // tpbTrial
      // 
      this.tpbTrial.Controls.Add(this.tpbTrialStandard);
      this.tpbTrial.Controls.Add(this.tbpTrialAdditional);
      this.tpbTrial.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tpbTrial.Location = new System.Drawing.Point(0, 0);
      this.tpbTrial.Name = "tpbTrial";
      this.tpbTrial.SelectedIndex = 0;
      this.tpbTrial.Size = new System.Drawing.Size(767, 174);
      this.tpbTrial.TabIndex = 17;
      // 
      // tpbTrialStandard
      // 
      this.tpbTrialStandard.Controls.Add(this.groupBox17);
      this.tpbTrialStandard.Controls.Add(this.btnStandardDeselectAllTrials);
      this.tpbTrialStandard.Controls.Add(this.btnStandardSelectAllTrials);
      this.tpbTrialStandard.Controls.Add(this.trvTrialsDefault);
      this.tpbTrialStandard.Controls.Add(this.groupBox1);
      this.tpbTrialStandard.Location = new System.Drawing.Point(4, 22);
      this.tpbTrialStandard.Name = "tpbTrialStandard";
      this.tpbTrialStandard.Padding = new System.Windows.Forms.Padding(3);
      this.tpbTrialStandard.Size = new System.Drawing.Size(759, 148);
      this.tpbTrialStandard.TabIndex = 0;
      this.tpbTrialStandard.Text = "Default parameters";
      this.tpbTrialStandard.UseVisualStyleBackColor = true;
      // 
      // groupBox17
      // 
      this.groupBox17.Controls.Add(this.chbTRIDuration);
      this.groupBox17.Controls.Add(this.chbTRIStarttime);
      this.groupBox17.Location = new System.Drawing.Point(474, 6);
      this.groupBox17.Name = "groupBox17";
      this.groupBox17.Size = new System.Drawing.Size(150, 140);
      this.groupBox17.TabIndex = 25;
      this.groupBox17.TabStop = false;
      this.groupBox17.Text = "Timing";
      // 
      // btnStandardDeselectAllTrials
      // 
      this.btnStandardDeselectAllTrials.Location = new System.Drawing.Point(228, 40);
      this.btnStandardDeselectAllTrials.Name = "btnStandardDeselectAllTrials";
      this.btnStandardDeselectAllTrials.Size = new System.Drawing.Size(78, 23);
      this.btnStandardDeselectAllTrials.TabIndex = 23;
      this.btnStandardDeselectAllTrials.Text = "Deselect all";
      this.btnStandardDeselectAllTrials.UseVisualStyleBackColor = true;
      this.btnStandardDeselectAllTrials.Click += new System.EventHandler(this.btnStandardDeselectAllTrials_Click);
      // 
      // btnStandardSelectAllTrials
      // 
      this.btnStandardSelectAllTrials.Location = new System.Drawing.Point(228, 11);
      this.btnStandardSelectAllTrials.Name = "btnStandardSelectAllTrials";
      this.btnStandardSelectAllTrials.Size = new System.Drawing.Size(78, 23);
      this.btnStandardSelectAllTrials.TabIndex = 24;
      this.btnStandardSelectAllTrials.Text = "Select all";
      this.btnStandardSelectAllTrials.UseVisualStyleBackColor = true;
      this.btnStandardSelectAllTrials.Click += new System.EventHandler(this.btnStandardSelectAllTrials_Click);
      // 
      // trvTrialsDefault
      // 
      this.trvTrialsDefault.CheckBoxes = true;
      this.trvTrialsDefault.Location = new System.Drawing.Point(6, 11);
      this.trvTrialsDefault.Name = "trvTrialsDefault";
      this.trvTrialsDefault.Size = new System.Drawing.Size(216, 137);
      this.trvTrialsDefault.TabIndex = 3;
      this.trvTrialsDefault.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.trvTrialsDefault_AfterCheck);
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.chbTRICategory);
      this.groupBox1.Controls.Add(this.chbTRISequence);
      this.groupBox1.Controls.Add(this.chbTRIName);
      this.groupBox1.Controls.Add(this.chbTRITrialID);
      this.groupBox1.Location = new System.Drawing.Point(318, 6);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(150, 140);
      this.groupBox1.TabIndex = 13;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Identification";
      // 
      // tbpTrialAdditional
      // 
      this.tbpTrialAdditional.Controls.Add(this.groupBox2);
      this.tbpTrialAdditional.Controls.Add(this.groupBox3);
      this.tbpTrialAdditional.Controls.Add(this.groupBox4);
      this.tbpTrialAdditional.Location = new System.Drawing.Point(4, 22);
      this.tbpTrialAdditional.Name = "tbpTrialAdditional";
      this.tbpTrialAdditional.Padding = new System.Windows.Forms.Padding(3);
      this.tbpTrialAdditional.Size = new System.Drawing.Size(759, 148);
      this.tbpTrialAdditional.TabIndex = 1;
      this.tbpTrialAdditional.Text = "Additional parameters";
      this.tbpTrialAdditional.UseVisualStyleBackColor = true;
      // 
      // groupBox2
      // 
      this.groupBox2.Controls.Add(this.chbTRIAOISize);
      this.groupBox2.Controls.Add(this.chbTRITargetsize);
      this.groupBox2.Controls.Add(this.chbTRIAOIofResponse);
      this.groupBox2.Location = new System.Drawing.Point(6, 6);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new System.Drawing.Size(150, 140);
      this.groupBox2.TabIndex = 14;
      this.groupBox2.TabStop = false;
      this.groupBox2.Text = "Areas of interest";
      // 
      // groupBox3
      // 
      this.groupBox3.Controls.Add(this.chbTRIEliminate);
      this.groupBox3.Controls.Add(this.chbTRIResponseCorrectness);
      this.groupBox3.Controls.Add(this.chbTRIResponse);
      this.groupBox3.Location = new System.Drawing.Point(318, 6);
      this.groupBox3.Name = "groupBox3";
      this.groupBox3.Size = new System.Drawing.Size(150, 140);
      this.groupBox3.TabIndex = 15;
      this.groupBox3.TabStop = false;
      this.groupBox3.Text = "further options";
      // 
      // groupBox4
      // 
      this.groupBox4.Controls.Add(this.chbTRIDataLoss);
      this.groupBox4.Controls.Add(this.chbTRISamplesOutOfMonitor);
      this.groupBox4.Controls.Add(this.chbTRIDataLossPC);
      this.groupBox4.Controls.Add(this.chbTRISamplesOutOfMonitorPC);
      this.groupBox4.Location = new System.Drawing.Point(162, 6);
      this.groupBox4.Name = "groupBox4";
      this.groupBox4.Size = new System.Drawing.Size(150, 140);
      this.groupBox4.TabIndex = 16;
      this.groupBox4.TabStop = false;
      this.groupBox4.Text = "loss of data";
      // 
      // tbpGaze
      // 
      this.tbpGaze.Controls.Add(this.splitContainer3);
      this.tbpGaze.ImageIndex = 2;
      this.tbpGaze.Location = new System.Drawing.Point(4, 23);
      this.tbpGaze.Name = "tbpGaze";
      this.tbpGaze.Size = new System.Drawing.Size(767, 238);
      this.tbpGaze.TabIndex = 2;
      this.tbpGaze.Text = "Gaze parameters";
      this.tbpGaze.UseVisualStyleBackColor = true;
      // 
      // splitContainer3
      // 
      this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
      this.splitContainer3.IsSplitterFixed = true;
      this.splitContainer3.Location = new System.Drawing.Point(0, 0);
      this.splitContainer3.Margin = new System.Windows.Forms.Padding(0);
      this.splitContainer3.Name = "splitContainer3";
      this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // splitContainer3.Panel1
      // 
      this.splitContainer3.Panel1.Controls.Add(this.label8);
      this.splitContainer3.Panel1.Controls.Add(this.dialogTop2);
      // 
      // splitContainer3.Panel2
      // 
      this.splitContainer3.Panel2.Controls.Add(this.tacGazeParams);
      this.splitContainer3.Size = new System.Drawing.Size(767, 238);
      this.splitContainer3.SplitterDistance = 60;
      this.splitContainer3.TabIndex = 28;
      // 
      // label8
      // 
      this.label8.BackColor = System.Drawing.SystemColors.Control;
      this.label8.Location = new System.Drawing.Point(55, 30);
      this.label8.Name = "label8";
      this.label8.Size = new System.Drawing.Size(356, 30);
      this.label8.TabIndex = 17;
      this.label8.Text = "HINT: Most of these parameters need a fixations calculation which has to be done " +
    "before in the fixations interface.";
      // 
      // dialogTop2
      // 
      this.dialogTop2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(237)))));
      this.dialogTop2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("dialogTop2.BackgroundImage")));
      this.dialogTop2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
      this.dialogTop2.Description = "Select the gaze parameter columns that should be exported to the statistics table" +
    ".";
      this.dialogTop2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.dialogTop2.Location = new System.Drawing.Point(0, 0);
      this.dialogTop2.Logo = global::Ogama.Properties.Resources.FixationsLogo;
      this.dialogTop2.Margin = new System.Windows.Forms.Padding(0);
      this.dialogTop2.Name = "dialogTop2";
      this.dialogTop2.Size = new System.Drawing.Size(767, 60);
      this.dialogTop2.TabIndex = 29;
      // 
      // tacGazeParams
      // 
      this.tacGazeParams.Controls.Add(this.tbpGazeDefault);
      this.tacGazeParams.Controls.Add(this.tbpGazeAOI);
      this.tacGazeParams.Controls.Add(this.tbpGazeRegressions);
      this.tacGazeParams.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tacGazeParams.Location = new System.Drawing.Point(0, 0);
      this.tacGazeParams.Name = "tacGazeParams";
      this.tacGazeParams.SelectedIndex = 0;
      this.tacGazeParams.Size = new System.Drawing.Size(767, 174);
      this.tacGazeParams.TabIndex = 28;
      // 
      // tbpGazeDefault
      // 
      this.tbpGazeDefault.Controls.Add(this.groupBox7);
      this.tbpGazeDefault.Controls.Add(this.groupBox22);
      this.tbpGazeDefault.Controls.Add(this.groupBox9);
      this.tbpGazeDefault.Controls.Add(this.groupBox14);
      this.tbpGazeDefault.Location = new System.Drawing.Point(4, 22);
      this.tbpGazeDefault.Name = "tbpGazeDefault";
      this.tbpGazeDefault.Padding = new System.Windows.Forms.Padding(3);
      this.tbpGazeDefault.Size = new System.Drawing.Size(759, 148);
      this.tbpGazeDefault.TabIndex = 0;
      this.tbpGazeDefault.Text = "Default parameter";
      this.tbpGazeDefault.UseVisualStyleBackColor = true;
      // 
      // groupBox7
      // 
      this.groupBox7.Controls.Add(this.chbGAZFixations);
      this.groupBox7.Controls.Add(this.chbGAZFixationsPS);
      this.groupBox7.Controls.Add(this.chbGAZFixDurationMedian);
      this.groupBox7.Controls.Add(this.chbGAZFixDurationMean);
      this.groupBox7.Controls.Add(this.chbGAZFixationSaccadesRatio);
      this.groupBox7.Location = new System.Drawing.Point(6, 6);
      this.groupBox7.Name = "groupBox7";
      this.groupBox7.Size = new System.Drawing.Size(150, 140);
      this.groupBox7.TabIndex = 14;
      this.groupBox7.TabStop = false;
      this.groupBox7.Text = "Gaze fixations";
      // 
      // groupBox22
      // 
      this.groupBox22.Controls.Add(this.chbGAZPathlength);
      this.groupBox22.Controls.Add(this.chbGAZPathlengthPS);
      this.groupBox22.Location = new System.Drawing.Point(318, 6);
      this.groupBox22.Name = "groupBox22";
      this.groupBox22.Size = new System.Drawing.Size(150, 140);
      this.groupBox22.TabIndex = 16;
      this.groupBox22.TabStop = false;
      this.groupBox22.Text = "Gaze path";
      // 
      // groupBox9
      // 
      this.groupBox9.Controls.Add(this.chbGAZSaccadeLength);
      this.groupBox9.Controls.Add(this.chbGAZSaccadeVelocity);
      this.groupBox9.Location = new System.Drawing.Point(162, 6);
      this.groupBox9.Name = "groupBox9";
      this.groupBox9.Size = new System.Drawing.Size(150, 140);
      this.groupBox9.TabIndex = 16;
      this.groupBox9.TabStop = false;
      this.groupBox9.Text = "Gaze saccades";
      // 
      // groupBox14
      // 
      this.groupBox14.Controls.Add(this.chbMSEAverageDistance);
      this.groupBox14.Controls.Add(this.chbGAZCountFix2FirstClick);
      this.groupBox14.Location = new System.Drawing.Point(474, 6);
      this.groupBox14.Name = "groupBox14";
      this.groupBox14.Size = new System.Drawing.Size(150, 140);
      this.groupBox14.TabIndex = 18;
      this.groupBox14.TabStop = false;
      this.groupBox14.Text = "Gaze Mouse interaction";
      // 
      // tbpGazeAOI
      // 
      this.tbpGazeAOI.Controls.Add(this.groupBox8);
      this.tbpGazeAOI.Controls.Add(this.groupBox15);
      this.tbpGazeAOI.Location = new System.Drawing.Point(4, 22);
      this.tbpGazeAOI.Name = "tbpGazeAOI";
      this.tbpGazeAOI.Padding = new System.Windows.Forms.Padding(3);
      this.tbpGazeAOI.Size = new System.Drawing.Size(759, 148);
      this.tbpGazeAOI.TabIndex = 1;
      this.tbpGazeAOI.Text = "AOI parameter";
      this.tbpGazeAOI.UseVisualStyleBackColor = true;
      // 
      // groupBox8
      // 
      this.groupBox8.Controls.Add(this.chbGAZTime2FirstFixInSearchRect);
      this.groupBox8.Controls.Add(this.chbGAZTime2FirstFixAtTarget);
      this.groupBox8.Controls.Add(this.chbGAZTime2SecondFixAtTarget);
      this.groupBox8.Controls.Add(this.chbGAZFixTimeAtTarget);
      this.groupBox8.Location = new System.Drawing.Point(6, 6);
      this.groupBox8.Name = "groupBox8";
      this.groupBox8.Size = new System.Drawing.Size(150, 140);
      this.groupBox8.TabIndex = 15;
      this.groupBox8.TabStop = false;
      this.groupBox8.Text = "Gaze at AOI (predefined)";
      // 
      // groupBox15
      // 
      this.groupBox15.Controls.Add(this.rdbGazeAOISingle);
      this.groupBox15.Controls.Add(this.rdbGazeAOIGroup);
      this.groupBox15.Controls.Add(this.btnGazeAddCustomVariable);
      this.groupBox15.Controls.Add(this.nudGazeTimeUntilNumberOf);
      this.groupBox15.Controls.Add(this.rdbGazeSaccadeVelocity);
      this.groupBox15.Controls.Add(this.rdbGazeFixationDurationMedian);
      this.groupBox15.Controls.Add(this.rdbGazeSaccadeLength);
      this.groupBox15.Controls.Add(this.rdbGazeFixationDurationMean);
      this.groupBox15.Controls.Add(this.rdbGazeSaccadeDuration);
      this.groupBox15.Controls.Add(this.rdbGazeNumberOfFixations);
      this.groupBox15.Controls.Add(this.label11);
      this.groupBox15.Controls.Add(this.rdbGazeCompleteFixationTime);
      this.groupBox15.Controls.Add(this.rdbGazeTimeUntil);
      this.groupBox15.Controls.Add(this.cbbGazeAOISingle);
      this.groupBox15.Controls.Add(this.cbbGazeAOIGroups);
      this.groupBox15.Location = new System.Drawing.Point(162, 6);
      this.groupBox15.Name = "groupBox15";
      this.groupBox15.Size = new System.Drawing.Size(540, 140);
      this.groupBox15.TabIndex = 27;
      this.groupBox15.TabStop = false;
      this.groupBox15.Text = "Gaze at AOI (custom)";
      // 
      // rdbGazeAOISingle
      // 
      this.rdbGazeAOISingle.AutoCheck = false;
      this.rdbGazeAOISingle.AutoSize = true;
      this.rdbGazeAOISingle.Location = new System.Drawing.Point(323, 63);
      this.rdbGazeAOISingle.Name = "rdbGazeAOISingle";
      this.rdbGazeAOISingle.Size = new System.Drawing.Size(85, 17);
      this.rdbGazeAOISingle.TabIndex = 33;
      this.rdbGazeAOISingle.Text = "at single AOI";
      this.rdbGazeAOISingle.UseVisualStyleBackColor = true;
      this.rdbGazeAOISingle.Click += new System.EventHandler(this.rdbGazeAOISingle_Click);
      // 
      // rdbGazeAOIGroup
      // 
      this.rdbGazeAOIGroup.AutoCheck = false;
      this.rdbGazeAOIGroup.AutoSize = true;
      this.rdbGazeAOIGroup.Checked = true;
      this.rdbGazeAOIGroup.Location = new System.Drawing.Point(323, 41);
      this.rdbGazeAOIGroup.Name = "rdbGazeAOIGroup";
      this.rdbGazeAOIGroup.Size = new System.Drawing.Size(86, 17);
      this.rdbGazeAOIGroup.TabIndex = 32;
      this.rdbGazeAOIGroup.TabStop = true;
      this.rdbGazeAOIGroup.Text = "in AOI Group";
      this.rdbGazeAOIGroup.UseVisualStyleBackColor = true;
      this.rdbGazeAOIGroup.Click += new System.EventHandler(this.rdbGazeAOIGroup_Click);
      // 
      // btnGazeAddCustomVariable
      // 
      this.btnGazeAddCustomVariable.Location = new System.Drawing.Point(396, 112);
      this.btnGazeAddCustomVariable.Name = "btnGazeAddCustomVariable";
      this.btnGazeAddCustomVariable.Size = new System.Drawing.Size(138, 23);
      this.btnGazeAddCustomVariable.TabIndex = 27;
      this.btnGazeAddCustomVariable.Text = "Add this variable to list...";
      this.btnGazeAddCustomVariable.UseVisualStyleBackColor = true;
      this.btnGazeAddCustomVariable.Click += new System.EventHandler(this.btnGazeAddCustomVariable_Click);
      // 
      // nudGazeTimeUntilNumberOf
      // 
      this.nudGazeTimeUntilNumberOf.Location = new System.Drawing.Point(73, 41);
      this.nudGazeTimeUntilNumberOf.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nudGazeTimeUntilNumberOf.Name = "nudGazeTimeUntilNumberOf";
      this.nudGazeTimeUntilNumberOf.Size = new System.Drawing.Size(33, 20);
      this.nudGazeTimeUntilNumberOf.TabIndex = 20;
      this.nudGazeTimeUntilNumberOf.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
      // 
      // rdbGazeSaccadeVelocity
      // 
      this.rdbGazeSaccadeVelocity.AutoSize = true;
      this.rdbGazeSaccadeVelocity.Location = new System.Drawing.Point(162, 109);
      this.rdbGazeSaccadeVelocity.Name = "rdbGazeSaccadeVelocity";
      this.rdbGazeSaccadeVelocity.Size = new System.Drawing.Size(105, 17);
      this.rdbGazeSaccadeVelocity.TabIndex = 26;
      this.rdbGazeSaccadeVelocity.Text = "saccade velocity";
      this.rdbGazeSaccadeVelocity.UseVisualStyleBackColor = true;
      // 
      // rdbGazeFixationDurationMedian
      // 
      this.rdbGazeFixationDurationMedian.AutoSize = true;
      this.rdbGazeFixationDurationMedian.Location = new System.Drawing.Point(6, 109);
      this.rdbGazeFixationDurationMedian.Name = "rdbGazeFixationDurationMedian";
      this.rdbGazeFixationDurationMedian.Size = new System.Drawing.Size(136, 17);
      this.rdbGazeFixationDurationMedian.TabIndex = 26;
      this.rdbGazeFixationDurationMedian.Text = "fixation duration median";
      this.rdbGazeFixationDurationMedian.UseVisualStyleBackColor = true;
      // 
      // rdbGazeSaccadeLength
      // 
      this.rdbGazeSaccadeLength.AutoSize = true;
      this.rdbGazeSaccadeLength.Location = new System.Drawing.Point(162, 85);
      this.rdbGazeSaccadeLength.Name = "rdbGazeSaccadeLength";
      this.rdbGazeSaccadeLength.Size = new System.Drawing.Size(98, 17);
      this.rdbGazeSaccadeLength.TabIndex = 26;
      this.rdbGazeSaccadeLength.Text = "saccade length";
      this.rdbGazeSaccadeLength.UseVisualStyleBackColor = true;
      // 
      // rdbGazeSaccadeDuration
      // 
      this.rdbGazeSaccadeDuration.AutoSize = true;
      this.rdbGazeSaccadeDuration.Location = new System.Drawing.Point(162, 63);
      this.rdbGazeSaccadeDuration.Name = "rdbGazeSaccadeDuration";
      this.rdbGazeSaccadeDuration.Size = new System.Drawing.Size(107, 17);
      this.rdbGazeSaccadeDuration.TabIndex = 26;
      this.rdbGazeSaccadeDuration.Text = "saccade duration";
      this.rdbGazeSaccadeDuration.UseVisualStyleBackColor = true;
      // 
      // label11
      // 
      this.label11.AutoSize = true;
      this.label11.Location = new System.Drawing.Point(108, 43);
      this.label11.Name = "label11";
      this.label11.Size = new System.Drawing.Size(40, 13);
      this.label11.TabIndex = 21;
      this.label11.Text = "fixation";
      // 
      // tbpGazeRegressions
      // 
      this.tbpGazeRegressions.Controls.Add(this.label6);
      this.tbpGazeRegressions.Controls.Add(this.btnGazeAddCustomRegressionVariable);
      this.tbpGazeRegressions.Controls.Add(this.label5);
      this.tbpGazeRegressions.Controls.Add(this.nudGazeRegressionLineHeight);
      this.tbpGazeRegressions.Controls.Add(this.rdbGazeRegressionAOISingle);
      this.tbpGazeRegressions.Controls.Add(this.rdbGazeRegressionAll);
      this.tbpGazeRegressions.Controls.Add(this.rdbGazeRegressionAOIGroup);
      this.tbpGazeRegressions.Controls.Add(this.cbbGazeRegressionAOISingle);
      this.tbpGazeRegressions.Controls.Add(this.cbbGazeRegressionAOIGroups);
      this.tbpGazeRegressions.Location = new System.Drawing.Point(4, 22);
      this.tbpGazeRegressions.Name = "tbpGazeRegressions";
      this.tbpGazeRegressions.Padding = new System.Windows.Forms.Padding(3);
      this.tbpGazeRegressions.Size = new System.Drawing.Size(759, 148);
      this.tbpGazeRegressions.TabIndex = 2;
      this.tbpGazeRegressions.Text = "Regressions";
      this.tbpGazeRegressions.UseVisualStyleBackColor = true;
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Location = new System.Drawing.Point(6, 13);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(107, 13);
      this.label6.TabIndex = 41;
      this.label6.Text = "Calculate regressions";
      // 
      // btnGazeAddCustomRegressionVariable
      // 
      this.btnGazeAddCustomRegressionVariable.Location = new System.Drawing.Point(230, 109);
      this.btnGazeAddCustomRegressionVariable.Name = "btnGazeAddCustomRegressionVariable";
      this.btnGazeAddCustomRegressionVariable.Size = new System.Drawing.Size(138, 23);
      this.btnGazeAddCustomRegressionVariable.TabIndex = 40;
      this.btnGazeAddCustomRegressionVariable.Text = "Add this variable to list...";
      this.btnGazeAddCustomRegressionVariable.UseVisualStyleBackColor = true;
      this.btnGazeAddCustomRegressionVariable.Click += new System.EventHandler(this.btnGazeAddCustomRegressionVariable_Click);
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(51, 114);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(79, 13);
      this.label5.TabIndex = 39;
      this.label5.Text = "Line height (px)";
      // 
      // nudGazeRegressionLineHeight
      // 
      this.nudGazeRegressionLineHeight.Location = new System.Drawing.Point(138, 112);
      this.nudGazeRegressionLineHeight.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nudGazeRegressionLineHeight.Name = "nudGazeRegressionLineHeight";
      this.nudGazeRegressionLineHeight.Size = new System.Drawing.Size(64, 20);
      this.nudGazeRegressionLineHeight.TabIndex = 38;
      this.nudGazeRegressionLineHeight.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
      // 
      // rdbGazeRegressionAOISingle
      // 
      this.rdbGazeRegressionAOISingle.AutoSize = true;
      this.rdbGazeRegressionAOISingle.Location = new System.Drawing.Point(10, 86);
      this.rdbGazeRegressionAOISingle.Name = "rdbGazeRegressionAOISingle";
      this.rdbGazeRegressionAOISingle.Size = new System.Drawing.Size(85, 17);
      this.rdbGazeRegressionAOISingle.TabIndex = 37;
      this.rdbGazeRegressionAOISingle.Text = "at single AOI";
      this.rdbGazeRegressionAOISingle.UseVisualStyleBackColor = true;
      // 
      // rdbGazeRegressionAll
      // 
      this.rdbGazeRegressionAll.AutoSize = true;
      this.rdbGazeRegressionAll.Checked = true;
      this.rdbGazeRegressionAll.Location = new System.Drawing.Point(10, 41);
      this.rdbGazeRegressionAll.Name = "rdbGazeRegressionAll";
      this.rdbGazeRegressionAll.Size = new System.Drawing.Size(56, 17);
      this.rdbGazeRegressionAll.TabIndex = 36;
      this.rdbGazeRegressionAll.TabStop = true;
      this.rdbGazeRegressionAll.Text = "overall";
      this.rdbGazeRegressionAll.UseVisualStyleBackColor = true;
      // 
      // rdbGazeRegressionAOIGroup
      // 
      this.rdbGazeRegressionAOIGroup.AutoSize = true;
      this.rdbGazeRegressionAOIGroup.Location = new System.Drawing.Point(10, 64);
      this.rdbGazeRegressionAOIGroup.Name = "rdbGazeRegressionAOIGroup";
      this.rdbGazeRegressionAOIGroup.Size = new System.Drawing.Size(86, 17);
      this.rdbGazeRegressionAOIGroup.TabIndex = 36;
      this.rdbGazeRegressionAOIGroup.Text = "in AOI Group";
      this.rdbGazeRegressionAOIGroup.UseVisualStyleBackColor = true;
      // 
      // tbpMouse
      // 
      this.tbpMouse.Controls.Add(this.splitContainer5);
      this.tbpMouse.ImageIndex = 3;
      this.tbpMouse.Location = new System.Drawing.Point(4, 23);
      this.tbpMouse.Name = "tbpMouse";
      this.tbpMouse.Size = new System.Drawing.Size(767, 238);
      this.tbpMouse.TabIndex = 3;
      this.tbpMouse.Text = "Mouse parameters";
      this.tbpMouse.UseVisualStyleBackColor = true;
      // 
      // splitContainer5
      // 
      this.splitContainer5.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer5.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
      this.splitContainer5.IsSplitterFixed = true;
      this.splitContainer5.Location = new System.Drawing.Point(0, 0);
      this.splitContainer5.Name = "splitContainer5";
      this.splitContainer5.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // splitContainer5.Panel1
      // 
      this.splitContainer5.Panel1.Controls.Add(this.label1);
      this.splitContainer5.Panel1.Controls.Add(this.dialogTop4);
      // 
      // splitContainer5.Panel2
      // 
      this.splitContainer5.Panel2.Controls.Add(this.tacMouse);
      this.splitContainer5.Size = new System.Drawing.Size(767, 238);
      this.splitContainer5.SplitterDistance = 60;
      this.splitContainer5.TabIndex = 31;
      // 
      // label1
      // 
      this.label1.BackColor = System.Drawing.SystemColors.Control;
      this.label1.Location = new System.Drawing.Point(57, 28);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(356, 30);
      this.label1.TabIndex = 31;
      this.label1.Text = "HINT: Most of these parameters need a fixations calculation which has to be done " +
    "before in the fixations interface.";
      // 
      // dialogTop4
      // 
      this.dialogTop4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(237)))));
      this.dialogTop4.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("dialogTop4.BackgroundImage")));
      this.dialogTop4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
      this.dialogTop4.Description = "Select the gaze parameter columns that should be exported to the statistics table" +
    ".";
      this.dialogTop4.Dock = System.Windows.Forms.DockStyle.Fill;
      this.dialogTop4.Location = new System.Drawing.Point(0, 0);
      this.dialogTop4.Logo = global::Ogama.Properties.Resources.Mouse;
      this.dialogTop4.Margin = new System.Windows.Forms.Padding(0);
      this.dialogTop4.Name = "dialogTop4";
      this.dialogTop4.Size = new System.Drawing.Size(767, 60);
      this.dialogTop4.TabIndex = 30;
      // 
      // tacMouse
      // 
      this.tacMouse.Controls.Add(this.tbpMouseDefault);
      this.tacMouse.Controls.Add(this.tbpMouseAOI);
      this.tacMouse.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tacMouse.Location = new System.Drawing.Point(0, 0);
      this.tacMouse.Name = "tacMouse";
      this.tacMouse.SelectedIndex = 0;
      this.tacMouse.Size = new System.Drawing.Size(767, 174);
      this.tacMouse.TabIndex = 0;
      // 
      // tbpMouseDefault
      // 
      this.tbpMouseDefault.Controls.Add(this.groupBox23);
      this.tbpMouseDefault.Controls.Add(this.groupBox13);
      this.tbpMouseDefault.Controls.Add(this.groupBox12);
      this.tbpMouseDefault.Controls.Add(this.groupBox10);
      this.tbpMouseDefault.Location = new System.Drawing.Point(4, 22);
      this.tbpMouseDefault.Name = "tbpMouseDefault";
      this.tbpMouseDefault.Padding = new System.Windows.Forms.Padding(3);
      this.tbpMouseDefault.Size = new System.Drawing.Size(759, 148);
      this.tbpMouseDefault.TabIndex = 0;
      this.tbpMouseDefault.Text = "Default parameter";
      this.tbpMouseDefault.UseVisualStyleBackColor = true;
      // 
      // groupBox23
      // 
      this.groupBox23.Controls.Add(this.chbMSESaccadeLength);
      this.groupBox23.Controls.Add(this.chbMSESaccadeVelocity);
      this.groupBox23.Location = new System.Drawing.Point(162, 6);
      this.groupBox23.Name = "groupBox23";
      this.groupBox23.Size = new System.Drawing.Size(150, 140);
      this.groupBox23.TabIndex = 19;
      this.groupBox23.TabStop = false;
      this.groupBox23.Text = "Mouse saccades";
      // 
      // groupBox13
      // 
      this.groupBox13.Controls.Add(this.chbMSEFixDurationMedian);
      this.groupBox13.Controls.Add(this.chbMSEFixations);
      this.groupBox13.Controls.Add(this.chbMSEFixationsPS);
      this.groupBox13.Controls.Add(this.chbMSEFixDurationMean);
      this.groupBox13.Controls.Add(this.chbMSEFixationSaccadesRatio);
      this.groupBox13.Location = new System.Drawing.Point(6, 6);
      this.groupBox13.Name = "groupBox13";
      this.groupBox13.Size = new System.Drawing.Size(150, 140);
      this.groupBox13.TabIndex = 18;
      this.groupBox13.TabStop = false;
      this.groupBox13.Text = "Mouse fixations";
      // 
      // groupBox12
      // 
      this.groupBox12.Controls.Add(this.chbMSEPathlength);
      this.groupBox12.Controls.Add(this.chbMSEPathlengthPS);
      this.groupBox12.Location = new System.Drawing.Point(474, 6);
      this.groupBox12.Name = "groupBox12";
      this.groupBox12.Size = new System.Drawing.Size(150, 140);
      this.groupBox12.TabIndex = 17;
      this.groupBox12.TabStop = false;
      this.groupBox12.Text = "Mouse path";
      // 
      // tbpMouseAOI
      // 
      this.tbpMouseAOI.Controls.Add(this.groupBox11);
      this.tbpMouseAOI.Controls.Add(this.groupBox16);
      this.tbpMouseAOI.Location = new System.Drawing.Point(4, 22);
      this.tbpMouseAOI.Name = "tbpMouseAOI";
      this.tbpMouseAOI.Padding = new System.Windows.Forms.Padding(3);
      this.tbpMouseAOI.Size = new System.Drawing.Size(759, 148);
      this.tbpMouseAOI.TabIndex = 1;
      this.tbpMouseAOI.Text = "AOI parameter";
      this.tbpMouseAOI.UseVisualStyleBackColor = true;
      // 
      // groupBox11
      // 
      this.groupBox11.Controls.Add(this.chbMSETime2SecondFixAtTarget);
      this.groupBox11.Controls.Add(this.chbMSEFixTimeAtTarget);
      this.groupBox11.Controls.Add(this.chbMSETime2FirstFixInSearchRect);
      this.groupBox11.Controls.Add(this.chbMSETime2FirstFixAtTarget);
      this.groupBox11.Location = new System.Drawing.Point(6, 6);
      this.groupBox11.Name = "groupBox11";
      this.groupBox11.Size = new System.Drawing.Size(150, 140);
      this.groupBox11.TabIndex = 16;
      this.groupBox11.TabStop = false;
      this.groupBox11.Text = "Mouse at AOI (predefined)";
      // 
      // groupBox16
      // 
      this.groupBox16.Controls.Add(this.rdbMouseSaccadeVelocity);
      this.groupBox16.Controls.Add(this.rdbMouseSaccadeLength);
      this.groupBox16.Controls.Add(this.rdbMouseSaccadeDuration);
      this.groupBox16.Controls.Add(this.cbbMouseClickButton);
      this.groupBox16.Controls.Add(this.rdbMouseAOISingle);
      this.groupBox16.Controls.Add(this.rdbMouseAOIGroup);
      this.groupBox16.Controls.Add(this.rdbMouseClickAOI);
      this.groupBox16.Controls.Add(this.rdbMouseFixationDurationMedian);
      this.groupBox16.Controls.Add(this.rdbMouseFixationDurationMean);
      this.groupBox16.Controls.Add(this.cbbMouseAOISingle);
      this.groupBox16.Controls.Add(this.btnMouseAddCustomVariable);
      this.groupBox16.Controls.Add(this.nudMouseNumberOf);
      this.groupBox16.Controls.Add(this.rdbMouseNumberOfFixations);
      this.groupBox16.Controls.Add(this.label10);
      this.groupBox16.Controls.Add(this.rdbMouseCompleteFixationTime);
      this.groupBox16.Controls.Add(this.rdbMouseTimeUntil);
      this.groupBox16.Controls.Add(this.cbbMouseAOIGroup);
      this.groupBox16.Location = new System.Drawing.Point(162, 6);
      this.groupBox16.Name = "groupBox16";
      this.groupBox16.Size = new System.Drawing.Size(561, 140);
      this.groupBox16.TabIndex = 28;
      this.groupBox16.TabStop = false;
      this.groupBox16.Text = "Mouse at AOI (custom)";
      // 
      // rdbMouseSaccadeVelocity
      // 
      this.rdbMouseSaccadeVelocity.AutoSize = true;
      this.rdbMouseSaccadeVelocity.Location = new System.Drawing.Point(158, 59);
      this.rdbMouseSaccadeVelocity.Name = "rdbMouseSaccadeVelocity";
      this.rdbMouseSaccadeVelocity.Size = new System.Drawing.Size(105, 17);
      this.rdbMouseSaccadeVelocity.TabIndex = 35;
      this.rdbMouseSaccadeVelocity.Text = "saccade velocity";
      this.rdbMouseSaccadeVelocity.UseVisualStyleBackColor = true;
      // 
      // rdbMouseSaccadeLength
      // 
      this.rdbMouseSaccadeLength.AutoSize = true;
      this.rdbMouseSaccadeLength.Location = new System.Drawing.Point(158, 39);
      this.rdbMouseSaccadeLength.Name = "rdbMouseSaccadeLength";
      this.rdbMouseSaccadeLength.Size = new System.Drawing.Size(98, 17);
      this.rdbMouseSaccadeLength.TabIndex = 34;
      this.rdbMouseSaccadeLength.Text = "saccade length";
      this.rdbMouseSaccadeLength.UseVisualStyleBackColor = true;
      // 
      // rdbMouseSaccadeDuration
      // 
      this.rdbMouseSaccadeDuration.AutoSize = true;
      this.rdbMouseSaccadeDuration.Location = new System.Drawing.Point(158, 19);
      this.rdbMouseSaccadeDuration.Name = "rdbMouseSaccadeDuration";
      this.rdbMouseSaccadeDuration.Size = new System.Drawing.Size(107, 17);
      this.rdbMouseSaccadeDuration.TabIndex = 33;
      this.rdbMouseSaccadeDuration.Text = "saccade duration";
      this.rdbMouseSaccadeDuration.UseVisualStyleBackColor = true;
      // 
      // cbbMouseClickButton
      // 
      this.cbbMouseClickButton.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
      this.cbbMouseClickButton.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
      this.cbbMouseClickButton.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbbMouseClickButton.FormattingEnabled = true;
      this.cbbMouseClickButton.Location = new System.Drawing.Point(106, 117);
      this.cbbMouseClickButton.Name = "cbbMouseClickButton";
      this.cbbMouseClickButton.Size = new System.Drawing.Size(93, 21);
      this.cbbMouseClickButton.TabIndex = 32;
      // 
      // rdbMouseAOISingle
      // 
      this.rdbMouseAOISingle.AutoCheck = false;
      this.rdbMouseAOISingle.AutoSize = true;
      this.rdbMouseAOISingle.Location = new System.Drawing.Point(363, 60);
      this.rdbMouseAOISingle.Name = "rdbMouseAOISingle";
      this.rdbMouseAOISingle.Size = new System.Drawing.Size(85, 17);
      this.rdbMouseAOISingle.TabIndex = 31;
      this.rdbMouseAOISingle.Text = "at single AOI";
      this.rdbMouseAOISingle.UseVisualStyleBackColor = true;
      this.rdbMouseAOISingle.Click += new System.EventHandler(this.rdbMouseAOISingle_Click);
      // 
      // rdbMouseAOIGroup
      // 
      this.rdbMouseAOIGroup.AutoCheck = false;
      this.rdbMouseAOIGroup.AutoSize = true;
      this.rdbMouseAOIGroup.Checked = true;
      this.rdbMouseAOIGroup.Location = new System.Drawing.Point(363, 38);
      this.rdbMouseAOIGroup.Name = "rdbMouseAOIGroup";
      this.rdbMouseAOIGroup.Size = new System.Drawing.Size(86, 17);
      this.rdbMouseAOIGroup.TabIndex = 31;
      this.rdbMouseAOIGroup.TabStop = true;
      this.rdbMouseAOIGroup.Text = "in AOI Group";
      this.rdbMouseAOIGroup.UseVisualStyleBackColor = true;
      this.rdbMouseAOIGroup.Click += new System.EventHandler(this.rdbMouseAOIGroup_Click);
      // 
      // rdbMouseFixationDurationMedian
      // 
      this.rdbMouseFixationDurationMedian.AutoSize = true;
      this.rdbMouseFixationDurationMedian.Location = new System.Drawing.Point(6, 99);
      this.rdbMouseFixationDurationMedian.Name = "rdbMouseFixationDurationMedian";
      this.rdbMouseFixationDurationMedian.Size = new System.Drawing.Size(136, 17);
      this.rdbMouseFixationDurationMedian.TabIndex = 30;
      this.rdbMouseFixationDurationMedian.Text = "fixation duration median";
      this.rdbMouseFixationDurationMedian.UseVisualStyleBackColor = true;
      // 
      // cbbMouseAOISingle
      // 
      this.cbbMouseAOISingle.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
      this.cbbMouseAOISingle.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
      this.cbbMouseAOISingle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbbMouseAOISingle.FormattingEnabled = true;
      this.cbbMouseAOISingle.Location = new System.Drawing.Point(455, 59);
      this.cbbMouseAOISingle.Name = "cbbMouseAOISingle";
      this.cbbMouseAOISingle.Size = new System.Drawing.Size(100, 21);
      this.cbbMouseAOISingle.TabIndex = 29;
      this.cbbMouseAOISingle.DropDown += new System.EventHandler(this.cbbMouseAOISingle_DropDown);
      // 
      // btnMouseAddCustomVariable
      // 
      this.btnMouseAddCustomVariable.Location = new System.Drawing.Point(417, 109);
      this.btnMouseAddCustomVariable.Name = "btnMouseAddCustomVariable";
      this.btnMouseAddCustomVariable.Size = new System.Drawing.Size(138, 23);
      this.btnMouseAddCustomVariable.TabIndex = 27;
      this.btnMouseAddCustomVariable.Text = "Add this variable to list...";
      this.btnMouseAddCustomVariable.UseVisualStyleBackColor = true;
      this.btnMouseAddCustomVariable.Click += new System.EventHandler(this.btnMouseAddCustomVariable_Click);
      // 
      // nudMouseNumberOf
      // 
      this.nudMouseNumberOf.Location = new System.Drawing.Point(73, 41);
      this.nudMouseNumberOf.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nudMouseNumberOf.Name = "nudMouseNumberOf";
      this.nudMouseNumberOf.Size = new System.Drawing.Size(33, 20);
      this.nudMouseNumberOf.TabIndex = 20;
      this.nudMouseNumberOf.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
      // 
      // label10
      // 
      this.label10.AutoSize = true;
      this.label10.Location = new System.Drawing.Point(108, 43);
      this.label10.Name = "label10";
      this.label10.Size = new System.Drawing.Size(40, 13);
      this.label10.TabIndex = 21;
      this.label10.Text = "fixation";
      // 
      // cbbMouseAOIGroup
      // 
      this.cbbMouseAOIGroup.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
      this.cbbMouseAOIGroup.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
      this.cbbMouseAOIGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbbMouseAOIGroup.FormattingEnabled = true;
      this.cbbMouseAOIGroup.Location = new System.Drawing.Point(455, 37);
      this.cbbMouseAOIGroup.Name = "cbbMouseAOIGroup";
      this.cbbMouseAOIGroup.Size = new System.Drawing.Size(100, 21);
      this.cbbMouseAOIGroup.TabIndex = 23;
      this.cbbMouseAOIGroup.DropDown += new System.EventHandler(this.cbbMouseAOIGroup_DropDown);
      // 
      // tbpOptions
      // 
      this.tbpOptions.Controls.Add(this.splitContainer6);
      this.tbpOptions.ImageIndex = 4;
      this.tbpOptions.Location = new System.Drawing.Point(4, 23);
      this.tbpOptions.Name = "tbpOptions";
      this.tbpOptions.Size = new System.Drawing.Size(767, 238);
      this.tbpOptions.TabIndex = 4;
      this.tbpOptions.Text = "Options";
      this.tbpOptions.UseVisualStyleBackColor = true;
      // 
      // splitContainer6
      // 
      this.splitContainer6.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer6.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
      this.splitContainer6.IsSplitterFixed = true;
      this.splitContainer6.Location = new System.Drawing.Point(0, 0);
      this.splitContainer6.Name = "splitContainer6";
      this.splitContainer6.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // splitContainer6.Panel1
      // 
      this.splitContainer6.Panel1.Controls.Add(this.dialogTop5);
      // 
      // splitContainer6.Panel2
      // 
      this.splitContainer6.Panel2.Controls.Add(this.label3);
      this.splitContainer6.Panel2.Controls.Add(this.chb8CharacterRow);
      this.splitContainer6.Panel2.Controls.Add(this.label4);
      this.splitContainer6.Panel2.Controls.Add(this.nudTolerance);
      this.splitContainer6.Size = new System.Drawing.Size(767, 238);
      this.splitContainer6.SplitterDistance = 60;
      this.splitContainer6.TabIndex = 16;
      // 
      // dialogTop5
      // 
      this.dialogTop5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(237)))));
      this.dialogTop5.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("dialogTop5.BackgroundImage")));
      this.dialogTop5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
      this.dialogTop5.Description = "Specify options for the calculation.";
      this.dialogTop5.Dock = System.Windows.Forms.DockStyle.Fill;
      this.dialogTop5.Location = new System.Drawing.Point(0, 0);
      this.dialogTop5.Logo = global::Ogama.Properties.Resources.otheroptions;
      this.dialogTop5.Name = "dialogTop5";
      this.dialogTop5.Size = new System.Drawing.Size(767, 60);
      this.dialogTop5.TabIndex = 0;
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(12, 19);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(113, 13);
      this.label3.TabIndex = 11;
      this.label3.Text = "widen areas of interest";
      // 
      // chb8CharacterRow
      // 
      this.chb8CharacterRow.AutoSize = true;
      this.chb8CharacterRow.Location = new System.Drawing.Point(15, 49);
      this.chb8CharacterRow.Name = "chb8CharacterRow";
      this.chb8CharacterRow.Size = new System.Drawing.Size(340, 17);
      this.chb8CharacterRow.TabIndex = 15;
      this.chb8CharacterRow.Text = "Insert a row with 8 character column descriptions. (for compatibility)";
      this.chb8CharacterRow.UseVisualStyleBackColor = true;
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(206, 19);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(18, 13);
      this.label4.TabIndex = 12;
      this.label4.Text = "px";
      // 
      // imlTabImages
      // 
      this.imlTabImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imlTabImages.ImageStream")));
      this.imlTabImages.TransparentColor = System.Drawing.Color.Magenta;
      this.imlTabImages.Images.SetKeyName(0, "Subject");
      this.imlTabImages.Images.SetKeyName(1, "Trial");
      this.imlTabImages.Images.SetKeyName(2, "Gaze");
      this.imlTabImages.Images.SetKeyName(3, "Mouse");
      this.imlTabImages.Images.SetKeyName(4, "Options");
      this.imlTabImages.Images.SetKeyName(5, "AOI");
      // 
      // tosStandard
      // 
      this.tosStandard.Dock = System.Windows.Forms.DockStyle.None;
      this.tosStandard.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
      this.tosStandard.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnFillWithData,
            this.btnCancelCalculation,
            this.toolStripSeparator1,
            this.btnExport,
            this.btnSelectAll,
            this.toolStripSeparator3,
            this.btnHelp});
      this.tosStandard.Location = new System.Drawing.Point(0, 0);
      this.tosStandard.Name = "tosStandard";
      this.tosStandard.Padding = new System.Windows.Forms.Padding(0);
      this.tosStandard.Size = new System.Drawing.Size(775, 25);
      this.tosStandard.Stretch = true;
      this.tosStandard.TabIndex = 0;
      // 
      // btnFillWithData
      // 
      this.btnFillWithData.Image = global::Ogama.Properties.Resources.CalculatorHS;
      this.btnFillWithData.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnFillWithData.Name = "btnFillWithData";
      this.btnFillWithData.Size = new System.Drawing.Size(112, 22);
      this.btnFillWithData.Text = "Start calculation";
      this.btnFillWithData.ToolTipText = "Calculate selected statistical parameters.";
      this.btnFillWithData.Click += new System.EventHandler(this.btnFillWithData_Click);
      // 
      // btnCancelCalculation
      // 
      this.btnCancelCalculation.Image = global::Ogama.Properties.Resources.DeleteHS;
      this.btnCancelCalculation.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnCancelCalculation.Name = "btnCancelCalculation";
      this.btnCancelCalculation.Size = new System.Drawing.Size(118, 22);
      this.btnCancelCalculation.Text = "Abort calculation";
      this.btnCancelCalculation.ToolTipText = "Cancels calculation.";
      this.btnCancelCalculation.Click += new System.EventHandler(this.btnCancelCalculation_Click);
      // 
      // toolStripSeparator1
      // 
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
      // 
      // btnExport
      // 
      this.btnExport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnExport.Image = global::Ogama.Properties.Resources.ExportData;
      this.btnExport.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnExport.Name = "btnExport";
      this.btnExport.Size = new System.Drawing.Size(23, 22);
      this.btnExport.ToolTipText = "Export statistics table to file.";
      this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
      // 
      // btnSelectAll
      // 
      this.btnSelectAll.CheckOnClick = true;
      this.btnSelectAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnSelectAll.Image = global::Ogama.Properties.Resources.CheckBoxHS;
      this.btnSelectAll.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnSelectAll.Name = "btnSelectAll";
      this.btnSelectAll.Size = new System.Drawing.Size(23, 22);
      this.btnSelectAll.ToolTipText = "Select or deselect all checkboxes";
      this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
      // 
      // toolStripSeparator3
      // 
      this.toolStripSeparator3.Name = "toolStripSeparator3";
      this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
      // 
      // btnHelp
      // 
      this.btnHelp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnHelp.Image = global::Ogama.Properties.Resources.HelpBmp;
      this.btnHelp.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnHelp.Name = "btnHelp";
      this.btnHelp.Size = new System.Drawing.Size(23, 22);
      this.btnHelp.Text = "Show Help";
      // 
      // tbpMetaTransitions
      // 
      this.tbpMetaTransitions.BackColor = System.Drawing.SystemColors.Control;
      this.tbpMetaTransitions.Controls.Add(this.tscTransitions);
      this.tbpMetaTransitions.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tbpMetaTransitions.ImageKey = "AOI";
      this.tbpMetaTransitions.Location = new System.Drawing.Point(4, 34);
      this.tbpMetaTransitions.Name = "tbpMetaTransitions";
      this.tbpMetaTransitions.Padding = new System.Windows.Forms.Padding(3);
      this.tbpMetaTransitions.Size = new System.Drawing.Size(781, 387);
      this.tbpMetaTransitions.TabIndex = 1;
      this.tbpMetaTransitions.Text = "AOI transitions";
      // 
      // tscTransitions
      // 
      // 
      // tscTransitions.ContentPanel
      // 
      this.tscTransitions.ContentPanel.Controls.Add(this.splitContainer2);
      this.tscTransitions.ContentPanel.Size = new System.Drawing.Size(775, 356);
      this.tscTransitions.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tscTransitions.Location = new System.Drawing.Point(3, 3);
      this.tscTransitions.Margin = new System.Windows.Forms.Padding(0);
      this.tscTransitions.Name = "tscTransitions";
      this.tscTransitions.Size = new System.Drawing.Size(775, 381);
      this.tscTransitions.TabIndex = 3;
      this.tscTransitions.Text = "toolStripContainer3";
      // 
      // tscTransitions.TopToolStripPanel
      // 
      this.tscTransitions.TopToolStripPanel.Controls.Add(this.tosTransitions);
      // 
      // splitContainer2
      // 
      this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
      this.splitContainer2.IsSplitterFixed = true;
      this.splitContainer2.Location = new System.Drawing.Point(0, 0);
      this.splitContainer2.Name = "splitContainer2";
      this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // splitContainer2.Panel1
      // 
      this.splitContainer2.Panel1.Controls.Add(this.tableLayoutPanel1);
      // 
      // splitContainer2.Panel2
      // 
      this.splitContainer2.Panel2.Controls.Add(this.dgvTransitions);
      this.splitContainer2.Size = new System.Drawing.Size(775, 356);
      this.splitContainer2.SplitterDistance = 206;
      this.splitContainer2.TabIndex = 3;
      // 
      // tableLayoutPanel1
      // 
      this.tableLayoutPanel1.ColumnCount = 3;
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 230F));
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 270F));
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
      this.tableLayoutPanel1.Controls.Add(this.groupBox19, 0, 0);
      this.tableLayoutPanel1.Controls.Add(this.groupBox18, 0, 0);
      this.tableLayoutPanel1.Controls.Add(this.groupBox20, 0, 0);
      this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
      this.tableLayoutPanel1.Name = "tableLayoutPanel1";
      this.tableLayoutPanel1.RowCount = 1;
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel1.Size = new System.Drawing.Size(775, 206);
      this.tableLayoutPanel1.TabIndex = 23;
      // 
      // groupBox19
      // 
      this.groupBox19.Controls.Add(this.btnTransitionsDeselectAllTrials);
      this.groupBox19.Controls.Add(this.pictureBox7);
      this.groupBox19.Controls.Add(this.btnTransitionsSelectAllTrials);
      this.groupBox19.Controls.Add(this.label15);
      this.groupBox19.Controls.Add(this.trvTrialsAOI);
      this.groupBox19.Dock = System.Windows.Forms.DockStyle.Fill;
      this.groupBox19.Location = new System.Drawing.Point(503, 3);
      this.groupBox19.Name = "groupBox19";
      this.groupBox19.Size = new System.Drawing.Size(269, 200);
      this.groupBox19.TabIndex = 23;
      this.groupBox19.TabStop = false;
      this.groupBox19.Text = "Trials";
      // 
      // btnTransitionsDeselectAllTrials
      // 
      this.btnTransitionsDeselectAllTrials.Location = new System.Drawing.Point(185, 86);
      this.btnTransitionsDeselectAllTrials.Name = "btnTransitionsDeselectAllTrials";
      this.btnTransitionsDeselectAllTrials.Size = new System.Drawing.Size(70, 23);
      this.btnTransitionsDeselectAllTrials.TabIndex = 21;
      this.btnTransitionsDeselectAllTrials.Text = "Deselect all";
      this.btnTransitionsDeselectAllTrials.UseVisualStyleBackColor = true;
      this.btnTransitionsDeselectAllTrials.Click += new System.EventHandler(this.btnTransitionsDeselectAllTrials_Click);
      // 
      // pictureBox7
      // 
      this.pictureBox7.Image = global::Ogama.Properties.Resources.ThumbnailLoadingHS;
      this.pictureBox7.Location = new System.Drawing.Point(6, 17);
      this.pictureBox7.Name = "pictureBox7";
      this.pictureBox7.Size = new System.Drawing.Size(25, 25);
      this.pictureBox7.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
      this.pictureBox7.TabIndex = 20;
      this.pictureBox7.TabStop = false;
      // 
      // btnTransitionsSelectAllTrials
      // 
      this.btnTransitionsSelectAllTrials.Location = new System.Drawing.Point(185, 57);
      this.btnTransitionsSelectAllTrials.Name = "btnTransitionsSelectAllTrials";
      this.btnTransitionsSelectAllTrials.Size = new System.Drawing.Size(70, 23);
      this.btnTransitionsSelectAllTrials.TabIndex = 22;
      this.btnTransitionsSelectAllTrials.Text = "Select all";
      this.btnTransitionsSelectAllTrials.UseVisualStyleBackColor = true;
      this.btnTransitionsSelectAllTrials.Click += new System.EventHandler(this.btnTransitionsSelectAllTrials_Click);
      // 
      // label15
      // 
      this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label15.Location = new System.Drawing.Point(37, 17);
      this.label15.Name = "label15";
      this.label15.Size = new System.Drawing.Size(185, 37);
      this.label15.TabIndex = 19;
      this.label15.Text = "Check the trials that should be included in calculation.";
      // 
      // trvTrialsAOI
      // 
      this.trvTrialsAOI.CheckBoxes = true;
      this.trvTrialsAOI.Location = new System.Drawing.Point(6, 57);
      this.trvTrialsAOI.Name = "trvTrialsAOI";
      this.trvTrialsAOI.Size = new System.Drawing.Size(173, 140);
      this.trvTrialsAOI.TabIndex = 2;
      this.trvTrialsAOI.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.trvTrialsAOI_AfterCheck);
      // 
      // groupBox18
      // 
      this.groupBox18.Controls.Add(this.splitContainer7);
      this.groupBox18.Dock = System.Windows.Forms.DockStyle.Fill;
      this.groupBox18.Location = new System.Drawing.Point(233, 3);
      this.groupBox18.Name = "groupBox18";
      this.groupBox18.Size = new System.Drawing.Size(264, 200);
      this.groupBox18.TabIndex = 23;
      this.groupBox18.TabStop = false;
      this.groupBox18.Text = "Subjects";
      // 
      // splitContainer7
      // 
      this.splitContainer7.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer7.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
      this.splitContainer7.IsSplitterFixed = true;
      this.splitContainer7.Location = new System.Drawing.Point(3, 16);
      this.splitContainer7.Name = "splitContainer7";
      this.splitContainer7.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // splitContainer7.Panel1
      // 
      this.splitContainer7.Panel1.Controls.Add(this.pictureBox6);
      this.splitContainer7.Panel1.Controls.Add(this.label14);
      // 
      // splitContainer7.Panel2
      // 
      this.splitContainer7.Panel2.Controls.Add(this.trvTransitionsSubjects);
      this.splitContainer7.Size = new System.Drawing.Size(258, 181);
      this.splitContainer7.SplitterDistance = 40;
      this.splitContainer7.TabIndex = 21;
      // 
      // pictureBox6
      // 
      this.pictureBox6.Image = global::Ogama.Properties.Resources.users;
      this.pictureBox6.Location = new System.Drawing.Point(3, 3);
      this.pictureBox6.Name = "pictureBox6";
      this.pictureBox6.Size = new System.Drawing.Size(33, 37);
      this.pictureBox6.TabIndex = 18;
      this.pictureBox6.TabStop = false;
      // 
      // label14
      // 
      this.label14.AutoSize = true;
      this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label14.Location = new System.Drawing.Point(42, 7);
      this.label14.Name = "label14";
      this.label14.Size = new System.Drawing.Size(170, 26);
      this.label14.TabIndex = 19;
      this.label14.Text = "Select the subjects that should be \r\nincluded in calculation.";
      // 
      // trvTransitionsSubjects
      // 
      this.trvTransitionsSubjects.CheckBoxes = true;
      this.trvTransitionsSubjects.Dock = System.Windows.Forms.DockStyle.Fill;
      this.trvTransitionsSubjects.ImageIndex = 0;
      this.trvTransitionsSubjects.ImageList = this.imlTreeViewSubjects;
      this.trvTransitionsSubjects.Location = new System.Drawing.Point(0, 0);
      this.trvTransitionsSubjects.Name = "trvTransitionsSubjects";
      this.trvTransitionsSubjects.SelectedImageIndex = 0;
      this.trvTransitionsSubjects.Size = new System.Drawing.Size(258, 137);
      this.trvTransitionsSubjects.TabIndex = 20;
      this.trvTransitionsSubjects.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.trvSubjects_AfterCheck);
      // 
      // groupBox20
      // 
      this.groupBox20.Controls.Add(this.rdbTransitionUseAOIGroups);
      this.groupBox20.Controls.Add(this.label2);
      this.groupBox20.Controls.Add(this.rdbTransitionsUseTrial);
      this.groupBox20.Dock = System.Windows.Forms.DockStyle.Fill;
      this.groupBox20.Location = new System.Drawing.Point(3, 3);
      this.groupBox20.Name = "groupBox20";
      this.groupBox20.Size = new System.Drawing.Size(224, 200);
      this.groupBox20.TabIndex = 2;
      this.groupBox20.TabStop = false;
      this.groupBox20.Text = "Options";
      // 
      // rdbTransitionUseAOIGroups
      // 
      this.rdbTransitionUseAOIGroups.AutoSize = true;
      this.rdbTransitionUseAOIGroups.Checked = true;
      this.rdbTransitionUseAOIGroups.Location = new System.Drawing.Point(6, 77);
      this.rdbTransitionUseAOIGroups.Name = "rdbTransitionUseAOIGroups";
      this.rdbTransitionUseAOIGroups.Size = new System.Drawing.Size(214, 30);
      this.rdbTransitionUseAOIGroups.TabIndex = 0;
      this.rdbTransitionUseAOIGroups.TabStop = true;
      this.rdbTransitionUseAOIGroups.Text = "Calculate transition count between\r\nAOI Groups (sum over all checked trials)";
      this.rdbTransitionUseAOIGroups.UseVisualStyleBackColor = true;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label2.Location = new System.Drawing.Point(19, 28);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(135, 26);
      this.label2.TabIndex = 19;
      this.label2.Text = "Select the type of transition\r\ncalculation.";
      // 
      // rdbTransitionsUseTrial
      // 
      this.rdbTransitionsUseTrial.AutoSize = true;
      this.rdbTransitionsUseTrial.Location = new System.Drawing.Point(6, 113);
      this.rdbTransitionsUseTrial.Name = "rdbTransitionsUseTrial";
      this.rdbTransitionsUseTrial.Size = new System.Drawing.Size(192, 56);
      this.rdbTransitionsUseTrial.TabIndex = 1;
      this.rdbTransitionsUseTrial.Text = "Calculate transitions in selected trial\r\n(use all named AOIs)\r\nPlease note: check" +
    "ing state of \r\nthe trial list is ignored";
      this.rdbTransitionsUseTrial.UseVisualStyleBackColor = true;
      // 
      // tosTransitions
      // 
      this.tosTransitions.Dock = System.Windows.Forms.DockStyle.None;
      this.tosTransitions.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
      this.tosTransitions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnEye,
            this.btnMouse,
            this.toolStripSeparator4,
            this.btnTransitionsStartCalculation,
            this.btnTransitionsAbortCalculation,
            this.toolStripSeparator2,
            this.btnTransitionsExportTable});
      this.tosTransitions.Location = new System.Drawing.Point(0, 0);
      this.tosTransitions.Name = "tosTransitions";
      this.tosTransitions.Padding = new System.Windows.Forms.Padding(0);
      this.tosTransitions.Size = new System.Drawing.Size(775, 25);
      this.tosTransitions.Stretch = true;
      this.tosTransitions.TabIndex = 1;
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
      // toolStripSeparator4
      // 
      this.toolStripSeparator4.Name = "toolStripSeparator4";
      this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
      // 
      // btnTransitionsStartCalculation
      // 
      this.btnTransitionsStartCalculation.Image = global::Ogama.Properties.Resources.CalculatorHS;
      this.btnTransitionsStartCalculation.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnTransitionsStartCalculation.Name = "btnTransitionsStartCalculation";
      this.btnTransitionsStartCalculation.Size = new System.Drawing.Size(112, 22);
      this.btnTransitionsStartCalculation.Text = "Start calculation";
      this.btnTransitionsStartCalculation.ToolTipText = "Calculate selected statistical parameters.";
      this.btnTransitionsStartCalculation.Click += new System.EventHandler(this.btnTransitionsStartCalculation_Click);
      // 
      // btnTransitionsAbortCalculation
      // 
      this.btnTransitionsAbortCalculation.Image = global::Ogama.Properties.Resources.DeleteHS;
      this.btnTransitionsAbortCalculation.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnTransitionsAbortCalculation.Name = "btnTransitionsAbortCalculation";
      this.btnTransitionsAbortCalculation.Size = new System.Drawing.Size(118, 22);
      this.btnTransitionsAbortCalculation.Text = "Abort calculation";
      this.btnTransitionsAbortCalculation.ToolTipText = "Cancels calculation.";
      this.btnTransitionsAbortCalculation.Click += new System.EventHandler(this.btnTransitionsAbortCalculation_Click);
      // 
      // toolStripSeparator2
      // 
      this.toolStripSeparator2.Name = "toolStripSeparator2";
      this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
      // 
      // btnTransitionsExportTable
      // 
      this.btnTransitionsExportTable.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnTransitionsExportTable.Image = global::Ogama.Properties.Resources.ExportData;
      this.btnTransitionsExportTable.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnTransitionsExportTable.Name = "btnTransitionsExportTable";
      this.btnTransitionsExportTable.Size = new System.Drawing.Size(23, 22);
      this.btnTransitionsExportTable.ToolTipText = "Export statistics table to file.";
      this.btnTransitionsExportTable.Click += new System.EventHandler(this.btnTransitionsExportTable_Click);
      // 
      // bgwCalculateTransitions
      // 
      this.bgwCalculateTransitions.WorkerReportsProgress = true;
      this.bgwCalculateTransitions.WorkerSupportsCancellation = true;
      this.bgwCalculateTransitions.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwCalculateTransitions_DoWork);
      this.bgwCalculateTransitions.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgwCalculateTransitions_ProgressChanged);
      this.bgwCalculateTransitions.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwCalculateTransitions_RunWorkerCompleted);
      // 
      // StatisticsModule
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.SystemColors.Control;
      this.ClientSize = new System.Drawing.Size(799, 435);
      this.Controls.Add(this.tacMeta);
      this.DataBindings.Add(new System.Windows.Forms.Binding("Location", global::Ogama.Properties.Settings.Default, "StatisticsModuleLocation", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Location = global::Ogama.Properties.Settings.Default.StatisticsModuleLocation;
      this.Logo = global::Ogama.Properties.Resources.StatisticsLogo;
      this.Name = "StatisticsModule";
      this.Padding = new System.Windows.Forms.Padding(5);
      this.Text = "Statistics Module";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmStatistics_FormClosing);
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
      ((System.ComponentModel.ISupportInitialize)(this.dGVExportTable)).EndInit();
      this.cmuDGV.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.nudTolerance)).EndInit();
      this.groupBox10.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.dgvTransitions)).EndInit();
      this.tacMeta.ResumeLayout(false);
      this.tbpMetaStandard.ResumeLayout(false);
      this.tscStandard.ContentPanel.ResumeLayout(false);
      this.tscStandard.TopToolStripPanel.ResumeLayout(false);
      this.tscStandard.TopToolStripPanel.PerformLayout();
      this.tscStandard.ResumeLayout(false);
      this.tscStandard.PerformLayout();
      this.spcTacDgv.Panel1.ResumeLayout(false);
      this.spcTacDgv.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.spcTacDgv)).EndInit();
      this.spcTacDgv.ResumeLayout(false);
      this.tacStandard.ResumeLayout(false);
      this.tbpSubject.ResumeLayout(false);
      this.splitContainer4.Panel1.ResumeLayout(false);
      this.splitContainer4.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).EndInit();
      this.splitContainer4.ResumeLayout(false);
      this.groupBox24.ResumeLayout(false);
      this.groupBox21.ResumeLayout(false);
      this.groupBox6.ResumeLayout(false);
      this.groupBox6.PerformLayout();
      this.groupBox5.ResumeLayout(false);
      this.groupBox5.PerformLayout();
      this.tbpTrial.ResumeLayout(false);
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
      this.splitContainer1.ResumeLayout(false);
      this.tpbTrial.ResumeLayout(false);
      this.tpbTrialStandard.ResumeLayout(false);
      this.groupBox17.ResumeLayout(false);
      this.groupBox17.PerformLayout();
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.tbpTrialAdditional.ResumeLayout(false);
      this.groupBox2.ResumeLayout(false);
      this.groupBox3.ResumeLayout(false);
      this.groupBox4.ResumeLayout(false);
      this.tbpGaze.ResumeLayout(false);
      this.splitContainer3.Panel1.ResumeLayout(false);
      this.splitContainer3.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
      this.splitContainer3.ResumeLayout(false);
      this.tacGazeParams.ResumeLayout(false);
      this.tbpGazeDefault.ResumeLayout(false);
      this.groupBox7.ResumeLayout(false);
      this.groupBox22.ResumeLayout(false);
      this.groupBox9.ResumeLayout(false);
      this.groupBox14.ResumeLayout(false);
      this.tbpGazeAOI.ResumeLayout(false);
      this.groupBox8.ResumeLayout(false);
      this.groupBox15.ResumeLayout(false);
      this.groupBox15.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.nudGazeTimeUntilNumberOf)).EndInit();
      this.tbpGazeRegressions.ResumeLayout(false);
      this.tbpGazeRegressions.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.nudGazeRegressionLineHeight)).EndInit();
      this.tbpMouse.ResumeLayout(false);
      this.splitContainer5.Panel1.ResumeLayout(false);
      this.splitContainer5.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer5)).EndInit();
      this.splitContainer5.ResumeLayout(false);
      this.tacMouse.ResumeLayout(false);
      this.tbpMouseDefault.ResumeLayout(false);
      this.groupBox23.ResumeLayout(false);
      this.groupBox13.ResumeLayout(false);
      this.groupBox12.ResumeLayout(false);
      this.tbpMouseAOI.ResumeLayout(false);
      this.groupBox11.ResumeLayout(false);
      this.groupBox16.ResumeLayout(false);
      this.groupBox16.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.nudMouseNumberOf)).EndInit();
      this.tbpOptions.ResumeLayout(false);
      this.splitContainer6.Panel1.ResumeLayout(false);
      this.splitContainer6.Panel2.ResumeLayout(false);
      this.splitContainer6.Panel2.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer6)).EndInit();
      this.splitContainer6.ResumeLayout(false);
      this.tosStandard.ResumeLayout(false);
      this.tosStandard.PerformLayout();
      this.tbpMetaTransitions.ResumeLayout(false);
      this.tscTransitions.ContentPanel.ResumeLayout(false);
      this.tscTransitions.TopToolStripPanel.ResumeLayout(false);
      this.tscTransitions.TopToolStripPanel.PerformLayout();
      this.tscTransitions.ResumeLayout(false);
      this.tscTransitions.PerformLayout();
      this.splitContainer2.Panel1.ResumeLayout(false);
      this.splitContainer2.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
      this.splitContainer2.ResumeLayout(false);
      this.tableLayoutPanel1.ResumeLayout(false);
      this.groupBox19.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).EndInit();
      this.groupBox18.ResumeLayout(false);
      this.splitContainer7.Panel1.ResumeLayout(false);
      this.splitContainer7.Panel1.PerformLayout();
      this.splitContainer7.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer7)).EndInit();
      this.splitContainer7.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
      this.groupBox20.ResumeLayout(false);
      this.groupBox20.PerformLayout();
      this.tosTransitions.ResumeLayout(false);
      this.tosTransitions.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.CheckBox chbSUBSex;
    private System.Windows.Forms.CheckBox chbSUBID;
    private System.Windows.Forms.CheckBox chbSUBComments;
    private System.Windows.Forms.CheckBox chbSUBCategory;
    private System.Windows.Forms.CheckBox chbTRIDuration;
    private System.Windows.Forms.CheckBox chbTRIName;
    private System.Windows.Forms.CheckBox chbTRITrialID;
    private System.Windows.Forms.CheckBox chbGAZFixDurationMean;
    private System.Windows.Forms.CheckBox chbGAZTime2FirstFixInSearchRect;
    private System.Windows.Forms.CheckBox chbGAZTime2FirstFixAtTarget;
    private System.Windows.Forms.CheckBox chbMSEPathlength;
    private System.Windows.Forms.CheckBox chbMSELeftClicks;
    private System.Windows.Forms.CheckBox chbMSETime2FirstFixInSearchRect;
    private System.Windows.Forms.CheckBox chbTRICategory;
    private System.Windows.Forms.CheckBox chbTRIResponse;
    private System.Windows.Forms.CheckBox chbGAZFixations;
    private System.Windows.Forms.CheckBox chbMSETime2FirstFixAtTarget;
    private System.Windows.Forms.DataGridView dGVExportTable;
    private System.ComponentModel.BackgroundWorker bgwCalculateStandards;
    private System.Windows.Forms.CheckBox chbMSERightClicks;
    private System.Windows.Forms.SaveFileDialog sfdExport;
    private System.Windows.Forms.CheckBox chbTRIDataLoss;
    private System.Windows.Forms.CheckBox chbSUBAge;
    private System.Windows.Forms.CheckBox chbSUBHandedness;
    private System.Windows.Forms.CheckBox chbTRIAOISize;
    private System.Windows.Forms.CheckBox chbTRITargetsize;
    private System.Windows.Forms.ToolTip toolTipStatistic;
    private System.Windows.Forms.CheckBox chbMSETime2FirstClick;
    private System.Windows.Forms.CheckBox chbGAZCountFix2FirstClick;
    private System.Windows.Forms.CheckBox chbGAZFixTimeAtTarget;
    private System.Windows.Forms.CheckBox chbGAZTime2SecondFixAtTarget;
    private System.Windows.Forms.CheckBox chbTRIEliminate;
    private System.Windows.Forms.TabControl tacStandard;
    private System.Windows.Forms.TabPage tbpSubject;
    private System.Windows.Forms.TabPage tbpTrial;
    private System.Windows.Forms.TabPage tbpGaze;
    private System.Windows.Forms.TabPage tbpMouse;
    private System.Windows.Forms.TabPage tbpOptions;
    private System.Windows.Forms.NumericUpDown nudTolerance;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.ToolStrip tosStandard;
    private System.Windows.Forms.ToolStripButton btnFillWithData;
    private System.Windows.Forms.ToolStripButton btnExport;
    private System.Windows.Forms.ToolStripButton btnSelectAll;
    private System.Windows.Forms.CheckBox chbTRIDataLossPC;
    private System.Windows.Forms.CheckBox chbTRISamplesOutOfMonitorPC;
    private System.Windows.Forms.CheckBox chbTRISamplesOutOfMonitor;
    private System.Windows.Forms.CheckBox chbGAZSaccadeVelocity;
    private System.Windows.Forms.CheckBox chbGAZFixationsPS;
    private System.Windows.Forms.CheckBox chbGAZFixationSaccadesRatio;
    private System.Windows.Forms.CheckBox chbMSELeftClicksPS;
    private System.Windows.Forms.CheckBox chbMSERightClicksPS;
    private System.Windows.Forms.CheckBox chbMSEPathlengthPS;
    private System.Windows.Forms.ImageList imlTabImages;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.CheckBox chbTRIStarttime;
    private System.Windows.Forms.GroupBox groupBox4;
    private System.Windows.Forms.GroupBox groupBox3;
    private System.Windows.Forms.GroupBox groupBox2;
    private System.Windows.Forms.GroupBox groupBox5;
    private System.Windows.Forms.GroupBox groupBox6;
    private System.Windows.Forms.GroupBox groupBox7;
    private System.Windows.Forms.GroupBox groupBox9;
    private System.Windows.Forms.GroupBox groupBox8;
    private System.Windows.Forms.GroupBox groupBox10;
    private System.Windows.Forms.GroupBox groupBox12;
    private System.Windows.Forms.GroupBox groupBox11;
    private System.Windows.Forms.CheckBox chbGAZSaccadeLength;
    private System.Windows.Forms.Label label8;
    private System.Windows.Forms.ToolStripButton btnCancelCalculation;
    private System.Windows.Forms.CheckBox chbTRIResponseCorrectness;
    private System.Windows.Forms.GroupBox groupBox13;
    private System.Windows.Forms.CheckBox chbMSEFixations;
    private System.Windows.Forms.CheckBox chbMSEFixationsPS;
    private System.Windows.Forms.CheckBox chbMSECountFix2FirstClick;
    private System.Windows.Forms.CheckBox chbMSEFixDurationMean;
    private System.Windows.Forms.CheckBox chbMSEFixationSaccadesRatio;
    private System.Windows.Forms.CheckBox chbMSETime2SecondFixAtTarget;
    private System.Windows.Forms.CheckBox chbMSEFixTimeAtTarget;
    private System.Windows.Forms.CheckBox checkBox1;
    private System.Windows.Forms.CheckBox checkBox2;
    private System.Windows.Forms.CheckBox checkBox3;
    private System.Windows.Forms.CheckBox checkBox4;
    private System.Windows.Forms.CheckBox checkBox5;
    private System.Windows.Forms.GroupBox groupBox14;
    private System.Windows.Forms.CheckBox chbMSEAverageDistance;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    private System.Windows.Forms.CheckBox chb8CharacterRow;
    private System.Windows.Forms.NumericUpDown nudGazeTimeUntilNumberOf;
    private System.Windows.Forms.GroupBox groupBox15;
    private System.Windows.Forms.RadioButton rdbGazeNumberOfFixations;
    private System.Windows.Forms.RadioButton rdbGazeCompleteFixationTime;
    private System.Windows.Forms.RadioButton rdbGazeTimeUntil;
    private System.Windows.Forms.ComboBox cbbGazeAOIGroups;
    private System.Windows.Forms.Label label11;
    private System.Windows.Forms.Button btnGazeAddCustomVariable;
    private System.Windows.Forms.ContextMenuStrip cmuDGV;
    private System.Windows.Forms.ToolStripMenuItem cmuRecalculate;
    private System.Windows.Forms.ToolStripMenuItem cmuRemoveColumn;
    private System.Windows.Forms.GroupBox groupBox16;
    private System.Windows.Forms.Button btnMouseAddCustomVariable;
    private System.Windows.Forms.NumericUpDown nudMouseNumberOf;
    private System.Windows.Forms.RadioButton rdbMouseNumberOfFixations;
    private System.Windows.Forms.Label label10;
    private System.Windows.Forms.RadioButton rdbMouseCompleteFixationTime;
    private System.Windows.Forms.RadioButton rdbMouseTimeUntil;
    private System.Windows.Forms.ComboBox cbbMouseAOIGroup;
    private System.Windows.Forms.TabControl tacMeta;
    private System.Windows.Forms.TabPage tbpMetaStandard;
    private System.Windows.Forms.TabPage tbpMetaTransitions;
    private OgamaControls.CheckboxTreeView trvTrialsAOI;
    private System.Windows.Forms.RadioButton rdbTransitionsUseTrial;
    private System.Windows.Forms.RadioButton rdbTransitionUseAOIGroups;
    private System.ComponentModel.BackgroundWorker bgwCalculateTransitions;
    private System.Windows.Forms.SplitContainer spcTacDgv;
    private System.Windows.Forms.ToolStripContainer tscStandard;
    private System.Windows.Forms.ToolStripContainer tscTransitions;
    private System.Windows.Forms.SplitContainer splitContainer2;
    private System.Windows.Forms.DataGridView dgvTransitions;
    private System.Windows.Forms.ToolStrip tosTransitions;
    private System.Windows.Forms.ToolStripButton btnTransitionsStartCalculation;
    private System.Windows.Forms.ToolStripButton btnTransitionsAbortCalculation;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    private System.Windows.Forms.ToolStripButton btnTransitionsExportTable;
    private System.Windows.Forms.PictureBox pictureBox6;
    private System.Windows.Forms.Label label14;
    private System.Windows.Forms.Button btnTransitionsDeselectAllTrials;
    private System.Windows.Forms.Button btnTransitionsSelectAllTrials;
    private System.Windows.Forms.PictureBox pictureBox7;
    private System.Windows.Forms.Label label15;
    private System.Windows.Forms.TabControl tpbTrial;
    private System.Windows.Forms.TabPage tpbTrialStandard;
    private OgamaControls.CheckboxTreeView trvTrialsDefault;
    private System.Windows.Forms.TabPage tbpTrialAdditional;
    private DialogTop dialogTop1;
    private System.Windows.Forms.Button btnStandardDeselectAllTrials;
    private System.Windows.Forms.Button btnStandardSelectAllTrials;
    private System.Windows.Forms.SplitContainer splitContainer1;
    private System.Windows.Forms.SplitContainer splitContainer3;
    private DialogTop dialogTop2;
    private System.Windows.Forms.TabControl tacGazeParams;
    private System.Windows.Forms.TabPage tbpGazeDefault;
    private System.Windows.Forms.TabPage tbpGazeAOI;
    private System.Windows.Forms.SplitContainer splitContainer4;
    private DialogTop dialogTop3;
    private System.Windows.Forms.SplitContainer splitContainer5;
    private DialogTop dialogTop4;
    private System.Windows.Forms.TabControl tacMouse;
    private System.Windows.Forms.TabPage tbpMouseDefault;
    private System.Windows.Forms.TabPage tbpMouseAOI;
    private System.Windows.Forms.SplitContainer splitContainer6;
    private DialogTop dialogTop5;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.RadioButton rdbGazeFixationDurationMean;
    private System.Windows.Forms.ComboBox cbbGazeAOISingle;
    private System.Windows.Forms.RadioButton rdbMouseFixationDurationMean;
    private System.Windows.Forms.ComboBox cbbMouseAOISingle;
    private System.Windows.Forms.RadioButton rdbMouseAOISingle;
    private System.Windows.Forms.RadioButton rdbMouseAOIGroup;
    private System.Windows.Forms.RadioButton rdbGazeAOISingle;
    private System.Windows.Forms.RadioButton rdbGazeAOIGroup;
    private System.Windows.Forms.ComboBox cbbMouseClickButton;
    private System.Windows.Forms.RadioButton rdbMouseClickAOI;
    private System.Windows.Forms.CheckBox chbTRIAOIofResponse;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
    private System.Windows.Forms.ToolStripButton btnHelp;
    private System.Windows.Forms.GroupBox groupBox17;
    private System.Windows.Forms.CheckBox chbTRISequence;
    private System.Windows.Forms.GroupBox groupBox19;
    private System.Windows.Forms.GroupBox groupBox20;
    private System.Windows.Forms.GroupBox groupBox18;
    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    private System.Windows.Forms.GroupBox groupBox21;
    private System.Windows.Forms.CheckedListBox clbSUBCustomparameters;
    private System.Windows.Forms.GroupBox groupBox22;
    private System.Windows.Forms.CheckBox chbGAZPathlength;
    private System.Windows.Forms.CheckBox chbGAZPathlengthPS;
    private System.Windows.Forms.GroupBox groupBox23;
    private System.Windows.Forms.CheckBox chbMSESaccadeLength;
    private System.Windows.Forms.CheckBox chbMSESaccadeVelocity;
    private System.Windows.Forms.CheckBox chbGAZFixDurationMedian;
    private System.Windows.Forms.CheckBox chbMSEFixDurationMedian;
    private System.Windows.Forms.RadioButton rdbGazeFixationDurationMedian;
    private System.Windows.Forms.RadioButton rdbMouseFixationDurationMedian;
    private System.Windows.Forms.GroupBox groupBox24;
    private OgamaControls.CheckboxTreeView trvSubjects;
    private System.Windows.Forms.ImageList imlTreeViewSubjects;
    private System.Windows.Forms.SplitContainer splitContainer7;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TabPage tbpGazeRegressions;
    private System.Windows.Forms.RadioButton rdbGazeRegressionAOISingle;
    private System.Windows.Forms.RadioButton rdbGazeRegressionAll;
    private System.Windows.Forms.RadioButton rdbGazeRegressionAOIGroup;
    private System.Windows.Forms.ComboBox cbbGazeRegressionAOISingle;
    private System.Windows.Forms.ComboBox cbbGazeRegressionAOIGroups;
    private OgamaControls.CheckboxTreeView trvTransitionsSubjects;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.NumericUpDown nudGazeRegressionLineHeight;
    private System.Windows.Forms.Button btnGazeAddCustomRegressionVariable;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.RadioButton rdbGazeSaccadeVelocity;
    private System.Windows.Forms.RadioButton rdbGazeSaccadeLength;
    private System.Windows.Forms.RadioButton rdbGazeSaccadeDuration;
    private System.Windows.Forms.RadioButton rdbMouseSaccadeVelocity;
    private System.Windows.Forms.RadioButton rdbMouseSaccadeLength;
    private System.Windows.Forms.RadioButton rdbMouseSaccadeDuration;
    private System.Windows.Forms.ToolStripButton btnEye;
    private System.Windows.Forms.ToolStripButton btnMouse;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
  }
}