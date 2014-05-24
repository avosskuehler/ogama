using System;
using System.Drawing;
using System.Windows.Forms;

namespace OgamaControls.Dialogs
{
  using VectorGraphics.Tools.CustomEventArgs;

  /// <summary>
  /// Dialog for selecting a cursor shape style.
  /// </summary>
  [ToolboxBitmap(typeof(BrushStyleDlg))]
  public partial class BrushStyleDlg : Form
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
    /// Event handler. Raised, when dialogs shape parameters has changed.
    /// </summary>
    public event EventHandler<BrushChangedEventArgs> BrushStyleChanged;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Sets the dialogs cursor property and that of the underlying control.
    /// </summary>
    public Brush Brush
    {
      get { return brushSelectControl.Brush; }
      set { brushSelectControl.Brush = value; }
    }

    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Constructor. Initializes components and wires cursor changed event.
    /// </summary>
    public BrushStyleDlg()
    {
      InitializeComponent();
      this.brushSelectControl.BrushStyleChanged += 
        new EventHandler<BrushChangedEventArgs>(brushSelectControl_BrushStyleChanged);
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
    #endregion //WINDOWSEVENTHANDLER

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler for Custom Defined Events                                    //
    ///////////////////////////////////////////////////////////////////////////////
    #region CUSTOMEVENTHANDLER

    /// <summary>
    /// Raises the <see cref="BrushStyleChanged"/> event by invoking the delegates.
    /// </summary>
    /// <param name="e"><see cref="BrushChangedEventArgs"/> event arguments</param>.
    public void OnBrushStyleChanged(BrushChangedEventArgs e)
    {
      if (this.BrushStyleChanged != null)
      {
        this.BrushStyleChanged(this, e);
      }
    }

    /// <summary>
    /// Wires the event from the underlying control to the listeners.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">A <see cref="ShapeEventArgs"/> with the new cursor.</param>
    void brushSelectControl_BrushStyleChanged(object sender, BrushChangedEventArgs e)
    {
      OnBrushStyleChanged(e);
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