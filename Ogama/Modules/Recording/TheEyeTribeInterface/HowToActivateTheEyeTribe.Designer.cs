namespace Ogama.Modules.Recording.TheEyeTribeInterface
{
  partial class HowToActivateTheEyeTribe
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HowToActivateTheEyeTribe));
      this.pictureBox1 = new System.Windows.Forms.PictureBox();
      this.label1 = new System.Windows.Forms.Label();
      this.btnOK = new System.Windows.Forms.Button();
      this.label3 = new System.Windows.Forms.Label();
      this.pcbTheEyeTribeLogo = new System.Windows.Forms.PictureBox();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pcbTheEyeTribeLogo)).BeginInit();
      this.SuspendLayout();
      // 
      // pictureBox1
      // 
      this.pictureBox1.Image = global::Ogama.Properties.Resources.TheEyeTribeLogo;
      this.pictureBox1.Location = new System.Drawing.Point(18, 95);
      this.pictureBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.pictureBox1.Name = "pictureBox1";
      this.pictureBox1.Size = new System.Drawing.Size(98, 109);
      this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
      this.pictureBox1.TabIndex = 0;
      this.pictureBox1.TabStop = false;
      // 
      // label1
      // 
      this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label1.Location = new System.Drawing.Point(124, 95);
      this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(549, 57);
      this.label1.TabIndex = 1;
      this.label1.Text = "To enable the recording with the device from The Eye Tribe please perform the fol" +
    "lowing steps:";
      // 
      // btnOK
      // 
      this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnOK.AutoSize = true;
      this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.btnOK.Location = new System.Drawing.Point(630, 475);
      this.btnOK.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.btnOK.Name = "btnOK";
      this.btnOK.Size = new System.Drawing.Size(114, 35);
      this.btnOK.TabIndex = 3;
      this.btnOK.Text = "OK";
      this.btnOK.UseVisualStyleBackColor = true;
      // 
      // label3
      // 
      this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label3.Location = new System.Drawing.Point(14, 223);
      this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(717, 190);
      this.label3.TabIndex = 5;
      this.label3.Text = resources.GetString("label3.Text");
      // 
      // pcbTheEyeTribeLogo
      // 
      this.pcbTheEyeTribeLogo.BackColor = System.Drawing.Color.White;
      this.pcbTheEyeTribeLogo.Image = global::Ogama.Properties.Resources.TheEyeTribeBanner;
      this.pcbTheEyeTribeLogo.Location = new System.Drawing.Point(0, 0);
      this.pcbTheEyeTribeLogo.Margin = new System.Windows.Forms.Padding(0);
      this.pcbTheEyeTribeLogo.Name = "pcbTheEyeTribeLogo";
      this.pcbTheEyeTribeLogo.Size = new System.Drawing.Size(765, 80);
      this.pcbTheEyeTribeLogo.TabIndex = 7;
      this.pcbTheEyeTribeLogo.TabStop = false;
      this.pcbTheEyeTribeLogo.Click += new System.EventHandler(this.pcbTheEyeTribeLogo_Click);
      // 
      // HowToActivateTheEyeTribe
      // 
      this.AcceptButton = this.btnOK;
      this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(762, 529);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.btnOK);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.pictureBox1);
      this.Controls.Add(this.pcbTheEyeTribeLogo);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "HowToActivateTheEyeTribe";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "HowTo: Activate TheEyeTribe recording ...";
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pcbTheEyeTribeLogo)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.PictureBox pictureBox1;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Button btnOK;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.PictureBox pcbTheEyeTribeLogo;
  }
}