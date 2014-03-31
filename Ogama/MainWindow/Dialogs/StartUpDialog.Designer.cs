namespace Ogama.MainWindow.Dialogs
{
  using Ogama.Modules.Common.Controls;

  partial class StartUpDialog
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StartUpDialog));
      this.rdbRecentProjects = new System.Windows.Forms.RadioButton();
      this.rdbNewProject = new System.Windows.Forms.RadioButton();
      this.btnOK = new System.Windows.Forms.Button();
      this.btnCancel = new System.Windows.Forms.Button();
      this.lsbRecentProjects = new System.Windows.Forms.ListBox();
      this.btnClearRecentFilesList = new System.Windows.Forms.Button();
      this.pictureBox2 = new System.Windows.Forms.PictureBox();
      this.pictureBox3 = new System.Windows.Forms.PictureBox();
      this.dialogTop1 = new Ogama.Modules.Common.Controls.DialogTop();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
      this.SuspendLayout();
      // 
      // rdbRecentProjects
      // 
      this.rdbRecentProjects.AutoSize = true;
      this.rdbRecentProjects.Checked = true;
      this.rdbRecentProjects.Location = new System.Drawing.Point(60, 66);
      this.rdbRecentProjects.Name = "rdbRecentProjects";
      this.rdbRecentProjects.Size = new System.Drawing.Size(120, 17);
      this.rdbRecentProjects.TabIndex = 2;
      this.rdbRecentProjects.TabStop = true;
      this.rdbRecentProjects.Text = "open recent project ";
      this.rdbRecentProjects.UseVisualStyleBackColor = true;
      this.rdbRecentProjects.CheckedChanged += new System.EventHandler(this.rdbRecentProjects_CheckedChanged);
      // 
      // rdbNewProject
      // 
      this.rdbNewProject.AutoSize = true;
      this.rdbNewProject.Location = new System.Drawing.Point(60, 240);
      this.rdbNewProject.Name = "rdbNewProject";
      this.rdbNewProject.Size = new System.Drawing.Size(122, 17);
      this.rdbNewProject.TabIndex = 3;
      this.rdbNewProject.TabStop = true;
      this.rdbNewProject.Text = "create a new project";
      this.rdbNewProject.UseVisualStyleBackColor = true;
      this.rdbNewProject.CheckedChanged += new System.EventHandler(this.rdbNewProject_CheckedChanged);
      // 
      // btnOK
      // 
      this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.btnOK.Location = new System.Drawing.Point(186, 285);
      this.btnOK.Name = "btnOK";
      this.btnOK.Size = new System.Drawing.Size(75, 23);
      this.btnOK.TabIndex = 4;
      this.btnOK.Text = "&OK";
      this.btnOK.UseVisualStyleBackColor = true;
      // 
      // btnCancel
      // 
      this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnCancel.Location = new System.Drawing.Point(267, 285);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(75, 23);
      this.btnCancel.TabIndex = 5;
      this.btnCancel.Text = "&Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      // 
      // lsbRecentProjects
      // 
      this.lsbRecentProjects.FormattingEnabled = true;
      this.lsbRecentProjects.Location = new System.Drawing.Point(60, 89);
      this.lsbRecentProjects.Name = "lsbRecentProjects";
      this.lsbRecentProjects.Size = new System.Drawing.Size(287, 108);
      this.lsbRecentProjects.TabIndex = 6;
      this.lsbRecentProjects.SelectedIndexChanged += new System.EventHandler(this.lsbRecentProjects_SelectedIndexChanged);
      this.lsbRecentProjects.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lsbRecentProjects_MouseDoubleClick);
      // 
      // btnClearRecentFilesList
      // 
      this.btnClearRecentFilesList.Image = global::Ogama.Properties.Resources.DeleteHS;
      this.btnClearRecentFilesList.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.btnClearRecentFilesList.Location = new System.Drawing.Point(60, 203);
      this.btnClearRecentFilesList.Name = "btnClearRecentFilesList";
      this.btnClearRecentFilesList.Size = new System.Drawing.Size(157, 23);
      this.btnClearRecentFilesList.TabIndex = 7;
      this.btnClearRecentFilesList.Text = "clear recent files list";
      this.btnClearRecentFilesList.UseVisualStyleBackColor = true;
      this.btnClearRecentFilesList.Click += new System.EventHandler(this.btnClearRecentFilesList_Click);
      // 
      // pictureBox2
      // 
      this.pictureBox2.Image = global::Ogama.Properties.Resources.folderopen;
      this.pictureBox2.Location = new System.Drawing.Point(12, 66);
      this.pictureBox2.Name = "pictureBox2";
      this.pictureBox2.Size = new System.Drawing.Size(34, 34);
      this.pictureBox2.TabIndex = 0;
      this.pictureBox2.TabStop = false;
      // 
      // pictureBox3
      // 
      this.pictureBox3.BackColor = System.Drawing.Color.Transparent;
      this.pictureBox3.Image = global::Ogama.Properties.Resources.OgamaNew;
      this.pictureBox3.Location = new System.Drawing.Point(12, 240);
      this.pictureBox3.Name = "pictureBox3";
      this.pictureBox3.Size = new System.Drawing.Size(34, 41);
      this.pictureBox3.TabIndex = 0;
      this.pictureBox3.TabStop = false;
      // 
      // dialogTop1
      // 
      this.dialogTop1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(237)))));
      this.dialogTop1.BackgroundImage = global::Ogama.Properties.Resources.DialogBackground;
      this.dialogTop1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
      this.dialogTop1.Description = "What would you like to do ?";
      this.dialogTop1.Dock = System.Windows.Forms.DockStyle.Top;
      this.dialogTop1.Location = new System.Drawing.Point(0, 0);
      this.dialogTop1.Logo = null;
      this.dialogTop1.Name = "dialogTop1";
      this.dialogTop1.Size = new System.Drawing.Size(354, 60);
      this.dialogTop1.TabIndex = 8;
      // 
      // StartUpDialog
      // 
      this.AcceptButton = this.btnOK;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.btnCancel;
      this.ClientSize = new System.Drawing.Size(354, 320);
      this.Controls.Add(this.btnCancel);
      this.Controls.Add(this.btnOK);
      this.Controls.Add(this.dialogTop1);
      this.Controls.Add(this.pictureBox3);
      this.Controls.Add(this.pictureBox2);
      this.Controls.Add(this.rdbNewProject);
      this.Controls.Add(this.btnClearRecentFilesList);
      this.Controls.Add(this.lsbRecentProjects);
      this.Controls.Add(this.rdbRecentProjects);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "StartUpDialog";
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Welcome to Ogama ...";
      this.Load += new System.EventHandler(this.frmStartUpDlg_Load);
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.RadioButton rdbRecentProjects;
    private System.Windows.Forms.RadioButton rdbNewProject;
    private System.Windows.Forms.Button btnOK;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.ListBox lsbRecentProjects;
    private System.Windows.Forms.PictureBox pictureBox2;
    private System.Windows.Forms.PictureBox pictureBox3;
    private System.Windows.Forms.Button btnClearRecentFilesList;
    private DialogTop dialogTop1;
  }
}