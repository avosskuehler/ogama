namespace Ogama.Modules.Replay
{
  partial class VideoPropertiesDialog
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
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VideoPropertiesDialog));
      this.btnOK = new System.Windows.Forms.Button();
      this.btnCancel = new System.Windows.Forms.Button();
      this.checkBox1 = new System.Windows.Forms.CheckBox();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.psbUserCamera = new OgamaControls.PositionButton(this.components);
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      this.dsVideoProperties = new OgamaControls.DSVideoProperties();
      this.splitContainer2 = new System.Windows.Forms.SplitContainer();
      this.dialogTop1 = new Ogama.Modules.Common.DialogTop();
      this.groupBox1.SuspendLayout();
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      this.splitContainer2.Panel1.SuspendLayout();
      this.splitContainer2.Panel2.SuspendLayout();
      this.splitContainer2.SuspendLayout();
      this.SuspendLayout();
      // 
      // btnOK
      // 
      this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.btnOK.Location = new System.Drawing.Point(236, 8);
      this.btnOK.Name = "btnOK";
      this.btnOK.Size = new System.Drawing.Size(75, 23);
      this.btnOK.TabIndex = 1;
      this.btnOK.Text = "OK";
      this.btnOK.UseVisualStyleBackColor = true;
      // 
      // btnCancel
      // 
      this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnCancel.Location = new System.Drawing.Point(313, 8);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(75, 23);
      this.btnCancel.TabIndex = 2;
      this.btnCancel.Text = "Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      // 
      // checkBox1
      // 
      this.checkBox1.AutoSize = true;
      this.checkBox1.Location = new System.Drawing.Point(14, 21);
      this.checkBox1.Name = "checkBox1";
      this.checkBox1.Size = new System.Drawing.Size(150, 17);
      this.checkBox1.TabIndex = 6;
      this.checkBox1.Text = "overlay user camera video";
      this.checkBox1.UseVisualStyleBackColor = true;
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.psbUserCamera);
      this.groupBox1.Controls.Add(this.checkBox1);
      this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.groupBox1.Location = new System.Drawing.Point(5, 5);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Padding = new System.Windows.Forms.Padding(5);
      this.groupBox1.Size = new System.Drawing.Size(140, 15);
      this.groupBox1.TabIndex = 7;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "User camera";
      // 
      // psbUserCamera
      // 
      this.psbUserCamera.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
      this.psbUserCamera.CurrentPosition = new System.Drawing.Point(0, 0);
      this.psbUserCamera.Location = new System.Drawing.Point(187, 19);
      this.psbUserCamera.MinimumSize = new System.Drawing.Size(80, 22);
      this.psbUserCamera.Name = "psbUserCamera";
      this.psbUserCamera.Size = new System.Drawing.Size(80, 22);
      this.psbUserCamera.StimulusScreenSize = new System.Drawing.Size(0, 0);
      this.psbUserCamera.TabIndex = 7;
      this.psbUserCamera.Text = "(0,0)";
      this.psbUserCamera.TextAlign = System.Drawing.ContentAlignment.TopLeft;
      this.psbUserCamera.UseVisualStyleBackColor = true;
      // 
      // splitContainer1
      // 
      this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer1.Location = new System.Drawing.Point(0, 60);
      this.splitContainer1.Name = "splitContainer1";
      this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // splitContainer1.Panel1
      // 
      this.splitContainer1.Panel1.Controls.Add(this.dsVideoProperties);
      this.splitContainer1.Panel1.Padding = new System.Windows.Forms.Padding(3);
      // 
      // splitContainer1.Panel2
      // 
      this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
      this.splitContainer1.Panel2MinSize = 40;
      this.splitContainer1.Size = new System.Drawing.Size(400, 315);
      this.splitContainer1.SplitterDistance = 268;
      this.splitContainer1.TabIndex = 8;
      // 
      // dsVideoProperties
      // 
      this.dsVideoProperties.Dock = System.Windows.Forms.DockStyle.Fill;
      this.dsVideoProperties.Location = new System.Drawing.Point(3, 3);
      this.dsVideoProperties.Mode = ((OgamaControls.DSVideoProperties.DisplayMode)((OgamaControls.DSVideoProperties.DisplayMode.VideoPlayback | OgamaControls.DSVideoProperties.DisplayMode.AudioPlayback)));
      this.dsVideoProperties.Name = "dsVideoProperties";
      this.dsVideoProperties.Padding = new System.Windows.Forms.Padding(3);
      this.dsVideoProperties.ShouldPreview = false;
      this.dsVideoProperties.Size = new System.Drawing.Size(394, 262);
      this.dsVideoProperties.TabIndex = 0;
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
      this.splitContainer2.Panel1.Padding = new System.Windows.Forms.Padding(5);
      this.splitContainer2.Panel1Collapsed = true;
      // 
      // splitContainer2.Panel2
      // 
      this.splitContainer2.Panel2.Controls.Add(this.btnCancel);
      this.splitContainer2.Panel2.Controls.Add(this.btnOK);
      this.splitContainer2.Size = new System.Drawing.Size(400, 43);
      this.splitContainer2.SplitterDistance = 25;
      this.splitContainer2.TabIndex = 0;
      // 
      // dialogTop1
      // 
      this.dialogTop1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(237)))));
      this.dialogTop1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("dialogTop1.BackgroundImage")));
      this.dialogTop1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
      this.dialogTop1.Description = "Modify video options ...";
      this.dialogTop1.Dock = System.Windows.Forms.DockStyle.Top;
      this.dialogTop1.Location = new System.Drawing.Point(0, 0);
      this.dialogTop1.Logo = global::Ogama.Properties.Resources.otheroptions;
      this.dialogTop1.Name = "dialogTop1";
      this.dialogTop1.Size = new System.Drawing.Size(400, 60);
      this.dialogTop1.TabIndex = 5;
      this.dialogTop1.TabStop = false;
      // 
      // VideoPropertiesDialog
      // 
      this.AcceptButton = this.btnOK;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.btnCancel;
      this.ClientSize = new System.Drawing.Size(400, 375);
      this.Controls.Add(this.splitContainer1);
      this.Controls.Add(this.dialogTop1);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "VideoPropertiesDialog";
      this.ShowIcon = false;
      this.Text = "Options ..";
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel2.ResumeLayout(false);
      this.splitContainer1.ResumeLayout(false);
      this.splitContainer2.Panel1.ResumeLayout(false);
      this.splitContainer2.Panel2.ResumeLayout(false);
      this.splitContainer2.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Button btnOK;
    private System.Windows.Forms.Button btnCancel;
    private Ogama.Modules.Common.DialogTop dialogTop1;
    private System.Windows.Forms.CheckBox checkBox1;
    private System.Windows.Forms.GroupBox groupBox1;
    private OgamaControls.PositionButton psbUserCamera;
    private System.Windows.Forms.SplitContainer splitContainer1;
    private OgamaControls.DSVideoProperties dsVideoProperties;
    private System.Windows.Forms.SplitContainer splitContainer2;
  }
}