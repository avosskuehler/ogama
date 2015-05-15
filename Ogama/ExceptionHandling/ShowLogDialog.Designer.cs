namespace Ogama.ExceptionHandling
{
  using Ogama.Modules.Common.Controls;

  partial class ShowLogDialog
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShowLogDialog));
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      this.llbMailTo = new System.Windows.Forms.LinkLabel();
      this.lblFileName = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.txbLog = new System.Windows.Forms.TextBox();
      this.splitContainer2 = new System.Windows.Forms.SplitContainer();
      this.dialogTop1 = new DialogTop();
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      this.splitContainer2.Panel1.SuspendLayout();
      this.splitContainer2.Panel2.SuspendLayout();
      this.splitContainer2.SuspendLayout();
      this.SuspendLayout();
      // 
      // splitContainer1
      // 
      this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
      this.splitContainer1.Location = new System.Drawing.Point(0, 0);
      this.splitContainer1.Name = "splitContainer1";
      this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // splitContainer1.Panel1
      // 
      this.splitContainer1.Panel1.Controls.Add(this.llbMailTo);
      this.splitContainer1.Panel1.Controls.Add(this.lblFileName);
      this.splitContainer1.Panel1.Controls.Add(this.label2);
      // 
      // splitContainer1.Panel2
      // 
      this.splitContainer1.Panel2.Controls.Add(this.txbLog);
      this.splitContainer1.Size = new System.Drawing.Size(425, 322);
      this.splitContainer1.SplitterDistance = 48;
      this.splitContainer1.TabIndex = 1;
      // 
      // llbMailTo
      // 
      this.llbMailTo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.llbMailTo.AutoSize = true;
      this.llbMailTo.Location = new System.Drawing.Point(203, 30);
      this.llbMailTo.Name = "llbMailTo";
      this.llbMailTo.Size = new System.Drawing.Size(222, 13);
      this.llbMailTo.TabIndex = 3;
      this.llbMailTo.TabStop = true;
      this.llbMailTo.Text = "mailto:adrian@ogama.net";
      this.llbMailTo.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llbMailTo_LinkClicked);
      // 
      // lblFileName
      // 
      this.lblFileName.AutoSize = true;
      this.lblFileName.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblFileName.Location = new System.Drawing.Point(10, 8);
      this.lblFileName.Name = "lblFileName";
      this.lblFileName.Size = new System.Drawing.Size(174, 31);
      this.lblFileName.TabIndex = 2;
      this.lblFileName.Text = "exception.log";
      // 
      // label2
      // 
      this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(203, 12);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(168, 13);
      this.label2.TabIndex = 1;
      this.label2.Text = "Report this log file by sending it to:";
      // 
      // txbLog
      // 
      this.txbLog.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txbLog.Location = new System.Drawing.Point(0, 0);
      this.txbLog.MaxLength = 16777216;
      this.txbLog.Multiline = true;
      this.txbLog.Name = "txbLog";
      this.txbLog.ReadOnly = true;
      this.txbLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
      this.txbLog.Size = new System.Drawing.Size(425, 270);
      this.txbLog.TabIndex = 0;
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
      this.splitContainer2.Panel2.Controls.Add(this.splitContainer1);
      this.splitContainer2.Size = new System.Drawing.Size(425, 386);
      this.splitContainer2.SplitterDistance = 60;
      this.splitContainer2.TabIndex = 1;
      // 
      // dialogTop1
      // 
      this.dialogTop1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(237)))));
      this.dialogTop1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("dialogTop1.BackgroundImage")));
      this.dialogTop1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
      this.dialogTop1.Description = "This is the content of the file:";
      this.dialogTop1.Dock = System.Windows.Forms.DockStyle.Top;
      this.dialogTop1.Location = new System.Drawing.Point(0, 0);
      this.dialogTop1.Logo = global::Ogama.Properties.Resources.textdoc;
      this.dialogTop1.Name = "dialogTop1";
      this.dialogTop1.Size = new System.Drawing.Size(425, 60);
      this.dialogTop1.TabIndex = 0;
      // 
      // frmShowLog
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(425, 386);
      this.Controls.Add(this.splitContainer2);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "frmShowLog";
      this.Text = "Show log file ...";
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel1.PerformLayout();
      this.splitContainer1.Panel2.ResumeLayout(false);
      this.splitContainer1.Panel2.PerformLayout();
      this.splitContainer1.ResumeLayout(false);
      this.splitContainer2.Panel1.ResumeLayout(false);
      this.splitContainer2.Panel2.ResumeLayout(false);
      this.splitContainer2.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.SplitContainer splitContainer1;
    private System.Windows.Forms.Label lblFileName;
    private System.Windows.Forms.LinkLabel llbMailTo;
    private System.Windows.Forms.TextBox txbLog;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.SplitContainer splitContainer2;
    private DialogTop dialogTop1;
  }
}