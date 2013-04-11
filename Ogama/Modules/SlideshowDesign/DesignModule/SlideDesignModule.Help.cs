// <copyright file="SlideDesignModule.Help.cs" company="FU Berlin">
// ******************************************************
// OGAMA - open gaze and mouse analyzer 
// Copyright (C) 2012 Adrian Voßkühler  
// ------------------------------------------------------------------------
// This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
// This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
// You should have received a copy of the GNU General Public License along with this program; if not, write to the Free Software Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
// **************************************************************
// </copyright>
// <author>Adrian Voßkühler</author>
// <email>adrian@ogama.net</email>

namespace Ogama.Modules.SlideshowDesign.DesignModule
{
  using System;
  using System.Text;
  using System.Windows.Forms;

  using Ogama.Modules.Common.Controls;

  /// <summary>
  /// The SlideDesignModule.Help.cs contains the <see cref="Control.Click"/>
  /// event handler for all context help buttons in the module.
  /// </summary>
  public partial class SlideDesignModule
  {
    /// <summary>
    /// <see cref="Control.Click"/> event handler 
    /// for the <see cref="PictureBox"/> <see cref="pcbHelpTiming"/>
    /// Shows a <see cref="HelpDialog"/> with help information on
    /// how to define timing conditions.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void pcbHelpTiming_Click(object sender, EventArgs e)
    {
      HelpDialog dlg = new HelpDialog();
      dlg.HelpCaption = "How to: Stop slides from beeing displayed";
      StringBuilder sb = new StringBuilder();
      sb.AppendLine("Specify the response that the stimulus slide waits for.");
      sb.Append("This can be a mouse button or a key or an elapsed time, or ");
      sb.AppendLine("a combination.");
      sb.Append("By clicking the checkbox 'but only in target areas' the mouse click that stops the ");
      sb.AppendLine("slide is restricted to the area of the targets defined in the next section.");
      dlg.HelpMessage = sb.ToString();
      dlg.ShowDialog();
    }

    /// <summary>
    /// <see cref="Control.Click"/> event handler 
    /// for the <see cref="PictureBox"/> <see cref="pchHelpTargets"/>
    /// Shows a <see cref="HelpDialog"/> with help information on
    /// how to define targets.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void pchHelpTargets_Click(object sender, EventArgs e)
    {
      HelpDialog dlg = new HelpDialog();
      dlg.HelpCaption = "How to: Define target areas.";
      StringBuilder sb = new StringBuilder();
      sb.Append("When adding target areas, the subjects valid mouse clicks can be restricted ");
      sb.AppendLine("to specific regions of the slide.");
      sb.Append("For example in a multiple choice question where the subject has ");
      sb.Append("the choice between four figures, each one could be defined as an ");
      sb.Append("target area by clicking 'Add target rectangle' and clicking and dragging ");
      sb.AppendLine("the referring rectangle on the slide.");
      sb.Append("After this definition the newly created target appears in the target list ");
      sb.AppendLine("and can be used in the 'Timing' section for restricting users valid responses");
      dlg.HelpMessage = sb.ToString();
      dlg.ShowDialog();
    }

    /// <summary>
    /// <see cref="Control.Click"/> event handler 
    /// for the <see cref="PictureBox"/> <see cref="pcbHelpTesting"/>
    /// Shows a <see cref="HelpDialog"/> with help information on
    /// how to define testing conditions.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void pcbHelpTesting_Click(object sender, EventArgs e)
    {
      HelpDialog dlg = new HelpDialog();
      dlg.HelpCaption = "How to: Define correct responses.";
      StringBuilder sb = new StringBuilder();
      sb.AppendLine("Adding correct responses simplifies the analysation process.");
      sb.Append("When the users responses is in the list of correct responses ");
      sb.Append("the column 'CorrectResponse' of the database will be filled ");
      sb.Append("with 'correct answered', so quick check for errors is possible.");
      dlg.HelpMessage = sb.ToString();
      dlg.ShowDialog();
    }

