namespace Ogama.Modules.Replay.Video
{
  using Ogama.Modules.Common.Controls;

  partial class SaveVideoSplash
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SaveVideoSplash));
      this.progressBar = new System.Windows.Forms.ProgressBar();
      this.pictureBox1 = new System.Windows.Forms.PictureBox();
      this.pictureBox2 = new System.Windows.Forms.PictureBox();
      this.pictureBox3 = new System.Windows.Forms.PictureBox();
      this.label1 = new System.Windows.Forms.Label();
      this.dialogTop = new DialogTop();
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      this.spcPreviewSplash = new System.Windows.Forms.SplitContainer();
      this.previewPanel = new System.Windows.Forms.Panel();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      this.spcPreviewSplash.Panel1.SuspendLayout();
      this.spcPreviewSplash.Panel2.SuspendLayout();
      this.spcPreviewSplash.SuspendLayout();
      this.SuspendLayout();
      // 
      // progressBar
      // 
      this.progressBar.BackColor = System.Drawing.Color.White;
      this.progressBar.Location = new System.Drawing.Point(12, 71);
      this.progressBar.Name = "progressBar";
      this.progressBar.Size = new System.Drawing.Size(294, 13);
      this.progressBar.Step = 1;
      this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
      this.progressBar.TabIndex = 3;
      // 
      // pictureBox1
      // 
      this.pictureBox1.BackColor = System.Drawing.SystemColors.Control;
      this.pictureBox1.Image = global::Ogama.Properties.Resources.update2_00;
      this.pictureBox1.Location = new System.Drawing.Point(55, 6);
      this.pictureBox1.Name = "pictureBox1";
      this.pictureBox1.Size = new System.Drawing.Size(208, 40);
      this.pictureBox1.TabIndex = 5;
      this.pictureBox1.TabStop = false;
      // 
      // pictureBox2
      // 
      this.pictureBox2.Image = global::Ogama.Properties.Resources.Ogama;
      this.pictureBox2.Location = new System.Drawing.Point(12, 8);
      this.pictureBox2.Name = "pictureBox2";
      this.pictureBox2.Size = new System.Drawing.Size(37, 37);
      this.pictureBox2.TabIndex = 6;
      this.pictureBox2.TabStop = false;
      // 
      // pictureBox3
      // 
      this.pictureBox3.Image = global::Ogama.Properties.Resources.video;
      this.pictureBox3.Location = new System.Drawing.Point(269, 8);
      this.pictureBox3.Name = "pictureBox3";
      this.pictureBox3.Size = new System.Drawing.Size(37, 37);
      this.pictureBox3.TabIndex = 6;
      this.pictureBox3.TabStop = false;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.BackColor = System.Drawing.Color.Transparent;
      this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label1.ForeColor = System.Drawing.Color.Black;
      this.label1.Location = new System.Drawing.Point(26, 52);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(265, 12);
      this.label1.TabIndex = 4;
      this.label1.Text = "this may last a while, but you can still use the other interfaces ...";
      // 
      // dialogTop
      // 
      this.dialogTop.BackColor = System.Drawing.SystemColors.Control;
      this.dialogTop.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("dialogTop.BackgroundImage")));
      this.dialogTop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
      this.dialogTop.Description = "Creating video ...";
      this.dialogTop.Dock = System.Windows.Forms.DockStyle.Top;
      this.dialogTop.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
      this.dialogTop.Location = new System.Drawing.Point(0, 0);
      this.dialogTop.Logo = global::Ogama.Properties.Resources.video;
      this.dialogTop.Name = "dialogTop";
      this.dialogTop.Size = new System.Drawing.Size(322, 60);
      this.dialogTop.TabIndex = 7;
      this.dialogTop.TabStop = false;
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
      this.splitContainer1.Panel1.Controls.Add(this.dialogTop);
      this.splitContainer1.Panel1MinSize = 60;
      // 
      // splitContainer1.Panel2
      // 
      this.splitContainer1.Panel2.Controls.Add(this.spcPreviewSplash);
      this.splitContainer1.Size = new System.Drawing.Size(322, 371);
      this.splitContainer1.SplitterDistance = 60;
      this.splitContainer1.SplitterWidth = 1;
      this.splitContainer1.TabIndex = 8;
      // 
      // spcPreviewSplash
      // 
      this.spcPreviewSplash.Dock = System.Windows.Forms.DockStyle.Fill;
      this.spcPreviewSplash.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
      this.spcPreviewSplash.IsSplitterFixed = true;
      this.spcPreviewSplash.Location = new System.Drawing.Point(0, 0);
      this.spcPreviewSplash.Name = "spcPreviewSplash";
      this.spcPreviewSplash.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // spcPreviewSplash.Panel1
      // 
      this.spcPreviewSplash.Panel1.Controls.Add(this.previewPanel);
      // 
      // spcPreviewSplash.Panel2
      // 
      this.spcPreviewSplash.Panel2.Controls.Add(this.progressBar);
      this.spcPreviewSplash.Panel2.Controls.Add(this.label1);
      this.spcPreviewSplash.Panel2.Controls.Add(this.pictureBox1);
      this.spcPreviewSplash.Panel2.Controls.Add(this.pictureBox2);
      this.spcPreviewSplash.Panel2.Controls.Add(this.pictureBox3);
      this.spcPreviewSplash.Panel2MinSize = 100;
      this.spcPreviewSplash.Size = new System.Drawing.Size(322, 310);
      this.spcPreviewSplash.SplitterDistance = 206;
      this.spcPreviewSplash.TabIndex = 0;
      // 
      // previewPanel
      // 
      this.previewPanel.BackColor = System.Drawing.Color.Black;
      this.previewPanel.Dock = System.Windows.Forms.DockStyle.Fill;
      this.previewPanel.Location = new System.Drawing.Point(0, 0);
      this.previewPanel.Name = "previewPanel";
      this.previewPanel.Size = new System.Drawing.Size(322, 206);
      this.previewPanel.TabIndex = 0;
      // 
      // SaveVideoSplash
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(322, 371);
      this.Controls.Add(this.splitContainer1);
      this.Cursor = System.Windows.Forms.Cursors.AppStarting;
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MaximizeBox = false;
      this.Name = "SaveVideoSplash";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Video export";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SaveVideoSplash_FormClosing);
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel2.ResumeLayout(false);
      this.splitContainer1.ResumeLayout(false);
      this.spcPreviewSplash.Panel1.ResumeLayout(false);
      this.spcPreviewSplash.Panel2.ResumeLayout(false);
      this.spcPreviewSplash.Panel2.PerformLayout();
      this.spcPreviewSplash.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.PictureBox pictureBox1;
    private System.Windows.Forms.PictureBox pictureBox2;
    private System.Windows.Forms.PictureBox pictureBox3;
    private System.Windows.Forms.ProgressBar progressBar;
    internal System.Windows.Forms.Label label1;
    private DialogTop dialogTop;
    private System.Windows.Forms.SplitContainer splitContainer1;
    private System.Windows.Forms.SplitContainer spcPreviewSplash;
    private System.Windows.Forms.Panel previewPanel;

  }
}