// <copyright file="CustomRenderer.cs" company="FU Berlin">
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

namespace Ogama.Modules.Common.CustomRenderer
{
  using System;
  using System.Drawing;
  using System.Drawing.Drawing2D;

  using Ogama.Modules.Common.SlideCollections;

  using OgamaControls;

  using VectorGraphics.Elements;

  /// <summary>
  /// This class inherits <see cref="BaseRenderer"/> and extends it to enable
  /// some ogama specialized drawings like chains and dices and correct thumbnails. 
  /// </summary>
  /// <remarks>It extends the ObjectListView from Phillip Piper.</remarks>
  /// <seealso cref="ObjectListView"/>
  internal class CustomRenderer : BaseRenderer
  {
    ///////////////////////////////////////////////////////////////////////////////
    // Defining Constants                                                        //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTANTS

    /// <summary>
    /// The spacing around the list icon.
    /// </summary>
    private const int SPACING = 5;

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
    /// This static method draws the given title of the list view item
    /// to the given graphics object below the given thumbRect in the given
    /// bounding rect.
    /// </summary>
    /// <param name="g">The <see cref="Graphics"/> to draw to.</param>
    /// <param name="itemRect">The bounding <see cref="Rectangle"/> of the list view item.</param>
    /// <param name="thumbRect">The thumb <see cref="Rectangle"/> of the list view item.</param>
    /// <param name="title">A <see cref="String"/> with the title of the list view item.</param>
    protected static void DrawTitle(Graphics g, Rectangle itemRect, Rectangle thumbRect, string title)
    {
      // Now draw the text portion
      RectangleF textBoxRect = thumbRect;
      textBoxRect.Y += thumbRect.Height + SPACING;
      textBoxRect.Height = itemRect.Bottom - textBoxRect.Y - SPACING;
      
      // Spacing for Icons Dice and Mouse
      textBoxRect.Width -= 35; 

      // Measure the height of the title
      StringFormat fmt = new StringFormat(StringFormatFlags.NoWrap);
      fmt.Trimming = StringTrimming.EllipsisCharacter;
      fmt.Alignment = StringAlignment.Near;
      fmt.LineAlignment = StringAlignment.Near;
      Font font = new Font("Tahoma", 9);
      SizeF size = g.MeasureString(title, font, (int)textBoxRect.Width, fmt);

      // Draw the title
      RectangleF titelRect = textBoxRect;
      g.DrawString(title, font, Brushes.Black, textBoxRect, fmt);
    }

    /// <summary>
    /// This static method draws a chain icon at the bottom right corner of the list view item
    /// to the given graphics object with the given bounding rect.
    /// </summary>
    /// <param name="g">The <see cref="Graphics"/> to draw to.</param>
    /// <param name="boundsWithoutPadding">The bounding <see cref="Rectangle"/> of the list view item.</param>
    /// <param name="node">The <see cref="SlideshowTreeNode"/> that is the object of the list view item.</param>
    protected static void DrawChains(Graphics g, Rectangle boundsWithoutPadding, SlideshowTreeNode node)
    {
      if (node.Parent != null)
      {
        // Draw Chains
        int nodesAtLevel = node.Parent.Nodes.Count;
        if (nodesAtLevel > 1)
        {
          bool randomize = ((SlideshowTreeNode)node.Parent).Randomize;
          if (node.Index == 0)
          {
            DrawLeftConnectionPart(g, boundsWithoutPadding, randomize);
          }
          else if (node.Index > 0 && node.Index < nodesAtLevel - 1)
          {
            DrawLeftConnectionPart(g, boundsWithoutPadding, randomize);
            DrawRightConnectionPart(g, boundsWithoutPadding, randomize);
          }
          else
          {
            DrawRightConnectionPart(g, boundsWithoutPadding, randomize);
          }
        }
      }
    }

    /// <summary>
    /// This static method draws a dice icon at the bottom right corner of the list view item
    /// to the given graphics object with the given bounding rect.
    /// </summary>
    /// <param name="g">The <see cref="Graphics"/> to draw to.</param>
    /// <param name="itemRect">The bounding <see cref="Rectangle"/> of the list view item.</param>
    /// <param name="thumbRect">The thumb <see cref="Rectangle"/> of the list view item.</param>
    protected static void DrawDice(Graphics g, Rectangle itemRect, Rectangle thumbRect)
    {
      // Draw dice bitmap
      Image diceBitmap = Properties.Resources.DiceHS16;
      g.DrawImage(
        diceBitmap,
        thumbRect.Right - 2 - diceBitmap.Width,
        itemRect.Bottom - diceBitmap.Height - 10,
        diceBitmap.Width,
        diceBitmap.Height);
    }

    /// <summary>
    /// This static method draws a trial icon at the bottom right corner of the list view item
    /// to the given graphics object with the given bounding rect.
    /// </summary>
    /// <param name="g">The <see cref="Graphics"/> to draw to.</param>
    /// <param name="itemRect">The bounding <see cref="Rectangle"/> of the list view item.</param>
    /// <param name="thumbRect">The thumb <see cref="Rectangle"/> of the list view item.</param>
    protected static void DrawTrialIcon(Graphics g, Rectangle itemRect, Rectangle thumbRect)
    {
      // Draw a trial bitmap
      Image trialBitmap = Properties.Resources.OrgChartHS;
      g.DrawImage(
        trialBitmap,
        thumbRect.Right - 2 - trialBitmap.Width,
        itemRect.Bottom - trialBitmap.Height - 10,
        trialBitmap.Width,
        trialBitmap.Height);
    }

