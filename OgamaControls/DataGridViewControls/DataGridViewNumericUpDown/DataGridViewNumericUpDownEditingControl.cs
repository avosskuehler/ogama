using System;
using System.Drawing;
using System.Windows.Forms;

namespace OgamaControls
{
  /// <summary>
  /// Defines the editing control for the <see cref="DataGridViewNumericUpDownCell"/> custom cell type.
  /// </summary>
  class DataGridViewNumericUpDownEditingControl : NumericUpDown, IDataGridViewEditingControl
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
    /// The grid that owns this editing control
    /// </summary>
    private DataGridView dataGridView;

    /// <summary>
    /// Stores whether the editing control's value has changed or not
    /// </summary>
    private bool valueChanged;

    /// <summary>
    /// Stores the row index in which the editing control resides
    /// </summary>
    private int rowIndex;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets or sets the grid that uses this editing control
    /// </summary>
    /// <value>The <see cref="DataGridView"/> that owns this Editing Control</value>
    public virtual DataGridView EditingControlDataGridView
    {
      get
      {
        return this.dataGridView;
      }
      set
      {
        this.dataGridView = value;
      }
    }

    /// <summary>
    /// Gets or set the current formatted value of the editing control
    /// </summary>
    /// <value>The <see cref="Object"/> that represents the current formatted value of the editing control.</value>
    public virtual object EditingControlFormattedValue
    {
      get
      {
        return GetEditingControlFormattedValue(DataGridViewDataErrorContexts.Formatting);
      }
      set
      {
        this.Text = (string)value;
      }
    }

    /// <summary>
    /// Gets or sets the the row in which the editing control resides
    /// </summary>
    /// <value>The <see cref="int"/> that is the row index in which the editing control resides</value>
    public virtual int EditingControlRowIndex
    {
      get
      {
        return this.rowIndex;
      }
      set
      {
        this.rowIndex = value;
      }
    }

    /// <summary>
    /// Gets or sets the value whether the value of the editing control has changed or not
    /// </summary>
    /// <value>The <see cref="Boolean"/> value whether the value of the editing control has changed or not.</value>
    public virtual bool EditingControlValueChanged
    {
      get
      {
        return this.valueChanged;
      }
      set
      {
        this.valueChanged = value;
      }
    }

    /// <summary>
    /// Gets the cursor that must be used for the editing panel,
    /// i.e. the parent of the editing control.
    /// </summary>
    /// <value>The <see cref="Cursor"/> which must be used for the editing panel.</value>
    public virtual Cursor EditingPanelCursor
    {
      get
      {
        return Cursors.Default;
      }
    }

