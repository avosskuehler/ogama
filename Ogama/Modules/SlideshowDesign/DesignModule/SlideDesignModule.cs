// <copyright file="SlideDesignModule.cs" company="FU Berlin">
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
  using System.Collections.Generic;
  using System.Drawing;
  using System.IO;
  using System.Windows.Forms;

  using Ogama.ExceptionHandling;
  using Ogama.MainWindow;
  using Ogama.Modules.Common.FormTemplates;
  using Ogama.Modules.Common.SlideCollections;
  using Ogama.Modules.SlideshowDesign.DesignModule.StimuliDialogs;

  using OgamaControls;

  using VectorGraphics.Elements;
  using VectorGraphics.Elements.ElementCollections;
  using VectorGraphics.StopConditions;
  using VectorGraphics.Tools.CustomEventArgs;

  /// <summary>
  /// Inherits <see cref="FormWithPicture"/>.
  /// This module is used for slide creation.
  /// In its user interface all slide parameters can be modified
  /// along with the creation of new slide elements.
  /// </summary>
  public partial class SlideDesignModule : FormWithPicture
  {
    ///////////////////////////////////////////////////////////////////////////////
    // Defining Constants                                                        //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTANTS

    /// <summary>
    /// Default <see cref="int"/> value for the slide duration in seconds.
    /// </summary>
    public const int SLIDEDURATIONINS = 5;

    /// <summary>
    /// Default <see cref="Color"/>value for the slides background color.
    /// </summary>
    private static Color defaultBgColor = Color.Black;

    /// <summary>
    /// Default <see cref="Color"/> value for the text color.
    /// </summary>
    private static Color defaultTextColor = Color.Wheat;

    /// <summary>
    /// Default font for the instructions.
    /// </summary>
    private static Font defaultFont = new Font("Arial", 28);

    #endregion //CONSTANTS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Variables, Enumerations, Events                                  //
    ///////////////////////////////////////////////////////////////////////////////
    #region FIELDS

    /// <summary>
    /// Saves an optional shape type.
    /// If this is not <see cref="StimuliTypes.None"/> the referring
    /// creating dialog will be opened during load.
    /// </summary>
    private StimuliTypes stimulusTypeToCreate;

    /// <summary>
    /// Indicates that no UI updates are required.
    /// </summary>
    private bool isInitializingSelectedShape;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the SlideDesignModule class.
    /// </summary>
    /// <param name="newStimulusType">An <see cref="StimuliTypes"/>
    /// that indicates the type of shape to create during startup
    /// of dialog. If <see cref="StimuliTypes.None"/>
    /// the dialog will start in standard mode.</param>
    public SlideDesignModule(StimuliTypes newStimulusType)
    {
      this.stimulusTypeToCreate = newStimulusType;
      this.InitializeComponent();

      this.Picture = this.designPicture;
      this.InitializeCustomElements();
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets or sets the proposal for the slide name.
    /// </summary>
    /// <value>A <see cref="string"/> value with the slide name proposal.</value>
    public string SlideName
    {
      get { return this.txbName.Text; }
      set { this.txbName.Text = value; }
    }

    /// <summary>
    /// Sets a description for the slide design form.
    /// </summary>
    /// <value>A <see cref="string"/> value with the slide type description.</value>
    /// <remarks>This text will be displayed as a label at the top of the form.</remarks>
    public string Description
    {
      set { this.dltForm.Description = value; }
    }

    /// <summary>
    /// Gets or sets the slide for this design form.
    /// </summary>
    /// <value>The current used <see cref="Slide"/>.</value>
    /// <exception cref="ArgumentNullException">Thrown when value is null.</exception>
    public Slide Slide
    {
      get
      {
        return this.GetSlide();
      }

      set
      {
        if (value == null)
        {
          throw new ArgumentNullException("Setting 'null' for a slide is not allowed in StimulusDesignModule");
        }

        // Update the forms fields with slides values
        this.PopulateDialogWithSlide((Slide)value.Clone());
      }
    }

    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Public methods                                                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region PUBLICMETHODS

    /// <summary>
    /// Populates given combo box with <see cref="Keys"/> values.
    /// </summary>
    /// <param name="combo">The <see cref="ComboBox"/> to be populated.</param>
    public static void FillKeyCombo(ComboBox combo)
    {
      combo.Items.Clear();
      combo.Items.Add("Any");
      combo.Items.AddRange(Enum.GetNames(typeof(Keys)));
      combo.Items.Remove("F12");
      combo.Items.Remove("ESC");
      combo.Items.Remove("Escape");
      combo.Text = Keys.Space.ToString();
    }

    /// <summary>
    /// Populates given combo box with <see cref="Keys"/> values.
    /// </summary>
    /// <param name="combo">The <see cref="ComboBox"/> to be populated.</param>
    public static void FillMouseButtonCombo(ComboBox combo)
    {
      combo.Items.Clear();
      combo.Items.Add("Any");
      combo.Items.AddRange(Enum.GetNames(typeof(MouseButtons)));
      combo.Text = MouseButtons.Left.ToString();
    }

    /// <summary>
    /// This method deletes the <see cref="ListBox.SelectedItems"/>
    /// collection in the given <see cref="ListBox"/>
    /// </summary>
    /// <param name="listBox">The <see cref="ListBox"/> for which
    /// the selected items should be deleted.</param>
    public static void DeleteSelectedItems(ListBox listBox)
    {
      List<object> selectedObjects = new List<object>();
      foreach (object obj in listBox.SelectedItems)
      {
        selectedObjects.Add(obj);
      }

      foreach (object obj in selectedObjects)
      {
        listBox.Items.Remove(obj);
      }
    }

    #endregion //PUBLICMETHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Inherited methods                                                         //
    ///////////////////////////////////////////////////////////////////////////////
    #region OVERRIDES

    /// <summary>
    /// This methods is used to initialize elements that are not
    /// initialized in the designer.
    /// </summary>
    protected override void InitializeCustomElements()
    {
      base.InitializeCustomElements();
      this.pnlCanvas.Resize += new EventHandler(this.pnlCanvas_Resize);

      // Properties
      this.audioControl.PathToCopyTo = Document.ActiveDocument.ExperimentSettings.SlideResourcesPath;

      // Initialize combo boxes and property pages.

      // Timing Tab
      FillMouseButtonCombo(this.cbbMouseButtons);
      FillKeyCombo(this.cbbKeys);
      this.rdbTime.Checked = true;
      this.nudTime.Value = SLIDEDURATIONINS;

      // Mouse Tab
      this.psbMouseCursor.CurrentPosition = new Point(
        Document.ActiveDocument.PresentationSize.Width / 2,
        Document.ActiveDocument.PresentationSize.Height / 2);

      this.psbMouseCursor.StimulusScreenSize = Document.ActiveDocument.PresentationSize;

      // Testing Tab
      FillMouseButtonCombo(this.cbbTestingMouseButtons);
      FillKeyCombo(this.cbbTestingKeys);
      this.cbbTestingTargets.Items.Add(string.Empty);
      this.cbbTestingTargets.Items.Add("Any");

      // Links Tab
      FillMouseButtonCombo(this.cbbLinksMouseButtons);
      FillKeyCombo(this.cbbLinksKeys);
      this.cbbLinksTargets.Items.Add(string.Empty);
      this.cbbLinksTargets.Items.Add("Any");
      List<Trial> trials = Document.ActiveDocument.ExperimentSettings.SlideShow.Trials;
      this.cbbLinksTrial.Items.AddRange(trials.ToArray());
      this.cbbLinksTrial.DisplayMember = "Name";
      this.cbbLinksTrial.ValueMember = "ID";

      // Images Tab
      this.cbbImageLayout.Items.AddRange(Enum.GetNames(typeof(ImageLayout)));

      // Background Tab
      this.bkgAudioControl.PathToCopyTo = Document.ActiveDocument.ExperimentSettings.SlideResourcesPath;
      this.btnBackgroundColor.CurrentColor = defaultBgColor;

      this.gveLayoutDockStyle.EditedType = typeof(DockStyle);
      this.gveLayoutDockStyle.Value = DockStyle.None;

      this.dltForm.Logo = this.Icon.ToBitmap();

      this.rtbInstructions.BackColor = this.btnBackgroundColor.CurrentColor;

      this.nudLayoutLeft.Maximum = Document.ActiveDocument.PresentationSize.Width * 2;
      this.nudLayoutTop.Maximum = Document.ActiveDocument.PresentationSize.Height * 2;
      this.nudLayoutWidth.Maximum = Document.ActiveDocument.PresentationSize.Width * 2;
      this.nudLayoutHeight.Maximum = Document.ActiveDocument.PresentationSize.Height * 2;

      foreach (Slide slide in Document.ActiveDocument.ExperimentSettings.SlideShow.Slides)
      {
        if (!this.cbbCategory.Items.Contains(slide.Category))
        {
          this.cbbCategory.Items.Add(slide.Category);
        }
      }
    }

    /// <summary>
    /// The <see cref="MainForm.EditCopy"/> event handler for the
    /// parent <see cref="Form"/> <see cref="MainForm"/>.
    /// This method handles the edit copy event from main form
    /// by  rendering a copy of the displayed picture 
    /// to clipboard.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    protected override void mainWindow_EditCopy(object sender, EventArgs e)
    {
      if (this.MdiParent.ActiveMdiChild.Name == this.Name)
      {
        this.designPicture.OnCopy();
        ((MainForm)this.MdiParent).StatusLabel.Text = "Selection exported to clipboard.";
      }
    }

    /// <summary>
    /// Overridden. The <see cref="MainForm.EditPaste"/> event handler for the
    /// parent <see cref="Form"/> <see cref="MainForm"/>.
    /// Calls the OnPaste method from the picture.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    protected override void mainWindow_EditPaste(object sender, EventArgs e)
    {
      if (this.MdiParent.ActiveMdiChild.Name == this.Name)
      {
        this.designPicture.OnPaste();
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

    /// <summary>
    /// <see cref="Form.Load"/> event handler, Initializes UI.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void SlideDesignModule_Load(object sender, EventArgs e)
    {
      try
      {
        // Intialize picture
        this.designPicture.PresentationSize = Document.ActiveDocument.PresentationSize;
        this.designPicture.DefaultPen = this.pbcElements.NewPen;
        this.ResizeCanvas();

        this.tacProperties.Visible = false;

        this.tctStimuli.TabPages.Clear();
        this.tctStimuli.TabPages.Add(this.tbpNewStimuli);
      }
      catch (Exception ex)
      {
        ExceptionMethods.HandleException(ex);
      }
    }

    /// <summary>
    /// <see cref="Form.Shown"/> event handler.
    /// If there is a new creation mode set, start the
    /// referring dialog.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void SlideDesignModule_Shown(object sender, EventArgs e)
    {
      this.OpenNewCreationDialog();
    }

    /// <summary>
    /// The <see cref="Form.FormClosing"/> event handler.
    /// Checks for a valid response. If there is none,
    /// cancel closing with a error message.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An <see cref="FormClosingEventArgs"/> that contains the event data.</param>
    private void frmStimulusDesign_FormClosing(object sender, FormClosingEventArgs e)
    {
      if (this.DialogResult == DialogResult.OK && this.lsbStopConditions.Items.Count == 0)
      {
        e.Cancel = true;
        string message = "You did not specify a response condition in the timing section." +
          Environment.NewLine + "At least one has to be created, e.g. Time:5000ms" +
          Environment.NewLine + "Please try again.";
        ExceptionMethods.ProcessMessage("Add response condition", message);
      }
    }

    /// <summary>
    /// The <see cref="Control.KeyDown"/> event handler for the
    /// <see cref="ListBox"/>es.
    /// Wires the delete button to the DeleteSelectedItems method.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A <see cref="KeyEventArgs"/>with the event data.</param>
    private void listBox_KeyDown(object sender, KeyEventArgs e)
    {
      switch (e.KeyCode)
      {
        case Keys.Delete:
          DeleteSelectedItems((ListBox)sender);
          break;
      }
    }

    /// <summary>
    /// The <see cref="NumericUpDown.ValueChanged"/> event handler for the 
    /// <see cref="NumericUpDown"/> <see cref="nudLayoutLeft"/>.
    /// If there is an element selected update its left position with the new 
    /// value from the <see cref="NumericUpDown"/>
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/>.</param>
    private void nudLayoutLeft_ValueChanged(object sender, EventArgs e)
    {
      if (this.designPicture.SelectedElement != null && !this.isInitializingSelectedShape)
      {
        this.ResetImageLayout();

        PointF newLocation = this.designPicture.SelectedElement.Location;
        newLocation.X = (float)this.nudLayoutLeft.Value;
        this.designPicture.SelectedElement.Location = newLocation;
        this.designPicture.RedrawAll();
        this.designPicture.Invalidate();
      }
    }

    /// <summary>
    /// The <see cref="NumericUpDown.ValueChanged"/> event handler for the 
    /// <see cref="NumericUpDown"/> <see cref="nudLayoutTop"/>.
    /// If there is an element selected update its top position with the new 
    /// value from the <see cref="NumericUpDown"/>
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/>.</param>
    private void nudLayoutTop_ValueChanged(object sender, EventArgs e)
    {
      if (this.designPicture.SelectedElement != null && !this.isInitializingSelectedShape)
      {
        this.ResetImageLayout();
        PointF newLocation = this.designPicture.SelectedElement.Location;
        newLocation.Y = (float)this.nudLayoutTop.Value;
        this.designPicture.SelectedElement.Location = newLocation;
        this.designPicture.RedrawAll();
        this.designPicture.Invalidate();
      }
    }

    /// <summary>
    /// The <see cref="NumericUpDown.ValueChanged"/> event handler for the 
    /// <see cref="NumericUpDown"/> <see cref="nudLayoutWidth"/>.
    /// If there is an element selected update its width with the new 
    /// value from the <see cref="NumericUpDown"/>
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/>.</param>
    private void nudLayoutWidth_ValueChanged(object sender, EventArgs e)
    {
      if (this.designPicture.SelectedElement != null && !this.isInitializingSelectedShape)
      {
        this.ResetImageLayout();
        SizeF newSize = this.designPicture.SelectedElement.Size;
        newSize.Width = (float)this.nudLayoutWidth.Value;
        this.designPicture.SelectedElement.Size = newSize;
        this.designPicture.RedrawAll();
        this.designPicture.Invalidate();
      }
    }

    /// <summary>
    /// The <see cref="NumericUpDown.ValueChanged"/> event handler for the 
    /// <see cref="NumericUpDown"/> <see cref="nudLayoutHeight"/>.
    /// If there is an element selected update its height with the new 
    /// value from the <see cref="NumericUpDown"/>
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/>.</param>
    private void NudLayoutHeightValueChanged(object sender, EventArgs e)
    {
      if (this.designPicture.SelectedElement != null && !this.isInitializingSelectedShape)
      {
        this.ResetImageLayout();
        SizeF newSize = this.designPicture.SelectedElement.Size;
        newSize.Height = (float)this.nudLayoutHeight.Value;
        this.designPicture.SelectedElement.Size = newSize;
        this.designPicture.RedrawAll();
        this.designPicture.Invalidate();
      }
    }

    /// <summary>
    /// This method centers the current selected element of the modules picture
    /// if there is any.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/>.</param>
    private void BtnLayoutCenterScreenClick(object sender, EventArgs e)
    {
      if (this.designPicture.SelectedElement != null && !this.isInitializingSelectedShape)
      {
        this.ResetImageLayout();
        this.designPicture.SelectedElement.Center = new PointF(
         this.designPicture.PresentationSize.Width / 2,
         this.designPicture.PresentationSize.Height / 2);
        this.UpdateShapePositionAndSizeNumerics(this.designPicture.SelectedElement);
        this.designPicture.RedrawAll();
        this.designPicture.Invalidate();
      }
    }

    /// <summary>
    /// This method updates the position of the selected element in the desing picture
    /// (if there is any) according to the selected docking style.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/>.</param>
    private void GveLayoutDockStyleValueChanged(object sender, EventArgs e)
    {
      if (this.designPicture.SelectedElement != null && !this.isInitializingSelectedShape)
      {
        if (this.designPicture.SelectedElement is VGImage)
        {
          var selectedImage = this.designPicture.SelectedElement as VGImage;
          selectedImage.Layout = ImageLayout.None;
          this.cbbImageLayout.SelectedItem = ImageLayout.None.ToString();
        }

        switch ((DockStyle)this.gveLayoutDockStyle.Value)
        {
          case DockStyle.None:
            break;
          case DockStyle.Top:
            this.designPicture.SelectedElement.Location =
              new PointF(this.designPicture.SelectedElement.Location.X, 0);
            break;
          case DockStyle.Bottom:
            this.designPicture.SelectedElement.Location = new PointF(
              this.designPicture.SelectedElement.Location.X,
              this.designPicture.PresentationSize.Height - this.designPicture.SelectedElement.Height);
            break;
          case DockStyle.Left:
            this.designPicture.SelectedElement.Location =
              new PointF(0, this.designPicture.SelectedElement.Location.Y);
            break;
          case DockStyle.Right:
            this.designPicture.SelectedElement.Location = new PointF(
              this.designPicture.PresentationSize.Width - this.designPicture.SelectedElement.Width,
              this.designPicture.SelectedElement.Location.Y);
            break;
          case DockStyle.Fill:
            this.designPicture.SelectedElement.Size = this.designPicture.PresentationSize;
            this.designPicture.SelectedElement.Center = new PointF(
              this.designPicture.PresentationSize.Width / 2,
              this.designPicture.PresentationSize.Height / 2);
            break;
          default:
            throw new ArgumentOutOfRangeException();
        }

        this.UpdateShapePositionAndSizeNumerics(this.designPicture.SelectedElement);
        this.designPicture.RedrawAll();
        this.designPicture.Invalidate();
      }
    }

    #endregion //WINDOWSEVENTHANDLER

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler for Custom Defined Events                                    //
    ///////////////////////////////////////////////////////////////////////////////
    #region CUSTOMEVENTHANDLER

    /// <summary>
    /// <see cref="PenAndBrushControl.ShapePropertiesChanged"/> event handler 
    /// for the <see cref="PenAndBrushControl"/> <see cref="pbcElements"/>
    /// Changes the properties of the selected element if there is any,
    /// to the new values.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A <see cref="ShapePropertiesChangedEventArgs"/> with
    /// the new shape properties.</param>
    private void pbcElements_ShapePropertiesChanged(object sender, ShapePropertiesChangedEventArgs e)
    {
      if (this.designPicture.SelectedElement != null && !this.isInitializingSelectedShape)
      {
        this.designPicture.SelectedElement.Pen = e.Pen;
        this.designPicture.SelectedElement.Brush = e.Brush;
        this.designPicture.SelectedElement.ShapeDrawAction = e.ShapeDrawAction;
        this.designPicture.SelectedElement.Font = e.NewFont;
        this.designPicture.SelectedElement.FontColor = e.NewFontColor;
        this.designPicture.SelectedElement.Name = e.NewName;
        this.designPicture.RedrawAll();
        this.designPicture.Invalidate();
      }
    }

    /// <summary>
    /// <see cref="VectorGraphics.Canvas.PictureModifiable.ShapeAdded"/> event handler of the
    /// <see cref="VectorGraphics.Canvas.PictureModifiable"/> <see cref="designPicture"/>.
    /// Updates the <see cref="Slide"/>s TargetShapes
    /// property.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A <see cref="ShapeEventArgs"/> with the new shape.</param>
    private void picPreview_ShapeAdded(object sender, ShapeEventArgs e)
    {
      if (e.Shape.StyleGroup == VGStyleGroup.AOI_TARGET)
      {
        this.lsbTargets.Items.Add(e.Shape.Name);
        this.chbOnlyWhenInTarget.Visible = true;
        this.cbbTestingTargets.Items.Add(e.Shape.Name);
        this.cbbLinksTargets.Items.Add(e.Shape.Name);
      }

      this.UpdateShapePositionAndSizeNumerics(e.Shape);

      this.Cursor = Cursors.Default;
    }

    /// <summary>
    /// <see cref="VectorGraphics.Canvas.PictureModifiable.ShapeChanged"/> event handler of the
    /// <see cref="VectorGraphics.Canvas.PictureModifiable"/> <see cref="designPicture"/>.
    /// Updates the shapes position and soze properties.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A <see cref="ShapeEventArgs"/> with the new shape.</param>
    private void designPicture_ShapeChanged(object sender, ShapeEventArgs e)
    {
      this.UpdateShapePositionAndSizeNumerics(e.Shape);
    }

    /// <summary>
    /// <see cref="VectorGraphics.Canvas.PictureModifiable.ShapeSelected"/> event handler of the
    /// <see cref="VectorGraphics.Canvas.PictureModifiable"/> <see cref="designPicture"/>.
    /// Raises the appropriate design tab page and fills the
    /// desing fields.
    /// </summary>
    /// <remarks>For setting the <see cref="RichTextBox.ZoomFactor"/>  property
    /// see http://www.vbforums.com/showthread.php?t=410548</remarks>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A <see cref="ShapeEventArgs"/> with the new shape.</param>
    private void picPreview_ShapeSelected(object sender, ShapeEventArgs e)
    {
      if (e.Shape != null)
      {
        this.isInitializingSelectedShape = true;

        // Check for target shapes which should not be modifiable
        if (e.Shape.StyleGroup == VGStyleGroup.AOI_TARGET)
        {
          this.tctStandards.SelectedTab = this.tbpTargets;
          if (e.Shape is VGPolyline)
          {
            VGPolyline poly = (VGPolyline)e.Shape;
            poly.RecalculateBounds();
          }
        }
        else
        {
          this.tacProperties.Visible = true;
          this.pbcElements.SetFillVisibility(true);
          if (e.Shape is VGPolyline)
          {
            VGPolyline poly = (VGPolyline)e.Shape;
            poly.RecalculateBounds();
          }
          else if (e.Shape is VGLine)
          {
            VGLine line = (VGLine)e.Shape;
            line.RecalculateBounds();
            this.pbcElements.SetFillVisibility(false);
          }

          // Update preview style areas
          string backupName = e.Shape.Name;
          Font backupFont = e.Shape.Font;
          Color backupColor = e.Shape.FontColor;
          this.pbcElements.NewPen = e.Shape.Pen;
          this.pbcElements.NewBrush = e.Shape.Brush;
          this.pbcElements.DrawAction = e.Shape.ShapeDrawAction;
          e.Shape.Name = backupName;
          this.pbcElements.NewName = e.Shape.Name;
          e.Shape.Font = backupFont;
          this.pbcElements.NewFont = e.Shape.Font;
          e.Shape.FontColor = backupColor;
          this.pbcElements.NewFontColor = e.Shape.FontColor;
          this.pbcElements.Refresh();
          this.audioControl.Sound = e.Shape.Sound;

          this.nudLayoutHeight.Enabled = true;

          if (e.Shape.Location.X > Document.ActiveDocument.PresentationSize.Width
            || e.Shape.Location.Y > Document.ActiveDocument.PresentationSize.Height)
          {
            e.Shape.Location = PointF.Empty;
          }

          if (e.Shape.Width > Document.ActiveDocument.PresentationSize.Width
            || e.Shape.Height > Document.ActiveDocument.PresentationSize.Height)
          {
            e.Shape.Size = Document.ActiveDocument.PresentationSize;
          }

          this.UpdateShapePositionAndSizeNumerics(e.Shape);

          if (e.Shape is VGRichText)
          {
            VGRichText text = (VGRichText)e.Shape;
            this.rtbInstructions.RichTextBox.ZoomFactor = 1f;
            this.rtbInstructions.RichTextBox.Rtf = text.RtfToDraw;
            if (!this.tctStimuli.TabPages.Contains(this.tbpRtfInstructions))
            {
              this.tctStimuli.TabPages.Add(this.tbpRtfInstructions);
            }

            this.tctStimuli.SelectedTab = this.tbpRtfInstructions;
            this.rtbInstructions.RichTextBox.ZoomFactor = 0.5f;
            this.nudLayoutHeight.Enabled = false;
          }
          else if (e.Shape is VGText)
          {
            VGText text = (VGText)e.Shape;
            this.txbInstructions.Text = text.StringToDraw;
            this.txbInstructions.TextAlign = text.Alignment;
            if (!this.tctStimuli.TabPages.Contains(this.tbpInstructions))
            {
              this.tctStimuli.TabPages.Add(this.tbpInstructions);
            }

            this.tctStimuli.SelectedTab = this.tbpInstructions;
            this.nudLayoutHeight.Enabled = false;
          }
          else if (e.Shape is VGImage)
          {
            VGImage image = (VGImage)e.Shape;
            this.txbImageFilename.Text = image.Filename;

            this.toolTip.SetToolTip(
              this.txbImageFilename,
              Path.Combine(image.Filepath == string.Empty ? Document.ActiveDocument.ExperimentSettings.SlideResourcesPath : image.Filepath, image.Filename));

            this.cbbImageLayout.SelectedItem = image.Layout.ToString();
            if (!this.tctStimuli.TabPages.Contains(this.tbpImages))
            {
              this.tctStimuli.TabPages.Add(this.tbpImages);
            }

            this.tctStimuli.SelectedTab = this.tbpImages;
          }
          else if (e.Shape is VGFlash)
          {
            VGFlash flash = (VGFlash)e.Shape;
            this.txbFlashFilename.Text = flash.Filename;

            this.toolTip.SetToolTip(
              this.txbFlashFilename,
              Path.Combine(flash.Filepath == string.Empty ? Document.ActiveDocument.ExperimentSettings.SlideResourcesPath : flash.Filepath, flash.Filename));

            if (!this.tctStimuli.TabPages.Contains(this.tbpFlashMovies))
            {
              this.tctStimuli.TabPages.Add(this.tbpFlashMovies);
            }

            this.tctStimuli.SelectedTab = this.tbpFlashMovies;
          }
        }

        this.isInitializingSelectedShape = false;
      }
    }

    /// <summary>
    /// <see cref="VectorGraphics.Canvas.PictureModifiable.ShapeDeselected"/> event handler of the
    /// <see cref="VectorGraphics.Canvas.PictureModifiable"/> <see cref="designPicture"/>.
    /// Erases the design fields.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A <see cref="ShapeEventArgs"/> with the new shape.</param>
    private void PicPreviewShapeDeselected(object sender, EventArgs e)
    {
      this.rtbInstructions.RichTextBox.Text = string.Empty;
      this.txbInstructions.Text = string.Empty;
      this.tacProperties.Visible = false;
      this.txbImageFilename.Text = string.Empty;
      this.gveLayoutDockStyle.Text = "None";
    }

    /// <summary>
    /// <see cref="VectorGraphics.Canvas.PictureModifiable.ShapeDoubleClick"/> event handler of the
    /// <see cref="VectorGraphics.Canvas.PictureModifiable"/> <see cref="designPicture"/>.
    /// Calls the detailed stimuli dialogs for the clicked element.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A <see cref="ShapeEventArgs"/> with the selected shape.</param>
    private void PicPreviewShapeDoubleClick(object sender, ShapeEventArgs e)
    {
      if (e.Shape is VGText)
      {
        var text = (VGText)e.Shape;
        var dlg = new TextDialog { NewText = text };
        if (dlg.ShowDialog() == DialogResult.OK)
        {
          var modifiedText = dlg.NewText;
          ((VGText)this.designPicture.SelectedElement).StringToDraw = modifiedText.StringToDraw;
          ((VGText)this.designPicture.SelectedElement).Alignment = modifiedText.Alignment;
          ((VGText)this.designPicture.SelectedElement).TextFont = modifiedText.TextFont;
          ((VGText)this.designPicture.SelectedElement).TextFontColor = modifiedText.TextFontColor;
          ((VGText)this.designPicture.SelectedElement).LineSpacing = modifiedText.LineSpacing;
          this.UpdateSelectedElementAndPropertyControl(modifiedText);
        }
      }
      else if (e.Shape is VGRichText)
      {
        VGRichText text = (VGRichText)e.Shape;
        RichTextDialog dlg = new RichTextDialog();
        dlg.NewRichText = text;
        if (dlg.ShowDialog() == DialogResult.OK)
        {
          VGRichText modifiedText = (VGRichText)dlg.NewRichText;
          ((VGRichText)this.designPicture.SelectedElement).RtfToDraw = modifiedText.RtfToDraw;
          this.UpdateSelectedElementAndPropertyControl(modifiedText);
        }
      }
      else if (e.Shape is VGImage)
      {
        VGImage image = (VGImage)e.Shape;
        ImageDialog dlg = new ImageDialog();
        dlg.NewImage = image;
        if (dlg.ShowDialog() == DialogResult.OK)
        {
          VGImage modifiedImage = (VGImage)dlg.NewImage;
          ((VGImage)this.designPicture.SelectedElement).Filename = modifiedImage.Filename;
          ((VGImage)this.designPicture.SelectedElement).Filepath = modifiedImage.Filepath;
          ((VGImage)this.designPicture.SelectedElement).Layout = modifiedImage.Layout;
          ((VGImage)this.designPicture.SelectedElement).CreateInternalImage();
          this.UpdateSelectedElementAndPropertyControl(modifiedImage);
        }
      }
      else if (e.Shape.StyleGroup != VGStyleGroup.AOI_TARGET)
      {
        ShapeDialog dlg = new ShapeDialog();
        dlg.NewShape = e.Shape;
        if (dlg.ShowDialog() == DialogResult.OK)
        {
          this.UpdateSelectedElementAndPropertyControl(dlg.NewShape);
        }
      }
      else if (e.Shape.StyleGroup == VGStyleGroup.AOI_TARGET)
      {
        this.tctStandards.SelectedTab = this.tbpTargets;
      }

      this.designPicture.Invalidate();
    }

    /// <summary>
    /// <see cref="VectorGraphics.Canvas.PictureModifiable.ShapeDeleted"/> event handler of the
    /// <see cref="VectorGraphics.Canvas.PictureModifiable"/> <see cref="designPicture"/>.
    /// Removes the selected shape from the target list.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A <see cref="ShapeEventArgs"/> with the deleted shape.</param>
    private void picPreview_ShapeDeleted(object sender, ShapeEventArgs e)
    {
      if (e.Shape.StyleGroup == VGStyleGroup.AOI_TARGET)
      {
        this.lsbTargets.Items.Remove(e.Shape.Name);
        this.cbbTestingTargets.Items.Remove(e.Shape.Name);
        this.cbbLinksTargets.Items.Remove(e.Shape.Name);
        if (this.lsbTargets.Items.Count == 0)
        {
          this.chbOnlyWhenInTarget.Visible = false;

          // Remove StopConditions with target entries.
          this.RemoveTargetConditions(this.lsbStopConditions);

          // Remove CorrectResponses with target entries.
          this.RemoveTargetConditions(this.lsbCorrectResponses);

          // Remove Links with target entries.
          this.RemoveTargetConditions(this.lsbLinks);
        }
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
    // Methods for doing main class job                                          //
    ///////////////////////////////////////////////////////////////////////////////
    #region METHODS

    /// <summary>
    /// This method updates the forms numeric up and downs
    /// for the shapes position and size with the given
    /// elements new values.
    /// </summary>
    /// <param name="shape">A <see cref="VGElement"/> thats values
    /// should be available on the form.</param>
    private void UpdateShapePositionAndSizeNumerics(VGElement shape)
    {
      try
      {
        this.isInitializingSelectedShape = true;
        this.nudLayoutLeft.Value = (decimal)shape.Location.X;
        this.nudLayoutTop.Value = (decimal)shape.Location.Y;
        this.nudLayoutWidth.Value = (decimal)shape.Width;
        this.nudLayoutHeight.Value = (decimal)shape.Height;
        this.isInitializingSelectedShape = false;
      }
      catch (ArgumentOutOfRangeException ex)
      {
        ExceptionMethods.HandleException(ex);
      }
    }

    /// <summary>
    /// When properties changed the selected element in the picture
    /// and the property control <see cref="PenAndBrushControl"/>
    /// should be updated.
    /// </summary>
    /// <param name="modifiedElement">The element with the new properties,
    /// that should be copied to the selected element.</param>
    private void UpdateSelectedElementAndPropertyControl(VGElement modifiedElement)
    {
      this.designPicture.SelectedElement.ShapeDrawAction = modifiedElement.ShapeDrawAction;
      this.designPicture.SelectedElement.Pen = modifiedElement.Pen;
      this.designPicture.SelectedElement.Brush = modifiedElement.Brush;
      this.designPicture.SelectedElement.Font = modifiedElement.Font;
      this.designPicture.SelectedElement.FontColor = modifiedElement.FontColor;
      this.designPicture.SelectedElement.Name = modifiedElement.Name;
      this.pbcElements.DrawAction = modifiedElement.ShapeDrawAction;
      this.pbcElements.NewPen = modifiedElement.Pen;
      this.pbcElements.NewBrush = modifiedElement.Brush;
      this.pbcElements.NewFont = modifiedElement.Font;
      this.pbcElements.NewFontColor = modifiedElement.FontColor;
      this.pbcElements.NewName = modifiedElement.Name;
    }

    /// <summary>
    /// This method switches for the <see cref="StimuliTypes"/> set during
    /// construction whether to open a design dialog for a specific stimulus.
    /// </summary>
    private void OpenNewCreationDialog()
    {
      switch (this.stimulusTypeToCreate)
      {
        case StimuliTypes.Blank:
          break;
        case StimuliTypes.Shape:
          this.OpenNewShapeDialog();
          break;
        case StimuliTypes.Instruction:
          this.OpenNewInstructionDialog();
          break;
        case StimuliTypes.RTFInstruction:
          this.OpenNewRtfInstructionDialog();
          break;
        case StimuliTypes.Image:
          this.OpenNewImageDialog();
          break;
        case StimuliTypes.Flash:
          this.OpenNewFlashDialog();
          break;
        case StimuliTypes.None:
        case StimuliTypes.Mixed:
          // Load Flash Movies here because during PopulateDialog
          // it will give a InvalidActiveXHostStateException,
          // because the control is not visible at this moment.
          foreach (VGElement element in this.designPicture.Elements)
          {
            if (element is VGFlash)
            {
              VGFlash flash = element as VGFlash;
              flash.InitializeOnControl(this.designPicture.Parent, false, this.Picture.StimulusToScreen);
            }

            if (element is VGBrowser)
            {
              VGBrowser browser = element as VGBrowser;
              browser.InitializeOnControl(this.designPicture.Parent, false);
            }
          }

          this.designPicture.RedrawAll();
          break;
      }
    }

    /// <summary>
    /// This method opens a <see cref="FlashDialog"/> to create a new
    /// <see cref="VGFlash"/> item on the slide.
    /// </summary>
    private void OpenNewFlashDialog()
    {
      FlashDialog dlg = new FlashDialog();
      if (dlg.ShowDialog() == DialogResult.OK)
      {
        this.designPicture.NewFlashStart(dlg.NewFlash);
      }
    }

    /// <summary>
    /// This method opens a <see cref="TextDialog"/> to create a new
    /// <see cref="VGText"/> item on the slide.
    /// </summary>
    private void OpenNewInstructionDialog()
    {
      TextDialog dlg = new TextDialog();
      if (dlg.ShowDialog() == DialogResult.OK)
      {
        this.designPicture.NewTextStart(dlg.NewText);
      }
    }

    /// <summary>
    /// This method opens a <see cref="RichTextDialog"/> to create a new
    /// <see cref="VGRichText"/> item on the slide.
    /// </summary>
    private void OpenNewRtfInstructionDialog()
    {
      RichTextDialog dlg = new RichTextDialog();
      dlg.RichTextBackgroundColor = this.btnBackgroundColor.CurrentColor;
      if (dlg.ShowDialog() == DialogResult.OK)
      {
        this.designPicture.NewRtfTextStart(dlg.NewRichText);
      }
    }

    /// <summary>
    /// This method opens a <see cref="ImageDialog"/> to create a new
    /// <see cref="VGImage"/> item on the slide.
    /// </summary>
    private void OpenNewImageDialog()
    {
      ImageDialog dlg = new ImageDialog();
      if (dlg.ShowDialog() == DialogResult.OK)
      {
        this.designPicture.NewImageStart(dlg.NewImage);
      }
    }

    /// <summary>
    /// This method opens a <see cref="ShapeDialog"/> to create a new
    /// <see cref="VGElement"/> shape item on the slide.
    /// </summary>
    private void OpenNewShapeDialog()
    {
      ShapeDialog dlg = new ShapeDialog();
      if (dlg.ShowDialog() == DialogResult.OK)
      {
        this.designPicture.NewShapeStart(dlg.NewShape);
      }
    }

    /// <summary>
    /// This method opens a <see cref="SoundDialog"/> to create a new
    /// <see cref="VGSound"/> item on the slide.
    /// </summary>
    private void OpenNewSoundDialog()
    {
      SoundDialog dlg = new SoundDialog();
      if (dlg.ShowDialog() == DialogResult.OK)
      {
        this.designPicture.NewShapeStart(dlg.NewSound);
      }
    }

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

      // Tab Testing
      foreach (StopCondition condition in slide.CorrectResponses)
      {
        this.lsbCorrectResponses.Items.Add(condition);
      }

      // Tab Link
      foreach (StopCondition condition in slide.Links)
      {
        this.lsbLinks.Items.Add(condition);
      }

      // Tab Background
      this.btnBackgroundColor.CurrentColor = slide.BackgroundColor;
      this.designPicture.BackColor = slide.BackgroundColor;
      this.designPicture.BackgroundImage = slide.BackgroundImage;
      this.bkgAudioControl.Sound = slide.BackgroundSound;

      // Tab Mouse
      this.rdbShowMouseCursor.Checked = slide.MouseCursorVisible;
      this.rdbHideMouseCursor.Checked = !slide.MouseCursorVisible;
      this.psbMouseCursor.CurrentPosition = slide.MouseInitialPosition;
      this.chbMouseDontChangePosition.Checked = !slide.ForceMousePositionChange;

      // Tab trigger 
      this.triggerControl.TriggerSignal = slide.TriggerSignal;

      // VGElements
      foreach (VGElement element in slide.VGStimuli)
      {
        if (element is VGSound)
        {
          ((VGSound)element).DesignMode = true;
        }

        this.designPicture.Elements.Add(element);
      }

      // Flash objects are added but 
      // loaded during OpenNewCreationDialog(...)
      foreach (VGElement element in slide.ActiveXStimuli)
      {
        this.designPicture.Elements.Add(element);
      }

      // Tab and Picture Targets
      // Add targets last in the list, to make them top visible
      foreach (VGElement targetShape in slide.TargetShapes)
      {
        this.chbOnlyWhenInTarget.Visible = true;
        this.designPicture.Elements.Add(targetShape);
        this.lsbTargets.Items.Add(targetShape.Name);
        this.cbbTestingTargets.Items.Add(targetShape.Name);
        this.cbbLinksTargets.Items.Add(targetShape.Name);
      }

      // Finish
      this.designPicture.DrawForeground(false);
    }

    /// <summary>
    /// Creates a new <see cref="Slide"/> with the
    /// properties defined on this dialog and creates a thumb for it.
    /// </summary>
    /// <returns>The new <see cref="Slide"/> to be added to the slideshow.</returns>
    private Slide GetSlide()
    {
      Slide slide = new Slide();

      // Store category and name.
      slide.Category = this.cbbCategory.Text;
      slide.Name = this.txbName.Text;

      // Add standard stop condition if none is specified.
      if (this.lsbStopConditions.Items.Count == 0)
      {
        this.lsbStopConditions.Items.Add(new TimeStopCondition(5000));
      }

      // Store Stop conditions
      foreach (StopCondition cond in this.lsbStopConditions.Items)
      {
        slide.StopConditions.Add(cond);
      }

      // Store correct responses
      foreach (StopCondition cond in this.lsbCorrectResponses.Items)
      {
        slide.CorrectResponses.Add(cond);
      }

      // Store links
      foreach (StopCondition cond in this.lsbLinks.Items)
      {
        slide.Links.Add(cond);
      }

      // Remove grab handles if there is a selected element in the picture.
      // Otherwise the thumb would be wrong displayed
      if (this.designPicture.SelectedElement != null)
      {
        this.designPicture.SelectedElement.IsInEditMode = false;
        this.designPicture.Refresh();
      }

      // Store graphical elements.
      VGElementCollection allElements = this.designPicture.Elements;
      VGElementCollection targetElements = this.designPicture.Elements.FindAllGroupMembers(VGStyleGroup.AOI_TARGET);
      VGElementCollection activeXElements = this.designPicture.Elements.FindAllGroupMembers(VGStyleGroup.ACTIVEX);
      allElements.RemoveAll(targetElements);
      allElements.RemoveAll(activeXElements);
      slide.VGStimuli = allElements;

      // Store target shapes
      slide.TargetShapes = targetElements;

      // Store activeX elements
      slide.ActiveXStimuli = activeXElements;

      // Reset design properties
      foreach (VGElement element in allElements)
      {
        if (element is VGSound)
        {
          ((VGSound)element).DesignMode = false;
        }
      }

      // Store background properties
      slide.BackgroundColor = this.btnBackgroundColor.CurrentColor;
      slide.BackgroundImage = this.designPicture.BackgroundImage;

      if (this.bkgAudioControl.Sound != null && this.bkgAudioControl.Sound.FullFilename != null)
      {
        slide.BackgroundSound = this.bkgAudioControl.Sound;
      }

      // Store mouse properties.
      slide.MouseCursorVisible = this.rdbShowMouseCursor.Checked;
      slide.MouseInitialPosition = this.psbMouseCursor.CurrentPosition;
      slide.ForceMousePositionChange = !this.chbMouseDontChangePosition.Checked;

      // Store trigger properties
      slide.TriggerSignal = this.triggerControl.TriggerSignal;

      // Set presentation size
      slide.PresentationSize = Document.ActiveDocument.PresentationSize;

      // Set modified flag.
      slide.Modified = true;

      return slide;
    }

    #endregion //METHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER

    /// <summary>
    /// This methods resets the image layout of an selected <see cref="VGImage"/>
    /// to ImageLayout.None.
    /// </summary>
    private void ResetImageLayout()
    {
      if (this.designPicture.SelectedElement is VGImage)
      {
        var selectedImage = this.designPicture.SelectedElement as VGImage;
        selectedImage.Layout = ImageLayout.None;
        this.cbbImageLayout.SelectedItem = ImageLayout.None.ToString();
        this.gveLayoutDockStyle.Text = "None";
      }
    }

    /// <summary>
    /// Removes all <see cref="StopCondition"/>s with a target condition
    /// of the given <see cref="ListBox"/>
    /// </summary>
    /// <param name="box">A <see cref="ListBox"/> which should be checked for
    /// target conditions.</param>
    private void RemoveTargetConditions(ListBox box)
    {
      // Get conditions to remove
      StopConditionCollection itemsToRemove = new StopConditionCollection();
      foreach (StopCondition stc in box.Items)
      {
        if (stc is MouseStopCondition)
        {
          if (((MouseStopCondition)stc).Target != string.Empty)
          {
            itemsToRemove.Add(stc);
          }
        }
      }

      // Remove them from the ListBox
      foreach (StopCondition removeStc in itemsToRemove)
      {
        box.Items.Remove(removeStc);
      }
    }

    #endregion //HELPER
  }
}