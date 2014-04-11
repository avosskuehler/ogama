namespace Ogama.Modules.Recording.GazegroupInterface
{
  partial class HowToActivateGazetracker
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HowToActivateGazetracker));
      this.label3 = new System.Windows.Forms.Label();
      this.btnOK = new System.Windows.Forms.Button();
      this.label1 = new System.Windows.Forms.Label();
      this.pcbITU = new System.Windows.Forms.PictureBox();
      this.pcbITULogo = new System.Windows.Forms.PictureBox();
      this.llbITUWebsite = new System.Windows.Forms.LinkLabel();
      this.llbITUManual = new System.Windows.Forms.LinkLabel();
      ((System.ComponentModel.ISupportInitialize)(this.pcbITU)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pcbITULogo)).BeginInit();
      this.SuspendLayout();
      // 
      // label3
      // 
      this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label3.Location = new System.Drawing.Point(143, 166);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(354, 251);
      this.label3.TabIndex = 11;
      this.label3.Text = resources.GetString("label3.Text");
      // 
      // btnOK
      // 
      this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnOK.AutoSize = true;
      this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.btnOK.Location = new System.Drawing.Point(421, 420);
      this.btnOK.Name = "btnOK";
      this.btnOK.Size = new System.Drawing.Size(76, 23);
      this.btnOK.TabIndex = 10;
      this.btnOK.Text = "OK";
      this.btnOK.UseVisualStyleBackColor = true;
      // 
      // label1
      // 
      this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label1.Location = new System.Drawing.Point(143, 124);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(366, 56);
      this.label1.TabIndex = 9;
      this.label1.Text = "To enable the recording with the ITU GazeTracker please perform the following ste" +
          "ps:";
      // 
      // pcbITU
      // 
      this.pcbITU.Image = global::Ogama.Properties.Resources.ITU_Logo;
      this.pcbITU.Location = new System.Drawing.Point(12, 124);
      this.pcbITU.Name = "pcbITU";
      this.pcbITU.Size = new System.Drawing.Size(113, 144);
      this.pcbITU.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
      this.pcbITU.TabIndex = 8;
      this.pcbITU.TabStop = false;
      // 
      // pcbITULogo
      // 
      this.pcbITULogo.BackColor = System.Drawing.SystemColors.Control;
      this.pcbITULogo.Cursor = System.Windows.Forms.Cursors.Hand;
      this.pcbITULogo.Dock = System.Windows.Forms.DockStyle.Top;
      this.pcbITULogo.Image = global::Ogama.Properties.Resources.ITUBanner;
      this.pcbITULogo.Location = new System.Drawing.Point(0, 0);
      this.pcbITULogo.Margin = new System.Windows.Forms.Padding(0);
      this.pcbITULogo.Name = "pcbITULogo";
      this.pcbITULogo.Size = new System.Drawing.Size(509, 110);
      this.pcbITULogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
      this.pcbITULogo.TabIndex = 12;
      this.pcbITULogo.TabStop = false;
      this.pcbITULogo.Click += new System.EventHandler(this.PcbITULogoClick);
      // 
      // llbITUWebsite
      // 
      this.llbITUWebsite.AutoSize = true;
      this.llbITUWebsite.Location = new System.Drawing.Point(8, 289);
      this.llbITUWebsite.Name = "llbITUWebsite";
      this.llbITUWebsite.Size = new System.Drawing.Size(129, 13);
      this.llbITUWebsite.TabIndex = 13;
      this.llbITUWebsite.TabStop = true;
      this.llbITUWebsite.Text = "ITU GazeTacker Website";
      this.llbITUWebsite.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LlbITUWebsiteLinkClicked);
      // 
      // llbITUManual
      // 
      this.llbITUManual.AutoSize = true;
      this.llbITUManual.Location = new System.Drawing.Point(9, 312);
      this.llbITUManual.Name = "llbITUManual";
      this.llbITUManual.Size = new System.Drawing.Size(125, 13);
      this.llbITUManual.TabIndex = 13;
      this.llbITUManual.TabStop = true;
      this.llbITUManual.Text = "ITU GazeTacker Manual";
      this.llbITUManual.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LlbITUManualLinkClicked);
      // 
      // HowToActivateGazetracker
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(509, 455);
      this.Controls.Add(this.llbITUManual);
      this.Controls.Add(this.llbITUWebsite);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.btnOK);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.pcbITU);
      this.Controls.Add(this.pcbITULogo);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "HowToActivateGazetracker";
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "HowTo: Activate GazeTracker recording...";
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
    private System.Windows.Forms.LinkLabel llbITUManual;
  }
}