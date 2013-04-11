namespace Ogama.ExceptionHandling
{
  using Ogama.Modules.Common.Controls;

  partial class InformationDialog
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InformationDialog));
      this.txbMessage = new System.Windows.Forms.TextBox();
      this.btnOK = new System.Windows.Forms.Button();
      this.dialogTop1 = new DialogTop();
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      this.splitContainer2 = new System.Windows.Forms.SplitContainer();
      this.spcYesNoCancelOK = new System.Windows.Forms.SplitContainer();
      this.btnCancel = new System.Windows.Forms.Button();
      this.btnNo = new System.Windows.Forms.Button();
      this.btnYes = new System.Windows.Forms.Button();
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      this.splitContainer2.Panel1.SuspendLayout();
      this.splitContainer2.Panel2.SuspendLayout();
      this.splitContainer2.SuspendLayout();
      this.spcYesNoCancelOK.Panel1.SuspendLayout();
      this.spcYesNoCancelOK.Panel2.SuspendLayout();
      this.spcYesNoCancelOK.SuspendLayout();
      this.SuspendLayout();
      // 
      // txbMessage
      // 
      this.txbMessage.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txbMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txbMessage.Location = new System.Drawing.Point(0, 0);
      this.txbMessage.Margin = new System.Windows.Forms.Padding(0);
      this.txbMessage.Multiline = true;
      this.txbMessage.Name = "txbMessage";
      this.txbMessage.Size = new System.Drawing.Size(393, 185);
      this.txbMessage.TabIndex = 2;
      // 
      // btnOK
      // 
      this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnOK.AutoSize = true;
      this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.btnOK.Location = new System.Drawing.Point(84, 3);
      this.btnOK.Name = "btnOK";
      this.btnOK.Size = new System.Drawing.Size(65, 23);
      this.btnOK.TabIndex = 3;
      this.btnOK.Text = "&OK";
      this.btnOK.UseVisualStyleBackColor = true;
      // 
      // dialogTop1
      // 
      this.dialogTop1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(237)))));
      this.dialogTop1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("dialogTop1.BackgroundImage")));
      this.dialogTop1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
      this.dialogTop1.Description = "";
      this.dialogTop1.Dock = System.Windows.Forms.DockStyle.Top;
      this.dialogTop1.Location = new System.Drawing.Point(0, 0);
      this.dialogTop1.Logo = global::Ogama.Properties.Resources.Ogama;
      this.dialogTop1.Name = "dialogTop1";
      this.dialogTop1.Size = new System.Drawing.Size(393, 60);
      this.dialogTop1.TabIndex = 5;
      this.dialogTop1.TabStop = false;
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
      this.splitContainer1.Size = new System.Drawing.Size(393, 280);
      this.splitContainer1.SplitterDistance = 60;
      this.splitContainer1.SplitterWidth = 1;
      this.splitContainer1.TabIndex = 6;
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
      this.splitContainer2.Panel1.Controls.Add(this.txbMessage);
      // 
      // splitContainer2.Panel2
      // 
      this.splitContainer2.Panel2.Controls.Add(this.spcYesNoCancelOK);
      this.splitContainer2.Panel2MinSize = 30;
      this.splitContainer2.Size = new System.Drawing.Size(393, 219);
      this.splitContainer2.SplitterDistance = 185;
      this.splitContainer2.TabIndex = 0;
      // 
      // spcYesNoCancelOK
      // 
      this.spcYesNoCancelOK.Dock = System.Windows.Forms.DockStyle.Fill;
      this.spcYesNoCancelOK.Location = new System.Drawing.Point(0, 0);
      this.spcYesNoCancelOK.Name = "spcYesNoCancelOK";
      // 
      // spcYesNoCancelOK.Panel1
      // 
      this.spcYesNoCancelOK.Panel1.Controls.Add(this.btnCancel);
      this.spcYesNoCancelOK.Panel1.Controls.Add(this.btnNo);
      this.spcYesNoCancelOK.Panel1.Controls.Add(this.btnYes);
      // 
      // spcYesNoCancelOK.Panel2
      // 
      this.spcYesNoCancelOK.Panel2.Controls.Add(this.btnOK);
      this.spcYesNoCancelOK.Size = new System.Drawing.Size(393, 30);
      this.spcYesNoCancelOK.SplitterDistance = 237;
      this.spcYesNoCancelOK.TabIndex = 4;
      // 
      // btnCancel
      // 
      this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnCancel.AutoSize = true;
      this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnCancel.Location = new System.Drawing.Point(169, 3);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(65, 23);
      this.btnCancel.TabIndex = 3;
      this.btnCancel.Text = "&Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      // 
      // btnNo
      // 
      this.btnNo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnNo.AutoSize = true;
      this.btnNo.DialogResult = System.Windows.Forms.DialogResult.No;
      this.btnNo.Location = new System.Drawing.Point(96, 3);
      this.btnNo.Name = "btnNo";
      this.btnNo.Size = new System.Drawing.Size(65, 23);
      this.btnNo.TabIndex = 3;
      this.btnNo.Text = "&No";
      this.btnNo.UseVisualStyleBackColor = true;
      // 
      // btnYes
      // 
      this.btnYes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnYes.AutoSize = true;
      this.btnYes.DialogResult = System.Windows.Forms.DialogResult.Yes;
      this.btnYes.Location = new System.Drawing.Point(23, 3);
      this.btnYes.Name = "btnYes";
      this.btnYes.Size = new System.Drawing.Size(65, 23);
      this.btnYes.TabIndex = 3;
      this.btnYes.Text = "&Yes";
      this.btnYes.UseVisualStyleBackColor = true;
      // 
      // InformationDialog
      // 
      this.AcceptButton = this.btnOK;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.btnCancel;
      this.ClientSize = new System.Drawing.Size(393, 280);
      this.Controls.Add(this.splitContainer1);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "InformationDialog";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Ogama Message ...";
      this.TopMost = true;
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel2.ResumeLayout(false);
      this.splitContainer1.ResumeLayout(false);
      this.splitContainer2.Panel1.ResumeLayout(false);
      this.splitContainer2.Panel1.PerformLayout();
      this.splitContainer2.Panel2.ResumeLayout(false);
      this.splitContainer2.ResumeLayout(false);
      this.spcYesNoCancelOK.Panel1.ResumeLayout(false);
      this.spcYesNoCancelOK.Panel1.PerformLayout();
      this.spcYesNoCancelOK.Panel2.ResumeLayout(false);
      this.spcYesNoCancelOK.Panel2.PerformLayout();
      this.spcYesNoCancelOK.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Button btnOK;
    private System.Windows.Forms.TextBox txbMessage;
    private DialogTop dialogTop1;
    private System.Windows.Forms.SplitContainer splitContainer1;
    private System.Windows.Forms.SplitContainer splitContainer2;
    private System.Windows.Forms.SplitContainer spcYesNoCancelOK;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.Button btnNo;
    private System.Windows.Forms.Button btnYes;
  }
}