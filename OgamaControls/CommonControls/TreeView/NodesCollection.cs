using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace OgamaControls
{
  /// <summary>
  /// Collection of selected nodes.
  /// </summary>
  public class NodesCollection : List<TreeNode>
  {
    /// <summary>
    /// Event fired when a tree node has been added to the collection.
    /// </summary>
    internal event TreeNodeEventHandler TreeNodeAdded;

    /// <summary>
    /// Event fired when a tree node has been removed to the collection.
    /// </summary>
    internal event TreeNodeEventHandler TreeNodeRemoved;

    /// <summary>
    /// Event fired when a tree node has been inserted to the collection.
    /// </summary>
    internal event TreeNodeEventHandler TreeNodeInserted;

    /// <summary>
    /// Event fired the collection has been cleared.
    /// </summary>
    internal event EventHandler SelectedNodesCleared;

    /// <summary>
    /// Adds a tree node to the collection.
    /// </summary>
    /// <param name="treeNode">Tree node to add.</param>
    /// <returns>The position into which the new element was inserted.</returns>
    public new int Add(TreeNode treeNode)
    {
      if (TreeNodeAdded != null)
        TreeNodeAdded(treeNode);

      base.Add(treeNode);

      return this.Count-1;
    }

    /// <summary>
    /// Inserts a tree node at specified index.
    /// </summary>
    /// <param name="index">The position into which the new element has to be inserted.</param>
    /// <param name="treeNode">Tree node to insert.</param>
    public new void Insert(int index, TreeNode treeNode)
    {
      if (TreeNodeInserted != null)
        TreeNodeInserted(treeNode);

      base.Insert(index, treeNode);
    }

    /// <summary>
    /// Removed a tree node from the collection.
    /// </summary>
    /// <param name="treeNode">Tree node to remove.</param>
    public new void Remove(TreeNode treeNode)
    {
      if (TreeNodeRemoved != null)
        TreeNodeRemoved(treeNode);

      base.Remove(treeNode);
    }

    /// <summary>
    /// Occurs when collection is being cleared.
    /// </summary>
    public new void Clear()
    {
      if (SelectedNodesCleared != null)
        SelectedNodesCleared(this, EventArgs.Empty);

      base.Clear();
    }

    /// <summary>
    /// Sorts the TreeNodes in the collection by their TreeNode.Index
    /// </summary>
    public new void Sort()
    {
      TreeNodeComparer cp=new TreeNodeComparer();
      base.Sort(cp);
    }
  }

}