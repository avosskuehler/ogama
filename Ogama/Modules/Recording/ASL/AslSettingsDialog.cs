// <copyright file="aslSettingsDialog.cs" company="FU Berlin">
// ******************************************************
// OGAMA - open gaze and mouse analyzer 
// Copyright (C) 2010 Adrian Voßkühler  
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

namespace Ogama.Modules.Recording.ASL
{
  using System;
  using System.Collections.Generic;
  using System.Collections;
  using System.ComponentModel;
  using System.Windows.Forms;
  using Ogama.ExceptionHandling;
  using Ogama.Modules.Common;
  using System.Data;
  using System.Drawing;
  using System.Text;
  using System.IO;
  using System.Runtime.InteropServices;

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
    UserSettings userSettings;

    ASLSerialOutPort3Class m_serialOut;
    bool m_connected = false;
    bool somethingChange = false;
    ArrayList m_records = new ArrayList();
    StreamWriter m_file = null;
    AslTracker aslTracker;
    bool continuousModeStarted;

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
    public aslSettingsDialog(AslTracker tracker, ASLSerialOutPort3Class aslSerialPort)
    {
      // call the Windows Form Designer generated method
      this.InitializeComponent();

      this.aslTracker = tracker;
      this.userSettings = this.aslTracker.Settings;
      this.m_serialOut = aslSerialPort;

      // call the additional local initialize method 
      this.initialize();
      this.InitializeControls();

      this.somethingChange = false;
    }

    #endregion //CONSTRUCTION
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
    // Eventhandler                                                              //
    ///////////////////////////////////////////////////////////////////////////////
    #region EVENTS

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler for UI, Menu, Buttons, Toolbars etc.                         //
    ///////////////////////////////////////////////////////////////////////////////
    #region WINDOWSEVENTHANDLER

    private void Form1_Load(object sender, EventArgs e)
    {
      this.userSettings = UserSettings.Load(this.aslTracker.SettingsFile);
      this.InitializeControls();

      // Initialize interface
      this.m_serialOut.Notify += new _IASLSerialOutPort2Events_NotifyEventHandler(OnNotify);
      this.EnableDisable();
      this.bStreaming_CheckedChanged(sender, e);
      this.somethingChange = false;
    }

    private void Form1_Resize(object sender, EventArgs e)
    {
      this.lvLog.Size = new Size(Size.Width - 11, Size.Height - 229);
    }

