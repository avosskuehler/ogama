namespace Ogama.Modules.ImportExport.Common
{
  using Ogama.Modules.Common.Controls;

  partial class ImportImagesDialog
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImportImagesDialog));
      this.dataSetImport = new System.Data.DataSet();
      this.btnCancel = new System.Windows.Forms.Button();
      this.btnImport = new System.Windows.Forms.Button();
      this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
      this.btnOpenAssignFile = new System.Windows.Forms.Button();
      this.txbImportAssignFile = new System.Windows.Forms.TextBox();
      this.rdbEnterTableImages = new System.Windows.Forms.RadioButton();
      this.rdbImportTableImages = new System.Windows.Forms.RadioButton();
      this.dgvAssignments = new System.Windows.Forms.DataGridView();
      this.columnAssignTrialID = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.columnAssignStimulusFile = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.rdbiViewX = new System.Windows.Forms.RadioButton();
      this.label2 = new System.Windows.Forms.Label();
      this.dgvTrialsPreview = new System.Windows.Forms.DataGridView();
      this.columnSubjectName = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.columnTrialSequence = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.columnTrialID = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.columnCategory = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.columnStimulusFile = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.columnTrialStartTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.columnDuration = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.panel3 = new System.Windows.Forms.Panel();
      this.spcTableDropDowns = new System.Windows.Forms.SplitContainer();
      this.cbbTrialIDColumn = new System.Windows.Forms.ComboBox();
      this.cbbStimulusFileColumn = new System.Windows.Forms.ComboBox();
      this.label5 = new System.Windows.Forms.Label();
      this.label4 = new System.Windows.Forms.Label();
      this.label3 = new System.Windows.Forms.Label();
      this.nudImportLines = new System.Windows.Forms.NumericUpDown();
      this.txbStimulusFileEndings = new System.Windows.Forms.TextBox();
      this.txbStimulusFileColumn = new System.Windows.Forms.TextBox();
      this.rdbUseStimulusFileColumn = new System.Windows.Forms.RadioButton();
      this.rdbSearchForImageEndings = new System.Windows.Forms.RadioButton();
      this.btnBack = new System.Windows.Forms.Button();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.ofdAssignmentFile = new System.Windows.Forms.OpenFileDialog();
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      this.splitContainer3 = new System.Windows.Forms.SplitContainer();
      this.dialogTop1 = new DialogTop();
      this.splitContainer2 = new System.Windows.Forms.SplitContainer();
      ((System.ComponentModel.ISupportInitialize)(this.dataSetImport)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.dgvAssignments)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.dgvTrialsPreview)).BeginInit();
      this.panel3.SuspendLayout();
      this.spcTableDropDowns.Panel1.SuspendLayout();
      this.spcTableDropDowns.Panel2.SuspendLayout();
      this.spcTableDropDowns.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.nudImportLines)).BeginInit();
      this.groupBox1.SuspendLayout();
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
      // dataSetImport
      // 
      this.dataSetImport.DataSetName = "ImportDataSet";
      // 
      // btnCancel
      // 
      this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Abort;
      this.btnCancel.Location = new System.Drawing.Point(604, 5);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(75, 23);
      this.btnCancel.TabIndex = 27;
      this.btnCancel.Text = "&Cancel";
      this.toolTip1.SetToolTip(this.btnCancel, "Cancel import.");
      // 
      // btnImport
      // 
      this.btnImport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnImport.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.btnImport.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.btnImport.Location = new System.Drawing.Point(523, 5);
      this.btnImport.Name = "btnImport";
      this.btnImport.Size = new System.Drawing.Size(75, 23);
      this.btnImport.TabIndex = 26;
      this.btnImport.Text = "Start &Import";
      this.toolTip1.SetToolTip(this.btnImport, "Start importing with given settings.");
      this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
      // 
      // btnOpenAssignFile
      // 
      this.btnOpenAssignFile.Image = global::Ogama.Properties.Resources.openfolderHS;
      this.btnOpenAssignFile.Location = new System.Drawing.Point(139, 127);
      this.btnOpenAssignFile.Name = "btnOpenAssignFile";
      this.btnOpenAssignFile.Size = new System.Drawing.Size(24, 22);
      this.btnOpenAssignFile.TabIndex = 25;
      this.btnOpenAssignFile.UseVisualStyleBackColor = true;
      this.btnOpenAssignFile.Visible = false;
      this.btnOpenAssignFile.Click += new System.EventHandler(this.btnOpenAssignFile_Click);
      // 
      // txbImportAssignFile
      // 
      this.txbImportAssignFile.Location = new System.Drawing.Point(169, 129);
      this.txbImportAssignFile.Name = "txbImportAssignFile";
      this.txbImportAssignFile.ReadOnly = true;
      this.txbImportAssignFile.Size = new System.Drawing.Size(123, 20);
      this.txbImportAssignFile.TabIndex = 24;
      this.txbImportAssignFile.Visible = false;
      // 
      // rdbEnterTableImages
      // 
      this.rdbEnterTableImages.AutoSize = true;
      this.rdbEnterTableImages.Location = new System.Drawing.Point(9, 107);
      this.rdbEnterTableImages.Name = "rdbEnterTableImages";
      this.rdbEnterTableImages.Size = new System.Drawing.Size(75, 17);
      this.rdbEnterTableImages.TabIndex = 23;
      this.rdbEnterTableImages.Text = "enter table";
      this.toolTip1.SetToolTip(this.rdbEnterTableImages, "Assign custom trial ID to stimulus filenames");
      this.rdbEnterTableImages.UseVisualStyleBackColor = true;
      this.rdbEnterTableImages.CheckedChanged += new System.EventHandler(this.rdbImportMode_CheckedChanged);
      // 
      // rdbImportTableImages
      // 
      this.rdbImportTableImages.AutoSize = true;
      this.rdbImportTableImages.Location = new System.Drawing.Point(9, 130);
      this.rdbImportTableImages.Name = "rdbImportTableImages";
      this.rdbImportTableImages.Size = new System.Drawing.Size(91, 17);
      this.rdbImportTableImages.TabIndex = 23;
      this.rdbImportTableImages.Text = "import table ...";
      this.toolTip1.SetToolTip(this.rdbImportTableImages, "Import a table with trial ID to image filename assignments. \r\n(Two column file, f" +
              "irst column with trial ID, second with image filename including ending)");
      this.rdbImportTableImages.UseVisualStyleBackColor = true;
      this.rdbImportTableImages.CheckedChanged += new System.EventHandler(this.rdbImportMode_CheckedChanged);
      // 
      // dgvAssignments
      // 
      this.dgvAssignments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dgvAssignments.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnAssignTrialID,
            this.columnAssignStimulusFile});
      this.dgvAssignments.Dock = System.Windows.Forms.DockStyle.Fill;
      this.dgvAssignments.Location = new System.Drawing.Point(0, 0);
      this.dgvAssignments.Name = "dgvAssignments";
      this.dgvAssignments.Size = new System.Drawing.Size(374, 132);
      this.dgvAssignments.TabIndex = 40;
      this.dgvAssignments.Visible = false;
      this.dgvAssignments.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvAssignments_CellLeave);
      this.dgvAssignments.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgvAssignments_CellValidating);
      this.dgvAssignments.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgvAssignments_EditingControlShowing);
      // 
      // columnAssignTrialID
      // 
      this.columnAssignTrialID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
      this.columnAssignTrialID.HeaderText = "TrialID";
      this.columnAssignTrialID.Name = "columnAssignTrialID";
      this.columnAssignTrialID.Width = 50;
      // 
      // columnAssignStimulusFile
      // 
      this.columnAssignStimulusFile.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
      this.columnAssignStimulusFile.HeaderText = "Stimulus filename";
      this.columnAssignStimulusFile.Name = "columnAssignStimulusFile";
      // 
      // rdbiViewX
      // 
      this.rdbiViewX.AutoSize = true;
      this.rdbiViewX.Checked = true;
      this.rdbiViewX.Location = new System.Drawing.Point(9, 60);
      this.rdbiViewX.Name = "rdbiViewX";
      this.rdbiViewX.Size = new System.Drawing.Size(128, 17);
      this.rdbiViewX.TabIndex = 22;
      this.rdbiViewX.TabStop = true;
      this.rdbiViewX.Text = "use iViewX MSG lines";
      this.toolTip1.SetToolTip(this.rdbiViewX, "Triggers for lines containing \"scene image: bitmap.bmp\" or \"# Message: bitmap.bmp" +
              "\".\r\nAll rows after such an entry are assigned to the trial with the image \"bitma" +
              "p.bmp\".");
      this.rdbiViewX.UseVisualStyleBackColor = true;
      this.rdbiViewX.CheckedChanged += new System.EventHandler(this.rdbImportMode_CheckedChanged);
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(9, 3);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(169, 13);
      this.label2.TabIndex = 20;
      this.label2.Text = "How to detect stimuli image files ...";
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
            this.columnStimulusFile,
            this.columnTrialStartTime,
            this.columnDuration});
      this.dgvTrialsPreview.Dock = System.Windows.Forms.DockStyle.Fill;
      this.dgvTrialsPreview.Location = new System.Drawing.Point(3, 16);
      this.dgvTrialsPreview.Name = "dgvTrialsPreview";
      this.dgvTrialsPreview.Size = new System.Drawing.Size(685, 151);
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
      // columnStimulusFile
      // 
      this.columnStimulusFile.HeaderText = "StimulusFile";
      this.columnStimulusFile.Name = "columnStimulusFile";
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
      this.panel3.Controls.Add(this.spcTableDropDowns);
      this.panel3.Controls.Add(this.label3);
      this.panel3.Controls.Add(this.nudImportLines);
      this.panel3.Controls.Add(this.txbStimulusFileEndings);
      this.panel3.Controls.Add(this.txbStimulusFileColumn);
      this.panel3.Controls.Add(this.btnOpenAssignFile);
      this.panel3.Controls.Add(this.txbImportAssignFile);
      this.panel3.Controls.Add(this.rdbImportTableImages);
      this.panel3.Controls.Add(this.rdbUseStimulusFileColumn);
      this.panel3.Controls.Add(this.rdbSearchForImageEndings);
      this.panel3.Controls.Add(this.rdbEnterTableImages);
      this.panel3.Controls.Add(this.rdbiViewX);
      this.panel3.Controls.Add(this.label2);
      this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel3.Location = new System.Drawing.Point(0, 0);
      this.panel3.Name = "panel3";
      this.panel3.Size = new System.Drawing.Size(691, 196);
      this.panel3.TabIndex = 35;
      // 
      // spcTableDropDowns
      // 
      this.spcTableDropDowns.Location = new System.Drawing.Point(308, 3);
      this.spcTableDropDowns.Name = "spcTableDropDowns";
      this.spcTableDropDowns.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // spcTableDropDowns.Panel1
      // 
      this.spcTableDropDowns.Panel1.Controls.Add(this.dgvAssignments);
      // 
      // spcTableDropDowns.Panel2
      // 
      this.spcTableDropDowns.Panel2.Controls.Add(this.cbbTrialIDColumn);
      this.spcTableDropDowns.Panel2.Controls.Add(this.cbbStimulusFileColumn);
      this.spcTableDropDowns.Panel2.Controls.Add(this.label5);
      this.spcTableDropDowns.Panel2.Controls.Add(this.label4);
      this.spcTableDropDowns.Size = new System.Drawing.Size(374, 188);
      this.spcTableDropDowns.SplitterDistance = 132;
      this.spcTableDropDowns.TabIndex = 44;
      // 
      // cbbTrialIDColumn
      // 
      this.cbbTrialIDColumn.FormattingEnabled = true;
      this.cbbTrialIDColumn.Location = new System.Drawing.Point(144, 5);
      this.cbbTrialIDColumn.Name = "cbbTrialIDColumn";
      this.cbbTrialIDColumn.Size = new System.Drawing.Size(112, 21);
      this.cbbTrialIDColumn.TabIndex = 4;
      this.toolTip1.SetToolTip(this.cbbTrialIDColumn, "Specify the column in the import file which contains the trial ids.");
      this.cbbTrialIDColumn.SelectionChangeCommitted += new System.EventHandler(this.cbbTrialIDColumn_SelectionChangeCommitted);
      // 
      // cbbStimulusFileColumn
      // 
      this.cbbStimulusFileColumn.FormattingEnabled = true;
      this.cbbStimulusFileColumn.Location = new System.Drawing.Point(144, 29);
      this.cbbStimulusFileColumn.Name = "cbbStimulusFileColumn";
      this.cbbStimulusFileColumn.Size = new System.Drawing.Size(112, 21);
      this.cbbStimulusFileColumn.TabIndex = 5;
      this.toolTip1.SetToolTip(this.cbbStimulusFileColumn, "Specify the column in the import file which contains the stimulus file name with " +
              "file ending.");
      this.cbbStimulusFileColumn.SelectionChangeCommitted += new System.EventHandler(this.cbbStimulusFileColumn_SelectionChangeCommitted);
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(11, 8);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(81, 13);
      this.label5.TabIndex = 2;
      this.label5.Text = "Trial ID column:";
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(11, 32);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(128, 13);
      this.label4.TabIndex = 3;
      this.label4.Text = "Stimulus filename column:";
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(26, 167);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(135, 13);
      this.label3.TabIndex = 43;
      this.label3.Text = "Number of previewed lines:";
      // 
      // nudImportLines
      // 
      this.nudImportLines.Increment = new decimal(new int[] {
            5000,
            0,
            0,
            0});
      this.nudImportLines.Location = new System.Drawing.Point(169, 165);
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
      this.nudImportLines.Size = new System.Drawing.Size(123, 20);
      this.nudImportLines.TabIndex = 42;
      this.nudImportLines.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.nudImportLines.ThousandsSeparator = true;
      this.nudImportLines.Value = new decimal(new int[] {
            20000,
            0,
            0,
            0});
      this.nudImportLines.ValueChanged += new System.EventHandler(this.nudImportLines_ValueChanged);
      // 
      // txbStimulusFileEndings
      // 
      this.txbStimulusFileEndings.Location = new System.Drawing.Point(169, 83);
      this.txbStimulusFileEndings.Name = "txbStimulusFileEndings";
      this.txbStimulusFileEndings.Size = new System.Drawing.Size(123, 20);
      this.txbStimulusFileEndings.TabIndex = 41;
      this.txbStimulusFileEndings.Text = "bmp";
      this.txbStimulusFileEndings.TextChanged += new System.EventHandler(this.txbStimulusFileEndings_TextChanged);
      // 
      // txbStimulusFileColumn
      // 
      this.txbStimulusFileColumn.Location = new System.Drawing.Point(169, 36);
      this.txbStimulusFileColumn.Name = "txbStimulusFileColumn";
      this.txbStimulusFileColumn.ReadOnly = true;
      this.txbStimulusFileColumn.Size = new System.Drawing.Size(123, 20);
      this.txbStimulusFileColumn.TabIndex = 41;
      // 
      // rdbUseStimulusFileColumn
      // 
      this.rdbUseStimulusFileColumn.AutoSize = true;
      this.rdbUseStimulusFileColumn.Enabled = false;
      this.rdbUseStimulusFileColumn.Location = new System.Drawing.Point(9, 37);
      this.rdbUseStimulusFileColumn.Name = "rdbUseStimulusFileColumn";
      this.rdbUseStimulusFileColumn.Size = new System.Drawing.Size(126, 17);
      this.rdbUseStimulusFileColumn.TabIndex = 23;
      this.rdbUseStimulusFileColumn.Text = "use image file column";
      this.toolTip1.SetToolTip(this.rdbUseStimulusFileColumn, "All consecutive rows containing the image file specified in the image column are " +
              "assigned to that trial.");
      this.rdbUseStimulusFileColumn.UseVisualStyleBackColor = true;
      this.rdbUseStimulusFileColumn.CheckedChanged += new System.EventHandler(this.rdbImportMode_CheckedChanged);
      // 
      // rdbSearchForImageEndings
      // 
      this.rdbSearchForImageEndings.AutoSize = true;
      this.rdbSearchForImageEndings.Location = new System.Drawing.Point(9, 84);
      this.rdbSearchForImageEndings.Name = "rdbSearchForImageEndings";
      this.rdbSearchForImageEndings.Size = new System.Drawing.Size(154, 17);
      this.rdbSearchForImageEndings.TabIndex = 23;
      this.rdbSearchForImageEndings.Text = "search for image file ending";
      this.toolTip1.SetToolTip(this.rdbSearchForImageEndings, "Searches for rows with a string entry ending on \".bmp\" or what ending you specify" +
              ".\r\nAll rows following this image row are assigned to the trial with the image fi" +
              "le found in this row.");
      this.rdbSearchForImageEndings.UseVisualStyleBackColor = true;
      this.rdbSearchForImageEndings.CheckedChanged += new System.EventHandler(this.rdbImportMode_CheckedChanged);
      // 
      // btnBack
      // 
      this.btnBack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnBack.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.btnBack.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnBack.Location = new System.Drawing.Point(442, 5);
      this.btnBack.Name = "btnBack";
      this.btnBack.Size = new System.Drawing.Size(75, 23);
      this.btnBack.TabIndex = 26;
      this.btnBack.Text = "< &Back";
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.dgvTrialsPreview);
      this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.groupBox1.Location = new System.Drawing.Point(0, 0);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(691, 170);
      this.groupBox1.TabIndex = 36;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Trial preview";
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
      this.splitContainer1.Size = new System.Drawing.Size(691, 478);
      this.splitContainer1.SplitterDistance = 260;
      this.splitContainer1.TabIndex = 45;
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
      this.splitContainer3.Size = new System.Drawing.Size(691, 260);
      this.splitContainer3.SplitterDistance = 60;
      this.splitContainer3.TabIndex = 0;
      // 
      // dialogTop1
      // 
      this.dialogTop1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(237)))));
      this.dialogTop1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("dialogTop1.BackgroundImage")));
      this.dialogTop1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
      this.dialogTop1.Description = "Please specify where to get the stimuli images for the trials.";
      this.dialogTop1.Dock = System.Windows.Forms.DockStyle.Top;
      this.dialogTop1.Location = new System.Drawing.Point(0, 0);
      this.dialogTop1.Logo = global::Ogama.Properties.Resources.MagicWand;
      this.dialogTop1.Name = "dialogTop1";
      this.dialogTop1.Size = new System.Drawing.Size(691, 60);
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
      this.splitContainer2.Panel1.Controls.Add(this.groupBox1);
      // 
      // splitContainer2.Panel2
      // 
      this.splitContainer2.Panel2.Controls.Add(this.btnImport);
      this.splitContainer2.Panel2.Controls.Add(this.btnBack);
      this.splitContainer2.Panel2.Controls.Add(this.btnCancel);
      this.splitContainer2.Size = new System.Drawing.Size(691, 214);
      this.splitContainer2.SplitterDistance = 170;
      this.splitContainer2.TabIndex = 0;
      // 
      // ImportImagesDialog
      // 
      this.AcceptButton = this.btnImport;
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
      this.AutoSize = true;
      this.CancelButton = this.btnCancel;
      this.ClientSize = new System.Drawing.Size(691, 478);
      this.Controls.Add(this.splitContainer1);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "ImportImagesDialog";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Import assistant ... step 5 - Images";
      this.Load += new System.EventHandler(this.frmImportImages_Load);
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmImportImages_FormClosing);
      ((System.ComponentModel.ISupportInitialize)(this.dataSetImport)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.dgvAssignments)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.dgvTrialsPreview)).EndInit();
      this.panel3.ResumeLayout(false);
      this.panel3.PerformLayout();
      this.spcTableDropDowns.Panel1.ResumeLayout(false);
      this.spcTableDropDowns.Panel2.ResumeLayout(false);
      this.spcTableDropDowns.Panel2.PerformLayout();
      this.spcTableDropDowns.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.nudImportLines)).EndInit();
      this.groupBox1.ResumeLayout(false);
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

    private System.Data.DataSet dataSetImport;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.Button btnImport;
    private System.Windows.Forms.ToolTip toolTip1;
    private System.Windows.Forms.DataGridView dgvTrialsPreview;
    private System.Windows.Forms.Button btnBack;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.DataGridView dgvAssignments;
    private System.Windows.Forms.RadioButton rdbiViewX;
    private System.Windows.Forms.RadioButton rdbEnterTableImages;
    private System.Windows.Forms.RadioButton rdbImportTableImages;
    private System.Windows.Forms.Button btnOpenAssignFile;
    private System.Windows.Forms.TextBox txbImportAssignFile;
    private System.Windows.Forms.OpenFileDialog ofdAssignmentFile;
    private System.Windows.Forms.Panel panel3;
    private System.Windows.Forms.TextBox txbStimulusFileColumn;
    private System.Windows.Forms.RadioButton rdbUseStimulusFileColumn;
    private System.Windows.Forms.TextBox txbStimulusFileEndings;
    private System.Windows.Forms.RadioButton rdbSearchForImageEndings;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.NumericUpDown nudImportLines;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.ComboBox cbbTrialIDColumn;
    private System.Windows.Forms.ComboBox cbbStimulusFileColumn;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.SplitContainer spcTableDropDowns;
    private System.Windows.Forms.SplitContainer splitContainer1;
    private System.Windows.Forms.SplitContainer splitContainer3;
    private DialogTop dialogTop1;
    private System.Windows.Forms.SplitContainer splitContainer2;
    private System.Windows.Forms.DataGridViewTextBoxColumn columnAssignTrialID;
    private System.Windows.Forms.DataGridViewTextBoxColumn columnAssignStimulusFile;
    private System.Windows.Forms.DataGridViewTextBoxColumn columnSubjectName;
    private System.Windows.Forms.DataGridViewTextBoxColumn columnTrialSequence;
    private System.Windows.Forms.DataGridViewTextBoxColumn columnTrialID;
    private System.Windows.Forms.DataGridViewTextBoxColumn columnCategory;
    private System.Windows.Forms.DataGridViewTextBoxColumn columnStimulusFile;
    private System.Windows.Forms.DataGridViewTextBoxColumn columnTrialStartTime;
    private System.Windows.Forms.DataGridViewTextBoxColumn columnDuration;


  }
}
