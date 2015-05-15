// <copyright file="aslSettingsDialog.cs" company="FU Berlin">
// ******************************************************
// OGAMA - open gaze and mouse analyzer 
// Copyright (C) 2015 Dr. Adrian Voßkühler  
// ------------------------------------------------------------------------
// This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
// This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
// You should have received a copy of the GNU General Public License along with this program; if not, write to the Free Software Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
// **************************************************************
// </copyright>
// <author>
//  University Toulouse 2 - CLLE-LTC UMR5263
//  Yves LECOURT
//  Modifications : Smaïl KHAMED
//  </author>
// <email>virginie.feraud@univ-tlse2.fr</email>

namespace Ogama.Modules.Recording.ASLInterface
{
  using System;
  using System.Collections;
  using System.Drawing;
  using System.IO;
  using System.Runtime.InteropServices;
  using System.Windows.Forms;

  using ASLSERIALOUTLIB2Lib;

  /// <summary>
  /// A Popup <see cref="Form"/> to specify settings for the ASL System movements.
  /// </summary>
  public partial class aslSettingsDialog : Form
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
    /// The current ASL <see cref="UserSettings"/>
    /// </summary>
    private UserSettings userSettings;

    /// <summary>
    /// The connection to the dll.
    /// </summary>
    private ASLSerialOutPort3Class serialOutClass;

    /// <summary>
    /// Indicates the connection state.
    /// </summary>
    private bool isConnected;

    /// <summary>
    /// Indicates modifications.
    /// </summary>
    private bool somethingChange;

    /// <summary>
    /// Saves the new data records.
    /// </summary>
    private ArrayList records;

    /// <summary>
    /// Contains the filte to write to
    /// </summary>
    private StreamWriter logFile;

    /// <summary>
    /// This is the tracker.
    /// </summary>
    private AslTracker aslTracker;

    /// <summary>
    /// Indicates the status of the connection.
    /// </summary>
    private bool continuousModeStarted;

