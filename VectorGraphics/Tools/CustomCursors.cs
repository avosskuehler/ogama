// <copyright file="CustomCursors.cs" company="FU Berlin">
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
  using System.Reflection;
  using System.Text;
  using System.Windows.Forms;
  using VectorGraphics.Elements;

  /// <summary>
  /// This class provides a set of custom cursors for
  /// the creation of the vector graphics elements in this namespace.
  /// </summary>
  public static class CustomCursors
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
    /// This is a cursor for creating rectangles.
    /// </summary>
    private static Cursor rectangle = new Cursor(
      Assembly.GetAssembly(typeof(VGElement)).GetManifestResourceStream("VectorGraphics.Cursors.Rectangle.Cur"));

    /// <summary>
    /// This is a cursor for creating ellipse.
    /// </summary>
    private static Cursor ellipse = new Cursor(
      Assembly.GetAssembly(typeof(VGElement)).GetManifestResourceStream("VectorGraphics.Cursors.Ellipse.Cur"));

    /// <summary>
    /// This is a cursor for creating polylines.
    /// </summary>
    private static Cursor polyline = new Cursor(
      Assembly.GetAssembly(typeof(VGElement)).GetManifestResourceStream("VectorGraphics.Cursors.Polyline.Cur"));

    /// <summary>
    /// This is a cursor for creating line.
    /// </summary>
    private static Cursor line = new Cursor(
      Assembly.GetAssembly(typeof(VGElement)).GetManifestResourceStream("VectorGraphics.Cursors.Line.Cur"));

    /// <summary>
    /// This is a cursor for creating instructions.
    /// </summary>
    private static Cursor text = new Cursor(
      Assembly.GetAssembly(typeof(VGElement)).GetManifestResourceStream("VectorGraphics.Cursors.Text.Cur"));

    /// <summary>
    /// This is a cursor for creating images.
    /// </summary>
    private static Cursor image = new Cursor(
      Assembly.GetAssembly(typeof(VGElement)).GetManifestResourceStream("VectorGraphics.Cursors.Image.Cur"));

    /// <summary>
    /// This is a cursor for creating sounds.
    /// </summary>
    private static Cursor sound = new Cursor(
      Assembly.GetAssembly(typeof(VGElement)).GetManifestResourceStream("VectorGraphics.Cursors.Sound.Cur"));

    /// <summary>
    /// This is a cursor for creating sharps.
    /// </summary>
    private static Cursor sharp = new Cursor(
      Assembly.GetAssembly(typeof(VGElement)).GetManifestResourceStream("VectorGraphics.Cursors.Sharp.Cur"));

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION
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
    /// Gets the cursor for creating rectangles.
    /// </summary>
    public static Cursor Rectangle
    {
      get { return rectangle; }
    }

    /// <summary>
    /// Gets the cursor for creating ellipse.
    /// </summary>
    public static Cursor Ellipse
    {
      get { return ellipse; }
    }

    /// <summary>
    /// Gets the cursor for creating polylines.
    /// </summary>
    public static Cursor Polyline
    {
      get { return polyline; }
    }

    /// <summary>
    /// Gets the cursor for creating line.
    /// </summary>
    public static Cursor Line
    {
      get { return line; }
    }

    /// <summary>
    /// Gets the cursor for creating instructions.
    /// </summary>
    public static Cursor Text
    {
      get { return text; }
    }

    /// <summary>
    /// Gets the cursor for creating images.
    /// </summary>
    public static Cursor Image
    {
      get { return image; }
    }

    /// <summary>
    /// Gets the cursor for creating sounds.
    /// </summary>
    public static Cursor Sound
    {
      get { return sound; }
    }

    /// <summary>
    /// Gets the cursor for creating sharps.
    /// </summary>
    public static Cursor Sharp
    {
      get { return sharp; }
    }

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
