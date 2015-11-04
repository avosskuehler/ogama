// <copyright file="PictureModifiable.cs" company="FU Berlin">
// ******************************************************
// OGAMA - open gaze and mouse analyzer 
// Copyright (C) 2015 Dr. Adrian Voßkühler  
// ------------------------------------------------------------------------
// This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
// This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
// You should have received a copy of the GNU General Public License along with this program; if not, write to the Free Software Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
// **************************************************************
// </copyright>
// <author>Adrian Voßkühler</author>
// <email>adrian@ogama.net</email>

namespace VectorGraphics.Canvas
{
  using System;
  using System.ComponentModel;
  using System.Drawing;
  using System.Windows.Forms;

  using VectorGraphics.Elements;
  using VectorGraphics.Elements.ElementCollections;
  using VectorGraphics.Tools;
  using VectorGraphics.Tools.CustomEventArgs;

  /// <summary>
  /// Derived from <see cref="Picture"/>. Allows modifying of shapes through selection
  /// with the mouse and grab handles.
  /// Adds <see cref="ShapeAdded"/>, <see cref="ShapeDeleted"/>, <see cref="ShapeSelected"/>,
  /// <see cref="ShapeDeselected"/>, <see cref="ShapeChanged"/>, <see cref="ShapeDoubleClick"/> events.
  /// to the base class.
  /// </summary>
  public abstract partial class PictureModifiable : Picture
  {
    ///////////////////////////////////////////////////////////////////////////////
    // Defining Constants                                                        //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTANTS

    /// <summary>
    /// Determines maximum distance of two points (in pixel) given by mouse input
    /// that should be considere as two different.
    /// If they are closer than this value polyline would suggest closing
    /// during creation.
    /// </summary>
    private const int MAXDISTANCEPOLYLINECLOSE = 15;

    /// <summary>
    /// This value determines the amount of pixels the object should
    /// move, when it is moved with the arrow keys.
    /// </summary>
    private const int MOVEOFFSET = 2;

    /// <summary>
    /// This value determines the amount of pixels of the margin to be snapped to.
    /// </summary>
    private const int SNAPPADDING = 20;

    #endregion //CONSTANTS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Variables, Enumerations, Events                                  //
    ///////////////////////////////////////////////////////////////////////////////
    #region FIELDS

    /// <summary>
    /// Saves mouse click point for calculation.
    /// </summary>
    private PointF firstClickPoint;

    /// <summary>
    /// Line segment of the current mouse movement point and the last mouse down point
    /// during polyline creation
    /// </summary>
    private VGLine currentLine;

    /// <summary>
    /// Saves the currently created shape.
    /// </summary>
    private VGElement newShape;

    /// <summary>
    /// Saves the build state of a new element.
    /// </summary>
    private BuildState state;

    /// <summary>
    /// Saves the current selected element which
    /// is shown with grab handles and selection frame.
    /// </summary>
    private VGElement selectedElement;

    /// <summary>
    /// Currently active grab handle
    /// </summary>
    private GrabHandle activeGrabHandle;

    /// <summary>
    /// Last mouse down point
    /// </summary>
    private Point mouseDownPoint;

    /// <summary>
    /// Pen for standard shapes. Column "Group" has no entry.
    /// </summary>
    private Pen defaultPen;

    /// <summary>
    /// Pen for shapes with column "Group" set to "SearchRect".
    /// </summary>
    private Pen searchRectPen;

    /// <summary>
    /// Pen for shapes with column "Group" set to "Target".
    /// </summary>
    private Pen targetPen;

    /// <summary>
    /// Pen for currently edited shapes.
    /// </summary>
    private Pen dottedPen;

    /// <summary>
    /// Font for standard shapes.Column "Group" has no entry.
    /// </summary>
    private Font defaultFont;

    /// <summary>
    /// Font for shapes with column "Group" set to "SearchRect".
    /// </summary>
    private Font searchRectFont;

    /// <summary>
    /// Font for shapes with column "Group" set to "Target".
    /// </summary>
    private Font targetFont;

    /// <summary>
    /// Brush for standard shapes.Column "Group" has no entry.
    /// </summary>
    private Color defaultFontColor;

    /// <summary>
    /// Brush for shapes with column "Group" set to "SearchRect".
    /// </summary>
    private Color searchRectFontColor;

    /// <summary>
    /// Brush for shapes with column "Group" set to "Target".
    /// </summary>
    private Color targetFontColor;

    /// <summary>
    /// Brush for standard shapes. Column "Group" has no entry.
    /// </summary>
    private Brush defaultBrush;

    /// <summary>
    /// The VGAlignment for default shapes with column "Group" has no entry.
    /// </summary>
    private VGAlignment defaultTextAlignment;

    /// <summary>
    /// VGAlignment for shapes with column "Group" set to "SearchRect".
    /// </summary>
    private VGAlignment searchRectTextAlignment;

    /// <summary>
    /// VGAlignment for shapes with column "Group" set to "Target".
    /// </summary>
    private VGAlignment targetTextAlignment;

    /// <summary>
    /// Saves the current index of the element that 
    /// is selected of the list of elements that are
    /// under the mouse cursor.
    /// </summary>
    private int counterUnderCursor;

    /// <summary>
    /// Saves a <see cref="Rectangle"/> with a margin 
    /// indicating the area for snapping at the 
    /// top of this <see cref="PictureModifiable"/>.
    /// </summary>
    private Rectangle topMargin;

    /// <summary>
    /// Saves a <see cref="Rectangle"/> with a margin 
    /// indicating the area for snapping at the 
    /// top of this <see cref="PictureModifiable"/>.
    /// </summary>
    private Rectangle leftMargin;

    /// <summary>
    /// Saves a <see cref="Rectangle"/> with a margin 
    /// indicating the area for snapping at the 
    /// bottom of this <see cref="PictureModifiable"/>.
    /// </summary>
    private Rectangle bottomMargin;

    /// <summary>
    /// Saves a <see cref="Rectangle"/> with a margin 
    /// indicating the area for snapping at the 
    /// right of this <see cref="PictureModifiable"/>.
    /// </summary>
    private Rectangle rightMargin;

