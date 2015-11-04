// <copyright file="AxFlash.cs" company="FU Berlin">
// ******************************************************
// OGAMA - open gaze and mouse analyzer 
// Copyright (C) 2015 Dr. Adrian Voßkühler  
// ------------------------------------------------------------------------
// This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
// This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
// You should have received a copy of the GNU General Public License along with this program; if not, write to the Free Software Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
// **************************************************************
// </copyright>
// <author>Adrian Voßkühler</author>
// <email>adrian@ogama.net</email>

namespace VectorGraphics.Controls.Flash
{
  using System;
  using System.ComponentModel;
  using System.Runtime.InteropServices;
  using System.Windows.Forms;

  using ShockwaveFlashObjects;

  /// <summary>
  /// Derives from <see cref="AxHost"/>.
  /// This class is auto generated with the tool aximp.exe from the flash10a.ocx.
  /// Adobe does not document the Com object very well, so docu is omitted
  /// </summary>
  [Clsid("{d27cdb6e-ae6d-11cf-96b8-444553540000}")]
  [DesignTimeVisible(true)]
  public class AxFlash : AxHost
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
    /// The <see cref="IShockwaveFlash"/> ocx object.
    /// </summary>
    private IShockwaveFlash ocx;

    /// <summary>
    /// The <see cref="AxFlashEventMulticaster"/> event multicaster.
    /// </summary>
    private AxFlashEventMulticaster eventMulticaster;

    /// <summary>
    /// The <see cref="AxHost.ConnectionPointCookie"/> cookie.
    /// </summary>
    private AxHost.ConnectionPointCookie cookie;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the AxFlash class.
    /// </summary>
    public AxFlash()
      : base("d27cdb6e-ae6d-11cf-96b8-444553540000")
    {
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Enumerations                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region ENUMS

    /// <summary>
    /// Generated when the ready state of the control changes. 
    /// The possible states are 0=Loading, 1=Uninitialized, 2=Loaded, 3=Interactive, 4=Complete.
    /// </summary>
    public event AxFlashReadyStateChangeEventHandler OnReadyStateChange;

    /// <summary>
    /// Generated as the Flash Player movie is downloading.
    /// </summary>
    public event AxFlashProgressEventHandler OnProgress;

    /// <summary>
    /// This event is generated when a GetURL action is performed 
    /// in the movie with a URL and the URL starts with "FSCommand:". 
    /// The portion of the URL after the : is provided in command and 
    /// the target is provided in args. This can be used to create a 
    /// response to a frame or button action in the Shockwave Flash movie.
    /// </summary>
    public event AxFlashFSCommandEventHandler FSCommand;

    /// <summary>
    /// Called when you receive a FlashCall.
    /// </summary>
    public event AxFlashCallEventHandler FlashCall;

    #endregion ENUMS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets the state of the Flash movie. Use to check for availability. 
    /// 0=Loading, 1=Uninitialized, 2=Loaded, 3=Interactive, 4=Complete.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DispId(-525)]
    public virtual int ReadyState
    {
      get
      {
        if (this.ocx == null)
        {
          throw new AxHost.InvalidActiveXStateException("ReadyState", AxHost.ActiveXInvokeKind.PropertyGet);
        }

        return this.ocx.ReadyState;
      }
    }

    /// <summary>
    /// Gets the total number of frames in the movie. This is not available until the movie has loaded. 
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DispId(124)]
    public virtual int TotalFrames
    {
      get
      {
        if (this.ocx == null)
        {
          throw new AxHost.InvalidActiveXStateException("TotalFrames", AxHost.ActiveXInvokeKind.PropertyGet);
        }

        return this.ocx.TotalFrames;
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the movie is playing. 
    /// Returns true if the movie is currently playing, false if it is paused.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DispId(125)]
    public virtual bool Playing
    {
      get
      {
        if (this.ocx == null)
        {
          throw new AxHost.InvalidActiveXStateException("Playing", AxHost.ActiveXInvokeKind.PropertyGet);
        }

        return this.ocx.Playing;
      }

      set
      {
        if (this.ocx == null)
        {
          throw new AxHost.InvalidActiveXStateException("Playing", AxHost.ActiveXInvokeKind.PropertySet);
        }

        this.ocx.Playing = value;
      }
    }

    /// <summary>
    /// Gets or sets the current rendering quality (0=Low, 1=High, 2=AutoLow, 3=AutoHigh).
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DispId(105)]
    public virtual int Quality
    {
      get
      {
        if (this.ocx == null)
        {
          throw new AxHost.InvalidActiveXStateException("Quality", AxHost.ActiveXInvokeKind.PropertyGet);
        }

        return this.ocx.Quality;
      }

      set
      {
        if (this.ocx == null)
        {
          throw new AxHost.InvalidActiveXStateException("Quality", AxHost.ActiveXInvokeKind.PropertySet);
        }

        this.ocx.Quality = value;
      }
    }

    /// <summary>
    /// Gets or sets the scale mode for the Flash movie 
    /// (0=ShowAll, 1= NoBorder, 2 = ExactFit)
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DispId(120)]
    public virtual int ScaleMode
    {
      get
      {
        if (this.ocx == null)
        {
          throw new AxHost.InvalidActiveXStateException("ScaleMode", AxHost.ActiveXInvokeKind.PropertyGet);
        }

        return this.ocx.ScaleMode;
      }

      set
      {
        if (this.ocx == null)
        {
          throw new AxHost.InvalidActiveXStateException("ScaleMode", AxHost.ActiveXInvokeKind.PropertySet);
        }

        this.ocx.ScaleMode = value;
      }
    }

