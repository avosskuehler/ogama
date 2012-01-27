﻿// <copyright file="SlideshowTreeNodeRenderer.cs" company="FU Berlin">
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

namespace Ogama.Modules.Common.CustomRenderer
{
  using System;
  using System.Drawing;
  using System.Windows.Forms;

  using Ogama.ExceptionHandling;
  using Ogama.Modules.Common.SlideCollections;

  /// <summary>
  /// This class inherits <see cref="CustomRenderer"/> to extend the list view
  /// renderer with a render for <see cref="SlideshowTreeNode"/>.
  /// </summary>
  internal class SlideshowTreeNodeRenderer : CustomRenderer
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
    /// Overridden. Defines specialized handling for drawing
    /// list view items thats object are of type <see cref="SlideshowTreeNode"/>
    /// </summary>
    /// <param name="g">The <see cref="Graphics"/> to be used.</param>
    /// <param name="r">The bounding <see cref="Rectangle"/> of the item.</param>
    /// <returns><strong>True</strong> if this method handled the drawing,
    /// otherwise <strong>false</strong>.</returns>
    public override bool OptionalRender(Graphics g, Rectangle r)
    {
      // If we're in any other view than Tile, just let the default process do it's stuff
      if (this.ListView.View != View.Tile)
      {
        return false;
      }

      try
      {
        Rectangle boundsWithoutPadding = r;
        boundsWithoutPadding.Inflate(2, 2);

        BufferedGraphics buffered;
        Rectangle thumbRect = this.GetThumbRect(ref g, r, out buffered);

        SlideshowTreeNode node = this.RowObject as SlideshowTreeNode;

        if (node.Slide != null)
        {
          if (node.Slide.PresentationSize == Size.Empty)
          {
            node.Slide.PresentationSize = Document.ActiveDocument.PresentationSize;
          }

          g.DrawImage(node.Slide.Thumb, thumbRect);

          if (node.Slide.MouseCursorVisible)
          {
            DrawMouse(g, r, thumbRect);
          }

          // Draw audio icon
          if (node.Slide.BackgroundSound != null && node.Slide.BackgroundSound.ShouldPlay)
          {
            DrawAudioIcon(g, r, thumbRect);
          }
        }
        else
        {
          g.FillRectangle(Brushes.LightGray, thumbRect);
          g.DrawRectangle(Pens.DarkGray, thumbRect);

          this.DrawNodeInRect(node, thumbRect, g);
        }

        // Draw title
        DrawTitle(g, r, thumbRect, node.Text);

        // Draw chains
        DrawChains(g, boundsWithoutPadding, node);

        // Draw dice icon
        if (node.Randomize)
        {
          DrawDice(g, r, thumbRect);
        }

        // Draw Trial icon
        if (node.Tag != null)
        {
          if (node.Tag.ToString() == "Trial")
          {
            DrawTrialIcon(g, r, thumbRect);
          }
        }

        // Finally render the buffered graphics
        buffered.Render();
        buffered.Dispose();
      }
      catch (Exception ex)
      {
        ExceptionMethods.HandleException(ex);
      }

      // Return true to say that we've handled the drawing
      return true;
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

    /// <summary>
    /// This method does the main job of drawing the given <see cref="SlideshowTreeNode"/>
    /// in the given <see cref="Rectangle"/> using the given <see cref="Graphics"/> object.
    /// It calls the various drawing methods from the base class <see cref="CustomRenderer"/>.
    /// </summary>
    /// <param name="node">The <see cref="SlideshowTreeNode"/> to be rendered.</param>
    /// <param name="thumbRect">A <see cref="Rectangle"/> with the bounds for the node.</param>
    /// <param name="g">The <see cref="Graphics"/> to be used.</param>
    private void DrawNodeInRect(SlideshowTreeNode node, Rectangle thumbRect, Graphics g)
    {
      if (thumbRect.Width < 8 || thumbRect.Height < 5)
      {
        return;
      }

      int nodeCount = node.Nodes.Count;
      Rectangle newBounds = thumbRect;
      newBounds.Inflate(-2, -2);
      int newWidth = newBounds.Width;
      int newHeight = newBounds.Height;

      if (nodeCount == 0)
      {
        if (node.Slide != null)
        {
          if (node.Slide.PresentationSize == Size.Empty)
          {
            node.Slide.PresentationSize = Document.ActiveDocument.PresentationSize;
          }

          g.DrawImage(node.Slide.Thumb, newBounds);
        }
        else
        {
          StringFormat sf = new StringFormat();
          sf.Alignment = StringAlignment.Center;
          sf.LineAlignment = StringAlignment.Center;
          g.DrawString("No slides defined ...", SystemFonts.MenuFont, Brushes.Black, thumbRect, sf);
        }
      }
      else if (nodeCount == 1)
      {
        this.DrawNodeInRect((SlideshowTreeNode)node.Nodes[0], newBounds, g);
      }
      else if (nodeCount <= 4)
      {
        g.DrawRectangle(Pens.DarkGray, newBounds);

        newWidth = (int)((newBounds.Width - 1) / 2f);
        newHeight = (int)((newBounds.Height - 1) / 2f);
        Rectangle subItemRect = new Rectangle(newBounds.Location, new Size(newWidth, newHeight));
        for (int i = 0; i < nodeCount; i++)
        {
          this.DrawNodeInRect((SlideshowTreeNode)node.Nodes[i], subItemRect, g);
          if (i % 2 == 0)
          {
            subItemRect.Offset(newWidth + 1, 0);
          }
          else
          {
            subItemRect.Offset(-newWidth - 1, newHeight + 1);
          }
        }
      }
      else
      {
        g.DrawRectangle(Pens.DarkGray, newBounds);

        newWidth = (int)((newBounds.Width - 2) / 3f);
        newHeight = (int)((newBounds.Height - 2) / 3f);
        Rectangle subItemRect = new Rectangle(newBounds.Location, new Size(newWidth, newHeight));
        for (int i = 0; i <= Math.Min(9, nodeCount - 1); i++)
        {
          if (i == 8 && nodeCount > 9)
          {
            int itemsLeft = nodeCount - i;
            g.DrawString(itemsLeft.ToString() + " more", SystemFonts.MenuFont, Brushes.Red, subItemRect);
            return;
          }

          this.DrawNodeInRect((SlideshowTreeNode)node.Nodes[i], subItemRect, g);
          if (i == 2 || i == 5)
          {
            subItemRect.Offset(-newWidth * 2 - 2, newHeight + 1);
          }
          else
          {
            subItemRect.Offset(newWidth + 1, 0);
          }
        }
      }
    }

    #endregion //PRIVATEMETHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER
    #endregion //HELPER
  }
}
