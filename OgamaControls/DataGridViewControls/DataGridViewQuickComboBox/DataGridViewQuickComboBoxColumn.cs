using System;
using System.Text;
using System.Globalization;
using System.Windows.Forms;
using System.ComponentModel;

namespace OgamaControls
{
  /// <summary>
  /// Custom column type dedicated to the <see cref="DataGridViewQuickComboBoxCell"/> cell type.
  /// </summary>
  public class DataGridViewQuickComboBoxColumn : DataGridViewColumn
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
      get
      {
        return base.CellTemplate;
      }
      set
      {
        DataGridViewQuickComboBoxCell dataGridViewQuickComboBoxCell = value as DataGridViewQuickComboBoxCell;
        if (value != null && dataGridViewQuickComboBoxCell == null)
        {
          throw new InvalidCastException("Value provided for CellTemplate must be of type DataGridViewQuickComboBoxCell or derive from it.");
        }

        base.CellTemplate = value;
      }
    }

    /// <summary>
    /// Small utility function that returns the template cell as a <see cref="DataGridViewColorButtonCell"/>
    /// </summary>
    /// <value>A <see cref="DataGridViewColorButtonCell"/> template.</value>
    private DataGridViewQuickComboBoxCell DataGridViewQuickComboBoxCellTemplate
    {
      get
      {
        return (DataGridViewQuickComboBoxCell)this.CellTemplate;
      }
    }

    /// <summary>
    /// Replicates the <see cref="DataSource"/> property of the <see cref="DataGridViewQuickComboBoxCell"/> cell type.
    /// Gets or sets the datasource for all combo box cells in the column.
    /// </summary>
    /// <value>The <see cref="object"/> with the data source for the cells</value>
    //[Category("Data"), Description("Indicates the Data source of the combo box cells.")]
    [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public object DataSource
    {
      get
      {
        if (this.DataGridViewQuickComboBoxCellTemplate == null)
        {
          throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
        }
        return this.DataGridViewQuickComboBoxCellTemplate.DataSource;
      }
      set
      {
        if (this.DataGridViewQuickComboBoxCellTemplate == null)
        {
          throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
        }
        // Update the template cell so that subsequent cloned cells use the new value.
        this.DataGridViewQuickComboBoxCellTemplate.DataSource = value;
        if (this.DataGridView != null)
        {
          // Update all the existing DataGridViewColorButtonCell cells in the column accordingly.
          DataGridViewRowCollection dataGridViewRows = this.DataGridView.Rows;
          int rowCount = dataGridViewRows.Count;
          for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
          {
            // Be careful not to unshare rows unnecessarily. 
            // This could have severe performance repercussions.
            DataGridViewRow dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
            DataGridViewQuickComboBoxCell dataGridViewCell = dataGridViewRow.Cells[this.Index] as DataGridViewQuickComboBoxCell;
            if (dataGridViewCell != null)
            {
              dataGridViewCell.DataSource = value;
            }
          }

          this.DataGridView.InvalidateColumn(this.Index);
        }
      }
    }

    /// <summary>
    /// Replicates the <see cref="DisplayMember"/> property of the <see cref="DataGridViewQuickComboBoxCell"/> cell type.
    /// Gets or sets the DisplayMember for all combo box cells in the column.
    /// </summary>
    /// <value>The <see cref="object"/> with the DisplayMember for the cells</value>
    [Category("Data"), Description("Indicates the DisplayMember of the combo box cells.")]
    public string DisplayMember
    {
      get
      {
        if (this.DataGridViewQuickComboBoxCellTemplate == null)
        {
          throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
        }
        return this.DataGridViewQuickComboBoxCellTemplate.DisplayMember;
      }
      set
      {
        if (this.DataGridViewQuickComboBoxCellTemplate == null)
        {
          throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
        }
        // Update the template cell so that subsequent cloned cells use the new value.
        this.DataGridViewQuickComboBoxCellTemplate.DisplayMember = value;
        if (this.DataGridView != null)
        {
          // Update all the existing DataGridViewColorButtonCell cells in the column accordingly.
          DataGridViewRowCollection dataGridViewRows = this.DataGridView.Rows;
          int rowCount = dataGridViewRows.Count;
          for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
          {
            // Be careful not to unshare rows unnecessarily. 
            // This could have severe performance repercussions.
            DataGridViewRow dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
            DataGridViewQuickComboBoxCell dataGridViewCell = dataGridViewRow.Cells[this.Index] as DataGridViewQuickComboBoxCell;
            if (dataGridViewCell != null)
            {
              dataGridViewCell.DisplayMember = value;
            }
          }

          this.DataGridView.InvalidateColumn(this.Index);
        }
      }
    }

    /// <summary>
    /// Replicates the <see cref="ValueMember"/> property of the <see cref="DataGridViewQuickComboBoxCell"/> cell type.
    /// Gets or sets the ValueMember for all combo box cells in the column.
    /// </summary>
    /// <value>The <see cref="object"/> with the ValueMember for the cells</value>
    [Category("Data"), Description("Indicates the ValueMember of the combo box cells.")]
    public string ValueMember
    {
      get
      {
        if (this.DataGridViewQuickComboBoxCellTemplate == null)
        {
          throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
        }
        return this.DataGridViewQuickComboBoxCellTemplate.ValueMember;
      }
      set
      {
        if (this.DataGridViewQuickComboBoxCellTemplate == null)
        {
          throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
        }
        // Update the template cell so that subsequent cloned cells use the new value.
        this.DataGridViewQuickComboBoxCellTemplate.ValueMember = value;
        if (this.DataGridView != null)
        {
          // Update all the existing DataGridViewColorButtonCell cells in the column accordingly.
          DataGridViewRowCollection dataGridViewRows = this.DataGridView.Rows;
          int rowCount = dataGridViewRows.Count;
          for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
          {
            // Be careful not to unshare rows unnecessarily. 
            // This could have severe performance repercussions.
            DataGridViewRow dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
            DataGridViewQuickComboBoxCell dataGridViewCell = dataGridViewRow.Cells[this.Index] as DataGridViewQuickComboBoxCell;
            if (dataGridViewCell != null)
            {
              dataGridViewCell.ValueMember = value;
            }
          }

          this.DataGridView.InvalidateColumn(this.Index);
        }
      }
    }

    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Constructor for the DataGridViewQuickComboBoxColumn class.
    /// </summary>
    public DataGridViewQuickComboBoxColumn()
      : base(new DataGridViewQuickComboBoxCell())
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
    /// <returns>A <see cref="string"/> with the compact string representation of the column.</returns>
    public override string ToString()
    {
      StringBuilder sb = new StringBuilder(100);
      sb.Append("DataGridViewQuickComboBoxColumn { Name=");
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
    #endregion //METHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER
    #endregion //HELPER
  }
}