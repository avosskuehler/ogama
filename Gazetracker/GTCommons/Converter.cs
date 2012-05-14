using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Media;
using Color = System.Windows.Media.Color;


namespace GTCommons
{
    public class Converter
    {
        public static SolidColorBrush GetColorFromString(string str)
        {
            string[] td = str.Split(' ');
            var _bytes = new byte[td.Length];
            int i = 0;

            foreach (string s in td)
            {
                try
                {
                    _bytes.SetValue(byte.Parse(s), i);
                    i++;
                }
                catch (Exception ex)
                {
                    _bytes = null;
                    Console.Out.WriteLine("Error in GetColorFromString: " + ex.Message);
                }
            }

            return new SolidColorBrush(Color.FromRgb(_bytes[0], _bytes[1], _bytes[2]));
        }


        [DllImport("user32.dll")]
        private static extern IntPtr GetDC(IntPtr hwnd);

        public static double ConvertPixelsToCentimeters(double width)
        {
            Single xDpi, yDpi;

            using (Graphics g = Graphics.FromHdc(GetDC(IntPtr.Zero)))
            {
                xDpi = g.DpiX;
                yDpi = g.DpiY;
            }
            return (width/xDpi)*2.54;
        }
    }
}