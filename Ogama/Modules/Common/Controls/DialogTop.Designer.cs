namespace Ogama.Modules.Common.Controls
{
  partial class DialogTop
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

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.lblDescription = new System.Windows.Forms.Label();
      this.pcbLogo = new System.Windows.Forms.PictureBox();
      this.spcPicText = new System.Windows.Forms.SplitContainer();
      ((System.ComponentModel.ISupportInitialize)(this.pcbLogo)).BeginInit();
      this.spcPicText.Panel1.SuspendLayout();
      this.spcPicText.Panel2.SuspendLayout();
      this.spcPicText.SuspendLayout();
      this.SuspendLayout();
      // 
      // lblDescription
      // 
      this.lblDescription.BackColor = System.Drawing.Color.Transparent;
      this.lblDescription.Dock = System.Windows.Forms.DockStyle.Fill;
      this.lblDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblDescription.Location = new System.Drawing.Point(0, 0);
      this.lblDescription.Margin = new System.Windows.Forms.Padding(3);
      this.lblDescription.Name = "lblDescription";
      this.lblDescription.Padding = new System.Windows.Forms.Padding(0, 7, 0, 0);
      this.lblDescription.Size = new System.Drawing.Size(446, 60);
      this.lblDescription.TabIndex = 0;
      this.lblDescription.Text = "Description text";
      // 
      // pcbLogo
      // 
      this.pcbLogo.BackColor = System.Drawing.Color.Transparent;
      this.pcbLogo.Image = global::Ogama.Properties.Resources.GenericPicDoc;
      this.pcbLogo.Location = new System.Drawing.Point(5, 10);
      this.pcbLogo.Name = "pcbLogo";
      this.pcbLogo.Size = new System.Drawing.Size(43, 47);
      this.pcbLogo.TabIndex = 5;
      this.pcbLogo.TabStop = false;
      // 
      // spcPicText
      // 
      this.spcPicText.BackColor = System.Drawing.Color.Transparent;
      this.spcPicText.Dock = System.Windows.Forms.DockStyle.Fill;
      this.spcPicText.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
      this.spcPicText.IsSplitterFixed = true;
      this.spcPicText.Location = new System.Drawing.Point(0, 0);
      this.spcPicText.Name = "spcPicText";
      // 
      // spcPicText.Panel1
      // 
      this.spcPicText.Panel1.Controls.Add(this.pcbLogo);
      // 
      // spcPicText.Panel2
      // 
      this.spcPicText.Panel2.Controls.Add(this.lblDescription);
      this.spcPicText.Size = new System.Drawing.Size(500, 60);
      this.spcPicText.TabIndex = 6;
      this.spcPicText.TabStop = false;
      // 
      // DialogTop
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(237)))));
      this.BackgroundImage = global::Ogama.Properties.Resources.DialogBackground;
      this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
      this.Controls.Add(this.spcPicText);
      this.Name = "DialogTop";
      this.Size = new System.Drawing.Size(500, 60);
      ((System.ComponentModel.ISupportInitialize)(this.pcbLogo)).EndInit();
      this.spcPicText.Panel1.ResumeLayout(false);
      this.spcPicText.Panel2.ResumeLayout(false);
      this.spcPicText.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.PictureBox pcbLogo;
    private System.Windows.Forms.Label lblDescription;
    private System.Windows.Forms.SplitContainer spcPicText;
  }
}
