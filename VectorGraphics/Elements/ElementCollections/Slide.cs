// <copyright file="Slide.cs" company="FU Berlin">
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

namespace VectorGraphics.Elements.ElementCollections
{
  using System;
  using System.ComponentModel;
  using System.Drawing;
  using System.Drawing.Drawing2D;
  using System.IO;
  using System.Xml.Serialization;

  using VectorGraphics.Controls;
  using VectorGraphics.Controls.Flash;
  using VectorGraphics.StopConditions;
  using VectorGraphics.Tools;
  using VectorGraphics.Tools.CustomTypeConverter;
  using VectorGraphics.Tools.Interfaces;
  using VectorGraphics.Tools.Trigger;

  /// <summary>
  /// Class to specify a slide with stimuli that can be 
  /// presented in the Presentation Interface.
  /// </summary>
  /// <remarks>Do not use the flag [DefaultValue(...)] for the properties, because otherwise
  /// it will not be serialized if it has the default value. 
  /// And then deserialization fails.
  /// </remarks>
  [Serializable]
  [TypeConverter(typeof(SlideConverter))]
  public class Slide : IClonableNamedObject, IDisposable
  {
    ///////////////////////////////////////////////////////////////////////////////
    // Defining Constants                                                        //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTANTS

    /// <summary>
    /// Size of slide thumbs.
    /// </summary>
    private static Size slideDesignThumbSize = new Size(150, 100);

    #endregion //CONSTANTS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Variables, Enumerations, Events                                  //
    ///////////////////////////////////////////////////////////////////////////////
    #region FIELDS

    /// <summary>
    /// Saves the list of vector graphic stimuli.
    /// </summary>
    private VGElementCollection stimuli;

    /// <summary>
    /// Saves the list of vector graphic stimuli that contain
    /// ActiveX controls (like <see cref="VGFlash"/>).
    /// </summary>
    private VGElementCollection activeXElements;

    /// <summary>
    /// This member saves the original presentation size of
    /// this Slide (used to calculate transformations)
    /// </summary>
    private Size presentationSize;

    /// <summary>
    /// Saves a thumb for this slide.
    /// </summary>
    private Image thumb;

    /// <summary>
    /// A list of <see cref="StopCondition"/> that can end the slide.
    /// </summary>
    private StopConditionCollection stopConditions;

    /// <summary>
    /// Saves the slides name.
    /// </summary>
    private string name;

    /// <summary>
    /// Saves the slides category.
    /// </summary>
    private string category;

    /// <summary>
    /// Saves a <see cref="StopConditionCollection"/> that contains
    /// valid responses for the slide.
    /// </summary>
    private StopConditionCollection correctResponses;

    /// <summary>
    /// Saves a <see cref="StopConditionCollection"/> that contains
    /// links from this slide to other trials.
    /// </summary>
    private StopConditionCollection links;

    /// <summary>
    /// Saves a <see cref="VGElementCollection"/> that contains the target
    /// areas of this slide.
    /// </summary>
    private VGElementCollection targets;

    /// <summary>
    /// Flag, indicating a modification of this slide.
    /// </summary>
    private bool modified;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the Slide class.
    /// </summary>
    public Slide()
    {
      this.stimuli = new VGElementCollection();
      this.activeXElements = new VGElementCollection();
      this.targets = new VGElementCollection();
      this.correctResponses = new StopConditionCollection();
      this.links = new StopConditionCollection();
      this.stopConditions = new StopConditionCollection();
      this.category = string.Empty;
      this.TriggerSignal = new Trigger(TriggerSignaling.None, TriggerOutputDevices.LPT, 40, 255, 0x0378);
    }