    /// <summary>
    /// Saves a <see cref="Rectangle"/> with a margin 
    /// indicating the area for snapping at the 
    /// center of this <see cref="PictureModifiable"/>.
    /// </summary>
    private Rectangle centerArea;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the PictureModifiable class.
    /// </summary>
    public PictureModifiable()
      : base()
    {
      this.InitializeComponent();
      this.InitializeSnapAreas();
      this.InitializePictureDefaultElements();
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Enumerations                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region ENUMS

    /// <summary>
    /// Event. Raised when new shape has been added to the picture.
    /// </summary>
    public event ShapeEventHandler ShapeAdded;

    /// <summary>
    /// Event. Raised when selected shape has been deleted.
    /// </summary>
    public event ShapeEventHandler ShapeDeleted;

    /// <summary>
    /// Event. Raised when a shape has been selected.
    /// </summary>
    public event ShapeEventHandler ShapeSelected;

    /// <summary>
    /// Event. Raised when the selected shape has been deselected.
    /// </summary>
    public event EventHandler ShapeDeselected;

    /// <summary>
    /// Event. Raised when the selected shape has been changed.
    /// </summary>
    public event ShapeEventHandler ShapeChanged;

    /// <summary>
    /// Event. Raised when a shape has been double clicked.
    /// </summary>
    public event ShapeEventHandler ShapeDoubleClick;

    #endregion ENUMS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets pen for target shapes.
    /// </summary>
    /// <value>A <see cref="Pen"/> for target shapes.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Pen TargetPen
    {
      get { return this.targetPen; }
    }

    /// <summary>
    /// Gets or sets pen for default shapes.
    /// </summary>
    /// <value>A <see cref="Pen"/> for default shapes.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Pen DefaultPen
    {
      get { return this.defaultPen; }
      set { this.defaultPen = value; }
    }

    /// <summary>
    /// Gets pen for search rectangle shapes.
    /// </summary>
    /// <value>A <see cref="Pen"/> for shapes marked as SearchRect.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Pen SearchRectPen
    {
      get { return this.searchRectPen; }
    }

    /// <summary>
    /// Gets or sets pen for currently edited shapes.
    /// </summary>
    /// <value>A <see cref="Pen"/> used during shape creation.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Pen DottedPen
    {
      get { return this.dottedPen; }
      set { this.dottedPen = value; }
    }

    /// <summary>
    /// Gets or sets the font for default shapes.
    /// </summary>
    /// <value>A <see cref="Font"/> used for default shapes text.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Font DefaultFonts
    {
      get { return this.defaultFont; }
      set { this.defaultFont = value; }
    }

    /// <summary>
    /// Gets or sets color of default shapes.
    /// </summary>
    /// <value>A <see cref="Color"/> used for default shapes text.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Color DefaultFontColor
    {
      get { return this.defaultFontColor; }
      set { this.defaultFontColor = value; }
    }

    /// <summary>
    /// Gets or sets brush for default shapes.
    /// </summary>
    /// <value>A <see cref="Brush"/> for default shapes.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Brush DefaultBrush
    {
      get { return this.defaultBrush; }
      set { this.defaultBrush = value; }
    }

    /// <summary>
    /// Gets font for search rectangle shapes.
    /// </summary>
    /// <value>A <see cref="Font"/> used for SearchRect shapes text.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Font SearchRectFont
    {
      get { return this.searchRectFont; }
    }

    /// <summary>
    /// Gets color for search rectangle shapes.
    /// </summary>
    /// <value>A <see cref="Color"/> used for search rectangle shapes text.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Color SearchRectFontColor
    {
      get { return this.searchRectFontColor; }
    }

    /// <summary>
    /// Gets font for target shapes.
    /// </summary>
    /// <value>A <see cref="Font"/> used for Target shapes text.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Font TargetFont
    {
      get { return this.targetFont; }
    }

    /// <summary>
    /// Gets color for target shapes.
    /// </summary>
    /// <value>A <see cref="Color"/> used for target shapes text.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Color TargetFontColor
    {
      get { return this.targetFontColor; }
    }

    /// <summary>
    /// Gets <see cref="VGAlignment"/> for default shapes.
    /// </summary>
    /// <value>A <see cref="Color"/> used for target shapes text.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public VGAlignment DefaultTextAlignment
    {
      get { return this.defaultTextAlignment; }
    }

    /// <summary>
    /// Gets <see cref="VGAlignment"/> for target shapes.
    /// </summary>
    /// <value>A <see cref="Color"/> used for target shapes text.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public VGAlignment TargetTextAlignment
    {
      get { return this.targetTextAlignment; }
    }

    /// <summary>
    /// Gets <see cref="VGAlignment"/> for search rect shapes.
    /// </summary>
    /// <value>A <see cref="Color"/> used for target shapes text.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public VGAlignment SearchRectTextAlignment
    {
      get { return this.searchRectTextAlignment; }
    }

    /// <summary>
    /// Gets or sets the current selected element.
    /// </summary>
    /// <value>A <see cref="VGElement"/> that is selected and drawn
    /// with selection frame and grab handles.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public VGElement SelectedElement
    {
      get
      {
        return this.selectedElement;
      }

      set
      {
        if (this.selectedElement != null)
        {
          this.selectedElement.IsInEditMode = false;
        }

        this.selectedElement = value;

        if (this.selectedElement != null)
        {
          this.selectedElement.IsInEditMode = true;
          this.OnShapeSelected(new ShapeEventArgs(this.selectedElement));
        }
        else
        {
          this.OnShapeDeselected(EventArgs.Empty);
        }
      }
    }

    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Public methods                                                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region PUBLICMETHODS

    /// <summary>
    /// Handles the Copy event by copying the current
    /// selected <see cref="VGElement"/> to the clipboard.
    /// </summary>
    public virtual void OnCopy()
    {
      if (this.selectedElement != null)
      {
        Clipboard.SetData(DataFormats.StringFormat, VGElement.Serialize(this.selectedElement));
      }
      else
      {
        Clipboard.SetImage(this.RenderToImage());
      }
    }

    /// <summary>
    /// Handles the Paste event by pasting an <see cref="VGElement"/> from the
    /// clipboard into the picture.
    /// </summary>
    public virtual void OnPaste()
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

        if (!this.Elements.Contains(elementToAdd))
        {
          this.Elements.Add(elementToAdd);
          this.selectedElement = elementToAdd;

          // New Graphic element created, so notify listeners.
          this.OnShapeAdded(new ShapeEventArgs(elementToAdd));
        }
      }
    }

    /// <summary>
    /// Handles the Cut event by copying the selected element to the clipboard
    /// and deleting it form the surface.
    /// </summary>
    public virtual void OnCut()
    {
      if (this.selectedElement != null)
      {
        Clipboard.SetData(DataFormats.StringFormat, VGElement.Serialize(this.selectedElement));
        this.Elements.Remove(this.selectedElement);
        this.OnShapeDeleted(new ShapeEventArgs(this.selectedElement));
        this.ResetSelectedElement();
      }
    }

    /// <summary>
    /// Starts new rectangular shape by creating a <see cref="VGRectangle"/>
    /// in the <see cref="newShape"/> field.
    /// Then calls <see cref="StartCreation(Cursor)"/>.
    /// </summary>
    /// <param name="newShapeDrawAction">The <see cref="ShapeDrawAction"/> for the
    /// new shape.</param>
    /// <param name="pen">The <see cref="Pen"/> for the new shape.</param>
    /// <param name="brush">The <see cref="Brush"/> for the new shape.</param>
    /// <param name="font">The <see cref="Font"/> for the new shape.</param>
    /// <param name="fontColor">The <see cref="Color"/> for the font of the new shape.</param>
    /// <param name="group">The <see cref="VGStyleGroup"/> for the new shape.</param>
    /// <param name="name">The optional name for the new shape.</param>
    public void NewRectangleStart(
      ShapeDrawAction newShapeDrawAction,
      Pen pen,
      Brush brush,
      Font font,
      Color fontColor,
      VGStyleGroup group,
      string name)
    {
      if (pen != null)
      {
        this.defaultPen = pen;
      }

      if (brush != null)
      {
        this.defaultBrush = brush;
      }

      if (font != null)
      {
        this.defaultFont = font;
      }

      if (fontColor != Color.Empty)
      {
        this.defaultFontColor = fontColor;
      }

      // Create Rect with defined stroke
      this.newShape = new VGRectangle(
        newShapeDrawAction,
        this.defaultPen,
        this.defaultBrush,
        this.defaultFont,
        this.defaultFontColor,
        RectangleF.Empty,
        group,
        name,
        string.Empty);

      this.StartCreation(CustomCursors.Rectangle);
    }

    /// <summary>
    /// Starts new ellipsoid shape by creating a <see cref="VGEllipse"/>
    /// in the <see cref="newShape"/> field.
    /// Then calls <see cref="StartCreation(Cursor)"/>.
    /// </summary>
    /// <param name="newShapeDrawAction">The <see cref="ShapeDrawAction"/> for the
    /// new shape.</param>
    /// <param name="pen">The <see cref="Pen"/> for the new shape.</param>
    /// <param name="brush">The <see cref="Brush"/> for the new shape.</param>
    /// <param name="font">The <see cref="Font"/> for the new shape.</param>
    /// <param name="fontColor">The <see cref="Color"/> for the font of the new shape.</param>
    /// <param name="group">The <see cref="VGStyleGroup"/> for the new shape.</param>
    /// <param name="name">The optional name for the new shape.</param>
    public void NewEllipseStart(
      ShapeDrawAction newShapeDrawAction,
      Pen pen,
      Brush brush,
      Font font,
      Color fontColor,
      VGStyleGroup group,
      string name)
    {
      if (pen != null)
      {
        this.defaultPen = pen;
      }

      if (brush != null)
      {
        this.defaultBrush = brush;
      }

      if (font != null)
      {
        this.defaultFont = font;
      }

      if (fontColor != Color.Empty)
      {
        this.defaultFontColor = fontColor;
      }

      this.newShape = new VGEllipse(
        newShapeDrawAction,
        this.defaultPen,
        this.defaultBrush,
        this.defaultFont,
        this.defaultFontColor,
        RectangleF.Empty,
        group,
        name,
        string.Empty);

      this.StartCreation(CustomCursors.Ellipse);
    }

