namespace Ogama.Modules.Recording.HaythamInterface
{
  partial class HowToActivateHaytham
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HowToActivateHaytham));
      this.label3 = new System.Windows.Forms.Label();
      this.btnOK = new System.Windows.Forms.Button();
      this.label1 = new System.Windows.Forms.Label();
      this.pcbITU = new System.Windows.Forms.PictureBox();
      this.pcbITULogo = new System.Windows.Forms.PictureBox();
      this.llbITUWebsite = new System.Windows.Forms.LinkLabel();
      ((System.ComponentModel.ISupportInitialize)(this.pcbITU)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pcbITULogo)).BeginInit();
      this.SuspendLayout();
      // 
      // label3
      // 
      this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label3.Location = new System.Drawing.Point(143, 148);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(308, 177);
      this.label3.TabIndex = 11;
      this.label3.Text = resources.GetString("label3.Text");
      // 
      // btnOK
      // 
      this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnOK.AutoSize = true;
      this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.btnOK.Location = new System.Drawing.Point(363, 340);
      this.btnOK.Name = "btnOK";
      this.btnOK.Size = new System.Drawing.Size(76, 23);
      this.btnOK.TabIndex = 10;
      this.btnOK.Text = "OK";
      this.btnOK.UseVisualStyleBackColor = true;
      // 
      // label1
      // 
      this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label1.Location = new System.Drawing.Point(143, 92);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(308, 56);
      this.label1.TabIndex = 9;
      this.label1.Text = "To enable the recording with the ITU Haytham gaze tracker please perform the foll" +
    "owing steps:";
      // 
      // pcbITU
      // 
      this.pcbITU.Image = global::Ogama.Properties.Resources.HaythamLogo;
      this.pcbITU.Location = new System.Drawing.Point(12, 92);
      this.pcbITU.Name = "pcbITU";
      this.pcbITU.Size = new System.Drawing.Size(125, 117);
      this.pcbITU.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
      this.pcbITU.TabIndex = 8;
      this.pcbITU.TabStop = false;
      // 
      // pcbITULogo
      // 
      this.pcbITULogo.BackColor = System.Drawing.SystemColors.Control;
      this.pcbITULogo.Cursor = System.Windows.Forms.Cursors.Hand;
      this.pcbITULogo.Dock = System.Windows.Forms.DockStyle.Top;
      this.pcbITULogo.Image = global::Ogama.Properties.Resources.HaythamBanner;
      this.pcbITULogo.Location = new System.Drawing.Point(0, 0);
      this.pcbITULogo.Margin = new System.Windows.Forms.Padding(0);
      this.pcbITULogo.Name = "pcbITULogo";
      this.pcbITULogo.Size = new System.Drawing.Size(451, 80);
      this.pcbITULogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
      this.pcbITULogo.TabIndex = 12;
      this.pcbITULogo.TabStop = false;
      this.pcbITULogo.Click += new System.EventHandler(this.PcbItuLogoClick);
      // 
      // llbITUWebsite
      // 
      this.llbITUWebsite.AutoSize = true;
      this.llbITUWebsite.Location = new System.Drawing.Point(8, 289);
      this.llbITUWebsite.Name = "llbITUWebsite";
      this.llbITUWebsite.Size = new System.Drawing.Size(112, 13);
      this.llbITUWebsite.TabIndex = 13;
      this.llbITUWebsite.TabStop = true;
      this.llbITUWebsite.Text = "ITU Haytham Website";
      this.llbITUWebsite.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LlbItuWebsiteLinkClicked);
      // 
      // HowToActivateHaytham
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(451, 375);
      this.Controls.Add(this.llbITUWebsite);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.btnOK);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.pcbITU);
      this.Controls.Add(this.pcbITULogo);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "HowToActivateHaytham";
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "HowTo: Activate Haytham recording...";
      ((System.ComponentModel.ISupportInitialize)(this.pcbITU)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pcbITULogo)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Button btnOK;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.PictureBox pcbITU;
    private System.Windows.Forms.PictureBox pcbITULogo;
    private System.Windows.Forms.LinkLabel llbITUWebsite;
  }
}