    /// <summary>
    /// Initializes a new instance of the Slide class as a clone of 
    /// the given slide.
    /// </summary>
    /// <param name="slide">The <see cref="Slide"/> to clone.</param>
    public Slide(Slide slide)
    {
      // Clone stimuli
      this.stimuli = new VGElementCollection();
      foreach (VGElement element in slide.VGStimuli)
      {
        this.stimuli.Add((VGElement)element.Clone());
      }

      // Clone activeXElements
      this.activeXElements = new VGElementCollection();
      foreach (VGElement element in slide.ActiveXStimuli)
      {
        this.activeXElements.Add((VGElement)element.Clone());
      }

      // Clone stop conditions
      this.stopConditions = new StopConditionCollection();
      foreach (StopCondition condition in slide.StopConditions)
      {
        this.stopConditions.Add((StopCondition)condition.Clone());
      }

      // Clone background properties
      this.BackgroundColor = slide.BackgroundColor;
      if (slide.BackgroundImage != null)
      {
        this.BackgroundImage = (Image)slide.BackgroundImage.Clone();
      }

      if (slide.BackgroundSound != null)
      {
        this.BackgroundSound = (AudioFile)slide.BackgroundSound.Clone();
      }

      // Clone trigger
      this.TriggerSignal = slide.TriggerSignal;

      this.IsDesktopSlide = slide.IsDesktopSlide;

      // Clone correct responses
      this.correctResponses = new StopConditionCollection();
      foreach (StopCondition condition in slide.CorrectResponses)
      {
        this.correctResponses.Add((StopCondition)condition.Clone());
      }

      // Clone links
      this.links = new StopConditionCollection();
      foreach (StopCondition condition in slide.Links)
      {
        this.links.Add((StopCondition)condition.Clone());
      }

      // Clone description
      this.name = slide.Name;
      this.category = slide.Category;

      // Clone target areas
      this.targets = new VGElementCollection();
      foreach (VGElement target in slide.TargetShapes)
      {
        this.targets.Add((VGElement)target.Clone());
      }

      // Clone mouse properties
      this.MouseInitialPosition = slide.MouseInitialPosition;
      this.MouseCursorVisible = slide.MouseCursorVisible;
      this.ForceMousePositionChange = slide.ForceMousePositionChange;

      this.presentationSize = slide.presentationSize;
      this.modified = slide.Modified;
    }

