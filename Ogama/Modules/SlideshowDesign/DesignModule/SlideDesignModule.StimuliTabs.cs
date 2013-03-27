// <copyright file="SlideDesignModule.StimuliTabs.cs" company="FU Berlin">
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

namespace Ogama.Modules.SlideshowDesign.DesignModule
{
  using System;
  using System.IO;
  using System.Windows.Forms;

  using Ogama.ExceptionHandling;

  using VectorGraphics.Elements;

  /// <summary>
  /// The SlideDesignModule.StimuliTabs.cs contains methods referring
  /// to the <see cref="TabControl"/> at the top left of the 
  /// module to define the stimuli properties that are currently selected
  /// or define new ones.
  /// </summary>
  public partial class SlideDesignModule
  {
    #region NewStimuli

    /// <summary>
    /// <see cref="Control.Click"/> event handler 
    /// for the <see cref="Button"/> <see cref="btnAddInstruction"/>
    /// Raises the referring text creation mode of the <see cref="designPicture"/>
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A empty <see cref="EventArgs"/></param>
    private void btnAddInstruction_Click(object sender, EventArgs e)
    {
      this.OpenNewInstructionDialog();
    }

    /// <summary>
    /// <see cref="Control.Click"/> event handler 
    /// for the <see cref="Button"/> <see cref="btnAddRtfInstruction"/>
    /// Raises the referring rtf text creation mode of the <see cref="designPicture"/>
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A empty <see cref="EventArgs"/></param>
    private void btnAddRtfInstruction_Click(object sender, EventArgs e)
    {
      this.OpenNewRtfInstructionDialog();
    }

    /// <summary>
    /// <see cref="Control.Click"/> event handler 
    /// for the <see cref="Button"/> <see cref="btnAddImage"/>
    /// Raises a new <see cref="VGImage"/> creation of the <see cref="designPicture"/>.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A empty <see cref="EventArgs"/></param>
    private void btnAddImage_Click(object sender, EventArgs e)
    {
      this.OpenNewImageDialog();
    }

    /// <summary>
    /// <see cref="Control.Click"/> event handler 
    /// for the <see cref="Button"/> <see cref="btnAddImage"/>
    /// Raises a new <see cref="VGElement"/> shape creation of the <see cref="designPicture"/>.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A empty <see cref="EventArgs"/></param>
    private void btnAddShape_Click(object sender, EventArgs e)
    {
      this.OpenNewShapeDialog();
    }

    /// <summary>
    /// <see cref="Control.Click"/> event handler 
    /// for the <see cref="Button"/> <see cref="btnAddSound"/>
    /// Raises a new <see cref="VGSound"/> creation of the <see cref="designPicture"/>.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A empty <see cref="EventArgs"/></param>
    private void btnAddSound_Click(object sender, EventArgs e)
    {
      this.OpenNewSoundDialog();
    }

    /// <summary>
    /// <see cref="Control.Click"/> event handler 
    /// for the <see cref="Button"/> <see cref="btnAddFlash"/>
    /// Raises a new <see cref="VGFlash"/> creation of the <see cref="designPicture"/>.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A empty <see cref="EventArgs"/></param>
    private void btnAddFlash_Click(object sender, EventArgs e)
    {
      this.OpenNewFlashDialog();
    }

    #endregion //NewStimuli

    #region Shapes
    #endregion //Shapes

    #region Instructions

    /// <summary>
    /// <see cref="Control.TextChanged"/> event handler 
    /// for the <see cref="TextBox"/> <see cref="txbInstructions"/>
    /// Updates the selected <see cref="VGText"/> element with the modified
    /// instruction.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A empty <see cref="EventArgs"/></param>
    private void txbInstructions_TextChanged(object sender, EventArgs e)
    {
      if (this.designPicture.SelectedElement != null && !this.isInitializingSelectedShape)
      {
        if (this.designPicture.SelectedElement is VGText)
        {
          VGText text = (VGText)this.designPicture.SelectedElement;
          text.StringToDraw = this.txbInstructions.Text;
          this.designPicture.DrawForeground(true);
        }
      }
    }

    /// <summary>
    /// <see cref="OgamaControls.RTBTextControl.RtfChanged"/> event handler 
    /// for the <see cref="OgamaControls.RTBTextControl"/> <see cref="rtbInstructions"/>
    /// Updates the selected <see cref="VGRichText"/> element with the modified
    /// instruction.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A empty <see cref="EventArgs"/></param>
    private void rtbInstructions_RtfChanged(object sender, EventArgs e)
    {
      if (this.designPicture.SelectedElement != null && !this.isInitializingSelectedShape)
      {
        if (this.designPicture.SelectedElement is VGRichText)
        {
          VGRichText text = (VGRichText)this.designPicture.SelectedElement;
          text.RtfToDraw = this.rtbInstructions.RichTextBox.Rtf;
          this.designPicture.DrawForeground(true);
        }
      }
    }

