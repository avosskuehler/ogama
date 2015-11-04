// <copyright file="PresentationScreen.cs" company="alea technologies">
// ******************************************************
// OGAMA - open gaze and mouse analyzer 
// Copyright (C) 2015 Dr. Adrian Voßkühler  
// ------------------------------------------------------------------------
// This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
// This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
// You should have received a copy of the GNU General Public License along with this program; if not, write to the Free Software Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
// **************************************************************
// </copyright>
// <author>Martin Werner</author>
// <email>martin.werner@alea-technologies.de</email>

namespace Ogama.Modules.Common.Tools
{
  using System;
  using System.Drawing;
  using System.Windows.Forms;

  /// <summary>
  /// Monitor enumeration. Primary or Secondary
  /// </summary>
  public enum Monitor
  {
    /// <summary>
    /// Primary Monitor
    /// </summary>
    Primary,
      
    /// <summary>
    /// Secondary Montor
    /// </summary>
    Secondary
  }

  /// <summary>
  /// Static helper class for getting presentation screens and bounds.
  /// </summary>
  public class PresentationScreen
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
    // Public methods                                                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region PUBLICMETHODS

    /// <summary>
    /// Places given form at optional full size on the presentation screen. 
    /// You can change the Presentation screen in Ogama Options
    /// or recording module.
    /// </summary>
    /// <param name="form">Windows form to put on presentation screen</param>
    /// <param name="fullSize">True, if form should be resized to fullscreen</param>
    /// <returns>True, if function succeeded.</returns>
    public static bool PutFormOnPresentationScreen(Form form, bool fullSize)
    {
      Monitor presentationMonitor;
      try
      {
        presentationMonitor = (Monitor)Enum.Parse(typeof(Monitor), Properties.Settings.Default.PresentationScreenMonitor);
      }
      catch (ArgumentException)
      {
        return false;
      }

      // Important !        
      form.StartPosition = FormStartPosition.Manual;

      // Get the monitor screen       
      Screen screen;

      if (presentationMonitor == Monitor.Primary)
      {
        screen = Screen.PrimaryScreen;
      }
      else
      {
        if (presentationMonitor == Monitor.Secondary)
        {
          if (SecondaryScreen.SystemHasSecondaryScreen())
          {
            screen = SecondaryScreen.GetSecondaryScreen();
          }
          else
          {
            return false;
          }
        }
        else
        {
          return false;
        }
      }

      // set the location to the top left of the presentation screen        
      form.Location = screen.Bounds.Location;
      if (fullSize)
      {
        // set it fullscreen        
        form.Size = new Size(screen.Bounds.Width, screen.Bounds.Height);
      }

      return true;
    }

    /// <summary>
    /// Gets the working area of the presentation screen.
    /// In multiple screen environments the monitor is defined in Ogama Options
    /// </summary>
    /// <returns>The working area size of the presentation screen.</returns>
    public static Rectangle GetPresentationWorkingArea()
    {
      return GetPresentationScreen().WorkingArea;
    }

    /// <summary>
    /// Gets the working area of the controller screen.
    /// </summary>
    /// <returns>The working area size of the controller screen.</returns>
    public static Rectangle GetControllerWorkingArea()
    {
      return GetControllerScreen().WorkingArea;
    }

    /// <summary>
    /// Gets the resolution of the presentation screen.
    /// </summary>
    /// <returns>A <see cref="Size"/> with the resolution of the presentation screen.</returns>
    public static Size GetPresentationResolution()
    {
      return GetPresentationScreen().Bounds.Size;
    }

    /// <summary>
    /// Gets the bounding area of the presentation screen.
    /// </summary>
    /// <returns>A <see cref="Rectangle"/> with the bounds of the presentation screen.</returns>
    public static Rectangle GetPresentationBounds()
    {
      return GetPresentationScreen().Bounds;
    }

    /// <summary>
    /// Gets the presentation screen in single and multiple screen environments.
    /// In multiple screen environments the method returns the screen you specify in Ogama Options.
    /// </summary>
    /// <returns>screen which is specified in Ogama Options if exists, otherwise primary.</returns>
    public static Screen GetPresentationScreen()
    {
      Monitor presentationMonitor;
      try
      {
        presentationMonitor = (Monitor)Enum.Parse(typeof(Monitor), Properties.Settings.Default.PresentationScreenMonitor);
      }
      catch (ArgumentException)
      {
        // error on parsing ... take primary
        presentationMonitor = Monitor.Primary;
        Properties.Settings.Default.PresentationScreenMonitor = "Primary";
      }

      // Get the monitor screen       
      if (presentationMonitor == Monitor.Secondary && SecondaryScreen.SystemHasSecondaryScreen())
      {
        return SecondaryScreen.GetSecondaryScreen();
      }
      else
      {
        return Screen.PrimaryScreen;
      }
    }

    /// <summary>
    /// Gets the controller screen in single and multiple screen environments.
    /// </summary>
    /// <returns>The <see cref="Screen"/> which is not
    /// the presentation screen in dual monitor setups,
    /// otherwise, otherwise primary screen.</returns>
    public static Screen GetControllerScreen()
    {
      Monitor presentationMonitor;
      try
      {
        presentationMonitor = (Monitor)Enum.Parse(typeof(Monitor), Properties.Settings.Default.PresentationScreenMonitor);
      }
      catch (ArgumentException)
      {
        // error on parsing ... take primary
        presentationMonitor = Monitor.Primary;
        Properties.Settings.Default.PresentationScreenMonitor = "Primary";
      }

      // Return the controller monitor screen       
      if (presentationMonitor == Monitor.Secondary || !SecondaryScreen.SystemHasSecondaryScreen())
      {
        return Screen.PrimaryScreen;
      }
      else
      {
        return SecondaryScreen.GetSecondaryScreen();
      }
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
    #endregion //METHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER
    #endregion //HELPER
  }
}
