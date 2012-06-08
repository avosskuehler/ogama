using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using VectorGraphics;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Printing;
using System.Runtime.InteropServices;
using VectorGraphics.Controls;

namespace OgamaControls
{
  /// <summary>
  /// This class is an extended <see cref="RichTextBox"/> used in <see cref="RTBTextControl"/>
  /// that could be printed or rendered via <see cref="FormatRange"/> 
  /// to a device.
  /// </summary>
  /// <remarks>Some references
  /// Original MSDN kb:
  /// Sites
  /// http://home.att.net/~robertdunn/Papers/REPnP2.html
  /// http://msdn.microsoft.com/library/default.asp?url=/library/en-us/shellcc/platform/commctls/richedit/richeditcontrols/richeditcontrolreference/richeditmessages/em_formatrange.asp
  /// http://support.microsoft.com/default.aspx?scid=kb;en-us;812425
  /// </remarks>
  [ToolboxBitmap(typeof(RichTextBox))]
  public partial class PrintableRichTextBox : RichEdit50Transparent
  {
    ///////////////////////////////////////////////////////////////////////////////
    // Defining Constants                                                        //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTANTS

    /// <summary>
    /// Constant used to convert an Inch into TWIPS.
    /// </summary>
    /// <remarks>Convert the unit used by the .NET framework (1/100 inch) 
    /// and the unit used by Win32 API calls (twips 1/1440 inch)
    /// </remarks>
    private const double anInch = 14.4;

    /// <summary>
    /// Constant used to convert an Pixel into TWIPS.
    /// </summary>
    /// <remarks>Convert the unit used by the used by the Screen (Pixel) 
    /// and the unit used by Win32 API calls (twips 1/1440 inch)
    /// </remarks>
    private const double anPixel = 14.4;

    #endregion //CONSTANTS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Variables, Enumerations, Events                                  //
    ///////////////////////////////////////////////////////////////////////////////
    #region FIELDS

    /// <summary>
    /// Saves a temporarily <see cref="RichEdit50Transparent"/> that is
    /// used.
    /// </summary>
    private RichEdit50Transparent rtbTemp = new RichEdit50Transparent();

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

