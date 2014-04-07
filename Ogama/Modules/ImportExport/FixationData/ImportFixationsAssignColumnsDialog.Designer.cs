namespace Ogama.Modules.ImportExport.FixationData
{
  using Ogama.Modules.Common.Controls;

  partial class ImportFixationsAssignColumnsDialog
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImportFixationsAssignColumnsDialog));
      this.dGVAssignments = new System.Windows.Forms.DataGridView();
      this.ColumnFixationTableColumns = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.ColumnImportColumns = new System.Windows.Forms.DataGridViewComboBoxColumn();
      this.dataSetImport = new System.Data.DataSet();
      this.ofdImport = new System.Windows.Forms.OpenFileDialog();
      this.btnCancel = new System.Windows.Forms.Button();
      this.btnNext = new System.Windows.Forms.Button();
      this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
      this.label14 = new System.Windows.Forms.Label();
      this.label11 = new System.Windows.Forms.Label();
      this.label6 = new System.Windows.Forms.Label();
      this.label5 = new System.Windows.Forms.Label();
      this.label10 = new System.Windows.Forms.Label();
      this.btnBack = new System.Windows.Forms.Button();
      this.splitContainer2 = new System.Windows.Forms.SplitContainer();
      this.label2 = new System.Windows.Forms.Label();
      this.label1 = new System.Windows.Forms.Label();
      this.label17 = new System.Windows.Forms.Label();
      this.label4 = new System.Windows.Forms.Label();
      this.label9 = new System.Windows.Forms.Label();
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      this.dialogTop1 = new Ogama.Modules.Common.Controls.DialogTop();
      this.splitContainer3 = new System.Windows.Forms.SplitContainer();
      this.label15 = new System.Windows.Forms.Label();
      ((System.ComponentModel.ISupportInitialize)(this.dGVAssignments)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.dataSetImport)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
      this.splitContainer2.Panel1.SuspendLayout();
      this.splitContainer2.Panel2.SuspendLayout();
      this.splitContainer2.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
      this.splitContainer3.Panel1.SuspendLayout();
      this.splitContainer3.Panel2.SuspendLayout();
      this.splitContainer3.SuspendLayout();
      this.SuspendLayout();
      // 
      // dGVAssignments
      // 
      this.dGVAssignments.AllowUserToAddRows = false;
      this.dGVAssignments.AllowUserToDeleteRows = false;
      this.dGVAssignments.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
      this.dGVAssignments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dGVAssignments.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnFixationTableColumns,
            this.ColumnImportColumns});
      this.dGVAssignments.Dock = System.Windows.Forms.DockStyle.Fill;
      this.dGVAssignments.Location = new System.Drawing.Point(0, 0);
      this.dGVAssignments.Name = "dGVAssignments";
      this.dGVAssignments.Size = new System.Drawing.Size(369, 270);
      this.dGVAssignments.TabIndex = 17;
      this.toolTip1.SetToolTip(this.dGVAssignments, "Please select from the dropdowns \r\nthe columns from your text file \r\nthat best ma" +
        "tch the given\r\nOgama columns.");
      this.dGVAssignments.CellValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGvAssignmentsCellValidated);
      this.dGVAssignments.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.DGvAssignmentsDataError);
      // 
      // ColumnFixationTableColumns
      // 
      this.ColumnFixationTableColumns.HeaderText = "FixationTableColumns";
      this.ColumnFixationTableColumns.Name = "ColumnFixationTableColumns";
      this.ColumnFixationTableColumns.ReadOnly = true;
      // 
      // ColumnImportColumns
      // 
      this.ColumnImportColumns.HeaderText = "ImportFileColumns";
      this.ColumnImportColumns.Items.AddRange(new object[] {
            " "});
      this.ColumnImportColumns.Name = "ColumnImportColumns";
      // 
      // dataSetImport
      // 
      this.dataSetImport.DataSetName = "ImportDataSet";
      // 
      // ofdImport
      // 
      this.ofdImport.DefaultExt = "txt";
      this.ofdImport.Filter = "ASCII-Files (*.csv,*.txt,*.asc)|*.csv;*.txt;*.asc|Text-Files (*.txt)|*.txt|Comma " +
    "separated-values (*.csv)|*.csv|ASC-Files (*.asc)|*.asc|All files|*.*";
      this.ofdImport.Title = "Please select ASCII file to import";
      // 
      // btnCancel
      // 
      this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Abort;
      this.btnCancel.Location = new System.Drawing.Point(486, 18);
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
      this.btnNext.Location = new System.Drawing.Point(405, 18);
      this.btnNext.Name = "btnNext";
      this.btnNext.Size = new System.Drawing.Size(75, 23);
      this.btnNext.TabIndex = 26;
      this.btnNext.Text = "&Next >";
      this.toolTip1.SetToolTip(this.btnNext, "Start importing with given settings.");
      this.btnNext.Click += new System.EventHandler(this.BtnNextClick);
      // 
      // label14
      // 
      this.label14.AutoSize = true;
      this.label14.Location = new System.Drawing.Point(0, 48);
      this.label14.Name = "label14";
      this.label14.Size = new System.Drawing.Size(200, 13);
      this.label14.TabIndex = 51;
      this.label14.Text = "... gives the trials presentation sequence.";
      this.toolTip1.SetToolTip(this.label14, "unique sequence (1,2,3,etc. ) for each subject");
      // 
      // label11
      // 
      this.label11.AutoSize = true;
      this.label11.Location = new System.Drawing.Point(11, 114);
      this.label11.Name = "label11";
      this.label11.Size = new System.Drawing.Size(188, 13);
      this.label11.TabIndex = 50;
      this.label11.Text = "... gives the order of the fixation in trial.";
      this.toolTip1.SetToolTip(this.label11, "the index of the fixation relative to the trial");
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Location = new System.Drawing.Point(44, 223);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(153, 13);
      this.label6.TabIndex = 44;
      this.label6.Text = "... gives the fixations y-position.";
      this.toolTip1.SetToolTip(this.label6, "measured in pixel");
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(44, 201);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(153, 13);
      this.label5.TabIndex = 43;
      this.label5.Text = "... gives the fixations x-position.";
      this.toolTip1.SetToolTip(this.label5, "measured in pixel");
      // 
      // label10
      // 
      this.label10.AutoSize = true;
      this.label10.Location = new System.Drawing.Point(31, 70);
      this.label10.Name = "label10";
      this.label10.Size = new System.Drawing.Size(168, 13);
      this.label10.TabIndex = 36;
      this.label10.Text = " ... distinguishes the different trials.";
      this.toolTip1.SetToolTip(this.label10, "Unique identifier for the trial (ID)");
      // 
      // btnBack
      // 
      this.btnBack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnBack.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.btnBack.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnBack.Location = new System.Drawing.Point(324, 18);
      this.btnBack.Name = "btnBack";
      this.btnBack.Size = new System.Drawing.Size(75, 23);
      this.btnBack.TabIndex = 30;
      this.btnBack.Text = "< &Back";
      // 
      // splitContainer2
      // 
      this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
      this.splitContainer2.IsSplitterFixed = true;
      this.splitContainer2.Location = new System.Drawing.Point(0, 0);
      this.splitContainer2.Name = "splitContainer2";
      // 
      // splitContainer2.Panel1
      // 
      this.splitContainer2.Panel1.Controls.Add(this.label15);
      this.splitContainer2.Panel1.Controls.Add(this.label14);
      this.splitContainer2.Panel1.Controls.Add(this.label11);
      this.splitContainer2.Panel1.Controls.Add(this.label2);
      this.splitContainer2.Panel1.Controls.Add(this.label6);
      this.splitContainer2.Panel1.Controls.Add(this.label5);
      this.splitContainer2.Panel1.Controls.Add(this.label1);
      this.splitContainer2.Panel1.Controls.Add(this.label17);
      this.splitContainer2.Panel1.Controls.Add(this.label4);
      this.splitContainer2.Panel1.Controls.Add(this.label10);
      this.splitContainer2.Panel1.Controls.Add(this.label9);
      // 
      // splitContainer2.Panel2
      // 
      this.splitContainer2.Panel2.Controls.Add(this.dGVAssignments);
      this.splitContainer2.Size = new System.Drawing.Size(573, 270);
      this.splitContainer2.SplitterDistance = 200;
      this.splitContainer2.TabIndex = 32;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label2.Location = new System.Drawing.Point(36, 7);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(151, 13);
      this.label2.TabIndex = 48;
      this.label2.Text = "Select the column that ...";
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(53, 157);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(144, 13);
      this.label1.TabIndex = 45;
      this.label1.Text = "... gives the fixation end time.";
      // 
      // label17
      // 
      this.label17.AutoSize = true;
      this.label17.Location = new System.Drawing.Point(53, 135);
      this.label17.Name = "label17";
      this.label17.Size = new System.Drawing.Size(146, 13);
      this.label17.TabIndex = 45;
      this.label17.Text = "... gives the fixation start time.";
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(55, 179);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(142, 13);
      this.label4.TabIndex = 46;
      this.label4.Text = "... gives the fixation duration.";
      // 
      // label9
      // 
      this.label9.AutoSize = true;
      this.label9.Location = new System.Drawing.Point(65, 26);
      this.label9.Name = "label9";
      this.label9.Size = new System.Drawing.Size(134, 13);
      this.label9.TabIndex = 35;
      this.label9.Text = " ... gives the subject name.";
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
      this.splitContainer1.Panel1.Controls.Add(this.dialogTop1);
      // 
      // splitContainer1.Panel2
      // 
      this.splitContainer1.Panel2.Controls.Add(this.splitContainer3);
      this.splitContainer1.Size = new System.Drawing.Size(573, 388);
      this.splitContainer1.SplitterDistance = 60;
      this.splitContainer1.SplitterWidth = 1;
      this.splitContainer1.TabIndex = 52;
      // 
      // dialogTop1
      // 
      this.dialogTop1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(237)))));
      this.dialogTop1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("dialogTop1.BackgroundImage")));
      this.dialogTop1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
      this.dialogTop1.Description = "Assign columns: Please select from the dropdown settings the columns, that match " +
    "the columns in OGAMAs fixation table.";
      this.dialogTop1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.dialogTop1.Location = new System.Drawing.Point(0, 0);
      this.dialogTop1.Logo = global::Ogama.Properties.Resources.MagicWand;
      this.dialogTop1.Name = "dialogTop1";
      this.dialogTop1.Size = new System.Drawing.Size(573, 60);
      this.dialogTop1.TabIndex = 0;
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
      this.splitContainer3.Panel1.Controls.Add(this.splitContainer2);
      // 
      // splitContainer3.Panel2
      // 
      this.splitContainer3.Panel2.Controls.Add(this.btnNext);
      this.splitContainer3.Panel2.Controls.Add(this.btnCancel);
      this.splitContainer3.Panel2.Controls.Add(this.btnBack);
      this.splitContainer3.Size = new System.Drawing.Size(573, 327);
      this.splitContainer3.SplitterDistance = 270;
      this.splitContainer3.TabIndex = 0;
      // 
      // label15
      // 
      this.label15.AutoSize = true;
      this.label15.Location = new System.Drawing.Point(78, 91);
      this.label15.Name = "label15";
      this.label15.Size = new System.Drawing.Size(120, 13);
      this.label15.TabIndex = 52;
      this.label15.Text = "... gives the trials image.";
      // 
      // ImportFixationsAssignColumnsDialog
      // 
      this.AcceptButton = this.btnNext;
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
      this.CancelButton = this.btnCancel;
      this.ClientSize = new System.Drawing.Size(573, 388);
      this.Controls.Add(this.splitContainer1);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "ImportFixationsAssignColumnsDialog";
      this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Fixations import assistant ... step 3 - assign columns";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmImportFixationsAssignColumnsFormClosing);
      this.Load += new System.EventHandler(this.FrmImportFixationsAssignColumnsLoad);
      ((System.ComponentModel.ISupportInitialize)(this.dGVAssignments)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.dataSetImport)).EndInit();
      this.splitContainer2.Panel1.ResumeLayout(false);
      this.splitContainer2.Panel1.PerformLayout();
      this.splitContainer2.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
      this.splitContainer2.ResumeLayout(false);
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
      this.splitContainer1.ResumeLayout(false);
      this.splitContainer3.Panel1.ResumeLayout(false);
      this.splitContainer3.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
      this.splitContainer3.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.DataGridView dGVAssignments;
    private System.Data.DataSet dataSetImport;
    private System.Windows.Forms.OpenFileDialog ofdImport;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.Button btnNext;
    private System.Windows.Forms.ToolTip toolTip1;
    private System.Windows.Forms.Button btnBack;
    private System.Windows.Forms.SplitContainer splitContainer2;
    private System.Windows.Forms.Label label11;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.Label label17;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Label label10;
    private System.Windows.Forms.Label label9;
    private System.Windows.Forms.DataGridViewTextBoxColumn ColumnFixationTableColumns;
    private System.Windows.Forms.DataGridViewComboBoxColumn ColumnImportColumns;
    private System.Windows.Forms.Label label14;
    private System.Windows.Forms.SplitContainer splitContainer1;
    private DialogTop dialogTop1;
    private System.Windows.Forms.SplitContainer splitContainer3;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label15;


  }
}
