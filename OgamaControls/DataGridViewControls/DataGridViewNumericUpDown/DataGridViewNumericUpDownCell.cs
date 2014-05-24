using System;
using System.Drawing;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Forms;
using System.ComponentModel;

namespace OgamaControls
{
  /// <summary>
  /// Defines a <see cref="NumericUpDown"/> cell type for the <see cref="DataGridView"/> control
  /// </summary>
  public class DataGridViewNumericUpDownCell : DataGridViewTextBoxCell
  {
    ///////////////////////////////////////////////////////////////////////////////
    // Defining Constants                                                        //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTANTS

    /// <summary>
    /// Used in <see cref="TranslateAlignment(DataGridViewContentAlignment)"/> function
    /// </summary>
    private static readonly DataGridViewContentAlignment anyRight = DataGridViewContentAlignment.TopRight |
                                                                    DataGridViewContentAlignment.MiddleRight |
                                                                    DataGridViewContentAlignment.BottomRight;

    /// <summary>
    /// Used in <see cref="TranslateAlignment(DataGridViewContentAlignment)"/> function
    /// </summary>
    private static readonly DataGridViewContentAlignment anyCenter = DataGridViewContentAlignment.TopCenter |
                                                                     DataGridViewContentAlignment.MiddleCenter |
                                                                     DataGridViewContentAlignment.BottomCenter;

    /// <summary>
    /// Default Width of the static rendering bitmap used for the painting of the non-edited cells
    /// </summary>
    private const int DATAGRIDVIEWNUMERICUPDOWNCELL_defaultRenderingBitmapWidth = 100;

    /// <summary>
    /// Default height of the static rendering bitmap used for the painting of the non-edited cells
    /// </summary>
    private const int DATAGRIDVIEWNUMERICUPDOWNCELL_defaultRenderingBitmapHeight = 22;

    /// <summary>
    /// Default value of the DefaultValue property
    /// </summary>
    public const int DATAGRIDVIEWNUMERICUPDOWNCELL_defaultValue = 8;

    /// <summary>
    /// Default value of the DecimalPlaces property
    /// </summary>
    internal const int DATAGRIDVIEWNUMERICUPDOWNCELL_defaultDecimalPlaces = 0;

    /// <summary>
    /// Default value of the Increment property
    /// </summary>
    internal const Decimal DATAGRIDVIEWNUMERICUPDOWNCELL_defaultIncrement = Decimal.One;

    /// <summary>
    /// Default value of the Maximum property
    /// </summary>
    internal const Decimal DATAGRIDVIEWNUMERICUPDOWNCELL_defaultMaximum = (Decimal)1000.0;

    /// <summary>
    /// Default value of the Minimum property
    /// </summary>
    internal const Decimal DATAGRIDVIEWNUMERICUPDOWNCELL_defaultMinimum = Decimal.Zero;

    /// <summary>
    /// Default value of the ThousandsSeparator property
    /// </summary>
    internal const bool DATAGRIDVIEWNUMERICUPDOWNCELL_defaultThousandsSeparator = false;

    /// <summary>
    /// Type of this cell's editing control
    /// </summary>
    private static Type defaultEditType = typeof(DataGridViewNumericUpDownEditingControl);

    /// <summary>
    /// Type of this cell's value. The formatted value type is string, 
    /// the same as the base class <see cref="DataGridViewTextBoxCell"/>
    /// </summary>
    private static Type defaultValueType = typeof(System.Decimal);

    /// <summary>
    /// The bitmap used to paint the non-edited cells via a 
    /// call to <see cref="Control.DrawToBitmap(Bitmap,Rectangle)"/>
    /// </summary>
    [ThreadStatic]
    private static Bitmap renderingBitmap;

    /// <summary>
    /// The NumericUpDown control used to paint the non-edited 
    /// cells via a call to <see cref="Control.DrawToBitmap(Bitmap,Rectangle)"/>
    /// </summary>
    [ThreadStatic]
    private static NumericUpDown paintingNumericUpDown;


    #endregion //CONSTANTS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Variables, Enumerations, Events                                  //
    ///////////////////////////////////////////////////////////////////////////////
    #region FIELDS

    /// <summary>
    /// Caches the value of the DecimalPlaces property
    /// </summary>
    private int decimalPlaces;

    /// <summary>
    /// Caches the value of the Increment property
    /// </summary>
    private Decimal increment;

    /// <summary>
    /// Caches the value of the Minimum property
    /// </summary>
    private Decimal minimum;

    /// <summary>
    /// Caches the value of the Maximum property
    /// </summary>
    private Decimal maximum;

    /// <summary>
    /// Caches the value of the ThousandsSeparator property
    /// </summary>
    private bool thousandsSeparator;

    /// <summary>
    /// Default value of the cell.
    /// </summary>
    private int defaultValue;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// The DecimalPlaces property replicates the one from the <see cref="NumericUpDown"/> control.
    /// </summary>
    /// <value>The number of decimal places to display in the spin box. The default is 0.</value>
    [DefaultValue(DATAGRIDVIEWNUMERICUPDOWNCELL_defaultDecimalPlaces)]
    public int DecimalPlaces
    {
      get { return this.decimalPlaces; }
      set
      {
        if (value < 0 || value > 99)
        {
          throw new ArgumentOutOfRangeException("The DecimalPlaces property cannot be smaller than 0 or larger than 99.");
        }
        if (this.decimalPlaces != value)
        {
          SetDecimalPlaces(this.RowIndex, value);
          OnCommonChange();  // Assure that the cell or column gets repainted and autosized if needed
        }
      }
    }

