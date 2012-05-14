namespace Ogama.Modules.SlideshowDesign.DesignModule.StimuliDialogs
{
  using Ogama.Modules.Common.Controls;

  partial class DesktopDialog
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
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DesktopDialog));
      this.btnOK = new System.Windows.Forms.Button();
      this.btnCancel = new System.Windows.Forms.Button();
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.txbName = new System.Windows.Forms.TextBox();
      this.label5 = new System.Windows.Forms.Label();
      this.cbbCategory = new System.Windows.Forms.ComboBox();
      this.label10 = new System.Windows.Forms.Label();
      this.grpImageProperties = new System.Windows.Forms.GroupBox();
      this.btnRemoveCondition = new System.Windows.Forms.Button();
      this.imlCommands = new System.Windows.Forms.ImageList(this.components);
      this.btnAddCondition = new System.Windows.Forms.Button();
      this.lsbStopConditions = new System.Windows.Forms.ListBox();
      this.cbbKeys = new System.Windows.Forms.ComboBox();
      this.label3 = new System.Windows.Forms.Label();
      this.nudTime = new System.Windows.Forms.NumericUpDown();
      this.rdbTime = new System.Windows.Forms.RadioButton();
      this.rdbKey = new System.Windows.Forms.RadioButton();
      this.btnHelp = new System.Windows.Forms.Button();
      this.panel1 = new System.Windows.Forms.Panel();
      this.dialogTop1 = new Ogama.Modules.Common.Controls.DialogTop();
      this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      this.groupBox1.SuspendLayout();
      this.grpImageProperties.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.nudTime)).BeginInit();
      this.panel1.SuspendLayout();
      this.SuspendLayout();
      // 
      // btnOK
      // 
      this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.btnOK.Location = new System.Drawing.Point(179, 3);
      this.btnOK.Name = "btnOK";
      this.btnOK.Size = new System.Drawing.Size(65, 25);
      this.btnOK.TabIndex = 21;
      this.btnOK.Text = "OK";
      this.btnOK.UseVisualStyleBackColor = true;
      this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
      // 
      // btnCancel
      // 
      this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnCancel.Location = new System.Drawing.Point(256, 3);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(65, 25);
      this.btnCancel.TabIndex = 20;
      this.btnCancel.Text = "Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      // 
      // splitContainer1
      // 
      this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
      this.splitContainer1.IsSplitterFixed = true;
      this.splitContainer1.Location = new System.Drawing.Point(3, 3);
      this.splitContainer1.Name = "splitContainer1";
      this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // splitContainer1.Panel1
      // 
      this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
      this.splitContainer1.Panel1.Controls.Add(this.grpImageProperties);
      // 
      // splitContainer1.Panel2
      // 
      this.splitContainer1.Panel2.Controls.Add(this.btnHelp);
      this.splitContainer1.Panel2.Controls.Add(this.btnOK);
      this.splitContainer1.Panel2.Controls.Add(this.btnCancel);
      this.splitContainer1.Size = new System.Drawing.Size(324, 316);
      this.splitContainer1.SplitterDistance = 282;
      this.splitContainer1.TabIndex = 23;
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.txbName);
      this.groupBox1.Controls.Add(this.label5);
      this.groupBox1.Controls.Add(this.cbbCategory);
      this.groupBox1.Controls.Add(this.label10);
      this.groupBox1.Location = new System.Drawing.Point(9, 12);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(291, 90);
      this.groupBox1.TabIndex = 35;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Name and category ...";
      // 
      // txbName
      // 
      this.txbName.Location = new System.Drawing.Point(99, 19);
      this.txbName.Name = "txbName";
      this.txbName.Size = new System.Drawing.Size(131, 20);
      this.txbName.TabIndex = 31;
      this.txbName.Text = "InstructionSlide";
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label5.Location = new System.Drawing.Point(6, 21);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(89, 19);
      this.label5.TabIndex = 30;
      this.label5.Text = "Slidename";
      // 
      // cbbCategory
      // 
      this.cbbCategory.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
      this.cbbCategory.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
      this.cbbCategory.FormattingEnabled = true;
      this.cbbCategory.Items.AddRange(new object[] {
            "",
            "BlankSlide",
            "Instruction",
            "FlashStimulus",
            "MultipleChoiceQuestion"});
      this.cbbCategory.Location = new System.Drawing.Point(99, 48);
      this.cbbCategory.Name = "cbbCategory";
      this.cbbCategory.Size = new System.Drawing.Size(131, 21);
      this.cbbCategory.TabIndex = 32;
      // 
      // label10
      // 
      this.label10.AutoSize = true;
      this.label10.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label10.Location = new System.Drawing.Point(6, 51);
      this.label10.Name = "label10";
      this.label10.Size = new System.Drawing.Size(79, 19);
      this.label10.TabIndex = 33;
      this.label10.Text = "Category";
      // 
      // grpImageProperties
      // 
      this.grpImageProperties.Controls.Add(this.btnRemoveCondition);
      this.grpImageProperties.Controls.Add(this.btnAddCondition);
      this.grpImageProperties.Controls.Add(this.lsbStopConditions);
      this.grpImageProperties.Controls.Add(this.cbbKeys);
      this.grpImageProperties.Controls.Add(this.label3);
      this.grpImageProperties.Controls.Add(this.nudTime);
      this.grpImageProperties.Controls.Add(this.rdbTime);
      this.grpImageProperties.Controls.Add(this.rdbKey);
      this.grpImageProperties.Location = new System.Drawing.Point(9, 108);
      this.grpImageProperties.Name = "grpImageProperties";
      this.grpImageProperties.Size = new System.Drawing.Size(291, 171);
      this.grpImageProperties.TabIndex = 27;
      this.grpImageProperties.TabStop = false;
      this.grpImageProperties.Text = "The slide should be completed by:";
      // 
      // btnRemoveCondition
      // 
      this.btnRemoveCondition.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.btnRemoveCondition.ImageKey = "Delete";
      this.btnRemoveCondition.ImageList = this.imlCommands;
      this.btnRemoveCondition.Location = new System.Drawing.Point(175, 49);
      this.btnRemoveCondition.Name = "btnRemoveCondition";
      this.btnRemoveCondition.Size = new System.Drawing.Size(100, 23);
      this.btnRemoveCondition.TabIndex = 14;
      this.btnRemoveCondition.Text = "Remove";
      this.btnRemoveCondition.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.btnRemoveCondition.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.btnRemoveCondition.UseVisualStyleBackColor = true;
      this.btnRemoveCondition.Click += new System.EventHandler(this.btnRemoveCondition_Click);
      // 
      // imlCommands
      // 
      this.imlCommands.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imlCommands.ImageStream")));
      this.imlCommands.TransparentColor = System.Drawing.Color.Transparent;
      this.imlCommands.Images.SetKeyName(0, "Time");
      this.imlCommands.Images.SetKeyName(1, "Picture");
      this.imlCommands.Images.SetKeyName(2, "Properties");
      this.imlCommands.Images.SetKeyName(3, "Delete");
      this.imlCommands.Images.SetKeyName(4, "Testing");
      this.imlCommands.Images.SetKeyName(5, "Mouse");
      this.imlCommands.Images.SetKeyName(6, "Instructions");
      this.imlCommands.Images.SetKeyName(7, "Images");
      this.imlCommands.Images.SetKeyName(8, "Shapes");
      this.imlCommands.Images.SetKeyName(9, "Flash");
      this.imlCommands.Images.SetKeyName(10, "Sound");
      this.imlCommands.Images.SetKeyName(11, "Naming");
      this.imlCommands.Images.SetKeyName(12, "Link");
      this.imlCommands.Images.SetKeyName(13, "Rtf");
      this.imlCommands.Images.SetKeyName(14, "Trigger");
      this.imlCommands.Images.SetKeyName(15, "Browser");
      // 
      // btnAddCondition
      // 
      this.btnAddCondition.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.btnAddCondition.ImageKey = "Time";
      this.btnAddCondition.ImageList = this.imlCommands;
      this.btnAddCondition.Location = new System.Drawing.Point(175, 21);
      this.btnAddCondition.Name = "btnAddCondition";
      this.btnAddCondition.Size = new System.Drawing.Size(100, 23);
      this.btnAddCondition.TabIndex = 13;
      this.btnAddCondition.Text = "Add condition";
      this.btnAddCondition.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.btnAddCondition.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.btnAddCondition.UseVisualStyleBackColor = true;
      this.btnAddCondition.Click += new System.EventHandler(this.btnAddCondition_Click);
      // 
      // lsbStopConditions
      // 
      this.lsbStopConditions.FormattingEnabled = true;
      this.lsbStopConditions.HorizontalScrollbar = true;
      this.lsbStopConditions.Location = new System.Drawing.Point(18, 77);
      this.lsbStopConditions.Name = "lsbStopConditions";
      this.lsbStopConditions.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
      this.lsbStopConditions.Size = new System.Drawing.Size(256, 82);
      this.lsbStopConditions.TabIndex = 11;
      // 
      // cbbKeys
      // 
      this.cbbKeys.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbbKeys.FormattingEnabled = true;
      this.cbbKeys.Location = new System.Drawing.Point(86, 50);
      this.cbbKeys.Name = "cbbKeys";
      this.cbbKeys.Size = new System.Drawing.Size(68, 21);
      this.cbbKeys.Sorted = true;
      this.cbbKeys.TabIndex = 8;
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(24, 26);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(0, 13);
      this.label3.TabIndex = 10;
      // 
      // nudTime
      // 
      this.nudTime.DecimalPlaces = 2;
      this.nudTime.Location = new System.Drawing.Point(91, 24);
      this.nudTime.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
      this.nudTime.Name = "nudTime";
      this.nudTime.Size = new System.Drawing.Size(68, 20);
      this.nudTime.TabIndex = 7;
      this.nudTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.nudTime.ThousandsSeparator = true;
      // 
      // rdbTime
      // 
      this.rdbTime.AutoSize = true;
      this.rdbTime.Checked = true;
      this.rdbTime.Location = new System.Drawing.Point(18, 24);
      this.rdbTime.Name = "rdbTime";
      this.rdbTime.Size = new System.Drawing.Size(67, 17);
      this.rdbTime.TabIndex = 9;
      this.rdbTime.TabStop = true;
      this.rdbTime.Text = "Time (/s)";
      this.rdbTime.UseVisualStyleBackColor = true;
      // 
      // rdbKey
      // 
      this.rdbKey.AutoSize = true;
      this.rdbKey.Location = new System.Drawing.Point(19, 51);
      this.rdbKey.Name = "rdbKey";
      this.rdbKey.Size = new System.Drawing.Size(43, 17);
      this.rdbKey.TabIndex = 12;
      this.rdbKey.Text = "Key";
      this.rdbKey.UseVisualStyleBackColor = true;
      // 
      // btnHelp
      // 
      this.btnHelp.Image = global::Ogama.Properties.Resources.HelpBmp;
      this.btnHelp.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.btnHelp.Location = new System.Drawing.Point(3, 3);
      this.btnHelp.Name = "btnHelp";
      this.btnHelp.Size = new System.Drawing.Size(25, 25);
      this.btnHelp.TabIndex = 23;
      this.btnHelp.UseVisualStyleBackColor = true;
      this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.splitContainer1);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel1.Location = new System.Drawing.Point(0, 60);
      this.panel1.Name = "panel1";
      this.panel1.Padding = new System.Windows.Forms.Padding(3);
      this.panel1.Size = new System.Drawing.Size(330, 322);
      this.panel1.TabIndex = 19;
      // 
      // dialogTop1
      // 
      this.dialogTop1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(237)))));
      this.dialogTop1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("dialogTop1.BackgroundImage")));
      this.dialogTop1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
      this.dialogTop1.Description = "Please specify the desktop slides properties ...";
      this.dialogTop1.Dock = System.Windows.Forms.DockStyle.Top;
      this.dialogTop1.Location = new System.Drawing.Point(0, 0);
      this.dialogTop1.Logo = global::Ogama.Properties.Resources.Desktop48;
      this.dialogTop1.Name = "dialogTop1";
      this.dialogTop1.Size = new System.Drawing.Size(330, 60);
      this.dialogTop1.TabIndex = 22;
      // 
      // toolTip1
      // 
      this.toolTip1.ShowAlways = true;
      // 
      // DesktopDialog
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.btnCancel;
      this.ClientSize = new System.Drawing.Size(330, 382);
      this.Controls.Add(this.panel1);
      this.Controls.Add(this.dialogTop1);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "DesktopDialog";
      this.ShowIcon = false;
      this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Add new desktop recording ...";
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
      this.splitContainer1.ResumeLayout(false);
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.grpImageProperties.ResumeLayout(false);
      this.grpImageProperties.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.nudTime)).EndInit();
      this.panel1.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Button btnOK;
    private System.Windows.Forms.Button btnCancel;
    private DialogTop dialogTop1;
    private System.Windows.Forms.SplitContainer splitContainer1;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.Button btnHelp;
    private System.Windows.Forms.GroupBox grpImageProperties;
    private System.Windows.Forms.ToolTip toolTip1;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.TextBox txbName;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.ComboBox cbbCategory;
    private System.Windows.Forms.Label label10;
    private System.Windows.Forms.Button btnRemoveCondition;
    private System.Windows.Forms.Button btnAddCondition;
    private System.Windows.Forms.ListBox lsbStopConditions;
    private System.Windows.Forms.ComboBox cbbKeys;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.NumericUpDown nudTime;
    private System.Windows.Forms.RadioButton rdbTime;
    private System.Windows.Forms.RadioButton rdbKey;
    private System.Windows.Forms.ImageList imlCommands;
  }
}