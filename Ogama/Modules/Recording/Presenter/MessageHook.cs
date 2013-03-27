// <copyright file="MessageHook.cs" company="FU Berlin">
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

namespace Ogama.Modules.Recording.Presenter
{
  using System;
  using System.Diagnostics;
  using System.Runtime.InteropServices;

  /// <summary>
  /// This class is used to insert low level global mouse and keyboard
  /// hooks in the message loop.
  /// </summary>
  public class MessageHook
  {
    /// <summary>
    /// Posted to the window with the keyboard focus when a nonsystem 
    /// key is pressed. A nonsystem key is a key that is pressed 
    /// when the ALT key is not pressed. 
    /// </summary>
    public const int WMKEYDOWN = 0x0100;

    /// <summary>
    /// The WH_MOUSE_LL hook enables you to monitor mouse input events 
    /// about to be posted in a thread input queue. 
    /// </summary>
    private const int WHMOUSELL = 14;

    /// <summary>
    /// The WH_MOUSE_LL hook enables you to monitor mouse input 
    /// events about to be posted in a thread input queue. 
    /// </summary>
    private const int WHKEYBOARDLL = 13;

    /// <summary>
    /// An application-defined or library-defined callback function used with the 
    /// SetWindowsHookEx function. The system call this function every time a 
    /// new mouse or keyboard input event is about to be posted into a thread input queue. 
    /// </summary>
    /// <param name="ncode">A code the hook procedure uses to determine how 
    /// to process the message. If nCode is less than zero, the hook procedure 
    /// must pass the message to the CallNextHookEx function without further processing 
    /// and should return the value returned by CallNextHookEx</param>
    /// <param name="wparam">The identifier of the mouse message. This parameter can be one 
    /// of the following messages: WM_LBUTTONDOWN, WM_LBUTTONUP, WM_MOUSEMOVE, 
    /// WM_MOUSEWHEEL, WM_MOUSEHWHEEL, WM_RBUTTONDOWN, or WM_RBUTTONUP.</param>
    /// <param name="lparam">A pointer to an MSLLHOOKSTRUCT structure.</param>
    /// <returns>If nCode is less than zero, the hook procedure must return the value returned by CallNextHookEx. 
    /// If nCode is greater than or equal to zero, and the hook procedure did 
    /// not process the message, it is highly recommended that you call 
    /// CallNextHookEx and return the value it returns; otherwise, 
    /// other applications that have installed WH_MOUSE_LL hooks will not 
    /// receive hook notifications and may behave incorrectly as a result. 
    /// If the hook procedure processed the message, it may return a nonzero #
    /// value to prevent the system from passing the message to the rest of the 
    /// hook chain or the target window procedure.</returns>
    public delegate IntPtr LowLevelProc(int ncode, IntPtr wparam, IntPtr lparam);

    /// <summary>
    /// The mouse messages that a mouse hook may receive.
    /// </summary>
    public enum MouseMessages
    {
      /// <summary>
      /// The WM_LBUTTONDOWN message is posted when the user presses
      /// the left mouse button while the cursor is in the client area of a window. 
      /// If the mouse is not captured, the message is posted to the window beneath the cursor. 
      /// Otherwise, the message is posted to the window that has captured the mouse.
      /// </summary>
      WM_LBUTTONDOWN = 0x0201,

      /// <summary>
      /// The WM_LBUTTONUP message is posted when the user releases the 
      /// left mouse button while the cursor is in the client area of a window.
      ///  If the mouse is not captured, the message is posted to the window 
      /// beneath the cursor. Otherwise, the message is posted to the window that has captured the mouse
      /// </summary>
      WM_LBUTTONUP = 0x0202,

      /// <summary>
      /// The WM_MOUSEMOVEmessage is posted to a window when the cursor moves. 
      /// If the mouse is not captured, the message is posted to 
      /// the window that contains the cursor. Otherwise, the message 
      /// is posted to the window that has captured the mouse. 
      /// </summary>
      WM_MOUSEMOVE = 0x0200,

      /// <summary>
      /// The WM_MOUSEWHEELmessage is sent to the focus window when 
      /// the mouse wheel is rotated. The DefWindowProcfunction propagates
      ///  the message to the window's parent. There should be no internal 
      /// forwarding of the message, since DefWindowProcpropagates it up the
      ///  parent chain until it finds a window that processes it. 
      /// </summary>
      WM_MOUSEWHEEL = 0x020A,

      /// <summary>
      /// The WM_RBUTTONDOWNmessage is posted when the user presses the 
      /// right mouse button while the cursor is in the client area of 
      /// a window. If the mouse is not captured, the message is posted
      ///  to the window beneath the cursor. Otherwise, the message is 
      /// posted to the window that has captured the mouse.
      /// </summary>
      WM_RBUTTONDOWN = 0x0204,

      /// <summary>
      /// The WM_RBUTTONUPmessage is posted when the user releases 
      /// the right mouse button while the cursor is in the client area 
      /// of a window. If the mouse is not captured, the message is posted
      ///  to the window beneath the cursor. Otherwise, the message is posted 
      /// to the window that has captured the mouse. 
      /// </summary>
      WM_RBUTTONUP = 0x0205
    }

    /// <summary>
    /// Uses SetWindowsHookEx to set a keyboard hook with the given
    /// LowLevelProc callback method.
    /// </summary>
    /// <param name="proc">The callback method to be called.</param>
    /// <returns>If the function succeeds, the return value is the handle to the hook procedure. 
    /// If the function fails, the return value is NULL.</returns>
    public static IntPtr SetKeyboardHook(LowLevelProc proc)
    {
      using (var curProcess = Process.GetCurrentProcess())
      using (var curModule = curProcess.MainModule)
      {
        return SetWindowsHookEx(WHKEYBOARDLL, proc, GetModuleHandle(curModule.ModuleName), 0);
      }
    }