    /// <summary>
    /// Starts new polylineal shape by creating a <see cref="VGPolyline"/>
    /// in the <see cref="newShape"/> field.
    /// Then calls <see cref="StartCreation(Cursor)"/>.
    /// </summary>
    /// <param name="newShapeDrawAction">The <see cref="ShapeDrawAction"/> for the
    /// new shape.</param>
    /// <param name="pen">The <see cref="Pen"/> for the new shape.</param>
    /// <param name="brush">The <see cref="Brush"/> for the new shape.</param>
    /// <param name="font">The <see cref="Font"/> for the new shape.</param>
    /// <param name="fontColor">The <see cref="Color"/> for the font of the new shape.</param>
    /// <param name="group">The <see cref="VGStyleGroup"/> for the new shape.</param>
    /// <param name="name">The optional name for the new shape.</param>
    public void NewPolylineStart(
      ShapeDrawAction newShapeDrawAction,
      Pen pen,
      Brush brush,
      Font font,
      Color fontColor,
      VGStyleGroup group,
      string name)
    {
      if (pen != null)
      {
        this.defaultPen = pen;
      }

      if (brush != null)
      {
        this.defaultBrush = brush;
      }

      if (font != null)
      {
        this.defaultFont = font;
      }

      if (fontColor != Color.Empty)
      {
        this.defaultFontColor = fontColor;
      }

      this.newShape = new VGPolyline(
        newShapeDrawAction,
        this.defaultPen,
        this.defaultBrush,
       this.defaultFont,
       this.defaultFontColor,
       group,
       name,
       string.Empty);

      this.currentLine = new VGLine(ShapeDrawAction.Edge, this.defaultPen);
      this.StartCreation(CustomCursors.Polyline);
    }

    /// <summary>
    /// Starts new line shape by creating a <see cref="VGLine"/>
    /// in the <see cref="newShape"/> field.
    /// Then calls <see cref="StartCreation(Cursor)"/>.
    /// </summary>
    /// <param name="newShapeDrawAction">The <see cref="ShapeDrawAction"/> for the
    /// new shape.</param>
    /// <param name="pen">The <see cref="Pen"/> for the new shape.</param>
    /// <param name="brush">The <see cref="Brush"/> for the new shape.</param>
    /// <param name="font">The <see cref="Font"/> for the new shape.</param>
    /// <param name="fontColor">The <see cref="Color"/> for the font of the new shape.</param>
    /// <param name="group">The <see cref="VGStyleGroup"/> for the new shape.</param>
    /// <param name="name">The optional name for the new shape.</param>
    public void NewLineStart(
      ShapeDrawAction newShapeDrawAction,
      Pen pen,
      Brush brush,
      Font font,
      Color fontColor,
      VGStyleGroup group,
      string name)
    {
      if (pen != null)
      {
        this.defaultPen = pen;
      }

      if (brush != null)
      {
        this.defaultBrush = brush;
      }

      if (font != null)
      {
        this.defaultFont = font;
      }

      if (fontColor != Color.Empty)
      {
        this.defaultFontColor = fontColor;
      }

      this.newShape = new VGLine(
        newShapeDrawAction,
        this.defaultPen,
        this.defaultFont,
        this.defaultFontColor,
        group,
        name,
        string.Empty);

      this.StartCreation(CustomCursors.Line);
    }

    /// <summary>
    /// Starts new shape by setting the <see cref="VGElement"/>
    /// in the <see cref="newShape"/> field.
    /// Then calls <see cref="StartCreation(Cursor)"/>.
    /// </summary>
    /// <param name="element">The <see cref="VGElement"/> to be added to the <see cref="Picture"/></param>
    public void NewShapeStart(VGElement element)
    {
      this.newShape = element;

      if (this.newShape is VGRectangle)
      {
        this.StartCreation(CustomCursors.Rectangle);
      }
      else if (this.newShape is VGEllipse)
      {
        this.StartCreation(CustomCursors.Ellipse);
      }
      else if (this.newShape is VGPolyline)
      {
        this.currentLine = new VGLine(ShapeDrawAction.Edge, this.newShape.Pen);
        this.StartCreation(CustomCursors.Polyline);
      }
      else if (this.newShape is VGLine)
      {
        this.StartCreation(CustomCursors.Line);
      }
      else if (this.newShape is VGSound)
      {
        this.StartCreation(CustomCursors.Sound);
      }
      else if (this.newShape is VGSharp)
      {
        this.StartCreation(CustomCursors.Sharp);
      }
    }

    /// <summary>
    /// Starts new textual shape by setting the <see cref="VGRichText"/>
    /// in the <see cref="newShape"/> field.
    /// Then calls <see cref="StartCreation(Cursor)"/>.
    /// </summary>
    /// <param name="text">The <see cref="VGRichText"/> to be added to the <see cref="Picture"/></param>
    public void NewRtfTextStart(VGRichText text)
    {
      Point position = this.Location;
      position.Offset(
        (int)((this.Width / 2) - (text.Size.Width / 2)),
        (int)((this.Height / 2) - (text.Size.Height / 2)));

      text.Location = position;
      this.newShape = text;

      this.StartCreation(CustomCursors.Text);
    }

    /// <summary>
    /// Starts new textual shape by setting the <see cref="VGText"/>
    /// in the <see cref="newShape"/> field.
    /// Then calls <see cref="StartCreation(Cursor)"/>.
    /// </summary>
    /// <param name="text">The <see cref="VGText"/> to be added to the <see cref="Picture"/></param>
    public void NewTextStart(VGText text)
    {
      Point position = this.Location;
      position.Offset(
        (int)((this.Width / 2) - (text.Size.Width / 2)),
        (int)((this.Height / 2) - (text.Size.Height / 2)));
      text.Location = position;
      this.newShape = text;

      this.StartCreation(CustomCursors.Text);
    }

    /// <summary>
    /// Starts new image object by setting the <see cref="VGImage"/>
    /// in the <see cref="newShape"/> field.
    /// Then calls <see cref="StartCreation(Cursor)"/>.
    /// </summary>
    /// <param name="image">The <see cref="VGImage"/> to be added to the <see cref="Picture"/></param>
    public void NewImageStart(VGImage image)
    {
      switch (image.Layout)
      {
        case ImageLayout.Stretch:
        case ImageLayout.Tile:
        case ImageLayout.Zoom:
        case ImageLayout.Center:
          // In this cases no more drag and pull is needed,
          // so add the new image to the list.
          this.Elements.Add(image);

          // Select it
          this.SelectedElement = image;
          break;
        case ImageLayout.None:
          // Position and size is needed, so start dragging
          this.newShape = image;
          this.StartCreation(CustomCursors.Image);
          break;
      }
    }

    /// <summary>
    /// Starts new flash object by setting the <see cref="VGFlash"/>
    /// in the <see cref="newShape"/> field.
    /// Then calls <see cref="StartCreation(Cursor)"/>.
    /// </summary>
    /// <param name="flash">The <see cref="VGFlash"/> to be added to the <see cref="Picture"/></param>
    public void NewFlashStart(VGFlash flash)
    {
      this.newShape = flash;
      this.StartCreation(CustomCursors.Image);
    }

