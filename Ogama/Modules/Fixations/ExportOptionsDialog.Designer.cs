namespace Ogama.Modules.Fixations
{
  partial class ExportOptionsDialog
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExportOptionsDialog));
      this.btnOK = new System.Windows.Forms.Button();
      this.btnCancel = new System.Windows.Forms.Button();
      this.dialogTop1 = new Ogama.Modules.Common.DialogTop();
      this.rdbFixations = new System.Windows.Forms.RadioButton();
      this.rdbSaccades = new System.Windows.Forms.RadioButton();
      this.chbGaze = new System.Windows.Forms.CheckBox();
      this.chbMouse = new System.Windows.Forms.CheckBox();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.groupBox2 = new System.Windows.Forms.GroupBox();
      this.groupBox3 = new System.Windows.Forms.GroupBox();
      this.chbAOIInfo = new System.Windows.Forms.CheckBox();
      this.chbTrialDetail = new System.Windows.Forms.CheckBox();
      this.chbSubjectDetail = new System.Windows.Forms.CheckBox();
      this.sfdExport = new System.Windows.Forms.SaveFileDialog();
      this.groupBox1.SuspendLayout();
      this.groupBox2.SuspendLayout();
      this.groupBox3.SuspendLayout();
      this.SuspendLayout();
      // 
      // btnOK
      // 
      this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.btnOK.Location = new System.Drawing.Point(274, 164);
      this.btnOK.Name = "btnOK";
      this.btnOK.Size = new System.Drawing.Size(75, 23);
      this.btnOK.TabIndex = 9;
      this.btnOK.Text = "&OK";
      this.btnOK.UseVisualStyleBackColor = true;
      this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
      // 
      // btnCancel
      // 
      this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnCancel.Location = new System.Drawing.Point(365, 164);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(75, 23);
      this.btnCancel.TabIndex = 10;
      this.btnCancel.Text = "&Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      // 
      // dialogTop1
      // 
      this.dialogTop1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(237)))));
      this.dialogTop1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("dialogTop1.BackgroundImage")));
      this.dialogTop1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
      this.dialogTop1.Description = "Please specify what to export from the fixation tables.";
      this.dialogTop1.Dock = System.Windows.Forms.DockStyle.Top;
      this.dialogTop1.Location = new System.Drawing.Point(0, 0);
      this.dialogTop1.Logo = global::Ogama.Properties.Resources.StatisticsLogo;
      this.dialogTop1.Name = "dialogTop1";
      this.dialogTop1.Size = new System.Drawing.Size(449, 60);
      this.dialogTop1.TabIndex = 11;
      // 
      // rdbFixations
      // 
      this.rdbFixations.AutoSize = true;
      this.rdbFixations.Checked = true;
      this.rdbFixations.Location = new System.Drawing.Point(6, 19);
      this.rdbFixations.Name = "rdbFixations";
      this.rdbFixations.Size = new System.Drawing.Size(66, 17);
      this.rdbFixations.TabIndex = 12;
      this.rdbFixations.TabStop = true;
      this.rdbFixations.Text = "Fixations";
      this.rdbFixations.UseVisualStyleBackColor = true;
      // 
      // rdbSaccades
      // 
      this.rdbSaccades.AutoSize = true;
      this.rdbSaccades.Location = new System.Drawing.Point(6, 42);
      this.rdbSaccades.Name = "rdbSaccades";
      this.rdbSaccades.Size = new System.Drawing.Size(73, 17);
      this.rdbSaccades.TabIndex = 12;
      this.rdbSaccades.Text = "Saccades";
      this.rdbSaccades.UseVisualStyleBackColor = true;
      // 
      // chbGaze
      // 
      this.chbGaze.AutoSize = true;
      this.chbGaze.Checked = true;
      this.chbGaze.CheckState = System.Windows.Forms.CheckState.Checked;
      this.chbGaze.Location = new System.Drawing.Point(6, 19);
      this.chbGaze.Name = "chbGaze";
      this.chbGaze.Size = new System.Drawing.Size(51, 17);
      this.chbGaze.TabIndex = 13;
      this.chbGaze.Text = "Gaze";
      this.chbGaze.UseVisualStyleBackColor = true;
      this.chbGaze.CheckedChanged += new System.EventHandler(this.chbGazeMouse_CheckedChanged);
      // 
      // chbMouse
      // 
      this.chbMouse.AutoSize = true;
      this.chbMouse.Location = new System.Drawing.Point(6, 41);
      this.chbMouse.Name = "chbMouse";
      this.chbMouse.Size = new System.Drawing.Size(58, 17);
      this.chbMouse.TabIndex = 13;
      this.chbMouse.Text = "Mouse";
      this.chbMouse.UseVisualStyleBackColor = true;
      this.chbMouse.CheckedChanged += new System.EventHandler(this.chbGazeMouse_CheckedChanged);
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.chbGaze);
      this.groupBox1.Controls.Add(this.chbMouse);
      this.groupBox1.Location = new System.Drawing.Point(12, 66);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(103, 92);
      this.groupBox1.TabIndex = 14;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Export data from";
      // 
      // groupBox2
      // 
      this.groupBox2.Controls.Add(this.rdbFixations);
      this.groupBox2.Controls.Add(this.rdbSaccades);
      this.groupBox2.Location = new System.Drawing.Point(121, 66);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new System.Drawing.Size(101, 92);
      this.groupBox2.TabIndex = 15;
      this.groupBox2.TabStop = false;
      this.groupBox2.Text = "Type";
      // 
      // groupBox3
      // 
      this.groupBox3.Controls.Add(this.chbAOIInfo);
      this.groupBox3.Controls.Add(this.chbTrialDetail);
      this.groupBox3.Controls.Add(this.chbSubjectDetail);
      this.groupBox3.Location = new System.Drawing.Point(228, 66);
      this.groupBox3.Name = "groupBox3";
      this.groupBox3.Size = new System.Drawing.Size(211, 92);
      this.groupBox3.TabIndex = 16;
      this.groupBox3.TabStop = false;
      this.groupBox3.Text = "Options";
      // 
      // chbAOIInfo
      // 
      this.chbAOIInfo.AutoSize = true;
      this.chbAOIInfo.Location = new System.Drawing.Point(6, 66);
      this.chbAOIInfo.Name = "chbAOIInfo";
      this.chbAOIInfo.Size = new System.Drawing.Size(161, 17);
      this.chbAOIInfo.TabIndex = 0;
      this.chbAOIInfo.Text = "include hitted AOI and group";
      this.chbAOIInfo.UseVisualStyleBackColor = true;
      // 
      // chbTrialDetail
      // 
      this.chbTrialDetail.AutoSize = true;
      this.chbTrialDetail.Location = new System.Drawing.Point(6, 43);
      this.chbTrialDetail.Name = "chbTrialDetail";
      this.chbTrialDetail.Size = new System.Drawing.Size(191, 17);
      this.chbTrialDetail.TabIndex = 0;
      this.chbTrialDetail.Text = "include trial name, category, slidenr";
      this.chbTrialDetail.UseVisualStyleBackColor = true;
      // 
      // chbSubjectDetail
      // 
      this.chbSubjectDetail.AutoSize = true;
      this.chbSubjectDetail.Location = new System.Drawing.Point(6, 20);
      this.chbSubjectDetail.Name = "chbSubjectDetail";
      this.chbSubjectDetail.Size = new System.Drawing.Size(191, 17);
      this.chbSubjectDetail.TabIndex = 0;
      this.chbSubjectDetail.Text = "include detailed subject information";
      this.chbSubjectDetail.UseVisualStyleBackColor = true;
      // 
      // sfdExport
      // 
      this.sfdExport.DefaultExt = "txt";
      this.sfdExport.FileName = "*.txt";
      this.sfdExport.Filter = "Text files|*.txt";
      this.sfdExport.Title = "Export fixations table ...";
      // 
      // ExportOptionsDialog
      // 
      this.AcceptButton = this.btnOK;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.btnCancel;
      this.ClientSize = new System.Drawing.Size(449, 196);
      this.Controls.Add(this.groupBox3);
      this.Controls.Add(this.groupBox2);
      this.Controls.Add(this.groupBox1);
      this.Controls.Add(this.dialogTop1);
      this.Controls.Add(this.btnCancel);
      this.Controls.Add(this.btnOK);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "ExportOptionsDialog";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Please choose option";
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.groupBox2.ResumeLayout(false);
      this.groupBox2.PerformLayout();
      this.groupBox3.ResumeLayout(false);
      this.groupBox3.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Button btnOK;
    private System.Windows.Forms.Button btnCancel;
    private Ogama.Modules.Common.DialogTop dialogTop1;
    private System.Windows.Forms.RadioButton rdbFixations;
    private System.Windows.Forms.RadioButton rdbSaccades;
    private System.Windows.Forms.CheckBox chbGaze;
    private System.Windows.Forms.CheckBox chbMouse;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.GroupBox groupBox2;
    private System.Windows.Forms.GroupBox groupBox3;
    private System.Windows.Forms.CheckBox chbSubjectDetail;
    private System.Windows.Forms.CheckBox chbTrialDetail;
    private System.Windows.Forms.SaveFileDialog sfdExport;
    private System.Windows.Forms.CheckBox chbAOIInfo;
  }
}