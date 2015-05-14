// <copyright file="SaliencyModule.cs" company="FU Berlin">
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

namespace Ogama.Modules.Saliency
{
  using System;
  using System.Data;
  using System.Diagnostics;
  using System.Drawing;
  using System.Drawing.Imaging;
  using System.Globalization;
  using System.IO;
  using System.Text;
  using System.Threading;
  using System.Windows.Forms;

  using Ogama.DataSet;
  using Ogama.ExceptionHandling;
  using Ogama.MainWindow;
  using Ogama.Modules.Common.FormTemplates;
  using Ogama.Modules.Common.PictureTemplates;
  using Ogama.Modules.Common.Types;
  using Ogama.Properties;

  using OgamaControls.Dialogs;

  using VectorGraphics.Elements;
  using VectorGraphics.Tools;
  using VectorGraphics.Tools.CustomEventArgs;

  /// <summary>
  /// Derived from <see cref="FormWithTrialSelection"/>.
  /// This <see cref="Form"/> is the class for the saliency interface. 
  /// This class handles the UI and the database connection for
  /// the <see cref="SaliencyPicture"/> class, which is the main element
  /// of this form.
  /// </summary>
  /// <remarks>This interface is intended to calculate and visualize the
  /// salient locations on the stimulus images. 
  /// It uses the _ezvision programm compiled for windows
  /// from the ilab toolkit.
  /// </remarks>
  public partial class SaliencyModule : FormWithTrialSelection
  {
    ////Just if sometimes I will manage to import the _ezvision dll.
    ////[DllImport("cyg-ilab-_ezvision.dll")]
    ////private static extern int ilabezvision(int argc,
    ////  [In][MarshalAsAttribute(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPWStr)] 
    ////  string[] argv);

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
    /// This delegate is invoked, when a new _ezvision
    /// output string was received.
    /// </summary>
    private WriteConsoleLine writeConsoleLineDelegate;

    /// <summary>
    /// This delegate is invoked, when the _ezvision
    /// calculation has finished.
    /// </summary>
    private CalculationFinished calculationFinishedDelegate;

    /// <summary>
    /// The calculation thread.
    /// </summary>
    private Thread saliencyThread;

