// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Picture.cs" company="Freie Universität Berlin">
//   GPL v3
// </copyright>
// <author>Adrian Voßkühler</author>
// <email>adrian@ogama.net</email>
// <summary>
//   Derived from .
//   Used to display VectorGraphic elements.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace VectorGraphics.Canvas
{
  using System;
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.Drawing;
  using System.Drawing.Drawing2D;
  using System.Drawing.Imaging;
  using System.Windows.Forms;

  using VectorGraphics.Elements;
  using VectorGraphics.Elements.ElementCollections;
  using VectorGraphics.Tools;
  using VectorGraphics.Tools.CustomEventArgs;
  using VectorGraphics.Tools.Win32;

  /// <summary>
  ///   Derived from <see cref="UserControl" />.
  ///   Used to display VectorGraphic elements.
  /// </summary>
  /// <remarks>
  ///   Add elements from the VectorGraphics.dll like <see cref="VGPolyline" />,
  ///   <see cref="VGRectangle" /> etc. to the Element list <see cref="Elements" /> , than you have
  ///   two layers, the background with a <see cref="Slide" /> and the transparent foreground with
  ///   the vector graphic elements.
  ///   You can use an animation timer with <see cref="StartAnimation()" /> and
  ///   <see cref="StopAnimation()" /> that updates the
  ///   <see cref="Picture" />every <see cref="AnimationInterval" /> milliseconds.
  /// </remarks>
  public partial class Picture : UserControl
  {
    #region Fields

    /// <summary>
    ///   The lock for the graphics processes
    /// </summary>
    private readonly object graphicsLock = new object();

    /// <summary>
    ///   Element List, owns all Graphic Elements to be drawn on the surface
    /// </summary>
    private VGElementCollection elements;

    /// <summary>
    ///   An alpha blended black brush, that hides grays out underlying content.
    /// </summary>
    private SolidBrush grayBrush;

    /// <summary>
    ///   The original screen size of the tracker.
    /// </summary>
    private Size presentationSize;

    #endregion

    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the Picture class.
    /// </summary>
    public Picture()
    {
      this.InitializeComponent();
      this.InitializeOther();
      this.SetStyle(
        ControlStyles.UserPaint |
        ControlStyles.AllPaintingInWmPaint |
        ControlStyles.OptimizedDoubleBuffer |
        ControlStyles.ResizeRedraw,
        true);
      this.UpdateStyles();
    }

    #endregion

    #region Delegates

    /// <summary>
    ///   Delegate. Support for thread-safe version of the <see cref="BeginUpdate()" /> method.
    /// </summary>
    private delegate void BeginUpdateDelegate();

    /// <summary>
    ///   Delegate. Support for thread-safe version of the <see cref="EndUpdate()" /> method.
    /// </summary>
    private delegate void EndUpdateDelegate();

    #endregion

    #region Public Events

    /// <summary>
    ///   An event that notifys the progress of the animation update.
    /// </summary>
    public event ProgressEventHandler Progress;

    #endregion

    #region Public Properties

    /// <summary>
    ///   Gets or sets the canvas updating animation interval
    ///   for the timer in milliseconds
    /// </summary>
    /// <value>
    ///   A <see cref="int" /> with the update interval in
    ///   miliseconds when this <see cref="Picture" /> is in animation mode
    /// </value>
    [Category("Behaviour")]
    [DefaultValue(50)]
    [Description("The canvas updating animation interval for the timer in milliseconds during animation")]
    public int AnimationInterval
    {
      get
      {
        return this.tmrForeground.Period;
      }

      set
      {
        this.tmrForeground.Period = value;
      }
    }

    /// <summary>
    ///   Gets or sets the <see cref="Slide" /> for the background of this <see cref="Picture" />.
    /// </summary>
    /// <value>A <see cref="Slide" /> with the contents that were seen during recording.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Slide BgSlide { get; set; }

    /// <summary>
    ///   Gets graphic element list of drawing canvas
    /// </summary>
    /// <value>
    ///   The <see cref="List{T}" /> with all <see cref="VGElement" />
    ///   objects to draw onto this <see cref="Picture" />
    /// </value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public VGElementCollection Elements
    {
      get
      {
        return this.elements;
      }
    }

    /// <summary>
    ///   Gets an alpha blended black brush, that hides grays out underlying content.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public SolidBrush GrayBrush
    {
      get
      {
        return this.grayBrush;
      }
    }

    /// <summary>
    ///   Gets or sets the canvas invalidation interval
    ///   in milliseconds
    /// </summary>
    /// <value>
    ///   A <see cref="int" /> with the invalidation interval in
    ///   miliseconds.
    /// </value>
    [Category("Behaviour")]
    [DefaultValue(50)]
    [Description("The canvas invalidating interval for the timer in milliseconds.")]
    public int InvalidateInterval
    {
      get
      {
        return this.tmrBackground.Interval;
      }

      set
      {
        this.tmrBackground.Interval = value;
      }
    }

    /// <summary>
    ///   Gets or sets the <see cref="Form" /> on which this <see cref="Picture" /> is located.
    /// </summary>
    /// <value>A <see cref="Form" /> that is the parent of this control.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Form OwningForm { get; set; }

    /// <summary>
    ///   Gets or sets the original canvas size
    ///   that is the screen size of the tracking monitor,
    ///   that is used to calculated correct transformation matrices.
    /// </summary>
    /// <value>
    ///   The <see cref="Size" />of the original
    ///   tracking screen.
    /// </value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Size PresentationSize
    {
      get
      {
        return this.presentationSize;
      }

      set
      {
        this.presentationSize = value;
        this.CalculateTransformMatrix();
      }
    }

    /// <summary>
    ///   Gets or sets the ScreenToStimulusMatrix
    /// </summary>
    /// <value>
    ///   A <see cref="Matrix" /> which transforms screen coordinates to
    ///   stimulus coordinates.
    /// </value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Matrix ScreenToStimulus { get; set; }

    /// <summary>
    ///   Gets or sets the current sections ending time.
    ///   Used to skip samples outside the selected time section.
    /// </summary>
    /// <value>A <see cref="int" /> with the sections ending time in milliseconds.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int SectionEndTime { get; set; }

    /// <summary>
    ///   Gets or sets the current sections start time.
    ///   Used to skip samples outside the selected time section.
    /// </summary>
    /// <value>A <see cref="int" /> with the sections starting time in milliseconds.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int SectionStartTime { get; set; }

    /// <summary>
    ///   Gets or sets the size of the stimulus that should be displayed
    ///   in this picture
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Size StimulusSize { get; set; }

    /// <summary>
    ///   Gets or sets the Stimulus to Screen Matrix.
    /// </summary>
    /// <value>
    ///   A <see cref="Matrix" /> which transforms stimulus coordinates to
    ///   screen coordinates.
    /// </value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Matrix StimulusToScreen { get; set; }

    /// <summary>
    ///   Gets or sets the zoom factor for the display of this picture
    /// </summary>
    /// <value>
    ///   A <see cref="float" /> with the zoom factor
    ///   relative to its parents bounds. A value of 1 = fit to bounds.
    /// </value>
    [Category("Behaviour")]
    [DefaultValue(1f)]
    [Description("The zoom factor for the picture.")]
    public float ZoomFactor { get; set; }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    ///   This method prevents the control from painting until the
    ///   <see cref="EndUpdate()" /> method is called.
    /// </summary>
    public void BeginUpdate()
    {
      if (this.InvokeRequired)
      {
        var beginUpdateDelegate = new BeginUpdateDelegate(this.BeginUpdate);
        this.Invoke(beginUpdateDelegate);
        return;
      }

      User32.SendMessage(this.Handle, User32.WMSETREDRAW, IntPtr.Zero, IntPtr.Zero);
    }

    /// <summary>
    /// This method invalidates this picture.
    /// </summary>
    /// <param name="invalidateAll">
    /// <strong>True</strong>, if the whole
    ///   canvas should be invalidated otherwise <strong>false</strong>. If
    ///   <strong>false</strong> then only moved element regions are redrawn.
    /// </param>
    public void DrawForeground(bool invalidateAll)
    {
      this.Invalidate();
    }

    /// <summary>
    ///   Resumes painting the <see cref="Picture" /> control after painting is suspended by the
    ///   <see cref="BeginUpdate" /> method.
    /// </summary>
    public void EndUpdate()
    {
      if (this.InvokeRequired)
      {
        var endUpdateDelegate = new EndUpdateDelegate(this.EndUpdate);
        this.Invoke(endUpdateDelegate);
        return;
      }

      User32.SendMessage(this.Handle, User32.WMSETREDRAW, new IntPtr(1), IntPtr.Zero);
    }

    /// <summary>
    ///   Renders whole picture to an image
    /// </summary>
    /// <remarks>Used for exporting to clipboard.</remarks>
    /// <returns>
    ///   Image with drawn graphic elements and background.
    ///   with size of stimulus screen
    /// </returns>
    public Image RenderToImage()
    {
      var canvasClone = new Bitmap(
        this.StimulusSize.Width,
        this.StimulusSize.Height,
        PixelFormat.Format32bppArgb);

      Graphics copyGraphics = Graphics.FromImage(canvasClone);
      copyGraphics.SmoothingMode = SmoothingMode.AntiAlias;
      copyGraphics.ResetTransform();
      copyGraphics.PageUnit = GraphicsUnit.Display;
      copyGraphics.PageScale = 1f;

      if (this.BackgroundImage != null)
      {
        copyGraphics.DrawImage(this.BackgroundImage, 0, 0, this.StimulusSize.Width, this.StimulusSize.Height);
      }

      if (this.BgSlide != null)
      {
        Slide.DrawSlideAsync(this.BgSlide, copyGraphics);
      }

      foreach (VGElement element in this.elements)
      {
        if (element.Visible)
        {
          element.Draw(copyGraphics);
        }
      }

      copyGraphics.Dispose();
      return canvasClone;
    }

    /// <summary>
    ///   Disposes background slide and image.
    /// </summary>
    public virtual void ResetBackground()
    {
      if (this.BgSlide != null)
      {
        this.BgSlide.Dispose();
        this.BgSlide = null;
      }

      if (this.BackgroundImage != null)
      {
        this.BackgroundImage.Dispose();
        this.BackgroundImage = null;
      }
    }

    /// <summary>
    ///   Erases element list
    /// </summary>
    public virtual void ResetPicture()
    {
      this.elements.Clear();
    }

    /// <summary>
    ///   Starts animation by starting the update timer.
    /// </summary>
    public virtual void StartAnimation()
    {
      this.tmrForeground.Start();
    }

    /// <summary>
    ///   Starts background update animation by starting the background timer.
    /// </summary>
    public virtual void StartBackgroundAnimation()
    {
      this.tmrBackground.Start();
    }

    /// <summary>
    ///   Stops animation by stopping the update timer.
    /// </summary>
    public virtual void StopAnimation()
    {
      this.tmrForeground.Stop();
    }

    /// <summary>
    ///   Stops background update animation by stopping the background timer.
    /// </summary>
    public virtual void StopBackgroundAnimation()
    {
      this.tmrBackground.Stop();
    }

    #endregion

    #region Methods

    /// <summary>
    ///   Creates transparent bitmap for drawing and the corresponding graphics
    ///   with correct transformation matrix.
    /// </summary>
    protected virtual void CalculateTransformMatrix()
    {
      // Create transformation matrices.
      this.StimulusToScreen = this.GetTransformationMatrix();
      this.ScreenToStimulus = this.StimulusToScreen.Clone();
      this.ScreenToStimulus.Invert();
    }

    /// <summary>
    ///   Dispose custom elements that are not
    ///   disposed by the designer.
    /// </summary>
    protected virtual void CustomDispose()
    {
      if (this.BgSlide != null)
      {
        this.BgSlide.Dispose();
      }

      this.grayBrush.Dispose();
      this.elements.Clear();
    }

    /// <summary>
    ///   Calculates the transformation matrix that transforms the output stimulus
    ///   picture to the currently visible drawing bounds of the owning picture
    /// </summary>
    /// <returns>
    ///   A scaling and translating <see cref="Matrix" />
    ///   that transforms output coordinates in client size coordinates.
    /// </returns>
    protected Matrix GetTransformationMatrix()
    {
      if (this.Width != 0 && this.Height != 0)
      {
        int widthData = this.presentationSize.Width;
        int heightData = this.presentationSize.Height;

        if (this.StimulusSize != Size.Empty)
        {
          widthData = this.StimulusSize.Width;
          heightData = this.StimulusSize.Height;
        }

        var largestRatio = (float)Math.Max(widthData / (double)this.Width, heightData / (double)this.Height);

        var mx = new Matrix(1.0f / largestRatio, 0, 0, 1.0f / largestRatio, 0, 0);
        float posX = (this.Width * largestRatio / 2) - (widthData / 2);
        float posY = (this.Height * largestRatio / 2) - (heightData / 2);
        mx.Translate(posX, posY);
        return mx;
      }

      return new Matrix();
    }

    /// <summary>
    /// The <see cref="Control.Paint"/> event handler for this picture.
    ///   Redraws the whole picture.
    /// </summary>
    /// <param name="pe">
    /// A <see cref="PaintEventArgs"/> with the event data.
    /// </param>
    protected override void OnPaint(PaintEventArgs pe)
    {
      //base.OnPaint(pe);

      lock (this.graphicsLock)
      {
        if (this.StimulusToScreen != null)
        {
          pe.Graphics.Transform = this.StimulusToScreen;
        }

        foreach (VGElement element in this.elements)
        {
          if (element.Visible)
          {
            element.ModifierKeys = Control.ModifierKeys;
            element.Draw(pe.Graphics);
          }
        }

        pe.Graphics.ResetTransform();
      }
    }

    /// <summary>
    /// Overridden <see cref="Control.OnPaintBackground(PaintEventArgs)"/>.
    ///   Draws the background stimulus image onto the clipping region.
    /// </summary>
    /// <param name="pevent">
    /// The <see cref="PaintEventArgs"/>with the event data.
    /// </param>
    protected override void OnPaintBackground(PaintEventArgs pevent)
    {
      lock (this.graphicsLock)
      {
        try
        {
          // Don´t call base, because it is unneeded overload. (lasts 5 ms longer than above call)
          // base.OnPaintBackground(pevent);

          if (this.StimulusToScreen != null)
          {
            pevent.Graphics.Transform = this.StimulusToScreen;
          }

          if (this.BackgroundImage == null)
          {
            pevent.Graphics.Clear(this.BackColor);
          }
          else
          {
            pevent.Graphics.DrawImage(this.BackgroundImage, 0, 0);
          }

          if (this.BgSlide != null)
          {
            this.BgSlide.Draw(pevent.Graphics);
          }

          pevent.Graphics.ResetTransform();

        }
        catch (InvalidOperationException)
        {
          // this occurs when the graphics object is in use elsewhere,
          // what is not a problem here
        }
        catch (Exception ex)
        {
          VGExceptionMethods.HandleException(ex);
        }
      }
    }

    /// <summary>
    /// The protected OnProgress method raises the progress event by invoking
    ///   the delegates
    /// </summary>
    /// <param name="e">
    /// A <see cref="ProgressEventArgs"/> with the
    ///   progress event arguments
    /// </param>
    protected virtual void OnProgress(ProgressEventArgs e)
    {
      if (this.Progress != null)
      {
        // Invokes the delegates. 
        this.Progress(this, e);
      }
    }

    /// <summary>
    /// The <see cref="Control.Resize"/> event handler.
    ///   Sets new transformation matrix for graphics picture
    /// </summary>
    /// <param name="e">
    /// An empty <see cref="EventArgs"/>
    /// </param>
    protected override void OnResize(EventArgs e)
    {
      this.CalculateTransformMatrix();
      base.OnResize(e);
    }

    /// <summary>
    /// The <see cref="Timer"/>s tick event handler.
    ///   Redraws every element by invalidating the picture method.
    /// </summary>
    /// <param name="sender">
    /// Source of the event.
    /// </param>
    /// <param name="e">
    /// An empty <see cref="EventArgs"/>
    /// </param>
    protected virtual void ForegroundTimerTick(object sender, EventArgs e)
    {
      this.DrawForeground(false);
    }

    /// <summary>
    /// The <see cref="Timer"/>s tick event handler for
    ///   the background animation.
    ///   Redraws the background.
    /// </summary>
    /// <param name="sender">
    /// Source of the event.
    /// </param>
    /// <param name="e">
    /// An empty <see cref="EventArgs"/>
    /// </param>
    protected void TmrBackgroundTick(object sender, EventArgs e)
    {
      this.Invalidate();
    }

    /// <summary>
    ///   Custom initializations.
    /// </summary>
    private void InitializeOther()
    {
      this.elements = new VGElementCollection();
      this.grayBrush = new SolidBrush(Color.FromArgb(200, Color.Black));
      this.presentationSize = Screen.PrimaryScreen.Bounds.Size;
    }

    #endregion
  }
}