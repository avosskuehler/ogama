// <copyright file="VGScrollImage.cs" company="FU Berlin">
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

namespace VectorGraphics.Elements
{
  using System;
  using System.ComponentModel;
  using System.Drawing;
  using System.Drawing.Drawing2D;
  using System.Drawing.Imaging;
  using System.IO;
  using System.Text;
  using System.Windows.Forms;
  using System.Xml.Serialization;
  using VectorGraphics.Tools;

  /// <summary>
  /// Inherited from <see cref="VGImage"/>. 
  /// A serializable class that represents a vector graphics image that is scrollable.
  /// </summary>
  [Serializable]
  public class VGScrollImage : VGImage
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
    /// Initializes a new instance of the VGScrollImage class.
    /// When newShapeDrawAction is set to None, only the image will
    /// be drawn, with Edge an additional border is drawn,
    /// with fill an additional (hopefully transparent) fill is drawn over the image.
    /// </summary>
    /// <param name="newShapeDrawAction">Drawing action: Edge, Fill, Both, None</param>
    /// <param name="newPen">Pen for additional borderline.</param>
    /// <param name="newBrush">Brush for additional fills.</param>
    /// <param name="newFont">Font for drawing name</param>
    /// <param name="newFontColor">Font color for drawing name.</param>
    /// <param name="newImageFile">filename without path</param>
    /// <param name="newPath">path to image file</param>
    /// <param name="newLayout"><see cref="ImageLayout"/> of the image</param>
    /// <param name="newAlpha">The transparency alpha for this image.0=transparent,1=opaque</param>
    /// <param name="newCanvas"><see cref="Size"/> of the owning original canvas</param>
    /// <param name="newStyleGroup">Group Enumeration, <see cref="VGStyleGroup"/></param>
    /// <param name="newName">Name of Element</param>
    /// <param name="newElementGroup">Element group description</param>
    public VGScrollImage(
      ShapeDrawAction newShapeDrawAction,
      Pen newPen,
      Brush newBrush,
      Font newFont,
      Color newFontColor,
      string newImageFile,
      string newPath,
      ImageLayout newLayout,
      float newAlpha,
      Size newCanvas,
      VGStyleGroup newStyleGroup,
      string newName,
      string newElementGroup)
      : base(
      newShapeDrawAction,
      newPen,
      newBrush,
      newFont,
      newFontColor,
      newImageFile,
      newPath,
      newLayout,
      newAlpha,
      newCanvas,
      newStyleGroup,
      newName,
      newElementGroup,
      false)
    {
    }

    /// <summary>
    /// Initializes a new instance of the VGScrollImage class.
    /// Creates a new VGImage with the given image and properties,
    /// that is not persistent, because it is not saved to disk.
    /// </summary>
    /// <param name="newImage">An <see cref="Image"/> that should be displayed.</param>
    /// <param name="newLayout">An <see cref="ImageLayout"/> for the image.</param>
    /// <param name="newCanvas"><see cref="Size"/> of the owning original canvas</param>
    public VGScrollImage(Image newImage, ImageLayout newLayout, Size newCanvas)
      : base(newImage, newLayout, newCanvas)
    {
    }

    /// <summary>
    /// Initializes a new instance of the VGScrollImage class.
    /// Parameterless constructor. Used for serialization.
    /// </summary>
    protected VGScrollImage()
    {
    }

