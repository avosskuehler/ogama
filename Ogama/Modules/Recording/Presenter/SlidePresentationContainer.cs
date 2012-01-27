// <copyright file="SlidePresentationContainer.cs" company="alea technologies">
// ******************************************************
// OGAMA - open gaze and mouse analyzer 
// Copyright (C) 2012 Adrian Voßkühler  
// ------------------------------------------------------------------------
// This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
// This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
// You should have received a copy of the GNU General Public License along with this program; if not, write to the Free Software Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
// **************************************************************
// </copyright>
// <author>Adrian Voßkühler</author>
// <email>adrian@ogama.net</email>

namespace Ogama.Modules.Recording.Presenter
{
  using System;
  using System.Drawing;
  using System.Windows.Forms;
  using Ogama.ExceptionHandling;
  using Ogama.Modules.Common;
  using Ogama.Modules.Common.SlideCollections;

  using OgamaControls;
  using VectorGraphics.Controls;
  using VectorGraphics.Controls.Timer;
  using VectorGraphics.Elements;
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
    /// Gets or sets the DirectShow capture class for getting
    /// a video out of screenshots during recording
    /// of dynamic flash movie content.
    /// </summary>
    public DSScreenCapture ScreenCapture { get; set; }

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

    /// <summary>
    /// This method should be called after the <see cref="CaptureDeviceProperties"/>
    /// for the screen capturing are set to initialize the <see cref="DSScreenCapture"/>
    /// one time.
    /// </summary>
    /// <param name="screenCaptureProperties">The <see cref="ScreenCaptureProperties"/>
    /// to be used for the screen capturing.</param>
    public void InitializeScreenCapture(ScreenCaptureProperties screenCaptureProperties)
    {
      this.ScreenCapture = new DSScreenCapture(
        screenCaptureProperties.VideoCompressor,
        screenCaptureProperties.FrameRate,
        screenCaptureProperties.MonitorIndex);
      this.ScreenCapture.PreviewWindow = screenCaptureProperties.PreviewWindow;
    }

    #endregion //PRIVATEMETHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER
    #endregion //HELPER
  }
}
