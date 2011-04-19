// <copyright file="VGBrowser.cs" company="FU Berlin">
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

namespace VectorGraphics.Elements
{
  using System;
  using System.ComponentModel;
  using System.Drawing;
  using System.Drawing.Drawing2D;
  using System.Text;
  using System.Windows.Forms;
  using System.Xml.Serialization;

  using Microsoft.VisualStudio.OLE.Interop;

  using VectorGraphics.Controls;

  /// <summary>
  /// Inherited from <see cref="VGElement"/>. 
  /// A serializable class that describes a <see cref="WebBrowser"/>
  /// element.
  /// </summary>
  [Serializable]
  public class VGBrowser : VGElement
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
    /// Saves the <see cref="Uri"/> that is the starting location
    /// for this browser object.
    /// </summary>
    private string browserURL;

    /// <summary>
    /// A <see cref="Matrix"/> with the current graphics transformation.
    /// </summary>
    private Matrix currentTransform;

    /// <summary>
    /// The <see cref="WebBrowser"/> control that is displayed via this class.
    /// </summary>
    private WebBrowser webBrowser;

    /// <summary>
    /// Indicates running disposal of this element.
    /// </summary>
    private bool disposing;

    /// <summary>
    /// A <see cref="FlashMessageFilter"/> that is inserted in the 
    /// message loop when the control is presented.
    /// </summary>
    private FlashMessageFilter messageFilter;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the VGBrowser class.
    /// </summary>
    /// <param name="newShapeDrawAction"><see cref="ShapeDrawAction"/> for the bounds.</param>
    /// <param name="newBrowserURL"><see cref="Uri"/> for the browser start location</param>
    /// <param name="newPen">Pen to use</param>
    /// <param name="newBrush">Brush for drawing</param>
    /// <param name="newFont">Font for drawing name</param>
    /// <param name="newFontColor">Font color for drawing name.</param>
    /// <param name="position">TopLeft text position</param>
    /// <param name="size">Size of the clipping rectangle.</param>
    /// <param name="newStyleGroup">Group Enumeration, <see cref="VGStyleGroup"/></param>
    /// <param name="newName">Name of Element</param>
    /// <param name="newElementGroup">Element group description</param>
    public VGBrowser(
      ShapeDrawAction newShapeDrawAction,
      string newBrowserURL,
      Pen newPen,
      Brush newBrush,
      Font newFont,
      Color newFontColor,
      PointF position,
      SizeF size,
      VGStyleGroup newStyleGroup,
      string newName,
      string newElementGroup)
      : base(
      newShapeDrawAction,
      newPen,
      newBrush,
      newFont,
      newFontColor,
      new RectangleF(position, size),
      newStyleGroup,
      newName,
      newElementGroup,
      null)
    {
      this.IntializeFields();
      this.browserURL = newBrowserURL;
    }

    /// <summary>
    /// Initializes a new instance of the VGBrowser class.
    /// Clone Constructor. Creates new VGBrowser that is
    /// identical to the given <see cref="VGBrowser"/>.
    /// </summary>
    /// <param name="oldBrowser">Browser element to clone.</param>
    private VGBrowser(VGBrowser oldBrowser)
      : base(
      oldBrowser.ShapeDrawAction,
      oldBrowser.Pen,
      oldBrowser.Brush,
      oldBrowser.Font,
      oldBrowser.FontColor,
      oldBrowser.Bounds,
      oldBrowser.StyleGroup,
      oldBrowser.Name,
      oldBrowser.ElementGroup,
      oldBrowser.Sound)
    {
      this.IntializeFields();
      this.browserURL = oldBrowser.BrowserURL;
    }

