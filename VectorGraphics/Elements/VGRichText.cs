// <copyright file="VGRichText.cs" company="FU Berlin">
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

namespace VectorGraphics.Elements
{
  using System;
  using System.ComponentModel;
  using System.Drawing;
  using System.Runtime.InteropServices;
  using System.Text;
  using System.Windows.Forms;

  using VectorGraphics.Controls;
  using VectorGraphics.Tools.CustomTypeConverter;
  using VectorGraphics.Tools.Win32;

  /// <summary>
  /// Inherited from <see cref="VGElement"/>. 
  /// A xml serializable class that represents a rich text formatted (RTF) string.
  /// </summary>
  [Serializable]
  public class VGRichText : VGElement
  {
    ///////////////////////////////////////////////////////////////////////////////
    // Defining Constants                                                        //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTANTS

    /// <summary>
    /// To this amount of pixels, the rtf text will have a padding
    /// to the bounds of this element.
    /// </summary>
    private const int TEXTPADDING = 5;

    #endregion //CONSTANTS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Variables, Enumerations, Events                                  //
    ///////////////////////////////////////////////////////////////////////////////
    #region FIELDS

    /// <summary>
    /// This is the underlying <see cref="RichTextBox"/> to
    /// handle the drawing and RTF formatting.
    /// </summary>
    private RichEdit50 richTextBox;

    /// <summary>
    /// Saves the <see cref="RichTextBox.TextLength"/> property.
    /// </summary>
    private int textLength;

    /// <summary>
    /// Saves the <see cref="Control.Handle"/> property.
    /// </summary>
    private IntPtr handleToRichTextBox;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the VGRichText class.
    /// </summary>
    /// <param name="newShapeDrawAction"><see cref="ShapeDrawAction"/> for the bounds.</param>
    /// <param name="newRtf">string to display</param>
    /// <param name="newTransparency">A flag indicating a transparent background if true.</param>
    /// <param name="newPen">Pen to use</param>
    /// <param name="newBrush">Brush for drawing</param>
    /// <param name="newFont">Font for drawing name</param>
    /// <param name="newFontColor">Font color for drawing name.</param>
    /// <param name="position">TopLeft text position</param>
    /// <param name="size">Size of the clipping rectangle.</param>
    /// <param name="newStyleGroup">Group Enumeration, <see cref="VGStyleGroup"/></param>
    /// <param name="newName">Name of Element</param>
    /// <param name="newElementGroup">Element group description</param>
    public VGRichText(
      ShapeDrawAction newShapeDrawAction,
      string newRtf,
      bool newTransparency,
      Pen newPen,
      Brush newBrush,
      Font newFont,
      Color newFontColor,
      PointF position,
      SizeF size,
      VGStyleGroup newStyleGroup,
      string newName,
      string newElementGroup)
      : base(
      newShapeDrawAction,
      newPen,
      newBrush,
      newFont,
      newFontColor,
      new RectangleF(position, size),
      newStyleGroup,
      newName,
      newElementGroup,
      null)
    {
      this.RebuildUnderlyingRichTextBox();
      this.richTextBox.Rtf = newRtf;
      this.textLength = this.richTextBox.TextLength;
    }

    /// <summary>
    /// Prevents a default instance of the VGRichText class from being created.
    /// Parameterless constructor. Used for serialization.
    /// </summary>
    private VGRichText()
    {
      this.richTextBox = new RichEdit50Opaque();
      this.textLength = this.richTextBox.TextLength;
      this.handleToRichTextBox = this.richTextBox.Handle;
    }

    /// <summary>
    /// Initializes a new instance of the VGRichText class.
    /// Clone Constructor. Creates new text that is
    /// identical to the given <see cref="VGText"/>.
    /// </summary>
    /// <param name="oldText">Text element to clone.</param>
    private VGRichText(VGRichText oldText)
      : base(
      oldText.ShapeDrawAction,
      oldText.Pen,
      oldText.Brush,
      oldText.Font,
      oldText.FontColor,
      oldText.Bounds,
      oldText.StyleGroup,
      oldText.Name,
      oldText.ElementGroup,
      oldText.Sound)
    {
      this.RebuildUnderlyingRichTextBox();
      this.richTextBox.Rtf = oldText.RtfToDraw;
      this.textLength = this.richTextBox.TextLength;
    }

