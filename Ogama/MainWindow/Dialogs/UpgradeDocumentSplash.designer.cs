namespace Ogama.MainWindow.Dialogs
{
  using Ogama.Modules.Common.Controls;

  partial class UpgradeDocumentSplash
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UpgradeDocumentSplash));
      this.progressBar1 = new System.Windows.Forms.ProgressBar();
      this.timer1 = new System.Windows.Forms.Timer(this.components);
      this.label1 = new System.Windows.Forms.Label();
      this.pictureBox1 = new System.Windows.Forms.PictureBox();
      this.label2 = new System.Windows.Forms.Label();
      this.dialogTop1 = new DialogTop();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
      this.SuspendLayout();
      // 
      // progressBar1
      // 
      this.progressBar1.BackColor = System.Drawing.Color.White;
      this.progressBar1.Location = new System.Drawing.Point(93, 108);
      this.progressBar1.Name = "progressBar1";
      this.progressBar1.Size = new System.Drawing.Size(328, 20);
      this.progressBar1.Step = 1;
      this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
      this.progressBar1.TabIndex = 3;
      // 
      // timer1
      // 
      this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.BackColor = System.Drawing.Color.Transparent;
      this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label1.ForeColor = System.Drawing.Color.Black;
      this.label1.Location = new System.Drawing.Point(283, 132);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(139, 12);
      this.label1.TabIndex = 4;
      this.label1.Text = "please wait or take a coffee ;-) ...";
      // 
      // pictureBox1
      // 
      this.pictureBox1.Image = global::Ogama.Properties.Resources.UpdateOgama1;
      this.pictureBox1.Location = new System.Drawing.Point(12, 73);
      this.pictureBox1.Name = "pictureBox1";
      this.pictureBox1.Size = new System.Drawing.Size(64, 64);
      this.pictureBox1.TabIndex = 8;
      this.pictureBox1.TabStop = false;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.BackColor = System.Drawing.Color.Transparent;
      this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label2.ForeColor = System.Drawing.Color.Black;
      this.label2.Location = new System.Drawing.Point(93, 73);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(319, 30);
      this.label2.TabIndex = 4;
      this.label2.Text = "Adding tables, renaming columns, moving data ... \r\nthis will last approx. 10 minu" +
          "tes for a 400MByte database.";
      // 
      // dialogTop1
      // 
      this.dialogTop1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(237)))));
      this.dialogTop1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("dialogTop1.BackgroundImage")));
      this.dialogTop1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
      this.dialogTop1.Description = "Upgrading database ...";
      this.dialogTop1.Dock = System.Windows.Forms.DockStyle.Top;
      this.dialogTop1.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
      this.dialogTop1.Location = new System.Drawing.Point(0, 0);
      this.dialogTop1.Logo = null;
      this.dialogTop1.Name = "dialogTop1";
      this.dialogTop1.Size = new System.Drawing.Size(435, 60);
      this.dialogTop1.TabIndex = 7;
      this.dialogTop1.TabStop = false;
      // 
      // UpgradeDocumentSplash
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(435, 151);
      this.Controls.Add(this.pictureBox1);
      this.Controls.Add(this.dialogTop1);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.progressBar1);
      this.Cursor = System.Windows.Forms.Cursors.AppStarting;
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
      this.Name = "UpgradeDocumentSplash";
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.TopMost = true;
      this.Load += new System.EventHandler(this.UpgradeDocumentSplash_Load);
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Timer timer1;
    private System.Windows.Forms.ProgressBar progressBar1;
    internal System.Windows.Forms.Label label1;
    private DialogTop dialogTop1;
    private System.Windows.Forms.PictureBox pictureBox1;
    internal System.Windows.Forms.Label label2;

  }
}