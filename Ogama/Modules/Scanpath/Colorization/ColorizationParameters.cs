// <copyright file="ColorizationParameters.cs" company="FU Berlin">
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

namespace Ogama.Modules.Scanpath.Colorization
{
  using System;
  using System.Collections.Generic;
  using System.Xml.Serialization;

  using Ogama.Modules.Common.Types;

  using OgamaControls;

  /// <summary>
  /// This class encapsulates a list of properties that are used
  /// to colorize and display a subjects scanpath.
  /// It uses two lists to allow categorized and subject specific colorization.
  /// </summary>
  [Serializable]
  public class ColorizationParameters : ICloneable
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
    /// Saves the current subject-color assignment.
    /// </summary>
    private XMLSerializableDictionary<string, ColorizationStyle> subjectStyles;

    /// <summary>
    /// Saves the current category-color assignment.
    /// </summary>
    private XMLSerializableDictionary<string, ColorizationStyle> categoryStyles;

    /// <summary>
    /// Saves the current colorization mode for the subjects.
    /// </summary>
    private ColorizationModes colorizationMode;

    /// <summary>
    /// Saves the gradient used for colorization
    /// </summary>
    [XmlIgnore]
    private Gradient colorizationGradient;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the ColorizationParameters class.
    /// </summary>
    public ColorizationParameters()
    {
      this.subjectStyles = new XMLSerializableDictionary<string, ColorizationStyle>();
      this.categoryStyles = new XMLSerializableDictionary<string, ColorizationStyle>();
      this.colorizationMode = ColorizationModes.Gradient;
    }

    /// <summary>
    /// Initializes a new instance of the ColorizationParameters class.
    /// </summary>
    /// <param name="parametersToClone">The <see cref="ColorizationParameters"/>
    /// to clone.</param>
    public ColorizationParameters(ColorizationParameters parametersToClone)
    {
      this.subjectStyles = new XMLSerializableDictionary<string, ColorizationStyle>();
      foreach (KeyValuePair<string, ColorizationStyle> kvp in parametersToClone.SubjectStyles)
      {
        this.subjectStyles.Add(kvp.Key, (ColorizationStyle)kvp.Value.Clone());
      }

      this.categoryStyles = new XMLSerializableDictionary<string, ColorizationStyle>();
      foreach (KeyValuePair<string, ColorizationStyle> kvp in parametersToClone.CategoryStyles)
      {
        this.categoryStyles.Add(kvp.Key, (ColorizationStyle)kvp.Value.Clone());
      }

      this.colorizationGradient = parametersToClone.ColorizationGradient != null ? (Gradient)parametersToClone.ColorizationGradient.Clone() : null;
      this.colorizationMode = parametersToClone.ColorizationMode;
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
    /// Gets or sets the current subject-color assignment.
    /// </summary>
    public XMLSerializableDictionary<string, ColorizationStyle> SubjectStyles
    {
      get { return this.subjectStyles; }
      set { this.subjectStyles = value; }
    }

    /// <summary>
    /// Gets or sets the current category-color assignment.
    /// </summary>
    public XMLSerializableDictionary<string, ColorizationStyle> CategoryStyles
    {
      get { return this.categoryStyles; }
      set { this.categoryStyles = value; }
    }

    /// <summary>
    /// Gets or sets the current colorization mode for the subjects.
    /// </summary>
    public ColorizationModes ColorizationMode
    {
      get { return this.colorizationMode; }
      set { this.colorizationMode = value; }
    }

    /// <summary>
    /// Gets or sets the gradient used for colorization
    /// </summary>
    [XmlIgnore]
    public Gradient ColorizationGradient
    {
      get { return this.colorizationGradient; }
      set { this.colorizationGradient = value; }
    }

    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Public methods                                                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region PUBLICMETHODS

    /// <summary>
    /// Implements ICloneable by copying all the contents.
    /// </summary>
    /// <returns>An <see cref="Object"/> with a copy of the current <see cref="ColorizationParameters"/></returns>
    public object Clone()
    {
      return new ColorizationParameters(this);
    }

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