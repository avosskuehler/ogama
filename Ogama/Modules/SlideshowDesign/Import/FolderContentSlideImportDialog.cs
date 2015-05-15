// <copyright file="FolderContentSlideImportDialog.cs" company="FU Berlin">
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

namespace Ogama.Modules.SlideshowDesign.Import
{
  using System;
  using System.Collections.Generic;
  using System.Drawing;
  using System.IO;
  using System.Windows.Forms;

  using Ogama.ExceptionHandling;
  using Ogama.Modules.Common.SlideCollections;
  using Ogama.Modules.Common.Tools;
  using Ogama.Modules.SlideshowDesign.DesignModule.StimuliDialogs;

  using OgamaControls;

  using VectorGraphics.Elements;
  using VectorGraphics.Elements.ElementCollections;
  using VectorGraphics.StopConditions;
  using VectorGraphics.Tools;

  /// <summary>
  /// This dialog <see cref="Form"/> is used to batch import the contents
  /// of a folder into ogama readable <see cref="Trial"/>s.
  /// </summary>
  public partial class FolderContentSlideImportDialog : Form
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
    /// This member is used to determine the length of parsed audio files.
    /// </summary>
    private AudioPlayer player;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the FolderContentSlideImportDialog class.
    /// </summary>
    public FolderContentSlideImportDialog()
    {
      this.InitializeComponent();
      this.player = new AudioPlayer();
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Enumerations                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region ENUMS
    #endregion ENUMS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets the <see cref="List{Slide}"/> that are imported within this dialog
    /// to be added to the slideshow.
    /// </summary>
    public List<Slide> Slides
    {
      get { return this.GetSlides(); }
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
    private void FolderImportDlg_Load(object sender, EventArgs e)
    {
      this.cbbKeys.Items.Clear();
      this.cbbKeys.Items.Add("Any");
      this.cbbKeys.Items.AddRange(Enum.GetNames(typeof(Keys)));
      this.cbbKeys.Text = Keys.Space.ToString();

      this.cbbMouseButtons.Items.Clear();
      this.cbbMouseButtons.Items.Add("Any");
      this.cbbMouseButtons.Items.AddRange(Enum.GetNames(typeof(MouseButtons)));
      this.cbbMouseButtons.Text = MouseButtons.Left.ToString();

      this.psbItems.StimulusScreenSize = Document.ActiveDocument.PresentationSize;
      this.psbMouseCursor.StimulusScreenSize = Document.ActiveDocument.PresentationSize;

      this.psbMouseCursor.CurrentPosition = new Point(
        Document.ActiveDocument.PresentationSize.Width / 2,
        Document.ActiveDocument.PresentationSize.Height / 2);
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnOK"/>.
    /// Does some error checking an sets <see cref="DialogResult"/>.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnOK_Click(object sender, EventArgs e)
    {
      if (this.txbFolder.Text == string.Empty)
      {
        ExceptionMethods.ProcessMessage("Missing folder", "No folder for import selected, so cancelling");
        this.DialogResult = DialogResult.Cancel;
        return;
      }
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnInstruction"/>.
    /// Shows a <see cref="TextDialog"/> to define a textual
    /// instruction that is added to all imported slides.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnInstruction_Click(object sender, EventArgs e)
    {
      TextDialog dlg = new TextDialog();
      if (dlg.ShowDialog() == DialogResult.OK)
      {
        VGText text = dlg.NewText;
        this.cbbDesignedItem.Items.Add(text);
        this.cbbDesignedItem.SelectedItem = text;
      }
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnImage"/>.
    /// Shows a <see cref="ImageDialog"/> to define an umage stimulus
    /// that is added to all imported slides.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnImage_Click(object sender, EventArgs e)
    {
      ImageDialog dlg = new ImageDialog();
      if (dlg.ShowDialog() == DialogResult.OK)
      {
        VGImage image = dlg.NewImage;
        image.Canvas = Document.ActiveDocument.PresentationSize;
        this.cbbDesignedItem.Items.Add(image);
        this.cbbDesignedItem.SelectedItem = image;
      }
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnShapes"/>.
    /// Shows a <see cref="ShapeDialog"/> to define a shape
    /// element that is added to all imported slides.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnShapes_Click(object sender, EventArgs e)
    {
      ShapeDialog dlg = new ShapeDialog();
      if (dlg.ShowDialog() == DialogResult.OK)
      {
        VGElement shape = dlg.NewShape;
        this.cbbDesignedItem.Items.Add(shape);
        this.cbbDesignedItem.SelectedItem = shape;
      }
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnOpenFolder"/>.
    /// Opens a <see cref="FolderBrowserDialog"/> to select the folder to 
    /// parse for stimuli.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnOpenFolder_Click(object sender, EventArgs e)
    {
      if (this.fbdStimuli.ShowDialog() == DialogResult.OK)
      {
        this.txbFolder.Text = this.fbdStimuli.SelectedPath;
      }
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnAddItem"/>.
    /// Adds the selected item in the <see cref="ComboBox"/> of designed items
    /// to the list of items to be added to the slide.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnAddItem_Click(object sender, EventArgs e)
    {
      if (this.cbbDesignedItem.SelectedItem != null &&
        this.cbbDesignedItem.SelectedItem is VGElement)
      {
        VGElement itemToAdd = (VGElement)this.cbbDesignedItem.SelectedItem;
        itemToAdd.Center = this.psbItems.CurrentPosition;
        itemToAdd.Size = new SizeF((float)this.nudWidth.Value, (float)this.nudHeight.Value);
        this.lsbStandardItems.Items.Add(itemToAdd);
        this.cbbDesignedItem.Items.Remove(itemToAdd);
      }
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnRemoveItem"/>.
    /// Removes the selected item(s) from the list of 
    /// slide items that should be added to each imported slide.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnRemoveItem_Click(object sender, EventArgs e)
    {
      List<object> selectedObjects = new List<object>();
      foreach (object item in this.lsbStandardItems.SelectedItems)
      {
        selectedObjects.Add(item);
      }

      foreach (object item in selectedObjects)
      {
        this.lsbStandardItems.Items.Remove(item);
      }
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
    // Methods for doing main class job                                          //
    ///////////////////////////////////////////////////////////////////////////////
    #region PRIVATEMETHODS

    /// <summary>
    /// This method does the parsing of the given path and creates for each readable
    /// image or media file a trial with the defined conditions.
    /// </summary>
    /// <returns>A <see cref="List{Slide}"/> to be imported in the slideshow of
    /// the experiment.</returns>
    private List<Slide> GetSlides()
    {
      var newSlides = new List<Slide>();

      var dirInfoStimuli = new DirectoryInfo(this.txbFolder.Text);
      if (dirInfoStimuli.Exists)
      {
        var files = dirInfoStimuli.GetFiles();
        Array.Sort(files, new NumericComparer());
        foreach (var file in files)
        {
          var extension = file.Extension.ToLower();
          // Ignore files with unrecognized extensions
          switch (extension)
          {
            case ".bmp":
            case ".png":
            case ".jpg":
            case ".wmf":
            case ".mp3":
            case ".wav":
            case ".wma":
              break;
            default:
              continue;
          }

          // Ignore hidden and MAC files
          if (file.Name.StartsWith("."))
          {
            continue;
          }

          var newSlide = new Slide
            {
              BackgroundColor = this.clbBackground.CurrentColor,
              Modified = true,
              MouseCursorVisible = this.chbShowMouseCursor.Checked,
              MouseInitialPosition = this.psbMouseCursor.CurrentPosition,
              Name = Path.GetFileNameWithoutExtension(file.Name),
              PresentationSize = Document.ActiveDocument.PresentationSize
            };

          StopCondition stop = null;
          if (this.rdbTime.Checked)
          {
            stop = new TimeStopCondition((int)(this.nudTime.Value * 1000));
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

            stop = ksc;
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

            msc.Target = string.Empty;
            stop = msc;
          }
          else if (this.rdbDuration.Checked)
          {
            if (extension == ".mp3" || extension == ".wav" || extension == ".wma")
            {
              int duration = this.GetAudioFileLength(file.FullName);
              if (duration != 0)
              {
                stop = new TimeStopCondition(duration + (int)this.nudLatency.Value);
              }
              else
              {
                stop = new TimeStopCondition((int)(this.nudTime.Value * 1000));
              }
            }
            else
            {
              stop = new TimeStopCondition((int)(this.nudTime.Value * 1000));
            }
          }

          newSlide.StopConditions.Add(stop);

          foreach (VGElement element in this.lsbStandardItems.Items)
          {
            newSlide.VGStimuli.Add(element);
          }

          string destination = Path.Combine(Document.ActiveDocument.ExperimentSettings.SlideResourcesPath, file.Name);
          switch (extension)
          {
            case ".bmp":
            case ".png":
            case ".jpg":
            case ".wmf":
              if (!File.Exists(destination))
              {
                File.Copy(file.FullName, destination, true);
              }

              VGImage image = new VGImage(
                ShapeDrawAction.None,
                Pens.Red,
                Brushes.Red,
                SystemFonts.MenuFont,
                Color.Red,
                file.Name,
                Document.ActiveDocument.ExperimentSettings.SlideResourcesPath,
                ImageLayout.Stretch,
                1f,
                Document.ActiveDocument.PresentationSize,
                VGStyleGroup.None,
                file.Name,
                string.Empty,
                true);

              newSlide.VGStimuli.Add(image);
              newSlides.Add(newSlide);
              break;
            case ".mp3":
            case ".wav":
            case ".wma":
              File.Copy(file.FullName, destination, true);
              VGSound sound = new VGSound(ShapeDrawAction.None, Pens.Red, new Rectangle(0, 0, 200, 300));
              sound.Center = newSlide.MouseInitialPosition;
              sound.Size = new SizeF(50, 50);
              AudioFile audioFile = new AudioFile();
              audioFile.Filename = file.Name;
              audioFile.Filepath = Document.ActiveDocument.ExperimentSettings.SlideResourcesPath;
              audioFile.Loop = false;
              audioFile.ShouldPlay = true;
              audioFile.ShowOnClick = false;
              sound.Sound = audioFile;
              newSlide.VGStimuli.Add(sound);
              newSlides.Add(newSlide);
              break;
          }
        }
      }

      return newSlides;
    }

    /// <summary>
    /// This method returns the length of the given audio file in milliseconds.
    /// </summary>
    /// <param name="audioFile">The full file path to the audio file.</param>
    /// <returns>The length of the given audio file in milliseconds.</returns>
    private int GetAudioFileLength(string audioFile)
    {
      this.player.LoadAudioFile(audioFile);
      int duration = (int)this.player.Duration;
      this.player.CloseAudioFile();
      return duration;
    }

    #endregion //PRIVATEMETHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER
    #endregion //HELPER
  }
}