// <copyright file="ImageDialog.cs" company="FU Berlin">
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
  using System.Drawing.Drawing2D;
  using System.IO;
  using System.Text;
  using System.Windows.Forms;

  using Ogama.ExceptionHandling;
  using Ogama.Modules.Common.Controls;

  using VectorGraphics.Elements;
  using VectorGraphics.Tools.CustomEventArgs;

  /// <summary>
  /// This dialog <see cref="Form"/> is used to initially define an 
  /// image that can be added to a slide.
  /// </summary>
  public partial class ImageDialog : Form
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
    /// Saves the full filename with path of the image
    /// specified in this dialog.
    /// </summary>
    private string fullFilename;

    /// <summary>
    /// Saves a copy of the image selected in this dialog.
    /// </summary>
    private Image image;

    /// <summary>
    /// This member saves a matrix that transforms form the stimulus design panel
    /// to this preview panel. It is used to display the alignment of the
    /// image in a convincing way.
    /// </summary>
    private Matrix canvasToPreviewTransform;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the ImageDialog class.
    /// </summary>
    public ImageDialog()
    {
      this.InitializeComponent();
      this.RepopulateImageLayoutDropDown();
      this.audioControl.PathToCopyTo = Document.ActiveDocument.ExperimentSettings.SlideResourcesPath;
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets the full filename with path of the image
    /// specified in this dialog.
    /// </summary>
    /// <value>A <see cref="string"/> with image filename with path.</value>
    public string ImageFile
    {
      get { return this.fullFilename; }
    }

    /// <summary>
    /// Gets the <see cref="ImageLayout"/> of the image
    /// specified in this dialog to use in the slide.
    /// </summary>
    /// <value>An <see cref="ImageLayout"/> to use for this image.</value>
    public ImageLayout ImageLayout
    {
      get
      {
        System.Windows.Forms.ImageLayout layout = System.Windows.Forms.ImageLayout.None;
        try
        {
          layout = (ImageLayout)Enum.Parse(typeof(ImageLayout), this.cbbImageLayout.SelectedItem.ToString());
        }
        catch (ArgumentException)
        {
          this.RepopulateImageLayoutDropDown();
        }

        return layout;
      }
    }

    /// <summary>
    /// Gets or sets the <see cref="VGImage"/> element that should be added to the slide.
    /// </summary>
    /// <value>A <see cref="VGImage"/> that should be added to the slide.</value>
    public VGImage NewImage
    {
      get
      {
        if (this.fullFilename == string.Empty)
        {
          throw new FileNotFoundException("The image file could not be found");
        }

        VGImage image = new VGImage(
          this.pbcImageBorder.DrawAction,
          this.pbcImageBorder.NewPen,
          this.pbcImageBorder.NewBrush,
          this.pbcImageBorder.NewFont,
          this.pbcImageBorder.NewFontColor,
          Path.GetFileName(this.fullFilename),
          Path.GetDirectoryName(this.fullFilename),
          this.ImageLayout,
          1f,
          new Size(Document.ActiveDocument.ExperimentSettings.WidthStimulusScreen, Document.ActiveDocument.ExperimentSettings.HeightStimulusScreen),
          VGStyleGroup.AOI_NORMAL,
          this.pbcImageBorder.NewName,
          string.Empty,
          false);

        image.Sound = this.audioControl.Sound;
        return image;
      }

      set
      {
        this.RepopulateImageLayoutDropDown();
        this.pbcImageBorder.DrawAction = value.ShapeDrawAction;
        this.pbcImageBorder.NewPen = value.Pen;
        this.pbcImageBorder.NewBrush = value.Brush;
        this.pbcImageBorder.NewName = value.Name;
        this.pbcImageBorder.NewFont = value.Font;
        this.pbcImageBorder.NewFontColor = value.FontColor;
        this.fullFilename = Path.Combine(value.Filepath, value.Filename);
        this.cbbImageLayout.SelectedItem = value.Layout.ToString();
        this.txbImageFilename.Text = value.Filename;
        this.image = Image.FromFile(this.fullFilename);
        this.audioControl.Sound = value.Sound;
        this.panelPreview.Refresh();
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
    /// The <see cref="Form.Load"/> event handler.
    /// Initializes the controls.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A empty <see cref="EventArgs"/></param>
    private void frmNewImage_Load(object sender, EventArgs e)
    {
      // Start with an transparent fill, to understand
      // how the fill works in images.
      this.pbcImageBorder.NewBrush = new SolidBrush(Color.FromArgb(125, Color.Red));

      // Scale preview to proportions of presentation screen
      int screenWidth = Document.ActiveDocument.ExperimentSettings.WidthStimulusScreen;
      int screenHeight = Document.ActiveDocument.ExperimentSettings.HeightStimulusScreen;
      float screenRatio = (float)screenHeight / (float)screenWidth;
      int newHeight = (int)(this.panelPreview.Width * screenRatio);
      int newWidth = (int)(this.panelPreview.Height / screenRatio);
      if (newHeight <= this.panelPreview.Height)
      {
        this.panelPreview.Height = newHeight;
      }
      else
      {
        this.panelPreview.Width = newWidth;
      }

      this.canvasToPreviewTransform = this.GetTransformationMatrix();
    }

    /// <summary>
    /// <see cref="Control.Click"/> event handler 
    /// for the <see cref="Button"/> <see cref="btnOpenImageFile"/>
    /// Raises a <see cref="OpenFileDialog"/> and updates the <see cref="txbImageFilename"/>.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A empty <see cref="EventArgs"/></param>
    private void btnOpenImageFile_Click(object sender, EventArgs e)
    {
      OpenFileDialog dlg = new OpenFileDialog();
      dlg.Filter = "Images (*.BMP;*.JPG;*.GIF;*.PNG;*.ICO;*.WMF)|*.BMP;*.JPG;*.GIF;*.PNG;*.ICO;*.WMF|All files (*.*)|*.*";
      dlg.Title = "Select stimulus image ...";
      dlg.InitialDirectory = Document.ActiveDocument.ExperimentSettings.SlideResourcesPath;

      if (dlg.ShowDialog() == DialogResult.OK)
      {
        string fileName = dlg.FileName;
        if (!File.Exists(fileName))
        {
          // Erase textbox entry
          this.txbImageFilename.Text = string.Empty;
          this.fullFilename = string.Empty;
          return;
        }

        try
        {
          this.image = Image.FromFile(fileName);
        }
        catch (Exception)
        {
          string message = "Sorry, but Ogama could not read this image file." +
            "Please try again using another format, e.g. .jpg";
          ExceptionMethods.ProcessMessage("Image could not be read", message);
          return;
        }

        string templatePath = Document.ActiveDocument.ExperimentSettings.SlideResourcesPath;
        this.fullFilename = fileName;
        if (Path.GetDirectoryName(fileName) != templatePath)
        {
          string target = Path.Combine(templatePath, Path.GetFileName(fileName));
          if (!File.Exists(target))
          {
            File.Copy(fileName, target);
            string message = "A copy of this image file is saved to the following slide resources folder of the current project :" +
              Environment.NewLine + target + Environment.NewLine +
              "This is done because of easy movement of experiments between different computers or locations.";

            ExceptionMethods.ProcessMessage("File copied", message);
          }

          this.fullFilename = target;
        }

        this.txbImageFilename.Text = Path.GetFileName(this.fullFilename);
        this.toolTip1.SetToolTip(this.txbImageFilename, this.fullFilename);
        this.panelPreview.Refresh();
      }
    }

    /// <summary>
    /// The <see cref="ComboBox.SelectionChangeCommitted"/> event handler.
    /// <see cref="ImageLayout"/> has been changed, so update preview.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A empty <see cref="EventArgs"/></param>
    private void cbbImageLayout_SelectionChangeCommitted(object sender, EventArgs e)
    {
      this.panelPreview.Refresh();
    }

    /// <summary>
    /// The <see cref="Control.Paint"/> event handler for
    /// the <see cref="Panel"/> <see cref="panelPreview"/>.
    /// Draws an preview of the designed image with the current settings.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A <see cref="PaintEventArgs"/> with the event data.</param>
    private void panelPreview_Paint(object sender, PaintEventArgs e)
    {
      e.Graphics.Transform = this.canvasToPreviewTransform;

      Rectangle canvas = new Rectangle(0, 0, Document.ActiveDocument.PresentationSize.Width, Document.ActiveDocument.PresentationSize.Height);
      Rectangle drawing_rectangle = canvas;
      if (this.pbcImageBorder.DrawAction == (this.pbcImageBorder.DrawAction | ShapeDrawAction.Edge))
      {
        drawing_rectangle.Inflate(-(int)this.pbcImageBorder.NewPen.Width, -(int)this.pbcImageBorder.NewPen.Width);
      }

      if (this.image != null)
      {
        switch (this.ImageLayout)
        {
          case ImageLayout.Tile:
            using (TextureBrush b = new TextureBrush(this.image, WrapMode.Tile))
            {
              e.Graphics.FillRectangle(b, drawing_rectangle);
            }

            break;
          case ImageLayout.Center:
            drawing_rectangle.Location = new Point(
              (int)(canvas.Width / 2 - this.image.Width / 2),
              (int)(canvas.Height / 2 - this.image.Height / 2));

            drawing_rectangle.Size = this.image.Size;
            e.Graphics.DrawImage(this.image, drawing_rectangle);
            break;
          case ImageLayout.None:
            drawing_rectangle.Size = this.image.Size;
            e.Graphics.DrawImage(this.image, drawing_rectangle);
            break;
          case ImageLayout.Stretch:
            e.Graphics.DrawImage(this.image, drawing_rectangle);
            break;
          case ImageLayout.Zoom:
            if ((float)this.image.Width / (float)this.image.Height < (float)drawing_rectangle.Width / (float)drawing_rectangle.Height)
            {
              drawing_rectangle.Width = (int)(this.image.Width * ((float)drawing_rectangle.Height / (float)this.image.Height));
              drawing_rectangle.X = (int)((canvas.Width - drawing_rectangle.Width) / 2);
            }
            else
            {
              drawing_rectangle.Height = (int)(this.image.Height * ((float)drawing_rectangle.Width / (float)this.image.Width));
              drawing_rectangle.Y = (int)((canvas.Height - drawing_rectangle.Height) / 2);
            }

            e.Graphics.DrawImage(this.image, drawing_rectangle);
            break;
        }
      }

      if (this.pbcImageBorder.DrawAction == (this.pbcImageBorder.DrawAction | ShapeDrawAction.Fill))
      {
        e.Graphics.FillRectangle(this.pbcImageBorder.NewBrush, drawing_rectangle);
      }

      if (this.pbcImageBorder.DrawAction == (this.pbcImageBorder.DrawAction | ShapeDrawAction.Edge))
      {
        RectangleF halfInlineRect = drawing_rectangle;
        halfInlineRect.Inflate(-this.pbcImageBorder.NewPen.Width / 2, -this.pbcImageBorder.NewPen.Width / 2);
        e.Graphics.DrawRectangle(this.pbcImageBorder.NewPen, halfInlineRect.X, halfInlineRect.Y, halfInlineRect.Width, halfInlineRect.Height);
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
      dlg.HelpCaption = "How to: Define the position of the image.";
      StringBuilder sb = new StringBuilder();
      sb.Append("When the button is clicked you can specify position and size of the image by specifying the bounding rectangle with the left mouse button (click and pull) only when image layout is set to 'None'.");
      dlg.HelpMessage = sb.ToString();
      dlg.ShowDialog();
    }

    #endregion //WINDOWSEVENTHANDLER

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler for Custom Defined Events                                    //
    ///////////////////////////////////////////////////////////////////////////////
    #region CUSTOMEVENTHANDLER

    /// <summary>
    /// The <see cref="OgamaControls.PenAndBrushControl.ShapePropertiesChanged"/> event handler.
    /// Updates the preview with the new edge and fill styles.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A <see cref="ShapePropertiesChangedEventArgs"/> with the event data.</param>
    private void pbcImageBorder_ShapePropertiesChanged(object sender, ShapePropertiesChangedEventArgs e)
    {
      this.panelPreview.Refresh();
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
    #endregion //METHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER

    /// <summary>
    /// This method repopulates the image layout dropdown with the
    /// <see cref="ImageLayout"/> values.
    /// </summary>
    private void RepopulateImageLayoutDropDown()
    {
      this.cbbImageLayout.Items.Clear();
      this.cbbImageLayout.Items.AddRange(Enum.GetNames(typeof(ImageLayout)));
      this.cbbImageLayout.SelectedIndex = 2;
    }

    /// <summary>
    /// Calculates the transformation matrix that transforms the output stimulus
    /// picture to the currently visible drawing bounds of the owning picture
    /// </summary>
    /// <returns>A scaling and translating <see cref="Matrix"/>
    /// that transforms output coordinates in client size coordinates.</returns>
    private Matrix GetTransformationMatrix()
    {
      int widthData = Document.ActiveDocument.PresentationSize.Width;
      int heightData = Document.ActiveDocument.PresentationSize.Height;
      if (this.Width != 0 && this.Height != 0)
      {
        float largestRatio = (float)Math.Max(
          (double)widthData / (double)this.panelPreview.Width,
          (double)heightData / (double)this.panelPreview.Height);

        Matrix mx = new Matrix(1.0f / largestRatio, 0, 0, 1.0f / largestRatio, 0, 0);
        float posX = (float)(this.panelPreview.Width * largestRatio / 2 - widthData / 2);
        float posY = (float)(this.panelPreview.Height * largestRatio / 2 - heightData / 2);
        mx.Translate(posX, posY);
        return mx;
      }

      return new Matrix();
    }

    #endregion //HELPER
  }
}