    #endregion //CONSTRUCTION

    /// <summary>
    /// This delegate enables asynchronous calls for getting
    /// a string property on a RichTextBox control.
    /// </summary>
    /// <returns>An <see cref="String"/> for the Text or RTF property
    /// of the <see cref="RichTextBox"/>.</returns>
    private delegate string GetStringInvoker();

    /// <summary>
    /// This delegate enables asynchronous calls for setting
    /// a string property on a RichTextBox control.
    /// </summary>
    /// <param name="rtfToDraw">A <see cref="String"/> for the Text or RTF property
    /// of the <see cref="RichTextBox"/>.</param>
    private delegate void SetStringInvoker(string rtfToDraw);

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets or sets the string of this element.
    /// </summary>
    /// <value>A <see cref="string"/> with the instruction.</value>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Category("Appearance")]
    [Description("The string to draw as instruction.")]
    public string StringToDraw
    {
      get { return this.ThreadSafeGetText(); }
      set { this.ThreadSafeSetText(value); }
    }

    /// <summary>
    /// Gets or sets the rtf formatted string of this element.
    /// </summary>
    /// <value>A <see cref="string"/> with the instruction in rtf format.</value>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public string RtfToDraw
    {
      get { return this.ThreadSafeGetRTF(); }
      set { this.ThreadSafeSetRTF(value); }
    }

    /// <summary>
    /// Gets or sets shape drawing mode enumeration, edge, fill or both.
    /// When this property is set this overload reinitializes the underlying
    /// RichTextBox.
    /// </summary>
    /// <value>The <see cref="ShapeDrawAction"/> of this shape.</value>
    public override ShapeDrawAction ShapeDrawAction
    {
      get
      {
        return base.ShapeDrawAction;
      }

      set
      {
        base.ShapeDrawAction = value;
        this.RebuildUnderlyingRichTextBox();
      }
    }

    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Public methods                                                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region PUBLICMETHODS
    #endregion //PUBLICMETHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Inherited methods                                                         //
    ///////////////////////////////////////////////////////////////////////////////
    #region OVERRIDES

    /// <summary>
    /// Overridden <see cref="VGElement.Draw(Graphics)"/>.  
    /// Draws text with the owning brush and font onto given 
    /// graphics context.
    /// </summary>
    /// <param name="graphics">Graphics context to draw on.</param>
    /// <exception cref="ArgumentNullException">Thrown, when graphics object is null.</exception>
    public override void Draw(Graphics graphics)
    {
      if (graphics == null)
      {
        throw new ArgumentNullException("Graphics object should not be null.");
      }

      try
      {
        Point topLeftTextCorner = Point.Round(this.Location);

        if (this.Bounds.Width == 0 || this.Bounds.Height == 0)
        {
          Size textSize = new Size(20, 20);
          topLeftTextCorner.Offset(TEXTPADDING, TEXTPADDING);

          while (this.FormatRange(
            0,
            this.textLength,
            topLeftTextCorner,
            textSize,
            true,
            graphics) < this.textLength)
          {
            if (textSize.Width <= 400)
            {
              textSize.Width += 50;
            }
            else
            {
              textSize.Height += 20;
            }
          }

          this.Bounds = new RectangleF(this.Location, textSize);
        }

        this.DrawFillAndEdge(graphics);

        Bitmap renderBitmap = new Bitmap(
          (int)Math.Max(this.Bounds.Width - 2 * TEXTPADDING, 1),
          (int)Math.Max(this.Bounds.Height - 2 * TEXTPADDING, 1),
          graphics);

        Graphics bmpGra = Graphics.FromImage(renderBitmap);
        this.FillBackground(this.Brush);
        this.FormatRange(
          0,
          this.textLength,
          new Point(0, 0),
          new Size(renderBitmap.Width, renderBitmap.Height),
          false,
          bmpGra);
        bmpGra.Dispose();

        topLeftTextCorner = Point.Round(this.Location);
        topLeftTextCorner.Offset(TEXTPADDING, TEXTPADDING);
        graphics.DrawImageUnscaled(renderBitmap, topLeftTextCorner);
      }
      catch (Exception)
      {
      }

      // Draw name and selection frame if applicable
      base.Draw(graphics);
    }

