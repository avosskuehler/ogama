namespace Ogama.Modules.Recording.MirametrixInterface
{
    partial class MirametrixTrackStatus
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
            this.pbTrackStatusMirametrix = new System.Windows.Forms.PictureBox();
            this.pbAccuracy = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbTrackStatusMirametrix)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbAccuracy)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pbTrackStatusMirametrix
            // 
            this.pbTrackStatusMirametrix.BackColor = System.Drawing.Color.Black;
            this.pbTrackStatusMirametrix.Location = new System.Drawing.Point(27, 152);
            this.pbTrackStatusMirametrix.Name = "pbTrackStatusMirametrix";
            this.pbTrackStatusMirametrix.Size = new System.Drawing.Size(344, 178);
            this.pbTrackStatusMirametrix.TabIndex = 0;
            this.pbTrackStatusMirametrix.TabStop = false;
            this.pbTrackStatusMirametrix.Paint += new System.Windows.Forms.PaintEventHandler(this.pbTrackStatusMirametrix_Paint);
            // 
            // pbAccuracy
            // 
            this.pbAccuracy.BackColor = System.Drawing.Color.Black;
            this.pbAccuracy.Location = new System.Drawing.Point(1, 152);
            this.pbAccuracy.Name = "pbAccuracy";
            this.pbAccuracy.Size = new System.Drawing.Size(20, 178);
            this.pbAccuracy.TabIndex = 1;
            this.pbAccuracy.TabStop = false;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(79, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(284, 100);
            this.label1.TabIndex = 2;
            this.label1.Text = "1 . Run Tracker application from Mirametrix.\r\n\r\n2. Under Settings select the expe" +
                "riment monitor.\r\n\r\n3. Prepare the user for recording by using the distance bar a" +
                "nd live image in Tracker\r\n";
            // 
            // label2
            // 
            this.label2.Image = global::Ogama.Properties.Resources.FixationsLogo;
            this.label2.Location = new System.Drawing.Point(-2, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 64);
            this.label2.TabIndex = 3;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Ogama.Properties.Resources.MirametrixLogo;
            this.pictureBox1.Location = new System.Drawing.Point(1, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(154, 34);
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // MirametrixTrackStatus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(375, 342);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pbAccuracy);
            this.Controls.Add(this.pbTrackStatusMirametrix);
            this.MaximumSize = new System.Drawing.Size(391, 380);
            this.MinimumSize = new System.Drawing.Size(391, 380);
            this.Name = "MirametrixTrackStatus";
            this.Text = "MirametrixTrackStatus";
            this.Load += new System.EventHandler(this.MirametrixTrackStatus_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbTrackStatusMirametrix)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbAccuracy)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbTrackStatusMirametrix;
        private System.Windows.Forms.PictureBox pbAccuracy;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox1;

    }
}