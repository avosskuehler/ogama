using System.Drawing;
using System.Windows.Forms;

namespace OgamaControls
{
  /// <summary>
  /// Double buffered panel which renders the <see cref="BufferedGraphics"/>
  /// given.
  /// </summary>
  [ToolboxBitmap(typeof(Panel))]
  public partial class BufferedGraphicsRenderPanel : Panel
  {
    ///////////////////////////////////////////////////////////////////////////////
    // Defining Constants                                                        //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTANTS
    #endregion //CONSTANTS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Variables, Enumerations, Events                                  //
    ///////////////////////////////////////////////////////////////////////////////
    #region FIELDS

    /// <summary>
    /// The <see cref="BufferedGraphics"/> that makes up the 
    /// graphics buffer that should be drawn onto this
    /// <see cref="Panel"/>s surface.
    /// </summary>
    private BufferedGraphics drawingSurface;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the BufferedGraphicsRenderPanel class.
    /// Setting the <see cref="ControlStyles.AllPaintingInWmPaint"/>,
    /// <see cref="ControlStyles.UserPaint"/> and <see cref="ControlStyles.DoubleBuffer"/> styles.
    /// </summary>
    public BufferedGraphicsRenderPanel()
    {
      this.SetStyle(ControlStyles.AllPaintingInWmPaint |
        ControlStyles.UserPaint |
        ControlStyles.DoubleBuffer,
        true);
      this.UpdateStyles();
      InitializeComponent();
    }


    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Enumerations                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region ENUMS
    #endregion ENUMS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Sets the <see cref="BufferedGraphics"/> that makes up the 
    /// graphics buffer that should be drawn onto this
    /// <see cref="Panel"/>s surface.
    /// </summary>
    public BufferedGraphics DrawingSurface
    {
      set { this.drawingSurface = value; }
    }

    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Public methods                                                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region PUBLICMETHODS
    #endregion //PUBLICMETHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Inherited methods                                                         //
    ///////////////////////////////////////////////////////////////////////////////
    #region OVERRIDES

    /// <summary>
    /// Overriden. Does nothing
    /// </summary>
    /// <param name="e">The <see cref="PaintEventArgs"/> with the event data.</param>
    protected override void OnPaintBackground(PaintEventArgs e)
    {
      // Do nothing
    }

    /// <summary>
    /// Overridden. Renders the <see cref="BufferedGraphics"/>
    /// into the <see cref="Graphics"/> of this control if there is any
    /// otherwise clears the surface with the controls BackColor.
    /// </summary>
    /// <param name="e">An <see cref="PaintEventArgs"/> with the event data.</param>
    protected override void OnPaint(PaintEventArgs e)
    {
      // base.OnPaint(e);
      if (this.drawingSurface != null && this.drawingSurface.Graphics != null)
      {
        this.drawingSurface.Render(e.Graphics);
      }
      else
      {
        e.Graphics.Clear(this.BackColor);
      }
    }

    #endregion //OVERRIDES

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler                                                              //
    ///////////////////////////////////////////////////////////////////////////////
    #region EVENTS

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler for UI, Menu, Buttons, Toolbars etc.                         //
    ///////////////////////////////////////////////////////////////////////////////
    #region WINDOWSEVENTHANDLER
    #endregion //WINDOWSEVENTHANDLER

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler for Custom Defined Events                                    //
    ///////////////////////////////////////////////////////////////////////////////
    #region CUSTOMEVENTHANDLER
    #endregion //CUSTOMEVENTHANDLER

    #endregion //EVENTS

    ///////////////////////////////////////////////////////////////////////////////
    // Methods and Eventhandling for Background tasks                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region BACKGROUNDWORKER
    #endregion //BACKGROUNDWORKER

    ///////////////////////////////////////////////////////////////////////////////
    // Methods for doing main class job                                          //
    ///////////////////////////////////////////////////////////////////////////////
    #region PRIVATEMETHODS
    #endregion //PRIVATEMETHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER
    #endregion //HELPER
  }
}
