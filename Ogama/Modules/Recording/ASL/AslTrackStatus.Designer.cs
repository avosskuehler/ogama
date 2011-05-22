namespace Ogama.Modules.Recording.ASL
{
    partial class AslTrackStatus
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox ();
            this.lblDescription = new System.Windows.Forms.Label ();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer ();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer ();
            this.lblDescription2 = new System.Windows.Forms.Label ();
            this.continueButton = new System.Windows.Forms.Button ();
            ((System.ComponentModel.ISupportInitialize) (this.pictureBox1)).BeginInit ();
            this.splitContainer1.Panel1.SuspendLayout ();
            this.splitContainer1.Panel2.SuspendLayout ();
            this.splitContainer1.SuspendLayout ();
            this.splitContainer2.Panel1.SuspendLayout ();
            this.splitContainer2.Panel2.SuspendLayout ();
            this.splitContainer2.SuspendLayout ();
            this.SuspendLayout ();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Image = global::Ogama.Properties.Resources.FixationsLogo;
            this.pictureBox1.Location = new System.Drawing.Point (0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size (40, 180);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // lblDescription
            // 
            this.lblDescription.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblDescription.Location = new System.Drawing.Point (5, 5);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size (353, 118);
            this.lblDescription.TabIndex = 3;
            this.lblDescription.UseMnemonic = false;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point (0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add (this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add (this.continueButton);
            this.splitContainer1.Size = new System.Drawing.Size (407, 384);
            this.splitContainer1.SplitterDistance = 180;
            this.splitContainer1.TabIndex = 4;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer2.IsSplitterFixed = true;
            this.splitContainer2.Location = new System.Drawing.Point (0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add (this.pictureBox1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add (this.lblDescription2);
            this.splitContainer2.Panel2.Controls.Add (this.lblDescription);
            this.splitContainer2.Panel2.Padding = new System.Windows.Forms.Padding (5);
            this.splitContainer2.Size = new System.Drawing.Size (407, 180);
            this.splitContainer2.SplitterDistance = 40;
            this.splitContainer2.TabIndex = 4;
            // 
            // lblDescription2
            // 
            this.lblDescription2.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblDescription2.Font = new System.Drawing.Font ("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.lblDescription2.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblDescription2.Location = new System.Drawing.Point (5, 123);
            this.lblDescription2.Name = "lblDescription2";
            this.lblDescription2.Size = new System.Drawing.Size (353, 52);
            this.lblDescription2.TabIndex = 4;
            this.lblDescription2.UseMnemonic = false;
            // 
            // continueButton
            // 
            this.continueButton.Location = new System.Drawing.Point (166, 79);
            this.continueButton.Name = "continueButton";
            this.continueButton.Size = new System.Drawing.Size (75, 23);
            this.continueButton.TabIndex = 0;
            this.continueButton.Text = "continue";
            this.continueButton.UseVisualStyleBackColor = true;
            this.continueButton.Click += new System.EventHandler (this.continueButton_Click);
            // 
            // AslTrackStatus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF (6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size (407, 384);
            this.Controls.Add (this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "AslTrackStatus";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Setting Up Calibration";
            ((System.ComponentModel.ISupportInitialize) (this.pictureBox1)).EndInit ();
            this.splitContainer1.Panel1.ResumeLayout (false);
            this.splitContainer1.Panel2.ResumeLayout (false);
            this.splitContainer1.ResumeLayout (false);
            this.splitContainer2.Panel1.ResumeLayout (false);
            this.splitContainer2.Panel2.ResumeLayout (false);
            this.splitContainer2.ResumeLayout (false);
            this.ResumeLayout (false);

        }

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;

        #endregion
        private System.Windows.Forms.Label lblDescription2;
        private System.Windows.Forms.Button continueButton;


    }
}