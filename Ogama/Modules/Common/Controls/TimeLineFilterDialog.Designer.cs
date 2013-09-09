namespace Ogama.Modules.Common.Controls
{
  partial class TimeLineFilterDialog
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TimeLineFilterDialog));
      this.dialogTop1 = new DialogTop();
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      this.btnOK = new System.Windows.Forms.Button();
      this.chbSound = new System.Windows.Forms.CheckBox();
      this.chbKeys = new System.Windows.Forms.CheckBox();
      this.chbFlash = new System.Windows.Forms.CheckBox();
      this.chbMouseDown = new System.Windows.Forms.CheckBox();
      this.chbMouseUp = new System.Windows.Forms.CheckBox();
      this.chbScroll = new System.Windows.Forms.CheckBox();
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      this.SuspendLayout();
      // 
      // dialogTop1
      // 
      this.dialogTop1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(237)))));
      this.dialogTop1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("dialogTop1.BackgroundImage")));
      this.dialogTop1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
      this.dialogTop1.Description = "Specify displayed trial events";
      this.dialogTop1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.dialogTop1.Location = new System.Drawing.Point(0, 0);
      this.dialogTop1.Logo = global::Ogama.Properties.Resources.Event;
      this.dialogTop1.Name = "dialogTop1";
      this.dialogTop1.Size = new System.Drawing.Size(261, 60);
      this.dialogTop1.TabIndex = 0;
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
      this.splitContainer1.Panel2.Controls.Add(this.chbScroll);
      this.splitContainer1.Panel2.Controls.Add(this.btnOK);
      this.splitContainer1.Panel2.Controls.Add(this.chbSound);
      this.splitContainer1.Panel2.Controls.Add(this.chbKeys);
      this.splitContainer1.Panel2.Controls.Add(this.chbFlash);
      this.splitContainer1.Panel2.Controls.Add(this.chbMouseDown);
      this.splitContainer1.Panel2.Controls.Add(this.chbMouseUp);
      this.splitContainer1.Size = new System.Drawing.Size(261, 237);
      this.splitContainer1.SplitterDistance = 60;
      this.splitContainer1.TabIndex = 1;
      // 
      // btnOK
      // 
      this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.btnOK.Location = new System.Drawing.Point(189, 138);
      this.btnOK.Name = "btnOK";
      this.btnOK.Size = new System.Drawing.Size(60, 23);
      this.btnOK.TabIndex = 1;
      this.btnOK.Text = "OK";
      this.btnOK.UseVisualStyleBackColor = true;
      // 
      // chbSound
      // 
      this.chbSound.AutoSize = true;
      this.chbSound.Location = new System.Drawing.Point(31, 116);
      this.chbSound.Name = "chbSound";
      this.chbSound.Size = new System.Drawing.Size(57, 17);
      this.chbSound.TabIndex = 0;
      this.chbSound.Text = "Sound";
      this.chbSound.UseVisualStyleBackColor = true;
      // 
      // chbKeys
      // 
      this.chbKeys.AutoSize = true;
      this.chbKeys.Location = new System.Drawing.Point(31, 94);
      this.chbKeys.Name = "chbKeys";
      this.chbKeys.Size = new System.Drawing.Size(44, 17);
      this.chbKeys.TabIndex = 0;
      this.chbKeys.Text = "Key";
      this.chbKeys.UseVisualStyleBackColor = true;
      // 
      // chbFlash
      // 
      this.chbFlash.AutoSize = true;
      this.chbFlash.Location = new System.Drawing.Point(31, 71);
      this.chbFlash.Name = "chbFlash";
      this.chbFlash.Size = new System.Drawing.Size(51, 17);
      this.chbFlash.TabIndex = 0;
      this.chbFlash.Text = "Flash";
      this.chbFlash.UseVisualStyleBackColor = true;
      // 
      // chbMouseDown
      // 
      this.chbMouseDown.AutoSize = true;
      this.chbMouseDown.Location = new System.Drawing.Point(31, 48);
      this.chbMouseDown.Name = "chbMouseDown";
      this.chbMouseDown.Size = new System.Drawing.Size(86, 17);
      this.chbMouseDown.TabIndex = 0;
      this.chbMouseDown.Text = "MouseDown";
      this.chbMouseDown.UseVisualStyleBackColor = true;
      // 
      // chbMouseUp
      // 
      this.chbMouseUp.AutoSize = true;
      this.chbMouseUp.Location = new System.Drawing.Point(31, 25);
      this.chbMouseUp.Name = "chbMouseUp";
      this.chbMouseUp.Size = new System.Drawing.Size(72, 17);
      this.chbMouseUp.TabIndex = 0;
      this.chbMouseUp.Text = "MouseUp";
      this.chbMouseUp.UseVisualStyleBackColor = true;
      // 
      // chbScroll
      // 
      this.chbScroll.AutoSize = true;
      this.chbScroll.Location = new System.Drawing.Point(31, 139);
      this.chbScroll.Name = "chbScroll";
      this.chbScroll.Size = new System.Drawing.Size(52, 17);
      this.chbScroll.TabIndex = 2;
      this.chbScroll.Text = "Scroll";
      this.chbScroll.UseVisualStyleBackColor = true;
      // 
      // TimeLineFilterDialog
      // 
      this.AcceptButton = this.btnOK;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(261, 237);
      this.Controls.Add(this.splitContainer1);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "TimeLineFilterDialog";
      this.Text = "Filter events";
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel2.ResumeLayout(false);
      this.splitContainer1.Panel2.PerformLayout();
      this.splitContainer1.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private DialogTop dialogTop1;
    private System.Windows.Forms.SplitContainer splitContainer1;
    private System.Windows.Forms.CheckBox chbKeys;
    private System.Windows.Forms.CheckBox chbFlash;
    private System.Windows.Forms.CheckBox chbMouseDown;
    private System.Windows.Forms.CheckBox chbMouseUp;
    private System.Windows.Forms.Button btnOK;
    private System.Windows.Forms.CheckBox chbSound;
    private System.Windows.Forms.CheckBox chbScroll;
  }
}