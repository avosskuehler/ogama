using System;
using System.Drawing;

namespace OgamaControls
{
  /// <summary>
  /// Triangle shaped marker, that are used to show the
  /// color blend positions of the gradient control.
  /// </summary>
  public class Marker
  {
    ///////////////////////////////////////////////////////////////////////////////
    // Defining Constants                                                        //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTANTS

    /// <summary>
    /// Size of triangle shaped color blend marker.
    /// </summary>
    public const int MARKER_SIZE = 12;

    #endregion //CONSTANTS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Variables, Enumerations, Events                                  //
    ///////////////////////////////////////////////////////////////////////////////
    #region FIELDS

    /// <summary>
    /// Event. Raised when a color blend marker has changed.
    /// </summary>
    public event EventHandler MarkerUpdated;

    /// <summary>
    /// Bounds of this marker
    /// </summary>
    private RectangleF bounds;

    /// <summary>
    /// Color of this marker.
    /// </summary>
    private Color color;

    /// <summary>
    /// Position of this marker.
    /// </summary>
    private float position;

    /// <summary>
    /// Flag. True, if marker is movable.
    /// </summary>
    /// <remarks>Only markers at the beginning and the end of the gradient are not movable.</remarks>
    public readonly bool IsMovable;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets or sets the bounds of the marker.
    /// </summary>
    public RectangleF Bounds
    {
      get { return bounds; }
      set
      {
        if (this.IsMovable)
          bounds = value;
      }
    }

    /// <summary>
    /// Gets or sets the color of the marker.
    /// </summary>
    public Color Color
    {
      get { return color; }
      set
      {
        color = value;
        if (MarkerUpdated != null)
          MarkerUpdated(this, EventArgs.Empty);
      }
    }

    /// <summary>
    /// Gets or sets the position of the marker.
    /// </summary>
    public float Position
    {
      get { return position; }
      set
      {
        position = value;
        if (MarkerUpdated != null)
          MarkerUpdated(this, EventArgs.Empty);
      }
    }
    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Constructor. Creates a new marker with given parameters.
    /// </summary>
    /// <param name="position">new marker position</param>
    /// <param name="color">new marker color</param>
    public Marker(float position, Color color) : this(position, color, true) { }

    /// <summary>
    /// Constructor. Creates a new marker with given parameters.
    /// </summary>
    /// <param name="position">new marker position</param>
    public Marker(float position) : this(position, Color.White, true) { }

    /// <summary>
    /// Constructor. Creates a new marker with given parameters.
    /// </summary>
    /// <param name="position">new marker position</param>
    /// <param name="color">new marker color</param>
    /// <param name="isMovable">true, if marker should be movable.</param>
    public Marker(float position, Color color, bool isMovable)
    {
      this.Position = position;
      this.Color = color;
      this.IsMovable = isMovable;
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
    ///  Set the Position value without causing the MarkerUpdated event to fire
    /// </summary>
    /// <param name="position">New marker position</param>
    internal void SetPositionSilently(float position)
    {
      this.position = position;
    }

    /// <summary>
    /// Update the marker's bounds based on the marker strip's bounds.
    /// </summary>
    /// <param name="markerStripBounds">bounds of marker strip</param>
    public void UpdateBounds(Rectangle markerStripBounds)
    {
      float xOffset = markerStripBounds.Width * Position + markerStripBounds.X;
      float adjustment = MARKER_SIZE / 2;

      if (Position <= 1.0) xOffset -= adjustment;
      else xOffset += adjustment;

      this.bounds = new RectangleF(
                          new PointF(xOffset, markerStripBounds.Top),
                          new SizeF(MARKER_SIZE, MARKER_SIZE));
    }

    #endregion //METHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER
    #endregion //HELPER
  }
}
