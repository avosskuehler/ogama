// <copyright file="FlashMessageFilter.cs" company="FU Berlin">
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
  using System.Drawing;
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
  public class FlashMessageFilter : IMessageFilter
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

    /// <summary>
    /// Saves the <see cref="IntPtr"/> handle for the parent that receives the
    /// copied message.
    /// </summary>
    private IntPtr receivingControlHandle;

    /// <summary>
    /// Saves the <see cref="Control"/> who's events 
    /// should be posted to its parent.
    /// </summary>
    private Control comObject;

    /// <summary>
    /// Saves the parent <see cref="Control"/> that receives the
    /// copied message.
    /// </summary>
    private Control receivingControl;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the FlashMessageFilter class.
    /// </summary>
    /// <param name="sendingComObject">A Handle for the object whos events 
    /// should be posted to its parent.</param>
    /// <param name="receivingControl">A Handle to the object should receive
    /// a copy of the events. Normally the parents handle.
    /// Can be received from <see cref="Control.Handle"/></param>
    public FlashMessageFilter(Control sendingComObject, Control receivingControl)
    {
      this.comObject = sendingComObject;
      this.receivingControl = receivingControl;
      if (this.comObject.InvokeRequired)
      {
        this.comObjectHandle = (IntPtr)this.comObject.Invoke(new HandleInvoker(this.GetControlHandle), this.comObject);
      }
      else
      {
        this.comObjectHandle = this.comObject.Handle;
      }

      if (this.receivingControl.InvokeRequired)
      {
        this.receivingControlHandle = (IntPtr)this.receivingControl.Invoke(new HandleInvoker(this.GetControlHandle), this.receivingControl);
      }
      else
      {
        this.receivingControlHandle = this.receivingControl.Handle;
      }
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Enumerations                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region ENUMS

    /// <summary>
    /// The delgate for the thread-safe call to GetControlHandle
    /// </summary>
    /// <param name="control">The <see cref="Control"/> to receive the handle for.</param>
    /// <returns>An <see cref="IntPtr"/> with the controls handle.</returns>
    private delegate IntPtr HandleInvoker(Control control);

    #endregion ENUMS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES
    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Public methods                                                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region PUBLICMETHODS

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
        if (m.Msg >= User32.WMMOUSEMOVE && m.Msg <= User32.WMMBUTTONDBLCLK)
        {
          // The following line did´nt work.
          // Point location = (Point)m.GetLParam(typeof(Point));
          // So use instead classic way :-)
          // The X value of the mouse position is stored in the
          // low-order bits of LParam, and the Y value in the
          // high-order bits.
          int x = (m.LParam.ToInt32() << 16) >> 16;
          int y = m.LParam.ToInt32() >> 16;

          // Convert coordinates to receiver coordinates
          Point mouseDownScreenPosition = this.comObject.PointToScreen(new Point(x, y));
          Point mouseDownReceiverPosition = this.receivingControl.PointToClient(mouseDownScreenPosition);
          IntPtr lParam = (IntPtr)((mouseDownReceiverPosition.Y << 16) | mouseDownReceiverPosition.X);

          // Post message to receiver
          User32.SendMessage(this.receivingControlHandle, m.Msg, m.WParam, lParam);
        }
      }

      // Go on pumping this message.
      return false;
    }

    #endregion //PUBLICMETHODS

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
    /// Returns the given controls handle.
    /// </summary>
    /// <param name="control">The <see cref="Control"/> to receive the handle for.</param>
    /// <returns>An <see cref="IntPtr"/> with the controls handle.</returns>
    private IntPtr GetControlHandle(Control control)
    {
      return control.Handle;
    }

    #endregion //METHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER
    #endregion //HELPER
  }
}