using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using VectorGraphics;
using VectorGraphics.Elements;

namespace OgamaControls
{
  using VectorGraphics.Tools.CustomEventArgs;

  /// <summary>
  /// This user control is intended to show a selection dialog for cursor.
  /// Its <see cref="CursorStyleChanged"/> event notifys listeners.
  /// </summary>
  [ToolboxBitmap(typeof(resfinder), "OgamaControls.CommonControls.Styles.CursorSelectControl.bmp")]
  public partial class CursorSelectControl : UserControl
  {
    ///////////////////////////////////////////////////////////////////////////////
    // Defining Constants                                                        //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTANTS
    /// <summary>
    /// The default cursor size, used for first initialization.
    /// </summary>
    private const int DEFAULT_CURSOR_SIZE = 80;

    /// <summary>
    /// The default cursor pen, used for first initialization.
    /// </summary>
    private static Pen DEFAULT_CURSOR_PEN = Pens.Aqua;

    /// <summary>
    /// The default cursor style, used for first initialization.
    /// Is of type <see cref="VGCursor.DrawingCursors"/>
    /// </summary>
    private static VGCursor.DrawingCursors DEFAULT_CURSOR_STYLE = VGCursor.DrawingCursors.Circle;

    #endregion //CONSTANTS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Variables, Enumerations, Events                                  //
    ///////////////////////////////////////////////////////////////////////////////
    #region FIELDS

    /// <summary>
    /// Stores the current active <see cref="VGCursor"/>
    /// </summary>
    private VGCursor _cursor;

    /// <summary>
    /// The event that is raised when the <see cref="DrawingCursor"/> style has changed.
    /// </summary>
    public event CursorStyleChangedEventHandler CursorStyleChanged;

    /// <summary>
    /// Delegate for CursorStyleChanged event.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">A <see cref="ShapeEventArgs"/> with the new cursor shape.</param>
    public delegate void CursorStyleChangedEventHandler(object sender, ShapeEventArgs e);

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Sets current cursor of the control and updates property fields.
    /// </summary>
    public VGCursor DrawingCursor
    {
      set
      {
        _cursor = (VGCursor)value.Clone();
        _cursor.Center = new PointF(pnlPreview.Width/2, pnlPreview.Height/2);
        cbbCursorType.Text = _cursor.CursorType.ToString();
        nudCursorSize.Value = Convert.ToDecimal(_cursor.Width);
        if (_cursor.Width != Cursors.Default.Size.Width)
        {
          rdbCustomSize.Checked = true;
          nudCursorSize.Visible = true;
        }
        else
        {
          rdbDefaultSize.Checked = true;
          nudCursorSize.Visible = false;
        }
      }
    }

    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Constructor. Initializes components.
    /// </summary>
    public CursorSelectControl()
    {
      InitializeComponent();
    }

    /// <summary>
    /// The controls <see cref="UserControl.Load"/> event handler.
    /// Creates a predefined cursor.
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void CursorSelectControl_Load(object sender, EventArgs e)
    {
      if (_cursor == null)
      {
        _cursor = new VGCursor(DEFAULT_CURSOR_PEN,
          DEFAULT_CURSOR_STYLE, DEFAULT_CURSOR_SIZE,VGStyleGroup.None);
        _cursor.Center = new PointF(pnlPreview.Width / 2, pnlPreview.Height / 2);
      }
      this.cbbCursorType.Items.AddRange(Enum.GetNames(typeof(VGCursor.DrawingCursors)));
      this.cbbCursorType.SelectedItem = _cursor.CursorType;
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
    /// The <see cref="Control.Paint"/> event handler.
    /// Redraws the current cursor.
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">The <see cref="PaintEventArgs"/> with the graphics object.</param>
    private void pnlPreview_Paint(object sender, PaintEventArgs e)
    {
      if (_cursor != null)
      {
        _cursor.Draw(e.Graphics);
      }
    }

    /// <summary>
    /// The <see cref="NumericUpDown.ValueChanged"/> event handler for
    /// the <see cref="NumericUpDown"/> <see cref="nudCursorSize"/>.
    /// Updates <see cref="_cursor"/> properties and raises <see cref="CursorStyleChanged"/> event.
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void nudCursorSize_ValueChanged(object sender, EventArgs e)
    {
      _cursor.Size = new SizeF((float)nudCursorSize.Value, (float)nudCursorSize.Value);
      _cursor.Center = new PointF(pnlPreview.Width / 2, pnlPreview.Height / 2);
      pnlPreview.Invalidate();
      OnCursorStyleChanged(new ShapeEventArgs((VGElement)_cursor.Clone()));
    }

    /// <summary>
    /// The <see cref="ComboBox.SelectionChangeCommitted"/> event handler for
    /// the <see cref="ComboBox"/> <see cref="cbbCursorType"/>.
    /// Updates <see cref="_cursor"/> properties and raises <see cref="CursorStyleChanged"/> event.
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void cbbCursorType_SelectionChangeCommitted(object sender, EventArgs e)
    {
      VGCursor.DrawingCursors cursorType=(VGCursor.DrawingCursors)Enum.Parse(
        typeof(VGCursor.DrawingCursors),cbbCursorType.SelectedItem.ToString());
      _cursor = new VGCursor(_cursor.Pen, cursorType, _cursor.Width,_cursor.StyleGroup);
      CheckForCorrectSize();
      _cursor.Center = new PointF(pnlPreview.Width / 2, pnlPreview.Height / 2);
      pnlPreview.Invalidate();
      OnCursorStyleChanged(new ShapeEventArgs((VGElement)_cursor.Clone()));
    }

    private void rdbDefaultSize_CheckedChanged(object sender, EventArgs e)
    {
      CheckForCorrectSize();
    }

    private void CheckForCorrectSize()
    {
      nudCursorSize.Visible = rdbCustomSize.Checked;
      if (rdbDefaultSize.Checked)
      {
        switch (_cursor.CursorType)
        {
          case VGCursor.DrawingCursors.Arrow:
            nudCursorSize.Value = Cursors.Arrow.Size.Width;
            break;
          case VGCursor.DrawingCursors.Cross:
            nudCursorSize.Value = Cursors.Cross.Size.Width;
            break;
          case VGCursor.DrawingCursors.Hand:
            nudCursorSize.Value = Cursors.Hand.Size.Width;
            break;
          case VGCursor.DrawingCursors.Help:
            nudCursorSize.Value = Cursors.Help.Size.Width;
            break;
          case VGCursor.DrawingCursors.SizeAll:
            nudCursorSize.Value = Cursors.SizeAll.Size.Width;
            break;
          case VGCursor.DrawingCursors.UpArrow:
            nudCursorSize.Value = Cursors.UpArrow.Size.Width;
            break;
          case VGCursor.DrawingCursors.WaitCursor:
            nudCursorSize.Value = Cursors.WaitCursor.Size.Width;
            break;
          case VGCursor.DrawingCursors.Circle:
          case VGCursor.DrawingCursors.Sharp:
          case VGCursor.DrawingCursors.Square:
            nudCursorSize.Value = DEFAULT_CURSOR_SIZE;
            break;
        }
      }
      pnlPreview.Invalidate();
    }

    private void rdbCustomSize_CheckedChanged(object sender, EventArgs e)
    {
      CheckForCorrectSize();
    }

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
