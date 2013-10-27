// *************************************************************************************** //
//  Created on 02/19/2005 by Kevin Menningen
//  This code is released to the public domain for any use, private or commercial.
//  You may modify this code and include it in any project. Please leave this comment
//  section in the code.
// *************************************************************************************** //

using System.Drawing;
using System.Windows.Forms;

namespace OgamaControls
{
  /// <summary>
  /// Direction of arrow button for the class <c>ArrowButton</c>
  /// </summary>
  public enum ArrowButtonDirection
  {
    /// <summary>
    /// arrow left direction
    /// </summary>
    Left = 0,
    /// <summary>
    /// arrow right direction
    /// </summary>
    Right = 1,
    /// <summary>
    /// arrow up direction
    /// </summary>
    Up = 2,
    /// <summary>
    /// arrown down direction
    /// </summary>
    Down = 3
  }

  /// <summary>
  /// Simple button that has a graphical arrow on it
  /// </summary>
  [ToolboxBitmap(typeof(resfinder), "OgamaControls.CommonControls.Buttons.ArrowButton.ArrowButton.bmp")]
  public class ArrowButton : System.Windows.Forms.Button
  {
    //////////////////////////////////////////////////////////////////////////////////////
    // Data Members
    //////////////////////////////////////////////////////////////////////////////////////
    #region Data Members

    /// <summary>
    /// The direction of the arrow.
    /// </summary>
    private ArrowButtonDirection m_Direction;

    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.Container components = null;

    #endregion

    //////////////////////////////////////////////////////////////////////////////////////
    // Construction / Destruction
    //////////////////////////////////////////////////////////////////////////////////////
    #region Construction / Destruction

