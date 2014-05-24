// <copyright file="VGFlash.cs" company="FU Berlin">
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

namespace VectorGraphics.Elements
{
  using System;
  using System.ComponentModel;
  using System.Drawing;
  using System.Drawing.Drawing2D;
  using System.IO;
  using System.Text;
  using System.Windows.Forms;
  using System.Xml.Serialization;

  using Microsoft.VisualStudio.OLE.Interop;

  using VectorGraphics.Controls.Flash;

  /// <summary>
  /// Inherited from <see cref="VGElement"/>. 
  /// A serializable class that is a vector graphics text,
  /// drawn with a specific font and font color.
  /// </summary>
  [Serializable]
  public class VGFlash : VGElement
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
    /// The <see cref="AxFlashControl"/> that is the ActiveX object that is
    /// represented by this <see cref="VGFlash"/>
    /// </summary>
    private AxFlashControl flashControl;

    /// <summary>
    /// A <see cref="FlashMessageFilter"/> that is inserted in the 
    /// message loop when the control is presented.
    /// </summary>
    private FlashMessageFilter messageFilter;

    /// <summary>
    /// A <see cref="FlashMouseLeaveMessageFilter"/> that is inserted in the 
    /// message loop when the control is presented.
    /// </summary>
    private FlashMouseLeaveMessageFilter messageFilterForMouseLeaveEvents;

    /// <summary>
    /// Indicates running disposal of this element.
    /// </summary>
    private bool disposing;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the VGFlash class.
    /// </summary>
    /// <param name="newShapeDrawAction"><see cref="ShapeDrawAction"/> for the bounds.</param>
    /// <param name="newFilename">Filename of the flash movie without path</param>
    /// <param name="newPath">Path to the flash movie.</param>
    /// <param name="newPen">Pen to use</param>
    /// <param name="newBrush">Brush for drawing</param>
    /// <param name="newFont">Font for drawing name</param>
    /// <param name="newFontColor">Font color for drawing name.</param>
    /// <param name="position">TopLeft text position</param>
    /// <param name="size">Size of the clipping rectangle.</param>
    /// <param name="newStyleGroup">Group Enumeration, <see cref="VGStyleGroup"/></param>
    /// <param name="newName">Name of Element</param>
    /// <param name="newElementGroup">Element group description</param>
    public VGFlash(
      ShapeDrawAction newShapeDrawAction,
      string newFilename,
      string newPath,
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
      this.Filepath = newPath;
      this.Filename = newFilename;
    }

    /// <summary>
    /// Initializes a new instance of the VGFlash class.
    /// Clone Constructor. Creates new text that is
    /// identical to the given <see cref="VGText"/>.
    /// </summary>
    /// <param name="oldFlash">Text element to clone.</param>
    private VGFlash(VGFlash oldFlash)
      : base(
      oldFlash.ShapeDrawAction,
      oldFlash.Pen,
      oldFlash.Brush,
      oldFlash.Font,
      oldFlash.FontColor,
      oldFlash.Bounds,
      oldFlash.StyleGroup,
      oldFlash.Name,
      oldFlash.ElementGroup,
      oldFlash.Sound)
    {
      this.IntializeFields();
      this.Filename = oldFlash.Filename;
      this.Filepath = oldFlash.Filepath;
    }

    /// <summary>
    /// Prevents a default instance of the VGFlash class from being created.
    /// Parameterless constructor. Used for serialization.
    /// </summary>
    private VGFlash()
    {
      this.IntializeFields();
    }

