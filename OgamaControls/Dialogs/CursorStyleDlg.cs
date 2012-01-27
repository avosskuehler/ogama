using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using VectorGraphics;
using VectorGraphics.Elements;

namespace OgamaControls.Dialogs
{
  using VectorGraphics.Tools.CustomEventArgs;

  /// <summary>
  /// Dialog for selecting a cursor shape style.
  /// </summary>
  [ToolboxBitmap(typeof(resfinder), "OgamaControls.Dialogs.CursorStyleDlg.bmp")]
  public partial class CursorStyleDlg : Form
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
    public event EventHandler<ShapeEventArgs> CursorStyleChanged;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Sets the dialogs cursor property and that of the underlying control.
    /// </summary>
    public VGCursor DrawingCursor
    {
      set { cursorSelectControl.DrawingCursor = value; }
    }

    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Constructor. Initializes components and wires cursor changed event.
    /// </summary>
    public CursorStyleDlg()
    {
      InitializeComponent();
      this.cursorSelectControl.CursorStyleChanged += new CursorSelectControl.CursorStyleChangedEventHandler(cursorSelectControl_CursorStyleChanged);
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
    /// Raises the <see cref="CursorStyleChanged"/> event by invoking the delegates.
    /// </summary>
    /// <param name="e"><see cref="ShapeEventArgs"/> event arguments</param>.
    public void OnCursorStyleChanged(ShapeEventArgs e)
    {
      if (this.CursorStyleChanged != null)
      {
        this.CursorStyleChanged(this, e);
      }
    }

    /// <summary>
    /// Wires the event from the underlying control to the listeners.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">A <see cref="ShapeEventArgs"/> with the new cursor.</param>
    private void cursorSelectControl_CursorStyleChanged(object sender, ShapeEventArgs e)
    {
      OnCursorStyleChanged(e);
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