// <copyright file="SlideshowModule.TreeView.cs" company="FU Berlin">
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

namespace Ogama.Modules.SlideshowDesign
{
  using System;
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.Drawing;
  using System.Windows.Forms;
  using Ogama.ExceptionHandling;
  using Ogama.Modules.Common;
  using Ogama.Modules.Common.SlideCollections;

  using OgamaControls;
  using VectorGraphics;
  using VectorGraphics.Elements;
  using VectorGraphics.Elements.ElementCollections;

  /// <summary>
  /// The SlideshowModule.TreeView.cs contains methods referring
  /// to the <see cref="TreeView"/> control at the left of the 
  /// module.
  /// </summary>
  public partial class SlideshowModule
  {
    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler for UI, Menu, Buttons, Toolbars etc.                         //
    ///////////////////////////////////////////////////////////////////////////////
    #region WINDOWSEVENTHANDLER

    /// <summary>
    /// The <see cref="Control.MouseDown"/> event handler for the
    /// <see cref="TreeView"/> <see cref="trvSlideshow"/>.
    /// Get the tree node under the mouse pointer and save it in the selectedNode variable.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A <see cref="MouseEventArgs"/> that contains the event data.</param>
    private void trvSlideshow_MouseDown(object sender, MouseEventArgs e)
    {
      this.selectedNode = this.trvSlideshow.GetNodeAt(e.X, e.Y);
    }

    /// <summary>
    /// The <see cref="Control.KeyDown"/> event handler
    /// for the <see cref="TreeView"/> <see cref="trvSlideshow"/>.
    /// Listens for delete key and F2 (edit key)
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A <see cref="KeyEventArgs"/> that contains the event data.</param>
    private void trvSlideshow_KeyDown(object sender, KeyEventArgs e)
    {
      switch (e.KeyCode)
      {
        case Keys.Delete:
          foreach (SlideshowTreeNode treeNode in this.trvSlideshow.SelectedNodes)
          {
            this.DeleteNode(treeNode);
          }

          this.trvSlideshow.SelectedNodes.Clear();
          this.trvSlideshow.SelectedNodes.Add(this.trvSlideshow.Nodes[0]);
          this.UpdateListView(this.trvSlideshow.SelectedNodes);
          this.SlideShowModified();
          break;
        case Keys.F2:
          if (this.trvSlideshow.SelectedNodes.Count > 0)
          {
            this.selectedNode = this.trvSlideshow.SelectedNodes[0];
            this.BeginNodeLabelEdit();
          }

          break;
      }
    }

    /// <summary>
    /// The <see cref="Control.DoubleClick"/> event handler
    /// for the <see cref="TreeView"/> <see cref="trvSlideshow"/>.
    /// Sets the selectedNode variable and starts editing the name of the node.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An epmty <see cref="EventArgs"/>.</param>
    private void trvSlideshow_DoubleClick(object sender, EventArgs e)
    {
      this.selectedNode = this.trvSlideshow.GetNodeAt(this.trvSlideshow.PointToClient(Control.MousePosition));
      this.BeginNodeLabelEdit();
    }

    /// <summary>
    /// The <see cref="TreeView.AfterLabelEdit"/> event handler
    /// for the <see cref="TreeView"/> <see cref="trvSlideshow"/>.
    /// Error checking for invalid labels.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A <see cref="NodeLabelEditEventArgs"/> that contains the event data.</param>
    private void trvSlideshow_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
    {
      if (e.Label != null)
      {
        if (e.Label.Length > 0)
        {
          if (this.GetTreeNodeByName(e.Label) != null)
          {
            // Cancel the label edit action, inform the user, and 
            // place the node in edit mode again.
            e.CancelEdit = true;
            ExceptionMethods.ProcessErrorMessage(
              "Invalid tree node label." + Environment.NewLine + "This name is already in use.");

            e.Node.BeginEdit();
            return;
          }

          if (e.Label.IndexOfAny(new char[] { '@', '.', ',', '!' }) == -1)
          {
            // Stop editing without cancelling the label change.
            e.Node.EndEdit(false);
            Slide slide = ((SlideshowTreeNode)e.Node).Slide;
            if (slide != null)
            {
              slide.Name = e.Label;
            }

            this.UpdateListView(this.trvSlideshow.SelectedNodes);
            this.SlideShowModified();
          }
          else
          {
            // Cancel the label edit action, inform the user, and 
            // place the node in edit mode again.
            e.CancelEdit = true;
            ExceptionMethods.ProcessErrorMessage(
              "Invalid tree node label." + Environment.NewLine + "The invalid characters are: '@','.', ',', '!'");

            e.Node.BeginEdit();
            return;
          }
        }
        else
        {
          // Cancel the label edit action, inform the user, and 
          // place the node in edit mode again. 
          e.CancelEdit = true;
          ExceptionMethods.ProcessErrorMessage("Invalid tree node label." + Environment.NewLine + "The label cannot be blank");
          e.Node.BeginEdit();
          return;
        }

        this.trvSlideshow.LabelEdit = false;
      }
    }