    /// <summary>
    /// Gets the property which indicates whether the editing control needs to be repositioned 
    /// when its value changes. Returns always false.
    /// </summary>
    /// <value>The <see cref="Boolean"/> value whether the editing control needs to be repositioned.</value>
    public virtual bool RepositionEditingControlOnValueChange
    {
      get
      {
        return false;
      }
    }

    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Constructor of the editing control class
    /// </summary>
    public DataGridViewNumericUpDownEditingControl()
    {
      // The editing control must not be part of the tabbing loop
      this.TabStop = false;
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
    /// Listen to the KeyPress notification to know when the value changed, and 
    /// notify the grid of the change.
    /// </summary>
    /// <param name="e">A <see cref="KeyPressEventArgs"/> that contains the event data.</param>
    protected override void OnKeyPress(KeyPressEventArgs e)
    {
      base.OnKeyPress(e);

      // The value changes when a digit, the decimal separator, the group separator or
      // the negative sign is pressed.
      bool notifyValueChange = false;
      if (char.IsDigit(e.KeyChar))
      {
        notifyValueChange = true;
      }
      else
      {
        System.Globalization.NumberFormatInfo numberFormatInfo = System.Globalization.CultureInfo.CurrentCulture.NumberFormat;
        string decimalSeparatorStr = numberFormatInfo.NumberDecimalSeparator;
        string groupSeparatorStr = numberFormatInfo.NumberGroupSeparator;
        string negativeSignStr = numberFormatInfo.NegativeSign;
        if (!string.IsNullOrEmpty(decimalSeparatorStr) && decimalSeparatorStr.Length == 1)
        {
          notifyValueChange = decimalSeparatorStr[0] == e.KeyChar;
        }
        if (!notifyValueChange && !string.IsNullOrEmpty(groupSeparatorStr) && groupSeparatorStr.Length == 1)
        {
          notifyValueChange = groupSeparatorStr[0] == e.KeyChar;
        }
        if (!notifyValueChange && !string.IsNullOrEmpty(negativeSignStr) && negativeSignStr.Length == 1)
        {
          notifyValueChange = negativeSignStr[0] == e.KeyChar;
        }
      }

      if (notifyValueChange)
      {
        // Let the DataGridView know about the value change
        NotifyDataGridViewOfValueChange();
      }
    }

    /// <summary>
    /// Listen to the OnValueChanged notification to forward the change to the grid.
    /// </summary>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    protected override void OnValueChanged(EventArgs e)
    {
      //// Notify the DataGridView that the contents of the cell
      //// have changed.
      //valueChanged = true;
      //this.EditingControlDataGridView.NotifyCurrentCellDirty(true);
      //base.OnValueChanged(eventargs);

      base.OnValueChanged(e);
      if (this.Focused)
      {
        // Let the DataGridView know about the value change
        NotifyDataGridViewOfValueChange();
      }
    }

    /// <summary>
    /// A few keyboard messages need to be forwarded to the inner textbox of the
    /// NumericUpDown control so that the first character pressed appears in it.
    /// </summary>
    /// <param name="m">A <see cref="Message"/> indicating the key that was pressed.</param>
    /// <returns><strong>true</strong> if the key event was handled by the editing control; 
    /// otherwise, <strong>false</strong>. </returns>
    protected override bool ProcessKeyEventArgs(ref Message m)
    {
      TextBox textBox = this.Controls[1] as TextBox;
      if (textBox != null)
      {
        User32.SendMessage(textBox.Handle, m.Msg, m.WParam, m.LParam);
        return true;
      }
      else
      {
        return base.ProcessKeyEventArgs(ref m);
      }
    }

    #endregion //OVERRIDES

    ///////////////////////////////////////////////////////////////////////////////
    // Methods for doing main class job                                          //
    ///////////////////////////////////////////////////////////////////////////////
    #region METHODS

    #region IDataGridViewEditingControlInterfaceImplementation

    /// <summary>
    /// Method called by the grid before the editing control is shown so it can adapt to the 
    /// provided cell style.
    /// Changes the control's user interface (UI) to be consistent with the specified cell style.
    /// </summary>
    /// <param name="dataGridViewCellStyle">The <see cref="DataGridViewCellStyle"/> to use 
    /// as the model for the UI.</param>
    public virtual void ApplyCellStyleToEditingControl(DataGridViewCellStyle dataGridViewCellStyle)
    {
      this.Font = dataGridViewCellStyle.Font;
      if (dataGridViewCellStyle.BackColor.A < 255)
      {
        // The NumericUpDown control does not support transparent back colors
        Color opaqueBackColor = Color.FromArgb(255, dataGridViewCellStyle.BackColor);
        this.BackColor = opaqueBackColor;
        this.dataGridView.EditingPanel.BackColor = opaqueBackColor;
      }
      else
      {
        this.BackColor = dataGridViewCellStyle.BackColor;
      }
      this.ForeColor = dataGridViewCellStyle.ForeColor;
      this.TextAlign = DataGridViewNumericUpDownCell.TranslateAlignment(dataGridViewCellStyle.Alignment);
    }

    /// <summary>Determines whether the specified key is a regular 
    /// input key that the editing control should process or a special 
    /// key that the <see cref="DataGridView"/> should process.
    /// </summary>
    /// <param name="keyData">A <see cref="Keys"/> that represents the key that was pressed.</param>
    /// <param name="dataGridViewWantsInputKey"><strong>true</strong> when the <see cref="DataGridView"/> 
    /// wants to process the <see cref="Keys"/> in keyData; otherwise, <strong>false</strong>.</param>
    /// <returns><strong>true</strong> if the specified key is a regular input key that should be 
    /// handled by the editing control; otherwise, <strong>false</strong>.</returns>
    public virtual bool EditingControlWantsInputKey(Keys keyData, bool dataGridViewWantsInputKey)
    {
      switch (keyData & Keys.KeyCode)
      {
        case Keys.Right:
          {
            TextBox textBox = this.Controls[1] as TextBox;
            if (textBox != null)
            {
              // If the end of the selection is at the end of the string,
              // let the DataGridView treat the key message
              if ((this.RightToLeft == RightToLeft.No && !(textBox.SelectionLength == 0 && textBox.SelectionStart == textBox.Text.Length)) ||
                  (this.RightToLeft == RightToLeft.Yes && !(textBox.SelectionLength == 0 && textBox.SelectionStart == 0)))
              {
                return true;
              }
            }
            break;
          }

        case Keys.Left:
          {
            TextBox textBox = this.Controls[1] as TextBox;
            if (textBox != null)
            {
              // If the end of the selection is at the begining of the string
              // or if the entire text is selected and we did not start editing,
              // send this character to the dataGridView, else process the key message
              if ((this.RightToLeft == RightToLeft.No && !(textBox.SelectionLength == 0 && textBox.SelectionStart == 0)) ||
                  (this.RightToLeft == RightToLeft.Yes && !(textBox.SelectionLength == 0 && textBox.SelectionStart == textBox.Text.Length)))
              {
                return true;
              }
            }
            break;
          }

        case Keys.Down:
          // If the current value hasn't reached its minimum yet, handle the key. Otherwise let
          // the grid handle it.
          if (this.Value > this.Minimum)
          {
            return true;
          }
          break;

        case Keys.Up:
          // If the current value hasn't reached its maximum yet, handle the key. Otherwise let
          // the grid handle it.
          if (this.Value < this.Maximum)
          {
            return true;
          }
          break;

        case Keys.Home:
        case Keys.End:
          {
            // Let the grid handle the key if the entire text is selected.
            TextBox textBox = this.Controls[1] as TextBox;
            if (textBox != null)
            {
              if (textBox.SelectionLength != textBox.Text.Length)
              {
                return true;
              }
            }
            break;
          }

        case Keys.Delete:
          {
            // Let the grid handle the key if the carret is at the end of the text.
            TextBox textBox = this.Controls[1] as TextBox;
            if (textBox != null)
            {
              if (textBox.SelectionLength > 0 ||
                  textBox.SelectionStart < textBox.Text.Length)
              {
                return true;
              }
            }
            break;
          }
      }
      return !dataGridViewWantsInputKey;
    }

    /// <summary>
    /// Retrieves the formatted value of the cell. 
    /// </summary>
    /// <param name="context">A bitwise combination of <see cref="DataGridViewDataErrorContexts"/> values 
    /// that specifies the context in which the data is needed.</param>
    /// <returns>An <see cref="Object"/> that represents the formatted version of the cell contents. </returns>
    public virtual object GetEditingControlFormattedValue(DataGridViewDataErrorContexts context)
    {
      bool userEdit = this.UserEdit;
      try
      {
        // Prevent the Value from being set to Maximum or Minimum when the cell is being painted.
        this.UserEdit = (context & DataGridViewDataErrorContexts.Display) == 0;
        return this.Value.ToString((this.ThousandsSeparator ? "N" : "F") + this.DecimalPlaces.ToString());
      }
      finally
      {
        this.UserEdit = userEdit;
      }
    }

    /// <summary>
    /// Called by the grid to give the editing control a chance to prepare itself for
    /// the editing session.
    /// </summary>
    /// <param name="selectAll"><strong>true</strong> to select all of the cell's 
    /// content; otherwise, <strong>false</strong>.</param>
    public virtual void PrepareEditingControlForEdit(bool selectAll)
    {
      TextBox textBox = this.Controls[1] as TextBox;
      if (textBox != null)
      {
        if (selectAll)
        {
          textBox.SelectAll();
        }
        else
        {
          // Do not select all the text, but
          // position the caret at the end of the text
          textBox.SelectionStart = textBox.Text.Length;
        }
      }
    }

    #endregion //IDataGridViewEditingControlInterfaceImplementation


    #endregion //METHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER

    /// <summary>
    /// Small utility function that updates the local dirty state and 
    /// notifies the grid of the value change.
    /// </summary>
    private void NotifyDataGridViewOfValueChange()
    {
      if (!this.valueChanged)
      {
        this.valueChanged = true;
        this.dataGridView.NotifyCurrentCellDirty(true);
      }
    }
    #endregion //HELPER


  }
}