    /// <summary>
    /// Finalizes an instance of the VGFlash class.
    /// Removes <see cref="FlashMessageFilter"/> from message stack.
    /// </summary>
    ~VGFlash()
    {
      this.RemoveMessageFilter();
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
    /// Gets the <see cref="IntPtr"/> that contains the handle of the <see cref="AxFlashControl"/>
    /// window surface, that is used to draw the activeX object.
    /// </summary>
    public IntPtr HandleToSurface
    {
      get { return this.flashControl.HandleToSurface; }
    }

    /// <summary>
    /// Gets or sets the filename for the flash movie.
    /// </summary>
    /// <value>A <see cref="string"/> with the filename for the flash movie.</value>
    [Category("Content"), Description("The file name without path to the flash movie (.swf) file.")]
    public string Filename { get; set; }

    /// <summary>
    /// Gets or sets the filenames path for this flash movie.
    /// </summary>
    /// <value>A <see cref="string"/> with the flash movies path.</value>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false), XmlIgnore]
    public string Filepath { get; set; }

    /// <summary>
    /// Gets the audio filename with path.
    /// </summary>
    [XmlIgnore]
    public string FullFilename
    {
      get { return Path.Combine(this.Filepath, this.Filename); }
    }

    /// <summary>
    /// Gets or sets the bounding rectangle of this
    /// <see cref="VGFlash"/>. When setting, the underlying flashcontrol is resized.
    /// </summary>
    public override RectangleF Bounds
    {
      get
      {
        return base.Bounds;
      }

      set
      {
        UpdateBoundsWithoutRaisingNewPosition(value);

        if (this.flashControl != null)
        {
          Rectangle newBounds = GetTransformedBounds(this.currentTransform, this.Bounds);
          if (this.flashControl.InvokeRequired)
          {
            SetBoundsDelegate setBoundsMethod = new SetBoundsDelegate(this.flashControl.SetBounds);
            this.flashControl.Invoke(setBoundsMethod, newBounds.X, newBounds.Y, newBounds.Width, newBounds.Height);
          }
          else
          {
            this.flashControl.Bounds = newBounds;
          }
        }
      }
    }

    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Public methods                                                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region PUBLICMETHODS

    /// <summary>
    /// This method returns the given rectangle transformed by the given
    /// <see cref="Matrix"/>.
    /// </summary>
    /// <param name="transform">The <see cref="Matrix"/> that defines the transformation.</param>
    /// <param name="activeXBounds">A <see cref="RectangleF"/> with the untransformed bounds
    /// of the flash object.</param>
    /// <returns>A <see cref="Rectangle"/> with the transformed rectangle.</returns>
    public static Rectangle GetTransformedBounds(Matrix transform, RectangleF activeXBounds)
    {
      Rectangle newBounds = new Rectangle();
      float posX = transform.Elements[4];
      float posY = transform.Elements[5];
      float factorX = transform.Elements[0];
      float factorY = transform.Elements[3];

      if (factorX != 0 && factorY != 0)
      {
        newBounds.X = (int)Math.Ceiling(activeXBounds.X * factorX + posX);
        newBounds.Y = (int)Math.Ceiling(activeXBounds.Y * factorY + posY);
        newBounds.Width = (int)Math.Ceiling(activeXBounds.Width * factorX);
        newBounds.Height = (int)Math.Ceiling(activeXBounds.Height * factorY);
      }

      return newBounds;
    }

    /// <summary>
    /// This method loads the movie, adds the flashobject to the controls
    /// controls list and puts a message filter into the Application
    /// message stack to push all messages sent to the flash object 
    /// also to the owning parent.
    /// </summary>
    /// <param name="control">The <see cref="Control"/> this flash object
    /// should be hosted on.</param>
    /// <param name="recreateFlash"><strong>True</strong>,
    /// if this function is called from separate thread to avoid error.</param>
    /// <param name="graphicsTransform">A <see cref="Matrix"/> with the
    /// transform of the canvas to display this activeX object.</param>
    public void InitializeOnControl(Control control, bool recreateFlash, Matrix graphicsTransform)
    {
      if (recreateFlash)
      {
        if (this.flashControl != null)
        {
          this.flashControl.Dispose();
        }

        this.flashControl = new AxFlashControl();
      }

      this.flashControl.Bounds = GetTransformedBounds(graphicsTransform, this.Bounds);

      if (control.InvokeRequired)
      {
        control.Invoke(new AddDelegate(control.Controls.Add), this.flashControl);
      }
      else
      {
        control.Controls.Add(this.flashControl);
      }

      this.flashControl.Visible = true;

      // Put it in the background
      control.Controls.SetChildIndex(this.flashControl, control.Controls.Count - 1);

      this.LoadMovie();

      // Initialize controls surface handle
      this.flashControl.CreateWindowHandle();
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
        this.messageFilter = new FlashMessageFilter(this.flashControl, this.flashControl.Parent);
        Application.AddMessageFilter(this.messageFilter);
      }
      else if (this.messageFilter != null)
      {
        Application.RemoveMessageFilter(this.messageFilter);
        this.messageFilter = null;
      }
    }

    /// <summary>
    /// Stops the running movie
    /// </summary>
    public void Stop()
    {
      this.flashControl.Stop();
    }

    /// <summary>
    /// Start playing the animation
    /// </summary>
    public void Play()
    {
      this.flashControl.Play();
    }

    /// <summary>
    /// Resets the flash movie by loading an empty movie and reloading the
    /// correct one. Rewinding does not work.
    /// </summary>
    public void ResetMovie()
    {
      // Unload and reload movie
      this.flashControl.LoadMovie(0, " ");
      this.LoadMovie();
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

      RectangleF innerBounds = this.DrawFillAndEdge(graphics);

      if (this.currentTransform != graphics.Transform)
      {
        this.currentTransform = graphics.Transform;
        if (this.flashControl != null)
        {
          Rectangle newBounds = GetTransformedBounds(this.currentTransform, innerBounds);
          if (this.flashControl.InvokeRequired)
          {
            SetBoundsDelegate setBoundsMethod = new SetBoundsDelegate(this.flashControl.SetBounds);
            this.flashControl.Invoke(setBoundsMethod, newBounds.X, newBounds.Y, newBounds.Width, newBounds.Height);
          }
          else
          {
            this.flashControl.Bounds = newBounds;
          }
        }
      }

      this.DrawFlashObject(graphics, innerBounds);

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
      this.flashControl.Stop();
      this.flashControl.Rewind();
      this.RemoveMessageFilter();
    }