    /// <summary>
    /// This process encapsulates the ezvision calculation.
    /// </summary>
    private Process ezvision;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the SaliencyModule class.
    /// </summary>
    public SaliencyModule()
    {
      // Init
      this.InitializeComponent();

      if (!Directory.Exists(Properties.Settings.Default.SaliencyCalculationPath))
      {
        this.CopyEZVisionToLocalApplicationData();
      }

      this.Picture = this.saliencyPicture;
      this.TrialCombo = this.cbbTrial;
      this.ZoomTrackBar = this.trbZoom;

      this.InitAccelerators();
      this.InitializeDataBindings();
      this.InitializeCustomElements();
      this.InitializeDropDowns();
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Enumerations                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region ENUMS

    /// <summary>
    /// Delegate for the writeConsoleLineDelegate.
    /// </summary>
    /// <param name="consoleLine">A <see cref="string"/> with the new console line.</param>
    private delegate void WriteConsoleLine(string consoleLine);

    /// <summary>
    /// Delegate for the calculationFinishedDelegate.
    /// </summary>
    private delegate void CalculationFinished();

    #endregion ENUMS

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
    /// This method parses the .png files in the SaliencyCalculation
    /// path of the <see cref="Settings.SaliencyCalculationPath"/>.
    /// Each channel image is added to the channel listview.
    /// Each saliency map is added to the saliency map listview.
    /// </summary>
    /// <param name="templateImage">A <see cref="string"/>
    /// with the name of the image that is analyzed, to avoid
    /// interpreting this as channel.</param>
    internal void LoadChannelMaps(string templateImage)
    {
      this.Cursor = Cursors.WaitCursor;
      string templateFileName = Path.GetFileNameWithoutExtension(templateImage);
      this.lsvChannels.BeginUpdate();
      this.lsvSalmaps.BeginUpdate();

      string workingPath = Properties.Settings.Default.SaliencyCalculationPath;
      DirectoryInfo dir = new DirectoryInfo(workingPath);
      FileInfo[] pngfiles = dir.GetFiles("*.png");
      if (pngfiles.Length > 1)
      {
        // All channels have equal size, so use first one except is is the template image.
        int usedChannelImage = 0;
        if (Path.GetFileNameWithoutExtension(pngfiles[0].Name) != templateFileName)
        {
          usedChannelImage = 0;
        }
        else
        {
          usedChannelImage = 1;
        }

        Image firstImage = Images.GetImageOfFile(pngfiles[usedChannelImage].FullName);

        this.imlChannels.ImageSize = new Size(firstImage.Width, firstImage.Height);
        this.imlSalmaps.ImageSize = new Size(firstImage.Width, firstImage.Height);

        // Parse all channel outputs
        foreach (FileInfo f in pngfiles)
        {
          int extensionPosition = f.Name.IndexOf(".png");

          if (f.Name == "COcolor-000000.png")
          {
            this.imlChannels.Images.Add(f.FullName, Images.GetImageOfFile(f.FullName));
            this.lsvChannels.Items.Add("Color Channel, Type C", f.FullName);
          }
          else if (f.Name == "COcomposite-color-000000.png")
          {
            this.imlChannels.Images.Add(f.FullName, Images.GetImageOfFile(f.FullName));
            this.lsvChannels.Items.Add("Color Channel, Type S1", f.FullName);
          }
          else if (f.Name == "COMultiColorBand-000000.png")
          {
            this.imlChannels.Images.Add(f.FullName, Images.GetImageOfFile(f.FullName));
            this.lsvChannels.Items.Add("Color Channel, Type G", f.FullName);
          }
          else if (f.Name == "COsingle-opponent-color-000000.png")
          {
            this.imlChannels.Images.Add(f.FullName, Images.GetImageOfFile(f.FullName));
            this.lsvChannels.Items.Add("Color Channel, Type S2", f.FullName);
          }
          else if (f.Name == "COIntensityBand-000000.png")
          {
            this.imlChannels.Images.Add(f.FullName, Images.GetImageOfFile(f.FullName));
            this.lsvChannels.Items.Add("Intensity Channel, Type N", f.FullName);
          }
          else if (f.Name == "SOintensity-000000.png")
          {
            this.imlChannels.Images.Add(f.FullName, Images.GetImageOfFile(f.FullName));
            this.lsvChannels.Items.Add("Intensity Channel, Type I", f.FullName);
          }
          else if (f.Name == "COorientation-000000.png")
          {
            this.imlChannels.Images.Add(f.FullName, Images.GetImageOfFile(f.FullName));
            this.lsvChannels.Items.Add("Orientation Channel", f.FullName);
          }
          else if (f.Name == "COLJunction-000000.png")
          {
            this.imlChannels.Images.Add(f.FullName, Images.GetImageOfFile(f.FullName));
            this.lsvChannels.Items.Add("L Junction Channel", f.FullName);
          }
          else if (f.Name == "COTJunction-000000.png")
          {
            this.imlChannels.Images.Add(f.FullName, Images.GetImageOfFile(f.FullName));
            this.lsvChannels.Items.Add("T Junction Channel", f.FullName);
          }
          else if (f.Name == "COXJunction-000000.png")
          {
            this.imlChannels.Images.Add(f.FullName, Images.GetImageOfFile(f.FullName));
            this.lsvChannels.Items.Add("X Junction Channel", f.FullName);
          }
          else if (f.Name == "SOpedestrian-000000.png")
          {
            this.imlChannels.Images.Add(f.FullName, Images.GetImageOfFile(f.FullName));
            this.lsvChannels.Items.Add("Pedestrian Channel", f.FullName);
          }
          else if (f.Name == "SOSkinHue-000000.png")
          {
            this.imlChannels.Images.Add(f.FullName, Images.GetImageOfFile(f.FullName));
            this.lsvChannels.Items.Add("Skin Hue Channel", f.FullName);
          }
          else if (f.Name.StartsWith("SM0"))
          {
            string key = "SaliencyMap" + f.Name.Substring(extensionPosition - 3, 3);
            this.imlSalmaps.Images.Add(f.FullName, Images.GetImageOfFile(f.FullName));
            this.lsvSalmaps.Items.Add(key, f.FullName);
          }
        }
      }

      this.lsvChannels.EndUpdate();
      this.lsvSalmaps.EndUpdate();
      this.Cursor = Cursors.Default;
    }

    #endregion //PUBLICMETHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Inherited methods                                                         //
    ///////////////////////////////////////////////////////////////// /////////////
    #region OVERRIDES

    /// <summary>
    /// Initialize the delegates.
    /// </summary>
    protected override void InitializeCustomElements()
    {
      base.InitializeCustomElements();
      this.writeConsoleLineDelegate = new WriteConsoleLine(this.WriteConsoleLineMethod);
      this.calculationFinishedDelegate = new CalculationFinished(this.CalculationFinishedMethod);

      // Remove this tab, because the options provided from ezvision
      // don´t work well with still images.
      this.tacOptions.TabPages.Remove(this.tabOptions);

      string gazeDisplayModeFromSettings = this.cbbGazeDisplayMode.Text;
      this.cbbGazeDisplayMode.Items.AddRange(Enum.GetNames(typeof(FixationDrawingMode)));
      this.cbbGazeDisplayMode.ComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cbbGazeDisplayMode.SelectedItem = gazeDisplayModeFromSettings;
      this.btnHelp.Click += new EventHandler(this.btnHelp_Click);
      this.pnlCanvas.Resize += new EventHandler(this.pnlCanvas_Resize);
    }

    /// <summary>
    /// Initialize drop down controls.
    /// </summary>
    /// <remarks>The toolstrip combo box does currently not know the
    /// <see cref="ComboBox.SelectionChangeCommitted"/> event, so here we initialize it
    /// from the <see cref="ToolStripComboBox.ComboBox"/> member.</remarks>
    protected override void InitializeDropDowns()
    {
      base.InitializeDropDowns();
      this.cbbGazeDisplayMode.ComboBox.SelectionChangeCommitted += new EventHandler(this.cbbGazeDisplayMode_SelectionChangeCommitted);
    }

    /// <summary>
    /// Initializes accelerator keys. Binds to methods.
    /// </summary>
    protected override void InitAccelerators()
    {
      base.InitAccelerators();
      SetAccelerator(Keys.Escape, new AcceleratorAction(this.OnEscape));
    }

    /// <summary>
    /// Unregister custom events.
    /// </summary>
    protected override void CustomDispose()
    {
      if (this.ezvision != null)
      {
        this.ezvision.Close();
        this.ezvision.Dispose();
      }

      base.CustomDispose();
      this.cbbGazeDisplayMode.ComboBox.SelectionChangeCommitted -= new EventHandler(this.cbbGazeDisplayMode_SelectionChangeCommitted);
    }

    /// <summary>
    /// Reads dropdown settings and loads corresponding images and data from database.
    /// Then notifys picture the changes.
    /// </summary>
    /// <returns><strong>True</strong>, if trial selection was successful, otherwise
    /// <strong>false</strong>.</returns>
    protected override bool NewTrialSelected()
    {
      try
      {
        // Reset picture
        this.ResetForm();

        // Read current selection state
        int trialID = Document.ActiveDocument.SelectionState.TrialID;

        // Switch to WaitCursor
        this.Cursor = Cursors.WaitCursor;

        // Read settings
        ExperimentSettings set = Document.ActiveDocument.ExperimentSettings;
        if (set != null)
        {
          // Load trial stimulus into picture
          if (!this.LoadTrialStimulus(trialID))
          {
            return false;
          }

          this.LoadTrialSlidesIntoTimeline(this.trialTimeLine);
        }

        // Reset data state label
        ((MainForm)this.MdiParent).StatusRightLabel.Text = string.Empty;
      }
      catch (Exception ex)
      {
        ExceptionMethods.HandleException(ex);
      }
      finally
      {
        // Reset Cursor
        this.Cursor = Cursors.Default;
      }

      return true;
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
    /// <see cref="Form.Load"/> event handler. 
    /// Wires Mainform events and initializes vector graphics picture.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void SaliencyModule_Load(object sender, EventArgs e)
    {
      try
      {
        this.cbbGazeDisplayMode.SelectedItem = Properties.Settings.Default.GazeFixationsDrawingMode;

        this.nudGazeFixDiameterDiv.Value = (decimal)Document.ActiveDocument.ExperimentSettings.GazeDiameterDiv;
        this.saliencyPicture.GazeFixationsDiameterDivisor = Document.ActiveDocument.ExperimentSettings.GazeDiameterDiv;
        this.saliencyPicture.SampleTypeToDraw = SampleType.Gaze;

        this.saliencyPicture.PresentationSize = Document.ActiveDocument.PresentationSize;
        this.ResizeCanvas();

        // Show first stimulus picture
        this.InitialDisplay();

        // Resize picture
        this.pnlPicture.Bounds = GetProportionalBounds(this.pnlCanvas);
      }
      catch (Exception ex)
      {
        ExceptionMethods.HandleException(ex);
      }
    }

    /// <summary>
    /// The <see cref="Form.FormClosing"/> event handler. 
    /// Cancels the closing if background threads are busy.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">The <see cref="FormClosingEventArgs"/> with the event data.</param>
    private void SaliencyModule_FormClosing(object sender, FormClosingEventArgs e)
    {
      if (this.saliencyThread != null && this.saliencyThread.IsAlive)
      {
        InformationDialog.Show(
          "Calculation in progress",
          "Please wait for finishing or abort calculation (ESC) before closing form.",
          false,
          MessageBoxIcon.Information);
        e.Cancel = true;
      }
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnGazePenStyle"/>.
    /// Opens a <see cref="PenAndFontStyleDlg"/> and wires update events to
    /// member methods <see cref="OnPenChanged"/> and <see cref="OnFontStyleChanged"/>
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnGazePenStyle_Click(object sender, EventArgs e)
    {
      Pen backupPen = this.saliencyPicture.GazeFixationsPen;
      Font backupFont = this.saliencyPicture.GazeFixationsFont;
      SolidBrush backupBrush = new SolidBrush(this.saliencyPicture.GazeFixationsFontColor);
      PenAndFontStyleDlg dlgFixationStyle = new PenAndFontStyleDlg();
      dlgFixationStyle.Text = "Set gaze fixations pen and font style...";
      dlgFixationStyle.Pen = this.saliencyPicture.GazeFixationsPen;
      dlgFixationStyle.CustomFont = this.saliencyPicture.GazeFixationsFont;
      dlgFixationStyle.CustomFontBrush = new SolidBrush(this.saliencyPicture.GazeFixationsFontColor);
      dlgFixationStyle.PenChanged += new EventHandler<PenChangedEventArgs>(this.OnPenChanged);
      dlgFixationStyle.FontStyleChanged += new EventHandler<FontChangedEventArgs>(this.OnFontStyleChanged);

      if (dlgFixationStyle.ShowDialog() == DialogResult.Cancel)
      {
        PenChangedEventArgs ea = new PenChangedEventArgs(backupPen, VGStyleGroup.FIX_GAZE_ELEMENT);
        FontChangedEventArgs eaf = new FontChangedEventArgs(backupFont, backupBrush.Color, VGAlignment.Center, VGStyleGroup.FIX_GAZE_ELEMENT);
        this.saliencyPicture.PenChanged(this, ea);
        this.saliencyPicture.FontStyleChanged(this, eaf);
      }
    }

    /// <summary>
    /// The <see cref="OgamaControls.Dialogs.PenAndFontStyleDlg.PenChanged"/> 
    /// event handler for the <see cref="OgamaControls.Dialogs.PenAndFontStyleDlg"/>.
    /// Updates pictures elements by calling the pictures pen changed event handler.
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">A <see cref="PenChangedEventArgs"/> with group and pen to change.</param>
    private void OnPenChanged(object sender, PenChangedEventArgs e)
    {
      e.ElementGroup = VGStyleGroup.FIX_GAZE_ELEMENT;
      this.saliencyPicture.PenChanged(sender, e);
    }

    /// <summary>
    /// The <see cref="OgamaControls.Dialogs.PenAndFontStyleDlg.FontStyleChanged"/> 
    /// event handler for the <see cref="OgamaControls.Dialogs.PenAndFontStyleDlg"/>.
    /// Updates picture elements by calling the pictures font changed event handler.
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">A <see cref="FontChangedEventArgs"/> with group and font to change.</param>
    private void OnFontStyleChanged(object sender, FontChangedEventArgs e)
    {
      e.ElementGroup = VGStyleGroup.FIX_GAZE_ELEMENT;
      this.saliencyPicture.FontStyleChanged(sender, e);
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnGazeConnections"/>.
    /// Enables or disables the gaze connections display.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnGazeConnections_Click(object sender, EventArgs e)
    {
      this.saliencyPicture.GazeConnections = this.btnGazeConnections.Checked;
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnGazeNumbers"/>.
    /// Enables or disables the gaze enumeration display.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnGazeNumbers_Click(object sender, EventArgs e)
    {
      this.saliencyPicture.GazeNumbers = this.btnGazeNumbers.Checked;
    }

    /// <summary>
    /// The <see cref="NumericUpDown.ValueChanged"/> event handler for the
    /// <see cref="NumericUpDown"/> <see cref="nudGazeFixDiameterDiv"/>.
    /// User selected new divider for gaze fixation diameters,
    /// so set the <see cref="PictureWithFixations.GazeFixationsDiameterDivisor"/> property.    
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void nudGazeFixDiameterDiv_ValueChanged(object sender, EventArgs e)
    {
      this.saliencyPicture.GazeFixationsDiameterDivisor = (float)this.nudGazeFixDiameterDiv.Value;
    }

    /// <summary>
    /// The <see cref="ComboBox.SelectionChangeCommitted"/> event handler for the
    /// <see cref="ComboBox"/> <see cref="cbbGazeDisplayMode"/>.
    /// User selected new gaze display mode from drop down list,
    /// so update pictures drawing mode flags and call recalculation.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void cbbGazeDisplayMode_SelectionChangeCommitted(object sender, EventArgs e)
    {
      this.saliencyPicture.GazeDrawingMode = (FixationDrawingMode)Enum.Parse(typeof(FixationDrawingMode), (string)this.cbbGazeDisplayMode.SelectedItem);
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnSeekNextSlide"/>.
    /// Activate next slide in trial.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnSeekNextSlide_Click(object sender, EventArgs e)
    {
      this.trialTimeLine.HighlightNextSlide(true);
      this.LoadSlide(this.CurrentTrial[this.trialTimeLine.HighlightedSlideIndex], ActiveXMode.BehindPicture);
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnSeekPreviousSlide"/>.
    /// Activate previous slide in trial.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnSeekPreviousSlide_Click(object sender, EventArgs e)
    {
      this.trialTimeLine.HighlightNextSlide(false);
      if (this.CurrentTrial.Count >= this.trialTimeLine.HighlightedSlideIndex)
      {
        this.LoadSlide(this.CurrentTrial[this.trialTimeLine.HighlightedSlideIndex], ActiveXMode.BehindPicture);
      }
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnStartCalculation"/>.
    /// User pressed "Start calculation" button. So run the 
    /// backgroundworker to do the job if it is not busy.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnStartCalculation_Click(object sender, EventArgs e)
    {
      this.ResetForm();

      // Return if we have no data
      if (Document.ActiveDocument.SelectionState.TrialID < 0)
      {
        return;
      }

      // Update status bar of main form.
      ((MainForm)this.MdiParent).StatusLabel.Text = "Calculating salient locations ...";
      ((MainForm)this.MdiParent).StatusProgressbar.Style = ProgressBarStyle.Marquee;

      // Start progress timer.
      this.tmrProgress.Start();

      // Initialize thread
      this.saliencyThread = new Thread(new ParameterizedThreadStart(this.SaliencyThread_DoWork));
      this.saliencyThread.SetApartmentState(ApartmentState.STA);
      string ezvisionArguments = this.GenerateArguments();
      this.saliencyThread.Start(ezvisionArguments);
    }

    /// <summary>
    /// The <see cref="CheckBox.CheckedChanged"/> event handler for the
    /// <see cref="CheckBox"/> of the custom channel selection.
    /// User selected to calculate a custom channel in the saliency
    /// map calculation, so check the <see cref="RadioButton"/> 
    /// <see cref="rdbCustomChannels"/>
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void chbChannels_CheckedChanged(object sender, EventArgs e)
    {
      this.rdbCustomChannels.Checked = true;
    }

    /// <summary>
    /// The <see cref="ComboBox.SelectionChangeCommitted"/> event handler for the
    /// <see cref="ComboBox"/> <see cref="cbbPredefinedChannels"/>.
    /// User selected a predefined channel selection, so check the <see cref="RadioButton"/> 
    /// <see cref="rdbPredefinedChannels"/>
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void cbbPredefinedChannels_SelectionChangeCommitted(object sender, EventArgs e)
    {
      this.rdbPredefinedChannels.Checked = true;
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnShowChannels"/>.
    /// Shows or hides the channel splitter control panel.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnShowChannels_Click(object sender, EventArgs e)
    {
      this.spcPicChannels.Panel2Collapsed = !this.btnShowChannels.Checked;
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnShowHideConsole"/>.
    /// Shows or hides the console output textbox at 
    /// the bottom of the form.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnShowHideConsole_Click(object sender, EventArgs e)
    {
      this.spcPictureConsole.Panel2Collapsed = !this.btnShowHideConsole.Checked;
    }

    /// <summary>
    /// The <see cref="RadioButton.CheckedChanged"/> event handler for
    /// the <see cref="RadioButton"/> <see cref="rdbLargeIcon"/>.
    /// Updates the <see cref="ListView.View"/> property to the selected
    /// state.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void rdbLargeIcon_CheckedChanged(object sender, EventArgs e)
    {
      if (!this.rdbLargeIcon.Checked)
      {
        this.lsvChannels.View = View.List;
        this.lsvSalmaps.View = View.List;
      }
      else
      {
        this.lsvChannels.View = View.LargeIcon;
        this.lsvSalmaps.View = View.LargeIcon;
      }
    }

    /// <summary>
    /// The <see cref="ListView.SelectedIndexChanged"/> event handler
    /// for the <see cref="ListView"/> <see cref="lsvChannels"/>.
    /// Updates the saliency picture with an attention map of
    /// the selected channel.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void lsvChannels_SelectedIndexChanged(object sender, EventArgs e)
    {
      ListViewItem currentItem = this.lsvChannels.FocusedItem;
      if (currentItem != null)
      {
        this.VisualizeOverlay(currentItem.ImageKey);
      }
    }

    /// <summary>
    /// Udates the saliency picture with an attention map of
    /// the selected overlay map.
    /// </summary>
    /// <param name="filename">A <see cref="string"/>with the channel filename to
    /// be visualized.</param>
    private void VisualizeOverlay(string filename)
    {
      this.Cursor = Cursors.WaitCursor;
      this.saliencyPicture.VisualizeChannelMapOverlay(filename);
      this.Cursor = Cursors.Default;
    }

    /// <summary>
    /// The <see cref="ListView.SelectedIndexChanged"/> event handler
    /// for the <see cref="ListView"/> <see cref="lsvSalmaps"/>.
    /// Updates the saliency picture with an attention map of
    /// the selected saliency map.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void lsvSalmaps_SelectedIndexChanged(object sender, EventArgs e)
    {
      ListViewItem currentItem = this.lsvSalmaps.FocusedItem;
      if (currentItem != null)
      {
        this.VisualizeOverlay(currentItem.ImageKey);
      }
    }

    #endregion //WINDOWSEVENTHANDLER

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler for Custom Defined Events                                    //
    ///////////////////////////////////////////////////////////////////////////////
    #region CUSTOMEVENTHANDLER

    /// <summary>
    /// The <see cref="Process.Exited"/> event handler for the
    /// <see cref="Process"/> <see cref="ezvision"/>.
    /// Updates the saliency picture with the
    /// calculated fixation positions and invokes
    /// the <see cref="calculationFinishedDelegate"/>.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void ezvision_Exited(object sender, EventArgs e)
    {
      int duration;
      DataTable simulatedFixations = this.ParseEzvisionLog(out duration);
      if (simulatedFixations != null && simulatedFixations.Rows.Count > 0)
      {
        this.Picture.SectionEndTime = duration;
        this.saliencyPicture.GazeFixations = simulatedFixations;
      }

      BeginInvoke(this.calculationFinishedDelegate);
    }

    /// <summary>
    /// The <see cref="Process.ErrorDataReceived"/> event handler for the
    /// <see cref="Process"/> <see cref="ezvision"/>.
    /// New data from the error output is available which is written
    /// to the output consol textbox by invoking
    /// <see cref="writeConsoleLineDelegate"/>.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="errorLine">A  <see cref="DataReceivedEventArgs"/> with the event data.</param>
    private void ezvision_ErrorDataReceived(object sender, DataReceivedEventArgs errorLine)
    {
      // Collect the sort command output.
      if (!string.IsNullOrEmpty(errorLine.Data))
      {
        BeginInvoke(this.writeConsoleLineDelegate, new object[] { errorLine.Data });
        if (errorLine.Data.Contains("DONE"))
        {
          this.ezvision.Kill();
          this.ezvision.Close();
        }
      }
    }

    /// <summary>
    /// The <see cref="Process.OutputDataReceived"/> event handler for the
    /// <see cref="Process"/> <see cref="ezvision"/>.
    /// New data from the ezvision output is available which is written
    /// to the output consol textbox by invoking
    /// <see cref="writeConsoleLineDelegate"/>.
    /// This method also kills the process, when the line contains the
    /// keyword "DONE", because otherwise ezvision will sometimes
    /// hang due to memory management errors that don´t affedted the calculation.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="outLine">A  <see cref="DataReceivedEventArgs"/> with the event data.</param>
    private void ezvision_OutputDataReceived(object sender, DataReceivedEventArgs outLine)
    {
      // Collect the sort command output.
      if (!string.IsNullOrEmpty(outLine.Data))
      {
        // Add the text to the collected output.
        BeginInvoke(this.writeConsoleLineDelegate, new object[] { outLine.Data });
        if (outLine.Data.Contains("DONE"))
        {
          this.ezvision.Kill();
          this.ezvision.Close();
        }
      }
    }

    /// <summary>
    /// Cancels background thread when escape key is pressed.
    /// </summary>
    private void OnEscape()
    {
      if (this.ezvision.SynchronizingObject != null)
      {
        this.ezvision.Kill();
        this.ezvision.Close();
      }

      ((MainForm)this.MdiParent).StatusLabel.Text = "Status: Saliency map calculation cancelled.";
      ((MainForm)this.MdiParent).StatusProgressbar.Value = 0;
      this.Cursor = Cursors.Default;
      this.saliencyPicture.Cursor = Cursors.Default;
    }

    #endregion //CUSTOMEVENTHANDLER

    #endregion //EVENTS

    ///////////////////////////////////////////////////////////////////////////////
    // Methods and Eventhandling for Background tasks                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region BACKGROUNDWORKER

    /// <summary>
    /// The Thread main function for the
    /// <see cref="Thread"/> SaliencyThread.
    /// Thread working method for calculating
    /// the saliency map according to settings in UI.
    /// </summary>
    /// <param name="data">An <see cref="object"/> with the parameters.</param>
    private void SaliencyThread_DoWork(object data)
    {
      this.CalculateSalienceMap((string)data);
    }

    /// <summary>
    /// The <see cref="System.Windows.Forms.Timer.Tick"/> event handler for the
    /// <see cref="System.Windows.Forms.Timer"/> <see cref="tmrProgress"/>
    /// Increases the progress value of the main forms
    /// progress bar to advice running job.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void tmrProgress_Tick(object sender, EventArgs e)
    {
      ((MainForm)this.MdiParent).StatusProgressbar.Value++;
      if (((MainForm)this.MdiParent).StatusProgressbar.Value >= 100)
      {
        ((MainForm)this.MdiParent).StatusProgressbar.Value = 0;
      }
    }

    #endregion //BACKGROUNDWORKER

    ///////////////////////////////////////////////////////////////////////////////
    // Methods for doing main class job                                          //
    ///////////////////////////////////////////////////////////////////////////////
    #region METHODS

    /// <summary>
    /// This method is used for one time after installation to
    /// ensure the ezvision files be available at
    /// local application data folder to enable writing into this folder.
    /// </summary>
    private void CopyEZVisionToLocalApplicationData()
    {
      if (!Directory.Exists(Properties.Settings.Default.SaliencyCalculationPath))
      {
        Directory.CreateDirectory(Properties.Settings.Default.SaliencyCalculationPath);
      }

      // Copy ezvision and cygwin files to local application data.
      DirectoryInfo info = new DirectoryInfo(Path.Combine(Application.StartupPath, "SaliencyCalculation"));
      foreach (FileInfo saliencyFile in info.GetFiles())
      {
        string destination = Path.Combine(Properties.Settings.Default.SaliencyCalculationPath, saliencyFile.Name);
        if (!File.Exists(destination))
        {
          File.Copy(saliencyFile.FullName, destination);
        }
      }
    }

    /// <summary>
    /// This methos is called when the ezvision
    /// calculation has finished. It updates the channel listviews
    /// and the main forms status bar.
    /// </summary>
    private void CalculationFinishedMethod()
    {
      this.tmrProgress.Stop();
      this.saliencyPicture.DrawFixations(true);
      this.LoadChannelMaps(Document.ActiveDocument.SelectionState.TrialID.ToString() + ".png");
      ((MainForm)this.MdiParent).StatusLabel.Text = "Calculation finished ...";
      ((MainForm)this.MdiParent).StatusProgressbar.Style = ProgressBarStyle.Continuous;
      ((MainForm)this.MdiParent).StatusProgressbar.Value = 0;
    }

    /// <summary>
    /// This method is called every time a new console line is available
    /// from the calculation thread and updates the <see cref="txbConsole"/>
    /// </summary>
    /// <param name="consoleLine">A <see cref="string"/> with the 
    /// line to append to the textbox.</param>
    private void WriteConsoleLineMethod(string consoleLine)
    {
      this.txbConsole.AppendText(consoleLine + Environment.NewLine);
    }

    /// <summary>
    /// This method is the main entry point for calling the ezvision.exe.
    /// It wires the event handler and starts the process.
    /// </summary>
    /// <param name="arguments">A <see cref="string"/> with the
    /// list of arguments that should be given to the ezvision.exe</param>
    private void CalculateSalienceMap(string arguments)
    {
      try
      {
        this.ezvision = new Process();

        string workingPath = Properties.Settings.Default.SaliencyCalculationPath;

        this.ezvision.Exited += new EventHandler(this.ezvision_Exited);

        // Set our event handler to asynchronously read the output.
        this.ezvision.OutputDataReceived += new DataReceivedEventHandler(this.ezvision_OutputDataReceived);

        // Set our event handler to asynchronously read the error.
        this.ezvision.ErrorDataReceived += new DataReceivedEventHandler(this.ezvision_ErrorDataReceived);

        this.ezvision.EnableRaisingEvents = true;
        this.ezvision.StartInfo.Arguments = arguments;
        this.ezvision.StartInfo.CreateNoWindow = true;
        string ezvisionFile = Path.Combine(workingPath, "ezvision.exe");

        // Be sure to have ezvision copied to local application data
        if (!File.Exists(ezvisionFile))
        {
          this.CopyEZVisionToLocalApplicationData();
        }

        this.ezvision.StartInfo.WorkingDirectory = workingPath;
        this.ezvision.StartInfo.FileName = ezvisionFile;

        Clipboard.SetText("ezvision.exe " + arguments.ToString());

        this.ezvision.StartInfo.UseShellExecute = false;

        // Redirect the standard error of the _ezvision program.  
        // This stream is read asynchronously using an event handler.
        this.ezvision.StartInfo.RedirectStandardError = true;

        // Redirect the standard output of the _ezvision program. 
        // This stream is read asynchronously using an event handler.
        this.ezvision.StartInfo.RedirectStandardOutput = true;

        // Start the process.
        this.ezvision.Start();

        // Start the asynchronous read of the output stream.
        this.ezvision.BeginOutputReadLine();

        // Start the asynchronous read of the error stream.
        this.ezvision.BeginErrorReadLine();

        this.ezvision.WaitForExit();

        this.ezvision.Exited -= new EventHandler(this.ezvision_Exited);
        this.ezvision.OutputDataReceived -= new DataReceivedEventHandler(this.ezvision_OutputDataReceived);
        this.ezvision.ErrorDataReceived -= new DataReceivedEventHandler(this.ezvision_ErrorDataReceived);

        this.ezvision.Close();
      }
      catch (Exception ex)
      {
        this.tmrProgress.Stop();

        ExceptionMethods.HandleException(ex);
      }
    }

    /// <summary>
    /// A future update will use this method to update the picture
    /// every time a new covert shift was detected.
    /// Currently the picture is only updated at the end of the process.
    /// </summary>
    /// <param name="line">A <see cref="string"/> with the output to parse for
    /// the WinnerTakeAll line.</param>
    private void ParseOutLine(string line)
    {
      ////WinnerTakeAll::evolve: ##### WinnerTakeAll winner (112,560) at 348.600000ms [boring] #####
      //////only use lines containing "CovertShift"
      ////if (line.Contains("WinnerTakeAll winner"))
      ////{
      ////  //Split separated line items
      ////  string[] items = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
      ////  //string timeString = items[0].Replace("ms", "");
      ////  Single startTime = 0f;

      ////  //Specify the dot as decimal separator.
      ////  NumberFormatInfo nfi = new CultureInfo("en-US", false).NumberFormat;
      ////  nfi.NumberDecimalSeparator = ".";

      ////  Single.TryParse(timeString, NumberStyles.Float, nfi, out startTime);
      ////  int startTimeMS = (int)startTime;

      ////  foreach (string item in items)
      ////  {
      ////    if (item.Contains("(") && item.Contains(")"))
      ////    {
      ////      string strCoordinatePair = item.Replace("(", "");
      ////      strCoordinatePair = strCoordinatePair.Replace(")", "");
      ////      string[] coordinates = strCoordinatePair.Split(',');

      ////      FixationData fixation = new FixationData();
      ////      fixation.StartTime = startTimeMS;
      ////      fixation.PosX = Convert.ToSingle(coordinates[0]);
      ////      fixation.PosY = Convert.ToSingle(coordinates[1]);
      ////      fixation.CountInTrial = fixCounter;

      ////      fixations.Add(fixation);

      ////      //Update length of foregoing fixation
      ////      if (fixations.Count > 1)
      ////      {
      ////        FixationData foregoingFix = fixations[fixCounter - 1];
      ////        foregoingFix.Length = (int)(startTimeMS - foregoingFix.StartTime);
      ////        fixations[fixCounter - 1] = foregoingFix;
      ////      }

      ////      fixCounter++;
      ////      break;
      ////    }
      ////  }
      ////}
    }

    /// <summary>
    /// This method parses the ezvisionlog.txt which is the output of the 
    /// ezvision.exe for the lines with the "CovertShift", 
    /// that indicates the fixation shift.
    /// </summary>
    /// <param name="duration">Out. An <see cref="Int32"/> with the duration
    /// up to the last fixation.</param>
    /// <returns>A <see cref="DataTable"/> with the parsed fixations.</returns>
    private DataTable ParseEzvisionLog(out int duration)
    {
      string workingPath = Properties.Settings.Default.SaliencyCalculationPath;
      string importFile = Path.Combine(workingPath, "ezvisionlog.txt");
      string line = string.Empty;
      int fixCounter = 0;
      duration = 0;
      var fixations = new SQLiteOgamaDataSet.GazeFixationsDataTable();

      // Check import file.
      if (!File.Exists(importFile))
      {
        throw new FileNotFoundException("The import file could not be found");
      }

      // Begin reading File
      try
      {
        using (StreamReader importReader = new StreamReader(importFile))
        {
          // Read ImportFile
          while ((line = importReader.ReadLine()) != null)
          {
            // only use lines containing "CovertShift"
            if (!line.Contains("CovertShift"))
            {
              continue;
            }

            // Split separated line items
            var items = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var timeString = items[0].Replace("ms", string.Empty);
            var startTime = 0f;

            // Specify the dot as decimal separator.
            NumberFormatInfo nfi = new CultureInfo("en-US", false).NumberFormat;
            nfi.NumberDecimalSeparator = ".";

            float.TryParse(timeString, NumberStyles.Float, nfi, out startTime);
            var startTimeMS = (int)startTime;

            foreach (string item in items)
            {
              if (item.Contains("(") && item.Contains(")"))
              {
                string strCoordinatePair = item.Replace("(", string.Empty);
                strCoordinatePair = strCoordinatePair.Replace(")", string.Empty);
                string[] coordinates = strCoordinatePair.Split(',');

                var fixationRow = fixations.NewGazeFixationsRow();

                // Dummy entries
                fixationRow.SubjectName = string.Empty;
                fixationRow.TrialID = 0;
                fixationRow.TrialSequence = 0;

                // Interesting entries.
                fixationRow.StartTime = startTimeMS;
                fixationRow.PosX = Convert.ToInt32(coordinates[0]);
                fixationRow.PosY = Convert.ToInt32(coordinates[1]);
                fixationRow.CountInTrial = fixCounter;
                fixations.AddGazeFixationsRow(fixationRow);

                // Update length of foregoing fixation
                if (fixCounter >= 1)
                {
                  fixations[fixCounter - 1].Length = (int)(startTimeMS - fixations[fixCounter - 1].StartTime);
                  duration = (int)(fixations[fixCounter - 1].StartTime + fixations[fixCounter - 1].Length);
                }

                fixCounter++;
                break;
              }
            }
          }

          if (fixations.Count > 0)
          {
            fixations.Rows[fixations.Count - 1].Delete();
          }
        }

        return fixations;
      }
      catch (Exception ex)
      {
        ExceptionMethods.HandleException(ex);
      }

      return null;
    }

    /// <summary>
    /// This method generates the command line arguments for the
    /// ezvision.exe using the parameters defined in the UI of
    /// the form
    /// </summary>
    /// <returns>A <see cref="string"/> with the ezvision.exe 
    /// command line arguments.</returns>
    private string GenerateArguments()
    {
      StringBuilder arguments = new StringBuilder();
      string workingPath = Properties.Settings.Default.SaliencyCalculationPath;

      // Append input image
      string fileName = Document.ActiveDocument.SelectionState.TrialID + ".png";
      Image convertedImage = this.Picture.RenderToImage();
      string tmpFile = Path.Combine(workingPath, fileName);
      if (File.Exists(tmpFile))
      {
        File.Delete(tmpFile);
      }

      convertedImage.Save(tmpFile, ImageFormat.Png);
      arguments.Append("--in='" + fileName + "' ");

      // all the command-line arguments to stdout after command-line parsing is done.
      arguments.Append("--echo-args ");

      // Don´t use floating-point exceptions, which will continue execution if overflow, 
      // underflow, invalid or division by zero are encountered
      arguments.Append("--nouse-fpe ");

      // Don´t Add small amounts of random noise to various stages of model
      arguments.Append("--nouse-random ");

      // The mode of the prefrontalCortex. Train: is for training from some data, 
      // Bias is to bias for some object and Rec is for recognition. None does nothing.
      arguments.Append("--pfc-mode=None ");

      // Type of PrefrontalCortex to use. 'Stub' for a simple pass-through of 
      // input (no biasing)frames, 'OG' for the Optimal Gains pfc, 
      // 'SalBayes' for Bayesian tuning of receptors. 
      arguments.Append("--pfc-type=Stub ");

      // Show trajectory, saliency and channels as a combo
      arguments.Append("-K ");

      // Type of Retina to use. 'Stub' for a simple pass-through 
      // of input frames, 'Std' for the standard retina that can foveate, 
      // shift inputs to eye position, embed inputs within a larger framing 
      // image, foveate inputs, etc. Use OSG to get the Open Scene Graph retina.
      arguments.Append("--retina-type=Std ");

      // Eye/Head Controller name 
      arguments.Append("--ehc-type=None ");

      // Type of VisualCortex to use. There are two ways to setup a VisualCortex: 
      // either by using one of the standard names defined below, which correspond 
      // to standard bundled collections of underlying visual processing channels, 
      // or by explicitly listing the visual channels to use. Pre-bundled 
      // collections of channels include:
      //  None: no VisualCortex at all
      //  Std: use all standard channels with unit weights
      //  BeoStd: use all Beowulf channels with unit weights
      //  SurpStd: use all standard Surprise channels
      //  Entrop: entropy model, computing pixel entropy in 16x16 image patches
      //  EyeMvt: fake visual cortex built from human eye movement traces
      //  PN03contrast: Parkhurst & Niebur'03 contrast model
      //  Variance: local variance in 16x16 image patches
      //  Tcorr: temporal correlation in image patches across frames
      //  Scorr: spatial correlation between image patches in a frame
      //  Info: DCT-based local information measure as in Itti et al, PAMI 1998
      // You may also configure which channels to use in your VisualCortex 
      // by specifying a series of letters from:
      //  C: double-opponent color center-surround
      //  D: dummy channel
      //  E: end-stop detector
      //  F: flicker center-surround
      //  G: multi-color band channel
      //  H: H2SV channel
      //  Y: Use a Depth Channel with spatial depth information
      //  I: intensity center-surround
      //  K: skin hue detector
      //  L: L-junction detector
      //  M: motion energy center-surround
      //  N: intensity-band channel
      //  O: orientation contrast
      //  P: SIFT channel
      //  R: Pedestrian channel
      //  S: composite single-opponent color center-surround
      //  T: T-junction detector
      //  V: short-range orientation interactions ("sox") channel
      //  W: contour channel
      //  X: X-junction detector
      //  Z: dummy zero channel
      //  A: Object Detection channel
      // with possible optional weights (given just after the channel letter, with a ':' in between).
      // Finally, in this string you may specify a one of the following 
      // single letters to use a alternate set of channels:
      //  'B' use Beowulf channels rather than normal channels
      //      (for this to work, you will need to be running a
      //      beochannel-server process on each of your Beowulf nodes)
      //  'J' dispatch SingleChannel computations to worker threads; (note
      //      that to actually create the worker threads you must also give
      //      a '-j N' option to create N threads); it is OK to have fewer
      //      threads than channels, in which case each thread would simply
      //      perform more than one channel computation per input cycle
      //  'U' use Surprise channels rather than normal channels
      // EXAMPLE: 'IO:5.0M' will use intensity (weight 1.0), 
      // orientation (weight 5.0) and motion (weight 1.0) channels
      if (this.rdbPredefinedChannels.Checked)
      {
        string typ = this.cbbPredefinedChannels.Text.Substring(0, this.cbbPredefinedChannels.Text.IndexOf(':'));
        arguments.Append("--vc-type=" + typ + " ");
      }
      else if (this.rdbCustomChannels.Checked)
      {
        arguments.Append("--vc-type=");
        string colorType = "S";
        if (this.rdbColorC.Checked)
        {
          colorType = "C";
        }
        else if (this.rdbColorG.Checked)
        {
          colorType = "G";
        }

        if (this.chbChannelColor.Checked)
        {
          arguments.Append(colorType + ":" + this.nudChannelColorWeight.Value.ToString());
        }

        // if (chbChannelContour.Checked)
        //  arguments.Append("W:" + nudChannelContourWeight.Value.ToString());
        string intensityType = "N";
        if (this.rdbIntensityI.Checked)
        {
          intensityType = "I";
        }

        if (this.chbChannelIntensity.Checked)
        {
          arguments.Append(intensityType + ":" + this.nudChannelIntensityWeight.Value.ToString());
        }

        if (this.chbChannelLJunction.Checked)
        {
          arguments.Append("L:" + this.nudChannelLJunctionWeight.Value.ToString());
        }

        if (this.chbChannelObject.Checked)
        {
          arguments.Append("A:" + this.nudChannelObjectWeight.Value.ToString());
        }

        if (this.chbChannelOrientation.Checked)
        {
          arguments.Append("O:" + this.nudChannelOrientationWeight.Value.ToString());
        }

        if (this.chbChannelPedestrian.Checked)
        {
          arguments.Append("R:" + this.nudChannelPedestrianWeight.Value.ToString());
        }

        if (this.chbChannelSkin.Checked)
        {
          arguments.Append("K:" + this.nudChannelSkinWeight.Value.ToString());
        }

        if (this.chbChannelTJunction.Checked)
        {
          arguments.Append("T:" + this.nudChannelTJunctionWeight.Value.ToString());
        }

        if (this.chbChannelXJunction.Checked)
        {
          arguments.Append("X:" + this.nudChannelXJunctionWeight.Value.ToString());
        }

        arguments.Append(" ");
      }

      // Type of Saliency Map to use. 'None' for no saliency map, 
      // 'Std' for the standard saliency map using LeakyIntegrator neurons 
      // (takes a lot of CPU cycles to evolve), 'Trivial' for just a 
      // non-evolving copy of the inputs, 'Fast' for a faster implementation 
      // thatjust takes a weighted average between current and new saliency map 
      // at each time step.
      arguments.Append("--sm-type=" + this.cbbSaliencyType.Text + " ");

      // Type of Task-Relevance Map to use. 'None' for no map,
      // 'Std' for the standard TRM that needs an agent to compute the 
      // relevance of objects, 'KillStatic' for a TRM that progressively 
      // decreases the relevance of static objects, and 'KillN' for a 
      // TRM that kills the last N objects that have been passed to it, 
      // 'GistClassify' for classifythe current frame into different 
      // categories and use the correspondingpre-defined TD map
      arguments.Append("--trm-type=Std ");

      // Type of Attention Guidance Map to use. 'None' for no map 
      // (in which case Brain will use the bottom-up saliency map 
      // for attention guidance), 'Std' for the standard AGM that just 
      // computes the pointwise product between bottom-up saliency map 
      // and top-down task-relevance map.
      arguments.Append("--agm-type=Std ");

      // Type of Winner-Take-All to use. 'None' for no winner-take-all, 
      // 'Std' for the standard winner-take-all using LeakyIntFire neurons 
      // (takes a lot of CPU cycles to evolve), 'Fast' for a faster 
      // implementation that just computes the location of the max at 
      // every time step, 'Greedy' is one that returns, out of a number 
      // of possible targets above a threshold, the one closest to current 
      // eye position. 'Notice' uses an adaptive leaky integrate and fire 
      // neuron that trys to notice things across frames
      // --wta-type=<None|Std|StdOptim|Fast|Greedy|Notice> [Std]
      arguments.Append("--wta-type=" + this.cbbWinnerTakeAllType.Text + " ");

      // Type of SimulationViewer to use. 'Std' is the standard 2D viewer. 
      // 'Compress' is a simple multi-foveated blurring viewer, 
      // in which a selection of most salient locations will be represented crisply, 
      // while the rest of the image will be increasingly blurred 
      // as we move away from those hot spots. 'EyeMvt' is a viewer for 
      // comparison between saliency maps and human eye movements nd 'EyeMvt'
      // also plus it adds a concept of visual memory buffer. 'EyeSim' 
      // simulates an eye-tracker recording from the model.We run surprise 
      // control (ASAC) also from this command option. To use surprise control 
      // use 'ASAC'. 'Stats' saves some simple statistics like mean and variance 
      // saliency, location of peak saliency, etc.
      // --sv-type=<None|Std|Compress|EyeMvt|EyeMvt2|EyeSim|ASAC|NerdCam|Stats|RecStats> [Std]  
      arguments.Append("--sv-type=" + this.cbbSimulationViewerType.Text + " ");

      // Type of InferoTemporalCortex to use. 'None','Std' or 'SalBayes'
      // --it-type=<None|Std|SalBayes> [None]
      arguments.Append("--it-type=None ");

      // Type of gist estimator to use
      // --ge-type=<None|Std|FFT|Texton> [None]
      arguments.Append("--ge-type=None ");

      // Type of MaxNormalization to use
      // --maxnorm-type=<None|Maxnorm|Fancy|FancyFast|FancyOne|FancyLandmark|Landmark|FancyWeak|Ignore|Surprise> [Fancy]  (MaxNormType)
      string maxnormType = "Maxnorm";
      if (this.cbbPredefinedChannels.Text == "SurpStd: use all standard Surprise channels"
                && this.rdbPredefinedChannels.Checked)
      {
        maxnormType = "Surprise";
      }

      arguments.Append("--maxnorm-type=" + maxnormType + " ");

      if (this.rdbStopAfterFixations.Checked)
      {
        int numFixations = (int)(this.nudCountFixations.Value + 1);

        // Exit after numFixations number of covert attention shifts
        arguments.Append("--too-many-shifts=" + numFixations.ToString() + " ");

        // Set output frames to number of fixations to use
        // Framerate is set to EVENT because that will give us only
        // a picture when attention has shifted
        int numFixationsMinusOne = numFixations - 1;
        arguments.Append("--output-frames=0-" + numFixationsMinusOne.ToString() + "@EVENT ");
      }
      else if (this.rdbStopAfterTime.Checked)
      {
        string maxTime = this.nudMilliseconds.Value.ToString() + "ms ";

        // Exit after the maximum specified time
        arguments.Append("--too-much-time=" + maxTime);

        // Set output frames to 0 up to MAX for given Time
        // Framerate is set to EVENT because that will give us only
        // a picture when attention has shifted
        arguments.Append("--output-frames=0-MAX@EVENT ");
      }

      // Keep going even after input is exhausted
      arguments.Append("-+ ");

      // Save conspicuity maps from all complex channels ("CO")
      arguments.Append("--save-conspic-maps ");

      // Save combined center-surround output maps from all single channels ("SO")
      if (this.cbbPredefinedChannels.Text != "EyeMvt: fake visual cortex built from human eye movement traces"
        && this.rdbPredefinedChannels.Checked)
      {
        arguments.Append("--save-featurecomb-maps ");
      }

      // Save text log messages to file
      // in particular this file owns the data for the fixation
      // times and centers
      // string logFile = Path.Combine(workingPath, "ezvisionlog.txt");
      arguments.Append("--textlog=ezvisionlog.txt ");

      // Save all output images to png format
      arguments.Append("--out=png ");

      // arguments.Append("--help ");
      // arguments.Append("--save-vcx-output ");
      // arguments.Append("--save-cumsalmap ");
      arguments.Append("--save-salmap ");

      // arguments.Append("--in=PhysikusKernschattenAn.jpg ");
      // arguments.Append("--just-initial-saliency-map ");
      // arguments.Append("--out=png ");
      // arguments.Append("--out=info:info.txt ");
      // arguments.Append("--out=stats:stats.txt ");
      // arguments.Append("-T ");
      // arguments.Append("--in=testpic001.ppm ");
      // arguments.Append("--nouse-fpe ");
      // arguments.Append("--nouse-random ");
      // arguments.Append("-T ");
      // arguments.Append("--textlog=top5.txt ");
      // arguments.Append("--too-many-shifts=5 ");
      // arguments.Append("--output-frames=0-4@EVENT ");
      // arguments.Append("--out=png ");
      // arguments.Append("-+ ");
      return arguments.ToString();
    }

    #endregion //METHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER

    /// <summary>
    /// This method resets the form by clearing its calculated 
    /// contents and resetting the picture.
    /// </summary>
    private void ResetForm()
    {
      this.lsvChannels.BeginUpdate();
      this.lsvSalmaps.BeginUpdate();
      this.lsvChannels.Clear();
      this.lsvSalmaps.Clear();
      this.imlChannels.Images.Clear();
      this.imlSalmaps.Images.Clear();
      this.lsvChannels.EndUpdate();
      this.lsvSalmaps.EndUpdate();

      Application.DoEvents();

      this.DeleteOldOutput();

      this.saliencyPicture.ResetPicture();
      this.txbConsole.Clear();
    }

    /// <summary>
    /// This method deletes all png files in the SaliencyCalculation
    /// folder of the application.
    /// </summary>
    private void DeleteOldOutput()
    {
      try
      {
        string workingPath = Properties.Settings.Default.SaliencyCalculationPath;
        DirectoryInfo dir = new DirectoryInfo(workingPath);
        FileInfo[] pngfiles = dir.GetFiles("*.png");
        foreach (FileInfo f in pngfiles)
        {
          File.Delete(f.FullName);
        }
      }
      catch (Exception ex)
      {
        ExceptionMethods.HandleExceptionSilent(ex);
      }
    }

    #endregion //HELPER
  }
}