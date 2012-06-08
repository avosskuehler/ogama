// <copyright file="DialogTop.cs" company="FU Berlin">
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

namespace Ogama.Modules.Common.Controls
{
  using System;
  using System.ComponentModel;
  using System.Drawing;
  using System.Windows.Forms;

  /// <summary>
  /// This <see cref="UserControl"/> is used to standardize
  /// the layout of popup dialogs with a convinient background logo
  /// an icon and a dialog description.
  /// </summary>
  public partial class DialogTop : UserControl
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
    /// Initializes a new instance of the DialogTop class.
    /// Sets style to double buffer.
    /// </summary>
    public DialogTop()
    {
      this.SetStyle(
        ControlStyles.AllPaintingInWmPaint |
        ControlStyles.UserPaint |
        ControlStyles.DoubleBuffer,
        true);
      this.UpdateStyles();

      this.InitializeComponent();
      this.lblDescription.Focus();
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// The delegate for the thread safe call to Label.Text
    /// </summary>
    /// <param name="newValue">The new <see cref="String"/> to set.</param>
    private delegate void SetTextCallback(string newValue);

    /// <summary>
    /// Gets or sets the <see cref="Image"/> to display in the logo picture box on the top left corner.
    /// </summary>
    [Category("Appearance")]
    [Description("The image to show in the logo picture box on the top left corner.")]
    public Image Logo
    {
      get
      {
        return this.pcbLogo.Image;
      }

      set
      {
        this.pcbLogo.Image = value;
        if (value != null)
        {
          this.spcPicText.Panel1Collapsed = false;
        }
        else
        {
          this.spcPicText.Panel1Collapsed = true;
        }
      }
    }

    /// <summary>
    /// Gets or sets the <see cref="string"/> to display in label at the top of the panel.
    /// </summary>
    [Category("Appearance")]
    [Description("The description to show in label at the top of the panel.")]
    public string Description
    {
      get { return this.lblDescription.Text; }
      set { this.SetDescription(value); }
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
    /// Overridden <see cref="Control.OnPaintBackground(PaintEventArgs)"/>.
    /// Fills background with special gray and draws given background image
    /// right aligned on the control.
    /// </summary>
    /// <param name="e">A <see cref="PaintEventArgs"/> with the event data.</param>
    protected override void OnPaintBackground(PaintEventArgs e)
    {
      e.Graphics.Clear(SystemColors.Control);
      Size backgroundImageSize = this.BackgroundImage.Size;
      Size panelSize = this.Size;
      Point upperLeftCorner = new Point(panelSize.Width - backgroundImageSize.Width, 0);
      e.Graphics.DrawImage(this.BackgroundImage, upperLeftCorner.X, upperLeftCorner.Y, backgroundImageSize.Width, backgroundImageSize.Height);
    }

    #endregion //OVERRIDES

    ///////////////////////////////////////////////////////////////////////////////
    // Methods for doing main class job                                          //
    ///////////////////////////////////////////////////////////////////////////////
    #region METHODS

    /// <summary>
    /// Thread safe sets the Description of the <see cref="lblDescription"/>
    /// </summary>
    /// <param name="header">A <see cref="String"/> with the new header.</param>
    private void SetDescription(string header)
    {
      // InvokeRequired required compares the thread ID of the
      // calling thread to the thread ID of the creating thread.
      // If these threads are different, it returns true.
      if (this.lblDescription.InvokeRequired)
      {
        SetTextCallback d = new SetTextCallback(this.SetDescription);
        this.lblDescription.BeginInvoke(d, new object[] { header });
      }
      else
      {
        this.lblDescription.Text = header;
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
