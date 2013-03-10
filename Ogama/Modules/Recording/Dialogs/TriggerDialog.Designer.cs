namespace Ogama.Modules.Recording.Dialogs
{
  using Ogama.Modules.Common.Controls;

  partial class TriggerDialog
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TriggerDialog));
      this.btnOK = new System.Windows.Forms.Button();
      this.chbOverrideSlideTrigger = new System.Windows.Forms.CheckBox();
      this.label1 = new System.Windows.Forms.Label();
      this.btnCancel = new System.Windows.Forms.Button();
      this.triggerControl = new OgamaControls.TriggerControl();
      this.dialogTop1 = new DialogTop();
      this.label2 = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // btnOK
      // 
      this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnOK.AutoSize = true;
      this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.btnOK.Location = new System.Drawing.Point(191, 306);
      this.btnOK.Name = "btnOK";
      this.btnOK.Size = new System.Drawing.Size(70, 23);
      this.btnOK.TabIndex = 3;
      this.btnOK.Text = "&OK";
      this.btnOK.UseVisualStyleBackColor = true;
      // 
      // chbOverrideSlideTrigger
      // 
      this.chbOverrideSlideTrigger.AutoSize = true;
      this.chbOverrideSlideTrigger.Location = new System.Drawing.Point(15, 271);
      this.chbOverrideSlideTrigger.Name = "chbOverrideSlideTrigger";
      this.chbOverrideSlideTrigger.Size = new System.Drawing.Size(274, 17);
      this.chbOverrideSlideTrigger.TabIndex = 7;
      this.chbOverrideSlideTrigger.Text = "This general trigger overrides the slide trigger signals.";
      this.chbOverrideSlideTrigger.UseVisualStyleBackColor = true;
      // 
      // label1
      // 
      this.label1.Location = new System.Drawing.Point(12, 63);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(314, 56);
      this.label1.TabIndex = 8;
      this.label1.Text = resources.GetString("label1.Text");
      // 
      // btnCancel
      // 
      this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnCancel.AutoSize = true;
      this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnCancel.Location = new System.Drawing.Point(266, 306);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(70, 23);
      this.btnCancel.TabIndex = 3;
      this.btnCancel.Text = "Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      // 
      // triggerControl
      // 
      this.triggerControl.Location = new System.Drawing.Point(12, 122);
      this.triggerControl.Name = "triggerControl";
      this.triggerControl.Size = new System.Drawing.Size(225, 143);
      this.triggerControl.TabIndex = 6;
      // 
      // dialogTop1
      // 
      this.dialogTop1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(237)))));
      this.dialogTop1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("dialogTop1.BackgroundImage")));
      this.dialogTop1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
      this.dialogTop1.Description = "Specify a general trigger.";
      this.dialogTop1.Dock = System.Windows.Forms.DockStyle.Top;
      this.dialogTop1.Location = new System.Drawing.Point(0, 0);
      this.dialogTop1.Logo = global::Ogama.Properties.Resources.Event;
      this.dialogTop1.Name = "dialogTop1";
      this.dialogTop1.Size = new System.Drawing.Size(348, 60);
      this.dialogTop1.TabIndex = 5;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(211, 210);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(111, 13);
      this.label2.TabIndex = 9;
      this.label2.Text = "e.g. 0378,D800,DC00";
      // 
      // TriggerDialog
      // 
      this.AcceptButton = this.btnOK;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.btnCancel;
      this.ClientSize = new System.Drawing.Size(348, 341);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.chbOverrideSlideTrigger);
      this.Controls.Add(this.triggerControl);
      this.Controls.Add(this.dialogTop1);
      this.Controls.Add(this.btnCancel);
      this.Controls.Add(this.btnOK);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "TriggerDialog";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Trigger options ...";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button btnOK;
    private DialogTop dialogTop1;
    private OgamaControls.TriggerControl triggerControl;
    private System.Windows.Forms.CheckBox chbOverrideSlideTrigger;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Label label2;
  }
}