    /// <summary>
    /// Returns the current DataGridView EditingControl as a 
    /// DataGridViewNumericUpDownEditingControl control
    /// </summary>
    private DataGridViewNumericUpDownEditingControl EditingNumericUpDown
    {
      get
      {
        return this.DataGridView.EditingControl as DataGridViewNumericUpDownEditingControl;
      }
    }

    /// <summary>
    /// Overridden. Define the type of the cell's editing control
    /// </summary>
    /// <value>A <see cref="Type"/> representing the <see cref="DataGridViewNumericUpDownEditingControl"/> type.</value>
    public override Type EditType
    {
      get
      {
        return defaultEditType; // the type is DataGridViewNumericUpDownEditingControl
      }
    }

    /// <summary>
    /// Gets or sets default value if the numeric up down cell.
    /// </summary>
    public int DefaultValue
    {
      get { return this.defaultValue; }
      set
      {
        if (this.defaultValue != value)
        {
          SetDefaultValue(this.RowIndex, value);
          OnCommonChange();  // Assure that the cell or column gets repainted and autosized if needed
        }
      }
    }

    /// <summary>
    /// Gets or sets the value to increment or decrement the spin box 
    /// (also known as an up-down control) when the up or down buttons are clicked.
    /// </summary>
    /// <value>The value to increment or decrement the value property when the up or 
    /// down buttons are clicked on the spin box. The default value is 1. </value>
    public Decimal Increment
    {
      get { return this.increment; }
      set
      {
        if (value < (Decimal)0.0)
        {
          throw new ArgumentOutOfRangeException("The Increment property cannot be smaller than 0.");
        }
        SetIncrement(this.RowIndex, value);
        // No call to OnCommonChange is needed since the increment value does not affect the rendering of the cell.
      }
    }

    /// <summary>
    /// Gets or sets the maximum value for the spin box (also known as an up-down control).
    /// </summary>
    /// <value>The maximum value for the spin box. The default value is 100. </value>
    public Decimal Maximum
    {
      get { return this.maximum; }
      set
      {
        if (this.maximum != value)
        {
          SetMaximum(this.RowIndex, value);
          OnCommonChange();
        }
      }
    }

