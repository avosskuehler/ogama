using System;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.ComponentModel;

namespace OgamaControls
{
  /// <summary>
  /// A customized <see cref="DataGridViewTextBoxCell"/> to show images 
  /// at the left side of the cell.
  /// </summary>
  public class DataGridViewImageButtonCell : DataGridViewTextBoxCell
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
    /// The <see cref="Image"/> to display in this cell.
    /// </summary>
    private Image _imageValue;

    /// <summary>
    /// The <see cref="Size"/> of the image displayed.
    /// </summary>
    private Size _imageSize;

    /// <summary>
    /// Saves a <see cref="string"/> with the initial directory for
    /// the image selection.
    /// </summary>
    private string _initialDirectory;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Sets the initial directory for the image selection dialog
    /// </summary>
    /// <value>A <see cref="string"/> with the directory to set as initial.</value>
    public string InitialDirectory
    {
      get { return _initialDirectory; }
      set { _initialDirectory = value; }
    }

    /// <summary>
    /// Gets or sets the <see cref="Image"/> for the cell.
    /// </summary>
    /// <value>The <see cref="Image"/> of this cell.</value>
    public Image Image
    {
      get
      {
        if (this.OwningColumn == null || this.OwningDataGridViewImageButtonColumn == null)
        {
          return _imageValue;
        }
        else if (this._imageValue != null)
        {
          return this._imageValue;
        }
        else
        {
          return this.OwningDataGridViewImageButtonColumn.Image;
        }
      }
      set
      {
        if (this._imageValue != value)
        {
          this._imageValue = value;

          Padding inheritedPadding = this.InheritedStyle.Padding;
          this.Style.Padding = new Padding(_imageSize.Width,
                                            inheritedPadding.Top, inheritedPadding.Right,
                                            inheritedPadding.Bottom);
        }
      }
    }

    /// <summary>
    /// Gets the <see cref="DataGridViewImageButtonColumn"/> that owns this cell.
    /// </summary>
    /// <value>A <see cref="DataGridViewImageButtonColumn"/> that owns this cell.</value>
    private DataGridViewImageButtonColumn OwningDataGridViewImageButtonColumn
    {
      get { return this.OwningColumn as DataGridViewImageButtonColumn; }
    }

    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Constructor.
    /// </summary>
    public DataGridViewImageButtonCell()
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
    /// Overridden. Creates an exact copy of this cell. 
    /// </summary>
    /// <returns>An <see cref="Object"/> that represents the 
    /// cloned <see cref="DataGridViewImageButtonCell"/>.</returns>
    public override object Clone()
    {
      DataGridViewImageButtonCell c = base.Clone() as DataGridViewImageButtonCell;
      c._imageValue = this._imageValue;
      c._imageSize = this._imageSize;
      return c;
    }

    /// <summary>
    /// Customized implementation of the GetFormattedValue function 
    /// in order to created the thumbs from the filename string in the formatted
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
    protected override object GetFormattedValue(object value, int rowIndex, 
      ref DataGridViewCellStyle cellStyle, TypeConverter valueTypeConverter, TypeConverter formattedValueTypeConverter, DataGridViewDataErrorContexts context)
    {
      // By default, the base implementation converts the Decimal 1234.5 into the string "1234.5"
      object formattedValue = base.GetFormattedValue(value, rowIndex, ref cellStyle, valueTypeConverter, formattedValueTypeConverter, context);
      string formattedString = formattedValue as string;
      if (!string.IsNullOrEmpty(formattedString) && value != null)
      {
        if (File.Exists(formattedString))
        {
          using (FileStream fs = File.OpenRead(formattedString))
          {
            Image newImage = Image.FromStream(fs);

            //Image newImage = Image.FromFile(formattedString);
            Image.GetThumbnailImageAbort myCallback =
              new Image.GetThumbnailImageAbort(ThumbnailCallback);
            Bitmap newThumb = (Bitmap)newImage.GetThumbnailImage(100, 75, myCallback, IntPtr.Zero);
            this.Image = newThumb;
          }
        }
        else
        {
          this.Image = null;
        }
      }
      return formattedValue;
    }

    /// <summary>
    /// Custom paints the cell. The base implementation of the DataGridViewTextBoxCell 
    /// type is called first to draw the text box part. Then the <see cref="Image"/> is drawn.
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
    protected override void Paint(Graphics graphics, Rectangle clipBounds,
                Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState,
                object value, object formattedValue, string errorText,
                DataGridViewCellStyle cellStyle,
                DataGridViewAdvancedBorderStyle advancedBorderStyle,
                DataGridViewPaintParts paintParts)
    {
      // Paint the base content
      base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState,
                  value, formattedValue, errorText, cellStyle,
                  advancedBorderStyle, paintParts);

      if (this.Image != null)
      {
        // Draw the image clipped to the cell.
        System.Drawing.Drawing2D.GraphicsContainer container = graphics.BeginContainer();

        graphics.SetClip(cellBounds);
        graphics.DrawImage(this.Image, cellBounds.Location.X, cellBounds.Location.Y, this.Image.Width, this.Image.Height);

        graphics.EndContainer(container);
      }
    }

    /// <summary>
    /// Overridden. Called when the cell is double-clicked. 
    /// Raises an openfile dialog to select the image file.
    /// </summary>
    /// <param name="e">A <see cref="DataGridViewCellEventArgs"/> that contains the event data.</param>
    protected override void OnDoubleClick(DataGridViewCellEventArgs e)
    {
      base.OnDoubleClick(e);
      OpenFileDialog dlg = new OpenFileDialog();
      dlg.Filter = "Images (*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF|All files (*.*)|*.*";
      dlg.Title = "Select stimulus image ...";
      dlg.InitialDirectory = _initialDirectory;

      if (dlg.ShowDialog() == DialogResult.OK)
      {
        using (FileStream fs = File.OpenRead(dlg.FileName))
        {
          Image newImage = Image.FromStream(fs);
          //Image newImage = Image.FromFile(dlg.FileName);
          Image.GetThumbnailImageAbort myCallback =
            new Image.GetThumbnailImageAbort(ThumbnailCallback);
          Bitmap newThumb = (Bitmap)newImage.GetThumbnailImage(100, 75, myCallback, IntPtr.Zero);
          this.Image = newThumb;
        }
        if (this.DataGridView.EditingControl == null)
        {
          this.DataGridView.BeginEdit(true);
        }
        TextBox txb = this.DataGridView.EditingControl as DataGridViewTextBoxEditingControl;
        txb.Text = dlg.FileName;
      }
    }

    #endregion //OVERRIDES

    ///////////////////////////////////////////////////////////////////////////////
    // Methods for doing main class job                                          //
    ///////////////////////////////////////////////////////////////////////////////
    #region METHODS

    /// <summary>
    /// Utility function that sets a new value for the InitialDirectory property of the cell. 
    /// Row index needs to be provided as a parameter because
    /// this cell may be shared among multiple rows.
    /// </summary>
    /// <param name="rowIndex">The row index of the cell.</param>
    /// <param name="value">Directory to set</param>
    internal void SetInitialDirectory(int rowIndex, string value)
    {
      this._initialDirectory = value;
    }


    #endregion //METHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER

    /// <summary>
    /// Thumbnail creation callback.
    /// Currently not used
    /// </summary>
    /// <returns>always false</returns>
    public bool ThumbnailCallback()
    {
      return false;
    }

    #endregion //HELPER
  }
}