    /// <summary>
    /// Initializes a new instance of the VGScrollImage class.
    /// Clone Constructor. Creates new <see cref="VGImage"/> that is
    /// identical to the given <see cref="VGImage"/>.
    /// </summary>
    /// <param name="cloneImage"><see cref="VGImage"/> to clone</param>
    private VGScrollImage(VGScrollImage cloneImage)
      : base(
      cloneImage.ShapeDrawAction,
      cloneImage.Pen,
      cloneImage.Brush,
      cloneImage.Font,
      cloneImage.FontColor,
      cloneImage.Filename,
      cloneImage.Filepath,
      cloneImage.Layout,
      cloneImage.Alpha,
      cloneImage.Canvas,
      cloneImage.StyleGroup,
      cloneImage.Name,
      cloneImage.ElementGroup,
      false)
    {
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Overridden. Returns the images size if it is available otherwise
    /// the elements size.
    /// </summary>
    public override SizeF Size
    {
      get
      {
        if (this.Image != null)
        {
          return this.Image.Size;
        }
        else
        {
          return base.Size;
        }
      }

      set
      {
        base.Size = value;
      }
    }

    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Public methods                                                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region PUBLICMETHODS
    #endregion //PUBLICMETHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Inherited methods                                                         //
    ///////////////////////////////////////////////////////////////////////////////
    #region OVERRIDES

    /// <summary>
    /// Overridden <see cref="VGElement.Draw(Graphics)"/>.  
    /// Draws the image to the given graphics context.
    /// </summary>
    /// <param name="graphics">Graphics context to draw to</param>
    /// <exception cref="ArgumentNullException">Thrown, when graphics object is null.</exception>
    public override void Draw(Graphics graphics)
    {
      if (graphics == null)
      {
        throw new ArgumentNullException("Graphics object should not be null.");
      }

      if (this.Image == null)
      {
        if (!this.CreateInternalImage())
        {
          return;
        }
      }

      graphics.DrawImage(this.Image, this.Location);

      if (this.ShapeDrawAction == (this.ShapeDrawAction | ShapeDrawAction.Fill))
      {
        Rectangle drawingCanvas = new Rectangle(0, 0, this.Image.Width, this.Image.Height);
        RectangleF drawing_rectangle = drawingCanvas;
        graphics.FillRectangle(this.Brush, drawing_rectangle);
      }
    }

    /// <summary>
    /// Overridden <see cref="VGElement.Reset()"/>. 
    /// Reset the current image element to
    /// default values.
    /// </summary>
    public override void Reset()
    {
      base.Reset();
    }

    /// <summary>
    /// Overridden <see cref="Object.ToString()"/> method.
    /// Returns the <see cref="VGImage"/> properties as a human readable string.
    /// </summary>
    /// <returns>A <see cref="string"/> that represents this <see cref="VGImage"/>.</returns>
    public override string ToString()
    {
      StringBuilder sb = new StringBuilder();
      sb.Append("VGScrollImage, File: ");
      sb.Append(Path.GetFileName(this.Filename));
      return sb.ToString();
    }

    /// <summary>
    /// Overridden <see cref="VGElement.ToShortString()"/> method.
    /// Returns the main <see cref="VGImage"/> properties as a human readable string.
    /// </summary>
    /// <returns>A <see cref="string"/> that represents the 
    /// current <see cref="VGImage"/> in short form with its main properties.</returns>
    public override string ToShortString()
    {
      StringBuilder sb = new StringBuilder();
      sb.Append("Image: ");
      string text = Path.GetFileName(this.Filename);
      sb.Append(text.Substring(0, text.Length > 12 ? Math.Max(12, text.Length - 1) : text.Length));
      sb.Append(" ...");
      return sb.ToString();
    }

    /// <summary>
    /// Releases the resources used by the element.
    /// </summary>
    public override void Dispose()
    {
      base.Dispose();
      if (this.Image != null)
      {
        this.Image.Dispose();
        this.Image = null;
      }
    }

    /// <summary>
    /// Overridden <see cref="VGElement.CloneCore()"/>.  
    /// Creates a excact copy of given <see cref="VGScrollImage"/>
    /// </summary>
    /// <returns>Excact copy of this <see cref="VGScrollImage"/></returns>
    protected override VGElement CloneCore()
    {
      return new VGScrollImage(this);
    }

    /// <summary>
    /// Overridden. This resets the layout to <see cref="ImageLayout.None"/>,
    /// because the image is going to be moved.
    /// </summary>
    /// <param name="translationMatrix">Translation Matrix, that performs the translation.</param>
    protected override void NewPosition(Matrix translationMatrix)
    {
      this.Layout = ImageLayout.None;
      base.NewPosition(translationMatrix);
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
    #endregion //HELPER
  }
}
