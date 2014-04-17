// <copyright file="Picture.cs" company="FU Berlin">
// ******************************************************
// OGAMA - open gaze and mouse analyzer 
// Copyright (C) 2013 Dr. Adrian Voßkühler  
// ------------------------------------------------------------------------
// This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
// This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
// You should have received a copy of the GNU General Public License along with this program; if not, write to the Free Software Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
// **************************************************************
// </copyright>
// <author>Adrian Voßkühler</author>
// <email>adrian@ogama.net</email>

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
  /// Derived from <see cref="UserControl"/>. 
  /// Used to display VectorGraphic elements.
  /// </summary>
  /// <remarks>
  /// Add elements from the VectorGraphics.dll like <see cref="VGPolyline"/>,
  /// <see cref="VGRectangle"/> etc. to the Element list <see cref="Elements"/> , than you have
  /// two layers, the background with a <see cref="Slide"/> and the transparent foreground with
  /// the vector graphic elements.
  /// You can use an animation timer with <see cref="StartAnimation()"/> and 
  /// <see cref="StopAnimation()"/> that updates the 
  /// <see cref="Picture"/>every <see cref="AnimationInterval"/> milliseconds.
  /// </remarks>
  public partial class Picture : UserControl
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

    /////// <summary>
    /////// A debug time timing watch.
    /////// </summary>
    ////private Stopwatch watch;
    
    /// <summary>
    /// A lock object for the graphics object
    /// </summary>
    private readonly object thisLock = new object();

    /// <summary>
    /// This member holds an background slide with the 
    /// stimuli to display behind the foreground graphics.
    /// This is the slide that was shown during recording.
    /// </summary>
    private Slide backgroundSlide;

    /// <summary>
    /// The form on which the picture is located.
    /// </summary>
    private Form owningForm;

    /// <summary>
    /// The original screen size of the tracker.
    /// </summary>
    private Size presentationSize;

    /// <summary>
    /// Bitmap that is assigned to <see cref="Image"/> property of inherited 
    /// <see cref="PictureBox"/>.
    /// Background will be set to transparent
    /// </summary>
    private Bitmap foregroundBitmap;

    /// <summary>
    /// Graphics from Canvas Picture
    /// </summary>
    private Graphics foregroundGraphics;

    /// <summary>
    /// An alpha blended black brush, that hides grays out underlying content.
    /// </summary>
    private SolidBrush grayBrush;

    /// <summary>
    /// Element List, owns all Graphic Elements to be drawn on the surface
    /// </summary>
    private VGElementCollection elements;

    /// <summary>
    /// A <see cref="Matrix"/>, that transforms the output stimulus
    /// picture to the currently visible drawing bounds of the owning picture
    /// </summary>
    private Matrix stimulusToScreenMatrix;

    /// <summary>
    /// A <see cref="Matrix"/>, that transforms screen coordinates to 
    /// the output stimulus picture coordinates
    /// </summary>
    private Matrix screenToStimulusMatrix;

    /// <summary>
    /// Saves the current sections start time. Used to skip samples outside 
    /// the selected time section.
    /// </summary>
    private int sectionStartTime;

    /// <summary>
    /// Saves the current sections ending time.Used to skip samples outside 
    /// the selected time section.
    /// </summary>
    private int sectionEndTime;

    /// <summary>
    /// Indicates redrawing of the picture, prevents
    /// double calls to the drawing routines.
    /// </summary>
    private bool isRedrawing;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the Picture class.
    /// </summary>
    public Picture()
    {
      this.InitializeComponent();
      this.InitializeOther();
      this.SetStyle(
            ControlStyles.UserPaint |
            ControlStyles.AllPaintingInWmPaint |
            ControlStyles.DoubleBuffer,
            true);
      this.UpdateStyles();
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Enumerations                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region ENUMS

    /// <summary>
    /// Delegate. Support for thread-safe version of the <see cref="BeginUpdate()"/> method.
    /// </summary>
    private delegate void BeginUpdateDelegate();

    /// <summary>
    /// Delegate. Support for thread-safe version of the <see cref="EndUpdate()"/> method.
    /// </summary>
    private delegate void EndUpdateDelegate();

    /// <summary>
    /// An event that notifys the progress of the animation update.
    /// </summary>
    public event ProgressEventHandler Progress;

    #endregion ENUMS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets or sets the <see cref="Form"/> on which this <see cref="Picture"/> is located.
    /// </summary>
    /// <value>A <see cref="Form"/> that is the parent of this control.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Form OwningForm
    {
      get { return this.owningForm; }
      set { this.owningForm = value; }
    }

    /// <summary>
    /// Gets or sets the <see cref="Slide"/> for the background of this <see cref="Picture"/>.
    /// </summary>
    /// <value>A <see cref="Slide"/> with the contents that were seen during recording.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Slide BGSlide
    {
      get
      {
        return this.backgroundSlide;
      }

      set
      {
        this.backgroundSlide = value;
      }
    }

    /// <summary>
    /// Gets graphic element list of drawing canvas
    /// </summary>
    /// <value>The <see cref="List{VGElement}"/> with all <see cref="VGElement"/>
    /// objects to draw onto this <see cref="Picture"/></value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public VGElementCollection Elements
    {
      get { return this.elements; }
    }

    /// <summary>
    /// Gets an alpha blended black brush, that hides grays out underlying content.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public SolidBrush GrayBrush
    {
      get { return this.grayBrush; }
    }

    /// <summary>
    /// Gets or sets the bitmap which holds the canvas for the
    /// drawing elements.
    /// </summary>
    /// <value>The <see cref="Bitmap"/> for the canvas.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Bitmap ForegroundImage
    {
      get { return this.foregroundBitmap; }
      set { this.foregroundBitmap = value; }
    }

    /// <summary>
    /// Gets or sets the canvas updating animation interval 
    /// for the timer in milliseconds
    /// </summary>
    /// <value>A <see cref="int"/> with the update interval in
    /// miliseconds when this <see cref="Picture"/> is in animation mode</value>
    [Category("Behaviour"), DefaultValue(50)]
    [Description("The canvas updating animation interval for the timer in milliseconds during animation")]
    public int AnimationInterval
    {
      get { return this.tmrForeground.Period; }
      set { this.tmrForeground.Period = value; }
    }

    /// <summary>
    /// Gets or sets the canvas invalidation interval 
    /// in milliseconds
    /// </summary>
    /// <value>A <see cref="int"/> with the invalidation interval in
    /// miliseconds.</value>
    [Category("Behaviour"), DefaultValue(50)]
    [Description("The canvas invalidating interval for the timer in milliseconds.")]
    public int InvalidateInterval
    {
      get { return this.tmrBackground.Interval; }
      set { this.tmrBackground.Interval = value; }
    }

    /// <summary>
    /// Gets or sets the zoom factor for the display of this picture
    /// </summary>
    /// <value>A <see cref="Single"/> with the zoom factor 
    /// relative to its parents bounds. A value of 1 = fit to bounds.</value>
    [Category("Behaviour"), DefaultValue(1f)]
    [Description("The zoom factor for the picture.")]
    public float ZoomFactor { get; set; }

    /// <summary>
    /// Gets or sets the ScreenToStimulusMatrix
    /// </summary>
    /// <value>A <see cref="Matrix"/> which transforms screen coordinates to
    /// stimulus coordinates.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Matrix ScreenToStimulus
    {
      get { return this.screenToStimulusMatrix; }
      set { this.screenToStimulusMatrix = value; }
    }

    /// <summary>
    /// Gets or sets the Stimulus to Screen Matrix.
    /// </summary>
    /// <value>A <see cref="Matrix"/> which transforms stimulus coordinates to
    /// screen coordinates.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Matrix StimulusToScreen
    {
      get { return this.stimulusToScreenMatrix; }
      set { this.stimulusToScreenMatrix = value; }
    }

    /// <summary>
    /// Gets or sets the original canvas size
    /// that is the screen size of the tracking monitor,
    /// that is used to calculated correct transformation matrices.
    /// </summary>
    /// <value>The <see cref="Size"/>of the original
    /// tracking screen.</value>
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
        this.InitalizeOverlayGraphics();
      }
    }

    /// <summary>
    /// Gets or sets the size of the stimulus that should be displayed
    /// in this picture
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Size StimulusSize { get; set; }

    /// <summary>
    /// Gets or sets the current sections start time. 
    /// Used to skip samples outside the selected time section.
    /// </summary>
    /// <value>A <see cref="int"/> with the sections starting time in milliseconds.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int SectionStartTime
    {
      get { return this.sectionStartTime; }
      set { this.sectionStartTime = value; }
    }

    /// <summary>
    /// Gets or sets the current sections ending time.
    /// Used to skip samples outside the selected time section.
    /// </summary>
    /// <value>A <see cref="int"/> with the sections ending time in milliseconds.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int SectionEndTime
    {
      get { return this.sectionEndTime; }
      set { this.sectionEndTime = value; }
    }

    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Public methods                                                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region PUBLICMETHODS

    /// <summary>
    /// This method calls <see cref="RedrawAll"/> for the elements and 
    /// calculates the clipping region that should be invalidated
    /// and finally invalidates it.
    /// </summary>
    /// <param name="invalidateAll"><strong>True</strong>, if the whole
    /// canvas should be invalidated otherwise <strong>false</strong>. If
    /// <strong>false</strong> then only moved element regions are redrawn.</param>
    public void DrawForeground(bool invalidateAll)
    {
      try
      {
        // AsyncHelper.FireAsync(new MethodInvoker(RedrawAndInvalidate));
        this.RedrawAndInvalidate();
      }
      catch (Exception ex)
      {
        VGExceptionMethods.HandleExceptionSilent(ex);
      }
    }

    /// <summary>
    /// This method prevents the control from painting until the 
    /// <see cref="EndUpdate()"/> method is called.
    /// </summary>
    public void BeginUpdate()
    {
      if (this.InvokeRequired)
      {
        BeginUpdateDelegate beginUpdateDelegate = new BeginUpdateDelegate(this.BeginUpdate);
        this.Invoke(beginUpdateDelegate);
        return;
      }

      User32.SendMessage(this.Handle, User32.WMSETREDRAW, IntPtr.Zero, IntPtr.Zero);
    }

    /// <summary>
    /// Resumes painting the <see cref="Picture"/> control after painting is suspended by the 
    /// <see cref="BeginUpdate"/> method. 
    /// </summary>
    public void EndUpdate()
    {
      if (this.InvokeRequired)
      {
        EndUpdateDelegate endUpdateDelegate = new EndUpdateDelegate(this.EndUpdate);
        this.Invoke(endUpdateDelegate);
        return;
      }

      User32.SendMessage(this.Handle, User32.WMSETREDRAW, new IntPtr(1), IntPtr.Zero);
    }

    /// <summary>
    /// Renders whole picture to an image
    /// </summary>
    /// <remarks>Used for exporting to clipboard.</remarks>
    /// <returns>Image with drawn graphic elements and background.
    /// with size of stimulus screen</returns>
    public Image RenderToImage()
    {
      Bitmap canvasClone = new Bitmap(
        this.presentationSize.Width,
        this.presentationSize.Height,
        PixelFormat.Format32bppArgb);

      Graphics copyGraphics = Graphics.FromImage(canvasClone);
      copyGraphics.SmoothingMode = SmoothingMode.AntiAlias;
      copyGraphics.ResetTransform();
      copyGraphics.PageUnit = GraphicsUnit.Display;
      copyGraphics.PageScale = 1f;

      if (this.BackgroundImage != null)
      {
        copyGraphics.DrawImage(this.BackgroundImage, 0, 0, this.presentationSize.Width, this.presentationSize.Height);
      }

      if (this.ForegroundImage != null)
      {
        copyGraphics.DrawImage(this.ForegroundImage, 0, 0, this.presentationSize.Width, this.presentationSize.Height);
      }

      if (this.backgroundSlide != null)
      {
        Slide.DrawSlideAsync(this.backgroundSlide, copyGraphics);
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

    #region AnimationTimer

    /// <summary>
    /// Starts animation by starting the update timer.
    /// </summary>
    public virtual void StartAnimation()
    {
      this.tmrForeground.Start();
    }

    /// <summary>
    /// Stops animation by stopping the update timer.
    /// </summary>
    public virtual void StopAnimation()
    {
      this.tmrForeground.Stop();
    }

    /// <summary>
    /// Starts background update animation by starting the background timer.
    /// </summary>
    public virtual void StartBackgroundAnimation()
    {
      this.tmrBackground.Start();
    }

    /// <summary>
    /// Stops background update animation by stopping the background timer.
    /// </summary>
    public virtual void StopBackgroundAnimation()
    {
      this.tmrBackground.Stop();
    }

    #endregion //AnimationTimer

    /// <summary>
    /// Erases element list
    /// </summary>
    public virtual void ResetPicture()
    {
      this.elements.Clear();

      if (this.foregroundGraphics == null)
      {
        return;
      }

      lock (this.thisLock)
      {
        this.foregroundGraphics.Clear(Color.Transparent);
      }
    }

    /// <summary>
    /// Disposes background slide and image.
    /// </summary>
    public virtual void ResetBackground()
    {
      if (this.backgroundSlide != null)
      {
        this.backgroundSlide.Dispose();
        this.backgroundSlide = null;
      }

      if (this.BackgroundImage != null)
      {
        this.BackgroundImage.Dispose();
        this.BackgroundImage = null;
      }
    }

    /// <summary>
    /// Redraws every visible element that is in the elements collection onto
    /// the transparent foregroundGraphics.
    /// </summary>
    public void RedrawAll()
    {
      if (this.isRedrawing || this.foregroundGraphics == null)
      {
        return;
      }

      // Care about single usage of the graphics object
      lock (thisLock)
      {
        this.isRedrawing = true;
        this.foregroundGraphics.Clear(Color.Transparent);

        foreach (VGElement element in this.elements)
        {
          if (element.Visible)
          {
            element.ModifierKeys = Control.ModifierKeys;
            element.Draw(this.foregroundGraphics);
          }
        }

        this.isRedrawing = false;
      }
    }

    #endregion //PUBLICMETHODS

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

    /// <summary>
    /// The <see cref="Timer"/>s tick event handler.
    /// Redraws every element by invalidating the picture method.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    protected virtual void OnUpdatePicture(object sender, EventArgs e)
    {
      this.DrawForeground(false);
    }

    /// <summary>
    /// The <see cref="Timer"/>s tick event handler for
    /// the background animation.
    /// Redraws the background.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    protected void tmrBackground_Tick(object sender, EventArgs e)
    {
      this.Invalidate();
    }

    /// <summary>
    /// The protected OnProgress method raises the progress event by invoking 
    /// the delegates
    /// </summary>
    /// <param name="e">A <see cref="ProgressEventArgs"/> with the 
    /// progress event arguments</param>
    protected virtual void OnProgress(ProgressEventArgs e)
    {
      if (this.Progress != null)
      {
        // Invokes the delegates. 
        this.Progress(this, e);
      }
    }

    #endregion //CUSTOMEVENTHANDLER

    #endregion //EVENTS

    ///////////////////////////////////////////////////////////////////////////////
    // Inherited methods                                                         //
    ///////////////////////////////////////////////////////////////////////////////
    #region OVERRIDES

    /// <summary>
    /// Dispose custom elements that are not
    /// disposed by the designer.
    /// </summary>
    protected virtual void CustomDispose()
    {
      if (this.backgroundSlide != null)
      {
        this.backgroundSlide.Dispose();
      }

      if (this.foregroundBitmap != null)
      {
        this.foregroundBitmap.Dispose();
      }

      if (this.foregroundBitmap != null)
      {
        this.foregroundGraphics.Dispose();
      }

      this.grayBrush.Dispose();
      this.elements.Clear();
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
        this.InitalizeOverlayGraphics();
        this.RedrawAll();
        this.Invalidate();
      }
      catch (Exception ex)
      {
        VGExceptionMethods.HandleException(ex);
      }
    }

    /// <summary>
    /// Overridden <see cref="Control.OnPaintBackground(PaintEventArgs)"/>.
    /// Draws the background stimulus image onto the clipping region.
    /// </summary>
    /// <param name="pevent">The <see cref="PaintEventArgs"/>with the event data.</param>
    protected override void OnPaintBackground(PaintEventArgs pevent)
    {
      try
      {
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

        if (this.backgroundSlide != null)
        {
          this.backgroundSlide.Draw(pevent.Graphics);
        }

        pevent.Graphics.ResetTransform();

        // Don´t call base, because it is unneeded overload. (lasts 5 ms longer than above call)
        // base.OnPaintBackground(pevent);
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

    /// <summary>
    /// The <see cref="Control.Paint"/> event handler for this picture.
    /// Redraws the whole picture to the <see cref="foregroundBitmap"/>
    /// and draws this image to the picture boxs foreground.
    /// </summary>
    /// <param name="pe">A <see cref="PaintEventArgs"/> with the event data.</param>
    protected override void OnPaint(PaintEventArgs pe)
    {
      // draw the foreground image
      if (this.foregroundBitmap != null)
      {
        pe.Graphics.DrawImage(this.foregroundBitmap, 0, 0);
      }
    }

    #endregion //OVERRIDES

    ///////////////////////////////////////////////////////////////////////////////
    // Methods for doing main class job                                          //
    ///////////////////////////////////////////////////////////////////////////////
    #region METHODS

    /// <summary>
    /// Creates transparent bitmap for drawing and the corresponding graphics
    /// with correct transformation matrix.
    /// </summary>
    protected virtual void InitalizeOverlayGraphics()
    {
      this.BeginUpdate();

      int widthData = this.Width;
      int heightData = this.Height;
      if (widthData > 0 && heightData > 0)
      {
        if (this.foregroundGraphics != null)
        {
          this.foregroundGraphics.Dispose();
        }

        if (this.foregroundBitmap != null)
        {
          this.foregroundBitmap.Dispose();
        }

        this.foregroundBitmap = new Bitmap(widthData, heightData, PixelFormat.Format32bppArgb);
        this.foregroundGraphics = Graphics.FromImage(this.foregroundBitmap);
        this.foregroundGraphics.SmoothingMode = SmoothingMode.AntiAlias;

        this.foregroundGraphics.ResetTransform();
        this.foregroundGraphics.PageUnit = GraphicsUnit.Display;
        this.foregroundGraphics.PageScale = 1f;

        // Create transformation matrices.
        this.stimulusToScreenMatrix = this.GetTransformationMatrix();
        this.screenToStimulusMatrix = this.stimulusToScreenMatrix.Clone();
        this.screenToStimulusMatrix.Invert();
        this.foregroundGraphics.Transform = this.stimulusToScreenMatrix;
      }

      this.EndUpdate();
    }

    /// <summary>
    /// Redraw picture with immediate forced refresh of the control.
    /// </summary>
    /// <remarks>Used for video export </remarks>
    /// <returns><strong>True</strong> if finished.</returns>
    protected bool RedrawAllAndRefresh()
    {
      this.RedrawAll();
      this.Refresh();
      return true;
    }

    #endregion //METHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER

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
        int widthData = this.presentationSize.Width;
        int heightData = this.presentationSize.Height;

        if (this.StimulusSize != Size.Empty)
        {
          widthData = this.StimulusSize.Width;
          heightData = this.StimulusSize.Height;
        }

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

    /// <summary>
    /// Does a redraw all followed by an invalidate.
    /// </summary>
    private void RedrawAndInvalidate()
    {
      this.RedrawAll();
      this.Invalidate();
    }

    /// <summary>
    /// Custom initializations.
    /// </summary>
    private void InitializeOther()
    {
      this.elements = new VGElementCollection();

      //// this.watch = new Stopwatch();
      //// this.watch.Start();

      this.grayBrush = new SolidBrush(Color.FromArgb(200, Color.Black));
      this.presentationSize = Screen.PrimaryScreen.Bounds.Size;
    }

    #endregion //HELPER
  }
}
