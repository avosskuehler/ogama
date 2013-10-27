using System;
using System.Text;
using System.Globalization;
using System.Windows.Forms;
using System.ComponentModel;

namespace OgamaControls
{
  /// <summary>
  /// Custom column type dedicated to the <see cref="DataGridViewFontButtonCell"/> cell type.
  /// </summary>
  public class DataGridViewFontButtonColumn : DataGridViewColumn
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
        DataGridViewFontButtonCell dataGridViewFontButtonCell = value as DataGridViewFontButtonCell;
        if (value != null && dataGridViewFontButtonCell == null)
        {
          throw new InvalidCastException("Value provided for CellTemplate must be of type DataGridViewFontButtonCell or derive from it.");
        }
        base.CellTemplate = value;
      }
    }

    /// <summary>
    /// Small utility function that returns the template cell
    /// as a <see cref="DataGridViewFontButtonCell"/>
    /// </summary>
    /// <value>A <see cref="DataGridViewFontButtonCell"/> template.</value>
    private DataGridViewFontButtonCell FontButtonCellTemplate
    {
      get
      {
        return (DataGridViewFontButtonCell)this.CellTemplate;
      }
    }

    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Constructor for the DataGridViewFontButtonColumn class.
    /// </summary>
    public DataGridViewFontButtonColumn()
      : base(new DataGridViewFontButtonCell())
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
    /// <returns>A <see cref="string"/> with the compact string representation of the column.</returns>
    public override string ToString()
    {
      StringBuilder sb = new StringBuilder(100);
      sb.Append("DataGridViewFontButtonColumn { Name=");
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