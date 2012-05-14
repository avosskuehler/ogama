namespace Ogama.Modules.ImportExport.AOIData
{
  using Ogama.Modules.Common.Controls;

  partial class ImportAOIAssignColumnsDialog
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImportAOIAssignColumnsDialog));
      this.dGVAssignments = new System.Windows.Forms.DataGridView();
      this.ColumnAOITableColumns = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.ColumnTextFileColumns = new System.Windows.Forms.DataGridViewComboBoxColumn();
      this.rdbSimpleRects = new System.Windows.Forms.RadioButton();
      this.rdbOgamaFormat = new System.Windows.Forms.RadioButton();
      this.ofdImport = new System.Windows.Forms.OpenFileDialog();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.btnCancel = new System.Windows.Forms.Button();
      this.btnImport = new System.Windows.Forms.Button();
      this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
      this.btnBack = new System.Windows.Forms.Button();
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      this.splitContainer2 = new System.Windows.Forms.SplitContainer();
      this.dialogTop1 = new DialogTop();
      this.splitContainer3 = new System.Windows.Forms.SplitContainer();
      ((System.ComponentModel.ISupportInitialize)(this.dGVAssignments)).BeginInit();
      this.groupBox1.SuspendLayout();
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      this.splitContainer2.Panel1.SuspendLayout();
      this.splitContainer2.Panel2.SuspendLayout();
      this.splitContainer2.SuspendLayout();
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
            this.ColumnAOITableColumns,
            this.ColumnTextFileColumns});
      this.dGVAssignments.Dock = System.Windows.Forms.DockStyle.Fill;
      this.dGVAssignments.Location = new System.Drawing.Point(0, 0);
      this.dGVAssignments.Name = "dGVAssignments";
      this.dGVAssignments.Size = new System.Drawing.Size(591, 215);
      this.dGVAssignments.TabIndex = 17;
      this.toolTip1.SetToolTip(this.dGVAssignments, "Please select from the dropdowns \r\nthe columns from your text file \r\nthat best ma" +
              "tch the given\r\nOgama columns.");
      this.dGVAssignments.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dGVAssignments_DataError);
      // 
      // ColumnAOITableColumns
      // 
      this.ColumnAOITableColumns.HeaderText = "AOITableColumns";
      this.ColumnAOITableColumns.Name = "ColumnAOITableColumns";
      this.ColumnAOITableColumns.ReadOnly = true;
      // 
      // ColumnTextFileColumns
      // 
      this.ColumnTextFileColumns.HeaderText = "TextFileColumns";
      this.ColumnTextFileColumns.Name = "ColumnTextFileColumns";
      // 
      // rdbSimpleRects
      // 
      this.rdbSimpleRects.AutoSize = true;
      this.rdbSimpleRects.Location = new System.Drawing.Point(13, 19);
      this.rdbSimpleRects.Name = "rdbSimpleRects";
      this.rdbSimpleRects.Size = new System.Drawing.Size(141, 17);
      this.rdbSimpleRects.TabIndex = 19;
      this.rdbSimpleRects.Text = "Import simple rectangles.";
      this.toolTip1.SetToolTip(this.rdbSimpleRects, "Use this setting, if you have a file with named\r\nrectangles in screen coordinates" +
              ".\r\nYou can define the assignments to the Ogama \r\ncolumns below.");
      this.rdbSimpleRects.UseVisualStyleBackColor = true;
      this.rdbSimpleRects.CheckedChanged += new System.EventHandler(this.rdbImportFormat_CheckedChanged);
      // 
      // rdbOgamaFormat
      // 
      this.rdbOgamaFormat.AutoSize = true;
      this.rdbOgamaFormat.Checked = true;
      this.rdbOgamaFormat.Location = new System.Drawing.Point(13, 42);
      this.rdbOgamaFormat.Name = "rdbOgamaFormat";
      this.rdbOgamaFormat.Size = new System.Drawing.Size(201, 17);
      this.rdbOgamaFormat.TabIndex = 20;
      this.rdbOgamaFormat.TabStop = true;
      this.rdbOgamaFormat.Text = "Import Ogama areas of interest format";
      this.toolTip1.SetToolTip(this.rdbOgamaFormat, "Select this, if you have exported a areas of interest table\r\nwith this program be" +
              "fore and you would like to reimport it.\r\nThis will automatically assign the corr" +
              "ect column mappings.");
      this.rdbOgamaFormat.UseVisualStyleBackColor = true;
      this.rdbOgamaFormat.CheckedChanged += new System.EventHandler(this.rdbImportFormat_CheckedChanged);
      // 
      // ofdImport
      // 
      this.ofdImport.DefaultExt = "txt";
      this.ofdImport.Filter = "ASCII-Files (*.csv,*.txt,*.asc)|*.csv;*.txt;*.asc|Text-Files (*.txt)|*.txt|Comma " +
          "separated-values (*.csv)|*.csv|ASC-Files (*.asc)|*.asc|All files|*.*";
      this.ofdImport.Title = "Please select ASCII file to import";
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.rdbOgamaFormat);
      this.groupBox1.Controls.Add(this.rdbSimpleRects);
      this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.groupBox1.Location = new System.Drawing.Point(5, 5);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(581, 69);
      this.groupBox1.TabIndex = 24;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Settings";
      // 
      // btnCancel
      // 
      this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Abort;
      this.btnCancel.Location = new System.Drawing.Point(504, 5);
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
      this.btnImport.Location = new System.Drawing.Point(423, 5);
      this.btnImport.Name = "btnImport";
      this.btnImport.Size = new System.Drawing.Size(75, 23);
      this.btnImport.TabIndex = 26;
      this.btnImport.Text = "&Import";
      this.toolTip1.SetToolTip(this.btnImport, "Start importing with given settings.");
      this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
      // 
      // btnBack
      // 
      this.btnBack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnBack.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.btnBack.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnBack.Location = new System.Drawing.Point(342, 5);
      this.btnBack.Name = "btnBack";
      this.btnBack.Size = new System.Drawing.Size(75, 23);
      this.btnBack.TabIndex = 29;
      this.btnBack.Text = "< &Back";
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
      this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
      // 
      // splitContainer1.Panel2
      // 
      this.splitContainer1.Panel2.Controls.Add(this.splitContainer3);
      this.splitContainer1.Size = new System.Drawing.Size(591, 403);
      this.splitContainer1.SplitterDistance = 140;
      this.splitContainer1.TabIndex = 21;
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
      this.splitContainer2.Panel1.Controls.Add(this.dialogTop1);
      // 
      // splitContainer2.Panel2
      // 
      this.splitContainer2.Panel2.Controls.Add(this.groupBox1);
      this.splitContainer2.Panel2.Padding = new System.Windows.Forms.Padding(5);
      this.splitContainer2.Size = new System.Drawing.Size(591, 140);
      this.splitContainer2.SplitterDistance = 60;
      this.splitContainer2.SplitterWidth = 1;
      this.splitContainer2.TabIndex = 0;
      // 
      // dialogTop1
      // 
      this.dialogTop1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(237)))));
      this.dialogTop1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("dialogTop1.BackgroundImage")));
      this.dialogTop1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
      this.dialogTop1.Description = "Assign columns: Please select from the dropdown the columns of the import file, t" +
          "hat match the columns of the import mode.";
      this.dialogTop1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.dialogTop1.Location = new System.Drawing.Point(0, 0);
      this.dialogTop1.Logo = global::Ogama.Properties.Resources.MagicWand;
      this.dialogTop1.Name = "dialogTop1";
      this.dialogTop1.Size = new System.Drawing.Size(591, 60);
      this.dialogTop1.TabIndex = 0;
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
      this.splitContainer3.Panel1.Controls.Add(this.dGVAssignments);
      // 
      // splitContainer3.Panel2
      // 
      this.splitContainer3.Panel2.Controls.Add(this.btnImport);
      this.splitContainer3.Panel2.Controls.Add(this.btnCancel);
      this.splitContainer3.Panel2.Controls.Add(this.btnBack);
      this.splitContainer3.Panel2MinSize = 40;
      this.splitContainer3.Size = new System.Drawing.Size(591, 259);
      this.splitContainer3.SplitterDistance = 215;
      this.splitContainer3.TabIndex = 0;
      // 
      // ImportAOIAssignColumnsDialog
      // 
      this.AcceptButton = this.btnImport;
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
      this.CancelButton = this.btnCancel;
      this.ClientSize = new System.Drawing.Size(591, 403);
      this.Controls.Add(this.splitContainer1);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "ImportAOIAssignColumnsDialog";
      this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "AOI Import assistant ... step 3 - assign columns";
      this.Load += new System.EventHandler(this.frmImportAOIAssignColumns_Load);
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmImportAOIAssignColumns_FormClosing);
      ((System.ComponentModel.ISupportInitialize)(this.dGVAssignments)).EndInit();
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel2.ResumeLayout(false);
      this.splitContainer1.ResumeLayout(false);
      this.splitContainer2.Panel1.ResumeLayout(false);
      this.splitContainer2.Panel2.ResumeLayout(false);
      this.splitContainer2.ResumeLayout(false);
      this.splitContainer3.Panel1.ResumeLayout(false);
      this.splitContainer3.Panel2.ResumeLayout(false);
      this.splitContainer3.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.DataGridView dGVAssignments;
    private System.Windows.Forms.DataGridViewTextBoxColumn ColumnAOITableColumns;
    private System.Windows.Forms.DataGridViewComboBoxColumn ColumnTextFileColumns;
    private System.Windows.Forms.RadioButton rdbSimpleRects;
    private System.Windows.Forms.RadioButton rdbOgamaFormat;
    private System.Windows.Forms.OpenFileDialog ofdImport;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.Button btnImport;
    private System.Windows.Forms.ToolTip toolTip1;
    private System.Windows.Forms.Button btnBack;
    private System.Windows.Forms.SplitContainer splitContainer1;
    private System.Windows.Forms.SplitContainer splitContainer2;
    private DialogTop dialogTop1;
    private System.Windows.Forms.SplitContainer splitContainer3;


  }
}