    /// <summary>
    /// Prevents a default instance of the VGBrowser class from being created.
    /// Parameterless constructor. Used for serialization.
    /// </summary>
    private VGBrowser()
    {
      this.IntializeFields();
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Enumerations                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region ENUMS

    /// <summary>
    /// The delegate for the thread-safe call to GetControlHandle
    /// </summary>
    /// <param name="control">The <see cref="Control"/> to receive the handle for.</param>
    /// <returns>An <see cref="IntPtr"/> with the controls handle.</returns>
    private delegate IntPtr HandleInvoker(Control control);

    /// <summary>
    /// The delegate for the thread-safe call to Control.Controls.Add(Control)
    /// </summary>
    /// <param name="control">The <see cref="Control"/> to be added to the controls container.</param>
    private delegate void AddDelegate(Control control);

    /// <summary>
    /// The delegate for the thread-safe call to Control.Controls.Remove(Control)
    /// </summary>
    /// <param name="control">The <see cref="Control"/> to be removed from the controls container.</param>
    private delegate void RemoveDelegate(Control control);

    /// <summary>
    /// The delegate for the thread-safe call to Control.SetBounds(...)
    /// </summary>
    /// <param name="x">The new Left property value of the control. </param>
    /// <param name="y">The new Top property value of the control.</param>
    /// <param name="width">The new Width property value of the control.</param>
    /// <param name="height">The new Height property value of the control.</param>
    private delegate void SetBoundsDelegate(int x, int y, int width, int height);

    #endregion ENUMS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets the underlying <see cref="WebBrowser"/> control.
    /// </summary>
    /// <value>A <see cref="WebBrowser"/> control that contains the ActiveX control.</value>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [XmlIgnore()]
    public WebBrowser WebBrowser
    {
      get { return this.webBrowser; }
    }

    /// <summary>
    /// Gets or sets the <see cref="Uri"/> for the browser.
    /// </summary>
    /// <value>A <see cref="String"/> with the starting location for the browser.</value>
    [Category("Content")]
    [Description("The Uri for the browsing start location.")]
    public string BrowserURL
    {
      get { return this.browserURL; }
      set { this.browserURL = value; }
    }

    /// <summary>
    /// Gets or sets the bounding rectangle of this
    /// <see cref="VGFlash"/>. Setting this property has no effect, 
    /// because the control is always full sized
    /// </summary>
    public override RectangleF Bounds
    {
      get
      {
        return base.Bounds;
      }

      set
      {
      }
    }

    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Public methods                                                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region PUBLICMETHODS

    /// <summary>
    /// This method loads the given url into the browser control, adds the 
    /// web browser control to the controls
    /// controls list.
    /// </summary>
    /// <param name="control">The <see cref="Control"/> this web browser object
    /// should be hosted on.</param>
    /// <param name="recreateBrowser"><strong>True</strong>,
    /// if this function is called from separate thread to avoid error.</param>
    public void InitializeOnControl(Control control, bool recreateBrowser)
    {
      if (recreateBrowser)
      {
        if (this.webBrowser != null)
        {
          if (this.webBrowser.InvokeRequired)
          {
            this.webBrowser.Invoke(new MethodInvoker(this.webBrowser.Dispose));
          }
          else
          {
            this.webBrowser.Dispose();
          }
        }

        this.webBrowser = new WebBrowser();
      }

      this.webBrowser.Dock = DockStyle.Fill;

      if (control.InvokeRequired)
      {
        control.Invoke(new AddDelegate(control.Controls.Add), this.webBrowser);
      }
      else
      {
        control.Controls.Add(this.webBrowser);
      }

      // Put it in the foreground
      control.Controls.SetChildIndex(this.webBrowser, 0);

      this.webBrowser.Url = new Uri(this.browserURL);
      this.webBrowser.ScriptErrorsSuppressed = true;
    }

    /// <summary>
    /// This message adds or removes a <see cref="FlashMessageFilter"/>
    /// to or from the applications message pump to ensure the duplication
    /// of events captured by the activeX are notified also by the parent form.
    /// </summary>
    /// <param name="sendFlashMessagesToParent"><strong>True</strong>,
    /// if message filter should be added, <strong>false</strong>,
    /// if it should be removed.</param>
    public void SendMessagesToParent(bool sendFlashMessagesToParent)
    {
      // This message filter ensures the notification of
      // key and mouse events that the flash object receives
      // for recording
      if (sendFlashMessagesToParent)
      {
        this.messageFilter = new FlashMessageFilter(this.webBrowser, this.webBrowser.Parent);
        Application.AddMessageFilter(this.messageFilter);
      }
      else if (this.messageFilter != null)
      {
        Application.RemoveMessageFilter(this.messageFilter);
        this.messageFilter = null;
      }
    }

    #endregion //PUBLICMETHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Inherited methods                                                         //
    ///////////////////////////////////////////////////////////////////////////////
    #region OVERRIDES

    /// <summary>
    /// Overridden <see cref="VGElement.Draw(Graphics)"/>.  
    /// Draws text with the owning brush and font onto given 
    /// graphics context.
    /// </summary>
    /// <param name="graphics">Graphics context to draw on.</param>
    /// <exception cref="ArgumentNullException">Thrown, when graphics object is null.</exception>
    public override void Draw(Graphics graphics)
    {
      if (graphics == null)
      {
        throw new ArgumentNullException("Graphics object should not be null.");
      }

      // this.DrawFlashObject(graphics);

      // Draw name and selection frame if applicable
      base.Draw(graphics);
    }

    /// <summary>
    /// Overridden <see cref="VGElement.Reset()"/>. 
    /// Resets the current text element to
    /// default values (empty instruction).
    /// </summary>
    public override void Reset()
    {
      base.Reset();
      this.webBrowser.Url = new Uri("about:blank");
    }

    /// <summary>
    /// Overridden <see cref="Object.ToString()"/> method.
    /// Returns the <see cref="VGText"/> properties as a human readable string.
    /// </summary>
    /// <returns>A <see cref="string"/> that represents this <see cref="VGText"/>.</returns>
    public override string ToString()
    {
      StringBuilder sb = new StringBuilder();
      sb.Append("VGBrowser, Name: ");
      sb.Append(Name);
      sb.Append(" ; '");
      sb.Append(this.browserURL.ToString());
      return sb.ToString();
    }

    /// <summary>
    /// Overridden <see cref="VGElement.ToShortString()"/> method.
    /// Returns the main <see cref="VGText"/> properties as a human readable string.
    /// </summary>
    /// <returns>A <see cref="string"/> that represents the 
    /// current <see cref="VGText"/> in short form with its main properties.</returns>
    public override string ToShortString()
    {
      StringBuilder sb = new StringBuilder();
      sb.Append("Web browser: ");
      string text = this.browserURL.ToString();
      sb.Append(text.Substring(0, text.Length > 12 ? Math.Max(12, text.Length - 1) : text.Length));
      sb.Append(" ...");
      return sb.ToString();
    }

    /// <summary>
    /// Releases the resources used by the element.
    /// </summary>
    public override void Dispose()
    {
      this.disposing = true;
      base.Dispose();

      if (this.webBrowser.Parent != null)
      {
        if (this.webBrowser.Parent.InvokeRequired)
        {
          this.webBrowser.Parent.Invoke(new RemoveDelegate(this.webBrowser.Parent.Controls.Remove), this.webBrowser);
        }
        else
        {
          this.webBrowser.Parent.Controls.Remove(this.webBrowser);
        }
      }

      // Do not call webbrowser.dispose, because you will receive
      // RCW errors. Let garbage collector do the job.
    }

    /// <summary>
    /// Overridden <see cref="VGElement.CloneCore()"/>.  
    /// Creates an excact copy of given <see cref="VGBrowser"/>.
    /// </summary>
    /// <returns>Excact copy of this web browser element.</returns>
    protected override VGElement CloneCore()
    {
      return new VGBrowser(this);
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
    #region METHODS

    /// <summary>
    /// Returns the given controls handle.
    /// </summary>
    /// <param name="control">The <see cref="Control"/> to receive the handle for.</param>
    /// <returns>An <see cref="IntPtr"/> with the controls handle.</returns>
    private IntPtr GetControlHandle(Control control)
    {
      return control.Handle;
    }

    /// <summary>
    /// This method is the kernel of this class and draws the flash activeX
    /// com object surface in the current state to the given <see cref="Graphics"/>.
    /// </summary>
    /// <remarks>Note that the <see cref="IViewObject"/> is the key to
    /// provide us with a method to draw a com object on a graphics, without beeing visible itself.</remarks>
    /// <param name="graphics">The <see cref="Graphics"/> to draw to.</param>
    private void DrawFlashObject(Graphics graphics)
    {
      // Sanity check
      if (this.webBrowser == null)
      {
        return;
      }

      // Some painting calls can occur when this object is beeing disposed.
      if (this.disposing)
      {
        return;
      }

      // Grab IViewObject interface from the ocx.  
      Interfaces.IViewObject viewObject =
        (Interfaces.IViewObject)this.webBrowser.ActiveXInstance;

      // Check for success
      if (viewObject == null)
      {
        return;
      }

      int hr = -1;

      // Set up RECTL structure
      RECTL bounds = new RECTL();
      bounds.left = 0;
      bounds.top = 0;
      bounds.right = this.webBrowser.Right;
      bounds.bottom = this.webBrowser.Bottom;

      // get hdc
      IntPtr hdc = graphics.GetHdc();

      // Draw
      hr = viewObject.Draw(
        DVASPECT.DVASPECT_CONTENT,
        -1,
        IntPtr.Zero,
        IntPtr.Zero,
        IntPtr.Zero,
        hdc,
        ref bounds,
        ref bounds,
        IntPtr.Zero,
        (uint)0);

      // Release HDC
      graphics.ReleaseHdc(hdc);
    }

    /// <summary>
    /// This method initializes member of this class.
    /// </summary>
    private void IntializeFields()
    {
      this.webBrowser = new WebBrowser();
      this.currentTransform = new Matrix();
      this.browserURL = "about:blank";
      this.disposing = false;
      this.webBrowser.Url = new Uri(this.browserURL);
    }

    #endregion //METHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER
    #endregion //HELPER
  }
}