using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Configuration;

using VectorGraphics.Elements;

namespace OgamaControls
{
  using VectorGraphics.Tools.CustomEventArgs;

  /// <summary>
  /// Area to show pen style.
  /// </summary>
  [ToolboxBitmap(typeof(resfinder), "OgamaControls.CommonControls.Styles.PenStyleArea.bmp")]
  public partial class PenStyleArea : UserControl
  {
    /// <summary>
    /// Saves the controls visualized Pen.
    /// </summary>
    private Pen m_Pen;
    private Color m_PenColor;
    private DashStyle m_PenDashStyle;
    private Single m_PenSize;

    /// <summary>
    /// Event handler. Raised, when dialogs parameters has changed.
    /// </summary>
    public event EventHandler<PenChangedEventArgs> PenStyleChanged;

    /// <summary>
    /// Gets or sets the controls pen.
    /// </summary>
    public Pen Pen
    {
      get { return m_Pen; }
      set
      {
        m_Pen = (Pen)value.Clone();
        this.PenColor = m_Pen.Color;
        this.PenDashStyle = m_Pen.DashStyle;
        this.PenSize = m_Pen.Width;
        OnPenStyleChanged(new PenChangedEventArgs(m_Pen, VGStyleGroup.None));
        Invalidate();
      }
    }

    /// <summary>
    /// Gets or sets the control pens color.
    /// </summary>
    [Description("PenColor")]
    [Category("Appearance")]
    [SettingsBindable(true), Bindable(true)]
    [SettingsSerializeAs(SettingsSerializeAs.String)]
    public Color PenColor
    {
      get { return m_PenColor; }
      set
      {
        m_PenColor = value;
        m_Pen.Color = value;
      }
    }

    /// <summary>
    /// Gets or sets the control pens dash style.
    /// </summary>
    [Description("PenDashStyle")]
    [Category("Appearance")]
    [SettingsBindable(true), Bindable(true)]
    [SettingsSerializeAs(SettingsSerializeAs.String)]
    public DashStyle PenDashStyle
    {
      get { return m_PenDashStyle; }
      set
      {
        m_PenDashStyle = value;
        m_Pen.DashStyle = m_PenDashStyle;
      }
    }

    /// <summary>
    /// Gets or sets the control pens line size.
    /// </summary>
    [Description("PenSize")]
    [Category("Appearance")]
    [SettingsBindable(true), Bindable(true)]
    [SettingsSerializeAs(SettingsSerializeAs.String)]
    public Single PenSize
    {
      get { return m_PenSize; }
      set
      {
        m_PenSize = value;
        m_Pen.Width = m_PenSize;
      }
    }

    /// <summary>
    /// Constructor. Initializes members.
    /// </summary>
    public PenStyleArea()
    {
      //Only Required for Designer will be overwritten in Initialize Component
      //with Application Settings value.
      this.Pen = Pens.Red;
      InitializeComponent();
    }

    /// <summary>
    /// Paint event handler. Redraws area with current Pen.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void PenStyleArea_Paint(object sender, PaintEventArgs e)
    {
      if (this.Enabled)
      {
        e.Graphics.DrawRectangle(SystemPens.ActiveBorder, 0, 0, this.Width - 1, this.Height - 1);
        e.Graphics.DrawLine(m_Pen, 5, this.Height / 2, this.Width - 5, this.Height / 2);
      }
      else
      {
        e.Graphics.DrawRectangle(SystemPens.InactiveBorder, 0, 0, this.Width - 1, this.Height - 1);
        e.Graphics.DrawLine(SystemPens.InactiveBorder, 5, this.Height / 2, this.Width - 5, this.Height / 2);
      }
    }

    /// <summary>
    /// The protected OnPenStyleChanged method raises the progress event by invoking 
    /// the delegates
    /// </summary>
    /// <param name="e">A <see cref="PenChangedEventArgs"/> with the event arguments</param>
    protected virtual void OnPenStyleChanged(PenChangedEventArgs e)
    {
      if (PenStyleChanged != null)
      {
        // Invokes the delegates. 
        PenStyleChanged(this, e);
      }
    }
  }
}
