// <copyright file="SlideChangedEventArgs.cs" company="FU Berlin">
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

namespace Ogama.Modules.Common.CustomEventArgs
{
  using System;

  using VectorGraphics.Elements.ElementCollections;
  using VectorGraphics.StopConditions;

  /// <summary>
  /// Delegate. Handles slide changed event.
  /// </summary>
  /// <param name="sender">Source of the event.</param>
  /// <param name="e">A <see cref="SlideChangedEventArgs"/> with event arguments</param>
  public delegate void SlideChangedEventHandler(object sender, SlideChangedEventArgs e);

  /// <summary>
  /// Derived from <see cref="System.EventArgs"/>
  /// Class that contains the data for the slide changed event.
  /// </summary>
  public class SlideChangedEventArgs : EventArgs
  {
    ///////////////////////////////////////////////////////////////////////////////
    // Defining Variables, Enumerations, Events                                  //
    ///////////////////////////////////////////////////////////////////////////////
    #region FIELDS

    /// <summary>
    /// The <see cref="Slide"/> that is the new slide to show.
    /// </summary>
    private readonly Slide nextSlide;

    /// <summary>
    /// The response that ended the slide as a <see cref="StopCondition"/> value.
    /// </summary>
    private readonly StopCondition response;

    /// <summary>
    /// This member saves the index of the current visible slide
    /// in the current trial.
    /// </summary>
    private readonly int slideCounter;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the SlideChangedEventArgs class.
    /// </summary>
    /// <param name="newNextSlide">A <see cref="Slide"/> The <see cref="Slide"/> that is the new slide to show.</param>
    /// <param name="newResponse">The response of type <see cref="StopCondition"/>
    /// that ended the trial.</param>
    /// <param name="newSlideCounter">The index of the current visible slide
    /// in the current trial.</param>
    public SlideChangedEventArgs(Slide newNextSlide, StopCondition newResponse, int newSlideCounter)
    {
      this.nextSlide = newNextSlide;
      this.response = newResponse;
      this.slideCounter = newSlideCounter;
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets the <see cref="Slide"/> that is the new slide to show.
    /// </summary>
    public Slide NextSlide
    {
      get { return this.nextSlide; }
    }

    /// <summary>
    /// Gets the current response.
    /// </summary>
    /// <value>The response as a <see cref="StopCondition"/> value.</value>
    public StopCondition Response
    {
      get { return this.response; }
    }

    /// <summary>
    /// Gets the index of the current visible slide
    /// in the current trial.
    /// </summary>
    public int SlideCounter
    {
      get { return this.slideCounter; }
    }

    #endregion //PROPERTIES
  }
}
