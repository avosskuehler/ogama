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
      this.btnAnalyze = new System.Windows.Forms.Button();
      this.btnImport = new System.Windows.Forms.Button();
      this.btnDesign = new System.Windows.Forms.Button();
      this.btnRecord = new System.Windows.Forms.Button();
      this.dialogTop1 = new DialogTop();
      this.SuspendLayout();
      // 
      // btnAnalyze
      // 
      this.btnAnalyze.Image = global::Ogama.Properties.Resources.Run;
      this.btnAnalyze.Location = new System.Drawing.Point(90, 81);
      this.btnAnalyze.Name = "btnAnalyze";
      this.btnAnalyze.Size = new System.Drawing.Size(156, 40);
      this.btnAnalyze.TabIndex = 2;
      this.btnAnalyze.Text = "Analyze existing data.";
      this.btnAnalyze.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.btnAnalyze.UseVisualStyleBackColor = true;
      this.btnAnalyze.Click += new System.EventHandler(this.btnAnalyze_Click);
      // 
      // btnImport
      // 
      this.btnImport.Image = global::Ogama.Properties.Resources.ImportData;
      this.btnImport.Location = new System.Drawing.Point(90, 129);
      this.btnImport.Name = "btnImport";
      this.btnImport.Size = new System.Drawing.Size(156, 40);
      this.btnImport.TabIndex = 2;
      this.btnImport.Text = "Import data from log files.";
      this.btnImport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.btnImport.UseVisualStyleBackColor = true;
      this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
      // 
      // btnDesign
      // 
      this.btnDesign.Image = global::Ogama.Properties.Resources.Design;
      this.btnDesign.Location = new System.Drawing.Point(90, 177);
      this.btnDesign.Name = "btnDesign";
      this.btnDesign.Size = new System.Drawing.Size(156, 40);
      this.btnDesign.TabIndex = 2;
      this.btnDesign.Text = "Design slideshow.";
      this.btnDesign.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.btnDesign.UseVisualStyleBackColor = true;
      this.btnDesign.Click += new System.EventHandler(this.btnDesign_Click);
      // 
      // btnRecord
      // 
      this.btnRecord.Image = global::Ogama.Properties.Resources.Record;
      this.btnRecord.Location = new System.Drawing.Point(90, 225);
      this.btnRecord.Name = "btnRecord";
      this.btnRecord.Size = new System.Drawing.Size(156, 40);
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
      this.dialogTop1.Size = new System.Drawing.Size(336, 60);
      this.dialogTop1.TabIndex = 3;
      // 
      // StartTask
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(336, 285);
      this.Controls.Add(this.dialogTop1);
      this.Controls.Add(this.btnRecord);
      this.Controls.Add(this.btnDesign);
      this.Controls.Add(this.btnImport);
      this.Controls.Add(this.btnAnalyze);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "StartTask";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Choose Task ...";
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Button btnAnalyze;
    private System.Windows.Forms.Button btnImport;
    private System.Windows.Forms.Button btnDesign;
    private System.Windows.Forms.Button btnRecord;
    private DialogTop dialogTop1;
  }
}