    /// <summary>
    /// Eventhandler for the PenChanged event. 
    /// Updates all graphic elements from the given group
    /// with the new pen.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A <see cref="PenChangedEventArgs"/> that hold new group and pen</param>
    public void PenChanged(object sender, PenChangedEventArgs e)
    {
      VGElementCollection sublist = this.Elements.FindAllGroupMembers(e.ElementGroup);
      foreach (VGElement element in sublist)
      {
        element.Pen = e.Pen;
      }

      this.DrawForeground(true);

      switch (e.ElementGroup)
      {
        case VGStyleGroup.AOI_NORMAL:
          this.defaultPen = e.Pen;
          break;
        case VGStyleGroup.AOI_TARGET:
          this.targetPen = e.Pen;
          break;
        case VGStyleGroup.AOI_SEARCHRECT:
          this.searchRectPen = e.Pen;
          break;
        default:
          this.defaultPen = e.Pen;
          break;
      }
    }

    /// <summary>
    /// Eventhandler for the FontStyleChanged event. 
    /// Updates all graphic elements from the given group
    /// with the new font.
    /// </summary>
    /// <param name="sender">message sender</param>
    /// <param name="e">Font change event arguments that hold new group and font and brush</param>
    public void FontStyleChanged(object sender, FontChangedEventArgs e)
    {
      VGElementCollection sublist = this.Elements.FindAllGroupMembers(e.ElementGroup);
      foreach (VGElement element in sublist)
      {
        element.Font = e.Font;
        element.FontColor = e.FontColor;
        element.TextAlignment = e.FontAlignment;
      }

      this.DrawForeground(true);

      switch (e.ElementGroup)
      {
        case VGStyleGroup.AOI_NORMAL:
          this.defaultFont = e.Font;
          this.defaultFontColor = e.FontColor;
          break;
        case VGStyleGroup.AOI_TARGET:
          this.targetFont = e.Font;
          this.targetFontColor = e.FontColor;
          break;
        case VGStyleGroup.AOI_SEARCHRECT:
          this.searchRectFont = e.Font;
          this.searchRectFontColor = e.FontColor;
          break;
        default:
          this.defaultFont = e.Font;
          this.defaultFontColor = e.FontColor;
          break;
      }
    }

    /// <summary>
    /// Resets the selected element and its dependencies to null.
    /// If there was a selected element, invokes <see cref="ShapeDeselected"/> event.
    /// </summary>
    public void ResetSelectedElement()
    {
      if (this.selectedElement != null)
      {
        this.selectedElement.IsInEditMode = false;

        // Use property to invoke the ShapeDeselected event
        this.SelectedElement = null;
        this.activeGrabHandle = null;
        this.DrawForeground(false);
      }
    }

    #endregion //PUBLICMETHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Inherited methods                                                         //
    ///////////////////////////////////////////////////////////////////////////////
    #region OVERRIDES

    /// <summary>
    /// Resets the current picture. And resets eventually selected element.
    /// </summary>
    public override void ResetPicture()
    {
      base.ResetPicture();
      this.ResetSelectedElement();
    }

    /// <summary>
    /// Overridden. Frees resources of objects that are not disposed
    /// by the designer, mainly private objects.
    /// Is called during the call to <see cref="Control.Dispose(Boolean)"/>.
    /// </summary>
    protected override void CustomDispose()
    {
      if (this.currentLine != null)
      {
        this.currentLine.Dispose();
      }

      if (this.newShape != null)
      {
        this.newShape.Dispose();
      }

      this.defaultPen.Dispose();
      this.searchRectPen.Dispose();
      this.targetPen.Dispose();
      this.dottedPen.Dispose();
      this.defaultFont.Dispose();
      this.searchRectFont.Dispose();
      this.targetFont.Dispose();
      if (this.defaultBrush != null)
      {
        this.defaultBrush.Dispose();
      }

      base.CustomDispose();
    }

    /// <summary>
    /// Overridden <see cref="Control.MouseMove"/> event handler.
    /// During shape creation this finishes creation. So the
    /// <see cref="ShapeAdded"/> event is raised.
    /// Otherwise update bounds of moved objects.
    /// </summary>
    /// <param name="e">The <see cref="MouseEventArgs"/> with the event data.</param>
    protected override void OnMouseUp(MouseEventArgs e)
    {
      if (Control.ModifierKeys != Keys.ShiftKey)
      {
        // Transform mouse down point to stimulus coordinates
        PointF mouseLocation = this.GetTransformedMouseLocation(e.Location);

        // If is in new shape creation mode
        if (this.state == BuildState.FirstPointSet)
        {
          // Special handling for VGPolylines
          // return if only a new point is set, and
          // the polyline is not closed yet.
          if (this.newShape is VGPolyline)
          {
            VGPolyline poly = (VGPolyline)this.newShape;
            if (!poly.IsClosed)
            {
              return;
            }
          }
          else if (this.newShape is VGLine)
          {
            // Cast newShape to VGLine
            VGLine line = (VGLine)this.newShape;
            line.SecondPoint = mouseLocation;
          }
          else
          {
            // Shape creation done
            this.newShape.Bounds =
              this.GetBoundingRectFromMousePosition(mouseLocation);
          }

          this.state = BuildState.None;

          // New Graphic element created, so notify listeners.
          this.OnShapeAdded(new ShapeEventArgs(this.newShape));

          // CleanUp creation mode
          this.newShape = null;
        }
        else if (this.SelectedElement != null)
        {
          // Is not in creation mode, so look for selected elements that were moved.
          if (this.SelectedElement.Modified)
          {
            // Reset modified flag.
            this.SelectedElement.Modified = false;

            // Existing GraphicElement Changed, so notify listeners.
            this.OnShapeChanged(new ShapeEventArgs(this.SelectedElement));
          }
        }

        this.Cursor = Cursors.Default;
      }
    }

    /// <summary>
    /// Overridden <see cref="Control.MouseDoubleClick"/> event handler.
    /// Raises the event <see cref="ShapeDoubleClick"/> when 
    /// a selected element was double clicked.
    /// </summary>
    /// <param name="e">The <see cref="MouseEventArgs"/> with the event data (click point).</param>
    protected override void OnMouseDoubleClick(MouseEventArgs e)
    {
      base.OnMouseDoubleClick(e);
      if (this.selectedElement != null)
      {
        this.OnShapeDoubleClick(new ShapeEventArgs(this.selectedElement));
      }
    }

