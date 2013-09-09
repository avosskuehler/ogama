// <copyright file="AudioFile.cs" company="FU Berlin">
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

namespace VectorGraphics.Tools
{
  using System;
  using System.Collections.Generic;
  using System.IO;
  using System.Text;
  using System.Xml.Serialization;

  /// <summary>
  /// This class is a wrapper for an audio file with replay properties.
  /// Implements <see cref="ICloneable"/>
  /// </summary>
  [Serializable]
  public class AudioFile : ICloneable
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
    /// Initializes a new instance of the AudioFile class.
    /// </summary>
    public AudioFile()
    {
      this.Filepath = string.Empty;
      this.Filename = string.Empty;
      this.Loop = false;
      this.ShouldPlay = false;
      this.ShowOnClick = false;
    }

    /// <summary>
    /// Initializes a new instance of the AudioFile class.
    /// Clone Constructor. Creates new <see cref="AudioFile"/> that is
    /// identical to the given <see cref="AudioFile"/>.
    /// </summary>
    /// <param name="audioFileToClone">The <see cref="AudioFile"/> to clone.</param>
    private AudioFile(AudioFile audioFileToClone)
    {
      this.Filename = audioFileToClone.Filename;
      this.Filepath = audioFileToClone.Filepath;
      this.Loop = audioFileToClone.Loop;
      this.ShouldPlay = audioFileToClone.ShouldPlay;
      this.ShowOnClick = audioFileToClone.ShowOnClick;
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
    /// Gets or sets the filename without path to the audio file.
    /// </summary>
    public string Filename { get; set; }

    /// <summary>
    /// Gets or sets the path to the audio file.
    /// </summary>
    [XmlIgnore]
    public string Filepath { get; set; }

    /// <summary>
    /// Gets the audio filename with path.
    /// </summary>
    [XmlIgnore]
    public string FullFilename
    {
      get
      {
        if (this.Filepath == null)
        {
          string newPath = Path.GetDirectoryName(this.Filename);
          if (newPath != string.Empty)
          {
            this.Filepath = newPath;
            this.Filename = Path.GetFileName(this.Filename);
          }
          else
          {
            throw new ArgumentNullException("Audio file with empty path. File not found.");
          }
        }

        return Path.Combine(this.Filepath, this.Filename);
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether this file should be played or not.
    /// </summary>
    public bool ShouldPlay { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this audio files should be looped during replay.
    /// </summary>
    public bool Loop { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the file should only 
    /// be played when the object it belongs to
    /// is clicked, otherwise the file will be played immediately.
    /// </summary>
    public bool ShowOnClick { get; set; }

    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Public methods                                                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region PUBLICMETHODS

    /// <summary>
    /// Creates an exact copy of given audio file.
    /// </summary>
    /// <returns>Exact copy of this audio file.</returns>
    public object Clone()
    {
      return new AudioFile(this);
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
