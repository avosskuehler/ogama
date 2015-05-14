// <copyright file="FlashMouseLeaveMessageFilter.cs" company="FU Berlin">
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

namespace VectorGraphics.Controls.Flash
{
  using System;
  using System.Windows.Forms;

  using VectorGraphics.Tools.Win32;

  /// <summary>
  /// A Message Filter class derived from <see cref="IMessageFilter"/>.
  /// Hooks messages to the given Object and posts them also to 
  /// their underlying parent.
  /// </summary>
  /// <remarks>That is needed for getting mouse and key events behind a 
  /// ActiveX object, that does not support the standard windows event handlers.</remarks>
  /// <seealso cref="AxHost"/>
  /// <seealso cref="IMessageFilter"/>
  public class FlashMouseLeaveMessageFilter : IMessageFilter
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
    /// Saves the <see cref="IntPtr"/> handle for the object whos events 
    /// should be posted to its parent.
    /// </summary>
    private IntPtr comObjectHandle;

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

    /// <summary>
    /// Initializes a new instance of the FlashMouseLeaveMessageFilter class.
    /// </summary>
    /// <param name="flashComObjectHandle">A Handle for the object whos events 
    /// should be filtered.</param>
    public FlashMouseLeaveMessageFilter(IntPtr flashComObjectHandle)
    {
      this.comObjectHandle = flashComObjectHandle;
    }

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
    /// Filters out a message before it is dispatched. 
    /// </summary>
    /// <param name="m">The message to be dispatched. 
    /// You cannot modify this message. </param>
    /// <returns><strong>True</strong> to filter the message and stop 
    /// it from being dispatched; <strong>False</strong> to allow the message to continue 
    /// to the next filter or control. </returns>
    public bool PreFilterMessage(ref Message m)
    {
      // Only if message is targetet to the com object.
      if (m.HWnd == this.comObjectHandle)
      {
        if (m.Msg == User32.WMMOUSELEAVE || m.Msg == User32.WMCAPTURECHANGED)
        {
          // Do not process this message
          return true;
        }
      }

      // Go on pumping this message.
      return false;
    }

    #endregion //METHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER
    #endregion //HELPER
  }
}