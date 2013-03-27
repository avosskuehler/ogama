// <copyright file="WebsiteThumbnailGenerator.cs" company="FU Berlin">
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

namespace VectorGraphics.Controls
{
  using System;
  using System.Drawing;
  using System.Threading;
  using System.Windows.Forms;

  /// <summary>
  /// This class creates thumbnails or screenshots of websites.
  /// </summary>
  public class WebsiteThumbnailGenerator
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
    // Defining events, enums, delegates                                         //
    ///////////////////////////////////////////////////////////////////////////////
    #region EVENTS
    #endregion EVENTS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES
    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Public methods                                                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region PUBLICMETHODS

    /// <summary>
    /// Static method to create a website thumbnail
    /// </summary>
    /// <param name="url">The url string of the adress</param>
    /// <param name="browserWidth">The browser controls initial width</param>
    /// <param name="browserHeight">The browser controls initial height</param>
    /// <param name="thumbnailSize">Size of the thumbnail</param>
    /// <returns>A <see cref="Bitmap"/> with the web sites thumbnail</returns>
    public static Bitmap GetWebSiteThumbnail(
      string url,
      int browserWidth,
      int browserHeight,
      Size thumbnailSize)
    {
      WebsiteImage thumbnailGenerator =
        new WebsiteImage(url, browserWidth, browserHeight, thumbnailSize);
      return thumbnailGenerator.GenerateWebSiteThumbnailImage();
    }

    /// <summary>
    /// Static method to create a website screenshot including full height.
    /// </summary>
    /// <param name="url">The url string of the adress</param>
    /// <param name="presentationSize">Size of the presentation screen</param>
    /// <returns>A <see cref="Bitmap"/> with the web sites screenshot</returns>
    public static Bitmap GetWebSiteScreenshot(string url, Size presentationSize)
    {
      WebsiteImage thumbnailGenerator =
        new WebsiteImage(url, presentationSize);
      Bitmap screenshot = thumbnailGenerator.GenerateWebSiteScreenshot();
      return screenshot;
    }

    /// <summary>
    /// Static method to create a website screenshot including full height
    /// including navigation to frames
    /// </summary>
    /// <param name="baseUrl">The url string of the base web adress</param>
    /// <param name="navigatingArgs">The url and target frame to navigate to.</param>
    /// <param name="presentationSize">Size of the presentation screen</param>
    /// <returns>A <see cref="Bitmap"/> with the web sites screenshot</returns>
    public static Bitmap GetWebSiteScreenshot(
      string baseUrl,
      WebBrowserNavigatingEventArgs navigatingArgs,
      Size presentationSize)
    {
      WebsiteImage thumbnailGenerator =
        new WebsiteImage(baseUrl, navigatingArgs, presentationSize);
      Bitmap screenshot = thumbnailGenerator.GenerateWebSiteScreenshot();
      return screenshot;
    }

    #endregion //PUBLICMETHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Inherited methods                                                         //
    ///////////////////////////////////////////////////////////////////////////////
    #region OVERRIDES
    #endregion //OVERRIDES

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler                                                              //
    ///////////////////////////////////////////////////////////////////////////////
    #region EVENTHANDLER
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
    #endregion //PRIVATEMETHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER
    #endregion //HELPER

    /// <summary>
    /// Class that contains images of websites.
    /// </summary>
    private class WebsiteImage
    {
      /// <summary>
      /// Initializes a new instance of the WebsiteImage class.
      /// </summary>
      /// <param name="url">The url string of the adress</param>
      /// <param name="browserWidth">The browser controls initial width</param>
      /// <param name="browserHeight">The browser controls initial height</param>
      /// <param name="thumbnailSize">Size of the thumbnail</param>
      public WebsiteImage(string url, int browserWidth, int browserHeight, Size thumbnailSize)
      {
        this.BaseUrl = url;
        this.NavigatingArgs = null;
        this.BrowserWidth = browserWidth;
        this.BrowserHeight = browserHeight;
        this.ThumbnailSize = thumbnailSize;
      }

