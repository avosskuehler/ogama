namespace Ogama.Modules.Recording.GazepointInterface
{
    partial class HowToActivateGazepoint
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HowToActivateGazepoint));
            this.btnOK = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pcbTobiiLogo = new System.Windows.Forms.PictureBox();
            this.linkGazepointText = new System.Windows.Forms.LinkLabel();
            this.linkGazepointSupportText = new System.Windows.Forms.LinkLabel();
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
            this.pictureBox1.Image = global::Ogama.Properties.Resources.GazepointFoto64;
            this.pictureBox1.Location = new System.Drawing.Point(15, 86);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(64, 64);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pcbGazepointLogo_Click);
            // 
            // pcbTobiiLogo
            // 
            this.pcbTobiiLogo.BackColor = System.Drawing.Color.LightGray;
            this.pcbTobiiLogo.Image = global::Ogama.Properties.Resources.GazepointBanner;
            this.pcbTobiiLogo.Location = new System.Drawing.Point(-1, -3);
            this.pcbTobiiLogo.Margin = new System.Windows.Forms.Padding(0);
            this.pcbTobiiLogo.Name = "pcbTobiiLogo";
            this.pcbTobiiLogo.Size = new System.Drawing.Size(545, 68);
            this.pcbTobiiLogo.TabIndex = 12;
            this.pcbTobiiLogo.TabStop = false;
            // 
            // linkGazepointText
            // 
            this.linkGazepointText.AutoSize = true;
            this.linkGazepointText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.linkGazepointText.Location = new System.Drawing.Point(165, 101);
            this.linkGazepointText.Name = "linkGazepointText";
            this.linkGazepointText.Size = new System.Drawing.Size(140, 16);
            this.linkGazepointText.TabIndex = 13;
            this.linkGazepointText.TabStop = true;
            this.linkGazepointText.Text = "http://www.gazept.com";
            this.linkGazepointText.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkGazepointText_LinkClicked);
            // 
            // linkGazepointSupportText
            // 
            this.linkGazepointSupportText.AutoSize = true;
            this.linkGazepointSupportText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.linkGazepointSupportText.Location = new System.Drawing.Point(165, 151);
            this.linkGazepointSupportText.Name = "linkGazepointSupportText";
            this.linkGazepointSupportText.Size = new System.Drawing.Size(210, 16);
            this.linkGazepointSupportText.TabIndex = 14;
            this.linkGazepointSupportText.TabStop = true;
            this.linkGazepointSupportText.Text = "http://www.gazept.com/downloads";
            this.linkGazepointSupportText.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkGazepointSupportText_LinkClicked);
            // 
            // HowToActivateGazepoint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(514, 432);
            this.Controls.Add(this.linkGazepointSupportText);
            this.Controls.Add(this.linkGazepointText);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.pcbTobiiLogo);
            this.Name = "HowToActivateGazepoint";
            this.Text = "HowTo: Activate Gazepoint recording...";
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
        private System.Windows.Forms.LinkLabel linkGazepointText;
        private System.Windows.Forms.LinkLabel linkGazepointSupportText;

    }
}