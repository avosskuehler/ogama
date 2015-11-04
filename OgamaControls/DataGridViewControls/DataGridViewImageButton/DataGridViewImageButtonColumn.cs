using System;
using System.Windows.Forms;
using System.Drawing;

namespace OgamaControls
{
  /// <summary>
  /// Image button column for the <see cref="DataGridView"/>.
  /// Uses <see cref="DataGridViewImageButtonCell"/>.
  /// </summary>
  public class DataGridViewImageButtonColumn : DataGridViewTextBoxColumn
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
    /// The <see cref="Image"/> to display in this column.
    /// </summary>
    private Image _imageValue;

    /// <summary>
    /// The <see cref="Size"/> of the image displayed.
    /// </summary>
    private Size _imageSize;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets the size of the image.
    /// </summary>
    /// <value>A <see cref="Size"/> for the image in this column</value>
    public Size ImageSize
    {
      get { return _imageSize; }
    }

    /// <summary>
    /// Sets the initial directory for the image selection dialog
    /// </summary>
    /// <value>A <see cref="string"/> with the directory to set as initial.</value>
    public string InitialDirectory
    {
      get
      {
        if (this.DataGridViewImageButtonCellTemplate == null)
        {
          throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
        }
        return this.DataGridViewImageButtonCellTemplate.InitialDirectory;
      }
      set 
      { 
        if (this.DataGridViewImageButtonCellTemplate == null)
        {
          throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
        }
        // Update the template cell so that subsequent cloned cells use the new value.
        this.DataGridViewImageButtonCellTemplate.InitialDirectory = value;
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
            DataGridViewImageButtonCell dataGridViewCell = dataGridViewRow.Cells[this.Index] as DataGridViewImageButtonCell;
            if (dataGridViewCell != null)
            {
              // Call the internal SetDecimalPlaces method instead of the property to avoid invalidation 
              // of each cell. The whole column is invalidated later in a single operation for better performance.
              dataGridViewCell.SetInitialDirectory(rowIndex, value);
            }
          }
          this.DataGridView.InvalidateColumn(this.Index);
          // TODO: Call the grid's autosizing methods to autosize the column, rows, column headers / row headers as needed.
        }

      }
    }

    /// <summary>
    /// Gets or sets the image for all the cells in this column.
    /// </summary>
    /// <value>An <see cref="Image"/> for the cells.</value>
    public Image Image
    {
      get { return this._imageValue; }
      set
      {
        if (this.Image != value)
        {
          this._imageValue = value;
          this._imageSize = value.Size;

          if (this.InheritedStyle != null)
          {
            Padding inheritedPadding = this.InheritedStyle.Padding;
            this.DefaultCellStyle.Padding = new Padding(_imageSize.Width,
                                  inheritedPadding.Top, inheritedPadding.Right,
                                    inheritedPadding.Bottom);
          }
        }
      }
    }

    /// <summary>
    /// Small utility function that returns the template cell 
    /// as a <see cref="DataGridViewImageButtonCell"/>
    /// </summary>
    /// <value>A <see cref="DataGridViewImageButtonCell"/> template.</value>
    private DataGridViewImageButtonCell DataGridViewImageButtonCellTemplate
    {
      get { return this.CellTemplate as DataGridViewImageButtonCell; }
    }

    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Constructor.
    /// </summary>
    public DataGridViewImageButtonColumn()
    {
      this.MinimumWidth = 250;
      this.CellTemplate = new DataGridViewImageButtonCell();
      DataGridViewCellStyle newStyle = new DataGridViewCellStyle();
      newStyle.Alignment = DataGridViewContentAlignment.TopLeft;
      newStyle.WrapMode = DataGridViewTriState.True;
      newStyle.Padding = new Padding(105, 0, 0, 0);
      this.DefaultCellStyle = newStyle;
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
    /// cloned <see cref="DataGridViewImageButtonColumn"/>.</returns>
    public override object Clone()
    {
      DataGridViewImageButtonColumn c = base.Clone() as DataGridViewImageButtonColumn;
      c._imageValue = this._imageValue;
      c._imageSize = this._imageSize;
      return c;
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