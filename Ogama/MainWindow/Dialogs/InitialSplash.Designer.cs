namespace Ogama.MainWindow.Dialogs
{
    partial class InitialSplash
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
          this.components = new System.ComponentModel.Container();
          System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InitialSplash));
          this.timer1 = new System.Windows.Forms.Timer(this.components);
          this.labelVersion = new System.Windows.Forms.Label();
          this.labelCopyright = new System.Windows.Forms.Label();
          this.pictureBox1 = new System.Windows.Forms.PictureBox();
          ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
          this.SuspendLayout();
          // 
          // timer1
          // 
          this.timer1.Enabled = true;
          this.timer1.Interval = 5000;
          this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
          // 
          // labelVersion
          // 
          this.labelVersion.AutoSize = true;
          this.labelVersion.BackColor = System.Drawing.Color.White;
          this.labelVersion.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.labelVersion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
          this.labelVersion.Location = new System.Drawing.Point(12, 193);
          this.labelVersion.Name = "labelVersion";
          this.labelVersion.Size = new System.Drawing.Size(50, 13);
          this.labelVersion.TabIndex = 2;
          this.labelVersion.Text = "Version";
          // 
          // labelCopyright
          // 
          this.labelCopyright.AutoSize = true;
          this.labelCopyright.BackColor = System.Drawing.Color.White;
          this.labelCopyright.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.labelCopyright.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
          this.labelCopyright.Location = new System.Drawing.Point(134, 125);
          this.labelCopyright.Name = "labelCopyright";
          this.labelCopyright.Size = new System.Drawing.Size(63, 13);
          this.labelCopyright.TabIndex = 2;
          this.labelCopyright.Text = "Copyright";
          // 
          // pictureBox1
          // 
          this.pictureBox1.BackColor = System.Drawing.Color.White;
          this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
          this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
          this.pictureBox1.Location = new System.Drawing.Point(0, 0);
          this.pictureBox1.Name = "pictureBox1";
          this.pictureBox1.Size = new System.Drawing.Size(320, 220);
          this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
          this.pictureBox1.TabIndex = 3;
          this.pictureBox1.TabStop = false;
          // 
          // InitialSplash
          // 
          this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
          this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
          this.BackColor = System.Drawing.Color.Magenta;
          this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
          this.ClientSize = new System.Drawing.Size(320, 220);
          this.ControlBox = false;
          this.Controls.Add(this.labelCopyright);
          this.Controls.Add(this.labelVersion);
          this.Controls.Add(this.pictureBox1);
          this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
          this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
          this.Name = "InitialSplash";
          this.ShowIcon = false;
          this.ShowInTaskbar = false;
          this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
          this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
          this.Text = "frmSplash";
          ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
          this.ResumeLayout(false);
          this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
      private System.Windows.Forms.Label labelVersion;
      private System.Windows.Forms.Label labelCopyright;
      private System.Windows.Forms.PictureBox pictureBox1;
    }
}