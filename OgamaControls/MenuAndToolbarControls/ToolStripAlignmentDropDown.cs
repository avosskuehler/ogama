// <copyright file="CaptureMode.cs" company="FU Berlin">
// ******************************************************
// OGAMA - open gaze and mouse analyzer 
// Copyright (C) 2013 Dr. Adrian Voßkühler  
// ------------------------------------------------------------------------
// This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
// This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
// You should have received a copy of the GNU General Public License along with this program; if not, write to the Free Software Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
// **************************************************************
// </copyright>
// <author>Adrian Voßkühler</author>
// <email>adrian@ogama.net</email>

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using VectorGraphics;

namespace OgamaControls
{
  using VectorGraphics.Tools.CustomTypeConverter;

  /// <summary>
  /// Class for a toolstrip hostable color drop down control
  /// </summary>
  [ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.All)]
  [ToolboxBitmap(typeof(PositionDropdown))]
  public class ToolStripAlignmentDropdown : ToolStripComboBox, IPositionControl
  {
    /// <summary>
    /// The position in stimulus screen coordinates.
    /// </summary>
    private Point _position;

    /// <summary>
    /// The stimulus screen size.
    /// </summary>
    private Size _stimulusScreenSize;

    /// <summary>
    /// Horizontal alignment of object at given position
    /// </summary>
    private HorizontalAlignment _alignment;

    /// <summary>
    /// 
    /// </summary>
    public event EventHandler PositionChanged;

    /// <summary>
    /// Gets or sets the current position value of the dropdown button.
    /// </summary>
    /// <value>A <see cref="Point"/> with the new position value.</value>
    public Point CurrentPosition
    {
      get
      {
        return _position;
      }
      set
      {
        _position = value;
        this.Items.Clear();
        this.Items.Add(ObjectStringConverter.PointToString(value));
        this.SelectedIndex = 0;
        OnPositionChanged(EventArgs.Empty);
      }
    }

    /// <summary>
    /// Gets or set the stimulus screen size
    /// </summary>
    /// <remarks>Used to size the position control correctly.</remarks>
    /// <value>A <see cref="Size"/> with the stimulus screen size.</value>
    public Size StimulusScreenSize
    {
      get { return _stimulusScreenSize; }
      set { _stimulusScreenSize = value; }
    }

    /// <summary>
    /// Horizontal text alignment for text at position value.
    /// </summary>
    /// <value>A <see cref="HorizontalAlignment"/> with the new alignment of the text.</value>
    public new HorizontalAlignment Alignment
    {
      get { return _alignment; }
      set { _alignment = value; }
    }

    /// <summary>
    /// On mouse down event handler. Shows color selection panel of type <see cref="ColorPanel"/>.
    /// </summary>
    /// <param name="e">A <see cref="MouseEventArgs"/> that contains the event data.</param>
    protected override void OnMouseDown(MouseEventArgs e)
    {
      //Open Position Selector.
      PositionSelector posSelector = new PositionSelector(
          base.Parent.PointToScreen(new Point(base.Bounds.Left, base.Bounds.Bottom)),
          this, this._position, this._stimulusScreenSize);
      posSelector.Alignment = this._alignment;
      if (posSelector.ShowDialog() == DialogResult.OK)
      {
        this.CurrentPosition = this._position;
      }
    }

    /// <summary>
    /// On position changed event handler. Raises delegate.
    /// </summary>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    public void OnPositionChanged(EventArgs e)
    {
      if (this.PositionChanged != null)
      {
        this.PositionChanged(this, e);
      }
    }

  }

}
