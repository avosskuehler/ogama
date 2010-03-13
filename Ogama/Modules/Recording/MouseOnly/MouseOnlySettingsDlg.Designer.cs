namespace Ogama.Modules.Recording.MouseOnly
{
  partial class MouseOnlySettingsDlg
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
      this.pictureBox1 = new System.Windows.Forms.PictureBox();
      this.label1 = new System.Windows.Forms.Label();
      this.btnOK = new System.Windows.Forms.Button();
      this.btnCancel = new System.Windows.Forms.Button();
      this.label5 = new System.Windows.Forms.Label();
      this.cbbSampleRate = new System.Windows.Forms.ComboBox();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
      this.SuspendLayout();
      // 
      // pictureBox1
      // 
      this.pictureBox1.Image = global::Ogama.Properties.Resources.Mouse;
      this.pictureBox1.Location = new System.Drawing.Point(12, 12);
      this.pictureBox1.Name = "pictureBox1";
      this.pictureBox1.Size = new System.Drawing.Size(57, 40);
      this.pictureBox1.TabIndex = 23;
      this.pictureBox1.TabStop = false;
      // 
      // label1
      // 
      this.label1.Location = new System.Drawing.Point(90, 17);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(168, 35);
      this.label1.TabIndex = 24;
      this.label1.Text = "Please specify the settings for the MouseOnly tracking interface.";
      // 
      // btnOK
      // 
      this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.btnOK.Location = new System.Drawing.Point(102, 111);
      this.btnOK.Name = "btnOK";
      this.btnOK.Size = new System.Drawing.Size(75, 23);
      this.btnOK.TabIndex = 26;
      this.btnOK.Text = "&OK";
      this.btnOK.UseVisualStyleBackColor = true;
      // 
      // btnCancel
      // 
      this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnCancel.Location = new System.Drawing.Point(183, 111);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(75, 23);
      this.btnCancel.TabIndex = 26;
      this.btnCancel.Text = "&Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(9, 71);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(73, 13);
      this.label5.TabIndex = 29;
      this.label5.Text = "Tracking rate:";
      // 
      // cbbSampleRate
      // 
      this.cbbSampleRate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbbSampleRate.FormattingEnabled = true;
      this.cbbSampleRate.Items.AddRange(new object[] {
            "5 Hz",
            "10 Hz",
            "20 Hz",
            "50 Hz",
            "60 Hz",
            "100 Hz",
            "120 Hz",
            "200 Hz",
            "500 Hz",
            "1000 Hz",
            "1200 Hz"});
      this.cbbSampleRate.Location = new System.Drawing.Point(93, 68);
      this.cbbSampleRate.Name = "cbbSampleRate";
      this.cbbSampleRate.Size = new System.Drawing.Size(71, 21);
      this.cbbSampleRate.TabIndex = 30;
      this.cbbSampleRate.SelectedIndexChanged += new System.EventHandler(this.cbbSampleRate_SelectedIndexChanged);
      // 
      // MouseOnlySettingsDlg
      // 
      this.AcceptButton = this.btnOK;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.btnCancel;
      this.ClientSize = new System.Drawing.Size(277, 144);
      this.Controls.Add(this.cbbSampleRate);
      this.Controls.Add(this.label5);
      this.Controls.Add(this.btnCancel);
      this.Controls.Add(this.btnOK);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.pictureBox1);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "MouseOnlySettingsDlg";
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Specify mouse only settings ...";
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.PictureBox pictureBox1;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Button btnOK;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.ComboBox cbbSampleRate;
  }
}