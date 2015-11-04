using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace OgamaControls
{
  /// <summary>
  /// ColorSelectControl - A control that lets the user rapidly select a color by picking
  /// a specific hue, then adjusting luminance/brightness and saturation to tweak it. The 
  /// user may also left click on the selected color area to get the more precise but 
  /// clumsier color pick dialog provided by Windows.
  /// </summary>
  /// <remarks>Created on 02/19/2005 by Kevin Menningen
  /// This code is released to the public domain for any use, private or commercial.
  /// You may modify this code and include it in any project. Please leave this comment
  /// section in the code.
  /// Added transpareny option - Adrian Voßkühler 03.08.08</remarks>
  [ToolboxBitmap(typeof(resfinder), "OgamaControls.CommonControls.Styles.ColorSelectControl.bmp")]
  public partial class ColorSelectControl : UserControl
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
    /// The height of the hue arrow.
    /// </summary>
    private static int HUE_SELECT_ARROW_HEIGHT = 3;

    /// <summary>
    /// The hue value. Varies 0.0f - 360.0f (degrees)
    /// </summary>
    float m_Hue;

    /// <summary>
    /// The Luminance Value, varies 0.0f - 1.0f
    /// </summary>
    float m_Luminance;

    /// <summary>
    /// The Saturation value,varies 0.0f - 1.0f;
    /// </summary>
    float m_Saturation;

    /// <summary>
    /// The Transparency value,varies 0.0f - 1.0f;
    /// </summary>
    float m_Transparency;

    /// <summary>
    /// maximum scroll bar value
    /// </summary>
    float m_LumMax;

    /// <summary>
    /// maximum scroll bar value
    /// </summary>
    float m_SatMax;

    /// <summary>
    /// maximum scroll bar value
    /// </summary>
    float m_TransMax;

    /// <summary>
    /// amount of hue per pixel
    /// </summary>
    float m_HuePixelStep;

    /// <summary>
    /// Event handler. Raised, when controls parameters has changed.
    /// </summary>
    public event EventHandler<ColorChangedEventArgs> ColorChanged;

    /// <summary>
    /// Saves which button engaged the timer
    /// </summary>
    MouseButtons m_Button;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets or sets the selected color. You might set it prior to showing the control
    /// so that the control is initialized using a default or previously stored color.
    /// </summary>
    public Color SelectedColor
    {
      get
      {
        return HSLToRGB(m_Hue, m_Saturation, m_Luminance, m_Transparency);
      }
      set
      {
        RGBToHSL(value, ref m_Hue, ref m_Saturation, ref m_Luminance, ref m_Transparency);
        // update scroll bar positions
        wndScrollBrightness.Value = (int)(m_LumMax * m_Luminance + 0.5f);
        wndScrollSaturation.Value = (int)(m_SatMax * m_Saturation + 0.5f);
        wndScrollTranspareny.Value = (int)(m_TransMax * m_Transparency + 0.5f);

        nudA.Value = value.A;
        nudR.Value = value.R;
        nudG.Value = value.G;
        nudB.Value = value.B;

        NotifyColorChange();
      }
    } // property SelectedColor

    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Constructor. Initializes control with default values.
    /// </summary>
    public ColorSelectControl()
    {
      InitializeComponent();

      // important calculation for matching a screen pixel to a hue amount
      m_HuePixelStep = 360.0f / ((float)panelHue.Width);
      m_Hue = 240.0f;
      m_Luminance = 0.5f;
      m_Saturation = 0.5f;
      m_Transparency = 0.0f;

      // fast double buffering for flicker-free drawing
      SetStyle(
         ControlStyles.AllPaintingInWmPaint |
         ControlStyles.UserPaint |
         ControlStyles.DoubleBuffer |
         ControlStyles.ResizeRedraw, true);
    }

    /// <summary>
    /// The control is being initialized for the first time
    /// </summary>
    /// <param name="sender">Event sender</param>
    /// <param name="e">Event parameters</param>
    private void ColorSelectControl_Load(object sender, EventArgs e)
    {
      // see the MSDN article on the ScrollBar Maximum property for the reason behind this equation
      m_LumMax = (float)(wndScrollBrightness.Maximum - wndScrollBrightness.LargeChange + 1);
      if (m_LumMax <= 0.0f)
      {
        wndScrollBrightness.LargeChange = 10;
        wndScrollBrightness.Maximum = 109;
        wndScrollBrightness.Minimum = 1;
        m_LumMax = 100.0f;
      }
      // see the MSDN article on the ScrollBar Maximum property for the reason behind this equation
      m_SatMax = (float)(wndScrollSaturation.Maximum - wndScrollSaturation.LargeChange + 1);
      if (m_SatMax <= 0.0f)
      {
        wndScrollSaturation.LargeChange = 10;
        wndScrollSaturation.Maximum = 109;
        wndScrollSaturation.SmallChange = 1;
        m_SatMax = 100.0f;
      }
      // see the MSDN article on the ScrollBar Maximum property for the reason behind this equation
      m_TransMax = (float)(wndScrollTranspareny.Maximum - wndScrollTranspareny.LargeChange + 1);
      if (m_TransMax <= 0.0f)
      {
        wndScrollTranspareny.LargeChange = 10;
        wndScrollTranspareny.Maximum = 109;
        wndScrollTranspareny.SmallChange = 1;
        m_TransMax = 100.0f;
      }
      wndScrollBrightness.Value = (int)(m_LumMax * m_Luminance + 0.5f);
      wndScrollSaturation.Value = (int)(m_SatMax * m_Saturation + 0.5f);
      wndScrollTranspareny.Value = (int)(m_TransMax * m_Transparency + 0.5f);

      Color color = HSLToRGB(m_Hue, m_Saturation, m_Luminance, m_Transparency);
      nudA.Value = color.A;
      nudR.Value = color.R;
      nudG.Value = color.G;
      nudB.Value = color.B;
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler                                                              //
    ///////////////////////////////////////////////////////////////////////////////
    #region EVENTS

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler for UI, Menu, Buttons, Toolbars etc.                         //
    ///////////////////////////////////////////////////////////////////////////////
    #region WINDOWSEVENTHANDLER

    /// <summary>
    /// The <see cref="Control.Paint"/> event handler for
    /// the <see cref="Panel"/> panelHue.
    /// Redraws the hue panel.
    /// </summary>
    /// <param name="sender">Event sender</param>
    /// <param name="e">Paint arguments</param>
    private void panelHue_Paint(object sender, PaintEventArgs e)
    {
      Graphics g = e.Graphics;

      //// fill in the background 
      //SolidBrush brBack = new SolidBrush(this.BackColor);
      //g.FillRectangle(brBack, this.ClientRectangle);

      // Paint the checkerboard style background 
      Rectangle hueRect = new Rectangle(0, HUE_SELECT_ARROW_HEIGHT + 1,
           panelHue.Width, panelHue.Height - 2 * HUE_SELECT_ARROW_HEIGHT - 2);

      using (HatchBrush hb = new HatchBrush(HatchStyle.LargeCheckerBoard, Color.White, Color.LightGray))
        e.Graphics.FillRectangle(hb, hueRect);

      // draw the hue bar
      int kk = 0;
      int NumShades = panelHue.Width;
      float Hue = 0.0f;
      float HueMinimumDiff = float.MaxValue;
      float HueAtMinimum = 0.0f;
      for (kk = 0; kk < NumShades; kk++)
      {
        // draw the hue shade
        Color clrShade = HSLToRGB(Hue, m_Saturation, m_Luminance, m_Transparency);
        Pen penColor = new Pen(clrShade, 1.0f);
        g.DrawLine(penColor,
           kk,
           HUE_SELECT_ARROW_HEIGHT + 1,
           kk,
           panelHue.Height - 2 * HUE_SELECT_ARROW_HEIGHT + 1);

        // are we closest to the selected value?
        float Diff = Math.Abs(m_Hue - Hue);
        if (Diff < HueMinimumDiff)
        {
          HueMinimumDiff = Diff;
          HueAtMinimum = Hue;
        }

        // go to the next Hue
        Hue += m_HuePixelStep;
      }

      // determine the X-offset of the currently selected hue
      int XOffset = 0;
      if (m_HuePixelStep > 0.0f)
      {
        XOffset = (int)(m_Hue / m_HuePixelStep);
      }

      // draw the arrows for the currently selected color
      AdjustableArrowCap myArrow = new AdjustableArrowCap(HUE_SELECT_ARROW_HEIGHT * 2 - 1,
         HUE_SELECT_ARROW_HEIGHT, true);

      Pen capPen = new Pen(Color.Black);
      capPen.CustomEndCap = myArrow;
      g.DrawLine(capPen,
         XOffset,
         0,
         XOffset,
         0 + HUE_SELECT_ARROW_HEIGHT + 1);

      g.DrawLine(capPen,
         XOffset,
         panelHue.Bottom,
         XOffset,
         panelHue.Bottom - (2 * HUE_SELECT_ARROW_HEIGHT + 2));
    }

    /// <summary>
    /// Draw the control event
    /// </summary>
    /// <param name="sender">Event sender</param>
    /// <param name="e">Paint arguments</param>
    private void ColorSelectControl_Paint(object sender, PaintEventArgs e)
    {
      // Draw the selected color area
      Color clrSelected = HSLToRGB(m_Hue, m_Saturation, m_Luminance, m_Transparency);
      panelSelectedColor.BackColor = clrSelected;
    }

    /// <summary>
    /// Draws the preview panel with the current selected color.
    /// </summary>
    /// <param name="sender">Event sender</param>
    /// <param name="e">Event arguments</param>
    private void panelSelectedColor_Paint(object sender, PaintEventArgs e)
    {
      // Paint the checkerboard style background 
      using (HatchBrush hb = new HatchBrush(HatchStyle.LargeCheckerBoard, Color.White, Color.LightGray))
        e.Graphics.FillRectangle(hb, e.ClipRectangle);
      e.Graphics.FillRectangle(new SolidBrush(panelSelectedColor.BackColor), e.ClipRectangle);
    }

    /// <summary>
    /// User is moving the the scroll control for brightness/luminance
    /// </summary>
    /// <param name="sender">Event sender</param>
    /// <param name="e">Event arguments</param>
    private void wndScrollBrightness_Scroll(object sender, ScrollEventArgs e)
    {
      m_Luminance = e.NewValue / m_LumMax;
      NotifyColorChange();
    }

    /// <summary>
    /// User is moving the scroll control for saturation
    /// </summary>
    /// <param name="sender">Event sender</param>
    /// <param name="e">Event arguments</param>
    private void wndScrollSaturation_Scroll(object sender, ScrollEventArgs e)
    {
      m_Saturation = e.NewValue / m_SatMax;
      NotifyColorChange();
    }

    /// <summary>
    /// User is moving the scroll control for transparency
    /// </summary>
    /// <param name="sender">Event sender</param>
    /// <param name="e">Event arguments</param>
    private void wndScrollTranspareny_Scroll(object sender, ScrollEventArgs e)
    {
      m_Transparency = e.NewValue / m_TransMax;
      NotifyColorChange();
    }

    /// <summary>
    /// User is clicking (once) the button to move the hue to the left. This is also called
    /// when the user presses the left arrow when this control has keyboard focus.
    /// </summary>
    /// <param name="sender">Event sender</param>
    /// <param name="e">Event arguments</param>
    private void btnLeft_Click(object sender, EventArgs e)
    {
      // move hue left (towards zero)
      if (m_Hue > m_HuePixelStep)
      {
        m_Hue -= m_HuePixelStep;
        NotifyColorChange();
      }
    }

    /// <summary>
    /// User is clicking (once) the button to move the hue to the right. This is also called
    /// when the user presses the right arrow when this control has keyboard focus.
    /// </summary>
    /// <param name="sender">Event sender</param>
    /// <param name="e">Event arguments</param>
    private void btnRight_Click(object sender, EventArgs e)
    {
      // move hue right (towards 360.0)
      if (m_Hue < (360.0f - m_HuePixelStep))
      {
        m_Hue += m_HuePixelStep;
        NotifyColorChange();
      }
    }
    /// <summary>
    /// User is holding down the arrow left button to scroll left. This is different from a 
    /// single click, so we start a timer to perform multiple "clicks" while the timer
    /// is running, which allows the user to move the hue continuously left.
    /// </summary>
    /// <param name="sender">Event sender</param>
    /// <param name="e">Event arguments</param>
    private void btnLeft_MouseDown(object sender, MouseEventArgs e)
    {
      tmrUpdate.Enabled = true;
      // now we aren't really talking about the 'left mouse button', we're talking about
      // the 'left arrow button', but the enumeration still makes sense.
      m_Button = MouseButtons.Left;
      tmrUpdate.Start();
    }

    /// <summary>
    /// User is holding down the arrow left button to scroll right. This is different from a 
    /// single click, so we start a timer to perform multiple "clicks" while the timer
    /// is running, which allows the user to move the hue continuously right.
    /// </summary>
    /// <param name="sender">Event sender</param>
    /// <param name="e">Event arguments</param>
    private void btnRight_MouseDown(object sender, MouseEventArgs e)
    {
      tmrUpdate.Enabled = true;
      // now we are't really talking about the 'right mouse button', we're talking about
      // the 'right arrow button', but the enumeration still makes sense.
      m_Button = MouseButtons.Right;
      tmrUpdate.Start();
    }

    /// <summary>
    /// User has let go of the left mouse button after holding down either the arrow
    /// left or the arrow right button. Stop the timer.
    /// </summary>
    /// <param name="sender">Event sender</param>
    /// <param name="e">Event arguments</param>
    private void OnMouseButtonUp(object sender, System.Windows.Forms.MouseEventArgs e)
    {
      tmrUpdate.Stop();
      tmrUpdate.Enabled = false;
    } // OnMouseButtonUp()

    /// <summary>
    /// Timer has fired while the user is holding down the left mouse button over either the
    /// arrow left or the arrow right buttons.
    /// </summary>
    /// <param name="sender">Event sender</param>
    /// <param name="e">Event arguments</param>
    private void Timer_Tick(object sender, EventArgs e)
    {
      // fire a click event
      if (m_Button == MouseButtons.Left)
      {
        btnLeft_Click(sender, e);
      }
      else if (m_Button == MouseButtons.Right)
      {
        btnRight_Click(sender, e);
      }
    }

    /// <summary>
    /// Updates the color during moving the dragged mouse on the hue panel.
    /// </summary>
    /// <param name="sender">Event sender</param>
    /// <param name="e">Event arguments</param>
    private void panelHue_MouseMove(object sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Left)
      {
        m_Hue = (float)(e.X) * m_HuePixelStep;
        NotifyColorChange();
      }
    }

    /// <summary>
    /// Updates the color during dragging mouse on the hue panel.
    /// </summary>
    /// <param name="sender">Event sender</param>
    /// <param name="e">Event arguments</param>
    private void panelHue_MouseDown(object sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Left)
      {
        m_Hue = (float)(e.X) * m_HuePixelStep;
        NotifyColorChange();
      }
    }

    /// <summary>
    /// Invokes a <see cref="ColorDialog"/> when user
    /// clicked on the preview Panel.
    /// </summary>
    /// <param name="sender">Event sender</param>
    /// <param name="e">Event arguments</param>
    private void panelSelectedColor_MouseUp(object sender, MouseEventArgs e)
    {
      // show the more cumbersome but very precise color selection dialog
      ColorDialog dlgColor = new ColorDialog();
      dlgColor.FullOpen = true;
      dlgColor.Color = HSLToRGB(m_Hue, m_Saturation, m_Luminance, m_Transparency);
      DialogResult result = dlgColor.ShowDialog(this);
      if (result == DialogResult.OK)
      {
        RGBToHSL(dlgColor.Color, ref m_Hue, ref m_Saturation, ref m_Luminance, ref m_Transparency);

        // update the scroll bars
        wndScrollBrightness.Value = (int)(m_LumMax * m_Luminance + 0.5f);
        wndScrollSaturation.Value = (int)(m_SatMax * m_Saturation + 0.5f);
        wndScrollTranspareny.Value = (int)(m_TransMax * m_Transparency + 0.5f);

        Color color = HSLToRGB(m_Hue, m_Saturation, m_Luminance, m_Transparency);
        nudA.Value = color.A;
        nudR.Value = color.R;
        nudG.Value = color.G;
        nudB.Value = color.B;

        // update the dialog
        NotifyColorChange();
      }
    }

    #endregion //WINDOWSEVENTHANDLER

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler for Custom Defined Events                                    //
    ///////////////////////////////////////////////////////////////////////////////
    #region CUSTOMEVENTHANDLER

    /// <summary>
    /// The OnColorChanged method raises the 
    /// <see cref="ColorChanged"/> event by invoking the delegates.
    /// </summary>
    /// <param name="e">The <see cref="ColorChangedEventArgs"/> with the event data.</param>
    private void OnColorChanged(ColorChangedEventArgs e)
    {
      if (ColorChanged != null)
      {
        // Invokes the delegates. 
        ColorChanged(this, e);
      }
    }

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

    /// <summary>
    /// User is pressing a dialog key while the control has keyboard focus. Check for arrow keys and
    /// allow hue select if the arrow keys are used.
    /// </summary>
    /// <param name="keyData">Key data with key pressed enumeration</param>
    /// <returns>always true</returns>
    protected override bool ProcessDialogKey(Keys keyData)
    {
      // override default behavior of left and right arrow keys
      if (keyData == Keys.Left)
      {
        btnLeft_Click(null, new EventArgs());
        return true;
      }
      if (keyData == Keys.Right)
      {
        btnRight_Click(null, new EventArgs());
        return true;
      }

      // do the default
      return base.ProcessDialogKey(keyData);
    }

    #endregion //OVERRIDES

    ///////////////////////////////////////////////////////////////////////////////
    // Methods for doing main class job                                          //
    ///////////////////////////////////////////////////////////////////////////////
    #region METHODS

    /// <summary>
    /// The color has been modified, notify the callback host if any
    /// </summary>
    private void NotifyColorChange()
    {
      this.Refresh();
      Color clrNewColor = HSLToRGB(m_Hue, m_Saturation, m_Luminance, m_Transparency);
      OnColorChanged(new ColorChangedEventArgs(clrNewColor));
    }

    #endregion //METHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER

    /// <summary>
    /// Convert RGB color to Hue, Saturation, Luminance. Adapted from C++ code for CColor
    /// obtained from CColor - RGB and HLS combined in one class By Christian Rodemeyer 
    /// at http://codeproject.com/bitmap/ccolor.asp
    /// </summary>
    /// <param name="inColor">Color in</param>
    /// <param name="Hue">Hue 0-360</param>
    /// <param name="Saturation">Saturation (0.0 - 1.0)</param>
    /// <param name="Luminance">Luminance (0.0 - 1.0)</param>
    /// <param name="Transparency">Transparency (0.0 - 1.0)</param>
    void RGBToHSL(Color inColor, ref float Hue, ref float Saturation, ref float Luminance, ref float Transparency)
    {
      float Red = (float)inColor.R;
      float Green = (float)inColor.G;
      float Blue = (float)inColor.B;
      Transparency = (float)(1 - inColor.A / 255.0f);

      float minval = Red;
      if (Green < minval) minval = Green;
      if (Blue < minval) minval = Blue;
      float maxval = Red;
      if (Green > maxval) maxval = Green;
      if (Blue > maxval) maxval = Blue;

      float mdiff = maxval - minval;
      float msum = maxval + minval;

      Luminance = msum / 510.0f;
      Saturation = 0.0f;
      Hue = 0.0f;

      if (maxval == minval)
      {
        Saturation = 0.0f;
        Hue = 0.0f;
      }
      else
      {
        float rnorm = (maxval - Red) / mdiff;
        float gnorm = (maxval - Green) / mdiff;
        float bnorm = (maxval - Blue) / mdiff;

        if (Luminance <= 0.5f)
        {
          Saturation = (mdiff / msum);
        }
        else
        {
          Saturation = (mdiff / (510.0f - msum));
        }

        if (Red == maxval) Hue = 60.0f * (6.0f + bnorm - gnorm);
        if (Green == maxval) Hue = 60.0f * (2.0f + rnorm - bnorm);
        if (Blue == maxval) Hue = 60.0f * (4.0f + gnorm - rnorm);
        if (Hue > 360.0f) Hue = Hue - 360.0f;
      }
    } // RGBToHSL()

    /// <summary>
    /// Helper method to do some math to convert HSL/HLS to RGB. Adapted from C++ code for CColor
    /// obtained from CColor - RGB and HLS combined in one class By Christian Rodemeyer 
    /// at http://codeproject.com/bitmap/ccolor.asp
    /// </summary>
    /// <param name="rm1">Root mean 1</param>
    /// <param name="rm2">Root mean 2</param>
    /// <param name="rh">Right hand side</param>
    /// <returns>Byte containing part of a RGB value</returns>
    private byte ToRGB1(float rm1, float rm2, float rh)
    {
      if (rh > 360.0f)
      {
        rh -= 360.0f;
      }
      else if (rh < 0.0f)
      {
        rh += 360.0f;
      }

      if (rh < 60.0f)
      {
        rm1 = rm1 + (rm2 - rm1) * rh / 60.0f;
      }
      else if (rh < 180.0f)
      {
        rm1 = rm2;
      }
      else if (rh < 240.0f)
      {
        rm1 = rm1 + (rm2 - rm1) * (240.0f - rh) / 60.0f;
      }

      return (byte)(rm1 * 255);
    } // ToRGB1()


    /// <summary>
    /// Convert HSL to RGB (.NET Color class). Adapted from C++ code for CColor
    /// obtained from CColor - RGB and HLS combined in one class By Christian Rodemeyer 
    /// at http://codeproject.com/bitmap/ccolor.asp
    /// </summary>
    /// <param name="Hue">Hue in degrees 0.0f - 360.0f</param>
    /// <param name="Saturation">Saturation from 0.0f - 1.0f</param>
    /// <param name="Luminance">Luminance/Brightness from 0.0f - 1.0f</param>
    /// <param name="Transparency">Transparency from 0.0f - 1.0f</param>
    /// <returns>Color class initialized to the selected color.</returns>
    private Color HSLToRGB(float Hue, float Saturation, float Luminance, float Transparency)
    {
      int Red = (int)(Luminance * 255.0f);
      int Green = (int)(Luminance * 255.0f);
      int Blue = (int)(Luminance * 255.0f);
      int Trans = (int)(255 - Transparency * 255.0f);
      if (Saturation != 0.0)
      {
        float rm1, rm2;

        if (Luminance <= 0.5f)
        {
          rm2 = Luminance + Luminance * Saturation;
        }
        else
        {
          rm2 = Luminance + Saturation - Luminance * Saturation;
        }
        rm1 = 2.0f * Luminance - rm2;
        Red = ToRGB1(rm1, rm2, Hue + 120.0f);
        Green = ToRGB1(rm1, rm2, Hue);
        Blue = ToRGB1(rm1, rm2, Hue - 120.0f);
      }

      return Color.FromArgb(Trans, Red, Green, Blue);
    }

    private void btnSubmitARGB_Click(object sender, EventArgs e)
    {
      Color color = Color.FromArgb((int)nudA.Value, (int)nudR.Value, (int)nudG.Value, (int)nudB.Value);

      RGBToHSL(color, ref m_Hue, ref m_Saturation, ref m_Luminance, ref m_Transparency);

      // update the scroll bars
      wndScrollBrightness.Value = (int)(m_LumMax * m_Luminance + 0.5f);
      wndScrollSaturation.Value = (int)(m_SatMax * m_Saturation + 0.5f);
      wndScrollTranspareny.Value = (int)(m_TransMax * m_Transparency + 0.5f);

      // update the dialog
      NotifyColorChange();
    }

    private void btnGetARGB_Click(object sender, EventArgs e)
    {
      Color color = HSLToRGB(m_Hue, m_Saturation, m_Luminance, m_Transparency);
      nudA.Value = color.A;
      nudR.Value = color.R;
      nudG.Value = color.G;
      nudB.Value = color.B;
    }

    // HSLToRGB()

    #endregion //HELPER

  }
}