    /// <summary>
    /// Overridden <see cref="Control.MouseDown"/> event handler.
    /// Starts requested shape creation when left mouse button is pressed.
    /// Otherwise looks for elements under mouse cursor and selects them.
    /// If there is already a selected element and the mouse is over a grab
    /// handle it is selected.
    /// </summary>
    /// <remarks>Pressing the ALT key during selection iterates
    /// through the elementsUnderMouseCursor collection.</remarks>
    /// <param name="e">The <see cref="MouseEventArgs"/> with the event data (click point).</param>
    protected override void OnMouseDown(MouseEventArgs e)
    {
      // base.OnMouseDown(e);

      // Achieve focus to the picture, because otherwise it will not
      // receive input keys which are interesting as modifiers.
      this.Focus();

      if (Control.ModifierKeys != Keys.ShiftKey)
      {
        // Transform mouse down point to stimulus coordinates
        PointF mouseLocationF = this.GetTransformedMouseLocation(e.Location);
        Point mouseLocation = Point.Round(mouseLocationF);

        if (this.state != BuildState.None)
        {
          // Is creating a new shape
          // Left Mouse Button pressed, so start Logging
          if (e.Button == MouseButtons.Left)
          {
            this.state = BuildState.FirstPointSet;

            // Add the new shape to the list.
            if (!this.Elements.Contains(this.newShape))
            {
              this.Elements.Add(this.newShape);
            }

            // Select it
            this.SelectedElement = this.newShape;

            // save click point for later bounding rectangle creation
            this.firstClickPoint = mouseLocationF;

            // Special handling for polyline construction
            // every click is a polyline point
            // up to the moment the polyline is closed
            if (this.newShape is VGPolyline)
            {
              this.currentLine.FirstPoint = this.firstClickPoint;

              // Add the _CurrentLine element during polyline
              // creation
              if (!Elements.Contains(this.currentLine))
              {
                Elements.Add(this.currentLine);
              }

              // Cast newShape to polyline
              VGPolyline poly = (VGPolyline)this.newShape;

              // Check for closing polyline conditions
              if (this.PointsAreNear(poly.FirstPt, mouseLocationF) &&
                poly.GetPointCount() > 2)
              {
                // If the mouse click point is near the first point
                // close polyline.
                poly.ClosePolyline();

                this.state = BuildState.None;

                // New Graphic element created, so notify listeners.
                this.OnShapeAdded(new ShapeEventArgs(this.newShape));

                // CleanUp creation mode
                this.Elements.Remove(this.currentLine);
                this.newShape = null;
              }
              else
              {
                // Point is anywhere else, but not near the first
                // polyline point.
                // Add new polyline point and update CurrentLine.
                this.currentLine.FirstPoint = mouseLocationF;
                poly.AddPt(mouseLocationF);
              }
            }
            else if (this.newShape is VGLine)
            {
              // Cast newShape to VGLine
              VGLine line = (VGLine)this.newShape;
              line.FirstPoint = this.firstClickPoint;
            }
            else if (this.newShape is VGFlash)
            {
              // Cast newShape to VGFlash
              VGFlash flash = (VGFlash)this.newShape;
              flash.InitializeOnControl(this.Parent, false, this.StimulusToScreen);
            }
          }
        }
        else
        {
          // We are not in new shape creation mode
          if (e.Button == MouseButtons.Left)
          {
            this.mouseDownPoint = e.Location;

            // Store elements under mouse cursor.
            VGElementCollection elementsUnderMouseCursor = new VGElementCollection();
            int numberOfElements = this.Elements.Count;
            for (int i = 0; i < numberOfElements; i++)
            {
              VGElement currentElement = this.Elements[numberOfElements - i - 1];
              if (currentElement.Contains(mouseLocation))
              {
                elementsUnderMouseCursor.Add(currentElement);
                if (currentElement == this.SelectedElement)
                {
                  this.counterUnderCursor = elementsUnderMouseCursor.Count - 1;
                }
              }
            }

            if (this.selectedElement == null)
            {
              // Check whether an element lies under
              // mouse cursor. Select the next item.
              if (elementsUnderMouseCursor.Count > 0)
              {
                this.SelectedElement = elementsUnderMouseCursor[0];
                this.selectedElement.IsInEditMode = true;
              }
              else
              {
                this.SelectedElement = null;
              }
            }
            else if (this.selectedElement.Contains(mouseLocation))
            {
              // Click in selected element 
              // could be more than one, so iterate if Modifier alt is pressed.
              if (Control.ModifierKeys == Keys.Alt)
              {
                this.Cursor = Cursors.Default;
                this.counterUnderCursor++;

                // Reset counter if larger than element list size.
                if (this.counterUnderCursor >= elementsUnderMouseCursor.Count)
                {
                  this.counterUnderCursor = 0;
                }

                // Select next item if there is any
                if (elementsUnderMouseCursor.Count > 1)
                {
                  this.ResetSelectedElement();

                  // Use property to invoke Shape Selected event.
                  this.SelectedElement = elementsUnderMouseCursor[this.counterUnderCursor];
                  this.selectedElement.IsInEditMode = true;
                }
              }
              else
              {
                // No alt is pressed
                // Select grab handle, if mouse is over it
                foreach (GrabHandle handle in this.selectedElement.GrabHandles)
                {
                  if (handle.Contains(mouseLocation))
                  {
                    this.activeGrabHandle = handle;
                    break;
                  }

                  // Select center handle
                  this.activeGrabHandle = this.selectedElement.GrabHandles[0];
                }
              }
            }
            else
            {
              // Click in region outside current selected element
              // Reset selected element. And look for other element
              // under mouse cursor.
              this.ResetSelectedElement();

              // Select the first element that lies under mouse cursor.
              if (elementsUnderMouseCursor.Count > 0)
              {
                this.SelectedElement = elementsUnderMouseCursor[0];
                this.selectedElement.IsInEditMode = true;
              }
              else
              {
                this.SelectedElement = null;
              }
            }

            this.DrawForeground(false);
          }
        }
      }
    }

    /// <summary>
    /// Overridden <see cref="Control.MouseMove"/> event handler.
    /// During shape creation updates currently created shape with new bounds.
    /// Otherwise raises mouse cursors depending on position.
    /// If it is over grab handle use grab handles cursor, if
    /// is over shape use <see cref="Cursors.SizeAll"/> cursor.
    /// If mouse button is pressed, update shapes postion properties.
    /// </summary>
    /// <param name="e">The <see cref="MouseEventArgs"/> with the event data.</param>
    protected override void OnMouseMove(MouseEventArgs e)
    {
      base.OnMouseMove(e);

      if (Control.ModifierKeys != Keys.ShiftKey)
      {
        // Transform mouse down point to stimulus coordinates
        PointF mouseLocationF = this.GetTransformedMouseLocation(e.Location);
        Point mouseLocation = Point.Round(mouseLocationF);

        // Creating shapes
        if (this.state != BuildState.None)
        {
          // if mouse down point is set.
          if (this.state == BuildState.FirstPointSet)
          {
            // Special handling for VGPolyline objects
            // Shows close cursor, if mouse is near first polyline point
            if (this.newShape is VGPolyline)
            {
              // Cast newShape to VGPolyline
              VGPolyline poly = (VGPolyline)this.newShape;
              if (this.PointsAreNear(poly.FirstPt, mouseLocationF) && this.newShape.GetPointCount() > 2)
              {
                this.currentLine.SecondPoint = poly.FirstPt;
                this.Cursor = Cursors.Hand;
              }
              else
              {
                this.currentLine.SecondPoint = mouseLocationF;
                this.Cursor = CustomCursors.Polyline;
              }
            }
            else if (this.newShape is VGLine)
            {
              // Cast newShape to VGLine
              VGLine line = (VGLine)this.newShape;
              line.SecondPoint = mouseLocationF;
            }
            else
            {
              // For all other shape types update bounds.
              this.newShape.Bounds = this.GetBoundingRectFromMousePosition(mouseLocationF);
            }

            this.DrawForeground(false);
          }
        }
        else
        {
          // Not creating shapes.
          Point movement = new Point(
            this.mouseDownPoint.X - e.Location.X,
            this.mouseDownPoint.Y - e.Location.Y);

          this.mouseDownPoint = e.Location;

          Point[] mouseMovementTransformed = new Point[1];
          mouseMovementTransformed[0] = movement;
          this.ScreenToStimulus.VectorTransformPoints(mouseMovementTransformed);

          // if an element is selected check for movement of grab handles
          if (this.selectedElement != null)
          {
            // Raise SizeAllCursor if mouse is over the selected graphic element
            if (this.selectedElement.Contains(mouseLocation))
            {
              this.Cursor = Cursors.SizeAll;
            }
            else
            {
              this.Cursor = Cursors.Default;
            }

            // Also Reset Cursor if Alt Key is pressed, because that indicates
            // no movement but new selection
            if (Control.ModifierKeys == Keys.Alt)
            {
              this.Cursor = Cursors.Default;
            }

            if (e.Button == MouseButtons.None)
            {
              // Raise grab handles cursor if one graphic element is selected and mouse is
              // Over grab handle
              foreach (GrabHandle handle in this.selectedElement.GrabHandles)
              {
                if (handle.Contains(mouseLocation))
                {
                  this.Cursor = handle.Cursor;
                  break;
                }
              }
            }
            else if (e.Button == MouseButtons.Left)
            {
              // Selected element should move
              // if mouse is not over grab handle move center of object
              if (this.activeGrabHandle != null)
              {
                this.Cursor = this.activeGrabHandle.Cursor;
                this.selectedElement.GrabHandleMoved(ref this.activeGrabHandle, mouseMovementTransformed[0]);
              }

              this.DrawForeground(false);
            }
          }
        }

        if (this.selectedElement != null)
        {
          // Existing GraphicElement Changed, so notify listeners.
          this.OnShapeChanged(new ShapeEventArgs(this.SelectedElement));
        }
      }
    }

