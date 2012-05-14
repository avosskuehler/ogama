// <copyright file="DesktopDialog.cs" company="FU Berlin">
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

namespace Ogama.Modules.SlideshowDesign.DesignModule.StimuliDialogs
{
  using System;
  using System.Text;
  using System.Windows.Forms;

  using Ogama.Modules.Common.Controls;

  using VectorGraphics.Elements;
  using VectorGraphics.Elements.ElementCollections;
  using VectorGraphics.StopConditions;

  /// <summary>
  /// This dialog <see cref="Form"/> is used to initially define an 
  /// desktop slide.
  /// </summary>
  public partial class DesktopDialog : Form
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
    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the DesktopDialog class.
    /// </summary>
    public DesktopDialog()
    {
      this.InitializeComponent();
      SlideDesignModule.FillKeyCombo(this.cbbKeys);
      this.nudTime.Value = SlideDesignModule.SLIDEDURATIONINS;

      foreach (Slide slide in Document.ActiveDocument.ExperimentSettings.SlideShow.Slides)
      {
        if (!this.cbbCategory.Items.Contains(slide.Category))
        {
          this.cbbCategory.Items.Add(slide.Category);
        }
      }
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets or sets the <see cref="Slide"/> specified in this dialog.
    /// </summary>
    public Slide Slide
    {
      get
      {
        var desktopSlide = new Slide
          {
            Name = this.SlideName,
            Category = this.cbbCategory.Text
          };

        // Add standard stop condition if none is specified.
        if (this.lsbStopConditions.Items.Count == 0)
        {
          this.lsbStopConditions.Items.Add(new TimeStopCondition(5000));
        }

        // Store Stop conditions
        foreach (StopCondition cond in this.lsbStopConditions.Items)
        {
          desktopSlide.StopConditions.Add(cond);
        }

        // Set presentation size
        desktopSlide.PresentationSize = Document.ActiveDocument.PresentationSize;

        // Set mouse cursor visibility
        desktopSlide.MouseCursorVisible = true;

        // Set modified flag.
        desktopSlide.Modified = true;

        desktopSlide.IsDesktopSlide = true;

        return desktopSlide;
      }

      set
      {
        this.SlideName = value.Name;

        // Update the forms fields with slides values
        this.PopulateDialogWithSlide((Slide)value.Clone());
      }
    }

    /// <summary>
    /// Gets or sets the slidename
    /// </summary>
    /// <value>A <see cref="string"/> with the slidename.</value>
    public string SlideName
    {
      get
      {
        return this.txbName.Text;
      }

      set
      {
        this.txbName.Text = value;
      }
    }

    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler                                                              //
    ///////////////////////////////////////////////////////////////////////////////
    #region EVENTS

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler for UI, Menu, Buttons, Toolbars etc.                         //
    ///////////////////////////////////////////////////////////////////////////////
    #region WINDOWSEVENTHANDLER

    /// <summary>
    /// <see cref="Control.Click"/> event handler 
    /// for the <see cref="Button"/> <see cref="btnHelp"/>
    /// Raises a new <see cref="HelpDialog"/> dialog with instructions.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A empty <see cref="EventArgs"/></param>
    private void btnHelp_Click(object sender, EventArgs e)
    {
      var dlg = new HelpDialog { HelpCaption = "How to: Define a desktop slide." };
      var sb = new StringBuilder();
      sb.Append("The desktop slide is a dummy that can be used to record anything" +
      " that is on the desktop. It starts a screen recorder.");
      dlg.HelpMessage = sb.ToString();
      dlg.ShowDialog();
    }

    /// <summary>
    /// <see cref="Control.Click"/> event handler 
    /// for the <see cref="Button"/> <see cref="btnOK"/>
    /// Sets the <see cref="Form.DialogResult"/> property to <see cref="DialogResult.OK"/>.
    /// Set it manually, because otherwise hitting "enter" in TextBox will
    /// finish dialog.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A empty <see cref="EventArgs"/></param>
    private void btnOK_Click(object sender, EventArgs e)
    {
      this.DialogResult = DialogResult.OK;
    }

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
      SlideDesignModule.DeleteSelectedItems(this.lsbStopConditions);
    }

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
    #endregion //OVERRIDES

    ///////////////////////////////////////////////////////////////////////////////
    // Methods for doing main class job                                          //
    ///////////////////////////////////////////////////////////////////////////////
    #region METHODS

    /// <summary>
    /// Update the dialog forms fields with the content from the given <see cref="Slide"/>.
    /// </summary>
    /// <param name="slide">The <see cref="Slide"/> whichs content  should
    /// be shown in the dialog.</param>
    private void PopulateDialogWithSlide(Slide slide)
    {
      // Common properties
      this.txbName.Text = slide.Name;
      this.cbbCategory.Text = slide.Category;

      // Tab Timing
      foreach (StopCondition condition in slide.StopConditions)
      {
        this.lsbStopConditions.Items.Add(condition);
      }
    }

    #endregion //METHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER
    #endregion //HELPER
  }
}