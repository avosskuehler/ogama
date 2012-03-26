using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using VectorGraphics;

namespace OgamaControls
{
  /// <summary>
  /// This is a customized <see cref="ListView"/> with protected property 
  /// <see cref="ListView.DoubleBuffered"/> set to true.
  /// </summary>
  [ToolboxBitmap(typeof(ListView))]
  public partial class DoubleBufferListView : ListView
  {
    /// <summary>
    /// Constructor. Sets double buffering.
    /// </summary>
    public DoubleBufferListView()
    {
      InitializeComponent();
      this.DoubleBuffered = true;
    }
  }
}
