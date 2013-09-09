using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace GTApplication.Tools
{
    internal class MouseDriver
    {
        // Redirects the gaze coordinates (X,Y) to replace the mouse pointer on a system level
        // this gives access to a whole lot of functions in U.I programming such as MouseEnter/Leave etc.

        // Based on the Pinvoke.net tutorial on SendInput(User32)
        // HTTP://www.pinvoke.net/default.aspx/user32.SendInput


        [DllImport("user32.dll", EntryPoint = "SendInput", SetLastError = true)]
        private static extern uint SendInput(uint nInputs, INPUT[] pInputs, int cbSize);

        [DllImport("user32.dll", EntryPoint = "GetMessageExtraInfo", SetLastError = true)]
        private static extern IntPtr GetMessageExtraInfo();

        /// <summary>
        /// This function moves the cursor to a specific point at the screen.
        /// </summary>
        /// <param name="x">X coordinate of the posotion as pixel</param>
        /// <param name="y">Y coordinate of the posotion as pixel</param>
        /// <returns>Returns 0 if there was an error otherwise 1.</returns>
        /// 
        public void Move(double x, double y)
        {
            double ScreenWidth = Screen.PrimaryScreen.Bounds.Width;
            double ScreenHeight = Screen.PrimaryScreen.Bounds.Height;

            var input_move = new INPUT();
            input_move.mi.dx = (int) Math.Round(x*(65535/ScreenWidth), 0);
            input_move.mi.dy = (int) Math.Round(y*(65535/ScreenHeight), 0);

            input_move.mi.mouseData = 0;
            input_move.mi.dwFlags = (int) (MOUSEEVENTF.MOVE | MOUSEEVENTF.ABSOLUTE);

            INPUT[] input = {input_move};
            SendInput(1, input, Marshal.SizeOf(input_move));
        }

        /// <summary>
        /// Instancely not used in the gaze project /Martin
        /// This function simulates a simple mouseclick at the Instance cursor position.
        /// </summary>
        /// <returns>All right if it is 2. All below indicates an error.</returns>
        public static uint Click()
        {
            INPUT input_down = new INPUT();
            input_down.mi.dx = 0;
            input_down.mi.dy = 0;
            input_down.mi.mouseData = 0;
            input_down.mi.dwFlags = (int) MOUSEEVENTF.LEFTDOWN;

            INPUT input_up = input_down;
            input_up.mi.dwFlags = (int) MOUSEEVENTF.LEFTUP;

            INPUT[] input = {input_down, input_up};
            return SendInput(2, input, Marshal.SizeOf(input_down));
        }

        #region Nested type: HARDWAREINPUT

        [StructLayout(LayoutKind.Sequential)]
        private struct HARDWAREINPUT
        {
            public readonly int uMsg;
            public readonly short wParamL;
            public readonly short wParamH;
        }

        #endregion

        #region Nested type: INPUT

        [StructLayout(LayoutKind.Explicit)]
        private struct INPUT
        {
            [FieldOffset(0)] public readonly int type;
            [FieldOffset(4)] public MOUSEINPUT mi;
            [FieldOffset(4)] public readonly KEYBDINPUT ki;
            [FieldOffset(4)] public readonly HARDWAREINPUT hi;
        }

        #endregion

        #region Nested type: InputType

        private enum InputType
        {
            INPUT_MOUSE = 0,
            INPUT_KEYBOARD = 1,
            INPUT_HARDWARE = 2,
        }

        #endregion

        #region Nested type: KEYBDINPUT

        [StructLayout(LayoutKind.Sequential)]
        private struct KEYBDINPUT
        {
            public readonly short wVk;
            public readonly short wScan;
            public readonly int dwFlags;
            public readonly int time;
            public readonly IntPtr dwExtraInfo;
        }

        #endregion

        #region Nested type: KEYEVENTF

        [Flags]
        private enum KEYEVENTF
        {
            EXTENDEDKEY = 0x0001,
            KEYUP = 0x0002,
            UNICODE = 0x0004,
            SCANCODE = 0x0008,
        }

        #endregion

        #region Nested type: MOUSEEVENTF

        [Flags]
        private enum MOUSEEVENTF
        {
            MOVE = 0x0001, // mouse move
            LEFTDOWN = 0x0002, // left button down
            LEFTUP = 0x0004, // left button up
            RIGHTDOWN = 0x0008, // right button down
            RIGHTUP = 0x0010, // right button up
            MIDDLEDOWN = 0x0020, // middle button down
            MIDDLEUP = 0x0040, // middle button up
            XDOWN = 0x0080, // x button down
            XUP = 0x0100, // x button down
            WHEEL = 0x0800, // wheel button rolled
            VIRTUALDESK = 0x4000, // map to entire virtual desktop
            ABSOLUTE = 0x8000, // absolute move
        }

        #endregion

        #region Nested type: MOUSEINPUT

        [StructLayout(LayoutKind.Sequential)]
        private struct MOUSEINPUT
        {
            public int dx;
            public int dy;
            public int mouseData;
            public int dwFlags;
            public readonly int time;
            public readonly IntPtr dwExtraInfo;
        }

        #endregion
    }
}