using System;
using System.Drawing;
using System.Windows.Forms;

using VectorGraphics.Elements;

namespace OgamaControls
{
  using VectorGraphics.Tools.CustomEventArgs;

  /// <summary>
  /// Area to show pen style.
  /// </summary>
  [ToolboxBitmap(typeof(BrushStyleArea))]
  public partial class BrushStyleArea : UserControl
  {
    private static Brush DEFAULT_BRUSH = Brushes.Red;

    private Brush m_Brush;

    /// <summary>
    /// Event handler. Raised, when dialogs parameters has changed.
    /// </summary>
    public event EventHandler<BrushChangedEventArgs> BrushStyleChanged;

    /// <summary>
    /// Gets or sets the controls brush.
    /// </summary>
    public Brush Brush
    {
      get { return m_Brush; }
      set
      {
        if (value != null)
        {
          m_Brush = (Brush)value.Clone();
          OnBrushStyleChanged(new BrushChangedEventArgs(m_Brush, VGStyleGroup.None));
          Refresh();
        }
      }
    }

    /// <summary>
    /// Constructor. Initializes members.
    /// </summary>
    public BrushStyleArea()
    {
      //Only Required for Designer will be overwritten in Initialize Component
      //with Application Settings value.
      this.Brush = DEFAULT_BRUSH;
      InitializeComponent();
    }

    /// <summary>
    /// Paint event handler. Redraws area with current Pen.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void BrushStyleArea_Paint(object sender, PaintEventArgs e)
    {
      if (this.Enabled)
      {
        e.Graphics.DrawRectangle(SystemPens.ActiveBorder, 0, 0, this.Width - 1, this.Height - 1);
        e.Graphics.FillRectangle(m_Brush, 4, 4, this.Width - 8, this.Height - 8);
      }
      else
      {
        e.Graphics.DrawRectangle(SystemPens.InactiveBorder,0, 0, this.Width-1, this.Height-1);
        e.Graphics.FillRectangle(SystemBrushes.GrayText, 4,4,this.Width-8,this.Height-8);
      }
    }

    /// <summary>
    /// The protected OnPenStyleChanged method raises the progress event by invoking 
    /// the delegates
    /// </summary>
    /// <param name="e">A <see cref="BrushChangedEventArgs"/> with the event arguments</param>
    protected virtual void OnBrushStyleChanged(BrushChangedEventArgs e)
    {
      if (BrushStyleChanged != null)
      {
        // Invokes the delegates. 
        BrushStyleChanged(this, e);
      }
    }


    ///// <summary>
    ///// Eventhandler. Updates the line sample with the new pen.
    ///// </summary>
    ///// <param name="sender">message sender</param>
    ///// <param name="e">Pen change event arguments that hold new pen</param>
    //public void ThisBrushChanged(object sender, BrushChangedEventArgs e)
    //{
    //  this.Brush = e.Brush;
    //  OnBrushStyleChanged(e);
    //  Invalidate();
    //}
  }
}
