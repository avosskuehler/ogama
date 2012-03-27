// <copyright file="BrowserDialog.cs" company="FU Berlin">
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
  using System.IO;
  using System.Text;
  using System.Text.RegularExpressions;
  using System.Windows.Forms;

  using Ogama.Modules.Common.Controls;
  using Ogama.Modules.Common.SlideCollections;

  using VectorGraphics.Controls;
  using VectorGraphics.Elements;
  using VectorGraphics.Elements.ElementCollections;
  using VectorGraphics.StopConditions;

  /// <summary>
  /// This dialog <see cref="Form"/> is used to initially define a 
  /// browser window that can be added to a slide.
  /// </summary>
  public partial class BrowserDialog : Form
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
    /// Saves the full URL that identifies
    /// the target for browsing.
    /// </summary>
    private string browserURL;

    /// <summary>
    /// Saves the designed browser tree node that can be 
    /// adapted in this form.
    /// </summary>
    private BrowserTreeNode browserTreeNode;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the BrowserDialog class.
    /// </summary>
    public BrowserDialog()
    {
      this.InitializeComponent();

      // Timing Tab
      SlideDesignModule.FillKeyCombo(this.cbbKeys);
      this.rdbTime.Checked = true;
      this.nudTime.Value = SlideDesignModule.SLIDEDURATIONINS;
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining events, enums, delegates                                         //
    ///////////////////////////////////////////////////////////////////////////////
    #region EVENTS
    #endregion EVENTS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets or sets the BrowserSlide for this design form.
    /// </summary>
    /// <value>The current used <see cref="BrowserTreeNode"/>.</value>
    /// <exception cref="ArgumentNullException">Thrown when value is null.</exception>
    public BrowserTreeNode BrowserNode
    {
      get
      {
        return this.GetBrowserSlide();
      }

      set
      {
        if (value == null)
        {
          throw new ArgumentNullException("Setting 'null' for a browser Slide is not allowed in BrowserDialog");
        }

        // Update the forms fields with slides values
        this.PopulateDialogWithBrowserTreeNode(value);
      }
    }

    /// <summary>
    /// Gets or sets the <see cref="Uri"/> for the browser target 
    /// specified in this dialog.
    /// </summary>
    /// <value>A <see cref="Uri"/> with browser url.</value>
    public string BrowserURL
    {
      get
      {
        return this.browserURL;
      }

      set
      {
        this.browserURL = value;
        this.webBrowserPreview.Url = new Uri(this.browserURL);
      }
    }

    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Public methods                                                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region PUBLICMETHODS

    /// <summary>
    /// Create a unique filename from the given <see cref="Uri"/>
    /// with path to the slide resources folder of the experiment
    /// </summary>
    /// <param name="url">The <see cref="Uri"/> to be converted to a filename</param>
    /// <returns>A <see cref="String"/> with a valid and unique filename.</returns>
    public static string GetFilenameFromUrl(Uri url)
    {
      string filename = Regex.Replace(url.ToString(), @"(\\|\/|\:|\*|\?|\""|\<|\>|\|)?", string.Empty);
      filename = filename.Substring(0, Math.Min(filename.Length, 100));
      filename += ".png";
      filename = Path.Combine(Document.ActiveDocument.ExperimentSettings.SlideResourcesPath, filename);
      return filename;
    }

    #endregion //PUBLICMETHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Inherited methods                                                         //
    ///////////////////////////////////////////////////////////////////////////////
    #region OVERRIDES
    #endregion //OVERRIDES

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler                                                              //
    ///////////////////////////////////////////////////////////////////////////////
    #region EVENTHANDLER

    /// <summary>
    /// The TextBox.TextChanged event handler 
    /// which updates the browser window
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A empty <see cref="EventArgs"/></param>
    private void txbURL_TextChanged(object sender, EventArgs e)
    {
      try
      {
        this.webBrowserPreview.Url = new Uri(this.txbURL.Text);
      }
      catch (Exception)
      {
        // Ignore, just wait for valid URL
      }
    }

    /// <summary>
    /// <see cref="Control.Click"/> event handler 
    /// for the <see cref="Button"/> <see cref="btnHelp"/>
    /// Raises a new <see cref="HelpDialog"/> dialog with instructions.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A empty <see cref="EventArgs"/></param>
    private void btnHelp_Click(object sender, EventArgs e)
    {
      HelpDialog dlg = new HelpDialog();
      dlg.HelpCaption = "How to: Interact with the browser.";
      StringBuilder sb = new StringBuilder();
      sb.Append("In the preview and on the slide you can interact with the browser window just as usual.");
      sb.AppendLine();
      sb.Append("The preview will be updated once a valid URL is specified.");
      dlg.HelpMessage = sb.ToString();
      dlg.ShowDialog();
    }

    /// <summary>
    /// <see cref="Control.Click"/> event handler 
    /// for the <see cref="Button"/> <see cref="btnOK"/>
    /// Sets the <see cref="Form.DialogResult"/> property to <see cref="DialogResult.OK"/>.
    /// Set it manually, because otherwise hitting "enter" in TextBox will
    /// finish dialog.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A empty <see cref="EventArgs"/></param>
    private void btnOK_Click(object sender, EventArgs e)
    {
      this.DialogResult = DialogResult.OK;
    }

    /// <summary>
    /// <see cref="Control.Click"/> event handler 
    /// for the <see cref="btnAddCondition"/> <see cref="Button"/>
    /// Updates the <see cref="lsbStopConditions"/> items with the new 
    /// stop condition, resp. response.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnAddCondition_Click(object sender, EventArgs e)
    {
      if (this.rdbTime.Checked)
      {
        TimeStopCondition tsc = new TimeStopCondition((int)(this.nudTime.Value * 1000));

        // Remove existing TimeConditions, because only one can be valid...
        TimeStopCondition oldTimeCondition = null;
        foreach (object condition in this.lsbStopConditions.Items)
        {
          if (condition is TimeStopCondition)
          {
            oldTimeCondition = (TimeStopCondition)condition;
          }
        }

        if (oldTimeCondition != null)
        {
          this.lsbStopConditions.Items.Remove(oldTimeCondition);
        }

        this.lsbStopConditions.Items.Add(tsc);
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

        if (!this.lsbStopConditions.Items.Contains(ksc))
        {
          this.lsbStopConditions.Items.Add(ksc);
        }
      }
    }

    /// <summary>
    /// <see cref="Control.Click"/> event handler 
    /// for the <see cref="btnRemoveCondition"/> <see cref="Button"/>
    /// Removes the selected <see cref="StopCondition"/> items from
    /// the <see cref="lsbStopConditions"/> list.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnRemoveCondition_Click(object sender, EventArgs e)
    {
      SlideDesignModule.DeleteSelectedItems(this.lsbStopConditions);
    }

    /// <summary>
    /// <see cref="Control.Click"/> event handler 
    /// for the <see cref="PictureBox"/> <see cref="pcbHelpTiming"/>
    /// Shows a <see cref="HelpDialog"/> with help information on
    /// how to define timing conditions.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void pcbHelpTiming_Click(object sender, EventArgs e)
    {
      HelpDialog dlg = new HelpDialog();
      dlg.HelpCaption = "How to: Stop web browser from beeing displayed";
      StringBuilder sb = new StringBuilder();
      sb.AppendLine("Specify the response that the webbrowser slide waits for.");
      sb.Append("This can be a mouse button or a key or an elapsed time, or ");
      sb.AppendLine("a combination.");
      sb.AppendLine("Use the button 'Add condition' to add the condition to the list.");
      dlg.HelpMessage = sb.ToString();
      dlg.ShowDialog();
    }

    /// <summary>
    /// <see cref="Control.Click"/> event handler 
    /// for the <see cref="PictureBox"/> <see cref="pcbHelpNaming"/>
    /// Shows a <see cref="HelpDialog"/> with help information.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void pcbHelpNaming_Click(object sender, EventArgs e)
    {
      HelpDialog dlg = new HelpDialog();
      dlg.HelpCaption = "How to: Name web browser slides.";
      StringBuilder sb = new StringBuilder();
      sb.Append("Specify a name for the starting browser page.");
      sb.Append("The category is an optional grouping possibility at slide level, ");
      sb.Append("that can be used in the statistics output.");

      dlg.HelpMessage = sb.ToString();
      dlg.ShowDialog();
    }

    /// <summary>
    /// <see cref="Control.Click"/> event handler 
    /// for the <see cref="PictureBox"/> <see cref="pcbHelpBrowseDepth"/>
    /// Shows a <see cref="HelpDialog"/> with help information on
    /// how to define slide groups.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void pcbHelpBrowseDepth_Click(object sender, EventArgs e)
    {
      HelpDialog dlg = new HelpDialog();
      dlg.HelpCaption = "How to: Allow or disable browsing.";
      StringBuilder sb = new StringBuilder();
      sb.AppendLine("Set the depth to zero disables browsing for the subject at all.");
      sb.AppendLine("The depth otherwise indicates the maximum number of navigations (links clicked) the subject is allowed to make.");
      sb.AppendLine("Please note, that this also includes backward and forward links available on lots of web pages.");

      dlg.HelpMessage = sb.ToString();
      dlg.ShowDialog();
    }

    #endregion //EVENTHANDLER

    ///////////////////////////////////////////////////////////////////////////////
    // Methods and Eventhandling for Background tasks                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region THREAD
    #endregion //THREAD

    ///////////////////////////////////////////////////////////////////////////////
    // Methods for doing main class job                                          //
    ///////////////////////////////////////////////////////////////////////////////
    #region PRIVATEMETHODS

    /// <summary>
    /// Update the dialog forms fields with the content from the given <see cref="BrowserTreeNode"/>.
    /// </summary>
    /// <param name="browserTreeNode">The <see cref="BrowserTreeNode"/> whichs content should
    /// be shown in the dialog.</param>
    private void PopulateDialogWithBrowserTreeNode(BrowserTreeNode browserTreeNode)
    {
      this.browserTreeNode = browserTreeNode;

      // Common properties
      this.txbName.Text = browserTreeNode.Text;
      this.cbbCategory.Text = browserTreeNode.Category;
      this.txbURL.Text = browserTreeNode.OriginURL;
      this.nudBrowseDepth.Value = browserTreeNode.BrowseDepth;

      // Tab Timing
      foreach (StopCondition condition in browserTreeNode.Slide.StopConditions)
      {
        this.lsbStopConditions.Items.Add(condition);
      }
    }

    /// <summary>
    /// Creates a new <see cref="Slide"/> with the
    /// properties defined on this dialog and creates a thumb for it.
    /// </summary>
    /// <returns>The new <see cref="Slide"/> to be added to the slideshow.</returns>
    private BrowserTreeNode GetBrowserSlide()
    {
      if (this.browserTreeNode == null)
      {
        this.browserTreeNode = new BrowserTreeNode();
      }

      // Store category and name.
      this.browserTreeNode.Category = this.cbbCategory.Text;
      this.browserTreeNode.Text = this.txbName.Text;
      this.browserTreeNode.OriginURL = this.txbURL.Text;
      this.browserTreeNode.BrowseDepth = (int)this.nudBrowseDepth.Value;

      // Add standard stop condition if none is specified.
      if (this.lsbStopConditions.Items.Count == 0)
      {
        this.lsbStopConditions.Items.Add(new TimeStopCondition(SlideDesignModule.SLIDEDURATIONINS * 1000));
      }

      Slide baseURLSlide = new Slide();
      baseURLSlide.Category = this.cbbCategory.Text;
      baseURLSlide.Modified = true;
      baseURLSlide.Name = this.txbName.Text;
      baseURLSlide.PresentationSize = Document.ActiveDocument.PresentationSize;
      baseURLSlide.MouseCursorVisible = true;

      // Store Stop conditions
      foreach (StopCondition cond in this.lsbStopConditions.Items)
      {
        baseURLSlide.StopConditions.Add(cond);
      }

      Bitmap screenshot = WebsiteThumbnailGenerator.GetWebSiteScreenshot(
        this.txbURL.Text,
        Document.ActiveDocument.PresentationSize);
      string screenshotFilename = GetFilenameFromUrl(new Uri(this.txbURL.Text));
      screenshot.Save(screenshotFilename, System.Drawing.Imaging.ImageFormat.Png);

      VGScrollImage baseURLScreenshot = new VGScrollImage(
        ShapeDrawAction.None,
        Pens.Transparent,
        Brushes.Black,
        SystemFonts.DefaultFont,
        Color.Black,
        Path.GetFileName(screenshotFilename),
        Document.ActiveDocument.ExperimentSettings.SlideResourcesPath,
        ImageLayout.None,
        1f,
        Document.ActiveDocument.PresentationSize,
        VGStyleGroup.None,
        baseURLSlide.Name,
        string.Empty);

      baseURLSlide.VGStimuli.Add(baseURLScreenshot);
      this.browserTreeNode.Slide = baseURLSlide;

      // HtmlElementCollection es = webBrowser1.Document.GetElementsByTagName("a");
      // if (es != null && es.Count != 0)
      // {
      //  HtmlElement ele = es[0];
      //  //This line is optional, it only visually scolls the first link element into view
      //  ele.ScrollIntoView(true);
      //  ele.Focus();
      // SendKeys.Send("{ENTER}");
      // }
      return this.browserTreeNode;
    }

    #endregion //PRIVATEMETHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER
    #endregion //HELPER
  }
}