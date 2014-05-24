using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace OgamaControls
{
  /// <summary>
  /// A form that is used to select a position respective to screen.
  /// </summary>
  /// <remarks>It has 9 alignment buttons for the standard alignments topleft, etc.
  /// Its initial size is the screen size divided with <see cref="SCREENSIZEDIVIDER"/>.
  /// The alignment can be deactivated by holding the shift key.</remarks>
  [DesignTimeVisible(false)]
  public partial class PositionSelector : Form
  {
    ///////////////////////////////////////////////////////////////////////////////
    // Defining Constants                                                        //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTANTS

    /// <summary>
    /// To this amount the stimulus screen size is divided to size the panel.
    /// </summary>
    private const int SCREENSIZEDIVIDER = 4;

    /// <summary>
    /// Size of the alignment buttons.
    /// </summary>
    private const int BUTTONSIZE = 14;

    #endregion //CONSTANTS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Variables, Enumerations, Events                                  //
    ///////////////////////////////////////////////////////////////////////////////
    #region FIELDS

    /// <summary>
    /// Owning control that has created an instance of this position selector
    /// </summary>
    private IPositionControl _positionControl;

    /// <summary>
    /// Horizontal alignment for the stimulus text 
    /// </summary>
    private HorizontalAlignment _alignment;

    /// <summary>
    /// Position in stimulus screen coordinates
    /// </summary>
    private Point _textPosition;

    /// <summary>
    /// List of rectangles to hold the position and size of the alignment buttons.
    /// </summary>
    private List<Rectangle> _alignmentButtons;

    /// <summary>
    /// Default position marker text.
    /// </summary>
    private Font _defaultFont = SystemFonts.MenuFont;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Horizontal alignment for the position value.
    /// </summary>
    /// <value>The <see cref="HorizontalAlignment"/> of the displayed reference text.</value>
    public HorizontalAlignment Alignment
    {
      get { return _alignment; }
      set { _alignment = value; }
    }
    
    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="stimulusScreenSize">stimulus screen size</param>
    public PositionSelector(Size stimulusScreenSize)
    {
      InitializeComponent();
      CustomInitialize(stimulusScreenSize);
    }


    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="ctrlPosition">Position of this control</param>
    /// <param name="positionControl">owning control</param>
    /// <param name="location">position of instruction cursor</param>
    /// <param name="stimulusScreenSize">stimulus screen size</param>
    public PositionSelector(Point ctrlPosition, IPositionControl positionControl, Point location, Size stimulusScreenSize)
    {
      InitializeComponent();
      CustomInitialize(stimulusScreenSize);

      this._positionControl = positionControl;
      base.Location = ctrlPosition;

      //Set cursor to location
      NotifyNewPosition(new Point(location.X / SCREENSIZEDIVIDER, location.Y / SCREENSIZEDIVIDER));
    }

    /// <summary>
    /// Provides custom initialization.
    /// Sets size and generates alignment buttons.
    /// </summary>
    private void CustomInitialize(Size stimulusScreenSize)
    {
      this._alignment = HorizontalAlignment.Center;
      base.CenterToScreen();
      base.Capture = true;

      //Set window size
      SizeF newClientSize = new SizeF(stimulusScreenSize.Width / (float)SCREENSIZEDIVIDER,
                                  stimulusScreenSize.Height / (float)SCREENSIZEDIVIDER);
      base.ClientSize = Size.Round(newClientSize);

      //cursortext is only for quick measuring the size of the "instruction" text
      //because of a transparent label needed, we need to draw everything in the OnPaint
      //method for ourselves
      this.cursorText.Font = _defaultFont;

      //Add alignment buttons
      CalculatePositionOfAlignmentButtons();
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
    /// OnMouseDown event handler. Notifys parent of new position.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A <see cref="MouseEventArgs"/> that contains the event data.</param>
    private void PositionSelector_MouseDown(object sender, MouseEventArgs e)
    {
      NotifyNewPosition(e.Location);
    }

    /// <summary>
    /// OnMouseMove event handler. Checks for alignments and updates parent with new position
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A <see cref="MouseEventArgs"/> that contains the event data.</param>
    private void PositionSelector_MouseMove(object sender, MouseEventArgs e)
    {
      MouseMoveHandler(e);
    }

    /// <summary>
    /// OnMouseUp event handler. Checks for alignments and updates parent with new position.
    /// If cursor is out of panel close the panel without setting <see cref="DialogResult"/> to OK,
    /// so that changes will not be committed.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A <see cref="MouseEventArgs"/> that contains the event data.</param>
    private void PositionSelector_MouseUp(object sender, MouseEventArgs e)
    {
      if (base.RectangleToScreen(base.ClientRectangle).Contains(Cursor.Position))
      {
        Point currentPoint;
        //Check for Modifier Shiftm to avoid aligning when it is pressed.
        if (ModifierKeys != Keys.Shift)
        {
          currentPoint = CheckForAlignments(e);
        }
        else
        {
          currentPoint = e.Location;
        }
        //Align if is in range of an alignment button.

        NotifyNewPosition(currentPoint);
      }
      this.Close();
    }

    /// <summary>
    /// OnPaint event handler. Draws the alignment buttons and the position marker text.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A <see cref="PaintEventArgs"/> that contains the event data.</param>
    private void PositionSelector_Paint(object sender, PaintEventArgs e)
    {
      foreach (Rectangle rect in _alignmentButtons)
      {
        ButtonRenderer.DrawButton(e.Graphics, rect, PushButtonState.Default);
      }

      e.Graphics.DrawString("Instruction", _defaultFont,
        new SolidBrush(SystemColors.ControlText), _textPosition);
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
    /// Calculates position of alignment buttons and add them to a list
    /// of rectangles. (TopLeft,TopMiddle,TopRight,MiddleLeft and so on)
    /// </summary>
    private void CalculatePositionOfAlignmentButtons()
    {
      int top = base.ClientRectangle.Top + 10;
      int centerY = base.ClientRectangle.Height / 2 - BUTTONSIZE / 2;
      int bottom = base.ClientRectangle.Bottom - 10 - BUTTONSIZE;
      int left = base.ClientRectangle.Left + 10;
      int centerX = base.ClientRectangle.Width / 2 - BUTTONSIZE / 2;
      int right = base.ClientRectangle.Right - 10 - BUTTONSIZE;

      _alignmentButtons = new List<Rectangle>();
      _alignmentButtons.Add(new Rectangle(left, top, BUTTONSIZE, BUTTONSIZE));
      _alignmentButtons.Add(new Rectangle(centerX, top, BUTTONSIZE, BUTTONSIZE));
      _alignmentButtons.Add(new Rectangle(right, top, BUTTONSIZE, BUTTONSIZE));
      _alignmentButtons.Add(new Rectangle(left, centerY, BUTTONSIZE, BUTTONSIZE));
      _alignmentButtons.Add(new Rectangle(centerX, centerY, BUTTONSIZE, BUTTONSIZE));
      _alignmentButtons.Add(new Rectangle(right, centerY, BUTTONSIZE, BUTTONSIZE));
      _alignmentButtons.Add(new Rectangle(left, bottom, BUTTONSIZE, BUTTONSIZE));
      _alignmentButtons.Add(new Rectangle(centerX, bottom, BUTTONSIZE, BUTTONSIZE));
      _alignmentButtons.Add(new Rectangle(right, bottom, BUTTONSIZE, BUTTONSIZE));
    }

    /// <summary>
    /// When mouse has new position, notify the parent control of the value.
    /// </summary>
    /// <param name="e">A <see cref="MouseEventArgs"/> that contains the event data.</param>
    private void MouseMoveHandler(MouseEventArgs e)
    {
      if (base.RectangleToScreen(base.ClientRectangle).Contains(Cursor.Position))
      {
        if (e.Button == MouseButtons.Left)
        {
          Point currentPoint;
          //Check for Modifier Shiftm to avoid aligning when it is pressed.
          if (ModifierKeys != Keys.Shift)
          {
            currentPoint = CheckForAlignments(e);
          }
          else
          {
            currentPoint = e.Location;
          }
          //Align if is in range of an alignment button.

          NotifyNewPosition(currentPoint);
        }
      }
    }

    /// <summary>
    /// Snaps mouse location to the center of an alignment button,
    /// if it is 10 pixel near it.
    /// </summary>
    /// <param name="e">A <see cref="MouseEventArgs"/> that contains the event data.</param>
    /// <returns>snapped position (center of alignment button)</returns>
    private Point CheckForAlignments(MouseEventArgs e)
    {
      Point currentPoint = e.Location;
      foreach (Rectangle rect in _alignmentButtons)
      {
        rect.Inflate(10, 10);
        if (rect.Contains(currentPoint))
        {
          currentPoint = rect.Location;
          currentPoint.Offset(BUTTONSIZE / 2 + 10, BUTTONSIZE / 2 + 10);
        }
      }
      return currentPoint;
    }

    /// <summary>
    /// Let the owner know the new position.
    /// Take care of the alignment property.
    /// </summary>
    /// <param name="pt">A <see cref="Point"/> with the current position to send.</param>
    private void NotifyNewPosition(Point pt)
    {
      Point newPosition;

      //Set cursor position to alignment
      switch (_alignment)
      {
        case HorizontalAlignment.Left:
          newPosition = new Point(pt.X, pt.Y - this.cursorText.Size.Height / 2);
          break;
        case HorizontalAlignment.Right:
          newPosition = new Point(pt.X - this.cursorText.Size.Width,
                                       pt.Y - this.cursorText.Size.Height / 2);
          break;
        case HorizontalAlignment.Center:
        default:
          newPosition = new Point(pt.X - this.cursorText.Size.Width / 2,
                                       pt.Y - this.cursorText.Size.Height / 2);
          break;
      }
      this._textPosition = newPosition;
      //newPosition.X = (newPosition.X + this.cursorText.Size.Width / 2) * SCREENSIZEDIVIDER;
      //newPosition.Y = (newPosition.Y + this.cursorText.Size.Height / 2) * SCREENSIZEDIVIDER;
      newPosition.X = pt.X * SCREENSIZEDIVIDER;
      newPosition.Y = pt.Y * SCREENSIZEDIVIDER;
      //newPosition.X = (newPosition.X) * SCREENSIZEDIVIDER;
      //newPosition.Y = (newPosition.Y) * SCREENSIZEDIVIDER;

      this._positionControl.CurrentPosition = newPosition;
      this.Refresh();
    }


    #endregion //METHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER
    #endregion //HELPER

  }
}