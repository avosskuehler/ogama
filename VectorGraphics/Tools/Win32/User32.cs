// <copyright file="User32.cs" company="FU Berlin">
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

namespace VectorGraphics.Tools.Win32
{
  using System;
  using System.Runtime.InteropServices;
  using System.Text;

  /// <summary>
  /// Helper class containing User32 API functions
  /// </summary>
  public class User32
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
    /// The WM_USER constant is used by applications to help 
    /// define private messages for use by private window classes, 
    /// usually of the form WM_USER+X, where X is an integer value. 
    /// </summary>
    public const int WMUSER = 0x0400;

    /// <summary>
    /// The left mouse button is down.
    /// </summary>
    public const int MKLBUTTON = 0x0001;

    /// <summary>
    /// The right mouse button is down.
    /// </summary>
    public const int MKRBUTTON = 0x0002;

    /// <summary>
    /// The middle mouse button is down.
    /// </summary>
    public const int MKMBUTTON = 0x0010;

    /// <summary>
    /// The EM_FORMATRANGE message formats a range of text in a rich edit control for a specific device.
    /// </summary>
    public const int EMFORMATRANGE = WMUSER + 57;

    /// <summary>
    /// The EM_SETTARGETDEVICE message sets the target device and line width used for WYSIWYG formatting in a rich edit control.
    /// </summary>
    public const int EMSETTARGETDEVICE = WMUSER + 72;

    /// <summary>
    /// The EM_SETBKGNDCOLOR message sets the background color for a rich edit control.
    /// </summary>
    public const int EMSETBKGNDCOLOR = WMUSER + 67;

    /// <summary>
    /// An application sends the WM_SETREDRAW message to 
    /// a window to allow changes in that window to be redrawn 
    /// or to prevent changes in that window from being redrawn. 
    /// </summary>
    public const int WMSETREDRAW = 0x000B;

    /// <summary>
    /// The WM_PAINT message is sent when the system or another 
    /// application makes a request to paint a portion of an application's window. 
    /// The message is sent when the UpdateWindow or RedrawWindow 
    /// function is called, or by the DispatchMessage function when 
    /// the application obtains a WM_PAINT message by using the GetMessage or PeekMessage function. 
    /// </summary>
    public const int WMPAINT = 0x0F;

    /// <summary>
    /// The WM_ERASEBKGND message is sent when the window 
    /// background must be erased (for example, when a window is resized). 
    /// The message is sent to prepare an invalidated portion of a window for painting. 
    /// </summary>
    public const int WMERASEBKGND = 0x0014;

    /// <summary>
    /// Windows XP: Paints all descendants of a window 
    /// in bottom-to-top painting order using double-buffering. For more information, see Remarks. 
    /// This cannot be used if the window has a class style of either CS_OWNDC or CS_CLASSDC. 
    /// </summary>
    public const int WSEXCOMPOSITED = 0x02000000;

    /// <summary>
    /// Creates a child window. Cannot be used with the WS_POPUP style.
    /// </summary>
    public const int WSCHILD = 0x40000000;

    /// <summary>
    /// Excludes the area occupied by child windows 
    /// when you draw within the parent window. Used when you create the parent window.
    /// </summary>
    public const int WSCLIPCHILDREN = 0x02000000;

    /// <summary>
    /// Clips child windows relative to each other; 
    /// that is, when a particular child window receives a paint message, 
    /// the WS_CLIPSIBLINGS style clips all other overlapped child windows 
    /// out of the region of the child window to be updated. (If WS_CLIPSIBLINGS 
    /// is not given and child windows overlap, when you draw within the 
    /// client area of a child window, it is possible to draw within the 
    /// client area of a neighboring child window.) For use with the WS_CHILD style only.
    /// </summary>
    public const int WSCLIPSIBLINGS = 0x04000000;

    /// <summary>
    /// Specifies that a window created with this style is 
    /// to be transparent. That is, any windows that are beneath 
    /// the window are not obscured by the window. A window created 
    /// with this style receives WM_PAINT messages only after all 
    /// sibling windows beneath it have been updated.
    /// </summary>
    public const int WSEXTRANSPARENT = 0x00000020;

