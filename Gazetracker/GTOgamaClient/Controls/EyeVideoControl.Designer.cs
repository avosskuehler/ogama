namespace GTOgamaClient.Controls
{
  partial class EyeVideoControl
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
      this.pictureBox = new Emgu.CV.UI.ImageBox();
      this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
      this.lblFPSHeader = new System.Windows.Forms.Label();
      this.lblFPS = new System.Windows.Forms.Label();
      this.lblCPUHeader = new System.Windows.Forms.Label();
      this.lblCPU = new System.Windows.Forms.Label();
      this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
      this.rdbRaw = new System.Windows.Forms.RadioButton();
      this.rdbNormal = new System.Windows.Forms.RadioButton();
      this.rdbProcessed = new System.Windows.Forms.RadioButton();
      this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
      this.tableLayoutPanel1.SuspendLayout();
      this.flowLayoutPanel2.SuspendLayout();
      this.tableLayoutPanel2.SuspendLayout();
      this.SuspendLayout();
      // 
      // pictureBox
      // 
      this.pictureBox.BackColor = System.Drawing.Color.DarkRed;
      this.pictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pictureBox.Location = new System.Drawing.Point(0, 15);
      this.pictureBox.Margin = new System.Windows.Forms.Padding(0);
      this.pictureBox.Name = "pictureBox";
      this.pictureBox.Size = new System.Drawing.Size(150, 120);
      this.pictureBox.TabIndex = 2;
      this.pictureBox.TabStop = false;
      // 
      // tableLayoutPanel1
      // 
      this.tableLayoutPanel1.ColumnCount = 4;
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
      this.tableLayoutPanel1.Controls.Add(this.lblFPSHeader, 0, 0);
      this.tableLayoutPanel1.Controls.Add(this.lblFPS, 1, 0);
      this.tableLayoutPanel1.Controls.Add(this.lblCPUHeader, 2, 0);
      this.tableLayoutPanel1.Controls.Add(this.lblCPU, 3, 0);
      this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
      this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
      this.tableLayoutPanel1.Name = "tableLayoutPanel1";
      this.tableLayoutPanel1.RowCount = 1;
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel1.Size = new System.Drawing.Size(150, 15);
      this.tableLayoutPanel1.TabIndex = 3;
      // 
      // lblFPSHeader
      // 
      this.lblFPSHeader.AutoSize = true;
      this.lblFPSHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblFPSHeader.Location = new System.Drawing.Point(0, 3);
      this.lblFPSHeader.Margin = new System.Windows.Forms.Padding(0, 3, 0, 0);
      this.lblFPSHeader.Name = "lblFPSHeader";
      this.lblFPSHeader.Size = new System.Drawing.Size(73, 9);
      this.lblFPSHeader.TabIndex = 0;
      this.lblFPSHeader.Text = "FPS Video/Tracking";
      // 
      // lblFPS
      // 
      this.lblFPS.AutoSize = true;
      this.lblFPS.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblFPS.Location = new System.Drawing.Point(73, 0);
      this.lblFPS.Margin = new System.Windows.Forms.Padding(0);
      this.lblFPS.Name = "lblFPS";
      this.lblFPS.Size = new System.Drawing.Size(13, 13);
      this.lblFPS.TabIndex = 1;
      this.lblFPS.Text = "0";
      // 
      // lblCPUHeader
      // 
      this.lblCPUHeader.AutoSize = true;
      this.lblCPUHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblCPUHeader.Location = new System.Drawing.Point(86, 3);
      this.lblCPUHeader.Margin = new System.Windows.Forms.Padding(0, 3, 0, 0);
      this.lblCPUHeader.Name = "lblCPUHeader";
      this.lblCPUHeader.Size = new System.Drawing.Size(22, 9);
      this.lblCPUHeader.TabIndex = 2;
      this.lblCPUHeader.Text = "CPU";
      // 
      // lblCPU
      // 
      this.lblCPU.AutoSize = true;
      this.lblCPU.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblCPU.Location = new System.Drawing.Point(108, 0);
      this.lblCPU.Margin = new System.Windows.Forms.Padding(0);
      this.lblCPU.Name = "lblCPU";
      this.lblCPU.Size = new System.Drawing.Size(13, 13);
      this.lblCPU.TabIndex = 1;
      this.lblCPU.Text = "0";
      // 
      // flowLayoutPanel2
      // 
      this.flowLayoutPanel2.Controls.Add(this.rdbRaw);
      this.flowLayoutPanel2.Controls.Add(this.rdbNormal);
      this.flowLayoutPanel2.Controls.Add(this.rdbProcessed);
      this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.flowLayoutPanel2.Location = new System.Drawing.Point(0, 135);
      this.flowLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
      this.flowLayoutPanel2.MaximumSize = new System.Drawing.Size(0, 15);
      this.flowLayoutPanel2.Name = "flowLayoutPanel2";
      this.flowLayoutPanel2.Size = new System.Drawing.Size(150, 15);
      this.flowLayoutPanel2.TabIndex = 1;
      // 
      // rdbRaw
      // 
      this.rdbRaw.AutoSize = true;
      this.rdbRaw.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.rdbRaw.Location = new System.Drawing.Point(0, 0);
      this.rdbRaw.Margin = new System.Windows.Forms.Padding(0);
      this.rdbRaw.Name = "rdbRaw";
      this.rdbRaw.Size = new System.Drawing.Size(39, 13);
      this.rdbRaw.TabIndex = 0;
      this.rdbRaw.TabStop = true;
      this.rdbRaw.Text = "Raw";
      this.rdbRaw.UseVisualStyleBackColor = true;
      this.rdbRaw.CheckedChanged += new System.EventHandler(this.RdbModeCheckedChanged);
      // 
      // rdbNormal
      // 
      this.rdbNormal.AutoSize = true;
      this.rdbNormal.Checked = true;
      this.rdbNormal.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.rdbNormal.Location = new System.Drawing.Point(39, 0);
      this.rdbNormal.Margin = new System.Windows.Forms.Padding(0);
      this.rdbNormal.Name = "rdbNormal";
      this.rdbNormal.Size = new System.Drawing.Size(49, 13);
      this.rdbNormal.TabIndex = 0;
      this.rdbNormal.TabStop = true;
      this.rdbNormal.Text = "Normal";
      this.rdbNormal.UseVisualStyleBackColor = true;
      this.rdbNormal.CheckedChanged += new System.EventHandler(this.RdbModeCheckedChanged);
      // 
      // rdbProcessed
      // 
      this.rdbProcessed.AutoSize = true;
      this.rdbProcessed.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.rdbProcessed.Location = new System.Drawing.Point(88, 0);
      this.rdbProcessed.Margin = new System.Windows.Forms.Padding(0);
      this.rdbProcessed.Name = "rdbProcessed";
      this.rdbProcessed.Size = new System.Drawing.Size(59, 13);
      this.rdbProcessed.TabIndex = 0;
      this.rdbProcessed.TabStop = true;
      this.rdbProcessed.Text = "Processed";
      this.rdbProcessed.UseVisualStyleBackColor = true;
      this.rdbProcessed.CheckedChanged += new System.EventHandler(this.RdbModeCheckedChanged);
      // 
      // tableLayoutPanel2
      // 
      this.tableLayoutPanel2.ColumnCount = 1;
      this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel2.Controls.Add(this.flowLayoutPanel2, 0, 2);
      this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel1, 0, 0);
      this.tableLayoutPanel2.Controls.Add(this.pictureBox, 0, 1);
      this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
      this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
      this.tableLayoutPanel2.Name = "tableLayoutPanel2";
      this.tableLayoutPanel2.RowCount = 3;
      this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 15F));
      this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 15F));
      this.tableLayoutPanel2.Size = new System.Drawing.Size(150, 150);
      this.tableLayoutPanel2.TabIndex = 4;
      // 
      // EyeVideoControl
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.tableLayoutPanel2);
      this.Name = "EyeVideoControl";
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
      this.tableLayoutPanel1.ResumeLayout(false);
      this.tableLayoutPanel1.PerformLayout();
      this.flowLayoutPanel2.ResumeLayout(false);
      this.flowLayoutPanel2.PerformLayout();
      this.tableLayoutPanel2.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private Emgu.CV.UI.ImageBox pictureBox;
    private System.Windows.Forms.Label lblFPSHeader;
    private System.Windows.Forms.Label lblFPS;
    private System.Windows.Forms.Label lblCPUHeader;
    private System.Windows.Forms.Label lblCPU;
    private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
    private System.Windows.Forms.RadioButton rdbRaw;
    private System.Windows.Forms.RadioButton rdbNormal;
    private System.Windows.Forms.RadioButton rdbProcessed;
    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
  }
}
