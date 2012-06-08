using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using OgamaControls.Dialogs;
using VectorGraphics;
using VectorGraphics.Elements;

namespace OgamaControls
{
  using VectorGraphics.Tools.CustomEventArgs;

  /// <summary>
  /// This <see cref="UserControl"/> can be used to define a <see cref="Pen"/>
  /// and <see cref="Brush"/> that are visualized in two preview 
  /// frames. Additionally the user selects, if edge or fill or both should be used.
  /// </summary>
  public partial class PenAndBrushControl : UserControl
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
    /// Saves the <see cref="VGAlignment"/> of the current shape.
    /// </summary>
    private VGAlignment textAlignment;

    /// <summary>
    /// This event is fired, whenever the controls content has 
    /// changed.
    /// </summary>
    public event EventHandler<ShapePropertiesChangedEventArgs> ShapePropertiesChanged;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets or sets the <see cref="VGAlignment"/> for the text.
    /// </summary>
    public VGAlignment NewTextAlignment
    {
      get { return this.textAlignment; }
      set { this.textAlignment = value; }
    }

    /// <summary>
    /// Gets or sets the Name defined in this control.
    /// </summary>
    /// <value>The newly defined name.</value>
    public string NewName
    {
      get
      {
        if (txbName.Text != "name element here")
          return txbName.Text;
        else return "";
      }
      set
      {
        txbName.Text = value;
        FillEmptyNameTextBox();
      }
    }

    /// <summary>
    /// Gets or sets the <see cref="Font"/> defined in this control.
    /// </summary>
    /// <remarks>The font value is saved in the invisible buttons Font property,
    /// because otherwise the name would be to large for the control.</remarks>
    /// <value>The newly defined <see cref="Font"/>.</value>
    public Font NewFont
    {
      get { return btnFont.Font; }
      set
      {
        if (value != null)
          btnFont.Font = (Font)value.Clone();
      }
    }

    /// <summary>
    /// Gets or sets the fonts <see cref="Color"/> defined in this control.
    /// </summary>
    /// <value>The newly defined <see cref="Color"/> for the font.</value>
    public Color NewFontColor
    {
      get { return txbName.ForeColor; }
      set
      {
        if (value != null)
          txbName.ForeColor = value;
      }
    }

    /// <summary>
    /// Gets or sets the <see cref="Pen"/> defined in this control.
    /// </summary>
    /// <value>The newly defined <see cref="Pen"/>.</value>
    public Pen NewPen
    {
      get { return psaLinestyle.Pen; }
      set
      {
        if (value != null)
        {
          psaLinestyle.Pen = (Pen)value.Clone();
        }
      }
    }

    /// <summary>
    /// Gets or sets the <see cref="Brush"/> defined in this control.
    /// </summary>
    /// <value>The newly defined <see cref="Brush"/>.</value>
    public Brush NewBrush
    {
      get { return bsaFillstyle.Brush; }
      set
      {
        if (value != null)
          bsaFillstyle.Brush = (Brush)value.Clone();
      }
    }

