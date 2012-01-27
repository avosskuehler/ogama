// <copyright file="RichEdit50.cs" company="FU Berlin">
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

namespace VectorGraphics.Controls
{
  using System;
  using System.Drawing;
  using System.Runtime.InteropServices;
  using System.Windows.Forms;

  /// <summary>
  /// Derived from <see cref="RichTextBox"/>.
  /// This class is the RichTextBox version used by WordPad.
  /// It is imported from the msftedit.dll.
  /// </summary>
  /// <remarks>Standard .Net 2 <see cref="RichTextBox"/> uses an old 
  /// version which doesn´t support transparency.</remarks>
  public class RichEdit50 : RichTextBox
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
    /// Initializes a new instance of the RichEdit50 class.
    /// </summary>
    public RichEdit50() :
      base()
    {
      this.SetStyle(ControlStyles.DoubleBuffer, true);
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Enumerations                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region ENUMS

    /// <summary>
    /// This delegate enables asynchronous calls for getting
    /// the TextLength property on a RichTextBox control.
    /// </summary>
    /// <returns>An <see cref="Int32"/> with the text length of the
    /// content int the <see cref="RichTextBox"/>.</returns>
    private delegate int GetTextCallback();

    /// <summary>
    /// This delegate enables asynchronous calls for getting
    /// the BackgroundColor property on a RichTextBox control.
    /// </summary>
    /// <returns>An <see cref="Color"/> with the BackgroundColor of the
    /// <see cref="RichTextBox"/>.</returns>
    private delegate Color GetBackgroundColorCallback();

    /// <summary>
    /// This delegate enables asynchronous calls for setting
    /// the BackgroundColor property on a RichTextBox control.
    /// </summary>
    /// <param name="color">An <see cref="Color"/> with the BackgroundColor of the
    /// <see cref="RichTextBox"/>.</param>
    private delegate void SetBackgroundColorCallback(Color color);

    #endregion ENUMS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets the text length of the
    /// content in the <see cref="RichTextBox"/>.
    /// Overridden. Thread safe version of the
    /// <see cref="RichTextBox.TextLength"/> property.
    /// </summary>
    /// <returns>An <see cref="Int32"/> with the text length of the
    /// content in the <see cref="RichTextBox"/>.</returns>
    public override int TextLength
    {
      get
      {
        if (this.InvokeRequired)
        {
          GetTextCallback d = new GetTextCallback(this.GetTextLength);
          return (int)this.Invoke(d);
        }

        return base.TextLength;
      }
    }

    /// <summary>
    /// Gets or sets the BackColor of the
    /// content in the <see cref="RichTextBox"/>.
    /// Overridden. Thread safe version of the
    /// <see cref="RichTextBox"/> BackColor property.
    /// </summary>
    public override Color BackColor
    {
      get
      {
        if (this.InvokeRequired)
        {
          GetBackgroundColorCallback d = new GetBackgroundColorCallback(this.GetBackgroundColor);
          return (Color)this.Invoke(d);
        }

        return base.BackColor;
      }

      set
      {
        if (this.InvokeRequired)
        {
          SetBackgroundColorCallback d = new SetBackgroundColorCallback(this.SetBackgroundColor);
          this.Invoke(d, value);
          return;
        }

        base.BackColor = value;
      }
    }

    /// <summary>
    /// Gets the required creation parameters when the control handle is created.
    /// It sets the class name to the modern rich text version.
    /// </summary>
    /// <value>A <see cref="CreateParams"/> that contains the required creation 
    /// parameters when the handle to the control is created.</value>
    protected override CreateParams CreateParams
    {
      get
      {
        CreateParams prams = base.CreateParams;
        if (LoadLibrary("msftedit.dll") != IntPtr.Zero)
        {
          prams.ClassName = "RICHEDIT50W";
        }

        return prams;
      }
    }

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
    /// The LoadLibrary function maps the specified executable 
    /// module into the address space of the calling process.
    /// </summary>
    /// <param name="fileName">[in] Pointer to a null-terminated string 
    /// that names the executable module (either a .dll or .exe file). 
    /// The name specified is the file name of the module and is 
    /// not related to the name stored in the library module 
    /// itself, as specified by the LIBRARY keyword in the module-definition (.def) file. </param>
    /// <returns>If the function succeeds, the return value is a handle to the module.
    /// If the function fails, the return value is NULL. 
    /// To get extended error information, call GetLastError.</returns>
    [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
    private static extern IntPtr LoadLibrary(string fileName);

    /// <summary>
    /// This method returns the <see cref="RichTextBox.TextLength"/>
    /// property
    /// </summary>
    /// <returns>An <see cref="Int32"/> with the text length of the
    /// content in the <see cref="RichTextBox"/>.</returns>
    private int GetTextLength()
    {
      return base.TextLength;
    }

    /// <summary>
    /// This method returns the <see cref="RichTextBox"/> BackColor.
    /// property
    /// </summary>
    /// <returns>An <see cref="Color"/> with the BackgroundColor of the
    /// content in the <see cref="RichTextBox"/>.</returns>
    private Color GetBackgroundColor()
    {
      return base.BackColor;
    }

    /// <summary>
    /// This method sets the <see cref="RichTextBox"/> BackColor.
    /// property.
    /// </summary>
    /// <param name="newColor">A <see cref="Color"/> with the BackgroundColor of the
    /// content in the <see cref="RichTextBox"/>.</param>
    private void SetBackgroundColor(Color newColor)
    {
      base.BackColor = newColor;
    }

    #endregion //METHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER
    #endregion //HELPER
  }
}
