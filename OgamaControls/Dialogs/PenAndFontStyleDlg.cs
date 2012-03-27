using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using VectorGraphics;
using VectorGraphics.Elements;

namespace OgamaControls.Dialogs
{
  using VectorGraphics.Tools.CustomEventArgs;

  /// <summary>
  /// Dialog for selecting pen and font style together.
  /// </summary>
  [ToolboxBitmap(typeof(resfinder), "OgamaControls.Dialogs.PenAndFontStyleDlg.bmp")]
  public partial class PenAndFontStyleDlg : Form
  {
    /// <summary>
    /// Event handler. Raised, when dialogs font parameters has changed.
    /// </summary>
    public event EventHandler<FontChangedEventArgs> FontStyleChanged;

    /// <summary>
    /// Event handler. Raised, when dialogs pen parameters has changed.
    /// </summary>
    public event EventHandler<PenChangedEventArgs> PenChanged;

    /// <summary>
    /// Sets current dialogs and children controls font.
    /// </summary>
    public Font CustomFont
    {
      set { fontSelectControl.SelectedFont = value; }
    }

    /// <summary>
    /// Sets current dialogs and children controls font brush
    /// </summary>
    public Brush CustomFontBrush
    {
      set
      {
        SolidBrush newBrush = (SolidBrush)value;
        fontSelectControl.SelectedFontColor = newBrush.Color;
      }
    }

    /// <summary>
    /// Sets current dialogs and children controls font text alignment
    /// </summary>
    public VGAlignment CustomFontTextAlignment
    {
      set
      {
        fontSelectControl.SelectedFontAlignment = value;
      }
    }

    /// <summary>
    /// Sets current dialogs and children controls pen.
    /// </summary>
    public Pen Pen
    {
      set { penSelectControl.Pen = value; }
    }

    /// <summary>
    /// Constructor. Initializes dialog and callback host for owned controls.
    /// </summary>
    public PenAndFontStyleDlg()
    {
      InitializeComponent();
      penSelectControl.Pen = Pens.Red;
      fontSelectControl.Font = SystemFonts.MenuFont;
    }

    private void OnPenChanged(PenChangedEventArgs e)
    {
      if (PenChanged != null)
      {
        // Invokes the delegates. 
        PenChanged(this, e);
      }
    }

    /// <summary>
    /// The PenStyle has been modified, notify the callback host if any
    /// </summary>
    private void OnFontStyleChanged(FontChangedEventArgs e)
    {
      if (FontStyleChanged != null)
      {
        // Invokes the delegates. 
        FontStyleChanged(this, e);
      }
    }

    private void penSelectControl_PenChanged(object sender, PenChangedEventArgs e)
    {
      OnPenChanged(e);
    }

    private void fontSelectControl_FontStyleChanged(object sender, FontChangedEventArgs e)
    {
      OnFontStyleChanged(e);
    }

  }
}