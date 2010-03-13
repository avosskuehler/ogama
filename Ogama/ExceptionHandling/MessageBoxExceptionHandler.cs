// <copyright file="MessageBoxExceptionHandler.cs" company="FU Berlin">
// ******************************************************
// OGAMA - open gaze and mouse analyzer 
// Copyright (C) 2010 Adrian Voßkühler  
// ------------------------------------------------------------------------
// This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
// This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
// You should have received a copy of the GNU General Public License along with this program; if not, write to the Free Software Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
// **************************************************************
// </copyright>
// <author>Adrian Voßkühler</author>
// <email>adrian.vosskuehler@fu-berlin.de</email>

namespace Ogama.ExceptionHandling
{
  using System;
  using System.Collections.Specialized;
  using System.Windows.Forms;

  using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
  using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.ObjectBuilder;
  using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
  using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling.Configuration;

  /// <summary>
  /// <para>Defines the contract for an ExceptionHandler. 
  /// An ExceptionHandler contains specific handling logic 
  /// (i.e. logging the exception, replacing the exception, etc.) 
  /// that is executed in a chain of multiple ExceptionHandlers.</para>
  /// <para>This implementation is to handle exceptions with a message box.
  /// It is used in the "Message Box Only Policy" and "Global Policy"
  /// to show the user a convenient message box.</para>
  /// </summary>
  [ConfigurationElementType(typeof(CustomHandlerData))]
  public class MessageBoxExceptionHandler : IExceptionHandler
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
    /// Initializes a new instance of the MessageBoxExceptionHandler class.
    /// </summary>
    /// <param name="ignore">An unused <see cref="NameValueCollection"/>.</param>
    public MessageBoxExceptionHandler(NameValueCollection ignore)
    {
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
    /// Implementation of the <see cref="IExceptionHandler.HandleException(Exception,Guid)"/>
    /// The method that is used to handle the exception.
    /// In this implementation a message box is shown,
    /// if the user clicks abort, the application is closed.
    /// When "try to continue" is clicked, the application
    /// trys to ignore this exception.
    /// </summary>
    /// <param name="exception">The <see cref="Exception"/> to handle.</param>
    /// <param name="correlationID">The unique <see cref="Guid"/> attached to 
    /// the handling chain for this handling instance.</param>
    /// <returns>Modified exception to pass to the next exceptionHandlerData in the chain.</returns>
    public Exception HandleException(Exception exception, Guid correlationID)
    {
      DialogResult result = this.ShowThreadExceptionDialog(exception);

      // Exits the program when the user clicks Abort
      if (result == DialogResult.Abort)
      {
        Application.Exit();
      }

      return exception;
    }

    /// <summary>
    /// Creates the error message and displays it in 
    /// a <see cref="ExceptionDialog"/>.
    /// </summary>
    /// <param name="e">The <see cref="Exception"/> to show.</param>
    /// <returns>The <see cref="DialogResult"/> of the dialog with the
    /// users choice.</returns>
    private DialogResult ShowThreadExceptionDialog(Exception e)
    {
      ExceptionDialog newExceptionDlg = new ExceptionDialog();
      newExceptionDlg.ExceptionMessage = e.Message;
      newExceptionDlg.ExceptionDetails = e.ToString();
      return newExceptionDlg.ShowDialog();
    }

    #endregion //METHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER
    #endregion //HELPER
  }
}
