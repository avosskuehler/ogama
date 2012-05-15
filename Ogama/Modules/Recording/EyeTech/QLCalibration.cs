using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;

using Ogama.Modules.Recording.EyeTech;

namespace QuickLinkAPI
{
  /// <summary>
  /// This form is used to to start a calibration for the EyeTech Tracker. 
  /// As soon as it is shown, the calibration will start based upon the
  /// settings found in QuickGlance.
  /// </summary>
  public partial class QLCalibration : Form
  {
    // Used for hiding the taskbar to achieve real fullscreen.
    [DllImport("user32.dll")]
        private static extern int FindWindow(string lpszClassName, string lpszWindowName);
        [DllImport("user32.dll")]
        private static extern int ShowWindow(int hWnd, int nCmdShow);
        private const int SW_HIDE = 0;
        private const int SW_SHOW = 1;

        public QLCalibration()
        {
            InitializeComponent();
        }

        private void QLCalibration_Load(object sender, EventArgs e)
        {
          // Calibration result.
          CalibrationErrorEx calibrationResult = CalibrationErrorEx.CALIBRATIONEX_NOT_INITIALIZED;

          // Containers for calibration results.
          double scoreLeft = 0.0;
          double scoreRight = 0.0;

          // Hide cursor and taskbar.
          Cursor.Hide();
          int hWnd = FindWindow("Shell_TrayWnd", "");
          ShowWindow(hWnd, SW_HIDE);

          // Set window options to fullscreen
          FormBorderStyle = FormBorderStyle.None;
          this.Location = new Point(0, 0);
          this.WindowState = FormWindowState.Maximized;

          // Start custom calibration method.
          Calibrate();

          calibrationResult = NativeMethods.GetScoreEx(ref scoreLeft, ref scoreRight);
          //label2.Text = "Left: " + scoreLeft + " Right: " + scoreRight;

          // Restore mouse and show results
          Cursor.Show();

          if (calibrationResult == CalibrationErrorEx.CALIBRATIONEX_OK)
          {
            MessageBox.Show("Left: " + scoreLeft + " Right: " + scoreRight, "Calibrationresults", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
          }
          else
          {
            MessageBox.Show("An error occured (" + Enum.GetName(typeof(CalibrationErrorEx), calibrationResult) + "), but calibration is still applied", "Calibrationresults", MessageBoxButtons.OK, MessageBoxIcon.Asterisk); 
          }

          NativeMethods.ApplyCalibrationEx();

          // Exit calibration form and restore taskbar.
          this.Close();

          int hwnd = FindWindow("Shell_TrayWnd", "");
          ShowWindow(hwnd, SW_SHOW);
        }

        private void Calibrate()
        {
          // SDK calibration variables.
          int posX = 0;
          int posY = 0;
          int targetHandle = 0;

          int radius = 10;

          CalibrationOptions calibration = new CalibrationOptions();
          NativeMethods.GetCalibrationOptions(ref calibration);

          // Time not needed?
          int calibrationTime = calibration.Calibration_TargetTime;
          int calibrationPoints = (int)calibration.Calibration_Style;

          // Convert the found calibrationpoints from an enum to a integer.
          switch (calibrationPoints)
          {
            case (int)CalibrationStyle.CAL_STYLE_5_POINT:
              calibrationPoints = 5;
              break;
            case (int)CalibrationStyle.CAL_STYLE_9_POINT:
              calibrationPoints = 9;
              break;
            case (int)CalibrationStyle.CAL_STYLE_16_POINT:
              calibrationPoints = 16;
              break;
            default:
              calibrationPoints = 0;
              break;
          }

          // Create drawing figures.
          SolidBrush myBrush = new SolidBrush(Color.LightSkyBlue);
          SolidBrush myBrush2 = new SolidBrush(Color.LightSteelBlue);
          SolidBrush myBrush3 = new SolidBrush(Color.CadetBlue);
          System.Drawing.Graphics formGraphics = this.CreateGraphics();

          // Prepare tracker for calibration with a devilish calibration index.
          NativeMethods.InitializeCalibrationEx(666);

          // Loop over the amount of calibration points.
          for (int i = 0; i < calibrationPoints; i++)
          {
            // Retrieve the coordinate to be calibrated
            NativeMethods.GetNewTargetPositionEx(ref posX, ref posY, ref targetHandle);

            // Clean previous drawings
            formGraphics.Clear(Color.LightGray);

            // Draw the figures at the retrieved coordinate.
            formGraphics.FillEllipse(myBrush2, new Rectangle(posX - (4 * radius), posY - (4 * radius), 8 * radius, 8 * radius));
            formGraphics.FillEllipse(myBrush, new Rectangle(posX - radius, posY - radius, 2 * radius, 2 * radius));

            // Allow time for the user to find and look at the target.
            Thread.Sleep(1000);

            // Draw a smaller circle to indicate that calibration started.
            formGraphics.FillEllipse(myBrush3, new Rectangle(posX - radius, posY - radius, 2 * radius, 2 * radius));

            // Calibrate.
            NativeMethods.CalibrateEx(targetHandle);
          }

          formGraphics.Clear(Color.LightGray);

          myBrush.Dispose();
          myBrush2.Dispose();
          myBrush3.Dispose();
          formGraphics.Dispose();
        }
  }
}