    /// <summary>
    /// Initializes a new instance of the Slide class.
    /// </summary>
    /// <param name="newName">A <see cref="string"/> with the unique name of this Slide</param>
    /// <param name="newBackgroundColor">A <see cref="Color"/> with the background color for this slide</param>
    /// <param name="newBackgroundImage">A <see cref="Image"/> with the background image for this slide</param>
    /// <param name="newStopConditions">A <see cref="StopConditionCollection"/> list
    /// of responses for which the slide presentation should stop.</param>
    /// <param name="newResponses">An optional <see cref="StopConditionCollection"/> with responses
    /// that indicate correct answers.</param>
    /// <param name="newCategory">A <see cref="string"/> with an optional category 
    /// that gives an additional slide distinction.</param>
    /// <param name="newPresentationSize">A <see cref="Size"/> with the original 
    /// presentation size of this slide.</param>
    public Slide(
      string newName,
      Color newBackgroundColor,
      Image newBackgroundImage,
      StopConditionCollection newStopConditions,
      StopConditionCollection newResponses,
      string newCategory,
      Size newPresentationSize)
    {
      this.stimuli = new VGElementCollection();
      this.activeXElements = new VGElementCollection();
      this.targets = new VGElementCollection();
      this.links = new StopConditionCollection();
      this.stopConditions = newStopConditions;
      this.BackgroundColor = newBackgroundColor;
      this.BackgroundImage = newBackgroundImage;
      this.name = newName;
      this.correctResponses = newResponses;
      this.category = newCategory;
      this.presentationSize = newPresentationSize;
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Enumerations                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region ENUMS

    /// <summary>
    /// Delegate to call a slide.Draw() asynchrous from another thread
    /// </summary>
    /// <param name="slide">The <see cref="Slide"/> to be drawn.</param>
    /// <param name="g">The <see cref="Graphics"/> to draw the slide to.</param>
    public delegate void AsyncDrawSlideMethodCaller(Slide slide, Graphics g);

    #endregion ENUMS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets or sets the size of slide thumbs.
    /// </summary>
    [XmlIgnore]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public static Size SlideDesignThumbSize
    {
      get { return slideDesignThumbSize; }
      set { slideDesignThumbSize = value; }
    }

    /// <summary>
    /// Gets a value indicating whether one of the slides
    /// contains flash content
    /// </summary>
    [XmlIgnore]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public bool HasActiveXContent
    {
      get
      {
        if (this.activeXElements.Count > 0)
        {
          return true;
        }

        return false;
      }
    }

    /// <summary>
    /// Gets a value indicating whether the current thumb is empty.
    /// </summary>
    [XmlIgnore]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public bool IsThumbNull
    {
      get
      {
        return this.thumb == null;
      }
    }

    /// <summary>
    /// Gets or sets a thumb for this slide.
    /// </summary>
    /// <value>A <see cref="Image"/> with the slides thumb.</value>
    /// <remarks>Because images can not be xml serialized directly,
    /// this property is duplicated by <see cref="SerializedThumb"/>,
    /// which uses <see cref="TypeDescriptor"/> to serialize to bytes.</remarks>
    [XmlIgnore]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public Image Thumb
    {
      get
      {
        if (this.thumb == null)
        {
          this.RecreateThumb();
        }

        return this.thumb;
      }

      set
      {
        this.thumb = value;
      }
    }

    /// <summary>
    /// Gets or sets the SerializedThumb.
    /// Serializes and deserializes the <see cref="Thumb"/> to XML,
    /// because XMLSerializer can not automatically 
    /// serialize <see cref="Image"/> values.
    /// </summary>
    /// <value>A <see cref="Byte"/> array with the slides thumb bytes.</value>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public byte[] SerializedThumb
    {
      get
      {
        if (this.thumb != null)
        {
          TypeConverter bitmapConverter = TypeDescriptor.GetConverter(this.thumb.GetType());
          return (byte[])bitmapConverter.ConvertTo(this.thumb, typeof(byte[]));
        }
        else
        {
          return null;
        }
      }

      set
      {
        if (value != null)
        {
          this.thumb = new Bitmap(new MemoryStream(value));
        }
        else
        {
          this.thumb = null;
        }
      }
    }

    /// <summary>
    /// Gets or sets the original presentation size of
    /// this Slide (used to calculate transformations)
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public Size PresentationSize
    {
      get { return this.presentationSize; }
      set { this.presentationSize = value; }
    }

    /// <summary>
    /// Gets the original size of the stimulus content
    /// on this slide. (That may be greater than the presentation size
    /// during web browsing)
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public Size StimulusSize
    {
      get
      {
        Size stimulusSize = this.PresentationSize;
        foreach (VGElement element in this.VGStimuli)
        {
          if (element is VGScrollImage)
          {
            if (element.Size.ToSize().Width > stimulusSize.Width)
            {
              stimulusSize.Width = (int)element.Size.Width;
            }

            if (element.Size.ToSize().Height > stimulusSize.Height)
            {
              stimulusSize.Height = (int)element.Size.Height;
            }
          }
        }

        return stimulusSize;
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the slide is modified
    /// </summary>
    /// <value>A <see cref="Boolean"/> indication the modification
    /// state of this slide.</value>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public bool Modified
    {
      get { return this.modified; }
      set { this.modified = value; }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the slide is modified
    /// a desktop slide, that means invisible to the user showing
    /// the underlying desktop during recording
    /// </summary>
    /// <value>A <see cref="Boolean"/> indicating that this is
    /// a desktop slide.</value>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public bool IsDesktopSlide { get; set; }

    /// <summary>
    /// Gets or sets the list of vector graphic stimuli for this slide.
    /// </summary>
    /// <value>A <see cref="VGElementCollection"/> with the vector graphic
    /// elements.</value>
    /// <remarks>This elements include all <see cref="VGElement"/> derived classes,
    /// which are at the moment: <see cref="VGCursor"/>,<see cref="VGLine"/>,
    /// <see cref="VGEllipse"/>, <see cref="VGImage"/>, <see cref="VGPolyline"/>,
    /// <see cref="VGRectangle"/>, <see cref="VGText"/>, <see cref="VGRichText"/>.</remarks>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public VGElementCollection VGStimuli
    {
      get
      {
        if (this.stimuli == null)
        {
          this.stimuli = new VGElementCollection();
        }

        return this.stimuli;
      }

      set
      {
        this.stimuli = value;
      }
    }

    /// <summary>
    /// Gets or sets the list of vector graphic stimuli containing
    /// activeX controls like <see cref="VGFlash"/>
    /// </summary>
    /// <value>A <see cref="VGElementCollection"/> with the vector graphic
    /// elements that contain activeX controls</value>
    /// <remarks>This elements include at the moment: <see cref="VGFlash"/>.</remarks>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public VGElementCollection ActiveXStimuli
    {
      get
      {
        return this.activeXElements ?? (this.activeXElements = new VGElementCollection());
      }

      set
      {
        this.activeXElements = value;
      }
    }

    /// <summary>
    /// Gets or sets a unique name for this Slide.
    /// </summary>
    /// <value>A <see cref="string"/> with the unique slide name.</value>
    [Category("General")]
    [Description("The name of this slide. Should be unique.")]
    [ReadOnly(true)]
    public string Name
    {
      get { return this.name; }
      set { this.name = value; }
    }

    /// <summary>
    /// Gets or sets a category of this <see cref="Slide"/>.
    /// </summary>
    /// <value>A <see cref="string"/> with a slide category.</value>
    /// <remarks>Can be for example "BlankSlide" or "MultipleChoiceQuestion" etc.</remarks>
    [Category("General")]
    [Description("A string for categorization of trials. Can be used to restrict the statistical analysis to a certain category.")]
    public string Category
    {
      get { return this.category; }
      set { this.category = value; }
    }

    /// <summary>
    /// Gets or sets the list of conditions that end this slide. 
    /// Can be of any <see cref="StopCondition"/> type.
    /// </summary>
    /// <value>A <see cref="StopConditionCollection"/> with a list of <see cref="StopCondition"/>s.</value>
    /// <remarks>Currently three <see cref="StopCondition"/> types
    /// are known: <see cref="TimeStopCondition"/>,
    /// <see cref="MouseStopCondition"/> and <see cref="KeyStopCondition"/>.</remarks>
    [Category("Timing")]
    [Description("A list of conditions under which the slide is closed.")]
    public StopConditionCollection StopConditions
    {
      get
      {
        return this.stopConditions ?? (this.stopConditions = new StopConditionCollection());
      }

      set
      {
        this.stopConditions = value;
      }
    }

    /// <summary>
    /// Gets or sets the background color of the current slide.
    /// </summary>
    /// <value>A <see cref="Color"/> with the slides background color.</value>
    [XmlIgnore, Category("Background"), Description("The background color of this slide.")]
    public Color BackgroundColor { get; set; }

    /// <summary>
    /// Gets or sets the SerializedBackgroundColor.
    /// Serializes and deserializes the <see cref="BackgroundColor"/> to XML,
    /// because XMLSerializer can not automatically 
    /// serialize <see cref="Color"/> values.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public string SerializedBackgroundColor
    {
      get { return ObjectStringConverter.ColorToHtmlAlpha(this.BackgroundColor); }
      set { this.BackgroundColor = ObjectStringConverter.HtmlAlphaToColor(value); }
    }

    /// <summary>
    /// Gets or sets the background image of the slide.
    /// </summary>
    /// <value>A <see cref="Image"/> with the background image.</value>
    /// <remarks>Because this property can not be xml serialized,
    /// there is the <see cref="SerializedBackgroundImage"/>
    /// property from which this property can be persisted.</remarks>
    [XmlIgnore, Category("Background"), Description("A background image for this slide.")]
    public Image BackgroundImage { get; set; }

    /// <summary>
    /// Gets or sets the SerializedBackgroundImage.
    /// Serializes and deserializes the <see cref="BackgroundImage"/> to XML,
    /// because XMLSerializer can not automatically 
    /// serialize <see cref="Image"/> values.
    /// </summary>
    /// <value>A <see cref="Byte"/> array with the background image bytes.</value>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public byte[] SerializedBackgroundImage
    {
      get
      {
        if (this.BackgroundImage != null)
        {
          var bitmapConverter = TypeDescriptor.GetConverter(this.BackgroundImage.GetType());
          return (byte[])bitmapConverter.ConvertTo(this.BackgroundImage, typeof(byte[]));
        }

        return null;
      }

      set
      {
        if (value != null)
        {
          this.BackgroundImage = new Bitmap(new MemoryStream(value));
        }
        else
        {
          this.BackgroundImage = null;
        }
      }
    }

    /// <summary>
    /// Gets or sets an <see cref="AudioFile"/> for the background sound.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
    public AudioFile BackgroundSound { get; set; }

    /// <summary>
    /// Gets or sets the device trigger signal element, that can be used
    /// to send a trigger to other devices during presentation.
    /// </summary>
    [Category("Options"), Description("An optional trigger that can be sent to connected devices during presentation.")]
    public Trigger TriggerSignal { get; set; }

    /// <summary>
    /// Gets or sets the mouse or key that is the correct 
    /// answer to a slides question.
    /// </summary>
    /// <value>A <see cref="StopConditionCollection"/> with the correct responses to the slide.</value>
    [Category("Testing")]
    [Description("An optional condition for testing. Should contain the correct response to this slide.")]
    public StopConditionCollection CorrectResponses
    {
      get
      {
        return this.correctResponses ?? (this.correctResponses = new StopConditionCollection());
      }

      set
      {
        this.correctResponses = value;
      }
    }

    /// <summary>
    /// Gets or sets the collection of links from this slide
    /// to other trials.
    /// </summary>
    /// <value>A <see cref="StopConditionCollection"/> with the
    /// links to other Trial s.</value>
    [Category("Links")]
    [Description("An optional condition for linking to other trials.")]
    public StopConditionCollection Links
    {
      get
      {
        return this.links ?? (this.links = new StopConditionCollection());
      }

      set
      {
        this.links = value;
      }
    }

    /// <summary>
    /// Gets or sets a <see cref="VGElementCollection"/> with the shapes 
    /// that define target areas for a response mouse event that is 
    /// accepted to be a valid response.
    /// </summary>
    /// <value>A <see cref="VGElementCollection"/> that with the  target areas of this slide.</value>
    [Category("Testing")]
    [Description("An optional list of graphical elements that define target areas of this slide.")]
    public VGElementCollection TargetShapes
    {
      get
      {
        return this.targets ?? (this.targets = new VGElementCollection());
      }

      set
      {
        this.targets = value;
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the mouse cursor is visible during
    /// presentation of this slide.
    /// </summary>
    /// <value>A <see cref="Boolean"/> value. 
    /// <strong>True</strong> if mouse cursor should be visible during 
    /// presentation of this slide.</value>
    [Category("Mouse"), Description("Defines the visibility of the mouse cursor during presentation.")]
    public bool MouseCursorVisible { get; set; }

    /// <summary>
    /// Gets or sets the mouse cursors initial position.
    /// </summary>
    /// <value>A <see cref="Point"/> with the initial position of the mouse cursor.</value>
    [Category("Mouse"), Description("The initial position of the mouse cursor, if it is visible.")]
    public Point MouseInitialPosition { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to let the mouse cursor
    /// at its current location during slide change
    /// </summary>
    /// <value>A <see cref="Boolean"/> indicating whether to let the mouse cursor
    /// at its current location during slide change</value>
    [Category("Mouse"), Description("False if the position of the mouse cursor should not change on slide change.")]
    public bool ForceMousePositionChange { get; set; }

    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Public methods                                                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region PUBLICMETHODS

    /// <summary>
    /// Draw the slide onto the given graphics in the background thread.
    /// </summary>
    /// <param name="slide">A <see cref="Slide"/> that should be drawn.</param>
    /// <param name="g">A <see cref="Graphics"/> on which the slide should be drawn.</param>
    public static void DrawSlideAsync(Slide slide, Graphics g)
    {
      slide.Draw(g);
    }

    /// <summary>
    /// <see cref="ICloneable"/> interface implementation.
    /// Creates a new object that is a copy of the current instance. 
    /// </summary>
    /// <returns>A new object that is a copy of this <see cref="Slide"/>.</returns>
    public object Clone()
    {
      return this.CloneCore();
    }

    /// <summary>
    /// Releases the resources used by the slide.
    /// </summary>
    public void Dispose()
    {
      if (this.thumb != null)
      {
        this.thumb.Dispose();
        this.thumb = null;
      }

      if (this.BackgroundImage != null)
      {
        // this.bgImage.Dispose();
      }

      foreach (VGElement element in this.VGStimuli)
      {
        element.Dispose();
      }

      foreach (VGElement element in this.ActiveXStimuli)
      {
        element.Dispose();
      }

      foreach (VGElement element in this.TargetShapes)
      {
        element.Dispose();
      }
    }

    /// <summary>
    /// Drawing method. Draws the whole slide content to the given
    /// graphics.
    /// </summary>
    /// <param name="graphics">A <see cref="Graphics"/> to render to.</param>
    /// <exception cref="ArgumentNullException">Thrown, when graphics object is null.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Thrown, when <see cref="PresentationSize"/> is empty.</exception>
    public void Draw(Graphics graphics)
    {
      if (graphics == null)
      {
        throw new ArgumentNullException("Graphics object should not be null.");
      }

      graphics.SmoothingMode = SmoothingMode.AntiAlias;
      if (this.presentationSize == Size.Empty)
      {
        throw new ArgumentOutOfRangeException("The presentation size of the slide is empty.");
      }

      if (this.BackgroundColor != null)
      {
        graphics.Clear(this.BackgroundColor);
      }

      if (this.BackgroundImage != null)
      {
        graphics.DrawImage(
          this.BackgroundImage,
          0,
          0,
          this.presentationSize.Width,
          this.presentationSize.Height);
      }

      foreach (VGElement element in this.stimuli)
      {
        element.Draw(graphics);
      }

      foreach (VGElement element in this.activeXElements)
      {
        element.Draw(graphics);
      }
    }

    /// <summary>
    /// Removes all vector graphic stimuli from the slide.
    /// </summary>
    public void RemoveVGElements()
    {
      foreach (VGElement element in this.stimuli)
      {
        element.Dispose();
      }

      this.stimuli.Clear();
    }

    /// <summary>
    /// Removes all ActiveX stimuli from the slide.
    /// </summary>
    public void RemoveActiveXElements()
    {
      foreach (VGElement element in this.activeXElements)
      {
        element.Dispose();
      }

      this.activeXElements.Clear();
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
    /// Creates a thumbnail for the given flash by invoking a dialog,
    /// loads the movie, captures the surface, closes the dialog.
    /// </summary>
    /// <param name="filename">Filename with full path to .swf file</param>
    /// <param name="thumbSize">The <see cref="Size"/> for the thumb.</param>
    /// <returns>An <see cref="Image"/> with the thumbnail image.</returns>
    private static Image CreateFlashThumb(string filename, Size thumbSize)
    {
      FlashCapture dlg = new FlashCapture(thumbSize, filename);
      dlg.ShowDialog();
      Image thumb = dlg.ScreenShot;
      return thumb;
    }

    /// <summary>
    /// Creates a thumbnail for the given website by invoking a dialog,
    /// loads the url, captures the surface, closes the dialog.
    /// </summary>
    /// <param name="url">URL of browser location</param>
    /// <param name="presentationSize">The <see cref="Size"/> for the thumb.</param>
    /// <returns>An <see cref="Image"/> with the thumbnail image.</returns>
    private static Image CreateBrowserThumb(string url, Size presentationSize)
    {
      Bitmap bmp = WebsiteThumbnailGenerator.GetWebSiteThumbnail(
        url,
        presentationSize.Width,
        presentationSize.Height,
        presentationSize);

      return bmp;
    }

    /// <summary>
    /// Clones given slide object.
    /// </summary>
    /// <returns>A <see cref="Slide"/> with a clone of the current slide.</returns>
    private Slide CloneCore()
    {
      return new Slide(this);
    }

    /// <summary>
    /// This method creates a new thumb for this slide.
    /// </summary>
    private void RecreateThumb()
    {
      Bitmap newThumb = new Bitmap(slideDesignThumbSize.Width, slideDesignThumbSize.Height);

      using (Graphics g = Graphics.FromImage(newThumb))
      {
        float factorX = (float)newThumb.Width / this.presentationSize.Width;
        float factorY = (float)newThumb.Height / this.presentationSize.Height;
        if (factorX != 0 && factorY != 0)
        {
          g.ScaleTransform(factorX, factorY);
        }

        this.Draw(g);

        foreach (VGElement element in this.ActiveXStimuli)
        {
          if (element is VGFlash)
          {
            VGFlash flash = element as VGFlash;
            if (File.Exists(flash.FullFilename))
            {
              Image flashThumb = CreateFlashThumb(flash.FullFilename, flash.Size.ToSize());
              g.DrawImage(flashThumb, flash.Location);
            }
          }
          else if (element is VGBrowser)
          {
            VGBrowser browser = element as VGBrowser;
            Image browserThumb = CreateBrowserThumb(browser.BrowserURL, this.presentationSize);
            g.DrawImage(browserThumb, Point.Empty);
          }
        }
      }

      newThumb.Tag = this.Name;

      this.thumb = newThumb;
    }

    #endregion //METHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER
    #endregion //HELPER
  }
}