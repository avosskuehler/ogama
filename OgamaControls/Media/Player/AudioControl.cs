using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using VectorGraphics.Tools;

namespace OgamaControls
{
  /// <summary>
  /// This <see cref="UserControl"/> can be used to define a <see cref="Pen"/>
  /// and <see cref="Brush"/> that are visualized in two preview 
  /// frames. Additionally the user selects, if edge or fill or both should be used.
  /// </summary>
  public partial class AudioControl : UserControl
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
    /// This member saves the <see cref="AudioFile"/> that this control is bound to.
    /// </summary>
    private AudioFile audioFile;

    /// <summary>
    /// The preview player for the audio file.
    /// </summary>
    private AudioPlayer player;

    /// <summary>
    /// A path where to put the audio file if successfully loaded.
    /// </summary>
    private string pathToCopyTo;

    /// <summary>
    /// This event is fired, whenever the controls content has 
    /// changed.
    /// </summary>
    public event EventHandler<AudioPropertiesChangedEventArgs> AudioPropertiesChanged;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES


    /// <summary>
    /// Gets or sets the audio file defined in this control.
    /// </summary>
    /// <value>The newly defined audio file.</value>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public AudioFile Sound
    {
      get { return this.audioFile; }
      set
      {
        this.audioFile = value;
        UpdateUIWithAudioFile();
      }
    }

    /// <summary>
    /// Gets or sets a value indicating wheter the ShouldPlay checkbox
    /// should be checked or not.
    /// </summary>
    [Category("Appearance")]
    [Description("The ShouldPlay checkbox should be checked or not.")]
    public bool ShouldPlay
    {
      get { return this.audioFile.ShouldPlay; }
      set 
      { 
        this.audioFile.ShouldPlay = value;
        this.chbPlaySound.Checked = value;
      }
    }