    /// <summary>
    /// This static method draws a mouse icon at the bottom right corner of the list view item
    /// to the given graphics object with the given bounding rect.
    /// </summary>
    /// <param name="g">The <see cref="Graphics"/> to draw to.</param>
    /// <param name="itemRect">The bounding <see cref="Rectangle"/> of the list view item.</param>
    /// <param name="thumbRect">The thumb <see cref="Rectangle"/> of the list view item.</param>
    protected static void DrawMouse(Graphics g, Rectangle itemRect, Rectangle thumbRect)
    {
      // Draw a mouse icon
      Image mouseBitmap = Properties.Resources.Mouse;
      g.DrawImage(
        mouseBitmap,
        thumbRect.Right - 2 - 16,
        itemRect.Bottom - 16 - 10,
        16,
        16);
    }

    /// <summary>
    /// This static method draws a sound icon at the bottom right corner of the list view item
    /// to the given graphics object with the given bounding rect.
    /// </summary>
    /// <param name="g">The <see cref="Graphics"/> to draw to.</param>
    /// <param name="itemRect">The bounding <see cref="Rectangle"/> of the list view item.</param>
    /// <param name="thumbRect">The thumb <see cref="Rectangle"/> of the list view item.</param>
    protected static void DrawAudioIcon(Graphics g, Rectangle itemRect, Rectangle thumbRect)
    {
      // Draw audio icon
      Image audioBitmap = Properties.Resources.sound;
      g.DrawImage(
        audioBitmap,
        thumbRect.Right - 2 - 16 - 2 - 16,
        itemRect.Bottom - 16 - 10,
        16,
        16);
    }

    /// <summary>
    /// This static method draws the right part of a chain at the left of the list view item
    /// to the given graphics object with the given bounding rect.
    /// If applicable the icon is replaced with the right part of a dice bitmap to 
    /// visualize randomization.
    /// </summary>
    /// <param name="g">The <see cref="Graphics"/> to draw to.</param>
    /// <param name="boundsWithoutPadding">The bounding <see cref="Rectangle"/> of the list view item.</param>
    /// <param name="randomize"><strong>True</strong> if the list view item is to be shuffled (should be drawn with a dice),
    /// otherwise <strong>false</strong> (will be drawn with a chain)</param>
    protected static void DrawRightConnectionPart(Graphics g, Rectangle boundsWithoutPadding, bool randomize)
    {
      Image rightBitmap = Properties.Resources.ChainRightPart;
      if (randomize)
      {
        rightBitmap = Properties.Resources.DiceRightPart;
      }

      Point position = new Point(
        boundsWithoutPadding.Left,
        boundsWithoutPadding.Top + boundsWithoutPadding.Height / 2 - rightBitmap.Height);

      g.DrawImageUnscaled(rightBitmap, position);
    }

    /// <summary>
    /// This static method draws the left part of a chain at the right of the list view item
    /// to the given graphics object with the given bounding rect.
    /// If applicable the icon is replaced with the left part of a dice bitmap to 
    /// visualize randomization.
    /// </summary>
    /// <param name="g">The <see cref="Graphics"/> to draw to.</param>
    /// <param name="boundsWithoutPadding">The bounding <see cref="Rectangle"/> of the list view item.</param>
    /// <param name="randomize"><strong>True</strong> if the list view item is to be shuffled (should be drawn with a dice),
    /// otherwise <strong>false</strong> (will be drawn with a chain)</param>
    protected static void DrawLeftConnectionPart(Graphics g, Rectangle boundsWithoutPadding, bool randomize)
    {
      Image leftBitmap = Properties.Resources.ChainLeftPart;
      if (randomize)
      {
        leftBitmap = Properties.Resources.DiceLeftPart;
      }

      Point position = new Point(
        boundsWithoutPadding.Right - leftBitmap.Width,
        boundsWithoutPadding.Top + boundsWithoutPadding.Height / 2 - leftBitmap.Height);
      g.DrawImageUnscaled(leftBitmap, position);
    }

    /// <summary>
    /// This static method calculates the bounding rectangle for the thumb of 
    /// the list view item and creates a <see cref="BufferedGraphics"/> for the drawing.
    /// </summary>
    /// <param name="g">Ref. The <see cref="Graphics"/> to be drawn to.</param>
    /// <param name="r">The <see cref="Rectangle"/> with the bounds of the list view item.</param>
    /// <param name="buffered">Out. <see cref="BufferedGraphics"/> to be used for drawing.</param>
    /// <returns>The bounding <see cref="Rectangle"/> for the thumb of 
    /// the list view item</returns>
    protected Rectangle GetThumbRect(ref Graphics g, Rectangle r, out BufferedGraphics buffered)
    {
      Rectangle boundsWithoutPadding = r;
      boundsWithoutPadding.Inflate(2, 2);

      // Use buffered graphics to kill flickers
      buffered = BufferedGraphicsManager.Current.Allocate(g, boundsWithoutPadding);
      g = buffered.Graphics;
      g.Clear(this.ListView.BackColor);
      g.SmoothingMode = SmoothingMode.AntiAlias;

      Rectangle bounds = r;

      // Allow a border around the item
      bounds.Inflate(-2, -2);

      // Draw background
      if (this.IsItemSelected)
      {
        Pen focusPen = new Pen(Color.Red, 2.0f);
        GraphicsPath roundedRectangle = VGRectangle.GetRoundedRect(bounds, 5f);
        g.FillPath(new SolidBrush(Color.FromArgb(125, Color.Gray)), roundedRectangle);
        g.DrawPath(focusPen, roundedRectangle);
      }

      // Draw the Image
      Size thumbsSize = this.ListView.TileSize;
      thumbsSize.Width -= 15;
      thumbsSize.Height -= 40;
      Rectangle thumbRect = new Rectangle(
        new Point(bounds.X + bounds.Width / 2 - thumbsSize.Width / 2, bounds.Y + SPACING),
        thumbsSize);

      return thumbRect;
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
