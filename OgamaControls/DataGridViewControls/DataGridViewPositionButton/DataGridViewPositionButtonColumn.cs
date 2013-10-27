using System;
using System.Text;
using System.Globalization;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;

namespace OgamaControls
{
  /// <summary>
  /// Custom column type dedicated to the <see cref="DataGridViewNumericUpDownCell"/> cell type.
  /// </summary>
  public class DataGridViewPositionButtonColumn : DataGridViewColumn
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
        DataGridViewPositionButtonCell dataGridViewPositionButtonCell = value as DataGridViewPositionButtonCell;
        if (value != null && dataGridViewPositionButtonCell == null)
        {
          throw new InvalidCastException("Value provided for CellTemplate must be of type DataGridViewPositionButtonCell or derive from it.");
        }
        base.CellTemplate = value;
      }
    }

    /// <summary>
    /// Replicates the <see cref="NewPosition"/> property of the <see cref="DataGridViewPositionButtonCell"/> cell type.
    /// Gets or sets the position value for all position button cells in the column.
    /// </summary>
    /// <value>The <see cref="Point"/> with the location value of the button cell</value>
    [Category("Appearance"), Description("Indicates the location value of the button to display.")]
    public Point NewPosition
    {
      get
      {
        if (this.PositionButtonCellTemplate == null)
        {
          throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
        }
        return this.PositionButtonCellTemplate.NewPosition;
      }
      set
      {
        if (this.PositionButtonCellTemplate == null)
        {
          throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
        }
        // Update the template cell so that subsequent cloned cells use the new value.
        this.PositionButtonCellTemplate.NewPosition = value;
        if (this.DataGridView != null)
        {
          // Update all the existing DataGridViewPositionButtonCell cells in the column accordingly.
          DataGridViewRowCollection dataGridViewRows = this.DataGridView.Rows;
          int rowCount = dataGridViewRows.Count;
          for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
          {
            // Be careful not to unshare rows unnecessarily. 
            // This could have severe performance repercussions.
            DataGridViewRow dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
            DataGridViewPositionButtonCell dataGridViewCell = dataGridViewRow.Cells[this.Index] as DataGridViewPositionButtonCell;
            if (dataGridViewCell != null)
            {
              // Call the internal SetDecimalPlaces method instead of the property to avoid invalidation 
              // of each cell. The whole column is invalidated later in a single operation for better performance.
              dataGridViewCell.SetPosition(rowIndex, value);
            }
          }
          this.DataGridView.InvalidateColumn(this.Index);
          // TODO: Call the grid's autosizing methods to autosize the column, rows, column headers / row headers as needed.
        }
      }
    }

    /// <summary>
    /// The stimulus screen size of the cells for correct sizing of the position control.
    /// </summary>
    /// <value>The <see cref="Size"/> which should be set as default value.</value>
    [Category("Data"),
    Description("Indicates the default value for the stimulus screen size.")]
    public Size StimulusScreenSize
    {
      get
      {
        if (this.PositionButtonCellTemplate == null)
        {
          throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
        }
        return this.PositionButtonCellTemplate.StimulusScreenSize;
      }
      set
      {
        if (this.PositionButtonCellTemplate == null)
        {
          throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
        }
        // Update the template cell so that subsequent cloned cells use the new value.
        this.PositionButtonCellTemplate.StimulusScreenSize = value;
        if (this.DataGridView != null)
        {
          // Update all the existing DataGridViewPositionButtonCell cells in the column accordingly.
          DataGridViewRowCollection dataGridViewRows = this.DataGridView.Rows;
          int rowCount = dataGridViewRows.Count;
          for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
          {
            // Be careful not to unshare rows unnecessarily. 
            // This could have severe performance repercussions.
            DataGridViewRow dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
            DataGridViewPositionButtonCell dataGridViewCell = dataGridViewRow.Cells[this.Index] as DataGridViewPositionButtonCell;
            if (dataGridViewCell != null)
            {
              // Call the internal SetDecimalPlaces method instead of the property to avoid invalidation 
              // of each cell. The whole column is invalidated later in a single operation for better performance.
              dataGridViewCell.SetStimulusScreenSize(rowIndex, value);
            }
          }
          this.DataGridView.InvalidateColumn(this.Index);
          // TODO: Call the grid's autosizing methods to autosize the column, rows, column headers / row headers as needed.
        }
      }
    }


    /// <summary>
    /// Small utility function that returns the template cell as a <see cref="DataGridViewPositionButtonCell"/>
    /// </summary>
    /// <value>A <see cref="DataGridViewPositionButtonCell"/> template.</value>
    private DataGridViewPositionButtonCell PositionButtonCellTemplate
    {
      get
      {
        return (DataGridViewPositionButtonCell)this.CellTemplate;
      }
    }

    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Constructor for the DataGridViewPositionButtonColumn class.
    /// </summary>
    public DataGridViewPositionButtonColumn()
      : base(new DataGridViewPositionButtonCell())
    {
      this.MinimumWidth = 100;
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
    /// <returns>A <see cref="string"/> with the compact string 
    /// representation of the column.</returns>
    public override string ToString()
    {
      StringBuilder sb = new StringBuilder(100);
      sb.Append("DataGridViewPositionButtonColumn { Name=");
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