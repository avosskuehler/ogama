// <copyright file="ColorDefinitionDialog.cs" company="FU Berlin">
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

namespace Ogama.Modules.Scanpath.Colorization
{
  using System;
  using System.Collections.Generic;
  using System.Drawing;
  using System.Drawing.Drawing2D;
  using System.Drawing.Imaging;
  using System.Windows.Forms;

  using Ogama.Modules.AttentionMap;
  using Ogama.Modules.Common.PictureTemplates;
  using Ogama.Modules.Common.Types;

  using OgamaControls;
  using OgamaControls.Dialogs;

  /// <summary>
  /// This dialog is designed to edit colorization parameters for subjects.
  /// In different <see cref="ColorizationModes"/> the visual differentiation between
  /// the fixational data of subjects is improved.
  /// </summary>
  public partial class ColorDefinitionDialog : Form
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
    /// The currently used <see cref="ColorizationParameters"/>
    /// </summary>
    private ColorizationParameters colorParams;

    /// <summary>
    /// Predefined rainbow gradient
    /// </summary>
    private Gradient rainbowGradient;

    /// <summary>
    /// Predefined traffic light gradient
    /// </summary>
    private Gradient trafficLightGradient;

    /// <summary>
    /// Custom gradient
    /// </summary>
    private Gradient customGradient;

    /// <summary>
    /// Bitmap to fill with gradient to grab single color values.
    /// </summary>
    private Bitmap colorMap;

    /// <summary>
    /// The <see cref="FixationDrawingMode"/> to use for the preview.
    /// </summary>
    private FixationDrawingMode currentDrawingMode;

    /// <summary>
    /// The font of the numbers of the fixations for the current selected subject.
    /// </summary>
    private Font selectedFont;

    /// <summary>
    /// The color of the numbers of the fixations for the current selected subject.
    /// </summary>
    private Color selectedFontColor;

    /// <summary>
    /// The pen for the fixations for the current selected subject.
    /// </summary>
    private Pen selectedFixationsPen;

