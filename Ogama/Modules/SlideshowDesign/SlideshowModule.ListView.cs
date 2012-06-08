// <copyright file="SlideshowModule.ListView.cs" company="FU Berlin">
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

namespace Ogama.Modules.SlideshowDesign
{
  using System;
  using System.Drawing;
  using System.Windows.Forms;
  using Ogama.ExceptionHandling;
  using Ogama.Modules.Common;
  using Ogama.Modules.Common.CustomRenderer;
  using Ogama.Modules.Common.SlideCollections;

  using OgamaControls;
  using VectorGraphics.Elements;
  using VectorGraphics.Elements.ElementCollections;

  /// <summary>
  /// The SlideshowModule.ListView.cs contains methods referring
  /// to the <see cref="ListView"/> control at the top of the 
  /// module.
  /// </summary>
  public partial class SlideshowModule
  {
    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler for UI, Menu, Buttons, Toolbars etc.                         //
    ///////////////////////////////////////////////////////////////////////////////
    #region WINDOWSEVENTHANDLER

    /// <summary>
    /// The <see cref="Control.DoubleClick"/> event handler for the
    /// <see cref="ListView"/> <see cref="lsvDetails"/>.
    /// Opens a designer form for the current slide.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void lsvDetails_DoubleClick(object sender, EventArgs e)
    {
      // Retrieve the client coordinates of the mouse pointer.
      Point mouseClientLocation = this.lsvDetails.PointToClient(Control.MousePosition);

      OLVColumn column;
      OLVListItem item = this.lsvDetails.GetItemAt(mouseClientLocation.X, mouseClientLocation.Y, out column);

      if (item == null)
      {
        return;
      }

      SlideshowTreeNode treeNode = item.RowObject as SlideshowTreeNode;

      if (treeNode is BrowserTreeNode)
      {
        this.OpenBrowserDesignerForm(treeNode as BrowserTreeNode);
      }
      else
      {
        // If there is a slide open it, otherwise zoom in.
        Slide currentSlide = treeNode.Slide;
        if (currentSlide != null)
        {
          if (currentSlide.IsDesktopSlide)
          {
            this.OpenDesktopDesignForm(treeNode, currentSlide);
          }
          else
          {
            this.OpenSlideDesignForm(treeNode, currentSlide);
          }
        }
        else
        {
          this.lsvDetails.SetObjects(treeNode.Nodes);
        }
      }
    }

    /// <summary>
    /// The <see cref="Control.KeyDown"/> event handler
    /// for the <see cref="ListView"/> <see cref="lsvDetails"/>.
    /// Listens for delete key.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A <see cref="KeyEventArgs"/> that contains the event data.</param>
    private void lsvDetails_KeyDown(object sender, KeyEventArgs e)
    {
      switch (e.KeyCode)
      {
        case Keys.Delete:
          foreach (SlideshowTreeNode treeNode in this.lsvDetails.SelectedObjects)
          {
            this.DeleteNode(treeNode);
          }

          this.UpdateListView(this.trvSlideshow.SelectedNodes);
          this.SlideShowModified();
          break;
        default:
          break;
      }
    }

    /// <summary>
    /// The <see cref="ListView.SelectedIndexChanged"/> event handler
    /// for the <see cref="ListView"/> <see cref="lsvDetails"/>.
    /// Updates the selected node value, which itself updates the preview window.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/>.</param>
    private void lsvDetails_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (this.lsvDetails.SelectedObject != null)
      {
        this.SelectedNode = (TreeNode)this.lsvDetails.SelectedObject;
      }
    }

    #endregion //WINDOWSEVENTHANDLER

    ///////////////////////////////////////////////////////////////////////////////
    // Methods for doing main class job                                          //
    ///////////////////////////////////////////////////////////////////////////////
    #region PRIVATEMETHODS

    /// <summary>
    /// This method sets the <see cref="ListView.TileSize"/> of the <see cref="ListView"/>
    /// with spacing around for description and icons.
    /// </summary>
    /// <param name="tileSize">A <see cref="Size"/> with the thumb size
    /// in the tile.</param>
    private void SetListViewTileSize(Size tileSize)
    {
      tileSize.Width += 15;
      tileSize.Height += 40;

      this.lsvDetails.TileSize = tileSize;
    }

    /// <summary>
    /// This method loads the first node of the given nodes collection
    /// into the listview.
    /// </summary>
    /// <param name="nodes">A <see cref="NodesCollection"/>
    /// to be shown in the <see cref="ListView"/>.</param>
    private void UpdateListView(NodesCollection nodes)
    {
      if (nodes != null && nodes.Count > 0)
      {
        this.UpdateListView(nodes[0]);
      }
    }

    /// <summary>
    /// This method shows the child nodes of the given <see cref="TreeNode"/>
    /// in the <see cref="ListView"/>, if it is itself a child node without childs
    /// its parent child collection is shown instead.
    /// </summary>
    /// <param name="node">The <see cref="TreeNode"/> thats child 
    /// collection is to be shown in the <see cref="TreeView"/>.</param>
    private void UpdateListView(TreeNode node)
    {
      if (node == null)
      {
        return;
      }

      try
      {
        this.InitializeDetailListView();
        if (node.FirstNode != null)
        {
          this.lsvDetails.SetObjects(node.Nodes);
        }
        else if (node.Parent != null)
        {
          this.lsvDetails.SetObjects(node.Parent.Nodes);
          this.lsvDetails.EnsureVisible(node.Index);
        }
        else if (node.Level == 0)
        {
          this.lsvDetails.ClearObjects();
        }

        this.SelectedNode = node;
      }
      catch (Exception ex)
      {
        ExceptionMethods.HandleException(ex);
      }
    }

    /// <summary>
    /// This method initializes the <see cref="ObjectListView"/> how to populate
    /// and render it. Please refer to the <see cref="ObjectListView"/>
    /// documentation to understand.
    /// </summary>
    private void InitializeDetailListView()
    {
      this.colPosition.AspectToStringConverter = delegate(object cellValue)
      {
        return cellValue.ToString();
      };

      this.colPosition.AspectGetter = delegate(object row)
      {
        return ((SlideshowTreeNode)row).Index;
      };

      this.colPosition.ToolTipGetter = delegate(object row)
      {
        SlideshowTreeNode node = row as SlideshowTreeNode;
        Slide slide = node.Slide;
        if (slide != null)
        {
          return slide.StopConditions.ToString();
        }
        else
        {
          return "Contains: " + node.Nodes.Count + " items";
        }
      };

      this.colPosition.Renderer = new SlideshowTreeNodeRenderer();

      this.SetListViewTileSize(Slide.SlideDesignThumbSize);

      this.lsvDetails.View = View.Tile;
      this.lsvDetails.InsertionMark.Color = Color.Green;
    }

    #endregion //PRIVATEMETHODS
  }
}
