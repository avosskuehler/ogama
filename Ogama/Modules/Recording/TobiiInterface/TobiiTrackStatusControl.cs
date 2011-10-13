// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TobiiTrackStatusControl.cs" company="">
//   
// </copyright>
// <summary>
//   The tobii track status control.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Ogama.Modules.Recording.TobiiInterface
{
  using System.Collections.Generic;
  using System.Drawing;
  using System.Windows.Forms;

  using global::Tobii.Eyetracking.Sdk;

  /// <summary>
  /// The tobii track status control.
  /// </summary>
  public partial class TobiiTrackStatusControl : UserControl
  {
    #region Constants and Fields

    /// <summary>
    /// The _brush.
    /// </summary>
    private readonly SolidBrush _brush;

    /// <summary>
    /// The _data history.
    /// </summary>
    private readonly Queue<GazeDataItem> _dataHistory;

    /// <summary>
    /// The _eye brush.
    /// </summary>
    private readonly SolidBrush _eyeBrush;

    /// <summary>
    /// The bar height.
    /// </summary>
    private static int BarHeight = 25;

    /// <summary>
    /// The eye radius.
    /// </summary>
    private static int EyeRadius = 8;

    /// <summary>
    /// The history size.
    /// </summary>
    private static int HistorySize = 30;

    /// <summary>
    /// The _left eye.
    /// </summary>
    private Point3D _leftEye;

    /// <summary>
    /// The _left validity.
    /// </summary>
    private int _leftValidity;

    /// <summary>
    /// The _right eye.
    /// </summary>
    private Point3D _rightEye;

    /// <summary>
    /// The _right validity.
    /// </summary>
    private int _rightValidity;

    #endregion

    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="TobiiTrackStatusControl"/> class.
    /// </summary>
    public TobiiTrackStatusControl()
    {
      this.InitializeComponent();

      this.SetStyle(ControlStyles.UserPaint, true);
      this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
      this.SetStyle(ControlStyles.DoubleBuffer, true);

      this._dataHistory = new Queue<GazeDataItem>(HistorySize);

      this._brush = new SolidBrush(Color.Red);
      this._eyeBrush = new SolidBrush(Color.White);

      this._leftValidity = 4;
      this._rightValidity = 4;
    }

    #endregion

    #region Properties

    /// <summary>
    ///   Gets the current brush
    /// </summary>
    private SolidBrush Brush
    {
      get
      {
        if (this._leftValidity == 4 && this._rightValidity == 4)
        {
          this._brush.Color = Color.Red;
        }
        else if (this._leftValidity == 0 && this._rightValidity == 0)
        {
          this._brush.Color = Color.Lime;
        }
        else if (this._leftValidity == 2 && this._rightValidity == 2)
        {
          this._brush.Color = Color.Orange;
        }
        else
        {
          this._brush.Color = Color.Yellow;
        }

        return this._brush;
      }
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// The clear.
    /// </summary>
    public void Clear()
    {
      this._dataHistory.Clear();
      this._leftValidity = 0;
      this._rightValidity = 0;
      this._leftEye = new Point3D();
      this._rightEye = new Point3D();

      this.Invalidate();
    }

    /// <summary>
    /// The on gaze data.
    /// </summary>
    /// <param name="gd">
    /// The gd.
    /// </param>
    public void OnGazeData(GazeDataItem gd)
    {
      // Add data to history
      this._dataHistory.Enqueue(gd);

      // Remove history item if necessary
      while (this._dataHistory.Count > HistorySize)
      {
        this._dataHistory.Dequeue();
      }

      this._leftValidity = gd.LeftValidity;
      this._rightValidity = gd.RightValidity;

      this._leftEye = gd.LeftEyePosition3DRelative;
      this._rightEye = gd.RightEyePosition3DRelative;

      this.Invalidate();
    }

    #endregion

    #region Methods

    /// <summary>
    /// The on paint.
    /// </summary>
    /// <param name="e">
    /// The e.
    /// </param>
    protected override void OnPaint(PaintEventArgs e)
    {
      base.OnPaint(e);

      // Compute status bar color
      this._brush.Color = this.ComputeStatusColor();

      // Draw bottom bar
      e.Graphics.FillRectangle(this._brush, new Rectangle(0, this.Height - BarHeight, this.Width, BarHeight));

      // Draw eyes
      if (this._leftValidity <= 2)
      {
        var r = new RectangleF(
          (float)((1.0 - this._leftEye.X) * this.Width - EyeRadius), 
          (float)(this._leftEye.Y * this.Height - EyeRadius), 
          2 * EyeRadius, 
          2 * EyeRadius);
        e.Graphics.FillEllipse(this._eyeBrush, r);
      }

      if (this._rightValidity <= 2)
      {
        var r = new RectangleF(
          (float)((1 - this._rightEye.X) * this.Width - EyeRadius), 
          (float)(this._rightEye.Y * this.Height - EyeRadius), 
          2 * EyeRadius, 
          2 * EyeRadius);
        e.Graphics.FillEllipse(this._eyeBrush, r);
      }
    }

    /// <summary>
    /// The compute status color.
    /// </summary>
    /// <returns>
    /// </returns>
    private Color ComputeStatusColor()
    {
      if (!this.Enabled)
      {
        return Color.Gray;
      }

      int quality = 0;
      int count = 0;

      foreach (GazeDataItem item in this._dataHistory)
      {
        if (item.LeftValidity == 4 && item.RightValidity == 4)
        {
          quality += 0;
        }
        else if (item.LeftValidity == 0 && item.RightValidity == 0)
        {
          quality += 2;
        }
        else
        {
          quality++;
        }

        count++;
      }

      float q = count == 0 ? 0 : quality / (2F * count);

      if (q > 0.8)
      {
        return Color.Lime;
      }

      if (q < 0.1)
      {
        return Color.Red;
      }

      return Color.Red;
    }

    #endregion
  }
}