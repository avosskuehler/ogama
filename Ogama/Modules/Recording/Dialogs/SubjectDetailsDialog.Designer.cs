namespace Ogama.Modules.Recording.Dialogs
{
  using Ogama.Modules.Common.Controls;

  partial class SubjectDetailsDialog
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SubjectDetailsDialog));
      this.txbSubjectName = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.btnOK = new System.Windows.Forms.Button();
      this.btnCancel = new System.Windows.Forms.Button();
      this.label3 = new System.Windows.Forms.Label();
      this.label4 = new System.Windows.Forms.Label();
      this.label5 = new System.Windows.Forms.Label();
      this.label6 = new System.Windows.Forms.Label();
      this.label7 = new System.Windows.Forms.Label();
      this.label8 = new System.Windows.Forms.Label();
      this.label9 = new System.Windows.Forms.Label();
      this.label10 = new System.Windows.Forms.Label();
      this.cbbCategory = new System.Windows.Forms.ComboBox();
      this.cbbSex = new System.Windows.Forms.ComboBox();
      this.cbbHandedness = new System.Windows.Forms.ComboBox();
      this.txbParam1 = new System.Windows.Forms.TextBox();
      this.txbParam2 = new System.Windows.Forms.TextBox();
      this.txbParam3 = new System.Windows.Forms.TextBox();
      this.txbComments = new System.Windows.Forms.TextBox();
      this.txbAge = new System.Windows.Forms.TextBox();
      this.dialogTop1 = new DialogTop();
      this.SuspendLayout();
      // 
      // txbSubjectName
      // 
      this.txbSubjectName.Location = new System.Drawing.Point(91, 72);
      this.txbSubjectName.Name = "txbSubjectName";
      this.txbSubjectName.Size = new System.Drawing.Size(288, 20);
      this.txbSubjectName.TabIndex = 0;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(13, 74);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(72, 13);
      this.label1.TabIndex = 1;
      this.label1.Text = "Subject name";
      // 
      // btnOK
      // 
      this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.btnOK.Location = new System.Drawing.Point(213, 253);
      this.btnOK.Name = "btnOK";
      this.btnOK.Size = new System.Drawing.Size(75, 23);
      this.btnOK.TabIndex = 9;
      this.btnOK.Text = "&OK";
      this.btnOK.UseVisualStyleBackColor = true;
      this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
      // 
      // btnCancel
      // 
      this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnCancel.Location = new System.Drawing.Point(304, 253);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(75, 23);
      this.btnCancel.TabIndex = 10;
      this.btnCancel.Text = "&Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(13, 100);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(49, 13);
      this.label3.TabIndex = 5;
      this.label3.Text = "Category";
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(13, 126);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(26, 13);
      this.label4.TabIndex = 5;
      this.label4.Text = "Age";
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(13, 152);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(25, 13);
      this.label5.TabIndex = 5;
      this.label5.Text = "Sex";
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Location = new System.Drawing.Point(13, 178);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(67, 13);
      this.label6.TabIndex = 5;
      this.label6.Text = "Handedness";
      // 
      // label7
      // 
      this.label7.AutoSize = true;
      this.label7.Location = new System.Drawing.Point(13, 204);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(56, 13);
      this.label7.TabIndex = 5;
      this.label7.Text = "Comments";
      // 
      // label8
      // 
      this.label8.AutoSize = true;
      this.label8.Location = new System.Drawing.Point(230, 103);
      this.label8.Name = "label8";
      this.label8.Size = new System.Drawing.Size(43, 13);
      this.label8.TabIndex = 5;
      this.label8.Text = "Param1";
      this.label8.Visible = false;
      // 
      // label9
      // 
      this.label9.AutoSize = true;
      this.label9.Location = new System.Drawing.Point(230, 153);
      this.label9.Name = "label9";
      this.label9.Size = new System.Drawing.Size(43, 13);
      this.label9.TabIndex = 5;
      this.label9.Text = "Param3";
      this.label9.Visible = false;
      // 
      // label10
      // 
      this.label10.AutoSize = true;
      this.label10.Location = new System.Drawing.Point(230, 128);
      this.label10.Name = "label10";
      this.label10.Size = new System.Drawing.Size(43, 13);
      this.label10.TabIndex = 5;
      this.label10.Text = "Param2";
      this.label10.Visible = false;
      // 
      // cbbCategory
      // 
      this.cbbCategory.FormattingEnabled = true;
      this.cbbCategory.Items.AddRange(new object[] {
            "not specified"});
      this.cbbCategory.Location = new System.Drawing.Point(91, 97);
      this.cbbCategory.Name = "cbbCategory";
      this.cbbCategory.Size = new System.Drawing.Size(120, 21);
      this.cbbCategory.TabIndex = 1;
      this.cbbCategory.Text = "not specified";
      // 
      // cbbSex
      // 
      this.cbbSex.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbbSex.FormattingEnabled = true;
      this.cbbSex.Items.AddRange(new object[] {
            "not specified",
            "female",
            "male"});
      this.cbbSex.Location = new System.Drawing.Point(91, 148);
      this.cbbSex.Name = "cbbSex";
      this.cbbSex.Size = new System.Drawing.Size(120, 21);
      this.cbbSex.TabIndex = 3;
      // 
      // cbbHandedness
      // 
      this.cbbHandedness.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbbHandedness.FormattingEnabled = true;
      this.cbbHandedness.Items.AddRange(new object[] {
            "not specified",
            "left",
            "right"});
      this.cbbHandedness.Location = new System.Drawing.Point(91, 174);
      this.cbbHandedness.Name = "cbbHandedness";
      this.cbbHandedness.Size = new System.Drawing.Size(120, 21);
      this.cbbHandedness.TabIndex = 4;
      // 
      // txbParam1
      // 
      this.txbParam1.Location = new System.Drawing.Point(279, 98);
      this.txbParam1.Name = "txbParam1";
      this.txbParam1.Size = new System.Drawing.Size(100, 20);
      this.txbParam1.TabIndex = 6;
      this.txbParam1.Visible = false;
      // 
      // txbParam2
      // 
      this.txbParam2.Location = new System.Drawing.Point(279, 123);
      this.txbParam2.Name = "txbParam2";
      this.txbParam2.Size = new System.Drawing.Size(100, 20);
      this.txbParam2.TabIndex = 7;
      this.txbParam2.Visible = false;
      // 
      // txbParam3
      // 
      this.txbParam3.Location = new System.Drawing.Point(279, 149);
      this.txbParam3.Name = "txbParam3";
      this.txbParam3.Size = new System.Drawing.Size(100, 20);
      this.txbParam3.TabIndex = 8;
      this.txbParam3.Visible = false;
      // 
      // txbComments
      // 
      this.txbComments.Location = new System.Drawing.Point(91, 200);
      this.txbComments.Multiline = true;
      this.txbComments.Name = "txbComments";
      this.txbComments.Size = new System.Drawing.Size(288, 47);
      this.txbComments.TabIndex = 5;
      // 
      // txbAge
      // 
      this.txbAge.Location = new System.Drawing.Point(91, 123);
      this.txbAge.Name = "txbAge";
      this.txbAge.Size = new System.Drawing.Size(120, 20);
      this.txbAge.TabIndex = 2;
      this.txbAge.Text = "not specified";
      this.txbAge.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txbAge_KeyDown);
      // 
      // dialogTop1
      // 
      this.dialogTop1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(237)))));
      this.dialogTop1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("dialogTop1.BackgroundImage")));
      this.dialogTop1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
      this.dialogTop1.Description = "Please specify a unique subject name and if needed the other fields.";
      this.dialogTop1.Dock = System.Windows.Forms.DockStyle.Top;
      this.dialogTop1.Location = new System.Drawing.Point(0, 0);
      this.dialogTop1.Logo = global::Ogama.Properties.Resources.user1;
      this.dialogTop1.Name = "dialogTop1";
      this.dialogTop1.Size = new System.Drawing.Size(389, 60);
      this.dialogTop1.TabIndex = 11;
      // 
      // SubjectDetailsDialog
      // 
      this.AcceptButton = this.btnOK;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.btnCancel;
      this.ClientSize = new System.Drawing.Size(389, 284);
      this.Controls.Add(this.dialogTop1);
      this.Controls.Add(this.txbAge);
      this.Controls.Add(this.txbComments);
      this.Controls.Add(this.txbParam3);
      this.Controls.Add(this.txbParam2);
      this.Controls.Add(this.txbParam1);
      this.Controls.Add(this.cbbHandedness);
      this.Controls.Add(this.cbbSex);
      this.Controls.Add(this.cbbCategory);
      this.Controls.Add(this.label10);
      this.Controls.Add(this.label9);
      this.Controls.Add(this.label8);
      this.Controls.Add(this.label7);
      this.Controls.Add(this.label6);
      this.Controls.Add(this.label5);
      this.Controls.Add(this.label4);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.btnCancel);
      this.Controls.Add(this.btnOK);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.txbSubjectName);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "SubjectDetailsDialog";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Please enter subject details ...";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmAskForSubjectName_FormClosing);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox txbSubjectName;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Button btnOK;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.Label label8;
    private System.Windows.Forms.Label label9;
    private System.Windows.Forms.Label label10;
    private System.Windows.Forms.ComboBox cbbCategory;
    private System.Windows.Forms.ComboBox cbbSex;
    private System.Windows.Forms.ComboBox cbbHandedness;
    private System.Windows.Forms.TextBox txbParam1;
    private System.Windows.Forms.TextBox txbParam2;
    private System.Windows.Forms.TextBox txbParam3;
    private System.Windows.Forms.TextBox txbComments;
    private System.Windows.Forms.TextBox txbAge;
    private DialogTop dialogTop1;
  }
}