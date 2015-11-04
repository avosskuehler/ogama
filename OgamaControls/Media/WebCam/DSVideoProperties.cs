// <copyright file="WebcamPropertiesDlg.cs" company="FU Berlin">
// Copyright (c) 2008 All Rights Reserved
// </copyright>
// <author>Adrian Voßkühler</author>
// <email>adrian@ogama.net</email>
namespace OgamaControls
{
  using System;
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.Drawing;
  using System.Windows.Forms;
  using DirectShowLib;

  using GTHardware.Cameras.DirectShow;
  
  /// <summary>
  /// This <see cref="UserControl"/> encapsulates an dialog for getting, setting
  /// and previewing the properties of a direct show video including audio input.
  /// </summary>
  public partial class DSVideoProperties : UserControl
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
    /// Saves the current <see cref="CaptureDeviceProperties"/>.
    /// </summary>
    private CaptureDeviceProperties properties;

    ///// <summary>
    ///// Save the current <see cref="DisplayMode"/>
    ///// </summary>
    //private DisplayMode mode;

    ///// <summary>
    ///// This member indicates the preview mode of the control.
    ///// </summary>
    //private bool shouldPreview;

    /// <summary>
    /// The <see cref="DXCapture"/> which is to be controlled.
    /// </summary>
    private DXCapture dxCapture;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the DSVideoProperties class.
    /// </summary>
    public DSVideoProperties()
    {
      InitializeComponent();
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    ///// <summary>
    ///// This <strong>DisplayMode</strong> enumeration desribes
    ///// the different modes in which the dialog can be run. 
    ///// </summary>
    //[Flags]
    //public enum DisplayMode
    //{
    //  /// <summary>
    //  /// No display at all.
    //  /// </summary>
    //  None = 0,

    //  /// <summary>
    //  /// Displays video playback properties
    //  /// </summary>
    //  VideoPlayback = 1,

    //  /// <summary>
    //  /// Displays audio playback properties
    //  /// </summary>
    //  AudioPlayback = 2,

    //  /// <summary>
    //  /// Displays video recording properties (including preview)
    //  /// </summary>
    //  VideoRecord = 4,

    //  /// <summary>
    //  /// Displays all video properties
    //  /// </summary>
    //  Video = 5,

    //  /// <summary>
    //  /// Displays audio recording properties
    //  /// </summary>
    //  AudioRecord = 8,

    //  /// <summary>
    //  /// Displays all audio properties
    //  /// </summary>
    //  Audio = 10,

    //  /// <summary>
    //  /// Displays audio and video playback properties
    //  /// </summary>
    //  Playback = 3,

    //  /// <summary>
    //  /// Displays audio and video recording properties (including preview)
    //  /// </summary>
    //  Record = 12,

    //  /// <summary>
    //  /// Displays all properties including preview.
    //  /// </summary>
    //  All = 15,
    //}

    /// <summary>
    /// Gets or sets the <see cref="DXCapture"/> for this control
    /// </summary>
    public DXCapture DxCapture
    {
      get
      {
        return this.dxCapture;
      }
      set
      {
        this.dxCapture = value;
        this.properties = value.CaptureDeviceProperties;
        this.properties.PreviewWindow = this.panelPreview;
      }
    }

    /// <summary>
    /// Gets or sets the current <see cref="CaptureDeviceProperties"/>.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public CaptureDeviceProperties Properties
    {
      get
      {
        return this.properties;
      }
      //set
      //{
      //  this.properties = value;
      //  this.PopulateDlgWithProperties();
      //}
    }

    ///// <summary>
    ///// Sets the <see cref="DisplayMode"/> for this dialog.
    ///// </summary>
    ///// <value>A <see cref="DisplayMode"/> which indicates the properties to display.</value>
    //[Category("Appearance")]
    //[Description("Sets the displayed properties of this control")]
    //public DisplayMode Mode
    //{
    //  get
    //  {
    //    return mode;
    //  }
    //  set
    //  {
    //    mode = value;
    //    SwitchDisplayMode(mode);
    //  }
    //}

    /// <summary>
    /// Gets or sets the available video sizes.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public List<Size> VideoSizes
    {
      get
      {
        List<Size> videoSizes = new List<Size>();
        foreach (string entry in cbbVideoSize.Items)
        {
          string[] sizes = entry.Split('x');
          videoSizes.Add(new Size(Convert.ToInt32(sizes[0]), Convert.ToInt32(sizes[1])));
        }
        return videoSizes;
      }
      set
      {
        cbbVideoSize.Items.Clear();
        foreach (Size entry in value)
        {
          cbbVideoSize.Items.Add(entry.Width + "x" + entry.Height);
        }

        if (cbbVideoSize.Items.Count > 0)
        {
          cbbVideoSize.SelectedIndex = 0;
        }
      }
    }

    ///// <summary>
    ///// Gets or sets whether this control should be in preview mode or paused.
    ///// </summary>
    ///// <value><strong>True</strong> when control should running preview,
    ///// otherwise <strong>false</strong>.</value>
    //[Category("Appearance")]
    //[Description("True when of this control should preview on start.")]
    //public bool ShouldPreview
    //{
    //  get { return shouldPreview; }
    //  set { shouldPreview = value; }
    //}

    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Public methods                                                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region PUBLICMETHODS
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
    /// The <see cref="UserControl.Load"/> event handler.
    /// Runs preview if possible.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/>.</param>
    private void DSVideoProperties_Load(object sender, EventArgs e)
    {
      this.InitializeUI();
      this.PopulateDlgWithProperties();
      this.RebuildDXCapture(this.properties);
    }

    /// <summary>
    /// The <see cref="Form.FormClosing"/> event handler of the parent form.
    /// Gets called when the parent form is closed.
    /// Has been wired in overridden <see cref="OnCreateControl()"/>.
    /// Stops the webcam control, to free video stream.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A <see cref="FormClosingEventArgs"/> with the event data.</param>
    private void ParentForm_FormClosing(object sender, FormClosingEventArgs e)
    {
      this.UpdateProperties();

      // Only if the purpose is recording, test the capturing
      //if (this.shouldPreview)
      {
        Form parent = (Form)sender;
        if (parent.DialogResult == DialogResult.OK && !this.dxCapture.HasValidGraph)
        {
          e.Cancel = true;
          string message = "Sorry, Ogama cannot initialize the capturing, "
          + Environment.NewLine + "please try using other compressors or devices.";
          MessageBox.Show(message, Application.ProductName);
          return;
        }
      }

      if (this.dxCapture != null)
      {
        this.dxCapture.Dispose();
        this.dxCapture = null;
      }
    }

    private bool TestCapturing()
    {
      if (this.dxCapture == null)
      {
        return false;
      }

      try
      {
        //string temp = System.IO.Path.GetTempFileName();
        //this.DxCapture.Filename = temp;
        this.dxCapture.Start();
      }
      catch (Exception)
      {
        return false;
      }
      finally
      {
        this.dxCapture.Stop();
        this.dxCapture.Dispose();
        this.dxCapture = null;
      }

      return true;
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnVideoDeviceProperties"/>.
    /// Calls a <see cref="Webcam.ShowVideoDeviceConfigureDialog()"/>.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/>.</param>
    private void btnVideoDeviceProperties_Click(object sender, EventArgs e)
    {
      if (this.dxCapture.VideoDeviceFilter != null)
      {
        DirectShowUtils.DisplayPropertyPage(this.Handle, this.dxCapture.VideoDeviceFilter);
      }
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnVideoCompressorProperties"/>.
    /// Calls a <see cref="Webcam.ShowVideoCompressorConfigureDialog()"/>.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/>.</param>
    private void btnVideoCompressorProperties_Click(object sender, EventArgs e)
    {
      if (this.dxCapture.VideoCompressorFilter != null)
      {
        DirectShowUtils.DisplayPropertyPage(this.Handle, this.dxCapture.VideoCompressorFilter);
      }
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnAudioDeviceProperties"/>.
    /// Calls a <see cref="Webcam.ShowAudioDeviceConfigureDialog()"/>.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/>.</param>
    private void btnAudioDeviceProperties_Click(object sender, EventArgs e)
    {
      if (this.dxCapture.AudioDeviceFilter != null)
      {
        DirectShowUtils.DisplayPropertyPage(this.Handle, this.dxCapture.AudioDeviceFilter);
      }
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnAudioCompressorProperties"/>.
    /// Calls a <see cref="Webcam.ShowAudioCompressorConfigureDialog()"/>.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/>.</param>
    private void btnAudioCompressorProperties_Click(object sender, EventArgs e)
    {
      if (this.dxCapture.AudioCompressorFilter != null)
      {
        DirectShowUtils.DisplayPropertyPage(this.Handle, this.dxCapture.AudioCompressorFilter);
      }
    }

    /// <summary>
    /// The <see cref="ComboBox.SelectionChangeCommitted"/> event handler
    /// for the <see cref="ComboBox"/> <see cref="cbbVideoDevices"/>
    /// Updates webcams properties and preview.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/>.</param>
    private void CbbVideoDevices_SelectionChangeCommitted(object sender, EventArgs e)
    {
      this.properties.VideoInputDevice = DirectShowDevices.Instance.Cameras[cbbVideoDevices.SelectedIndex];
      this.PopulateVideoProperties(true);
      this.RebuildDXCapture(this.properties);
    }

    /// <summary>
    /// The <see cref="ComboBox.SelectionChangeCommitted"/> event handler
    /// for the <see cref="ComboBox"/> <see cref="cbbVideoCompressor"/>
    /// Updates webcams properties and preview.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/>.</param>
    private void cbbVideoCompressor_SelectionChangeCommitted(object sender, EventArgs e)
    {
      this.properties.VideoCompressor = cbbVideoCompressor.SelectedItem.ToString();
      this.RebuildDXCapture(this.properties);
    }

    /// <summary>
    /// The <see cref="ComboBox.SelectionChangeCommitted"/> event handler
    /// for the <see cref="ComboBox"/> <see cref="cbbAudioDevices"/>
    /// Updates webcams properties and preview.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/>.</param>
    private void cbbAudioDevices_SelectionChangeCommitted(object sender, EventArgs e)
    {
      string audioDevice = cbbAudioDevices.SelectedItem.ToString();
      if (audioDevice == "Disabled")
      {
        this.properties.AudioInputDevice = string.Empty;
      }
      else
      {
        this.properties.AudioInputDevice = audioDevice;
      }
      this.RebuildDXCapture(this.properties);
    }

    /// <summary>
    /// The <see cref="ComboBox.SelectionChangeCommitted"/> event handler
    /// for the <see cref="ComboBox"/> <see cref="cbbAudioCompressor"/>
    /// Updates webcams properties and preview.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/>.</param>
    private void cbbAudioCompressor_SelectionChangeCommitted(object sender, EventArgs e)
    {
      this.properties.AudioCompressor = cbbAudioCompressor.SelectedItem.ToString();
      this.RebuildDXCapture(this.properties);
    }

    /// <summary>
    /// The <see cref="ComboBox.SelectionChangeCommitted"/> event handler
    /// for the <see cref="ComboBox"/> <see cref="cbbVideoSize"/>
    /// Updates webcams video size.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/>.</param>
    private void CbbVideoSize_SelectionChangeCommitted(object sender, EventArgs e)
    {
      string[] sizes = cbbVideoSize.Text.Split('x');
      this.properties.VideoSize = new Size(Convert.ToInt32(sizes[0]), Convert.ToInt32(sizes[1]));
      this.RebuildDXCapture(this.properties);
    }

    /// <summary>
    /// The <see cref="Control.Leave"/> event handler
    /// for the <see cref="NumericUpDown"/> <see cref="nudFrameRate"/>.
    /// Updates the webcams video frame rate.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/>.</param>
    private void nudFrameRate_Leave(object sender, EventArgs e)
    {
      this.properties.FrameRate = (int)nudFrameRate.Value;
      this.RebuildDXCapture(this.properties);
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
    // Inherited methods                                                         //
    ///////////////////////////////////////////////////////////////////////////////
    #region OVERRIDES

    /// <summary>
    /// Overridden <see cref="OnCreateControl"/>.
    /// Wires the <see cref="Form.FormClosing"/> event to this control.
    /// </summary>
    protected override void OnCreateControl()
    {
      base.OnCreateControl();
      if (!this.DesignMode)
      {
        this.ParentForm.FormClosing += new FormClosingEventHandler(ParentForm_FormClosing);
      }
    }

    #endregion //OVERRIDES

    ///////////////////////////////////////////////////////////////////////////////
    // Methods for doing main class job                                          //
    ///////////////////////////////////////////////////////////////////////////////
    #region PRIVATEMETHODS

    /// <summary>
    /// This method populates the combo boxes with valid entries.
    /// </summary>
    private void InitializeUI()
    {
      //this.properties = new CaptureDeviceProperties();

      cbbVideoDevices.Items.Clear();
      cbbVideoCompressor.Items.Clear();
      cbbAudioDevices.Items.Clear();
      cbbAudioCompressor.Items.Clear();

      //cbbVideoDevices.Items.Add("Disabled");
      foreach (DsDevice filter in DsDevice.GetDevicesOfCat(FilterCategory.VideoInputDevice))
      {
        if (filter.Name != "OgamaScreenCapture Filter")
        {
          cbbVideoDevices.Items.Add(filter.Name);
        }
      }

      foreach (DsDevice filter in DsDevice.GetDevicesOfCat(FilterCategory.VideoCompressorCategory))
      {
        cbbVideoCompressor.Items.Add(filter.Name);
      }

      cbbAudioDevices.Items.Add("Disabled");
      foreach (DsDevice filter in DsDevice.GetDevicesOfCat(FilterCategory.AudioInputDevice))
      {
        cbbAudioDevices.Items.Add(filter.Name);
      }

      foreach (DsDevice filter in DsDevice.GetDevicesOfCat(FilterCategory.AudioCompressorCategory))
      {
        cbbAudioCompressor.Items.Add(filter.Name);
      }

      if (cbbVideoDevices.Items.Count > 1)
      {
        cbbVideoDevices.SelectedIndex = 1;
      }
      else
      {
        cbbVideoDevices.Text = "No video devices found";
      }

      if (cbbVideoCompressor.Items.Count > 0)
      {
        cbbVideoCompressor.SelectedIndex = 0;
        int counter = 0;
        foreach (string item in cbbVideoCompressor.Items)
        {
          if (item.Contains("Microsoft Video 1"))
          {
            cbbVideoCompressor.SelectedIndex = counter;
            break;
          }
          counter++;
        }
      }
      else
      {
        cbbVideoCompressor.Text = "No video compressors found";
      }

      if (cbbAudioDevices.Items.Count > 0)
      {
        cbbAudioDevices.SelectedIndex = 0;
      }
      else
      {
        cbbAudioDevices.Text = "No audio devices found";
      }

      if (cbbAudioCompressor.Items.Count > 0)
      {
        cbbAudioCompressor.SelectedIndex = 0;
        int counter = 0;
        foreach (string item in cbbAudioCompressor.Items)
        {
          if (item == "PCM")
          {
            cbbAudioCompressor.SelectedIndex = counter;
            break;
          }
          counter++;
        }
      }
      else
      {
        cbbAudioCompressor.Text = "No audio compressors found";
      }

      //UpdateProperties();

      //if (initializeForCapturing)
      //{
      //  if (cbbVideoDevices.Items.Count > 0)
      //  {
      //    this.PopulateVideoProperties(true);
      //  }
      //}
      //else
      //{
      //  this.PopulateDefaultProperties();
      //}

      //UpdateProperties();
    }

    /// <summary>
    /// This method fills the <see cref="CaptureDeviceProperties"/> according to the current selections.
    /// </summary>
    private void UpdateProperties()
    {
      if (cbbVideoDevices.SelectedItem != null)
      {
        this.properties.VideoInputDevice = DirectShowDevices.Instance.Cameras[cbbVideoDevices.SelectedIndex];
      }

      if (cbbAudioDevices.SelectedItem != null)
      {
        string audioDevice = cbbAudioDevices.SelectedItem.ToString();
        if (audioDevice == "Disabled")
        {
          this.properties.AudioInputDevice = string.Empty;
        }
        else
        {
          this.properties.AudioInputDevice = audioDevice;
        }
      }

      if (cbbVideoCompressor.SelectedItem != null)
      {
        this.properties.VideoCompressor = cbbVideoCompressor.SelectedItem.ToString();
      }

      if (cbbAudioCompressor.SelectedItem != null)
      {
        this.properties.AudioCompressor = cbbAudioCompressor.SelectedItem.ToString();
      }

      if (cbbVideoSize.SelectedItem != null)
      {
        string[] sizes = cbbVideoSize.SelectedItem.ToString().Split('x');
        this.properties.VideoSize = new Size(Convert.ToInt32(sizes[0]), Convert.ToInt32(sizes[1]));
      }

      this.properties.FrameRate = (int)nudFrameRate.Value;
      this.properties.PreviewWindow = this.panelPreview;
    }

    #endregion //METHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER

    /// <summary>
    /// This method populates the dialog with the current <see cref="CaptureDeviceProperties"/>
    /// </summary>
    private void PopulateDlgWithProperties()
    {
      cbbVideoDevices.Text = this.properties.VideoInputDevice.Name;
      this.PopulateVideoProperties(true);
      cbbVideoCompressor.Text = this.properties.VideoCompressor;
      cbbAudioDevices.Text = this.properties.AudioInputDevice;
      cbbAudioCompressor.Text = this.properties.AudioCompressor;
      cbbVideoSize.SelectedItem = this.properties.VideoSize.Width.ToString() + " x " +
        this.properties.VideoSize.Height.ToString();
      nudFrameRate.Value = (decimal)this.properties.FrameRate;
    }


    ///// <summary>
    ///// This method changes the display of the user control elements
    ///// according to the given <see cref="DisplayMode"/>
    ///// </summary>
    ///// <param name="mode">The <see cref="DisplayMode"/> flags that specify
    ///// what properties should be shown to the user.</param>
    //private void SwitchDisplayMode(DisplayMode mode)
    //{
    //  spcVideoAudio.Panel2Collapsed = true;
    //  if ((mode & DisplayMode.AudioPlayback) == DisplayMode.AudioPlayback ||
    //    (mode & DisplayMode.AudioRecord) == DisplayMode.AudioRecord)
    //  {
    //    spcVideoAudio.Panel2Collapsed = false;
    //    spcVideoAudio.Panel1Collapsed = true;
    //    //spcAudioPropPreview.Panel2Collapsed = true;
    //    cbbAudioDevices.Enabled = false;
    //    //cbbAudioDevices.Text = "";
    //    btnAudioDeviceProperties.Enabled = false;
    //    if ((mode & DisplayMode.AudioRecord) == DisplayMode.AudioRecord)
    //    {
    //      //spcAudioPropPreview.Panel2Collapsed = false;
    //      cbbAudioDevices.Enabled = true;
    //      btnAudioDeviceProperties.Enabled = true;
    //    }
    //  }
    //  if ((mode & DisplayMode.VideoPlayback) == DisplayMode.VideoPlayback ||
    //    (mode & DisplayMode.VideoRecord) == DisplayMode.VideoRecord)
    //  {
    //    spcVideoAudio.Panel1Collapsed = false;
    //    //spcVideoPropPreview.Panel2Collapsed = true;
    //    cbbVideoDevices.Enabled = false;
    //    //cbbVideoDevices.Text = "";
    //    btnVideoDeviceProperties.Enabled = false;
    //    if ((mode & DisplayMode.VideoRecord) == DisplayMode.VideoRecord)
    //    {
    //      //spcVideoPropPreview.Panel2Collapsed = false;
    //      cbbVideoDevices.Enabled = true;
    //      btnVideoDeviceProperties.Enabled = true;
    //    }
    //  }

    //}

    //private void PopulateDefaultProperties()
    //{
    //  // Erase old items
    //  cbbVideoSize.Items.Clear();

    //  List<Size> defaultSizes = new List<Size>();
    //  defaultSizes.Add(VIDEO_SIZE_1);
    //  defaultSizes.Add(VIDEO_SIZE_2);
    //  defaultSizes.Add(VIDEO_SIZE_3);
    //  defaultSizes.Add(VIDEO_SIZE_4);
    //  defaultSizes.Add(VIDEO_SIZE_5);

    //  foreach (Size entry in defaultSizes)
    //  {
    //    cbbVideoSize.Items.Add(entry.Width.ToString() + " x " + entry.Height.ToString());
    //  }

    //  cbbVideoSize.SelectedIndex = 0;
    //  this.properties.VideoSize = VIDEO_SIZE_1;

    //  nudFrameRate.Value = 25;
    //  this.properties.FrameRate = 25;
    //}

    /// <summary>
    /// This method populates the video property fields of the dialog
    /// with valid entries for the selected video device.
    /// </summary>
    /// <param name="selectFirstValidEntries"><strong>True</strong>
    /// if first valid entries of the combo boxes should be selected.</param>
    private void PopulateVideoProperties(Boolean selectFirstValidEntries)
    {
      // Erase old items
      cbbVideoSize.Items.Clear();

      //// Get device capabilities and fill UI
      //this.RebuildDXCapture(this.properties);

      int minFramerate = int.MaxValue;
      int maxFramerate = 0;

      this.properties.FrameRate = (int)this.dxCapture.CaptureDeviceProperties.FrameRate;
      this.properties.VideoSize = this.dxCapture.CaptureDeviceProperties.VideoSize;

      if (this.dxCapture.VideoDeviceFilter != null)
      {
        foreach (CamSizeFPS cap in this.dxCapture.CaptureDeviceProperties.VideoInputDevice.SupportedSizesAndFPS)
        {
          string size = cap.Width.ToString() + " x " + cap.Height.ToString();
          if (!cbbVideoSize.Items.Contains(size))
          {
            cbbVideoSize.Items.Add(size);
          }

          if (cap.FPS < minFramerate)
          {
            minFramerate = (int)cap.FPS;
          }

          if (cap.FPS > maxFramerate)
          {
            maxFramerate = (int)cap.FPS;
          }
        }

        if (minFramerate <= maxFramerate)
        {
          nudFrameRate.Minimum = minFramerate;
          nudFrameRate.Maximum = maxFramerate;
          nudFrameRate.Value = nudFrameRate.Maximum;
        }
      }

      if (selectFirstValidEntries)
      {
        this.properties.FrameRate = (int)nudFrameRate.Value;
      }

      if (cbbVideoSize.Items.Count > 0)
      {
        cbbVideoSize.SelectedIndex = 0;
        string[] sizes = cbbVideoSize.Text.Split('x');
        if (selectFirstValidEntries)
        {
          this.properties.VideoSize = new Size(Convert.ToInt32(sizes[0]), Convert.ToInt32(sizes[1]));
        }
      }
    }

    private void RebuildDXCapture(CaptureDeviceProperties captureProperties)
    {
      if (this.dxCapture != null)
      {
        this.dxCapture.Dispose();
      }

      if (!this.DesignMode)//this.shouldPreview && 
      {
        captureProperties.PreviewWindow = this.panelPreview;
      }
      else
      {
        captureProperties.PreviewWindow = null;
      }

      this.dxCapture = new DXCapture(captureProperties);
      this.dxCapture.ShowPreviewWindow();
      //this.dxCapture.Start();
    }

    #endregion //HELPER
  }
}