    /// <summary>
    /// Overridden <see cref="VGElement.Reset()"/>. 
    /// Resets the current text element to
    /// default values (empty instruction).
    /// </summary>
    public override void Reset()
    {
      base.Reset();
      this.richTextBox.Clear();
      this.textLength = this.richTextBox.TextLength;
    }

    /// <summary>
    /// Overridden <see cref="Object.ToString()"/> method.
    /// Returns the <see cref="VGText"/> properties as a human readable string.
    /// </summary>
    /// <returns>A <see cref="string"/> that represents this <see cref="VGText"/>.</returns>
    public override string ToString()
    {
      StringBuilder sb = new StringBuilder();
      sb.Append("VGRichText, Name: ");
      sb.Append(Name);
      sb.Append(" ; '");
      sb.Append(this.richTextBox.Text);
      sb.Append(" ; Pen: ");
      sb.Append(ObjectStringConverter.PenToString(Pen));
      sb.Append(" ; Group: ");
      sb.Append(StyleGroup.ToString());
      sb.Append(" ; Bounds: ");
      sb.Append(Bounds.ToString());
      return sb.ToString();
    }

    /// <summary>
    /// Overridden <see cref="VGElement.ToShortString()"/> method.
    /// Returns the main <see cref="VGText"/> properties as a human readable string.
    /// </summary>
    /// <returns>A <see cref="string"/> that represents the 
    /// current <see cref="VGText"/> in short form with its main properties.</returns>
    public override string ToShortString()
    {
      StringBuilder sb = new StringBuilder();
      sb.Append("RichText ");
      string text = this.richTextBox.Text;
      sb.Append(text.Substring(0, text.Length > 12 ? Math.Max(12, text.Length - 1) : text.Length));
      sb.Append(" ...");
      return sb.ToString();
    }

    /// <summary>
    /// Overridden <see cref="VGElement.CloneCore()"/>.  
    /// Creates an excact copy of given <see cref="VGRichText"/>.
    /// </summary>
    /// <returns>Excact copy of this text element.</returns>
    protected override VGElement CloneCore()
    {
      return new VGRichText(this);
    }

    /// <summary>
    /// Overridden <see cref="VGElement.AddGrabHandles()"/>. 
    /// Adds a middle right, bottom middle and bottom right 
    /// grab handle to the current text.
    /// </summary>
    protected override void AddGrabHandles()
    {
      this.AddGrabHandles(true, false, false, false, false, true, false, true, true);
    }

    #endregion //OVERRIDES

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
    // Methods for doing main class job                                          //
    ///////////////////////////////////////////////////////////////////////////////
    #region METHODS

    /// <summary>
    /// Thread safe version to set the <see cref="RichTextBox.Rtf"/> property
    /// </summary>
    /// <param name="rtfToDraw">The <see cref="String"/> with the rtf string to
    /// be set to the RichTextBox.</param>
    private void ThreadSafeSetRTF(string rtfToDraw)
    {
      if (this.richTextBox.InvokeRequired)
      {
        this.richTextBox.Invoke(new SetStringInvoker(this.ThreadSafeSetRTF), rtfToDraw);
        return;
      }

      this.richTextBox.Rtf = rtfToDraw;
      this.textLength = this.richTextBox.TextLength;
    }

    /// <summary>
    /// Thread safe version to get the <see cref="RichTextBox.Rtf"/> property
    /// </summary>
    /// <returns>The <see cref="String"/> with the rtf string to
    /// be set to the RichTextBox.</returns>
    private string ThreadSafeGetRTF()
    {
      if (this.richTextBox.InvokeRequired)
      {
        return (string)this.richTextBox.Invoke(new GetStringInvoker(this.ThreadSafeGetRTF));
      }

      return this.richTextBox.Rtf;
    }

