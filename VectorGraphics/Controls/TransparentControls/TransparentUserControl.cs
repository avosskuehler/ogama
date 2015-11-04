using System.Windows.Forms;

namespace VectorGraphics.Controls
{
  /// <summary>
  /// A <see cref="UserControl"/> which has a real transparent background.
  /// It works for all windows forms components, but not for ActiveX containers in the background.
  /// </summary>
  public partial class TransparentUserControl : UserControl
	{
    /// <summary>
    /// Constructor.
    /// </summary>
		public TransparentUserControl()
		{
      //SetStyle(ControlStyles.SupportsTransparentBackColor | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
      //this.UpdateStyles();
      //this.BackColor = Color.Transparent;
      this.DoubleBuffered = true;
			InitializeComponent();
		}
		/// <summary>
		/// OnPaintBackground
		/// </summary>
		/// <param name="pevent"></param>
    protected override void OnPaintBackground(PaintEventArgs pevent)
    {
      //do not allow the background to be painted  
    } 

    /// <summary>
    /// Overridden. Adds the WS_TRANSPARENT style.
    /// </summary>
		protected override CreateParams CreateParams
		{
			get
			{
				CreateParams cp = base.CreateParams;
				cp.ExStyle |= 0x00000020; //WS_TRANSPARENT
				return cp;
			}
		}
	}
}