    /// <summary>
    /// The pen for the fixation connections for the current selected subject.
    /// </summary>
    private Pen selectedConnectionsPen;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the ColorDefinitionDialog class.
    /// Initializes components and gradients.
    /// </summary>
    /// <param name="drawingMode">The used <see cref="FixationDrawingMode"/> for the preview.</param>
    public ColorDefinitionDialog(FixationDrawingMode drawingMode)
    {
      this.InitializeComponent();
      this.currentDrawingMode = drawingMode;
      this.InitializeGradients();
      Ogama.Properties.Settings set = Properties.Settings.Default;
      this.selectedFont = (Font)set.GazeFixationsFont.Clone();
      this.selectedFontColor = set.GazeFixationsFontColor;
      this.selectedFixationsPen = new Pen(set.GazeFixationsPenColor, set.GazeFixationsPenWidth);
      this.selectedFixationsPen.DashStyle = set.GazeFixationsPenStyle;
      this.selectedConnectionsPen = new Pen(set.GazeFixationConnectionsPenColor, set.GazeFixationConnectionsPenWidth);
      this.selectedFixationsPen.DashStyle = set.GazeFixationConnectionsPenStyle;
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Sets the subjects <see cref="TreeNode"/>s of this dialog.
    /// </summary>
    public TreeNodeCollection SubjectNodes
    {
      set
      {
        foreach (TreeNode node in value)
        {
          this.trvSubjects.Nodes.Add((TreeNode)node.Clone());
        }
      }
    }

    /// <summary>
    /// Gets or sets the <see cref="ColorizationParameters"/> used
    /// for displaying the subjects.
    /// </summary>
    public ColorizationParameters ColorParams
    {
      get
      {
        return this.colorParams;
      }

      set
      {
        this.colorParams = value;
        this.colorMap = new Bitmap(this.colorParams.SubjectStyles.Count, 1, PixelFormat.Format32bppArgb);
        if (this.colorParams.ColorizationGradient == null)
        {
          this.colorParams.ColorizationGradient = this.customGradient;
        }

        this.gradientControl.Gradient = this.colorParams.ColorizationGradient;
        this.DrawGradientToColorMap(this.colorParams.ColorizationGradient);

        switch (this.colorParams.ColorizationMode)
        {
          case ColorizationModes.Subject:
            this.rdbColorSubjects.Checked = true;
            break;
          case ColorizationModes.Category:
            this.rdbColorGroups.Checked = true;
            break;
          case ColorizationModes.Gradient:
            this.rdbColorAutomatic.Checked = true;
            this.AssignGradientToSubjects();
            break;
        }

        this.trvSubjects.ExpandAll();
        this.SetColorizationMode();
      }
    }

    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Public methods                                                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region PUBLICMETHODS

    /// <summary>
    /// This static method draws a custom tree node in the given bounds
    /// with the given colorParams.
    /// </summary>
    /// <param name="e">The <see cref="DrawTreeNodeEventArgs"/> with the <see cref="TreeNode"/>
    /// to draw.</param>
    /// <param name="colorParams">A <see cref="ColorizationParameters"/> with the colors and pens to use.</param>
    /// <param name="colorRectBounds">The <see cref="Rectangle"/> with the bounds of the color rect.</param>
    public static void DrawNodes(DrawTreeNodeEventArgs e, ColorizationParameters colorParams, Rectangle colorRectBounds)
    {
      colorRectBounds.Width = 20;
      colorRectBounds.Height -= 1;
      colorRectBounds.Inflate(-2, -2);

      if (e.Node.Level == 0)
      {
        if (colorParams.ColorizationMode == ColorizationModes.Category)
        {
          Pen boundsPen = (Pen)Pens.Red.Clone();
          if (colorParams.CategoryStyles.ContainsKey(e.Node.Text))
          {
            boundsPen = (Pen)colorParams.CategoryStyles[e.Node.Text].ConnectionPen.Clone();
          }

          boundsPen.Width = 1;
          e.Graphics.DrawRectangle(boundsPen, colorRectBounds);
        }
      }
      else
      {
        if (colorParams.ColorizationMode != ColorizationModes.Category)
        {
          Pen boundsPen = (Pen)Pens.Red.Clone();
          if (colorParams.SubjectStyles.ContainsKey(e.Node.Text))
          {
            boundsPen = (Pen)colorParams.SubjectStyles[e.Node.Text].ConnectionPen.Clone();
          }

          boundsPen.Width = 1;
          e.Graphics.DrawRectangle(boundsPen, colorRectBounds);
        }
      }

      colorRectBounds.Inflate(-2, -2);
      colorRectBounds.Width += 1;
      colorRectBounds.Height += 1;

      Color usedColor = Color.Transparent;
      if (e.Node.Level == 0)
      {
        if (colorParams.ColorizationMode == ColorizationModes.Category)
        {
          if (colorParams.CategoryStyles.ContainsKey(e.Node.Text))
          {
            usedColor = colorParams.CategoryStyles[e.Node.Text].FixationPen.Color;
          }
        }
      }
      else
      {
        if (colorParams.ColorizationMode != ColorizationModes.Category)
        {
          if (colorParams.SubjectStyles.ContainsKey(e.Node.Text))
          {
            usedColor = colorParams.SubjectStyles[e.Node.Text].FixationPen.Color;
          }
        }
      }

      e.Graphics.FillRectangle(new SolidBrush(usedColor), colorRectBounds);
    }

    #endregion //PUBLICMETHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler                                                              //
    ///////////////////////////////////////////////////////////////////////////////
    #region EVENTS

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler for UI, Menu, Buttons, Toolbars etc.                         //
    ///////////////////////////////////////////////////////////////////////////////
    #region WINDOWSEVENTHANDLER

    /// <summary>
    /// The <see cref="TreeView.DrawNode"/> event handler for
    /// the <see cref="TreeView"/> <see cref="trvSubjects"/>.
    /// Implements custom TreeView node drawing by adding colored rectangles
    /// to the right of the node text indicating the pens and colors for the 
    /// current subject.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A <see cref="DrawTreeNodeEventArgs"/> with the event data.</param>
    private void trvSubjects_DrawNode(object sender, DrawTreeNodeEventArgs e)
    {
      e.DrawDefault = true;

      Rectangle colorBounds = e.Bounds;
      colorBounds.Offset(colorBounds.Width, 0);
      DrawNodes(e, this.colorParams, colorBounds);
    }

    /// <summary>
    /// The <see cref="RadioButton.CheckedChanged"/> event handler for
    /// the colorization <see cref="RadioButton"/>s.
    /// Updates the user interface with the new settings.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void rdbColorization_CheckedChanged(object sender, EventArgs e)
    {
      this.SetColorizationMode();
      this.pnlPreview.Refresh();
      this.trvSubjects.Refresh();
    }

    /// <summary>
    /// The <see cref="TreeView.AfterSelect"/> event handler for
    /// the <see cref="TreeView"/> <see cref="trvSubjects"/>.
    /// Populates the user interface with the color properties of the
    /// selected subject.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A <see cref="TreeViewEventArgs"/> with the event data.</param>
    private void trvSubjects_AfterSelect(object sender, TreeViewEventArgs e)
    {
      switch (this.colorParams.ColorizationMode)
      {
        case ColorizationModes.Subject:
          if (e.Node.Level == 1)
          {
            if (this.colorParams.SubjectStyles.ContainsKey(e.Node.Text))
            {
              this.PopulateStyleGroup(this.colorParams.SubjectStyles[e.Node.Text]);
            }
          }

          break;
        case ColorizationModes.Category:
          if (e.Node.Level == 0)
          {
            if (this.colorParams.CategoryStyles.ContainsKey(e.Node.Text))
            {
              this.PopulateStyleGroup(this.colorParams.CategoryStyles[e.Node.Text]);
            }
          }

          break;
        case ColorizationModes.Gradient:
          break;
      }
    }

    /// <summary>
    /// The <see cref="ComboBox.SelectionChangeCommitted"/> event handler for
    /// the <see cref="ComboBox"/> <see cref="cbbPredefinedGradient"/>.
    /// Updates the user interface with the new settings.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void cbbPredefinedGradient_SelectionChangeCommitted(object sender, EventArgs e)
    {
      switch (this.cbbPredefinedGradient.SelectedItem.ToString())
      {
        case "Custom":
        default:
          this.DrawGradientToColorMap(this.gradientControl.Gradient);
          this.AssignGradientToSubjects();
          break;
        case "Traffic Light":
          this.gradientControl.Gradient = this.trafficLightGradient;
          this.DrawGradientToColorMap(this.gradientControl.Gradient);
          this.AssignGradientToSubjects();
          break;
        case "Rainbow":
          this.gradientControl.Gradient = this.rainbowGradient;
          this.DrawGradientToColorMap(this.gradientControl.Gradient);
          this.AssignGradientToSubjects();
          break;
      }
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for
    /// the <see cref="Button"/> <see cref="btnFixationsStyle"/>.
    /// Updates the colorization property of the selected subject or group
    /// with the changes made in the <see cref="PenStyleDlg"/> for
    /// the fixation pen.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnFixationsStyle_Click(object sender, EventArgs e)
    {
      if (this.trvSubjects.SelectedNode != null)
      {
        PenStyleDlg dlg = new PenStyleDlg();
        dlg.Pen = this.selectedFixationsPen;
        if (dlg.ShowDialog() == DialogResult.OK)
        {
          this.selectedFixationsPen = dlg.Pen;
          this.SubmitStyleToSubjectOrCategory();
        }
      }
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for
    /// the <see cref="Button"/> <see cref="btnConnectionsStyle"/>.
    /// Updates the colorization property of the selected subject or group
    /// with the changes made in the <see cref="PenStyleDlg"/> for
    /// the fixation connection pen.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnConnectionsStyle_Click(object sender, EventArgs e)
    {
      if (this.trvSubjects.SelectedNode != null)
      {
        PenStyleDlg dlg = new PenStyleDlg();
        dlg.Pen = this.selectedConnectionsPen;
        if (dlg.ShowDialog() == DialogResult.OK)
        {
          this.selectedConnectionsPen = dlg.Pen;
          this.SubmitStyleToSubjectOrCategory();
        }
      }
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for
    /// the <see cref="Button"/> <see cref="btnNumbersStyle"/>.
    /// Updates the colorization property of the selected subject or group
    /// with the changes made in the <see cref="PenStyleDlg"/> for
    /// the fixation number font.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnNumbersStyle_Click(object sender, EventArgs e)
    {
      if (this.trvSubjects.SelectedNode != null)
      {
        FontStyleDlg dlg = new FontStyleDlg();
        dlg.CurrentFontColor = this.selectedFontColor;
        dlg.CurrentFont = this.selectedFont;
        if (dlg.ShowDialog() == DialogResult.OK)
        {
          this.selectedFontColor = dlg.CurrentFontColor;
          this.selectedFont = dlg.CurrentFont;
          this.SubmitStyleToSubjectOrCategory();
        }
      }
    }

    /// <summary>
    /// The <see cref="Control.Paint"/> event handler for
    /// the <see cref="Panel"/> <see cref="pnlPreview"/>.
    /// Updates the preview of the fixation style
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A <see cref="PaintEventArgs"/> with the event data.</param>
    private void pnlPreview_Paint(object sender, PaintEventArgs e)
    {
      bool useDots = this.currentDrawingMode == FixationDrawingMode.Dots;

      if (useDots)
      {
        SolidBrush dotBrush = new SolidBrush(this.selectedFixationsPen.Color);
        e.Graphics.FillEllipse(dotBrush, 34, 34, 12, 12);
        e.Graphics.FillEllipse(dotBrush, 144, 54, 12, 12);
        e.Graphics.FillEllipse(dotBrush, 274, 24, 12, 12);
      }
      else
      {
        e.Graphics.DrawEllipse(this.selectedFixationsPen, 10, 10, 60, 60);
        e.Graphics.DrawEllipse(this.selectedFixationsPen, 90, 0, 120, 120);
        e.Graphics.DrawEllipse(this.selectedFixationsPen, 260, 10, 40, 40);
      }

      e.Graphics.DrawString("1", this.selectedFont, new SolidBrush(this.selectedFontColor), new Point(25, 20));
      e.Graphics.DrawLine(this.selectedConnectionsPen, 40, 40, 150, 60);
      e.Graphics.DrawString("2", this.selectedFont, new SolidBrush(this.selectedFontColor), new Point(135, 40));
      e.Graphics.DrawLine(this.selectedConnectionsPen, 150, 60, 280, 30);
      e.Graphics.DrawString("3", this.selectedFont, new SolidBrush(this.selectedFontColor), new Point(265, 10));
    }

    #endregion //WINDOWSEVENTHANDLER

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler for Custom Defined Events                                    //
    ///////////////////////////////////////////////////////////////////////////////
    #region CUSTOMEVENTHANDLER

    /// <summary>
    /// The <see cref="GradientTypeEditorUI.GradientChanged"/> event handler for
    /// the <see cref="GradientTypeEditorUI"/> <see cref="gradientControl"/>.
    /// Updates the UI.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void gradientControl_GradientChanged(object sender, EventArgs e)
    {
      this.DrawGradientToColorMap(this.gradientControl.Gradient);
      if (this.colorParams != null &&
        this.colorParams.ColorizationMode == ColorizationModes.Gradient)
      {
        this.colorParams.ColorizationGradient = this.gradientControl.Gradient;
        this.AssignGradientToSubjects();
        this.trvSubjects.Refresh();
      }
    }

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
    /// This method submits the given <see cref="ColorizationStyle"/>
    /// to the user interface toolbox items.
    /// </summary>
    /// <param name="style">The <see cref="ColorizationStyle"/> to be edited in the UI.</param>
    private void PopulateStyleGroup(ColorizationStyle style)
    {
      this.selectedConnectionsPen = style.ConnectionPen;
      this.selectedFixationsPen = style.FixationPen;
      this.selectedFont = style.Font;
      this.selectedFontColor = style.FontColor;
      this.pnlPreview.Refresh();
    }

    /// <summary>
    /// Initialize gradients.
    /// </summary>
    private void InitializeGradients()
    {
      this.customGradient = new Gradient();
      ColorBlend customBlend = new ColorBlend(2);
      customBlend.Colors = new Color[2] 
      { 
        Color.FromArgb(255, 0, 255, 0),
        Color.FromArgb(255, 0, 0, 255)
      };
      customBlend.Positions = new float[2] { 0.0f, 1.0f };
      this.customGradient.ColorBlend = customBlend;
      this.customGradient.GradientDirection = LinearGradientMode.Horizontal;

      this.trafficLightGradient = new Gradient();
      ColorBlend trafficLightBlend = new ColorBlend(3);
      trafficLightBlend.Colors = new Color[3] 
      {
        Color.FromArgb(255, 0, 255, 0),
        Color.FromArgb(255, 255, 255, 0),
        Color.FromArgb(255, 255, 0, 0)
      };
      trafficLightBlend.Positions = new float[3] { 0.0f, 0.6f, 1.0f };
      this.trafficLightGradient.ColorBlend = trafficLightBlend;
      this.trafficLightGradient.GradientDirection = LinearGradientMode.Horizontal;

      this.rainbowGradient = new Gradient();
      ColorBlend rainbowBlend = new ColorBlend(7);
      rainbowBlend.Colors = new Color[7] 
      { 
        Color.FromArgb(255, 128, 0, 128),
        Color.FromArgb(255, 0, 0, 255),
        Color.FromArgb(255, 0, 255, 255),
        Color.FromArgb(255, 0, 255, 0),
        Color.FromArgb(255, 255, 255, 0),
        Color.FromArgb(255, 255, 128, 0),
        Color.FromArgb(255, 255, 0, 0)
      };
      rainbowBlend.Positions = new float[7] { 0.0f, 0.25f, 0.4f, 0.65f, 0.75f, 0.93f, 1.0f };
      this.rainbowGradient.ColorBlend = rainbowBlend;
      this.rainbowGradient.GradientDirection = LinearGradientMode.Horizontal;
    }

    /// <summary>
    /// This method draws the new gradient into the cache bitmap
    /// to have a bitmap for parsing single colors out if the gradient.
    /// </summary>
    /// <param name="gradient">The <see cref="Gradient"/> to be used.</param>
    private void DrawGradientToColorMap(Gradient gradient)
    {
      // Cache the gradient by painting it onto a bitmap
      using (Graphics bitmapGraphics = Graphics.FromImage(this.colorMap))
      {
        Rectangle bmpRct = new Rectangle(0, 0, this.colorMap.Width, this.colorMap.Height);
        gradient.PaintGradientWithDirectionOverride(
          bitmapGraphics,
          bmpRct,
          LinearGradientMode.Horizontal);
      }
    }

    /// <summary>
    /// This method updates the <see cref="ColorizationStyle"/>
    /// of the selected subject or group.
    /// </summary>
    private void SubmitStyleToSubjectOrCategory()
    {
      ColorizationStyle newStyle = new ColorizationStyle(
        this.selectedFixationsPen,
        this.selectedConnectionsPen,
        this.selectedFont,
        this.selectedFontColor);

      switch (this.colorParams.ColorizationMode)
      {
        case ColorizationModes.Subject:
          if (this.trvSubjects.SelectedNode.Level == 1)
          {
            this.colorParams.SubjectStyles[this.trvSubjects.SelectedNode.Text] = newStyle;
          }

          break;
        case ColorizationModes.Category:
          if (this.trvSubjects.SelectedNode.Level == 0)
          {
            this.colorParams.CategoryStyles[this.trvSubjects.SelectedNode.Text] = newStyle;
            this.AssignGroupToSubjects();
          }

          break;
        case ColorizationModes.Gradient:
          if (this.trvSubjects.SelectedNode.Level == 1)
          {
            this.colorParams.SubjectStyles[this.trvSubjects.SelectedNode.Text] = newStyle;
          }

          break;
      }

      this.trvSubjects.Refresh();
      this.pnlPreview.Refresh();
    }

    /// <summary>
    /// This method sets the <see cref="ColorizationModes"/> according 
    /// to the UI setting.
    /// </summary>
    private void SetColorizationMode()
    {
      if (this.rdbColorSubjects.Checked)
      {
        this.colorParams.ColorizationMode = ColorizationModes.Subject;
        this.spcGradientSingle.Panel1Collapsed = true;
      }
      else if (this.rdbColorGroups.Checked)
      {
        this.colorParams.ColorizationMode = ColorizationModes.Category;
        this.spcGradientSingle.Panel1Collapsed = true;
        this.AssignGroupToSubjects();
      }
      else if (this.rdbColorAutomatic.Checked)
      {
        this.colorParams.ColorizationMode = ColorizationModes.Gradient;
        this.spcGradientSingle.Panel2Collapsed = true;
        this.AssignGradientToSubjects();
      }
    }

    /// <summary>
    /// Set the color style for the subjects according to categroy styles.
    /// </summary>
    private void AssignGroupToSubjects()
    {
      foreach (TreeNode node in this.trvSubjects.Nodes)
      {
        foreach (TreeNode subnode in node.Nodes)
        {
          if (this.colorParams.CategoryStyles.ContainsKey(node.Text))
          {
            this.colorParams.SubjectStyles[subnode.Text] = this.colorParams.CategoryStyles[node.Text];
          }
        }
      }
    }

    /// <summary>
    /// Populates the drawing colors for all subjects using
    /// the current gradient.
    /// </summary>
    private void AssignGradientToSubjects()
    {
      PaletteBitmap palBmp = new PaletteBitmap(this.colorMap);
      int i = 0;
      XMLSerializableDictionary<string, ColorizationStyle> newStyles =
        new XMLSerializableDictionary<string, ColorizationStyle>();
      foreach (KeyValuePair<string, ColorizationStyle> kvp in this.colorParams.SubjectStyles)
      {
        Color newColor = palBmp.GetPixel(i, 0);
        Pen newFixationPen = (Pen)this.selectedFixationsPen.Clone();
        newFixationPen.Color = newColor;
        Pen newConnectionPen = (Pen)this.selectedConnectionsPen.Clone();
        newConnectionPen.Color = newColor;
        Font newFont = (Font)this.selectedFont.Clone();
        newStyles.Add(kvp.Key, new ColorizationStyle(newFixationPen, newConnectionPen, newFont, newColor));
        i++;
      }

      this.colorParams.SubjectStyles = newStyles;
    }

    #endregion //METHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER
    #endregion //HELPER
  }
}