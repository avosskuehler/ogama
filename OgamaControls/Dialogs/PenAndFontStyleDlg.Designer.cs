namespace OgamaControls.Dialogs
{
  using VectorGraphics.Tools.CustomEventArgs;

  partial class PenAndFontStyleDlg
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
      this.penSelectControl = new OgamaControls.PenSelectControl();
      this.fontSelectControl = new OgamaControls.FontSelectControl();
      this.btnCancel = new System.Windows.Forms.Button();
      this.btnOK = new System.Windows.Forms.Button();
      this.grbPen = new System.Windows.Forms.GroupBox();
      this.grpFont = new System.Windows.Forms.GroupBox();
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      this.splitContainer2 = new System.Windows.Forms.SplitContainer();
      this.grbPen.SuspendLayout();
      this.grpFont.SuspendLayout();
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      this.splitContainer2.Panel1.SuspendLayout();
      this.splitContainer2.Panel2.SuspendLayout();
      this.splitContainer2.SuspendLayout();
      this.SuspendLayout();
      // 
      // penSelectControl
      // 
      this.penSelectControl.AccessibleDescription = "Select a color to use";
      this.penSelectControl.AccessibleName = "Select Color";
      this.penSelectControl.Dock = System.Windows.Forms.DockStyle.Fill;
      this.penSelectControl.Location = new System.Drawing.Point(3, 16);
      this.penSelectControl.Margin = new System.Windows.Forms.Padding(5);
      this.penSelectControl.Name = "penSelectControl";
      this.penSelectControl.Padding = new System.Windows.Forms.Padding(3);
      this.penSelectControl.Size = new System.Drawing.Size(294, 254);
      this.penSelectControl.TabIndex = 1;
      this.penSelectControl.PenChanged += new System.EventHandler<PenChangedEventArgs>(this.penSelectControl_PenChanged);
      // 
      // fontSelectControl
      // 
      this.fontSelectControl.Dock = System.Windows.Forms.DockStyle.Fill;
      this.fontSelectControl.Location = new System.Drawing.Point(3, 16);
      this.fontSelectControl.Name = "fontSelectControl";
      this.fontSelectControl.Padding = new System.Windows.Forms.Padding(3);
      this.fontSelectControl.Size = new System.Drawing.Size(294, 254);
      this.fontSelectControl.TabIndex = 0;
      this.fontSelectControl.FontStyleChanged += new System.EventHandler<FontChangedEventArgs>(this.fontSelectControl_FontStyleChanged);
      // 
      // btnCancel
      // 
      this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnCancel.Location = new System.Drawing.Point(523, 13);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(68, 25);
      this.btnCancel.TabIndex = 5;
      this.btnCancel.Text = "&Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      // 
      // btnOK
      // 
      this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.btnOK.Location = new System.Drawing.Point(449, 13);
      this.btnOK.Name = "btnOK";
      this.btnOK.Size = new System.Drawing.Size(68, 25);
      this.btnOK.TabIndex = 4;
      this.btnOK.Text = "&OK";
      this.btnOK.UseVisualStyleBackColor = true;
      // 
      // grbPen
      // 
      this.grbPen.Controls.Add(this.penSelectControl);
      this.grbPen.Dock = System.Windows.Forms.DockStyle.Fill;
      this.grbPen.Location = new System.Drawing.Point(0, 0);
      this.grbPen.Name = "grbPen";
      this.grbPen.Size = new System.Drawing.Size(300, 273);
      this.grbPen.TabIndex = 6;
      this.grbPen.TabStop = false;
      this.grbPen.Text = "Select pen style and color ...";
      // 
      // grpFont
      // 
      this.grpFont.Controls.Add(this.fontSelectControl);
      this.grpFont.Dock = System.Windows.Forms.DockStyle.Fill;
      this.grpFont.Location = new System.Drawing.Point(0, 0);
      this.grpFont.Name = "grpFont";
      this.grpFont.Size = new System.Drawing.Size(300, 273);
      this.grpFont.TabIndex = 7;
      this.grpFont.TabStop = false;
      this.grpFont.Text = "Select font style and color ...";
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
      this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
      // 
      // splitContainer1.Panel2
      // 
      this.splitContainer1.Panel2.Controls.Add(this.btnOK);
      this.splitContainer1.Panel2.Controls.Add(this.btnCancel);
      this.splitContainer1.Panel2MinSize = 50;
      this.splitContainer1.Size = new System.Drawing.Size(603, 327);
      this.splitContainer1.SplitterDistance = 273;
      this.splitContainer1.TabIndex = 8;
      // 
      // splitContainer2
      // 
      this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer2.Location = new System.Drawing.Point(0, 0);
      this.splitContainer2.Name = "splitContainer2";
      // 
      // splitContainer2.Panel1
      // 
      this.splitContainer2.Panel1.Controls.Add(this.grpFont);
      this.splitContainer2.Panel1MinSize = 300;
      // 
      // splitContainer2.Panel2
      // 
      this.splitContainer2.Panel2.Controls.Add(this.grbPen);
      this.splitContainer2.Size = new System.Drawing.Size(603, 273);
      this.splitContainer2.SplitterDistance = 300;
      this.splitContainer2.TabIndex = 0;
      // 
      // PenAndFontStyleDlg
      // 
      this.AcceptButton = this.btnOK;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.btnCancel;
      this.ClientSize = new System.Drawing.Size(603, 327);
      this.Controls.Add(this.splitContainer1);
      this.DoubleBuffered = true;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "PenAndFontStyleDlg";
      this.Text = "Select Pen and Font properties";
      this.grbPen.ResumeLayout(false);
      this.grpFont.ResumeLayout(false);
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel2.ResumeLayout(false);
      this.splitContainer1.ResumeLayout(false);
      this.splitContainer2.Panel1.ResumeLayout(false);
      this.splitContainer2.Panel2.ResumeLayout(false);
      this.splitContainer2.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private FontSelectControl fontSelectControl;
    private PenSelectControl penSelectControl;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.Button btnOK;
    private System.Windows.Forms.GroupBox grbPen;
    private System.Windows.Forms.GroupBox grpFont;
    private System.Windows.Forms.SplitContainer splitContainer1;
    private System.Windows.Forms.SplitContainer splitContainer2;

  }
}