namespace Ogama.MainWindow.Dialogs
{
  using Ogama.Modules.Common.Controls;

  partial class CheckForUpdates
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CheckForUpdates));
      this.btnOK = new System.Windows.Forms.Button();
      this.lblResult = new System.Windows.Forms.Label();
      this.llbDownloadUri = new System.Windows.Forms.LinkLabel();
      this.pgbDownload = new System.Windows.Forms.ProgressBar();
      this.lblDownloadStatus = new System.Windows.Forms.Label();
      this.lblYourVersion = new System.Windows.Forms.Label();
      this.lblYourVersionNumber = new System.Windows.Forms.Label();
      this.lblCurrentVersion = new System.Windows.Forms.Label();
      this.lblCurrentVersionNumber = new System.Windows.Forms.Label();
      this.btnCancel = new System.Windows.Forms.Button();
      this.lblInstallHint = new System.Windows.Forms.Label();
      this.pcbOgama = new System.Windows.Forms.PictureBox();
      this.lblAdditionalHint = new System.Windows.Forms.Label();
      this.tmrConnect = new System.Windows.Forms.Timer(this.components);
      this.grbResults = new System.Windows.Forms.GroupBox();
      this.dialogTop1 = new DialogTop();
      ((System.ComponentModel.ISupportInitialize)(this.pcbOgama)).BeginInit();
      this.grbResults.SuspendLayout();
      this.SuspendLayout();
      // 
      // btnOK
      // 
      this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnOK.AutoSize = true;
      this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.btnOK.Location = new System.Drawing.Point(201, 281);
      this.btnOK.Name = "btnOK";
      this.btnOK.Size = new System.Drawing.Size(76, 23);
      this.btnOK.TabIndex = 3;
      this.btnOK.Text = "OK";
      this.btnOK.UseVisualStyleBackColor = true;
      // 
      // lblResult
      // 
      this.lblResult.AutoSize = true;
      this.lblResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblResult.Location = new System.Drawing.Point(6, 16);
      this.lblResult.Name = "lblResult";
      this.lblResult.Size = new System.Drawing.Size(11, 13);
      this.lblResult.TabIndex = 5;
      this.lblResult.Text = " ";
      // 
      // llbDownloadUri
      // 
      this.llbDownloadUri.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.llbDownloadUri.AutoSize = true;
      this.llbDownloadUri.Location = new System.Drawing.Point(54, 259);
      this.llbDownloadUri.Name = "llbDownloadUri";
      this.llbDownloadUri.Size = new System.Drawing.Size(243, 13);
      this.llbDownloadUri.TabIndex = 6;
      this.llbDownloadUri.TabStop = true;
      this.llbDownloadUri.Text = "http://www.ogama.net";
      this.llbDownloadUri.Visible = false;
      this.llbDownloadUri.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llbDownloadUri_LinkClicked);
      // 
      // pgbDownload
      // 
      this.pgbDownload.Location = new System.Drawing.Point(146, 100);
      this.pgbDownload.Name = "pgbDownload";
      this.pgbDownload.Size = new System.Drawing.Size(212, 16);
      this.pgbDownload.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
      this.pgbDownload.TabIndex = 10;
      // 
      // lblDownloadStatus
      // 
      this.lblDownloadStatus.AutoSize = true;
      this.lblDownloadStatus.Location = new System.Drawing.Point(18, 103);
      this.lblDownloadStatus.Name = "lblDownloadStatus";
      this.lblDownloadStatus.Size = new System.Drawing.Size(73, 13);
      this.lblDownloadStatus.TabIndex = 11;
      this.lblDownloadStatus.Text = "Connecting ...";
      // 
      // lblYourVersion
      // 
      this.lblYourVersion.AutoSize = true;
      this.lblYourVersion.Location = new System.Drawing.Point(18, 77);
      this.lblYourVersion.Name = "lblYourVersion";
      this.lblYourVersion.Size = new System.Drawing.Size(82, 13);
      this.lblYourVersion.TabIndex = 12;
      this.lblYourVersion.Text = "Your version is: ";
      // 
      // lblYourVersionNumber
      // 
      this.lblYourVersionNumber.AutoSize = true;
      this.lblYourVersionNumber.Location = new System.Drawing.Point(143, 77);
      this.lblYourVersionNumber.Name = "lblYourVersionNumber";
      this.lblYourVersionNumber.Size = new System.Drawing.Size(40, 13);
      this.lblYourVersionNumber.TabIndex = 13;
      this.lblYourVersionNumber.Text = "0.0.0.0";
      // 
      // lblCurrentVersion
      // 
      this.lblCurrentVersion.AutoSize = true;
      this.lblCurrentVersion.Location = new System.Drawing.Point(18, 128);
      this.lblCurrentVersion.Name = "lblCurrentVersion";
      this.lblCurrentVersion.Size = new System.Drawing.Size(94, 13);
      this.lblCurrentVersion.TabIndex = 12;
      this.lblCurrentVersion.Text = "Current version is: ";
      // 
      // lblCurrentVersionNumber
      // 
      this.lblCurrentVersionNumber.AutoSize = true;
      this.lblCurrentVersionNumber.Location = new System.Drawing.Point(143, 128);
      this.lblCurrentVersionNumber.Name = "lblCurrentVersionNumber";
      this.lblCurrentVersionNumber.Size = new System.Drawing.Size(13, 13);
      this.lblCurrentVersionNumber.TabIndex = 13;
      this.lblCurrentVersionNumber.Text = "?";
      // 
      // btnCancel
      // 
      this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnCancel.Location = new System.Drawing.Point(283, 281);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(75, 23);
      this.btnCancel.TabIndex = 14;
      this.btnCancel.Text = "Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
      // 
      // lblInstallHint
      // 
      this.lblInstallHint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.lblInstallHint.Location = new System.Drawing.Point(54, 239);
      this.lblInstallHint.Name = "lblInstallHint";
      this.lblInstallHint.Size = new System.Drawing.Size(295, 20);
      this.lblInstallHint.TabIndex = 5;
      this.lblInstallHint.Text = "Please install the newest OGAMA from the following website:";
      this.lblInstallHint.Visible = false;
      // 
      // pcbOgama
      // 
      this.pcbOgama.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.pcbOgama.Image = global::Ogama.Properties.Resources.Ogama;
      this.pcbOgama.Location = new System.Drawing.Point(21, 239);
      this.pcbOgama.Name = "pcbOgama";
      this.pcbOgama.Size = new System.Drawing.Size(36, 39);
      this.pcbOgama.TabIndex = 15;
      this.pcbOgama.TabStop = false;
      this.pcbOgama.Visible = false;
      this.pcbOgama.Click += new System.EventHandler(this.pcbOgama_Click);
      // 
      // lblAdditionalHint
      // 
      this.lblAdditionalHint.Location = new System.Drawing.Point(6, 37);
      this.lblAdditionalHint.Name = "lblAdditionalHint";
      this.lblAdditionalHint.Size = new System.Drawing.Size(322, 36);
      this.lblAdditionalHint.TabIndex = 5;
      this.lblAdditionalHint.Text = "Additional hint: ";
      // 
      // tmrConnect
      // 
      this.tmrConnect.Enabled = true;
      this.tmrConnect.Interval = 200;
      this.tmrConnect.Tick += new System.EventHandler(this.tmrConnect_Tick);
      // 
      // grbResults
      // 
      this.grbResults.Controls.Add(this.lblResult);
      this.grbResults.Controls.Add(this.lblAdditionalHint);
      this.grbResults.Location = new System.Drawing.Point(21, 157);
      this.grbResults.Name = "grbResults";
      this.grbResults.Size = new System.Drawing.Size(337, 76);
      this.grbResults.TabIndex = 16;
      this.grbResults.TabStop = false;
      this.grbResults.Text = "Result";
      this.grbResults.Visible = false;
      // 
      // dialogTop1
      // 
      this.dialogTop1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(237)))));
      this.dialogTop1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("dialogTop1.BackgroundImage")));
      this.dialogTop1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
      this.dialogTop1.Description = "Checking if a new version of OGAMA is available.";
      this.dialogTop1.Dock = System.Windows.Forms.DockStyle.Top;
      this.dialogTop1.Location = new System.Drawing.Point(0, 0);
      this.dialogTop1.Logo = global::Ogama.Properties.Resources.searchweb;
      this.dialogTop1.Name = "dialogTop1";
      this.dialogTop1.Size = new System.Drawing.Size(375, 60);
      this.dialogTop1.TabIndex = 9;
      // 
      // CheckForUpdates
      // 
      this.AcceptButton = this.btnOK;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.btnCancel;
      this.ClientSize = new System.Drawing.Size(375, 316);
      this.Controls.Add(this.grbResults);
      this.Controls.Add(this.pcbOgama);
      this.Controls.Add(this.btnCancel);
      this.Controls.Add(this.lblCurrentVersionNumber);
      this.Controls.Add(this.lblCurrentVersion);
      this.Controls.Add(this.lblYourVersionNumber);
      this.Controls.Add(this.lblYourVersion);
      this.Controls.Add(this.lblDownloadStatus);
      this.Controls.Add(this.pgbDownload);
      this.Controls.Add(this.dialogTop1);
      this.Controls.Add(this.llbDownloadUri);
      this.Controls.Add(this.lblInstallHint);
      this.Controls.Add(this.btnOK);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "CheckForUpdates";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Checking for updates...";
      this.Load += new System.EventHandler(this.frmCheckForUpdates_Load);
      this.Shown += new System.EventHandler(this.CheckForUpdates_Shown);
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CheckForUpdates_FormClosing);
      ((System.ComponentModel.ISupportInitialize)(this.pcbOgama)).EndInit();
      this.grbResults.ResumeLayout(false);
      this.grbResults.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button btnOK;
    private System.Windows.Forms.Label lblResult;
    private System.Windows.Forms.LinkLabel llbDownloadUri;
    private DialogTop dialogTop1;
    private System.Windows.Forms.ProgressBar pgbDownload;
    private System.Windows.Forms.Label lblDownloadStatus;
    private System.Windows.Forms.Label lblYourVersion;
    private System.Windows.Forms.Label lblYourVersionNumber;
    private System.Windows.Forms.Label lblCurrentVersion;
    private System.Windows.Forms.Label lblCurrentVersionNumber;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.Label lblInstallHint;
    private System.Windows.Forms.PictureBox pcbOgama;
    private System.Windows.Forms.Label lblAdditionalHint;
    private System.Windows.Forms.Timer tmrConnect;
    private System.Windows.Forms.GroupBox grbResults;
  }
}