    /// <summary>
    /// The <see cref="TreeView.DrawNode"/> event handler
    /// for the <see cref="TreeView"/> <see cref="trvSlideshow"/>.
    /// Performs customized drawing of the <see cref="TreeNode"/>s.
    /// That is a trial icon or a dice icon when trial or shuffling mode is set.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A <see cref="DrawTreeNodeEventArgs"/> that contains the event data.</param>
    private void trvSlideshow_DrawNode(object sender, DrawTreeNodeEventArgs e)
    {
      e.DrawDefault = true;

      Rectangle idBounds = e.Node.Bounds;
      idBounds.Offset(e.Node.Bounds.Width + 2, 0);
      string idString = "(ID:" + e.Node.Name + ")";
      e.Graphics.DrawString(idString, this.trvSlideshow.Font, Brushes.Black, idBounds.Location);
      SizeF textSize = e.Graphics.MeasureString(idString, this.trvSlideshow.Font);

      // If the randomize flag is set, draw a dice icon 
      // to the right of the label text.
      bool randomize = ((SlideshowTreeNode)e.Node).Randomize;
      if (randomize)
      {
        Rectangle diceBounds = e.Node.Bounds;
        diceBounds.Offset(e.Node.Bounds.Width + 10 + (int)textSize.Width, 0);
        Slideshow.DrawDice(e.Graphics, diceBounds.Location);
      }

      // If the disabled flag is set, draw a disabled icon 
      // to the right of the label text.
      var node = e.Node as SlideshowTreeNode;
      if (node.Slide != null)
      {
        if (node.Slide.IsDisabled)
        {
          Rectangle disabledBounds = e.Node.Bounds;
          disabledBounds.Offset(e.Node.Bounds.Width + 10 + (int)textSize.Width, 0);
          Slideshow.DrawDisabled(e.Graphics, disabledBounds.Location);
        }
      }
    }

    /// <summary>
    /// The <see cref="TreeView.AfterSelect"/> event handler
    /// for the <see cref="TreeView"/> <see cref="trvSlideshow"/>.
    /// Shows the selected nodes child collection in the <see cref="ListView"/>
    /// at the top of the module.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A <see cref="TreeViewEventArgs"/> that contains the event data.</param>
    private void trvSlideshow_AfterSelect(object sender, TreeViewEventArgs e)
    {
      this.UpdateListView(e.Node);
    }

    #region TreeViewDragDrop

    /// <summary>
    /// The <see cref="TreeView.ItemDrag"/> event handler for the
    /// <see cref="TreeView"/> <see cref="trvSlideshow"/>.
    /// Starts the drag and drop operation.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An <see cref="ItemDragEventArgs"/> that contains the event data. </param>
    private void trvSlideshow_ItemDrag(object sender, ItemDragEventArgs e)
    {
      this.trvSlideshow.DoDragDrop(this.trvSlideshow.SelectedNodes, DragDropEffects.Move | DragDropEffects.Copy);
    }

    /// <summary>
    /// The <see cref="Control.DragEnter"/> event handler for the
    /// <see cref="TreeView"/> <see cref="trvSlideshow"/>.
    /// Occurs when an object is dragged into the control's bounds. 
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An <see cref="DragEventArgs"/> that contains the event data. </param>
    private void trvSlideshow_DragEnter(object sender, DragEventArgs e)
    {
      int len = e.Data.GetFormats().Length - 1;
      int i;
      for (i = 0; i <= len; i++)
      {
        if (!e.Data.GetFormats()[i].Contains("NodesCollection"))
        {
          // The data from the drag source is moved to the target.
          e.Effect = DragDropEffects.None;
        }
      }
    }

