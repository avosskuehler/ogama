namespace Ogama.MainWindow.Dialogs
{
  using Ogama.Modules.Common.Controls;

  sealed partial class AboutBox
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutBox));
      this.pcbFUBerlin = new System.Windows.Forms.PictureBox();
      this.textBoxDescription = new System.Windows.Forms.TextBox();
      this.labelProductName = new System.Windows.Forms.Label();
      this.labelVersion = new System.Windows.Forms.Label();
      this.labelCopyright = new System.Windows.Forms.Label();
      this.labelCompanyName = new System.Windows.Forms.Label();
      this.okButton = new System.Windows.Forms.Button();
      this.label1 = new System.Windows.Forms.Label();
      this.link = new System.Windows.Forms.LinkLabel();
      this.grpAssembly = new System.Windows.Forms.GroupBox();
      this.pcbWWW = new System.Windows.Forms.PictureBox();
      this.groupBox2 = new System.Windows.Forms.GroupBox();
      this.lblDescription = new System.Windows.Forms.Label();
      this.dialogTop1 = new Ogama.Modules.Common.Controls.DialogTop();
      ((System.ComponentModel.ISupportInitialize)(this.pcbFUBerlin)).BeginInit();
      this.grpAssembly.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pcbWWW)).BeginInit();
      this.groupBox2.SuspendLayout();
      this.SuspendLayout();
      // 
      // pcbFUBerlin
      // 
      this.pcbFUBerlin.BackColor = System.Drawing.Color.White;
      this.pcbFUBerlin.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
      this.pcbFUBerlin.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.pcbFUBerlin.Image = global::Ogama.Properties.Resources.LogoFU;
      this.pcbFUBerlin.Location = new System.Drawing.Point(0, 320);
      this.pcbFUBerlin.Margin = new System.Windows.Forms.Padding(0);
      this.pcbFUBerlin.Name = "pcbFUBerlin";
      this.pcbFUBerlin.Size = new System.Drawing.Size(457, 66);
      this.pcbFUBerlin.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
      this.pcbFUBerlin.TabIndex = 27;
      this.pcbFUBerlin.TabStop = false;
      // 
      // textBoxDescription
      // 
      this.textBoxDescription.AcceptsReturn = true;
      this.textBoxDescription.BackColor = System.Drawing.SystemColors.Control;
      this.textBoxDescription.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.textBoxDescription.Location = new System.Drawing.Point(15, 95);
      this.textBoxDescription.Margin = new System.Windows.Forms.Padding(6, 3, 3, 3);
      this.textBoxDescription.Multiline = true;
      this.textBoxDescription.Name = "textBoxDescription";
      this.textBoxDescription.ReadOnly = true;
      this.textBoxDescription.Size = new System.Drawing.Size(413, 67);
      this.textBoxDescription.TabIndex = 23;
      this.textBoxDescription.TabStop = false;
      this.textBoxDescription.Text = "It is developed by Adrian Voßkühler and written in C#.NET. It is published under " +
    "GPL, uses .NET Framework 4 and SQL Database and is open source.\r\n";
      // 
      // labelProductName
      // 
      this.labelProductName.AutoSize = true;
      this.labelProductName.Location = new System.Drawing.Point(9, 61);
      this.labelProductName.Margin = new System.Windows.Forms.Padding(6, 0, 3, 0);
      this.labelProductName.MaximumSize = new System.Drawing.Size(0, 17);
      this.labelProductName.Name = "labelProductName";
      this.labelProductName.Size = new System.Drawing.Size(75, 13);
      this.labelProductName.TabIndex = 25;
      this.labelProductName.Text = "Product Name";
      this.labelProductName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // labelVersion
      // 
      this.labelVersion.AutoSize = true;
      this.labelVersion.Location = new System.Drawing.Point(9, 82);
      this.labelVersion.Margin = new System.Windows.Forms.Padding(6, 0, 3, 0);
      this.labelVersion.MaximumSize = new System.Drawing.Size(0, 17);
      this.labelVersion.Name = "labelVersion";
      this.labelVersion.Size = new System.Drawing.Size(42, 13);
      this.labelVersion.TabIndex = 0;
      this.labelVersion.Text = "Version";
      this.labelVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // labelCopyright
      // 
      this.labelCopyright.AutoSize = true;
      this.labelCopyright.Location = new System.Drawing.Point(9, 40);
      this.labelCopyright.Margin = new System.Windows.Forms.Padding(6, 0, 3, 0);
      this.labelCopyright.MaximumSize = new System.Drawing.Size(0, 17);
      this.labelCopyright.Name = "labelCopyright";
      this.labelCopyright.Size = new System.Drawing.Size(51, 13);
      this.labelCopyright.TabIndex = 21;
      this.labelCopyright.Text = "Copyright";
      this.labelCopyright.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // labelCompanyName
      // 
      this.labelCompanyName.AutoSize = true;
      this.labelCompanyName.Location = new System.Drawing.Point(9, 19);
      this.labelCompanyName.Margin = new System.Windows.Forms.Padding(6, 0, 3, 0);
      this.labelCompanyName.MaximumSize = new System.Drawing.Size(0, 17);
      this.labelCompanyName.Name = "labelCompanyName";
      this.labelCompanyName.Size = new System.Drawing.Size(82, 13);
      this.labelCompanyName.TabIndex = 22;
      this.labelCompanyName.Text = "Company Name";
      this.labelCompanyName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // okButton
      // 
      this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.okButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.okButton.Location = new System.Drawing.Point(370, 281);
      this.okButton.Name = "okButton";
      this.okButton.Size = new System.Drawing.Size(75, 23);
      this.okButton.TabIndex = 24;
      this.okButton.Text = "&OK";
      // 
      // label1
      // 
      this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.label1.Location = new System.Drawing.Point(3, 16);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(240, 81);
      this.label1.TabIndex = 25;
      this.label1.Text = "- Dixon Cleveland from LC Technologies allowed us to use their fixation detection" +
    " algorithm.\r\n- The Gazegroup for creating the Gazetracker.\r\n- Many users providi" +
    "ng bug fixes and improvement requests.";
      // 
      // link
      // 
      this.link.Anchor = System.Windows.Forms.AnchorStyles.Top;
      this.link.AutoSize = true;
      this.link.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.link.Location = new System.Drawing.Point(66, 286);
      this.link.Name = "link";
      this.link.Size = new System.Drawing.Size(88, 20);
      this.link.TabIndex = 30;
      this.link.TabStop = true;
      this.link.Text = "Homepage";
      this.link.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLinkClicked);
      // 
      // grpAssembly
      // 
      this.grpAssembly.Controls.Add(this.labelCompanyName);
      this.grpAssembly.Controls.Add(this.labelCopyright);
      this.grpAssembly.Controls.Add(this.labelProductName);
      this.grpAssembly.Controls.Add(this.labelVersion);
      this.grpAssembly.Location = new System.Drawing.Point(15, 168);
      this.grpAssembly.Name = "grpAssembly";
      this.grpAssembly.Size = new System.Drawing.Size(181, 100);
      this.grpAssembly.TabIndex = 31;
      this.grpAssembly.TabStop = false;
      this.grpAssembly.Text = "Assembly information";
      // 
      // pcbWWW
      // 
      this.pcbWWW.Image = global::Ogama.Properties.Resources.globe;
      this.pcbWWW.Location = new System.Drawing.Point(12, 276);
      this.pcbWWW.Name = "pcbWWW";
      this.pcbWWW.Size = new System.Drawing.Size(42, 38);
      this.pcbWWW.TabIndex = 32;
      this.pcbWWW.TabStop = false;
      // 
      // groupBox2
      // 
      this.groupBox2.Controls.Add(this.label1);
      this.groupBox2.Location = new System.Drawing.Point(202, 168);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new System.Drawing.Size(246, 100);
      this.groupBox2.TabIndex = 33;
      this.groupBox2.TabStop = false;
      this.groupBox2.Text = "Acknowledgements";
      // 
      // lblDescription
      // 
      this.lblDescription.AutoSize = true;
      this.lblDescription.Location = new System.Drawing.Point(12, 69);
      this.lblDescription.Name = "lblDescription";
      this.lblDescription.Size = new System.Drawing.Size(405, 13);
      this.lblDescription.TabIndex = 34;
      this.lblDescription.Text = "This program is intended to analyze still images overlayed with mouse and gaze da" +
    "ta.";
      // 
      // dialogTop1
      // 
      this.dialogTop1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(237)))));
      this.dialogTop1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("dialogTop1.BackgroundImage")));
      this.dialogTop1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
      this.dialogTop1.Description = "About this software ...";
      this.dialogTop1.Dock = System.Windows.Forms.DockStyle.Top;
      this.dialogTop1.Location = new System.Drawing.Point(0, 0);
      this.dialogTop1.Logo = ((System.Drawing.Image)(resources.GetObject("dialogTop1.Logo")));
      this.dialogTop1.Name = "dialogTop1";
      this.dialogTop1.Size = new System.Drawing.Size(457, 60);
      this.dialogTop1.TabIndex = 35;
      // 
      // AboutBox
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.SystemColors.Control;
      this.ClientSize = new System.Drawing.Size(457, 386);
      this.Controls.Add(this.dialogTop1);
      this.Controls.Add(this.lblDescription);
      this.Controls.Add(this.groupBox2);
      this.Controls.Add(this.pcbWWW);
      this.Controls.Add(this.grpAssembly);
      this.Controls.Add(this.okButton);
      this.Controls.Add(this.link);
      this.Controls.Add(this.pcbFUBerlin);
      this.Controls.Add(this.textBoxDescription);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "AboutBox";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "About OGAMA ...";
      ((System.ComponentModel.ISupportInitialize)(this.pcbFUBerlin)).EndInit();
      this.grpAssembly.ResumeLayout(false);
      this.grpAssembly.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pcbWWW)).EndInit();
      this.groupBox2.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label labelVersion;
    private System.Windows.Forms.Label labelCopyright;
    private System.Windows.Forms.Label labelCompanyName;
    private System.Windows.Forms.TextBox textBoxDescription;
    private System.Windows.Forms.Label labelProductName;
    private System.Windows.Forms.PictureBox pcbFUBerlin;
    private System.Windows.Forms.Button okButton;
    private System.Windows.Forms.LinkLabel link;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.GroupBox grpAssembly;
    private System.Windows.Forms.GroupBox groupBox2;
    private System.Windows.Forms.Label lblDescription;
    private System.Windows.Forms.PictureBox pcbWWW;
    private DialogTop dialogTop1;
  }
}