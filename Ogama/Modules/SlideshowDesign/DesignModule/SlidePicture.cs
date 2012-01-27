// <copyright file="SlidePicture.cs" company="FU Berlin">
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
  using System.ComponentModel;
  using System.Drawing;
  using System.Drawing.Drawing2D;
  using System.Windows.Forms;

  using VectorGraphics.Canvas;
  using VectorGraphics.Elements;
  using VectorGraphics.Tools.CustomEventArgs;

  /// <summary>
  /// A <see cref="Picture"/> derived from specialized
  /// <see cref="PictureModifiable"/> to display stimuli
  /// along with target shapes.
  /// </summary>
  public partial class SlidePicture : PictureModifiable
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
    /// Initializes a new instance of the SlidePicture class.
    /// </summary>
    public SlidePicture()
    {
      this.InitializeComponent();
    }

    /// <summary>
    /// Initializes a new instance of the SlidePicture class.
    /// </summary>
    /// <param name="container">owning container</param>
    public SlidePicture(IContainer container)
    {
      container.Add(this);

      this.InitializeComponent();
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES
    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Public methods                                                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region PUBLICMETHODS

    /// <summary>
    /// Starts new target shape creation by starting
    /// one of the pictures creation methods depending on 
    /// current shape.
    /// </summary>
    /// <param name="type">A <see cref="VGShapeType"/> with the type of shape to create
    /// for target.</param>
    public void NewTargetShape(VGShapeType type)
    {
      int count = this.GetNextTargetNumber();

      switch (type)
      {
        case VGShapeType.Rectangle:
          this.NewRectangleStart(
            ShapeDrawAction.NameAndEdge,
            this.TargetPen,
            new SolidBrush(this.TargetPen.Color),
            this.TargetFont,
            this.TargetFontColor,
            VGStyleGroup.AOI_TARGET,
            "Target" + count.ToString());

          break;
        case VGShapeType.Ellipse:
          this.NewEllipseStart(
            ShapeDrawAction.NameAndEdge,
            this.TargetPen,
            new SolidBrush(this.TargetPen.Color),
            this.TargetFont,
            this.TargetFontColor,
            VGStyleGroup.AOI_TARGET,
            "Target" + count.ToString());

          break;
        case VGShapeType.Polyline:
          this.NewPolylineStart(
            ShapeDrawAction.NameAndEdge,
            this.TargetPen,
            new SolidBrush(this.TargetPen.Color),
            this.TargetFont,
            this.TargetFontColor,
            VGStyleGroup.AOI_TARGET,
            "Target" + count.ToString());

          break;
      }
    }

    #endregion //PUBLICMETHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Inherited methods                                                         //
    ///////////////////////////////////////////////////////////////////////////////
    #region OVERRIDES

    /// <summary>
    /// Overridden. This custom on paste invokes a dialog that switches
    /// between target and default stimulus element insertion.
    /// </summary>
    public override void OnPaste()
    {
      // Retrieves the data from the clipboard.
      object test = Clipboard.GetData(DataFormats.StringFormat);
      if (test == null)
      {
        return;
      }

      VGElement t = VGElement.Deserialize(test.ToString());

      // Determines whether the data is in a format you can use.
      if (t != null)
      {
        VGElement elementToAdd = t;
        this.ResetSelectedElement();

        PasteAsDialog dialog = new PasteAsDialog();
        dialog.ElementToPaste = elementToAdd;

        if (dialog.ShowDialog() == DialogResult.OK)
        {
          if (!this.Elements.Contains(elementToAdd))
          {
            if (dialog.IsTarget)
            {
              int count = this.GetNextTargetNumber();
              elementToAdd.Name = "Target" + count.ToString();
              elementToAdd.StyleGroup = VGStyleGroup.AOI_TARGET;
              elementToAdd.Pen = this.TargetPen;
              elementToAdd.Font = this.TargetFont;
              elementToAdd.FontColor = this.TargetFontColor;
              elementToAdd.TextAlignment = this.TargetTextAlignment;
            }
            else
            {
              elementToAdd.StyleGroup = VGStyleGroup.None;
            }
          }

          this.Elements.Add(elementToAdd);
          this.SelectedElement = elementToAdd;

          // New Graphic element created, so notify listeners.
          this.OnShapeAdded(new ShapeEventArgs(elementToAdd));
          this.DrawForeground(false);
        }
      }
    }

    /// <summary>
    /// Overridden. Initializes the drawing elements of this <see cref="SlidePicture"/>
    /// by calling the <c>Properties.Settings.Default</c> values with
    /// <see cref="PictureModifiable.InitializeElements(Pen,Pen,Pen,Pen,Font,Color,Font,Color,Font,Color, VGAlignment, VGAlignment, VGAlignment)"/>.
    /// </summary>
    protected override void InitializePictureDefaultElements()
    {
      // AppSettings are always set except when in design mode
      if (Properties.Settings.Default != null)
      {
        Pen targetPen = new Pen(Properties.Settings.Default.AOITargetColor, Properties.Settings.Default.AOITargetWidth);
        targetPen.DashStyle = Properties.Settings.Default.AOITargetStyle;
        targetPen.LineJoin = LineJoin.Miter;

        Pen dottedPen = new Pen(Properties.Settings.Default.AOIStandardColor, Properties.Settings.Default.AOIStandardWidth);
        dottedPen.DashStyle = DashStyle.Dash;
        dottedPen.LineJoin = LineJoin.Miter;

        Pen defaultPen = new Pen(Properties.Settings.Default.AOIStandardColor, Properties.Settings.Default.AOIStandardWidth);
        defaultPen.DashStyle = Properties.Settings.Default.AOIStandardStyle;
        defaultPen.LineJoin = LineJoin.Miter;

        Pen searchRectPen = new Pen(Properties.Settings.Default.AOISearchRectColor, Properties.Settings.Default.AOISearchRectWidth);
        searchRectPen.DashStyle = Properties.Settings.Default.AOISearchRectStyle;
        searchRectPen.LineJoin = LineJoin.Miter;

        Font targetFont = (Font)Properties.Settings.Default.AOITargetFont.Clone();
        Color targetFontColor = Properties.Settings.Default.AOITargetFontColor;

        Font defaultFont = (Font)Properties.Settings.Default.AOIStandardFont.Clone();
        Color defaultFontColor = Properties.Settings.Default.AOIStandardFontColor;

        Font searchRectFont = (Font)Properties.Settings.Default.AOISearchRectFont.Clone();
        Color searchRectFontColor = Properties.Settings.Default.AOISearchRectFontColor;

        VGAlignment defaultTextAlignment = (VGAlignment)Enum.Parse(typeof(VGAlignment), Properties.Settings.Default.AOIDefaultTextAlignment);
        VGAlignment targetTextAlignment = (VGAlignment)Enum.Parse(typeof(VGAlignment), Properties.Settings.Default.AOITargetTextAlignment);
        VGAlignment searchRectTextAlignment = (VGAlignment)Enum.Parse(typeof(VGAlignment), Properties.Settings.Default.AOISearchRectTextAlignment);

        this.InitializeElements(
          targetPen,
          dottedPen,
          defaultPen,
          searchRectPen,
          targetFont,
          targetFontColor,
          defaultFont,
          defaultFontColor,
          searchRectFont,
          searchRectFontColor,
          targetTextAlignment,
          defaultTextAlignment,
          searchRectTextAlignment);
      }
    }

    #endregion //OVERRIDES

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler                                                              //
    ///////////////////////////////////////////////////////////////////////////////
    #region EVENTS

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler for UI, Menu, Buttons, Toolbars etc.                         //
    ///////////////////////////////////////////////////////////////////////////////
    #region WINDOWSEVENTHANDLER
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
    // Methods for doing main class job                                          //
    ///////////////////////////////////////////////////////////////////////////////
    #region METHODS
    #endregion //METHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER

    /// <summary>
    /// This method counts the target shapes and returns
    /// the next valid target number.
    /// </summary>
    /// <returns>An <see cref="Int32"/> with the next valid target number.</returns>
    private int GetNextTargetNumber()
    {
      int count = 0;
      int largest = 0;
      foreach (VGElement element in this.Elements)
      {
        if (element.StyleGroup == VGStyleGroup.AOI_TARGET)
        {
          int number = Convert.ToInt32(element.Name.Replace("Target", string.Empty));
          if (number > largest)
          {
            largest = number;
          }

          count++;
        }
      }

      if (largest >= count)
      {
        count = largest + 1;
      }

      return count;
    }

    #endregion //HELPER
  }
}
