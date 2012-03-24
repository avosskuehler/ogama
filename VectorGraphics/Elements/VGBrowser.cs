// <copyright file="VGBrowser.cs" company="FU Berlin">
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

namespace VectorGraphics.Elements
{
  using System;
  using System.ComponentModel;
  using System.Drawing;
  using System.Drawing.Drawing2D;
  using System.Text;
  using System.Windows.Forms;
  using System.Xml.Serialization;
  
  using VectorGraphics.Controls;
  using VectorGraphics.Controls.Flash;

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
    /// A <see cref="Matrix"/> with the current graphics transformation.
    /// </summary>
    private Matrix currentTransform;

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
    /// <param name="newBrowseDepth">The number of links the user is allowed to follow,
    /// including backward links.</param>
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
      int newBrowseDepth,
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
      this.InitializeFields();
      this.BrowserURL = newBrowserURL;
      this.BrowseDepth = newBrowseDepth;
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
      this.InitializeFields();
      this.BrowserURL = oldBrowser.BrowserURL;
      this.BrowseDepth = oldBrowser.BrowseDepth;
    }

    /// <summary>
    /// Prevents a default instance of the VGBrowser class from being created.
    /// Parameterless constructor. Used for serialization.
    /// </summary>
    private VGBrowser()
    {
      this.InitializeFields();
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Enumerations                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region ENUMS

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

    #endregion ENUMS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets or sets the underlying <see cref="WebBrowser"/> control.
    /// </summary>
    /// <value>A <see cref="WebBrowser"/> control that contains the ActiveX control.</value>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false), XmlIgnore]
    public WebBrowser WebBrowser { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="Uri"/> for the browser.
    /// </summary>
    /// <value>A <see cref="String"/> with the starting location for the browser.</value>
    [Category("Content"), Description("The Uri for the browsing start location.")]
    public string BrowserURL { get; set; }

    /// <summary>
    /// Gets or sets the number of links the user is allowed to follow,
    /// including backward links
    /// </summary>
    /// <value>A <see cref="Int32"/> with the number of links the user is allowed to follow,
    /// including backward links.</value>
    [Category("Content"), Description("The number of links the user is allowed to follow, including backward links.")]
    public int BrowseDepth { get; set; }

    /// <summary>
    /// Gets or sets the bounding rectangle of this
    /// <see cref="VGBrowser"/>. Setting this property has no effect, 
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
        if (this.WebBrowser != null)
        {
          if (this.WebBrowser.InvokeRequired)
          {
            this.WebBrowser.Invoke(new MethodInvoker(this.WebBrowser.Dispose));
          }
          else
          {
            this.WebBrowser.Dispose();
          }
        }

        this.WebBrowser = new WebBrowser();
      }

      this.WebBrowser.Dock = DockStyle.Fill;

      if (control.InvokeRequired)
      {
        control.Invoke(new AddDelegate(control.Controls.Add), this.WebBrowser);
      }
      else
      {
        control.Controls.Add(this.WebBrowser);
      }

      // Put it in the foreground
      control.Controls.SetChildIndex(this.WebBrowser, 0);

      this.WebBrowser.Url = new Uri(this.BrowserURL);
      this.WebBrowser.ScriptErrorsSuppressed = true;
      if (this.BrowseDepth == 0)
      {
        this.WebBrowser.AllowNavigation = false;
      }
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
        this.messageFilter = new FlashMessageFilter(this.WebBrowser, this.WebBrowser.Parent);
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
    /// Draws webbrowser onto given graphics context. 
    /// This method does only basic drawing, because the
    /// webbrowser is only used during recording, afterwards the screenshots
    /// are used. In recording mode, there will be the activex control on top, 
    /// so no gdi drawing.
    /// </summary>
    /// <param name="graphics">Graphics context to draw on.</param>
    /// <exception cref="ArgumentNullException">Thrown, when graphics object is null.</exception>
    public override void Draw(Graphics graphics)
    {
      if (graphics == null)
      {
        throw new ArgumentNullException("Graphics object should not be null.");
      }

      // Draw name and selection frame if applicable
      base.Draw(graphics);
    }

    /// <summary>
    /// Overridden <see cref="VGElement.Reset()"/>. 
    /// Resets the current browser element to
    /// default values (empty url).
    /// </summary>
    public override void Reset()
    {
      base.Reset();
      this.WebBrowser.Url = new Uri("about:blank");
    }

    /// <summary>
    /// Overridden <see cref="Object.ToString()"/> method.
    /// Returns the <see cref="VGBrowser"/> properties as a human readable string.
    /// </summary>
    /// <returns>A <see cref="string"/> that represents this <see cref="VGBrowser"/>.</returns>
    public override string ToString()
    {
      StringBuilder sb = new StringBuilder();
      sb.Append("VGBrowser, Name: ");
      sb.Append(Name);
      sb.Append(" ; '");
      sb.Append(this.BrowserURL.ToString());
      return sb.ToString();
    }

    /// <summary>
    /// Overridden <see cref="VGElement.ToShortString()"/> method.
    /// Returns the main <see cref="VGBrowser"/> properties as a human readable string.
    /// </summary>
    /// <returns>A <see cref="string"/> that represents the 
    /// current <see cref="VGBrowser"/> in short form with its main properties.</returns>
    public override string ToShortString()
    {
      StringBuilder sb = new StringBuilder();
      sb.Append("Web browser: ");
      string text = this.BrowserURL.ToString();
      sb.Append(text.Substring(0, text.Length > 12 ? Math.Max(12, text.Length - 1) : text.Length));
      sb.Append(" ...");
      return sb.ToString();
    }

    /// <summary>
    /// Releases the resources used by the element.
    /// </summary>
    public override void Dispose()
    {
      base.Dispose();

      if (this.WebBrowser.Parent != null)
      {
        if (this.WebBrowser.Parent.InvokeRequired)
        {
          this.WebBrowser.Parent.Invoke(new RemoveDelegate(this.WebBrowser.Parent.Controls.Remove), this.WebBrowser);
        }
        else
        {
          this.WebBrowser.Parent.Controls.Remove(this.WebBrowser);
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
    /// This method initializes member of this class.
    /// </summary>
    private void InitializeFields()
    {
      this.WebBrowser = new WebBrowser();
      this.currentTransform = new Matrix();
      this.BrowserURL = "about:blank";
      this.BrowseDepth = 0;
      this.WebBrowser.Url = new Uri(this.BrowserURL);
    }

    #endregion //METHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER
    #endregion //HELPER
  }
}