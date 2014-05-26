// <copyright file="CheckForUpdates.cs" company="FU Berlin">
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

namespace Ogama.MainWindow.Dialogs
{
  using System;
  using System.ComponentModel;
  using System.IO;
  using System.Net;
  using System.Reflection;
  using System.Windows.Forms;

  using Ogama.ExceptionHandling;

  /// <summary>
  /// A small popup <see cref="Form"/> for showing a convinient error message,
  /// when connecting to eye tracker failed.
  /// </summary>
  public partial class CheckForUpdates : Form
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
    /// The <see cref="WebClient"/> which manages the internet connection.
    /// </summary>
    private WebClient updateClient;

    /// <summary>
    /// Saves the assembly version of installed OGAMA.
    /// </summary>
    private Version assemblyVersion;

    /// <summary>
    /// Saves the assembly version of OGAMA version on the server.
    /// </summary>
    private Version serverVersion;

    /// <summary>
    /// Saves the local file to store the version.txt from the server.
    /// </summary>
    private string versionLocalFilename;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the CheckForUpdates class.
    /// </summary>
    public CheckForUpdates()
    {
      this.InitializeComponent();
      this.updateClient = new WebClient();
      this.updateClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(this.updateClient_DownloadProgressChanged);
      this.updateClient.DownloadFileCompleted += new AsyncCompletedEventHandler(this.updateClient_DownloadFileCompleted);
      this.assemblyVersion = Assembly.GetExecutingAssembly().GetName().Version;
      this.versionLocalFilename = Path.Combine(Properties.Settings.Default.LogfilePath, "version.txt");

      this.llbDownloadUri.Links.Add(0, 51, Properties.Settings.Default.OgamaUri);
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES
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
    /// Initializes assembly version of installed OGAMA version.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void frmCheckForUpdates_Load(object sender, EventArgs e)
    {
      this.lblYourVersionNumber.Text = this.assemblyVersion.ToString(3);
    }

    /// <summary>
    /// The <see cref="Form.FormClosing"/> event handler. 
    /// Resets the cursor.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void CheckForUpdates_FormClosing(object sender, FormClosingEventArgs e)
    {
      this.Cursor = Cursors.Default;
    }

    /// <summary>
    /// The <see cref="Form.Shown"/> event handler. 
    /// Initializes the update client. Starts the timer that indicates connection
    /// establishing and starts downloading the version.txt file
    /// from the server asynchronously.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void CheckForUpdates_Shown(object sender, EventArgs e)
    {
      Uri uri = new Uri(Properties.Settings.Default.UpdateUri);
      Application.DoEvents();
      this.tmrConnect.Start();
      this.Cursor = Cursors.WaitCursor;
      this.updateClient.DownloadFileAsync(uri, this.versionLocalFilename);
    }

    /// <summary>
    /// The <see cref="LinkLabel.LinkClicked"/> event handler for the
    /// <see cref="LinkLabel"/> <see cref="llbDownloadUri"/>.
    /// Starts a new browser with the given adress of Ogama download homepage.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A <see cref="LinkLabelLinkClickedEventArgs"/> with the event data.</param>
    private void llbDownloadUri_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      // Determine which link was clicked within the LinkLabel.
      this.llbDownloadUri.Links[this.llbDownloadUri.Links.IndexOf(e.Link)].Visited = true;

      // Display the appropriate link based on the value of the 
      // LinkData property of the Link object.
      string target = e.Link.LinkData as string;

      // If the value looks like a URL, navigate to it.
      // Otherwise, display it in a message box.
      if (null != target)
      {
        System.Diagnostics.Process.Start(target);
      }
      else
      {
        MessageBox.Show("Item clicked: " + target);
      }
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for
    /// the <see cref="PictureBox"/> <see cref="pcbOgama"/>.
    /// User clicked the ogama icon, so open ogama website.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void pcbOgama_Click(object sender, EventArgs e)
    {
      System.Diagnostics.Process.Start(Properties.Settings.Default.OgamaUri);
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for
    /// the <see cref="Button"/> <see cref="btnCancel"/>
    /// User cancels the dialog, so stop downloading.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnCancel_Click(object sender, EventArgs e)
    {
      this.updateClient.CancelAsync();
    }

    /// <summary>
    /// The <see cref="Timer.Tick"/> event handler for
    /// the <see cref="Timer"/> <see cref="tmrConnect"/>
    /// Increases the connection marquee position.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void tmrConnect_Tick(object sender, EventArgs e)
    {
      this.pgbDownload.Value++;
      if (this.pgbDownload.Value >= 100)
      {
        this.pgbDownload.Value = 0;
      }
    }

    #endregion //WINDOWSEVENTHANDLER

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler for Custom Defined Events                                    //
    ///////////////////////////////////////////////////////////////////////////////
    #region CUSTOMEVENTHANDLER

    /// <summary>
    /// The <see cref="WebClient.DownloadProgressChanged"/> event for the
    /// <see cref="WebClient"/> <see cref="updateClient"/>.
    /// Occurs when an asynchronous download operation successfully 
    /// transfers some or all of the data.
    /// So connecting has finished, change marquee style of progress bar and
    /// update its position with new value.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A <see cref="DownloadProgressChangedEventArgs"/> with the event data.</param>
    private void updateClient_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
    {
      this.tmrConnect.Stop();
      this.lblDownloadStatus.Text = "Downloading version file ...";
      this.pgbDownload.Style = ProgressBarStyle.Blocks;
      this.pgbDownload.Value = e.ProgressPercentage;
    }

    /// <summary>
    /// The <see cref="WebClient.DownloadFileCompleted"/> event for the
    /// <see cref="WebClient"/> <see cref="updateClient"/>.
    /// Occurs when an asynchronous file download operation completes.
    /// So download was finished. Check for errors and if successful,
    /// start parsing version.txt and display results.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A <see cref="AsyncCompletedEventArgs"/> with the event data.</param>
    private void updateClient_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
    {
      if (e.Error != null)
      {
        this.lblDownloadStatus.Text = "Downloading failed";
        string message = "Update file could not be downloaded for the following reason" +
          Environment.NewLine + e.Error.Message;
        ExceptionMethods.ProcessErrorMessage(message);
      }
      else
      {
        this.lblDownloadStatus.Text = "Download successful";
        if (this.ParseVersionTxt())
        {
          int result = this.serverVersion.CompareTo(this.assemblyVersion);
          this.lblCurrentVersionNumber.Text = this.serverVersion.ToString();

          if (result > 0)
          {
            this.lblResult.Text = "A new version of OGAMA is available.";
            this.grbResults.Visible = true;
            this.lblInstallHint.Visible = true;
            this.lblAdditionalHint.Visible = true;
            this.llbDownloadUri.Visible = true;
            this.pcbOgama.Visible = true;
          }
          else if (result < 0)
          {
            this.lblResult.Text = "Your version is newer. OOPS. How did you do ?";
            this.grbResults.Visible = true;
            this.lblInstallHint.Visible = false;
            this.llbDownloadUri.Visible = true;
            this.lblAdditionalHint.Visible = false;
            this.pcbOgama.Visible = true;
          }
          else
          {
            this.lblResult.Text = "You already have the latest version installed.";
            this.grbResults.Visible = true;
            this.lblInstallHint.Visible = false;
            this.llbDownloadUri.Visible = false;
            this.lblAdditionalHint.Visible = false;
            this.pcbOgama.Visible = false;
          }
        }
      }

      this.Cursor = Cursors.Default;
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

    /// <summary>
    /// This method parses the version.txt which 
    /// gives the current version of ogama on the server.
    /// </summary>
    /// <returns><strong>True</strong> if parsing was successful, otherwise
    /// <strong>false</strong>.</returns>
    /// <exception cref="FileNotFoundException">If version.txt could not be found.</exception>
    private bool ParseVersionTxt()
    {
      // Check import file.
      if (!File.Exists(this.versionLocalFilename))
      {
        throw new FileNotFoundException("The version.txt file could not be found");
      }

      string line = string.Empty;

      // Begin reading File
      try
      {
        using (StreamReader importReader = new StreamReader(this.versionLocalFilename))
        {
          int majorVersion = 0;
          int minorVersion = 0;
          int build = 0;
          int revision = 0;

          // Read first line in file
          line = importReader.ReadLine();

          // Split separated line items
          string[] items = line.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
          bool parsingFailed = false;

          if (items.Length == 4)
          {
            if (!int.TryParse(items[0], out majorVersion))
            {
              parsingFailed = true;
            }

            if (!int.TryParse(items[1], out minorVersion))
            {
              parsingFailed = true;
            }

            if (!int.TryParse(items[2], out build))
            {
              parsingFailed = true;
            }

            if (!int.TryParse(items[3], out revision))
            {
              parsingFailed = true;
            }
          }
          else
          {
            parsingFailed = true;
          }

          // Read second line in file
          line = importReader.ReadLine();
          if (line != null)
          {
            this.lblAdditionalHint.Text = line;
          }

          if (parsingFailed)
          {
            string message = "The version.txt from the server could not be successfully parsed."
            + Environment.NewLine + "Please go to the website by yourself.";
            ExceptionMethods.ProcessErrorMessage(message);
            return false;
          }
          else
          {
            this.serverVersion = new Version(majorVersion, minorVersion, build, revision);
          }
        }
      }
      catch (Exception ex)
      {
        ExceptionMethods.HandleException(ex);
      }

      return true;
    }

    #endregion //METHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER
    #endregion //HELPER
  }
}