    private void Form1_FormClosing(object sender, FormClosingEventArgs e)
    {
      if (this.somethingChange)
      {
        if (MessageBox.Show("Save new settings ?", "Settings change", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            == DialogResult.Yes)
        {
          if (!File.Exists(this.txtConfigFile.Text.ToString()))
          {
            MessageBox.Show("Specified file do not exist !",
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            this.txtConfigFile.Text = this.userSettings.defaultConfigFile;
          }

          this.SaveControls();
          this.aslTracker.Settings = this.userSettings;

          //MessageBox.Show("Config save");                
          if (aslTracker.IsConnected)
          {
            aslTracker.CloseComPort();
            aslTracker.CleanUp();
          }

        }
      }

      this.btnDisconnect_Click(sender, e);
    }

    private void btnConnect_Click(object sender, EventArgs e)
    {
      this.m_serialOut.Notify += new _IASLSerialOutPort2Events_NotifyEventHandler(OnNotify);
      this.SaveControls();

      if (aslTracker.IsConnected)
      {
        aslTracker.CloseComPort();
        aslTracker.CleanUp();
      }

      int baudRate, updateRate, itemCount = 0;
      bool streamingMode;
      Array itemNames = null;

      try
      {
        if (File.Exists(this.txtConfigFile.Text.ToString()))
        {
          this.m_serialOut.Connect(this.txtConfigFile.Text.ToString(),
              this.userSettings.comPortNo,
              this.userSettings.eyeHead,
              out baudRate,
              out updateRate,
              out streamingMode,
              out itemCount,
              out itemNames);

          this.cbPort.SelectedIndex = this.userSettings.comPortNo - 1;
          this.bStreaming.Checked = streamingMode;
          this.bEyeHead.Checked = this.userSettings.eyeHead;
          this.txtConfigFile.Text = this.userSettings.configFile;
          this.btnDisconnect.Enabled = true;
        }
        else
        {
          MessageBox.Show("The specified file do not exist !",
              "Error",
              MessageBoxButtons.OK,
              MessageBoxIcon.Error);
          this.txtConfigFile.Text = this.userSettings.defaultConfigFile;
          return;
        }

      }
      catch (COMException)
      {
        string errorDesc;
        this.m_serialOut.GetLastError(out errorDesc);
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
        if (this.m_file != null)
        {
          this.m_file.Write(itemName);
          this.m_file.Write(",");
        }

      }
      // Set column width to fit header text
      foreach (ColumnHeader header in this.lvLog.Columns)
        header.Width = -2;

      // Open log file and setup file headers
      if (userSettings.writeLogFile)
      {
        m_file = new StreamWriter(userSettings.logFile);
        for (int i = 0; i < itemCount; i++)
        {
          string itemName = itemNames.GetValue(i).ToString();
          m_file.Write(itemName);
          m_file.Write(",");
        }
        m_file.WriteLine();
      }

      m_connected = true;
      EnableDisable();
    }

    private void btnDisconnect_Click(object sender, EventArgs e)
    {
      try
      {
        if (this.continuousModeStarted)
          this.btnStartStopContinuous_Click(sender, e);
        m_serialOut.Disconnect();
      }
      catch (COMException)
      {
        string errorDesc;
        m_serialOut.GetLastError(out errorDesc);
        string msg = "Error disconnecting from serial port:\n" + errorDesc;
        MessageBox.Show(msg, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }

      if (m_file != null)
      {
        m_file.Close();
        m_file = null;
      }

      lvLog.Items.Clear();
      lvLog.Columns.Clear();
      m_records.Clear();
      m_connected = false;
      EnableDisable();
      this.m_serialOut.Notify -= new _IASLSerialOutPort2Events_NotifyEventHandler(OnNotify);
    }

    private void btnGetRecord_Click(object sender, EventArgs e)
    {
      Array dataItems;
      int count;
      bool available;

      try
      {
        m_serialOut.GetScaledData(out dataItems, out count, out available);
      }
      catch (COMException)
      {
        return;
      }

      if (available == false)
        return;

      DisplayRecord(dataItems);
    }

    private void btnStartStopContinuous_Click(object sender, EventArgs e)
    {
      if (!continuousModeStarted)
      {
        m_serialOut.StartContinuousMode();
        timerUpdate.Enabled = true;
        continuousModeStarted = true;
        this.btnStartStopContinuous.Text = "Stop Continuous Read";
      }
      else
      {
        m_serialOut.StopContinuousMode();
        timerUpdate.Enabled = false;
        timerUpdate_Tick(sender, e); // display remaining records
        continuousModeStarted = false;
        this.btnStartStopContinuous.Text = "Start Continuous Read";
      }
    }

    private void btnRestoreDefaults_Click(object sender, EventArgs e)
    {
      this.userSettings.configFile = this.userSettings.defaultConfigFile;
      InitializeControls();
      EnableDisable();
      this.somethingChange = true;
    }

    private void btnBrowseConfig_Click(object sender, EventArgs e)
    {
      OpenFileDialog dlg = new OpenFileDialog();
      dlg.FileName = txtConfigFile.Text;
      dlg.Filter = "Eye Tracker Configuration Files (*.cfg;*.xml)|*.cfg;*.xml";
      dlg.CheckFileExists = true;
      dlg.Multiselect = false;
      if (dlg.ShowDialog() == DialogResult.OK)
      {
        //this.userSettings.configFile = dlg.FileName;
        txtConfigFile.Text = dlg.FileName;
        this.somethingChange = true;
      }
    }

    private void btnBrowseLog_Click(object sender, EventArgs e)
    {
      SaveFileDialog dlg = new SaveFileDialog();
      dlg.FileName = txtLogFile.Text;
      dlg.Filter = "CSV files|*.csv|All files|*.*";
      dlg.OverwritePrompt = true;
      dlg.AddExtension = true;
      if (dlg.ShowDialog() == DialogResult.OK)
      {
        txtLogFile.Text = dlg.FileName;
        this.somethingChange = true;
      }
    }

    private void CheckedChanged(object sender, EventArgs e)
    {
      EnableDisable();
    }

    private void timerUpdate_Tick(object sender, EventArgs e)
    {
      // Display all records that have been received since previous tick
      lock (this)
      {
        for (int i = 0; i < m_records.Count; i++)
        {
          Array dataItems = (Array)m_records[i];
          DisplayRecord(dataItems);
        }
        m_records.Clear();
      }
    }

    private void cbWarning_CheckedChanged(object sender, EventArgs e)
    {
      this.somethingChange = true;
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
    #endregion //OVERRIDES

    ///////////////////////////////////////////////////////////////////////////////
    // Methods for doing main class job                                          //
    ///////////////////////////////////////////////////////////////////////////////
    #region METHODS

    /// <summary>
    /// Sets up Asl system settings
    /// </summary>
    protected void initialize()
    {
      this.Text = "Select and test ASL configuration file";
      this.EnableDisable();
      this.gbReadOptions.Enabled = false;
      this.btnDisconnect.Enabled = false;
      this.continuousModeStarted = false;
      this.somethingChange = false;
    } // end of initialize()

    private void InitializeControls()
    {
      this.cbPort.SelectedIndex = this.userSettings.comPortNo - 1;
      this.bStreaming.Checked = this.userSettings.streaming;
      this.bEyeHead.Checked = this.userSettings.eyeHead;
      this.txtConfigFile.Text = this.userSettings.configFile;
      this.bWriteLogFile.Checked = this.userSettings.writeLogFile;
      this.txtLogFile.Text = this.userSettings.logFile;
      this.cbWarning.Checked = this.userSettings.displayWarning;
    }

    private void SaveControls()
    {
      this.userSettings.comPortNo = this.cbPort.SelectedIndex + 1;
      this.userSettings.streaming = this.bStreaming.Checked;
      this.userSettings.eyeHead = this.bEyeHead.Checked;
      this.userSettings.configFile = this.txtConfigFile.Text;
      this.userSettings.writeLogFile = this.bWriteLogFile.Checked;
      this.userSettings.logFile = this.txtLogFile.Text;
      this.userSettings.displayWarning = this.cbWarning.Checked;

      this.userSettings.Store();
    }

    private void EnableDisable()
    {
      bool disconnected = !m_connected;
      bool continuous = timerUpdate.Enabled;
      // disable user interface items that cannot be changed
      // in current state or not relevant

      // all user entries should be disabled when connected

      this.cbPort.Enabled = disconnected;
      this.bStreaming.Enabled = disconnected;
      this.bEyeHead.Enabled = disconnected;
      this.txtConfigFile.Enabled = disconnected;
      this.bWriteLogFile.Enabled = disconnected;
      this.txtLogFile.Enabled = disconnected && this.bWriteLogFile.Checked;
      this.btnBrowseConfig.Enabled = disconnected;
      this.btnBrowseLog.Enabled = disconnected && this.bWriteLogFile.Checked;

      // disable commands depending on the state
      this.btnConnect.Enabled = disconnected;
      this.btnStartStopContinuous.Enabled = m_connected && (!this.continuousModeStarted) && this.bStreaming.Checked;
      this.btnGetRecord.Enabled = m_connected && (!this.continuousModeStarted);
      this.btnRestoreDefaults.Enabled = disconnected;
    }

    private void OnNotify()
    {
      Array dataItems;
      if (GetRecord(out dataItems))
        m_records.Add(dataItems);
    }

    private bool GetRecord(out Array dataItems)
    {
      int count;
      bool available;
      try
      {
        m_serialOut.GetScaledData(out dataItems, out count, out available);
      }
      catch (COMException)
      {
        dataItems = null;
        return false;
      }

      return available;
    }

    private void DisplayRecord(Array dataItems)
    {
      if (lvLog.Items.Count > 1000)
        lvLog.Items.Clear();

      ListViewItem listItem = new ListViewItem();
      listItem.SubItems[0].Text = dataItems.GetValue(0).ToString();
      for (int j = 1; j < dataItems.Length; j++)
      {
        string itemStr = dataItems.GetValue(j).ToString();
        listItem.SubItems.Add(itemStr);
        if (m_file != null)
        {
          m_file.Write(itemStr);
          m_file.Write(",");
        }
      }
      this.lvLog.Items.Add(listItem);
      this.lvLog.EnsureVisible(this.lvLog.Items.Count - 1);
      if (this.m_file != null)
        this.m_file.WriteLine();
    }
    /// <summary>
    /// Method which search the index position of one particular int in an array.
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

    #endregion //METHODS

    private void bStreaming_CheckedChanged(object sender, EventArgs e)
    {
      if (this.bStreaming.Checked)
      {
        this.lblErrorStreamingMode.Hide();
      }
      else
      {
        this.lblErrorStreamingMode.Show();
      }
    }
    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER
    #endregion //HELPER
  }
}