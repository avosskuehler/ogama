namespace Ogama.Modules.Recording.GazepointInterface
{
    partial class GazepointTrackStatusControl
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
            this.pbTrackStatusGazepointControl = new System.Windows.Forms.PictureBox();
            this.pbAccuracyControl = new System.Windows.Forms.PictureBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbTrackStatusGazepointControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbAccuracyControl)).BeginInit();
            this.SuspendLayout();
            // 
            // pbTrackStatusGazepointControl
            // 
            this.pbTrackStatusGazepointControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pbTrackStatusGazepointControl.BackColor = System.Drawing.Color.Maroon;
            this.pbTrackStatusGazepointControl.Location = new System.Drawing.Point(20, 0);
            this.pbTrackStatusGazepointControl.Name = "pbTrackStatusGazepointControl";
            this.pbTrackStatusGazepointControl.Size = new System.Drawing.Size(177, 150);
            this.pbTrackStatusGazepointControl.TabIndex = 0;
            this.pbTrackStatusGazepointControl.TabStop = false;
            // 
            // pbAccuracyControl
            // 
            this.pbAccuracyControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.pbAccuracyControl.BackColor = System.Drawing.Color.White;
            this.pbAccuracyControl.Location = new System.Drawing.Point(0, 0);
            this.pbAccuracyControl.Name = "pbAccuracyControl";
            this.pbAccuracyControl.Size = new System.Drawing.Size(20, 150);
            this.pbAccuracyControl.TabIndex = 1;
            this.pbAccuracyControl.TabStop = false;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(30, 57);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(164, 37);
            this.textBox1.TabIndex = 2;
            this.textBox1.Text = "Please, click \"Show on presentation screen\" to visualize.";
            // 
            // GazepointTrackStatusControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.pbAccuracyControl);
            this.Controls.Add(this.pbTrackStatusGazepointControl);
            this.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.Name = "GazepointTrackStatusControl";
            this.Size = new System.Drawing.Size(197, 150);
            ((System.ComponentModel.ISupportInitialize)(this.pbTrackStatusGazepointControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbAccuracyControl)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbTrackStatusGazepointControl;
        private System.Windows.Forms.PictureBox pbAccuracyControl;
        private System.Windows.Forms.TextBox textBox1;
    }
}
