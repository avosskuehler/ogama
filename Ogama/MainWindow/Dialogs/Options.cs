// <copyright file="Options.cs" company="FU Berlin">
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

namespace Ogama.MainWindow.Dialogs
{
  using System;
  using System.Drawing;
  using System.Drawing.Drawing2D;
  using System.Windows.Forms;

  using Ogama.Modules.Common.PictureTemplates;
  using Ogama.Modules.Common.Tools;

  using OgamaControls;
  using OgamaControls.Dialogs;

  using VectorGraphics.Elements;

  /// <summary>
  /// This dialog <see cref="Form"/> is used to modify the
  /// application settings of OGAMA.
  /// </summary>
  public partial class Options : Form
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

    /// <summary>
    /// Stores Application settings
    /// </summary>
    private Properties.Settings curSettings;

    /// <summary>
    /// Color to use for new color dialogs.
    /// </summary>
    private Color setColor;

    /// <summary>
    /// DashStyle to use for new pen dialogs.
    /// </summary>
    private DashStyle setStyle;

    /// <summary>
    /// Width to use for new pens dialogs.
    /// </summary>
    private float setWidth;

    /// <summary>
    /// Font to use for new font select dialogs.
    /// </summary>
    private Font setFont;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the Options class.
    /// </summary>
    public Options()
    {
      this.InitializeComponent();
      this.curSettings = global::Ogama.Properties.Settings.Default;
      this.cbbGazeCursorType.Items.AddRange(Enum.GetNames(typeof(VGCursor.DrawingCursors)));
      this.cbbMouseCursorType.Items.AddRange(Enum.GetNames(typeof(VGCursor.DrawingCursors)));
      this.cbbGazeFixationsDisplayMode.Items.AddRange(Enum.GetNames(typeof(FixationDrawingMode)));
      this.cbbMouseFixationsDisplayMode.Items.AddRange(Enum.GetNames(typeof(FixationDrawingMode)));
      this.cbbPresentationMonitor.Items.AddRange(Enum.GetNames(typeof(Monitor)));
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES
    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler                                                              //
    ///////////////////////////////////////////////////////////////////////////////
    #region EVENTS

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler for UI, Menu, Buttons, Toolbars etc.                         //
    ///////////////////////////////////////////////////////////////////////////////
    #region WINDOWSEVENTHANDLER

    #region ReplayGazeOptions

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for
    /// the <see cref="Button"/> <see cref="btnGazeCursorStyle"/>
    /// Shows pen style selection dialog for the gaze cursor.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnGazeCursorStyle_Click(object sender, EventArgs e)
    {
      if (this.btnPenStyleClicked(this.psaGazeCursor, out this.setColor, out this.setStyle, out this.setWidth))
      {
        this.curSettings.GazeCursorColor = this.setColor;
        this.curSettings.GazeCursorStyle = this.setStyle;
        this.curSettings.GazeCursorWidth = this.setWidth;
      }
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for
    /// the <see cref="Button"/> <see cref="btnGazePathStyle"/>
    /// Shows pen style selection dialog for the gaze path.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnGazePathStyle_Click(object sender, EventArgs e)
    {
      if (this.btnPenStyleClicked(this.psaGazePath, out this.setColor, out this.setStyle, out this.setWidth))
      {
        this.curSettings.GazePathColor = this.setColor;
        this.curSettings.GazePathStyle = this.setStyle;
        this.curSettings.GazePathWidth = this.setWidth;
      }
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for
    /// the <see cref="Button"/> <see cref="btnGazeFixStyle"/>
    /// Shows pen style selection dialog for the gaze fixations.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnGazeFixStyle_Click(object sender, EventArgs e)
    {
      if (this.btnPenStyleClicked(this.psaGazeFix, out this.setColor, out this.setStyle, out this.setWidth))
      {
        this.curSettings.GazeFixationsPenColor = this.setColor;
        this.curSettings.GazeFixationsPenStyle = this.setStyle;
        this.curSettings.GazeFixationsPenWidth = this.setWidth;
      }
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for
    /// the <see cref="Button"/> <see cref="btnGazeFixConStyle"/>
    /// Shows pen style selection dialog for the gaze fixation connections.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnGazeFixConStyle_Click(object sender, EventArgs e)
    {
      if (this.btnPenStyleClicked(this.psaGazeFixCon, out this.setColor, out this.setStyle, out this.setWidth))
      {
        this.curSettings.GazeFixationConnectionsPenColor = this.setColor;
        this.curSettings.GazeFixationConnectionsPenStyle = this.setStyle;
        this.curSettings.GazeFixationConnectionsPenWidth = this.setWidth;
      }
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for
    /// the <see cref="Button"/> <see cref="btnGazeModeCursor"/>
    /// Switches checked state of the gaze mode "Cursor".
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnGazeModeCursor_Click(object sender, EventArgs e)
    {
      this.curSettings.GazeModeCursor = this.btnGazeModeCursor.Checked;
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for
    /// the <see cref="Button"/> <see cref="btnGazeModePath"/>
    /// Switches checked state of the gaze mode "Path".
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnGazeModePath_Click(object sender, EventArgs e)
    {
      this.curSettings.GazeModePath = this.btnGazeModePath.Checked;
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for
    /// the <see cref="Button"/> <see cref="btnGazeModeFix"/>
    /// Switches checked state of the gaze mode "Fixations".
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnGazeModeFix_Click(object sender, EventArgs e)
    {
      this.curSettings.GazeModeFixations = this.btnGazeModeFix.Checked;
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for
    /// the <see cref="Button"/> <see cref="btnGazeModeFixCon"/>
    /// Switches checked state of the gaze mode "FixationConnections".
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnGazeModeFixCon_Click(object sender, EventArgs e)
    {
      this.curSettings.GazeModeFixCon = this.btnGazeModeFixCon.Checked;
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for
    /// the <see cref="Button"/> <see cref="btnGazeModeSpot"/>
    /// Switches checked state of the gaze mode "Spotlight".
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnGazeModeSpot_Click(object sender, EventArgs e)
    {
      this.curSettings.GazeModeSpotlight = this.btnGazeModeSpot.Checked;
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for
    /// the <see cref="Button"/> <see cref="btnGazeCutPath"/>
    /// Switches checked state of the gaze mode "CutPath".
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnGazeCutPath_Click(object sender, EventArgs e)
    {
      this.curSettings.GazeModeCutPath = this.btnGazeCutPath.Checked;
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for
    /// the <see cref="Button"/> <see cref="btnGazeBlinks"/>
    /// Switches checked state of the gaze mode "Blinks".
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnGazeBlinks_Click(object sender, EventArgs e)
    {
      this.curSettings.GazeModeBlinks = this.btnGazeBlinks.Checked;
    }

    #endregion // ReplayGazeOptions

    #region ReplayMouseOptions

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for
    /// the <see cref="Button"/> <see cref="btnMouseCursorStyle"/>
    /// Shows pen style selection dialog for the mouse cursor.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnMouseCursorStyle_Click(object sender, EventArgs e)
    {
      if (this.btnPenStyleClicked(this.psaMouseCursor, out this.setColor, out this.setStyle, out this.setWidth))
      {
        this.curSettings.MouseCursorColor = this.setColor;
        this.curSettings.MouseCursorStyle = this.setStyle;
        this.curSettings.MouseCursorWidth = this.setWidth;
      }
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for
    /// the <see cref="Button"/> <see cref="btnMousePathStyle"/>
    /// Shows pen style selection dialog for the mouse path.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnMousePathStyle_Click(object sender, EventArgs e)
    {
      if (this.btnPenStyleClicked(this.psaMousePath, out this.setColor, out this.setStyle, out this.setWidth))
      {
        this.curSettings.MousePathColor = this.setColor;
        this.curSettings.MousePathStyle = this.setStyle;
        this.curSettings.MousePathWidth = this.setWidth;
      }
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for
    /// the <see cref="Button"/> <see cref="btnMouseFixStyle"/>
    /// Shows pen style selection dialog for the mouse fixations.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnMouseFixStyle_Click(object sender, EventArgs e)
    {
      if (this.btnPenStyleClicked(this.psaMouseFix, out this.setColor, out this.setStyle, out this.setWidth))
      {
        this.curSettings.MouseFixationsPenColor = this.setColor;
        this.curSettings.MouseFixationsPenStyle = this.setStyle;
        this.curSettings.MouseFixationsPenWidth = this.setWidth;
      }
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for
    /// the <see cref="Button"/> <see cref="btnMouseFixConStyle"/>
    /// Shows pen style selection dialog for the mouse fixation connections.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnMouseFixConStyle_Click(object sender, EventArgs e)
    {
      if (this.btnPenStyleClicked(this.psaMouseFixCon, out this.setColor, out this.setStyle, out this.setWidth))
      {
        this.curSettings.MouseFixationConnectionsPenColor = this.setColor;
        this.curSettings.MouseFixationConnectionsPenStyle = this.setStyle;
        this.curSettings.MouseFixationConnectionsPenWidth = this.setWidth;
      }
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for
    /// the <see cref="Button"/> <see cref="btnNoDataStyle"/>
    /// Shows pen style selection dialog for the lines
    /// that indicate no data connections.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnNoDataStyle_Click(object sender, EventArgs e)
    {
      if (this.btnPenStyleClicked(this.psaNoData, out this.setColor, out this.setStyle, out this.setWidth))
      {
        this.curSettings.GazeNoDataColor = this.setColor;
        this.curSettings.GazeNoDataStyle = this.setStyle;
        this.curSettings.GazeNoDataWidth = this.setWidth;
      }
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for
    /// the <see cref="Button"/> <see cref="btnMouseModeCursor"/>
    /// Switches checked state of the mouse mode "Cursor".
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnMouseModeCursor_Click(object sender, EventArgs e)
    {
      this.curSettings.MouseModeCursor = this.btnMouseCursorMode.Checked;
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for
    /// the <see cref="Button"/> <see cref="btnMouseModePath"/>
    /// Switches checked state of the mouse mode "Path".
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnMouseModePath_Click(object sender, EventArgs e)
    {
      this.curSettings.MouseModePath = this.btnMouseModePath.Checked;
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for
    /// the <see cref="Button"/> <see cref="btnMouseModeFix"/>
    /// Switches checked state of the mouse mode "Fixations".
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnMouseModeFix_Click(object sender, EventArgs e)
    {
      this.curSettings.MouseModeFixations = this.btnMouseModeFix.Checked;
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for
    /// the <see cref="Button"/> <see cref="btnMouseModeFixCon"/>
    /// Switches checked state of the mouse mode "FixationConnections".
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnMouseModeFixCon_Click(object sender, EventArgs e)
    {
      this.curSettings.MouseModeFixCon = this.btnMouseModeFixCon.Checked;
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for
    /// the <see cref="Button"/> <see cref="btnMouseModeSpot"/>
    /// Switches checked state of the mouse mode "CutPath".
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnMouseModeSpot_Click(object sender, EventArgs e)
    {
      this.curSettings.MouseModeSpotlight = this.btnMouseModeSpot.Checked;
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for
    /// the <see cref="Button"/> <see cref="btnMouseCutPath"/>
    /// Switches checked state of the mouse mode "CutPath".
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnMouseCutPath_Click(object sender, EventArgs e)
    {
      this.curSettings.MouseModeCutPath = this.btnMouseCutPath.Checked;
    }

    #endregion //ReplayMouseOptions

    #region FixationsOptions

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for
    /// the <see cref="Button"/> <see cref="btnGazeCursorStyle"/>
    /// Shows pen style selection dialog for the gaze fixations.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnGazeFixationsPenStyle_Click(object sender, EventArgs e)
    {
      if (this.btnPenStyleClicked(this.psaGazeFixationsPenStyle, out this.setColor, out this.setStyle, out this.setWidth))
      {
        this.curSettings.GazeFixationsPenColor = this.setColor;
        this.curSettings.GazeFixationsPenStyle = this.setStyle;
        this.curSettings.GazeFixationsPenWidth = this.setWidth;
      }
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for
    /// the <see cref="Button"/> <see cref="btnGazeFixationsFontStyle"/>
    /// Shows font style selection dialog for the gaze fixations.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnGazeFixationsFontStyle_Click(object sender, EventArgs e)
    {
      if (this.btnFontStyleClicked(this.fsaGazeFixationsFont, out this.setColor, out this.setFont))
      {
        this.curSettings.GazeFixationsFontColor = this.setColor;
        this.curSettings.GazeFixationsFont = (Font)this.setFont.Clone();
      }
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for
    /// the <see cref="Button"/> <see cref="btnGazeCursorStyle"/>
    /// Shows pen style selection dialog for the mouse fixations.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnMouseFixationsPenStyle_Click(object sender, EventArgs e)
    {
      if (this.btnPenStyleClicked(this.psaMouseFixationsPenStyle, out this.setColor, out this.setStyle, out this.setWidth))
      {
        this.curSettings.MouseFixationsPenColor = this.setColor;
        this.curSettings.MouseFixationsPenStyle = this.setStyle;
        this.curSettings.MouseFixationsPenWidth = this.setWidth;
      }
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for
    /// the <see cref="Button"/> <see cref="btnMouseFixationsFontStyle"/>
    /// Shows font style selection dialog for the mouse fixations.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnMouseFixationsFontStyle_Click(object sender, EventArgs e)
    {
      if (this.btnFontStyleClicked(this.fsaMouseFixationsFont, out this.setColor, out this.setFont))
      {
        this.curSettings.MouseFixationsFontColor = this.setColor;
        this.curSettings.MouseFixationsFont = this.setFont;
      }
    }

    #endregion //FixationsOptions

    #region AOIOptions

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for
    /// the <see cref="Button"/> <see cref="btnAOIStandardStyle"/>
    /// Shows pen style selection dialog for the default AOI.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnAOIStandardStyle_Click(object sender, EventArgs e)
    {
      if (this.btnPenStyleClicked(this.psaAOIStandard, out this.setColor, out this.setStyle, out this.setWidth))
      {
        this.curSettings.AOIStandardColor = this.setColor;
        this.curSettings.AOIStandardStyle = this.setStyle;
        this.curSettings.AOIStandardWidth = this.setWidth;
      }
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for
    /// the <see cref="Button"/> <see cref="btnAOITargetStyle"/>
    /// Shows pen style selection dialog for the AOI "Target".
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnAOITargetStyle_Click(object sender, EventArgs e)
    {
      if (this.btnPenStyleClicked(this.psaAOITarget, out this.setColor, out this.setStyle, out this.setWidth))
      {
        this.curSettings.AOITargetColor = this.setColor;
        this.curSettings.AOITargetStyle = this.setStyle;
        this.curSettings.AOITargetWidth = this.setWidth;
      }
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for
    /// the <see cref="Button"/> <see cref="btnAOISearchRectStyle"/>
    /// Shows pen style selection dialog for the AOI "SearchRect".
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnAOISearchRectStyle_Click(object sender, EventArgs e)
    {
      if (this.btnPenStyleClicked(this.psaAOISearchRect, out this.setColor, out this.setStyle, out this.setWidth))
      {
        this.curSettings.AOISearchRectColor = this.setColor;
        this.curSettings.AOISearchRectStyle = this.setStyle;
        this.curSettings.AOISearchRectWidth = this.setWidth;
      }
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for
    /// the <see cref="Button"/> <see cref="btnAOIStandardFont"/>
    /// Shows font style selection dialog for the default AOI.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnAOIDefaultFont_Click(object sender, EventArgs e)
    {
      if (this.btnFontStyleClicked(this.fsaAOIDefault, out this.setColor, out this.setFont))
      {
        this.curSettings.AOIStandardFontColor = this.setColor;
        this.curSettings.AOIStandardFont = this.setFont;
      }
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for
    /// the <see cref="Button"/> <see cref="btnAOITargetFont"/>
    /// Shows font style selection dialog for the AOI "Target".
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnAOITargetFont_Click(object sender, EventArgs e)
    {
      if (this.btnFontStyleClicked(this.fsaAOITarget, out this.setColor, out this.setFont))
      {
        this.curSettings.AOITargetFontColor = this.setColor;
        this.curSettings.AOITargetFont = this.setFont;
      }
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for
    /// the <see cref="Button"/> <see cref="btnAOISearchRectFont"/>
    /// Shows font style selection dialog for the AOI "SearchRect".
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnAOISearchRectFont_Click(object sender, EventArgs e)
    {
      if (this.btnFontStyleClicked(this.fsaAOISearchRect, out this.setColor, out this.setFont))
      {
        this.curSettings.AOISearchRectFontColor = this.setColor;
        this.curSettings.AOISearchRectFont = this.setFont;
      }
    }

    #endregion //AOIOptions

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for
    /// the <see cref="Button"/> <see cref="btnOK"/>
    /// Saves the modified settings into the applications settings
    /// file.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnOK_Click(object sender, EventArgs e)
    {
      this.Cursor = Cursors.WaitCursor;
      this.curSettings.Save();
      this.Cursor = Cursors.Default;
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for
    /// the <see cref="Button"/> <see cref="btnCancel"/>
    /// Cancels the saving of the application settings and
    /// reloads the old ones from last saving,
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnCancel_Click(object sender, EventArgs e)
    {
      this.Cursor = Cursors.WaitCursor;
      this.curSettings.Reload();
      this.Cursor = Cursors.Default;
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
    /// Opens <see cref="PenStyleDlg"/> and updates application settings and 
    /// given pen style area when new pen style is choosen.
    /// </summary>
    /// <param name="psa">A <see cref="PenStyleArea"/> that visualizes the changed pen.</param>
    /// <param name="colorToSet">Out. An Application Setting Pen <see cref="Color"/>.</param>
    /// <param name="styleToSet">Out. An Application Setting Pen <see cref="DashStyle"/>.</param>
    /// <param name="widthToSet">Out. An <see cref="Single"/> with the Application Setting Pen Width</param>
    /// <returns><strong>True</strong>, if style should be applied to settings,
    /// otherwise <strong>false</strong> (User cancelled dialog)</returns>
    private bool btnPenStyleClicked(
      PenStyleArea psa,
      out Color colorToSet,
      out DashStyle styleToSet,
      out float widthToSet)
    {
      PenStyleDlg dlgStyle = new PenStyleDlg();
      dlgStyle.Text = "Set pen style...";
      dlgStyle.Pen = psa.Pen;
      dlgStyle.Icon = Properties.Resources.RPLCursorPenIcon;
      if (dlgStyle.ShowDialog() == DialogResult.OK)
      {
        psa.Pen = dlgStyle.Pen;
        psa.Refresh();
        colorToSet = dlgStyle.Pen.Color;
        styleToSet = dlgStyle.Pen.DashStyle;
        widthToSet = dlgStyle.Pen.Width;
        return true;
      }
      else
      {
        colorToSet = Color.Empty;
        styleToSet = DashStyle.Custom;
        widthToSet = 0;
        return false;
      }
    }

    /// <summary>
    /// Opens FontStyle Dialog and updates application settings and 
    /// font style area when new font style is choosen.
    /// </summary>
    /// <param name="fsa">A <see cref="FontStyleArea"/> that visualizes the changed font.</param>
    /// <param name="colorToSet">Out. An Application Setting Font <see cref="Color"/>.</param>
    /// <param name="fontToSet">Out. An Application Setting <see cref="Font"/>.</param>
    /// <returns><strong>True</strong>, if style should be applied to settings,
    /// otherwise <strong>false</strong> (User cancelled dialog)</returns>
    private bool btnFontStyleClicked(
      FontStyleArea fsa,
      out Color colorToSet,
      out Font fontToSet)
    {
      FontStyleDlg dlgStyle = new FontStyleDlg();
      dlgStyle.Text = "Set font style...";
      dlgStyle.CurrentFont = fsa.Font;
      dlgStyle.CurrentFontColor = fsa.FontColor;
      dlgStyle.Icon = Properties.Resources.AOIFontIcon;
      if (dlgStyle.ShowDialog() == DialogResult.OK)
      {
        fsa.Font = dlgStyle.CurrentFont;
        fsa.FontColor = dlgStyle.CurrentFontColor;
        fsa.Refresh();
        colorToSet = dlgStyle.CurrentFontColor;
        fontToSet = dlgStyle.CurrentFont;
        return true;
      }
      else
      {
        colorToSet = Color.Empty;
        fontToSet = SystemInformation.MenuFont;
        return false;
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