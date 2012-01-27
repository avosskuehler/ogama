// <copyright file="IOHelpers.cs" company="FU Berlin">
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

namespace Ogama.Modules.Common.Tools
{
  using System;
  using System.IO;

  /// <summary>
  /// Class for import and export functionality.
  /// </summary>
  public sealed class IOHelpers
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
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES
    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION
    #endregion //CONSTRUCTION

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
    /// Returns a Boolean value indicating whether an expression can be evaluated as a number. 
    /// </summary>
    /// <param name="expression">object to test</param>
    /// <returns><strong>True</strong> if given expression is a number,
    /// otherwise <strong>false</strong>.</returns>
    public static bool IsNumeric(object expression)
    {
      // Variable to collect the Return value of the TryParse method.
      bool isNum;

      // Define variable to collect out parameter of the TryParse method. If the conversion fails, the out parameter is zero.
      double retNum;

      // The TryParse method converts a string in a specified style 
      // and culture-specific format to its double-precision floating point number equivalent.
      // The TryParse method does not generate an exception if the 
      // conversion fails. If the conversion passes, True is returned. 
      // If it does not, False is returned.
      isNum = double.TryParse(
        Convert.ToString(expression),
        System.Globalization.NumberStyles.Any,
        System.Globalization.NumberFormatInfo.InvariantInfo, 
        out retNum);

      return isNum;
    }

    /// <summary>
    /// This method checks for the appearance of invalid
    /// characters for filenames in the given string,
    /// linke white space or slashes etc.
    /// </summary>
    /// <param name="filename">A <see cref="string"/> with the filename (without)
    /// path to check for.</param>
    /// <returns><strong>True</strong>, if filename is valid,
    /// otherwise <strong>false</strong>.</returns>
    public static bool IsValidFilename(string filename)
    {
      // Get a list of invalid file characters.
      char[] invalidFileChars = Path.GetInvalidFileNameChars();

      return filename.IndexOfAny(invalidFileChars) < 0;
    }

    #endregion //METHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER
    #endregion //HELPER
  }
}
