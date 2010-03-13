namespace Ogama.MainWindow
{
  partial class HowToActivateITUPS3
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HowToActivateITUPS3));
      this.label3 = new System.Windows.Forms.Label();
      this.btnOK = new System.Windows.Forms.Button();
      this.label1 = new System.Windows.Forms.Label();
      this.pcbITU = new System.Windows.Forms.PictureBox();
      this.pcbITULogo = new System.Windows.Forms.PictureBox();
      this.llbITUWebsite = new System.Windows.Forms.LinkLabel();
      this.llbITUManual = new System.Windows.Forms.LinkLabel();
      this.pcbEyeWriter = new System.Windows.Forms.PictureBox();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.llbEyeWriter = new System.Windows.Forms.LinkLabel();
      this.llbCodeLaboratories = new System.Windows.Forms.LinkLabel();
      ((System.ComponentModel.ISupportInitialize)(this.pcbITU)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pcbITULogo)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pcbEyeWriter)).BeginInit();
      this.groupBox1.SuspendLayout();
      this.SuspendLayout();
      // 
      // label3
      // 
      this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label3.Location = new System.Drawing.Point(12, 192);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(485, 182);
      this.label3.TabIndex = 11;
      this.label3.Text = resources.GetString("label3.Text");
      // 
      // btnOK
      // 
      this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnOK.AutoSize = true;
      this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.btnOK.Location = new System.Drawing.Point(421, 469);
      this.btnOK.Name = "btnOK";
      this.btnOK.Size = new System.Drawing.Size(76, 23);
      this.btnOK.TabIndex = 10;
      this.btnOK.Text = "OK";
      this.btnOK.UseVisualStyleBackColor = true;
      // 
      // label1
      // 
      this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label1.Location = new System.Drawing.Point(94, 124);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(415, 56);
      this.label1.TabIndex = 9;
      this.label1.Text = "To enable the recording with the ITU GazeTracker using the PlayStation3 Eye camer" +
          "a please perform the following steps:";
      // 
      // pcbITU
      // 
      this.pcbITU.Image = global::Ogama.Properties.Resources.PS3_61_61;
      this.pcbITU.Location = new System.Drawing.Point(12, 124);
      this.pcbITU.Name = "pcbITU";
      this.pcbITU.Size = new System.Drawing.Size(61, 61);
      this.pcbITU.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
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
      this.pcbITULogo.Click += new System.EventHandler(this.pcbITULogo_Click);
      // 
      // llbITUWebsite
      // 
      this.llbITUWebsite.AutoSize = true;
      this.llbITUWebsite.Location = new System.Drawing.Point(6, 61);
      this.llbITUWebsite.Name = "llbITUWebsite";
      this.llbITUWebsite.Size = new System.Drawing.Size(129, 13);
      this.llbITUWebsite.TabIndex = 13;
      this.llbITUWebsite.TabStop = true;
      this.llbITUWebsite.Text = "ITU GazeTacker Website";
      this.llbITUWebsite.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llbITUWebsite_LinkClicked);
      // 
      // llbITUManual
      // 
      this.llbITUManual.AutoSize = true;
      this.llbITUManual.Location = new System.Drawing.Point(6, 83);
      this.llbITUManual.Name = "llbITUManual";
      this.llbITUManual.Size = new System.Drawing.Size(125, 13);
      this.llbITUManual.TabIndex = 13;
      this.llbITUManual.TabStop = true;
      this.llbITUManual.Text = "ITU GazeTacker Manual";
      this.llbITUManual.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llbITUManual_LinkClicked);
      // 
      // pcbEyeWriter
      // 
      this.pcbEyeWriter.Image = global::Ogama.Properties.Resources.Tempt_Logo;
      this.pcbEyeWriter.Location = new System.Drawing.Point(12, 12);
      this.pcbEyeWriter.Name = "pcbEyeWriter";
      this.pcbEyeWriter.Size = new System.Drawing.Size(100, 73);
      this.pcbEyeWriter.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
      this.pcbEyeWriter.TabIndex = 14;
      this.pcbEyeWriter.TabStop = false;
      this.pcbEyeWriter.Click += new System.EventHandler(this.pcbEyeWriter_Click);
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.llbITUWebsite);
      this.groupBox1.Controls.Add(this.llbCodeLaboratories);
      this.groupBox1.Controls.Add(this.llbEyeWriter);
      this.groupBox1.Controls.Add(this.llbITUManual);
      this.groupBox1.Location = new System.Drawing.Point(15, 377);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(333, 115);
      this.groupBox1.TabIndex = 15;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Resources";
      // 
      // llbEyeWriter
      // 
      this.llbEyeWriter.AutoSize = true;
      this.llbEyeWriter.Location = new System.Drawing.Point(6, 16);
      this.llbEyeWriter.Name = "llbEyeWriter";
      this.llbEyeWriter.Size = new System.Drawing.Size(307, 13);
      this.llbEyeWriter.TabIndex = 13;
      this.llbEyeWriter.TabStop = true;
      this.llbEyeWriter.Text = "EyeWriter.org Manual on how to modify the PlayStation camera.";
      this.llbEyeWriter.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llbEyeWriter_LinkClicked);
      // 
      // llbCodeLaboratories
      // 
      this.llbCodeLaboratories.AutoSize = true;
      this.llbCodeLaboratories.Location = new System.Drawing.Point(6, 38);
      this.llbCodeLaboratories.Name = "llbCodeLaboratories";
      this.llbCodeLaboratories.Size = new System.Drawing.Size(249, 13);
      this.llbCodeLaboratories.TabIndex = 13;
      this.llbCodeLaboratories.TabStop = true;
      this.llbCodeLaboratories.Text = "CodeLaboratories driver for the PlayStation camera.";
      this.llbCodeLaboratories.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llbCodeLaboratories_LinkClicked);
      // 
      // HowToActivateITUPS3
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(509, 504);
      this.Controls.Add(this.groupBox1);
      this.Controls.Add(this.pcbEyeWriter);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.btnOK);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.pcbITU);
      this.Controls.Add(this.pcbITULogo);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "HowToActivateITUPS3";
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "HowTo: Activate ITU GazeTracker recording with the playstation 3 camera ...";
      ((System.ComponentModel.ISupportInitialize)(this.pcbITU)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pcbITULogo)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pcbEyeWriter)).EndInit();
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
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
    private System.Windows.Forms.PictureBox pcbEyeWriter;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.LinkLabel llbEyeWriter;
    private System.Windows.Forms.LinkLabel llbCodeLaboratories;
  }
}