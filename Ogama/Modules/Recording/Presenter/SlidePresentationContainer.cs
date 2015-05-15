// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SlidePresentationContainer.cs" company="FU Berlin">
//   OGAMA - open gaze and mouse analyzer 
//   Copyright (C) 2015 Dr. Adrian Voßkühler  
//   Licensed under GPL V3
// </copyright>
// <author>Adrian Voßkühler</author>
// <email>adrian@ogama.net</email>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Ogama.Modules.Recording.Presenter
{
  using System.Drawing;
  using System.Windows.Forms;

  using Ogama.Modules.Common.SlideCollections;

  using OgamaControls;

  using VectorGraphics.Controls.Timer;
  using VectorGraphics.Elements.ElementCollections;

  /// <summary>
  /// This class contains all objects that are needed to prepare
  /// the slide in the background and present it when
  /// it should be shown.
  /// </summary>
  public class SlidePresentationContainer
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
    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the SlidePresentationContainer class.
    /// </summary>
    public SlidePresentationContainer()
    {
      this.Timer = new MultimediaTimer();
      this.ElementsWithAudioOnClick = new VGElementCollection();
      this.AudioPlayer = new AudioPlayer();
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Enumerations                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region ENUMS
    #endregion ENUMS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets or sets the <see cref="Control"/> that is the parent
    /// of this <see cref="SlidePresentationContainer"/>
    /// </summary>
    public Control ContainerControl { get; set; }

    /// <summary>
    /// Gets or sets the the trial that owns the slide of this container
    /// </summary>
    public Trial Trial { get; set; }

    /// <summary>
    /// Gets or sets the slide of this container.
    /// </summary>
    public Slide Slide { get; set; }

    /// <summary>
    /// Gets or sets the elements that play audio files on click for each slide.
    /// </summary>
    public VGElementCollection ElementsWithAudioOnClick { get; set; }

    /// <summary>
    /// Gets or sets the audio player used to play all sounds.
    /// </summary>
    public AudioPlayer AudioPlayer { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="BufferedGraphics"/> 
    /// that is the drawing surface on which the slide contents
    /// should be rendered an presented to the graphics
    /// object.
    /// </summary>
    public BufferedGraphics DrawingSurface { get; set; }

    /// <summary>
    /// Gets or sets the precise <see cref="MultimediaTimer"/> that is run
    /// when the slide display starts and a TimeStopCondition
    /// is set, to raise the tick event when slide should be changed.
    /// </summary>
    public MultimediaTimer Timer { get; set; }

    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Public methods                                                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region PUBLICMETHODS
    #endregion //PUBLICMETHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Inherited methods                                                         //
    ///////////////////////////////////////////////////////////////////////////////
    #region OVERRIDES
    #endregion //OVERRIDES

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
    // Methods for doing main class job                                          //
    ///////////////////////////////////////////////////////////////////////////////
    #region PRIVATEMETHODS
    #endregion //PRIVATEMETHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER
    #endregion //HELPER
  }
}
