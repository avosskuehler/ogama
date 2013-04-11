using System;
using System.Drawing;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Forms;
using System.Collections.Generic;
using System.ComponentModel;
using VectorGraphics;
using System.Windows.Forms.VisualStyles;

namespace OgamaControls
{
  using VectorGraphics.Tools.CustomTypeConverter;

  /// <summary>
  /// Defines a <strong>ColorButton</strong> cell type for the <see cref="System.Windows.Forms.DataGridView"/> control
  /// </summary>
  public class DataGridViewColorButtonCell : DataGridViewTextBoxCell
  {
    ///////////////////////////////////////////////////////////////////////////////
    // Defining Constants                                                        //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTANTS
    /// <summary>
    /// Default value of the color property
    /// </summary>
    private static Color DataGridViewColorButton_defaultColor = Color.Transparent;

    /// <summary>
    /// Type of this cell's editing control
    /// </summary>
    private static Type defaultEditType = typeof(DataGridViewColorButtonEditingControl);

    /// <summary>
    /// Type of this cell's value. The formatted value type is string, 
    /// the same as the base class DataGridViewTextBoxCell
    /// </summary>
    private static Type defaultValueType = typeof(Color);


    #endregion //CONSTANTS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Variables, Enumerations, Events                                  //
    ///////////////////////////////////////////////////////////////////////////////
    #region FIELDS

    /// <summary>
    /// Caches the value of the <see cref="ButtonColor"/> property
    /// </summary>
    private Color _currentColor;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// The <strong>ButtonColor</strong> property replicates the one from the <strong>ColorButton</strong> control
    /// Gets or sets the buttons color.
    /// </summary>
    /// <value>The <see cref="Color"/> of the button.</value>
    public Color ButtonColor
    {
      get { return this._currentColor; }
      set
      {
        if (this._currentColor != value)
        {
          SetColor(this.RowIndex, value);
          // Assure that the cell or column gets repainted and autosized if needed
          OnCommonChange();
        }
      }
    }

    /// <summary>
    /// Returns the current <see cref="IDataGridViewEditingControl"/> as a <see cref="DataGridViewColorButtonEditingControl"/> control
    /// </summary>
    /// <value>A <see cref="DataGridViewColorButtonEditingControl"/> that is currently active.</value>
    private DataGridViewColorButtonEditingControl EditingColorButton
    {
      get
      {
        return this.DataGridView.EditingControl as DataGridViewColorButtonEditingControl;
      }
    }

    /// <summary>
    /// Overridden. Define the type of the cell's editing control
    /// </summary>
    /// <value>A <see cref="Type"/> representing the <see cref="DataGridViewColorButtonEditingControl"/> type.</value>
    public override Type EditType
    {
      get
      {
        return defaultEditType; // the type is DataGridViewColorButtonEditingControl
      }
    }

    /// <summary>
    /// Overridden. Gets the data type of the values in the cell. 
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
    /// Constructor for the <strong>DataGridViewColorButton</strong> cell type
    /// </summary>
    public DataGridViewColorButtonCell()
    {
      // Set the default values of the properties:
      this._currentColor = DataGridViewColorButton_defaultColor;

      DataGridViewCellStyle newStyle = new DataGridViewCellStyle();
      newStyle.Alignment = DataGridViewContentAlignment.TopLeft;
      this.Style = newStyle;
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
    /// <returns>An <see cref="Object"/> that represents the cloned <see cref="DataGridViewColorButtonCell"/>.</returns>
    public override object Clone()
    {
      DataGridViewColorButtonCell dataGridViewCell = base.Clone() as DataGridViewColorButtonCell;
      if (dataGridViewCell != null)
      {
        dataGridViewCell.ButtonColor = this.ButtonColor;
      }
      return dataGridViewCell;
    }

    /// <summary>
    /// Customized implementation of the GetErrorIconBounds function in order to draw the potential 
    /// error icon next to the down button and not on top of them.
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
    /// in order to include the buttons color in the formatted
    /// representation of the cell value.
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
        this.ButtonColor = ObjectStringConverter.StringToColor(formattedNumber);
      }
      return formattedValue;
    }

