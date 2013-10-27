using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Configuration;

using VectorGraphics.Elements;

namespace OgamaControls
{
  using VectorGraphics.Tools.CustomEventArgs;

  /// <summary>
  /// Area to show font styles
  /// </summary>
  [ToolboxBitmap(typeof(resfinder), "OgamaControls.CommonControls.Styles.FontStyleArea.bmp")]
  public partial class FontStyleArea : UserControl
  {
    private Color _fontColor;
    private VGAlignment fontAlignment;
    private string sampleString;

    /// <summary>
    /// Event handler. Raised, when dialogs parameters has changed.
    /// </summary>
    public event EventHandler<FontChangedEventArgs> FontStyleChanged;


    /// <summary>
    /// Sets the Controls font color.
    /// </summary>
    [Description("Font Color")]
    [Category("Appearance")]
    [SettingsBindable(true), Bindable(true)]
    [SettingsSerializeAs(SettingsSerializeAs.String)]
    public Color FontColor
    {
      get { return _fontColor; }
      set
      {
        _fontColor = value;
        OnFontStyleChanged(new FontChangedEventArgs(Font,_fontColor, this.FontAlignment, VGStyleGroup.None));
        Invalidate();
      }
    }

    /// <summary>
    /// Sets the font text alignment
    /// </summary>
    [Description("Font Alignment")]
    [Category("Appearance")]
    [SettingsBindable(true), Bindable(true)]
    [SettingsSerializeAs(SettingsSerializeAs.String)]
    public VGAlignment FontAlignment
    {
      get { return this.fontAlignment; }
      set
      {
        this.fontAlignment = value;
        OnFontStyleChanged(new FontChangedEventArgs(Font, _fontColor, this.fontAlignment, VGStyleGroup.None));
        Invalidate();
      }
    }

    /// <summary>
    /// Sets the Controls sample string
    /// </summary>
    [Description("Sample string used in the style area.")]
    [Category("Appearance")]
    [SettingsBindable(true), Bindable(true)]
    [SettingsSerializeAs(SettingsSerializeAs.String)]
    public string SampleString
    {
      get { return sampleString; }
      set { sampleString = value; }
    }

    /// <summary>
    /// Constructor. Initializes components and sets standard font values.
    /// </summary>
    public FontStyleArea()
    {
      //Only Required for Designer will be overwritten in Initialize Component
      //with Application Settings value.
      this.Font = SystemInformation.MenuFont;
      this.FontColor = Color.Black;
      this.FontAlignment = VGAlignment.Center;
      sampleString = "Sample ...";
      InitializeComponent();
    }

    /// <summary>
    /// Paint event handler. Draws sample text with current font.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void FontStyleArea_Paint(object sender, PaintEventArgs e)
    {
      SizeF sizeText = e.Graphics.MeasureString(this.sampleString, this.Font);
      PointF textTopLeft = new PointF();
      StringFormat sf = new StringFormat();

      PointF center = new PointF(this.Location.X + this.Width / 2, this.Location.Y + this.Height / 2);
      RectangleF bounds = this.Bounds;
      bounds.Inflate(-bounds.Width / 2 + 20, -bounds.Height / 2 + 20);
      Pen dotted = new Pen(Color.Gray, 1f);
      dotted.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
      switch (this.fontAlignment)
      {
        case VGAlignment.Bottom:
          textTopLeft.X = center.X - sizeText.Width / 2;
          textTopLeft.Y = bounds.Bottom;
          e.Graphics.DrawRectangle(dotted, bounds.X, bounds.Y, bounds.Width, bounds.Height);
          e.Graphics.DrawString(this.sampleString, this.Font, new SolidBrush(this.FontColor), textTopLeft, sf);
          break;
        case VGAlignment.None:
        case VGAlignment.Center:
          textTopLeft.X = center.X - sizeText.Width / 2;
          textTopLeft.Y = center.Y - sizeText.Height / 2;
          e.Graphics.DrawString(this.sampleString, this.Font, new SolidBrush(this.FontColor), textTopLeft, sf);
          break;
        case VGAlignment.Left:
          textTopLeft.X = bounds.Left;
          textTopLeft.Y = center.Y + sizeText.Width / 2;
          sf.FormatFlags = StringFormatFlags.DirectionVertical;

          // Save graphics state.
          e.Graphics.DrawRectangle(dotted, bounds.X, bounds.Y, bounds.Width, bounds.Height);
          e.Graphics.RotateTransform(180);
          e.Graphics.DrawString(this.sampleString, this.Font, new SolidBrush(this.FontColor),
            -textTopLeft.X, -textTopLeft.Y, sf);
          e.Graphics.RotateTransform(-180);
          break;
        case VGAlignment.Right:
          textTopLeft.X = bounds.Right;
          textTopLeft.Y = center.Y - sizeText.Width / 2;
          sf.FormatFlags = StringFormatFlags.DirectionVertical;
          e.Graphics.DrawRectangle(dotted, bounds.X, bounds.Y, bounds.Width, bounds.Height);
          e.Graphics.DrawString(this.sampleString, this.Font, new SolidBrush(this.FontColor), textTopLeft, sf);
          break;
        case VGAlignment.Top:
          textTopLeft.X = center.X - sizeText.Width / 2;
          textTopLeft.Y = bounds.Top - sizeText.Height;
          e.Graphics.DrawRectangle(dotted, bounds.X, bounds.Y, bounds.Width, bounds.Height);
          e.Graphics.DrawString(this.sampleString, this.Font, new SolidBrush(this.FontColor), textTopLeft, sf);
          break;
      }
    }

    /// <summary>
    /// The protected OnFontStyleChanged method raises the progress event by invoking 
    /// the delegates
    /// </summary>
    /// <param name="e">FontChanged event arguments</param>
    protected virtual void OnFontStyleChanged(FontChangedEventArgs e)
    {
      if (FontStyleChanged != null)
      {
        // Invokes the delegates. 
        FontStyleChanged(this, e);
      }
    }

    /// <summary>
    /// Eventhandler. Updates the line sample with the new pen.
    /// </summary>
    /// <param name="sender">message sender</param>
    /// <param name="e">Pen change event arguments that hold new pen</param>
    public void ThisFontChanged(object sender, FontChangedEventArgs e)
    {
      this.Font = e.Font;
      this.FontColor = e.FontColor;
      this.FontAlignment = e.FontAlignment;
      OnFontStyleChanged(e);
      Invalidate();
    }
  }
}
