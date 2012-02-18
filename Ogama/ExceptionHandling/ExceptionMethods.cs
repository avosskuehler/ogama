// <copyright file="ExceptionMethods.cs" company="FU Berlin">
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

namespace Ogama.ExceptionHandling
{
  using System;
  using System.Collections.Generic;
  using System.Diagnostics;
  using System.IO;
  using System.Text;
  using System.Windows.Forms;

  using Ogama.MainWindow;

  /// <summary>
  /// Static methods used for OGAMAs exception handling.
  /// </summary>
  /// <remarks>The methods in this class for sending errors and exceptions per mail
  /// to me are commented out, because they didn´t work as expected.
  /// The outlook export shows always a security error message and the net mail 
  /// smtp did´t worked.</remarks>
  public static class ExceptionMethods
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
    /// Creates the error message and logs it to the exception file.
    /// </summary>
    /// <param name="ex">The <see cref="Exception"/> to be logged.</param>
    public static void HandleExceptionSilent(Exception ex)
    {
      // Add error to error log
      string exceptionLogFile = Path.Combine(Properties.Settings.Default.LogfilePath, "exception.log");
      string message = GetLogEntryForException(ex);

      using (StreamWriter w = File.AppendText(exceptionLogFile))
      {
        Log(message, w);

        // Close the writer and underlying file.
        w.Close();
      }
    }

    /// <summary>
    /// Creates the error message, logs it to file and displays it in 
    /// an <see cref="ExceptionDialog"/>.
    /// </summary>
    /// <param name="e">The <see cref="Exception"/> to show.</param>
    public static void HandleException(Exception e)
    {
      // Add error to error log
      string exceptionLogFile = Path.Combine(Properties.Settings.Default.LogfilePath, "exception.log");

      var message = new StringBuilder(e.Message);
      Exception innerException = e;
      message.AppendLine("------------------------------------");

      var trace = new StackTrace(e, true);
      message.AppendLine("Method name:" + trace.GetFrame(0).GetMethod().Name);
      message.AppendLine("Line: " + trace.GetFrame(0).GetFileLineNumber());
      message.AppendLine("Column: " + trace.GetFrame(0).GetFileColumnNumber());

      // Loop inner exceptions.
      while (innerException.InnerException != null)
      {
        innerException = innerException.InnerException;
        message.AppendLine(GetLogEntryForException(innerException));
      }

      message.AppendLine(GetLogEntryForException(innerException));

      using (StreamWriter w = File.AppendText(exceptionLogFile))
      {
        Log(message.ToString(), w);

        // Close the writer and underlying file.
        w.Close();
      }

      var newExceptionDlg = new ExceptionDialog
        {
          ExceptionMessage = e.Message,
          ExceptionDetails = message.ToString()
        };

      var result = newExceptionDlg.ShowDialog();
      switch (result)
      {
        case DialogResult.Abort:
          Application.Exit();
          break;
        case DialogResult.OK:
          break;
      }
    }