    /// <summary>
    /// The <see cref="Control.DragOver"/> event handler for the
    /// <see cref="TreeView"/> <see cref="trvSlideshow"/>.
    /// Occurs when an object is dragged over the control's bounds. 
    /// Updates the drag cursor with copy or move state, referring to Modifier Key.
    /// Ensures the visibility of the item under the mouse cursor.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An <see cref="DragEventArgs"/> that contains the event data. </param>
    private void trvSlideshow_DragOver(object sender, DragEventArgs e)
    {
      // Retrieve the client coordinates of the mouse pointer.
      Point targetPoint = this.trvSlideshow.PointToClient(new Point(e.X, e.Y));

      SlideshowTreeNode targetNode = this.trvSlideshow.GetNodeAt(targetPoint) as SlideshowTreeNode;

      // Retrieve the dragged items.
      NodesCollection draggedItems =
           (NodesCollection)e.Data.GetData(typeof(NodesCollection));

      if (targetNode != null)
      {
        if (((SlideshowTreeNode)draggedItems[0]).Contains(targetNode) || targetNode == draggedItems[0])
        {
          e.Effect = DragDropEffects.None;
          return;
        }

        bool isSlide = targetNode.Slide != null;

        // If target is not a slide
        if (!isSlide)
        {
          if (Control.ModifierKeys == Keys.Control)
          {
            e.Effect = DragDropEffects.Copy;
          }
          else
          {
            e.Effect = DragDropEffects.Move;
          }

          return;
        }
      }

      e.Effect = DragDropEffects.None;
    }

    /// <summary>
    /// The <see cref="Control.DragDrop"/> event handler for the
    /// <see cref="TreeView"/> <see cref="trvSlideshow"/>.
    /// Occurs when a drag-and-drop operation is completed. 
    /// Inserts the moved or copied tree node item and updates the member slideshow.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An <see cref="DragEventArgs"/> that contains the event data. </param>
    private void trvSlideshow_DragDrop(object sender, DragEventArgs e)
    {
      // If the insertion mark is not visible, exit the method.
      if (e.Effect == DragDropEffects.None)
      {
        return;
      }

      // Retrieve the client coordinates of the mouse pointer.
      Point targetPoint = this.trvSlideshow.PointToClient(new Point(e.X, e.Y));

      TreeNode targetItem = this.trvSlideshow.GetNodeAt(targetPoint);

      // Retrieve the dragged items.
      NodesCollection draggedItems =
           (NodesCollection)e.Data.GetData(typeof(NodesCollection));

      this.trvSlideshow.BeginUpdate();

      if (e.Effect == DragDropEffects.Move)
      {
        foreach (TreeNode node in draggedItems)
        {
          // Update TreeView
          node.Parent.Nodes.Remove(node);
          targetItem.Nodes.Add(node);
        }
      }
      else if (e.Effect == DragDropEffects.Copy)
      {
        List<string> names = new List<string>();
        this.slideshow.GetNodeNames(this.slideshow, ref names);

        foreach (TreeNode node in draggedItems)
        {
          // Update TreeView And Slideshow names
          SlideshowTreeNode copyNode = (SlideshowTreeNode)node.Clone();
          this.RenameNodes(copyNode, ref names);
          targetItem.Nodes.Add(copyNode);
        }
      }

      this.trvSlideshow.EndUpdate();
      this.SlideShowModified();
      this.UpdateListView(this.trvSlideshow.SelectedNodes);
    }

    #endregion TreeViewDragDrop

    #region CONTEXTMENU

