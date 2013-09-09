namespace Ogama.Modules.SlideshowDesign.DesignModule
{
  using Ogama.Modules.Common.Controls;

  partial class PasteAsDialog
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PasteAsDialog));
      this.btnOK = new System.Windows.Forms.Button();
      this.btnCancel = new System.Windows.Forms.Button();
      this.dialogTop1 = new DialogTop();
      this.rdbTarget = new System.Windows.Forms.RadioButton();
      this.rdbStimulus = new System.Windows.Forms.RadioButton();
      this.label1 = new System.Windows.Forms.Label();
      this.txbElement = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // btnOK
      // 
      this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.btnOK.Location = new System.Drawing.Point(208, 182);
      this.btnOK.Name = "btnOK";
      this.btnOK.Size = new System.Drawing.Size(75, 23);
      this.btnOK.TabIndex = 9;
      this.btnOK.Text = "&OK";
      this.btnOK.UseVisualStyleBackColor = true;
      // 
      // btnCancel
      // 
      this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnCancel.Location = new System.Drawing.Point(299, 182);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(75, 23);
      this.btnCancel.TabIndex = 10;
      this.btnCancel.Text = "&Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      // 
      // dialogTop1
      // 
      this.dialogTop1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(237)))));
      this.dialogTop1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("dialogTop1.BackgroundImage")));
      this.dialogTop1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
      this.dialogTop1.Description = "Please specify how to insert the element.";
      this.dialogTop1.Dock = System.Windows.Forms.DockStyle.Top;
      this.dialogTop1.Location = new System.Drawing.Point(0, 0);
      this.dialogTop1.Logo = global::Ogama.Properties.Resources.AOILogo;
      this.dialogTop1.Name = "dialogTop1";
      this.dialogTop1.Size = new System.Drawing.Size(383, 60);
      this.dialogTop1.TabIndex = 11;
      // 
      // rdbTarget
      // 
      this.rdbTarget.AutoSize = true;
      this.rdbTarget.Checked = true;
      this.rdbTarget.Location = new System.Drawing.Point(24, 132);
      this.rdbTarget.Name = "rdbTarget";
      this.rdbTarget.Size = new System.Drawing.Size(96, 17);
      this.rdbTarget.TabIndex = 12;
      this.rdbTarget.TabStop = true;
      this.rdbTarget.Text = "Target element";
      this.rdbTarget.UseVisualStyleBackColor = true;
      // 
      // rdbStimulus
      // 
      this.rdbStimulus.AutoSize = true;
      this.rdbStimulus.Location = new System.Drawing.Point(24, 155);
      this.rdbStimulus.Name = "rdbStimulus";
      this.rdbStimulus.Size = new System.Drawing.Size(104, 17);
      this.rdbStimulus.TabIndex = 12;
      this.rdbStimulus.Text = "Stimulus element";
      this.rdbStimulus.UseVisualStyleBackColor = true;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(21, 73);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(165, 13);
      this.label1.TabIndex = 13;
      this.label1.Text = "Insert the vector graphic element:";
      // 
      // txbElement
      // 
      this.txbElement.Location = new System.Drawing.Point(24, 89);
      this.txbElement.Name = "txbElement";
      this.txbElement.ReadOnly = true;
      this.txbElement.Size = new System.Drawing.Size(350, 20);
      this.txbElement.TabIndex = 14;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(21, 112);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(27, 13);
      this.label2.TabIndex = 13;
      this.label2.Text = "as a";
      // 
      // PasteAsDialog
      // 
      this.AcceptButton = this.btnOK;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.btnCancel;
      this.ClientSize = new System.Drawing.Size(383, 214);
      this.Controls.Add(this.txbElement);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.rdbStimulus);
      this.Controls.Add(this.rdbTarget);
      this.Controls.Add(this.dialogTop1);
      this.Controls.Add(this.btnCancel);
      this.Controls.Add(this.btnOK);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "PasteAsDialog";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Please choose option";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button btnOK;
    private System.Windows.Forms.Button btnCancel;
    private DialogTop dialogTop1;
    private System.Windows.Forms.RadioButton rdbTarget;
    private System.Windows.Forms.RadioButton rdbStimulus;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox txbElement;
    private System.Windows.Forms.Label label2;
  }
}