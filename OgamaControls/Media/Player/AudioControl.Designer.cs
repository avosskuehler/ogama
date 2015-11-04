namespace OgamaControls
{
  partial class AudioControl
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

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.chbPlaySound = new System.Windows.Forms.CheckBox();
      this.txbFilename = new System.Windows.Forms.TextBox();
      this.rdbOnAppearance = new System.Windows.Forms.RadioButton();
      this.rdbOnClick = new System.Windows.Forms.RadioButton();
      this.toolStrip1 = new System.Windows.Forms.ToolStrip();
      this.btnPreviewPlay = new System.Windows.Forms.ToolStripButton();
      this.btnPreviewStop = new System.Windows.Forms.ToolStripButton();
      this.btnDeleteFile = new System.Windows.Forms.Button();
      this.btnOpenFile = new System.Windows.Forms.Button();
      this.chbLoop = new System.Windows.Forms.CheckBox();
      this.ofdAudioFiles = new System.Windows.Forms.OpenFileDialog();
      this.toolStrip1.SuspendLayout();
      this.SuspendLayout();
      // 
      // chbPlaySound
      // 
      this.chbPlaySound.AutoSize = true;
      this.chbPlaySound.Checked = true;
      this.chbPlaySound.CheckState = System.Windows.Forms.CheckState.Checked;
      this.chbPlaySound.Location = new System.Drawing.Point(3, 5);
      this.chbPlaySound.Name = "chbPlaySound";
      this.chbPlaySound.Size = new System.Drawing.Size(79, 18);
      this.chbPlaySound.TabIndex = 33;
      this.chbPlaySound.Text = "Play sound";
      this.chbPlaySound.UseCompatibleTextRendering = true;
      this.chbPlaySound.UseVisualStyleBackColor = true;
      this.chbPlaySound.CheckedChanged += new System.EventHandler(this.chbPlaySound_CheckedChanged);
      // 
      // txbFilename
      // 
      this.txbFilename.Location = new System.Drawing.Point(88, 3);
      this.txbFilename.Name = "txbFilename";
      this.txbFilename.ReadOnly = true;
      this.txbFilename.Size = new System.Drawing.Size(100, 20);
      this.txbFilename.TabIndex = 34;
      // 
      // rdbOnAppearance
      // 
      this.rdbOnAppearance.AutoSize = true;
      this.rdbOnAppearance.Checked = true;
      this.rdbOnAppearance.Location = new System.Drawing.Point(20, 29);
      this.rdbOnAppearance.Name = "rdbOnAppearance";
      this.rdbOnAppearance.Size = new System.Drawing.Size(97, 17);
      this.rdbOnAppearance.TabIndex = 35;
      this.rdbOnAppearance.TabStop = true;
      this.rdbOnAppearance.Text = "on appearance";
      this.rdbOnAppearance.UseVisualStyleBackColor = true;
      this.rdbOnAppearance.CheckedChanged += new System.EventHandler(this.rdbOnAppearance_CheckedChanged);
      // 
      // rdbOnClick
      // 
      this.rdbOnClick.AutoSize = true;
      this.rdbOnClick.Location = new System.Drawing.Point(20, 52);
      this.rdbOnClick.Name = "rdbOnClick";
      this.rdbOnClick.Size = new System.Drawing.Size(62, 17);
      this.rdbOnClick.TabIndex = 35;
      this.rdbOnClick.Text = "on click";
      this.rdbOnClick.UseVisualStyleBackColor = true;
      this.rdbOnClick.CheckedChanged += new System.EventHandler(this.rdbOnClick_CheckedChanged);
      // 
      // toolStrip1
      // 
      this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
      this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
      this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnPreviewPlay,
            this.btnPreviewStop});
      this.toolStrip1.Location = new System.Drawing.Point(196, 36);
      this.toolStrip1.Name = "toolStrip1";
      this.toolStrip1.Size = new System.Drawing.Size(49, 25);
      this.toolStrip1.TabIndex = 36;
      this.toolStrip1.Text = "toolStrip1";
      // 
      // btnPreviewPlay
      // 
      this.btnPreviewPlay.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnPreviewPlay.Image = global::OgamaControls.Properties.Resources.PlayHS;
      this.btnPreviewPlay.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnPreviewPlay.Name = "btnPreviewPlay";
      this.btnPreviewPlay.Size = new System.Drawing.Size(23, 22);
      this.btnPreviewPlay.Text = "toolStripButton1";
      this.btnPreviewPlay.Click += new System.EventHandler(this.btnPreviewPlay_Click);
      // 
      // btnPreviewStop
      // 
      this.btnPreviewStop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnPreviewStop.Image = global::OgamaControls.Properties.Resources.StopHS;
      this.btnPreviewStop.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnPreviewStop.Name = "btnPreviewStop";
      this.btnPreviewStop.Size = new System.Drawing.Size(23, 22);
      this.btnPreviewStop.Text = "toolStripButton2";
      this.btnPreviewStop.Click += new System.EventHandler(this.btnPreviewStop_Click);
      // 
      // btnDeleteFile
      // 
      this.btnDeleteFile.AutoSize = true;
      this.btnDeleteFile.Image = global::OgamaControls.Properties.Resources.DeleteHS;
      this.btnDeleteFile.Location = new System.Drawing.Point(221, 1);
      this.btnDeleteFile.Name = "btnDeleteFile";
      this.btnDeleteFile.Size = new System.Drawing.Size(25, 25);
      this.btnDeleteFile.TabIndex = 28;
      this.btnDeleteFile.UseVisualStyleBackColor = true;
      this.btnDeleteFile.Click += new System.EventHandler(this.btnDeleteFile_Click);
      // 
      // btnOpenFile
      // 
      this.btnOpenFile.AutoSize = true;
      this.btnOpenFile.Image = global::OgamaControls.Properties.Resources.openHS;
      this.btnOpenFile.Location = new System.Drawing.Point(194, 1);
      this.btnOpenFile.Name = "btnOpenFile";
      this.btnOpenFile.Size = new System.Drawing.Size(25, 25);
      this.btnOpenFile.TabIndex = 28;
      this.btnOpenFile.UseVisualStyleBackColor = true;
      this.btnOpenFile.Click += new System.EventHandler(this.btnOpenFile_Click);
      // 
      // chbLoop
      // 
      this.chbLoop.AutoSize = true;
      this.chbLoop.Location = new System.Drawing.Point(123, 29);
      this.chbLoop.Name = "chbLoop";
      this.chbLoop.Size = new System.Drawing.Size(48, 18);
      this.chbLoop.TabIndex = 33;
      this.chbLoop.Text = "Loop";
      this.chbLoop.UseCompatibleTextRendering = true;
      this.chbLoop.UseVisualStyleBackColor = true;
      this.chbLoop.Visible = false;
      this.chbLoop.CheckedChanged += new System.EventHandler(this.chbLoop_CheckedChanged);
      // 
      // ofdAudioFiles
      // 
      this.ofdAudioFiles.DefaultExt = "wav";
      this.ofdAudioFiles.Filter = "Audio files (*.WAV;*.MP3;*.AU;*.AIF;*.SND;*.MID;*WMA)|*.WAV;*.MP3;*.AU;*.AIF;*.SN" +
          "D;*.MID;*WMA|All files (*.*)|*.*";
      this.ofdAudioFiles.Title = "Please choose an audio file...";
      // 
      // AudioControl
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.toolStrip1);
      this.Controls.Add(this.rdbOnClick);
      this.Controls.Add(this.rdbOnAppearance);
      this.Controls.Add(this.txbFilename);
      this.Controls.Add(this.chbLoop);
      this.Controls.Add(this.chbPlaySound);
      this.Controls.Add(this.btnDeleteFile);
      this.Controls.Add(this.btnOpenFile);
      this.Name = "AudioControl";
      this.Size = new System.Drawing.Size(249, 72);
      this.toolStrip1.ResumeLayout(false);
      this.toolStrip1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.CheckBox chbPlaySound;
    private System.Windows.Forms.TextBox txbFilename;
    private System.Windows.Forms.RadioButton rdbOnAppearance;
    private System.Windows.Forms.RadioButton rdbOnClick;
    private System.Windows.Forms.Button btnOpenFile;
    private System.Windows.Forms.Button btnDeleteFile;
    private System.Windows.Forms.ToolStrip toolStrip1;
    private System.Windows.Forms.ToolStripButton btnPreviewPlay;
    private System.Windows.Forms.ToolStripButton btnPreviewStop;
    private System.Windows.Forms.CheckBox chbLoop;
    private System.Windows.Forms.OpenFileDialog ofdAudioFiles;
  }
}
