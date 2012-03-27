// <copyright file="AxFlashControl.cs" company="FU Berlin">
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

namespace VectorGraphics.Controls.Flash
{
  using System;
  using System.Windows.Forms;

  using Microsoft.VisualStudio.OLE.Interop;

  /// <summary>
  /// Derives from <see cref="AxFlash"/> which is the import
  /// class for the Macromedia Flash com object.
  /// Adds a window handle support to retrieve the surface of the com object
  /// to draw directly on it without flicker.
  /// </summary>
  public class AxFlashControl : AxFlash
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
    /// This <see cref="IntPtr"/> saves the window handle
    /// of the flash com objects surface.
    /// </summary>
    private IntPtr handleToSurface;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the AxFlashControl class.
    /// </summary>
    public AxFlashControl()
    {
      this.handleToSurface = IntPtr.Zero;
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

    /// <summary>
    /// Gets the <see cref="IntPtr"/> with the window handle
    /// of the flash com objects surface.
    /// </summary>
    public IntPtr HandleToSurface
    {
      get { return this.handleToSurface; }
    }

    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Public methods                                                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region PUBLICMETHODS

    /// <summary>
    /// This method creates the window handle of the com object surface.
    /// </summary>
    /// <remarks>Note: this only can be done, when the control is already added
    /// to another controls control collection that is windowed.</remarks>
    public void CreateWindowHandle()
    {
      this.handleToSurface = this.GetFlashWindow();
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

    /// <summary>
    /// Returns the given controls handle.
    /// </summary>
    /// <param name="control">The <see cref="Control"/> to receive the handle for.</param>
    /// <returns>An <see cref="IntPtr"/> with the controls handle.</returns>
    private IntPtr GetControlHandle(Control control)
    {
      return control.Handle;
    }

    /// <summary>
    /// This method iterates through the window tree of the controls parent
    /// to find the surface window of the com object, that is
    /// the window thats name is starting with "Macromedia"
    /// </summary>
    /// <returns>The window handle <see cref="IntPtr"/>
    /// of the flash com objects surface.</returns>
    private IntPtr GetFlashWindow()
    {
      IntPtr window = IntPtr.Zero;
      IOleWindow oleWindow = (IOleWindow)this.GetOcx();
      if (oleWindow != null)
      {
        oleWindow.GetWindow(out window);
      }

      return window;
    }

    #endregion //PRIVATEMETHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER
    #endregion //HELPER
  }
}