    /// <summary>
    /// Creates a window that is initially visible.
    /// </summary>
    public const int WSVISIBLE = 0x10000000;

    /// <summary>
    /// The WM_MOUSEMOVE message is posted to a window when the cursor moves. 
    /// If the mouse is not captured, the message is posted to the window 
    /// that contains the cursor. Otherwise, the message is posted 
    /// to the window that has captured the mouse.
    /// </summary>
    public const int WMMOUSEMOVE = 0x0200;

    /// <summary>
    /// The WM_MOUSEACTIVATE message is sent when the cursor is in an 
    /// inactive window and the user presses a mouse button. 
    /// The parent window receives this message only if the child 
    /// window passes it to the DefWindowProc function.
    /// </summary>
    public const int WMMOUSEACTIVATE = 0x0021;

    /// <summary>
    /// The WM_CAPTURECHANGED message is sent to the 
    /// window that is losing the mouse capture.
    /// </summary>
    public const int WMCAPTURECHANGED = 0x0215;

    /// <summary>
    /// The WM_SETCURSOR message is sent to a window if the mouse 
    /// causes the cursor to move within a window and mouse input is not captured
    /// </summary>
    public const int WMSETCURSOR = 0x0020;

    /// <summary>
    /// The WM_MOUSELEAVE message is posted to a window when the cursor 
    /// leaves the client area of the window specified in a prior call to TrackMouseEvent.
    /// </summary>
    public const int WMMOUSELEAVE = 0x02A3;

    /// <summary>
    /// The WM_LBUTTONDOWN message is posted when the user presses 
    /// the left mouse button while the cursor is in the client 
    /// area of a window. If the mouse is not captured, 
    /// the message is posted to the window beneath the cursor. 
    /// Otherwise, the message is posted to the window that has captured the mouse.
    /// </summary>
    public const int WMLBUTTONDOWN = 0x0201;

    /// <summary>
    /// The WM_LBUTTONUP message is posted when the user releases 
    /// the left mouse button while the cursor is in the client 
    /// area of a window. If the mouse is not captured, the message 
    /// is posted to the window beneath the cursor. Otherwise, the 
    /// message is posted to the window that has captured the mouse.
    /// </summary>
    public const int WMLBUTTONUP = 0x0202;

    /// <summary>
    /// The WM_RBUTTONDOWN message is posted when the user presses
    /// the right mouse button while the cursor is in the client 
    /// area of a window. If the mouse is not captured, the message
    /// is posted to the window beneath the cursor. Otherwise, the 
    /// message is posted to the window that has captured the mouse.
    /// </summary>
    public const int WMRBUTTONDOWN = 0x0204;

    /// <summary>
    /// The WM_RBUTTONUP message is posted when the user releases 
    /// the right mouse button while the cursor is in the 
    /// client area of a window. If the mouse is not captured, 
    /// the message is posted to the window beneath the cursor. 
    /// Otherwise, the message is posted to the window that has 
    /// captured the mouse
    /// </summary>
    public const int WMRBUTTONUP = 0x0205;

    /// <summary>
    /// The WM_RBUTTONDBLCLK message is posted when the user 
    /// double-clicks the right mouse button while the cursor 
    /// is in the client area of a window. If the mouse is not 
    /// captured, the message is posted to the window beneath 
    /// the cursor. Otherwise, the message is posted to the window 
    /// that has captured the mouse.
    /// </summary>
    public const int WMRBUTTONDBLCLK = 0x0206;

    /// <summary>
    /// The WM_MBUTTONDOWN message is posted when the user 
    /// presses the middle mouse button while the cursor 
    /// is in the client area of a window. If the mouse is 
    /// not captured, the message is posted to the window beneath 
    /// the cursor. Otherwise, the message is posted to the window that has captured the mouse.
    /// </summary>
    public const int WMMBUTTONDOWN = 0x0207;

    /// <summary>
    /// The WM_MBUTTONUP message is posted when the user releases 
    /// the middle mouse button while the cursor is in the 
    /// client area of a window. If the mouse is not captured, 
    /// the message is posted to the window beneath the cursor. 
    /// Otherwise, the message is posted to the window that has captured the mouse.
    /// </summary>
    public const int WMMBUTTONUP = 0x0208;