    /// <summary>
    /// Uses SetWindowsHookEx to set a mouse hook with the given
    /// LowLevelProc callback method.
    /// </summary>
    /// <param name="proc">The callback method to be called.</param>
    /// <returns>If the function succeeds, the return value is the handle to the hook procedure. 
    /// If the function fails, the return value is NULL.</returns>
    public static IntPtr SetMouseHook(LowLevelProc proc)
    {
      using (var curProcess = Process.GetCurrentProcess())
      using (var curModule = curProcess.MainModule)
      {
        return SetWindowsHookEx(WHMOUSELL, proc, GetModuleHandle(curModule.ModuleName), 0);
      }
    }

    /// <summary>
    /// Removes a hook procedure installed in a hook chain by the SetWindowsHookEx function. 
    /// </summary>
    /// <param name="hhk">A handle to the hook to be removed. This parameter 
    /// is a hook handle obtained by a previous call to SetWindowsHookEx. </param>
    /// <returns>If the function succeeds, the return value is nonzero.
    /// If the function fails, the return value is zero. </returns>
    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool UnhookWindowsHookEx(IntPtr hhk);

    /// <summary>
    /// Passes the hook information to the next hook procedure in the current hook chain. 
    /// A hook procedure can call this function either before or after processing the hook information. 
    /// </summary>
    /// <param name="hhk">This parameter is ignored. </param>
    /// <param name="ncode">The hook code passed to the current hook procedure. The next hook procedure uses this 
    /// code to determine how to process the hook information.</param>
    /// <param name="wparam">The wParam value passed to the current hook procedure. The meaning of 
    /// this parameter depends on the type of hook associated with the current hook chain.</param>
    /// <param name="lparam">The lParam value passed to the current hook procedure. The meaning of 
    /// this parameter depends on the type of hook associated with the current hook chain.</param>
    /// <returns>This value is returned by the next hook procedure in the chain. 
    /// The current hook procedure must also return this value. 
    /// The meaning of the return value depends on the hook type. 
    /// For more information, see the descriptions of the individual hook procedures.</returns>
    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern IntPtr CallNextHookEx(
      IntPtr hhk, 
      int ncode,
      IntPtr wparam, 
      IntPtr lparam);

    /// <summary>
    /// Retrieves a module handle for the specified module. 
    /// The module must have been loaded by the calling process.
    /// </summary>
    /// <param name="moduleName">The name of the loaded module (either a .dll or .exe file). 
    /// If the file name extension is omitted, the default library extension .dll is appended. </param>
    /// <returns>If the function succeeds, the return value is a handle to the specified module.
    /// If the function fails, the return value is NULL.</returns>
    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern IntPtr GetModuleHandle(string moduleName);

    /// <summary>
    /// Installs an application-defined hook procedure into a hook chain. 
    /// You would install a hook procedure to monitor the system for certain 
    /// types of events. These events are associated either with a specific 
    /// thread or with all threads in the same desktop as the calling thread.
    /// </summary>
    /// <param name="hookID">The type of hook procedure to be installed. </param>
    /// <param name="callback">A pointer to the hook procedure.</param>
    /// <param name="module">A handle to the DLL containing the hook procedure pointed to by the lpfn parameter. </param>
    /// <param name="threadId">The identifier of the thread with which the hook procedure is to be associated.</param>
    /// <returns>If the function succeeds, the return value is the handle to the hook procedure. 
    /// If the function fails, the return value is NULL.</returns>
    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern IntPtr SetWindowsHookEx(
      int hookID,
      LowLevelProc callback,
      IntPtr module,
      uint threadId);

    /// <summary>
    /// The POINT structure defines the x- and y- coordinates of a point. 
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct Point
    {
      /// <summary>
      /// The x-coordinate of the point.
      /// </summary>
      public int X;

      /// <summary>
      /// The y-coordinate of the point.
      /// </summary>
      public int Y;
    }

    /// <summary>
    /// Contains information about a low-level mouse input event. 
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct MSLLHOOKSTRUCT
    {
      /// <summary>
      /// The x- and y-coordinates of the cursor, in screen coordinates. 
      /// </summary>
      public Point Point;

      /// <summary>
      /// If the message is WM_MOUSEWHEEL, the high-order word of this member 
      /// is the wheel delta. The low-order word is reserved.
      ///  A positive value indicates that the wheel was rotated 
      /// forward, away from the user; a negative value indicates that 
      /// the wheel was rotated backward, toward the user. One wheel click 
      /// is defined as WHEEL_DELTA, which is 120. 
      /// If the message is WM_XBUTTONDOWN, WM_XBUTTONUP, WM_XBUTTONDBLCLK, 
      /// WM_NCXBUTTONDOWN, WM_NCXBUTTONUP, or WM_NCXBUTTONDBLCLK, 
      /// the high-order word specifies which X button was pressed or
      ///  released, and the low-order word is reserved. This value can 
      /// be one or more of the following values. Otherwise, MouseData is not used. 
      /// </summary>
      public uint MouseData;

      /// <summary>
      /// The event-injected flag.
      /// </summary>
      public uint Flags;

      /// <summary>
      /// The time stamp for this message. 
      /// </summary>
      public uint Time;

      /// <summary>
      /// Additional information associated with the message. 
      /// </summary>
      public IntPtr ExtraInfo;
    }
  }
}
