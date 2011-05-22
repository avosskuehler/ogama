namespace OgamaControls
{
  partial class DSVideoProperties
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
      this.label5 = new System.Windows.Forms.Label();
      this.btnVideoDeviceProperties = new System.Windows.Forms.Button();
      this.label6 = new System.Windows.Forms.Label();
      this.pictureBox2 = new System.Windows.Forms.PictureBox();
      this.btnVideoCompressorProperties = new System.Windows.Forms.Button();
      this.spcVideoAudio = new System.Windows.Forms.SplitContainer();
      this.spcVideoPropPreview = new System.Windows.Forms.SplitContainer();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.nudFrameRate = new System.Windows.Forms.NumericUpDown();
      this.label4 = new System.Windows.Forms.Label();
      this.label3 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.cbbVideoSize = new System.Windows.Forms.ComboBox();
      this.label1 = new System.Windows.Forms.Label();
      this.pictureBox1 = new System.Windows.Forms.PictureBox();
      this.cbbVideoDevices = new System.Windows.Forms.ComboBox();
      this.cbbVideoCompressor = new System.Windows.Forms.ComboBox();
      this.grpPreview = new System.Windows.Forms.GroupBox();
      this.panelPreview = new System.Windows.Forms.Panel();
      this.spcAudioPropPreview = new System.Windows.Forms.SplitContainer();
      this.groupBox2 = new System.Windows.Forms.GroupBox();
      this.btnAudioCompressorProperties = new System.Windows.Forms.Button();
      this.btnAudioDeviceProperties = new System.Windows.Forms.Button();
      this.cbbAudioDevices = new System.Windows.Forms.ComboBox();
      this.cbbAudioCompressor = new System.Windows.Forms.ComboBox();
      this.groupBox3 = new System.Windows.Forms.GroupBox();
      this.pmcAudio = new OgamaControls.PeakMeterCtrl();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
      this.spcVideoAudio.Panel1.SuspendLayout();
      this.spcVideoAudio.Panel2.SuspendLayout();
      this.spcVideoAudio.SuspendLayout();
      this.spcVideoPropPreview.Panel1.SuspendLayout();
      this.spcVideoPropPreview.Panel2.SuspendLayout();
      this.spcVideoPropPreview.SuspendLayout();
      this.groupBox1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.nudFrameRate)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
      this.grpPreview.SuspendLayout();
      this.spcAudioPropPreview.Panel1.SuspendLayout();
      this.spcAudioPropPreview.Panel2.SuspendLayout();
      this.spcAudioPropPreview.SuspendLayout();
      this.groupBox2.SuspendLayout();
      this.groupBox3.SuspendLayout();
      this.SuspendLayout();
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(51, 57);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(94, 13);
      this.label5.TabIndex = 5;
      this.label5.Text = "Audio compressor:";
      // 
      // btnVideoDeviceProperties
      // 
      this.btnVideoDeviceProperties.Image = global::OgamaControls.Properties.Resources.PropertiesHS;
      this.btnVideoDeviceProperties.Location = new System.Drawing.Point(363, 23);
      this.btnVideoDeviceProperties.Name = "btnVideoDeviceProperties";
      this.btnVideoDeviceProperties.Size = new System.Drawing.Size(23, 23);
      this.btnVideoDeviceProperties.TabIndex = 9;
      this.btnVideoDeviceProperties.UseVisualStyleBackColor = true;
      this.btnVideoDeviceProperties.Click += new System.EventHandler(this.btnVideoDeviceProperties_Click);
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Location = new System.Drawing.Point(51, 24);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(72, 13);
      this.label6.TabIndex = 4;
      this.label6.Text = "Audio device:";
      // 
      // pictureBox2
      // 
      this.pictureBox2.Image = global::OgamaControls.Properties.Resources.sound;
      this.pictureBox2.Location = new System.Drawing.Point(13, 24);
      this.pictureBox2.Name = "pictureBox2";
      this.pictureBox2.Size = new System.Drawing.Size(32, 32);
      this.pictureBox2.TabIndex = 2;
      this.pictureBox2.TabStop = false;
      // 
      // btnVideoCompressorProperties
      // 
      this.btnVideoCompressorProperties.Image = global::OgamaControls.Properties.Resources.PropertiesHS;
      this.btnVideoCompressorProperties.Location = new System.Drawing.Point(363, 56);
      this.btnVideoCompressorProperties.Name = "btnVideoCompressorProperties";
      this.btnVideoCompressorProperties.Size = new System.Drawing.Size(23, 23);
      this.btnVideoCompressorProperties.TabIndex = 9;
      this.btnVideoCompressorProperties.UseVisualStyleBackColor = true;
      this.btnVideoCompressorProperties.Click += new System.EventHandler(this.btnVideoCompressorProperties_Click);
      // 
      // spcVideoAudio
      // 
      this.spcVideoAudio.Dock = System.Windows.Forms.DockStyle.Fill;
      this.spcVideoAudio.Location = new System.Drawing.Point(5, 5);
      this.spcVideoAudio.Name = "spcVideoAudio";
      this.spcVideoAudio.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // spcVideoAudio.Panel1
      // 
      this.spcVideoAudio.Panel1.Controls.Add(this.spcVideoPropPreview);
      this.spcVideoAudio.Panel1MinSize = 150;
      // 
      // spcVideoAudio.Panel2
      // 
      this.spcVideoAudio.Panel2.Controls.Add(this.spcAudioPropPreview);
      this.spcVideoAudio.Panel2MinSize = 100;
      this.spcVideoAudio.Size = new System.Drawing.Size(579, 257);
      this.spcVideoAudio.SplitterDistance = 151;
      this.spcVideoAudio.TabIndex = 6;
      // 
      // spcVideoPropPreview
      // 
      this.spcVideoPropPreview.Dock = System.Windows.Forms.DockStyle.Fill;
      this.spcVideoPropPreview.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
      this.spcVideoPropPreview.IsSplitterFixed = true;
      this.spcVideoPropPreview.Location = new System.Drawing.Point(0, 0);
      this.spcVideoPropPreview.Name = "spcVideoPropPreview";
      // 
      // spcVideoPropPreview.Panel1
      // 
      this.spcVideoPropPreview.Panel1.Controls.Add(this.groupBox1);
      this.spcVideoPropPreview.Panel1MinSize = 350;
      // 
      // spcVideoPropPreview.Panel2
      // 
      this.spcVideoPropPreview.Panel2.Controls.Add(this.grpPreview);
      this.spcVideoPropPreview.Size = new System.Drawing.Size(579, 151);
      this.spcVideoPropPreview.SplitterDistance = 400;
      this.spcVideoPropPreview.TabIndex = 5;
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.btnVideoCompressorProperties);
      this.groupBox1.Controls.Add(this.btnVideoDeviceProperties);
      this.groupBox1.Controls.Add(this.nudFrameRate);
      this.groupBox1.Controls.Add(this.label4);
      this.groupBox1.Controls.Add(this.label3);
      this.groupBox1.Controls.Add(this.label2);
      this.groupBox1.Controls.Add(this.cbbVideoSize);
      this.groupBox1.Controls.Add(this.label1);
      this.groupBox1.Controls.Add(this.pictureBox1);
      this.groupBox1.Controls.Add(this.cbbVideoDevices);
      this.groupBox1.Controls.Add(this.cbbVideoCompressor);
      this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.groupBox1.Location = new System.Drawing.Point(0, 0);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(400, 151);
      this.groupBox1.TabIndex = 4;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Video properties";
      // 
      // nudFrameRate
      // 
      this.nudFrameRate.Location = new System.Drawing.Point(147, 123);
      this.nudFrameRate.Name = "nudFrameRate";
      this.nudFrameRate.Size = new System.Drawing.Size(48, 20);
      this.nudFrameRate.TabIndex = 8;
      this.nudFrameRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.nudFrameRate.Leave += new System.EventHandler(this.nudFrameRate_Leave);
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(51, 125);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(87, 13);
      this.label4.TabIndex = 3;
      this.label4.Text = "Video frame rate:";
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(51, 93);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(58, 13);
      this.label3.TabIndex = 3;
      this.label3.Text = "Video size:";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(51, 60);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(94, 13);
      this.label2.TabIndex = 3;
      this.label2.Text = "Video compressor:";
      // 
      // cbbVideoSize
      // 
      this.cbbVideoSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbbVideoSize.FormattingEnabled = true;
      this.cbbVideoSize.Location = new System.Drawing.Point(147, 90);
      this.cbbVideoSize.Name = "cbbVideoSize";
      this.cbbVideoSize.Size = new System.Drawing.Size(103, 21);
      this.cbbVideoSize.Sorted = true;
      this.cbbVideoSize.TabIndex = 6;
      this.cbbVideoSize.SelectionChangeCommitted += new System.EventHandler(this.CbbVideoSize_SelectionChangeCommitted);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(51, 27);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(72, 13);
      this.label1.TabIndex = 3;
      this.label1.Text = "Video device:";
      // 
      // pictureBox1
      // 
      this.pictureBox1.Image = global::OgamaControls.Properties.Resources.video;
      this.pictureBox1.Location = new System.Drawing.Point(13, 27);
      this.pictureBox1.Name = "pictureBox1";
      this.pictureBox1.Size = new System.Drawing.Size(32, 32);
      this.pictureBox1.TabIndex = 2;
      this.pictureBox1.TabStop = false;
      // 
      // cbbVideoDevices
      // 
      this.cbbVideoDevices.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbbVideoDevices.FormattingEnabled = true;
      this.cbbVideoDevices.Location = new System.Drawing.Point(147, 24);
      this.cbbVideoDevices.Name = "cbbVideoDevices";
      this.cbbVideoDevices.Size = new System.Drawing.Size(210, 21);
      this.cbbVideoDevices.TabIndex = 1;
      this.cbbVideoDevices.SelectionChangeCommitted += new System.EventHandler(this.CbbVideoDevices_SelectionChangeCommitted);
      // 
      // cbbVideoCompressor
      // 
      this.cbbVideoCompressor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbbVideoCompressor.FormattingEnabled = true;
      this.cbbVideoCompressor.Location = new System.Drawing.Point(147, 57);
      this.cbbVideoCompressor.Name = "cbbVideoCompressor";
      this.cbbVideoCompressor.Size = new System.Drawing.Size(210, 21);
      this.cbbVideoCompressor.TabIndex = 1;
      this.cbbVideoCompressor.SelectionChangeCommitted += new System.EventHandler(this.cbbVideoCompressor_SelectionChangeCommitted);
      // 
      // grpPreview
      // 
      this.grpPreview.Controls.Add(this.panelPreview);
      this.grpPreview.Dock = System.Windows.Forms.DockStyle.Fill;
      this.grpPreview.Location = new System.Drawing.Point(0, 0);
      this.grpPreview.Name = "grpPreview";
      this.grpPreview.Padding = new System.Windows.Forms.Padding(10);
      this.grpPreview.Size = new System.Drawing.Size(175, 151);
      this.grpPreview.TabIndex = 3;
      this.grpPreview.TabStop = false;
      this.grpPreview.Text = "Preview";
      // 
      // panelPreview
      // 
      this.panelPreview.BackColor = System.Drawing.Color.Black;
      this.panelPreview.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panelPreview.Location = new System.Drawing.Point(10, 23);
      this.panelPreview.Name = "panelPreview";
      this.panelPreview.Size = new System.Drawing.Size(155, 118);
      this.panelPreview.TabIndex = 0;
      // 
      // spcAudioPropPreview
      // 
      this.spcAudioPropPreview.Dock = System.Windows.Forms.DockStyle.Fill;
      this.spcAudioPropPreview.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
      this.spcAudioPropPreview.IsSplitterFixed = true;
      this.spcAudioPropPreview.Location = new System.Drawing.Point(0, 0);
      this.spcAudioPropPreview.Name = "spcAudioPropPreview";
      // 
      // spcAudioPropPreview.Panel1
      // 
      this.spcAudioPropPreview.Panel1.Controls.Add(this.groupBox2);
      this.spcAudioPropPreview.Panel1MinSize = 350;
      // 
      // spcAudioPropPreview.Panel2
      // 
      this.spcAudioPropPreview.Panel2.Controls.Add(this.groupBox3);
      this.spcAudioPropPreview.Size = new System.Drawing.Size(579, 102);
      this.spcAudioPropPreview.SplitterDistance = 400;
      this.spcAudioPropPreview.TabIndex = 7;
      // 
      // groupBox2
      // 
      this.groupBox2.Controls.Add(this.btnAudioCompressorProperties);
      this.groupBox2.Controls.Add(this.btnAudioDeviceProperties);
      this.groupBox2.Controls.Add(this.label5);
      this.groupBox2.Controls.Add(this.label6);
      this.groupBox2.Controls.Add(this.cbbAudioDevices);
      this.groupBox2.Controls.Add(this.cbbAudioCompressor);
      this.groupBox2.Controls.Add(this.pictureBox2);
      this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.groupBox2.Location = new System.Drawing.Point(0, 0);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new System.Drawing.Size(400, 102);
      this.groupBox2.TabIndex = 5;
      this.groupBox2.TabStop = false;
      this.groupBox2.Text = "Audio properties";
      // 
      // btnAudioCompressorProperties
      // 
      this.btnAudioCompressorProperties.Image = global::OgamaControls.Properties.Resources.PropertiesHS;
      this.btnAudioCompressorProperties.Location = new System.Drawing.Point(363, 53);
      this.btnAudioCompressorProperties.Name = "btnAudioCompressorProperties";
      this.btnAudioCompressorProperties.Size = new System.Drawing.Size(23, 23);
      this.btnAudioCompressorProperties.TabIndex = 9;
      this.btnAudioCompressorProperties.UseVisualStyleBackColor = true;
      this.btnAudioCompressorProperties.Click += new System.EventHandler(this.btnAudioCompressorProperties_Click);
      // 
      // btnAudioDeviceProperties
      // 
      this.btnAudioDeviceProperties.Image = global::OgamaControls.Properties.Resources.PropertiesHS;
      this.btnAudioDeviceProperties.Location = new System.Drawing.Point(363, 20);
      this.btnAudioDeviceProperties.Name = "btnAudioDeviceProperties";
      this.btnAudioDeviceProperties.Size = new System.Drawing.Size(23, 23);
      this.btnAudioDeviceProperties.TabIndex = 9;
      this.btnAudioDeviceProperties.UseVisualStyleBackColor = true;
      this.btnAudioDeviceProperties.Click += new System.EventHandler(this.btnAudioDeviceProperties_Click);
      // 
      // cbbAudioDevices
      // 
      this.cbbAudioDevices.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbbAudioDevices.FormattingEnabled = true;
      this.cbbAudioDevices.Location = new System.Drawing.Point(147, 21);
      this.cbbAudioDevices.Name = "cbbAudioDevices";
      this.cbbAudioDevices.Size = new System.Drawing.Size(210, 21);
      this.cbbAudioDevices.TabIndex = 1;
      this.cbbAudioDevices.SelectionChangeCommitted += new System.EventHandler(this.cbbAudioDevices_SelectionChangeCommitted);
      // 
      // cbbAudioCompressor
      // 
      this.cbbAudioCompressor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbbAudioCompressor.FormattingEnabled = true;
      this.cbbAudioCompressor.Location = new System.Drawing.Point(147, 54);
      this.cbbAudioCompressor.Name = "cbbAudioCompressor";
      this.cbbAudioCompressor.Size = new System.Drawing.Size(210, 21);
      this.cbbAudioCompressor.TabIndex = 1;
      this.cbbAudioCompressor.SelectionChangeCommitted += new System.EventHandler(this.cbbAudioCompressor_SelectionChangeCommitted);
      // 
      // groupBox3
      // 
      this.groupBox3.Controls.Add(this.pmcAudio);
      this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
      this.groupBox3.Location = new System.Drawing.Point(0, 0);
      this.groupBox3.Name = "groupBox3";
      this.groupBox3.Size = new System.Drawing.Size(175, 102);
      this.groupBox3.TabIndex = 6;
      this.groupBox3.TabStop = false;
      this.groupBox3.Text = "Audio level";
      this.groupBox3.Visible = false;
      // 
      // pmcAudio
      // 
      this.pmcAudio.BandsCount = 25;
      this.pmcAudio.ColoredGrid = true;
      this.pmcAudio.ColorHigh = System.Drawing.Color.Red;
      this.pmcAudio.ColorHighBack = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
      this.pmcAudio.ColorMedium = System.Drawing.Color.Yellow;
      this.pmcAudio.ColorMediumBack = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(150)))));
      this.pmcAudio.ColorNormal = System.Drawing.Color.Green;
      this.pmcAudio.ColorNormalBack = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(255)))), ((int)(((byte)(150)))));
      this.pmcAudio.FalloffColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
      this.pmcAudio.GridColor = System.Drawing.Color.Gainsboro;
      this.pmcAudio.Location = new System.Drawing.Point(10, 19);
      this.pmcAudio.Name = "pmcAudio";
      this.pmcAudio.Size = new System.Drawing.Size(150, 78);
      this.pmcAudio.TabIndex = 0;
      // 
      // DSVideoProperties
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.spcVideoAudio);
      this.Name = "DSVideoProperties";
      this.Padding = new System.Windows.Forms.Padding(5);
      this.Size = new System.Drawing.Size(589, 267);
      this.Load += new System.EventHandler(this.DSVideoProperties_Load);
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
      this.spcVideoAudio.Panel1.ResumeLayout(false);
      this.spcVideoAudio.Panel2.ResumeLayout(false);
      this.spcVideoAudio.ResumeLayout(false);
      this.spcVideoPropPreview.Panel1.ResumeLayout(false);
      this.spcVideoPropPreview.Panel2.ResumeLayout(false);
      this.spcVideoPropPreview.ResumeLayout(false);
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.nudFrameRate)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
      this.grpPreview.ResumeLayout(false);
      this.spcAudioPropPreview.Panel1.ResumeLayout(false);
      this.spcAudioPropPreview.Panel2.ResumeLayout(false);
      this.spcAudioPropPreview.ResumeLayout(false);
      this.groupBox2.ResumeLayout(false);
      this.groupBox2.PerformLayout();
      this.groupBox3.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.Button btnVideoDeviceProperties;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.PictureBox pictureBox2;
    private System.Windows.Forms.Button btnVideoCompressorProperties;
    private System.Windows.Forms.SplitContainer spcVideoAudio;
    private System.Windows.Forms.SplitContainer spcVideoPropPreview;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.NumericUpDown nudFrameRate;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.ComboBox cbbVideoSize;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.PictureBox pictureBox1;
    private System.Windows.Forms.ComboBox cbbVideoDevices;
    private System.Windows.Forms.ComboBox cbbVideoCompressor;
    private System.Windows.Forms.GroupBox grpPreview;
    private System.Windows.Forms.SplitContainer spcAudioPropPreview;
    private System.Windows.Forms.GroupBox groupBox2;
    private System.Windows.Forms.Button btnAudioCompressorProperties;
    private System.Windows.Forms.Button btnAudioDeviceProperties;
    private System.Windows.Forms.ComboBox cbbAudioDevices;
    private System.Windows.Forms.ComboBox cbbAudioCompressor;
    private System.Windows.Forms.GroupBox groupBox3;
    private PeakMeterCtrl pmcAudio;
    private System.Windows.Forms.Panel panelPreview;
  }
}
