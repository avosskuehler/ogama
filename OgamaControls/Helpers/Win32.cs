using System;
using System.Runtime.InteropServices;

namespace OgamaControls
{
  /// <summary>
  /// Helper class containing User32 API functions
  /// </summary>
  public class User32
  {
    /// <summary>
    /// Needed constant for ListViewMessages
    /// </summary>
    public const int LVM_FIRST = 0x1000;

    /// <summary>
    /// A constant for icon spacing in list views.
    /// </summary>
    public const int LVM_SETICONSPACING = LVM_FIRST + 53;
    
    /// <summary>
    /// The VkKeyScan function translates a character to the corresponding
    /// virtual-key code and shift state for the current keyboard.
    /// </summary>
    /// <remarks>Used in KeyEntersEditMode function</remarks>
    /// <param name="key">Specifies the character to be translated into a virtual-key code. </param>
    /// <returns>If the function succeeds, the low-order byte of the 
    /// return value contains the virtual-key code and the high-order 
    /// byte contains the shift state, which can be a combination of the 
    /// following flag bits. If the function finds no key that translates 
    /// to the passed character code, both the low-order and high-order bytes contain –1.
    /// </returns>
    [DllImport("USER32.DLL", CharSet = CharSet.Auto)]
    public static extern short VkKeyScan(char key);

    /// <summary>
    /// Needed to forward keyboard messages to the child TextBox control.
    /// </summary>
    /// <param name="hWnd">Handle of window to send the message to.</param>
    /// <param name="msg">Specifies the message to be sent.</param>
    /// <param name="wParam">Specifies additional message-dependent information.</param>
    /// <param name="lParam">Specifies additional message-dependent information.</param>
    /// <returns>The result of the message processing; its value depends on the message sent.</returns>
    [DllImport("USER32.DLL", CharSet = CharSet.Auto)]
    public static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

    /// <summary>
    /// The MsgWaitForMultipleObjects function returns when any one or 
    /// all of the specified objects are in the signaled state or 
    /// the time-out interval elapses. The objects can include input 
    /// event objects, which you specify using the dwWakeMask parameter.
    /// </summary>
    /// <param name="nCount">[in] Number of object handles in the array 
    /// pointed to by pHandles. The maximum number of object 
    /// handles is MAXIMUM_WAIT_OBJECTS minus one. </param>
    /// <param name="pHandles">[in] Pointer to an array of object
    /// handles. For a list of the object types whose handles can 
    /// be specified, see the following Remarks section. The array
    /// can contain handles of objects of different types. It may 
    /// not contain multiple copies of the same handle. 
    /// If one of these handles is closed while the wait is still 
    /// pending, the function's behavior is undefined.</param>
    /// <param name="bWaitAll">[in] If this parameter is TRUE, 
    /// the function returns when the states of all objects 
    /// in the pHandles array have been set to signaled and 
    /// an input event has been received. If this parameter 
    /// is FALSE, the function returns when the state of any one 
    /// of the objects is set to signaled or an input event has been 
    /// received. In this case, the return value indicates the object 
    /// whose state caused the function to return. </param>
    /// <param name="dwMilliseconds">[in] Time-out interval, in milliseconds. 
    /// The function returns if the interval elapses, even if the 
    /// criteria specified by the bWaitAll or dwWakeMask 
    /// parameter have not been met. If dwMilliseconds is 
    /// zero, the function tests the states of the specified 
    /// objects and returns immediately. If dwMilliseconds is 
    /// INFINITE, the function's time-out interval never elapses.</param>
    /// <param name="dwWakeMask">[in] Input types for which an input
    /// event object handle will be added to the array of object handles. </param>
    /// <returns>If the function succeeds, the return value indicates the 
    /// event that caused the function to return.</returns>
    [DllImport("user32.dll")]
    public static extern int MsgWaitForMultipleObjects(
        int nCount,		// number of handles in array
        int pHandles,	// object-handle array
        bool bWaitAll,	// wait option
        int dwMilliseconds,	// time-out interval
        int dwWakeMask	// input-event type
        );
  }

  /// <summary>
  /// This static class exposes methods from the Kernel32.dll to C#.
  /// </summary>
  public static class Kernel32
  {
    /// <summary>
    /// The CopyMemory function copies a block of memory from one location to another. 
    /// </summary>
    /// <param name="Destination">Pointer to the starting address of the copied block's destination.</param>
    /// <param name="Source">Pointer to the starting address of the block of memory to copy.</param>
    /// <param name="Length">Size of the block of memory to copy, in bytes.</param>
    [DllImport("Kernel32.dll", EntryPoint = "RtlMoveMemory")]
    public static extern void CopyMemory(IntPtr Destination, IntPtr Source, int Length);
  }
}

