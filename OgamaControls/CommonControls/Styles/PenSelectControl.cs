using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

using VectorGraphics.Elements;

namespace OgamaControls
{
  using VectorGraphics.Tools.CustomEventArgs;

  /// <summary>
  /// PenSelectControl - A control that lets the user rapidly select a pen
  /// </summary>
  [ToolboxBitmap(typeof(resfinder), "OgamaControls.CommonControls.Styles.PenSelectControl.bmp")]
  public partial class PenSelectControl : UserControl
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
    /// The pen that is created within this control
    /// </summary>
    private Pen m_Pen;

    /// <summary>
    /// Flag that is set when the <see cref="PenChanged"/> event should be raised.
    /// </summary>
    private Boolean _raiseEvent;

    /// <summary>
    /// The event that is raised whenever this controls pen changes.
    /// </summary>
    public event EventHandler<PenChangedEventArgs> PenChanged;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets or sets the controls pen.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public Pen Pen
    {
      get { return m_Pen; }
      set
      {
        m_Pen = (Pen)value.Clone();
        _raiseEvent = false;
        cbbPenStyle.SelectedItem = m_Pen.DashStyle.ToString();
        nudPenSize.Value = (decimal)m_Pen.Width;
        penColorSelectControl.SelectedColor = m_Pen.Color;

        if (m_Pen.StartCap != LineCap.Flat)
          chbLeftCap.Checked = true;

        if (m_Pen.StartCap == LineCap.Custom)
        {
          cbbLeftCapStyle.SelectedItem = "AdjustableArrow";
          nudLeftCapWidth.Value = (decimal)((AdjustableArrowCap)m_Pen.CustomStartCap).Width;
          nudLeftCapHeight.Value = (decimal)((AdjustableArrowCap)m_Pen.CustomStartCap).Height;
        }
        else
        {
          cbbLeftCapStyle.SelectedItem = m_Pen.StartCap.ToString();
        }

        if (m_Pen.EndCap != LineCap.Flat)
          chbRightCap.Checked = true;

        if (m_Pen.EndCap == LineCap.Custom)
        {
          cbbRightCapStyle.SelectedItem = "AdjustableArrow";
          nudRightCapWidth.Value = (decimal)((AdjustableArrowCap)m_Pen.CustomStartCap).Width;
          nudRightCapHeight.Value = (decimal)((AdjustableArrowCap)m_Pen.CustomStartCap).Height;
        }
        else
        {
          cbbRightCapStyle.SelectedItem = m_Pen.EndCap.ToString();
        }

        ShowOrHideLeftCapSize();
        ShowOrHideRightCapSize();

        this.penStyleArea.Pen = m_Pen;
        OnPenChanged(new PenChangedEventArgs(m_Pen, VGStyleGroup.None));
        _raiseEvent = true;
      }
    }

    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Constructor. Initializes the combo box with enumeration values.
    /// </summary>
    public PenSelectControl()
    {
      InitializeComponent();
      cbbPenStyle.Items.AddRange(Enum.GetNames(typeof(DashStyle)));
      cbbLeftCapStyle.Items.Add("AdjustableArrow");
      cbbLeftCapStyle.Items.AddRange(Enum.GetNames(typeof(LineCap)));
      cbbLeftCapStyle.SelectedItem = "AdjustableArrow";
      cbbRightCapStyle.Items.Add("AdjustableArrow");
      cbbRightCapStyle.Items.AddRange(Enum.GetNames(typeof(LineCap)));
      cbbRightCapStyle.SelectedItem = "AdjustableArrow";
      _raiseEvent = true;
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

    private void penColorSelectControl_ColorChanged(object sender, ColorChangedEventArgs e)
    {
      RecreatePen();
    }

    private void cbbPenStyle_SelectionChangeCommitted(object sender, EventArgs e)
    {
      RecreatePen();
    }

    private void nudPenSize_ValueChanged(object sender, EventArgs e)
    {
      RecreatePen();
    }

    private void chbLeftCap_CheckedChanged(object sender, EventArgs e)
    {
      RecreatePen();
    }

    private void chbRightCap_CheckedChanged(object sender, EventArgs e)
    {
      RecreatePen();
    }

    private void cbbLeftCapStyle_SelectionChangeCommitted(object sender, EventArgs e)
    {
      ShowOrHideLeftCapSize();
      RecreatePen();
    }


    private void cbbRightCapStyle_SelectionChangeCommitted(object sender, EventArgs e)
    {
      ShowOrHideRightCapSize();
      RecreatePen();
    }


    private void nudLeftCapWidth_ValueChanged(object sender, EventArgs e)
    {
      RecreatePen();
    }

    private void nudLeftCapHeight_ValueChanged(object sender, EventArgs e)
    {
      RecreatePen();
    }

    private void nudRightCapWidth_ValueChanged(object sender, EventArgs e)
    {
      RecreatePen();
    }

    private void nudRightCapHeight_ValueChanged(object sender, EventArgs e)
    {
      RecreatePen();
    }

    #endregion //WINDOWSEVENTHANDLER

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler for Custom Defined Events                                    //
    ///////////////////////////////////////////////////////////////////////////////
    #region CUSTOMEVENTHANDLER

    private void OnPenChanged(PenChangedEventArgs e)
    {
      if (PenChanged != null)
      {
        // Invokes the delegates. 
        PenChanged(this, e);
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
    #endregion //OVERRIDES

    ///////////////////////////////////////////////////////////////////////////////
    // Methods for doing main class job                                          //
    ///////////////////////////////////////////////////////////////////////////////
    #region METHODS

    private void RecreatePen()
    {
      if (_raiseEvent)
      {
        m_Pen = new Pen(penColorSelectControl.SelectedColor, (float)nudPenSize.Value);
        m_Pen.DashStyle = (DashStyle)Enum.Parse(typeof(DashStyle), cbbPenStyle.Text);
        if (chbLeftCap.Checked)
        {
          if (cbbLeftCapStyle.Text == "AdjustableArrow")
          {
            Single width = (Single)nudLeftCapWidth.Value;
            Single height = (Single)nudLeftCapHeight.Value;
            AdjustableArrowCap capLeft = new AdjustableArrowCap(width, height);
            m_Pen.CustomStartCap = capLeft;
          }
          else
          {
            LineCap capLeft = (LineCap)Enum.Parse(typeof(LineCap), cbbLeftCapStyle.Text);
            m_Pen.StartCap = capLeft;
          }
        }
        if (chbRightCap.Checked)
        {
          if (cbbRightCapStyle.Text == "AdjustableArrow")
          {
            Single width = (Single)nudRightCapWidth.Value;
            Single height = (Single)nudRightCapHeight.Value;
            AdjustableArrowCap capRight = new AdjustableArrowCap(width, height);
            m_Pen.CustomEndCap = capRight;
          }
          else
          {
            LineCap capRight = (LineCap)Enum.Parse(typeof(LineCap), cbbRightCapStyle.Text);
            m_Pen.EndCap = capRight;
          }
        }
        penStyleArea.Pen = m_Pen;
        OnPenChanged(new PenChangedEventArgs(m_Pen, VGStyleGroup.None));
      }
    }

    #endregion //METHODS


    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER

    private void ShowOrHideRightCapSize()
    {
      Boolean showWidthHeight = false;
      if (cbbRightCapStyle.SelectedItem.ToString() == "AdjustableArrow")
      {
        showWidthHeight = true;
      }
      nudRightCapHeight.Visible = showWidthHeight;
      nudRightCapWidth.Visible = showWidthHeight;
      lblRightCapHeight.Visible = showWidthHeight;
      lblRightCapWidth.Visible = showWidthHeight;
    }

    private void ShowOrHideLeftCapSize()
    {
      Boolean showWidthHeight = false;
      if (cbbLeftCapStyle.SelectedItem.ToString() == "AdjustableArrow")
      {
        showWidthHeight = true;
      }
      nudLeftCapHeight.Visible = showWidthHeight;
      nudLeftCapWidth.Visible = showWidthHeight;
      lblLeftCapHeight.Visible = showWidthHeight;
      lblLeftCapWidth.Visible = showWidthHeight;
    }

    #endregion //HELPER

  }
}