      /// <summary>
      /// Initializes a new instance of the WebsiteImage class.
      /// </summary>
      /// <param name="baseUrl">The url string of the base web adress</param>
      /// <param name="presentationSize">Size of the presentation screen</param>
      public WebsiteImage(string baseUrl, Size presentationSize)
      {
        this.BaseUrl = baseUrl;
        this.NavigatingArgs = null;
        this.BrowserWidth = presentationSize.Width;
        this.BrowserHeight = presentationSize.Height;
        this.ThumbnailSize = presentationSize;
      }

      /// <summary>
      /// Initializes a new instance of the WebsiteImage class.
      /// </summary>
      /// <param name="baseUrl">The url string of the base web adress</param>
      /// <param name="navigatingArgs">The url string and target frame to navigate to.</param>
      /// <param name="presentationSize">Size of the presentation screen</param>
      public WebsiteImage(string baseUrl, WebBrowserNavigatingEventArgs navigatingArgs, Size presentationSize)
      {
        this.BaseUrl = baseUrl;
        this.NavigatingArgs = navigatingArgs;
        this.BrowserWidth = presentationSize.Width;
        this.BrowserHeight = presentationSize.Height;
        this.ThumbnailSize = presentationSize;
      }

      /// <summary>
      /// Gets or sets the URL for the website.
      /// </summary>
      public string BaseUrl { get; set; }

      /// <summary>
      /// Gets or sets the URL for the website to navigate to.
      /// </summary>
      public WebBrowserNavigatingEventArgs NavigatingArgs { get; set; }

      /// <summary>
      /// Gets or sets the screenshot or thumbnail of the website
      /// </summary>
      public Bitmap WebSiteImage { get; set; }

      /// <summary>
      /// Gets or sets the thumbnail size.
      /// </summary>
      public Size ThumbnailSize { get; set; }

      /// <summary>
      /// Gets or sets the width of the browser control
      /// </summary>
      public int BrowserWidth { get; set; }

      /// <summary>
      /// Gets or sets the height of the browser control
      /// </summary>
      public int BrowserHeight { get; set; }

      /// <summary>
      /// Starts a thread that creates a websites thumbnail.
      /// </summary>
      /// <returns>A <see cref="Bitmap"/> with the websites thumbnail</returns>
      public Bitmap GenerateWebSiteThumbnailImage()
      {
        Thread m_thread = new Thread(new ThreadStart(this._GenerateWebSiteThumbnailImage));
        m_thread.SetApartmentState(ApartmentState.STA);
        m_thread.Start();
        m_thread.Join();
        return this.WebSiteImage;
      }

      /// <summary>
      /// Starts a thread that creates a websites thumbnail.
      /// </summary>
      /// <returns>A <see cref="Bitmap"/> with the websites thumbnail</returns>
      public Bitmap GenerateWebSiteScreenshot()
      {
        Thread m_thread = new Thread(new ThreadStart(this._GenerateWebSiteScreenshot));
        m_thread.SetApartmentState(ApartmentState.STA);
        m_thread.Start();
        m_thread.Join();
        return this.WebSiteImage;
      }

      /// <summary>
      /// Does the threads job on creating a thumbnail of the website.
      /// Waiting for the DocumentCompleted event.
      /// </summary>
      private void _GenerateWebSiteThumbnailImage()
      {
        WebBrowser webBrowser = new WebBrowser();
        webBrowser.ScrollBarsEnabled = false;
        webBrowser.Navigate(this.BaseUrl);
        if (this.NavigatingArgs != null)
        {
          while (webBrowser.ReadyState != WebBrowserReadyState.Complete)
          {
            Application.DoEvents();
          }

          webBrowser.Navigate(this.NavigatingArgs.Url, this.NavigatingArgs.TargetFrameName);
        }

        webBrowser.DocumentCompleted +=
          new WebBrowserDocumentCompletedEventHandler(this.WebBrowser_DocumentCompletedThumbnail);
        while (webBrowser.ReadyState != WebBrowserReadyState.Complete)
        {
          Application.DoEvents();
        }

        webBrowser.Dispose();
      }