    /// <summary>
    /// The WM_MBUTTONDBLCLK message is posted when the user double-clicks
    /// the middle mouse button while the cursor is in the client area 
    /// of a window. If the mouse is not captured, the message is posted 
    /// to the window beneath the cursor. Otherwise, the message is 
    /// posted to the window that has captured the mouse.
    /// </summary>
    public const int WMMBUTTONDBLCLK = 0x0209;

    /// <summary>
    /// The retrieved handle identifies the window below the specified window 
    /// in the Z order. If the specified window is a topmost window, 
    /// the handle identifies the topmost window below the specified window. 
    /// If the specified window is a top-level window, the handle identifies 
    /// the top-level window below the specified window. If the specified 
    /// window is a child window, the handle identifies the sibling window 
    /// below the specified window.
    /// </summary>
    public const uint GWHWNDNEXT = 2;

    /// <summary>
    /// The retrieved handle identifies the child window at the top 
    /// of the Z order, if the specified window is a parent window;
    /// otherwise, the retrieved handle is NULL. The function examines 
    /// only child windows of the specified window. It does not examine descendant windows.
    /// </summary>
    public const uint GWCHILD = 5;

    /// <summary>
    /// The WM_SETFOCUS message is sent to a window after it has gained the keyboard focus. 
    /// </summary>
    public const int WMSETFOCUS = 0x0007;

    /// <summary>
    /// Indicates client window.
    /// </summary>
    public const int HTCLIENT = 1;

    /// <summary>
    /// The WM_KILLFOCUS message is sent to a window immediately before it loses the keyboard focus. 
    /// </summary>
    public const int WMKILLFOCUS = 0x0008;

    /// <summary>
    /// Causes the affected windows (as specified by the RDW_ALLCHILDREN and RDW_NOCHILDREN flags) 
    /// to receive WM_NCPAINT, WM_ERASEBKGND, and WM_PAINT messages, if necessary, 
    /// before the function returns.
    /// </summary>
    public const int RDWUPDATENOW = 0x0100;

    /// <summary>
    /// Excludes child windows, if any, from the repainting operation.
    /// </summary>
    public const int RDWNOCHILDREN = 0x0040;

    /// <summary>
    /// Includes child windows, if any, in the repainting operation.
    /// </summary>
    public const int RDWALLCHILDREN = 0x0080;

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
    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Public methods                                                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region PUBLICMETHODS

    /// <summary>
    /// The RedrawWindow function updates the specified rectangle or region in a window's client area. 
    /// </summary>
    /// <param name="windowHandle">[in] Handle to the window to be redrawn. 
    /// If this parameter is NULL, the desktop window is updated.</param>
    /// <param name="updateRectangle">[in] Pointer to a RECT structure containing 
    /// the coordinates, in device units, of the update rectangle. 
    /// This parameter is ignored if the hrgnUpdate parameter identifies a region. </param>
    /// <param name="updateRegion">[in] Handle to the update region. 
    /// If both the hrgnUpdate and lprcUpdate parameters are NULL,
    /// the entire client area is added to the update region. </param>
    /// <param name="flags">[in] Specifies one or more redraw flags. 
    /// This parameter can be used to invalidate or validate a window, 
    /// control repainting, and control which windows are affected by RedrawWindow. </param>
    /// <returns>If the function succeeds, the return value is nonzero.
    /// If the function fails, the return value is zero.</returns>
    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool RedrawWindow(
      IntPtr windowHandle,
      IntPtr updateRectangle,
      IntPtr updateRegion,
      uint flags);

    /// <summary>
    /// The GetDesktopWindow function returns a handle to the desktop window. 
    /// The desktop window covers the entire screen. 
    /// The desktop window is the area on top of which other windows are painted.
    /// </summary>
    /// <returns>The return value is a handle to the desktop window. </returns>
    [DllImport("user32.dll")]
    public static extern IntPtr GetDesktopWindow();