    /// <summary>
    /// Gets or sets the <see cref="ShapeDrawAction"/> defined in this control.
    /// </summary>
    /// <value>The <see cref="ShapeDrawAction"/> that should be used.</value>
    public ShapeDrawAction DrawAction
    {
      get { return GetDrawAction(); }
      set { SetDrawAction(value); }
    }

    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Constructor.
    /// </summary>
    public PenAndBrushControl()
    {
      InitializeComponent();
      FillEmptyNameTextBox();
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
    /// <see cref="Control.Click"/> event handler 
    /// for the <see cref="Button"/> <see cref="btnPenStyle"/>
    /// Raises a <see cref="PenStyleDlg"/>.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A empty <see cref="EventArgs"/></param>
    private void btnPenStyle_Click(object sender, EventArgs e)
    {
      PenStyleDlg dlgStyle = new PenStyleDlg();
      dlgStyle.Text = "Set pen style ...";
      dlgStyle.Pen = psaLinestyle.Pen;
      if (dlgStyle.ShowDialog() == DialogResult.OK)
      {
        psaLinestyle.Pen = dlgStyle.Pen;
        OnShapePropertiesChanged(new ShapePropertiesChangedEventArgs(GetDrawAction(),
          this.NewPen, this.NewBrush, this.NewFont, this.NewFontColor, this.NewName, this.textAlignment));
      }
    }

    /// <summary>
    /// <see cref="Control.Click"/> event handler 
    /// for the <see cref="Button"/> <see cref="btnBrushStyle"/>
    /// Raises a <see cref="BrushStyleDlg"/>.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A empty <see cref="EventArgs"/></param>
    private void btnBrushStyle_Click(object sender, EventArgs e)
    {
      BrushStyleDlg dlgStyle = new BrushStyleDlg();
      dlgStyle.Text = "Set fill style ...";
      dlgStyle.Brush = bsaFillstyle.Brush;
      if (dlgStyle.ShowDialog() == DialogResult.OK)
      {
        bsaFillstyle.Brush = dlgStyle.Brush;
        OnShapePropertiesChanged(new ShapePropertiesChangedEventArgs(GetDrawAction(),
          this.NewPen, this.NewBrush, this.NewFont, this.NewFontColor, this.NewName, this.textAlignment));
      }
    }

    /// <summary>
    /// <see cref="Control.Click"/> event handler 
    /// for the <see cref="Button"/> <see cref="btnFont"/>
    /// Raises a <see cref="BrushStyleDlg"/>.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A empty <see cref="EventArgs"/></param>
    private void btnFont_Click(object sender, EventArgs e)
    {
      FontStyleDlg dlgStyle = new FontStyleDlg();
      dlgStyle.Text = "Set font style for the elements name ...";
      dlgStyle.CurrentFont = btnFont.Font;
      dlgStyle.CurrentFontColor = txbName.ForeColor;
      dlgStyle.CurrentFontAlignment = this.textAlignment;
      if (dlgStyle.ShowDialog() == DialogResult.OK)
      {
        btnFont.Font = dlgStyle.CurrentFont;
        txbName.ForeColor = dlgStyle.CurrentFontColor;
        this.textAlignment = dlgStyle.CurrentFontAlignment;
        OnShapePropertiesChanged(new ShapePropertiesChangedEventArgs(GetDrawAction(),
          this.NewPen, this.NewBrush, this.NewFont, this.NewFontColor, this.NewName, this.textAlignment));
      }
    }

    /// <summary>
    /// <see cref="Control.TextChanged"/> event handler for the
    /// <see cref="TextBox"/> <see cref="txbName"/>.
    /// Raises the <see cref="ShapePropertiesChanged"/> event.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void txbName_TextChanged(object sender, EventArgs e)
    {
      OnShapePropertiesChanged(new ShapePropertiesChangedEventArgs(GetDrawAction(),
        this.NewPen, this.NewBrush, this.NewFont, this.NewFontColor, this.NewName, this.textAlignment));
    }

    /// <summary>
    /// <see cref="Control.Enter"/> event handler for the
    /// <see cref="TextBox"/> <see cref="txbName"/>.
    /// Checks for instruction and erases it if applicable.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void txbName_Enter(object sender, EventArgs e)
    {
      if (txbName.Text == "name element here")
      {
        txbName.Text = "";
        txbName.ForeColor = Color.Black;
      }
    }

    /// <summary>
    /// <see cref="CheckBox.CheckedChanged"/> event handler for the
    /// <see cref="CheckBox"/> <see cref="chbDrawEdge"/>.
    /// Raises the <see cref="ShapePropertiesChanged"/> event.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void chbDrawEdge_CheckedChanged(object sender, EventArgs e)
    {
      OnShapePropertiesChanged(new ShapePropertiesChangedEventArgs(GetDrawAction(),
        this.NewPen, this.NewBrush, this.NewFont, this.NewFontColor, this.NewName, this.textAlignment));
    }

    /// <summary>
    /// <see cref="CheckBox.CheckedChanged"/> event handler for the
    /// <see cref="CheckBox"/> <see cref="chbDrawFill"/>.
    /// Raises the <see cref="ShapePropertiesChanged"/> event.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void chbDrawFill_CheckedChanged(object sender, EventArgs e)
    {
      OnShapePropertiesChanged(new ShapePropertiesChangedEventArgs(GetDrawAction(),
        this.NewPen, this.NewBrush, this.NewFont, this.NewFontColor, this.NewName, this.textAlignment));
    }

    /// <summary>
    /// <see cref="CheckBox.CheckedChanged"/> event handler for the
    /// <see cref="CheckBox"/> <see cref="chbDrawName"/>.
    /// Raises the <see cref="ShapePropertiesChanged"/> event.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void chbDrawName_CheckedChanged(object sender, EventArgs e)
    {
      OnShapePropertiesChanged(new ShapePropertiesChangedEventArgs(GetDrawAction(),
        this.NewPen, this.NewBrush, this.NewFont, this.NewFontColor, this.NewName, this.textAlignment));
    }


    #endregion //WINDOWSEVENTHANDLER

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler for Custom Defined Events                                    //
    ///////////////////////////////////////////////////////////////////////////////
    #region CUSTOMEVENTHANDLER

    /// <summary>
    /// The protected OnShapePropertiesChanged method raises the event by invoking 
    /// the delegates
    /// </summary>
    /// <param name="e">A <see cref="ShapePropertiesChangedEventArgs"/> with the event arguments</param>
    protected virtual void OnShapePropertiesChanged(ShapePropertiesChangedEventArgs e)
    {
      if (ShapePropertiesChanged != null)
      {
        // Invokes the delegates. 
        ShapePropertiesChanged(this, e);
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
    /// <summary>
    /// 
    /// Enables or disables the fill options of this control
    /// </summary>
    /// <param name="visible"><strong>True</strong> if fill styles should be available,
    /// otherwise <strong>false</strong>.</param>
    public void SetFillVisibility(Boolean visible)
    {
      chbDrawFill.Enabled = visible;
      bsaFillstyle.Enabled = visible;
      btnBrushStyle.Enabled = visible;
    }

    /// <summary>
    /// Get current <see cref="ShapeDrawAction"/>
    /// according to checkbox selections in shape design panel.
    /// </summary>
    /// <returns>A <see cref="ShapeDrawAction"/> that should be used for
    /// the current element.</returns>
    private ShapeDrawAction GetDrawAction()
    {
      ShapeDrawAction newShapeDrawAction = ShapeDrawAction.None;
      if (chbDrawName.Checked)
      {
        newShapeDrawAction |= ShapeDrawAction.Name;
      }
      if (chbDrawEdge.Checked)
      {
        newShapeDrawAction |= ShapeDrawAction.Edge;
      }
      if (chbDrawFill.Checked)
      {
        newShapeDrawAction |= ShapeDrawAction.Fill;
      }
      return newShapeDrawAction;
    }

    private void SetDrawAction(ShapeDrawAction value)
    {
      switch (value)
      {
        case ShapeDrawAction.None:
          chbDrawName.Checked = false;
          chbDrawEdge.Checked = false;
          chbDrawFill.Checked = false;
          break;
        case ShapeDrawAction.Edge:
          chbDrawName.Checked = false;
          chbDrawEdge.Checked = true;
          chbDrawFill.Checked = false;
          break;
        case ShapeDrawAction.Fill:
          chbDrawName.Checked = false;
          chbDrawEdge.Checked = false;
          chbDrawFill.Checked = true;
          break;
        case ShapeDrawAction.EdgeAndFill:
          chbDrawName.Checked = false;
          chbDrawEdge.Checked = true;
          chbDrawFill.Checked = true;
          break;
        case ShapeDrawAction.Name:
          chbDrawName.Checked = true;
          chbDrawEdge.Checked = false;
          chbDrawFill.Checked = false;
          break;
        case ShapeDrawAction.NameAndEdge:
          chbDrawName.Checked = true;
          chbDrawEdge.Checked = true;
          chbDrawFill.Checked = false;
          break;
        case ShapeDrawAction.NameAndFill:
          chbDrawName.Checked = true;
          chbDrawEdge.Checked = false;
          chbDrawFill.Checked = true;
          break;
        case ShapeDrawAction.NameEdgeFill:
          chbDrawName.Checked = true;
          chbDrawEdge.Checked = true;
          chbDrawFill.Checked = true;
          break;
      }
    }

    #endregion //METHODS



    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER

    /// <summary>
    /// This method writes an grayed instruction text in an empty name textbox.
    /// </summary>
    private void FillEmptyNameTextBox()
    {
      if (txbName.Text == "")
      {
        txbName.Text = "name element here";
        txbName.ForeColor = SystemColors.GrayText;
      }
    }

    #endregion //HELPER

  }

}
