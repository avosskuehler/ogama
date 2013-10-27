using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace OgamaControls
{
  /// <summary>
  /// Defines a <strong>DataGridViewQuickComboBoxCell</strong> cell type for the 
  /// <see cref="System.Windows.Forms.DataGridView"/> control
  /// </summary>
  public class DataGridViewQuickComboBoxCell : DataGridViewComboBoxCell
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
    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Constructor for the <strong>DataGridViewColorButton</strong> cell type
    /// </summary>
    public DataGridViewQuickComboBoxCell()
    {
      //DataGridViewCellStyle newStyle = new DataGridViewCellStyle();
      //newStyle.Alignment = DataGridViewContentAlignment.TopLeft;
      //this.Style = newStyle;
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
    /// Override OnMouseClick in a class derived from DataGridViewCell to 
    /// enter edit mode when the user clicks the cell. 
    /// </summary>
    /// <param name="e">The <see cref="DataGridViewCellMouseEventArgs"/>
    /// with the event data.</param>
    protected override void OnMouseClick(DataGridViewCellMouseEventArgs e)
    {
      if (base.DataGridView != null)
      {
        Point point1 = base.DataGridView.CurrentCellAddress;
        if (point1.X == e.ColumnIndex &&
            point1.Y == e.RowIndex &&
            e.Button == MouseButtons.Left &&
            base.DataGridView.EditMode !=
            DataGridViewEditMode.EditProgrammatically)
        {
          base.DataGridView.BeginEdit(true);
          ((ComboBox)base.DataGridView.EditingControl).DroppedDown = true;
        }
      }
    }

    /// <summary>
    /// Returns a string that describes the current object. 
    /// </summary>
    /// <returns>A <see cref="string"/> that represents the current object. </returns>
    public override string ToString()
    {
      return "DataGridViewQuickComboBoxCell { ColumnIndex=" + ColumnIndex.ToString(CultureInfo.CurrentCulture) + ", RowIndex=" + RowIndex.ToString(CultureInfo.CurrentCulture) + " }";
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