    /// <summary>
    /// Gets or sets the align mode of bit flags. 
    /// (Left=+1, Right=+2, Top=+4, Bottom=+8). 
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DispId(121)]
    public virtual int AlignMode
    {
      get
      {
        if (this.ocx == null)
        {
          throw new AxHost.InvalidActiveXStateException("AlignMode", AxHost.ActiveXInvokeKind.PropertyGet);
        }

        return this.ocx.AlignMode;
      }

      set
      {
        if (this.ocx == null)
        {
          throw new AxHost.InvalidActiveXStateException("AlignMode", AxHost.ActiveXInvokeKind.PropertySet);
        }

        this.ocx.AlignMode = value;
      }
    }

    /// <summary>
    /// Gets or sets the BackgroundColor.
    /// Override the background color of a movie. 
    /// An integer of the form red*65536+green*256+blue use -1 for the default movie color.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DispId(123)]
    public virtual int BackgroundColor
    {
      get
      {
        if (this.ocx == null)
        {
          throw new AxHost.InvalidActiveXStateException("BackgroundColor", AxHost.ActiveXInvokeKind.PropertyGet);
        }

        return this.ocx.BackgroundColor;
      }

      set
      {
        if (this.ocx == null)
        {
          throw new AxHost.InvalidActiveXStateException("BackgroundColor", AxHost.ActiveXInvokeKind.PropertySet);
        }

        this.ocx.BackgroundColor = value;
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the loop state of the Flash movie.
    /// Returns true if the movie loops, false to play once. 
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DispId(106)]
    public virtual bool Loop
    {
      get
      {
        if (this.ocx == null)
        {
          throw new AxHost.InvalidActiveXStateException("Loop", AxHost.ActiveXInvokeKind.PropertyGet);
        }

        return this.ocx.Loop;
      }

      set
      {
        if (this.ocx == null)
        {
          throw new AxHost.InvalidActiveXStateException("Loop", AxHost.ActiveXInvokeKind.PropertySet);
        }

        this.ocx.Loop = value;
      }
    }

    /// <summary>
    /// Gets or sets the URL source for the movie file. 
    /// Setting this property will load a new movie.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DispId(102)]
    public virtual string Movie
    {
      get
      {
        if (this.ocx == null)
        {
          throw new AxHost.InvalidActiveXStateException("Movie", AxHost.ActiveXInvokeKind.PropertyGet);
        }

        return this.ocx.Movie;
      }

      set
      {
        if (this.ocx == null)
        {
          throw new AxHost.InvalidActiveXStateException("Movie", AxHost.ActiveXInvokeKind.PropertySet);
        }

        this.ocx.Movie = value;
      }
    }

    /// <summary>
    /// Gets or sets the currently displayed frame of the movie. 
    /// Setting this property advances or rewinds the movie.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DispId(107)]
    public virtual int FrameNum
    {
      get
      {
        if (this.ocx == null)
        {
          throw new AxHost.InvalidActiveXStateException("FrameNum", AxHost.ActiveXInvokeKind.PropertyGet);
        }

        return this.ocx.FrameNum;
      }

      set
      {
        if (this.ocx == null)
        {
          throw new AxHost.InvalidActiveXStateException("FrameNum", AxHost.ActiveXInvokeKind.PropertySet);
        }

        this.ocx.FrameNum = value;
      }
    }

    /// <summary>
    /// Gets or sets the Window Mode property of the Flash movie for transparency, 
    /// layering, and positioning in the browser.
    /// Possible values: window, opaque, transparent. 
    /// * window - movie plays in its own rectangular window on a web page.
    /// * opaque - the movie hides everything on the page behind it.
    /// * transparent - the background of the HTML page shows through all 
    /// transparent portions of the movie, this may slow animation performance. 
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DispId(133)]
    public virtual string WMode
    {
      get
      {
        if (this.ocx == null)
        {
          throw new AxHost.InvalidActiveXStateException("WMode", AxHost.ActiveXInvokeKind.PropertyGet);
        }

        return this.ocx.WMode;
      }

      set
      {
        if (this.ocx == null)
        {
          throw new AxHost.InvalidActiveXStateException("WMode", AxHost.ActiveXInvokeKind.PropertySet);
        }

        this.ocx.WMode = value;
      }
    }

    /// <summary>
    /// Gets or sets the align mode.
    /// Possible values: l, t, r, b, tl, tr, bl, br.
    /// * l, r, t, and b align the movie along the left, right,
    /// top or bottom edge, respectively, of the browser 
    /// window and crop the remaining three sides as needed.
    /// * tl and tr align the movie to the top left and top 
    /// right corner, respectively, of the browser window and 
    /// crop the bottom and remaining right or left side as needed.
    /// * bl and br align the movie to the bottom left and bottom right 
    /// corner, respectively, of the browser window and crop the
    /// top and remaining right or left side as needed. 
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DispId(134)]
    public virtual string SAlign
    {
      get
      {
        if (this.ocx == null)
        {
          throw new AxHost.InvalidActiveXStateException("SAlign", AxHost.ActiveXInvokeKind.PropertyGet);
        }

        return this.ocx.SAlign;
      }

      set
      {
        if (this.ocx == null)
        {
          throw new AxHost.InvalidActiveXStateException("SAlign", AxHost.ActiveXInvokeKind.PropertySet);
        }

        this.ocx.SAlign = value;
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the menu is visible.
    /// Possible values: true, false.
    /// * true displays the full menu, allowing the user a 
    /// variety of options to enhance or control playback.
    /// * false displays a menu that contains only the 
    /// Settings option and the About Flash option. 
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DispId(135)]
    public virtual bool Menu
    {
      get
      {
        if (this.ocx == null)
        {
          throw new AxHost.InvalidActiveXStateException("Menu", AxHost.ActiveXInvokeKind.PropertyGet);
        }

        return this.ocx.Menu;
      }

      set
      {
        if (this.ocx == null)
        {
          throw new AxHost.InvalidActiveXStateException("Menu", AxHost.ActiveXInvokeKind.PropertySet);
        }

        this.ocx.Menu = value;
      }
    }

    /// <summary>
    /// Gets or sets the base directory.[base directory] or [URL]. 
    /// Specifies the base directory or URL used to resolve all 
    /// relative path statements in the Flash Player movie. This 
    /// attribute is helpful when your Flash Player movies are kept 
    /// in a different directory from your other files.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DispId(136)]
    public virtual string Base
    {
      get
      {
        if (this.ocx == null)
        {
          throw new AxHost.InvalidActiveXStateException("Base", AxHost.ActiveXInvokeKind.PropertyGet);
        }

        return this.ocx.Base;
      }

      set
      {
        if (this.ocx == null)
        {
          throw new AxHost.InvalidActiveXStateException("Base", AxHost.ActiveXInvokeKind.PropertySet);
        }

        this.ocx.Base = value;
      }
    }

    /// <summary>
    /// Gets or sets the control scale mode.
    /// Possible values: showall, noborder, exactfit.
    /// * default (Show all) makes the entire movie visible in the 
    /// specified area without distortion, while maintaining the 
    /// original aspect ratio of the movie. Borders may appear on two sides of the movie.
    /// * noorder scales the movie to fill the specified area, without 
    /// distortion but possibly with some cropping, while maintaining 
    /// the original aspect ratio of the movie.
    /// * exactfit makes the entire movie visible in the specified 
    /// area without trying to preserve the original aspect ratio. Distortion may occur. 
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DispId(137)]
    public virtual string CtlScale
    {
      get
      {
        if (this.ocx == null)
        {
          throw new AxHost.InvalidActiveXStateException("CtlScale", AxHost.ActiveXInvokeKind.PropertyGet);
        }

        return this.ocx.Scale;
      }

      set
      {
        if (this.ocx == null)
        {
          throw new AxHost.InvalidActiveXStateException("CtlScale", AxHost.ActiveXInvokeKind.PropertySet);
        }

        this.ocx.Scale = value;
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether DeviceFonts are used.
    /// can be true or false indicating whethert to use device font or not.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DispId(138)]
    public virtual bool DeviceFont
    {
      get
      {
        if (this.ocx == null)
        {
          throw new AxHost.InvalidActiveXStateException("DeviceFont", AxHost.ActiveXInvokeKind.PropertyGet);
        }

        return this.ocx.DeviceFont;
      }

      set
      {
        if (this.ocx == null)
        {
          throw new AxHost.InvalidActiveXStateException("DeviceFont", AxHost.ActiveXInvokeKind.PropertySet);
        }

        this.ocx.DeviceFont = value;
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether movie should be embedded.
    /// Can be true or false indicating whether to embed the movie in the player or not.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DispId(139)]
    public virtual bool EmbedMovie
    {
      get
      {
        if (this.ocx == null)
        {
          throw new AxHost.InvalidActiveXStateException("EmbedMovie", AxHost.ActiveXInvokeKind.PropertyGet);
        }

        return this.ocx.EmbedMovie;
      }

      set
      {
        if (this.ocx == null)
        {
          throw new AxHost.InvalidActiveXStateException("EmbedMovie", AxHost.ActiveXInvokeKind.PropertySet);
        }

        this.ocx.EmbedMovie = value;
      }
    }

    /// <summary>
    /// Gets or sets BGColor property.
    /// Override the background color of a movie. 
    /// [hexadecimal RGB value] in the format #RRGGBB . 
    /// Specifies the background color of the movie. Use this attribute to 
    /// override the background color setting specified in the 
    /// Flash file. This attribute does not affect the background color of the HTML page.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DispId(140)]
    public virtual string BGColor
    {
      get
      {
        if (this.ocx == null)
        {
          throw new AxHost.InvalidActiveXStateException("BGColor", AxHost.ActiveXInvokeKind.PropertyGet);
        }

        return this.ocx.BGColor;
      }

      set
      {
        if (this.ocx == null)
        {
          throw new AxHost.InvalidActiveXStateException("BGColor", AxHost.ActiveXInvokeKind.PropertySet);
        }

        this.ocx.BGColor = value;
      }
    }

    /// <summary>
    /// Gets or sets the Quality2 property.
    /// </summary>
    /// <seealso cref="Quality"/>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DispId(141)]
    public virtual string Quality2
    {
      get
      {
        if (this.ocx == null)
        {
          throw new AxHost.InvalidActiveXStateException("Quality2", AxHost.ActiveXInvokeKind.PropertyGet);
        }

        return this.ocx.Quality2;
      }

      set
      {
        if (this.ocx == null)
        {
          throw new AxHost.InvalidActiveXStateException("Quality2", AxHost.ActiveXInvokeKind.PropertySet);
        }

        this.ocx.Quality2 = value;
      }
    }

    /// <summary>
    /// Gets or sets the Shockmachine parameters.
    /// To specify an  swRemote  parameter, use a structure like this: "NAME='VALUE' NAME='VALUE'.
    /// -> swPicon: A fully qualified URL to a picon BMP or GIF file.
    /// The image should be a 64 x 64 pixels, 8-bit image and use the Web system palette.
    /// If a picon is not specified, one will be generated automatically, 
    /// but Macromedia strongly recommends that you provide custom art.
    /// -> swPackage: A fully qualified or relative URL (relative to the primary movie) 
    /// of a package text file. This text file, in XML format, provides a list of URLs 
    /// of all the files that need to be downloaded for a complete save of this piece.
    /// For more information about package files, see Saving titles with multiple movies
    /// or external media.
    /// -> swTitle: A text string that provides the Shockwave movie's title; 
    /// this title appears in the Name field of the Save to Shockmachine dialog
    /// box when a user saves your movie to Shockmachine. The text string can include spaces.
    /// If you do not provide a title, one is generated based on the movie's 
    /// file name, but Macromedia strongly recommends that you provide a title.
    /// -> swTotalSize: For content with packages (see swPackage , above), specifies
    /// the size, in bytes, of all the content that needs to be downloaded for the 
    /// piece to save successfully. The size should include the movie currently being 
    /// saved, even though it's already in the cache. If nothing is specified, an 
    /// estimate is calculated based on URLs already downloaded.
    /// -> swCategory Specifies the category that will appear in the Category 
    /// field of the Save to Shockmachine dialog box when a user saves your movie to Shockmachine.
    /// If a category is not specified or is unavailable, the category that 
    /// comes first alphabetically will be chosen.
    /// -> swSendUrl: Specifies the URL to be sent when the user clicks the 
    /// Shockmachine Send button. Typically, the URL is the location of the 
    /// Web page that contains your movie.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DispId(159)]
    public virtual string SWRemote
    {
      get
      {
        if (this.ocx == null)
        {
          throw new AxHost.InvalidActiveXStateException("SWRemote", AxHost.ActiveXInvokeKind.PropertyGet);
        }

        return this.ocx.SWRemote;
      }

      set
      {
        if (this.ocx == null)
        {
          throw new AxHost.InvalidActiveXStateException("SWRemote", AxHost.ActiveXInvokeKind.PropertySet);
        }

        this.ocx.SWRemote = value;
      }
    }

    /// <summary>
    /// Gets or sets FlashVars property.
    /// Possible values: variable to pass to Flash Player. Requires Macromedia Flash Player 6 or later.
    /// * Used to send root level variables to the movie. The format 
    /// of the string is a set of name=value combinations separated by AND.
    /// * Browsers will support string sizes of up to 64KB (65535 bytes) in length.
    /// * For more information on FlashVars, please refer to Using FlashVars 
    /// to pass variables to a SWF (TechNote 16417). 
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DispId(170)]
    public virtual string FlashVars
    {
      get
      {
        if (this.ocx == null)
        {
          throw new AxHost.InvalidActiveXStateException("FlashVars", AxHost.ActiveXInvokeKind.PropertyGet);
        }

        return this.ocx.FlashVars;
      }

      set
      {
        if (this.ocx == null)
        {
          throw new AxHost.InvalidActiveXStateException("FlashVars", AxHost.ActiveXInvokeKind.PropertySet);
        }

        this.ocx.FlashVars = value;
      }
    }

    /// <summary>
    /// Gets or sets AllowScriptAccess property.
    /// AllowScriptAccesscan have two possible values: "always" and "never":
    /// When AllowScriptAccess is "never", outbound scripting will always fail.
    /// When AllowScriptAccess is "always", outbound scripting will always succeed.
    /// IfAllowScriptAccess is not specified by an HTML page, it defaults to "always".
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DispId(171)]
    public virtual string AllowScriptAccess
    {
      get
      {
        if (this.ocx == null)
        {
          throw new AxHost.InvalidActiveXStateException("AllowScriptAccess", AxHost.ActiveXInvokeKind.PropertyGet);
        }

        return this.ocx.AllowScriptAccess;
      }

      set
      {
        if (this.ocx == null)
        {
          throw new AxHost.InvalidActiveXStateException("AllowScriptAccess", AxHost.ActiveXInvokeKind.PropertySet);
        }

        this.ocx.AllowScriptAccess = value;
      }
    }

    /// <summary>
    /// Gets or sets additinal movie data.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DispId(190)]
    public virtual string MovieData
    {
      get
      {
        if (this.ocx == null)
        {
          throw new AxHost.InvalidActiveXStateException("MovieData", AxHost.ActiveXInvokeKind.PropertyGet);
        }

        return this.ocx.MovieData;
      }

      set
      {
        if (this.ocx == null)
        {
          throw new AxHost.InvalidActiveXStateException("MovieData", AxHost.ActiveXInvokeKind.PropertySet);
        }

        this.ocx.MovieData = value;
      }
    }

    /// <summary>
    /// Gets or sets the InlineData property.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DispId(191)]
    public virtual object InlineData
    {
      get
      {
        if (this.ocx == null)
        {
          throw new AxHost.InvalidActiveXStateException("InlineData", AxHost.ActiveXInvokeKind.PropertyGet);
        }

        return this.ocx.InlineData;
      }

      set
      {
        if (this.ocx == null)
        {
          throw new AxHost.InvalidActiveXStateException("InlineData", AxHost.ActiveXInvokeKind.PropertySet);
        }

        this.ocx.InlineData = value;
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to use seamless tabbing or not.
    /// The default value is true; set this parameter to false 
    /// to disable "seamless tabbing", which allows users to use the 
    /// Tab key to move keyboard focus out of a Flash movie and into the 
    /// surrounding HTML (or the browser, if there is nothing focusable 
    /// in the HTML following the Flash movie).
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DispId(192)]
    public virtual bool SeamlessTabbing
    {
      get
      {
        if (this.ocx == null)
        {
          throw new AxHost.InvalidActiveXStateException("SeamlessTabbing", AxHost.ActiveXInvokeKind.PropertyGet);
        }

        return this.ocx.SeamlessTabbing;
      }

      set
      {
        if (this.ocx == null)
        {
          throw new AxHost.InvalidActiveXStateException("SeamlessTabbing", AxHost.ActiveXInvokeKind.PropertySet);
        }

        this.ocx.SeamlessTabbing = value;
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to use a profile or not.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DispId(194)]
    public virtual bool Profile
    {
      get
      {
        if (this.ocx == null)
        {
          throw new AxHost.InvalidActiveXStateException("Profile", AxHost.ActiveXInvokeKind.PropertyGet);
        }

        return this.ocx.Profile;
      }

      set
      {
        if (this.ocx == null)
        {
          throw new AxHost.InvalidActiveXStateException("Profile", AxHost.ActiveXInvokeKind.PropertySet);
        }

        this.ocx.Profile = value;
      }
    }

    /// <summary>
    /// Gets or sets ProfileAddress property.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DispId(195)]
    public virtual string ProfileAddress
    {
      get
      {
        if (this.ocx == null)
        {
          throw new AxHost.InvalidActiveXStateException("ProfileAddress", AxHost.ActiveXInvokeKind.PropertyGet);
        }

        return this.ocx.ProfileAddress;
      }

      set
      {
        if (this.ocx == null)
        {
          throw new AxHost.InvalidActiveXStateException("ProfileAddress", AxHost.ActiveXInvokeKind.PropertySet);
        }

        this.ocx.ProfileAddress = value;
      }
    }

    /// <summary>
    /// Gets or sets ProfilePort property.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DispId(196)]
    public virtual int ProfilePort
    {
      get
      {
        if (this.ocx == null)
        {
          throw new AxHost.InvalidActiveXStateException("ProfilePort", AxHost.ActiveXInvokeKind.PropertyGet);
        }

        return this.ocx.ProfilePort;
      }

      set
      {
        if (this.ocx == null)
        {
          throw new AxHost.InvalidActiveXStateException("ProfilePort", AxHost.ActiveXInvokeKind.PropertySet);
        }

        this.ocx.ProfilePort = value;
      }
    }

    /// <summary>
    /// Gets or sets AllowNetworking property.
    /// "allowNetworking" affects the ability of a SWF to perform network I/O, 
    /// either via the browser (opening new windows, etc.) or directly 
    /// using Flash networking APIs. This implicitly may also restrict 
    /// scripting access to the browser, as you cannot prevent network 
    /// I/O without prohibiting access to the browser's JavaScript DOM.
    /// In order of least to most restrictive, you can specify:
    /// "allowScriptAccess=always": This permits the SWF to call arbitrary 
    /// browser JavaScript at all times, even if the SWF came from 
    /// another domain. This is generally not safe to do unless you 
    /// completely trust the SWF you are loading (and any children SWFs it may subsequently load).
    /// "allowNetworking=all": All normal network I/O is allowed 
    /// (as permitted by the Flash Player security model).
    /// "allowScriptAccess=sameDomain": This permits the SWF to call
    /// into the browser's JavaScript DOM only if the SWF came from 
    /// the same domain as the HTML hosting it. This is equivalent to the 
    /// typical browser "same origin policy" model.
    /// "allowScriptAccess=never": The SWF is never permitted to call into 
    /// the browser's JavaScript, even if it came from the same domain as
    /// its HTML container. You can use this tag if you host SWFs in the
    /// same domain as the HTML, but don't trust the SWFs to interact with
    /// the surrounding HTML, cookies, etc. In particular this setting will
    /// also prevent the SWF from modifying or redirecting existing frames
    /// windows. However, if you really don't trust the SWF you may need 
    /// some stronger medicine.
    /// "allowNetworking=internal": Everything with "allowScriptAccess=never" 
    /// applies, and also prevents the SWF from opening new browser windows, 
    /// modifying existing ones, or otherwise affecting any browser state. 
    /// The SWF can still use internal networking ActionScript APIs like 
    /// loadMovie(), XML.load(), LoadVars, etc.
    /// Finally, "allowNetworking=none" prohibits any browser or 
    /// network interaction. This means that the SWF cannot do much 
    /// more than interact with the assets it contains internally,
    /// and cannot do anything to influence the browser, or load 
    /// or send any data over the network.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DispId(201)]
    public virtual string AllowNetworking
    {
      get
      {
        if (this.ocx == null)
        {
          throw new AxHost.InvalidActiveXStateException("AllowNetworking", AxHost.ActiveXInvokeKind.PropertyGet);
        }

        return this.ocx.AllowNetworking;
      }

      set
      {
        if (this.ocx == null)
        {
          throw new AxHost.InvalidActiveXStateException("AllowNetworking", AxHost.ActiveXInvokeKind.PropertySet);
        }

        this.ocx.AllowNetworking = value;
      }
    }

    /// <summary>
    /// Gets or sets AllowFullScreen property.
    /// Can be "true" or "false".
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DispId(202)]
    public virtual string AllowFullScreen
    {
      get
      {
        if (this.ocx == null)
        {
          throw new AxHost.InvalidActiveXStateException("AllowFullScreen", AxHost.ActiveXInvokeKind.PropertyGet);
        }

        return this.ocx.AllowFullScreen;
      }

      set
      {
        if (this.ocx == null)
        {
          throw new AxHost.InvalidActiveXStateException("AllowFullScreen", AxHost.ActiveXInvokeKind.PropertySet);
        }

        this.ocx.AllowFullScreen = value;
      }
    }

    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Public methods                                                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region PUBLICMETHODS

    /// <summary>
    /// In the Timeline indicated by target, executes the action in the 
    /// frame specified by the label frame label. The argument 
    /// type for both arguments is string.
    /// </summary>
    /// <example>This example runs the actions in the frame labeled HandleScriptNotify in the main timeline:
    /// movie.TCallLabel("/", "HandleScriptNotify");</example>
    /// <param name="target">A <see cref="String"/> with the target.</param>
    /// <param name="label">A <see cref="String"/> with the label of the target.</param>
    public virtual void TCallLabel(string target, string label)
    {
      if (this.ocx == null)
      {
        throw new AxHost.InvalidActiveXStateException("TCallLabel", AxHost.ActiveXInvokeKind.MethodInvoke);
      }

      this.ocx.TCallLabel(target, label);
    }

    /// <summary>
    /// For the timeline indicated by target, sets the value of the property 
    /// specified by property to the value specified by value, which can be a 
    /// number. For property, enter the integer corresponding to the desired 
    /// property.
    /// </summary>
    /// <param name="target">The <see cref="String"/> with the target name.</param>
    /// <param name="property">The <see cref="Int32"/> with the number of the property.</param>
    /// <param name="value">The <see cref="double"/> with the value to set.</param>
    public virtual void TSetPropertyNum(string target, int property, double value)
    {
      if (this.ocx == null)
      {
        throw new AxHost.InvalidActiveXStateException("TSetPropertyNum", AxHost.ActiveXInvokeKind.MethodInvoke);
      }

      this.ocx.TSetPropertyNum(target, property, value);
    }

    /// <summary>
    /// For the timeline indicated by target, returns a number indicating 
    /// the value of the property specified by property. For property,
    /// enter the integer corresponding to the desired property. 
    /// </summary>
    /// <param name="target">The <see cref="String"/> with the target name.</param>
    /// <param name="property">The <see cref="Int32"/> with the number of the property.</param>
    /// <returns>The <see cref="double"/> with the value to retrieve.</returns>
    public virtual double TGetPropertyNum(string target, int property)
    {
      if (this.ocx == null)
      {
        throw new AxHost.InvalidActiveXStateException("TGetPropertyNum", AxHost.ActiveXInvokeKind.MethodInvoke);
      }

      double returnValue = (double)this.ocx.TGetPropertyNum(target, property);
      return returnValue;
    }

    /// <summary>
    /// For the timeline indicated by target, returns a number indicating 
    /// the value of the property specified by property. For property,
    /// enter the integer corresponding to the desired property. 
    /// </summary>
    /// <param name="target">The <see cref="String"/> with the target name.</param>
    /// <param name="property">The <see cref="Int32"/> with the number of the property.</param>
    /// <returns>The <see cref="double"/> with the value to retrieve.</returns>
    public virtual double TGetPropertyAsNumber(string target, int property)
    {
      if (this.ocx == null)
      {
        throw new AxHost.InvalidActiveXStateException("TGetPropertyAsNumber", AxHost.ActiveXInvokeKind.MethodInvoke);
      }

      double returnValue = (double)this.ocx.TGetPropertyAsNumber(target, property);
      return returnValue;
    }

    /// <summary>
    /// Does what the name says.
    /// </summary>
    public virtual void EnforceLocalSecurity()
    {
      if (this.ocx == null)
      {
        throw new AxHost.InvalidActiveXStateException("EnforceLocalSecurity", AxHost.ActiveXInvokeKind.MethodInvoke);
      }

      this.ocx.EnforceLocalSecurity();
    }

    /// <summary>
    /// Calls the callback function with the given xml formatted request.
    /// </summary>
    /// <param name="request">An xml formatted <see cref="String"/></param>
    /// <returns>The return value of the function if there is any.</returns>
    public virtual string CallFunction(string request)
    {
      if (this.ocx == null)
      {
        throw new AxHost.InvalidActiveXStateException("CallFunction", AxHost.ActiveXInvokeKind.MethodInvoke);
      }

      string returnValue = (string)this.ocx.CallFunction(request);
      return returnValue;
    }

    /// <summary>
    /// Return a value to ActionScript; 
    /// the returned value must be encoded as an XML-formatted string. 
    /// </summary>
    /// <param name="returnValue">An xml formatted <see cref="String"/></param>
    public virtual void SetReturnValue(string returnValue)
    {
      if (this.ocx == null)
      {
        throw new AxHost.InvalidActiveXStateException("SetReturnValue", AxHost.ActiveXInvokeKind.MethodInvoke);
      }

      this.ocx.SetReturnValue(returnValue);
    }

    /// <summary>
    /// Does what the name says.
    /// </summary>
    public virtual void DisableLocalSecurity()
    {
      if (this.ocx == null)
      {
        throw new AxHost.InvalidActiveXStateException("DisableLocalSecurity", AxHost.ActiveXInvokeKind.MethodInvoke);
      }

      this.ocx.DisableLocalSecurity();
    }

    /// <summary>
    /// Zoom in on a rectangular area of the movie. 
    /// Note that the units of the coordinates are in twips (1440 units per inch). 
    /// To calculate a rectangle in Flash, set the ruler units to Points and 
    /// multiply the coordinates by 20 to get TWIPS.
    /// </summary>
    /// <param name="left">X- Value of the top left corner.</param>
    /// <param name="top">Y- Value of the top left corner.</param>
    /// <param name="right">X- Value of the bottom right corner.</param>
    /// <param name="bottom">Y- Value of the bottom right corner.</param>
    public virtual void SetZoomRect(int left, int top, int right, int bottom)
    {
      if (this.ocx == null)
      {
        throw new AxHost.InvalidActiveXStateException("SetZoomRect", AxHost.ActiveXInvokeKind.MethodInvoke);
      }

      this.ocx.SetZoomRect(left, top, right, bottom);
    }

    /// <summary>
    /// Zoom the view by a relative scale factor. 
    /// Zoom(50) will double the size of the objects in the view. 
    /// Zoom(200) will reduce the size of objects in the view by one half.
    /// </summary>
    /// <param name="factor">Indicates the zoom factor (in percent)</param>
    public virtual void Zoom(int factor)
    {
      if (this.ocx == null)
      {
        throw new AxHost.InvalidActiveXStateException("Zoom", AxHost.ActiveXInvokeKind.MethodInvoke);
      }

      this.ocx.Zoom(factor);
    }

    /// <summary>
    /// Pan a zoomed in movie. The mode can be: 0 = pixels, 1 = % of window.
    /// </summary>
    /// <param name="x">x-zoom factor</param>
    /// <param name="y">y-zoom factor</param>
    /// <param name="mode">0 = pixels, 1 = % of window.</param>
    public virtual void Pan(int x, int y, int mode)
    {
      if (this.ocx == null)
      {
        throw new AxHost.InvalidActiveXStateException("Pan", AxHost.ActiveXInvokeKind.MethodInvoke);
      }

      this.ocx.Pan(x, y, mode);
    }

    /// <summary>
    /// Start playing the animation.
    /// </summary>
    public virtual void Play()
    {
      if (this.ocx == null)
      {
        throw new AxHost.InvalidActiveXStateException("Play", AxHost.ActiveXInvokeKind.MethodInvoke);
      }

      this.ocx.Play();
    }

    /// <summary>
    /// Stop playing the animation.
    /// </summary>
    public virtual void Stop()
    {
      if (this.ocx == null)
      {
        throw new AxHost.InvalidActiveXStateException("Stop", AxHost.ActiveXInvokeKind.MethodInvoke);
      }

      this.ocx.Stop();
    }

    /// <summary>
    /// Go to the previous frame.
    /// </summary>
    public virtual void Back()
    {
      if (this.ocx == null)
      {
        throw new AxHost.InvalidActiveXStateException("Back", AxHost.ActiveXInvokeKind.MethodInvoke);
      }

      this.ocx.Back();
    }

    /// <summary>
    /// Go to the next frame.
    /// </summary>
    public virtual void Forward()
    {
      if (this.ocx == null)
      {
        throw new AxHost.InvalidActiveXStateException("Forward", AxHost.ActiveXInvokeKind.MethodInvoke);
      }

      this.ocx.Forward();
    }

    /// <summary>
    /// Go to the first frame.
    /// </summary>
    public virtual void Rewind()
    {
      if (this.ocx == null)
      {
        throw new AxHost.InvalidActiveXStateException("Rewind", AxHost.ActiveXInvokeKind.MethodInvoke);
      }

      this.ocx.Rewind();
    }

    /// <summary>
    /// Stops playing.
    /// </summary>
    public virtual void StopPlay()
    {
      if (this.ocx == null)
      {
        throw new AxHost.InvalidActiveXStateException("StopPlay", AxHost.ActiveXInvokeKind.MethodInvoke);
      }

      this.ocx.StopPlay();
    }

    /// <summary>
    /// Goto frame with the given number
    /// </summary>
    /// <param name="frameNum">An <see cref="Int32"/> with the frame to jump to.</param>
    public virtual void GotoFrame(int frameNum)
    {
      if (this.ocx == null)
      {
        throw new AxHost.InvalidActiveXStateException("GotoFrame", AxHost.ActiveXInvokeKind.MethodInvoke);
      }

      this.ocx.GotoFrame(frameNum);
    }

    /// <summary>
    /// Gets the current frame of the flash movie.
    /// </summary>
    /// <returns>An <see cref="Int32"/> with the current frame.</returns>
    public virtual int CurrentFrame()
    {
      if (this.ocx == null)
      {
        throw new AxHost.InvalidActiveXStateException("CurrentFrame", AxHost.ActiveXInvokeKind.MethodInvoke);
      }

      int returnValue = (int)this.ocx.CurrentFrame();
      return returnValue;
    }

    /// <summary>
    /// Gets a value indicating whether the flash movie is playing.
    /// </summary>
    /// <returns><strong>True</strong>, if the flash movie is playing, otherwise <strong>false</strong></returns>
    public virtual bool IsPlaying()
    {
      if (this.ocx == null)
      {
        throw new AxHost.InvalidActiveXStateException("IsPlaying", AxHost.ActiveXInvokeKind.MethodInvoke);
      }

      bool returnValue = (bool)this.ocx.IsPlaying();
      return returnValue;
    }

    /// <summary>
    /// Gets the percentage loaded.
    /// </summary>
    /// <returns>An <see cref="Int32"/> with the percentage loaded.</returns>
    public virtual int PercentLoaded()
    {
      if (this.ocx == null)
      {
        throw new AxHost.InvalidActiveXStateException("PercentLoaded", AxHost.ActiveXInvokeKind.MethodInvoke);
      }

      int returnValue = (int)this.ocx.PercentLoaded();
      return returnValue;
    }

    /// <summary>
    /// No documentation available
    /// </summary>
    /// <param name="frameNum">No documentation available</param>
    /// <returns>No documentation available</returns>
    public virtual bool FrameLoaded(int frameNum)
    {
      if (this.ocx == null)
      {
        throw new AxHost.InvalidActiveXStateException("FrameLoaded", AxHost.ActiveXInvokeKind.MethodInvoke);
      }

      bool returnValue = (bool)this.ocx.FrameLoaded(frameNum);
      return returnValue;
    }

    /// <summary>
    /// Returns the flash version number
    /// </summary>
    /// <returns>An <see cref="Int32"/> with the flash version number.</returns>
    public virtual int FlashVersion()
    {
      if (this.ocx == null)
      {
        throw new AxHost.InvalidActiveXStateException("FlashVersion", AxHost.ActiveXInvokeKind.MethodInvoke);
      }

      int returnValue = (int)this.ocx.FlashVersion();
      return returnValue;
    }

    /// <summary>
    /// Loads the movie identified by url to the layer specified by layerNumber.  
    /// The argument type is integer for layerNumber and string for url. 
    /// </summary>
    /// <param name="layer">The layer number</param>
    /// <param name="url">The filename with full path or URL to the movie.</param>
    public virtual void LoadMovie(int layer, string url)
    {
      if (this.ocx == null)
      {
        throw new AxHost.InvalidActiveXStateException("LoadMovie", AxHost.ActiveXInvokeKind.MethodInvoke);
      }

      this.ocx.LoadMovie(layer, url);
    }

    /// <summary>
    /// For the timeline indicated by target, goes to the frame number specified by frameNumber.
    /// </summary>
    /// <param name="target">The <see cref="String"/> with the target name.</param>
    /// <param name="frameNum">An <see cref="Int32"/> with the frame number.</param>
    public virtual void TGotoFrame(string target, int frameNum)
    {
      if (this.ocx == null)
      {
        throw new AxHost.InvalidActiveXStateException("TGotoFrame", AxHost.ActiveXInvokeKind.MethodInvoke);
      }

      this.ocx.TGotoFrame(target, frameNum);
    }

    /// <summary>
    /// For the timeline indicated by target, goes to the label specified by label.
    /// </summary>
    /// <param name="target">The <see cref="String"/> with the target name.</param>
    /// <param name="label">A <see cref="String"/> with the label name.</param>
    public virtual void TGotoLabel(string target, string label)
    {
      if (this.ocx == null)
      {
        throw new AxHost.InvalidActiveXStateException("TGotoLabel", AxHost.ActiveXInvokeKind.MethodInvoke);
      }

      this.ocx.TGotoLabel(target, label);
    }

    /// <summary>
    /// For the timeline indicated by target, gets the current frame
    /// </summary>
    /// <param name="target">The <see cref="String"/> with the target name.</param>
    /// <returns>An <see cref="Int32"/> with the current frame number.</returns>
    public virtual int TCurrentFrame(string target)
    {
      if (this.ocx == null)
      {
        throw new AxHost.InvalidActiveXStateException("TCurrentFrame", AxHost.ActiveXInvokeKind.MethodInvoke);
      }

      int returnValue = (int)this.ocx.TCurrentFrame(target);
      return returnValue;
    }

    /// <summary>
    /// For the timeline indicated by target, gets the current label
    /// </summary>
    /// <param name="target">The <see cref="String"/> with the target name.</param>
    /// <returns>A <see cref="String"/> with the label name.</returns>
    public virtual string TCurrentLabel(string target)
    {
      if (this.ocx == null)
      {
        throw new AxHost.InvalidActiveXStateException("TCurrentLabel", AxHost.ActiveXInvokeKind.MethodInvoke);
      }

      string returnValue = (string)this.ocx.TCurrentLabel(target);
      return returnValue;
    }

    /// <summary>
    /// For the timeline indicated by target starts play.
    /// </summary>
    /// <param name="target">The <see cref="String"/> with the target name.</param>
    public virtual void TPlay(string target)
    {
      if (this.ocx == null)
      {
        throw new AxHost.InvalidActiveXStateException("TPlay", AxHost.ActiveXInvokeKind.MethodInvoke);
      }

      this.ocx.TPlay(target);
    }

    /// <summary>
    /// For the timeline indicated by target, stops playing.
    /// </summary>
    /// <param name="target">The <see cref="String"/> with the target name.</param>
    public virtual void TStopPlay(string target)
    {
      if (this.ocx == null)
      {
        throw new AxHost.InvalidActiveXStateException("TStopPlay", AxHost.ActiveXInvokeKind.MethodInvoke);
      }

      this.ocx.TStopPlay(target);
    }

    /// <summary>
    /// No documentation available.
    /// </summary>
    /// <param name="name">No documentation available.</param>
    /// <param name="value">No documentation available.</param>
    public virtual void SetVariable(string name, string value)
    {
      if (this.ocx == null)
      {
        throw new AxHost.InvalidActiveXStateException("SetVariable", AxHost.ActiveXInvokeKind.MethodInvoke);
      }

      this.ocx.SetVariable(name, value);
    }

    /// <summary>
    /// No documentation available.
    /// </summary>
    /// <param name="name">No documentation available.</param>
    /// <returns>No documentation available.</returns>
    public virtual string GetVariable(string name)
    {
      if (this.ocx == null)
      {
        throw new AxHost.InvalidActiveXStateException("GetVariable", AxHost.ActiveXInvokeKind.MethodInvoke);
      }

      string returnValue = (string)this.ocx.GetVariable(name);
      return returnValue;
    }

    /// <summary>
    /// For the timeline indicated by target, sets the value of the property 
    /// specified by property to the value specified by value, which can be a 
    /// string. For property, enter the integer corresponding to the desired 
    /// property.
    /// </summary>
    /// <param name="target">The <see cref="String"/> with the target name.</param>
    /// <param name="property">The <see cref="Int32"/> with the number of the property.</param>
    /// <param name="value">The <see cref="string"/> with the value to set.</param>
    public virtual void TSetProperty(string target, int property, string value)
    {
      if (this.ocx == null)
      {
        throw new AxHost.InvalidActiveXStateException("TSetProperty", AxHost.ActiveXInvokeKind.MethodInvoke);
      }

      this.ocx.TSetProperty(target, property, value);
    }

    /// <summary>
    /// For the timeline indicated by target, gets the value of the property 
    /// specified by property to the value specified by value, which can be a 
    /// string. For property, enter the integer corresponding to the desired 
    /// property.
    /// </summary>
    /// <param name="target">The <see cref="String"/> with the target name.</param>
    /// <param name="property">The <see cref="Int32"/> with the number of the property.</param>
    /// <returns>The <see cref="string"/> with the value to retrieve.</returns>
    public virtual string TGetProperty(string target, int property)
    {
      if (this.ocx == null)
      {
        throw new AxHost.InvalidActiveXStateException("TGetProperty", AxHost.ActiveXInvokeKind.MethodInvoke);
      }

      string returnValue = (string)this.ocx.TGetProperty(target, property);
      return returnValue;
    }

    /// <summary>
    /// For the timeline indicated by target call the given frame.
    /// </summary>
    /// <param name="target">The <see cref="String"/> with the target name.</param>
    /// <param name="frameNum">An <see cref="Int32"/> with the frame number to call.</param>
    public virtual void TCallFrame(string target, int frameNum)
    {
      if (this.ocx == null)
      {
        throw new AxHost.InvalidActiveXStateException("TCallFrame", AxHost.ActiveXInvokeKind.MethodInvoke);
      }

      this.ocx.TCallFrame(target, frameNum);
    }

    /// <summary>
    /// Raises the OnReadyStateChange event.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">An <see cref="AxFlashReadyStateChangeEvent"/> with the event data.</param>
    internal void RaiseOnOnReadyStateChange(object sender, AxFlashReadyStateChangeEvent e)
    {
      if (this.OnReadyStateChange != null)
      {
        this.OnReadyStateChange(sender, e);
      }
    }

    /// <summary>
    /// Raises the OnProgress event.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">An <see cref="AxFlashProgressEvent"/> with the event data.</param>
    internal void RaiseOnOnProgress(object sender, AxFlashProgressEvent e)
    {
      if (this.OnProgress != null)
      {
        this.OnProgress(sender, e);
      }
    }

    /// <summary>
    /// Raises the FSCommand event.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">An <see cref="AxFlashFSCommandEvent"/> with the event data.</param>
    internal void RaiseOnFSCommand(object sender, AxFlashFSCommandEvent e)
    {
      if (this.FSCommand != null)
      {
        this.FSCommand(sender, e);
      }
    }

    /// <summary>
    /// Raises the FlashCall event.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">An <see cref="AxFlashCallEvent"/> with the event data.</param>
    internal void RaiseOnFlashCall(object sender, AxFlashCallEvent e)
    {
      if (this.FlashCall != null)
      {
        this.FlashCall(sender, e);
      }
    }

    #endregion //PUBLICMETHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Inherited methods                                                         //
    ///////////////////////////////////////////////////////////////////////////////
    #region OVERRIDES

    /// <summary>
    /// Overridden. Associates the underlying ActiveX control with a client that can handle control events. 
    /// </summary>
    protected override void CreateSink()
    {
      try
      {
        this.eventMulticaster = new AxFlashEventMulticaster(this);
        this.cookie = new AxHost.ConnectionPointCookie(this.ocx, this.eventMulticaster, typeof(ShockwaveFlashObjects._IShockwaveFlashEvents));
      }
      catch (System.Exception)
      {
      }
    }

    /// <summary>
    /// Overridden. Releases the event-handling client attached 
    /// in the CreateSink method from the underlying ActiveX control. 
    /// </summary>
    protected override void DetachSink()
    {
      try
      {
        this.cookie.Disconnect();
      }
      catch (System.Exception)
      {
      }
    }

    /// <summary>
    /// When overridden in a derived class, attaches interfaces to the underlying ActiveX control. 
    /// </summary>
    protected override void AttachInterfaces()
    {
      try
      {
        this.ocx = (IShockwaveFlash)this.GetOcx();
      }
      catch (System.Exception)
      {
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
    #endregion //PRIVATEMETHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER
    #endregion //HELPER
  }
}
