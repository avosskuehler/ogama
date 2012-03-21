namespace Ogama.Modules.Recording.Presenter
{
  using System;
  using System.Diagnostics;
  using System.Runtime.InteropServices;

  public class MessageHook
  {
    private const int WH_MOUSE_LL = 14;
    private const int WH_KEYBOARD_LL = 13;
    public const int WM_KEYDOWN = 0x0100;

    public enum MouseMessages
    {
      WM_LBUTTONDOWN = 0x0201,
      WM_LBUTTONUP = 0x0202,
      WM_MOUSEMOVE = 0x0200,
      WM_MOUSEWHEEL = 0x020A,
      WM_RBUTTONDOWN = 0x0204,
      WM_RBUTTONUP = 0x0205
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct Point
    {
      public int x;
      public int y;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MSLLHOOKSTRUCT
    {
      public Point pt;
      public uint mouseData;
      public uint flags;
      public uint time;
      public IntPtr dwExtraInfo;
    }


    public static IntPtr SetKeyboardHook(LowLevelProc proc)
    {
      using (var curProcess = Process.GetCurrentProcess())
      using (var curModule = curProcess.MainModule)
      {
        return SetWindowsHookEx(WH_KEYBOARD_LL, proc, GetModuleHandle(curModule.ModuleName), 0);
      }
    }

    public static IntPtr SetMouseHook(LowLevelProc proc)
    {
      using (var curProcess = Process.GetCurrentProcess())
      using (var curModule = curProcess.MainModule)
      {
        return SetWindowsHookEx(WH_MOUSE_LL, proc, GetModuleHandle(curModule.ModuleName), 0);
      }
    }

    public delegate IntPtr LowLevelProc(int nCode, IntPtr wParam, IntPtr lParam);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern IntPtr SetWindowsHookEx(int idHook,
        LowLevelProc lpfn, IntPtr hMod, uint dwThreadId);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool UnhookWindowsHookEx(IntPtr hhk);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode,
        IntPtr wParam, IntPtr lParam);

    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern IntPtr GetModuleHandle(string lpModuleName);
  }
}