      /// <summary>
      /// Does the threads job on creating a screenshot of the website.
      /// Waiting for the DocumentCompleted event.
      /// </summary>
      private void _GenerateWebSiteScreenshot()
      {
        WebBrowser webBrowser = new WebBrowser();
        webBrowser.ScrollBarsEnabled = true;
        webBrowser.Navigate(this.BaseUrl);
        if (this.NavigatingArgs != null)
        {
          webBrowser.Navigate(this.NavigatingArgs.Url, this.NavigatingArgs.TargetFrameName);
        }

        webBrowser.DocumentCompleted +=
          new WebBrowserDocumentCompletedEventHandler(this.WebBrowser_DocumentCompletedScreenshot);
        while (webBrowser.ReadyState != WebBrowserReadyState.Complete)
        {
          Application.DoEvents();
        }

        webBrowser.Dispose();
      }

      /// <summary>
      /// The <see cref="WebBrowser.DocumentCompleted"/> event handler.
      /// That takes a screenshot via DrawToBitmap and creates the thumbnail of it.
      /// </summary>
      /// <param name="sender">Source of the event</param>
      /// <param name="e">A <see cref="WebBrowserDocumentCompletedEventArgs"/> with the event data.</param>
      private void WebBrowser_DocumentCompletedThumbnail(object sender, WebBrowserDocumentCompletedEventArgs e)
      {
        WebBrowser webBrowser = (WebBrowser)sender;
        webBrowser.ClientSize = new Size(this.BrowserWidth, this.BrowserHeight);
        webBrowser.ScrollBarsEnabled = false;
        this.WebSiteImage = new Bitmap(webBrowser.Bounds.Width, webBrowser.Bounds.Height);
        webBrowser.BringToFront();
        webBrowser.DrawToBitmap(this.WebSiteImage, webBrowser.Bounds);
        this.WebSiteImage = (Bitmap)this.WebSiteImage.GetThumbnailImage(
          this.ThumbnailSize.Width,
          this.ThumbnailSize.Height,
          null,
          IntPtr.Zero);
      }

      /// <summary>
      /// The <see cref="WebBrowser.DocumentCompleted"/> event handler.
      /// That takes a full size screenshot including scroll rectangle
      /// via DrawToBitmap.
      /// </summary>
      /// <param name="sender">Source of the event</param>
      /// <param name="e">A <see cref="WebBrowserDocumentCompletedEventArgs"/> with the event data.</param>
      private void WebBrowser_DocumentCompletedScreenshot(object sender, WebBrowserDocumentCompletedEventArgs e)
      {
        WebBrowser webBrowser = (WebBrowser)sender;
        webBrowser.ClientSize = new Size(this.BrowserWidth, this.BrowserHeight);
        int htmlWidth = webBrowser.Document.GetElementsByTagName("HTML")[0].ScrollRectangle.Width;
        int htmlHeight = webBrowser.Document.GetElementsByTagName("HTML")[0].ScrollRectangle.Height;
        int scrollWidth = webBrowser.Document.Body.ScrollRectangle.Width;
        int scrollHeight = webBrowser.Document.Body.ScrollRectangle.Height;

        int width = this.BrowserWidth;
        int height = (int)Math.Max(htmlHeight, scrollHeight);

        webBrowser.ClientSize = new Size(width, height);
        webBrowser.ScrollBarsEnabled = true;
        this.WebSiteImage = new Bitmap(width, height);
        webBrowser.BringToFront();
        webBrowser.DrawToBitmap(
          this.WebSiteImage,
          new Rectangle(0, 0, width, height));
      }
    }
  }
}
