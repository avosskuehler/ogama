namespace Ogama.MainWindow.Dialogs
{
  using Ogama.Modules.Common.Controls;

  partial class NewExperiment
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewExperiment));
      this.btnOK = new System.Windows.Forms.Button();
      this.btnCancel = new System.Windows.Forms.Button();
      this.fbdExperiment = new System.Windows.Forms.FolderBrowserDialog();
      this.txbExperimentName = new System.Windows.Forms.TextBox();
      this.txbParentFolder = new System.Windows.Forms.TextBox();
      this.btnOpenFolder = new System.Windows.Forms.Button();
      this.lblDescription = new System.Windows.Forms.Label();
      this.lblFolder = new System.Windows.Forms.Label();
      this.lblName = new System.Windows.Forms.Label();
      this.dialogTop1 = new Ogama.Modules.Common.Controls.DialogTop();
      this.SuspendLayout();
      // 
      // btnOK
      // 
      this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.btnOK.Location = new System.Drawing.Point(237, 181);
      this.btnOK.Name = "btnOK";
      this.btnOK.Size = new System.Drawing.Size(75, 23);
      this.btnOK.TabIndex = 1;
      this.btnOK.Text = "&OK";
      this.btnOK.UseVisualStyleBackColor = true;
      // 
      // btnCancel
      // 
      this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnCancel.Location = new System.Drawing.Point(318, 181);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(75, 23);
      this.btnCancel.TabIndex = 1;
      this.btnCancel.Text = "&Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      // 
      // fbdExperiment
      // 
      this.fbdExperiment.Description = "Select or create the folder where the new experiment should be located.";
      // 
      // txbExperimentName
      // 
      this.txbExperimentName.Location = new System.Drawing.Point(83, 154);
      this.txbExperimentName.Name = "txbExperimentName";
      this.txbExperimentName.Size = new System.Drawing.Size(310, 20);
      this.txbExperimentName.TabIndex = 2;
      // 
      // txbParentFolder
      // 
      this.txbParentFolder.Location = new System.Drawing.Point(83, 107);
      this.txbParentFolder.Multiline = true;
      this.txbParentFolder.Name = "txbParentFolder";
      this.txbParentFolder.Size = new System.Drawing.Size(310, 41);
      this.txbParentFolder.TabIndex = 3;
      // 
      // btnOpenFolder
      // 
      this.btnOpenFolder.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
      this.btnOpenFolder.Image = global::Ogama.Properties.Resources.openfolderHS;
      this.btnOpenFolder.Location = new System.Drawing.Point(399, 107);
      this.btnOpenFolder.Name = "btnOpenFolder";
      this.btnOpenFolder.Size = new System.Drawing.Size(23, 23);
      this.btnOpenFolder.TabIndex = 4;
      this.btnOpenFolder.UseVisualStyleBackColor = true;
      this.btnOpenFolder.Click += new System.EventHandler(this.btnOpenFolder_Click);
      // 
      // lblDescription
      // 
      this.lblDescription.AutoSize = true;
      this.lblDescription.Location = new System.Drawing.Point(13, 72);
      this.lblDescription.Name = "lblDescription";
      this.lblDescription.Size = new System.Drawing.Size(382, 26);
      this.lblDescription.TabIndex = 5;
      this.lblDescription.Text = "A folder with the experiments name will be created in the selected parent folder." +
    " \r\nTherein all files and folders that are used for the experiment are located.";
      // 
      // lblFolder
      // 
      this.lblFolder.AutoSize = true;
      this.lblFolder.Location = new System.Drawing.Point(13, 107);
      this.lblFolder.Name = "lblFolder";
      this.lblFolder.Size = new System.Drawing.Size(67, 13);
      this.lblFolder.TabIndex = 6;
      this.lblFolder.Text = "Parent folder";
      // 
      // lblName
      // 
      this.lblName.Location = new System.Drawing.Point(13, 152);
      this.lblName.Name = "lblName";
      this.lblName.Size = new System.Drawing.Size(67, 28);
      this.lblName.TabIndex = 6;
      this.lblName.Text = "Experiment name";
      // 
      // dialogTop1
      // 
      this.dialogTop1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(237)))));
      this.dialogTop1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("dialogTop1.BackgroundImage")));
      this.dialogTop1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
      this.dialogTop1.Description = "Specify the parent folder and a name for the experiment.";
      this.dialogTop1.Dock = System.Windows.Forms.DockStyle.Top;
      this.dialogTop1.Location = new System.Drawing.Point(0, 0);
      this.dialogTop1.Logo = global::Ogama.Properties.Resources.OgamaNew;
      this.dialogTop1.Name = "dialogTop1";
      this.dialogTop1.Size = new System.Drawing.Size(438, 60);
      this.dialogTop1.TabIndex = 7;
      // 
      // NewExperiment
      // 
      this.AcceptButton = this.btnOK;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.btnCancel;
      this.ClientSize = new System.Drawing.Size(438, 214);
      this.Controls.Add(this.dialogTop1);
      this.Controls.Add(this.lblName);
      this.Controls.Add(this.lblFolder);
      this.Controls.Add(this.lblDescription);
      this.Controls.Add(this.btnOpenFolder);
      this.Controls.Add(this.txbParentFolder);
      this.Controls.Add(this.txbExperimentName);
      this.Controls.Add(this.btnCancel);
      this.Controls.Add(this.btnOK);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "NewExperiment";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Create new experiment ...";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmNewExperimentDlg_FormClosing);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button btnOK;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.FolderBrowserDialog fbdExperiment;
    private System.Windows.Forms.TextBox txbExperimentName;
    private System.Windows.Forms.TextBox txbParentFolder;
    private System.Windows.Forms.Button btnOpenFolder;
    private System.Windows.Forms.Label lblDescription;
    private System.Windows.Forms.Label lblFolder;
    private System.Windows.Forms.Label lblName;
    private DialogTop dialogTop1;
  }
}