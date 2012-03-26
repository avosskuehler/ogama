using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace OgamaControls
{
  /// <summary>
  /// This is a bugfixed version of the <see cref="TreeView"/>
  /// which correct handles checkboxes activation on double clicks.
  /// </summary>
  /// <remarks>Have a look at https://connect.microsoft.com/VisualStudio/feedback/ViewFeedback.aspx?FeedbackID=374516</remarks>
  [ToolboxItem(true)]
  [ToolboxBitmap(typeof(TreeView))]
  public partial class CheckboxTreeView : TreeView
  {
    private bool nodeLastClickedOnCheckBox = false;

    /// <summary>
    /// Initializes a new instance of the CheckboxTreeView class.
    /// </summary>
    public CheckboxTreeView()
    {
      InitializeComponent();
      CustomInitialize();
    }

    /// <summary>
    /// Initializes a new instance of the CheckboxTreeView class.
    /// </summary>
    /// <param name="container"></param>
    public CheckboxTreeView(IContainer container)
    {
      container.Add(this);
      InitializeComponent();
      CustomInitialize();
    }

    private void CustomInitialize()
    {
      this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
      this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
      this.UpdateStyles();
      this.CheckBoxes = true;
      this.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.CustomTreeView_NodeMouseClick);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="m"></param>
    protected override void WndProc(ref Message m)
    {
      // Suppress WM_LBUTTONDBLCLK
      if (m.Msg == 0x203)
      {
        m.Result = IntPtr.Zero;
        //the first click of the double click will select the node, if it's clicked on the body.
        if (SelectedNode != null && !nodeLastClickedOnCheckBox)
        {
          SelectedNode.Toggle();
        }
      }
      else
      {
        base.WndProc(ref m);
      }
    }

    private void CustomTreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
    {
      //why minus 20? good question. But combined with Bounds.Left it gives us the right hand border of the checkbox.
      this.nodeLastClickedOnCheckBox = e.X <= e.Node.Bounds.Left - 20;
    }
  }
}
