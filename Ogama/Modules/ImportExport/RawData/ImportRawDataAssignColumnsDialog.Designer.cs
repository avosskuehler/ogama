namespace Ogama.Modules.ImportExport.RawData
{
  using Ogama.Modules.Common.Controls;

  partial class ImportRawDataAssignColumnsDialog
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImportRawDataAssignColumnsDialog));
      this.dGVAssignments = new System.Windows.Forms.DataGridView();
      this.ColumnOgamaColumns = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.ColumnImportColumns = new System.Windows.Forms.DataGridViewComboBoxColumn();
      this.btnCancel = new System.Windows.Forms.Button();
      this.btnNext = new System.Windows.Forms.Button();
      this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
      this.panel3 = new System.Windows.Forms.Panel();
      this.label14 = new System.Windows.Forms.Label();
      this.label15 = new System.Windows.Forms.Label();
      this.label11 = new System.Windows.Forms.Label();
      this.label1 = new System.Windows.Forms.Label();
      this.label6 = new System.Windows.Forms.Label();
      this.label5 = new System.Windows.Forms.Label();
      this.label4 = new System.Windows.Forms.Label();
      this.label13 = new System.Windows.Forms.Label();
      this.label12 = new System.Windows.Forms.Label();
      this.label7 = new System.Windows.Forms.Label();
      this.label8 = new System.Windows.Forms.Label();
      this.label9 = new System.Windows.Forms.Label();
      this.btnBack = new System.Windows.Forms.Button();
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      this.splitContainer2 = new System.Windows.Forms.SplitContainer();
      this.dialogTop1 = new DialogTop();
      this.splitContainer3 = new System.Windows.Forms.SplitContainer();
      this.label2 = new System.Windows.Forms.Label();
      ((System.ComponentModel.ISupportInitialize)(this.dGVAssignments)).BeginInit();
      this.panel3.SuspendLayout();
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
      this.dGVAssignments.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnOgamaColumns,
            this.ColumnImportColumns});
      this.dGVAssignments.Dock = System.Windows.Forms.DockStyle.Fill;
      this.dGVAssignments.Location = new System.Drawing.Point(0, 0);
      this.dGVAssignments.Name = "dGVAssignments";
      this.dGVAssignments.Size = new System.Drawing.Size(411, 318);
      this.dGVAssignments.TabIndex = 17;
      this.toolTip1.SetToolTip(this.dGVAssignments, "Please select from the dropdowns \r\nthe columns from your text file \r\nthat best ma" +
              "tch the given\r\nOgama columns.");
      this.dGVAssignments.CellValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.dGVAssignments_CellValidated);
      this.dGVAssignments.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dGVAssignments_DataError);
      // 
      // ColumnOgamaColumns
      // 
      this.ColumnOgamaColumns.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
      this.ColumnOgamaColumns.HeaderText = "Ogama raw data columns";
      this.ColumnOgamaColumns.Name = "ColumnOgamaColumns";
      this.ColumnOgamaColumns.ReadOnly = true;
      this.ColumnOgamaColumns.Width = 155;
      // 
      // ColumnImportColumns
      // 
      this.ColumnImportColumns.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
      this.ColumnImportColumns.HeaderText = "Import columns";
      this.ColumnImportColumns.Name = "ColumnImportColumns";
      // 
      // btnCancel
      // 
      this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Abort;
      this.btnCancel.Location = new System.Drawing.Point(528, 7);
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
      this.btnNext.Location = new System.Drawing.Point(447, 7);
      this.btnNext.Name = "btnNext";
      this.btnNext.Size = new System.Drawing.Size(75, 23);
      this.btnNext.TabIndex = 26;
      this.btnNext.Text = "Next >";
      this.toolTip1.SetToolTip(this.btnNext, "goto Step 3: read trials");
      this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
      // 
      // panel3
      // 
      this.panel3.Controls.Add(this.label2);
      this.panel3.Controls.Add(this.label14);
      this.panel3.Controls.Add(this.label15);
      this.panel3.Controls.Add(this.label11);
      this.panel3.Controls.Add(this.label1);
      this.panel3.Controls.Add(this.label6);
      this.panel3.Controls.Add(this.label5);
      this.panel3.Controls.Add(this.label4);
      this.panel3.Controls.Add(this.label13);
      this.panel3.Controls.Add(this.label12);
      this.panel3.Controls.Add(this.label7);
      this.panel3.Controls.Add(this.label8);
      this.panel3.Controls.Add(this.label9);
      this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel3.Location = new System.Drawing.Point(0, 0);
      this.panel3.Name = "panel3";
      this.panel3.Size = new System.Drawing.Size(200, 318);
      this.panel3.TabIndex = 33;
      // 
      // label14
      // 
      this.label14.AutoSize = true;
      this.label14.Location = new System.Drawing.Point(-1, 49);
      this.label14.Name = "label14";
      this.label14.Size = new System.Drawing.Size(200, 13);
      this.label14.TabIndex = 34;
      this.label14.Text = "... gives the trials presentation sequence.";
      // 
      // label15
      // 
      this.label15.AutoSize = true;
      this.label15.Location = new System.Drawing.Point(79, 93);
      this.label15.Name = "label15";
      this.label15.Size = new System.Drawing.Size(120, 13);
      this.label15.TabIndex = 34;
      this.label15.Text = "... gives the trials image.";
      // 
      // label11
      // 
      this.label11.AutoSize = true;
      this.label11.Location = new System.Drawing.Point(66, 115);
      this.label11.Name = "label11";
      this.label11.Size = new System.Drawing.Size(133, 13);
      this.label11.TabIndex = 34;
      this.label11.Text = "... gives the trials category.";
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label1.Location = new System.Drawing.Point(29, 8);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(151, 13);
      this.label1.TabIndex = 33;
      this.label1.Text = "Select the column that ...";
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Location = new System.Drawing.Point(4, 181);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(195, 13);
      this.label6.TabIndex = 32;
      this.label6.Text = "... gives the pupil diameter in y-direction.";
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(4, 159);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(195, 13);
      this.label5.TabIndex = 32;
      this.label5.Text = "... gives the pupil diameter in x-direction.";
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(104, 137);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(95, 13);
      this.label4.TabIndex = 32;
      this.label4.Text = "... gives the timing.";
      // 
      // label13
      // 
      this.label13.AutoSize = true;
      this.label13.Location = new System.Drawing.Point(53, 247);
      this.label13.Name = "label13";
      this.label13.Size = new System.Drawing.Size(146, 13);
      this.label13.TabIndex = 32;
      this.label13.Text = "... gives the mouse x-position.";
      // 
      // label12
      // 
      this.label12.AutoSize = true;
      this.label12.Location = new System.Drawing.Point(53, 269);
      this.label12.Name = "label12";
      this.label12.Size = new System.Drawing.Size(146, 13);
      this.label12.TabIndex = 32;
      this.label12.Text = "... gives the mouse y-position.";
      // 
      // label7
      // 
      this.label7.AutoSize = true;
      this.label7.Location = new System.Drawing.Point(61, 225);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(138, 13);
      this.label7.TabIndex = 32;
      this.label7.Text = "... gives the gaze y-position.";
      // 
      // label8
      // 
      this.label8.AutoSize = true;
      this.label8.Location = new System.Drawing.Point(61, 203);
      this.label8.Name = "label8";
      this.label8.Size = new System.Drawing.Size(138, 13);
      this.label8.TabIndex = 32;
      this.label8.Text = "... gives the gaze x-position.";
      // 
      // label9
      // 
      this.label9.AutoSize = true;
      this.label9.Location = new System.Drawing.Point(65, 29);
      this.label9.Name = "label9";
      this.label9.Size = new System.Drawing.Size(134, 13);
      this.label9.TabIndex = 30;
      this.label9.Text = " ... gives the subject name.";
      // 
      // btnBack
      // 
      this.btnBack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnBack.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.btnBack.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnBack.Location = new System.Drawing.Point(366, 7);
      this.btnBack.Name = "btnBack";
      this.btnBack.Size = new System.Drawing.Size(75, 23);
      this.btnBack.TabIndex = 28;
      this.btnBack.Text = "< &Back";
      // 
      // splitContainer1
      // 
      this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
      this.splitContainer1.IsSplitterFixed = true;
      this.splitContainer1.Location = new System.Drawing.Point(0, 0);
      this.splitContainer1.Name = "splitContainer1";
      // 
      // splitContainer1.Panel1
      // 
      this.splitContainer1.Panel1.Controls.Add(this.panel3);
      // 
      // splitContainer1.Panel2
      // 
      this.splitContainer1.Panel2.Controls.Add(this.dGVAssignments);
      this.splitContainer1.Size = new System.Drawing.Size(615, 318);
      this.splitContainer1.SplitterDistance = 200;
      this.splitContainer1.TabIndex = 35;
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
      this.splitContainer2.Panel2.Controls.Add(this.splitContainer3);
      this.splitContainer2.Size = new System.Drawing.Size(615, 425);
      this.splitContainer2.SplitterDistance = 60;
      this.splitContainer2.SplitterWidth = 1;
      this.splitContainer2.TabIndex = 36;
      // 
      // dialogTop1
      // 
      this.dialogTop1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(237)))));
      this.dialogTop1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("dialogTop1.BackgroundImage")));
      this.dialogTop1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
      this.dialogTop1.Description = "Assign columns: Please select from the dropdown the columns of the import file, t" +
          "hat match the columns in ogamas raw data table.";
      this.dialogTop1.Dock = System.Windows.Forms.DockStyle.Top;
      this.dialogTop1.Location = new System.Drawing.Point(0, 0);
      this.dialogTop1.Logo = global::Ogama.Properties.Resources.MagicWand;
      this.dialogTop1.Name = "dialogTop1";
      this.dialogTop1.Size = new System.Drawing.Size(615, 60);
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
      this.splitContainer3.Panel1.Controls.Add(this.splitContainer1);
      // 
      // splitContainer3.Panel2
      // 
      this.splitContainer3.Panel2.Controls.Add(this.btnCancel);
      this.splitContainer3.Panel2.Controls.Add(this.btnNext);
      this.splitContainer3.Panel2.Controls.Add(this.btnBack);
      this.splitContainer3.Size = new System.Drawing.Size(615, 364);
      this.splitContainer3.SplitterDistance = 318;
      this.splitContainer3.TabIndex = 0;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(50, 71);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(147, 13);
      this.label2.TabIndex = 34;
      this.label2.Text = "... distinguishes different trials.";
      // 
      // frmImportRawDataAssignColumns
      // 
      this.AcceptButton = this.btnNext;
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
      this.AutoSize = true;
      this.CancelButton = this.btnCancel;
      this.ClientSize = new System.Drawing.Size(615, 425);
      this.Controls.Add(this.splitContainer2);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "frmImportRawDataAssignColumns";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Import assistant ... step 3 - assign columns";
      this.Load += new System.EventHandler(this.frmImportRawDataAssignColumns_Load);
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmImportRawDataAssignColumns_FormClosing);
      ((System.ComponentModel.ISupportInitialize)(this.dGVAssignments)).EndInit();
      this.panel3.ResumeLayout(false);
      this.panel3.PerformLayout();
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
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.Button btnNext;
    private System.Windows.Forms.ToolTip toolTip1;
    private System.Windows.Forms.Panel panel3;
    private System.Windows.Forms.Label label11;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label8;
    private System.Windows.Forms.Label label9;
    private System.Windows.Forms.Button btnBack;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.Label label13;
    private System.Windows.Forms.Label label12;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.Label label15;
    private System.Windows.Forms.DataGridViewTextBoxColumn ColumnOgamaColumns;
    private System.Windows.Forms.DataGridViewComboBoxColumn ColumnImportColumns;
    private System.Windows.Forms.Label label14;
    private System.Windows.Forms.SplitContainer splitContainer1;
    private System.Windows.Forms.SplitContainer splitContainer2;
    private DialogTop dialogTop1;
    private System.Windows.Forms.SplitContainer splitContainer3;
    private System.Windows.Forms.Label label2;
  }
}