    /// <summary>
    /// The GetWindow function retrieves a handle to a window 
    /// that has the specified relationship (Z-Order or owner) to the specified window. 
    /// </summary>
    /// <param name="windowHandle">[in] Handle to a window. The window handle 
    /// retrieved is relative to this window, based on the value of the uCmd parameter. </param>
    /// <param name="cmd">[in] Specifies the relationship between the specified 
    /// window and the window whose handle is to be retrieved.
    /// Use one of the GW_ constants</param>
    /// <returns>If the function succeeds, the return value is a window handle. 
    /// If no window exists with the specified relationship to the specified window,
    /// the return value is NULL.</returns>
    [DllImport("user32.dll", SetLastError = true)]
    public static extern IntPtr GetWindow(IntPtr windowHandle, uint cmd);

    /// <summary>
    /// The GetClassName function retrieves the name of the class to which the specified window belongs.
    /// </summary>
    /// <param name="windowHandle">[in] Handle to the window and, indirectly, the class to which the window belongs. </param>
    /// <param name="className">[out] Pointer to the buffer that is to receive the class name string.</param>
    /// <param name="maxCount">[in] Specifies the length, in TCHAR, of the buffer 
    /// pointed to by the lpClassName parameter. The class name string 
    /// is truncated if it is longer than the buffer and is always null-terminated. </param>
    /// <returns>If the function succeeds, the return value is 
    /// the number of TCHAR copied to the specified buffer.
    /// If the function fails, the return value is zero. </returns>
    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern int GetClassName(IntPtr windowHandle, StringBuilder className, int maxCount);

    /// <summary>
    /// The GetWindowDC function retrieves the device context (DC) 
    /// for the entire window, including title bar, menus, and scroll bars. 
    /// A window device context permits painting anywhere in a window, 
    /// because the origin of the device context is the upper-left 
    /// corner of the window instead of the client area. 
    /// GetWindowDC assigns default attributes to the window device 
    /// context each time it retrieves the device context. 
    /// Previous attributes are lost. 
    /// </summary>
    /// <param name="windowHandle">[in] Handle to the window with a device 
    /// context that is to be retrieved. If this value is NULL, 
    /// GetWindowDC retrieves the device context for the entire screen. </param>
    /// <returns>If the function succeeds, the return value is a handle 
    /// to a device context for the specified window. 
    /// If the function fails, the return value is NULL, 
    /// indicating an error or an invalid windowHandle parameter. </returns>
    [DllImport("user32.dll")]
    public static extern IntPtr GetWindowDC(IntPtr windowHandle);

    /// <summary>
    /// The GetWindowRect function retrieves the dimensions of 
    /// the bounding rectangle of the specified window. 
    /// The dimensions are given in screen coordinates that 
    /// are relative to the upper-left corner of the screen. 
    /// </summary>
    /// <param name="windowHandle">[in] Handle to the window. </param>
    /// <param name="rect">[out] Pointer to a structure that receives 
    /// the screen coordinates of the upper-left and lower-right 
    /// corners of the window. </param>
    /// <returns>If the function succeeds, the return value is nonzero.
    /// If the function fails, the return value is zero. To get extended 
    /// error information, call. </returns>
    [DllImport("user32.dll")]
    public static extern IntPtr GetWindowRect(IntPtr windowHandle, ref RECT rect);

    /// <summary>
    /// The PrintWindow function copies a visual window into 
    /// the specified device context (DC), typically a printer DC.
    /// </summary>
    /// <param name="windowHandle">Handle to the window that will be copied.</param>
    /// <param name="dc">Handle to the device context.</param>
    /// <param name="flags">Specifies the drawing options. </param>
    /// <returns>If the function succeeds, it returns a nonzero value.
    /// If the function fails, it returns zero.</returns>
    [DllImport("user32.dll")]
    public static extern int PrintWindow(IntPtr windowHandle, IntPtr dc, uint flags);

