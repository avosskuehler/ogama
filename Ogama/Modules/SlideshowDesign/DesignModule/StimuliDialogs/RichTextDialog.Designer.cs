namespace Ogama.Modules.SlideshowDesign.DesignModule.StimuliDialogs
{
  using Ogama.Modules.Common.Controls;

  using VectorGraphics.Tools.CustomEventArgs;

  partial class RichTextDialog
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RichTextDialog));
      this.btnOK = new System.Windows.Forms.Button();
      this.btnCancel = new System.Windows.Forms.Button();
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      this.splitContainer2 = new System.Windows.Forms.SplitContainer();
      this.rtbInstruction = new OgamaControls.RTBTextControl();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.audioControl = new OgamaControls.AudioControl();
      this.grpBorder = new System.Windows.Forms.GroupBox();
      this.pbcBorder = new OgamaControls.PenAndBrushControl();
      this.btnHelp = new System.Windows.Forms.Button();
      this.panel1 = new System.Windows.Forms.Panel();
      this.dialogTop1 = new DialogTop();
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      this.splitContainer2.Panel1.SuspendLayout();
      this.splitContainer2.Panel2.SuspendLayout();
      this.splitContainer2.SuspendLayout();
      this.groupBox1.SuspendLayout();
      this.grpBorder.SuspendLayout();
      this.panel1.SuspendLayout();
      this.SuspendLayout();
      // 
      // btnOK
      // 
      this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.btnOK.Location = new System.Drawing.Point(555, 3);
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
      this.btnCancel.Location = new System.Drawing.Point(632, 3);
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
      this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
      // 
      // splitContainer1.Panel2
      // 
      this.splitContainer1.Panel2.Controls.Add(this.btnHelp);
      this.splitContainer1.Panel2.Controls.Add(this.btnOK);
      this.splitContainer1.Panel2.Controls.Add(this.btnCancel);
      this.splitContainer1.Size = new System.Drawing.Size(700, 383);
      this.splitContainer1.SplitterDistance = 349;
      this.splitContainer1.TabIndex = 23;
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
      this.splitContainer2.Panel1.Controls.Add(this.rtbInstruction);
      // 
      // splitContainer2.Panel2
      // 
      this.splitContainer2.Panel2.Controls.Add(this.groupBox1);
      this.splitContainer2.Panel2.Controls.Add(this.grpBorder);
      this.splitContainer2.Size = new System.Drawing.Size(700, 349);
      this.splitContainer2.SplitterDistance = 220;
      this.splitContainer2.TabIndex = 1;
      // 
      // rtbInstruction
      // 
      this.rtbInstruction.AcceptsTab = false;
      this.rtbInstruction.AutoWordSelection = true;
      this.rtbInstruction.BackColor = System.Drawing.Color.Lavender;
      this.rtbInstruction.BackgroundImage = global::Ogama.Properties.Resources.CheckBoard;
      this.rtbInstruction.DetectURLs = true;
      this.rtbInstruction.Dock = System.Windows.Forms.DockStyle.Fill;
      this.rtbInstruction.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.rtbInstruction.Label = "Instruction";
      this.rtbInstruction.Location = new System.Drawing.Point(0, 0);
      this.rtbInstruction.Margin = new System.Windows.Forms.Padding(10, 9, 10, 9);
      this.rtbInstruction.Name = "rtbInstruction";
      this.rtbInstruction.ReadOnly = false;
      // 
      // 
      // 
      this.rtbInstruction.RichTextBox.AutoWordSelection = true;
      this.rtbInstruction.RichTextBox.BackColor = System.Drawing.SystemColors.Window;
      this.rtbInstruction.RichTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
      this.rtbInstruction.RichTextBox.EnableAutoDragDrop = true;
      this.rtbInstruction.RichTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.rtbInstruction.RichTextBox.Location = new System.Drawing.Point(0, 0);
      this.rtbInstruction.RichTextBox.Margin = new System.Windows.Forms.Padding(10, 9, 10, 9);
      this.rtbInstruction.RichTextBox.Name = "rtb1";
      this.rtbInstruction.RichTextBox.Size = new System.Drawing.Size(700, 195);
      this.rtbInstruction.RichTextBox.TabIndex = 1;
      this.rtbInstruction.ShowColors = true;
      this.rtbInstruction.ShowOpen = true;
      this.rtbInstruction.ShowSave = true;
      this.rtbInstruction.ShowStrikeout = true;
      this.rtbInstruction.ShowToolBarText = false;
      this.rtbInstruction.Size = new System.Drawing.Size(700, 220);
      this.rtbInstruction.TabIndex = 0;
      this.rtbInstruction.ToolbarRenderMode = System.Windows.Forms.ToolStripRenderMode.System;
      // 
      // groupBox1
      // 
      this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.groupBox1.Controls.Add(this.audioControl);
      this.groupBox1.Location = new System.Drawing.Point(442, 3);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(258, 122);
      this.groupBox1.TabIndex = 1;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Audio properties";
      // 
      // audioControl
      // 
      this.audioControl.Dock = System.Windows.Forms.DockStyle.Fill;
      this.audioControl.Location = new System.Drawing.Point(3, 16);
      this.audioControl.Name = "audioControl";
      this.audioControl.ShouldPlay = false;
      this.audioControl.Size = new System.Drawing.Size(252, 103);
      this.audioControl.TabIndex = 0;
      // 
      // grpBorder
      // 
      this.grpBorder.Controls.Add(this.pbcBorder);
      this.grpBorder.Dock = System.Windows.Forms.DockStyle.Left;
      this.grpBorder.Location = new System.Drawing.Point(0, 0);
      this.grpBorder.Name = "grpBorder";
      this.grpBorder.Size = new System.Drawing.Size(259, 125);
      this.grpBorder.TabIndex = 0;
      this.grpBorder.TabStop = false;
      this.grpBorder.Text = "Border and fill style ...";
      // 
      // pbcBorder
      // 
      this.pbcBorder.Dock = System.Windows.Forms.DockStyle.Left;
      this.pbcBorder.DrawAction = VectorGraphics.Elements.ShapeDrawAction.Fill;
      this.pbcBorder.Location = new System.Drawing.Point(3, 16);
      this.pbcBorder.Name = "pbcBorder";
      this.pbcBorder.NewFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.pbcBorder.NewFontColor = System.Drawing.SystemColors.GrayText;
      this.pbcBorder.NewName = "";
      this.pbcBorder.NewTextAlignment = VectorGraphics.Elements.VGAlignment.None;
      this.pbcBorder.Size = new System.Drawing.Size(249, 106);
      this.pbcBorder.TabIndex = 0;
      this.pbcBorder.ShapePropertiesChanged += new System.EventHandler<ShapePropertiesChangedEventArgs>(this.pbcBorder_ShapePropertiesChanged);
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
      this.panel1.Size = new System.Drawing.Size(706, 389);
      this.panel1.TabIndex = 19;
      // 
      // dialogTop1
      // 
      this.dialogTop1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(237)))));
      this.dialogTop1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("dialogTop1.BackgroundImage")));
      this.dialogTop1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
      this.dialogTop1.Description = "Please enter the new instruction, and define its appearance";
      this.dialogTop1.Dock = System.Windows.Forms.DockStyle.Top;
      this.dialogTop1.Location = new System.Drawing.Point(0, 0);
      this.dialogTop1.Logo = global::Ogama.Properties.Resources.textdoc;
      this.dialogTop1.Name = "dialogTop1";
      this.dialogTop1.Size = new System.Drawing.Size(706, 60);
      this.dialogTop1.TabIndex = 22;
      // 
      // RichTextDialog
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.btnCancel;
      this.ClientSize = new System.Drawing.Size(706, 449);
      this.Controls.Add(this.panel1);
      this.Controls.Add(this.dialogTop1);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "RichTextDialog";
      this.ShowIcon = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Add new instruction ...";
      this.Shown += new System.EventHandler(this.RichTextDialog_Shown);
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel2.ResumeLayout(false);
      this.splitContainer1.ResumeLayout(false);
      this.splitContainer2.Panel1.ResumeLayout(false);
      this.splitContainer2.Panel2.ResumeLayout(false);
      this.splitContainer2.ResumeLayout(false);
      this.groupBox1.ResumeLayout(false);
      this.grpBorder.ResumeLayout(false);
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
    private OgamaControls.RTBTextControl rtbInstruction;
    private System.Windows.Forms.SplitContainer splitContainer2;
    private System.Windows.Forms.GroupBox grpBorder;
    private OgamaControls.PenAndBrushControl pbcBorder;
    private System.Windows.Forms.GroupBox groupBox1;
    private OgamaControls.AudioControl audioControl;
  }
}