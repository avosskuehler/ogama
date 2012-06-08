namespace Ogama.Modules.SlideshowDesign.Import
{
  using Ogama.Modules.Common.Controls;

  partial class FolderContentSlideImportDialog
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FolderContentSlideImportDialog));
      this.dialogTop1 = new DialogTop();
      this.btnOK = new System.Windows.Forms.Button();
      this.btnCancel = new System.Windows.Forms.Button();
      this.cbbKeys = new System.Windows.Forms.ComboBox();
      this.cbbMouseButtons = new System.Windows.Forms.ComboBox();
      this.label6 = new System.Windows.Forms.Label();
      this.nudTime = new System.Windows.Forms.NumericUpDown();
      this.rdbTime = new System.Windows.Forms.RadioButton();
      this.rdbMouse = new System.Windows.Forms.RadioButton();
      this.rdbDuration = new System.Windows.Forms.RadioButton();
      this.rdbKey = new System.Windows.Forms.RadioButton();
      this.clbBackground = new OgamaControls.ColorButton(this.components);
      this.psbItems = new OgamaControls.PositionButton(this.components);
      this.nudHeight = new System.Windows.Forms.NumericUpDown();
      this.nudWidth = new System.Windows.Forms.NumericUpDown();
      this.label7 = new System.Windows.Forms.Label();
      this.label8 = new System.Windows.Forms.Label();
      this.label3 = new System.Windows.Forms.Label();
      this.label4 = new System.Windows.Forms.Label();
      this.lsbStandardItems = new System.Windows.Forms.ListBox();
      this.tosStimuli = new System.Windows.Forms.ToolStrip();
      this.btnInstruction = new System.Windows.Forms.ToolStripButton();
      this.btnImage = new System.Windows.Forms.ToolStripButton();
      this.btnShapes = new System.Windows.Forms.ToolStripButton();
      this.fbdStimuli = new System.Windows.Forms.FolderBrowserDialog();
      this.txbFolder = new System.Windows.Forms.TextBox();
      this.label5 = new System.Windows.Forms.Label();
      this.btnOpenFolder = new System.Windows.Forms.Button();
      this.psbMouseCursor = new OgamaControls.PositionButton(this.components);
      this.chbShowMouseCursor = new System.Windows.Forms.CheckBox();
      this.label2 = new System.Windows.Forms.Label();
      this.groupBox3 = new System.Windows.Forms.GroupBox();
      this.label9 = new System.Windows.Forms.Label();
      this.nudLatency = new System.Windows.Forms.NumericUpDown();
      this.tacSettings = new System.Windows.Forms.TabControl();
      this.tabPage1 = new System.Windows.Forms.TabPage();
      this.groupBox5 = new System.Windows.Forms.GroupBox();
      this.groupBox4 = new System.Windows.Forms.GroupBox();
      this.tabPage2 = new System.Windows.Forms.TabPage();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.cbbDesignedItem = new System.Windows.Forms.ComboBox();
      this.btnAddItem = new System.Windows.Forms.Button();
      this.btnRemoveItem = new System.Windows.Forms.Button();
      this.label1 = new System.Windows.Forms.Label();
      ((System.ComponentModel.ISupportInitialize)(this.nudTime)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.nudHeight)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.nudWidth)).BeginInit();
      this.tosStimuli.SuspendLayout();
      this.groupBox3.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.nudLatency)).BeginInit();
      this.tacSettings.SuspendLayout();
      this.tabPage1.SuspendLayout();
      this.groupBox5.SuspendLayout();
      this.groupBox4.SuspendLayout();
      this.tabPage2.SuspendLayout();
      this.groupBox1.SuspendLayout();
      this.SuspendLayout();
      // 
      // dialogTop1
      // 
      this.dialogTop1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(237)))));
      this.dialogTop1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("dialogTop1.BackgroundImage")));
      this.dialogTop1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
      this.dialogTop1.Description = "Select the folder and settings to import.";
      this.dialogTop1.Dock = System.Windows.Forms.DockStyle.Top;
      this.dialogTop1.Location = new System.Drawing.Point(0, 0);
      this.dialogTop1.Logo = global::Ogama.Properties.Resources.ImportPictureDocument;
      this.dialogTop1.Name = "dialogTop1";
      this.dialogTop1.Size = new System.Drawing.Size(427, 60);
      this.dialogTop1.TabIndex = 0;
      // 
      // btnOK
      // 
      this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.btnOK.Location = new System.Drawing.Point(273, 360);
      this.btnOK.Name = "btnOK";
      this.btnOK.Size = new System.Drawing.Size(65, 25);
      this.btnOK.TabIndex = 23;
      this.btnOK.Text = "OK";
      this.btnOK.UseVisualStyleBackColor = true;
      this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
      // 
      // btnCancel
      // 
      this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnCancel.Location = new System.Drawing.Point(350, 360);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(65, 25);
      this.btnCancel.TabIndex = 22;
      this.btnCancel.Text = "Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      // 
      // cbbKeys
      // 
      this.cbbKeys.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbbKeys.FormattingEnabled = true;
      this.cbbKeys.Location = new System.Drawing.Point(120, 94);
      this.cbbKeys.Name = "cbbKeys";
      this.cbbKeys.Size = new System.Drawing.Size(68, 21);
      this.cbbKeys.Sorted = true;
      this.cbbKeys.TabIndex = 8;
      // 
      // cbbMouseButtons
      // 
      this.cbbMouseButtons.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbbMouseButtons.FormattingEnabled = true;
      this.cbbMouseButtons.Location = new System.Drawing.Point(120, 67);
      this.cbbMouseButtons.Name = "cbbMouseButtons";
      this.cbbMouseButtons.Size = new System.Drawing.Size(68, 21);
      this.cbbMouseButtons.TabIndex = 9;
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Location = new System.Drawing.Point(6, 23);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(168, 13);
      this.label6.TabIndex = 12;
      this.label6.Text = "The slide should be completed by:";
      // 
      // nudTime
      // 
      this.nudTime.DecimalPlaces = 2;
      this.nudTime.Location = new System.Drawing.Point(120, 43);
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
      this.nudTime.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
      // 
      // rdbTime
      // 
      this.rdbTime.AutoSize = true;
      this.rdbTime.Checked = true;
      this.rdbTime.Location = new System.Drawing.Point(11, 43);
      this.rdbTime.Name = "rdbTime";
      this.rdbTime.Size = new System.Drawing.Size(67, 17);
      this.rdbTime.TabIndex = 10;
      this.rdbTime.TabStop = true;
      this.rdbTime.Text = "Time (/s)";
      this.rdbTime.UseVisualStyleBackColor = true;
      // 
      // rdbMouse
      // 
      this.rdbMouse.AutoSize = true;
      this.rdbMouse.Location = new System.Drawing.Point(11, 69);
      this.rdbMouse.Name = "rdbMouse";
      this.rdbMouse.Size = new System.Drawing.Size(57, 17);
      this.rdbMouse.TabIndex = 11;
      this.rdbMouse.Text = "Mouse";
      this.rdbMouse.UseVisualStyleBackColor = true;
      // 
      // rdbDuration
      // 
      this.rdbDuration.AutoSize = true;
      this.rdbDuration.Location = new System.Drawing.Point(11, 121);
      this.rdbDuration.Name = "rdbDuration";
      this.rdbDuration.Size = new System.Drawing.Size(189, 17);
      this.rdbDuration.TabIndex = 13;
      this.rdbDuration.Text = "Duration of media (sound or movie)";
      this.rdbDuration.UseVisualStyleBackColor = true;
      // 
      // rdbKey
      // 
      this.rdbKey.AutoSize = true;
      this.rdbKey.Location = new System.Drawing.Point(11, 95);
      this.rdbKey.Name = "rdbKey";
      this.rdbKey.Size = new System.Drawing.Size(43, 17);
      this.rdbKey.TabIndex = 13;
      this.rdbKey.Text = "Key";
      this.rdbKey.UseVisualStyleBackColor = true;
      // 
      // clbBackground
      // 
      this.clbBackground.AutoButtonString = "Automatic";
      this.clbBackground.CurrentColor = System.Drawing.Color.Black;
      this.clbBackground.Location = new System.Drawing.Point(6, 23);
      this.clbBackground.Name = "clbBackground";
      this.clbBackground.Size = new System.Drawing.Size(75, 23);
      this.clbBackground.TabIndex = 0;
      this.clbBackground.UseVisualStyleBackColor = true;
      // 
      // psbItems
      // 
      this.psbItems.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
      this.psbItems.CurrentPosition = new System.Drawing.Point(512, 384);
      this.psbItems.Location = new System.Drawing.Point(91, 71);
      this.psbItems.MinimumSize = new System.Drawing.Size(80, 22);
      this.psbItems.Name = "psbItems";
      this.psbItems.Size = new System.Drawing.Size(90, 22);
      this.psbItems.StimulusScreenSize = new System.Drawing.Size(0, 0);
      this.psbItems.TabIndex = 4;
      this.psbItems.Text = "(512,384)";
      this.psbItems.TextAlign = System.Drawing.ContentAlignment.TopLeft;
      this.psbItems.UseVisualStyleBackColor = true;
      // 
      // nudHeight
      // 
      this.nudHeight.Location = new System.Drawing.Point(91, 45);
      this.nudHeight.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
      this.nudHeight.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nudHeight.Name = "nudHeight";
      this.nudHeight.Size = new System.Drawing.Size(90, 20);
      this.nudHeight.TabIndex = 8;
      this.nudHeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.nudHeight.Value = new decimal(new int[] {
            200,
            0,
            0,
            0});
      // 
      // nudWidth
      // 
      this.nudWidth.Location = new System.Drawing.Point(91, 19);
      this.nudWidth.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
      this.nudWidth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nudWidth.Name = "nudWidth";
      this.nudWidth.Size = new System.Drawing.Size(90, 20);
      this.nudWidth.TabIndex = 8;
      this.nudWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.nudWidth.Value = new decimal(new int[] {
            300,
            0,
            0,
            0});
      // 
      // label7
      // 
      this.label7.AutoSize = true;
      this.label7.Location = new System.Drawing.Point(9, 47);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(38, 13);
      this.label7.TabIndex = 7;
      this.label7.Text = "Height";
      // 
      // label8
      // 
      this.label8.AutoSize = true;
      this.label8.Location = new System.Drawing.Point(6, 15);
      this.label8.Name = "label8";
      this.label8.Size = new System.Drawing.Size(100, 13);
      this.label8.TabIndex = 7;
      this.label8.Text = "Specify  item to add";
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(9, 21);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(35, 13);
      this.label3.TabIndex = 7;
      this.label3.Text = "Width";
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(9, 75);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(44, 13);
      this.label4.TabIndex = 7;
      this.label4.Text = "Position";
      // 
      // lsbStandardItems
      // 
      this.lsbStandardItems.FormattingEnabled = true;
      this.lsbStandardItems.Location = new System.Drawing.Point(212, 7);
      this.lsbStandardItems.Name = "lsbStandardItems";
      this.lsbStandardItems.Size = new System.Drawing.Size(172, 186);
      this.lsbStandardItems.TabIndex = 6;
      // 
      // tosStimuli
      // 
      this.tosStimuli.Dock = System.Windows.Forms.DockStyle.None;
      this.tosStimuli.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
      this.tosStimuli.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnInstruction,
            this.btnImage,
            this.btnShapes});
      this.tosStimuli.Location = new System.Drawing.Point(114, 8);
      this.tosStimuli.Name = "tosStimuli";
      this.tosStimuli.Size = new System.Drawing.Size(72, 25);
      this.tosStimuli.TabIndex = 5;
      // 
      // btnInstruction
      // 
      this.btnInstruction.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnInstruction.Image = global::Ogama.Properties.Resources.textdoc1;
      this.btnInstruction.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnInstruction.Name = "btnInstruction";
      this.btnInstruction.Size = new System.Drawing.Size(23, 22);
      this.btnInstruction.Text = "Add instruction slide ...";
      this.btnInstruction.Click += new System.EventHandler(this.btnInstruction_Click);
      // 
      // btnImage
      // 
      this.btnImage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnImage.Image = global::Ogama.Properties.Resources.GenericPicDoc;
      this.btnImage.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnImage.Name = "btnImage";
      this.btnImage.Size = new System.Drawing.Size(23, 22);
      this.btnImage.Text = "Add image slide ...";
      this.btnImage.Click += new System.EventHandler(this.btnImage_Click);
      // 
      // btnShapes
      // 
      this.btnShapes.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnShapes.Image = global::Ogama.Properties.Resources.GenericShape;
      this.btnShapes.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnShapes.Name = "btnShapes";
      this.btnShapes.Size = new System.Drawing.Size(23, 22);
      this.btnShapes.Text = "Add shape elements slide ...";
      this.btnShapes.Click += new System.EventHandler(this.btnShapes_Click);
      // 
      // fbdStimuli
      // 
      this.fbdStimuli.Description = "Select the folder with the stimuli to import.";
      this.fbdStimuli.ShowNewFolderButton = false;
      // 
      // txbFolder
      // 
      this.txbFolder.Location = new System.Drawing.Point(104, 99);
      this.txbFolder.Name = "txbFolder";
      this.txbFolder.ReadOnly = true;
      this.txbFolder.Size = new System.Drawing.Size(280, 20);
      this.txbFolder.TabIndex = 26;
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(9, 102);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(89, 13);
      this.label5.TabIndex = 27;
      this.label5.Text = "Folder with stimuli";
      // 
      // btnOpenFolder
      // 
      this.btnOpenFolder.Image = global::Ogama.Properties.Resources.openfolderHS;
      this.btnOpenFolder.Location = new System.Drawing.Point(390, 96);
      this.btnOpenFolder.Name = "btnOpenFolder";
      this.btnOpenFolder.Size = new System.Drawing.Size(25, 25);
      this.btnOpenFolder.TabIndex = 28;
      this.btnOpenFolder.UseVisualStyleBackColor = true;
      this.btnOpenFolder.Click += new System.EventHandler(this.btnOpenFolder_Click);
      // 
      // psbMouseCursor
      // 
      this.psbMouseCursor.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
      this.psbMouseCursor.CurrentPosition = new System.Drawing.Point(100, 100);
      this.psbMouseCursor.Location = new System.Drawing.Point(79, 42);
      this.psbMouseCursor.MinimumSize = new System.Drawing.Size(80, 22);
      this.psbMouseCursor.Name = "psbMouseCursor";
      this.psbMouseCursor.Size = new System.Drawing.Size(80, 22);
      this.psbMouseCursor.StimulusScreenSize = new System.Drawing.Size(0, 0);
      this.psbMouseCursor.TabIndex = 14;
      this.psbMouseCursor.Text = "(100,100)";
      this.psbMouseCursor.TextAlign = System.Drawing.ContentAlignment.TopLeft;
      this.psbMouseCursor.UseVisualStyleBackColor = true;
      // 
      // chbShowMouseCursor
      // 
      this.chbShowMouseCursor.AutoSize = true;
      this.chbShowMouseCursor.Location = new System.Drawing.Point(6, 19);
      this.chbShowMouseCursor.Name = "chbShowMouseCursor";
      this.chbShowMouseCursor.Size = new System.Drawing.Size(119, 17);
      this.chbShowMouseCursor.TabIndex = 15;
      this.chbShowMouseCursor.Text = "Show mouse cursor";
      this.chbShowMouseCursor.UseVisualStyleBackColor = true;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(3, 46);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(70, 13);
      this.label2.TabIndex = 16;
      this.label2.Text = "Initial position";
      // 
      // groupBox3
      // 
      this.groupBox3.Controls.Add(this.label9);
      this.groupBox3.Controls.Add(this.label6);
      this.groupBox3.Controls.Add(this.rdbKey);
      this.groupBox3.Controls.Add(this.rdbDuration);
      this.groupBox3.Controls.Add(this.cbbKeys);
      this.groupBox3.Controls.Add(this.rdbMouse);
      this.groupBox3.Controls.Add(this.cbbMouseButtons);
      this.groupBox3.Controls.Add(this.rdbTime);
      this.groupBox3.Controls.Add(this.nudLatency);
      this.groupBox3.Controls.Add(this.nudTime);
      this.groupBox3.Location = new System.Drawing.Point(6, 6);
      this.groupBox3.Name = "groupBox3";
      this.groupBox3.Size = new System.Drawing.Size(200, 184);
      this.groupBox3.TabIndex = 17;
      this.groupBox3.TabStop = false;
      this.groupBox3.Text = "Timing";
      // 
      // label9
      // 
      this.label9.AutoSize = true;
      this.label9.Location = new System.Drawing.Point(26, 142);
      this.label9.Name = "label9";
      this.label9.Size = new System.Drawing.Size(94, 13);
      this.label9.TabIndex = 14;
      this.label9.Text = "add ms to duration";
      // 
      // nudLatency
      // 
      this.nudLatency.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
      this.nudLatency.Location = new System.Drawing.Point(120, 139);
      this.nudLatency.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
      this.nudLatency.Name = "nudLatency";
      this.nudLatency.Size = new System.Drawing.Size(68, 20);
      this.nudLatency.TabIndex = 7;
      this.nudLatency.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.nudLatency.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
      // 
      // tacSettings
      // 
      this.tacSettings.Controls.Add(this.tabPage1);
      this.tacSettings.Controls.Add(this.tabPage2);
      this.tacSettings.Location = new System.Drawing.Point(12, 132);
      this.tacSettings.Name = "tacSettings";
      this.tacSettings.SelectedIndex = 0;
      this.tacSettings.Size = new System.Drawing.Size(403, 222);
      this.tacSettings.TabIndex = 29;
      // 
      // tabPage1
      // 
      this.tabPage1.Controls.Add(this.groupBox5);
      this.tabPage1.Controls.Add(this.groupBox4);
      this.tabPage1.Controls.Add(this.groupBox3);
      this.tabPage1.Location = new System.Drawing.Point(4, 22);
      this.tabPage1.Name = "tabPage1";
      this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage1.Size = new System.Drawing.Size(395, 196);
      this.tabPage1.TabIndex = 0;
      this.tabPage1.Text = "General slide properties";
      this.tabPage1.UseVisualStyleBackColor = true;
      // 
      // groupBox5
      // 
      this.groupBox5.Controls.Add(this.label2);
      this.groupBox5.Controls.Add(this.chbShowMouseCursor);
      this.groupBox5.Controls.Add(this.psbMouseCursor);
      this.groupBox5.Location = new System.Drawing.Point(212, 76);
      this.groupBox5.Name = "groupBox5";
      this.groupBox5.Size = new System.Drawing.Size(172, 114);
      this.groupBox5.TabIndex = 19;
      this.groupBox5.TabStop = false;
      this.groupBox5.Text = "Mouse";
      // 
      // groupBox4
      // 
      this.groupBox4.Controls.Add(this.clbBackground);
      this.groupBox4.Location = new System.Drawing.Point(212, 6);
      this.groupBox4.Name = "groupBox4";
      this.groupBox4.Size = new System.Drawing.Size(172, 64);
      this.groupBox4.TabIndex = 18;
      this.groupBox4.TabStop = false;
      this.groupBox4.Text = "Background color";
      // 
      // tabPage2
      // 
      this.tabPage2.Controls.Add(this.groupBox1);
      this.tabPage2.Controls.Add(this.cbbDesignedItem);
      this.tabPage2.Controls.Add(this.btnAddItem);
      this.tabPage2.Controls.Add(this.btnRemoveItem);
      this.tabPage2.Controls.Add(this.lsbStandardItems);
      this.tabPage2.Controls.Add(this.label8);
      this.tabPage2.Controls.Add(this.tosStimuli);
      this.tabPage2.Location = new System.Drawing.Point(4, 22);
      this.tabPage2.Name = "tabPage2";
      this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage2.Size = new System.Drawing.Size(395, 196);
      this.tabPage2.TabIndex = 1;
      this.tabPage2.Text = "Slide contents";
      this.tabPage2.UseVisualStyleBackColor = true;
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.nudWidth);
      this.groupBox1.Controls.Add(this.label4);
      this.groupBox1.Controls.Add(this.label3);
      this.groupBox1.Controls.Add(this.psbItems);
      this.groupBox1.Controls.Add(this.nudHeight);
      this.groupBox1.Controls.Add(this.label7);
      this.groupBox1.Location = new System.Drawing.Point(6, 65);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(187, 100);
      this.groupBox1.TabIndex = 12;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Size and position";
      // 
      // cbbDesignedItem
      // 
      this.cbbDesignedItem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbbDesignedItem.FormattingEnabled = true;
      this.cbbDesignedItem.Location = new System.Drawing.Point(7, 40);
      this.cbbDesignedItem.Name = "cbbDesignedItem";
      this.cbbDesignedItem.Size = new System.Drawing.Size(186, 21);
      this.cbbDesignedItem.TabIndex = 11;
      // 
      // btnAddItem
      // 
      this.btnAddItem.Location = new System.Drawing.Point(7, 170);
      this.btnAddItem.Name = "btnAddItem";
      this.btnAddItem.Size = new System.Drawing.Size(90, 23);
      this.btnAddItem.TabIndex = 9;
      this.btnAddItem.Text = "Add item";
      this.btnAddItem.UseVisualStyleBackColor = true;
      this.btnAddItem.Click += new System.EventHandler(this.btnAddItem_Click);
      // 
      // btnRemoveItem
      // 
      this.btnRemoveItem.Location = new System.Drawing.Point(103, 170);
      this.btnRemoveItem.Name = "btnRemoveItem";
      this.btnRemoveItem.Size = new System.Drawing.Size(90, 23);
      this.btnRemoveItem.TabIndex = 9;
      this.btnRemoveItem.Text = "Remove item";
      this.btnRemoveItem.UseVisualStyleBackColor = true;
      this.btnRemoveItem.Click += new System.EventHandler(this.btnRemoveItem_Click);
      // 
      // label1
      // 
      this.label1.Location = new System.Drawing.Point(12, 63);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(407, 33);
      this.label1.TabIndex = 30;
      this.label1.Text = "Select the folder that contains the images or audio files that should be converte" +
          "d into slides.Then specify properties and contents that all of the slides should" +
          " contain.";
      // 
      // FolderContentSlideImportDialog
      // 
      this.AcceptButton = this.btnOK;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.btnCancel;
      this.ClientSize = new System.Drawing.Size(427, 397);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.tacSettings);
      this.Controls.Add(this.btnOpenFolder);
      this.Controls.Add(this.label5);
      this.Controls.Add(this.txbFolder);
      this.Controls.Add(this.btnOK);
      this.Controls.Add(this.btnCancel);
      this.Controls.Add(this.dialogTop1);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "FolderContentSlideImportDialog";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Import folder contents";
      this.Load += new System.EventHandler(this.FolderImportDlg_Load);
      ((System.ComponentModel.ISupportInitialize)(this.nudTime)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.nudHeight)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.nudWidth)).EndInit();
      this.tosStimuli.ResumeLayout(false);
      this.tosStimuli.PerformLayout();
      this.groupBox3.ResumeLayout(false);
      this.groupBox3.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.nudLatency)).EndInit();
      this.tacSettings.ResumeLayout(false);
      this.tabPage1.ResumeLayout(false);
      this.groupBox5.ResumeLayout(false);
      this.groupBox5.PerformLayout();
      this.groupBox4.ResumeLayout(false);
      this.tabPage2.ResumeLayout(false);
      this.tabPage2.PerformLayout();
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

		}

		#endregion

		private DialogTop dialogTop1;
		private System.Windows.Forms.Button btnOK;
    private System.Windows.Forms.Button btnCancel;
		private OgamaControls.ColorButton clbBackground;
    private OgamaControls.PositionButton psbItems;
		private System.Windows.Forms.ToolStrip tosStimuli;
		private System.Windows.Forms.ToolStripButton btnInstruction;
		private System.Windows.Forms.ToolStripButton btnImage;
		private System.Windows.Forms.ToolStripButton btnShapes;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.ListBox lsbStandardItems;
		private System.Windows.Forms.FolderBrowserDialog fbdStimuli;
		private System.Windows.Forms.TextBox txbFolder;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Button btnOpenFolder;
		private System.Windows.Forms.ComboBox cbbKeys;
		private System.Windows.Forms.ComboBox cbbMouseButtons;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.NumericUpDown nudTime;
		private System.Windows.Forms.RadioButton rdbTime;
		private System.Windows.Forms.RadioButton rdbMouse;
    private System.Windows.Forms.RadioButton rdbKey;
		private System.Windows.Forms.RadioButton rdbDuration;
		private System.Windows.Forms.NumericUpDown nudHeight;
		private System.Windows.Forms.NumericUpDown nudWidth;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
    private System.Windows.Forms.Label label3;
    private OgamaControls.PositionButton psbMouseCursor;
    private System.Windows.Forms.GroupBox groupBox3;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.CheckBox chbShowMouseCursor;
    private System.Windows.Forms.TabControl tacSettings;
    private System.Windows.Forms.TabPage tabPage1;
    private System.Windows.Forms.TabPage tabPage2;
    private System.Windows.Forms.GroupBox groupBox5;
    private System.Windows.Forms.GroupBox groupBox4;
    private System.Windows.Forms.Button btnAddItem;
    private System.Windows.Forms.Button btnRemoveItem;
    private System.Windows.Forms.ComboBox cbbDesignedItem;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label9;
    private System.Windows.Forms.NumericUpDown nudLatency;
	}
}