using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;

namespace OgamaControls
{
  using VectorGraphics.Tools.CustomTypeConverter;

  /// <summary>
  /// Position button is a specialized dropdown button for a position selection.
  /// OnClick it opens a stimulus screen proportional panel of type <see cref="PositionSelector"/>on which
  /// the user can select a screen position and it will be recalculated in
  /// stimuli screen coordinates. 
  /// </summary>
  [ToolboxBitmap(typeof(resfinder), "OgamaControls.CommonControls.Buttons.PositionButton.PositionDropdown.bmp")]
  public partial class PositionDropdown : ComboBox, IPositionControl
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
    /// The position in stimulus screen coordinates.
    /// </summary>
    private Point _position;

    /// <summary>
    /// The stimulus screen size.
    /// </summary>
    private Size _stimulusScreenSize;

    /// <summary>
    /// Horizontal alignment of object at given position
    /// </summary>
    private HorizontalAlignment _alignment;

    /// <summary>
    /// Event. Raised, when the position has changed.
    /// </summary>
    public event EventHandler PositionChanged;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets or sets the current position value of the dropdown button.
    /// </summary>
    /// <value>A <see cref="Point"/> with the new position value.</value>
    public Point CurrentPosition
    {
      get
      {
        return _position;
      }
      set
      {
        _position = value;
        this.Items.Clear();
        this.Items.Add(ObjectStringConverter.PointToString(value));
        this.SelectedIndex = 0;
        OnPositionChanged(EventArgs.Empty);
      }
    }

    /// <summary>
    /// Gets or set the stimulus screen size
    /// </summary>
    /// <remarks>Used to size the position control correctly.</remarks>
    /// <value>A <see cref="Size"/> with the stimulus screen size.</value>
    public Size StimulusScreenSize
    {
      get { return _stimulusScreenSize; }
      set { _stimulusScreenSize = value; }
    }

    /// <summary>
    /// Horizontal text alignment for text at position value.
    /// </summary>
    /// <value>A <see cref="HorizontalAlignment"/> with the new alignment of the text.</value>
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
    /// Constructor.
    /// </summary>
    public PositionDropdown()
    {
      InitializeComponent();
      InitializeCustomComponents();
    }

    /// <summary>
    /// Component Constructor
    /// </summary>
    /// <param name="container">owning container</param>
    public PositionDropdown(IContainer container)
    {
      container.Add(this);
      InitializeComponent();
      InitializeCustomComponents();
    }

    /// <summary>
    /// Initialize fields with standard values.
    /// Add zero to dropdown item list.
    /// </summary>
    private void InitializeCustomComponents()
    {
      this._position = new Point(0, 0);
      this._alignment = HorizontalAlignment.Center;
      this.Items.Add(ObjectStringConverter.PointToString(this._position));
      this.Font = SystemFonts.DialogFont;
      this.BackColor = SystemColors.Window;
      this.ForeColor = SystemColors.WindowText;
      this._stimulusScreenSize = new Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
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
    /// OnClick event handler. Initializes the position selection control 
    /// and shows it.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void PositionDropdown_Click(object sender, EventArgs e)
    {
      //Open Position Selector.
      PositionSelector posSelector = new PositionSelector(
          base.Parent.PointToScreen(new Point(base.Left, base.Bottom)),
          this, this._position, this._stimulusScreenSize);
      posSelector.Alignment = this._alignment;
      if (posSelector.ShowDialog() == DialogResult.OK)
      {
        this.CurrentPosition = this._position;
      }
    }

    #endregion //WINDOWSEVENTHANDLER

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler for Custom Defined Events                                    //
    ///////////////////////////////////////////////////////////////////////////////
    #region CUSTOMEVENTHANDLER

    /// <summary>
    /// OnPositionChanged event handler. Raises delegate. 
    /// Notifys listeners that position value has changed.
    /// </summary>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    public virtual void OnPositionChanged(EventArgs e)
    {
      if (this.PositionChanged != null)
      {
        this.PositionChanged(this, e);
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
