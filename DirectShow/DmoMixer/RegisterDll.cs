// <copyright file="RegisterDll.cs" company="FU Berlin">
// ****************************************************************************
// While the underlying libraries are covered by LGPL, this sample is released
// as public domain.  It is distributed in the hope that it will be useful, but
// WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY
// or FITNESS FOR A PARTICULAR PURPOSE.
// *****************************************************************************/
// </copyright>

namespace DmoMixer
{
  using System.Collections;
  using System.ComponentModel;
  using System.Configuration.Install;
  using System.Security.Permissions;

  /// <summary>
  /// This class customizes the installation of the overlay DMO
  /// by registering the COM assemply during commit und unregistering it 
  /// during uninstall custom actions.
  /// </summary>
  [RunInstaller(true)]
  public partial class RegisterDll : Installer
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
    /// Initializes a new instance of the RegisterDll class.
    /// </summary>
    public RegisterDll()
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
    /// Completes the install transaction of the DMO
    /// by calling regasm /codebase DmoOverlay.dll
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
    /// Performs the installation of the DMO.
    /// </summary>
    /// <param name="stateSaver">An <see cref="IDictionary"/> used to save 
    /// information needed to perform a commit, rollback, or uninstall operation.</param>
    [SecurityPermission(SecurityAction.Demand)]
    public override void Install(IDictionary stateSaver)
    {
      base.Install(stateSaver);

      ////RegistrationServices regSvc = new RegistrationServices();
      ////regSvc.RegisterAssembly(
      ////  this.GetType().Assembly,
      ////  AssemblyRegistrationFlags.SetCodeBase);

      // Get the location of regasm
      string regasmPath = System.Runtime.InteropServices.RuntimeEnvironment.GetRuntimeDirectory() + @"regasm.exe";

      // Get the location of our DLL
      string componentPath = typeof(RegisterDll).Assembly.Location;

      // Execute regasm
      System.Diagnostics.Process.Start(regasmPath, "/codebase /s \"" + componentPath + "\"");
    }

    /// <summary>
    /// Removes an installation of the DMO by calling regasm /u.
    /// </summary>
    /// <param name="savedState">An <see cref="IDictionary"/> that contains the state 
    /// of the computer after the installation was complete.</param>
    [SecurityPermission(SecurityAction.Demand)]
    public override void Uninstall(System.Collections.IDictionary savedState)
    {
      base.Uninstall(savedState);

      ////RegistrationServices regSvc = new RegistrationServices();
      ////regSvc.UnregisterAssembly(this.GetType().Assembly);

      // Get the location of regasm
      string regasmPath = System.Runtime.InteropServices.RuntimeEnvironment.GetRuntimeDirectory() + @"regasm.exe";

      // Get the location of our DLL
      string componentPath = typeof(RegisterDll).Assembly.Location;

      // Execute regasm
      System.Diagnostics.Process.Start(regasmPath, "/u /s \"" + componentPath + "\"");
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
    #endregion //PRIVATEMETHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER
    #endregion //HELPER
  }
}
