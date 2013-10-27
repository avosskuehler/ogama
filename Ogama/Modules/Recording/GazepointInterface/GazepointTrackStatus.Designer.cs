namespace Ogama.Modules.Recording.GazepointInterface
{
    partial class GazepointTrackStatus
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
            this.pbTrackStatusGazepoint = new System.Windows.Forms.PictureBox();
            this.pbAccuracy = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbTrackStatusGazepoint)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbAccuracy)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pbTrackStatusGazepoint
            // 
            this.pbTrackStatusGazepoint.BackColor = System.Drawing.Color.Black;
            this.pbTrackStatusGazepoint.Location = new System.Drawing.Point(27, 152);
            this.pbTrackStatusGazepoint.Name = "pbTrackStatusGazepoint";
            this.pbTrackStatusGazepoint.Size = new System.Drawing.Size(344, 178);
            this.pbTrackStatusGazepoint.TabIndex = 0;
            this.pbTrackStatusGazepoint.TabStop = false;
            this.pbTrackStatusGazepoint.Paint += new System.Windows.Forms.PaintEventHandler(this.pbTrackStatusGazepoint_Paint);
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
            this.label1.Text = "1 . Run Gazepoint Control application from Gazepoint.\r\n\r\n2. Prepare the user for " +
    "recording by using the distance bar and live image in Control\r\n";
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
            this.pictureBox1.Image = global::Ogama.Properties.Resources.GazepointLogo;
            this.pictureBox1.Location = new System.Drawing.Point(1, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(154, 34);
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // GazepointTrackStatus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(375, 342);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pbAccuracy);
            this.Controls.Add(this.pbTrackStatusGazepoint);
            this.MaximumSize = new System.Drawing.Size(391, 380);
            this.MinimumSize = new System.Drawing.Size(391, 380);
            this.Name = "GazepointTrackStatus";
            this.Text = "GazepointTrackStatus";
            this.Load += new System.EventHandler(this.GazepointTrackStatus_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbTrackStatusGazepoint)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbAccuracy)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbTrackStatusGazepoint;
        private System.Windows.Forms.PictureBox pbAccuracy;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox1;

    }
}