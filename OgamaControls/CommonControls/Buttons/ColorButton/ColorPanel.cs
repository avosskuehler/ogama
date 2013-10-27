using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace OgamaControls
{
  /// <summary>
  /// Color selection panel adopted from paint application.
  /// Offers a lot of predefined colors and additional a custom color selection
  /// via the <see cref="ColorDialog"/>.
  /// </summary>
  [DesignTimeVisible(false)]
  public partial class ColorPanel : Form
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
    /// <see cref="string"/> at top of panel to name the automatic color selection
    /// </summary>
    private string autoButton = "Automatic";

    /// <summary>
    /// <see cref="string"/> at bottom of panel to name the possibility of choosing new colors.
    /// </summary>
    private string moreButton = "More Colors...";

    /// <summary>
    /// Parent color control (Button, Dropdown) that owns this panel
    /// </summary>
    private IColorControl _colorControl;

    /// <summary>
    /// Current active color
    /// </summary>
    private Color _currentColor;

    /// <summary>
    /// Color index of current color.
    /// </summary>
    private int colorIndex = -1;

    /// <summary>
    /// Some standard colors in the dropdown list.
    /// </summary>
    private Color[] colorList = new Color[] { 
      Color.FromArgb(0, 0, 0), Color.FromArgb(0x99, 0x33, 0), 
      Color.FromArgb(0x33, 0x33, 0), Color.FromArgb(0, 0x33, 0), 
      Color.FromArgb(0, 0x33, 0x66), Color.FromArgb(0, 0, 0x80), 
      Color.FromArgb(0x33, 0x33, 0x99), Color.FromArgb(0x33, 0x33, 0x33), 
      Color.FromArgb(0x80, 0, 0), Color.FromArgb(0xff, 0x66, 0), 
      Color.FromArgb(0x80, 0x80, 0), Color.FromArgb(0, 0x80, 0), 
      Color.FromArgb(0, 0x80, 0x80), Color.FromArgb(0, 0, 0xff), 
      Color.FromArgb(0x66, 0x66, 0x99), Color.FromArgb(0x80, 0x80, 0x80), 
      Color.FromArgb(0xff, 0, 0), Color.FromArgb(0xff, 0x99, 0), 
      Color.FromArgb(0x99, 0xcc, 0), Color.FromArgb(0x33, 0x99, 0x66), 
      Color.FromArgb(0x33, 0xcc, 0xcc), Color.FromArgb(0x33, 0x66, 0xff), 
      Color.FromArgb(0x80, 0, 0x80), Color.FromArgb(0x99, 0x99, 0x99), 
      Color.FromArgb(0xff, 0, 0xff), Color.FromArgb(0xff, 0xcc, 0), 
      Color.FromArgb(0xff, 0xff, 0), Color.FromArgb(0, 0xff, 0), 
      Color.FromArgb(0, 0xff, 0xff), Color.FromArgb(0, 0xcc, 0xff),
      Color.FromArgb(0x99, 0x33, 0x66), Color.FromArgb(0xc0, 0xc0, 0xc0), 
      Color.FromArgb(0xff, 0x99, 0xcc), Color.FromArgb(0xff, 0xcc, 0x99), 
      Color.FromArgb(0xff, 0xff, 0x99), Color.FromArgb(0xcc, 0xff, 0xcc), 
      Color.FromArgb(0xcc, 0xff, 0xff), Color.FromArgb(0x99, 0xcc, 0xff), 
      Color.FromArgb(0xcc, 0x99, 0xff), Color.FromArgb(0xff, 0xff, 0xff)};

    /// <summary>
    /// Index of keyboard.
    /// </summary>
    private int keyboardIndex = -50;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES
    /// <summary>
    /// Value of automatic string.
    /// </summary>
    /// <value>A <see cref="string"/> with the text in the automatic button section.</value>
    public string Automatic
    {
      get
      {
        return this.autoButton;
      }
      set
      {
        this.autoButton = value;
      }
    }

    /// <summary>
    /// string for more colors.
    /// </summary>
    /// <value>A <see cref="string"/> with the text for the more colors section.</value>
    public string MoreColors
    {
      get
      {
        return this.moreButton;
      }
      set
      {
        this.moreButton = value;
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
    /// <param name="pt">Position of the new panel.</param>
    /// <param name="clrControl">owning control</param>
    /// <param name="currentColor">current color to use</param>
    public ColorPanel(Point pt, IColorControl clrControl, Color currentColor)
    {
      InitializeComponent();
      _currentColor = currentColor;
      this._colorControl = clrControl;
      base.Width = 0x9c;
      base.Height = 100;
      if (this.autoButton != "")
      {
        base.Height += 0x17;
      }
      if (this.moreButton != "")
      {
        base.Height += 0x17;
      }
      base.CenterToScreen();
      base.Location = pt;
      base.Capture = true;
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
    /// ColorPanel click event handler. 
    /// Selects color from predefined colors if a color is marked.
    /// Opens standard windows <see cref="ColorDialog"/>, if requested.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void ColorPanel_Click(object sender, EventArgs e)
    {
      if (this.colorIndex >= 0)
      {
        if (this.colorIndex < 40)
        {
          this._colorControl.CurrentColor = this.colorList[this.colorIndex];
        }
        else if (this.colorIndex == 100)
        {
          this._colorControl.CurrentColor = Color.Transparent;
        }
        else
        {
          ColorDialog dlg = new ColorDialog();
          dlg.Color = _colorControl.CurrentColor;
          dlg.FullOpen = true;
          if (dlg.ShowDialog(this) != DialogResult.OK)
          {
            base.Close();
            return;
          }
          _colorControl.CurrentColor = dlg.Color;
        }
        this.DialogResult = DialogResult.OK;
        this._colorControl.OnColorChanged(EventArgs.Empty);
        base.Close();
      }
    }

    /// <summary>
    /// Key Down event handler. Moves color index in response to 
    /// arrow keys.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">KeyEventArgs</param>
    private void ColorPanel_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Escape)
      {
        base.Close();
      }
      else if (e.KeyCode == Keys.Left)
      {
        this.MoveIndex(-1);
      }
      else if (e.KeyCode == Keys.Up)
      {
        this.MoveIndex(-8);
      }
      else if (e.KeyCode == Keys.Down)
      {
        this.MoveIndex(8);
      }
      else if (e.KeyCode == Keys.Right)
      {
        this.MoveIndex(1);
      }
      else if ((e.KeyCode == Keys.Return) || (e.KeyCode == Keys.Space))
      {
        this.OnClick(EventArgs.Empty);
      }
    }

    /// <summary>
    /// Mouse down event handler. closes panel if cursor is out of client rectangle
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A <see cref="MouseEventArgs"/> that contains the event data.</param>
    private void ColorPanel_MouseDown(object sender, MouseEventArgs e)
    {
      if (!base.RectangleToScreen(base.ClientRectangle).Contains(Cursor.Position))
      {
        base.Close();
      }
    }
    
    /// <summary>
    /// Mouse move event handler. Selects color according to mouse position.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A <see cref="MouseEventArgs"/> that contains the event data.</param>
    private void ColorPanel_MouseMove(object sender, MouseEventArgs e)
    {
      if (base.RectangleToScreen(base.ClientRectangle).Contains(Cursor.Position))
      {
        Point pt = base.PointToClient(Cursor.Position);
        int x = 6;
        int y = 5;
        if (this.autoButton != "")
        {
          if (this.SetColorIndex(new Rectangle(x - 3, y - 3, 0x8f, 0x16), pt, 100))
          {
            return;
          }
          y += 0x17;
        }
        for (int i = 0; i < 40; i++)
        {
          if (this.SetColorIndex(new Rectangle(x - 3, y - 3, 0x11, 0x11), pt, i))
          {
            return;
          }
          if (((i + 1) % 8) == 0)
          {
            x = 6;
            y += 0x12;
          }
          else
          {
            x += 0x12;
          }
        }
        if ((this.moreButton != "") && this.SetColorIndex(new Rectangle(x - 3, y - 3, 0x8f, 0x16), pt, 0x65))
        {
          return;
        }
      }
      if (this.colorIndex != -1)
      {
        this.colorIndex = -1;
      }
    }

    /// <summary>
    /// Paint event handler. Paints the panel with predefined color rectangles
    /// an automatic color button and a custom color button.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A <see cref="PaintEventArgs"/> that contains the event data.</param>
    private void ColorPanel_Paint(object sender, PaintEventArgs e)
    {
      Pen darkPen = new Pen(SystemColors.ControlDark);
      Pen lightPen = new Pen(SystemColors.ControlLightLight);
      SolidBrush lightBrush = new SolidBrush(SystemColors.ControlLightLight);
      bool selected = false;
      int x = 6;
      int y = 5;
      if (this.autoButton != "")
      {
        selected = this._colorControl.CurrentColor == Color.Transparent;
        this.DrawButton(e, x, y, this.autoButton, 100, selected);
        y += 0x17;
      }
      for (int i = 0; i < 40; i++)
      {
        if (this._colorControl.CurrentColor.ToArgb() == this.colorList[i].ToArgb())
        {
          selected = true;
        }
        if (this.colorIndex == i)
        {
          e.Graphics.DrawRectangle(lightPen, x - 3, y - 3, 0x11, 0x11);
          e.Graphics.DrawLine(darkPen, (int)(x - 2), (int)(y + 14), (int)(x + 14), (int)(y + 14));
          e.Graphics.DrawLine(darkPen, (int)(x + 14), (int)(y - 2), (int)(x + 14), (int)(y + 14));
        }
        else if (this._colorControl.CurrentColor.ToArgb() == this.colorList[i].ToArgb())
        {
          if (this.keyboardIndex == -50)
          {
            this.keyboardIndex = i;
          }
          e.Graphics.FillRectangle(lightBrush, x - 3, y - 3, 0x12, 0x12);
          e.Graphics.DrawLine(darkPen, (int)(x - 3), (int)(y - 3), (int)(x + 13), (int)(y - 3));
          e.Graphics.DrawLine(darkPen, (int)(x - 3), (int)(y - 3), (int)(x - 3), (int)(y + 13));
        }
        e.Graphics.FillRectangle(new SolidBrush(this.colorList[i]), x, y, 11, 11);
        e.Graphics.DrawRectangle(darkPen, x, y, 11, 11);
        if (((i + 1) % 8) == 0)
        {
          x = 6;
          y += 0x12;
        }
        else
        {
          x += 0x12;
        }
      }
      if (this.moreButton != "")
      {
        this.DrawButton(e, x, y, this.moreButton, 0x65, !selected);
      }

    }
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
    // Inherited methods                                                         //
    ///////////////////////////////////////////////////////////////////////////////
    #region OVERRIDES
    #endregion //OVERRIDES

    ///////////////////////////////////////////////////////////////////////////////
    // Methods for doing main class job                                          //
    ///////////////////////////////////////////////////////////////////////////////
    #region METHODS
    /// <summary>
    /// Draw color button.
    /// </summary>
    /// <param name="e">A <see cref="PaintEventArgs"/> that contains the event data.</param>
    /// <param name="x">Position X</param>
    /// <param name="y">Position Y</param>
    /// <param name="text">text</param>
    /// <param name="index">index of color</param>
    /// <param name="selected">True if button is selected</param>
    protected void DrawButton(PaintEventArgs e, int x, int y, string text, int index, bool selected)
    {
      Pen darkPen = new Pen(SystemColors.ControlDark);
      Pen lightPen = new Pen(SystemColors.ControlLightLight);
      SolidBrush lightBrush = new SolidBrush(SystemColors.ControlLightLight);
      if (this.colorIndex == index)
      {
        e.Graphics.DrawRectangle(lightPen, x - 3, y - 3, 0x8f, 0x16);
        e.Graphics.DrawLine(darkPen, (int)(x - 2), (int)(y + 0x13), (int)(x + 140), (int)(y + 0x13));
        e.Graphics.DrawLine(darkPen, (int)(x + 140), (int)(y - 2), (int)(x + 140), (int)(y + 0x13));
      }
      else if (selected)
      {
        e.Graphics.FillRectangle(lightBrush, x - 3, y - 3, 0x90, 0x17);
        e.Graphics.DrawLine(darkPen, (int)(x - 3), (int)(y - 3), (int)(x + 0x8b), (int)(y - 3));
        e.Graphics.DrawLine(darkPen, (int)(x - 3), (int)(y - 3), (int)(x - 3), (int)(y + 0x12));
      }
      Rectangle rc = new Rectangle(x, y, 0x89, 0x10);
      SolidBrush textBrush = new SolidBrush(SystemColors.ControlText);
      StringFormat textFormat = new StringFormat();
      textFormat.Alignment = StringAlignment.Center;
      textFormat.LineAlignment = StringAlignment.Center;
      e.Graphics.DrawRectangle(darkPen, rc);
      e.Graphics.DrawString(text, this.Font, textBrush, rc, textFormat);
    }

    /// <summary>
    /// Move color select cursor
    /// </summary>
    /// <param name="delta">steps</param>
    private void MoveIndex(int delta)
    {
      int lbound = (this.autoButton != "") ? -8 : 0;
      int ubound = 0x27 + ((this.moreButton != "") ? 8 : 0);
      int d = (ubound - lbound) + 1;
      if ((delta == -1) && (this.keyboardIndex < 0))
      {
        this.keyboardIndex = ubound;
      }
      else if ((delta == 1) && (this.keyboardIndex > 0x27))
      {
        this.keyboardIndex = lbound;
      }
      else if ((delta == 1) && (this.keyboardIndex < 0))
      {
        this.keyboardIndex = 0;
      }
      else if ((delta == -1) && (this.keyboardIndex > 0x27))
      {
        this.keyboardIndex = 0x27;
      }
      else
      {
        this.keyboardIndex += delta;
      }
      if (this.keyboardIndex < lbound)
      {
        this.keyboardIndex += d;
      }
      if (this.keyboardIndex > ubound)
      {
        this.keyboardIndex -= d;
      }
      if (this.keyboardIndex < 0)
      {
        this.colorIndex = 100;
      }
      else if (this.keyboardIndex > 0x27)
      {
        this.colorIndex = 0x65;
      }
      else
      {
        this.colorIndex = this.keyboardIndex;
      }
      this.Refresh();
    }

    /// <summary>
    /// Set current color.
    /// </summary>
    /// <param name="rc">Predefined color rectangle</param>
    /// <param name="pt">mouse point</param>
    /// <param name="index">index of color to select</param>
    /// <returns>true if pt lies in given rectangle</returns>
    protected bool SetColorIndex(Rectangle rc, Point pt, int index)
    {
      if (rc.Contains(pt))
      {
        if (this.colorIndex != index)
        {
          this.colorIndex = index;
          base.Invalidate();
        }
        return true;
      }
      return false;
    }

    #endregion //METHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER
    #endregion //HELPER
  }
}