using System.Windows.Forms;

namespace OgamaControls
{
  partial class RTBTextControl
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

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      this.ofd1 = new System.Windows.Forms.OpenFileDialog();
      this.sfd1 = new System.Windows.Forms.SaveFileDialog();
      this.tosMenu = new System.Windows.Forms.ToolStrip();
      this.lblLabel = new System.Windows.Forms.ToolStripLabel();
      this.cbbFontFamilies = new System.Windows.Forms.ToolStripComboBox();
      this.cbbFontSize = new System.Windows.Forms.ToolStripComboBox();
      this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
      this.btnBold = new System.Windows.Forms.ToolStripButton();
      this.btnItalic = new System.Windows.Forms.ToolStripButton();
      this.btnUnderline = new System.Windows.Forms.ToolStripButton();
      this.btnStrikeout = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
      this.btnLeftAlign = new System.Windows.Forms.ToolStripButton();
      this.btnCenterAlign = new System.Windows.Forms.ToolStripButton();
      this.btnRightAlign = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
      this.btnUndo = new System.Windows.Forms.ToolStripButton();
      this.btnRedo = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
      this.btnCut = new System.Windows.Forms.ToolStripButton();
      this.btnCopy = new System.Windows.Forms.ToolStripButton();
      this.btnPaste = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.btnSave = new System.Windows.Forms.ToolStripButton();
      this.btnOpen = new System.Windows.Forms.ToolStripButton();
      this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
      this.cmuText = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.fontStyleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.cmuFont = new System.Windows.Forms.ToolStripComboBox();
      this.cmuFontsize = new System.Windows.Forms.ToolStripComboBox();
      this.cmuSepAfterColor = new System.Windows.Forms.ToolStripSeparator();
      this.cmuBold = new System.Windows.Forms.ToolStripMenuItem();
      this.cmuItalic = new System.Windows.Forms.ToolStripMenuItem();
      this.cmuUnderline = new System.Windows.Forms.ToolStripMenuItem();
      this.cmuStrikeout = new System.Windows.Forms.ToolStripMenuItem();
      this.cmuSepAfterStrikeout = new System.Windows.Forms.ToolStripSeparator();
      this.cmuAlignleft = new System.Windows.Forms.ToolStripMenuItem();
      this.cmuAlignCenter = new System.Windows.Forms.ToolStripMenuItem();
      this.cmuAlignRight = new System.Windows.Forms.ToolStripMenuItem();
      this.cmuSepAfterAlignRight = new System.Windows.Forms.ToolStripSeparator();
      this.cmuUndo = new System.Windows.Forms.ToolStripMenuItem();
      this.cmuRedo = new System.Windows.Forms.ToolStripMenuItem();
      this.cmuSepAfterRedo = new System.Windows.Forms.ToolStripSeparator();
      this.cmuCut = new System.Windows.Forms.ToolStripMenuItem();
      this.cmuCopy = new System.Windows.Forms.ToolStripMenuItem();
      this.cmuPaste = new System.Windows.Forms.ToolStripMenuItem();
      this.cmuSepAfterPaste = new System.Windows.Forms.ToolStripSeparator();
      this.cmuSave = new System.Windows.Forms.ToolStripMenuItem();
      this.cmuOpen = new System.Windows.Forms.ToolStripMenuItem();
      this.rtb1 = new OgamaControls.PrintableRichTextBox();
      this.cbbColor = new OgamaControls.ToolStripColorDropdown();
      this.cmuFontColor = new OgamaControls.ToolStripColorDropdown();
      this.tosMenu.SuspendLayout();
      this.toolStripContainer1.ContentPanel.SuspendLayout();
      this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
      this.toolStripContainer1.SuspendLayout();
      this.cmuText.SuspendLayout();
      this.SuspendLayout();
      // 
      // ofd1
      // 
      this.ofd1.DefaultExt = "rtf";
      this.ofd1.Filter = "Rich Text Files|*.rtf|Plain Text File|*.txt";
      this.ofd1.Title = "Open File";
      // 
      // sfd1
      // 
      this.sfd1.DefaultExt = "rtf";
      this.sfd1.Filter = "Rich Text File|*.rtf|Plain Text File|*.txt";
      this.sfd1.Title = "Save As";
      // 
      // tosMenu
      // 
      this.tosMenu.AllowMerge = false;
      this.tosMenu.Dock = System.Windows.Forms.DockStyle.None;
      this.tosMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblLabel,
            this.cbbFontFamilies,
            this.cbbFontSize,
            this.cbbColor,
            this.toolStripSeparator2,
            this.btnBold,
            this.btnItalic,
            this.btnUnderline,
            this.btnStrikeout,
            this.toolStripSeparator3,
            this.btnLeftAlign,
            this.btnCenterAlign,
            this.btnRightAlign,
            this.toolStripSeparator4,
            this.btnUndo,
            this.btnRedo,
            this.toolStripSeparator5,
            this.btnCut,
            this.btnCopy,
            this.btnPaste,
            this.toolStripSeparator1,
            this.btnSave,
            this.btnOpen});
      this.tosMenu.Location = new System.Drawing.Point(3, 0);
      this.tosMenu.Name = "tosMenu";
      this.tosMenu.Size = new System.Drawing.Size(648, 25);
      this.tosMenu.TabIndex = 2;
      // 
      // lblLabel
      // 
      this.lblLabel.Name = "lblLabel";
      this.lblLabel.Size = new System.Drawing.Size(32, 22);
      this.lblLabel.Text = "Label";
      // 
      // cbbFontFamilies
      // 
      this.cbbFontFamilies.Name = "cbbFontFamilies";
      this.cbbFontFamilies.Size = new System.Drawing.Size(121, 25);
      this.cbbFontFamilies.Sorted = true;
      this.cbbFontFamilies.SelectedIndexChanged += new System.EventHandler(this.cbbFontFamilies_SelectedIndexChanged);
      // 
      // cbbFontSize
      // 
      this.cbbFontSize.AutoSize = false;
      this.cbbFontSize.Items.AddRange(new object[] {
            "8",
            "9",
            "10",
            "11",
            "12",
            "14",
            "16",
            "18",
            "20",
            "22",
            "24",
            "26",
            "28",
            "36",
            "48",
            "72"});
      this.cbbFontSize.Name = "cbbFontSize";
      this.cbbFontSize.Size = new System.Drawing.Size(50, 21);
      this.cbbFontSize.Text = "8";
      this.cbbFontSize.SelectedIndexChanged += new System.EventHandler(this.cbbFontSize_SelectedIndexChanged);
      this.cbbFontSize.TextChanged += new System.EventHandler(this.cbbFontSize_TextChanged);
      // 
      // toolStripSeparator2
      // 
      this.toolStripSeparator2.Name = "toolStripSeparator2";
      this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
      // 
      // btnBold
      // 
      this.btnBold.CheckOnClick = true;
      this.btnBold.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnBold.Image = global::OgamaControls.Properties.Resources.boldhs;
      this.btnBold.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnBold.Name = "btnBold";
      this.btnBold.Size = new System.Drawing.Size(23, 22);
      this.btnBold.Text = "Fett";
      this.btnBold.Click += new System.EventHandler(this.btnBold_Click);
      // 
      // btnItalic
      // 
      this.btnItalic.CheckOnClick = true;
      this.btnItalic.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnItalic.Image = global::OgamaControls.Properties.Resources.ItalicHS;
      this.btnItalic.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnItalic.Name = "btnItalic";
      this.btnItalic.Size = new System.Drawing.Size(23, 22);
      this.btnItalic.Text = "Kursiv";
      this.btnItalic.Click += new System.EventHandler(this.btnItalic_Click);
      // 
      // btnUnderline
      // 
      this.btnUnderline.CheckOnClick = true;
      this.btnUnderline.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnUnderline.Image = global::OgamaControls.Properties.Resources.underlineHS;
      this.btnUnderline.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnUnderline.Name = "btnUnderline";
      this.btnUnderline.Size = new System.Drawing.Size(23, 22);
      this.btnUnderline.Text = "Unterstrichen";
      this.btnUnderline.Click += new System.EventHandler(this.btnUnderline_Click);
      // 
      // btnStrikeout
      // 
      this.btnStrikeout.CheckOnClick = true;
      this.btnStrikeout.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnStrikeout.Image = global::OgamaControls.Properties.Resources.StrikeoutHS;
      this.btnStrikeout.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnStrikeout.Name = "btnStrikeout";
      this.btnStrikeout.Size = new System.Drawing.Size(23, 22);
      this.btnStrikeout.Text = "Durchgestrichen";
      this.btnStrikeout.Click += new System.EventHandler(this.btnStrikeout_Click);
      // 
      // toolStripSeparator3
      // 
      this.toolStripSeparator3.Name = "toolStripSeparator3";
      this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
      // 
      // btnLeftAlign
      // 
      this.btnLeftAlign.CheckOnClick = true;
      this.btnLeftAlign.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnLeftAlign.Image = global::OgamaControls.Properties.Resources.AlignTableCellMiddleLeftHS;
      this.btnLeftAlign.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnLeftAlign.Name = "btnLeftAlign";
      this.btnLeftAlign.Size = new System.Drawing.Size(23, 22);
      this.btnLeftAlign.Text = "Linksbündig";
      this.btnLeftAlign.Click += new System.EventHandler(this.btnLeftAlign_Click);
      // 
      // btnCenterAlign
      // 
      this.btnCenterAlign.CheckOnClick = true;
      this.btnCenterAlign.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnCenterAlign.Image = global::OgamaControls.Properties.Resources.AlignTableCellMiddleCenterHS;
      this.btnCenterAlign.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnCenterAlign.Name = "btnCenterAlign";
      this.btnCenterAlign.Size = new System.Drawing.Size(23, 22);
      this.btnCenterAlign.Text = "Zentriert";
      this.btnCenterAlign.Click += new System.EventHandler(this.btnCenter_Click);
      // 
      // btnRightAlign
      // 
      this.btnRightAlign.CheckOnClick = true;
      this.btnRightAlign.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnRightAlign.Image = global::OgamaControls.Properties.Resources.AlignTableCellMiddleRightHS;
      this.btnRightAlign.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnRightAlign.Name = "btnRightAlign";
      this.btnRightAlign.Size = new System.Drawing.Size(23, 22);
      this.btnRightAlign.Text = "Rechtsbündig";
      this.btnRightAlign.Click += new System.EventHandler(this.btnRightAlign_Click);
      // 
      // toolStripSeparator4
      // 
      this.toolStripSeparator4.Name = "toolStripSeparator4";
      this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
      // 
      // btnUndo
      // 
      this.btnUndo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnUndo.Image = global::OgamaControls.Properties.Resources.Edit_UndoHS;
      this.btnUndo.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnUndo.Name = "btnUndo";
      this.btnUndo.Size = new System.Drawing.Size(23, 22);
      this.btnUndo.Text = "Rückgängig";
      this.btnUndo.Click += new System.EventHandler(this.btnUndo_Click);
      // 
      // btnRedo
      // 
      this.btnRedo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnRedo.Image = global::OgamaControls.Properties.Resources.Edit_RedoHS;
      this.btnRedo.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnRedo.Name = "btnRedo";
      this.btnRedo.Size = new System.Drawing.Size(23, 22);
      this.btnRedo.Text = "Wiederherstellen";
      this.btnRedo.Click += new System.EventHandler(this.btnRedo_Click);
      // 
      // toolStripSeparator5
      // 
      this.toolStripSeparator5.Name = "toolStripSeparator5";
      this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
      // 
      // btnCut
      // 
      this.btnCut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnCut.Image = global::OgamaControls.Properties.Resources.CutHS;
      this.btnCut.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnCut.Name = "btnCut";
      this.btnCut.Size = new System.Drawing.Size(23, 22);
      this.btnCut.Text = "Ausschneiden";
      this.btnCut.Click += new System.EventHandler(this.btnCut_Click);
      // 
      // btnCopy
      // 
      this.btnCopy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnCopy.Image = global::OgamaControls.Properties.Resources.CopyHS;
      this.btnCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnCopy.Name = "btnCopy";
      this.btnCopy.Size = new System.Drawing.Size(23, 22);
      this.btnCopy.Text = "Kopieren";
      this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
      // 
      // btnPaste
      // 
      this.btnPaste.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnPaste.Image = global::OgamaControls.Properties.Resources.PasteHS;
      this.btnPaste.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnPaste.Name = "btnPaste";
      this.btnPaste.Size = new System.Drawing.Size(23, 22);
      this.btnPaste.Text = "Einfügen";
      this.btnPaste.Click += new System.EventHandler(this.btnPaste_Click);
      // 
      // toolStripSeparator1
      // 
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
      // 
      // btnSave
      // 
      this.btnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnSave.Image = global::OgamaControls.Properties.Resources.saveHS;
      this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnSave.Name = "btnSave";
      this.btnSave.Size = new System.Drawing.Size(23, 22);
      this.btnSave.Text = "Speichern";
      this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
      // 
      // btnOpen
      // 
      this.btnOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnOpen.Image = global::OgamaControls.Properties.Resources.openHS;
      this.btnOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnOpen.Name = "btnOpen";
      this.btnOpen.Size = new System.Drawing.Size(23, 22);
      this.btnOpen.Text = "Öffnen";
      this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
      // 
      // toolStripContainer1
      // 
      // 
      // toolStripContainer1.ContentPanel
      // 
      this.toolStripContainer1.ContentPanel.Controls.Add(this.rtb1);
      this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(691, 186);
      this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
      this.toolStripContainer1.Name = "toolStripContainer1";
      this.toolStripContainer1.Size = new System.Drawing.Size(691, 211);
      this.toolStripContainer1.TabIndex = 3;
      this.toolStripContainer1.Text = "toolStripContainer1";
      // 
      // toolStripContainer1.TopToolStripPanel
      // 
      this.toolStripContainer1.TopToolStripPanel.BackColor = System.Drawing.SystemColors.Control;
      this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.tosMenu);
      this.toolStripContainer1.TopToolStripPanel.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
      // 
      // cmuText
      // 
      this.cmuText.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fontStyleToolStripMenuItem,
            this.cmuFont,
            this.cmuFontsize,
            this.cmuFontColor,
            this.cmuSepAfterColor,
            this.cmuBold,
            this.cmuItalic,
            this.cmuUnderline,
            this.cmuStrikeout,
            this.cmuSepAfterStrikeout,
            this.cmuAlignleft,
            this.cmuAlignCenter,
            this.cmuAlignRight,
            this.cmuSepAfterAlignRight,
            this.cmuUndo,
            this.cmuRedo,
            this.cmuSepAfterRedo,
            this.cmuCut,
            this.cmuCopy,
            this.cmuPaste,
            this.cmuSepAfterPaste,
            this.cmuSave,
            this.cmuOpen});
      this.cmuText.Name = "contextMenuStrip1";
      this.cmuText.Size = new System.Drawing.Size(182, 439);
      // 
      // fontStyleToolStripMenuItem
      // 
      this.fontStyleToolStripMenuItem.Enabled = false;
      this.fontStyleToolStripMenuItem.Image = global::OgamaControls.Properties.Resources.FontHS;
      this.fontStyleToolStripMenuItem.Name = "fontStyleToolStripMenuItem";
      this.fontStyleToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
      this.fontStyleToolStripMenuItem.Text = "Font Style";
      // 
      // cmuFont
      // 
      this.cmuFont.Name = "cmuFont";
      this.cmuFont.Size = new System.Drawing.Size(121, 21);
      this.cmuFont.Sorted = true;
      this.cmuFont.SelectedIndexChanged += new System.EventHandler(this.cbbFontFamilies_SelectedIndexChanged);
      // 
      // cmuFontsize
      // 
      this.cmuFontsize.AutoSize = false;
      this.cmuFontsize.Items.AddRange(new object[] {
            "8",
            "9",
            "10",
            "11",
            "12",
            "14",
            "16",
            "18",
            "20",
            "22",
            "24",
            "26",
            "28",
            "36",
            "48",
            "72"});
      this.cmuFontsize.Name = "cmuFontsize";
      this.cmuFontsize.Size = new System.Drawing.Size(50, 21);
      this.cmuFontsize.Text = "8";
      this.cmuFontsize.SelectedIndexChanged += new System.EventHandler(this.cbbFontSize_SelectedIndexChanged);
      this.cmuFontsize.TextChanged += new System.EventHandler(this.cbbFontSize_TextChanged);
      // 
      // cmuSepAfterColor
      // 
      this.cmuSepAfterColor.Name = "cmuSepAfterColor";
      this.cmuSepAfterColor.Size = new System.Drawing.Size(178, 6);
      // 
      // cmuBold
      // 
      this.cmuBold.CheckOnClick = true;
      this.cmuBold.Image = global::OgamaControls.Properties.Resources.boldhs;
      this.cmuBold.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.cmuBold.Name = "cmuBold";
      this.cmuBold.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.B)));
      this.cmuBold.Size = new System.Drawing.Size(181, 22);
      this.cmuBold.Text = "Bold";
      this.cmuBold.Click += new System.EventHandler(this.btnBold_Click);
      // 
      // cmuItalic
      // 
      this.cmuItalic.CheckOnClick = true;
      this.cmuItalic.Image = global::OgamaControls.Properties.Resources.ItalicHS;
      this.cmuItalic.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.cmuItalic.Name = "cmuItalic";
      this.cmuItalic.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I)));
      this.cmuItalic.Size = new System.Drawing.Size(181, 22);
      this.cmuItalic.Text = "Italic";
      this.cmuItalic.Click += new System.EventHandler(this.btnItalic_Click);
      // 
      // cmuUnderline
      // 
      this.cmuUnderline.CheckOnClick = true;
      this.cmuUnderline.Image = global::OgamaControls.Properties.Resources.underlineHS;
      this.cmuUnderline.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.cmuUnderline.Name = "cmuUnderline";
      this.cmuUnderline.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.U)));
      this.cmuUnderline.Size = new System.Drawing.Size(181, 22);
      this.cmuUnderline.Text = "Underline";
      this.cmuUnderline.Click += new System.EventHandler(this.btnUnderline_Click);
      // 
      // cmuStrikeout
      // 
      this.cmuStrikeout.CheckOnClick = true;
      this.cmuStrikeout.Image = global::OgamaControls.Properties.Resources.StrikeoutHS;
      this.cmuStrikeout.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.cmuStrikeout.Name = "cmuStrikeout";
      this.cmuStrikeout.Size = new System.Drawing.Size(181, 22);
      this.cmuStrikeout.Text = "Strikeout";
      this.cmuStrikeout.Click += new System.EventHandler(this.btnStrikeout_Click);
      // 
      // cmuSepAfterStrikeout
      // 
      this.cmuSepAfterStrikeout.Name = "cmuSepAfterStrikeout";
      this.cmuSepAfterStrikeout.Size = new System.Drawing.Size(178, 6);
      // 
      // cmuAlignleft
      // 
      this.cmuAlignleft.CheckOnClick = true;
      this.cmuAlignleft.Image = global::OgamaControls.Properties.Resources.AlignTableCellMiddleLeftHS;
      this.cmuAlignleft.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.cmuAlignleft.Name = "cmuAlignleft";
      this.cmuAlignleft.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
      this.cmuAlignleft.Size = new System.Drawing.Size(181, 22);
      this.cmuAlignleft.Text = "Align left";
      this.cmuAlignleft.Click += new System.EventHandler(this.btnLeftAlign_Click);
      // 
      // cmuAlignCenter
      // 
      this.cmuAlignCenter.CheckOnClick = true;
      this.cmuAlignCenter.Image = global::OgamaControls.Properties.Resources.AlignTableCellMiddleCenterHS;
      this.cmuAlignCenter.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.cmuAlignCenter.Name = "cmuAlignCenter";
      this.cmuAlignCenter.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
      this.cmuAlignCenter.Size = new System.Drawing.Size(181, 22);
      this.cmuAlignCenter.Text = "Align center";
      this.cmuAlignCenter.Click += new System.EventHandler(this.btnCenter_Click);
      // 
      // cmuAlignRight
      // 
      this.cmuAlignRight.CheckOnClick = true;
      this.cmuAlignRight.Image = global::OgamaControls.Properties.Resources.AlignTableCellMiddleRightHS;
      this.cmuAlignRight.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.cmuAlignRight.Name = "cmuAlignRight";
      this.cmuAlignRight.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
      this.cmuAlignRight.Size = new System.Drawing.Size(181, 22);
      this.cmuAlignRight.Text = "Align right";
      this.cmuAlignRight.Click += new System.EventHandler(this.btnRightAlign_Click);
      // 
      // cmuSepAfterAlignRight
      // 
      this.cmuSepAfterAlignRight.Name = "cmuSepAfterAlignRight";
      this.cmuSepAfterAlignRight.Size = new System.Drawing.Size(178, 6);
      // 
      // cmuUndo
      // 
      this.cmuUndo.Image = global::OgamaControls.Properties.Resources.Edit_UndoHS;
      this.cmuUndo.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.cmuUndo.Name = "cmuUndo";
      this.cmuUndo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
      this.cmuUndo.Size = new System.Drawing.Size(181, 22);
      this.cmuUndo.Text = "Undo";
      this.cmuUndo.Click += new System.EventHandler(this.btnUndo_Click);
      // 
      // cmuRedo
      // 
      this.cmuRedo.Image = global::OgamaControls.Properties.Resources.Edit_RedoHS;
      this.cmuRedo.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.cmuRedo.Name = "cmuRedo";
      this.cmuRedo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
      this.cmuRedo.Size = new System.Drawing.Size(181, 22);
      this.cmuRedo.Text = "Redo";
      this.cmuRedo.Click += new System.EventHandler(this.btnRedo_Click);
      // 
      // cmuSepAfterRedo
      // 
      this.cmuSepAfterRedo.Name = "cmuSepAfterRedo";
      this.cmuSepAfterRedo.Size = new System.Drawing.Size(178, 6);
      // 
      // cmuCut
      // 
      this.cmuCut.Image = global::OgamaControls.Properties.Resources.CutHS;
      this.cmuCut.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.cmuCut.Name = "cmuCut";
      this.cmuCut.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
      this.cmuCut.Size = new System.Drawing.Size(181, 22);
      this.cmuCut.Text = "Cut";
      this.cmuCut.Click += new System.EventHandler(this.btnCut_Click);
      // 
      // cmuCopy
      // 
      this.cmuCopy.Image = global::OgamaControls.Properties.Resources.CopyHS;
      this.cmuCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.cmuCopy.Name = "cmuCopy";
      this.cmuCopy.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
      this.cmuCopy.Size = new System.Drawing.Size(181, 22);
      this.cmuCopy.Text = "Copy";
      this.cmuCopy.Click += new System.EventHandler(this.btnCopy_Click);
      // 
      // cmuPaste
      // 
      this.cmuPaste.Image = global::OgamaControls.Properties.Resources.PasteHS;
      this.cmuPaste.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.cmuPaste.Name = "cmuPaste";
      this.cmuPaste.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
      this.cmuPaste.Size = new System.Drawing.Size(181, 22);
      this.cmuPaste.Text = "Paste";
      this.cmuPaste.Click += new System.EventHandler(this.btnPaste_Click);
      // 
      // cmuSepAfterPaste
      // 
      this.cmuSepAfterPaste.Name = "cmuSepAfterPaste";
      this.cmuSepAfterPaste.Size = new System.Drawing.Size(178, 6);
      // 
      // cmuSave
      // 
      this.cmuSave.Image = global::OgamaControls.Properties.Resources.saveHS;
      this.cmuSave.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.cmuSave.Name = "cmuSave";
      this.cmuSave.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
      this.cmuSave.Size = new System.Drawing.Size(181, 22);
      this.cmuSave.Text = "Save";
      this.cmuSave.Click += new System.EventHandler(this.btnSave_Click);
      // 
      // cmuOpen
      // 
      this.cmuOpen.Image = global::OgamaControls.Properties.Resources.openHS;
      this.cmuOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.cmuOpen.Name = "cmuOpen";
      this.cmuOpen.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
      this.cmuOpen.Size = new System.Drawing.Size(181, 22);
      this.cmuOpen.Text = "Open";
      this.cmuOpen.Click += new System.EventHandler(this.btnOpen_Click);
      // 
      // rtb1
      // 
      this.rtb1.AutoWordSelection = true;
      this.rtb1.BackColor = System.Drawing.SystemColors.Window;
      this.rtb1.ContextMenuStrip = this.cmuText;
      this.rtb1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.rtb1.EnableAutoDragDrop = true;
      this.rtb1.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.rtb1.Location = new System.Drawing.Point(0, 0);
      this.rtb1.Name = "rtb1";
      this.rtb1.Size = new System.Drawing.Size(691, 186);
      this.rtb1.TabIndex = 1;
      this.rtb1.Text = "";
      this.rtb1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.rtb1_KeyDown);
      this.rtb1.SelectionChanged += new System.EventHandler(this.rtb1_SelectionChanged);
      this.rtb1.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.rtb1_LinkClicked);
      this.rtb1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.rtb1_KeyPress);
      this.rtb1.TextChanged += new System.EventHandler(this.rtb1_TextChanged);
      // 
      // cbbColor
      // 
      this.cbbColor.BackColor = System.Drawing.Color.Red;
      this.cbbColor.CurrentColor = System.Drawing.Color.Red;
      this.cbbColor.Name = "cbbColor";
      this.cbbColor.Size = new System.Drawing.Size(75, 25);
      this.cbbColor.ColorChanged += new System.EventHandler(this.cbbColor_ColorChanged);
      // 
      // cmuFontColor
      // 
      this.cmuFontColor.BackColor = System.Drawing.Color.Red;
      this.cmuFontColor.CurrentColor = System.Drawing.Color.Red;
      this.cmuFontColor.Name = "cmuFontColor";
      this.cmuFontColor.Size = new System.Drawing.Size(75, 21);
      this.cmuFontColor.ColorChanged += new System.EventHandler(this.cbbColor_ColorChanged);
      // 
      // RTBTextControl
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.SystemColors.Window;
      this.Controls.Add(this.toolStripContainer1);
      this.Name = "RTBTextControl";
      this.Size = new System.Drawing.Size(691, 211);
      this.Load += new System.EventHandler(this.RTBTextControl_Load);
      this.Leave += new System.EventHandler(this.RTBTextControl_Leave);
      this.tosMenu.ResumeLayout(false);
      this.tosMenu.PerformLayout();
      this.toolStripContainer1.ContentPanel.ResumeLayout(false);
      this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
      this.toolStripContainer1.TopToolStripPanel.PerformLayout();
      this.toolStripContainer1.ResumeLayout(false);
      this.toolStripContainer1.PerformLayout();
      this.cmuText.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private PrintableRichTextBox rtb1;
    private System.Windows.Forms.OpenFileDialog ofd1;
    private System.Windows.Forms.SaveFileDialog sfd1;
    private ToolStrip tosMenu;
    private ToolStripButton btnBold;
    private ToolStripButton btnItalic;
    private ToolStripButton btnUnderline;
    private ToolStripSeparator toolStripSeparator2;
    private ToolStripButton btnUndo;
    private ToolStripButton btnRedo;
    private ToolStripSeparator toolStripSeparator5;
    private ToolStripButton btnCut;
    private ToolStripButton btnCopy;
    private ToolStripButton btnPaste;
    private ToolStripSeparator toolStripSeparator3;
    private ToolStripButton btnOpen;
    private ToolStripButton btnSave;
    private ToolStripSeparator toolStripSeparator1;
    private ToolStripButton btnStrikeout;
    private ToolStripButton btnLeftAlign;
    private ToolStripButton btnCenterAlign;
    private ToolStripButton btnRightAlign;
    private ToolStripSeparator toolStripSeparator4;
    private ToolStripContainer toolStripContainer1;
    private ToolStripLabel lblLabel;
    private ToolStripComboBox cbbFontSize;
    private ToolStripColorDropdown cbbColor;
    private ToolStripComboBox cbbFontFamilies;
    private ContextMenuStrip cmuText;
    private ToolStripMenuItem cmuSave;
    private ToolStripMenuItem cmuOpen;
    private ToolStripSeparator cmuSepAfterPaste;
    private ToolStripComboBox cmuFont;
    private ToolStripComboBox cmuFontsize;
    private ToolStripColorDropdown cmuFontColor;
    private ToolStripSeparator cmuSepAfterColor;
    private ToolStripMenuItem cmuBold;
    private ToolStripMenuItem cmuItalic;
    private ToolStripSeparator cmuSepAfterStrikeout;
    private ToolStripSeparator cmuSepAfterAlignRight;
    private ToolStripSeparator cmuSepAfterRedo;
    private ToolStripMenuItem cmuUnderline;
    private ToolStripMenuItem cmuStrikeout;
    private ToolStripMenuItem cmuAlignleft;
    private ToolStripMenuItem cmuAlignCenter;
    private ToolStripMenuItem cmuAlignRight;
    private ToolStripMenuItem cmuRedo;
    private ToolStripMenuItem cmuCut;
    private ToolStripMenuItem cmuCopy;
    private ToolStripMenuItem cmuPaste;
    private ToolStripMenuItem cmuUndo;
    private ToolStripMenuItem fontStyleToolStripMenuItem;

  }
}
