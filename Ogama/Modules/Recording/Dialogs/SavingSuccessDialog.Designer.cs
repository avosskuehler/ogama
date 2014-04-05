namespace Ogama.Modules.Recording.Dialogs
{
  using Ogama.Modules.Common.Controls;

  partial class SavingSuccessDialog
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SavingSuccessDialog));
			this.btnYes = new System.Windows.Forms.Button();
			this.dialogTop1 = new DialogTop();
			this.SuspendLayout();
			// 
			// btnYes
			// 
			this.btnYes.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.btnYes.AutoSize = true;
			this.btnYes.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnYes.Location = new System.Drawing.Point(166, 99);
			this.btnYes.Name = "btnYes";
			this.btnYes.Size = new System.Drawing.Size(53, 23);
			this.btnYes.TabIndex = 3;
			this.btnYes.Text = "&OK";
			this.btnYes.UseVisualStyleBackColor = true;
			// 
			// dialogTop1
			// 
			this.dialogTop1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(237)))));
			this.dialogTop1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("dialogTop1.BackgroundImage")));
			this.dialogTop1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.dialogTop1.Description = "The tracking data has been successfully written into OGAMAs database.";
			this.dialogTop1.Dock = System.Windows.Forms.DockStyle.Top;
			this.dialogTop1.Location = new System.Drawing.Point(0, 0);
			this.dialogTop1.Logo = global::Ogama.Properties.Resources.propertiesORoptions;
			this.dialogTop1.Name = "dialogTop1";
			this.dialogTop1.Size = new System.Drawing.Size(394, 60);
			this.dialogTop1.TabIndex = 4;
			// 
			// frmSavingSuccess
			// 
			this.AcceptButton = this.btnYes;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(394, 134);
			this.ControlBox = false;
			this.Controls.Add(this.dialogTop1);
			this.Controls.Add(this.btnYes);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmSavingSuccess";
			this.ShowIcon = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "Success ...";
			this.ResumeLayout(false);
			this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button btnYes;
    private DialogTop dialogTop1;
  }
}