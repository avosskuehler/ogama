using System.Drawing;
using System.Windows.Forms;

namespace OgamaControls
{
  /// <summary>
  /// Double buffered panel.
  /// </summary>
  [ToolboxBitmap(typeof(Panel))]
  public partial class DoubleBufferPanel : Panel
  {
    /// <summary>
    /// Constructor.
    /// </summary>
    public DoubleBufferPanel()
    {
      this.SetStyle(ControlStyles.AllPaintingInWmPaint | 
        ControlStyles.UserPaint | 
        ControlStyles.DoubleBuffer, 
        true);
      this.UpdateStyles();
      InitializeComponent();
    }

    /// <summary>
    /// support for thread-safe version
    /// </summary>
    /// <returns></returns>
    private delegate Point GetThreadSafePointInClientCoordinatesDelegate(Point point);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="point"></param>
    /// <returns></returns>
    public Point GetThreadSafePointInClientCoordinates(Point point)
    {
      if (this.InvokeRequired)
      {
        GetThreadSafePointInClientCoordinatesDelegate startDelegate = new GetThreadSafePointInClientCoordinatesDelegate(this.GetThreadSafePointInClientCoordinates);
        return (Point)this.Invoke(startDelegate,point);
      }
      return this.PointToClient(point);
    }

    /// <summary>
    /// support for thread-safe version
    /// </summary>
    /// <returns></returns>
    private delegate Point GetThreadSafePointInScreenCoordinatesDelegate(Point point);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="point"></param>
    /// <returns></returns>
    public Point GetThreadSafePointInScreenCoordinates(Point point)
    {
      if (this.InvokeRequired)
      {
        GetThreadSafePointInScreenCoordinatesDelegate startDelegate = new GetThreadSafePointInScreenCoordinatesDelegate(this.GetThreadSafePointInScreenCoordinates);
        return (Point)this.Invoke(startDelegate, point);
      }
      return this.PointToScreen(point);
    }

    /// <summary>
    /// support for thread-safe version
    /// </summary>
    /// <returns></returns>
    private delegate bool ThreadSafeFocusDelegate();

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public bool ThreadSafeFocus()
    {
      if (this.InvokeRequired)
      {
        ThreadSafeFocusDelegate startDelegate = new ThreadSafeFocusDelegate(this.ThreadSafeFocus);
        return (bool)this.Invoke(startDelegate);
      }
      return this.Focus();
    }

    ///// <summary>
    ///// Overriden. Does nothing
    ///// </summary>
    ///// <param name="e"></param>
    //protected override void OnPaintBackground(PaintEventArgs e)
    //{
    //  //
    //}
  }
}
