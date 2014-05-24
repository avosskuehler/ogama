namespace Ogama.MainWindow.Dialogs
{
  using Ogama.Modules.Common.Controls;

  partial class FlashMissing
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FlashMissing));
      this.btnOK = new System.Windows.Forms.Button();
      this.label2 = new System.Windows.Forms.Label();
      this.label3 = new System.Windows.Forms.Label();
      this.llbGetFlash = new System.Windows.Forms.LinkLabel();
      this.pcbGetFlash = new System.Windows.Forms.PictureBox();
      this.label4 = new System.Windows.Forms.Label();
      this.dialogTop1 = new DialogTop();
      ((System.ComponentModel.ISupportInitialize)(this.pcbGetFlash)).BeginInit();
      this.SuspendLayout();
      // 
      // btnOK
      // 
      this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnOK.AutoSize = true;
      this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.btnOK.Location = new System.Drawing.Point(338, 254);
      this.btnOK.Name = "btnOK";
      this.btnOK.Size = new System.Drawing.Size(76, 23);
      this.btnOK.TabIndex = 3;
      this.btnOK.Text = "Exit OGAMA";
      this.btnOK.UseVisualStyleBackColor = true;
      // 
      // label2
      // 
      this.label2.Location = new System.Drawing.Point(55, 237);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(220, 44);
      this.label2.TabIndex = 4;
      this.label2.Text = "OGAMA will now close.\r\nAfter successful flash player installation,\r\nrestart OGAMA" +
          " and try again ...";
      // 
      // label3
      // 
      this.label3.Location = new System.Drawing.Point(55, 65);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(217, 34);
      this.label3.TabIndex = 5;
      this.label3.Text = "Please install the newest Adobe Flash Player from the following website:";
      // 
      // llbGetFlash
      // 
      this.llbGetFlash.AutoSize = true;
      this.llbGetFlash.Location = new System.Drawing.Point(55, 209);
      this.llbGetFlash.Name = "llbGetFlash";
      this.llbGetFlash.Size = new System.Drawing.Size(205, 13);
      this.llbGetFlash.TabIndex = 6;
      this.llbGetFlash.TabStop = true;
      this.llbGetFlash.Text = "http://www.adobe.com/go/getflashplayer";
      this.llbGetFlash.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llbGetFlash_LinkClicked);
      // 
      // pcbGetFlash
      // 
      this.pcbGetFlash.Cursor = System.Windows.Forms.Cursors.Hand;
      this.pcbGetFlash.Image = global::Ogama.Properties.Resources.getflash;
      this.pcbGetFlash.Location = new System.Drawing.Point(55, 160);
      this.pcbGetFlash.Name = "pcbGetFlash";
      this.pcbGetFlash.Size = new System.Drawing.Size(167, 46);
      this.pcbGetFlash.TabIndex = 7;
      this.pcbGetFlash.TabStop = false;
      this.pcbGetFlash.Click += new System.EventHandler(this.pcbGetFlash_Click);
      // 
      // label4
      // 
      this.label4.ForeColor = System.Drawing.Color.Red;
      this.label4.Location = new System.Drawing.Point(55, 98);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(347, 59);
      this.label4.TabIndex = 8;
      this.label4.Text = resources.GetString("label4.Text");
      // 
      // dialogTop1
      // 
      this.dialogTop1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(237)))));
      this.dialogTop1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("dialogTop1.BackgroundImage")));
      this.dialogTop1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
      this.dialogTop1.Description = "The initalization of the flash com object failed.";
      this.dialogTop1.Dock = System.Windows.Forms.DockStyle.Top;
      this.dialogTop1.Location = new System.Drawing.Point(0, 0);
      this.dialogTop1.Logo = global::Ogama.Properties.Resources.FlashPlayer;
      this.dialogTop1.Name = "dialogTop1";
      this.dialogTop1.Size = new System.Drawing.Size(426, 60);
      this.dialogTop1.TabIndex = 9;
      // 
      // FlashMissing
      // 
      this.AcceptButton = this.btnOK;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(426, 289);
      this.Controls.Add(this.dialogTop1);
      this.Controls.Add(this.label4);
      this.Controls.Add(this.pcbGetFlash);
      this.Controls.Add(this.llbGetFlash);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.btnOK);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "FlashMissing";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Flash initialization failed ...";
      ((System.ComponentModel.ISupportInitialize)(this.pcbGetFlash)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button btnOK;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.LinkLabel llbGetFlash;
    private System.Windows.Forms.PictureBox pcbGetFlash;
    private System.Windows.Forms.Label label4;
    private DialogTop dialogTop1;
  }
}