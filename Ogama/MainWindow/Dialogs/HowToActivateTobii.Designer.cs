namespace Ogama.MainWindow.Dialogs
{
  partial class HowToActivateTobii
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HowToActivateTobii));
      this.pictureBox1 = new System.Windows.Forms.PictureBox();
      this.label1 = new System.Windows.Forms.Label();
      this.btnOK = new System.Windows.Forms.Button();
      this.label3 = new System.Windows.Forms.Label();
      this.pcbTobiiLogo = new System.Windows.Forms.PictureBox();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pcbTobiiLogo)).BeginInit();
      this.SuspendLayout();
      // 
      // pictureBox1
      // 
      this.pictureBox1.Image = global::Ogama.Properties.Resources.TobiiLogo;
      this.pictureBox1.Location = new System.Drawing.Point(12, 62);
      this.pictureBox1.Name = "pictureBox1";
      this.pictureBox1.Size = new System.Drawing.Size(65, 71);
      this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
      this.pictureBox1.TabIndex = 0;
      this.pictureBox1.TabStop = false;
      // 
      // label1
      // 
      this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label1.Location = new System.Drawing.Point(83, 62);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(366, 37);
      this.label1.TabIndex = 1;
      this.label1.Text = "To enable the recording with Tobii Technology tracking systems please perform the" +
    " following steps:";
      // 
      // btnOK
      // 
      this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnOK.AutoSize = true;
      this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.btnOK.Location = new System.Drawing.Point(420, 309);
      this.btnOK.Name = "btnOK";
      this.btnOK.Size = new System.Drawing.Size(76, 23);
      this.btnOK.TabIndex = 3;
      this.btnOK.Text = "OK";
      this.btnOK.UseVisualStyleBackColor = true;
      // 
      // label3
      // 
      this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label3.Location = new System.Drawing.Point(9, 145);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(478, 165);
      this.label3.TabIndex = 5;
      this.label3.Text = resources.GetString("label3.Text");
      // 
      // pcbTobiiLogo
      // 
      this.pcbTobiiLogo.Image = global::Ogama.Properties.Resources.TobiiBanner;
      this.pcbTobiiLogo.Location = new System.Drawing.Point(0, 0);
      this.pcbTobiiLogo.Margin = new System.Windows.Forms.Padding(0);
      this.pcbTobiiLogo.Name = "pcbTobiiLogo";
      this.pcbTobiiLogo.Size = new System.Drawing.Size(510, 49);
      this.pcbTobiiLogo.TabIndex = 7;
      this.pcbTobiiLogo.TabStop = false;
      this.pcbTobiiLogo.Click += new System.EventHandler(this.pcbTobiiLogo_Click);
      // 
      // HowToActivateTobii
      // 
      this.AcceptButton = this.btnOK;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(508, 344);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.btnOK);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.pictureBox1);
      this.Controls.Add(this.pcbTobiiLogo);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "HowToActivateTobii";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "HowTo: Activate Tobii recording ...";
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pcbTobiiLogo)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.PictureBox pictureBox1;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Button btnOK;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.PictureBox pcbTobiiLogo;
  }
}