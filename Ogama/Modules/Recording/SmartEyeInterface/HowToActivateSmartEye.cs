// <copyright file="HowToActivateSmartEye.cs" company="Smart Eye AB">
// ******************************************************
// OGAMA - open gaze and mouse analyzer 
// Copyright (C) 2014 Dr. Adrian Voßkühler  
// ------------------------------------------------------------------------
// This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
// This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
// You should have received a copy of the GNU General Public License along with this program; if not, write to the Free Software Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
// **************************************************************
// </copyright>
// <author>Kathrin Scheil</author>
// <email>kathrin.scheil@smarteye.se</email>

namespace Ogama.Modules.Recording.SmartEyeInterface
{
  using System;
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.Data;
  using System.Drawing;
  using System.Linq;
  using System.Text;
  using System.Windows.Forms;

  /// <summary>
  /// A small popup <see cref="Form"/> for showing a dialog on how to
  /// install the Smart Eye recording option.
  /// </summary>
  public partial class HowToActivateSmartEye : Form
  {
    /// <summary>
    /// Initializes a new instance of the HowToActivateSmartEye class.
    /// </summary>
    public HowToActivateSmartEye()
    {
      this.InitializeComponent();
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for
    /// the <see cref="PictureBox"/> <see cref="pcbSmartEyeLogo"/>.
    /// User clicked the Smart Eye logo,
    /// so open Smart Eye website.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void PcbSmartEyeLogo_Click(object sender, EventArgs e)
    {
      System.Diagnostics.Process.Start("http://www.smarteye.se");
    }

    /// <summary>    
    /// User clicked link, so open Smart Eye website.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void LlbSmartEyeWebsite_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      System.Diagnostics.Process.Start("http://www.smarteye.se");
    }
  }
}