    /// <summary>
    /// <see cref="Control.PreviewKeyDown"/> event handler. 
    /// Listens for <see cref="Keys.Delete"/> to remove selected shapes.
    /// Also listens for arrow keys to move elements.
    /// </summary>
    /// <param name="e">A <see cref="PreviewKeyDownEventArgs"/> with the event arguments.</param>
    protected override void OnPreviewKeyDown(PreviewKeyDownEventArgs e)
    {
      base.OnPreviewKeyDown(e);

      if (e.KeyCode == Keys.Delete)
      {
        this.OnDelete();
      }
      else if (e.KeyCode == Keys.Up)
      {
        this.OnArrowKeyUp();
      }
      else if (e.KeyCode == Keys.Down)
      {
        this.OnArrowKeyDown();
      }
      else if (e.KeyCode == Keys.Left)
      {
        this.OnArrowKeyLeft();
      }
      else if (e.KeyCode == Keys.Right)
      {
        this.OnArrowKeyRight();
      }
      else if (e.KeyCode == Keys.Oemplus)
      {
        this.OnAddKey();
      }
      else if (e.KeyCode == Keys.OemMinus)
      {
        this.OnSubtractKey();
      }
      else if (e.KeyCode == Keys.PageDown)
      {
        this.OnPageDownKey();
      }
      else if (e.KeyCode == Keys.PageUp)
      {
        this.OnPageUpKey();
      }
      else if (e.KeyCode == Keys.C && e.Modifiers == Keys.Control)
      {
        this.OnCopy();
        e.IsInputKey = false;
      }
      else if (e.KeyCode == Keys.V && e.Modifiers == Keys.Control)
      {
        this.OnPaste();
      }
      else if (e.KeyCode == Keys.X && e.Modifiers == Keys.Control)
      {
        this.OnCut();
      }
    }

    /// <summary>
    /// Overridden <see cref="Control.IsInputKey(Keys)"/> method. 
    /// Sets all arrow keys as input keys, to avoid jumping in the form
    /// to the next control and using it for moving of elements.
    /// </summary>
    /// <param name="keyData">A <see cref="Keys"/> with the key data.</param>
    /// <returns><strong>True</strong>, if key should be processed in the control,
    /// otherwise <strong>false</strong>.</returns>
    protected override bool IsInputKey(Keys keyData)
    {
      switch (keyData & Keys.KeyCode)
      {
        case Keys.Up:
          return true;
        case Keys.Down:
          return true;
        case Keys.Right:
          return true;
        case Keys.Left:
          return true;
        default:
          return base.IsInputKey(keyData);
      }
    }

    #endregion //OVERRIDES

    ///////////////////////////////////////////////////////////////////////////////
    // Public methods                                                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROTECTEDMETHODS

    /// <summary>
    /// Overrides should initialize the drawing elements of this <see cref="PictureModifiable"/>
    /// by calling the <c>Properties.Settings.Default</c> values with
    /// <see cref="InitializeElements(Pen,Pen,Pen,Pen,Font,Color,Font,Color,Font,Color, VGAlignment,VGAlignment,VGAlignment)"/>.
    /// </summary>
    protected abstract void InitializePictureDefaultElements();

    /// <summary>
    /// Initializes standard values of drawing elements
    /// </summary>
    /// <param name="targetPen">A <see cref="Pen"/> for "target" elements</param>
    /// <param name="dottedPen">A <see cref="Pen"/> for dotted elements</param>
    /// <param name="defaultPen">A <see cref="Pen"/> for default elements</param>
    /// <param name="searchRectPen">A <see cref="Pen"/> for "searchRect" elements</param>
    /// <param name="targetFont">A <see cref="Font"/> for "target" elements</param>
    /// <param name="targetFontColor">A <see cref="Color"/> for "target" elements</param>
    /// <param name="defaultFont">A <see cref="Font"/> for default elements</param>
    /// <param name="defaultFontColor">A <see cref="Color"/> for default elements</param>
    /// <param name="searchRectFont">A <see cref="Font"/> for "searchRect" elements</param>
    /// <param name="searchRectFontColor">A <see cref="Color"/> for "searchRect" elements</param>
    /// <param name="targetTextAlignment">A <see cref="VGAlignment"/> for default elements</param>
    /// <param name="defaultTextAlignment">A <see cref="VGAlignment"/> for "target" elements</param>
    /// <param name="searchRectTextAlignment">A <see cref="VGAlignment"/> for "searchRect" elements</param>
    protected void InitializeElements(
      Pen targetPen,
      Pen dottedPen,
      Pen defaultPen,
      Pen searchRectPen,
      Font targetFont,
      Color targetFontColor,
      Font defaultFont,
      Color defaultFontColor,
      Font searchRectFont,
      Color searchRectFontColor,
      VGAlignment targetTextAlignment,
      VGAlignment defaultTextAlignment,
      VGAlignment searchRectTextAlignment)
    {
      this.targetPen = targetPen;
      this.targetFont = targetFont;
      this.targetFontColor = targetFontColor;
      this.defaultPen = defaultPen;
      this.defaultFont = defaultFont;
      this.defaultFontColor = defaultFontColor;
      this.searchRectPen = searchRectPen;
      this.searchRectFont = searchRectFont;
      this.searchRectFontColor = searchRectFontColor;
      this.dottedPen = dottedPen;
      this.targetTextAlignment = targetTextAlignment;
      this.defaultTextAlignment = defaultTextAlignment;
      this.searchRectTextAlignment = searchRectTextAlignment;
    }

    /// <summary>
    /// The protected OnShapeSelected method raises the 
    /// <see cref="ShapeSelected"/>event by invoking the delegates.
    /// </summary>
    /// <param name="e">The <see cref="ShapeEventArgs"/> with the event data.</param>
    protected virtual void OnShapeSelected(ShapeEventArgs e)
    {
      if (this.ShapeSelected != null)
      {
        // Invokes the delegates. 
        this.ShapeSelected(this, e);
      }
    }

    /// <summary>
    /// The protected OnShapeDeselected method raises the 
    /// <see cref="ShapeDeselected"/>event by invoking the delegates.
    /// </summary>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    protected virtual void OnShapeDeselected(EventArgs e)
    {
      if (this.ShapeDeselected != null)
      {
        // Invokes the delegates. 
        this.ShapeDeselected(this, e);
      }
    }

    /// <summary>
    /// The protected OnShapeAdded method raises the 
    /// <see cref="ShapeAdded"/>event by invoking the delegates.
    /// </summary>
    /// <param name="e">The <see cref="ShapeEventArgs"/> with the event data.</param>
    protected virtual void OnShapeAdded(ShapeEventArgs e)
    {
      if (this.ShapeAdded != null)
      {
        // Invokes the delegates. 
        this.ShapeAdded(this, e);
      }
    }

    /// <summary>
    /// The protected OnShapeDeleted method raises the <see cref="ShapeDeleted"/>  
    /// event by invoking the delegates.
    /// </summary>
    /// <param name="e">The <see cref="ShapeEventArgs"/> with the event data.</param>
    protected virtual void OnShapeDeleted(ShapeEventArgs e)
    {
      if (this.ShapeDeleted != null)
      {
        // Invokes the delegates. 
        this.ShapeDeleted(this, e);
      }
    }

    /// <summary>
    /// The protected OnShapeChanged method raises the <see cref="ShapeChanged"/>  
    /// event by invoking the delegates.
    /// </summary>
    /// <param name="e">The <see cref="ShapeEventArgs"/> with the event data.</param>
    protected virtual void OnShapeChanged(ShapeEventArgs e)
    {
      if (this.ShapeChanged != null)
      {
        // Invokes the delegates. 
        this.ShapeChanged(this, e);
      }
    }

