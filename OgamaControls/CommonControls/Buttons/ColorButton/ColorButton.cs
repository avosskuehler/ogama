using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace OgamaControls
{
  /// <summary>
  /// Color button control, shows a <see cref="ColorPanel"/> for <see cref="Color"/> selection on click.
  /// </summary>
  [ToolboxBitmap(typeof(resfinder), "OgamaControls.CommonControls.Buttons.ColorButton.ColorButton.bmp")]
  public partial class ColorButton : Button, IColorControl
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
    /// Buttons color member.
    /// </summary>
    private Color _color = Color.Transparent;

    /// <summary>
    /// Description of <see cref="ColorPanel"/>s automatic button.
    /// </summary>
    private string autoButtonString="Automatic";

    /// <summary>
    /// Event. Raised, when buttons color has changed
    /// </summary>
    public event EventHandler ColorChanged;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES
    /// <summary>
    /// Gets or sets current buttons color
    /// </summary>
    /// <value>The <see cref="Color"/> value of the button.</value>
    [Category("Appearance")]
    [Description("The color that is defined within this control")]
    public Color CurrentColor
    {
      get
      {
        return _color;
      }
      set
      {
        _color = value;
      }
    }

    /// <summary>
    /// Description of <see cref="ColorPanel"/>s automatic button.
    /// </summary>
    /// <value>The <see cref="string"/> decription of the button.</value>
    [Category("Appearance")]
    [Description("The decription at the top of the color select control that appears when clicking on the button.")]
    public string AutoButtonString
    {
      get { return this.autoButtonString; }
      set { this.autoButtonString = value; }
    }

    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION
    /// <summary>
    /// Constructor.
    /// </summary>
    public ColorButton()
    {
      InitializeComponent();
      this.Text = "";
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="container">owning container</param>
    public ColorButton(IContainer container)
    {
      container.Add(this);
      InitializeComponent();
      this.Text = "";
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler                                                              //
    ///////////////////////////////////////////////////////////////////////////////
    #region EVENTS

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler for UI, Menu, Buttons, Toolbars etc.                         //
    ///////////////////////////////////////////////////////////////////////////////
    #region WINDOWSEVENTHANDLER

    /// <summary>
    /// OnClick event handler. Shows color selection panel of type <see cref="ColorPanel"/>.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void ColorButton_Click(object sender, EventArgs e)
    {
      ColorPanel clrPanel = new ColorPanel(base.Parent.PointToScreen(new Point(base.Left, base.Bottom)),
  this, _color);
      clrPanel.Automatic = this.autoButtonString;
      clrPanel.Show();
    }

    /// <summary>
    /// OnPaint event handler. Draws a filled rectangle with the buttons color
    /// inside the button area and a small triangle to show the 
    /// colorpanel dropdown property to the user
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A <see cref="PaintEventArgs"/> that contains the event data.</param>
    private void ColorButton_Paint(object sender, PaintEventArgs e)
    {
      Rectangle rc = new Rectangle((e.ClipRectangle.Left + 5), (e.ClipRectangle.Top + 5),
        e.ClipRectangle.Width - 0x1c, e.ClipRectangle.Height - 11);
      Pen darkPen = new Pen(SystemColors.ControlDark);
      if (base.Enabled)
      {
        e.Graphics.FillRectangle(new SolidBrush(this._color), rc);
        e.Graphics.DrawRectangle(darkPen, rc);
      }
      Pen textPen = new Pen(base.Enabled ? SystemColors.ControlText : SystemColors.GrayText);
      Point pt = new Point(rc.Right, e.ClipRectangle.Height / 2);
      e.Graphics.DrawLine(textPen, (int)(pt.X + 9), (int)(pt.Y - 1), (int)(pt.X + 13), (int)(pt.Y - 1));
      e.Graphics.DrawLine(textPen, pt.X + 10, pt.Y, pt.X + 12, pt.Y);
      e.Graphics.DrawLine(textPen, pt.X + 11, pt.Y, pt.X + 11, pt.Y + 1);
    }

    #endregion //WINDOWSEVENTHANDLER

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler for Custom Defined Events                                    //
    ///////////////////////////////////////////////////////////////////////////////
    #region CUSTOMEVENTHANDLER
    /// <summary>
    /// Raised when buttons color has changed
    /// </summary>
    /// <param name="e">Empty <see cref="EventArgs"/></param>.
    public virtual void OnColorChanged(EventArgs e)
    {
      if (this.ColorChanged != null)
      {
        this.ColorChanged(this, e);
      }
    }

    #endregion //CUSTOMEVENTHANDLER

    #endregion //EVENTS

    ///////////////////////////////////////////////////////////////////////////////
    // Methods and Eventhandling for Background tasks                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region BACKGROUNDWORKER
    #endregion //BACKGROUNDWORKER

    ///////////////////////////////////////////////////////////////////////////////
    // Inherited methods                                                         //
    ///////////////////////////////////////////////////////////////////////////////
    #region OVERRIDES
    #endregion //OVERRIDES

    ///////////////////////////////////////////////////////////////////////////////
    // Methods for doing main class job                                          //
    ///////////////////////////////////////////////////////////////////////////////
    #region METHODS
    #endregion //METHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER
    #endregion //HELPER
  }
}