    #endregion //Instructions

    #region Images

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
      dlg.Filter = "Images (*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF|All files (*.*)|*.*";
      dlg.Title = "Select stimulus image ...";
      dlg.InitialDirectory = Document.ActiveDocument.ExperimentSettings.SlideResourcesPath;

      if (dlg.ShowDialog() == DialogResult.OK)
      {
        string fileName = dlg.FileName;
        if (!File.Exists(fileName))
        {
          // Erase textbox entry
          this.txbImageFilename.Text = string.Empty;
          return;
        }

        string templatePath = Document.ActiveDocument.ExperimentSettings.SlideResourcesPath;
        if (Path.GetDirectoryName(fileName) != templatePath)
        {
          string target = Path.Combine(templatePath, Path.GetFileName(fileName));
          if (!File.Exists(target))
          {
            File.Copy(fileName, target);

            string message = "A copy of this image file is saved to the following slide resources folder of the current project :" +
              Environment.NewLine + target + Environment.NewLine +
              "This is done because of easy movement of experiments between different computers or locations.";

            ExceptionMethods.ProcessMessage("File is copied", message);
          }

          fileName = target;
        }

        this.txbImageFilename.Text = Path.GetFileName(fileName);
        this.toolTip.SetToolTip(this.txbImageFilename, fileName);
      }
    }

    /// <summary>
    /// <see cref="Control.TextChanged"/> event handler for the
    /// <see cref="TextBox"/> <see cref="txbImageFilename"/>.
    /// Updates the image file of a selected <see cref="VGImage"/> element with the new value.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A empty <see cref="EventArgs"/></param>
    private void txbImageFilename_TextChanged(object sender, EventArgs e)
    {
      if (this.designPicture.SelectedElement != null && !this.isInitializingSelectedShape)
      {
        if (this.designPicture.SelectedElement is VGImage)
        {
          VGImage image = (VGImage)this.designPicture.SelectedElement;
          image.Filename = Path.GetFileName(this.txbImageFilename.Text);
          image.CreateInternalImage();
          this.designPicture.DrawForeground(true);
        }
      }
    }

    /// <summary>
    /// <see cref="ComboBox.SelectedIndexChanged"/> event handler 
    /// for the <see cref="cbbImageLayout"/> <see cref="ComboBox"/>.
    /// Updates a selected <see cref="VGImage"/> with the new <see cref="ImageLayout"/>.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void cbbImageLayout_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (this.designPicture.SelectedElement != null && !this.isInitializingSelectedShape)
      {
        if (this.designPicture.SelectedElement is VGImage)
        {
          VGImage image = (VGImage)this.designPicture.SelectedElement;
          image.Layout = (ImageLayout)Enum.Parse(typeof(ImageLayout), this.cbbImageLayout.Text);
          this.designPicture.DrawForeground(true);
        }
      }
    }

    #endregion //Images

    #region FlashControls

    /// <summary>
    /// <see cref="Control.Click"/> event handler 
    /// for the <see cref="btnSelectFlashMovie"/> <see cref="Button"/>
    /// Opens file open dialog.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/>.</param>
    private void btnSelectFlashMovie_Click(object sender, EventArgs e)
    {
      if (this.ofdFlashMovie.ShowDialog() == DialogResult.OK)
      {
        if (Path.GetFileName(this.ofdFlashMovie.FileName).Contains("#"))
        {
          ExceptionMethods.ProcessMessage("Invalid filename", "Please do not use hashes '#' in the filename.");
        }
        else
        {
          string fileName = this.ofdFlashMovie.FileName;
          this.txbFlashFilename.Text = fileName;
          string templatePath = Document.ActiveDocument.ExperimentSettings.SlideResourcesPath;

          if (Path.GetDirectoryName(fileName) != templatePath)
          {
            string target = Path.Combine(templatePath, Path.GetFileName(fileName));
            if (!File.Exists(target))
            {
              File.Copy(fileName, target);
              string message = "A copy of this movie file is saved to the following slide resources folder of the current project :" +
                Environment.NewLine + target + Environment.NewLine +
                "This is done because of easy movement of experiments between different computers or locations.";

              ExceptionMethods.ProcessMessage("File is copied", message);
            }

            this.txbFlashFilename.Text = target;
          }

          this.designPicture.DrawForeground(true);
        }
      }
    }

    #endregion //FlashControls
  }
}