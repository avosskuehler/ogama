// <copyright file="Trial.cs" company="FU Berlin">
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

namespace Ogama.Modules.Common.SlideCollections
{
  using System;
  using System.Collections.Generic;

  using Ogama.Modules.Common.Types;

  using VectorGraphics.Elements;
  using VectorGraphics.Elements.ElementCollections;
  using VectorGraphics.Tools.Interfaces;

  /// <summary>
  /// The Trial class is a <see cref="List{Slide}"/> that implements <see cref="IClonableNamedObject"/>
  /// and <see cref="IObjectWithID"/> so it is cloneable, named and owns a unique identifier.
  /// <strong>Trials</strong> are the basis of Ogamas analysis. They can consist of
  /// a number of <see cref="Slide"/> but by default contain only one <see cref="Slide"/>
  /// to make analysis as fine grained as possible.
  /// </summary>
  [Serializable]
  public class Trial : List<Slide>, IClonableNamedObject, IObjectWithID
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
    /// Saves the trial name.
    /// </summary>
    private string name;

    /// <summary>
    /// Saves the trials unique identifier.
    /// </summary>
    private int id;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the Trial class.
    /// </summary>
    public Trial()
    {
      this.name = "Trial";
      this.id = 0;
    }

    /// <summary>
    /// Initializes a new instance of the Trial class by cloneing the given <see cref="Trial"/>
    /// </summary>
    /// <param name="newTrial">A <see cref="Trial"/> with the trial to clone.</param>
    public Trial(Trial newTrial)
    {
      this.name = newTrial.Name;
      this.id = newTrial.ID;
      foreach (Slide slide in newTrial)
      {
        this.Add((Slide)slide.Clone());
      }
    }

    /// <summary>
    /// Initializes a new instance of the Trial class with name and ID.
    /// </summary>
    /// <param name="newName">A <see cref="string"/> with the trials name.</param>
    /// <param name="newId">An <see cref="int"/> with the trials unique identifier.</param>
    public Trial(string newName, int newId)
    {
      this.name = newName;
      this.id = newId;
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets or sets the trials name.
    /// </summary>
    /// <value>A <see cref="string"/> with the name for this trial.</value>
    public string Name
    {
      get { return this.name; }
      set { this.name = value; }
    }

    /// <summary>
    /// Gets or sets the trials unique identifier.
    /// </summary>
    /// <value>An <see cref="int"/> with a unique id.</value>
    public int ID
    {
      get { return this.id; }
      set { this.id = value; }
    }

    /// <summary>
    /// Gets a value indicating whether one of the slides
    /// contains flash content
    /// </summary>
    public bool HasActiveXContent
    {
      get
      {
        foreach (Slide slide in this)
        {
          if (slide.HasActiveXContent)
          {
            return true;
          }
        }

        return false;
      }
    }

    /// <summary>
    /// Gets a value indicating whether one of the slides
    /// is a desktop recording slide.
    /// </summary>
    public bool HasDesktopRecordingContent
    {
      get
      {
        foreach (Slide slide in this)
        {
          if (slide.IsDesktopSlide)
          {
            return true;
          }
        }

        return false;
      }
    }

    /// <summary>
    /// Gets a value indicating whether one of the slides
    /// contains flash content
    /// </summary>
    public bool HasFlashContent
    {
      get
      {
        foreach (Slide slide in this)
        {
          int flashCount = 0;
          foreach (VGElement element in slide.ActiveXStimuli)
          {
            if (element is VGFlash)
            {
              flashCount++;
            }
          }

          if (flashCount > 0)
          {
            return true;
          }
        }

        return false;
      }
    }

    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Public methods                                                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region PUBLICMETHODS

    /// <summary>
    /// Clones this trial object.
    /// </summary>
    /// <returns>A clone of the current <see cref="Trial"/>.</returns>
    public object Clone()
    {
      return new Trial(this);
    }

    #endregion //PUBLICMETHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Inherited methods                                                         //
    ///////////////////////////////////////////////////////////////////////////////
    #region OVERRIDES

    /// <summary>
    /// Overridden. Returns the name of the trial.
    /// </summary>
    /// <returns>A <see cref="string"/> with the name of the trial.</returns>
    public override string ToString()
    {
      return this.name;
    }

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
    #region METHODS
    #endregion //METHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER
    #endregion //HELPER
  }
}
