// <copyright file="ColorizationStyle.cs" company="FU Berlin">
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
  using System.ComponentModel;
  using System.Drawing;
  using System.Xml.Serialization;

  using VectorGraphics.Tools.CustomTypeConverter;

  /// <summary>
  /// A class to save the drawing style of a subject.
  /// This class is XMLSerializable
  /// </summary>
  [Serializable]
  public class ColorizationStyle : ICloneable
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
    /// The pen for fixation circles or dots.
    /// </summary>
    private Pen fixationPen;

    /// <summary>
    /// The pen for fixation connections.
    /// </summary>
    private Pen connectionPen;

    /// <summary>
    /// The font to use for element names.
    /// </summary>
    private Font font;

    /// <summary>
    /// The color to use during writing.
    /// </summary>
    private Color fontColor;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the ColorizationStyle class.
    /// Parameterless constructor. Needed for serialization.
    /// </summary>
    public ColorizationStyle()
    {
    }

    /// <summary>
    /// Initializes a new instance of the ColorizationStyle class.
    /// </summary>
    /// <param name="newFixationPen">The new fixation pen to set.</param>
    /// <param name="newConnectionPen">The new fixation connection pen to set.</param>
    /// <param name="newFont">The new font to set.</param>
    /// <param name="newFontColor">The new font color to set.</param>
    public ColorizationStyle(Pen newFixationPen, Pen newConnectionPen, Font newFont, Color newFontColor)
    {
      this.fixationPen = (Pen)newFixationPen.Clone();
      this.connectionPen = (Pen)newConnectionPen.Clone();
      this.font = (Font)newFont.Clone();
      this.fontColor = newFontColor;
    }

    /// <summary>
    /// Initializes a new instance of the ColorizationStyle class
    /// by cloneing the given <see cref="ColorizationStyle"/>
    /// </summary>
    /// <param name="styleToClone">The <see cref="ColorizationStyle"/> to be cloned.</param>
    public ColorizationStyle(ColorizationStyle styleToClone) :
      this(styleToClone.fixationPen, styleToClone.connectionPen, styleToClone.font, styleToClone.fontColor)
    {
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
    /// Gets or sets the pen for fixation circles or dots.
    /// </summary>
    [XmlIgnore]
    public Pen FixationPen
    {
      get { return this.fixationPen; }
      set { this.fixationPen = value; }
    }

    /// <summary>
    /// Gets or sets the serialized 'FixationPen'property
    /// </summary>
    /// <value>A <see cref="string"/> with the string representation of the FixationPen.</value>
    public string SerializedFixationPen
    {
      get { return ObjectStringConverter.PenToString(this.fixationPen); }
      set { this.fixationPen = ObjectStringConverter.StringToPen(value); }
    }

    /// <summary>
    /// Gets or sets the pen for fixation connections.
    /// </summary>
    [XmlIgnore]
    public Pen ConnectionPen
    {
      get { return this.connectionPen; }
      set { this.connectionPen = value; }
    }

    /// <summary>
    /// Gets or sets the serialized 'ConnectionPen' property
    /// </summary>
    /// <value>A <see cref="string"/> with the string representation of the ConnectionPen.</value>
    public string SerializedConnectionPen
    {
      get { return ObjectStringConverter.PenToString(this.connectionPen); }
      set { this.connectionPen = ObjectStringConverter.StringToPen(value); }
    }

    /// <summary>
    /// Gets or sets the font to use for element names.
    /// </summary>
    [XmlIgnore]
    public Font Font
    {
      get { return this.font; }
      set { this.font = value; }
    }

    /// <summary>
    /// Gets or sets the serialized <see cref="Font"/> property
    /// </summary>
    /// <value>A <see cref="string"/> with the string representation of the font.</value>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public string SerializedFont
    {
      get
      {
        if (this.font != null)
        {
          TypeConverter fontConverter = TypeDescriptor.GetConverter(typeof(Font));
          return fontConverter.ConvertToInvariantString(this.font);
        }
        else
        {
          return null;
        }
      }

      set
      {
        if (!string.IsNullOrEmpty(value))
        {
          try
          {
            TypeConverter fontConverter = TypeDescriptor.GetConverter(typeof(Font));
            this.font = (Font)fontConverter.ConvertFromInvariantString(value);
          }
          catch (ArgumentException)
          {
            // Paranoia check for older versions.
            this.font = new Font(SystemFonts.MenuFont.FontFamily, 28f, GraphicsUnit.Point);
          }
        }
        else
        {
          this.font = null;
        }
      }
    }

    /// <summary>
    /// Gets or sets the color to use during writing.
    /// </summary>
    [XmlIgnore]
    public Color FontColor
    {
      get { return this.fontColor; }
      set { this.fontColor = value; }
    }

    /// <summary>
    /// Gets or sets the serialized font color. Serializes and deserializes the <see cref="FontColor"/> to XML,
    /// because XMLSerializer can not serialize <see cref="Color"/> values.
    /// </summary>
    /// <value>A <see cref="string"/> with the string representation of the font color.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string SerializedFontColor
    {
      get { return ObjectStringConverter.ColorToHtmlAlpha(this.fontColor); }
      set { this.fontColor = ObjectStringConverter.HtmlAlphaToColor(value); }
    }

    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Public methods                                                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region PUBLICMETHODS

    /// <summary>
    /// Creates a copy of this <see cref="ColorizationStyle"/> object.
    /// </summary>
    /// <returns>An exact copy of this <see cref="ColorizationStyle"/>.</returns>
    public object Clone()
    {
      return new ColorizationStyle(this);
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