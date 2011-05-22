namespace OgamaControls
{
  partial class CursorSelectControl
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
      this.pnlPreview = new System.Windows.Forms.Panel();
      this.label2 = new System.Windows.Forms.Label();
      this.cbbCursorType = new System.Windows.Forms.ComboBox();
      this.nudCursorSize = new System.Windows.Forms.NumericUpDown();
      this.rdbDefaultSize = new System.Windows.Forms.RadioButton();
      this.rdbCustomSize = new System.Windows.Forms.RadioButton();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      ((System.ComponentModel.ISupportInitialize)(this.nudCursorSize)).BeginInit();
      this.groupBox1.SuspendLayout();
      this.SuspendLayout();
      // 
      // pnlPreview
      // 
      this.pnlPreview.BackColor = System.Drawing.Color.Wheat;
      this.pnlPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.pnlPreview.Location = new System.Drawing.Point(140, 7);
      this.pnlPreview.Margin = new System.Windows.Forms.Padding(5);
      this.pnlPreview.Name = "pnlPreview";
      this.pnlPreview.Size = new System.Drawing.Size(110, 110);
      this.pnlPreview.TabIndex = 8;
      this.pnlPreview.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlPreview_Paint);
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(5, 16);
      this.label2.Margin = new System.Windows.Forms.Padding(0);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(63, 13);
      this.label2.TabIndex = 7;
      this.label2.Text = "Cursor Style";
      // 
      // cbbCursorType
      // 
      this.cbbCursorType.FormattingEnabled = true;
      this.cbbCursorType.Location = new System.Drawing.Point(67, 13);
      this.cbbCursorType.Name = "cbbCursorType";
      this.cbbCursorType.Size = new System.Drawing.Size(71, 21);
      this.cbbCursorType.TabIndex = 9;
      this.cbbCursorType.Text = "Circle";
      this.cbbCursorType.SelectionChangeCommitted += new System.EventHandler(this.cbbCursorType_SelectionChangeCommitted);
      // 
      // nudCursorSize
      // 
      this.nudCursorSize.Location = new System.Drawing.Point(71, 42);
      this.nudCursorSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nudCursorSize.Name = "nudCursorSize";
      this.nudCursorSize.Size = new System.Drawing.Size(53, 20);
      this.nudCursorSize.TabIndex = 10;
      this.nudCursorSize.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
      this.nudCursorSize.ValueChanged += new System.EventHandler(this.nudCursorSize_ValueChanged);
      // 
      // rdbDefaultSize
      // 
      this.rdbDefaultSize.AutoSize = true;
      this.rdbDefaultSize.Checked = true;
      this.rdbDefaultSize.Location = new System.Drawing.Point(6, 19);
      this.rdbDefaultSize.Name = "rdbDefaultSize";
      this.rdbDefaultSize.Size = new System.Drawing.Size(59, 17);
      this.rdbDefaultSize.TabIndex = 11;
      this.rdbDefaultSize.TabStop = true;
      this.rdbDefaultSize.Text = "Default";
      this.rdbDefaultSize.UseVisualStyleBackColor = true;
      this.rdbDefaultSize.CheckedChanged += new System.EventHandler(this.rdbDefaultSize_CheckedChanged);
      // 
      // rdbCustomSize
      // 
      this.rdbCustomSize.AutoSize = true;
      this.rdbCustomSize.Location = new System.Drawing.Point(6, 42);
      this.rdbCustomSize.Name = "rdbCustomSize";
      this.rdbCustomSize.Size = new System.Drawing.Size(60, 17);
      this.rdbCustomSize.TabIndex = 11;
      this.rdbCustomSize.Text = "Custom";
      this.rdbCustomSize.UseVisualStyleBackColor = true;
      this.rdbCustomSize.CheckedChanged += new System.EventHandler(this.rdbCustomSize_CheckedChanged);
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.rdbDefaultSize);
      this.groupBox1.Controls.Add(this.nudCursorSize);
      this.groupBox1.Controls.Add(this.rdbCustomSize);
      this.groupBox1.Location = new System.Drawing.Point(8, 39);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(130, 78);
      this.groupBox1.TabIndex = 12;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Size";
      // 
      // CursorSelectControl
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.groupBox1);
      this.Controls.Add(this.cbbCursorType);
      this.Controls.Add(this.pnlPreview);
      this.Controls.Add(this.label2);
      this.Name = "CursorSelectControl";
      this.Size = new System.Drawing.Size(259, 126);
      this.Load += new System.EventHandler(this.CursorSelectControl_Load);
      ((System.ComponentModel.ISupportInitialize)(this.nudCursorSize)).EndInit();
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Panel pnlPreview;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.ComboBox cbbCursorType;
    private System.Windows.Forms.NumericUpDown nudCursorSize;
    private System.Windows.Forms.RadioButton rdbDefaultSize;
    private System.Windows.Forms.RadioButton rdbCustomSize;
    private System.Windows.Forms.GroupBox groupBox1;

  }
}
