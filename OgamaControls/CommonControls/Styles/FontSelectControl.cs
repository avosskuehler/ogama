using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Text;

using VectorGraphics.Elements;

namespace OgamaControls
{
  using VectorGraphics.Tools.CustomEventArgs;

  /// <summary>
  /// FontSelectControl - A control that lets the user rapidly select a font
  /// </summary>
  [ToolboxBitmap(typeof(resfinder), "OgamaControls.CommonControls.Styles.FontSelectControl.bmp")]
  public partial class FontSelectControl : UserControl
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
    /// Stores the created font.
    /// </summary>
    private Font font;

    /// <summary>
    /// Stores the font color.
    /// </summary>
    private Color fontColor;

    /// <summary>
    /// Stores the font alignment.
    /// </summary>
    private VGAlignment alignment;

    /// <summary>
    /// The event that is raised whenever this controls font style changes.
    /// </summary>
    public event EventHandler<FontChangedEventArgs> FontStyleChanged;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets or sets the font of this control
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public Font SelectedFont
    {
      get
      {
        return font;
      }
      set
      {
        font = (Font)value.Clone();
        SetFontSelection(value);
        OnFontStyleChanged(new FontChangedEventArgs(
          this.font,
          this.fontColor,
          this.alignment,
          VGStyleGroup.None));
      }
    }

    /// <summary>
    /// Gets or sets the font color for the text
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public Color SelectedFontColor
    {
      get
      {
        return fontColor;
      }
      set
      {
        fontColor = value;
        this.fontColorSelectControl.SelectedColor = value;
        OnFontStyleChanged(new FontChangedEventArgs(
          this.font,
          this.fontColor,
          this.alignment,
          VGStyleGroup.None));
      }
    }

    /// <summary>
    /// Gets or sets the alignment for the text
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public VGAlignment SelectedFontAlignment
    {
      get
      {
        return this.alignment;
      }
      set
      {
        this.alignment = value;
        this.cbbAlignment.SelectedItem = value;
        OnFontStyleChanged(new FontChangedEventArgs(
          this.font,
          this.fontColor,
          this.alignment,
          VGStyleGroup.None));
      }
    }

    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Constructor.
    /// </summary>
    public FontSelectControl()
    {
      InitializeComponent();
      foreach (object item in Enum.GetValues(typeof(VGAlignment)))
      {
        this.cbbAlignment.Items.Add((VGAlignment)item);
      }

      this.cbbAlignment.SelectedItem = VGAlignment.Center;

      foreach (object item in Enum.GetValues(typeof(FontStyle)))
      {
        this.cbbFontStyle.Items.Add((FontStyle)item);
      }
      this.cbbFontStyle.SelectedItem = FontStyle.Regular;
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

    private void OnFontSizeTextChanged(object sender, System.EventArgs e)
    {
      RecreateFont();
    }

    private void OnFontStyleTextChanged(object sender, System.EventArgs e)
    {
      RecreateFont();
    }

    private void OnFontFaceChanged(object sender, System.EventArgs e)
    {
      RecreateFont();
    }

    private void fontColorSelectControl_ColorChanged(object sender, ColorChangedEventArgs e)
    {
      RecreateFont();
    }

    private void nudFontSize_ValueChanged(object sender, EventArgs e)
    {
      RecreateFont();
    }

    private void cbbAlignment_SelectedIndexChanged(object sender, EventArgs e)
    {
      RecreateFont();
    }

    #endregion //WINDOWSEVENTHANDLER

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler for Custom Defined Events                                    //
    ///////////////////////////////////////////////////////////////////////////////
    #region CUSTOMEVENTHANDLER

    /// <summary>
    /// The font style has been modified, notify listeners.
    /// </summary>
    private void OnFontStyleChanged(FontChangedEventArgs e)
    {
      if (FontStyleChanged != null)
      {
        // Invokes the delegates. 
        FontStyleChanged(this, e);
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

    private void RecreateFont()
    {
      if (cbbFontStyle.SelectedItem != null)
      {
        Single fontSize = (float)nudFontSize.Value;
        FontStyle style = (FontStyle)this.cbbFontStyle.SelectedItem;
        this.alignment = (VGAlignment)this.cbbAlignment.SelectedItem;

        font = new Font(cbbFontFace.Text, fontSize, style);
        fontColor = fontColorSelectControl.SelectedColor;
        fontStyleArea.FontColor = fontColor;
        fontStyleArea.Font = font;
        fontStyleArea.FontAlignment = this.alignment;

        OnFontStyleChanged(new FontChangedEventArgs(
          this.font,
          this.fontColor,
          this.alignment,
          VGStyleGroup.None));
      }
    }

    /// <summary>
    /// Setup the controls to match the current font
    /// </summary>
    public void SetFontSelection(Font font)
    {
      // font face
      InstalledFontCollection fontSet = new InstalledFontCollection();
      FontFamily[] fontList = fontSet.Families;
      int SelIndex = 0;
      for (int ii = 0; ii < fontList.Length; ii++)
      {
        if (fontList[ii].IsStyleAvailable(FontStyle.Bold) &&
           fontList[ii].IsStyleAvailable(FontStyle.Italic) &&
           fontList[ii].IsStyleAvailable(FontStyle.Underline) &&
           fontList[ii].IsStyleAvailable(FontStyle.Strikeout))
        {
          int idx = cbbFontFace.Items.Add(fontList[ii].Name);
          if (fontList[ii].Name == font.Name)
          {
            SelIndex = idx;
          }
        }
      }
      FontStyle style = font.Style;
      cbbFontFace.SelectedIndex = SelIndex;

      for (int i = 0; i < cbbFontStyle.Items.Count; i++)
      {
        if (cbbFontStyle.Items[i].ToString()==style.ToString())
        {
          SelIndex = i;
          break;
        }
      }

      cbbFontStyle.SelectedIndex = SelIndex;
      // font size
      nudFontSize.Value = (decimal)font.Size;
    }

    #endregion //METHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER
    #endregion //HELPER
  }
}
