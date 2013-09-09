namespace OgamaControls
{
  partial class PrintableRichTextBox
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
      this.cmu = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.mnuCut = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuCopy = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuPaste = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.mnuBold = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuItalic = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuUnderline = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
      this.mnuGreek = new System.Windows.Forms.ToolStripMenuItem();
      this.cbbGreek = new System.Windows.Forms.ToolStripComboBox();
      this.cmu.SuspendLayout();
      this.SuspendLayout();
      // 
      // cmu
      // 
      this.cmu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuCut,
            this.mnuCopy,
            this.mnuPaste,
            this.toolStripSeparator1,
            this.mnuBold,
            this.mnuItalic,
            this.mnuUnderline,
            this.toolStripSeparator2,
            this.mnuGreek,
            this.cbbGreek});
      this.cmu.Name = "contextMenu";
      this.cmu.Size = new System.Drawing.Size(190, 191);
      this.cmu.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.cmu_ItemClicked);
      // 
      // mnuCut
      // 
      this.mnuCut.Image = global::OgamaControls.Properties.Resources.CutHS;
      this.mnuCut.Name = "mnuCut";
      this.mnuCut.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
      this.mnuCut.Size = new System.Drawing.Size(189, 22);
      this.mnuCut.Text = "Ausschneiden";
      // 
      // mnuCopy
      // 
      this.mnuCopy.Image = global::OgamaControls.Properties.Resources.CopyHS;
      this.mnuCopy.Name = "mnuCopy";
      this.mnuCopy.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
      this.mnuCopy.Size = new System.Drawing.Size(189, 22);
      this.mnuCopy.Text = "Kopieren";
      // 
      // mnuPaste
      // 
      this.mnuPaste.Image = global::OgamaControls.Properties.Resources.PasteHS;
      this.mnuPaste.Name = "mnuPaste";
      this.mnuPaste.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
      this.mnuPaste.Size = new System.Drawing.Size(189, 22);
      this.mnuPaste.Text = "Einfügen";
      // 
      // toolStripSeparator1
      // 
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new System.Drawing.Size(186, 6);
      // 
      // mnuBold
      // 
      this.mnuBold.Image = global::OgamaControls.Properties.Resources.boldhs;
      this.mnuBold.Name = "mnuBold";
      this.mnuBold.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.B)));
      this.mnuBold.Size = new System.Drawing.Size(189, 22);
      this.mnuBold.Text = "Fett";
      // 
      // mnuItalic
      // 
      this.mnuItalic.Image = global::OgamaControls.Properties.Resources.ItalicHS;
      this.mnuItalic.Name = "mnuItalic";
      this.mnuItalic.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I)));
      this.mnuItalic.Size = new System.Drawing.Size(189, 22);
      this.mnuItalic.Text = "Kursiv";
      // 
      // mnuUnderline
      // 
      this.mnuUnderline.Image = global::OgamaControls.Properties.Resources.underlineHS;
      this.mnuUnderline.Name = "mnuUnderline";
      this.mnuUnderline.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.U)));
      this.mnuUnderline.Size = new System.Drawing.Size(189, 22);
      this.mnuUnderline.Text = "Unterstrichen";
      // 
      // toolStripSeparator2
      // 
      this.toolStripSeparator2.Name = "toolStripSeparator2";
      this.toolStripSeparator2.Size = new System.Drawing.Size(186, 6);
      // 
      // mnuGreek
      // 
      this.mnuGreek.AutoSize = false;
      this.mnuGreek.Image = global::OgamaControls.Properties.Resources.SymbolHS;
      this.mnuGreek.Name = "mnuGreek";
      this.mnuGreek.Size = new System.Drawing.Size(189, 18);
      this.mnuGreek.Text = "Sonderzeichen:";
      // 
      // cbbGreek
      // 
      this.cbbGreek.Items.AddRange(new object[] {
            "Ø",
            "α",
            "β",
            "γ",
            "δ",
            "ε",
            "ζ",
            "η",
            "θ",
            "ι",
            "κ",
            "λ",
            "μ",
            "ν",
            "ξ",
            "π",
            "ρ",
            "ς",
            "σ",
            "τ",
            "υ",
            "φ",
            "χ",
            "ψ",
            "ω"});
      this.cbbGreek.Name = "cbbGreek";
      this.cbbGreek.Size = new System.Drawing.Size(121, 21);
      // 
      // PrintableRichTextBox
      // 
      this.ContextMenuStrip = this.cmu;
      this.cmu.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.ContextMenuStrip cmu;
    private System.Windows.Forms.ToolStripMenuItem mnuCut;
    private System.Windows.Forms.ToolStripMenuItem mnuCopy;
    private System.Windows.Forms.ToolStripMenuItem mnuPaste;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    private System.Windows.Forms.ToolStripMenuItem mnuBold;
    private System.Windows.Forms.ToolStripMenuItem mnuItalic;
    private System.Windows.Forms.ToolStripMenuItem mnuUnderline;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    private System.Windows.Forms.ToolStripMenuItem mnuGreek;
    private System.Windows.Forms.ToolStripComboBox cbbGreek;
  }
}