    /// <summary>
    /// The protected OnShapeDoubleClick method raises the <see cref="ShapeChanged"/>  
    /// event by invoking the delegates.
    /// </summary>
    /// <param name="e">The <see cref="ShapeEventArgs"/> with the event data.</param>
    protected virtual void OnShapeDoubleClick(ShapeEventArgs e)
    {
      if (this.ShapeDoubleClick != null)
      {
        // Invokes the delegates. 
        this.ShapeDoubleClick(this, e);
      }
    }

    /// <summary>
    /// Is called, when the user clicked the delete button.
    /// This base class implementation removes a selected shape
    /// from the elements list and invokes the <see cref="ShapeDeleted"/> event.
    /// </summary>
    protected virtual void OnDelete()
    {
      if (this.selectedElement != null)
      {
        this.Elements.Remove(this.selectedElement);
        this.OnShapeDeleted(new ShapeEventArgs(this.selectedElement));
        this.ResetSelectedElement();
        this.DrawForeground(false);
      }
    }

    #endregion //PROTECTEDMETHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler                                                              //
    ///////////////////////////////////////////////////////////////////////////////
    #region EVENTS

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler for UI, Menu, Buttons, Toolbars etc.                         //
    ///////////////////////////////////////////////////////////////////////////////
    #region WINDOWSEVENTHANDLER

    /// <summary>
    /// The <see cref="Control.Resize"/> event handler. Updates the snap
    /// areas.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void PictureModifiable_Resize(object sender, EventArgs e)
    {
      this.InitializeSnapAreas();
    }

