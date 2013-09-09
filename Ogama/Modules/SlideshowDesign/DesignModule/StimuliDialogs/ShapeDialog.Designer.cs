namespace Ogama.Modules.SlideshowDesign.DesignModule.StimuliDialogs
{
  using Ogama.Modules.Common.Controls;

  using VectorGraphics.Tools.CustomEventArgs;

  partial class ShapeDialog
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShapeDialog));
      this.btnOK = new System.Windows.Forms.Button();
      this.btnCancel = new System.Windows.Forms.Button();
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      this.groupBox3 = new System.Windows.Forms.GroupBox();
      this.audioControl = new OgamaControls.AudioControl();
      this.groupBox2 = new System.Windows.Forms.GroupBox();
      this.pbcStyle = new OgamaControls.PenAndBrushControl();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.rdbEllipse = new System.Windows.Forms.RadioButton();
      this.rdbSharp = new System.Windows.Forms.RadioButton();
      this.rdbLine = new System.Windows.Forms.RadioButton();
      this.rdbPolyline = new System.Windows.Forms.RadioButton();
      this.rdbRectangle = new System.Windows.Forms.RadioButton();
      this.btnHelp = new System.Windows.Forms.Button();
      this.panel1 = new System.Windows.Forms.Panel();
      this.dialogTop1 = new DialogTop();
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      this.groupBox3.SuspendLayout();
      this.groupBox2.SuspendLayout();
      this.groupBox1.SuspendLayout();
      this.panel1.SuspendLayout();
      this.SuspendLayout();
      // 
      // btnOK
      // 
      this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.btnOK.Location = new System.Drawing.Point(259, 3);
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
      this.btnCancel.Location = new System.Drawing.Point(336, 3);
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
      this.splitContainer1.Panel1.Controls.Add(this.groupBox3);
      this.splitContainer1.Panel1.Controls.Add(this.groupBox2);
      this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
      // 
      // splitContainer1.Panel2
      // 
      this.splitContainer1.Panel2.Controls.Add(this.btnHelp);
      this.splitContainer1.Panel2.Controls.Add(this.btnOK);
      this.splitContainer1.Panel2.Controls.Add(this.btnCancel);
      this.splitContainer1.Size = new System.Drawing.Size(404, 259);
      this.splitContainer1.SplitterDistance = 225;
      this.splitContainer1.TabIndex = 23;
      // 
      // groupBox3
      // 
      this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.groupBox3.Controls.Add(this.audioControl);
      this.groupBox3.Location = new System.Drawing.Point(132, 129);
      this.groupBox3.Name = "groupBox3";
      this.groupBox3.Size = new System.Drawing.Size(258, 93);
      this.groupBox3.TabIndex = 31;
      this.groupBox3.TabStop = false;
      this.groupBox3.Text = "Audio properties";
      // 
      // audioControl
      // 
      this.audioControl.Dock = System.Windows.Forms.DockStyle.Fill;
      this.audioControl.Location = new System.Drawing.Point(3, 16);
      this.audioControl.Name = "audioControl";
      this.audioControl.ShouldPlay = false;
      this.audioControl.Size = new System.Drawing.Size(252, 74);
      this.audioControl.TabIndex = 0;
      // 
      // groupBox2
      // 
      this.groupBox2.Controls.Add(this.pbcStyle);
      this.groupBox2.Location = new System.Drawing.Point(133, 13);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new System.Drawing.Size(257, 111);
      this.groupBox2.TabIndex = 30;
      this.groupBox2.TabStop = false;
      this.groupBox2.Text = "Appearance";
      // 
      // pbcStyle
      // 
      this.pbcStyle.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pbcStyle.DrawAction = VectorGraphics.Elements.ShapeDrawAction.Edge;
      this.pbcStyle.Location = new System.Drawing.Point(3, 16);
      this.pbcStyle.Name = "pbcStyle";
      this.pbcStyle.NewFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.pbcStyle.NewFontColor = System.Drawing.SystemColors.GrayText;
      this.pbcStyle.NewName = "";
      this.pbcStyle.Size = new System.Drawing.Size(251, 92);
      this.pbcStyle.TabIndex = 25;
      this.pbcStyle.ShapePropertiesChanged += new System.EventHandler<ShapePropertiesChangedEventArgs>(this.pbcStyle_ShapePropertiesChanged);
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.rdbEllipse);
      this.groupBox1.Controls.Add(this.rdbSharp);
      this.groupBox1.Controls.Add(this.rdbLine);
      this.groupBox1.Controls.Add(this.rdbPolyline);
      this.groupBox1.Controls.Add(this.rdbRectangle);
      this.groupBox1.Location = new System.Drawing.Point(9, 13);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(107, 169);
      this.groupBox1.TabIndex = 29;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Type";
      // 
      // rdbEllipse
      // 
      this.rdbEllipse.Image = global::Ogama.Properties.Resources.Ellipse;
      this.rdbEllipse.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.rdbEllipse.Location = new System.Drawing.Point(6, 47);
      this.rdbEllipse.Name = "rdbEllipse";
      this.rdbEllipse.Size = new System.Drawing.Size(95, 24);
      this.rdbEllipse.TabIndex = 28;
      this.rdbEllipse.Text = "Ellipse";
      this.rdbEllipse.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.rdbEllipse.UseVisualStyleBackColor = true;
      // 
      // rdbSharp
      // 
      this.rdbSharp.Image = global::Ogama.Properties.Resources.Sharp;
      this.rdbSharp.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.rdbSharp.Location = new System.Drawing.Point(6, 103);
      this.rdbSharp.Name = "rdbSharp";
      this.rdbSharp.Size = new System.Drawing.Size(95, 24);
      this.rdbSharp.TabIndex = 28;
      this.rdbSharp.Text = "Sharp";
      this.rdbSharp.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.rdbSharp.UseVisualStyleBackColor = true;
      this.rdbSharp.CheckedChanged += new System.EventHandler(this.rdbLine_CheckedChanged);
      // 
      // rdbLine
      // 
      this.rdbLine.Image = global::Ogama.Properties.Resources.Line;
      this.rdbLine.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.rdbLine.Location = new System.Drawing.Point(6, 131);
      this.rdbLine.Name = "rdbLine";
      this.rdbLine.Size = new System.Drawing.Size(95, 24);
      this.rdbLine.TabIndex = 28;
      this.rdbLine.Text = "Line";
      this.rdbLine.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.rdbLine.UseVisualStyleBackColor = true;
      this.rdbLine.CheckedChanged += new System.EventHandler(this.rdbLine_CheckedChanged);
      // 
      // rdbPolyline
      // 
      this.rdbPolyline.Image = global::Ogama.Properties.Resources.Polyline;
      this.rdbPolyline.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.rdbPolyline.Location = new System.Drawing.Point(6, 75);
      this.rdbPolyline.Name = "rdbPolyline";
      this.rdbPolyline.Size = new System.Drawing.Size(95, 24);
      this.rdbPolyline.TabIndex = 28;
      this.rdbPolyline.Text = "Polyline";
      this.rdbPolyline.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.rdbPolyline.UseVisualStyleBackColor = true;
      // 
      // rdbRectangle
      // 
      this.rdbRectangle.Checked = true;
      this.rdbRectangle.Image = global::Ogama.Properties.Resources.Rectangle;
      this.rdbRectangle.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.rdbRectangle.Location = new System.Drawing.Point(6, 19);
      this.rdbRectangle.Name = "rdbRectangle";
      this.rdbRectangle.Size = new System.Drawing.Size(95, 24);
      this.rdbRectangle.TabIndex = 28;
      this.rdbRectangle.TabStop = true;
      this.rdbRectangle.Text = "Rectangle";
      this.rdbRectangle.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.rdbRectangle.UseVisualStyleBackColor = true;
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
      this.panel1.Size = new System.Drawing.Size(410, 265);
      this.panel1.TabIndex = 19;
      // 
      // dialogTop1
      // 
      this.dialogTop1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(237)))));
      this.dialogTop1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("dialogTop1.BackgroundImage")));
      this.dialogTop1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
      this.dialogTop1.Description = "Please specify the shapes type, and define its appearance.";
      this.dialogTop1.Dock = System.Windows.Forms.DockStyle.Top;
      this.dialogTop1.Location = new System.Drawing.Point(0, 0);
      this.dialogTop1.Logo = global::Ogama.Properties.Resources.GenericShape;
      this.dialogTop1.Name = "dialogTop1";
      this.dialogTop1.Size = new System.Drawing.Size(410, 60);
      this.dialogTop1.TabIndex = 22;
      // 
      // ShapeDialog
      // 
      this.AcceptButton = this.btnOK;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.btnCancel;
      this.ClientSize = new System.Drawing.Size(410, 325);
      this.Controls.Add(this.panel1);
      this.Controls.Add(this.dialogTop1);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "ShapeDialog";
      this.ShowIcon = false;
      this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Add new shape ...";
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel2.ResumeLayout(false);
      this.splitContainer1.ResumeLayout(false);
      this.groupBox3.ResumeLayout(false);
      this.groupBox2.ResumeLayout(false);
      this.groupBox1.ResumeLayout(false);
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
    private OgamaControls.PenAndBrushControl pbcStyle;
    private System.Windows.Forms.GroupBox groupBox2;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.RadioButton rdbEllipse;
    private System.Windows.Forms.RadioButton rdbPolyline;
    private System.Windows.Forms.RadioButton rdbRectangle;
    private System.Windows.Forms.RadioButton rdbLine;
    private System.Windows.Forms.RadioButton rdbSharp;
    private System.Windows.Forms.GroupBox groupBox3;
    private OgamaControls.AudioControl audioControl;
  }
}