// <copyright file="CaptureMode.cs" company="FU Berlin">
// ******************************************************
// OGAMA - open gaze and mouse analyzer 
// Copyright (C) 2015 Dr. Adrian Voßkühler  
// ------------------------------------------------------------------------
// This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
// This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
// You should have received a copy of the GNU General Public License along with this program; if not, write to the Free Software Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
// **************************************************************
// </copyright>
// <author>Adrian Voßkühler</author>
// <email>adrian@ogama.net</email>

using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace OgamaControls
{
  /// <summary>
  /// Class for a toolstrip hostable numeric up down control
  /// </summary>
  [ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.All)]
  [ToolboxBitmap(typeof(RadioButton))]
  public class ToolStripRadioButton : ToolStripControlHost
  {
    /// <summary>
    /// Value changed event handler from numeric up down control
    /// </summary>
    public event EventHandler CheckedChanged;

    /// <summary>
    /// Constructor. Initializes Component and event handler
    /// </summary>
    public ToolStripRadioButton()
      : base(new RadioButton())
    {
      //InitializeComponent();
    }

    /// <summary>
    /// Gets or sets a value indicating whether the control is checked.
    /// </summary>
    public Boolean Checked
    {
      get { return RadioButton.Checked; }
      set { RadioButton.Checked = value; }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the <strong>Checked</strong> 
    /// value and the appearance of the control automatically 
    /// change when the control is clicked.
    /// </summary>
    public Boolean AutoCheck
    {
      get { return RadioButton.AutoCheck; }
      set { RadioButton.AutoCheck = value; }
    }

    /// <summary>
    /// Gets or sets the location of the check box portion of the <strong>RadioButton</strong>. 
    /// </summary>
    public ContentAlignment CheckAlign
    {
      get { return RadioButton.CheckAlign; }
      set { RadioButton.CheckAlign = value; }
    }

    /// <summary>
    /// Gets the underlying radio button control
    /// </summary>
    public RadioButton RadioButton
    {
      get { return Control as RadioButton; }
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
      RadioButton radioButtonControl = (RadioButton)c;

      // Add the event.
      radioButtonControl.CheckedChanged += 
        new EventHandler(RadioButton_CheckedChanged);
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
      RadioButton radioButtonControl = (RadioButton)c;

      // Remove the event.
      radioButtonControl.CheckedChanged -=
          new EventHandler(RadioButton_CheckedChanged);
    }

    /// <summary>
    /// Event handler. Raises RadioButton.CheckedChanged event.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    public void RadioButton_CheckedChanged(object sender, EventArgs e)
    {
      if (CheckedChanged != null)
        CheckedChanged(sender, e);
    }
  }

}
