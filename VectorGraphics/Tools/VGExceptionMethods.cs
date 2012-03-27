// <copyright file="VGExceptionMethods.cs" company="FU Berlin">
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

namespace VectorGraphics.Tools
{
  using System;
  using System.Windows.Forms;

  /// <summary>
  /// Static methods used for the Vector Graphics exception handling.
  /// </summary>
  public static class VGExceptionMethods
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
    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES
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
    #endregion //OVERRIDES

    ///////////////////////////////////////////////////////////////////////////////
    // Methods for doing main class job                                          //
    ///////////////////////////////////////////////////////////////////////////////
    #region METHODS

    /// <summary>
    /// This static method logs the current call stack into the log file.
    /// </summary>
    public static void LogStack()
    {
      var trace = new System.Diagnostics.StackTrace();
      foreach (var frame in trace.GetFrames())
      {
        var method = frame.GetMethod();
        if (method.Name.Equals("LogStack"))
        {
          continue;
        }

        Console.WriteLine(string.Format("{0}::{1}", method.ReflectedType != null ? method.ReflectedType.Name : string.Empty, method.Name));
      }
    }

    /// <summary>
    /// Creates the error message and displays it.
    /// </summary>
    /// <param name="e">The <see cref="Exception"/> to show.</param>
    public static void HandleException(Exception e)
    {
      DialogResult result = MessageBox.Show(e.ToString(), e.Message, MessageBoxButtons.AbortRetryIgnore);
      switch (result)
      {
        case DialogResult.Abort:
          Application.Exit();
          break;
        case DialogResult.Retry:
        case DialogResult.Ignore:
          break;
      }
    }

    /// <summary>
    /// Silently handles the incoming exception.
    /// </summary>
    /// <param name="ex">The <see cref="Exception"/> to be processed.</param>
    public static void HandleExceptionSilent(Exception ex)
    {
      // Currently does nothing... should be logged.
    }

    #endregion //METHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER
    #endregion //HELPER
  }
}