    /// <summary>
    /// Thread safe version to set the <see cref="RichTextBox.Text"/> property
    /// </summary>
    /// <param name="txtToDraw">The <see cref="String"/> with the string to
    /// be set to the RichTextBox.</param>
    private void ThreadSafeSetText(string txtToDraw)
    {
      if (this.richTextBox.InvokeRequired)
      {
        this.richTextBox.Invoke(new SetStringInvoker(this.ThreadSafeSetText), txtToDraw);
        return;
      }

      this.richTextBox.Text = txtToDraw;
      this.textLength = this.richTextBox.TextLength;
    }

    /// <summary>
    /// Thread safe version to get the <see cref="RichTextBox.Text"/> property
    /// </summary>
    /// <returns>The <see cref="String"/> with the string to
    /// be set to the RichTextBox.</returns>
    private string ThreadSafeGetText()
    {
      if (this.richTextBox.InvokeRequired)
      {
        return (string)this.richTextBox.Invoke(new GetStringInvoker(this.ThreadSafeGetText));
      }

      return this.richTextBox.Text;
    }

    /// <summary>
    /// Fills the background of the RichTextBox with the given brush.
    /// </summary>
    /// <param name="brush">A <see cref="Brush"/> to use for the fill.</param>
    private void FillBackground(Brush brush)
    {
      if (brush is SolidBrush)
      {
        this.richTextBox.BackgroundImage = null;
        SolidBrush sb = (SolidBrush)brush;
        this.richTextBox.BackColor = sb.Color;
      }
      else
      {
        this.richTextBox.BackColor = Color.Transparent;
        Bitmap bmp = new Bitmap(this.richTextBox.Width, this.richTextBox.Height);
        Graphics graBmp = Graphics.FromImage(bmp);
        graBmp.FillRectangle(brush, 0, 0, bmp.Width, bmp.Height);
        this.richTextBox.BackgroundImage = bmp;
        graBmp.Dispose();
      }
    }

    /// <summary>
    /// This method converts dots per inch into HIMETRIC values
    /// </summary>
    /// <param name="graphics">The <see cref="Graphics"/> with the dots per inch values.</param>
    /// <param name="size">Ref. A <see cref="SizeF"/> with the new HIMETRIC values.</param>
    private void DPToHIMETRIC(Graphics graphics, ref SizeF size)
    {
      size.Width = (size.Width * 2540.0f) / graphics.DpiX;
      size.Height = (size.Height * 2540.0f) / graphics.DpiY;
    }

    /// <summary>
    /// Formats a range of text in a rich edit control for a specific device.
    /// This implementation draws the contents of the <see cref="RichTextBox"/>
    /// to the given <see cref="Graphics"/>.
    /// </summary>
    /// <param name="charFrom">Character position index immediately preceding the first character in the range.</param>
    /// <param name="charTo">Character position immediately following the last character in the range.</param>
    /// <param name="position">A <see cref="Point"/> with the location of the top left 
    /// corner of the rectangle to draw the text portion to.</param>
    /// <param name="size">A <see cref="Size"/> with the width and height of the 
    /// rectangle to draw the text portion to.</param>
    /// <param name="measure"><strong>False</strong> if the text should be rendered,
    /// if <strong>true</strong>, the text is just measured.</param>
    /// <param name="g">The <see cref="Graphics"/> to draw to</param>
    /// <returns>The index of the last character that fits in the region plus one.</returns>
    private int FormatRange(int charFrom, int charTo, Point position, Size size, bool measure, Graphics g)
    {
      // Calculate the area to render and print
      User32.RECT rectToPrint;
      double convertToTwipsX = 1440f / g.DpiX;
      double convertToTwipsY = 1440f / g.DpiY;

      rectToPrint.Left = (int)(position.X * convertToTwipsX * g.Transform.Elements[0]);
      rectToPrint.Top = (int)(position.Y * convertToTwipsY * g.Transform.Elements[3]);
      rectToPrint.Right = rectToPrint.Left + (int)(size.Width * convertToTwipsX * g.Transform.Elements[0]);
      rectToPrint.Bottom = rectToPrint.Top + (int)(size.Height * convertToTwipsY * g.Transform.Elements[3]);

      IntPtr hdc = g.GetHdc();

      // Initialize Formatrange structure
      User32.FORMATRANGE fmtRange;
      fmtRange.Chrg.CharacterPositionMax = charTo;
      fmtRange.Chrg.CharacterPositionMin = charFrom;

      // Use the same DC for measuring and rendering
      fmtRange.Hdc = hdc;
      fmtRange.HdcTarget = hdc;
      fmtRange.Area = rectToPrint;
      fmtRange.AreaPage = rectToPrint;

      // Set measure flag
      IntPtr wparam = new IntPtr(measure ? 0 : 1);

      // Get the pointer to the FORMATRANGE structure in memory
      IntPtr lparam = IntPtr.Zero;
      lparam = Marshal.AllocCoTaskMem(Marshal.SizeOf(fmtRange));
      Marshal.StructureToPtr(fmtRange, lparam, false);

      // Tell the RTF to base it's display off of the printer at the desired line width
      IntPtr result = User32.SendMessage(this.handleToRichTextBox, User32.EMSETTARGETDEVICE, hdc, new IntPtr(300));

      // Send the rendered data 
      result = User32.SendMessage(this.handleToRichTextBox, User32.EMFORMATRANGE, wparam, lparam);

      // Free the block of memory allocated
      Marshal.FreeCoTaskMem(lparam);

      // Release the device context handle obtained by a previous call
      g.ReleaseHdc(hdc);

      // Allow the RTF to free up memory
      result = User32.SendMessage(this.handleToRichTextBox, User32.EMFORMATRANGE, IntPtr.Zero, IntPtr.Zero);

      // Return last + 1 character printer
      return result.ToInt32();
    }