    /// <summary>
    /// A path where to put the audio file if successfully loaded.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public string PathToCopyTo
    {
      get { return this.pathToCopyTo; }
      set { this.pathToCopyTo = value; }
    }

    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Constructor.
    /// </summary>
    public AudioControl()
    {
      InitializeComponent();
      this.pathToCopyTo = "";
      this.audioFile = new AudioFile();
      this.audioFile.Loop = false;
      this.audioFile.ShouldPlay = true;
      this.audioFile.ShowOnClick = false;
      this.player = new AudioPlayer();
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler                                                              //
    ///////////////////////////////////////////////////////////////////////////////
    #region EVENTS

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler for UI, Menu, Buttons, Toolbars etc.                         //
    ///////////////////////////////////////////////////////////////////////////////
    #region WINDOWSEVENTHANDLER

    /// <summary>
    /// <see cref="Control.Click"/> event handler 
    /// for the <see cref="Button"/> <see cref="btnOpenFile"/>
    /// Raises a <see cref="OpenFileDialog"/>.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A empty <see cref="EventArgs"/></param>
    private void btnOpenFile_Click(object sender, EventArgs e)
    {
      ofdAudioFiles.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);
      if (ofdAudioFiles.ShowDialog() == DialogResult.OK)
      {
        string fileName = ofdAudioFiles.FileName;
        if (!File.Exists(fileName))
        {
          //Erase textbox entry
          txbFilename.Text = "";
          return;
        }

        string target = fileName;
        if (pathToCopyTo != string.Empty)
        {
          string templatePath = pathToCopyTo;
          if (Path.GetDirectoryName(fileName) != templatePath)
          {
            target = Path.Combine(templatePath, Path.GetFileName(fileName));
            if (!File.Exists(target))
            {
              File.Copy(fileName, target);
              MessageBox.Show("A copy of this music file is saved to the following resources folder of the current project :" +
                Environment.NewLine + target + Environment.NewLine +
                "This is done because of easy movement of experiments between different computers or locations.",
                Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
          }
        }

        this.audioFile.Filename = Path.GetFileName(fileName);
        this.audioFile.Filepath = pathToCopyTo;
        this.txbFilename.Text = this.audioFile.Filename;
        this.chbPlaySound.Checked = true;
        this.audioFile.ShouldPlay = true;
        this.RaiseEvent();
      }
    }

    /// <summary>
    /// <see cref="Control.Click"/> event handler 
    /// for the <see cref="Button"/> <see cref="btnDeleteFile"/>
    /// Empties the filename <see cref="TextBox"/>.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A empty <see cref="EventArgs"/></param>
    private void btnDeleteFile_Click(object sender, EventArgs e)
    {
      this.audioFile.Filename = "";
      this.audioFile.Filepath = "";
      this.audioFile.ShouldPlay = false;
      this.txbFilename.Text = "";
      this.chbPlaySound.Checked = false;
    }

    /// <summary>
    /// <see cref="CheckBox.CheckedChanged"/> event handler for the
    /// <see cref="CheckBox"/> <see cref="chbPlaySound"/>.
    /// Raises the <see cref="AudioPropertiesChanged"/> event.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void chbPlaySound_CheckedChanged(object sender, EventArgs e)
    {
      if (this.audioFile == null)
      {
        this.audioFile = new AudioFile();
      }
      this.audioFile.ShouldPlay = chbPlaySound.Checked;
      chbLoop.Enabled = chbPlaySound.Checked;
      rdbOnAppearance.Enabled = chbPlaySound.Checked;
      rdbOnClick.Enabled = chbPlaySound.Checked;
      btnPreviewPlay.Enabled = chbPlaySound.Checked;
      btnPreviewStop.Enabled = chbPlaySound.Checked;
      this.RaiseEvent();
    }

    /// <summary>
    /// <see cref="CheckBox.CheckedChanged"/> event handler for the
    /// <see cref="CheckBox"/> <see cref="chbLoop"/>.
    /// Raises the <see cref="AudioPropertiesChanged"/> event.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void chbLoop_CheckedChanged(object sender, EventArgs e)
    {
      this.audioFile.Loop = chbLoop.Checked;
      this.RaiseEvent();
    }

    /// <summary>
    /// <see cref="RadioButton.CheckedChanged"/> event handler for the
    /// <see cref="RadioButton"/> <see cref="rdbOnAppearance"/>.
    /// Raises the <see cref="AudioPropertiesChanged"/> event.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void rdbOnAppearance_CheckedChanged(object sender, EventArgs e)
    {
      this.audioFile.ShowOnClick = !rdbOnAppearance.Checked;
      this.RaiseEvent();
    }

    /// <summary>
    /// <see cref="RadioButton.CheckedChanged"/> event handler for the
    /// <see cref="RadioButton"/> <see cref="rdbOnAppearance"/>.
    /// Raises the <see cref="AudioPropertiesChanged"/> event.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void rdbOnClick_CheckedChanged(object sender, EventArgs e)
    {
      this.audioFile.ShowOnClick = rdbOnClick.Checked;
      this.RaiseEvent();
    }

    /// <summary>
    /// <see cref="Control.Click"/> event handler 
    /// for the <see cref="Button"/> <see cref="btnPreviewPlay"/>
    /// Tries to play the audio file.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A empty <see cref="EventArgs"/></param>
    private void btnPreviewPlay_Click(object sender, EventArgs e)
    {
      if (File.Exists(this.audioFile.FullFilename))
      {
        player.LoadAudioFile(this.audioFile.FullFilename);
        player.Play();
      }
    }

    /// <summary>
    /// <see cref="Control.Click"/> event handler 
    /// for the <see cref="Button"/> <see cref="btnPreviewStop"/>
    /// Empties the filename <see cref="TextBox"/>.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A empty <see cref="EventArgs"/></param>
    private void btnPreviewStop_Click(object sender, EventArgs e)
    {
      if (player.PlayState==PlayState.Running)
      {
        player.Stop();
      }

    }

    #endregion //WINDOWSEVENTHANDLER

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler for Custom Defined Events                                    //
    ///////////////////////////////////////////////////////////////////////////////
    #region CUSTOMEVENTHANDLER

    /// <summary>
    /// The protected OnAudioPropertiesChanged method raises the event by invoking 
    /// the delegates
    /// </summary>
    /// <param name="e">A <see cref="AudioPropertiesChangedEventArgs"/> with the event arguments</param>
    protected virtual void OnAudioPropertiesChanged(AudioPropertiesChangedEventArgs e)
    {
      if (AudioPropertiesChanged != null)
      {
        // Invokes the delegates. 
        AudioPropertiesChanged(this, e);
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
    /// This helper method raises the <see cref="AudioPropertiesChanged"/>
    /// event.
    /// </summary>
    private void RaiseEvent()
    {
      this.OnAudioPropertiesChanged(new AudioPropertiesChangedEventArgs(this.audioFile));
    }

    /// <summary>
    /// This method fills the UI with the current audio file.
    /// </summary>
    private void UpdateUIWithAudioFile()
    {
      if (this.audioFile==null)
      {
        return;
      }

      if (this.audioFile.Filename != string.Empty)
      {
        this.txbFilename.Text = this.audioFile.Filename;
      }

      this.chbLoop.Checked = this.audioFile.Loop;
      this.chbPlaySound.Checked = this.audioFile.ShouldPlay;
      this.rdbOnClick.Checked = this.audioFile.ShowOnClick;
      this.rdbOnAppearance.Checked = !this.audioFile.ShowOnClick;
    }

    #endregion //HELPER



  }

}
