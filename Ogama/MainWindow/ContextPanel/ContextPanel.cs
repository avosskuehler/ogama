// <copyright file="ContextPanel.cs" company="FU Berlin">
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

namespace Ogama.MainWindow.ContextPanel
{
  using System;
  using System.Collections;
  using System.Data;
  using System.Drawing;
  using System.Windows.Forms;

  using Ogama.Modules.Common.CustomRenderer;
  using Ogama.Modules.Common.SlideCollections;

  using OgamaControls;

  using VectorGraphics.Elements.ElementCollections;

  /// <summary>
  /// Derived from <see cref="UserControl"/>.
  /// A component for displaying context information.
  /// This includes a help text tab, 
  /// a subject choose tab and two stimulus choosing tabs.
  /// </summary>
  public partial class ContextPanel : UserControl
  {
    ///////////////////////////////////////////////////////////////////////////////
    // Defining Constants                                                        //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTANTS

    /// <summary>
    /// Size of slide thumbs.
    /// </summary>
    private static Size contextPanelThumbSize = new Size(128, 96);

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

    /// <summary>
    /// Initializes a new instance of the ContextPanel class.
    /// </summary>
    public ContextPanel()
    {
      this.InitializeComponent();
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Enumerations                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region ENUMS

    /// <summary>
    /// Support for thread-safe version of <see cref="RepopulateSubjectTab()"/>
    /// </summary>
    private delegate void RepopulateSubjectTabDelegate();

    #endregion ENUMS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Sets the thumb size of the context panel
    /// </summary>
    /// <value>A <see cref="Size"/> with the thumb size of the context panel.</value>
    public static Size ContextPanelThumbSize
    {
      set { contextPanelThumbSize = value; }
    }

    /// <summary>
    /// Gets or sets the caption of the help tab.
    /// </summary>
    /// <value>A <see cref="string"/> with the caption for the help tab.</value>
    public string HelpTabCaption
    {
      get { return this.lblHelpInterface.Text; }
      set { this.lblHelpInterface.Text = value; }
    }

    /// <summary>
    /// Gets the logo of the help tab.
    /// </summary>
    /// <value>A <see cref="PictureBox"/> which displays the logo
    /// in the help <see cref="TabPage"/>.</value>
    public PictureBox HelpTabLogo
    {
      get { return this.pcbInterfaceLogo; }
    }

    /// <summary>
    /// Gets rich text box of help tab to fill it with help text.
    /// </summary>
    /// <value>A <see cref="RichTextBox"/> which displays the help
    /// text in the help <see cref="TabPage"/>.</value>
    public RichTextBox HelpTabRichTextBox
    {
      get { return this.rtbHelpInterface; }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to show or hide the usercam control.
    /// </summary>
    /// <value><strong>True</strong>, if user cam should be visible otherwise <strong>false</strong>.</value>
    public bool IsUsercamVisible
    {
      get { return !this.spcTabsVideo.Panel2Collapsed; }
      set { this.spcTabsVideo.Panel2Collapsed = !value; }
    }

    /// <summary>
    /// Gets the <see cref="AVPlayer"/> of this context panel.
    /// </summary>
    /// <value>The <see cref="AVPlayer"/> of this context panel.</value>
    public AVPlayer AVPlayer
    {
      get { return this.avpUsercam; }
    }

    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Public methods                                                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region PUBLICMETHODS

    /// <summary>
    /// Initializes context panel and updates the tab pages.
    /// </summary>
    public void Init()
    {
      this.InitializeTrialListView();
      this.PopulateTrialTreeView();
      this.PopulateTrialListView();
      this.PopulateSubjectListView();
      this.Refresh();
    }

    /// <summary>
    /// Removes all entrys from tab pages.
    /// </summary>
    public void Clear()
    {
      this.lsvSubjects.Items.Clear();
      this.trvTrials.Nodes.Clear();
      this.lsvTrials.ClearObjects();
      this.lblHelpInterface.Text = string.Empty;
      this.rtbHelpInterface.Text = string.Empty;
    }

    /// <summary>
    /// Selects the help tab of the context panel.
    /// </summary>
    public void SelectHelpTab()
    {
      this.pnlContextTabs.SelectTab(this.tabPageHelp);
    }

    /// <summary>
    /// Fills the subjects from the database table into the list view.
    /// </summary>
    public void RepopulateSubjectTab()
    {
      if (this.InvokeRequired)
      {
        RepopulateSubjectTabDelegate repopulateSubjectTabDelegate =
          new RepopulateSubjectTabDelegate(this.RepopulateSubjectTab);
        this.Invoke(repopulateSubjectTabDelegate);
        return;
      }

      this.PopulateSubjectListView();
      this.Refresh();
    }

    /// <summary>
    /// Fills the file list with the images from the stimuli path.
    /// </summary>
    public void RepopulateImageListTab()
    {
      this.PopulateTrialListView();
      this.Refresh();
    }

    /// <summary>
    /// Calls a new thumb creation and updates list view.
    /// </summary>
    public void RepopulateThumbsListTab()
    {
      this.Cursor = Cursors.WaitCursor;

      // Resets thumb property, so during next call in
      // PopulateTrialTreeView the thumbs will be recalculated.
      foreach (Slide slide in Document.ActiveDocument.ExperimentSettings.SlideShow.Slides)
      {
        if (slide.IsThumbNull)
        {
          continue;
        }

        slide.Thumb.Dispose();
        slide.Thumb = null;
      }

      this.PopulateTrialTreeView();
      this.Refresh();
      this.Cursor = Cursors.Default;
    }

    #endregion //PUBLICMETHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler                                                              //
    ///////////////////////////////////////////////////////////////////////////////
    #region EVENTS

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler for UI, Menu, Buttons, Toolbars etc.                         //
    ///////////////////////////////////////////////////////////////////////////////
    #region WINDOWSEVENTHANDLER

    /// <summary>
    /// The <see cref="ListView.SelectedIndexChanged"/> event handler
    /// for the <see cref="ListView"/> <see cref="lsvTrials"/>.
    /// User selected a new stimulus image from stimulus thumbs view.
    /// Raises <see cref="MainForm.StimulusChanged"/> event.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void lsvTrials_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (this.lsvTrials.SelectedItems.Count == 1)
      {
        // Update documents selection state with Trial
        Document.ActiveDocument.SelectionState.Update(
          null,
          ((Trial)this.lsvTrials.SelectedObject).ID,
          null,
          null);

        // Raise Event
        ((MainForm)this.Parent).OnStimulusChanged(null);
      }
    }

    /// <summary>
    /// The <see cref="ListView.SelectedIndexChanged"/> event handler
    /// for the <see cref="ListView"/> <see cref="lsvSubjects"/>.
    /// User selected a new subject from subjects list view.
    /// Raises <see cref="MainForm.StimulusChanged"/> event.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void lsvSubjects_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (this.lsvSubjects.SelectedItems.Count == 1)
      {
        // Update documents selection state with subject name
        Document.ActiveDocument.SelectionState.Update(
          this.lsvSubjects.SelectedItems[0].Text,
          null,
          null,
          null);

        // Raise Event
        ((MainForm)this.Parent).OnStimulusChanged(null);
      }
    }