    /// <summary>
    /// The PostMessage function places (posts) a message in the message 
    /// queue associated with the thread that created the specified 
    /// window and returns without waiting for the thread to process the message. 
    /// </summary>
    /// <param name="windowHandle">[in] Handle to the window whose window procedure 
    /// is to receive the message. The following values have special meanings.
    /// HWND_BROADCAST
    /// The message is posted to all top-level windows in the system,
    /// including disabled or invisible unowned windows, overlapped windows, 
    /// and pop-up windows. The message is not posted to child windows.
    /// NULL
    /// The function behaves like a call to PostThreadMessage with the 
    /// dwThreadId parameter set to the identifier of the current thread.</param>
    /// <param name="msg">[in] Specifies the message to be posted.</param>
    /// <param name="wParam">[in] Specifies additional message-specific information 1.</param>
    /// <param name="lParam">[in] Specifies additional message-specific information 2.</param>
    /// <returns>If the function succeeds, the return value is nonzero.
    /// If the function fails, the return value is zero. To get extended 
    /// error information, call GetLastError.</returns>
    [DllImport("USER32.DLL", CharSet = CharSet.Auto)]
    public static extern bool PostMessage(IntPtr windowHandle, int msg, IntPtr wParam, IntPtr lParam);

    /// <summary>
    /// Needed to forward keyboard messages to the child TextBox control.
    /// </summary>
    /// <param name="windowHandle">Handle of window to send the message to.</param>
    /// <param name="msg">Specifies the message to be sent.</param>
    /// <param name="wParam">Specifies additional message-dependent information. 1</param>
    /// <param name="lParam">Specifies additional message-dependent information. 2</param>
    /// <returns>The result of the message processing; its value depends on the message sent.</returns>
    [DllImport("USER32.DLL", CharSet = CharSet.Auto)]
    public static extern IntPtr SendMessage(IntPtr windowHandle, int msg, IntPtr wParam, IntPtr lParam);

    /// <summary>
    /// The InvalidateRect function adds a rectangle to the 
    /// specified window's update region. The update region 
    /// represents the portion of the window's client area that must be redrawn. 
    /// </summary>
    /// <param name="windowHandle">[in] Handle to the window whose update region 
    /// has changed. If this parameter is NULL, the system invalidates 
    /// and redraws all windows, and sends the WM_ERASEBKGND and 
    /// WM_NCPAINT messages to the window procedure before the function returns.</param>
    /// <param name="rect">[in] Pointer to a RECT structure that 
    /// contains the client coordinates of the rectangle to be added 
    /// to the update region. If this parameter is NULL, the entire 
    /// client area is added to the update region. </param>
    /// <param name="erase">[in] Specifies whether the background within 
    /// the update region is to be erased when the update region 
    /// is processed. If this parameter is TRUE, the background 
    /// is erased when the BeginPaint function is called. If this 
    /// parameter is FALSE, the background remains unchanged. </param>
    /// <returns>If the function succeeds, the return value is nonzero.
    /// If the function fails, the return value is zero. </returns>
    [DllImport("user32.dll")]
    public static extern bool InvalidateRect(IntPtr windowHandle, IntPtr rect, bool erase);

    /// <summary>
    /// The InvalidateRgn function invalidates the client area within 
    /// the specified region by adding it to the current update 
    /// region of a window. The invalidated region, along with all 
    /// other areas in the update region, is marked for painting when 
    /// the next WM_PAINT message occurs.
    /// </summary>
    /// <param name="windowHandle">[in] Handle to the window with an update region that is to be modified. </param>
    /// <param name="rgn">[in] Handle to the region to be added to the update 
    /// region. The region is assumed to have client coordinates. 
    /// If this parameter is NULL, the entire client area is added to the update region. </param>
    /// <param name="erase">[in] Specifies whether the background within
    /// the update region should be erased when the update region 
    /// is processed. If this parameter is TRUE, the background is erased 
    /// when the BeginPaint function is called. If the parameter is FALSE, 
    /// the background remains unchanged. </param>
    /// <returns>The return value is always nonzero. </returns>
    [DllImport("user32.dll")]
    public static extern int InvalidateRgn(IntPtr windowHandle, IntPtr rgn, bool erase);