    /// <summary>
    /// Gets or sets the minimum allowed value for the spin box 
    /// (also known as an up-down control). 
    /// </summary>
    /// <value>The minimum allowed value for the spin box. 
    /// The default value is 0.</value>
    public Decimal Minimum
    {
      get { return this.minimum; }
      set
      {
        if (this.minimum != value)
        {
          SetMinimum(this.RowIndex, value);
          OnCommonChange();
        }
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether a thousands separator 
    /// is displayed in the spin box (also known as an up-down control) when appropriate.
    /// </summary>
    /// <value><strong>true</strong> if a thousands separator is displayed in the spin 
    /// box when appropriate; otherwise, <strong>false</strong>. 
    /// The default value is <strong>false</strong>.</value>
    [DefaultValue(DATAGRIDVIEWNUMERICUPDOWNCELL_defaultThousandsSeparator)]
    public bool ThousandsSeparator
    {
      get { return this.thousandsSeparator; }
      set
      {
        if (this.thousandsSeparator != value)
        {
          SetThousandsSeparator(this.RowIndex, value);
          OnCommonChange();
        }
      }
    }

    /// <summary>
    /// Overridden. Gets or sets the data type of the values in the cell. 
    /// </summary>
    /// <value>A <see cref="Type"/> representing the data type of the value in the cell. </value>
    public override Type ValueType
    {
      get
      {
        Type valueType = base.ValueType;
        if (valueType != null)
        {
          return valueType;
        }
        return defaultValueType;
      }
    }

    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Constructor for the <see cref="DataGridViewNumericUpDownCell"/> cell type
    /// </summary>
    public DataGridViewNumericUpDownCell()
    {
      // Create a thread specific bitmap used for the painting of the non-edited cells
      if (renderingBitmap == null)
      {
        renderingBitmap = new Bitmap(DATAGRIDVIEWNUMERICUPDOWNCELL_defaultRenderingBitmapWidth, 
          DATAGRIDVIEWNUMERICUPDOWNCELL_defaultRenderingBitmapHeight);
      }

      // Create a thread specific NumericUpDown control used for the painting of the non-edited cells
      if (paintingNumericUpDown == null)
      {
        paintingNumericUpDown = new NumericUpDown();
        // Some properties only need to be set once for the lifetime of the control:
        paintingNumericUpDown.BorderStyle = BorderStyle.None;
        paintingNumericUpDown.Maximum = Decimal.MaxValue / 10;
        paintingNumericUpDown.Minimum = Decimal.MinValue / 10;
        paintingNumericUpDown.Value = DATAGRIDVIEWNUMERICUPDOWNCELL_defaultValue;
      }

      // Set the default values of the properties:
      this.decimalPlaces = DATAGRIDVIEWNUMERICUPDOWNCELL_defaultDecimalPlaces;
      this.increment = DATAGRIDVIEWNUMERICUPDOWNCELL_defaultIncrement;
      this.minimum = DATAGRIDVIEWNUMERICUPDOWNCELL_defaultMinimum;
      this.maximum = DATAGRIDVIEWNUMERICUPDOWNCELL_defaultMaximum;
      this.thousandsSeparator = DATAGRIDVIEWNUMERICUPDOWNCELL_defaultThousandsSeparator;
      this.defaultValue = DATAGRIDVIEWNUMERICUPDOWNCELL_defaultValue;
      DataGridViewCellStyle newStyle = new DataGridViewCellStyle();
      newStyle.Alignment = DataGridViewContentAlignment.TopLeft;
      this.Style = newStyle;

      this.Value = defaultValue;
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

    /// <summary>
    /// Overridden. Creates an exact copy of this cell. 
    /// </summary>
    /// <returns>An <see cref="Object"/> that represents the 
    /// cloned <see cref="DataGridViewNumericUpDownCell"/>.</returns>
    public override object Clone()
    {
      DataGridViewNumericUpDownCell dataGridViewCell = base.Clone() as DataGridViewNumericUpDownCell;
      if (dataGridViewCell != null)
      {
        dataGridViewCell.DecimalPlaces = this.DecimalPlaces;
        dataGridViewCell.Increment = this.Increment;
        dataGridViewCell.Maximum = this.Maximum;
        dataGridViewCell.Minimum = this.Minimum;
        dataGridViewCell.ThousandsSeparator = this.ThousandsSeparator;
        dataGridViewCell.DefaultValue = this.defaultValue;
      }
      return dataGridViewCell;
    }

    /// <summary>
    /// DetachEditingControl gets called by the DataGridView control 
    /// when the editing session is ending.
    /// Removes the cell's editing control from the DataGridView.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public override void DetachEditingControl()
    {
      DataGridView dataGridView = this.DataGridView;
      if (dataGridView == null || dataGridView.EditingControl == null)
      {
        throw new InvalidOperationException("Cell is detached or its grid has no editing control.");
      }

      NumericUpDown numericUpDown = dataGridView.EditingControl as NumericUpDown;
      if (numericUpDown != null)
      {
        // Editing controls get recycled. Indeed, when a DataGridViewNumericUpDownCell cell gets edited
        // after another DataGridViewNumericUpDownCell cell, the same editing control gets reused for 
        // performance reasons (to avoid an unnecessary control destruction and creation). 
        // Here the undo buffer of the TextBox inside the NumericUpDown control gets cleared to avoid
        // interferences between the editing sessions.
        TextBox textBox = numericUpDown.Controls[1] as TextBox;
        if (textBox != null)
        {
          textBox.ClearUndo();
        }
      }

      base.DetachEditingControl();
    }

    /// <summary>
    /// Customized implementation of the GetErrorIconBounds function in order to draw the potential 
    /// error icon next to the up/down buttons and not on top of them.
    /// Returns the bounding rectangle that encloses the cell's error icon, if one is displayed. 
    /// </summary>
    /// <param name="graphics">The graphics context for the cell.</param>
    /// <param name="cellStyle">The <see cref="DataGridViewCellStyle"/> to be applied to the cell.</param>
    /// <param name="rowIndex">The index of the cell's parent row.</param>
    /// <returns>The <see cref="Rectangle"/> that bounds the cell's error icon, if one is displayed; otherwise, <see cref="Empty"/>. </returns>
    protected override Rectangle GetErrorIconBounds(Graphics graphics, DataGridViewCellStyle cellStyle, int rowIndex)
    {
      const int ButtonsWidth = 16;

      Rectangle errorIconBounds = base.GetErrorIconBounds(graphics, cellStyle, rowIndex);
      if (this.DataGridView.RightToLeft == RightToLeft.Yes)
      {
        errorIconBounds.X = errorIconBounds.Left + ButtonsWidth;
      }
      else
      {
        errorIconBounds.X = errorIconBounds.Left - ButtonsWidth;
      }
      return errorIconBounds;
    }

    /// <summary>
    /// Customized implementation of the GetFormattedValue function
    /// in order to include the decimal and thousand separator
    /// characters in the formatted representation of the cell value.
    /// </summary>
    /// <param name="value">The value to be formatted. </param>
    /// <param name="rowIndex">The index of the cell's parent row.</param>
    /// <param name="cellStyle">The <see cref="DataGridViewCellStyle"/> in effect for the cell.</param>
    /// <param name="valueTypeConverter">A <see cref="TypeConverter"/> associated with the value 
    /// type that provides custom conversion to the formatted value type, 
    /// or a null reference (Nothing in Visual Basic) if no such custom conversion is needed.</param>
    /// <param name="formattedValueTypeConverter">A <see cref="TypeConverter"/> associated with the 
    /// formatted value type that provides custom conversion from the value type,
    /// or a null reference (Nothing in Visual Basic) if no such custom conversion is needed.</param>
    /// <param name="context">A bitwise combination of <see cref="DataGridViewDataErrorContexts"/> values 
    /// describing the context in which the formatted value is needed.</param>
    /// <returns>The formatted value of the cell or a null reference (Nothing in Visual Basic) if the cell does not belong to a <see cref="DataGridView"/> control.</returns>
    protected override object GetFormattedValue(object value,
                                                int rowIndex,
                                                ref DataGridViewCellStyle cellStyle,
                                                TypeConverter valueTypeConverter,
                                                TypeConverter formattedValueTypeConverter,
                                                DataGridViewDataErrorContexts context)
    {
      // By default, the base implementation converts the Decimal 1234.5 into the string "1234.5"
      object formattedValue = base.GetFormattedValue(value, rowIndex, ref cellStyle, valueTypeConverter, formattedValueTypeConverter, context);
      string formattedNumber = formattedValue as string;
      if (!string.IsNullOrEmpty(formattedNumber) && value != null)
      {
        Decimal unformattedDecimal = System.Convert.ToDecimal(value);
        Decimal formattedDecimal = System.Convert.ToDecimal(formattedNumber);
        if (unformattedDecimal == formattedDecimal)
        {
          // The base implementation of GetFormattedValue (which triggers the CellFormatting event) did nothing else than 
          // the typical 1234.5 to "1234.5" conversion. But depending on the values of ThousandsSeparator and DecimalPlaces,
          // this may not be the actual string displayed. The real formatted value may be "1,234.500"
          return formattedDecimal.ToString((this.ThousandsSeparator ? "N" : "F") + this.DecimalPlaces.ToString());
        }
      }
      else
      {
        //Set standard entry
        return defaultValue;
      }
      return formattedValue;
    }

    /// <summary>
    /// Custom implementation of the GetPreferredSize function. 
    /// This implementation uses the preferred size of the base 
    /// DataGridViewTextBoxCell cell and adds room for the up/down buttons.
    /// Calculates the preferred size, in pixels, of the cell. 
    /// </summary>
    /// <param name="graphics">The <see cref="Graphics"/> used to draw the cell.</param>
    /// <param name="cellStyle">A <see cref="DataGridViewCellStyle"/> that represents the style of the cell.</param>
    /// <param name="rowIndex">The zero-based row index of the cell.</param>
    /// <param name="constraintSize">The cell's maximum allowable size.</param>
    /// <returns>A <see cref="Size"/> that represents the preferred size, in pixels, of the cell. </returns>
    protected override Size GetPreferredSize(Graphics graphics, DataGridViewCellStyle cellStyle, int rowIndex, Size constraintSize)
    {
      if (this.DataGridView == null)
      {
        return new Size(-1, -1);
      }

      Size preferredSize = base.GetPreferredSize(graphics, cellStyle, rowIndex, constraintSize);
      if (constraintSize.Width == 0)
      {
        const int ButtonsWidth = 16; // Account for the width of the up/down buttons.
        const int ButtonMargin = 8;  // Account for some blank pixels between the text and buttons.
        preferredSize.Width += ButtonsWidth + ButtonMargin;
      }
      return preferredSize;
    }

    /// <summary>
    /// Custom implementation of the InitializeEditingControl function. 
    /// This function is called by the DataGridView control 
    /// at the beginning of an editing session. It makes sure that 
    /// the properties of the NumericUpDown editing control are 
    /// set according to the cell properties.
    /// </summary>
    /// <param name="rowIndex">The zero-based row index of the cell.</param>
    /// <param name="initialFormattedValue">An <see cref="Object"/> that represents the value displayed by the cell when editing is started.</param>
    /// <param name="dataGridViewCellStyle">A <see cref="DataGridViewCellStyle"/> that represents the style of the cell.</param>
    public override void InitializeEditingControl(int rowIndex, object initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
    {
      base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);
      NumericUpDown numericUpDown = this.DataGridView.EditingControl as NumericUpDown;
      if (numericUpDown != null)
      {
        numericUpDown.BorderStyle = BorderStyle.None;
        numericUpDown.DecimalPlaces = this.DecimalPlaces;
        numericUpDown.Increment = this.Increment;
        numericUpDown.Maximum = this.Maximum;
        numericUpDown.Minimum = this.Minimum;
        numericUpDown.ThousandsSeparator = this.ThousandsSeparator;

        string initialFormattedValueStr = initialFormattedValue as string;
        if (initialFormattedValueStr == null)
        {
          numericUpDown.Text = string.Empty;
        }
        else
        {
          numericUpDown.Text = initialFormattedValueStr;
        }
      }
    }

    /// <summary>
    /// Custom implementation of the KeyEntersEditMode function. 
    /// This function is called by the DataGridView control
    /// to decide whether a keystroke must start an editing session or not. 
    /// In this case, a new session is started when
    /// a digit or negative sign key is hit.
    /// </summary>
    /// <param name="e">A <see cref="KeyEventArgs"/> that represents the key that was pressed.</param>
    /// <returns><strong>true</strong> if edit mode should be started; 
    /// otherwise, <strong>false</strong>.</returns>
    public override bool KeyEntersEditMode(KeyEventArgs e)
    {
      NumberFormatInfo numberFormatInfo = System.Globalization.CultureInfo.CurrentCulture.NumberFormat;
      Keys negativeSignKey = Keys.None;
      string negativeSignStr = numberFormatInfo.NegativeSign;
      if (!string.IsNullOrEmpty(negativeSignStr) && negativeSignStr.Length == 1)
      {
        negativeSignKey = (Keys)(User32.VkKeyScan(negativeSignStr[0]));
      }

      if ((char.IsDigit((char)e.KeyCode) ||
           (e.KeyCode >= Keys.NumPad0 && e.KeyCode <= Keys.NumPad9) ||
           negativeSignKey == e.KeyCode ||
           Keys.Subtract == e.KeyCode) &&
          !e.Shift && !e.Alt && !e.Control)
      {
        return true;
      }
      return false;
    }

    /// <summary>
    /// Custom paints the cell. The base implementation of the 
    /// DataGridViewTextBoxCell type is called first,
    /// dropping the icon error and content foreground parts. 
    /// Those two parts are painted by this custom implementation.
    /// In this sample, the non-edited NumericUpDown control is 
    /// painted by using a call to Control.DrawToBitmap. This is
    /// an easy solution for painting controls but it's not necessarily 
    /// the most performant. An alternative would be to paint
    /// the NumericUpDown control piece by piece (text and up/down buttons).
    /// </summary>
    /// <param name="graphics">The <see cref="Graphics"/> used to paint the <see cref="DataGridViewCell"/>.</param>
    /// <param name="clipBounds">A <see cref="Rectangle"/> that represents the area of the <see cref="DataGridView"/> that needs to be repainted.</param>
    /// <param name="cellBounds">A <see cref="Rectangle"/> that contains the bounds of the <see cref="DataGridViewCell"/> that is being painted.</param>
    /// <param name="rowIndex">The row index of the cell that is being painted.</param>
    /// <param name="cellState">A bitwise combination of <see cref="DataGridViewElementStates"/> values that specifies the state of the cell.</param>
    /// <param name="value">The data of the <see cref="DataGridViewCell"/> that is being painted.</param>
    /// <param name="formattedValue">The formatted data of the <see cref="DataGridViewCell"/> that is being painted.</param>
    /// <param name="errorText">An error message that is associated with the cell.</param>
    /// <param name="cellStyle">A <see cref="DataGridViewCellStyle"/> that contains formatting and style information about the cell.</param>
    /// <param name="advancedBorderStyle">A <see cref="DataGridViewAdvancedBorderStyle"/> that contains border styles for the cell that is being painted.</param>
    /// <param name="paintParts">A bitwise combination of the <see cref="DataGridViewPaintParts"/> values that specifies which parts of the cell need to be painted.</param>
    protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState,
                                  object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle,
                                  DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
    {
      if (this.DataGridView == null)
      {
        return;
      }

      // First paint the borders and background of the cell.
      base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, value, formattedValue, errorText, cellStyle, advancedBorderStyle,
                 paintParts & ~(DataGridViewPaintParts.ErrorIcon | DataGridViewPaintParts.ContentForeground));

      Point ptCurrentCell = this.DataGridView.CurrentCellAddress;
      bool cellCurrent = ptCurrentCell.X == this.ColumnIndex && ptCurrentCell.Y == rowIndex;
      bool cellEdited = cellCurrent && this.DataGridView.EditingControl != null;

      // If the cell is in editing mode, there is nothing else to paint
      if (!cellEdited)
      {
        if (PartPainted(paintParts, DataGridViewPaintParts.ContentForeground))
        {
          // Paint a NumericUpDown control
          // Take the borders into account
          Rectangle borderWidths = BorderWidths(advancedBorderStyle);
          Rectangle valBounds = cellBounds;
          valBounds.Offset(borderWidths.X, borderWidths.Y);
          valBounds.Width -= borderWidths.Right;
          valBounds.Height -= borderWidths.Bottom;
          // Also take the padding into account
          if (cellStyle.Padding != Padding.Empty)
          {
            if (this.DataGridView.RightToLeft == RightToLeft.Yes)
            {
              valBounds.Offset(cellStyle.Padding.Right, cellStyle.Padding.Top);
            }
            else
            {
              valBounds.Offset(cellStyle.Padding.Left, cellStyle.Padding.Top);
            }
            valBounds.Width -= cellStyle.Padding.Horizontal;
            valBounds.Height -= cellStyle.Padding.Vertical;
          }
          // Determine the NumericUpDown control location
          valBounds = GetAdjustedEditingControlBounds(valBounds, cellStyle);

          bool cellSelected = (cellState & DataGridViewElementStates.Selected) != 0;

          if (renderingBitmap.Width < valBounds.Width ||
              renderingBitmap.Height < valBounds.Height)
          {
            // The static bitmap is too small, a bigger one needs to be allocated.
            renderingBitmap.Dispose();
            renderingBitmap = new Bitmap(valBounds.Width, valBounds.Height);
          }
          // Make sure the NumericUpDown control is parented to a visible control
          if (paintingNumericUpDown.Parent == null || !paintingNumericUpDown.Parent.Visible)
          {
            paintingNumericUpDown.Parent = this.DataGridView;
          }
          // Set all the relevant properties
          paintingNumericUpDown.TextAlign = DataGridViewNumericUpDownCell.TranslateAlignment(cellStyle.Alignment);
          paintingNumericUpDown.DecimalPlaces = this.DecimalPlaces;
          paintingNumericUpDown.ThousandsSeparator = this.ThousandsSeparator;
          paintingNumericUpDown.Font = cellStyle.Font;
          paintingNumericUpDown.Width = valBounds.Width;
          paintingNumericUpDown.Height = valBounds.Height;
          paintingNumericUpDown.RightToLeft = this.DataGridView.RightToLeft;
          paintingNumericUpDown.Location = new Point(0, -paintingNumericUpDown.Height - 100);
          paintingNumericUpDown.Text = formattedValue as string;
          //paintingNumericUpDown.Value = Decimal.Parse(formattedValue as string);

          Color backColor;
          if (PartPainted(paintParts, DataGridViewPaintParts.SelectionBackground) && cellSelected)
          {
            backColor = cellStyle.SelectionBackColor;
          }
          else
          {
            backColor = cellStyle.BackColor;
          }
          if (PartPainted(paintParts, DataGridViewPaintParts.Background))
          {
            if (backColor.A < 255)
            {
              // The NumericUpDown control does not support transparent back colors
              backColor = Color.FromArgb(255, backColor);
            }
            paintingNumericUpDown.BackColor = backColor;
          }
          // Finally paint the NumericUpDown control
          Rectangle srcRect = new Rectangle(0, 0, valBounds.Width, valBounds.Height);
          if (srcRect.Width > 0 && srcRect.Height > 0)
          {
            paintingNumericUpDown.DrawToBitmap(renderingBitmap, srcRect);
            graphics.DrawImage(renderingBitmap, new Rectangle(valBounds.Location, valBounds.Size),
                               srcRect, GraphicsUnit.Pixel);
          }
        }
        if (PartPainted(paintParts, DataGridViewPaintParts.ErrorIcon))
        {
          // Paint the potential error icon on top of the NumericUpDown control
          base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, value, formattedValue, errorText,
                     cellStyle, advancedBorderStyle, DataGridViewPaintParts.ErrorIcon);
        }
      }
    }
    /// <summary>
    /// Custom implementation of the PositionEditingControl method called 
    /// by the <see cref="DataGridView"/> control when it
    /// needs to relocate and/or resize the editing control.
    /// </summary>
    /// <param name="setLocation"><strong>true</strong> to have the control placed as 
    /// specified by the other arguments; <strong>false</strong> to allow the control to place itself.</param>
    /// <param name="setSize"><strong>true</strong> to specify the size; 
    /// <strong>false</strong> to allow the control to size itself. </param>
    /// <param name="cellBounds">A <see cref="Rectangle"/> that defines the cell bounds. </param>
    /// <param name="cellClip">The area that will be used to paint the editing control.</param>
    /// <param name="cellStyle">A <see cref="DataGridViewCellStyle"/> that represents the style of the cell being edited.</param>
    /// <param name="singleVerticalBorderAdded"><strong>true</strong> to add a vertical border to the cell; 
    /// otherwise, <strong>false</strong>.</param>
    /// <param name="singleHorizontalBorderAdded"><strong>true</strong> to add a horizontal border to the cell;
    /// otherwise, <strong>false</strong>.</param>
    /// <param name="isFirstDisplayedColumn"><strong>true</strong> if the hosting cell is in 
    /// the first visible column; otherwise, <strong>false</strong>.</param>
    /// <param name="isFirstDisplayedRow"><strong>true</strong> if the hosting cell is in 
    /// the first visible row; otherwise, <strong>false</strong>.</param>
    public override void PositionEditingControl(bool setLocation,
                                        bool setSize,
                                        Rectangle cellBounds,
                                        Rectangle cellClip,
                                        DataGridViewCellStyle cellStyle,
                                        bool singleVerticalBorderAdded,
                                        bool singleHorizontalBorderAdded,
                                        bool isFirstDisplayedColumn,
                                        bool isFirstDisplayedRow)
    {
      Rectangle editingControlBounds = PositionEditingPanel(cellBounds,
                                                  cellClip,
                                                  cellStyle,
                                                  singleVerticalBorderAdded,
                                                  singleHorizontalBorderAdded,
                                                  isFirstDisplayedColumn,
                                                  isFirstDisplayedRow);
      editingControlBounds = GetAdjustedEditingControlBounds(editingControlBounds, cellStyle);
      this.DataGridView.EditingControl.Location = new Point(editingControlBounds.X, editingControlBounds.Y);
      this.DataGridView.EditingControl.Size = new Size(editingControlBounds.Width, editingControlBounds.Height);
    }

    /// <summary>
    /// Returns a string that describes the current object. 
    /// </summary>
    /// <returns>A <see cref="string"/> that represents the current object. </returns>
    public override string ToString()
    {
      return "DataGridViewNumericUpDownCell { ColumnIndex=" + ColumnIndex.ToString(CultureInfo.CurrentCulture) + ", RowIndex=" + RowIndex.ToString(CultureInfo.CurrentCulture) + " }";
    }

    #endregion //OVERRIDES

    ///////////////////////////////////////////////////////////////////////////////
    // Methods for doing main class job                                          //
    ///////////////////////////////////////////////////////////////////////////////
    #region METHODS

    /// <summary>
    /// Returns the provided value constrained to be within the min and max. 
    /// </summary>
    /// <param name="value">value to constrain</param>
    /// <returns>A <see cref="Decimal"/> with the constrained value.</returns>
    private Decimal Constrain(Decimal value)
    {
      Debug.Assert(this.minimum <= this.maximum);
      if (value < this.minimum)
      {
        value = this.minimum;
      }
      if (value > this.maximum)
      {
        value = this.maximum;
      }
      return value;
    }

    /// <summary>
    /// Adjusts the location and size of the editing control given 
    /// the alignment characteristics of the cell
    /// </summary>
    /// <param name="editingControlBounds">A <see cref="Rectangle"/> with the editing controls bounds.</param>
    /// <param name="cellStyle">A <see cref="DataGridViewCellStyle"/> enumeration member.</param>
    /// <returns>An adjusted bounding <see cref="Rectangle"/> for the Editing Control</returns>
    private Rectangle GetAdjustedEditingControlBounds(Rectangle editingControlBounds, DataGridViewCellStyle cellStyle)
    {
      // Add a 1 pixel padding on the left and right of the editing control
      editingControlBounds.X += 3;
      editingControlBounds.Y += 3;
      editingControlBounds.Width = Math.Max(0, editingControlBounds.Width - 5);
      editingControlBounds.Height = Math.Max(0, editingControlBounds.Width - 5);

      // Adjust the vertical location of the editing control:
      int preferredHeight = cellStyle.Font.Height + 3;
      if (preferredHeight < editingControlBounds.Height)
      {
        switch (cellStyle.Alignment)
        {
          case DataGridViewContentAlignment.MiddleLeft:
          case DataGridViewContentAlignment.MiddleCenter:
          case DataGridViewContentAlignment.MiddleRight:
            editingControlBounds.Y += (editingControlBounds.Height - preferredHeight) / 2;
            break;
          case DataGridViewContentAlignment.BottomLeft:
          case DataGridViewContentAlignment.BottomCenter:
          case DataGridViewContentAlignment.BottomRight:
            editingControlBounds.Y += editingControlBounds.Height - preferredHeight;
            break;
        }
      }

      return editingControlBounds;
    }

    /// <summary>
    /// Called when a cell characteristic that affects its rendering and/or preferred size has changed.
    /// This implementation only takes care of repainting the cells. The DataGridView's autosizing methods
    /// also need to be called in cases where some grid elements autosize.
    /// </summary>
    private void OnCommonChange()
    {
      if (this.DataGridView != null && !this.DataGridView.IsDisposed && !this.DataGridView.Disposing)
      {
        if (this.RowIndex == -1)
        {
          // Invalidate and autosize column
          this.DataGridView.InvalidateColumn(this.ColumnIndex);

          // TODO: Add code to autosize the cell's column, the rows, the column headers 
          // and the row headers depending on their autosize settings.
          // The DataGridView control does not expose a public method that takes care of this.
        }
        else
        {
          // The DataGridView control exposes a public method called UpdateCellValue
          // that invalidates the cell so that it gets repainted and also triggers all
          // the necessary autosizing: the cell's column and/or row, the column headers
          // and the row headers are autosized depending on their autosize settings.
          this.DataGridView.UpdateCellValue(this.ColumnIndex, this.RowIndex);
        }
      }
    }

    /// <summary>
    /// Utility function that sets a new value for the DecimalPlaces property of the cell. This function is used by
    /// the cell and column DecimalPlaces property. The column uses this method instead of the DecimalPlaces
    /// property for performance reasons. This way the column can invalidate the entire column at once instead of 
    /// invalidating each cell of the column individually. A row index needs to be provided as a parameter because
    /// this cell may be shared among multiple rows.
    /// </summary>
    /// <param name="rowIndex">The zero-based row index of the cell's location.</param>
    /// <param name="value">A <see cref="int"/> decimal places value</param>
    internal void SetDecimalPlaces(int rowIndex, int value)
    {
      Debug.Assert(value >= 0 && value <= 99);
      this.decimalPlaces = value;
      if (OwnsEditingNumericUpDown(rowIndex))
      {
        this.EditingNumericUpDown.DecimalPlaces = value;
      }
    }

    /// <summary>
    /// Utility function that sets a new value for the DecimalPlaces property of the cell. This function is used by
    /// the cell and column DecimalPlaces property. The column uses this method instead of the DecimalPlaces
    /// property for performance reasons. This way the column can invalidate the entire column at once instead of 
    /// invalidating each cell of the column individually. A row index needs to be provided as a parameter because
    /// this cell may be shared among multiple rows.
    /// </summary>
    /// <param name="rowIndex">The zero-based row index of the cell's location.</param>
    /// <param name="value">The <see cref="int"/> new default value.</param>
    internal void SetDefaultValue(int rowIndex, int value)
    {
      this.defaultValue = value;
      Decimal currentValue = System.Convert.ToDecimal(value);
      Decimal constrainedValue = Constrain(currentValue);
      {
        SetValue(rowIndex, constrainedValue);
      }

      if (OwnsEditingNumericUpDown(rowIndex))
      {
        this.EditingNumericUpDown.Value = constrainedValue;
      }
    }

    /// <summary>
    /// Utility function that sets a new value for the Increment property of the cell. This function is used by
    /// the cell and column Increment property. A row index needs to be provided as a parameter because
    /// this cell may be shared among multiple rows.
    /// </summary>
    /// <param name="rowIndex">The zero-based row index of the cell's location.</param>
    /// <param name="value">A <see cref="Decimal"/> value of the new increment.</param>
    internal void SetIncrement(int rowIndex, Decimal value)
    {
      Debug.Assert(value >= (Decimal)0.0);
      this.increment = value;
      if (OwnsEditingNumericUpDown(rowIndex))
      {
        this.EditingNumericUpDown.Increment = value;
      }
    }

    /// <summary>
    /// Utility function that sets a new value for the Maximum property of the cell. This function is used by
    /// the cell and column Maximum property. The column uses this method instead of the Maximum
    /// property for performance reasons. This way the column can invalidate the entire column at once instead of 
    /// invalidating each cell of the column individually. A row index needs to be provided as a parameter because
    /// this cell may be shared among multiple rows.
    /// </summary>
    /// <param name="rowIndex">The zero-based row index of the cell's location.</param>
    /// <param name="value">A <see cref="Decimal"/> value of the maximum to set.</param>
    internal void SetMaximum(int rowIndex, Decimal value)
    {
      this.maximum = value;
      if (this.minimum > this.maximum)
      {
        this.minimum = this.maximum;
      }
      object cellValue = GetValue(rowIndex);
      if (cellValue != null)
      {
        Decimal currentValue = System.Convert.ToDecimal(cellValue);
        Decimal constrainedValue = Constrain(currentValue);
        if (constrainedValue != currentValue)
        {
          SetValue(rowIndex, constrainedValue);
        }
      }
      Debug.Assert(this.maximum == value);
      if (OwnsEditingNumericUpDown(rowIndex))
      {
        this.EditingNumericUpDown.Maximum = value;
      }
    }

    /// <summary>
    /// Utility function that sets a new value for the Minimum property of the cell. This function is used by
    /// the cell and column Minimum property. The column uses this method instead of the Minimum
    /// property for performance reasons. This way the column can invalidate the entire column at once instead of 
    /// invalidating each cell of the column individually. A row index needs to be provided as a parameter because
    /// this cell may be shared among multiple rows.
    /// </summary>
    /// <param name="rowIndex">The zero-based row index of the cell's location.</param>
    /// <param name="value">A <see cref="Decimal"/> value of the minimum to set.</param>
    internal void SetMinimum(int rowIndex, Decimal value)
    {
      this.minimum = value;
      if (this.minimum > this.maximum)
      {
        this.maximum = value;
      }
      object cellValue = GetValue(rowIndex);
      if (cellValue != null)
      {
        Decimal currentValue = System.Convert.ToDecimal(cellValue);
        Decimal constrainedValue = Constrain(currentValue);
        if (constrainedValue != currentValue)
        {
          SetValue(rowIndex, constrainedValue);
        }
      }
      Debug.Assert(this.minimum == value);
      if (OwnsEditingNumericUpDown(rowIndex))
      {
        this.EditingNumericUpDown.Minimum = value;
      }
    }

    /// <summary>
    /// Utility function that sets a new value for the ThousandsSeparator property of the cell. This function is used by
    /// the cell and column ThousandsSeparator property. The column uses this method instead of the ThousandsSeparator
    /// property for performance reasons. This way the column can invalidate the entire column at once instead of 
    /// invalidating each cell of the column individually. A row index needs to be provided as a parameter because
    /// this cell may be shared among multiple rows.
    /// </summary>
    /// <param name="rowIndex">The zero-based row index of the cell's location.</param>
    /// <param name="value">The <see cref="Boolean"/> value wheter to use a separator.</param>
    internal void SetThousandsSeparator(int rowIndex, bool value)
    {
      this.thousandsSeparator = value;
      if (OwnsEditingNumericUpDown(rowIndex))
      {
        this.EditingNumericUpDown.ThousandsSeparator = value;
      }
    }

    #endregion //METHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER

    /// <summary>
    /// Determines whether this cell, at the given row index, shows the grid's editing control or not.
    /// The row index needs to be provided as a parameter because this cell may be shared among multiple rows.
    /// </summary>
    /// <param name="rowIndex">The row index of the cell.</param>
    /// <returns><strong>True</strong>, if this cell, at the given row index, shows the grid's editing control.</returns>
    private bool OwnsEditingNumericUpDown(int rowIndex)
    {
      if (rowIndex == -1 || this.DataGridView == null)
      {
        return false;
      }
      DataGridViewNumericUpDownEditingControl numericUpDownEditingControl = this.DataGridView.EditingControl as DataGridViewNumericUpDownEditingControl;
      return numericUpDownEditingControl != null && rowIndex == ((IDataGridViewEditingControl)numericUpDownEditingControl).EditingControlRowIndex;
    }

    /// <summary>
    /// Little utility function called by the Paint function to see if a particular part needs to be painted. 
    /// </summary>
    /// <param name="paintParts">The whole <see cref="DataGridViewPaintParts"/> object in the paint control</param>
    /// <param name="paintPart">The teste <see cref="DataGridViewPaintParts"/> enumeration member.</param>
    /// <returns><strong>True</strong>, if the particular part needs to be painted.</returns>
    private static bool PartPainted(DataGridViewPaintParts paintParts, DataGridViewPaintParts paintPart)
    {
      return (paintParts & paintPart) != 0;
    }

    /// <summary>
    /// Little utility function used by both the cell and column types to translate
    /// a <see cref="DataGridViewContentAlignment"/> value into
    /// a <see cref="HorizontalAlignment"/> value.
    /// </summary>
    /// <param name="align">The <see cref="DataGridViewContentAlignment"/> object that should be converted</param>
    /// <returns>The <see cref="HorizontalAlignment"/> that represents the <see cref="DataGridViewContentAlignment"/> best.</returns>
    internal static HorizontalAlignment TranslateAlignment(DataGridViewContentAlignment align)
    {
      if ((align & anyRight) != 0)
      {
        return HorizontalAlignment.Right;
      }
      else if ((align & anyCenter) != 0)
      {
        return HorizontalAlignment.Center;
      }
      else
      {
        return HorizontalAlignment.Left;
      }
    }
    #endregion //HELPER

  }
}