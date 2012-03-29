namespace Ogama.Modules.Diagrams
{
    partial class DiagramsModule
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DiagramsModule));
            this.elementHost1 = new System.Windows.Forms.Integration.ElementHost();
            this.diagramsControl1 = new Ogama.Modules.Diagrams.DiagramsControl();
            ((System.ComponentModel.ISupportInitialize)(this.bsoSubjects)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsoFKSubjectsTrials)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ogamaDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsoFKTrialsEvents)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsoTrialsAOIs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsoTrialsGazeFixations)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsoTrialsMouseFixations)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsoShapeGroups)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsoFKSubjectsSubjectParameters)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsoSubjectParameters)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsoTrialEvents)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsoTrials)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsoGazeFixations)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsoMouseFixations)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsoAOIs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsoParams)).BeginInit();
            this.SuspendLayout();
            // 
            // elementHost1
            // 
            this.elementHost1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.elementHost1.Location = new System.Drawing.Point(12, 12);
            this.elementHost1.Name = "elementHost1";
            this.elementHost1.Size = new System.Drawing.Size(550, 450);
            this.elementHost1.TabIndex = 0;
            this.elementHost1.Text = "elementHost1";
            this.elementHost1.Child = this.diagramsControl1;
            // 
            // DiagramsModule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(576, 473);
            this.Controls.Add(this.elementHost1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Logo = global::Ogama.Properties.Resources.bar_chart_32;
            this.Name = "DiagramsModule";
            this.Text = "DiagramsModule";
            ((System.ComponentModel.ISupportInitialize)(this.bsoSubjects)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsoFKSubjectsTrials)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ogamaDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsoFKTrialsEvents)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsoTrialsAOIs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsoTrialsGazeFixations)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsoTrialsMouseFixations)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsoShapeGroups)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsoFKSubjectsSubjectParameters)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsoSubjectParameters)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsoTrialEvents)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsoTrials)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsoGazeFixations)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsoMouseFixations)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsoAOIs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsoParams)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Integration.ElementHost elementHost1;
        private DiagramsControl diagramsControl1;
    }
}