    /// <summary>
    /// <see cref="Control.Click"/> event handler 
    /// for the <see cref="PictureBox"/> <see cref="pcbLinksHelp"/>
    /// Shows a <see cref="HelpDialog"/> with help information on
    /// how to define links between slides.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void pcbLinksHelp_Click(object sender, EventArgs e)
    {
      HelpDialog dlg = new HelpDialog();
      dlg.HelpCaption = "How to: Define links to other trials.";
      StringBuilder sb = new StringBuilder();
      sb.AppendLine("Adding links to other trials enables to jump to different trials from one slide.");
      sb.Append("When the users response is in the list of links ");
      sb.Append("the slideshow will jump to the linked trial. ");
      dlg.HelpMessage = sb.ToString();
      dlg.ShowDialog();
    }

    /// <summary>
    /// <see cref="Control.Click"/> event handler 
    /// for the <see cref="PictureBox"/> <see cref="pcbHelpNaming"/>
    /// Shows a <see cref="HelpDialog"/> with help information on
    /// how to define slide groups.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void pcbHelpNaming_Click(object sender, EventArgs e)
    {
      HelpDialog dlg = new HelpDialog();
      dlg.HelpCaption = "How to: Categorize slides.";
      StringBuilder sb = new StringBuilder();
      sb.Append("You can group multiple slides to one TRIAL, ");
      sb.Append("which is the unit OGAMA uses for analysation, ");
      sb.AppendLine("by right clicking in the slide treeview. ");
      sb.Append("The category is an optional grouping possibility at slide level, ");
      sb.Append("that can be used in the statistics output.");

      dlg.HelpMessage = sb.ToString();
      dlg.ShowDialog();
    }

    /// <summary>
    /// <see cref="Control.Click"/> event handler 
    /// for the <see cref="Button"/> <see cref="btnHelp"/>
    /// Shows a <see cref="HelpDialog"/> with help information on
    /// tips and tricks.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnHelpModule_Click(object sender, EventArgs e)
    {
      HelpDialog dlg = new HelpDialog();
      dlg.HelpCaption = "TIPS & TRICKS:";
      StringBuilder sb = new StringBuilder();
      sb.AppendLine("If some elements overlay each other, press during selection the ALT key to switch elements.");
      sb.AppendLine("For images: ");
      sb.Append("Press CONTROL key during creation to get an original sized image, ");
      sb.AppendLine("press ALT key to resize proportional.");
      sb.AppendLine();
      sb.AppendLine("Layout: ");
      sb.AppendLine("Press the DELETE key to remove elements.");
      sb.AppendLine("+: Bring forward");
      sb.AppendLine("-: Send backward");
      sb.AppendLine("PgDown: Send to back");
      sb.AppendLine("PgUp: Bring to front");
      dlg.HelpMessage = sb.ToString();
      dlg.ShowDialog();
    }

    /// <summary>
    /// <see cref="Control.Click"/> event handler 
    /// for the <see cref="PictureBox"/> <see cref="pcbHelpTiming"/>
    /// Shows a <see cref="HelpDialog"/> with help information on
    /// how to define timing conditions.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void pcbHelpTrigger_Click(object sender, EventArgs e)
    {
      HelpDialog dlg = new HelpDialog();
      dlg.HelpCaption = "How to: Define slide triggers.";
      StringBuilder sb = new StringBuilder();
      sb.Append("You can send a trigger signal to an output device at the beginning of each slide, ");
      sb.AppendLine("with the settings made in this tab. ");
      sb.Append("There is also the option to define a general trigger for all slides ");
      sb.Append("in the recording interface. ");
      sb.AppendLine("Don´t forget to enable trigger sending there.");

      dlg.HelpMessage = sb.ToString();
      dlg.ShowDialog();
    }
  }
}