// <copyright file="ToolStripTrackBar.cs" company="FU Berlin">
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
  /// Class for a toolstrip hostable track bar control
  /// </summary>
  [ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.All)]
  [ToolboxBitmap(typeof(TrackBar))]
  public class ToolStripTrackBar : ToolStripControlHost
  {
    /// <summary>
    /// Value changed event handler from track bar control
    /// </summary>
    public event EventHandler ValueChanged;

    /// <summary>
    /// Constructor. Initializes Component and event handler
    /// </summary>
    public ToolStripTrackBar()
      : base(new TrackBar())
    {
      this.ToolTipText = "Zoom, right-click for autozoom";
      this.SendValueChangedEvents = true;
    }

    /// <summary>
    /// Gets or sets a value indicating whether to send value changed events.
    /// </summary>
    public bool SendValueChangedEvents;

    /// <summary>
    /// Gets or sets current value
    /// </summary>
    public int Value
    {
      get { return TrackBar.Value; }
      set { TrackBar.Value = value; }
    }

    /// <summary>
    /// Gets or sets the minimum allowed value for the track bar
    /// </summary>
    public int Minimum
    {
      get { return TrackBar.Minimum; }
      set { TrackBar.Minimum = value; }
    }

    /// <summary>
    /// Gets or sets the maximal allowed value for the track bar
    /// </summary>
    public int Maximum
    {
      get { return TrackBar.Maximum; }
      set { TrackBar.Maximum = value; }
    }

    /// <summary>
    /// Gets or sets the TickFrequency for the track bar
    /// </summary>
    public int TickFrequency
    {
      get { return TrackBar.TickFrequency; }
      set { TrackBar.TickFrequency = value; }
    }

    /// <summary>
    /// Gets or sets the tick style for the track bar
    /// </summary>
    public TickStyle TickStyle
    {
      get { return TrackBar.TickStyle; }
      set { TrackBar.TickStyle = value; }
    }

    /// <summary>
    /// Gets the underlying track bar control
    /// </summary>
    public TrackBar TrackBar
    {
      get { return Control as TrackBar; }
    }

    /// <summary>
    /// Overridden. Subscribes events from the hosted control.
    /// </summary>
    /// <param name="c">control</param>
    protected override void OnSubscribeControlEvents(Control c)
    {
      // Call the base so the base events are connected.
      base.OnSubscribeControlEvents(c);

      // Cast the control to a TrackBar control.
      TrackBar trackBarControl = (TrackBar)c;

      // Add the event.
      trackBarControl.ValueChanged += new EventHandler(TrackBar_ValueChanged);
    }

    /// <summary>
    /// Overridden. Unsubscribes events from the hosted control.
    /// </summary>
    /// <param name="c">control</param>
    protected override void OnUnsubscribeControlEvents(Control c)
    {
      // Call the base method so the basic events are unsubscribed.
      base.OnUnsubscribeControlEvents(c);

      // Cast the control to a TrackBar control.
      TrackBar trackBarControl = (TrackBar)c;

      // Remove the event.
      trackBarControl.ValueChanged -=
          new EventHandler(TrackBar_ValueChanged);
    }

    /// <summary>
    /// Event handler. Raises value changed event.
    /// </summary>
    /// <param name="sender">sender</param>
    /// <param name="e">EventArgs</param>
    public void TrackBar_ValueChanged(object sender, EventArgs e)
    {
      if (ValueChanged != null && this.SendValueChangedEvents)
      {
        ValueChanged(sender, e);
      }
    }

    /// <summary>
    /// Overridden GetPreferredSize. Shrinks control to height of 23.
    /// </summary>
    /// <param name="constrainingSize">The constraining size input</param>
    /// <returns>The preferred size for this control.</returns>
    public override Size GetPreferredSize(Size constrainingSize)
    {
      // Retrieve the preferred size from the base class, but change the
      // height 
      Size size = base.GetPreferredSize(constrainingSize);
      size.Height = 23;
      return size;
    }
  }
}