    /// <summary>
    /// Process any unhandled exceptions that occur in the application.
    /// This code is called by all UI entry points in the application (e.g. button click events)
    /// when an unhandled exception occurs.
    /// You could also achieve this by handling the Application.ThreadException event, however
    /// the VS2005 debugger will break before this event is called.
    /// </summary>
    /// <param name="ex">The unhandled exception</param>
    public static void ProcessUnhandledException(Exception ex)
    {
      // An unhandled exception occured somewhere in our application. Let
      // the 'Global Policy' handler have a try at handling it.
      try
      {
        HandleException(ex);
      }
      catch
      {
        // Something has gone wrong during HandleException (e.g. incorrect configuration of the block).
        // Exit the application
        string errorMsg = "An unexpected exception occured while calling HandleException.";
        errorMsg += Environment.NewLine;

        MessageBox.Show(errorMsg, "Application Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);

        Application.Exit();
      }
    }

    /// <summary>
    /// Raises a "user-friendly" error message dialog.
    /// </summary>
    /// <param name="message">A <see cref="string"/> with the message to display.</param>
    public static void ProcessErrorMessage(string message)
    {
      if (message == null)
      {
        throw new ArgumentException("Error message string is NULL");
      }

      // Add error to error log
      string errorLogFile = Path.Combine(Properties.Settings.Default.LogfilePath, "error.log");
      using (StreamWriter w = File.AppendText(errorLogFile))
      {
        Log(message, w);

        // Close the writer and underlying file.
        w.Close();
      }

      // Show error message dialog
      ErrorDialog newErrorDlg = new ErrorDialog(message);

      // Exits the program when the user clicks Abort
      if (newErrorDlg.ShowDialog() == DialogResult.Abort)
      {
        Application.Exit();
      }
    }

    /// <summary>
    /// Raises a ogama styled message dialog.
    /// </summary>
    /// <param name="title">A <see cref="string"/> with the title to display.</param>
    /// <param name="message">A <see cref="string"/> with the message to display.</param>
    public static void ProcessMessage(string title, string message)
    {
      // Show message dialog
      InformationDialog newDlg = new InformationDialog(title, message, false, MessageBoxIcon.Information);
      newDlg.ShowDialog();
    }

    /// <summary>
    /// This method logs the given message into the file
    /// given by the <see cref="TextWriter"/>.
    /// </summary>
    /// <param name="logMessage">A <see cref="string"/> with the message to log.</param>
    /// <param name="w">The <see cref="TextWriter"/> to write the message to.</param>
    public static void Log(string logMessage, TextWriter w)
    {
      w.Write("Error on ");
      w.WriteLine("{0}, {1}", DateTime.Now.ToLongDateString(), DateTime.Now.ToLongTimeString());
      w.WriteLine("  - {0}", logMessage);
      w.WriteLine("--------------------------------------------------------------------------------");

      // Update the underlying file.
      w.Flush();
    }

    //// The following could be used to send the message per mail,
    //// but it didn´t work, only the outlook interface worked,
    //// but I didn´t liked it...

    /////// <summary>
    /////// Sends the given exception per smtp to "zorro58@gmx.de".
    /////// </summary>
    /////// <param name="e">The <see cref="Exception"/> to send.</param>
    /////// <returns><strong>True</strong> if successfull, otherwise <strong>false</strong>.</returns>
    ////public static bool SendErrorMessagePerMail(Exception e)
    ////{
    ////  try
    ////  {
    ////    if (e == null)
    ////      throw (new ArgumentNullException());

    ////    OutlookMail newMail = new OutlookMail();
    ////    newMail.addToOutBox("zorro58@gmx.de", "OGAMA error" + e.Message, e.ToString());
    ////    //MailMessage message = new MailMessage("zorro58@gmx.de", "zorro58@gmx.de", "Testmail", "Dies ist der Body" + Environment.NewLine + Environment.NewLine);
    ////    //SmtpClient emailClient = new SmtpClient();
    ////    //System.Net.NetworkCredential SMTPUserInfo = new System.Net.NetworkCredential("pt8533746-advo", "efbafpo6");
    ////    //emailClient.UseDefaultCredentials = false;
    ////    //emailClient.Credentials = SMTPUserInfo;
    ////    //emailClient.Send(message);

    ////    //MailMessage mailMessage = new MailMessage("zorro58@gmx.de", "zorro58@gmx.de", "Testmail", "Dies ist der Body"+Environment.NewLine+Environment.NewLine);

    ////    //SmtpClient sc = new SmtpClient("smtp.1und1.com");
    ////    //sc.Credentials = new NetworkCredential("pt8533746-advo", "efbafpo6");
    ////    //sc.EnableSsl = true;
    ////    //sc.DeliveryMethod = SmtpDeliveryMethod.Network;
    ////    //sc.UseDefaultCredentials = false;
    ////    ////SmtpClient sc = new SmtpClient("smpt.1und1.com");
    ////    ////sc.Credentials = new NetworkCredential("zorro58@gmx.de", "efbafpo6");
    ////    //sc.Send(mailMessage);
    ////    //// To
    ////    //MailMessage mailMsg = new MailMessage();
    ////    //mailMsg.To="zorro58@gmx.de";

    ////    //// From
    ////    //mailMsg.From = "ogama@gmx.de";

    ////    //// Subject and Body
    ////    //mailMsg.Subject = "OGAMA error: " + e.Message;
    ////    //mailMsg.Body = e.ToString();
    ////    //mailMsg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", "1");//basic authentication
    ////    //mailMsg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusername", "zorro58@gmx.de");//set your username here
    ////    //mailMsg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendpassword", "efbafpo6");//set your password here

    ////    //// Init SmtpClient and send
    ////    //SmtpMail.SmtpServer = "pop.gmx.net";
    ////    //SmtpMail.Send(mailMsg);
    ////    ////System.Net.NetworkCredential credentials = new System.Net.NetworkCredential("pt8533746-advo", "efbafpo6");
    ////    ////smtpClient.Credentials = credentials;

    ////    ////smtpClient.Send(mailMsg);
    ////    return true;
    ////  }
    ////  catch (Exception ex)
    ////  {
    ////    bool rethrow = ExceptionPolicy.HandleException(ex, "Global Policy");
    ////    if (rethrow)
    ////      throw;
    ////    return false;
    ////  }

    ////}

    /////// <summary>
    /////// A class for sending error messages via outlook client of the users.
    /////// </summary>
    ////public class OutlookMail
    ////{
    ////  private Outlook.Application oApp;
    ////  private Outlook._NameSpace oNameSpace;
    ////  private Outlook.MAPIFolder oOutboxFolder;

    ////  /// <summary>
    ////  /// Constructor. Initalizes interface.
    ////  /// </summary>
    ////  public OutlookMail()
    ////  {
    ////    //Return a reference to the MAPI layer 
    ////    oApp = new Outlook.Application();

    ////    //The Namespace object represents the messaging service provider. In order to get access to all Outlook folders and items we have to use the MAPI namespace. 

    ////    oApp = new Outlook.Application();
    ////    oNameSpace = oApp.GetNamespace("MAPI");

    ////    //Now that we have the MAPI namespace, we can log on using using: 

    ////    //<mapinamespace>.Logon(object Profile, object Password, object ShowDialog, object NewSession) 

    ////    //Profile: This is a string value that indicates what MAPI profile to use for logging on. Set this to null if using the currently logged on user, or set to an empty string (string.Empty) if you wish to use the default Outlook Profile. 
    ////    //Password: The password for the indicated profile. Set to null if using the currently logged on user, or set to an empty string (string.Empty) if you wish to use the default Outlook Profile password. 
    ////    //ShowDialog: Set to True to display the Outlook Profile dialog box. 
    ////    //NewSession: Set to True to start a new session or set to False to use the current session. 

    ////    oNameSpace.Logon(null, null, true, true);

    ////    //We now choose which folder we want to work with. A MAPIFolder object represents a single Outlook folder. For example you could use:

    ////    //Calender: Outlook.OlDefaultFolders.olFolderCalendar 
    ////    //Contacts: Outlook.OlDefaultFolders.olFolderContacts 
    ////    //Inbox: Outlook.OlDefaultFolders.olFolderInbox 

    ////    //For this example we choose the Outbox folder

    ////    //gets defaultfolder for my Outlook Outbox 
    ////    oOutboxFolder = oNameSpace.GetDefaultFolder(Outlook.OlDefaultFolders.olFolderOutbox);
    ////  }

    ////  /// <summary>
    ////  /// Creates an outlook email item and puts it in the outgoing folder.
    ////  /// We create a MailItem, and set the To, Subject, and Body fields.
    ////  /// </summary>
    ////  /// <param name="toValue">A <see cref="string"/> with the To Email.</param>
    ////  /// <param name="subjectValue">A <see cref="string"/> with the emails subject line.</param>
    ////  /// <param name="bodyValue">A <see cref="string"/> with the emails body.</param>
    ////  public void addToOutBox(string toValue, string subjectValue, string bodyValue)
    ////  {
    ////    Outlook._MailItem oMailItem = (Outlook._MailItem)oApp.CreateItem(Outlook.OlItemType.olMailItem);
    ////    oMailItem.To = toValue;
    ////    oMailItem.Subject = subjectValue;
    ////    oMailItem.Body = bodyValue;
    ////    oMailItem.SaveSentMessageFolder = oOutboxFolder;
    ////    //uncomment this to also save this in your draft 
    ////    //oMailItem.Save(); 
    ////    //adds it to the outbox 
    ////    oMailItem.Send();
    ////  }
    ////}

    #endregion //METHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER

    /// <summary>
    /// Returns a human readable string for the exception
    /// </summary>
    /// <param name="e">An <see cref="Exception"/> to be processed</param>
    /// <returns>A human readable <see cref="String"/> for the exception</returns>
    private static string GetLogEntryForException(Exception e)
    {
      StringBuilder sb = new StringBuilder();
      sb.AppendLine("Message: " + e.Message);
      sb.AppendLine("Source: " + e.Source);
      sb.AppendLine("TargetSite: " + e.TargetSite.ToString());
      sb.AppendLine("StackTrace: " + e.StackTrace);

      return sb.ToString();
    }

    #endregion //HELPER
  }
}