    /// <summary>
    /// Overridden <see cref="Object.ToString()"/> method.
    /// Returns the <see cref="VGText"/> properties as a human readable string.
    /// </summary>
    /// <returns>A <see cref="string"/> that represents this <see cref="VGText"/>.</returns>
    public override string ToString()
    {
      StringBuilder sb = new StringBuilder();
      sb.Append("VGFlash, Name: ");
      sb.Append(Name);
      sb.Append(" ; '");
      sb.Append(this.Filename);
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
      sb.Append("Flash movie ");
      string text = this.Filename;
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

      if (this.flashControl.Parent != null)
      {
        if (this.flashControl.Parent.InvokeRequired)
        {
          this.flashControl.Parent.Invoke(new RemoveDelegate(this.flashControl.Parent.Controls.Remove), this.flashControl);
        }
        else
        {
          this.flashControl.Parent.Controls.Remove(this.flashControl);
        }
      }

      if (this.flashControl != null)
      {
        try
        {
          this.flashControl.Dispose();
        }
        catch (Exception)
        {
        }
      }

      this.RemoveMessageFilter();
    }

    /// <summary>
    /// Overridden <see cref="VGElement.CloneCore()"/>.  
    /// Creates an excact copy of given <see cref="VGRichText"/>.
    /// </summary>
    /// <returns>Excact copy of this text element.</returns>
    protected override VGElement CloneCore()
    {
      return new VGFlash(this);
    }

    /// <summary>
    /// Overridden <see cref="VGElement.NewPosition(Matrix)"/>.
    /// Updates the bounds of the underlying <see cref="AxFlashControl"/>
    /// when bounds are changing to have correct sized drawing during
    /// call to <see cref="DrawFlashObject(Graphics, RectangleF)"/> 
    /// </summary>
    /// <param name="translationMatrix">The <see cref="Matrix"/> with the new transformation.</param>
    protected override void NewPosition(Matrix translationMatrix)
    {
      // This does the job, because resizing is done in the Bounds property.
      this.Bounds = this.Bounds;
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
    /// <param name="elementBounds">The <see cref="RectangleF"/> to draw into.</param>
    private void DrawFlashObject(Graphics graphics, RectangleF elementBounds)
    {
      // Sanity check
      if (this.flashControl == null)
      {
        return;
      }

      // Some painting calls can occur when this object is beeing disposed.
      if (this.disposing)
      {
        return;
      }

      // Transform size and location with the current transform
      PointF newSize = new PointF(elementBounds.Width, elementBounds.Height);

      PointF[] mousePts = { newSize, elementBounds.Location };
      this.currentTransform.TransformPoints(mousePts);
      Point scaledSize = Point.Round(mousePts[0]);
      Point scaledLocation = Point.Round(mousePts[1]);

      // Grab IViewObject interface from the ocx.  
      Tools.Interfaces.IViewObject viewObject =
        (Tools.Interfaces.IViewObject)this.flashControl.GetOcx();

      // Check for success
      if (viewObject == null)
      {
        return;
      }

      int hr = -1;

      // Set up RECTL structure
      RECTL bounds = new RECTL();
      bounds.left = scaledLocation.X;
      bounds.top = scaledLocation.Y;
      bounds.right = scaledLocation.X + scaledSize.X;
      bounds.bottom = scaledLocation.Y + scaledSize.Y;

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
      this.flashControl = new AxFlashControl();
      this.currentTransform = new Matrix();
      this.Filepath = string.Empty;
      this.Filename = string.Empty;
      this.disposing = false;
    }

    /// <summary>
    /// This method loads the movie with the file name define 
    /// in the member fields into the flashControl if the file exists.
    /// </summary>
    private void LoadMovie()
    {
      if (File.Exists(this.FullFilename))
      {
        this.flashControl.LoadMovie(0, this.FullFilename);
        this.flashControl.Stop();
      }
    }

    /// <summary>
    /// This method removes the current <see cref="FlashMessageFilter"/> 
    /// from the message loop.
    /// </summary>
    private void RemoveMessageFilter()
    {
      if (this.messageFilter != null)
      {
        Application.RemoveMessageFilter(this.messageFilter);
        this.messageFilter = null;
      }

      if (this.messageFilterForMouseLeaveEvents != null)
      {
        Application.RemoveMessageFilter(this.messageFilterForMouseLeaveEvents);
        this.messageFilterForMouseLeaveEvents = null;
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