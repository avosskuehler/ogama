namespace Ogama.Modules.SlideshowDesign.DesignModule.StimuliDialogs
{
  using Ogama.Modules.Common.Controls;

  partial class BrowserDialog
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BrowserDialog));
      this.btnOK = new System.Windows.Forms.Button();
      this.btnCancel = new System.Windows.Forms.Button();
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      this.splitContainer2 = new System.Windows.Forms.SplitContainer();
      this.grpImageProperties = new System.Windows.Forms.GroupBox();
      this.txbURL = new System.Windows.Forms.TextBox();
      this.splitContainer3 = new System.Windows.Forms.SplitContainer();
      this.tabControl1 = new System.Windows.Forms.TabControl();
      this.tbpName = new System.Windows.Forms.TabPage();
      this.label11 = new System.Windows.Forms.Label();
      this.cbbCategory = new System.Windows.Forms.ComboBox();
      this.label10 = new System.Windows.Forms.Label();
      this.txbName = new System.Windows.Forms.TextBox();
      this.label5 = new System.Windows.Forms.Label();
      this.pcbHelpNaming = new System.Windows.Forms.PictureBox();
      this.tbpTiming = new System.Windows.Forms.TabPage();
      this.pcbHelpTiming = new System.Windows.Forms.PictureBox();
      this.btnRemoveCondition = new System.Windows.Forms.Button();
      this.imlCommands = new System.Windows.Forms.ImageList(this.components);
      this.btnAddCondition = new System.Windows.Forms.Button();
      this.lsbStopConditions = new System.Windows.Forms.ListBox();
      this.cbbKeys = new System.Windows.Forms.ComboBox();
      this.label3 = new System.Windows.Forms.Label();
      this.nudTime = new System.Windows.Forms.NumericUpDown();
      this.rdbTime = new System.Windows.Forms.RadioButton();
      this.rdbKey = new System.Windows.Forms.RadioButton();
      this.tbpBowseDepth = new System.Windows.Forms.TabPage();
      this.pcbHelpBrowseDepth = new System.Windows.Forms.PictureBox();
      this.label2 = new System.Windows.Forms.Label();
      this.label4 = new System.Windows.Forms.Label();
      this.label1 = new System.Windows.Forms.Label();
      this.nudBrowseDepth = new System.Windows.Forms.NumericUpDown();
      this.grpPreview = new System.Windows.Forms.GroupBox();
      this.webBrowserPreview = new OgamaControls.CommonControls.Browser.ExtendedBrowser();
      this.btnHelp = new System.Windows.Forms.Button();
      this.panel1 = new System.Windows.Forms.Panel();
      this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
      this.dialogTop1 = new Ogama.Modules.Common.Controls.DialogTop();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
      this.splitContainer2.Panel1.SuspendLayout();
      this.splitContainer2.Panel2.SuspendLayout();
      this.splitContainer2.SuspendLayout();
      this.grpImageProperties.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
      this.splitContainer3.Panel1.SuspendLayout();
      this.splitContainer3.Panel2.SuspendLayout();
      this.splitContainer3.SuspendLayout();
      this.tabControl1.SuspendLayout();
      this.tbpName.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pcbHelpNaming)).BeginInit();
      this.tbpTiming.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pcbHelpTiming)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.nudTime)).BeginInit();
      this.tbpBowseDepth.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pcbHelpBrowseDepth)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.nudBrowseDepth)).BeginInit();
      this.grpPreview.SuspendLayout();
      this.panel1.SuspendLayout();
      this.SuspendLayout();
      // 
      // btnOK
      // 
      this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.btnOK.Location = new System.Drawing.Point(516, 3);
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
      this.btnCancel.Location = new System.Drawing.Point(593, 3);
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
      this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
      // 
      // splitContainer1.Panel2
      // 
      this.splitContainer1.Panel2.Controls.Add(this.btnHelp);
      this.splitContainer1.Panel2.Controls.Add(this.btnOK);
      this.splitContainer1.Panel2.Controls.Add(this.btnCancel);
      this.splitContainer1.Size = new System.Drawing.Size(661, 353);
      this.splitContainer1.SplitterDistance = 319;
      this.splitContainer1.TabIndex = 23;
      // 
      // splitContainer2
      // 
      this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
      this.splitContainer2.IsSplitterFixed = true;
      this.splitContainer2.Location = new System.Drawing.Point(0, 0);
      this.splitContainer2.Name = "splitContainer2";
      this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // splitContainer2.Panel1
      // 
      this.splitContainer2.Panel1.Controls.Add(this.grpImageProperties);
      this.splitContainer2.Panel1MinSize = 45;
      // 
      // splitContainer2.Panel2
      // 
      this.splitContainer2.Panel2.Controls.Add(this.splitContainer3);
      this.splitContainer2.Size = new System.Drawing.Size(661, 319);
      this.splitContainer2.SplitterDistance = 45;
      this.splitContainer2.TabIndex = 29;
      // 
      // grpImageProperties
      // 
      this.grpImageProperties.Controls.Add(this.txbURL);
      this.grpImageProperties.Dock = System.Windows.Forms.DockStyle.Fill;
      this.grpImageProperties.Location = new System.Drawing.Point(0, 0);
      this.grpImageProperties.Name = "grpImageProperties";
      this.grpImageProperties.Size = new System.Drawing.Size(661, 45);
      this.grpImageProperties.TabIndex = 27;
      this.grpImageProperties.TabStop = false;
      this.grpImageProperties.Text = "URL";
      // 
      // txbURL
      // 
      this.txbURL.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txbURL.Location = new System.Drawing.Point(3, 16);
      this.txbURL.Name = "txbURL";
      this.txbURL.Size = new System.Drawing.Size(655, 20);
      this.txbURL.TabIndex = 19;
      this.txbURL.Text = "http://www.";
      this.toolTip1.SetToolTip(this.txbURL, "Please specify the URL including http://");
      this.txbURL.TextChanged += new System.EventHandler(this.txbURL_TextChanged);
      // 
      // splitContainer3
      // 
      this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
      this.splitContainer3.IsSplitterFixed = true;
      this.splitContainer3.Location = new System.Drawing.Point(0, 0);
      this.splitContainer3.Name = "splitContainer3";
      // 
      // splitContainer3.Panel1
      // 
      this.splitContainer3.Panel1.Controls.Add(this.tabControl1);
      this.splitContainer3.Panel1MinSize = 280;
      // 
      // splitContainer3.Panel2
      // 
      this.splitContainer3.Panel2.Controls.Add(this.grpPreview);
      this.splitContainer3.Size = new System.Drawing.Size(661, 270);
      this.splitContainer3.SplitterDistance = 280;
      this.splitContainer3.TabIndex = 0;
      // 
      // tabControl1
      // 
      this.tabControl1.Controls.Add(this.tbpName);
      this.tabControl1.Controls.Add(this.tbpTiming);
      this.tabControl1.Controls.Add(this.tbpBowseDepth);
      this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tabControl1.Location = new System.Drawing.Point(0, 0);
      this.tabControl1.Name = "tabControl1";
      this.tabControl1.SelectedIndex = 0;
      this.tabControl1.Size = new System.Drawing.Size(280, 270);
      this.tabControl1.TabIndex = 28;
      // 
      // tbpName
      // 
      this.tbpName.Controls.Add(this.label11);
      this.tbpName.Controls.Add(this.cbbCategory);
      this.tbpName.Controls.Add(this.label10);
      this.tbpName.Controls.Add(this.txbName);
      this.tbpName.Controls.Add(this.label5);
      this.tbpName.Controls.Add(this.pcbHelpNaming);
      this.tbpName.ImageKey = "Naming";
      this.tbpName.Location = new System.Drawing.Point(4, 22);
      this.tbpName.Name = "tbpName";
      this.tbpName.Padding = new System.Windows.Forms.Padding(3);
      this.tbpName.Size = new System.Drawing.Size(272, 244);
      this.tbpName.TabIndex = 7;
      this.tbpName.Text = "Naming";
      this.tbpName.UseVisualStyleBackColor = true;
      // 
      // label11
      // 
      this.label11.AutoSize = true;
      this.label11.Location = new System.Drawing.Point(7, 6);
      this.label11.Name = "label11";
      this.label11.Size = new System.Drawing.Size(148, 13);
      this.label11.TabIndex = 26;
      this.label11.Text = "Specify name and category ...";
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
            "Webpage",
            "MultipleChoiceQuestion"});
      this.cbbCategory.Location = new System.Drawing.Point(115, 62);
      this.cbbCategory.Name = "cbbCategory";
      this.cbbCategory.Size = new System.Drawing.Size(131, 21);
      this.cbbCategory.TabIndex = 9;
      this.cbbCategory.Text = "Webpage";
      // 
      // label10
      // 
      this.label10.AutoSize = true;
      this.label10.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label10.Location = new System.Drawing.Point(22, 65);
      this.label10.Name = "label10";
      this.label10.Size = new System.Drawing.Size(79, 19);
      this.label10.TabIndex = 19;
      this.label10.Text = "Category";
      // 
      // txbName
      // 
      this.txbName.Location = new System.Drawing.Point(115, 33);
      this.txbName.Name = "txbName";
      this.txbName.Size = new System.Drawing.Size(131, 20);
      this.txbName.TabIndex = 7;
      this.txbName.Text = "Webpage0";
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label5.Location = new System.Drawing.Point(22, 35);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(89, 19);
      this.label5.TabIndex = 0;
      this.label5.Text = "Slidename";
      // 
      // pcbHelpNaming
      // 
      this.pcbHelpNaming.Cursor = System.Windows.Forms.Cursors.Help;
      this.pcbHelpNaming.Image = global::Ogama.Properties.Resources.HelpBmp;
      this.pcbHelpNaming.Location = new System.Drawing.Point(250, 5);
      this.pcbHelpNaming.Name = "pcbHelpNaming";
      this.pcbHelpNaming.Size = new System.Drawing.Size(16, 16);
      this.pcbHelpNaming.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
      this.pcbHelpNaming.TabIndex = 14;
      this.pcbHelpNaming.TabStop = false;
      this.pcbHelpNaming.Click += new System.EventHandler(this.pcbHelpNaming_Click);
      // 
      // tbpTiming
      // 
      this.tbpTiming.Controls.Add(this.pcbHelpTiming);
      this.tbpTiming.Controls.Add(this.btnRemoveCondition);
      this.tbpTiming.Controls.Add(this.btnAddCondition);
      this.tbpTiming.Controls.Add(this.lsbStopConditions);
      this.tbpTiming.Controls.Add(this.cbbKeys);
      this.tbpTiming.Controls.Add(this.label3);
      this.tbpTiming.Controls.Add(this.nudTime);
      this.tbpTiming.Controls.Add(this.rdbTime);
      this.tbpTiming.Controls.Add(this.rdbKey);
      this.tbpTiming.ImageKey = "Time";
      this.tbpTiming.Location = new System.Drawing.Point(4, 22);
      this.tbpTiming.Name = "tbpTiming";
      this.tbpTiming.Padding = new System.Windows.Forms.Padding(3);
      this.tbpTiming.Size = new System.Drawing.Size(272, 244);
      this.tbpTiming.TabIndex = 2;
      this.tbpTiming.Text = "Timing";
      this.tbpTiming.UseVisualStyleBackColor = true;
      // 
      // pcbHelpTiming
      // 
      this.pcbHelpTiming.Cursor = System.Windows.Forms.Cursors.Help;
      this.pcbHelpTiming.Image = global::Ogama.Properties.Resources.HelpBmp;
      this.pcbHelpTiming.Location = new System.Drawing.Point(250, 5);
      this.pcbHelpTiming.Name = "pcbHelpTiming";
      this.pcbHelpTiming.Size = new System.Drawing.Size(16, 16);
      this.pcbHelpTiming.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
      this.pcbHelpTiming.TabIndex = 7;
      this.pcbHelpTiming.TabStop = false;
      this.pcbHelpTiming.Click += new System.EventHandler(this.pcbHelpTiming_Click);
      // 
      // btnRemoveCondition
      // 
      this.btnRemoveCondition.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.btnRemoveCondition.ImageKey = "Delete";
      this.btnRemoveCondition.ImageList = this.imlCommands;
      this.btnRemoveCondition.Location = new System.Drawing.Point(166, 98);
      this.btnRemoveCondition.Name = "btnRemoveCondition";
      this.btnRemoveCondition.Size = new System.Drawing.Size(100, 23);
      this.btnRemoveCondition.TabIndex = 6;
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
      this.btnAddCondition.Location = new System.Drawing.Point(166, 70);
      this.btnAddCondition.Name = "btnAddCondition";
      this.btnAddCondition.Size = new System.Drawing.Size(100, 23);
      this.btnAddCondition.TabIndex = 6;
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
      this.lsbStopConditions.Location = new System.Drawing.Point(10, 125);
      this.lsbStopConditions.Name = "lsbStopConditions";
      this.lsbStopConditions.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
      this.lsbStopConditions.Size = new System.Drawing.Size(256, 82);
      this.lsbStopConditions.TabIndex = 5;
      // 
      // cbbKeys
      // 
      this.cbbKeys.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbbKeys.FormattingEnabled = true;
      this.cbbKeys.Location = new System.Drawing.Point(77, 52);
      this.cbbKeys.Name = "cbbKeys";
      this.cbbKeys.Size = new System.Drawing.Size(68, 21);
      this.cbbKeys.Sorted = true;
      this.cbbKeys.TabIndex = 2;
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(6, 6);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(189, 13);
      this.label3.TabIndex = 4;
      this.label3.Text = "The browsing should be completed by:";
      // 
      // nudTime
      // 
      this.nudTime.DecimalPlaces = 2;
      this.nudTime.Location = new System.Drawing.Point(77, 25);
      this.nudTime.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
      this.nudTime.Name = "nudTime";
      this.nudTime.Size = new System.Drawing.Size(68, 20);
      this.nudTime.TabIndex = 1;
      this.nudTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.nudTime.ThousandsSeparator = true;
      // 
      // rdbTime
      // 
      this.rdbTime.AutoSize = true;
      this.rdbTime.Checked = true;
      this.rdbTime.Location = new System.Drawing.Point(10, 25);
      this.rdbTime.Name = "rdbTime";
      this.rdbTime.Size = new System.Drawing.Size(67, 17);
      this.rdbTime.TabIndex = 3;
      this.rdbTime.TabStop = true;
      this.rdbTime.Text = "Time (/s)";
      this.rdbTime.UseVisualStyleBackColor = true;
      // 
      // rdbKey
      // 
      this.rdbKey.AutoSize = true;
      this.rdbKey.Location = new System.Drawing.Point(10, 53);
      this.rdbKey.Name = "rdbKey";
      this.rdbKey.Size = new System.Drawing.Size(43, 17);
      this.rdbKey.TabIndex = 6;
      this.rdbKey.Text = "Key";
      this.rdbKey.UseVisualStyleBackColor = true;
      // 
      // tbpBowseDepth
      // 
      this.tbpBowseDepth.Controls.Add(this.pcbHelpBrowseDepth);
      this.tbpBowseDepth.Controls.Add(this.label2);
      this.tbpBowseDepth.Controls.Add(this.label4);
      this.tbpBowseDepth.Controls.Add(this.label1);
      this.tbpBowseDepth.Controls.Add(this.nudBrowseDepth);
      this.tbpBowseDepth.Location = new System.Drawing.Point(4, 22);
      this.tbpBowseDepth.Name = "tbpBowseDepth";
      this.tbpBowseDepth.Padding = new System.Windows.Forms.Padding(3);
      this.tbpBowseDepth.Size = new System.Drawing.Size(272, 244);
      this.tbpBowseDepth.TabIndex = 8;
      this.tbpBowseDepth.Text = "Browse Depth";
      this.tbpBowseDepth.UseVisualStyleBackColor = true;
      // 
      // pcbHelpBrowseDepth
      // 
      this.pcbHelpBrowseDepth.Cursor = System.Windows.Forms.Cursors.Help;
      this.pcbHelpBrowseDepth.Image = global::Ogama.Properties.Resources.HelpBmp;
      this.pcbHelpBrowseDepth.Location = new System.Drawing.Point(250, 8);
      this.pcbHelpBrowseDepth.Name = "pcbHelpBrowseDepth";
      this.pcbHelpBrowseDepth.Size = new System.Drawing.Size(16, 16);
      this.pcbHelpBrowseDepth.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
      this.pcbHelpBrowseDepth.TabIndex = 10;
      this.pcbHelpBrowseDepth.TabStop = false;
      this.pcbHelpBrowseDepth.Click += new System.EventHandler(this.pcbHelpBrowseDepth_Click);
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(6, 30);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(72, 13);
      this.label2.TabIndex = 9;
      this.label2.Text = "Browse depth";
      // 
      // label4
      // 
      this.label4.Location = new System.Drawing.Point(6, 64);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(260, 105);
      this.label4.TabIndex = 9;
      this.label4.Text = resources.GetString("label4.Text");
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(5, 9);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(210, 13);
      this.label1.TabIndex = 9;
      this.label1.Text = "How deep the website should be available:";
      // 
      // nudBrowseDepth
      // 
      this.nudBrowseDepth.Location = new System.Drawing.Point(87, 28);
      this.nudBrowseDepth.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
      this.nudBrowseDepth.Name = "nudBrowseDepth";
      this.nudBrowseDepth.Size = new System.Drawing.Size(68, 20);
      this.nudBrowseDepth.TabIndex = 8;
      this.nudBrowseDepth.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      // 
      // grpPreview
      // 
      this.grpPreview.Controls.Add(this.webBrowserPreview);
      this.grpPreview.Dock = System.Windows.Forms.DockStyle.Fill;
      this.grpPreview.Location = new System.Drawing.Point(0, 0);
      this.grpPreview.Name = "grpPreview";
      this.grpPreview.Size = new System.Drawing.Size(377, 270);
      this.grpPreview.TabIndex = 24;
      this.grpPreview.TabStop = false;
      this.grpPreview.Text = "Preview";
      // 
      // webBrowserPreview
      // 
      this.webBrowserPreview.Dock = System.Windows.Forms.DockStyle.Fill;
      this.webBrowserPreview.Location = new System.Drawing.Point(3, 16);
      this.webBrowserPreview.MinimumSize = new System.Drawing.Size(20, 20);
      this.webBrowserPreview.Name = "webBrowserPreview";
      this.webBrowserPreview.ScriptErrorsSuppressed = true;
      this.webBrowserPreview.Size = new System.Drawing.Size(371, 251);
      this.webBrowserPreview.TabIndex = 0;
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
      this.panel1.Size = new System.Drawing.Size(667, 359);
      this.panel1.TabIndex = 19;
      // 
      // toolTip1
      // 
      this.toolTip1.ShowAlways = true;
      // 
      // dialogTop1
      // 
      this.dialogTop1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(237)))));
      this.dialogTop1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("dialogTop1.BackgroundImage")));
      this.dialogTop1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
      this.dialogTop1.Description = "Please specify the URL for the browsers starting page.";
      this.dialogTop1.Dock = System.Windows.Forms.DockStyle.Top;
      this.dialogTop1.Location = new System.Drawing.Point(0, 0);
      this.dialogTop1.Logo = global::Ogama.Properties.Resources.Globe32;
      this.dialogTop1.Name = "dialogTop1";
      this.dialogTop1.Size = new System.Drawing.Size(667, 60);
      this.dialogTop1.TabIndex = 22;
      // 
      // BrowserDialog
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.btnCancel;
      this.ClientSize = new System.Drawing.Size(667, 419);
      this.Controls.Add(this.panel1);
      this.Controls.Add(this.dialogTop1);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "BrowserDialog";
      this.ShowIcon = false;
      this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Add new browser slide ...";
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
      this.splitContainer1.ResumeLayout(false);
      this.splitContainer2.Panel1.ResumeLayout(false);
      this.splitContainer2.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
      this.splitContainer2.ResumeLayout(false);
      this.grpImageProperties.ResumeLayout(false);
      this.grpImageProperties.PerformLayout();
      this.splitContainer3.Panel1.ResumeLayout(false);
      this.splitContainer3.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
      this.splitContainer3.ResumeLayout(false);
      this.tabControl1.ResumeLayout(false);
      this.tbpName.ResumeLayout(false);
      this.tbpName.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pcbHelpNaming)).EndInit();
      this.tbpTiming.ResumeLayout(false);
      this.tbpTiming.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pcbHelpTiming)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.nudTime)).EndInit();
      this.tbpBowseDepth.ResumeLayout(false);
      this.tbpBowseDepth.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pcbHelpBrowseDepth)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.nudBrowseDepth)).EndInit();
      this.grpPreview.ResumeLayout(false);
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
    private System.Windows.Forms.TextBox txbURL;
    private System.Windows.Forms.GroupBox grpPreview;
    private System.Windows.Forms.ToolTip toolTip1;
    private System.Windows.Forms.GroupBox grpImageProperties;
    private OgamaControls.CommonControls.Browser.ExtendedBrowser webBrowserPreview;
    private System.Windows.Forms.TabControl tabControl1;
    private System.Windows.Forms.TabPage tbpTiming;
    private System.Windows.Forms.PictureBox pcbHelpTiming;
    private System.Windows.Forms.Button btnRemoveCondition;
    private System.Windows.Forms.Button btnAddCondition;
    private System.Windows.Forms.ListBox lsbStopConditions;
    private System.Windows.Forms.ComboBox cbbKeys;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.NumericUpDown nudTime;
    private System.Windows.Forms.RadioButton rdbTime;
    private System.Windows.Forms.RadioButton rdbKey;
    private System.Windows.Forms.SplitContainer splitContainer2;
    private System.Windows.Forms.SplitContainer splitContainer3;
    private System.Windows.Forms.TabPage tbpName;
    private System.Windows.Forms.Label label11;
    private System.Windows.Forms.ComboBox cbbCategory;
    private System.Windows.Forms.Label label10;
    private System.Windows.Forms.TextBox txbName;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.TabPage tbpBowseDepth;
    private System.Windows.Forms.PictureBox pcbHelpBrowseDepth;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.NumericUpDown nudBrowseDepth;
    private System.Windows.Forms.ImageList imlCommands;
    private System.Windows.Forms.PictureBox pcbHelpNaming;
    private System.Windows.Forms.Label label4;
  }
}