    /// <summary>
    /// The UpdateWindow function updates the client area of the specified 
    /// window by sending a WM_PAINT message to the window if the window's 
    /// update region is not empty. The function sends a WM_PAINT message 
    /// directly to the window procedure of the specified window, bypassing 
    /// the application queue. If the update region is empty, no message is sent.
    /// </summary>
    /// <param name="windowHandle">[in] Handle to the window to be updated. </param>
    /// <returns>If the function succeeds, the return value is nonzero.
    /// If the function fails, the return value is zero. </returns>
    [DllImport("user32.dll")]
    public static extern bool UpdateWindow(IntPtr windowHandle);

    /// <summary>
    /// The GetDC function retrieves a handle to a display device 
    /// context (DC) for the client area of a specified window or 
    /// for the entire screen. You can use the returned handle in 
    /// subsequent GDI functions to draw in the DC. 
    /// </summary>
    /// <param name="hwnd">[in] Handle to the window whose DC is to be retrieved. 
    /// If this value is NULL, GetDC retrieves the DC for the entire screen. </param>
    /// <returns>If the function succeeds, the return value is a handle 
    /// to the DC for the specified window's client area. 
    /// If the function fails, the return value is NULL.</returns>
    [DllImport("user32.dll")]
    public static extern IntPtr GetDC(IntPtr hwnd);

    /// <summary>
    /// The ReleaseDC function releases a device context (DC), 
    /// freeing it for use by other applications. The effect 
    /// of the ReleaseDC function depends on the type of DC. 
    /// It frees only common and window DCs. It has no effect 
    /// on class or private DCs. 
    /// </summary>
    /// <param name="windowHandle">[in] Handle to the window whose DC is to be released.</param>
    /// <param name="hDC">[in] Handle to the DC to be released.</param>
    /// <returns>The return value indicates whether the DC was released. 
    /// If the DC was released, the return value is 1. 
    /// If the DC was not released, the return value is zero.</returns>
    [DllImport("user32.dll")]
    public static extern IntPtr ReleaseDC(IntPtr windowHandle, IntPtr hDC);

    #endregion //PUBLICMETHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Structs                                                                   //
    ///////////////////////////////////////////////////////////////////////////////
    #region STRUCTS

    /// <summary>
    /// The RECT structure defines the coordinates of the 
    /// upper-left and lower-right corners of a rectangle. 
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct RECT
    {
      /// <summary>
      /// Specifies the x-coordinate of the upper-left corner of the rectangle. 
      /// </summary>
      public int Left;

      /// <summary>
      /// Specifies the y-coordinate of the upper-left corner of the rectangle. 
      /// </summary>
      public int Top;

      /// <summary>
      /// Specifies the x-coordinate of the lower-right corner of the rectangle. 
      /// </summary>
      public int Right;

      /// <summary>
      /// Specifies the y-coordinate of the lower-right corner of the rectangle. 
      /// </summary>
      public int Bottom;
    }

    /// <summary>
    /// The CHARRANGE structure specifies a range of characters 
    /// in a rich edit control. This structure is used with the 
    /// EM_EXGETSEL and EM_EXSETSEL messages. 
    /// If the cpMin and cpMax members are equal, the range is empty. 
    /// The range includes everything if cpMin is 0 and cpMax is —1.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CHARRANGE
    {
      /// <summary>
      /// Character position index immediately preceding the first character in the range. 
      /// </summary>
      public int CharacterPositionMin;

      /// <summary>
      /// Character position immediately following the last character in the range. 
      /// </summary>
      public int CharacterPositionMax;
    }

    /// <summary>
    /// The FORMATRANGE structure contains information that a rich edit control 
    /// uses to format its output for a particular device. This structure is 
    /// used with the EM_FORMATRANGE message.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct FORMATRANGE
    {
      /// <summary>
      /// Device to render to.
      /// </summary>
      public IntPtr Hdc;

      /// <summary>
      /// Target device to format for. 
      /// </summary>
      public IntPtr HdcTarget;

      /// <summary>
      /// Area to render to. Units are measured in twips. 
      /// </summary>
      public RECT Area;

      /// <summary>
      /// Entire area of rendering device. Units are measured in twips. 
      /// </summary>
      public RECT AreaPage;

      /// <summary>
      /// <see cref="CHARRANGE"/> structure that specifies the range of text to format. 
      /// </summary>
      public CHARRANGE Chrg;
    }

    #endregion //STRUCTS
  }
}
