namespace Ogama.MainWindow.Dialogs
{
  using Ogama.Modules.Common.Controls;

  partial class ExperimentSettingsDialog
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExperimentSettingsDialog));
      this.txbXSizeEyeMon = new System.Windows.Forms.TextBox();
      this.txbYSizeEyeMon = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.btnOK = new System.Windows.Forms.Button();
      this.btnCancel = new System.Windows.Forms.Button();
      this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
      this.txbSQLInstanceName = new System.Windows.Forms.TextBox();
      this.groupBox2 = new System.Windows.Forms.GroupBox();
      this.label10 = new System.Windows.Forms.Label();
      this.label9 = new System.Windows.Forms.Label();
      this.label11 = new System.Windows.Forms.Label();
      this.label5 = new System.Windows.Forms.Label();
      this.txbSamplingRateMouse = new System.Windows.Forms.TextBox();
      this.txbSamplingRateGaze = new System.Windows.Forms.TextBox();
      this.nudMinGazeSamples = new System.Windows.Forms.NumericUpDown();
      this.nudMaxDistanceGaze = new System.Windows.Forms.NumericUpDown();
      this.label6 = new System.Windows.Forms.Label();
      this.label7 = new System.Windows.Forms.Label();
      this.groupBox3 = new System.Windows.Forms.GroupBox();
      this.label4 = new System.Windows.Forms.Label();
      this.nudFixationLimit = new System.Windows.Forms.NumericUpDown();
      this.chbEliminateFirstFixationOfGivenLength = new System.Windows.Forms.CheckBox();
      this.chbMergeConsecutiveFixations = new System.Windows.Forms.CheckBox();
      this.label3 = new System.Windows.Forms.Label();
      this.nudFixationRingSize = new System.Windows.Forms.NumericUpDown();
      this.groupBox5 = new System.Windows.Forms.GroupBox();
      this.nudMaxDistanceMouse = new System.Windows.Forms.NumericUpDown();
      this.nudMinMouseSamples = new System.Windows.Forms.NumericUpDown();
      this.groupBox4 = new System.Windows.Forms.GroupBox();
      this.label8 = new System.Windows.Forms.Label();
      this.grbFixParams = new System.Windows.Forms.GroupBox();
      this.groupBox6 = new System.Windows.Forms.GroupBox();
      this.nudGazeDiameterDiv = new System.Windows.Forms.NumericUpDown();
      this.groupBox7 = new System.Windows.Forms.GroupBox();
      this.nudMouseDiameterDiv = new System.Windows.Forms.NumericUpDown();
      this.groupBox8 = new System.Windows.Forms.GroupBox();
      this.btnTestSQLConnection = new System.Windows.Forms.Button();
      this.label12 = new System.Windows.Forms.Label();
      this.dialogTop1 = new DialogTop();
      this.groupBox1.SuspendLayout();
      this.groupBox2.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.nudMinGazeSamples)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.nudMaxDistanceGaze)).BeginInit();
      this.groupBox3.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.nudFixationLimit)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.nudFixationRingSize)).BeginInit();
      this.groupBox5.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.nudMaxDistanceMouse)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.nudMinMouseSamples)).BeginInit();
      this.groupBox4.SuspendLayout();
      this.grbFixParams.SuspendLayout();
      this.groupBox6.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.nudGazeDiameterDiv)).BeginInit();
      this.groupBox7.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.nudMouseDiameterDiv)).BeginInit();
      this.groupBox8.SuspendLayout();
      this.SuspendLayout();
      // 
      // txbXSizeEyeMon
      // 
      this.txbXSizeEyeMon.Location = new System.Drawing.Point(100, 24);
      this.txbXSizeEyeMon.Name = "txbXSizeEyeMon";
      this.txbXSizeEyeMon.Size = new System.Drawing.Size(74, 20);
      this.txbXSizeEyeMon.TabIndex = 1;
      this.txbXSizeEyeMon.Text = "1024";
      this.txbXSizeEyeMon.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.toolTip1.SetToolTip(this.txbXSizeEyeMon, "Width of the stimulus presentation screen.");
      this.txbXSizeEyeMon.Validating += new System.ComponentModel.CancelEventHandler(this.fldXSizeEyeMon_Validating);
      // 
      // txbYSizeEyeMon
      // 
      this.txbYSizeEyeMon.Location = new System.Drawing.Point(100, 50);
      this.txbYSizeEyeMon.Name = "txbYSizeEyeMon";
      this.txbYSizeEyeMon.Size = new System.Drawing.Size(74, 20);
      this.txbYSizeEyeMon.TabIndex = 2;
      this.txbYSizeEyeMon.Text = "768";
      this.txbYSizeEyeMon.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.toolTip1.SetToolTip(this.txbYSizeEyeMon, "Height of the stimulus presentation screen.");
      this.txbYSizeEyeMon.Validating += new System.ComponentModel.CancelEventHandler(this.fldYSizeEyeMon_Validating);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(10, 27);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(68, 13);
      this.label1.TabIndex = 3;
      this.label1.Text = "xSize in Pixel";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(10, 53);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(68, 13);
      this.label2.TabIndex = 4;
      this.label2.Text = "ySize in Pixel";
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.label2);
      this.groupBox1.Controls.Add(this.label1);
      this.groupBox1.Controls.Add(this.txbYSizeEyeMon);
      this.groupBox1.Controls.Add(this.txbXSizeEyeMon);
      this.groupBox1.Location = new System.Drawing.Point(54, 123);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(190, 80);
      this.groupBox1.TabIndex = 5;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Screen size of eyetracker monitor";
      // 
      // btnOK
      // 
      this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.btnOK.Location = new System.Drawing.Point(315, 527);
      this.btnOK.Name = "btnOK";
      this.btnOK.Size = new System.Drawing.Size(64, 23);
      this.btnOK.TabIndex = 12;
      this.btnOK.Text = "OK";
      this.btnOK.UseVisualStyleBackColor = true;
      // 
      // btnCancel
      // 
      this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnCancel.Location = new System.Drawing.Point(385, 527);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(64, 23);
      this.btnCancel.TabIndex = 13;
      this.btnCancel.Text = "Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      // 
      // txbSQLInstanceName
      // 
      this.txbSQLInstanceName.Location = new System.Drawing.Point(179, 21);
      this.txbSQLInstanceName.Name = "txbSQLInstanceName";
      this.txbSQLInstanceName.Size = new System.Drawing.Size(96, 20);
      this.txbSQLInstanceName.TabIndex = 2;
      this.txbSQLInstanceName.Text = "SQLEXPRESS";
      this.txbSQLInstanceName.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.toolTip1.SetToolTip(this.txbSQLInstanceName, "The name of the SQL Server instance name.\r\nE.g. SQLEXPRESS. (Without quotes)");
      // 
      // groupBox2
      // 
      this.groupBox2.Controls.Add(this.label10);
      this.groupBox2.Controls.Add(this.label9);
      this.groupBox2.Controls.Add(this.label11);
      this.groupBox2.Controls.Add(this.label5);
      this.groupBox2.Controls.Add(this.txbSamplingRateMouse);
      this.groupBox2.Controls.Add(this.txbSamplingRateGaze);
      this.groupBox2.Location = new System.Drawing.Point(257, 123);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new System.Drawing.Size(198, 80);
      this.groupBox2.TabIndex = 12;
      this.groupBox2.TabStop = false;
      this.groupBox2.Text = "Sampling rates";
      // 
      // label10
      // 
      this.label10.AutoSize = true;
      this.label10.Location = new System.Drawing.Point(6, 53);
      this.label10.Name = "label10";
      this.label10.Size = new System.Drawing.Size(39, 13);
      this.label10.TabIndex = 2;
      this.label10.Text = "Mouse";
      // 
      // label9
      // 
      this.label9.AutoSize = true;
      this.label9.Location = new System.Drawing.Point(6, 27);
      this.label9.Name = "label9";
      this.label9.Size = new System.Drawing.Size(32, 13);
      this.label9.TabIndex = 2;
      this.label9.Text = "Gaze";
      // 
      // label11
      // 
      this.label11.AutoSize = true;
      this.label11.Location = new System.Drawing.Point(139, 53);
      this.label11.Name = "label11";
      this.label11.Size = new System.Drawing.Size(20, 13);
      this.label11.TabIndex = 1;
      this.label11.Text = "Hz";
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(139, 27);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(20, 13);
      this.label5.TabIndex = 1;
      this.label5.Text = "Hz";
      // 
      // txbSamplingRateMouse
      // 
      this.txbSamplingRateMouse.Location = new System.Drawing.Point(58, 50);
      this.txbSamplingRateMouse.Name = "txbSamplingRateMouse";
      this.txbSamplingRateMouse.Size = new System.Drawing.Size(75, 20);
      this.txbSamplingRateMouse.TabIndex = 4;
      this.txbSamplingRateMouse.Text = "10";
      this.txbSamplingRateMouse.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.txbSamplingRateMouse.Validated += new System.EventHandler(this.txbSamplingRateMouse_Validated);
      this.txbSamplingRateMouse.Validating += new System.ComponentModel.CancelEventHandler(this.txbSamplingRateMouse_Validating);
      // 
      // txbSamplingRateGaze
      // 
      this.txbSamplingRateGaze.Location = new System.Drawing.Point(58, 24);
      this.txbSamplingRateGaze.Name = "txbSamplingRateGaze";
      this.txbSamplingRateGaze.Size = new System.Drawing.Size(75, 20);
      this.txbSamplingRateGaze.TabIndex = 3;
      this.txbSamplingRateGaze.Text = "60";
      this.txbSamplingRateGaze.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.txbSamplingRateGaze.Validated += new System.EventHandler(this.txbSamplingRateGaze_Validated);
      this.txbSamplingRateGaze.Validating += new System.ComponentModel.CancelEventHandler(this.txbSamplingRateGaze_Validating);
      // 
      // nudMinGazeSamples
      // 
      this.nudMinGazeSamples.Location = new System.Drawing.Point(11, 64);
      this.nudMinGazeSamples.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
      this.nudMinGazeSamples.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nudMinGazeSamples.Name = "nudMinGazeSamples";
      this.nudMinGazeSamples.Size = new System.Drawing.Size(43, 20);
      this.nudMinGazeSamples.TabIndex = 6;
      this.nudMinGazeSamples.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.nudMinGazeSamples.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
      // 
      // nudMaxDistanceGaze
      // 
      this.nudMaxDistanceGaze.Location = new System.Drawing.Point(11, 19);
      this.nudMaxDistanceGaze.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
      this.nudMaxDistanceGaze.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nudMaxDistanceGaze.Name = "nudMaxDistanceGaze";
      this.nudMaxDistanceGaze.Size = new System.Drawing.Size(43, 20);
      this.nudMaxDistanceGaze.TabIndex = 5;
      this.nudMaxDistanceGaze.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.nudMaxDistanceGaze.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
      // 
      // label6
      // 
      this.label6.Location = new System.Drawing.Point(6, 71);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(249, 26);
      this.label6.TabIndex = 14;
      this.label6.Text = "Minimum number of samples that can be considered a fixation.";
      // 
      // label7
      // 
      this.label7.Location = new System.Drawing.Point(6, 23);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(249, 41);
      this.label7.TabIndex = 13;
      this.label7.Text = "Maximum distance in pixels that a point may vary from the average fixation point " +
          "and still be considered part of the fixation.";
      // 
      // groupBox3
      // 
      this.groupBox3.Controls.Add(this.label4);
      this.groupBox3.Controls.Add(this.nudFixationLimit);
      this.groupBox3.Controls.Add(this.chbEliminateFirstFixationOfGivenLength);
      this.groupBox3.Controls.Add(this.chbMergeConsecutiveFixations);
      this.groupBox3.Controls.Add(this.label3);
      this.groupBox3.Controls.Add(this.nudFixationRingSize);
      this.groupBox3.Controls.Add(this.groupBox5);
      this.groupBox3.Controls.Add(this.groupBox4);
      this.groupBox3.Controls.Add(this.label7);
      this.groupBox3.Controls.Add(this.label6);
      this.groupBox3.Location = new System.Drawing.Point(54, 209);
      this.groupBox3.Name = "groupBox3";
      this.groupBox3.Size = new System.Drawing.Size(401, 230);
      this.groupBox3.TabIndex = 17;
      this.groupBox3.TabStop = false;
      this.groupBox3.Text = "Fixation calculation parameters";
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(369, 202);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(20, 13);
      this.label4.TabIndex = 23;
      this.label4.Text = "ms";
      // 
      // nudFixationLimit
      // 
      this.nudFixationLimit.Increment = new decimal(new int[] {
            50,
            0,
            0,
            0});
      this.nudFixationLimit.Location = new System.Drawing.Point(299, 198);
      this.nudFixationLimit.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
      this.nudFixationLimit.Name = "nudFixationLimit";
      this.nudFixationLimit.Size = new System.Drawing.Size(63, 20);
      this.nudFixationLimit.TabIndex = 22;
      this.nudFixationLimit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.nudFixationLimit.Value = new decimal(new int[] {
            300,
            0,
            0,
            0});
      // 
      // chbEliminateFirstFixationOfGivenLength
      // 
      this.chbEliminateFirstFixationOfGivenLength.AutoSize = true;
      this.chbEliminateFirstFixationOfGivenLength.Location = new System.Drawing.Point(9, 198);
      this.chbEliminateFirstFixationOfGivenLength.Name = "chbEliminateFirstFixationOfGivenLength";
      this.chbEliminateFirstFixationOfGivenLength.Size = new System.Drawing.Size(290, 30);
      this.chbEliminateFirstFixationOfGivenLength.TabIndex = 21;
      this.chbEliminateFirstFixationOfGivenLength.Text = "Eliminate first fixation if it is at same place as last fixation \r\nof foregoing t" +
          "rial and shorter than";
      this.chbEliminateFirstFixationOfGivenLength.UseVisualStyleBackColor = true;
      // 
      // chbMergeConsecutiveFixations
      // 
      this.chbMergeConsecutiveFixations.AutoSize = true;
      this.chbMergeConsecutiveFixations.Checked = true;
      this.chbMergeConsecutiveFixations.CheckState = System.Windows.Forms.CheckState.Checked;
      this.chbMergeConsecutiveFixations.Location = new System.Drawing.Point(9, 175);
      this.chbMergeConsecutiveFixations.Name = "chbMergeConsecutiveFixations";
      this.chbMergeConsecutiveFixations.Size = new System.Drawing.Size(333, 17);
      this.chbMergeConsecutiveFixations.TabIndex = 21;
      this.chbMergeConsecutiveFixations.Text = "Merge consecutive fixations within max distance into one fixation.";
      this.chbMergeConsecutiveFixations.UseVisualStyleBackColor = true;
      // 
      // label3
      // 
      this.label3.Location = new System.Drawing.Point(6, 111);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(258, 52);
      this.label3.TabIndex = 20;
      this.label3.Text = resources.GetString("label3.Text");
      // 
      // nudFixationRingSize
      // 
      this.nudFixationRingSize.Increment = new decimal(new int[] {
            2,
            0,
            0,
            0});
      this.nudFixationRingSize.Location = new System.Drawing.Point(342, 127);
      this.nudFixationRingSize.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
      this.nudFixationRingSize.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
      this.nudFixationRingSize.Name = "nudFixationRingSize";
      this.nudFixationRingSize.Size = new System.Drawing.Size(43, 20);
      this.nudFixationRingSize.TabIndex = 11;
      this.nudFixationRingSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.nudFixationRingSize.Value = new decimal(new int[] {
            31,
            0,
            0,
            0});
      // 
      // groupBox5
      // 
      this.groupBox5.Controls.Add(this.nudMaxDistanceMouse);
      this.groupBox5.Controls.Add(this.nudMinMouseSamples);
      this.groupBox5.Location = new System.Drawing.Point(331, 13);
      this.groupBox5.Name = "groupBox5";
      this.groupBox5.Size = new System.Drawing.Size(64, 94);
      this.groupBox5.TabIndex = 19;
      this.groupBox5.TabStop = false;
      this.groupBox5.Text = "Mouse";
      // 
      // nudMaxDistanceMouse
      // 
      this.nudMaxDistanceMouse.Location = new System.Drawing.Point(11, 19);
      this.nudMaxDistanceMouse.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
      this.nudMaxDistanceMouse.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nudMaxDistanceMouse.Name = "nudMaxDistanceMouse";
      this.nudMaxDistanceMouse.Size = new System.Drawing.Size(43, 20);
      this.nudMaxDistanceMouse.TabIndex = 8;
      this.nudMaxDistanceMouse.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.nudMaxDistanceMouse.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
      // 
      // nudMinMouseSamples
      // 
      this.nudMinMouseSamples.Location = new System.Drawing.Point(11, 64);
      this.nudMinMouseSamples.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
      this.nudMinMouseSamples.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nudMinMouseSamples.Name = "nudMinMouseSamples";
      this.nudMinMouseSamples.Size = new System.Drawing.Size(43, 20);
      this.nudMinMouseSamples.TabIndex = 9;
      this.nudMinMouseSamples.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.nudMinMouseSamples.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
      // 
      // groupBox4
      // 
      this.groupBox4.Controls.Add(this.nudMaxDistanceGaze);
      this.groupBox4.Controls.Add(this.nudMinGazeSamples);
      this.groupBox4.Location = new System.Drawing.Point(261, 13);
      this.groupBox4.Name = "groupBox4";
      this.groupBox4.Size = new System.Drawing.Size(64, 94);
      this.groupBox4.TabIndex = 19;
      this.groupBox4.TabStop = false;
      this.groupBox4.Text = "Gaze";
      // 
      // label8
      // 
      this.label8.Location = new System.Drawing.Point(10, 16);
      this.label8.Name = "label8";
      this.label8.Size = new System.Drawing.Size(249, 46);
      this.label8.TabIndex = 20;
      this.label8.Text = "Diameter ratio of fixations. The fixation time in milliseconds over this number w" +
          "ill give the fixations circle diameter.";
      // 
      // grbFixParams
      // 
      this.grbFixParams.Controls.Add(this.label8);
      this.grbFixParams.Controls.Add(this.groupBox6);
      this.grbFixParams.Controls.Add(this.groupBox7);
      this.grbFixParams.Location = new System.Drawing.Point(54, 445);
      this.grbFixParams.Name = "grbFixParams";
      this.grbFixParams.Size = new System.Drawing.Size(401, 67);
      this.grbFixParams.TabIndex = 19;
      this.grbFixParams.TabStop = false;
      this.grbFixParams.Text = "Fixation display parameters";
      // 
      // groupBox6
      // 
      this.groupBox6.Controls.Add(this.nudGazeDiameterDiv);
      this.groupBox6.Location = new System.Drawing.Point(261, 10);
      this.groupBox6.Name = "groupBox6";
      this.groupBox6.Size = new System.Drawing.Size(64, 46);
      this.groupBox6.TabIndex = 19;
      this.groupBox6.TabStop = false;
      this.groupBox6.Text = "Gaze";
      // 
      // nudGazeDiameterDiv
      // 
      this.nudGazeDiameterDiv.DecimalPlaces = 1;
      this.nudGazeDiameterDiv.Increment = new decimal(new int[] {
            2,
            0,
            0,
            65536});
      this.nudGazeDiameterDiv.Location = new System.Drawing.Point(11, 19);
      this.nudGazeDiameterDiv.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
      this.nudGazeDiameterDiv.Name = "nudGazeDiameterDiv";
      this.nudGazeDiameterDiv.Size = new System.Drawing.Size(43, 20);
      this.nudGazeDiameterDiv.TabIndex = 7;
      this.nudGazeDiameterDiv.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
      // 
      // groupBox7
      // 
      this.groupBox7.Controls.Add(this.nudMouseDiameterDiv);
      this.groupBox7.Location = new System.Drawing.Point(331, 10);
      this.groupBox7.Name = "groupBox7";
      this.groupBox7.Size = new System.Drawing.Size(64, 46);
      this.groupBox7.TabIndex = 19;
      this.groupBox7.TabStop = false;
      this.groupBox7.Text = "Mouse";
      // 
      // nudMouseDiameterDiv
      // 
      this.nudMouseDiameterDiv.DecimalPlaces = 1;
      this.nudMouseDiameterDiv.Increment = new decimal(new int[] {
            2,
            0,
            0,
            65536});
      this.nudMouseDiameterDiv.Location = new System.Drawing.Point(11, 19);
      this.nudMouseDiameterDiv.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
      this.nudMouseDiameterDiv.Name = "nudMouseDiameterDiv";
      this.nudMouseDiameterDiv.Size = new System.Drawing.Size(43, 20);
      this.nudMouseDiameterDiv.TabIndex = 10;
      this.nudMouseDiameterDiv.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
      // 
      // groupBox8
      // 
      this.groupBox8.Controls.Add(this.btnTestSQLConnection);
      this.groupBox8.Controls.Add(this.txbSQLInstanceName);
      this.groupBox8.Controls.Add(this.label12);
      this.groupBox8.Location = new System.Drawing.Point(54, 66);
      this.groupBox8.Name = "groupBox8";
      this.groupBox8.Size = new System.Drawing.Size(400, 47);
      this.groupBox8.TabIndex = 21;
      this.groupBox8.TabStop = false;
      this.groupBox8.Text = "SQL Server instance";
      // 
      // btnTestSQLConnection
      // 
      this.btnTestSQLConnection.Location = new System.Drawing.Point(281, 18);
      this.btnTestSQLConnection.Name = "btnTestSQLConnection";
      this.btnTestSQLConnection.Size = new System.Drawing.Size(108, 23);
      this.btnTestSQLConnection.TabIndex = 4;
      this.btnTestSQLConnection.Text = "Test connection";
      this.toolTip1.SetToolTip(this.btnTestSQLConnection, "Test the given SQL connection.");
      this.btnTestSQLConnection.UseVisualStyleBackColor = true;
      this.btnTestSQLConnection.Click += new System.EventHandler(this.btnTestSQLConnection_Click);
      // 
      // label12
      // 
      this.label12.AutoSize = true;
      this.label12.Location = new System.Drawing.Point(10, 24);
      this.label12.Name = "label12";
      this.label12.Size = new System.Drawing.Size(163, 13);
      this.label12.TabIndex = 3;
      this.label12.Text = "Instance name of the SQL server";
      // 
      // dialogTop1
      // 
      this.dialogTop1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(237)))));
      this.dialogTop1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("dialogTop1.BackgroundImage")));
      this.dialogTop1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
      this.dialogTop1.Description = "Please specify the settings for this experiment.";
      this.dialogTop1.Dock = System.Windows.Forms.DockStyle.Top;
      this.dialogTop1.Location = new System.Drawing.Point(0, 0);
      this.dialogTop1.Logo = global::Ogama.Properties.Resources.propertiesORoptions;
      this.dialogTop1.Name = "dialogTop1";
      this.dialogTop1.Size = new System.Drawing.Size(460, 60);
      this.dialogTop1.TabIndex = 20;
      // 
      // ExperimentSettingsDialog
      // 
      this.AcceptButton = this.btnOK;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.btnCancel;
      this.ClientSize = new System.Drawing.Size(460, 556);
      this.Controls.Add(this.groupBox8);
      this.Controls.Add(this.dialogTop1);
      this.Controls.Add(this.grbFixParams);
      this.Controls.Add(this.groupBox3);
      this.Controls.Add(this.groupBox2);
      this.Controls.Add(this.btnCancel);
      this.Controls.Add(this.btnOK);
      this.Controls.Add(this.groupBox1);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "ExperimentSettingsDialog";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Experiment Settings ...";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ExperimentSettingsDialog_FormClosing);
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.groupBox2.ResumeLayout(false);
      this.groupBox2.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.nudMinGazeSamples)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.nudMaxDistanceGaze)).EndInit();
      this.groupBox3.ResumeLayout(false);
      this.groupBox3.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.nudFixationLimit)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.nudFixationRingSize)).EndInit();
      this.groupBox5.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.nudMaxDistanceMouse)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.nudMinMouseSamples)).EndInit();
      this.groupBox4.ResumeLayout(false);
      this.grbFixParams.ResumeLayout(false);
      this.groupBox6.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.nudGazeDiameterDiv)).EndInit();
      this.groupBox7.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.nudMouseDiameterDiv)).EndInit();
      this.groupBox8.ResumeLayout(false);
      this.groupBox8.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.Button btnOK;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.TextBox txbXSizeEyeMon;
    private System.Windows.Forms.TextBox txbYSizeEyeMon;
    private System.Windows.Forms.ToolTip toolTip1;
    private System.Windows.Forms.GroupBox groupBox2;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.TextBox txbSamplingRateGaze;
    private System.Windows.Forms.NumericUpDown nudMinGazeSamples;
    private System.Windows.Forms.NumericUpDown nudMaxDistanceGaze;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.GroupBox groupBox3;
    private System.Windows.Forms.GroupBox groupBox4;
    private System.Windows.Forms.NumericUpDown nudMinMouseSamples;
    private System.Windows.Forms.NumericUpDown nudMaxDistanceMouse;
    private System.Windows.Forms.GroupBox groupBox5;
    private System.Windows.Forms.Label label8;
    private System.Windows.Forms.Label label10;
    private System.Windows.Forms.Label label9;
    private System.Windows.Forms.Label label11;
    private System.Windows.Forms.TextBox txbSamplingRateMouse;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.NumericUpDown nudFixationRingSize;
    private System.Windows.Forms.GroupBox grbFixParams;
    private System.Windows.Forms.GroupBox groupBox6;
    private System.Windows.Forms.NumericUpDown nudGazeDiameterDiv;
    private System.Windows.Forms.GroupBox groupBox7;
    private System.Windows.Forms.NumericUpDown nudMouseDiameterDiv;
    private DialogTop dialogTop1;
    private System.Windows.Forms.CheckBox chbMergeConsecutiveFixations;
    private System.Windows.Forms.CheckBox chbEliminateFirstFixationOfGivenLength;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.NumericUpDown nudFixationLimit;
    private System.Windows.Forms.GroupBox groupBox8;
    private System.Windows.Forms.Button btnTestSQLConnection;
    private System.Windows.Forms.TextBox txbSQLInstanceName;
    private System.Windows.Forms.Label label12;
  }
}