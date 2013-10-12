// <copyright file="WebsiteScreenshot.cs" company="alea technologies">
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

namespace Ogama.Modules.Recording.Presenter
{
  using System;
  using System.Drawing;
  using System.Drawing.Imaging;
  using System.Runtime.InteropServices;
  using System.Windows.Forms;

  using Ogama.ExceptionHandling;

  using OgamaControls;

  /// <summary>
  /// This class creates screenshots of websites.
  /// </summary>
  public class WebsiteScreenshot : IDisposable
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
    /// The static member, that holds the recent files list.
    /// </summary>
    private static WebsiteScreenshot instance;

    /// <summary>
    /// The <see cref="WebBrowser"/> duplicate of the site to
    /// be navigated to, used to create the screenshots
    /// </summary>
    private WebBrowser webBrowser;

    /// <summary>
    /// This <see cref="Form"/> contains the <see cref="WebBrowser"/>
    /// control but is minimal sized and at the back of the z-order.
    /// </summary>
    private Form dummyForm;

    /// <summary>
    /// The url that is targeted for navigating.
    /// When this is receiving the message document completed,
    /// all sub frames are loaded.
    /// </summary>
    private Uri targetUrl;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Prevents a default instance of the WebsiteScreenshot class from being created.
    /// </summary>
    private WebsiteScreenshot()
    {
      this.CreateBrowserObject();
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining events, enums, delegates                                         //
    ///////////////////////////////////////////////////////////////////////////////
    #region EVENTS

    /// <summary>
    /// This delegate is used for calling the navigate method
    /// in a separate thread.
    /// </summary>
    /// <param name="navigatingArgs">A <see cref="WebBrowserNavigatingEventArgs"/>
    /// with the event data to navigate to.</param>
    public delegate void NavigateInvoker(WebBrowserNavigatingEventArgs navigatingArgs);

    /// <summary>
    /// The delegate for the thread safe call to a navigate.
    /// </summary>
    /// <param name="newUri">The new <see cref="Uri"/> to navigate to.</param>
    private delegate void NavigateCallback(Uri newUri);

    /// <summary>
    /// The delegate for the thread safe call to a navigate.
    /// </summary>
    /// <param name="newUri">The new <see cref="Uri"/> to navigate to.</param>
    /// <param name="newFrame">The frame <see cref="String"/> to navigate to.</param>
    private delegate void NavigateFrameCallback(Uri newUri, string newFrame);

    #endregion EVENTS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets the singleton instance of this class.
    /// </summary>
    /// <value>A <see cref="WebsiteScreenshot"/> with the 
    /// singleton instance of this class.</value>
    public static WebsiteScreenshot Instance
    {
      get
      {
        if (instance == null)
        {
          instance = new WebsiteScreenshot();
        }

        return instance;
      }
    }

    /// <summary>
    /// Gets a value indicating whether this class has been used,
    /// to be aware of its disposal.
    /// </summary>
    public static bool HasBeenUsed
    {
      get
      {
        return instance != null;
      }
    }

    /// <summary>
    /// Gets or sets the filename with full path to the screenshot 
    /// this class should write to.
    /// </summary>
    public string ScreenshotFilename { get; set; }

    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Public methods                                                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region PUBLICMETHODS

    /// <summary>
    /// Implementation of the IDispose interface.
    /// Releases the <see cref="WebBrowser"/> object.
    /// </summary>
    public void Dispose()
    {
      if (this.webBrowser != null)
      {
        try
        {
          ThreadSafe.Dispose(this.webBrowser);
        }
        catch (InvalidComObjectException ex)
        {
          ExceptionMethods.HandleExceptionSilent(ex);
        }

        this.webBrowser = null;
      }

      if (this.dummyForm != null)
      {
        try
        {
          ThreadSafe.Close(this.dummyForm);
        }
        catch (Exception ex)
        {
          ExceptionMethods.HandleExceptionSilent(ex);
        }

        this.dummyForm = null;
      }
    }

    /// <summary>
    /// Navigates the internal <see cref="WebBrowser"/> to the given url,
    /// using the given <see cref="WebBrowserNavigatingEventArgs"/>.
    /// Can include frames.
    /// </summary>
    /// <param name="navigatingArgs">A <see cref="WebBrowserNavigatingEventArgs"/>
    /// with the url and optional frame name.</param>
    public void Navigate(WebBrowserNavigatingEventArgs navigatingArgs)
    {
      this.CreateBrowserObject();
      this.targetUrl = navigatingArgs.Url;

      if (navigatingArgs.TargetFrameName == string.Empty)
      {
        // There is no frame navigated
        this.ThreadSafeNavigate(navigatingArgs.Url);
      }
      else
      {
        // The url is called in a specific target frame.
        this.ThreadSafeNavigate(navigatingArgs.Url, navigatingArgs.TargetFrameName);
      }
    }

    #endregion //PUBLICMETHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Inherited methods                                                         //
    ///////////////////////////////////////////////////////////////////////////////
    #region OVERRIDES

    /// <summary>
    /// Thread safe navigate to a uri with frame.
    /// </summary>
    /// <param name="newUri">A <see cref="Uri"/> to navigate to.</param>
    protected void ThreadSafeNavigate(Uri newUri)
    {
      if (this.webBrowser.InvokeRequired)
      {
        var d = new NavigateCallback(this.ThreadSafeNavigate);
        this.webBrowser.Invoke(d, new object[] { newUri });
      }
      else
      {
        this.webBrowser.Navigate(newUri);
      }
    }

    /// <summary>
    /// Thread safe navigate to a uri with frame.
    /// </summary>
    /// <param name="newUri">A <see cref="Size"/> to navigate to.</param>
    /// <param name="newFrame">The frame <see cref="String"/> to navigate to.</param>
    protected void ThreadSafeNavigate(Uri newUri, string newFrame)
    {
      if (this.webBrowser.InvokeRequired)
      {
        var d = new NavigateFrameCallback(this.ThreadSafeNavigate);
        this.webBrowser.Invoke(d, new object[] { newUri, newFrame });
      }
      else
      {
        this.webBrowser.Navigate(newUri, newFrame);
      }
    }

    #endregion //OVERRIDES

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler                                                              //
    ///////////////////////////////////////////////////////////////////////////////
    #region EVENTHANDLER

    /// <summary>
    /// The <see cref="WebBrowser.DocumentCompleted"/> event handler.
    /// That takes a full size screenshot including scroll rectangle
    /// via DrawToBitmap.
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">A <see cref="WebBrowserDocumentCompletedEventArgs"/> with the event data.</param>
    private void WebBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
    {
      if (e.Url == this.targetUrl)
      {
        WebBrowser webBrowser = (WebBrowser)sender;

        // Reset window to default size
        Size scrollSize = Document.ActiveDocument.PresentationSize;
        webBrowser.ClientSize = scrollSize;

        // Get maximal sizes
        GetMaxScrollSizeOfDocument(webBrowser.Document, ref scrollSize);
        this.GetLargestScrollSizeOfAllFrames(webBrowser.Document.Window.Frames, ref scrollSize);

        // Update the undocked webbrowser controls client size
        // to have the fully sized content
        webBrowser.ClientSize = scrollSize;

        // Create a bitmap with the size
        Bitmap websiteImage = new Bitmap(scrollSize.Width, scrollSize.Height);

        // Fill the bitmap
        webBrowser.DrawToBitmap(
          websiteImage,
          new Rectangle(0, 0, scrollSize.Width, scrollSize.Height));

        // Save to disk
        websiteImage.Save(this.ScreenshotFilename, ImageFormat.Png);
      }
    }

    #endregion //EVENTHANDLER

    ///////////////////////////////////////////////////////////////////////////////
    // Methods and Eventhandling for Background tasks                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region THREAD
    #endregion //THREAD

    ///////////////////////////////////////////////////////////////////////////////
    // Methods for doing main class job                                          //
    ///////////////////////////////////////////////////////////////////////////////
    #region PRIVATEMETHODS

    /// <summary>
    /// Static method to parse the current <see cref="HtmlDocument"/> of the
    /// <see cref="WebBrowser"/> for its maximal scrollsize.
    /// Returns the maximum of the given scrollsize and the documents scrollsize.
    /// So be sure to reset currentSrollsize first.
    /// </summary>
    /// <param name="document">The <see cref="HtmlDocument"/> to check its maximal
    /// size.</param>
    /// <param name="currentScrollsize">Ref. A <see cref="Size"/> to be updated with
    /// maximal scroll size values.</param>
    private static void GetMaxScrollSizeOfDocument(HtmlDocument document, ref Size currentScrollsize)
    {
      if (document.GetElementsByTagName("HTML").Count > 0 && document.Body != null)
      {
        var htmlWidth = document.GetElementsByTagName("HTML")[0].ScrollRectangle.Width;
        var htmlHeight = document.GetElementsByTagName("HTML")[0].ScrollRectangle.Height;
        var scrollWidth = document.Body.ScrollRectangle.Width;
        var scrollHeight = document.Body.ScrollRectangle.Height;

        var maxWidth = (int)Math.Max(htmlWidth, scrollWidth);
        var maxHeight = (int)Math.Max(htmlHeight, scrollHeight);

        currentScrollsize.Width = Math.Max(currentScrollsize.Width, maxWidth);
        currentScrollsize.Height = Math.Max(currentScrollsize.Height, maxHeight);
      }
    }

    /// <summary>
    /// This method iterates recursively through the <see cref="HtmlWindowCollection"/>
    /// of a webbrowser document get the maximal scroll size of the containing frames.
    /// </summary>
    /// <param name="htmlWindows">The first <see cref="HtmlWindowCollection"/>
    /// to start parsing. You get it from browser.Document.Window.Frames</param>
    /// <param name="currentScrollsize">Ref. A <see cref="Size"/> to be updated with
    /// maximal scroll size values.</param>
    private void GetLargestScrollSizeOfAllFrames(HtmlWindowCollection htmlWindows, ref Size currentScrollsize)
    {
      try
      {
        foreach (HtmlWindow window in htmlWindows)
        {
          GetMaxScrollSizeOfDocument(window.Document, ref currentScrollsize);
          this.GetLargestScrollSizeOfAllFrames(window.Document.Window.Frames, ref currentScrollsize);
        }
      }
      catch (UnauthorizedAccessException ex)
      {
        ExceptionMethods.HandleExceptionSilent(ex);
      }
    }

    /// <summary>
    /// Initializes the <see cref="WebBrowser"/> for the first time
    /// in a form. Attaches the <see cref="WebBrowser.DocumentCompleted"/> event.
    /// Puts the container form in the background and hide it from the taskbar.
    /// </summary>
    private void CreateBrowserObject()
    {
      if (this.webBrowser != null)
      {
        return;
      }

      // The webbrowser is not docked, because
      // the screenshot works if its client size is correctly sized
      // but the form does not have to.
      this.webBrowser = new WebBrowser();
      this.dummyForm = new Form
        {
          ClientSize = new Size(1, 1),
          FormBorderStyle = FormBorderStyle.None
        };

      this.dummyForm.Controls.Add(this.webBrowser);
      this.dummyForm.SendToBack();
      this.dummyForm.Show();
      this.dummyForm.ShowInTaskbar = false;
      this.webBrowser.ScrollBarsEnabled = true;
      this.webBrowser.DocumentCompleted += this.WebBrowser_DocumentCompleted;
    }

    #endregion //PRIVATEMETHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER
    #endregion //HELPER
  }
}
