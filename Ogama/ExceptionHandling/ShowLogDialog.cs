// <copyright file="ShowLogDialog.cs" company="FU Berlin">
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

namespace Ogama.ExceptionHandling
{
  using System;
  using System.ComponentModel;
  using System.IO;
  using System.Text;
  using System.Windows.Forms;

  /// <summary>
  /// This popup <see cref="Form"/> shows the content of 
  /// a given ascii text file.
  /// </summary>
  public partial class ShowLogDialog : Form
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
    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the ShowLogDialog class.
    /// The Constructor with the file to specify.
    /// </summary>
    /// <param name="file">A <see cref="string"/> with the full filename with path
    /// to the log file to show.</param>
    public ShowLogDialog(string file)
    {
      this.InitializeComponent();
      this.PopulateDialogWithFileContents(file);
      var mailtoStatement = new StringBuilder();
      mailtoStatement.Append("mailto:adrian@ogama.net");
      mailtoStatement.Append("?subject=OGAMA%20error%20report");
      mailtoStatement.Append("&body=Please insert a copy of the logfile here by pressing Ctrl+V.");
      this.llbMailTo.Links.Add(0, this.llbMailTo.Text.Length, mailtoStatement.ToString());
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
    /// The <see cref="LinkLabel.LinkClicked"/> event handler for the
    /// <see cref="LinkLabel"/> <see cref="llbMailTo"/>.
    /// Starts a new browser with the given link.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A <see cref="LinkLabelLinkClickedEventArgs"/> with the event data.</param>
    private void llbMailTo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      this.ExportLogFileToClipboard();

      // Determine which link was clicked within the LinkLabel.
      this.llbMailTo.Links[this.llbMailTo.Links.IndexOf(e.Link)].Visited = true;

      // Display the appropriate link based on the value of the 
      // LinkData property of the Link object.
      var target = e.Link.LinkData as string;

      // If the value looks like a URL, navigate to it.
      // Otherwise, display it in a message box.
      if (null != target)
      {
        try
        {
          System.Diagnostics.Process.Start(target);
        }
        catch (Win32Exception)
        {
          ExceptionMethods.ProcessErrorMessage("You have no application associated for sending mails."
            + " Please send the error.log and exception.log files in your local application data folder "
            + " to adrian@ogama.net to help debugging.");
        }
      }
      else
      {
        MessageBox.Show("Item clicked: " + target);
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
    // Inherited methods                                                         //
    ///////////////////////////////////////////////////////////////////////////////
    #region OVERRIDES
    #endregion //OVERRIDES

    ///////////////////////////////////////////////////////////////////////////////
    // Methods for doing main class job                                          //
    ///////////////////////////////////////////////////////////////////////////////
    #region METHODS

    /// <summary>
    /// Thies method populates the filename label and the file
    /// content text box with the contents of the given file.
    /// </summary>
    /// <param name="file">A <see cref="string"/> with the full filename with path
    /// to the log file to show.</param>
    private void PopulateDialogWithFileContents(string file)
    {
      this.lblFileName.Text = Path.GetFileName(file);
      if (File.Exists(file))
      {
        var backupLogFile = Path.Combine(Properties.Settings.Default.LogfilePath, "copyOfLog.log");
        File.Copy(file, backupLogFile, true);

        // Create an instance of StreamReader to read from a file.
        // The using statement also closes the StreamReader.
        using (var sr = new StreamReader(backupLogFile))
        {
          // Read and display lines from the file until the end of 
          // the file is reached.
          this.txbLog.Text = sr.ReadToEnd();
          sr.Close();
        }

        File.Delete(backupLogFile);
      }
    }

    /// <summary>
    /// This method puts the content of the <see cref="txbLog"/>
    /// into the clipboard.
    /// </summary>
    private void ExportLogFileToClipboard()
    {
      try
      {
        if (this.txbLog.Text != null)
        {
          Clipboard.SetText(this.txbLog.Text);
        }
      }
      catch (Exception ex)
      {
        ExceptionMethods.HandleException(ex);
      }
    }

    #endregion //METHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER
    #endregion //HELPER
  }
}