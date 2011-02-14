namespace Ogama.Modules.Recording
{
  partial class SelectTracker
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectTracker));
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      this.dialogTop1 = new Ogama.Modules.Common.DialogTop();
      this.splitContainer2 = new System.Windows.Forms.SplitContainer();
      this.tabControl1 = new System.Windows.Forms.TabControl();
      this.tabPage1 = new System.Windows.Forms.TabPage();
      this.pcbHelpSMI = new System.Windows.Forms.PictureBox();
      this.pcbHelpAlea = new System.Windows.Forms.PictureBox();
      this.pcbHelpTobii = new System.Windows.Forms.PictureBox();
      this.chbTobii = new System.Windows.Forms.CheckBox();
      this.chbAlea = new System.Windows.Forms.CheckBox();
      this.pcbTobii = new System.Windows.Forms.PictureBox();
      this.pcbSMI = new System.Windows.Forms.PictureBox();
      this.pcbAlea = new System.Windows.Forms.PictureBox();
      this.chbSMI = new System.Windows.Forms.CheckBox();
      this.tabPage2 = new System.Windows.Forms.TabPage();
      this.pcbHelpITU = new System.Windows.Forms.PictureBox();
      this.pcbITU = new System.Windows.Forms.PictureBox();
      this.chbMouseOnly = new System.Windows.Forms.CheckBox();
      this.chbITU = new System.Windows.Forms.CheckBox();
      this.btnCancel = new System.Windows.Forms.Button();
      this.btnOK = new System.Windows.Forms.Button();
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      this.splitContainer2.Panel1.SuspendLayout();
      this.splitContainer2.Panel2.SuspendLayout();
      this.splitContainer2.SuspendLayout();
      this.tabControl1.SuspendLayout();
      this.tabPage1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pcbHelpSMI)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pcbHelpAlea)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pcbHelpTobii)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pcbTobii)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pcbSMI)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pcbAlea)).BeginInit();
      this.tabPage2.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pcbHelpITU)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pcbITU)).BeginInit();
      this.SuspendLayout();
      // 
      // splitContainer1
      // 
      this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
      this.splitContainer1.IsSplitterFixed = true;
      this.splitContainer1.Location = new System.Drawing.Point(0, 0);
      this.splitContainer1.Name = "splitContainer1";
      this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // splitContainer1.Panel1
      // 
      this.splitContainer1.Panel1.Controls.Add(this.dialogTop1);
      // 
      // splitContainer1.Panel2
      // 
      this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
      this.splitContainer1.Size = new System.Drawing.Size(515, 422);
      this.splitContainer1.SplitterDistance = 60;
      this.splitContainer1.SplitterWidth = 1;
      this.splitContainer1.TabIndex = 6;
      // 
      // dialogTop1
      // 
      this.dialogTop1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(237)))));
      this.dialogTop1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("dialogTop1.BackgroundImage")));
      this.dialogTop1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
      this.dialogTop1.Description = "Check the tracking devices that should be available in the recording module.";
      this.dialogTop1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.dialogTop1.Location = new System.Drawing.Point(0, 0);
      this.dialogTop1.Logo = global::Ogama.Properties.Resources.Record;
      this.dialogTop1.Name = "dialogTop1";
      this.dialogTop1.Size = new System.Drawing.Size(515, 60);
      this.dialogTop1.TabIndex = 0;
      // 
      // splitContainer2
      // 
      this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
      this.splitContainer2.IsSplitterFixed = true;
      this.splitContainer2.Location = new System.Drawing.Point(0, 0);
      this.splitContainer2.Name = "splitContainer2";
      this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // splitContainer2.Panel1
      // 
      this.splitContainer2.Panel1.Controls.Add(this.tabControl1);
      // 
      // splitContainer2.Panel2
      // 
      this.splitContainer2.Panel2.Controls.Add(this.btnCancel);
      this.splitContainer2.Panel2.Controls.Add(this.btnOK);
      this.splitContainer2.Panel2MinSize = 35;
      this.splitContainer2.Size = new System.Drawing.Size(515, 361);
      this.splitContainer2.SplitterDistance = 322;
      this.splitContainer2.TabIndex = 15;
      // 
      // tabControl1
      // 
      this.tabControl1.Controls.Add(this.tabPage1);
      this.tabControl1.Controls.Add(this.tabPage2);
      this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tabControl1.Location = new System.Drawing.Point(0, 0);
      this.tabControl1.Name = "tabControl1";
      this.tabControl1.SelectedIndex = 0;
      this.tabControl1.Size = new System.Drawing.Size(515, 322);
      this.tabControl1.TabIndex = 14;
      // 
      // tabPage1
      // 
      this.tabPage1.Controls.Add(this.pcbHelpSMI);
      this.tabPage1.Controls.Add(this.pcbHelpAlea);
      this.tabPage1.Controls.Add(this.pcbHelpTobii);
      this.tabPage1.Controls.Add(this.chbTobii);
      this.tabPage1.Controls.Add(this.chbAlea);
      this.tabPage1.Controls.Add(this.pcbTobii);
      this.tabPage1.Controls.Add(this.pcbSMI);
      this.tabPage1.Controls.Add(this.pcbAlea);
      this.tabPage1.Controls.Add(this.chbSMI);
      this.tabPage1.Location = new System.Drawing.Point(4, 22);
      this.tabPage1.Name = "tabPage1";
      this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage1.Size = new System.Drawing.Size(507, 296);
      this.tabPage1.TabIndex = 0;
      this.tabPage1.Text = "Commercial Tracker";
      this.tabPage1.UseVisualStyleBackColor = true;
      // 
      // pcbHelpSMI
      // 
      this.pcbHelpSMI.Cursor = System.Windows.Forms.Cursors.Help;
      this.pcbHelpSMI.Image = global::Ogama.Properties.Resources.HelpBmp;
      this.pcbHelpSMI.Location = new System.Drawing.Point(483, 204);
      this.pcbHelpSMI.Name = "pcbHelpSMI";
      this.pcbHelpSMI.Size = new System.Drawing.Size(16, 16);
      this.pcbHelpSMI.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
      this.pcbHelpSMI.TabIndex = 11;
      this.pcbHelpSMI.TabStop = false;
      this.pcbHelpSMI.Click += new System.EventHandler(this.pcbHelpSMI_Click);
      // 
      // pcbHelpAlea
      // 
      this.pcbHelpAlea.Cursor = System.Windows.Forms.Cursors.Help;
      this.pcbHelpAlea.Image = global::Ogama.Properties.Resources.HelpBmp;
      this.pcbHelpAlea.Location = new System.Drawing.Point(483, 125);
      this.pcbHelpAlea.Name = "pcbHelpAlea";
      this.pcbHelpAlea.Size = new System.Drawing.Size(16, 16);
      this.pcbHelpAlea.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
      this.pcbHelpAlea.TabIndex = 11;
      this.pcbHelpAlea.TabStop = false;
      this.pcbHelpAlea.Click += new System.EventHandler(this.pcbHelpAlea_Click);
      // 
      // pcbHelpTobii
      // 
      this.pcbHelpTobii.Cursor = System.Windows.Forms.Cursors.Help;
      this.pcbHelpTobii.Image = global::Ogama.Properties.Resources.HelpBmp;
      this.pcbHelpTobii.Location = new System.Drawing.Point(483, 42);
      this.pcbHelpTobii.Name = "pcbHelpTobii";
      this.pcbHelpTobii.Size = new System.Drawing.Size(16, 16);
      this.pcbHelpTobii.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
      this.pcbHelpTobii.TabIndex = 11;
      this.pcbHelpTobii.TabStop = false;
      this.pcbHelpTobii.Click += new System.EventHandler(this.pcbHelpTobii_Click);
      // 
      // chbTobii
      // 
      this.chbTobii.BackColor = System.Drawing.Color.Transparent;
      this.chbTobii.Image = global::Ogama.Properties.Resources.Tobii_61_75;
      this.chbTobii.Location = new System.Drawing.Point(100, 10);
      this.chbTobii.Name = "chbTobii";
      this.chbTobii.Size = new System.Drawing.Size(330, 86);
      this.chbTobii.TabIndex = 1;
      this.chbTobii.Text = "The Tobii technologies T60,T120,X120 gaze tracker series. Needs to have a purchas" +
          "ed Tobii SDK to be installed on the computer.";
      this.chbTobii.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.chbTobii.UseVisualStyleBackColor = false;
      // 
      // chbAlea
      // 
      this.chbAlea.Image = global::Ogama.Properties.Resources.Alea_61_75;
      this.chbAlea.Location = new System.Drawing.Point(100, 100);
      this.chbAlea.Name = "chbAlea";
      this.chbAlea.Size = new System.Drawing.Size(370, 82);
      this.chbAlea.TabIndex = 1;
      this.chbAlea.Text = "The alea technologies IG-30 Pro Eyetracking-System. Needs to have Intelligaze Sof" +
          "tware 1.2 to be installed.\r\n";
      this.chbAlea.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.chbAlea.UseVisualStyleBackColor = true;
      // 
      // pcbTobii
      // 
      this.pcbTobii.Cursor = System.Windows.Forms.Cursors.Hand;
      this.pcbTobii.Image = global::Ogama.Properties.Resources.TobiiLogoText;
      this.pcbTobii.Location = new System.Drawing.Point(8, 35);
      this.pcbTobii.Name = "pcbTobii";
      this.pcbTobii.Size = new System.Drawing.Size(76, 33);
      this.pcbTobii.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
      this.pcbTobii.TabIndex = 9;
      this.pcbTobii.TabStop = false;
      this.pcbTobii.Click += new System.EventHandler(this.pcbTobii_Click);
      // 
      // pcbSMI
      // 
      this.pcbSMI.Image = global::Ogama.Properties.Resources.SMILogo;
      this.pcbSMI.Location = new System.Drawing.Point(8, 190);
      this.pcbSMI.Name = "pcbSMI";
      this.pcbSMI.Size = new System.Drawing.Size(52, 48);
      this.pcbSMI.TabIndex = 10;
      this.pcbSMI.TabStop = false;
      this.pcbSMI.Click += new System.EventHandler(this.pcbSMI_Click);
      // 
      // pcbAlea
      // 
      this.pcbAlea.Cursor = System.Windows.Forms.Cursors.Hand;
      this.pcbAlea.Image = global::Ogama.Properties.Resources.AleaLogo;
      this.pcbAlea.Location = new System.Drawing.Point(8, 125);
      this.pcbAlea.Name = "pcbAlea";
      this.pcbAlea.Size = new System.Drawing.Size(33, 34);
      this.pcbAlea.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
      this.pcbAlea.TabIndex = 9;
      this.pcbAlea.TabStop = false;
      this.pcbAlea.Click += new System.EventHandler(this.pcbAlea_Click);
      // 
      // chbSMI
      // 
      this.chbSMI.Image = global::Ogama.Properties.Resources.SMI_61_25;
      this.chbSMI.Location = new System.Drawing.Point(100, 190);
      this.chbSMI.Name = "chbSMI";
      this.chbSMI.Size = new System.Drawing.Size(285, 43);
      this.chbSMI.TabIndex = 1;
      this.chbSMI.Text = "The SMI iViewX interface. Needs to have a iViewX software running on the same or " +
          "a connected computer.\r\n";
      this.chbSMI.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.chbSMI.UseVisualStyleBackColor = true;
      // 
      // tabPage2
      // 
      this.tabPage2.Controls.Add(this.pcbHelpITU);
      this.tabPage2.Controls.Add(this.pcbITU);
      this.tabPage2.Controls.Add(this.chbMouseOnly);
      this.tabPage2.Controls.Add(this.chbITU);
      this.tabPage2.Location = new System.Drawing.Point(4, 22);
      this.tabPage2.Name = "tabPage2";
      this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage2.Size = new System.Drawing.Size(507, 296);
      this.tabPage2.TabIndex = 1;
      this.tabPage2.Text = "Open Source Tracker";
      this.tabPage2.UseVisualStyleBackColor = true;
      // 
      // pcbHelpITU
      // 
      this.pcbHelpITU.Cursor = System.Windows.Forms.Cursors.Help;
      this.pcbHelpITU.Image = global::Ogama.Properties.Resources.HelpBmp;
      this.pcbHelpITU.Location = new System.Drawing.Point(483, 44);
      this.pcbHelpITU.Name = "pcbHelpITU";
      this.pcbHelpITU.Size = new System.Drawing.Size(16, 16);
      this.pcbHelpITU.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
      this.pcbHelpITU.TabIndex = 13;
      this.pcbHelpITU.TabStop = false;
      this.pcbHelpITU.Click += new System.EventHandler(this.pcbHelpITU_Click);
      // 
      // pcbITU
      // 
      this.pcbITU.Cursor = System.Windows.Forms.Cursors.Hand;
      this.pcbITU.Image = global::Ogama.Properties.Resources.ITU_Logo;
      this.pcbITU.Location = new System.Drawing.Point(6, 10);
      this.pcbITU.Name = "pcbITU";
      this.pcbITU.Size = new System.Drawing.Size(88, 97);
      this.pcbITU.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pcbITU.TabIndex = 12;
      this.pcbITU.TabStop = false;
      this.pcbITU.Click += new System.EventHandler(this.pcbITU_Click);
      // 
      // chbMouseOnly
      // 
      this.chbMouseOnly.Image = global::Ogama.Properties.Resources.Maus_61_61;
      this.chbMouseOnly.Location = new System.Drawing.Point(100, 110);
      this.chbMouseOnly.Name = "chbMouseOnly";
      this.chbMouseOnly.Size = new System.Drawing.Size(341, 57);
      this.chbMouseOnly.TabIndex = 1;
      this.chbMouseOnly.Text = "This recorder captures just mouse movements and events. This can be used when gaz" +
          "e tracking is not needed.\r\n\r\n";
      this.chbMouseOnly.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.chbMouseOnly.UseVisualStyleBackColor = true;
      // 
      // chbITU
      // 
      this.chbITU.Image = global::Ogama.Properties.Resources.ITU_61_61;
      this.chbITU.Location = new System.Drawing.Point(100, 10);
      this.chbITU.Name = "chbITU";
      this.chbITU.Size = new System.Drawing.Size(341, 86);
      this.chbITU.TabIndex = 11;
      this.chbITU.Text = "The ITU GazeTracker application which uses a webcam as an eye tracker and can be " +
          "used in both remote and head-mounted setup.";
      this.chbITU.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.chbITU.UseVisualStyleBackColor = true;
      // 
      // btnCancel
      // 
      this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnCancel.Location = new System.Drawing.Point(428, 8);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(75, 23);
      this.btnCancel.TabIndex = 2;
      this.btnCancel.Text = "Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      // 
      // btnOK
      // 
      this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnOK.AutoSize = true;
      this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.btnOK.Location = new System.Drawing.Point(346, 8);
      this.btnOK.Name = "btnOK";
      this.btnOK.Size = new System.Drawing.Size(76, 23);
      this.btnOK.TabIndex = 7;
      this.btnOK.Text = "OK";
      this.btnOK.UseVisualStyleBackColor = true;
      // 
      // SelectTracker
      // 
      this.AcceptButton = this.btnOK;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.btnCancel;
      this.ClientSize = new System.Drawing.Size(515, 422);
      this.Controls.Add(this.splitContainer1);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "SelectTracker";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Please select the tracking devices ...";
      this.Load += new System.EventHandler(this.SelectTracker_Load);
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SelectTracker_FormClosing);
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel2.ResumeLayout(false);
      this.splitContainer1.ResumeLayout(false);
      this.splitContainer2.Panel1.ResumeLayout(false);
      this.splitContainer2.Panel2.ResumeLayout(false);
      this.splitContainer2.Panel2.PerformLayout();
      this.splitContainer2.ResumeLayout(false);
      this.tabControl1.ResumeLayout(false);
      this.tabPage1.ResumeLayout(false);
      this.tabPage1.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pcbHelpSMI)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pcbHelpAlea)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pcbHelpTobii)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pcbTobii)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pcbSMI)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pcbAlea)).EndInit();
      this.tabPage2.ResumeLayout(false);
      this.tabPage2.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pcbHelpITU)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pcbITU)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.SplitContainer splitContainer1;
    private Ogama.Modules.Common.DialogTop dialogTop1;
    private System.Windows.Forms.CheckBox chbMouseOnly;
    private System.Windows.Forms.CheckBox chbAlea;
    private System.Windows.Forms.CheckBox chbTobii;
    private System.Windows.Forms.Button btnOK;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.PictureBox pcbAlea;
    private System.Windows.Forms.CheckBox chbSMI;
    private System.Windows.Forms.PictureBox pcbSMI;
    private System.Windows.Forms.CheckBox chbITU;
    private System.Windows.Forms.PictureBox pcbTobii;
    private System.Windows.Forms.SplitContainer splitContainer2;
    private System.Windows.Forms.TabControl tabControl1;
    private System.Windows.Forms.TabPage tabPage1;
    private System.Windows.Forms.TabPage tabPage2;
    private System.Windows.Forms.PictureBox pcbITU;
    private System.Windows.Forms.PictureBox pcbHelpTobii;
    private System.Windows.Forms.PictureBox pcbHelpSMI;
    private System.Windows.Forms.PictureBox pcbHelpAlea;
    private System.Windows.Forms.PictureBox pcbHelpITU;
  }
}