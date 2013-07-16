// <copyright file="SlideDesignModule.Common.cs" company="FU Berlin">
// ******************************************************
// OGAMA - open gaze and mouse analyzer 
// Copyright (C) 2008 Adrian Voßkühler  
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
  using System.Collections.Generic;
  using System.Drawing;
  using System.IO;
  using System.Windows.Forms;

  using Ogama.ExceptionHandling;
  using Ogama.Modules.Common.SlideCollections;

  using VectorGraphics.Elements;
  using VectorGraphics.StopConditions;

  /// <summary>
  /// The SlideDesignModule.Common.cs contains methods referring
  /// to the <see cref="TabControl"/> at the left bottom of the 
  /// module to define common properties for all slides like
  /// <see cref="StopCondition"/>, Background and Mouse properties.
  /// </summary>
  public partial class SlideDesignModule
  {
    #region TimingSection

    /// <summary>
    /// <see cref="Control.Click"/> event handler 
    /// for the <see cref="btnAddCondition"/> <see cref="Button"/>
    /// Updates the <see cref="lsbStopConditions"/> items with the new 
    /// stop condition, resp. response.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnAddCondition_Click(object sender, EventArgs e)
    {
      if (this.rdbTime.Checked)
      {
        TimeStopCondition tsc = new TimeStopCondition((int)(this.nudTime.Value * 1000));

        // Remove existing TimeConditions, because only one can be valid...
        TimeStopCondition oldTimeCondition = null;
        foreach (object condition in this.lsbStopConditions.Items)
        {
          if (condition is TimeStopCondition)
          {
            oldTimeCondition = (TimeStopCondition)condition;
          }
        }

        if (oldTimeCondition != null)
        {
          this.lsbStopConditions.Items.Remove(oldTimeCondition);
        }

        this.lsbStopConditions.Items.Add(tsc);
      }
      else if (this.rdbMouse.Checked)
      {
        string selectedItemMouse = (string)this.cbbMouseButtons.SelectedItem;
        MouseStopCondition msc = new MouseStopCondition();
        msc.CanBeAnyInputOfThisType = selectedItemMouse == "Any" ? true : false;
        if (!msc.CanBeAnyInputOfThisType)
        {
          msc.StopMouseButton = (MouseButtons)Enum.Parse(typeof(MouseButtons), selectedItemMouse);
        }

        msc.Target = this.chbOnlyWhenInTarget.Checked ? "Any" : string.Empty;

        if (!this.lsbStopConditions.Items.Contains(msc))
        {
          this.lsbStopConditions.Items.Add(msc);
        }
      }
      else if (this.rdbKey.Checked)
      {
        string selectedItemKeys = (string)this.cbbKeys.SelectedItem;
        KeyStopCondition ksc = new KeyStopCondition();
        if (selectedItemKeys == "Any")
        {
          ksc.CanBeAnyInputOfThisType = true;
        }
        else
        {
          ksc.StopKey = (Keys)Enum.Parse(typeof(Keys), selectedItemKeys);
        }

        if (!this.lsbStopConditions.Items.Contains(ksc))
        {
          this.lsbStopConditions.Items.Add(ksc);
        }
      }
    }

    /// <summary>
    /// <see cref="Control.Click"/> event handler 
    /// for the <see cref="btnRemoveCondition"/> <see cref="Button"/>
    /// Removes the selected <see cref="StopCondition"/> items from
    /// the <see cref="lsbStopConditions"/> list.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnRemoveCondition_Click(object sender, EventArgs e)
    {
      DeleteSelectedItems(this.lsbStopConditions);
    }

    #endregion TimingSection

    #region Targetssection

    /// <summary>
    /// <see cref="Control.Click"/> event handler 
    /// for the  <see cref="Button"/> <see cref="btnAddTargetRectangle"/>
    /// Starts a new target creation.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnAddTargetRectangle_Click(object sender, EventArgs e)
    {
      this.designPicture.NewTargetShape(VGShapeType.Rectangle);
      this.designPicture.Invalidate();
    }

    /// <summary>
    /// <see cref="Control.Click"/> event handler 
    /// for the  <see cref="Button"/> <see cref="btnAddTargetEllipse"/>
    /// Starts a new target creation.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnAddTargetEllipse_Click(object sender, EventArgs e)
    {
      this.designPicture.NewTargetShape(VGShapeType.Ellipse);
      this.designPicture.Invalidate();
    }

    /// <summary>
    /// <see cref="Control.Click"/> event handler 
    /// for the  <see cref="Button"/> <see cref="btnAddTargetPolyline"/>
    /// Starts a new target creation.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnAddTargetPolyline_Click(object sender, EventArgs e)
    {
      this.designPicture.NewTargetShape(VGShapeType.Polyline);
      this.designPicture.Invalidate();
    }

    /// <summary>
    /// <see cref="Control.Click"/> event handler 
    /// for the  <see cref="Button"/> <see cref="btnRemoveTarget"/>
    /// Removes the selected targets from the list.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnRemoveTarget_Click(object sender, EventArgs e)
    {
      List<string> selectedObjects = new List<string>();
      foreach (string name in this.lsbTargets.SelectedItems)
      {
        selectedObjects.Add(name);
      }

      foreach (string name in selectedObjects)
      {
        this.lsbTargets.Items.Remove(name);
        this.cbbTestingTargets.Items.Remove(name);
        this.cbbLinksTargets.Items.Remove(name);
        this.designPicture.Elements.Remove(name);
      }

      this.designPicture.DrawForeground(true);
    }

    /// <summary>
    /// <see cref="Control.Click"/> event handler 
    /// for the  <see cref="ListBox"/> <see cref="lsbTargets"/>
    /// Selectes the selected target element in the picture (show grab handles and selection frame)
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void lsbTargets_Click(object sender, EventArgs e)
    {
      if (this.lsbTargets.SelectedItem != null)
      {
        this.designPicture.SelectedElement = this.designPicture.Elements.GetElementByName(this.lsbTargets.SelectedItem.ToString());
        this.designPicture.Invalidate();
      }
    }

    #endregion Targetssection

    #region Testingsection

    /// <summary>
    /// <see cref="Control.Click"/> event handler 
    /// for the  <see cref="Button"/> <see cref="btnAddCorrectResponse"/>
    /// Adds a new <see cref="StopCondition"/> (response condition) to the slide
    /// according to the settings made in the UI.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnAddCorrectResponse_Click(object sender, EventArgs e)
    {
      if (this.rdbTestingMouse.Checked)
      {
        MouseStopCondition msc = new MouseStopCondition();

        string selectedItemMouse = (string)this.cbbTestingMouseButtons.SelectedItem;
        msc.CanBeAnyInputOfThisType = selectedItemMouse == "Any" ? true : false;
        msc.Target = this.cbbTestingTargets.Text;
        if (!msc.CanBeAnyInputOfThisType)
        {
          msc.StopMouseButton = (MouseButtons)Enum.Parse(typeof(MouseButtons), selectedItemMouse);
        }

        if (!this.lsbCorrectResponses.Items.Contains(msc))
        {
          this.lsbCorrectResponses.Items.Add(msc);
        }

        // Add this stop condition to the stopcondition list.
        if (!this.lsbStopConditions.Items.Contains((MouseStopCondition)msc.Clone()))
        {
          this.lsbStopConditions.Items.Add(msc);
        }
      }
      else if (this.rdbTestingKey.Checked)
      {
        KeyStopCondition ksc = new KeyStopCondition();

        string selectedItemKeys = (string)this.cbbTestingKeys.SelectedItem;
        ksc.CanBeAnyInputOfThisType = selectedItemKeys == "Any" ? true : false;
        if (!ksc.CanBeAnyInputOfThisType)
        {
          ksc.StopKey = (Keys)Enum.Parse(typeof(Keys), selectedItemKeys);
        }

        // Add this condition to the stopcondition list.
        if (!this.lsbStopConditions.Items.Contains(ksc))
        {
          this.lsbStopConditions.Items.Add((KeyStopCondition)ksc.Clone());
        }

        // Add this condition to the testing list.
        if (!this.lsbCorrectResponses.Items.Contains(ksc))
        {
          this.lsbCorrectResponses.Items.Add(ksc);
        }
      }
    }

    /// <summary>
    /// <see cref="Control.Click"/> event handler 
    /// for the  <see cref="Button"/> <see cref="btnRemoveCorrectResponse"/>
    /// Removes the selected responses from the list.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnRemoveCorrectResponse_Click(object sender, EventArgs e)
    {
      DeleteSelectedItems(this.lsbCorrectResponses);
    }

    #endregion //Testingsection

    #region Linkssection

    /// <summary>
    /// <see cref="Control.Click"/> event handler 
    /// for the  <see cref="Button"/> <see cref="btnAddLink"/>
    /// Adds a new <see cref="StopCondition"/> (as link condition) to the slide
    /// according to the settings made in the UI.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnAddLink_Click(object sender, EventArgs e)
    {
      if (this.rdbLinksMouse.Checked)
      {
        MouseStopCondition msc = new MouseStopCondition();

        string selectedItemMouse = (string)this.cbbLinksMouseButtons.SelectedItem;
        msc.CanBeAnyInputOfThisType = selectedItemMouse == "Any" ? true : false;
        msc.Target = this.cbbLinksTargets.Text;
        if (!msc.CanBeAnyInputOfThisType)
        {
          msc.StopMouseButton = (MouseButtons)Enum.Parse(typeof(MouseButtons), selectedItemMouse);
        }

        Trial selectedTrial = (Trial)this.cbbLinksTrial.SelectedItem;
        if (selectedTrial != null)
        {
          msc.TrialID = selectedTrial.ID;

          // Add this link to the link list.
          if (!this.lsbLinks.Items.Contains(msc))
          {
            this.lsbLinks.Items.Add((MouseStopCondition)msc.Clone());
          }

          if (msc.Target != string.Empty)
          {
            msc.Target = "Any";
          }

          msc.TrialID = null;

          // Add this link to the stopcondition list.
          if (!this.lsbStopConditions.Items.Contains(msc))
          {
            this.lsbStopConditions.Items.Add(msc);
          }
        }
      }
      else if (this.rdbLinksKey.Checked)
      {
        KeyStopCondition ksc = new KeyStopCondition();

        string selectedItemKeys = (string)this.cbbLinksKeys.SelectedItem;
        ksc.CanBeAnyInputOfThisType = selectedItemKeys == "Any" ? true : false;
        if (!ksc.CanBeAnyInputOfThisType)
        {
          ksc.StopKey = (Keys)Enum.Parse(typeof(Keys), selectedItemKeys);
        }

        Trial selectedTrial = (Trial)this.cbbLinksTrial.SelectedItem;
        if (selectedTrial != null)
        {
          ksc.TrialID = selectedTrial.ID;

          // Add this link to the link list.
          if (!this.lsbLinks.Items.Contains(ksc))
          {
            this.lsbLinks.Items.Add((KeyStopCondition)ksc.Clone());
          }

          // Add this link to the stopcondition list.
          if (!this.lsbStopConditions.Items.Contains(ksc))
          {
            this.lsbStopConditions.Items.Add(ksc);
          }
        }
      }
    }

    /// <summary>
    /// <see cref="Control.Click"/> event handler 
    /// for the  <see cref="Button"/> <see cref="btnRemoveLink"/>
    /// Removes the selected link from the list.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnRemoveLink_Click(object sender, EventArgs e)
    {
      DeleteSelectedItems(this.lsbLinks);
    }

    #endregion //Linkssection

    #region BackgroundSection

    /// <summary>
    /// <see cref="OgamaControls.ColorButton.ColorChanged"/> event handler 
    /// for the <see cref="btnBackgroundColor"/> <see cref="OgamaControls.ColorButton"/>.
    /// Updates slide and preview with the new background color.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnBackgroundColor_ColorChanged(object sender, EventArgs e)
    {
      this.designPicture.BackColor = this.btnBackgroundColor.CurrentColor;
      this.rtbInstructions.BackColor = this.btnBackgroundColor.CurrentColor;
      this.designPicture.Invalidate();
    }

    /// <summary>
    /// <see cref="Control.Click"/> event handler 
    /// for the <see cref="btnBackgroundImage"/> <see cref="Button"/>.
    /// Opens the <see cref="ofdBackgroundImage"/> <see cref="OpenFileDialog"/>
    /// to ask for the background image file, then updates slide and preview
    /// with the new background image.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnBackgroundImage_Click(object sender, EventArgs e)
    {
      if (this.ofdBackgroundImage.ShowDialog() == DialogResult.OK)
      {
        if (File.Exists(this.ofdBackgroundImage.FileName))
        {
          using (FileStream fs = File.OpenRead(this.ofdBackgroundImage.FileName))
          {
            Image original = Image.FromStream(fs);
            this.designPicture.BackgroundImage = new Bitmap(original);
          }
        }
        else
        {
          this.designPicture.BackgroundImage = null;

          string message = "Background image file: " + Environment.NewLine +
            this.ofdBackgroundImage.FileName + Environment.NewLine +
            " could not be found";

          ExceptionMethods.ProcessMessage("File not found", message);
        }
      }

      this.designPicture.Invalidate();
    }

    /// <summary>
    /// <see cref="Control.Click"/> event handler for the <see cref="btnDeleteBackgroundImage"/>
    /// <see cref="Button"/>.
    /// Updates slide with deleting file reference and redraws preview.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnDeleteBackgroundImage_Click(object sender, EventArgs e)
    {
      this.designPicture.BackgroundImage = null;
      this.designPicture.Invalidate();
    }

    #endregion //BackgroundSection

    #region Mousesection

    /// <summary>
    /// <see cref="RadioButton.CheckedChanged"/> event handler 
    /// for the <see cref="rdbShowMouseCursor"/> <see cref="RadioButton"/>
    /// Shows or hides the <see cref="grbInitialPosition"/> to define intial positions.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void rdbShowMouseCursor_CheckedChanged(object sender, EventArgs e)
    {
      this.grbInitialPosition.Visible = this.rdbShowMouseCursor.Checked;
    }

    #endregion //Mousesection
  }
}