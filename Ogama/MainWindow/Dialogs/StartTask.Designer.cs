namespace Ogama.MainWindow.Dialogs
{
  using Ogama.Modules.Common.Controls;

  partial class StartTask
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StartTask));
      this.btnReplay = new System.Windows.Forms.Button();
      this.btnImport = new System.Windows.Forms.Button();
      this.btnDesign = new System.Windows.Forms.Button();
      this.btnRecord = new System.Windows.Forms.Button();
      this.dialogTop1 = new Ogama.Modules.Common.Controls.DialogTop();
      this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
      this.grpAnalyze = new System.Windows.Forms.GroupBox();
      this.grpImport = new System.Windows.Forms.GroupBox();
      this.grbDesign = new System.Windows.Forms.GroupBox();
      this.grpRecord = new System.Windows.Forms.GroupBox();
      this.btnAttentionMap = new System.Windows.Forms.Button();
      this.btnScanpaths = new System.Windows.Forms.Button();
      this.btnFixations = new System.Windows.Forms.Button();
      this.btnAOI = new System.Windows.Forms.Button();
      this.btnStatistics = new System.Windows.Forms.Button();
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      this.tableLayoutPanel1.SuspendLayout();
      this.grpAnalyze.SuspendLayout();
      this.grpImport.SuspendLayout();
      this.grbDesign.SuspendLayout();
      this.grpRecord.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      this.SuspendLayout();
      // 
      // btnReplay
      // 
      this.btnReplay.Image = global::Ogama.Properties.Resources.ReplayLogo;
      this.btnReplay.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.btnReplay.Location = new System.Drawing.Point(3, 19);
      this.btnReplay.Name = "btnReplay";
      this.btnReplay.Size = new System.Drawing.Size(114, 40);
      this.btnReplay.TabIndex = 2;
      this.btnReplay.Text = "Replay";
      this.btnReplay.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.btnReplay.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.btnReplay.UseVisualStyleBackColor = true;
      this.btnReplay.Click += new System.EventHandler(this.btnReplay_Click);
      // 
      // btnImport
      // 
      this.btnImport.Dock = System.Windows.Forms.DockStyle.Top;
      this.btnImport.Image = global::Ogama.Properties.Resources.ImportData;
      this.btnImport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.btnImport.Location = new System.Drawing.Point(3, 16);
      this.btnImport.Name = "btnImport";
      this.btnImport.Size = new System.Drawing.Size(114, 40);
      this.btnImport.TabIndex = 2;
      this.btnImport.Text = "Import data";
      this.btnImport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.btnImport.UseVisualStyleBackColor = true;
      this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
      // 
      // btnDesign
      // 
      this.btnDesign.Dock = System.Windows.Forms.DockStyle.Top;
      this.btnDesign.Image = global::Ogama.Properties.Resources.Design;
      this.btnDesign.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.btnDesign.Location = new System.Drawing.Point(3, 16);
      this.btnDesign.Name = "btnDesign";
      this.btnDesign.Size = new System.Drawing.Size(115, 40);
      this.btnDesign.TabIndex = 2;
      this.btnDesign.Text = "Design slideshow.";
      this.btnDesign.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.btnDesign.UseVisualStyleBackColor = true;
      this.btnDesign.Click += new System.EventHandler(this.btnDesign_Click);
      // 
      // btnRecord
      // 
      this.btnRecord.Dock = System.Windows.Forms.DockStyle.Top;
      this.btnRecord.Image = global::Ogama.Properties.Resources.Record;
      this.btnRecord.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.btnRecord.Location = new System.Drawing.Point(3, 16);
      this.btnRecord.Name = "btnRecord";
      this.btnRecord.Size = new System.Drawing.Size(116, 40);
      this.btnRecord.TabIndex = 2;
      this.btnRecord.Text = "Record data.";
      this.btnRecord.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.btnRecord.UseVisualStyleBackColor = true;
      this.btnRecord.Click += new System.EventHandler(this.btnRecord_Click);
      // 
      // dialogTop1
      // 
      this.dialogTop1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(237)))));
      this.dialogTop1.BackgroundImage = global::Ogama.Properties.Resources.DialogBackground;
      this.dialogTop1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
      this.dialogTop1.Description = "Please choose the task that you would like to start with:";
      this.dialogTop1.Dock = System.Windows.Forms.DockStyle.Top;
      this.dialogTop1.Location = new System.Drawing.Point(0, 0);
      this.dialogTop1.Logo = global::Ogama.Properties.Resources.Tasks;
      this.dialogTop1.Name = "dialogTop1";
      this.dialogTop1.Size = new System.Drawing.Size(381, 60);
      this.dialogTop1.TabIndex = 3;
      // 
      // tableLayoutPanel1
      // 
      this.tableLayoutPanel1.ColumnCount = 3;
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
      this.tableLayoutPanel1.Controls.Add(this.grpAnalyze, 0, 0);
      this.tableLayoutPanel1.Controls.Add(this.grpImport, 0, 1);
      this.tableLayoutPanel1.Controls.Add(this.grbDesign, 1, 1);
      this.tableLayoutPanel1.Controls.Add(this.grpRecord, 2, 1);
      this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
      this.tableLayoutPanel1.Name = "tableLayoutPanel1";
      this.tableLayoutPanel1.RowCount = 2;
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 66.66666F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
      this.tableLayoutPanel1.Size = new System.Drawing.Size(381, 188);
      this.tableLayoutPanel1.TabIndex = 4;
      // 
      // grpAnalyze
      // 
      this.tableLayoutPanel1.SetColumnSpan(this.grpAnalyze, 3);
      this.grpAnalyze.Controls.Add(this.btnStatistics);
      this.grpAnalyze.Controls.Add(this.btnAOI);
      this.grpAnalyze.Controls.Add(this.btnFixations);
      this.grpAnalyze.Controls.Add(this.btnScanpaths);
      this.grpAnalyze.Controls.Add(this.btnAttentionMap);
      this.grpAnalyze.Controls.Add(this.btnReplay);
      this.grpAnalyze.Dock = System.Windows.Forms.DockStyle.Fill;
      this.grpAnalyze.Location = new System.Drawing.Point(3, 3);
      this.grpAnalyze.Name = "grpAnalyze";
      this.grpAnalyze.Size = new System.Drawing.Size(375, 119);
      this.grpAnalyze.TabIndex = 0;
      this.grpAnalyze.TabStop = false;
      this.grpAnalyze.Text = "Analyze Data";
      // 
      // grpImport
      // 
      this.grpImport.Controls.Add(this.btnImport);
      this.grpImport.Dock = System.Windows.Forms.DockStyle.Fill;
      this.grpImport.Location = new System.Drawing.Point(3, 128);
      this.grpImport.Name = "grpImport";
      this.grpImport.Size = new System.Drawing.Size(120, 57);
      this.grpImport.TabIndex = 1;
      this.grpImport.TabStop = false;
      this.grpImport.Text = "Import";
      // 
      // grbDesign
      // 
      this.grbDesign.Controls.Add(this.btnDesign);
      this.grbDesign.Dock = System.Windows.Forms.DockStyle.Fill;
      this.grbDesign.Location = new System.Drawing.Point(129, 128);
      this.grbDesign.Name = "grbDesign";
      this.grbDesign.Size = new System.Drawing.Size(121, 57);
      this.grbDesign.TabIndex = 2;
      this.grbDesign.TabStop = false;
      this.grbDesign.Text = "Design";
      // 
      // grpRecord
      // 
      this.grpRecord.Controls.Add(this.btnRecord);
      this.grpRecord.Dock = System.Windows.Forms.DockStyle.Fill;
      this.grpRecord.Location = new System.Drawing.Point(256, 128);
      this.grpRecord.Name = "grpRecord";
      this.grpRecord.Size = new System.Drawing.Size(122, 57);
      this.grpRecord.TabIndex = 3;
      this.grpRecord.TabStop = false;
      this.grpRecord.Text = "Record";
      // 
      // btnAttentionMap
      // 
      this.btnAttentionMap.Image = global::Ogama.Properties.Resources.AttentionMapLogo;
      this.btnAttentionMap.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.btnAttentionMap.Location = new System.Drawing.Point(129, 19);
      this.btnAttentionMap.Name = "btnAttentionMap";
      this.btnAttentionMap.Size = new System.Drawing.Size(114, 40);
      this.btnAttentionMap.TabIndex = 2;
      this.btnAttentionMap.Text = "Attention Map";
      this.btnAttentionMap.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.btnAttentionMap.UseVisualStyleBackColor = true;
      this.btnAttentionMap.Click += new System.EventHandler(this.btnAttentionMap_Click);
      // 
      // btnScanpaths
      // 
      this.btnScanpaths.Image = global::Ogama.Properties.Resources.Scanpath;
      this.btnScanpaths.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.btnScanpaths.Location = new System.Drawing.Point(256, 19);
      this.btnScanpaths.Name = "btnScanpaths";
      this.btnScanpaths.Size = new System.Drawing.Size(114, 40);
      this.btnScanpaths.TabIndex = 2;
      this.btnScanpaths.Text = "Scanpaths";
      this.btnScanpaths.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.btnScanpaths.UseVisualStyleBackColor = true;
      this.btnScanpaths.Click += new System.EventHandler(this.btnScanpaths_Click);
      // 
      // btnFixations
      // 
      this.btnFixations.Image = global::Ogama.Properties.Resources.FixationsLogo;
      this.btnFixations.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.btnFixations.Location = new System.Drawing.Point(3, 65);
      this.btnFixations.Name = "btnFixations";
      this.btnFixations.Size = new System.Drawing.Size(114, 40);
      this.btnFixations.TabIndex = 2;
      this.btnFixations.Text = "Fixations";
      this.btnFixations.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.btnFixations.UseVisualStyleBackColor = true;
      this.btnFixations.Click += new System.EventHandler(this.btnFixations_Click);
      // 
      // btnAOI
      // 
      this.btnAOI.Image = global::Ogama.Properties.Resources.AOILogo;
      this.btnAOI.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.btnAOI.Location = new System.Drawing.Point(129, 65);
      this.btnAOI.Name = "btnAOI";
      this.btnAOI.Size = new System.Drawing.Size(114, 40);
      this.btnAOI.TabIndex = 2;
      this.btnAOI.Text = "AOI";
      this.btnAOI.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.btnAOI.UseVisualStyleBackColor = true;
      this.btnAOI.Click += new System.EventHandler(this.btnAOI_Click);
      // 
      // btnStatistics
      // 
      this.btnStatistics.Image = global::Ogama.Properties.Resources.StatisticsLogo;
      this.btnStatistics.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.btnStatistics.Location = new System.Drawing.Point(256, 65);
      this.btnStatistics.Name = "btnStatistics";
      this.btnStatistics.Size = new System.Drawing.Size(114, 40);
      this.btnStatistics.TabIndex = 2;
      this.btnStatistics.Text = "Statistics";
      this.btnStatistics.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.btnStatistics.UseVisualStyleBackColor = true;
      this.btnStatistics.Click += new System.EventHandler(this.btnStatistics_Click);
      // 
      // splitContainer1
      // 
      this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer1.Location = new System.Drawing.Point(0, 0);
      this.splitContainer1.Name = "splitContainer1";
      this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // splitContainer1.Panel1
      // 
      this.splitContainer1.Panel1.Controls.Add(this.dialogTop1);
      // 
      // splitContainer1.Panel2
      // 
      this.splitContainer1.Panel2.Controls.Add(this.tableLayoutPanel1);
      this.splitContainer1.Size = new System.Drawing.Size(381, 251);
      this.splitContainer1.SplitterDistance = 59;
      this.splitContainer1.TabIndex = 5;
      // 
      // StartTask
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(381, 251);
      this.Controls.Add(this.splitContainer1);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "StartTask";
      this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Choose Task ...";
      this.tableLayoutPanel1.ResumeLayout(false);
      this.grpAnalyze.ResumeLayout(false);
      this.grpImport.ResumeLayout(false);
      this.grbDesign.ResumeLayout(false);
      this.grpRecord.ResumeLayout(false);
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
      this.splitContainer1.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Button btnReplay;
    private System.Windows.Forms.Button btnImport;
    private System.Windows.Forms.Button btnDesign;
    private System.Windows.Forms.Button btnRecord;
    private DialogTop dialogTop1;
    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    private System.Windows.Forms.GroupBox grpAnalyze;
    private System.Windows.Forms.GroupBox grpImport;
    private System.Windows.Forms.GroupBox grbDesign;
    private System.Windows.Forms.GroupBox grpRecord;
    private System.Windows.Forms.Button btnStatistics;
    private System.Windows.Forms.Button btnAOI;
    private System.Windows.Forms.Button btnFixations;
    private System.Windows.Forms.Button btnScanpaths;
    private System.Windows.Forms.Button btnAttentionMap;
    private System.Windows.Forms.SplitContainer splitContainer1;
  }
}