    /// <summary>
    /// The <see cref="TreeView.AfterSelect"/> event handler
    /// for the <see cref="TreeView"/> <see cref="trvTrials"/>.
    /// User selected a new stimulus image from stimulus tree view.
    /// Raises <see cref="MainForm.StimulusChanged"/> event.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void trvTrials_AfterSelect(object sender, TreeViewEventArgs e)
    {
      int trialID = -1;
      if (e.Node.Parent != null && e.Node.Parent.Tag != null && e.Node.Parent.Tag.ToString() == "Trial")
      {
        trialID = Convert.ToInt32(e.Node.Parent.Name);
      }
      else if (((SlideshowTreeNode)e.Node).Slide != null)
      {
        trialID = Convert.ToInt32(e.Node.Name);
      }
      else if (e.Node.Nodes.Count > 0 && ((SlideshowTreeNode)e.Node.Nodes[0]).Slide != null)
      {
        if (e.Node.Tag != null && e.Node.Tag.ToString() == "Trial")
        {
          trialID = Convert.ToInt32(e.Node.Name);
        }
        else
        {
          trialID = Convert.ToInt32(e.Node.Nodes[0].Name);
        }
      }

      if (trialID >= 0)
      {
        // Update documents selection state with trial id
        Document.ActiveDocument.SelectionState.Update(null, trialID, null, null);

        // Raise Event
        ((MainForm)this.Parent).OnStimulusChanged(null);
      }
    }

    /// <summary>
    /// The <see cref="TreeView.DrawNode"/> event handler
    /// for the <see cref="TreeView"/> <see cref="trvTrials"/>.
    /// Adds drawing of trial icons to default drawing behaviour.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void trvTrials_DrawNode(object sender, DrawTreeNodeEventArgs e)
    {
      e.DrawDefault = true;

      Rectangle idBounds = e.Node.Bounds;
      idBounds.Offset(e.Node.Bounds.Width + 2, 0);
      string idString = "(ID:" + e.Node.Name + ")";
      e.Graphics.DrawString(idString, this.trvTrials.Font, Brushes.Black, idBounds.Location);
      SizeF textSize = e.Graphics.MeasureString(idString, this.trvTrials.Font);

      // If the randomize flag is set, draw a dice icon 
      // to the right of the label text.
      bool randomize = ((SlideshowTreeNode)e.Node).Randomize;
      if (randomize)
      {
        Rectangle diceBounds = e.Node.Bounds;
        diceBounds.Offset(e.Node.Bounds.Width + 10 + (int)textSize.Width, 0);
        Slideshow.DrawDice(e.Graphics, diceBounds.Location);
      }
    }

    /// <summary>
    /// The <see cref="ListView.ColumnClick"/> event handler
    /// for the <see cref="ListView"/> <see cref="lsvSubjects"/>.
    /// User clicked on column title, so reorder entrys according to selected column.
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">A <see cref="ColumnClickEventArgs"/> with the event data.</param>
    private void lsvSubjects_ColumnClick(object sender, ColumnClickEventArgs e)
    {
      this.lsvSubjects.ListViewItemSorter = new ListViewItemComparer(e.Column);
    }

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
    // Inherited methods                                                         //
    ///////////////////////////////////////////////////////////////////////////////
    #region OVERRIDES
    #endregion //OVERRIDES