    /// <summary>
    /// This method switches the mode of the underlying RichTextBox
    /// that is used for rendering according to the
    /// <see cref="ShapeDrawAction"/> of this element.
    /// When no fill ist set, the RichTextBox is transparent and
    /// only in this case use the CreateParams.Transparent styled
    /// RichTextBox, because this one renders crispy for
    /// unknown reason.
    /// </summary>
    private void RebuildUnderlyingRichTextBox()
    {
      string rtfBackup = this.richTextBox != null ? this.richTextBox.Rtf : string.Empty;

      if (this.richTextBox != null)
      {
        this.richTextBox.Dispose();
      }

      if ((this.ShapeDrawAction & ShapeDrawAction.Fill) == ShapeDrawAction.Fill)
      {
        this.richTextBox = new RichEdit50Opaque();
      }
      else
      {
        this.richTextBox = new RichEdit50Transparent();
      }

      this.richTextBox.Rtf = rtfBackup;
      this.textLength = this.richTextBox.TextLength;
      this.handleToRichTextBox = this.richTextBox.Handle;
    }

    #endregion //METHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER

    ////// Method to create a rotated font using a LOGFONT structure.
    ////Font CreateRotatedFont(string fontname, int angleInDegrees)
    ////{
    ////  Font.loF
    ////  LogFont logf = new Microsoft.WindowsCE.Forms.LogFont();

    ////  // Create graphics object for the form, and obtain
    ////  // the current DPI value at design time. In this case,
    ////  // only the vertical resolution is petinent, so the DpiY
    ////  // property is used. 

    ////  Graphics g = this.CreateGraphics();
    ////  // Scale an 18-point font for current screen vertical DPI.
    ////  logf.Height = (int)(-18f * g.DpiY / curDPI);

    ////  // Convert specified rotation angle to tenths of degrees.  
    ////  logf.Escapement = angleInDegrees * 10;

    ////  // Orientation is the same as Escapement in mobile platforms.
    ////  logf.Orientation = logf.Escapement;

    ////  logf.FaceName = fontname;

    ////  // Set LogFont enumerations.
    ////  logf.CharSet = LogFontCharSet.Default;
    ////  logf.OutPrecision = LogFontPrecision.Default;
    ////  logf.ClipPrecision = LogFontClipPrecision.Default;
    ////  logf.Quality = LogFontQuality.ClearType;
    ////  logf.PitchAndFamily = LogFontPitchAndFamily.Default;

    ////  // Explicitly dispose any drawing objects created.
    ////  g.Dispose();

    ////  return Font.FromLogFont(logf);
    ////}
    #endregion //HELPER
  }
}