namespace Ogama.Modules.SlideshowDesign.DesignModule.StimuliDialogs
{
  using Ogama.Modules.Common.Controls;

  partial class FlashDialog
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FlashDialog));
      this.btnOK = new System.Windows.Forms.Button();
      this.btnCancel = new System.Windows.Forms.Button();
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      this.audioControl = new OgamaControls.AudioControl();
      this.pbcBorder = new OgamaControls.PenAndBrushControl();
      this.grpImageProperties = new System.Windows.Forms.GroupBox();
      this.label12 = new System.Windows.Forms.Label();
      this.txbFlashMovieFilename = new System.Windows.Forms.TextBox();
      this.btnOpenFlashMovieFile = new System.Windows.Forms.Button();
      this.grpPreview = new System.Windows.Forms.GroupBox();
      this.pnlPreview = new System.Windows.Forms.Panel();
      this.axShockwaveFlash = new AxShockwaveFlashObjects.AxShockwaveFlash();
      this.btnHelp = new System.Windows.Forms.Button();
      this.panel1 = new System.Windows.Forms.Panel();
      this.dialogTop1 = new DialogTop();
      this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      this.grpImageProperties.SuspendLayout();
      this.grpPreview.SuspendLayout();
      this.pnlPreview.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.axShockwaveFlash)).BeginInit();
      this.panel1.SuspendLayout();
      this.SuspendLayout();
      // 
      // btnOK
      // 
      this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.btnOK.Location = new System.Drawing.Point(350, 3);
      this.btnOK.Name = "btnOK";
      this.btnOK.Size = new System.Drawing.Size(65, 25);
      this.btnOK.TabIndex = 21;
      this.btnOK.Text = "OK";
      this.btnOK.UseVisualStyleBackColor = true;
      this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
      // 
      // btnCancel
      // 
      this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnCancel.Location = new System.Drawing.Point(427, 3);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(65, 25);
      this.btnCancel.TabIndex = 20;
      this.btnCancel.Text = "Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      // 
      // splitContainer1
      // 
      this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
      this.splitContainer1.IsSplitterFixed = true;
      this.splitContainer1.Location = new System.Drawing.Point(3, 3);
      this.splitContainer1.Name = "splitContainer1";
      this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // splitContainer1.Panel1
      // 
      this.splitContainer1.Panel1.Controls.Add(this.audioControl);
      this.splitContainer1.Panel1.Controls.Add(this.pbcBorder);
      this.splitContainer1.Panel1.Controls.Add(this.grpImageProperties);
      this.splitContainer1.Panel1.Controls.Add(this.grpPreview);
      // 
      // splitContainer1.Panel2
      // 
      this.splitContainer1.Panel2.Controls.Add(this.btnHelp);
      this.splitContainer1.Panel2.Controls.Add(this.btnOK);
      this.splitContainer1.Panel2.Controls.Add(this.btnCancel);
      this.splitContainer1.Size = new System.Drawing.Size(495, 316);
      this.splitContainer1.SplitterDistance = 282;
      this.splitContainer1.TabIndex = 23;
      // 
      // audioControl
      // 
      this.audioControl.Location = new System.Drawing.Point(223, 103);
      this.audioControl.Name = "audioControl";
      this.audioControl.ShouldPlay = false;
      this.audioControl.Size = new System.Drawing.Size(249, 72);
      this.audioControl.TabIndex = 29;
      this.audioControl.Visible = false;
      // 
      // pbcBorder
      // 
      this.pbcBorder.DrawAction = VectorGraphics.Elements.ShapeDrawAction.None;
      this.pbcBorder.Location = new System.Drawing.Point(226, 181);
      this.pbcBorder.Name = "pbcBorder";
      this.pbcBorder.NewFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.pbcBorder.NewFontColor = System.Drawing.SystemColors.GrayText;
      this.pbcBorder.NewName = "";
      this.pbcBorder.NewTextAlignment = VectorGraphics.Elements.VGAlignment.None;
      this.pbcBorder.Size = new System.Drawing.Size(246, 85);
      this.pbcBorder.TabIndex = 28;
      // 
      // grpImageProperties
      // 
      this.grpImageProperties.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.grpImageProperties.Controls.Add(this.label12);
      this.grpImageProperties.Controls.Add(this.txbFlashMovieFilename);
      this.grpImageProperties.Controls.Add(this.btnOpenFlashMovieFile);
      this.grpImageProperties.Location = new System.Drawing.Point(217, 12);
      this.grpImageProperties.Name = "grpImageProperties";
      this.grpImageProperties.Size = new System.Drawing.Size(269, 78);
      this.grpImageProperties.TabIndex = 27;
      this.grpImageProperties.TabStop = false;
      this.grpImageProperties.Text = "Flash movie properties";
      // 
      // label12
      // 
      this.label12.AutoSize = true;
      this.label12.Location = new System.Drawing.Point(6, 22);
      this.label12.Name = "label12";
      this.label12.Size = new System.Drawing.Size(49, 13);
      this.label12.TabIndex = 21;
      this.label12.Text = "Filename";
      // 
      // txbFlashMovieFilename
      // 
      this.txbFlashMovieFilename.Location = new System.Drawing.Point(61, 19);
      this.txbFlashMovieFilename.Multiline = true;
      this.txbFlashMovieFilename.Name = "txbFlashMovieFilename";
      this.txbFlashMovieFilename.ReadOnly = true;
      this.txbFlashMovieFilename.Size = new System.Drawing.Size(154, 21);
      this.txbFlashMovieFilename.TabIndex = 19;
      // 
      // btnOpenFlashMovieFile
      // 
      this.btnOpenFlashMovieFile.Image = global::Ogama.Properties.Resources.openfolderHS;
      this.btnOpenFlashMovieFile.Location = new System.Drawing.Point(221, 16);
      this.btnOpenFlashMovieFile.Name = "btnOpenFlashMovieFile";
      this.btnOpenFlashMovieFile.Size = new System.Drawing.Size(25, 25);
      this.btnOpenFlashMovieFile.TabIndex = 23;
      this.btnOpenFlashMovieFile.UseVisualStyleBackColor = true;
      this.btnOpenFlashMovieFile.Click += new System.EventHandler(this.btnOpenFlashMovieFile_Click);
      // 
      // grpPreview
      // 
      this.grpPreview.Controls.Add(this.pnlPreview);
      this.grpPreview.Location = new System.Drawing.Point(9, 12);
      this.grpPreview.Name = "grpPreview";
      this.grpPreview.Size = new System.Drawing.Size(202, 215);
      this.grpPreview.TabIndex = 24;
      this.grpPreview.TabStop = false;
      this.grpPreview.Text = "Preview";
      // 
      // pnlPreview
      // 
      this.pnlPreview.BackColor = System.Drawing.SystemColors.Window;
      this.pnlPreview.Controls.Add(this.axShockwaveFlash);
      this.pnlPreview.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pnlPreview.Location = new System.Drawing.Point(3, 16);
      this.pnlPreview.Name = "pnlPreview";
      this.pnlPreview.Size = new System.Drawing.Size(196, 196);
      this.pnlPreview.TabIndex = 1;
      // 
      // axShockwaveFlash
      // 
      this.axShockwaveFlash.Dock = System.Windows.Forms.DockStyle.Fill;
      this.axShockwaveFlash.Enabled = true;
      this.axShockwaveFlash.Location = new System.Drawing.Point(0, 0);
      this.axShockwaveFlash.Name = "axShockwaveFlash";
      this.axShockwaveFlash.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axShockwaveFlash.OcxState")));
      this.axShockwaveFlash.Size = new System.Drawing.Size(196, 196);
      this.axShockwaveFlash.TabIndex = 0;
      // 
      // btnHelp
      // 
      this.btnHelp.Image = global::Ogama.Properties.Resources.HelpBmp;
      this.btnHelp.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.btnHelp.Location = new System.Drawing.Point(3, 3);
      this.btnHelp.Name = "btnHelp";
      this.btnHelp.Size = new System.Drawing.Size(25, 25);
      this.btnHelp.TabIndex = 23;
      this.btnHelp.UseVisualStyleBackColor = true;
      this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.splitContainer1);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel1.Location = new System.Drawing.Point(0, 60);
      this.panel1.Name = "panel1";
      this.panel1.Padding = new System.Windows.Forms.Padding(3);
      this.panel1.Size = new System.Drawing.Size(501, 322);
      this.panel1.TabIndex = 19;
      // 
      // dialogTop1
      // 
      this.dialogTop1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(237)))));
      this.dialogTop1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("dialogTop1.BackgroundImage")));
      this.dialogTop1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
      this.dialogTop1.Description = "Please specify the flash movie file.";
      this.dialogTop1.Dock = System.Windows.Forms.DockStyle.Top;
      this.dialogTop1.Location = new System.Drawing.Point(0, 0);
      this.dialogTop1.Logo = global::Ogama.Properties.Resources.FlashPlayer;
      this.dialogTop1.Name = "dialogTop1";
      this.dialogTop1.Size = new System.Drawing.Size(501, 60);
      this.dialogTop1.TabIndex = 22;
      // 
      // toolTip1
      // 
      this.toolTip1.ShowAlways = true;
      // 
      // FlashDialog
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.btnCancel;
      this.ClientSize = new System.Drawing.Size(501, 382);
      this.Controls.Add(this.panel1);
      this.Controls.Add(this.dialogTop1);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "FlashDialog";
      this.ShowIcon = false;
      this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Add new flash movie ...";
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
      this.splitContainer1.ResumeLayout(false);
      this.grpImageProperties.ResumeLayout(false);
      this.grpImageProperties.PerformLayout();
      this.grpPreview.ResumeLayout(false);
      this.pnlPreview.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.axShockwaveFlash)).EndInit();
      this.panel1.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Button btnOK;
    private System.Windows.Forms.Button btnCancel;
    private DialogTop dialogTop1;
    private System.Windows.Forms.SplitContainer splitContainer1;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.Button btnHelp;
    private System.Windows.Forms.Button btnOpenFlashMovieFile;
    private System.Windows.Forms.Label label12;
    private System.Windows.Forms.TextBox txbFlashMovieFilename;
    private System.Windows.Forms.GroupBox grpPreview;
    private System.Windows.Forms.GroupBox grpImageProperties;
    private System.Windows.Forms.Panel pnlPreview;
    private System.Windows.Forms.ToolTip toolTip1;
    private AxShockwaveFlashObjects.AxShockwaveFlash axShockwaveFlash;
    private OgamaControls.PenAndBrushControl pbcBorder;
    private OgamaControls.AudioControl audioControl;
  }
}