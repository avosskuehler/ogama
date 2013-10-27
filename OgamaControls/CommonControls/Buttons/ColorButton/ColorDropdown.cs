using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;

namespace OgamaControls
{
  /// <summary>
  /// Custom <see cref="ComboBox"/> for <see cref="Color"/> selection.
  /// Shows a <see cref="ColorPanel"/> on click.
  /// </summary>
  [ToolboxBitmap(typeof(resfinder), "OgamaControls.CommonControls.Buttons.ColorButton.ColorDropdown.bmp")]
  public partial class ColorDropdown : ComboBox, IColorControl
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
    /// Dropdowns color member.
    /// </summary>
    private Color _color = Color.Transparent;


    /// <summary>
    /// Event. Raised when color has changed.
    /// </summary>
    public event EventHandler ColorChanged;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets or sets the dropdowns current color
    /// </summary>
    /// <value>The <see cref="Color"/> value of the dropdown</value>
    public Color CurrentColor
    {
      get
      {
        return _color;
      }
      set
      {
        _color = value;
        if (value.A < 255)
        {
          // The Dropdown control does not support transparent back colors
          Color opaqueBackColor = Color.FromArgb(255, value);
          this.BackColor = opaqueBackColor;
        }
        else
        {
          this.BackColor = value;
        }
      }
    }

    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION
    /// <summary>
    /// Constructor.
    /// </summary>
    public ColorDropdown()
    {
      InitializeComponent();
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="container">owning container</param>
    public ColorDropdown(IContainer container)
    {
      container.Add(this);
      InitializeComponent();
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
    /// On mouse down event handler. Shows color selection panel of type <see cref="ColorPanel"/>.
    /// </summary>
    /// <param name="e">A <see cref="MouseEventArgs"/> that contains the event data.</param>
    protected override void OnMouseDown(MouseEventArgs e)
    {
      //Instatiate Color panel
      ColorPanel clrPanel = new ColorPanel(base.Parent.PointToScreen(new Point(base.Left, base.Bottom)),
        this, _color);
      //Show dialog
      if (clrPanel.ShowDialog() == DialogResult.OK)
      {
        //Set property.
        this.CurrentColor = this._color;
        //Raise event.
        OnColorChanged(EventArgs.Empty);
      }
    }

    #endregion //WINDOWSEVENTHANDLER

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler for Custom Defined Events                                    //
    ///////////////////////////////////////////////////////////////////////////////
    #region CUSTOMEVENTHANDLER
    /// <summary>
    /// On color changed event handler. Raises delegate.
    /// </summary>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
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
