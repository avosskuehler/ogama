// <copyright file="OgamaInstaller.cs" company="FU Berlin">
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

namespace Ogama
{
  using System;
  using System.Collections;
  using System.ComponentModel;
  using System.Configuration.Install;
  using System.IO;
  using System.Reflection;
  using System.Security.AccessControl;
  using System.Security.Permissions;
  using System.Security.Principal;
  using System.Windows.Forms;

  /// <summary>
  /// This class inherits <see cref="Installer"/> and is called during installation and
  /// removal of OGAMA to remove custom copied files in the
  /// <see cref="Application.UserAppDataPath"/> of OGAMA.
  /// </summary>
  [RunInstaller(true)]
  public partial class OgamaInstaller : Installer
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
    /// Initializes a new instance of the OgamaInstaller class.
    /// </summary>
    public OgamaInstaller()
    {
      this.InitializeComponent();
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Enumerations                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region ENUMS
    #endregion ENUMS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets the company string from the assembly.
    /// </summary>
    private string AssemblyCompany
    {
      get
      {
        // Get all Company attributes on this assembly
        object[] attributes =
          Assembly.GetAssembly(this.GetType()).GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);

        // If there aren't any Company attributes, return an empty string
        if (attributes.Length == 0)
        {
          return string.Empty;
        }

        // If there is a Company attribute, return its value
        return ((AssemblyCompanyAttribute)attributes[0]).Company;
      }
    }

    /// <summary>
    /// Gets the product property from the assembly.
    /// </summary>
    private string AssemblyProduct
    {
      get
      {
        // Get all Product attributes on this assembly
        object[] attributes =
          Assembly.GetAssembly(this.GetType()).GetCustomAttributes(typeof(AssemblyProductAttribute), false);

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
    /// Gets OGAMAs current version.
    /// </summary>
    private string AssemblyVersion
    {
      get { return Assembly.GetAssembly(this.GetType()).GetName().Version.ToString(2); }
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

    /// <summary>
    /// Completes the install transaction.
    /// </summary>
    /// <param name="savedState">An <see cref="IDictionary"/> that contains
    /// the state of the computer after all the installers 
    /// in the collection have run. </param>
    [SecurityPermission(SecurityAction.Demand)]
    public override void Commit(IDictionary savedState)
    {
      base.Commit(savedState);
    }

    /// <summary>
    /// Performs the installation.
    /// </summary>
    /// <param name="stateSaver">An <see cref="IDictionary"/> used to save 
    /// information needed to perform a commit, rollback, or uninstall operation.</param>
    [SecurityPermission(SecurityAction.Demand)]
    public override void Install(IDictionary stateSaver)
    {
      base.Install(stateSaver);

      // Get path to our OGAMA common application data folder
      string commonAppData = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
      commonAppData = Path.Combine(commonAppData, this.AssemblyCompany);
      commonAppData = Path.Combine(commonAppData, this.AssemblyProduct);

      // Set read and write rights for everybody on this directory
      // to enable writing of the ogamasc.xml file needed by the
      // screen capture filter
      try
      {
        SecurityIdentifier sid = new SecurityIdentifier(WellKnownSidType.WorldSid, null);

        DirectorySecurity security = Directory.GetAccessControl(commonAppData, AccessControlSections.All);
        security.AddAccessRule(new FileSystemAccessRule(
          sid,
          FileSystemRights.FullControl,
          InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit,
          PropagationFlags.None,
          AccessControlType.Allow));

        Directory.SetAccessControl(commonAppData, security);
      }
      catch (Exception ex)
      {
        MessageBox.Show("Error: " + ex.Message);
      }
    }

    /// <summary>
    /// Removes an installation of OGAMA by removing all the custom
    /// files copied to <see cref="Application.UserAppDataPath"/>.
    /// </summary>
    /// <param name="savedState">An <see cref="IDictionary"/> that contains the state 
    /// of the computer after the installation was complete.</param>
    [SecurityPermission(SecurityAction.Demand)]
    public override void Uninstall(IDictionary savedState)
    {
      base.Uninstall(savedState);

      try
      {
        // Get path to our OGAMA application data folder
        string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        appData = Path.Combine(appData, this.AssemblyCompany);
        appData = Path.Combine(appData, this.AssemblyProduct);
        appData = Path.Combine(appData, this.AssemblyVersion);

        // Remove the whole content.
        DirectoryInfo dir = new DirectoryInfo(appData);
        this.DeleteContent(dir);
      }
      catch (Exception ex)
      {
        MessageBox.Show("Error: " + ex.Message);
      }
    }

    #endregion //OVERRIDES

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
    // Methods for doing main class job                                          //
    ///////////////////////////////////////////////////////////////////////////////
    #region PRIVATEMETHODS

    /// <summary>
    /// Deletes the content of the directory recursively.
    /// </summary>
    /// <param name="dir">A <see cref="DirectoryInfo"/> with the 
    /// directory to delete.</param>
    private void DeleteContent(DirectoryInfo dir)
    {
      foreach (DirectoryInfo subDir in dir.GetDirectories())
      {
        this.DeleteContent(subDir);
      }

      this.DeleteFiles(dir);
      Directory.Delete(dir.FullName);
    }

    /// <summary>
    /// Deletes all the files in the given Directory
    /// </summary>
    /// <param name="dir">A <see cref="DirectoryInfo"/> with the 
    /// directory to delete the content for.</param>
    private void DeleteFiles(DirectoryInfo dir)
    {
      foreach (FileInfo f in dir.GetFiles())
      {
        File.Delete(f.FullName);
      }
    }

    #endregion //PRIVATEMETHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER
    #endregion //HELPER
  }
}
