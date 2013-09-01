namespace Ogama.Modules.Rta
{
    partial class ProgressDialog
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
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.label1 = new System.Windows.Forms.Label();
            this.labelCurrentVideoFilter = new System.Windows.Forms.Label();
            this.labelMaxItems = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(12, 61);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(352, 19);
            this.progressBar.TabIndex = 0;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(136, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "processing video filter:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // labelCurrentVideoFilter
            // 
            this.labelCurrentVideoFilter.AutoSize = true;
            this.labelCurrentVideoFilter.Location = new System.Drawing.Point(13, 36);
            this.labelCurrentVideoFilter.Name = "labelCurrentVideoFilter";
            this.labelCurrentVideoFilter.Size = new System.Drawing.Size(19, 13);
            this.labelCurrentVideoFilter.TabIndex = 2;
            this.labelCurrentVideoFilter.Text = ">>";
            // 
            // labelMaxItems
            // 
            this.labelMaxItems.AutoSize = true;
            this.labelMaxItems.Location = new System.Drawing.Point(339, 83);
            this.labelMaxItems.Name = "labelMaxItems";
            this.labelMaxItems.Size = new System.Drawing.Size(25, 13);
            this.labelMaxItems.TabIndex = 3;
            this.labelMaxItems.Text = "100";
            this.labelMaxItems.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 83);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(13, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "0";
            // 
            // ProgressDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(376, 119);
            this.ControlBox = false;
            this.Controls.Add(this.label3);
            this.Controls.Add(this.labelMaxItems);
            this.Controls.Add(this.labelCurrentVideoFilter);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.progressBar);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ProgressDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Analyzing Video Filters";
            this.Load += new System.EventHandler(this.ProgressDialog_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBar;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelCurrentVideoFilter;
        private System.Windows.Forms.Label labelMaxItems;
        private System.Windows.Forms.Label label3;
    }
}