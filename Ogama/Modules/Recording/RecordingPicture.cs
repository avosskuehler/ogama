// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RecordingPicture.cs" company="Freie Universität Berlin">
//   OGAMA - open gaze and mouse analyzer 
//   Copyright (C) 2014 Dr. Adrian Voßkühler  
//   Licensed under GPL V3
// </copyright>
// <author>Adrian Voßkühler</author>
// <email>adrian@ogama.net</email>
// <summary>
//   Derived from <see cref="Picture" />.
//   Used to display vector graphic elements of the recording Interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Ogama.Modules.Recording
{
  using System;
  using System.ComponentModel;
  using System.Drawing;
  using System.Drawing.Drawing2D;
  using System.Windows.Forms;

  using Ogama.ExceptionHandling;

  using VectorGraphics.Canvas;
  using VectorGraphics.Controls.Timer;
  using VectorGraphics.Elements;
  using VectorGraphics.Elements.ElementCollections;

  /// <summary>
  ///   Derived from <see cref="Picture" />.
  ///   Used to display vector graphic elements of the recording Interface.
  /// </summary>
  public partial class RecordingPicture : UserControl
  {
    #region Fields

    /// <summary>
    ///   Shape of gaze cursor
    /// </summary>
    private VGCursor gazeCursor;

    /// <summary>
    ///   Shape of mouse cursor
    /// </summary>
    private VGCursor mouseCursor;

    /// <summary>
    ///   Pen for gaze cursor
    /// </summary>
    private Pen penGazeCursor;

    /// <summary>
    ///   Pen for mouse cursor
    /// </summary>
    private Pen penMouseCursor;

    private MultimediaTimer foregroundUpdateTimer;

    #endregion

    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the RecordingPicture class.
    /// </summary>
    public RecordingPicture()
    {
      this.InitializeComponent();
      this.InitializeElements();
      this.SetStyle(
            ControlStyles.UserPaint |
            ControlStyles.AllPaintingInWmPaint |
            ControlStyles.DoubleBuffer,
            true);
      this.UpdateStyles();
    }

    #endregion

    #region Public Properties

    /// <summary>
    ///   Gets or sets gaze cursor
    /// </summary>
    /// <value>A <see cref="VGCursor" /> that is used to visualize the gaze position.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public VGCursor GazeCursor
    {
      get
      {
        return this.gazeCursor;
      }

      set
      {
        this.gazeCursor = value;
      }
    }

    /// <summary>
    ///   Gets or sets mouse cursor
    /// </summary>
    /// <value>A <see cref="VGCursor" /> that is used to visualize the mouse position</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public VGCursor MouseCursor
    {
      get
      {
        return this.mouseCursor;
      }

      set
      {
        this.mouseCursor = value;
      }
    }

    /// <summary>
    ///   Sets a value indicating whether the mouse cursor is visible or not.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool MouseCursorVisible
    {
      set
      {
        this.mouseCursor.Visible = value;
      }
    }

    /// <summary>
    /// Gets or sets the <see cref="Slide"/> for the background of this <see cref="Picture"/>.
    /// </summary>
    /// <value>A <see cref="Slide"/> with the contents that were seen during recording.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Slide BGSlide { get; set; }

    /// <summary>
    /// A <see cref="Matrix"/>, that transforms the output stimulus
    /// picture to the currently visible drawing bounds of the owning picture
    /// </summary>
    public Matrix StimulusToScreenMatrix { get; set; }

    /// <summary>
    /// Gets or sets the size of the stimulus that should be displayed
    /// in this picture
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Size StimulusSize { get; set; }


    /// <summary>
    /// Gets or sets the original canvas size
    /// that is the screen size of the tracking monitor,
    /// that is used to calculated correct transformation matrices.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Size PresentationSize { get; set; }


    #endregion

    #region Methods

    protected override void OnPaintBackground(PaintEventArgs e)
    {
      try
      {
        if (this.StimulusToScreenMatrix != null)
        {
          e.Graphics.Transform = this.StimulusToScreenMatrix;
        }

        if (this.BackgroundImage == null)
        {
          e.Graphics.Clear(this.BackColor);
        }
        else
        {
          e.Graphics.DrawImage(this.BackgroundImage, 0, 0);
        }

        if (this.BGSlide != null)
        {
          this.BGSlide.Draw(e.Graphics);
        }

        e.Graphics.ResetTransform();

        // Don´t call base, because it is unneeded overload. (lasts 5 ms longer than above call)
        // base.OnPaintBackground(pevent);
      }
      //catch (InvalidOperationException)
      //{
      //  // this occurs when the graphics object is in use elsewhere,
      //  // what is not a problem here
      //}
      catch (Exception ex)
      {
        ExceptionMethods.HandleException(ex);
      }
    }

    /// <summary>
    /// Calculates the transformation matrix that transforms the output stimulus
    /// picture to the currently visible drawing bounds of the owning picture
    /// </summary>
    /// <returns>A scaling and translating <see cref="Matrix"/>
    /// that transforms output coordinates in client size coordinates.</returns>
    protected Matrix GetTransformationMatrix()
    {
      if (this.Width != 0 && this.Height != 0)
      {
        int widthData = this.PresentationSize.Width;
        int heightData = this.PresentationSize.Height;

        //if (this.StimulusSize != Size.Empty)
        //{
        //  widthData = this.StimulusSize.Width;
        //  heightData = this.StimulusSize.Height;
        //}

        float largestRatio = (float)Math.Max(
          (double)widthData / (double)this.Width,
          (double)heightData / (double)this.Height);

        Matrix mx = new Matrix(1.0f / largestRatio, 0, 0, 1.0f / largestRatio, 0, 0);
        float posX = (float)((this.Width * largestRatio / 2) - (widthData / 2));
        float posY = (float)((this.Height * largestRatio / 2) - (heightData / 2));
        mx.Translate(posX, posY);
        return mx;
      }

      return new Matrix();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      //base.OnPaint(e);

      // Retreive new cursor positions.
      PointF gazeCenter;
      PointF mouseCenter;
      RecordModule.GetCurrentCursorPositions(out gazeCenter, out mouseCenter);

      this.gazeCursor.Center = gazeCenter;
      this.mouseCursor.Center = mouseCenter;

      if (this.StimulusToScreenMatrix != null)
      {
        e.Graphics.Transform = this.StimulusToScreenMatrix;
      }

      this.gazeCursor.Draw(e.Graphics);
      this.mouseCursor.Draw(e.Graphics);

      e.Graphics.ResetTransform();
    }

    /// <summary>
    ///   Overridden. Frees resources of objects that are not disposed
    ///   by the designer, mainly private objects.
    ///   Is called during the call to <see cref="Control.Dispose(bool)" />.
    /// </summary>
    protected void CustomDispose()
    {
      this.penGazeCursor.Dispose();
      this.penMouseCursor.Dispose();
      this.gazeCursor.Dispose();
      this.mouseCursor.Dispose();
    }

    /// <summary>
    /// The <see cref="Control.Resize"/> event handler. 
    /// Sets new transformation matrix for graphics picture 
    /// and updates it.
    /// </summary>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    protected override void OnResize(EventArgs e)
    {
      base.OnResize(e);
      try
      {
        this.AutoZoomPicture();
        this.Invalidate();
      }
      catch (Exception ex)
      {
        ExceptionMethods.HandleException(ex);
      }
    }

    /// <summary>
    ///   Initializes standard values of drawing elements
    /// </summary>
    private void InitializeElements()
    {
      try
      {
        this.penGazeCursor = new Pen(
          Properties.Settings.Default.GazeCursorColor,
          Properties.Settings.Default.GazeCursorWidth);
        this.penGazeCursor.DashStyle = Properties.Settings.Default.GazeCursorStyle;

        this.penMouseCursor = new Pen(
          Properties.Settings.Default.MouseCursorColor,
          Properties.Settings.Default.MouseCursorWidth);
        this.penMouseCursor.DashStyle = Properties.Settings.Default.MouseCursorStyle;

        var gazeCursorSize = (float)Properties.Settings.Default.GazeCursorSize;
        var gazeCursorType =
          (VGCursor.DrawingCursors)
          Enum.Parse(typeof(VGCursor.DrawingCursors), Properties.Settings.Default.GazeCursorType);
        this.gazeCursor = new VGCursor(
          this.penGazeCursor,
          gazeCursorType,
          gazeCursorSize,
          VGStyleGroup.RPL_PEN_GAZE_CURSOR);

        var mouseCursorSize = (float)Properties.Settings.Default.MouseCursorSize;
        var mouseCursorType =
          (VGCursor.DrawingCursors)
          Enum.Parse(typeof(VGCursor.DrawingCursors), Properties.Settings.Default.MouseCursorType);
        this.mouseCursor = new VGCursor(
          this.penMouseCursor,
          mouseCursorType,
          mouseCursorSize,
          VGStyleGroup.RPL_PEN_MOUSE_CURSOR);

        this.foregroundUpdateTimer = new MultimediaTimer();
        this.foregroundUpdateTimer.Period = 50;
        this.foregroundUpdateTimer.Tick += this.ForegroundUpdateTimerTick;
      }
      catch (Exception ex)
      {
        ExceptionMethods.HandleException(ex);
      }
    }

    /// <summary>
    /// The <see cref="Timer"/>s tick event handler for
    /// the background animation.
    /// Redraws the background.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void ForegroundUpdateTimerTick(object sender, EventArgs e)
    {
      this.Invalidate();
    }

    /// <summary>
    /// Starts animation by starting the update timer.
    /// </summary>
    public void StartAnimation()
    {
      this.foregroundUpdateTimer.Start();
    }

    /// <summary>
    /// Stops animation by stopping the update timer.
    /// </summary>
    public void StopAnimation()
    {
      this.foregroundUpdateTimer.Stop();
    }

    #endregion

    internal void AutoZoomPicture()
    {
      this.StimulusToScreenMatrix = this.GetTransformationMatrix();
    }
  }
}