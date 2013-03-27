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
  /// Class for a toolstrip hostable color drop down control
  /// </summary>
  [ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.All)]
  [ToolboxBitmap(typeof(ColorDropdown))]
  public class ToolStripColorDropDownOld : ToolStripControlHost
  {
    /// <summary>
    /// Color changed event handler from color drop down control
    /// </summary>
    public event EventHandler ColorChanged;

    /// <summary>
    /// Constructor.
    /// </summary>
    public ToolStripColorDropDownOld()
      : base(new ColorDropdown())
    {
    }

    /// <summary>
    /// Gets or sets current color
    /// </summary>
    public Color Color
    {
      get { return ColorDropdown.CurrentColor; }
      set { ColorDropdown.CurrentColor = value; }
    }

    /// <summary>
    /// Gets the underlying color drop down control
    /// </summary>
    public ColorDropdown ColorDropdown
    {
      get { return Control as ColorDropdown; }
    }

    /// <summary>
    /// Overridden. Subscribes events from the hosted control.
    /// </summary>
    /// <param name="c">control</param>
    protected override void OnSubscribeControlEvents(Control c)
    {
      // Call the base so the base events are connected.
      base.OnSubscribeControlEvents(c);

      // Cast the control to a ColorDropdown control.
      ColorDropdown colorDropdownControl = (ColorDropdown)c;

      // Add the event.
      colorDropdownControl.ColorChanged += 
        new EventHandler(colorDropdownControl_ColorChanged);
    }


    /// <summary>
    /// Overridden. Unsubscribes events from the hosted control.
    /// </summary>
    /// <param name="c">control</param>
    protected override void OnUnsubscribeControlEvents(Control c)
    {
      // Call the base method so the basic events are unsubscribed.
      base.OnUnsubscribeControlEvents(c);

      // Cast the control to a ColorDropdown control.
      ColorDropdown colorDropdownControl = (ColorDropdown)c;

      // Remove the event.
      colorDropdownControl.ColorChanged -= 
        new EventHandler(colorDropdownControl_ColorChanged);
    }

    /// <summary>
    /// Event handler. Raises color changed event.
    /// </summary>
    /// <param name="sender">sender</param>
    /// <param name="e">EventArgs</param>
    public void colorDropdownControl_ColorChanged(object sender, EventArgs e)
    {
      if (ColorChanged != null)
        ColorChanged(sender, e);
    }
  }

}
