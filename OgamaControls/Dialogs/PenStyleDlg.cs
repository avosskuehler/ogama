using System;
using System.Drawing;
using System.Windows.Forms;

namespace OgamaControls.Dialogs
{
  using VectorGraphics.Tools.CustomEventArgs;

  /// <summary>
  /// Dialog for selecting pen style.
  /// i.e. pen style, thickness and color
  /// </summary>
  [ToolboxBitmap(typeof(resfinder), "OgamaControls.Dialogs.PenStyleDlg.bmp")]
  public partial class PenStyleDlg : Form
  {
    /// <summary>
    /// Event handler. Raised, when dialogs parameters has changed.
    /// </summary>
    public event EventHandler<PenChangedEventArgs> PenChanged;

    /// <summary>
    /// Gets or sets dialogs current pen.
    /// </summary>
    public Pen Pen
    {
      get { return penSelectControl.Pen; }
      set { penSelectControl.Pen = value; }
    }

    /// <summary>
    /// Constructor. Initializes dialog and callback host.
    /// </summary>
    public PenStyleDlg()
    {
      InitializeComponent();
    }


    /// <summary>
    /// Raises <see cref="PenChanged"/> event, when 
    /// new pen is selected in the underlying control.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void penSelectControl_PenChanged(object sender, PenChangedEventArgs e)
    {
      if (PenChanged != null)
      {
        PenChanged(this, new PenChangedEventArgs((Pen)e.Pen.Clone(), e.ElementGroup));
      }
    }
  }
}