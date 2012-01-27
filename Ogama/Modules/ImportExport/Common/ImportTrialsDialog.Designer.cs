namespace Ogama.Modules.ImportExport.Common
{
  using Ogama.Modules.Common.Controls;

  partial class ImportTrialsDialog
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImportTrialsDialog));
      this.btnCancel = new System.Windows.Forms.Button();
      this.btnNext = new System.Windows.Forms.Button();
      this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
      this.txbTrialMSG = new System.Windows.Forms.TextBox();
      this.groupBox2 = new System.Windows.Forms.GroupBox();
      this.spcTableDropDowns = new System.Windows.Forms.SplitContainer();
      this.dgvAssignments = new System.Windows.Forms.DataGridView();
      this.columnAssignTrialSequence = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.columnAssignTrialID = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.columnAssignStartTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.cbbAssignTrialSequenceColumn = new System.Windows.Forms.ComboBox();
      this.cbbAssignTrialIDColumn = new System.Windows.Forms.ComboBox();
      this.label1 = new System.Windows.Forms.Label();
      this.cbbAssignStartingTimeColumn = new System.Windows.Forms.ComboBox();
      this.label5 = new System.Windows.Forms.Label();
      this.label4 = new System.Windows.Forms.Label();
      this.txbTrialSequenceColumn = new System.Windows.Forms.TextBox();
      this.grbFileName = new System.Windows.Forms.GroupBox();
      this.btnOpenAssignFile = new System.Windows.Forms.Button();
      this.txbImportAssignFile = new System.Windows.Forms.TextBox();
      this.rdbUseTrialColumn = new System.Windows.Forms.RadioButton();
      this.rdbImportTableTrialID = new System.Windows.Forms.RadioButton();
      this.rdbEnterTableTrialID = new System.Windows.Forms.RadioButton();
      this.rdbUseMSGLines = new System.Windows.Forms.RadioButton();
      this.label2 = new System.Windows.Forms.Label();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.label12 = new System.Windows.Forms.Label();
      this.cbbTimeUnit = new System.Windows.Forms.ComboBox();
      this.label3 = new System.Windows.Forms.Label();
      this.nudImportLines = new System.Windows.Forms.NumericUpDown();
      this.dgvTrialsPreview = new System.Windows.Forms.DataGridView();
      this.columnSubjectName = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.columnTrialSequence = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.columnTrialID = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.columnCategory = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.columnTrialStartTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.columnDuration = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.panel3 = new System.Windows.Forms.Panel();
      this.label6 = new System.Windows.Forms.Label();
      this.btnBack = new System.Windows.Forms.Button();
      this.groupBox4 = new System.Windows.Forms.GroupBox();
      this.ofdAssignmentFile = new System.Windows.Forms.OpenFileDialog();
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      this.splitContainer3 = new System.Windows.Forms.SplitContainer();
      this.dialogTop1 = new DialogTop();
      this.splitContainer2 = new System.Windows.Forms.SplitContainer();
      this.groupBox2.SuspendLayout();
      this.spcTableDropDowns.Panel1.SuspendLayout();
      this.spcTableDropDowns.Panel2.SuspendLayout();
      this.spcTableDropDowns.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.dgvAssignments)).BeginInit();
      this.grbFileName.SuspendLayout();
      this.groupBox1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.nudImportLines)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.dgvTrialsPreview)).BeginInit();
      this.panel3.SuspendLayout();
      this.groupBox4.SuspendLayout();
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      this.splitContainer3.Panel1.SuspendLayout();
      this.splitContainer3.Panel2.SuspendLayout();
      this.splitContainer3.SuspendLayout();
      this.splitContainer2.Panel1.SuspendLayout();
      this.splitContainer2.Panel2.SuspendLayout();
      this.splitContainer2.SuspendLayout();
      this.SuspendLayout();
      // 
      // btnCancel
      // 
      this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Abort;
      this.btnCancel.Location = new System.Drawing.Point(603, 4);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(75, 23);
      this.btnCancel.TabIndex = 27;
      this.btnCancel.Text = "&Cancel";
      this.toolTip1.SetToolTip(this.btnCancel, "Cancel import.");
      // 
      // btnNext
      // 
      this.btnNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnNext.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.btnNext.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.btnNext.Location = new System.Drawing.Point(489, 4);
      this.btnNext.Name = "btnNext";
      this.btnNext.Size = new System.Drawing.Size(108, 23);
      this.btnNext.TabIndex = 26;
      this.btnNext.Text = "> &Next (Stimuli)";
      this.toolTip1.SetToolTip(this.btnNext, "goto Step 4: specify stimulus images");
      this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
      // 
      // txbTrialMSG
      // 
      this.txbTrialMSG.Location = new System.Drawing.Point(161, 58);
      this.txbTrialMSG.Name = "txbTrialMSG";
      this.txbTrialMSG.Size = new System.Drawing.Size(86, 20);
      this.txbTrialMSG.TabIndex = 42;
      this.txbTrialMSG.Text = "MSG";
      this.toolTip1.SetToolTip(this.txbTrialMSG, "This field is case-sensitive !");
      this.txbTrialMSG.TextChanged += new System.EventHandler(this.txbTrialMSG_TextChanged);
      this.txbTrialMSG.Leave += new System.EventHandler(this.txbTrialMSG_Leave);
      // 
      // groupBox2
      // 
      this.groupBox2.Controls.Add(this.spcTableDropDowns);
      this.groupBox2.Controls.Add(this.txbTrialSequenceColumn);
      this.groupBox2.Controls.Add(this.txbTrialMSG);
      this.groupBox2.Controls.Add(this.grbFileName);
      this.groupBox2.Controls.Add(this.rdbUseTrialColumn);
      this.groupBox2.Controls.Add(this.rdbImportTableTrialID);
      this.groupBox2.Controls.Add(this.rdbEnterTableTrialID);
      this.groupBox2.Controls.Add(this.rdbUseMSGLines);
      this.groupBox2.Controls.Add(this.label2);
      this.groupBox2.Location = new System.Drawing.Point(3, 3);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new System.Drawing.Size(518, 230);
      this.groupBox2.TabIndex = 39;
      this.groupBox2.TabStop = false;
      this.groupBox2.Text = "Trials";
      // 
      // spcTableDropDowns
      // 
      this.spcTableDropDowns.Location = new System.Drawing.Point(264, 16);
      this.spcTableDropDowns.Name = "spcTableDropDowns";
      this.spcTableDropDowns.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // spcTableDropDowns.Panel1
      // 
      this.spcTableDropDowns.Panel1.Controls.Add(this.dgvAssignments);
      // 
      // spcTableDropDowns.Panel2
      // 
      this.spcTableDropDowns.Panel2.Controls.Add(this.cbbAssignTrialSequenceColumn);
      this.spcTableDropDowns.Panel2.Controls.Add(this.cbbAssignTrialIDColumn);
      this.spcTableDropDowns.Panel2.Controls.Add(this.label1);
      this.spcTableDropDowns.Panel2.Controls.Add(this.cbbAssignStartingTimeColumn);
      this.spcTableDropDowns.Panel2.Controls.Add(this.label5);
      this.spcTableDropDowns.Panel2.Controls.Add(this.label4);
      this.spcTableDropDowns.Size = new System.Drawing.Size(248, 208);
      this.spcTableDropDowns.SplitterDistance = 127;
      this.spcTableDropDowns.TabIndex = 43;
      // 
      // dgvAssignments
      // 
      this.dgvAssignments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dgvAssignments.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnAssignTrialSequence,
            this.columnAssignTrialID,
            this.columnAssignStartTime});
      this.dgvAssignments.Dock = System.Windows.Forms.DockStyle.Fill;
      this.dgvAssignments.Location = new System.Drawing.Point(0, 0);
      this.dgvAssignments.Name = "dgvAssignments";
      this.dgvAssignments.Size = new System.Drawing.Size(248, 127);
      this.dgvAssignments.TabIndex = 40;
      this.dgvAssignments.Visible = false;
      this.dgvAssignments.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvAssignments_CellLeave);
      this.dgvAssignments.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgvAssignments_CellValidating);
      this.dgvAssignments.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgvAssignments_EditingControlShowing);
      // 
      // columnAssignTrialSequence
      // 
      this.columnAssignTrialSequence.HeaderText = "Sequence";
      this.columnAssignTrialSequence.Name = "columnAssignTrialSequence";
      this.columnAssignTrialSequence.Width = 60;
      // 
      // columnAssignTrialID
      // 
      this.columnAssignTrialID.HeaderText = "TrialID";
      this.columnAssignTrialID.Name = "columnAssignTrialID";
      this.columnAssignTrialID.Width = 50;
      // 
      // columnAssignStartTime
      // 
      this.columnAssignStartTime.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
      this.columnAssignStartTime.HeaderText = "Starttime";
      this.columnAssignStartTime.Name = "columnAssignStartTime";
      // 
      // cbbAssignTrialSequenceColumn
      // 
      this.cbbAssignTrialSequenceColumn.FormattingEnabled = true;
      this.cbbAssignTrialSequenceColumn.Location = new System.Drawing.Point(136, 2);
      this.cbbAssignTrialSequenceColumn.Name = "cbbAssignTrialSequenceColumn";
      this.cbbAssignTrialSequenceColumn.Size = new System.Drawing.Size(112, 21);
      this.cbbAssignTrialSequenceColumn.TabIndex = 1;
      this.toolTip1.SetToolTip(this.cbbAssignTrialSequenceColumn, "Choose the column in the text file which contains the sequence numbers.");
      this.cbbAssignTrialSequenceColumn.SelectionChangeCommitted += new System.EventHandler(this.cbbAssignColumn_SelectionChangeCommitted);
      // 
      // cbbAssignTrialIDColumn
      // 
      this.cbbAssignTrialIDColumn.FormattingEnabled = true;
      this.cbbAssignTrialIDColumn.Location = new System.Drawing.Point(136, 26);
      this.cbbAssignTrialIDColumn.Name = "cbbAssignTrialIDColumn";
      this.cbbAssignTrialIDColumn.Size = new System.Drawing.Size(112, 21);
      this.cbbAssignTrialIDColumn.TabIndex = 1;
      this.toolTip1.SetToolTip(this.cbbAssignTrialIDColumn, "Specify the column in the import file which contains the trial ids.");
      this.cbbAssignTrialIDColumn.SelectionChangeCommitted += new System.EventHandler(this.cbbAssignColumn_SelectionChangeCommitted);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(3, 5);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(96, 13);
      this.label1.TabIndex = 0;
      this.label1.Text = "Sequence column:";
      // 
      // cbbAssignStartingTimeColumn
      // 
      this.cbbAssignStartingTimeColumn.FormattingEnabled = true;
      this.cbbAssignStartingTimeColumn.Location = new System.Drawing.Point(136, 50);
      this.cbbAssignStartingTimeColumn.Name = "cbbAssignStartingTimeColumn";
      this.cbbAssignStartingTimeColumn.Size = new System.Drawing.Size(112, 21);
      this.cbbAssignStartingTimeColumn.TabIndex = 1;
      this.toolTip1.SetToolTip(this.cbbAssignStartingTimeColumn, "Specify the column in the import file which contains the starting times of the tr" +
              "ials.");
      this.cbbAssignStartingTimeColumn.SelectionChangeCommitted += new System.EventHandler(this.cbbAssignColumn_SelectionChangeCommitted);
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(3, 29);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(58, 13);
      this.label5.TabIndex = 0;
      this.label5.Text = "ID column:";
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(3, 53);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(105, 13);
      this.label4.TabIndex = 0;
      this.label4.Text = "Starting time column:";
      // 
      // txbTrialSequenceColumn
      // 
      this.txbTrialSequenceColumn.Location = new System.Drawing.Point(161, 34);
      this.txbTrialSequenceColumn.Name = "txbTrialSequenceColumn";
      this.txbTrialSequenceColumn.ReadOnly = true;
      this.txbTrialSequenceColumn.Size = new System.Drawing.Size(86, 20);
      this.txbTrialSequenceColumn.TabIndex = 42;
      this.txbTrialSequenceColumn.TextChanged += new System.EventHandler(this.txbTrialMSG_TextChanged);
      // 
      // grbFileName
      // 
      this.grbFileName.Controls.Add(this.btnOpenAssignFile);
      this.grbFileName.Controls.Add(this.txbImportAssignFile);
      this.grbFileName.Location = new System.Drawing.Point(7, 145);
      this.grbFileName.Name = "grbFileName";
      this.grbFileName.Size = new System.Drawing.Size(251, 53);
      this.grbFileName.TabIndex = 41;
      this.grbFileName.TabStop = false;
      this.grbFileName.Text = "assignment table: Trial Sequence, ID, Starttime";
      this.grbFileName.Visible = false;
      // 
      // btnOpenAssignFile
      // 
      this.btnOpenAssignFile.Image = global::Ogama.Properties.Resources.openfolderHS;
      this.btnOpenAssignFile.Location = new System.Drawing.Point(221, 20);
      this.btnOpenAssignFile.Name = "btnOpenAssignFile";
      this.btnOpenAssignFile.Size = new System.Drawing.Size(24, 22);
      this.btnOpenAssignFile.TabIndex = 25;
      this.btnOpenAssignFile.UseVisualStyleBackColor = true;
      this.btnOpenAssignFile.Visible = false;
      this.btnOpenAssignFile.Click += new System.EventHandler(this.btnOpenAssignFile_Click);
      // 
      // txbImportAssignFile
      // 
      this.txbImportAssignFile.Location = new System.Drawing.Point(6, 22);
      this.txbImportAssignFile.Name = "txbImportAssignFile";
      this.txbImportAssignFile.ReadOnly = true;
      this.txbImportAssignFile.Size = new System.Drawing.Size(209, 20);
      this.txbImportAssignFile.TabIndex = 24;
      this.txbImportAssignFile.Visible = false;
      // 
      // rdbUseTrialColumn
      // 
      this.rdbUseTrialColumn.AutoSize = true;
      this.rdbUseTrialColumn.Location = new System.Drawing.Point(7, 35);
      this.rdbUseTrialColumn.Name = "rdbUseTrialColumn";
      this.rdbUseTrialColumn.Size = new System.Drawing.Size(148, 17);
      this.rdbUseTrialColumn.TabIndex = 23;
      this.rdbUseTrialColumn.Text = "use trial sequence column";
      this.toolTip1.SetToolTip(this.rdbUseTrialColumn, "Each raw data row is assigned to the trial that is specified in the trial id row." +
              "");
      this.rdbUseTrialColumn.UseVisualStyleBackColor = true;
      this.rdbUseTrialColumn.CheckedChanged += new System.EventHandler(this.rdbTrialMode_CheckedChanged);
      // 
      // rdbImportTableTrialID
      // 
      this.rdbImportTableTrialID.AutoSize = true;
      this.rdbImportTableTrialID.Location = new System.Drawing.Point(7, 108);
      this.rdbImportTableTrialID.Name = "rdbImportTableTrialID";
      this.rdbImportTableTrialID.Size = new System.Drawing.Size(170, 17);
      this.rdbImportTableTrialID.TabIndex = 23;
      this.rdbImportTableTrialID.Text = "import table with trial start times";
      this.toolTip1.SetToolTip(this.rdbImportTableTrialID, "Import a table containing sequence, trial id and starttime assignments (in this o" +
              "rder)");
      this.rdbImportTableTrialID.UseVisualStyleBackColor = true;
      this.rdbImportTableTrialID.CheckedChanged += new System.EventHandler(this.rdbTrialMode_CheckedChanged);
      // 
      // rdbEnterTableTrialID
      // 
      this.rdbEnterTableTrialID.AutoSize = true;
      this.rdbEnterTableTrialID.Location = new System.Drawing.Point(7, 83);
      this.rdbEnterTableTrialID.Name = "rdbEnterTableTrialID";
      this.rdbEnterTableTrialID.Size = new System.Drawing.Size(166, 17);
      this.rdbEnterTableTrialID.TabIndex = 23;
      this.rdbEnterTableTrialID.Text = "enter table with trial start times";
      this.toolTip1.SetToolTip(this.rdbEnterTableTrialID, "Customize the sequence, trial ID to starttime assignment by entering a table whic" +
              "h tells Ogama at which starttime it should assign new rawdata to a given sequenc" +
              "e and trial id.");
      this.rdbEnterTableTrialID.UseVisualStyleBackColor = true;
      this.rdbEnterTableTrialID.CheckedChanged += new System.EventHandler(this.rdbTrialMode_CheckedChanged);
      // 
      // rdbUseMSGLines
      // 
      this.rdbUseMSGLines.AutoSize = true;
      this.rdbUseMSGLines.Checked = true;
      this.rdbUseMSGLines.Location = new System.Drawing.Point(7, 58);
      this.rdbUseMSGLines.Name = "rdbUseMSGLines";
      this.rdbUseMSGLines.Size = new System.Drawing.Size(148, 17);
      this.rdbUseMSGLines.TabIndex = 22;
      this.rdbUseMSGLines.TabStop = true;
      this.rdbUseMSGLines.Text = "use MSG lines containing:";
      this.toolTip1.SetToolTip(this.rdbUseMSGLines, "A line in the raw data file containing this string increases the trial counter fo" +
              "r the following data rows.");
      this.rdbUseMSGLines.UseVisualStyleBackColor = true;
      this.rdbUseMSGLines.CheckedChanged += new System.EventHandler(this.rdbTrialMode_CheckedChanged);
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(4, 16);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(151, 13);
      this.label2.TabIndex = 20;
      this.label2.Text = "How to detect different trials ...";
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.label12);
      this.groupBox1.Controls.Add(this.cbbTimeUnit);
      this.groupBox1.Controls.Add(this.label3);
      this.groupBox1.Controls.Add(this.nudImportLines);
      this.groupBox1.Location = new System.Drawing.Point(527, 3);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(141, 119);
      this.groupBox1.TabIndex = 39;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "other Settings";
      // 
      // label12
      // 
      this.label12.Location = new System.Drawing.Point(3, 16);
      this.label12.Name = "label12";
      this.label12.Size = new System.Drawing.Size(131, 27);
      this.label12.TabIndex = 15;
      this.label12.Text = "The time code in the time column is given in:";
      // 
      // cbbTimeUnit
      // 
      this.cbbTimeUnit.FormattingEnabled = true;
      this.cbbTimeUnit.Items.AddRange(new object[] {
            "s   (seconds)",
            "ms (milliseconds)",
            "µs (microseconds)"});
      this.cbbTimeUnit.Location = new System.Drawing.Point(3, 46);
      this.cbbTimeUnit.Name = "cbbTimeUnit";
      this.cbbTimeUnit.Size = new System.Drawing.Size(128, 21);
      this.cbbTimeUnit.TabIndex = 17;
      this.cbbTimeUnit.SelectionChangeCommitted += new System.EventHandler(this.cbbTimeUnit_SelectionChangeCommitted);
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(3, 70);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(135, 13);
      this.label3.TabIndex = 37;
      this.label3.Text = "Number of previewed lines:";
      // 
      // nudImportLines
      // 
      this.nudImportLines.Increment = new decimal(new int[] {
            5000,
            0,
            0,
            0});
      this.nudImportLines.Location = new System.Drawing.Point(3, 86);
      this.nudImportLines.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
      this.nudImportLines.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
      this.nudImportLines.Name = "nudImportLines";
      this.nudImportLines.Size = new System.Drawing.Size(128, 20);
      this.nudImportLines.TabIndex = 36;
      this.nudImportLines.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.nudImportLines.ThousandsSeparator = true;
      this.nudImportLines.Value = new decimal(new int[] {
            20000,
            0,
            0,
            0});
      this.nudImportLines.ValueChanged += new System.EventHandler(this.nudImportLines_ValueChanged);
      // 
      // dgvTrialsPreview
      // 
      this.dgvTrialsPreview.AllowUserToAddRows = false;
      this.dgvTrialsPreview.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
      this.dgvTrialsPreview.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dgvTrialsPreview.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnSubjectName,
            this.columnTrialSequence,
            this.columnTrialID,
            this.columnCategory,
            this.columnTrialStartTime,
            this.columnDuration});
      this.dgvTrialsPreview.Dock = System.Windows.Forms.DockStyle.Fill;
      this.dgvTrialsPreview.Location = new System.Drawing.Point(3, 16);
      this.dgvTrialsPreview.Name = "dgvTrialsPreview";
      this.dgvTrialsPreview.Size = new System.Drawing.Size(684, 131);
      this.dgvTrialsPreview.TabIndex = 15;
      // 
      // columnSubjectName
      // 
      this.columnSubjectName.HeaderText = "SubjectName";
      this.columnSubjectName.Name = "columnSubjectName";
      // 
      // columnTrialSequence
      // 
      this.columnTrialSequence.HeaderText = "Sequence";
      this.columnTrialSequence.Name = "columnTrialSequence";
      // 
      // columnTrialID
      // 
      this.columnTrialID.HeaderText = "TrialID";
      this.columnTrialID.Name = "columnTrialID";
      // 
      // columnCategory
      // 
      this.columnCategory.HeaderText = "Category";
      this.columnCategory.Name = "columnCategory";
      // 
      // columnTrialStartTime
      // 
      this.columnTrialStartTime.HeaderText = "TrialStartTime in ms";
      this.columnTrialStartTime.Name = "columnTrialStartTime";
      // 
      // columnDuration
      // 
      this.columnDuration.HeaderText = "Duration in ms";
      this.columnDuration.Name = "columnDuration";
      // 
      // panel3
      // 
      this.panel3.Controls.Add(this.label6);
      this.panel3.Controls.Add(this.groupBox2);
      this.panel3.Controls.Add(this.groupBox1);
      this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel3.Location = new System.Drawing.Point(0, 0);
      this.panel3.Name = "panel3";
      this.panel3.Size = new System.Drawing.Size(690, 236);
      this.panel3.TabIndex = 35;
      // 
      // label6
      // 
      this.label6.Location = new System.Drawing.Point(527, 125);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(141, 65);
      this.label6.TabIndex = 40;
      this.label6.Text = "Please note: The preview table is truncated by the number of preview lines specif" +
          "ied.";
      // 
      // btnBack
      // 
      this.btnBack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnBack.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.btnBack.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnBack.Location = new System.Drawing.Point(408, 4);
      this.btnBack.Name = "btnBack";
      this.btnBack.Size = new System.Drawing.Size(75, 23);
      this.btnBack.TabIndex = 26;
      this.btnBack.Text = "< &Back";
      // 
      // groupBox4
      // 
      this.groupBox4.Controls.Add(this.dgvTrialsPreview);
      this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
      this.groupBox4.Location = new System.Drawing.Point(0, 0);
      this.groupBox4.Name = "groupBox4";
      this.groupBox4.Size = new System.Drawing.Size(690, 150);
      this.groupBox4.TabIndex = 36;
      this.groupBox4.TabStop = false;
      this.groupBox4.Text = "Trial preview";
      // 
      // ofdAssignmentFile
      // 
      this.ofdAssignmentFile.DefaultExt = "txt";
      this.ofdAssignmentFile.FileName = "stimuli.txt";
      this.ofdAssignmentFile.Filter = "ASCII txt-Files|*.txt|all files|*.*";
      this.ofdAssignmentFile.Title = "Select text file with trial ID to stimulus image file assignments ...";
      // 
      // splitContainer1
      // 
      this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
      this.splitContainer1.IsSplitterFixed = true;
      this.splitContainer1.Location = new System.Drawing.Point(0, 0);
      this.splitContainer1.Name = "splitContainer1";
      this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // splitContainer1.Panel1
      // 
      this.splitContainer1.Panel1.Controls.Add(this.splitContainer3);
      // 
      // splitContainer1.Panel2
      // 
      this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
      this.splitContainer1.Size = new System.Drawing.Size(690, 497);
      this.splitContainer1.SplitterDistance = 300;
      this.splitContainer1.TabIndex = 44;
      // 
      // splitContainer3
      // 
      this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
      this.splitContainer3.IsSplitterFixed = true;
      this.splitContainer3.Location = new System.Drawing.Point(0, 0);
      this.splitContainer3.Name = "splitContainer3";
      this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // splitContainer3.Panel1
      // 
      this.splitContainer3.Panel1.Controls.Add(this.dialogTop1);
      // 
      // splitContainer3.Panel2
      // 
      this.splitContainer3.Panel2.Controls.Add(this.panel3);
      this.splitContainer3.Size = new System.Drawing.Size(690, 300);
      this.splitContainer3.SplitterDistance = 60;
      this.splitContainer3.TabIndex = 0;
      // 
      // dialogTop1
      // 
      this.dialogTop1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(237)))));
      this.dialogTop1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("dialogTop1.BackgroundImage")));
      this.dialogTop1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
      this.dialogTop1.Description = "Please specify the trial list creating process you prefer.";
      this.dialogTop1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.dialogTop1.Location = new System.Drawing.Point(0, 0);
      this.dialogTop1.Logo = global::Ogama.Properties.Resources.MagicWand;
      this.dialogTop1.Name = "dialogTop1";
      this.dialogTop1.Size = new System.Drawing.Size(690, 60);
      this.dialogTop1.TabIndex = 0;
      // 
      // splitContainer2
      // 
      this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
      this.splitContainer2.IsSplitterFixed = true;
      this.splitContainer2.Location = new System.Drawing.Point(0, 0);
      this.splitContainer2.Name = "splitContainer2";
      this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // splitContainer2.Panel1
      // 
      this.splitContainer2.Panel1.Controls.Add(this.groupBox4);
      // 
      // splitContainer2.Panel2
      // 
      this.splitContainer2.Panel2.Controls.Add(this.btnNext);
      this.splitContainer2.Panel2.Controls.Add(this.btnBack);
      this.splitContainer2.Panel2.Controls.Add(this.btnCancel);
      this.splitContainer2.Size = new System.Drawing.Size(690, 193);
      this.splitContainer2.SplitterDistance = 150;
      this.splitContainer2.TabIndex = 0;
      // 
      // ImportTrialsDialog
      // 
      this.AcceptButton = this.btnNext;
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
      this.AutoSize = true;
      this.CancelButton = this.btnCancel;
      this.ClientSize = new System.Drawing.Size(690, 497);
      this.Controls.Add(this.splitContainer1);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "ImportTrialsDialog";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Import assistant ... step 4 - Trials";
      this.Load += new System.EventHandler(this.frmImportTrials_Load);
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmImportTrials_FormClosing);
      this.groupBox2.ResumeLayout(false);
      this.groupBox2.PerformLayout();
      this.spcTableDropDowns.Panel1.ResumeLayout(false);
      this.spcTableDropDowns.Panel2.ResumeLayout(false);
      this.spcTableDropDowns.Panel2.PerformLayout();
      this.spcTableDropDowns.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.dgvAssignments)).EndInit();
      this.grbFileName.ResumeLayout(false);
      this.grbFileName.PerformLayout();
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.nudImportLines)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.dgvTrialsPreview)).EndInit();
      this.panel3.ResumeLayout(false);
      this.groupBox4.ResumeLayout(false);
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel2.ResumeLayout(false);
      this.splitContainer1.ResumeLayout(false);
      this.splitContainer3.Panel1.ResumeLayout(false);
      this.splitContainer3.Panel2.ResumeLayout(false);
      this.splitContainer3.ResumeLayout(false);
      this.splitContainer2.Panel1.ResumeLayout(false);
      this.splitContainer2.Panel2.ResumeLayout(false);
      this.splitContainer2.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.Button btnNext;
    private System.Windows.Forms.ToolTip toolTip1;
    private System.Windows.Forms.DataGridView dgvTrialsPreview;
    private System.Windows.Forms.ComboBox cbbTimeUnit;
    private System.Windows.Forms.Label label12;
    private System.Windows.Forms.NumericUpDown nudImportLines;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Button btnBack;
    private System.Windows.Forms.GroupBox groupBox2;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.DataGridView dgvAssignments;
    private System.Windows.Forms.RadioButton rdbUseMSGLines;
    private System.Windows.Forms.GroupBox grbFileName;
    private System.Windows.Forms.RadioButton rdbEnterTableTrialID;
    private System.Windows.Forms.RadioButton rdbImportTableTrialID;
    private System.Windows.Forms.Button btnOpenAssignFile;
    private System.Windows.Forms.TextBox txbImportAssignFile;
    private System.Windows.Forms.OpenFileDialog ofdAssignmentFile;
    private System.Windows.Forms.TextBox txbTrialMSG;
    private System.Windows.Forms.Panel panel3;
    private System.Windows.Forms.RadioButton rdbUseTrialColumn;
    private System.Windows.Forms.TextBox txbTrialSequenceColumn;
    private System.Windows.Forms.GroupBox groupBox4;
    private System.Windows.Forms.SplitContainer spcTableDropDowns;
    private System.Windows.Forms.ComboBox cbbAssignTrialIDColumn;
    private System.Windows.Forms.ComboBox cbbAssignStartingTimeColumn;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.SplitContainer splitContainer1;
    private System.Windows.Forms.SplitContainer splitContainer3;
    private DialogTop dialogTop1;
    private System.Windows.Forms.SplitContainer splitContainer2;
    private System.Windows.Forms.DataGridViewTextBoxColumn columnSubjectName;
    private System.Windows.Forms.DataGridViewTextBoxColumn columnTrialSequence;
    private System.Windows.Forms.DataGridViewTextBoxColumn columnTrialID;
    private System.Windows.Forms.DataGridViewTextBoxColumn columnCategory;
    private System.Windows.Forms.DataGridViewTextBoxColumn columnTrialStartTime;
    private System.Windows.Forms.DataGridViewTextBoxColumn columnDuration;
    private System.Windows.Forms.DataGridViewTextBoxColumn columnAssignTrialSequence;
    private System.Windows.Forms.DataGridViewTextBoxColumn columnAssignTrialID;
    private System.Windows.Forms.DataGridViewTextBoxColumn columnAssignStartTime;
    private System.Windows.Forms.ComboBox cbbAssignTrialSequenceColumn;
    private System.Windows.Forms.Label label1;
  }
}