    /// <summary>
    /// Constructor.
    /// </summary>
    public PrintableRichTextBox()
    {
      InitializeComponent();
      this.cbbGreek.ComboBox.SelectionChangeCommitted += new EventHandler(cbbGreek_SelectionChangeCommitted);
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
    /// <see cref="ToolStrip.ItemClicked"/> event handler 
    /// for the <see cref="ContextMenu"/> <see cref="cmu"/>
    /// Triggers the commands of the menu to the underlying control.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A <see cref="ToolStripItemClickedEventArgs"/> with the event data.</param>
    private void cmu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
    {
      switch (e.ClickedItem.Name)
      {
        case "mnuCut":
          this.Cut();
          break;
        case "mnuCopy":
          this.Copy();
          break;
        case "mnuPaste":
          this.Paste();
          break;
        case "mnuBold":
          ChangeFontStyle(FontStyle.Bold, !this.SelectionFont.Bold);
          break;
        case "mnuItalic":
          ChangeFontStyle(FontStyle.Italic, !this.SelectionFont.Italic);
          break;
        case "mnuUnderline":
          ChangeFontStyle(FontStyle.Underline, !this.SelectionFont.Underline);
          break;
      }
    }

    /// <summary>
    /// <see cref="ComboBox.SelectionChangeCommitted"/> event handler 
    /// for the <see cref="ToolStripComboBox"/> <see cref="cbbGreek"/>
    /// Copy the special character to the clipboard and the pastes it
    /// into the rich text box.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A empty <see cref="EventArgs"/></param>
    void cbbGreek_SelectionChangeCommitted(object sender, EventArgs e)
    {
      Clipboard.SetText((string)cbbGreek.SelectedItem);
      this.Paste();
    }

    /// <summary>
    /// <see cref="Control.KeyDown"/> event handler 
    /// for the <see cref="PrintableRichTextBox"/>.
    /// Triggers the keyboard commands to the underlying control.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A <see cref="KeyEventArgs"/> with the event data.</param>
    public void PrintableRichTextBox_KeyDown(object sender, KeyEventArgs e)
    {

      if (e.Modifiers == Keys.Control)
      {
        switch (e.KeyCode)
        {
          case Keys.C:
            this.Copy();
            break;
          case Keys.V:
            this.Paste();
            break;
          case Keys.X:
            this.Cut();
            break;
        }
      }
      if (e.Modifiers == (Keys.Control | Keys.Shift))
      {
        switch (e.KeyCode)
        {
          case Keys.D:
            Clipboard.SetText("Ø");
            this.Paste();
            break;
          case Keys.P:
            Clipboard.SetText("±");
            this.Paste();
            break;
        }
      }
      if (e.Modifiers == (Keys.Control | Keys.Shift | Keys.Alt))
      {
        switch (e.KeyCode)
        {
          case Keys.D:
            Clipboard.SetText("Δ");
            this.Paste();
            break;
          case Keys.F:
            Clipboard.SetText("Φ");
            this.Paste();
            break;
          case Keys.O:
            Clipboard.SetText("Ω");
            this.Paste();
            break;
        }
      }
      if (e.Modifiers == (Keys.Alt | Keys.Control))
      {
        switch (e.KeyCode)
        {
          case Keys.A:
            Clipboard.SetText("α");
            this.Paste();
            break;
          case Keys.B:
            Clipboard.SetText("β");
            this.Paste();
            break;
          case Keys.C:
            Clipboard.SetText("χ");
            this.Paste();
            break;
          case Keys.D:
            Clipboard.SetText("δ");
            this.Paste();
            break;
          case Keys.E:
            Clipboard.SetText("ε");
            this.Paste();
            break;
          case Keys.F:
            Clipboard.SetText("ф");
            this.Paste();
            break;
          case Keys.G:
            Clipboard.SetText("γ");
            this.Paste();
            break;
          case Keys.H:
            Clipboard.SetText("η");
            this.Paste();
            break;
          case Keys.I:
            Clipboard.SetText("ι");
            this.Paste();
            break;
          case Keys.J:
            Clipboard.SetText("φ");
            this.Paste();
            break;
          case Keys.K:
            Clipboard.SetText("κ");
            this.Paste();
            break;
          case Keys.L:
            Clipboard.SetText("λ");
            this.Paste();
            break;
          case Keys.M:
            //Clipboard.SetText("μ");
            //this.Paste();
            break;
          case Keys.N:
            Clipboard.SetText("ν");
            this.Paste();
            break;
          case Keys.O:
            Clipboard.SetText("ο");
            this.Paste();
            break;
          case Keys.P:
            Clipboard.SetText("π");
            this.Paste();
            break;
          case Keys.Q:
            Clipboard.SetText("θ");
            this.Paste();
            break;
          case Keys.R:
            Clipboard.SetText("ρ");
            this.Paste();
            break;
          case Keys.S:
            Clipboard.SetText("σ");
            this.Paste();
            break;
          case Keys.T:
            Clipboard.SetText("τ");
            this.Paste();
            break;
          case Keys.U:
            Clipboard.SetText("υ");
            this.Paste();
            break;
          case Keys.W:
            Clipboard.SetText("ω");
            this.Paste();
            break;
          case Keys.X:
            Clipboard.SetText("ξ");
            this.Paste();
            break;
          case Keys.Y:
            Clipboard.SetText("ψ");
            this.Paste();
            break;
          case Keys.Z:
            Clipboard.SetText("ζ");
            this.Paste();
            break;
        }
      }
    }

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
    /// Render the contents of the RichTextBox for printing into a page given in the
    /// <see cref="PrintPageEventArgs"/>.
    /// </summary>
    /// <param name="charFrom">Character position index immediately preceding the first character in the range. </param>
    /// <param name="charTo">Character position immediately following the last character in the range. </param>
    /// <param name="position">A <see cref="Point"/> with the location on the printing pahe</param>
    /// <param name="size">A <see cref="Size"/> with the bounds for the rendering.</param>
    /// <param name="measure">A flag, indicating, if this method is used to render (false) or to measure (true).</param>
    /// <param name="e">The <see cref="PrintPageEventArgs"/> to render to.</param>
    /// <returns> Return the last character printed + 1 (printing start from this point for next page)</returns>
    public int Print(int charFrom, int charTo, Point position, Size size, Boolean measure, PrintPageEventArgs e)
    {
      //Calculate the area to render and print
      VectorGraphics.Tools.Win32.User32.RECT rectToPrint;
      //rectToPrint.Top = (int)(e.MarginBounds.Top * anInch);
      //rectToPrint.Bottom = (int)(e.MarginBounds.Bottom * anInch);
      //rectToPrint.Left = (int)(e.MarginBounds.Left * anInch);
      //rectToPrint.Right = (int)(e.MarginBounds.Right * anInch);
      rectToPrint.Left = (int)(position.X * anInch);
      rectToPrint.Top = (int)(position.Y * anInch);
      rectToPrint.Right = rectToPrint.Left + (int)(size.Width * anInch);
      rectToPrint.Bottom = rectToPrint.Top + (int)(size.Height * anInch);

      //      Rectangle printRect = new Rectangle(rectToPrint.Left, rectToPrint.Top, rectToPrint.Right - rectToPrint.Left, rectToPrint.Bottom - rectToPrint.Top);
      //this.BackColor = Color.Green;
      //Rectangle printRect = new Rectangle(0,0, 500, 200);
      //Calculate the size of the page

      VectorGraphics.Tools.Win32.User32.RECT rectPage;
      rectPage.Top = (int)(e.PageBounds.Top * anInch);
      rectPage.Bottom = (int)(e.PageBounds.Bottom * anInch);
      rectPage.Left = (int)(e.PageBounds.Left * anInch);
      rectPage.Right = (int)(e.PageBounds.Right * anInch);

      //e.Graphics.FillRectangle(new SolidBrush(Color.Green), printRect);

      IntPtr hdc = e.Graphics.GetHdc();

      VectorGraphics.Tools.Win32.User32.FORMATRANGE fmtRange;
      fmtRange.Chrg.CharacterPositionMax = charTo;				//Indicate character from to character to 
      fmtRange.Chrg.CharacterPositionMin = charFrom;
      fmtRange.Hdc = hdc;                    //Use the same DC for measuring and rendering
      fmtRange.HdcTarget = hdc;              //Point at printer hDC
      fmtRange.Area = rectToPrint;             //Indicate the area on page to print
      fmtRange.AreaPage = rectPage;            //Indicate size of page

      IntPtr res = IntPtr.Zero;

      IntPtr wparam = IntPtr.Zero;
      wparam = new IntPtr(measure ? 0 : 1);

      //Get the pointer to the FORMATRANGE structure in memory
      IntPtr lparam = IntPtr.Zero;
      lparam = Marshal.AllocCoTaskMem(Marshal.SizeOf(fmtRange));
      Marshal.StructureToPtr(fmtRange, lparam, false);

      //IntPtr lparam2 = IntPtr.Zero;
      //lparam2 = Marshal.AllocCoTaskMem(Marshal.SizeOf(bkColor));
      //Marshal.Copy(bkColor, 0, lparam2, 4);

      //long bkColor = 0x000000FF;//0x00bbggrr
      //IntPtr lparam2 = new IntPtr(bkColor);
      //IntPtr wparam2 = new IntPtr(0);
      ////Set background color of rtb 
      //IntPtr lResult = SendMessage(Handle, EM_SETBKGNDCOLOR, wparam2, lparam2);

      //Send the rendered data for printing 
      res = VectorGraphics.Tools.Win32.User32.SendMessage(Handle, VectorGraphics.Tools.Win32.User32.EMFORMATRANGE, wparam, lparam);

      //Free the block of memory allocated
      Marshal.FreeCoTaskMem(lparam);

      //Release the device context handle obtained by a previous call
      e.Graphics.ReleaseHdc(hdc);

      //Return last + 1 character printer
      return res.ToInt32();
    }
    // Render the contents of the RichTextBox for printing
    //	Return the last character printed + 1 (printing start from this point for next page)

    /// <summary>
    /// Formats a range of text in a rich edit control for a specific device.
    /// </summary>
    /// <param name="charFrom">Character position index immediately preceding the first character in the range. </param>
    /// <param name="charTo">Character position immediately following the last character in the range. </param>
    /// <param name="position">A <see cref="Point"/> with the location on the printing pahe</param>
    /// <param name="size">A <see cref="Size"/> with the bounds for the rendering.</param>
    /// <param name="measure">A flag, indicating, if this method is used to render (false) or to measure (true).</param>
    /// <param name="g">The <see cref="Graphics"/> to render to.</param>
    /// <returns> Return the last character printed + 1 (printing start from this point for next page)</returns>
    public int FormatRange(int charFrom, int charTo, Point position, Size size, Boolean measure, Graphics g)
    {
      //Calculate the area to render and print
      VectorGraphics.Tools.Win32.User32.RECT rectToPrint;
      //rectToPrint.Top = (int)(e.MarginBounds.Top * anInch);
      //rectToPrint.Bottom = (int)(e.MarginBounds.Bottom * anInch);
      //rectToPrint.Left = (int)(e.MarginBounds.Left * anInch);
      //rectToPrint.Right = (int)(e.MarginBounds.Right * anInch);
      double XconvertToTwips = 1440f / g.DpiX;//14.9999999f;
      double YconvertToTwips = 1440f / g.DpiY;//14.9999999f; 
      rectToPrint.Left = (int)(position.X * XconvertToTwips);
      rectToPrint.Top = (int)(position.Y * YconvertToTwips);
      rectToPrint.Right = rectToPrint.Left + (int)(size.Width * XconvertToTwips);
      rectToPrint.Bottom = rectToPrint.Top + (int)(size.Height * YconvertToTwips);

      //Calculate the size of the page
      VectorGraphics.Tools.Win32.User32.RECT rectPage;
      rectPage.Top = rectToPrint.Top;
      rectPage.Bottom = rectToPrint.Bottom;
      rectPage.Left = rectToPrint.Left;
      rectPage.Right = rectToPrint.Right;

      IntPtr hdc = g.GetHdc();

      VectorGraphics.Tools.Win32.User32.FORMATRANGE fmtRange;
      fmtRange.Chrg.CharacterPositionMax = charTo;				//Indicate character from to character to 
      fmtRange.Chrg.CharacterPositionMin = charFrom;
      fmtRange.Hdc = hdc;                    //Use the same DC for measuring and rendering
      fmtRange.HdcTarget = hdc;              //Point at printer hDC
      fmtRange.Area = rectToPrint;             //Indicate the area on page to print
      fmtRange.AreaPage = rectPage;            //Indicate size of page

      IntPtr res = IntPtr.Zero;

      IntPtr wparam = IntPtr.Zero;
      wparam = new IntPtr(measure ? 0 : 1);

      //Get the pointer to the FORMATRANGE structure in memory
      IntPtr lparam = IntPtr.Zero;
      lparam = Marshal.AllocCoTaskMem(Marshal.SizeOf(fmtRange));
      Marshal.StructureToPtr(fmtRange, lparam, false);

      //Send the rendered data for VectorGraphics.User32. 
      res = VectorGraphics.Tools.Win32.User32.SendMessage(Handle, VectorGraphics.Tools.Win32.User32.EMFORMATRANGE, wparam, lparam);

      //Free the block of memory allocated
      Marshal.FreeCoTaskMem(lparam);

      //Release the device context handle obtained by a previous call
      g.ReleaseHdc(hdc);

      //Return last + 1 character printer
      return res.ToInt32();
    }

    /// <summary>
    /// This method renders the content of this
    /// <see cref="RichTextBox"/> into the given bitmap.
    /// </summary>
    /// <param name="drawBitmap">A <see cref="Bitmap"/> with the size
    /// that the rich text box should be rendered to.</param>
    public void drawToBitmap(Bitmap drawBitmap)
    {
      Graphics bmpGra = Graphics.FromImage(drawBitmap);
      FormatRange(0, this.TextLength, new Point(0, 0),
        new Size(drawBitmap.Width, drawBitmap.Height), false, bmpGra);
      bmpGra.Dispose();
    }

    /// <summary>
    /// Change the richtextbox font for the current selection
    /// </summary>
    /// <param name="fontFamily">A <see cref="string"/> with the font family name.</param>
    /// <remarks>This method should handle cases that occur when multiple fonts/styles are selected
    /// Parameters:-
    /// 
    /// fontFamily - the font to be applied, eg "Courier New"
    /// Reason: The reason this method and the others exist is because
    /// setting these items via the selection font doen't work because
    /// a null selection font is returned for a selection with more 
    /// than one font!</remarks>
    public void ChangeFont(string fontFamily)
    {

      int rtb1start = this.SelectionStart;
      int len = this.SelectionLength;
      int rtbTempStart = 0;

      // If len <= 1 and there is a selection font, amend and return
      if (len <= 1 && this.SelectionFont != null)
      {
        this.SelectionFont =
          new Font(fontFamily, this.SelectionFont.Size, this.SelectionFont.Style);
        return;
      }

      // Step through the selected text one char at a time
      rtbTemp.Rtf = this.SelectedRtf;
      for (int i = 0; i < len; ++i)
      {
        rtbTemp.Select(rtbTempStart + i, 1);
        rtbTemp.SelectionFont = new Font(fontFamily, rtbTemp.SelectionFont.Size, rtbTemp.SelectionFont.Style);
      }

      // Replace & reselect
      rtbTemp.Select(rtbTempStart, len);
      this.SelectedRtf = rtbTemp.SelectedRtf;
      this.Select(rtb1start, len);
      return;
    }

    /// <summary>
    /// Change the richtextbox style for the current selection
    ///  This method should handle cases that occur when multiple fonts/styles are selected
    /// </summary>
    /// <param name="style">The new <see cref="FontStyle"/>, e.g. <see cref="FontStyle.Bold"/></param>
    /// <param name="add">IF <strong>true</strong> then add else remove</param>
    public void ChangeFontStyle(FontStyle style, bool add)
    {
      // throw error if style isn't: bold, italic, strikeout or underline
      if (style != FontStyle.Bold
        && style != FontStyle.Italic
        && style != FontStyle.Strikeout
        && style != FontStyle.Underline)
        throw new System.InvalidProgramException("Invalid style parameter to ChangeFontStyle");

      int rtb1start = this.SelectionStart;
      int len = this.SelectionLength;
      int rtbTempStart = 0;

      //if len <= 1 and there is a selection font then just handle and return
      if (len <= 1 && this.SelectionFont != null)
      {
        //add or remove style 
        if (add)
          this.SelectionFont = new Font(this.SelectionFont, this.SelectionFont.Style | style);
        else
          this.SelectionFont = new Font(this.SelectionFont, this.SelectionFont.Style & ~style);

        return;
      }

      // Step through the selected text one char at a time	
      rtbTemp.Rtf = this.SelectedRtf;
      for (int i = 0; i < len; ++i)
      {
        rtbTemp.Select(rtbTempStart + i, 1);

        //add or remove style 
        if (add)
          rtbTemp.SelectionFont = new Font(rtbTemp.SelectionFont, rtbTemp.SelectionFont.Style | style);
        else
          rtbTemp.SelectionFont = new Font(rtbTemp.SelectionFont, rtbTemp.SelectionFont.Style & ~style);
      }

      // Replace & reselect
      rtbTemp.Select(rtbTempStart, len);
      this.SelectedRtf = rtbTemp.SelectedRtf;
      this.Select(rtb1start, len);
      return;
    }

    /// <summary>
    /// Change the richtextbox font size for the current selection.
    /// This method should handle cases that occur when multiple fonts/styles are selected.
    /// </summary>
    /// <param name="fontSize">A <see cref="Single"/> with the fontsize to be applied, eg 33.5</param>
    public void ChangeFontSize(float fontSize)
    {

      if (fontSize <= 0.0)
        throw new System.InvalidProgramException("Invalid font size parameter to ChangeFontSize");

      int rtb1start = this.SelectionStart;
      int len = this.SelectionLength;
      int rtbTempStart = 0;

      // If len <= 1 and there is a selection font, amend and return
      if (len <= 1 && this.SelectionFont != null)
      {
        this.SelectionFont =
          new Font(this.SelectionFont.FontFamily, fontSize, this.SelectionFont.Style);
        return;
      }

      // Step through the selected text one char at a time
      rtbTemp.Rtf = this.SelectedRtf;
      for (int i = 0; i < len; ++i)
      {
        rtbTemp.Select(rtbTempStart + i, 1);
        rtbTemp.SelectionFont = new Font(rtbTemp.SelectionFont.FontFamily, fontSize, rtbTemp.SelectionFont.Style);
      }

      // Replace & reselect
      rtbTemp.Select(rtbTempStart, len);
      this.SelectedRtf = rtbTemp.SelectedRtf;
      this.Select(rtb1start, len);
      return;
    }

    /// <summary>
    /// Change the richtextbox font color for the current selection,
    /// This method should handle cases that occur when multiple fonts/styles are selected.
    /// </summary>
    /// <param name="newColor">The new <see cref="Color"/> to set.</param>
    public void ChangeFontColor(Color newColor)
    {
      int rtb1start = this.SelectionStart;
      int len = this.SelectionLength;
      int rtbTempStart = 0;

      //if len <= 1 and there is a selection font then just handle and return
      if (len <= 1 && this.SelectionFont != null)
      {
        this.SelectionColor = newColor;
        return;
      }

      // Step through the selected text one char at a time	
      rtbTemp.Rtf = this.SelectedRtf;
      for (int i = 0; i < len; ++i)
      {
        rtbTemp.Select(rtbTempStart + i, 1);

        //change color
        rtbTemp.SelectionColor = newColor;
      }

      // Replace & reselect
      rtbTemp.Select(rtbTempStart, len);
      this.SelectedRtf = rtbTemp.SelectedRtf;
      this.Select(rtb1start, len);
      return;
    }

    /// <summary>
    /// Change the richtextbox background color for the current selection,
    /// This method should handle cases that occur when multiple fonts/styles are selected.
    /// </summary>
    /// <param name="newColor">The new <see cref="Color"/> to set.</param>
    public void ChangeBackgroundColor(Color newColor)
    {
      int rtb1start = 0;
      int len = this.TextLength;
      int rtbTempStart = 0;

      //if len <= 1 and there is a selection font then just handle and return
      if (len <= 1 && this.SelectionFont != null)
      {
        this.SelectionBackColor = newColor;
        return;
      }

      // Step through the selected text one char at a time	
      rtbTemp.Rtf = this.SelectedRtf;
      for (int i = 0; i < len; ++i)
      {
        rtbTemp.Select(rtbTempStart + i, 1);

        //change color
        rtbTemp.SelectionBackColor = newColor;
      }

      // Replace & reselect
      rtbTemp.Select(rtbTempStart, len);
      this.SelectedRtf = rtbTemp.SelectedRtf;
      this.Select(rtb1start, len);
      return;
    }

    #endregion //METHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER
    #endregion //HELPER
  }
}
