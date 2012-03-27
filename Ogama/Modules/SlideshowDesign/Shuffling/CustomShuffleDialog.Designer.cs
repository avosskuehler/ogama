namespace Ogama.Modules.SlideshowDesign.Shuffling
{
  using Ogama.Modules.Common.Controls;

  partial class CustomShuffleDialog
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CustomShuffleDialog));
      this.btnOK = new System.Windows.Forms.Button();
      this.btnCancel = new System.Windows.Forms.Button();
      this.label1 = new System.Windows.Forms.Label();
      this.chbShuffleSections = new System.Windows.Forms.CheckBox();
      this.nudCountSectionItemsInGroup = new System.Windows.Forms.NumericUpDown();
      this.label2 = new System.Windows.Forms.Label();
      this.chbShuffleGroups = new System.Windows.Forms.CheckBox();
      this.label3 = new System.Windows.Forms.Label();
      this.rdbEnableShuffling = new System.Windows.Forms.RadioButton();
      this.rdbDisableShuffling = new System.Windows.Forms.RadioButton();
      this.grpProperties = new System.Windows.Forms.GroupBox();
      this.txbParentNodeName = new System.Windows.Forms.TextBox();
      this.chbShuffleInsideGroups = new System.Windows.Forms.CheckBox();
      this.trvSlideshow = new System.Windows.Forms.TreeView();
      this.dialogTop1 = new DialogTop();
      ((System.ComponentModel.ISupportInitialize)(this.nudCountSectionItemsInGroup)).BeginInit();
      this.grpProperties.SuspendLayout();
      this.SuspendLayout();
      // 
      // btnOK
      // 
      this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.btnOK.Location = new System.Drawing.Point(407, 381);
      this.btnOK.Name = "btnOK";
      this.btnOK.Size = new System.Drawing.Size(65, 25);
      this.btnOK.TabIndex = 23;
      this.btnOK.Text = "OK";
      this.btnOK.UseVisualStyleBackColor = true;
      // 
      // btnCancel
      // 
      this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnCancel.Location = new System.Drawing.Point(478, 381);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(65, 25);
      this.btnCancel.TabIndex = 22;
      this.btnCancel.Text = "Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(6, 16);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(291, 13);
      this.label1.TabIndex = 25;
      this.label1.Text = "Please select the node that is the parent of the trial sections.";
      // 
      // chbShuffleSections
      // 
      this.chbShuffleSections.AutoSize = true;
      this.chbShuffleSections.Checked = true;
      this.chbShuffleSections.CheckState = System.Windows.Forms.CheckState.Checked;
      this.chbShuffleSections.Location = new System.Drawing.Point(9, 88);
      this.chbShuffleSections.Name = "chbShuffleSections";
      this.chbShuffleSections.Size = new System.Drawing.Size(260, 17);
      this.chbShuffleSections.TabIndex = 26;
      this.chbShuffleSections.Text = "Shuffle section items before composing to groups.";
      this.chbShuffleSections.UseVisualStyleBackColor = true;
      // 
      // nudCountSectionItemsInGroup
      // 
      this.nudCountSectionItemsInGroup.Location = new System.Drawing.Point(9, 62);
      this.nudCountSectionItemsInGroup.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nudCountSectionItemsInGroup.Name = "nudCountSectionItemsInGroup";
      this.nudCountSectionItemsInGroup.Size = new System.Drawing.Size(43, 20);
      this.nudCountSectionItemsInGroup.TabIndex = 27;
      this.nudCountSectionItemsInGroup.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(59, 64);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(288, 13);
      this.label2.TabIndex = 25;
      this.label2.Text = "Number of items of each section to be combined in a group.";
      // 
      // chbShuffleGroups
      // 
      this.chbShuffleGroups.AutoSize = true;
      this.chbShuffleGroups.Checked = true;
      this.chbShuffleGroups.CheckState = System.Windows.Forms.CheckState.Checked;
      this.chbShuffleGroups.Location = new System.Drawing.Point(9, 134);
      this.chbShuffleGroups.Name = "chbShuffleGroups";
      this.chbShuffleGroups.Size = new System.Drawing.Size(179, 17);
      this.chbShuffleGroups.TabIndex = 28;
      this.chbShuffleGroups.Text = "Shuffle newly composed groups.";
      this.chbShuffleGroups.UseVisualStyleBackColor = true;
      // 
      // label3
      // 
      this.label3.Location = new System.Drawing.Point(12, 63);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(356, 79);
      this.label3.TabIndex = 29;
      this.label3.Text = resources.GetString("label3.Text");
      // 
      // rdbEnableShuffling
      // 
      this.rdbEnableShuffling.AutoSize = true;
      this.rdbEnableShuffling.Location = new System.Drawing.Point(30, 145);
      this.rdbEnableShuffling.Name = "rdbEnableShuffling";
      this.rdbEnableShuffling.Size = new System.Drawing.Size(137, 17);
      this.rdbEnableShuffling.TabIndex = 30;
      this.rdbEnableShuffling.Text = "Enable custom shuffling";
      this.rdbEnableShuffling.UseVisualStyleBackColor = true;
      this.rdbEnableShuffling.CheckedChanged += new System.EventHandler(this.rdbEnableShuffling_CheckedChanged);
      // 
      // rdbDisableShuffling
      // 
      this.rdbDisableShuffling.AutoSize = true;
      this.rdbDisableShuffling.Checked = true;
      this.rdbDisableShuffling.Location = new System.Drawing.Point(30, 168);
      this.rdbDisableShuffling.Name = "rdbDisableShuffling";
      this.rdbDisableShuffling.Size = new System.Drawing.Size(139, 17);
      this.rdbDisableShuffling.TabIndex = 30;
      this.rdbDisableShuffling.TabStop = true;
      this.rdbDisableShuffling.Text = "Disable custom shuffling";
      this.rdbDisableShuffling.UseVisualStyleBackColor = true;
      // 
      // grpProperties
      // 
      this.grpProperties.Controls.Add(this.txbParentNodeName);
      this.grpProperties.Controls.Add(this.label1);
      this.grpProperties.Controls.Add(this.label2);
      this.grpProperties.Controls.Add(this.chbShuffleSections);
      this.grpProperties.Controls.Add(this.chbShuffleInsideGroups);
      this.grpProperties.Controls.Add(this.chbShuffleGroups);
      this.grpProperties.Controls.Add(this.nudCountSectionItemsInGroup);
      this.grpProperties.Location = new System.Drawing.Point(15, 191);
      this.grpProperties.Name = "grpProperties";
      this.grpProperties.Size = new System.Drawing.Size(353, 166);
      this.grpProperties.TabIndex = 31;
      this.grpProperties.TabStop = false;
      this.grpProperties.Text = "Properties";
      this.grpProperties.Visible = false;
      // 
      // txbParentNodeName
      // 
      this.txbParentNodeName.Location = new System.Drawing.Point(27, 35);
      this.txbParentNodeName.Name = "txbParentNodeName";
      this.txbParentNodeName.Size = new System.Drawing.Size(100, 20);
      this.txbParentNodeName.TabIndex = 29;
      // 
      // chbShuffleInsideGroups
      // 
      this.chbShuffleInsideGroups.AutoSize = true;
      this.chbShuffleInsideGroups.Checked = true;
      this.chbShuffleInsideGroups.CheckState = System.Windows.Forms.CheckState.Checked;
      this.chbShuffleInsideGroups.Location = new System.Drawing.Point(9, 111);
      this.chbShuffleInsideGroups.Name = "chbShuffleInsideGroups";
      this.chbShuffleInsideGroups.Size = new System.Drawing.Size(217, 17);
      this.chbShuffleInsideGroups.TabIndex = 28;
      this.chbShuffleInsideGroups.Text = "Shuffle items in newly composed groups.";
      this.chbShuffleInsideGroups.UseVisualStyleBackColor = true;
      // 
      // trvSlideshow
      // 
      this.trvSlideshow.Location = new System.Drawing.Point(374, 66);
      this.trvSlideshow.Name = "trvSlideshow";
      this.trvSlideshow.Size = new System.Drawing.Size(169, 291);
      this.trvSlideshow.TabIndex = 32;
      this.trvSlideshow.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.trvSlideshow_AfterSelect);
      // 
      // dialogTop1
      // 
      this.dialogTop1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(237)))));
      this.dialogTop1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("dialogTop1.BackgroundImage")));
      this.dialogTop1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
      this.dialogTop1.Description = "Specify custom shuffling for the slideshow.";
      this.dialogTop1.Dock = System.Windows.Forms.DockStyle.Top;
      this.dialogTop1.Location = new System.Drawing.Point(0, 0);
      this.dialogTop1.Logo = global::Ogama.Properties.Resources.DiceHS;
      this.dialogTop1.Name = "dialogTop1";
      this.dialogTop1.Size = new System.Drawing.Size(555, 60);
      this.dialogTop1.TabIndex = 0;
      // 
      // CustomShuffleDialog
      // 
      this.AcceptButton = this.btnOK;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.btnCancel;
      this.ClientSize = new System.Drawing.Size(555, 418);
      this.Controls.Add(this.trvSlideshow);
      this.Controls.Add(this.grpProperties);
      this.Controls.Add(this.rdbDisableShuffling);
      this.Controls.Add(this.rdbEnableShuffling);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.btnOK);
      this.Controls.Add(this.btnCancel);
      this.Controls.Add(this.dialogTop1);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "CustomShuffleDialog";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Custom shuffling ...";
      ((System.ComponentModel.ISupportInitialize)(this.nudCountSectionItemsInGroup)).EndInit();
      this.grpProperties.ResumeLayout(false);
      this.grpProperties.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

		}

		#endregion

		private DialogTop dialogTop1;
		private System.Windows.Forms.Button btnOK;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.CheckBox chbShuffleSections;
    private System.Windows.Forms.NumericUpDown nudCountSectionItemsInGroup;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.CheckBox chbShuffleGroups;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.RadioButton rdbEnableShuffling;
    private System.Windows.Forms.RadioButton rdbDisableShuffling;
    private System.Windows.Forms.GroupBox grpProperties;
    private System.Windows.Forms.TreeView trvSlideshow;
    private System.Windows.Forms.TextBox txbParentNodeName;
    private System.Windows.Forms.CheckBox chbShuffleInsideGroups;
	}
}