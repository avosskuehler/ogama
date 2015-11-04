#region - Terms of Usage -
/*
 * Copyright 2006 Sameera Perera
 *
 *  Licensed under the Apache License, Version 2.0 (the "License");
 *  you may not use this file except in compliance with the License.
 *  You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 *  Unless required by applicable law or agreed to in writing, software
 *  distributed under the License is distributed on an "AS IS" BASIS,
 *  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 *  See the License for the specific language governing permissions and
 *  limitations under the License.
 *
 */
#endregion

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;

namespace OgamaControls
{
  /// <summary>
  /// Class for creating and maintaining custom gradients.
  /// Used in attention map interface.
  /// </summary>
  public class Gradient : ICloneable
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
    /// Start color of gradient.
    /// </summary>
    private Color startColor;

    /// <summary>
    /// End color of gradient.
    /// </summary>
    private Color endColor;

    /// <summary>
    /// The <see cref="ColorBlend"/> to be used in the gradient.
    /// </summary>
    private ColorBlend colorBlend;

    /// <summary>
    /// Current gradient direction. Of type <see cref="LinearGradientMode"/> enumeration.
    /// </summary>
    private LinearGradientMode gradientDir = LinearGradientMode.Vertical;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Constructs a Gradient with both start and end colors set to transparent.
    /// </summary>
    public Gradient() :
      this(Color.Transparent, Color.Transparent)
    {
    }

    /// <summary>
    /// Constructs a new Gradient with the specified start and end colors.
    /// </summary>
    /// <param name="startColor">start color</param>
    /// <param name="endColor">end color</param>
    public Gradient(Color startColor, Color endColor)
    {
      this.startColor = startColor;
      this.endColor = endColor;
      ColorBlend blend = new ColorBlend(2);
      blend.Colors = new Color[2] { startColor, endColor };
      blend.Positions = new float[2] { 0.0f, 1.0f };
      this.colorBlend = blend;
    }

    /// <summary>
    /// Initializes a new instance of the Gradient class.
    /// </summary>
    /// <param name="gradientToClone">The <see cref="Gradient"/> to be cloned.</param>
    public Gradient(Gradient gradientToClone)
    {
      this.startColor = gradientToClone.startColor;
      this.endColor = gradientToClone.endColor;
      this.colorBlend = new ColorBlend();
      this.colorBlend.Colors = (Color[])gradientToClone.colorBlend.Colors.Clone();
      this.colorBlend.Positions = (float[])gradientToClone.colorBlend.Positions.Clone();
      this.gradientDir = gradientToClone.GradientDirection;
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
    /// Gets or sets the color blend of the gradient.
    /// </summary>
    [DefaultValue(null)]
    public ColorBlend ColorBlend
    {
      get { return colorBlend; }
      set
      {
        colorBlend = value;
        startColor = value.Colors[0];
        endColor = value.Colors[value.Colors.Length - 1];
      }
    }

    /// <summary>
    /// Gets or sets the gradient direction.
    /// </summary>
    [DefaultValue(LinearGradientMode.Vertical)]
    public LinearGradientMode GradientDirection
    {
      get { return gradientDir; }
      set { gradientDir = value; }
    }

    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Public methods                                                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region PUBLICMETHODS

    /// <summary>
    /// Clones this gradient.
    /// </summary>
    /// <returns>The cloned gradient.</returns>
    public object Clone()
    {
      return new Gradient(this);
    }

    /// <summary>
    /// Fills the specified rectangular area on the given graphics context.
    /// </summary>
    /// <param name="g">Graphics context</param>
    /// <param name="rect">rectangle to fill</param>
    public void FillRectangle(Graphics g, Rectangle rect)
    {
      PaintGradientWithDirectionOverride(g, rect, this.gradientDir);
    }

    /// <summary>
    /// Fills the specified Region area on the given graphics context.
    /// </summary>
    /// <param name="g">Graphics context</param>
    /// <param name="rgn">region to fill</param>
    public void FillRegion(Graphics g, Region rgn)
    {
      using (LinearGradientBrush brush = new LinearGradientBrush(rgn.GetBounds(g), startColor, endColor, gradientDir))
      {
        brush.InterpolationColors = this.colorBlend;
        g.FillRegion(brush, rgn);
      }
    }

    /// <summary>
    /// Fills the specified GraphicsPath using a PathGradientBrush.
    /// </summary>
    /// <param name="g">Graphics context</param>
    /// <param name="path">path to fill</param>
    public void FillPath(Graphics g, GraphicsPath path)
    {
      using (PathGradientBrush pgb = new PathGradientBrush(path))
      {
        pgb.InterpolationColors = this.colorBlend;
        g.FillPath(pgb, path);
      }
    }


    /// <summary>
    /// Returns a LinearGradientBrush object initialized based on the current
    /// gradient configuration.
    /// </summary>
    /// <remarks>
    /// The user is responsible for disposing the returned Brush object.
    /// </remarks>
    /// <param name="rect">base rectangle</param>
    /// <returns>LinearGradientBrush</returns>
    public LinearGradientBrush GetLinearGradientBrush(Rectangle rect)
    {
      LinearGradientBrush brush = new LinearGradientBrush(rect, startColor, endColor, gradientDir);
      brush.InterpolationColors = colorBlend;
      return brush;
    }

    /// <summary>
    /// Returns a PathGradientBrush object initialized based on the current
    /// gradient configuration.
    /// </summary>
    /// <remarks>
    /// The user is responsible for desposing the returned Brush object.
    /// </remarks>
    /// <param name="path">GraphicsPath</param>
    /// <returns>PathGradientBrush</returns>
    public PathGradientBrush GetPathGradientBrush(GraphicsPath path)
    {
      PathGradientBrush pgb = new PathGradientBrush(path);
      pgb.InterpolationColors = colorBlend;
      return pgb;
    }

    /// <summary>
    /// Paints this gradient with given direction in given rectangle
    /// </summary>
    /// <param name="g">Graphics to draw to</param>
    /// <param name="rect">rectangle within graphics to draw to</param>
    /// <param name="mode">LinearGradientMode enumeration member</param>
    public void PaintGradientWithDirectionOverride(Graphics g, Rectangle rect, LinearGradientMode mode)
    {
      int colorStepsCount = this.colorBlend.Colors.Length;

      if ((startColor == endColor) && (colorStepsCount == 2))
        using (SolidBrush solidBrush = new SolidBrush(startColor))
          g.FillRectangle(solidBrush, rect);
      else
        using (LinearGradientBrush brush = new LinearGradientBrush(rect, this.startColor, this.endColor, mode))
        {
          if (this.colorBlend != null && this.colorBlend.Colors.Length > 0)
          {
            // Paranoid fix for position values
            colorBlend.Positions[0] = 0.0f;
            colorBlend.Positions[colorBlend.Positions.Length - 1] = 1.0f;
            brush.InterpolationColors = colorBlend;
          }
          g.FillRectangle(brush, rect);
        }
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
