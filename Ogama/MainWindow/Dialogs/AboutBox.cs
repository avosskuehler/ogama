// <copyright file="AboutBox.cs" company="FU Berlin">
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
  using System.Reflection;
  using System.Windows.Forms;

  /// <summary>
  /// A <see cref="Form"/> for displaying the about box.
  /// </summary>
  public sealed partial class AboutBox : Form
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
    /// Initializes a new instance of the AboutBox class.
    /// Initialize the AboutBox to display the product 
    /// information from the assembly information.
    ///  Change assembly information settings for your application through either:
    ///  - Project->Properties->Application->Assembly Information
    ///  - AssemblyInfo.cs
    /// </summary>
    public AboutBox()
    {
      this.InitializeComponent();

      this.Text = string.Format("About {0}", this.AssemblyTitle);
      this.labelProductName.Text = this.AssemblyProduct;
      this.labelVersion.Text = string.Format("Version {0}", this.AssemblyVersion);
      this.labelCopyright.Text = this.AssemblyCopyright;
      this.labelCompanyName.Text = this.AssemblyCompany;
      this.link.Links.Add(0, 20, "http://www.ogama.net");
    }

    #endregion //CONSTRUCTION
    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets the assemblies title.
    /// </summary>
    public string AssemblyTitle
    {
      get
      {
        // Get all Title attributes on this assembly
        object[] attributes =
          Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);

        // If there is at least one Title attribute
        if (attributes.Length > 0)
        {
          // Select the first one
          var titleAttribute = (AssemblyTitleAttribute)attributes[0];

          // If it is not an empty string, return it
          if (titleAttribute.Title != string.Empty)
          {
            return titleAttribute.Title;
          }
        }

        // If there was no Title attribute, or if the Title attribute was the empty string, return the .exe name
        return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
      }
    }

    /// <summary>
    /// Gets OGAMAs current version.
    /// </summary>
    public string AssemblyVersion
    {
      get { return Assembly.GetExecutingAssembly().GetName().Version.ToString(); }
    }

    /// <summary>
    /// Gets assembly description.
    /// </summary>
    public string AssemblyDescription
    {
      get
      {
        // Get all Description attributes on this assembly
        object[] attributes =
          Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);

        // If there aren't any Description attributes, return an empty string
        if (attributes.Length == 0)
        {
          return string.Empty;
        }

        // If there is a Description attribute, return its value
        return ((AssemblyDescriptionAttribute)attributes[0]).Description;
      }
    }

    /// <summary>
    /// Gets the product property from the assembly.
    /// </summary>
    public string AssemblyProduct
    {
      get
      {
        // Get all Product attributes on this assembly
        object[] attributes =
          Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);

        // If there aren't any Product attributes, return an empty string
        if (attributes.Length == 0)
        {
          return string.Empty;
        }

        // If there is a Product attribute, return its value
        return ((AssemblyProductAttribute)attributes[0]).Product;
      }
    }

    /// <summary>
    /// Gets the copyright property from the assembly.
    /// </summary>
    public string AssemblyCopyright
    {
      get
      {
        // Get all Copyright attributes on this assembly
        object[] attributes =
          Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);

        // If there aren't any Copyright attributes, return an empty string
        if (attributes.Length == 0)
        {
          return string.Empty;
        }

        // If there is a Copyright attribute, return its value
        return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
      }
    }

    /// <summary>
    /// Gets the company string from the assembly.
    /// </summary>
    public string AssemblyCompany
    {
      get
      {
        // Get all Company attributes on this assembly
        object[] attributes =
          Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);

        // If there aren't any Company attributes, return an empty string
        if (attributes.Length == 0)
        {
          return string.Empty;
        }

        // If there is a Company attribute, return its value
        return ((AssemblyCompanyAttribute)attributes[0]).Company;
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

    /// <summary>
    /// The <see cref="LinkLabel.LinkClicked"/> event handler for the
    /// <see cref="LinkLabel"/> <see cref="link"/>.
    /// Starts a new browser with the given adress of OGAMAs homepage.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A <see cref="LinkLabelLinkClickedEventArgs"/> with the event data.</param>
    private void LinkLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      // Determine which link was clicked within the LinkLabel.
      this.link.Links[this.link.Links.IndexOf(e.Link)].Visited = true;

      // Display the appropriate link based on the value of the 
      // LinkData property of the Link object.
      var target = e.Link.LinkData as string;

      // If the value looks like a URL, navigate to it.
      // Otherwise, display it in a message box.
      if (target != null)
      {
        System.Diagnostics.Process.Start(target);
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
    #endregion //METHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER
    #endregion //HELPER
  }
}
