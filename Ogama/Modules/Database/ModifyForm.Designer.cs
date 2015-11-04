namespace Ogama.Modules.Database
{
  partial class ModifyForm
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
      this.numericDropdownPosx = new System.Windows.Forms.NumericUpDown();
      this.label1 = new System.Windows.Forms.Label();
      this.button1 = new System.Windows.Forms.Button();
      this.label2 = new System.Windows.Forms.Label();
      this.numericUpDownPoxY = new System.Windows.Forms.NumericUpDown();
      ((System.ComponentModel.ISupportInitialize)(this.numericDropdownPosx)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPoxY)).BeginInit();
      this.SuspendLayout();
      // 
      // numericDropdownPosx
      // 
      this.numericDropdownPosx.Location = new System.Drawing.Point(113, 7);
      this.numericDropdownPosx.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
      this.numericDropdownPosx.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
      this.numericDropdownPosx.Name = "numericDropdownPosx";
      this.numericDropdownPosx.Size = new System.Drawing.Size(120, 20);
      this.numericDropdownPosx.TabIndex = 0;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(12, 9);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(78, 13);
      this.label1.TabIndex = 1;
      this.label1.Text = "Gazeposition-X";
      // 
      // button1
      // 
      this.button1.Location = new System.Drawing.Point(272, 57);
      this.button1.Name = "button1";
      this.button1.Size = new System.Drawing.Size(75, 23);
      this.button1.TabIndex = 2;
      this.button1.Text = "OK";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button1_MouseDown);
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(12, 48);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(78, 13);
      this.label2.TabIndex = 3;
      this.label2.Text = "Gazeposition-Y";
      // 
      // numericUpDownPoxY
      // 
      this.numericUpDownPoxY.Location = new System.Drawing.Point(113, 41);
      this.numericUpDownPoxY.Name = "numericUpDownPoxY";
      this.numericUpDownPoxY.Size = new System.Drawing.Size(120, 20);
      this.numericUpDownPoxY.TabIndex = 4;
      // 
      // ModifyForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(359, 92);
      this.Controls.Add(this.numericUpDownPoxY);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.button1);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.numericDropdownPosx);
      this.Name = "ModifyForm";
      this.Text = "ModifyForm";
      ((System.ComponentModel.ISupportInitialize)(this.numericDropdownPosx)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPoxY)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.NumericUpDown numericDropdownPosx;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.NumericUpDown numericUpDownPoxY;
  }
}