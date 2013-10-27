using System;
using System.Windows.Forms;

namespace OgamaControls
{
  /// <summary>
  /// Defines the editing control for the <see cref="DataGridViewColorButtonCell"/> custom cell type.
  /// </summary>
  class DataGridViewColorButtonEditingControl : ColorDropdown, IDataGridViewEditingControl
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
    public DataGridViewColorButtonEditingControl()
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
    /// Listen to the OnColorChanged notification to forward the change to the grid.
    /// </summary>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    public override void OnColorChanged(EventArgs e)
    {
      base.OnColorChanged(e);
      if (this.Focused)
      {
        // Let the DataGridView know about the value change
        NotifyDataGridViewOfValueChange();
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
      //Don´t set back and fore color and font, because no text is painted and back color should be 
      //the value color, not the datagridview back color
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
            // If the end of the selection is at the end of the string,
            // let the DataGridView treat the key message
            if ((this.RightToLeft == RightToLeft.No && !(this.SelectionLength == 0 && this.SelectionStart == this.Text.Length)) ||
                  (this.RightToLeft == RightToLeft.Yes && !(this.SelectionLength == 0 && this.SelectionStart == 0)))
            {
              return true;
            }
            break;
          }

        case Keys.Left:
          {
            // If the end of the selection is at the begining of the string
            // or if the entire text is selected and we did not start editing,
            // send this character to the dataGridView, else process the key message
            if ((this.RightToLeft == RightToLeft.No && !(this.SelectionLength == 0 && this.SelectionStart == 0)) ||
                (this.RightToLeft == RightToLeft.Yes && !(this.SelectionLength == 0 && this.SelectionStart == this.Text.Length)))
            {
              return true;
            }
            break;
          }

        case Keys.Down:
          break;

        case Keys.Up:
          break;

        case Keys.Home:
        case Keys.End:
          {
            // Let the grid handle the key if the entire text is selected.
            if (this.SelectionLength != this.Text.Length)
            {
              return true;
            }
            break;
          }

        case Keys.Delete:
          {
            // Let the grid handle the key if the carret is at the end of the text.
            if (this.SelectionLength > 0 ||
                this.SelectionStart < this.Text.Length)
            {
              return true;
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
      return this.CurrentColor.Name;
    }

    /// <summary>
    /// Called by the grid to give the editing control a chance to prepare itself for
    /// the editing session.
    /// </summary>
    /// <param name="selectAll"><strong>true</strong> to select all of the cell's 
    /// content; otherwise, <strong>false</strong>.</param>
    public virtual void PrepareEditingControlForEdit(bool selectAll)
    {
      //Nothing is needed here
    }

    #endregion //IDataGridViewEditingControlInterfaceImplementation

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

    #endregion //METHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER
    #endregion //HELPER
  }
}
