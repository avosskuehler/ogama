// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WebsiteScreenshot.cs" company="Freie Universität Berlin">
//   OGAMA - open gaze and mouse analyzer 
//   Copyright (C) 2014 Dr. Adrian Voßkühler  
//   Licensed under GPL V3
// </copyright>
// <author>Adrian Voßkühler</author>
// <email>adrian@ogama.net</email>
// <summary>
//   This class creates screenshots of websites.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Ogama.Modules.Recording.Presenter
{
  using System;
  using System.Drawing;
  using System.Drawing.Imaging;
  using System.IO;
  using System.Windows.Forms;

  using Microsoft.VisualStudio.OLE.Interop;

  using Ogama.ExceptionHandling;

  using IViewObject = VectorGraphics.Tools.Interfaces.IViewObject;

  /// <summary>
  ///   This class creates screenshots of websites.
  /// </summary>
  public class WebsiteScreenshot
  {
    #region Static Fields

    /// <summary>
    ///   The new screenshot filename.
    /// </summary>
    private static string newScreenshotFilename;

    /// <summary>
    ///   The new target url.
    /// </summary>
    private static Uri newTargetUrl;

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// This method creates a new form with a webbrowser of correct size on it,
    /// to make a fullsize website screenhot.
    /// </summary>
    /// <param name="navigatingArgs">The <see cref="WebBrowserNavigatingEventArgs"/> instance containing the event data,
    /// especially the url.</param>
    /// <param name="filename">The filename to save the screenshot to.</param>
    public static void DoScreenshot(WebBrowserNavigatingEventArgs navigatingArgs, string filename)
    {
      newTargetUrl = navigatingArgs.Url;
      newScreenshotFilename = filename;

      var tempBrowser = new WebBrowser();
      var dummyForm = new Form { ClientSize = new Size(1, 1), FormBorderStyle = FormBorderStyle.None };

      dummyForm.Controls.Add(tempBrowser);
      dummyForm.Show();
      tempBrowser.ScrollBarsEnabled = true;
      tempBrowser.ScriptErrorsSuppressed = true;
      tempBrowser.DocumentCompleted += WebBrowserDocumentCompleted;

      if (navigatingArgs.TargetFrameName != string.Empty)
      {
        tempBrowser.Navigate(navigatingArgs.Url, navigatingArgs.TargetFrameName);
      }
      else
      {
        tempBrowser.Navigate(newTargetUrl);
      }

      while (tempBrowser.ReadyState != WebBrowserReadyState.Complete)
      {
        Application.DoEvents();
      }

      tempBrowser.Dispose();
    }

    #endregion

    #region Methods

    /// <summary>
    /// This method iterates recursively through the <see cref="HtmlWindowCollection"/>
    ///   of a webbrowser document get the maximal scroll size of the containing frames.
    /// </summary>
    /// <param name="htmlWindows">
    /// The first <see cref="HtmlWindowCollection"/>
    ///   to start parsing. You get it from browser.Document.Window.Frames
    /// </param>
    /// <param name="currentScrollsize">
    /// Ref. A <see cref="Size"/> to be updated with
    ///   maximal scroll size values.
    /// </param>
    private static void GetLargestScrollSizeOfAllFrames(HtmlWindowCollection htmlWindows, ref Size currentScrollsize)
    {
      foreach (HtmlWindow window in htmlWindows)
      {
        try
        {
          HtmlDocument document = window.Document;
          if (document == null)
          {
            continue;
          }

          GetMaxScrollSizeOfDocument(document, ref currentScrollsize);
          if (document.Window != null)
          {
            GetLargestScrollSizeOfAllFrames(document.Window.Frames, ref currentScrollsize);
          }
        }
        catch (UnauthorizedAccessException ex)
        {
          ExceptionMethods.HandleExceptionSilent(ex);
        }
      }
    }

    /// <summary>
    /// Static method to parse the current <see cref="HtmlDocument"/> of the
    ///   <see cref="WebBrowser"/> for its maximal scrollsize.
    ///   Returns the maximum of the given scrollsize and the documents scrollsize.
    ///   So be sure to reset current srollsize first.
    /// </summary>
    /// <param name="document"> The <see cref="HtmlDocument"/> to check its maximal size.
    /// </param>
    /// <param name="currentScrollsize"> Ref. A <see cref="Size"/> to be updated with
    ///   maximal scroll size values.
    /// </param>
    private static void GetMaxScrollSizeOfDocument(HtmlDocument document, ref Size currentScrollsize)
    {
      if (document.GetElementsByTagName("HTML").Count > 0 && document.Body != null)
      {
        int htmlWidth = document.GetElementsByTagName("HTML")[0].ScrollRectangle.Width;
        int htmlHeight = document.GetElementsByTagName("HTML")[0].ScrollRectangle.Height;
        if (document.Body != null)
        {
          int scrollWidth = document.Body.ScrollRectangle.Width;
          int scrollHeight = document.Body.ScrollRectangle.Height;

          int maxWidth = Math.Max(htmlWidth, scrollWidth);
          int maxHeight = Math.Max(htmlHeight, scrollHeight);

          currentScrollsize.Width = Math.Max(currentScrollsize.Width, maxWidth);
          currentScrollsize.Height = Math.Max(currentScrollsize.Height, maxHeight);
        }
      }
    }

    /// <summary>
    /// The web browser document completed event handler which creates the screenshot for
    /// the correct URL.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="WebBrowserDocumentCompletedEventArgs"/> instance containing the event data.</param>
    private static void WebBrowserDocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
    {
      if (e.Url == newTargetUrl)
      {
        var localBrowser = (WebBrowser)sender;

        if (localBrowser.Document == null)
        {
          return;
        }

        try
        {
          // Reset window to default size
          Size scrollSize = Document.ActiveDocument.PresentationSize;
          
          // ! This line is critical to receive correct scroll sizes
          // in the next step
          localBrowser.ClientSize = Document.ActiveDocument.PresentationSize;

          // Get maximal sizes
          GetMaxScrollSizeOfDocument(localBrowser.Document, ref scrollSize);
          if (localBrowser.Document.Window != null)
          {
            GetLargestScrollSizeOfAllFrames(localBrowser.Document.Window.Frames, ref scrollSize);
          }

          // Now update the clientsize to full size, not screen size
          localBrowser.ClientSize = scrollSize;

          // create a bitmap object 
          var bitmap = new Bitmap(scrollSize.Width, scrollSize.Height);

          // get the viewobject of the WebBrowser to draw to bitmap
          var ivo = localBrowser.Document.DomDocument as IViewObject;

          using (Graphics g = Graphics.FromImage(bitmap))
          {
            // get the handle to the device context and draw
            IntPtr hdc = g.GetHdc();

            if (ivo != null)
            {
              // get the size of the document's body
              var docRectangle = new RECTL { top = 0, left = 0, right = scrollSize.Width, bottom = scrollSize.Height };

              ivo.Draw(
                DVASPECT.DVASPECT_CONTENT, 
                -1, 
                IntPtr.Zero, 
                IntPtr.Zero, 
                IntPtr.Zero, 
                hdc, 
                ref docRectangle, 
                ref docRectangle, 
                IntPtr.Zero, 
                0);
            }

            g.ReleaseHdc(hdc);
          }

          // Alternative to IViewObject, not allowed but works...
          //// localBrowser.DrawToBitmap(bitmap, new Rectangle(0, 0, scrollSize.Width, scrollSize.Height));
          
          string filename = Path.Combine(
            Document.ActiveDocument.ExperimentSettings.SlideResourcesPath, 
            newScreenshotFilename);
          bitmap.Save(filename, ImageFormat.Png);
          bitmap.Dispose();
        }
        catch (Exception ex)
        {
          ExceptionMethods.HandleException(ex);
        }
      }
    }

    #endregion

    // TODO: Maybe of use for the unauthorized exception which is currently ignored
    ////public static dynamic GetDocumentFromWindow(IHTMLWindow2 htmlWindow)
    ////{
    ////  if (htmlWindow == null)
    ////  {
    ////    return null;
    ////  }

    ////  // First try the usual way to get the document.
    ////  try
    ////  {
    ////    IHTMLDocument2 doc = htmlWindow.document;

    ////    return doc;
    ////  }
    ////  catch (COMException comEx)
    ////  {
    ////    // I think COMException won't be ever fired but just to be sure ...
    ////    if (comEx.ErrorCode != E_ACCESSDENIED)
    ////    {
    ////      return null;
    ////    }
    ////  }
    ////  catch (System.UnauthorizedAccessException)
    ////  {
    ////  }
    ////  catch
    ////  {
    ////    // Any other error.
    ////    return null;
    ////  }

    ////  // At this point the error was E_ACCESSDENIED because the frame contains a document from another domain.
    ////  // IE tries to prevent a cross frame scripting security issue.
    ////  try
    ////  {
    ////    // Convert IHTMLWindow2 to IWebBrowser2 using IServiceProvider.
    ////    IServiceProvider sp = (IServiceProvider)htmlWindow;

    ////    // Use IServiceProvider.QueryService to get IWebBrowser2 object.
    ////    Object brws = null;
    ////    sp.QueryService(ref IID_IWebBrowserApp, ref IID_IWebBrowser2, out brws);

    ////    // Get the document from IWebBrowser2.
    ////    IWebBrowser2 browser = (IWebBrowser2)(brws);

    ////    return browser.Document;
    ////  }
    ////  catch
    ////  {
    ////  }

    ////  return null;
    ////}

    ////private const int E_ACCESSDENIED = unchecked((int)0x80070005L);

    ////private static Guid IID_IWebBrowserApp = new Guid("0002DF05-0000-0000-C000-000000000046");

    ////private static Guid IID_IWebBrowser2 = new Guid("D30C1661-CDAF-11D0-8A3E-00C04FC9E26E");

    ////// This is the COM IServiceProvider interface, not System.IServiceProvider .Net interface!

    ////[ComImport(), ComVisible(true), Guid("6D5140C1-7436-11CE-8034-00AA006009FA"),
    //// InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    ////public interface IServiceProvider
    ////{
    ////  [return: MarshalAs(UnmanagedType.I4)]
    ////  [PreserveSig]
    ////  int QueryService(ref Guid guidService, ref Guid riid, [MarshalAs(UnmanagedType.Interface)] out object ppvObject);
    ////}
  }
}