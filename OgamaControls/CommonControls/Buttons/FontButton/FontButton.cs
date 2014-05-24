using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;

namespace OgamaControls
{
  /// <summary>
  /// The font button control is a specialized button for selecting fonts from the standard
  /// windows forms font dialog. Its specific is a triangle that imitates a dropdown
  /// possibility and a onclick opening of the font selection dialog.
  /// Its text "Sample..." is drawn with the currently selected font.
  /// </summary>
  [ToolboxBitmap(typeof(Button))]
  public partial class FontButton : Button
  {
    ///////////////////////////////////////////////////////////////////////////////
    // Defining Constants                                                        //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTANTS

    /// <summary>
    /// Default text size of font button
    /// </summary>
    public static int DEFAULTTEXTSIZE = 8;

    /// <summary>
    /// Default caption of font button
    /// </summary>
    private static string DEFAULTCAPTION = "Sample...";

    #endregion //CONSTANTS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Variables, Enumerations, Events                                  //
    ///////////////////////////////////////////////////////////////////////////////
    #region FIELDS

    /// <summary>
    /// Flag. True, if font selection panel is visible
    /// </summary>
    private bool _panelVisible = false;

    ///// <summary>
    ///// Event. Raised, when buttons font has changed.
    ///// </summary>
    //public event EventHandler FontHasChanged;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets or sets font selection panels visibility.
    /// </summary>
    /// <value>A <see cref="Boolean"/> whether the font selection dialog is visible.</value>
    public Boolean PanelVisible
    {
      get { return _panelVisible; }
      set { _panelVisible = value; }
    }

    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Constructor
    /// </summary>
    public FontButton()
    {
      this.InitializeComponent();
    }

    /// <summary>
    /// Container Constructor.
    /// </summary>
    public FontButton(IContainer container)
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
    /// On Click event handler. Opens <see cref="FontDialog"/> dialog and
    /// raises <see cref="Control.FontChanged"/> event.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">Empty <see cref="EventArgs"/></param>
    private void FontButton_Click(object sender, EventArgs e)
    {
      this._panelVisible = true;
      this.Refresh();
      FontDialog dlg = new FontDialog();
      dlg.ShowColor = false;
      dlg.Font = this.Font;
      if (dlg.ShowDialog() == DialogResult.OK)
      {
        System.Drawing.Font newFont = new System.Drawing.Font(dlg.Font.FontFamily.Name, DEFAULTTEXTSIZE,
          dlg.Font.Style, dlg.Font.Unit, dlg.Font.GdiCharSet, dlg.Font.GdiVerticalFont);
        this.Font = newFont;

        //Notify Buttons change to listeners (DataGridViewCells for example)
        this.OnFontChanged(EventArgs.Empty);
      }
    }

    /// <summary>
    /// On Paint event handler. Draws a button with the string "Sample ..."
    /// in the buttons font and a triangle to show the drop down possibility.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A <see cref="PaintEventArgs"/> that contains the event data.</param>
    private void FontButton_Paint(object sender, PaintEventArgs e)
    {
      Rectangle rc = new Rectangle(e.ClipRectangle.Left + 5, e.ClipRectangle.Top + 5, 
        e.ClipRectangle.Width - 0x1c, e.ClipRectangle.Height - 11);
      if (base.Enabled)
      {
        e.Graphics.DrawString(DEFAULTCAPTION, this.Font, new SolidBrush(Color.Black), rc.Location);
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

    ///// <summary>
    ///// OnFontChanged event handler. Raises delegate.
    ///// </summary>
    ///// <param name="e">not used</param>
    //protected virtual void OnFontHasChanged(EventArgs e)
    //{
    //  if (this.FontHasChanged != null)
    //  {
    //    this.FontHasChanged(this, e);
    //  }
    //}

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