using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace OgamaControls
{
  /// <summary>
  /// A <see cref="UserControl"/> which has a real transparent background.
  /// It works for all windows forms components, but not for ActiveX containers in the background.
  /// </summary>
  [ToolboxBitmap(typeof(resfinder), "OgamaControls.CommonControls.TransparentControls.TransparentUserControl.bmp")]
  public partial class TransparentUserControl : UserControl
	{
    /// <summary>
    /// Constructor.
    /// </summary>
		public TransparentUserControl()
		{
      //SetStyle(ControlStyles.SupportsTransparentBackColor | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
      //this.UpdateStyles();
      //this.BackColor = Color.Transparent;
      this.DoubleBuffered = true;
			InitializeComponent();
		}

    /// <summary>
    /// Overridden. Adds the WS_TRANSPARENT style.
    /// </summary>
		protected override CreateParams CreateParams
		{
			get
			{
				CreateParams cp = base.CreateParams;
				cp.ExStyle |= 0x00000020; //WS_TRANSPARENT
				return cp;
			}
		}
	}
}
