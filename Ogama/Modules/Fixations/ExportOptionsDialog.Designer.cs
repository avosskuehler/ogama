namespace Ogama.Modules.Fixations
{
  using Ogama.Modules.Common.Controls;

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
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExportOptionsDialog));
      this.btnOK = new System.Windows.Forms.Button();
      this.btnCancel = new System.Windows.Forms.Button();
      this.dialogTop1 = new DialogTop();
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
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      this.splitContainer2 = new System.Windows.Forms.SplitContainer();
      this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
      this.groupBox24 = new System.Windows.Forms.GroupBox();
      this.trvSubjects = new OgamaControls.CheckboxTreeView(this.components);
      this.imlTreeViewSubjects = new System.Windows.Forms.ImageList(this.components);
      this.grbTrials = new System.Windows.Forms.GroupBox();
      this.trvTrialsDefault = new OgamaControls.CheckboxTreeView(this.components);
      this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
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
      this.groupBox1.SuspendLayout();
      this.groupBox2.SuspendLayout();
      this.groupBox3.SuspendLayout();
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      this.splitContainer2.Panel1.SuspendLayout();
      this.splitContainer2.Panel2.SuspendLayout();
      this.splitContainer2.SuspendLayout();
      this.tableLayoutPanel2.SuspendLayout();
      this.groupBox24.SuspendLayout();
      this.grbTrials.SuspendLayout();
      this.tableLayoutPanel1.SuspendLayout();
      this.SuspendLayout();
      // 
      // btnOK
      // 
      this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.btnOK.Location = new System.Drawing.Point(290, 9);
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
      this.btnCancel.Location = new System.Drawing.Point(371, 9);
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
      this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.groupBox1.Location = new System.Drawing.Point(3, 3);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(104, 94);
      this.groupBox1.TabIndex = 14;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Export data from";
      // 
      // groupBox2
      // 
      this.groupBox2.Controls.Add(this.rdbFixations);
      this.groupBox2.Controls.Add(this.rdbSaccades);
      this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.groupBox2.Location = new System.Drawing.Point(113, 3);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new System.Drawing.Size(104, 94);
      this.groupBox2.TabIndex = 15;
      this.groupBox2.TabStop = false;
      this.groupBox2.Text = "Type";
      // 
      // groupBox3
      // 
      this.groupBox3.Controls.Add(this.chbAOIInfo);
      this.groupBox3.Controls.Add(this.chbTrialDetail);
      this.groupBox3.Controls.Add(this.chbSubjectDetail);
      this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
      this.groupBox3.Location = new System.Drawing.Point(223, 3);
      this.groupBox3.Name = "groupBox3";
      this.groupBox3.Size = new System.Drawing.Size(223, 94);
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
      // splitContainer1
      // 
      this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
      this.splitContainer1.IsSplitterFixed = true;
      this.splitContainer1.Location = new System.Drawing.Point(0, 60);
      this.splitContainer1.Name = "splitContainer1";
      this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // splitContainer1.Panel1
      // 
      this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
      // 
      // splitContainer1.Panel2
      // 
      this.splitContainer1.Panel2.Controls.Add(this.btnCancel);
      this.splitContainer1.Panel2.Controls.Add(this.btnOK);
      this.splitContainer1.Panel2MinSize = 35;
      this.splitContainer1.Size = new System.Drawing.Size(449, 262);
      this.splitContainer1.SplitterDistance = 223;
      this.splitContainer1.TabIndex = 17;
      // 
      // splitContainer2
      // 
      this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
      this.splitContainer2.IsSplitterFixed = true;
      this.splitContainer2.Location = new System.Drawing.Point(0, 0);
      this.splitContainer2.Name = "splitContainer2";
      this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // splitContainer2.Panel1
      // 
      this.splitContainer2.Panel1.Controls.Add(this.tableLayoutPanel2);
      // 
      // splitContainer2.Panel2
      // 
      this.splitContainer2.Panel2.Controls.Add(this.tableLayoutPanel1);
      this.splitContainer2.Panel2MinSize = 100;
      this.splitContainer2.Size = new System.Drawing.Size(449, 223);
      this.splitContainer2.SplitterDistance = 119;
      this.splitContainer2.TabIndex = 0;
      // 
      // tableLayoutPanel2
      // 
      this.tableLayoutPanel2.ColumnCount = 2;
      this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
      this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
      this.tableLayoutPanel2.Controls.Add(this.groupBox24, 0, 0);
      this.tableLayoutPanel2.Controls.Add(this.grbTrials, 1, 0);
      this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
      this.tableLayoutPanel2.Name = "tableLayoutPanel2";
      this.tableLayoutPanel2.RowCount = 1;
      this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
      this.tableLayoutPanel2.Size = new System.Drawing.Size(449, 119);
      this.tableLayoutPanel2.TabIndex = 0;
      // 
      // groupBox24
      // 
      this.groupBox24.Controls.Add(this.trvSubjects);
      this.groupBox24.Dock = System.Windows.Forms.DockStyle.Fill;
      this.groupBox24.Location = new System.Drawing.Point(3, 3);
      this.groupBox24.Name = "groupBox24";
      this.groupBox24.Size = new System.Drawing.Size(218, 113);
      this.groupBox24.TabIndex = 16;
      this.groupBox24.TabStop = false;
      this.groupBox24.Text = "Subjects";
      // 
      // trvSubjects
      // 
      this.trvSubjects.CheckBoxes = true;
      this.trvSubjects.Dock = System.Windows.Forms.DockStyle.Fill;
      this.trvSubjects.ImageIndex = 0;
      this.trvSubjects.ImageList = this.imlTreeViewSubjects;
      this.trvSubjects.Location = new System.Drawing.Point(3, 16);
      this.trvSubjects.Name = "trvSubjects";
      this.trvSubjects.SelectedImageIndex = 0;
      this.trvSubjects.Size = new System.Drawing.Size(212, 94);
      this.trvSubjects.TabIndex = 0;
      this.trvSubjects.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.trvSubjects_AfterCheck);
      // 
      // imlTreeViewSubjects
      // 
      this.imlTreeViewSubjects.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imlTreeViewSubjects.ImageStream")));
      this.imlTreeViewSubjects.TransparentColor = System.Drawing.Color.Transparent;
      this.imlTreeViewSubjects.Images.SetKeyName(0, "Categorie");
      this.imlTreeViewSubjects.Images.SetKeyName(1, "Subject");
      // 
      // grbTrials
      // 
      this.grbTrials.Controls.Add(this.trvTrialsDefault);
      this.grbTrials.Dock = System.Windows.Forms.DockStyle.Fill;
      this.grbTrials.Location = new System.Drawing.Point(227, 3);
      this.grbTrials.Name = "grbTrials";
      this.grbTrials.Size = new System.Drawing.Size(219, 113);
      this.grbTrials.TabIndex = 17;
      this.grbTrials.TabStop = false;
      this.grbTrials.Text = "Trials";
      // 
      // trvTrialsDefault
      // 
      this.trvTrialsDefault.CheckBoxes = true;
      this.trvTrialsDefault.Dock = System.Windows.Forms.DockStyle.Fill;
      this.trvTrialsDefault.Location = new System.Drawing.Point(3, 16);
      this.trvTrialsDefault.Name = "trvTrialsDefault";
      this.trvTrialsDefault.Size = new System.Drawing.Size(213, 94);
      this.trvTrialsDefault.TabIndex = 4;
      this.trvTrialsDefault.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.trvTrialsDefault_AfterCheck);
      // 
      // tableLayoutPanel1
      // 
      this.tableLayoutPanel1.ColumnCount = 3;
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 110F));
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 110F));
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
      this.tableLayoutPanel1.Controls.Add(this.groupBox3, 2, 0);
      this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 0);
      this.tableLayoutPanel1.Controls.Add(this.groupBox2, 1, 0);
      this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
      this.tableLayoutPanel1.Name = "tableLayoutPanel1";
      this.tableLayoutPanel1.RowCount = 1;
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel1.Size = new System.Drawing.Size(449, 100);
      this.tableLayoutPanel1.TabIndex = 1;
      // 
      // ExportOptionsDialog
      // 
      this.AcceptButton = this.btnOK;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.btnCancel;
      this.ClientSize = new System.Drawing.Size(449, 322);
      this.Controls.Add(this.splitContainer1);
      this.Controls.Add(this.dialogTop1);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "ExportOptionsDialog";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Please choose option";
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
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.groupBox2.ResumeLayout(false);
      this.groupBox2.PerformLayout();
      this.groupBox3.ResumeLayout(false);
      this.groupBox3.PerformLayout();
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel2.ResumeLayout(false);
      this.splitContainer1.ResumeLayout(false);
      this.splitContainer2.Panel1.ResumeLayout(false);
      this.splitContainer2.Panel2.ResumeLayout(false);
      this.splitContainer2.ResumeLayout(false);
      this.tableLayoutPanel2.ResumeLayout(false);
      this.groupBox24.ResumeLayout(false);
      this.grbTrials.ResumeLayout(false);
      this.tableLayoutPanel1.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Button btnOK;
    private System.Windows.Forms.Button btnCancel;
    private DialogTop dialogTop1;
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
    private System.Windows.Forms.SplitContainer splitContainer1;
    private System.Windows.Forms.SplitContainer splitContainer2;
    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
    private System.Windows.Forms.GroupBox groupBox24;
    private OgamaControls.CheckboxTreeView trvSubjects;
    private System.Windows.Forms.GroupBox grbTrials;
    private OgamaControls.CheckboxTreeView trvTrialsDefault;
    private System.Windows.Forms.ImageList imlTreeViewSubjects;
  }
}