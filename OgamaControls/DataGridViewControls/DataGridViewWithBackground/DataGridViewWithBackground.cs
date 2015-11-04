using System.Drawing;
using System.Windows.Forms;

namespace OgamaControls
{
  /// <summary>
  /// This class inherits <see cref="DataGridView"/> and extends it
  /// with the possibility to show an <see cref="Image"/> as background
  /// behind the cells and on the surface.
  /// </summary>
  public partial class DataGridViewWithBackground : DataGridView
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
    /// Saves the background image for the data grid view.
    /// </summary>
    private Image backgroundImage;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION
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
    /// Overriden. To enable public setting of a background image property
    /// which itself is inherited from <see cref="Control"/> but not
    /// realized in default implementation.
    /// </summary>
    public override Image BackgroundImage
    {
      set { this.backgroundImage = value; }
    }

    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Public methods                                                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region PUBLICMETHODS

    /// <summary>
    /// Initializes a new instance of the DataGridViewWithBackground class.
    /// Triggers double buffering and redraw on resize.
    /// </summary>
    public DataGridViewWithBackground()
    {
      this.ResizeRedraw = true;
      this.DoubleBuffered = true;
    }

    #endregion //PUBLICMETHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Inherited methods                                                         //
    ///////////////////////////////////////////////////////////////////////////////
    #region OVERRIDES

    /// <summary>
    /// Overridden <see cref="DataGridView.PaintBackground(Graphics, Rectangle, Rectangle)"/>.
    /// Paints the background of the DataGridView with the background image.
    /// </summary>
    /// <param name="graphics">The <see cref="Graphics"/> used to paint the background.</param>
    /// <param name="clipBounds">A <see cref="Rectangle"/> that represents the area of the <see cref="DataGridView"/> that needs to be painted.</param>
    /// <param name="gridBounds">A <see cref="Rectangle"/> that represents the area in which cells are drawn.</param>
    protected override void PaintBackground(Graphics graphics, Rectangle clipBounds, Rectangle gridBounds)
    {
      base.PaintBackground(graphics, clipBounds, gridBounds);
      graphics.DrawImage(this.backgroundImage, gridBounds);
    }

    /// <summary>
    /// Overridden <see cref="DataGridView.OnRowPrePaint(DataGridViewRowPrePaintEventArgs)"/>.
    /// Is called before the row is painted.
    /// </summary>
    /// <param name="e">A <see cref="DataGridViewRowPrePaintEventArgs"/> with the event data.</param>
    protected override void OnRowPrePaint(DataGridViewRowPrePaintEventArgs e)
    {
      //e.PaintHeader(true);
      //e.PaintParts = DataGridViewPaintParts.ContentForeground | DataGridViewPaintParts.SelectionBackground | DataGridViewPaintParts.Focus
      //| DataGridViewPaintParts.ErrorIcon | DataGridViewPaintParts.Border | DataGridViewPaintParts.ContentBackground;
      base.OnRowPrePaint(e);
    }

    /// <summary>
    /// Overridden <see cref="DataGridView.OnCellPainting(DataGridViewCellPaintingEventArgs)"/>.
    /// Occurs when a cell needs to be drawn. 
    /// Performs custom background drawing for data cells.
    /// </summary>
    /// <param name="e">A <see cref="DataGridViewCellPaintingEventArgs"/> with the event data.</param>
    protected override void OnCellPainting(DataGridViewCellPaintingEventArgs e)
    {
      if (e.RowIndex >= 0 && e.ColumnIndex >= 0)  // data cell
      {
        this.DrawCellBackColor(e);
      }
      else
      {
        base.OnCellPainting(e);
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

    /// <summary>
    /// This method draws data cells with semitransparent background.
    /// Selected cells are drawn by default.
    /// </summary>
    /// <param name="e">A <see cref="DataGridViewCellPaintingEventArgs"/> with the event data.</param>
    private void DrawCellBackColor(DataGridViewCellPaintingEventArgs e)
    {
      if ((e.State & DataGridViewElementStates.Selected) == DataGridViewElementStates.Selected)
      {
        base.OnCellPainting(e);
        return;
      }

      using (SolidBrush fillBrush = new SolidBrush(Color.FromArgb(200,Color.White)))
      {
        using (Pen gridPenColor = new Pen(this.GridColor))
        {
          Rectangle rect1 = new Rectangle(e.CellBounds.Location, e.CellBounds.Size);
          Rectangle rect2 = new Rectangle(e.CellBounds.Location, e.CellBounds.Size);

          rect1.X -= 1;
          rect1.Y -= 1;

          rect2.Width -= 1;
          rect2.Height -= 1;

          // must draw border for grid scrolling horizontally 
          e.Graphics.DrawRectangle(gridPenColor, rect1);

          e.Graphics.FillRectangle(fillBrush, rect2);
        }
      }

      e.PaintContent(e.CellBounds);  // output cell text
      e.Handled = true;
    }

  }
}