    /// <summary>
    /// Custom implementation of the GetPreferredSize function. 
    /// This implementation uses the preferred size of the base 
    /// <see cref="DataGridViewTextBoxCell"/> cell and adds room for the down button.
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
    /// Custom implementation of the <see cref="InitializeEditingControl"/> function. 
    /// This function is called by the <see cref="DataGridView"/> control 
    /// at the beginning of an editing session. It makes sure 
    /// that the properties of the <see cref="ColorButton"/> editing control are 
    /// set according to the cell properties.
    /// </summary>
    /// <param name="rowIndex">The zero-based row index of the cell.</param>
    /// <param name="initialFormattedValue">An <see cref="Object"/> that represents the value displayed by the cell when editing is started.</param>
    /// <param name="dataGridViewCellStyle">A <see cref="DataGridViewCellStyle"/> that represents the style of the cell.</param>
    public override void InitializeEditingControl(int rowIndex, object initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
    {
      base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);
      ColorDropdown colorDropdown = this.DataGridView.EditingControl as ColorDropdown;
      if (colorDropdown != null)
      {
        colorDropdown.CurrentColor = this._currentColor;
      }
    }

    /// <summary>
    /// Custom paints the cell. The base implementation of the DataGridViewTextBoxCell type is called first,
    /// dropping the icon error and content foreground parts. Those two parts are painted by this custom implementation.
    /// In this sample, the non-edited ColorButton control is painted by hand.
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
          // Paint a ColorButton control
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


          if (valBounds.Width > 0 && valBounds.Height > 0)
          {
            //Fix the height to 21 pixels because I hate ugly big buttons,
            //if row height is huge.
            if (valBounds.Height > 21)
            {
              valBounds.Height = 21;
            }
            //Draw the colored rectangle
            Rectangle colorRect = valBounds;
            colorRect.Width = colorRect.Width - 1;
            colorRect.Height = colorRect.Height - 1;
            colorRect.Inflate(-1, -1);
            colorRect.Height = colorRect.Height - 1;
            colorRect.Width = colorRect.Width - 18;
            colorRect.Offset(1, 1);
            graphics.FillRectangle(new SolidBrush(this._currentColor), colorRect);

            //Draw the dropdown combo box surrounding
            if (!ComboBoxRenderer.IsSupported)
            {
              Pen textPen = new Pen(SystemColors.ControlText);
              Point pt = new Point(valBounds.Right, colorRect.Y);
              graphics.DrawLine(textPen, pt.X - 14, pt.Y + 5, pt.X - 10, pt.Y + 5);
              graphics.DrawLine(textPen, pt.X - 13, pt.Y + 6, pt.X - 11, pt.Y + 6);
              graphics.DrawLine(textPen, pt.X - 11, pt.Y + 5, pt.X - 12, pt.Y + 7);
            }
            else
            {
              Rectangle arrowBounds = new Rectangle(valBounds.Right - 18, valBounds.Top + 1, 17, valBounds.Height - 2);
              ComboBoxRenderer.DrawDropDownButton(graphics, arrowBounds, ComboBoxState.Normal);
            }
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
    /// Custom implementation of the <see cref="PositionEditingControl"/> 
    /// method called by the <see cref="DataGridView"/> control when it
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
      return "DataGridViewColorButtonCell { ColumnIndex=" + ColumnIndex.ToString(CultureInfo.CurrentCulture) + ", RowIndex=" + RowIndex.ToString(CultureInfo.CurrentCulture) + " }";
    }

    #endregion //OVERRIDES

    ///////////////////////////////////////////////////////////////////////////////
    // Methods for doing main class job                                          //
    ///////////////////////////////////////////////////////////////////////////////
    #region METHODS

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
      //editingControlBounds.Offset(-1, -1);
      //editingControlBounds.Inflate(1, 0);
      //editingControlBounds.Y -= 1;
      //editingControlBounds.Height = Math.Max(0, editingControlBounds.Height + 2);

      // Adjust the vertical location of the editing control:
      int preferredHeight = cellStyle.Font.Height + 10;
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
    /// Utility function that sets a new value for the Color property of the cell. 
    /// Row index needs to be provided as a parameter because
    /// this cell may be shared among multiple rows.
    /// </summary>
    /// <param name="rowIndex">The row index of the cell.</param>
    /// <param name="value">color to set</param>
    internal void SetColor(int rowIndex, Color value)
    {
      this._currentColor = value;
      if (OwnsEditingColorButton(rowIndex))
      {
        this.EditingColorButton.CurrentColor = value;
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
    private bool OwnsEditingColorButton(int rowIndex)
    {
      if (rowIndex == -1 || this.DataGridView == null)
      {
        return false;
      }
      DataGridViewColorButtonEditingControl colorButtonEditingControl = this.DataGridView.EditingControl as DataGridViewColorButtonEditingControl;
      return colorButtonEditingControl != null && rowIndex == ((IDataGridViewEditingControl)colorButtonEditingControl).EditingControlRowIndex;
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

    #endregion //HELPER












    

  }
}
