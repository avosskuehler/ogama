using System.Drawing;
using System.Windows.Forms;
namespace Ogama.Modules.Recording.SmartEyeInterface
{
    partial class SmartEyeTrackStatusControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private PictureBox liveImagePictureBox;

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
            this.liveImagePictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.liveImagePictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // liveImagePictureBox
            // 
            this.liveImagePictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.liveImagePictureBox.BackColor = System.Drawing.Color.Transparent;
            this.liveImagePictureBox.Location = new System.Drawing.Point(0, 0);
            this.liveImagePictureBox.Name = "liveImagePictureBox";
            this.liveImagePictureBox.Size = new System.Drawing.Size(320, 240);
            this.liveImagePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.liveImagePictureBox.TabIndex = 0;
            this.liveImagePictureBox.TabStop = false;
            // 
            // SmartEyeTrackStatusControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.liveImagePictureBox);
            this.Name = "SmartEyeTrackStatusControl";
            this.Size = new System.Drawing.Size(320, 240);
            ((System.ComponentModel.ISupportInitialize)(this.liveImagePictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion





    }
}