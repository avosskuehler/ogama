namespace Ogama.Modules.Recording.ASLInterface
{
    partial class HowToActivateAsl
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
            this.label3 = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.pcbAsl = new System.Windows.Forms.PictureBox();
            this.pcbAslLogo = new System.Windows.Forms.PictureBox();
            this.llbAslWebsite = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.pcbAsl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcbAslLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 163);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(525, 285);
            this.label3.TabIndex = 11;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.AutoSize = true;
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(461, 453);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(76, 23);
            this.btnOK.TabIndex = 10;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(121, 91);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(416, 37);
            this.label1.TabIndex = 9;
            this.label1.Text = "To enable the recording with Asl Eye Tracker please perform the following steps :" +
                "";
            // 
            // pcbAsl
            // 
            this.pcbAsl.Image = global::Ogama.Properties.Resources.ASLLogo;
            this.pcbAsl.Location = new System.Drawing.Point(0, 91);
            this.pcbAsl.Name = "pcbAsl";
            this.pcbAsl.Size = new System.Drawing.Size(115, 69);
            this.pcbAsl.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pcbAsl.TabIndex = 8;
            this.pcbAsl.TabStop = false;
            // 
            // pcbAslLogo
            // 
            this.pcbAslLogo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pcbAslLogo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pcbAslLogo.Image = global::Ogama.Properties.Resources.ASLBanner;
            this.pcbAslLogo.Location = new System.Drawing.Point(0, 0);
            this.pcbAslLogo.Margin = new System.Windows.Forms.Padding(0);
            this.pcbAslLogo.Name = "pcbAslLogo";
            this.pcbAslLogo.Size = new System.Drawing.Size(549, 91);
            this.pcbAslLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pcbAslLogo.TabIndex = 12;
            this.pcbAslLogo.TabStop = false;
            this.pcbAslLogo.Click += new System.EventHandler(this.pcbAslLogo_Click);
            // 
            // llbAslWebsite
            // 
            this.llbAslWebsite.AutoSize = true;
            this.llbAslWebsite.Location = new System.Drawing.Point(12, 463);
            this.llbAslWebsite.Name = "llbAslWebsite";
            this.llbAslWebsite.Size = new System.Drawing.Size(66, 13);
            this.llbAslWebsite.TabIndex = 14;
            this.llbAslWebsite.TabStop = true;
            this.llbAslWebsite.Text = "ASL website";
            this.llbAslWebsite.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llbAslWebsite_LinkClicked);
            // 
            // HowToActivateAsl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(549, 488);
            this.Controls.Add(this.llbAslWebsite);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pcbAsl);
            this.Controls.Add(this.pcbAslLogo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "HowToActivateAsl";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "HowTo : Activate Asl recording...";
            ((System.ComponentModel.ISupportInitialize)(this.pcbAsl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcbAslLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pcbAsl;
        private System.Windows.Forms.PictureBox pcbAslLogo;
        private System.Windows.Forms.LinkLabel llbAslWebsite;
    }
}