namespace Ogama.Modules.Scanpaths
{
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
      OgamaControls.Gradient gradient1 = new OgamaControls.Gradient();
      System.Drawing.Drawing2D.ColorBlend colorBlend1 = new System.Drawing.Drawing2D.ColorBlend();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ColorDefinitionDialog));
      this.splitContainer2 = new System.Windows.Forms.SplitContainer();
      this.trvSubjects = new System.Windows.Forms.TreeView();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.fsaNumbers = new OgamaControls.FontStyleArea();
      this.label5 = new System.Windows.Forms.Label();
      this.label4 = new System.Windows.Forms.Label();
      this.psaConnectionPen = new OgamaControls.PenStyleArea();
      this.psaFixationPen = new OgamaControls.PenStyleArea();
      this.cbbPredefinedGradient = new System.Windows.Forms.ComboBox();
      this.btnCancel = new System.Windows.Forms.Button();
      this.btnOK = new System.Windows.Forms.Button();
      this.gradientControl = new OgamaControls.GradientTypeEditorUI();
      this.label3 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.label1 = new System.Windows.Forms.Label();
      this.rdbColorAutomatic = new System.Windows.Forms.RadioButton();
      this.rdbColorGroups = new System.Windows.Forms.RadioButton();
      this.rdbColorSubjects = new System.Windows.Forms.RadioButton();
      this.dialogTop1 = new Ogama.Modules.Common.DialogTop();
      this.splitContainer2.Panel1.SuspendLayout();
      this.splitContainer2.Panel2.SuspendLayout();
      this.splitContainer2.SuspendLayout();
      this.groupBox1.SuspendLayout();
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
      this.splitContainer2.Panel2.Controls.Add(this.groupBox1);
      this.splitContainer2.Panel2.Controls.Add(this.cbbPredefinedGradient);
      this.splitContainer2.Panel2.Controls.Add(this.btnCancel);
      this.splitContainer2.Panel2.Controls.Add(this.btnOK);
      this.splitContainer2.Panel2.Controls.Add(this.gradientControl);
      this.splitContainer2.Panel2.Controls.Add(this.label3);
      this.splitContainer2.Panel2.Controls.Add(this.label2);
      this.splitContainer2.Panel2.Controls.Add(this.label1);
      this.splitContainer2.Panel2.Controls.Add(this.rdbColorAutomatic);
      this.splitContainer2.Panel2.Controls.Add(this.rdbColorGroups);
      this.splitContainer2.Panel2.Controls.Add(this.rdbColorSubjects);
      this.splitContainer2.Size = new System.Drawing.Size(719, 392);
      this.splitContainer2.SplitterDistance = 213;
      this.splitContainer2.TabIndex = 1;
      // 
      // trvSubjects
      // 
      this.trvSubjects.Dock = System.Windows.Forms.DockStyle.Fill;
      this.trvSubjects.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawText;
      this.trvSubjects.Location = new System.Drawing.Point(0, 0);
      this.trvSubjects.Name = "trvSubjects";
      this.trvSubjects.Size = new System.Drawing.Size(213, 392);
      this.trvSubjects.TabIndex = 1;
      this.trvSubjects.DrawNode += new System.Windows.Forms.DrawTreeNodeEventHandler(this.trvSubjects_DrawNode);
      this.trvSubjects.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.trvSubjects_AfterSelect);
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.fsaNumbers);
      this.groupBox1.Controls.Add(this.label5);
      this.groupBox1.Controls.Add(this.label4);
      this.groupBox1.Controls.Add(this.psaConnectionPen);
      this.groupBox1.Controls.Add(this.psaFixationPen);
      this.groupBox1.Location = new System.Drawing.Point(12, 244);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(487, 99);
      this.groupBox1.TabIndex = 15;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Modify styles (click on preview) ...";
      // 
      // fsaNumbers
      // 
      this.fsaNumbers.BackColor = System.Drawing.Color.WhiteSmoke;
      this.fsaNumbers.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.fsaNumbers.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
      this.fsaNumbers.FontColor = System.Drawing.Color.Black;
      this.fsaNumbers.Location = new System.Drawing.Point(289, 24);
      this.fsaNumbers.Name = "fsaNumbers";
      this.fsaNumbers.SampleString = "Font ...";
      this.fsaNumbers.Size = new System.Drawing.Size(189, 54);
      this.fsaNumbers.TabIndex = 17;
      this.fsaNumbers.Click += new System.EventHandler(this.fsaNumbers_Click);
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(142, 24);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(120, 13);
      this.label5.TabIndex = 16;
      this.label5.Text = "Fixation connection pen";
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(9, 24);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(64, 13);
      this.label4.TabIndex = 16;
      this.label4.Text = "Fixation pen";
      // 
      // psaConnectionPen
      // 
      this.psaConnectionPen.BackColor = System.Drawing.Color.WhiteSmoke;
      this.psaConnectionPen.Location = new System.Drawing.Point(145, 46);
      this.psaConnectionPen.Name = "psaConnectionPen";
      this.psaConnectionPen.PenColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
      this.psaConnectionPen.PenDashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
      this.psaConnectionPen.PenSize = 1F;
      this.psaConnectionPen.Size = new System.Drawing.Size(119, 32);
      this.psaConnectionPen.TabIndex = 15;
      this.psaConnectionPen.Click += new System.EventHandler(this.psaConnectionPen_Click);
      // 
      // psaFixationPen
      // 
      this.psaFixationPen.BackColor = System.Drawing.Color.WhiteSmoke;
      this.psaFixationPen.Location = new System.Drawing.Point(12, 46);
      this.psaFixationPen.Name = "psaFixationPen";
      this.psaFixationPen.PenColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
      this.psaFixationPen.PenDashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
      this.psaFixationPen.PenSize = 1F;
      this.psaFixationPen.Size = new System.Drawing.Size(119, 32);
      this.psaFixationPen.TabIndex = 15;
      this.psaFixationPen.Click += new System.EventHandler(this.psaFixationPen_Click);
      // 
      // cbbPredefinedGradient
      // 
      this.cbbPredefinedGradient.FormattingEnabled = true;
      this.cbbPredefinedGradient.Items.AddRange(new object[] {
            "Custom",
            "Traffic Light",
            "Rainbow"});
      this.cbbPredefinedGradient.Location = new System.Drawing.Point(278, 95);
      this.cbbPredefinedGradient.Name = "cbbPredefinedGradient";
      this.cbbPredefinedGradient.Size = new System.Drawing.Size(87, 21);
      this.cbbPredefinedGradient.TabIndex = 4;
      this.cbbPredefinedGradient.Text = "Custom";
      this.cbbPredefinedGradient.SelectionChangeCommitted += new System.EventHandler(this.cbbPredefinedGradient_SelectionChangeCommitted);
      // 
      // btnCancel
      // 
      this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnCancel.Location = new System.Drawing.Point(424, 366);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(75, 23);
      this.btnCancel.TabIndex = 3;
      this.btnCancel.Text = "&Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      // 
      // btnOK
      // 
      this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.btnOK.Location = new System.Drawing.Point(343, 366);
      this.btnOK.Name = "btnOK";
      this.btnOK.Size = new System.Drawing.Size(75, 23);
      this.btnOK.TabIndex = 3;
      this.btnOK.Text = "&OK";
      this.btnOK.UseVisualStyleBackColor = true;
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
      this.gradientControl.Location = new System.Drawing.Point(28, 140);
      this.gradientControl.Name = "gradientControl";
      this.gradientControl.Size = new System.Drawing.Size(471, 76);
      this.gradientControl.TabIndex = 0;
      this.gradientControl.GradientChanged += new System.EventHandler(this.gradientControl_GradientChanged);
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(42, 116);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(357, 13);
      this.label3.TabIndex = 2;
      this.label3.Text = "Please define the gradient below or select one of the predefined gradients.";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(42, 75);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(425, 13);
      this.label2.TabIndex = 2;
      this.label2.Text = "Please select a group and modify the color of its members by clicking on the colo" +
          "r button.";
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(42, 32);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(348, 13);
      this.label1.TabIndex = 2;
      this.label1.Text = "Please select subject and modify its color by clicking on the color button.";
      // 
      // rdbColorAutomatic
      // 
      this.rdbColorAutomatic.AutoSize = true;
      this.rdbColorAutomatic.Checked = true;
      this.rdbColorAutomatic.Location = new System.Drawing.Point(12, 96);
      this.rdbColorAutomatic.Name = "rdbColorAutomatic";
      this.rdbColorAutomatic.Size = new System.Drawing.Size(243, 17);
      this.rdbColorAutomatic.TabIndex = 0;
      this.rdbColorAutomatic.TabStop = true;
      this.rdbColorAutomatic.Text = "Automatically colorize subjects with a gradient.";
      this.rdbColorAutomatic.UseVisualStyleBackColor = true;
      this.rdbColorAutomatic.CheckedChanged += new System.EventHandler(this.rdbColorization_CheckedChanged);
      // 
      // rdbColorGroups
      // 
      this.rdbColorGroups.AutoSize = true;
      this.rdbColorGroups.Location = new System.Drawing.Point(12, 55);
      this.rdbColorGroups.Name = "rdbColorGroups";
      this.rdbColorGroups.Size = new System.Drawing.Size(258, 17);
      this.rdbColorGroups.TabIndex = 0;
      this.rdbColorGroups.Text = "Specify a color for each subject group separately.";
      this.rdbColorGroups.UseVisualStyleBackColor = true;
      this.rdbColorGroups.CheckedChanged += new System.EventHandler(this.rdbColorization_CheckedChanged);
      // 
      // rdbColorSubjects
      // 
      this.rdbColorSubjects.AutoSize = true;
      this.rdbColorSubjects.Location = new System.Drawing.Point(12, 12);
      this.rdbColorSubjects.Name = "rdbColorSubjects";
      this.rdbColorSubjects.Size = new System.Drawing.Size(219, 17);
      this.rdbColorSubjects.TabIndex = 0;
      this.rdbColorSubjects.Text = "Specify color for each subject separately.";
      this.rdbColorSubjects.UseVisualStyleBackColor = true;
      this.rdbColorSubjects.CheckedChanged += new System.EventHandler(this.rdbColorization_CheckedChanged);
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
      this.ClientSize = new System.Drawing.Size(719, 452);
      this.Controls.Add(this.splitContainer2);
      this.Controls.Add(this.dialogTop1);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "ColorDefinitionDialog";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Define color range for the subjects.";
      this.splitContainer2.Panel1.ResumeLayout(false);
      this.splitContainer2.Panel2.ResumeLayout(false);
      this.splitContainer2.Panel2.PerformLayout();
      this.splitContainer2.ResumeLayout(false);
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
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
    private System.Windows.Forms.GroupBox groupBox1;
    private OgamaControls.PenStyleArea psaFixationPen;
    private OgamaControls.FontStyleArea fsaNumbers;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.Label label4;
    private OgamaControls.PenStyleArea psaConnectionPen;
    private Ogama.Modules.Common.DialogTop dialogTop1;
  }
}