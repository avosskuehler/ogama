// <copyright file="ShapeDialog.cs" company="FU Berlin">
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
  using System.Drawing;
  using System.Text;
  using System.Windows.Forms;

  using Ogama.Modules.Common.Controls;

  using VectorGraphics.Elements;
  using VectorGraphics.Tools.CustomEventArgs;

  /// <summary>
  /// This dialog <see cref="Form"/> is used to initially define an 
  /// shape that can be added to a slide.
  /// </summary>
  public partial class ShapeDialog : Form
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
    /// Initializes a new instance of the ShapeDialog class.
    /// </summary>
    public ShapeDialog()
    {
      this.InitializeComponent();
      this.audioControl.PathToCopyTo = Document.ActiveDocument.ExperimentSettings.SlideResourcesPath;
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets or sets the <see cref="VGElement"/> element that should be added to the slide.
    /// </summary>
    /// <value>A <see cref="VGElement"/> that should be added to the slide,
    /// this can be a <see cref="VGRectangle"/>, a <see cref="VGEllipse"/>, or
    /// a <see cref="VGPolyline"/> or null if failed.</value>
    public VGElement NewShape
    {
      get
      {
        return this.GenerateNewShape();
      }

      set
      {
        this.pbcStyle.DrawAction = value.ShapeDrawAction;
        this.pbcStyle.NewPen = value.Pen;
        this.pbcStyle.NewBrush = value.Brush;
        this.pbcStyle.NewName = value.Name;
        this.pbcStyle.NewFont = value.Font;
        this.pbcStyle.NewFontColor = value.FontColor;
        this.rdbRectangle.Enabled = false;
        this.rdbEllipse.Enabled = false;
        this.rdbPolyline.Enabled = false;
        this.rdbLine.Enabled = false;
        this.rdbSharp.Enabled = false;
        this.audioControl.Sound = value.Sound;
        if (value is VGRectangle)
        {
          this.rdbRectangle.Checked = true;
          this.rdbRectangle.Enabled = true;
        }
        else if (value is VGEllipse)
        {
          this.rdbEllipse.Checked = true;
          this.rdbEllipse.Enabled = true;
        }
        else if (value is VGPolyline)
        {
          this.rdbPolyline.Checked = true;
          this.rdbPolyline.Enabled = true;
        }
        else if (value is VGLine)
        {
          this.rdbLine.Checked = true;
          this.rdbLine.Enabled = true;
        }
        else if (value is VGSharp)
        {
          this.rdbSharp.Checked = true;
          this.rdbSharp.Enabled = true;
        }
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
    /// The <see cref="RadioButton.CheckedChanged"/> event handler for
    /// the <see cref="RadioButton"/> <see cref="rdbLine"/>.
    /// Sets the <see cref="ShapeDrawAction"/> to Edge if the line is checked.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A empty <see cref="EventArgs"/></param>
    private void rdbLine_CheckedChanged(object sender, EventArgs e)
    {
      if (this.rdbLine.Checked)
      {
        this.pbcStyle.DrawAction = ShapeDrawAction.Edge;
      }
    }

    /// <summary>
    /// <see cref="OgamaControls.PenAndBrushControl.ShapePropertiesChanged"/> event handler 
    /// for the <see cref="OgamaControls.PenAndBrushControl"/> <see cref="pbcStyle"/>
    /// Update <see cref="ShapeDrawAction"/> if designed element is a <see cref="VGLine"/>
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A <see cref="ShapePropertiesChangedEventArgs"/> with the event data.</param>
    private void pbcStyle_ShapePropertiesChanged(object sender, ShapePropertiesChangedEventArgs e)
    {
      if (this.rdbLine.Checked && (e.ShapeDrawAction == (e.ShapeDrawAction | ShapeDrawAction.Fill)))
      {
        this.pbcStyle.DrawAction = ShapeDrawAction.Edge;
      }
    }

    /// <summary>
    /// <see cref="Control.Click"/> event handler 
    /// for the <see cref="Button"/> <see cref="btnHelp"/>
    /// Raises a new <see cref="HelpDialog"/> dialog with instructions
    /// on how to define a new instruction and its position on the picture.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A empty <see cref="EventArgs"/></param>
    private void btnHelp_Click(object sender, EventArgs e)
    {
      HelpDialog dlg = new HelpDialog();
      dlg.HelpCaption = "How to: Define the position of the shape.";
      StringBuilder sb = new StringBuilder();
      sb.AppendLine("When the add rectangle or add ellipse buttons is clicked you can specify position and size of the shape by specifying the bounding rectangle with the left mouse button (click and pull).");
      sb.AppendLine("When adding a polyline you can specify each point of the polyline with a single left mouse click. The polyline creation is finished, when you clicked the first point again.");
      dlg.HelpMessage = sb.ToString();
      dlg.ShowDialog();
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
    /// This method creates the shape that is defined in this dialog to be added to a slide.
    /// </summary>
    /// <returns>The ready to use <see cref="VGElement"/>.</returns>
    private VGElement GenerateNewShape()
    {
      VGElement element = null;
      if (this.rdbRectangle.Checked)
      {
        element = new VGRectangle(
          this.pbcStyle.DrawAction,
          this.pbcStyle.NewPen,
          this.pbcStyle.NewBrush,
          this.pbcStyle.NewFont, 
          this.pbcStyle.NewFontColor,
          new RectangleF(0, 0, 100, 100), 
          VGStyleGroup.None,
          this.pbcStyle.NewName,
          string.Empty);
      }
      else if (this.rdbEllipse.Checked)
      {
        element = new VGEllipse(
          this.pbcStyle.DrawAction,
          this.pbcStyle.NewPen,
          this.pbcStyle.NewBrush, 
          this.pbcStyle.NewFont, 
          this.pbcStyle.NewFontColor,
          new RectangleF(0, 0, 100, 100),
          VGStyleGroup.None, 
          this.pbcStyle.NewName,
          string.Empty);
      }
      else if (this.rdbPolyline.Checked)
      {
        element = new VGPolyline(
          this.pbcStyle.DrawAction,
          this.pbcStyle.NewPen,
          this.pbcStyle.NewBrush,
          this.pbcStyle.NewFont,
          this.pbcStyle.NewFontColor, 
          VGStyleGroup.None,
          this.pbcStyle.NewName,
          string.Empty);
      }
      else if (this.rdbSharp.Checked)
      {
        element = new VGSharp(
          this.pbcStyle.DrawAction,
          this.pbcStyle.NewPen, 
          this.pbcStyle.NewFont, 
          this.pbcStyle.NewFontColor,
          new RectangleF(0, 0, 100, 100), 
          VGStyleGroup.None,
          this.pbcStyle.NewName,
          string.Empty);
      }
      else if (this.rdbLine.Checked)
      {
        element = new VGLine(
          this.pbcStyle.DrawAction,
          this.pbcStyle.NewPen, 
          this.pbcStyle.NewFont, 
          this.pbcStyle.NewFontColor,
          VGStyleGroup.None, 
          this.pbcStyle.NewName,
          string.Empty);
      }

      element.Sound = this.audioControl.Sound;

      return element;
    }

    #endregion //METHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER
    #endregion //HELPER
  }
}