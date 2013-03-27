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

namespace OgamaControls
{
  /// <summary>
  /// Class for a toolstrip hostable numeric up down control
  /// </summary>
  [ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.All)]
  [ToolboxBitmap(typeof(NumericUpDown))]
  public class ToolStripNumericUpDown : ToolStripControlHost
  {
    /// <summary>
    /// Value changed event handler from numeric up down control
    /// </summary>
    public event EventHandler ValueChanged;

    /// <summary>
    /// Constructor. Initializes Component and event handler
    /// </summary>
    public ToolStripNumericUpDown()
      : base(new NumericUpDown())
    {
      //InitializeComponent();
      //this.NumericUpDown.ValueChanged += new EventHandler(NumericUpDown_ValueChanged);
    }

    /// <summary>
    /// Gets or sets current value
    /// </summary>
    public Decimal Value
    {
      get { return NumericUpDown.Value; }
      set { NumericUpDown.Value = value; }
    }

    /// <summary>
    /// Gets or sets the minimum allowed value for the numeric up down
    /// </summary>
    public Decimal Minimum
    {
      get { return NumericUpDown.Minimum; }
      set { NumericUpDown.Minimum = value; }
    }

    /// <summary>
    /// Gets or sets the maximal allowed value for the numeric up down
    /// </summary>
    public Decimal Maximum
    {
      get { return NumericUpDown.Maximum; }
      set { NumericUpDown.Maximum = value; }
    }

    /// <summary>
    /// Gets or sets the increment for the numeric up down
    /// </summary>
    public Decimal Increment
    {
      get { return NumericUpDown.Increment; }
      set { NumericUpDown.Increment = value; }
    }

    /// <summary>
    /// Gets or sets the number of decimal places for the numeric up down
    /// </summary>
    public int DecimalPlaces
    {
      get { return NumericUpDown.DecimalPlaces; }
      set { NumericUpDown.DecimalPlaces = value; }
    }

    /// <summary>
    /// Gets the underlying nueric up down control
    /// </summary>
    public NumericUpDown NumericUpDown
    {
      get { return Control as NumericUpDown; }
    }

    /// <summary>
    /// Overridden. Subscribes events from the hosted control.
    /// </summary>
    /// <param name="c">control</param>
    protected override void OnSubscribeControlEvents(Control c)
    {
      // Call the base so the base events are connected.
      base.OnSubscribeControlEvents(c);

      // Cast the control to a NumericUpDown control.
      NumericUpDown numericUpDownControl = (NumericUpDown)c;

      // Add the event.
      numericUpDownControl.ValueChanged += new EventHandler(NumericUpDown_ValueChanged);
    }

    /// <summary>
    /// Overridden. Unsubscribes events from the hosted control.
    /// </summary>
    /// <param name="c">control</param>
    protected override void OnUnsubscribeControlEvents(Control c)
    {
      // Call the base method so the basic events are unsubscribed.
      base.OnUnsubscribeControlEvents(c);

      // Cast the control to a NumericUpDown control.
      NumericUpDown numericUpDownControl = (NumericUpDown)c;

      // Remove the event.
      numericUpDownControl.ValueChanged -=
          new EventHandler(NumericUpDown_ValueChanged);
    }

    /// <summary>
    /// Event handler. Raises value changed event.
    /// </summary>
    /// <param name="sender">sender</param>
    /// <param name="e">EventArgs</param>
    public void NumericUpDown_ValueChanged(object sender, EventArgs e)
    {
      if (ValueChanged != null)
        ValueChanged(sender, e);
    }
  }

}
