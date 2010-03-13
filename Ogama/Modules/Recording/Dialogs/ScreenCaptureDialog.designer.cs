namespace Ogama.Modules.Recording
{
  partial class ScreenCaptureDialog
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScreenCaptureDialog));
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      this.splitContainer2 = new System.Windows.Forms.SplitContainer();
      this.btnEncoderProperties = new System.Windows.Forms.Button();
      this.btnScreenCaptureProperties = new System.Windows.Forms.Button();
      this.cbbEncoderFilter = new System.Windows.Forms.ComboBox();
      this.cbbCaptureFilter = new System.Windows.Forms.ComboBox();
      this.label10 = new System.Windows.Forms.Label();
      this.label9 = new System.Windows.Forms.Label();
      this.btnOK = new System.Windows.Forms.Button();
      this.dialogTop1 = new Ogama.Modules.Common.DialogTop();
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
      this.splitContainer1.Size = new System.Drawing.Size(403, 231);
      this.splitContainer1.SplitterDistance = 60;
      this.splitContainer1.TabIndex = 24;
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
      this.splitContainer2.Panel1.Controls.Add(this.btnEncoderProperties);
      this.splitContainer2.Panel1.Controls.Add(this.btnScreenCaptureProperties);
      this.splitContainer2.Panel1.Controls.Add(this.cbbEncoderFilter);
      this.splitContainer2.Panel1.Controls.Add(this.cbbCaptureFilter);
      this.splitContainer2.Panel1.Controls.Add(this.label10);
      this.splitContainer2.Panel1.Controls.Add(this.label9);
      // 
      // splitContainer2.Panel2
      // 
      this.splitContainer2.Panel2.Controls.Add(this.btnOK);
      this.splitContainer2.Panel2MinSize = 35;
      this.splitContainer2.Size = new System.Drawing.Size(403, 167);
      this.splitContainer2.SplitterDistance = 128;
      this.splitContainer2.TabIndex = 0;
      // 
      // btnEncoderProperties
      // 
      this.btnEncoderProperties.Location = new System.Drawing.Point(82, 92);
      this.btnEncoderProperties.Name = "btnEncoderProperties";
      this.btnEncoderProperties.Size = new System.Drawing.Size(201, 23);
      this.btnEncoderProperties.TabIndex = 25;
      this.btnEncoderProperties.Text = "Screen Capture Encoder Properties";
      this.btnEncoderProperties.UseVisualStyleBackColor = true;
      this.btnEncoderProperties.Click += new System.EventHandler(this.btnEncoderProperties_Click);
      // 
      // btnScreenCaptureProperties
      // 
      this.btnScreenCaptureProperties.Location = new System.Drawing.Point(82, 36);
      this.btnScreenCaptureProperties.Name = "btnScreenCaptureProperties";
      this.btnScreenCaptureProperties.Size = new System.Drawing.Size(201, 23);
      this.btnScreenCaptureProperties.TabIndex = 25;
      this.btnScreenCaptureProperties.Text = "Screen Capture Filter Properties";
      this.btnScreenCaptureProperties.UseVisualStyleBackColor = true;
      this.btnScreenCaptureProperties.Click += new System.EventHandler(this.btnScreenCaptureProperties_Click);
      // 
      // cbbEncoderFilter
      // 
      this.cbbEncoderFilter.FormattingEnabled = true;
      this.cbbEncoderFilter.Location = new System.Drawing.Point(82, 65);
      this.cbbEncoderFilter.Name = "cbbEncoderFilter";
      this.cbbEncoderFilter.Size = new System.Drawing.Size(310, 21);
      this.cbbEncoderFilter.TabIndex = 24;
      // 
      // cbbCaptureFilter
      // 
      this.cbbCaptureFilter.Enabled = false;
      this.cbbCaptureFilter.FormattingEnabled = true;
      this.cbbCaptureFilter.Location = new System.Drawing.Point(82, 9);
      this.cbbCaptureFilter.Name = "cbbCaptureFilter";
      this.cbbCaptureFilter.Size = new System.Drawing.Size(310, 21);
      this.cbbCaptureFilter.TabIndex = 24;
      // 
      // label10
      // 
      this.label10.AutoSize = true;
      this.label10.Location = new System.Drawing.Point(7, 68);
      this.label10.Name = "label10";
      this.label10.Size = new System.Drawing.Size(69, 13);
      this.label10.TabIndex = 0;
      this.label10.Text = "Encode with:";
      // 
      // label9
      // 
      this.label9.AutoSize = true;
      this.label9.Location = new System.Drawing.Point(7, 12);
      this.label9.Name = "label9";
      this.label9.Size = new System.Drawing.Size(69, 13);
      this.label9.TabIndex = 0;
      this.label9.Text = "Capture with:";
      // 
      // btnOK
      // 
      this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.btnOK.Location = new System.Drawing.Point(310, 0);
      this.btnOK.Name = "btnOK";
      this.btnOK.Size = new System.Drawing.Size(81, 23);
      this.btnOK.TabIndex = 0;
      this.btnOK.Text = "&OK";
      this.btnOK.UseVisualStyleBackColor = true;
      this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
      // 
      // dialogTop1
      // 
      this.dialogTop1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(237)))));
      this.dialogTop1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("dialogTop1.BackgroundImage")));
      this.dialogTop1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
      this.dialogTop1.Description = "Specify settings for the video screen capture during record.";
      this.dialogTop1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.dialogTop1.Location = new System.Drawing.Point(0, 0);
      this.dialogTop1.Logo = global::Ogama.Properties.Resources.otheroptions;
      this.dialogTop1.Name = "dialogTop1";
      this.dialogTop1.Size = new System.Drawing.Size(403, 60);
      this.dialogTop1.TabIndex = 5;
      // 
      // ScreenCaptureDialog
      // 
      this.AcceptButton = this.btnOK;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(403, 231);
      this.Controls.Add(this.splitContainer1);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MaximizeBox = false;
      this.Name = "ScreenCaptureDialog";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Screen Capture Settings ...";
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel2.ResumeLayout(false);
      this.splitContainer1.ResumeLayout(false);
      this.splitContainer2.Panel1.ResumeLayout(false);
      this.splitContainer2.Panel1.PerformLayout();
      this.splitContainer2.Panel2.ResumeLayout(false);
      this.splitContainer2.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.SplitContainer splitContainer1;
    private Ogama.Modules.Common.DialogTop dialogTop1;
    private System.Windows.Forms.SplitContainer splitContainer2;
    private System.Windows.Forms.ComboBox cbbCaptureFilter;
    private System.Windows.Forms.Label label9;
    private System.Windows.Forms.Button btnOK;
    private System.Windows.Forms.Label label10;
    private System.Windows.Forms.ComboBox cbbEncoderFilter;
    private System.Windows.Forms.Button btnScreenCaptureProperties;
    private System.Windows.Forms.Button btnEncoderProperties;
  }
}