    ///////////////////////////////////////////////////////////////////////////////
    // Methods for doing main class job                                          //
    ///////////////////////////////////////////////////////////////////////////////
    #region METHODS

    /// <summary>
    /// This method initializes the <see cref="ObjectListView"/>
    /// tile mode with the property delegates.
    /// </summary>
    private void InitializeTrialListView()
    {
      this.columnTrials.AspectGetter = delegate(object row)
      {
        return ((Trial)row).ID;
      };

      this.columnTrials.AspectToStringConverter = delegate(object cellValue)
      {
        return cellValue.ToString();
      };

      this.columnTrials.Renderer = new TrialRenderer();

      Size tileSize = contextPanelThumbSize;
      tileSize.Width += 15;
      tileSize.Height += 40;

      this.lsvTrials.TileSize = tileSize;
      this.lsvTrials.View = View.Tile;
    }

    /// <summary>
    /// Populates stimulus image list view with entrys of stimulus directory.
    /// </summary>
    /// <remarks>Unknown file formats were marked with an "unknown" image.</remarks>
    private void PopulateTrialListView()
    {
      this.InitializeTrialListView();
      this.lsvTrials.SetObjects(Document.ActiveDocument.ExperimentSettings.SlideShow.Trials);
    }

    /// <summary>
    /// Populates subject list view with entrys of 
    /// <see cref="Ogama.DataSet.OgamaDataSet.SubjectsDataTable"/>subjects table.
    /// The columns SubjectName, Category, Age and Comments
    /// are shown in the list view detail view.
    /// </summary>
    private void PopulateSubjectListView()
    {
      DataTable subjectsTable = Document.ActiveDocument.DocDataSet.SubjectsAdapter.GetData();
      if (subjectsTable != null)
      {
        ListViewItem.ListViewSubItem[] subItems;
        ListViewItem item = null;

        this.lsvSubjects.Items.Clear();

        foreach (DataRow subjectRow in subjectsTable.Rows)
        {
          string strSubjectName = subjectRow.IsNull("SubjectName") ? string.Empty : (string)subjectRow["SubjectName"];
          string strGroup = subjectRow.IsNull("Category") ? string.Empty : (string)subjectRow["Category"];
          string strAge = subjectRow.IsNull("Age") ? string.Empty : ((int)subjectRow["Age"]).ToString();
          string strComments = subjectRow.IsNull("Comments") ? string.Empty : (string)subjectRow["Comments"];

          item = new ListViewItem((string)subjectRow["SubjectName"], "User");

          subItems = new ListViewItem.ListViewSubItem[]
            { 
              new ListViewItem.ListViewSubItem(item, strGroup),
              new ListViewItem.ListViewSubItem(item, strAge),
              new ListViewItem.ListViewSubItem(item, strComments)
            };

          item.SubItems.AddRange(subItems);
          this.lsvSubjects.Items.Add(item);
        }

        this.lsvSubjects.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
      }

      this.lsvSubjects.ListViewItemSorter = new ListViewItemComparer(0);
    }

    /// <summary>
    /// Loads stimuli thumb images from thumb file into the <see cref="ListView"/>.
    /// </summary>
    private void PopulateTrialTreeView()
    {
      if (this.trvTrials != null && Document.ActiveDocument != null)
      {
        this.trvTrials.Nodes.Clear();
        this.trvTrials.Nodes.Add((Slideshow)Document.ActiveDocument.ExperimentSettings.SlideShow.Clone());
        this.trvTrials.ExpandAll();
      }
    }

    #endregion //METHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER

    /// <summary>
    /// Class. Implements the manual sorting of items by columns.
    /// </summary>
    private class ListViewItemComparer : IComparer
    {
      /// <summary>
      /// Saves the column number.
      /// </summary>
      private int col;

      /// <summary>
      /// Initializes a new instance of the ListViewItemComparer class.
      /// </summary>
      public ListViewItemComparer()
      {
        this.col = 0;
      }

      /// <summary>
      /// Initializes a new instance of the ListViewItemComparer class.
      /// </summary>
      /// <param name="column">The number of the column to sort for.</param>
      public ListViewItemComparer(int column)
      {
        this.col = column;
      }

      /// <summary>
      /// Compares two objects and returns a value indicating 
      /// whether one is less than, equal to, or greater than the other.
      /// </summary>
      /// <param name="x">The first <see cref="object"/> to compare. </param>
      /// <param name="y">The second <see cref="object"/> to compare. </param>
      /// <returns>"Less than zero" if x is less than y. 
      /// "Zero" if x equals y. and "Greater than zero", if
      /// x is greater than y.</returns>
      public int Compare(object x, object y)
      {
        return string.Compare(
          ((ListViewItem)x).SubItems[this.col].Text,
          ((ListViewItem)y).SubItems[this.col].Text);
      }
    }

    #endregion //HELPER
  }
}
