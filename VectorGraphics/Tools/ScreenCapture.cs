// <copyright file="ScreenCapture.cs" company="FU Berlin">
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

namespace VectorGraphics.Tools
{
  using System;
  using System.Drawing;
  using System.Drawing.Imaging;

  using VectorGraphics.Tools.Win32;

  /// <summary>
  /// This class provides functions to capture the entire screen, 
  /// or a particular window, and save it to a file or a image.
  /// </summary>
  public class ScreenCapture
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
    /// Creates an Image object containing a screen shot of the entire desktop.
    /// </summary>
    /// <returns>An <see cref="Image"/> of the entire desktop in its original size.</returns>
    public static Image CaptureScreen()
    {
      return CaptureWindow(User32.GetDesktopWindow());
    }

    /// <summary>
    /// Captures a screen shot of a specific window, and saves it to a file
    /// </summary>
    /// <param name="handle">A <see cref="IntPtr"/> to the window to capture.
    /// (In windows forms, this is obtained by the Handle property)</param>
    /// <param name="filename">A <see cref="string"/> with full filename
    /// and path of the file to save to.</param>
    /// <param name="format">The <see cref="ImageFormat"/> that the 
    /// saved <see cref="Image"/> should have.</param>
    public static void CaptureWindowToFile(IntPtr handle, string filename, ImageFormat format)
    {
      Image img = CaptureWindow(handle);
      img.Save(filename, format);
    }

    /// <summary>
    /// Captures a screen shot of the entire desktop, and saves it to a file
    /// </summary>
    /// <param name="filename">A <see cref="string"/> with full filename
    /// and path of the file to save to.</param>
    /// <param name="format">The <see cref="ImageFormat"/> that the 
    /// saved <see cref="Image"/> should have.</param>
    public static void CaptureScreenToFile(string filename, ImageFormat format)
    {
      Image img = CaptureScreen();
      img.Save(filename, format);
    }

    /// <summary>
    /// Creates an Image object containing a screen shot of a specific window
    /// </summary>
    /// <param name="handle">A <see cref="IntPtr"/> to the window to capture.
    /// (In windows forms, this is obtained by the Handle property)</param>
    /// <returns>An <see cref="Image"/> with the captured window if successfull,
    /// otherwise null.</returns>
    /// <remarks>This method uses the GDI32.BitBlt()
    /// method, so the window has to be visible and should not be 
    /// minimized or hidden for example. In this cases use 
    /// <see cref="GetWindowImage(IntPtr,Size)"/> which uses
    /// User32.PrintWindow().
    /// </remarks>
    public static Image CaptureWindow(IntPtr handle)
    {
      IntPtr hdcSrc = IntPtr.Zero;
      IntPtr hdcDest = IntPtr.Zero;
      IntPtr bitmap = IntPtr.Zero;
      Image img;
      try
      {
        // get te hDC of the target window
        hdcSrc = User32.GetWindowDC(handle);

        // get the size
        User32.RECT windowRect = new User32.RECT();
        User32.GetWindowRect(handle, ref windowRect);
        int width = windowRect.Right - windowRect.Left;
        int height = windowRect.Bottom - windowRect.Top;

        // create a device context we can copy to
        hdcDest = Gdi32.CreateCompatibleDC(hdcSrc);

        // create a bitmap we can copy it to,
        // using GetDeviceCaps to get the width/height
        bitmap = Gdi32.CreateCompatibleBitmap(hdcSrc, width, height);

        // select the bitmap object
        IntPtr old = Gdi32.SelectObject(hdcDest, bitmap);

        // bitblt over
        Gdi32.BitBlt(hdcDest, 0, 0, width, height, hdcSrc, 0, 0, Gdi32.TernaryRasterOperations.SRCCOPY);

        // restore selection
        Gdi32.SelectObject(hdcDest, old);

        // get a .NET image object for it
        img = Image.FromHbitmap(bitmap);
      }
      catch (Exception ex)
      {
        VGExceptionMethods.HandleException(ex);

        return null;
      }
      finally
      {
        // clean up 
        Gdi32.DeleteDC(hdcDest);
        User32.ReleaseDC(handle, hdcSrc);

        // free up the Bitmap object
        Gdi32.DeleteObject(bitmap);
      }

      return img;
    }

    /// <summary>
    /// Captures a screen shot of a specific window of given size,
    /// even if it is hidden.
    /// </summary>
    /// <param name="windowHandle">A <see cref="IntPtr"/> to the window to capture.
    /// (In windows forms, this is obtained by the Handle property)</param>
    /// <param name="size">The <see cref="Size"/> of the captured image.</param>
    /// <returns>An <see cref="Bitmap"/> with the captured window if successful,
    /// otherwise null.</returns>
    /// <exception cref="ArgumentNullException">Thrown, when size is empty, 
    /// or width ot height are less than zero.</exception>
    public static Bitmap GetWindowImage(IntPtr windowHandle, Size size)
    {
      if (size.IsEmpty || size.Height < 0 || size.Width < 0)
      {
        throw new ArgumentException("The window size should not be zero");
      }

      Bitmap bmp = new Bitmap(size.Width, size.Height);
      Graphics g = Graphics.FromImage(bmp);
      IntPtr dc = g.GetHdc();
      try
      {
        User32.PrintWindow(windowHandle, dc, 0); // flags:0 means PW_CLIENTONLY
      }
      catch (Exception ex)
      {
        VGExceptionMethods.HandleException(ex);

        return null;
      }
      finally
      {
        g.ReleaseHdc();
        g.Dispose();
      }

      return bmp;
    }

    #endregion //METHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER
    #endregion //HELPER
  }
}
