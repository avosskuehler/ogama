namespace Ogama.Modules.Scanpath
{
  using Ogama.Modules.Common.Controls;

  partial class ExportDialog
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExportDialog));
      this.btnCancel = new System.Windows.Forms.Button();
      this.dialogTop1 = new DialogTop();
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      this.splitContainer2 = new System.Windows.Forms.SplitContainer();
      this.btnOK = new System.Windows.Forms.Button();
      this.chbExportAllTrials = new System.Windows.Forms.CheckBox();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.rdbLociSimilarity = new System.Windows.Forms.RadioButton();
      this.rdbSequenceSimilarity = new System.Windows.Forms.RadioButton();
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      this.splitContainer2.Panel1.SuspendLayout();
      this.splitContainer2.Panel2.SuspendLayout();
      this.splitContainer2.SuspendLayout();
      this.groupBox1.SuspendLayout();
      this.SuspendLayout();
      // 
      // btnCancel
      // 
      this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnCancel.AutoSize = true;
      this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnCancel.Location = new System.Drawing.Point(187, 1);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(84, 26);
      this.btnCancel.TabIndex = 3;
      this.btnCancel.Text = "&Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      // 
      // dialogTop1
      // 
      this.dialogTop1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(237)))));
      this.dialogTop1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("dialogTop1.BackgroundImage")));
      this.dialogTop1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
      this.dialogTop1.Description = "Specify the data to export.";
      this.dialogTop1.Dock = System.Windows.Forms.DockStyle.Top;
      this.dialogTop1.Location = new System.Drawing.Point(0, 0);
      this.dialogTop1.Logo = global::Ogama.Properties.Resources.ExportToDocument;
      this.dialogTop1.Name = "dialogTop1";
      this.dialogTop1.Size = new System.Drawing.Size(274, 60);
      this.dialogTop1.TabIndex = 5;
      this.dialogTop1.TabStop = false;
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
      this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
      this.splitContainer1.Size = new System.Drawing.Size(274, 219);
      this.splitContainer1.SplitterDistance = 60;
      this.splitContainer1.SplitterWidth = 1;
      this.splitContainer1.TabIndex = 6;
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
      this.splitContainer2.Panel1.Controls.Add(this.chbExportAllTrials);
      // 
      // splitContainer2.Panel2
      // 
      this.splitContainer2.Panel2.Controls.Add(this.btnOK);
      this.splitContainer2.Panel2.Controls.Add(this.btnCancel);
      this.splitContainer2.Panel2MinSize = 30;
      this.splitContainer2.Size = new System.Drawing.Size(274, 158);
      this.splitContainer2.SplitterDistance = 124;
      this.splitContainer2.TabIndex = 0;
      // 
      // btnOK
      // 
      this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnOK.AutoSize = true;
      this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.btnOK.Location = new System.Drawing.Point(97, 1);
      this.btnOK.Name = "btnOK";
      this.btnOK.Size = new System.Drawing.Size(84, 26);
      this.btnOK.TabIndex = 3;
      this.btnOK.Text = "&OK";
      this.btnOK.UseVisualStyleBackColor = true;
      // 
      // chbExportAllTrials
      // 
      this.chbExportAllTrials.AutoSize = true;
      this.chbExportAllTrials.Location = new System.Drawing.Point(18, 86);
      this.chbExportAllTrials.Name = "chbExportAllTrials";
      this.chbExportAllTrials.Size = new System.Drawing.Size(175, 17);
      this.chbExportAllTrials.TabIndex = 1;
      this.chbExportAllTrials.Text = "Export similarity table for all trials";
      this.chbExportAllTrials.UseVisualStyleBackColor = true;
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.rdbSequenceSimilarity);
      this.groupBox1.Controls.Add(this.rdbLociSimilarity);
      this.groupBox1.Location = new System.Drawing.Point(12, 5);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(147, 64);
      this.groupBox1.TabIndex = 2;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Similarity Type";
      // 
      // rdbLociSimilarity
      // 
      this.rdbLociSimilarity.AutoSize = true;
      this.rdbLociSimilarity.Checked = true;
      this.rdbLociSimilarity.Location = new System.Drawing.Point(6, 19);
      this.rdbLociSimilarity.Name = "rdbLociSimilarity";
      this.rdbLociSimilarity.Size = new System.Drawing.Size(86, 17);
      this.rdbLociSimilarity.TabIndex = 1;
      this.rdbLociSimilarity.TabStop = true;
      this.rdbLociSimilarity.Text = "Loci similarity";
      this.rdbLociSimilarity.UseVisualStyleBackColor = true;
      // 
      // rdbSequenceSimilarity
      // 
      this.rdbSequenceSimilarity.AutoSize = true;
      this.rdbSequenceSimilarity.Location = new System.Drawing.Point(6, 41);
      this.rdbSequenceSimilarity.Name = "rdbSequenceSimilarity";
      this.rdbSequenceSimilarity.Size = new System.Drawing.Size(117, 17);
      this.rdbSequenceSimilarity.TabIndex = 2;
      this.rdbSequenceSimilarity.Text = "Sequence Similarity";
      this.rdbSequenceSimilarity.UseVisualStyleBackColor = true;
      // 
      // ExportDialog
      // 
      this.AcceptButton = this.btnOK;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.btnCancel;
      this.ClientSize = new System.Drawing.Size(274, 219);
      this.Controls.Add(this.splitContainer1);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "ExportDialog";
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Export ...";
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel2.ResumeLayout(false);
      this.splitContainer1.ResumeLayout(false);
      this.splitContainer2.Panel1.ResumeLayout(false);
      this.splitContainer2.Panel1.PerformLayout();
      this.splitContainer2.Panel2.ResumeLayout(false);
      this.splitContainer2.Panel2.PerformLayout();
      this.splitContainer2.ResumeLayout(false);
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Button btnCancel;
    private DialogTop dialogTop1;
    private System.Windows.Forms.SplitContainer splitContainer1;
    private System.Windows.Forms.SplitContainer splitContainer2;
    private System.Windows.Forms.Button btnOK;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.CheckBox chbExportAllTrials;
    private System.Windows.Forms.RadioButton rdbSequenceSimilarity;
    private System.Windows.Forms.RadioButton rdbLociSimilarity;
  }
}