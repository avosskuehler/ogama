namespace Ogama.Modules.Recording.SmartEyeInterface
{
    partial class HowToActivateSmartEye
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HowToActivateSmartEye));
            this.label3 = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.pcbSmartEye = new System.Windows.Forms.PictureBox();
            this.pcbSmartEyeLogo = new System.Windows.Forms.PictureBox();
            this.llbSmartEyeWebsite = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.pcbSmartEye)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcbSmartEyeLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(11, 125);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(486, 182);
            this.label3.TabIndex = 16;
            this.label3.Text = resources.GetString("label3.Text");
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.AutoSize = true;
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(421, 344);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(76, 23);
            this.btnOK.TabIndex = 15;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(86, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(366, 37);
            this.label1.TabIndex = 14;
            this.label1.Text = "To enable the recording with Smart Eye Aurora eye tracking system please perform " +
    "the following steps:";
            // 
            // pcbSmartEye
            // 
            this.pcbSmartEye.Image = global::Ogama.Properties.Resources.SmartEyeAuroraFoto64;
            this.pcbSmartEye.Location = new System.Drawing.Point(12, 53);
            this.pcbSmartEye.Name = "pcbSmartEye";
            this.pcbSmartEye.Size = new System.Drawing.Size(64, 64);
            this.pcbSmartEye.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pcbSmartEye.TabIndex = 13;
            this.pcbSmartEye.TabStop = false;
            // 
            // pcbSmartEyeLogo
            // 
            this.pcbSmartEyeLogo.BackColor = System.Drawing.SystemColors.Control;
            this.pcbSmartEyeLogo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pcbSmartEyeLogo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pcbSmartEyeLogo.Image = global::Ogama.Properties.Resources.SmartEyeLogo;
            this.pcbSmartEyeLogo.Location = new System.Drawing.Point(0, 0);
            this.pcbSmartEyeLogo.Margin = new System.Windows.Forms.Padding(10);
            this.pcbSmartEyeLogo.Name = "pcbSmartEyeLogo";
            this.pcbSmartEyeLogo.Padding = new System.Windows.Forms.Padding(10);
            this.pcbSmartEyeLogo.Size = new System.Drawing.Size(509, 53);
            this.pcbSmartEyeLogo.TabIndex = 17;
            this.pcbSmartEyeLogo.TabStop = false;
            this.pcbSmartEyeLogo.Click += new System.EventHandler(this.PcbSmartEyeLogo_Click);
            // 
            // llbSmartEyeWebsite
            // 
            this.llbSmartEyeWebsite.AutoSize = true;
            this.llbSmartEyeWebsite.Location = new System.Drawing.Point(12, 314);
            this.llbSmartEyeWebsite.Name = "llbSmartEyeWebsite";
            this.llbSmartEyeWebsite.Size = new System.Drawing.Size(97, 13);
            this.llbSmartEyeWebsite.TabIndex = 18;
            this.llbSmartEyeWebsite.TabStop = true;
            this.llbSmartEyeWebsite.Text = "Smart Eye Website";
            this.llbSmartEyeWebsite.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LlbSmartEyeWebsite_LinkClicked);
            // 
            // HowToActivateSmartEye
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(509, 379);
            this.Controls.Add(this.llbSmartEyeWebsite);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pcbSmartEye);
            this.Controls.Add(this.pcbSmartEyeLogo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "HowToActivateSmartEye";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "HowTo: Activate Smart Eye Aurora recording ...";
            ((System.ComponentModel.ISupportInitialize)(this.pcbSmartEye)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcbSmartEyeLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pcbSmartEye;
        private System.Windows.Forms.PictureBox pcbSmartEyeLogo;
        private System.Windows.Forms.LinkLabel llbSmartEyeWebsite;
    }
}