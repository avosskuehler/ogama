// <copyright file="CaptureMode.cs" company="FU Berlin">
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

namespace Ogama.MainWindow.ContextPanel
{
  partial class ContextPanel
  {
    /// <summary> 
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary> 
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (this.components != null))
      {
        this.components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      System.Windows.Forms.ColumnHeader columnCategorie;
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ContextPanel));
      this.pnlContextTabs = new System.Windows.Forms.TabControl();
      this.tbpTrialTreeView = new System.Windows.Forms.TabPage();
      this.trvTrials = new OgamaControls.MultiselectTreeView();
      this.imlSlideTypes = new System.Windows.Forms.ImageList(this.components);
      this.tbpTrialList = new System.Windows.Forms.TabPage();
      this.lsvTrials = new OgamaControls.ObjectListView();
      this.columnTrials = ((OgamaControls.OLVColumn)(new OgamaControls.OLVColumn()));
      this.tabPageSubjectList = new System.Windows.Forms.TabPage();
      this.lsvSubjects = new System.Windows.Forms.ListView();
      this.columnSubjectName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.columnAge = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.columnComments = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.tabPageHelp = new System.Windows.Forms.TabPage();
      this.tableLayoutPanelHelp = new System.Windows.Forms.TableLayoutPanel();
      this.rtbHelpInterface = new System.Windows.Forms.RichTextBox();
      this.panel1 = new System.Windows.Forms.Panel();
      this.pcbInterfaceLogo = new System.Windows.Forms.PictureBox();
      this.lblHelpInterface = new System.Windows.Forms.Label();
      this.imlTabIcons = new System.Windows.Forms.ImageList(this.components);
      this.cmuSlides = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.mnuDelete = new System.Windows.Forms.ToolStripMenuItem();
      this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
      this.spcTabsVideo = new System.Windows.Forms.SplitContainer();
      this.grpUsercam = new System.Windows.Forms.GroupBox();
      this.avpUsercam = new OgamaControls.AVPlayer();
      columnCategorie = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.pnlContextTabs.SuspendLayout();
      this.tbpTrialTreeView.SuspendLayout();
      this.tbpTrialList.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.lsvTrials)).BeginInit();
      this.tabPageSubjectList.SuspendLayout();
      this.tabPageHelp.SuspendLayout();
      this.tableLayoutPanelHelp.SuspendLayout();
      this.panel1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pcbInterfaceLogo)).BeginInit();
      this.cmuSlides.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.spcTabsVideo)).BeginInit();
      this.spcTabsVideo.Panel1.SuspendLayout();
      this.spcTabsVideo.Panel2.SuspendLayout();
      this.spcTabsVideo.SuspendLayout();
      this.grpUsercam.SuspendLayout();
      this.SuspendLayout();
      // 
      // columnCategorie
      // 
      columnCategorie.Text = "Group";
      // 
      // pnlContextTabs
      // 
      this.pnlContextTabs.Controls.Add(this.tbpTrialTreeView);
      this.pnlContextTabs.Controls.Add(this.tbpTrialList);
      this.pnlContextTabs.Controls.Add(this.tabPageSubjectList);
      this.pnlContextTabs.Controls.Add(this.tabPageHelp);
      this.pnlContextTabs.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pnlContextTabs.HotTrack = true;
      this.pnlContextTabs.ImageList = this.imlTabIcons;
      this.pnlContextTabs.ItemSize = new System.Drawing.Size(20, 22);
      this.pnlContextTabs.Location = new System.Drawing.Point(0, 0);
      this.pnlContextTabs.Margin = new System.Windows.Forms.Padding(0);
      this.pnlContextTabs.Name = "pnlContextTabs";
      this.pnlContextTabs.Padding = new System.Drawing.Point(0, 0);
      this.pnlContextTabs.SelectedIndex = 0;
      this.pnlContextTabs.ShowToolTips = true;
      this.pnlContextTabs.Size = new System.Drawing.Size(200, 250);
      this.pnlContextTabs.TabIndex = 6;
      this.toolTip1.SetToolTip(this.pnlContextTabs, "This is an image and \r\nsubject browser panel.");
      // 
      // tbpTrialTreeView
      // 
      this.tbpTrialTreeView.Controls.Add(this.trvTrials);
      this.tbpTrialTreeView.ImageKey = "StimuliList";
      this.tbpTrialTreeView.Location = new System.Drawing.Point(4, 26);
      this.tbpTrialTreeView.Name = "tbpTrialTreeView";
      this.tbpTrialTreeView.Size = new System.Drawing.Size(192, 220);
      this.tbpTrialTreeView.TabIndex = 3;
      this.tbpTrialTreeView.UseVisualStyleBackColor = true;
      // 
      // trvTrials
      // 
      this.trvTrials.Dock = System.Windows.Forms.DockStyle.Fill;
      this.trvTrials.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawAll;
      this.trvTrials.ImageKey = "Folder";
      this.trvTrials.ImageList = this.imlSlideTypes;
      this.trvTrials.Location = new System.Drawing.Point(0, 0);
      this.trvTrials.Name = "trvTrials";
      this.trvTrials.SelectedImageIndex = 0;
      this.trvTrials.SelectionBackColor = System.Drawing.SystemColors.Highlight;
      this.trvTrials.SelectionMode = OgamaControls.TreeViewSelectionMode.SingleSelect;
      this.trvTrials.Size = new System.Drawing.Size(192, 220);
      this.trvTrials.TabIndex = 0;
      this.trvTrials.DrawNode += new System.Windows.Forms.DrawTreeNodeEventHandler(this.trvTrials_DrawNode);
      this.trvTrials.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.trvTrials_AfterSelect);
      // 
      // imlSlideTypes
      // 
      this.imlSlideTypes.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imlSlideTypes.ImageStream")));
      this.imlSlideTypes.TransparentColor = System.Drawing.Color.Transparent;
      this.imlSlideTypes.Images.SetKeyName(0, "Instructions");
      this.imlSlideTypes.Images.SetKeyName(1, "Images");
      this.imlSlideTypes.Images.SetKeyName(2, "Shapes");
      this.imlSlideTypes.Images.SetKeyName(3, "MixedMedia");
      this.imlSlideTypes.Images.SetKeyName(4, "Flash");
      this.imlSlideTypes.Images.SetKeyName(5, "Blank");
      this.imlSlideTypes.Images.SetKeyName(6, "Slide");
      this.imlSlideTypes.Images.SetKeyName(7, "CategoryClosed");
      this.imlSlideTypes.Images.SetKeyName(8, "CategoryOpen");
      this.imlSlideTypes.Images.SetKeyName(9, "Rtf");
      this.imlSlideTypes.Images.SetKeyName(10, "Trial");
      this.imlSlideTypes.Images.SetKeyName(11, "Folder");
      this.imlSlideTypes.Images.SetKeyName(12, "Browser");
      this.imlSlideTypes.Images.SetKeyName(13, "Desktop");
      // 
      // tbpTrialList
      // 
      this.tbpTrialList.Controls.Add(this.lsvTrials);
      this.tbpTrialList.ImageKey = "StimuliPictures";
      this.tbpTrialList.Location = new System.Drawing.Point(4, 26);
      this.tbpTrialList.Margin = new System.Windows.Forms.Padding(0);
      this.tbpTrialList.Name = "tbpTrialList";
      this.tbpTrialList.Size = new System.Drawing.Size(192, 220);
      this.tbpTrialList.TabIndex = 0;
      this.tbpTrialList.UseVisualStyleBackColor = true;
      // 
      // lsvTrials
      // 
      this.lsvTrials.AllColumns.Add(this.columnTrials);
      this.lsvTrials.AlternateRowBackColor = System.Drawing.Color.Empty;
      this.lsvTrials.BackColor = System.Drawing.Color.White;
      this.lsvTrials.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lsvTrials.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnTrials});
      this.lsvTrials.Dock = System.Windows.Forms.DockStyle.Fill;
      this.lsvTrials.EmptyListMsg = "No Trials defined.";
      this.lsvTrials.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lsvTrials.HideSelection = false;
      this.lsvTrials.Location = new System.Drawing.Point(0, 0);
      this.lsvTrials.Margin = new System.Windows.Forms.Padding(0);
      this.lsvTrials.MultiSelect = false;
      this.lsvTrials.Name = "lsvTrials";
      this.lsvTrials.OwnerDraw = true;
      this.lsvTrials.ShowGroups = false;
      this.lsvTrials.Size = new System.Drawing.Size(192, 220);
      this.lsvTrials.TabIndex = 2;
      this.toolTip1.SetToolTip(this.lsvTrials, "Select the trial you wish to \r\nbe analyzed in the modules.");
      this.lsvTrials.UseCompatibleStateImageBehavior = false;
      this.lsvTrials.View = System.Windows.Forms.View.Tile;
      this.lsvTrials.SelectedIndexChanged += new System.EventHandler(this.lsvTrials_SelectedIndexChanged);
      // 
      // columnTrials
      // 
      this.columnTrials.AspectName = null;
      this.columnTrials.IsTileViewColumn = true;
      this.columnTrials.Text = "Trials";
      // 
      // tabPageSubjectList
      // 
      this.tabPageSubjectList.Controls.Add(this.lsvSubjects);
      this.tabPageSubjectList.ImageKey = "User";
      this.tabPageSubjectList.Location = new System.Drawing.Point(4, 26);
      this.tabPageSubjectList.Name = "tabPageSubjectList";
      this.tabPageSubjectList.Size = new System.Drawing.Size(192, 220);
      this.tabPageSubjectList.TabIndex = 4;
      this.tabPageSubjectList.UseVisualStyleBackColor = true;
      // 
      // lsvSubjects
      // 
      this.lsvSubjects.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnSubjectName,
            columnCategorie,
            this.columnAge,
            this.columnComments});
      this.lsvSubjects.Dock = System.Windows.Forms.DockStyle.Fill;
      this.lsvSubjects.FullRowSelect = true;
      this.lsvSubjects.Location = new System.Drawing.Point(0, 0);
      this.lsvSubjects.Name = "lsvSubjects";
      this.lsvSubjects.Size = new System.Drawing.Size(192, 220);
      this.lsvSubjects.TabIndex = 0;
      this.toolTip1.SetToolTip(this.lsvSubjects, "Select the subject you wish to \r\nbe analyzed in the modules.");
      this.lsvSubjects.UseCompatibleStateImageBehavior = false;
      this.lsvSubjects.View = System.Windows.Forms.View.Details;
      this.lsvSubjects.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lsvSubjects_ColumnClick);
      this.lsvSubjects.SelectedIndexChanged += new System.EventHandler(this.lsvSubjects_SelectedIndexChanged);
      // 
      // columnSubjectName
      // 
      this.columnSubjectName.Text = "Name";
      // 
      // columnAge
      // 
      this.columnAge.Text = "Age";
      // 
      // tabPageHelp
      // 
      this.tabPageHelp.Controls.Add(this.tableLayoutPanelHelp);
      this.tabPageHelp.ImageKey = "Help";
      this.tabPageHelp.Location = new System.Drawing.Point(4, 26);
      this.tabPageHelp.Margin = new System.Windows.Forms.Padding(0);
      this.tabPageHelp.Name = "tabPageHelp";
      this.tabPageHelp.Size = new System.Drawing.Size(192, 220);
      this.tabPageHelp.TabIndex = 1;
      this.tabPageHelp.UseVisualStyleBackColor = true;
      // 
      // tableLayoutPanelHelp
      // 
      this.tableLayoutPanelHelp.BackColor = System.Drawing.SystemColors.Control;
      this.tableLayoutPanelHelp.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Inset;
      this.tableLayoutPanelHelp.ColumnCount = 1;
      this.tableLayoutPanelHelp.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanelHelp.Controls.Add(this.rtbHelpInterface, 0, 1);
      this.tableLayoutPanelHelp.Controls.Add(this.panel1, 0, 0);
      this.tableLayoutPanelHelp.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tableLayoutPanelHelp.Location = new System.Drawing.Point(0, 0);
      this.tableLayoutPanelHelp.Name = "tableLayoutPanelHelp";
      this.tableLayoutPanelHelp.RowCount = 2;
      this.tableLayoutPanelHelp.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
      this.tableLayoutPanelHelp.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanelHelp.Size = new System.Drawing.Size(192, 220);
      this.tableLayoutPanelHelp.TabIndex = 5;
      // 
      // rtbHelpInterface
      // 
      this.rtbHelpInterface.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.rtbHelpInterface.Dock = System.Windows.Forms.DockStyle.Fill;
      this.rtbHelpInterface.Location = new System.Drawing.Point(2, 52);
      this.rtbHelpInterface.Margin = new System.Windows.Forms.Padding(0);
      this.rtbHelpInterface.Name = "rtbHelpInterface";
      this.rtbHelpInterface.Size = new System.Drawing.Size(188, 166);
      this.rtbHelpInterface.TabIndex = 4;
      this.rtbHelpInterface.Text = "";
      // 
      // panel1
      // 
      this.panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
      this.panel1.Controls.Add(this.pcbInterfaceLogo);
      this.panel1.Controls.Add(this.lblHelpInterface);
      this.panel1.Location = new System.Drawing.Point(2, 2);
      this.panel1.Margin = new System.Windows.Forms.Padding(0);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(188, 48);
      this.panel1.TabIndex = 3;
      // 
      // pcbInterfaceLogo
      // 
      this.pcbInterfaceLogo.BackColor = System.Drawing.Color.White;
      this.pcbInterfaceLogo.Image = global::Ogama.Properties.Resources.DatabaseLogo;
      this.pcbInterfaceLogo.Location = new System.Drawing.Point(0, 0);
      this.pcbInterfaceLogo.Name = "pcbInterfaceLogo";
      this.pcbInterfaceLogo.Padding = new System.Windows.Forms.Padding(8);
      this.pcbInterfaceLogo.Size = new System.Drawing.Size(48, 48);
      this.pcbInterfaceLogo.TabIndex = 1;
      this.pcbInterfaceLogo.TabStop = false;
      // 
      // lblHelpInterface
      // 
      this.lblHelpInterface.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblHelpInterface.Location = new System.Drawing.Point(64, 9);
      this.lblHelpInterface.Margin = new System.Windows.Forms.Padding(0);
      this.lblHelpInterface.Name = "lblHelpInterface";
      this.lblHelpInterface.Size = new System.Drawing.Size(121, 39);
      this.lblHelpInterface.TabIndex = 2;
      this.lblHelpInterface.Text = "Module name";
      // 
      // imlTabIcons
      // 
      this.imlTabIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imlTabIcons.ImageStream")));
      this.imlTabIcons.TransparentColor = System.Drawing.Color.Transparent;
      this.imlTabIcons.Images.SetKeyName(0, "StimuliList");
      this.imlTabIcons.Images.SetKeyName(1, "StimuliPictures");
      this.imlTabIcons.Images.SetKeyName(2, "Help");
      this.imlTabIcons.Images.SetKeyName(3, "User");
      // 
      // cmuSlides
      // 
      this.cmuSlides.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuDelete});
      this.cmuSlides.Name = "cmuSlides";
      this.cmuSlides.Size = new System.Drawing.Size(132, 26);
      // 
      // mnuDelete
      // 
      this.mnuDelete.Image = global::Ogama.Properties.Resources.DeleteHS;
      this.mnuDelete.Name = "mnuDelete";
      this.mnuDelete.ShortcutKeys = System.Windows.Forms.Keys.Delete;
      this.mnuDelete.Size = new System.Drawing.Size(131, 22);
      this.mnuDelete.Text = "Delete";
      // 
      // spcTabsVideo
      // 
      this.spcTabsVideo.Dock = System.Windows.Forms.DockStyle.Fill;
      this.spcTabsVideo.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
      this.spcTabsVideo.Location = new System.Drawing.Point(0, 0);
      this.spcTabsVideo.Name = "spcTabsVideo";
      this.spcTabsVideo.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // spcTabsVideo.Panel1
      // 
      this.spcTabsVideo.Panel1.Controls.Add(this.pnlContextTabs);
      this.spcTabsVideo.Panel1MinSize = 200;
      // 
      // spcTabsVideo.Panel2
      // 
      this.spcTabsVideo.Panel2.Controls.Add(this.grpUsercam);
      this.spcTabsVideo.Panel2MinSize = 50;
      this.spcTabsVideo.Size = new System.Drawing.Size(200, 430);
      this.spcTabsVideo.SplitterDistance = 250;
      this.spcTabsVideo.TabIndex = 1;
      // 
      // grpUsercam
      // 
      this.grpUsercam.Controls.Add(this.avpUsercam);
      this.grpUsercam.Dock = System.Windows.Forms.DockStyle.Fill;
      this.grpUsercam.Location = new System.Drawing.Point(0, 0);
      this.grpUsercam.Name = "grpUsercam";
      this.grpUsercam.Size = new System.Drawing.Size(200, 176);
      this.grpUsercam.TabIndex = 0;
      this.grpUsercam.TabStop = false;
      this.grpUsercam.Text = "Usercam";
      // 
      // avpUsercam
      // 
      this.avpUsercam.BackColor = System.Drawing.Color.Black;
      this.avpUsercam.Dock = System.Windows.Forms.DockStyle.Fill;
      this.avpUsercam.Location = new System.Drawing.Point(3, 16);
      this.avpUsercam.Name = "avpUsercam";
      this.avpUsercam.PlaybackRate = 1D;
      this.avpUsercam.Size = new System.Drawing.Size(194, 157);
      this.avpUsercam.TabIndex = 0;
      // 
      // ContextPanel
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.spcTabsVideo);
      this.Name = "ContextPanel";
      this.Size = new System.Drawing.Size(200, 430);
      this.pnlContextTabs.ResumeLayout(false);
      this.tbpTrialTreeView.ResumeLayout(false);
      this.tbpTrialList.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.lsvTrials)).EndInit();
      this.tabPageSubjectList.ResumeLayout(false);
      this.tabPageHelp.ResumeLayout(false);
      this.tableLayoutPanelHelp.ResumeLayout(false);
      this.panel1.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.pcbInterfaceLogo)).EndInit();
      this.cmuSlides.ResumeLayout(false);
      this.spcTabsVideo.Panel1.ResumeLayout(false);
      this.spcTabsVideo.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.spcTabsVideo)).EndInit();
      this.spcTabsVideo.ResumeLayout(false);
      this.grpUsercam.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.TabPage tbpTrialTreeView;
    private System.Windows.Forms.TabPage tabPageHelp;
    private System.Windows.Forms.TableLayoutPanel tableLayoutPanelHelp;
    private System.Windows.Forms.RichTextBox rtbHelpInterface;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.PictureBox pcbInterfaceLogo;
    private System.Windows.Forms.Label lblHelpInterface;
    private System.Windows.Forms.TabPage tbpTrialList;
    private OgamaControls.ObjectListView lsvTrials;
    private System.Windows.Forms.TabPage tabPageSubjectList;
    private System.Windows.Forms.ListView lsvSubjects;
    private System.Windows.Forms.ColumnHeader columnSubjectName;
    private System.Windows.Forms.ColumnHeader columnAge;
    private System.Windows.Forms.ColumnHeader columnComments;
    private System.Windows.Forms.ImageList imlTabIcons;
    private System.Windows.Forms.TabControl pnlContextTabs;
    private System.Windows.Forms.ToolTip toolTip1;
    private System.Windows.Forms.SplitContainer spcTabsVideo;
    private System.Windows.Forms.GroupBox grpUsercam;
    private OgamaControls.AVPlayer avpUsercam;
    private System.Windows.Forms.ContextMenuStrip cmuSlides;
    private System.Windows.Forms.ToolStripMenuItem mnuDelete;
    private OgamaControls.MultiselectTreeView trvTrials;
    private OgamaControls.OLVColumn columnTrials;
    private System.Windows.Forms.ImageList imlSlideTypes;
  }
}
