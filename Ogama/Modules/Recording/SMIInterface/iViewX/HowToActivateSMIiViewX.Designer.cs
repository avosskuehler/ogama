namespace Ogama.Modules.Recording.SMIInterface.iViewX
{
  partial class HowToActivateSMIiViewX
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HowToActivateSMIiViewX));
      this.label3 = new System.Windows.Forms.Label();
      this.btnOK = new System.Windows.Forms.Button();
      this.label1 = new System.Windows.Forms.Label();
      this.pcbSMI = new System.Windows.Forms.PictureBox();
      this.pcbSMILogo = new System.Windows.Forms.PictureBox();
      ((System.ComponentModel.ISupportInitialize)(this.pcbSMI)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pcbSMILogo)).BeginInit();
      this.SuspendLayout();
      // 
      // label3
      // 
      this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label3.Location = new System.Drawing.Point(11, 169);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(486, 182);
      this.label3.TabIndex = 11;
      this.label3.Text = resources.GetString("label3.Text");
      // 
      // btnOK
      // 
      this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnOK.AutoSize = true;
      this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.btnOK.Location = new System.Drawing.Point(421, 359);
      this.btnOK.Name = "btnOK";
      this.btnOK.Size = new System.Drawing.Size(76, 23);
      this.btnOK.TabIndex = 10;
      this.btnOK.Text = "OK";
      this.btnOK.UseVisualStyleBackColor = true;
      // 
      // label1
      // 
      this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label1.Location = new System.Drawing.Point(86, 86);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(366, 56);
      this.label1.TabIndex = 9;
      this.label1.Text = "To enable the recording with Senso Motoric Instruments (SMI) iViewX tracking syst" +
          "em please perform the following steps:";
      // 
      // pcbSMI
      // 
      this.pcbSMI.Image = global::Ogama.Properties.Resources.SMIiViewXFoto64;
      this.pcbSMI.Location = new System.Drawing.Point(12, 84);
      this.pcbSMI.Name = "pcbSMI";
      this.pcbSMI.Size = new System.Drawing.Size(64, 64);
      this.pcbSMI.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
      this.pcbSMI.TabIndex = 8;
      this.pcbSMI.TabStop = false;
      // 
      // pcbSMILogo
      // 
      this.pcbSMILogo.BackColor = System.Drawing.SystemColors.Control;
      this.pcbSMILogo.Cursor = System.Windows.Forms.Cursors.Hand;
      this.pcbSMILogo.Dock = System.Windows.Forms.DockStyle.Top;
      this.pcbSMILogo.Image = global::Ogama.Properties.Resources.SMILogo;
      this.pcbSMILogo.Location = new System.Drawing.Point(0, 0);
      this.pcbSMILogo.Margin = new System.Windows.Forms.Padding(10);
      this.pcbSMILogo.Name = "pcbSMILogo";
      this.pcbSMILogo.Padding = new System.Windows.Forms.Padding(10);
      this.pcbSMILogo.Size = new System.Drawing.Size(509, 76);
      this.pcbSMILogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
      this.pcbSMILogo.TabIndex = 12;
      this.pcbSMILogo.TabStop = false;
      this.pcbSMILogo.Click += new System.EventHandler(this.pcbSMILogo_Click);
      // 
      // HowToActivateSMI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(509, 394);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.btnOK);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.pcbSMI);
      this.Controls.Add(this.pcbSMILogo);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "HowToActivateSMIiViewX";
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "HowTo: Activate SMI iViewX recording...";
      ((System.ComponentModel.ISupportInitialize)(this.pcbSMI)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pcbSMILogo)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Button btnOK;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.PictureBox pcbSMI;
    private System.Windows.Forms.PictureBox pcbSMILogo;
  }
}