    #region CONTEXTMENU

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="ToolStripMenuItem"/> <see cref="cmuToFront"/>.
    /// Calls <see cref="OnPageUpKey()"/> to move element to tail.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void cmuToFront_Click(object sender, EventArgs e)
    {
      this.OnPageUpKey();
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="ToolStripMenuItem"/> <see cref="cmuForward"/>.
    /// Calls <see cref="OnAddKey()"/> to move element on index up.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void cmuForward_Click(object sender, EventArgs e)
    {
      this.OnAddKey();
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="ToolStripMenuItem"/> <see cref="cmuBackward"/>.
    /// Calls <see cref="OnSubtractKey()"/> to move element on index down.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void cmuBackward_Click(object sender, EventArgs e)
    {
      this.OnSubtractKey();
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="ToolStripMenuItem"/> <see cref="cmuToBack"/>.
    /// Calls <see cref="OnPageDownKey()"/> to move element to head of list.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void cmuToBack_Click(object sender, EventArgs e)
    {
      this.OnPageDownKey();
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="ToolStripMenuItem"/> <see cref="cmuDelete"/>.
    /// Calls <see cref="OnDelete()"/> to remove the element from the list.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void cmuDelete_Click(object sender, EventArgs e)
    {
      this.OnDelete();
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="ToolStripMenuItem"/> <see cref="cmuAlignCenter"/>.
    /// Aligns the selected element at the center of the <see cref="Picture"/>.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void cmuAlignCenter_Click(object sender, EventArgs e)
    {
      if (this.selectedElement != null)
      {
        this.selectedElement.Center = new PointF(
          this.PresentationSize.Width / 2,
          this.PresentationSize.Height / 2);

        this.DrawForeground(false);
      }
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="ToolStripMenuItem"/> <see cref="cmuAlignLeft"/>.
    /// Aligns the selected element at the left order of the <see cref="Picture"/>.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void cmuAlignLeft_Click(object sender, EventArgs e)
    {
      if (this.selectedElement != null)
      {
        this.selectedElement.Location =
          new PointF(0, this.selectedElement.Location.Y);
        this.DrawForeground(false);
      }
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="ToolStripMenuItem"/> <see cref="cmuAlignRight"/>.
    /// Aligns the selected element at the right border of the <see cref="Picture"/>.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void cmuAlignRight_Click(object sender, EventArgs e)
    {
      if (this.selectedElement != null)
      {
        this.selectedElement.Location = new PointF(
          this.PresentationSize.Width - this.selectedElement.Width,
          this.selectedElement.Location.Y);
        this.DrawForeground(false);
      }
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="ToolStripMenuItem"/> <see cref="cmuAlignTop"/>.
    /// Aligns the selected element at the top of the <see cref="Picture"/>.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void cmuAlignTop_Click(object sender, EventArgs e)
    {
      if (this.selectedElement != null)
      {
        this.selectedElement.Location = new PointF(this.selectedElement.Location.X, 0);
        this.DrawForeground(false);
      }
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="ToolStripMenuItem"/> <see cref="cmuAlignBottom"/>.
    /// Aligns the selected element at the bottom of the <see cref="Picture"/>.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void cmuAlignBottom_Click(object sender, EventArgs e)
    {
      if (this.selectedElement != null)
      {
        this.selectedElement.Location = new PointF(
          this.selectedElement.Location.X,
          this.PresentationSize.Height - this.selectedElement.Height);
        this.DrawForeground(false);
      }
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="ToolStripMenuItem"/> <see cref="cmuSizeFullScreen"/>.
    /// Resizes the selected element to full screen picture size.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void cmuSizeFullScreen_Click(object sender, EventArgs e)
    {
      if (this.selectedElement != null)
      {
        this.selectedElement.Size = this.PresentationSize;

        this.selectedElement.Center = new PointF(
          this.PresentationSize.Width / 2,
          this.PresentationSize.Height / 2);

        this.DrawForeground(false);
      }
    }

    #endregion //CONTEXTMENU

    #endregion //WINDOWSEVENTHANDLER

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler for Custom Defined Events                                    //
    ///////////////////////////////////////////////////////////////////////////////
    #region CUSTOMEVENTHANDLER

    /// <summary>
    /// Handles the arrow key up event by moving a selected object 
    /// by the <see cref="MOVEOFFSET"/> value up.
    /// </summary>
    private void OnArrowKeyUp()
    {
      this.MoveBoundsOfSelectedObject(0, -MOVEOFFSET);
    }

    /// <summary>
    /// Handles the arrow key down event by moving a selected object 
    /// by the <see cref="MOVEOFFSET"/> value down.
    /// </summary>
    private void OnArrowKeyDown()
    {
      this.MoveBoundsOfSelectedObject(0, MOVEOFFSET);
    }

    /// <summary>
    /// Handles the arrow key left event by moving a selected object 
    /// by the <see cref="MOVEOFFSET"/> value to the left.
    /// </summary>
    private void OnArrowKeyLeft()
    {
      this.MoveBoundsOfSelectedObject(-MOVEOFFSET, 0);
    }

    /// <summary>
    /// Handles the arrow key right event by moving a selected object 
    /// by the <see cref="MOVEOFFSET"/> value to the right.
    /// </summary>
    private void OnArrowKeyRight()
    {
      this.MoveBoundsOfSelectedObject(MOVEOFFSET, 0);
    }

    /// <summary>
    /// Handles the add key event by moving a selected object 
    /// one index up in the <see cref="Picture.Elements"/> list.
    /// </summary>
    private void OnAddKey()
    {
      if (this.selectedElement != null)
      {
        this.Elements.MoveUp(this.selectedElement);
        this.Invalidate();
      }
    }

    /// <summary>
    /// Handles the subtract key event by moving a selected object 
    /// one index down in the <see cref="Picture.Elements"/> list.
    /// </summary>
    private void OnSubtractKey()
    {
      if (this.selectedElement != null)
      {
        this.Elements.MoveDown(this.selectedElement);
        this.Invalidate();
      }
    }

    /// <summary>
    /// Handles the page down key event by moving a selected object 
    /// to the head of the <see cref="Picture.Elements"/> list.
    /// </summary>
    private void OnPageDownKey()
    {
      if (this.selectedElement != null)
      {
        this.Elements.ToHead(this.selectedElement);
        this.Invalidate();
      }
    }

    /// <summary>
    /// Handles the page up key event by moving a selected object 
    /// to the tail of the <see cref="Picture.Elements"/> list.
    /// </summary>
    private void OnPageUpKey()
    {
      if (this.selectedElement != null)
      {
        this.Elements.ToTail(this.selectedElement);
        this.Invalidate();
      }
    }

    #endregion //CUSTOMEVENTHANDLER

    #endregion //EVENTS

    ///////////////////////////////////////////////////////////////////////////////
    // Methods for doing main class job                                          //
    ///////////////////////////////////////////////////////////////////////////////
    #region METHODS

    /// <summary>
    /// This method checks the currently selected element beeing moved if the given
    /// movement position is in one of the alignment rectangles for snapping.
    /// If it is so, it changes the given point to be snapped.
    /// </summary>
    /// <param name="point">Ref. A <see cref="Point"/> with the new location to check
    /// for snapping.</param>
    private void CheckElementForAlignments(ref Point point)
    {
      // Center of element is beeing moved
      if (this.activeGrabHandle != null)
      {
        if (this.activeGrabHandle.GrabHandlePosition == GrabHandle.HandlePosition.Center)
        {
          bool aligned = false;
          foreach (GrabHandle handle in this.selectedElement.GrabHandles)
          {
            Point position = Point.Round(handle.Location);
            if (this.CheckForAlignments(ref position))
            {
              Point movement = new Point(
                (int)(handle.Location.X - position.X),
                (int)(handle.Location.Y - position.Y));

              this.selectedElement.GrabHandleMoved(ref this.activeGrabHandle, position);
              aligned = true;
              break;
            }
          }

          if (!aligned)
          {
            this.selectedElement.GrabHandleMoved(ref this.activeGrabHandle, point);
          }
        }
        else
        {
          this.CheckForAlignments(ref point);
          this.selectedElement.GrabHandleMoved(ref this.activeGrabHandle, point);
        }
      }
    }

    /// <summary>
    /// This method checks the given point to be contained in one of the
    /// snap rectangles <see cref="topMargin"/>, <see cref="bottomMargin"/>,
    /// <see cref="leftMargin"/>, <see cref="rightMargin"/>.
    /// When it is contained, the point is snapped to the margin of the 
    /// containing rectangle.
    /// </summary>
    /// <param name="point">Ref. A <see cref="Point"/> with the new location to check
    /// for snapping.</param>
    /// <returns><strong>True</strong>, if point was modified, otherwise
    /// <strong>false</strong>.</returns>
    private bool CheckForAlignments(ref Point point)
    {
      bool modified = false;
      if (this.topMargin.Contains(point))
      {
        point.Y = 0;
        modified = true;
      }
      else if (this.bottomMargin.Contains(point))
      {
        point.Y = this.Bottom;
        modified = true;
      }

      if (this.leftMargin.Contains(point))
      {
        point.X = 0;
        modified = true;
      }
      else if (this.rightMargin.Contains(point))
      {
        point.X = this.Right;
        modified = true;
      }

      if (this.centerArea.Contains(point))
      {
        Point centerPoint = this.Location;
        centerPoint.Offset(this.Width / 2, this.Height / 2);
        point = centerPoint;
        modified = true;
      }

      return modified;
    }

    /// <summary>
    /// Method for new shape creation standards.
    /// It should be called when the <see cref="newShape"/> field is set 
    /// and the element is going to be added.
    /// </summary>
    /// <param name="usedCursor">A <see cref="Cursor"/> to use during creation.</param>
    private void StartCreation(Cursor usedCursor)
    {
      // Reset selected element if there is any.
      this.ResetSelectedElement();
      this.state = BuildState.BuildStarted;
      this.Cursor = usedCursor;
      this.DrawForeground(false);
    }

    #endregion //METHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER

    /// <summary>
    /// Checks if two points are nearer than MAX_DISTANCE_POLYLINE_CLOSE
    /// </summary>
    /// <remarks>Polyline is automatically closed, if they are.</remarks>
    /// <param name="point1">A <see cref="PointF"/> with point one </param>
    /// <param name="point2">A <see cref="PointF"/> with point two</param>
    /// <returns><strong>True</strong>, if points are nearer than MAX_DISTANCE_POLYLINE_CLOSE,
    /// otherwise <strong>false</strong>.</returns>
    private bool PointsAreNear(PointF point1, PointF point2)
    {
      if (VGPolyline.Distance(point1, point2) < MAXDISTANCEPOLYLINECLOSE)
      {
        return true;
      }
      else
      {
        return false;
      }
    }

    /// <summary>
    /// Transforms given mouse point from screen coordinates to stimulus
    /// screen coordinates.
    /// </summary>
    /// <param name="mouseLocation">A <see cref="Point"/> with the
    /// mouse location to transform.</param>
    /// <returns>A <see cref="PointF"/> with the transformed position.</returns>
    private PointF GetTransformedMouseLocation(Point mouseLocation)
    {
      PointF[] mousePts = { mouseLocation };
      this.ScreenToStimulus.TransformPoints(mousePts);
      return mousePts[0];
    }

    /// <summary>
    /// This method moves a selected object, if there is one by the amount
    /// given in the parameters in x and y direction.
    /// </summary>
    /// <param name="offsetX">A <see cref="int"/> with the offset in x-direction.</param>
    /// <param name="offsetY">A <see cref="int"/> with the offset in y-direction.</param>
    private void MoveBoundsOfSelectedObject(int offsetX, int offsetY)
    {
      if (this.selectedElement != null)
      {
        RectangleF newBounds = this.selectedElement.Bounds;
        newBounds.Offset(offsetX, offsetY);
        this.selectedElement.Bounds = newBounds;
        this.DrawForeground(false);
        this.Focus();
        this.OnShapeChanged(new ShapeEventArgs(this.selectedElement));
      }
    }

    /// <summary>
    /// Creates a <see cref="RectangleF"/> with new bounds out of
    /// the given mouseLocation and the <see cref="firstClickPoint"/>.
    /// It resolves the four cases the both points can have.
    /// </summary>
    /// <param name="mouseLocation">A <see cref="PointF"/> with the new mouse location.</param>
    /// <returns>A <see cref="RectangleF"/> with new bounding rectangle for the given points.</returns>
    private RectangleF GetBoundingRectFromMousePosition(PointF mouseLocation)
    {
      float x1 = this.firstClickPoint.X;
      float y1 = this.firstClickPoint.Y;
      float x2 = mouseLocation.X;
      float y2 = mouseLocation.Y;
      RectangleF newBoundingRect = new RectangleF();

      if (x2 > x1)
      {
        if (y2 > y1)
        {
          // Case 1
          newBoundingRect = RectangleF.FromLTRB(x1, y1, x2, y2);
        }
        else
        {
          // Case 4
          newBoundingRect = RectangleF.FromLTRB(x1, y2, x2, y1);
        }
      }
      else if (y2 > y1)
      {
        // Case 2
        newBoundingRect = RectangleF.FromLTRB(x2, y1, x1, y2);
      }
      else
      {
        // Case 3
        newBoundingRect = RectangleF.FromLTRB(x2, y2, x1, y1);
      }

      return newBoundingRect;
    }

    /// <summary>
    /// This method initially sizes the snap areas at the margin and center
    /// of the picture.
    /// </summary>
    private void InitializeSnapAreas()
    {
      this.leftMargin = new Rectangle(0, 0, SNAPPADDING, this.Height);
      this.rightMargin = new Rectangle(this.Width - SNAPPADDING, 0, SNAPPADDING, this.Height);
      this.topMargin = new Rectangle(0, 0, this.Width, SNAPPADDING);
      this.bottomMargin = new Rectangle(0, this.Height - SNAPPADDING, this.Width, SNAPPADDING);
      this.centerArea = new Rectangle(
        (this.Width / 2) - (SNAPPADDING / 2),
        (this.Height / SNAPPADDING) - (SNAPPADDING / 2),
        SNAPPADDING,
        SNAPPADDING);
    }

    #endregion //HELPER
  }
}
