namespace Ogama.Modules.Scanpath.Colorization
{
  using Ogama.Modules.Common.Controls;

  partial class ColorDefinitionDialog
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
      OgamaControls.Gradient gradient1 = new OgamaControls.Gradient();
      System.Drawing.Drawing2D.ColorBlend colorBlend1 = new System.Drawing.Drawing2D.ColorBlend();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ColorDefinitionDialog));
      this.splitContainer2 = new System.Windows.Forms.SplitContainer();
      this.trvSubjects = new System.Windows.Forms.TreeView();
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      this.btnNumbersStyle = new System.Windows.Forms.Button();
      this.btnConnectionsStyle = new System.Windows.Forms.Button();
      this.btnFixationsStyle = new System.Windows.Forms.Button();
      this.panel1 = new System.Windows.Forms.Panel();
      this.pnlPreview = new System.Windows.Forms.Panel();
      this.splitContainer3 = new System.Windows.Forms.SplitContainer();
      this.rdbColorSubjects = new System.Windows.Forms.RadioButton();
      this.cbbPredefinedGradient = new System.Windows.Forms.ComboBox();
      this.rdbColorGroups = new System.Windows.Forms.RadioButton();
      this.label3 = new System.Windows.Forms.Label();
      this.rdbColorAutomatic = new System.Windows.Forms.RadioButton();
      this.label2 = new System.Windows.Forms.Label();
      this.label1 = new System.Windows.Forms.Label();
      this.spcGradientSingle = new System.Windows.Forms.SplitContainer();
      this.gradientControl = new OgamaControls.GradientTypeEditorUI();
      this.grpPreview = new System.Windows.Forms.GroupBox();
      this.spcPreview = new System.Windows.Forms.SplitContainer();
      this.btnOK = new System.Windows.Forms.Button();
      this.btnCancel = new System.Windows.Forms.Button();
      this.dialogTop1 = new DialogTop();
      this.splitContainer2.Panel1.SuspendLayout();
      this.splitContainer2.Panel2.SuspendLayout();
      this.splitContainer2.SuspendLayout();
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      this.panel1.SuspendLayout();
      this.splitContainer3.Panel1.SuspendLayout();
      this.splitContainer3.Panel2.SuspendLayout();
      this.splitContainer3.SuspendLayout();
      this.spcGradientSingle.Panel1.SuspendLayout();
      this.spcGradientSingle.Panel2.SuspendLayout();
      this.spcGradientSingle.SuspendLayout();
      this.grpPreview.SuspendLayout();
      this.spcPreview.Panel1.SuspendLayout();
      this.spcPreview.Panel2.SuspendLayout();
      this.spcPreview.SuspendLayout();
      this.SuspendLayout();
      // 
      // splitContainer2
      // 
      this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer2.Location = new System.Drawing.Point(0, 60);
      this.splitContainer2.Name = "splitContainer2";
      // 
      // splitContainer2.Panel1
      // 
      this.splitContainer2.Panel1.Controls.Add(this.trvSubjects);
      // 
      // splitContainer2.Panel2
      // 
      this.splitContainer2.Panel2.Controls.Add(this.splitContainer1);
      this.splitContainer2.Size = new System.Drawing.Size(719, 431);
      this.splitContainer2.SplitterDistance = 213;
      this.splitContainer2.TabIndex = 1;
      // 
      // trvSubjects
      // 
      this.trvSubjects.Dock = System.Windows.Forms.DockStyle.Fill;
      this.trvSubjects.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawText;
      this.trvSubjects.Location = new System.Drawing.Point(0, 0);
      this.trvSubjects.Name = "trvSubjects";
      this.trvSubjects.Size = new System.Drawing.Size(213, 431);
      this.trvSubjects.TabIndex = 1;
      this.trvSubjects.DrawNode += new System.Windows.Forms.DrawTreeNodeEventHandler(this.trvSubjects_DrawNode);
      this.trvSubjects.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.trvSubjects_AfterSelect);
      // 
      // splitContainer1
      // 
      this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
      this.splitContainer1.IsSplitterFixed = true;
      this.splitContainer1.Location = new System.Drawing.Point(0, 0);
      this.splitContainer1.Name = "splitContainer1";
      this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // splitContainer1.Panel1
      // 
      this.splitContainer1.Panel1.Controls.Add(this.splitContainer3);
      // 
      // splitContainer1.Panel2
      // 
      this.splitContainer1.Panel2.Controls.Add(this.btnOK);
      this.splitContainer1.Panel2.Controls.Add(this.btnCancel);
      this.splitContainer1.Panel2MinSize = 35;
      this.splitContainer1.Size = new System.Drawing.Size(502, 431);
      this.splitContainer1.SplitterDistance = 392;
      this.splitContainer1.TabIndex = 16;
      // 
      // btnNumbersStyle
      // 
      this.btnNumbersStyle.Image = global::Ogama.Properties.Resources.Color_fontHS;
      this.btnNumbersStyle.Location = new System.Drawing.Point(7, 65);
      this.btnNumbersStyle.Name = "btnNumbersStyle";
      this.btnNumbersStyle.Size = new System.Drawing.Size(122, 23);
      this.btnNumbersStyle.TabIndex = 0;
      this.btnNumbersStyle.Text = "Numbers style";
      this.btnNumbersStyle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.btnNumbersStyle.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.btnNumbersStyle.UseVisualStyleBackColor = true;
      this.btnNumbersStyle.Click += new System.EventHandler(this.btnNumbersStyle_Click);
      // 
      // btnConnectionsStyle
      // 
      this.btnConnectionsStyle.Image = global::Ogama.Properties.Resources.RPLFixConPen;
      this.btnConnectionsStyle.Location = new System.Drawing.Point(7, 36);
      this.btnConnectionsStyle.Name = "btnConnectionsStyle";
      this.btnConnectionsStyle.Size = new System.Drawing.Size(122, 23);
      this.btnConnectionsStyle.TabIndex = 0;
      this.btnConnectionsStyle.Text = "Connections style";
      this.btnConnectionsStyle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.btnConnectionsStyle.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.btnConnectionsStyle.UseVisualStyleBackColor = true;
      this.btnConnectionsStyle.Click += new System.EventHandler(this.btnConnectionsStyle_Click);
      // 
      // btnFixationsStyle
      // 
      this.btnFixationsStyle.Image = global::Ogama.Properties.Resources.RPLFixPen;
      this.btnFixationsStyle.Location = new System.Drawing.Point(7, 7);
      this.btnFixationsStyle.Name = "btnFixationsStyle";
      this.btnFixationsStyle.Size = new System.Drawing.Size(122, 23);
      this.btnFixationsStyle.TabIndex = 0;
      this.btnFixationsStyle.Text = "Fixations style";
      this.btnFixationsStyle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.btnFixationsStyle.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.btnFixationsStyle.UseVisualStyleBackColor = true;
      this.btnFixationsStyle.Click += new System.EventHandler(this.btnFixationsStyle_Click);
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.pnlPreview);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel1.Location = new System.Drawing.Point(0, 0);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(362, 94);
      this.panel1.TabIndex = 0;
      // 
      // pnlPreview
      // 
      this.pnlPreview.BackColor = System.Drawing.Color.White;
      this.pnlPreview.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnlPreview.Location = new System.Drawing.Point(0, 0);
      this.pnlPreview.Name = "pnlPreview";
      this.pnlPreview.Size = new System.Drawing.Size(362, 90);
      this.pnlPreview.TabIndex = 0;
      this.pnlPreview.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlPreview_Paint);
      // 
      // splitContainer3
      // 
      this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
      this.splitContainer3.IsSplitterFixed = true;
      this.splitContainer3.Location = new System.Drawing.Point(0, 0);
      this.splitContainer3.Name = "splitContainer3";
      this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // splitContainer3.Panel1
      // 
      this.splitContainer3.Panel1.Controls.Add(this.rdbColorSubjects);
      this.splitContainer3.Panel1.Controls.Add(this.cbbPredefinedGradient);
      this.splitContainer3.Panel1.Controls.Add(this.rdbColorGroups);
      this.splitContainer3.Panel1.Controls.Add(this.label3);
      this.splitContainer3.Panel1.Controls.Add(this.rdbColorAutomatic);
      this.splitContainer3.Panel1.Controls.Add(this.label2);
      this.splitContainer3.Panel1.Controls.Add(this.label1);
      // 
      // splitContainer3.Panel2
      // 
      this.splitContainer3.Panel2.Controls.Add(this.spcGradientSingle);
      this.splitContainer3.Size = new System.Drawing.Size(502, 392);
      this.splitContainer3.SplitterDistance = 150;
      this.splitContainer3.TabIndex = 0;
      // 
      // rdbColorSubjects
      // 
      this.rdbColorSubjects.AutoSize = true;
      this.rdbColorSubjects.Location = new System.Drawing.Point(6, 3);
      this.rdbColorSubjects.Name = "rdbColorSubjects";
      this.rdbColorSubjects.Size = new System.Drawing.Size(219, 17);
      this.rdbColorSubjects.TabIndex = 0;
      this.rdbColorSubjects.Text = "Specify color for each subject separately.";
      this.rdbColorSubjects.UseVisualStyleBackColor = true;
      this.rdbColorSubjects.CheckedChanged += new System.EventHandler(this.rdbColorization_CheckedChanged);
      // 
      // cbbPredefinedGradient
      // 
      this.cbbPredefinedGradient.FormattingEnabled = true;
      this.cbbPredefinedGradient.Items.AddRange(new object[] {
            "Custom",
            "Traffic Light",
            "Rainbow"});
      this.cbbPredefinedGradient.Location = new System.Drawing.Point(272, 86);
      this.cbbPredefinedGradient.Name = "cbbPredefinedGradient";
      this.cbbPredefinedGradient.Size = new System.Drawing.Size(87, 21);
      this.cbbPredefinedGradient.TabIndex = 4;
      this.cbbPredefinedGradient.Text = "Custom";
      this.cbbPredefinedGradient.SelectionChangeCommitted += new System.EventHandler(this.cbbPredefinedGradient_SelectionChangeCommitted);
      // 
      // rdbColorGroups
      // 
      this.rdbColorGroups.AutoSize = true;
      this.rdbColorGroups.Location = new System.Drawing.Point(6, 46);
      this.rdbColorGroups.Name = "rdbColorGroups";
      this.rdbColorGroups.Size = new System.Drawing.Size(258, 17);
      this.rdbColorGroups.TabIndex = 0;
      this.rdbColorGroups.Text = "Specify a color for each subject group separately.";
      this.rdbColorGroups.UseVisualStyleBackColor = true;
      this.rdbColorGroups.CheckedChanged += new System.EventHandler(this.rdbColorization_CheckedChanged);
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(36, 107);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(357, 13);
      this.label3.TabIndex = 2;
      this.label3.Text = "Please define the gradient below or select one of the predefined gradients.";
      // 
      // rdbColorAutomatic
      // 
      this.rdbColorAutomatic.AutoSize = true;
      this.rdbColorAutomatic.Checked = true;
      this.rdbColorAutomatic.Location = new System.Drawing.Point(6, 87);
      this.rdbColorAutomatic.Name = "rdbColorAutomatic";
      this.rdbColorAutomatic.Size = new System.Drawing.Size(243, 17);
      this.rdbColorAutomatic.TabIndex = 0;
      this.rdbColorAutomatic.TabStop = true;
      this.rdbColorAutomatic.Text = "Automatically colorize subjects with a gradient.";
      this.rdbColorAutomatic.UseVisualStyleBackColor = true;
      this.rdbColorAutomatic.CheckedChanged += new System.EventHandler(this.rdbColorization_CheckedChanged);
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(36, 66);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(425, 13);
      this.label2.TabIndex = 2;
      this.label2.Text = "Please select a group and modify the color of its members by clicking on the colo" +
          "r button.";
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(36, 23);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(348, 13);
      this.label1.TabIndex = 2;
      this.label1.Text = "Please select subject and modify its color by clicking on the color button.";
      // 
      // spcGradientSingle
      // 
      this.spcGradientSingle.Dock = System.Windows.Forms.DockStyle.Fill;
      this.spcGradientSingle.Location = new System.Drawing.Point(0, 0);
      this.spcGradientSingle.Name = "spcGradientSingle";
      this.spcGradientSingle.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // spcGradientSingle.Panel1
      // 
      this.spcGradientSingle.Panel1.Controls.Add(this.gradientControl);
      // 
      // spcGradientSingle.Panel2
      // 
      this.spcGradientSingle.Panel2.Controls.Add(this.grpPreview);
      this.spcGradientSingle.Size = new System.Drawing.Size(502, 238);
      this.spcGradientSingle.SplitterDistance = 121;
      this.spcGradientSingle.TabIndex = 0;
      // 
      // gradientControl
      // 
      colorBlend1.Colors = new System.Drawing.Color[] {
        System.Drawing.Color.Transparent,
        System.Drawing.Color.Transparent};
      colorBlend1.Positions = new float[] {
        0F,
        1F};
      gradient1.ColorBlend = colorBlend1;
      gradient1.GradientDirection = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
      this.gradientControl.Gradient = gradient1;
      this.gradientControl.Location = new System.Drawing.Point(0, 0);
      this.gradientControl.Name = "gradientControl";
      this.gradientControl.Size = new System.Drawing.Size(502, 102);
      this.gradientControl.TabIndex = 0;
      this.gradientControl.GradientChanged += new System.EventHandler(this.gradientControl_GradientChanged);
      // 
      // grpPreview
      // 
      this.grpPreview.Controls.Add(this.spcPreview);
      this.grpPreview.Dock = System.Windows.Forms.DockStyle.Fill;
      this.grpPreview.Location = new System.Drawing.Point(0, 0);
      this.grpPreview.Name = "grpPreview";
      this.grpPreview.Size = new System.Drawing.Size(502, 113);
      this.grpPreview.TabIndex = 15;
      this.grpPreview.TabStop = false;
      this.grpPreview.Text = "Preview and modify custom style ...";
      // 
      // spcPreview
      // 
      this.spcPreview.Dock = System.Windows.Forms.DockStyle.Fill;
      this.spcPreview.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
      this.spcPreview.IsSplitterFixed = true;
      this.spcPreview.Location = new System.Drawing.Point(3, 16);
      this.spcPreview.Name = "spcPreview";
      // 
      // spcPreview.Panel1
      // 
      this.spcPreview.Panel1.Controls.Add(this.btnConnectionsStyle);
      this.spcPreview.Panel1.Controls.Add(this.btnFixationsStyle);
      this.spcPreview.Panel1.Controls.Add(this.btnNumbersStyle);
      // 
      // spcPreview.Panel2
      // 
      this.spcPreview.Panel2.Controls.Add(this.panel1);
      this.spcPreview.Size = new System.Drawing.Size(496, 94);
      this.spcPreview.SplitterDistance = 130;
      this.spcPreview.TabIndex = 1;
      // 
      // btnOK
      // 
      this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.btnOK.Location = new System.Drawing.Point(343, 3);
      this.btnOK.Name = "btnOK";
      this.btnOK.Size = new System.Drawing.Size(75, 23);
      this.btnOK.TabIndex = 3;
      this.btnOK.Text = "&OK";
      this.btnOK.UseVisualStyleBackColor = true;
      // 
      // btnCancel
      // 
      this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnCancel.Location = new System.Drawing.Point(424, 4);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(75, 23);
      this.btnCancel.TabIndex = 3;
      this.btnCancel.Text = "&Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      // 
      // dialogTop1
      // 
      this.dialogTop1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(237)))));
      this.dialogTop1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("dialogTop1.BackgroundImage")));
      this.dialogTop1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
      this.dialogTop1.Description = "Please specify how to colorize the subjects scanpaths.";
      this.dialogTop1.Dock = System.Windows.Forms.DockStyle.Top;
      this.dialogTop1.Location = new System.Drawing.Point(0, 0);
      this.dialogTop1.Logo = global::Ogama.Properties.Resources.DisplayInColorHS;
      this.dialogTop1.Name = "dialogTop1";
      this.dialogTop1.Size = new System.Drawing.Size(719, 60);
      this.dialogTop1.TabIndex = 2;
      // 
      // ColorDefinitionDialog
      // 
      this.AcceptButton = this.btnOK;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.btnCancel;
      this.ClientSize = new System.Drawing.Size(719, 491);
      this.Controls.Add(this.splitContainer2);
      this.Controls.Add(this.dialogTop1);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "ColorDefinitionDialog";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Define color range for the subjects.";
      this.splitContainer2.Panel1.ResumeLayout(false);
      this.splitContainer2.Panel2.ResumeLayout(false);
      this.splitContainer2.ResumeLayout(false);
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel2.ResumeLayout(false);
      this.splitContainer1.ResumeLayout(false);
      this.panel1.ResumeLayout(false);
      this.splitContainer3.Panel1.ResumeLayout(false);
      this.splitContainer3.Panel1.PerformLayout();
      this.splitContainer3.Panel2.ResumeLayout(false);
      this.splitContainer3.ResumeLayout(false);
      this.spcGradientSingle.Panel1.ResumeLayout(false);
      this.spcGradientSingle.Panel2.ResumeLayout(false);
      this.spcGradientSingle.ResumeLayout(false);
      this.grpPreview.ResumeLayout(false);
      this.spcPreview.Panel1.ResumeLayout(false);
      this.spcPreview.Panel2.ResumeLayout(false);
      this.spcPreview.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private OgamaControls.GradientTypeEditorUI gradientControl;
    private System.Windows.Forms.SplitContainer splitContainer2;
    private System.Windows.Forms.TreeView trvSubjects;
    private System.Windows.Forms.RadioButton rdbColorAutomatic;
    private System.Windows.Forms.RadioButton rdbColorGroups;
    private System.Windows.Forms.RadioButton rdbColorSubjects;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.Button btnOK;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.ComboBox cbbPredefinedGradient;
    private System.Windows.Forms.GroupBox grpPreview;
    private DialogTop dialogTop1;
    private System.Windows.Forms.SplitContainer spcPreview;
    private System.Windows.Forms.Button btnNumbersStyle;
    private System.Windows.Forms.Button btnConnectionsStyle;
    private System.Windows.Forms.Button btnFixationsStyle;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.Panel pnlPreview;
    private System.Windows.Forms.SplitContainer splitContainer1;
    private System.Windows.Forms.SplitContainer splitContainer3;
    private System.Windows.Forms.SplitContainer spcGradientSingle;
  }
}