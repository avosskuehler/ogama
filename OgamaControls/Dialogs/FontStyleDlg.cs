using System;
using System.Drawing;
using System.Windows.Forms;

using VectorGraphics.Elements;

namespace OgamaControls.Dialogs
{
  using VectorGraphics.Tools.CustomEventArgs;

  /// <summary>
  /// Dialog for selecting font style.
  /// i.e. font face, size and style
  /// </summary>
  [ToolboxBitmap(typeof(FontDialog))]
  public partial class FontStyleDlg : Form
  {
    /// <summary>
    /// Event handler. Raised, when dialogs parameters has changed.
    /// </summary>
    public event EventHandler<FontChangedEventArgs> FontStyleChanged;

    /// <summary>
    /// Gets or sets dialogs current font color.
    /// </summary>
    public Color CurrentFontColor
    {
      get { return fontSelectControl1.SelectedFontColor; }
      set { fontSelectControl1.SelectedFontColor = value; }
    }

    /// <summary>
    /// Gets or sets dialogs current font.
    /// </summary>
    public Font CurrentFont
    {
      get { return fontSelectControl1.SelectedFont; }
      set { fontSelectControl1.SelectedFont = value; }
    }

    /// <summary>
    /// Gets or sets dialogs current font.
    /// </summary>
    public VGAlignment CurrentFontAlignment
    {
      get { return fontSelectControl1.SelectedFontAlignment; }
      set { fontSelectControl1.SelectedFontAlignment = value; }
    }

    /// <summary>
    /// Constructor. Initializes dialog and callback host.
    /// </summary>
    public FontStyleDlg()
    {
      InitializeComponent();
    }

    /// <summary>
    /// The PenStyle has been modified, notify the callback host if any
    /// </summary>
    private void OnFontChanged(FontChangedEventArgs e)
    {
      if (FontStyleChanged != null)
      {
        // Invokes the delegates. 
        FontStyleChanged(this, e);
      }
    }

    private void fontSelectControl1_FontStyleChanged(object sender, FontChangedEventArgs e)
    {
      OnFontChanged(e);
    }
  }
}