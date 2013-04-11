namespace OgamaControls
{
  partial class AVPlayer
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
      this.CloseInterfaces();
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
      this.toolTip = new System.Windows.Forms.ToolTip(this.components);
      this.cmuPlayer = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.cmuAudio = new System.Windows.Forms.ToolStripMenuItem();
      this.cmuPlayer.SuspendLayout();
      this.SuspendLayout();
      // 
      // openFileDialog1
      // 
      this.openFileDialog1.FileName = "openFileDialog1";
      // 
      // cmuPlayer
      // 
      this.cmuPlayer.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmuAudio});
      this.cmuPlayer.Name = "cmuPlayer";
      this.cmuPlayer.Size = new System.Drawing.Size(158, 48);
      // 
      // cmuAudio
      // 
      this.cmuAudio.Checked = true;
      this.cmuAudio.CheckOnClick = true;
      this.cmuAudio.CheckState = System.Windows.Forms.CheckState.Checked;
      this.cmuAudio.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
      this.cmuAudio.Name = "cmuAudio";
      this.cmuAudio.Size = new System.Drawing.Size(157, 22);
      this.cmuAudio.Text = "Audio Playback";
      this.cmuAudio.Click += new System.EventHandler(this.cmuAudio_Click);
      // 
      // AVPlayer
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.Color.Black;
      this.ContextMenuStrip = this.cmuPlayer;
      this.Name = "AVPlayer";
      this.Resize += new System.EventHandler(this.AVPlayer_Resize);
      this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AVPlayer_KeyDown);
      this.cmuPlayer.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.OpenFileDialog openFileDialog1;
    private System.Windows.Forms.ToolTip toolTip;
    private System.Windows.Forms.ContextMenuStrip cmuPlayer;
    private System.Windows.Forms.ToolStripMenuItem cmuAudio;
  }
}
