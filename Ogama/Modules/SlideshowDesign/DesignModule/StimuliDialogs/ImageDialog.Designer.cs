namespace Ogama.Modules.SlideshowDesign.DesignModule.StimuliDialogs
{
  using Ogama.Modules.Common.Controls;

  using VectorGraphics.Tools.CustomEventArgs;

  partial class ImageDialog
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImageDialog));
      this.btnOK = new System.Windows.Forms.Button();
      this.btnCancel = new System.Windows.Forms.Button();
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.audioControl = new OgamaControls.AudioControl();
      this.grpImageProperties = new System.Windows.Forms.GroupBox();
      this.cbbImageLayout = new System.Windows.Forms.ComboBox();
      this.label12 = new System.Windows.Forms.Label();
      this.txbImageFilename = new System.Windows.Forms.TextBox();
      this.label8 = new System.Windows.Forms.Label();
      this.btnOpenImageFile = new System.Windows.Forms.Button();
      this.grpImageBorder = new System.Windows.Forms.GroupBox();
      this.pbcImageBorder = new OgamaControls.PenAndBrushControl();
      this.grpPreview = new System.Windows.Forms.GroupBox();
      this.btnHelp = new System.Windows.Forms.Button();
      this.panel1 = new System.Windows.Forms.Panel();
      this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
      this.dialogTop1 = new DialogTop();
      this.panelPreview = new OgamaControls.DoubleBufferPanel();
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      this.groupBox1.SuspendLayout();
      this.grpImageProperties.SuspendLayout();
      this.grpImageBorder.SuspendLayout();
      this.grpPreview.SuspendLayout();
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
      this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
      this.splitContainer1.Panel1.Controls.Add(this.grpImageProperties);
      this.splitContainer1.Panel1.Controls.Add(this.grpImageBorder);
      this.splitContainer1.Panel1.Controls.Add(this.grpPreview);
      // 
      // splitContainer1.Panel2
      // 
      this.splitContainer1.Panel2.Controls.Add(this.btnHelp);
      this.splitContainer1.Panel2.Controls.Add(this.btnOK);
      this.splitContainer1.Panel2.Controls.Add(this.btnCancel);
      this.splitContainer1.Size = new System.Drawing.Size(495, 361);
      this.splitContainer1.SplitterDistance = 327;
      this.splitContainer1.TabIndex = 23;
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.audioControl);
      this.groupBox1.Location = new System.Drawing.Point(228, 233);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(258, 87);
      this.groupBox1.TabIndex = 28;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Audio properties";
      // 
      // audioControl
      // 
      this.audioControl.Dock = System.Windows.Forms.DockStyle.Fill;
      this.audioControl.Location = new System.Drawing.Point(3, 16);
      this.audioControl.Name = "audioControl";
      this.audioControl.ShouldPlay = false;
      this.audioControl.Size = new System.Drawing.Size(252, 68);
      this.audioControl.TabIndex = 0;
      // 
      // grpImageProperties
      // 
      this.grpImageProperties.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.grpImageProperties.Controls.Add(this.cbbImageLayout);
      this.grpImageProperties.Controls.Add(this.label12);
      this.grpImageProperties.Controls.Add(this.txbImageFilename);
      this.grpImageProperties.Controls.Add(this.label8);
      this.grpImageProperties.Controls.Add(this.btnOpenImageFile);
      this.grpImageProperties.Location = new System.Drawing.Point(228, 12);
      this.grpImageProperties.Name = "grpImageProperties";
      this.grpImageProperties.Size = new System.Drawing.Size(258, 78);
      this.grpImageProperties.TabIndex = 27;
      this.grpImageProperties.TabStop = false;
      this.grpImageProperties.Text = "Image properties";
      // 
      // cbbImageLayout
      // 
      this.cbbImageLayout.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbbImageLayout.FormattingEnabled = true;
      this.cbbImageLayout.Location = new System.Drawing.Point(85, 48);
      this.cbbImageLayout.Name = "cbbImageLayout";
      this.cbbImageLayout.Size = new System.Drawing.Size(83, 21);
      this.cbbImageLayout.TabIndex = 20;
      this.toolTip1.SetToolTip(this.cbbImageLayout, "When set to \"None\" you can specify \r\nsize and position of the image on the slide " +
              "\r\nwith the mouse after clicking OK.");
      this.cbbImageLayout.SelectionChangeCommitted += new System.EventHandler(this.cbbImageLayout_SelectionChangeCommitted);
      // 
      // label12
      // 
      this.label12.AutoSize = true;
      this.label12.Location = new System.Drawing.Point(25, 22);
      this.label12.Name = "label12";
      this.label12.Size = new System.Drawing.Size(49, 13);
      this.label12.TabIndex = 21;
      this.label12.Text = "Filename";
      // 
      // txbImageFilename
      // 
      this.txbImageFilename.Location = new System.Drawing.Point(85, 19);
      this.txbImageFilename.Multiline = true;
      this.txbImageFilename.Name = "txbImageFilename";
      this.txbImageFilename.ReadOnly = true;
      this.txbImageFilename.Size = new System.Drawing.Size(130, 21);
      this.txbImageFilename.TabIndex = 19;
      // 
      // label8
      // 
      this.label8.AutoSize = true;
      this.label8.Location = new System.Drawing.Point(25, 48);
      this.label8.Name = "label8";
      this.label8.Size = new System.Drawing.Size(39, 13);
      this.label8.TabIndex = 22;
      this.label8.Text = "Layout";
      // 
      // btnOpenImageFile
      // 
      this.btnOpenImageFile.Image = global::Ogama.Properties.Resources.openfolderHS;
      this.btnOpenImageFile.Location = new System.Drawing.Point(221, 16);
      this.btnOpenImageFile.Name = "btnOpenImageFile";
      this.btnOpenImageFile.Size = new System.Drawing.Size(25, 25);
      this.btnOpenImageFile.TabIndex = 23;
      this.btnOpenImageFile.UseVisualStyleBackColor = true;
      this.btnOpenImageFile.Click += new System.EventHandler(this.btnOpenImageFile_Click);
      // 
      // grpImageBorder
      // 
      this.grpImageBorder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.grpImageBorder.Controls.Add(this.pbcImageBorder);
      this.grpImageBorder.Location = new System.Drawing.Point(228, 98);
      this.grpImageBorder.Name = "grpImageBorder";
      this.grpImageBorder.Size = new System.Drawing.Size(258, 129);
      this.grpImageBorder.TabIndex = 26;
      this.grpImageBorder.TabStop = false;
      this.grpImageBorder.Text = "Border and additional fill style ...";
      // 
      // pbcImageBorder
      // 
      this.pbcImageBorder.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pbcImageBorder.DrawAction = VectorGraphics.Elements.ShapeDrawAction.None;
      this.pbcImageBorder.Location = new System.Drawing.Point(3, 16);
      this.pbcImageBorder.Name = "pbcImageBorder";
      this.pbcImageBorder.NewFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.pbcImageBorder.NewFontColor = System.Drawing.SystemColors.GrayText;
      this.pbcImageBorder.NewName = "";
      this.pbcImageBorder.NewTextAlignment = VectorGraphics.Elements.VGAlignment.None;
      this.pbcImageBorder.Size = new System.Drawing.Size(252, 110);
      this.pbcImageBorder.TabIndex = 25;
      this.pbcImageBorder.ShapePropertiesChanged += new System.EventHandler<ShapePropertiesChangedEventArgs>(this.pbcImageBorder_ShapePropertiesChanged);
      // 
      // grpPreview
      // 
      this.grpPreview.Controls.Add(this.panelPreview);
      this.grpPreview.Location = new System.Drawing.Point(9, 12);
      this.grpPreview.Name = "grpPreview";
      this.grpPreview.Size = new System.Drawing.Size(202, 215);
      this.grpPreview.TabIndex = 24;
      this.grpPreview.TabStop = false;
      this.grpPreview.Text = "Preview";
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
      this.panel1.Size = new System.Drawing.Size(501, 367);
      this.panel1.TabIndex = 19;
      // 
      // toolTip1
      // 
      this.toolTip1.ShowAlways = true;
      // 
      // dialogTop1
      // 
      this.dialogTop1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(237)))));
      this.dialogTop1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("dialogTop1.BackgroundImage")));
      this.dialogTop1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
      this.dialogTop1.Description = "Please specify the image file, and define its layout on the slide.";
      this.dialogTop1.Dock = System.Windows.Forms.DockStyle.Top;
      this.dialogTop1.Location = new System.Drawing.Point(0, 0);
      this.dialogTop1.Logo = global::Ogama.Properties.Resources.GenericPicDoc;
      this.dialogTop1.Name = "dialogTop1";
      this.dialogTop1.Size = new System.Drawing.Size(501, 60);
      this.dialogTop1.TabIndex = 22;
      // 
      // panelPreview
      // 
      this.panelPreview.BackColor = System.Drawing.SystemColors.Window;
      this.panelPreview.Dock = System.Windows.Forms.DockStyle.Top;
      this.panelPreview.Location = new System.Drawing.Point(3, 16);
      this.panelPreview.Name = "panelPreview";
      this.panelPreview.Size = new System.Drawing.Size(196, 193);
      this.panelPreview.TabIndex = 0;
      this.panelPreview.Paint += new System.Windows.Forms.PaintEventHandler(this.panelPreview_Paint);
      // 
      // ImageDialog
      // 
      this.AcceptButton = this.btnOK;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.btnCancel;
      this.ClientSize = new System.Drawing.Size(501, 427);
      this.Controls.Add(this.panel1);
      this.Controls.Add(this.dialogTop1);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "ImageDialog";
      this.ShowIcon = false;
      this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Add new image ...";
      this.Load += new System.EventHandler(this.frmNewImage_Load);
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel2.ResumeLayout(false);
      this.splitContainer1.ResumeLayout(false);
      this.groupBox1.ResumeLayout(false);
      this.grpImageProperties.ResumeLayout(false);
      this.grpImageProperties.PerformLayout();
      this.grpImageBorder.ResumeLayout(false);
      this.grpPreview.ResumeLayout(false);
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
    private System.Windows.Forms.Button btnOpenImageFile;
    private System.Windows.Forms.Label label8;
    private System.Windows.Forms.Label label12;
    private System.Windows.Forms.TextBox txbImageFilename;
    private System.Windows.Forms.ComboBox cbbImageLayout;
    private System.Windows.Forms.GroupBox grpPreview;
    private System.Windows.Forms.GroupBox grpImageProperties;
    private System.Windows.Forms.GroupBox grpImageBorder;
    private OgamaControls.PenAndBrushControl pbcImageBorder;
    private System.Windows.Forms.ToolTip toolTip1;
    private System.Windows.Forms.GroupBox groupBox1;
    private OgamaControls.AudioControl audioControl;
    private OgamaControls.DoubleBufferPanel panelPreview;
  }
}