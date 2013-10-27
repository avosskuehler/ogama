using System;
using System.Text;
using System.Globalization;
using System.Windows.Forms;
using System.ComponentModel;

namespace OgamaControls
{
  /// <summary>
  /// Custom column type dedicated to the <see cref="DataGridViewNumericUpDownCell"/> cell type.
  /// </summary>
  public class DataGridViewNumericUpDownColumn : DataGridViewColumn
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
    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Represents the implicit cell that gets cloned when adding rows to the grid.
    /// Gets or sets the template used to create new cells. 
    /// </summary>
    /// <value>A <see cref="DataGridViewCell"/> that all other cells in the column are modeled after. 
    /// The default is a null reference (Nothing in Visual Basic). </value>
    [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override DataGridViewCell CellTemplate
    {
      get { return base.CellTemplate; }
      set
      {
        DataGridViewNumericUpDownCell dataGridViewNumericUpDownCell = value as DataGridViewNumericUpDownCell;
        if (value != null && dataGridViewNumericUpDownCell == null)
        {
          throw new InvalidCastException("Value provided for CellTemplate must be of type DataGridViewNumericUpDownElements.DataGridViewNumericUpDownCell or derive from it.");
        }
        base.CellTemplate = value;
      }
    }

    /// <summary>
    /// The Default value of the cells.
    /// </summary>
    /// <value>The <see cref="int"/> which should be set as default value.</value>
    [Category("Data"),
    DefaultValue(DataGridViewNumericUpDownCell.DATAGRIDVIEWNUMERICUPDOWNCELL_defaultValue),
    Description("Indicates the default value to display.")]
    public int DefaultValue
    {
      get
      {
        if (this.NumericUpDownCellTemplate == null)
        {
          throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
        }
        return this.NumericUpDownCellTemplate.DefaultValue;
      }
      set
      {
        if (this.NumericUpDownCellTemplate == null)
        {
          throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
        }
        // Update the template cell so that subsequent cloned cells use the new value.
        this.NumericUpDownCellTemplate.DefaultValue = value;
        if (this.DataGridView != null)
        {
          // Update all the existing DataGridViewNumericUpDownCell cells in the column accordingly.
          DataGridViewRowCollection dataGridViewRows = this.DataGridView.Rows;
          int rowCount = dataGridViewRows.Count;
          for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
          {
            // Be careful not to unshare rows unnecessarily. 
            // This could have severe performance repercussions.
            DataGridViewRow dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
            DataGridViewNumericUpDownCell dataGridViewCell = dataGridViewRow.Cells[this.Index] as DataGridViewNumericUpDownCell;
            if (dataGridViewCell != null)
            {
              // Call the internal SetDecimalPlaces method instead of the property to avoid invalidation 
              // of each cell. The whole column is invalidated later in a single operation for better performance.
              dataGridViewCell.SetDefaultValue(rowIndex, value);
            }
          }
          this.DataGridView.InvalidateColumn(this.Index);
          // TODO: Call the grid's autosizing methods to autosize the column, rows, column headers / row headers as needed.
        }
      }
    }

    /// <summary>
    /// Replicates the DecimalPlaces property of the DataGridViewNumericUpDownCell cell type.
    /// </summary>
    /// <value>A <see cref="int"/> with value for the number of decimal places.</value>
    [Category("Data"),Description("Indicates the number of decimal places to show.")]
    public int DecimalPlaces
    {
      get
      {
        if (this.NumericUpDownCellTemplate == null)
        {
          throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
        }
        return this.NumericUpDownCellTemplate.DecimalPlaces;
      }
      set
      {
        if (this.NumericUpDownCellTemplate == null)
        {
          throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
        }
        // Update the template cell so that subsequent cloned cells use the new value.
        this.NumericUpDownCellTemplate.DecimalPlaces = value;
        if (this.DataGridView != null)
        {
          // Update all the existing DataGridViewNumericUpDownCell cells in the column accordingly.
          DataGridViewRowCollection dataGridViewRows = this.DataGridView.Rows;
          int rowCount = dataGridViewRows.Count;
          for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
          {
            // Be careful not to unshare rows unnecessarily. 
            // This could have severe performance repercussions.
            DataGridViewRow dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
            DataGridViewNumericUpDownCell dataGridViewCell = dataGridViewRow.Cells[this.Index] as DataGridViewNumericUpDownCell;
            if (dataGridViewCell != null)
            {
              // Call the internal SetDecimalPlaces method instead of the property to avoid invalidation 
              // of each cell. The whole column is invalidated later in a single operation for better performance.
              dataGridViewCell.SetDecimalPlaces(rowIndex, value);
            }
          }
          this.DataGridView.InvalidateColumn(this.Index);
          // TODO: Call the grid's autosizing methods to autosize the column, rows, column headers / row headers as needed.
        }
      }
    }

    /// <summary>
    /// Replicates the Increment property of the DataGridViewNumericUpDownCell cell type.
    /// </summary>
    /// <value>A <see cref="Decimal"/> value for the amount to increment or decrement on each button click.</value>
    [Category("Data"),Description("Indicates the amount to increment or decrement on each button click.")]
    public Decimal Increment
    {
      get
      {
        if (this.NumericUpDownCellTemplate == null)
        {
          throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
        }
        return this.NumericUpDownCellTemplate.Increment;
      }
      set
      {
        if (this.NumericUpDownCellTemplate == null)
        {
          throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
        }
        this.NumericUpDownCellTemplate.Increment = value;
        if (this.DataGridView != null)
        {
          DataGridViewRowCollection dataGridViewRows = this.DataGridView.Rows;
          int rowCount = dataGridViewRows.Count;
          for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
          {
            DataGridViewRow dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
            DataGridViewNumericUpDownCell dataGridViewCell = dataGridViewRow.Cells[this.Index] as DataGridViewNumericUpDownCell;
            if (dataGridViewCell != null)
            {
              dataGridViewCell.SetIncrement(rowIndex, value);
            }
          }
        }
      }
    }

    /// <summary>
    /// Replicates the Maximum property of the DataGridViewNumericUpDownCell cell type.
    /// </summary>
    /// <value>A <see cref="Decimal"/> value that indicates the maximum value 
    /// for the numeric up-down cells.</value>
    [Category("Data"),Description("Indicates the maximum value for the numeric up-down cells."),RefreshProperties(RefreshProperties.All)]
    public Decimal Maximum
    {
      get
      {
        if (this.NumericUpDownCellTemplate == null)
        {
          throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
        }
        return this.NumericUpDownCellTemplate.Maximum;
      }
      set
      {
        if (this.NumericUpDownCellTemplate == null)
        {
          throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
        }
        this.NumericUpDownCellTemplate.Maximum = value;
        if (this.DataGridView != null)
        {
          DataGridViewRowCollection dataGridViewRows = this.DataGridView.Rows;
          int rowCount = dataGridViewRows.Count;
          for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
          {
            DataGridViewRow dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
            DataGridViewNumericUpDownCell dataGridViewCell = dataGridViewRow.Cells[this.Index] as DataGridViewNumericUpDownCell;
            if (dataGridViewCell != null)
            {
              dataGridViewCell.SetMaximum(rowIndex, value);
            }
          }
          this.DataGridView.InvalidateColumn(this.Index);
          // TODO: This column and/or grid rows may need to be autosized depending on their
          //       autosize settings. Call the autosizing methods to autosize the column, rows, 
          //       column headers / row headers as needed.
        }
      }
    }


    /// <summary>
    /// Replicates the Minimum property of the DataGridViewNumericUpDownCell cell type.
    /// </summary>
    /// <value>A <see cref="Decimal"/> value that indicates the minimum value 
    /// for the numeric up-down cells</value>
    [Category("Data"),Description("Indicates the minimum value for the numeric up-down cells."),RefreshProperties(RefreshProperties.All)]
    public Decimal Minimum
    {
      get
      {
        if (this.NumericUpDownCellTemplate == null)
        {
          throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
        }
        return this.NumericUpDownCellTemplate.Minimum;
      }
      set
      {
        if (this.NumericUpDownCellTemplate == null)
        {
          throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
        }
        this.NumericUpDownCellTemplate.Minimum = value;
        if (this.DataGridView != null)
        {
          DataGridViewRowCollection dataGridViewRows = this.DataGridView.Rows;
          int rowCount = dataGridViewRows.Count;
          for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
          {
            DataGridViewRow dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
            DataGridViewNumericUpDownCell dataGridViewCell = dataGridViewRow.Cells[this.Index] as DataGridViewNumericUpDownCell;
            if (dataGridViewCell != null)
            {
              dataGridViewCell.SetMinimum(rowIndex, value);
            }
          }
          this.DataGridView.InvalidateColumn(this.Index);
          // TODO: This column and/or grid rows may need to be autosized depending on their
          //       autosize settings. Call the autosizing methods to autosize the column, rows, 
          //       column headers / row headers as needed.
        }
      }
    }


    /// <summary>
    /// Replicates the ThousandsSeparator property of the DataGridViewNumericUpDownCell cell type.
    /// </summary>
    /// <value>A <see cref="Boolean"/> that indicates whether the thousands
    /// separator will be inserted between every three decimal digits.</value>
    [Category("Data"), DefaultValue(DataGridViewNumericUpDownCell.DATAGRIDVIEWNUMERICUPDOWNCELL_defaultThousandsSeparator),
    Description("Indicates whether the thousands separator will be inserted between every three decimal digits.")]
    public bool ThousandsSeparator
    {
      get
      {
        if (this.NumericUpDownCellTemplate == null)
        {
          throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
        }
        return this.NumericUpDownCellTemplate.ThousandsSeparator;
      }
      set
      {
        if (this.NumericUpDownCellTemplate == null)
        {
          throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
        }
        this.NumericUpDownCellTemplate.ThousandsSeparator = value;
        if (this.DataGridView != null)
        {
          DataGridViewRowCollection dataGridViewRows = this.DataGridView.Rows;
          int rowCount = dataGridViewRows.Count;
          for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
          {
            DataGridViewRow dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
            DataGridViewNumericUpDownCell dataGridViewCell = dataGridViewRow.Cells[this.Index] as DataGridViewNumericUpDownCell;
            if (dataGridViewCell != null)
            {
              dataGridViewCell.SetThousandsSeparator(rowIndex, value);
            }
          }
          this.DataGridView.InvalidateColumn(this.Index);
          // TODO: This column and/or grid rows may need to be autosized depending on their
          //       autosize settings. Call the autosizing methods to autosize the column, rows, 
          //       column headers / row headers as needed.
        }
      }
    }

    /// <summary>
    /// Small utility function that returns the template cell as 
    /// a <see cref="DataGridViewNumericUpDownCell"/>
    /// </summary>
    /// <value>A <see cref="DataGridViewNumericUpDownCell"/> template.</value>
    private DataGridViewNumericUpDownCell NumericUpDownCellTemplate
    {
      get
      {
        return (DataGridViewNumericUpDownCell)this.CellTemplate;
      }
    }

    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Constructor for the DataGridViewNumericUpDownColumn class.
    /// </summary>
    public DataGridViewNumericUpDownColumn()
      : base(new DataGridViewNumericUpDownCell())
    {
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
    /// Returns a standard compact string representation of the column.
    /// </summary>
    /// <returns>A <see cref="string"/> with the compact string representation 
    /// of the column.</returns>
    public override string ToString()
    {
      StringBuilder sb = new StringBuilder(100);
      sb.Append("DataGridViewNumericUpDownColumn { Name=");
      sb.Append(this.Name);
      sb.Append(", Index=");
      sb.Append(this.Index.ToString(CultureInfo.CurrentCulture));
      sb.Append(" }");
      return sb.ToString();
    }

    #endregion //OVERRIDES

    ///////////////////////////////////////////////////////////////////////////////
    // Methods for doing main class job                                          //
    ///////////////////////////////////////////////////////////////////////////////
    #region METHODS

    /// <summary>
    /// Indicates whether the Increment property should be persisted.
    /// </summary>
    /// <returns><strong>True</strong>, if it should be persisted.</returns>
    private bool ShouldSerializeIncrement()
    {
      return !this.Increment.Equals(DataGridViewNumericUpDownCell.DATAGRIDVIEWNUMERICUPDOWNCELL_defaultIncrement);
    }

    /// <summary>
    /// Indicates whether the Minimum property should be persisted.
    /// </summary>
    /// <returns><strong>True</strong>, if it should be persisted.</returns>
    private bool ShouldSerializeMinimum()
    {
      return !this.Minimum.Equals(DataGridViewNumericUpDownCell.DATAGRIDVIEWNUMERICUPDOWNCELL_defaultMinimum);
    }

    /// <summary>
    /// Indicates whether the Maximum property should be persisted.
    /// </summary>
    /// <returns><strong>True</strong>, if it should be persisted.</returns>
    private bool ShouldSerializeMaximum()
    {
      return !this.Maximum.Equals(DataGridViewNumericUpDownCell.DATAGRIDVIEWNUMERICUPDOWNCELL_defaultMaximum);
    }

    #endregion //METHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER
    #endregion //HELPER
  }
}