    /// <summary>
    /// Constructor. Initializes designer components.
    /// </summary>
    public ArrowButton()
    {
      // This call is required by the Windows.Forms Form Designer.
      InitializeComponent();
    }

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if disposing</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        if (components != null)
          components.Dispose();
      }
      base.Dispose(disposing);
    }

    #endregion

    //////////////////////////////////////////////////////////////////////////////////////
    // Component Code - Auto Generated
    //////////////////////////////////////////////////////////////////////////////////////
    #region Component Designer generated code
    /// <summary>
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      components = new System.ComponentModel.Container();
    }
    #endregion

    //////////////////////////////////////////////////////////////////////////////////////
    // Windows Event Handlers
    //////////////////////////////////////////////////////////////////////////////////////
    #region Windows Event Handlers
    /// <summary>
    /// Draw the arrow. You might be wondering why I didn't use AdjustableArrowCap from
    /// the .NET API. The width and height specified for that class didn't seem to have
    /// anything to do with pixels, and thus it was hard to use when a pixel region is
    /// given. After playing with the math and getting right arrows that were different
    /// from left arrows of the same size, I gave up and drew them myself.
    /// </summary>
    /// <param name="pe">PaintEventArgs</param>
    protected override void OnPaint(PaintEventArgs pe)
    {
      // draw a standard button
      base.OnPaint(pe);

      // text is not allowed
      if (this.Text.Length > 0)
      {
        return;
      }

      Rectangle rcArrow = this.ClientRectangle;
      if ((rcArrow.Width < 8) || (rcArrow.Height < 8))
      {
        // can't draw the arrow
        return;
      }

      // buffer around the arrow
      rcArrow.Inflate(-4, -4);

      int PixelHeight = rcArrow.Height;
      int PixelWidth = rcArrow.Width;
      int OffsetX = rcArrow.Left;
      int OffsetY = rcArrow.Top;
      if ((m_Direction == ArrowButtonDirection.Left) || (m_Direction == ArrowButtonDirection.Right))
      {
        // cap the width
        int MaxWidth = PixelHeight / 2;
        if ((PixelHeight % 2) == 1)
        {
          // odd numbers round up
          ++MaxWidth;
        }
        if (PixelWidth > MaxWidth)
        {
          // Offset lets us center the arrow
          OffsetX = rcArrow.Left + ((PixelWidth - MaxWidth) / 2);
          PixelWidth = MaxWidth;
        }
        else if (PixelWidth < MaxWidth)
        {
          // constrict in the other direction
          PixelHeight = PixelWidth * 2 - 1;
        }

        OffsetY = rcArrow.Top + (rcArrow.Height / 2);
      }
      else
      {
        // cap the Height for vertical arrows
        int MaxHeight = PixelWidth / 2;
        if ((PixelWidth % 2) == 1)
        {
          // odd numbers round up
          ++MaxHeight;
        }
        if (PixelHeight > MaxHeight)
        {
          // Offset lets us center the arrow
          OffsetY = rcArrow.Top + ((PixelHeight - MaxHeight) / 2);
          PixelHeight = MaxHeight;
        }
        else if (PixelHeight < MaxHeight)
        {
          // constrict in the other direction
          PixelWidth = PixelHeight * 2 - 1;
        }
        OffsetX = rcArrow.Left + (rcArrow.Width / 2);
      }

      //Debug.WriteLine(
      //   "Arrow dir: " + m_Direction.ToString("d") +
      //   "  pixel width:" + PixelWidth.ToString("d") +
      //   "  height:" + PixelHeight.ToString("d"));

      Pen capPen = new Pen(Color.Black);

      if (!ScrollBarRenderer.IsSupported)
      {
        if (m_Direction == ArrowButtonDirection.Left)
        {
          pe.Graphics.DrawLine(capPen, OffsetX, OffsetY, OffsetX + PixelWidth - 1, OffsetY);
          int SliceHeight = 1;
          for (int ii = OffsetX + 1; ii < (OffsetX + PixelWidth); ii++)
          {
            pe.Graphics.DrawLine(capPen,
               ii,
               OffsetY - SliceHeight,
               ii,
               OffsetY + SliceHeight);

            ++SliceHeight;
          } // for ii
        }
        if (m_Direction == ArrowButtonDirection.Right)
        {
          pe.Graphics.DrawLine(capPen, OffsetX + PixelWidth, OffsetY, OffsetX + 1, OffsetY);
          int SliceHeight = 1;
          for (int ii = (OffsetX + PixelWidth - 1); ii >= (OffsetX + 1); ii--)
          {
            pe.Graphics.DrawLine(capPen,
               ii,
               OffsetY - SliceHeight,
               ii,
               OffsetY + SliceHeight);

            ++SliceHeight;
          } // for ii
        }
        else if (m_Direction == ArrowButtonDirection.Up)
        {
          pe.Graphics.DrawLine(capPen, OffsetX, OffsetY, OffsetX, OffsetY + PixelHeight - 1);
          int SliceWidth = 1;
          for (int ii = OffsetY + 1; ii < (OffsetY + PixelHeight); ii++)
          {
            pe.Graphics.DrawLine(capPen,
               OffsetX - SliceWidth,
               ii,
               OffsetX + SliceWidth,
               ii);

            ++SliceWidth;
          } // for ii
        }
        else if (m_Direction == ArrowButtonDirection.Down)
        {
          pe.Graphics.DrawLine(capPen, OffsetX, OffsetY + PixelHeight, OffsetX, OffsetY + 1);
          int SliceWidth = 1;
          for (int ii = (OffsetY + PixelHeight - 1); ii >= (OffsetY + 1); ii--)
          {
            pe.Graphics.DrawLine(capPen,
               OffsetX - SliceWidth,
               ii,
               OffsetX + SliceWidth,
               ii);

            ++SliceWidth;
          } // for ii
        }
      }
      else
      {
        switch (m_Direction)
        {
          case ArrowButtonDirection.Left:
            ScrollBarRenderer.DrawArrowButton(pe.Graphics, this.ClientRectangle,
              System.Windows.Forms.VisualStyles.ScrollBarArrowButtonState.LeftNormal);
            break;
          case ArrowButtonDirection.Right:
            ScrollBarRenderer.DrawArrowButton(pe.Graphics, this.ClientRectangle,
              System.Windows.Forms.VisualStyles.ScrollBarArrowButtonState.RightNormal);
            break;
          case ArrowButtonDirection.Up:
            ScrollBarRenderer.DrawArrowButton(pe.Graphics, this.ClientRectangle,
             System.Windows.Forms.VisualStyles.ScrollBarArrowButtonState.UpNormal);
            break;
          case ArrowButtonDirection.Down:
            ScrollBarRenderer.DrawArrowButton(pe.Graphics, this.ClientRectangle,
              System.Windows.Forms.VisualStyles.ScrollBarArrowButtonState.DownNormal);
            break;
        }
      }

    } // OnPaint()

    #endregion

    //////////////////////////////////////////////////////////////////////////////////////
    // Public Properties
    //////////////////////////////////////////////////////////////////////////////////////
    #region Public Properties

    /// <summary>
    /// Specifies the direction the arrow will point
    /// </summary>
    public ArrowButtonDirection Direction
    {
      get
      {
        return m_Direction;
      }
      set
      {
        m_Direction = value;
        this.Text = "";
      }
    }

    #endregion

  } // class ArrowButton
} // namespace OgamaControls
