namespace Ogama.MainWindow.Dialogs
{
    partial class HowToActivateMirametrix
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HowToActivateMirametrix));
      this.btnOK = new System.Windows.Forms.Button();
      this.label1 = new System.Windows.Forms.Label();
      this.pictureBox1 = new System.Windows.Forms.PictureBox();
      this.pcbTobiiLogo = new System.Windows.Forms.PictureBox();
      this.linkMirametrixText = new System.Windows.Forms.LinkLabel();
      this.linkMirametrixSupportText = new System.Windows.Forms.LinkLabel();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pcbTobiiLogo)).BeginInit();
      this.SuspendLayout();
      // 
      // btnOK
      // 
      this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnOK.AutoSize = true;
      this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.btnOK.Location = new System.Drawing.Point(218, 397);
      this.btnOK.Name = "btnOK";
      this.btnOK.Size = new System.Drawing.Size(76, 23);
      this.btnOK.TabIndex = 10;
      this.btnOK.Text = "OK";
      this.btnOK.UseVisualStyleBackColor = true;
      // 
      // label1
      // 
      this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label1.Location = new System.Drawing.Point(86, 86);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(414, 305);
      this.label1.TabIndex = 9;
      this.label1.Text = resources.GetString("label1.Text");
      // 
      // pictureBox1
      // 
      this.pictureBox1.Image = global::Ogama.Properties.Resources.MirametrixFoto64;
      this.pictureBox1.Location = new System.Drawing.Point(15, 86);
      this.pictureBox1.Name = "pictureBox1";
      this.pictureBox1.Size = new System.Drawing.Size(64, 64);
      this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
      this.pictureBox1.TabIndex = 8;
      this.pictureBox1.TabStop = false;
      this.pictureBox1.Click += new System.EventHandler(this.pcbMirametrixLogo_Click);
      // 
      // pcbTobiiLogo
      // 
      this.pcbTobiiLogo.BackColor = System.Drawing.Color.LightGray;
      this.pcbTobiiLogo.Image = global::Ogama.Properties.Resources.MirametrixBanner;
      this.pcbTobiiLogo.Location = new System.Drawing.Point(-1, -3);
      this.pcbTobiiLogo.Margin = new System.Windows.Forms.Padding(0);
      this.pcbTobiiLogo.Name = "pcbTobiiLogo";
      this.pcbTobiiLogo.Size = new System.Drawing.Size(517, 68);
      this.pcbTobiiLogo.TabIndex = 12;
      this.pcbTobiiLogo.TabStop = false;
      // 
      // linkMirametrixText
      // 
      this.linkMirametrixText.AutoSize = true;
      this.linkMirametrixText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
      this.linkMirametrixText.Location = new System.Drawing.Point(165, 102);
      this.linkMirametrixText.Name = "linkMirametrixText";
      this.linkMirametrixText.Size = new System.Drawing.Size(160, 16);
      this.linkMirametrixText.TabIndex = 13;
      this.linkMirametrixText.TabStop = true;
      this.linkMirametrixText.Text = "http://www.mirametrix.com";
      this.linkMirametrixText.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkMirametrixText_LinkClicked);
      // 
      // linkMirametrixSupportText
      // 
      this.linkMirametrixSupportText.AutoSize = true;
      this.linkMirametrixSupportText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
      this.linkMirametrixSupportText.Location = new System.Drawing.Point(100, 149);
      this.linkMirametrixSupportText.Name = "linkMirametrixSupportText";
      this.linkMirametrixSupportText.Size = new System.Drawing.Size(217, 16);
      this.linkMirametrixSupportText.TabIndex = 14;
      this.linkMirametrixSupportText.TabStop = true;
      this.linkMirametrixSupportText.Text = "http://www.mirametrix,coom/support";
      this.linkMirametrixSupportText.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkMirametrixSupportText_LinkClicked);
      // 
      // HowToActivateMirametrix
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(514, 432);
      this.Controls.Add(this.linkMirametrixSupportText);
      this.Controls.Add(this.linkMirametrixText);
      this.Controls.Add(this.btnOK);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.pictureBox1);
      this.Controls.Add(this.pcbTobiiLogo);
      this.Name = "HowToActivateMirametrix";
      this.Text = "HowTo: Activate Mirametrix recording...";
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pcbTobiiLogo)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pcbTobiiLogo;
        private System.Windows.Forms.LinkLabel linkMirametrixText;
        private System.Windows.Forms.LinkLabel linkMirametrixSupportText;

    }
}