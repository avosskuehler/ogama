// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExtendedBrowser.cs" company="">
//   
// </copyright>
// <author>Adrian Voßkühler</author>
// <email>adrian@ogama.net</email>
// <summary>
//   Defines the ExtendedBrowser type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace OgamaControls.CommonControls.Browser
{
  using System;
  using System.Runtime.InteropServices;
  using System.Windows.Forms;

  /// <summary>
  /// </summary>
  public class ExtendedBrowser : WebBrowser
  {
    #region Fields

    /// <summary>
    ///   The ax i web browser2
    /// </summary>
    private IWebBrowser2 axIWebBrowser2;

    #endregion

    #region Enums

    /// <summary>
    /// </summary>
    public enum OLECMDEXECOPT
    {
      // ...
      /// <summary>
      ///   The olecmdexecop t_ dontpromptuser
      /// </summary>
      OLECMDEXECOPT_DONTPROMPTUSER, 

      // ...
    }

    /// <summary>
    /// </summary>
    public enum OLECMDF
    {
      // ...
      /// <summary>
      ///   The olecmd f_ supported
      /// </summary>
      OLECMDF_SUPPORTED = 1
    }

    /// <summary>
    /// </summary>
    public enum OLECMDID
    {
      // ...
      /// <summary>
      ///   The olecmdi d_ optica l_ zoom
      /// </summary>
      OLECMDID_OPTICAL_ZOOM = 63, 

      /// <summary>
      ///   The olecmdi d_ optica l_ getzoomrange
      /// </summary>
      OLECMDID_OPTICAL_GETZOOMRANGE = 64, 

      // ...
    }

    #endregion

    #region Interfaces

    /// <summary>
    /// </summary>
    [ComImport]
    [TypeLibType(TypeLibTypeFlags.FOleAutomation | TypeLibTypeFlags.FDual | TypeLibTypeFlags.FHidden)]
    [Guid("D30C1661-CDAF-11d0-8A3E-00C04FC9E26E")]
    public interface IWebBrowser2
    {
      /// <summary>
      ///   Goes the back.
      /// </summary>
      [DispId(100)]
      void GoBack();

      /// <summary>
      ///   Goes the forward.
      /// </summary>
      [DispId(0x65)]
      void GoForward();

      /// <summary>
      ///   Goes the home.
      /// </summary>
      [DispId(0x66)]
      void GoHome();

      /// <summary>
      ///   Goes the search.
      /// </summary>
      [DispId(0x67)]
      void GoSearch();

      /// <summary>
      /// Navigates the specified URL.
      /// </summary>
      /// <param name="Url">
      /// The URL.
      /// </param>
      /// <param name="flags">
      /// The flags.
      /// </param>
      /// <param name="targetFrameName">
      /// Name of the target frame.
      /// </param>
      /// <param name="postData">
      /// The post data.
      /// </param>
      /// <param name="headers">
      /// The headers.
      /// </param>
      [DispId(0x68)]
      void Navigate(
        [In] string Url, 
        [In] ref object flags, 
        [In] ref object targetFrameName, 
        [In] ref object postData, 
        [In] ref object headers);

      /// <summary>
      ///   Refreshes this instance.
      /// </summary>
      [DispId(-550)]
      void Refresh();

      /// <summary>
      /// Refresh2s the specified level.
      /// </summary>
      /// <param name="level">
      /// The level.
      /// </param>
      [DispId(0x69)]
      void Refresh2([In] ref object level);

      /// <summary>
      ///   Stops this instance.
      /// </summary>
      [DispId(0x6a)]
      void Stop();

      /// <summary>
      ///   Gets the application.
      /// </summary>
      /// <value>
      ///   The application.
      /// </value>
      [DispId(200)]
      object Application { [return: MarshalAs(UnmanagedType.IDispatch)] get; }

      /// <summary>
      ///   Gets the parent.
      /// </summary>
      /// <value>
      ///   The parent.
      /// </value>
      [DispId(0xc9)]
      object Parent { [return: MarshalAs(UnmanagedType.IDispatch)] get; }

      /// <summary>
      ///   Gets the container.
      /// </summary>
      /// <value>
      ///   The container.
      /// </value>
      [DispId(0xca)]
      object Container { [return: MarshalAs(UnmanagedType.IDispatch)] get; }

      /// <summary>
      ///   Gets the document.
      /// </summary>
      /// <value>
      ///   The document.
      /// </value>
      [DispId(0xcb)]
      object Document { [return: MarshalAs(UnmanagedType.IDispatch)] get; }

      /// <summary>
      ///   Gets a value indicating whether [top level container].
      /// </summary>
      /// <value>
      ///   <c>true</c> if [top level container]; otherwise, <c>false</c>.
      /// </value>
      [DispId(0xcc)]
      bool TopLevelContainer { get; }

      /// <summary>
      ///   Gets the type.
      /// </summary>
      /// <value>
      ///   The type.
      /// </value>
      [DispId(0xcd)]
      string Type { get; }

      /// <summary>
      ///   Gets or sets the left.
      /// </summary>
      /// <value>
      ///   The left.
      /// </value>
      [DispId(0xce)]
      int Left { get; set; }

      /// <summary>
      ///   Gets or sets the top.
      /// </summary>
      /// <value>
      ///   The top.
      /// </value>
      [DispId(0xcf)]
      int Top { get; set; }

      /// <summary>
      ///   Gets or sets the width.
      /// </summary>
      /// <value>
      ///   The width.
      /// </value>
      [DispId(0xd0)]
      int Width { get; set; }

      /// <summary>
      ///   Gets or sets the height.
      /// </summary>
      /// <value>
      ///   The height.
      /// </value>
      [DispId(0xd1)]
      int Height { get; set; }

      /// <summary>
      ///   Gets the name of the location.
      /// </summary>
      /// <value>
      ///   The name of the location.
      /// </value>
      [DispId(210)]
      string LocationName { get; }

      /// <summary>
      ///   Gets the location URL.
      /// </summary>
      /// <value>
      ///   The location URL.
      /// </value>
      [DispId(0xd3)]
      string LocationURL { get; }

      /// <summary>
      ///   Gets a value indicating whether this <see cref="IWebBrowser2" /> is busy.
      /// </summary>
      /// <value>
      ///   <c>true</c> if busy; otherwise, <c>false</c>.
      /// </value>
      [DispId(0xd4)]
      bool Busy { get; }

      /// <summary>
      ///   Quits this instance.
      /// </summary>
      [DispId(300)]
      void Quit();

      /// <summary>
      /// Clients to window.
      /// </summary>
      /// <param name="pcx">
      /// The PCX.
      /// </param>
      /// <param name="pcy">
      /// The pcy.
      /// </param>
      [DispId(0x12d)]
      void ClientToWindow(out int pcx, out int pcy);

      /// <summary>
      /// Puts the property.
      /// </summary>
      /// <param name="property">
      /// The property.
      /// </param>
      /// <param name="vtValue">
      /// The vt value.
      /// </param>
      [DispId(0x12e)]
      void PutProperty([In] string property, [In] object vtValue);

      /// <summary>
      /// Gets the property.
      /// </summary>
      /// <param name="property">
      /// The property.
      /// </param>
      /// <returns>
      /// The <see cref="object"/>.
      /// </returns>
      [DispId(0x12f)]
      object GetProperty([In] string property);

      /// <summary>
      ///   Gets the name.
      /// </summary>
      /// <value>
      ///   The name.
      /// </value>
      [DispId(0)]
      string Name { get; }

      /// <summary>
      ///   Gets the HWND.
      /// </summary>
      /// <value>
      ///   The HWND.
      /// </value>
      [DispId(-515)]
      int HWND { get; }

      /// <summary>
      ///   Gets the full name.
      /// </summary>
      /// <value>
      ///   The full name.
      /// </value>
      [DispId(400)]
      string FullName { get; }

      /// <summary>
      ///   Gets the path.
      /// </summary>
      /// <value>
      ///   The path.
      /// </value>
      [DispId(0x191)]
      string Path { get; }

      /// <summary>
      ///   Gets or sets a value indicating whether this <see cref="IWebBrowser2" /> is visible.
      /// </summary>
      /// <value>
      ///   <c>true</c> if visible; otherwise, <c>false</c>.
      /// </value>
      [DispId(0x192)]
      bool Visible { get; set; }

      /// <summary>
      ///   Gets or sets a value indicating whether [status bar].
      /// </summary>
      /// <value>
      ///   <c>true</c> if [status bar]; otherwise, <c>false</c>.
      /// </value>
      [DispId(0x193)]
      bool StatusBar { get; set; }

      /// <summary>
      ///   Gets or sets the status text.
      /// </summary>
      /// <value>
      ///   The status text.
      /// </value>
      [DispId(0x194)]
      string StatusText { get; set; }

      /// <summary>
      ///   Gets or sets the tool bar.
      /// </summary>
      /// <value>
      ///   The tool bar.
      /// </value>
      [DispId(0x195)]
      int ToolBar { get; set; }

      /// <summary>
      ///   Gets or sets a value indicating whether [menu bar].
      /// </summary>
      /// <value>
      ///   <c>true</c> if [menu bar]; otherwise, <c>false</c>.
      /// </value>
      [DispId(0x196)]
      bool MenuBar { get; set; }

      /// <summary>
      ///   Gets or sets a value indicating whether [full screen].
      /// </summary>
      /// <value>
      ///   <c>true</c> if [full screen]; otherwise, <c>false</c>.
      /// </value>
      [DispId(0x197)]
      bool FullScreen { get; set; }

      /// <summary>
      /// Navigate2s the specified URL.
      /// </summary>
      /// <param name="URL">
      /// The URL.
      /// </param>
      /// <param name="flags">
      /// The flags.
      /// </param>
      /// <param name="targetFrameName">
      /// Name of the target frame.
      /// </param>
      /// <param name="postData">
      /// The post data.
      /// </param>
      /// <param name="headers">
      /// The headers.
      /// </param>
      [DispId(500)]
      void Navigate2(
        [In] ref object URL, 
        [In] ref object flags, 
        [In] ref object targetFrameName, 
        [In] ref object postData, 
        [In] ref object headers);

      /// <summary>
      /// Queries the status wb.
      /// </summary>
      /// <param name="cmdID">
      /// The command identifier.
      /// </param>
      /// <returns>
      /// The <see cref="OLECMDF"/>.
      /// </returns>
      [DispId(0x1f5)]
      OLECMDF QueryStatusWB([In] OLECMDID cmdID);

      /// <summary>
      /// Executes the wb.
      /// </summary>
      /// <param name="cmdID">
      /// The command identifier.
      /// </param>
      /// <param name="cmdexecopt">
      /// The cmdexecopt.
      /// </param>
      /// <param name="pvaIn">
      /// The pva in.
      /// </param>
      /// <param name="pvaOut">
      /// The pva out.
      /// </param>
      [DispId(0x1f6)]
      void ExecWB([In] OLECMDID cmdID, [In] OLECMDEXECOPT cmdexecopt, ref object pvaIn, IntPtr pvaOut);

      /// <summary>
      /// Shows the browser bar.
      /// </summary>
      /// <param name="pvaClsid">
      /// The pva CLSID.
      /// </param>
      /// <param name="pvarShow">
      /// The pvar show.
      /// </param>
      /// <param name="pvarSize">
      /// Size of the pvar.
      /// </param>
      [DispId(0x1f7)]
      void ShowBrowserBar([In] ref object pvaClsid, [In] ref object pvarShow, [In] ref object pvarSize);

      /// <summary>
      ///   Gets the state of the ready.
      /// </summary>
      /// <value>
      ///   The state of the ready.
      /// </value>
      [DispId(-525)]
      WebBrowserReadyState ReadyState { get; }

      /// <summary>
      ///   Gets or sets a value indicating whether this <see cref="IWebBrowser2" /> is offline.
      /// </summary>
      /// <value>
      ///   <c>true</c> if offline; otherwise, <c>false</c>.
      /// </value>
      [DispId(550)]
      bool Offline { get; set; }

      /// <summary>
      ///   Gets or sets a value indicating whether this <see cref="IWebBrowser2" /> is silent.
      /// </summary>
      /// <value>
      ///   <c>true</c> if silent; otherwise, <c>false</c>.
      /// </value>
      [DispId(0x227)]
      bool Silent { get; set; }

      /// <summary>
      ///   Gets or sets a value indicating whether [register as browser].
      /// </summary>
      /// <value>
      ///   <c>true</c> if [register as browser]; otherwise, <c>false</c>.
      /// </value>
      [DispId(0x228)]
      bool RegisterAsBrowser { get; set; }

      /// <summary>
      ///   Gets or sets a value indicating whether [register as drop target].
      /// </summary>
      /// <value>
      ///   <c>true</c> if [register as drop target]; otherwise, <c>false</c>.
      /// </value>
      [DispId(0x229)]
      bool RegisterAsDropTarget { get; set; }

      /// <summary>
      ///   Gets or sets a value indicating whether [theater mode].
      /// </summary>
      /// <value>
      ///   <c>true</c> if [theater mode]; otherwise, <c>false</c>.
      /// </value>
      [DispId(0x22a)]
      bool TheaterMode { get; set; }

      /// <summary>
      ///   Gets or sets a value indicating whether [address bar].
      /// </summary>
      /// <value>
      ///   <c>true</c> if [address bar]; otherwise, <c>false</c>.
      /// </value>
      [DispId(0x22b)]
      bool AddressBar { get; set; }

      /// <summary>
      ///   Gets or sets a value indicating whether this <see cref="IWebBrowser2" /> is resizable.
      /// </summary>
      /// <value>
      ///   <c>true</c> if resizable; otherwise, <c>false</c>.
      /// </value>
      [DispId(0x22c)]
      bool Resizable { get; set; }
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// Zooms the specified factor.
    /// </summary>
    /// <param name="factor">
    /// The factor.
    /// </param>
    public void Zoom(int factor)
    {
      object pvaIn = factor;
      try
      {
        this.axIWebBrowser2.ExecWB(
          OLECMDID.OLECMDID_OPTICAL_ZOOM, 
          OLECMDEXECOPT.OLECMDEXECOPT_DONTPROMPTUSER, 
          ref pvaIn, 
          IntPtr.Zero);
      }
      catch (Exception)
      {
        throw;
      }
    }

    #endregion

    #region Methods

    /// <summary>
    /// Called by the control when the underlying ActiveX control is created.
    /// </summary>
    /// <param name="nativeActiveXObject">
    /// An object that represents the underlying ActiveX control.
    /// </param>
    protected override void AttachInterfaces(object nativeActiveXObject)
    {
      base.AttachInterfaces(nativeActiveXObject);
      this.axIWebBrowser2 = (IWebBrowser2)nativeActiveXObject;
    }

    /// <summary>
    ///   Called by the control when the underlying ActiveX control is discarded.
    /// </summary>
    protected override void DetachInterfaces()
    {
      base.DetachInterfaces();
      this.axIWebBrowser2 = null;
    }

    #endregion
  }
}