    /// <summary>
    /// Array of possible values of the eye camera update rate
    /// <value>An <see cref="int"/> which can be 50, 60 (default), 120, 240 or 360 Hertz</value>
    /// </summary>
    private int[] eyeCameraSpeedArray = { 50, 60, 120, 240, 360 };

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the aslSettingsDialog class.
    /// </summary>
    /// <param name="tracker">The <see cref="AslTracker"/> to be used</param>
    /// <param name="aslSerialPort">The connection to the dll.</param>
    public aslSettingsDialog(AslTracker tracker, ASLSerialOutPort3Class aslSerialPort)
    {
      this.records = new ArrayList();

      // call the Windows Form Designer generated method
      this.InitializeComponent();

      this.aslTracker = tracker;
      this.userSettings = this.aslTracker.Settings;

      this.serialOutClass = aslSerialPort;

      // call the additional local initialize method 
      this.CustomInitialize();
      this.InitializeControls();

      this.somethingChange = false;
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
    /// Gets or sets the <see cref="UserSettings"/> class.
    /// <value>A <see cref="UserSettings"/> with the current settings.</value>
    /// </summary>
    public UserSettings AslSettings
    {
      get
      {
        return this.userSettings;
      }

      set
      {
        this.userSettings = value;
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
    #endregion //OVERRIDES

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler                                                              //
    ///////////////////////////////////////////////////////////////////////////////
    #region EVENTHANDLER

    /// <summary>
    /// The on Load event handler for the dialog.
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void AslSettingsDialog_Load(object sender, EventArgs e)
    {
 #if ASL
     this.userSettings = UserSettings.Load(this.aslTracker.SettingsFile);
#endif 
      this.InitializeControls();

      // Initialize interface
      this.serialOutClass.Notify += new _IASLSerialOutPort2Events_NotifyEventHandler(this.OnNotify);
      this.EnableDisable();
      this.StreamingCheckbox_CheckedChanged(sender, e);
      this.somethingChange = false;
    }

    /// <summary>
    /// The resize event handler for the dialog. Updates the size if the list view.
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void AslSettingsDialog_Resize(object sender, EventArgs e)
    {
      this.lvLog.Size = new Size(Size.Width - 11, Size.Height - 229);
    }

    /// <summary>
    /// The closing event handler for the dialog which checks for changes.
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void AslSettingsDialog_FormClosing(object sender, FormClosingEventArgs e)
    {
      if (this.somethingChange)
      {
        if (MessageBox.Show("Save new settings ?", "Settings change", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            == DialogResult.Yes)
        {
          if (!File.Exists(this.txtConfigFile.Text.ToString()))
          {
            MessageBox.Show(
              "Specified file do not exist !",
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            this.txtConfigFile.Text = this.userSettings.DefaultConfigFile;
          }

          this.SaveControls();
          this.aslTracker.Settings = this.userSettings;

          if (this.aslTracker.IsConnected)
          {
            this.aslTracker.CloseComPort();
            this.aslTracker.CleanUp();
          }
        }
      }

      this.btnDisconnect_Click(sender, e);
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler
    /// for the <see cref="Button"/> <see cref="btnConnect"/>
    /// Connects the tracker.
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnConnect_Click(object sender, EventArgs e)
    {
      this.SaveControls();

      if (this.aslTracker.IsConnected)
      {
        this.aslTracker.CloseComPort();
        this.aslTracker.CleanUp();
      }

      int baudRate, updateRate, itemCount = 0;
      bool streamingMode;
      Array itemNames = null;

      try
      {
        if (File.Exists(this.txtConfigFile.Text.ToString()))
        {
          this.serialOutClass.Connect(
            this.txtConfigFile.Text.ToString(),
              this.userSettings.ComPortNo,
              this.userSettings.EyeHead,
              out baudRate,
              out updateRate,
              out streamingMode,
              out itemCount,
              out itemNames);

          this.cbPort.SelectedIndex = this.userSettings.ComPortNo - 1;
          this.StreamingCheckbox.Checked = streamingMode;
          this.bEyeHead.Checked = this.userSettings.EyeHead;
          this.txtConfigFile.Text = this.userSettings.ConfigFile;
          this.btnDisconnect.Enabled = true;
        }
        else
        {
          MessageBox.Show(
            "Eye tracker configuration file does not exist Use browse button to locate the file.",
              "Error",
              MessageBoxButtons.OK,
              MessageBoxIcon.Error);
          this.txtConfigFile.Text = this.userSettings.DefaultConfigFile;
          return;
        }
      }
      catch (COMException)
      {
        string errorDesc;
        this.serialOutClass.GetLastError(out errorDesc);
        string msg = "Error connecting to serial port:\n" + errorDesc;
        MessageBox.Show(msg, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }

      // Setup columns to display messages
      this.lvLog.Columns.Clear();
      for (int i = 0; i < itemCount; i++)
      {
        string itemName = itemNames.GetValue(i).ToString();
        this.lvLog.Columns.Add(itemName);
        if (this.logFile != null)
        {
          this.logFile.Write(itemName);
          this.logFile.Write(",");
        }
      }

      // Set column width to fit header text
      foreach (ColumnHeader header in this.lvLog.Columns)
      {
        header.Width = -2;
      }

      // Open log file and setup file headers
      if (this.userSettings.WriteLogFile)
      {
        this.logFile = new StreamWriter(this.userSettings.LogFile);
        for (int i = 0; i < itemCount; i++)
        {
          string itemName = itemNames.GetValue(i).ToString();
          this.logFile.Write(itemName);
          this.logFile.Write(",");
        }

        this.logFile.WriteLine();
      }

      this.isConnected = true;
      this.EnableDisable();
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler
    /// for the <see cref="Button"/> <see cref="btnDisconnect"/>
    /// Disconnects the tracker.
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnDisconnect_Click(object sender, EventArgs e)
    {
      try
      {
        if (this.continuousModeStarted)
        {
          this.btnStartStopContinuous_Click(sender, e);
        }

        this.serialOutClass.Disconnect();
      }
      catch (COMException)
      {
        string errorDesc;
        this.serialOutClass.GetLastError(out errorDesc);
        string msg = "Error disconnecting from serial port:\n" + errorDesc;
        MessageBox.Show(msg, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }

      if (this.logFile != null)
      {
        this.logFile.Close();
        this.logFile = null;
      }

      this.lvLog.Items.Clear();
      this.lvLog.Columns.Clear();
      this.records.Clear();
      this.isConnected = false;
      this.EnableDisable();
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler
    /// for the <see cref="Button"/> <see cref="btnGetRecord"/>
    /// Tries to get a data record from the tracker.
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnGetRecord_Click(object sender, EventArgs e)
    {
      Array dataItems;
      int count;
      bool available;

      try
      {
        this.serialOutClass.GetScaledData(out dataItems, out count, out available);
      }
      catch (COMException)
      {
        return;
      }

      if (available == false)
      {
        return;
      }

      this.DisplayRecord(dataItems);
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler
    /// for the <see cref="Button"/> <see cref="btnStartStopContinuous"/>
    /// Starts or stops the continuous data mode of the tracker.
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnStartStopContinuous_Click(object sender, EventArgs e)
    {
      if (!this.continuousModeStarted)
      {
        this.serialOutClass.StartContinuousMode();
        this.timerUpdate.Enabled = true;
        this.continuousModeStarted = true;
        this.btnStartStopContinuous.Text = "Stop Continuous Read";
      }
      else
      {
        this.serialOutClass.StopContinuousMode();
        this.timerUpdate.Enabled = false;
        this.timerUpdate_Tick(sender, e); // display remaining records
        this.continuousModeStarted = false;
        this.btnStartStopContinuous.Text = "Start Continuous Read";
      }
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler
    /// for the <see cref="Button"/> <see cref="btnRestoreDefaults"/>
    /// Restores the default asl settings.
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnRestoreDefaults_Click(object sender, EventArgs e)
    {
      this.userSettings.ConfigFile = this.userSettings.DefaultConfigFile;
      this.InitializeControls();
      this.EnableDisable();
      this.somethingChange = true;
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler
    /// for the <see cref="Button"/> <see cref="btnBrowseConfig"/>
    /// Gets the configuration file location.
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnBrowseConfig_Click(object sender, EventArgs e)
    {
      OpenFileDialog dlg = new OpenFileDialog();
      dlg.FileName = this.txtConfigFile.Text;
      dlg.Filter = "Eye Tracker Configuration Files (*.cfg;*.xml)|*.cfg;*.xml";
      dlg.CheckFileExists = true;
      dlg.Multiselect = false;
      if (dlg.ShowDialog() == DialogResult.OK)
      {
        this.txtConfigFile.Text = dlg.FileName;
        this.somethingChange = true;
      }
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler
    /// for the <see cref="Button"/> <see cref="btnBrowseLog"/>
    /// Browses a log file for the data.
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnBrowseLog_Click(object sender, EventArgs e)
    {
      SaveFileDialog dlg = new SaveFileDialog();
      dlg.FileName = this.txtLogFile.Text;
      dlg.Filter = "CSV files|*.csv|All files|*.*";
      dlg.OverwritePrompt = true;
      dlg.AddExtension = true;
      if (dlg.ShowDialog() == DialogResult.OK)
      {
        this.txtLogFile.Text = dlg.FileName;
        this.somethingChange = true;
      }
    }

    /// <summary>
    /// The <see cref="Timer.Tick"/> event handler
    /// for the <see cref="Timer"/> <see cref="timerUpdate"/>
    /// Updates new data samples.
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void timerUpdate_Tick(object sender, EventArgs e)
    {
      // Display all records that have been received since previous tick
      lock (this)
      {
        for (int i = 0; i < this.records.Count; i++)
        {
          Array dataItems = (Array)this.records[i];
          this.DisplayRecord(dataItems);
        }

        this.records.Clear();
      }
    }

    /// <summary>
    /// The <see cref="CheckBox.CheckedChanged"/> event handler
    /// for the <see cref="CheckBox"/> <see cref="StreamingCheckbox"/>
    /// Updates the streaming mode.
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void StreamingCheckbox_CheckedChanged(object sender, EventArgs e)
    {
      if (this.StreamingCheckbox.Checked)
      {
        this.LabelErrorStreamingMode.Hide();
      }
      else
      {
        this.LabelErrorStreamingMode.Show();
      }
    }

    /// <summary>
    /// The <see cref="CheckBox.CheckedChanged"/> event handler
    /// for the logfile checkbox. 
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void CheckedChanged(object sender, EventArgs e)
    {
      this.EnableDisable();
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
    /// Sets up Asl system settings
    /// </summary>
    private void CustomInitialize()
    {
      this.Text = "Select and test ASL configuration file";
      this.EnableDisable();
      this.btnDisconnect.Enabled = false;
      this.continuousModeStarted = false;
      this.somethingChange = false;
    } // end of initialize()

    /// <summary>
    /// Initializes the form controls with default settings.
    /// </summary>
    private void InitializeControls()
    {
      this.cbPort.SelectedIndex = this.userSettings.ComPortNo - 1;
      this.StreamingCheckbox.Checked = this.userSettings.Streaming;
      this.bEyeHead.Checked = this.userSettings.EyeHead;
      this.txtConfigFile.Text = this.userSettings.ConfigFile;
      this.bWriteLogFile.Checked = this.userSettings.WriteLogFile;
      this.txtLogFile.Text = this.userSettings.LogFile;
    }

    /// <summary>
    /// Saves the forms control settings to the settings file.
    /// </summary>
    private void SaveControls()
    {
      this.userSettings.ComPortNo = this.cbPort.SelectedIndex + 1;
      this.userSettings.Streaming = this.StreamingCheckbox.Checked;
      this.userSettings.EyeHead = this.bEyeHead.Checked;
      this.userSettings.ConfigFile = this.txtConfigFile.Text;
      this.userSettings.WriteLogFile = this.bWriteLogFile.Checked;
      this.userSettings.LogFile = this.txtLogFile.Text;

 #if ASL
     this.userSettings.Store();
#endif
    }

    /// <summary>
    /// Updates enabled state of forms controls depended on tracking state.
    /// </summary>
    private void EnableDisable()
    {
      bool disconnected = !this.isConnected;
      bool continuous = this.timerUpdate.Enabled;

      // disable user interface items that cannot be changed
      // in current state or not relevant
      // all user entries should be disabled when connected
      this.cbPort.Enabled = disconnected;
      this.bEyeHead.Enabled = disconnected;
      this.txtConfigFile.Enabled = disconnected;
      this.bWriteLogFile.Enabled = disconnected;
      this.txtLogFile.Enabled = disconnected && this.bWriteLogFile.Checked;
      this.btnBrowseConfig.Enabled = disconnected;
      this.btnBrowseLog.Enabled = disconnected && this.bWriteLogFile.Checked;

      // disable commands depending on the state
      this.btnConnect.Enabled = disconnected;
      this.btnStartStopContinuous.Enabled = this.isConnected && (!this.continuousModeStarted) && this.StreamingCheckbox.Checked;
      this.btnGetRecord.Enabled = this.isConnected && (!this.continuousModeStarted);
      this.btnRestoreDefaults.Enabled = disconnected;
    }

    /// <summary>
    /// Event handler for new data.
    /// </summary>
    private void OnNotify()
    {
      Array dataItems;
      if (this.GetRecord(out dataItems))
      {
        this.records.Add(dataItems);
      }
    }

    /// <summary>
    /// Checks for new data samples
    /// </summary>
    /// <param name="dataItems">Out. Returns the newly samples data.</param>
    /// <returns>True if successful, otherwise false.</returns>
    private bool GetRecord(out Array dataItems)
    {
      int count;
      bool available;
      try
      {
        this.serialOutClass.GetScaledData(out dataItems, out count, out available);
      }
      catch (COMException)
      {
        dataItems = null;
        return false;
      }

      return available;
    }

    /// <summary>
    /// Updates the listview with the given list of samples.
    /// </summary>
    /// <param name="dataItems">An <see cref="Array"/> with the samples to be displayed.</param>
    private void DisplayRecord(Array dataItems)
    {
      if (this.lvLog.Items.Count > 1000)
      {
        this.lvLog.Items.Clear();
      }

      ListViewItem listItem = new ListViewItem();
      listItem.SubItems[0].Text = dataItems.GetValue(0).ToString();
      for (int j = 1; j < dataItems.Length; j++)
      {
        string itemStr = dataItems.GetValue(j).ToString();
        listItem.SubItems.Add(itemStr);
        if (this.logFile != null)
        {
          this.logFile.Write(itemStr);
          this.logFile.Write(",");
        }
      }

      this.lvLog.Items.Add(listItem);
      this.lvLog.EnsureVisible(this.lvLog.Items.Count - 1);
      if (this.logFile != null)
      {
        this.logFile.WriteLine();
      }
    }

    /// <summary>
    /// Method which searchs the index position of one particular int in an array.
    /// </summary>
    /// <param name="tab">Array of int.</param>
    /// <param name="item">The int to search.</param>
    /// <returns>The position of the item int the aray if exists, 
    /// otherwise <strong>-1</strong></returns>
    private int getIndex(int[] tab, int item)
    {
      int index = -1;
      for (int i = 0; i < tab.Length; i++)
      {
        if (tab[i] == item)
        {
          index = i;
          i = tab.Length;
        }
      }

      return index;
    }

    #endregion //PRIVATEMETHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER
    #endregion //HELPER
  }
}