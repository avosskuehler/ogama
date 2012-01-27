namespace Ogama.MainWindow.Dialogs
{
    partial class HowToActivateAlea
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HowToActivateAlea));
            this.label3 = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.pcbAlea = new System.Windows.Forms.PictureBox();
            this.pcbAleaLogo = new System.Windows.Forms.PictureBox();
            this.llbAleaWebsite = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.pcbAlea)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcbAleaLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(11, 147);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(486, 137);
            this.label3.TabIndex = 11;
            this.label3.Text = resources.GetString("label3.Text");
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.AutoSize = true;
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(421, 289);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(76, 23);
            this.btnOK.TabIndex = 10;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(86, 81);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(366, 37);
            this.label1.TabIndex = 9;
            this.label1.Text = "To enable the recording with Alea Technologies tracking system please perform the" +
                " following steps:";
            // 
            // pcbAlea
            // 
            this.pcbAlea.Image = global::Ogama.Properties.Resources.Alea_54_75;
            this.pcbAlea.Location = new System.Drawing.Point(12, 62);
            this.pcbAlea.Name = "pcbAlea";
            this.pcbAlea.Size = new System.Drawing.Size(54, 75);
            this.pcbAlea.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pcbAlea.TabIndex = 8;
            this.pcbAlea.TabStop = false;
            // 
            // pcbAleaLogo
            // 
            this.pcbAleaLogo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pcbAleaLogo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pcbAleaLogo.Image = global::Ogama.Properties.Resources.alea_banner3;
            this.pcbAleaLogo.Location = new System.Drawing.Point(0, 0);
            this.pcbAleaLogo.Margin = new System.Windows.Forms.Padding(0);
            this.pcbAleaLogo.Name = "pcbAleaLogo";
            this.pcbAleaLogo.Size = new System.Drawing.Size(509, 50);
            this.pcbAleaLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pcbAleaLogo.TabIndex = 12;
            this.pcbAleaLogo.TabStop = false;
            this.pcbAleaLogo.Click += new System.EventHandler(this.pcbAleaLogo_Click);
            // 
            // llbAleaWebsite
            // 
            this.llbAleaWebsite.AutoSize = true;
            this.llbAleaWebsite.Location = new System.Drawing.Point(12, 271);
            this.llbAleaWebsite.Name = "llbAleaWebsite";
            this.llbAleaWebsite.Size = new System.Drawing.Size(129, 13);
            this.llbAleaWebsite.TabIndex = 14;
            this.llbAleaWebsite.TabStop = true;
            this.llbAleaWebsite.Text = "alea technologies website";
            this.llbAleaWebsite.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llbAleaWebsite_LinkClicked);
            // 
            // HowToActivateAlea
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(509, 324);
            this.Controls.Add(this.llbAleaWebsite);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pcbAlea);
            this.Controls.Add(this.pcbAleaLogo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "HowToActivateAlea";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "HowTo: Activate Alea recording...";
            ((System.ComponentModel.ISupportInitialize)(this.pcbAlea)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcbAleaLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pcbAlea;
        private System.Windows.Forms.PictureBox pcbAleaLogo;
        private System.Windows.Forms.LinkLabel llbAleaWebsite;
    }
}