    /// <summary>
    /// The <see cref="ToolStripDropDown.Opening"/> event handler for the
    /// <see cref="ContextMenuStrip"/> <see cref="cmuItemView"/>.
    /// Occurs when the context menu is opening.
    /// Updates the menus items visibility and read only states according
    /// to selected node.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An <see cref="CancelEventArgs"/> that contains the event data. </param>
    private void cmuItemView_Opening(object sender, CancelEventArgs e)
    {
      this.cmuDisable.Text = "Disable Slide";
      this.cmuDisable.Checked = false;
 
      int itemCount = this.trvSlideshow.SelectedNodes.Count;
      if (itemCount > 1)
      {
        this.cmuShuffle.Enabled = false;
        this.cmuCountCombo.Enabled = false;
        this.cmuDescription.Enabled = false;

        // If the selected nodes are trial nodes enable the combine to trial menu item
        // otherwise disable.
        SlideshowTreeNode firstNode = this.trvSlideshow.SelectedNodes[0] as SlideshowTreeNode;
        if (firstNode.Slide != null)
        {
          this.cmuCombineToTrial.Enabled = true;
          this.cmuCombineToTrial.Checked = false;
        }
        else
        {
          this.cmuCombineToTrial.Enabled = false;
          this.cmuCombineToTrial.Checked = false;
        }
      }
      else
      {
        this.cmuCombineToTrial.Enabled = false;
        this.cmuCombineToTrial.Checked = false;

        // Update the shuffle item status.
        if (this.trvSlideshow.SelectedNodes[0].Tag != null && this.trvSlideshow.SelectedNodes[0].Tag.ToString() == "Trial")
        {
          this.cmuShuffle.Enabled = false;
          this.cmuShuffle.Checked = false;
          this.cmuCombineToTrial.Checked = true;
          this.cmuCombineToTrial.Enabled = true;
          this.cmuCountCombo.Enabled = false;
          this.cmuCountCombo.Items.Clear();
          this.cmuDescription.Enabled = false;
        }
        else
        {
          SlideshowTreeNode node = this.trvSlideshow.SelectedNodes[0] as SlideshowTreeNode;
          if (node.Slide != null && node.Slide.IsDisabled)
          {
            this.cmuDisable.Text = "Enable Slide";
            this.cmuDisable.Checked = true;
          }

          this.cmuShuffle.Enabled = true;
          this.cmuShuffle.Checked = node.Randomize;
          if (this.cmuShuffle.Checked)
          {
            this.cmuCountCombo.Enabled = true;
            this.cmuDescription.Enabled = true;
            this.cmuCountCombo.Items.Clear();
            this.cmuCountCombo.Items.Add("All");
            int subNodesCount = node.Nodes.Count;
            for (int i = 1; i <= subNodesCount; i++)
            {
              this.cmuCountCombo.Items.Add(i);
            }

            if (node.NumberOfItemsToUse == 0 || node.NumberOfItemsToUse == subNodesCount)
            {
              this.cmuCountCombo.SelectedItem = "All";
            }
            else
            {
              this.cmuCountCombo.SelectedItem = node.NumberOfItemsToUse;
            }

            this.cmuCombineToTrial.Enabled = false;
            this.cmuCombineToTrial.Checked = false;
          }
          else
          {
            this.cmuCountCombo.Enabled = false;
            this.cmuDescription.Enabled = false;
          }
        }
      }
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="ToolStripMenuItem"/> <see cref="cmuDisable"/>.
    /// Occurs when the context menues disable button is clicked
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/>.</param>
    private void cmuDisable_Click(object sender, EventArgs e)
    {
      var selectedNodes = this.trvSlideshow.SelectedNodes;

      foreach (var selectedNode1 in selectedNodes)
      {
        var node = selectedNode1 as SlideshowTreeNode;
        if (node.Slide != null)
        {
          node.Slide.IsDisabled = !node.Slide.IsDisabled;
        }
      }

      this.trvSlideshow.Invalidate();
      this.lsvDetails.Invalidate();
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="ToolStripMenuItem"/> <see cref="cmuShuffle"/>.
    /// Occurs when the context menues shuffle entry is clicked.
    /// Sets ur unsets the nodes randomize flag.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/>.</param>
    private void cmuShuffle_Click(object sender, EventArgs e)
    {
      NodesCollection selectedNodes = this.trvSlideshow.SelectedNodes;
      if (selectedNodes.Count != 1)
      {
        return;
      }

      SlideshowTreeNode node = this.trvSlideshow.SelectedNodes[0] as SlideshowTreeNode;
      if (node != null)
      {
        if (node.Slide == null)
        {
          node.Randomize = this.cmuShuffle.Checked;
        }
        else
        {
          ExceptionMethods.ProcessMessage(
            "Please note:",
            "You cannot shuffle/unshuffle single slides, please select the parent node");
        }
      }

      this.trvSlideshow.Invalidate();
      this.lsvDetails.Invalidate();
    }

    /// <summary>
    /// The <see cref="ToolStripComboBox.SelectedIndexChanged"/> event handler for the
    /// <see cref="ToolStripComboBox"/> <see cref="cmuCountCombo"/>.
    /// Indicates the number of items in the child nodes collection
    /// that should be used during presentation of the shuffled collection.
    /// </summary>
    /// <remarks>Use this to reduce the stimuli that are shown during presentation
    /// to a limited number of shuffled items of the child collection.</remarks>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/>.</param>
    private void cmuCountCombo_SelectedIndexChanged(object sender, EventArgs e)
    {
      NodesCollection selectedNodes = this.trvSlideshow.SelectedNodes;
      if (selectedNodes.Count != 1)
      {
        return;
      }

      SlideshowTreeNode node = this.trvSlideshow.SelectedNodes[0] as SlideshowTreeNode;
      if (node != null)
      {
        if (node.Slide == null)
        {
          if (this.cmuCountCombo.SelectedItem.ToString() == "All")
          {
            node.NumberOfItemsToUse = node.Nodes.Count;
          }
          else
          {
            node.NumberOfItemsToUse = (int)this.cmuCountCombo.SelectedItem;
          }
        }
        else
        {
          ExceptionMethods.ProcessMessage(
            "Please note:",
            "You cannot change subitem range for single slides, please select the parent node");
        }
      }

      this.trvSlideshow.Invalidate();
      this.lsvDetails.Invalidate();
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="ToolStripComboBox"/> <see cref="cmuShuffle"/>.
    /// Occurs when the context menues combine to trial entry is clicked.
    /// Combines the selected nodes to a trial, if applicable create a new group for
    /// the trial.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/>.</param>
    private void cmuCombineToTrial_Click(object sender, EventArgs e)
    {
      if (this.cmuCombineToTrial.Checked)
      {
        NodesCollection nodes = this.trvSlideshow.SelectedNodes;
        if (nodes.Count > 1)
        {
          SlideshowTreeNode firstNode = nodes[0] as SlideshowTreeNode;
          if (firstNode.Slide != null)
          {
            TreeNode parent = firstNode.Parent;
            foreach (TreeNode subNode in nodes)
            {
              if (subNode.Parent != parent)
              {
                ExceptionMethods.ProcessMessage(
                  "Please note:",
                 "You can only combine slides with the same parent node to a trial.");
                return;
              }
            }

            // If all nodes of parent were marked to be a trial
            // it is enough to mark the parent with the "Trial" tag,
            // except it is the base node
            // otherwise create a new "Trial" marked group with the items
            if (parent.Nodes.Count != nodes.Count || parent.Text == "Slideshow")
            {
              this.MoveNodesLevelUp(nodes, true);
            }
            else
            {
              parent.Tag = "Trial";
              ((SlideshowTreeNode)parent).Randomize = false;
              ((SlideshowTreeNode)parent).SetTreeNodeImageKey((SlideshowTreeNode)parent);
            }
          }
          else
          {
            ExceptionMethods.ProcessMessage(
              "Please note:",
             "You can only combine slide nodes to a trial.");
          }
        }
      }
      else
      {
        // Remove trial tag
        NodesCollection nodes = this.trvSlideshow.SelectedNodes;
        if (nodes.Count == 1)
        {
          SlideshowTreeNode firstNode = nodes[0] as SlideshowTreeNode;
          firstNode.Tag = string.Empty;
        }
      }

      this.trvSlideshow.Invalidate();
      this.lsvDetails.Invalidate();
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="ToolStripComboBox"/> <see cref="cmuDelete"/>.
    /// Deletes all selected nodes of the <see cref="TreeView"/>.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/>.</param>
    private void cmuDelete_Click(object sender, EventArgs e)
    {
      foreach (TreeNode node in this.trvSlideshow.SelectedNodes)
      {
        this.DeleteNode(node as SlideshowTreeNode);
      }

      this.trvSlideshow.SelectedNodes.Clear();
      this.trvSlideshow.SelectedNodes.Add(this.trvSlideshow.Nodes[0]);
      this.UpdateListView(this.trvSlideshow.SelectedNodes);
      this.SlideShowModified();
    }

    #endregion CONTEXTMENU

    #endregion //WINDOWSEVENTHANDLER

    ///////////////////////////////////////////////////////////////////////////////
    // Methods for doing main class job                                          //
    ///////////////////////////////////////////////////////////////////////////////
    #region PRIVATEMETHODS

    /// <summary>
    /// This method recursively iterates the given node and its child
    /// renaming them with new IDs.
    /// It is used during insertion of new slideshows to prevent duplicate IDs.
    /// </summary>
    /// <param name="copyNode">The <see cref="TreeNode"/> to rename.</param>
    /// <param name="names">Ref. A <see cref="List{String}"/> with the already used names.</param>
    private void RenameNodes(TreeNode copyNode, ref List<string> names)
    {
      string newName = this.GetUnusedNodeID(ref names);
      copyNode.Name = newName;
      foreach (TreeNode node in copyNode.Nodes)
      {
        this.RenameNodes(node, ref names);
      }
    }

    /// <summary>
    /// This method returns an unused node id for a new inserted node.
    /// </summary>
    /// <param name="names">Ref. A <see cref="List{String}"/> with already used slide names.</param>
    /// <returns>An unused node id as a <see cref="String"/> to be used
    /// for a new inserted node <see cref="TreeNode.Name"/>.</returns>
    private string GetUnusedNodeID(ref List<string> names)
    {
      int count = names.Count;
      string proposal = count.ToString();

      while (names.Contains(proposal))
      {
        count++;
        proposal = count.ToString();
      }

      names.Add(proposal);

      return proposal;
    }

    /// <summary>
    /// This method sets the state of the current selected node
    /// to edit mode to begin label editing.
    /// </summary>
    private void BeginNodeLabelEdit()
    {
      if (this.selectedNode != null && this.selectedNode.Parent != null)
      {
        this.trvSlideshow.LabelEdit = true;
        if (!this.selectedNode.IsEditing)
        {
          this.selectedNode.BeginEdit();
        }
      }
      else
      {
        string message = "No tree node selected or selected node is a root node." + Environment.NewLine +
           "The root node cannot be renamed.";
        ExceptionMethods.ProcessMessage("Invalid selection:", message);
      }
    }

    /// <summary>
    /// This method deletes the given node from the listview,
    /// and updates the views.
    /// </summary>
    /// <param name="treeNode">The <see cref="SlideshowTreeNode"/> to delete.</param>
    private void DeleteNode(SlideshowTreeNode treeNode)
    {
      if (treeNode == null)
      {
        return;
      }

      Slide deleteSlide = treeNode.Slide;
      if (deleteSlide != null)
      {
        deleteSlide.Dispose();
      }

      foreach (SlideshowTreeNode subNode in treeNode.Nodes)
      {
        this.DeleteNode(subNode);
      }

      // Update UrlToID dictionary of BrowserTreeNodes
      if (treeNode.Parent is BrowserTreeNode)
      {
        BrowserTreeNode parent = (BrowserTreeNode)treeNode.Parent;
        string keyToRemove = string.Empty;
        foreach (KeyValuePair<string, int> urlToID in parent.UrlToID)
        {
          if (urlToID.Value == Convert.ToInt32(treeNode.Name))
          {
            keyToRemove = urlToID.Key;
          }
        }

        if (keyToRemove != string.Empty)
        {
          parent.UrlToID.Remove(keyToRemove);
        }
      }

      // Update TreeView.
      treeNode.Remove();
      this.UpdateListView(this.trvSlideshow.SelectedNodes);
    }

    /// <summary>
    /// This method iterates the slideshow tree to retrieve
    /// the node with the given name.
    /// </summary>
    /// <param name="name">A <see cref="String"/> with a node name to search for</param>
    /// <returns>The <see cref="SlideshowTreeNode"/> with the given name,
    /// or null if no one is found.</returns>
    private SlideshowTreeNode GetTreeNodeByName(string name)
    {
      return Slideshow.IterateTreeNodes(name, (SlideshowTreeNode)this.trvSlideshow.Nodes[0], false);
    }

    /// <summary>
    /// This method iterates the slideshow tree to retrieve
    /// the node with the given node id.
    /// </summary>
    /// <param name="nodeID">A <see cref="String"/> with a node id to search for</param>
    /// <returns>The <see cref="SlideshowTreeNode"/> with the given id,
    /// or null if no one is found.</returns>
    private SlideshowTreeNode GetTreeNodeByID(string nodeID)
    {
      return Slideshow.IterateTreeNodes(nodeID, (SlideshowTreeNode)this.trvSlideshow.Nodes[0], true);
    }

    /// <summary>
    /// Submits given <see cref="Slideshow"/> to the <see cref="TreeView"/> <see cref="trvSlideshow"/>.
    /// </summary>
    /// <param name="slideshow">A <see cref="Slideshow"/> with the slides to import.</param>
    private void PopulateTreeView(Slideshow slideshow)
    {
      this.Cursor = Cursors.WaitCursor;
      this.trvSlideshow.BeginUpdate();
      this.trvSlideshow.Nodes.Clear();
      this.trvSlideshow.Nodes.Add(slideshow);
      this.trvSlideshow.EndUpdate();
      this.trvSlideshow.ExpandAll();
      this.trvSlideshow.Nodes[0].EnsureVisible();
      this.Cursor = Cursors.Default;
    }

    #endregion //PRIVATEMETHODS
  }
}
