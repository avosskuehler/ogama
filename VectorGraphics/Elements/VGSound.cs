// <copyright file="VGSound.cs" company="FU Berlin">
// ******************************************************
// OGAMA - open gaze and mouse analyzer 
// Copyright (C) 2015 Dr. Adrian Voßkühler  
// ------------------------------------------------------------------------
// This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
// This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
// You should have received a copy of the GNU General Public License along with this program; if not, write to the Free Software Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
// **************************************************************
// </copyright>
// <author>Adrian Voßkühler</author>
// <email>adrian@ogama.net</email>

namespace VectorGraphics.Elements
{
  using System;
  using System.Drawing;
  using System.Text;

  /// <summary>
  /// Inherited from <see cref="VGElement"/>. Creates new sound element that
  /// is only visible in edit mode but is clickable.
  /// </summary>
  [Serializable]
  public class VGSound : VGElement
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
    /// Indicates whether this object is in design mode.
    /// If it is a sound player image will be shown at the center of the bounds.
    /// </summary>
    private bool designMode;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the VGSound class.
    /// </summary>
    /// <param name="newShapeDrawAction">Drawing action: Edge, Fill, Both</param>
    /// <param name="newPen">Pen for edges</param>
    /// <param name="newBounds">Bounds of element</param>
    public VGSound(ShapeDrawAction newShapeDrawAction, Pen newPen, Rectangle newBounds)
      : base(newShapeDrawAction, newPen)
    {
      this.Bounds = newBounds;
    }

    /// <summary>
    /// Prevents a default instance of the VGSound class from being created.
    /// Parameterless constructor. Used for serialization.
    /// </summary>
    private VGSound()
    {
    }

    /// <summary>
    /// Initializes a new instance of the VGSound class.
    /// Clone Constructor. Creates new sound that is
    /// identical to the given <see cref="VGSound"/>
    /// </summary>
    /// <param name="sound">AudioFile to clone</param>
    private VGSound(VGSound sound)
      : base(
      sound.ShapeDrawAction,
      sound.Pen,
      sound.Brush,
      sound.Font,
      sound.FontColor,
      sound.Bounds,
      sound.StyleGroup,
      sound.Name,
      sound.ElementGroup,
      sound.Sound)
    {
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets or sets a value indicating whether this object is in design mode.
    /// If it is a sound player image will be shown at the center of the bounds.
    /// </summary>
    public bool DesignMode
    {
      get { return this.designMode; }
      set { this.designMode = value; }
    }

    #endregion //PROPERTIES

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

    /// <summary>
    /// Overridden <see cref="VGElement.Draw(Graphics)"/> method. 
    /// If in <see cref="DesignMode"/>, draws a sound bitmap at the top
    /// left-corner of the bounds.
    /// </summary>
    /// <param name="graphics">Graphics context to draw to</param>
    /// <exception cref="ArgumentNullException">Thrown, when graphics object is null.</exception>
    public override void Draw(Graphics graphics)
    {
      if (graphics == null)
      {
        throw new ArgumentNullException("Graphics object should not be null.");
      }

      if (this.designMode)
      {
        graphics.DrawImageUnscaled(Properties.Resources.sound, Rectangle.Round(this.Bounds));
      }

      // Draw selection frame and name if applicable
      base.Draw(graphics);
    }

    /// <summary>
    /// Overridden <see cref="Object.ToString()"/> method.
    /// Returns the <see cref="VGSound"/> properties as a human readable string.
    /// </summary>
    /// <returns>A <see cref="string"/> that represents this <see cref="VGSound"/>.</returns>
    public override string ToString()
    {
      StringBuilder sb = new StringBuilder();
      sb.Append("VGSound, File: ");
      sb.Append(this.Sound.Filename);
      sb.Append(" ; Loop: ");
      sb.Append(this.Sound.Loop.ToString());
      sb.Append(" ; ShowOnClick: ");
      sb.Append(this.Sound.ShowOnClick);
      return sb.ToString();
    }

    /// <summary>
    /// Overridden <see cref="VGElement.ToShortString()"/> method.
    /// Returns the main <see cref="VGSound"/> properties as a human readable string.
    /// </summary>
    /// <returns>A <see cref="string"/> that represents the 
    /// current <see cref="VGSound"/> in short form with its main properties.</returns>
    public override string ToShortString()
    {
      StringBuilder sb = new StringBuilder();
      sb.Append("Sound ");
      string text = this.Sound.Filename;
      sb.Append(text.Substring(0, text.Length > 12 ? Math.Max(12, text.Length - 1) : text.Length));
      sb.Append(" ...");
      return sb.ToString();
    }

    /// <summary>
    /// Overridden <see cref="VGElement.CloneCore()"/> method. 
    /// Creates a excact copy of given sound
    /// </summary>
    /// <returns>Excact copy of this rectangle</returns>
    protected override VGElement CloneCore()
    {
      return new VGSound(this);
    }

    #endregion //OVERRIDES

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
